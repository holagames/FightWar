using UnityEngine;
using System.Collections;

public class WayItem : MonoBehaviour
{

    public UILabel LabelDetail;
    public GameObject ChanceInfo;
    public GameObject StatueInfo;
    public UILabel LabelInfo;
    public UILabel LabelGo;

    int Status = 1;
    string Source = "0";
    string WindowName;
    bool IsBgClick = false;
    void Start()
    {
        //if (UIEventListener.Get(StatueInfo.transform.FindChild("SpriteGoBtn").gameObject).onClick == null)
        //{
        //    UIEventListener.Get(StatueInfo.transform.FindChild("SpriteGoBtn").gameObject).onClick = delegate(GameObject go)
        //    {
        //        Debug.Log("AAAAA");
        //        PictureCreater.instance.DestroyAllComponent();
        //        GameObject.Find("WayWindow").layer = 10;
        //        foreach (Component c in GameObject.Find("WayWindow").GetComponentsInChildren(typeof(Transform), true))
        //        {
        //            c.gameObject.layer = 10;
        //        }
        //        GoWindow();

        //    };
        //}
        //LabelInfo.gameObject.SetActive(false);
        //if (UIEventListener.Get(this.gameObject).onClick == null)
        {
            UIEventListener.Get(this.gameObject).onClick = delegate(GameObject go)
            {
                if (!IsBgClick)
                    return;
                PictureCreater.instance.DestroyAllComponent();
                GameObject.Find("WayWindow").layer = 10;
                foreach (Component c in GameObject.Find("WayWindow").GetComponentsInChildren(typeof(Transform), true))
                {
                    c.gameObject.layer = 10;
                }
                GoWindow();

            };
        }
    }

    void GoWindow()
    {

        if (Status == 1)
        {
            //if (CharacterRecorder.instance.lastGateID >= CharacterRecorder.instance.gotoGateID)
            {
                UIManager.instance.OpenPanel("MapUiWindow", true);
                if (int.Parse(Source) != 10000)
                {
                    if (int.Parse(Source) > 20000 && int.Parse(Source) < 30000)
                    {
                        CharacterRecorder.instance.IsOpenCreamCN = true;
                    }
                    CharacterRecorder.instance.gotoGateID = int.Parse(Source);
                    CharacterRecorder.instance.IsJumpMove = true;
                }
            }
        }
        else if (Status == 2)
        {
            switch (Source)
            {
                case "1":
                    UIManager.instance.OpenPanel("MapUiWindow", true);
                    break;
                case "2": //普通商店
                    // UIManager.instance.OpenPanel("RankShopWindow", false);
                    UIManager.instance.OpenPanel("RankShopWindow", true);
                    break;
                case "3": //荣誉商店
                    //  UIManager.instance.OpenPanel("RankShopWindow", false);
                    CharacterRecorder.instance.RankShopType = 10003;
                    UIManager.instance.OpenPanel("RankShopWindow", true);
                    break;
                case "4": //试炼商店
                    //  UIManager.instance.OpenPanel("RankShopWindow", false);
                    CharacterRecorder.instance.RankShopType = 10004;
                    UIManager.instance.OpenPanel("RankShopWindow", true);
                    break;
                case "5": //军团商店
                    //  UIManager.instance.OpenPanel("RankShopWindow", false);
                    CharacterRecorder.instance.RankShopType = 10005;
                    UIManager.instance.OpenPanel("RankShopWindow", true);
                    break;
                case "6": //金条商店
                    //  UIManager.instance.OpenPanel("SecretShopWindow", false);
                    CharacterRecorder.instance.RankShopType = 10006;
                    UIManager.instance.OpenPanel("RankShopWindow", true);
                    break;
                case "7": //抽奖
                    //    UIManager.instance.OpenPanel("GaChaWindow", false);
                    UIManager.instance.OpenPanel("GachaWindow", true);
                    break;
                case "8": //夺宝
                    //   UIManager.instance.OpenPanel("GrabItemWindow", false);
                    {
                        UIManager.instance.OpenPanel("GrabItemWindow", true);
                    }
                    break;
                case "9": //丛林敢死队
                    NetworkHandler.instance.SendProcess("1501#;");
                    break;
                case "10": //世界事件
                    UIManager.instance.OpenPanel("MapUiWindow", true);
                    break;
                case "11": //竞技场
                    UIManager.instance.OpenPanel("PVPWindow", true);
                    break;
                case "12": //王者商店
                    CharacterRecorder.instance.RankShopType = 10007;
                    UIManager.instance.OpenPanel("RankShopWindow", true);
                    break;
                case "13": //军团副本
                    UIManager.instance.OpenPanel("LegionCopyWindowNew", true);
                    break;
                case "14": //vip礼包
                    //UIManager.instance.OpenSinglePanel("VIPShopWindow", false);
                    UIManager.instance.OpenPanel("VIPShopWindow", true);
                    GameObject.Find("VIPShopWindow").GetComponent<VipShopWindow>().SetChangeButton();
                    break;
                case "15": //每日副本
                    UIManager.instance.OpenPanel("EverydayWindow", true);
                    break;
                case "16": //世界boss
                    UIManager.instance.OpenPanel("WorldBossWindow", true);
                    break;
                case "17": //组队副本
                    UIManager.instance.OpenPanel("TeamCopyWindow", true);
                    break;
                case "19": //战场杀敌
                    UIManager.instance.OpenPanel("LegionWarWindow", true);
                    break;
                case "22": //核武器
                    CharacterRecorder.instance.heroPresentWeapon = GameObject.Find("WayWindow").GetComponent<WayWindow>().itemid;
                    CharacterRecorder.instance.HeroWeaponID = CharacterRecorder.instance.heroPresentWeapon - 30000;
                    UIManager.instance.OpenPanel("TurntableWindow", true);
                    break;
                case "23": //芯片
                    CharacterRecorder.instance.SBlaoxu = true;
                    UIManager.instance.OpenPanel("ChipWindow", true);
                    break;
            }
        }
    }

    public void UpdateCreamNum(int _num)
    {
        if (_num != 0)
        {
            LabelInfo.color = new Color(0 / 255f, 255 / 255f, 76 / 255f);
        }
        else
        {
            LabelInfo.color = new Color(255 / 255f, 0 / 255f, 0 / 255f);
        }
        LabelInfo.text = _num + "/3";
        LabelGo.transform.localPosition = new Vector3(LabelGo.transform.localPosition.x, LabelGo.transform.localPosition.y - 12f);
        LabelInfo.gameObject.SetActive(true);
    }

    public void Init(string _Source, int statue, int _isShowNum = 0)
    {
        Status = statue;
        Source = _Source;
        if (statue == 1)
        {
            ChanceInfo.transform.FindChild("LabelChance").gameObject.SetActive(true);
            ChanceInfo.transform.FindChild("LabelOther").gameObject.SetActive(false);
            TextTranslator.Gate _gate = null;
            if (int.Parse(_Source) == 10000)
            {
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "大地图";
                LabelDetail.text = "大地图";
                StatueInfo.transform.FindChild("SpriteGoBtn").gameObject.SetActive(true);
                StatueInfo.transform.FindChild("LabelStatue").gameObject.SetActive(false);
            }
            else
            {
                if (_Source[0] == '1' && _Source != "10000")
                {
                    ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "主线";
                }
                else if (_Source[0] == '2' && _Source != "10000")
                {
                    ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "精英";
                    int _mapId = int.Parse(_Source);
                    if (_isShowNum == 1 && _mapId > 20000 && _mapId < 30000)
                    {
                        NetworkHandler.instance.SendProcess("2017#" + _mapId + ";");
                    }
                }
                _gate = TextTranslator.instance.GetGateByID(int.Parse(_Source));
                if (_gate == null)
                {
                    return;
                }
                LabelDetail.text = _gate.name;

                int gateId = int.Parse(Source);
                bool _isShowGoBtn = false;

                Transform _mapCon = SceneTransformer.instance.MainScene.transform.Find("MapCon");
                int MapSatrListCount = 0;
                if (_mapCon != null)
                {
                    MapSatrListCount = _mapCon.GetComponent<MapWindow>().MapSatrList.Count;
                }

                if (_gate.id > 80000 && _gate.id < 90000)
                {
                    if (CharacterRecorder.instance.lastGateID >= 10033)
                    {
                        _isShowGoBtn = true;
                    }
                }
                else if (_gate.id > 90000)
                {
                    if (CharacterRecorder.instance.lastGateID >= 10038)
                    {
                        _isShowGoBtn = true;
                    }
                }
                else if (_gate.id > 20000 && _gate.id < 30000)
                {
                    //gateId = _gate.id - 10000;
                    if (_gate.id <= CharacterRecorder.instance.lastCreamGateID)
                    {
                        _isShowGoBtn = true;
                    }
                }
                else
                {
                    //gateId = _gate.id;
                    if (_gate.id <= CharacterRecorder.instance.lastGateID && MapSatrListCount >= gateId - 10000)
                    {
                        _isShowGoBtn = true;
                    }
                }

                if (_isShowGoBtn)
                //if (_gate.id <= CharacterRecorder.instance.lastGateID)
                //if (CharacterRecorder.instance.lastGateID >= CharacterRecorder.instance.gotoGateID)
                {
                    StatueInfo.transform.FindChild("SpriteGoBtn").gameObject.SetActive(true);
                    StatueInfo.transform.FindChild("LabelStatue").gameObject.SetActive(false);
                    IsBgClick = true;
                }
                else
                {
                    StatueInfo.transform.FindChild("SpriteGoBtn").gameObject.SetActive(false);
                    StatueInfo.transform.FindChild("LabelStatue").gameObject.SetActive(true);
                    IsBgClick = false;
                }

            }

            //Debug.Log("获取副本的信息："+ CharacterRecorder.instance.lastGateID);
            //Debug.Log("查看人物副本信息：" + CharacterRecorder.instance.lastGateID);
            //Debug.Log("查看副本信息Id：" + _gate.id);


        }
        else if (statue == 2)
        {
            //ChanceInfo.transform.FindChild("LabelChance").gameObject.SetActive(false);
            //ChanceInfo.transform.FindChild("LabelOther").gameObject.SetActive(true);
            SetOtherInfo(_Source);
            if (_Source == "8")
            {
                if (CharacterRecorder.instance.lastGateID >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.duobao).Level)
                {
                    StatueInfo.transform.FindChild("SpriteGoBtn").gameObject.SetActive(true);
                    StatueInfo.transform.FindChild("LabelStatue").gameObject.SetActive(false);
                    IsBgClick = true;
                }
                else
                {
                    StatueInfo.transform.FindChild("SpriteGoBtn").gameObject.SetActive(false);
                    StatueInfo.transform.FindChild("LabelStatue").gameObject.SetActive(true);
                    IsBgClick = false;
                }
            }
            else if (_Source == "11")
            {
                if (CharacterRecorder.instance.lastGateID >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.jingjichang).Level)
                {
                    StatueInfo.transform.FindChild("SpriteGoBtn").gameObject.SetActive(true);
                    StatueInfo.transform.FindChild("LabelStatue").gameObject.SetActive(false);
                    IsBgClick = true;
                }
                else
                {
                    StatueInfo.transform.FindChild("SpriteGoBtn").gameObject.SetActive(false);
                    StatueInfo.transform.FindChild("LabelStatue").gameObject.SetActive(true);
                    IsBgClick = false;
                }
            }
            else if (_Source == "13")
            {
                if (CharacterRecorder.instance.legionID != 0)
                {
                    StatueInfo.transform.FindChild("SpriteGoBtn").gameObject.SetActive(true);
                    StatueInfo.transform.FindChild("LabelStatue").gameObject.SetActive(false);
                    IsBgClick = true;
                }
                else
                {
                    StatueInfo.transform.FindChild("SpriteGoBtn").gameObject.SetActive(false);
                    StatueInfo.transform.FindChild("LabelStatue").gameObject.SetActive(true);
                    IsBgClick = false;
                }
            }
            else if (_Source == "16")
            {
                if (PlayerPrefs.GetInt("WorldBossIsOpen") == 1)
                {
                    StatueInfo.transform.FindChild("SpriteGoBtn").gameObject.SetActive(true);
                    StatueInfo.transform.FindChild("LabelStatue").gameObject.SetActive(false);
                    IsBgClick = true;
                }
                else
                {
                    StatueInfo.transform.FindChild("SpriteGoBtn").gameObject.SetActive(false);
                    StatueInfo.transform.FindChild("LabelStatue").gameObject.SetActive(true);
                    IsBgClick = false;
                }
            }
            else if (_Source == "17")
            {
                if (CharacterRecorder.instance.lastGateID >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zuduifuben).Level)
                {
                    StatueInfo.transform.FindChild("SpriteGoBtn").gameObject.SetActive(true);
                    StatueInfo.transform.FindChild("LabelStatue").gameObject.SetActive(false);
                    IsBgClick = true;
                }
                else
                {
                    StatueInfo.transform.FindChild("SpriteGoBtn").gameObject.SetActive(false);
                    StatueInfo.transform.FindChild("LabelStatue").gameObject.SetActive(true);
                    IsBgClick = false;
                }
            }
            else if (_Source == "19")
            {
                int OpenGate = TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.guozhan).Level;
                if (CharacterRecorder.instance.lastGateID > OpenGate)
                //if (CharacterRecorder.instance.lastGateID >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zuduifuben).Level)
                {
                    StatueInfo.transform.FindChild("SpriteGoBtn").gameObject.SetActive(true);
                    StatueInfo.transform.FindChild("LabelStatue").gameObject.SetActive(false);
                    IsBgClick = true;
                }
                else
                {
                    StatueInfo.transform.FindChild("SpriteGoBtn").gameObject.SetActive(false);
                    StatueInfo.transform.FindChild("LabelStatue").gameObject.SetActive(true);
                    IsBgClick = false;
                }
            }
            else
            {
                StatueInfo.transform.FindChild("SpriteGoBtn").gameObject.SetActive(true);
                StatueInfo.transform.FindChild("LabelStatue").gameObject.SetActive(false);
                IsBgClick = true;
            }
        }
    }

    void SetOtherInfo(string num)
    {
        Debug.Log("source:" + num);
        switch (num)
        {
            case "1":
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "主线";
                LabelDetail.text = "任意主线关卡";
                break;
            case "2":
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "商店";
                LabelDetail.text = "普通商店";
                WindowName = "RankShopWindow";
                break;
            case "3":
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "商店";
                LabelDetail.text = "荣誉商店";
                WindowName = "RankShopWindow";
                break;
            case "4":
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "商店";
                LabelDetail.text = "试炼商店";
                WindowName = "RankShopWindow";
                break;
            case "5":
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "商店";
                LabelDetail.text = "军团商店";
                WindowName = "RankShopWindow";
                break;
            case "6":
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "商店";
                LabelDetail.text = "金条商店";
                WindowName = "RankShopWindow";
                break;
            case "7":
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "招募";
                LabelDetail.text = "抽奖";
                WindowName = "GaChaWindow";
                break;
            case "8":
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "夺宝奇兵";
                LabelDetail.text = "夺宝";
                WindowName = "GrabItemWindow";
                break;
            case "9":
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "其他";
                LabelDetail.text = "丛林冒险";
                WindowName = "WoodsTheExpendables";
                break;
            case "10":
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "其他";
                LabelDetail.text = "世界事件";
                WindowName = "MapUiWindow";
                break;
            case "11":
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "竞技场";
                LabelDetail.text = "竞技场";
                WindowName = "PVPWindow";
                break;
            case "12":
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "王者商店";
                LabelDetail.text = "王者商店";
                WindowName = "RankShopWindow";
                break;
            case "13":
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "军团副本";
                LabelDetail.text = "军团副本";
                WindowName = "LegionCopyWindow";
                break;
            case "14":
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "VIP礼包";
                LabelDetail.text = "VIP礼包";
                WindowName = "VIPShopWindow";
                break;
            case "15":
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "每日副本";
                LabelDetail.text = "每日副本";
                WindowName = "EverydayWindow";
                break;
            case "16":
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "世界Boss";
                LabelDetail.text = "世界Boss";
                WindowName = "WorldBossWindow";
                break;
            case "17":
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "组队副本";
                LabelDetail.text = "组队副本";
                WindowName = "TeamCopyWindow";
                break;
            case "18":
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "活动";
                LabelDetail.text = "特定运营活动";
                WindowName = "TeamCopyWindow";
                this.transform.GetComponent<BoxCollider>().enabled = false;
                LabelGo.enabled = false;
                break;
            case "19":
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "战场杀敌";
                LabelDetail.text = "战场杀敌";
                WindowName = "LegionWarWindow";
                break;
            case "22":
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "核武器";
                LabelDetail.text = "核武器";
                WindowName = "TurntableWindow";
                break;
            case "23":
                ChanceInfo.transform.Find("LabelChance").GetComponent<UILabel>().text = "芯片";
                LabelDetail.text = "芯片";
                WindowName = "ChipWindow";
                break;
        }
    }
}
