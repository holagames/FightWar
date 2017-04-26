using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using Holagames;
using System;

public class LoginWindow : MonoBehaviour
{
    bool IsPlaySound = false;
    public UIInput userName;
    public UIInput passWord;
    public GameObject Login;


    public GameObject di;
    //public GameObject DaZhanLue;
    public GameObject Man;
    public GameObject WangPai;
    public GameObject Daodan;
    public GameObject HuoXin;
    public GameObject BaoZha;
    public GameObject BG;
    public GameObject Light;
    public GameObject TouchToStart;

    public GameObject DaZhanLue;
    public GameObject DaZhanLue02;
    public GameObject LOGO_di2;

    public GameObject SpriteBg;
    public UILabel ChoseSever;
    public UISprite Zhuangtai;
    public GameObject ServerButton;
    public GameObject StratButton;
    public GameObject LoginButton;

    public GameObject AnnouncementBtn;
    public GameObject AdminBtn;

    public GameObject effectPrefab;
    public UILabel LabelVersion;

    public GameObject GameObjectQQ;
    public GameObject TextureQQ;
    public GameObject TextureWX;
    public GameObject ChangeUser;

    //public int ServerId;
    public string ServerTag;
    bool isOpenDaoDan = false;
    bool isOpenBaoZha = false;

    private ServerList _OneServerItem;
    void Start()
    {
        UIEventListener.Get(LoginButton).onClick = delegate(GameObject go)
        {
            if (GameObject.Find("MainCamera").GetComponent<HuaWeiGameCenter>().IsInit)
            {
                HolagamesSDK.getInstance().Login("");
            }
            else
            {
                GameObject.Find("MainCamera").GetComponent<HuaWeiGameCenter>().StartSDK();
            }
        };
        //userName.value = ObscuredPrefs.GetString("Account", "");
        //passWord.value = ObscuredPrefs.GetString("Password", "");

        //UIEventListener.Get(BG).onClick += delegate(GameObject go)
        //{
        //    if (GameObject.Find("LoginWindow/WF_UI_LoGo") != null)
        //    {
        //        NetworkHandler.instance.IsPrefetch = false;
        //        if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 0)
        //        {
        //            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_200);
        //        }
        //        StartCoroutine(NetworkHandler.instance.LoadScene());
        //    }
        //    //Login.SetActive(true);
        //};
        //if (PlayerPrefs.GetInt("ServerNum") == 0)
        //{
        //    ChoseSever.text = "请选择服务器";
        //}
        //else
        //{
        //    ChoseSever.text = "请选择服务器1";
        //}
        //UIEventListener.Get(ServerButton).onClick += delegate(GameObject go)
        //{
        //    UIManager.instance.OpenPanel("ServerSelectionWindow", true);
        //};
        Invoke("PlaySoundForElec", 2.5f);
        Invoke("PlaySoundForWaiPai", 0.3f);
        if (NetworkHandler.instance.IsSDKLogin) //for android haima
        {
            Invoke("State7", 3.5f);
        }
        else
        {
            LoginButton.SetActive(true);
        }
        NetworkHandler.instance.IsReconnect = false;
        NetworkHandler.instance.ReconnectTimer = 0.1f;
        string Version = CharacterRecorder.instance.GameVersion.ToString();
        LabelVersion.text = Version[0] + "." + Version[1] + "." + Version[2] + "." + Version[3];
    }

    void PlaySoundForElec()
    {
        AudioEditer.instance.PlayOneShot("electricboom");
    }
    void PlaySoundForWaiPai()
    {
        AudioEditer.instance.PlayOneShot("ui_start");
    }

    void LateUpdate()
    {
        if (!IsPlaySound)
        {
            IsPlaySound = true;
            AudioEditer.instance.PlayLoop("Init");
        }
    }

    //public void ClickSubmit()
    //{
    //    if (!string.IsNullOrEmpty(userName.value) && !string.IsNullOrEmpty(passWord.value))
    //    {
    //        NetworkHandler.instance.Account = userName.value;
    //        NetworkHandler.instance.Password = passWord.value;
    //        NetworkHandler.instance.SendProcess("1001#" + userName.value + ";" + passWord.value + ";");
    //        //AudioEditer.instance.PlayLoop("Scene");
    //    }
    //}

    //public void ClickLogin()
    //{
    //    NetworkHandler.instance.IsAuto = true;
    //    float RandName = Random.Range(1000000, 9000000);
    //    NetworkHandler.instance.Account = "u" + RandName.ToString();
    //    NetworkHandler.instance.Password = "p" + RandName.ToString();
    //    NetworkHandler.instance.SendProcess("1001#" + NetworkHandler.instance.Account + ";" + NetworkHandler.instance.Password + ";");        
    //}

    //public void ClickExit()
    //{
    //    Debug.Log("退出游戏");
    //}
    /*   void Update()
       {
           //if (userName.value != "")
           //{
           //    TweenAlpha ta = userName.transform.FindChild("Label").GetComponent<TweenAlpha>();
           //    ta.enabled = false;
           //    UILabel uil = userName.transform.FindChild("Label").GetComponent<UILabel>();
           //    uil.color = Color.black;
           //}
           //else
           //{
           //    TweenAlpha ta = userName.transform.FindChild("Label").GetComponent<TweenAlpha>();
           //    ta.enabled = true;
           //}
           //if (passWord.value != "")
           //{
           //    TweenAlpha ta = passWord.transform.FindChild("Label").GetComponent<TweenAlpha>();
           //    ta.enabled = false;
           //    UILabel uil = passWord.transform.FindChild("Label").GetComponent<UILabel>();
           //    uil.color = Color.black;
           //}
           //else
           //{
           //    TweenAlpha ta = passWord.transform.FindChild("Label").GetComponent<TweenAlpha>();
           //    ta.enabled = true;
           //}



           if (isOpenDaoDan)
           {
               if (!Daodan.GetComponent<UISpriteAnimation>().isPlaying)
               {
                   BaoZha.SetActive(true);
                   isOpenDaoDan = false;
                   isOpenBaoZha = true;
               }
           }

           if (isOpenBaoZha)
           {
               if (!BaoZha.GetComponent<UISpriteAnimation>().isPlaying)
               {
                   BaoZha.SetActive(false);
                   isOpenBaoZha = false;
                   WangPai.SetActive(true);
                   Man.SetActive(true);
                   HuoXin.SetActive(true);
               }
           }
       }*/

    //void State2()
    //{
    //    DaZhanLue.SetActive(true);

    //    Invoke("State3", 0.5f);
    //}

    //void State3()
    //{
    //    Daodan.SetActive(true);

    //    Invoke("State4", 0.65f);
    //}

    void State4()
    {
        BaoZha.SetActive(true);

        Invoke("State5", 0.6f);
    }

    void State5()
    {
        Man.SetActive(true);
        WangPai.SetActive(true);
        HuoXin.SetActive(true);
        Light.SetActive(true);
        if (!BaoZha.GetComponent<UISpriteAnimation>().isPlaying)
        {
            BaoZha.SetActive(false);
        }

        //Invoke("State6", 0.6f);
    }

    void State7()
    {
        //TouchToStart.SetActive(true);
        if (VersionChecker.instance.IsLogin)
        {
            SpriteBg.SetActive(true);
        }
        else
        {
#if QQ
            ShowQQ();
#endif
        }
        if (PlayerPrefs.GetString("ServerID", "0") == "0")//PlayerPrefs.GetInt("ServerID") == 0
        {
            //ChoseSever.text = "请选择服务器";
            //Zhuangtai.gameObject.SetActive(false);
            //Debug.Log("ServerID " + PlayerPrefs.GetString("ServerID"));
            ServerList ServerItem = null;
            List<ServerList> RandomList = new List<ServerList>();

            Debug.LogError(RandomList.Count);

            foreach (var item in TextTranslator.instance.ServerLists)
            {
                if (item.Type == 2)
                {
                    RandomList.Add(item);
                    //ServerItem = item;
                    //break;
                }
            }

            if (RandomList.Count > 0)
            {
                int ttt = UnityEngine.Random.Range(0, RandomList.Count);
                ServerItem = RandomList[ttt];
            }
            RandomList.Clear();
            if (ServerItem == null)
            {
                foreach (var item in TextTranslator.instance.ServerLists)
                {
                    if (item.Type == 1)
                    {
                        RandomList.Add(item);
                    }
                }
                if (RandomList.Count > 0)
                {
                    ServerItem = RandomList[UnityEngine.Random.Range(0, RandomList.Count)];
                }
                RandomList.Clear();
            }
            if (ServerItem == null)
            {
                foreach (var item in TextTranslator.instance.ServerLists)
                {
                    if (item.Type == 3)
                    {
                        RandomList.Add(item);
                    }
                }
                if (RandomList.Count > 0)
                {
                    ServerItem = RandomList[UnityEngine.Random.Range(0, RandomList.Count)];
                }
                RandomList.Clear();
            }
            RandomList.Clear();
            if (ServerItem == null)
            {
                foreach (var item in TextTranslator.instance.ServerLists)
                {
                    if (item.Type == 4)
                    {
                        RandomList.Add(item);
                    }
                }
                if (RandomList.Count > 0)
                {
                    ServerItem = RandomList[UnityEngine.Random.Range(0, RandomList.Count)];
                }
                RandomList.Clear();
            }
            //打开公告和用户中心按钮           
            this._OneServerItem = ServerItem;
            SetServerState(ServerItem);
            ChoseSever.text = ServerItem.ServerName;
            ServerTag = ServerItem.ServerTag;//PlayerPrefs.GetInt("ServerID");//
            StratButton.SetActive(true);
            //PlayerPrefs.SetString("ServerID", ServerTag);
            UIEventListener.Get(StratButton).onClick = delegate(GameObject go)
            {
                int state = CompareTiemState();
                if (state == 1)
                {
                    PlayerPrefs.SetString("ServerID", ServerTag);
                    PlayerPrefs.SetString("ServerName", ServerItem.ServerName);
                    ObscuredPrefs.SetString("GameHost", ServerItem.ServerIP);
                    ObscuredPrefs.SetString("GamePort", ServerItem.ServerPort.ToString());
                    NetworkHandler.instance.IsPrefetch = false;
                    if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 0)
                    {
                        UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_200);
                    }
                    StartCoroutine(NetworkHandler.instance.LoadScene());
                    ObscuredPrefs.SetString("ObServerName", ServerItem.ServerName);
                }
                else if (state == 0)
                {
                    GameObject parent = GameObject.Find("UIRoot");
                    GameObject obj = NGUITools.AddChild(parent, (GameObject)Resources.Load("Prefab/Effect/LabelEffect"));
                    obj.GetComponent<UILabelEffect>().SetLabelInLoginWindow("服务器开服时间" + _OneServerItem.OpenDate, new Vector3(0, -170, 0), new Vector3(0, -170, 0));
                    obj.AddComponent<DestroySelf>();
                }
                else if (state == 2)
                {
                    GameObject parent = GameObject.Find("UIRoot");
                    GameObject obj = NGUITools.AddChild(parent, (GameObject)Resources.Load("Prefab/Effect/LabelEffect"));
                    obj.GetComponent<UILabelEffect>().SetLabelInLoginWindow("服务器关闭", new Vector3(0, -170, 0), new Vector3(0, -170, 0));
                    obj.AddComponent<DestroySelf>();
                }
            };
        }
        else
        {
            Debug.Log("ServerID " + PlayerPrefs.GetString("ServerID"));

            ServerList ServerItem = TextTranslator.instance.GetServerListsByID(PlayerPrefs.GetString("ServerID"));//TextTranslator.instance.GetServerListsByID(PlayerPrefs.GetInt("ServerID"));

            if (ServerItem == null)
            {
                List<ServerList> RandomList = new List<ServerList>();

                Debug.LogError(RandomList.Count);

                foreach (var item in TextTranslator.instance.ServerLists)
                {
                    if (item.Type == 2)
                    {
                        RandomList.Add(item);
                        //ServerItem = item;
                        //break;
                    }
                }

                if (RandomList.Count > 0)
                {
                    int ttt = UnityEngine.Random.Range(0, RandomList.Count);
                    ServerItem = RandomList[ttt];
                }
                RandomList.Clear();
                if (ServerItem == null)
                {
                    foreach (var item in TextTranslator.instance.ServerLists)
                    {
                        if (item.Type == 1)
                        {
                            RandomList.Add(item);
                        }
                    }
                    if (RandomList.Count > 0)
                    {
                        ServerItem = RandomList[UnityEngine.Random.Range(0, RandomList.Count)];
                    }
                    RandomList.Clear();
                }
                if (ServerItem == null)
                {
                    foreach (var item in TextTranslator.instance.ServerLists)
                    {
                        if (item.Type == 3)
                        {
                            RandomList.Add(item);
                        }
                    }
                    if (RandomList.Count > 0)
                    {
                        ServerItem = RandomList[UnityEngine.Random.Range(0, RandomList.Count)];
                    }
                    RandomList.Clear();
                }
                RandomList.Clear();
                if (ServerItem == null)
                {
                    foreach (var item in TextTranslator.instance.ServerLists)
                    {
                        if (item.Type == 4)
                        {
                            RandomList.Add(item);
                        }
                    }
                    if (RandomList.Count > 0)
                    {
                        ServerItem = RandomList[UnityEngine.Random.Range(0, RandomList.Count)];
                    }
                    RandomList.Clear();
                }
            }

            this._OneServerItem = ServerItem;

            SetServerState(ServerItem);

            //ChoseSever.text = ServerItem.ServerID.ToString() + "区 " + ServerItem.ServerName;
            ChoseSever.text = ServerItem.ServerName;

            ServerTag = ServerItem.ServerTag;//PlayerPrefs.GetInt("ServerID");//

            StratButton.SetActive(true);

            UIEventListener.Get(StratButton).onClick = delegate(GameObject go)
            {
                int state = CompareTiemState();
                if (state == 1)
                {
                    PlayerPrefs.SetString("ServerID", ServerTag);
                    PlayerPrefs.SetString("ServerName", ServerItem.ServerName);
                    ObscuredPrefs.SetString("GameHost", ServerItem.ServerIP);
                    ObscuredPrefs.SetString("GamePort", ServerItem.ServerPort.ToString());
                    NetworkHandler.instance.IsPrefetch = false;
                    if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 0)
                    {
                        UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_200);
                    }
                    StartCoroutine(NetworkHandler.instance.LoadScene());
                    ObscuredPrefs.SetString("ObServerName", ServerItem.ServerName);
                }
                else if (state == 0)
                {
                    GameObject parent = GameObject.Find("UIRoot");
                    GameObject obj = NGUITools.AddChild(parent, (GameObject)Resources.Load("Prefab/Effect/LabelEffect"));
                    obj.GetComponent<UILabelEffect>().SetLabelInLoginWindow("服务器开服时间" + _OneServerItem.OpenDate, new Vector3(0, -170, 0), new Vector3(0, -170, 0));
                    obj.AddComponent<DestroySelf>();
                }
                else if (state == 2)
                {
                    GameObject parent = GameObject.Find("UIRoot");
                    GameObject obj = NGUITools.AddChild(parent, (GameObject)Resources.Load("Prefab/Effect/LabelEffect"));
                    obj.GetComponent<UILabelEffect>().SetLabelInLoginWindow("服务器关闭", new Vector3(0, -170, 0), new Vector3(0, -170, 0));
                    obj.AddComponent<DestroySelf>();
                }
            };
        }
        UIEventListener.Get(ServerButton).onClick = delegate(GameObject go)
        {
            //NetworkHandler.instance.IsPrefetch = false;
            //DaZhanLue.SetActive(false);
            //DaZhanLue02.SetActive(false);
            //LOGO_di2.SetActive(false);
            UIManager.instance.OpenPanel("ServerSelectionWindow", false);
        };
        StartCoroutine(OpenGameAnnoucement());
        //UIManager.instance.OpenPanel("GameAnnouncementWindow", false);
        //GameObject.Find("GameAnnouncementWindow").GetComponent<GameAnnouncementWindow>().SetAnnouncement(TextTranslator.instance.Announcement);
    }

    IEnumerator OpenGameAnnoucement()
    {
        AnnouncementBtn.SetActive(true);
        AdminBtn.SetActive(true);
        if (UIEventListener.Get(AnnouncementBtn).onClick == null)
        {
            UIEventListener.Get(AnnouncementBtn).onClick += delegate(GameObject go)
            {
                DisplayGameAnnoucement();
            };
        }
        if (UIEventListener.Get(AdminBtn).onClick == null)
        {
            UIEventListener.Get(AdminBtn).onClick += delegate(GameObject go)
            {
                
#if HOLA
                HolagamesSDK.getInstance().entryGameCenter();
#else
                GameObject obj = NGUITools.AddChild(GameObject.Find("UIRoot"), effectPrefab);
                obj.GetComponent<UILabelEffect>().SetLabelNormal("用户中心暂未开启!", new Vector3(0, 180, 0), new Vector3(0, 320, 0));
                obj.AddComponent<DestroySelf>();
                //UIManager.instance.OpenPromptWindow("用户中心暂未开启!", PromptWindow.PromptType.Hint, null, null);
#endif
            };
        }


#if QQ || HOLA
        ChangeUser.SetActive(true);
        if (UIEventListener.Get(ChangeUser).onClick == null)
        {
            UIEventListener.Get(ChangeUser).onClick += delegate(GameObject go)
            {
                HolagamesSDK.getInstance().logout();
                ShowQQ();
            };
        }
#endif

        yield return new WaitForSeconds(1.5f);
        DisplayGameAnnoucement();
    }

    public void ShowQQ()
    {
#if QQ
        GameObjectQQ.SetActive(true);
        SpriteBg.SetActive(false);

        UIEventListener.Get(TextureQQ).onClick = delegate(GameObject go)
        {
            HolagamesSDK.getInstance().Login("QQ");
        };

        UIEventListener.Get(TextureWX).onClick = delegate(GameObject go)
        {
            HolagamesSDK.getInstance().Login("WX");
        };
#endif
    }

    public void DisplayGameAnnoucement()
    {
        if (GameObject.Find("ServerSelectionWindow") == null)
        {
            if (GameObject.Find("GameAnnouncementWindow") == null)
            {
                UIManager.instance.OpenSinglePanel("GameAnnouncementWindow", false);
                GameObject.Find("GameAnnouncementWindow").GetComponent<GameAnnouncementWindow>().SetAnnouncement(TextTranslator.instance.Announcement);
            }
        }
    }
    public void SetServerId(string _ServerTag) //int id
    {
        //ChoseSever.text = ServerId.ToString() + "区";
        this.ServerTag = _ServerTag;
        Debug.LogError("ServerID1" + ServerTag);
        ServerList ServerItem = TextTranslator.instance.GetServerListsByID(ServerTag);
        this._OneServerItem = ServerItem;
        SetServerState(ServerItem);

        //ChoseSever.text = ServerItem.ServerID.ToString() + "区 " + ServerItem.ServerName;
        ChoseSever.text = ServerItem.ServerName;

        StratButton.SetActive(true);

        //if (UIEventListener.Get(StratButton).onClick == null)
        {
            UIEventListener.Get(StratButton).onClick = delegate(GameObject go)
            {
                int state = CompareTiemState();
                if (state == 1)
                {
                    PlayerPrefs.SetString("ServerID", ServerTag);
                    PlayerPrefs.SetString("ServerName", ServerItem.ServerName);
                    ObscuredPrefs.SetString("GameHost", ServerItem.ServerIP);
                    ObscuredPrefs.SetString("GamePort", ServerItem.ServerPort.ToString());
                    NetworkHandler.instance.IsPrefetch = false;
                    if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 0)
                    {
                        UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_200);
                    }
                    StartCoroutine(NetworkHandler.instance.LoadScene());
                    ObscuredPrefs.SetString("ObServerName", ServerItem.ServerName);
                }
                else if (state == 0)
                {
                    GameObject parent = GameObject.Find("UIRoot");
                    GameObject obj = NGUITools.AddChild(parent, (GameObject)Resources.Load("Prefab/Effect/LabelEffect"));
                    obj.GetComponent<UILabelEffect>().SetLabelInLoginWindow("服务器开服时间" + _OneServerItem.OpenDate, new Vector3(0, -170, 0), new Vector3(0, -170, 0));
                    obj.AddComponent<DestroySelf>();
                }
                else if (state == 2)
                {
                    GameObject parent = GameObject.Find("UIRoot");
                    GameObject obj = NGUITools.AddChild(parent, (GameObject)Resources.Load("Prefab/Effect/LabelEffect"));
                    obj.GetComponent<UILabelEffect>().SetLabelInLoginWindow("服务器关闭", new Vector3(0, -170, 0), new Vector3(0, -170, 0));
                    obj.AddComponent<DestroySelf>();
                }
            };
        }
    }
    void SetServerState(ServerList ServerItem)
    {
        int _Type = ServerItem.Type;
        DaZhanLue.SetActive(true);
        //if (_Type < 4) 
        //{
        //    Zhuangtai.gameObject.SetActive(true);
        //}
        if (_Type == 1)
        {
            Zhuangtai.spriteName = "Servericon1";

        }
        else if (_Type == 2)
        {
            Zhuangtai.spriteName = "Servericon2";
        }
        else if (_Type == 3)
        {
            Zhuangtai.spriteName = "Servericon3";
        }
        else if (_Type == 4)
        {
            Zhuangtai.spriteName = "Servericon4";
        }
        else if (_Type == 5)
        {
            Zhuangtai.spriteName = "Servericon5";
        }
    }
    public void DaoDanActive()
    {
        Daodan.SetActive(true);
        //isOpenDaoDan = true;
        Invoke("State4", 0.65f);
    }

    private int CompareTiemState() //是否到了开服时间  0-未开服，1-开服中 2-关服；
    {
        int state = 0;
        string timeStr1 = _OneServerItem.OpenDate;
        string timeStr2 = _OneServerItem.CloseDate;
        DateTime timeOpen = Convert.ToDateTime(timeStr1);
        DateTime timeClose = Convert.ToDateTime(timeStr2);
        if (timeOpen > Downloader.instance.NowTime)
        {
            Debug.Log("时间未到");
            Debug.Log("开服时间" + timeOpen);
            Debug.Log("现在时间" + Downloader.instance.NowTime);
            state = 0;
        }
        else if (timeOpen < Downloader.instance.NowTime && timeClose > Downloader.instance.NowTime)
        {
            Debug.Log("时间到了");
            Debug.Log("开服时间" + timeOpen);
            Debug.Log("现在时间" + Downloader.instance.NowTime);
            state = 1;
        }
        else if (timeClose < Downloader.instance.NowTime)
        {
            Debug.Log("时间过了");
            Debug.Log("关服时间" + timeClose);
            Debug.Log("现在时间" + Downloader.instance.NowTime);
            state = 2;
        }
        return state;
    }
}
