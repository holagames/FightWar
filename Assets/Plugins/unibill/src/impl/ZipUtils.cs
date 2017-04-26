#if !(UNITY_WP8 || UNITY_METRO)
using System;
using System.IO;
#if !UNITY_WP8
using ICSharpCode.SharpZipLib.Zip;
#endif

namespace Unibill.Impl
{
    public class ZipUtils
    {
        public static void decompress(Stream stream, String outputPath) {
            #if !UNITY_WP8
            ZipInputStream zipInputStream = new ZipInputStream (stream);

            ZipEntry zipEntry = zipInputStream.GetNextEntry();
            byte[] buffer = new byte[4096];     // 4K is optimum
            while (zipEntry != null) {
                string entryFileName = zipEntry.Name;
                string fullZipToPath = Path.Combine(outputPath, entryFileName);
                string directoryName = Path.GetDirectoryName(fullZipToPath);
                if (directoryName.Length > 0)
                    Directory.CreateDirectory(directoryName);

                if (!zipEntry.IsDirectory) {
                    using (FileStream streamWriter = File.Create (fullZipToPath)) {
                        Copy (zipInputStream, streamWriter, buffer);
                    }
                }
                zipEntry = zipInputStream.GetNextEntry();
            }
            #endif
        }

        private static void Copy(Stream source, Stream destination, byte[] buffer)
        {
            bool copying = true;

            while (copying) {
                int bytesRead = source.Read(buffer, 0, buffer.Length);
                if (bytesRead > 0) {
                    destination.Write(buffer, 0, bytesRead);
                }
                else {
                    destination.Flush();
                    copying = false;
                }
            }
        }
    }
}
#endif
