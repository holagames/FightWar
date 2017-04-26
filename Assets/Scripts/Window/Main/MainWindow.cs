using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
using System.Text;

public class TeamMumberPosition
{
    internal GameObject _Character;
    internal int _CharacterPosition = 0;
    internal int _CharacterRoleID = 0;
}

public class MainWindow : MonoBehaviour
{
    public GameObject WillOpenTipButton;

    public GameObject ChatButton;
    public GameObject TopChatButton;
    public GameObject ChatButtonSetting;
    public GameObject PrivateChatButton;
    public GameObject Menu;
    public GameObject ActivityButton;
    public GameObject HelpButton;
    public GameObject LeftButton;
    public GameObject ButtonParent;

    public GameObject SpriteArrow;
    bool IsMenuOpen = false;

    public UITexture headIcon;
    public UILabel HoldOnLeftTimeLabel;
    public UILabel LabelExp;
    public UILabel LabelName;
    public UILabel LabelLevel;
    public UILabel LabelVip;
    public UILabel LabelFight;
    public UILabel LabelBlood;
    public UISlider SliderExp;
    public UISlider SliderBlood;
    public GameObject MovePart;
    public Camera RoleCamera;
    public GameObject CloudWindow;
    public GameObject MapWindowObj;

    public GameObject Hero1;
    public GameObject Hero2;
    public GameObject Hero3;
    public GameObject Hero4;
    public GameObject Hero5;
    public GameObject Hero6;
    private Dictionary<int, Hero> ReccordHeroCardIdDic = new Dictionary<int, Hero>();

    public UILabel LabelName1;
    public UILabel LabelName2;
    public UILabel LabelName3;
    public UILabel LabelName4;
    public UILabel LabelName5;
    public UILabel LabelName6;

    public UILabel LabelLevel1;
    public UILabel LabelLevel2;
    public UILabel LabelLevel3;
    public UILabel LabelLevel4;
    public UILabel LabelLevel5;
    public UILabel LabelLevel6;

    public UISprite SpriteJX1;
    public UISprite SpriteJX2;
    public UISprite SpriteJX3;
    public UISprite SpriteJX4;
    public UISprite SpriteJX5;
    public UISprite SpriteJX6;
    public GameObject SpriteJX1Obj;
    public GameObject SpriteJX2Obj;
    public GameObject SpriteJX3Obj;
    public GameObject SpriteJX4Obj;
    public GameObject SpriteJX5Obj;
    public GameObject SpriteJX6Obj;

    public GameObject UiGrid;

    [HideInInspector]
    public List<TeamMumberPosition> mTeamPosition = new List<TeamMumberPosition>();

    public List<GameObject> Prompt = new List<GameObject>();
    public List<GameObject> FunctionLock = new List<GameObject>();

    public List<GameObject> TopButton = new List<GameObject>();
    public List<GameObject> BottomButton = new List<GameObject>();

    public GameObject[] HeroAddClickArr;

    public GameObject QieHuanButton;//切换场景按钮
    public GameObject RoleButton;//英雄
    public GameObject EquipButton;//装备
    public GameObject GachaButton;//招募
    public GameObject BagButton;//背包
    public GameObject StoreButton;//商城
    public GameObject TaskButton;//任务

    public GameObject MailButton;//邮件
    public GameObject FriendButton;//好友
    public GameObject HeroMapButton;//图鉴

    public GameObject HelperButton;//小贴士
    public GameObject HoldOnButton;//外勤收益
    public GameObject QuestionButton;//问卷调查
    public GameObject JunhuokuButton;//超级军火商

    public List<PictureCreater.RolePicture> HeroTeam = new List<PictureCreater.RolePicture>();
    bool isSetHero = false;
    bool isOnPress = false;
    float oldPosition = 0;
    float ClickPosition = 0;
    float newPosition = 0;
    float cutPosition = 0;
    int ShiftPosition = 0;
    public int PositionCount = 0;
    List<Vector3> mVector3List = new List<Vector3>();
    int staminTime = 0;
    int spriteTime = 0;

    public TweenScale KillScale;
    private GameObject zhujiemian1;
    private GameObject zhujiemian2;
    private GameObject zhujiemian3;
    private int ScenceNum = 0;
    bool IsMoveScence = false;

    public GameObject YiWaiHuoDe01;
    public GameObject YiWaiHuoDe02;
    public GameObject SkillEffect;

    public GameObject Scence;
    public GameObject ScenceBeijing;
    public GameObject ScenceBeijing2;


    //提示窗口
    public List<int> RedPointList = new List<int>();
    public GameObject RandomObj;
    public int RandomNum = -1;

    public GameObject OnClickButton;//隐藏按钮，单击按钮启用
    private int StayTime = 60;//隐藏按钮时间
    private int[] Levellimit = new int[] { 0, 20, 30, 40, 50, 60, 70 };

    GameObject HostageObject;
    public GameObject LabelHostage;
    public GameObject LegionWarMessage;
    public TweenScale tweenscale;
    public TweenAlpha tweenalpha;
    public GameObject EffectSprite;
    public GameObject EffectChangJing;


    public TweenScale Guozhantweenscale;
    public TweenAlpha Guozhantweenalpha;
    public GameObject GuozhanEffectObj;

    public GameObject LabelEffectExp;//挂机经验加成
    public GameObject LabelEffectAward;//挂机奖励加成

    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;
    private List<GameObject> AssetBossList = new List<GameObject>();


    private int OnhookItemCode = 0;//挂机奖励
    private int OnhookItemNum = 0;//挂机奖励
    private int OnhookExp = 0;

    public UISprite KongBai;
    public UILabel JunhuoLabel;
    private int powerNum = 0;
    bool isOpenSmall = false;//小目标是否打开
    int smallGoalID = 0;

    void OnEnable()
    {
        AddMainWindowScence();
    }
    void Awake()
    {
        //Debug.LogError("切换图片" + System.Convert.ToSingle(Screen.height) / Screen.width + "          " + System.Convert.ToSingle(600) / 900);
        if (System.Convert.ToSingle(Screen.height) / Screen.width >= System.Convert.ToSingle(600) / 900 - 0.02f && System.Convert.ToSingle(Screen.height) / Screen.width < System.Convert.ToSingle(1536) / 2048)
        {
            KongBai.width = 1225;
        }
        else if (System.Convert.ToSingle(Screen.height) / Screen.width >= System.Convert.ToSingle(1536) / 2048)
        {
            KongBai.width = 1422;
        }
        else
        {
            //Debug.LogError("ss " + (System.Convert.ToSingle(Screen.width) / Screen.height) + "   " + (System.Convert.ToSingle(960) / 640));
            KongBai.width = (int)(1200 * (System.Convert.ToSingle(Screen.width) / Screen.height) / (System.Convert.ToSingle(960) / 640));
        }
        //Debug.LogError("图片的长度：" + KongBai.width + "界面 " + Screen.width);
    }

    /// <summary>
    /// 获取Android手机电量（百分比）
    /// </summary>
    /// <returns></returns>
    int GetBatteryLevel()
    {
        try
        {
            string CapacityString = System.IO.File.ReadAllText("/sys/class/power_supply/battery/capacity");
            return int.Parse(CapacityString);
        }
        catch (System.Exception e)
        {
            Debug.Log("Failed to read battery power; " + e.Message);
        }
        return -1;
    }
    /// <summary>
    /// 获取手机电量
    /// </summary>
    void GetPowerNum()
    {
        powerNum = GetBatteryLevel();
        if (PlayerPrefs.GetFloat("ElectractySlider", 1) >= 1)
        {
            if (powerNum <= 40 && CharacterRecorder.instance.IsOpenPowerMode && GameObject.Find("SettingWindow") == null && GameObject.Find("PromptWindow") == null)
            {
                UIManager.instance.OpenPromptWindow("电量低于40%, 是否开启省电模式.", PromptWindow.PromptType.Confirm, OpenPowerSavingMode, OpenCancelMode);
            }
        }

    }
    /// <summary>
    /// 开启省电模式面板
    /// </summary>
    void OpenPowerSavingMode()
    {
        UIManager.instance.OpenPanel("SettingWindow", false);
    }
    /// <summary>
    /// 取消开启省电模式面板
    /// </summary>
    void OpenCancelMode()
    {
        CharacterRecorder.instance.IsOpenPowerMode = false;
    }
    void Start()
    {
#if UNITY_ANDROID
        //GetPowerNum();
#endif
        RenderSettings.fog = false;
        CharacterRecorder.instance.TabeID = 0;
        GameObject.Find("MainCamera").GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
        PictureCreater.instance.FightStyle = 0;

        SceneTransformer.instance.ShowMainScene(false);

        //if (GameObject.Find("MapObject") == null)  //kino
        //{
        //    SceneTransformer.instance.ShowMainScene(false);
        //}
        //else
        //{
        //    //GameObject obj = GameObject.Find("MapObject/MapCon"); //kino
        //    //if (obj.GetComponent<MapWindow>().ReceiveNum > 0)
        //    //{
        //    //    //世界地图有可领取宝箱
        //    //    this.transform.Find("ButtonParent/Sprite/WorldButton/SpriteRedPoint").gameObject.SetActive(true);
        //    //}
        //    //else
        //    //{
        //    //    this.transform.Find("ButtonParent/Sprite/WorldButton/SpriteRedPoint").gameObject.SetActive(false);
        //    //}
        //    SceneTransformer.instance.ShowMainScene(false);
        //}

        //强制新手引导过后弹出明日登陆界面只显示一次
        if (CharacterRecorder.instance.GuideID[26] == 7 || CharacterRecorder.instance.GuideID[28] == 6)
        {
            //CharacterRecorder.instance.GuideID[26] += 1;
            StartCoroutine(SceneTransformer.instance.NewbieGuide());
        }
        ////十连抽后弹出四选一活动界面
        //if ((CharacterRecorder.instance.TenCaChaNumber == 1 && CharacterRecorder.instance.GuideID[55] == 0))//CharacterRecorder.instance.ActivityRewardIsGet == false
        //{
        //    ShowPrompt(7, true);
        //    CharacterRecorder.instance.GuideID[55] = 99;
        //    SceneTransformer.instance.SendGuide();
        //    UIManager.instance.OpenSinglePanel("AnnouncementWindow", false);
        //    GameObject.Find("AnnouncementWindow").GetComponent<AnnouncementWindow>().SetTexture("TenGacha");
        //}
        //else if (CharacterRecorder.instance.isPowerRedPoint ||CharacterRecorder.instance.isFoundationPoint ||CharacterRecorder.instance.isBenifPoint || (CharacterRecorder.instance.TenCaChaNumber == 1 && CharacterRecorder.instance.ActivityRewardIsGet == false))
        //{
        //    ShowPrompt(7, true);
        //}
        //else
        //{
        //    ShowPrompt(7, false);
        //}


        //删除战斗界面
        if (GameObject.Find("FightScene") != null)
        {
            DestroyImmediate(GameObject.Find("FightScene"));
        }
        //HeroMapRedPoint();
        //StartCoroutine(DelayShowRoleRedPoint());//角色和装备 小红点
        //SetSignRedPoint();//签到小红点
        //MailRedPont();//邮件小红点
        //SetLoginSignRedPoint(); //登录签到Seven小红点
        //SetSevenDayRedPoint();//七日活动（大放送）红点//SetSevenDayRedPoint();
        //SetFriendRedPoint();//好友小红点
        //SetLabRedPoint();//实验室红点
        SetNewChatButtonInfo();//新聊天按钮信息
        SetChatSpriteColor();//聊天按钮颜色
        //ChallengeRedPont();//冲突小红点
        //Collision();//冲突红点
        //GachaRedpoint();//抽卡小红点
        //TaskRedPoint();//任务小红点
        //TechTreeRedpoint();//情报小红点
        //FirstVipRedpoint();//首冲红点
        OpenResourcetycoon();//资源大亨
        WorldBossIsOpen();//boss开启判断
        FirstRechargeWindowIsOpen();//首冲开启判断
        //QuestionWindowIsOpen();////问卷调查开启判断
        ShopMallRedPoint();
        RedHeroIsOpen();
        ControlAllButtonOpen();//控制开启
        SureJunhuokuIsOpen();//军火商
        //ConquerWindowRedPoint();//征服红点
        if (!CharacterRecorder.instance.isFirstMainWindow)
        {
            CharacterRecorder.instance.CheckRedPoint();
            ResetAllRedPoint();//所有红点刷新
        }
        else
        {
            CharacterRecorder.instance.isFirstMainWindow = false;
        }

        LegionWarInfo();//军团战集火
        SetPrivateChatButtonIsOpen();//私聊
        HelperButtonIsOpenNew();//我要变强
        //if (UIEventListener.Get(GameObject.Find("SpriteList")).onClick == null) //kino
        //{
        //    UIEventListener.Get(GameObject.Find("SpriteList")).onClick += delegate(GameObject go)
        //    {
        //        if (IsMenuOpen)
        //        {
        //            IsMenuOpen = !IsMenuOpen;
        //            Menu.GetComponent<Animator>().Play("CloseMenu");
        //            ActivityButton.GetComponent<Animator>().Play("CloseActivityButton");
        //            HelpButton.GetComponent<Animator>().Play("CloseHelpButton");
        //            LeftButton.GetComponent<Animator>().Play("CLoseLeftButton");
        //            //ButtonParent.GetComponent<Animator>().Play("CloseButtonParent");
        //            ChatButton.SetActive(true);
        //            TopChatButton.SetActive(true);
        //            SpriteArrow.transform.Rotate(0, 0, 180);
        //            //StayTime = 60;
        //            //CancelInvoke("StayMainWindow");
        //            //InvokeRepeating("StayMainWindow", 0, 1.0f);
        //        }
        //        else
        //        {
        //            IsMenuOpen = !IsMenuOpen;
        //            Menu.GetComponent<Animator>().Play("OpenMenu");

        //            ActivityButton.GetComponent<Animator>().Play("OpenActivityButton");
        //            HelpButton.GetComponent<Animator>().Play("OpenHelpButton");
        //            LeftButton.GetComponent<Animator>().Play("OpenLeftButton");
        //            //ButtonParent.GetComponent<Animator>().Play("OpenButtonParent");
        //            ChatButton.SetActive(false);
        //            TopChatButton.SetActive(false);
        //            SpriteArrow.transform.Rotate(0, 0, 180);
        //            //CancelInvoke("StayMainWindow");
        //            //OnClickOpenMianWindow();
        //        }
        //    };
        //}
        FunctionUnlock();
        UIEventListener.Get(GameObject.Find("WorldButton")).onClick += delegate(GameObject go)
        {
            if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) < 18)
            {
                if ((PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 3 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 3)
                    || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 4 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 9)
                    || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 5 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 0)
                    || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 6 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 0)
                    || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 10 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 0)
                    || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 10 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 1)
                    || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 12 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 0)
                    || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 16 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 0)
                    || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 16 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 1)
                    || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 17 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 0)
                    )
                {
                    SceneTransformer.instance.NewGuideButtonClick();
                }
            }
            if (CharacterRecorder.instance.GuideID[26] == 7)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
            GameObject obj = Instantiate(CloudWindow) as GameObject;
            if (GameObject.Find("UIRoot") != null)
            {
                obj.transform.parent = GameObject.Find("UIRoot").transform;
                obj.transform.localScale = Vector3.one;
                obj.transform.localPosition = Vector3.zero;
                obj.name = "CloudWindow";
            }
            Invoke("OpenMapWindow", 0.5f);
            AudioEditer.instance.PlayOneShot("Jet");
        };
        UIEventListener.Get(BottomButton[7]).onClick += delegate(GameObject go)
        {
            if (GameObject.Find("Lock8"))
            {
                UIManager.instance.OpenPromptWindow("长官,25级开启", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                if (CharacterRecorder.instance.GuideID[38] == 10 || CharacterRecorder.instance.GuideID[13] == 5 || CharacterRecorder.instance.GuideID[14] == 5
                    || CharacterRecorder.instance.GuideID[12] == 5 || CharacterRecorder.instance.GuideID[10] == 5 || CharacterRecorder.instance.GuideID[9] == 5
                    || CharacterRecorder.instance.GuideID[21] == 5 || CharacterRecorder.instance.GuideID[58] == 2)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
                if (RandomObj != null)
                {
                    if (RandomObj.name == "ChallengeButton")
                    {//是从神秘商店进入冲突里面
                        bool isNewGuide = false;
                        foreach (int id in CharacterRecorder.instance.GuideID)
                        {
                            if (id != 0 && id != 99)
                            {
                                isNewGuide = true;
                            }
                        }
                        if (isNewGuide == false)
                        {
                            CharacterRecorder.instance.RandomMysteryNumber = 0;//0代表冲突界面
                        }
                    }
                }
                UIManager.instance.OpenPanel("ChallengeWindow", true);
            }
        };

        UIEventListener.Get(BottomButton[1]).onClick += delegate(GameObject go)  //英雄榜
        {
            int OpenGate = TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.heroRank).Level;
            if (CharacterRecorder.instance.lastGateID <= OpenGate)
            {
                UIManager.instance.OpenPromptWindow("长官,通关" + (OpenGate % 10000) + "开启", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                UIManager.instance.OpenPanel("RankListWindow", true);
            }
        };

        UIEventListener.Get(BottomButton[8]).onClick += delegate(GameObject go)  //芯片
        {
            UIManager.instance.OpenPanel("ChipWindow", true);
            CharacterRecorder.instance.SBlaoxu = true;
        };


        UIEventListener.Get(BottomButton[2]).onClick += delegate(GameObject go)
        {

            int OpenGate = TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.shop).Level;
            if (CharacterRecorder.instance.lastGateID <= OpenGate)
            {
                UIManager.instance.OpenPromptWindow("长官,通关" + (OpenGate % 10000) + "开启", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                UIManager.instance.OpenPanel("RankShopWindow", true);
            }
        };

        UIEventListener.Get(BottomButton[0]).onClick += delegate(GameObject go)
        {

            if (CharacterRecorder.instance.lastGateID <= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.juntuan).Level) //(CharacterRecorder.instance.level < 25)
            {
                UIManager.instance.OpenPromptWindow(string.Format("长官,通关{0}开启", TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.juntuan).Level % 10000), PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                //UIManager.instance.OpenPanel("LegionWindow", false);
                if (CharacterRecorder.instance.legionID == 0)
                {
                    UIManager.instance.OpenPanel("LegionMainNoneWindow", true);
                }
                else
                {
                    UIManager.instance.OpenPanel("LegionMainHaveWindow", true);
                }
            }
        };


        UIEventListener.Get(GachaButton).onClick += delegate(GameObject go)
        {
            if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) < 18)
            {
                if ((PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 2 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 4) ||
                    (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 4 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 4))
                {
                    SceneTransformer.instance.NewGuideButtonClick();
                }
            }
            UIManager.instance.OpenPanel("GachaWindow", true);
        };

        UIEventListener.Get(StoreButton).onClick += delegate(GameObject go)
        {

            UIManager.instance.OpenPanel("DiamondShopWindow", true);
        };

        UIEventListener.Get(EquipButton).onClick += delegate(GameObject go)
        {
            //UIManager.instance.OpenPromptWindow("暂未开启", PromptWindow.PromptType.Hint, null, null);           
            CharacterRecorder.instance.isNeedRecordStrengTabType = false;
            UIManager.instance.OpenPanel("StrengEquipWindow", true);
            if (CharacterRecorder.instance.GuideID[34] == 2 || CharacterRecorder.instance.GuideID[28] == 2 || CharacterRecorder.instance.GuideID[38] == 5)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
        };

        UIEventListener.Get(BottomButton[3]).onClick += delegate(GameObject go)
        {
            int OpenGate = TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.shiyanshi).Level;
            if (CharacterRecorder.instance.lastGateID <= OpenGate)
            {
                UIManager.instance.OpenPromptWindow("长官,通关" + (OpenGate % 10000) + "开启", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                UIManager.instance.OpenPanel("LabWindow", true);
            }

        };
        UIEventListener.Get(BottomButton[4]).onClick += delegate(GameObject go)
        {

            int OpenGate = TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.qingbao).Level;
            if (CharacterRecorder.instance.lastGateID <= OpenGate)
            {
                UIManager.instance.OpenPromptWindow("长官,通关" + (OpenGate % 10000) + "开启", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                if (CharacterRecorder.instance.GuideID[11] == 5)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
                UIManager.instance.OpenPanel("TechWindow", true);
            }
        };

        UIEventListener.Get(BottomButton[5]).onClick += delegate(GameObject go)
        {
            int OpenGate = TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zhanshu).Level;
            if (CharacterRecorder.instance.lastGateID <= OpenGate)
            {
                UIManager.instance.OpenPromptWindow("长官,通关" + (OpenGate % 10000) + "开启", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                UIManager.instance.OpenPanel("TacticsWindow", true);
            }

        };

        UIEventListener.Get(BottomButton[6]).onClick += delegate(GameObject go)
        {
            int OpenGate = TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.guozhan).Level;
            if (CharacterRecorder.instance.lastGateID <= OpenGate)
            {
                UIManager.instance.OpenPromptWindow("长官,通关" + (OpenGate % 10000) + "开启", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                UIManager.instance.OpenPanel("LegionWarWindow", true);
            }
        };
        UIEventListener.Get(HeroMapButton).onClick += delegate(GameObject go)
        {
            UIManager.instance.OpenPanel("HeroMapWindow", true);
        };

        UIEventListener.Get(ChatButton).onClick += delegate(GameObject go)
        {
            //UIManager.instance.OpenPanel("ChatWindowNew", false);
            UIManager.instance.OpenSinglePanel("ChatWindowNew", false);
            //UIRoot root = GameObject.FindObjectOfType<UIRoot>();
            //if (root != null)
            //{
            //    float s = (float)root.activeHeight / Screen.height;
            //    int width = Mathf.CeilToInt(Screen.width * s);
            //    if (GameObject.Find("ChatWindowNew") != null)
            //    {
            //        GameObject.Find("ChatWindowNew/Content").transform.localPosition = new Vector3(1200 - width / 2f - 320, 0);
            //    }
            //}

        };

        UIEventListener.Get(TopChatButton).onClick += delegate(GameObject go)
        {
            //UIManager.instance.OpenPanel("ChatWindowNew", false);
            UIManager.instance.OpenSinglePanel("ChatWindowNew", false);
            UIRoot root = GameObject.FindObjectOfType<UIRoot>();
            if (root != null)
            {
                float s = (float)root.activeHeight / Screen.height;
                int width = Mathf.CeilToInt(Screen.width * s);
                if (GameObject.Find("ChatWindowNew") != null)
                {
                    GameObject.Find("ChatWindowNew/Content").transform.localPosition = new Vector3(1200 - width / 2f - 320, 0);
                }
            }
        };

        UIEventListener.Get(TopChatButton.transform.Find("ScrollView/NameLabel").gameObject).onClick += delegate(GameObject go)
        {
            //UIManager.instance.OpenPanel("ChatWindowNew", false);
            UIManager.instance.OpenSinglePanel("ChatWindowNew", false);
            UIRoot root = GameObject.FindObjectOfType<UIRoot>();
            if (root != null)
            {
                float s = (float)root.activeHeight / Screen.height;
                int width = Mathf.CeilToInt(Screen.width * s);
                if (GameObject.Find("ChatWindowNew") != null)
                {
                    GameObject.Find("ChatWindowNew/Content").transform.localPosition = new Vector3(1200 - width / 2f - 320, 0);
                }
            }
        };



        UIEventListener.Get(ChatButtonSetting).onClick += delegate(GameObject go)
        {
            UIManager.instance.OpenSinglePanel("ChatChanelChoseWindow", false);
        };
        UIEventListener.Get(TaskButton).onClick += delegate(GameObject go)
        {
            SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            UIManager.instance.OpenPanel("TaskWindow", true);          
        };

        UIEventListener.Get(TopButton[0]).onClick += delegate(GameObject go) //世界boss
        {
            UIManager.instance.OpenPanel("WorldBossWindow", true);
        };

        UIEventListener.Get(TopButton[1]).onClick += delegate(GameObject go)//大放送
        {
            UIManager.instance.OpenSinglePanel("OpenServiceWindow", false);
            RandomListShow();
        };
        UIEventListener.Get(TopButton[2]).onClick += delegate(GameObject go)//战力排行
        {
            UIManager.instance.OpenPanel("ActiveAwardWindow", false);
        };
        UIEventListener.Get(TopButton[3]).onClick += delegate(GameObject go)//登陆签到
        {
            UIManager.instance.OpenSinglePanel("LoginSignWindow", false);
            RandomListShow();
        };
        UIEventListener.Get(TopButton[4]).onClick += delegate(GameObject go)//首冲
        {
            UIManager.instance.OpenSinglePanel("FirstRechargeWindow", false);
        };
        UIEventListener.Get(TopButton[5]).onClick += delegate(GameObject go)//充值
        {
            UIManager.instance.OpenPanel("VIPShopWindow", true);
        };
        UIEventListener.Get(TopButton[6]).onClick = delegate(GameObject go)//签到w
        {
            RandomListShow();
            UIManager.instance.OpenPanel("SignWindow", false);
        };
        UIEventListener.Get(TopButton[8]).onClick = delegate(GameObject go)//砸金蛋按钮
        {
            UIManager.instance.OpenSinglePanel("GoldenEggActivity", false);
        };
        UIEventListener.Get(TopButton[9]).onClick = delegate(GameObject go)//限时活动
        {
            UIManager.instance.OpenSinglePanel("ActivityGachaRedHeroWindow", false);
            NetworkHandler.instance.SendProcess("5204#;");
            NetworkHandler.instance.SendProcess("9711#;");
            NetworkHandler.instance.SendProcess("9712#;");
        };
        UIEventListener.Get(TopButton[10]).onClick = delegate(GameObject go)//许愿活动
        {
            UIManager.instance.OpenSinglePanel("MyWishWindow", false);
        };

        UIEventListener.Get(TopButton[11]).onClick = delegate(GameObject go)//四选一
        {
            ResourceLoader.instance.OpenActivity(1);
        };

        UIEventListener.Get(TopButton[12]).onClick = delegate(GameObject go)//累计福利
        {
            //ResourceLoader.instance.OpenActivity(130001);
            UIManager.instance.OpenSinglePanel("VipRechargeWindow", false);
        };

        UIEventListener.Get(TopButton[13]).onClick = delegate(GameObject go)//资源大亨
        {
            UIManager.instance.OpenSinglePanel("ResourcetycoonWindow", false);
        };

        UIEventListener.Get(QuestionButton).onClick += delegate(GameObject go)
        {
            UIManager.instance.OpenSinglePanel("QuestionWindow", false);
            RandomListShow();
        };
        UIEventListener.Get(RoleButton).onClick += delegate(GameObject go)
        {
            if (SceneTransformer.instance.CheckGuideIsFinish() == false)
            {
                if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 9 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 2)
                {
                    SceneTransformer.instance.NewGuideButtonClick();
                }
            }
            else
            {
                if (CharacterRecorder.instance.GuideID[26] == 2 ||
                     CharacterRecorder.instance.GuideID[37] == 2 || CharacterRecorder.instance.GuideID[30] == 5
                      || CharacterRecorder.instance.GuideID[31] == 5)
                {
                    RoleWindow.targetRoleCardId = 60016;
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
                else
                {
                    RoleWindow.targetRoleCardId = 0;
                }

            }

            CharacterRecorder.instance.enterRoleFromMain = true;
            UIManager.instance.OpenPanel("RoleWindow", true);

        };
        UIEventListener.Get(GameObject.Find("HeadInfoButton")).onClick += delegate(GameObject go)
        {
            UIManager.instance.OpenPanel("InfoWindow", false);
        };
        UIEventListener.Get(MailButton).onClick += delegate(GameObject go)
        {
            CharacterRecorder.instance.MailButtonOnClick = true;
            NetworkHandler.instance.SendProcess("9001#;");
            RandomListShow();
            //UIManager.instance.OpenPanel("MailWindow", false);
        };
        UIEventListener.Get(HelperButton).onClick += delegate(GameObject go)
        {
            //UIManager.instance.OpenPanel("LittleHelperWindow", true);
            UIManager.instance.OpenSinglePanel("LittleHelperWindow", false);
        };
        UIEventListener.Get(BagButton).onClick += delegate(GameObject go)
        {
            if (SceneTransformer.instance.CheckGuideIsFinish() == false)
            {
                if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 15 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 2)
                {
                    SceneTransformer.instance.NewGuideButtonClick();
                }
            }
            UIManager.instance.OpenPanel("BagWindow", true);
        };
        UIEventListener.Get(FriendButton).onClick += delegate(GameObject go)
        {
            UIManager.instance.OpenPanel("FriendWindow", true);
        };

        UIEventListener.Get(LabelVip.gameObject).onClick += delegate(GameObject go)
        {
            UIManager.instance.OpenPanel("VIPShopWindow", true);
        };

        UIEventListener.Get(HoldOnButton).onClick += delegate(GameObject go)
        {
            //UIManager.instance.OpenSinglePanel("HoldOnAwardWindow", false);
            NetworkHandler.instance.SendProcess("1903#;");
        };

        UIEventListener.Get(JunhuokuButton).onClick += delegate(GameObject go)
        {
            UIManager.instance.OpenSinglePanel("SuperArmsStoreWindow", false);
        };

        //UIEventListener.Get(WillOpenTipButton).onClick += delegate(GameObject go)
        //{
        //    if (WillOpenTipButton.transform.Find("SpriteSmallGoalTop").gameObject.activeSelf) 
        //    {

        //    }
        //    else if (GameObject.Find("GanTanHao_02(Clone)") != null)
        //    {

        //        //GotoButtonClick(RandomNum);
        //    }
        //    else
        //    {
        //        UIManager.instance.OpenPanel("WillOpenWindow", false);
        //    }
        //};

        UIEventListener.Get(QieHuanButton).onClick = delegate(GameObject go)
        {
            if (PlayerPrefs.GetInt("MainScenceNum") < 10)
            {
                EffectChangJing.SetActive(false);
                EffectChangJing.SetActive(true);
                Invoke("QieHuanMainScene", 0.5f);
                //QieHuanMainScene();
            }
        };

        UIEventListener.Get(PrivateChatButton).onClick = delegate(GameObject go)
        {
            UIManager.instance.OpenPanel("PrivateChatWindow", false);
            GameObject.Find("PrivateChatWindow").GetComponent<PrivateChatWindow>().GetCharacterNumber();
        };
        AudioEditer.instance.PlayLoop("Scene");

        ActivityIsOpen();//活动是否结束;
        LoginSignIsOpen();//活动是否结束;
        Reset();
        ChoseWillOpenTipButton();//小目标，红点提示，关卡开放
        //NetworkHandler.instance.SendProcess("6006#1;");
        if ((CharacterRecorder.instance.GuideID[32] < 6 && CharacterRecorder.instance.GuideID[10] > 15 && TextTranslator.instance.GetItemByID(51030) != null))
        {
            StartCoroutine(SceneTransformer.instance.NewbieGuide());
        }
        if (SceneTransformer.instance.CheckGuideIsFinish())
        {
            ClickHeroToEnterRolewindow();
        }

        LevelExp();

        //if (CharacterRecorder.instance.HoldOnLeftTime == 0)

        //{
        //    HoldOnLeftTimeLabel.text = "";
        //}
        //else
        //{
        //    InvokeRepeating("UpdateHoldOnTime", 0, 1.0f);
        //}
        //CancelInvoke("StayMainWindow");
        //InvokeRepeating("StayMainWindow", 0, 1.0f);
        HoldOnLeftTimeLabel.text = CharacterRecorder.instance.HoldKillNum.ToString();
        //KillScale = HoldOnLeftTimeLabel.GetComponent<TweenScale>(); 
        //KillScale.from = new Vector3(1f, 1f, 1f); //kino
        //KillScale.to = new Vector3(1.5f, 1.5f, 1.5f);
        //KillScale.ResetToBeginning();
        //KillScale.PlayForward();
        StartCoroutine(StopAllEffectOnLoadingWindow());
        SetFirstButtonEffect();//首冲特效
        SetGuozhanEffect();//国战动画
        //ResetAllRedPoint();//所有红点刷新
        SetGuozhanIsOpen();
        //打开金蛋活动
        OpenGoldEgg(CharacterRecorder.instance.IsGoldOpen, CharacterRecorder.instance.IsGoldEggRedPoint);
        OpenWishActivity(CharacterRecorder.instance.IsWishOpen, CharacterRecorder.instance.IsWishRedpoint);

        if (SystemInfo.systemMemorySize < 700)
        {
            if (TextTranslator.instance.GUIReloadCount > 10)
            {
                TextTranslator.instance.GUIReloadCount = 1;
                TextTranslator.instance.DoGC();
            }
        }
        else if (SystemInfo.systemMemorySize < 1000)
        {
            if (TextTranslator.instance.GUIReloadCount > 30)
            {
                TextTranslator.instance.GUIReloadCount = 1;
                TextTranslator.instance.DoGC();
            }
        }
    }
    /// <summary>
    /// 是否开启砸金蛋活动
    /// </summary>
    /// <param name="isOpen"></param>
    public void OpenGoldEgg(bool isOpen, bool red)
    {
        if (isOpen)
        {
            TopButton[8].SetActive(true);
            if (red)
            {
                TopButton[8].transform.Find("RedPointEgg").gameObject.SetActive(true);
            }
            else
            {
                TopButton[8].transform.Find("RedPointEgg").gameObject.SetActive(false);
            }
        }
        else
        {
            TopButton[8].SetActive(false);
        }
    }

    /// <summary>
    /// 是否开启资源大亨活动
    /// </summary>
    /// <param name="isOpen"></param>
    public void OpenResourcetycoon()
    {
        if (PlayerPrefs.GetInt("IsOpenResourcetycoon")==1)
        {
            TopButton[13].SetActive(true);
        }
        else
        {
            TopButton[13].SetActive(false);
        }
    }


    /// <summary>
    /// 是否开启许愿活动
    /// </summary>
    /// <param name="isOpen">true 开启，否则关闭</param>
    public void OpenWishActivity(bool isOpen, bool red)
    {
        if (isOpen)
        {
            TopButton[10].SetActive(true);
            if (red)
            {
                TopButton[10].transform.Find("RedPointWish").gameObject.SetActive(true);
            }
            else
            {
                TopButton[10].transform.Find("RedPointWish").gameObject.SetActive(false);
            }
        }
        else
        {
            TopButton[10].SetActive(false);
        }
    }
    public void UpRedPointFrist()
    {
        CharacterRecorder.instance.CheckRedPoint();
        ResetAllRedPoint();//所有红点刷新
    }
    void StayMainWindow() //15秒倒计时隐藏窗口
    {
        Debug.Log("-------" + StayTime);
        if (StayTime == 0)
        {
            bool isOpenButton = true;
            if ((GameObject.Find("BigGuide") != null || GameObject.Find("MinGuide") != null || GameObject.Find("MaxWindow") != null || GameObject.Find("GuideArrow") != null || GameObject.Find("SmallGuide") != null || GameObject.Find("CommonGuide") != null || GameObject.Find("TalkGuide") != null || GameObject.Find("MoveGuide") != null) && SceneTransformer.instance.isNewGuide == false)
            {
                isOpenButton = false;
            }

            if (isOpenButton)
            {
                IsMenuOpen = !IsMenuOpen;
                Menu.GetComponent<Animator>().Play("OpenMenu");

                ActivityButton.GetComponent<Animator>().Play("OpenActivityButton");
                HelpButton.GetComponent<Animator>().Play("OpenHelpButton");
                LeftButton.GetComponent<Animator>().Play("OpenLeftButton");
                ButtonParent.GetComponent<Animator>().Play("OpenButtonParent");
                ChatButton.SetActive(false);
                TopChatButton.SetActive(false);
                SpriteArrow.transform.Rotate(0, 0, 180);
                CancelInvoke("StayMainWindow");
                OnClickOpenMianWindow();
            }
            else
            {
                StayTime = 60;
            }
        }
        else
        {
            StayTime--;
        }
    }

    void OnClickOpenMianWindow()//单机屏幕打开界面
    {
        OnClickButton.SetActive(true);
        if (UIEventListener.Get(OnClickButton).onClick == null)
        {
            UIEventListener.Get(OnClickButton).onClick += delegate(GameObject go)
            {
                if (IsMenuOpen)
                {
                    IsMenuOpen = !IsMenuOpen;
                    Menu.GetComponent<Animator>().Play("CloseMenu");
                    ActivityButton.GetComponent<Animator>().Play("CloseActivityButton");
                    HelpButton.GetComponent<Animator>().Play("CloseHelpButton");
                    LeftButton.GetComponent<Animator>().Play("CLoseLeftButton");
                    ButtonParent.GetComponent<Animator>().Play("CloseButtonParent");
                    ChatButton.SetActive(true);
                    TopChatButton.SetActive(true);
                    SpriteArrow.transform.Rotate(0, 0, 180);
                    StayTime = 60;
                    CancelInvoke("StayMainWindow");
                    InvokeRepeating("StayMainWindow", 0, 1.0f);
                }
                OnClickButton.SetActive(false);
            };
        }
    }

    IEnumerator StopAllEffectOnLoadingWindow() //loading加载是清除主界面特效
    {
        if (GameObject.Find("LoadingWindow") != null)
        {
            GameObject.Find("TopContent").transform.Find("MoneyButton").transform.Find("Icon").transform.Find("WF_ui_star_icon_jin").gameObject.SetActive(false);
            GameObject.Find("TopContent").transform.Find("DiamondButton").transform.Find("Icon").transform.Find("WF_ui_star_icon_lan").gameObject.SetActive(false);
            yield return new WaitForSeconds(10f);
            GameObject.Find("TopContent").transform.Find("MoneyButton").transform.Find("Icon").transform.Find("WF_ui_star_icon_jin").gameObject.SetActive(true);
            GameObject.Find("TopContent").transform.Find("DiamondButton").transform.Find("Icon").transform.Find("WF_ui_star_icon_lan").gameObject.SetActive(true);
        }
    }
    private void AddMainWindowScence() //加载主界面场景
    {
        StopCoroutine(SetScenceTomove());
        StopCoroutine(SetHarborScenceTomove());
        StopCoroutine(CloneBossTomove());
        PictureCreater.instance.DestroyAllComponent();//防止重新连接时原来界面有英雄存在
        if (UIRootExtend.instance.isUiRootRatio != 1f)
        {
            RoleCamera.fieldOfView = 29f * UIRootExtend.instance.isUiRootRatio;
        }
        else
        {
            RoleCamera.fieldOfView = 29f;
        }
        if (CharacterRecorder.instance.level < 25)
        {
            PlayerPrefs.SetInt("MainScenceNum", 1);
        }

        int rangNum = PlayerPrefs.GetInt("MainScenceNum");
        while (this.gameObject.transform.Find("CloneBossName") != null)
        {
            DestroyImmediate(this.gameObject.transform.Find("CloneBossName").gameObject);
        }
        while (Scence.transform.Find("Zhujiemian1") != null) //防止断网有之前的场景
        {
            DestroyImmediate(Scence.transform.Find("Zhujiemian1").gameObject);
        }
        while (Scence.transform.Find("Zhujiemian2") != null)
        {
            DestroyImmediate(Scence.transform.Find("Zhujiemian2").gameObject);
        }
        while (Scence.transform.Find("Zhujiemian3") != null)
        {
            DestroyImmediate(Scence.transform.Find("Zhujiemian3").gameObject);
        }
        //(CharacterRecorder.instance.level >= 32 && CharacterRecorder.instance.level < 38) || (CharacterRecorder.instance.level >= 58 && CharacterRecorder.instance.level < 60)
        //if (rangNum == 1 || rangNum == 6)//沙漠
        //{
        //    ScenceBeijing.SetActive(false);
        //    ScenceBeijing2.SetActive(false);
        //    zhujiemian1 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_desert")) as GameObject;
        //    zhujiemian1.transform.parent = Scence.transform;
        //    zhujiemian1.transform.localPosition = new Vector3(-10470f, 0, 0);
        //    zhujiemian1.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
        //    zhujiemian1.name = "Zhujiemian1";

        //    zhujiemian2 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_desert")) as GameObject;
        //    zhujiemian2.transform.parent = Scence.transform;
        //    zhujiemian2.transform.localPosition = new Vector3(0, 0, 0);
        //    zhujiemian2.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
        //    zhujiemian2.name = "Zhujiemian2";

        //    zhujiemian3 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_desert")) as GameObject;
        //    zhujiemian3.transform.parent = Scence.transform;
        //    zhujiemian3.transform.localPosition = new Vector3(10470f, 0, 0);
        //    zhujiemian3.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
        //    zhujiemian3.name = "Zhujiemian3";

        //    RenderSettings.fog = false;
        //    LightMapAsset lightAsset = Resources.Load("LightMapAsset/lightMapAsset_127") as LightMapAsset;
        //    int count = lightAsset.lightmapFar.Length;
        //    LightmapData[] lightmapDatas = new LightmapData[count];
        //    for (int i = 0; i < count; i++)
        //    {
        //        LightmapData lightmapData = new LightmapData();
        //        lightmapData.lightmapFar = lightAsset.lightmapFar[i];
        //        lightmapData.lightmapNear = lightAsset.lightmapNear[i];
        //        lightmapDatas[i] = lightmapData;
        //    }
        //    LightmapSettings.lightmaps = lightmapDatas;
        //}
        //else if (rangNum == 2 || rangNum == 7)//海港
        //{
        //    Scence.transform.localPosition = new Vector3(0, 10260, 97420);
        //    ScenceBeijing.SetActive(false);
        //    ScenceBeijing2.SetActive(false);
        //    zhujiemian1 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_harbor")) as GameObject;
        //    zhujiemian1.transform.parent = Scence.transform;
        //    zhujiemian1.transform.localPosition = new Vector3(0, 0, 0);
        //    zhujiemian1.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
        //    zhujiemian1.name = "Zhujiemian1";

        //    zhujiemian2 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_harbor")) as GameObject;
        //    zhujiemian2.transform.parent = Scence.transform;
        //    zhujiemian2.transform.localPosition = new Vector3(-31410, 0, 0);
        //    zhujiemian2.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
        //    zhujiemian2.name = "Zhujiemian2";

        //    RenderSettings.fog = false;
        //    LightMapAsset lightAsset = Resources.Load("LightMapAsset/lightMapAsset_131") as LightMapAsset;
        //    int count = lightAsset.lightmapFar.Length;
        //    LightmapData[] lightmapDatas = new LightmapData[count];
        //    for (int i = 0; i < count; i++)
        //    {
        //        LightmapData lightmapData = new LightmapData();
        //        lightmapData.lightmapFar = lightAsset.lightmapFar[i];
        //        lightmapData.lightmapNear = lightAsset.lightmapNear[i];
        //        lightmapDatas[i] = lightmapData;
        //    }
        //    LightmapSettings.lightmaps = lightmapDatas;
        //}
        //else if (rangNum == 3 || rangNum == 8) //工厂
        //{
        //    ScenceBeijing.SetActive(true);
        //    ScenceBeijing2.SetActive(false);
        //    zhujiemian1 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian")) as GameObject;
        //    zhujiemian1.transform.parent = Scence.transform;
        //    zhujiemian1.transform.localPosition = new Vector3(-10470f, 0, 0);
        //    zhujiemian1.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
        //    zhujiemian1.name = "Zhujiemian1";

        //    zhujiemian2 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian")) as GameObject;
        //    zhujiemian2.transform.parent = Scence.transform;
        //    zhujiemian2.transform.localPosition = new Vector3(0, 0, 0);
        //    zhujiemian2.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
        //    zhujiemian2.name = "Zhujiemian2";

        //    zhujiemian3 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian")) as GameObject;
        //    zhujiemian3.transform.parent = Scence.transform;
        //    zhujiemian3.transform.localPosition = new Vector3(10471f, 0, 0);
        //    zhujiemian3.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
        //    zhujiemian3.name = "Zhujiemian3";

        //    RenderSettings.fog = false;
        //    LightMapAsset lightAsset = Resources.Load("LightMapAsset/lightMapAsset_128") as LightMapAsset;
        //    int count = lightAsset.lightmapFar.Length;
        //    LightmapData[] lightmapDatas = new LightmapData[count];
        //    for (int i = 0; i < count; i++)
        //    {
        //        LightmapData lightmapData = new LightmapData();
        //        lightmapData.lightmapFar = lightAsset.lightmapFar[i];
        //        lightmapData.lightmapNear = lightAsset.lightmapNear[i];
        //        lightmapDatas[i] = lightmapData;
        //    }
        //    LightmapSettings.lightmaps = lightmapDatas;
        //}
        //else if (rangNum == 4 || rangNum == 9)//丛林
        //{
        //    ScenceBeijing.SetActive(false);
        //    ScenceBeijing2.SetActive(false);
        //    zhujiemian1 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_forest")) as GameObject;
        //    zhujiemian1.transform.parent = Scence.transform;
        //    zhujiemian1.transform.localPosition = new Vector3(-10470f, 0, 0);
        //    zhujiemian1.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
        //    zhujiemian1.name = "Zhujiemian1";

        //    zhujiemian2 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_forest")) as GameObject;
        //    zhujiemian2.transform.parent = Scence.transform;
        //    zhujiemian2.transform.localPosition = new Vector3(0, 0, 0);
        //    zhujiemian2.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
        //    zhujiemian2.name = "Zhujiemian2";

        //    zhujiemian3 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_forest")) as GameObject;
        //    zhujiemian3.transform.parent = Scence.transform;
        //    zhujiemian3.transform.localPosition = new Vector3(10470f, 0, 0);
        //    zhujiemian3.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
        //    zhujiemian3.name = "Zhujiemian3";

        //    RenderSettings.fog = false;
        //    LightMapAsset lightAsset = Resources.Load("LightMapAsset/lightMapAsset_129") as LightMapAsset;
        //    int count = lightAsset.lightmapFar.Length;
        //    LightmapData[] lightmapDatas = new LightmapData[count];
        //    for (int i = 0; i < count; i++)
        //    {
        //        LightmapData lightmapData = new LightmapData();
        //        lightmapData.lightmapFar = lightAsset.lightmapFar[i];
        //        lightmapData.lightmapNear = lightAsset.lightmapNear[i];
        //        lightmapDatas[i] = lightmapData;
        //    }
        //    LightmapSettings.lightmaps = lightmapDatas;
        //}
        //else if (rangNum == 5 || rangNum == 10)//城市
        //{
        //    ScenceBeijing.SetActive(false);
        //    ScenceBeijing2.SetActive(true);
        //    zhujiemian1 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_city")) as GameObject;
        //    zhujiemian1.transform.parent = Scence.transform;
        //    zhujiemian1.transform.localPosition = new Vector3(-10470f, 0, 0);
        //    zhujiemian1.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
        //    zhujiemian1.name = "Zhujiemian1";

        //    zhujiemian2 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_city")) as GameObject;
        //    zhujiemian2.transform.parent = Scence.transform;
        //    zhujiemian2.transform.localPosition = new Vector3(0, 0, 0);
        //    zhujiemian2.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
        //    zhujiemian2.name = "Zhujiemian2";

        //    zhujiemian3 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_city")) as GameObject;
        //    zhujiemian3.transform.parent = Scence.transform;
        //    zhujiemian3.transform.localPosition = new Vector3(10470f, 0, 0);
        //    zhujiemian3.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
        //    zhujiemian3.name = "Zhujiemian3";

        //    RenderSettings.fog = false;
        //    LightMapAsset lightAsset = Resources.Load("LightMapAsset/lightMapAsset_130") as LightMapAsset;
        //    int count = lightAsset.lightmapFar.Length;
        //    LightmapData[] lightmapDatas = new LightmapData[count];
        //    for (int i = 0; i < count; i++)
        //    {
        //        LightmapData lightmapData = new LightmapData();
        //        lightmapData.lightmapFar = lightAsset.lightmapFar[i];
        //        lightmapData.lightmapNear = lightAsset.lightmapNear[i];
        //        lightmapDatas[i] = lightmapData;
        //    }
        //    LightmapSettings.lightmaps = lightmapDatas;
        //}

        //SetTeamInfo();
        Invoke("SetTeamInfo", 0.3f);
        QieHuanIsOpen();
    }

    private void QieHuanMainScene() //切换主场景按钮
    {
        StopCoroutine("SetScenceTomove");
        StopCoroutine("SetHarborScenceTomove");

        int rangNum = PlayerPrefs.GetInt("MainScenceNum") + 1;
        PlayerPrefs.SetInt("MainScenceNum", rangNum);
        Debug.Log("rangNum" + rangNum);
        if (Scence.transform.Find("Zhujiemian1") != null) //防止断网有之前的场景
        {
            DestroyImmediate(Scence.transform.Find("Zhujiemian1").gameObject);
        }
        if (Scence.transform.Find("Zhujiemian2") != null)
        {
            DestroyImmediate(Scence.transform.Find("Zhujiemian2").gameObject);
        }
        if (Scence.transform.Find("Zhujiemian3") != null)
        {
            DestroyImmediate(Scence.transform.Find("Zhujiemian3").gameObject);
        }

        if (rangNum == 1 || rangNum == 6)//沙漠
        {
            //ScenceBeijing.SetActive(false);
            ScenceBeijing2.SetActive(false);
            zhujiemian1 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_desert")) as GameObject;
            zhujiemian1.transform.parent = Scence.transform;
            zhujiemian1.transform.localPosition = new Vector3(-10470f, 0, 0);
            zhujiemian1.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
            zhujiemian1.name = "Zhujiemian1";

            zhujiemian2 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_desert")) as GameObject;
            zhujiemian2.transform.parent = Scence.transform;
            zhujiemian2.transform.localPosition = new Vector3(0, 0, 0);
            zhujiemian2.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
            zhujiemian2.name = "Zhujiemian2";

            zhujiemian3 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_desert")) as GameObject;
            zhujiemian3.transform.parent = Scence.transform;
            zhujiemian3.transform.localPosition = new Vector3(10470f, 0, 0);
            zhujiemian3.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
            zhujiemian3.name = "Zhujiemian3";

            RenderSettings.fog = false;
            LightMapAsset lightAsset = Resources.Load("LightMapAsset/lightMapAsset_127") as LightMapAsset;
            int count = lightAsset.lightmapFar.Length;
            LightmapData[] lightmapDatas = new LightmapData[count];
            for (int i = 0; i < count; i++)
            {
                LightmapData lightmapData = new LightmapData();
                lightmapData.lightmapFar = lightAsset.lightmapFar[i];
                lightmapData.lightmapNear = lightAsset.lightmapNear[i];
                lightmapDatas[i] = lightmapData;
            }
            LightmapSettings.lightmaps = lightmapDatas;
        }
        else if (rangNum == 2 || rangNum == 7)//海港
        {
            Scence.transform.localPosition = new Vector3(0, 10260, 97420);
            //ScenceBeijing.SetActive(false);
            ScenceBeijing2.SetActive(false);
            zhujiemian1 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_harbor")) as GameObject;
            zhujiemian1.transform.parent = Scence.transform;
            zhujiemian1.transform.localPosition = new Vector3(0, 0, 0);
            zhujiemian1.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
            zhujiemian1.name = "Zhujiemian1";

            zhujiemian2 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_harbor")) as GameObject;
            zhujiemian2.transform.parent = Scence.transform;
            zhujiemian2.transform.localPosition = new Vector3(-31410, 0, 0);
            zhujiemian2.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
            zhujiemian2.name = "Zhujiemian2";

            RenderSettings.fog = false;
            LightMapAsset lightAsset = Resources.Load("LightMapAsset/lightMapAsset_131") as LightMapAsset;
            int count = lightAsset.lightmapFar.Length;
            LightmapData[] lightmapDatas = new LightmapData[count];
            for (int i = 0; i < count; i++)
            {
                LightmapData lightmapData = new LightmapData();
                lightmapData.lightmapFar = lightAsset.lightmapFar[i];
                lightmapData.lightmapNear = lightAsset.lightmapNear[i];
                lightmapDatas[i] = lightmapData;
            }
            LightmapSettings.lightmaps = lightmapDatas;
        }
        else if (rangNum == 3 || rangNum == 8) //工厂
        {
            //ScenceBeijing.SetActive(true);
            ScenceBeijing2.SetActive(false);
            zhujiemian1 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian")) as GameObject;
            zhujiemian1.transform.parent = Scence.transform;
            zhujiemian1.transform.localPosition = new Vector3(-10470f, 0, 0);
            zhujiemian1.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
            zhujiemian1.name = "Zhujiemian1";

            zhujiemian2 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian")) as GameObject;
            zhujiemian2.transform.parent = Scence.transform;
            zhujiemian2.transform.localPosition = new Vector3(0, 0, 0);
            zhujiemian2.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
            zhujiemian2.name = "Zhujiemian2";

            zhujiemian3 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian")) as GameObject;
            zhujiemian3.transform.parent = Scence.transform;
            zhujiemian3.transform.localPosition = new Vector3(10471f, 0, 0);
            zhujiemian3.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
            zhujiemian3.name = "Zhujiemian3";

            RenderSettings.fog = false;
            LightMapAsset lightAsset = Resources.Load("LightMapAsset/lightMapAsset_128") as LightMapAsset;
            int count = lightAsset.lightmapFar.Length;
            LightmapData[] lightmapDatas = new LightmapData[count];
            for (int i = 0; i < count; i++)
            {
                LightmapData lightmapData = new LightmapData();
                lightmapData.lightmapFar = lightAsset.lightmapFar[i];
                lightmapData.lightmapNear = lightAsset.lightmapNear[i];
                lightmapDatas[i] = lightmapData;
            }
            LightmapSettings.lightmaps = lightmapDatas;
        }
        else if (rangNum == 4 || rangNum == 9)//丛林
        {
            //ScenceBeijing.SetActive(false);
            ScenceBeijing2.SetActive(false);
            zhujiemian1 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_forest")) as GameObject;
            zhujiemian1.transform.parent = Scence.transform;
            zhujiemian1.transform.localPosition = new Vector3(-10470f, 0, 0);
            zhujiemian1.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
            zhujiemian1.name = "Zhujiemian1";

            zhujiemian2 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_forest")) as GameObject;
            zhujiemian2.transform.parent = Scence.transform;
            zhujiemian2.transform.localPosition = new Vector3(0, 0, 0);
            zhujiemian2.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
            zhujiemian2.name = "Zhujiemian2";

            zhujiemian3 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_forest")) as GameObject;
            zhujiemian3.transform.parent = Scence.transform;
            zhujiemian3.transform.localPosition = new Vector3(10470f, 0, 0);
            zhujiemian3.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
            zhujiemian3.name = "Zhujiemian3";

            RenderSettings.fog = false;
            LightMapAsset lightAsset = Resources.Load("LightMapAsset/lightMapAsset_129") as LightMapAsset;
            int count = lightAsset.lightmapFar.Length;
            LightmapData[] lightmapDatas = new LightmapData[count];
            for (int i = 0; i < count; i++)
            {
                LightmapData lightmapData = new LightmapData();
                lightmapData.lightmapFar = lightAsset.lightmapFar[i];
                lightmapData.lightmapNear = lightAsset.lightmapNear[i];
                lightmapDatas[i] = lightmapData;
            }
            LightmapSettings.lightmaps = lightmapDatas;
        }
        else if (rangNum == 5 || rangNum == 10)//城市
        {
            //ScenceBeijing.SetActive(false);
            ScenceBeijing2.SetActive(true);
            zhujiemian1 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_city")) as GameObject;
            zhujiemian1.transform.parent = Scence.transform;
            zhujiemian1.transform.localPosition = new Vector3(-10470f, 0, 0);
            zhujiemian1.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
            zhujiemian1.name = "Zhujiemian1";

            zhujiemian2 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_city")) as GameObject;
            zhujiemian2.transform.parent = Scence.transform;
            zhujiemian2.transform.localPosition = new Vector3(0, 0, 0);
            zhujiemian2.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
            zhujiemian2.name = "Zhujiemian2";

            zhujiemian3 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_city")) as GameObject;
            zhujiemian3.transform.parent = Scence.transform;
            zhujiemian3.transform.localPosition = new Vector3(10470f, 0, 0);
            zhujiemian3.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
            zhujiemian3.name = "Zhujiemian3";

            RenderSettings.fog = false;
            LightMapAsset lightAsset = Resources.Load("LightMapAsset/lightMapAsset_130") as LightMapAsset;
            int count = lightAsset.lightmapFar.Length;
            LightmapData[] lightmapDatas = new LightmapData[count];
            for (int i = 0; i < count; i++)
            {
                LightmapData lightmapData = new LightmapData();
                lightmapData.lightmapFar = lightAsset.lightmapFar[i];
                lightmapData.lightmapNear = lightAsset.lightmapNear[i];
                lightmapDatas[i] = lightmapData;
            }
            LightmapSettings.lightmaps = lightmapDatas;
        }
        //isMove = true;
        if (rangNum == 2 || rangNum == 7)
        {
            StartCoroutine("SetHarborScenceTomove");
        }
        else
        {
            StartCoroutine("SetScenceTomove");
        }

        QieHuanIsOpen();
        //if (!isSetHero)
        //{
        //    if (rangNum == 5)
        //    {
        //        StartCoroutine("SetHarborScenceTomove");
        //    }
        //    else
        //    {
        //        StartCoroutine("SetScenceTomove");
        //    }
        //    isSetHero = true;
        //}
    }



    public void UpdateHoldOnLeftTime()
    {
        Debug.Log("诞生boss");
        while (transform.Find("CloneBossName") != null)
        {
            DestroyImmediate(transform.Find("CloneBossName").gameObject);
        }
        if (PlayerPrefs.GetFloat("ElectractySlider") != 0)
        {
            StartCoroutine(CloneBossTomove());
        }
        else
        {
            HoldOnLeftTimeLabel.text = CharacterRecorder.instance.HoldKillNum.ToString();
            //StartCoroutine(CloneBossTomoveWhenElectractySlider());
        }
    }

    private void QieHuanIsOpen() //场景按钮是否开启
    {
        //int level = CharacterRecorder.instance.level; //kino
        //if (level >= 25 && level < 32 && PlayerPrefs.GetInt("MainScenceNum") < 2) //海港
        //{
        //    QieHuanButton.SetActive(true);
        //}
        //else if (level >= 32 && level < 38 && PlayerPrefs.GetInt("MainScenceNum") < 3) //工厂
        //{
        //    QieHuanButton.SetActive(true);
        //}
        //else if (level >= 38 && level < 43 && PlayerPrefs.GetInt("MainScenceNum") < 4) //丛林
        //{
        //    QieHuanButton.SetActive(true);
        //}
        //else if (level >= 43 && level < 48 && PlayerPrefs.GetInt("MainScenceNum") < 5)//城市高速公路
        //{
        //    QieHuanButton.SetActive(true);
        //}
        //else if (level >= 48 && level < 53 && PlayerPrefs.GetInt("MainScenceNum") < 6)//沙漠
        //{
        //    QieHuanButton.SetActive(true);
        //}
        //else if (level >= 53 && level < 58 && PlayerPrefs.GetInt("MainScenceNum") < 7)//冰冻海港
        //{
        //    QieHuanButton.SetActive(true);
        //}
        //else if (level >= 58 && level < 60 && PlayerPrefs.GetInt("MainScenceNum") < 8) //工厂
        //{
        //    QieHuanButton.SetActive(true);
        //}
        //else if (level >= 60 && level < 62 && PlayerPrefs.GetInt("MainScenceNum") < 9) //丛林
        //{
        //    QieHuanButton.SetActive(true);
        //}
        //else if (level >= 62 && PlayerPrefs.GetInt("MainScenceNum") < 10)//城市高速公路
        //{
        //    QieHuanButton.SetActive(true);
        //}
        //else
        //{
        //    QieHuanButton.SetActive(false);
        //}
    }


    void UpdateHoldOnTime()
    {
        if (CharacterRecorder.instance.HoldOnLeftTime > 0)
        {
            string houreStr = (CharacterRecorder.instance.HoldOnLeftTime / 3600).ToString("00");
            string miniteStr = (CharacterRecorder.instance.HoldOnLeftTime % 3600 / 60).ToString("00");
            string secondStr = (CharacterRecorder.instance.HoldOnLeftTime % 60).ToString("00");
            HoldOnLeftTimeLabel.text = string.Format("{0}:{1}:{2}", houreStr, miniteStr, secondStr);
            if (secondStr == "53" || secondStr == "43" || secondStr == "33" || secondStr == "23" || secondStr == "13" || secondStr == "03")
            {
                if (this.gameObject.activeSelf == true) //cardintrude窗口会把mainwindow  false
                {
                    StartCoroutine(CloneBossTomove());
                }
            }
            //if (secondStr == "00")
            //{
            //    StartCoroutine(SetGiftAnimation());
            //}
            //leftTime -= 1;
        }
        else
        {
            //HoldOnLeftTimeLabel.transform.parent.gameObject.SetActive(false);
            HoldOnLeftTimeLabel.text = "";
        }

    }

    void LevelExp()
    {
        SliderBlood.value = CharacterRecorder.instance.exp / (float)CharacterRecorder.instance.expMax;
    }
    void OnDestroy()
    {
        PictureCreater.instance.DestroyAllComponent();
    }
    void ClickHeroToEnterRolewindow()
    {
        UIEventListener.Get(Hero1).onClick += delegate(GameObject go)
        {
            RoleWindow.targetRoleCardId = ReccordHeroCardIdDic[1].cardID;
            CharacterRecorder.instance.enterRoleFromMain = true;
            UIManager.instance.OpenPanel("RoleWindow", true);
        };
        UIEventListener.Get(Hero2).onClick += delegate(GameObject go)
        {
            RoleWindow.targetRoleCardId = ReccordHeroCardIdDic[2].cardID;
            CharacterRecorder.instance.enterRoleFromMain = true;
            UIManager.instance.OpenPanel("RoleWindow", true);
        };
        UIEventListener.Get(Hero3).onClick += delegate(GameObject go)
        {
            RoleWindow.targetRoleCardId = ReccordHeroCardIdDic[3].cardID;
            CharacterRecorder.instance.enterRoleFromMain = true;
            UIManager.instance.OpenPanel("RoleWindow", true);
        };
        UIEventListener.Get(Hero4).onClick += delegate(GameObject go)
        {
            RoleWindow.targetRoleCardId = ReccordHeroCardIdDic[4].cardID;
            CharacterRecorder.instance.enterRoleFromMain = true;
            UIManager.instance.OpenPanel("RoleWindow", true);
        };
        UIEventListener.Get(Hero5).onClick += delegate(GameObject go)
        {
            RoleWindow.targetRoleCardId = ReccordHeroCardIdDic[5].cardID;
            CharacterRecorder.instance.enterRoleFromMain = true;
            UIManager.instance.OpenPanel("RoleWindow", true);
        };
        UIEventListener.Get(Hero6).onClick += delegate(GameObject go)
        {
            RoleWindow.targetRoleCardId = ReccordHeroCardIdDic[6].cardID;
            CharacterRecorder.instance.enterRoleFromMain = true;
            UIManager.instance.OpenPanel("RoleWindow", true);
        };
    }
    void MoveBG(GameObject go, bool isPress)
    {
        isOnPress = isPress;
        if (isPress)
        {
            oldPosition = Input.mousePosition.x;
            newPosition = MovePart.transform.localPosition.x;
        }
        else
        {
            StartCoroutine(moveTeamMumber());
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (UICamera.hoveredObject != null)
            {
                if (UICamera.hoveredObject.name == "UIRoot")
                {
                    oldPosition = Input.mousePosition.x;
                    ClickPosition = Input.mousePosition.x;
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (UICamera.hoveredObject != null)
            {
                if (UICamera.hoveredObject.name == "UIRoot")
                {
                    //Ray ray = RoleCamera.ScreenPointToRay(Input.mousePosition);
                    //RaycastHit hit;

                    float MainPosition = (Input.mousePosition.x - oldPosition);
                    float clickPosition = (Input.mousePosition.x - ClickPosition);
                    //Debug.Log(Input.mousePosition.x + "    -------   " + oldPosition);
                    if (clickPosition < 0)
                    {
                        if (Input.mousePosition.x < oldPosition)
                        {
                            //Debug.Log(MainPosition + "    ---------");
                            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[0]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[0]._CharacterPosition + 1], (Mathf.Abs(MainPosition) / 200));
                            if (PictureCreater.instance.ListRolePicture.Count > 1)
                            {
                                PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[1]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[1]._CharacterPosition + 1], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 2)
                            {
                                PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[2]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[2]._CharacterPosition + 1], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 3)
                            {
                                PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[3]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[3]._CharacterPosition + 1], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 4)
                            {
                                PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[4]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[4]._CharacterPosition + 1], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 5)
                            {
                                PictureCreater.instance.ListRolePicture[5].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[5].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[5]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[5]._CharacterPosition + 1], (Mathf.Abs(MainPosition) / 200));
                            }
                            oldPosition = Input.mousePosition.x;
                            cutPosition = MainPosition;
                            ResetHero(false);
                        }
                        else if (Input.mousePosition.x > oldPosition)
                        {
                            //Debug.Log(MainPosition + "      +++++++");
                            //Debug.Log((mTeamPosition[0]._CharacterPosition - 1));
                            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition, mVector3List[mTeamPosition[0]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            if (PictureCreater.instance.ListRolePicture.Count > 1)
                            {
                                PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition, mVector3List[mTeamPosition[1]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 2)
                            {
                                PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition, mVector3List[mTeamPosition[2]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 3)
                            {
                                PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition, mVector3List[mTeamPosition[3]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 4)
                            {
                                PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition, mVector3List[mTeamPosition[4]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 5)
                            {
                                PictureCreater.instance.ListRolePicture[5].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[5].RoleObject.transform.localPosition, mVector3List[mTeamPosition[5]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            }
                            oldPosition = Input.mousePosition.x;
                            cutPosition = MainPosition;
                            ResetHero(false);
                        }
                    }
                    if (clickPosition > 0)
                    {
                        if (Input.mousePosition.x < oldPosition)
                        {
                            //Debug.Log(MainPosition + "    ---------");
                            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition, mVector3List[mTeamPosition[0]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            if (PictureCreater.instance.ListRolePicture.Count > 1)
                            {
                                PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition, mVector3List[mTeamPosition[1]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 2)
                            {
                                PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition, mVector3List[mTeamPosition[2]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 3)
                            {
                                PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition, mVector3List[mTeamPosition[3]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 4)
                            {
                                PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition, mVector3List[mTeamPosition[4]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 5)
                            {
                                PictureCreater.instance.ListRolePicture[5].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[5].RoleObject.transform.localPosition, mVector3List[mTeamPosition[5]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            }
                            oldPosition = Input.mousePosition.x;
                            cutPosition = MainPosition;
                            ResetHero(false);
                        }
                        else if (Input.mousePosition.x > oldPosition)
                        {
                            //Debug.Log(MainPosition + "      +++++++");
                            //Debug.Log((mTeamPosition[0]._CharacterPosition - 1));
                            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[0]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[0]._CharacterPosition - 1], (Mathf.Abs(MainPosition) / 200));
                            if (PictureCreater.instance.ListRolePicture.Count > 1)
                            {
                                PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[1]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[1]._CharacterPosition - 1], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 2)
                            {
                                PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[2]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[2]._CharacterPosition - 1], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 3)
                            {
                                PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[3]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[3]._CharacterPosition - 1], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 4)
                            {
                                PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[4]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[4]._CharacterPosition - 1], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 5)
                            {
                                PictureCreater.instance.ListRolePicture[5].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[5].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[5]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[5]._CharacterPosition - 1], (Mathf.Abs(MainPosition) / 200));
                            }
                            oldPosition = Input.mousePosition.x;
                            cutPosition = MainPosition;
                            ResetHero(false);
                        }
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.LogError(UICamera.hoveredObject);
            if (UICamera.hoveredObject != null)
            {
                if (UICamera.hoveredObject.name == "UIRoot")
                {
                    StartCoroutine(moveTeamMumber());
                }
            }
        }

    }

    void ResetHero(bool IsVisible)
    {
        if (!IsVisible)
        {
            Hero1.SetActive(IsVisible);
            Hero2.SetActive(IsVisible);
            Hero3.SetActive(IsVisible);
            Hero4.SetActive(IsVisible);
            Hero5.SetActive(IsVisible);
            Hero6.SetActive(IsVisible);
        }
        else
        {
            foreach (var m in mTeamPosition)
            {
                for (int i = 0; i < CharacterRecorder.instance.ownedHeroList.size; i++)
                {
                    //Debug.LogError(CharacterRecorder.instance.ownedHeroList[i].characterRoleID + "        " + mCharacterID);
                    if (CharacterRecorder.instance.ownedHeroList[i].characterRoleID == m._CharacterRoleID)
                    {
                        PositionCount++;

                        int ShowNumber = 1;
                        if (PositionCount % PictureCreater.instance.ListRolePicture.Count == 0)
                        {
                            ShowNumber = PictureCreater.instance.ListRolePicture.Count;
                        }
                        else
                        {
                            ShowNumber = PositionCount % PictureCreater.instance.ListRolePicture.Count;
                        }
                        switch (ShowNumber)
                        {
                            case 1:
                                if (ReccordHeroCardIdDic.ContainsKey(1))
                                {
                                    ReccordHeroCardIdDic[1] = CharacterRecorder.instance.ownedHeroList[i];
                                }
                                else
                                {
                                    ReccordHeroCardIdDic.Add(1, CharacterRecorder.instance.ownedHeroList[i]);
                                }
                                SetNameColor(false, Hero1, LabelName1, LabelLevel1, SpriteJX1, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                                SpriteJX1Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                                break;
                            case 2:
                                if (ReccordHeroCardIdDic.ContainsKey(2))
                                {
                                    ReccordHeroCardIdDic[2] = CharacterRecorder.instance.ownedHeroList[i];
                                }
                                else
                                {
                                    ReccordHeroCardIdDic.Add(2, CharacterRecorder.instance.ownedHeroList[i]);
                                }
                                SetNameColor(false, Hero2, LabelName2, LabelLevel2, SpriteJX2, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                                SpriteJX2Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                                break;
                            case 3:
                                if (ReccordHeroCardIdDic.ContainsKey(3))
                                {
                                    ReccordHeroCardIdDic[3] = CharacterRecorder.instance.ownedHeroList[i];
                                }
                                else
                                {
                                    ReccordHeroCardIdDic.Add(3, CharacterRecorder.instance.ownedHeroList[i]);
                                }
                                SetNameColor(false, Hero3, LabelName3, LabelLevel3, SpriteJX3, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                                SpriteJX3Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                                break;
                            case 4:
                                if (ReccordHeroCardIdDic.ContainsKey(4))
                                {
                                    ReccordHeroCardIdDic[4] = CharacterRecorder.instance.ownedHeroList[i];
                                }
                                else
                                {
                                    ReccordHeroCardIdDic.Add(4, CharacterRecorder.instance.ownedHeroList[i]);
                                }
                                SetNameColor(false, Hero4, LabelName4, LabelLevel4, SpriteJX4, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                                SpriteJX4Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                                break;
                            case 5:
                                if (ReccordHeroCardIdDic.ContainsKey(5))
                                {
                                    ReccordHeroCardIdDic[5] = CharacterRecorder.instance.ownedHeroList[i];
                                }
                                else
                                {
                                    ReccordHeroCardIdDic.Add(5, CharacterRecorder.instance.ownedHeroList[i]);
                                }
                                SetNameColor(false, Hero5, LabelName5, LabelLevel5, SpriteJX5, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                                SpriteJX5Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                                break;
                            case 6:
                                if (ReccordHeroCardIdDic.ContainsKey(6))
                                {
                                    ReccordHeroCardIdDic[6] = CharacterRecorder.instance.ownedHeroList[i];
                                }
                                else
                                {
                                    ReccordHeroCardIdDic.Add(6, CharacterRecorder.instance.ownedHeroList[i]);
                                }
                                SetNameColor(false, Hero6, LabelName6, LabelLevel6, SpriteJX6, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                                SpriteJX6Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                                break;
                        }
                    }
                }
            }
        }
    }

    IEnumerator moveTeamMumber()
    {
        if (cutPosition > 0)
        {
            for (int i = 1; i < 11; i++)
            {
                PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[0]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[0]._CharacterPosition - 1], i / 10f);
                if (PictureCreater.instance.ListRolePicture.Count > 1)
                {
                    PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[1]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[1]._CharacterPosition - 1], i / 10f);
                }
                if (PictureCreater.instance.ListRolePicture.Count > 2)
                {
                    PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[2]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[2]._CharacterPosition - 1], i / 10f);
                }
                if (PictureCreater.instance.ListRolePicture.Count > 3)
                {
                    PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[3]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[3]._CharacterPosition - 1], i / 10f);
                }
                if (PictureCreater.instance.ListRolePicture.Count > 4)
                {
                    PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[4]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[4]._CharacterPosition - 1], i / 10f);
                }
                if (PictureCreater.instance.ListRolePicture.Count > 5)
                {
                    PictureCreater.instance.ListRolePicture[5].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[5].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[5]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[5]._CharacterPosition - 1], i / 10f);
                }
                if (i == 10)
                {
                    cutPosition = 0;
                    mTeamPosition[0]._CharacterPosition = (mTeamPosition[0]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[0]._CharacterPosition - 1;
                    if (PictureCreater.instance.ListRolePicture.Count > 1)
                    {
                        mTeamPosition[1]._CharacterPosition = (mTeamPosition[1]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[1]._CharacterPosition - 1;
                    }
                    if (PictureCreater.instance.ListRolePicture.Count > 2)
                    {
                        mTeamPosition[2]._CharacterPosition = (mTeamPosition[2]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[2]._CharacterPosition - 1;
                    }
                    if (PictureCreater.instance.ListRolePicture.Count > 3)
                    {
                        mTeamPosition[3]._CharacterPosition = (mTeamPosition[3]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[3]._CharacterPosition - 1;
                    }
                    if (PictureCreater.instance.ListRolePicture.Count > 4)
                    {
                        mTeamPosition[4]._CharacterPosition = (mTeamPosition[4]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[4]._CharacterPosition - 1;
                    }
                    if (PictureCreater.instance.ListRolePicture.Count > 5)
                    {
                        mTeamPosition[5]._CharacterPosition = (mTeamPosition[5]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[5]._CharacterPosition - 1;
                    }
                }
                yield return new WaitForSeconds(0.01f);
            }
            PositionCount--;
        }
        else if (cutPosition < 0)
        {
            for (int i = 1; i < 11; i++)
            {
                PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[0]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[0]._CharacterPosition + 1], i / 10f);
                if (PictureCreater.instance.ListRolePicture.Count > 1)
                {
                    PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[1]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[1]._CharacterPosition + 1], i / 10f);
                }
                if (PictureCreater.instance.ListRolePicture.Count > 2)
                {
                    PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[2]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[2]._CharacterPosition + 1], i / 10f);
                }
                if (PictureCreater.instance.ListRolePicture.Count > 3)
                {
                    PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[3]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[3]._CharacterPosition + 1], i / 10f);
                }
                if (PictureCreater.instance.ListRolePicture.Count > 4)
                {
                    PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[4]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[4]._CharacterPosition + 1], i / 10f);
                }
                if (PictureCreater.instance.ListRolePicture.Count > 5)
                {
                    PictureCreater.instance.ListRolePicture[5].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[5].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[5]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[5]._CharacterPosition + 1], i / 10f);
                }
                if (i == 10)
                {
                    cutPosition = 0;
                    mTeamPosition[0]._CharacterPosition = (mTeamPosition[0]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[0]._CharacterPosition + 1;
                    if (PictureCreater.instance.ListRolePicture.Count > 1)
                    {
                        mTeamPosition[1]._CharacterPosition = (mTeamPosition[1]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[1]._CharacterPosition + 1;
                    }
                    if (PictureCreater.instance.ListRolePicture.Count > 2)
                    {
                        mTeamPosition[2]._CharacterPosition = (mTeamPosition[2]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[2]._CharacterPosition + 1;
                    }
                    if (PictureCreater.instance.ListRolePicture.Count > 3)
                    {
                        mTeamPosition[3]._CharacterPosition = (mTeamPosition[3]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[3]._CharacterPosition + 1;
                    }
                    if (PictureCreater.instance.ListRolePicture.Count > 4)
                    {
                        mTeamPosition[4]._CharacterPosition = (mTeamPosition[4]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[4]._CharacterPosition + 1;
                    }
                    if (PictureCreater.instance.ListRolePicture.Count > 5)
                    {
                        mTeamPosition[5]._CharacterPosition = (mTeamPosition[5]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[5]._CharacterPosition + 1;
                    }
                }
                yield return new WaitForSeconds(0.01f);
            }
            PositionCount++;
        }
        ResetHero(true);
    }
    //即将开始提示
    public void Reset()
    {
        headIcon.mainTexture = Resources.Load(string.Format("Head/{0}", CharacterRecorder.instance.headIcon), typeof(Texture)) as Texture;
        LabelVip.text = string.Format("VIP{0}", CharacterRecorder.instance.Vip);
        LabelName.text = CharacterRecorder.instance.characterName;
        LabelLevel.text = "Lv." + CharacterRecorder.instance.level.ToString();
      //  LabelFight.text = CharacterRecorder.instance.Fight.ToString();
        LabelFight.text = CharacterRecorder.instance.ChangeNum(CharacterRecorder.instance.Fight);//修改
        //if (RedPointList.Count > 0 && SceneTransformer.instance.CheckGuideIsFinish())
        //{
        //    RandomListShow();
        //}
        //else
        //{
        //    WillOpenTipButton.SetActive(true);
        //    SetWillOpenInfo();
        //}

    }

    public void ChoseWillOpenTipButton() //小目标，红点小提示，关卡开放
    {
        int smallGoalGetID = PlayerPrefs.GetInt("SmallGoalGetID");
        int quest = 0;        
        isOpenSmall = false;
        for (int i = 0; i < TextTranslator.instance.SmallGoalList.size; i++)
        {
            if (TextTranslator.instance.SmallGoalList[i].SmallGoalID > smallGoalGetID)//&&CharacterRecorder.instance.lastGateID >= TextTranslator.instance.SmallGoalList[i].Quest)
            {
                isOpenSmall = true;
                quest = TextTranslator.instance.SmallGoalList[i].Quest;
                smallGoalID = TextTranslator.instance.SmallGoalList[i].SmallGoalID;
                break;
            }
        }
        //if (isOpenSmall == false) 
        //{
        //    if (GameObject.Find("SmallGoalWindow") != null) 
        //    {
        //        DestroyImmediate(GameObject.Find("SmallGoalWindow"));
        //    }
        //}

        if (isOpenSmall)
        {
            SmallGoalButtonWillOpen(smallGoalID, quest);
            //UIEventListener.Get(WillOpenTipButton).onClick = delegate(GameObject go)
            //{
            //    UIManager.instance.OpenSinglePanel("SmallGoalWindow", false);
            //    GameObject.Find("SmallGoalWindow").GetComponent<SmallGoalWindow>().ShowInfoLastGateIDAward(smallGoalID, quest);
            //};
            //if (GameObject.Find("SmallGoalWindow") != null) 
            //{
            //    GameObject.Find("SmallGoalWindow").GetComponent<SmallGoalWindow>().ShowInfoLastGateIDAward(smallGoalID, quest);
            //}
        }
        else if (RedPointList.Count > 0 && SceneTransformer.instance.CheckGuideIsFinish())
        {
            RandomListShow();
        }
        else
        {
            WillOpenTipButton.SetActive(true);
            SetWillOpenInfo();
            UIEventListener.Get(WillOpenTipButton).onClick = delegate(GameObject go)
            {
                UIManager.instance.OpenPanel("WillOpenWindow", false);
            };
        }
    }

    private void SmallGoalButtonWillOpen(int _smallGoalID, int quest)
    {
        WillOpenTipButton.SetActive(true);
        SmallGoal smallGoal = TextTranslator.instance.GetSmallGoalByID(_smallGoalID);
        SetItemDetail(smallGoal.ItemID, smallGoal.ItemNum, WillOpenTipButton.transform.Find("SpriteSmallGoalTop/Item").gameObject);
        WillOpenTipButton.transform.Find("NameLabel").GetComponent<UILabel>().text = "小目标";
        WillOpenTipButton.transform.Find("OpenLvLabel").GetComponent<UILabel>().text = "通关第" + (quest - 10000).ToString() + "关领奖";
        WillOpenTipButton.transform.Find("SpriteTexture").gameObject.SetActive(false);
        WillOpenTipButton.transform.Find("SpriteSmallGoalTop").gameObject.SetActive(true);
        StartCoroutine(SetWillOpenEffect(WillOpenTipButton.transform.Find("SpriteSmallGoalTop")));

        if (CharacterRecorder.instance.lastGateID > quest)
        {
            WillOpenTipButton.transform.Find("SpriteSmallGoalTop/QuanEffect").gameObject.SetActive(true);
            UIEventListener.Get(WillOpenTipButton).onClick = delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("1922#" + _smallGoalID + ";");
            };
        }
        else
        {
            WillOpenTipButton.transform.Find("SpriteSmallGoalTop/QuanEffect").gameObject.SetActive(false);
            UIEventListener.Get(WillOpenTipButton).onClick = UIEventListener.Get(GameObject.Find("WorldButton")).onClick;
        }
    }

    /// <summary>
    /// 角色在MainWindow界面升级的时候 刷新角色信息
    /// </summary>
    public void ReSetRole()
    {
        //Reset();
        LabelLevel.text = "Lv." + CharacterRecorder.instance.level.ToString();
        LevelExp();
    }
    public void RandomListShow()
    {
        RandomNum = -1;
        if (RedPointList.Count > 0 && SceneTransformer.instance.CheckGuideIsFinish() && isOpenSmall == false)
        {
            WillOpenTipButton.SetActive(true);
            WillOpenTipButton.transform.Find("SpriteTexture").gameObject.SetActive(true);
            WillOpenTipButton.transform.Find("SpriteSmallGoalTop").gameObject.SetActive(false);
            bool isSame = false;
            while (!isSame)
            {
                int num = Random.Range(0, RedPointList.Count);
                if (RandomNum != num)
                {
                    RandomNum = num;
                    isSame = true;
                }

            }
            RandomRedPointInfo(RedPointList[RandomNum]);
            if (GameObject.Find(RedPointList[RandomNum].ToString()) != null)
            {
                RandomObj = GameObject.Find(RedPointList[RandomNum].ToString()).transform.parent.gameObject;
                //SceneTransformer.instance.isNewGuide = true;
                UIEventListener.Get(WillOpenTipButton).onClick = UIEventListener.Get(RandomObj).onClick;
            }
        }
    }
    void SetWillOpenInfo()
    {
        // NewGuide _NewGuide = TextTranslator.instance.FindNewGuideNextWillOpenByCurLevel(CharacterRecorder.instance.level);
        WillOpenTipButton.transform.Find("SpriteTexture").gameObject.SetActive(true);
        WillOpenTipButton.transform.Find("SpriteSmallGoalTop").gameObject.SetActive(false);
        NewGuide _NewGuide = TextTranslator.instance.FindNewGuideNextWillOpenByCurLevel(CharacterRecorder.instance.lastGateID - 1);
        if (_NewGuide == null)
        {
            WillOpenTipButton.SetActive(false);
            return;
        }
        UILabel willOpenNameLabel = WillOpenTipButton.transform.FindChild("NameLabel").GetComponent<UILabel>();
        UILabel willOpenLvLabel = WillOpenTipButton.transform.FindChild("OpenLvLabel").GetComponent<UILabel>();
        //UISprite spriteIcon = WillOpenTipButton.transform.FindChild("SpriteIcon").GetComponent<UISprite>();
        UITexture spriteTexture = WillOpenTipButton.transform.FindChild("SpriteTexture").GetComponent<UITexture>();
        spriteTexture.transform.localPosition = new Vector3(-83, 0, 0);
        willOpenNameLabel.text = _NewGuide.Name;
        willOpenLvLabel.text = string.Format("通关第{0}关开启", _NewGuide.Level - 10000);
        // spriteIcon.spriteName = _NewGuide.LevelUpTipIcon;
        spriteTexture.mainTexture = Resources.Load(string.Format("NewGuide/{0}", _NewGuide.MainIcon), typeof(Texture)) as Texture;
        //StartCoroutine(SetWillOpenEffect(spriteTexture.transform));
    }
    IEnumerator SetWillOpenEffect(Transform parentTrans)
    {
        if (GameObject.Find("GanTanHao_02(Clone)") != null)
        {
            DestroyImmediate(GameObject.Find("GanTanHao_02(Clone)"));
        }
        yield return new WaitForSeconds(0.5f);
        GameObject _Effect = GameObject.Instantiate(Resources.Load("Prefab/Effect/GanTanHao_02", typeof(GameObject))) as GameObject;
        _Effect.AddComponent<VFXRenderQueueSorter>();
        _Effect.transform.parent = parentTrans;
        _Effect.transform.localPosition = new Vector3(-50, -20, 0);
        _Effect.transform.localScale = new Vector3(70, 70, 1);
        _Effect.transform.localRotation = new Quaternion(0, 0, 0, 0);

        //StartCoroutine(SetWillOpenEffect(parentTrans));
    }

    //随机提醒忘记可以进行的操作
    #region 小贴士直接跳转

    void RandomRedPointInfo(int RandomNumber)
    {
        UILabel willOpenNameLabel = WillOpenTipButton.transform.FindChild("NameLabel").GetComponent<UILabel>();
        UILabel willOpenLvLabel = WillOpenTipButton.transform.FindChild("OpenLvLabel").GetComponent<UILabel>();
        UITexture spriteTexture = WillOpenTipButton.transform.FindChild("SpriteTexture").GetComponent<UITexture>();
        spriteTexture.transform.localPosition = new Vector3(-83, 5, 0);
        spriteTexture.mainTexture = Resources.Load(string.Format("NewGuide/{0}", "Lili"), typeof(Texture)) as Texture;
        switch (RandomNumber)
        {
            case 1://世界Boss
                willOpenNameLabel.text = "Boss";
                willOpenLvLabel.text = "世界Boss来了，快杀";
                break;
            case 2://大放送
                willOpenNameLabel.text = "大放送";
                willOpenLvLabel.text = "您有大放送奖励可领";
                break;
            case 3://战力排行
                willOpenNameLabel.text = "战力排行";
                willOpenLvLabel.text = "您战力排行更新了";
                break;
            case 4://登录奖励
                willOpenNameLabel.text = "登录奖励";
                willOpenLvLabel.text = "您有登录奖励可领";
                break;
            case 5://首冲
                willOpenNameLabel.text = "首充奖励";
                willOpenLvLabel.text = "您的首充奖励可领";
                break;
            case 6://充值
                break;
            case 7://签到
                willOpenNameLabel.text = "签到";
                willOpenLvLabel.text = "您还没有签到";
                break;
            case 8://活动
                willOpenNameLabel.text = "活动";
                willOpenLvLabel.text = "您有活动奖励可领";
                break;
            case 9://英雄
                willOpenNameLabel.text = "英雄";
                willOpenLvLabel.text = "您有英雄可以强化";
                break;
            case 10://装备
                willOpenNameLabel.text = "装备";
                willOpenLvLabel.text = "您有装备可以强化";
                break;
            case 11://抽卡
                willOpenNameLabel.text = "招募";
                willOpenLvLabel.text = "您有免费招募次数";
                break;
            case 12://背包
                willOpenNameLabel.text = "背包";
                willOpenLvLabel.text = "您有新的物品";
                break;
            case 13://商城
                willOpenNameLabel.text = "商城";
                willOpenLvLabel.text = "您有东西可以购买";
                break;
            case 14://任务
                willOpenNameLabel.text = "任务";
                willOpenLvLabel.text = "您有任务奖励可领";
                break;
            case 15://征服
                //willOpenNameLabel.text = "征服";
                //willOpenLvLabel.text = "您有新的征服等待处理";
                break;
            case 16://冲突
                willOpenNameLabel.text = "冲突";
                willOpenLvLabel.text = "您有新的冲突等待处理";
                break;
            case 17://战术
                willOpenNameLabel.text = "战术";
                willOpenLvLabel.text = "您有新的战术";
                break;
            case 18://情报
                willOpenNameLabel.text = "情报";
                willOpenLvLabel.text = "您有情报点可以升级";
                break;
            case 19://实验室
                willOpenNameLabel.text = "实验室";
                willOpenLvLabel.text = "您有实验室可以升级";
                break;


            case 21://英雄榜
                willOpenNameLabel.text = "英雄榜";
                willOpenLvLabel.text = "您英雄榜信息更新";
                break;
            case 22://军团
                willOpenNameLabel.text = "军团";
                willOpenLvLabel.text = "您的军团有情况";
                break;

            case 24://邮件
                willOpenNameLabel.text = "邮件";
                willOpenLvLabel.text = "您有邮件没有接受";
                break;
            case 26://图鉴
                willOpenNameLabel.text = "图鉴";
                willOpenLvLabel.text = "您有新的英雄可以合成";
                break;
            case 25://朋友
                willOpenNameLabel.text = "好友";
                willOpenLvLabel.text = "您有新的好友等待处理";
                break;

            case 29://问卷
                willOpenNameLabel.text = "问卷";
                willOpenLvLabel.text = "您的问卷奖励可领";
                break;



            default:
                WillOpenTipButton.SetActive(false);
                break;
        }
        StartCoroutine(SetWillOpenEffect(spriteTexture.transform));

    }
    #endregion


    public void SetTeamInfo()
    {
        //CharacterRecorder.instance.HostageRoleID = 60011;
        //CharacterRecorder.instance.HostageName = "60001";
        //AddRandomBossInMainwindow(); //kino
        PictureCreater.instance.DestroyAllComponent();
        ReccordHeroCardIdDic.Clear();
        HeroTeam.Clear();
        mTeamPosition.Clear();

        List<int> ListForce = new List<int>();
        for (int i = 0; i < CharacterRecorder.instance.ownedHeroList.size; i++)
        {
            ListForce.Add(CharacterRecorder.instance.ownedHeroList[i].force);
        }

        ListForce.Sort();
        int MinForce = 0;
        int heroNum = 0;
        if (ListForce.Count > 5)
        {
            if (CharacterRecorder.instance.HostageName != "")
            {
                MinForce = ListForce[ListForce.Count - 5];
            }
            else
            {
                MinForce = ListForce[ListForce.Count - 6];
            }
        }
        int ShowCount = 6;
        int TeamNumber = 6;
        PositionCount = 0;
        ReccordHeroCardIdDic.Clear();
        HeroTeam.Clear();




        for (int i = 0; i < CharacterRecorder.instance.ownedHeroList.size; i++)
        {
            if (CharacterRecorder.instance.ownedHeroList[i].force >= MinForce && ShowCount > 0)
            {
                ShowCount--;
                int j = PictureCreater.instance.CreateRole(CharacterRecorder.instance.ownedHeroList[i].cardID, "Model", 18, Color.red, 0, 0, 1, 2f, false, false, 1, 1.5f, 0.2f, "Model", 0, 10, 10, 2, 2, 0, 0, 1001, 1002, CharacterRecorder.instance.ownedHeroList[i].WeaponList[0].WeaponClass, 1, CharacterRecorder.instance.ownedHeroList[i].WeaponList[0].WeaponClass, 1, 1, 0, "");
                PictureCreater.instance.ListRolePicture[j].RoleObject.transform.parent = gameObject.transform;
                //PictureCreater.instance.ListRolePicture[j].RolePictureObject.GetComponent<Animator>().Play("idle2");
                //PictureCreater.instance.ListRolePicture[j].RolePictureObject.GetComponent<Animator>().SetFloat("id", 0);

                if (TeamNumber == 4)
                {
                    if (j == 0)
                    {
                        if ((Screen.width * 1f / Screen.height) > 1.7f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(1862, 10790, 94205);
                        }
                        else if ((Screen.width * 1f / Screen.height) < 1.4f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(1862, 10790, 94205);
                        }
                        else
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(1862, 10790, 94205);
                        }
                    }
                    else if (j == 1)
                    {
                        if ((Screen.width * 1f / Screen.height) > 1.7f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(7904, -400, 13065);
                        }
                        else if ((Screen.width * 1f / Screen.height) < 1.4f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(7904, -400, 13065);
                        }
                        else
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(7904, -400, 13065);
                        }
                    }
                    else if (j == 2)
                    {
                        if ((Screen.width * 1f / Screen.height) > 1.7f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(10927, -400, 13065);
                        }
                        else if ((Screen.width * 1f / Screen.height) < 1.4f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(10927, -400, 13065);
                        }
                        else
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(10927, -400, 13065);
                        }
                    }
                    else if (j == 3)
                    {
                        if ((Screen.width * 1f / Screen.height) > 1.7f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(9370, -400, 12077);
                        }
                        else if ((Screen.width * 1f / Screen.height) < 1.4f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(9370, -400, 12077);
                        }
                        else
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(9370, -400, 12077);
                        }
                    }
                }

                if (TeamNumber == 6)
                {
                    if (j == 0)
                    {
                        if ((Screen.width * 1f / Screen.height) > 1.7f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(2122, 10591, 93860);
                        }
                        else if ((Screen.width * 1f / Screen.height) < 1.4f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(2122, 10591, 93860);
                        }
                        else
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(2122, 10591, 93860);
                        }
                    }
                    else if (j == 1)
                    {
                        if ((Screen.width * 1f / Screen.height) > 1.7f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(1274, 10591, 93860);
                        }
                        else if ((Screen.width * 1f / Screen.height) < 1.4f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(1274, 10591, 93860);
                        }
                        else
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(1274, 10591, 93860);
                        }
                    }
                    else if (j == 2)
                    {
                        if ((Screen.width * 1f / Screen.height) > 1.7f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(410, 10591, 93860);
                        }
                        else if ((Screen.width * 1f / Screen.height) < 1.4f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(410, 10591, 93860);
                        }
                        else
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(410, 10591, 93860);
                        }
                    }
                    else if (j == 3)
                    {
                        if ((Screen.width * 1f / Screen.height) > 1.7f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(-454, 10591, 93860);
                        }
                        else if ((Screen.width * 1f / Screen.height) < 1.4f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(-454, 10591, 93860);
                        }
                        else
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(-454, 10591, 93860);
                        }
                    }
                    else if (j == 4)
                    {
                        if ((Screen.width * 1f / Screen.height) > 1.7f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(-1318, 10591, 93860);//12900
                        }
                        else if ((Screen.width * 1f / Screen.height) < 1.4f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(-1318, 10591, 93860);
                        }
                        else
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(-1318, 10591, 93860);
                        }
                    }
                    else if (j == 5)
                    {
                        if ((Screen.width * 1f / Screen.height) > 1.7f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(-2182, 10591, 93860);//12900
                        }
                        else if ((Screen.width * 1f / Screen.height) < 1.4f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(-2182, 10591, 93860);
                        }
                        else
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(-2182, 10591, 93860);
                        }
                    }
                    mVector3List.Add(PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition);
                }

                heroNum = j;
                TeamMumberPosition mTeam = new TeamMumberPosition();
                mTeam._Character = PictureCreater.instance.ListRolePicture[j].RoleObject;
                mTeam._CharacterPosition = j;
                mTeam._CharacterRoleID = CharacterRecorder.instance.ownedHeroList[i].characterRoleID;
                mTeamPosition.Add(mTeam);
                PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localScale = new Vector3(500, 500, 500);
                PictureCreater.instance.ListRolePicture[j].RoleObject.transform.Rotate(new Vector3(0, 0, 0));/////yy

                PositionCount++;
                //PictureCreater.instance.ListRolePicture[j].RolePictureObject.GetComponent<Animator>().SetFloat("ft", 0);
                HeroTeam.Add(PictureCreater.instance.ListRolePicture[j]);
                switch (PositionCount)
                {
                    case 1:
                        ReccordHeroCardIdDic.Add(1, CharacterRecorder.instance.ownedHeroList[i]);
                        SetNameColor(true, Hero1, LabelName1, LabelLevel1, SpriteJX1, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                        SpriteJX1Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                        break;
                    case 2:
                        ReccordHeroCardIdDic.Add(2, CharacterRecorder.instance.ownedHeroList[i]);
                        SetNameColor(true, Hero2, LabelName2, LabelLevel2, SpriteJX2, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                        SpriteJX2Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                        break;
                    case 3:
                        ReccordHeroCardIdDic.Add(3, CharacterRecorder.instance.ownedHeroList[i]);
                        SetNameColor(true, Hero3, LabelName3, LabelLevel3, SpriteJX3, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                        SpriteJX3Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                        break;
                    case 4:
                        ReccordHeroCardIdDic.Add(4, CharacterRecorder.instance.ownedHeroList[i]);
                        SetNameColor(true, Hero4, LabelName4, LabelLevel4, SpriteJX4, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                        SpriteJX4Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                        break;
                    case 5:
                        ReccordHeroCardIdDic.Add(5, CharacterRecorder.instance.ownedHeroList[i]);
                        SetNameColor(true, Hero5, LabelName5, LabelLevel5, SpriteJX5, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                        SpriteJX5Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                        break;
                    case 6:
                        ReccordHeroCardIdDic.Add(6, CharacterRecorder.instance.ownedHeroList[i]);
                        SetNameColor(true, Hero6, LabelName6, LabelLevel6, SpriteJX6, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                        SpriteJX6Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                        break;
                }
            }
        }


        if (CharacterRecorder.instance.HostageName != "")
        {
            Hero6.SetActive(false);
            heroNum++;
            if (HostageObject == null)
            {
                HostageObject = Instantiate(Resources.Load("Prefab/Role/" + CharacterRecorder.instance.HostageRoleID.ToString(), typeof(GameObject))) as GameObject;
                HeroInfo hi = TextTranslator.instance.GetHeroInfoByHeroID(CharacterRecorder.instance.HostageRoleID);

                HostageObject.transform.parent = gameObject.transform;
                HostageObject.transform.Rotate(new Vector3(0, 90, 0));
                HostageObject.transform.localScale = new Vector3(hi.heroScale * 500f, hi.heroScale * 500f, hi.heroScale * 500f);

                HostageObject.GetComponent<Animator>().Play("fulu_idle");

                if (PlayerPrefs.GetFloat("ElectractySlider") != 0 && false)//耗电模式
                {
                    HostageObject.GetComponent<Animator>().SetFloat("ft", 0);
                }


                foreach (Component c in HostageObject.GetComponentsInChildren(typeof(SkinnedMeshRenderer), true))
                {
                    if (c.name == "Object001" || c.name == "Object002")
                    {
                        c.gameObject.SetActive(false);
                    }
                }

                foreach (Component c in HostageObject.GetComponentsInChildren(typeof(MeshRenderer), true))
                {
                    if (c.name == "Object001" || c.name == "Object002")
                    {
                        c.gameObject.SetActive(false);
                    }
                }
            }

            if ((Screen.width * 1f / Screen.height) > 1.7f)
            {
                HostageObject.transform.localPosition = new Vector3(-2182, 10591, 93860);//12900
            }
            else if ((Screen.width * 1f / Screen.height) < 1.4f)
            {
                HostageObject.transform.localPosition = new Vector3(-2182, 10591, 93860);
            }
            else
            {
                HostageObject.transform.localPosition = new Vector3(-2182, 10591, 93860);
            }
            LabelHostage.GetComponent<UILabel>().text = CharacterRecorder.instance.HostageName;
            LabelHostage.SetActive(true);
        }

        for (int i = 0; i < HeroAddClickArr.Length; i++)
        {
            if (i > heroNum)
            {
                HeroAddClickArr[i].SetActive(true);
                int num = i;
                UIEventListener.Get(HeroAddClickArr[i]).onClick = delegate(GameObject go)
                {
                    if (num == 5)
                    {
                        UIManager.instance.OpenPromptWindow("25级开启第六个英雄栏位", PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("赶紧去获得英雄", PromptWindow.PromptType.Hint, null, null);
                    }
                };
            }
        }
        //AddRandomBossInMainwindow();
        if (PlayerPrefs.GetFloat("ElectractySlider") != 0 && false) //耗电模式
        {
            IsMoveScence = true;
            for (int i = 0; i < HeroTeam.Count; i++)
            {
                HeroTeam[i].RolePictureObject.GetComponent<Animator>().SetFloat("ft", 0);
            }

            //if (!isSetHero)
            //{
            //    if (PlayerPrefs.GetInt("MainScenceNum") == 2 || PlayerPrefs.GetInt("MainScenceNum") == 7)
            //    {
            //        //StartCoroutine(SetHarborScenceTomove());
            //        StartCoroutine("SetHarborScenceTomove");
            //    }
            //    else
            //    {
            //        StartCoroutine("SetScenceTomove");
            //    }
            //    isSetHero = true;
            //}
        }
        else
        {
            IsMoveScence = false;
            for (int i = 0; i < HeroTeam.Count; i++)
            {
                HeroTeam[i].RolePictureObject.transform.parent.localRotation = new Quaternion(0, 0, 0, 0);
                HeroTeam[i].RolePictureObject.transform.parent.Rotate(new Vector3(0, 90, 0));
            }

            if (HostageObject != null)
            {
                HostageObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
                HostageObject.transform.Rotate(new Vector3(0, 180, 0));
            }
        }
        if (!isSetHero)
        {
            if (PlayerPrefs.GetInt("MainScenceNum") == 2 || PlayerPrefs.GetInt("MainScenceNum") == 7)
            {
                //StartCoroutine(SetHarborScenceTomove());
                StartCoroutine("SetHarborScenceTomove");
            }
            else
            {
                StartCoroutine("SetScenceTomove");
            }
            isSetHero = true;
        }
    }


    public void ChangeWhenElectractySlider() //省电或耗电情况下场景转变
    {
        if (PlayerPrefs.GetFloat("ElectractySlider") != 0 && false) //耗电模式
        {
            IsMoveScence = true;
            for (int i = 0; i < HeroTeam.Count; i++)
            {
                HeroTeam[i].RolePictureObject.GetComponent<Animator>().SetFloat("ft", 0);
                HeroTeam[i].RolePictureObject.transform.parent.localRotation = new Quaternion(0, 0, 0, 0);
                HeroTeam[i].RolePictureObject.transform.parent.Rotate(new Vector3(0, 0, 0));
            }

            if (HostageObject != null)
            {
                HostageObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
                HostageObject.transform.Rotate(new Vector3(0, 90, 0));
                HostageObject.GetComponent<Animator>().SetFloat("ft", 0);
            }
        }
        else
        {
            IsMoveScence = false;
            StopCoroutine("CloneBossTomove");
            for (int i = 0; i < HeroTeam.Count; i++)
            {
                HeroTeam[i].RolePictureObject.GetComponent<Animator>().SetFloat("ft", 2);
                HeroTeam[i].RolePictureObject.transform.parent.localRotation = new Quaternion(0, 0, 0, 0);
                HeroTeam[i].RolePictureObject.transform.parent.Rotate(new Vector3(0, 90, 0));
            }

            if (HostageObject != null)
            {
                HostageObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
                HostageObject.transform.Rotate(new Vector3(0, 180, 0));
                HostageObject.GetComponent<Animator>().SetFloat("ft", 2);
            }
        }
    }





    void AddRandomBossInMainwindow()  //boss预制加载
    {
        AssetBossList.Clear();
        for (int i = 1; i <= 9; i++)
        {
            int heroid = 65000 + i;
            //PictureCreater.instance.DestroyComponentByName(heroid.ToString());
            string name = "BossName" + heroid;
            if (GameObject.Find(name) != null)
            {
                DestroyImmediate(GameObject.Find(name));
            }
        }
        for (int i = 1; i <= 9; i++)
        {
            int heroid = 65000 + i;
            int j = PictureCreater.instance.CreateRole(heroid, "Model", 18, Color.red, 0, 0, 1, 2f, false, false, 1, 1.5f, 0.2f, "Model", 0, 10, 10, 2, 2, 0, 0, 1001, 1, 1002, 1, 0, 1, 1, 0, "");

            GameObject go = Instantiate(PictureCreater.instance.ListRolePicture[j].RoleObject) as GameObject;
            go.transform.parent = gameObject.transform;
            go.transform.localScale = new Vector3(500, 500, 500);
            go.transform.Rotate(new Vector3(0, 0, 0));
            //PictureCreater.instance.ListRolePicture[j].RoleObject.transform.Find("Role").transform.Rotate(new Vector3(0, 90f, 0));
            go.transform.Find("Role").transform.localRotation = new Quaternion(0, 0, 0, 0);
            go.transform.Find("Role").transform.Rotate(new Vector3(0, -90f, 0));
            go.SetActive(false);
            go.name = "BossName" + heroid;
            AssetBossList.Add(go);
        }
        //PictureCreater.instance.DestroyAllComponent();
    }

    IEnumerator CloneBossTomove()
    {
        int p = Random.Range(0, 9);
        for (int i = 0; i < AssetBossList.Count; i++)
        {
            AssetBossList[i].SetActive(false);
        }
        AssetBossList[p].SetActive(true);
        AssetBossList[p].transform.localPosition = new Vector3(10205, 10820, 93860);
        while (AssetBossList[p].transform.localPosition.x > 2705)//2280
        {
            AssetBossList[p].transform.localPosition = new Vector3(AssetBossList[p].transform.localPosition.x - 37.5f, 10820, 93860);
            yield return new WaitForSeconds(0.01f);
        }
        IsMoveScence = false;
        if (PlayerPrefs.GetFloat("ElectractySlider") != 0)
        {
            for (int i = 0; i < HeroTeam.Count; i++)
            {
                HeroTeam[i].RolePictureObject.GetComponent<Animator>().SetFloat("ft", 2);
                HeroTeam[i].RolePictureObject.GetComponent<Animator>().Play("attack");
                FightMotion fm2 = TextTranslator.instance.fightMotionDic[HeroTeam[i].RoleID * 10 + 1];
                PictureCreater.instance.PlayEffect(HeroTeam, i, fm2);

                if (HostageObject != null)
                {
                    HostageObject.GetComponent<Animator>().SetFloat("ft", 2);
                }
            }
        }

        if (PlayerPrefs.GetFloat("ElectractySlider") != 0) //耗电模式
        {
            LabelEffectAnimation(OnhookItemCode, OnhookItemNum, OnhookExp);//挂机奖励弹窗
        }

        yield return new WaitForSeconds(0.2f);
        AssetBossList[p].transform.Find("Role").GetComponent<Animator>().Play("dead");

        StartCoroutine(DestorySkillEffect());
        HoldOnLeftTimeLabel.text = CharacterRecorder.instance.HoldKillNum.ToString();
        KillScale.from = new Vector3(1f, 1f, 1f);
        KillScale.to = new Vector3(1.5f, 1.5f, 1.5f);
        KillScale.ResetToBeginning();
        KillScale.PlayForward();
        yield return new WaitForSeconds(1.4f);

        AssetBossList[p].SetActive(false);


        if (PlayerPrefs.GetFloat("ElectractySlider") != 0)
        {
            for (int i = 0; i < HeroTeam.Count; i++)
            {
                HeroTeam[i].RolePictureObject.GetComponent<Animator>().SetFloat("ft", 0);
                if (HostageObject != null)
                {
                    HostageObject.GetComponent<Animator>().SetFloat("ft", 0);
                }
            }
            IsMoveScence = true;
        }
    }


    IEnumerator CloneBossTomoveWhenElectractySlider()//省电模式
    {
        int p = Random.Range(0, 9);

        for (int i = 0; i < AssetBossList.Count; i++)
        {
            AssetBossList[i].SetActive(false);
        }
        AssetBossList[p].SetActive(true);
        AssetBossList[p].transform.localPosition = new Vector3(10205, 10820, 93860);
        AssetBossList[p].transform.Find("Role").GetComponent<Animator>().SetFloat("ft", 0);
        while (AssetBossList[p].transform.localPosition.x > 2705)//2280
        {
            AssetBossList[p].transform.localPosition = new Vector3(AssetBossList[p].transform.localPosition.x - 20f, 10820, 93860);
            yield return new WaitForSeconds(0.01f);
        }

        AssetBossList[p].transform.Find("Role").GetComponent<Animator>().SetFloat("ft", 2);

        for (int i = 0; i < HeroTeam.Count; i++)
        {
            HeroTeam[i].RolePictureObject.GetComponent<Animator>().Play("attack");
        }

        yield return new WaitForSeconds(0.2f);
        AssetBossList[p].transform.Find("Role").GetComponent<Animator>().Play("dead");
        StartCoroutine(DestorySkillEffect());

        HoldOnLeftTimeLabel.text = CharacterRecorder.instance.HoldKillNum.ToString();

        KillScale.from = new Vector3(1f, 1f, 1f);
        KillScale.to = new Vector3(1.5f, 1.5f, 1.5f);
        KillScale.ResetToBeginning();
        KillScale.PlayForward();
        yield return new WaitForSeconds(1.4f);

        //DestroyImmediate(BossClone);
        AssetBossList[p].SetActive(false);
    }



    IEnumerator DestorySkillEffect()
    {
        GameObject skilleffect = Instantiate(SkillEffect) as GameObject;
        skilleffect.transform.parent = SkillEffect.transform.parent;
        skilleffect.transform.localPosition = SkillEffect.transform.localPosition;
        skilleffect.transform.localScale = SkillEffect.transform.localScale;
        skilleffect.SetActive(true);
        yield return new WaitForSeconds(1f);
        DestroyImmediate(skilleffect);
    }

    IEnumerator SetScenceTomove()
    {
        while (true)
        {
            while (IsMoveScence)
            {
                zhujiemian1.transform.localPosition = new Vector3(zhujiemian1.transform.localPosition.x - 60, 0, 0);
                zhujiemian2.transform.localPosition = new Vector3(zhujiemian2.transform.localPosition.x - 60, 0, 0);
                zhujiemian3.transform.localPosition = new Vector3(zhujiemian3.transform.localPosition.x - 60, 0, 0);

                if (zhujiemian1.transform.localPosition.x <= -15705)//15705
                {
                    //zhujiemian1.transform.localPosition = new Vector3(15705, 0, 0);
                    if (zhujiemian2.transform.localPosition.x > 0)
                    {
                        zhujiemian1.transform.localPosition = new Vector3(zhujiemian2.transform.localPosition.x + 10470, 0, 0);
                    }
                    else if (zhujiemian3.transform.localPosition.x > 0)
                    {
                        zhujiemian1.transform.localPosition = new Vector3(zhujiemian3.transform.localPosition.x + 10470, 0, 0);
                    }
                    else
                    {
                        zhujiemian1.transform.localPosition = new Vector3(15705, 0, 0);
                    }
                }
                else if (zhujiemian2.transform.localPosition.x <= -15705)
                {
                    //zhujiemian2.transform.localPosition = new Vector3(15705, 0, 0);
                    if (zhujiemian1.transform.localPosition.x > 0)
                    {
                        zhujiemian2.transform.localPosition = new Vector3(zhujiemian1.transform.localPosition.x + 10470, 0, 0);
                    }
                    else if (zhujiemian3.transform.localPosition.x > 0)
                    {
                        zhujiemian2.transform.localPosition = new Vector3(zhujiemian3.transform.localPosition.x + 10470, 0, 0);
                    }
                    else
                    {
                        zhujiemian2.transform.localPosition = new Vector3(15705, 0, 0);
                    }
                }
                else if (zhujiemian3.transform.localPosition.x <= -15705)
                {
                    //zhujiemian3.transform.localPosition = new Vector3(15705, 0, 0);
                    if (zhujiemian1.transform.localPosition.x > 0)
                    {
                        zhujiemian3.transform.localPosition = new Vector3(zhujiemian1.transform.localPosition.x + 10470, 0, 0);
                    }
                    else if (zhujiemian2.transform.localPosition.x > 0)
                    {
                        zhujiemian3.transform.localPosition = new Vector3(zhujiemian2.transform.localPosition.x + 10470, 0, 0);
                    }
                    else
                    {
                        zhujiemian3.transform.localPosition = new Vector3(15705, 0, 0);
                    }
                }
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator SetHarborScenceTomove()
    {
        while (true)
        {
            while (IsMoveScence)
            {
                zhujiemian1.transform.localPosition = new Vector3(zhujiemian1.transform.localPosition.x - 60, 0, 0);
                zhujiemian2.transform.localPosition = new Vector3(zhujiemian2.transform.localPosition.x - 60, 0, 0);

                if (zhujiemian1.transform.localPosition.x <= -31410)
                {
                    if (zhujiemian2.transform.localPosition.x > -31410)
                    {
                        zhujiemian1.transform.localPosition = new Vector3(zhujiemian2.transform.localPosition.x + 31410, 0, 0);
                    }
                    else
                    {
                        zhujiemian1.transform.localPosition = new Vector3(31410, 0, 0);
                    }
                }
                else if (zhujiemian2.transform.localPosition.x <= -31410)
                {
                    if (zhujiemian1.transform.localPosition.x > -31410)
                    {
                        zhujiemian2.transform.localPosition = new Vector3(zhujiemian1.transform.localPosition.x + 31410, 0, 0);
                    }
                    else
                    {
                        zhujiemian2.transform.localPosition = new Vector3(31410, 0, 0);
                    }
                }
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator SetGiftAnimation()
    {
        YiWaiHuoDe01.SetActive(true);
        YiWaiHuoDe01.GetComponent<Animation>().Play("Main_Gift");
        yield return new WaitForSeconds(1.2f);
        YiWaiHuoDe01.GetComponent<Animation>().Stop("Main_Gift");
        YiWaiHuoDe01.SetActive(false);
        YiWaiHuoDe02.SetActive(false);
        YiWaiHuoDe02.SetActive(true);
    }

    void SetLightMap()
    {
        RenderSettings.fog = false;
        LightMapAsset lightAsset = Resources.Load("LightMapAsset/lightMapAsset_128") as LightMapAsset;
        int count = lightAsset.lightmapFar.Length;
        LightmapData[] lightmapDatas = new LightmapData[count];
        for (int i = 0; i < count; i++)
        {
            LightmapData lightmapData = new LightmapData();
            lightmapData.lightmapFar = lightAsset.lightmapFar[i];
            lightmapData.lightmapNear = lightAsset.lightmapNear[i];
            lightmapDatas[i] = lightmapData;
        }
        LightmapSettings.lightmaps = lightmapDatas;
    }

    void SetNameColor(bool isFirstEnterMain, GameObject go, UILabel NameColor, UILabel LabelLevel, UISprite SpriteJX, string Name, int ClassNumber, int Level, int Rank)
    {
        go.SetActive(true);
        LabelLevel.text = "Lv." + Level.ToString();
        SpriteJX.spriteName = "rank" + (Rank).ToString("00");
        TextTranslator.instance.SetHeroNameColor(NameColor, Name, ClassNumber);
        // SetHeroItemRedPoint(go, RoleRedPointType.HeroTabsRedPint);//主界面Models红点
        StartCoroutine(SetHeroItemRedPoint(isFirstEnterMain, go, RoleRedPointType.HeroTabsRedPint));//主界面Models红点
    }

    public void OpenMapWindow()
    {
        CharacterRecorder.instance.enterMapFromMain = true;
        UIManager.instance.OpenPanel("MapUiWindow", true);
    }

    public void SetTopContent(int _staminaOneTime, int _bloodOneTime)
    {
        TopContent tp = GameObject.Find("TopContent").GetComponent<TopContent>();
        tp.Reset();
        staminTime = _staminaOneTime;
        spriteTime = _bloodOneTime;
        CancelInvoke();
        InvokeRepeating("UpdateTime", 0, 1.0f);
    }
    void UpdateTime()
    {
        if (staminTime > 0)
        {
            staminTime -= 1;
        }
        else
        {
            staminTime = 300 - 1;
            if (CharacterRecorder.instance.staminaCap - CharacterRecorder.instance.stamina > 0)
            {
                CharacterRecorder.instance.AddStamina(1);
                if (GameObject.Find("MainWindow") != null)
                {
                    TopContent tp = GameObject.Find("TopContent").GetComponent<TopContent>();
                    tp.Reset();
                }
            }
        }
        if (spriteTime > 0)
        {
            spriteTime -= 1;
        }
        else
        {
            spriteTime = 900 - 1;
            if (CharacterRecorder.instance.spriteCap - CharacterRecorder.instance.sprite > 0)
            {
                CharacterRecorder.instance.sprite += 1;
                if (GameObject.Find("MainWindow") != null)
                {
                    TopContent tp = GameObject.Find("TopContent").GetComponent<TopContent>();
                    tp.Reset();
                }
            }
        }
    }
    //红点开启条件（传入坐标1-20）
    public void ShowPrompt(int PromptID, bool isOpen)
    {
        if (isOpen)
        {

            if (Prompt[PromptID] != null)
            {
                Prompt[PromptID].gameObject.SetActive(true);
                if (RedPointList.Count == 0)
                {
                    RedPointList.Add(PromptID + 1);
                }
                else
                {
                    bool isSame = false;
                    for (int i = 0; i < RedPointList.Count; i++)
                    {
                        if (PromptID + 1 == RedPointList[i])
                        {
                            isSame = true;
                        }
                    }
                    if (isSame == false)
                    {
                        RedPointList.Add(PromptID + 1);
                    }
                }
            }
        }
        else
        {
            if (Prompt[PromptID] != null)
            {
                Prompt[PromptID].gameObject.SetActive(false);
                for (int i = 0; i < RedPointList.Count; i++)
                {
                    if (PromptID + 1 == RedPointList[i])
                    {
                        RedPointList.Remove(PromptID + 1);
                        CharacterRecorder.instance.RedPointList.Remove(PromptID + 1);
                    }
                }
            }
        }
    }

    //功能解锁条件
    public void FunctionUnlock()
    {
        //Transform hideParentTr = BottomButton[0].transform.parent.parent;
        ////for (int i = 0; i < FunctionLock.Count; i++)
        //{
        //    FunctionLock[1].SetActive(false);

        //    if (ControlButtonOpen(BottomButton[6])&&CharacterRecorder.instance.lastGateID<=TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zhengfu).Level)
        //    {
        //        FunctionLock[6].SetActive(true);
        //        BottomButton[6].SetActive(true);
        //    }
        //    else
        //    {
        //        BottomButton[6].transform.parent = hideParentTr;
        //        FunctionLock[6].SetActive(false);
        //        BottomButton[6].SetActive(false);
        //    }

        //    if (ControlButtonOpen(BottomButton[5]) && CharacterRecorder.instance.lastGateID <= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zhanshu).Level)
        //    {
        //        FunctionLock[5].SetActive(true);
        //        BottomButton[5].SetActive(true);
        //    }
        //    else
        //    {
        //        BottomButton[5].transform.parent = hideParentTr;
        //        FunctionLock[5].SetActive(false);
        //        BottomButton[5].SetActive(false);
        //    }

        //    if (ControlButtonOpen(BottomButton[4]) && CharacterRecorder.instance.lastGateID <= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.qingbao).Level)
        //    {
        //        FunctionLock[4].SetActive(true);
        //        BottomButton[4].SetActive(true);
        //    }
        //    else
        //    {
        //        BottomButton[4].transform.parent = hideParentTr;
        //        BottomButton[4].SetActive(false);
        //        FunctionLock[4].SetActive(false);
        //    }

        //    if (ControlButtonOpen(BottomButton[3])&& CharacterRecorder.instance.lastGateID <= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.shiyanshi).Level)
        //    {
        //        FunctionLock[3].SetActive(true);
        //        BottomButton[3].SetActive(true);
        //    }
        //    else
        //    {
        //        BottomButton[3].transform.parent = hideParentTr;
        //        BottomButton[3].SetActive(false);
        //        FunctionLock[3].SetActive(false);
        //    }

        //    if (ControlButtonOpen(BottomButton[2]) && CharacterRecorder.instance.lastGateID <= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.shop).Level)
        //    {
        //        FunctionLock[2].SetActive(true);
        //        BottomButton[2].SetActive(true);
        //    }
        //    else
        //    {
        //        BottomButton[2].transform.parent = hideParentTr;
        //        BottomButton[2].SetActive(false);
        //        FunctionLock[2].SetActive(false);
        //    }

        //    if (ControlButtonOpen(BottomButton[1]) && CharacterRecorder.instance.lastGateID <= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.heroRank).Level)
        //    {
        //        FunctionLock[1].SetActive(true);
        //        BottomButton[1].SetActive(true);
        //    }
        //    else
        //    {
        //        BottomButton[1].transform.parent = hideParentTr;
        //        BottomButton[1].SetActive(false);
        //        FunctionLock[1].SetActive(false);
        //    }

        //    if (ControlButtonOpen(BottomButton[0]) && CharacterRecorder.instance.lastGateID <= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.juntuan).Level)
        //    {
        //        FunctionLock[0].SetActive(true);
        //        BottomButton[0].SetActive(true);
        //    }
        //    else
        //    {
        //        BottomButton[0].transform.parent = hideParentTr;
        //        BottomButton[0].SetActive(false);
        //        FunctionLock[0].SetActive(false);
        //    }
        //}


        if (CharacterRecorder.instance.lastGateID <= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.guozhan).Level)
        {
            FunctionLock[6].SetActive(true);
        }
        else
        {
            FunctionLock[6].SetActive(false);
        }

        if (CharacterRecorder.instance.lastGateID <= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zhanshu).Level)
        {
            FunctionLock[5].SetActive(true);
        }
        else
        {
            FunctionLock[5].SetActive(false);
        }

        if (CharacterRecorder.instance.lastGateID <= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.qingbao).Level)
        {
            FunctionLock[4].SetActive(true);
        }
        else
        {
            FunctionLock[4].SetActive(false);
        }

        if (CharacterRecorder.instance.lastGateID <= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.shiyanshi).Level)
        {
            FunctionLock[3].SetActive(true);
        }
        else
        {
            FunctionLock[3].SetActive(false);
        }

        if (CharacterRecorder.instance.lastGateID <= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.shop).Level)
        {
            FunctionLock[2].SetActive(true);
        }
        else
        {
            FunctionLock[2].SetActive(false);
        }

        if (CharacterRecorder.instance.lastGateID <= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.heroRank).Level)
        {
            FunctionLock[1].SetActive(true);
        }
        else
        {
            FunctionLock[1].SetActive(false);
        }

        if (CharacterRecorder.instance.lastGateID <= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.juntuan).Level)
        {
            FunctionLock[0].SetActive(true);
        }
        else
        {
            FunctionLock[0].SetActive(false);
        }
    }



    #region 角色和装备 红点判断
    IEnumerator DelayShowRoleRedPoint()
    {
        float _waiteTime = 0.0f;
        if (CharacterRecorder.instance.enterMainWindowFirst)
        {
            _waiteTime = 1.6f;//1.5f;
            Invoke("SortHeroListByForce", 1.5f);//战力降序排序
            CharacterRecorder.instance.enterMainWindowFirst = false;
        }
        yield return new WaitForSeconds(_waiteTime);
        //ShowPrompt(9, GetRoleButtonRedPointState());//角色小红点 
        //ShowPrompt(10, GetEquipButtonRedPointState());//装备小红点 
        //SetLegionRedPoint();////军团小红点
    }
    //战力降序排序
    void SortHeroListByForce()
    {
        int listSize = CharacterRecorder.instance.ownedHeroList.size;
        for (int i = 0; i < listSize; i++)
        {
            for (var j = listSize - 1; j > i; j--)
            {
                Hero heroA = CharacterRecorder.instance.ownedHeroList[i];
                Hero heroB = CharacterRecorder.instance.ownedHeroList[j];
                if (heroA.force < heroB.force)
                {
                    var temp = CharacterRecorder.instance.ownedHeroList[i];
                    CharacterRecorder.instance.ownedHeroList[i] = CharacterRecorder.instance.ownedHeroList[j];
                    CharacterRecorder.instance.ownedHeroList[j] = temp;
                }
            }
        }
    }
    private bool GetRoleButtonRedPointState()
    {
        //for (int i = 0; i < CharacterRecorder.instance.ownedHeroList.size; i++)
        int usefulCount = CharacterRecorder.instance.ownedHeroList.size >= 6 ? 6 : CharacterRecorder.instance.ownedHeroList.size;
        for (int i = 0; i < usefulCount; i++) //角色小红点 排行前六名的角色
        {
            Hero mHero = CharacterRecorder.instance.ownedHeroList[i];
            if (GetHeroRedPointStateOfHeroTabs(mHero))// || GetHeroRedPointStateOfEquip(mHero))//去掉装备红点
            {
                return true;
            }
        }
        return false;
    }
    private bool GetEquipButtonRedPointState()
    {
        //for (int i = 0; i < CharacterRecorder.instance.ownedHeroList.size; i++)
        int usefulCount = CharacterRecorder.instance.ownedHeroList.size >= 6 ? 6 : CharacterRecorder.instance.ownedHeroList.size;
        for (int i = 0; i < usefulCount; i++) //装备小红点 排行前六名的角色
        {
            Hero mHero = CharacterRecorder.instance.ownedHeroList[i];
            if (GetHeroRedPointStateOfEquip(mHero))
            {
                return true;
            }
        }
        return false;
    }
    private void SetHeroItemRedPoint(Hero mHero)
    {
        GameObject _RedPoint = gameObject.transform.FindChild("RedPoint").gameObject;
        if (GetHeroRedPointStateOfHeroTabs(mHero) || GetHeroRedPointStateOfEquip(mHero))
        {
            _RedPoint.SetActive(true);
        }
        else
        {
            _RedPoint.SetActive(false);
        }
    }
    bool GetHeroRedPointStateOfHeroTabs(Hero _curHero)
    {
        //升品
        RoleClassUp rcu = TextTranslator.instance.GetRoleClassUpInfoByID(_curHero.cardID, _curHero.classNumber);
        //Debug.Log("cardId...." + _curHero.cardID + "...level..." + _curHero.level);
        //Debug.Log(rcu.Levelcap);
        //Debug.Log(GetShengPinOneContionState(rcu.NeedItemList));
        if (_curHero.level > rcu.Levelcap && GetShengPinOneContionState(rcu.NeedItemList) == true)
        {
            return true;
        }
        //军衔
        RoleBreach rb = TextTranslator.instance.GetRoleBreachByID(_curHero.cardID, _curHero.rank);
        int item1Count = TextTranslator.instance.GetItemCountByID(10102);
        //Debug.Log("cardID..." + _curHero.cardID + "...rank..." + _curHero.rank + ".." + rb);
        int item2Count = TextTranslator.instance.GetItemCountByID(rb.roleId + 10000);
        if (CharacterRecorder.instance.lastGateID > 10008 && _curHero.level >= rb.levelCup && item1Count >= rb.stoneNeed && item2Count >= rb.debrisNum)
        {
            return true;
        }
        return false;
    }
    bool GetShengPinOneContionState(List<Item> classUpList)
    {
        for (int i = 0; i < classUpList.Count; i++)
        {
            int bagItemCount1 = TextTranslator.instance.GetItemCountByID(classUpList[i].itemCode);
            if (bagItemCount1 < classUpList[i].itemCount)
            {
                return false;
            }
        }
        return true;
    }
    bool GetHeroRedPointStateOfEquip(Hero mHero)
    {
        foreach (var _OneEquipInfo in mHero.equipList)
        {
            if (_OneEquipInfo.equipPosition < 5)
            {
                //Debug.LogError(_OneEquipInfo.equipColorNum + "..." + IsAdvanceState(_OneEquipInfo.equipLevel, _OneEquipInfo.equipColorNum));
                int _EquipLevel = _OneEquipInfo.equipLevel;
                int _EquipColorNum = _OneEquipInfo.equipColorNum;
                if (IsAdvanceState(_EquipLevel, _EquipColorNum) && IsEnoughToAdvance(_OneEquipInfo, mHero) && IsAdvanceMaterailEnough(_OneEquipInfo, mHero))//升品
                {
                    return true;
                }
                else if (IsAdvanceState(_EquipLevel, _EquipColorNum) == false && IsEnoughToUpGrade(_OneEquipInfo, mHero) && _OneEquipInfo.equipLevel < CharacterRecorder.instance.level)//升级
                {
                    return true;
                }
            }
        }
        return false;
    }
    bool IsEnoughToUpGrade(Hero.EquipInfo _OneEquipInfo, Hero mHero)
    {
        HeroInfo mHeroInfo = TextTranslator.instance.GetHeroInfoByHeroID(mHero.cardID);
        int needMoney = TextTranslator.instance.GetEquipStrongCostByID(_OneEquipInfo.equipLevel, mHeroInfo.heroRarity, _OneEquipInfo.equipPosition);
        if (CharacterRecorder.instance.gold >= needMoney)
        {
            return true;
        }
        return false;
    }
    bool IsAdvanceMaterailEnough(Hero.EquipInfo _OneEquipInfo, Hero mHero)
    {
        HeroInfo mHeroInfo = TextTranslator.instance.GetHeroInfoByHeroID(mHero.cardID);
        EquipStrongQuality esq = TextTranslator.instance.GetEquipStrongQualityByIDAndColor(_OneEquipInfo.equipCode, _OneEquipInfo.equipColorNum, mHeroInfo.heroRace);
        for (int i = 0; i < esq.materialItemList.size; i++)
        {
            int itemCode = esq.materialItemList[i].itemCode;
            int itemCountInBag = TextTranslator.instance.GetItemCountByID(itemCode);
            if (esq.materialItemList[i].itemCount > itemCountInBag)
            {
                return false;
            }
        }
        return true;
    }
    bool IsEnoughToAdvance(Hero.EquipInfo _OneEquipInfo, Hero mHero)
    {
        int _EquipPosition = _OneEquipInfo.equipPosition;
        HeroInfo mHeroInfo = TextTranslator.instance.GetHeroInfoByHeroID(mHero.cardID);
        EquipStrongQuality esq = TextTranslator.instance.GetEquipStrongQualityByIDAndColor(mHero.equipList[_EquipPosition - 1].equipCode, mHero.equipList[_EquipPosition - 1].equipColorNum, mHeroInfo.heroRace);
        if (CharacterRecorder.instance.gold >= esq.Money)
        {
            return true;
        }
        return false;
    }
    bool IsAdvanceState(int _EquipLevel, int _EquipColorNum)
    {
        if (_EquipColorNum * 5 == _EquipLevel)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    #endregion

    #region 主界面Models红点
    IEnumerator SetHeroItemRedPoint(bool isFirstEnterMain, GameObject go, RoleRedPointType _RoleRedPointType)
    {
        //Debug.LogError("Count..." + ReccordHeroCardIdDic.Count + "...go.name..." + go.name);
        //Debug.LogError("...cardId...." + ReccordHeroCardIdDic[1].cardID);
        Hero mHero = ReccordHeroCardIdDic[int.Parse(go.name[4].ToString())];
        if (mHero == null)
        {
            yield return new WaitForSeconds(0.1f);
            StopCoroutine(SetHeroItemRedPoint(isFirstEnterMain, go, _RoleRedPointType));
        }
        if (isFirstEnterMain)
        {
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            yield return new WaitForSeconds(0.0f);
        }
        GameObject _RedPoint = go.transform.FindChild("RedPoint").gameObject;
        switch (_RoleRedPointType)
        {
            case RoleRedPointType.HeroTabsRedPint:
                if (GetHeroRedPointStateOfHeroTabs(mHero))
                {
                    _RedPoint.SetActive(true);
                }
                else
                {
                    _RedPoint.SetActive(false);
                }
                break;
            case RoleRedPointType.EquipAdvanceRedPint:
                if (GetHeroRedPointStateOfEquip(mHero))
                {
                    _RedPoint.SetActive(true);
                }
                else
                {

                    _RedPoint.SetActive(false);
                }
                break;
            case RoleRedPointType.Both:
                if (GetHeroRedPointStateOfHeroTabs(mHero) || GetHeroRedPointStateOfEquip(mHero))
                {
                    _RedPoint.SetActive(true);
                }
                else
                {

                    _RedPoint.SetActive(false);
                }
                break;
        }
    }
    #endregion

    #region 新聊天按钮信息
    //public void SetNewChatButtonInfo()
    //{
    //    //ChatItemData _ChatItemDataNew = GetLastNewChatDataIsNoTeamCopy();//非组队
    //    //ChatItemData _ChatItemDataTeam = GetLastNewChatDataIsTeamCopy();//最新组队
    //    //SetNewChatButtonInfo(_ChatItemDataNew,1);
    //    //SetNewChatButtonInfo(_ChatItemDataTeam,2);
    //}


    public void SetChatSpriteColor()
    {
        UISprite TopChatButtonSprite = TopChatButton.GetComponent<UISprite>();
        TopChatButtonSprite.spriteName = "noticedi1";//10.12+
        //switch (CharacterRecorder.instance.ButtonChannel)  //10.12
        //{
        //    case 1: TopChatButtonSprite.spriteName = "noticedi4";
        //        break;
        //    case 2: TopChatButtonSprite.spriteName = "noticedi1";
        //        break;
        //    case 3: TopChatButtonSprite.spriteName = "noticedi3";
        //        break;
        //    case 4: TopChatButtonSprite.spriteName = "noticedi2";
        //        break;
        //}
    }
    public void SetNewChatButtonInfo()
    {
        Transform TopNametr = TopChatButton.transform.Find("ScrollView/NameLabel");
        UILabel TopNameLabel = TopNametr.GetComponent<UILabel>();
        UILabel TopTypeLabel = TopNametr.FindChild("TypeLabel").GetComponent<UILabel>();
        UISprite TopTypeFrame = TopNametr.FindChild("TypeFrame").GetComponent<UISprite>();
        UISprite TopChatButtonSprite = TopChatButton.GetComponent<UISprite>();

        Transform Nametr = ChatButton.transform.FindChild("NameLabel");
        UILabel NameLabel = Nametr.GetComponent<UILabel>();
        UILabel TypeLabel = Nametr.FindChild("TypeLabel").GetComponent<UILabel>();
        UISprite TypeFrame = Nametr.FindChild("TypeFrame").GetComponent<UISprite>();
        UISprite ChatButtonSprite = ChatButton.GetComponent<UISprite>();
        if (ObscuredPrefs.GetString("LastChatItemInfo" + CharacterRecorder.instance.userId.ToString()) == "")//聊天
        {
            TopNameLabel.text = "";
            TopTypeLabel.text = "";
            TopNametr.gameObject.SetActive(false);
        }
        else
        {
            string Getstring = "";
            Getstring = ObscuredPrefs.GetString("LastChatItemInfo" + CharacterRecorder.instance.userId.ToString());
            string[] dataSplit = Getstring.Split(';');
            string[] dataSplit2 = dataSplit[3].Split('$');
            ChatItemData _ChatItemData = new ChatItemData(int.Parse(dataSplit[0]), dataSplit[1], dataSplit[2], int.Parse(dataSplit2[0]), int.Parse(dataSplit2[1]), int.Parse(dataSplit2[2]), int.Parse(dataSplit2[3]), int.Parse(dataSplit2[4]), int.Parse(dataSplit2[5]));
            TopNametr.gameObject.SetActive(true);

            if (_ChatItemData.nationId != null)
            {
                if (_ChatItemData.nationId > 0 && _ChatItemData.nationId <= TextTranslator.instance.NationList.size)
                {
                    string OfficeName = TextTranslator.instance.GetNationByID(_ChatItemData.nationId).OfficeName;//ff9907
                    TopNameLabel.text = (_ChatItemData.name != "" ? "[ff2652]" + OfficeName + "[-] " + "[cc6633]" + _ChatItemData.name + "[-]" : "") + "[999999]" + _ChatItemData.textWords + "[-]";
                }
                else
                {
                    TopNameLabel.text = (_ChatItemData.name != "" ? "[cc6633]" + _ChatItemData.name + "[-]" : "") + "[999999]" + _ChatItemData.textWords + "[-]";
                }
            }

            switch (_ChatItemData.channel)
            {
                case 1:
                    TopTypeLabel.text = "[ffdb50]系统[-]";
                    TopTypeFrame.spriteName = "frameRectangle4";
                    break;
                case 2:
                    TopTypeLabel.text = "[50e8ff]世界[-]";
                    TopTypeFrame.spriteName = "frameRectangle";
                    break;
                case 3:
                    TopTypeLabel.text = "[3fff4d]军团[-]";
                    TopTypeFrame.spriteName = "frameRectangle3";
                    break;
                case 4:
                    TopTypeLabel.text = "[ff2652]国家[-]";
                    //ChatButtonSprite.spriteName = "noticedi2";
                    TopTypeFrame.spriteName = "frameRectangle2";
                    //NameLabel.text = (_ChatItemData.name != "" ? "[ff2652]" + _ChatItemData.name + "[-]" : "") + _ChatItemData.textWords;
                    break;
                default:
                    break;
            }
        }

        if (ObscuredPrefs.GetString("LastChatItemInfoOnTeam") == "")//组队或军团
        {
            NameLabel.text = "";
            TypeLabel.text = "";
            Nametr.gameObject.SetActive(false);
        }
        else
        {
            int legionId = 0;
            int RoomID = -1;
            string Getstring = "";
            Getstring = ObscuredPrefs.GetString("LastChatItemInfoOnTeam");
            string[] dataSplit = Getstring.Split(';');
            string[] dataSplit2 = dataSplit[3].Split('$');
            ChatItemData _ChatItemData = new ChatItemData(int.Parse(dataSplit[0]), dataSplit[1], dataSplit[2], int.Parse(dataSplit2[0]), int.Parse(dataSplit2[1]), int.Parse(dataSplit2[2]), int.Parse(dataSplit2[3]), int.Parse(dataSplit2[4]), int.Parse(dataSplit2[5]));
            GameObject applayButton = ChatButton.transform.FindChild("ApplayButton").gameObject;
            if (_ChatItemData.textWords.Contains("军团ID:"))//是否为军团长招募
            {
                string[] legionIdtr = _ChatItemData.textWords.Split(':');
                legionId = int.Parse(legionIdtr[1]);
                applayButton.SetActive(true);
            }
            else if (_ChatItemData.textWords.Contains("房间号")) //此处符号判断是否为组队邀请
            {
                Debug.Log("进入组队");
                if (_ChatItemData.textWords.Contains("："))
                {
                    int num = 0;
                    string[] Roomstr = _ChatItemData.textWords.Split('：');
                    ASCIIEncoding ascii = new ASCIIEncoding();
                    byte[] bytestr = ascii.GetBytes(Roomstr[1]);
                    for (int j = 0; j < bytestr.Length; j++)
                    {
                        if (bytestr[j] < 48 || bytestr[j] > 57)
                        {
                            num++;
                            break;
                        }
                    }
                    if (num == 0)
                    {
                        Debug.Log("进入房间号" + Roomstr[1]);
                        RoomID = int.Parse(Roomstr[1]);
                        applayButton.SetActive(true);
                    }
                }
            }
            else
            {
                applayButton.SetActive(false);
            }

            if (legionId != 0 || RoomID != -1)
            {
                Nametr.gameObject.SetActive(true);
                //NameLabel.text = (_ChatItemData.name != "" ? "[ff9907]" + _ChatItemData.name + "[-]" : "") + _ChatItemData.textWords;

                if (_ChatItemData.nationId != null)
                {
                    if (_ChatItemData.nationId > 0 && _ChatItemData.nationId <= TextTranslator.instance.NationList.size)
                    {
                        string OfficeName = TextTranslator.instance.GetNationByID(_ChatItemData.nationId).OfficeName;
                        NameLabel.text = (_ChatItemData.name != "" ? "[ff2652]" + OfficeName + "[-] " + "[cc6633]" + _ChatItemData.name + "[-]" : "") + "[999999]" + _ChatItemData.textWords + "[-]";
                    }
                    else
                    {
                        NameLabel.text = (_ChatItemData.name != "" ? "[cc6633]" + _ChatItemData.name + "[-]" : "") + "[999999]" + _ChatItemData.textWords + "[-]";
                    }
                }

                switch (1)
                {
                    case 1:
                        TypeLabel.text = "[ffdb50]系统[-]";
                        TypeFrame.spriteName = "frameRectangle4";
                        break;
                    case 2:
                        TypeLabel.text = "[50e8ff]世界[-]";
                        TypeFrame.spriteName = "frameRectangle";
                        break;
                    case 3:
                        TypeLabel.text = "[3fff4d]军团[-]";
                        TypeFrame.spriteName = "frameRectangle3";
                        break;
                    case 4:
                        TypeLabel.text = "[ff2652]国家[-]";
                        //ChatButtonSprite.spriteName = "noticedi2";
                        TypeFrame.spriteName = "frameRectangle2";
                        //NameLabel.text = (_ChatItemData.name != "" ? "[ff2652]" + _ChatItemData.name + "[-]" : "") + _ChatItemData.textWords;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                Debug.Log("组队聊天窗口中非组队或军团邀请,进入表示错误");
            }

            UIEventListener.Get(applayButton).onClick = delegate(GameObject go)
            {
                if (legionId != 0 && legionId == CharacterRecorder.instance.legionID)
                {
                    UIManager.instance.OpenPromptWindow("已是此军团成员", PromptWindow.PromptType.Hint, null, null);
                }
                else if (legionId != 0)
                {
                    if (CharacterRecorder.instance.lastGateID > TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.juntuan).Level)
                    {
                        NetworkHandler.instance.SendProcess("8008#" + legionId + ";");
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("通关" + (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.heroRank).Level % 10000) + "开放军团", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                if (RoomID != -1 && GameObject.Find("TeamInvitationWindow") == null)
                {
                    NetworkHandler.instance.SendProcess("6108#;");
                    NetworkHandler.instance.SendProcess("6109#" + RoomID + ";");
                }
            };
        }
    }

    //ChatItemData GetLastNewChatDataByChanel(List<int> openChanelList)
    //{
    //    for (int i = TextTranslator.instance.ChatItemDataList.size - 1; i >= 0; i--)
    //    {
    //        for (int j = 0; j < openChanelList.Count; j++)
    //        {
    //            if (TextTranslator.instance.ChatItemDataList[i].channel == openChanelList[j])
    //            {
    //                return TextTranslator.instance.ChatItemDataList[i];
    //            }
    //        }
    //    }
    //    return null;
    //}

    ChatItemData GetLastNewChatDataIsNoTeamCopy() //yy
    {
        for (int i = TextTranslator.instance.ChatItemDataList.size - 1; i >= 0; i--)
        {
            int num = 0;
            if (TextTranslator.instance.ChatItemDataList[i].textWords.Contains("军团ID:"))//是否为军团长招募
            {
                num = 1;
            }
            else if (TextTranslator.instance.ChatItemDataList[i].textWords.Contains("房间号")) //此处符号判断是否为组队邀请
            {
                if (TextTranslator.instance.ChatItemDataList[i].textWords.Contains("："))
                {
                    num = 1;
                }
            }
            if (num == 0)
            {
                return TextTranslator.instance.ChatItemDataList[i];
            }
        }
        return null;
    }

    ChatItemData GetLastNewChatDataIsTeamCopy() //yy
    {
        for (int i = TextTranslator.instance.ChatItemDataList.size - 1; i >= 0; i--)
        {
            if (TextTranslator.instance.ChatItemDataList[i].textWords.Contains("军团ID:"))//是否为军团长招募
            {
                return TextTranslator.instance.ChatItemDataList[i];
            }
            else if (TextTranslator.instance.ChatItemDataList[i].textWords.Contains("房间号")) //此处符号判断是否为组队邀请
            {
                if (TextTranslator.instance.ChatItemDataList[i].textWords.Contains("："))
                {
                    return TextTranslator.instance.ChatItemDataList[i];
                }
            }
        }
        return null;
    }
    #endregion

    #region 活动是否结束
    void ActivityIsOpen()
    {
        if (CharacterRecorder.instance.ActivityTime == 0)//大放送
        {
            //TopButton[1].transform.parent = GameObject.Find("ActivityButton").transform;
            TopButton[1].SetActive(false);
            TopButton[2].SetActive(false);
        }

        //if (CharacterRecorder.instance.ActivityTime - (86400 * 2) <= 0) //战力排行
        //{
        //    //TopButton[2].transform.parent = GameObject.Find("ActivityButton").transform;
        //    TopButton[2].SetActive(false);
        //}

        UiGrid.GetComponent<UIGrid>().Reposition();
    }
    #endregion

    #region 登录签到活动是否关闭
    public void LoginSignIsOpen()
    {
        if (CharacterRecorder.instance.IsOpenloginSign == false)
        {
            //TopButton[3].transform.parent = GameObject.Find("ActivityButton").transform;
            TopButton[3].SetActive(false);
            UiGrid.GetComponent<UIGrid>().Reposition();
        }
    }
    #endregion

    #region 世界boss开启
    public void WorldBossIsOpen()
    {
        if (PlayerPrefs.GetInt("WorldBossIsOpen") == 0)
        {
            //TopButton[0].transform.parent = GameObject.Find("ActivityButton").transform;
            TopButton[0].SetActive(false);

        }
        else if (PlayerPrefs.GetInt("WorldBossIsOpen") == 1)
        {
            TopButton[0].SetActive(true);
        }
        UiGrid.GetComponent<UIGrid>().Reposition();
    }
    #endregion

    #region 抽红将开启
    public void RedHeroIsOpen()
    {
        if (PlayerPrefs.GetInt("RedHeroIsOpen") == 0)
        {
            TopButton[9].SetActive(false);
        }
        else if (PlayerPrefs.GetInt("RedHeroIsOpen") == 1)
        {
            TopButton[9].SetActive(true);

        }
        UiGrid.GetComponent<UIGrid>().Reposition();
    }
    #endregion

    #region 组队前往按钮判断
    public void JoinTeamCondition(TeamBrowseItemDate _oneTeamBrowsItemDate)//查找组队信息
    {
        if (CharacterRecorder.instance.lastGateID > TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zuduifuben).Level)
        {
            StartCoroutine(StatyTime(_oneTeamBrowsItemDate));
        }
        else
        {
            UIManager.instance.OpenPromptWindow("通过" + (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.heroRank).Level % 10000) + "关解锁组队副本", PromptWindow.PromptType.Hint, null, null);
        }
    }
    IEnumerator StatyTime(TeamBrowseItemDate _oneTeamBrowsItemDate)
    {
        Levellimit = new int[] { 0, 20, 30, 40, 50, 60, 70 };
        yield return new WaitForSeconds(0.4f);
        string[] trcSplit = _oneTeamBrowsItemDate.condition1.Split('-');

        int OpenLevel = 0;
        for (int i = 0; i < TextTranslator.instance.TeamGateList.size; i++)
        {
            if (TextTranslator.instance.TeamGateList[i].GroupID == _oneTeamBrowsItemDate.copyNumber)
            {
                Debug.Log("关卡开放等级" + TextTranslator.instance.TeamGateList[i].NeedLevel);
                OpenLevel = TextTranslator.instance.TeamGateList[i].NeedLevel;
                break;
            }
        }
        Debug.Log("此队伍需要的等级" + Levellimit[int.Parse(_oneTeamBrowsItemDate.condition3)]);
        Debug.Log("帮打时间" + CharacterRecorder.instance.TeamHelpTime);

        if (CharacterRecorder.instance.level < Levellimit[int.Parse(_oneTeamBrowsItemDate.condition3)])
        {
            UIManager.instance.OpenPromptWindow("房间等级限制，无法加入", PromptWindow.PromptType.Hint, null, null);
        }
        else if (trcSplit[0] != "0")
        {
            if (trcSplit[0] == "2")
            {
                if (!CharacterRecorder.instance.MyFriendUIDList.Contains(int.Parse(trcSplit[1])))
                {
                    UIManager.instance.OpenPromptWindow("仅限队长好友可加入", PromptWindow.PromptType.Hint, null, null);
                }
            }
            else
            {
                if (trcSplit[1] != "0" && CharacterRecorder.instance.legionID.ToString() != trcSplit[1])
                {
                    UIManager.instance.OpenPromptWindow("仅限队长同军团成员可加入", PromptWindow.PromptType.Hint, null, null);
                }
            }
        }
        else if (CharacterRecorder.instance.TeamHelpTime > 0 && CharacterRecorder.instance.TeamFightNum == 0)
        {
            UIManager.instance.OpenPromptWindow("还在CD时间", PromptWindow.PromptType.Hint, null, null);
        }
        else if (CharacterRecorder.instance.level < OpenLevel)
        {
            UIManager.instance.OpenPromptWindow("等级不足，尚未解锁该难度", PromptWindow.PromptType.Hint, null, null);
        }
        else if (_oneTeamBrowsItemDate.teamstate == 1)
        {
            UIManager.instance.OpenPromptWindow("副本进行中", PromptWindow.PromptType.Hint, null, null);
        }
        else
        {
            NetworkHandler.instance.SendProcess("6103#" + _oneTeamBrowsItemDate.teamId + ";");
        }
    }
    #endregion

    #region 首冲界面是否开启
    public void FirstRechargeWindowIsOpen()
    {
        if (CharacterRecorder.instance.BuyGiftBag != null)
        {
            if (CharacterRecorder.instance.BuyGiftBag[0] == "1")
            {
                //TopButton[4].transform.parent = GameObject.Find("ActivityButton").transform;
                TopButton[4].SetActive(false);
                //UiGrid.GetComponent<UIGrid>().Reposition();
                FourchooseoneWindowIsOpen();
            }
        }
    }
    #endregion

    #region 商城红点
    void ShopMallRedPoint()
    {
        if (PlayerPrefs.GetInt("ShopBuy", 0) == 1)
        {
            CharacterRecorder.instance.SetRedPoint(54, true);
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(54, false);
        }
    }
    #endregion

    #region 四选一界面是否开启
    public void FourchooseoneWindowIsOpen()
    {
        //Debug.LogError("TenCaChaNumber "+CharacterRecorder.instance.TenCaChaNumber);
        if (CharacterRecorder.instance.TenCaChaNumber == 0 & CharacterRecorder.instance.ActivityRewardIsGet == false)
        {
            TopButton[11].SetActive(true);
            UiGrid.GetComponent<UIGrid>().Reposition();
        }
        else
        {
            TopButton[11].SetActive(false);
            UiGrid.GetComponent<UIGrid>().Reposition(); //kino
            GrandTotalWindowIsOpen();
        }
    }
    #endregion

    #region 累计福利界面是否开启
    public void GrandTotalWindowIsOpen()
    {
        if (CharacterRecorder.instance.BuyGiftBag[1] == "0")
        {
            TopButton[12].SetActive(true);
            UiGrid.GetComponent<UIGrid>().Reposition();
            if(CharacterRecorder.instance.Vip >= 2)
            {
                TopButton[12].transform.Find("RedPoint").gameObject.SetActive(true);
            }
        }
        else 
        {
            TopButton[12].SetActive(false);
            UiGrid.GetComponent<UIGrid>().Reposition();
        }
    }
    #endregion

    #region 问卷调查界面是否开启
    public void QuestionWindowIsOpen()
    {
        if (PlayerPrefs.GetInt("QuestionState_" + PlayerPrefs.GetString("ServerID") + "_" + PlayerPrefs.GetInt("UserID")) == 1 || CharacterRecorder.instance.lunaGem < 1000)
        {
            QuestionButton.SetActive(false);
        }
        else
        {
            //if (PlayerPrefs.GetInt("QuestionState_" + PlayerPrefs.GetString("ServerID") + "_" + PlayerPrefs.GetInt("UserID")) == 0 && CharacterRecorder.instance.lunaGem >= 1000)
            //{
            //    ShowPrompt(28, true);
            //    QuestionButton.SetActive(true);
            //}
        }
    }

    IEnumerator StaySomeTime()
    {
        yield return new WaitForSeconds(1f);
        if (GameObject.Find("LoadingWindow") == null)
        {
            UIManager.instance.OpenSinglePanel("QuestionWindow", false);
            yield return new WaitForSeconds(1f);
            UIManager.instance.OpenPromptWindow("2280钻石可以10连抽", PromptWindow.PromptType.Hint, null, null);
        }
    }
    #endregion

    #region 版本控制
    public bool ControlButtonOpen(GameObject button)//true打开，false关闭
    {
        int Features = 0;
        string buttonName = button.name;
        switch (buttonName)
        {
            case "ConquerButton": Features = 3;
                break;
            case "ChallengeButton": Features = 4;
                break;
            case "TaskButton": Features = 5;
                break;
            case "StoreButton": Features = 6;
                break;
            case "HeroMapButton": Features = 7;
                break;
            case "FriendButton": Features = 8;
                break;
            case "MailButton": Features = 9;
                break;
            case "HelperButton": Features = 10;
                break;
        }

        if (Features != 0)
        {
            ControlGateOpen CG = TextTranslator.instance.GetControlGateOpenByID(Features);
            if (CG.GateID < CharacterRecorder.instance.lastGateID)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return true;
        }
    }
    #endregion

    #region 控制下面开关开放
    public void ControlAllButtonOpen()
    {
        HeroMapButtonIsOpen();
        FriendButtonIsOpen();
        MailButtonIsOpen();
        HelperButtonIsOpen();
        ChallengeButtonIsOpen();
        TaskButtonIsOpen();
        StoreButtonIsOpen();
    }
    #endregion

    #region 图鉴是否开放
    public void HeroMapButtonIsOpen()
    {
        if (ControlButtonOpen(HeroMapButton) == false)
        {
            HeroMapButton.SetActive(false);
        }
        else
        {
            HeroMapButton.SetActive(true);
        }
    }
    #endregion

    #region 好友是否开放
    public void FriendButtonIsOpen()
    {
        if (ControlButtonOpen(FriendButton) == false)
        {
            FriendButton.SetActive(false);
        }
        else
        {
            FriendButton.SetActive(true);
        }
    }
    #endregion

    #region 邮件是否开放
    public void MailButtonIsOpen()
    {
        if (ControlButtonOpen(MailButton) == false)
        {
            MailButton.SetActive(false);
        }
        else
        {
            MailButton.SetActive(true);
        }
    }
    #endregion

    #region 小贴士是否开放
    private void HelperButtonIsOpen()
    {
        Transform parent = HelperButton.transform.parent;
        if (ControlButtonOpen(HelperButton) == false)
        {
            HelperButton.transform.parent = parent.parent;
            HelperButton.SetActive(false);
            parent.GetComponent<UIGrid>().Reposition();
        }
        else
        {
            HelperButton.SetActive(true);
        }
    }
    #endregion

    #region 冲突是否开放
    public void ChallengeButtonIsOpen()
    {
        if (ControlButtonOpen(BottomButton[7]) == false)
        {
            BottomButton[7].SetActive(false);
        }
        else
        {
            BottomButton[7].SetActive(true);
        }
    }
    #endregion

    #region 任务是否开放
    public void TaskButtonIsOpen()
    {
        if (ControlButtonOpen(TaskButton) == false)
        {
            //TaskButton.transform.parent = TaskButton.transform.parent.parent;
            TaskButton.SetActive(false);
        }
        else
        {
            TaskButton.SetActive(true);
        }
    }
    #endregion

    #region 商城是否开放
    public void StoreButtonIsOpen()
    {
        if (ControlButtonOpen(StoreButton) == false)
        {
            //TaskButton.transform.parent = TaskButton.transform.parent.parent;
            StoreButton.SetActive(false);
        }
        else
        {
            StoreButton.SetActive(true);
        }
    }
    #endregion

    #region 第一次加载红点
    public void AllRedPointInfo(string dataSplit)
    {
        CharacterRecorder.instance.RedPointList.Clear();
        string[] itemStr = dataSplit.Split('$');
        int PromptID = 0;
        for (int i = 0; i < dataSplit.Length - 1; i++)
        {
            switch (i)
            {
                case 0:
                    PromptID = 13;
                    SetPromptInfo(0, 13, itemStr[i]);//任务
                    break;
                case 1:
                    PromptID = 1;
                    SetPromptInfo(1, 1, itemStr[i]);//大放送
                    break;
                case 2:
                    PromptID = 15;
                    if (!CharacterRecorder.instance.GetRedPoint(21))
                    {
                        CharacterRecorder.instance.SetRedPoint(21, int.Parse(itemStr[i]) == 1 ? true : false);
                        SetPromptInfo(2, 15, itemStr[i]);//竞技场
                    }
                    else
                    {
                        UpdateRedPoint(21);
                        CharacterRecorder.instance.SetRedPoint(2, int.Parse(itemStr[i]) == 1 ? true : false);
                    }
                    break;
                case 3:
                    PromptID = 15;
                    if (!CharacterRecorder.instance.GetRedPoint(21))
                    {
                        CharacterRecorder.instance.SetRedPoint(21, int.Parse(itemStr[i]) == 1 ? true : false);
                        SetPromptInfo(3, 15, itemStr[i]);//竞技场
                    }
                    else
                    {
                        UpdateRedPoint(21);
                        CharacterRecorder.instance.SetRedPoint(3, int.Parse(itemStr[i]) == 1 ? true : false);
                    }
                    break;
                case 4:
                    PromptID = 16;
                    SetPromptInfo(4, 16, itemStr[i]);//战术
                    break;
                case 5:
                    PromptID = 15;
                    if (!CharacterRecorder.instance.GetRedPoint(21))
                    {
                        CharacterRecorder.instance.SetRedPoint(21, int.Parse(itemStr[i]) == 1 ? true : false);
                        SetPromptInfo(5, 15, itemStr[i]);//竞技场
                    }
                    else
                    {
                        UpdateRedPoint(21);
                        CharacterRecorder.instance.SetRedPoint(5, int.Parse(itemStr[i]) == 1 ? true : false);
                    }
                    break;
                case 6:
                    PromptID = 10;
                    if (int.Parse(itemStr[i]) == 1)
                    {
                        CharacterRecorder.instance.GachaOnce = 1;
                        CharacterRecorder.instance.GachaMore = 1;
                        CharacterRecorder.instance.GachaMoreTime = 0;
                    }

                    SetPromptInfo(6, 10, itemStr[i]);//招募
                    break;
                case 7:
                    PromptID = 6;
                    SetPromptInfo(7, 6, itemStr[i]);//签到
                    break;
                case 8:
                    PromptID = 23;
                    if (int.Parse(itemStr[i]) == 1)
                    {
                        CharacterRecorder.instance.MailCount = 1;
                    }
                    SetPromptInfo(8, 23, itemStr[i]);//邮件
                    break;
                case 9:
                    PromptID = 24;
                    if (int.Parse(itemStr[i]) == 1)
                    {
                        //CharacterRecorder.instance.applayFriendListCount = 1; //kino
                        CharacterRecorder.instance.SetRedPoint(9, true);
                    }
                    SetPromptInfo(9, 24, itemStr[i]);//好友
                    break;
                case 10:
                    PromptID = 3;
                    SetPromptInfo(10, 3, itemStr[i]);//七日
                    break;
                case 11:
                    //PromptID = 6;
                    //SetPromptInfo(6, itemStr[i]);//vip
                    break;
                case 12:
                    PromptID = 21;
                    if (!CharacterRecorder.instance.GetRedPoint(20))
                    {
                        CharacterRecorder.instance.SetRedPoint(20, int.Parse(itemStr[i]) == 1 ? true : false);
                        SetPromptInfo(12, 21, itemStr[i]);//训练场
                    }
                    else
                    {
                        UpdateRedPoint(20);
                        CharacterRecorder.instance.SetRedPoint(12, int.Parse(itemStr[i]) == 1 ? true : false);
                    }
                    break;
                case 13:
                    PromptID = 18;
                    SetPromptInfo(13, 18, itemStr[i]);//实验室
                    break;
                case 14:
                    PromptID = 14;
                    if (!CharacterRecorder.instance.GetRedPoint(21))
                    {
                        CharacterRecorder.instance.SetRedPoint(21, int.Parse(itemStr[i]) == 1 ? true : false);
                        SetPromptInfo(14, 15, itemStr[i]);//征服
                    }
                    else
                    {
                        UpdateRedPoint(21);
                        CharacterRecorder.instance.SetRedPoint(14, int.Parse(itemStr[i]) == 1 ? true : false);
                    }
                    break;
            }
        }
        UpRedPointFrist();
    }
    ///<summary>
    ///主界面红点位置
    ///1 世界boss 2大放送 3战力排行 4七日登录奖励  5首冲 6充值  7登录  8活动
    ///9英雄  10装备  11招募 12背包 13商城 14任务
    ///15征服  16 冲突   17战术  18情报  19实验室  20 商店   21英雄榜 22军团  23世界
    ///24邮件  25好友 26图鉴  27小贴士  28收益  29问卷
    ///<summary>
    /// <summary> 索引值
    /// 0是任务,1是大放送,2是丛林冒险,3是走私,4是战术,5是竞技场,6是招募,7是签到,8是邮件,9是好友,10是七日登入,11是vip,12训练场,13实验室,14征服,15图鉴,16情报,17英雄,18装备,19活动,20军团
    /// 21是冲突,22是日常副本,23是夺宝,24是组队,25是王者,26军团副本,27军团成员,28军团训练,29军团捐献,30军团任务,31军团红包,32军团酒吧,52军团副本奖励
    /// </summary>
    public void ResetAllRedPoint()
    {
        for (int i = 0; i < CharacterRecorder.instance.AllRedPoint.size; i++)
        {
            UpdateRedPoint(i);
        }
    }
    public void UpdateRedPoint(int i)
    {
        switch (i)
        {
            case 0:
                ShowPrompt(13, CharacterRecorder.instance.GetRedPoint(i));
                break;
            case 1:
                ShowPrompt(1, CharacterRecorder.instance.GetRedPoint(i));
                break;
            case 4:
                ShowPrompt(16, CharacterRecorder.instance.GetRedPoint(i));
                break;
            case 6:
                ShowPrompt(10, CharacterRecorder.instance.GetRedPoint(i));
                break;
            case 7:
                ShowPrompt(6, CharacterRecorder.instance.GetRedPoint(i));
                break;
            case 8:
                ShowPrompt(23, CharacterRecorder.instance.GetRedPoint(i));
                break;
            case 9:
                ShowPrompt(24, CharacterRecorder.instance.GetRedPoint(i));
                break;
            case 10:
                ShowPrompt(3, CharacterRecorder.instance.GetRedPoint(i));
                break;
            case 11:
                // ShowPrompt(5, CharacterRecorder.instance.GetRedPoint(i));
                break;
            case 13:
                ShowPrompt(18, CharacterRecorder.instance.GetRedPoint(i));
                break;
            //case 14:
            //    ShowPrompt(15, CharacterRecorder.instance.GetRedPoint(i));
            //    break;
            case 15:
                ShowPrompt(25, CharacterRecorder.instance.GetRedPoint(i));
                break;
            case 16:
                ShowPrompt(17, CharacterRecorder.instance.GetRedPoint(i));
                break;
            case 17:
                ShowPrompt(8, CharacterRecorder.instance.GetRedPoint(i));
                break;
            case 18:
                ShowPrompt(9, CharacterRecorder.instance.GetRedPoint(i));
                break;
            case 20:
                ShowPrompt(21, CharacterRecorder.instance.GetRedPoint(i));
                break;
            case 21:
                ShowPrompt(15, CharacterRecorder.instance.GetRedPoint(i));
                break;
            case 36:
                ShowPrompt(29, CharacterRecorder.instance.GetRedPoint(i));
                break;
            case 53:
                ShowPrompt(30, CharacterRecorder.instance.GetRedPoint(i));
                break;
            case 54:
                ShowPrompt(12, CharacterRecorder.instance.GetRedPoint(i));
                break;
        }
        //if (CharacterRecorder.instance.AllRedPoint[19] == true || CharacterRecorder.instance.AllRedPoint[34] == true || CharacterRecorder.instance.AllRedPoint[35] == true
        //    || CharacterRecorder.instance.AllRedPoint[33] == true || CharacterRecorder.instance.AllRedPoint[37] == true || CharacterRecorder.instance.AllRedPoint[38] == true
        //    || CharacterRecorder.instance.AllRedPoint[46] == true || CharacterRecorder.instance.AllRedPoint[50] == true || CharacterRecorder.instance.AllRedPoint[51] == true)
        if (PlayerPrefs.GetInt("FirstActivities", 0) == 0 || CharacterRecorder.instance.AllRedPoint[19] == true || CharacterRecorder.instance.AllRedPoint[34] == true || CharacterRecorder.instance.AllRedPoint[35] == true
            || CharacterRecorder.instance.AllRedPoint[33] == true || CharacterRecorder.instance.AllRedPoint[37] == true || CharacterRecorder.instance.AllRedPoint[38] == true)
        {
            ShowPrompt(7, true);
        }
        else
        {
            ShowPrompt(7, false);
        }
    }
    void SetPromptInfo(int position, int ID, string OpenID)
    {
        if (int.Parse(OpenID) == 1)
        {
            CharacterRecorder.instance.RedPointList.Add(ID + 1);
            CharacterRecorder.instance.SetRedPoint(position, true);
            ShowPrompt(ID, true);
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(position, false);
            ShowPrompt(ID, false);
        }
    }
    #endregion

    #region 主界面军团集火信息跑马灯灯
    public void LegionWarInfo()
    {
        //if (PlayerPrefs.HasKey("LegionMarktar") && PlayerPrefs.GetString("LegionMarktar") != "0" && PlayerPrefs.GetString("LegionMarktar") != "" && PlayerPrefs.GetString("LegionMarktar") != null)
        if (CharacterRecorder.instance.LegionMarktarStr != "")
        {
            string Recving = CharacterRecorder.instance.LegionMarktarStr;//PlayerPrefs.GetString("LegionMarktar");
            string[] dataSplit = Recving.Split(';');
            string[] trcSplit = dataSplit[0].Split('$');

            if (CharacterRecorder.instance.isLegionChairman == false && CharacterRecorder.instance.myLegionData != null)
            {
                if (trcSplit[2] != CharacterRecorder.instance.myLegionData.legionName && trcSplit[5] == "1")
                {
                    LegionWarMessage.SetActive(true);
                    LegionWarMessage.transform.Find("MessageLabel").GetComponent<UILabel>().text = "您的军团长发布了发布了[fb2d50]攻占[-]指示，请去战场查看";
                }
                else if (trcSplit[2] == CharacterRecorder.instance.myLegionData.legionName && trcSplit[5] == "1")
                {
                    LegionWarMessage.SetActive(true);
                    LegionWarMessage.transform.Find("MessageLabel").GetComponent<UILabel>().text = "您的军团长发布了发布了[3ee817]防守[-]指示，请去战场查看";
                }
            }
        }
        else
        {
            LegionWarMessage.SetActive(false);
        }
        UIEventListener.Get(LegionWarMessage.transform.Find("IconButton").gameObject).onClick = delegate(GameObject go)
        {
            //PlayerPrefs.SetString("LegionMarktar", "0");
            CharacterRecorder.instance.LegionMarktarStr = "";
            UIManager.instance.OpenPanel("LegionWarWindow", true);
        };
    }
    #endregion

    #region 首冲气泡特效
    void SetFirstButtonEffect()
    {
        StopCoroutine("IESetFirstButtonEffect");
        StartCoroutine("IESetFirstButtonEffect");
    }
    IEnumerator IESetFirstButtonEffect()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            EffectSprite.SetActive(true);
            //yield return new WaitForSeconds(0.2f);
            tweenalpha.from = 0.5f;
            tweenalpha.to = 1f;
            tweenalpha.duration = 0.1f;
            tweenalpha.delay = 0f;

            tweenscale.from = new Vector3(0.5f, 0.5f, 0.5f);
            tweenscale.to = new Vector3(1f, 1f, 1f);
            tweenscale.duration = 0.1f;
            tweenscale.delay = 0f;

            tweenalpha.ResetToBeginning();
            tweenscale.ResetToBeginning();
            tweenalpha.PlayForward();
            tweenscale.PlayForward();
            yield return new WaitForSeconds(1.9f);

            tweenalpha.from = 1f;
            tweenalpha.to = 0.5f;
            tweenalpha.duration = 0.1f;
            tweenalpha.delay = 0f;

            tweenscale.from = new Vector3(1f, 1f, 1f);
            tweenscale.to = new Vector3(0.5f, 0.5f, 0.5f);
            tweenscale.duration = 0.1f;
            tweenscale.delay = 0f;

            tweenalpha.ResetToBeginning();
            tweenscale.ResetToBeginning();
            tweenalpha.PlayForward();
            tweenscale.PlayForward();

            //tweenalpha.PlayReverse();
            //tweenscale.PlayReverse();
            yield return new WaitForSeconds(0.1f);
            EffectSprite.SetActive(false);
        }

    }
    #endregion

    #region 私聊按钮开启
    public void SetPrivateChatButtonIsOpen()
    {
        if (TextTranslator.instance.PrivateChatItemDataList.size > 0)
        {
            PrivateChatButton.SetActive(true);
            if (CharacterRecorder.instance.HaveNewPrivateChatInfo)
            {
                PrivateChatButton.transform.Find("PaiMing_ui02").gameObject.SetActive(true);
            }
            else
            {
                PrivateChatButton.transform.Find("PaiMing_ui02").gameObject.SetActive(false);
            }
        }
        else
        {
            PrivateChatButton.SetActive(false);
        }
    }
    #endregion

    #region 国战按钮开启
    public void SetGuozhanIsOpen()
    {
        if (CharacterRecorder.instance.lastGateID > TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.guozhan).Level && CharacterRecorder.instance.legionCountryID == 0)
        {
            UIManager.instance.OpenSinglePanel("CountryWarWindow", false);
        }
        else
        {

        }
    }
    #endregion


    #region 国战弹出提示特效
    void SetGuozhanEffect()
    {
        if (CharacterRecorder.instance.legionCountryID == 1)
        {
            GuozhanEffectObj.transform.Find("Sprite").GetComponent<UISprite>().spriteName = "tongmengtu1";
            GuozhanEffectObj.transform.Find("Label").GetComponent<UILabel>().text = "我们以鲜血捍卫荣誉!";
            StopCoroutine("IESetGuozhanEffect");
            StartCoroutine("IESetGuozhanEffect");
        }
        else if (CharacterRecorder.instance.legionCountryID == 2)
        {
            GuozhanEffectObj.transform.Find("Sprite").GetComponent<UISprite>().spriteName = "diguotu4";
            GuozhanEffectObj.transform.Find("Label").GetComponent<UILabel>().text = "用武力征服一切!";
            StopCoroutine("IESetGuozhanEffect");
            StartCoroutine("IESetGuozhanEffect");
        }
    }
    IEnumerator IESetGuozhanEffect()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);

            int num = 0;
            while (num < 3)
            {
                num++;
                GuozhanEffectObj.SetActive(true);
                Guozhantweenalpha.from = 0.5f;
                Guozhantweenalpha.to = 1f;
                Guozhantweenalpha.duration = 0.1f;
                Guozhantweenalpha.delay = 0f;

                Guozhantweenscale.from = new Vector3(0.5f, 0.5f, 0.5f);
                Guozhantweenscale.to = new Vector3(1f, 1f, 1f);
                Guozhantweenscale.duration = 0.1f;
                Guozhantweenscale.delay = 0f;

                Guozhantweenalpha.ResetToBeginning();
                Guozhantweenscale.ResetToBeginning();
                Guozhantweenalpha.PlayForward();
                Guozhantweenscale.PlayForward();
                yield return new WaitForSeconds(1.9f);

                Guozhantweenalpha.from = 1f;
                Guozhantweenalpha.to = 0.5f;
                Guozhantweenalpha.duration = 0.1f;
                Guozhantweenalpha.delay = 0f;

                Guozhantweenscale.from = new Vector3(1f, 1f, 1f);
                Guozhantweenscale.to = new Vector3(0.5f, 0.5f, 0.5f);
                Guozhantweenscale.duration = 0.1f;
                Guozhantweenscale.delay = 0f;

                Guozhantweenalpha.ResetToBeginning();
                Guozhantweenscale.ResetToBeginning();
                Guozhantweenalpha.PlayForward();
                Guozhantweenscale.PlayForward();

                //tweenalpha.PlayReverse();
                //tweenscale.PlayReverse();
                yield return new WaitForSeconds(0.1f);
                GuozhanEffectObj.SetActive(false);
            }
        }

    }
    #endregion

    #region 挂机经验奖励弹出框动画与参数设置

    public void SetLabelEffectAwardInfo(int ItemCode, int ItemNum, int NowExp)
    {
        this.OnhookItemCode = ItemCode;
        this.OnhookItemNum = ItemNum;
        this.OnhookExp = NowExp;
    }
    private void LabelEffectAnimation(int ItemCode, int ItemNum, int NowExp)
    {
        UILabel LabelExp = LabelEffectExp.transform.Find("LabelEffect").GetComponent<UILabel>();
        TweenPosition tweenPositionExp = LabelEffectExp.GetComponent<TweenPosition>();
        TweenScale tweenScaleExp = LabelEffectExp.GetComponent<TweenScale>();
        TweenAlpha tweenAlphaExp = LabelEffectExp.GetComponent<TweenAlpha>();

        UILabel LabelAward = LabelEffectAward.transform.Find("LabelEffect").GetComponent<UILabel>();
        TweenPosition tweenPositionAward = LabelEffectAward.GetComponent<TweenPosition>();
        TweenScale tweenScaleAward = LabelEffectAward.GetComponent<TweenScale>();
        TweenAlpha tweenAlphaAward = LabelEffectAward.GetComponent<TweenAlpha>();

        if (NowExp > 0)
        {
            LabelEffectExp.transform.localPosition = new Vector3(-82f, 20.9f, 0);
            LabelExp.text = "经验[00ff00]Exp+" + NowExp.ToString() + "[-]";
            tweenPositionExp.ResetToBeginning();
            tweenScaleExp.ResetToBeginning();
            tweenAlphaExp.ResetToBeginning();

            tweenPositionExp.from = new Vector3(-82f, 20.9f, 0);
            tweenPositionExp.to = new Vector3(-82f, 200f, 0);
            tweenPositionExp.duration = 1.6f;
            tweenPositionExp.delay = 1.6f;

            tweenScaleExp.from = new Vector3(0, 0, 0);
            tweenScaleExp.to = new Vector3(1.2f, 1.2f, 1.2f);
            tweenScaleExp.duration = 1.6f;
            tweenScaleExp.delay = 0;

            tweenAlphaExp.from = 1f;
            tweenAlphaExp.to = 0;
            tweenAlphaExp.duration = 1.6f;
            tweenAlphaExp.delay = 1.6f;

            tweenScaleExp.PlayForward();
            tweenPositionExp.PlayForward();
            tweenAlphaExp.PlayForward();
        }

        if (ItemCode > 0)
        {
            //LabelAward.text = "获得[00ff00]" + TextTranslator.instance.GetItemNameByItemCode(ItemCode) + "X" + ItemNum.ToString() + "[-]";
            LabelEffectAward.transform.localPosition = new Vector3(-82f, -44.3f, 0);
            LabelEffectColor(ItemCode, ItemNum, LabelAward);
            tweenPositionAward.ResetToBeginning();
            tweenScaleAward.ResetToBeginning();
            tweenAlphaAward.ResetToBeginning();

            tweenPositionAward.from = new Vector3(-82f, -44.3f, 0);
            tweenPositionAward.to = new Vector3(-82f, 134.8f, 0);
            tweenPositionAward.duration = 1.6f;
            tweenPositionAward.delay = 1.6f;

            tweenScaleAward.from = new Vector3(0, 0, 0);
            tweenScaleAward.to = new Vector3(1.2f, 1.2f, 1.2f);
            tweenScaleAward.duration = 1.6f;
            tweenScaleAward.delay = 0;

            tweenAlphaAward.from = 1f;
            tweenAlphaAward.to = 0;
            tweenAlphaAward.duration = 1.6f;
            tweenAlphaAward.delay = 1.6f;

            tweenScaleAward.PlayForward();
            tweenPositionAward.PlayForward();
            tweenAlphaAward.PlayForward();
        }
    }
    #endregion

    #region 挂机经验奖励文字颜色
    private void LabelEffectColor(int ItemCode, int ItemNum, UILabel LabelAward)
    {
        int colorNum = TextTranslator.instance.GetItemByItemCode(ItemCode).itemGrade;
        switch (colorNum)
        {
            case 1:
                LabelAward.text = "获得[dbdbdb]" + TextTranslator.instance.GetItemNameByItemCode(ItemCode) + "X" + ItemNum.ToString() + "[-]";
                break;
            case 2:
                LabelAward.text = "获得[00ff3c]" + TextTranslator.instance.GetItemNameByItemCode(ItemCode) + "X" + ItemNum.ToString() + "[-]";
                break;
            case 3:
                LabelAward.text = "获得[009cff]" + TextTranslator.instance.GetItemNameByItemCode(ItemCode) + "X" + ItemNum.ToString() + "[-]";
                break;
            case 4:
                LabelAward.text = "获得[b500ff]" + TextTranslator.instance.GetItemNameByItemCode(ItemCode) + "X" + ItemNum.ToString() + "[-]";
                break;
            case 5:
                LabelAward.text = "获得[ff6c00]" + TextTranslator.instance.GetItemNameByItemCode(ItemCode) + "X" + ItemNum.ToString() + "[-]";
                break;
            case 6:
                LabelAward.text = "获得[ff0000]" + TextTranslator.instance.GetItemNameByItemCode(ItemCode) + "X" + ItemNum.ToString() + "[-]";
                break;
            default:
                LabelAward.text = "获得[dbdbdb]" + TextTranslator.instance.GetItemNameByItemCode(ItemCode) + "X" + ItemNum.ToString() + "[-]";
                break;
        }
    }
    #endregion

    #region 超级军火库是否开启
    public void SureJunhuokuIsOpen()
    {
        int vip = CharacterRecorder.instance.Vip;
        CancelInvoke("InvokeUpdateJunhuoTime");
        if (CharacterRecorder.instance.HoldJunhuoTime > 0 && vip < 10)
        {
            JunhuokuButton.SetActive(true);
            if (vip < 4)
            {
                JunhuokuButton.GetComponent<UISprite>().spriteName = "junhuoku1";
            }
            else if (vip < 6)
            {
                JunhuokuButton.GetComponent<UISprite>().spriteName = "junhuoku2";
            }
            else if (vip < 8)
            {
                JunhuokuButton.GetComponent<UISprite>().spriteName = "junhuoku3";
            }
            else if (vip < 10)
            {
                JunhuokuButton.GetComponent<UISprite>().spriteName = "junhuoku4";
            }
            else
            {
                JunhuokuButton.GetComponent<UISprite>().spriteName = "junhuoku5";
            }

            JunhuoLabel.gameObject.SetActive(true);
            InvokeRepeating("InvokeUpdateJunhuoTime", 0, 1f);
        }
        else if (vip >= 10)
        {
            JunhuokuButton.SetActive(true);
            JunhuokuButton.GetComponent<UISprite>().spriteName = "junhuoku5";
            JunhuoLabel.gameObject.SetActive(false);
        }
        else
        {
            JunhuokuButton.SetActive(false);
            if (GameObject.Find("SuperArmsStoreWindow") != null)
            {
                DestroyImmediate(GameObject.Find("SuperArmsStoreWindow").gameObject);
            }
        }
    }
    #endregion

    #region 军火商倒计时Invoke刷新
    void InvokeUpdateJunhuoTime()
    {
        string houreStr = (CharacterRecorder.instance.HoldJunhuoTime / 3600).ToString("00");
        string miniteStr = (CharacterRecorder.instance.HoldJunhuoTime % 3600 / 60).ToString("00");
        string secondStr = (CharacterRecorder.instance.HoldJunhuoTime % 60).ToString("00");
        JunhuoLabel.text = string.Format("{0}:{1}:{2}", houreStr, miniteStr, secondStr);
    }
    #endregion
    #region item窗口显示颜色物品
    private void SetItemDetail(int _itemId, int _itemCount, GameObject ItemObj)
    {
        UISprite spriteFrame = ItemObj.GetComponent<UISprite>();
        UISprite spriteIcon = ItemObj.transform.Find("Icon").GetComponent<UISprite>();
        GameObject suiPian = ItemObj.transform.Find("suiPian").gameObject;
        ItemObj.transform.Find("Number").GetComponent<UILabel>().text = _itemCount.ToString();
        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(_itemId);
        spriteFrame.spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
        //TextTranslator.instance.ItemDescription(ItemObj, _itemId, _itemCount); //kino

        if (_itemId.ToString()[0] == '4')
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = _itemId.ToString();
            suiPian.SetActive(false);
        }
        else if (_itemId.ToString()[0] == '6')
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = _itemId.ToString();
            suiPian.SetActive(false);
        }
        else if (_itemId == 70000 || _itemId == 79999)
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = _itemId.ToString();
            suiPian.SetActive(true);
        }
        else if (_itemId.ToString()[0] == '7' && _itemId > 70000)
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = (_itemId - 10000).ToString();
            suiPian.SetActive(true);
        }
        else if (_itemId.ToString()[0] == '8')
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_itemId);
            //spriteIcon.spriteName = (ItemCode - 30000).ToString();
            spriteIcon.spriteName = mItemInfo.picID.ToString();
            suiPian.SetActive(true);
        }
        else if (_itemId.ToString()[0] == '2')
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_itemId);
            spriteIcon.spriteName = mItemInfo.picID.ToString();
            suiPian.SetActive(false);
        }
        else
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = _itemId.ToString();
            suiPian.SetActive(false);
        }

        UIEventListener.Get(ItemObj).onClick = delegate(GameObject go)
        {
            if (WillOpenTipButton.transform.Find("SpriteSmallGoalTop/QuanEffect").gameObject.activeSelf)
            {
                NetworkHandler.instance.SendProcess("1922#" + smallGoalID + ";");
            }
            else
            {
                UIManager.instance.OpenSinglePanel("SmallGoalWindow", false);
            }
        };
    }
    #endregion

    #region 我要变强的开放条件
    private void HelperButtonIsOpenNew()
    {
        if (CharacterRecorder.instance.level >= 25)
        {
            HelperButton.SetActive(true);
        }
        else
        {
            HelperButton.SetActive(false);
        }

        HelperButton.transform.parent.GetComponent<UIGrid>().Reposition();
    }
    #endregion
}


