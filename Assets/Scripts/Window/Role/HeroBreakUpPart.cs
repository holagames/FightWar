using UnityEngine;
using System.Collections;

public class HeroBreakUpPart : MonoBehaviour
{

    public UILabel oldPowerNumber;
    public UILabel newPowerNumber;
    public UILabel oldHp;
    public UILabel newHp;
    public UILabel oldAtk;
    public UILabel newAtk;
    public UILabel oldDef;
    public UILabel newDef;
    public GameObject oldRankIcon;
    public GameObject newRankIcon;
    public GameObject oldRankHorizontal;
    public GameObject newRankHorizontal;
    public GameObject jinShengJunXianEffect;
    public UILabel talentLabel;
    public GameObject Item1;
    public GameObject Item2;
    private int Item2Code;
    private int Item2OwnedCount;
    private int Item2NeedCount;

    public UILabel Name;
    public UISprite RarityIcon;
    [SerializeField]
    private UISprite RankIcon;
    public GameObject heroRankHorizontal;
    public UILabel AdviceLabel;
    public GameObject Money;
    public GameObject UpButton;
    public GameObject AllPowerfulButton;
    public GameObject pieceStoneExChangeBoard;
    [SerializeField]
    private GameObject ATKtype;
    [SerializeField]
    private GameObject DEFtype;

    private Hero heroBefore = new Hero();
    Hero mHero;
    HeroInfo mHeroInfo;
    // Use this for initialization
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.军衔);
        UIManager.instance.UpdateSystems(UIManager.Systems.军衔);

        UIEventListener.Get(ATKtype).onPress = ClickAtkOrDefTypeButton;
        UIEventListener.Get(DEFtype).onPress = ClickAtkOrDefTypeButton;
        UIEventListener.Get(Item1).onClick = ClikItemListButton;
        UIEventListener.Get(Item2).onClick = ClikItemListButton;
        UIEventListener.Get(AllPowerfulButton).onClick = delegate(GameObject go)
        {
            pieceStoneExChangeBoard.SetActive(true);
            PieceStoneExChangeBoard.IsAfterChanged = false;
            pieceStoneExChangeBoard.GetComponent<PieceStoneExChangeBoard>().SetPieceStoneExChangeBoardInfo(Item2Code, Item2OwnedCount, Item2NeedCount);
        };
        UIEventListener.Get(UpButton).onClick += delegate(GameObject go)
        {
          /*  if (CharacterRecorder.instance.lastGateID <= 10008)
            {
                UIManager.instance.OpenPromptWindow("打过第8关开启", 11, false, PromptWindow.PromptType.Hint, null, null);
                return;
            }*/
            if (CharacterRecorder.instance.GuideID[37]==4)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
            heroBefore.SetHeroCardID(mHero.cardID);
            heroBefore.SetHeroProperty(mHero.force, mHero.level, mHero.exp, mHero.maxExp, mHero.rank, mHero.rare, mHero.classNumber, mHero.skillLevel, mHero.skillNumber, mHero.HP, mHero.strength, mHero.physicalDefense, mHero.physicalCrit,
                    mHero.UNphysicalCrit, mHero.dodge, mHero.hit, mHero.moreDamige, mHero.avoidDamige, mHero.aspd, mHero.move, mHero.HPAdd, mHero.strengthAdd, mHero.physicalDefenseAdd, mHero.physicalCritAdd, mHero.UNphysicalCritAdd,
                    mHero.dodgeAdd, mHero.hitAdd, mHero.moreDamigeAdd, mHero.avoidDamigeAdd, 0);
            NetworkHandler.instance.SendProcess("1022#" + mHero.characterRoleID + ";");
            CharacterRecorder.instance.IsNeedOpenFight = false;
            Invoke("DelayIsNeedOpenFightTrue",3.0f);
        };
    }
    void DelayIsNeedOpenFightTrue()
    {
        CharacterRecorder.instance.IsNeedOpenFight = true;
    }
    void ClikItemListButton(GameObject go)
    {
        //UIManager.instance.OpenPanel("WayWindow", false);
        UIManager.instance.OpenSinglePanel("WayWindow",false);
        GameObject _WayWindow = GameObject.Find("WayWindow");
        RoleBreach rb = TextTranslator.instance.GetRoleBreachByID(mHero.cardID, mHero.rank);
        int itemCode = 0;
        switch (go.name)
        {
            case "Item1": WayWindow.NeedItemCount = rb.stoneNeed; itemCode = int.Parse(go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName); break;
            case "Item2": WayWindow.NeedItemCount = rb.debrisNum; itemCode = int.Parse(go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName) + 10000; break;
        }
        _WayWindow.GetComponent<WayWindow>().SetWayInfo(itemCode);
        _WayWindow.layer = 11;

        CharacterRecorder.instance.SweptIconID = itemCode;
        CharacterRecorder.instance.SweptIconNum = 0;
        Debug.LogError("升星SweptIconID " + CharacterRecorder.instance.SweptIconID);
        foreach (Component c in _WayWindow.GetComponentsInChildren(typeof(Transform), true))
        {
            c.gameObject.layer = 11;
        }
    }
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
    public void StartPart(Hero _hero)
    {
        mHero = _hero;
        HeroInfo _HeroInfo = TextTranslator.instance.GetHeroInfoByHeroID(mHero.cardID);
        mHeroInfo = _HeroInfo;
        Name.text = _HeroInfo.heroName;
        //SetNameColor();//旧的
        TextTranslator.instance.SetHeroNameColor(Name, mHeroInfo.heroName, mHero.classNumber);
        SetOldState();
        SetAtkOrDefTRype();
        SetRarityIcon();
        SetRankIcon();
        heroRankHorizontal.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(_hero.rank);
        SetTalentInfo(_hero);
        NetworkHandler.instance.SendProcess("1024#" + mHero.characterRoleID + ";");

        

        //人物角色生成
        PictureCreater.instance.DestroyAllComponent();
        int K = PictureCreater.instance.CreateRole(mHero.cardID, "Model", 18, Color.red, 0, 0, 1, 2f, false, false, 1, 1.5f, 0.2f, "Model", 0, 10, 10, 2, 2, 0, 0, 1001, 1002, mHero.WeaponList[0].WeaponClass, 1, mHero.WeaponList[0].WeaponClass, 1, 1, 0, "");
        PictureCreater.instance.ListRolePicture[0].RoleObject.transform.parent = gameObject.transform;
        //角色旋转、播动画
        GameObject _roleWindow = GameObject.Find("RoleWindow");
        if (_roleWindow != null)
        {
            _roleWindow.GetComponent<PlayAniOrRotation>().ResetTargetRole(PictureCreater.instance.ListRolePicture[0].RoleObject.transform.FindChild("Role").gameObject, _hero);
        }
        int modelPositionY = -950;
        if (_hero.cardID == 60201)// 飞机的话|| _hero.cardID == 60200
        {
            modelPositionY = -1200;
        }
        if ((Screen.width * 1f / Screen.height) > 1.7f)
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = new Vector3(6940, modelPositionY, 13339);
        }
        else if ((Screen.width * 1f / Screen.height) < 1.4f)
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = new Vector3(7749 - 636 + 160 - 92 - 341 + 210, modelPositionY, 13339);
        }
        else
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = new Vector3(7649 - 636 + 160 - 92 - 341 + 210, modelPositionY, 13339);
        }
        //PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localScale = new Vector3(450, 450, 450) * TextTranslator.instance.GetHeroInfoByHeroID(PictureCreater.instance.ListRolePicture[0].RoleID).heroScale;
        //PictureCreater.instance.ListRolePicture[0].RoleObject.transform.Rotate(new Vector3(0, 65, 0));
        PictureCreater.instance.ListRolePicture[0].RolePictureObject.transform.Rotate(new Vector3(0, 65, 0));
        PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localScale = new Vector3(400, 400, 400);
    }
    public void SetOldState()
    {
        oldPowerNumber.text = mHero.force.ToString();
        oldHp.text = (mHero.HP + mHero.HPAdd).ToString();
        oldAtk.text = (mHero.strength + mHero.strengthAdd).ToString();
        oldDef.text = (mHero.physicalDefense + mHero.physicalDefenseAdd).ToString();

        //oldRankIcon.transform.Find("Sprite").GetComponent<UISprite>().spriteName = "ui_junxian_icon" + (mHero.rank + 3);
        //newRankIcon.transform.Find("Sprite").GetComponent<UISprite>().spriteName = "ui_junxian_icon" + (mHero.rank + 4);
        oldRankIcon.GetComponent<UISprite>().spriteName = string.Format("rank{0}", (mHero.rank).ToString("00"));
        newRankIcon.GetComponent<UISprite>().spriteName = string.Format("rank{0}", (mHero.rank + 1).ToString("00"));

        oldRankHorizontal.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(mHero.rank);
        newRankHorizontal.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(mHero.rank + 1);

        RoleBreach rb = TextTranslator.instance.GetRoleBreachByID(mHero.cardID, mHero.rank);
        int item1Count = TextTranslator.instance.GetItemCountByID(10102);
        int item2Count = TextTranslator.instance.GetItemCountByID(rb.roleId + 10000);
        string _labelColorStrItem1 = "";
        Item2Code = rb.roleId + 10000;
        if (TextTranslator.instance.GetItemCountByID(10102) == null)
        {
            _labelColorStrItem1 = "[ff0000]";//[fb2d50]
            Item1.transform.FindChild("Mask").gameObject.SetActive(true);
        }
        else
        {
            if (TextTranslator.instance.GetItemCountByID(10102) >= rb.stoneNeed)
            {
                _labelColorStrItem1 = "[3ee817]";
            }
            else
            {
                _labelColorStrItem1 = "[ff0000]";//[fb2d50]
            }
            
            Item1.transform.FindChild("Mask").gameObject.SetActive(false);
        }
        Item1.transform.FindChild("Label").GetComponent<UILabel>().text = string.Format(_labelColorStrItem1 + "{0}[-]/{1}", TextTranslator.instance.GetItemCountByID(10102) != null ? TextTranslator.instance.GetItemCountByID(10102) : 0,
                rb.stoneNeed);
        if (rb.debrisNum == 0)
        {
            Item2.SetActive(false);
            AllPowerfulButton.SetActive(false);
        }
        else
        {
            Item2.SetActive(true);
            AllPowerfulButton.SetActive(true);
            string _labelColorStr = "";
            if (TextTranslator.instance.GetItemCountByID(rb.roleId + 10000) == null)
            {
                _labelColorStr = "[ff0000]";
                Item2.transform.FindChild("Mask").gameObject.SetActive(true);
            }
            else
            {
                if (TextTranslator.instance.GetItemCountByID(rb.roleId + 10000) >= rb.debrisNum)
                {
                    _labelColorStr = "[3ee817]";
                }
                else
                {
                    _labelColorStr = "[ff0000]";
                }
                Item2.transform.FindChild("Mask").gameObject.SetActive(false);
            }
            //Debug.LogError("rb.roleId...." + rb.roleId);
            Item2Code = rb.roleId + 10000;
            Item2OwnedCount = TextTranslator.instance.GetItemCountByID(rb.roleId + 10000);
            Item2NeedCount = rb.debrisNum;
            Item2.GetComponent<UISprite>().spriteName = string.Format("Grade{0}", TextTranslator.instance.GetItemByItemCode(rb.roleId + 10000).itemGrade);//TextTranslator.instance.GetItemByID(rb.roleId + 10000).itemGrade
            Item2.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = rb.roleId.ToString();
            Item2.transform.FindChild("Label").GetComponent<UILabel>().text = string.Format(_labelColorStr + "{0}[-]/{1}", TextTranslator.instance.GetItemCountByID(rb.roleId + 10000) != null ? TextTranslator.instance.GetItemCountByID(rb.roleId + 10000) : 0,
                rb.debrisNum);
        }
        //Debug.LogError(mHero.level + ".....rb.levelCup....." + rb.levelCup);
        UILabel _UpButtonLabel = UpButton.transform.FindChild("Label").GetComponent<UILabel>();
        GameObject _RedPoint = UpButton.transform.FindChild("RedPoint").gameObject;
        if (mHero.level >= rb.levelCup)
        {
            AdviceLabel.gameObject.SetActive(false);
            Money.SetActive(true);
            //Money.transform.FindChild("Label").GetComponent<UILabel>().text = rb.money.ToString();
            UILabel _moneyLabel = Money.transform.FindChild("Label").GetComponent<UILabel>();
            if (CharacterRecorder.instance.gold >= rb.money)
            {
                _moneyLabel.text = rb.money.ToString();
            }
            else
            {
                _moneyLabel.color = Color.white;
                _moneyLabel.text = "[ff0000]" + rb.money.ToString();
            }
            UpButton.GetComponent<UIButton>().isEnabled = true;
            _UpButtonLabel.text = "升 星";
            _UpButtonLabel.fontSize = 31;
            if (item1Count >= rb.stoneNeed && item2Count >= rb.debrisNum)
            {
                _RedPoint.SetActive(true);
            }
            else
            {
                _RedPoint.SetActive(false);
            }
            _UpButtonLabel.effectStyle = UILabel.Effect.Outline;
            _UpButtonLabel.color = Color.white;
        }
        else
        {
            AdviceLabel.gameObject.SetActive(true);
            Money.SetActive(false);
            UpButton.GetComponent<UIButton>().isEnabled = false;
            _UpButtonLabel.text = string.Format("" + rb.levelCup + "级可升星");//达到30级晋升
            _UpButtonLabel.fontSize = 23;
            _RedPoint.SetActive(false);
            _UpButtonLabel.effectStyle = UILabel.Effect.None;
            _UpButtonLabel.color = Color.gray;
        }
    }
    public void UpDataItemStateAfterExChange()
    {
        RoleBreach rb = TextTranslator.instance.GetRoleBreachByID(mHero.cardID, mHero.rank);
        string _labelColorStr = "";
        if (TextTranslator.instance.GetItemCountByID(rb.roleId + 10000) == null)
        {
            _labelColorStr = "[ff0000]";
            Item2.transform.FindChild("Mask").gameObject.SetActive(true);
        }
        else
        {
            if (TextTranslator.instance.GetItemCountByID(rb.roleId + 10000) >= rb.debrisNum)
            {
                _labelColorStr = "[3ee817]";
            }
            else
            {
                _labelColorStr = "[ff0000]";
            }
            Item2.transform.FindChild("Mask").gameObject.SetActive(false);
        }
        Item2OwnedCount = TextTranslator.instance.GetItemCountByID(rb.roleId + 10000);
        Item2.transform.FindChild("Label").GetComponent<UILabel>().text = string.Format(_labelColorStr + "{0}[-]/{1}", TextTranslator.instance.GetItemCountByID(rb.roleId + 10000) != null ? TextTranslator.instance.GetItemCountByID(rb.roleId + 10000) : 0,
            rb.debrisNum);

        Item2NeedCount = rb.debrisNum;
        PieceStoneExChangeBoard.IsAfterChanged  = true;
        pieceStoneExChangeBoard.GetComponent<PieceStoneExChangeBoard>().SetPieceStoneExChangeBoardInfo(Item2Code, Item2OwnedCount, Item2NeedCount);
    }
    public void SetNewState(int _powerNumber, int _hp, int _atk, int _def)
    {
        newPowerNumber.text = (_powerNumber).ToString();
        newHp.text = (_hp).ToString();
        newAtk.text = (_atk).ToString();
        newDef.text = (_def).ToString();
    }
    public void ScuccedJinJieJunXian(HeroNewData _HeroNewData)
    {
        StartCoroutine(OnEquipDataUpEffect(oldPowerNumber));
        StartCoroutine(OnEquipDataUpEffect(oldHp));
        StartCoroutine(OnEquipDataUpEffect(oldAtk));
        StartCoroutine(OnEquipDataUpEffect(oldDef));

     /*   GameObject _jinShengJunXianEffect = NGUITools.AddChild(this.gameObject, jinShengJunXianEffect);
        _jinShengJunXianEffect.transform.localPosition = new Vector3(240, 0, 0);
        _jinShengJunXianEffect.transform.localScale = new Vector3(300, 300, 300);
        _jinShengJunXianEffect.AddComponent<DestroySelf>().DestroyTimer = 2.0f;*/

        UIManager.instance.OpenSinglePanel("AdvanceWindow", false);
        GameObject.Find("AdvanceWindow").layer = 11;
        AdvanceWindow aw = GameObject.Find("AdvanceWindow").GetComponent<AdvanceWindow>();
        aw.SetInfo(AdvanceWindowType.HeroUpJunXian, heroBefore, mHero, null, null, null);
    }
    //设置名字颜色
    void SetNameColor()
    {
        switch (mHero.classNumber)
        {
            case 1:
                Name.color = Color.white;
                break;
            case 2:
                Name.text = "[3ee817]" + Name.text + "[-]";
                break;
            case 3:
                Name.text = "[3ee817]" + Name.text + "+1[-]";
                break;
            case 4:
                Name.text = "[249bd2]" + Name.text + "[-]";
                break;
            case 5:
                Name.text = "[249bd2]" + Name.text + "+1[-]";
                break;
            case 6:
                Name.text = "[249bd2]" + Name.text + "+2[-]";
                break;
            case 7:
                Name.text = "[bb44ff]" + Name.text + "[-]";
                break;
            case 8:
                Name.text = "[bb44ff]" + Name.text + "+1[-]";
                break;
            case 9:
                Name.text = "[bb44ff]" + Name.text + "+2[-]";
                break;
            case 10:
                Name.text = "[bb44ff]" + Name.text + "+3[-]";
                break;
            case 11:
                Name.text = "[ff8c04]" + Name.text + "[-]";
                break;
            case 12:
                Name.text = "[ff8c04]" + Name.text + "+1[-]";
                break;
            default:
                break;
        }
    }
    //设置攻击防御类型
    void SetAtkOrDefTRype()
    {
        ATKtype.GetComponent<UISprite>().spriteName = string.Format("atk{0}", mHeroInfo.heroAtkType);
        DEFtype.GetComponent<UISprite>().spriteName = string.Format("def{0}", mHeroInfo.heroDefType);
        Transform _ATKtypeInfoBoard = ATKtype.transform.FindChild("InfoBoard");
        string ATKtypeName = "";
        string ATKtypeDes = "";
        switch (mHeroInfo.heroAtkType)
        {
            case 1: ATKtypeName = "普通枪械"; ATKtypeDes = "对[ff0000]轻甲[-]造成较多伤害，对[ff0000]建筑[-]伤害很弱。"; break;
            case 2: ATKtypeName = "重型枪械"; ATKtypeDes = "对[ff0000]中甲[-]造成较多伤害，对[ff0000]建筑[-]伤害很弱。"; break;
            case 3: ATKtypeName = "炮弹"; ATKtypeDes = "对[ff0000]重装[-]造成较多伤害，对[ff0000]中甲[-]伤害较弱。"; break;
            case 4: ATKtypeName = "穿甲弹"; ATKtypeDes = "对[ff0000]建筑[-]造成大量伤害，对[ff0000]轻甲[-]伤害较弱。"; break;
        }
        _ATKtypeInfoBoard.FindChild("TypeName").GetComponent<UILabel>().text = ATKtypeName;
        _ATKtypeInfoBoard.FindChild("TypeDes").GetComponent<UILabel>().text = ATKtypeDes;

        Transform _DEFtypeInfoBoard = DEFtype.transform.FindChild("InfoBoard");
        string DEFtypeName = "";
        string DEFtypeDes = "";
        switch (mHeroInfo.heroAtkType)
        {
            case 1: DEFtypeName = "轻甲"; DEFtypeDes = "步兵护甲，防护能力较低。"; break;
            case 2: DEFtypeName = "中甲"; DEFtypeDes = "步兵强化护甲，可有效抵御伤害。"; break;
            case 3: DEFtypeName = "重装"; DEFtypeDes = "机械单位护甲，对普通枪械较强。"; break;
            case 4: DEFtypeName = "建筑"; DEFtypeDes = "强固如城壁的护甲，但是惧怕穿甲弹。"; break;
        }
        _DEFtypeInfoBoard.FindChild("TypeName").GetComponent<UILabel>().text = DEFtypeName;
        _DEFtypeInfoBoard.FindChild("TypeDes").GetComponent<UILabel>().text = DEFtypeDes;
    }
    //设置稀有度
    void SetRarityIcon()
    {
        switch (mHeroInfo.heroRarity)
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
    //数字变大
    public IEnumerator OnEquipDataUpEffect(UILabel _myLabel)
    {
        _myLabel.color = new Color(62 / 255f, 232 / 255f, 23 / 255f, 255 / 255f);
        TweenScale _TweenScale = _myLabel.gameObject.GetComponent<TweenScale>();
        if (_TweenScale == null)
        {
            _TweenScale = _myLabel.gameObject.AddComponent<TweenScale>();
        }
        _TweenScale.from = Vector3.one;
        _TweenScale.to = 1.2f * Vector3.one;
        _TweenScale.duration = 0.3f;
        _TweenScale.PlayForward();
        yield return new WaitForSeconds(0.3f);
        _TweenScale.duration = 0.1f;
        _TweenScale.PlayReverse();
        Color backColor = new Color(116 / 255f, 229 / 255f, 243 / 255f, 255 / 255f);
        _myLabel.color = backColor;
    }

    void SetTalentInfo(Hero _myHero)
    {
        talentLabel.text = "升星后解锁天赋: ";
        //Debug.LogError("_myHero.cardID........" + _myHero.cardID);
        _myHero.heroTalentList = TextTranslator.instance.GetHeroTalentListByHeroID(_myHero.cardID);
        for (int i = 0; i < _myHero.heroTalentList.Count; i++)
        {
            if (mHero.rank + 1 == _myHero.heroTalentList[i].BreachLevel)
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
                    desAndDataStr += "初始怒气+" + usefulNumStr + " ";
                }
                talentLabel.text += string.Format("[74e5f3][{0}][ffffff] {1}({2}激活)", _myHero.heroTalentList[i].Name, desAndDataStr, GetJunXianName(_myHero.heroTalentList[i].BreachLevel));
            }
        }
    }
    string ChangeDecimalToPercent(float _Decimal)
    {
        return string.Format("{0}%", _Decimal * 100);
    }
    //设置军衔
    void SetRankIcon()
    {
        //RankIcon.GetComponent<UISprite>().spriteName = string.Format("rank{0}",(curHero.rank + 1).ToString("00"));
        RankIcon.GetComponent<UISprite>().spriteName = string.Format("rank{0}", mHero.rank.ToString("00"));
        UILabel _myUILabel = RankIcon.transform.FindChild("Label").GetComponent<UILabel>();
        _myUILabel.text = GetJunXianName(mHero.rank);
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
}
