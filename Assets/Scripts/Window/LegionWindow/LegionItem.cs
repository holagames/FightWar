using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum LegionDepth
{
    LegionMain = 0,//军团主界面
    LegionSecond = 1,//军团主界面
}
public class LegionItem : MonoBehaviour
{
    public List<UISprite> RankNumberSprite;
    public GameObject ApplyButton;
    public UILabel RankNumber;
    public UILabel LegionName;
    public UILabel LegionLevel;
    public UILabel CaptainName;
    public UILabel LegionPartNumber;
    public UILabel LimitLabel;
    public UILabel fightLabel;
    private LegionItemData _LegionItemData;
    public static LegionItemData _CurApplayLegionItemData;
    // Use this for initialization
    void Start()
    {

        if (UIEventListener.Get(ApplyButton).onClick == null)
        {
            UIEventListener.Get(ApplyButton).onClick += delegate(GameObject go)
            {
                if (_LegionItemData.MemberNumber < TextTranslator.instance.GetLegionByID(_LegionItemData.legionLevel).LimitNum)
                {
                    if (ApplyButton.transform.GetComponent<UISprite>().spriteName == "buttonHui")
                    {
                        UIManager.instance.OpenPromptWindow("军团拒绝任何人加入.", PromptWindow.PromptType.Hint, null, null);
                        return;
                    }
                    if (_LegionItemData.settingStateValue == 1 && _LegionItemData.settingLevelState <= CharacterRecorder.instance.level)//申请自动加入
                    {
                        CharacterRecorder.instance.isOnlyApplayToJoinIn = true;
                        NetworkHandler.instance.SendProcess(string.Format("8013#{0};{1};{2}", CharacterRecorder.instance.userId, 1, _LegionItemData.legionId));
                        //ApplyButton.GetComponent<UIButton>().isEnabled = false;
                        //ApplyButton.transform.FindChild("Label").GetComponent<UILabel>().text = "已加入";
                    }
                    else if (_LegionItemData.settingLevelState <= CharacterRecorder.instance.level)
                    {
                        _CurApplayLegionItemData = _LegionItemData;
                        NetworkHandler.instance.SendProcess(string.Format("8008#{0};", _LegionItemData.legionId));
                        // ApplyButton.GetComponent<UIButton>().isEnabled = false;
                        // ApplyButton.transform.FindChild("Label").GetComponent<UILabel>().text = "已申请";
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow(string.Format("等级不足{0}", _LegionItemData.settingLevelState), PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("军团人数达到上限", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        if (UIEventListener.Get(this.gameObject).onClick == null)
        {
            UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
            {
                Debug.Log("打开军团排行2  基本信息 军团排名 ");
            };
        }
    }

    public void SetLegionItemInfo(LegionDepth _LegionDepth, LegionItemData _LegionItemData)
    {
        this._LegionItemData = _LegionItemData;
        //RankNumber.text = _LegionItemData.rankNum.ToString();//rankNumber.ToString();
        LegionLevel.text = string.Format("Lv.{0}", _LegionItemData.legionLevel);//_LegionItemData.legionLevel.ToString();
        LegionName.text = _LegionItemData.legionName;
        CaptainName.text = _LegionItemData.legionChairmanName;
        fightLabel.text = _LegionItemData.fight.ToString();
        switch (_LegionItemData.rankNum)//_LegionItemData.legionId
        {
            case 1:
                RankNumber.gameObject.SetActive(false);
                RankNumberSprite[0].spriteName = "bingtuan_icon1";//"u32_icon4";
                RankNumberSprite[1].spriteName = "word1";
                RankNumberSprite[0].MakePixelPerfect();
                break;
            case 2:
                RankNumber.gameObject.SetActive(false);
                RankNumberSprite[0].spriteName = "bingtuan_icon2";//"u32_icon3";
                RankNumberSprite[1].spriteName = "word2";
                RankNumberSprite[0].MakePixelPerfect();
                break;
            case 3:
                RankNumber.gameObject.SetActive(false);
                RankNumberSprite[0].spriteName = "bingtuan_icon3";//"u32_icon5";
                RankNumberSprite[1].spriteName = "word3";
                RankNumberSprite[0].MakePixelPerfect();
                break;
            default:
                RankNumber.gameObject.SetActive(true);
                RankNumberSprite[0].gameObject.SetActive(false);
                RankNumber.text = _LegionItemData.rankNum.ToString();
                break;
        }
        switch (_LegionDepth)
        {
            case LegionDepth.LegionMain:
                switch (_LegionItemData.applayState)
                {
                    case 1:
                        ApplyButton.GetComponent<UIButton>().isEnabled = false;
                        ApplyButton.transform.FindChild("Label").GetComponent<UILabel>().text = "已申请";
                        break;
                }
                ApplyButton.SetActive(true);
                //LegionPartNumber.text = string.Format("{0}/{1}",_LegionItemData.MemberNumber,30 + (_LegionItemData.legionLevel - 1)* 2);//_LegionItemData.MemberNumber.ToString();//军团初始等级 1
                LegionPartNumber.text = string.Format("{0}/{1}", _LegionItemData.MemberNumber, TextTranslator.instance.GetLegionByID(_LegionItemData.legionLevel).LimitNum);
                /* switch (_LegionItemData.settingStateValue)
                 {
                     case 1: LimitLabel.text = "申请自动加入"; break;
                     case 0: LimitLabel.text = "需审核才能加入"; break;
                     case -1: LimitLabel.text = "禁止任何人加入"; break;
                 }*/
                if (_LegionItemData.settingLevelState > 0)
                {
                    LimitLabel.text = string.Format("{0}\n(等级限制:{1})", TextTranslator.instance.GetLegionLimitByState(_LegionItemData.settingStateValue), TextTranslator.instance.GetLegionLevelLimitByState(_LegionItemData.settingLevelState));
                }
                else
                {
                    LimitLabel.text = TextTranslator.instance.GetLegionLimitByState(_LegionItemData.settingStateValue);
                }
                LimitLabel.gameObject.SetActive(true);
                CaptainName.gameObject.SetActive(false);
                fightLabel.transform.parent.gameObject.SetActive(false);

                if (_LegionItemData.settingStateValue == -1)//不允许任何人加入
                {
                    ApplyButton.GetComponent<UISprite>().spriteName = "buttonHui";
                    ApplyButton.GetComponent<UIButton>().enabled = false;
                    //ApplyButton.transform.FindChild("Label").GetComponent<UILabel>().text = "不可加入";
                    ApplyButton.transform.FindChild("Label").GetComponent<UILabel>().effectColor = new Color(0.5f, 0.5f, 0.5f, 1);
                }
                break;
            case LegionDepth.LegionSecond:
                ApplyButton.SetActive(false);
                LegionPartNumber.gameObject.SetActive(false);
                LimitLabel.gameObject.SetActive(false);
                CaptainName.gameObject.SetActive(true);
                fightLabel.transform.parent.gameObject.SetActive(true);
                break;
        }


    }

    public void ScueedApplayingResult()
    {
        if (_LegionItemData.legionId == _CurApplayLegionItemData.legionId)
        {
            ApplyButton.GetComponent<UIButton>().isEnabled = false;
            ApplyButton.transform.FindChild("Label").GetComponent<UILabel>().text = "已申请";
        }
    }
}
public class LegionItemData
{
    public int legionId { get; set; }
    public string legionName { get; set; }
    public string legionChairmanName { get; set; }
    public int legionLevel { get; set; }
    public int MemberNumber { get; set; }
    public int settingStateValue { get; set; }
    public int settingLevelState { get; set; }
    public int fight { get; set; }
    public int applayState { get; set; }
    public int rankNum { get; set; }
    public LegionItemData(int legionId, string legionName, string legionChairmanName, int legionLevel, int MemberNumber)
    {
        this.legionId = legionId;
        this.legionName = legionName;
        this.legionChairmanName = legionChairmanName;
        this.legionLevel = legionLevel;
        this.MemberNumber = MemberNumber;
    }
    public LegionItemData(int legionId, string legionName, string legionChairmanName, int legionLevel, int MemberNumber, int fight, int applayState, int rankNum)
    {
        this.legionId = legionId;
        this.legionName = legionName;
        this.legionChairmanName = legionChairmanName;
        this.legionLevel = legionLevel;
        this.MemberNumber = MemberNumber;
        this.fight = fight;
        this.applayState = applayState;
        this.rankNum = rankNum;
    }
    public LegionItemData(int legionId, string legionName, string legionChairmanName, int legionLevel, int MemberNumber, int settingStateValue, int settingLevelState, int fight, int applayState, int rankNum)
    {
        this.legionId = legionId;
        this.legionName = legionName;
        this.legionChairmanName = legionChairmanName;
        this.legionLevel = legionLevel;
        this.MemberNumber = MemberNumber;
        this.settingStateValue = settingStateValue;
        this.settingLevelState = settingLevelState;
        this.fight = fight;
        this.applayState = applayState;
        this.rankNum = rankNum;
    }
}
public class LegionMemberData
{
    public int uId { get; set; }
    public int iconHead = 60011;//没有数据
    public string name { get; set; }
    public int level { get; set; }
    public int vip { get; set; }
    public int fight { get; set; }
    public int contribute { get; set; }
    public int officialPosition { get; set; }
    public int lastLoginTime { get; set; }
    /// <summary>
    /// 今日贡献
    /// </summary>
    public int todayContribution { get; set; }
    public int sumHert { get; set; }//总伤害
    public int rankNum { get; set; }//总伤害排名
    public LegionMemberData(int uId, string name, int level, int vip, int fight, int contribute, int officialPosition, int lastLoginTime, int iconHead, int _todayContribution)
    {
        this.uId = uId;
        this.name = name;
        this.level = level;
        this.vip = vip;
        this.fight = fight;
        this.contribute = contribute;
        this.officialPosition = officialPosition;

        this.todayContribution = _todayContribution;

        this.lastLoginTime = lastLoginTime;
        this.iconHead = iconHead;
    }
    public LegionMemberData(int uId, string name, int level, int vip, int fight, int lastLoginTime, int iconHead)
    {
        this.uId = uId;
        this.name = name;
        this.level = level;
        this.vip = vip;
        this.fight = fight;
        this.contribute = 0;
        this.officialPosition = 0;
        this.lastLoginTime = lastLoginTime;
        this.iconHead = iconHead;
    }
    //副本伤害排名
    public LegionMemberData(int rankNum, int uId, string name, int level, int vip, int fight, int contribute, int officialPosition, int sumHert, int iconHead)
    {
        this.uId = uId;
        this.name = name;
        this.level = level;
        this.vip = vip;
        this.fight = fight;
        this.contribute = contribute;
        this.officialPosition = officialPosition;

        this.sumHert = sumHert;
        this.rankNum = rankNum;
        this.iconHead = iconHead;
    }
}
