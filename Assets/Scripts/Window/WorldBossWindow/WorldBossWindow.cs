using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class WorldBossWindow : MonoBehaviour {

    public GameObject Window1;
    public GameObject Window2;
    public GameObject Window3;
    public GameObject ListWindow;

    public GameObject AwardItem;
    public GameObject ListItem;
    public GameObject ListAwardItem;
    public GameObject BackButton;
    public GameObject uiGrid;
    public GameObject uiListGrid;
    public GameObject uiListGrid2;


    public GameObject GoScenceButton;
    public GameObject GoButton;
    public GameObject LookStandButton;
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;

    public UILabel Time1Label;
    public UILabel StartTimeLabel;
    public UILabel Time3Label;

    public UILabel KilltimeLabel;
    public UILabel KillNameLabel;
    public UILabel RankLabel;
    public UILabel KillNumLabel;
    public GameObject NohaveLabel;
    private int time;
    private string KillBossName;
    private DateTime NowTime;
    private DateTime StartTime;
    private DateTime CloseTime;

	void Start () {
        UIManager.instance.CountSystem(UIManager.Systems.世界boss);
        UIManager.instance.UpdateSystems(UIManager.Systems.世界boss);

        NetworkHandler.instance.SendProcess("6201#;");
        if (UIEventListener.Get(BackButton).onClick == null) 
        {
            UIEventListener.Get(BackButton).onClick += delegate(GameObject go)
            {
                //DestroyImmediate(this.gameObject);
                //UIManager.instance.OpenPanel("MainWindow", true);
                if (UIManager.instance.TheWindowIsStay("ChallengeWindow"))
                {
                    GameCenter.leavelName = "WorldBossWindow";
                    UIManager.instance.BackUI();
                }
                else
                {
                    UIManager.instance.OpenPanel("MainWindow", true);
                }
            };

        }
	}
    public void SetWindowState(string _NowTime,string _StartTime,string _CloseTime,string _KillBossName) //6201
    {
        NowTime = GetTime(_NowTime);
        StartTime = GetTime(_StartTime);
        CloseTime = GetTime(_CloseTime);       
        Debugger.LogError("现在时间" + NowTime);
        Debugger.LogError("开始时间" + StartTime);
        Debugger.LogError("结束时间" + CloseTime);

        KillBossName = _KillBossName;
        if (NowTime > StartTime && _StartTime == "0" && _CloseTime == "0") //特殊情况,开服无数据
        {
            KillBossName = "0";
            SetWindow3((int)(long.Parse(_CloseTime) - long.Parse(_StartTime)));
        }
        else if (NowTime < StartTime&&_CloseTime=="0")
        {
            SetWindow1((int)(long.Parse(_StartTime) - long.Parse(_NowTime)));
        }
        else if (NowTime >= StartTime && _CloseTime == "0")//NowTime < CloseTime
        {
            SetWindow2((int)(long.Parse(_NowTime) - long.Parse(_StartTime)));
        }
        else if (_CloseTime!="0") 
        {
            SetWindow3((int)(long.Parse(_CloseTime) - long.Parse(_StartTime)));
        }
        AudioEditer.instance.PlayLoop("Boss");
    }
    private DateTime GetTime(string timeStamp)
    {
        DateTime dtStart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long lTime = long.Parse(timeStamp + "0000000");
        TimeSpan toNow = new TimeSpan(lTime);
        return dtStart.Add(toNow);
    }
    public void SetWindow1(int time)//世界boss剩余时间
    {
        this.time = time;
        Window1.SetActive(true);
        Window2.SetActive(false);
        Window3.SetActive(false);
        SetRewarWindow();
        CancelInvoke();
        InvokeRepeating("UpdateTime", 0, 1.0f);

        UIEventListener.Get(GoScenceButton).onClick = delegate(GameObject go)
        {
            //UIManager.instance.OpenPanel("WorldBossFightWindow", true);
            CharacterRecorder.instance.IsTimeToOpen = false;
            NetworkHandler.instance.SendProcess("6210#" + CharacterRecorder.instance.characterName + ";" + CharacterRecorder.instance.headIcon + ";");
        };
    }

    public void SetWindow2(int time)//世界bossk开始时间
    {
        Window1.SetActive(false);
        Window2.SetActive(true);
        Window3.SetActive(false);
        CancelInvoke();
        SetRewarWindow();
        if (time < 60f)
        {
            StartTimeLabel.text = "战斗已经开始" + time + "秒，赶紧加入吧";
        }
        else 
        {
            StartTimeLabel.text = "战斗已经开始" + time/60 + "分钟，赶紧加入吧";
        }

        UIEventListener.Get(GoButton).onClick = delegate(GameObject go)
        {
            //UIManager.instance.OpenPanel("WorldBossFightWindow", true);
            CharacterRecorder.instance.IsTimeToOpen = true;
            NetworkHandler.instance.SendProcess("6210#" + CharacterRecorder.instance.characterName + ";" + CharacterRecorder.instance.headIcon + ";");
        };
        //CancelInvoke();
        //InvokeRepeating("UpdateTime2", 0, 1.0f);
    }

    public void SetWindow3(int time)//世界bossk结束
    {
        Window1.SetActive(false);
        Window2.SetActive(false);
        Window3.SetActive(true);
        CancelInvoke();
        SetRewarWindow();
        if (KillBossName == "0" || KillBossName == "" || KillBossName == null)
        {
            Window3.transform.Find("Label1").gameObject.SetActive(false);
            Window3.transform.Find("Label2").gameObject.SetActive(false);
            Window3.transform.Find("Label3").gameObject.SetActive(true);
            KilltimeLabel.gameObject.SetActive(false);
            KillNameLabel.gameObject.SetActive(false);
        }
        else 
        {
            Window3.transform.Find("Label1").gameObject.SetActive(true);
            Window3.transform.Find("Label2").gameObject.SetActive(true);
            Window3.transform.Find("Label3").gameObject.SetActive(false);
            KilltimeLabel.gameObject.SetActive(true);
            KillNameLabel.gameObject.SetActive(true);
            if (time < 60f)
            {
                KilltimeLabel.text = time.ToString() + "秒";
                KillNameLabel.text = KillBossName;
            }
            else
            {
                KilltimeLabel.text = (time / 60).ToString() + "分钟";
                KillNameLabel.text = KillBossName;
            }
        }
        UIEventListener.Get(LookStandButton).onClick = delegate(GameObject go)
        {
            //UIManager.instance.OpenPanel("WorldBossFightWindow", true);
            NetworkHandler.instance.SendProcess("6209#;");
            ListWindow.SetActive(true);
        };
        UIEventListener.Get(ListWindow.transform.Find("All").transform.Find("CloseButton").gameObject).onClick = delegate(GameObject go)
        {
            //UIManager.instance.OpenPanel("WorldBossFightWindow", true);
            ListWindow.SetActive(false);
        };
    }

    void UpdateTime()//刷新时间
    {
        if (time > 0)
        {
            string houreStr = (time / 3600).ToString("00");
            string miniteStr = (time % 3600 / 60).ToString("00");
            string secondStr = (time % 60).ToString("00");
            Time1Label.text = string.Format("{0}:{1}:{2}", houreStr, miniteStr, secondStr);
            time--;
        }
        else if(time==0)
        {
            //Window1.SetActive(false);
            //Window2.SetActive(true);
            //Window3.SetActive(false);
            //SetRewarWindow();
            //StartTimeLabel.text = "战斗已经开始，赶紧加入吧";

            //UIEventListener.Get(GoButton).onClick = delegate(GameObject go)
            //{
            //    NetworkHandler.instance.SendProcess("6210#" + CharacterRecorder.instance.characterName + ";" + CharacterRecorder.instance.headIcon + ";");
            //};
            NetworkHandler.instance.SendProcess("6201#;");
            CancelInvoke();
        }
    }
    void UpdateTime2()//刷新时间
    {
        string houreStr = (time / 3600).ToString("00");
        string miniteStr = (time % 3600 / 60).ToString("00");
        string secondStr = (time % 60).ToString("00");
        Time3Label.text = string.Format("{0}:{1}:{2}", houreStr, miniteStr, secondStr);
        time--;
    }

    void SetRewarWindow() //奖励
    {
        List<Item> BossAwardList = TextTranslator.instance.GetWorldBossRewardListID(1).RewardList;
        for (int i = uiGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGrid.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < BossAwardList.Count; i++) 
        {
            int ItemCode = BossAwardList[i].itemCode;
            int ItemNum = BossAwardList[i].itemCount;
            GameObject go = NGUITools.AddChild(uiGrid, AwardItem);
            go.SetActive(true);
            TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
            go.GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
            TextTranslator.instance.ItemDescription(go, ItemCode, ItemNum);
            if (ItemCode.ToString()[0] == '6')
            {
                go.transform.Find("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
                go.transform.Find("Icon").GetComponent<UISprite>().spriteName = ItemCode.ToString();
                go.transform.Find("Label").GetComponent<UILabel>().text = ItemNum.ToString();
                go.transform.Find("Fragment").gameObject.SetActive(false);
            }
            else if (ItemCode == 70000 || ItemCode == 79999)
            {
                go.transform.Find("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("Icon").GetComponent<UISprite>().spriteName = ItemCode.ToString();
                go.transform.Find("Label").GetComponent<UILabel>().text = ItemNum.ToString();
                go.transform.Find("Fragment").gameObject.SetActive(true);
            }
            else if (ItemCode.ToString()[0] == '7' && ItemCode > 70000)
            {
                go.transform.Find("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
                go.transform.Find("Icon").GetComponent<UISprite>().spriteName = (ItemCode - 10000).ToString();
                go.transform.Find("Label").GetComponent<UILabel>().text = ItemNum.ToString();
                go.transform.Find("Fragment").gameObject.SetActive(true);
            }
            else if (ItemCode.ToString()[0] == '8')
            {
                go.transform.Find("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("Icon").GetComponent<UISprite>().spriteName = (ItemCode - 30000).ToString();
                go.transform.Find("Label").GetComponent<UILabel>().text = ItemNum.ToString();
                go.transform.Find("Fragment").gameObject.SetActive(true);
            }
            else if (ItemCode.ToString()[0] == '2')
            {
                go.transform.Find("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("Icon").GetComponent<UISprite>().spriteName = _ItemInfo.picID.ToString();
                go.transform.Find("Label").GetComponent<UILabel>().text = ItemNum.ToString();
                go.transform.Find("Fragment").gameObject.SetActive(false);
            }
            else
            {
                go.transform.Find("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("Icon").GetComponent<UISprite>().spriteName = ItemCode.ToString();
                go.transform.Find("Label").GetComponent<UILabel>().text = ItemNum.ToString();
                go.transform.Find("Fragment").gameObject.SetActive(false);
            }
            if (ItemNum >= 10000)
            {
                go.transform.Find("Label").GetComponent<UILabel>().text = (ItemNum / 10000) + "." + (ItemNum % 10000 / 1000) + "W";
            }
        }
        GameObject other = NGUITools.AddChild(uiGrid, AwardItem);//增加红色饰品宝箱
        other.SetActive(true);
        TextTranslator.ItemInfo ItemInfo = TextTranslator.instance.GetItemByItemCode(51200);
        other.GetComponent<UISprite>().spriteName = "Grade" + ItemInfo.itemGrade.ToString();
        other.transform.Find("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
        other.transform.Find("Icon").GetComponent<UISprite>().spriteName = "51200";
        other.transform.Find("Label").GetComponent<UILabel>().text = "1";
        other.transform.Find("Fragment").gameObject.SetActive(true);
        TextTranslator.instance.ItemDescription(other, 51200, 0);
        uiGrid.GetComponent<UIGrid>().Reposition();
    }

    public void LookStandList(string RankReceive) //上次战绩
    {
         string[] dataSplit=RankReceive.Split(';');
         string[] trcSplit = dataSplit[2].Split('!');
         for (int i = uiListGrid.transform.childCount - 1; i >= 0; i--)
         {
             DestroyImmediate(uiListGrid.transform.GetChild(i).gameObject);
         }
         for (int i = 0; i < trcSplit.Length - 1; i++) 
         {
             string[] prcSplit=trcSplit[i].Split('$');
             GameObject go = NGUITools.AddChild(uiListGrid, ListItem);
             go.SetActive(true);
             if (int.Parse(prcSplit[0]) < 4)
             {
                 go.transform.Find("Rank").gameObject.SetActive(false);
                 go.transform.Find("TopNumber").gameObject.SetActive(true);
                 go.transform.Find("TopNumber").GetComponent<UISprite>().spriteName = "word" + prcSplit[0];
                 go.transform.Find("PlayerName").GetComponent<UILabel>().text = prcSplit[1];
                 go.transform.Find("KillNumber").GetComponent<UILabel>().text = prcSplit[2];

                 List<Item> BossAwardList = TextTranslator.instance.GetWorldBossRewardListID(int.Parse(prcSplit[0])).RewardList;
                 //go.transform.Find("AwardOne").GetComponent<UISprite>().spriteName = BossAwardList[0].itemCode.ToString();
                 go.transform.Find("AwardOne").transform.Find("Label").GetComponent<UILabel>().text = "x"+BossAwardList[0].itemCount.ToString();
                 //go.transform.Find("AwardTwo").GetComponent<UISprite>().spriteName = BossAwardList[1].itemCode.ToString();
                 go.transform.Find("AwardTwo").transform.Find("Label").GetComponent<UILabel>().text = "x" + BossAwardList[1].itemCount.ToString();
                 SetAwardColor(BossAwardList[0].itemCode, go.transform.Find("AwardOne").GetComponent<UISprite>());
                 SetAwardColor(BossAwardList[1].itemCode, go.transform.Find("AwardTwo").GetComponent<UISprite>());
                 //SetAwardColor()
             }
             else 
             {
                 go.transform.Find("Rank").gameObject.SetActive(true);
                 go.transform.Find("TopNumber").gameObject.SetActive(false);
                 go.transform.Find("Rank").GetComponent<UILabel>().text = prcSplit[0];
                 go.transform.Find("PlayerName").GetComponent<UILabel>().text = prcSplit[1];
                 go.transform.Find("KillNumber").GetComponent<UILabel>().text = prcSplit[2];

                 List<Item> BossAwardList = TextTranslator.instance.GetWorldBossRewardListID(int.Parse(prcSplit[0])).RewardList;
                 //go.transform.Find("AwardOne").GetComponent<UISprite>().spriteName = BossAwardList[0].itemCode.ToString();
                 go.transform.Find("AwardOne").transform.Find("Label").GetComponent<UILabel>().text = "x" + BossAwardList[0].itemCount.ToString();
                 //go.transform.Find("AwardTwo").GetComponent<UISprite>().spriteName = BossAwardList[1].itemCode.ToString();
                 go.transform.Find("AwardTwo").transform.Find("Label").GetComponent<UILabel>().text = "x" + BossAwardList[1].itemCount.ToString();
                 SetAwardColor(BossAwardList[0].itemCode, go.transform.Find("AwardOne").GetComponent<UISprite>());
                 SetAwardColor(BossAwardList[1].itemCode, go.transform.Find("AwardTwo").GetComponent<UISprite>());
             }
             uiListGrid.GetComponent<UIGrid>().Reposition();
         }

         if (dataSplit[0] == "0")
         {
              RankLabel.text = "未上榜";
              NohaveLabel.SetActive(true);
              KillNumLabel.text = dataSplit[1];
         }
         else
         {
              RankLabel.text = dataSplit[0];
              NohaveLabel.SetActive(false);
              KillNumLabel.text = dataSplit[1];
              int worldbossId = 0;
              for (int i = 0; i < TextTranslator.instance.WorldBossRewardList.size; i++)
              {
                  if (TextTranslator.instance.WorldBossRewardList[i].WorldBossRank >= int.Parse(dataSplit[0]))
                  {
                      worldbossId = TextTranslator.instance.WorldBossRewardList[i].WorldBossID;
                      break;
                  }
              }
              for (int i = uiListGrid2.transform.childCount - 1; i >= 0; i--)
              {
                  DestroyImmediate(uiListGrid2.transform.GetChild(i).gameObject);
              }
              for (int i = 0; i < TextTranslator.instance.GetWorldBossRewardListID(worldbossId).RewardList.Count; i++)
              {
                  GameObject go = NGUITools.AddChild(uiListGrid2, ListAwardItem);
                  go.SetActive(true);
                  int ItemCode = TextTranslator.instance.GetWorldBossRewardListID(worldbossId).RewardList[i].itemCode;
                  int ItemNum = TextTranslator.instance.GetWorldBossRewardListID(worldbossId).RewardList[i].itemCount;
                  SetColor(ItemCode, ItemNum, go);
                  TextTranslator.instance.ItemDescription(go, ItemCode, ItemNum);
              }
              uiListGrid2.GetComponent<UIGrid>().Reposition();
         }
    }

    void SetAwardColor(int ItemCode, UISprite uisprite)
    {
        if (ItemCode.ToString()[0] == '6')
        {
            uisprite.atlas = RoleAtlas;
            uisprite.spriteName = ItemCode.ToString();
        }
        else if (ItemCode == 70000 || ItemCode == 79999)
        {
            uisprite.atlas = ItemAtlas;
            uisprite.spriteName = ItemCode.ToString();
        }
        else if (ItemCode.ToString()[0] == '7' && ItemCode > 70000)
        {
            uisprite.atlas = RoleAtlas;
            uisprite.spriteName = ItemCode.ToString();

        }
        else if (ItemCode.ToString()[0] == '8')
        {
            uisprite.atlas = ItemAtlas;
            uisprite.spriteName = ItemCode.ToString();

        }
        else if (ItemCode.ToString()[0] == '2')
        {
            TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
            uisprite.atlas = ItemAtlas;
            uisprite.spriteName = _ItemInfo.picID.ToString();
        }
        else
        {
            uisprite.atlas = ItemAtlas;
            uisprite.spriteName = ItemCode.ToString();
        }
    }


    void SetColor(int ItemCode, int ItemNum, GameObject go)
    {
        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
        go.GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
        if (ItemCode.ToString()[0] == '6')
        {
            go.transform.Find("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
            go.transform.Find("Icon").GetComponent<UISprite>().spriteName = ItemCode.ToString();
            go.transform.Find("Label").GetComponent<UILabel>().text = ItemNum.ToString();
            go.transform.Find("Fragment").gameObject.SetActive(false);
        }
        else if (ItemCode == 70000 || ItemCode == 79999)
        {
            go.transform.Find("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.Find("Icon").GetComponent<UISprite>().spriteName = ItemCode.ToString();
            go.transform.Find("Label").GetComponent<UILabel>().text = ItemNum.ToString();
            go.transform.Find("Fragment").gameObject.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '7' && ItemCode > 70000)
        {
            go.transform.Find("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
            go.transform.Find("Icon").GetComponent<UISprite>().spriteName = (ItemCode - 10000).ToString();
            go.transform.Find("Label").GetComponent<UILabel>().text = ItemNum.ToString();
            go.transform.Find("Fragment").gameObject.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '8')
        {
            go.transform.Find("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.Find("Icon").GetComponent<UISprite>().spriteName = (ItemCode - 30000).ToString();
            go.transform.Find("Label").GetComponent<UILabel>().text = ItemNum.ToString();
            go.transform.Find("Fragment").gameObject.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '2')
        {
            go.transform.Find("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.Find("Icon").GetComponent<UISprite>().spriteName = _ItemInfo.picID.ToString();
            go.transform.Find("Label").GetComponent<UILabel>().text = ItemNum.ToString();
            go.transform.Find("Fragment").gameObject.SetActive(false);
        }
        else
        {
            go.transform.Find("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.Find("Icon").GetComponent<UISprite>().spriteName = ItemCode.ToString();
            go.transform.Find("Label").GetComponent<UILabel>().text = ItemNum.ToString();
            go.transform.Find("Fragment").gameObject.SetActive(false);
        }
        if (ItemNum >= 10000) 
        {
            go.transform.Find("Label").GetComponent<UILabel>().text = (ItemNum / 10000) + "." + (ItemNum % 10000/1000) + "W";
        }
    }
}
