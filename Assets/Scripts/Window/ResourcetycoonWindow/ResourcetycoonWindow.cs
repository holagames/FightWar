using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourcetycoonWindow : MonoBehaviour {

    public GameObject HunterMessage;
    public GameObject RoleButton;
    public GameObject RoleCloseButton;

    public GameObject LeftButton1;
    public GameObject LeftButton2;
    public GameObject LeftButton3;
    public GameObject LeftButton4;

    public GameObject PartInfo1;
    public GameObject PartInfo2;
    public GameObject PartInfo3;
    public GameObject PartInfo4;

    public GameObject ChoiceTab1;
    public GameObject ChoiceTab2;
    public GameObject ChoiceTab3;
    public GameObject ChoiceTab4;

    public GameObject UiGrid1;
    public GameObject UiGrid2;
    public GameObject UiGrid3;
    public GameObject UiGrid4;

    public GameObject CloseButton;
    public GameObject ResourcetycoonItem;
    public GameObject RankListItem;

    public UILabel MyRankNumber;
    public UILabel PointLabel;

    public UILabel MyRankNumber2;
    public UILabel PointLabel2;

    public UISprite dayTime;
    public UILabel labelTime;
    public UILabel MessageLabel;
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;

    private List<string> tabNameList = new List<string>();
    private int Time = 0;
    private int LeftType=1;
    private int RightType = 1;
    //public struct PointStr
    //{
    //    public int ResourceTycoonID=0;
    //    public int GetState=0;
    //    public int GetNum=0;
    //    public PointStr(int _ResourceTycoonID, int _GetState, int _GetNum) 
    //    {
    //        this.ResourceTycoonID = _ResourceTycoonID;
    //        this.GetState = _GetState;
    //        this.GetNum = _GetNum;
    //    }
    //}

    //private List<PointStr> PointStrList=new List<PointStr>();

    void Awake() 
    {
        NetworkHandler.instance.SendProcess("9731#;");
    }
	void Start () {

        UIEventListener.Get(RoleButton).onClick = delegate(GameObject go)
        {
            HunterMessage.SetActive(true);
        };

        UIEventListener.Get(RoleCloseButton).onClick = delegate(GameObject go)
        {
            HunterMessage.SetActive(false);
        };

        UIEventListener.Get(CloseButton).onClick = delegate(GameObject go)
        {
            DestroyImmediate(this.gameObject);
        };

        UIEventListener.Get(LeftButton1).onClick = delegate(GameObject go)
        {
            SetLeftPart(1);
            SetRightPart(1);
            ChangeLeftButton(1);
            SetOneChoiceTabInfo(1);
        };

        UIEventListener.Get(LeftButton2).onClick = delegate(GameObject go)
        {
            SetLeftPart(2);
            ChangeLeftButton(2);
            NetworkHandler.instance.SendProcess("9733#;");
        };

        UIEventListener.Get(LeftButton3).onClick = delegate(GameObject go)
        {
            SetLeftPart(3);
            ChangeLeftButton(3);
            SetAllPointButtonInfo();
        };

        UIEventListener.Get(LeftButton4).onClick = delegate(GameObject go)
        {
            SetLeftPart(4);
            ChangeLeftButton(4);
            NetworkHandler.instance.SendProcess("9734#;");
        };

        UIEventListener.Get(ChoiceTab1).onClick = delegate(GameObject go)
        {
            SetRightPart(1);
            SetOneChoiceTabInfo(1);
        };

        UIEventListener.Get(ChoiceTab2).onClick = delegate(GameObject go)
        {
            SetRightPart(2);
            SetOneChoiceTabInfo(2);
        };

        UIEventListener.Get(ChoiceTab3).onClick = delegate(GameObject go)
        {
            SetRightPart(3);
            SetOneChoiceTabInfo(3);
        };

        UIEventListener.Get(ChoiceTab4).onClick = delegate(GameObject go)
        {
            SetRightPart(4);
            SetOneChoiceTabInfo(4);
        };

        SetFourButtonName();
	}


    /// <summary>
    /// 左边按钮切换状态
    /// </summary>
    /// <param name="id"></param>
    private void ChangeLeftButton(int id) 
    {
        LeftType = id;
        switch (id) 
        {
            case 1:
                PartInfo1.SetActive(true);
                PartInfo2.SetActive(false);
                PartInfo3.SetActive(false);
                PartInfo4.SetActive(false);
                break;
            case 2:
                PartInfo1.SetActive(false);
                PartInfo2.SetActive(true);
                PartInfo3.SetActive(false);
                PartInfo4.SetActive(false);
                break;
            case 3:
                PartInfo1.SetActive(false);
                PartInfo2.SetActive(false);
                PartInfo3.SetActive(true);
                PartInfo4.SetActive(false);
                break;
            case 4:
                PartInfo1.SetActive(false);
                PartInfo2.SetActive(false);
                PartInfo3.SetActive(false);
                PartInfo4.SetActive(true);
                break;
        }
    }

    /// <summary>
    /// 设置左边按钮toggle
    /// </summary>
    /// <param name="id"></param>
    public void SetLeftPart(int id)
    {
        if (id != 1)
        {
            LeftButton1.GetComponent<UIToggle>().value = false;
            LeftButton1.GetComponent<UIToggle>().startsActive = false;
            if (id == 2)
            {
                LeftButton2.GetComponent<UIToggle>().value = true;
                LeftButton2.GetComponent<UIToggle>().enabled = true;
                LeftButton2.transform.Find("ChangeButton").gameObject.SetActive(true);
            }
            else if (id == 3)
            {
                LeftButton3.GetComponent<UIToggle>().value = true;
                LeftButton3.GetComponent<UIToggle>().enabled = true;
                LeftButton3.transform.Find("ChangeButton").gameObject.SetActive(true);
            }
            else if (id == 4)
            {
                LeftButton4.GetComponent<UIToggle>().value = true;
                LeftButton4.GetComponent<UIToggle>().enabled = true;
                LeftButton4.transform.Find("ChangeButton").gameObject.SetActive(true);
            }
        }
        else
        {
            LeftButton1.GetComponent<UIToggle>().value = true;
            LeftButton1.GetComponent<UIToggle>().startsActive = true;
        }
    }

    /// <summary>
    /// 设置右边按钮toggle
    /// </summary>
    /// <param name="id"></param>
    public void SetRightPart(int id)
    {
        if (id != 1)
        {
            ChoiceTab1.GetComponent<UIToggle>().value = false;
            ChoiceTab1.GetComponent<UIToggle>().startsActive = false;
            if (id == 2)
            {
                ChoiceTab2.GetComponent<UIToggle>().value = true;
                ChoiceTab2.GetComponent<UIToggle>().enabled = true;
                ChoiceTab2.transform.Find("Sprite").gameObject.SetActive(true);
            }
            else if (id == 3)
            {
                ChoiceTab3.GetComponent<UIToggle>().value = true;
                ChoiceTab3.GetComponent<UIToggle>().enabled = true;
                ChoiceTab3.transform.Find("Sprite").gameObject.SetActive(true);
            }
            else if (id == 4)
            {
                ChoiceTab4.GetComponent<UIToggle>().value = true;
                ChoiceTab4.GetComponent<UIToggle>().enabled = true;
                ChoiceTab4.transform.Find("Sprite").gameObject.SetActive(true);
            }
        }
        else
        {
            ChoiceTab1.GetComponent<UIToggle>().value = true;
            ChoiceTab1.GetComponent<UIToggle>().startsActive = true;
            ChoiceTab2.transform.Find("Sprite").gameObject.SetActive(false);
            ChoiceTab3.transform.Find("Sprite").gameObject.SetActive(false);
            ChoiceTab4.transform.Find("Sprite").gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 初始化资源收集四中按钮名称
    /// </summary>
    private void SetFourButtonName() 
    {
        List<int> CumulativeItemID = new List<int>();
        for (int i = 0; i < TextTranslator.instance.ResourceTycoonList.size; i++) 
        {
            if (TextTranslator.instance.ResourceTycoonList[i].Type == 1) 
            {
                if (!CumulativeItemID.Contains(TextTranslator.instance.ResourceTycoonList[i].CumulativeItemID)) 
                {
                    int itemid=TextTranslator.instance.ResourceTycoonList[i].CumulativeItemID;
                    CumulativeItemID.Add(itemid);
                    tabNameList.Add(TextTranslator.instance.GetItemNameByItemCode(itemid));
                }
            }
        }

        ChoiceTab1.transform.Find("Label").GetComponent<UILabel>().text = tabNameList[0];
        ChoiceTab1.transform.Find("Sprite/Label").GetComponent<UILabel>().text = tabNameList[0];

        ChoiceTab2.transform.Find("Label").GetComponent<UILabel>().text = tabNameList[1];
        ChoiceTab2.transform.Find("Sprite/Label").GetComponent<UILabel>().text = tabNameList[1];

        ChoiceTab3.transform.Find("Label").GetComponent<UILabel>().text = tabNameList[2];
        ChoiceTab3.transform.Find("Sprite/Label").GetComponent<UILabel>().text = tabNameList[2];

        //ChoiceTab4.transform.Find("Label").GetComponent<UILabel>().text = tabNameList[3];
        //ChoiceTab4.transform.Find("Sprite/Label").GetComponent<UILabel>().text = tabNameList[3];

        if (tabNameList[3]=="金色经验奖章") 
        {
            tabNameList[3] = "金色奖章";
        }
        ChoiceTab4.transform.Find("Label").GetComponent<UILabel>().text = tabNameList[3];
        ChoiceTab4.transform.Find("Sprite/Label").GetComponent<UILabel>().text = tabNameList[3];
    }


    /// <summary>
    /// 9371初始化所有奖励领取数量，领取状态
    /// </summary>
    /// <param name="DataSplit"></param>
    public void GetMethodBy_9731(string[] DataSplit) 
    {
        string[] dataSplit = DataSplit;
        for (int i = 2; i < dataSplit.Length - 1;i++ ) 
        {
            string[] trcSplit=dataSplit[i].Split('$');
            //PointStr ps = new PointStr(int.Parse(trcSplit[0]), int.Parse(trcSplit[1]), int.Parse(trcSplit[2]));
            //PointStrList.Add(ps); 
            TextTranslator.instance.SetResourceTycoonStateByID(int.Parse(trcSplit[0]), int.Parse(trcSplit[1]), int.Parse(trcSplit[2]));
        }

        SetLeftPart(1);//初始话指定按钮位置
        SetRightPart(1);
        ChangeLeftButton(1);
        SetOneChoiceTabInfo(1);

        Time = int.Parse(dataSplit[1]);
        int daytime = Time / 86400;
        if (daytime == 0) { dayTime.spriteName = "Serviceword0"; }
        else if (daytime == 1) { dayTime.spriteName = "Serviceword1"; }
        else if (daytime == 2) { dayTime.spriteName = "Serviceword2"; }
        else if (daytime == 3) { dayTime.spriteName = "Serviceword3"; }
        else if (daytime == 4) { dayTime.spriteName = "Serviceword4"; }
        else if (daytime == 5) { dayTime.spriteName = "Serviceword5"; }
        else if (daytime == 6) { dayTime.spriteName = "Serviceword6"; }
        else if (daytime == 7) { dayTime.spriteName = "Serviceword7"; }
        CancelInvoke();
        InvokeRepeating("UpdateTime", 0, 1.0f);
    }

    /// <summary>
    /// 资源收集四种模块切换
    /// </summary>
    /// <param name="_ActivitySheet"></param>
    private void SetOneChoiceTabInfo(int _ActivitySheet) 
    {
        RightType = _ActivitySheet;
        for(int i=UiGrid1.transform.childCount-1;i>=0;i--)
        {
            DestroyImmediate(UiGrid1.transform.GetChild(i).gameObject);
        }
        foreach (var item in TextTranslator.instance.ResourceTycoonList)
        {
            if (item.Type == 1)
            {
                if (item.ActivitySheet == _ActivitySheet && item.GetState == 1) //可领取
                {
                    GameObject go = NGUITools.AddChild(UiGrid1, ResourcetycoonItem);
                    go.SetActive(true);
                    go.transform.Find("LabelCompleteNum").gameObject.SetActive(true);                   
                    go.transform.Find("AwardButton").gameObject.SetActive(true);

                    go.name = item.ResourceTycoonID.ToString();
                    int num = item.ResourceTycoonID;
                    go.transform.Find("Labeldescribe").GetComponent<UILabel>().text = "[b2f5ff]获得" + TextTranslator.instance.GetItemNameByItemCode(item.CumulativeItemID) + "[-][f98342]" + item.CumulativeItemNum + "[-][b2f5ff]个";
                    go.transform.Find("LabelCompleteNum").GetComponent<UILabel>().text = "[27f081]" + item.GetNum + "[-]/" + item.CumulativeItemNum.ToString();
                    //SetItemDetail(item.CumulativeItemID, item.CumulativeItemNum, go.transform.Find("Award1").gameObject);
                    UIEventListener.Get(go.transform.Find("AwardButton").gameObject).onClick = delegate(GameObject ob)
                    {
                        NetworkHandler.instance.SendProcess("9732#" + num + ";");
                    };

                    go.transform.Find("Award1").gameObject.SetActive(true);
                    SetItemDetail(10912, item.CumulativePoints, go.transform.Find("Award1").gameObject);

                    if (item.ItemID1 > 0) 
                    {
                        go.transform.Find("Award2").gameObject.SetActive(true);
                        SetItemDetail(item.ItemID1, item.ItemNum1, go.transform.Find("Award2").gameObject);
                    }
                    if (item.ItemID2 > 0)
                    {
                        go.transform.Find("Award3").gameObject.SetActive(true);
                        SetItemDetail(item.ItemID2, item.ItemNum2, go.transform.Find("Award3").gameObject);
                    }
                    if (item.ItemID3 > 0)
                    {
                        go.transform.Find("Award4").gameObject.SetActive(true);
                        SetItemDetail(item.ItemID3, item.ItemNum3, go.transform.Find("Award4").gameObject);
                    }
                    
                }
            }
        }
        foreach (var item in TextTranslator.instance.ResourceTycoonList)
        {
            if (item.Type == 1)
            {
                if (item.ActivitySheet == _ActivitySheet && item.GetState == 0) //不可领取
                {
                    GameObject go = NGUITools.AddChild(UiGrid1, ResourcetycoonItem);
                    go.SetActive(true);
                    go.transform.Find("LabelCompleteNum").gameObject.SetActive(true);
                    //go.transform.Find("Award1").gameObject.SetActive(true);
                    go.transform.Find("GoToButton").gameObject.SetActive(true);

                    go.name = item.ResourceTycoonID.ToString();
                    go.transform.Find("Labeldescribe").GetComponent<UILabel>().text = "[b2f5ff]获得" + TextTranslator.instance.GetItemNameByItemCode(item.CumulativeItemID) + "[-][f98342]" + item.CumulativeItemNum + "[-][b2f5ff]个";
                    go.transform.Find("LabelCompleteNum").GetComponent<UILabel>().text = "[27f081]" + item.GetNum + "[-]/" + item.CumulativeItemNum.ToString();
                    //SetItemDetail(item.CumulativeItemID, item.CumulativeItemNum, go.transform.Find("Award1").gameObject);
                    UIEventListener.Get(go.transform.Find("GoToButton").gameObject).onClick = delegate(GameObject ob)
                    {
                        Debug.Log("前往");
                        if (_ActivitySheet != 4)
                        {
                            UIManager.instance.OpenPanel("EverydayWindow", true);
                        }
                        else 
                        {
                            UIManager.instance.OpenPanel("GrabItemWindow", true);
                        }
                    };
                    go.transform.Find("Award1").gameObject.SetActive(true);
                    SetItemDetail(10912, item.CumulativePoints, go.transform.Find("Award1").gameObject);

                    if (item.ItemID1 > 0)
                    {
                        go.transform.Find("Award2").gameObject.SetActive(true);
                        SetItemDetail(item.ItemID1, item.ItemNum1, go.transform.Find("Award2").gameObject);
                    }
                    if (item.ItemID2 > 0)
                    {
                        go.transform.Find("Award3").gameObject.SetActive(true);
                        SetItemDetail(item.ItemID2, item.ItemNum2, go.transform.Find("Award3").gameObject);
                    }
                    if (item.ItemID3 > 0)
                    {
                        go.transform.Find("Award4").gameObject.SetActive(true);
                        SetItemDetail(item.ItemID3, item.ItemNum3, go.transform.Find("Award4").gameObject);
                    }
                }
            }
        }
        foreach (var item in TextTranslator.instance.ResourceTycoonList)
        {
            if (item.Type == 1)
            {
                if (item.ActivitySheet == _ActivitySheet && item.GetState == 2) //可领取
                {
                    GameObject go = NGUITools.AddChild(UiGrid1, ResourcetycoonItem);
                    go.SetActive(true);
                    go.transform.Find("LabelCompleteNum").gameObject.SetActive(true);
                    //go.transform.Find("Award1").gameObject.SetActive(true);
                    go.transform.Find("HaveButton").gameObject.SetActive(true);

                    go.name = item.ResourceTycoonID.ToString();
                    go.transform.Find("Labeldescribe").GetComponent<UILabel>().text = "[b2f5ff]获得" + TextTranslator.instance.GetItemNameByItemCode(item.CumulativeItemID) + "[-][f98342]" + item.CumulativeItemNum + "[-][b2f5ff]个";
                    go.transform.Find("LabelCompleteNum").GetComponent<UILabel>().text = "[27f081]" + item.GetNum + "[-]/" + item.CumulativeItemNum.ToString();
                    //SetItemDetail(item.CumulativeItemID, item.CumulativeItemNum, go.transform.Find("Award1").gameObject);
                    UIEventListener.Get(go.transform.Find("HaveButton").gameObject).onClick = delegate(GameObject ob)
                    {
                        Debug.Log("已领");
                    };

                    go.transform.Find("Award1").gameObject.SetActive(true);
                    SetItemDetail(10912, item.CumulativePoints, go.transform.Find("Award1").gameObject);

                    if (item.ItemID1 > 0)
                    {
                        go.transform.Find("Award2").gameObject.SetActive(true);
                        SetItemDetail(item.ItemID1, item.ItemNum1, go.transform.Find("Award2").gameObject);
                    }
                    if (item.ItemID2 > 0)
                    {
                        go.transform.Find("Award3").gameObject.SetActive(true);
                        SetItemDetail(item.ItemID2, item.ItemNum2, go.transform.Find("Award3").gameObject);
                    }
                    if (item.ItemID3 > 0)
                    {
                        go.transform.Find("Award4").gameObject.SetActive(true);
                        SetItemDetail(item.ItemID3, item.ItemNum3, go.transform.Find("Award4").gameObject);
                    }
                }
            }
        }
        //GetOneActivitySheetPoint(_ActivitySheet);
        UiGrid1.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        UiGrid1.GetComponent<UIGrid>().Reposition();
    }

    /// <summary>
    /// 领取奖励后刷新id状态，不重复呼叫9731，重进刷新
    /// </summary>
    /// <param name="DataSplit"></param>
    public void GetMethodBy_9732(int _ResourceTycoonID)
    {
        ResourceTycoon RT =TextTranslator.instance.GetResourceTycoonByID(_ResourceTycoonID);
        if (RT != null)
        {
            RT.AddAwardState(2, RT.CumulativeItemNum);
        }

        if (LeftType == 1) //资源收集界面刷新
        {
            SetOneChoiceTabInfo(RightType);
        }
        else if (LeftType == 3) //积分界面刷新
        {
            SetAllPointButtonInfo();
        }
    }


    /// <summary>
    /// 积分奖励领取
    /// </summary>
    private void SetAllPointButtonInfo()
    {
        for (int i = UiGrid3.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(UiGrid3.transform.GetChild(i).gameObject);
        }
        foreach (var item in TextTranslator.instance.ResourceTycoonList)
        {
            if (item.Type == 3)
            {
                if (item.GetState == 1) //可领取
                {
                    GameObject go = NGUITools.AddChild(UiGrid3, ResourcetycoonItem);
                    go.SetActive(true);
                    go.transform.Find("LabelCompleteNum").gameObject.SetActive(false);
                    go.transform.Find("AwardButton").gameObject.SetActive(true);

                    go.name = item.ResourceTycoonID.ToString();
                    int num = item.ResourceTycoonID;
                    go.transform.Find("Labeldescribe").GetComponent<UILabel>().text = "[b2f5ff]累计积分[-][27f081]" + item.CumulativeItemNum + "[-]";

                    if (item.ItemID1 > 0)
                    {
                        GameObject award = go.transform.Find("Award1").gameObject;
                        award.SetActive(true);
                        SetItemDetail(item.ItemID1, item.ItemNum1, award);
                    }
                    if (item.ItemID2 > 0)
                    {
                        GameObject award = go.transform.Find("Award2").gameObject;
                        award.SetActive(true);
                        SetItemDetail(item.ItemID2, item.ItemNum2, award);
                    }
                    if (item.ItemID3 > 0)
                    {
                        GameObject award = go.transform.Find("Award3").gameObject;
                        award.SetActive(true);
                        SetItemDetail(item.ItemID3, item.ItemNum3, award);
                    }
                    if (item.ItemID4 > 0)
                    {
                        GameObject award = go.transform.Find("Award4").gameObject;
                        award.SetActive(true);
                        SetItemDetail(item.ItemID4, item.ItemNum4, award);
                    }

                    UIEventListener.Get(go.transform.Find("AwardButton").gameObject).onClick = delegate(GameObject ob)
                    {
                        NetworkHandler.instance.SendProcess("9732#" + num + ";");
                    };
                }
            }
        }

        foreach (var item in TextTranslator.instance.ResourceTycoonList)
        {
            if (item.Type == 3)
            {
                if (item.GetState == 0) //不可领取
                {
                    GameObject go = NGUITools.AddChild(UiGrid3, ResourcetycoonItem);
                    go.SetActive(true);
                    go.transform.Find("LabelCompleteNum").gameObject.SetActive(false);
                    go.transform.Find("HaveButton").gameObject.SetActive(true);

                    go.name = item.ResourceTycoonID.ToString();
                    go.transform.Find("Labeldescribe").GetComponent<UILabel>().text = "[b2f5ff]累计积分[-][27f081]" + item.CumulativeItemNum + "[-]";

                    if (item.ItemID1 > 0)
                    {
                        GameObject award = go.transform.Find("Award1").gameObject;
                        award.SetActive(true);
                        SetItemDetail(item.ItemID1, item.ItemNum1, award);
                    }
                    if (item.ItemID2 > 0)
                    {
                        GameObject award = go.transform.Find("Award2").gameObject;
                        award.SetActive(true);
                        SetItemDetail(item.ItemID2, item.ItemNum2, award);
                    }
                    if (item.ItemID3 > 0)
                    {
                        GameObject award = go.transform.Find("Award3").gameObject;
                        award.SetActive(true);
                        SetItemDetail(item.ItemID3, item.ItemNum3, award);
                    }
                    if (item.ItemID4 > 0)
                    {
                        GameObject award = go.transform.Find("Award4").gameObject;
                        award.SetActive(true);
                        SetItemDetail(item.ItemID4, item.ItemNum4, award);
                    }

                    go.transform.Find("HaveButton/LabelYellow").GetComponent<UILabel>().text = "不可领";
                    UIEventListener.Get(go.transform.Find("HaveButton").gameObject).onClick = delegate(GameObject ob)
                    {
                        Debug.Log("未达到条件");
                    };
                }
            }
        }

        foreach (var item in TextTranslator.instance.ResourceTycoonList)
        {
            if (item.Type == 3)
            {
                if (item.GetState == 2) //不可领取
                {
                    GameObject go = NGUITools.AddChild(UiGrid3, ResourcetycoonItem);
                    go.SetActive(true);
                    go.transform.Find("LabelCompleteNum").gameObject.SetActive(false);
                    go.transform.Find("HaveButton").gameObject.SetActive(true);

                    go.name = item.ResourceTycoonID.ToString();
                    go.transform.Find("Labeldescribe").GetComponent<UILabel>().text = "[b2f5ff]累计积分[-][27f081]" + item.CumulativeItemNum + "[-]";

                    if (item.ItemID1 > 0)
                    {
                        GameObject award = go.transform.Find("Award1").gameObject;
                        award.SetActive(true);
                        SetItemDetail(item.ItemID1, item.ItemNum1, award);
                    }
                    if (item.ItemID2 > 0)
                    {
                        GameObject award = go.transform.Find("Award2").gameObject;
                        award.SetActive(true);
                        SetItemDetail(item.ItemID2, item.ItemNum2, award);
                    }
                    if (item.ItemID3 > 0)
                    {
                        GameObject award = go.transform.Find("Award3").gameObject;
                        award.SetActive(true);
                        SetItemDetail(item.ItemID3, item.ItemNum3, award);
                    }
                    if (item.ItemID4 > 0)
                    {
                        GameObject award = go.transform.Find("Award4").gameObject;
                        award.SetActive(true);
                        SetItemDetail(item.ItemID4, item.ItemNum4, award);
                    }

                    UIEventListener.Get(go.transform.Find("HaveButton").gameObject).onClick = delegate(GameObject ob)
                    {
                        Debug.Log("已经领过");
                    };
                }
            }
        }
        UiGrid3.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        UiGrid3.GetComponent<UIGrid>().Reposition();
    }


    /// <summary>
    /// 取得累计充值排行9733
    /// </summary>
    /// <param name="DataSplit"></param>
    public void GetMethodBy_9733(string _PointNumber,string String)
    {
        for (int i = UiGrid2.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(UiGrid2.transform.GetChild(i).gameObject);
        }

        string[] dataSplit = String.Split('!');
        bool isShangbang = false;
        int num=0;
        for (int i = 0; i < dataSplit.Length - 1; i++)
        {
            string[] trcSplit = dataSplit[i].Split('$');
            GameObject go = NGUITools.AddChild(UiGrid2, RankListItem);
            go.SetActive(true);
            switch (i + 1)
            {
                case 1:
                    go.transform.Find("LabelRank").gameObject.SetActive(false);
                    go.transform.Find("RankSprite").gameObject.SetActive(true);
                    go.transform.Find("RankSprite/LabelRankSprite").GetComponent<UISprite>().spriteName = "word1";
                    break;
                case 2:
                    go.transform.Find("LabelRank").gameObject.SetActive(false);
                    go.transform.Find("RankSprite").gameObject.SetActive(true);
                    go.transform.Find("RankSprite/LabelRankSprite").GetComponent<UISprite>().spriteName = "word2";
                    break;
                case 3:
                    go.transform.Find("LabelRank").gameObject.SetActive(false);
                    go.transform.Find("RankSprite").gameObject.SetActive(true);
                    go.transform.Find("RankSprite/LabelRankSprite").GetComponent<UISprite>().spriteName = "word3";
                    break;
                default:
                    go.transform.Find("LabelRank").gameObject.SetActive(true);
                    go.transform.Find("RankSprite").gameObject.SetActive(false);
                    go.transform.Find("LabelRank").GetComponent<UILabel>().text = (i + 1).ToString();
                    break;
            }
            go.transform.Find("LabelName").GetComponent<UILabel>().text = trcSplit[2];
            go.transform.Find("LabelNumber1").GetComponent<UILabel>().text = (int.Parse(trcSplit[3]) / 10).ToString();
            go.transform.Find("LabelNumber2").GetComponent<UILabel>().text = trcSplit[3];
            go.transform.Find("LabelNumber1").gameObject.SetActive(true);
            go.transform.Find("LabelNumber2").gameObject.SetActive(true);
            if (int.Parse(trcSplit[1]) == CharacterRecorder.instance.userId) //自己上榜
            {
                isShangbang = true;
                num=i+1;
            }
        }
        if (isShangbang)
        {
            MyRankNumber.text = "第" + num.ToString() + "名";
        }
        else 
        {
            MyRankNumber.text = "未上榜";
        }
        PointLabel.text = _PointNumber;
        UiGrid2.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        UiGrid2.GetComponent<UIGrid>().Reposition();
    }


    /// <summary>
    /// 取得积分排行9734
    /// </summary>
    /// <param name="DataSplit"></param>
    public void GetMethodBy_9734(string _PointNumber,string String)
    {
        for (int i = UiGrid4.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(UiGrid4.transform.GetChild(i).gameObject);
        }

        string[] dataSplit = String.Split('!');
        bool isShangbang = false;
        int num = 0;
        for (int i = 0; i < dataSplit.Length - 1; i++)
        {
            string[] trcSplit = dataSplit[i].Split('$');
            GameObject go = NGUITools.AddChild(UiGrid4, RankListItem);
            go.SetActive(true);
            switch (i+1) 
            {
                case 1:
                    go.transform.Find("LabelRank").gameObject.SetActive(false);
                    go.transform.Find("RankSprite").gameObject.SetActive(true);
                    go.transform.Find("RankSprite/LabelRankSprite").GetComponent<UISprite>().spriteName = "word1";
                    break;
                case 2:
                    go.transform.Find("LabelRank").gameObject.SetActive(false);
                    go.transform.Find("RankSprite").gameObject.SetActive(true);
                    go.transform.Find("RankSprite/LabelRankSprite").GetComponent<UISprite>().spriteName = "word2";
                    break;
                case 3:
                    go.transform.Find("LabelRank").gameObject.SetActive(false);
                    go.transform.Find("RankSprite").gameObject.SetActive(true);
                    go.transform.Find("RankSprite/LabelRankSprite").GetComponent<UISprite>().spriteName = "word3";
                    break;
                default:
                    go.transform.Find("LabelRank").gameObject.SetActive(true);
                    go.transform.Find("RankSprite").gameObject.SetActive(false);
                    go.transform.Find("LabelRank").GetComponent<UILabel>().text = (i + 1).ToString();
                    break;
            }
            go.transform.Find("LabelName").GetComponent<UILabel>().text = trcSplit[2];
            go.transform.Find("LabelNumber1").GetComponent<UILabel>().text = trcSplit[3];
            go.transform.Find("LabelNumber1").gameObject.SetActive(true);
            go.transform.Find("LabelNumber2").gameObject.SetActive(false);
            foreach (var item in TextTranslator.instance.ResourceTycoonList)
            {
                if (item.Type == 4 && item.Sort == i + 1) 
                {
                    if (item.ItemID1 > 0) 
                    {
                        GameObject award= go.transform.Find("AwardBg2").gameObject;
                        award.SetActive(true);
                        SetItemDetail(item.ItemID1, item.ItemNum1, award);
                    }
                    if (item.ItemID2 > 0)
                    {
                        GameObject award = go.transform.Find("AwardBg3").gameObject;
                        award.SetActive(true);
                        SetItemDetail(item.ItemID2, item.ItemNum2, award);
                    }
                    //if (item.ItemID3 > 0)
                    //{
                    //    GameObject award = go.transform.Find("AwardBg3").gameObject;
                    //    award.SetActive(true);
                    //    SetItemDetail(item.ItemID3, item.ItemNum3, award);
                    //}
                }
            }
            if (int.Parse(trcSplit[1]) == CharacterRecorder.instance.userId) //自己上榜
            {
                isShangbang = true;
                num = i + 1;
            }
        }
        if (isShangbang)
        {
            MyRankNumber2.text = "第" + num.ToString() + "名";
        }
        else
        {
            MyRankNumber2.text = "未上榜";
        }
        PointLabel2.text = _PointNumber;
        UiGrid4.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        UiGrid4.GetComponent<UIGrid>().Reposition();
    }


    /// <summary>
    /// Icon对应信息
    /// </summary>
    /// <param name="_itemId"></param>
    /// <param name="_itemCount"></param>
    /// <param name="ItemObj"></param>
    private void SetItemDetail(int _itemId, int _itemCount, GameObject ItemObj)
    {
        UISprite spriteFrame = ItemObj.GetComponent<UISprite>();
        UISprite spriteIcon = ItemObj.transform.Find("Icon").GetComponent<UISprite>();
        GameObject suiPian = ItemObj.transform.Find("suiPian").gameObject;
        ItemObj.transform.Find("Number").GetComponent<UILabel>().text = _itemCount.ToString();
        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(_itemId);
        spriteFrame.spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
        TextTranslator.instance.ItemDescription(ItemObj, _itemId, _itemCount);

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
    }


    /// <summary>
    /// 资源收集单个物品的累计积分
    /// </summary>
    /// <param name="_ActivitySheet"></param>
    public void GetOneActivitySheetPoint(int _ActivitySheet) 
    {
        int onePoint = 0;//单个物品积分价格
        int oneAllPoint = 0;
        if (_ActivitySheet == 1) 
        {
            ResourceTycoon rt=TextTranslator.instance.GetResourceTycoonByID(1);
            onePoint = rt.CumulativePoints / rt.CumulativeItemNum;
        }
        else if (_ActivitySheet == 2)
        {
            ResourceTycoon rt = TextTranslator.instance.GetResourceTycoonByID(11);
            onePoint = rt.CumulativePoints / rt.CumulativeItemNum;
        }
        else if (_ActivitySheet == 3)
        {
            ResourceTycoon rt = TextTranslator.instance.GetResourceTycoonByID(21);
            onePoint = rt.CumulativePoints / rt.CumulativeItemNum;
        }
        else if (_ActivitySheet == 4)
        {
            ResourceTycoon rt = TextTranslator.instance.GetResourceTycoonByID(31);
            onePoint = rt.CumulativePoints / rt.CumulativeItemNum;
        }

        foreach (var item in TextTranslator.instance.ResourceTycoonList)
        {
            if (item.Type == 1)
            {
                if (item.ActivitySheet == _ActivitySheet)
                {
                    if (item.GetState == 2) //已经领取
                    {
                        oneAllPoint += item.CumulativePoints;
                    }
                    else 
                    {
                        oneAllPoint += item.GetNum * onePoint;
                    }
                }
            }
        }

        MessageLabel.text = "该种类材料共获得" + oneAllPoint + "积分";
    }


    /// <summary>
    /// 倒计时
    /// </summary>
    void UpdateTime()
    {
        if (Time > 0)
        {
            if (Time > 86400) { }
            string houreStr = (Time % 86400 / 3600).ToString("00");
            string miniteStr = (Time % 3600 / 60).ToString("00");
            string secondStr = (Time % 60).ToString("00");
            labelTime.text = string.Format("{0}:{1}:{2}", houreStr, miniteStr, secondStr);
            Time -= 1;
        }
        else if (Time == 0) 
        {
            PlayerPrefs.SetInt("IsOpenResourcetycoon", 0);
            GameObject Mw = GameObject.Find("MainWindow");
            if (Mw != null)
            {
                Mw.GetComponent<MainWindow>().OpenResourcetycoon();
            }
            CancelInvoke();
        }
    }
}
