using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroLevelUpPart : MonoBehaviour
{
    public GameObject OneKeyButton;
    public GameObject OnLineTrainingObj;
    public UISlider EXPselider;
    public GameObject RankIcon;
    public UILabel PowerNumber;
    public UILabel HP;
    public UILabel ATK;
    public UILabel DEF;
    public UILabel Name;
    public UILabel Level;
    public UILabel EXPLabel;
    public List<GameObject> EXPitemList = new List<GameObject>();
    [SerializeField]
    private UISprite RarityIcon;
    public GameObject heroRankHorizontal;
    [SerializeField]
    private UISprite heroTypeIcon;
    public GameObject LabelEffect;
    public GameObject ExpEffect;
    public GameObject Xuetiao2Sprite;
    [SerializeField]
    private GameObject BuyItemBoard;
    [SerializeField]
    private GameObject ATKtype;
    [SerializeField]
    private GameObject DEFtype;


    public GameObject FocePrefab;


    int CharacterRoleID = 0;
    Hero mHero;
    HeroInfo mHeroInfo;
    int level = 0;

    bool isOnPress = false;
    float timer = 0;
    int NowExpItemID = 0;
    int NowExpItemLeftCount = 0;
    private GameObject targetRoleObj;

    float myAddExpDirectlyTimer = 0;
    float oldExp = 0;
    float newExp = 0;
    int levelupNumber = 0;

    int timesOfSendToSeverContinus = 0;//次数
    public bool IsLevelUp = false;
    int roleModelCount = 0;


    int nowLevel = 0;
    int nowCount = 0;
    int nowExp = 0;
    int nowHeroId = 0;
    /// <summary>
    /// 记录当前的Item的个数
    /// </summary>
    int itemCount = 0;

    private float upLevelSpeed = 0.3f;

    // Use this for initialization
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.升级系统);
        UIManager.instance.UpdateSystems(UIManager.Systems.升级系统);

        UIEventListener.Get(ATKtype).onPress = ClickAtkOrDefTypeButton;
        UIEventListener.Get(DEFtype).onPress = ClickAtkOrDefTypeButton;
        UIEventListener.Get(OneKeyButton).onClick += delegate(GameObject go)
        {
            bool flag = IsMaterailEnough();
            if (mHero.level < CharacterRecorder.instance.level && flag)
            {
                NetworkHandler.instance.SendProcess("5015#" + CharacterRoleID + ";");
            }
            else if (flag)
            {
                UIManager.instance.OpenPromptWindow("英雄等级不能超过战队等级", 11, false, PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                UIManager.instance.OpenPromptWindow("军粮用完啦", 11, false, PromptWindow.PromptType.Hint, null, null);
            }
        };
    }
    bool IsMaterailEnough()
    {
        for (int i = 10001; i <= 10006; i++)
        {
            if (TextTranslator.instance.GetItemCountByID(i) > 0)
            {
                return true;
            }
        }
        return false;
    }
    void OnEnable()
    {
        roleModelCount = 0;
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
    public void PlayShengJiEffect()
    {
        StartCoroutine(ScuccedShengJiEffect());
    }
    IEnumerator ScuccedShengJiEffect()
    {
        /* GameObject _shengPinEffect = NGUITools.AddChild(this.gameObject, shengPinEffect);
         _shengPinEffect.transform.localPosition = new Vector3(0, 0, -10);
         _shengPinEffect.transform.localScale = new Vector3(200, 200, 200);*/
        yield return new WaitForSeconds(0.1f);
        GameObject go = GameObject.Instantiate(Resources.Load("Prefab/Effect/JueSeShengJi", typeof(GameObject)), PictureCreater.instance.ListRolePicture[0].RoleObject.transform.position, Quaternion.identity) as GameObject;
        go.transform.parent = targetRoleObj.transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localEulerAngles = Vector3.zero;
        go.transform.localScale = new Vector3(3f, 3f, 3f);
        AudioEditer.instance.PlayOneShot("ui_levelup");
    }
    void ClickEXPitemListButton(GameObject go)
    {
        if (go != null)
        {
            /*  if (go.transform.FindChild("Number").GetComponent<UILabel>().text == "0")
              {
                  UIManager.instance.OpenPanel("WayWindow", false);
                  GameObject _WayWindow = GameObject.Find("WayWindow");
                  _WayWindow.GetComponent<WayWindow>().SetWayInfo(int.Parse(go.name));
                  _WayWindow.layer = 11;
                  foreach (Component c in _WayWindow.GetComponentsInChildren(typeof(Transform), true))
                  {
                      c.gameObject.layer = 11;
                  }
                  return;
              }*/

            if (go.transform.FindChild("Number").GetComponent<UILabel>().text == "0")
            {
                TextTranslator.ItemInfo miteminfo = TextTranslator.instance.GetItemByItemCode(int.Parse(go.name));
                if (CharacterRecorder.instance.level < miteminfo.BuyLevel)
                {
                    UIManager.instance.OpenPromptWindow(string.Format("战队等级达到{0}级可以购买该道具！", miteminfo.BuyLevel), 11, false, PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                BuyItemBoard.SetActive(true);
                BuyItemBoard.GetComponent<BuyItemBoard>().SetBuyItemBoardInfo(int.Parse(go.name));
                BuyItemBoard.layer = 11;
                foreach (Component c in BuyItemBoard.GetComponentsInChildren(typeof(Transform), true))
                {
                    c.gameObject.layer = 11;
                }
                return;
            }
            NetworkHandler.instance.SendProcess("5004#" + CharacterRoleID + ";" + go.name + ";");
        }
    }
    void PressEXPitemListButton(GameObject go, bool isPress)
    {
        if (go != null)
        {
            if (CharacterRecorder.instance.GuideID[26] == 4 || CharacterRecorder.instance.GuideID[26] == 5)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
            NowExpItemID = int.Parse(go.name);
            NowExpItemLeftCount = TextTranslator.instance.GetItemCountByID(NowExpItemID);

            if (isPress && NowExpItemLeftCount != 0 && mHero.level < CharacterRecorder.instance.level)
            {
                isOnPress = isPress;
                NowExpItemID = int.Parse(go.name);
                go.transform.FindChild("PressEffect").gameObject.SetActive(true);
                NowExpItemLeftCount = TextTranslator.instance.GetItemCountByID(NowExpItemID);
                timesOfSendToSeverContinus = 0;

                itemCount = NowExpItemLeftCount;
                //Debug.LogError("itemCount:::" + itemCount);
                nowCount = 0;
                nowExp = mHero.exp;
                nowLevel = mHero.level;
                nowHeroId = mHero.cardID;
                StartCoroutine(SendToSeverContinus(go));
            }
            else if (isPress && NowExpItemLeftCount == 0)
            {
                CanNotUpGrade(go);
                isOnPress = false;
                timer = 0;
            }
            else if (isPress && mHero.level >= CharacterRecorder.instance.level)
            {
                UIManager.instance.OpenPromptWindow("英雄等级不能超过战队等级", 11, false, PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                go.transform.FindChild("PressEffect").gameObject.SetActive(false);
                isOnPress = isPress;
                timer = 0;
            }
        }
    }
    IEnumerator SendToSeverContinus(GameObject go)
    {
        //Debug.LogError(isOnPress);
        //Debug.LogError(NowExpItemLeftCount);

        if (isOnPress && NowExpItemLeftCount > 0 && nowLevel < CharacterRecorder.instance.level)
        {
            yield return new WaitForSeconds(0.12f);
            timesOfSendToSeverContinus += 1;
            if (timesOfSendToSeverContinus >= 30)
            {
                timesOfSendToSeverContinus = 30;
            }
            int useCount = (int)Mathf.Pow(2, timesOfSendToSeverContinus / 10);
            //NetworkHandler.instance.SendProcess("5004#" + CharacterRoleID + ";" + NowExpItemID + ";" + (timesOfSendToSeverContinus /10 + 1));//线性增长
            //NetworkHandler.instance.SendProcess("5004#" + CharacterRoleID + ";" + NowExpItemID + ";" + useCount);//指数增长

            nowCount += useCount;
            NowExpItemLeftCount = itemCount - nowCount;
            if (NowExpItemLeftCount <= 0)
            {
                nowCount = itemCount;
                useCount += NowExpItemLeftCount;
            }
            LevelUpMenu(useCount);
            upLevelSpeed = Mathf.Lerp(upLevelSpeed, 0.1f, 0.1f);
            StartCoroutine(SendToSeverContinus(go));
            //Debug.LogError("逅。。" + NowExpItemLeftCount);
        }
        else
        {
            upLevelSpeed = 0.3f;
            if (NowExpItemLeftCount <= 0)
            {
                CanNotUpGrade(go);
            }
            else if (nowLevel >= CharacterRecorder.instance.level)
            {
                UIManager.instance.OpenPromptWindow("英雄等级不能超过战队等级", 11, false, PromptWindow.PromptType.Hint, null, null);

            }
            if (nowCount > 0)
            {
                //Debug.LogError("itemCount:::" + itemCount);

                //Debug.LogError(string.Format("NowExpItemLeftCount:::{0}.............{1}", NowExpItemLeftCount, nowCount));

                NetworkHandler.instance.SendProcess("5004#" + CharacterRoleID + ";" + NowExpItemID + ";" + nowCount);//指数增长
            }
        }
    }
    public int GetHeroClassType()
    {
        int type = 0;
        switch (mHeroInfo.heroRarity)
        {
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            default:
                break;
        }
        return type;
    }
    public void LevelUpMenu(int usCount)
    {
        int nowMaxExp = 0;
        if (mHeroInfo.heroRarity == 1)//
        {
            nowMaxExp = TextTranslator.instance.GetExpsHeroInfoByLevel(nowLevel).RoleExp2;
        }
        else if (mHeroInfo.heroRarity == 2)//蓝色
        {
            nowMaxExp = TextTranslator.instance.GetExpsHeroInfoByLevel(nowLevel).RoleExp2;
        }
        else if (mHeroInfo.heroRarity == 3)//紫色
        {
            nowMaxExp = TextTranslator.instance.GetExpsHeroInfoByLevel(nowLevel).RoleExp3;
        }
        else if (mHeroInfo.heroRarity == 4)//橙色
        {
            nowMaxExp = TextTranslator.instance.GetExpsHeroInfoByLevel(nowLevel).RoleExp4;
        }
        else if (mHeroInfo.heroRarity == 5)//红色
        {
            nowMaxExp = TextTranslator.instance.GetExpsHeroInfoByLevel(nowLevel).RoleExp5;
        }
        else if (mHeroInfo.heroRarity == 6)//金色
        {
            nowMaxExp = TextTranslator.instance.GetExpsHeroInfoByLevel(nowLevel).RoleExp6;
        }

        TextTranslator.ItemInfo iteminfo = TextTranslator.instance.GetItemByItemCode(NowExpItemID);
        nowExp += int.Parse(iteminfo.ToValue) * usCount;
        if (nowExp >= nowMaxExp)
        {
            nowLevel++;
            nowExp -= nowMaxExp;
            GameObject shnegji = GameObject.Find("LabelEffect1");
            if (shnegji == null)
            {
                GameObject go = GameObject.Instantiate(Resources.Load("Prefab/Effect/JueSeShengJi", typeof(GameObject)), PictureCreater.instance.ListRolePicture[0].RoleObject.transform.position, Quaternion.identity) as GameObject;
                go.name = "RoleJueSeShengJi";
                AudioEditer.instance.PlayOneShot("ui_levelup");

                GameObject obj = NGUITools.AddChild(LabelEffect.transform.parent.gameObject, LabelEffect);
                obj.SetActive(true);
                obj.name = "LabelEffect1";
                Destroy(obj, 0.7f);
                RoleGrow roleGrow = TextTranslator.instance.GetRoleGrowInfoByRoleId(mHero.cardID, mHero.rank);
                if (roleGrow != null)
                {
                    PowerNumber.text = string.Format("{0}", int.Parse(PowerNumber.text) + roleGrow.Hit);
                    HP.text = string.Format("{0}", int.Parse(HP.text) + roleGrow.Hp);
                    ATK.text = string.Format("{0}", int.Parse(ATK.text) + roleGrow.Atk);
                    DEF.text = string.Format("{0}", int.Parse(DEF.text) + roleGrow.Def);
                }
                else
                {
                    //Debug.LogError(mHero.cardID + "  " + mHero.rank);

                    //Debug.LogError(TextTranslator.instance.GetRoleGrowInfoByRoleId(60015, 5).Atk);
                }
                StartCoroutine(OnEquipDataUpEffect(PowerNumber));
                StartCoroutine(OnEquipDataUpEffect(HP));
                StartCoroutine(OnEquipDataUpEffect(ATK));
                StartCoroutine(OnEquipDataUpEffect(DEF));
            }
            GameObject heroItem = GameObject.Find("HeroItem" + nowHeroId);
            if (heroItem != null)
            {
                heroItem.transform.FindChild("Level").GetComponent<UILabel>().text = nowLevel.ToString();
            }

            StartCoroutine(StartXuetiao());
            //UIManager.instance.OpenPanel("ForceChangesWindow", false);
            //GameObject foce = GameObject.Find("ForceChangesWindow");
            //if (foce != null)
            //{
            //    foce.transform.localPosition = new Vector3(0, 280, 0);
            //    //int Forcebefore = 0;
            //    //int ForceNow = 0;
            //    //foce.GetComponent<ForceChangesWindow>().LookForceChange(Forcebefore, ForceNow);
            //}
        }
        Level.text = "Lv." + nowLevel.ToString();
        foreach (var item in EXPitemList)
        {
            if (item.name == NowExpItemID.ToString())
            {
                item.transform.FindChild("Number").GetComponent<UILabel>().text = NowExpItemLeftCount.ToString();
            }
        }
        //StopCoroutine("UpExpLabel");
        //StartCoroutine("UpExpLabel", nowExp);
        EXPselider.value = nowExp * 1.0f / nowMaxExp;
        EXPLabel.text = string.Format("{0}/{1}", nowExp, nowMaxExp);
    }
    IEnumerator UpExpLabel(int oldExp)
    {
        int nowMaxExp = TextTranslator.instance.GetExpsHeroInfoByLevel(nowLevel).RoleExp5;
        TextTranslator.ItemInfo iteminfo = TextTranslator.instance.GetItemByItemCode(NowExpItemID);
        oldExp += int.Parse(iteminfo.ToValue) / 100;
        EXPselider.value = nowExp * 1.0f / nowMaxExp;
        EXPLabel.text = string.Format("{0}/{1}", nowExp, nowMaxExp);
        yield return new WaitForSeconds(0.003f);
        if (oldExp < nowExp)
        {
            StartCoroutine("UpExpLabel", oldExp);
        }
    }
    IEnumerator StartXuetiao()
    {
        Xuetiao2Sprite.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        Xuetiao2Sprite.SetActive(false);
    }

    void CanNotUpGrade(GameObject go)
    {
        go.transform.FindChild("PressEffect").gameObject.SetActive(false);
        TextTranslator.ItemInfo miteminfo = TextTranslator.instance.GetItemByItemCode(NowExpItemID);
        if (CharacterRecorder.instance.level < miteminfo.BuyLevel)
        {
            UIManager.instance.OpenPromptWindow(string.Format("战队等级达到{0}级可以购买该道具！", miteminfo.BuyLevel), 11, false, PromptWindow.PromptType.Hint, null, null);
            return;
        }
        BuyItemBoard.SetActive(true);
        BuyItemBoard.GetComponent<BuyItemBoard>().SetBuyItemBoardInfo(NowExpItemID);
        BuyItemBoard.layer = 11;
        foreach (Component c in BuyItemBoard.GetComponentsInChildren(typeof(Transform), true))
        {
            c.gameObject.layer = 11;
        }
    }
    public void UpDataItemStateAfterBuy()
    {
        int _BuyItemID = BuyItemBoard.GetComponent<BuyItemBoard>().curItemID;
        for (int j = 0; j < EXPitemList.Count; j++)
        {
            if (TextTranslator.instance.GetItemCountByID(int.Parse(EXPitemList[j].name)) != null && int.Parse(EXPitemList[j].name) == _BuyItemID)
            {
                ShowItemIconOrAddIconByCode(int.Parse(EXPitemList[j].name), EXPitemList[j].transform.FindChild("Number").GetComponent<UILabel>(),
                EXPitemList[j].transform.FindChild("Mask").gameObject);
                return;
            }
        }
    }
    void ShowItemIconOrAddIconByCode(int _itemCode, UILabel _countLabel, GameObject _mask)
    {
        int ItemCount = TextTranslator.instance.GetItemCountByID(_itemCode);
        _countLabel.text = ItemCount.ToString();
        if (TextTranslator.instance.GetItemCountByID(_itemCode) > 0)
        {
            _mask.SetActive(false);
        }
        else
        {
            _mask.SetActive(true);
        }
        if (NowExpItemID == _itemCode)
        {
            NowExpItemLeftCount = ItemCount;
        }
    }
    void SpecialOnTheLastItem()//最后一个特殊处理 发升50级
    {
        if (UIEventListener.Get(EXPitemList[5]).onClick == null)
        {
            UIEventListener.Get(EXPitemList[5]).onClick += delegate(GameObject go)
            {
                /* if (go.transform.FindChild("Number").GetComponent<UILabel>().text == "0")
                 {
                     UIManager.instance.OpenPanel("WayWindow", false);
                     GameObject _WayWindow = GameObject.Find("WayWindow");
                     _WayWindow.GetComponent<WayWindow>().SetWayInfo(int.Parse(go.name));
                     _WayWindow.layer = 11;
                     foreach (Component c in _WayWindow.GetComponentsInChildren(typeof(Transform), true))
                     {
                         c.gameObject.layer = 11;
                     }
                     return;
                 }*/
                if (go.transform.FindChild("Number").GetComponent<UILabel>().text == "0")
                {
                    TextTranslator.ItemInfo miteminfo = TextTranslator.instance.GetItemByItemCode(int.Parse(go.name));
                    if (CharacterRecorder.instance.level < miteminfo.BuyLevel)
                    {
                        UIManager.instance.OpenPromptWindow(string.Format("战队等级达到{0}级可以购买该道具！", miteminfo.BuyLevel), 11, false, PromptWindow.PromptType.Hint, null, null);
                        return;
                    }
                    BuyItemBoard.SetActive(true);
                    BuyItemBoard.GetComponent<BuyItemBoard>().SetBuyItemBoardInfo(int.Parse(go.name));
                    BuyItemBoard.layer = 11;
                    foreach (Component c in BuyItemBoard.GetComponentsInChildren(typeof(Transform), true))
                    {
                        c.gameObject.layer = 11;
                    }
                    return;
                }
                //NetworkHandler.instance.SendProcess("5004#" + CharacterRoleID + ";" + EXPitemList[5].name + ";");
                NetworkHandler.instance.SendProcess("9301#1;50;");
            };
        }
        if (UIEventListener.Get(EXPitemList[5]).onPress == null)
        {
            UIEventListener.Get(EXPitemList[5]).onPress += delegate(GameObject go, bool isPress)
            {
                /*
                 if (isPress)
                 {
                      isOnPress = isPress;
                     NowExpItemID = int.Parse(EXPitemList[5].name);
                 }
                 else
                 {
                     isOnPress = isPress;
                     timer = 0;
                 }
                  */
                NetworkHandler.instance.SendProcess("9301#1;50;");
            };
        }
    }
    //启动
    public void StarPart(int _CharacterRoleID, Hero _hero)
    {

        mHero = _hero;
        HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(mHero.cardID);
        mHeroInfo = hinfo;
        Name.text = hinfo.heroName;

        //Debug.LogError(mHeroInfo.heroName + " ::: " + mHeroInfo.heroRarity);

        if (LegionTrainingGroundWindow.mOnLineTrainingHeroList.Contains(_hero))
        {
            OnLineTrainingObj.SetActive(true);
        }
        else
        {
            OnLineTrainingObj.SetActive(false);
        }

        Level.text = "Lv." + mHero.level.ToString();

        level = mHero.level;


        PowerNumber.text = mHero.force.ToString();
        HP.text = mHero.HP.ToString();
        ATK.text = mHero.strength.ToString();
        DEF.text = mHero.physicalDefense.ToString();
        EXPselider.value = (float)mHero.exp / (float)mHero.maxExp;
        EXPLabel.text = string.Format("{0}/{1}", mHero.exp, mHero.maxExp);
        SetAtkOrDefTRype();
        SetRankIcon();
        SetRarityIcon();
        heroRankHorizontal.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(_hero.rank);
        SetHeroType(hinfo.heroCarrerType);
        //SetNameColor();//旧的
        TextTranslator.instance.SetHeroNameColor(Name, mHeroInfo.heroName, mHero.classNumber);

        for (int j = 0; j < EXPitemList.Count; j++)
        {
            //EXPitemList[j].transform.FindChild("Number").GetComponent<UILabel>().text = TextTranslator.instance.GetItemCountByID(int.Parse(EXPitemList[j].name)).ToString();
            ShowItemIconOrAddIconByCode(int.Parse(EXPitemList[j].name), EXPitemList[j].transform.FindChild("Number").GetComponent<UILabel>(),
                EXPitemList[j].transform.FindChild("Mask").gameObject);
            //    if (j < 5)
            {
                //  UIEventListener.Get(EXPitemList[j]).onClick = ClickEXPitemListButton;
                UIEventListener.Get(EXPitemList[j]).onPress = PressEXPitemListButton;
            }
            //    else if (j == 5)
            {

            }
        }
        if (roleModelCount == 0 || CharacterRoleID != _CharacterRoleID)
        {
            CharacterRoleID = _CharacterRoleID;
            PictureCreater.instance.DestroyAllComponent();
            int i = PictureCreater.instance.CreateRole(mHero.cardID, "Model", 18, Color.red, 0, 0, 1, 2f, false, false, 1, 1.5f, 0.2f, "Model", 0, 10, 10, 2, 2, 0, 0, 1001, 1002, mHero.WeaponList[0].WeaponClass, 1, mHero.WeaponList[0].WeaponClass, 1, 1, 0, "");
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.parent = gameObject.transform;
            //角色旋转、播动画
            GameObject _roleWindow = GameObject.Find("RoleWindow");
            if (_roleWindow != null)
            {
                _roleWindow.GetComponent<PlayAniOrRotation>().ResetTargetRole(PictureCreater.instance.ListRolePicture[0].RoleObject.transform.FindChild("Role").gameObject, _hero);
            }
            targetRoleObj = PictureCreater.instance.ListRolePicture[0].RoleObject;
            int modelPositionY = -660;
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
            roleModelCount++;
        }
    }

    void SetExp(int _LevelUPnumber, float _oldExp, float _newExp)
    {
        levelupNumber += _LevelUPnumber;
        oldExp = _oldExp;
        newExp = _newExp;
        myAddExpDirectlyTimer = oldExp;
    }
    bool isNeedInvoke = true;
    //更新
    public void UpdateExp(int ExpItemID, int _LevelUPnumber, float _oldExp, float _newExp)
    {
        //Debug.LogError(mHero.HP + "             " + int.Parse(HP.text));

        //        PlayShengJiEffect();//成功升级才播放特效
        UpdateState();
        // Debug.LogError(string.Format("oldExp..{0}..newExp..{1}..LevelUPnumber..{2}", oldExp, newExp, _LevelUPnumber));
        //  StopAllCoroutines();
        StopCoroutine(OnEquipDataUpEffect(PowerNumber));
        StopCoroutine(OnEquipDataUpEffect(HP));
        StopCoroutine(OnEquipDataUpEffect(ATK));
        StopCoroutine(OnEquipDataUpEffect(DEF));

        level += _LevelUPnumber;
        SetExp(_LevelUPnumber, _oldExp, _newExp);
        //        StartCoroutine(AddExp());//去掉进度条增长的效果,直接跳到newExp
        StartCoroutine(AddExpDirectly());//直接跳到newExp
        /*  if (isNeedInvoke)//新的，没改成功
          {
              InvokeRepeating("OnSecondFunction", 0.0f, 0.01f);
              isNeedInvoke = false;
          }*/
        for (int j = 0; j < EXPitemList.Count; j++)
        {
            if (TextTranslator.instance.GetItemCountByID(int.Parse(EXPitemList[j].name)) != null && int.Parse(EXPitemList[j].name) == ExpItemID)
            {
                // EXPitemList[j].transform.FindChild("Number").GetComponent<UILabel>().text = TextTranslator.instance.GetItemCountByID(int.Parse(EXPitemList[j].name)).ToString();
                ShowItemIconOrAddIconByCode(int.Parse(EXPitemList[j].name), EXPitemList[j].transform.FindChild("Number").GetComponent<UILabel>(),
                EXPitemList[j].transform.FindChild("Mask").gameObject);
            }
        }
        if (CharacterRecorder.instance.GuideID[26] == 4 || CharacterRecorder.instance.GuideID[26] == 5)
        {
            CharacterRecorder.instance.GuideID[26]=6;
            StartCoroutine(SceneTransformer.instance.NewbieGuide());
        }

    }
    public void UpdateExp(List<int> ExpItemID, int _LevelUPnumber, float _oldExp, float _newExp)
    {
        UpdateState();
        StopCoroutine(OnEquipDataUpEffect(PowerNumber));
        StopCoroutine(OnEquipDataUpEffect(HP));
        StopCoroutine(OnEquipDataUpEffect(ATK));
        StopCoroutine(OnEquipDataUpEffect(DEF));
        SetExp(_LevelUPnumber, _oldExp, _newExp);
        //        StartCoroutine(AddExp());//去掉进度条增长的效果,直接跳到newExp
        StartCoroutine(AddExpDirectly());//直接跳到newExp
        for (int i = 0; i < ExpItemID.Count; i++)
        {
            for (int j = 0; j < EXPitemList.Count; j++)
            {
                if (TextTranslator.instance.GetItemCountByID(int.Parse(EXPitemList[j].name)) != null && int.Parse(EXPitemList[j].name) == ExpItemID[i])
                {
                    ShowItemIconOrAddIconByCode(int.Parse(EXPitemList[j].name), EXPitemList[j].transform.FindChild("Number").GetComponent<UILabel>(),
                    EXPitemList[j].transform.FindChild("Mask").gameObject);
                }
            }
        }
    }
    public void UpdateState()
    {
        AddFlyLabel(int.Parse(HP.text) - mHero.HP, int.Parse(ATK.text) - mHero.strength, int.Parse(DEF.text) - mHero.physicalDefense);
        //Name.text = mHeroInfo.heroName;
        TextTranslator.instance.SetHeroNameColor(Name, mHeroInfo.heroName, mHero.classNumber);
        PowerNumber.text = mHero.force.ToString();
        HP.text = mHero.HP.ToString();
        ATK.text = mHero.strength.ToString();
        DEF.text = mHero.physicalDefense.ToString();

        if (IsLevelUp == true)
        {
            StartCoroutine(OnEquipDataUpEffect(PowerNumber));
            StartCoroutine(OnEquipDataUpEffect(HP));
            StartCoroutine(OnEquipDataUpEffect(ATK));
            StartCoroutine(OnEquipDataUpEffect(DEF));
        }
    }
    IEnumerator AddExp()
    {
        if (levelupNumber <= 0)
        {
            EXPselider.value = oldExp;
            while (true)
            {
                EXPselider.value += 0.05f;
                if (EXPselider.value >= newExp)
                {
                    break;
                }
                yield return new WaitForSeconds(0.01f);
            }
        }
        else
        {
            while (true)
            {
                EXPselider.value += 0.05f;
                if (levelupNumber > 0 && EXPselider.value >= 1)
                {
                    //Debug.LogError(level + " " + levelupNumber);
                    levelupNumber -= 1;
                    Level.text = "Lv." + (level - levelupNumber).ToString();
                    EXPselider.value = 0;
                    GameObject go = GameObject.Instantiate(Resources.Load("Prefab/Effect/JueSeShengJi", typeof(GameObject)), PictureCreater.instance.ListRolePicture[0].RoleObject.transform.position, Quaternion.identity) as GameObject;
                    AudioEditer.instance.PlayOneShot("ui_levelup");

                    GameObject obj = NGUITools.AddChild(LabelEffect.transform.parent.gameObject, LabelEffect);
                    obj.SetActive(true);
                    Destroy(obj, 0.7f);
                }
                if (levelupNumber <= 0 && EXPselider.value >= newExp)
                {
                    Level.text = "Lv." + level.ToString();
                    IsLevelUp = false;
                    break;
                }
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
    IEnumerator AddExpDirectly()
    {
        if (levelupNumber <= 0)
        {
            EXPselider.value = newExp;
        }
        else
        {
            if (level <= levelupNumber)
            {
                yield return new WaitForSeconds(0.01f);
            }
            while (true)
            {
                myAddExpDirectlyTimer += 0.05f;
                if (levelupNumber > 0 && myAddExpDirectlyTimer >= 1)
                {
                    //Debug.LogError(level + " " + levelupNumber);
                    levelupNumber = 0;
                    Level.text = "Lv." + level.ToString();
                    /* GameObject go = GameObject.Instantiate(Resources.Load("Prefab/Effect/JueSeShengJi", typeof(GameObject)), PictureCreater.instance.ListRolePicture[0].RoleObject.transform.position, Quaternion.identity) as GameObject;
                     AudioEditer.instance.PlayOneShot("ui_levelup");
                     myAddExpDirectlyTimer = 0;
                     GameObject obj = NGUITools.AddChild(LabelEffect.transform.parent.gameObject, LabelEffect);
                     obj.SetActive(true);
                     Destroy(obj, 0.7f);*/
                    yield break;
                }
                /*if (levelupNumber <= 0 && myAddExpDirectlyTimer >= newExp)
                {
                    Level.text = "Lv." + level.ToString();
                    IsLevelUp = false;
                    break;
                }*/
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
    void OnSecondFunction()
    {
        //Debug.Log("levelupNumber..." + levelupNumber + "..newExp.." + newExp);
        if (levelupNumber > 0)
        {
            if (levelupNumber == 1)
            {
                if (EXPselider.value < newExp)
                    EXPselider.value += 0.01f;
                else
                {
                    levelupNumber--;
                    Level.text = "Lv." + (level - levelupNumber).ToString();
                }
            }
            else
            {
                if (EXPselider.value < 1)
                    EXPselider.value += 0.01f;
                else
                {
                    levelupNumber--;
                    Level.text = "Lv." + (level - levelupNumber).ToString();
                    //Level.text = "Lv." + (level - levelupNumber <= 0 ? level : level - levelupNumber).ToString();
                    EXPselider.value = 0;
                }
            }
        }
        else
        {
            IsLevelUp = false;
            isNeedInvoke = true;
            CancelInvoke("OnSecondFunction");
            // OverPlay();
            InvokeRepeating("OverPlay", 0.0f, 0.01f);
        }
    }
    void OverPlay()
    {
        // EXPselider.value = newExp;
        if (EXPselider.value < newExp)
            EXPselider.value += 0.01f;
        else
        {
            CancelInvoke("OverPlay");
            EXPselider.value = newExp;
            Level.text = "Lv." + level.ToString();
        }
    }


    public void AddFlyLabel(int _Hp, int _Atk, int _Def)
    {
        StartCoroutine(FlyLabel(_Hp, _Atk, _Def));
    }

    IEnumerator FlyLabel(int _Hp, int _Atk, int _Def)
    {
        //Debug.LogError(_Hp + "   " + _Atk + "     " + _Def);
        Vector3 fromPos = new Vector3(-100, -260, 0);
        Vector3 toPos = new Vector3(-100, -80, 0);
        if (_Hp != 0)
        {
            UIManager.instance.OpenPromptWindow("生命+" + _Hp, 11, true, fromPos, toPos, PromptWindow.PromptType.Hint, null, null);
            yield return new WaitForSeconds(0.6f);
        }
        if (_Atk != 0)
        {
            UIManager.instance.OpenPromptWindow("攻击+" + _Atk, 11, true, fromPos, toPos, PromptWindow.PromptType.Hint, null, null);
            yield return new WaitForSeconds(0.6f);
        }
        if (_Def != 0)
        {
            UIManager.instance.OpenPromptWindow("防御+" + _Def, 11, true, fromPos, toPos, PromptWindow.PromptType.Hint, null, null);
            yield return new WaitForSeconds(0.6f);
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
    //设置军衔
    void SetRankIcon()
    {
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
