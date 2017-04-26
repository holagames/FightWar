using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroInfoPart : MonoBehaviour
{
    public GameObject equipItemsEffect;
    public GameObject WearEquipEffect;

    public List<GameObject> ListEquip = new List<GameObject>();
    [SerializeField]
    private List<UILabel> ListFateNameLabel = new List<UILabel>();
    [SerializeField]
    private GameObject fateBox;
    private int ClickIndex = 0;
    private int CharacterRoleID = 0;

    public GameObject LabelHp;
    public GameObject LabelPDefend;
    public GameObject LabelPAttack;
    public GameObject RightLabelLevel;
    public GameObject LabelSkill;
    public GameObject LabelSkillDesc;
    public GameObject LabelSkill2;
    public GameObject LabelSkillDesc2;
    public GameObject SkillBoard1;
    public GameObject SkillBoard2;
    public GameObject LabelName;
    public GameObject LabelFight;
    public GameObject LabelSpeed;
    public GameObject LabelIntrodunce;
    [SerializeField]
    private GameObject carrerItem;
    public GameObject RankIcon;
    public GameObject heroRankHorizontal;
    [SerializeField]
    private UISprite RarityIcon;
    [SerializeField]
    private UISprite heroTypeIcon;
    [SerializeField]
    private GameObject ATKtype;
    [SerializeField]
    private GameObject DEFtype;
    [SerializeField]
    private UILabel talentLabel;
    public UILabel InForceLabel;
    public UILabel PetPhraseLabel;
    [SerializeField]
    private UITexture mySkill1;
    [SerializeField]
    private UITexture mySkill2;

    int RoleIndex = -1;
    private HeroInfo _heroInfo;
    private Hero curHero;

    public GameObject CombinButton;
    public GameObject RebirthButton;
    public GameObject RongluButton;

    void ClickAtkOrDefTypeButton(GameObject go, bool isPress)
    {
        GameObject _InfoBoard = go.transform.FindChild("InfoBoard").gameObject;
        if (isPress)
        {
            _InfoBoard.SetActive(true);
        }
        else
        {
            _InfoBoard.SetActive(false);
        }
    }
    void ClickAtkOrDefTypeButton(GameObject go)
    {
        if (go != null)
        {
            GameObject _InfoBoard = ATKtype.transform.FindChild("InfoBoard").gameObject;
            _InfoBoard.SetActive(true);
        }
    }
    // Use this for initialization
    void Start()
    {
        //去掉特效
        //       StartCoroutine(SetEquipItemsEffect());
        //旧的
        //UIEventListener.Get(ATKtype).onPress = ClickAtkOrDefTypeButton;
        //UIEventListener.Get(DEFtype).onPress = ClickAtkOrDefTypeButton;
        //新的
        UIEventListener.Get(ATKtype).onClick = ClickAtkOrDefTypeButton;
        UIEventListener.Get(DEFtype).onClick = ClickAtkOrDefTypeButton;

        if (UIEventListener.Get(fateBox).onClick == null)
        {
            UIEventListener.Get(fateBox).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPanel("FateWindow", false);
                GameObject _FateWindow = GameObject.Find("FateWindow");
                _FateWindow.GetComponent<FateWindow>().SetFateWindow(curHero, TextTranslator.instance.MyRoleFateList);
                _FateWindow.layer = 11;
                foreach (Component c in _FateWindow.GetComponentsInChildren(typeof(Transform), true))
                {
                    c.gameObject.layer = 11;
                }
                return;
            };
        }

        InitOpenState();
    }
    void InitOpenState()
    {
        for (int i = 0; i < ListEquip.Count; i++)
        {
            UIEventListener.Get(ListEquip[i]).onClick = ClickListEquipButton;
            if (ListEquip[i].transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName != "add")
            {
                switch (ListEquip[i].name)
                {
                    case "5":
                    case "6":
                        GameObject _Lock = ListEquip[i].transform.FindChild("Lock").gameObject;
                        if (CharacterRecorder.instance.level < 16)
                        {
                            _Lock.SetActive(true);
                        }
                        else
                        {
                            _Lock.SetActive(false);
                        }
                        break;
                }
            }
            else
            {
                GameObject _Lock = ListEquip[i].transform.FindChild("Lock").gameObject;
                _Lock.SetActive(false);
            }
        }
    }
    void ClickListEquipButton(GameObject go)
    {
        if (go != null)
        {
            ClickIndex = int.Parse(go.name);
            if (go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName == "add")
            {
                UIManager.instance.OpenPanel("RoleEquipWindow", false);
                if (GameObject.Find("RoleEquipWindow") != null)
                {
                    int number = 0;
                    if (_heroInfo.heroBio == 1)
                    {
                        number = 1;
                    }
                    else
                    {
                        if (_heroInfo.heroFly == 0)
                        {
                            number = 2;
                        }
                        else
                        {
                            number = 3;
                        }
                    }

                    RoleEquipWindow rew = GameObject.Find("RoleEquipWindow").GetComponent<RoleEquipWindow>();
                    rew.SetInfo(_heroInfo.heroAtkType, number, ClickIndex - 1);
                }
            }
            else
            {
                switch (go.name)
                {
                    case "5":
                    case "6":
                        if (CharacterRecorder.instance.level < 16)
                        {
                            UIManager.instance.OpenPromptWindow(string.Format("{0}级开放饰品强化精炼晋级", 16), PromptWindow.PromptType.Hint, null, null);
                        }
                        else
                        {
                            PictureCreater.instance.DestroyAllComponent();
                            CharacterRecorder.instance.isNeedRecordStrengTabType = false;
                            CharacterRecorder.instance.IsNeedResetHedIndexAfterSortHero = true;
                            UIManager.instance.OpenPanel("StrengEquipWindow", true);
                            StrengEquipWindow _sew = GameObject.Find("StrengEquipWindow").GetComponent<StrengEquipWindow>();
                            _sew.SetEquipPosition(ClickIndex);
                        }
                        break;
                    default:
                        SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                        //Debug.LogError(CharacterRecorder.instance.lastGateID + "..." + CharacterRecorder.instance.GuideID[28]);
                        PictureCreater.instance.DestroyAllComponent();
                        CharacterRecorder.instance.isNeedRecordStrengTabType = false;
                        CharacterRecorder.instance.IsNeedResetHedIndexAfterSortHero = true;
                        UIManager.instance.OpenPanel("StrengEquipWindow", true);
                        StrengEquipWindow sew = GameObject.Find("StrengEquipWindow").GetComponent<StrengEquipWindow>();
                        sew.SetEquipPosition(ClickIndex);
                        break;
                }

            }
        }
    }
    //设置英雄信息
    public void SetHero(int Index)
    {
        Hero h = CharacterRecorder.instance.ownedHeroList[Index];
        HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(h.cardID);
        _heroInfo = hinfo;
        curHero = h;
        CharacterRoleID = h.characterRoleID;

        SetFateName(h.cardID);
        //GameObject.Find("MySkill1").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetSkillByID(hinfo.heroSkillList[0], h.skillLevel).skillID.ToString();
        //GameObject.Find("MySkill2").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetSkillByID(hinfo.heroSkillList[1], 1).skillID.ToString();
        mySkill1.mainTexture = Resources.Load(string.Format("Skill/{0}", TextTranslator.instance.GetSkillByID(hinfo.heroSkillList[0], h.skillLevel).skillID), typeof(Texture)) as Texture;
        mySkill2.mainTexture = Resources.Load(string.Format("Skill/{0}", TextTranslator.instance.GetSkillByID(hinfo.heroSkillList[1], 1).skillID), typeof(Texture)) as Texture;

        mySkill1.transform.FindChild("level").GetComponent<UILabel>().text = "lv." + h.skillLevel;

        StartCoroutine(CreatModel(h));

        LabelName.GetComponent<UILabel>().text = hinfo.heroName;

        LabelHp.GetComponent<UILabel>().text = h.HP.ToString();
        LabelPDefend.GetComponent<UILabel>().text = h.physicalDefense.ToString();
        LabelPAttack.GetComponent<UILabel>().text = h.strength.ToString();
        RightLabelLevel.GetComponent<UILabel>().text = h.level.ToString();
        LabelFight.GetComponent<UILabel>().text = h.force.ToString();
        LabelSpeed.GetComponent<UILabel>().text = h.aspd.ToString();
        LabelIntrodunce.GetComponent<UILabel>().text = _heroInfo.careerShow;
        carrerItem.GetComponent<CareerItem>().SetCareerInfo(_heroInfo.careerShow, h.area, h.move);
        //   SetAtkOrDefTRype();//旧的
        SetAtkOrDefTRype(0);
        //  SetRankIcon();
        heroRankHorizontal.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(curHero.rank);
        SetTalentInfo(curHero);
        SetBackgroundPart(_heroInfo);
        //SetNameColor();//旧的
        TextTranslator.instance.SetHeroNameColor(LabelName.GetComponent<UILabel>(), _heroInfo.heroName, curHero.classNumber);
        SetRarityIcon();
        SetHeroType(_heroInfo.heroCarrerType);

        UIEventListener.Get(mySkill1.gameObject).onPress = delegate(GameObject go, bool isPress)
        {
            if (isPress)
            {
                SkillBoard1.SetActive(true);
                LabelSkill.GetComponent<UILabel>().text = string.Format("[{0}]", TextTranslator.instance.GetSkillByID(_heroInfo.heroSkillList[0], h.skillLevel).skillName);
                LabelSkillDesc.GetComponent<UILabel>().text = TextTranslator.instance.GetSkillByID(_heroInfo.heroSkillList[0], h.skillLevel).description;
            }
            else
            {
                SkillBoard1.SetActive(false);
            }
        };
        UIEventListener.Get(mySkill2.gameObject).onPress = delegate(GameObject go, bool isPress)
        {
            if (isPress)
            {
                SkillBoard2.SetActive(true);
                LabelSkill2.GetComponent<UILabel>().text = string.Format("[{0}]", TextTranslator.instance.GetSkillByID(_heroInfo.heroSkillList[1], 1).skillName);
                LabelSkillDesc2.GetComponent<UILabel>().text = TextTranslator.instance.GetSkillByID(_heroInfo.heroSkillList[1], 1).description;
            }
            else
            {
                SkillBoard2.SetActive(false);
            }
        };

        foreach (var e in h.equipList)
        {
            TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(e.equipCode);
            //  SetEquip(h.characterRoleID, e.equipCode, e.equipID, e.equipPosition, e.equipClass, e.equipLevel);
            SetEquip(e);
        }
        if (curHero.rare < 4)
        {
            CombinButton.SetActive(false);
        }
        else
        {
            CombinButton.SetActive(true);
        }
        UIEventListener.Get(CombinButton).onClick += delegate(GameObject go)
        {
            UIManager.instance.CountSystem(UIManager.Systems.小队技能);
            UIManager.instance.UpdateSystems(UIManager.Systems.小队技能);
            GameObject CombosWindow = UIManager.instance.OpenSinglePanel("RoleCombosWindow", false);
            CombosWindow.GetComponent<RoleCombosWindow>().SetHeroInfo(curHero.cardID);

        };



        // RebirthButton.SetActive(true);
        UIEventListener.Get(RebirthButton).onClick = delegate(GameObject go)
        {
            if (CharacterRecorder.instance.lastGateID > TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.chongsheng).Level)
            {
                PictureCreater.instance.DestroyAllComponent();
                CharacterRecorder.instance.isNeedRecordStrengTabType = false;
                CharacterRecorder.instance.IsNeedResetHedIndexAfterSortHero = true;
                CharacterRecorder.instance.RebirthRoleId = curHero.cardID;
                UIManager.instance.OpenPanel("RebirthWindow", true);
            }
            else
            {
                UIManager.instance.OpenPromptWindow(string.Format("通过{0}关开启！", TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.chongsheng).Level - 10000), PromptWindow.PromptType.Hint, null, null);
            }
        };

        UIEventListener.Get(RongluButton).onClick = delegate(GameObject go)
        {
            if (CharacterRecorder.instance.lastGateID > TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.chongsheng).Level)
            {
                PictureCreater.instance.DestroyAllComponent();
                UIManager.instance.OpenPanel("TechnologyfurnaceWindow", true);
            }
            else
            {
                UIManager.instance.OpenPromptWindow(string.Format("通过{0}关开启！", TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.chongsheng).Level - 10000), PromptWindow.PromptType.Hint, null, null);
            }
        };

    }
    IEnumerator CreatModel(Hero h)
    {
        PictureCreater.instance.DestroyAllComponent();
        int i = PictureCreater.instance.CreateRole(h.cardID, "Model", 18, Color.red, 0, 0, 1, 2f, false, false, 1, 1.5f, 0.2f, "Model", 0, 10, 10, 2, 2, 0, 0, 1001, 1002, h.WeaponList[0].WeaponClass, 1, h.WeaponList[0].WeaponClass, 1, 1, 0, "");
        PictureCreater.instance.ListRolePicture[0].RoleObject.transform.parent = gameObject.transform;
        //角色旋转、播动画
        GameObject _roleWindow = GameObject.Find("RoleWindow");
        if (_roleWindow != null)
        {
            _roleWindow.GetComponent<PlayAniOrRotation>().ResetTargetRole(PictureCreater.instance.ListRolePicture[0].RoleObject.transform.FindChild("Role").gameObject, h);
        }
        int modelPositionY = -950;//-1370 飞机的话
        if (h.cardID == 60201)//|| h.cardID == 60200
        {
            modelPositionY = -1200;
        }
        if ((Screen.width * 1f / Screen.height) > 1.7f)
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = new Vector3(7280, modelPositionY, 13339);
        }
        else if ((Screen.width * 1f / Screen.height) < 1.4f)
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = new Vector3(8413 - 636 - 92 - 341, modelPositionY, 13339);
        }
        else
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = new Vector3(7300, modelPositionY, 13339);
        }
        //PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localScale = new Vector3(500, 500, 500) * TextTranslator.instance.GetHeroInfoByHeroID(PictureCreater.instance.ListRolePicture[0].RoleID).heroScale;
        //PictureCreater.instance.ListRolePicture[0].RoleObject.transform.Rotate(new Vector3(0, 65, 0));
        PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localScale = new Vector3(400, 400, 400);
        PictureCreater.instance.ListRolePicture[0].RolePictureObject.transform.Rotate(new Vector3(0, 65, 0));
        yield return new WaitForSeconds(0.1f);
    }
    IEnumerator CloseEffect(GameObject go, string EffectName)
    {
        yield return new WaitForSeconds(0.5f);
        if (go.transform.FindChild(EffectName) != null)
        {
            go.transform.FindChild(EffectName).gameObject.SetActive(false);
        }

    }

    //设置装备
    public void SetEquip(int _CharacterRoleID, int _ItemCode, int _EquipID, int _Index, int _ItemGrade, int _Lv)
    {
        GameObject go = ListEquip[_Index - 1];
        if (_Index < 5)
        {
            go.GetComponent<EquipItemInStreng>().SetEquipItemInStrengh(curHero, _Index);
        }

        StartCoroutine(CloseEffect(go, "WearEquipEffect"));
        if (_ItemCode != 0)
        {
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_ItemCode);
            go.GetComponent<UISprite>().spriteName = string.Format("Grade{0}", mItemInfo.itemGrade);
            go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = _ItemCode.ToString();
            go.transform.Find("LabelEquipLevel").gameObject.GetComponent<UILabel>().text = _Lv.ToString();
        }
        else
        {
            Debug.Log("装备有空格!!!!!!!!!!!!!!");
            go.GetComponent<UISprite>().spriteName = string.Format("Grade{0}", 1);
            go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = "add";
            go.transform.Find("LabelEquipLevel").gameObject.GetComponent<UILabel>().text = "";
        }
    }
    public void SetEquip(Hero.EquipInfo _OneEquipInfo)
    {
        GameObject go = ListEquip[_OneEquipInfo.equipPosition - 1];
        if (_OneEquipInfo.equipPosition < 5)
        {
            go.GetComponent<EquipItemInStreng>().SetEquipItemInStrengh(curHero, _OneEquipInfo.equipPosition);
            SetEquipStrenghRedPoint(_OneEquipInfo, go);
        }

        StartCoroutine(CloseEffect(go, "WearEquipEffect"));
        if (_OneEquipInfo.equipCode != 0)
        {
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_OneEquipInfo.equipCode);
            go.GetComponent<UISprite>().spriteName = string.Format("Grade{0}", mItemInfo.itemGrade);
            go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = _OneEquipInfo.equipCode.ToString();
            go.transform.Find("LabelEquipLevel").gameObject.GetComponent<UILabel>().text = _OneEquipInfo.equipLevel.ToString();
        }
        else
        {
            Debug.Log("装备有空格!!!!!!!!!!!!!!");
            go.GetComponent<UISprite>().spriteName = string.Format("Grade{0}", 1);
            go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = "add";
            go.transform.Find("LabelEquipLevel").gameObject.GetComponent<UILabel>().text = "";
        }
    }
    void SetEquipStrenghRedPoint(Hero.EquipInfo _OneEquipInfo, GameObject go)
    {
        GameObject _RedPoint = go.transform.FindChild("RedPoint").gameObject;
        GameObject _UpJianTou = go.transform.FindChild("UpJianTou").gameObject;
        //Debug.LogError(IsAdvanceState(_OneEquipInfo.equipLevel, _OneEquipInfo.equipColorNum));
        int _EquipPosition = _OneEquipInfo.equipPosition;
        int _EquipLevel = _OneEquipInfo.equipLevel;
        int _EquipColorNum = _OneEquipInfo.equipColorNum;
        if (IsAdvanceState(_EquipLevel, _EquipColorNum) && IsEnoughToAdvance(_OneEquipInfo) && IsAdvanceMaterailEnough(_OneEquipInfo))
        {
            _RedPoint.SetActive(true);
            _UpJianTou.SetActive(false);
        }
        else if (IsAdvanceState(_EquipLevel, _EquipColorNum) == false && IsEnoughToUpGrade(_OneEquipInfo) && _OneEquipInfo.equipLevel < CharacterRecorder.instance.level)
        {
            _RedPoint.SetActive(false);
            _UpJianTou.SetActive(true);
            go.GetComponent<EquipItemInStreng>().IsUpJianTouActive = true;
        }
        else
        {
            _RedPoint.SetActive(false);
            _UpJianTou.SetActive(false);
        }
    }
    bool IsAdvanceMaterailEnough(Hero.EquipInfo _OneEquipInfo)
    {
        EquipStrongQuality esq = TextTranslator.instance.GetEquipStrongQualityByIDAndColor(_OneEquipInfo.equipCode, _OneEquipInfo.equipColorNum, _heroInfo.heroRace);
        for (int i = 0; i < esq.materialItemList.size; i++)
        {
            int itemCode = esq.materialItemList[i].itemCode;
            int itemCountInBag = TextTranslator.instance.GetItemCountByID(itemCode);
            if (esq.materialItemList[i].itemCount > itemCountInBag)
            {
                return false;
            }
        }
        return true;
    }
    bool IsEnoughToUpGrade(Hero.EquipInfo _OneEquipInfo)
    {
        int needMoney = TextTranslator.instance.GetEquipStrongCostByID(_OneEquipInfo.equipLevel, _heroInfo.heroRarity, _OneEquipInfo.equipPosition);
        if (CharacterRecorder.instance.gold >= needMoney)
        {
            return true;
        }
        return false;
    }
    bool IsEnoughToAdvance(Hero.EquipInfo _OneEquipInfo)
    {
        int _EquipPosition = _OneEquipInfo.equipPosition;
        EquipStrongQuality esq = TextTranslator.instance.GetEquipStrongQualityByIDAndColor(curHero.equipList[_EquipPosition - 1].equipCode, curHero.equipList[_EquipPosition - 1].equipColorNum, _heroInfo.heroRace);
        if (CharacterRecorder.instance.gold >= esq.Money)
        {
            return true;
        }
        return false;
    }
    bool IsAdvanceState(int _EquipLevel, int _EquipColorNum)
    {
        if (_EquipColorNum * 5 == _EquipLevel)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SetEquipLevel(int _CharacterRoleID, int _Index, int _EquipID, int _Class, int _Lv, int _Exp)
    {
        foreach (var h in CharacterRecorder.instance.ownedHeroList)
        {
            if (h.characterRoleID == _CharacterRoleID)
            {
                foreach (var e in h.equipList)
                {
                    if (e.equipID == _EquipID)
                    {
                        e.equipLevel = _Lv;
                        e.equipClass = _Class;
                        e.equipExp = _Exp;
                        break;
                    }
                }
                break;
            }
        }
    }
    IEnumerator SetEquipItemsEffect()
    {
        for (int i = 0; i < ListEquip.Count; i++)
        {
            GameObject objEffect = NGUITools.AddChild(ListEquip[i], equipItemsEffect) as GameObject;
            objEffect.transform.localScale = new Vector3(400, 400, 1);
            yield return new WaitForSeconds(0.2f);
        }

    }

    void SetRoleColor(GameObject go, int rankNumber)
    {
        switch (rankNumber)
        {
            case 1:
                go.GetComponent<UISprite>().spriteName = "yxdi3";

                break;
            case 2:
                go.GetComponent<UISprite>().spriteName = "yxdi3";
                break;
            case 3:
                go.GetComponent<UISprite>().spriteName = "yxdi3";
                break;
            case 4:
                go.GetComponent<UISprite>().spriteName = "yxdi3";
                break;
            case 5:
                go.GetComponent<UISprite>().spriteName = "yxdi";
                break;
            case 6:
                go.GetComponent<UISprite>().spriteName = "yxdi";
                break;
            case 7:
                go.GetComponent<UISprite>().spriteName = "yxdi";
                break;
            case 8:
                go.GetComponent<UISprite>().spriteName = "yxdi";
                break;
            case 9:
                go.GetComponent<UISprite>().spriteName = "yxdi";
                break;
            case 10:
                go.GetComponent<UISprite>().spriteName = "yxdi5";
                break;
            case 11:
                go.GetComponent<UISprite>().spriteName = "yxdi5";
                break;
            case 12:
                go.GetComponent<UISprite>().spriteName = "yxdi5";
                break;
            case 13:
                go.GetComponent<UISprite>().spriteName = "yxdi5";
                break;
            case 14:
                go.GetComponent<UISprite>().spriteName = "yxdi5";
                break;
            default:
                break;
        }
    }
    //设置攻击防御类型（新的）
    void SetAtkOrDefTRype(int _newUnUsed)
    {
        ATKtype.GetComponent<UISprite>().spriteName = string.Format("atk{0}", _heroInfo.heroAtkType);
        DEFtype.GetComponent<UISprite>().spriteName = string.Format("def{0}", _heroInfo.heroDefType);
        Transform _ATKtypeInfoBoard = ATKtype.transform.FindChild("InfoBoard");
        GameObject _InfoBoardBox = _ATKtypeInfoBoard.transform.FindChild("InfoBoard").gameObject;
        if (UIEventListener.Get(_InfoBoardBox).onClick == null)
        {
            UIEventListener.Get(_InfoBoardBox).onClick += delegate(GameObject go)
            {
                _ATKtypeInfoBoard.gameObject.SetActive(false);
            };
        }
        string ATKtypeName = "";
        string ATKtypeDes = "";
        switch (_heroInfo.heroAtkType)
        {
            case 1: ATKtypeName = "普通枪械"; ATKtypeDes = "对[ff0000]轻甲[-]造成较多伤害，对[ff0000]建筑[-]伤害很弱。"; break;
            case 2: ATKtypeName = "重型枪械"; ATKtypeDes = "对[ff0000]中甲[-]造成较多伤害，对[ff0000]建筑[-]伤害很弱。"; break;
            case 3: ATKtypeName = "炮弹"; ATKtypeDes = "对[ff0000]重装[-]造成较多伤害，对[ff0000]中甲[-]伤害较弱。"; break;
            case 4: ATKtypeName = "穿甲弹"; ATKtypeDes = "对[ff0000]建筑[-]造成大量伤害，对[ff0000]轻甲[-]伤害较弱。"; break;
        }
        _InfoBoardBox.transform.FindChild("TypeName1").GetComponent<UILabel>().text = ATKtypeName;
        _InfoBoardBox.transform.FindChild("TypeDes1").GetComponent<UILabel>().text = ATKtypeDes;

        string DEFtypeName = "";
        string DEFtypeDes = "";
        switch (_heroInfo.heroAtkType)
        {
            case 1: DEFtypeName = "轻甲"; DEFtypeDes = "步兵护甲，防护能力较低。"; break;
            case 2: DEFtypeName = "中甲"; DEFtypeDes = "步兵强化护甲，可有效抵御伤害。"; break;
            case 3: DEFtypeName = "重装"; DEFtypeDes = "机械单位护甲，对普通枪械较强。"; break;
            case 4: DEFtypeName = "建筑"; DEFtypeDes = "强固如城壁的护甲，但是惧怕穿甲弹。"; break;
        }
        _InfoBoardBox.transform.FindChild("TypeName2").GetComponent<UILabel>().text = DEFtypeName;
        _InfoBoardBox.transform.FindChild("TypeDes2").GetComponent<UILabel>().text = DEFtypeDes;
    }
    //设置攻击防御类型(旧的)
    void SetAtkOrDefTRype()
    {
        ATKtype.GetComponent<UISprite>().spriteName = string.Format("atk{0}", _heroInfo.heroAtkType);
        DEFtype.GetComponent<UISprite>().spriteName = string.Format("def{0}", _heroInfo.heroDefType);
        Transform _ATKtypeInfoBoard = ATKtype.transform.FindChild("InfoBoard");
        string ATKtypeName = "";
        string ATKtypeDes = "";
        switch (_heroInfo.heroAtkType)
        {
            case 1: ATKtypeName = "普通枪械"; ATKtypeDes = "对[ff0000]轻甲[-]造成较多伤害，对[ff0000]建筑[-]伤害很弱。"; break;
            case 2: ATKtypeName = "重型枪械"; ATKtypeDes = "对[ff0000]中甲[-]造成较多伤害，对[ff0000]建筑[-]伤害很弱。"; break;
            case 3: ATKtypeName = "炮弹"; ATKtypeDes = "对[ff0000]重装[-]造成较多伤害，对[ff0000]中甲[-]伤害较弱。"; break;
            case 4: ATKtypeName = "穿甲弹"; ATKtypeDes = "对[ff0000]建筑[-]造成大量伤害，对[ff0000]轻甲[-]伤害较弱。"; break;
        }
        _ATKtypeInfoBoard.transform.FindChild("InfoBoard").FindChild("TypeName").GetComponent<UILabel>().text = ATKtypeName;
        _ATKtypeInfoBoard.transform.FindChild("InfoBoard").FindChild("TypeDes").GetComponent<UILabel>().text = ATKtypeDes;

        Transform _DEFtypeInfoBoard = DEFtype.transform.FindChild("InfoBoard");
        string DEFtypeName = "";
        string DEFtypeDes = "";
        switch (_heroInfo.heroAtkType)
        {
            case 1: DEFtypeName = "轻甲"; DEFtypeDes = "步兵护甲，防护能力较低。"; break;
            case 2: DEFtypeName = "中甲"; DEFtypeDes = "步兵强化护甲，可有效抵御伤害。"; break;
            case 3: DEFtypeName = "重装"; DEFtypeDes = "机械单位护甲，对普通枪械较强。"; break;
            case 4: DEFtypeName = "建筑"; DEFtypeDes = "强固如城壁的护甲，但是惧怕穿甲弹。"; break;
        }
        _DEFtypeInfoBoard.transform.FindChild("InfoBoard").FindChild("TypeName").GetComponent<UILabel>().text = DEFtypeName;
        _DEFtypeInfoBoard.transform.FindChild("InfoBoard").FindChild("TypeDes").GetComponent<UILabel>().text = DEFtypeDes;
    }
    //设置军衔
    void SetRankIcon()
    {
        //RankIcon.GetComponent<UISprite>().spriteName = string.Format("rank{0}",(curHero.rank + 1).ToString("00"));
        RankIcon.GetComponent<UISprite>().spriteName = string.Format("rank{0}", curHero.rank.ToString("00"));
        UILabel _myUILabel = RankIcon.transform.FindChild("Label").GetComponent<UILabel>();
        _myUILabel.text = GetJunXianName(curHero.rank);
    }
    string GetJunXianName(int rank)
    {
        string rankName = "";
        switch (rank)
        {
            case 1:
                rankName = "一星";
                break;
            case 2:
                rankName = "二星";
                break;
            case 3:
                rankName = "三星";
                break;
            case 4:
                rankName = "四星";
                break;
            case 5:
                rankName = "五星";
                break;
            case 6:
                rankName = "六星";
                break;
            case 7:
                rankName = "红一星";
                break;
            case 8:
                rankName = "红二星";
                break;
            case 9:
                rankName = "红三星";
                break;
            case 10:
                rankName = "红四星";
                break;
            case 11:
                rankName = "红五星";
                break;
            case 12:
                rankName = "红六星";
                break;
            default:
                break;
        }
        return rankName;
    }
    //设置名字颜色
    void SetNameColor()
    {
        switch (curHero.classNumber)
        {
            case 1:
                LabelName.GetComponent<UILabel>().color = Color.white;
                break;
            case 2:
                LabelName.GetComponent<UILabel>().text = "[3ee817]" + LabelName.GetComponent<UILabel>().text + "[-]";
                break;
            case 3:
                LabelName.GetComponent<UILabel>().text = "[3ee817]" + LabelName.GetComponent<UILabel>().text + "+1[-]";
                break;
            case 4:
                LabelName.GetComponent<UILabel>().text = "[249bd2]" + LabelName.GetComponent<UILabel>().text + "[-]";
                break;
            case 5:
                LabelName.GetComponent<UILabel>().text = "[249bd2]" + LabelName.GetComponent<UILabel>().text + "+1[-]";
                break;
            case 6:
                LabelName.GetComponent<UILabel>().text = "[249bd2]" + LabelName.GetComponent<UILabel>().text + "+2[-]";
                break;
            case 7:
                LabelName.GetComponent<UILabel>().text = "[bb44ff]" + LabelName.GetComponent<UILabel>().text + "[-]";
                break;
            case 8:
                LabelName.GetComponent<UILabel>().text = "[bb44ff]" + LabelName.GetComponent<UILabel>().text + "+1[-]";
                break;
            case 9:
                LabelName.GetComponent<UILabel>().text = "[bb44ff]" + LabelName.GetComponent<UILabel>().text + "+2[-]";
                break;
            case 10:
                LabelName.GetComponent<UILabel>().text = "[bb44ff]" + LabelName.GetComponent<UILabel>().text + "+3[-]";
                break;
            case 11:
                LabelName.GetComponent<UILabel>().text = "[ff8c04]" + LabelName.GetComponent<UILabel>().text + "[-]";
                break;
            case 12:
                LabelName.GetComponent<UILabel>().text = "[ff8c04]" + LabelName.GetComponent<UILabel>().text + "+1[-]";
                break;
            default:
                break;
        }
    }

    //设置稀有度
    void SetRarityIcon()
    {
        // Debug.LogError(_heroInfo.heroRarity);
        switch (_heroInfo.heroRarity)
        {
            case 1:
                RarityIcon.spriteName = "word4";
                break;
            case 2:
                RarityIcon.spriteName = "word5";
                break;
            case 3:
                RarityIcon.spriteName = "word6";
                break;
            case 4:
                RarityIcon.spriteName = "word7";
                break;
            case 5:
                RarityIcon.spriteName = "word8";
                break;
            case 6:
                RarityIcon.spriteName = "word9";
                break;
            default:
                break;
        }
    }
    //设置英雄职业
    void SetHeroType(int _herotype)
    {
        if (_herotype == 1 || _herotype == 2 || _herotype == 3)
        {
            heroTypeIcon.spriteName = string.Format("heroType{0}", _herotype);
        }
        else
        {
            Debug.LogError("英雄职业类型参数错误" + _herotype);
        }
    }
    //设置缘分名字
    void SetFateName(int cardID)
    {
        BetterList<RoleFate> MyRoleFateList = TextTranslator.instance.GetMyRoleFateListByRoleID(cardID);
        TextTranslator.instance.MyRoleFateList = MyRoleFateList;
        //Debug.LogError("..roleID.." + roleID + "....MyRoleFateList..." + MyRoleFateList.size);
        for (int i = 0; i < ListFateNameLabel.Count; i++)
        {
            if (i < MyRoleFateList.size)
            {
                string clolrStr = "";
                if (CharacterRecorder.instance.ListRoleFateId.Contains(MyRoleFateList[i].RoleFateID))
                {
                    clolrStr = "[ff8c04]";
                }
                else
                {
                    clolrStr = "[919090]";
                }
                ListFateNameLabel[i].text = clolrStr + MyRoleFateList[i].Name;
            }
        }
    }
    string ChangeDecimalToPercent(float _Decimal)
    {
        return string.Format("{0}%", _Decimal * 100);
    }
    void SetTalentInfo(Hero _myHero)
    {
        talentLabel.text = "";
        //Debug.LogError("_myHero.cardID........" + _myHero.cardID);
        _myHero.heroTalentList = TextTranslator.instance.GetHeroTalentListByHeroID(_myHero.cardID);
        for (int i = 0; i < _myHero.heroTalentList.Count; i++)
        {
            string desAndDataStr = "";
            if (_myHero.heroTalentList[i].Hp > 0)
            {
                string usefulNumStr = "";
                if (_myHero.heroTalentList[i].Hp < 1 && _myHero.heroTalentList[i].Hp > 0)
                {
                    usefulNumStr = ChangeDecimalToPercent(_myHero.heroTalentList[i].Hp);
                }
                else
                {
                    usefulNumStr = (_myHero.heroTalentList[i].Hp).ToString();
                }
                desAndDataStr += "生命+" + usefulNumStr + " ";
            }
            if (_myHero.heroTalentList[i].Atk > 0)
            {
                string usefulNumStr = "";
                if (_myHero.heroTalentList[i].Atk < 1 && _myHero.heroTalentList[i].Atk > 0)
                {
                    usefulNumStr = ChangeDecimalToPercent(_myHero.heroTalentList[i].Atk);
                }
                else
                {
                    usefulNumStr = (_myHero.heroTalentList[i].Atk).ToString();
                }
                desAndDataStr += "攻击+" + usefulNumStr + " ";
            }
            if (_myHero.heroTalentList[i].Def > 0)
            {
                string usefulNumStr = "";
                if (_myHero.heroTalentList[i].Def < 1 && _myHero.heroTalentList[i].Def > 0)
                {
                    usefulNumStr = ChangeDecimalToPercent(_myHero.heroTalentList[i].Def);
                }
                else
                {
                    usefulNumStr = (_myHero.heroTalentList[i].Def).ToString();
                }
                desAndDataStr += "防御+" + usefulNumStr + " ";
            }
            if (_myHero.heroTalentList[i].Hit > 0)
            {
                string usefulNumStr = "";
                if (_myHero.heroTalentList[i].Hit < 1 && _myHero.heroTalentList[i].Hit > 0)
                {
                    usefulNumStr = ChangeDecimalToPercent(_myHero.heroTalentList[i].Hit);
                }
                else
                {
                    usefulNumStr = (_myHero.heroTalentList[i].Hit).ToString();
                }
                desAndDataStr += "命中+" + usefulNumStr + " ";
            }
            if (_myHero.heroTalentList[i].NoHit > 0)
            {
                string usefulNumStr = "";
                if (_myHero.heroTalentList[i].NoHit < 1 && _myHero.heroTalentList[i].NoHit > 0)
                {
                    usefulNumStr = ChangeDecimalToPercent(_myHero.heroTalentList[i].NoHit);
                }
                else
                {
                    usefulNumStr = (_myHero.heroTalentList[i].NoHit).ToString();
                }
                desAndDataStr += "闪避+" + usefulNumStr + " ";
            }
            if (_myHero.heroTalentList[i].Crit > 0)
            {
                string usefulNumStr = "";
                if (_myHero.heroTalentList[i].Crit < 1 && _myHero.heroTalentList[i].Crit > 0)
                {
                    usefulNumStr = ChangeDecimalToPercent(_myHero.heroTalentList[i].Crit);
                }
                else
                {
                    usefulNumStr = (_myHero.heroTalentList[i].Crit).ToString();
                }
                desAndDataStr += "暴击+" + usefulNumStr + " ";
            }
            if (_myHero.heroTalentList[i].NoCrit > 0)
            {
                string usefulNumStr = "";
                if (_myHero.heroTalentList[i].NoCrit < 1 && _myHero.heroTalentList[i].NoCrit > 0)
                {
                    usefulNumStr = ChangeDecimalToPercent(_myHero.heroTalentList[i].NoCrit);
                }
                else
                {
                    usefulNumStr = (_myHero.heroTalentList[i].NoCrit).ToString();
                }
                desAndDataStr += "抗暴+" + usefulNumStr + " ";
            }
            if (_myHero.heroTalentList[i].DmgBonus > 0)
            {
                string usefulNumStr = "";
                if (_myHero.heroTalentList[i].DmgBonus < 1 && _myHero.heroTalentList[i].DmgBonus > 0)
                {
                    usefulNumStr = ChangeDecimalToPercent(_myHero.heroTalentList[i].DmgBonus);
                }
                else
                {
                    usefulNumStr = (_myHero.heroTalentList[i].DmgBonus).ToString();
                }
                desAndDataStr += "伤害加成+" + usefulNumStr + " ";
            }
            if (_myHero.heroTalentList[i].DmgReduction > 0)
            {
                string usefulNumStr = "";
                if (_myHero.heroTalentList[i].DmgReduction < 1 && _myHero.heroTalentList[i].DmgReduction > 0)
                {
                    usefulNumStr = ChangeDecimalToPercent(_myHero.heroTalentList[i].DmgReduction);
                }
                else
                {
                    usefulNumStr = (_myHero.heroTalentList[i].DmgReduction).ToString();
                }
                desAndDataStr += "伤害减免+" + usefulNumStr + " ";
            }
            if (_myHero.heroTalentList[i].Angry > 0)
            {
                string usefulNumStr = "";
                if (_myHero.heroTalentList[i].Angry < 1 && _myHero.heroTalentList[i].Angry > 0)
                {
                    usefulNumStr = ChangeDecimalToPercent(_myHero.heroTalentList[i].Angry);
                }
                else
                {
                    usefulNumStr = (_myHero.heroTalentList[i].Angry).ToString();
                }
                desAndDataStr += "怒气+" + usefulNumStr + " ";
            }
            //talentLabel.text += string.Format("[50e8ff][{0}][ffffff] {1}({2}激活)\n", _myHero.heroTalentList[i].Name, desAndDataStr, GetJunXianName(_myHero.heroTalentList[i].BreachLevel));
            string inactiveColorStr = "[919090]";
            string activeDesColorStr = "[ffffff]";
            string activeNameColorStr = "[50e8ff]";
            if (_myHero.heroTalentList[i].Name != "0")
            {
                if (_myHero.rank >= _myHero.heroTalentList[i].BreachLevel)
                {
                    talentLabel.text += string.Format("{0}[{1}]{2} {3}({4})\n", activeNameColorStr, _myHero.heroTalentList[i].Name, activeDesColorStr, desAndDataStr, GetJunXianName(_myHero.heroTalentList[i].BreachLevel));
                    //talentLabel.text += string.Format("{0}[{1}]{2} {3}({4}激活)\n", activeNameColorStr, _myHero.heroTalentList[i].Name, activeDesColorStr, desAndDataStr, GetJunXianName(_myHero.heroTalentList[i].BreachLevel));
                }
                else
                {
                    talentLabel.text += string.Format("{0}[{1}] {2}({3})\n", inactiveColorStr, _myHero.heroTalentList[i].Name, desAndDataStr, GetJunXianName(_myHero.heroTalentList[i].BreachLevel));
                    //talentLabel.text += string.Format("{0}[{1}] {2}({3}激活)\n", inactiveColorStr, _myHero.heroTalentList[i].Name, desAndDataStr, GetJunXianName(_myHero.heroTalentList[i].BreachLevel));
                }
            }

        }
    }
    //设置英雄背景部分
    void SetBackgroundPart(HeroInfo _heroInfo)
    {
        InForceLabel.text = string.Format("所属势力:{0}", _heroInfo.InForces);
        PetPhraseLabel.text = _heroInfo.heroPetPhrase;
    }
}
