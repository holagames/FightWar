using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LegionHertRankItem: MonoBehaviour 
{
    public List<UISprite> RankNumberSprite;
    public GameObject ApplyButton;
    public UILabel RankNumber;
    public UILabel LegionName;
    public UILabel LegionLevel;
    public UILabel CaptainName;
    public UILabel LegionPartNumber;
    public UILabel LimitLabel;
    public UILabel sumHertLabel;
    public UILabel awardItemCountLabel;
    private LegionMemberData _LegionItemData;
	// Use this for initialization
	void Start () 
    {
        if (UIEventListener.Get(this.gameObject).onClick == null)
        {
            UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
            {
                Debug.Log("打开军团排行2  基本信息 军团排名 ");
            };
        }
	}
    public void SetLegionHertItemInfo(LegionMemberData _LegionItemData)
    {
        this._LegionItemData = _LegionItemData;
        //RankNumber.text = _LegionItemData.legionId.ToString();//rankNumber.ToString();
        LegionLevel.text = string.Format("Lv.{0}", _LegionItemData.level);//_LegionItemData.legionLevel.ToString();
        LegionName.text = _LegionItemData.name;
        sumHertLabel.text = _LegionItemData.sumHert.ToString();
       // CaptainName.text = _LegionItemData.legionChairmanName;
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
        awardItemCountLabel.text = TextTranslator.instance.GetLegionRankByRankNum(_LegionItemData.rankNum).AwardItemList[0].itemCount.ToString();
    }
    public void SetLegionHertItemInfo(LegionDepth _LegionDepth,LegionItemData _LegionItemData)
    {
        //this._LegionItemData = _LegionItemData;
        LegionLevel.text = string.Format("Lv.{0}", _LegionItemData.legionLevel);//_LegionItemData.legionLevel.ToString();
        LegionName.text = _LegionItemData.legionName;
        CaptainName.text = _LegionItemData.legionChairmanName;    
        switch (_LegionItemData.legionId)
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
                RankNumber.text = _LegionItemData.legionId.ToString();
                break;
        }
        switch (_LegionDepth)
        {
            case LegionDepth.LegionMain:
                ApplyButton.SetActive(true);
                //LegionPartNumber.text = string.Format("{0}/{1}",_LegionItemData.MemberNumber,30 + (_LegionItemData.legionLevel - 1)* 2);//_LegionItemData.MemberNumber.ToString();//军团初始等级 1
                LegionPartNumber.text = string.Format("{0}/{1}", _LegionItemData.MemberNumber, TextTranslator.instance.GetLegionByID(_LegionItemData.legionLevel).LimitNum);
               /* switch (_LegionItemData.settingStateValue)
                {
                    case 1: LimitLabel.text = "申请自动加入"; break;
                    case 0: LimitLabel.text = "需审核才能加入"; break;
                    case -1: LimitLabel.text = "禁止任何人加入"; break;
                }*/
                LimitLabel.text = string.Format("{0}\n(等级限制:{1})", TextTranslator.instance.GetLegionLimitByState(_LegionItemData.settingStateValue), TextTranslator.instance.GetLegionLevelLimitByState(_LegionItemData.settingLevelState));
                LimitLabel.gameObject.SetActive(true);
                CaptainName.gameObject.SetActive(false);
                break;
            case LegionDepth.LegionSecond:
                ApplyButton.SetActive(false);
                LegionPartNumber.gameObject.SetActive(false);
                LimitLabel.gameObject.SetActive(false);
                CaptainName.gameObject.SetActive(true);
                break;
        }

    }

}
