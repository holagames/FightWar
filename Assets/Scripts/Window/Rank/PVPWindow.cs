using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PVPWindow : MonoBehaviour
{
    public UILabel mRankNumber;
    public UILabel mPowerNumber;
    public UILabel ChanceCount;
    public UILabel RefreshCost;
    public GameObject gride;
    public GameObject pvpItemObj;
    [SerializeField]
    private GameObject leftButton;
    [SerializeField]
    private GameObject rightButton;

    public GameObject[] TeamIconList;
    [HideInInspector]
    public List<TeamPosition> mTeamPosition = new List<TeamPosition>();
    [HideInInspector]
    public int HonerNumber = 0;

    string mRankIntegral;
    public List<GameObject> pvpItemList = new List<GameObject>();
    private Vector3 scrollViewInitPos = new Vector3(0, -30f, 0);
    private UICenterOnChild uiCenter;
    public GameObject RedPoint1;
    public GameObject RedPoint2;
    public GameObject RedPoint3;
    public UILabel UILabelTime;
    public int leftTime;
    public int leftCount;
    public int RefreshNum;
    //public UIPanel UIpanel;
    //private Vector3 PanelView;
    // void OnEnable() { }
    void Start()
    {
        if (CharacterRecorder.instance.PVPComeNum == 1)
        {
            CharacterRecorder.instance.PVPComeNum = 0;
        }
        uiCenter = gride.GetComponent<UICenterOnChild>();
        StartCoroutine(IsShowPvPMessage());

        UIEventListener.Get(leftButton).onClick = ClickLeftButton;
        UIEventListener.Get(rightButton).onClick = ClickRightButton;
        PictureCreater.instance.FightStyle = 1;
        if (UIEventListener.Get(GameObject.Find("RuleButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("RuleButton")).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPanel("RankRuleWindow", false);
            };
        }

        if (UIEventListener.Get(GameObject.Find("RankButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("RankButton")).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPanel("PVPListWindow", false);
            };
        }
        if (UIEventListener.Get(GameObject.Find("AwardButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("AwardButton")).onClick += delegate(GameObject go)
            {
                CharacterRecorder.instance.PVPComeNum = 0;
                UIManager.instance.OpenPanel("GetRankingReward", true);
                //NetworkHandler.instance.SendProcess("6011#;");
            };
        }

        if (UIEventListener.Get(GameObject.Find("PositionButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("PositionButton")).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPanel("RankPositionWindow", false);
            };
        }
        if (UIEventListener.Get(GameObject.Find("IntegrationButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("IntegrationButton")).onClick += delegate(GameObject go)
            {
                IntegrationButton_OnClick();
                //CharacterRecorder.instance.PVPComeNum = 0;
                //GameObject IntWindow= UIManager.instance.OpenPanel("IntegrationWindow", true);
                //IntWindow.GetComponent<IntegrationWindow>().GetInfo();
                //NetworkHandler.instance.SendProcess("6009#;");
            };
        }
        if (UIEventListener.Get(GameObject.Find("ShopButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("ShopButton")).onClick += delegate(GameObject go)
            {
                //NetworkHandler.instance.SendProcess("5102#10003;");
                CharacterRecorder.instance.PVPComeNum = 0;
                CharacterRecorder.instance.RankShopType = 10003;
                UIManager.instance.OpenPanel("RankShopWindow", true);
            };
        }
        if (UIEventListener.Get(GameObject.Find("ChangeButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("ChangeButton")).onClick += delegate(GameObject go)
            {
                //NetworkHandler.instance.SendProcess("6002#;");
                if (GameObject.Find("ChangeButton/Label").GetComponent<UILabel>().text == "换一批")
                {
                    //NetworkHandler.instance.SendProcess("6015#;");
                    //UIManager.instance.OpenPromptWindow("是否消耗钻石换一批？", PromptWindow.PromptType.Confirm, null, null);
                    NetworkHandler.instance.SendProcess("6015#;");
                }
                else if (GameObject.Find("ChangeButton/Label").GetComponent<UILabel>().text == "重 置")
                {
                    //NetworkHandler.instance.SendProcess("6016#;");
                    UIManager.instance.OpenPromptWindow("是否消费10钻石重置时间？", PromptWindow.PromptType.Confirm, null, null);
                }
                else if (GameObject.Find("ChangeButton/Label").GetComponent<UILabel>().text == "购买次数")
                {
                    NetworkHandler.instance.SendProcess("6013#;");
                    //UIManager.instance.OpenPromptWindow("是否消耗钻石购买挑战次数？",PromptWindow.PromptType.Confirm, null, null);
                }
            };
        }
        if (CharacterRecorder.instance.GuideID[21] == 7)
        {
            StartCoroutine(SceneTransformer.instance.NewbieGuide());
        }
        AudioEditer.instance.PlayLoop("Challenge");
        PositionButtonRedPoint();

        if (CharacterRecorder.instance.bagItemOpenWindows.ContainsKey("70000"))
        {
            IntegrationButton_OnClick();
            CharacterRecorder.instance.bagItemOpenWindows.Remove("70000");
        }
    }
    /// <summary>
    /// 积分按钮的Btn事件
    /// </summary>
    void IntegrationButton_OnClick()
    {
        CharacterRecorder.instance.PVPComeNum = 0;
        GameObject IntWindow = UIManager.instance.OpenPanel("IntegrationWindow", true);
        //IntWindow.GetComponent<IntegrationWindow>().GetInfo();
    }
    public void PositionButtonRedPoint() //布阵红点
    {
        if (PlayerPrefs.GetInt("PvpRankPodition_" + PlayerPrefs.GetString("ServerID") + "_" + PlayerPrefs.GetInt("UserID")) == 0)
        {
            RedPoint3.SetActive(true);
        }
        else
        {
            RedPoint3.SetActive(false);
        }
    }

    #region 左右点击
    private void ClickRightButton(GameObject go)
    {
        if (go != null)
        {
            AutoAddDragForce(true);
        }
    }
    private void ClickLeftButton(GameObject go)
    {
        if (go != null)
        {
            AutoAddDragForce(false);
        }
    }
    void AutoAddDragForce(bool clickRightButton)
    {
        if (clickRightButton)
        {
            SpringPanel.Begin(gride.transform.parent.gameObject, gride.transform.parent.gameObject.transform.localPosition + new Vector3(-1047, 0, 0), 10.0f);
        }
        else
        {
            SpringPanel.Begin(gride.transform.parent.gameObject, gride.transform.parent.gameObject.transform.localPosition + new Vector3(+1047, 0, 0), 10.0f);
        }
    }
    #endregion

    public void SetInfo(string _LeftCount, string _RankNumber, string _HonerNumber, string _PowerNumber, string _leftTime)//6001
    {                   //挑战次数          排名                  荣耀值             总战力             倒计时
        HonerNumber = int.Parse(_HonerNumber);
        mRankNumber.text = _RankNumber;
        leftTime = int.Parse(_leftTime);

        leftCount = int.Parse(_LeftCount);
        ChanceCount.text = string.Format("挑战次数:{0}{1}/{2}", "[f8911d]", int.Parse(_LeftCount), 5);
        if (_LeftCount == "0")
        {
            ChanceCount.text = string.Format("挑战次数:{0}{1}{2}{3}/{4}{5}", "[ff0000]", int.Parse(_LeftCount), "[-]", "[f8911d]", 5, "[-]");
        }

        mPowerNumber.text = CharacterRecorder.instance.Fight.ToString();

        mTeamPosition.Clear();
        for (int i = 0; i < TeamIconList.Length; i++)
        {
            TeamIconList[i].SetActive(false);
        }
        NetworkHandler.instance.SendProcess("6006#6;");
    }

    /// <summary>
    /// 6002
    /// </summary>
    void DestroyGride()
    {
        pvpItemList.Clear();
        for (int i = gride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(gride.transform.GetChild(i).gameObject);
        }
    }
    public void SetEnemyInfo(List<PVPItemData> _mList)//6002
    {
        gride.transform.parent.localPosition = new Vector3(0, -30f, 0);
        gride.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2(0, 0);
        DestroyGride();
        if (CharacterRecorder.instance.PVPComeNum == 0)
        {
            for (int i = 0; i < _mList.Count; i++)
            {
                if (_mList[i].playerID == CharacterRecorder.instance.userId)
                {
                    CharacterRecorder.instance.RankNumber = _mList[i].rankNum;
                }
            }
            for (int i = 0; i < _mList.Count; i++)
            {
                GameObject obj = NGUITools.AddChild(gride, pvpItemObj) as GameObject;
                obj.transform.localPosition = new Vector3(195f * i, 0, 0);
                //if (_mList[i].rankNum <= 10)
                //{
                //    obj.transform.Find("SpriteBg").GetComponent<UITexture>().mainTexture = (Texture)Resources.Load("Game/diPvp7");
                //}
                //if (_mList[i].rankNum <= 10 && CharacterRecorder.instance.RankNumber > 20)
                //{
                //    obj.transform.Find("FightButton").gameObject.SetActive(false);
                //    obj.transform.Find("FightButtonHui").gameObject.SetActive(true);
                //}

                //if (_mList[i].playerID == CharacterRecorder.instance.userId)
                //{
                //    obj.transform.Find("SpriteBg").GetComponent<UITexture>().mainTexture = (Texture)Resources.Load("Game/diPvp2");
                //    obj.transform.Find("FightButton").gameObject.SetActive(false);
                //    obj.transform.Find("KuangSprite").gameObject.SetActive(true);
                //}
                SetPvpItemSprite(obj, _mList[i].rankNum, _mList[i].playerID);
                obj.GetComponent<PVPItem>().SetPVPItemInfo(_mList[i]);
                pvpItemList.Add(obj);
            }
            gride.GetComponent<UIGrid>().Reposition();
            StartCoroutine(CenterOnView(_mList));
        }
        else
        {
            for (int i = 0; i < CharacterRecorder.instance.PVPItemList.Count; i++)
            {
                if (CharacterRecorder.instance.PVPItemList[i].playerID == CharacterRecorder.instance.userId)
                {
                    CharacterRecorder.instance.RankNumber = CharacterRecorder.instance.PVPItemList[i].rankNum;
                }
            }
            for (int i = 0; i < CharacterRecorder.instance.PVPItemList.Count; i++)
            {
                GameObject obj = NGUITools.AddChild(gride, pvpItemObj) as GameObject;
                //if (CharacterRecorder.instance.PVPItemList[i].rankNum <= 10)
                //{
                //    obj.transform.Find("SpriteBg").GetComponent<UITexture>().mainTexture = (Texture)Resources.Load("Game/diPvp7");
                //    //obj.transform.Find("SpriteBg").GetComponent<UISprite>().spriteName = "diPvp7";
                //}

                //if (CharacterRecorder.instance.PVPItemList[i].rankNum <= 10 && CharacterRecorder.instance.RankNumber > 20)
                //{
                //    obj.transform.Find("FightButton").gameObject.SetActive(false);
                //    //obj.transform.Find("FightButtonHui").GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
                //    obj.transform.Find("FightButtonHui").gameObject.SetActive(true);
                //}
                //if (CharacterRecorder.instance.PVPItemList[i].playerID == CharacterRecorder.instance.userId)
                //{
                //    //obj.transform.Find("SpriteBg").GetComponent<UISprite>().spriteName = "diPvp0";
                //    obj.transform.Find("SpriteBg").GetComponent<UITexture>().mainTexture = (Texture)Resources.Load("Game/diPvp2");
                //    obj.transform.Find("FightButton").gameObject.SetActive(false);
                //    obj.transform.Find("KuangSprite").gameObject.SetActive(true);
                //}

                SetPvpItemSprite(obj, CharacterRecorder.instance.PVPItemList[i].rankNum, CharacterRecorder.instance.PVPItemList[i].playerID);
                obj.GetComponent<PVPItem>().SetPVPItemInfo(CharacterRecorder.instance.PVPItemList[i]);
                pvpItemList.Add(obj);
            }
            //uiGrid.transform.parent.GetComponent<UIPanel>().clipOffset
            gride.GetComponent<UIGrid>().Reposition();
            StartCoroutine(CenterOnView(CharacterRecorder.instance.PVPItemList));
            StartCoroutine(TweenPositionWin());
        }

    }
    IEnumerator CenterOnView(List<PVPItemData> _mList)//居中
    {
        uiCenter.enabled = true;
        int num = 0;
        for (int i = 0; i < _mList.Count; i++)
        {
            if (_mList[i].playerID == CharacterRecorder.instance.userId)
            {
                num = i;
            }
        }
        yield return new WaitForSeconds(0.5f);
        if (num > 4)
        {
            uiCenter.CenterOn(gride.transform.GetChild(num - 2));
            uiCenter.enabled = false;
        }
        uiCenter.enabled = false;
    }


    private void SetPvpItemSprite(GameObject pvpObj, int rankNum, int playerID) 
    {
        if (rankNum <= 10)
        {
            pvpObj.transform.Find("SpriteBg").GetComponent<UITexture>().mainTexture = (Texture)Resources.Load("Game/diPvp7");
            if (rankNum == 1) 
            {
                pvpObj.transform.Find("KuangBgSprite").gameObject.SetActive(true);
                pvpObj.transform.Find("KuangBgSprite/TopSprite").gameObject.SetActive(true);
                pvpObj.transform.Find("KuangBgSprite").GetComponent<UISprite>().spriteName = "Pvpkuang1";
                pvpObj.transform.Find("KuangBgSprite/TopSprite").GetComponent<UISprite>().spriteName = "Pvpicon1";
                pvpObj.transform.Find("KuangBgSprite/TopSprite/Sprite").GetComponent<UISprite>().spriteName = "word1";
            }
            else if (rankNum == 2) 
            {
                pvpObj.transform.Find("KuangBgSprite").gameObject.SetActive(true);
                pvpObj.transform.Find("KuangBgSprite/TopSprite").gameObject.SetActive(true);
                pvpObj.transform.Find("KuangBgSprite").GetComponent<UISprite>().spriteName = "Pvpkuang2";
                pvpObj.transform.Find("KuangBgSprite/TopSprite").GetComponent<UISprite>().spriteName = "Pvpicon2";
                pvpObj.transform.Find("KuangBgSprite/TopSprite/Sprite").GetComponent<UISprite>().spriteName = "word2";
            }
            else if (rankNum == 3)
            {
                pvpObj.transform.Find("KuangBgSprite").gameObject.SetActive(true);
                pvpObj.transform.Find("KuangBgSprite/TopSprite").gameObject.SetActive(true);
                pvpObj.transform.Find("KuangBgSprite").GetComponent<UISprite>().spriteName = "Pvpkuang3";
                pvpObj.transform.Find("KuangBgSprite/TopSprite").GetComponent<UISprite>().spriteName = "Pvpicon3";
                pvpObj.transform.Find("KuangBgSprite/TopSprite/Sprite").GetComponent<UISprite>().spriteName = "word3";
            }
            else 
            {
                pvpObj.transform.Find("KuangBgSprite").gameObject.SetActive(true);
                pvpObj.transform.Find("KuangBgSprite").GetComponent<UISprite>().enabled = false;
                pvpObj.transform.Find("KuangBgSprite/NumSprite").gameObject.SetActive(true);
                pvpObj.transform.Find("KuangBgSprite/NumSprite/Label").GetComponent<UILabel>().text = rankNum.ToString();
            }
            
        }
        if (rankNum <= 10 && CharacterRecorder.instance.RankNumber > 20)
        {
            pvpObj.transform.Find("FightButton").gameObject.SetActive(false);
            pvpObj.transform.Find("FightButtonHui").gameObject.SetActive(true);
        }

        if (playerID == CharacterRecorder.instance.userId)
        {
            pvpObj.transform.Find("SpriteBg").GetComponent<UITexture>().mainTexture = (Texture)Resources.Load("Game/diPvp2");
            pvpObj.transform.Find("FightButton").gameObject.SetActive(false);
            pvpObj.transform.Find("KuangSprite").gameObject.SetActive(true);
            pvpObj.transform.Find("KuangBgSprite").gameObject.SetActive(false);
        }
    }




    /// <summary>
    /// 6006
    /// </summary>
    /// <param name="_TeamPositionList"></param>
    public void SetTeamInfo(List<TeamPosition> _TeamPositionList)
    {
        mTeamPosition = _TeamPositionList;
        for (int i = 0; i < _TeamPositionList.Count; i++)
        {
            Hero _Hero = CharacterRecorder.instance.GetHeroByCharacterRoleID(_TeamPositionList[i]._CharacterID);
            TeamIconList[i].SetActive(true);
            TeamIconList[i].transform.FindChild("Icon").GetComponent<UISprite>().spriteName = _Hero.cardID.ToString();
        }
    }
    public void SetTeamInfo(int index, int mCharacterID, int mCharacterPosition)
    {
        TeamPosition mPosition = new TeamPosition();
        mPosition._CharacterID = mCharacterID;
        mPosition._CharacterPosition = mCharacterPosition;
        mTeamPosition.Add(mPosition);
        foreach (var item in CharacterRecorder.instance.ownedHeroList)
        {
            if (item.characterRoleID == mCharacterID)
            {
                TeamIconList[index].SetActive(true);
                TeamIconList[index].transform.FindChild("Icon").GetComponent<UISprite>().spriteName = item.cardID.ToString();
            }
        }
    }
    public void _UpdataSetTeamInfo(int index, int mCharacterID, int mCharacterPosition)
    {
        if (index == 0)
        {
            mTeamPosition.Clear();
            for (int i = 0; i < TeamIconList.Length; i++)
            {
                TeamIconList[i].SetActive(false);
            }
        }
        TeamPosition mPosition = new TeamPosition();
        mPosition._CharacterID = mCharacterID;
        mPosition._CharacterPosition = mCharacterPosition;
        mTeamPosition.Add(mPosition);

        foreach (var item in CharacterRecorder.instance.ownedHeroList)
        {
            if (item.characterRoleID == mCharacterID)
            {
                Debug.Log(mCharacterID);
                TeamIconList[index].SetActive(true);
                TeamIconList[index].transform.FindChild("Icon").GetComponent<UISprite>().spriteName = item.cardID.ToString();
            }
        }
    }

    /// <summary>
    /// 开始发送协议
    /// </summary>
    /// <returns></returns>
    IEnumerator IsShowPvPMessage()
    {
        NetworkHandler.instance.SendProcess("6001#;");
        yield return new WaitForSeconds(0.02f);
        NetworkHandler.instance.SendProcess("6002#;");
        yield return new WaitForSeconds(0.1f);
        NetworkHandler.instance.SendProcess("6009#;");
        yield return new WaitForSeconds(0.02f);
        NetworkHandler.instance.SendProcess("6011#;");
        yield return new WaitForSeconds(0.5f);
        if (CharacterRecorder.instance.GetRankLayer < CharacterRecorder.instance.HaveRankLayer)//奖励与积分红点显示
        {
            RedPoint1.SetActive(true);
        }
        else
        {
            RedPoint1.SetActive(false);
        }

        if (CharacterRecorder.instance.GetPointLayer < 10 && CharacterRecorder.instance.PvpPoint >= CharacterRecorder.instance.GetPointLayer * 2 + 2)
        {
            RedPoint2.SetActive(true);
        }
        else
        {
            RedPoint2.SetActive(false);
        }

        if (leftCount > 0 && leftTime > 0) //按钮三种状态
        {
            RefreshCost.transform.parent.gameObject.SetActive(true);
            UILabelTime.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
            CancelInvoke();
            InvokeRepeating("UpdateTime", 0, 1.0f);

            GameObject.Find("ChangeButton").transform.Find("Label").GetComponent<UILabel>().text = "重 置";
            RefreshCost.text = "10";
        }
        else if (leftCount == 0)
        {
            RefreshCost.transform.parent.gameObject.SetActive(true);
            GameObject.Find("ChangeButton").transform.Find("Label").GetComponent<UILabel>().text = "购买次数";
            GameObject.Find("RefreshCostType").transform.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
        }
        else if (leftCount > 0 && leftTime == 0)
        {
            RefreshCost.transform.parent.gameObject.SetActive(false);
            //GetRefreshNum();
        }
    }


    public void GetResultWindow()   //防止红点显示不全，再次呼叫
    {
        if (CharacterRecorder.instance.GetRankLayer < CharacterRecorder.instance.HaveRankLayer)//奖励与积分红点显示
        {
            RedPoint1.SetActive(true);
        }
        else
        {
            RedPoint1.SetActive(false);
        }

        if (CharacterRecorder.instance.GetPointLayer < 10 && CharacterRecorder.instance.PvpPoint >= CharacterRecorder.instance.GetPointLayer * 2 + 2)
        {
            RedPoint2.SetActive(true);
        }
        else
        {
            RedPoint2.SetActive(false);
        }

        if (leftCount > 0 && leftTime > 0) //按钮三种状态
        {
            RefreshCost.transform.parent.gameObject.SetActive(true);
            UILabelTime.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
            CancelInvoke();
            InvokeRepeating("UpdateTime", 0, 1.0f);

            GameObject.Find("ChangeButton").transform.Find("Label").GetComponent<UILabel>().text = "重 置";
            RefreshCost.text = "10";
        }
        else if (leftCount == 0)
        {
            RefreshCost.transform.parent.gameObject.SetActive(true);
            GameObject.Find("ChangeButton").transform.Find("Label").GetComponent<UILabel>().text = "购买次数";
            GameObject.Find("RefreshCostType").transform.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
        }
        else if (leftCount > 0 && leftTime == 0)
        {
            RefreshCost.transform.parent.gameObject.SetActive(false);
            //GetRefreshNum();
        }
    }


    public void GetRefreshNum()//刷新次数
    {
        RefreshCost.transform.parent.gameObject.SetActive(false);
        //if (RefreshNum <= 20) 
        //{
        //    RefreshCost.text = TextTranslator.instance.GetMarketByID(RefreshNum).PvpRefreshDiamondCost.ToString();
        //}
        //else if (RefreshNum > 20 && RefreshNum <= 50) 
        //{
        //    RefreshCost.text = TextTranslator.instance.GetMarketByID(21).PvpRefreshDiamondCost.ToString();
        //}
        //else if (RefreshNum > 50 && RefreshNum <= 100) 
        //{
        //    RefreshCost.text = TextTranslator.instance.GetMarketByID(22).PvpRefreshDiamondCost.ToString();
        //}
        //else if (RefreshNum > 100) 
        //{
        //    RefreshCost.text = TextTranslator.instance.GetMarketByID(23).PvpRefreshDiamondCost.ToString();
        //}

        //if (int.Parse(RefreshCost.text) == 0) 
        //{
        //    RefreshCost.text = "免费";
        //}
    }
    void UpdateTime()
    {
        if (leftTime > 0)
        {
            string miniteStr = (leftTime % 3600 / 60).ToString("00");
            string secondStr = (leftTime % 60).ToString("00");
            UILabelTime.text = string.Format("{0}:{1}", miniteStr, secondStr);
            leftTime -= 1;
        }
        else if (leftTime == 0)//&&leftCount>0
        {
            UILabelTime.color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
            GameObject.Find("ChangeButton").transform.Find("Label").GetComponent<UILabel>().text = "换一批";
            RefreshCost.transform.parent.gameObject.SetActive(false);
            //GetRefreshNum();
            leftTime -= 1;
        }
    }

    IEnumerator TweenPositionWin() //交换的的动画效果
    {
        yield return new WaitForSeconds(1f);
        ////////////////方法1
        GameObject MyObject = GameObject.Find(CharacterRecorder.instance.userId.ToString());
        GameObject OtherObject = GameObject.Find(CharacterRecorder.instance.PVPRoleID.ToString());
        Vector3 OtherPosition = GameObject.Find(CharacterRecorder.instance.PVPRoleID.ToString()).transform.localPosition;
        Vector3 MyPosition = GameObject.Find(CharacterRecorder.instance.userId.ToString()).transform.localPosition;

        MyObject.GetComponent<TweenPosition>().from = MyPosition;
        MyObject.GetComponent<TweenPosition>().to = OtherPosition;

        OtherObject.GetComponent<TweenPosition>().from = OtherPosition;
        OtherObject.GetComponent<TweenPosition>().to = MyPosition;

        MyObject.GetComponent<UIPanel>().depth = 7;
        OtherObject.GetComponent<UIPanel>().depth = 7;
        MyObject.GetComponent<TweenPosition>().PlayForward();
        OtherObject.GetComponent<TweenPosition>().PlayForward();//动画交换结束
        yield return new WaitForSeconds(1.7f);

        CharacterRecorder.instance.PVPComeNum = 0;
        NetworkHandler.instance.SendProcess("6002#;");

        //交换排名，颜色设置等
        /*       int MyObject_RankNumber = int.Parse(MyObject.transform.Find("RankDes/RankNumber").GetComponent<UILabel>().text);
               MyObject.transform.Find("RankDes/RankNumber").GetComponent<UILabel>().text = CharacterRecorder.instance.PVPRankNumber.ToString();
               OtherObject.transform.Find("RankDes/RankNumber").GetComponent<UILabel>().text = MyObject_RankNumber.ToString();//CharacterRecorder.instance.RankNumber.ToString();
               if (CharacterRecorder.instance.RankNumber < 10)
               {
                   OtherObject.transform.Find("SpriteBg").GetComponent<UITexture>().mainTexture = (Texture)Resources.Load("Game/diPvp7");
                   //OtherObject.transform.Find("FightButton").gameObject.SetActive(false);
               }
               else
               {
                   OtherObject.transform.Find("SpriteBg").GetComponent<UITexture>().mainTexture = (Texture)Resources.Load("Game/diPvp2");
               }
               if (CharacterRecorder.instance.PVPRankNumber <= 20)
               {
                   for (int i = 0; i < 10; i++)
                   {
                       gride.transform.GetChild(i).transform.Find("FightButton").gameObject.SetActive(true);
                       gride.transform.GetChild(i).transform.Find("FightButtonHui").GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
                   }
               }

               uiCenter.enabled = true;//重新居中
               int num = 0;
               for (int i = 0; i < gride.transform.childCount; i++)
               {
                   if (OtherObject.name == gride.transform.GetChild(i).gameObject.name)
                   {
                       num = i;
                   }
               }
               yield return new WaitForSeconds(0.5f);
               if (num > 4)
               {
                   uiCenter.CenterOn(gride.transform.GetChild(num - 2));
                   uiCenter.enabled = false;
               }
               uiCenter.enabled = false;

               int myItemnum = 0;//交换数组位置，从新排序item位置
               int otherItemnum = 0;
               for (int i = 0; i < CharacterRecorder.instance.PVPItemList.Count; i++)
               {
                   if (CharacterRecorder.instance.PVPItemList[i].playerID == CharacterRecorder.instance.userId)
                   {
                       myItemnum = i;
                       CharacterRecorder.instance.PVPItemList[i].rankNum = CharacterRecorder.instance.PVPRankNumber;
                   }
                   if (CharacterRecorder.instance.PVPItemList[i].playerID == int.Parse(OtherObject.name))
                   {
                       otherItemnum = i;
                       CharacterRecorder.instance.PVPItemList[i].rankNum = CharacterRecorder.instance.RankNumber;
                   }
               }
               PVPItemData Titemdata = CharacterRecorder.instance.PVPItemList[otherItemnum];
               CharacterRecorder.instance.PVPItemList[otherItemnum] = CharacterRecorder.instance.PVPItemList[myItemnum];
               CharacterRecorder.instance.PVPItemList[myItemnum] = Titemdata;

               CharacterRecorder.instance.RankNumber = CharacterRecorder.instance.PVPRankNumber;
               GameObject.Find("PVPWindow/ALL/Top/Label/LabelRank").GetComponent<UILabel>().text = CharacterRecorder.instance.RankNumber.ToString();
               yield return new WaitForSeconds(1f);

               CharacterRecorder.instance.PVPComeNum = 0;
               NetworkHandler.instance.SendProcess("6002#;");*/

        ////////////////方法2
        //GameObject MyObject = GameObject.Find(CharacterRecorder.instance.userId.ToString());
        //GameObject OtherObject = GameObject.Find(CharacterRecorder.instance.PVPRoleID.ToString());
        //Vector3 MyPosition = GameObject.Find(CharacterRecorder.instance.userId.ToString()).transform.localPosition;
        //Vector3 MyPositionWord = GameObject.Find(CharacterRecorder.instance.userId.ToString()).transform.position;
        //GameObject obj = Instantiate(MyObject, MyObject.transform.position, Quaternion.identity) as GameObject;

        //int myobj=0;
        //int otherobj=0;
        //for (int i = 0; i < CharacterRecorder.instance.PVPItemList.Count; i++) 
        //{
        //    if (CharacterRecorder.instance.PVPItemList[i].playerID == CharacterRecorder.instance.userId) 
        //    {
        //        myobj = i;
        //    }
        //    if (CharacterRecorder.instance.PVPItemList[i].playerID == CharacterRecorder.instance.PVPRoleID) 
        //    {
        //        otherobj = i;
        //    }
        //}
        //PVPItemData t = CharacterRecorder.instance.PVPItemList[myobj];
        //CharacterRecorder.instance.PVPItemList[myobj] = CharacterRecorder.instance.PVPItemList[otherobj];
        //CharacterRecorder.instance.PVPItemList[otherobj] = t;
        //yield return new WaitForSeconds(0.2f);
        //uiCenter.CenterOn(GameObject.Find(CharacterRecorder.instance.userId.ToString()).transform);
        //uiCenter.enabled = false;
        //CharacterRecorder.instance.PVPItemList.Clear();
        //for (int i = gride.transform.childCount - 1; i >= 0; i--)
        //{
        //    CharacterRecorder.instance.PVPItemList.Add(gride.transform.GetChild(i).gameObject);
        //}
        //for (int i = 0; i < CharacterRecorder.instance.PVPItemList.Count - 1; i++) 
        //{
        //    for (int j = 0; j < CharacterRecorder.instance.PVPItemList.Count - 1 - i; j++) 
        //    {
        //        if (CharacterRecorder.instance.PVPItemList[j].transform.localPosition.y > CharacterRecorder.instance.PVPItemList[j + 1].transform.localPosition.y) 
        //        {
        //            GameObject a = CharacterRecorder.instance.PVPItemList[j];
        //            CharacterRecorder.instance.PVPItemList[j] = CharacterRecorder.instance.PVPItemList[j + 1];
        //            CharacterRecorder.instance.PVPItemList[j + 1] = a;
        //        }
        //    }
        //}
    }
}


public class TeamPosition
{
    internal int _CharacterID = 0;
    internal int _CharacterPosition = 0;
}
