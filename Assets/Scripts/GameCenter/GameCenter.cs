using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;

public class GameCenter : MonoBehaviour
{
    
    public static string leavelName = "PVPWindow";
    /************************************************翻译完成********************************************/
    //Public Value
    /** 输出类 **/
    public GameObject gameDebugTracer = null;
    /** 角色类 **/
    public GameObject gameCharacterRecorder = null;
    /** 文字类 **/
    public GameObject gameTextTranslator = null;
    /** 资源类 **/
    public GameObject gameResourceLoader = null;
    /** 时间类 **/
    public GameObject gameTimeController = null;
    /** 网络类 **/
    public GameObject gameNetworkHandler = null;
    /** 图片类 **/
    public GameObject gamePictureCreater = null;
    /** 战斗类 **/
    public GameObject gameFightEmulator = null;
    /** 聊天类 **/
    public GameObject gameChatReceiver = null;
    /** 介面类 **/
    public GameObject gameUIManager = null;
    /** 场景类 **/
    public GameObject gameSceneTransformer = null;
    /** 任务类 **/
    public GameObject gameTaskDesigner = null;
    /** 副本（关卡）类 **/
    public GameObject gameGateChanger = null;
    /** 特效类 **/
    public GameObject gameEffectMaker = null;
    /** 界面类 **/
    public GameObject gameGUIPainter = null;
    /** 外挂类 **/
    public GameObject gameXMLParser = null;
    //背景音乐控制
    public GameObject gameAudioEditer = null;
    //背景音乐控制
    public GameObject gameLuaDeliver = null;
    
    /** 使用语言 **/
    public string GameLanguage;
    /** 使用平台 **/
    public string GamePlatform;
    /** 版本号 **/
    public int GameVersion = 1;
    public int CodeVersion = 1;
    public string GameVersionCode = "";
    /** 下载地址 **/
    public string DownloadUrl = "";

    
    
    void Awake()
    {
        GameVersion = ObscuredPrefs.GetInt("GameVersion", 1);
        CodeVersion = ObscuredPrefs.GetInt("CodeVersion", 1);
        DownloadUrl = ObscuredPrefs.GetString("DownloadUrl", "");
        Debug.Log("GameVersionCode:" + GameVersion + "DownloadUrl:" + DownloadUrl);

        GameLanguage = "CN";
        GamePlatform = "Android";
        GameVersionCode = (1000 + GameVersion).ToString();
        GameVersionCode = GameVersionCode[0] + "." + GameVersionCode[1] + "." + GameVersionCode[2] + "." + GameVersionCode[3];
        
        //帧率
        Application.targetFrameRate = 30;
        //禁用屏幕变暗
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        ////////////////////////载入任务管理(以下)////////////////////
        //gameObject.GetComponent<GameCenter>().gameLuaDeliver = new GameObject("gameLuaDeliver");
        //gameObject.GetComponent<GameCenter>().gameLuaDeliver.AddComponent("LuaDeliver");
        //gameObject.GetComponent<GameCenter>().gameLuaDeliver.transform.parent = gameObject.transform;
        ////////////////////////载入任务管理(以上)////////////////////

        //////////////////////////载入介面管理(以下)////////////////////
        gameObject.GetComponent<GameCenter>().gameUIManager = new GameObject("gameUIManager");
        gameObject.GetComponent<GameCenter>().gameUIManager.AddComponent("UIManager");
        gameObject.GetComponent<GameCenter>().gameUIManager.transform.parent = gameObject.transform;
        //////////////////////////载入介面管理(以上)////////////////////


        //////////////////////////载入网路管理(以下)////////////////////
        gameObject.GetComponent<GameCenter>().gameNetworkHandler = new GameObject("gameNetworkHandler");
        gameObject.GetComponent<GameCenter>().gameNetworkHandler.AddComponent("NetworkHandler");
        gameObject.GetComponent<GameCenter>().gameNetworkHandler.transform.parent = gameObject.transform;
        //////////////////////////载入网路管理(以上)////////////////////

        ////////////////////载入音乐控制(以下)////////////////////
        gameObject.GetComponent<GameCenter>().gameAudioEditer = new GameObject("gameAudioEditer");
        gameObject.GetComponent<GameCenter>().gameAudioEditer.AddComponent("AudioEditer");
        gameObject.GetComponent<GameCenter>().gameAudioEditer.transform.parent = gameObject.transform;
        ////////////////////载入音乐控制(以上)////////////////////

        //////////////////////载入XML管理(以下)////////////////////
        gameObject.GetComponent<GameCenter>().gameXMLParser = new GameObject("gameXMLParser");
        gameObject.GetComponent<GameCenter>().gameXMLParser.AddComponent("XMLParser");
        gameObject.GetComponent<GameCenter>().gameXMLParser.transform.parent = gameObject.transform;
        //////////////////////载入XML管理(以上)////////////////////

        ////////////////////载入文字管理(以下)////////////////////
        gameObject.GetComponent<GameCenter>().gameTextTranslator = new GameObject("gameTextTranslator");
        gameObject.GetComponent<GameCenter>().gameTextTranslator.AddComponent("TextTranslator");
        gameObject.GetComponent<GameCenter>().gameTextTranslator.transform.parent = gameObject.transform;
        ////////////////////载入文字管理(以上)////////////////////

        ////////////////////载入Debug管理(以下)////////////////////
        gameObject.GetComponent<GameCenter>().gameDebugTracer = new GameObject("gameDebugTracer");
        gameObject.GetComponent<GameCenter>().gameDebugTracer.AddComponent("DebugTracer");
        gameObject.GetComponent<GameCenter>().gameDebugTracer.transform.parent = gameObject.transform;
        ////////////////////载入Debug管理(以上)////////////////////

        //////////////////载入角色管理(以下)////////////////////
        gameObject.GetComponent<GameCenter>().gameCharacterRecorder = new GameObject("gameCharacterRecorder");
        gameObject.GetComponent<GameCenter>().gameCharacterRecorder.AddComponent("CharacterRecorder");
        gameObject.GetComponent<GameCenter>().gameCharacterRecorder.transform.parent = gameObject.transform;
        //////////////////载入角色管理(以上)////////////////////

        ////////////////////载入资源管理(以下)////////////////////
        gameObject.GetComponent<GameCenter>().gameResourceLoader = new GameObject("gameResourceLoader");
        gameObject.GetComponent<GameCenter>().gameResourceLoader.AddComponent("ResourceLoader");
        gameObject.GetComponent<GameCenter>().gameResourceLoader.transform.parent = gameObject.transform;
        ////////////////////载入资源管理(以上)////////////////////

        //////////////////////载入场景管理(以下)////////////////////
        gameObject.GetComponent<GameCenter>().gameSceneTransformer = new GameObject("gameSceneTransformer");
        gameObject.GetComponent<GameCenter>().gameSceneTransformer.AddComponent("SceneTransformer");
        gameObject.GetComponent<GameCenter>().gameSceneTransformer.transform.parent = gameObject.transform;
        //////////////////////载入场景管理(以上)////////////////////

        //////////////////////载入图片管理(以下)////////////////////        
        gameObject.GetComponent<GameCenter>().gamePictureCreater = new GameObject("gamePictureCreater");
        gameObject.GetComponent<GameCenter>().gamePictureCreater.AddComponent("PictureCreater");
        gameObject.GetComponent<GameCenter>().gamePictureCreater.transform.parent = gameObject.transform;
        //////////////////////载入图片管理(以上)////////////////////

        //////////////////////载入特效管理(以下)////////////////////
        gameObject.GetComponent<GameCenter>().gameEffectMaker = new GameObject("gameEffectMaker");
        gameObject.GetComponent<GameCenter>().gameEffectMaker.AddComponent("EffectMaker");
        gameObject.GetComponent<GameCenter>().gameEffectMaker.transform.parent = gameObject.transform;
        //////////////////////载入特效管理(以上)////////////////////

        //////////////////////载入战斗管理(以下)////////////////////
        //gameFightEmulator = new GameObject("gameFightEmulator");
        //gameFightEmulator.AddComponent<FightEmulator>();
        //gameFightEmulator.transform.parent = gameObject.transform;
        //////////////////////载入战斗管理(以上)////////////////////

        //////////////////////载入介面管理(以下)////////////////////
        //gameGUIPainter = new GameObject("gameGUIPainter");
        //gameGUIPainter.AddComponent<GUIPainter>();
        //gameGUIPainter.transform.parent = gameObject.transform;
        //////////////////////载入介面管理(以上)////////////////////

        //////////////////////载入任务管理(以下)////////////////////
        gameObject.GetComponent<GameCenter>().gameTaskDesigner = new GameObject("gameTaskDesigner");
        gameObject.GetComponent<GameCenter>().gameTaskDesigner.AddComponent("TaskDesigner");
        gameObject.GetComponent<GameCenter>().gameTaskDesigner.transform.parent = gameObject.transform;
        //////////////////////载入任务管理(以上)////////////////////
    }
}
