using UnityEngine;
using System.Collections;

public class StrengthenMasterPart : MonoBehaviour {

    public UILabel level;

    public UILabel currentPower;
    public UILabel currentHP;
    public UILabel currentAtt;
    public UILabel currentDef;

    public UILabel nextPower;
    public UILabel nextHP;
    public UILabel nextAtt;
    public UILabel nextDef;

    public GameObject mask;

    public void SetStrengthenMasterInfo(int _type,int _level,int _LastPower,int _nowPower)
    {
        switch (_type)
        {
            case 1:
                level.text = string.Format("装备强化等级突破至[-][00FF00]{0}[-]级", _level.ToString());
                break;

            case 2:
                level.text = string.Format("装备精炼等级提升至[-][00FF00]{0}[-]级", _level.ToString());
                break;
            case 3:
                level.text = string.Format("饰品强化等级提升至[-][00FF00]{0}[-]级", _level.ToString());
                break;

            case 4:
                level.text = string.Format("饰品精炼等级提升至[-][00FF00]{0}[-]级", _level.ToString());
                break;

            default:
                break;
        }

    
        StrengthenMaster mMater = TextTranslator.instance.GetStrengthenMasterByTypeAndLevel(_type, _level);
        StrengthenMaster mLastMaster = TextTranslator.instance.GetStrengthenMasterByTypeAndLevel(_type, _level-1);

        if (mLastMaster != null)
        {
            currentHP.text = mLastMaster.hp.ToString();
            currentAtt.text = mLastMaster.attack.ToString();
            currentDef.text = mLastMaster.defense.ToString();
            currentPower.text = _LastPower.ToString();
        }
        else
        {
            currentHP.text = "0";
            currentAtt.text = "0";
            currentDef.text = "0";
            currentPower.text = _LastPower.ToString();
        }

        nextAtt.text = mMater.attack.ToString();
        nextDef.text = mMater.defense.ToString();
        nextHP.text = mMater.hp.ToString();
        nextPower.text = _nowPower.ToString();

    }

    void Start()
    {
        if (UIEventListener.Get(mask).onClick == null)
        {
            UIEventListener.Get(mask).onClick += delegate(GameObject go)
            {
                gameObject.SetActive(false);
            };
        }
    }
}
