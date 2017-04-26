using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;

public class MapGateInfoWindow : MonoBehaviour
{
    //public GameObject SpriteStar1;
    //public GameObject SpriteStar2;
    //public GameObject SpriteStar3;
    //public GameObject SpriteStar4;
    //public GameObject SpriteStar5;
    //public GameObject SpriteStar6;

    public GameObject uiGrid;
    public GameObject uiGrid1;

    //public GameObject ButtonTab2;
    public int ChallengeCount = 3;
    public GameObject SweepBtn;
    public GameObject Sweep10Btn;
    public GameObject ResetBtn;

    public GameObject GateAwardItem;

    public GameObject LabelExplane;
    public GameObject LabelDesc;
    public GameObject LabelCount;
    public GameObject LabelName;
    public UILabel couldRefightLabel;
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;
    public GameObject BaseButton;
    public GameObject ChallengeButton;
    public GameObject ChallengeInfo;
    public GameObject MapIcon;

    public GameObject GateNum1;
    public GameObject GateNum2;
    public GameObject GateNum3;
    public GameObject GateNum4;

    public GameObject CreamObj;
    //public GameObject NomalObj;

    public int SweepNum = 0;//扫荡次数
    public int GateID;


    private GameObject GateInfoWindowObj;
    float[] SimpleStar = new float[6];
    float[] MasterStar = new float[6];
    float[] ChallengeStar = new float[6];
    int MapStart;
    //int GateID;
    int Index;
    int Tab;
    public int InitGateID;
    public int InitSelectNum = 0;
    int CreamInitSelectNum = 0;//精英关初始选择关卡
    int ChapterStarNum;
    int MapPoint;
    int PassGateNum = 0;//当前Group过关数
    int CurStarNum = 0;//当前Group获得的星级数
    int GateStarNum = 0;//当前Group最大星级数
    int CurClearanceNum = 0;//当前通关数
    bool Tab2IsEnableClick = true;//Tab2点击是否有用
    bool IsShowLock = false;
    List<TextTranslator.Gate> myCreamGate = new List<TextTranslator.Gate>();
    public int GateNum = 4;
    public UILabel LabelCurStar;
    public UILabel LabelCompletion;
    public UILabel LabelCreamStar;

    //public GameObject GateBox1;
    //public GameObject GateBox2;
    //public GameObject GateBox3;
    //public GameObject GateBox4;
    public GameObject GateTotalBox;
    public GameObject GatePrecentBox;
    Vector3 MapCameraPosition;
    public GameObject SweepWindowObj;

    //index = 0普通关卡 index = 1精英关卡 index = 2通关
    public int[] boxStatue = new int[3];
    GameObject MapNameObj;

    public int ResetChallageNum;//重置次数
    string resetMessage = "";

    void Start()
    {
        MapNameObj = GameObject.Find("MapUiWindow/All/MapName");
        //GameObject.Find("MapObject").transform.Find("MapCon").GetComponent<MapWindow>().MapSatrList.Clear();
        //NetworkHandler.instance.SendProcess("2001#1;");
        GateInfoWindowObj = GameObject.Find("GateInfoWindow");

        PictureCreater.instance.ResetRandom();

        //if (CharacterRecorder.instance.level < 8)
        //{

        //}
        if (UIEventListener.Get(GameObject.Find("Tab1Button")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("Tab1Button")).onClick += delegate(GameObject go)
            {
                CharacterRecorder.instance.IsOpenCreamCN = false;
                this.transform.FindChild("HeroList").gameObject.SetActive(true);
                //if (GateID == CharacterRecorder.instance.lastGateID)
                //{
                //    ChallengeInfo.transform.GetChild(1).GetComponent<UILabel>().text = "0";
                //}
                //else
                ChallengeInfo.transform.GetChild(1).GetComponent<UILabel>().text = "6";
                SetTab(0);
                CreamObj.SetActive(false);
                //NomalObj.SetActive(true);
                SetGateShow(MapPoint, 1);
                SetPageInfo(InitGateID);
                SetSweepBtn();
                GateNum1.GetComponent<SpriteGateNum>().IsBtnClick = true;
                GateNum1.GetComponent<SpriteGateNum>().SetSelectGateNum(InitSelectNum);
                ShowTabButtonColor(0);
            };
        }
        //if (UIEventListener.Get(GateTotalBox).onClick == null)
        {
            UIEventListener.Get(GateTotalBox).onClick += delegate(GameObject go)
            {
                if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 5 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 1)
                {
                    //GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().CloseGateInfoWindow(1);
                    SceneTransformer.instance.NewGuideButtonClick();
                }

                if (boxStatue[0] == 1)
                {
                    GameObject MapUiWindowObj = GameObject.Find("MapUiWindow");
                    MapUiWindowObj.GetComponent<MapUiWindow>().GetRewardGroupId = MapPoint;
                    NetworkHandler.instance.SendProcess("2014#" + MapPoint + ";1;");
                    NetworkHandler.instance.SendProcess("2016#" + MapPoint + ";");
                }
                else
                {
                    if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 5 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) < 3)
                    {
                        GameObject.Find("MapUiWindow").SetActive(false);
                    }
                    else
                    {
                        UIManager.instance.OpenSinglePanel("TheChestWindow", false);
                        TheChestWindow tcw = GameObject.Find("TheChestWindow").GetComponent<TheChestWindow>();
                        tcw.InitGetStarReward(1, MapPoint);
                    }
                }
            };
        }

        //if (UIEventListener.Get(transform.Find("CenterContent/CreamSprite/SpriteBox").gameObject).onClick == null)
        {
            UIEventListener.Get(transform.Find("CenterContent/CreamSprite/SpriteBox").gameObject).onClick += delegate(GameObject go)
            {
                if (boxStatue[1] == 1)
                {
                    GameObject MapUiWindowObj = GameObject.Find("MapUiWindow");
                    MapUiWindowObj.GetComponent<MapUiWindow>().GetRewardGroupId = MapPoint;
                    NetworkHandler.instance.SendProcess("2014#" + MapPoint + ";2;");
                    NetworkHandler.instance.SendProcess("2016#" + MapPoint + ";");
                }
                else
                {
                    UIManager.instance.OpenSinglePanel("TheChestWindow", false);
                    TheChestWindow tcw = GameObject.Find("TheChestWindow").GetComponent<TheChestWindow>();
                    tcw.InitGetStarReward(2, MapPoint);
                }
            };
        }

        if (UIEventListener.Get(GameObject.Find("BgCollider")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("BgCollider")).onClick += delegate(GameObject go)
            {
                if (SceneTransformer.instance.CheckGuideIsFinish() && CharacterRecorder.instance.GuideID[27] != 1 && CharacterRecorder.instance.GuideID[27] != 2)
                {
                    if (CharacterRecorder.instance.GuideID[27] != 1)
                    {
                        SetTab(0);
                        gameObject.SetActive(false);
                        GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().CloseTopContent();
                        InitSelectNum = 0;
                        CreamInitSelectNum = 0;
                        CharacterRecorder.instance.IsOpenCreamCN = false;
                        if (GameObject.Find("MapMask") != null)
                        {
                            GameObject.Find("MapMask").SetActive(false);
                        }
                    }
                }
            };
        }

        //if (UIEventListener.Get(GameObject.Find("Tab2Button")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("Tab2Button")).onClick += SetOpenTab2;
        }

        if (SweepBtn != null)
        {//1次扫荡按钮
            UIEventListener.Get(SweepBtn).onClick += delegate(GameObject go)
            {
                if (GateID > 10000 && GateID < 20000 && CharacterRecorder.instance.stamina < 6)
                {
                    //UIManager.instance.OpenPanel("BuyPropsWindow", false);
                    //NetworkHandler.instance.SendProcess("5012#10602;");
                    //UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
                    NetworkHandler.instance.SendProcess("5012#10602;");
                }
                else if (GateID > 20000 && CharacterRecorder.instance.stamina < 12)
                {
                    //UIManager.instance.OpenPanel("BuyPropsWindow", false);
                    //NetworkHandler.instance.SendProcess("5012#10602;");
                    //UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
                    NetworkHandler.instance.SendProcess("5012#10602;");
                }
                else if (GateID > 20000 && ChallengeCount <= 0)
                {
                    UIManager.instance.OpenPromptWindow("无扫荡次数", PromptWindow.PromptType.Alert, null, null);
                }
                else
                {
                    SweepWindowObj.SetActive(true);
                    SweepWindowObj.transform.GetComponent<SweepChoseWindow>().GetMapId(GateID);
                }

                //UIManager.instance.OpenPromptWindow("即将开放", PromptWindow.PromptType.Alert, null, null);
            };
        }

        if (ResetBtn != null)
        {
            UIEventListener.Get(ResetBtn).onClick += delegate(GameObject go)
            {
                //ChallengeCount = 3;
                //ResetBtn.SetActive(false);
                if (CreateResetBtnString())
                {
                    UIManager.instance.OpenPromptWindow(resetMessage, PromptWindow.PromptType.Confirm, ResetBtnClick, null);
                }

            };
        }

        if (ChallengeButton != null)
        {
            UIEventListener.Get(ChallengeButton).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.lastGateID == 10006)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1801);
                }
                else if (CharacterRecorder.instance.lastGateID == 10007)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1900);
                }
                else if (CharacterRecorder.instance.lastGateID == 10008)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2101);
                }
                else if (CharacterRecorder.instance.lastGateID == 10009)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2301);
                }
                else if (CharacterRecorder.instance.lastGateID == 10010)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2400);
                }
                else if (CharacterRecorder.instance.lastGateID == 10011 && CharacterRecorder.instance.lastCreamGateID >= 20002)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2601);
                }
                if (SceneTransformer.instance.CheckGuideIsFinish() == false)
                {
                    if ((PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 3 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 10)
                      || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 6 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 5)
                      || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 10 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 3)
                      || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 12 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 3)
                      || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 17 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 4)
                    )
                    {
                        SceneTransformer.instance.NewGuideButtonClick();
                    }

                }
                else
                {
                    if (CharacterRecorder.instance.GuideID[27] == 2)
                    {
                        SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                    }
                }
                if (GateID > 20000)
                {
                    if (CharacterRecorder.instance.stamina < 12)
                    {
                        UIManager.instance.OpenPromptWindow("体力不足，是否购买体力体力药剂?", PromptWindow.PromptType.Confirm, OpenBuyWindow, null);
                        return;
                    }
                }
                else
                {
                    if (CharacterRecorder.instance.stamina < 6 && GateID != CharacterRecorder.instance.lastGateID)
                    {
                        UIManager.instance.OpenPromptWindow("体力不足，是否购买体力药剂?", PromptWindow.PromptType.Confirm, OpenBuyWindow, null);
                        return;
                    }
                }
                UIManager.instance.CurGateId = GateID.ToString();
                SceneTransformer.instance.NowGateID = GateID;
                UIManager.instance.StartGate(UIManager.GateAnchorName.gateFight);
                UIManager.instance.MapGateInfoLoading = true;
                if (GateID > 20000 && GateID < 30000)
                {
                    CharacterRecorder.instance.CurCreamGateCount = ChallengeCount;
                    CharacterRecorder.instance.IsOpenCreamCN = true;
                }
                CharacterRecorder.instance.IsOpenMapGate = true;
                //CharacterRecorder.instance.backGateID = GateID;
                //CharacterRecorder.instance.gotoGateID = InitGateID;
                TextTranslator.Gate g = TextTranslator.instance.GetGateByID(GateID);

                GameObject _mapMask = GameObject.Find("MapObject/MapMask");
                if (_mapMask != null)
                {
                    _mapMask.SetActive(false);
                }
                if (GameObject.Find("MapUiWindow") != null)
                {
                    DestroyImmediate(GameObject.Find("MapUiWindow"));
                }

                if (g.bossID > 0)
                {
                    UIManager.instance.OpenPanel("LoadingWindow", true);
                    UIManager.instance.OpenSinglePanel("BossWarningWindow", false);
                    GameObject.Find("BossWarningWindow").GetComponent<BossWarningWindow>().ShowBossInfo(g.bossID);
                }
                else
                {
                    UIManager.instance.OpenPanel("LoadingWindow", true);
                }

                if (GameObject.Find("MapObject/MapCon/MapCamera") != null)
                {
                    GameObject MapCamera = GameObject.Find("MapObject/MapCon/MapCamera");
                    if (MapCamera != null)
                    {
                        MapCameraPosition = MapCamera.transform.position;
                    }
                    if (GameObject.Find("MainCamera") != null)
                    {
                        GameObject.Find("MainCamera").GetComponent<MouseClick>().MapCameraPosition = MapCameraPosition;
                    }
                }

                PictureCreater.instance.StartFight();
            };
        }
        GameObject _heroList = this.transform.FindChild("HeroList").gameObject;
        if (_heroList != null)
        {
            if (UIEventListener.Get(_heroList).onClick == null)
            {
                UIEventListener.Get(_heroList).onClick = delegate(GameObject go)
                {
                    if (GateID > 0)
                    {
                        UIManager.instance.OpenPanel("HeroListWindow", false);
                        NetworkHandler.instance.SendProcess("2018#" + GateID + ";");
                    }
                };
            }
        }

        UIEventListener.Get(GameObject.Find("BgTexture")).onClick = delegate(GameObject go)
        {
            if (!SceneTransformer.instance.CheckGuideIsFinish())
            {
                gameObject.SetActive(false);
            }
        };
    }

    bool CreateResetBtnString()
    {
        int CanResetChallengeCount = TextTranslator.instance.GetVipDicByID(CharacterRecorder.instance.Vip).ResetMasterGateCount;
        int Diamond = TextTranslator.instance.GetMarketByID(CanResetChallengeCount - ResetChallageNum + 1).ResetMstGateDiamondCost;
        if (ResetChallageNum > 0 && ChallengeCount == 0)
        {
            resetMessage = "重置关卡需要消费" + Diamond + "钻石，是否继续？\n（今天还可重置" + ResetChallageNum + "次）";
            return true;
        }
        else if (ResetChallageNum == 0)
        {
            UIManager.instance.OpenPromptWindow("今天的重置次数用完，明天再来哦", PromptWindow.PromptType.Alert, null, null);
        }
        return false;
    }

    void ResetBtnClick()
    {
        NetworkHandler.instance.SendProcess("2010#" + GateID + ";");
        //int ChallengCount = GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().ChallengeCount;
    }

    void OpenBuyWindow()
    {
        //UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
        NetworkHandler.instance.SendProcess("5012#10602;");
    }

    private void ShowTabButtonColor(int i)
    {//点击普通和精英按钮的图标变换
        if (i == 0)
        {
            GateInfoWindowObj.transform.Find("Tab2Button").GetComponent<UISprite>().spriteName = "sandang_button1";//点击普通和精英按钮的图标变换
            GateInfoWindowObj.transform.Find("Tab1Button").GetComponent<UISprite>().spriteName = "sandang_button2";
            this.transform.Find("Tab1Button").transform.Find("Label").GetComponent<UILabel>().color = new Color(1, 1, 1);
            this.transform.Find("Tab1Button").transform.Find("Label").GetComponent<UILabel>().applyGradient = true;
            this.transform.Find("Tab1Button").transform.Find("Label").GetComponent<UILabel>().gradientTop = new Color(1, 1, 1);
            this.transform.Find("Tab1Button").transform.Find("Label").GetComponent<UILabel>().gradientBottom = new Color(122 / 255f, 234 / 255f, 235 / 255f);
            this.transform.Find("Tab1Button").transform.Find("Label").GetComponent<UILabel>().effectStyle = UILabel.Effect.Outline;
            this.transform.Find("Tab1Button").transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(45 / 255f, 105 / 255f, 105 / 255f);
            //this.transform.Find("SimpleObj/shuoming").transform.GetComponent<UILabel>().color = new Color(128 / 255f, 199 / 255f, 104 / 255f);

            //GateInfoWindowObj.transform.Find("BgTexture").GetComponent<UITexture>().name = "saodangTex 1";
            GateInfoWindowObj.transform.Find("BgTexture").GetComponent<UITexture>().mainTexture = Resources.Load("Game/" + "saodangTex") as Texture;
            //GateInfoWindowObj.transform.Find("SimpleObj/SpriteScrollBG").GetComponent<UISprite>().spriteName = "ui_saodang_di5";

            //设置普通关宝箱
            GateInfoWindowObj.transform.Find("BottomContent").Find("SpriteBG").GetComponent<UISprite>().spriteName = "";    //sandang_kuang2
            GateInfoWindowObj.transform.Find("CenterContent").Find("RightInfo").Find("SpriteBG").GetComponent<UISprite>().spriteName = "";      //sandang_kuang0
            GateInfoWindowObj.transform.Find("CenterContent").Find("RightInfo").gameObject.SetActive(true);
            //GateInfoWindowObj.transform.Find("BgTexture").Find("SpriteTitle").GetComponent<UISprite>().spriteName = "xdi6";
            //GateInfoWindowObj.transform.Find("BgTexture").Find("MapIcon").GetComponent<UISprite>().spriteName = "sj_bluedi";
            //GateInfoWindowObj.transform.Find("Tab1Button").transform.Find("Label").GetComponent<UILabel>().color = new Color(46 / 255f, 112 / 255f, 148 / 255f, 255 / 255f);
            GateInfoWindowObj.transform.Find("Tab2Button").transform.Find("Label").GetComponent<UILabel>().color = new Color(135 / 255f, 171 / 255f, 230 / 255f, 255 / 255f);
            this.transform.Find("Tab2Button").transform.Find("Label").GetComponent<UILabel>().effectStyle = UILabel.Effect.Outline;
            this.transform.Find("Tab2Button").transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(63 / 255f, 75 / 255f, 91 / 255f);
            this.transform.Find("Tab2Button").transform.Find("Label").GetComponent<UILabel>().applyGradient = false;
            LabelCount.SetActive(false);
        }
        else
        {
            GateInfoWindowObj.transform.Find("Tab2Button").GetComponent<UISprite>().spriteName = "sandang_button3";
            GateInfoWindowObj.transform.Find("Tab1Button").GetComponent<UISprite>().spriteName = "sandang_button1";
            this.transform.Find("Tab2Button").transform.Find("Label").GetComponent<UILabel>().color = new Color(1, 1, 1);
            this.transform.Find("Tab2Button").transform.Find("Label").GetComponent<UILabel>().applyGradient = true;
            this.transform.Find("Tab2Button").transform.Find("Label").GetComponent<UILabel>().gradientTop = new Color(1, 1, 1);
            this.transform.Find("Tab2Button").transform.Find("Label").GetComponent<UILabel>().gradientBottom = new Color(185 / 255f, 146 / 255f, 250 / 255f);
            this.transform.Find("Tab2Button").transform.Find("Label").GetComponent<UILabel>().effectStyle = UILabel.Effect.Outline;
            this.transform.Find("Tab2Button").transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(42 / 255f, 42 / 255f, 71 / 255f);
            //this.transform.Find("SimpleObj/shuoming").transform.GetComponent<UILabel>().color = new Color(193 / 255f, 133 / 255f, 193 / 255f);
            //GateInfoWindowObj.transform.Find("BgTexture").GetComponent<UITexture>().name = "saodangTex2";

            GateInfoWindowObj.transform.Find("BgTexture").GetComponent<UITexture>().mainTexture = Resources.Load("Game/" + "saodangTex2") as Texture;
            //GateInfoWindowObj.transform.Find("SimpleObj/SpriteScrollBG").GetComponent<UISprite>().spriteName = "ui_saodang_di3";
            GateInfoWindowObj.transform.Find("BottomContent").Find("SpriteBG").GetComponent<UISprite>().spriteName = "sandang_kuang2";
            GateInfoWindowObj.transform.Find("CenterContent").Find("RightInfo").Find("SpriteBG").GetComponent<UISprite>().spriteName = "sandang_kuang1";
            GateInfoWindowObj.transform.Find("CenterContent").Find("RightInfo").gameObject.SetActive(false);
            //GateInfoWindowObj.transform.Find("BgTexture").Find("SpriteTitle").GetComponent<UISprite>().spriteName = "xdi5";
            //GateInfoWindowObj.transform.Find("BgTexture").Find("MapIcon").GetComponent<UISprite>().spriteName = "ui_saodang_xdi3";
            //GateInfoWindowObj.transform.Find("Tab2Button").transform.Find("Label").GetComponent<UILabel>().color = new Color(46 / 255f, 112 / 255f, 148 / 255f, 255 / 255f);
            GateInfoWindowObj.transform.Find("Tab1Button").transform.Find("Label").GetComponent<UILabel>().color = new Color(135 / 255f, 171 / 255f, 230 / 255f, 255 / 255f);
            this.transform.Find("Tab1Button").transform.Find("Label").GetComponent<UILabel>().effectStyle = UILabel.Effect.Outline;
            this.transform.Find("Tab1Button").transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(63 / 255f, 75 / 255f, 91 / 255f);
            this.transform.Find("Tab1Button").transform.Find("Label").GetComponent<UILabel>().applyGradient = false;
            LabelCount.SetActive(true);
        }
    }
    public void Init(int _GateID, int _Index, float[] _SimpleStar, float[] _MasterStar, float[] _ChallengeStar, int _Mapstart)
    {//开始 地图id，索引，星级

        GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().ShowTopContent();
        for (int i = 0; i < boxStatue.Length; i++)
        {
            boxStatue[i] = 0;
        }
        IsShowLock = false;
        myCreamGate = TextTranslator.instance.GetGateByType(2);
        MapPoint = _GateID % 10000;
        for (int i = 0; i < myCreamGate.Count; i++)
        {
            if (myCreamGate[i].group == MapPoint)
            {
                List<TextTranslator.Gate> _myGate = TextTranslator.instance.GetGroupGate(MapPoint, 2);
                if (_GateID < _myGate[_myGate.Count - 1].id)
                {
                    CharacterRecorder.instance.backGateID = 20000 + MapPoint + 1;
                    NetworkHandler.instance.SendProcess("2017#" + (MapPoint + 20001) + ";");
                }
                else
                {
                    CharacterRecorder.instance.backGateID = 20000 + MapPoint;
                    NetworkHandler.instance.SendProcess("2017#" + (MapPoint + 20000) + ";");
                }
                break;
            }
        }
        GateTotalBox.GetComponent<UISprite>().spriteName = "hezi";
        GatePrecentBox.GetComponent<UISprite>().spriteName = "hezi";


        ChallengeInfo.transform.GetChild(1).GetComponent<UILabel>().text = "6";

        CurStarNum = 0;
        GateStarNum = 0;
        CurClearanceNum = 0;
        GateInfoWindowObj = GameObject.Find("GateInfoWindow");
        SimpleStar = _SimpleStar;
        MasterStar = _MasterStar;

        InitSelectNum = CharacterRecorder.instance.InitSelectGate;
        CreamInitSelectNum = CharacterRecorder.instance.InitSelectCreamGate;
        ChallengeStar = _ChallengeStar;
        MapStart = _Mapstart;
        GateID = _GateID;
        InitGateID = _GateID;
        Index = _Index;
        CreamObj.SetActive(false);
        //MapPoint = TextTranslator.instance.GetGateByID(_GateID).group;
        NetworkHandler.instance.SendProcess("2016#" + MapPoint + ";");
        CharacterRecorder.instance.backGateID = 10000 + MapPoint;
        ShowTabButtonColor(0);

        //CreamObj.SetActive(false);
        //NomalObj.SetActive(true);
        SetGateShow(MapPoint, 1);
        SetPageInfo(GateID);
        SetSweepBtn();
        GateNum1.GetComponent<SpriteGateNum>().SetSelectGateNum(InitSelectNum);
        LabelCurStar.text = CurStarNum + "/" + GateStarNum;
        LabelCompletion.text = Mathf.RoundToInt((float)CurClearanceNum / (float)GateNum * 100) + "%";

        if (CharacterRecorder.instance.IsOpenCreamCN)
        {
            if (CharacterRecorder.instance.lastGateID < 10011)
            {
                UIManager.instance.OpenPromptWindow("长官，通关第10关开放该功能！", PromptWindow.PromptType.Hint, null, null);
                return;
            }
            GameObject _mapCon = GameObject.Find("MapCon");
            if (_mapCon.GetComponent<MapWindow>().IsOpenCream(MapPoint))
            {
                SetOpenTab2(GameObject.Find("Tab2Button"));
            }
            CharacterRecorder.instance.IsOpenCreamCN = false;
        }
    }

    public bool CheckGateId(int _gateId)
    {
        if (_gateId == GateID)
        {
            return true;
        }

        return false;
    }

    public void SetChallengeInfo()
    {
        if (InitGateID < 20000)
        {
            if (InitGateID == CharacterRecorder.instance.lastGateID)
            {
                ChallengeInfo.transform.GetChild(1).GetComponent<UILabel>().text = "0";
            }
            else
            {
                ChallengeInfo.transform.GetChild(1).GetComponent<UILabel>().text = "6";
            }
        }
    }

    public void SetTheChestStatue(int _BoxType, string _BoxStatue)
    {
        string pathStr = "";
        if (_BoxType == 0)
        {
            pathStr = "CenterContent/RightInfo/AboveContent/AllSatrBox";
        }
        else if (_BoxType == 1)
        {
            pathStr = "CenterContent/CreamSprite/SpriteBox";
        }
        //else if(_BoxType == 2)
        //{
        //    //pathStr = "CenterContent/RightInfo/BottomContent/PrecentBox";
        //}
        else
        {
            Debug.Log("服务器数据超出！" + _BoxType);
        }
        int _boxStatue = 0;
        string[] str = _BoxStatue.Split('$');
        if (str[0] != "" && str[0] != null)
        {
            _boxStatue = int.Parse(str[0]);
        }
        boxStatue[_BoxType] = _boxStatue;
        switch (_boxStatue)
        {
            case 0:
                transform.Find(pathStr).GetComponent<UISprite>().spriteName = "hezi";
                transform.Find(pathStr).transform.Find("WF_HeZi_UIeff").gameObject.SetActive(false);
                break;
            case 1:
                transform.Find(pathStr).GetComponent<UISprite>().spriteName = "";
                transform.Find(pathStr).transform.Find("WF_HeZi_UIeff").gameObject.SetActive(true);
                break;
            case 2:
                transform.Find(pathStr).transform.Find("WF_HeZi_UIeff").gameObject.SetActive(false);
                transform.Find(pathStr).GetComponent<UISprite>().spriteName = "hezi2";
                break;
            default:
                Debug.Log("服务器宝箱状态超出！");
                break;
        }
        if (_BoxType == 1 && _boxStatue == 1)
        {
            GameObject _tab2Btn = GameObject.Find("Tab2Button");
            if (_tab2Btn != null)
            {
                _tab2Btn.transform.Find("SpriteRedPoint").gameObject.SetActive(true);
            }
        }
        else if (_BoxType == 1 && _boxStatue != 1)
        {
            GameObject _tab2Btn = GameObject.Find("Tab2Button");
            if (_tab2Btn != null)
            {
                _tab2Btn.transform.Find("SpriteRedPoint").gameObject.SetActive(false);
            }
        }
    }

    public void SetTab(int _Tab)
    {
        int count = uiGrid.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            GameObject go = uiGrid.transform.GetChild(0).gameObject;
            DestroyImmediate(go);
        }
        Tab = _Tab;

        TextTranslator.Gate gate;
        if (Tab == 1)
        {
            gate = TextTranslator.instance.GetGateByID(MapPoint + Tab * 20000);
        }
        else
        {
            gate = TextTranslator.instance.GetGateByID(InitGateID + Tab * 10000);
        }
        //TextTranslator.Gate gate = TextTranslator.instance.GetGateByID(GateID + Tab * 10000);
        //SceneTransformer.instance.NowGateID = GateID + Tab * 10000;

        if (gate.itemID1 != 0 && gate.itemID1 > 10000)
        {
            TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(gate.itemID1);
            GameObject go = NGUITools.AddChild(uiGrid, GateAwardItem);
            go.SetActive(true);
            go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);

            go.GetComponent<ItemExplanation>().SetAwardItem(gate.itemID1, 0);

            SetColor(go, mitemInfo.itemGrade);
            if (gate.itemID1 > 40000 && gate.itemID1 < 50000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID1 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID1 > 80000 && gate.itemID2 < 90000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID1 - 30000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID1 > 70000 && gate.itemID1 < 79999)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID1 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = gate.itemID1.ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(false);
            }
            go.transform.Find("LabelName").gameObject.GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(gate.itemID1);
        }

        if (gate.itemID2 != 0 && gate.itemID2 > 10000)
        {
            TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(gate.itemID2);
            GameObject go = NGUITools.AddChild(uiGrid, GateAwardItem);
            go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            go.SetActive(true);

            go.GetComponent<ItemExplanation>().SetAwardItem(gate.itemID2, 0);

            SetColor(go, mitemInfo.itemGrade);
            if (gate.itemID2 > 40000 && gate.itemID2 < 50000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID2 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID2 > 80000 && gate.itemID2 < 90000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID2 - 30000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID2 > 70000 && gate.itemID2 < 79999)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID2 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = gate.itemID2.ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(false);
            }
            go.transform.Find("LabelName").gameObject.GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(gate.itemID2);
        }

        if (gate.itemID3 != 0 && gate.itemID3 > 10000)
        {
            TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(gate.itemID3);
            GameObject go = NGUITools.AddChild(uiGrid, GateAwardItem);
            go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            go.SetActive(true);

            go.GetComponent<ItemExplanation>().SetAwardItem(gate.itemID3, 0);

            SetColor(go, mitemInfo.itemGrade);
            if (gate.itemID3 > 40000 && gate.itemID3 < 50000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID3 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID3 > 80000 && gate.itemID2 < 90000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID3 - 40000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(false);
            }
            else if (gate.itemID3 > 70000 && gate.itemID3 < 79999)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID3 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = gate.itemID3.ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(false);
            }
            go.transform.Find("LabelName").gameObject.GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(gate.itemID3);
        }

        if (gate.itemID4 != 0 && gate.itemID4 > 10000)
        {
            TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(gate.itemID4);
            GameObject go = NGUITools.AddChild(uiGrid, GateAwardItem);
            go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            go.SetActive(true);

            go.GetComponent<ItemExplanation>().SetAwardItem(gate.itemID4, 0);

            SetColor(go, mitemInfo.itemGrade);
            if (gate.itemID4 > 40000 && gate.itemID4 < 50000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID4 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID4 > 80000 && gate.itemID2 < 90000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID4 - 30000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID4 > 70000 && gate.itemID4 < 79999)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID4 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = gate.itemID4.ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(false);
            }
            go.transform.Find("LabelName").gameObject.GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(gate.itemID4);
        }

        if (gate.itemID5 != 0 && gate.itemID5 > 10000)
        {
            TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(gate.itemID5);
            GameObject go = NGUITools.AddChild(uiGrid, GateAwardItem);
            go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            go.SetActive(true);

            go.GetComponent<ItemExplanation>().SetAwardItem(gate.itemID5, 0);

            SetColor(go, mitemInfo.itemGrade);
            if (gate.itemID5 > 40000 && gate.itemID5 < 50000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID5 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID5 > 80000 && gate.itemID2 < 90000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID5 - 30000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID5 > 70000 && gate.itemID5 < 79999)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID5 - 10000).ToString();//-10000
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = gate.itemID5.ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(false);
            }
            go.transform.Find("LabelName").gameObject.GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(gate.itemID5);
        }

        if (gate.itemID6 != 0 && gate.itemID6 > 10000)
        {
            TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(gate.itemID6);
            GameObject go = NGUITools.AddChild(uiGrid, GateAwardItem);
            go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            go.SetActive(true);

            go.GetComponent<ItemExplanation>().SetAwardItem(gate.itemID6, 0);

            SetColor(go, mitemInfo.itemGrade);
            if (gate.itemID6 > 40000 && gate.itemID6 < 50000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID6 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID6 > 80000 && gate.itemID2 < 90000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID6 - 30000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID6 > 70000 && gate.itemID6 < 79999)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID6 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = gate.itemID6.ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(false);
            }
            go.transform.Find("LabelName").gameObject.GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(gate.itemID6);
        }

        uiGrid.GetComponent<UIGrid>().Reposition();
    }

    void SetOpenTab2(GameObject go)
    {
        //if (CharacterRecorder.instance.level < 8)
        //{
        //    UIManager.instance.OpenPromptWindow("长官，8级开放该功能！", PromptWindow.PromptType.Hint, null, null);
        //    return;
        //}
        if (CharacterRecorder.instance.lastGateID < 10011)
        {
            UIManager.instance.OpenPromptWindow("长官，通关第10关开放该功能！", PromptWindow.PromptType.Hint, null, null);
            return;
        }
        if (!Tab2IsEnableClick)
        {
            UIManager.instance.OpenPromptWindow("当前点上所有关卡3星通关，可开启该点精英关", PromptWindow.PromptType.Hint, null, null);
            return;
        }
        if (IsShowLock)
        {
            UIManager.instance.OpenPromptWindow("通关前一关精英关，可开启该点精英关", PromptWindow.PromptType.Hint, null, null);
            return;
            //int _lastCreamGroup = TextTranslator.instance.GetGateCompleteBoxGroup(TextTranslator.instance.GetGateCompleteBox(MapPoint).GateCompleteBoxID - 1);
            //List<TextTranslator.Gate> _myGate = TextTranslator.instance.GetGroupGate(_lastCreamGroup, 2);
        }
        if (CharacterRecorder.instance.GuideID[27] == 1)
        {
            SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
        }
        CharacterRecorder.instance.IsOpenCreamCN = true;
        ChallengeInfo.transform.GetChild(1).GetComponent<UILabel>().text = "12";
        SetTab(1);
        CreamObj.SetActive(true);
        //NomalObj.SetActive(false);

        CharacterRecorder.instance.backGateID = 20000 + MapPoint;

        SetPageInfo(MapPoint + 20000);
        //SetCreamStar(MapPoint);
        SetGateShow(MapPoint, 2);
        SetSweepBtn();
        GateNum1.GetComponent<SpriteGateNum>().IsBtnClick = true;
        GateNum1.GetComponent<SpriteGateNum>().SetSelectGateNum(CreamInitSelectNum);
        ShowTabButtonColor(1);
    }

    void SetColor(GameObject go, int color)
    {
        switch (color)
        {
            case 1:
                go.GetComponent<UISprite>().spriteName = "Grade1";
                break;
            case 2:
                go.GetComponent<UISprite>().spriteName = "Grade2";
                break;
            case 3:
                go.GetComponent<UISprite>().spriteName = "Grade3";
                break;
            case 4:
                go.GetComponent<UISprite>().spriteName = "Grade4";
                break;
            case 5:
                go.GetComponent<UISprite>().spriteName = "Grade5";
                break;
            case 6:
                go.GetComponent<UISprite>().spriteName = "Grade6";
                break;
            case 7:
                go.GetComponent<UISprite>().spriteName = "Grade7";
                break;
        }
    }

    //void SetCreamStar(int _MapPoint)
    //{
    //    int index = (_MapPoint + 1) / 2 - 1;
    //    int CurCreamStar = 0;
    //    if (index < GameObject.Find("MapObject").transform.Find("MapCon").GetComponent<MapWindow>().CreamSatrList.Count)
    //    {
    //        CurCreamStar = GameObject.Find("MapObject").transform.Find("MapCon").GetComponent<MapWindow>().CreamSatrList[index];
    //    }
    //    switch (CurCreamStar)
    //    {
    //        case 0:
    //            GameObject.Find("GateInfoWindow/CenterContent/CreamSprite/EnemyIcon/Grid").transform.GetChild(0).GetComponent<UISprite>().spriteName = "grab_star4";
    //            GameObject.Find("GateInfoWindow/CenterContent/CreamSprite/EnemyIcon/Grid").transform.GetChild(1).GetComponent<UISprite>().spriteName = "grab_star4";
    //            GameObject.Find("GateInfoWindow/CenterContent/CreamSprite/EnemyIcon/Grid").transform.GetChild(2).GetComponent<UISprite>().spriteName = "grab_star4";
    //            break;
    //        case 1:
    //            GameObject.Find("GateInfoWindow/CenterContent/CreamSprite/EnemyIcon/Grid").transform.GetChild(0).GetComponent<UISprite>().spriteName = "grab_star3";
    //            GameObject.Find("GateInfoWindow/CenterContent/CreamSprite/EnemyIcon/Grid").transform.GetChild(1).GetComponent<UISprite>().spriteName = "grab_star4";
    //            GameObject.Find("GateInfoWindow/CenterContent/CreamSprite/EnemyIcon/Grid").transform.GetChild(2).GetComponent<UISprite>().spriteName = "grab_star4";
    //            break;
    //        case 2:
    //            GameObject.Find("GateInfoWindow/CenterContent/CreamSprite/EnemyIcon/Grid").transform.GetChild(0).GetComponent<UISprite>().spriteName = "grab_star3";
    //            GameObject.Find("GateInfoWindow/CenterContent/CreamSprite/EnemyIcon/Grid").transform.GetChild(1).GetComponent<UISprite>().spriteName = "grab_star3";
    //            GameObject.Find("GateInfoWindow/CenterContent/CreamSprite/EnemyIcon/Grid").transform.GetChild(2).GetComponent<UISprite>().spriteName = "grab_star4";
    //            break;
    //        case 3:
    //            GameObject.Find("GateInfoWindow/CenterContent/CreamSprite/EnemyIcon/Grid").transform.GetChild(0).GetComponent<UISprite>().spriteName = "grab_star3";
    //            GameObject.Find("GateInfoWindow/CenterContent/CreamSprite/EnemyIcon/Grid").transform.GetChild(1).GetComponent<UISprite>().spriteName = "grab_star3";
    //            GameObject.Find("GateInfoWindow/CenterContent/CreamSprite/EnemyIcon/Grid").transform.GetChild(2).GetComponent<UISprite>().spriteName = "grab_star3";
    //            break;
    //    }
    //    LabelCreamStar.text = CurCreamStar + "/3";
    //}

    int CountCreamStar(int mapPoint)
    {
        int countStar = 0;
        List<int> star = new List<int>();
        GameObject _mapWindow = GameObject.Find("MapCon");
        if (_mapWindow != null)
        {
            star = _mapWindow.transform.Find("MapCon").GetComponent<MapWindow>().CreamSatrList;
        }
        else
        {
            return countStar;
        }
        List<TextTranslator.Gate> _gate = TextTranslator.instance.GetGroupGate(mapPoint, 2);
        for (int i = 0; i < myCreamGate.Count; i++)
        {
            foreach (var gate in _gate)
            {
                if (gate.id == myCreamGate[i].id)
                {
                    countStar += star[i];
                }
            }
        }
        return countStar;
    }
    void SetCreamStar(List<TextTranslator.Gate> _gate)
    {
        CurStarNum = 0;
        ChapterStarNum = 0;
        List<int> star = GameObject.Find("MapObject").transform.Find("MapCon").GetComponent<MapWindow>().CreamSatrList;
        for (int i = 0; i < _gate.Count; i++)
        {
            if (i > 3)
            {
                Debug.Log("关卡设置出错！");
                return;
            }
            GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString()).transform.GetChild(0).GetComponent<TweenScale>().ResetToBeginning();
            GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString()).transform.GetChild(0).GetComponent<TweenPosition>().ResetToBeginning();
            GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString()).transform.GetChild(0).GetComponent<TweenScale>().PlayForward();
            GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString()).transform.GetChild(0).GetComponent<TweenPosition>().PlayForward();
            if (_gate[i].id - 20001 < star.Count)
            {
                switch (star[_gate[i].id - 20001])
                {
                    case 0:
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar1").GetComponent<UISprite>().spriteName = "grab_star4";
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar2").GetComponent<UISprite>().spriteName = "grab_star4";
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar3").GetComponent<UISprite>().spriteName = "grab_star4";
                        break;
                    case 1:
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar1").GetComponent<UISprite>().spriteName = "sandang_xingxing";
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar2").GetComponent<UISprite>().spriteName = "grab_star4";
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar3").GetComponent<UISprite>().spriteName = "grab_star4";
                        break;
                    case 2:
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar1").GetComponent<UISprite>().spriteName = "sandang_xingxing";
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar2").GetComponent<UISprite>().spriteName = "sandang_xingxing";
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar3").GetComponent<UISprite>().spriteName = "grab_star4";
                        break;
                    case 3:
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar1").GetComponent<UISprite>().spriteName = "sandang_xingxing";
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar2").GetComponent<UISprite>().spriteName = "sandang_xingxing";
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar3").GetComponent<UISprite>().spriteName = "sandang_xingxing";
                        break;
                    default:
                        Debug.Log("设置副本星级参数有误！");
                        break;
                }
                if (star[_gate[i].id - 20001] != 0)
                {
                    ChapterStarNum++;
                }
                CurStarNum += star[_gate[i].id - 20001];
            }
            else
            {
                GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar1").GetComponent<UISprite>().spriteName = "grab_star4";
                GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar2").GetComponent<UISprite>().spriteName = "grab_star4";
                GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar3").GetComponent<UISprite>().spriteName = "grab_star4";
            }
        }
        if (CreamInitSelectNum == 0)
        {
            if (_gate.Count != ChapterStarNum)
            {
                ChapterStarNum += 1;//开到第N小关，应该从1开始 - 精英关星星数没有对应关卡
            }
            if (CurStarNum != 0)
            {
                CreamInitSelectNum = ChapterStarNum;
            }
            else
            {
                CreamInitSelectNum = 1;
            }
        }
        else
        {
            if (_gate.Count != ChapterStarNum)
            {
                ChapterStarNum += 1;//开到第N小关，应该从1开始 - 精英关星星数没有对应关卡
            }
            CharacterRecorder.instance.InitSelectCreamGate = 0;
        }
        LabelCreamStar.text = CurStarNum + "/" + _gate.Count * 3;
    }

    void SetStarNum(List<TextTranslator.Gate> _gate)
    {
        CurStarNum = 0;
        ChapterStarNum = 0;
        PassGateNum = 0;
        List<int> star = GameObject.Find("MapObject").transform.Find("MapCon").GetComponent<MapWindow>().MapSatrList;
        for (int i = 0; i < _gate.Count; i++)
        {
            if (i > 3)
            {
                Debug.Log("关卡设置出错！");
                return;
            }
            GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString()).transform.GetChild(0).GetComponent<TweenScale>().ResetToBeginning();
            GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString()).transform.GetChild(0).GetComponent<TweenPosition>().ResetToBeginning();
            GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString()).transform.GetChild(0).GetComponent<TweenScale>().PlayForward();
            GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString()).transform.GetChild(0).GetComponent<TweenPosition>().PlayForward();
            if (_gate[i].id - 10001 < star.Count)
            {
                switch (star[_gate[i].id - 10001])
                {
                    case 0:
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar1").GetComponent<UISprite>().spriteName = "grab_star4";
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar2").GetComponent<UISprite>().spriteName = "grab_star4";
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar3").GetComponent<UISprite>().spriteName = "grab_star4";
                        break;
                    case 1:
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar1").GetComponent<UISprite>().spriteName = "xingxing2";
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar2").GetComponent<UISprite>().spriteName = "grab_star4";
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar3").GetComponent<UISprite>().spriteName = "grab_star4";
                        break;
                    case 2:
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar1").GetComponent<UISprite>().spriteName = "xingxing2";
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar2").GetComponent<UISprite>().spriteName = "xingxing2";
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar3").GetComponent<UISprite>().spriteName = "grab_star4";
                        break;
                    case 3:
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar1").GetComponent<UISprite>().spriteName = "xingxing2";
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar2").GetComponent<UISprite>().spriteName = "xingxing2";
                        GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar3").GetComponent<UISprite>().spriteName = "xingxing2";
                        break;
                    default:
                        Debug.Log("设置副本星级参数有误！");
                        break;
                }
                ChapterStarNum++;
                CurStarNum += star[_gate[i].id - 10001];
                if (star[_gate[i].id - 10001] > 0)
                {
                    PassGateNum++;
                }
            }
            else
            {
                GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar1").GetComponent<UISprite>().spriteName = "grab_star4";
                GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar2").GetComponent<UISprite>().spriteName = "grab_star4";
                GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/StarGrid/SpriteStar3").GetComponent<UISprite>().spriteName = "grab_star4";
            }
        }
        if (InitSelectNum == 0)
        {
            //普通关星星数和关卡对应
            if (CurStarNum != 0)
            {
                InitSelectNum = ChapterStarNum;
            }
            else
            {
                InitSelectNum = 1;
            }
        }
        else
        {
            CharacterRecorder.instance.InitSelectGate = 0;
        }
    }

    void SetGateEnable(int count)
    {
        for (int i = 0; i < count; i++)
        {
            if (CurStarNum != 0)
            {
                int _gateNum = 0;
                if (ChapterStarNum == 0)
                {
                    _gateNum = 1;
                }
                else
                {
                    _gateNum = ChapterStarNum;
                }
                if (i + 1 > _gateNum)
                {
                    GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg").GetComponent<UISprite>().color = new Color(128 / 255f, 128 / 255f, 128 / 255f);
                    GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/SpriteRole").GetComponent<UISprite>().color = new Color(128 / 255f, 128 / 255f, 128 / 255f);
                    GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg").GetComponent<BoxCollider>().enabled = false;
                }
                else
                {
                    GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg").GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                    GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/SpriteRole").GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                    GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg").GetComponent<BoxCollider>().enabled = true;
                }
            }
            else
            {
                if (i != 0)
                {
                    GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg").GetComponent<UISprite>().color = new Color(128 / 255f, 128 / 255f, 128 / 255f);
                    GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/SpriteRole").GetComponent<UISprite>().color = new Color(128 / 255f, 128 / 255f, 128 / 255f);
                    GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg").GetComponent<BoxCollider>().enabled = false;
                }
                else
                {
                    GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg").GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                    GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg/SpriteRole").GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                    GameObject.Find("GateInfoWindow/CenterContent/SpriteLine/SpriteGateNum" + (i + 1).ToString() + "/RoleBg").GetComponent<BoxCollider>().enabled = true;
                }
            }
        }
    }

    public void SetSweepBtn()
    {
        if (CharacterRecorder.instance.lastGateID > GateID)
        {
            int starNum = 0;
            if (InitGateID - 10001 <= GameObject.Find("MapObject").transform.Find("MapCon").GetComponent<MapWindow>().MapSatrList.Count - 1)
            {
                starNum = GameObject.Find("MapObject").transform.Find("MapCon").GetComponent<MapWindow>().MapSatrList[InitGateID - 10001];
            }
            else
            {
                Debug.Log("超出MapSatrList数组的界限，MapSatrList元素个数：" + GameObject.Find("MapObject").transform.Find("MapCon").GetComponent<MapWindow>().MapSatrList.Count);
            }
            if (starNum > 2)
            {
                SweepBtn.SetActive(true);
                ResetBtn.SetActive(false);
                ChallengeButton.SetActive(true);
                ChallengeInfo.SetActive(true);
                return;
            }
        }
        //int index = (MapPoint + 1) / 2 - 1;
        int index = 0;
        for (int i = 0; i < myCreamGate.Count; i++)
        {
            if (GateID == myCreamGate[i].id)
            {
                index = i;
                break;
            }
        }
        if (GateID > 20000)
        {
            //if (GameObject.Find("MapObject").transform.Find("MapCon").GetComponent<MapWindow>().CreamSatrList[index] == 3)
            {
                if (ChallengeCount > 0)
                {
                    //ChallengeButton.SetActive(false);
                    //ChallengeInfo.SetActive(false);
                    if ((GameObject.Find("MapObject").transform.Find("MapCon").GetComponent<MapWindow>().CreamSatrList[index] == 3))
                    {
                        SweepBtn.SetActive(true);
                    }
                    else
                    {
                        SweepBtn.SetActive(false);
                    }
                    ResetBtn.SetActive(false);
                    ChallengeButton.SetActive(true);
                    ChallengeInfo.SetActive(true);
                    //Sweep10Btn.SetActive(true);
                }
                else
                {
                    ResetBtn.SetActive(true);
                    SweepBtn.SetActive(false);
                    ChallengeButton.SetActive(false);
                    ChallengeInfo.SetActive(false);
                }
                return;
            }
            //else
            //{
            //    ResetBtn.SetActive(false);
            //    ChallengeButton.SetActive(true);
            //    ChallengeInfo.SetActive(true);
            //}
        }
        else
        {
            if (GameObject.Find("MapObject").transform.Find("MapCon").GetComponent<MapWindow>().MapSatrList[InitGateID - 10001] > 2)
            {
                SweepBtn.gameObject.SetActive(true);
            }
            else
            {
                SweepBtn.gameObject.SetActive(false);
            }
            ResetBtn.SetActive(false);
            ChallengeButton.SetActive(true);
            ChallengeInfo.SetActive(true);
        }
        //SweepBtn.gameObject.SetActive(false);
        //Sweep10Btn.gameObject.SetActive(false);
    }

    public void SetPageInfo(int _gateID)
    {
        GateID = _gateID;
        //SceneTransformer.instance.NowGateID = GateID;
        TextTranslator.Gate gate = TextTranslator.instance.GetGateByID(_gateID);
        if (ObscuredPrefs.GetString("Account").IndexOf("test_") > -1)
        {
            this.transform.Find("BgTexture/LabelDj").GetComponent<UILabel>().text = "等级要求：" + gate.needLevel.ToString();

        }

        string colorCode = "";
        int recommendedPower = gate.needForce;
        if (recommendedPower > CharacterRecorder.instance.Fight)
        {
            colorCode = "[ff0000]";
        }
        this.transform.Find("BgTexture/LabelZdl").GetComponent<UILabel>().text = "[597598]推荐战力:[-]" + colorCode + recommendedPower;
        LabelName.GetComponent<UILabel>().text = gate.name;
        if (_gateID > 20000)
        {
            SetChallengNum();
            LabelDesc.GetComponent<UILabel>().text = gate.description;
            //GateInfoWindowObj.transform.Find("CenterContent").Find("CreamSprite/EnemyIcon").GetComponent<UITexture>().mainTexture = Resources.Load("BigAvatar/" + gate.icon.ToString()) as Texture;
            //GateInfoWindowObj.transform.Find("CenterContent/CreamSprite/LabelDes").GetComponent<UILabel>().text = gate.description;
        }
        else
        {
            LabelDesc.GetComponent<UILabel>().text = gate.description;
            LabelCount.GetComponent<UILabel>().text = "";
        }
    }

    public void SetChallengNum()
    {
        //this.transform.FindChild("HeroList").gameObject.SetActive(false);
        //LabelCount.GetComponent<UILabel>().text = "挑战次数：[74e5f3]" + ChallengeCount + "[-]";
        if (ChallengeCount < 0)
        {
            ChallengeCount = 0;
        }
        string _colorCode = "";
        if (ChallengeCount > 0)
        {
            _colorCode = "[00FF00]";
        }
        else
        {
            _colorCode = "[FF0000]";
        }


        LabelCount.GetComponent<UILabel>().text = "[597598]挑战次数:[-]" + _colorCode + ChallengeCount + "[-]/3";
    }

    void SetGateShow(int _GroupID, int _Type)
    {
        List<TextTranslator.Gate> myGate = TextTranslator.instance.GetGroupGate(_GroupID, _Type);
        GateNum = myGate.Count;
        GateStarNum = GateNum * 3;
        #region 小关卡初始化
        switch (myGate.Count)
        {
            case 1:
                GateNum1.SetActive(true);
                GateNum2.SetActive(false);
                GateNum3.SetActive(false);
                GateNum4.SetActive(false);
                GateNum1.transform.Find("LabelChapter").GetComponent<UILabel>().text = myGate[0].id.ToString();
                if (myGate[0].icon < 60000)
                {
                    GateNum1.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().atlas = ItemAtlas;
                }
                else
                {
                    GateNum1.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().atlas = RoleAtlas;
                }
                GateNum1.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().spriteName = myGate[0].icon.ToString();
                break;
            case 2:
                GateNum1.SetActive(true);
                GateNum2.SetActive(true);
                GateNum3.SetActive(false);
                GateNum4.SetActive(false);

                GateNum1.transform.Find("LabelChapter").GetComponent<UILabel>().text = myGate[0].id.ToString();
                GateNum2.transform.Find("LabelChapter").GetComponent<UILabel>().text = myGate[1].id.ToString();

                if (myGate[0].icon < 60000)
                {
                    GateNum1.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().atlas = ItemAtlas;
                }
                else
                {
                    GateNum1.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().atlas = RoleAtlas;
                }
                if (myGate[1].icon < 60000)
                {
                    GateNum2.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().atlas = ItemAtlas;
                }
                else
                {
                    GateNum2.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().atlas = RoleAtlas;
                }

                GateNum1.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().spriteName = myGate[0].icon.ToString();
                GateNum2.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().spriteName = myGate[1].icon.ToString();
                break;
            case 3:
                GateNum1.SetActive(true);
                GateNum2.SetActive(true);
                GateNum3.SetActive(true);
                GateNum4.SetActive(false);

                GateNum1.transform.Find("LabelChapter").GetComponent<UILabel>().text = myGate[0].id.ToString();
                GateNum2.transform.Find("LabelChapter").GetComponent<UILabel>().text = myGate[1].id.ToString();
                GateNum3.transform.Find("LabelChapter").GetComponent<UILabel>().text = myGate[2].id.ToString();

                if (myGate[0].icon < 60000)
                {
                    GateNum1.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().atlas = ItemAtlas;
                }
                else
                {
                    GateNum1.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().atlas = RoleAtlas;
                }
                if (myGate[1].icon < 60000)
                {
                    GateNum2.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().atlas = ItemAtlas;
                }
                else
                {
                    GateNum2.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().atlas = RoleAtlas;
                }
                if (myGate[2].icon < 60000)
                {
                    GateNum3.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().atlas = ItemAtlas;
                }
                else
                {
                    GateNum3.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().atlas = RoleAtlas;
                }

                GateNum1.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().spriteName = myGate[0].icon.ToString();
                GateNum2.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().spriteName = myGate[1].icon.ToString();
                GateNum3.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().spriteName = myGate[2].icon.ToString();
                break;
            case 4:
                GateNum1.SetActive(true);
                GateNum2.SetActive(true);
                GateNum3.SetActive(true);
                GateNum4.SetActive(true);

                GateNum1.transform.Find("LabelChapter").GetComponent<UILabel>().text = myGate[0].id.ToString();
                GateNum2.transform.Find("LabelChapter").GetComponent<UILabel>().text = myGate[1].id.ToString();
                GateNum3.transform.Find("LabelChapter").GetComponent<UILabel>().text = myGate[2].id.ToString();
                GateNum4.transform.Find("LabelChapter").GetComponent<UILabel>().text = myGate[3].id.ToString();

                if (myGate[0].icon < 60000)
                {
                    GateNum1.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().atlas = ItemAtlas;
                }
                else
                {
                    GateNum1.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().atlas = RoleAtlas;
                }
                if (myGate[1].icon < 60000)
                {
                    GateNum2.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().atlas = ItemAtlas;
                }
                else
                {
                    GateNum2.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().atlas = RoleAtlas;
                }
                if (myGate[2].icon < 60000)
                {
                    GateNum3.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().atlas = ItemAtlas;
                }
                else
                {
                    GateNum3.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().atlas = RoleAtlas;
                }
                if (myGate[3].icon < 60000)
                {
                    GateNum4.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().atlas = ItemAtlas;
                }
                else
                {
                    GateNum4.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().atlas = RoleAtlas;
                }

                GateNum1.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().spriteName = myGate[0].icon.ToString();
                GateNum2.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().spriteName = myGate[1].icon.ToString();
                GateNum3.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().spriteName = myGate[2].icon.ToString();
                GateNum4.transform.Find("RoleBg").Find("SpriteRole").GetComponent<UISprite>().spriteName = myGate[3].icon.ToString();
                break;
        }
        #endregion
        for (int i = 0; i < myGate.Count; i++)
        {
            if (myGate[i].id < CharacterRecorder.instance.lastGateID)
            {
                CurClearanceNum++;
            }
        }
        if (_Type == 1)
        {
            SetStarNum(myGate);
        }
        else
        {
            SetCreamStar(myGate);
        }
        SetGateEnable(myGate.Count);
        if (_Type == 2)
        {
            return;
        }

        bool isShowTab = false;
        for (int i = 0; i < myCreamGate.Count; i++)
        {
            if (myCreamGate[i].group == MapPoint)
            {
                isShowTab = true;
                break;
            }
        }
        GameObject tab2 = GameObject.Find("Tab2Button");
        if (GateStarNum == CurStarNum && isShowTab)
        {
            Tab2IsEnableClick = true;
            tab2.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
            tab2.GetComponent<BoxCollider>().enabled = true;

            if (CharacterRecorder.instance.lastGateID <= 10010)
            {
                GameObject.Find("Tab2Button/Lock").GetComponent<UISprite>().color = new Color(255 / 255f, 206 / 255f, 242 / 255f, 255 / 255f);
            }
            else
            {
                GameObject.Find("Tab2Button/Lock").GetComponent<UISprite>().color = new Color(255 / 255f, 206 / 255f, 242 / 255f, 0 / 255f);
            }
        }
        else
        {
            tab2.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
            tab2.GetComponent<BoxCollider>().enabled = false;
        }
        //if(PassGateNum == GateNum)
        if (isShowTab)
        {
            if (GateStarNum != CurStarNum)
            {
                Tab2IsEnableClick = false;
                tab2.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
                tab2.GetComponent<BoxCollider>().enabled = true;
                GameObject.Find("Tab2Button/Lock").GetComponent<UISprite>().color = new Color(255 / 255f, 206 / 255f, 242 / 255f, 255 / 255f);
            }
        }
        GateCompleteBox _curPoint = TextTranslator.instance.GetGateCompleteBox(MapPoint);
        if (_curPoint != null)
        {
            if (MapPoint != 1)
            {
                GameObject _mapWindow = GameObject.Find("MapCon");
                int _lastCreamBoxIndex = _curPoint.GateCompleteBoxID - 1;

                if (_mapWindow != null)
                {
                    int _lastGroup = TextTranslator.instance.GetGateCompleteBoxGroup(_lastCreamBoxIndex);
                    List<TextTranslator.Gate> _creamGate = TextTranslator.instance.GetGroupGate(_lastGroup, 2);
                    if (_mapWindow.GetComponent<MapWindow>().CreamSatrList[_creamGate[_creamGate.Count - 1].id - 20001] == 0)
                    {
                        IsShowLock = true;
                        GameObject.Find("Tab2Button/Lock").GetComponent<UISprite>().color = new Color(255 / 255f, 206 / 255f, 242 / 255f, 255 / 255f);
                    }
                }
            }
        }
    }

    public void ResetChanllage(int num)
    {
        this.ResetChallageNum = num;
    }


}