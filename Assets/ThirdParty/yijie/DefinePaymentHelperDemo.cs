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

public class DefinePaymentHelperDemo : MonoBehaviour
{


	Rect windowRect = new Rect (0, 0, 400, 600);
	string str = "Pay";
	
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
		windowRect = GUI.Window (2, windowRect, DopayWindow, str);
	}
	
		
	public  void DopayWindow (int windowID)
	{		
		GUI.Label (new Rect (20, 30, 100, 40), "Price:");
		money = GUI.TextField (new Rect (130, 30, 100, 40), money, 15);

		GUI.Label (new Rect (20, 100, 100, 40), "Product Name:"); 
		goodName = GUI.TextField (new Rect (130, 100, 100, 40), goodName, 15);

		if (GUI.Button (new Rect (10, 200, 100, 50), "OK")) {
			if (System.Text.RegularExpressions.Regex.IsMatch (money, @"^\d*$")) {
				PlayerPrefs.SetString ("goodName", goodName);   
				PlayerPrefs.SetInt ("money", int.Parse (money));   
				PlayerPrefs.SetString ("desc", "购买金币");  
				PlayerPrefs.SetString ("paytype", "pay");
				PlayerPrefs.SetInt ("login", 1);
				Application.LoadLevel ("MainCamera");
			} else {
				str = "paymoney illegal";
				money = "";
			}
		}
		if (GUI.Button (new Rect (150, 200, 100, 50), "Cancel")) {
			PlayerPrefs.SetInt ("login", 1);
			Application.LoadLevel ("MainCamera");
		}
		Debug.Log ("-------------------");
	}

	void Start ()
	{
		windowFunction = DopayWindow;
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
