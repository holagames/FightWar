using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class LegionPassData
{
    public int rankNum { get; set; }
    public int legionId { get; set; }
    public string legionName { get; set; }
    public int legionFlag { get; set; }
    public int time { get; set; }
    public LegionPassData(int rankNum,int legionId , string legionName, int legionFlag, int timer)
    {
        this.rankNum = rankNum;
        this.legionId = legionId;
        this.legionName = legionName;
        this.legionFlag = legionFlag;
        this.time = timer;
        
    }
}
public class LegionPassItem: MonoBehaviour 
{
    public UISprite flagSprite;
    public List<UISprite> RankNumberSprite;
    public UILabel RankNumber;
    public UILabel LegionName;
    public UILabel LegionLevel;
    public UILabel sumHertLabel;    
    private LegionMemberData _LegionItemData;
    // Use this for initialization
    void Start()
    {
        if (UIEventListener.Get(this.gameObject).onClick == null)
        {
            UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
            {
                //Debug.Log("打开军团排行2  基本信息 军团排名 ");
            };
        }
    }
    public void SetLegionHertRankItem(LegionMemberData _LegionItemData)
    {
        this._LegionItemData = _LegionItemData;
        LegionLevel.text = string.Format("Lv.{0}", _LegionItemData.level);
        LegionName.text = _LegionItemData.name;
        sumHertLabel.text = _LegionItemData.sumHert.ToString();
        switch (_LegionItemData.rankNum)
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
                RankNumber.text = _LegionItemData.rankNum.ToString();
                break;
        }
    }
    public void SetLegionHertRankItem(LegionPassData _LegionPassData)
    {
        flagSprite.spriteName = "legionFlag" + _LegionPassData.legionFlag;
        LegionName.text = _LegionPassData.legionName;
        DateTime mTime = Utils.GetTime(_LegionPassData.time.ToString());
        sumHertLabel.text = mTime.ToString("yyyy-MM-dd HH:mm");
        switch (_LegionPassData.rankNum)
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
                RankNumber.text = _LegionPassData.rankNum.ToString();
                break;
        }
    }
}
