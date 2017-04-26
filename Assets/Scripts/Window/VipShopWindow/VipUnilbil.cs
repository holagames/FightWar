
//-----------------------------------------------------------------
//  Copyright 2013 Alex McAusland and Ballater Creations
//  All rights reserved
//  www.outlinegames.com
//-----------------------------------------------------------------
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unibill.Impl;
using System.Text;
/// <summary>
/// An example of basic Unibill functionality.
/// </summary>
public class VipUnilbil : MonoBehaviour
{
    private PurchasableItem[] items;
	private string payid="";
    private const string DLC_ID = "episode2";
    void Start() {
        if (UnityEngine.Resources.Load ("unibillInventory.json") == null) {
            Debug.LogError("You must define your purchasable inventory within the inventory editor!");
            this.gameObject.SetActive(false);
            return;
        }

        // We must first hook up listeners to Unibill's events.
        Unibiller.onBillerReady += onBillerReady;
        Unibiller.onTransactionsRestored += onTransactionsRestored;
        Unibiller.onPurchaseCancelled += onCancelled;
	    Unibiller.onPurchaseFailed += onFailed;
		Unibiller.onPurchaseCompleteEvent += onPurchased;
        Unibiller.onPurchaseDeferred += onDeferred;
        //Unibiller.onDownloadProgressedEvent += (item, progress) => {
        //    Debug.Log(item + " " + progress);
        //};

        //Unibiller.onDownloadFailedEvent += (arg1, arg2) => {
        //    Debug.LogError(arg2);
        //};

        //Unibiller.onDownloadCompletedEventString += (obj, dir) => {
        //    Debug.Log("Completed download: " + obj);
        //    #if !(UNITY_WP8 || UNITY_METRO || UNITY_WEBPLAYER)
        //    foreach (var f in  new DirectoryInfo(dir).GetFiles()) {
        //        Debug.Log(f.Name);
        //        if (f.Name.EndsWith("txt") && f.Length < 10000) {
        //        #if !(UNITY_WP8 || UNITY_METRO || UNITY_WEBPLAYER)
        //            Debug.Log(Util.ReadAllText(f.FullName));
        //            #endif
        //        }
        //    }
        //    #endif
        //};

        // Now we're ready to initialise Unibill.
        Unibiller.Initialise();
//#if UNITY_IOS
//        Debug.Log("IOS打包");
//        var appleExtensions = Unibiller.getAppleExtensions();
//        appleExtensions.onAppReceiptRefreshed += x=>{
//            Debug.LogError(x);
//            Debug.Log("Refreshed app receipt!");
//        };

//        appleExtensions.onAppReceiptRefreshFailed += () =>
//        {
//            Debug.LogError("Failed to refresh app receipt.");
//        };
//#endif
        //initCombobox();
        //Unibiller.initiatePurchase(Unibiller.GetPurchasableItemById("80"));//充值
    }


    /// <summary>
    /// This will be called when Unibill has finished initialising.
    /// </summary>
    private void onBillerReady(UnibillState state)
    {
        UnityEngine.Debug.Log("onBillerReady:" + state);
    }

    /// <summary>
    /// This will be called after a call to Unibiller.restoreTransactions().
    /// </summary>
    private void onTransactionsRestored(bool success)
    {
        Debug.Log("Transactions restored.");
    }

    /// <summary>
    /// This will be called when a purchase completes.
    /// </summary>
    private void onPurchased(PurchaseEvent e)
    {
#if   UNITY_IOSOFFCIAL
			payid=e.PurchasedItem.Id;
			SuccessPay(e.Receipt);
			Debug.Log("Purchase OK: " + e.PurchasedItem.Id);
			Debug.Log("Receipt: " + e.Receipt);
#endif

        Debug.Log(string.Format("{0} has now been purchased {1} times.",
                                 e.PurchasedItem.name,
                                 Unibiller.GetPurchaseCount(e.PurchasedItem)));
    }

    /// <summary>
    /// This will be called if a user opts to cancel a purchase
    /// after going to the billing system's purchase menu.
    /// </summary>
    private void onCancelled(PurchasableItem item)
    {
        Debug.Log("Purchase cancelled: " + item.Id);
    }

    /// <summary>
    /// iOS Specific.
    /// This is called as part of Apple's 'Ask to buy' functionality,
    /// when a purchase is requested by a minor and referred to a parent
    /// for approval.
    /// 
    /// When the purchase is approved or rejected, the normal purchase events
    /// will fire.
    /// </summary>
    /// <param name="item">Item.</param>
    private void onDeferred(PurchasableItem item)
    {
        Debug.Log("Purchase deferred blud: " + item.Id);
    }

    /// <summary>
    /// This will be called is an attempted purchase fails.
    /// </summary>
    private void onFailed(PurchasableItem item)
    {
        Debug.Log("Purchase failed: " + item.Id);
    }
    //void OnGUI()
    //{
    //    if (GUI.Button(new Rect(0, Screen.height - Screen.width / 6.0f, Screen.width / 2.0f, Screen.width / 6.0f), "Buy"))
    //    {
    //        //switch (itemType)
    //        //{
    //        //    case 0:
    //        //        itemId = "valiantsoul.orb_monthly"; break;
    //        //    case 1:
    //        //        itemId = "valiantsoul.orb_f"; break;
    //        //    case 2:
    //        //        itemId = "valiantsoul.orb_e"; break;
    //        //    case 3:
    //        //        itemId = "valiantsoul.orb_d"; break;
    //        //    case 4:
    //        //        itemId = "valiantsoul.orb_c"; break;
    //        //    case 5:
    //        //        itemId = "valiantsoul.orb_b"; break;
    //        //    case 6:
    //        //        itemId = "valiantsoul.orb_a"; break;
    //        //}
    //        Unibiller.initiatePurchase(Unibiller.GetPurchasableItemById("com.qianhuan.yxgsd.ios.d60"));
    //    }
    //}

	//CHOGNZHI Suecces
    public void SuccessPay(string json)
    {
		StartCoroutine (PayInfo(json));
		Debug.LogError ("json  " + json);
	}
    IEnumerator PayInfo(string json)
    {
        string url = "http://139.196.148.228/iossdk/iospay.php?uid=";
		StringBuilder s = new StringBuilder();
		s.Append(url);
		Debug.LogError ("url  " + s);
		s.Append(CharacterRecorder.instance.userId.ToString() + "_" + PlayerPrefs.GetString("ServerID") + "_" + ExchangeID(payid));//CharacterID
		Debug.LogError ("yonghuxinxi  " + s);
		s.Append("&inapp_purchase_data=");
		s.Append(WWW.EscapeURL(json));
		Debug.LogError ("zuihouxinxi  " + s);
		WWW www = new WWW(s.ToString());
		yield return www;
		if (www.isDone)
		{
			Debug.LogError("购买成功");
		}
	}

    string ExchangeID(string id)
    {
        string idinfo = "";
        switch (id)
        {
            case "com.qianhuan.yxgsd.ios.d60":
                idinfo = "60_1";
                break;
            case "com.qianhuan.yxgsd.ios.d300":
                idinfo = "300_2";
                break;
            case "com.qianhuan.yxgsd.ios.d980":
                idinfo = "980_3";
                break;
            case "com.qianhuan.yxgsd.ios.d1980":
                idinfo = "1980_4";
                break;
            case "com.qianhuan.yxgsd.ios.d3280":
                idinfo = "3280_5";
                break;
            case "com.qianhuan.yxgsd.ios.d6480":
                idinfo = "6480_6";
                break;
            case "com.qianhuan.yxgsd.ios.d10":
                idinfo = "10_9";
                break;
            case "com.qianhuan.yxgsd.ios.m300":
                idinfo = "300_7";
                break;
            case "com.qianhuan.yxgsd.ios.m980":
                idinfo = "980_8";
                break;
        }
        return idinfo;
    }
}

