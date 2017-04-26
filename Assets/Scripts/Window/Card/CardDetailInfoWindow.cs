using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardDetailInfoWindow : MonoBehaviour
{


    public GameObject BackButton;
    public GameObject HeroProperty;
    public UILabel HeroCarrer;
    public UILabel HeroLocation;
    public GameObject HeroSkill;
    public GameObject HeroKarma;
    public GameObject HeroIntroduce;
    public GameObject KarmaItem;
    public GameObject SkillInfoBoard;
    public GameObject Skill2InfoBoard;
    public List<UILabel> ListFateNameLabel = new List<UILabel>();


    public UISprite SpriteRare;
    public UITexture HeroSprite;
    public UISprite SpriteCareer;
    public UITexture BGSprite;
    public UITexture FrameSprite;

    int FateYMoveDistance = 25;

    int RoleID;
    float moveY = 0;
    int LoadingSize = 13;
    void Start()
    {
        SkillInfoBoard.SetActive(false);
        Skill2InfoBoard.SetActive(false);
        if (UIEventListener.Get(BackButton).onClick == null)
        {
            UIEventListener.Get(BackButton).onClick = delegate(GameObject go)
            {
                DestroyImmediate(this.gameObject);

                if (GameObject.Find("UIRoot").transform.Find("ActivityGachaRedHeroWindow") != null)
                {
                    GameObject.Find("UIRoot").transform.Find("ActivityGachaRedHeroWindow").gameObject.SetActive(true);
                }
            };
        }

        UIEventListener.Get(HeroSkill.transform.Find("SkillOne").gameObject).onClick += delegate(GameObject go)
        {
            SkillInfoBoard.SetActive(true);
            SkillInfoBoard.GetComponent<SkillInfoBoard>().SetSkillInfoBoard(TextTranslator.instance.GetHeroInfoByHeroID(RoleID), CharacterRecorder.instance.GetHeroByRoleID(RoleID));
        };

        UIEventListener.Get(HeroSkill.transform.Find("SkillTwo").gameObject).onClick += delegate(GameObject go)
        {
            Skill2InfoBoard.SetActive(true);
            Skill2InfoBoard.GetComponent<Skill2InfoBoard>().SetSkillInfoBoard(TextTranslator.instance.GetHeroInfoByHeroID(RoleID));
        };

        UIEventListener.Get(FrameSprite.gameObject).onClick = delegate(GameObject go)
        {
            if (CharacterRecorder.instance.CompositeSuipian == 0)
            {//从图鉴进入角色信息
                CharacterRecorder.instance.CompositeSuipian = 1;
            }
            else
            {
                CharacterRecorder.instance.CompositeSuipian = -1;
            }
            UIManager.instance.OpenSinglePanel("CardIntroduceWindow", false);
            GameObject obj = GameObject.Find("CardIntroduceWindow");
            if (obj != null)
            {
                CardIntroduceWindow _card = obj.GetComponent<CardIntroduceWindow>();
                _card.SetIntroduceInfo(RoleID);
                int num = 0;
                foreach (Hero item in CharacterRecorder.instance.ownedHeroList)
                {
                    //Debug.LogError(item.cardID + "    " + RoleID);
                    if (item.cardID == RoleID)
                    {
                        num++;
                    }
                }
                //if (num == 0)
                //{
                //    _card.HideBottomInfo();
                //}
                // _card.HideBottomInfo();
            }
        };
    }

    public void SetDetaiInfo(int HeroId)
    {
        HeroInfo _heroInfo = TextTranslator.instance.GetHeroInfoByHeroID(HeroId);
        CombinSkill HeroInfo = TextTranslator.instance.GetCombinSkillByID(HeroId);
        SetCardInfo(HeroId);
        SetProperty(HeroId);
        if (HeroInfo != null)
        {
            SetCarrer(HeroInfo.SkillName);
        }
        else
        {
            SetCarrer("无");
        }

        SetLocation(_heroInfo.heroDescription);

        SetHeroSkill(_heroInfo.heroSkillList);

        SetHerokarma();
        SetHeroIntroduce(_heroInfo.heroPetPhrase);
        Debug.Log("英雄介绍：" + _heroInfo.heroDescription);
    }

    void SetCardInfo(int _RoleID)
    {
        RoleID = _RoleID;
        HeroInfo mheroInfo = TextTranslator.instance.GetHeroInfoByHeroID(_RoleID);
        SetHeroType(mheroInfo.heroCarrerType);
        SetRarityIcon(mheroInfo.heroRarity);
        SetFrame(mheroInfo.heroRarity);
        HeroSprite.mainTexture = Resources.Load("Loading/" + _RoleID.ToString()) as Texture;
        HeroSprite.GetComponent<UITexture>().MakePixelPerfect();
        HeroSprite.GetComponent<UITexture>().width = HeroSprite.GetComponent<UITexture>().width / 20 * LoadingSize;
        HeroSprite.GetComponent<UITexture>().height = HeroSprite.GetComponent<UITexture>().height / 20 * LoadingSize;
        SpriteCareer.transform.FindChild("LabelName").GetComponent<UILabel>().text = mheroInfo.heroName;
        PictureCreater.instance.PlayRoleSound(RoleID);
    }

    void SetProperty(int _HeroId)
    {
        bool isHaveHero = false;
        foreach (var item in CharacterRecorder.instance.ownedHeroList)
        {
            if (item.cardID == _HeroId)
            {
                transform.GetChild(0).GetChild(3).GetChild(0).GetChild(item.rank).gameObject.SetActive(true);

                HeroProperty.transform.Find("Power/PowerBg/Label").GetComponent<UILabel>().text = item.force.ToString();
                HeroProperty.transform.Find("Property/SpriteLevel/LabelLevel/Label").GetComponent<UILabel>().text = item.level.ToString();

                transform.GetChild(0).GetChild(3).GetChild(0).GetChild(item.rank).gameObject.SetActive(true);

                HeroProperty.transform.Find("Property/SpriteDefense/LabelDefense/Label").GetComponent<UILabel>().text = item.physicalDefense.ToString();
                HeroProperty.transform.Find("Property/SpriteLife/LabelLife/Label").GetComponent<UILabel>().text = item.HP.ToString();
                HeroProperty.transform.Find("Property/SpriteAttack/LabelAttack/Label").GetComponent<UILabel>().text = item.strength.ToString();
                HeroSkill.transform.Find("SkillOne/SkillLevel").GetComponent<UILabel>().text = "Lv." + item.skillLevel.ToString();
                isHaveHero = true;
                break;
            }
        }
        if (!isHaveHero)
        {
            transform.GetChild(0).GetChild(3).GetChild(0).GetChild(1).gameObject.SetActive(true);

            HeroProperty.transform.Find("Power/PowerBg/Label").GetComponent<UILabel>().text = "";
            HeroProperty.transform.Find("Power/PowerBg/WHGrid").gameObject.SetActive(true);
            HeroProperty.transform.Find("Property/SpriteLevel/LabelLevel/Label").GetComponent<UILabel>().text = "???";
            transform.GetChild(0).GetChild(3).GetChild(0).GetChild(1).gameObject.SetActive(true);
            HeroProperty.transform.Find("Property/SpriteDefense/LabelDefense/Label").GetComponent<UILabel>().text = "???";
            HeroProperty.transform.Find("Property/SpriteLife/LabelLife/Label").GetComponent<UILabel>().text = "???";
            HeroProperty.transform.Find("Property/SpriteAttack/LabelAttack/Label").GetComponent<UILabel>().text = "???";
            HeroSkill.transform.Find("SkillOne/SkillLevel").GetComponent<UILabel>().text = "Lv." + 1;
        }
    }

    void SetCarrer(string _HeroCareer)
    {
        HeroCarrer.text = _HeroCareer;
    }

    void SetLocation(string _HeroLocation)
    {
        HeroLocation.text = _HeroLocation;
    }

    void SetHeroSkill(List<int> _HeroSkillList)
    {
        List<Skill> _SkillOne = TextTranslator.instance.GetHeroSkillListByID(_HeroSkillList[0]);

        Skill _SkillTwo = TextTranslator.instance.GetSkillByID(_HeroSkillList[1], 1);

        HeroSkill.transform.Find("SkillOne/SkillBg/SkillFg").GetComponent<UITexture>().mainTexture = Resources.Load("Skill/" + _HeroSkillList[0]) as Texture;
        HeroSkill.transform.Find("SkillTwo/SkillBg/SkillFg").GetComponent<UITexture>().mainTexture = Resources.Load("Skill/" + _HeroSkillList[1]) as Texture;

        //Debug.LogError("length:" + _SkillOne.Count);
        //for (int i = 0; i < _SkillOne.Count; i++)
        //{
        //    Debug.LogError(i+"    name:" + _SkillOne[i].skillName + "   leavel:" + _SkillOne[i].skillLevel);
        //}
        //Debug.LogError("name:" + _SkillOne[1].skillName + "   leavel:" + _SkillOne[1].skillLevel);

        HeroSkill.transform.Find("SkillOne/SkillName").GetComponent<UILabel>().text = _SkillOne[0].skillName;

        HeroSkill.transform.Find("SkillTwo/SkillName").GetComponent<UILabel>().text = _SkillTwo.skillName;
    }

    void SetHerokarma()
    {
        if (SetFateName(RoleID))
        {
            SetFateWindow();
        }
    }

    void SetHeroIntroduce(string _heroIntroduce)
    {
        if (GameObject.Find("KarmaItem" + moveY) != null)
        {
            float positionY = GameObject.Find("KarmaItem" + moveY).transform.localPosition.y;
            HeroIntroduce.transform.GetChild(0).localPosition = new Vector3(-45, positionY - 111, 0);
            HeroIntroduce.transform.GetChild(1).localPosition = new Vector3(25, positionY - 164, 0);
            HeroIntroduce.transform.GetChild(1).GetComponent<UILabel>().text = _heroIntroduce;
        }
    }

    bool SetFateName(int cardID)
    {
        BetterList<RoleFate> MyRoleFateList = TextTranslator.instance.GetMyRoleFateListByRoleID(cardID);
        TextTranslator.instance.MyRoleFateList = MyRoleFateList;

        // Debug.LogError("缘分值："+ListFateNameLabel.Count);

        for (int i = 0; i < ListFateNameLabel.Count; i++)
        {
            if (i < MyRoleFateList.size)
            {
                string clolrStr = "";
                //Debug.LogError("167---card " + MyRoleFateList[i].RoleFateID);
                if (CharacterRecorder.instance.ListRoleFateId.Contains(MyRoleFateList[i].RoleFateID))
                {
                    clolrStr = "[ff8c04]";
                }
                else
                {
                    clolrStr = "[75a3b8]";
                }
                ListFateNameLabel[i].text = clolrStr + MyRoleFateList[i].Name;
            }
            else
            {
                ListFateNameLabel[i].gameObject.SetActive(false);
            }
        }
        return true;
    }

    public void SetFateWindow()
    {
        BetterList<RoleFate> _MyRoleFateList = TextTranslator.instance.GetMyRoleFateListByRoleID(RoleID);
        moveY = _MyRoleFateList.size;
        Debug.Log("_MyRoleFateList.." + _MyRoleFateList.size);
        for (int i = 0; i < _MyRoleFateList.size; i++)
        {
            GameObject obj = NGUITools.AddChild(HeroKarma, KarmaItem);
            obj.name = "KarmaItem" + (i + 1);
            obj.transform.localPosition = new Vector3(KarmaItem.transform.localPosition.x, KarmaItem.transform.localPosition.y - FateYMoveDistance - i * 145);
            obj.GetComponent<FateItem>().SetFateItem(RoleID, _MyRoleFateList[i]);
        }
    }

    void SetheroRank()
    {

    }
    //设置英雄职业
    void SetHeroType(int _herotype)
    {
        if (_herotype == 1 || _herotype == 2 || _herotype == 3)
        {
            SpriteCareer.spriteName = string.Format("heroType{0}", _herotype);
        }
        else
        {
            Debug.LogError("英雄职业类型参数错误" + _herotype);
        }
    }
    //设置稀有度
    void SetRarityIcon(int _heroRarity)
    {
        switch (_heroRarity)
        {
            case 1:
                SpriteRare.spriteName = "word4";
                break;
            case 2:
                SpriteRare.spriteName = "word5";
                break;
            case 3:
                SpriteRare.spriteName = "word6";
                break;
            case 4:
                SpriteRare.spriteName = "word7";
                break;
            case 5:
                SpriteRare.spriteName = "word8";
                break;
            case 6:
                SpriteRare.spriteName = "word9";
                break;
            default:
                break;
        }
    }
    void SetFrame(int _heroRarity)
    {
        string bgName = null;
        string frameName = null;
        switch (_heroRarity)
        {
            case 1:
                bgName = "carddi0";
                frameName = "kuang0";
                break;
            case 2:
                bgName = "carddi1";
                frameName = "kuang1";
                break;
            case 3:
                bgName = "carddi2";
                frameName = "kuang2";
                break;
            case 4:
                bgName = "carddi3";
                frameName = "kuang3";
                break;
            case 5:
                bgName = "carddi4";
                frameName = "kuang4";
                break;
            case 6:
                bgName = "carddi5";
                frameName = "kuang5";
                break;
        }
        FrameSprite.mainTexture = Resources.Load("Game/" + frameName) as Texture;
        BGSprite.mainTexture = Resources.Load("Game/" + bgName) as Texture;
    }
}
