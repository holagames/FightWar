using UnityEngine;
using System.Collections;
using System;
public class ReformLabItemData
{
    public int state;
    public int score;
    public int characteroleId;
    public Hero mHero;

    public int RoleType { get; set; }
    public int LabItemPosNum;
    public int NeedLevel { get; set; }
    public int VipUnLock { get; set; }
    public int DiamondCost { get; set; }
    public ReformLabItemData(int RoleType,int LabItemPosNum, int NeedLevel, int VipUnLock, int DiamondCost)
    {
        this.RoleType = RoleType;
        this.LabItemPosNum = LabItemPosNum;
        this.NeedLevel = NeedLevel;
        this.VipUnLock = VipUnLock;
        this.DiamondCost = DiamondCost;
    }
    public void SetReformLabItemData(int characteroleId, int icon, int score, float  addPercent)
    {
        this.characteroleId = characteroleId;
        if (characteroleId == 0)
        {
            return;
        }
        foreach (var h in CharacterRecorder.instance.ownedHeroList)
        {
            if (h.characterRoleID == characteroleId)
            {
                mHero = h;
                mHero.score = score;
                mHero.addPercent = addPercent;
                break;
            }
        }
    }
    public void SetReformLabItemData(int characteroleId,int score, int addPercent)
    {
        this.characteroleId = characteroleId;
        if (characteroleId == 0)
        {
            return;
        }
        foreach (var h in CharacterRecorder.instance.ownedHeroList)
        {
            if (h.characterRoleID == characteroleId)
            {
                mHero = h;
                mHero.score = score;
                mHero.addPercent = addPercent;
                break;
            }
        }
    }
   /* public void SetReformLabItemData(int _state, int characteroleId, int exp, int level)
    {
        this.state = _state;
        this.characteroleId = characteroleId;
        if (characteroleId == 0)
        {
            return;
        }
        foreach (var h in CharacterRecorder.instance.ownedHeroList)
        {
            if (h.characterRoleID == characteroleId)
            {
                mHero = h;
                if (mHero.level != level)
                {
                    NetworkHandler.instance.SendProcess(string.Format("1005#{0};", characteroleId));
                }
                mHero.exp = exp;
                mHero.level = level;
                break;
            }
        }
    }*/
}
public class ReformLabItem: MonoBehaviour 
{
    //[SerializeField]
    //private UISprite needLevelSprite;
    [SerializeField]
    private UISprite heroIcon;
    [SerializeField]
    private UILabel labelName;
    [SerializeField]
    private UILabel labelLevelNumber;
    public UILabel scoreLabel;
    public UILabel addPercentLabel;
    [SerializeField]
    private UILabel openCostLabel;
    [SerializeField]
    private UILabel needLevelLabel;
    [SerializeField]
    private UILabel EXPLabel;
    public UISlider EXPselider;
    [SerializeField]
    private GameObject buttonOpen;
    [SerializeField]
    private GameObject buttonRemove;

    public GameObject lockObj;
    public GameObject noneHeroObj;
    public GameObject haveHeroObj;

    public static int curClickTrainingGroundItemId = 1;

    public ReformLabItemData mItemData;
	void Start () 
    {
        if (UIEventListener.Get(buttonOpen).onClick == null)
        {
            UIEventListener.Get(buttonOpen).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.level >= mItemData.NeedLevel || CharacterRecorder.instance.Vip >= mItemData.VipUnLock)
                {
                    NetworkHandler.instance.SendProcess(string.Format("1804#{0};{1};", mItemData.RoleType, mItemData.LabItemPosNum));
                }
                else if (mItemData.VipUnLock == 99)
                {
                    UIManager.instance.OpenPromptWindow(string.Format("等级不足{0}级", mItemData.NeedLevel), PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow(string.Format("VIP{0}可开通", mItemData.VipUnLock), PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        if (UIEventListener.Get(buttonRemove).onClick == null)
        {
            UIEventListener.Get(buttonRemove).onClick += delegate(GameObject go)
            {
                if (mItemData.state == 2)
                {
                    NetworkHandler.instance.SendProcess(string.Format("1803#{0};{1};", mItemData.RoleType, mItemData.LabItemPosNum));
                }
            };
        }
        
        UIEventListener.Get(this.gameObject).onClick = ClickThisItem;
	}


    public void SetReformLabItemInfo(ReformLabItemData _oneData)
    {
        mItemData = _oneData;
        switch(_oneData.state)
        {
            case 0: 
                lockObj.SetActive(true);
                noneHeroObj.SetActive(false);
                haveHeroObj.SetActive(false);
                openCostLabel.text = _oneData.DiamondCost.ToString();
                if (_oneData.VipUnLock == 99)
                {
                    needLevelLabel.text = string.Format("{0}级可解锁", _oneData.NeedLevel);
                    //needLevelSprite.height = 38;
                }
                else
                {
                    needLevelLabel.text = string.Format("{0}级可解锁 或V{1}可解锁",_oneData.NeedLevel, _oneData.VipUnLock);
                    //needLevelSprite.height = 55;
                }
                break;
            case 1:
                lockObj.SetActive(false);
                noneHeroObj.SetActive(true);
                haveHeroObj.SetActive(false);
                break;
            default:
                lockObj.SetActive(false);
                noneHeroObj.SetActive(false);
                haveHeroObj.SetActive(true);
                break;
        }
        if (_oneData.mHero != null && _oneData.state == 2)
        {
            //labelName.text = _oneData.mHero.name;
            TextTranslator.instance.SetHeroNameColor(labelName, _oneData.mHero.name, _oneData.mHero.classNumber);
            labelLevelNumber.text = _oneData.mHero.level.ToString();
            scoreLabel.text = "评分:" + _oneData.mHero.score.ToString();
            addPercentLabel.text = string.Format("加成:{0}%", Math.Round(_oneData.mHero.addPercent, 1));//_oneData.mHero.addPercent);

          /*  EXPselider.GetComponent<UISlider>().value = (float)_oneData.mHero.exp / (float)_oneData.mHero.maxExp;
            EXPLabel.text = string.Format("{0}/{1}", _oneData.mHero.exp, _oneData.mHero.maxExp);*/
            heroIcon.spriteName = _oneData.mHero.cardID.ToString();

            if (!LabWindow.mOnLineTrainingHeroList.Contains(_oneData.mHero))
            {
                LabWindow.mOnLineTrainingHeroList.Add(_oneData.mHero);
            }
        }
    }
    public void ResetLabItemInfo(ReformLabItemData _TrainingGroundItemData)
    {
        SetReformLabItemInfo(_TrainingGroundItemData);
    }
    private void ClickThisItem(GameObject go)
    {
        if (go != null)
        {
            if (mItemData.state != 0)// && mItemData.state != 2)
            {
                curClickTrainingGroundItemId = mItemData.LabItemPosNum;
                UIManager.instance.OpenSinglePanel("LabSelectWindow", false);

            }
        }
    }
}
