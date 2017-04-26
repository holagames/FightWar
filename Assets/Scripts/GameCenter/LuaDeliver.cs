using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using LuaInterface;
using CodeStage.AntiCheat.ObscuredTypes;

using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Core;


public class LuaDeliver : MonoBehaviour
{
    // Use this for initialization

    public static LuaDeliver instance;
    public LuaFunction func;
    public LuaFunction func1;
    public LuaFunction OpenWindow;
    public LuaFunction funcUpdate;
    public LuaFunction Guide_Main;
    public LuaFunction Guide_Manager;

    public Dictionary<string, string> DictionaryLua = new Dictionary<string, string>();

    public static LuaScriptMgr luaMgr = null;
    public Vector3 startPositionV3;
    public Vector3 endPositionV3;
    public Vector3 startRotationV3;
    public Vector3 endRotationV3;

    public static string NowLua = "";

    void Awake()
    {
        luaMgr = new LuaScriptMgr();

        luaMgr.Start();
    }

    void Start()
    {
        instance = this;
    }

    public void StartLua()
    {
        LoadLua("LuaManager");
        LoadLua("NetManager");
        func = luaMgr.GetLuaFunction("NetSplit");
        func1 = luaMgr.GetLuaFunction("UpdateTimeShow");  //活动时间刷新
        OpenWindow = luaMgr.GetLuaFunction("OpenActivityWindow");
        LoadGuideLua();
    }

    public void UILoadLua(string Lua)
    {
        if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) != -1)
        {
            if (Lua == "NewGuideWindow")
            {
                Lua = "GuideWindow";
            }
            if (Lua != "LoginWindow" && Lua != "InitWindow" && Lua != "LoadingWindow" && Lua != "LoadWindow" && Lua != "GuideWindow" && Lua != "LevelUpWindow" && Lua != "WhiteWindow" && Lua != "AccountWindow" && Lua != "ServerSelectionWindow" && Lua != "GameAnnouncementWindow" && Lua != "PreloadCommon" && Lua != "PreloadNormal" && Lua != "PreloadMap" && Lua != "PreloadSkill" && Lua != "PreloadItem" && Lua != "PreloadAvatar" && Lua != "PreloadCloud" && Lua != "PreloadWood" && Lua != "PreloadSkillText" && Lua != "AdvanceWindow" && Lua != "PromptWindow")
            {
                UIManager.instance.OpenGuideWindow();
            }
            if (GameObject.Find("MainWindow") != null && GameObject.Find("GuideWindow") != null && GameObject.Find("GuideButtonPoint"))
            {
                //Debug.LogError(Lua);
                GameObject.Find("GuideButtonPoint").GetComponent<UITexture>().height = 200;
                GameObject.Find("GuideButtonPoint").GetComponent<UITexture>().width = 200;
                GameObject.Find("GuidePartLeft").GetComponent<UITexture>().height = 200;
                GameObject.Find("GuidePartRight").GetComponent<UITexture>().height = 200;
                GameObject.Find("GuidePartTop").transform.localPosition = new Vector3(GameObject.Find("GuidePartTop").transform.localPosition.x, GameObject.Find("GuidePartTop").GetComponent<UITexture>().height / 2f + 100, GameObject.Find("GuidePartTop").transform.localPosition.z);
                GameObject.Find("GuidePartBottom").transform.localPosition = new Vector3(GameObject.Find("GuidePartBottom").transform.localPosition.x, (-GameObject.Find("GuidePartBottom").GetComponent<UITexture>().height / 2f) - 100, GameObject.Find("GuidePartBottom").transform.localPosition.z);
                GameObject.Find("GuidePartLeft").transform.localPosition = new Vector3((-GameObject.Find("GuidePartLeft").GetComponent<UITexture>().width / 2f) - 100, GameObject.Find("GuidePartLeft").transform.localPosition.y, GameObject.Find("GuidePartLeft").transform.localPosition.z);
                GameObject.Find("GuidePartRight").transform.localPosition = new Vector3(GameObject.Find("GuidePartRight").GetComponent<UITexture>().width / 2f + 100, GameObject.Find("GuidePartRight").transform.localPosition.y, GameObject.Find("GuidePartRight").transform.localPosition.z);
            }
        }

        LoadLua(Lua);
        if (Lua == "GuideWindow")
        {
            luaMgr.CallLuaFunction("GuideStation");
        }
    }

    public void UseGuideStation()
    {
        luaMgr.CallLuaFunction("GuideStation");
    }

    public void UseAddGuideState()
    {
        luaMgr.CallLuaFunction("AddGuideState");
    }

    public void LoadGuideLua()
    {
        LoadLua("GuideManager");
        //LoadLua("MainGuide");
        //Guide_Main = l.GetFunction("Lsss");
        //LoadLua("GateGuide");
    }

    public void ResetGuide()
    {
        luaMgr.CallLuaFunction("ResetGuide");
    }

    public void LoadLua(string Lua)
    {
        ////////////////////////////////直接读////////////////////////////////
        if (Resources.Load("Lua/" + Lua + ".lua") != null)
        {
            TextAsset TextScript = Resources.Load("Lua/" + Lua + ".lua") as TextAsset;
            luaMgr.DoString(TextScript.text);
            //l.DoString(TextScript.text);
        }
        ////////////////////////////////直接读////////////////////////////////

        ///////////////////////////////////下载读//////////////////////////////////////////
        //if (!DictionaryLua.ContainsKey(Lua))
        //{
        //    string url = ObscuredPrefs.GetString("DownloadUrl", "") + Lua + ".lua.unity3d";
        //    Debug.Log(url + "++++" + VersionChecker.instance.DictionaryVersion[Lua + ".lua"]);
        //    WWW www = WWW.LoadFromCacheOrDownload(url, VersionChecker.instance.DictionaryVersion[Lua + ".lua"]);
        //    TextAsset TextScript = www.assetBundle.Load(Lua + ".lua") as TextAsset;
        //    Debug.Log(TextScript.bytes.Length);


        //    System.IO.MemoryStream tMS = new System.IO.MemoryStream(TextScript.bytes);
        //    ZipInputStream tZIS = null;
        //    byte[] tBuffer = null;

        //    try
        //    {
        //        tZIS = new ZipInputStream(tMS);
        //        tZIS.Password = "projectn";

        //        ZipEntry tEntry;

        //        for (; ; )
        //        {
        //            tEntry = tZIS.GetNextEntry();
        //            if (tEntry == null)
        //            {
        //                break;
        //            }
        //            if (tEntry.Size <= 0)
        //            {
        //                Debug.LogError("Bad Table File : " + tEntry.Name + "  Size is :" + tEntry.Size);
        //            }
        //            tBuffer = new byte[tEntry.Size];
        //            int i, length = tBuffer.Length, o = 0, r;
        //            for (i = 0; i < length; i++)
        //            {
        //                r = tZIS.Read(tBuffer, o, length - o);
        //                if (r <= 0)
        //                {
        //                    break;
        //                }

        //                o = o + r;
        //                if (o >= length)
        //                {
        //                    break;
        //                }
        //            }
        //            Debug.Log(tEntry.Size);
        //            //break;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.Log(ex.ToString());
        //    }

        //    if (tZIS != null)
        //    {
        //        tZIS.Close();
        //        tZIS = null;
        //    }

        //    tMS.Close();
        //    tMS = null;
        //    www.Dispose();

        //    Debug.Log(Encoding.UTF8.GetString(tBuffer));
        //    l.DoString(Encoding.UTF8.GetString(tBuffer));
        //    DictionaryLua.Add(Lua, Encoding.UTF8.GetString(tBuffer));
        //}
        //else
        //{
        //    l.DoString(DictionaryLua[Lua]);
        //}

        ///////////////////////////////////下载读//////////////////////////////////////////
    }

    public void LoadGuide()
    {
        //DoFile("Main.lua");
        //CallLuaFunction("Main");
        //funcUpdate = luaMgr.GetLuaFunction("GuideUpdate");
        //lateUpdateFunc = GetLuaFunction("LateUpdate");
        //fixedUpdateFunc = GetLuaFunction("FixedUpdate");
        //levelLoaded = GetLuaFunction("OnLevelWasLoaded");
    }

    public static void AddPress(GameObject go, string lua)
    {
        if (go != null)
        {
            UIEventListener.Get(go).onPress += delegate(GameObject o, bool IsPress)
            {
                luaMgr.CallLuaFunction(lua, go, IsPress);
            };
        }
    }

    public static void AddGuideClick(GameObject go, float delayTime)
    {
        ResourceLoader.instance.AddGuideClick(go, delayTime);
    }

    public static void SetGuideDelay(float delayTime)
    {
        ResourceLoader.instance.SetGuideDelay(delayTime);
    }

    public void SetGuideDelayCsharp(float delayTime)
    {
        StartCoroutine(GuideDelayCsharp(delayTime));
    }
    IEnumerator GuideDelayCsharp(float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
    }
    public static void AddArrowClick(Vector3 EndPV3, float EndR)
    {
        ResourceLoader.instance.SetGuideArrow(EndPV3, EndR);
    }

    public static void AddArrowMove(Vector3 StartPV3, Vector3 EndPV3)
    {
        ResourceLoader.instance.MoveGuideArrow(StartPV3, EndPV3);
    }

    public static void AddLabelClick(Vector3 EndPV3)
    {
        GameObject.Find("LabelWindow").transform.localPosition = EndPV3;
    }

    public static void StartLogin()
    {
        VersionChecker.instance.StartLogin();
    }

    public static void AddClick(GameObject go, string lua)
    {
        //Debug.LogError(go + " " + lua);
        NowLua = lua;
        if (go != null)
        {
            if (UIEventListener.Get(go).onClick == null)
            {
                UIEventListener.Get(go).onClick += NewClick;
            }
            else
            {
                if (UIEventListener.Get(go).onClick.GetInvocationList().Length < 2)
                {
                    UIEventListener.Get(go).onClick += NewClick;
                }
            }

            //Debug.LogError(UIEventListener.Get(go).onClick.GetInvocationList().Length + "  **************");
        }
    }

    public static void MainWindowButtonAddClick(GameObject go)
    {
        if (go != null)
        {
            if (UIEventListener.Get(go).onClick == null)
            {
                UIEventListener.Get(go).onClick += MessageClick;
            }
            else
            {
                if (UIEventListener.Get(go).onClick.GetInvocationList().Length < 3)
                {
                    UIEventListener.Get(go).onClick += MessageClick;
                }
            }

            Debug.LogError(UIEventListener.Get(go).onClick.GetInvocationList().Length + "  **************提醒事件");
        }
    }
    static void MessageClick(GameObject go)
    {
        GameObject.Find("NewGuideWindow").GetComponent<NewGuidWindow>().SetInfo(0, "战斗", 0, "", "");
        SceneTransformer.instance.isNewGuide = false;
        UIEventListener.Get(go).onClick -= NewClick;
    }

    static void NewClick(GameObject go)
    {
        Debug.LogError(go + " " + NowLua);
        if (go != null)
        {
            if (go.name == "RoleButton")
            {
                CharacterRecorder.instance.enterRoleFromMain = true;
            }
            luaMgr.CallLuaFunction(NowLua, go);
            UIEventListener.Get(go).onClick -= NewClick;
            Debug.LogError("-----------------------------------");
        }
    }


    public static void AddChangeClick(GameObject go, string lua)//关闭/跳转界面使用
    {
        Debug.Log(go + " " + lua);
        NowLua = lua;
        if (go != null)
        {
            //if (GameObject.Find("PromptWindow") == null && GameObject.Find("AwardWindow") == null)
            {
                UIEventListener.Get(go).onClick += NewClick;
            }

            Debug.Log(UIEventListener.Get(go).onClick.GetInvocationList().Length + "  **************");

            if (UIEventListener.Get(go).onClick.GetInvocationList().Length == 2)
            {
                UIEventListener.VoidDelegate Temp1 = (UIEventListener.VoidDelegate)UIEventListener.Get(go).onClick.GetInvocationList()[0];
                UIEventListener.VoidDelegate Temp2 = (UIEventListener.VoidDelegate)UIEventListener.Get(go).onClick.GetInvocationList()[1];
                UIEventListener.Get(go).onClick = null;
                UIEventListener.Get(go).onClick += Temp2;
                UIEventListener.Get(go).onClick += Temp1;
            }
            else if (UIEventListener.Get(go).onClick.GetInvocationList().Length > 2)
            {
                UIEventListener.VoidDelegate Temp1 = (UIEventListener.VoidDelegate)UIEventListener.Get(go).onClick.GetInvocationList()[0];
                UIEventListener.VoidDelegate Temp2 = (UIEventListener.VoidDelegate)UIEventListener.Get(go).onClick.GetInvocationList()[1];
                UIEventListener.Get(go).onClick = null;
                UIEventListener.Get(go).onClick += Temp1;
                UIEventListener.Get(go).onClick += Temp2;
            }
        }
    }




    public static void SetActive(GameObject go, bool IsActive)
    {
        if (go != null)
        {
            go.SetActive(IsActive);
            if(go.name == "TenCardRewardWindow" && IsActive)
            {
                GameObject.Find("LeftDrawButoton").GetComponent<UIToggle>().value = true;
            }
        }
    }

    public static void SetGuideChildActive(GameObject go, string ChildName, bool IsActive)
    {
        if (go != null)
        {
            if (!IsActive)
            {
                for (int g = 0; g < go.transform.childCount; g++)
                {
                    GameObject obj = go.transform.GetChild(g).gameObject;
                    go.transform.Find(obj.name).gameObject.SetActive(IsActive);
                }
                go.transform.Find(ChildName).gameObject.SetActive(!IsActive);
            }
            else
            {
                for (int g = 0; g < go.transform.childCount; g++)
                {
                    GameObject obj = go.transform.GetChild(g).gameObject;
                    go.transform.Find(obj.name).gameObject.SetActive(IsActive);
                }
            }
        }
    }

    public static void OpenPanel(string PanelName, bool IsCloseOther)
    {
        UIManager.instance.OpenPanel(PanelName, IsCloseOther);
    }
    public static void OpenSinglePanel(string PanelName, bool IsCloseOther)
    {
        UIManager.instance.OpenSinglePanel(PanelName, IsCloseOther);
    }
    public static void ClosePanel(string PanelName, bool isDesObJ)
    {
        if (isDesObJ)
        {
            GameObject go = GameObject.Find("UIRoot").transform.Find(PanelName).gameObject;
            UIManager.instance.ClosePanel(PanelName);
            DestroyGameObjct(go);
        }
        else
        {
            UIManager.instance.ClosePanel(PanelName);
        }

    }

    public static void UpdateGUIAtlas(string GUIName, string AtlasName)
    {
        VersionChecker.instance.UpdateGUIAtlas(GUIName, AtlasName);
    }

    public static void LoadGridPrefab(string Path, string Name, GameObject go)
    {
        GameObject PrefabButton = GameObject.Instantiate(Resources.Load("GUI/" + Path, typeof(GameObject))) as GameObject;
        PrefabButton.name = Name;
        PrefabButton.transform.parent = go.transform;
        PrefabButton.transform.localScale = new Vector3(1, 1, 1);
        PrefabButton.transform.localPosition = new Vector3(0, 0, 0);
    }

    public static void GridReposition(GameObject go)
    {
        if (go != null)
        {
            if (go.GetComponent<UIGrid>() != null)
            {
                go.GetComponent<UIGrid>().Reposition();
            }
        }
    }

    public static void StartFight()
    {
        PictureCreater.instance.StartFight();
    }
    public static void SetClickButtonPosition(Vector3 positionV3)
    {
        GameObject.Find("GuideButtonPoint").transform.position = positionV3;
    }
    public static void AddOnClick(GameObject obj, string lua)
    {
        UIEventListener.Get(obj).onClick = null;
        UIEventListener.Get(obj).onClick = delegate(GameObject Button)
        {
            luaMgr.CallLuaFunction(lua, obj);
        };

    }
    //新手引导恢复战斗
    public static void GuideIsLock()
    {
        PictureCreater.instance.IsLock = false;
    }
    //新手引导查询内容
    public string GetGuideStateName()
    {
        string name = "GuideState_" + PlayerPrefs.GetString("ServerID") + "_" + PlayerPrefs.GetInt("UserID");
        return name;
    }

    public string GetGuideSubStateName()
    {
        string name = "GuideSubState_" + PlayerPrefs.GetString("ServerID") + "_" + PlayerPrefs.GetInt("UserID");
        return name;
    }
    public static void AudioNewGuide(string audioName)//新手引导声音特效
    {
        //AudioEditer.instance.PlayOneShot(audioName);
        if (audioName == "Story22")
        {
            GameObject.Find("NewGuideWindow").GetComponent<NewGuidWindow>().EventTalk = true;
        }
        GameObject.Find("NewGuideWindow").GetComponent<AudioSource>().Stop();
        GameObject.Find("NewGuideWindow").GetComponent<AudioSource>().PlayOneShot(Resources.Load("Audio/" + audioName) as AudioClip);

    }

    #region 热更新活动
    public static void AddHotFixClick(GameObject go, string lua)
    {
        Debug.Log(go + " " + lua);
        //NowLua = lua;
        if (go != null)
        {
            UIEventListener.Get(go).onClick = delegate(GameObject Go)
            {
                luaMgr.CallLuaFunction(lua, go);
            };
            Debug.Log(UIEventListener.Get(go).onClick.GetInvocationList().Length + "  **************");
        }
    }

    public static void SetLocalPosition(GameObject go, Vector3 Vecid)
    {
        go.transform.localPosition = Vecid;
    }
    public static void UpdateShowTime() //时间显示
    {
        ResourceLoader.instance.UpdateShowTime();
    }
    //查找对象
    public static GameObject GameFindObj(string ObjName)
    {
        GameObject go = GameObject.Find(ObjName) as GameObject;
        return go;
    }
    //transform查找对象
    public static GameObject TransformFindObj(string ParentObj, string ObjName)
    {
        GameObject ParObj = GameObject.Find(ParentObj) as GameObject;
        GameObject go = ParObj.transform.Find(ObjName).gameObject;
        return go;
    }
    //只克隆对象
    public static GameObject GameInstantiateObj(string ObjItem)
    {
        GameObject obj = Instantiate(GameObject.Find(ObjItem)) as GameObject;
        return obj;
    }
    //发送提示信息
    public static void UIManagerOpen(string SendMessage)
    {
        UIManager.instance.OpenPromptWindow(SendMessage, PromptWindow.PromptType.Hint, null, null);
    }
    //打开界面
    public static void UIManagerOpenPanl(string SendMessage, bool isFlase, Vector3 Vecid)
    {
        UIManager.instance.OpenSinglePanel(SendMessage, isFlase);
        GameObject.Find(SendMessage).transform.localPosition = Vecid;

    }
    //
    public static Texture ResourcesLoadTexture(string id, string ItemID)
    {
        Texture go=Resources.Load(id+"/" + ItemID) as Texture;
        return go;
    }

    //克隆 服务体和名字
    public static GameObject InstantiateObj(GameObject item, GameObject ParentObj, string itemName)
    {
        GameObject go = Instantiate(item) as GameObject;
        go.transform.parent = ParentObj.transform;
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;
        go.name = itemName;
        go.SetActive(true);
        return go;
    }

    //grid刷新
    public static void GridResNow(GameObject Grid)
    {
        Grid.GetComponent<UIGrid>().repositionNow = true;
    }

    //删除对象
    public static void DestroyGameObjct(GameObject item)
    {
        DestroyImmediate(item);
    }

    //抽奖弹出动画
    public static void GachaAnimation(int RoleID)
    {
        UIManager.instance.OpenSinglePanel("CardWindow", false);
        GameObject _CardWindow = GameObject.Find("CardWindow");
        if (_CardWindow != null)
        {
            _CardWindow.GetComponent<CardWindow>().SetCardInfo(RoleID);
        }
    }
    //热更 活动itemlist
    public static void LuaOpenAdvanceWindow(string dataSplit)
    {
        List<Item> _itemList = new List<Item>();
        string[] itemSplit = dataSplit.Split('!');
        for (int i = 0; i < itemSplit.Length - 1; i++)
        {
            string[] dataSplit3 = itemSplit[i].Split('$');
            _itemList.Add(new Item(int.Parse(dataSplit3[0]), int.Parse(dataSplit3[1])));
        }
        UIManager.instance.OpenSinglePanel("AdvanceWindow", false);
        GameObject.Find("AdvanceWindow").GetComponent<AdvanceWindow>().SetInfo(AdvanceWindowType.GainResult, null, null, null, null, _itemList);
        NetworkHandler.instance.UpDateTopContentData(_itemList);
    }
    public static void LuaTopContent()
    {
        GameObject.Find("TopContent").GetComponent<TopContent>().Reset();
    }
    //按住显示物品信息
    public static void ItemInfoMessage(GameObject OneItem, int ItemCode, int ItemCount)
    {
        TextTranslator.instance.ItemDescription(OneItem, ItemCode, ItemCount);
    }
    //跑马灯
    public static void MarqueeInfoMessage(int HeroID)
    {
        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(HeroID);
        NetworkHandler.instance.SendProcess(string.Format("7002#{0};{1};{2};{3}", 7, CharacterRecorder.instance.characterName, _ItemInfo.itemName, 0));
    }
    //活动统计
    public static void AcivityButtonStatistics(string ButtonName)
    {
        switch (ButtonName)
        {
            case "十连送橙将":
                UIManager.instance.CountActivitys(UIManager.Activitys.十连送橙将);
                UIManager.instance.UpdateActivitys(UIManager.Activitys.十连送橙将);
                break;
            case "每日折扣":
                UIManager.instance.CountActivitys(UIManager.Activitys.每日折扣);
                UIManager.instance.UpdateActivitys(UIManager.Activitys.每日折扣);
                break;
            case "活动":
                UIManager.instance.CountActivitys(UIManager.Activitys.活动);
                UIManager.instance.UpdateActivitys(UIManager.Activitys.活动);
                break;
            case "领取体力":
                UIManager.instance.CountActivitys(UIManager.Activitys.领取体力);
                UIManager.instance.UpdateActivitys(UIManager.Activitys.领取体力);
                break;
        }

    }
    //活动红点提示
    public static void EventButtonRedMessage(int ButtonID, bool isOpen)
    {
        //GameObject.Find("UIRoot").transform.Find("MainWindow").GetComponent<MainWindow>().ShowPrompt(ButtonID, isOpen);
        CharacterRecorder.instance.SetRedPoint(ButtonID, isOpen);
    }
    #endregion
}
