using UnityEngine;
using System.Collections;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System;
using System.IO;  
using System.Net;
using Common;
using System.Globalization;

public class DefineChargementHelperDemo : MonoBehaviour
{

	
	Rect windowRect = new Rect (20, 20, 400, 600);
	string str = "Charge";
	public Boolean isDebug = false;
	public string  money = "";
	public string goodName = "";
	public GUISkin guiSkin;
	GUI.WindowFunction windowFunction;

	void OnGUI ()
	{
		if (guiSkin) {   
			GUI.skin = guiSkin;
		} 

		windowRect = GUI.Window (3, windowRect, DochargeWindow, str);
	
	}
	
	//bool startcheck;  

	public  void DochargeWindow (int windowID)
	{

		GUI.Label (new Rect (20, 30, 100, 40), "Price:");
		money = GUI.TextField (new Rect (130, 30, 100, 40), money, 15);

		GUI.Label (new Rect (20, 100, 100, 40), "Product Name:"); 
		goodName = GUI.TextField (new Rect (130, 100, 100, 40), goodName, 15);
		 
		if (GUI.Button (new Rect (20, 200, 100, 50), "OK")) {
			if (System.Text.RegularExpressions.Regex.IsMatch (money, @"^\d*$")) {
				PlayerPrefs.SetString ("goodName", goodName);   
				PlayerPrefs.SetInt ("money", int.Parse (money));   
				PlayerPrefs.SetString ("desc", "购买金币");  	
				PlayerPrefs.SetString ("paytype", "charge");
				PlayerPrefs.SetInt ("login", 1);
				Application.LoadLevel ("MainCamera");
			} else {
				str = "chargemoney illegal";
				money = "";
			}
		}
		if (GUI.Button (new Rect (150, 200, 100, 50), "Cancel")) {
//			using (AndroidJavaClass unityPlayer = new AndroidJavaClass ("com.unity3d.player.UnityPlayer")) {
//				using (AndroidJavaObject curActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity")) {
//					PlayerPrefs.SetInt ("login", 1);
//					Application.LoadLevel ("MainCamera");					
//				}
//			}
		}
		Debug.Log ("-------------------");
	}
	// Use this for initialization
	void Start ()
	{
		windowFunction = DochargeWindow;
	}
	
	void Update ()
	{
	    if(Input.GetKey(KeyCode.Backspace) || Input.GetKey(KeyCode.Escape)){
			PlayerPrefs.SetInt ("login", 1);
            Application.LoadLevel ("MainCamera");	
        }
	}
	
	void Awake ()
	{
	  
		
	}
}
