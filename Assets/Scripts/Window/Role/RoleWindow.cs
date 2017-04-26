using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoleWindow : MonoBehaviour
{
    public GameObject MyGrid;
    public GameObject MyHeroItem;
    public List<GameObject> ListTabs = new List<GameObject>();
    private List<GameObject> ListHero = new List<GameObject>();

    public GameObject HeroInfoPart;
    public GameObject HeroLevelUpPart;
    public GameObject HeroAdvancedPart;
    public GameObject HeroTrainPart;
    public GameObject HeroBreakUpPart;
    public GameObject HeroSkillPart;
    public GameObject HeroDowerPart;

    public int ClickIndex = 0;
    public int CharacterRoleID = 0;
    public int RoleID = 0;

    public static int targetRoleCardId = 0;
    private int RoleIndex = -1;
    private HeroInfo _heroInfo;
    private Hero curHero;
    private Dictionary<int, bool> listTabsOpenState = new Dictionary<int, bool>();
    private bool IsCheckLeavingSkillUI = false;
    void OnEnable()
    {
        listTabsOpenState.Add(1, true);
        listTabsOpenState.Add(2, true);
        listTabsOpenState.Add(3, true);
        listTabsOpenState.Add(4, true);
        listTabsOpenState.Add(5, true);
        listTabsOpenState.Add(6, true);
        listTabsOpenState.Add(7, true);
        SortHeroListByForce();
        InitHeroList();
    }
    void OnDestroy()
    {
        SortHeroListByForce();
    }
    // Use this for initialization
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.英雄);
        UIManager.instance.UpdateSystems(UIManager.Systems.英雄);

        NetworkHandler.instance.SendProcess(string.Format("8018#{0};", ""));
        PlayAniOrRotation.IsNeedAutoPlayAni = true;
        //Debug.LogError("HeroClick");
        InitOpenState();
        //   SetTab(1);
        if (CharacterRecorder.instance.enterRoleFromMain)
        {
            SetTab(1);
            CharacterRecorder.instance.enterRoleFromMain = false;
        }
        else
        {
            //Debug.LogError("RoleTabIndex............." + CharacterRecorder.instance.RoleTabIndex);
            // if (GameObject.Find("ResultWindow") == null)
            {
                SetTab(CharacterRecorder.instance.RoleTabIndex);
            }
            if (CharacterRecorder.instance.RoleTabIndex == 6)
            {
                for (int i = 0; i < 5; i++)
                {
                    ListTabs[i].GetComponent<UIToggle>().enabled = true;
                }
            }
            //CharacterRecorder.instance.RoleTabIndex = 1;

        }
        //if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 9 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName())==4)
        //{
        //    PlayerPrefs.SetInt(LuaDeliver.instance.GetGuideSubStateName(), PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) + 1);
        //}
    }
    void InitOpenState()
    {
        for (int i = 0; i < ListTabs.Count; i++)
        {
            UIEventListener.Get(ListTabs[i]).onClick = ClickListTabs;
            switch (ListTabs[i].name)
            {
                case "SpriteTab1": break;
                case "SpriteTab2": break;
                case "SpriteTab3":
                    //ListTabs[i].GetComponent<UIToggle>().enabled = true;
                    //ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(false);
                    if (CharacterRecorder.instance.lastGateID - 1 >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.heroStrength).Level)
                    {
                        //ListTabs[i].GetComponent<UIToggle>().enabled = true;
                        ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(false);
                    }
                    else
                    {
                        //ListTabs[i].GetComponent<UIToggle>().value = false;
                        StartCoroutine(DelaySetUIToggleFalse(ListTabs[i].GetComponent<UIToggle>()));
                        ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(true);
                        listTabsOpenState[3] = false;
                    }
                    break;
                case "SpriteTab4": break;
                case "SpriteTab5"://军衔开启关卡限制
                    /*        if (CharacterRecorder.instance.lastGateID <= 10008)
                            {
                                ListTabs[i].GetComponent<UIToggle>().value = false;
                                //ListTabs[i].GetComponent<UIToggle>().enabled = false;
                                StartCoroutine(DelaySetUIToggleFalse(ListTabs[i].GetComponent<UIToggle>()));
                                ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(true);
                                listTabsOpenState[5] = false;
                            }
                            else
                            {
                                ListTabs[i].GetComponent<UIToggle>().enabled = true;
                                ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(false);
                            }*/
                    ListTabs[i].GetComponent<UIToggle>().enabled = true;
                    ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(false);
                    break;
                case "SpriteTab6":
                    ListTabs[i].GetComponent<UIToggle>().enabled = true;
                    ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(false);
                    break;
                case "SpriteTab7": break;
            }
        }
    }
    IEnumerator DelaySetUIToggleFalse(UIToggle _UIToggle)
    {
        yield return new WaitForSeconds(0.5f);
        _UIToggle.enabled = false;
    }
    //战力降序排序
    void SortHeroListByForce()
    {
        int listSize = CharacterRecorder.instance.ownedHeroList.size;
        for (int i = 0; i < listSize; i++)
        {
            for (var j = listSize - 1; j > i; j--)
            {
                Hero heroA = CharacterRecorder.instance.ownedHeroList[i];
                Hero heroB = CharacterRecorder.instance.ownedHeroList[j];
                if (heroA.force < heroB.force)
                {
                    var temp = CharacterRecorder.instance.ownedHeroList[i];
                    CharacterRecorder.instance.ownedHeroList[i] = CharacterRecorder.instance.ownedHeroList[j];
                    CharacterRecorder.instance.ownedHeroList[j] = temp;
                }
            }
        }

    }
    void InitHeroList()
    {
        int i = 0;
        foreach (var _hero in CharacterRecorder.instance.ownedHeroList)
        {
            //Debug.Log(h.cardID);
            if (CharacterRoleID == 0)
            {
                CharacterRoleID = _hero.characterRoleID;
                RoleID = _hero.cardID;
            }
            GameObject go = NGUITools.AddChild(MyGrid, MyHeroItem);
            go.name = "HeroItem" + _hero.cardID;
            go.AddComponent<RoleHeroItem>();
            //go.GetComponent<RoleHeroItem>().RoleID = h.cardID;
            //go.GetComponent<RoleHeroItem>().CharacterRoleID = h.characterRoleID;
            go.GetComponent<RoleHeroItem>().Init(true, RoleRedPointType.HeroTabsRedPint, _hero.cardID, _hero.characterRoleID, i);
            go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("ScrollView").GetComponent<UIScrollView>();

            ListHero.Add(go);
            i++;
        }

        MyGrid.GetComponent<UIGrid>().Reposition();
        //     SetHeroClick(0);
        if (targetRoleCardId == 0)
        {
            SetHeroClick(0);
        }
        else
        {
            for (int j = 0; j < CharacterRecorder.instance.ownedHeroList.size; j++)
            {
                if (targetRoleCardId == CharacterRecorder.instance.ownedHeroList[j].cardID)
                {
                    SetHeroClick(j);
                    break;
                }
            }
        }
    }
    //刷新hero头像
    public void UpDateDownHeroIcon()
    {
        for (int i = ListHero.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(ListHero[i]);
        }
        ListHero.Clear();

        int j = 0;
        foreach (var h in CharacterRecorder.instance.ownedHeroList)
        {
            if (CharacterRoleID == h.characterRoleID)
            {
                RoleIndex = j;
            }
            if (CharacterRoleID == 0)
            {
                CharacterRoleID = h.characterRoleID;
                RoleID = h.cardID;
            }
            GameObject go = NGUITools.AddChild(MyGrid, MyHeroItem);
            go.name = "HeroItem" + h.cardID;
            go.AddComponent<RoleHeroItem>();
            //go.GetComponent<RoleHeroItem>().RoleID = h.cardID;
            //go.GetComponent<RoleHeroItem>().CharacterRoleID = h.characterRoleID;
            go.GetComponent<RoleHeroItem>().Init(true, RoleRedPointType.HeroTabsRedPint, h.cardID, h.characterRoleID, j);
            go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("ScrollView").GetComponent<UIScrollView>();

            ListHero.Add(go);
            j++;
        }
        MyGrid.GetComponent<UIGrid>().Reposition();

        //      SetHeroClick(-1);
        SetHeroClick(RoleIndex);
    }

    public void SetHeroClick(int Index)
    {
        HeroTrainPart _HeroTrainPart = HeroTrainPart.GetComponent<HeroTrainPart>();
        if (_HeroTrainPart.mHero != null && _HeroTrainPart.IsHeroTrained == true)
        {
            Debug.Log("培养后重新获取英雄属性");
            NetworkHandler.instance.SendProcess("1005#" + _HeroTrainPart.mHero.characterRoleID + ";");
            _HeroTrainPart.IsHeroTrained = false;
        }
        if (Index == -1)
        {
            Index = RoleIndex;
        }
        else
        {
            RoleIndex = Index;
            TextTranslator.instance.HeadIndex = RoleIndex;
        }
        foreach (GameObject go in ListHero)
        {
            go.transform.localScale = Vector3.one;
            go.GetComponent<RoleHeroItem>().Select.SetActive(false);
        }
        ListHero[Index].GetComponent<RoleHeroItem>().Select.SetActive(true);
        SetHero(Index);
    }

    //设置模块信息
    public void SetHero(int Index)
    {
        //Debug.Log("Index" + Index);
        Hero h = CharacterRecorder.instance.ownedHeroList[Index];
        HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(h.cardID);
        _heroInfo = hinfo;
        curHero = h;
        CharacterRoleID = h.characterRoleID;
        SetRedPointStateOfTabs();
        if (HeroInfoPart.activeSelf)
        {
            HeroInfoPart.GetComponent<HeroInfoPart>().SetHero(Index);
        }
        else if (HeroLevelUpPart.activeSelf)
        {
            HeroLevelUpPart.GetComponent<HeroLevelUpPart>().StarPart(CharacterRoleID, h);
        }
        else if (HeroAdvancedPart.activeSelf)
        {
            HeroAdvancedPart.GetComponent<HeroAdvancedPart>().StartPart(CharacterRoleID, h);
        }
        else if (HeroTrainPart.activeSelf)
        {
            HeroTrainPart.GetComponent<HeroTrainPart>().StartPart(h);
        }
        else if (HeroBreakUpPart.activeSelf)
        {
            HeroBreakUpPart.GetComponent<HeroBreakUpPart>().StartPart(h);
        }
        else if (HeroSkillPart.activeSelf)
        {
            HeroSkillPart.GetComponent<HeroSkillPart>().StartPart(h);
        }
        else if (HeroDowerPart.activeSelf)
        {
            HeroDowerPart.GetComponent<HeroDowerPart>().SetItemList(h);
        }
    }
    public void SetRedPointStateOfTabs()
    {
        for (int i = 0; i < ListTabs.Count; i++)
        {
            if (i == 2 || i == 3 || i == 4 || i == 6)
            {
                ListTabs[i].transform.FindChild("RedPoint").gameObject.SetActive(GetRedPointState(i, curHero));
            }
        }

    }
    bool GetShengPinOneContionState(List<Item> classUpList)
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
    bool GetRedPointState(int _tabIndex, Hero _curHero)
    {
        switch (_tabIndex)
        {
            case 2:
                int count = TextTranslator.instance.GetItemCountByID(10500) != null ? TextTranslator.instance.GetItemCountByID(10500) : 0;
                if (count >= 50 && CharacterRecorder.instance.lastGateID > 10030)
                {
                    return true;
                }
                break;
            case 3://升品
                RoleClassUp rcu = TextTranslator.instance.GetRoleClassUpInfoByID(_curHero.cardID, _curHero.classNumber);
                if (_curHero.level >= rcu.Levelcap && GetShengPinOneContionState(rcu.NeedItemList) == true)
                {
                    return true;
                }
                break;
            case 4://军衔
                RoleBreach rb = TextTranslator.instance.GetRoleBreachByID(_curHero.cardID, _curHero.rank);
                int item1Count = TextTranslator.instance.GetItemCountByID(10102);
                int item2Count = TextTranslator.instance.GetItemCountByID(rb.roleId + 10000);
                if (CharacterRecorder.instance.lastGateID > 10008 && _curHero.level >= rb.levelCup && item1Count >= rb.stoneNeed && item2Count >= rb.debrisNum)//关卡限制
                {
                    return true;
                }
                break;
            case 6://天赋
              int num=  TextTranslator.instance.GetNowNum(_curHero.characterRoleID);
                if (num>0)
                {
                    return true;
                }
                break;
            default: break;
        }
        return false;
    }
    #region 旧的，应该没用了
    public void SetTab1()
    {
        if (UIToggle.current.value)
        {
            SetTab(1);
        }
    }

    public void SetTab2()
    {
        if (UIToggle.current.value)
        {
            SetTab(2);
        }
    }

    public void SetTab3()
    {
        if (UIToggle.current.value)
        {
            SetTab(3);
        }
    }

    public void SetTab4()
    {
        if (UIToggle.current.value)
        {
            SetTab(4);
        }
    }

    public void SetTab5()
    {
        if (UIToggle.current.value)
        {
            SetTab(5);
        }
    }

    public void SetTab6()
    {
        if (UIToggle.current.value)
        {
            SetTab(6);
        }

    }
    #endregion
    private void ClickListTabs(GameObject go)
    {
        if (SceneTransformer.instance.CheckGuideIsFinish() == false)
        {
            if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 9 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 4)
            {
                SceneTransformer.instance.NewGuideButtonClick();
            }
        }
        else
        {
            if (CharacterRecorder.instance.GuideID[26] == 3 || CharacterRecorder.instance.GuideID[37] == 3 || CharacterRecorder.instance.GuideID[30] == 6 || CharacterRecorder.instance.GuideID[31] == 6)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
        }
        HeroTrainPart _HeroTrainPart = HeroTrainPart.GetComponent<HeroTrainPart>();
        if (_HeroTrainPart.mHero != null && _HeroTrainPart.IsHeroTrained == true)
        {
            Debug.Log("培养后重新获取英雄属性");
            NetworkHandler.instance.SendProcess("1005#" + _HeroTrainPart.mHero.characterRoleID + ";");
            _HeroTrainPart.IsHeroTrained = false;
        }
        //if (CheckSkillTab(RoleIndex, go.name))
        //if (CheckSkillTab(RoleIndex, CharacterRecorder.instance.RoleTabIndex))

        bool _IsNeedShowTips = CheckToShowTipsOfLeavingSkill(RoleIndex, int.Parse(go.name[9].ToString()));
        if (_IsNeedShowTips)
        {
            #region
            /* switch(go.name)
             {
            
                 case "SpriteTab1": SetTab(1); go.GetComponent<UIToggle>().value = true; break;
                 case "SpriteTab2": SetTab(2); go.GetComponent<UIToggle>().value = true; break;
                 case "SpriteTab3":
                     if (CharacterRecorder.instance.level >= 21)
                     {
                         SetTab(3);
                         go.GetComponent<UIToggle>().value = true;
                     }
                     else
                     {
                         go.GetComponent<UIToggle>().value = false;
                         UIManager.instance.OpenPromptWindow(string.Format("{0}级开放培养", 21), 11, false, PromptWindow.PromptType.Hint, null, null);
                     }
                     break;
                 case "SpriteTab4": SetTab(4); go.GetComponent<UIToggle>().value = true; break;
                 case "SpriteTab5": SetTab(5); go.GetComponent<UIToggle>().value = true; break;
                 case "SpriteTab6":
                     if (CharacterRecorder.instance.level >= 29)
                     {
                         SetTab(6);
                         go.GetComponent<UIToggle>().value = true;
                     }
                     else
                     {
                         go.GetComponent<UIToggle>().value = false;
                         UIManager.instance.OpenPromptWindow(string.Format("{0}级开放技能突破", 29), 11, false, PromptWindow.PromptType.Hint, null, null);
                     }
                     break; 
             }*/
            #endregion
            return;
        }
        switch (go.name)
        {
            case "SpriteTab1": SetTab(1); go.GetComponent<UIToggle>().value = true; break;
            case "SpriteTab2": SetTab(2); go.GetComponent<UIToggle>().value = true; break;
            case "SpriteTab3":
                if (CharacterRecorder.instance.lastGateID - 1 >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.heroStrength).Level)
                {
                    SetTab(3);
                    go.GetComponent<UIToggle>().value = true;
                }
                else
                {
                    go.GetComponent<UIToggle>().value = false;
                    UIManager.instance.OpenPromptWindow(string.Format("通过{0}关开放培养", TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.heroStrength).Level - 10000), 11, false, PromptWindow.PromptType.Hint, null, null);
                }
                //SetTab(3);
                //go.GetComponent<UIToggle>().value = true;
                break;
            case "SpriteTab4": SetTab(4); go.GetComponent<UIToggle>().value = true; break;
            case "SpriteTab5": //SetTab(5); go.GetComponent<UIToggle>().value = true; break;
                /*   if (CharacterRecorder.instance.lastGateID <= 10008)
                   {
                       go.GetComponent<UIToggle>().value = false;
                       UIManager.instance.OpenPromptWindow("打过第8关开放军衔", 11, false, PromptWindow.PromptType.Hint, null, null);
                   }
                   else
                   {
                       SetTab(5); 
                       go.GetComponent<UIToggle>().value = true;
                   }*/
                SetTab(5);
                go.GetComponent<UIToggle>().value = true;
                break;
            case "SpriteTab6":
                //if (CharacterRecorder.instance.level >= 29)
                /*   if (CharacterRecorder.instance.lastGateID > 10078)
                   {
                       SetTab(6);
                       go.GetComponent<UIToggle>().value = true;
                   }
                   else
                   {
                       go.GetComponent<UIToggle>().value = false;
                       UIManager.instance.OpenPromptWindow(string.Format("{0}关开放技能突破", 78), 11, false, PromptWindow.PromptType.Hint, null, null);
                   }*/
                SetTab(6);
                go.GetComponent<UIToggle>().value = true;
                break;
            case "SpriteTab7": SetTab(7); go.GetComponent<UIToggle>().value = true; break;
        }
    }
    int clickRoleIndex;
    int clickTabNum;
    #region 提示是否确认离开技能界面
    bool CheckToShowTipsOfLeavingSkill(int clickRoleIndex, int clickTabNum)
    {
        //Debug.LogError("clickTabNum..." + clickTabNum);
        this.clickRoleIndex = clickRoleIndex;
        this.clickTabNum = clickTabNum;
        if (CharacterRecorder.instance.RoleTabIndex == 6 && CharacterRecorder.instance.RoleTabIndex != clickTabNum && HeroSkillPart.GetComponent<HeroSkillPart>().SkillSlider.GetComponent<UISlider>().value < 1.0f && listTabsOpenState[clickTabNum] == true)
        {
            //Debug.LogError("弹出提示");
            if (HeroSkillPart.GetComponent<HeroSkillPart>().SkillSlider.GetComponent<UISlider>().value > 0.0f)
            {
                UIManager.instance.OpenPromptWindow("当前技能槽内还有经验，每天凌晨5点清空，建议完成升级，避免损失。", PromptWindow.PromptType.Confirm, CheckLeavingSkillUI, null);
            }
            else
            {
                CheckLeavingSkillUI();
            }

            return true;
        }
        if (RoleIndex != clickRoleIndex)
        {
            //Debug.LogError("弹出提示");
        }
        return false;
    }
    void CheckLeavingSkillUI()
    {
        CharacterRecorder.instance.RoleTabIndex = clickTabNum;
        RoleIndex = clickRoleIndex;
        GameObject go = ListTabs[CharacterRecorder.instance.RoleTabIndex - 1];
        switch (go.name)
        {
            case "SpriteTab1": SetTab(1); go.GetComponent<UIToggle>().value = true; break;
            case "SpriteTab2": SetTab(2); go.GetComponent<UIToggle>().value = true; break;
            case "SpriteTab3":
                if (CharacterRecorder.instance.level >= 21)
                {
                    SetTab(3);
                    go.GetComponent<UIToggle>().value = true;
                }
                else
                {
                    go.GetComponent<UIToggle>().value = false;
                    UIManager.instance.OpenPromptWindow(string.Format("{0}级开放培养", 21), 11, false, PromptWindow.PromptType.Hint, null, null);
                }
                break;
            case "SpriteTab4": SetTab(4); go.GetComponent<UIToggle>().value = true; break;
            case "SpriteTab5": SetTab(5); go.GetComponent<UIToggle>().value = true; break;
            case "SpriteTab6":
                //if (CharacterRecorder.instance.level >= 29)
                if (CharacterRecorder.instance.lastGateID > 10078)
                {
                    SetTab(6);
                    go.GetComponent<UIToggle>().value = true;
                }
                else
                {
                    go.GetComponent<UIToggle>().value = false;
                    UIManager.instance.OpenPromptWindow(string.Format("{0}关开放技能突破", 78), 11, false, PromptWindow.PromptType.Hint, null, null);
                }
                break;
            case "SpriteTab7": SetTab(7); go.GetComponent<UIToggle>().value = true; break;
        }
        for (int i = 0; i < 6; i++)
        {
            ListTabs[i].GetComponent<UIToggle>().enabled = true;
        }
    }
    bool CheckSkillTab(int NowIndex, int NowTab)
    {
        if (ListTabs[5].GetComponent<UIToggle>().value)
        {
            //Debug.LogError("CheckSkillTab" + false);
            return false;
        }
        return true;
    }
    #endregion
    public void SetTab(int Index)
    {
        CharacterRecorder.instance.RoleTabIndex = Index;
        //Debug.LogError(Index);
        if (Index == 1)
        {
            HeroInfoPart.SetActive(true);
            HeroLevelUpPart.SetActive(false);
            HeroTrainPart.SetActive(false);
            HeroAdvancedPart.SetActive(false);
            HeroBreakUpPart.SetActive(false);
            HeroSkillPart.SetActive(false);
            HeroDowerPart.SetActive(false);
            SetHeroClick(-1);
        }
        else if (Index == 2)
        {
            //Debug.LogError("....Index..." + Index);
            HeroInfoPart.SetActive(false);
            HeroLevelUpPart.SetActive(true);
            HeroTrainPart.SetActive(false);
            HeroAdvancedPart.SetActive(false);
            HeroBreakUpPart.SetActive(false);
            HeroSkillPart.SetActive(false);
            HeroDowerPart.SetActive(false);
            HeroLevelUpPart.GetComponent<HeroLevelUpPart>().StarPart(CharacterRoleID, curHero);
        }
        else if (Index == 3)
        {
            HeroInfoPart.SetActive(false);
            HeroLevelUpPart.SetActive(false);
            HeroTrainPart.SetActive(true);
            HeroAdvancedPart.SetActive(false);
            HeroBreakUpPart.SetActive(false);
            HeroSkillPart.SetActive(false);
            HeroDowerPart.SetActive(false);
            HeroTrainPart.GetComponent<HeroTrainPart>().StartPart(curHero);
        }
        else if (Index == 4)
        {
            HeroInfoPart.SetActive(false);
            HeroLevelUpPart.SetActive(false);
            HeroTrainPart.SetActive(false);
            HeroAdvancedPart.SetActive(true);
            HeroBreakUpPart.SetActive(false);
            HeroSkillPart.SetActive(false);
            HeroDowerPart.SetActive(false);
            HeroAdvancedPart.GetComponent<HeroAdvancedPart>().StartPart(CharacterRoleID, curHero);
        }
        else if (Index == 5)
        {
            HeroInfoPart.SetActive(false);
            HeroLevelUpPart.SetActive(false);
            HeroTrainPart.SetActive(false);
            HeroAdvancedPart.SetActive(false);
            HeroBreakUpPart.SetActive(true);
            HeroSkillPart.SetActive(false);
            HeroDowerPart.SetActive(false);
            HeroBreakUpPart.GetComponent<HeroBreakUpPart>().StartPart(curHero);
        }
        else if (Index == 6)
        {
            HeroInfoPart.SetActive(false);
            HeroLevelUpPart.SetActive(false);
            HeroTrainPart.SetActive(false);
            HeroAdvancedPart.SetActive(false);
            HeroBreakUpPart.SetActive(false);
            HeroSkillPart.SetActive(true);
            HeroDowerPart.SetActive(false);
            HeroSkillPart.GetComponent<HeroSkillPart>().StartPart(curHero);
        }
        else if (Index == 7)
        {
            HeroInfoPart.SetActive(false);
            HeroLevelUpPart.SetActive(false);
            HeroTrainPart.SetActive(false);
            HeroAdvancedPart.SetActive(false);
            HeroBreakUpPart.SetActive(false);
            HeroSkillPart.SetActive(false);
            HeroDowerPart.SetActive(true);
            HeroDowerPart.GetComponent<HeroDowerPart>().SetItemList(curHero);
        }

        //GameObject.Find(string.Format("SpriteTab{0}", Index)).GetComponent<UIToggle>().value = true;
        ListTabs[Index - 1].GetComponent<UIToggle>().value = true;
    }
}
