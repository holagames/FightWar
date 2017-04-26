using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LegionRedPacketWindow : MonoBehaviour {

    public GameObject QuestionButton;
    public GameObject QuestionMessage;

    public GameObject JuntuanButton;
    public GameObject TuhaoButton;
    public GameObject ChongzhiButton;
    public GameObject QiangButton;

    public GameObject LegionRedTab;
    public GameObject RichRedneckTab;
    public GameObject RechargeRedTab;
    public GameObject GrabRedTab;

    public GameObject RedPacketItem;
    //juntuan
    public GameObject JuntuanGrid;

    //土豪
    public GameObject TuhaoGrid;
    public GameObject TuhaoRankButton;
    public GameObject TuhaoBottom;


    //充值
    public GameObject ChongzhiGrid;
    public GameObject ChongzhiRankButton;
    public GameObject TimeLabel;
    public GameObject AwardButton;
    

    //抢红包
    public GameObject ItemLabel;
    public GameObject ItemInfo;
    public UILabel TopNumLabel;
    public GameObject QiangLeftGrid;
    public GameObject QiangRightGrid;
    public GameObject QiangRankButton;

    public GameObject LookChongInfo;

    public GameObject[] MessageAwardArr;
    private List<RedPacketItem> RedPacketItemList = new List<RedPacketItem>();



    private int CanQiangRedNum = 0;//可以抢红包的次数
     
	void Start () {
        SetLegionRedPoint();
        NetworkHandler.instance.SendProcess("8701#;");
        TopTabsChange(1);
        UIEventListener.Get(AwardButton).onClick = delegate(GameObject go)
        {
            UIManager.instance.OpenPanel("VIPShopWindow", true);
        };


        UIEventListener.Get(JuntuanButton).onClick = delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("8701#;");
        };

        UIEventListener.Get(TuhaoButton).onClick = delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("8703#;");
        };

        UIEventListener.Get(ChongzhiButton).onClick = delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("8705#;");
        };

        UIEventListener.Get(QiangButton).onClick = delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("8707#;");
            NetworkHandler.instance.SendProcess("8708#;");
        };
        UIEventListener.Get(QuestionButton).onClick = delegate(GameObject go)
        {
            QuestionMessage.SetActive(true);
        };

        UIEventListener.Get(QuestionMessage.transform.Find("CloseButton").gameObject).onClick = delegate(GameObject go)
        {
            QuestionMessage.SetActive(false);
        };

        UIEventListener.Get(TuhaoRankButton).onClick = delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("8710#;");
        };

        UIEventListener.Get(ChongzhiRankButton).onClick = delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("8711#;");
        };

        UIEventListener.Get(QiangRankButton).onClick = delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("8712#;");
            //NetworkHandler.instance.SendProcess("8713#" + 3 + ";");
        };


        SetMessageInfo();

        //SuoBnaben();
	}



    void SuoBnaben() 
    {
        //if (id == 2)
        {
            TuhaoButton.transform.Find("Sprite").gameObject.SetActive(false);
        }
        //else if (id == 3)
        {
            ChongzhiButton.transform.Find("Sprite").gameObject.SetActive(false);
        }
        //else if (id == 4)
        {
            QiangButton.transform.Find("Sprite").gameObject.SetActive(false);
        }
        TuhaoButton.GetComponent<UIToggle>().enabled = false;
        ChongzhiButton.GetComponent<UIToggle>().enabled = false;
        QiangButton.GetComponent<UIToggle>().enabled = false;
        UIEventListener.Get(TuhaoButton).onClick = delegate(GameObject go)
        {
            UIManager.instance.OpenPromptWindow("暂未开放!", PromptWindow.PromptType.Hint, null, null);
        };

        UIEventListener.Get(ChongzhiButton).onClick = delegate(GameObject go)
        {
            UIManager.instance.OpenPromptWindow("暂未开放!", PromptWindow.PromptType.Hint, null, null);
        };

        UIEventListener.Get(QiangButton).onClick = delegate(GameObject go)
        {
            UIManager.instance.OpenPromptWindow("暂未开放!", PromptWindow.PromptType.Hint, null, null);
        };
    }

    void SetMessageInfo() //设置说明界面奖励
    {
        int lv4, lv5, lv6, lv7, lv8, lv9, lv10;
        lv4 = lv5 = lv6 = lv7 = lv8 = lv9 = lv10 = 0;
        foreach (var item in TextTranslator.instance.LegionRedBagList) 
        {
            if (item.RedBagType == 320001) 
            {
                switch (item.LegionLevel) 
                {
                    case 4: lv4++;
                        GameObject lv4Item=MessageAwardArr[0].transform.Find(("Item"+lv4)).gameObject;
                        lv4Item.SetActive(true);
                        lv4Item.GetComponent<UISprite>().spriteName = item.GetRewardItemId1.ToString();
                        lv4Item.transform.Find("Label").GetComponent<UILabel>().text = item.GetRewardItemNum1.ToString();                     
                        break;
                    case 5: lv5++;
                        GameObject lv5Item = MessageAwardArr[1].transform.Find(("Item" + lv5)).gameObject;
                        lv5Item.SetActive(true);
                        lv5Item.GetComponent<UISprite>().spriteName = item.GetRewardItemId1.ToString();
                        lv5Item.transform.Find("Label").GetComponent<UILabel>().text = item.GetRewardItemNum1.ToString();
                        break;
                    case 6: lv6++;
                        GameObject lv6Item = MessageAwardArr[2].transform.Find(("Item" + lv6)).gameObject;
                        lv6Item.SetActive(true);
                        lv6Item.GetComponent<UISprite>().spriteName = item.GetRewardItemId1.ToString();
                        lv6Item.transform.Find("Label").GetComponent<UILabel>().text = item.GetRewardItemNum1.ToString();
                        break;
                    case 7: lv7++;
                        GameObject lv7Item = MessageAwardArr[3].transform.Find(("Item" + lv7)).gameObject;
                        lv7Item.SetActive(true);
                        lv7Item.GetComponent<UISprite>().spriteName = item.GetRewardItemId1.ToString();
                        lv7Item.transform.Find("Label").GetComponent<UILabel>().text = item.GetRewardItemNum1.ToString();
                        break;
                    case 8: lv8++;
                        GameObject lv8Item = MessageAwardArr[4].transform.Find(("Item" + lv8)).gameObject;
                        lv8Item.SetActive(true);
                        lv8Item.GetComponent<UISprite>().spriteName = item.GetRewardItemId1.ToString();
                        lv8Item.transform.Find("Label").GetComponent<UILabel>().text = item.GetRewardItemNum1.ToString();
                        break;
                    case 9: lv9++;
                        GameObject lv9Item = MessageAwardArr[5].transform.Find(("Item" + lv9)).gameObject;
                        lv9Item.SetActive(true);
                        lv9Item.GetComponent<UISprite>().spriteName = item.GetRewardItemId1.ToString();
                        lv9Item.transform.Find("Label").GetComponent<UILabel>().text = item.GetRewardItemNum1.ToString();
                        break;
                    case 10: lv10++;
                        GameObject lv10Item = MessageAwardArr[6].transform.Find(("Item" + lv10)).gameObject;
                        lv10Item.SetActive(true);
                        lv10Item.GetComponent<UISprite>().spriteName = item.GetRewardItemId1.ToString();
                        lv10Item.transform.Find("Label").GetComponent<UILabel>().text = item.GetRewardItemNum1.ToString();
                        break;
                }
            }
        }
    }

    void TopTabsChange(int id) 
    {
        if (id != 1)
        {
            JuntuanButton.GetComponent<UIToggle>().value = false;
            JuntuanButton.GetComponent<UIToggle>().startsActive = false;
            if (id == 2)
            {
                TuhaoButton.GetComponent<UIToggle>().value = true;
            }
            else if (id == 3)
            {
                ChongzhiButton.GetComponent<UIToggle>().value = true;
            }
            else if (id == 4)
            {
                QiangButton.GetComponent<UIToggle>().value = true;
            }
        }
        else
        {
            JuntuanButton.GetComponent<UIToggle>().value = true;
            JuntuanButton.GetComponent<UIToggle>().startsActive = true;
        }
    }


    public void JuntuanRedPacket(string Recving) //8701
    {
        RedPacketItemList.Clear();
        JuntuanGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();

        for (int i = JuntuanGrid.transform.childCount - 1; i >= 0; i--) 
        {
            DestroyImmediate(JuntuanGrid.transform.GetChild(i).gameObject);
        }

        LegionRedTab.SetActive(true);
        RichRedneckTab.SetActive(false);
        RechargeRedTab.SetActive(false);
        GrabRedTab.SetActive(false);
        //JuntuanGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        string[] dataSplit = Recving.Split(';');
        for (int i = 0; i < dataSplit.Length-1; i++)
        {
            string[] trcSplit = dataSplit[i].Split('$');
            GameObject go = NGUITools.AddChild(JuntuanGrid, RedPacketItem);
            go.SetActive(true);
            RedPacketItem RP=new RedPacketItem();
            LegionRedBag LR = TextTranslator.instance.GetLegionRedBagByID(int.Parse(trcSplit[0]));
            RedPacketItemList.Add(RP);
            go.name = trcSplit[0];
            RP.SetJuntuanIteminfo(go, LR.LegionRedID, LR.ItemName, int.Parse(trcSplit[3]), LR.LegionLevel, LR.ItemIcon, LR.GetRewardItemNum1, int.Parse(trcSplit[1]), int.Parse(trcSplit[2]), trcSplit[4]);
        }
        //JuntuanGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        JuntuanGrid.GetComponent<UIGrid>().Reposition();
       
        JuntuanGrid.transform.parent.localPosition = new Vector3(0, -122f, 0);
        JuntuanGrid.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2(0, 0);
        JuntuanGrid.transform.localPosition = new Vector3(-300f, 72f, 0);
        SetLegionRedPoint();
    }

    public void TuhaoRedPacket(string Recving)//8703
    {
        //TuhaoGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        for (int i = TuhaoGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(TuhaoGrid.transform.GetChild(i).gameObject);
        }

        LegionRedTab.SetActive(false);
        RichRedneckTab.SetActive(true);
        RechargeRedTab.SetActive(false);
        GrabRedTab.SetActive(false);
        RedPacketItemList.Clear();

        string[] dataSplit = Recving.Split(';');
        string[] trcSplit = dataSplit[2].Split('$');

        int num0=int.Parse(dataSplit[1])-int.Parse(dataSplit[0]);
        int num1=int.Parse(dataSplit[1]);

        for (int i = 0; i < trcSplit.Length - 1; i++) 
        {
            GameObject go = NGUITools.AddChild(TuhaoGrid, RedPacketItem);
            go.SetActive(true);
            RedPacketItem RP = new RedPacketItem();
            LegionRedBag LR = TextTranslator.instance.GetLegionRedBagByID(int.Parse(trcSplit[i]));
            RedPacketItemList.Add(RP);
            RP.SetTuhaoIteminfo(go,LR.LegionRedID, LR.ItemName, LR.LegionLevel, LR.ItemIcon, num0);
        }
        //TuhaoGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        TuhaoGrid.GetComponent<UIGrid>().Reposition();

        TuhaoGrid.transform.parent.localPosition = new Vector3(0, -49f, 0);
        TuhaoGrid.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2(0, 0);
        TuhaoGrid.transform.localPosition = new Vector3(-300f, -2f, 0);


        if (num0 == 0)
        {
            TuhaoBottom.transform.Find("AllNumLabel").GetComponent<UILabel>().text = "[fb2d50]" + num0 + "/" + num1 + "[-]";
        }
        else 
        {
            TuhaoBottom.transform.Find("AllNumLabel").GetComponent<UILabel>().text = "[ffffff]" + num0 + "/" + num1 + "[-]";
        }
        SetLegionRedPoint();
    }

    public void ChongzhiRedPacket(string Recving) //8705
    {
        string[] dataSplit = Recving.Split(';');
        DateTime StartTime = Downloader.GetTime(dataSplit[dataSplit.Length - 5]);
        DateTime NowTime = Downloader.GetTime(dataSplit[dataSplit.Length - 4]);
        DateTime EndTime = Downloader.GetTime(dataSplit[dataSplit.Length - 3]);

        string hh = StartTime.ToString("yyyy/MM/dd/HH:mm");
        string ehh = EndTime.ToString("yyyy/MM/dd/HH:mm");

        Debug.Log(StartTime);
        Debug.Log(NowTime);
        Debug.Log(EndTime);
        if (NowTime >= StartTime && NowTime < EndTime) //活动开始
        {
            Debug.Log("进入");
            RedPacketItemList.Clear();
            TimeLabel.GetComponent<UILabel>().text = hh + "~" + ehh;
            //ChongzhiGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
            for (int i = ChongzhiGrid.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(ChongzhiGrid.transform.GetChild(i).gameObject);
            }

            LegionRedTab.SetActive(false);
            RichRedneckTab.SetActive(false);
            RechargeRedTab.SetActive(true);
            GrabRedTab.SetActive(false);

            for (int i = 0; i < dataSplit.Length - 5; i++) 
            {
                string[] trcSplit = dataSplit[i].Split('$');
                GameObject go = NGUITools.AddChild(ChongzhiGrid, RedPacketItem);
                go.SetActive(true);
                RedPacketItem RP = new RedPacketItem();
                LegionRedBag LR = TextTranslator.instance.GetLegionRedBagByID(int.Parse(trcSplit[0]));
                RedPacketItemList.Add(RP);
                RP.SetChongzhiIteminfo(go, LR.LegionRedID, LR.ItemName, LR.LegionLevel, LR.ItemIcon, int.Parse(trcSplit[1]));
            }
            //ChongzhiGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
            ChongzhiGrid.GetComponent<UIGrid>().Reposition();

            ChongzhiGrid.transform.parent.localPosition = new Vector3(0, -38f, 0);
            ChongzhiGrid.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2(0, 0);
            ChongzhiGrid.transform.localPosition = new Vector3(-300f, -2f, 0);
        }
        SetLegionRedPoint();
    }

    public void SetLookChongInfo(int RedID) //充值查看
    {
        LookChongInfo.SetActive(true);

        Debug.Log("RedID" + RedID);
        LegionRedBag LR = TextTranslator.instance.GetLegionRedBagByID(RedID);
        GameObject item1 = LookChongInfo.transform.Find("BG/Item1").gameObject;
        GameObject item2 = LookChongInfo.transform.Find("BG/Item2").gameObject;
        GameObject item3 = LookChongInfo.transform.Find("BG/Item3").gameObject;
        GameObject item4 = LookChongInfo.transform.Find("BG/Item4").gameObject;
        LookChongInfo.transform.Find("BG/TitleLabel").GetComponent<UILabel>().text = "充值" + LR.Diamond + "元将获得";


        if (LR.RewardItemId1 > 0)
        {
            item1.SetActive(true);
            TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(LR.RewardItemId1);
            item1.GetComponent<UISprite>().spriteName= "Grade" + _ItemInfo.itemGrade.ToString();
            item1.transform.Find("Icon").GetComponent<UISprite>().spriteName = LR.RewardItemId1.ToString();
            item1.transform.Find("Label").GetComponent<UILabel>().text = LR.RewardItemNum1.ToString();
        }
        else 
        {
            item1.SetActive(false);
        }


        if (LR.RewardItemId2 > 0)
        {
            item2.SetActive(true);
            TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(LR.RewardItemId2);
            item2.GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
            item2.transform.Find("Icon").GetComponent<UISprite>().spriteName = LR.RewardItemId2.ToString();
            item2.transform.Find("Label").GetComponent<UILabel>().text = LR.RewardItemNum2.ToString();
        }
        else 
        {
            item2.SetActive(false);
        }

        if (LR.RewardItemId3 > 0)
        {
            item3.SetActive(true);
            TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(LR.RewardItemId3);
            item3.GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
            item3.transform.Find("Icon").GetComponent<UISprite>().spriteName = LR.RewardItemId3.ToString();
            item3.transform.Find("Label").GetComponent<UILabel>().text = LR.RewardItemNum3.ToString();
        }
        else 
        {
            item3.SetActive(false);
        }

        if (LR.GetRewardItemId1 > 0)
        {
            item4.SetActive(true);
            TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(LR.GetRewardItemId1);
            item4.GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
            item4.transform.Find("Icon").GetComponent<UISprite>().spriteName = LR.GetRewardItemId1.ToString();
            item4.transform.Find("Label").GetComponent<UILabel>().text = LR.GetRewardItemNum1.ToString();
        }
        else
        {
            item4.SetActive(false);
        }

        UIEventListener.Get(LookChongInfo.transform.Find("BG/CloseButton").gameObject).onClick = delegate(GameObject go)
        {
            LookChongInfo.SetActive(false);
        };
    }


    public void QiangRedPacket(string Recving) //8708
    {
        string[] dataSplit = Recving.Split(';');
        LegionRedTab.SetActive(false);
        RichRedneckTab.SetActive(false);
        RechargeRedTab.SetActive(false);
        GrabRedTab.SetActive(true);
        RedPacketItemList.Clear();

        QiangRightGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        for (int i = QiangRightGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(QiangRightGrid.transform.GetChild(i).gameObject);
        }

        if (dataSplit[0] != "") 
        {
            int length = dataSplit.Length;
            for (int i = length - 2; i>=0; i--)
            {
                if (i + 100 >= length) //100条数据
                {
                    string[] trcSplit = dataSplit[i].Split('$');
                    GameObject go = NGUITools.AddChild(QiangRightGrid, ItemInfo);
                    go.SetActive(true);
                    go.name = (i + 1).ToString();   //名称用来确定位置
                    RedPacketItem RP = new RedPacketItem();
                    LegionRedBag LR = TextTranslator.instance.GetLegionRedBagByID(int.Parse(trcSplit[0]));
                    RedPacketItemList.Add(RP);
                    RP.SetQiangRedIteminfo(go, LR.LegionRedID, LR.ItemName, LR.ItemIcon, trcSplit[1], int.Parse(trcSplit[2]), int.Parse(trcSplit[3]), int.Parse(trcSplit[4]));
                }
            }
            QiangRightGrid.GetComponent<UIGrid>().Reposition();
        }
        SetLegionRedPoint();
    }


    public void GetGiftCharacterInfo(string Recving) //8707
    {
        string[] dataSplit = Recving.Split(';');
        int MyGetRedNum = int.Parse(dataSplit[0]);
        int MyCanRedNum = int.Parse(dataSplit[1]);
        CanQiangRedNum = int.Parse(dataSplit[0]);
        TopNumLabel.text = dataSplit[0] + "/" + dataSplit[1];

        string[] trcSplit=dataSplit[2].Split('!');
        QiangLeftGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        for (int i = QiangLeftGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(QiangLeftGrid.transform.GetChild(i).gameObject);
        }


        int length = trcSplit.Length-1;
        for (int i = length - 1; i >= 0; i--) 
        {
            if (length - i <= 20) //20条数据
            {
                string[] prcSplit = trcSplit[i].Split('$');
                GameObject go = NGUITools.AddChild(QiangLeftGrid, ItemLabel);
                go.SetActive(true);
                go.GetComponent<UILabel>().text = "您抢到" + prcSplit[0] + "的红包,获得" + prcSplit[2] + TextTranslator.instance.GetItemNameByItemCode(int.Parse(prcSplit[1]));
            }
        }
        QiangLeftGrid.GetComponent<UIGrid>().Reposition();
        SetLegionRedPoint();
    }


    public int GetCanGetQiangRedNum() //取得本人可抢红包总次数
    {
        return this.CanQiangRedNum;
    }


    public void SetGiftMoney(string Recving) //8709  领红包后advance信息
    {
        string[] dataSplit=Recving.Split('$');
        for (int i = 0; i < RedPacketItemList.Count; i++) 
        {
            if (RedPacketItemList[i].QiangObj.name == dataSplit[0]) 
            {
                LegionRedBag LR = TextTranslator.instance.GetLegionRedBagByID(int.Parse(dataSplit[1]));
                RedPacketItemList[i].SetQiangRedIteminfo(RedPacketItemList[i].QiangObj, LR.LegionRedID, LR.ItemName, LR.ItemIcon, dataSplit[2], int.Parse(dataSplit[3]), int.Parse(dataSplit[4]), int.Parse(dataSplit[5]));
                break;
            }
        }
    }


    private void SetLegionRedPoint() //红包红点开启
    {
        if (CharacterRecorder.instance.isOpenRechargeRed)
        {
            ChongzhiButton.SetActive(true);
            ChongzhiButton.transform.parent.GetComponent<UIGrid>().Reposition();
        }
        else
        {
            ChongzhiButton.SetActive(false);
            ChongzhiButton.transform.parent.GetComponent<UIGrid>().Reposition();
        }




        if (CharacterRecorder.instance.isLegionRedPoint)
        {
            JuntuanButton.transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else 
        {
            JuntuanButton.transform.Find("RedPoint").gameObject.SetActive(false);
        }

        if (CharacterRecorder.instance.isRichRedneckPoint)
        {
            TuhaoButton.transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else
        {
            TuhaoButton.transform.Find("RedPoint").gameObject.SetActive(false);
        }

        if (CharacterRecorder.instance.isRechargeRedPoint)
        {
            ChongzhiButton.transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else
        {
            ChongzhiButton.transform.Find("RedPoint").gameObject.SetActive(false);
        }

        if (CharacterRecorder.instance.isGrabRedPoint)
        {
            QiangButton.transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else
        {
            QiangButton.transform.Find("RedPoint").gameObject.SetActive(false);
        }
    }
}












public class RedPacketItem
{
    public GameObject Obj;      //红包预制
    public GameObject QiangObj; //抢红包预制
    public int RedID;           //红包ID
    public string TopName;      //红包名称
    public int RedState;        //红包领取状态
    public int OpenLegionLevel; //解锁等级
    public int ItemId;          //icon
    public int AllNum;          //红包金额总量
    public int NowNum;          //红包已经领取数量
    public int NowAllNum;       //红包总数量
    public string FirstName;    //第一名领取红包玩家

    public int RedNum;          //发红包次数

    public int ChongzhiNum;     //充值钱数


    public string SendRedName;  //发红包玩家名字
    public int GetRedNum;       //已经被抢数量
    public int CanGetRedNum;    //可抢数量
    public int MyGetState;      //自己抢红包的状态


    //军团红包
    public void SetJuntuanIteminfo(GameObject go, int _RedID, string _TopName, int _RedState, int _OpenLegionLevel, int _ItemId, int _AllNum, int _NowNum, int _NowAllNum, string _FirstName) 
    {
        this.Obj = go;
        this.RedID = _RedID;
        this.TopName = _TopName;
        this.RedState = _RedState;
        this.OpenLegionLevel = _OpenLegionLevel;
        this.ItemId = _ItemId;
        this.AllNum = _AllNum;
        this.NowNum = _NowNum;
        this.NowAllNum = _NowAllNum;
        this.FirstName = _FirstName;
        if (UIEventListener.Get(Obj).onClick == null)
        {
            UIEventListener.Get(Obj).onClick = delegate(GameObject go2)
            {
                if (OpenLegionLevel > CharacterRecorder.instance.myLegionData.legionLevel)
                {
                    UIManager.instance.OpenPromptWindow("军团等级不足，需" + OpenLegionLevel + "级军团开启该档红包!", PromptWindow.PromptType.Hint, null, null);
                }
                else if (NowNum == NowAllNum) 
                {
                    UIManager.instance.OpenPromptWindow("红包已经被抢光了！下次19：00记得准时来抢呦~", PromptWindow.PromptType.Hint, null, null);
                }
                else if (RedState == 0)
                {
                    UIManager.instance.OpenPromptWindow("红包不可领取", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    NetworkHandler.instance.SendProcess("8702#" + TextTranslator.instance.GetLegionRedBagByID(RedID).SendBagType + ";");
                }
            };
        }

        TextTranslator.instance.ItemDescription(Obj.transform.Find("ItemSprite").gameObject, ItemId, 0);
        ChangeJuntuanItem();
    }

    public void ChangeJuntuanItem() 
    {
        Obj.transform.Find("TopLabel").GetComponent<UILabel>().text = TopName;
        Obj.transform.Find("ItemSprite").GetComponent<UISprite>().spriteName = ItemId.ToString();
        if (RedState == 1)
        {
            Obj.transform.Find("StateSprite").gameObject.SetActive(false);
            Obj.transform.Find("StateSprite2").gameObject.SetActive(false);//修复
            Obj.transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else if (RedState == 2)
        {
            Obj.transform.Find("StateSprite").gameObject.SetActive(true);
            Obj.transform.Find("StateSprite2").gameObject.SetActive(false);
            Obj.transform.Find("RedPoint").gameObject.SetActive(false);
        }

        if (OpenLegionLevel <= CharacterRecorder.instance.myLegionData.legionLevel)
        {
            Obj.transform.Find("SuoSprite").gameObject.SetActive(false);
        }
        else 
        {
            Obj.transform.Find("SuoSprite").gameObject.SetActive(true);
            Obj.transform.Find("SuoSprite/SuoLabel").GetComponent<UILabel>().text = "军团" + OpenLegionLevel + "级解锁";
        }
        Obj.transform.Find("Bottom").gameObject.SetActive(true);
        Obj.transform.Find("Bottom/AllNumLabel").GetComponent<UILabel>().text = AllNum.ToString() + TextTranslator.instance.GetItemNameByItemCode(ItemId);
        Obj.transform.Find("Bottom/OneNumLabel").GetComponent<UILabel>().text = NowNum + "/" + NowAllNum;
        if (FirstName == "")
        {
            Obj.transform.Find("Bottom/FirstNameLabel").GetComponent<UILabel>().text = "无";
        }
        else 
        {
            Obj.transform.Find("Bottom/FirstNameLabel").GetComponent<UILabel>().text = FirstName;
        }   
    }

    //土豪红包
    public void SetTuhaoIteminfo(GameObject go, int _RedID, string _TopName, int _OpenLegionLevel, int _ItemId, int _RedNum) 
    {
        this.Obj = go;
        this.TopName = _TopName;
        this.ItemId = _ItemId;
        this.RedNum = _RedNum;
        this.RedID = _RedID;
        this.OpenLegionLevel = _OpenLegionLevel;
        if (UIEventListener.Get(Obj).onClick == null)
        {
            UIEventListener.Get(Obj).onClick = delegate(GameObject go2)
            {
                if (OpenLegionLevel > CharacterRecorder.instance.myLegionData.legionLevel)
                {
                    UIManager.instance.OpenPromptWindow("军团等级不足，需" + OpenLegionLevel + "级军团开启该档红包!", PromptWindow.PromptType.Hint, null, null);
                }
                else if (RedNum == 0)
                {
                    UIManager.instance.OpenPromptWindow("所需发红包的次数不足,19：00刷新发送红包次数.", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    UIManager.instance.OpenPanel("RedPacketsRankWindow", false);
                    GameObject.Find("RedPacketsRankWindow").GetComponent<RedPacketsRankWindow>().SetChoseRedPartOpen(TextTranslator.instance.GetLegionRedBagByID(RedID).SendBagType);
                }
            };
        }
        ChangeTuhaoItem();
        TextTranslator.instance.ItemDescription(Obj.transform.Find("ItemSprite").gameObject, ItemId, 0);
    }

    public void ChangeTuhaoItem() 
    {
        Obj.transform.Find("TopLabel").GetComponent<UILabel>().text = TopName;
        Obj.transform.Find("ItemSprite").GetComponent<UISprite>().spriteName = ItemId.ToString();
        if (OpenLegionLevel <= CharacterRecorder.instance.myLegionData.legionLevel)
        {
            Obj.transform.Find("SuoSprite").gameObject.SetActive(false);
        }
        else
        {
            Obj.transform.Find("SuoSprite").gameObject.SetActive(true);
            Obj.transform.Find("SuoSprite/SuoLabel").GetComponent<UILabel>().text = "军团" + OpenLegionLevel + "级解锁";
        }
    }

    //充值红包
    public void SetChongzhiIteminfo(GameObject go, int _RedID, string _TopName, int _OpenLegionLevel, int _ItemId, int _ChongzhiNum) 
    {
        this.Obj = go;
        this.RedID = _RedID;
        this.TopName = _TopName;
        this.OpenLegionLevel = _OpenLegionLevel;
        this.ItemId = _ItemId;
        this.ChongzhiNum = _ChongzhiNum;

        if (UIEventListener.Get(Obj).onClick == null)
        {
            UIEventListener.Get(Obj).onClick = delegate(GameObject go2)
            {
                if (OpenLegionLevel > CharacterRecorder.instance.myLegionData.legionLevel)
                {
                    UIManager.instance.OpenPromptWindow("军团等级不足，需" + OpenLegionLevel + "级军团开启该档红包!", PromptWindow.PromptType.Hint, null, null);
                }
                else if (ChongzhiNum == 0)
                {
                    LegionRedPacketWindow LR = GameObject.Find("LegionRedPacketWindow").GetComponent<LegionRedPacketWindow>();
                    LR.SetLookChongInfo(RedID);
                    //UIManager.instance.OpenPromptWindow("今日累计充值" + TextTranslator.instance.GetLegionRedBagByID(RedID).Diamond+ "元后才可领取该档红包!", PromptWindow.PromptType.Hint, null, null);
                }
                else if (ChongzhiNum == 1)
                {
                    NetworkHandler.instance.SendProcess("8706#" + TextTranslator.instance.GetLegionRedBagByID(RedID).SendBagType + ";");
                }
                else if (ChongzhiNum == 2) 
                {
                    UIManager.instance.OpenPromptWindow("您已发过并领取了该档奖励的红包!", PromptWindow.PromptType.Hint, null, null);
                }
                //else 
                //{
                //    LegionRedPacketWindow LR = GameObject.Find("LegionRedPacketWindow").GetComponent<LegionRedPacketWindow>();
                //    LR.SetLookChongInfo(RedID);
                //}
            };
        }
        ChangeChongzhiItem();
        TextTranslator.instance.ItemDescription(Obj.transform.Find("ItemSprite").gameObject, ItemId, 0);
    }

    public void ChangeChongzhiItem() 
    {
        Obj.transform.Find("TopLabel").GetComponent<UILabel>().text = TopName;
        Obj.transform.Find("ItemSprite").GetComponent<UISprite>().spriteName = ItemId.ToString();
        if (OpenLegionLevel <= CharacterRecorder.instance.myLegionData.legionLevel)
        {
            Obj.transform.Find("SuoSprite").gameObject.SetActive(false);
        }
        else
        {
            Obj.transform.Find("SuoSprite").gameObject.SetActive(true);
            Obj.transform.Find("SuoSprite/SuoLabel").GetComponent<UILabel>().text = "军团" + OpenLegionLevel + "级解锁";
        }

        if (ChongzhiNum == 1)
        {
            Obj.transform.Find("RedPoint").gameObject.SetActive(true);
            Obj.transform.Find("StateSprite2").gameObject.SetActive(false);
        }
        else if (ChongzhiNum == 2)
        {
            Obj.transform.Find("RedPoint").gameObject.SetActive(false);
            Obj.transform.Find("StateSprite2").gameObject.SetActive(true);
        }
    }


    //抢红包
    public void SetQiangRedIteminfo(GameObject go, int _RedID, string _TopName, int _ItemId, string _SendRedName, int _GetRedNum, int _CanGetRedNum, int _MyGetState) 
    {
        this.QiangObj = go;
        this.RedID = _RedID;
        this.TopName = _TopName;
        this.ItemId = _ItemId;
        this.SendRedName = _SendRedName;
        this.GetRedNum = _GetRedNum;
        this.CanGetRedNum = _CanGetRedNum;
        this.MyGetState = _MyGetState;

        if (UIEventListener.Get(QiangObj).onClick == null)
        {
            UIEventListener.Get(QiangObj).onClick = delegate(GameObject go2)
            {
                LegionRedPacketWindow LR = GameObject.Find("LegionRedPacketWindow").GetComponent<LegionRedPacketWindow>();
                if (LR != null) 
                {
                    int num = LR.GetCanGetQiangRedNum();
                    if (num == 0)
                    {
                        UIManager.instance.OpenPromptWindow("您的抢红包次数不足!", PromptWindow.PromptType.Hint, null, null);
                    }
                    else 
                    {
                        NetworkHandler.instance.SendProcess("8709#" + go.name + ";");
                    }
                }
                //if (GetRedNum == CanGetRedNum)
                //{

                //}
                //else if (MyGetState == 1)
                //{

                //}
                //else
                //{

                //}
            };
        }
        ChangeQiangRedItem();
    }

    public void ChangeQiangRedItem() 
    {
        LegionRedBag LR = TextTranslator.instance.GetLegionRedBagByID(this.RedID);
        if (LR.RedBagType == 300001) 
        {
            QiangObj.transform.Find("LeftSprite/topLabel").GetComponent<UILabel>().text = "土豪";
        }
        else if (LR.RedBagType == 310001) 
        {
            QiangObj.transform.Find("LeftSprite/topLabel").GetComponent<UILabel>().text = "充值";
        }
        //QiangObj.transform.Find("LeftSprite/topLabel").GetComponent<UILabel>().text = TopName;
        QiangObj.transform.Find("LeftSprite/Icon").GetComponent<UISprite>().spriteName = ItemId.ToString();
        QiangObj.transform.Find("CharacterName").GetComponent<UILabel>().text = SendRedName;
        QiangObj.transform.Find("RedNumber").GetComponent<UILabel>().text = GetRedNum + "/" + CanGetRedNum;

        if (MyGetState == 1)
        {
            QiangObj.transform.Find("StateSprite").gameObject.SetActive(false);
            QiangObj.transform.Find("StateSprite2").gameObject.SetActive(true);
        }
        else 
        {
            QiangObj.transform.Find("StateSprite").gameObject.SetActive(true);
            QiangObj.transform.Find("StateSprite2").gameObject.SetActive(false);
            UIEventListener.Get(QiangObj.transform.Find("StateSprite").gameObject).onClick = delegate(GameObject gameobj)
            {
                NetworkHandler.instance.SendProcess("8713#" + QiangObj.name + ";");
            };
        }
    }
}