using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class LegionCopyWindowNew : MonoBehaviour
{
    private int curGateGroupID = 0;//当前打到的大关卡
    public static int curSelectGateGroupID;//当前选择的大关卡
    public GameObject doubltButton;
    public GameObject DoubltObjCloseButton;
    public GameObject DoubltObj;

    public GameObject hertRankButton;
    public GameObject legionRankButton;
    public GameObject addButton;
    public GameObject leftButton;
    public GameObject rightButton;
    public UILabel leftFightTimesLabel;
    private int buyTimes = 0;

    private List<GameObject> ConfirmLegionItemList = new List<GameObject>();
    public List<GameObject> GroupGameObject = new List<GameObject>();

    public GameObject itemContainer;
    public GameObject itemGridBg1;
    public GameObject itemGridBg2;


    public GameObject enterButton;
    public GameObject PreviewButton;
    public UISlider mSlider;
    public UILabel mSliderLabel;
    public UILabel mTimeLabel;
    public UILabel mNameLabel;
    private int leftTime;
    private int FightStyle = 12;//军团副本
    private BetterList<int> percentList = new BetterList<int>();
    private bool canGacha = false;

    /// <summary>
    /// 当前显示的页数
    /// </summary>
    private int mCurrentShowPage;

    private bool mHasInit = false;

    private List<GameObject> itemList = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.军团副本);
        UIManager.instance.UpdateSystems(UIManager.Systems.军团副本);

        NetworkHandler.instance.SendProcess("8201#;");
        NetworkHandler.instance.SendProcess(string.Format("8309#{0};", LegionCopyWindowNew.curSelectGateGroupID));
       
        for (var i = 0; i < GroupGameObject.Count; i++)
        {
            UIEventListener.Get(GroupGameObject[i]).onClick += delegate(GameObject go)
            {
                if (mHasInit)
                {
                    if (curGateGroupID >= int.Parse(go.name) && curSelectGateGroupID != int.Parse(go.name))
                    {
                        curSelectGateGroupID = int.Parse(go.name);
                        NetworkHandler.instance.SendProcess(string.Format("8202#{0};", curSelectGateGroupID));
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("长官，请先通过前面关卡", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else
                {
                    //UIManager.instance.OpenPromptWindow("长官，请先通过当前关卡", PromptWindow.PromptType.Alert, null, null);
                }
            };
        }
        if (UIEventListener.Get(hertRankButton).onClick == null)
        {
            UIEventListener.Get(hertRankButton).onClick += delegate(GameObject go)
            {
                //UIManager.instance.OpenPanel("LegionSecondWindow", true);
                //LegionSecondWindow.enterFromLegionCopy = true;
                //UIManager.instance.OpenPromptWindow("即将开放", PromptWindow.PromptType.Alert, null, null);
                CharacterRecorder.instance.LegionHertRankTab = 1;
                NetworkHandler.instance.SendProcess(string.Format("8309#{0};", LegionCopyWindowNew.curSelectGateGroupID));
                UIManager.instance.OpenPanel("LegionHertRankWindow", false);

            };
        }
        if (UIEventListener.Get(enterButton).onClick == null)
        {
            UIEventListener.Get(enterButton).onClick += delegate(GameObject go)
            {
                ClickEnterButton();
            };
        }
        UIEventListener.Get(PreviewButton).onClick = delegate(GameObject go)
        {
            UIManager.instance.OpenPanel("LegionGachaWindow", true);
            GameObject _LegionGachaWindow = GameObject.Find("LegionGachaWindow");
            if (_LegionGachaWindow != null)
            {
                _LegionGachaWindow.GetComponent<LegionGachaWindow>().SetLegionGachaPercentList(this.percentList);
            }
        };

        if (UIEventListener.Get(doubltButton).onClick == null)
        {
            UIEventListener.Get(doubltButton).onClick += delegate(GameObject go)
            {
                DoubltObj.SetActive(true);
            };
        }
        if (UIEventListener.Get(DoubltObjCloseButton).onClick == null)
        {
            UIEventListener.Get(DoubltObjCloseButton).onClick += delegate(GameObject go)
            {
                DoubltObj.SetActive(false);
            };
        }
        #region leftBtn
        if (UIEventListener.Get(leftButton).onClick == null)
        {
            UIEventListener.Get(leftButton).onClick += delegate(GameObject go)
            {
                /* int curIndex = int.Parse(myUICenterOnChild.centeredObject.name);
                 Debug.Log("curIndex。。。" + curIndex);
                 if (curIndex > 1 + 1)
                 {
                     curIndex -= 1;
                     myUICenterOnChild.CenterOn(uiGride.transform.GetChild(curIndex - 1));
                 }*/

                if (curSelectGateGroupID > 1)
                {
                    curSelectGateGroupID--;
                    if (curSelectGateGroupID <= mCurrentShowPage * 5)
                    {
                        mCurrentShowPage--;
                        ChangeShowPage(mCurrentShowPage);
                    }
                    else
                    {
                        NetworkHandler.instance.SendProcess(string.Format("8202#{0};", curSelectGateGroupID));
                    }
                }
                /*if (mHasInit && mCurrentShowPage > 1)
                {
                    ChangeShowPage(mCurrentShowPage-1);
                    //NetworkHandler.instance.SendProcess(string.Format("8202#{0};", curSelectGateGroupID));
                }*/
            };
        }
        #endregion
        #region rightBtn
        if (UIEventListener.Get(rightButton).onClick == null)
        {
            UIEventListener.Get(rightButton).onClick += delegate(GameObject go)
            {
                /* Debug.Log("rightButton。。。");
                 int curIndex = int.Parse(myUICenterOnChild.centeredObject.name);
                 if (curIndex < uiGride.transform.childCount - 1)
                 {
                     curIndex += 1;
                     myUICenterOnChild.CenterOn(uiGride.transform.GetChild(curIndex - 1));
                 }*/
                /*if (mHasInit && curSelectGateGroupID/5 < curGateGroupID/5)
                {
                    ChangeShowPage(mCurrentShowPage + 1);
                    //NetworkHandler.instance.SendProcess(string.Format("8202#{0};", curSelectGateGroupID));
                }*/
                if (curSelectGateGroupID < curGateGroupID)
                {
                    curSelectGateGroupID++;
                    if (curSelectGateGroupID > mCurrentShowPage * 5 + 5)
                    {
                        mCurrentShowPage++;
                        ChangeShowPage(mCurrentShowPage);
                    }
                    NetworkHandler.instance.SendProcess(string.Format("8202#{0};", curSelectGateGroupID));
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("长官，请先通过当前关卡", PromptWindow.PromptType.Alert, null, null);
                }
            };
        }
        #endregion
        if (UIEventListener.Get(legionRankButton).onClick == null)
        {
            UIEventListener.Get(legionRankButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPanel("LegionSecondWindow", true);
                LegionSecondWindow.enterFromLegionCopy = true;
            };
        }
        if (UIEventListener.Get(addButton).onClick == null)
        {
            UIEventListener.Get(addButton).onClick += delegate(GameObject go)
            {
                Market _myMarket = TextTranslator.instance.GetMarketByBuyCount(buyTimes + 1);
                int costDiomond = _myMarket == null ? 50 : _myMarket.LegionGateBuyPrice;
                UIManager.instance.OpenPromptWindow(string.Format("是否花费{0}钻石购买一次\n军团副本挑战次数？", costDiomond), PromptWindow.PromptType.Confirm, ConfirmBuy, null);
                //UIManager.instance.OpenPromptWindow("即将开放", PromptWindow.PromptType.Alert, null, null);
            };
        }

        SetLegionCopyRedPoint();
    }
    /*void SetLegionCopyWindow(BetterList<LegionGate> _fubenItemList)
    {
        for (int i = 0; i < _fubenItemList.size; i++)
        {
            if (uiGride.transform.FindChild(_fubenItemList[i].GateGroupID.ToString()) == null)
            {
                GameObject obj = NGUITools.AddChild(uiGride, legionCopyItem);
                obj.name = _fubenItemList[i].GateGroupID.ToString();
                obj.GetComponent<LegionCopyItem>().SetLegionCopyItem(_fubenItemList[i]);
            }
        }
        uiGride.GetComponent<UIGrid>().Reposition();
    }*/

    /// <summary>
    /// 设置排行的红点
    /// </summary>
    /// <param name="awardStatus"></param>
    /// <param name="_mList"></param>
    public void SetHertRankButtonRedPoint(int awardStatus, List<LegionPassData> _mList)
    {
        //Debug.LogError("hertRankButton::::" + awardStatus);
        if (awardStatus == 1)//可以领取
        {
            hertRankButton.transform.FindChild("RedPoint").gameObject.SetActive(true);
        }
        else
        {
            hertRankButton.transform.FindChild("RedPoint").gameObject.SetActive(false);
        }
    }

    public void SetLegionCopyRedPoint()
    {
        PreviewButton.transform.FindChild("RedPoint").gameObject.SetActive(CharacterRecorder.instance.AllRedPoint[52]);
    }

    public void SetLegionCopyWindow(int leftFightTimes, int GateGroupID, int buyTimes)
    {
        this.buyTimes = buyTimes;
        curGateGroupID = GateGroupID;
        curSelectGateGroupID = GateGroupID;

        mCurrentShowPage = (int)(GateGroupID / 5);
        if (GateGroupID % 5 == 0)
        {
            mCurrentShowPage -= 1;
        }
        if (curSelectGateGroupID <= 1)
        {
            leftButton.SetActive(false);
        }
        else
        {
            leftButton.SetActive(true);
        }

        if (curSelectGateGroupID == 5)// >= curGateGroupID && curGateGroupID!=)
        {
            rightButton.SetActive(false);
        }
        else if (curSelectGateGroupID >= curGateGroupID)
        {
            rightButton.SetActive(true);
            rightButton.GetComponent<UITexture>().shader = Shader.Find("Unlit/GrayShader");//Compiled-Alpha-Diffuse
        }
        else
        {
            rightButton.SetActive(true);
            rightButton.GetComponent<UITexture>().shader = Shader.Find("Unlit/Transparent Colored");
        }

        mNameLabel.text = string.Format("第{0}战区", GetNumberStr(mCurrentShowPage + 1));

        if ((mCurrentShowPage + 1) * 5 > TextTranslator.instance.LeginGateGroupList.size)
        {
            for (var i = GroupGameObject.Count - 1; i >= 0; i--)
            {
                GroupGameObject[i].name = TextTranslator.instance.LeginGateGroupList[TextTranslator.instance.LeginGateGroupList.size + i - 5].ToString();
                if (int.Parse(GroupGameObject[i].name) < GateGroupID)
                {
                    GroupGameObject[i].transform.FindChild("pass").gameObject.SetActive(false);
                    GroupGameObject[i].transform.FindChild("unpass").gameObject.SetActive(false);

                    GroupGameObject[i].transform.FindChild("Background").GetComponent<UITexture>().shader = Shader.Find("Unlit/Transparent Colored");//Compiled-Alpha-Diffuse

                }
                else if (int.Parse(GroupGameObject[i].name) == GateGroupID)
                {
                    GroupGameObject[i].transform.FindChild("pass").gameObject.SetActive(false);
                    GroupGameObject[i].transform.FindChild("unpass").gameObject.SetActive(false);

                    GroupGameObject[i].transform.FindChild("Background").GetComponent<UITexture>().shader = Shader.Find("Unlit/Transparent Colored");//Compiled-Alpha-Diffuse

                }
                else if (int.Parse(GroupGameObject[i].name) > GateGroupID)
                {
                    GroupGameObject[i].transform.FindChild("pass").gameObject.SetActive(false);
                    GroupGameObject[i].transform.FindChild("unpass").gameObject.SetActive(false);

                    GroupGameObject[i].transform.FindChild("Background").GetComponent<UITexture>().shader = Shader.Find("Unlit/GrayShader");//Compiled-Alpha-Diffuse

                }
            }
        }
        else
        {
            for (var i = 0; i < GroupGameObject.Count; i++)
            {
                GroupGameObject[i].name = TextTranslator.instance.LeginGateGroupList[mCurrentShowPage * 5 + i].ToString();
                if (int.Parse(GroupGameObject[i].name) < GateGroupID)
                {
                    GroupGameObject[i].transform.FindChild("pass").gameObject.SetActive(false);
                    GroupGameObject[i].transform.FindChild("unpass").gameObject.SetActive(false);

                    GroupGameObject[i].transform.FindChild("Background").GetComponent<UITexture>().shader = Shader.Find("Unlit/Transparent Colored");//Compiled-Alpha-Diffuse

                }
                else if (int.Parse(GroupGameObject[i].name) == GateGroupID)
                {
                    GroupGameObject[i].transform.FindChild("pass").gameObject.SetActive(false);
                    GroupGameObject[i].transform.FindChild("unpass").gameObject.SetActive(false);

                    GroupGameObject[i].transform.FindChild("Background").GetComponent<UITexture>().shader = Shader.Find("Unlit/Transparent Colored");//Compiled-Alpha-Diffuse

                }
                else if (int.Parse(GroupGameObject[i].name) > GateGroupID)
                {
                    GroupGameObject[i].transform.FindChild("pass").gameObject.SetActive(false);
                    GroupGameObject[i].transform.FindChild("unpass").gameObject.SetActive(false);

                    GroupGameObject[i].transform.FindChild("Background").GetComponent<UITexture>().shader = Shader.Find("Unlit/GrayShader");//Compiled-Alpha-Diffuse

                }
            }
        }

        NetworkHandler.instance.SendProcess(string.Format("8202#{0};", curSelectGateGroupID));
        leftFightTimesLabel.text = string.Format("{0}/{1}", leftFightTimes, 3);


        mHasInit = true;
        //SetLegionCopyWindow(TextTranslator.instance.LegionGateList);
        //Invoke("DelayCenterOn", 0.2f);
    }

    /// <summary>
    /// 改变显示的副本页签
    /// </summary>
    private void ChangeShowPage(int _currentPage)
    {
        mCurrentShowPage = _currentPage;

        mNameLabel.text = string.Format("第{0}战区", GetNumberStr(_currentPage + 1));

        if ((mCurrentShowPage + 1) * 5 > TextTranslator.instance.LeginGateGroupList.size)
        {
            for (var i = GroupGameObject.Count - 1; i >= 0; i--)
            {
                GroupGameObject[i].name = TextTranslator.instance.LeginGateGroupList[TextTranslator.instance.LeginGateGroupList.size + i - 5].ToString();
                if (int.Parse(GroupGameObject[i].name) < curGateGroupID)
                {
                    GroupGameObject[i].transform.FindChild("pass").gameObject.SetActive(false);
                    GroupGameObject[i].transform.FindChild("unpass").gameObject.SetActive(false);
                }
                else if (int.Parse(GroupGameObject[i].name) == curGateGroupID)
                {
                    GroupGameObject[i].transform.FindChild("pass").gameObject.SetActive(false);
                    GroupGameObject[i].transform.FindChild("unpass").gameObject.SetActive(false);
                }
                else if (int.Parse(GroupGameObject[i].name) > curGateGroupID)
                {
                    GroupGameObject[i].transform.FindChild("pass").gameObject.SetActive(false);
                    GroupGameObject[i].transform.FindChild("unpass").gameObject.SetActive(false);
                }
            }
        }
        else
        {
            for (var i = 0; i < GroupGameObject.Count; i++)
            {
                GroupGameObject[i].name = TextTranslator.instance.LeginGateGroupList[mCurrentShowPage * 5 + i].ToString();
                if (int.Parse(GroupGameObject[i].name) < curGateGroupID)
                {
                    GroupGameObject[i].transform.FindChild("pass").gameObject.SetActive(false);
                    GroupGameObject[i].transform.FindChild("unpass").gameObject.SetActive(false);
                }
                else if (int.Parse(GroupGameObject[i].name) == curGateGroupID)
                {
                    GroupGameObject[i].transform.FindChild("pass").gameObject.SetActive(false);
                    GroupGameObject[i].transform.FindChild("unpass").gameObject.SetActive(false);
                }
                else if (int.Parse(GroupGameObject[i].name) > curGateGroupID)
                {
                    GroupGameObject[i].transform.FindChild("pass").gameObject.SetActive(false);
                    GroupGameObject[i].transform.FindChild("unpass").gameObject.SetActive(false);
                }
            }
        }

        //curSelectGateGroupID = int.Parse(GroupGameObject[0].name);
        NetworkHandler.instance.SendProcess(string.Format("8202#{0};", curSelectGateGroupID));
    }

    #region 当前显示关卡信息
    public void SetLegionGachaEnterWindow(int leftTime, int GateGroupID, BetterList<int> percentList)
    {
        this.leftTime = leftTime;
        this.percentList = percentList;
        CancelInvoke();
        InvokeRepeating("UpdateTime", 0, 1.0f);


        BetterList<LegionGate> mList = TextTranslator.instance.GetLegionSmallGateListByGateGroupID(GateGroupID);

        for (var i = 0; i < GroupGameObject.Count; i++)
        {
            if (int.Parse(GroupGameObject[i].name) <= curGateGroupID)
            {
                GroupGameObject[i].transform.FindChild("Background").GetComponent<UITexture>().shader = Shader.Find("Unlit/Transparent Colored");//Compiled-Alpha-Diffuse
                //Unlit - Transparent Colored
            }
            else
            {
                GroupGameObject[i].transform.FindChild("Background").GetComponent<UITexture>().shader = Shader.Find("Unlit/GrayShader");//Compiled-Alpha-Diffuse

            }
        }

        if (curSelectGateGroupID <= 1)
        {
            leftButton.SetActive(false);
        }
        else
        {
            leftButton.SetActive(true);
        }

        if (curSelectGateGroupID == 5)// >= curGateGroupID && curGateGroupID!=)
        {
            rightButton.SetActive(false);
        }
        else if (curSelectGateGroupID >= curGateGroupID)
        {
            rightButton.SetActive(true);
            rightButton.GetComponent<UITexture>().shader = Shader.Find("Unlit/GrayShader");//Compiled-Alpha-Diffuse
        }
        else
        {
            rightButton.SetActive(true);
            rightButton.GetComponent<UITexture>().shader = Shader.Find("Unlit/Transparent Colored");
        }
        //Debug.LogError("Create OK 000 !");
        for (var i = 0; i < GroupGameObject.Count; i++)
        {
            if (GroupGameObject[i].name == GateGroupID.ToString())
            {
                CreateItemContainerPrefab(i);

                //if (i % 2 == 0)
                //{

                //}
                /*else
                {
                    itemContainer.transform.localPosition = new Vector3(GroupGameObject[i].transform.localPosition.x, GroupGameObject[i].transform.localPosition.y - 90, GroupGameObject[i].transform.localPosition.z);
                    itemGridBg2.SetActive(true);
                    itemGridBg1.SetActive(false);
                }*/
                break;
            }
        }
        //Debug.LogError("Create OK !");
        int sumPercent = 0;
        for (int i = 0; i < ConfirmLegionItemList.Count; i++)
        {
            ConfirmLegionItemList[i].transform.FindChild("Select").gameObject.SetActive(false);
            ConfirmLegionItemList[i].transform.FindChild("Icon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetGateByID(mList[i].NextGateID).icon.ToString();
            ConfirmLegionItemList[i].transform.FindChild("PercentLabel").GetComponent<UILabel>().text = string.Format("{0}%", percentList[i]);
            if (percentList[i] == 100)
            {
                ConfirmLegionItemList[i].transform.FindChild("HadPass").gameObject.SetActive(true);
            }
            else
            {
                ConfirmLegionItemList[i].transform.FindChild("HadPass").gameObject.SetActive(false);
            }
            sumPercent += percentList[i];
        }
        mSlider.value = (float)sumPercent / ConfirmLegionItemList.Count / 100;
        mSliderLabel.text = string.Format("{0}%", sumPercent / ConfirmLegionItemList.Count);

        /* canGacha = percentList[0] == 100 ? true : false;
         ConfirmLegionItemList[0].transform.FindChild("Select").gameObject.SetActive(true);*/
        ClickConfirmLegionItemNew(ConfirmLegionItemList[LegionGachaWindow.curGateBoxID - 1]);

        
    }
    void CreateItemContainerPrefab(int index)
    {
        if (itemList.Count > 0)
        {
            for (int j = 0; j < itemList.Count; j++)
            {
                DestroyImmediate(itemList[j]);
            }
        }
        itemList.Clear();
        GameObject all = this.gameObject.transform.FindChild("All").gameObject;
        GameObject con = NGUITools.AddChild(all, itemContainer);

        con.SetActive(true);
        con.transform.localPosition = new Vector3(GroupGameObject[index].transform.localPosition.x, GroupGameObject[index].transform.localPosition.y + 110, GroupGameObject[index].transform.localPosition.z);
        ConfirmLegionItemList.Clear();
        for (int j = 0; j < con.transform.FindChild("ItemGrid").childCount; j++)
        {
            ConfirmLegionItemList.Add(con.transform.FindChild("ItemGrid").GetChild(j).gameObject);
        }
        //Debug.LogError("ConfirmLegionItemList" + ConfirmLegionItemList.Count);
        for (int i = 0; i < ConfirmLegionItemList.Count; i++)
        {
            UIEventListener.Get(ConfirmLegionItemList[i]).onClick = ClickConfirmLegionItemNew;
        }
        //itemContainer.SetActive(true);
        //itemContainer.transform.localPosition = new Vector3(GroupGameObject[i].transform.localPosition.x, GroupGameObject[i].transform.localPosition.y + 110, GroupGameObject[i].transform.localPosition.z);
        con.transform.FindChild("bg2").gameObject.SetActive(false);
        con.transform.FindChild("bg1").gameObject.SetActive(true);
        //itemGridBg2.SetActive(false);
        //itemGridBg1.SetActive(true);

        itemList.Add(con);
    }
    void UpdateTime()
    {
        if (leftTime > 0)
        {
            string houreStr = (leftTime / 3600).ToString("00");
            string miniteStr = (leftTime % 3600 / 60).ToString("00");
            string secondStr = (leftTime % 60).ToString("00");
            mTimeLabel.text = string.Format("{0}:{1}:{2}", houreStr, miniteStr, secondStr);
            leftTime -= 1;
        }
    }
    int NextGateID = 0;//关卡
    int GateBoxID = 1;//小关卡 1- 5
    void ClickConfirmLegionItemNew(GameObject go)
    {
        if (go != null)
        {
            switch (go.name)
            {
                case "ConfirmLegionItem1": GateBoxID = 1; break;
                case "ConfirmLegionItem2": GateBoxID = 2; break;
                case "ConfirmLegionItem3": GateBoxID = 3; break;
                case "ConfirmLegionItem4": GateBoxID = 4; break;
                case "ConfirmLegionItem5": GateBoxID = 5; break;
            }
            LegionGachaWindow.curGateBoxID = GateBoxID;
            UISprite _enterSprite = enterButton.GetComponent<UISprite>();
            for (int i = 0; i < ConfirmLegionItemList.Count; i++)
            {
                if (ConfirmLegionItemList[i].name == go.name)
                {
                    if (percentList[i] == 100)
                    {
                        LegionGachaWindow.curGateBoxID = GateBoxID;
                        _enterSprite.spriteName = "bingtuan_buttonword2";
                        _enterSprite.MakePixelPerfect();
                        canGacha = true;
                    }
                    else
                    {
                        _enterSprite.spriteName = "bingtuan_buttonword1";
                        _enterSprite.MakePixelPerfect();
                        canGacha = false;
                    }
                    ConfirmLegionItemList[i].transform.FindChild("Select").gameObject.SetActive(true);
                }
                else
                {
                    ConfirmLegionItemList[i].transform.FindChild("Select").gameObject.SetActive(false);
                }
            }

        }
    }

    void ClickEnterButton()
    {
        if (canGacha)
        {
            UIManager.instance.OpenPanel("LegionGachaWindow", true);
            GameObject _LegionGachaWindow = GameObject.Find("LegionGachaWindow");
            if (_LegionGachaWindow != null)
            {
                _LegionGachaWindow.GetComponent<LegionGachaWindow>().SetLegionGachaPercentList(this.percentList);
            }
        }
        else
        {
            if (percentList[GateBoxID - 1] == 100)
            {
                UIManager.instance.OpenPromptWindow("当前副本已通关", PromptWindow.PromptType.Hint, null, null);
                return;
            }
            if (CharacterRecorder.instance.legionFightLeftTimes <= 0)
            {
                UIManager.instance.OpenPromptWindow("没有副本挑战次数啦", PromptWindow.PromptType.Hint, null, null);
                return;
            }
            LegionGate _LegionGate = TextTranslator.instance.GetLegionGateByGroupIDAndBoxID(curSelectGateGroupID, GateBoxID);

            #region 接入战斗
            NextGateID = _LegionGate.NextGateID;
            UIManager.instance.OpenPanel("LoadingWindow", true);
            SceneTransformer.instance.NowGateID = NextGateID;
            PictureCreater.instance.StartFight();
            PictureCreater.instance.FightStyle = FightStyle;

            NetworkHandler.instance.SendProcess(string.Format("8305#{0};{1};", curSelectGateGroupID, GateBoxID));
            #endregion
        }
    }

    void ClickConfirmLegionItem(GameObject go)
    {
        if (go != null)
        {
            int NextGateID = 0;//关卡
            int GateBoxID = 0;//小关卡 1- 5
            switch (go.name)
            {
                case "ConfirmLegionItem1": GateBoxID = 1; break;
                case "ConfirmLegionItem2": GateBoxID = 2; break;
                case "ConfirmLegionItem3": GateBoxID = 3; break;
                case "ConfirmLegionItem4": GateBoxID = 4; break;
                case "ConfirmLegionItem5": GateBoxID = 5; break;
            }
            if (percentList[GateBoxID - 1] == 100)
            {
                UIManager.instance.OpenPromptWindow("当前副本已通关", PromptWindow.PromptType.Hint, null, null);
                return;
            }
            if (CharacterRecorder.instance.legionFightLeftTimes <= 0)
            {
                UIManager.instance.OpenPromptWindow("没有副本挑战次数啦", PromptWindow.PromptType.Hint, null, null);
                return;
            }
            LegionGate _LegionGate = TextTranslator.instance.GetLegionGateByGroupIDAndBoxID(curSelectGateGroupID, GateBoxID);
            #region 临时测试1
            /*  string sendBloodStr = "";
            for (int i = 0; i < _LegionGate.BossBloodList.size;i++ )
            {
                if (_LegionGate.BossBloodList[i] != 0)
                {
                    sendBloodStr += _LegionGate.BossBloodList[i] + "$";
                }
            }
            NetworkHandler.instance.SendProcess(string.Format("8306#{0};{1};{2};",LegionCopyItem.curClickLegionGate.GateGroupID, GateBoxID, sendBloodStr)); */

            //临时测试2
            //   NetworkHandler.instance.SendProcess(string.Format("8305#{0};{1};", LegionCopyItem.curClickLegionGate.GateGroupID, GateBoxID));
            #endregion


            #region 接入战斗
            NextGateID = _LegionGate.NextGateID;
            //CharacterRecorder.instance.EveryDiffID = 1;
            UIManager.instance.OpenPanel("LoadingWindow", true);
            SceneTransformer.instance.NowGateID = NextGateID;
            PictureCreater.instance.StartFight();
            PictureCreater.instance.FightStyle = FightStyle;

            NetworkHandler.instance.SendProcess(string.Format("8305#{0};{1};", curSelectGateGroupID, GateBoxID));
            #endregion

        }
    }

    #endregion

    private string GetNumberStr(int _value)
    {
        string returnValue = string.Empty;
        for (var i = 0; i < _value.ToString().Length; i++)
        {
            if (i == _value.ToString().Length - 1 && _value.ToString()[i].ToString() == "0")
            {
                return returnValue;
            }

            returnValue += GetSingleNum(int.Parse(_value.ToString()[i].ToString()));
            if (i == 0)
            {
                if (_value.ToString().Length == 3)
                {
                    returnValue += "百";
                }
                if (_value.ToString().Length == 2)
                {
                    returnValue += "十";
                }
            }

            if (i == 1)
            {
                if (_value.ToString().Length == 3)
                {
                    returnValue += "十";
                }
            }
        }

        return returnValue;
    }

    private string GetSingleNum(int _value)
    {
        switch (_value)
        {
            case 1:
                return "一";

            case 2:
                return "二";

            case 3:
                return "三";

            case 4:
                return "四";

            case 5:
                return "五";

            case 6:
                return "六";

            case 7:
                return "七";

            case 8:
                return "八";

            case 9:
                return "九";

            default:
                return "零";
        }
    }

    #region 次数购买
    public void ResetLegionFightTimes(int leftFightTimes, int buyTimes)
    {
        this.buyTimes = buyTimes;
        //leftFightTimesLabel.text = string.Format("{0}/{1}", leftFightTimes, 3 + buyTimes);
        leftFightTimesLabel.text = string.Format("{0}/{1}", leftFightTimes, 3);
    }
    void ConfirmBuy()
    {
        NetworkHandler.instance.SendProcess("8308#;");
    }
    #endregion
}
