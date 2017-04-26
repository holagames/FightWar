using UnityEngine;

using System;
using System.Collections;
using System.Collections.Generic;

public class ChallengeWindow : MonoBehaviour
{
    // Use this for initialization

    public UILabel LabelName;
    public UILabel LabelAward;
    public UILabel labelCurCName;
    public UICenterOnChild uiSV;
    public GameObject CGrid;
    public UIAtlas ItemAtlas;
    public UIAtlas AvatarAtlas;
    public UIAtlas CommonAtlas;
    public GameObject SpriteAward1;
    public GameObject SpriteAward2;
    public GameObject SpriteAward3;
    public GameObject SpriteAward4;
    public GameObject SpriteAward5;
    private bool isPress = false;
    private float datatime = 0;
    private int ItemId;
    private int ItemCount;
    private GameObject AwardObj;
    private int SpriteAwardId1;
    private int SpriteAwardId2;
    private int SpriteAwardId3;
    private int SpriteAwardId4;
    private int SpriteAwardId5;
    //移动窗口
    public int NowCenterNum = 0;
    public GameObject LeftButton;
    public GameObject RightButton;
    public UIGrid ScrollGrid;
    public UIScrollView ScrollView;
    public float ScrollViewSize = 0;
    public float Timer;
    //
    public List<GameObject> ItemC = new List<GameObject>();
    public int Hight = 0;
    //public int Point=0;//pvp积分
    //public int GetPointLayer = 0;//领取的pvp层级
    //public int GetRankLayer = 0;
    //public int HaveRankLayer = 0;

    public GameObject BackUI;
    public GameObject ChallengeInfo;
    public GameObject GoBtn;
    public GameObject uiGrid;


    private int SeedCount = 0;
    /// <summary>
    /// 活动1  1701#3
    /// </summary>
    public EverydayActivity ImpregnableItem;
    /// <summary>
    /// 活动2  1701#4
    /// </summary>
    public EverydayActivity AttackDefItem;
    /// <summary>
    /// 活动3  1701#5
    /// </summary>
    public EverydayActivity StormItem;

    void Start()
    {
        //SpriteAwardId1 = 70012;
        //SpriteAwardId2 = 90003;
        //SpriteAwardId3 = 10104;
        //SpriteAwardId4 = 90001;
        //SpriteAwardId5 = 0;

        ImpregnableItem = TextTranslator.instance.GetEverydayActivityDicByID(13);
        AttackDefItem = TextTranslator.instance.GetEverydayActivityDicByID(19);
        StormItem = TextTranslator.instance.GetEverydayActivityDicByID(25);

        IsShowMessage();
        //GetCenterNum(2);

        #region 按钮事件
        //UIEventListener.Get(ItemC[0]).onPress += delegate(GameObject go, bool isPress)
        //{
        //    if (!isPress)
        //    {
        //        SetMoveChapter();
        //    }
        //};
        //UIEventListener.Get(ItemC[0]).onClick += delegate(GameObject go)
        //{
        //    if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.jingjichang).Level + 1)
        //    {
        //        UIManager.instance.OpenPromptWindow("长官,通关" + (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.jingjichang).Level - 10000).ToString() + "关开放", PromptWindow.PromptType.Hint, null, null);
        //    }
        //    else
        //    {
        //        EnterChapter(1);
        //    }
        //};

        //UIEventListener.Get(ItemC[1]).onClick += delegate(GameObject go)
        //{
        //    if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.richangfuben).Level + 1)
        //    {
        //        UIManager.instance.OpenPromptWindow("长官,通关" + (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.richangfuben).Level - 10000).ToString() + "关开放", PromptWindow.PromptType.Hint, null, null);
        //    }
        //    else
        //    {
        //        CharacterRecorder.instance.EverydayTab = 1;
        //        EnterChapter(2);
        //    }
        //};
        //UIEventListener.Get(ItemC[3]).onPress += delegate(GameObject go, bool isPress)
        //{
        //    if (!isPress)
        //    {
        //        SetMoveChapter();
        //    }
        //};

        //UIEventListener.Get(ItemC[2]).onPress += delegate(GameObject go, bool isPress)
        //{
        //    if (!isPress)
        //    {
        //        SetMoveChapter();
        //    }
        //};
        //UIEventListener.Get(ItemC[2]).onClick += delegate(GameObject go)
        //{
        //    if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.duobao).Level + 1)
        //    {
        //        UIManager.instance.OpenPromptWindow("长官,通关" + (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.duobao).Level - 10000).ToString() + "关开放", PromptWindow.PromptType.Hint, null, null);
        //    }
        //    else
        //    {
        //        EnterChapter(3);
        //    }
        //};
        //UIEventListener.Get(ItemC[1]).onPress += delegate(GameObject go, bool isPress)
        //{
        //    if (!isPress)
        //    {
        //        SetMoveChapter();
        //    }
        //};

        //UIEventListener.Get(ItemC[3]).onClick += delegate(GameObject go)
        //{
        //    UIManager.instance.OpenPromptWindow("暂未开放", PromptWindow.PromptType.Alert, null, null);
        //    if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zuduifuben).Level + 1)
        //    {
        //        UIManager.instance.OpenPromptWindow("长官,通关" + (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zuduifuben).Level - 10000).ToString() + "关开放", PromptWindow.PromptType.Hint, null, null);
        //    }
        //    else
        //    {
        //        EnterChapter(4);
        //    }
        //};

        //UIEventListener.Get(ItemC[4]).onPress += delegate(GameObject go, bool isPress)
        //{
        //    if (!isPress)
        //    {
        //        SetMoveChapter();
        //    }
        //};
        //UIEventListener.Get(ItemC[4]).onClick += delegate(GameObject go)
        //{
        //    if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.conglingmaoxian).Level + 1)
        //    {
        //        UIManager.instance.OpenPromptWindow("长官,通关" + (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.conglingmaoxian).Level - 10000).ToString() + "关开放", PromptWindow.PromptType.Hint, null, null);
        //    }
        //    else
        //    {
        //        EnterChapter(5);
        //    }
        //};

        //UIEventListener.Get(ItemC[5]).onClick += delegate(GameObject go)
        //{
        //    UIManager.instance.OpenPromptWindow("暂未开放", PromptWindow.PromptType.Alert, null, null);
        //    if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zhengfu).Level + 1)
        //    {
        //        UIManager.instance.OpenPromptWindow("长官,通关" + (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zhengfu).Level - 10000).ToString() + "关开放", PromptWindow.PromptType.Hint, null, null);
        //    }
        //    else
        //    {
        //        EnterChapter(6);
        //    }
        //};
        //UIEventListener.Get(ItemC[5]).onPress += delegate(GameObject go, bool isPress)
        //{
        //    if (!isPress)
        //    {
        //        SetMoveChapter();
        //    }
        //};



        //UIEventListener.Get(ItemC[6]).onClick += delegate(GameObject go)
        //{
        //    UIManager.instance.OpenPromptWindow("暂未开放", PromptWindow.PromptType.Alert, null, null);
        //    if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.bianjingzousi).Level + 1)
        //    {
        //        UIManager.instance.OpenPromptWindow("长官,通关" + (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.bianjingzousi).Level - 10000).ToString() + "关开放", PromptWindow.PromptType.Hint, null, null);
        //    }
        //    else
        //    {
        //        EnterChapter(7);
        //    }
        //};
        //UIEventListener.Get(ItemC[6]).onPress += delegate(GameObject go, bool isPress)
        //{
        //    if (!isPress)
        //    {
        //        SetMoveChapter();
        //    }
        //};



        //UIEventListener.Get(ItemC[7]).onClick += delegate(GameObject go)
        //{
        //    UIManager.instance.OpenPromptWindow("暂未开放", PromptWindow.PromptType.Alert, null, null);
        //    if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.wangzhezhilu).Level + 1)
        //    {
        //        UIManager.instance.OpenPromptWindow("长官,通关" + (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.wangzhezhilu).Level - 10000).ToString() + "关开放", PromptWindow.PromptType.Hint, null, null);
        //    }
        //    else
        //    {
        //        EnterChapter(8);
        //    }
        //};
        //UIEventListener.Get(ItemC[7]).onPress += delegate(GameObject go, bool isPress)
        //{
        //    if (!isPress)
        //    {
        //        SetMoveChapter();
        //    }
        //};

        //UIEventListener.Get(ItemC[8]).onPress += delegate(GameObject go, bool isPress)
        //{
        //    if (!isPress)
        //    {
        //        SetMoveChapter();
        //    }
        //};
        //UIEventListener.Get(ItemC[8]).onClick += delegate(GameObject go)
        //{
        //    EnterChapter(9);
        //};

        #endregion

        if (UIEventListener.Get(BackUI).onClick == null)
        {
            UIEventListener.Get(BackUI).onClick = delegate(GameObject go)
            {
                //UIManager.instance.BackUI();
                Time.timeScale = 1;
                if ((PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) != 2 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) != 6) ||
                    (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) != 4 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) != 6))
                {
                    if ((PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 2 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 8) ||
                        (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 4 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 8) ||
                        (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 9 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 7) ||
                       (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 15 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 8))
                    {
                        SceneTransformer.instance.NewGuideButtonClick();
                    }
                    if (CharacterRecorder.instance.GuideID[26] == 6 || CharacterRecorder.instance.GuideID[38] == 9)
                    {
                        SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                    }
                    if (CharacterRecorder.instance.ChangeAttribute != false)
                    {
                        NetworkHandler.instance.SendProcess("1005#0;");
                        CharacterRecorder.instance.ChangeAttribute = false;
                    }
                    if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) >= 18)
                    {
                        if (CharacterRecorder.instance.GuideID[29] == 7)
                        {
                            CharacterRecorder.instance.GuideID[29] += 1;
                        }
                        else if (CharacterRecorder.instance.GuideID[10] == 16)
                        {
                            CharacterRecorder.instance.GuideID[10] += 1;
                        }
                    }
                    if (CharacterRecorder.instance.GuideID[29] != 6)
                    {
                        if (GameObject.Find("RoleEquipInfoWindow") != null)
                        {
                            UIManager.instance.BackUI();
                        }
                        else
                        {
                            StartCoroutine(WaitForSencondNew());//新的 返回上一层界面
                        }
                    }
                }
            };
        }

        isLock();//解锁 

        //StartCoroutine(IsShowPvPMessage());
        StartCoroutine(IsShowTeamCopyMessage());
        //StartCoroutine(IsShowKingRoadMessage());

        SetRedPointInit();
        SetNuclearRed();

        #region
        //UIEventListener.Get(LeftButton).onClick = delegate(GameObject go)
        //{
        //    //ScrollBar.GetComponent<UIScrollBar>().value -= 0.76f;
        //    if (NowCenterNum - 1 > 1)
        //    {
        //        GetCenterNum(NowCenterNum - 1);
        //        //RightButton.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        GetCenterNum(1);
        //        //隐藏左按钮图标
        //        //LeftButton.gameObject.SetActive(false);
        //    }
        //};
        //UIEventListener.Get(RightButton).onClick = delegate(GameObject go)
        //{
        //    //ScrollBar.GetComponent<UIScrollBar>().value += 0.76f;
        //    if (NowCenterNum + 1 < 9)
        //    {
        //        GetCenterNum(NowCenterNum + 1);
        //        //LeftButton.gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        GetCenterNum(8);
        //        //隐藏右按钮图标
        //        //RightButton.gameObject.SetActive(false);
        //    }
        //};
        #endregion
        AudioEditer.instance.PlayLoop("Challenge");

        //添加事件
        for (int i = 0; i < ItemC.Count; i++)
        {
            if (ItemC[i].GetComponent<UIToggle>() == null)
            {
                ItemC[i].AddComponent<UIToggle>();
            }
            UIToggle checkStatus = ItemC[i].GetComponent<UIToggle>();
            //给控件绑定选择及取消选择事件
            EventDelegate.Add(checkStatus.onChange, SetToogle_Changed);
        }
        int redPosition = GetRedPointPosition();
        //Debug.LogError("RandomMysteryNumber:: " + CharacterRecorder.instance.RandomMysteryNumber);
        if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.jingjichang).Level + 1)
        {
            ItemC[8].GetComponent<UIToggle>().value = true;
        }
        else if (CharacterRecorder.instance.RandomMysteryNumber == 0)
        {
            ItemC[redPosition - 1].GetComponent<UIToggle>().value = true;
        }
        else
        {
            ItemC[NowItem(GameCenter.leavelName) - 1].GetComponent<UIToggle>().value = true;
            //GameCenter.leavelName = null;
        }
        CharacterRecorder.instance.RandomMysteryNumber = -1;
        //ItemC[2].GetComponent<UIToggle>().value = true;
        //UpdateWindowSize();
    }

    void SetNuclearRed()
    {
        if (CharacterRecorder.instance.HaveNuclear)
        {
            CharacterRecorder.instance.SetRedPoint(55, true);
            ItemC[9].transform.Find("RedPoint").gameObject.SetActive(true);
        }else
        {
            CharacterRecorder.instance.SetRedPoint(55, false);
            ItemC[9].transform.Find("RedPoint").gameObject.SetActive(false);
        }
    }
    IEnumerator WaitForSencondNew()//返回上一层界面
    {
        yield return new WaitForSeconds(0.01f);
        PictureCreater.instance.DestroyAllComponent();
        if (GameObject.Find("WayWindow") != null)
        {
            UIManager.instance.BackUI();
        }
        else if (GameObject.Find("VIPShopWindow") != null)
        {
            if (CharacterRecorder.instance.isWoods)
            {
                NetworkHandler.instance.SendProcess("1501#");
                CharacterRecorder.instance.isWoods = false;
            }
            else if (GameObject.Find("GaChaGetWindow") != null)
            {
                //UIManager.instance.BackUI();
                DestroyImmediate(GameObject.Find("VIPShopWindow"));
            }
            else
            {
                UIManager.instance.BackUI();
            }
        }
        else if (GameObject.Find("HeroMapWindow") != null)
        {
            UIManager.instance.BackUI();
        }
        else if (GameObject.Find("ChallengeWindow") != null)
        {
            UIManager.instance.OpenPanel("MainWindow", true);
        }
        else if (GameObject.Find("ConquerWindow") != null && GameObject.Find("HarvestWindow") != null)
        {
            CharacterRecorder.instance.TabeID = 0;
            NetworkHandler.instance.SendProcess("6501#");
            UIManager.instance.BackUI();
            // UIManager.instance.OpenPanel("MainWindow", true);
        }
        //else if (GameObject.Find("ConquerWindow") != null && GameObject.Find("CheckGateWindow") != null)
        //{
        //     UIManager.instance.OpenPanel("MainWindow", true);
        //}
        else if (CharacterRecorder.instance.isToQualify)
        {
            CharacterRecorder.instance.isToQualify = false;
            UIManager.instance.OpenPanel("MainWindow", true);
        }
        else if (GameObject.Find("RoleWindow") != null)
        {
            // bool IsNeedShowTips = CheckToShowTipsOfLeavingSkill();
            //if (IsNeedShowTips == false)
            //{
            //    if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 9 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 9)
            //    {

            //    }
            UIManager.instance.BackUI();
            //}
        }
        //PVPWindow-->"竞技场"    GrabItemWindow-->"夺宝奇兵"    EverydayWindow-->"日常副本"
        else if (GameObject.Find("PVPWindow") != null || GameObject.Find("GrabItemWindow") != null || GameObject.Find("EverydayWindow") != null)
        {
            if (GameObject.Find("PVPWindow") != null)
            {
                GameCenter.leavelName = "PVPWindow";
            }
            else if (GameObject.Find("GrabItemWindow") != null)
            {
                GameCenter.leavelName = "GrabItemWindow";
                CharacterRecorder.instance.isFailed = false;//夺宝返回后清除
            }
            else if (GameObject.Find("EverydayWindow") != null)
            {
                GameCenter.leavelName = "EverydayWindow";
            }
            CharacterRecorder.instance.PVPComeNum = 0;
            CharacterRecorder.instance.OnceSuceessID = 0;
            UIManager.instance.BackUI();
        }
        else if (GameObject.Find("GetRankingReward") != null || GameObject.Find("IntegrationWindow") != null)
        {
            UIManager.instance.BackUI();
        }
        //WoodsTheExpendablesobList --> "丛林冒险"
        else if (GameObject.Find("WoodsTheExpendablesMapList") != null)
        {

            if (GameObject.Find("RankShopWindow") != null)
            {
                UIManager.instance.BackUI();
                NetworkHandler.instance.SendProcess("1501#");
            }
            else if (CharacterRecorder.instance.isTaskGoto)
            {
                CharacterRecorder.instance.isTaskGoto = false;
                UIManager.instance.BackUI();
            }
            else
            {
                GameCenter.leavelName = "WoodsTheExpendables";
                UIManager.instance.OpenPanel("ChallengeWindow", true);
            }
        }
        else if (GameObject.Find("TaskWindow") != null)
        {
            UIManager.instance.BackUI();
        }
        else if (GameObject.Find("BagWindow") != null)
        {
            if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 15 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 11)
            {

            }
            UIManager.instance.OpenPanel("MainWindow", true);
        }
        else if (GameObject.Find("GaChaGetWindow") != null)
        {
            if (GameObject.Find("GachaWindow") != null)
            {
                UIManager.instance.BackUI();
                NetworkHandler.instance.SendProcess("5204#;");
            }
        }
        // 
        else if (GameObject.Find("TeamInvitationWindow") != null)
        {
            GameObject.Find("TeamInvitationWindow").GetComponent<TeamInvitationWindow>().HintWindow.SetActive(true);
            GameObject.Find("TeamInvitationWindow").GetComponent<TeamInvitationWindow>().HintWindow.GetComponent<HintWindow>().SetHintWindow(CharacterRecorder.instance.TeamID, CharacterRecorder.instance.TeamPosition);

        }
        else if (GameObject.Find("TeamBrowseWindow") != null)
        {
            UIManager.instance.OpenSinglePanel("TeamCopyWindow", true);
        }
        //TeamCopyWindow --> "组队副本"
        else if (GameObject.Find("TeamCopyWindow") != null)
        {
            //if (UIManager.instance.TheWindowIsStay("ChallengeWindow"))
            //{
            //    GameCenter.leavelName = "TeamCopyWindow";
            //    UIManager.instance.BackUI();
            //}
            //else
            //{
            //    UIManager.instance.OpenPanel("MainWindow", true);
            //}
            UIManager.instance.OpenPanel("ChallengeWindow", true);
        }
        else if (GameObject.Find("WorldBossFightWindow") != null)
        {
            NetworkHandler.instance.SendProcess("6211#" + CharacterRecorder.instance.characterName + ";" + CharacterRecorder.instance.headIcon + ";" + CharacterRecorder.instance.MyLocationInWorldBoss + ";");
            GameObject.Find("WorldBossFightWindow").GetComponent<WorldBossFightWindow>().LiveWorldBossFightWindow();
            //NetworkHandler.instance.SendProcess("6211#" + CharacterRecorder.instance.characterName + ";" + CharacterRecorder.instance.headIcon + ";");
        }
        else
        {
            if (CharacterRecorder.instance.IsOnputOrRemove)
            {
                NetworkHandler.instance.SendProcess("1005#0;");
                CharacterRecorder.instance.IsOnputOrRemove = false;
            }
            if (GameObject.Find("SmuggleWindow") != null)
            {
                GameCenter.leavelName = "SmuggleWindow";
            }
            else if (GameObject.Find("KingRoadWindow") != null)
            {
                GameCenter.leavelName = "KingRoadWindow";
            }
            UIManager.instance.BackUI();
        }

        if (TextTranslator.instance.isUpdateBag)
        {
            NetworkHandler.instance.SendProcess("5001#;");
        }

        CharacterRecorder.instance.GrabIntegrationOpen = false;//夺宝积分界面是否打卡
        CharacterRecorder.instance.SweptIconID = 0;//此处为装备强化材料扫荡返回清除
        CharacterRecorder.instance.SweptIconNum = 0;
    }

    public void SetRedPointInit()
    {
        //Debug.LogError("SetRedPoint");
        if (!ItemC[0].transform.Find("Lock").gameObject.activeSelf)
        {
            if (!ItemC[0].transform.Find("RedPoint").gameObject.activeSelf)
            {
                if (PlayerPrefs.GetInt("PvpRankPodition_" + PlayerPrefs.GetString("ServerID") + "_" + PlayerPrefs.GetInt("UserID")) == 0)
                {
                    CharacterRecorder.instance.SetRedPoint(5, true);
                    ItemC[0].transform.Find("RedPoint").gameObject.SetActive(true);
                }
                else
                {
                    CharacterRecorder.instance.SetRedPoint(5, false);
                    ItemC[0].transform.Find("RedPoint").gameObject.SetActive(false);
                    NetworkHandler.instance.SendProcess("6001#;");
                }
            }
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(5, false);
            ItemC[0].transform.Find("RedPoint").gameObject.SetActive(false);
        }
        if (!ItemC[1].transform.Find("Lock").gameObject.activeSelf)
        {
            if (!ItemC[1].transform.Find("RedPoint").gameObject.activeSelf)
            {
                NetworkHandler.instance.SendProcess("1701#1;");
                //SeedCount++;
            }
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(22, false);
            ItemC[1].transform.Find("RedPoint").gameObject.SetActive(false);
        }
        if (!ItemC[3].transform.FindChild("Lock").gameObject.activeSelf && CharacterRecorder.instance.level >= 20)
        {
            if (!ItemC[3].transform.FindChild("RedPoint").gameObject.activeSelf)
            {
                NetworkHandler.instance.SendProcess("6108#");
            }
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(24, false);
            ItemC[3].transform.Find("RedPoint").gameObject.SetActive(false);
        }
        if (!ItemC[5].transform.FindChild("Lock").gameObject.activeSelf)
        {
            if (!ItemC[5].transform.FindChild("RedPoint").gameObject.activeSelf)
            {
                CharacterRecorder.instance.ChallengeToZhengFu = 0;
                NetworkHandler.instance.SendProcess("6501#");
            }
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(14, false);
            ItemC[5].transform.Find("RedPoint").gameObject.SetActive(false);
        }
        if (!ItemC[7].transform.FindChild("Lock").gameObject.activeSelf)
        {
            if (!ItemC[7].transform.FindChild("RedPoint").gameObject.activeSelf)
            {
                NetworkHandler.instance.SendProcess("6401#");
            }
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(25, false);
            ItemC[7].transform.Find("RedPoint").gameObject.SetActive(false);
        }
        if (!ItemC[6].transform.FindChild("Lock").gameObject.activeSelf)
        {
            if (!ItemC[6].transform.FindChild("RedPoint").gameObject.activeSelf)
            {
                NetworkHandler.instance.SendProcess("6301#");
            }
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(3, false);
            ItemC[6].transform.Find("RedPoint").gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 6001
    /// </summary>
    public void SetJingJiChangRedPoint_6001(string count)
    {
        if (!ItemC[0].transform.Find("RedPoint").gameObject.activeSelf)
        {
            if (CharacterRecorder.instance.PvpChallengeNum > 0 && CharacterRecorder.instance.PvpRefreshTime == 0)//奖励与积分红点显示
            {
                CharacterRecorder.instance.SetRedPoint(5, true);
                ItemC[0].transform.Find("RedPoint").gameObject.SetActive(true);
            }
            else
            {
                CharacterRecorder.instance.SetRedPoint(5, false);
                ItemC[0].transform.Find("RedPoint").gameObject.SetActive(false);
                NetworkHandler.instance.SendProcess("6009#;");
            }
        }
    }
    /// <summary>
    /// 6009
    /// </summary>
    public void SetJingJiChangRedPoint_6009()
    {
        if (CharacterRecorder.instance.GetPointLayer < 10 && CharacterRecorder.instance.PvpPoint >= CharacterRecorder.instance.GetPointLayer * 2 + 2)
        {
            CharacterRecorder.instance.SetRedPoint(5, true);
            ItemC[0].transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(5, false);
            ItemC[0].transform.Find("RedPoint").gameObject.SetActive(false);
            NetworkHandler.instance.SendProcess("6011#;");
        }
    }


    /// <summary>
    /// 6011
    /// </summary>
    public void SetJingJiChangRedPoint_6011()
    {
        if (CharacterRecorder.instance.GetRankLayer < CharacterRecorder.instance.HaveRankLayer)
        {
            CharacterRecorder.instance.SetRedPoint(5, true);
            ItemC[0].transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(5, false);
            ItemC[0].transform.Find("RedPoint").gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 1701
    /// </summary>
    public void SetEveryDayRedPoint_1701(int count, int cd, int week)
    {
        if (count > 0 && cd == 0)
        {
            CharacterRecorder.instance.SetRedPoint(22, true);
            ItemC[1].transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(22, false);
            ItemC[1].transform.Find("RedPoint").gameObject.SetActive(false);
            SeedCount++;
            if (SeedCount == 1 && CharacterRecorder.instance.lastGateID >= 10073)
            {
                NetworkHandler.instance.SendProcess("1701#2;");
            }
            else if (SeedCount < 5 && CharacterRecorder.instance.lastGateID >= 10091)
            {
                if (SeedCount == 2)
                {
                    string[] openday = ImpregnableItem.OpenWeek.Split('$');
                    for (int i = 0; i < openday.Length - 1; i++)
                    {
                        if (week == int.Parse(openday[i]))
                        {
                            NetworkHandler.instance.SendProcess("1701#3;");
                            break;
                        }
                    }
                }
                else if (SeedCount == 3)
                {
                    string[] openday1 = AttackDefItem.OpenWeek.Split('$');
                    for (int i = 0; i < openday1.Length - 1; i++)
                    {
                        if (week == int.Parse(openday1[i]))
                        {
                            NetworkHandler.instance.SendProcess("1701#4;");
                            break;
                        }
                    }
                }
                else if (SeedCount == 4)
                {
                    string[] openday2 = StormItem.OpenWeek.Split('$');
                    for (int i = 0; i < openday2.Length - 1; i++)
                    {
                        if (week == int.Parse(openday2[i]))
                        {
                            NetworkHandler.instance.SendProcess("1701#5;");
                            break;
                        }
                    }
                }
            }
            else if (SeedCount == 5 && CharacterRecorder.instance.lastGateID >= 10097)
            {
                if (week == 1 || week == 3 || week == 5 || week == 7)
                {
                    NetworkHandler.instance.SendProcess("1701#6;");
                }
            }
        }
    }
    /// <summary>
    /// 6108
    /// </summary>
    /// <param name="count">剩余次数</param>
    /// <param name="index">副本编号</param>
    /// <param name="cd">cd时间</param>
    public void SetTeamCopyRedPoint_6108(int count, int index, int cd)
    {
        if (count > 0 && cd <= 0)
        {
            CharacterRecorder.instance.SetRedPoint(24, true);
            ItemC[3].transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(24, false);
            ItemC[3].transform.Find("RedPoint").gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 6501
    /// </summary>
    /// <param name="fulushu"></param>
    public void SetZhenFuRedPoint_6501(int fulushu)
    {
        // if (fulushu < GetKeFuLuNum()) 这个红点的条件不对
        if (CharacterRecorder.instance.isConquerRedPoint && TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zhengfu).Level < CharacterRecorder.instance.lastGateID)
        {
            CharacterRecorder.instance.SetRedPoint(14, true);
            ItemC[5].transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(14, false);
            ItemC[5].transform.Find("RedPoint").gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 6401
    /// </summary>
    /// <param name="fightCount"></param>
    /// <param name="cd"></param>
    public void SetWangZheRedPoint_6401(int fightCount, int cd)
    {
        if (fightCount > 0 && cd > 0)
        {

            if (CharacterRecorder.instance.lastGateID >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.wangzhezhilu).Level + 1 && CharacterRecorder.instance.KingChallengeNum > 0)
            {
                CharacterRecorder.instance.SetRedPoint(25, true);
                ItemC[7].transform.Find("RedPoint").gameObject.SetActive(true);
            }
            else
            {
                CharacterRecorder.instance.SetRedPoint(25, false);
                ItemC[7].transform.Find("RedPoint").gameObject.SetActive(false);
            }
        }
    }
    /// <summary>
    /// 6301
    /// </summary>
    /// <param name="count"></param>
    public void SetZouSiRedPoint_6301(int count, int cd)
    {
        if (count > 0 && cd == 0)
        {
            CharacterRecorder.instance.SetRedPoint(3, true);
            ItemC[6].transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(3, false);
            ItemC[6].transform.Find("RedPoint").gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 获取征服可俘虏数量
    /// </summary>
    /// <returns></returns>
    public int GetKeFuLuNum()
    {
        int CounperUpNumber = 0;
        if (CharacterRecorder.instance.level >= TextTranslator.instance.GetFortressByListId(1, 1).Level)
        {

            if (CharacterRecorder.instance.Vip < 3)
            {
                CounperUpNumber = 1;
            }
            else
            {
                CounperUpNumber = 2;
            }
        }

        if (CharacterRecorder.instance.level >= TextTranslator.instance.GetFortressByListId(1, 2).Level)
        {
            if (CharacterRecorder.instance.Vip < 3)
            {
                CounperUpNumber = 3;
            }
            else
            {
                CounperUpNumber = 4;
            }
        }
        if (CharacterRecorder.instance.level >= TextTranslator.instance.GetFortressByListId(1, 3).Level)
        {
            if (CharacterRecorder.instance.Vip < 3)
            {
                CounperUpNumber = 5;
            }
            else
            {
                CounperUpNumber = 6;
            }
        }
        return CounperUpNumber;
    }
    /// <summary>
    /// Toggle状态改变事件
    /// </summary>
    public void SetToogle_Changed()
    {
        int index = -1;

        if (UIToggle.current.value)
        {
            index = int.Parse(UIToggle.current.name[0] + "");
            UIToggle.current.transform.Find("Guang").gameObject.SetActive(true);
        }
        else
        {
            //int x = Convert.ToInt32(index);
            UIToggle.current.transform.Find("Guang").gameObject.SetActive(false);
        }
        //Debug.LogError("SetToogle_Changed:: " + index);
        if (!IsOpen(index))
        {
            return;
        }
        //if (UIToggle.current.transform.FindChild("Lock").gameObject.activeSelf)
        //{

        //}
        if (index != -1)
        {
            SetChallengeInfo(index);
        }
    }
    /// <summary>
    /// 判断某一个副本是否开启
    /// </summary>
    /// <param name="index">索引</param>
    /// <returns></returns>
    public bool IsOpen(int index)
    {
        bool isOpen = true;
        switch (index)
        {
            case 0:
                if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.hedianzhan).Level + 1)
                {
                    UIManager.instance.OpenPromptWindow("长官,通关" + (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.hedianzhan ).Level - 10000).ToString() + "关开放", PromptWindow.PromptType.Hint, null, null);
                    isOpen = false;
                }
                break;
            case 1:
                if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.jingjichang).Level + 1)
                {
                    UIManager.instance.OpenPromptWindow("长官,通关" + (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.jingjichang).Level - 10000).ToString() + "关开放", PromptWindow.PromptType.Hint, null, null);
                    isOpen = false;
                }
                break;
            case 2:
                if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.richangfuben).Level + 1)
                {
                    UIManager.instance.OpenPromptWindow("长官,通关" + (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.richangfuben).Level - 10000).ToString() + "关开放", PromptWindow.PromptType.Hint, null, null);
                    isOpen = false;
                }
                break;
            case 3:
                if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.duobao).Level + 1)
                {
                    UIManager.instance.OpenPromptWindow("长官,通关" + (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.duobao).Level - 10000).ToString() + "关开放", PromptWindow.PromptType.Hint, null, null);
                    isOpen = false;
                }
                break;
            case 4:
                if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zuduifuben).Level + 1)
                {
                    UIManager.instance.OpenPromptWindow("长官,通关" + (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zuduifuben).Level - 10000).ToString() + "关开放", PromptWindow.PromptType.Hint, null, null);
                    isOpen = false;
                }
                break;
            case 5:
                if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.conglingmaoxian).Level + 1)
                {
                    UIManager.instance.OpenPromptWindow("长官,通关" + (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.conglingmaoxian).Level - 10000).ToString() + "关开放", PromptWindow.PromptType.Hint, null, null);
                    isOpen = false;
                }
                break;
            case 6:
                if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zhengfu).Level + 1)
                {
                    UIManager.instance.OpenPromptWindow("长官,通关" + (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zhengfu).Level - 10000).ToString() + "关开放", PromptWindow.PromptType.Hint, null, null);
                    isOpen = false;
                }
                break;
            case 7:
                if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.bianjingzousi).Level + 1)
                {
                    UIManager.instance.OpenPromptWindow("长官,通关" + (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.bianjingzousi).Level - 10000).ToString() + "关开放", PromptWindow.PromptType.Hint, null, null);
                    isOpen = false;
                }
                break;
            case 8:
                if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.wangzhezhilu).Level + 1)
                {
                    UIManager.instance.OpenPromptWindow("长官,通关" + (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.wangzhezhilu).Level - 10000).ToString() + "关开放", PromptWindow.PromptType.Hint, null, null);
                    isOpen = false;
                }

                break;
            default:
                break;
        }
        return isOpen;
    }

    /// <summary>
    /// 设置Challenge的Info
    /// </summary>
    /// <param name="index">索引</param>
    public void SetChallengeInfo(int index)
    {
        ChallengeInfo.name = string.Format("C{0}", index);

        ChallengeInfo.GetComponent<UITexture>().mainTexture = Resources.Load(string.Format("MapTexture/map0{0}", GetTexture(index))) as Texture;
        //ChallengeInfo.transform.FindChild("LabelInfo").GetComponent<UILabel>().text = PresentChallengeName(index);
        UIEventListener.Get(ChallengeInfo).onClick = delegate(GameObject go)
        {
            EnterChapter(index);
        };
        UIEventListener.Get(GoBtn).onClick = delegate(GameObject go)
        {
            EnterChapter(index);
        };
        GetCenterNum(index);
    }

    public int GetTexture(int index)
    {
        int texture = 0;
        switch (index)
        {
            case 1:
                texture = 1;
                break;
            case 2:
                texture = 4;
                break;
            case 3:
                texture = 3;
                break;
            case 4:
                texture = 5;
                break;
            case 5:
                texture = 2;
                break;
            case 6:
                texture = 9;
                break;
            case 7:
                texture = 6;
                break;
            case 8:
                texture = 7;
                break;
            case 9:
                texture = 8;
                break;
            case 0:
                texture = 0;
                break;
            default:
                break;
        }
        return texture;
    }

    /// <summary>
    /// 根据索引获取当前的副本的名字
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public string PresentChallengeName(int index)
    {
        string name = string.Empty;
        switch (index)
        {
            case 1:
                name = "竞技场";
                break;
            case 2:
                name = "日常副本";
                break;
            case 3:
                name = "夺宝奇兵";
                break;
            case 4:
                name = "组队副本";
                break;
            case 5:
                name = "丛林冒险";
                break;
            case 6:
                name = "征服";
                break;
            case 7:
                name = "边境走私";
                break;
            case 8:
                name = "王者之路";
                break;
            case 9:
                name = "世界BOSS";
                break;
            case 0:
                name = "核电战";
                break;
            default:
                break;
        }

        return name;
    }
    /// <summary>
    /// 获取
    /// </summary>
    /// <returns></returns>
    public int GetRedPointPosition()
    {
        int num = 0;
        int redCount = 0;
        for (int i = 0; i < ItemC.Count; i++)
        {
            if (ItemC[i].transform.Find("RedPoint").gameObject.activeSelf)
            {
                num = i + 1;
                redCount = 1;
                break;
            }
        }
        if (redCount > 0)
        {
            CharacterRecorder.instance.SetRedPoint(21, true);
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(21, false);
        }
        return num;
    }

    public int NowItem(string name)
    {
        int index = 0;
        switch (name)
        {
            case "PVPWindow":
                index = 1;
                //GetCenterNum(1);
                break;
            case "EverydayWindow":
                index = 2;
                //GetCenterNum(2);
                break;
            case "GrabItemWindow":
                index = 3;
                //GetCenterNum(3);
                break;
            case "TeamCopyWindow":
                index = 4;
                //GetCenterNum(4);
                break;
            case "WoodsTheExpendables":
                index = 5;
                //GetCenterNum(5);
                break;
            case "ConquerWindow":
                index = 6;
                //GetCenterNum(6);
                break;
            case "SmuggleWindow":
                index = 7;
                //GetCenterNum(7);
                break;
            case "KingRoadWindow":
                index = 8;
                //GetCenterNum(8);
                break;
            case "WorldBossWindow":
                //GetCenterNum(9);
                index = 9;
                break;
            case "NuclearwarWindow":
                //GetCenterNum(9);
                index = 0;
                break;
            default:
                index = 1;
                //Debug.LogError("第一次打开.");
                break;
        }
        return index;
    }

    IEnumerator ButtonEffect()
    {
        LeftButton.transform.Find("LeftButton1").gameObject.SetActive(true);
        LeftButton.transform.Find("LeftButton2").gameObject.SetActive(false);
        RightButton.transform.Find("RightButton1").gameObject.SetActive(true);
        RightButton.transform.Find("RightButton2").gameObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        LeftButton.transform.Find("LeftButton1").gameObject.SetActive(false);
        LeftButton.transform.Find("LeftButton2").gameObject.SetActive(true);
        RightButton.transform.Find("RightButton1").gameObject.SetActive(false);
        RightButton.transform.Find("RightButton2").gameObject.SetActive(true);
    }

    void SetMoveChapter()
    {
        // Debug.LogError(uiSV.centeredObject.name);
        if (uiSV.centeredObject.name == "C1")
        {
            SetChapter(1);
            LeftButton.gameObject.SetActive(false);
            RightButton.gameObject.SetActive(true);
        }
        else if (uiSV.centeredObject.name == "C2")
        {
            SetChapter(2);
            LeftButton.gameObject.SetActive(true);
            RightButton.gameObject.SetActive(true);
        }
        else if (uiSV.centeredObject.name == "C3")
        {
            SetChapter(3);
            LeftButton.gameObject.SetActive(true);
            RightButton.gameObject.SetActive(true);
        }
        else if (uiSV.centeredObject.name == "C4")
        {
            SetChapter(4);
            LeftButton.gameObject.SetActive(true);
            RightButton.gameObject.SetActive(true);
        }
        else if (uiSV.centeredObject.name == "C5")
        {
            SetChapter(5);
            LeftButton.gameObject.SetActive(true);
            RightButton.gameObject.SetActive(true);
        }
        else if (uiSV.centeredObject.name == "C6")
        {
            SetChapter(6);
            LeftButton.gameObject.SetActive(true);
            RightButton.gameObject.SetActive(true);
        }
        else if (uiSV.centeredObject.name == "C7")
        {
            SetChapter(7);
            LeftButton.gameObject.SetActive(true);
            RightButton.gameObject.SetActive(true);
        }
        else if (uiSV.centeredObject.name == "C8")
        {
            SetChapter(8);
            LeftButton.gameObject.SetActive(true);
            RightButton.gameObject.SetActive(true);
        }
        else if (uiSV.centeredObject.name == "C9")
        {
            SetChapter(9);
            LeftButton.gameObject.SetActive(true);
            RightButton.gameObject.SetActive(false);
        }
        else if (uiSV.centeredObject.name == "C0")
        {
            SetChapter(0);
            LeftButton.gameObject.SetActive(true);
            RightButton.gameObject.SetActive(false);
        }
    }

    public void SetChapter(int _Chapter)
    {
        NowCenterNum = _Chapter;
        SpriteAward1.SetActive(true);
        SpriteAward2.SetActive(true);
        SpriteAward3.SetActive(true);
        SpriteAward4.SetActive(true);
        SpriteAward5.SetActive(false);
        SpriteAward1.transform.Find("Fragment").gameObject.SetActive(false);
        SpriteAward2.transform.Find("Fragment").gameObject.SetActive(false);
        SpriteAward3.transform.Find("Fragment").gameObject.SetActive(false);
        SpriteAward4.transform.Find("Fragment").gameObject.SetActive(false);
        SpriteAward5.transform.Find("Fragment").gameObject.SetActive(false);
        SpriteAward1.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
        SpriteAward2.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
        SpriteAward3.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
        SpriteAward4.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
        SpriteAward5.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
        switch (_Chapter)
        {
            case 1:
                LabelName.text = "争夺竞技排名，收获";
                LabelAward.text = "稀有资源";
                //LabelAward.transform.localPosition = new Vector3(LabelName.GetComponent<UILabel>().localSize.x, 2, 0);
                // LabelName.transform.localPosition = new Vector3(LabelAward.GetComponent<UILabel>().localSize.x * -0.5f, -2, 0);
                LabelAward.transform.localPosition = new Vector3(186, 96, 0);
                LabelName.transform.localPosition = new Vector3(-10, 0, 0);
                labelCurCName.text = "竞技场";

                SpriteAward1.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(60012).picID).itemGrade.ToString();
                SpriteAward2.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(90003).picID).itemGrade.ToString();
                SpriteAward3.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(10104).picID).itemGrade.ToString();
                SpriteAward4.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(90001).picID).itemGrade.ToString();
                SpriteAward1.transform.Find("Fragment").gameObject.SetActive(true);
                SpriteAward1.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().atlas = AvatarAtlas;
                SpriteAward1.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "60012";
                SpriteAward2.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "90003";
                SpriteAward3.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "10104";
                SpriteAward4.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "90001";

                SpriteAwardId1 = 70012;
                SpriteAwardId2 = 90003;
                SpriteAwardId3 = 10104;
                SpriteAwardId4 = 90001;
                TextTranslator.instance.ItemDescription(SpriteAward1, SpriteAwardId1, 0);
                TextTranslator.instance.ItemDescription(SpriteAward2, SpriteAwardId2, 0);
                TextTranslator.instance.ItemDescription(SpriteAward3, SpriteAwardId3, 0);
                TextTranslator.instance.ItemDescription(SpriteAward4, SpriteAwardId4, 0);
                break;
            case 2:
                LabelName.text = "挑战日常副本，领取";
                LabelAward.text = "大量资源";
                //LabelAward.transform.localPosition = new Vector3(LabelName.GetComponent<UILabel>().localSize.x, 2, 0);
                //LabelName.transform.localPosition = new Vector3(LabelAward.GetComponent<UILabel>().localSize.x * -0.5f, -2, 0);
                LabelAward.transform.localPosition = new Vector3(192, 96, 0);
                LabelName.transform.localPosition = new Vector3(-10, 0, 0);
                labelCurCName.text = "日常副本";
                SpriteAward1.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(90001).picID).itemGrade.ToString();
                SpriteAward2.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(10006).picID).itemGrade.ToString();
                SpriteAward3.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(10103).picID).itemGrade.ToString();
                SpriteAward4.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(10101).picID).itemGrade.ToString();
                SpriteAward1.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "90001";
                SpriteAward2.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "10006";
                SpriteAward3.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "10103";
                SpriteAward4.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "10101";

                SpriteAwardId1 = 90001;
                SpriteAwardId2 = 10006;
                SpriteAwardId3 = 10103;
                SpriteAwardId4 = 10101;
                TextTranslator.instance.ItemDescription(SpriteAward1, SpriteAwardId1, 0);
                TextTranslator.instance.ItemDescription(SpriteAward2, SpriteAwardId2, 0);
                TextTranslator.instance.ItemDescription(SpriteAward3, SpriteAwardId3, 0);
                TextTranslator.instance.ItemDescription(SpriteAward4, SpriteAwardId4, 0);
                break;
            case 3:
                LabelName.text = "抢夺饰品碎片，获取";
                LabelAward.text = "极品勋章";
                //LabelAward.transform.localPosition = new Vector3(LabelName.GetComponent<UILabel>().localSize.x, 2, 0);
                // LabelName.transform.localPosition = new Vector3(LabelAward.GetComponent<UILabel>().localSize.x * -0.5f, -2, 0);
                LabelAward.transform.localPosition = new Vector3(188, 96, 0);
                LabelName.transform.localPosition = new Vector3(-10, 0, 0);
                labelCurCName.text = "夺宝奇兵";
                SpriteAward1.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(51190).picID).itemGrade.ToString();
                SpriteAward2.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(52190).picID).itemGrade.ToString();
                SpriteAward3.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(53190).picID).itemGrade.ToString();
                SpriteAward4.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(90002).picID).itemGrade.ToString();
                SpriteAward1.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "51190";
                SpriteAward2.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "52190";
                SpriteAward3.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "53190";
                SpriteAward4.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "90002";

                SpriteAwardId1 = 51190;
                SpriteAwardId2 = 52190;
                SpriteAwardId3 = 53190;
                SpriteAwardId4 = 90002;
                TextTranslator.instance.ItemDescription(SpriteAward1, SpriteAwardId1, 0);
                TextTranslator.instance.ItemDescription(SpriteAward2, SpriteAwardId2, 0);
                TextTranslator.instance.ItemDescription(SpriteAward3, SpriteAwardId3, 0);
                TextTranslator.instance.ItemDescription(SpriteAward4, SpriteAwardId4, 0);
                break;
            case 4:
                LabelName.text = "挑战组队副本，获得";
                LabelAward.text = "大量装备";
                //LabelAward.transform.localPosition = new Vector3(LabelName.GetComponent<UILabel>().localSize.x, 2, 0);
                // LabelName.transform.localPosition = new Vector3(LabelAward.GetComponent<UILabel>().localSize.x * -0.5f, -2, 0);
                LabelAward.transform.localPosition = new Vector3(179, 96, 0);
                LabelName.transform.localPosition = new Vector3(-10, 0, 0);
                labelCurCName.text = "组队副本";
                SpriteAward1.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(40006).picID).itemGrade.ToString();
                SpriteAward2.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(40106).picID).itemGrade.ToString();
                SpriteAward3.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(40206).picID).itemGrade.ToString();
                SpriteAward4.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(40306).picID).itemGrade.ToString();
                SpriteAward1.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "40006";
                SpriteAward2.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "40106";
                SpriteAward3.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "40206";
                SpriteAward4.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "40306";

                SpriteAwardId1 = 40006;
                SpriteAwardId2 = 40106;
                SpriteAwardId3 = 40206;
                SpriteAwardId4 = 40306;

                TextTranslator.instance.ItemDescription(SpriteAward1, SpriteAwardId1, 0);
                TextTranslator.instance.ItemDescription(SpriteAward2, SpriteAwardId2, 0);
                TextTranslator.instance.ItemDescription(SpriteAward3, SpriteAwardId3, 0);
                TextTranslator.instance.ItemDescription(SpriteAward4, SpriteAwardId4, 0);
                break;
            case 5:
                LabelName.text = "手动战斗，获取";
                LabelAward.text = "大量金币";
                //LabelAward.transform.localPosition = new Vector3(LabelName.GetComponent<UILabel>().localSize.x, 2, 0);
                // LabelName.transform.localPosition = new Vector3(LabelAward.GetComponent<UILabel>().localSize.x * -0.5f, -2, 0);
                LabelAward.transform.localPosition = new Vector3(143, 96, 0);
                LabelName.transform.localPosition = new Vector3(-10, 0, 0);
                labelCurCName.text = "丛林冒险";
                SpriteAward1.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(60025).picID).itemGrade.ToString();
                SpriteAward2.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(90004).picID).itemGrade.ToString();
                SpriteAward3.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(90001).picID).itemGrade.ToString();
                SpriteAward4.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(59010).picID).itemGrade.ToString();
                SpriteAward1.transform.Find("Fragment").gameObject.SetActive(true);
                SpriteAward1.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().atlas = AvatarAtlas;
                SpriteAward1.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "60025";
                SpriteAward2.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "90004";
                SpriteAward3.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "90001";
                SpriteAward4.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "59010";

                SpriteAwardId1 = 70025;
                SpriteAwardId2 = 90004;
                SpriteAwardId3 = 90001;
                SpriteAwardId4 = 59010;

                TextTranslator.instance.ItemDescription(SpriteAward1, SpriteAwardId1, 0);
                TextTranslator.instance.ItemDescription(SpriteAward2, SpriteAwardId2, 0);
                TextTranslator.instance.ItemDescription(SpriteAward3, SpriteAwardId3, 0);
                TextTranslator.instance.ItemDescription(SpriteAward4, SpriteAwardId4, 0);
                break;
            case 6:
                LabelName.text = "征服全世界，获得";
                LabelAward.text = "大量金币&金条";
                //LabelAward.transform.localPosition = new Vector3(LabelName.GetComponent<UILabel>().localSize.x, 2, 0);
                // LabelName.transform.localPosition = new Vector3(LabelAward.GetComponent<UILabel>().localSize.x * -0.5f, -2, 0);
                LabelAward.transform.localPosition = new Vector3(111, 96, 0);
                LabelName.transform.localPosition = new Vector3(-10, 0, 0);
                labelCurCName.text = "征服";
                SpriteAward2.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(90001).picID).itemGrade.ToString();
                SpriteAward3.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(90006).picID).itemGrade.ToString();
                SpriteAward1.SetActive(false);
                SpriteAward3.SetActive(true);
                SpriteAward2.SetActive(true);
                SpriteAward4.SetActive(false);
                //SpriteAward5.SetActive(true);
                SpriteAward2.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "90001";
                SpriteAward3.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "90006";
                SpriteAwardId2 = 90001;
                SpriteAwardId3 = 90006;
                TextTranslator.instance.ItemDescription(SpriteAward2, SpriteAwardId2, 0);
                TextTranslator.instance.ItemDescription(SpriteAward3, SpriteAwardId3, 0);
                break;
            case 7:
                LabelName.text = "边境走私物资，获得";
                LabelAward.text = "大量金条";
                // LabelAward.transform.localPosition = new Vector3(LabelName.GetComponent<UILabel>().localSize.x, 2, 0);
                // LabelName.transform.localPosition = new Vector3(LabelAward.GetComponent<UILabel>().localSize.x * -0.5f, -2, 0);
                LabelAward.transform.localPosition = new Vector3(173, 96, 0);
                LabelName.transform.localPosition = new Vector3(-10, 0, 0);
                labelCurCName.text = "边境走私";
                SpriteAward5.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(90006).picID).itemGrade.ToString();
                SpriteAward1.SetActive(false);
                SpriteAward3.SetActive(false);
                SpriteAward2.SetActive(false);
                SpriteAward4.SetActive(false);
                SpriteAward5.SetActive(true);
                SpriteAward5.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "90006";
                SpriteAwardId5 = 90006;
                TextTranslator.instance.ItemDescription(SpriteAward5, SpriteAwardId5, 0);
                break;
            case 8:
                LabelName.text = "追寻王者之路，获得";
                LabelAward.text = "大量王者币";
                //LabelAward.transform.localPosition = new Vector3(LabelName.GetComponent<UILabel>().localSize.x, 2, 0);
                //  LabelName.transform.localPosition = new Vector3(LabelAward.GetComponent<UILabel>().localSize.x * -0.5f, -2, 0);
                LabelAward.transform.localPosition = new Vector3(143, 96, 0);
                LabelName.transform.localPosition = new Vector3(-10, 0, 0);
                labelCurCName.text = "王者之路";
                SpriteAward5.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(90010).picID).itemGrade.ToString();
                SpriteAward1.SetActive(false);
                SpriteAward3.SetActive(false);
                SpriteAward2.SetActive(false);
                SpriteAward4.SetActive(false);
                SpriteAward5.SetActive(true);
                SpriteAward5.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "90010";
                SpriteAwardId5 = 90010;
                TextTranslator.instance.ItemDescription(SpriteAward5, SpriteAwardId5, 0);
                break;
            case 9:
                LabelName.text = "讨伐BOSS，争夺";
                LabelAward.text = "红色勋章&手册";
                //LabelAward.transform.localPosition = new Vector3(LabelName.GetComponent<UILabel>().localSize.x, 2, 0);
                // LabelName.transform.localPosition = new Vector3(LabelAward.GetComponent<UILabel>().localSize.x * -0.5f, -2, 0);
                LabelAward.transform.localPosition = new Vector3(117, 96, 0);
                LabelName.transform.localPosition = new Vector3(-10,0, 0);
                labelCurCName.text = "世界BOSS";
                //SpriteAward5.GetComponent<UISprite>().spriteName = "Grade5";
                SpriteAward1.SetActive(false);
                SpriteAward3.SetActive(true);
                SpriteAward2.SetActive(true);
                SpriteAward4.SetActive(false);
                SpriteAward5.SetActive(false);

                SpriteAward2.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "51190";
                SpriteAward3.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "51200";
                SpriteAward2.transform.Find("Fragment").gameObject.SetActive(true);
                SpriteAward3.transform.Find("Fragment").gameObject.SetActive(true);
                SpriteAwardId2 = 81190;
                SpriteAwardId3 = 81200;
                //SpriteAward5.GetComponent<UISprite>().spriteName = "Grade5";
                SpriteAward2.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(51190).picID).itemGrade.ToString();
                SpriteAward3.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(51200).picID).itemGrade.ToString();
                TextTranslator.instance.ItemDescription(SpriteAward2, SpriteAwardId2, 0);
                TextTranslator.instance.ItemDescription(SpriteAward3, SpriteAwardId3, 0);
                break;
            case 0:
                LabelName.text = "攻击核电战，获得";
                LabelAward.text = "反应原料&红色万能碎片";
                LabelAward.transform.localPosition = new Vector3(-74, 94, 0);
                LabelName.transform.localPosition = new Vector3(322, 45, 0);
                labelCurCName.text = "核电战";
                //SpriteAward5.GetComponent<UISprite>().spriteName = "Grade5";
                SpriteAward1.SetActive(false);
                SpriteAward3.SetActive(true);
                SpriteAward2.SetActive(true);
                SpriteAward4.SetActive(false);
                SpriteAward5.SetActive(false);
                SpriteAward2.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "79999";
                SpriteAward3.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = "10106";
                SpriteAward2.transform.Find("Fragment").gameObject.SetActive(true);
                SpriteAward3.transform.Find("Fragment").gameObject.SetActive(false);
                SpriteAward2.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(79999).picID).itemGrade.ToString();
                SpriteAward3.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(10106).picID).itemGrade.ToString();
                SpriteAwardId2 = 79999;
                SpriteAwardId3 = 10106;
                SpriteAward2.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(79999).picID).itemGrade.ToString();
                SpriteAward3.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetItemByItemCode(10106).picID).itemGrade.ToString();
                TextTranslator.instance.ItemDescription(SpriteAward2, SpriteAwardId2, 0);
                TextTranslator.instance.ItemDescription(SpriteAward3, SpriteAwardId3, 0);
                break;

        }

        uiGrid.GetComponent<UIGrid>().Reposition();
        //for (int i = 0; i < ItemC.Count; i++)
        //{
        //    if (i != _Chapter - 1)
        //    {
        //        ItemC[i].transform.Find("Chapter").GetComponent<UITexture>().MakePixelPerfect();
        //        ItemC[i].transform.Find("Chapter").GetComponent<UITexture>().width = 460 * 4 / 5;
        //        ItemC[i].transform.Find("Chapter").GetComponent<UITexture>().height = 460 * 4 / 5;
        //    }
        //    else
        //    {
        //        ItemC[i].transform.Find("Chapter").GetComponent<UITexture>().width = 460;
        //        ItemC[i].transform.Find("Chapter").GetComponent<UITexture>().height = 460;

        //    }
        //}

    }
    void UpdateWindowSize()
    {
        //    Debug.LogError("sssssss" + Screen.width);
        if ((float)Screen.width / (float)Screen.height < 1.52f)
        {
            ScrollView.transform.localScale = new Vector3(0.85f, 0.85f, 0.85f);
            ScrollViewSize = 0.85f;
            //ScrollGrid.GetComponent<UIGrid>().cellWidth = 365f;
            ScrollView.transform.localPosition = new Vector3(ScrollView.transform.localPosition.x * 0.85f, ScrollView.transform.localPosition.y, ScrollView.transform.localPosition.z);
        }
        else
        {
            ScrollViewSize = 1f;
            ScrollView.transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void EnterChapter(int _Chapter)
    {
        if (CharacterRecorder.instance.GuideID[21] == 6 || CharacterRecorder.instance.GuideID[9] == 6 || CharacterRecorder.instance.GuideID[10] == 6
            || CharacterRecorder.instance.GuideID[12] == 6 || CharacterRecorder.instance.GuideID[38] == 11 || CharacterRecorder.instance.GuideID[13] == 6
            || CharacterRecorder.instance.GuideID[14] == 6 || CharacterRecorder.instance.GuideID[36] == 6)
        {
            SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
        }
        switch (_Chapter)
        {
            case 1:
                UIManager.instance.OpenPanel("PVPWindow", true);
                UIManager.instance.CountSystem(UIManager.Systems.竞技场);
                UIManager.instance.UpdateSystems(UIManager.Systems.竞技场);
                break;
            case 2:
                UIManager.instance.OpenPanel("EverydayWindow", true);
                UIManager.instance.CountSystem(UIManager.Systems.日常副本);
                UIManager.instance.UpdateSystems(UIManager.Systems.日常副本);
                break;
            case 3:
                UIManager.instance.OpenPanel("GrabItemWindow", true);
                UIManager.instance.CountSystem(UIManager.Systems.夺宝奇兵);
                UIManager.instance.UpdateSystems(UIManager.Systems.夺宝奇兵);
                break;
            case 4:
                UIManager.instance.OpenPanel("TeamCopyWindow", true);
                UIManager.instance.CountSystem(UIManager.Systems.组队副本);
                UIManager.instance.UpdateSystems(UIManager.Systems.组队副本);
                break;
            case 5:
                NetworkHandler.instance.SendProcess("1501#;");
                UIManager.instance.CountSystem(UIManager.Systems.丛林冒险);
                UIManager.instance.UpdateSystems(UIManager.Systems.丛林冒险);
                break;
            case 6:
                if (CharacterRecorder.instance.GuideID[58] == 3)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
                NetworkHandler.instance.SendProcess("6501#");
                UIManager.instance.CountSystem(UIManager.Systems.征服);
                UIManager.instance.UpdateSystems(UIManager.Systems.征服);
                break;
            case 7:
                UIManager.instance.OpenPanel("SmuggleWindow", true);
                UIManager.instance.CountSystem(UIManager.Systems.边境走私);
                UIManager.instance.UpdateSystems(UIManager.Systems.边境走私);
                break;
            case 8:
                UIManager.instance.OpenPanel("KingRoadWindow", true);
                UIManager.instance.CountSystem(UIManager.Systems.王者之路);
                UIManager.instance.UpdateSystems(UIManager.Systems.王者之路);
                break;
            case 9:
                UIManager.instance.OpenPanel("WorldBossWindow", true);
                UIManager.instance.CountSystem(UIManager.Systems.世界boss);
                UIManager.instance.UpdateSystems(UIManager.Systems.世界boss);
                break;
            case 0:
                UIManager.instance.OpenPanel("NuclearwarWindow", true);
                break;
        }
    }

    public void IsShowMessage()
    {
        List<Item> ItemList = new List<Item>();
        ItemList = TextTranslator.instance.GetAllGrabInBag();
        List<Item> FinishItemList = TextTranslator.instance.GetFinishItem(ItemList);
        if (FinishItemList.Count > 0 || CharacterRecorder.instance.GrabIntegrationRedPoint)
        {
            CharacterRecorder.instance.SetRedPoint(23, true);
            ItemC[2].transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else
        {

            CharacterRecorder.instance.SetRedPoint(23, false);
            ItemC[2].transform.Find("RedPoint").gameObject.SetActive(false);
        }
        if (CharacterRecorder.instance.CanGetRewardID > CharacterRecorder.instance.HadRewardID)
        {

            CharacterRecorder.instance.SetRedPoint(2, true);
            ItemC[4].transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else
        {

            CharacterRecorder.instance.SetRedPoint(2, false);
            ItemC[4].transform.Find("RedPoint").gameObject.SetActive(false);
        }
        if (CharacterRecorder.instance.SmuggleNum > 0 && CharacterRecorder.instance.SmuggleTime == 0 && CharacterRecorder.instance.lastGateID >= 10087)
        {

            CharacterRecorder.instance.SetRedPoint(3, true);
            ItemC[6].transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else
        {

            CharacterRecorder.instance.SetRedPoint(3, false);
            ItemC[6].transform.Find("RedPoint").gameObject.SetActive(false);
        }
        //for (int i = 0; i < CharacterRecorder.instance.EveryDayNumberRedPoint.Count; i++)
        //{
        //    if (CharacterRecorder.instance.EveryDayNumberRedPoint[i] != 0 || CharacterRecorder.instance.EveryDayTimerRedPoint[i] != 0)
        //    {

        //        CharacterRecorder.instance.SetRedPoint(22, true);
        //        ItemC[1].transform.Find("RedPoint").gameObject.SetActive(true);
        //        break;
        //    }
        //    else
        //    {
        //        CharacterRecorder.instance.SetRedPoint(22, false);
        //        ItemC[1].transform.Find("RedPoint").gameObject.SetActive(false);

        //    }
        //}
        //14征服
        //if (CharacterRecorder.instance.isConquerRedPoint && TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zhengfu).Level < CharacterRecorder.instance.lastGateID)
        //{
        //    CharacterRecorder.instance.SetRedPoint(14, true);
        //    ItemC[5].transform.Find("RedPoint").gameObject.SetActive(true);
        //}
        //else
        //{
        //    CharacterRecorder.instance.SetRedPoint(14, false);
        //    ItemC[5].transform.Find("RedPoint").gameObject.SetActive(false);
        //}
    }

    public void isLock()
    {
        if (CharacterRecorder.instance.lastGateID >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.jingjichang).Level + 1)
        {
            ItemC[0].transform.Find("Lock").gameObject.SetActive(false);
        }
        else
        {
            ItemC[0].transform.Find("Lock").gameObject.SetActive(true);
            ItemC[0].GetComponent<UITexture>().shader = Shader.Find("Unlit/GrayShader");
        }
        if (CharacterRecorder.instance.lastGateID >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.richangfuben).Level + 1)
        {
            ItemC[1].transform.Find("Lock").gameObject.SetActive(false);
        }
        else
        {
            ItemC[1].transform.Find("Lock").gameObject.SetActive(true);
            ItemC[1].GetComponent<UITexture>().shader = Shader.Find("Unlit/GrayShader");
        }
        if (CharacterRecorder.instance.lastGateID >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.duobao).Level + 1)
        {
            ItemC[2].transform.Find("Lock").gameObject.SetActive(false);
        }
        else
        {
            ItemC[2].transform.Find("Lock").gameObject.SetActive(true);
            ItemC[2].GetComponent<UITexture>().shader = Shader.Find("Unlit/GrayShader");
        }
        if (CharacterRecorder.instance.lastGateID >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zuduifuben).Level + 1)
        {
            ItemC[3].transform.Find("Lock").gameObject.SetActive(false);
        }
        else
        {
            ItemC[3].transform.Find("Lock").gameObject.SetActive(true);
            ItemC[3].GetComponent<UITexture>().shader = Shader.Find("Unlit/GrayShader");
        }
        if (CharacterRecorder.instance.lastGateID >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.conglingmaoxian).Level + 1)
        {
            ItemC[4].transform.Find("Lock").gameObject.SetActive(false);
        }
        else
        {
            ItemC[4].transform.Find("Lock").gameObject.SetActive(true);
            ItemC[4].GetComponent<UITexture>().shader = Shader.Find("Unlit/GrayShader");
        }
        if (CharacterRecorder.instance.lastGateID >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zhengfu).Level + 1)
        {
            ItemC[5].transform.Find("Lock").gameObject.SetActive(false);
        }
        else
        {
            ItemC[5].transform.Find("Lock").gameObject.SetActive(true);
            ItemC[5].GetComponent<UITexture>().shader = Shader.Find("Unlit/GrayShader");
        }
        if (CharacterRecorder.instance.lastGateID >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.bianjingzousi).Level + 1)
        {
            ItemC[6].transform.Find("Lock").gameObject.SetActive(false);
        }
        else
        {
            ItemC[6].transform.Find("Lock").gameObject.SetActive(true);
            ItemC[6].GetComponent<UITexture>().shader = Shader.Find("Unlit/GrayShader");
        }
        if (CharacterRecorder.instance.lastGateID >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.wangzhezhilu).Level + 1)
        {
            ItemC[7].transform.Find("Lock").gameObject.SetActive(false);
        }
        else
        {
            ItemC[7].transform.Find("Lock").gameObject.SetActive(true);
            ItemC[7].GetComponent<UITexture>().shader = Shader.Find("Unlit/GrayShader");
        }
        if (CharacterRecorder.instance.lastGateID >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.hedianzhan).Level + 1)
        {
            ItemC[9].transform.Find("Lock").gameObject.SetActive(false);
        }
        else
        {
            ItemC[9].transform.Find("Lock").gameObject.SetActive(true);
            ItemC[9].GetComponent<UITexture>().shader = Shader.Find("Unlit/GrayShader");
        }
        ItemC[8].transform.Find("Lock").gameObject.SetActive(false);
    }



    IEnumerator IsShowPvPMessage()
    {
        yield return new WaitForSeconds(0.05f);

        if (CharacterRecorder.instance.lastGateID > TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.jingjichang).Level)
        {
            if ((CharacterRecorder.instance.GetPointLayer < 10 && CharacterRecorder.instance.PvpPoint >= CharacterRecorder.instance.GetPointLayer * 2 + 2
                    || CharacterRecorder.instance.GetRankLayer < CharacterRecorder.instance.HaveRankLayer || CharacterRecorder.instance.PvpChallengeNum > 0 && CharacterRecorder.instance.PvpRefreshTime == 0))//&& CharacterRecorder.instance.level >= 12
            {
                CharacterRecorder.instance.SetRedPoint(5, true);
                ItemC[0].transform.Find("RedPoint").gameObject.SetActive(true);
            }
            else
            {
                CharacterRecorder.instance.SetRedPoint(5, false);
                ItemC[0].transform.Find("RedPoint").gameObject.SetActive(false);
            }
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(5, false);
            ItemC[0].transform.Find("RedPoint").gameObject.SetActive(false);
        }

    }
    /// <summary>
    /// 王者之路
    /// </summary>
    /// <returns></returns>
    IEnumerator IsShowTeamCopyMessage()
    {
        NetworkHandler.instance.SendProcess("6401#");
        yield return new WaitForSeconds(0.5f);
        if (CharacterRecorder.instance.lastGateID >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.wangzhezhilu).Level + 1 && CharacterRecorder.instance.KingChallengeNum > 0)
        {
            CharacterRecorder.instance.SetRedPoint(24, true);
            ItemC[7].transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(24, false);
            ItemC[7].transform.Find("RedPoint").gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 组队红点
    /// </summary>
    /// <returns></returns>
    IEnumerator IsShowKingRoadMessage()
    {
        NetworkHandler.instance.SendProcess("6108#");
        yield return new WaitForSeconds(0.5f);
        if (CharacterRecorder.instance.TeamFightNum > 0 && ItemC[3].transform.Find("Lock").gameObject.activeSelf == false && CharacterRecorder.instance.lastGateID >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.wangzhezhilu).Level + 1)
        {
            CharacterRecorder.instance.SetRedPoint(25, true);
            ItemC[3].transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(25, false);
            ItemC[3].transform.Find("RedPoint").gameObject.SetActive(false);
        }
    }
    //新手引导居中
    public void GetCenterNum(int num)
    {
        //switch (num)
        //{
        //    case 1:
        //        LeftButton.gameObject.SetActive(false);
        //        RightButton.gameObject.SetActive(true);
        //        break;
        //    case 2:
        //    case 3:
        //    case 4:
        //    case 5:
        //    case 6:
        //    case 7:
        //    case 8:
        //        LeftButton.gameObject.SetActive(true);
        //        RightButton.gameObject.SetActive(true);
        //        break;
        //    case 9:
        //        LeftButton.gameObject.SetActive(true);
        //        RightButton.gameObject.SetActive(false);
        //        break;
        //    default:
        //        break;
        //}
        //if (ScrollViewSize == 0.85f)
        //{

        //    ScrollView.transform.localPosition = new Vector3(365 * (num - 1) * -1, 68, 0);
        //    if (num >= 6)
        //    {
        //        ScrollView.GetComponent<UIPanel>().clipOffset = new Vector3(365 * (num - 1) + 400, 0, 0);
        //    }
        //    else if (num >= 7)
        //    {
        //        ScrollView.GetComponent<UIPanel>().clipOffset = new Vector3(365 * (num - 1) + 500, 0, 0);
        //    }

        //    else
        //    {
        //        ScrollView.GetComponent<UIPanel>().clipOffset = new Vector3(365 * (num - 1) + 195, 0, 0);
        //    }
        //    Debug.LogError("ssss" + ScrollViewSize + "      " + ScrollView.transform.localPosition + "   " + ScrollView.GetComponent<UIPanel>().clipOffset);
        //}
        //else
        //{
        //    GameObject.Find("ChapterParent/Scroll View").GetComponent<UIPanel>().clipOffset = new Vector3(430 * (num - 1), 0, 0);
        //    GameObject.Find("ChapterParent/Scroll View").transform.localPosition = new Vector3(430 * (num - 1) * -1, 68, 0);
        //}
        SetChapter(num);

    }

}
