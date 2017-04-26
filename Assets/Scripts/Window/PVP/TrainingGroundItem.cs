using UnityEngine;
using System.Collections;
public class LegionTrain
{
    public int state;
    public int TrainID { get; set; }
    public int characteroleId;
    public Hero mHero;

    public int NeedLevel { get; set; }
    public int VipUnLock { get; set; }
    public int DiamondCost { get; set; }
    public int BonusExp { get; set; }
    public LegionTrain(int TrainID, int NeedLevel, int VipUnLock, int DiamondCost, int BonusExp)
    {
        this.TrainID = TrainID;
        this.NeedLevel = NeedLevel;
        this.VipUnLock = VipUnLock;
        this.DiamondCost = DiamondCost;
        this.BonusExp = BonusExp;
    }
    public void SetLegionTrainSeverData(int _state, int characteroleId, int exp)
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
                mHero.exp = exp;
                break;
            }
        }
    }
    public void SetLegionTrainSeverData(int _state, int characteroleId, int exp, int level)
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
    }
}
public class TrainingGroundItem : MonoBehaviour
{
    [SerializeField]
    private UISprite heroIcon;
    [SerializeField]
    private UILabel labelName;
    [SerializeField]
    private UILabel labelLevelNumber;
    [SerializeField]
    private UILabel openCostLabel;
    [SerializeField]
    private UILabel needLevelLabel;
    [SerializeField]
    private UILabel EXPLabel;
    public UISlider EXPselider;
    [SerializeField]
    private GameObject buttonOpen;

    public GameObject lockObj;
    public GameObject noneHeroObj;
    public GameObject haveHeroObj;

    public static int curClickTrainingGroundItemId = 1;

    public LegionTrain mTrainingGroundItemData;
    void Start()
    {
        if (UIEventListener.Get(buttonOpen).onClick == null)
        {
            UIEventListener.Get(buttonOpen).onClick += delegate(GameObject go)
            {
                if (mTrainingGroundItemData.VipUnLock != 99 && CharacterRecorder.instance.Vip < mTrainingGroundItemData.VipUnLock)
                {
                    UIManager.instance.OpenPromptWindow(string.Format("VIP{0}可开通", mTrainingGroundItemData.VipUnLock), PromptWindow.PromptType.Hint, null, null);
                    return;
                }

                if (CharacterRecorder.instance.level >= mTrainingGroundItemData.NeedLevel)
                {
                    if (CharacterRecorder.instance.lunaGem >= mTrainingGroundItemData.DiamondCost)
                    {
                        NetworkHandler.instance.SendProcess(string.Format("8020#{0};", mTrainingGroundItemData.TrainID));//int.Parse(this.gameObject.name) + 1)
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("钻石不足", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else if (mTrainingGroundItemData.VipUnLock == 99)
                {
                    UIManager.instance.OpenPromptWindow(string.Format("等级不足{0}级", mTrainingGroundItemData.NeedLevel), PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow(string.Format("VIP{0}可开通", mTrainingGroundItemData.VipUnLock), PromptWindow.PromptType.Hint, null, null);
                }
            };
        }

        UIEventListener.Get(this.gameObject).onClick = ClickThisItem;
    }

    public void SetRedPoint(LegionTrain train)
    {
        bool leftTimes = false;
        if (train.DiamondCost <= CharacterRecorder.instance.lunaGem)
        {
            if (train.VipUnLock == 99 && train.NeedLevel <= CharacterRecorder.instance.level)
            {
                leftTimes = true;
            }
            else if (train.VipUnLock <= CharacterRecorder.instance.Vip)
            {
                leftTimes = true;
            }
        }
        lockObj.transform.FindChild("RedPoint").gameObject.SetActive(leftTimes);
    }

    public void SetTrainingGroundItemInfo(LegionTrain _oneData)
    {
        mTrainingGroundItemData = _oneData;
        switch (_oneData.state)
        {
            case 0:
                lockObj.SetActive(true);
                noneHeroObj.SetActive(false);
                haveHeroObj.SetActive(false);
                openCostLabel.text = _oneData.DiamondCost.ToString();
                SetRedPoint(_oneData);
                //needLevelLabel.text = string.Format("{0}级可解锁", _oneData.NeedLevel); 
                if (_oneData.VipUnLock == 99)
                {
                    needLevelLabel.text = string.Format("{0}级可解锁", _oneData.NeedLevel);
                }
                else
                {
                    needLevelLabel.text = string.Format("V{0}可解锁", _oneData.VipUnLock);
                }
                break;
            case 1:
                lockObj.SetActive(false);
                noneHeroObj.SetActive(true);
                haveHeroObj.SetActive(false);
                noneHeroObj.transform.FindChild("RedPoint").gameObject.SetActive(true);
                break;
            default:
                lockObj.SetActive(false);
                noneHeroObj.SetActive(false);
                haveHeroObj.SetActive(true);
                break;
        }
        if (_oneData.mHero != null)
        {
            //labelName.text = _oneData.mHero.name;
            TextTranslator.instance.SetHeroNameColor(labelName, _oneData.mHero.name, _oneData.mHero.classNumber);
            labelLevelNumber.text = _oneData.mHero.level.ToString();
            //达到最高等级 且 达到最大经验时 经验 减 1
            _oneData.mHero.exp = (_oneData.mHero.level == CharacterRecorder.instance.level && _oneData.mHero.exp == _oneData.mHero.maxExp) ? _oneData.mHero.exp - 1 : _oneData.mHero.exp;
            EXPselider.GetComponent<UISlider>().value = (float)_oneData.mHero.exp / (float)_oneData.mHero.maxExp;
            EXPLabel.text = string.Format("{0}/{1}", _oneData.mHero.exp, _oneData.mHero.maxExp);
            heroIcon.spriteName = _oneData.mHero.cardID.ToString();

            //在协议里面处理
            /*  if (!LegionTrainingGroundWindow.mOnLineTrainingHeroList.Contains(_oneData.mHero))
              {
                  LegionTrainingGroundWindow.mOnLineTrainingHeroList.Add(_oneData.mHero);
              }*/
        }
    }
    public void ResetTrainingGroundItemInfo(LegionTrain _TrainingGroundItemData)
    {
        SetTrainingGroundItemInfo(_TrainingGroundItemData);
    }
    private void ClickThisItem(GameObject go)
    {
        if (go != null)
        {
            if (mTrainingGroundItemData.state != 0)
            {
                curClickTrainingGroundItemId = mTrainingGroundItemData.TrainID;
                UIManager.instance.OpenPanel("LegionTrainingSelectWindow", false);
                /* switch(mTrainingGroundItemData.state)
                 {
                     case 2:
                         if (LegionTrainingSelectItem.curSelectRoleId != 0 && LegionTrainingSelectItem.curSelectRoleId != mTrainingGroundItemData.mHero.characterRoleID)
                         {
                             LegionTrainingGroundWindow.mOnLineTrainingHeroList.Remove(mTrainingGroundItemData.mHero); 
                         }
                         break;
                     default: break;
                 }*/
            }
        }
    }
}
