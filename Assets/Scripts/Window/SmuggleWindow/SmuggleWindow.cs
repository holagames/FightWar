using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SmuggleWindow : MonoBehaviour
{
    //抢夺界面
    public GameObject SmuggleSelfListWindow;
    public GameObject PlayerListWindow;
    public GameObject EnemyItem;
    public GameObject EnemyGrid;
    public GameObject SumgglistItem;
    public GameObject SmuggleSelfNumber;
    public GameObject SmuggleButton;
    public GameObject SmuggleBlackButton;
    public GameObject QuesButton;
    public List<GameObject> Sumgglist = new List<GameObject>();
    public UILabel FightPower;
    public UILabel SumHaveNumber;
    public UILabel NeedTime;
    public int AllTime;
    public GameObject SmuggleGridOne;
    public GameObject SmuggleGridTwo;
    public Dictionary<string, int> SumgglistDic = new Dictionary<string, int>();
    public List<GameObject> SumggEnemylist = new List<GameObject>();
    //走私列表
    public int SmuggleCarID;
    public GameObject SmuggleOneObj;
    public GameObject SmuggleSecondObj;
    public GameObject SmuggleThridObj;
    public GameObject SmuggleForthObj;
    public GameObject RefreshButton;
    public GameObject RefreshDiamond;
    public UILabel FreeNumber;
    //英雄信息
    public GameObject ItemInfoWindow;
    public GameObject HeroInfoItem;
    public GameObject HeroGrid;
    public List<GameObject> HeroItemList = new List<GameObject>();
    //提示信息
    public GameObject QusMessageInfoWindow;
    public GameObject CloseButton;
    // Use this for initialization
    void Start()
    {
        NetworkHandler.instance.SendProcess("6301#");
        NetworkHandler.instance.SendProcess("6302#");
        UIEventListener.Get(SmuggleButton).onClick += delegate(GameObject go)
        {
            if (int.Parse(SmuggleSelfNumber.GetComponent<UILabel>().text) > 0)
            {
                PlayerListWindow.SetActive(false);
                SmuggleSelfListWindow.SetActive(true);
                NetworkHandler.instance.SendProcess("6304#");
                ShowSmuggleCar();
            }
            else
            {
                UIManager.instance.OpenPromptWindow("刷新次数不足,请提升VIP等级", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(QuesButton).onClick += delegate(GameObject go)
        {
            QusMessageInfoWindow.SetActive(true);
            QusMessageInfo();
        };
    }
    void QusMessageInfo()
    {
        UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
        {
            QusMessageInfoWindow.SetActive(false);
        };
    }
    void CloseSmuggleListWindow()
    {
        NetworkHandler.instance.SendProcess("6301#");
        NetworkHandler.instance.SendProcess("6302#");
        PlayerListWindow.SetActive(true);
        SmuggleSelfListWindow.SetActive(false);
    }
    #region PlayerList
    //抢夺列表
    public void PlayerListInfo(string[] dataSplit)
    {
        for (int i = 0; i < Sumgglist.Count; i++)
        {
            DestroyImmediate(Sumgglist[i]);
        }
        Sumgglist.Clear();
        FightPower.text = CharacterRecorder.instance.Fight.ToString();
        SumHaveNumber.text = dataSplit[0];
        if (int.Parse(dataSplit[1]) > 0)
        {
            SmuggleSelfNumber.SetActive(true);
            SmuggleBlackButton.SetActive(false);
            SmuggleSelfNumber.GetComponent<UILabel>().text = dataSplit[1];
        }
        else
        {
            SmuggleBlackButton.SetActive(true);
            SmuggleSelfNumber.SetActive(false);
        }
        NeedTime.gameObject.SetActive(false);
        if (int.Parse(dataSplit[2]) > 0)
        {
            NeedTime.gameObject.SetActive(true);
            SmuggleButton.SetActive(false);
            SmuggleBlackButton.SetActive(true);
            UpdateTime(int.Parse(dataSplit[2]));
        }
        else if (int.Parse(dataSplit[1]) > 0)
        {        
            SmuggleButton.SetActive(true);
            SmuggleBlackButton.SetActive(false);
        }
        string[] sumgglist = dataSplit[3].Split('!');
        for (int i = 0; i < sumgglist.Length - 1; i++)
        {
            string[] iteminfo = sumgglist[i].Split('$');
            GameObject go = Instantiate(SumgglistItem) as GameObject;
            switch (i % 2)
            {
                case 0:
                    go.transform.parent = SmuggleGridOne.transform;
                    break;
                case 1:
                    go.transform.parent = SmuggleGridTwo.transform;
                    break;
            }
            int nowpos = 1200 - int.Parse(iteminfo[0]);
            go.transform.localPosition = new Vector3(nowpos * 0.5f, 0, 0);
            string CarID = "";
            switch (int.Parse(iteminfo[1]))
            {
                case 1:
                    go.GetComponent<UISprite>().spriteName = "60100";
                    CarID = "摩托车";
                    break;
                case 2:
                    go.GetComponent<UISprite>().spriteName = "60101";
                    CarID = "冲锋车";
                    break;
                case 3:
                    go.GetComponent<UISprite>().spriteName = "60102";
                    CarID = "坦克车";
                    break;
                case 4:
                    go.GetComponent<UISprite>().spriteName = "60200";
                    CarID = "飞机";
                    break;
            }
            go.SetActive(true);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.transform.Find("LevelLabel").GetComponent<UILabel>().text = "Lv." + iteminfo[3];
            go.transform.Find("NameLabel").GetComponent<UILabel>().text = iteminfo[4];
            go.transform.Find("HaveNumLabel").GetComponent<UILabel>().text = iteminfo[6];
            if (int.Parse(iteminfo[6]) == 1)
            {
                go.transform.Find("LoseGold").gameObject.SetActive(true);
                go.transform.Find("LoseGold").GetComponent<UISpriteAnimation>().namePrefix = "MaoYan_";
                go.transform.Find("LoseGold").GetComponent<UISpriteAnimation>().framesPerSecond = 15;
            }
            else if (int.Parse(iteminfo[6]) == 2)
            {
                go.transform.Find("LoseGold").gameObject.SetActive(true);
                go.transform.Find("LoseGold").GetComponent<UISpriteAnimation>().namePrefix = "ZhaoHuo_";
                go.transform.Find("LoseGold").GetComponent<UISpriteAnimation>().framesPerSecond = 10;
            }
            else
            {
                go.transform.Find("LoseGold").gameObject.SetActive(false);
            }
            go.transform.Find("FightLabel").GetComponent<UILabel>().text = "战力：" + iteminfo[5];
            go.GetComponent<SmuggleListItem>().HeroItemInfo(int.Parse(iteminfo[2]), int.Parse(iteminfo[6]), iteminfo[4], CarID, int.Parse(iteminfo[0]));
            //SumgglistDic.Add(iteminfo[4], int.Parse(iteminfo[2]));
            Sumgglist.Add(go);
        }
    }
    public int GetSumgglistByName(string Name)
    {
        if (SumgglistDic.ContainsKey(Name))
        {
            return SumgglistDic[Name];
        }
        return 0;
    }
    //仇人列表
    public void EnemyListInfo(string dataSplit)
    {
        for (int i = 0; i < SumggEnemylist.Count; i++)
        {
            DestroyImmediate(SumggEnemylist[i]);
        }
        SumggEnemylist.Clear();
        string[] enemystr = dataSplit.Split('!');
        for (int i = 0; i < enemystr.Length - 1; i++)
        {
            string[] itemStr = enemystr[i].Split('$');
            foreach (var item in Sumgglist)
            {
                if (itemStr[0] == item.transform.Find("NameLabel").GetComponent<UILabel>().text)
                {
                    GameObject go = Instantiate(EnemyItem) as GameObject;
                    go.transform.parent = EnemyGrid.transform;
                    go.SetActive(true);
                    go.transform.localPosition = new Vector3(0, 0, 0);
                    go.transform.localScale = new Vector3(1, 1, 1);
                    item.GetComponent<SmuggleListItem>().EnemyObjClick(go);
                    //go.GetComponent<UISprite>().spriteName = "yxdi"+TextTranslator.instance.GetItemByItemCode(int.Parse(itemStr[1])).itemCode.ToString();
                    go.transform.Find("HeroIcon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(int.Parse(itemStr[1])).picID.ToString();
                    go.transform.Find("HeroName").GetComponent<UILabel>().text = itemStr[0];
                    SumggEnemylist.Add(go);
                }
            }
        }
        EnemyGrid.GetComponent<UIGrid>().repositionNow = true;
    }
    void UpdateTime(int NowTime)
    {
        CancelInvoke("ShowTime");
        AllTime = NowTime;
        InvokeRepeating("ShowTime", 0, 1.0f);
    }
    void ShowTime()
    {
        if (AllTime >= 0)
        {
            string HourStr = (AllTime / 3600 % 24).ToString("00");
            string MiniStr = (AllTime % 3600 / 60).ToString("00");
            string SecondStr = (AllTime % 60).ToString("00");
            NeedTime.text = HourStr + ":" + MiniStr + ":" + SecondStr;
            AllTime -= 1;
            //SmuggleGridOne.transform.localPosition += new Vector3(0.5f, 0, 0);
            //SmuggleGridTwo.transform.localPosition += new Vector3(0.5f, 0, 0);
        }
        //for (int i = 0; i < Sumgglist.Count; i++)
        //{
        //    switch (i % 2)
        //    {
        //        case 0:
        //            if (Sumgglist[i].transform.localPosition.x + SmuggleGridOne.transform.localPosition.x > 960)
        //            {
        //                DestroyImmediate(Sumgglist[i]);
        //            }
        //            break;
        //        case 1:
        //            if (Sumgglist[i].transform.localPosition.x + SmuggleGridTwo.transform.localPosition.x > 960)
        //            {
        //                DestroyImmediate(Sumgglist[i]);
        //            }
        //            break;
        //    }
        //}
    }

    #endregion


    #region SmuggleSelfList
    public void ShowSmuggleCar()
    {
        SmuggleOneObj.transform.Find("Goldbar").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetSmuggleCarByID(1).Bonus1.ToString();
        SmuggleOneObj.transform.Find("Goldbar/Label").GetComponent<UILabel>().text = TextTranslator.instance.GetSmuggleCarByID(1).BonusNum1.ToString();
        SmuggleSecondObj.transform.Find("Goldbar").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetSmuggleCarByID(2).Bonus1.ToString();
        SmuggleSecondObj.transform.Find("Goldbar/Label").GetComponent<UILabel>().text = TextTranslator.instance.GetSmuggleCarByID(2).BonusNum1.ToString();
        SmuggleThridObj.transform.Find("Goldbar").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetSmuggleCarByID(3).Bonus1.ToString();
        SmuggleThridObj.transform.Find("Goldbar/Label").GetComponent<UILabel>().text = TextTranslator.instance.GetSmuggleCarByID(3).BonusNum1.ToString();
        SmuggleForthObj.transform.Find("Goldbar").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetSmuggleCarByID(4).Bonus1.ToString();
        SmuggleForthObj.transform.Find("Goldbar/Label").GetComponent<UILabel>().text = TextTranslator.instance.GetSmuggleCarByID(4).BonusNum1.ToString();
        if (CharacterRecorder.instance.Vip >= 6)
        {
            SmuggleForthObj.transform.Find("Lock").gameObject.SetActive(false);
        }
        else
        {
            SmuggleForthObj.transform.Find("Lock").gameObject.SetActive(true);
        }
        UIEventListener.Get(SmuggleOneObj.transform.Find("StartButton").gameObject).onClick += delegate(GameObject go)
        {
            if (SmuggleCarID == 1)
            {
                NetworkHandler.instance.SendProcess("6307#");
                CloseSmuggleListWindow();
            }
        };
        UIEventListener.Get(SmuggleSecondObj.transform.Find("StartButton").gameObject).onClick += delegate(GameObject go)
        {
            if (SmuggleCarID == 2)
            {
                NetworkHandler.instance.SendProcess("6307#");
                CloseSmuggleListWindow();
            }
        };
        UIEventListener.Get(SmuggleThridObj.transform.Find("StartButton").gameObject).onClick += delegate(GameObject go)
        {
            if (SmuggleCarID == 3)
            {
                NetworkHandler.instance.SendProcess("6307#");
                CloseSmuggleListWindow();
            }
        };
        UIEventListener.Get(SmuggleForthObj.transform.Find("StartButton").gameObject).onClick += delegate(GameObject go)
        {
            if (SmuggleCarID == 4)
            {
                NetworkHandler.instance.SendProcess("6307#");
                CloseSmuggleListWindow();
            }
        };
        UIEventListener.Get(SmuggleForthObj.transform.Find("EmployButton").gameObject).onClick += delegate(GameObject go)
        {
            if (CharacterRecorder.instance.Vip >= 6)
            {
                NetworkHandler.instance.SendProcess("6306#");
                CloseSmuggleListWindow();
            }
            else
            {
                UIManager.instance.OpenPromptWindow("Vip等级不足,请选择正确走私物资", PromptWindow.PromptType.Hint, null, null);
            }
        };
    }

    public void SmuggleCarInfo(int ResID, int CarID)
    {
        UIEventListener.Get(RefreshButton).onClick = null;
        SmuggleCarID = CarID;
        SmuggleOneObj.transform.Find("BlackButton").gameObject.SetActive(true);
        SmuggleSecondObj.transform.Find("BlackButton").gameObject.SetActive(true);
        SmuggleThridObj.transform.Find("BlackButton").gameObject.SetActive(true);
        SmuggleForthObj.transform.Find("EmployButton").gameObject.SetActive(true);
        switch (CarID)
        {
            case 1:
                SmuggleOneObj.transform.Find("BlackButton").gameObject.SetActive(false);
                SmuggleOneObj.transform.Find("StartButton").gameObject.SetActive(true);
                break;
            case 2:
                SmuggleSecondObj.transform.Find("BlackButton").gameObject.SetActive(false);
                SmuggleSecondObj.transform.Find("StartButton").gameObject.SetActive(true);
                break;
            case 3:
                SmuggleThridObj.transform.Find("BlackButton").gameObject.SetActive(false);
                SmuggleThridObj.transform.Find("StartButton").gameObject.SetActive(true);
                break;
            case 4:
                SmuggleForthObj.transform.Find("EmployButton").gameObject.SetActive(false);
                SmuggleForthObj.transform.Find("StartButton").gameObject.SetActive(true);
                SmuggleForthObj.transform.Find("Lock").gameObject.SetActive(false);
                SmuggleForthObj.transform.Find("CostLabel").gameObject.SetActive(false);
                break;
        }

        RefreshDiamond.SetActive(false);
        if (ResID > 0)
        {
           
            if (ResID >= 21 && ResID < 50)
            {
                ResID = 20;
            }
            else if (ResID >= 50 && ResID < 100)
            {
                ResID = 21;
            }
            else if (ResID > 100)
            {
                ResID = 22;
            }
            int Cost = TextTranslator.instance.GetMarketByID(ResID + 1).SmuggleRefreshDaimondCost;
            if (Cost != 0)
            {
                RefreshDiamond.SetActive(true);
                RefreshDiamond.GetComponent<UILabel>().text = Cost.ToString();
                FreeNumber.gameObject.SetActive(false);
            }
            else
            {
                FreeNumber.text = "免费次数："+(3 - ResID) + "/3";
            }
        }
        else
        {
            FreeNumber.text = "免费次数：3/3";
        }
        UIEventListener.Get(RefreshButton).onClick += delegate(GameObject go)
        {
            if (CharacterRecorder.instance.lunaGem >= int.Parse(RefreshDiamond.GetComponent<UILabel>().text))
            {
                if (SmuggleCarID != 4)
                {
                    NetworkHandler.instance.SendProcess("6305#");
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("最好的运输工具", PromptWindow.PromptType.Hint, null, null);
                }
            }
            else
            {
                UIManager.instance.OpenPromptWindow("钻石不足", PromptWindow.PromptType.Hint, null, null);
            }
        };
    }
    #endregion


}
