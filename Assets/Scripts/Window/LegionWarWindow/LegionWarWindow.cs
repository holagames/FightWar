using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionInfo
{
    public int PointID;
    public int LegionID;
    public string LegionName;
    public int LegionFlag;
    public int LegionState;
    public int LegionNum;
    public int CountryId;


    public LegionInfo(int _PointID, int _LegionID, string _LegionName, int _LegionFlag, int _LegionState, int _LegionNum,int _CountryId)
    {
        this.PointID = _PointID;
        this.LegionID = _LegionID;
        this.LegionName = _LegionName;
        this.LegionFlag = _LegionFlag;
        this.LegionState = _LegionState;
        this.LegionNum = _LegionNum;
        this.CountryId = _CountryId;
    }
}



public class LegionWarWindow : MonoBehaviour
{
    public GameObject ChatCloseButton;
    public GameObject[] ArrLegionPoint = new GameObject[41];
    public GameObject BackButton;
    public GameObject QuestionButton;

    public GameObject PointParent;
    public GameObject ButtonParent;

    public GameObject LegionInfoWindow;
    public GameObject GainResultPart;
    public GameObject LookAwardPart;
    public GameObject HunterMessage;
    public GameObject ExchangePart;
    public GameObject LegionRankPart;
    public GameObject KillRankInfo;
    public GameObject FirstRankInfo;

    public GameObject NoOneOccupation;
    public GameObject SomeOneOccupation;
    public GameObject LockInfoOccupation;
    public GameObject NoOneHarassment;
    public GameObject SomeOneHarassment;
    public GameObject MyHarassment;
    public GameObject ZhuHarassment;

    public GameObject AwardButton;
    public GameObject CloseButton;

    public GameObject AwardItem;
    public GameObject EffectCG;
    public GameObject EffectSB;
    public GameObject EffectBZ;
    public GameObject EffectGuang;

    public UIAtlas RoleAtlas;
    public UIAtlas ItemAtlas;

    public GameObject LegionMessage1;
    public GameObject LegionMessage2;
    public GameObject LegionMessage3;

    public GameObject ClickPointInfoWindow;//信息窗口
    public GameObject ClickLittlePointWindow;//骚扰点信息窗口


    public GameObject DuiHuanButton;
    public UILabel JungongLabel;
    public UILabel XingdongLabel;

    public GameObject MaskBg;
    public GameObject InfoWindowMask;
    public List<LegionTeamPosition> mTeamPosition = new List<LegionTeamPosition>();
    private List<LegionInfo> LegionInfoList = new List<LegionInfo>();//占领的
    private List<LegionInfo> OneTypeLegionList = new List<LegionInfo>();//排序后占领的
    private int ActionPoint;//行动点数
    private int JunGongPoint;//军工


    private bool IsOnceInWindow = true;//判断是否第一次进入界面，领取收益按钮协议

    public GameObject ZhanButton;
    public GameObject XuanButton;
    public GameObject ChaButton;
    public GameObject GongButton;
    public GameObject FangButton;
    public GameObject GuanButton;
    public GameObject JiButton;


    public GameObject ZhanbaoButton;
    public GameObject JunxianButton;
    public GameObject JiangliButton;
    public GameObject StayCountryName;


    public GameObject TeamButton1;
    public GameObject TeamButton2;
    public GameObject TeamButton3;


    public GameObject TeamHero1;
    public GameObject TeamHero2;
    public GameObject TeamHero3;

    private MarinesInfomation MaIn1;
    private MarinesInfomation MaIn2;
    private MarinesInfomation MaIn3;


    public GameObject FirstEnterParent;
    public GameObject PopupWindow;
    public UILabel LabelMessage;
    public TweenScale TweenBg;
    public TweenScale TweenMessage;

    public UISprite KongBai;
    public GameObject TogglebrushCity;//自动刷城按钮
    private List<int> brushCityIdlist=new List<int>();//满足条件的资源城市id;
    private List<int> brushCityIdNoGoodList = new List<int>();//d级非资源点城市id

    private List<string> PopupList = new List<string>();
    private UILabel TimeNumber;
    private int timeNum;
    void OnEnable()
    {
        NetworkHandler.instance.SendProcess("8601#;");  //取得城市点状态
        NetworkHandler.instance.SendProcess("8611#;");  //取得累计收益红点
        NetworkHandler.instance.SendProcess("8627#;");  //取得杀敌排行榜首位
        NetworkHandler.instance.SendProcess("8630#;");  //取得奖励红点
        NetworkHandler.instance.SendProcess("8636#;");  //取得战队位置
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
    void Start()
    {
        //GameObject.Find("UIRoot").transform.Find("SelfAdaptionBlackWindow/Up").gameObject.SetActive(false);
        FirstEnterPlayAnimation();
        IsOpenTogglebrushCity();
        if (UIEventListener.Get(BackButton).onClick == null)
        {
            UIEventListener.Get(BackButton).onClick = delegate(GameObject go)
            {
                UIManager.instance.BackUI();
                GameObject.Find("UIRoot").transform.Find("SelfAdaptionBlackWindow/Up").gameObject.SetActive(true);
            };
        }
        if (UIEventListener.Get(AwardButton).onClick == null)
        {
            UIEventListener.Get(AwardButton).onClick = delegate(GameObject go)
            {
                IsOnceInWindow = false;
                NetworkHandler.instance.SendProcess("8611#;");
            };
        }

        if (UIEventListener.Get(QuestionButton).onClick == null)
        {
            UIEventListener.Get(QuestionButton).onClick = delegate(GameObject go)
            {
                HunterMessage.SetActive(true);
                ClickPointInfoWindow.SetActive(false);
                ClickLittlePointWindow.SetActive(false);
            };
        }

        if (UIEventListener.Get(HunterMessage.transform.Find("CloseButton").gameObject).onClick == null)
        {
            UIEventListener.Get(HunterMessage.transform.Find("CloseButton").gameObject).onClick = delegate(GameObject go)
            {
                HunterMessage.SetActive(false);
            };
        }

        if (UIEventListener.Get(ClickPointInfoWindow.transform.Find("BgMask").gameObject).onClick == null)
        {
            UIEventListener.Get(ClickPointInfoWindow.transform.Find("BgMask").gameObject).onClick = delegate(GameObject go)
            {
                ClickPointInfoWindow.SetActive(false);
            };
        }

        if (UIEventListener.Get(ClickLittlePointWindow.transform.Find("BgMask").gameObject).onClick == null)
        {
            UIEventListener.Get(ClickLittlePointWindow.transform.Find("BgMask").gameObject).onClick = delegate(GameObject go)
            {
                ClickLittlePointWindow.SetActive(false);
            };
        }

        if(UIEventListener.Get(KillRankInfo).onClick==null)
        {
            UIEventListener.Get(KillRankInfo).onClick += delegate(GameObject go)
            {
                CharacterRecorder.instance.LegionRankListButtonType = 4;
                UIManager.instance.OpenPanel("RankListWindow", true);
                //Invoke("SetRankListWindowOnButton", 1f);
            };
        }

        if (UIEventListener.Get(FirstRankInfo).onClick == null)
        {
            UIEventListener.Get(FirstRankInfo).onClick += delegate(GameObject go)
            {
                LookOfLegionRankPart();
            };
        }

        if (UIEventListener.Get(GameObject.Find("LegionWarWindow/ObjParent")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("LegionWarWindow/ObjParent")).onClick += delegate(GameObject go)
            {
                ClickPointInfoWindow.SetActive(false);
                ClickLittlePointWindow.SetActive(false);
            };
        }

        if (UIEventListener.Get(InfoWindowMask).onClick == null)
        {
            UIEventListener.Get(InfoWindowMask).onClick += delegate(GameObject go)
            {
                LegionInfoWindow.SetActive(false);
            };
        }
        if (UIEventListener.Get(ChatCloseButton).onClick == null)
        {
            UIEventListener.Get(ChatCloseButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenSinglePanel("ChatWindowNew", false);
            };
        }

        if (UIEventListener.Get(ZhanbaoButton).onClick == null)
        {
            UIEventListener.Get(ZhanbaoButton).onClick += delegate(GameObject go)
            {
                ClickPointInfoWindow.SetActive(false);
                ClickLittlePointWindow.SetActive(false);
                UIManager.instance.OpenSinglePanel("BattlefieldReportWindow", false);
            };
        }
        if (UIEventListener.Get(JunxianButton).onClick == null)
        {
            UIEventListener.Get(JunxianButton).onClick += delegate(GameObject go)
            {
                ClickPointInfoWindow.SetActive(false);
                ClickLittlePointWindow.SetActive(false);
                UIManager.instance.OpenSinglePanel("MilitaryRankWindow", false);
            };
        }
        if (UIEventListener.Get(JiangliButton).onClick == null)
        {
            UIEventListener.Get(JiangliButton).onClick += delegate(GameObject go)
            {
                ClickPointInfoWindow.SetActive(false);
                ClickLittlePointWindow.SetActive(false);
                UIManager.instance.OpenSinglePanel("MeritAwardWindow", false);
            };
        }

        if (UIEventListener.Get(TeamButton1).onClick == null)
        {
            UIEventListener.Get(TeamButton1).onClick += delegate(GameObject go)
            {
                ClickPointInfoWindow.SetActive(false);
                ClickLittlePointWindow.SetActive(false);
                CharacterRecorder.instance.MarinesTabe = 1;
                if (TeamButton1.transform.Find("HeroGrade").gameObject.activeSelf)
                {
                    UIManager.instance.OpenSinglePanel("LegionTeamWindow", false);
                }
                else if (TeamButton1.transform.Find("TimeLabel").gameObject.activeSelf)
                {
                    //NetworkHandler.instance.SendProcess("8617#;");
                    UIManager.instance.OpenPromptWindow("还在CD时间!", PromptWindow.PromptType.Hint, null, null);
                }
                else if (CharacterRecorder.instance.LegionActionPoint <= 0) 
                {
                    UIManager.instance.OpenPromptWindow("行动力不足!", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    UIManager.instance.OpenSinglePanel("LegionPositionWindow", false);
                }
            };
        }


        if (UIEventListener.Get(TeamButton2).onClick == null)
        {
            UIEventListener.Get(TeamButton2).onClick += delegate(GameObject go)
            {
                ClickPointInfoWindow.SetActive(false);
                ClickLittlePointWindow.SetActive(false);
                CharacterRecorder.instance.MarinesTabe = 2;
                if (TeamButton2.transform.Find("HeroGrade").gameObject.activeSelf)
                {
                    UIManager.instance.OpenSinglePanel("LegionTeamWindow", false);
                }
                else if (TeamButton2.transform.Find("TimeLabel").gameObject.activeSelf)
                {
                    //NetworkHandler.instance.SendProcess("8617#;");
                    UIManager.instance.OpenPromptWindow("还在CD时间!", PromptWindow.PromptType.Hint, null, null);
                }
                else if (CharacterRecorder.instance.LegionActionPoint <= 0)
                {
                    UIManager.instance.OpenPromptWindow("行动力不足!", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    UIManager.instance.OpenSinglePanel("LegionPositionWindow", false);
                }
            };
        }

        if (UIEventListener.Get(TeamButton3).onClick == null)
        {
            UIEventListener.Get(TeamButton3).onClick += delegate(GameObject go)
            {
                ClickPointInfoWindow.SetActive(false);
                ClickLittlePointWindow.SetActive(false);
                CharacterRecorder.instance.MarinesTabe = 3;
                if (TeamButton3.transform.Find("HeroGrade").gameObject.activeSelf)
                {
                    UIManager.instance.OpenSinglePanel("LegionTeamWindow", false);
                }
                else if (TeamButton3.transform.Find("TimeLabel").gameObject.activeSelf)
                {
                    //NetworkHandler.instance.SendProcess("8617#;");
                    UIManager.instance.OpenPromptWindow("还在CD时间!", PromptWindow.PromptType.Hint, null, null);
                }
                else if (CharacterRecorder.instance.LegionActionPoint <= 0)
                {
                    UIManager.instance.OpenPromptWindow("行动力不足!", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    UIManager.instance.OpenSinglePanel("LegionPositionWindow", false);
                }
            };
        }

        UIEventListener.Get(DuiHuanButton).onClick = delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("8621#;");
            ClickPointInfoWindow.SetActive(false);
            ClickLittlePointWindow.SetActive(false);
        };

        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                LegionInfoWindow.SetActive(false);
            };
        }
        if (CharacterRecorder.instance.Vip >= 8)
        {
            TogglebrushCity.SetActive(true);
        }
        else 
        {
            TogglebrushCity.SetActive(false);
        }

        UIEventListener.Get(TogglebrushCity).onClick = delegate(GameObject go)
        {
            ClickTogglebrushCity();
        };
        MyLegionIsWitchCountry();
    }



    /// <summary>
    /// 自己所属同盟或帝国显示
    /// </summary>
    void MyLegionIsWitchCountry() 
    {
        if (CharacterRecorder.instance.legionCountryID == 0) 
        {
            StayCountryName.SetActive(false);
        }
        else if (CharacterRecorder.instance.legionCountryID == 1)
        {
            StayCountryName.SetActive(true);
            StayCountryName.GetComponent<UISprite>().spriteName = "guozhanicon1";
            StayCountryName.transform.Find("CountryLabel").GetComponent<UILabel>().text = "同盟";
        }
        else 
        {
            StayCountryName.SetActive(true);
            StayCountryName.GetComponent<UISprite>().spriteName = "guozhanicon2";
            StayCountryName.transform.Find("CountryLabel").GetComponent<UILabel>().text = "帝国";
        }
    }


    #region 旧的8601，8602协议,暂时都改成骚扰，以后跨服用，不删
    /* 
    /// <summary>
    /// 初始化所有城市点信息，8601协议   旧的，暂时不用，后面跨服战可能会用，不删
    /// </summary>
    public void LegionWarGetScene(string Recving)
    {
        LegionInfoList.Clear();
        string[] dataSplit = Recving.Split(';');
        ActionPoint = int.Parse(dataSplit[0]);
        JunGongPoint = int.Parse(dataSplit[1]);
        JungongLabel.text = JunGongPoint.ToString();
        XingdongLabel.text = ActionPoint.ToString();
        CharacterRecorder.instance.LegionActionPoint = ActionPoint;
        List<int> BiaoJiCity = new List<int>();    //哪些标记点
        string[] prcSplit = dataSplit[3].Split('!');
        for (int i = 0; i < prcSplit.Length - 1; i++)
        {
            string[] qrcSplit = prcSplit[i].Split('$');
            BiaoJiCity.Add(int.Parse(qrcSplit[0]));
        }


        for (int i = 4; i < dataSplit.Length - 1; i++)
        {
            string[] trcSplit = dataSplit[i].Split('$');

            GameObject CityObj = ArrLegionPoint[int.Parse(trcSplit[0]) - 1];

            CityObj.name = trcSplit[0];
            CityObj.transform.Find("CityName").GetComponent<UILabel>().text = TextTranslator.instance.GetLegionCityByID(int.Parse(trcSplit[0])).CityName;
            CityObj.transform.Find("EffectState").gameObject.SetActive(false);  //火焰特效
            CityObj.transform.Find("EffectRedColor").gameObject.SetActive(false);//宣战红色特效
            CityObj.transform.Find("EffectLeiDaBo").gameObject.SetActive(false);//占领雷达波特效
            CityObj.transform.Find("JihuoEffect").gameObject.SetActive(false);  //集火特效

            if (CharacterRecorder.instance.legionID != 0) //我有加入军团，判断集火标志
            {
                if (BiaoJiCity.Contains(int.Parse(trcSplit[0]))) //此点被标记
                {
                    CityObj.transform.Find("JihuoEffect").gameObject.SetActive(true);
                    //if (trcSplit[2] == CharacterRecorder.instance.myLegionData.legionName) //防旗子
                    //{
                    //    CityObj.transform.Find("ChangeIcon").gameObject.SetActive(true);
                    //    CityObj.transform.Find("ChangeIcon").GetComponent<UISprite>().spriteName = "Fang_000";
                    //    CityObj.transform.Find("ChangeIcon").GetComponent<UISpriteAnimation>().namePrefix = "Fang_00";
                    //}
                    //else if (trcSplit[2] != CharacterRecorder.instance.myLegionData.legionName)
                    //{
                    //    CityObj.transform.Find("ChangeIcon").gameObject.SetActive(true);
                    //    CityObj.transform.Find("ChangeIcon").GetComponent<UISprite>().spriteName = "Gong_000";
                    //    CityObj.transform.Find("ChangeIcon").GetComponent<UISpriteAnimation>().namePrefix = "Gong_00";
                    //}
                }
            }

            if (trcSplit[1] == "0") //此据点无帮会占领
            {
                //CityObj.transform.Find("LegionLogo").gameObject.SetActive(false);
                CityObj.transform.Find("Label").gameObject.SetActive(false);
                LegionCity NewLegionCity = TextTranslator.instance.GetLegionCityByID(int.Parse(trcSplit[0]));
                int NeedLevel = NewLegionCity.LegionNeedLevel;

                if (CharacterRecorder.instance.legionID != 0) //我有加入军团，城市颜色
                {
                    switch (NewLegionCity.CityType)
                    {
                        case 1:
                            if (NeedLevel > CharacterRecorder.instance.myLegionData.legionLevel)
                            {
                                CityObj.GetComponent<UISprite>().spriteName = "dian05G";
                            }
                            break;
                        case 2:
                            if (NeedLevel > CharacterRecorder.instance.myLegionData.legionLevel)
                            {
                                CityObj.GetComponent<UISprite>().spriteName = "dian04G";
                            }
                            break;
                        case 3:
                            if (NeedLevel > CharacterRecorder.instance.myLegionData.legionLevel)
                            {
                                CityObj.GetComponent<UISprite>().spriteName = "dian03G";
                            }
                            break;
                        case 4:
                            if (NeedLevel > CharacterRecorder.instance.myLegionData.legionLevel)
                            {
                                CityObj.GetComponent<UISprite>().spriteName = "dian02G";
                            }
                            break;
                    }
                }
                else //我没有加入军团，城市颜色
                {
                    switch (NewLegionCity.CityType)
                    {
                        case 1:
                            CityObj.GetComponent<UISprite>().spriteName = "dian05G";
                            break;
                        case 2:
                            CityObj.GetComponent<UISprite>().spriteName = "dian04G";
                            break;
                        case 3:
                            CityObj.GetComponent<UISprite>().spriteName = "dian03G";
                            break;
                        case 4:
                            CityObj.GetComponent<UISprite>().spriteName = "dian02G";
                            break;
                    }
                }
            }
            else  //此据点有军团占领
            {
                LegionInfoList.Add(new LegionInfo(int.Parse(trcSplit[0]), int.Parse(trcSplit[1]), trcSplit[2], int.Parse(trcSplit[3]), int.Parse(trcSplit[4]), 0, int.Parse(trcSplit[5])));
                LegionCity NewLegionCity = TextTranslator.instance.GetLegionCityByID(int.Parse(trcSplit[0]));
                int NeedLevel = NewLegionCity.LegionNeedLevel;
                switch (NewLegionCity.CityType)
                {
                    case 1:
                        CityObj.transform.Find("Label").gameObject.SetActive(true);

                        //CityObj.transform.Find("LegionLogo").GetComponent<UISprite>().spriteName = "legionFlag" + trcSplit[3];
                        CityObj.transform.Find("Label").GetComponent<UILabel>().text = "(" + trcSplit[2] + ")";
                        if (CharacterRecorder.instance.legionCountryID == int.Parse(trcSplit[5]))  //CharacterRecorder.instance.legionName == trcSplit[2] 
                        {
                            CityObj.transform.Find("Label").GetComponent<UILabel>().color = Color.green;
                            CityObj.transform.Find("CityName").GetComponent<UILabel>().color = Color.white;
                        }
                        else
                        {
                            CityObj.transform.Find("Label").GetComponent<UILabel>().color = Color.red;
                        }
                        break;
                    case 2:

                        CityObj.transform.Find("Label").gameObject.SetActive(true);

                        //CityObj.transform.Find("LegionLogo").GetComponent<UISprite>().spriteName = "legionFlag" + trcSplit[3];
                        CityObj.transform.Find("Label").GetComponent<UILabel>().text = "(" + trcSplit[2] + ")";
                        if (CharacterRecorder.instance.legionCountryID == int.Parse(trcSplit[5]))
                        {
                            CityObj.transform.Find("Label").GetComponent<UILabel>().color = Color.green;
                            CityObj.transform.Find("CityName").GetComponent<UILabel>().color = Color.white;
                        }
                        else
                        {
                            CityObj.transform.Find("Label").GetComponent<UILabel>().color = Color.red;
                        }
                        break;
                    case 3:
                        CityObj.transform.Find("Label").gameObject.SetActive(true);
                        //CityObj.transform.Find("LegionLogo").GetComponent<UISprite>().spriteName = "legionFlag" + trcSplit[3];
                        CityObj.transform.Find("Label").GetComponent<UILabel>().text = "(" + trcSplit[2] + ")";
                        if (CharacterRecorder.instance.legionCountryID == int.Parse(trcSplit[5]))
                        {
                            CityObj.transform.Find("Label").GetComponent<UILabel>().color = Color.green;
                            CityObj.transform.Find("CityName").GetComponent<UILabel>().color = Color.white;
                        }
                        else
                        {
                            CityObj.transform.Find("Label").GetComponent<UILabel>().color = Color.red;
                        }
                        break;
                    case 4:
                        CityObj.transform.Find("Label").gameObject.SetActive(true);
                        //CityObj.transform.Find("LegionLogo").GetComponent<UISprite>().spriteName = "legionFlag" + trcSplit[3];
                        CityObj.transform.Find("Label").GetComponent<UILabel>().text = "(" + trcSplit[2] + ")";
                        if (CharacterRecorder.instance.legionCountryID == int.Parse(trcSplit[5]))
                        {
                            CityObj.transform.Find("Label").GetComponent<UILabel>().color = Color.green;
                            CityObj.transform.Find("CityName").GetComponent<UILabel>().color = Color.white;
                        }
                        else
                        {
                            CityObj.transform.Find("Label").GetComponent<UILabel>().color = Color.red;
                        }
                        break;
                    case 5:
                        CityObj.transform.Find("Label").gameObject.SetActive(true);
                        CityObj.transform.Find("Label").GetComponent<UILabel>().text = "(" + trcSplit[2] + ")";
                        if (CharacterRecorder.instance.legionCountryID == int.Parse(trcSplit[5]))
                        {
                            CityObj.transform.Find("Label").GetComponent<UILabel>().color = Color.green;
                            CityObj.transform.Find("CityName").GetComponent<UILabel>().color = Color.white;
                        }
                        else
                        {
                            CityObj.transform.Find("Label").GetComponent<UILabel>().color = Color.red;
                        }

                        break;
                }

                if (CharacterRecorder.instance.legionID != 0) //加入军团，此据点为我军团占领
                {
                    if (CharacterRecorder.instance.myLegionData.legionName == trcSplit[2])
                    {
                        CityObj.transform.Find("EffectLeiDaBo").gameObject.SetActive(true);
                        CityObj.transform.Find("EffectLeiDaBo").GetComponent<VFXRenderQueueSorter>().mTarget = CityObj.GetComponent<UISprite>();
                    }
                }
            }

            if (trcSplit[4] == "2")
            {
                CityObj.transform.Find("EffectState").gameObject.SetActive(true);
                CityObj.transform.Find("EffectState").GetComponent<VFXRenderQueueSorter>().mTarget = CityObj.GetComponent<UISprite>();
            }
            else if (trcSplit[4] == "1")
            {
                CityObj.transform.Find("EffectRedColor").gameObject.SetActive(true);
            }

            UIEventListener.Get(CityObj).onClick = delegate(GameObject go)
            {
                if (TextTranslator.instance.GetLegionCityByID(int.Parse(trcSplit[0])).CityType < 5)
                {
                    CharacterRecorder.instance.MarinesTabe = 0;
                }
                NetworkHandler.instance.SendProcess("8602#" + CityObj.name + ";");
                Debug.LogError("MarinesTabe1" + CharacterRecorder.instance.MarinesTabe);
            };
        }

        UIEventListener.Get(DuiHuanButton).onClick = delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("8621#;");
            ClickPointInfoWindow.SetActive(false);
            ClickLittlePointWindow.SetActive(false);
        };


        ShowFourLegion();
    }


    /// <summary>
    /// 单个城市点具体信息，8602协议用,旧的，暂时不用，后面跨服战可能会用，不删
    /// </summary>
                                    //战场点ID ;    占领帮会ID;         帮会名称;       帮会旗;        资源产出;       被宣战状态;        援军数量;  是否有上兵1有0没有;   免战状态;         哪个帮会宣战;          剩馀宣战次数;  军团等级;
    public void LegionWarGetNode(int LegionPoint, int LegionID, string LegionName, int LegionFlag, string ItemStr, int DeclareDate, int Reinforcement, int IsSoldier, int IsAvoidWar, string DeclareLegionName, int DeclareHaveNum, int LegionGrade,int CountryID) 
        {
            CharacterRecorder.instance.LegionHarasPoint = LegionPoint;
            ClickPointInfoWindow.SetActive(true);
            ClickPointInfoWindow.transform.localPosition = ArrLegionPoint[LegionPoint-1].transform.localPosition;
            int cityType = TextTranslator.instance.GetLegionCityByID(LegionPoint).CityType;
            int NeedLevel = TextTranslator.instance.GetLegionCityByID(LegionPoint).LegionNeedLevel;
            if (UIEventListener.Get(CloseButton).onClick == null)
            {
                UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
                {
                    LegionInfoWindow.SetActive(false);
                };
            }

            if (cityType < 5)
            {
                ClickPointInfoWindowSetButton(LegionPoint, LegionID, LegionName, LegionFlag, ItemStr, DeclareDate, Reinforcement, IsSoldier, IsAvoidWar, DeclareLegionName, DeclareHaveNum, LegionGrade, CountryID);
                ClickPointInfoWindow.SetActive(true);
                ClickLittlePointWindow.SetActive(false);

                Vector3 vecposition = ArrLegionPoint[LegionPoint - 1].transform.localPosition;
                ClickPointInfoWindow.transform.localPosition = new Vector3(vecposition.x, vecposition.y + 53, vecposition.z);
                SetPointTweenScale();

                if (CharacterRecorder.instance.legionID != 0)//我加入了军团
                {
                    if (IsAvoidWar == 1)      //5：00~20：00 
                    {
                        #region  可“宣&占”“攻&防”“查”“集"
                        if (LegionID == 0) //该城市未被任何玩家占领时
                        {
                            ZhanButton.SetActive(true);

                            if (CharacterRecorder.instance.isLegionChairman)
                            {
                                ZhanButton.GetComponent<UISprite>().spriteName = "juntuan_icon8";
                            }
                            else
                            {
                                ZhanButton.GetComponent<UISprite>().spriteName = "juntuan_icon8_2";
                            }
                        }
                        else if (LegionID == CharacterRecorder.instance.myLegionData.legionId) //当该城市为我方城市时
                        {
                            XuanButton.SetActive(true);
                            ChaButton.SetActive(true);
                            FangButton.SetActive(true);
                            JiButton.SetActive(true);

                            if (CharacterRecorder.instance.isLegionChairman) //	当军团长查看时显示“查”、“防”、“集”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                FangButton.GetComponent<UISprite>().spriteName = "juntuan_icon4";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon15";
                            }
                            else //	普通玩家查看时显示“查”、“防”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                FangButton.GetComponent<UISprite>().spriteName = "juntuan_icon4";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
                            }

                        }
                        else if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareDate == 0) //当该城市为敌方城市但未宣战时
                        {
                            XuanButton.SetActive(true);
                            ChaButton.SetActive(true);
                            GongButton.SetActive(true);
                            JiButton.SetActive(true);

                            if (CharacterRecorder.instance.isLegionChairman) //	当军团长查看时显示“宣”“查”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                GongButton.GetComponent<UISprite>().spriteName = "juntuan_icon5_2";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
                            }
                            else //	普通玩家查看时显示“查”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                GongButton.GetComponent<UISprite>().spriteName = "juntuan_icon5_2";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
                            }
                        }
                        else if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareDate == 1 && DeclareLegionName != CharacterRecorder.instance.myLegionData.legionName) //当该城市为敌方城市但其他军团宣战时
                        {
                            XuanButton.SetActive(true);
                            ChaButton.SetActive(true);
                            GuanButton.SetActive(true);
                            JiButton.SetActive(true);

                            XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                            ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                            GuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon10";
                            JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7_2";

                        }
                        else if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareDate == 1 && DeclareLegionName == CharacterRecorder.instance.myLegionData.legionName) //当该城市为敌方城市且我方军团宣战时
                        {
                            XuanButton.SetActive(true);
                            ChaButton.SetActive(true);
                            GongButton.SetActive(true);
                            JiButton.SetActive(true);

                            if (CharacterRecorder.instance.isLegionChairman) //	当军团长查看时显示“查”“攻”“集”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                GongButton.GetComponent<UISprite>().spriteName = "juntuan_icon5";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7";
                            }
                            else //	普通玩家查看时显示“查”“攻”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                GongButton.GetComponent<UISprite>().spriteName = "juntuan_icon5";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
                            }
                        }
                        #endregion
                    }
                    else if (IsAvoidWar == 2)  //20：00~20：30
                    {
                        #region  可“占”“攻&防”“查”“集”
                        if (LegionID == 0) //该城市未被任何玩家占领时
                        {
                            ZhanButton.SetActive(true);

                            if (CharacterRecorder.instance.isLegionChairman)
                            {
                                ZhanButton.GetComponent<UISprite>().spriteName = "juntuan_icon8";
                            }
                            else
                            {
                                ZhanButton.GetComponent<UISprite>().spriteName = "juntuan_icon8_2";
                            }
                        }
                        else if (LegionID == CharacterRecorder.instance.myLegionData.legionId) //当该城市为我方城市时
                        {
                            XuanButton.SetActive(true);
                            ChaButton.SetActive(true);
                            FangButton.SetActive(true);
                            JiButton.SetActive(true);

                            if (CharacterRecorder.instance.isLegionChairman) //	当军团长查看时显示“查”、“防”、“集”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                FangButton.GetComponent<UISprite>().spriteName = "juntuan_icon4";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon15";
                            }
                            else //	普通玩家查看时显示“查”、“防”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                FangButton.GetComponent<UISprite>().spriteName = "juntuan_icon4";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
                            }

                        }
                        else if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareDate == 0) //当该城市为敌方城市但未宣战时
                        {
                            XuanButton.SetActive(true);
                            ChaButton.SetActive(true);
                            GongButton.SetActive(true);
                            JiButton.SetActive(true);

                            if (CharacterRecorder.instance.isLegionChairman) //	当军团长查看时显示“宣”“查”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                GongButton.GetComponent<UISprite>().spriteName = "juntuan_icon5_2";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
                            }
                            else //	普通玩家查看时显示“查”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                GongButton.GetComponent<UISprite>().spriteName = "juntuan_icon5_2";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
                            }
                        }
                        else if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareDate == 1 && DeclareLegionName != CharacterRecorder.instance.myLegionData.legionName) //当该城市为敌方城市但其他军团宣战时
                        {
                            XuanButton.SetActive(true);
                            ChaButton.SetActive(true);
                            GuanButton.SetActive(true);
                            JiButton.SetActive(true);

                            XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                            ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                            GuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon10";
                            JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7_2";

                        }
                        else if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareDate == 1 && DeclareLegionName == CharacterRecorder.instance.myLegionData.legionName) //当该城市为敌方城市且我方军团宣战时
                        {
                            XuanButton.SetActive(true);
                            ChaButton.SetActive(true);
                            GongButton.SetActive(true);
                            JiButton.SetActive(true);

                            if (CharacterRecorder.instance.isLegionChairman) //	当军团长查看时显示“查”“攻”“集”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                GongButton.GetComponent<UISprite>().spriteName = "juntuan_icon5";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7";
                            }
                            else //	普通玩家查看时显示“查”“攻”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                GongButton.GetComponent<UISprite>().spriteName = "juntuan_icon5";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
                            }
                        }
                        #endregion
                    }
                    else if (IsAvoidWar == 3 && DeclareDate == 2) // 20：30~战斗结束
                    {
                        #region  可“查”“攻&防&观”“集”
                        if (LegionID == 0) //该城市未被任何玩家占领时
                        {
                            ZhanButton.SetActive(true);

                            ZhanButton.GetComponent<UISprite>().spriteName = "juntuan_icon8_2";
                        }
                        else if (LegionID == CharacterRecorder.instance.myLegionData.legionId) //当该城市为我方城市时
                        {
                            XuanButton.SetActive(true);
                            ChaButton.SetActive(true);
                            FangButton.SetActive(true);
                            JiButton.SetActive(true);

                            if (CharacterRecorder.instance.isLegionChairman) //	当军团长查看时显示“查”、“防”、“集”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                FangButton.GetComponent<UISprite>().spriteName = "juntuan_icon4";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon15";
                            }
                            else //	普通玩家查看时显示“查”、“防”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                FangButton.GetComponent<UISprite>().spriteName = "juntuan_icon4";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
                            }

                        }
                        else if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareDate == 0) //当该城市为敌方城市但未宣战时
                        {
                            XuanButton.SetActive(true);
                            ChaButton.SetActive(true);
                            GongButton.SetActive(true);
                            JiButton.SetActive(true);

                            if (CharacterRecorder.instance.isLegionChairman) //	当军团长查看时显示“宣”“查”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                GongButton.GetComponent<UISprite>().spriteName = "juntuan_icon5_2";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
                            }
                            else //	普通玩家查看时显示“查”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                GongButton.GetComponent<UISprite>().spriteName = "juntuan_icon5_2";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
                            }
                        }
                        else if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareLegionName != CharacterRecorder.instance.myLegionData.legionName) //当该城市为敌方城市但其他军团宣战时
                        {
                            XuanButton.SetActive(true);
                            ChaButton.SetActive(true);
                            GuanButton.SetActive(true);
                            JiButton.SetActive(true);

                            XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                            ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                            GuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon10";
                            JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7_2";

                        }
                        else if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareLegionName == CharacterRecorder.instance.myLegionData.legionName) //当该城市为敌方城市且我方军团宣战时
                        {
                            XuanButton.SetActive(true);
                            ChaButton.SetActive(true);
                            GongButton.SetActive(true);
                            JiButton.SetActive(true);

                            if (CharacterRecorder.instance.isLegionChairman) //	当军团长查看时显示“查”“攻”“集”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                GongButton.GetComponent<UISprite>().spriteName = "juntuan_icon5";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7";
                            }
                            else //	普通玩家查看时显示“查”“攻”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                GongButton.GetComponent<UISprite>().spriteName = "juntuan_icon5";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
                            }
                        }
                        #endregion
                    }
                    else if (IsAvoidWar == 4 || (IsAvoidWar == 3 && DeclareDate != 2)) //其余时间
                    {
                        #region  可“查”
                        if (LegionID == 0) //该城市未被任何玩家占领时
                        {
                            ZhanButton.SetActive(true);

                            ZhanButton.GetComponent<UISprite>().spriteName = "juntuan_icon8_2";
                        }
                        else if (LegionID == CharacterRecorder.instance.myLegionData.legionId) //当该城市为我方城市时
                        {
                            XuanButton.SetActive(true);
                            ChaButton.SetActive(true);
                            FangButton.SetActive(true);
                            JiButton.SetActive(true);

                            if (CharacterRecorder.instance.isLegionChairman) //	当军团长查看时显示“查”、“防”、“集”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                FangButton.GetComponent<UISprite>().spriteName = "juntuan_icon4_2";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon15";
                            }
                            else //	普通玩家查看时显示“查”、“防”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                FangButton.GetComponent<UISprite>().spriteName = "juntuan_icon4_2";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
                            }

                        }
                        else if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareDate == 0) //当该城市为敌方城市但未宣战时
                        {
                            XuanButton.SetActive(true);
                            ChaButton.SetActive(true);
                            GongButton.SetActive(true);
                            JiButton.SetActive(true);

                            if (CharacterRecorder.instance.isLegionChairman) //	当军团长查看时显示“宣”“查”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                GongButton.GetComponent<UISprite>().spriteName = "juntuan_icon5_2";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
                            }
                            else //	普通玩家查看时显示“查”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                GongButton.GetComponent<UISprite>().spriteName = "juntuan_icon5_2";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
                            }
                        }
                        else if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareLegionName != CharacterRecorder.instance.myLegionData.legionName) //当该城市为敌方城市但其他军团宣战时
                        {
                            XuanButton.SetActive(true);
                            ChaButton.SetActive(true);
                            GuanButton.SetActive(true);
                            JiButton.SetActive(true);

                            XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                            ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                            GuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon10_2";
                            JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7_2";

                        }
                        else if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareLegionName == CharacterRecorder.instance.myLegionData.legionName) //当该城市为敌方城市且我方军团宣战时
                        {
                            XuanButton.SetActive(true);
                            ChaButton.SetActive(true);
                            GongButton.SetActive(true);
                            JiButton.SetActive(true);

                            if (CharacterRecorder.instance.isLegionChairman) //	当军团长查看时显示“查”“攻”“集”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                GongButton.GetComponent<UISprite>().spriteName = "juntuan_icon5_2";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
                            }
                            else //	普通玩家查看时显示“查”“攻”
                            {
                                XuanButton.GetComponent<UISprite>().spriteName = "juntuan_icon11_2";
                                ChaButton.GetComponent<UISprite>().spriteName = "juntuan_icon9";
                                GongButton.GetComponent<UISprite>().spriteName = "juntuan_icon5_2";
                                JiButton.GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
                            }
                        }
                        #endregion
                    }

                    int Num = 0;

                    for (int i = 1; i < ClickPointInfoWindow.transform.childCount; i++)
                    {
                        if (ClickPointInfoWindow.transform.GetChild(i).gameObject.activeSelf)
                        {
                            Num++;
                        }
                    }

                    if (Num == 1 && ZhanButton.activeSelf) //占
                    {
                        ZhanButton.transform.localPosition = new Vector3(0, 55f, 0);
                    }
                    else
                    {
                        ZhanButton.transform.localPosition = new Vector3(-90f, 15f, 0);
                    }
                }
                else 
                {
                    ZhanButton.SetActive(true);
                    ZhanButton.GetComponent<UISprite>().spriteName = "juntuan_icon8_2";
                    ZhanButton.transform.localPosition = new Vector3(0, 55f, 0);
                }
            }
            else
            {
                #region 骚扰
                ClickPointInfoWindow.SetActive(false);
                ClickLittlePointWindow.SetActive(true);
                Vector3 vecposition = ArrLegionPoint[LegionPoint-1].transform.localPosition;
                ClickLittlePointWindow.transform.localPosition = new Vector3(vecposition.x, vecposition.y, vecposition.z);

                //ClickLittlePointWindow.transform.Find("RaoIcon").gameObject.SetActive(false);
                //ClickLittlePointWindow.transform.Find("ZhuIcon").gameObject.SetActive(false);
                //ClickLittlePointWindow.transform.Find("JiIcon").gameObject.SetActive(false);
                //ClickLittlePointWindow.transform.Find("CheIcon").gameObject.SetActive(false);
                //ClickLittlePointWindow.transform.Find("QiIcon").gameObject.SetActive(false);
                TeamHero1.SetActive(false);
                TeamHero2.SetActive(false);
                TeamHero3.SetActive(false);
                SetLittleTweenScale();
                SaoRaoHeroButton(LegionPoint);

                if (CharacterRecorder.instance.legionID == 0) //未加入任何军团
                {
                    ClickLittlePointWindow.transform.Find("RaoIcon").gameObject.SetActive(true);
                    //ClickLittlePointWindow.transform.Find("CheIcon").gameObject.SetActive(true);
                    ClickLittlePointWindow.transform.Find("QiIcon").gameObject.SetActive(true);
                    ClickLittlePointWindow.transform.Find("JiIcon").gameObject.SetActive(true);

                    ClickLittlePointWindow.transform.Find("RaoIcon").GetComponent<UISprite>().spriteName = "juntuan_icon9";
                    //ClickLittlePointWindow.transform.Find("CheIcon").GetComponent<UISprite>().spriteName = "juntuan_icon12_2";
                    ClickLittlePointWindow.transform.Find("QiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon13_2";

                    ClickLittlePointWindow.transform.Find("JiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon7";


                    UIEventListener.Get(ClickLittlePointWindow.transform.Find("RaoIcon").gameObject).onClick = delegate(GameObject go)
                    {
                        //if (CountryID != CharacterRecorder.instance.legionCountryID)
                        {
                            NoOneHarassmentPart(LegionPoint, LegionID, LegionName, LegionFlag, ItemStr, DeclareDate, Reinforcement, IsSoldier, IsAvoidWar, DeclareLegionName, DeclareHaveNum, LegionGrade);
                        }
                        //else 
                        {
                            //UIManager.instance.OpenPromptWindow("不可骚扰本国据点!", PromptWindow.PromptType.Hint, null, null);
                        }
                    
                    };

                    //UIEventListener.Get(ClickLittlePointWindow.transform.Find("CheIcon").gameObject).onClick = delegate(GameObject go)
                    //{
                    //    UIManager.instance.OpenPromptWindow("只有加入军团才可做此操作!", PromptWindow.PromptType.Hint, null, null);
                    //};
                    UIEventListener.Get(ClickLittlePointWindow.transform.Find("QiIcon").gameObject).onClick = delegate(GameObject go)
                    {
                        UIManager.instance.OpenPromptWindow("只有加入军团才可做此操作!", PromptWindow.PromptType.Hint, null, null);
                    };
                    UIEventListener.Get(ClickLittlePointWindow.transform.Find("JiIcon").gameObject).onClick = delegate(GameObject go)
                    {
                        UIManager.instance.OpenPromptWindow("只有军团长才可以进行此操作!", PromptWindow.PromptType.Hint, null, null);
                    };
                }
                else 
                {
                    if (LegionID == 0) //无人占领，骚扰
                    {
                        ClickLittlePointWindow.transform.Find("RaoIcon").gameObject.SetActive(true);
                        //ClickLittlePointWindow.transform.Find("CheIcon").gameObject.SetActive(true);
                        ClickLittlePointWindow.transform.Find("QiIcon").gameObject.SetActive(true);
                        ClickLittlePointWindow.transform.Find("JiIcon").gameObject.SetActive(true);

                        ClickLittlePointWindow.transform.Find("RaoIcon").GetComponent<UISprite>().spriteName = "juntuan_icon9";
                        //ClickLittlePointWindow.transform.Find("CheIcon").GetComponent<UISprite>().spriteName = "juntuan_icon12_2";
                        ClickLittlePointWindow.transform.Find("QiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon13_2";
                        //ClickLittlePointWindow.transform.Find("JiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon7_2";  
                        if (CharacterRecorder.instance.isLegionChairman)
                        {
                            ClickLittlePointWindow.transform.Find("JiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon7";
                        }
                        else
                        {
                            ClickLittlePointWindow.transform.Find("JiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
                        }

                        UIEventListener.Get(ClickLittlePointWindow.transform.Find("RaoIcon").gameObject).onClick = delegate(GameObject go)
                        {
                            NoOneHarassmentPart(LegionPoint, LegionID, LegionName, LegionFlag, ItemStr, DeclareDate, Reinforcement, IsSoldier, IsAvoidWar, DeclareLegionName, DeclareHaveNum, LegionGrade);
                        };

                        //UIEventListener.Get(ClickLittlePointWindow.transform.Find("CheIcon").gameObject).onClick = delegate(GameObject go)
                        //{
                        //    UIManager.instance.OpenPromptWindow("您还没有驻守英雄!", PromptWindow.PromptType.Hint, null, null);
                        //};
                        UIEventListener.Get(ClickLittlePointWindow.transform.Find("QiIcon").gameObject).onClick = delegate(GameObject go)
                        {
                            if (CharacterRecorder.instance.isLegionChairman)
                            {
                                UIManager.instance.OpenPromptWindow("您还没有占领此据点!", PromptWindow.PromptType.Hint, null, null);
                            }
                            else
                            {
                                UIManager.instance.OpenPromptWindow("只有军团长才可放弃该城池!", PromptWindow.PromptType.Hint, null, null);
                            }
                        };
                        UIEventListener.Get(ClickLittlePointWindow.transform.Find("JiIcon").gameObject).onClick = delegate(GameObject go)
                        {
                            if (CharacterRecorder.instance.isLegionChairman)
                            {
                                //UIManager.instance.OpenPromptWindow("您还没有占领此据点!", PromptWindow.PromptType.Hint, null, null);
                                NetworkHandler.instance.SendProcess("8625#" + CharacterRecorder.instance.LegionHarasPoint + ";");
                            }
                            else
                            {
                                UIManager.instance.OpenPromptWindow("只有军团长才可以进行此操作!", PromptWindow.PromptType.Hint, null, null);
                            }
                        };
                    }
                    else if (LegionName != CharacterRecorder.instance.myLegionData.legionName) //据点非我，骚扰
                    {
                        ClickLittlePointWindow.transform.Find("RaoIcon").gameObject.SetActive(true);
                        //ClickLittlePointWindow.transform.Find("CheIcon").gameObject.SetActive(true);
                        ClickLittlePointWindow.transform.Find("QiIcon").gameObject.SetActive(true);
                        ClickLittlePointWindow.transform.Find("JiIcon").gameObject.SetActive(true);

                        ClickLittlePointWindow.transform.Find("RaoIcon").GetComponent<UISprite>().spriteName = "juntuan_icon9";
                        //ClickLittlePointWindow.transform.Find("CheIcon").GetComponent<UISprite>().spriteName = "juntuan_icon12_2";
                        ClickLittlePointWindow.transform.Find("QiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon13_2";
                        //ClickLittlePointWindow.transform.Find("JiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
                        if (CharacterRecorder.instance.isLegionChairman)
                        {
                            ClickLittlePointWindow.transform.Find("JiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon7";
                        }
                        else
                        {
                            ClickLittlePointWindow.transform.Find("JiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
                        }

                        UIEventListener.Get(ClickLittlePointWindow.transform.Find("RaoIcon").gameObject).onClick = delegate(GameObject go)
                        {
                            //if (CountryID != CharacterRecorder.instance.legionCountryID)
                            {
                                SomeOneHarassmentPart(LegionPoint, LegionID, LegionName, LegionFlag, ItemStr, DeclareDate, Reinforcement, IsSoldier, IsAvoidWar, DeclareLegionName, DeclareHaveNum, LegionGrade);
                            }
                            //else
                            {
                                //UIManager.instance.OpenPromptWindow("不可骚扰相同国家的据点!", PromptWindow.PromptType.Hint, null, null);
                            }
                        };

                        //UIEventListener.Get(ClickLittlePointWindow.transform.Find("CheIcon").gameObject).onClick = delegate(GameObject go)
                        //{
                        //    UIManager.instance.OpenPromptWindow("您无权操作其他军团据点!", PromptWindow.PromptType.Hint, null, null);
                        //};
                        UIEventListener.Get(ClickLittlePointWindow.transform.Find("QiIcon").gameObject).onClick = delegate(GameObject go)
                        {
                            //if (CharacterRecorder.instance.isLegionChairman)
                            //{
                            //    UIManager.instance.OpenPromptWindow("您还没有占领此据点!", PromptWindow.PromptType.Hint, null, null);
                            //}
                            //else
                            //{
                            //    UIManager.instance.OpenPromptWindow("只有军团长才可放弃该城池!", PromptWindow.PromptType.Hint, null, null);
                            //}
                            UIManager.instance.OpenPromptWindow("您无权操作其他军团据点!", PromptWindow.PromptType.Hint, null, null);
                        };
                        UIEventListener.Get(ClickLittlePointWindow.transform.Find("JiIcon").gameObject).onClick = delegate(GameObject go)
                        {
                            if (CountryID != CharacterRecorder.instance.legionCountryID)
                            {
                                if (CharacterRecorder.instance.isLegionChairman)
                                {
                                    NetworkHandler.instance.SendProcess("8625#" + CharacterRecorder.instance.LegionHarasPoint + ";");
                                }
                                else
                                {
                                    UIManager.instance.OpenPromptWindow("只有军团长才可以进行此操作!", PromptWindow.PromptType.Hint, null, null);
                                }
                            }
                            else 
                            {
                                UIManager.instance.OpenPromptWindow("您无权对友军的据点集火!", PromptWindow.PromptType.Hint, null, null);
                            }
                        };
                    }
                    else if (LegionName == CharacterRecorder.instance.myLegionData.legionName) //据点为我，驻守
                    {
                        //ClickLittlePointWindow.transform.Find("ZhuIcon").gameObject.SetActive(true);
                        //ClickLittlePointWindow.transform.Find("CheIcon").gameObject.SetActive(true);
                        ClickLittlePointWindow.transform.Find("RaoIcon").gameObject.SetActive(true);
                        ClickLittlePointWindow.transform.Find("QiIcon").gameObject.SetActive(true);
                        ClickLittlePointWindow.transform.Find("JiIcon").gameObject.SetActive(true);

                        //ClickLittlePointWindow.transform.Find("ZhuIcon").GetComponent<UISprite>().spriteName = "juntuan_icon14";
                        //ClickLittlePointWindow.transform.Find("CheIcon").GetComponent<UISprite>().spriteName = "juntuan_icon12_2";
                        //ClickLittlePointWindow.transform.Find("QiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon13_2";
                        //ClickLittlePointWindow.transform.Find("JiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon7_2";

                        //if (IsSoldier == 1)
                        //{
                        //    ClickLittlePointWindow.transform.Find("CheIcon").GetComponent<UISprite>().spriteName = "juntuan_icon12";
                        //}
                        //else
                        //{
                        //    ClickLittlePointWindow.transform.Find("CheIcon").GetComponent<UISprite>().spriteName = "juntuan_icon12_2";
                        //}
                        UIEventListener.Get(ClickLittlePointWindow.transform.Find("RaoIcon").gameObject).onClick = delegate(GameObject go)
                        {
                            //if (CountryID != CharacterRecorder.instance.legionCountryID)
                            {
                                SomeOneHarassmentPart(LegionPoint, LegionID, LegionName, LegionFlag, ItemStr, DeclareDate, Reinforcement, IsSoldier, IsAvoidWar, DeclareLegionName, DeclareHaveNum, LegionGrade);
                            }
                        };
                        if (CharacterRecorder.instance.isLegionChairman)
                        {
                            ClickLittlePointWindow.transform.Find("JiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon15";
                            ClickLittlePointWindow.transform.Find("QiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon13";
                        }
                        else
                        {
                            ClickLittlePointWindow.transform.Find("JiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
                            ClickLittlePointWindow.transform.Find("QiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon13_2";
                        }


                        //UIEventListener.Get(ClickLittlePointWindow.transform.Find("ZhuIcon").gameObject).onClick = delegate(GameObject go)
                        //{
                        //    
                        //    ZhuHarassmentPart(LegionPoint, LegionID, LegionName, LegionFlag, ItemStr, DeclareDate, Reinforcement, IsSoldier, IsAvoidWar, DeclareLegionName, DeclareHaveNum, LegionGrade);
                        //};

                        //UIEventListener.Get(ClickLittlePointWindow.transform.Find("CheIcon").gameObject).onClick = delegate(GameObject go)
                        //{
                        //    if (IsSoldier == 1)
                        //    {
                        //        NetworkHandler.instance.SendProcess("8618#" + CharacterRecorder.instance.LegionHarasPoint + ";");
                        //    }
                        //    else
                        //    {
                        //        UIManager.instance.OpenPromptWindow("您还没有驻守英雄!", PromptWindow.PromptType.Hint, null, null);
                        //    }
                        //};

                        UIEventListener.Get(ClickLittlePointWindow.transform.Find("QiIcon").gameObject).onClick = delegate(GameObject go)
                        {
                            if (CharacterRecorder.instance.isLegionChairman)
                            {
                                //UIManager.instance.OpenPromptWindow("您还没有占领此据点!", PromptWindow.PromptType.Hint, null, null);
                                //NetworkHandler.instance.SendProcess("8629#" + CharacterRecorder.instance.LegionHarasPoint + ";");
                                MyHarassmentPart(LegionPoint, LegionID, LegionName, LegionFlag, ItemStr, DeclareDate, Reinforcement, IsSoldier, IsAvoidWar, DeclareLegionName, DeclareHaveNum, LegionGrade);
                            }
                            else
                            {
                                UIManager.instance.OpenPromptWindow("只有军团长才可弃城!", PromptWindow.PromptType.Hint, null, null);
                            }
                        };

                        UIEventListener.Get(ClickLittlePointWindow.transform.Find("JiIcon").gameObject).onClick = delegate(GameObject go)
                        {
                            if (CharacterRecorder.instance.isLegionChairman)
                            {
                                NetworkHandler.instance.SendProcess("8625#" + CharacterRecorder.instance.LegionHarasPoint + ";");
                            }
                            else
                            {
                                UIManager.instance.OpenPromptWindow("只有军团长才可操作此功能!", PromptWindow.PromptType.Hint, null, null);
                            }
                        };
                    }
                }
                #endregion
            }
        }
*/
    #endregion





    /// <summary>
    /// 特殊城市点的特殊奖励
    /// </summary>
    private void SetItemDetail(int ItemCode, GameObject ItemObj)
    {
        UISprite spriteIcon = ItemObj.GetComponent<UISprite>();
        GameObject suiPian = ItemObj.transform.Find("suiPian").gameObject;
        UISprite spritegrade = ItemObj.transform.Find("Grade").GetComponent<UISprite>();

        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
        spritegrade.spriteName = "Grade" + _ItemInfo.itemGrade.ToString();

        if (ItemCode.ToString()[0] == '4')
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            if (ItemCode.ToString()[1] == '0')
            {
                suiPian.SetActive(false);
            }
            else
            {
                suiPian.SetActive(true);
            }
        }
        else if (ItemCode.ToString()[0] == '6')
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            suiPian.SetActive(false);
        }
        else if (ItemCode == 70000 || ItemCode == 79999)
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            suiPian.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '7' && ItemCode > 70000 && ItemCode != 79999)
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = (ItemCode - 10000).ToString();
            suiPian.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '8')
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
            //spriteIcon.spriteName = (ItemCode - 30000).ToString();
            spriteIcon.spriteName = mItemInfo.picID.ToString();
            suiPian.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '2')
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
            spriteIcon.spriteName = mItemInfo.picID.ToString();
            suiPian.SetActive(false);
        }
        else
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            suiPian.SetActive(false);
        }
    }


    /// <summary>
    /// 特殊城市点的特殊奖励,弹出界面第二个奖励显示
    /// </summary>
    private void SetItemDetail(int ItemCode,UISprite spriteIcon)
    {
        if (ItemCode.ToString()[0] == '4')
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
        }
        else if (ItemCode.ToString()[0] == '6')
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
        }
        else if (ItemCode == 70000 || ItemCode == 79999)
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
        }
        else if (ItemCode.ToString()[0] == '7' && ItemCode > 70000 && ItemCode != 79999)
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = (ItemCode - 10000).ToString();
        }
        else if (ItemCode.ToString()[0] == '8')
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
            //spriteIcon.spriteName = (ItemCode - 30000).ToString();
            spriteIcon.spriteName = mItemInfo.picID.ToString();
        }
        else if (ItemCode.ToString()[0] == '2')
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
            spriteIcon.spriteName = mItemInfo.picID.ToString();
        }
        else
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
        }
    } 



    /// <summary>
    /// 初始化所有城市点信息，8601协议
    /// </summary>
    public void LegionWarGetScene(string Recving) //8601
    {
        LegionInfoList.Clear();
        string[] dataSplit = Recving.Split(';');
        ActionPoint = int.Parse(dataSplit[0]);
        JunGongPoint = int.Parse(dataSplit[1]);
        JungongLabel.text = JunGongPoint.ToString();
        XingdongLabel.text = ActionPoint.ToString();
        CharacterRecorder.instance.LegionActionPoint = ActionPoint;


        List<int> BiaoJiCity = new List<int>();    //哪些标记点
        string[] prcSplit = dataSplit[3].Split('!');
        for (int i = 0; i < prcSplit.Length - 1; i++)
        {
            string[] qrcSplit = prcSplit[i].Split('$');
            BiaoJiCity.Add(int.Parse(qrcSplit[0]));
        }

        brushCityIdlist.Clear();//刷城点
        brushCityIdNoGoodList.Clear();

        for (int i = 4; i < dataSplit.Length - 1; i++)
        {
            string[] trcSplit = dataSplit[i].Split('$');

            GameObject CityObj = ArrLegionPoint[int.Parse(trcSplit[0]) - 1];

            CityObj.name = trcSplit[0];
            CityObj.transform.Find("CityName").GetComponent<UILabel>().text = TextTranslator.instance.GetLegionCityByID(int.Parse(trcSplit[0])).CityName;
            CityObj.transform.Find("EffectState").gameObject.SetActive(false);  //火焰特效
            CityObj.transform.Find("EffectRedColor").gameObject.SetActive(false);//宣战红色特效
            CityObj.transform.Find("EffectLeiDaBo").gameObject.SetActive(false);//占领雷达波特效
            CityObj.transform.Find("JihuoEffect").gameObject.SetActive(false);  //集火特效

            if (CharacterRecorder.instance.legionID != 0) //我有加入军团，判断集火标志
            {
                if (BiaoJiCity.Contains(int.Parse(trcSplit[0]))) //此点被标记
                {
                    CityObj.transform.Find("JihuoEffect").gameObject.SetActive(true);
                }
            }

            LegionCity NewLegionCity = TextTranslator.instance.GetLegionCityByID(int.Parse(trcSplit[0]));//取得城市信息
            int NeedLevel = NewLegionCity.LegionNeedLevel;

            #region D级以上城市颜色变化           
            if (CharacterRecorder.instance.legionID != 0) //我有加入军团，城市颜色
            {
                switch (NewLegionCity.CityType)
                {
                    case 1:
                        if (NeedLevel > CharacterRecorder.instance.myLegionData.legionLevel)
                        {
                            CityObj.GetComponent<UISprite>().spriteName = "dian05G";
                        }
                        break;
                    case 2:
                        if (NeedLevel > CharacterRecorder.instance.myLegionData.legionLevel)
                        {
                            CityObj.GetComponent<UISprite>().spriteName = "dian04G";
                        }
                        break;
                    case 3:
                        if (NeedLevel > CharacterRecorder.instance.myLegionData.legionLevel)
                        {
                            CityObj.GetComponent<UISprite>().spriteName = "dian03G";
                        }
                        break;
                    case 4:
                        if (NeedLevel > CharacterRecorder.instance.myLegionData.legionLevel)
                        {
                            CityObj.GetComponent<UISprite>().spriteName = "dian02G";
                        }
                        break;
                }
            }
            else //我没有加入军团，城市颜色
            {
                switch (NewLegionCity.CityType)
                {
                    case 1:
                        CityObj.GetComponent<UISprite>().spriteName = "dian05G";
                        break;
                    case 2:
                        CityObj.GetComponent<UISprite>().spriteName = "dian04G";
                        break;
                    case 3:
                        CityObj.GetComponent<UISprite>().spriteName = "dian03G";
                        break;
                    case 4:
                        CityObj.GetComponent<UISprite>().spriteName = "dian02G";
                        break;
                }
            }
            #endregion

            #region 城市点占领军团信息,包含雷达波特效（我方军团占领）
            if (trcSplit[1] == "0") //此据点无军团占领
            {
                CityObj.transform.Find("Label").gameObject.SetActive(false);
                CityObj.transform.Find("CountryLabel").gameObject.SetActive(false);
            }
            else if (trcSplit[2] != "")//此据点有军团占领
            {
                LegionInfoList.Add(new LegionInfo(int.Parse(trcSplit[0]), int.Parse(trcSplit[1]), trcSplit[2], int.Parse(trcSplit[3]), int.Parse(trcSplit[4]), 0, int.Parse(trcSplit[5])));//占领军团信息，底部浏览用

                CityObj.transform.Find("Label").gameObject.SetActive(true);
                CityObj.transform.Find("CountryLabel").gameObject.SetActive(true);
                CityObj.transform.Find("Label").GetComponent<UILabel>().text = "(" + trcSplit[2] + ")";
                if (trcSplit[5] == "1")
                {
                    CityObj.transform.Find("CountryLabel").GetComponent<UILabel>().text = "同盟";
                }
                else 
                {
                    CityObj.transform.Find("CountryLabel").GetComponent<UILabel>().text = "帝国";
                }

                //if (CharacterRecorder.instance.legionCountryID == int.Parse(trcSplit[5]))      //yytest  2017/4/11
                //{
                //    CityObj.transform.Find("Label").GetComponent<UILabel>().color = Color.green;
                //    CityObj.transform.Find("CityName").GetComponent<UILabel>().color = Color.white;
                //    CityObj.transform.Find("CountryLabel").GetComponent<UILabel>().color = Color.green;
                //}
                //else
                //{
                //    CityObj.transform.Find("Label").GetComponent<UILabel>().color = Color.red;
                //    CityObj.transform.Find("CountryLabel").GetComponent<UILabel>().color = Color.red;
                //}

                if (CharacterRecorder.instance.legionID == int.Parse(trcSplit[1])) //加入军团，此据点为我军团占领
                {
                    CityObj.transform.Find("EffectLeiDaBo").gameObject.SetActive(true);
                    CityObj.transform.Find("EffectLeiDaBo").GetComponent<VFXRenderQueueSorter>().mTarget = CityObj.GetComponent<UISprite>();

                    CityObj.transform.Find("Label").GetComponent<UILabel>().color = Color.green;            //yytest  2017/4/11 add
                    CityObj.transform.Find("CityName").GetComponent<UILabel>().color = Color.white;         //yytest  2017/4/11
                    CityObj.transform.Find("CountryLabel").GetComponent<UILabel>().color = Color.green;     //yytest  2017/4/11
                }
                else 
                {
                    CityObj.transform.Find("Label").GetComponent<UILabel>().color = Color.red;              //yytest  2017/4/11
                    CityObj.transform.Find("CountryLabel").GetComponent<UILabel>().color = Color.red;       //yytest  2017/4/11
                }
            }
            #endregion

            #region 宣战状态特效
            if (trcSplit[4] == "2")//战斗中，火焰特效
            {
                CityObj.transform.Find("EffectState").gameObject.SetActive(true);
                CityObj.transform.Find("EffectState").GetComponent<VFXRenderQueueSorter>().mTarget = CityObj.GetComponent<UISprite>();
            }
            else if (trcSplit[4] == "1")//已被宣,红色闪动特效
            {
                CityObj.transform.Find("EffectRedColor").gameObject.SetActive(true);
            }
            #endregion

            #region 特殊城市奖励
            if (trcSplit[6] != "0")
            {
                CityObj.transform.Find("ItemIcon").gameObject.SetActive(true);
                SetItemDetail(int.Parse(trcSplit[6]), CityObj.transform.Find("ItemIcon").gameObject);

                //if (NewLegionCity.CityType == 5 && CharacterRecorder.instance.legionCountryID != int.Parse(trcSplit[5]) && trcSplit[0] != "40")   yytest 2017/4/11
                //{
                //    brushCityIdlist.Add(int.Parse(trcSplit[0]));//D 级城市资源点且非我国家的
                //}

                if (NewLegionCity.CityType == 5 && CharacterRecorder.instance.legionID != int.Parse(trcSplit[1]) && trcSplit[0] != "40")
                {
                    brushCityIdlist.Add(int.Parse(trcSplit[0]));//D 级城市资源点且非我军团的
                }
            }
            else 
            {
                CityObj.transform.Find("ItemIcon").gameObject.SetActive(false);
                //if(NewLegionCity.CityType == 5 && CharacterRecorder.instance.legionCountryID != int.Parse(trcSplit[5]))
                //{
                //    brushCityIdNoGoodList.Add(int.Parse(trcSplit[0]));//D 级城市非资源点且非我国家的
                //}
                if (NewLegionCity.CityType == 5 && CharacterRecorder.instance.legionID != int.Parse(trcSplit[1]))
                {
                    brushCityIdNoGoodList.Add(int.Parse(trcSplit[0]));//D 级城市非资源点且非我国家的
                }
            }
            #endregion

            UIEventListener.Get(CityObj).onClick = delegate(GameObject go)
            {
                //CharacterRecorder.instance.LegionHarasPoint = int.Parse(trcSplit[0]);
                NetworkHandler.instance.SendProcess("8602#" + CityObj.name + ";");
            };
        }
        ShowFourLegion();
    }

    /// <summary>
    /// 前三名占领城市的军团信息
    /// </summary>
    private void ShowFourLegion()
    {
        ///现在取第一名占领城市的军团信息
        OneTypeLegionList.Clear();
        FirstRankInfo.SetActive(false);
        //List<LegionInfo> OneTypeLegionList = new List<LegionInfo>();
        for (int i = 0; i < LegionInfoList.Count; i++) //获得军团种类
        {
            bool isHave = false;
            for (int j = 0; j < OneTypeLegionList.Count; j++)
            {
                if (OneTypeLegionList[j].LegionID == LegionInfoList[i].LegionID)
                {
                    isHave = true;
                    break;
                }
            }
            if (isHave == false)
            {
                OneTypeLegionList.Add(LegionInfoList[i]);
            }
        }

        for (int i = 0; i < OneTypeLegionList.Count; i++) //单个军团种类对应的数量
        {
            int num = 0;
            for (int j = 0; j < LegionInfoList.Count; j++)
            {
                if (LegionInfoList[j].LegionID == OneTypeLegionList[i].LegionID)
                {
                    num++;
                }
            }
            OneTypeLegionList[i].LegionNum = num;
        }

        for (int i = 0; i < OneTypeLegionList.Count; i++)//排序
        {
            for (var j = OneTypeLegionList.Count - 1; j > i; j--)
            {
                if (OneTypeLegionList[i].LegionNum < OneTypeLegionList[j].LegionNum)
                {
                    var temp = OneTypeLegionList[i];
                    OneTypeLegionList[i] = OneTypeLegionList[j];
                    OneTypeLegionList[j] = temp;
                }
            }
        }

        if (OneTypeLegionList.Count > 0)
        {
            FirstRankInfo.SetActive(true);
            FirstRankInfo.transform.Find("RankNumber").GetComponent<UILabel>().text = "1";
            FirstRankInfo.transform.Find("LegionName").GetComponent<UILabel>().text = OneTypeLegionList[0].LegionName;
            FirstRankInfo.transform.Find("Number").GetComponent<UILabel>().text = OneTypeLegionList[0].LegionNum.ToString();
        }
        #region 旧的，取前三名军团占领据点数量
        /*  
        LegionMessage1.SetActive(false);
        LegionMessage2.SetActive(false);
        LegionMessage3.SetActive(false);
        //LegionMessage4.SetActive(false);
        List<LegionInfo> OneTypeLegionList = new List<LegionInfo>();
        for (int i = 0; i < LegionInfoList.Count; i++) //获得军团种类
        {
            bool isHave = false;
            for (int j = 0; j < OneTypeLegionList.Count; j++)
            {
                if (OneTypeLegionList[j].LegionID == LegionInfoList[i].LegionID)
                {
                    isHave = true;
                    break;
                }
            }
            if (isHave == false)
            {
                OneTypeLegionList.Add(LegionInfoList[i]);
            }
        }

        for (int i = 0; i < OneTypeLegionList.Count; i++) //单个军团种类对应的数量
        {
            int num = 0;
            for (int j = 0; j < LegionInfoList.Count; j++)
            {
                if (LegionInfoList[j].LegionID == OneTypeLegionList[i].LegionID)
                {
                    num++;
                }
            }
            OneTypeLegionList[i].LegionNum = num;
        }

        for (int i = 0; i < OneTypeLegionList.Count; i++)//排序
        {
            for (var j = OneTypeLegionList.Count - 1; j > i; j--)
            {
                if (OneTypeLegionList[i].LegionNum < OneTypeLegionList[j].LegionNum)
                {
                    var temp = OneTypeLegionList[i];
                    OneTypeLegionList[i] = OneTypeLegionList[j];
                    OneTypeLegionList[j] = temp;
                }
            }
        }

        if (OneTypeLegionList.Count > 0)
        {
            for (int i = 0; i < OneTypeLegionList.Count; i++)//排序
            {
                if (i == 0)
                {
                    LegionMessage1.SetActive(true);
                    LegionMessage1.GetComponent<UISprite>().spriteName = "legionFlag" + OneTypeLegionList[i].LegionFlag;
                    LegionMessage1.transform.Find("LabelNumber").GetComponent<UILabel>().text = "占领:" + OneTypeLegionList[i].LegionNum.ToString();
                    LegionMessage1.transform.Find("LegionName").GetComponent<UILabel>().text = OneTypeLegionList[i].LegionName;
                    if (OneTypeLegionList[i].CountryId == 1)
                    {
                        LegionMessage1.transform.Find("flag").GetComponent<UISprite>().spriteName = "guozhanicon1";
                    }
                    else
                    {
                        LegionMessage1.transform.Find("flag").GetComponent<UISprite>().spriteName = "guozhanicon2";
                    }
                }
                else if (i == 1)
                {
                    LegionMessage2.SetActive(true);
                    LegionMessage2.GetComponent<UISprite>().spriteName = "legionFlag" + OneTypeLegionList[i].LegionFlag;
                    LegionMessage2.transform.Find("LabelNumber").GetComponent<UILabel>().text = "占领:" + OneTypeLegionList[i].LegionNum.ToString();
                    LegionMessage2.transform.Find("LegionName").GetComponent<UILabel>().text = OneTypeLegionList[i].LegionName;
                    //if (OneTypeLegionList[i].LegionName == CharacterRecorder.instance.legionName)
                    //{
                    //    LegionMessage2.transform.Find("flag").GetComponent<UISprite>().spriteName = "bingtuanzhan_icon4_2";
                    //}
                    //else
                    //{
                    //    LegionMessage2.transform.Find("flag").GetComponent<UISprite>().spriteName = "bingtuanzhan_icon4_3";
                    //}
                    if (OneTypeLegionList[i].CountryId == 1)
                    {
                        LegionMessage2.transform.Find("flag").GetComponent<UISprite>().spriteName = "guozhanicon1";
                    }
                    else
                    {
                        LegionMessage2.transform.Find("flag").GetComponent<UISprite>().spriteName = "guozhanicon2";
                    }
                }
                else if (i == 2)
                {
                    LegionMessage3.SetActive(true);
                    LegionMessage3.GetComponent<UISprite>().spriteName = "legionFlag" + OneTypeLegionList[i].LegionFlag;
                    LegionMessage3.transform.Find("LabelNumber").GetComponent<UILabel>().text = "占领:" + OneTypeLegionList[i].LegionNum.ToString();
                    LegionMessage3.transform.Find("LegionName").GetComponent<UILabel>().text = OneTypeLegionList[i].LegionName;
                    //if (OneTypeLegionList[i].LegionName == CharacterRecorder.instance.legionName)
                    //{
                    //    LegionMessage3.transform.Find("flag").GetComponent<UISprite>().spriteName = "bingtuanzhan_icon4_2";
                    //}
                    //else
                    //{
                    //    LegionMessage3.transform.Find("flag").GetComponent<UISprite>().spriteName = "bingtuanzhan_icon4_3";
                    //}
                    if (OneTypeLegionList[i].CountryId == 1)
                    {
                        LegionMessage3.transform.Find("flag").GetComponent<UISprite>().spriteName = "guozhanicon1";
                    }
                    else
                    {
                        LegionMessage3.transform.Find("flag").GetComponent<UISprite>().spriteName = "guozhanicon2";
                    }
                }
                //else if (i == 3)
                //{
                //    LegionMessage4.SetActive(true);
                //    LegionMessage4.GetComponent<UISprite>().spriteName = "legionFlag" + OneTypeLegionList[i].LegionFlag;
                //    LegionMessage4.transform.Find("LabelNumber").GetComponent<UILabel>().text = "占领:" + OneTypeLegionList[i].LegionNum.ToString();
                //    LegionMessage4.transform.Find("LegionName").GetComponent<UILabel>().text = OneTypeLegionList[i].LegionName;
                //    if (OneTypeLegionList[i].LegionName == CharacterRecorder.instance.legionName)
                //    {
                //        LegionMessage4.transform.Find("flag").GetComponent<UISprite>().spriteName = "bingtuanzhan_icon4_2";
                //    }
                //    else
                //    {
                //        LegionMessage4.transform.Find("flag").GetComponent<UISprite>().spriteName = "bingtuanzhan_icon4_3";
                //    }
                //}

            }
        }
        */
        #endregion
    }


    /// <summary>
    /// 军团占领城市点数量排名
    /// </summary>
    private void LookOfLegionRankPart() 
    {
        if (OneTypeLegionList.Count > 0) 
        {
            GameObject Item = LegionRankPart.transform.Find("Item").gameObject;
            GameObject RankCloseButton = LegionRankPart.transform.Find("All/CloseButton").gameObject;
            GameObject RankGrid = LegionRankPart.transform.Find("All/ScrollView/Grid").gameObject;
            if (UIEventListener.Get(RankCloseButton).onClick == null) 
            {
                UIEventListener.Get(RankCloseButton).onClick += delegate(GameObject go)
                {
                    LegionRankPart.SetActive(false);
                };
            }
            for (int i = RankGrid.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(RankGrid.transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < OneTypeLegionList.Count; i++)
            {
                GameObject go = NGUITools.AddChild(RankGrid, Item);
                go.transform.Find("LegionName").GetComponent<UILabel>().text = OneTypeLegionList[i].LegionName;
                go.transform.Find("CityNumber").GetComponent<UILabel>().text = OneTypeLegionList[i].LegionNum.ToString();
                switch (i) 
                {
                    case 0:
                        go.transform.Find("RankSprite").gameObject.SetActive(true);
                        go.transform.Find("RankSprite").GetComponent<UISprite>().spriteName = "word1";
                        break;
                    case 1:
                        go.transform.Find("RankSprite").gameObject.SetActive(true);
                        go.transform.Find("RankSprite").GetComponent<UISprite>().spriteName = "word2";
                        break;
                    case 2:
                        go.transform.Find("RankSprite").gameObject.SetActive(true);
                        go.transform.Find("RankSprite").GetComponent<UISprite>().spriteName = "word3";
                        break;
                    default:
                        go.transform.Find("RankLabel").gameObject.SetActive(true);
                        go.transform.Find("RankLabel").GetComponent<UILabel>().text = (i+1).ToString();
                        break;
                }
                go.SetActive(true);
            }
            RankGrid.transform.parent.localPosition = new Vector3(66.1f, -67.7f, 0);
            RankGrid.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2(0, 0);
            RankGrid.GetComponent<UIGrid>().Reposition();
            LegionRankPart.SetActive(true);
        }
    }



    /// <summary>
    /// 军团战按钮响应事件
    /// </summary>

                                                    //战场点ID ;    占领帮会ID;         帮会名称;       帮会旗;        资源产出;       被宣战状态;        援军数量;  是否有上兵1有0没有;   免战状态;         哪个帮会宣战;          剩馀宣战次数;    占领军团等级
    private void ClickPointInfoWindowSetButton(int LegionPoint, int LegionID, string LegionName, int LegionFlag, string ItemStr, int DeclareDate, int Reinforcement, int IsSoldier, int IsAvoidWar, string DeclareLegionName, int DeclareHaveNum, int LegionGrade, int CountryID, string PointAward) ////8602
    {

        UIEventListener.Get(ZhanButton).onClick = delegate(GameObject go)
        {
            if (CharacterRecorder.instance.legionID != 0)
            {
                if (IsAvoidWar == 1 || IsAvoidWar == 2)
                {
                    if (CharacterRecorder.instance.isLegionChairman == false)
                    {
                        UIManager.instance.OpenPromptWindow("只有军团长才可操作此功能!", PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        NoOneOccupationPart(LegionPoint, LegionID, LegionName, LegionFlag, ItemStr, DeclareDate, Reinforcement, IsSoldier, IsAvoidWar, DeclareLegionName, DeclareHaveNum, LegionGrade, PointAward);
                    }
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("占领失败，每日5：00~20：30开放军团占领!", PromptWindow.PromptType.Hint, null, null);
                }
            }
            else 
            {
                UIManager.instance.OpenPromptWindow("只有加入军团才可做此操作!", PromptWindow.PromptType.Hint, null, null);
            }
        };

        UIEventListener.Get(XuanButton).onClick = delegate(GameObject go)
        {
            if (IsAvoidWar == 1)
            {
                if (LegionID == CharacterRecorder.instance.myLegionData.legionId) //当该城市为我方城市时
                {
                    if (CharacterRecorder.instance.isLegionChairman)
                    {
                        UIManager.instance.OpenPromptWindow("您无需对我方城市宣战!", PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("只有军团长才可宣战!", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareDate == 0) //当该城市为敌方城市但未宣战时
                {
                    if (CharacterRecorder.instance.isLegionChairman)
                    {
                        SomeOneOccupationPart(LegionPoint, LegionID, LegionName, LegionFlag, ItemStr, DeclareDate, Reinforcement, IsSoldier, IsAvoidWar, DeclareLegionName, DeclareHaveNum, LegionGrade, PointAward);
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("只有军团长才可宣战!", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareLegionName != CharacterRecorder.instance.myLegionData.legionName) //当该城市为敌方城市但其他军团宣战时
                {
                    if (CharacterRecorder.instance.isLegionChairman)
                    {
                        UIManager.instance.OpenPromptWindow("该城市已被其它军团宣战!", PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("只有军团长才可宣战!", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareLegionName == CharacterRecorder.instance.myLegionData.legionName) //当该城市为敌方城市且我方军团宣战时
                {
                    if (CharacterRecorder.instance.isLegionChairman)
                    {
                        UIManager.instance.OpenPromptWindow("您已经宣战过了!", PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("只有军团长才可宣战!", PromptWindow.PromptType.Hint, null, null);
                    }
                }
            }
            else
            {
                UIManager.instance.OpenPromptWindow("宣战失败，每日5：00~20：00开放军团宣战!", PromptWindow.PromptType.Hint, null, null);
            }
        };


        UIEventListener.Get(ChaButton).onClick = delegate(GameObject go)//查
        {
            LockInfoOccupationPart(LegionPoint, LegionID, LegionName, LegionFlag, ItemStr, DeclareDate, Reinforcement, IsSoldier, IsAvoidWar, DeclareLegionName, DeclareHaveNum, LegionGrade, PointAward);
        };

        UIEventListener.Get(GongButton).onClick = delegate(GameObject go)
        {
            if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareDate == 0) //当该城市为敌方城市但未宣战时
            {
                if (IsAvoidWar == 4 || (IsAvoidWar == 3 && DeclareDate != 2))
                {
                    UIManager.instance.OpenPromptWindow("进入失败，每日5：00~20：00开放军团布阵!", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("只有宣战的城市才可布阵!", PromptWindow.PromptType.Hint, null, null);
                }
            }
            else if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareLegionName == CharacterRecorder.instance.myLegionData.legionName) //当该城市为敌方城市且我方军团宣战时
            {
                if (IsAvoidWar == 1 || IsAvoidWar == 2)
                {
                    CharacterRecorder.instance.IsNoFightEntLegion = true;
                    UIManager.instance.OpenPanel("LegionFightWindow", true);
                }
                else if (IsAvoidWar == 3 && DeclareDate == 2)
                {
                    CharacterRecorder.instance.IsNoFightEntLegion = false;
                    UIManager.instance.OpenPanel("LegionFightWindow", true);
                }
                else if (IsAvoidWar == 4 || (IsAvoidWar == 3 && DeclareDate != 2))
                {
                    UIManager.instance.OpenPromptWindow("进入失败，每日5：00~20：00开放军团布阵!", PromptWindow.PromptType.Hint, null, null);
                }
            }
        };

        UIEventListener.Get(FangButton).onClick = delegate(GameObject go)  //防
        {
            if (LegionID == CharacterRecorder.instance.myLegionData.legionId) //当该城市为我方
            {
                if (IsAvoidWar == 1 || IsAvoidWar == 2)
                {
                    CharacterRecorder.instance.IsNoFightEntLegion = true;
                    UIManager.instance.OpenPanel("LegionFightWindow", true);
                }
                else if (IsAvoidWar == 3 && DeclareDate == 2)
                {
                    CharacterRecorder.instance.IsNoFightEntLegion = false;
                    UIManager.instance.OpenPanel("LegionFightWindow", true);
                }
                else if (IsAvoidWar == 4 || (IsAvoidWar == 3 && DeclareDate != 2))
                {
                    UIManager.instance.OpenPromptWindow("进入失败，每日5：00~20：00开放军团布阵!", PromptWindow.PromptType.Hint, null, null);
                }
            }
        };

        UIEventListener.Get(GuanButton).onClick = delegate(GameObject go)//观察
        {
            if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareLegionName != CharacterRecorder.instance.myLegionData.legionName)
            {
                if (IsAvoidWar == 1 || IsAvoidWar == 2)
                {
                    CharacterRecorder.instance.IsNoFightEntLegion = true;
                    UIManager.instance.OpenPanel("LegionFightWindow", true);
                }
                else
                {
                    CharacterRecorder.instance.IsNoFightEntLegion = false;
                    UIManager.instance.OpenPanel("LegionFightWindow", true);
                }
            }
        };

        UIEventListener.Get(JiButton).onClick = delegate(GameObject go)
        {
            if (IsAvoidWar == 4 || (IsAvoidWar == 3 && DeclareDate != 2))
            {
                UIManager.instance.OpenPromptWindow("指挥失败，每日5：00~20：00开放军团指挥!", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                if (LegionID == CharacterRecorder.instance.myLegionData.legionId) ////当该城市为我方城市时   
                {
                    if (CharacterRecorder.instance.isLegionChairman)
                    {
                        NetworkHandler.instance.SendProcess("8625#" + CharacterRecorder.instance.LegionHarasPoint + ";");
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("只有军团长才可操作此功能!", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareDate == 0) //当该城市为敌方城市但未宣战时
                {
                    if (CharacterRecorder.instance.isLegionChairman)
                    {
                        UIManager.instance.OpenPromptWindow("只有宣战的城市才可标记进攻!", PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("只有军团长才可操作此功能!", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else if (LegionID != CharacterRecorder.instance.myLegionData.legionId &&DeclareLegionName != CharacterRecorder.instance.myLegionData.legionName) //当该城市为敌方城市但其他军团宣战时
                {
                    if (CharacterRecorder.instance.isLegionChairman)
                    {
                        UIManager.instance.OpenPromptWindow("这个城市您并未宣战!", PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("只有军团长才可操作此功能!", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareLegionName == CharacterRecorder.instance.myLegionData.legionName) //当该城市为敌方城市且我方军团宣战时
                {
                    if (CharacterRecorder.instance.isLegionChairman)
                    {
                        NetworkHandler.instance.SendProcess("8625#" + CharacterRecorder.instance.LegionHarasPoint + ";");
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("只有军团长才可操作此功能!", PromptWindow.PromptType.Hint, null, null);
                    }
                }
            }
        };
    }



    /// <summary>
    /// 骚扰按钮响应事件
    /// </summary>
                                                    //战场点ID ;    占领帮会ID;         帮会名称;       帮会旗;        资源产出;       被宣战状态;        援军数量;  是否有上兵1有0没有;   免战状态;         哪个帮会宣战;          剩馀宣战次数;  军团等级;
    public void ClickLittlePointWindowSetButton(int LegionPoint, int LegionID, string LegionName, int LegionFlag, string ItemStr, int DeclareDate, int Reinforcement, int IsSoldier, int IsAvoidWar, string DeclareLegionName, int DeclareHaveNum, int LegionGrade, int CountryID, string PointAward)
    {

        UIEventListener.Get(ClickLittlePointWindow.transform.Find("ZhanIcon").gameObject).onClick = delegate(GameObject go)
        {
            NoOneOccupationPart(LegionPoint, LegionID, LegionName, LegionFlag, ItemStr, DeclareDate, Reinforcement, IsSoldier, IsAvoidWar, DeclareLegionName, DeclareHaveNum, LegionGrade, PointAward);
        };

        UIEventListener.Get(ClickLittlePointWindow.transform.Find("XuanIcon").gameObject).onClick = delegate(GameObject go)
        {

            SomeOneOccupationPart(LegionPoint, LegionID, LegionName, LegionFlag, ItemStr, DeclareDate, Reinforcement, IsSoldier, IsAvoidWar, DeclareLegionName, DeclareHaveNum, LegionGrade, PointAward);
        };

        UIEventListener.Get(ClickLittlePointWindow.transform.Find("ChaIcon").gameObject).onClick = delegate(GameObject go)
        {
            if (CharacterRecorder.instance.legionID == 0) //未加入任何军团
            {
                if (LegionID == 0||LegionName == "")
                {
                    NoOneHarassmentPart(LegionPoint, LegionID, LegionName, LegionFlag, ItemStr, DeclareDate, Reinforcement, IsSoldier, IsAvoidWar, DeclareLegionName, DeclareHaveNum, LegionGrade, PointAward);
                }
                else
                {
                    SomeOneHarassmentPart(LegionPoint, LegionID, LegionName, LegionFlag, ItemStr, DeclareDate, Reinforcement, IsSoldier, IsAvoidWar, DeclareLegionName, DeclareHaveNum, LegionGrade, PointAward);
                }
            }
            else if (LegionID == 0 || LegionName == "")//无人骚扰
            {
                NoOneHarassmentPart(LegionPoint, LegionID, LegionName, LegionFlag, ItemStr, DeclareDate, Reinforcement, IsSoldier, IsAvoidWar, DeclareLegionName, DeclareHaveNum, LegionGrade, PointAward);
            }
            else if (LegionName != CharacterRecorder.instance.myLegionData.legionName)//据点非我，骚扰
            {
                SomeOneHarassmentPart(LegionPoint, LegionID, LegionName, LegionFlag, ItemStr, DeclareDate, Reinforcement, IsSoldier, IsAvoidWar, DeclareLegionName, DeclareHaveNum, LegionGrade, PointAward);
            }
            else if (LegionName == CharacterRecorder.instance.myLegionData.legionName) //据点为我，驻守
            {
                SomeOneHarassmentPart(LegionPoint, LegionID, LegionName, LegionFlag, ItemStr, DeclareDate, Reinforcement, IsSoldier, IsAvoidWar, DeclareLegionName, DeclareHaveNum, LegionGrade, PointAward);
            }
        };

        UIEventListener.Get(ClickLittlePointWindow.transform.Find("QiIcon").gameObject).onClick = delegate(GameObject go)
        {
            int cityType = TextTranslator.instance.GetLegionCityByID(LegionPoint).CityType;

            if (cityType < 5) 
            {
                UIManager.instance.OpenPromptWindow("D级以上城市不可以放弃!", PromptWindow.PromptType.Hint, null, null);
            }
            else if (CharacterRecorder.instance.myLegionPosition >= 2)
            {
                if (LegionID == 0)
                {
                    UIManager.instance.OpenPromptWindow("您还没有占领此据点!", PromptWindow.PromptType.Hint, null, null);
                }
                else if (LegionName != CharacterRecorder.instance.myLegionData.legionName)//据点非我
                {
                    UIManager.instance.OpenPromptWindow("您无权操作其他军团据点!", PromptWindow.PromptType.Hint, null, null);
                }
                else if (LegionName == CharacterRecorder.instance.myLegionData.legionName) //据点为我
                {
                    MyHarassmentPart(LegionPoint, LegionID, LegionName, LegionFlag, ItemStr, DeclareDate, Reinforcement, IsSoldier, IsAvoidWar, DeclareLegionName, DeclareHaveNum, LegionGrade, PointAward);
                }
            }
            else
            {
                UIManager.instance.OpenPromptWindow("只有军团长或副团长才可弃城!", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(ClickLittlePointWindow.transform.Find("JiIcon").gameObject).onClick = delegate(GameObject go)
        {
            if (CharacterRecorder.instance.myLegionPosition >= 2)
            {
                int NeedLevel = TextTranslator.instance.GetLegionCityByID(LegionPoint).LegionNeedLevel;
                if (NeedLevel <= CharacterRecorder.instance.myLegionData.legionLevel)
                {
                    NetworkHandler.instance.SendProcess("8625#" + CharacterRecorder.instance.LegionHarasPoint + ";");
                }
                else 
                {
                    UIManager.instance.OpenPromptWindow("军团等级不足,无法集火!", PromptWindow.PromptType.Hint, null, null);
                }
                
            }
            else 
            {
                UIManager.instance.OpenPromptWindow("只有军团长或副团长才可集火!", PromptWindow.PromptType.Hint, null, null);
            }
        };
    }



    /// <summary>
    /// 所有单个城市点具体信息
    /// </summary>
    private void AddLegionwarGetnodeList(int LegionPoint, int LegionID, string LegionName, int LegionFlag, string ItemStr, int DeclareDate, int Reinforcement, int IsSoldier, int IsAvoidWar, string DeclareLegionName, int DeclareHaveNum, int LegionGrade, int CountryID) 
    {
        bool Ishave = false;
        for (int i = 0; i < CharacterRecorder.instance.LegionwarGetnodeList.Count; i++) 
        {
            if (CharacterRecorder.instance.LegionwarGetnodeList[i].LegionPoint == LegionPoint) 
            {
                Ishave = true;
                CharacterRecorder.instance.LegionwarGetnodeList[i]=new LegionwarGetnode (LegionPoint, LegionID, LegionName, LegionFlag, ItemStr, DeclareDate, Reinforcement, IsSoldier, IsAvoidWar, DeclareLegionName, DeclareHaveNum, LegionGrade, CountryID);
                break;
            }
        }

        if (Ishave == false) 
        {
            CharacterRecorder.instance.LegionwarGetnodeList.Add(new LegionwarGetnode(LegionPoint, LegionID, LegionName, LegionFlag, ItemStr, DeclareDate, Reinforcement, IsSoldier, IsAvoidWar, DeclareLegionName, DeclareHaveNum, LegionGrade, CountryID));
        }
    }
    
    /// <summary>
    /// 单个城市点具体信息，8602协议用
    /// </summary>
                                    //战场点ID ;    占领帮会ID;         帮会名称;       帮会旗;        资源产出;       被宣战状态;        援军数量;  是否有上兵1有0没有;   免战状态;         哪个帮会宣战;          剩馀宣战次数;  军团等级;         国家;           城市特殊奖励
    public void LegionWarGetNode(int LegionPoint, int LegionID, string LegionName, int LegionFlag, string ItemStr, int DeclareDate, int Reinforcement, int IsSoldier, int IsAvoidWar, string DeclareLegionName, int DeclareHaveNum, int LegionGrade, int CountryID,string PointAward)
    {
        CharacterRecorder.instance.LegionHarasPoint = LegionPoint;
        //AddLegionwarGetnodeList( LegionPoint,  LegionID,  LegionName,  LegionFlag,  ItemStr,  DeclareDate,  Reinforcement,  IsSoldier,  IsAvoidWar,  DeclareLegionName,  DeclareHaveNum,  LegionGrade,  CountryID);
        int cityType = TextTranslator.instance.GetLegionCityByID(LegionPoint).CityType;
        int NeedLevel = TextTranslator.instance.GetLegionCityByID(LegionPoint).LegionNeedLevel;

        ClickLittlePointWindow.SetActive(true);
        Vector3 vecposition = ArrLegionPoint[LegionPoint - 1].transform.localPosition;
        ClickLittlePointWindow.transform.localPosition = new Vector3(vecposition.x, vecposition.y, vecposition.z);

        TeamHero1.SetActive(false);
        TeamHero2.SetActive(false);
        TeamHero3.SetActive(false);
        SetLittleTweenScale();
        SaoRaoHeroButton(LegionPoint);


        if (cityType < 5 && LegionID == 0 && CharacterRecorder.instance.myLegionPosition>=2) //占领&& (IsAvoidWar == 1 || IsAvoidWar==2)
        {
            ClickLittlePointWindow.transform.Find("ZhanIcon").gameObject.SetActive(true);
        }

        if (cityType < 5 && LegionID != 0 && CharacterRecorder.instance.myLegionPosition >= 2) //宣战&& IsAvoidWar == 1
        {
            ClickLittlePointWindow.transform.Find("XuanIcon").gameObject.SetActive(true);
        }

        if (cityType == 5 ||CharacterRecorder.instance.myLegionPosition<2) //查看
        {
            ClickLittlePointWindow.transform.Find("ChaIcon").gameObject.SetActive(true);
        }


        ClickLittlePointWindow.transform.Find("QiIcon").gameObject.SetActive(true);//弃
        ClickLittlePointWindow.transform.Find("JiIcon").gameObject.SetActive(true);//集火


        if (CharacterRecorder.instance.myLegionPosition >= 2)
        {
            if (LegionName != CharacterRecorder.instance.myLegionData.legionName)
            {
                ClickLittlePointWindow.transform.Find("JiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon7";
            }
            else 
            {
                ClickLittlePointWindow.transform.Find("JiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon15";
            }

            if (cityType < 5)
            {
                ClickLittlePointWindow.transform.Find("QiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon13_2";    
            }
            else 
            {
                ClickLittlePointWindow.transform.Find("QiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon13";
            }
        }
        else
        {
            ClickLittlePointWindow.transform.Find("JiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon7_2";
            ClickLittlePointWindow.transform.Find("QiIcon").GetComponent<UISprite>().spriteName = "juntuan_icon13_2";
        }

        ClickLittlePointWindowSetButton( LegionPoint,  LegionID,  LegionName,  LegionFlag,  ItemStr,  DeclareDate,  Reinforcement,  IsSoldier,  IsAvoidWar,  DeclareLegionName,  DeclareHaveNum,  LegionGrade,  CountryID,PointAward);
    }


    /// <summary>
    /// 弹出信息窗口
    /// </summary>
                                //战场点ID ;    占领帮会ID;         帮会名称;       帮会旗;        资源产出;       被宣战状态;        援军数量;  是否有上兵1有0没有;   免战状态;         哪个帮会宣战;          剩馀宣战次数;
    void NoOneOccupationPart(int LegionPoint, int LegionID, string LegionName, int LegionFlag, string ItemStr, int DeclareDate, int Reinforcement, int IsSoldier, int IsAvoidWar, string DeclareLegionName, int DeclareHaveNum, int LegionGrade, string PointAward) 
    {
        ClickPointInfoWindow.SetActive(false);
        ClickLittlePointWindow.SetActive(false);
        Debug.LogError("占领");
        LegionInfoWindow.SetActive(true);
        NoOneOccupation.SetActive(true);
        SomeOneOccupation.SetActive(false);
        LockInfoOccupation.SetActive(false);
        NoOneHarassment.SetActive(false);
        SomeOneHarassment.SetActive(false);
        MyHarassment.SetActive(false);
        ZhuHarassment.SetActive(false);

        NoOneOccupation.transform.Find("ItemIcon1").gameObject.SetActive(false);
        NoOneOccupation.transform.Find("ItemIcon2").gameObject.SetActive(false);
        NoOneOccupation.transform.Find("ItemIcon3").gameObject.SetActive(false);

        int cityType = TextTranslator.instance.GetLegionCityByID(LegionPoint).CityType;
        int NeedLevel = TextTranslator.instance.GetLegionCityByID(LegionPoint).LegionNeedLevel;
        LegionCity legionCity = TextTranslator.instance.GetLegionCityByID(LegionPoint);
        if (legionCity.ItemID1 > 0)
        {
            NoOneOccupation.transform.Find("ItemIcon1").gameObject.SetActive(true);
            NoOneOccupation.transform.Find("ItemIcon1").GetComponent<UISprite>().spriteName = legionCity.ItemID1.ToString();
            NoOneOccupation.transform.Find("ItemIcon1/LabelNumber").GetComponent<UILabel>().text = "x" + legionCity.ItemNum1.ToString() + "/小时";
        }


        string[] awardSplit = PointAward.Split('!');
        string[] trcSplit = awardSplit[0].Split('$');

        if (trcSplit[0] != "0") 
        {
            NoOneOccupation.transform.Find("ItemIcon2").gameObject.SetActive(true);
            SetItemDetail(int.Parse(trcSplit[0]), NoOneOccupation.transform.Find("ItemIcon2").GetComponent<UISprite>());
            NoOneOccupation.transform.Find("ItemIcon2/LabelNumber").GetComponent<UILabel>().text = "x" + trcSplit[1] + "/6小时";
            TextTranslator.instance.ItemDescription(NoOneOccupation.transform.Find("ItemIcon2").gameObject, int.Parse(trcSplit[0]), 0);
        }       

        NoOneOccupation.transform.Find("Label").GetComponent<UILabel>().text = "此据点还无人占领";
        NoOneOccupation.transform.Find("OccupationButton").transform.Find("Label").GetComponent<UILabel>().text = "占 领";

        UIEventListener.Get(NoOneOccupation.transform.Find("OccupationButton").gameObject).onClick = delegate(GameObject go)//占领
        {
            if (NeedLevel <= CharacterRecorder.instance.myLegionData.legionLevel) //军团等级达到
            {
                if (CharacterRecorder.instance.MarinesInfomation1.CityId != CharacterRecorder.instance.LegionHarasPoint && CharacterRecorder.instance.MarinesInfomation2.CityId != CharacterRecorder.instance.LegionHarasPoint && CharacterRecorder.instance.MarinesInfomation3.CityId != CharacterRecorder.instance.LegionHarasPoint)
                {
                    UIManager.instance.OpenPromptWindow("需要此城市有编队才能占领!", PromptWindow.PromptType.Hint, null, null);
                }
                else if (DeclareHaveNum == 0)
                {
                    UIManager.instance.OpenPromptWindow("每天只能进行一次宣战或占领行动!", PromptWindow.PromptType.Hint, null, null);
                }
                else if (IsAvoidWar != 1 && IsAvoidWar != 2)
                {
                    UIManager.instance.OpenPromptWindow("占领失败，每日5：00~20：30开放军团占领!", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    int num = 0;
                    if (CharacterRecorder.instance.MarinesInfomation1.CityId == CharacterRecorder.instance.LegionHarasPoint)
                    {
                        num = 1;
                    }
                    else if (CharacterRecorder.instance.MarinesInfomation2.CityId == CharacterRecorder.instance.LegionHarasPoint)
                    {
                        num = 2;
                    }
                    else if (CharacterRecorder.instance.MarinesInfomation3.CityId == CharacterRecorder.instance.LegionHarasPoint)
                    {
                        num = 3;
                    }
                    if (num != 0)
                    {
                        NetworkHandler.instance.SendProcess("8603#" + CharacterRecorder.instance.LegionHarasPoint + ";" + num + ";");
                    }
                    else
                    {
                        Debug.LogError("占领失败,进入此代表判断错误");
                    }
                    LegionInfoWindow.SetActive(false);
                }
            }
            else
            {
                UIManager.instance.OpenPromptWindow("军团等级不足,无法占领!", PromptWindow.PromptType.Hint, null, null);
            }
        };

        UIEventListener.Get(NoOneOccupation.transform.Find("LockFightButton").gameObject).onClick += delegate(GameObject go)
        {
            UIManager.instance.OpenPanel("LegionDeclareWarWindow", true);
        };

        NoOneOccupation.transform.Find("NeedLevelNumber").GetComponent<UILabel>().text = NeedLevel.ToString() + "级";
        if (NeedLevel <= CharacterRecorder.instance.myLegionData.legionLevel)
        {
            NoOneOccupation.transform.Find("NeedLevelNumber").GetComponent<UILabel>().color = new Color(62 / 255f, 232 / 255f, 23 / 255f, 255 / 255f);
            NoOneOccupation.transform.Find("OccupationButton").GetComponent<UISprite>().spriteName = "button1";
            NoOneOccupation.transform.Find("OccupationButton").GetComponent<UIButton>().normalSprite = "button1";
            NoOneOccupation.transform.Find("OccupationButton").GetComponent<UIButton>().hoverSprite = "button1";
            NoOneOccupation.transform.Find("OccupationButton").GetComponent<UIButton>().pressedSprite = "button1_an";
            NoOneOccupation.transform.Find("OccupationButton").transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(0 / 255f, 122 / 255f, 245 / 255f);
        }
        else
        {
            NoOneOccupation.transform.Find("NeedLevelNumber").GetComponent<UILabel>().color = new Color(248 / 255f, 52 / 255f, 79 / 255f, 255 / 255f);
            NoOneOccupation.transform.Find("OccupationButton").GetComponent<UISprite>().spriteName = "buttonHui";
            NoOneOccupation.transform.Find("OccupationButton").GetComponent<UIButton>().normalSprite = "buttonHui";
            NoOneOccupation.transform.Find("OccupationButton").GetComponent<UIButton>().hoverSprite = "buttonHui";
            NoOneOccupation.transform.Find("OccupationButton").GetComponent<UIButton>().pressedSprite = "buttonHui_an";
            NoOneOccupation.transform.Find("OccupationButton").transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(103 / 255f, 96 / 255f, 96 / 255f, 255 / 255f);
        }
    }

    void SomeOneOccupationPart(int LegionPoint, int LegionID, string LegionName, int LegionFlag, string ItemStr, int DeclareDate, int Reinforcement, int IsSoldier, int IsAvoidWar, string DeclareLegionName, int DeclareHaveNum, int LegionGrade, string PointAward)
    {
        ClickPointInfoWindow.SetActive(false);
        ClickLittlePointWindow.SetActive(false);
        Debug.LogError("宣战");
        LegionInfoWindow.SetActive(true);
        NoOneOccupation.SetActive(false);
        SomeOneOccupation.SetActive(true);
        LockInfoOccupation.SetActive(false);
        NoOneHarassment.SetActive(false);
        SomeOneHarassment.SetActive(false);
        MyHarassment.SetActive(false);
        ZhuHarassment.SetActive(false);

        SomeOneOccupation.transform.Find("DeclareWarButton").gameObject.SetActive(true);
        SomeOneOccupation.transform.Find("DeclareWarHuiButton").gameObject.SetActive(false);

        SomeOneOccupation.transform.Find("ItemIcon1").gameObject.SetActive(false);
        SomeOneOccupation.transform.Find("ItemIcon2").gameObject.SetActive(false);
        SomeOneOccupation.transform.Find("ItemIcon3").gameObject.SetActive(false);


        SomeOneOccupation.transform.Find("LegionIcon").GetComponent<UISprite>().spriteName = "legionFlag" + LegionFlag.ToString();
        //SomeOneOccupation.transform.Find("LegionLevel").GetComponent<UILabel>().text = "Lv." + "15";
        SomeOneOccupation.transform.Find("LegionName").GetComponent<UILabel>().text = LegionName;
        if (DeclareLegionName != "")
        {
            SomeOneOccupation.transform.Find("LabelMessage").GetComponent<UILabel>().text = DeclareLegionName+"已经向该据点发起宣战";
        }
        else 
        {
            SomeOneOccupation.transform.Find("LabelMessage").GetComponent<UILabel>().text = "可向该据点发起宣战";
        }
       
        int cityType = TextTranslator.instance.GetLegionCityByID(LegionPoint).CityType;
        int NeedLevel = TextTranslator.instance.GetLegionCityByID(LegionPoint).LegionNeedLevel;
        LegionCity legionCity = TextTranslator.instance.GetLegionCityByID(LegionPoint);
        if (legionCity.ItemID1 > 0)
        {
            SomeOneOccupation.transform.Find("ItemIcon1").gameObject.SetActive(true);
            SomeOneOccupation.transform.Find("ItemIcon1").GetComponent<UISprite>().spriteName = legionCity.ItemID1.ToString();
            SomeOneOccupation.transform.Find("ItemIcon1/LabelNumber").GetComponent<UILabel>().text = "x" + legionCity.ItemNum1.ToString() + "/小时";            
        }

        string[] awardSplit = PointAward.Split('!');
        string[] trcSplit = awardSplit[0].Split('$');

        if (trcSplit[0] != "0")
        {
            SomeOneOccupation.transform.Find("ItemIcon2").gameObject.SetActive(true);
            SetItemDetail(int.Parse(trcSplit[0]), SomeOneOccupation.transform.Find("ItemIcon2").GetComponent<UISprite>());
            SomeOneOccupation.transform.Find("ItemIcon2/LabelNumber").GetComponent<UILabel>().text = "x" + trcSplit[1] + "/6小时";
            TextTranslator.instance.ItemDescription(SomeOneOccupation.transform.Find("ItemIcon2").gameObject, int.Parse(trcSplit[0]), 0);
        }   

        UIEventListener.Get(SomeOneOccupation.transform.Find("DeclareWarButton").gameObject).onClick = delegate(GameObject go)
        {
            if (NeedLevel <= CharacterRecorder.instance.myLegionData.legionLevel) //军团等级达到
            {
                if (CharacterRecorder.instance.MarinesInfomation1.CityId != CharacterRecorder.instance.LegionHarasPoint && CharacterRecorder.instance.MarinesInfomation2.CityId != CharacterRecorder.instance.LegionHarasPoint && CharacterRecorder.instance.MarinesInfomation3.CityId != CharacterRecorder.instance.LegionHarasPoint)
                {
                    UIManager.instance.OpenPromptWindow("需要此城市有编队才能宣战!", PromptWindow.PromptType.Hint, null, null);
                }
                else if (DeclareHaveNum == 0)
                {
                    UIManager.instance.OpenPromptWindow("每天只能进行一次宣战或占领行动!", PromptWindow.PromptType.Hint, null, null);
                }
                else if (IsAvoidWar != 1)
                {
                    UIManager.instance.OpenPromptWindow("宣战失败，每日5：00~20：00开放军团宣战!", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    if (LegionID == CharacterRecorder.instance.myLegionData.legionId) //当该城市为我方城市时
                    {
                        UIManager.instance.OpenPromptWindow("您无需对我方城市宣战!", PromptWindow.PromptType.Hint, null, null);
                    }
                    else if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareDate == 0) //当该城市为敌方城市但未宣战时
                    {
                        int num = 0;
                        if (CharacterRecorder.instance.MarinesInfomation1.CityId == CharacterRecorder.instance.LegionHarasPoint)
                        {
                            num = 1;
                        }
                        else if (CharacterRecorder.instance.MarinesInfomation2.CityId == CharacterRecorder.instance.LegionHarasPoint)
                        {
                            num = 2;
                        }
                        else if (CharacterRecorder.instance.MarinesInfomation3.CityId == CharacterRecorder.instance.LegionHarasPoint)
                        {
                            num = 3;
                        }
                        if (num != 0)
                        {
                            NetworkHandler.instance.SendProcess("8603#" + CharacterRecorder.instance.LegionHarasPoint + ";" + num + ";");
                        }
                        else
                        {
                            Debug.LogError("占领失败,进入此代表判断错误");
                        }
                        LegionInfoWindow.SetActive(false);
                    }
                    else if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareLegionName != CharacterRecorder.instance.myLegionData.legionName) //当该城市为敌方城市但其他军团宣战时
                    {
                        UIManager.instance.OpenPromptWindow("该城市已被其它军团宣战!", PromptWindow.PromptType.Hint, null, null);
                    }
                    else if (LegionID != CharacterRecorder.instance.myLegionData.legionId && DeclareLegionName == CharacterRecorder.instance.myLegionData.legionName) //当该城市为敌方城市且我方军团宣战时
                    {
                        UIManager.instance.OpenPromptWindow("您已经宣战过了!", PromptWindow.PromptType.Hint, null, null);
                    }
                }
            }
            else
            {
                UIManager.instance.OpenPromptWindow("军团等级不足,无法宣战!", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(SomeOneOccupation.transform.Find("LockFightButton").gameObject).onClick += delegate(GameObject go)
        {
            UIManager.instance.OpenPanel("LegionDeclareWarWindow", true);
        };


        SomeOneOccupation.transform.Find("NeedLevelNumber").GetComponent<UILabel>().text = NeedLevel.ToString() + "级";
        if ((NeedLevel <= CharacterRecorder.instance.myLegionData.legionLevel) && CharacterRecorder.instance.isLegionChairman)
        {
            SomeOneOccupation.transform.Find("NeedLevelNumber").GetComponent<UILabel>().color = new Color(62 / 255f, 232 / 255f, 23 / 255f, 255 / 255f);
            SomeOneOccupation.transform.Find("DeclareWarButton").GetComponent<UISprite>().spriteName = "button1";
            SomeOneOccupation.transform.Find("DeclareWarButton").GetComponent<UIButton>().normalSprite = "button1";
            SomeOneOccupation.transform.Find("DeclareWarButton").GetComponent<UIButton>().hoverSprite = "button1";
            SomeOneOccupation.transform.Find("DeclareWarButton").GetComponent<UIButton>().pressedSprite = "button1_an";
            SomeOneOccupation.transform.Find("DeclareWarButton").transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(0 / 255f, 122 / 255f, 245 / 255f);
        }
        else
        {
            SomeOneOccupation.transform.Find("NeedLevelNumber").GetComponent<UILabel>().color = new Color(248 / 255f, 52 / 255f, 79 / 255f, 255 / 255f);
            SomeOneOccupation.transform.Find("DeclareWarButton").GetComponent<UISprite>().spriteName = "buttonHui";
            SomeOneOccupation.transform.Find("DeclareWarButton").GetComponent<UIButton>().normalSprite = "buttonHui";
            SomeOneOccupation.transform.Find("DeclareWarButton").GetComponent<UIButton>().hoverSprite = "buttonHui";
            SomeOneOccupation.transform.Find("DeclareWarButton").GetComponent<UIButton>().pressedSprite = "buttonHui_an";
            SomeOneOccupation.transform.Find("DeclareWarButton").transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(103 / 255f, 96 / 255f, 96 / 255f, 255 / 255f);
        }
    }

    void LockInfoOccupationPart(int LegionPoint, int LegionID, string LegionName, int LegionFlag, string ItemStr, int DeclareDate, int Reinforcement, int IsSoldier, int IsAvoidWar, string DeclareLegionName, int DeclareHaveNum, int LegionGrade, string PointAward)
    {
        ClickPointInfoWindow.SetActive(false);
        ClickLittlePointWindow.SetActive(false);
        Debug.LogError("查看");
        LegionInfoWindow.SetActive(true);
        NoOneOccupation.SetActive(false);
        SomeOneOccupation.SetActive(false);
        LockInfoOccupation.SetActive(true);
        NoOneHarassment.SetActive(false);
        SomeOneHarassment.SetActive(false);
        MyHarassment.SetActive(false);
        ZhuHarassment.SetActive(false);

        if (LegionID == 0)
        {
            LockInfoOccupation.transform.Find("LegionName").GetComponent<UILabel>().text = "无";
            LockInfoOccupation.transform.Find("LegionRank").GetComponent<UILabel>().text = "无";
            LockInfoOccupation.transform.Find("LegionSolider").GetComponent<UILabel>().text = Reinforcement.ToString();
        }
        else 
        {
            LockInfoOccupation.transform.Find("LegionName").GetComponent<UILabel>().text = LegionName;
            LockInfoOccupation.transform.Find("LegionRank").GetComponent<UILabel>().text = LegionGrade.ToString();
            LockInfoOccupation.transform.Find("LegionSolider").GetComponent<UILabel>().text = Reinforcement.ToString();
        }

        UIEventListener.Get(LockInfoOccupation.transform.Find("KnowButton").gameObject).onClick = delegate(GameObject go)
        {
            LegionInfoWindow.SetActive(false);
        };
    }

    void NoOneHarassmentPart(int LegionPoint, int LegionID, string LegionName, int LegionFlag, string ItemStr, int DeclareDate, int Reinforcement, int IsSoldier, int IsAvoidWar, string DeclareLegionName, int DeclareHaveNum, int LegionGrade, string PointAward) 
    {
        ClickPointInfoWindow.SetActive(false);
        ClickLittlePointWindow.SetActive(false);
        Debug.LogError("骚扰");
        LegionInfoWindow.SetActive(true);
        NoOneOccupation.SetActive(false);
        SomeOneOccupation.SetActive(false);
        LockInfoOccupation.SetActive(false);
        NoOneHarassment.SetActive(true);
        SomeOneHarassment.SetActive(false);
        MyHarassment.SetActive(false);
        ZhuHarassment.SetActive(false);

        NoOneHarassment.transform.Find("ItemIcon1").gameObject.SetActive(false);
        NoOneHarassment.transform.Find("ItemIcon2").gameObject.SetActive(false);
        NoOneHarassment.transform.Find("ItemIcon3").gameObject.SetActive(false);


        NoOneHarassment.transform.Find("GuardNumber").GetComponent<UILabel>().text = Reinforcement.ToString();//守卫数量
        NoOneHarassment.transform.Find("GuardPoint").GetComponent<UILabel>().text = ActionPoint.ToString();//行动点数

        int cityType = TextTranslator.instance.GetLegionCityByID(LegionPoint).CityType;
        int NeedLevel = TextTranslator.instance.GetLegionCityByID(LegionPoint).LegionNeedLevel;
        LegionCity legionCity = TextTranslator.instance.GetLegionCityByID(LegionPoint);
        if (legionCity.ItemID1 > 0)
        {
            NoOneHarassment.transform.Find("ItemIcon1").gameObject.SetActive(true);
            NoOneHarassment.transform.Find("ItemIcon1").GetComponent<UISprite>().spriteName = legionCity.ItemID1.ToString();
            NoOneHarassment.transform.Find("ItemIcon1/LabelNumber").GetComponent<UILabel>().text = "x" + legionCity.ItemNum1.ToString() + "/小时";
        }

        string[] awardSplit = PointAward.Split('!');
        string[] trcSplit = awardSplit[0].Split('$');

        if (trcSplit[0] != "0")
        {
            NoOneHarassment.transform.Find("ItemIcon2").gameObject.SetActive(true);
            SetItemDetail(int.Parse(trcSplit[0]), NoOneHarassment.transform.Find("ItemIcon2").GetComponent<UISprite>());
            NoOneHarassment.transform.Find("ItemIcon2/LabelNumber").GetComponent<UILabel>().text = "x" + trcSplit[1] + "/6小时";
            TextTranslator.instance.ItemDescription(NoOneHarassment.transform.Find("ItemIcon2").gameObject, int.Parse(trcSplit[0]), 0);
        } 

        if (UIEventListener.Get(NoOneHarassment.transform.Find("DeclareWarButton").gameObject).onClick == null)
        {
            UIEventListener.Get(NoOneHarassment.transform.Find("DeclareWarButton").gameObject).onClick += delegate(GameObject go)
            {
                LegionInfoWindow.SetActive(false);
                //if (CharacterRecorder.instance.legionID != 0) //有军团
                //{
                //    if (NeedLevel <= CharacterRecorder.instance.myLegionData.legionLevel)
                //    {
                //        if (ActionPoint > 0)
                //        {
                //            //CharacterRecorder.instance.LegionHarasPoint = int.Parse(trcSplit[0]);
                //            NetworkHandler.instance.SendProcess("8612#" + CharacterRecorder.instance.LegionHarasPoint + ";");//发起骚扰
                //        }
                //        else
                //        {
                //            UIManager.instance.OpenPromptWindow("行动点数不足", PromptWindow.PromptType.Hint, null, null);
                //        }
                //    }
                //    else
                //    {
                //        UIManager.instance.OpenPromptWindow("军团等级不足", PromptWindow.PromptType.Hint, null, null);
                //    }
                //}
                //else 
                //{
                //    if (ActionPoint > 0)
                //    {
                //        NetworkHandler.instance.SendProcess("8612#" + CharacterRecorder.instance.LegionHarasPoint + ";");//发起骚扰
                //    }
                //    else
                //    {
                //        UIManager.instance.OpenPromptWindow("行动点数不足", PromptWindow.PromptType.Hint, null, null);
                //    }
                //}
            };
        }
        if (UIEventListener.Get(NoOneHarassment.transform.Find("LockFightButton").gameObject).onClick == null)
        {
            UIEventListener.Get(NoOneHarassment.transform.Find("LockFightButton").gameObject).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPanel("LegionDeclareWarWindow", true);
            };
        }
    }

    void SomeOneHarassmentPart(int LegionPoint, int LegionID, string LegionName, int LegionFlag, string ItemStr, int DeclareDate, int Reinforcement, int IsSoldier, int IsAvoidWar, string DeclareLegionName, int DeclareHaveNum, int LegionGrade, string PointAward) 
    {
        ClickPointInfoWindow.SetActive(false);
        ClickLittlePointWindow.SetActive(false);
        Debug.LogError("骚扰,据点非我");
        LegionInfoWindow.SetActive(true);
        NoOneOccupation.SetActive(false);
        SomeOneOccupation.SetActive(false);
        LockInfoOccupation.SetActive(false);
        NoOneHarassment.SetActive(false);
        SomeOneHarassment.SetActive(true);
        MyHarassment.SetActive(false);
        ZhuHarassment.SetActive(false);

        SomeOneHarassment.transform.Find("ItemIcon1").gameObject.SetActive(false);
        SomeOneHarassment.transform.Find("ItemIcon2").gameObject.SetActive(false);
        SomeOneHarassment.transform.Find("LegionIcon").GetComponent<UISprite>().spriteName = "legionFlag" + LegionFlag.ToString();
        SomeOneHarassment.transform.Find("LegionName").GetComponent<UILabel>().text = LegionName;
        SomeOneHarassment.transform.Find("GuardNumber").GetComponent<UILabel>().text = Reinforcement.ToString();//守卫数量
        SomeOneHarassment.transform.Find("GuardPoint").GetComponent<UILabel>().text = ActionPoint.ToString();//行动点数

        SomeOneHarassment.transform.Find("ItemIcon1").gameObject.SetActive(false);
        int cityType = TextTranslator.instance.GetLegionCityByID(LegionPoint).CityType;
        int NeedLevel = TextTranslator.instance.GetLegionCityByID(LegionPoint).LegionNeedLevel;
        LegionCity legionCity = TextTranslator.instance.GetLegionCityByID(LegionPoint);

        if (legionCity.ItemID1 > 0)
        {
            SomeOneHarassment.transform.Find("ItemIcon1").gameObject.SetActive(true);
            SomeOneHarassment.transform.Find("ItemIcon1").GetComponent<UISprite>().spriteName = legionCity.ItemID1.ToString();
            SomeOneHarassment.transform.Find("ItemIcon1/LabelNumber").GetComponent<UILabel>().text = "x" + legionCity.ItemNum1.ToString() + "/小时";
        }

        string[] awardSplit = PointAward.Split('!');
        string[] trcSplit = awardSplit[0].Split('$');

        if (trcSplit[0] != "0")
        {
            SomeOneHarassment.transform.Find("ItemIcon2").gameObject.SetActive(true);
            SetItemDetail(int.Parse(trcSplit[0]), SomeOneHarassment.transform.Find("ItemIcon2").GetComponent<UISprite>());
            SomeOneHarassment.transform.Find("ItemIcon2/LabelNumber").GetComponent<UILabel>().text = "x" + trcSplit[1] + "/6小时";
            TextTranslator.instance.ItemDescription(SomeOneHarassment.transform.Find("ItemIcon2").gameObject, int.Parse(trcSplit[0]), 0);
        } 

        if (UIEventListener.Get(SomeOneHarassment.transform.Find("DeclareWarButton").gameObject).onClick == null)
        {
            UIEventListener.Get(SomeOneHarassment.transform.Find("DeclareWarButton").gameObject).onClick += delegate(GameObject go)
            {
                LegionInfoWindow.SetActive(false);
                //if (NeedLevel <= CharacterRecorder.instance.myLegionData.legionLevel)
                //{
                //    if (ActionPoint > 0)
                //    {
                //        NetworkHandler.instance.SendProcess("8612#" + CharacterRecorder.instance.LegionHarasPoint + ";");//发起骚扰
                //    }
                //    else
                //    {
                //        UIManager.instance.OpenPromptWindow("行动点数不足", PromptWindow.PromptType.Hint, null, null);
                //    }
                //}
                //else
                //{
                //    UIManager.instance.OpenPromptWindow("军团等级不足", PromptWindow.PromptType.Hint, null, null);
                //}

            };
        }

        if (UIEventListener.Get(SomeOneHarassment.transform.Find("LockFightButton").gameObject).onClick == null)
        {
            UIEventListener.Get(SomeOneHarassment.transform.Find("LockFightButton").gameObject).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPanel("LegionDeclareWarWindow", true);
            };
        }
    }

    void MyHarassmentPart(int LegionPoint, int LegionID, string LegionName, int LegionFlag, string ItemStr, int DeclareDate, int Reinforcement, int IsSoldier, int IsAvoidWar, string DeclareLegionName, int DeclareHaveNum, int LegionGrade, string PointAward) 
    {
        ClickPointInfoWindow.SetActive(false);
        ClickLittlePointWindow.SetActive(false);
        Debug.LogError("骚扰,据点非我");
        LegionInfoWindow.SetActive(true);
        NoOneOccupation.SetActive(false);
        SomeOneOccupation.SetActive(false);
        LockInfoOccupation.SetActive(false);
        NoOneHarassment.SetActive(false);
        SomeOneHarassment.SetActive(false);
        MyHarassment.SetActive(true);
        ZhuHarassment.SetActive(false);

        MyHarassment.transform.Find("ItemIcon1").gameObject.SetActive(false);
        MyHarassment.transform.Find("ItemIcon2").gameObject.SetActive(false);
        MyHarassment.transform.Find("GuardNumber").GetComponent<UILabel>().text = Reinforcement.ToString();//守卫数量

        MyHarassment.transform.Find("ItemIcon1").gameObject.SetActive(false);
        int cityType = TextTranslator.instance.GetLegionCityByID(LegionPoint).CityType;
        int NeedLevel = TextTranslator.instance.GetLegionCityByID(LegionPoint).LegionNeedLevel;
        LegionCity legionCity = TextTranslator.instance.GetLegionCityByID(LegionPoint);

        if (legionCity.ItemID1 > 0)
        {
            MyHarassment.transform.Find("ItemIcon1").gameObject.SetActive(true);
            MyHarassment.transform.Find("ItemIcon1").GetComponent<UISprite>().spriteName = legionCity.ItemID1.ToString();
            MyHarassment.transform.Find("ItemIcon1/LabelNumber").GetComponent<UILabel>().text = "x" + legionCity.ItemNum1.ToString() + "/小时";
        }


        string[] awardSplit = PointAward.Split('!');
        string[] trcSplit = awardSplit[0].Split('$');

        if (trcSplit[0] != "0")
        {
            MyHarassment.transform.Find("ItemIcon2").gameObject.SetActive(true);
            SetItemDetail(int.Parse(trcSplit[0]), MyHarassment.transform.Find("ItemIcon2").GetComponent<UISprite>());
            MyHarassment.transform.Find("ItemIcon2/LabelNumber").GetComponent<UILabel>().text = "x" + trcSplit[1] + "/6小时";
            TextTranslator.instance.ItemDescription(MyHarassment.transform.Find("ItemIcon2").gameObject, int.Parse(trcSplit[0]), 0);
        } 
        if (UIEventListener.Get(MyHarassment.transform.Find("SureButton").gameObject).onClick == null)
        {
            UIEventListener.Get(MyHarassment.transform.Find("SureButton").gameObject).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("8629#" + CharacterRecorder.instance.LegionHarasPoint + ";");
                LegionInfoWindow.SetActive(false);
            };
        }
        if (UIEventListener.Get(MyHarassment.transform.Find("CancleButton").gameObject).onClick == null)
        {
            UIEventListener.Get(MyHarassment.transform.Find("CancleButton").gameObject).onClick += delegate(GameObject go)
            {
                LegionInfoWindow.SetActive(false);
            };
        }
    }

    void ZhuHarassmentPart(int LegionPoint, int LegionID, string LegionName, int LegionFlag, string ItemStr, int DeclareDate, int Reinforcement, int IsSoldier, int IsAvoidWar, string DeclareLegionName, int DeclareHaveNum, int LegionGrade, string PointAward) 
    {
        ClickPointInfoWindow.SetActive(false);
        ClickLittlePointWindow.SetActive(false);
        Debug.LogError("驻守");
        LegionInfoWindow.SetActive(true);
        NoOneOccupation.SetActive(false);
        SomeOneOccupation.SetActive(false);
        LockInfoOccupation.SetActive(false);
        NoOneHarassment.SetActive(false);
        SomeOneHarassment.SetActive(false);
        MyHarassment.SetActive(false);
        ZhuHarassment.SetActive(true);

        ZhuHarassment.transform.Find("LegionName").GetComponent<UILabel>().text = LegionName;//军团名字
        ZhuHarassment.transform.Find("GuardNumber").GetComponent<UILabel>().text = Reinforcement.ToString();//守卫数量

        ZhuHarassment.transform.Find("ItemIcon1").gameObject.SetActive(false);
        int cityType = TextTranslator.instance.GetLegionCityByID(LegionPoint).CityType;
        int NeedLevel = TextTranslator.instance.GetLegionCityByID(LegionPoint).LegionNeedLevel;
        LegionCity legionCity = TextTranslator.instance.GetLegionCityByID(LegionPoint);

        if (legionCity.ItemID1 > 0)
        {
            ZhuHarassment.transform.Find("ItemIcon1").gameObject.SetActive(true);
            ZhuHarassment.transform.Find("ItemIcon1").GetComponent<UISprite>().spriteName = legionCity.ItemID1.ToString();
            ZhuHarassment.transform.Find("ItemIcon1/LabelNumber").GetComponent<UILabel>().text = "x" + legionCity.ItemNum1.ToString() + "/小时";
        }

        if (UIEventListener.Get(ZhuHarassment.transform.Find("SureButton").gameObject).onClick == null)
        {
            UIEventListener.Get(ZhuHarassment.transform.Find("SureButton").gameObject).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("8617#;");
                LegionInfoWindow.SetActive(false);
            };
        }
        //if (UIEventListener.Get(MyHarassment.transform.Find("CancleButton").gameObject).onClick == null)
        //{
        //    UIEventListener.Get(MyHarassment.transform.Find("CancleButton").gameObject).onClick += delegate(GameObject go)
        //    {
        //        LegionInfoWindow.SetActive(false);
        //    };
        //}
    }





    /// <summary>
    /// 宣战结果弹出窗口
    /// </summary>
    public void LegionWarStartWar(bool IsWin, int GetId, int IsGongOrShou) //IsGongOrShou   0-守方，代表是占领成功,1-攻方，代表宣战成功
    {
        LegionInfoWindow.SetActive(false);
        GainResultPart.SetActive(true);

        UIEventListener.Get(GainResultPart.transform.Find("KnowButton").gameObject).onClick = delegate(GameObject go)
        {
            GainResultPart.SetActive(false);
            while (GainResultPart.transform.Find("effectcg") != null)
            {
                DestroyImmediate(GainResultPart.transform.Find("effectcg").gameObject);
            }
            while (GainResultPart.transform.Find("effectsb") != null)
            {
                DestroyImmediate(GainResultPart.transform.Find("effectsb").gameObject);
            }
            while (GainResultPart.transform.Find("effectguang") != null)
            {
                DestroyImmediate(GainResultPart.transform.Find("effectguang").gameObject);
            }
            NetworkHandler.instance.SendProcess("8601#;");
        };

        if (IsWin && IsGongOrShou == 0)
        {
            GameObject effectcg = Instantiate(EffectCG) as GameObject;
            effectcg.name = "effectcg";
            effectcg.transform.parent = GainResultPart.transform;
            effectcg.transform.localPosition = new Vector3(0, 260, 15);
            effectcg.transform.localScale = new Vector3(300, 300, 300);
            effectcg.SetActive(true);

            GameObject effectguang = Instantiate(EffectGuang) as GameObject;
            effectguang.name = "effectguang";
            effectguang.transform.parent = GainResultPart.transform;
            effectguang.transform.localPosition = new Vector3(0, 370, 0);
            effectguang.transform.localScale = new Vector3(400, 400, 400);
            effectguang.SetActive(true);

            TweenScale TweenBg = GainResultPart.transform.Find("SpriteBg").GetComponent<TweenScale>();
            TweenBg.from = new Vector3(1f, 0, 1);
            TweenBg.to = new Vector3(1f, 1f, 1);
            TweenBg.ResetToBeginning();
            TweenBg.PlayForward();

            if (ArrLegionPoint[GetId - 1].transform.Find("Label").gameObject.activeSelf == true)
            {
                GainResultPart.transform.Find("Message").GetComponent<UILabel>().text = "您的军团成功击败了[ff9900]" + ArrLegionPoint[GetId - 1].transform.Find("Label").GetComponent<UILabel>().text + "[-],[00e900]成功占领该据点[-]";
            }
            else
            {
                GainResultPart.transform.Find("Message").GetComponent<UILabel>().text = "成功占领[00e900]" + TextTranslator.instance.GetLegionCityByID(GetId).CityName+"[-]";
            }
        }
        else if (IsWin && IsGongOrShou == 1)
        {
            UIManager.instance.OpenPromptWindow("宣战成功", PromptWindow.PromptType.Hint, null, null);
        }
        else
        {
            UIManager.instance.OpenPromptWindow("宣战失败", PromptWindow.PromptType.Hint, null, null);
        }
    }



    /// <summary>
    /// 累计收益响应事件
    /// </summary>
    public void GetRewardList(string Recving)//收益
    {
        string[] dataSplit = Recving.Split(';');
        if (dataSplit[0] != "") 
        {
            AwardButton.transform.Find("RedPoint").gameObject.SetActive(true);
        }

        if (dataSplit[0] != "" && IsOnceInWindow == false)
        {
            #region  现在改成不要预览界面
            //LookAwardPart.SetActive(true);
            //UIEventListener.Get(LookAwardPart.transform.Find("Mask").gameObject).onClick = delegate(GameObject go)
            //{
            //    LookAwardPart.SetActive(false);
            //};
            //UIEventListener.Get(LookAwardPart.transform.Find("SpriteBg/GetButton").gameObject).onClick = delegate(GameObject go)
            //{
            //    NetworkHandler.instance.SendProcess("8610#;");
            //    LookAwardPart.SetActive(false);
            //};
            //string[] trcSplit = dataSplit[0].Split('!');
            //GameObject GridAward = LookAwardPart.transform.Find("SpriteBg/SpriteAward/GridAward").gameObject;

            //for (int i = GridAward.transform.childCount - 1; i >= 0; i--)
            //{
            //    DestroyImmediate(GridAward.transform.GetChild(i).gameObject);
            //}
            //for (int i = 0; i < trcSplit.Length - 1; i++)
            //{
            //    string[] prcSplit = trcSplit[i].Split('$');
            //    GameObject go = NGUITools.AddChild(GridAward, AwardItem);
            //    go.SetActive(true);
            //    SetItemColor(go, int.Parse(prcSplit[0]), int.Parse(prcSplit[1]));
            //}
            //GridAward.GetComponent<UIGrid>().Reposition();
            #endregion
            NetworkHandler.instance.SendProcess("8610#;");
            //AwardButton.transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else if (dataSplit[0] == "")
        {
            //AwardButton.GetComponent<UISprite>().spriteName = "buttonHui";
            //AwardButton.GetComponent<UIButton>().normalSprite = "buttonHui";
            //AwardButton.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(103 / 255f, 96 / 255f, 96 / 255f, 255 / 255f);
            AwardButton.transform.Find("RedPoint").gameObject.SetActive(false);
            UIEventListener.Get(AwardButton).onClick = delegate(GameObject go)
            {
                UIManager.instance.OpenPromptWindow("暂时没有收益，快去抢夺资源城", PromptWindow.PromptType.Hint, null, null);
            };
        }
        TimeNumber = AwardButton.transform.Find("TimeLabel").GetComponent<UILabel>();
        TimeNumber.gameObject.SetActive(true);
        timeNum = int.Parse(dataSplit[1]);
        CancelInvoke("UpdateTimeRight");
        InvokeRepeating("UpdateTimeRight", 0, 1.0f);
    }

    void UpdateTimeRight()
    {
        if (timeNum > 0)
        {
            string houreStr = (timeNum / 3600).ToString("00");
            string miniteStr = (timeNum % 3600 / 60).ToString("00");
            string secondStr = (timeNum % 60).ToString("00");
            TimeNumber.text = string.Format("{0}:{1}:{2}", houreStr, miniteStr, secondStr);
            timeNum -= 1;
        }
        else if (timeNum == 0)
        {
            TimeNumber.gameObject.SetActive(false);
            CancelInvoke("UpdateTimeRight");
        }
    }


    /// <summary>
    /// Item显示
    /// </summary>
    private void SetItemColor(GameObject go, int ItemCode, int ItemNum)
    {
        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
        go.GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo.itemGrade.ToString();

        UISprite spriteIcon = go.transform.Find("Icon").GetComponent<UISprite>();
        GameObject suiPian = go.transform.Find("Fragment").gameObject;
        if (ItemCode.ToString()[0] == '4')
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            suiPian.SetActive(false);
        }
        else if (ItemCode.ToString()[0] == '6')
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            suiPian.SetActive(false);
        }
        else if (ItemCode == 70000)
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            suiPian.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '7' && ItemCode > 70000)
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = (ItemCode - 10000).ToString();
            suiPian.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '8')
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
            //spriteIcon.spriteName = (ItemCode - 30000).ToString();
            spriteIcon.spriteName = mItemInfo.picID.ToString();
            suiPian.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '2')
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
            spriteIcon.spriteName = mItemInfo.picID.ToString();
            suiPian.SetActive(false);
        }
        else
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            suiPian.SetActive(false);
        }

        go.transform.Find("Label").GetComponent<UILabel>().text = ItemNum.ToString();
    }


    /// <summary>
    /// 军功兑换窗口 8621协议用
    /// </summary>
    public void LegionWarGetJunGong(string Recving)
    {
        string[] dataSplit = Recving.Split(';');
        string[] trcSplit1 = dataSplit[0].Split('$');
        string[] trcSplit2 = dataSplit[1].Split('$');
        string[] trcSplit3 = dataSplit[2].Split('$');

        if (ExchangePart.activeSelf == false)
        {
            ExchangePart.SetActive(true);
        }

        UILabel TopNumber = ExchangePart.transform.Find("Top").transform.Find("LabelNumber").GetComponent<UILabel>();
        GameObject item1 = ExchangePart.transform.Find("SpriteBg/Item1").gameObject;
        GameObject item2 = ExchangePart.transform.Find("SpriteBg/Item2").gameObject;
        GameObject item3 = ExchangePart.transform.Find("SpriteBg/Item3").gameObject;


        TopNumber.text = "x" + JunGongPoint.ToString();

        item1.transform.Find("TopName").GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(int.Parse(trcSplit1[1]));
        item1.transform.Find("ItemSprite").GetComponent<UISprite>().spriteName = trcSplit1[1];
        item1.transform.Find("LabelNumber").GetComponent<UILabel>().text = trcSplit1[2];
        item1.transform.Find("LabelNum").GetComponent<UILabel>().text = trcSplit1[3];

        item2.transform.Find("TopName").GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(int.Parse(trcSplit2[1]));
        item2.transform.Find("ItemSprite").GetComponent<UISprite>().spriteName = trcSplit2[1];
        item2.transform.Find("LabelNumber").GetComponent<UILabel>().text = trcSplit2[2];
        item2.transform.Find("LabelNum").GetComponent<UILabel>().text = trcSplit2[3];

        item3.transform.Find("TopName").GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(int.Parse(trcSplit3[1]));
        item3.transform.Find("ItemSprite").GetComponent<UISprite>().spriteName = trcSplit3[1];
        item3.transform.Find("LabelNumber").GetComponent<UILabel>().text = trcSplit3[2];
        item3.transform.Find("LabelNum").GetComponent<UILabel>().text = trcSplit3[3];

        UIEventListener.Get(item1).onClick = delegate(GameObject go)
        {
            if (int.Parse(trcSplit1[2]) > JunGongPoint)
            {
                UIManager.instance.OpenPromptWindow("军功不足", PromptWindow.PromptType.Hint, null, null);
            }
            else if (int.Parse(trcSplit1[3]) == 0)
            {
                UIManager.instance.OpenPromptWindow("兑换次数不足", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                NetworkHandler.instance.SendProcess("8622#" + trcSplit1[0] + ";");
            }
        };

        UIEventListener.Get(item2).onClick = delegate(GameObject go)
        {
            if (int.Parse(trcSplit2[2]) > JunGongPoint)
            {
                UIManager.instance.OpenPromptWindow("军功不足", PromptWindow.PromptType.Hint, null, null);
            }
            else if (int.Parse(trcSplit2[3]) == 0)
            {
                UIManager.instance.OpenPromptWindow("兑换次数不足", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                NetworkHandler.instance.SendProcess("8622#" + trcSplit2[0] + ";");
            }
        };

        UIEventListener.Get(item3).onClick = delegate(GameObject go)
        {
            if (int.Parse(trcSplit3[2]) > JunGongPoint)
            {
                UIManager.instance.OpenPromptWindow("军功不足", PromptWindow.PromptType.Hint, null, null);
            }
            else if (int.Parse(trcSplit3[3]) == 0)
            {
                UIManager.instance.OpenPromptWindow("兑换次数不足", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                NetworkHandler.instance.SendProcess("8622#" + trcSplit3[0] + ";");
            }
        };

        UIEventListener.Get(ExchangePart.transform.Find("Mask").gameObject).onClick = delegate(GameObject go)
        {
            ExchangePart.SetActive(false);
        };
    }


    /// <summary>
    /// 军功兑换结束重置点数 8621协议用
    /// </summary>
    public void LegionWarGetJunGong(int num) 
    {
        JunGongPoint = num;
        JungongLabel.text = num.ToString();
    }


    //public void SetTeamInfo(int index, int mCharacterID, int mCharacterPosition)
    //{
    //    LegionTeamPosition mPosition = new LegionTeamPosition();
    //    mPosition._CharacterID = mCharacterID;
    //    mPosition._CharacterPosition = mCharacterPosition;
    //    mTeamPosition.Add(mPosition);
    //}


    /// <summary>
    /// 刷新行动力 8612、8614协议用
    /// </summary>
    public void GetActionPoint(int num)
    {
        this.ActionPoint = num;
        CharacterRecorder.instance.LegionActionPoint = ActionPoint;
        XingdongLabel.text = ActionPoint.ToString();
    }


    /// <summary>
    /// 发起守护弹出窗口  8616协议用
    /// </summary>
    public void LegionWarSetDefend(int num) //8616
    {
        LegionInfoWindow.SetActive(false);
        this.ActionPoint = num;
        CharacterRecorder.instance.LegionActionPoint = ActionPoint;
        XingdongLabel.text = ActionPoint.ToString();
        if (EffectBZ.activeSelf)
        {
            EffectBZ.SetActive(false);
        }
        EffectBZ.SetActive(true);
        StartCoroutine(StaySomeTime());
        UIManager.instance.OpenPromptWindow("成功驻守该据点！", PromptWindow.PromptType.Hint, null, null);
    }

    IEnumerator StaySomeTime()
    {
        yield return new WaitForSeconds(2f);
        EffectBZ.SetActive(false);
    }



    /// <summary>
    /// 杀敌排行榜首名
    /// </summary>
    public void ShowFirstKillRank(string Recving) 
    {
        string[] dataSplit = Recving.Split(';');
        if (dataSplit[0] == "")
        {
            KillRankInfo.SetActive(false);
        }
        else
        {
            KillRankInfo.SetActive(true);
            string[] secSplit = dataSplit[0].Split('$');
            KillRankInfo.transform.Find("Name").GetComponent<UILabel>().text = secSplit[2];
            KillRankInfo.transform.Find("KillNum").GetComponent<UILabel>().text = secSplit[6];
        }
    }



    /// <summary>
    /// 集火标记   8625协议用
    /// </summary>
    public void LegionWarMarktalarm(string Recving)
    {
        string[] dataSplit=Recving.Split(';');
        string[] trcSplit = dataSplit[0].Split('$');

        GameObject CityObj = ArrLegionPoint[int.Parse(trcSplit[0]) - 1];

        Debug.Log("成功标记");

        if (trcSplit[5] == "1")
        {
            if (trcSplit[2] == CharacterRecorder.instance.myLegionData.legionName) //防旗子
            {
                UIManager.instance.OpenPromptWindow("成功号召军团成员防御该城市!", PromptWindow.PromptType.Hint, null, null);
                //CityObj.transform.Find("ChangeIcon").gameObject.SetActive(true);
                //CityObj.transform.Find("ChangeIcon").GetComponent<UISprite>().spriteName = "Fang_000";
                //CityObj.transform.Find("ChangeIcon").GetComponent<UISpriteAnimation>().namePrefix = "Fang_00";
                CityObj.transform.Find("JihuoEffect").gameObject.SetActive(true);
            }
            else if (trcSplit[2] != CharacterRecorder.instance.myLegionData.legionName)
            {
                UIManager.instance.OpenPromptWindow("成功号召军团成员进攻该城市!", PromptWindow.PromptType.Hint, null, null);
                //CityObj.transform.Find("ChangeIcon").gameObject.SetActive(true);
                //CityObj.transform.Find("ChangeIcon").GetComponent<UISprite>().spriteName = "Gong_000";
                //CityObj.transform.Find("ChangeIcon").GetComponent<UISpriteAnimation>().namePrefix = "Gong_00";
                CityObj.transform.Find("JihuoEffect").gameObject.SetActive(true);
            }
        }
        else 
        {
            //CityObj.transform.Find("ChangeIcon").gameObject.SetActive(false);
            if (trcSplit[2] == CharacterRecorder.instance.myLegionData.legionName) //防旗子
            {
                UIManager.instance.OpenPromptWindow("已取消防守的指令!", PromptWindow.PromptType.Hint, null, null);
            }
            else if (trcSplit[2] != CharacterRecorder.instance.myLegionData.legionName)
            {
                UIManager.instance.OpenPromptWindow("已取消攻击的指令!", PromptWindow.PromptType.Hint, null, null);
            }
            CityObj.transform.Find("JihuoEffect").gameObject.SetActive(false);
        }
    }



    /// <summary>
    /// 点击城市点弹出窗口的动画TweenSacle
    /// </summary>
    void SetPointTweenScale() 
    {
        GameObject PointBgMaskObj = ClickPointInfoWindow.transform.Find("BgMask").gameObject;
        PointBgMaskObj.SetActive(false);
        ZhanButton.SetActive(false);
        XuanButton.SetActive(false);
        ChaButton.SetActive(false);
        GongButton.SetActive(false);
        FangButton.SetActive(false);
        GuanButton.SetActive(false);
        JiButton.SetActive(false);


        TweenScale PointBgMask = ClickPointInfoWindow.transform.Find("BgMask").GetComponent<TweenScale>();
        //TweenScale PointScale = ClickPointInfoWindow.GetComponent<TweenScale>();
        TweenScale PointScaleXuan = XuanButton.GetComponent<TweenScale>();
        TweenScale PointScaleZhan = ZhanButton.GetComponent<TweenScale>();
        TweenScale PointScaleCha = ChaButton.GetComponent<TweenScale>();
        TweenScale PointScaleGong = GongButton.GetComponent<TweenScale>();
        TweenScale PointScaleFang = FangButton.GetComponent<TweenScale>();
        TweenScale PointScaleGuan = GuanButton.GetComponent<TweenScale>();
        TweenScale PointScaleJi = JiButton.GetComponent<TweenScale>();


        PointBgMaskObj.SetActive(true);
        PointBgMask.ResetToBeginning();
        //PointScale.ResetToBeginning();
        PointScaleXuan.ResetToBeginning();
        PointScaleZhan.ResetToBeginning();
        PointScaleCha.ResetToBeginning();
        PointScaleGong.ResetToBeginning();
        PointScaleFang.ResetToBeginning();
        PointScaleGuan.ResetToBeginning();
        PointScaleJi.ResetToBeginning();

        PointBgMask.PlayForward();
        //PointScale.PlayForward();
        PointScaleXuan.PlayForward();
        PointScaleZhan.PlayForward();
        PointScaleCha.PlayForward();
        PointScaleGong.PlayForward();
        PointScaleFang.PlayForward();
        PointScaleGuan.PlayForward();
        PointScaleJi.PlayForward();
    }


    void SetLittleTweenScale() 
    {
        GameObject PointBgMaskObj = ClickLittlePointWindow.transform.Find("BgMask").gameObject;

        GameObject ZhanIcon = ClickLittlePointWindow.transform.Find("ZhanIcon").gameObject;
        GameObject XuanIcon = ClickLittlePointWindow.transform.Find("XuanIcon").gameObject;
        GameObject ChaIcon = ClickLittlePointWindow.transform.Find("ChaIcon").gameObject;
        GameObject JiIcon = ClickLittlePointWindow.transform.Find("JiIcon").gameObject;
        GameObject QiIcon = ClickLittlePointWindow.transform.Find("QiIcon").gameObject;

        PointBgMaskObj.SetActive(false);
        ZhanIcon.SetActive(false);
        XuanIcon.SetActive(false);
        ChaIcon.SetActive(false);
        JiIcon.SetActive(false);
        QiIcon.SetActive(false);

        TweenScale PointBgMask = ClickLittlePointWindow.transform.Find("BgMask").GetComponent<TweenScale>();
        TweenScale ScaleZhanIcon = ZhanIcon.GetComponent<TweenScale>();
        TweenScale ScaleXuanIcon = XuanIcon.GetComponent<TweenScale>();
        TweenScale ScaleChaIcon = ChaIcon.GetComponent<TweenScale>();
        TweenScale ScaleJiIcon = JiIcon.GetComponent<TweenScale>();
        TweenScale ScaleQiIcon = QiIcon.GetComponent<TweenScale>();

        TweenScale Scalehero1 = TeamHero1.GetComponent<TweenScale>();
        TweenScale Scalehero2 = TeamHero2.GetComponent<TweenScale>();
        TweenScale Scalehero3 = TeamHero3.GetComponent<TweenScale>();

        PointBgMaskObj.SetActive(true);


        PointBgMask.ResetToBeginning();
        Scalehero1.ResetToBeginning();
        Scalehero2.ResetToBeginning();
        Scalehero3.ResetToBeginning();
        ScaleZhanIcon.ResetToBeginning();
        ScaleXuanIcon.ResetToBeginning();
        ScaleChaIcon.ResetToBeginning();
        ScaleJiIcon.ResetToBeginning();
        ScaleQiIcon.ResetToBeginning();


        PointBgMask.PlayForward();
        Scalehero1.PlayForward();
        Scalehero2.PlayForward();
        Scalehero3.PlayForward();
        ScaleZhanIcon.PlayForward();
        ScaleXuanIcon.PlayForward();
        ScaleChaIcon.PlayForward();
        ScaleJiIcon.PlayForward();
        ScaleQiIcon.PlayForward();

    }



    /// <summary>
    /// 初始化编队信息
    /// </summary>
    public void LegionWarGetteam() 
    {
        MaIn1 = CharacterRecorder.instance.MarinesInfomation1;
        MaIn2 = CharacterRecorder.instance.MarinesInfomation2;
        MaIn3 = CharacterRecorder.instance.MarinesInfomation3;

        if (CharacterRecorder.instance.MarinesInfomation1.CityId != 0) 
        {
            if (CharacterRecorder.instance.MarinesInfomation1.TeamHero == null) 
            {
                string[] trcSplit = CharacterRecorder.instance.MarinesInfomation1.BloodNumber.Split('!');
                string[] CaptainStr = trcSplit[5].Split('$');
                Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(int.Parse(CaptainStr[0]));
                CharacterRecorder.instance.MarinesInfomation1.AddTeamhero(SetHeroTeamPosition(mhero.cardID, CharacterRecorder.instance.MarinesInfomation1.CityId));
            }
        }

        if (CharacterRecorder.instance.MarinesInfomation2.CityId != 0)
        {
            if (CharacterRecorder.instance.MarinesInfomation2.TeamHero == null)
            {
                string[] trcSplit = CharacterRecorder.instance.MarinesInfomation2.BloodNumber.Split('!');
                string[] CaptainStr = trcSplit[5].Split('$');
                Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(int.Parse(CaptainStr[0]));
                CharacterRecorder.instance.MarinesInfomation2.AddTeamhero(SetHeroTeamPosition(mhero.cardID, CharacterRecorder.instance.MarinesInfomation2.CityId));
            }
        }

        if (CharacterRecorder.instance.MarinesInfomation3.CityId != 0)
        {
            if (CharacterRecorder.instance.MarinesInfomation3.TeamHero == null)
            {
                string[] trcSplit = CharacterRecorder.instance.MarinesInfomation3.BloodNumber.Split('!');
                string[] CaptainStr = trcSplit[5].Split('$');
                Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(int.Parse(CaptainStr[0]));
                CharacterRecorder.instance.MarinesInfomation3.AddTeamhero(SetHeroTeamPosition(mhero.cardID, CharacterRecorder.instance.MarinesInfomation3.CityId));
            }
        }

        SetTeamItemButtonInfo();
    }


    /// <summary>
    /// 国战队伍左边点击按钮状态
    /// </summary>
    public void SetTeamItemButtonInfo() 
    {
        if (CharacterRecorder.instance.MarinesInfomation1.CityId != 0)
        {
            string[] trcSplit = CharacterRecorder.instance.MarinesInfomation1.BloodNumber.Split('!');
            string[] CaptainStr = trcSplit[5].Split('$');
            //HeroInfo HI=TextTranslator.instance.GetHeroInfoByHeroID(int.Parse(CaptainStr[0]));

            TeamButton1.transform.Find("HeroGrade").gameObject.SetActive(true);
            TeamButton1.transform.Find("TimeLabel").gameObject.SetActive(false);
            TeamButton1.transform.Find("AddSprite").gameObject.SetActive(false);

            Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(int.Parse(CaptainStr[0]));
            TeamButton1.transform.Find("HeroGrade/HeroIcon").GetComponent<UISprite>().spriteName = mhero.cardID.ToString();

            float allbloodnum = 0f;
            float allmaxbloodnum = 0f;

            for (int i = 0; i < trcSplit.Length - 1; i++) 
            {
                string[] oneCaptainStr = trcSplit[i].Split('$');
                allbloodnum += float.Parse(oneCaptainStr[1]);
                allmaxbloodnum += float.Parse(oneCaptainStr[2]);
            }
            float bloodnum = allbloodnum / allmaxbloodnum;
            TeamButton1.transform.Find("HeroGrade/BloodNum").GetComponent<UILabel>().text = (bloodnum * 100).ToString("f2") + "%";
            TeamButton1.transform.Find("HeroGrade/ProgressBar").GetComponent<UISlider>().value = bloodnum;



            if (allbloodnum < allmaxbloodnum)
            {
                TeamButton1.transform.Find("HeroGrade/BloodSprite").gameObject.SetActive(true);
                float num = 103f * (1 - bloodnum);
                TeamButton1.transform.Find("HeroGrade/BloodSprite").GetComponent<UISprite>().height = (int)num;
            }
            else 
            {
                TeamButton1.transform.Find("HeroGrade/BloodSprite").gameObject.SetActive(false);
            }
        }
        else if (CharacterRecorder.instance.MarinesInfomation1.CityId == 0 && CharacterRecorder.instance.MarinesInfomation1.timeNum > 0)
        {
            TeamButton1.transform.Find("HeroGrade").gameObject.SetActive(false);
            TeamButton1.transform.Find("TimeLabel").gameObject.SetActive(true);
            TeamButton1.transform.Find("AddSprite").gameObject.SetActive(false);
        }
        else 
        {
            TeamButton1.transform.Find("HeroGrade").gameObject.SetActive(false);
            TeamButton1.transform.Find("TimeLabel").gameObject.SetActive(false);
            TeamButton1.transform.Find("AddSprite").gameObject.SetActive(true);
        }

        if (CharacterRecorder.instance.MarinesInfomation2.CityId != 0)
        {
            string[] trcSplit = CharacterRecorder.instance.MarinesInfomation2.BloodNumber.Split('!');
            string[] CaptainStr = trcSplit[5].Split('$');

            //HeroInfo HI = TextTranslator.instance.GetHeroInfoByHeroID(int.Parse(CaptainStr[0]));
            TeamButton2.transform.Find("HeroGrade").gameObject.SetActive(true);
            TeamButton2.transform.Find("TimeLabel").gameObject.SetActive(false);
            TeamButton2.transform.Find("AddSprite").gameObject.SetActive(false);

            Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(int.Parse(CaptainStr[0]));
            TeamButton2.transform.Find("HeroGrade/HeroIcon").GetComponent<UISprite>().spriteName = mhero.cardID.ToString();

            float allbloodnum = 0f;
            float allmaxbloodnum = 0f;

            for (int i = 0; i < trcSplit.Length - 1; i++)
            {
                string[] oneCaptainStr = trcSplit[i].Split('$');
                allbloodnum += float.Parse(oneCaptainStr[1]);
                allmaxbloodnum += float.Parse(oneCaptainStr[2]);
            }
            float bloodnum = allbloodnum / allmaxbloodnum;
            TeamButton2.transform.Find("HeroGrade/BloodNum").GetComponent<UILabel>().text = (bloodnum * 100).ToString("f2") + "%";
            TeamButton2.transform.Find("HeroGrade/ProgressBar").GetComponent<UISlider>().value = bloodnum;


            if (allbloodnum < allmaxbloodnum)
            {
                TeamButton2.transform.Find("HeroGrade/BloodSprite").gameObject.SetActive(true);
                float num = 103f * (1 - bloodnum);
                TeamButton2.transform.Find("HeroGrade/BloodSprite").GetComponent<UISprite>().height = (int)num;
            }
            else
            {
                TeamButton2.transform.Find("HeroGrade/BloodSprite").gameObject.SetActive(false);
            }
        }
        else if (CharacterRecorder.instance.MarinesInfomation2.CityId == 0 && CharacterRecorder.instance.MarinesInfomation2.timeNum > 0)
        {
            TeamButton2.transform.Find("HeroGrade").gameObject.SetActive(false);
            TeamButton2.transform.Find("TimeLabel").gameObject.SetActive(true);
            TeamButton2.transform.Find("AddSprite").gameObject.SetActive(false);
        }
        else
        {
            TeamButton2.transform.Find("HeroGrade").gameObject.SetActive(false);
            TeamButton2.transform.Find("TimeLabel").gameObject.SetActive(false);
            TeamButton2.transform.Find("AddSprite").gameObject.SetActive(true);
        }

        if (CharacterRecorder.instance.MarinesInfomation3.CityId != 0)
        {
            string[] trcSplit = CharacterRecorder.instance.MarinesInfomation3.BloodNumber.Split('!');
            string[] CaptainStr = trcSplit[5].Split('$');

            TeamButton3.transform.Find("HeroGrade").gameObject.SetActive(true);
            TeamButton3.transform.Find("TimeLabel").gameObject.SetActive(false);
            TeamButton3.transform.Find("AddSprite").gameObject.SetActive(false);

            Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(int.Parse(CaptainStr[0]));
            TeamButton3.transform.Find("HeroGrade/HeroIcon").GetComponent<UISprite>().spriteName = mhero.cardID.ToString();

            float allbloodnum = 0f;
            float allmaxbloodnum = 0f;

            for (int i = 0; i < trcSplit.Length - 1; i++)
            {
                string[] oneCaptainStr = trcSplit[i].Split('$');
                allbloodnum += float.Parse(oneCaptainStr[1]);
                allmaxbloodnum += float.Parse(oneCaptainStr[2]);
            }
            float bloodnum = allbloodnum / allmaxbloodnum;
            TeamButton3.transform.Find("HeroGrade/BloodNum").GetComponent<UILabel>().text = (bloodnum * 100).ToString("f2") + "%";
            TeamButton3.transform.Find("HeroGrade/ProgressBar").GetComponent<UISlider>().value = bloodnum;


            if (allbloodnum < allmaxbloodnum)
            {
                TeamButton3.transform.Find("HeroGrade/BloodSprite").gameObject.SetActive(true);
                float num = 103f * (1 - bloodnum);
                TeamButton3.transform.Find("HeroGrade/BloodSprite").GetComponent<UISprite>().height = (int)num;
            }
            else
            {
                TeamButton3.transform.Find("HeroGrade/BloodSprite").gameObject.SetActive(false);
            }
        }
        else if (CharacterRecorder.instance.MarinesInfomation3.CityId == 0 && CharacterRecorder.instance.MarinesInfomation3.timeNum > 0)
        {
            TeamButton3.transform.Find("HeroGrade").gameObject.SetActive(false);
            TeamButton3.transform.Find("TimeLabel").gameObject.SetActive(true);
            TeamButton3.transform.Find("AddSprite").gameObject.SetActive(false);
        }
        else
        {
            TeamButton3.transform.Find("HeroGrade").gameObject.SetActive(false);
            TeamButton3.transform.Find("TimeLabel").gameObject.SetActive(false);
            TeamButton3.transform.Find("AddSprite").gameObject.SetActive(true);
        }

        CancelInvoke("UpdateTimeLeft");
        InvokeRepeating("UpdateTimeLeft", 0, 1.0f);
    }


    /// <summary>
    /// 队伍点击人物头像移动编队
    /// </summary>
    private void SaoRaoHeroButton(int LegionPoint) 
    {
        if (CharacterRecorder.instance.MarinesInfomation1 != null) 
        {
            if (CharacterRecorder.instance.MarinesInfomation1.CityId != 0)
            {
                TeamHero1.SetActive(true);
                string[] trcSplit = CharacterRecorder.instance.MarinesInfomation1.BloodNumber.Split('!');
                string[] CaptainStr = trcSplit[5].Split('$');
                Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(int.Parse(CaptainStr[0]));
                TeamHero1.GetComponent<UISprite>().spriteName = mhero.cardID.ToString();
                UIEventListener.Get(TeamHero1).onClick = delegate(GameObject go)
                {
                    Debug.Log("点击队伍1");
                    CharacterRecorder.instance.MarinesTabe = 1;
                    if (CharacterRecorder.instance.MarinesInfomation1.TeamHero != null)
                    {
                        bool CanmoveOnBlood = false;
                        string[] prcSplit = CharacterRecorder.instance.MarinesInfomation1.BloodNumber.Split('!');
                        for (int j = 0; j < prcSplit.Length - 1; j++) 
                        {
                            string[] bloodSplit=prcSplit[j].Split('$');
                            if (int.Parse(bloodSplit[1]) > 0) 
                            {
                                CanmoveOnBlood = true;
                                break;
                            }
                        }

                        if (CanmoveOnBlood)
                        {
                            if (CharacterRecorder.instance.MarinesInfomation1.IsMove) //移动时转变方向
                            {
                                HeroTeamToMove(CharacterRecorder.instance.MarinesInfomation1, LegionPoint);
                            }
                            else if (CharacterRecorder.instance.MarinesInfomation1.IsMove == false && CharacterRecorder.instance.MarinesInfomation2.IsMove == false && CharacterRecorder.instance.MarinesInfomation3.IsMove == false)
                            {
                                if (CharacterRecorder.instance.MarinesInfomation1.NowCityId != LegionPoint) //移动到另一个点
                                {
                                    CharacterRecorder.instance.MarinesInfomation1.IsMove = true;
                                    HeroTeamToMove(CharacterRecorder.instance.MarinesInfomation1, LegionPoint);
                                }
                                else //本点骚扰
                                {
                                    NetworkHandler.instance.SendProcess("8635#" + 1 + ";" + LegionPoint + ";");
                                }
                            }
                            else
                            {
                                Debug.Log("有编队移动中");
                            }
                        }
                        else 
                        {
                            UIManager.instance.OpenPromptWindow("战队成员已阵亡!", PromptWindow.PromptType.Hint, null, null);
                        }
                    }
                };
            }
        }

        if (CharacterRecorder.instance.MarinesInfomation2 != null)
        {
            if (CharacterRecorder.instance.MarinesInfomation2.CityId != 0)
            {
                TeamHero2.SetActive(true);
                string[] trcSplit = CharacterRecorder.instance.MarinesInfomation2.BloodNumber.Split('!');
                string[] CaptainStr = trcSplit[5].Split('$');
                Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(int.Parse(CaptainStr[0]));
                TeamHero2.GetComponent<UISprite>().spriteName = mhero.cardID.ToString();
                UIEventListener.Get(TeamHero2).onClick = delegate(GameObject go)
                {
                    Debug.LogError("点击队伍2");
                    CharacterRecorder.instance.MarinesTabe = 2;
                    bool CanmoveOnBlood = false;
                    string[] prcSplit = CharacterRecorder.instance.MarinesInfomation2.BloodNumber.Split('!');
                    for (int j = 0; j < prcSplit.Length - 1; j++)
                    {
                        string[] bloodSplit = prcSplit[j].Split('$');
                        if (int.Parse(bloodSplit[1]) > 0)
                        {
                            CanmoveOnBlood = true;
                            break;
                        }
                    }

                    if (CanmoveOnBlood)
                    {
                        if (CharacterRecorder.instance.MarinesInfomation2.TeamHero != null)
                        {
                            if (CharacterRecorder.instance.MarinesInfomation2.IsMove) //移动时转变方向
                            {
                                HeroTeamToMove(CharacterRecorder.instance.MarinesInfomation2, LegionPoint);
                            }
                            else if (CharacterRecorder.instance.MarinesInfomation1.IsMove == false && CharacterRecorder.instance.MarinesInfomation2.IsMove == false && CharacterRecorder.instance.MarinesInfomation3.IsMove == false)
                            {
                                if (CharacterRecorder.instance.MarinesInfomation2.NowCityId != LegionPoint) //移动到另一个点
                                {
                                    CharacterRecorder.instance.MarinesInfomation2.IsMove = true;
                                    HeroTeamToMove(CharacterRecorder.instance.MarinesInfomation2, LegionPoint);
                                }
                                else //本点骚扰
                                {
                                    //NetworkHandler.instance.SendProcess("8612#" + LegionPoint + ";");
                                    NetworkHandler.instance.SendProcess("8635#" + 2 + ";" + LegionPoint + ";");
                                }
                            }
                            else
                            {
                                Debug.Log("有编队移动中");
                            }
                        }
                    }
                    else 
                    {
                        UIManager.instance.OpenPromptWindow("战队成员已阵亡!", PromptWindow.PromptType.Hint, null, null);
                    }
                };
            }
        }

        if (CharacterRecorder.instance.MarinesInfomation3 != null)
        {
            if (CharacterRecorder.instance.MarinesInfomation3.CityId != 0)
            {
                TeamHero3.SetActive(true);
                string[] trcSplit = CharacterRecorder.instance.MarinesInfomation3.BloodNumber.Split('!');
                string[] CaptainStr = trcSplit[5].Split('$');
                Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(int.Parse(CaptainStr[0]));
                TeamHero3.GetComponent<UISprite>().spriteName = mhero.cardID.ToString();
                UIEventListener.Get(TeamHero3).onClick = delegate(GameObject go)
                {
                    Debug.LogError("点击队伍3");
                    CharacterRecorder.instance.MarinesTabe = 3;
                    bool CanmoveOnBlood = false;
                    string[] prcSplit = CharacterRecorder.instance.MarinesInfomation3.BloodNumber.Split('!');
                    for (int j = 0; j < prcSplit.Length - 1; j++)
                    {
                        string[] bloodSplit = prcSplit[j].Split('$');
                        if (int.Parse(bloodSplit[1]) > 0)
                        {
                            CanmoveOnBlood = true;
                            break;
                        }
                    }

                    if (CanmoveOnBlood)
                    {
                        if (CharacterRecorder.instance.MarinesInfomation3.TeamHero != null)
                        {
                            if (CharacterRecorder.instance.MarinesInfomation3.IsMove) //移动时转变方向
                            {
                                HeroTeamToMove(CharacterRecorder.instance.MarinesInfomation3, LegionPoint);
                            }
                            else if (CharacterRecorder.instance.MarinesInfomation1.IsMove == false && CharacterRecorder.instance.MarinesInfomation2.IsMove == false && CharacterRecorder.instance.MarinesInfomation3.IsMove == false)
                            {
                                if (CharacterRecorder.instance.MarinesInfomation3.NowCityId != LegionPoint) //移动到另一个点
                                {
                                    CharacterRecorder.instance.MarinesInfomation3.IsMove = true;
                                    HeroTeamToMove(CharacterRecorder.instance.MarinesInfomation3, LegionPoint);
                                }
                                else //本点骚扰
                                {
                                    NetworkHandler.instance.SendProcess("8635#" + 3 + ";" + LegionPoint + ";");
                                }
                            }
                            else
                            {
                                Debug.Log("有编队移动中");
                                //NetworkHandler.instance.SendProcess("8635#" + 3 + ";" + LegionPoint + ";");
                            }
                        }
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("战队成员已阵亡!", PromptWindow.PromptType.Hint, null, null);
                    }
                };
            }
        }
    }


    /// <summary>
    /// 队伍编队刷新倒计时
    /// </summary>
    void UpdateTimeLeft()
    {
        if (MaIn1.timeNum > 0)
        {          
            //string houreStr = (leftTime / 3600).ToString("00");
            string miniteStr = (MaIn1.timeNum % 3600 / 60).ToString("00");
            string secondStr = (MaIn1.timeNum % 60).ToString("00");
            TeamButton1.transform.Find("TimeLabel").GetComponent<UILabel>().text = string.Format("{0}:{1}", miniteStr, secondStr);
            MaIn1.timeNum -= 1;
            if (MaIn1.timeNum == 0) 
            {
                TeamButton1.transform.Find("TimeLabel").gameObject.SetActive(false);
                TeamButton1.transform.Find("AddSprite").gameObject.SetActive(true);
            }
        }

        if (MaIn2.timeNum > 0)
        {
            //string houreStr = (leftTime / 3600).ToString("00");
            string miniteStr = (MaIn2.timeNum % 3600 / 60).ToString("00");
            string secondStr = (MaIn2.timeNum % 60).ToString("00");
            TeamButton2.transform.Find("TimeLabel").GetComponent<UILabel>().text = string.Format("{0}:{1}", miniteStr, secondStr);
            MaIn2.timeNum -= 1;
            if (MaIn2.timeNum == 0)
            {
                TeamButton2.transform.Find("TimeLabel").gameObject.SetActive(false);
                TeamButton2.transform.Find("AddSprite").gameObject.SetActive(true);
            }
        }

        if (MaIn3.timeNum > 0)
        {
            //string houreStr = (leftTime / 3600).ToString("00");
            string miniteStr = (MaIn3.timeNum % 3600 / 60).ToString("00");
            string secondStr = (MaIn3.timeNum % 60).ToString("00");
            TeamButton3.transform.Find("TimeLabel").GetComponent<UILabel>().text = string.Format("{0}:{1}", miniteStr, secondStr);
            MaIn3.timeNum -= 1;
            if (MaIn3.timeNum == 0)
            {
                TeamButton3.transform.Find("TimeLabel").gameObject.SetActive(false);
                TeamButton3.transform.Find("AddSprite").gameObject.SetActive(true);
            }
        }
    }


    /// <summary>
    /// 队伍创建编队3D人物角色,确定初始位置
    /// </summary>
    private GameObject SetHeroTeamPosition(int heroId,int heropositionId) //初始确定队伍位置
    {
        GameObject HeroObj = new GameObject(heroId.ToString());//GameObject.Instantiate(Resources.Load("Prefab/Role/" + heroId) as GameObject) as GameObject;
        HeroObj.transform.parent = PointParent.transform;
        HeroObj.transform.localScale = new Vector3(40f, 40f, 40f);
        HeroObj.transform.localPosition = ArrLegionPoint[heropositionId - 1].transform.localPosition - new Vector3(0, 0, 30f);

        HeroObj.AddComponent<VFXRenderQueueSorter>();
        HeroObj.GetComponent<VFXRenderQueueSorter>().mTarget = ArrLegionPoint[heropositionId - 1].GetComponent<UISprite>();
        HeroObj.AddComponent<BoxCollider>();
        HeroObj.GetComponent<BoxCollider>().center = new Vector3(0, 0.01f, 0);
        HeroObj.GetComponent<BoxCollider>().size = new Vector3(2.25f, 0.01f, 2.25f);

        GameObject Role = GameObject.Instantiate(Resources.Load("Prefab/Role/" + heroId) as GameObject) as GameObject;
        HeroInfo hi = TextTranslator.instance.GetHeroInfoByHeroID(heroId);
        Role.transform.parent = HeroObj.transform;
        Role.transform.localPosition = Vector3.zero;
        Role.transform.localScale = new Vector3(hi.heroScale, hi.heroScale, hi.heroScale);
        Role.transform.Rotate(0, 90, 0);
        Role.name = "Role";

        if (HeroObj.name == "60033") 
        {
            GameObject.Find("naima_qiu").SetActive(false);
        }
        foreach (Transform tran in HeroObj.GetComponentsInChildren<Transform>()) 
        {
            tran.gameObject.layer = 8;
        }
        HeroObj.layer = 8;

        return HeroObj;
    }


    /// <summary>
    /// 队伍刷新移动位置
    /// </summary>
    void FixedUpdate()
    {
        if (MaIn1 != null) 
        {
            if (MaIn1.IsMove) 
            {
                if (MaIn1.TeamHero != null)
                {
                    if (MaIn1.TeamHero.transform.localPosition.x != MaIn1.toPosition.x)
                    {
                        MaIn1.TeamHero.transform.localPosition = Vector3.MoveTowards(MaIn1.TeamHero.transform.localPosition, MaIn1.toPosition, 2.25f);
                        if (MaIn1.TeamHero.transform.localPosition.x == MaIn1.toPosition.x)
                        {
                            MaIn1.TeamHero.transform.Find("Role").GetComponent<Animator>().SetFloat("ft", 2);
                            CharacterRecorder.instance.MarinesInfomation1.IsMove = false;
                            CharacterRecorder.instance.MarinesInfomation2.IsMove = false;
                            CharacterRecorder.instance.MarinesInfomation3.IsMove = false;
                            CharacterRecorder.instance.MarinesInfomation1.NowCityId = MaIn1.ToCityId;
                            NetworkHandler.instance.SendProcess("8635#" + 1 + ";" + MaIn1.ToCityId);
                        }
                    }
                }
            }
        }

        if (MaIn2 != null)
        {
            if (MaIn2.IsMove) 
            {
                if (MaIn2.TeamHero != null)
                {
                    if (MaIn2.TeamHero.transform.localPosition.x != MaIn2.toPosition.x)
                    {
                        MaIn2.TeamHero.transform.localPosition = Vector3.MoveTowards(MaIn2.TeamHero.transform.localPosition, MaIn2.toPosition, 2.25f);
                        if (MaIn2.TeamHero.transform.localPosition.x == MaIn2.toPosition.x)
                        {
                            MaIn2.TeamHero.transform.Find("Role").GetComponent<Animator>().SetFloat("ft", 2);
                            CharacterRecorder.instance.MarinesInfomation1.IsMove = false;
                            CharacterRecorder.instance.MarinesInfomation2.IsMove = false;
                            CharacterRecorder.instance.MarinesInfomation3.IsMove = false;
                            CharacterRecorder.instance.MarinesInfomation2.NowCityId = MaIn2.ToCityId;
                            NetworkHandler.instance.SendProcess("8635#" + 2 + ";" + MaIn2.ToCityId);
                        }
                    }
                }
            }
        }

        if (MaIn3 != null)
        {
            if (MaIn3.IsMove) 
            {
                if (MaIn3.TeamHero != null)
                {
                    if (MaIn3.TeamHero.transform.localPosition.x != MaIn3.toPosition.x)
                    {
                        MaIn3.TeamHero.transform.localPosition = Vector3.MoveTowards(MaIn3.TeamHero.transform.localPosition, MaIn3.toPosition, 2.25f);
                        if (MaIn3.TeamHero.transform.localPosition.x == MaIn3.toPosition.x)
                        {
                            MaIn3.TeamHero.transform.Find("Role").GetComponent<Animator>().SetFloat("ft", 2);
                            CharacterRecorder.instance.MarinesInfomation1.IsMove = false;
                            CharacterRecorder.instance.MarinesInfomation2.IsMove = false;
                            CharacterRecorder.instance.MarinesInfomation3.IsMove = false;
                            CharacterRecorder.instance.MarinesInfomation3.NowCityId = MaIn3.ToCityId;
                            NetworkHandler.instance.SendProcess("8635#" + 3 + ";" + MaIn3.ToCityId);
                        }
                    }
                }
            }
        }
        #region  旧的
        //if (MaIn1 != null)
        //{
        //    if (MaIn1.TeamHero != null)
        //    {
        //        if (MaIn1.numx > 0)
        //        {
        //            if (MaIn1.TeamHero.transform.localPosition.x < MaIn1.toPosition.x)
        //            {
        //                MaIn1.TeamHero.transform.localPosition = new Vector3(MaIn1.TeamHero.transform.localPosition.x + MaIn1.stepNumx, MaIn1.TeamHero.transform.localPosition.y + MaIn1.stepNumy, 0);

        //                if (MaIn1.TeamHero.transform.localPosition.x >= MaIn1.toPosition.x)
        //                {
        //                    MaIn1.TeamHero.transform.localPosition = MaIn1.toPosition;

        //                    MaIn1.TeamHero.transform.Find("Role").GetComponent<Animator>().SetFloat("ft", 2);
        //                }
        //            }
        //        }
        //        else if (MaIn1.numx < 0)
        //        {
        //            if (MaIn1.TeamHero.transform.localPosition.x > MaIn1.toPosition.x)
        //            {
        //                MaIn1.TeamHero.transform.localPosition = new Vector3(MaIn1.TeamHero.transform.localPosition.x + MaIn1.stepNumx, MaIn1.TeamHero.transform.localPosition.y + MaIn1.stepNumy, 0);

        //                if (MaIn1.TeamHero.transform.localPosition.x <= MaIn1.toPosition.x)
        //                {
        //                    MaIn1.TeamHero.transform.localPosition = MaIn1.toPosition;

        //                    MaIn1.TeamHero.transform.Find("Role").GetComponent<Animator>().SetFloat("ft", 2);
        //                }
        //            }
        //        }
        //    }
        //}
        //if (MaIn2 != null) 
        //{
        //    if (MaIn2.TeamHero != null)
        //    {
        //        if (MaIn2.numx > 0)
        //        {
        //            if (MaIn2.TeamHero.transform.localPosition.x < MaIn2.toPosition.x)
        //            {
        //                MaIn2.TeamHero.transform.localPosition = new Vector3(MaIn2.TeamHero.transform.localPosition.x + MaIn2.stepNumx, MaIn2.TeamHero.transform.localPosition.y + MaIn2.stepNumy, 0);

        //                if (MaIn2.TeamHero.transform.localPosition.x >= MaIn2.toPosition.x)
        //                {
        //                    MaIn2.TeamHero.transform.localPosition = MaIn2.toPosition;

        //                    MaIn2.TeamHero.transform.Find("Role").GetComponent<Animator>().SetFloat("ft", 2);
        //                }
        //            }
        //        }
        //        else if (MaIn2.numx < 0)
        //        {
        //            if (MaIn2.TeamHero.transform.localPosition.x > MaIn2.toPosition.x)
        //            {
        //                MaIn2.TeamHero.transform.localPosition = new Vector3(MaIn2.TeamHero.transform.localPosition.x + MaIn2.stepNumx, MaIn2.TeamHero.transform.localPosition.y + MaIn2.stepNumy, 0);

        //                if (MaIn2.TeamHero.transform.localPosition.x <= MaIn2.toPosition.x)
        //                {
        //                    MaIn2.TeamHero.transform.localPosition = MaIn2.toPosition;

        //                    MaIn2.TeamHero.transform.Find("Role").GetComponent<Animator>().SetFloat("ft", 2);
        //                }
        //            }
        //        }
        //    }
        //}

        //if (MaIn3 != null) 
        //{
        //    if (MaIn3.TeamHero != null)
        //    {
        //        if (MaIn3.numx > 0)
        //        {
        //            if (MaIn3.TeamHero.transform.localPosition.x < MaIn3.toPosition.x)
        //            {
        //                MaIn3.TeamHero.transform.localPosition = new Vector3(MaIn3.TeamHero.transform.localPosition.x + MaIn3.stepNumx, MaIn3.TeamHero.transform.localPosition.y + MaIn3.stepNumy, 0);

        //                if (MaIn3.TeamHero.transform.localPosition.x >= MaIn3.toPosition.x)
        //                {
        //                    MaIn3.TeamHero.transform.localPosition = MaIn3.toPosition;

        //                    MaIn3.TeamHero.transform.Find("Role").GetComponent<Animator>().SetFloat("ft", 2);
        //                }
        //            }
        //        }
        //        else if (MaIn3.numx < 0)
        //        {
        //            if (MaIn3.TeamHero.transform.localPosition.x > MaIn3.toPosition.x)
        //            {
        //                MaIn3.TeamHero.transform.localPosition = new Vector3(MaIn3.TeamHero.transform.localPosition.x + MaIn3.stepNumx, MaIn3.TeamHero.transform.localPosition.y + MaIn3.stepNumy, 0);

        //                if (MaIn3.TeamHero.transform.localPosition.x <= MaIn3.toPosition.x)
        //                {
        //                    MaIn3.TeamHero.transform.localPosition = MaIn3.toPosition;

        //                    MaIn3.TeamHero.transform.Find("Role").GetComponent<Animator>().SetFloat("ft", 2);
        //                }
        //            }
        //        }
        //    }
        //}

        #endregion 
    }


    /// <summary>
    /// 队伍确定单一3D人物移动的目标点
    /// </summary>
    public void HeroTeamToMove(MarinesInfomation MaIn, int LegionPoint)
    {
        Vector3 nowPosition = MaIn.TeamHero.transform.localPosition;
        Vector3 toPosition = ArrLegionPoint[LegionPoint - 1].transform.localPosition - new Vector3(0, 0, 30f);

        float numx = toPosition.x - nowPosition.x;
        //float numy = toPosition.y - nowPosition.y;

        //float k = numy / numx;

        if (numx > 0) 
        {
            //float stepNumx = 2.25f;
            //float stepNumy = stepNumx * k;

            MaIn.SetHeroPosition(toPosition);
            MaIn.ToCityId = LegionPoint;
            MaIn.TeamHero.transform.Find("Role").transform.localRotation = new Quaternion(0, 0, 0, 0);
            MaIn.TeamHero.transform.Find("Role").transform.Rotate(new Vector3(0, 90f, 0));
            MaIn.TeamHero.transform.Find("Role").GetComponent<Animator>().SetFloat("ft", 0);
        }
        else if (numx < 0) 
        {
            //float stepNumx = -2.25f;
            //float stepNumy = stepNumx * k;

            MaIn.SetHeroPosition(toPosition);
            MaIn.ToCityId = LegionPoint;
            MaIn.TeamHero.transform.Find("Role").transform.localRotation = new Quaternion(0, 0, 0, 0);
            MaIn.TeamHero.transform.Find("Role").transform.Rotate(new Vector3(0, -90f, 0));
            MaIn.TeamHero.transform.Find("Role").GetComponent<Animator>().SetFloat("ft", 0);
        }
        else if (numx == 0) 
        {
            CharacterRecorder.instance.MarinesInfomation1.IsMove = false;
            CharacterRecorder.instance.MarinesInfomation2.IsMove = false;
            CharacterRecorder.instance.MarinesInfomation3.IsMove = false;
        }
        MaIn.TeamHero.GetComponent<VFXRenderQueueSorter>().mTarget = ArrLegionPoint[LegionPoint - 1].GetComponent<UISprite>();
    }


    /// <summary>
    /// 队伍定位某一编队视角
    /// </summary>
    public void GotoCityNum(int citynum)
    {
        Vector3 cityPosition = ArrLegionPoint[citynum - 1].transform.localPosition;
        Vector3 vec = -(cityPosition - new Vector3(-86f, 21f, 0));
        transform.Find("ObjParent").GetComponent<LegionWarCamera>().GotoCityNum(vec);
    }


    /// <summary>
    /// 奖励红点显示
    /// </summary>
    public void JiangliButtonRedPoint() 
    {
        string Receive = CharacterRecorder.instance.MilitaryExploitInfo;
        string[] dataSplit = Receive.Split(';');
        string[] RewardSplit = dataSplit[1].Split('$');
        bool CanGetReward = false;
        for (int i = 0; i < RewardSplit.Length - 1; i++) 
        {
            if (RewardSplit[i] == "1") 
            {
                CanGetReward = true;
                break;
            }
        }

        if (CanGetReward)
        {
            JiangliButton.transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else 
        {
            JiangliButton.transform.Find("RedPoint").gameObject.SetActive(false);
        }
    }



    /// <summary>
    /// 底部首次进入军团战界面弹出动画
    /// </summary>
    private void FirstEnterPlayAnimation() 
    {
        if (PlayerPrefs.GetInt(PlayerPrefs.GetString("ServerID") + CharacterRecorder.instance.userId.ToString() + "FirstLegionWar") == 0)
        {
            StartCoroutine(FirstEnterPlayAnimationIE());
            PlayerPrefs.SetInt(PlayerPrefs.GetString("ServerID") + CharacterRecorder.instance.userId.ToString() + "FirstLegionWar", 1);
        }
        else 
        {
            PlayerPrefs.SetInt(PlayerPrefs.GetString("ServerID") + CharacterRecorder.instance.userId.ToString() + "FirstLegionWar",1);
        }
    }

    IEnumerator FirstEnterPlayAnimationIE() 
    {
        ButtonParent.SetActive(false);
        //TweenAlpha FirstEnterSprTw = FirstEnterParent.transform.Find("FirstEnterSpr").GetComponent<TweenAlpha>();
        TweenAlpha HeroTexTw = FirstEnterParent.transform.Find("HeroTex").GetComponent<TweenAlpha>();
        TweenAlpha ZhanlinSprTw = FirstEnterParent.transform.Find("ZhanlinSpr").GetComponent<TweenAlpha>();
        TweenAlpha CityDTw = FirstEnterParent.transform.Find("CityD").GetComponent<TweenAlpha>();
        TweenAlpha CityCTw = FirstEnterParent.transform.Find("CityC").GetComponent<TweenAlpha>();
        TweenAlpha CityBTw = FirstEnterParent.transform.Find("CityB").GetComponent<TweenAlpha>();
        TweenAlpha CityATw = FirstEnterParent.transform.Find("CityA").GetComponent<TweenAlpha>();
        TweenAlpha CitySTw = FirstEnterParent.transform.Find("CityS").GetComponent<TweenAlpha>();
        TweenAlpha JiantouSpr1Tw = FirstEnterParent.transform.Find("JiantouSpr1").GetComponent<TweenAlpha>();
        TweenAlpha JiantouSpr2Tw = FirstEnterParent.transform.Find("JiantouSpr2").GetComponent<TweenAlpha>();
        TweenAlpha JiantouSpr3Tw = FirstEnterParent.transform.Find("JiantouSpr3").GetComponent<TweenAlpha>();
        TweenAlpha JiantouSpr4Tw = FirstEnterParent.transform.Find("JiantouSpr4").GetComponent<TweenAlpha>();

        HeroTexTw.ResetToBeginning();
        ZhanlinSprTw.ResetToBeginning();
        CityDTw.ResetToBeginning();
        CityCTw.ResetToBeginning();
        CityBTw.ResetToBeginning();
        CityATw.ResetToBeginning();
        CitySTw.ResetToBeginning();
        JiantouSpr1Tw.ResetToBeginning();
        JiantouSpr2Tw.ResetToBeginning();
        JiantouSpr3Tw.ResetToBeginning();
        JiantouSpr4Tw.ResetToBeginning();

        HeroTexTw.from = 0f;
        HeroTexTw.to = 1;
        HeroTexTw.duration = 1f;
        HeroTexTw.delay = 1.5f;

        ZhanlinSprTw.from = 0f;
        ZhanlinSprTw.to = 1;
        ZhanlinSprTw.duration = 1f;
        ZhanlinSprTw.delay = 2f;

        CityDTw.from = 0f;
        CityDTw.to = 1;
        CityDTw.duration = 0.3f;
        CityDTw.delay = 3f;

        JiantouSpr1Tw.from = 0f;
        JiantouSpr1Tw.to = 1;
        JiantouSpr1Tw.duration = 0.3f;
        JiantouSpr1Tw.delay = 3.3f;

        CityCTw.from = 0f;
        CityCTw.to = 1;
        CityCTw.duration = 0.3f;
        CityCTw.delay = 3.6f;

        JiantouSpr2Tw.from = 0f;
        JiantouSpr2Tw.to = 1;
        JiantouSpr2Tw.duration = 0.3f;
        JiantouSpr2Tw.delay = 3.9f;


        CityBTw.from = 0f;
        CityBTw.to = 1;
        CityBTw.duration = 0.3f;
        CityBTw.delay = 4.2f;


        JiantouSpr3Tw.from = 0f;
        JiantouSpr3Tw.to = 1;
        JiantouSpr3Tw.duration = 0.3f;
        JiantouSpr3Tw.delay = 4.5f;

        CityATw.from = 0f;
        CityATw.to = 1;
        CityATw.duration = 0.3f;
        CityATw.delay = 4.8f;


        JiantouSpr4Tw.from = 0f;
        JiantouSpr4Tw.to = 1;
        JiantouSpr4Tw.duration = 0.3f;
        JiantouSpr4Tw.delay = 5.2f;

        CitySTw.from = 0f;
        CitySTw.to = 1;
        CitySTw.duration = 0.3f;
        CitySTw.delay = 5.5f;

        FirstEnterParent.SetActive(true);

        HeroTexTw.PlayForward();
        ZhanlinSprTw.PlayForward();
        CityDTw.PlayForward();
        CityCTw.PlayForward();
        CityBTw.PlayForward();
        CityATw.PlayForward();
        CitySTw.PlayForward();
        JiantouSpr1Tw.PlayForward();
        JiantouSpr2Tw.PlayForward();
        JiantouSpr3Tw.PlayForward();
        JiantouSpr4Tw.PlayForward();

        yield return new WaitForSeconds(6f);
        FirstEnterParent.SetActive(false);
        ButtonParent.SetActive(true);
    }



    /// <summary>
    /// 底部战报公告
    /// </summary>
    /// <param name="InfoStr"></param>
    public void ShowPopupInfo(string InfoStr) 
    {
        PopupList.Add(InfoStr);
        if (!PopupWindow.activeSelf) 
        {
            StartCoroutine(ShowPopupInfoIE(InfoStr));
        }
    }

    IEnumerator ShowPopupInfoIE(string InfoStr)
    {
        PopupWindow.SetActive(true);
        TweenBg.from = new Vector3(1f, 0, 0);
        TweenBg.to = new Vector3(1f, 1f, 0);
        TweenBg.PlayForward();
        yield return new WaitForSeconds(1f);

        while (PopupList.Count>0)
        {
            LabelMessage.text = InfoStr;

            TweenMessage.from = new Vector3(1f, 0, 0);
            TweenMessage.to = new Vector3(1f, 1f, 0);
            TweenMessage.ResetToBeginning();
            TweenMessage.PlayForward();
            yield return new WaitForSeconds(4.2f);
            PopupList.RemoveAt(0);
        }

        TweenBg.from = new Vector3(1f, 1, 0);
        TweenBg.to = new Vector3(1f, 0f, 0);
        TweenBg.ResetToBeginning();
        TweenBg.PlayForward();
        yield return new WaitForSeconds(1.2f);
        PopupWindow.SetActive(false);
    }

    /// <summary>
    /// 初始化判断按钮是否点击
    /// </summary>

    private void IsOpenTogglebrushCity() 
    {
        if (CharacterRecorder.instance.AutomaticbrushCity)
        {
            CharacterRecorder.instance.AutomaticbrushCity = true;
            TogglebrushCity.transform.Find("Checkmark").gameObject.SetActive(true);

            CancelInvoke("InvokeRushCityEverytenSecond");
            InvokeRepeating("InvokeRushCityEverytenSecond", 3f, 10f);
        }
        else 
        {
            CharacterRecorder.instance.AutomaticbrushCity = false;
            TogglebrushCity.transform.Find("Checkmark").gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 点击自动刷城按钮
    /// </summary>
    public void ClickTogglebrushCity() 
    {
        if (CharacterRecorder.instance.AutomaticbrushCity)
        {
            CharacterRecorder.instance.AutomaticbrushCity = false;
            TogglebrushCity.transform.Find("Checkmark").gameObject.SetActive(false);
        }
        else 
        {
            if (CharacterRecorder.instance.legionID == 0) 
            {
                UIManager.instance.OpenPromptWindow("您还没有加入军团,不能开启此功能", PromptWindow.PromptType.Hint, null, null);
            }
            else if (CharacterRecorder.instance.MarinesInfomation1.CityId == 0) 
            {
                UIManager.instance.OpenPromptWindow("此功能默认第一编队上阵", PromptWindow.PromptType.Hint, null, null);
            }
            else if (CharacterRecorder.instance.LegionActionPoint <= 0)
            {
                UIManager.instance.OpenPromptWindow("行动力不足,无法开启此功能", PromptWindow.PromptType.Hint, null, null);
            }
            else 
            {
                CharacterRecorder.instance.AutomaticbrushCity = true;
                TogglebrushCity.transform.Find("Checkmark").gameObject.SetActive(true);
                CancelInvoke("InvokeRushCityEverytenSecond");
                InvokeRepeating("InvokeRushCityEverytenSecond", 3f, 10f);
            }
        }
    }


    /// <summary>
    /// 10s秒刷新城市
    /// </summary>
    private void InvokeRushCityEverytenSecond() 
    {
        CharacterRecorder.instance.AutomaticbrushCityID = 0;
        if (CharacterRecorder.instance.AutomaticbrushCity)
        {
            if (CharacterRecorder.instance.MarinesInfomation1.CityId == 0) //第一编队没有
            {
                CharacterRecorder.instance.AutomaticbrushCity = false;
                TogglebrushCity.transform.Find("Checkmark").gameObject.SetActive(false);
                CancelInvoke("InvokeRushCityEverytenSecond");
            }
            else if (CharacterRecorder.instance.LegionActionPoint <= 0) //行动力不足
            {
                CharacterRecorder.instance.AutomaticbrushCity = false;
                TogglebrushCity.transform.Find("Checkmark").gameObject.SetActive(false);
                CancelInvoke("InvokeRushCityEverytenSecond");
            }
            else if (CharacterRecorder.instance.MarinesInfomation1.CityId != 0) //判断是否需要复活
            {
                string[] trcSplit = CharacterRecorder.instance.MarinesInfomation1.BloodNumber.Split('!');
                float allbloodnum = 0f;

                for (int i = 0; i < trcSplit.Length - 1; i++)
                {
                    string[] oneCaptainStr = trcSplit[i].Split('$');
                    allbloodnum += float.Parse(oneCaptainStr[1]);
                }

                if (allbloodnum == 0)//血量为0，复活按钮    allbloodnum < allmaxbloodnum
                {
                    NetworkHandler.instance.SendProcess("8637#1;1;");
                }
                else
                {
                    Debug.Log("编队1自动行走");

                    int legionPoint = 0;//ArrLegionPoint.Length;
                    if (brushCityIdlist.Count > 0) 
                    {
                        //int max=brushCityIdlist.Count;
                        //int num=Random.Range(0,max);
                        legionPoint = brushCityIdlist[0];
                    }
                    else if (brushCityIdNoGoodList.Count > 0) 
                    {
                        legionPoint = brushCityIdNoGoodList[0];
                    }
                    Debug.Log("自动选择点  " + legionPoint);
                    if (legionPoint != 0)
                    {
                        CharacterRecorder.instance.AutomaticbrushCityID = legionPoint;//自动选点

                        NetworkHandler.instance.SendProcess("8602#" + legionPoint + ";");
                    }
                }
            }
        }
        else 
        {
            CharacterRecorder.instance.AutomaticbrushCity = false;
            TogglebrushCity.transform.Find("Checkmark").gameObject.SetActive(false);
            CancelInvoke("InvokeRushCityEverytenSecond");
        }
    }

    /// <summary>
    /// 选好点后发送移动协议
    /// </summary>
    public void AutoClickTeamHero1() 
    {
        CharacterRecorder.instance.MarinesTabe = 1;
        if (CharacterRecorder.instance.MarinesInfomation1.TeamHero != null && CharacterRecorder.instance.AutomaticbrushCityID!=0)
        {
            GotoCityNum(CharacterRecorder.instance.AutomaticbrushCityID);//定位
            if (CharacterRecorder.instance.MarinesInfomation1.IsMove) //移动时转变方向
            {
                HeroTeamToMove(CharacterRecorder.instance.MarinesInfomation1, CharacterRecorder.instance.AutomaticbrushCityID);
            }
            else if (CharacterRecorder.instance.MarinesInfomation1.IsMove == false && CharacterRecorder.instance.MarinesInfomation2.IsMove == false && CharacterRecorder.instance.MarinesInfomation3.IsMove == false)
            {
                if (CharacterRecorder.instance.MarinesInfomation1.NowCityId != CharacterRecorder.instance.AutomaticbrushCityID) //移动到另一个点
                {
                    CharacterRecorder.instance.MarinesInfomation1.IsMove = true;
                    HeroTeamToMove(CharacterRecorder.instance.MarinesInfomation1, CharacterRecorder.instance.AutomaticbrushCityID);
                }
                else //本点骚扰
                {
                    NetworkHandler.instance.SendProcess("8635#" + 1 + ";" + CharacterRecorder.instance.AutomaticbrushCityID + ";");
                }
            }
            else
            {
                Debug.Log("时间到了，但有编队自动行走,等待下次刷新");
            }
        }
    }
}




public class LegionTeamPosition
{
    internal int _CharacterID = 0;
    internal int _CharacterPosition = 0;
}


public class MarinesInfomation //战队信息
{
    public int TeamNumberId=0;
    public float WeakPoint=0;
    public int CityId=0;     //真实布阵所在城市点，服务端确定
    public string TeamInformation="";
    public string BloodNumber="";
    public int IsFight=0;
    public int timeNum=0;
    public GameObject TeamHero = null;
    public Vector3 toPosition=Vector3.zero;

    public int NowCityId = 0;//当前icon所在的城市位置，初始创建时和cityid一样
    public int ToCityId = 0;//将要前往的城市id

    public bool IsMove = false;//是否能移动

    public MarinesInfomation(int TeamNumberId, float WeakPoint,int CityId,string TeamInformation, string BloodNumber, int IsFight,int timeNum) 
    {
        this.TeamNumberId = TeamNumberId;
        this.WeakPoint = WeakPoint;
        this.CityId = CityId;
        this.TeamInformation = TeamInformation;
        this.BloodNumber = BloodNumber;
        this.IsFight = IsFight;
        this.timeNum = timeNum;
        this.NowCityId = CityId;
    }

    public void AddTeamhero(GameObject obj) 
    {
        this.TeamHero = obj;
        toPosition = obj.transform.localPosition;
    }

    public void SetHeroPosition(Vector3 toPosition) //float numx, float numy, float stepNumx, float stepNumy,
    {
        this.toPosition = toPosition; 
    }

    public void SetTimeNum(int time) 
    {
        this.timeNum = time;
    }
}


public class Harassformation //当前骚扰编队信息
{
    public int HeroId;
    public int Position;
    public float WeakPoint;
    public int BloodNum;
    public int MaxBloodNum;

    public Harassformation(int HeroId, int Position, float WeakPoint, int BloodNum, int MaxBloodNum) 
    {
        if (Position == 31)
        {
            Position = 21;
        }
        else if (Position == 36)
        {
            Position = 16;
        }
        else if (Position == 41)
        {
            Position = 11;
        }

        else if (Position == 32)
        {
            Position = 17;
        }
        else if (Position == 37)
        {
            Position = 12;
        }

        else if (Position == 28)
        {
            Position = 18;
        }
        else if (Position == 33)
        {
            Position = 13;
        }
        else if (Position == 38)
        {
            Position = 8;
        }

        else if (Position == 29)
        {
            Position = 14;
        }
        else if (Position == 34)
        {
            Position = 9;
        }

        else if (Position == 25)
        {
            Position = 15;
        }
        else if (Position == 30)
        {
            Position = 10;
        }
        else if (Position == 35)
        {
            Position = 5;
        }

        this.HeroId = HeroId;
        this.Position = Position;
        this.WeakPoint = WeakPoint;
        this.BloodNum = BloodNum;
        this.MaxBloodNum = MaxBloodNum;
    }
}

public class LegionwarGetnode 
{
    public int LegionPoint = 0;                 //战场点ID 
    public int LegionID = 0;                    //占领帮会ID
    public string LegionName = "";              //帮会名称 
    public int LegionFlag = 0;                  //帮会旗 
    public string ItemStr = "";                 //资源产出 
    public int DeclareDate = 0;                 //被宣战状态
    public int Reinforcement = 0;               //援军数量 
    public int IsSoldier = 0;                   //是否有上兵 
    public int IsAvoidWar = 0;                  //免战状态 
    public string DeclareLegionName = "";       //哪个帮会宣战    
    public int DeclareHaveNum = 0;              //剩馀宣战次数 
    public int LegionGrade = 0;                 //占领军团等级 
    public int CountryID = 0;                   //国家id


                                //战场点ID ;    占领帮会ID;         帮会名称;       帮会旗;        资源产出;       被宣战状态;    
    public LegionwarGetnode(int LegionPoint, int LegionID, string LegionName, int LegionFlag, string ItemStr, int DeclareDate,
                                //援军数量;  是否有上兵1有0没有;   免战状态;         哪个帮会宣战;          剩馀宣战次数;    占领军团等级   国家ID
                            int Reinforcement, int IsSoldier, int IsAvoidWar, string DeclareLegionName, int DeclareHaveNum, int LegionGrade, int CountryID) 
    {
        this.LegionPoint = LegionPoint;
        this.LegionID = LegionID;
        this.LegionName = LegionName;
        this.LegionFlag = LegionFlag;
        this.ItemStr = ItemStr;
        this.DeclareDate = DeclareDate;
        this.Reinforcement = Reinforcement;
        this.IsSoldier = IsSoldier;
        this.IsAvoidWar = IsAvoidWar;
        this.DeclareLegionName = DeclareLegionName;
        this.DeclareHaveNum = DeclareHaveNum;
        this.LegionGrade = LegionGrade;
        this.CountryID = CountryID;
    }
}