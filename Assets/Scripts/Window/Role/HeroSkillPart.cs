using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroSkillPart : MonoBehaviour
{
    //-------特效父物体相关
    public GameObject brainObj;
    public GameObject pressObj;
    public UISprite spriteAnimation;
    public UISprite bloodSprite;
    public UISprite bloodSpriteBold;
    //------- 左边技能
    public GameObject skillInfoBoard;
    public GameObject skill2InfoBoard;
    public UILabel SkillDes1;
    public UITexture SkillIcon1;
    public UILabel SkillLevel1;
    public UILabel SkillDes2;
    public UITexture SkillIcon2;

    public UISlider SkillSlider;
    public UILabel SkillSliderLabel;
    public UILabel SkillPointNumber;

    public UILabel stoneCostLabel;
    public UILabel SkillChanceLabel;
    //--------skill信息
    public UILabel OldHP;
    public UILabel OldATK;
    public UILabel OldDEF;
    public UILabel OldSkillName;
    public UILabel OldSkillDes;
    public UILabel OldSkillLevel;

    public UILabel NewHP;
    public UILabel NewATK;
    public UILabel NewDEF;
    public UILabel NewSkillName;
    public UILabel NewSkillDes;
    public UILabel NewSkillLevel;

    public GameObject newSkillInfoObj;
    public GameObject maxDesObj;
    //----------添加按钮
    public GameObject AddButton;

    public GameObject AddCountLabel;
    //----------脑白金按钮
    public GameObject powerItemButton;

    public GameObject leftGrid;
    public GameObject rightGrid;
    //----------数据
    Hero mHero;
    HeroInfo mHeroInfo;
    int CharacterRoleID = 0;
    bool isOnPress = false;
    bool isUpGrade = false;
    int StoneCost = 0;
    float timer = 0;
    private float curSliderValue;
    private float newSliderValue;
    [SerializeField]
    private List<GameObject> ListTabs;
    private bool _isWaiting = false;

    private bool isHight = false;
    // Use this for initialization
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.技能);
        UIManager.instance.UpdateSystems(UIManager.Systems.技能);
        //初始化技能升级网格的
        //for (int i = 0; i < leftGrid.transform.childCount; i++)
        //{
        //    leftGrid.transform.GetChild(i).gameObject.SetActive(false);
        //}
        //for (int i = 0; i < rightGrid.transform.childCount; i++)
        //{
        //    rightGrid.transform.GetChild(i).gameObject.SetActive(false);
        //}

        //spriteAnimation.GetComponent<UISpriteAnimation>().enabled = false;
        //bloodSpriteBold.gameObject.SetActive(false);
        if (CharacterRecorder.instance.lastGateID - 1 < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.jinengtupo).Level)
        {
            AddButton.transform.Find("Sprite").GetComponent<UISprite>().color = Color.gray;
            AddButton.transform.Find("Shiyong").GetComponent<UISprite>().color = Color.gray;
            AddCountLabel.transform.Find("Sprite").GetComponent<UISprite>().color = Color.gray;
            stoneCostLabel.color = Color.gray;
        }
        //角色旋转、播动画置空
        GameObject _roleWindow = GameObject.Find("RoleWindow");
        if (_roleWindow != null)
        {
            _roleWindow.GetComponent<PlayAniOrRotation>().ResetTargetRole(null, null);
        }
        if (UIEventListener.Get(AddButton).onClick == null)
        {
            UIEventListener.Get(AddButton).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.lastGateID - 1 < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.jinengtupo).Level)
                {
                    UIManager.instance.OpenPromptWindow(string.Format("{0}关开放技能突破", TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.jinengtupo).Level - 10000), 11, false, PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                StoneCost = TextTranslator.instance.GetRoleDestinyCostByID(mHero.skillLevel).StoneCost;
                RoleDestiny Old_rd = TextTranslator.instance.GetRoleDestinyByID(mHero.cardID, mHero.skillLevel);
                if (int.Parse(SkillPointNumber.text) >= StoneCost && CharacterRecorder.instance.level >= Old_rd.NeedLevel && !isUpGrade)
                {
                    TextTranslator.instance.heroBefore.SetHeroCardID(mHero.cardID);
                    TextTranslator.instance.heroBefore.SetHeroProperty(mHero.force, mHero.level, mHero.exp, mHero.maxExp, mHero.rank, mHero.rare, mHero.classNumber, mHero.skillLevel, mHero.skillNumber, mHero.HP, mHero.strength, mHero.physicalDefense, mHero.physicalCrit,
                        mHero.UNphysicalCrit, mHero.dodge, mHero.hit, mHero.moreDamige, mHero.avoidDamige, mHero.aspd, mHero.move, mHero.HPAdd, mHero.strengthAdd, mHero.physicalDefenseAdd, mHero.physicalCritAdd, mHero.UNphysicalCritAdd,
                        mHero.dodgeAdd, mHero.hitAdd, mHero.moreDamigeAdd, mHero.avoidDamigeAdd, 0);
                    NetworkHandler.instance.SendProcess("1010#" + CharacterRoleID + ";" + StoneCost + ";");
                    // StartCoroutine(SccedAddValueEffect());
                    CharacterRecorder.instance.IsNeedOpenFight = false;
                    Invoke("DelayIsNeedOpenFightTrue", 3.0f);
                }
                else if (int.Parse(SkillPointNumber.text) < StoneCost)
                {
                    UIManager.instance.OpenPromptWindow("脑·白金 不足", PromptWindow.PromptType.Hint, null, null);
                }
                else if (CharacterRecorder.instance.level < Old_rd.NeedLevel)
                {
                    UIManager.instance.OpenPromptWindow(string.Format("战队等级需达到{0}级", Old_rd.NeedLevel), PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        if (UIEventListener.Get(AddButton).onPress == null)
        {
            UIEventListener.Get(AddButton).onPress += delegate(GameObject go, bool isPress)
            {
                if (isPress)
                {
                    TextTranslator.instance.heroBefore.SetHeroCardID(mHero.cardID);
                    TextTranslator.instance.heroBefore.SetHeroProperty(mHero.force, mHero.level, mHero.exp, mHero.maxExp, mHero.rank, mHero.rare, mHero.classNumber, mHero.skillLevel, mHero.skillNumber, mHero.HP, mHero.strength, mHero.physicalDefense, mHero.physicalCrit,
                        mHero.UNphysicalCrit, mHero.dodge, mHero.hit, mHero.moreDamige, mHero.avoidDamige, mHero.aspd, mHero.move, mHero.HPAdd, mHero.strengthAdd, mHero.physicalDefenseAdd, mHero.physicalCritAdd, mHero.UNphysicalCritAdd,
                        mHero.dodgeAdd, mHero.hitAdd, mHero.moreDamigeAdd, mHero.avoidDamigeAdd, 0);
                    isOnPress = isPress;
                    // StartCoroutine(SccedAddValueEffect());
                }
                else
                {
                    isOnPress = isPress;
                    CharacterRecorder.instance.IsNeedOpenFight = true;
                    isUpGrade = false;
                }
            };
        }
        if (UIEventListener.Get(SkillIcon1.gameObject).onClick == null)
        {
            UIEventListener.Get(SkillIcon1.gameObject).onClick += delegate(GameObject go)
            {
                skillInfoBoard.SetActive(true);
                skillInfoBoard.GetComponent<SkillInfoBoard>().SetSkillInfoBoard(mHeroInfo, mHero);
            };
        }
        if (UIEventListener.Get(SkillIcon2.gameObject).onClick == null)
        {
            UIEventListener.Get(SkillIcon2.gameObject).onClick += delegate(GameObject go)
            {
                skill2InfoBoard.SetActive(true);
                skill2InfoBoard.GetComponent<Skill2InfoBoard>().SetSkillInfoBoard(mHeroInfo);
            };
        }

        UIEventListener.Get(powerItemButton).onClick = ClikPowerItemButton;
    }
    void DelayIsNeedOpenFightTrue()
    {
        CharacterRecorder.instance.IsNeedOpenFight = true;
    }
    void Update()
    {
        if (isOnPress)
        {
            /*  if (SkillSlider.value >= 0.5)
              {
                  SkillChanceLabel.text = "高概率直接升级";
              }
              else
              {
                  SkillChanceLabel.text = "低概率直接升级";
              }*/
            timer += Time.deltaTime;
            if (_isWaiting && timer < 1f)
            {
                return;
            }
            if (timer >= 0.2f)
            {
                /*if (int.Parse(SkillPointNumber.text) >= StoneCost)
                {
                    NetworkHandler.instance.SendProcess("1010#" + CharacterRoleID + ";" + StoneCost + ";");
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("脑·白金 不足", PromptWindow.PromptType.Hint, null, null);
                }*/
                if (CharacterRecorder.instance.lastGateID <= 10078)
                {
                    UIManager.instance.OpenPromptWindow(string.Format("{0}关开放技能突破", 78), 11, false, PromptWindow.PromptType.Hint, null, null);
                    timer = -999999;
                    return;
                }
                RoleDestiny Old_rd = TextTranslator.instance.GetRoleDestinyByID(mHero.cardID, mHero.skillLevel);
                if (int.Parse(SkillPointNumber.text) >= StoneCost && CharacterRecorder.instance.level >= Old_rd.NeedLevel && !isUpGrade)
                {
                    _isWaiting = true;
                    NetworkHandler.instance.SendProcess("1010#" + CharacterRoleID + ";" + StoneCost + ";");
                    if (CharacterRecorder.instance.IsNeedOpenFight)
                    {
                        CharacterRecorder.instance.IsNeedOpenFight = false;
                    }
                }
                else if (int.Parse(SkillPointNumber.text) < StoneCost)
                {
                    UIManager.instance.OpenPromptWindow("脑·白金 不足", PromptWindow.PromptType.Hint, null, null);
                }
                else if (CharacterRecorder.instance.level < Old_rd.NeedLevel)
                {
                    UIManager.instance.OpenPromptWindow(string.Format("战队等级需达到{0}级", Old_rd.NeedLevel), PromptWindow.PromptType.Hint, null, null);
                }
                timer = 0;
            }
        }
    }
    void ClikPowerItemButton(GameObject go)
    {
        //UIManager.instance.OpenPanel("WayWindow", false);
        UIManager.instance.OpenSinglePanel("WayWindow", false);
        GameObject _WayWindow = GameObject.Find("WayWindow");
        int itemCode = 0;
        switch (go.name)
        {
            case "PowerItem": WayWindow.NeedItemCount = StoneCost; itemCode = 10101; break;
        }
        _WayWindow.GetComponent<WayWindow>().SetWayInfo(itemCode);
        _WayWindow.layer = 11;
        foreach (Component c in _WayWindow.GetComponentsInChildren(typeof(Transform), true))
        {
            c.gameObject.layer = 11;
        }
    }
    public void StartPart(Hero _hero)
    {
        StoneCost = 0;
        PictureCreater.instance.DestroyAllComponent();
        mHero = _hero;
        mHeroInfo = TextTranslator.instance.GetHeroInfoByHeroID(mHero.cardID);
        CharacterRoleID = mHero.characterRoleID;
        NetworkHandler.instance.SendProcess("1010#" + CharacterRoleID + ";0;");  //kino预设
        //NetworkHandler.instance.SendProcess("1010#" + CharacterRoleID + ";" + StoneCost + ";");
        SkillPointNumber.text = TextTranslator.instance.GetItemCountByID(10101) == null ? "0" : TextTranslator.instance.GetItemCountByID(10101).ToString();

        //SkillPointNumber.transform.parent.localPosition = new Vector3(383.7f - SkillPointNumber.transform.GetComponent<UILabel>().localSize.x / 4, SkillPointNumber.transform.parent.localPosition.y, SkillPointNumber.transform.parent.localPosition.z);


        //SkillIcon1.spriteName = TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[0], mHero.skillLevel).skillID.ToString();
        //SkillIcon2.spriteName = TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[1], 1).skillID.ToString();
        SkillIcon1.mainTexture = Resources.Load(string.Format("Skill/{0}", TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[0], mHero.skillLevel).skillID), typeof(Texture)) as Texture;
        SkillIcon2.mainTexture = Resources.Load(string.Format("Skill/{0}", TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[1], 1).skillID), typeof(Texture)) as Texture;
        SkillDes1.text = TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[0], mHero.skillLevel).description;
        SkillLevel1.text = string.Format("Lv.{0}", mHero.skillLevel);
        SkillDes2.text = TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[1], 1).description;

        RoleDestiny Old_rd = TextTranslator.instance.GetRoleDestinyByID(mHero.cardID, mHero.skillLevel);
        RoleDestinyCost _RoleDestinyCost = TextTranslator.instance.GetRoleDestinyCostByID(mHero.skillLevel);
        //SkillCostNumber.text = Old_rd.StoneCost.ToString();
        //StoneCost = Old_rd.StoneCost;
        curSliderValue = (float)mHero.skillNumber / (float)_RoleDestinyCost.StoneMax;//Old_rd.MaxExp;
        SkillSlider.value = (float)mHero.skillNumber / (float)_RoleDestinyCost.StoneMax;//Old_rd.MaxExp;
        int count = Mathf.FloorToInt(SkillSlider.value / 0.04f);
        int leftCount = leftGrid.transform.childCount;
        for (int i = 0; i < leftCount; i++)
        {
            if (i < count)
            {
                leftGrid.transform.GetChild(leftCount - 1 - i).gameObject.SetActive(true);
                continue;
            }
            leftGrid.transform.GetChild(leftCount - 1 - i).gameObject.SetActive(false);
        }
        int rightCount = rightGrid.transform.childCount;
        for (int i = 0; i < rightCount; i++)
        {
            if (i < count)
            {
                rightGrid.transform.GetChild(rightCount - 1 - i).gameObject.SetActive(true);
                continue;
            }
            rightGrid.transform.GetChild(rightCount - 1 - i).gameObject.SetActive(false);
        }

        SkillSliderLabel.text = mHero.skillNumber + "/" + _RoleDestinyCost.StoneMax;// Old_rd.MaxExp;

        OnSliderValueChanged();

        if (SkillSlider.value >= 0.5)
        {
            SkillChanceLabel.text = "高概率";
            isHight = true;
        }
        else
        {
            SkillChanceLabel.text = "低概率";
            isHight = false;
        }
        //--------Skill数据
        Skill _curSkill = TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[0], mHero.skillLevel);
        Skill _nextSkill = TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[0], mHero.skillLevel + 1);
        if (_nextSkill == null)//已达最大限
        {
            newSkillInfoObj.SetActive(false);
            maxDesObj.SetActive(true);
            OldHP.text = "+" + Old_rd.HP * 100 + "%";
            OldATK.text = "+" + Old_rd.ATK * 100 + "%";
            OldDEF.text = "+" + Old_rd.DEF * 100 + "%";

            OldSkillName.text = string.Format("【{0}】{1}级", _curSkill.skillName, mHero.skillLevel);
            OldSkillLevel.text = string.Format("Lv.{0}", mHero.skillLevel);
            OldSkillDes.text = _curSkill.description;
        }
        //未达最大线
        else
        {
            newSkillInfoObj.SetActive(true);
            maxDesObj.SetActive(false);
            OldHP.text = "+" + Old_rd.HP * 100 + "%";
            OldATK.text = "+" + Old_rd.ATK * 100 + "%";
            OldDEF.text = "+" + Old_rd.DEF * 100 + "%";

            OldSkillName.text = string.Format("【{0}】{1}级", _curSkill.skillName, mHero.skillLevel);
            OldSkillLevel.text = string.Format("Lv.{0}", mHero.skillLevel);
            OldSkillDes.text = _curSkill.description;

            if (TextTranslator.instance.GetRoleDestinyByID(mHero.cardID, mHero.skillLevel + 1) != null)
            {
                RoleDestiny New_rd = TextTranslator.instance.GetRoleDestinyByID(mHero.cardID, mHero.skillLevel + 1);
                //Debug.LogError(New_rd);

                NewHP.text = "+" + New_rd.HP * 100 + "%";
                NewATK.text = "+" + New_rd.ATK * 100 + "%";
                NewDEF.text = "+" + New_rd.DEF * 100 + "%";
                NewSkillName.text = string.Format("【{0}】{1}级", _nextSkill.skillName, mHero.skillLevel + 1);
                NewSkillLevel.text = string.Format("Lv.{0}", mHero.skillLevel + 1);
                NewSkillDes.text = _nextSkill.description;
            }
        }
        //Debug.LogError(mHero.skillLevel);
        StoneCost = TextTranslator.instance.GetRoleDestinyCostByID(mHero.skillLevel).StoneCost;
        //Debug.LogError(StoneCost);
        stoneCostLabel.text = StoneCost.ToString();
    }
    void ShowOldAndNewSkillInfo(Hero _hero)
    {

    }
    void OpenAdvanceWindow()
    {
        UIManager.instance.OpenSinglePanel("AdvanceWindow", false);
        GameObject.Find("AdvanceWindow").layer = 11;
        AdvanceWindow aw = GameObject.Find("AdvanceWindow").GetComponent<AdvanceWindow>();
        aw.SetInfo(AdvanceWindowType.HeroSkillUp, mHero, null, null, null, null);
    }
    IEnumerator SccedAddValueEffect(int _SkillNumber, int _SkillLevel)
    {
        TweenPosition _TweenPosition = pressObj.GetComponent<TweenPosition>();
        _TweenPosition.from = new Vector3(152 - (152 - 110) * curSliderValue, 0, 0);
        _TweenPosition.to = new Vector3(152 - (152 - 110) * newSliderValue, 0, 0);
        _TweenPosition.duration = 0.2f;
        _TweenPosition.ResetToBeginning();
        _TweenPosition.PlayForward();
        curSliderValue = newSliderValue;

        spriteAnimation.GetComponent<UISpriteAnimation>().enabled = true;
        //yield return new WaitForSeconds(1.0f);

        //spriteAnimation.GetComponent<UISpriteAnimation>().enabled = false;
        _TweenPosition.duration = 0.1f;
        //_TweenPosition.PlayReverse();

        bloodSprite.spriteName = "blood3";
        TweenAlpha _TweenAlpha = bloodSprite.gameObject.GetComponent<TweenAlpha>();
        _TweenAlpha.enabled = true;
        _TweenAlpha.from = 0f;
        _TweenAlpha.to = 1.0f;
        _TweenAlpha.duration = 0.2f;
        _TweenAlpha.ResetToBeginning();
        _TweenAlpha.PlayForward();
        //bloodSpriteBold.gameObject.SetActive(true);
        if (isHight)
        {
            StartCoroutine(PlayBrainEffect());
        }
        //StartCoroutine(PlayBrainEffect());

        yield return new WaitForSeconds(1.0f);
        bloodSprite.alpha = 1.0f;
        bloodSprite.spriteName = "blood1";
        _TweenAlpha.enabled = false;
        //bloodSpriteBold.gameObject.SetActive(false);
    }
    IEnumerator PlayBrainEffect()
    {
        AudioEditer.instance.PlayOneShot("electricboom");
        GameObject _effect = GameObject.Instantiate(Resources.Load("Prefab/Effect/brain_ui", typeof(GameObject))) as GameObject;
        _effect.transform.parent = brainObj.transform;
        _effect.transform.localScale = Vector3.one;
        _effect.transform.localPosition = new Vector3(-60.0f, 0, 0);
        yield return new WaitForSeconds(1.0f);
    }
    IEnumerator SccedAddValueEffect()
    {
        TweenPosition _TweenPosition = pressObj.GetComponent<TweenPosition>();
        _TweenPosition.from = new Vector3(152, 0, 0);
        _TweenPosition.to = new Vector3(110, 0, 0);
        _TweenPosition.duration = 0.5f;
        _TweenPosition.ResetToBeginning();
        _TweenPosition.PlayForward();

        spriteAnimation.GetComponent<UISpriteAnimation>().enabled = true;
        yield return new WaitForSeconds(1.0f);

        //spriteAnimation.GetComponent<UISpriteAnimation>().enabled = false;
        _TweenPosition.duration = 0.1f;
        _TweenPosition.PlayReverse();

        //yield return new WaitForSeconds(1.0f);
        bloodSprite.spriteName = "blood3";
        TweenAlpha _TweenAlpha = bloodSprite.gameObject.GetComponent<TweenAlpha>();
        _TweenAlpha.enabled = true;
        _TweenAlpha.from = 0f;
        _TweenAlpha.to = 1.0f;
        _TweenAlpha.duration = 0.2f;
        _TweenAlpha.ResetToBeginning();
        _TweenAlpha.PlayForward();
        //bloodSpriteBold.gameObject.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        bloodSprite.alpha = 1.0f;
        bloodSprite.spriteName = "blood1";
        _TweenAlpha.enabled = false;
        //bloodSpriteBold.gameObject.SetActive(false);
    }
    public void TweenAlphaOnFinished()
    {
        bloodSprite.alpha = 1.0f;
    }
    IEnumerator RealSccucedAddValueEffect()
    {
        yield return new WaitForSeconds(1.0f);
        GameObject _effect = GameObject.Instantiate(Resources.Load("Prefab/Effect/brain_ui", typeof(GameObject))) as GameObject;
        _effect.transform.parent = brainObj.transform;
        _effect.transform.localScale = Vector3.one;
        _effect.transform.localPosition = new Vector3(-60.0f, 0, 0);
    }
    void DelayInitIsUpGradeValue()
    {
        isUpGrade = false;
    }
    public void UpDateState(bool _isUpGrade, int _SkillNumber, int _SkillLevel, int _clickNum)
    {
        /*if (StoneCost > 0)
        {
            StartCoroutine(RealSccucedAddValueEffect());
        }*/
        _isWaiting = false;
        isUpGrade = _isUpGrade;
        if (_isUpGrade)
        {
            mHero.skillLevel = _SkillLevel;
            OpenAdvanceWindow();
        }
        Debug.Log(_SkillNumber + "..............." + _SkillLevel);
        SkillPointNumber.text = TextTranslator.instance.GetItemCountByID(10101) == null ? "0" : TextTranslator.instance.GetItemCountByID(10101).ToString();
        //SkillPointNumber.transform.parent.localPosition = new Vector3(383.7f - SkillPointNumber.transform.GetComponent<UILabel>().localSize.x / 4, SkillPointNumber.transform.parent.localPosition.y, SkillPointNumber.transform.parent.localPosition.z);
        if (_SkillLevel > 0)
        {
            mHero.skillLevel = _SkillLevel;
            SkillPointNumber.text =TextTranslator.instance.GetItemCountByID(10101) == null ? "0" : TextTranslator.instance.GetItemCountByID(10101).ToString();
            //SkillPointNumber.transform.parent.localPosition = new Vector3(383.7f - SkillPointNumber.transform.GetComponent<UILabel>().localSize.x / 4, SkillPointNumber.transform.parent.localPosition.y, SkillPointNumber.transform.parent.localPosition.z);
            Debug.Log(TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[0], _SkillLevel));
            SkillDes1.text = TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[0], _SkillLevel).description;
            SkillLevel1.text = string.Format("Lv.{0}", _SkillLevel);
            Debug.Log(mHeroInfo.heroSkillList[1]);
            Debug.Log(TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[1], 1));
            SkillDes2.text = TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[1], 1).description;

            RoleDestiny Old_rd = TextTranslator.instance.GetRoleDestinyByID(mHero.cardID, _SkillLevel);
            RoleDestinyCost _RoleDestinyCost = TextTranslator.instance.GetRoleDestinyCostByID(mHero.skillLevel);
            Debug.Log(Old_rd);
            //SkillCostNumber.text = Old_rd.StoneCost.ToString();
            //StoneCost = Old_rd.StoneCost;
            SkillSlider.value = 0;
            int count = Mathf.FloorToInt(SkillSlider.value / 0.04f);
            int leftCount = leftGrid.transform.childCount;
            for (int i = 0; i < leftCount; i++)
            {
                if (i < count)
                {
                    leftGrid.transform.GetChild(leftCount - 1 - i).gameObject.SetActive(true);
                    continue;
                }
                leftGrid.transform.GetChild(leftCount - 1 - i).gameObject.SetActive(false);
            }
            int rightCount = rightGrid.transform.childCount;
            for (int i = 0; i < rightCount; i++)
            {
                if (i < count)
                {
                    rightGrid.transform.GetChild(rightCount - 1 - i).gameObject.SetActive(true);
                    continue;
                }
                rightGrid.transform.GetChild(rightCount - 1 - i).gameObject.SetActive(false);
            }
            SkillSliderLabel.text = "0/" + _RoleDestinyCost.StoneMax;//Old_rd.MaxExp;


            //--------Skill数据
            Skill _curSkill = TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[0], mHero.skillLevel);
            Skill _nextSkill = TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[0], mHero.skillLevel + 1);
            if (_nextSkill == null)//已达最大限
            {
                newSkillInfoObj.SetActive(false);
                maxDesObj.SetActive(true);
                OldHP.text = "+" + Old_rd.HP * 100 + "%";
                OldATK.text = "+" + Old_rd.ATK * 100 + "%";
                OldDEF.text = "+" + Old_rd.DEF * 100 + "%";

                OldSkillName.text = string.Format("【{0}】{1}级", _curSkill.skillName, mHero.skillLevel);
                OldSkillDes.text = _curSkill.description;
            }
            //未达最大线
            else
            {
                newSkillInfoObj.SetActive(true);
                maxDesObj.SetActive(false);
                OldHP.text = "+" + Old_rd.HP * 100 + "%";
                OldATK.text = "+" + Old_rd.ATK * 100 + "%";
                OldDEF.text = "+" + Old_rd.DEF * 100 + "%";

                OldSkillName.text = string.Format("【{0}】{1}级", _curSkill.skillName, mHero.skillLevel);
                OldSkillDes.text = _curSkill.description;

                if (TextTranslator.instance.GetRoleDestinyByID(mHero.cardID, mHero.skillLevel + 1) != null)
                {
                    RoleDestiny New_rd = TextTranslator.instance.GetRoleDestinyByID(mHero.cardID, mHero.skillLevel + 1);
                    //Debug.LogError(New_rd);

                    NewHP.text = "+" + New_rd.HP * 100 + "%";
                    NewATK.text = "+" + New_rd.ATK * 100 + "%";
                    NewDEF.text = "+" + New_rd.DEF * 100 + "%";
                    NewSkillName.text = string.Format("【{0}】{1}级", _nextSkill.skillName, mHero.skillLevel + 1);
                    NewSkillDes.text = _nextSkill.description;
                }
            }
            SkillChanceLabel.text = "低概率";

            isOnPress = false;

        }
        else
        {
            RoleDestiny Old_rd = TextTranslator.instance.GetRoleDestinyByID(mHero.cardID, mHero.skillLevel);
            RoleDestinyCost _RoleDestinyCost = TextTranslator.instance.GetRoleDestinyCostByID(mHero.skillLevel);
            mHero.skillNumber = _SkillNumber;
            SkillSlider.value = (float)_SkillNumber / (float)_RoleDestinyCost.StoneMax;//Old_rd.MaxExp;
            int count = Mathf.FloorToInt(SkillSlider.value / 0.04f);
            int leftCount = leftGrid.transform.childCount;
            for (int i = 0; i < leftCount; i++)
            {
                if (i < count)
                {
                    leftGrid.transform.GetChild(leftCount - 1 - i).gameObject.SetActive(true);
                    continue;
                }
                leftGrid.transform.GetChild(leftCount - 1 - i).gameObject.SetActive(false);
            }
            int rightCount = rightGrid.transform.childCount;
            for (int i = 0; i < rightCount; i++)
            {
                if (i < count)
                {
                    rightGrid.transform.GetChild(rightCount - 1 - i).gameObject.SetActive(true);
                    continue;
                }
                rightGrid.transform.GetChild(rightCount - 1 - i).gameObject.SetActive(false);
            }
            SkillSliderLabel.text = _SkillNumber + "/" + _RoleDestinyCost.StoneMax;//Old_rd.MaxExp;

            if (SkillSlider.value >= 0.5)
            {
                SkillChanceLabel.text = "高概率";
                isHight = true;
            }
            else
            {
                SkillChanceLabel.text = "低概率";
                isHight = false;
            }
        }
        SetChanceLabelForMoment(mHero.skillLevel, _clickNum);
        if (StoneCost > 0)
        {
            newSliderValue = SkillSlider.value;
            StartCoroutine(SccedAddValueEffect(_SkillNumber, _SkillLevel));
        }
        //Debug.LogError(mHero.skillLevel);
        StoneCost = TextTranslator.instance.GetRoleDestinyCostByID(mHero.skillLevel).StoneCost;
        //Debug.LogError(StoneCost);
        stoneCostLabel.text = StoneCost.ToString();
    }
    void SetChanceLabelForMoment(int _SkillLevel, int _clickNum)
    {
        List<int> limitNumList = new List<int>();
        RoleDestinyCost _roleDestinyCostData = TextTranslator.instance.GetRoleDestinyCostByID(mHero.skillLevel);

        for (int i = 0; i < _roleDestinyCostData.percentList.size; i++)
        {

            limitNumList.Add(_roleDestinyCostData.percentList[i]);
        }
        /*  limitNumList.Add(24);
          limitNumList.Add(33);
          limitNumList.Add(42);
          limitNumList.Add(60);*/
        string chanceStr = "";
        if (_clickNum < limitNumList[0])
        {
            chanceStr = "[ffffff]较低概率[-]";
            isHight = false;
        }
        else if (_clickNum >= limitNumList[0] && _clickNum < limitNumList[1])
        {
            chanceStr = "[3ee817]低概率[-]";
            isHight = false;
        }
        else if (_clickNum >= limitNumList[1] && _clickNum < limitNumList[2])
        {
            chanceStr = "[bb44ff]高概率[-]";
            isHight = true;
        }
        else if (_clickNum >= limitNumList[2] && _clickNum < limitNumList[3])
        {
            chanceStr = "[bb44ff]高概率[-]";
            isHight = true;
        }
        else
        {
            chanceStr = "[ff8c04]较高概率[-]";//直接升级
            isHight = true;
        }
        SkillChanceLabel.text = chanceStr;
    }
    public void OnSliderValueChanged()
    {
        if (SkillSlider.GetComponent<UISlider>().value < 1.0f)
        {
            for (int i = 0; i < 5; i++)
            {
                ListTabs[i].GetComponent<UIToggle>().enabled = false;
            }
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                ListTabs[i].GetComponent<UIToggle>().enabled = true;
            }
        }
    }
}
