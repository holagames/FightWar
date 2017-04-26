#if !(UNITY_WP8 || UNITY_METRO || UNITY_WEBPLAYER)
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Threading;
using Unibill;
using Uniject;
#if !UNITY_WP8
using ICSharpCode.SharpZipLib.Zip;
#endif

namespace Unibill.Impl
{
    public class DownloadManager {
        private IUtil util;
        private IStorage storage;
        private IURLFetcher fetcher;
        private ILogger logger;
        private volatile string persistentDataPath;

        /// <summary>
        /// This is a list of file bundle identifiers from unibiller.com.
        /// </summary>
        private List<string> scheduledDownloads = new List<string>();
        private int bufferSize = DEFAULT_BUFFER_SIZE;
        private const string DOWNLOAD_TOKEN_URL = "http://cdn.unibiller.com/download_token";
        private const string SCHEDULED_DOWNLOADS_KEY = "com.outlinegames.unibill.scheduled_downloads";
        private const int DEFAULT_BUFFER_SIZE = 2000000;
        private byte[] BUFFER = new byte[(int)(DEFAULT_BUFFER_SIZE * 1.1)];
        private AutoResetEvent DATA_READY = new AutoResetEvent (false);
        private volatile bool UNPACK_FINISHED = false;
        private volatile bool DATA_FLUSHED = false;
        private volatile FileStream fileStream;
        private volatile int bytesReceived;
        private BillingPlatform platform;
        private string appSecret;
        private string appId;

        public event Action<string, string> onDownloadCompletedEvent;
        public event Action<string, string> onDownloadFailedEvent;
        public event Action<string, int> onDownloadProgressedEvent;

        public DownloadManager(IUtil util, IStorage storage, IURLFetcher fetcher, ILogger logger, BillingPlatform platform, string appSecret, String appId) {
            this.util = util;
            this.storage = storage;
            this.fetcher = fetcher;
            this.logger = logger;
            this.platform = platform;
            this.appSecret = appSecret;
            this.appId = appId;
            this.scheduledDownloads = deserialiseDownloads ();
            this.persistentDataPath = util.persistentDataPath;
            Thread t = new Thread (DownloadFlusher);
            t.IsBackground = true;
            t.Start ();
        }

        /// <summary>
        /// Sets the size of the download buffer.
        /// Only for testing.
        /// </summary>
        public void setBufferSize(int size) {
            this.bufferSize = size;
        }

        public void downloadContentFor(string fileBundleId, string receipt) {
            if (isDownloaded (fileBundleId)) {
                onDownloadCompletedEvent (fileBundleId, getContentPath (fileBundleId));
                return;
            }

            if (!scheduledDownloads.Contains (fileBundleId)) {
                createDataPathIfNecessary (fileBundleId);
                saveReceipt (fileBundleId, receipt);
                scheduledDownloads.Add (fileBundleId);
                serialiseDownloads ();
            }
        }

        public bool isDownloadScheduled(string bundleId) {
            return scheduledDownloads.Contains (bundleId);
        }

        /// <summary>
        /// Work on any scheduled downloads.
        /// Used for unit testing.
        /// </summary>
        public IEnumerator checkDownloads() {
            for (int t = 0; t < scheduledDownloads.Count; t++) {
                var scheduledDownload = scheduledDownloads [t];
                yield return util.InitiateCoroutine (download (scheduledDownload.ToString()));
            }
        }

        private UnityEngine.WaitForFixedUpdate waiter = new UnityEngine.WaitForFixedUpdate();

        /// <summary>
        /// Continuously monitor our scheduled downloads.
        /// </summary>
        public IEnumerator monitorDownloads() {
            while (true) {
                while (scheduledDownloads.Count > 0) {
                    // Removal from the work queue is the responsibility of download function.
                    yield return util.InitiateCoroutine (download (scheduledDownloads[0]));
                }
                yield return waiter;
            }
        }

        public int getQueueSize() {
            return scheduledDownloads.Count;
        }

        private List<string> deserialiseDownloads() {
            var strings = storage.GetString (SCHEDULED_DOWNLOADS_KEY, "[]").arrayListFromJson ();
            var result = new List<string> ();
            if (null != strings) {
                foreach (var s in strings) {
                    result.Add (s.ToString ());
                }
            }
            return result;
        }

        private void serialiseDownloads() {
            List<object> toSerialise = new List<object> ();
            foreach (var s in scheduledDownloads) {
                toSerialise.Add (s);
            }
            storage.SetString (SCHEDULED_DOWNLOADS_KEY, MiniJSON.jsonEncode (toSerialise));
        }
            
        private IEnumerator download(string bundleId) {
            // Do we have a partial download?
            createDataPathIfNecessary (bundleId);
            if (!File.Exists (getZipPath (bundleId))) {
                logger.Log (bundleId);
                string downloadToken = "";
                // Get a download token.
                {
                    Dictionary<string, string> parameters = new Dictionary<string, string> ();
                    try {
                        parameters.Add ("receipt", getReceipt (bundleId));
                    } catch (IOException) {
                        onDownloadFailedPermanently (bundleId, string.Format("Bundle {0} no longer defined in inventory!", bundleId));
                        yield break;
                    }

                    parameters.Add ("appId", appId);
                    parameters.Add ("bundleName", bundleId);
                    parameters.Add ("platform", platform.ToString ());
                    parameters.Add ("appSecret", appSecret);
                    parameters.Add ("version", getVersionToDownload (bundleId));
                    parameters.Add ("unibillVersion", AnalyticsReporter.UNIBILL_VERSION);

                    yield return fetcher.doPost (DOWNLOAD_TOKEN_URL, parameters);
                    var response = fetcher.getResponse ();
                    if (!string.IsNullOrEmpty (response.error)) {
                        logger.Log ("Error downloading content: {0}. Unibill will retry later.", response.error);
                        yield return getRandomSleep ();
                        yield break;
                    }

                    var downloadTokenHash = (Dictionary<string, object>)MiniJSON.jsonDecode (response.contentString);
                    if (null == downloadTokenHash) {
                        logger.Log ("Error fetching download token. Unibill will retry later.");
                        yield return getRandomSleep ();
                        yield break;
                    }

                    bool success = bool.Parse (downloadTokenHash ["success"].ToString ());
                    if (!success) {
                        logger.LogError ("Error downloading bundle {0}. Download abandoned.", bundleId);
                        var errorString = "";
                        if (downloadTokenHash.ContainsKey ("error")) {
                            errorString = downloadTokenHash ["error"].ToString ();
                            logger.LogError (errorString);
                        }
                        onDownloadFailedPermanently (bundleId, errorString);
                        yield break;
                    }

                    if (!downloadTokenHash.ContainsKey ("url")) {
                        logger.LogError ("Error fetching download token. Missing URL. Will retry");
                        yield return getRandomSleep ();
                        yield break;
                    }

                    downloadToken = downloadTokenHash ["url"].ToString ();
                    // Persist this version token for resumed downloads.
                    if (!downloadTokenHash.ContainsKey ("version")) {
                        logger.LogError ("Error fetching download token. Missing version. Will retry");
                        yield return getRandomSleep ();
                        yield break;
                    }
                    var version = downloadTokenHash ["version"].ToString ();
                    saveVersion (bundleId, version);
                }

                // Figure out the content length.
                // We can't do a HEAD because of Unity's wonderful
                // WWW class, so we do a 2 byte range GET and look at the headers.
                Dictionary<string, string> headers = new Dictionary<string, string> ();
                // These headers are required since on iOS
                // Unity wrongly adds if-modified headers that cause google to (rightly)
                // return content not modified.
                headers ["If-Modified-Since"] = "Tue, 1 Jan 1980 00:00:00 GMT";
                headers ["If-None-Match"] = "notanetag";
                long contentLength;
                {
                    headers ["Range"] = "bytes=0-1";
                    yield return fetcher.doGet (downloadToken, headers);
                    IHTTPRequest response = fetcher.getResponse ();

                    if (isContentNotFound(response)) {
                        string error = string.Format("404 - Downloadable Content missing for bundle {0}!", bundleId);
                        logger.LogError (error);
                        onDownloadFailedPermanently(bundleId, error);
                        yield break;
                    }

                    if (!response.responseHeaders.ContainsKey ("CONTENT-RANGE")) {
                        logger.LogError ("Malformed server response. Missing content-range");
                        logger.LogError (response.error);
                        yield return getRandomSleep ();
                        yield break;
                    }

                    string contentRange = response.responseHeaders ["CONTENT-RANGE"].ToString ();
                    contentLength = long.Parse (contentRange.Split (new char[] { '/' }, 2) [1]);
                }

                #if !UNITY_METRO

                //// Fetch the content.
                {
                    using (fileStream = openDownload (bundleId)) {
                        long rangeStart = fileStream.Length;
                        if (rangeStart > 0) {
                            fileStream.Seek (0, SeekOrigin.End);
                        }
                        long rangeEnd = Math.Min (rangeStart + bufferSize, contentLength);

                        int lastProgress = -1;
                        while (rangeStart < rangeEnd) {
                            string header = string.Format ("bytes={0}-{1}", rangeStart, rangeEnd);
                            headers ["Range"] = header;
                            yield return fetcher.doGet (downloadToken, headers);

                            var response = fetcher.getResponse ();
                            if (!string.IsNullOrEmpty (response.error)) {
                                logger.LogError ("Error downloading content. Will retry.");
                                logger.LogError (response.error);
                                yield return getRandomSleep ();
                                yield break;
                            }

                            int progress = (int)(((float)rangeEnd / (float)contentLength) * 100.0f);
                            progress = Math.Min (99, progress);
                            if (null != onDownloadProgressedEvent && lastProgress != progress) {
                                onDownloadProgressedEvent (bundleId, progress);
                                lastProgress = progress;
                            }

                            // This should never happen, but avoids crashing our coroutine.
                            if (response.bytes.Length > BUFFER.Length) {
                                logger.LogError ("Malformed content. Unexpected length. Will retry.");
                                yield return getRandomSleep ();
                                yield break;
                            }

                            // Copy the data into our writer thread's buffer.
                            // Writing the data to disk on the main thread takes too long.
                            Buffer.BlockCopy(response.bytes, 0, BUFFER, 0, response.bytes.Length);
                            bytesReceived = response.bytes.Length;
                            DATA_FLUSHED = false;
                            // Notify our writer and wait for it to complete.
                            DATA_READY.Set();
                            while (!DATA_FLUSHED) {
                                yield return waiter;
                            }

                            // Advance our range.
                            rangeStart = rangeEnd + 1;
                            rangeEnd = rangeStart + bufferSize;
                            rangeEnd = Math.Min (rangeEnd, contentLength);
                        }
                    }
                }
                #endif

                File.Move(getPartialPath(bundleId), getZipPath(bundleId));
                File.Delete (getVersionPath (bundleId));
            }
                
            UNPACK_FINISHED = false;
            util.RunOnThreadPool(() => {
                Unpack(bundleId);
            });

            while (!UNPACK_FINISHED) {
                yield return waiter;
            }

            removeDownloadFromQueues (bundleId);
            if (null != onDownloadCompletedEvent) {
                onDownloadCompletedEvent (bundleId, getContentPath(bundleId));
            }
        }

        /// <summary>
        /// Determine if a request failed with a 404.
        /// Not trivial given the lack of access to response codes.
        /// </summary>
        private bool isContentNotFound(IHTTPRequest request) {
            foreach (var z in request.responseHeaders) {
                if (z.Value.ToUpper().Contains ("404 NOT FOUND")) {
                    return true;
                }
            }
            if (request.error != null) {
                return request.error.Contains("404");
            }

            return false;
        }

        /// <summary>
        /// Integrity check and extract the zip file.
        /// </summary>
        private void Unpack(string bundleId) {
            try {
                string zipPath = getZipPath(bundleId);
                if (!File.Exists(zipPath)) {
                    logger.LogError("No download found: " + zipPath);
                    return;
                }

                logger.Log("Verifying download...");
                if (!verifyDownload(zipPath)) {
                    logger.LogError("Download failed integrity check. Deleting...");
                    Directory.Delete(getDataPath(bundleId), true);
                    return;
                }
                logger.Log("Download verified.");
                logger.Log("Unpacking");
                // Unpack the zip into a temporary folder.
                DeleteIfExists (getUnpackPath (bundleId));

                Directory.CreateDirectory(getUnpackPath(bundleId));
                #if !UNITY_METRO
                using(var s = new FileStream(getZipPath(bundleId), FileMode.Open)) {
                    ZipUtils.decompress(s, getUnpackPath(bundleId));
                }
                #endif

                logger.Log("Unpack complete");

                // Delete any existing content folder.
                DeleteIfExists (getContentPath (bundleId));

                // Rename our unpack folder to content, which should be atomic.
                Directory.Move (getUnpackPath (bundleId), getContentPath (bundleId));

                // Clean up.
                File.Delete(getZipPath(bundleId));
            } catch (IOException i) {
                // This will typically indicate a disk full condition,
                // though it could occur if storage is removed.
                logger.LogError (i.Message);
                onDownloadFailedPermanently(bundleId, i.Message);
            } catch (Exception e) {
                logger.LogError (e.Message);
                logger.LogError (e.StackTrace);
                onDownloadFailedPermanently(bundleId, e.Message);
            } finally {
                UNPACK_FINISHED = true;
            }
        }

        private void DeleteIfExists(string folder) {
            if (Directory.Exists (folder)) {
                Directory.Delete (folder, true);
            }
        }

        private void onDownloadFailedPermanently(string bundleId, string error) {
            util.RunOnMainThread (() => {

                removeDownloadFromQueues(bundleId);
                deleteContent(bundleId);
                if (null != onDownloadFailedEvent) {
                    try {
                        onDownloadFailedEvent (bundleId, error);
                    } catch (ArgumentException) {
                        onDownloadFailedEvent(null, error);
                    }
                }
            });
        }

        private void removeDownloadFromQueues(string bundleId) {
            scheduledDownloads.Remove(bundleId);
            serialiseDownloads ();
        }

        private bool verifyDownload(string filepath) {
            try {
                #if !UNITY_WP8
                using (ZipFile z = new ZipFile(filepath)) {
                    return z.TestArchive(true);
                }
                #else
                throw new NotImplementedException();
                #endif
            }
            catch (Exception) {
                return false;
            }
        }

        private void DownloadFlusher() {
            while (true) {
                DATA_READY.WaitOne ();
                fileStream.Write (BUFFER, 0, bytesReceived);
                DATA_FLUSHED = true;
            }
        }

        private byte[] decodeBase64String(string s) {
            return Convert.FromBase64String(s);
        }

        #if !UNITY_METRO
        private FileStream openDownload(string bundleId) {
            return new FileStream(getPartialPath(bundleId), FileMode.OpenOrCreate);
        }
        #endif

        public string getContentPath(string bundleId) {
            return Path.Combine(getDataPath(bundleId), "content");
        }

        private string getUnpackPath(string bundleId) {
            return Path.Combine (getDataPath (bundleId), "unpack");
        }

        private string getZipPath(string bundleId) {
            return Path.Combine(getDataPath(bundleId), "download.zip");
        }

        private string getPartialPath(string bundleId) {
            return Path.Combine(getDataPath(bundleId), "download.partial");
        }

        private void saveVersion(string bundleId, string version) {
            Util.WriteAllText(getVersionPath(bundleId), version);
        }

        /// <summary>
        /// Get the version number of any in progress download
        /// by examining the data directory.
        /// If not version is found, return a wildcard.
        /// </summary>
        private string getVersionToDownload(string bundleId) {
            string versionPath = getVersionPath(bundleId);
            if (File.Exists(versionPath)) {
                string contents = Util.ReadAllText(versionPath);
                long l;
                if (long.TryParse(contents, out l)) {
                    return contents;
                }
            }

            // Latest version will do.
            return "*";
        }

        private void saveReceipt(string bundleId, String receipt) {
            File.WriteAllText (getReceiptPath (bundleId), receipt);
        }

        private string getReceipt(string bundleId) {
            return File.ReadAllText(getReceiptPath(bundleId));
        }

        private string getReceiptPath(string bundleId) {
            return Path.Combine(getDataPath(bundleId), "receipt");
        }

        private string getVersionPath(string bundleId) {
            return Path.Combine(getDataPath(bundleId), "download.version");
        }

        private string getRootContentPath() {
            return Path.Combine (persistentDataPath, "unibill-content");
        }

        public string getDataPath(string bundleId) {
            return Path.Combine (getRootContentPath(), bundleId);
        }

        public bool isDownloaded(string bundleId) {
            return Directory.Exists (getContentPath (bundleId));
        }

        private void createDataPathIfNecessary(string bundleId) {
            Directory.CreateDirectory(getDataPath(bundleId));
        }

        public void deleteContent(string bundleId) {
            if (isDownloadScheduled (bundleId)) {
                logger.LogError ("Bundle id {0} is still downloading", bundleId);
                return;
            }

            if (!Directory.Exists (getDataPath(bundleId))) {
                logger.LogError ("Bundle id {0} is not downloaded", bundleId);
                return;
            }
            Directory.Delete (getDataPath (bundleId), true);
        }

        private Random rand = new Random();
        private object getRandomSleep() {
            int delay = 30 + rand.Next (30);
            logger.Log ("Backing off for {0} seconds", delay);
            return util.getWaitForSeconds(delay);
        }
    }
}
#endif