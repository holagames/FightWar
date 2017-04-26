using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardIntroduceWindow : MonoBehaviour
{

    public UITexture SkillSprite;
    public UILabel SkillIntroduce;

    public UITexture HeroSprite;
    public UILabel CareerName;

    public UITexture Skill2Sprite;
    public UILabel LabelIntroduce;

    public UILabel HeroName;

    public UILabel HaveLabel;
    public GameObject GoLabel;

    public GameObject SureBtn;
    public GameObject HeroInfo;
    public GameObject TweenItem;
    public GameObject BottomInfo;

    private GameObject Create3DHero;
    public UILabel HaveHeroLabel;
    //缘分相关
    public UIGrid FateGrid;
    public GameObject FateItem;


    private int RoleID;
    BetterList<RoleFate> MyRoleFateList;
    List<GameObject> FateObjList = new List<GameObject>();
    int fateCount = 0;
    //属性相关
    public List<UISprite> HeroAttribute = new List<UISprite>();
    public GameObject HeroAttributeObj;
    float timer = 1f;
    void Start()
    {
        Transform _mainwindow = GameObject.Find("UIRoot").transform.Find("MainWindow");
        if (UIEventListener.Get(SureBtn).onClick == null)
        {
            UIEventListener.Get(SureBtn).onClick = delegate(GameObject go)
            {
                CharacterRecorder.instance.isGaChaFromItemClick = false;
                if (RoleID == 60026)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_503);
                }
                else if (RoleID == 60002)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_603);
                }
                else if (RoleID == 60016)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_805);
                }
                else if (RoleID == 60028)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1405);
                }

                if (GameObject.Find("GaChaGetWindow") != null)
                {
                    GameObject.Find("GaChaGetWindow").GetComponent<GaChaGetWindow>().IsLock = false;
                    if (HaveHeroLabel.gameObject.activeSelf)
                    {
                        GameObject.Find("GaChaGetWindow").GetComponent<GaChaGetWindow>().SetHeroSuipian2(RoleID);
                    }
                    else
                    {
                        CharacterRecorder.instance.heroIdList.Add(RoleID);
                    }
                }
                PictureCreater.instance.DestroyEnemyComponent();
                DestroyImmediate(this.gameObject);
                //if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) < 18) //kino
                //{
                //    SceneTransformer.instance.NewGuideButtonClick();
                //}
                if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 2 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 2) //kino
                {
                    PlayerPrefs.SetInt(LuaDeliver.instance.GetGuideSubStateName(), PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) + 1);                    
                }
                else if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 2 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 6) //kino
                {
                    PlayerPrefs.SetInt(LuaDeliver.instance.GetGuideSubStateName(), PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) + 1);
                }
                else if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 4 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 6) //kino
                {
                    PlayerPrefs.SetInt(LuaDeliver.instance.GetGuideSubStateName(), PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) + 1);
                }
                else if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 15 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 5) //kino
                {
                    PlayerPrefs.SetInt(LuaDeliver.instance.GetGuideSubStateName(), PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) + 1);
                }
                LuaDeliver.instance.UseGuideStation();

                if (CharacterRecorder.instance.isGaChaFromMainWindow == true && CharacterRecorder.instance.isRedHeroWindowFirst == false)
                {
                    CharacterRecorder.instance.isGaChaFromMainWindow = false;
                    //_mainwindow.gameObject.SetActive(true);
                    //_mainwindow.GetComponent<MainWindow>().SetTeamInfo();
                    PictureCreater.instance.DestroyEnemyComponent();
                    // DestroyImmediate(_mainwindow.gameObject);
                    UIManager.instance.OpenPanel("MainWindow", true);
                }
                GameObject hmw = GameObject.Find("HeroMapWindow");
                if (hmw != null)
                {
                    hmw.GetComponent<HeroMapWindow>().UpdatePageInfo();
                }
            };
        }
        Invoke("PlaySkillAnimator", 0.8f);
        Invoke("CloseLastWindow", 0.1f);
        StartCoroutine(ShowInfo());
    }
    //属性界面打开
    void OpenHeroHeroAttribute()
    {
        HeroAttributeObj.SetActive(true);
        HeroInfo hero = TextTranslator.instance.GetHeroInfoByHeroID(RoleID);
        HeroAttribute[0].fillAmount = (float)hero.AtkScore / 15;
        HeroAttributeInfo(HeroAttribute[0].gameObject, hero.AtkScore);
        HeroAttribute[1].fillAmount = (float)hero.DefScore / 15;
        HeroAttributeInfo(HeroAttribute[1].gameObject, hero.DefScore);
        HeroAttribute[2].fillAmount = (float)hero.HpScore / 15;
        HeroAttributeInfo(HeroAttribute[2].gameObject, hero.HpScore);
        HeroAttribute[3].fillAmount = (float)hero.SkillScore / 15;
        HeroAttributeInfo(HeroAttribute[3].gameObject, hero.SkillScore);
    }
    void HeroAttributeInfo(GameObject go, int FillAmount)
    {
        string label = "";
        if (FillAmount == 15)
        {
            label = "word_S";
        }
        else if (FillAmount >= 12 && FillAmount <= 14)
        {
            label = "word_A";
        }
        else if (FillAmount >= 9 && FillAmount <= 11)
        {
            label = "word_B";
        }
        else if (FillAmount >= 6 && FillAmount <= 8)
        {
            label = "word_C";
        }
        else
        {
            label = "word_D";
        }
        go.transform.Find("Label").GetComponent<UISprite>().spriteName = label;
    }
    IEnumerator ShowInfo()
    {
        yield return new WaitForSeconds(1.0f);
        yield return new WaitForSeconds(0.1f);
        HeroInfo.transform.FindChild("RightInfo").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        HeroInfo.transform.FindChild("LeftInfo").gameObject.SetActive(true);
        if (FateObjList.Count > 0)
            HeroInfo.transform.FindChild("FatePromptInfo").gameObject.SetActive(true);
        OpenHeroHeroAttribute();
    }

    public void SetIntroduceInfo(int _RoleID)
    {
        this.RoleID = _RoleID;
        HeroInfo mheroInfo = TextTranslator.instance.GetHeroInfoByHeroID(_RoleID);
        //mheroInfo.heroRarity
        HeroSprite.mainTexture = Resources.Load("Loading/" + _RoleID.ToString()) as Texture;
        HeroSprite.MakePixelPerfect();
        // SkillSprite.spriteName = mheroInfo.heroSkillList[0].ToString();
        SkillSprite.mainTexture = Resources.Load(string.Format("Skill/{0}", mheroInfo.heroSkillList[0]), typeof(Texture)) as Texture;
        Skill2Sprite.mainTexture = Resources.Load(string.Format("Skill/{0}", mheroInfo.heroSkillList[1]), typeof(Texture)) as Texture;

        //CareerName.text = mheroInfo.careerShow;
        HeroName.text = mheroInfo.heroName;
        HaveLabel.text = mheroInfo.heroName;//GetHeroName(mheroInfo);

        if (GameObject.Find("GaChaGetWindow") != null)
        {
            GetSureHeroHave(_RoleID, "1");
        }
        else
        {
            GetSureHeroHave(_RoleID);
        }
        SetRarityIcon(HeroName.gameObject, mheroInfo.heroRarity);
        SetRarityIcon(HaveLabel.gameObject, mheroInfo.heroRarity);

        //创建3D人物
        PictureCreater.instance.DestroyEnemyComponent();
        int i = PictureCreater.instance.CreateRole(_RoleID, "Model", 18, Color.red, 0, 0, 1, 2f, true, false, 1, 1.5f, 0.2f, "Model", 0, 10, 10, 2, 2, 0, 0, 1001, 1002, 4, 1, 4, 1, 1, 0, "");
        //GameObject _tween = NGUITools.AddChild(this.transform.Find("HeroInfo").gameObject,TweenItem);
        PictureCreater.instance.ListEnemyPicture[0].RoleObject.transform.parent = TweenItem.transform;
        PictureCreater.instance.ListEnemyPicture[0].RoleObject.transform.localPosition = Vector3.zero;
        PictureCreater.instance.ListEnemyPicture[0].RolePictureObject.transform.Rotate(new Vector3(0, 180, 0));
        Create3DHero = PictureCreater.instance.ListEnemyPicture[0].RoleObject.transform.FindChild("Role").gameObject;
        GameObject _cardIntroduceWindow = this.gameObject;
        if (_cardIntroduceWindow != null)
        {
            Hero mHero = CharacterRecorder.instance.GetHeroByRoleID(_RoleID);
            _cardIntroduceWindow.GetComponent<PlayAniOrRotation>().ResetTargetRole(PictureCreater.instance.ListEnemyPicture[0].RoleObject.transform.FindChild("Role").gameObject, mHero);
        }
        //targetRoleObj = PictureCreater.instance.ListEnemyPicture[0].RoleObject;
        int modelPositionY = -600;
        if (_RoleID == 60201)// 飞机的话|| _hero.cardID == 60200
        {
            modelPositionY = -800;
        }
        //if ((Screen.width * 1f / Screen.height) > 1.7f)
        //{
        //    PictureCreater.instance.ListEnemyPicture[0].RoleObject.transform.localPosition = new Vector3(7600, modelPositionY, 13339);
        //}
        //else if ((Screen.width * 1f / Screen.height) < 1.4f)
        //{
        //    PictureCreater.instance.ListEnemyPicture[0].RoleObject.transform.localPosition = new Vector3(7600, modelPositionY, 13339);
        //}
        //else
        //{
        //    PictureCreater.instance.ListEnemyPicture[0].RoleObject.transform.localPosition = new Vector3(7600, modelPositionY, 13339);
        //}
        //PictureCreater.instance.ListEnemyPicture[0].RoleObject.transform.localScale = new Vector3(450, 450, 450) * TextTranslator.instance.GetHeroInfoByHeroID(PictureCreater.instance.ListEnemyPicture[0].RoleID).heroScale;
        //PictureCreater.instance.ListEnemyPicture[0].RoleObject.transform.Rotate(new Vector3(0, 65, 0));
        PictureCreater.instance.ListEnemyPicture[0].RolePictureObject.transform.Rotate(new Vector3(0, 65, 0));
        PictureCreater.instance.ListEnemyPicture[0].RoleObject.transform.localScale = new Vector3(500, 500, 500);


        SkillIntroduce.text = TextTranslator.instance.GetSkillByID(mheroInfo.heroSkillList[0], 1).description;
        LabelIntroduce.text = TextTranslator.instance.GetSkillByID(mheroInfo.heroSkillList[1], 1).description;
        ShowActiveFate();
        //LabelIntroduce.text = mheroInfo.heroDescription;
        PictureCreater.instance.PlayRoleSound(RoleID);
    }

    string GetHeroName(HeroInfo heroInfo)
    {
        string name = null;
        int isOwne = 0;
        foreach (Hero item in CharacterRecorder.instance.ownedHeroList)
        {
            if (item.cardID == heroInfo.heroID)
            {
                isOwne++;
                name = heroInfo.heroName;
            }
        }


        return name;
    }
    public void HideBottomInfo()
    {
        BottomInfo.SetActive(false);
    }

    //缘分相关
    void SetHavedFateID(string _fateID)
    {
        PlayerPrefs.SetString("FateStr", GetHavedFateID() + _fateID.ToString() + "$");
    }

    string GetHavedFateID()
    {
        return PlayerPrefs.GetString("FateStr");
    }

    List<int> GetHavedFateIDList()
    {
        List<int> _fateList = new List<int>();
        string[] _fateStr = GetHavedFateID().Split('$');
        foreach (var item in _fateStr)
        {
            if (item != null && item != "")
            {
                _fateList.Add(int.Parse(item));
            }
        }

        return _fateList;
    }

    void ShowActiveFate()
    {
        List<int> _myFateList = GetHavedFateIDList();
        MyRoleFateList = TextTranslator.instance.GetMyRoleFateListByRoleID(RoleID);
        TextTranslator.instance.MyRoleFateList = MyRoleFateList;

        for (int i = 0; i < CharacterRecorder.instance.ListRoleFateId.Count; i++)
        {
            if (!_myFateList.Contains(CharacterRecorder.instance.ListRoleFateId[i]))
            {
                SetFateItemInfo(CharacterRecorder.instance.ListRoleFateId[i]);
                if (fateCount == 3)
                {
                    return;
                }
            }
        }
    }

    void SetFateItemInfo(int fateID)
    {
        int _fateIndex = GetFateIndex(fateID);
        string _desStr = _fateIndex == -1 ? null : GetDesStr(MyRoleFateList[_fateIndex].FateIDList);
        if (_desStr == null)
        {
            return;
        }
        fateCount++;
        GameObject _obj = NGUITools.AddChild(FateGrid.gameObject, FateItem) as GameObject;
        _obj.name = "FateItem" + FateGrid.transform.childCount;
        _obj.transform.GetChild(0).GetComponent<UILabel>().text = _fateIndex == -1 ? "" : MyRoleFateList[_fateIndex].Name + ":";
        _obj.transform.GetChild(1).GetComponent<UILabel>().text = _desStr + (_fateIndex == -1 ? "" : GetFateAttr(MyRoleFateList[_fateIndex]));
        FateGrid.Reposition();
        _obj.SetActive(true);
        FateObjList.Add(_obj);
        SetHavedFateID(fateID.ToString());
    }

    string GetDesStr(List<int> _itemIdList)
    {
        string _string = "同时拥有 [00ff00]";
        for (int i = 0; i < _itemIdList.Count; i++)
        {
            HeroInfo _HeroInfo = TextTranslator.instance.GetHeroInfoByHeroID(_itemIdList[i]);
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_itemIdList[i]);
            if (_HeroInfo != null)
            {
                _string += string.Format("{0},", _HeroInfo.heroName);
            }
            else if (mItemInfo != null)
            {
                //_string += string.Format("{0},", mItemInfo.itemName);
                return null;
            }
        }
        return _string;
    }
    string GetFateAttr(RoleFate _RoleFate)
    {
        string _attrStr = "";
        if (_RoleFate.Hp > 0)
        {
            _attrStr = string.Format("[FFFF00]生命增加+{0}%", _RoleFate.Hp * 100);
        }
        if (_RoleFate.Atk > 0)
        {
            _attrStr += string.Format("[FFFF00]攻击增加+{0}%", _RoleFate.Atk * 100);
        }
        return _attrStr;
    }
    int GetFateIndex(int fateId)
    {
        int count = -1;
        foreach (var item in MyRoleFateList)
        {
            count++;
            if (item.RoleFateID == fateId)
            {
                return count;
            }
        }
        return count;
    }


    //
    void PlaySkillAnimator()
    {
        Animator _Animator = Create3DHero.GetComponent<Animator>();
        _Animator.Play("skill");
        FightMotion fm2 = TextTranslator.instance.fightMotionDic[PictureCreater.instance.ListEnemyPicture[0].RoleID * 10 + 2];
        PictureCreater.instance.PlayEffect(PictureCreater.instance.ListEnemyPicture, 0, fm2);
    }

    void CloseLastWindow()
    {
        GameObject _lastWindow = GameObject.Find("CardWindow");
        if (_lastWindow != null)
        {
            DestroyImmediate(_lastWindow);
        }
    }

    void SetRarityIcon(GameObject _obj, int _heroRarity)
    {
        switch (_heroRarity)
        {
            case 1:
                _obj.GetComponent<UILabel>().color = Color.white;
                break;
            case 2:
                _obj.GetComponent<UILabel>().color = Color.blue;
                break;
            case 3:
                _obj.GetComponent<UILabel>().color = new Color(83f / 255f, 0f / 255f, 255f / 255f);
                break;
            case 4:
                _obj.GetComponent<UILabel>().color = new Color(255f / 255f, 121f / 255f, 0 / 255);
                break;
            case 5:
                _obj.GetComponent<UILabel>().color = Color.red;
                break;
            default:
                break;
        }
    }
    void GetSureHeroHave(int _RoleID, string x)
    {
        int count = 0;
        for (int i = 0; i < CharacterRecorder.instance.heroIdList.Count; i++)
        {
            if (CharacterRecorder.instance.heroIdList[i] == _RoleID)
            {
                count++;
                GoLabel.SetActive(false);
                HaveLabel.gameObject.SetActive(true);
                HaveHeroLabel.gameObject.SetActive(true);
                HaveHeroLabel.text = "你已经拥有此英雄，将转化为碎片";
            }
        }
        if (count == 0)
        {
            GoLabel.SetActive(true);
            HaveLabel.gameObject.SetActive(false);
        }

        CharacterRecorder.instance.upDateOwnenHeroList.Clear();
        foreach (var item in CharacterRecorder.instance.ownedHeroList)
        {
            CharacterRecorder.instance.upDateOwnenHeroList.Add(item);
        }
    }
    void GetSureHeroHave(int _RoleID)
    {
        int isOwne = 0;
        //Debug.LogError(CharacterRecorder.instance.upDateOwnenHeroList.Count);
        if (GameObject.Find("GaChaAwardWindow") != null)
        {
            GoLabel.SetActive(false);
            HaveLabel.gameObject.SetActive(true);
        }
        else
        {

            for (int i = 0; i < CharacterRecorder.instance.upDateOwnenHeroList.Count; i++)
            {
                if (CharacterRecorder.instance.upDateOwnenHeroList[i].cardID == _RoleID)
                {//已经拥有的英雄
                    isOwne++;
                    if (CharacterRecorder.instance.CompositeSuipian == 1)//从图鉴进入角色卡片
                    {
                        CharacterRecorder.instance.CompositeSuipian = 0;
                        GoLabel.SetActive(false);
                        HaveLabel.gameObject.SetActive(true);
                        break;
                    }
                    GoLabel.SetActive(false);
                    HaveLabel.gameObject.SetActive(true);
                    if (CharacterRecorder.instance.isGaChaFromItemClick == false)
                    {
                        HaveHeroLabel.gameObject.SetActive(true);
                        HaveHeroLabel.text = "你已经拥有此英雄，将转化为碎片";
                    }
                    break;
                }
            }
            if (isOwne == 0)
            {//未拥有
                if (CharacterRecorder.instance.CompositeSuipian == 1 || CharacterRecorder.instance.isGaChaFromItemClick)//从图鉴进入角色卡片
                {
                    CharacterRecorder.instance.CompositeSuipian = -1;
                    GoLabel.SetActive(false);
                    HaveLabel.gameObject.SetActive(true);
                }
                else
                {
                    GoLabel.SetActive(true);
                    HaveLabel.gameObject.SetActive(false);
                }
            }
        }
        //Debug.LogError("upDateOwnenHeroList:" + CharacterRecorder.instance.upDateOwnenHeroList.Count);
        //Debug.LogError("ownedHeroList:" + CharacterRecorder.instance.ownedHeroList.size);
        //重新更新已拥有前角色列表
        CharacterRecorder.instance.upDateOwnenHeroList.Clear();
        foreach (var item in CharacterRecorder.instance.ownedHeroList)
        {
            CharacterRecorder.instance.upDateOwnenHeroList.Add(item);
        }
        //Debug.LogError("upDateOwnenHeroList===:" + CharacterRecorder.instance.upDateOwnenHeroList.Count);
        #region 旧的
        /* foreach (Hero hero in CharacterRecorder.instance.ownedHeroList)
        {
            if (hero.cardID == _RoleID)//属于自己的英雄
            {
                isOwne++;
                foreach (int item in CharacterRecorder.instance.heroIdList)
                {
                    if (item == _RoleID)
                    {//招募之前就已经有了
                        isHave++;
                        if (GameObject.Find("GaChaGetWindow") != null)
                        {
                            GoLabel.SetActive(false);
                            HaveLabel.gameObject.SetActive(true);
                            HaveHeroLabel.gameObject.SetActive(true);
                            HaveHeroLabel.text = "你已经拥有此英雄，将转化为碎片";
                        }
                        else if (GameObject.Find("HeroMapWindow") != null)
                        {//从图鉴进入
                            foreach (Hero myHero in CharacterRecorder.instance.ownedHeroList)
                            {
                                if (myHero.cardID == _RoleID)
                                {
                                    GoLabel.SetActive(false);
                                    HaveLabel.gameObject.SetActive(true);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            GoLabel.SetActive(false);
                            HaveLabel.gameObject.SetActive(true);
                            HaveHeroLabel.gameObject.SetActive(true);
                            HaveHeroLabel.text = "你已经拥有此英雄，将转化为碎片";
                        }
                    }
                }
                if (isHave == 0)
                {//招募之前没有     
                    if (GameObject.Find("HeroMapWindow") != null || GameObject.Find("LoginSignWindow") != null)
                    {//从图鉴进入
                        if (CharacterRecorder.instance.CompositeSuipian == 0)//从图鉴合成进入角色卡片
                        {
                            CharacterRecorder.instance.CompositeSuipian = -1;
                            GoLabel.SetActive(true);
                            HaveLabel.gameObject.SetActive(false);
                        }
                        else
                        {
                            GoLabel.SetActive(false);
                            HaveLabel.gameObject.SetActive(true);
                        }

                    }
                    else
                    {
                        GoLabel.SetActive(true);
                        HaveLabel.gameObject.SetActive(false);
                    }
                    //if (GameObject.Find("GaChaGetWindow") != null || GameObject.Find("MainWindow") != null || GameObject.Find("BagWindow") != null || GameObject.Find("HeroMapWindow") != null)
                    //{

                    //}
                    //else
                    //{

                    //}

                }
            }
        }
        if (isOwne == 0)//不是自己的英雄
        {
            Debug.LogError("!!!!!!!!!!!!!!!!!!!!!");
            GoLabel.SetActive(false);
            HaveLabel.gameObject.SetActive(true);
        }
        * */
        #endregion
        //CharacterRecorder.instance.ownedHeroBeforeList = CharacterRecorder.instance.ownedHeroList;
    }
}
