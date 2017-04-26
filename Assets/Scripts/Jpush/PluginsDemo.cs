using UnityEngine;
using System.Collections;
using JPush;
using System.Collections.Generic;
using System;


public class PluginsDemo : MonoBehaviour
{
	#if UNITY_ANDROID
	string str_unity = "";
	bool B_MESSAGE = false;
	static string str_message = "";


	// Use this for initialization
	void Start()
	{       
        #if UNITY_EDITOR
        #elif JINLI
        #elif UNITY_ANDROID

        Debug.Log("进入PluginsDemo");
		gameObject.name = "Main Camera";
		JPushBinding.setDebug(true);
		JPushBinding.initJPush(gameObject.name, "");

		JPushEventManager.instance.addEventListener(
			CustomEventObj.EVENT_INIT_JPUSH, gameObject, "initJPush");

		JPushEventManager.instance.addEventListener(
			CustomEventObj.EVENT_STOP_JPUSH, gameObject, "stopJPush");

		JPushEventManager.instance.addEventListener(
			CustomEventObj.EVENT_RESUME_JPUSH, gameObject, "resumeJPush");

		JPushEventManager.instance.addEventListener(
			CustomEventObj.EVENT_SET_TAGS, gameObject, "setTags");

		JPushEventManager.instance.addEventListener(
			CustomEventObj.EVENT_SET_ALIAS, gameObject, "setAlias");

		JPushEventManager.instance.addEventListener(
			CustomEventObj.EVENT_ADD_LOCAL_NOTIFICATION, gameObject,
			"addLocalNotification");

#endif


    }

	// Update is called once per frame
    //void Update()
    //{
    //    if(Input.GetKeyUp(KeyCode.Escape) || Input.GetKeyUp(KeyCode.Home))
    //    {
    //        beforeQuit();
    //        Application.Quit();
    //    }
    //}

	// remove event listeners
	void OnDestroy()
	{
        #if UNITY_EDITOR
        #elif JINLI
        #elif UNITY_ANDROID
		print("unity3d---onDestroy");
		if (gameObject)
		{
			// remove all events
			JPushEventManager.instance.removeAllEventListeners(gameObject);
		}
        #endif
	}


	void initJPush(CustomEventObj evt)
	{
		Debug.Log("---triggered initjpush----");
		JPushBinding.initJPush(gameObject.name, "");
		//JPushBridge.initJPush();
	}

	void stopJPush(CustomEventObj evt)
	{
		Debug.Log("--triggered stopJPush----");
		JPushBinding.stopJPush();
	}

	void resumeJPush(CustomEventObj evt)
	{
		Debug.Log("---triggered resumeJPush----");
		JPushBinding.resumeJPush();
	}

	void setTags(CustomEventObj evt)
	{
		Debug.Log("---triggered setTags----");
		string tags = (string)evt.arguments["tags"];
		JPushBinding.setTags(tags);
	}

	void setAlias(CustomEventObj evt)
	{
		Debug.Log("---triggered setAlias----");
		string alias = (string) evt.arguments["alias"];
		JPushBinding.setAlias(alias);
	}

	void setPushTime(CustomEventObj evt)
	{
		Debug.Log("---triggered setPushTime----");
		string days = (string) evt.arguments["days"];
		int start_time = (int) evt.arguments["start_time"];
		int end_time = (int) evt.arguments["end_time"];
		JPushBinding.setPushTime(days, start_time, end_time);
	}

	void addLocalNotification(CustomEventObj evt)
	{
		Debug.Log("---triggered addLocalNotification---");
		int builderId = (int) evt.arguments["builderId"];
		string content = (string) evt.arguments["content"];
		string title = (string) evt.arguments["title"];
		int notiId = (int) evt.arguments["notificationId"];
		int broadcastTime = (int) evt.arguments["broadcastTime"];
		string extrasStr = (string) evt.arguments["extras"];
		JPushBinding.addLocalNotification(builderId, content, title, notiId,
			broadcastTime, extrasStr);
	}

	void removeLocalNotification(CustomEventObj evt)
	{
		Debug.Log("---triggered removeLocalNotification---");
		int notiId = (int) evt.arguments["notificationId"];
		JPushBinding.removeLocalNotification(notiId);
	}

	/* data format
		{
		    "message": "hhh",
		    "extras": {
		        "f": "fff",
		        "q": "qqq",
		        "a": "aaa"
		    }
		}
	*/
	//开发者自己处理由JPush推送下来的消息
	void recvMessage(string jsonStr)
	{
		Debug.Log("recv----message-----" + jsonStr);
		B_MESSAGE = true;
		str_message = jsonStr;
		str_unity = "有新消息";
	}

	/**
	* {
	*	"title": "notiTitle",
	*   "content": "content",
	*   "extras": {
	*		"key1": "value1",
	*       "key2": "value2"
	* 	}
	* }
	*/
	// 获取的是 json 格式数据，开发者根据自己的需要进行处理。
	void recvNotification(string jsonStr)
	{
		Debug.Log("recv---notification---" + jsonStr);
	}

	//开发者自己处理点击通知栏中的通知
	void openNotification(string jsonStr)
	{
		Debug.Log("recv---openNotification---" + jsonStr);
		str_unity = jsonStr;
	}

	void beforeQuit()
	{
		JPushBinding.isQuit();
	}
	#endif

	#if UNITY_IPHONE
	
	#endif

}
