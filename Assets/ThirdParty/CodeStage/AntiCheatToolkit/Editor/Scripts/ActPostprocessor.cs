#define DEBUG
#undef DEBUG

#define DEBUG_VERBOSE
#undef DEBUG_VERBOSE

using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using CodeStage.AntiCheat.ObscuredTypes;
using UnityEditor;
using Debug = UnityEngine.Debug;

namespace CodeStage.AntiCheat.Editor
{
	[InitializeOnLoad]
	internal class ActPostprocessor: AssetPostprocessor
	{
		private static readonly List<AllowedAssembly> allowedAssemblies = new List<AllowedAssembly>();
		private static readonly List<string> allLibraries = new List<string>();

#if (DEBUG || DEBUG_VERBOSE)
		[MenuItem("Anti-Cheat Toolkit/Scan project assemblies")]
		private static void CallInjectionScan()
		{
			InjectionAssembliesScan();
		}
#endif

		static ActPostprocessor()
		{
			// removing traces of legacy IntegrityChecker usage, rest in peace, good friend =D
			ActEditorGlobalStuff.CleanSigningData();
			if (EditorPrefs.GetBool(ActEditorGlobalStuff.EDITOR_PREFS_SIGN_AFTER_PROCESSING) || EditorPrefs.GetBool(ActEditorGlobalStuff.EDITOR_PREFS_AUTO_SIGN))
			{
				EditorPrefs.DeleteKey(ActEditorGlobalStuff.EDITOR_PREFS_SIGN_AFTER_PROCESSING);
				EditorPrefs.DeleteKey(ActEditorGlobalStuff.EDITOR_PREFS_AUTO_SIGN);
			}

			if (EditorPrefs.GetBool(ActEditorGlobalStuff.PREFS_INJECTION) && EditorPrefs.GetBool(ActEditorGlobalStuff.PREFS_INJECTION_SCAN_POSTPONED))
			{
#if (DEBUG_VERBOSE)
				Debug.Log("[ACT] Injection Detector enabled and assemblies scan postponed, executing now...");
#endif
				EditorPrefs.SetBool(ActEditorGlobalStuff.PREFS_INJECTION_SCAN_POSTPONED, false);
				InjectionAssembliesScan();
			}

			EditorUserBuildSettings.activeBuildTargetChanged += OnBuildTargetChanged;
		}

		private static void OnBuildTargetChanged()
		{
			IsInjectionDetectorTargetCompatible();
		}

		// called by Unity (we are AssetPostprocessor, remember?)
		private static void OnPostprocessAllAssets(string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
		{
			if (importedAssets.Length == 0) return;

			bool scan = EditorPrefs.GetBool(ActEditorGlobalStuff.PREFS_INJECTION);
#if (DEBUG || DEBUG_VERBOSE)
			string log = "[ACT] Assets Imported (" + importedAssets.Length + "):\n";
			foreach (var importedAsset in importedAssets)
			{
				log += importedAsset + "\n";
			}
			 
			Debug.Log(log);
#endif
#if (DEBUG_VERBOSE)
			Debug.Log("[ACT] Injection Detector enabled: " + scan);
#endif
			// checking if Injection Detector data file was imported
			if (scan)
			{
				if (importedAssets.Length > 0 && importedAssets.Length <= 2)
				{
					foreach (string asset in importedAssets)
					{
						if (asset.EndsWith(ActEditorGlobalStuff.INJECTION_DATA_FILE_NAME) ||
							asset.EndsWith(ActEditorGlobalStuff.INJECTION_UNITY_DATA_FILE_NAME))
						{
							scan = false;
							break;
						}
					}
				}
			}

			if (!scan)
			{
#if (DEBUG || DEBUG_VERBOSE)
				Debug.Log("[ACT] Ignoring assets changes.");
#endif
				return;
			}

			if (EditorApplication.isCompiling)
			{
#if (DEBUG_VERBOSE)
				Debug.Log("[ACT] Postponing Injection Detector assemblies scan (compilation in progress).");
#endif
				EditorPrefs.SetBool(ActEditorGlobalStuff.PREFS_INJECTION_SCAN_POSTPONED, true);
			}
			else
			{
				InjectionAssembliesScan();
			}
		}

		internal static void InjectionAssembliesScan()
		{
			if (!IsInjectionDetectorTargetCompatible())
			{
				return;
			}

#if (DEBUG || DEBUG_VERBOSE)
			Stopwatch sw = Stopwatch.StartNew();
#elif (DEBUG_VERBOSE)
			Debug.Log("[ACT] = Injection Detector Assemblies Scan =\n");
			Debug.Log("[ACT] Paths:\n" +

			          "dataPath: " + ActEditorGlobalStuff.ASSETS_PATH + "\n" +
			          "Assemblies: " + ActEditorGlobalStuff.ASSEMBLIES_PATH + "\n" +
			          "Injection Detector Data: " + ActEditorGlobalStuff.INJECTION_DATA_PATH);
#endif
			//EditorApplication.LockReloadAssemblies();

			if (File.Exists(ActEditorGlobalStuff.INJECTION_DATA_PATH))
			{
				File.Delete(ActEditorGlobalStuff.INJECTION_DATA_PATH);
			}
			else
			{
				string injectionDataFileDir = Path.GetDirectoryName(ActEditorGlobalStuff.INJECTION_DATA_PATH);
				if (injectionDataFileDir == null)
				{
					Debug.LogError("[ACT] Something went wrong while obtaining injectionDataFileDir! Please report to " + ActEditorGlobalStuff.REPORT_EMAIL);
#if (DEBUG || DEBUG_VERBOSE)
					sw.Stop();
#endif
					return;
				}

				if (!Directory.Exists(injectionDataFileDir))
				{
					Directory.CreateDirectory(injectionDataFileDir);
				}
			}
#if (DEBUG_VERBOSE)
			Debug.Log("[ACT] Looking for all assemblies in current project...");
#endif
			allLibraries.Clear();
			allowedAssemblies.Clear();

			ActEditorGlobalStuff.GetAllLibrariesFromDir(allLibraries, ActEditorGlobalStuff.ASSETS_PATH);
			ActEditorGlobalStuff.GetAllLibrariesFromDir(allLibraries, ActEditorGlobalStuff.ASSEMBLIES_PATH);
#if (DEBUG_VERBOSE)
			Debug.Log("[ACT] Total libraries found: " + allLibraries.Count);
#endif
			string editorSubdir = Path.DirectorySeparatorChar + "editor" + Path.DirectorySeparatorChar;
			string asembliesPathLowerCase = ActEditorGlobalStuff.ASSEMBLIES_PATH_RELATIVE.ToLower();
			foreach (string libraryPath in allLibraries)
			{
				string libraryPathLowerCase = libraryPath.ToLower();
				if (libraryPathLowerCase.Contains(editorSubdir)) continue;
				if (libraryPathLowerCase.Contains("-editor.dll") && libraryPathLowerCase.Contains(asembliesPathLowerCase)) continue;

				try
				{
					AssemblyName assName = AssemblyName.GetAssemblyName(libraryPath);
					string name = assName.Name;
					int hash = ActEditorGlobalStuff.GetAssemblyHash(assName);

					AllowedAssembly allowed = allowedAssemblies.FirstOrDefault(allowedAssembly => allowedAssembly.name == name);

					if (allowed != null)
					{
						allowed.AddHash(hash);
					}
					else
					{
						allowed = new AllowedAssembly(name, new[] { hash });
						allowedAssemblies.Add(allowed);
					}
				}
				catch{}
			}

#if (DEBUG || DEBUG_VERBOSE)
			sw.Stop();
			string trace = "[ACT] Found assemblies (" + allowedAssemblies.Count + "):\n";

			foreach (AllowedAssembly allowedAssembly in allowedAssemblies)
			{
				trace += "  Name: " + allowedAssembly.name + "\n";
				trace = allowedAssembly.hashes.Aggregate(trace, (current, hash) => current + ("    Hash: " + hash + "\n"));
			}

			Debug.Log(trace);
			sw.Start();
#endif
			FileStream fs = new FileStream(ActEditorGlobalStuff.INJECTION_DATA_PATH, FileMode.Create, FileAccess.Write);
			BinaryWriter bw = new BinaryWriter(fs);
			int allowedAssembliesCount = allowedAssemblies.Count;

			string allowedUnityAssemblies = ActEditorGlobalStuff.ResolveAllowedUnityAssembliesDataPath();

			if (File.Exists(allowedUnityAssemblies))
			{
				BinaryReader br = new BinaryReader(File.Open(allowedUnityAssemblies, FileMode.Open, FileAccess.Read));
				int allowedUnityAssembliesCount = br.ReadInt32();
				int totalRecords = allowedUnityAssembliesCount + allowedAssembliesCount;

				bw.Write(totalRecords);

				for (int i = 0; i < allowedUnityAssembliesCount; i++)
				{
					bw.Write(br.ReadString());
				}
				br.Close();
			}
			else
			{
#if (DEBUG || DEBUG_VERBOSE)
				sw.Stop();
#endif
				bw.Close();
				fs.Close();
				Debug.LogError("[ACT] Can't find AllowedAssemblies.bytes file!\nPlease, report to " + ActEditorGlobalStuff.REPORT_EMAIL);
				return;
			}

			
			for (int i = 0; i < allowedAssembliesCount; i++)
			{
				AllowedAssembly assembly = allowedAssemblies[i];
				string name = assembly.name;
				string hashes = "";

				for (int j = 0; j < assembly.hashes.Length; j++)
				{
					hashes += assembly.hashes[j];
					if (j < assembly.hashes.Length - 1)
					{
						hashes += ActEditorGlobalStuff.INJECTION_DATA_SEPARATOR;
					}
				}

				string line = ObscuredString.EncryptDecrypt(name + ActEditorGlobalStuff.INJECTION_DATA_SEPARATOR + hashes, "Elina");
				
#if (DEBUG_VERBOSE)
				Debug.Log("[ACT] Writing assembly:\n" + name + ActEditorGlobalStuff.INJECTION_DATA_SEPARATOR + hashes);
#endif
				bw.Write(line);
			}

			bw.Close();
			fs.Close();
			 
#if (DEBUG || DEBUG_VERBOSE)
			sw.Stop();
			Debug.Log("[ACT] Assemblies scan duration: " + sw.ElapsedMilliseconds + " ms.");
#endif

			AssetDatabase.Refresh();

			
			if (allowedAssembliesCount == 0)
			{
				Debug.LogError("[ACT] Can't find any assemblies!\nPlease, report to " + ActEditorGlobalStuff.REPORT_EMAIL);
			}
			//EditorApplication.UnlockReloadAssemblies();
		}

		private static bool IsInjectionDetectorTargetCompatible()
		{
#if UNITY_STANDALONE || UNITY_WEBPLAYER || UNITY_IPHONE || UNITY_ANDROID
			return true;
#else
			bool injectionEnabled = EditorPrefs.GetBool(ActEditorGlobalStuff.PREFS_INJECTION, false);
			if (injectionEnabled)
			{
				Debug.LogWarning("[ACT] Injection Detector is not available on selected platform and will be disabled!");
				EditorPrefs.SetBool(ActEditorGlobalStuff.PREFS_INJECTION, false);
				ActEditorGlobalStuff.CleanInjectionDetectorData();
			}
			return false;
#endif
		}
	}
}