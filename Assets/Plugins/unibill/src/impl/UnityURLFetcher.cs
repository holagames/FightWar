using System;
using System.Collections;
using System.Collections.Generic;
using Uniject;
using UnityEngine;

namespace Unibill.Impl
{
    public class UnityHTTPRequest : IHTTPRequest {
        private WWW w;

        public UnityHTTPRequest(WWW w) {
            this.w = w;
        }

        public Dictionary<string, string> responseHeaders {
            get {
                return w.responseHeaders;
            }
        }
        public byte[] bytes {
            get {
                return w.bytes;
            }
        }
        public string contentString {
            get {
                return w.text;
            }
        }
        public string error {
            get {
                return w.error;
            }
        }
    }

    public class UnityURLFetcher : IURLFetcher
    {
        private UnityHTTPRequest request;

        #region IURLFetcher implementation
        #if (UNITY_4_3 || UNITY_4_2) && !(UNITY_METRO || UNITY_WP8)
        Hashtable hashHeaders = new Hashtable();
        #endif
        public object doGet (string url, Dictionary<string, string> headers)
        {
            WWW w;
            #if (UNITY_4_3 || UNITY_4_2) && !(UNITY_METRO || UNITY_WP8)
            hashHeaders.Clear();
            foreach (var kvp in headers) {
                hashHeaders.Add (kvp.Key, kvp.Value);
            }
            w = new WWW (url, null, hashHeaders);
            #else
            w = new WWW(url, null, headers);
            #endif
            request = new UnityHTTPRequest (w);
            return w;
        }
        public object doPost (string url, Dictionary<string, string> parameters)
        {
            WWWForm form = new WWWForm ();
            foreach (var kvp in parameters) {
                form.AddField (kvp.Key, kvp.Value);
            }

            WWW w = new WWW (url, form);
            request = new UnityHTTPRequest (w);
            return w;
        }
        public IHTTPRequest getResponse ()
        {
            return request;
        }
        #endregion
        
    }
}

