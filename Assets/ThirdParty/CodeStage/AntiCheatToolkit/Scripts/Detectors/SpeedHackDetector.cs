using System;
using UnityEngine;
using Debug = UnityEngine.Debug;

#if UNITY_FLASH
using UnityEngine.Flash;
#endif

namespace CodeStage.AntiCheat.Detectors
{
	/// <summary>
	/// Allows to detect Cheat Engine's speed hack (and maybe some other speed hack tools) usage.
	/// Just call SpeedHackDetector.StartDetection() to use it.
	/// </summary>
	/// You also may add it to the scene in editor through the<br/>
	/// "GameObject->Create Other->Code Stage->Anti-Cheat Toolkit->Speed Hack Detector" menu.
	/// 
	/// It allows you to edit and store detector's settings in inspector.<br/>
	/// <strong>Please, keep in mind you still need to call SpeedHackDetector.StartDetection() to start detector!</strong>
	[AddComponentMenu("")] // sorry, but you shouldn't add it via Component menu, read above comment please
	public class SpeedHackDetector: MonoBehaviour
	{
		private const string COMPONENT_NAME = "Speed Hack Detector";

#if UNITY_FLASH && !UNITY_EDITOR
		private const int THRESHOLD = 500; // milliseconds
#else
		private const int THRESHOLD = 5000000; // ticks = 500 ms
#endif

		private static SpeedHackDetector instance;

		/// <summary>
		/// Speed Hack Detector will be automatically disposed after firing callback if enabled 
		/// or it will just stop internal processes otherwise.
		/// </summary>
		public bool autoDispose = true;

		/// <summary>
		/// Allows to keep Speed Hack Detector's game object on new level (scene) load.
		/// </summary>
		public bool keepAlive = true;

		/// <summary>
		/// Callback to call on speed hack detection.
		/// </summary>
		public Action onSpeedHackDetected;

		/// <summary>
		/// Time (in seconds) between detector checks.
		/// </summary>
		public float interval = 1f;

		/// <summary>
		/// Maximum false positives count allowed before registering speed hack.
		/// </summary>
		public byte maxFalsePositives = 3;

		private int errorsCount;
		private long ticksOnStart;
		private long ticksOnStartVulnerable;
		private bool running;

#if UNITY_EDITOR
		private const string MENU_PATH = "GameObject/Create Other/Code Stage/Anti-Cheat Toolkit/" + COMPONENT_NAME;

		[UnityEditor.MenuItem(MENU_PATH, false)]
		private static void AddToScene()
		{
			SpeedHackDetector component = (SpeedHackDetector)FindObjectOfType(typeof(SpeedHackDetector));
			if (component != null)
			{
				if (component.IsPlacedCorrectly())
				{
					if (UnityEditor.EditorUtility.DisplayDialog("Remove " + COMPONENT_NAME + "?", COMPONENT_NAME + " already exists in scene and placed correctly. Dou you wish to remove it?", "Yes", "No"))
					{
						DestroyImmediate(component.gameObject);
					}
				}
				else if (component.MayBePlacedHere())
				{
					int dialogResult = UnityEditor.EditorUtility.DisplayDialogComplex("Fix existing Game Object to work with " + COMPONENT_NAME + "?", COMPONENT_NAME+ " already exists in scene and placed onto empty Game Object \"" + component.name + "\".\nDo you wish to let component configure and use this Game Object further? Press Delete to remove component from scene at all.", "Fix", "Delete", "Cancel");

					switch (dialogResult)
					{
						case 0:
							component.FixCurrentGameObject();
							break;
						case 1:
							DestroyImmediate(component);
							break;
					}
				}
				else
				{
					int dialogResult = UnityEditor.EditorUtility.DisplayDialogComplex("Move existing " + COMPONENT_NAME + " to own Game Object?", "Looks like " + COMPONENT_NAME + " component already exists in scene and placed incorrectly on Game Object \"" + component.name + "\".\nDo you wish to let component move itself onto separate configured Game Object \"" + COMPONENT_NAME + "\"? Press Delete to remove plugin from scene at all.", "Move", "Delete", "Cancel");
					switch (dialogResult)
					{
						case 0:
							GameObject go = new GameObject(COMPONENT_NAME);
							SpeedHackDetector newComponent = go.AddComponent<SpeedHackDetector>();

							UnityEditor.EditorUtility.CopySerialized(component, newComponent);

							DestroyImmediate(component);
							break;
						case 1:
							DestroyImmediate(component);
							break;
					}
				}
			}
			else
			{
				GameObject go = new GameObject(COMPONENT_NAME);
				go.AddComponent<SpeedHackDetector>();
			}
		}

		private bool MayBePlacedHere()
		{
			return (gameObject.GetComponentsInChildren<Component>().Length == 2 && transform.childCount == 0 && transform.parent == null);
		}

		private void FixCurrentGameObject()
		{
			gameObject.name = COMPONENT_NAME;
			transform.position = Vector3.zero;
			transform.rotation = Quaternion.identity;
			transform.localScale = Vector3.one;
			tag = "Untagged";
			gameObject.layer = 0;
			gameObject.isStatic = true;
		}
#endif

		/// <summary>
		/// Allows to reach public properties to set up them from code.
		/// </summary>
		public static SpeedHackDetector Instance
		{
			get
			{
				if (instance == null)
				{
					SpeedHackDetector detector = (SpeedHackDetector)FindObjectOfType(typeof(SpeedHackDetector));
					if (detector == null)
					{
						GameObject go = new GameObject(COMPONENT_NAME);
						detector = go.AddComponent<SpeedHackDetector>();
					}
					return detector;
				}
				return instance;
			}
		}

		// preventing direct instantiation =P
		private SpeedHackDetector(){}

		/// <summary>
		/// Use it to stop and completely dispose Speed Hack Detector.
		/// </summary>
		public static void Dispose()
		{
			Instance.DisposeInternal();
		}

		private void DisposeInternal()
		{
			StopMonitoringInternal();
			instance = null;
			Destroy(gameObject);
		}

		private void Awake()
		{
			if (!IsPlacedCorrectly())
			{
				Debug.LogWarning("[ACT] " + COMPONENT_NAME + " is placed in scene incorrectly and will be auto-destroyed! Please, use \"GameObject->Create Other->Code Stage->Anti-Cheat Toolkit->" + COMPONENT_NAME + "\" menu to correct this!");
				Destroy(this);
				return;
			}

			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		private bool IsPlacedCorrectly()
		{
			return (name == COMPONENT_NAME &&
					GetComponentsInChildren<Component>().Length == 2 &&
					transform.childCount == 0);
		}

		private void OnLevelWasLoaded(int index)
		{
			if (!keepAlive)
			{
				Dispose();
			}
		}

		private void OnDisable()
		{
			StopMonitoringInternal();
		}

		private void OnApplicationQuit()
		{
			DisposeInternal();
		}

		/// <summary>
		/// Starts speed hack detection using default interval (1 second) 
		/// and allows default count of false positives (3 by default).
		/// All default values may be changed in inspector.
		/// </summary>
		/// <param name="callback">Method to call after detection.</param>
		public static void StartDetection(Action callback)
		{
			StartDetection(callback, Instance.interval);
		}

		/// <summary>
		/// Starts speed hack detection using passed checkInterval and allows default 
		/// count of false positives (3 by default). 
		/// All default values may be changed in inspector.
		/// </summary>
		/// <param name="callback">Method to call after detection.</param>
		/// <param name="checkInterval">Time in seconds between speed hack checks.</param>
		public static void StartDetection(Action callback, float checkInterval)
		{
			StartDetection(callback, checkInterval, Instance.maxFalsePositives);
		}

		/// <summary>
		/// Starts speed hack detection using passed checkInterval and maxErrors. 
		/// </summary>
		/// <param name="callback">Method to call after detection.</param>
		/// <param name="checkInterval">Time in seconds between speed hack checks.</param>
		/// <param name="maxErrors">Amount of possible false positives.</param>
		public static void StartDetection(Action callback, float checkInterval, byte maxErrors)
		{
			if (Instance.running)
			{
				Debug.LogWarning("[ACT] " + COMPONENT_NAME + " already running!");
				return;
			}
			Instance.StartDetectionInternal(callback, checkInterval, maxErrors);
		}

		private void StartDetectionInternal(Action callback, float checkInterval, byte maxErrors)
		{
			onSpeedHackDetected = callback;
			interval = checkInterval;
			maxFalsePositives = maxErrors;

#if UNITY_FLASH && !UNITY_EDITOR
			ticksOnStart = ActionScript.Expression<long>("ACTFlashGate.GetMillisecondsFromDate();");
			ticksOnStartVulnerable = ActionScript.Expression<long>("ACTFlashGate.GetMillisecondsFromStart();");
#else
			ticksOnStart = DateTime.UtcNow.Ticks;
			ticksOnStartVulnerable = Environment.TickCount * TimeSpan.TicksPerMillisecond;
#endif
			InvokeRepeating("OnTimer", checkInterval, checkInterval);
			errorsCount = 0;

			running = true;
		}

		/// <summary>
		/// Stops speed hack detection. 
		/// </summary>
		public static void StopMonitoring()
		{
			Instance.StopMonitoringInternal();
		}

		private void StopMonitoringInternal()
		{
			if (running)
			{
				CancelInvoke("OnTimer");
				onSpeedHackDetected = null;
				running = false;
			}
		}

		private void OnTimer()
		{
			long ticks = 0;
			long ticksVulnerable = 0;

#if UNITY_FLASH && !UNITY_EDITOR
			ticks = ActionScript.Expression<long>("ACTFlashGate.GetMillisecondsFromDate();");
			ticksVulnerable = ActionScript.Expression<long>("ACTFlashGate.GetMillisecondsFromStart();");
#else
			ticks = DateTime.UtcNow.Ticks;
			ticksVulnerable = Environment.TickCount * TimeSpan.TicksPerMillisecond;
#endif
			if (Mathf.Abs((ticksVulnerable - ticksOnStartVulnerable) - (ticks - ticksOnStart)) > THRESHOLD)
			{
				errorsCount++;
				Debug.LogWarning("[ACT] SpeedHackDetector: detection! Silent detections left: " + (maxFalsePositives - errorsCount));
				if (errorsCount > maxFalsePositives)
				{
					if (onSpeedHackDetected != null)
					{
						onSpeedHackDetected();
					}

					if (autoDispose)
					{
						Dispose();
					}
					else
					{
						StopMonitoring();
					}
				}
			}
		}
	}
}