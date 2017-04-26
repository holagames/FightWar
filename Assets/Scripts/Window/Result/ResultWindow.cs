using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ResultWindow : MonoBehaviour
{
    public GameObject MoneyExpLevelBoard;
    public GameObject MoneyExpLevelEffect1;
    public GameObject MoneyExpLevelEffect2;
    public GameObject LabelLv;
    public GameObject LabelExp;
    public GameObject LabelMoney;

    public GameObject ItemBoard;
    public GameObject MyGridRole;
    public GameObject MyGridAward;
    public GameObject MySpriteLose;
    public GameObject RestartButton;
    public GameObject ReplayButton;
    public GameObject BackButton;

    public GameObject SpriteWin;
    public GameObject SpriteStar1;
    public GameObject SpriteStar2;
    public GameObject SpriteStar3;
    public GameObject StarGlow;
    public GameObject SpriteLose;
    public GameObject SpriteWood;
    public GameObject SpriteResultBg;
    public GameObject SpriteAwardBg;

    public GameObject ResultItem;
    public GameObject ResultAwardItem;

    public GameObject WoodSuccesEffct;
    public GameObject WoodFailedEffct;
    public GameObject SpriteMask;
    public GameObject EffectYidaoguang;


    bool _IsWin = false;
    int _Stamina = 0;
    int _Money = 0;
    int _GateExp = 0;
    int _RoleExp = 0;
    string _Reward = "";
    int _AddMoney = 0;
    string _Exp = "";
    int _StarNum;
    int _NowGateExp;


    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;

    public UILabel BasescoringLabel;
    public UILabel AdditionLabel;
    public UILabel GetLabel;
    private Vector3 ScrollViewPosition;
    public bool WoodsFailed = false;
    // Use this for initialization
    void Start()
    {
        UIManager.instance.StartGate(UIManager.GateAnchorName.finishFight);
        ScrollViewPosition = MyGridAward.transform.parent.localPosition;

        //if (GameObject.Find("RestartButton") != null)
        //{
        //    if (UIEventListener.Get(GameObject.Find("RestartButton")).onClick == null)
        //    {
        //        UIEventListener.Get(GameObject.Find("RestartButton")).onClick += delegate(GameObject go)
        //        {
        //            PictureCreater.instance.StartFight();
        //            UIManager.instance.DestroyAllPanel();
        //        };
        //    }
        //}

        if (UIEventListener.Get(RestartButton).onClick == null)
        {
            UIEventListener.Get(RestartButton).onClick += delegate(GameObject go)
            {
                if (SceneTransformer.instance.NowGateID > 20000 && SceneTransformer.instance.NowGateID < 30000)
                {
                    CharacterRecorder.instance.CurCreamGateCount -= 1;
                }
                CharacterRecorder.instance.isReStartTimes += 1;
                CharacterRecorder.instance.IsShowHeroInfo = true;
                PictureCreater.instance.ResetRandom();
                PictureCreater.instance.StartFight();
                UIManager.instance.DestroyAllPanel();
            };
        }

        if (UIEventListener.Get(ReplayButton).onClick == null)
        {
            UIEventListener.Get(ReplayButton).onClick += delegate(GameObject go)
            {
                CharacterRecorder.instance.isReStartTimes += 1;
                PictureCreater.instance.ActObject.SetActive(false);
                CharacterRecorder.instance.IsShowHeroInfo = true;
                PictureCreater.instance.IsRemember = false;
                PictureCreater.instance.IsSkip = false;
                PictureCreater.instance.RememberCount = 0;
                PictureCreater.instance.StartFight();                
                UIManager.instance.DestroyAllPanel();
            };
        }

        //if (UIEventListener.Get(GameObject.Find("BackButton")).onClick == null)
        {
            UIEventListener.Get(BackButton).onClick += delegate(GameObject go)
            {
                PictureCreater.instance.ResetRandom();
                if (CharacterRecorder.instance.lastGateID == 10001)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_711);
                }
                else if (CharacterRecorder.instance.lastGateID == 10005)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1609);
                }
                else if (CharacterRecorder.instance.lastGateID == 10006)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1804);
                }
                else if (CharacterRecorder.instance.lastGateID == 10007)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1905);
                }
                else if (CharacterRecorder.instance.lastGateID == 10008)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2105);
                }
                else if (CharacterRecorder.instance.lastGateID == 10009)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2306);
                }
                else if (CharacterRecorder.instance.lastCreamGateID == 20001 && CharacterRecorder.instance.lastGateID > 10010)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2504);
                }
                else if (CharacterRecorder.instance.lastGateID == 10011)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2605);
                }
                if (PictureCreater.instance.FightStyle == 5)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_3406);
                }
                if (SceneTransformer.instance.CheckGuideIsFinish() == false)
                {
                    if ((PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 4 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 1)
                        || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 7 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 1)
                        || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 11 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 1)
                        || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 13 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 1)
                    )
                    {
                        SceneTransformer.instance.NewGuideButtonClick();
                    }
                }
                else
                {
                    if (CharacterRecorder.instance.GuideID[1] < 3 || CharacterRecorder.instance.GuideID[3] < 3 || CharacterRecorder.instance.GuideID[25] == 2)
                    {
                        SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                    }
                    if (CharacterRecorder.instance.NowGuideID == 17 && CharacterRecorder.instance.GuideID[17] < 6)
                    {
                        CharacterRecorder.instance.GuideID[17] = 0;
                        StartCoroutine(SceneTransformer.instance.NewbieGuide());
                    }
                }
                UIManager.instance.StartGate(UIManager.GateAnchorName.passNum);
                if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) >= 18)
                {
                    if (CharacterRecorder.instance.GuideID[1] == 9)
                    {
                        CharacterRecorder.instance.GuideID[1] += 1;
                    }
                }
                if (PictureCreater.instance.FightStyle == 0)
                {
                    PictureCreater.instance.StopFight(true);
                    //  UIManager.instance.OpenPanel("MapUiWindow", true);
                    //  CharacterRecorder.instance.enterMapFromMain = true;
                    for (int i = 0; i < (CharacterRecorder.instance.isReStartTimes + 1); i++)
                    {
                        UIManager.instance.BackUI();
                    }
                }
                else if (PictureCreater.instance.FightStyle == 4)
                {
                    PictureCreater.instance.StopFight(true);
                    //  UIManager.instance.OpenPanel("MapUiWindow", true);
                    //  CharacterRecorder.instance.enterMapFromMain = true;
                    for (int i = 0; i < (CharacterRecorder.instance.isReStartTimes + 1); i++)
                    {
                        UIManager.instance.BackUI();
                    }
                }
                else if (PictureCreater.instance.FightStyle == 2)
                {
                    PictureCreater.instance.StopFight(true);
                    NetworkHandler.instance.SendProcess("1501#;");
                }
                else if (PictureCreater.instance.FightStyle == 5)
                {
                    PictureCreater.instance.StopFight(true);
                    //   UIManager.instance.OpenPanel("MapUiWindow", true);
                    //   CharacterRecorder.instance.enterMapFromMain = true;
                    for (int i = 0; i < (CharacterRecorder.instance.isReStartTimes + 1); i++)
                    {
                        UIManager.instance.BackUI();
                    }
                }
                else if (PictureCreater.instance.FightStyle == 8 || PictureCreater.instance.FightStyle == 9 || PictureCreater.instance.FightStyle == 10 || PictureCreater.instance.FightStyle == 11)
                {
                    PictureCreater.instance.StopFight(true);
                    //  UIManager.instance.OpenPanel("EverydayWindow", true);
                    for (int i = 0; i < (CharacterRecorder.instance.isReStartTimes + 1); i++)
                    {
                        UIManager.instance.BackUI();
                    }
                }
                else if (PictureCreater.instance.FightStyle == 12)
                {
                    PictureCreater.instance.StopFight(true);
                    for (int i = 0; i < (CharacterRecorder.instance.isReStartTimes + 1) + 1; i++)
                    {
                        UIManager.instance.BackUI();
                    }
                }
                else if (PictureCreater.instance.FightStyle == 17)
                {
                    PictureCreater.instance.StopFight(true);
                    UIManager.instance.BackUI();
                }
                CharacterRecorder.instance.isReStartTimes = 0;
                CharacterRecorder.instance.IsShowHeroInfo = false;
            };
        }
        if (AdditionLabel.text == "")
        {
            if (WoodsFailed == false)
            {
                BackButton.SetActive(false);
            }
        }
        if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 7 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 1)
        {
        }
    }

    public void SetWood(int NowFloor, int NowHard, int NowStar)
    {
        TowerData Tower = TextTranslator.instance.GetTowerByID(NowFloor);
        if (NowHard == 1)
        {
            BasescoringLabel.text = Tower.GatePoint1.ToString();
        }
        else if (NowHard == 2)
        {
            BasescoringLabel.text = Tower.GatePoint2.ToString();
        }
        else if (NowHard == 3)
        {
            BasescoringLabel.text = Tower.GatePoint3.ToString();
        }

        if (NowStar == 1)
        {
            AdditionLabel.text = "1";
            GetLabel.text = ((int)Tower.GateStarRate1 * int.Parse(BasescoringLabel.text)).ToString();
        }
        else if (NowStar == 2)
        {

            AdditionLabel.text = "1.5";
            GetLabel.text = ((int)(1.5f * int.Parse(BasescoringLabel.text))).ToString();
        }
        else if (NowStar == 3)
        {

            AdditionLabel.text = "2.5";
            GetLabel.text = ((int)(2.5f * int.Parse(BasescoringLabel.text))).ToString();
        }
        _StarNum = NowStar;

        SpriteWood.SetActive(true);
        EffectYidaoguang.SetActive(true);
        SpriteWood.transform.Find("Sucessful").gameObject.SetActive(true);
        WoodSuccesEffct.SetActive(true);
        BackButton.SetActive(true);
        GameObject.Find("RestartButton").SetActive(false);
        RestartButton.transform.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
        //BackButton.transform.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        //StartCoroutine(AutoCloseWoodResultWindow());
    }
    public void SetWoodFailed()
    {
        WoodsFailed = true;
        SpriteWood.SetActive(true);
        WoodFailedEffct.SetActive(true);
        SpriteWood.transform.Find("Failed").gameObject.SetActive(true);
        BackButton.SetActive(true);
        GameObject.Find("RestartButton").SetActive(false);
        RestartButton.transform.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
        //StartCoroutine(AutoCloseWoodResultWindow());
    }
    public void Init(bool IsWin, int Stamina, int Money, int GateExp, int RoleExp, string Reward, int AddMoney, string Exp, int StarNum)
    {

        if (IsWin && SceneTransformer.instance.NowGateID < 30000)
        {
            UIManager.instance.UmengFinishLevel(SceneTransformer.instance.NowGateID.ToString());
        }
        else if (!IsWin && SceneTransformer.instance.NowGateID < 30000)
        {
            UIManager.instance.UmengFailLevel(SceneTransformer.instance.NowGateID.ToString());
        }

        _IsWin = IsWin;
        _Stamina = Stamina;
        _Money = Money;
        _GateExp = GateExp;
        _RoleExp = RoleExp;
        _Reward = Reward;
        _AddMoney = AddMoney;
        _Exp = Exp;
        _StarNum = StarNum;
        if (StarNum == 3)
        {
            UIManager.instance.StartGate(UIManager.GateAnchorName.threeStarNum);
        }
        if (_IsWin && CharacterRecorder.instance.lastGateID == 10010)
        {
            Debug.Log("进入精英关新手引导：" + CharacterRecorder.instance.lastGateID);
            StartCoroutine(SceneTransformer.instance.NewbieGuide());
        }
        if (GameObject.Find("FightWindow") != null) //结算关闭所有暂停
        {
            GameObject.Find("FightWindow").GetComponent<FightWindow>().ExitButton.SetActive(false);
        }
        if (PictureCreater.instance.FightStyle != 2)
        {
            CharacterRecorder.instance.AddMoney(_AddMoney);
            CharacterRecorder.instance.SetStamina(_Stamina);
            _NowGateExp = TextTranslator.instance.GetGateByID(SceneTransformer.instance.NowGateID).playerExpBonus;//GateExp - CharacterRecorder.instance.exp;
            CharacterRecorder.instance.exp = GateExp;
        }
        State1();
    }

    IEnumerator ShowEffect()
    {
        yield return new WaitForSeconds(2f);
        GameObject.Find("SpriteWin/SpriteStarblack1").GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        GameObject.Find("SpriteWin/SpriteStarblack2").GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        GameObject.Find("SpriteWin/SpriteStarblack3").GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        if (_StarNum == 1)
        {
            SpriteStar1.SetActive(true);
            AudioEditer.instance.PlayOneShot("ui_start");
            yield return new WaitForSeconds(0.5f);
        }
        else if (_StarNum == 2)
        {
            SpriteStar1.SetActive(true);
            AudioEditer.instance.PlayOneShot("ui_start");
            yield return new WaitForSeconds(0.5f);
            SpriteStar2.SetActive(true);
            AudioEditer.instance.PlayOneShot("ui_start");
            yield return new WaitForSeconds(0.5f);
        }
        else if (_StarNum == 3)
        {
            SpriteStar1.SetActive(true);
            AudioEditer.instance.PlayOneShot("ui_start");
            yield return new WaitForSeconds(0.5f);
            SpriteStar2.SetActive(true);
            AudioEditer.instance.PlayOneShot("ui_start");
            yield return new WaitForSeconds(0.5f);
            SpriteStar3.SetActive(true);
            AudioEditer.instance.PlayOneShot("ui_start");
            yield return new WaitForSeconds(0.5f);
            StarGlow.SetActive(true);
        }
        else
        {
            Debug.Log("111111");
        }
    }

    void State1()
    {
        if (_IsWin)
        {
            if (PictureCreater.instance.FightStyle != 2)
            {
                SpriteWin.SetActive(true);
                AudioEditer.instance.PlayOneShot("MissionComplete");
            }
            StartCoroutine(ShowEffect());
            SpriteLose.SetActive(false);
            Invoke("State2", 1f);
        }
        else
        {
            if (PictureCreater.instance.FightStyle != 2)
            {
                SpriteLose.SetActive(true);
                AudioEditer.instance.PlayOneShot("MissionFailed");
            }
            SpriteWin.SetActive(false);

            //StartCoroutine(ShowLoseEffect());
            Invoke("ShowLoseEffect", 2f);
        }
        //Invoke("State2_1", 1f);
        //Invoke("State2", 1f);
    }
    void ShowLoseEffect()//失败结果退出
    {
        if (PictureCreater.instance.IsSkip && !PictureCreater.instance.IsFireSkill)
        {
            ReplayButton.SetActive(true);
        }
        BackButton.SetActive(true);
        if (PictureCreater.instance.FightStyle == 0)
        {
            RestartButton.SetActive(true);
            RestartButton.transform.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        }
        else
        {
            RestartButton.SetActive(false);
        }
        //GameObject.Find("SpriteMask").GetComponent<UISprite>().color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 150 / 255f);
        SpriteMask.GetComponent<UISprite>().color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 150 / 255f);
        //BackButton.transform.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);

        MySpriteLose.SetActive(true);

        if (GameObject.Find("SpriteLose/SpriteLoseBG") != null)
        {
            if (UIEventListener.Get(GameObject.Find("SpriteLose/SpriteLoseBG/ButtonZhaomu")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("SpriteLose/SpriteLoseBG/ButtonZhaomu")).onClick += delegate(GameObject go)
                {
                    //UIManager.instance.DestroyAllPanel();
                    //UIManager.instance.BackUI();
                    PictureCreater.instance.StopFight(true);
                    UIManager.instance.OpenPanel("MainWindow", true);
                    CharacterRecorder.instance.enterMapFromMain = false;
                    UIManager.instance.OpenPanel("GachaWindow", true);
                };
            }
        }
        if (GameObject.Find("SpriteLose/SpriteLoseBG") != null)
        {
            if (UIEventListener.Get(GameObject.Find("SpriteLose/SpriteLoseBG/ButtonZhuangbei")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("SpriteLose/SpriteLoseBG/ButtonZhuangbei")).onClick += delegate(GameObject go)
                {
                    //UIManager.instance.DestroyAllPanel();
                    //PictureCreater.instance.StopFight(true);
                    PictureCreater.instance.StopFight(false);
                    UIManager.instance.OpenPanel("MainWindow", true);

                    CharacterRecorder.instance.enterMapFromMain = false;
                    GameObject rw = UIManager.instance.OpenPanel("StrengEquipWindow", true);
                    //rw.GetComponent<RoleWindow>().curHero = CharacterRecorder.instance.ownedHeroList[0];
                    //rw.GetComponent<RoleWindow>().CharacterRoleID = CharacterRecorder.instance.ownedHeroList[0].characterRoleID;
                   // GameObject.Find("StrengEquipWindow").GetComponent<StrengEquipWindow>().SetTab(2);
                    DestroyImmediate(GameObject.Find("FightScene"));
                };
            }
        }
        if (GameObject.Find("SpriteLose/SpriteLoseBG") != null)
        {
            if (UIEventListener.Get(GameObject.Find("SpriteLose/SpriteLoseBG/ButtonShenping")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("SpriteLose/SpriteLoseBG/ButtonShenping")).onClick += delegate(GameObject go)
                {
                    //UIManager.instance.DestroyAllPanel();
                    //PictureCreater.instance.StopFight(true);
                    if (CharacterRecorder.instance.GuideID[59] == 1)
                    {
                        SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                    }
                    PictureCreater.instance.StopFight(false);
                    UIManager.instance.OpenPanel("MainWindow", true);

                    CharacterRecorder.instance.enterMapFromMain = false;
                    GameObject rw= UIManager.instance.OpenPanel("RoleWindow", true);
                    //rw.GetComponent<RoleWindow>().curHero = CharacterRecorder.instance.ownedHeroList[0];
                    //rw.GetComponent<RoleWindow>().CharacterRoleID = CharacterRecorder.instance.ownedHeroList[0].characterRoleID;
                    rw.GetComponent<RoleWindow>().SetTab(4);
                    DestroyImmediate(GameObject.Find("FightScene"));
                };
            }
        }
        if (GameObject.Find("SpriteLose/SpriteLoseBG") != null)
        {
            if (UIEventListener.Get(GameObject.Find("SpriteLose/SpriteLoseBG/ButtonZhanshu")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("SpriteLose/SpriteLoseBG/ButtonZhanshu")).onClick += delegate(GameObject go)
                {
                    //UIManager.instance.DestroyAllPanel();
                    //PictureCreater.instance.StopFight(true);
                    PictureCreater.instance.StopFight(true);
                    UIManager.instance.OpenPanel("MainWindow", true);

                    CharacterRecorder.instance.enterMapFromMain = false;
                    UIManager.instance.OpenPanel("TaskWindow", true);

                };
            }
        }
    }
    void State2()
    {
        //MoneyExpLevelEffect1.SetActive(true);
        //Invoke("State2_2", 0.6f);
        //MoneyExpLevelEffect2.SetActive(true);
        Invoke("State3", 1f);
    }
    void State3()
    {

        BackButton.SetActive(true);
        RestartButton.SetActive(true);
        if(PictureCreater.instance.IsSkip && !PictureCreater.instance.IsFireSkill)
        {
            ReplayButton.SetActive(true);
        }
        MouseClick _mouseClick = GameObject.Find("MainCamera").GetComponent<MouseClick>();
        Debug.Log("这里是资源不：" + _mouseClick.IsResource);
        bool isShowReStartBtn = true;
        if (SceneTransformer.instance.NowGateID > 20000 && SceneTransformer.instance.NowGateID < 30000)
        {
            if (CharacterRecorder.instance.stamina < 12)
            {
                isShowReStartBtn = false;
            }

        }
        else if (SceneTransformer.instance.NowGateID > 10000)
        {
            if (CharacterRecorder.instance.stamina < 6)
            {
                isShowReStartBtn = false;
            }
        }
        if (_mouseClick.IsWorldEvev || _mouseClick.IsResource || _mouseClick.IsEnemy || _mouseClick.IsAction || CharacterRecorder.instance.CurCreamGateCount <= 1 || !isShowReStartBtn)
        {
            //if (_mouseClick.IsResource)
            //{
            //    SpriteAwardBg.GetComponent<UISprite>().spriteName = "resour_word";
            //    SpriteAwardBg.GetComponent<UISprite>().MakePixelPerfect();
            //}
            if (_mouseClick.IsAction && _IsWin)
            {
                CharacterRecorder.instance.sprite -= 2;
            }
            RestartButton.SetActive(false);
        }
        SpriteMask.GetComponent<UISprite>().color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 150 / 255f);
        //GameObject.Find("SpriteMask").GetComponent<UISprite>().color = new Color(0 / 255f, 0 / 255f, 0 / 255f, 150 / 255f);
        RestartButton.transform.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        //BackButton.transform.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);

        MoneyExpLevelEffect2.SetActive(true);
        SpriteResultBg.SetActive(true);
        EffectYidaoguang.SetActive(true);
        //MoneyExpLevelEffect2.SetActive(false);
        string[] expsSplit = _Exp.Split('!');
        for (int i = 0; i < expsSplit.Length - 1; i++)
        {
            string[] expSplit = expsSplit[i].Split('$');
            GameObject go = NGUITools.AddChild(MyGridRole, ResultItem);
            go.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
            //go.GetComponent<UIPanel>().depth = 10 + i;
            go.name = "GridRole" + expSplit[0];
            go.transform.localPosition = new Vector3(-1500, 0, 0);
            go.GetComponent<ResultItem>().Init(int.Parse(expSplit[0]), int.Parse(expSplit[1]), float.Parse(expSplit[2]), float.Parse(expSplit[3]), _RoleExp);
            go.AddComponent<TweenPosition>();
            TweenPosition tp = go.GetComponent<TweenPosition>();
            tp.from = new Vector3(-1500, 0, 0);
            tp.to = new Vector3(i * 140, 0, 0);
            tp.delay = 0.01f + i / 5f;
            tp.PlayForward();
        }
        //MyGridRole.GetComponent<UIGrid>().Reposition();
        //MoneyExpLevelBoard.SetActive(true);
        //SpriteAwardBg.SetActive(true);
        Invoke("State4", 2f);
    }
    void State4()
    {
        MoneyExpLevelBoard.SetActive(true);
        MoneyExpLevelEffect1.SetActive(true);
        LabelLv.GetComponent<UILabel>().text = "Lv." + CharacterRecorder.instance.level.ToString();
        StartCoroutine(AddNumber1(LabelMoney.GetComponent<UILabel>(), _AddMoney));
        StartCoroutine(AddNumber1(LabelExp.GetComponent<UILabel>(), _NowGateExp));
        if (!MoneyExpLevelEffect1.GetComponent<UISpriteAnimation>().isPlaying)
        {
            MoneyExpLevelEffect1.SetActive(false);
        }
        //Invoke("State3", 1f);
        Invoke("State5", 1f);
    }
    IEnumerator AddNumber1(UILabel label, int MaxNumber)
    {
        int count = 0;
        while (count <= MaxNumber)
        {
            count += 20;//5
            yield return new WaitForSeconds(0.01f);
            if (count >= MaxNumber)
            {
                count = MaxNumber;
            }
            label.text = "+" + count.ToString();
        }
    }

    void State5()
    {
        SpriteAwardBg.SetActive(true);
        ItemBoard.SetActive(true);
        string[] dataSplit = _Reward.Split('!');
        for (int i = 0; i < dataSplit.Length - 1; i++)
        {
            string[] awardSplit = dataSplit[i].Split('$');
            GameObject go = NGUITools.AddChild(MyGridAward, ResultAwardItem);
            go.name = "ResultAwardItem" + awardSplit[0];
            TextTranslator.ItemInfo miteminfo = TextTranslator.instance.GetItemByItemCode(int.Parse(awardSplit[0]));
            go.GetComponent<UISprite>().spriteName = "Grade" + miteminfo.itemGrade.ToString();
            //SetColor(go, miteminfo.itemGrade);
            go.GetComponent<ItemExplanation>().SetAwardItem(int.Parse(awardSplit[0]), int.Parse(awardSplit[1]));
            if (int.Parse(awardSplit[0]) > 80000 && int.Parse(awardSplit[0]) < 90000)
            {
                go.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = (int.Parse(awardSplit[0]) / 10 * 10 - 30000).ToString();//(int.Parse(awardSplit[0]) - 30000).ToString();
                go.transform.Find("LabelCount").gameObject.GetComponent<UILabel>().text = awardSplit[1];
                go.transform.Find("SpritePiece").gameObject.SetActive(true);
            }
            else if (int.Parse(awardSplit[0]) > 70000 && int.Parse(awardSplit[0]) < 79999)
            {
                go.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
                go.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = (int.Parse(awardSplit[0]) - 10000).ToString();
                go.transform.Find("LabelCount").gameObject.GetComponent<UILabel>().text = awardSplit[1];
                go.transform.Find("SpritePiece").gameObject.SetActive(true);
            }
            else if (int.Parse(awardSplit[0]) > 20000 && int.Parse(awardSplit[0]) < 30000)
            {
                go.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(int.Parse(awardSplit[0])).picID.ToString();
                go.transform.Find("LabelCount").gameObject.GetComponent<UILabel>().text = awardSplit[1];
                go.transform.Find("SpritePiece").gameObject.SetActive(false);
            }
            else
            {
                go.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = awardSplit[0];
                go.transform.Find("LabelCount").gameObject.GetComponent<UILabel>().text = awardSplit[1];
                go.transform.Find("SpritePiece").gameObject.SetActive(false);
            }
        }
        MyGridAward.GetComponent<UIGrid>().Reposition();

        RestartButton.GetComponent<BoxCollider>().size = new Vector3(138, 138, 0);
        GameObject.Find("BackButton").GetComponent<BoxCollider>().size = new Vector3(138, 138, 0);
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
        }
    }


    /// <summary>
    /// 丛林自动退出结果
    /// </summary>
    private IEnumerator AutoCloseWoodResultWindow() 
    {
        yield return new WaitForSeconds(3f);
        if (PlayerPrefs.GetInt("Automaticbrushfield" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID")) == 1) 
        {
            PictureCreater.instance.ResetRandom();
            if (CharacterRecorder.instance.lastGateID == 10001)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_711);
            }
            else if (CharacterRecorder.instance.lastGateID == 10005)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1609);
            }
            else if (CharacterRecorder.instance.lastGateID == 10006)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1804);
            }
            else if (CharacterRecorder.instance.lastGateID == 10007)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1905);
            }
            else if (CharacterRecorder.instance.lastGateID == 10008)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2105);
            }
            else if (CharacterRecorder.instance.lastGateID == 10009)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2306);
            }
            else if (CharacterRecorder.instance.lastCreamGateID == 20001 && CharacterRecorder.instance.lastGateID > 10010)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2504);
            }
            else if (CharacterRecorder.instance.lastGateID == 10011)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2605);
            }
            if (PictureCreater.instance.FightStyle == 5)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_3406);
            }
            if (SceneTransformer.instance.CheckGuideIsFinish() == false)
            {
                if ((PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 4 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 1)
                    || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 7 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 1)
                    || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 11 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 1)
                    || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 13 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 1)
                )
                {
                    SceneTransformer.instance.NewGuideButtonClick();
                }
            }
            else
            {
                if (CharacterRecorder.instance.GuideID[1] < 3 || CharacterRecorder.instance.GuideID[3] < 3 || CharacterRecorder.instance.GuideID[25] == 2)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
                if (CharacterRecorder.instance.NowGuideID == 17 && CharacterRecorder.instance.GuideID[17] < 6)
                {
                    CharacterRecorder.instance.GuideID[17] = 0;
                    StartCoroutine(SceneTransformer.instance.NewbieGuide());
                }
            }
            UIManager.instance.StartGate(UIManager.GateAnchorName.passNum);
            if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) >= 18)
            {
                if (CharacterRecorder.instance.GuideID[1] == 9)
                {
                    CharacterRecorder.instance.GuideID[1] += 1;
                }
            }
            if (PictureCreater.instance.FightStyle == 0)
            {
                PictureCreater.instance.StopFight(true);
                //  UIManager.instance.OpenPanel("MapUiWindow", true);
                //  CharacterRecorder.instance.enterMapFromMain = true;
                for (int i = 0; i < (CharacterRecorder.instance.isReStartTimes + 1); i++)
                {
                    UIManager.instance.BackUI();
                }
            }
            else if (PictureCreater.instance.FightStyle == 4)
            {
                PictureCreater.instance.StopFight(true);
                //  UIManager.instance.OpenPanel("MapUiWindow", true);
                //  CharacterRecorder.instance.enterMapFromMain = true;
                for (int i = 0; i < (CharacterRecorder.instance.isReStartTimes + 1); i++)
                {
                    UIManager.instance.BackUI();
                }
            }
            else if (PictureCreater.instance.FightStyle == 2)
            {
                PictureCreater.instance.StopFight(true);
                NetworkHandler.instance.SendProcess("1501#;");
            }
            else if (PictureCreater.instance.FightStyle == 5)
            {
                PictureCreater.instance.StopFight(true);
                //   UIManager.instance.OpenPanel("MapUiWindow", true);
                //   CharacterRecorder.instance.enterMapFromMain = true;
                for (int i = 0; i < (CharacterRecorder.instance.isReStartTimes + 1); i++)
                {
                    UIManager.instance.BackUI();
                }
            }
            else if (PictureCreater.instance.FightStyle == 8 || PictureCreater.instance.FightStyle == 9 || PictureCreater.instance.FightStyle == 10 || PictureCreater.instance.FightStyle == 11)
            {
                PictureCreater.instance.StopFight(true);
                //  UIManager.instance.OpenPanel("EverydayWindow", true);
                for (int i = 0; i < (CharacterRecorder.instance.isReStartTimes + 1); i++)
                {
                    UIManager.instance.BackUI();
                }
            }
            else if (PictureCreater.instance.FightStyle == 12)
            {
                PictureCreater.instance.StopFight(true);
                for (int i = 0; i < (CharacterRecorder.instance.isReStartTimes + 1) + 1; i++)
                {
                    UIManager.instance.BackUI();
                }
            }
            CharacterRecorder.instance.isReStartTimes = 0;
            CharacterRecorder.instance.IsShowHeroInfo = false;
        }
    }
}
