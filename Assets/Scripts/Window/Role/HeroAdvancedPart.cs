using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroAdvancedPart : MonoBehaviour
{

    public GameObject EXPselider;
    public GameObject RankIcon;
    public GameObject heroRankHorizontal;
    public GameObject shengPinEffect;

    public UILabel PowerNumber;
    public UILabel HP;
    public UILabel ATK;
    public UILabel DEF;
    public UILabel Name;
    public UILabel Level;
    public UILabel NeedMoney;
    public GameObject UpButton;
    [SerializeField]
    private UISprite RarityIcon;
    [SerializeField]
    private UISprite heroTypeIcon;
    [SerializeField]
    private GameObject ATKtype;
    [SerializeField]
    private GameObject DEFtype;
    public List<GameObject> UpItemList = new List<GameObject>();


    int CharacterRoleID = 0;
    private Hero heroBefore = new Hero();
    Hero mHero;
    HeroInfo mHeroInfo;
    private GameObject targetRoleObj;
    // Use this for initialization
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.升品);
        UIManager.instance.UpdateSystems(UIManager.Systems.升品);

        UIEventListener.Get(ATKtype).onPress = ClickAtkOrDefTypeButton;
        UIEventListener.Get(DEFtype).onPress = ClickAtkOrDefTypeButton;
        for (int i = 0; i < UpItemList.Count; i++)
        {
            UIEventListener.Get(UpItemList[i]).onClick = ClikItemListButton;
        }
    }
    void ClikItemListButton(GameObject go)
    {
        //UIManager.instance.OpenPanel("WayWindow", false);
        UIManager.instance.OpenSinglePanel("WayWindow", false);
        GameObject _WayWindow = GameObject.Find("WayWindow");
        RoleClassUp rcu = TextTranslator.instance.GetRoleClassUpInfoByID(mHero.cardID, mHero.classNumber);
        for (int i = 0; i < rcu.NeedItemList.Count; i++)
        {
            if (go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName == rcu.NeedItemList[i].itemCode.ToString())
            {
                WayWindow.NeedItemCount = rcu.NeedItemList[i].itemCount;
            }
        }
        _WayWindow.GetComponent<WayWindow>().SetWayInfo(int.Parse(go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName));
        CharacterRecorder.instance.SweptIconID = int.Parse(go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName);
        CharacterRecorder.instance.SweptIconNum = 0;
        _WayWindow.layer = 11;
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
    void ShowItemIconOrAddIconByCode(int _itemCode, UILabel _countLabel, GameObject _mask,int _needCount)
    {
        //_countLabel.text = TextTranslator.instance.GetItemCountByID(_itemCode).ToString();
        if (TextTranslator.instance.GetItemCountByID(_itemCode) >= _needCount)
        {
            _mask.SetActive(false);
        }
        else
        {
            _mask.SetActive(true);
        }
    }
    bool GetRedState(List<Item> classUpList)
    {
        for (int i = 0; i < classUpList.Count; i++)
        {
            int bagItemCount1 = TextTranslator.instance.GetItemCountByID(classUpList[i].itemCode);
            if (bagItemCount1 < classUpList[i].itemCount)
            {
                return false;
            }
        }
        return true;
    }
    void DelayIsNeedOpenFightTrue()
    {
        CharacterRecorder.instance.IsNeedOpenFight = true;
    }
    public void StartPart(int _CharacterRoleID, Hero _hero)
    {
        CharacterRoleID = _CharacterRoleID;
        mHero = _hero;
        HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(mHero.cardID);
        mHeroInfo = hinfo;
        Name.text = mHeroInfo.heroName;

        Level.text = "Lv." + mHero.level.ToString();
        PowerNumber.text = mHero.force.ToString();
        HP.text = mHero.HP.ToString();
        ATK.text = mHero.strength.ToString();
        DEF.text = mHero.physicalDefense.ToString();
        EXPselider.GetComponent<UISlider>().value = (float)mHero.exp / (float)mHero.maxExp;
        SetAtkOrDefTRype();
        SetRankIcon();
        heroRankHorizontal.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(_hero.rank);
        SetRarityIcon();
        SetHeroType(hinfo.heroCarrerType);
        //SetNameColor();//旧的
        TextTranslator.instance.SetHeroNameColor(Name, mHeroInfo.heroName, mHero.classNumber);

        //设置进阶物品
        //Debug.LogError(mHero.cardID + "mHero.classNumbe" + mHero.classNumber);
        RoleClassUp rcu = TextTranslator.instance.GetRoleClassUpInfoByID(mHero.cardID, mHero.classNumber);
        Debug.Log("rcu.." + rcu.ID);
        //NeedMoney.text = rcu.NeedMoney.ToString();
        if (CharacterRecorder.instance.gold >= rcu.NeedMoney)
        {
            NeedMoney.text = rcu.NeedMoney.ToString();
        }
        else
        {
            NeedMoney.color = Color.white;
            NeedMoney.text = "[ff0000]" + rcu.NeedMoney.ToString();
        }
        List<Item> classUpList = new List<Item>();
        for (int j = 0; j < rcu.NeedItemList.Count; j++)
        {
            if (rcu.NeedItemList[j].itemCode != 0)
            {
                classUpList.Add(rcu.NeedItemList[j]);
            }
        }
        for (int i = 0; i < classUpList.Count; i++)
        {
            UpItemList[i].transform.FindChild("Icon").GetComponent<UISprite>().spriteName = classUpList[i].itemCode.ToString();
            UpItemList[i].GetComponent<UISprite>().spriteName = "Grade" + classUpList[i].itemGrade.ToString();
            //Debug.Log(classUpList[i].itemCode);
            int bagItemCount1 = TextTranslator.instance.GetItemCountByID(classUpList[i].itemCode);
            ShowItemIconOrAddIconByCode(classUpList[i].itemCode, UpItemList[i].transform.FindChild("Label").GetComponent<UILabel>(), UpItemList[i].transform.FindChild("Mask").gameObject, classUpList[i].itemCount);
            if (bagItemCount1 >= classUpList[i].itemCount)
            {
                UpItemList[i].transform.FindChild("Label").GetComponent<UILabel>().text = string.Format("[3ee817]{0}[-]/{1}", bagItemCount1, classUpList[i].itemCount);
            }
            else
            {
                UpItemList[i].transform.FindChild("Label").GetComponent<UILabel>().text = string.Format("[ff0000]{0}[-]/{1}", bagItemCount1, classUpList[i].itemCount);//[b53727]
            }

        }
        UILabel _UpButtonLabel = UpButton.transform.FindChild("Label").GetComponent<UILabel>();
        GameObject _RedPoint = UpButton.transform.FindChild("RedPoint").gameObject;
        if (rcu.Levelcap > mHero.level)
        {
            _UpButtonLabel.text = "需要" + rcu.Levelcap + "级";
            _RedPoint.SetActive(false);
            UpButton.GetComponent<UIButton>().isEnabled = false;
            UpButton.transform.FindChild("ButtonLight").gameObject.SetActive(false);
            _UpButtonLabel.effectStyle = UILabel.Effect.None;
            _UpButtonLabel.color = Color.gray;
        }
        else
        {
            _UpButtonLabel.text = "升 品";
            _RedPoint.SetActive(GetRedState(classUpList));
            UpButton.GetComponent<UIButton>().isEnabled = true;
            UpButton.transform.FindChild("ButtonLight").gameObject.SetActive(true);
            _UpButtonLabel.effectStyle = UILabel.Effect.Outline;
            _UpButtonLabel.color = Color.white;
        }

        UIEventListener.Get(UpButton).onClick = delegate(GameObject go)
        {
            if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 9 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 5)
            {
                SceneTransformer.instance.NewGuideButtonClick();
            }
            if (rcu.Levelcap > mHero.level)
            {
                UpButton.transform.FindChild("Label").GetComponent<UILabel>().text = "需要" + rcu.Levelcap + "级";
                //NetworkHandler.instance.SendProcess("1006#" + CharacterRoleID + ";");//测试用，待去掉

            }
            else
            {
                heroBefore.SetHeroCardID(mHero.cardID);
                heroBefore.SetHeroProperty(mHero.force, mHero.level, mHero.exp, mHero.maxExp, mHero.rank, mHero.rare, mHero.classNumber, mHero.skillLevel, mHero.skillNumber, mHero.HP, mHero.strength, mHero.physicalDefense, mHero.physicalCrit,
                    mHero.UNphysicalCrit, mHero.dodge, mHero.hit, mHero.moreDamige, mHero.avoidDamige, mHero.aspd, mHero.move, mHero.HPAdd, mHero.strengthAdd, mHero.physicalDefenseAdd, mHero.physicalCritAdd, mHero.UNphysicalCritAdd,
                    mHero.dodgeAdd, mHero.hitAdd, mHero.moreDamigeAdd, mHero.avoidDamigeAdd, 0);
                UpButton.transform.FindChild("Label").GetComponent<UILabel>().text = "升 品";
                NetworkHandler.instance.SendProcess("1006#" + CharacterRoleID + ";");
                CharacterRecorder.instance.IsNeedOpenFight = false;
                Invoke("DelayIsNeedOpenFightTrue",3.0f);
            }

        };



        //人物角色生成
        PictureCreater.instance.DestroyAllComponent();
        int K = PictureCreater.instance.CreateRole(mHero.cardID, "Model", 18, Color.red, 0, 0, 1, 2f, false, false, 1, 1.5f, 0.2f, "Model", 0, 10, 10, 2, 2, 0, 0, 1001, 1002, mHero.WeaponList[0].WeaponClass, 1, mHero.WeaponList[0].WeaponClass, 1, 1, 0, "");
        PictureCreater.instance.ListRolePicture[0].RoleObject.transform.parent = gameObject.transform;
        targetRoleObj = PictureCreater.instance.ListRolePicture[0].RoleObject;
        //角色旋转、播动画
        GameObject _roleWindow = GameObject.Find("RoleWindow");
        if (_roleWindow != null)
        {
            _roleWindow.GetComponent<PlayAniOrRotation>().ResetTargetRole(PictureCreater.instance.ListRolePicture[0].RoleObject.transform.FindChild("Role").gameObject, _hero);
        }
        int modelPositionY = -660;//-711
        if (_hero.cardID == 60201)// 飞机的话|| _hero.cardID == 60200
        {
            modelPositionY = -1100;
        }
        if ((Screen.width * 1f / Screen.height) > 1.7f)
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = new Vector3(8510 - 636 - 92 - 85 - 341, modelPositionY, 13339);
        }
        else if ((Screen.width * 1f / Screen.height) < 1.4f)
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = new Vector3(8410 - 636 - 92 - 341, modelPositionY, 13339);
        }
        else
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = new Vector3(8310 - 636 - 92 - 341, modelPositionY, 13339);
        }
        //PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localScale = new Vector3(450, 450, 450) * TextTranslator.instance.GetHeroInfoByHeroID(PictureCreater.instance.ListRolePicture[0].RoleID).heroScale;
        //PictureCreater.instance.ListRolePicture[0].RoleObject.transform.Rotate(new Vector3(0, 65, 0));
        PictureCreater.instance.ListRolePicture[0].RolePictureObject.transform.Rotate(new Vector3(0, 65, 0));
        PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localScale = new Vector3(400, 400, 400);
    }

    //更新状态
    public void UpdateState()
    {
        PowerNumber.text = mHero.force.ToString();
        HP.text = mHero.HP.ToString();
        ATK.text = mHero.strength.ToString();
        DEF.text = mHero.physicalDefense.ToString();
        SetRankIcon();
       // SetNameColor();//旧的
        TextTranslator.instance.SetHeroNameColor(Name, mHeroInfo.heroName, mHero.classNumber);

        TopContent tp = GameObject.Find("TopContent").GetComponent<TopContent>();
        tp.Reset();

        RoleClassUp rcu = TextTranslator.instance.GetRoleClassUpInfoByID(mHero.cardID, mHero.classNumber + 1);
        //NeedMoney.text = rcu.NeedMoney.ToString();
        if (CharacterRecorder.instance.gold >= rcu.NeedMoney)
        {
            NeedMoney.text = rcu.NeedMoney.ToString();
        }
        else
        {
            NeedMoney.color = Color.white;
            NeedMoney.text = "[ff0000]" + rcu.NeedMoney.ToString();
        }
        List<Item> classUpList = new List<Item>();
        for (int j = 0; j < rcu.NeedItemList.Count; j++)
        {
            if (rcu.NeedItemList[j].itemCode != 0)
            {
                classUpList.Add(rcu.NeedItemList[j]);
            }
        }
        for (int i = 0; i < classUpList.Count; i++)
        {
            UpItemList[i].transform.FindChild("Icon").GetComponent<UISprite>().spriteName = classUpList[i].itemCode.ToString();
            int bagItemCount1 = TextTranslator.instance.GetItemCountByID(classUpList[i].itemCode);
            ShowItemIconOrAddIconByCode(classUpList[i].itemCode, UpItemList[i].transform.FindChild("Label").GetComponent<UILabel>(), UpItemList[i].transform.FindChild("Mask").gameObject, classUpList[i].itemCount);
            if (bagItemCount1 >= classUpList[i].itemCount)
            {
                UpItemList[i].transform.FindChild("Label").GetComponent<UILabel>().text = string.Format("[3d7483]{0}/{1}", bagItemCount1, classUpList[1].itemCount);
            }
            else
            {
                UpItemList[i].transform.FindChild("Label").GetComponent<UILabel>().text = string.Format("[b53727]{0}/{1}", bagItemCount1, classUpList[1].itemCount);
            }

        }

        if (rcu.Levelcap > mHero.level)
        {
            UpButton.transform.FindChild("Label").GetComponent<UILabel>().text = "需要" + rcu.Levelcap + "级";
            UpButton.GetComponent<UIButton>().isEnabled = false;
            UpButton.transform.FindChild("ButtonLight").gameObject.SetActive(false);
        }
        else
        {
            UpButton.transform.FindChild("Label").GetComponent<UILabel>().text = "升 品";
            UpButton.GetComponent<UIButton>().isEnabled = true;
            UpButton.transform.FindChild("ButtonLight").gameObject.SetActive(true);
        }

        RoleWindow rw = GameObject.Find("RoleWindow").GetComponent<RoleWindow>();
        rw.UpDateDownHeroIcon();

        //升品特效
        /*  UIManager.instance.OpenSinglePanel("AdvanceWindow", false);
          GameObject.Find("AdvanceWindow").layer = 11;
          AdvanceWindow aw = GameObject.Find("AdvanceWindow").GetComponent<AdvanceWindow>();
          Debug.LogError("beforeHero..2..." + beforeHero.force);
          aw.SetInfo(beforeHero,mHero);*/
        // ScuccedShengPinEffect();
    }
    public void PlayShengPinEffect(HeroNewData heroNewData)
    {
        StartCoroutine(OnEquipDataUpEffect(PowerNumber));
        StartCoroutine(OnEquipDataUpEffect(HP));
        StartCoroutine(OnEquipDataUpEffect(ATK));
        StartCoroutine(OnEquipDataUpEffect(DEF));

        StartCoroutine(ScuccedShengPinEffect());
        UIManager.instance.OpenSinglePanel("AdvanceWindow", false);
        GameObject.Find("AdvanceWindow").layer = 11;
        AdvanceWindow aw = GameObject.Find("AdvanceWindow").GetComponent<AdvanceWindow>();
        //Debug.LogError("heroBefore..." + heroBefore.force);
        aw.SetInfo(AdvanceWindowType.HeroShengPin, heroBefore, mHero, null, null, null);
    }
    IEnumerator ScuccedShengPinEffect()
    {
       /* GameObject _shengPinEffect = NGUITools.AddChild(this.gameObject, shengPinEffect);
        _shengPinEffect.transform.localPosition = new Vector3(0, 0, -10);
        _shengPinEffect.transform.localScale = new Vector3(200, 200, 200);*/
        yield return new WaitForSeconds(2.0f);
        GameObject go = GameObject.Instantiate(Resources.Load("Prefab/Effect/JueSeShengPin", typeof(GameObject)), PictureCreater.instance.ListRolePicture[0].RoleObject.transform.position, Quaternion.identity) as GameObject;
        go.transform.parent = targetRoleObj.transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localEulerAngles = Vector3.zero;
        go.transform.localScale = Vector3.one;
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
    //设置军衔
    void SetRankIcon()
    {
        //RankIcon.GetComponent<UISprite>().spriteName = string.Format("rank{0}",(mHero.rank + 1).ToString("00"));
        RankIcon.GetComponent<UISprite>().spriteName = string.Format("rank{0}", mHero.rank.ToString("00"));
        UILabel _myUILabel = RankIcon.transform.FindChild("Label").GetComponent<UILabel>();
        switch (mHero.rank)
        {
            case 1:
                _myUILabel.text = "下士";
                break;
            case 2:
                _myUILabel.text = "中士";
                break;
            case 3:
                _myUILabel.text = "上士";
                break;
            case 4:
                _myUILabel.text = "少尉";
                break;
            case 5:
                _myUILabel.text = "中尉";
                break;
            case 6:
                _myUILabel.text = "上尉";
                break;
            case 7:
                _myUILabel.text = "少校";
                break;
            case 8:
                _myUILabel.text = "中校";
                break;
            case 9:
                _myUILabel.text = "上校";
                break;
            case 10:
                _myUILabel.text = "少将";
                break;
            case 11:
                _myUILabel.text = "中将";
                break;
            case 12:
                _myUILabel.text = "上将";
                break;
            default:
                break;
        }
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
        Color backColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        _myLabel.color = backColor;
    }
}
