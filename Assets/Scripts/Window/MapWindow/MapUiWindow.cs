using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MapUiWindow : MonoBehaviour
{
    public GameObject GrabTerritory;
    public GameObject PatrolLong;
    public GameObject PatrolItem;
    public GameObject GateInfoWindow;
    public GameObject MapWindowObj;
    public GameObject PatrolFight;
    public GameObject BaseButton;
    public GameObject ChallengeButton;
    public GameObject WordEventObj;
    public GameObject EnemyObj;
    public GameObject ActionEventObj;

    public GameObject SpriteArrow1;
    public GameObject SpriteArrow2;
    public GameObject SpriteArrow3;
    public GameObject SpriteArrow4;
    public GameObject SpriteArrow5;
    public GameObject SpriteArrow6;
    public GameObject SpriteArrow7;
    public GameObject SpriteArrow8;
    public WorldEventList worldList;
    public GameObject TimerUpdateObj;
    public GameObject MapNameShowObj;

    public UILabel LabelStar;
    public UILabel LabelChapter;
    public GameObject ResourceBtn;
    public GameObject ResourcesBoard;
    public UIAtlas GrabItemAtlas;

    public GameObject WordButton;
    public GameObject ResourceButton;
    public GameObject MasterButton;
    public GameObject BackButton;

    public GameObject PassBox;
    public GameObject mTopContent;
    public bool IsWorldBtnClick = false;

    public bool IsMove = false;
    GameObject MapNameObj;
    public int GetRewardGroupId = 0;
    string WorldIconTimeStr;

    private GameObject MapObjectObj;
    bool isShowResource = true;
    List<GameObject> _timerUpdates = new List<GameObject>();
    public bool IsClearResourceTimes;//是否清空大地图上的时间
    int OneTime;
    // Use this for initialization


    public GameObject Cup1;
    public GameObject Cup2;
    public GameObject Cup3;
    public GameObject Cup4;

    public UISprite KongBai;
    void Awake()
    {
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
            KongBai.width = (int)(1200 * (System.Convert.ToSingle(Screen.width) / Screen.height) / (System.Convert.ToSingle(960) / 640));
        }
    }

    
    void Start()
    {
        if (SystemInfo.systemMemorySize < 700)
        {
            if (TextTranslator.instance.ReloadCount > 10)
            {
                TextTranslator.instance.ReloadCount = 1;
                TextTranslator.instance.DoGC();
                System.GC.Collect();
            }
        }
        else if (SystemInfo.systemMemorySize < 1000)
        {
            if (TextTranslator.instance.ReloadCount > 25)
            {
                TextTranslator.instance.ReloadCount = 1;
                TextTranslator.instance.DoGC();
                System.GC.Collect();
            }
        }
        UIManager.instance.CountSystem(UIManager.Systems.世界战斗);
        UIManager.instance.UpdateSystems(UIManager.Systems.世界战斗);

        CharacterRecorder.instance.IsOpenEventList = false;
        //if (CharacterRecorder.instance.lastGateID == 10010)
        //{
        //    Debug.Log("通关第十关启动新手引导： " + CharacterRecorder.instance.lastGateID);
        //    StartCoroutine(SceneTransformer.instance.NewbieGuide());
        //}
        if (CharacterRecorder.instance.lastGateID > 10033)
        {
            GameObject.Find("WordButton").transform.FindChild("SpriteSuo").gameObject.SetActive(false);
        }
        else
        {
            GameObject.Find("WordButton").transform.FindChild("SpriteRedDian").gameObject.SetActive(false);
        }
        NetworkHandler.instance.SendProcess("2201#;");
        NetworkHandler.instance.SendProcess("2016#0;");
        NetworkHandler.instance.SendProcess("6017#;");
        MapNameObj = GameObject.Find("MapUiWindow/All/MapName");
        if (CharacterRecorder.instance.lastGateID >= 10038)
        {
            GameObject.Find("ResourceButton").transform.FindChild("SpriteSuo").gameObject.SetActive(false);
            NetworkHandler.instance.SendProcess("1135#;");
        }
        AudioEditer.instance.PlayLoop("Gate");
        SceneTransformer.instance.ShowMainScene(true);
        MapObjectObj = SceneTransformer.instance.MainScene;
        NetworkHandler.instance.SendProcess("2001#;");

        if (MapObjectObj != null)
        {
            MapObjectObj.transform.Find("MapCon").GetComponent<MapWindow>().ShowPatrol();
        }
        GuideMoveSheXiangJi();
        //else if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 14)
        //{
        //    CharacterRecorder.instance.gotoGateID = 10003;
        //    MapObjectObj.transform.Find("MapCon").GetComponent<MapWindow>().SetMapTypeUpdate();
        //}

        //if (UIEventListener.Get(WordButton).onClick == null)
        {
            if (UIEventListener.Get(WordButton).onClick != null)
            {
                Debug.Log(UIEventListener.Get(WordButton).onClick.GetInvocationList().Length + "  **************");
            }
            UIEventListener.Get(WordButton).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.GuideID[18] != 2)
                {
                    TheWorldBtnClick();
                }
                if (CharacterRecorder.instance.GuideID[18] == 1)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }

            };
        }

        //if (UIEventListener.Get(WordButton).onClick != null)
        //{
        //    Debug.Log(UIEventListener.Get(WordButton).onClick.GetInvocationList().Length + "  **************");
        //}

        //if (UIEventListener.Get(ResourceButton).onClick == null)
        {
            UIEventListener.Get(ResourceButton).onClick += delegate(GameObject go)
            {
                GameObject wordEven = GameObject.Find("MapUiWindow/All/WordEvent");
                if (CharacterRecorder.instance.lastGateID < 10038)
                {
                    UIManager.instance.OpenPromptWindow("长官，通38关开放该功能！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                if (mTopContent.activeSelf)
                {
                    CloseTopContent();
                }
                ChoiceWindow(7);
                //if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) > 12)
                //{
                //    GameObject _mapMask = GameObject.Find("MapMask");
                //    if (_mapMask != null)
                //    {
                //        _mapMask.SetActive(false);
                //    }
                //}
                Transform _mapMask = GameObject.Find("MapObject").transform.Find("MapMask");
                if (wordEven != null)
                {
                    wordEven.GetComponent<WordEvent>().EscOnClick();
                }
                if (!isShowResource)
                {
                    _mapMask.gameObject.SetActive(false);
                    isShowResource = true;
                    ResourcesBoard.GetComponent<ResourceBoard>().MyGridClear();
                    ResourcesBoard.SetActive(false);
                    //ResourceBtn.GetComponent<TweenPosition>().PlayReverse();
                    //IsMove = false;
                }
                else
                {

                    _mapMask.gameObject.SetActive(true);
                    //ResourceBtn.GetComponent<TweenPosition>().PlayReverse();
                    //IsMove = false;
                    isShowResource = false;
                    GateInfoWindow.SetActive(false);
                    ResourcesBoard.SetActive(true);
                    NetworkHandler.instance.SendProcess("1135#;");
                    if (CharacterRecorder.instance.GuideID[19] == 1 || CharacterRecorder.instance.GuideID[17] == 0)
                    {
                        SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                    }
                }
            };
        }

        if (UIEventListener.Get(MasterButton).onClick == null)
        {
            UIEventListener.Get(MasterButton).onClick = delegate(GameObject go)
            {
                //if (GameObject.Find("EnemyinvasionWindow") != null)
                //{
                //    GameObject.Find("EnemyinvasionWindow").SetActive(false);
                //}
                if (GateInfoWindow.activeSelf)
                {
                    if (GateInfoWindow.GetComponent<MapGateInfoWindow>().CheckGateId(CharacterRecorder.instance.lastGateID))
                    {
                        UIManager.instance.OpenPromptWindow("长官，当前已是最新关卡。", PromptWindow.PromptType.Hint, null, null);
                        return;
                    }
                }
                if (mTopContent.activeSelf)
                {
                    CloseTopContent();
                }
                ChoiceWindow(7);
                GameObject _mainCamera = GameObject.Find("MainCamera");
                _mainCamera.GetComponent<MouseClick>().IsResource = false;
                _mainCamera.GetComponent<MouseClick>().IsWorldEvev = false;
                _mainCamera.GetComponent<MouseClick>().IsEnemy = false;
                _mainCamera.GetComponent<MouseClick>().IsAction = false;
                if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) > 12)
                {
                    GameObject _mapMask = GameObject.Find("MapMask");
                    if (_mapMask != null)
                    {
                        _mapMask.SetActive(false);
                    }
                }
                GameObject _worldEvenWindow = GameObject.Find("WorldEvenWindow");
                if (_worldEvenWindow != null)
                {
                    _worldEvenWindow.SetActive(false);
                }
                isShowResource = true;
                ThreadBtnClick();
            };
        }

        if (UIEventListener.Get(BackButton).onClick == null)
        {
            UIEventListener.Get(BackButton).onClick += delegate(GameObject go)
            {
                if (SceneTransformer.instance.CheckGuideIsFinish() == false)
                {
                    if ((PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 4 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 3)
                       || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 8 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 2)
                      || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 14 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 1)
                      )
                    {
                        SceneTransformer.instance.NewGuideButtonClick();
                    }
                }
                else
                {
                    if (CharacterRecorder.instance.GuideID[26] == 1 || CharacterRecorder.instance.GuideID[28] == 1 || CharacterRecorder.instance.GuideID[37] == 1 || CharacterRecorder.instance.GuideID[21] == 4 || CharacterRecorder.instance.GuideID[25] == 1
                        || CharacterRecorder.instance.GuideID[29] == 3 || CharacterRecorder.instance.GuideID[20] == 3 || CharacterRecorder.instance.GuideID[9] == 4 || CharacterRecorder.instance.GuideID[10] == 4
                        || CharacterRecorder.instance.GuideID[11] == 4 || CharacterRecorder.instance.GuideID[30] == 3 || CharacterRecorder.instance.GuideID[12] == 4 || CharacterRecorder.instance.GuideID[38] == 4
                        || CharacterRecorder.instance.GuideID[13] == 4 || CharacterRecorder.instance.GuideID[31] == 3 || CharacterRecorder.instance.GuideID[14] == 4 || CharacterRecorder.instance.GuideID[34] == 1 || CharacterRecorder.instance.GuideID[36] == 4
                        || CharacterRecorder.instance.GuideID[58] == 1)
                    {
                        SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                    }
                }
                GameObject _mapMask = GameObject.Find("MapObject/MapMask");
                if (_mapMask != null)
                {
                    _mapMask.SetActive(false);
                }
                MouseClick _mouse = GameObject.Find("MainCamera").GetComponent<MouseClick>();
                _mouse.IsResource = false;
                _mouse.IsWorldEvev = false;
                _mouse.IsEnemy = false;
                _mouse.IsAction = false;

                if (CharacterRecorder.instance.enterMapFromMain || GameObject.Find("GuideArrow") != null)
                {
                    UIManager.instance.OpenPanel("MainWindow", true);
                    CharacterRecorder.instance.enterMapFromMain = false;
                }
                else
                {

                    UIManager.instance.BackUI();
                    GameObject obj = GameObject.Find("MapObject");
                    if (obj != null)
                    {
                        obj.SetActive(false);
                    }
                }
                CharacterRecorder.instance.gotoGateID = -1;
                CharacterRecorder.instance.IsOpenCreamCN = false;

                if (TextTranslator.instance.isUpdateBag)
                {
                    NetworkHandler.instance.SendProcess("5001#;");
                }
            };
        }
        //LabelChapter.text = "第" + CharacterRecorder.instance.mapID.ToString() + "章";
        //ShowWorldEventIcon();
        //InitWorldEvenList();
        //if (CharacterRecorder.instance.GuideID[25] != 0 && CharacterRecorder.instance.GuideID[27] == 0)
        //{
        //    CharacterRecorder.instance.gotoGateID = 10001;
        //    Debug.Log("精英关引导进入关卡：" + CharacterRecorder.instance.gotoGateID);
        //    MapObjectObj.transform.Find("MapCon").GetComponent<MapWindow>().SetMapTypeUpdate();
        //    CharacterRecorder.instance.GuideID[27] = 1;
        //    StartCoroutine(SceneTransformer.instance.NewbieGuide());
        //}
        //if ((CharacterRecorder.instance.GuideID[26] == 0 && CharacterRecorder.instance.GuideID[1] > 0))
        //{
        //    StartCoroutine(SceneTransformer.instance.NewbieGuide());
        //}
    }
    /// <summary>
    /// 新手引导刷新摄像机
    /// </summary>
    public void GuideMoveSheXiangJi()
    {
        if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 5 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 2)
        {
            Debug.LogError("GGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGGG");
            CharacterRecorder.instance.gotoGateID = 10001;
            MapObjectObj.transform.Find("MapCon").GetComponent<MapWindow>().SetMapTypeUpdate();
        }
        else if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 10 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) >= 1)
        {
            CharacterRecorder.instance.gotoGateID = 10003;
            MapObjectObj.transform.Find("MapCon").GetComponent<MapWindow>().SetMapTypeUpdate();
        }
        else if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 12 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) >= 1)
        {
            CharacterRecorder.instance.gotoGateID = 10004;
            MapObjectObj.transform.Find("MapCon").GetComponent<MapWindow>().SetMapTypeUpdate();
        }
        //else if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 6 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) >= 1)
        //{
        //    CharacterRecorder.instance.gotoGateID = 10002;
        //    MapObjectObj.transform.Find("MapCon").GetComponent<MapWindow>().SetMapTypeUpdate();
        //}

        if (CharacterRecorder.instance.GuideID[37] == 0 && CharacterRecorder.instance.GuideID[15] > 3 && CharacterRecorder.instance.lastGateID == 10009)
        {
            CharacterRecorder.instance.gotoGateID = 10008;
            MapObjectObj.transform.Find("MapCon").GetComponent<MapWindow>().SetMapTypeUpdate();
        }
        if (CharacterRecorder.instance.GuideID[28] == 1)
        {
            CharacterRecorder.instance.gotoGateID = 10007;
            MapObjectObj.transform.Find("MapCon").GetComponent<MapWindow>().SetMapTypeUpdate();
        }
    }
    public void TheWorldBtnClick()
    {
        GameObject wordEven = GameObject.Find("MapUiWindow/All/WordEvent");
        if (CharacterRecorder.instance.level < 5 || CharacterRecorder.instance.lastGateID < 10034)
        {
            UIManager.instance.OpenPromptWindow("长官，通关33关开放该功能！", PromptWindow.PromptType.Hint, null, null);
            return;
        }
        if (mTopContent.activeSelf)
        {
            CloseTopContent();
        }
        IsWorldBtnClick = true;
        ChoiceWindow(7);
        //if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) > 12)
        //{
        //    GameObject _mapMask = GameObject.Find("MapMask");
        //    if (_mapMask != null)
        //    {
        //        _mapMask.SetActive(false);
        //    }
        //}
        Transform _mapMask = GameObject.Find("MapObject").transform.Find("MapMask");
        if (wordEven != null)
        {
            //
            if (GameObject.Find("WordEvent/EventList") == null)
            {
                _mapMask.gameObject.SetActive(false);
                isShowResource = true;
                wordEven.transform.Find("EventList").gameObject.SetActive(true);
                wordEven.transform.Find("WorldEvenWindow").gameObject.SetActive(false);
                //ResourceBtn.GetComponent<TweenPosition>().PlayForward();

                //IsMove = true;
            }
            else//收
            {
                _mapMask.gameObject.SetActive(false);
                GameObject _worldEvenWindow = GameObject.Find("WorldEvenWindow");
                if (_worldEvenWindow != null)
                {
                    _worldEvenWindow.SetActive(false);
                }
                wordEven.GetComponent<WordEvent>().EscOnClick();
                ChoiceWindow(7);
                ResourcesBoard.SetActive(false);
                //ResourceBtn.GetComponent<TweenPosition>().PlayReverse();

                //IsMove = false;
            }
        }
        else//显示
        {
            _mapMask.gameObject.SetActive(true);
            WordEventObj.SetActive(true);
            WordEventObj.transform.Find("EventList").gameObject.SetActive(true);

            WordEventObj.GetComponent<WordEvent>().ClearWorldEventList();

            WordEventObj.transform.Find("WorldEvenWindow").gameObject.SetActive(false);
            CharacterRecorder.instance.IsOpenEventList = true;
            NetworkHandler.instance.SendProcess("2201#;");
            if (GameObject.Find("ResourcesBoard") != null)
            {
                ResourcesBoard.GetComponent<ResourceBoard>().MyGridClear();
            }
            isShowResource = true;
            ResourcesBoard.SetActive(false);
            //ResourceBtn.GetComponent<TweenPosition>().PlayForward();

            //IsMove = true;
            //InitWorldEvenList();
        }
    }

    public void DeleteTime()
    {
        Invoke("TheWorldBtnClick", 1f);
    }

    void ThreadBtnClick()
    {
        TextTranslator.Gate _gate = TextTranslator.instance.GetGateByID(CharacterRecorder.instance.lastGateID);
        //int gotoId = _gate.group + 10000;
        Transform _mapCon = GameObject.Find("MapObject").transform.Find("MapCon");
        if (_mapCon != null)
        {
            int MapSatrListCount = _mapCon.GetComponent<MapWindow>().MapSatrList.Count;
            if (MapSatrListCount < CharacterRecorder.instance.lastGateID - 10000)
            {
                UIManager.instance.OpenPromptWindow("长官，请先解锁关卡！", PromptWindow.PromptType.Hint, null, null);
                return;
            }
            CharacterRecorder.instance.gotoGateID = _gate.id;
            _mapCon.GetComponent<MapWindow>().SetMapTypeUpdate();
        }
    }

    public void Cloud()
    {
        if (GameObject.Find("cloudWindow") != null)
        {
            GameObject.Find("cloudWindow").SetActive(false);
        }
    }

    public void InitWindow_Back(int _AddMapId)
    {
        if (CharacterRecorder.instance.lastGiftGateID != CharacterRecorder.instance.lastGateID) //判断是否是最新收复的关kino
        {
            int MaxGateID = TextTranslator.instance.GetMaxGateIDByGroupID(CharacterRecorder.instance.backGateID % 10000);

            if (CharacterRecorder.instance.lastGiftGateID == MaxGateID && MaxGateID > 10007)
            {
                CharacterRecorder.instance.IsOpenMapGate = false;
                Debug.LogError("弹出收复面版");
                if (GameObject.Find("BigGuide") == null && GameObject.Find("MinGuide") == null && GameObject.Find("FirstRechargeWindow") == null && GameObject.Find("NewGuideSkipWindow") == null)
                {
                    UIManager.instance.OpenSinglePanel("GateGiftWindow", false);
                    GameObject.Find("GateGiftWindow").GetComponent<GateGiftWindow>().SatrtInitPanel(0, "90001$20000!10102$50!");
                }
            }
            CharacterRecorder.instance.lastGiftGateID = CharacterRecorder.instance.lastGateID;
        }


        if (CharacterRecorder.instance.IsOpenMapGate)
        {
            CharacterRecorder.instance.IsOpenMapGate = false;
            ChoiceWindow(7);
            this.transform.Find("All/GateInfoWindow").gameObject.SetActive(true);
            SetArrow(CharacterRecorder.instance.Direction_Back);
            this.transform.Find("All/GateInfoWindow").GetComponent<MapGateInfoWindow>().Init(CharacterRecorder.instance.backGateID, 0, null, null, null, 0);
            //CharacterRecorder.instance.backGateID = -1;
            //CharacterRecorder.instance.Direction_Back = -1;
        }
    }
    /// <summary>
    /// 选择开启那个窗口
    /// </summary>
    /// <param name="Number"></param>
    public void ChoiceWindow(int Number)
    {
        switch (Number)
        {
            case 1:
                GrabTerritory.SetActive(true);
                PatrolLong.SetActive(false);
                PatrolItem.SetActive(false);
                GateInfoWindow.SetActive(false);
                PatrolFight.SetActive(false);
                //BaseButton.SetActive(false);
                //ChallengeButton.SetActive(false);
                WordEventObj.SetActive(false);
                EnemyObj.SetActive(false);
                ActionEventObj.SetActive(false);
                break;
            case 2:
                GrabTerritory.SetActive(false);
                PatrolLong.SetActive(true);
                PatrolItem.SetActive(false);
                GateInfoWindow.SetActive(false);
                PatrolFight.SetActive(false);
                //BaseButton.SetActive(false);
                //ChallengeButton.SetActive(false);
                WordEventObj.SetActive(false);
                EnemyObj.SetActive(false);
                ActionEventObj.SetActive(false);
                break;
            case 3:
                GrabTerritory.SetActive(false);
                PatrolLong.SetActive(false);
                PatrolItem.SetActive(true);
                GateInfoWindow.SetActive(false);
                PatrolFight.SetActive(false);
                //BaseButton.SetActive(false);
                //ChallengeButton.SetActive(false);
                WordEventObj.SetActive(false);
                EnemyObj.SetActive(false);
                ActionEventObj.SetActive(false);
                break;
            case 4:
                GrabTerritory.SetActive(false);
                PatrolLong.SetActive(false);
                PatrolItem.SetActive(false);
                GateInfoWindow.SetActive(true);
                PatrolFight.SetActive(false);
                //BaseButton.SetActive(false);
                //ChallengeButton.SetActive(false);
                WordEventObj.SetActive(false);
                EnemyObj.SetActive(false);
                ActionEventObj.SetActive(false);
                break;
            case 5:
                GrabTerritory.SetActive(false);
                PatrolLong.SetActive(false);
                PatrolItem.SetActive(false);
                GateInfoWindow.SetActive(false);
                PatrolFight.SetActive(true);
                //BaseButton.SetActive(false);
                //ChallengeButton.SetActive(false);
                WordEventObj.SetActive(false);
                EnemyObj.SetActive(false);
                ActionEventObj.SetActive(false);
                break;
            case 6:
                GrabTerritory.SetActive(false);
                PatrolLong.SetActive(false);
                PatrolItem.SetActive(false);
                GateInfoWindow.SetActive(false);
                PatrolFight.SetActive(false);
                //BaseButton.SetActive(false);
                //ChallengeButton.SetActive(false);
                //WordEventObj.SetActive(true);
                EnemyObj.SetActive(false);
                ActionEventObj.SetActive(false);
                break;
            case 7:
                GrabTerritory.SetActive(false);
                PatrolLong.SetActive(false);
                PatrolItem.SetActive(false);
                GateInfoWindow.SetActive(false);
                PatrolFight.SetActive(false);
                //BaseButton.SetActive(true);
                //ChallengeButton.SetActive(true);
                WordEventObj.SetActive(false);
                EnemyObj.SetActive(false);
                ActionEventObj.SetActive(false);
                break;
        }
        if (Number != 6)
        {
            if (GameObject.Find("ResourcesBoard") != null)
            {
                //isShowResource = true;
                ResourcesBoard.GetComponent<ResourceBoard>().MyGridClear();
                ResourcesBoard.SetActive(false);
            }
            GameObject _wordEvent = GameObject.Find("WordEvent");
            if (_wordEvent != null)
            {
                _wordEvent.gameObject.SetActive(false);
            }
        }
        //if(Number == 7)
        {
            GameObject _go = GameObject.Find("MapMask");
            if (_go != null)
            {
                _go.SetActive(false);
            }
        }
        //if(Number != 4)
        //{
        //    Debug.Log("此处设置不打开精英关：" + Number);
        //    //CharacterRecorder.instance.IsOpenCreamCN = false;
        //}
    }

    public void ShowTopContent()
    {
        mTopContent.GetComponent<TweenPosition>().ResetToBeginning();
        mTopContent.GetComponent<TweenPosition>().PlayForward();
        mTopContent.SetActive(true);
    }

    public void CloseTopContent()
    {
        mTopContent.SetActive(false);
    }

    public void SetArrow(int Direction)
    {
        CharacterRecorder.instance.Direction_Back = Direction;
        switch (Direction)
        {
            case 1:
                SpriteArrow1.SetActive(true);
                SpriteArrow2.SetActive(false);
                SpriteArrow3.SetActive(false);
                SpriteArrow4.SetActive(false);
                SpriteArrow5.SetActive(false);
                SpriteArrow6.SetActive(false);
                SpriteArrow7.SetActive(false);
                SpriteArrow8.SetActive(false);
                break;
            case 2:
                SpriteArrow1.SetActive(false);
                SpriteArrow2.SetActive(true);
                SpriteArrow3.SetActive(false);
                SpriteArrow4.SetActive(false);
                SpriteArrow5.SetActive(false);
                SpriteArrow6.SetActive(false);
                SpriteArrow7.SetActive(false);
                SpriteArrow8.SetActive(false);
                break;
            case 3:
                SpriteArrow1.SetActive(false);
                SpriteArrow2.SetActive(false);
                SpriteArrow3.SetActive(true);
                SpriteArrow4.SetActive(false);
                SpriteArrow5.SetActive(false);
                SpriteArrow6.SetActive(false);
                SpriteArrow7.SetActive(false);
                SpriteArrow8.SetActive(false);
                break;
            case 4:
                SpriteArrow1.SetActive(false);
                SpriteArrow2.SetActive(false);
                SpriteArrow3.SetActive(false);
                SpriteArrow4.SetActive(true);
                SpriteArrow5.SetActive(false);
                SpriteArrow6.SetActive(false);
                SpriteArrow7.SetActive(false);
                SpriteArrow8.SetActive(false);
                break;
            case 5:
                SpriteArrow1.SetActive(false);
                SpriteArrow2.SetActive(false);
                SpriteArrow3.SetActive(false);
                SpriteArrow4.SetActive(false);
                SpriteArrow5.SetActive(true);
                SpriteArrow6.SetActive(false);
                SpriteArrow7.SetActive(false);
                SpriteArrow8.SetActive(false);
                break;
            case 6:
                SpriteArrow1.SetActive(false);
                SpriteArrow2.SetActive(false);
                SpriteArrow3.SetActive(false);
                SpriteArrow4.SetActive(false);
                SpriteArrow5.SetActive(false);
                SpriteArrow6.SetActive(true);
                SpriteArrow7.SetActive(false);
                SpriteArrow8.SetActive(false);
                break;
            case 7:
                SpriteArrow1.SetActive(false);
                SpriteArrow2.SetActive(false);
                SpriteArrow3.SetActive(false);
                SpriteArrow4.SetActive(false);
                SpriteArrow5.SetActive(false);
                SpriteArrow6.SetActive(false);
                SpriteArrow7.SetActive(true);
                SpriteArrow8.SetActive(false);
                break;
            case 8:
                SpriteArrow1.SetActive(false);
                SpriteArrow2.SetActive(false);
                SpriteArrow3.SetActive(false);
                SpriteArrow4.SetActive(false);
                SpriteArrow5.SetActive(false);
                SpriteArrow6.SetActive(false);
                SpriteArrow7.SetActive(false);
                SpriteArrow8.SetActive(true);
                break;

        }
    }

    public void SetinstarTimerObj(GameObject obj, int Timer)
    {
        if (IsClearResourceTimes)
        {
            IsClearResourceTimes = false;
            for (int i = 0; i < _timerUpdates.Count; i++)
            {
                DestroyImmediate(_timerUpdates[i]);
            }
        }
        //if(_timerUpdate!=null)
        //{
        //    DestroyImmediate(_timerUpdate);
        //}
        GameObject Obj1 = Instantiate(TimerUpdateObj) as GameObject;
        //_timerUpdate = Obj1;
        Obj1.SetActive(true);
        Obj1.transform.parent = TimerUpdateObj.transform.parent;
        Obj1.transform.localRotation = TimerUpdateObj.transform.localRotation;
        Obj1.transform.localScale = TimerUpdateObj.transform.localScale;

        _timerUpdates.Add(Obj1);

        Obj1.GetComponent<UpdateWordItem>().SetGameObj(obj, Timer);
    }
    public void SetInstarMapName(GameObject obj, string Name, int StarNumber, string objName = "")
    {

        GameObject Obj1 = Instantiate(MapNameShowObj) as GameObject;
        Obj1.SetActive(true);
        Obj1.transform.parent = MapNameShowObj.transform.parent;
        Obj1.transform.localRotation = MapNameShowObj.transform.localRotation;
        Obj1.transform.localScale = MapNameShowObj.transform.localScale;
        if (objName != null && objName != "")
        {
            Obj1.name = objName;
        }

        Obj1.GetComponent<UpdateMapName>().SetGameObj(obj, Name, StarNumber);
    }
    public void SetTheChestPrompt(GameObject obj, string Name, string ObjName, string StarNum)
    {
        GameObject Obj1 = Instantiate(MapNameShowObj) as GameObject;
        Obj1.SetActive(true);
        Obj1.transform.parent = MapNameShowObj.transform.parent;
        Obj1.transform.localRotation = MapNameShowObj.transform.localRotation;
        Obj1.transform.localScale = MapNameShowObj.transform.localScale;
        Obj1.name = ObjName;
        if (Name == "")
        {
            Obj1.GetComponent<UpdateMapName>().SetGameObj(obj);
        }
        else
        {
            Obj1.GetComponent<UpdateMapName>().SetGameObj(obj, Name, StarNum);
        }
    }
    public void DelClickOpenInfo()
    {
        Transform obj = MapNameShowObj.transform.parent.Find("ClickOpen");
        if (obj != null)
        {
            DestroyImmediate(obj.gameObject);
        }
    }
    #region 世界事件
    /// <summary>
    /// 创建世界事件图标入口
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="IconName"></param>
    /// <param name="_gateID"></param>
    /// <param name="_index">1-世界事件 2-触发世界事件</param>
    public void ShowWorldEventIcon(GameObject obj, int IconName, int _gateID, int _index, int _Color)
    {
        GameObject _itemPrefab = NGUITools.AddChild(MapNameShowObj.transform.parent.gameObject, Resources.Load("GUI/WorldEventIcon") as GameObject);
        _itemPrefab.name = "WorldEventIcon";
        _itemPrefab.GetComponent<UpdateMapName>().worldEventId = _gateID;
        //_itemPrefab.GetComponent<TweenPosition>().from = new Vector3(_itemPrefab.transform.localPosition.x, _itemPrefab.transform.localPosition.y);
        //_itemPrefab.GetComponent<TweenPosition>().to = new Vector3(_itemPrefab.transform.localPosition.x, _itemPrefab.transform.localPosition.y + 5);
        _itemPrefab.transform.GetChild(0).Find("WorldEventFg").GetComponent<UITexture>().mainTexture = Resources.Load("Game/SJ_tubiao" + (5 + _Color)) as Texture; ;
        if (_index == 1)
        {
            UIEventListener.Get(_itemPrefab.transform.GetChild(0).gameObject).onClick = WorldEventIconClick;
            _itemPrefab.transform.GetChild(0).GetComponent<UITexture>().mainTexture = Resources.Load("Game/SJ_tubiao5") as Texture;
            _itemPrefab.transform.GetChild(0).Find("WorldEventFg").localPosition = new Vector3(_itemPrefab.transform.GetChild(0).Find("WorldEventFg").localPosition.x, 9);
        }
        else
        {
            UIEventListener.Get(_itemPrefab.transform.GetChild(0).gameObject).onClick = ActionEventIconClick;
            _itemPrefab.transform.GetChild(0).GetComponent<UITexture>().mainTexture = Resources.Load("Game/SJ_tubiao4") as Texture;
            _itemPrefab.transform.GetChild(0).Find("WorldEventFg").localPosition = new Vector3(_itemPrefab.transform.GetChild(0).Find("WorldEventFg").localPosition.x, 7);
        }
        _itemPrefab.transform.GetChild(0).GetComponent<UITexture>().MakePixelPerfect();
        _itemPrefab.GetComponent<UpdateMapName>().SetWorldEvenIconPos(obj, IconName);
    }

    void WorldEventIconClick(GameObject obj)
    {

        int WorldId = obj.transform.parent.GetComponent<UpdateMapName>().worldEventId;
        int CurMovePoint = TextTranslator.instance.GetGateByTypeGroup(1, TextTranslator.instance.GetWorldEventByID(WorldId).GateID).id;
        Debug.Log("世界事件移动的点：" + CurMovePoint + ";" + CharacterRecorder.instance.gotoGateID);
        if (CurMovePoint == CharacterRecorder.instance.gotoGateID)
        {
            GameObject _mapCon = GameObject.Find("MapCon");
            if (_mapCon.GetComponent<MapWindow>().IsLengthClick())
            {
                return;
            }
        }

        ChoiceWindow(7);
        int worldGateID = TextTranslator.instance.GetWorldEventByID(WorldId).GatePoint;
        if (worldGateID != 89998 && worldGateID != 89999)
        {

            Debug.Log("世界事件移动的ID:" + WorldId);
            GameObject.Find("MainCamera").GetComponent<MouseClick>().IsWorldEvev = true;
            GameObject.Find("MainCamera").GetComponent<MouseClick>().IsEnemy = false;
            GameObject.Find("MainCamera").GetComponent<MouseClick>().IsAction = false;
            GameObject.Find("MainCamera").GetComponent<MouseClick>().IsResource = false;
        }
        else
        {
            string _statue = "";
            if (worldGateID == 89998)
            {
                _statue = "2";
            }
            else
            {
                _statue = "1";
            }
            GameObject.Find("MainCamera").GetComponent<MouseClick>().IsEnemy = true;
            GameObject.Find("MainCamera").GetComponent<MouseClick>().IsWorldEvev = false;
            GameObject.Find("MainCamera").GetComponent<MouseClick>().IsAction = false;
            GameObject.Find("MainCamera").GetComponent<MouseClick>().IsResource = false;
            NetworkHandler.instance.SendProcess("2206#" + _statue + ";");
        }
        GameObject.Find("MapCon").GetComponent<MapWindow>().WorldEventId = WorldId;
        Debug.Log("我点击到了世界事件的图标：" + WorldId);
        SceneTransformer.instance.NowWorldEventID = WorldId;
        CharacterRecorder.instance.gotoGateID = TextTranslator.instance.GetGateByTypeGroup(1, TextTranslator.instance.GetWorldEventByID(WorldId).GateID).id;
        GameObject go = GameObject.Find("MapObject");
        if (go != null)
        {
            go.transform.FindChild("MapCon").GetComponent<MapWindow>().SetMapTypeUpdate();
        }
        if (GameObject.Find("MapUiWindow/All/WordEvent/EventList") != null)
        {
            GameObject.Find("MapUiWindow/All/WordEvent/EventList").SetActive(false);
        }
    }
    void ActionEventIconClick(GameObject obj)
    {
        int WorldId = obj.transform.parent.GetComponent<UpdateMapName>().worldEventId;
        int CurMovePoint = TextTranslator.instance.GetGateByTypeGroup(1, TextTranslator.instance.GetActionEventById(WorldId).ForGateID).id;
        if (CurMovePoint == CharacterRecorder.instance.gotoGateID)
        {
            GameObject _mapCon = GameObject.Find("MapCon");
            if (_mapCon.GetComponent<MapWindow>().IsLengthClick())
            {
                return;
            }
        }
        GameObject _mapUi = this.gameObject;
        if (_mapUi != null)
        {
            CharacterRecorder.instance.gotoGateID = TextTranslator.instance.GetGateByTypeGroup(1, TextTranslator.instance.GetActionEventById(WorldId).ForGateID).id;
            MouseClick _mouseClick = GameObject.Find("MainCamera").GetComponent<MouseClick>();
            _mouseClick.IsAction = true;
            _mouseClick.IsEnemy = false;
            _mouseClick.IsWorldEvev = false;
            _mouseClick.IsResource = false;
            GameObject go = GameObject.Find("MapObject");
            if (go != null)
            {
                go.transform.FindChild("MapCon").GetComponent<MapWindow>().WorldEventId = WorldId;
                go.transform.FindChild("MapCon").GetComponent<MapWindow>().SetMapTypeUpdate();
            }

            //_mapUi.transform.Find("All/ActionEventWindow").gameObject.SetActive(true);
            //GameObject.Find("ActionEventWindow").GetComponent<ActionEventWindow>().InitActionEvent(WorldId);
        }
        else
        {
            Debug.Log("MapUiWindow没有打开！---ActionButtonClick");
        }

        if (GameObject.Find("MapUiWindow/All/WordEvent") != null)
        {
            GameObject.Find("MapUiWindow/All/WordEvent").SetActive(false);
        }
    }
    void ClearWorldEventIcon()
    {
        for (int i = 0; i < MapNameObj.transform.childCount; i++)
        {
            if (MapNameObj.transform.GetChild(i).name == "WorldEventIcon")
            {
                DestroyImmediate(MapNameObj.transform.GetChild(i).gameObject);
                i--;
            }
        }
    }


    //显示世界事件图标入口
    public void ShowWorldEventIcon()
    {
        if (CharacterRecorder.instance.lastGateID <= 10033)
        {
            return;
        }
        ClearWorldEventIcon();
        string[] dataSplit = CharacterRecorder.instance.worldEventList.Split(';');
        if (dataSplit[0] == "1")
        {
            List<int> _EventId = new List<int>();
            List<int> _EventGoId = new List<int>();
            List<int> _EventColor = new List<int>();
            WorldEventList word = new WorldEventList();
            OneTime = int.Parse(dataSplit[1]);
            string[] secSplit = dataSplit[2].Split('!');
            GameObject go = GameObject.Find("MapObject");
            for (int i = 0; i < secSplit.Length - 1; i++)
            {
                string[] tirSplit = secSplit[i].Split('$');
                WorldEventInfo worldInfo = new WorldEventInfo();
                worldInfo.WorldId = int.Parse(tirSplit[0]);
                worldInfo.WorldColor = int.Parse(tirSplit[2]);
                word.WorldEventInfo.Add(worldInfo);
                //TextTranslator.instance.GetWorldEventByID(_gateID).GateID
                //go.transform.FindChild("MapCon").GetComponent<MapWindow>().ShowWorldEventIcon(int.Parse(tirSplit[0]));
            }
            for (int i = 0; i < word.WorldEventInfo.Count; i++)
            {
                if (!_EventId.Contains(TextTranslator.instance.GetWorldEventByID(word.WorldEventInfo[i].WorldId).GateID))
                {
                    _EventId.Add(TextTranslator.instance.GetWorldEventByID(word.WorldEventInfo[i].WorldId).GateID);
                    _EventGoId.Add(word.WorldEventInfo[i].WorldId);
                    _EventColor.Add(word.WorldEventInfo[i].WorldColor);
                }
                //else
                //{
                //    if (word.WorldEventInfo[i].WorldColor > _EventColor[_EventId.IndexOf(TextTranslator.instance.GetWorldEventByID(word.WorldEventInfo[i].WorldId).GateID)])
                //    {
                //        _EventGoId[_EventId.IndexOf(word.WorldEventInfo[i].WorldId)] = word.WorldEventInfo[i].WorldId;
                //    }
                //}
            }
            for (int i = 0; i < _EventId.Count; i++)
            {
                go.transform.FindChild("MapCon").GetComponent<MapWindow>().ShowWorldEventIcon(_EventGoId[i], _EventColor[i]);
            }
            string[] dataInfo = dataSplit[3].Split('$');
            if (dataInfo[0] != null && dataInfo[0] != "")
            {
                go.transform.FindChild("MapCon").GetComponent<MapWindow>().ShowActionEventIcon(int.Parse(dataInfo[0]));
            }
        }
    }

    public void InitWorldEvenList()
    {
        if (CharacterRecorder.instance.lastGateID <= 10033)
        {
            Debug.Log("登录时的最新地图id " + CharacterRecorder.instance.lastGateID);
            return;
        }
        string[] dataSplit = CharacterRecorder.instance.worldEventList.Split(';');
        if (dataSplit[0] == "1")
        {
            WorldEventList word = new WorldEventList();
            word.TimerNumber = int.Parse(dataSplit[1]);
            string[] secSplit = dataSplit[2].Split('!');
            //if (this != null)
            {
                if (!IsWorldBtnClick)
                {
                    GameObject _WordButtonRedPoint = GameObject.Find("WordButton").transform.FindChild("SpriteRedDian").gameObject;
                    if (secSplit.Length > 1 && CharacterRecorder.instance.WorldEventFightCount < 10)
                    {
                        _WordButtonRedPoint.SetActive(true);
                        return;
                    }
                    else
                    {
                        _WordButtonRedPoint.SetActive(false);
                        return;
                    }
                }
            }
            for (int i = 0; i < secSplit.Length - 1; i++)
            {
                string[] tirSplit = secSplit[i].Split('$');
                WorldEventInfo worldInfo = new WorldEventInfo();
                worldInfo.WorldId = int.Parse(tirSplit[0]);
                worldInfo.WorldType = int.Parse(tirSplit[1]);
                worldInfo.WorldColor = int.Parse(tirSplit[2]);
                worldInfo.WorldItem = int.Parse(tirSplit[3]);
                word.WorldEventInfo.Add(worldInfo);

            }

            GameObject _mapUiWindow = GameObject.Find("MapUiWindow");
            if (_mapUiWindow != null)
            {
                //_mapUiWindow.GetComponent<MapUiWindow>().ChoiceWindow(6);
                if (_mapUiWindow.transform.Find("All").Find("WordEvent") != null)
                {
                    //GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().worldList = word;
                    if (dataSplit[3] != "" && dataSplit[3] != null)
                    {
                        string[] dataInfo = dataSplit[3].Split('$');
                        _mapUiWindow.transform.Find("All").Find("WordEvent").GetComponent<WordEvent>().ActionEventId = int.Parse(dataInfo[0]);
                        Debug.Log("千里走单骑ID：" + int.Parse(dataInfo[0]));
                        if (GameObject.Find("MainCamera").GetComponent<MouseClick>().IsAction)
                        {
                            GameObject.Find("MapCon").GetComponent<MapWindow>().WorldEventId = int.Parse(dataInfo[0]);
                        }

                    }
                    _mapUiWindow.transform.Find("All").Find("WordEvent").GetComponent<WordEvent>().setInfo(word);
                }
            }
        }
        else
        {
            Debug.Log("获取世界时间列表失败");
        }
    }

    void WorldEvenTime(string timer)
    {
        GameObject go = GameObject.Find("WorldEvenWindow");
        if (go != null)
        {
            go.transform.FindChild("BottomContect").FindChild("Timer").GetComponent<UILabel>().text = timer;
        }
    }
    #endregion
    /// <summary>
    /// 领取奖励
    /// </summary>
    /// <param name="_Reward"></param>
    /// <param name="_Statue">对应宝箱状态</param>
    public void GetReward(string _Reward, int _Statue)
    {
        //MyGridClear();
        string[] reward = _Reward.Split('!');
        List<Item> _item = new List<Item>();
        for (int i = 0; i < reward.Length - 1; i++)
        {
            string[] item = reward[i].Split('$');
            if (item[0] == "90001")
            {
                CharacterRecorder.instance.AddMoney(int.Parse(item[1]));
            }
            if (item[0] == "90002")
            {
                CharacterRecorder.instance.lunaGem += int.Parse(item[1]);
            }
            Item _temp = new Item(int.Parse(item[0]), int.Parse(item[1]));
            _item.Add(_temp);
        }
        UIManager.instance.OpenSinglePanel("AdvanceWindow", false);
        GameObject.Find("AdvanceWindow").AddComponent<DestroySelf>();
        GameObject.Find("AdvanceWindow").GetComponent<AdvanceWindow>().SetInfo(AdvanceWindowType.GainResult, null, null, null, null, _item);
        CloseGateInfoWindow(_Statue);
        mTopContent.GetComponent<TopContent>().Reset();
        GameObject mapObj = GameObject.Find("MapObject");
        if (mapObj != null)
        {
            MapWindow mw = mapObj.transform.GetChild(0).GetComponent<MapWindow>();
            mw.ReceiveNum--;
            for (int i = 0; i < MapNameObj.transform.childCount; i++)
            {
                if (MapNameObj.transform.GetChild(i).name == GetRewardGroupId.ToString())
                {
                    if (_Statue != 3)
                    {
                        if (_Statue == 1)
                        {
                            mw.TheChestStatueList[GetRewardGroupId - 1] = 2;
                        }
                        else
                        {
                            mw.CreamStatueList[TextTranslator.instance.GetGateCompleteBox(GetRewardGroupId).GateCompleteBoxID - 1] = 2;
                        }
                    }
                    else
                    {
                        mw.PassStatueList[TextTranslator.instance.GetGateCompleteBoxGroup(GetRewardGroupId) - 1] = 2;
                    }
                    DestroyImmediate(MapNameObj.transform.GetChild(i).gameObject);
                    i--;
                }
            }
            GameObject go = GameObject.Find("MapUiWindow");
            if (_Statue != 3)
            {
                if (_Statue == 1)
                {
                    //for (int i = 0; i < mw.TheChestStatueList.Count; i++)
                    //{
                    //    if (mw.TheChestStatueList[i] == 1)
                    //    {
                    //        List<TextTranslator.Gate> myGate = TextTranslator.instance.GetGroupGate(i + 1, 1);
                    //        go.GetComponent<MapUiWindow>().SetTheChestPrompt(mw.MapItemList[i].transform.gameObject, myGate.Count * 3 + "/" + myGate.Count * 3, (i + 1).ToString());
                    //    }
                    //}
                    //for (int i = 0; i < mw.PassStatueList.Count; i++)
                    //{
                    //    if (mw.TheChestStatueList[i] != 1 && mw.PassStatueList[i] == 1)
                    //    {
                    //        //List<TextTranslator.Gate> myGate = TextTranslator.instance.GetGroupGate(i + 1, 1);
                    //        go.GetComponent<MapUiWindow>().SetTheChestPrompt(mw.MapItemList[i].transform.gameObject, "", (i + 1).ToString());
                    //    }
                    //}
                    //刷新点下面的Label显示-精英关
                    for (int i = 0; i < mw.CreamStatueList.Count; i++)
                    {
                        List<TextTranslator.Gate> myGate = TextTranslator.instance.GetGroupGate(TextTranslator.instance.GetGateCompleteBoxGroup(i + 1), 2);

                        List<TextTranslator.Gate> _nomalGate = TextTranslator.instance.GetGroupGate(TextTranslator.instance.GetGateCompleteBoxGroup(i + 1), 1);
                        int _nomalGateStar = mw.CountGroupStar(_nomalGate);
                        int _StarCount = mw.CountGroupStar(myGate);
                        //if (_StarCount != 0 && mw.CreamStatueList[i] != 2)
                        if (_nomalGateStar == (_nomalGate.Count * 3) && (myGate[0].id <= CharacterRecorder.instance.lastCreamGateID))
                        {
                            if (mw.CreamStatueList[i] != 2 && CharacterRecorder.instance.lastGateID > 10010)
                            {
                                //if (_StarCount != 0)
                                go.GetComponent<MapUiWindow>().SetTheChestPrompt(mw.MapItemList[TextTranslator.instance.GetGateCompleteBoxGroup(i + 1) - 1].transform.gameObject, "JY" + _StarCount + "/" + myGate.Count * 3, TextTranslator.instance.GetGateCompleteBoxGroup(i + 1).ToString(), (_StarCount / (myGate.Count * 3)).ToString());
                            }
                        }
                    }
                }
                else
                {
                    //for (int i = 0; i < mw.TheChestStatueList.Count; i++)
                    //{
                    //    if (mw.TheChestStatueList[i] == 1)
                    //    {
                    //        List<TextTranslator.Gate> myGate = TextTranslator.instance.GetGroupGate(i + 1, 1);
                    //        int _StarCount = mw.CountGroupStar(myGate);
                    //        if (_StarCount != 0)
                    //        {
                    //            go.GetComponent<MapUiWindow>().SetTheChestPrompt(mw.MapItemList[i].transform.gameObject, _StarCount + "/" + myGate.Count * 3, (i + 1).ToString(), (_StarCount / (myGate.Count * 3)).ToString());
                    //        }
                    //    }
                    //}
                }
            }
            else
            {
                //for (int i = 0; i < mw.TheChestStatueList.Count; i++)
                //{
                //    if (mw.TheChestStatueList[i] == 1)
                //    {
                //        List<TextTranslator.Gate> myGate = TextTranslator.instance.GetGroupGate(i + 1, 1);
                //        int _StarCount = mw.CountGroupStar(myGate);
                //        if (_StarCount != 0)
                //        {
                //            go.GetComponent<MapUiWindow>().SetTheChestPrompt(mw.MapItemList[i].transform.gameObject, _StarCount + "/" + myGate.Count * 3, (i + 1).ToString(),(_StarCount / (myGate.Count * 3)).ToString());
                //        }
                //    }
                //}
                //for (int i = 0; i < mw.CreamStatueList.Count; i++)
                //{
                //    List<TextTranslator.Gate> myGate = TextTranslator.instance.GetGroupGate(TextTranslator.instance.GetGateCompleteBoxGroup(i + 1), 2);
                //    int _StarCount = mw.CountGroupStar(myGate);
                //    if (_StarCount != 0 && mw.CreamStatueList[i] != 2)
                //    {
                //        go.GetComponent<MapUiWindow>().SetTheChestPrompt(mw.MapItemList[TextTranslator.instance.GetGateCompleteBoxGroup(i + 1) - 1].transform.gameObject, _StarCount + "/" + myGate.Count * 3, TextTranslator.instance.GetGateCompleteBoxGroup(i + 1).ToString(), (_StarCount / (myGate.Count * 3)).ToString());
                //    }
                //}
                for (int i = 0; i < mw.CreamStatueList.Count; i++)
                {
                    List<TextTranslator.Gate> myGate = TextTranslator.instance.GetGroupGate(TextTranslator.instance.GetGateCompleteBoxGroup(i + 1), 2);

                    List<TextTranslator.Gate> _nomalGate = TextTranslator.instance.GetGroupGate(TextTranslator.instance.GetGateCompleteBoxGroup(i + 1), 1);
                    int _nomalGateStar = mw.CountGroupStar(_nomalGate);
                    int _StarCount = mw.CountGroupStar(myGate);
                    //if (_StarCount != 0 && mw.CreamStatueList[i] != 2)
                    //if (mw.CreamStatueList[i] != 2 && mw.TheChestStatueList[TextTranslator.instance.GetGateCompleteBoxGroup(i + 1) - 1] == 2 && CharacterRecorder.instance.lastGateID > 10010)
                    if (_nomalGateStar == (_nomalGate.Count * 3) && (myGate[0].id <= CharacterRecorder.instance.lastCreamGateID))
                    {
                        if (mw.CreamStatueList[i] != 2 && CharacterRecorder.instance.lastGateID > 10010)
                        {
                            //if (_StarCount != 0)
                            go.GetComponent<MapUiWindow>().SetTheChestPrompt(mw.MapItemList[TextTranslator.instance.GetGateCompleteBoxGroup(i + 1) - 1].transform.gameObject, "JY" + _StarCount + "/" + myGate.Count * 3, TextTranslator.instance.GetGateCompleteBoxGroup(i + 1).ToString(), (_StarCount / (myGate.Count * 3)).ToString());
                        }
                    }
                }
            }
        }

    }

    void CloseGateInfoWindow(int _statue)
    {
        GameObject mapObj = GameObject.Find("MapObject");
        if (mapObj != null)
        {
            MapWindow mw = mapObj.transform.GetChild(0).GetComponent<MapWindow>();
            if (_statue == 2)
            {
                //Debug.Log("我认为是这里出错了：" + GetRewardGroupId + "-------:" + TextTranslator.instance.GetGateCompleteBox(GetRewardGroupId).GateCompleteBoxID + "--------:" + mw.CreamStatueList.Count);
                //if (mw.CreamStatueList[GetRewardGroupId - 1] == 1)
                if (mw.CreamStatueList[TextTranslator.instance.GetGateCompleteBox(GetRewardGroupId).GateCompleteBoxID - 1] == 1)
                {
                    GameObject.Find("GateInfoWindow").SetActive(false);
                    CloseTopContent();
                    //this.gameObject.SetActive(false);
                    if (GameObject.Find("MapMask") != null)
                    {
                        GameObject.Find("MapMask").SetActive(false);
                    }
                }
            }
            else
            {
                if (_statue == 1)
                {
                    if (mw.TheChestStatueList[GetRewardGroupId - 1] == 1)
                    {
                        GameObject.Find("GateInfoWindow").SetActive(false);
                        CloseTopContent();
                        if (GameObject.Find("MapMask") != null)
                        {
                            GameObject.Find("MapMask").SetActive(false);
                        }
                    }
                }
                //else
                //{
                //    if (mw.TheChestStatueList[GetRewardGroupId - 1] == 2 && mw.PassStatueList[GetRewardGroupId - 1] == 1)
                //    {
                //        GameObject.Find("GateInfoWindow").SetActive(false);
                //        if (GameObject.Find("MapMask") != null)
                //        {
                //            GameObject.Find("MapMask").SetActive(false);
                //        }
                //    }
                //}
            }
        }
    }

    #region 第一名奖杯显示

    public void CupShowInMainWindow()
    {
        StartCoroutine(IECupShowInMainWindow());
    }
    IEnumerator IECupShowInMainWindow()
    {
        yield return new WaitForSeconds(0.1f);
        if (CharacterRecorder.instance.level >= 25)
        {
            if (CharacterRecorder.instance.FirstPowerName != "0")
            {
                Cup1.SetActive(true);
                Cup1.transform.Find("NameLabel").GetComponent<UILabel>().text = CharacterRecorder.instance.FirstPowerName;
            }
            else
            {
                Cup1.transform.parent = gameObject.transform.Find("CupButton");
                Cup1.SetActive(false);
            }

            if (CharacterRecorder.instance.FirstPvpName != "0")
            {
                Cup2.SetActive(true);
                Cup2.transform.Find("NameLabel").GetComponent<UILabel>().text = CharacterRecorder.instance.FirstPvpName;
            }
            else
            {
                Cup2.transform.parent = gameObject.transform.Find("CupButton");
                Cup2.SetActive(false);
            }
            if (CharacterRecorder.instance.FirstWoodsName != "0")
            {
                Cup3.SetActive(true);
                Cup3.transform.Find("NameLabel").GetComponent<UILabel>().text = CharacterRecorder.instance.FirstWoodsName;
            }
            else
            {
                Cup3.transform.parent = gameObject.transform.Find("CupButton");
                Cup3.SetActive(false);
            }
            if (CharacterRecorder.instance.FirstLegionName != "0")
            {
                Cup4.SetActive(true);
                Cup4.transform.Find("NameLabel").GetComponent<UILabel>().text = CharacterRecorder.instance.FirstLegionName;
            }
            else
            {
                Cup4.transform.parent = gameObject.transform.Find("CupButton");
                Cup4.SetActive(false);
            }

            gameObject.transform.Find("All/CupButton/Grid").GetComponent<UIGrid>().Reposition();
        }
        else
        {
            Cup1.SetActive(false);
            Cup2.SetActive(false);
            Cup3.SetActive(false);
            Cup4.SetActive(false);
        }
    }
    #endregion
}
