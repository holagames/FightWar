using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Unibill.Impl {
    public class Util {
        public static string ReadAllText(string path) {
            #if !(UNITY_WP8 || UNITY_METRO)
            using (var r = new StreamReader(path)) {
                return r.ReadToEnd();
            }
            #else
            throw new NotImplementedException();
            #endif
        }

        public static void WriteAllText(string path, string text) {
            #if !(UNITY_WP8 || UNITY_METRO)
            using (var r = new StreamWriter(path)) {
                r.Write(text);
            }
            #else
            throw new NotImplementedException();
            #endif
        }
    }
}
