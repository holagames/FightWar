using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionHertRankWindow: MonoBehaviour 
{
    public GameObject closeButton;
    public GameObject uiGride;
    public List<UIToggle> ListTabs = new List<UIToggle>();
    public List<GameObject> ListParts = new List<GameObject>();
    //军团通关 相关
    public GameObject redPoint;

    public GameObject LegionPassItem;
    public UILabel myLegionRankNumLabel;
    public UILabel myLegionNameLabel;
    public UILabel myLegionTimeLabel;
    public GameObject LeginPassAwardItem;
    public GameObject LeginPassAwardParent;
    public GameObject LeginPassNoAwardLable;
    private int _mypassRank;
    public GameObject LeginPassGetAwardBtn;
    public GameObject LeginPassAwarduiGride;
    private int _awardStatus = 0;
    public GameObject hasGetAwardSprite;
    private bool hasGetPassInfo = false;
    private List<LegionPassData> mLegionPassDataList;



    //伤害排行 相关
    public GameObject LegionMemberItem;
    public UILabel myRankNumLabel;
    public UILabel myNameLabel;
    public UILabel myHertLabel;
    public UILabel myAwardsLabel;
    private bool hasGethurtInfo = false;
    private List<LegionMemberData> mLegionMemberDataList;
       
    
	// Use this for initialization
	void Start () 
    {
        //NetworkHandler.instance.SendProcess("8307#;");
        if (UIEventListener.Get(closeButton).onClick == null)
        {
            UIEventListener.Get(closeButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }

        if (UIEventListener.Get(LeginPassGetAwardBtn).onClick == null)
        {
            UIEventListener.Get(LeginPassGetAwardBtn).onClick += delegate(GameObject go)
            {
                if(_awardStatus == 1)
                {
                    NetworkHandler.instance.SendProcess(string.Format("8310#{0};", LegionCopyWindowNew.curSelectGateGroupID));                    
                }
            };
        }
        for (int i = 0; i < ListTabs.Count; i++)
        {
            ListTabs[i].gameObject.name = i.ToString();
            UIEventListener.Get(ListTabs[i].gameObject).onClick = ClickListTabs;
        }
        //CharacterRecorder.instance.LegionHertRankTab = 0;
        SendToSeverToGetList(CharacterRecorder.instance.LegionHertRankTab);
	}
    #region 军团通关排行

    public void SetRedPoint(int awardStatus)
    {
        if (awardStatus==1)
        {
            redPoint.SetActive(true);
        }
        else
        {
            redPoint.SetActive(false);
        }
    }
    public void SetLegionPassRankWindow(int awardStatus,List<LegionPassData> _LegionMemberDataList)//, LegionMemberData _MyLegionMemberData, 
    {
        DestroyGride();
        ListParts[0].SetActive(true);
        ListParts[1].SetActive(false);
        hasGetPassInfo = true;
        myLegionNameLabel.text = "未上榜";
        _mypassRank = 11;
        _awardStatus = awardStatus;
        mLegionPassDataList = _LegionMemberDataList;
        for (int i = 0; i < _LegionMemberDataList.Count; i++)
        {
            GameObject go = NGUITools.AddChild(uiGride, LegionPassItem);
            go.GetComponent<LegionPassItem>().SetLegionHertRankItem(_LegionMemberDataList[i]);
            if (_LegionMemberDataList[i].legionId == CharacterRecorder.instance.legionID)
            {
                _mypassRank = _LegionMemberDataList[i].rankNum;
                myLegionRankNumLabel.text = _LegionMemberDataList[i].rankNum.ToString();
                myLegionNameLabel.text = _LegionMemberDataList[i].legionName;
                myLegionTimeLabel.text = Utils.GetTime(_LegionMemberDataList[i].time.ToString()).ToString("yyyy-MM-dd HH:mm");
            }
        }
        uiGride.GetComponent<UIGrid>().Reposition();

        LegionRank mRank = TextTranslator.instance.GetLegionFirstPassByRankID(_mypassRank);

        while (LeginPassAwarduiGride.transform.childCount>0)
        {
            GameObject.DestroyImmediate(LeginPassAwarduiGride.transform.GetChild(0));
        }
        
        for (var i = 0; i < mRank.AwardItemList.size;i++ )
        {            
            GameObject go = NGUITools.AddChild(LeginPassAwarduiGride, LeginPassAwardItem);
                       
            TextTranslator.ItemInfo mItem = TextTranslator.instance.GetItemByItemCode(mRank.AwardItemList[i].itemCode);
            if (mItem.itemType == 7 || mItem.itemType == 8)
            {
                go.transform.Find("Fragment").gameObject.SetActive(true);
            }
            else
            {
                go.transform.Find("Fragment").gameObject.SetActive(false);
            }
            go.GetComponent<UISprite>().spriteName =  "Grade"+(mItem.itemGrade);
            go.SetActive(true);
            go.transform.FindChild("SpriteItem").GetComponent<UISprite>().spriteName = mItem.picID.ToString();
        }

        LeginPassAwarduiGride.GetComponent<UIGrid>().Reposition();
        //redPoint.SetActive(false);
        if (awardStatus == 0)
        {
            LeginPassAwardParent.SetActive(false);
            LeginPassNoAwardLable.SetActive(true);
        }
        else if (awardStatus == 1)//可以领取
        {
            LeginPassNoAwardLable.SetActive(false);
            LeginPassAwardParent.SetActive(true);
            hasGetAwardSprite.SetActive(false);
            LeginPassGetAwardBtn.SetActive(true);
            //redPoint.SetActive(true);
        }
        else
        {
            LeginPassNoAwardLable.SetActive(false);
            LeginPassAwardParent.SetActive(true);
            LeginPassGetAwardBtn.SetActive(false);
            hasGetAwardSprite.SetActive(true);
        }
    }

    /// <summary>
    /// 领取通光奖励的结果
    /// </summary>
    /// <param name="result"></param>
    public void GetLegionPassAwardResult(int result, List<Item> _itemList)
    {
        if(result == 1)
        {
            _awardStatus = 2;
            LeginPassNoAwardLable.SetActive(false);
            LeginPassAwardParent.SetActive(true);
            LeginPassGetAwardBtn.SetActive(false);
            hasGetAwardSprite.SetActive(true);
            UIManager.instance.OpenSinglePanel("AdvanceWindow", false);
            GameObject.Find("AdvanceWindow").GetComponent<AdvanceWindow>().SetInfo(AdvanceWindowType.GainResult, null, null, null, null, _itemList);
            
        }
    }
    #endregion
    #region 伤害排行
    public void SetLegionHertRankWindow(List<LegionMemberData> _LegionMemberDataList)//, LegionMemberData _MyLegionMemberData, 
    {
        DestroyGride();
        ListParts[0].SetActive(false);
        ListParts[1].SetActive(true);
        hasGethurtInfo = true;
        myNameLabel.text = "未上榜";
        mLegionMemberDataList = _LegionMemberDataList;
        for (int i = 0; i < _LegionMemberDataList.Count; i++)
        {
            GameObject go = NGUITools.AddChild(uiGride, LegionMemberItem);
            go.GetComponent<LegionHertRankItem>().SetLegionHertItemInfo(_LegionMemberDataList[i]);
            if (_LegionMemberDataList[i].uId == CharacterRecorder.instance.userId)
            {
                myRankNumLabel.text = _LegionMemberDataList[i].rankNum.ToString();
                myNameLabel.text = _LegionMemberDataList[i].name;
                myHertLabel.text = _LegionMemberDataList[i].sumHert.ToString();
                myAwardsLabel.text = TextTranslator.instance.GetLegionRankByRankNum(_LegionMemberDataList[i].rankNum).AwardItemList[0].itemCount.ToString();
            }
        }
        uiGride.GetComponent<UIGrid>().Reposition();
    }
    #endregion
  
    void ClickListTabs(GameObject go)
    {
        if (go != null)
        {
            //SetFriendWindow(int.Parse(go.name));
            if (go.name == "0" && hasGetPassInfo)
            {
                ListParts[0].SetActive(true);
                ListParts[1].SetActive(false);
                DestroyGride();
                for (int i = 0; i < mLegionPassDataList.Count; i++)
                {
                    GameObject go1 = NGUITools.AddChild(uiGride, LegionPassItem);
                    go1.GetComponent<LegionPassItem>().SetLegionHertRankItem(mLegionPassDataList[i]);                    
                }
                uiGride.GetComponent<UIGrid>().Reposition();
                return;
            }
            else if (go.name == "1" && hasGethurtInfo)
            {
                ListParts[0].SetActive(false);
                ListParts[1].SetActive(true);
                DestroyGride();
                for (int i = 0; i < mLegionMemberDataList.Count; i++)
                {
                    GameObject go1 = NGUITools.AddChild(uiGride, LegionMemberItem);
                    go1.GetComponent<LegionHertRankItem>().SetLegionHertItemInfo(mLegionMemberDataList[i]);                    
                }
                uiGride.GetComponent<UIGrid>().Reposition();
                return;  
            }
            SendToSeverToGetList(int.Parse(go.name));
        }
    }
    void SendToSeverToGetList(int tabIndex)
    {
        switch (tabIndex)
        {
            case 0: NetworkHandler.instance.SendProcess(string.Format("8309#{0};", LegionCopyWindowNew.curSelectGateGroupID)); break;
            case 1: NetworkHandler.instance.SendProcess("8307#;"); break;
        }
        for (int i = 0; i < ListTabs.Count; i++)
        {
            if (i == tabIndex && !ListTabs[i].value)
            {
                ListTabs[i].startsActive = true;
                ListTabs[i].value = true;
            }
        }
    }
    void DestroyGride()
    {
        for (int i = uiGride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGride.transform.GetChild(i).gameObject);
        }
    }
    
}
