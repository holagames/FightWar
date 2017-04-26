using System.IO;
using UnityEditor;
using UnityEngine;

namespace CodeStage.AntiCheat.Editor
{
	internal class ActOptionsWindow: EditorWindow
	{
		[MenuItem("Window/Anti-Cheat Toolkit/Options")]
		private static void ShowOptions()
		{
			GetWindow(typeof(ActOptionsWindow), false, "ACT Options", true);
		}

		private void OnGUI()
		{
			GUILayout.Label("Injection Detector options", EditorStyles.boldLabel);

			bool enableInjectionDetector = false;

			BuildTarget bt = EditorUserBuildSettings.activeBuildTarget;

			if (bt == BuildTarget.FlashPlayer)
			{
				GUILayout.Label("Sorry, Injection Detector is not available for target platform!");
			}
			else
			{
				enableInjectionDetector = EditorPrefs.GetBool(ActEditorGlobalStuff.PREFS_INJECTION);
				enableInjectionDetector = GUILayout.Toggle(enableInjectionDetector, "Enable Injection Detector");
			}


			EditorPrefs.SetBool(ActEditorGlobalStuff.PREFS_INJECTION, enableInjectionDetector);

			if (!enableInjectionDetector)
			{
				ActEditorGlobalStuff.CleanInjectionDetectorData();
			}
			else if (!File.Exists(ActEditorGlobalStuff.INJECTION_DATA_PATH))
			{
				ActPostprocessor.InjectionAssembliesScan();
			}
		}
	}
}