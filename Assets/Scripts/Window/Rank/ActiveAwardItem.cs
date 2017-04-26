using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActiveAwardItem: MonoBehaviour
{
    public UILabel RankNumber;
    public UILabel Level;
    public UILabel Name;
    public UILabel FightNumber;
    public List<UISprite> RankNumberSprite;
    [SerializeField]
    private GameObject HadArriveObj;
    [SerializeField]
    private GameObject MyGrid;
    [SerializeField]
    private GameObject LoginAwardItem;
	// Use this for initialization
	void Start () 
    {
        
        UIEventListener.Get(this.gameObject).onClick = ClickThisItem;
	}

    public void SetInfo(int tabIndex, ActivitySevenHero _ActiveAwardItemData)
    {
        HadArriveObj.SetActive(false);
        switch (tabIndex)
        {
            case 0:
                SetActiveAwardItemInfo(tabIndex, _ActiveAwardItemData.RankID, _ActiveAwardItemData.mAwardList);
                break;
        }
        
    }
    public void SetInfo(int tabIndex, ActivitySevenRank _ActiveAwardItemData)
    {
        HadArriveObj.SetActive(false);
        switch (tabIndex)
        {
            case 1:
                SetActiveAwardItemInfo(tabIndex, _ActiveAwardItemData.Force, _ActiveAwardItemData.mAwardList);
                break;
        }
    }
    public void SetInfo(int tabIndex,ActiveAwardItemData _ActiveAwardItemData)
    {
        HadArriveObj.SetActive(false);
        switch (tabIndex)
        {
            case 0:
                SetActiveAwardItemInfo(tabIndex, _ActiveAwardItemData.rankNumber, _ActiveAwardItemData.AwardItemList);
                break;
            case 1:
                SetActiveAwardItemInfo(tabIndex, _ActiveAwardItemData.fight, _ActiveAwardItemData.AwardItemList);
                break;
            case 2:
                SetActiveAwardItemInfo(tabIndex, _ActiveAwardItemData.rankNumber, _ActiveAwardItemData.name, _ActiveAwardItemData.level, _ActiveAwardItemData.fight);
                break;
        }
        
    }

    void SetActiveAwardItemInfo(int ActiveAwardTabType, int rankNumberOrFight, BetterList<AwardItem> AwardItemList)
    {
        switch (ActiveAwardTabType)
        {
            case 0:
                switch (rankNumberOrFight)
                {
                    case 1:
                        RankNumber.gameObject.SetActive(false);
                        RankNumberSprite[0].spriteName = "u32_icon4";
                        RankNumberSprite[1].spriteName = "word1";
                        break;
                    case 2:
                        RankNumber.gameObject.SetActive(false);
                        RankNumberSprite[0].spriteName = "u32_icon3";
                        RankNumberSprite[1].spriteName = "word2";
                        break;
                    case 3:
                        RankNumber.gameObject.SetActive(false);
                        RankNumberSprite[0].spriteName = "u32_icon5";
                        RankNumberSprite[1].spriteName = "word3";
                        break;
                    default:
                        RankNumber.gameObject.SetActive(true);
                        RankNumberSprite[0].gameObject.SetActive(false);
                        RankNumber.text = rankNumberOrFight.ToString();
                        break;
                }
                FightNumber.transform.parent.gameObject.SetActive(false);
                break;
            case 1:
                RankNumber.gameObject.SetActive(false);
                RankNumberSprite[0].gameObject.SetActive(false);
                FightNumber.text = rankNumberOrFight.ToString();
                if (CharacterRecorder.instance.Fight >= rankNumberOrFight)
                {
                    HadArriveObj.SetActive(true);
                }
                else 
                {
                    HadArriveObj.SetActive(false);
                }
                break;
        }
        Level.gameObject.SetActive(false);
        Name.gameObject.SetActive(false);
        for (int i = 0; i < AwardItemList.size; i++)
        {
            if (AwardItemList[i].itemCode != 0)
            {
                GameObject go = NGUITools.AddChild(MyGrid, LoginAwardItem);
                go.name = AwardItemList[i].itemCode.ToString();
                go.GetComponent<LoginAwardItem>().SetLoginAwardItem(AwardItemList[i]);
                TextTranslator.instance.ItemDescription(go, AwardItemList[i].itemCode, 0);
            }
        }
        MyGrid.GetComponent<UIGrid>().Reposition();
    }

    void SetActiveAwardItemInfo(int tabIndex, int rankNumber, string name, int level, int fight)
    {
        Level.text = string.Format("{0}", level);
        Name.text = name;
        FightNumber.text = fight.ToString();
        switch (rankNumber)
        {
            case 1:
                RankNumber.gameObject.SetActive(false);
                RankNumberSprite[0].spriteName = "u32_icon4";
                RankNumberSprite[1].spriteName = "word1";
                break;
            case 2:
                RankNumber.gameObject.SetActive(false);
                RankNumberSprite[0].spriteName = "u32_icon3";
                RankNumberSprite[1].spriteName = "word2";
                break;
            case 3:
                RankNumber.gameObject.SetActive(false);
                RankNumberSprite[0].spriteName = "u32_icon5";
                RankNumberSprite[1].spriteName = "word3";
                break;
            default:
                RankNumber.gameObject.SetActive(true);
                RankNumberSprite[0].gameObject.SetActive(false);
                RankNumber.text = rankNumber.ToString();
                break;
        }
    }

    private void ClickThisItem(GameObject go)
    {
        if(go != null)
        {
            //UIManager.instance.OpenPanel("LookInfoWindow", false);
            //NetworkHandler.instance.SendProcess(string.Format("6008#{0};{1};", userId, rankType));
        }
    }

}
public class ActiveAwardItemData
{
    //public int ActiveAwardTabType { get; set; }
    public int rankNumber { get; private set; }
    public int fight { get; private set; }
    public int level { get; private set; }
    public string name { get; private set; }
    public BetterList<AwardItem> AwardItemList = new BetterList<AwardItem>();
    public ActiveAwardItemData(int rankNumber, BetterList<AwardItem> AwardItemList)//int ActiveAwardTabType,
    {
        //this.ActiveAwardTabType = ActiveAwardTabType;
        this.rankNumber = rankNumber;
        this.AwardItemList = AwardItemList;
    }
    public ActiveAwardItemData(BetterList<AwardItem> AwardItemList, int fight)//int TabIndex,
    {
        //this.ActiveAwardTabType = TabIndex;
        this.fight = fight;
        this.AwardItemList = AwardItemList;
    }
    public ActiveAwardItemData(int rankNumber, string name, int level, int fight)//int ActiveAwardTabType, 
    {
        //this.ActiveAwardTabType = ActiveAwardTabType;
        this.rankNumber = rankNumber;
        this.name = name;
        this.level = level;
        this.fight = fight;
    }

}
