using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace CodeStage.AntiCheat.Editor
{
	internal class ActEditorGlobalStuff
	{
		internal const string EDITOR_PREFS_SIGN_AFTER_PROCESSING = "ACTsignAfterProcessing";
		internal const string EDITOR_PREFS_AUTO_SIGN = "ACTautoSign";

		internal const string PREFS_INJECTION = "ACTDIDEnabled";
		internal const string PREFS_INJECTION_SCAN_POSTPONED = "ACTDIDRecollect";

		internal const string REPORT_EMAIL = "focus@codestage.ru";
		internal const string FINGERPRINTS_RELATIVE_LOCATION = "/Resources/fn.txt";

		internal const string INJECTION_DATA_FILE_NAME_OLD = "fndid.txt";
		internal const string INJECTION_DATA_FILE_NAME = "fndid.bytes";
		internal const string INJECTION_UNITY_DATA_FILE_NAME = "AllowedAssemblies.bytes";
		internal const string INJECTION_DATA_SEPARATOR = ":";

		internal static readonly string ASSEMBLIES_PATH_RELATIVE = "Library" + Path.DirectorySeparatorChar + "ScriptAssemblies";
		internal static readonly string INJECTION_DATA_PATH_RELATIVE_OLD = "Resources" + Path.DirectorySeparatorChar + INJECTION_DATA_FILE_NAME_OLD;
		internal static readonly string INJECTION_DATA_PATH_RELATIVE = "Resources" + Path.DirectorySeparatorChar + INJECTION_DATA_FILE_NAME;

		internal static readonly string ASSETS_PATH = Application.dataPath;
		internal static readonly string ASSEMBLIES_PATH = ASSETS_PATH + Path.DirectorySeparatorChar + ".." + Path.DirectorySeparatorChar + ASSEMBLIES_PATH_RELATIVE;

		internal static readonly string FINGERPRINTS_PATH = ASSETS_PATH + FINGERPRINTS_RELATIVE_LOCATION;
		internal static readonly string INJECTION_DATA_PATH_OLD = ASSETS_PATH + Path.DirectorySeparatorChar + INJECTION_DATA_PATH_RELATIVE_OLD;
		internal static readonly string INJECTION_DATA_PATH = ASSETS_PATH + Path.DirectorySeparatorChar + INJECTION_DATA_PATH_RELATIVE;

		private static readonly string[] hexTable = Enumerable.Range(0, 256).Select(v => v.ToString("x2")).ToArray();
		
		/*internal static readonly string[] UNITY_ASSEMBLIES_NAMES =
		{
			"Assembly-CSharp", "Assembly-CSharp-firstpass",
			"Assembly-UnityScript", "Assembly-UnityScript-firstpass",
			"Assembly-Boo", "Assembly-Boo-firstpass"
		};*/

		internal static void CleanInjectionDetectorData()
		{
			if (!File.Exists(INJECTION_DATA_PATH))
			{
				return;
			}

			File.Delete(INJECTION_DATA_PATH);
			if (File.Exists(INJECTION_DATA_PATH + ".meta"))
			{
				File.Delete(INJECTION_DATA_PATH + ".meta");
			}

			string dir = Path.GetDirectoryName(INJECTION_DATA_PATH);
			if (dir != null)
			{
				if (Directory.Exists(dir) && DirectoryEmpty(dir))
				{
					Directory.Delete(dir);
					if (File.Exists(dir + ".meta"))
					{
						File.Delete(dir + ".meta");
					}
				}
			}

			AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
		}

		internal static void CleanSigningData()
		{
			if (!File.Exists(FINGERPRINTS_PATH) && !File.Exists(INJECTION_DATA_PATH_OLD))
			{
				return;
			}

			File.Delete(FINGERPRINTS_PATH);
			if (File.Exists(FINGERPRINTS_PATH + ".meta"))
			{
				File.Delete(FINGERPRINTS_PATH + ".meta");
			}

			File.Delete(INJECTION_DATA_PATH_OLD);
			if (File.Exists(INJECTION_DATA_PATH_OLD + ".meta"))
			{
				File.Delete(INJECTION_DATA_PATH_OLD + ".meta");
			}

			string fpDir = Path.GetDirectoryName(FINGERPRINTS_PATH);
			if (fpDir != null)
			{
				if (Directory.Exists(fpDir) && DirectoryEmpty(fpDir))
				{
					Directory.Delete(fpDir);
					if (File.Exists(fpDir + ".meta"))
					{
						File.Delete(fpDir + ".meta");
					}
				}
			}

			AssetDatabase.Refresh(ImportAssetOptions.ForceSynchronousImport);
		}

		internal static int GetAssemblyHash(AssemblyName ass)
		{
			byte[] bytes = ass.GetPublicKeyToken();
			string hashInfo;
			if (bytes.Length == 8)
			{
				hashInfo = ass.Name + PublicKeyTokenToString(bytes);
			}
			else
			{
				hashInfo = ass.Name;
			}

			// Jenkins hash function (http://en.wikipedia.org/wiki/Jenkins_hash_function)
			int result = 0;
			int len = hashInfo.Length;

			for (int i = 0; i < len; ++i)
			{
				result += hashInfo[i];
				result += (result << 10);
				result ^= (result >> 6);
			}
			result += (result << 3);
			result ^= (result >> 11);
			result += (result << 15);

			return result;
		}

		
		internal static string ResolveAllowedUnityAssembliesDataPath()
		{
			string result = "";
			string[] assembliesScannerScriptPath = Directory.GetFiles(Application.dataPath, "ActEditorGlobalStuff.cs", SearchOption.AllDirectories);
			if (assembliesScannerScriptPath.Length == 0 || assembliesScannerScriptPath.Length > 1)
			{
				Debug.LogError("[ACT] Can't find proper location for allowed assemblies data storage Errid: #100!");
				return result;
			}

			string allowedAssembliesPath = assembliesScannerScriptPath[0];
			if (allowedAssembliesPath.IndexOf("Scripts" + Path.DirectorySeparatorChar + "ActEditorGlobalStuff.cs", System.StringComparison.Ordinal) == -1)
			{
				Debug.LogError("[ACT] Can't find proper location for allowed assemblies data storage Errid: #101!");
				return result;
			}

			result = allowedAssembliesPath.Replace("Scripts" + Path.DirectorySeparatorChar + "ActEditorGlobalStuff.cs", "ServiceData" + Path.DirectorySeparatorChar + INJECTION_UNITY_DATA_FILE_NAME);
			return result;
		}

		internal static void GetAllLibrariesFromDir(List<string> libsList, string dir)
		{
			if (Directory.Exists(dir))
			{
				dir = dir.Replace('\\', Path.DirectorySeparatorChar).Replace('/', Path.DirectorySeparatorChar);
				libsList.AddRange(Directory.GetFiles(dir, "*.dll", SearchOption.AllDirectories));
			}
		}

		private static string PublicKeyTokenToString(byte[] bytes)
		{
			string result = "";

			// AssemblyName.GetPublicKeyToken() returns 8 bytes
			for (int i = 0; i < 8; i++)
			{
				result += hexTable[bytes[i]];
			}

			return result;
		}

		private static bool DirectoryEmpty(string path)
		{
			string[] dirs = Directory.GetDirectories(path);
			string[] files = Directory.GetFiles(path);
			return dirs.Length == 0 && files.Length == 0;
		}
	}
}