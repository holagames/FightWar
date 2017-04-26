using UnityEngine;
using System.Collections;

public class SelectRoleItem : MonoBehaviour {
    public GameObject LabelName;
    public GameObject SpriteAvatar;

    public UISprite HeroType;
    public UISprite HeroRarity;
    public UISprite HeroGrade;
    public GameObject HeroHaved;

    public UILabel PowerLabel;

    int RoleID;
    int characterRoleId;
    int TeamID;
    int Index;
    bool isOwn;
    // Use this for initialization
    void Start()
    {
        if (UIEventListener.Get(transform.FindChild("GetBtn").gameObject).onClick == null)
        {
            UIEventListener.Get(transform.FindChild("GetBtn").gameObject).onClick = delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("6104#" + TeamID + ";" + Index + ";" + characterRoleId + ";");
                if (GameObject.Find("SelectRoleWindow") != null) 
                {
                    Destroy(GameObject.Find("SelectRoleWindow"));
                }
                //UIManager.instance.BackUI();
                //UIManager.instance.OpenSinglePanel("WayWindow", false);
                //GameObject.Find("WayWindow").GetComponent<WayWindow>().SetWayInfo(RoleID + 10000);
            };
        }
    }

    public void Init(int _RoleID, string _Name,int teamid,int index)
    {
        RoleID = _RoleID;
        TeamID = teamid;
        Index = index;
        //isOwn = _isOwn;

        LabelName.GetComponent<UILabel>().text = _Name;

        HeroInfo mheroInfo = TextTranslator.instance.GetHeroInfoByHeroID(_RoleID);
        SetHeroType(mheroInfo.heroCarrerType);
        HeroRarity.spriteName = "word" + (mheroInfo.heroRarity + 3);

        Hero myHero = CharacterRecorder.instance.GetHeroByRoleID(_RoleID);
        characterRoleId = myHero.characterRoleID;

        SpriteAvatar.GetComponent<UISprite>().spriteName = RoleID.ToString();
        TextTranslator.instance.SetHeroNameColor(HeroGrade, myHero.classNumber);
        PowerLabel.text = myHero.force.ToString();


        //if (_isOwn)
        //{
        //    Hero myHero = CharacterRecorder.instance.GetHeroByRoleID(_RoleID);
        //    SpriteAvatar.GetComponent<UISprite>().spriteName = RoleID.ToString();
        //    TextTranslator.instance.SetHeroNameColor(HeroGrade, myHero.classNumber);
        //    PowerLabel.text = myHero.force.ToString();
        //}
        //else
        //{
        //    SpriteAvatar.GetComponent<UISprite>().spriteName = RoleID.ToString() + "1";
        //    LabelName.GetComponent<UILabel>().color = Color.white;
        //    HeroGrade.spriteName = "yxdi0";
        //    HeroHaved.SetActive(false);
        //}
    }
    //设置英雄职业
    void SetHeroType(int _herotype)
    {
        if (_herotype == 1 || _herotype == 2 || _herotype == 3)
        {
            HeroType.spriteName = string.Format("heroType{0}", _herotype);
        }
        else
        {
            Debug.LogError("英雄职业类型参数错误" + _herotype);
        }
    }

    void SetHeroGrade(int _ClassNumber)
    {
        switch (_ClassNumber)
        {
            case 1:
                HeroGrade.spriteName = "yxdi0";
                break;
            case 2:
                HeroGrade.spriteName = "yxdi1";
                break;
            case 3:
                HeroGrade.spriteName = "yxdi1";
                break;
            case 4:
                HeroGrade.spriteName = "yxdi2";
                break;
            case 5:
                HeroGrade.spriteName = "yxdi2";
                break;
            case 6:
                HeroGrade.spriteName = "yxdi2";
                break;
            case 7:
                HeroGrade.spriteName = "yxdi3";
                break;
            case 8:
                HeroGrade.spriteName = "yxdi3";
                break;
            case 9:
                HeroGrade.spriteName = "yxdi3";
                break;
            case 10:
                HeroGrade.spriteName = "yxdi3";
                break;
            case 11:
                HeroGrade.spriteName = "yxdi4";
                break;
            case 12:
                HeroGrade.spriteName = "yxdi4";
                break;
            default:
                break;
        }
    }
	
}
