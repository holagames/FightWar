//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Linq;
using System.Text;
using UnityEngine;
using Unibill.Impl;

public class UnityUtil : MonoBehaviour, Uniject.IUtil {

    public T[] getAnyComponentsOfType<T>() where T : class {
        GameObject[] objects = (GameObject[]) GameObject.FindObjectsOfType(typeof(GameObject));
        List<T> result = new List<T>();
        foreach (GameObject o in objects) {
            foreach (MonoBehaviour mono in o.GetComponents<MonoBehaviour>()) {
                if (mono is T) {
                    result.Add(mono as T);
                }
            }
        }

        return result.ToArray();
    }

    void Start() {
		DontDestroyOnLoad(this.gameObject);
    }

    public DateTime currentTime { get { return DateTime.Now; } }

    public string persistentDataPath {
        get { return Application.persistentDataPath; }
    }

    public string loadedLevelName() {
        return Application.loadedLevelName;
    }

    public RuntimePlatform Platform {
        get { return Application.platform; }
    }

    public bool IsEditor {
        get { return Application.isEditor; }
    }

    public string DeviceModel {
        get { return SystemInfo.deviceModel; }
    }

    public string DeviceName {
        get { return SystemInfo.deviceName; }
    }

    public DeviceType DeviceType {
        get { return SystemInfo.deviceType; }
    }

    public string OperatingSystem {
        get { return SystemInfo.operatingSystem; }
    }

    private static List<RuntimePlatform> PCControlledPlatforms = new List<RuntimePlatform>() {
	    RuntimePlatform.FlashPlayer,
	    RuntimePlatform.LinuxPlayer,
	    RuntimePlatform.NaCl,
        RuntimePlatform.OSXDashboardPlayer,
        RuntimePlatform.OSXEditor,
        RuntimePlatform.OSXPlayer,
        RuntimePlatform.OSXWebPlayer,
        RuntimePlatform.WindowsEditor,
        RuntimePlatform.WindowsPlayer,
        RuntimePlatform.WindowsWebPlayer,
	};

    public static T findInstanceOfType<T>() where T : MonoBehaviour {
        return (T) GameObject.FindObjectOfType(typeof(T));
    }

    public static T loadResourceInstanceOfType<T>() where T : MonoBehaviour {
        return ((GameObject) GameObject.Instantiate(Resources.Load(typeof(T).ToString()))).GetComponent<T>();
    }

    public static bool pcPlatform() {
        return PCControlledPlatforms.Contains(Application.platform);
    }

    public static void DebugLog(string message, params System.Object[] args) {
        try {
            UnityEngine.Debug.Log(string.Format(
                "com.ballatergames.debug - {0}",
                string.Format(message, args))
            );
        } catch (ArgumentNullException a) {
            UnityEngine.Debug.Log(a);
        } catch (FormatException f) {
            UnityEngine.Debug.Log(f);
        }
    }
	
	/*
	 * Returns: xMin, xMax, yMin, yMax, zMin, zMax
	 * */
	public static float[] getFrustumBoundaries(Camera camera) {
		
		Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
		return new float[] {
			(-planes[0].normal * planes[0].distance).x,
			(-planes[1].normal * planes[1].distance).x,
			(-planes[5].normal * planes[5].distance).y,
			(-planes[4].normal * planes[4].distance).y,
			(-planes[2].normal * planes[2].distance).z,
			(-planes[3].normal * planes[3].distance).z,
		};
	}

    object Uniject.IUtil.InitiateCoroutine (System.Collections.IEnumerator start)
    {
        return StartCoroutine (start);
    }

    void Uniject.IUtil.InitiateCoroutine(System.Collections.IEnumerator start, int delay) {
        delayedCoroutine(start, delay);
    }

    private IEnumerator delayedCoroutine(IEnumerator coroutine, int delay) {
        yield return new WaitForSeconds(delay);
        StartCoroutine(coroutine);
    }

    public void RunOnThreadPool(Action runnable) {
        #if !(UNITY_WP8 || UNITY_METRO)
        ThreadPool.QueueUserWorkItem (x => runnable());
        #endif
    }

    void Update() {
        while (mainThreadTasks.Count > 0) {
            Action toRun;
            lock (mainThreadTasks) {
                toRun = mainThreadTasks.Dequeue ();
            }
            toRun();
        }
    }

    private Queue<Action> mainThreadTasks = new Queue<Action> ();

    public void RunOnMainThread(Action runnable) {
        lock (mainThreadTasks) {
            mainThreadTasks.Enqueue (runnable);
        }
    }

    public object getWaitForSeconds (int seconds)
    {
        return new WaitForSeconds (seconds);
    }
}
