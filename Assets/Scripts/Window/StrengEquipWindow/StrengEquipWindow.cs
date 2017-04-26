using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class StrengEquipWindow : MonoBehaviour
{
    public static bool IsEnterEquipUIFromGrabGoods = false;
    public GameObject MyGrid;
    public GameObject MyHeroItem;
    public GameObject MasterBtn;

    public GameObject topRightObj;
    public GameObject equipStrengPart;
    public GameObject baoWuStrengPart;
    public GameObject equipRefinePart;
    public GameObject baoWuRefinePart;
    public GameObject awakeWindowPart;
    public GameObject secretStoneWindowPart;
    public GameObject NuclearWeaponWindow;
    public GameObject LuxuryCarWindow;
    public int CharacterRoleID = 0;
    public int RoleID = 0;

    public UISprite EquipIcon;
    public UILabel EquipNameLabel;
    public UILabel LeftInfoLabel;
    public UILabel RightInfoLabel;
    public UILabel LeftInfoLabelNew;
    public UILabel RightInfoLabelNew;


    [SerializeField]
    private UISprite RarityIcon;

    public GameObject masterSucess;


    private string InfoDesStr;
    private string DataStr;

    private string currentTabName;

    Hero mHero;
    HeroInfo mHeroInfo;
    public List<GameObject> ListHero = new List<GameObject>();
    public GameObject LevelUpEffect;
    public List<GameObject> ListEquip = new List<GameObject>();
    [SerializeField]
    public List<GameObject> ListTabs = new List<GameObject>();
    int RoleIndex = -1;

    public static int ClickIndex = 1; //装备位置
    private Hero.EquipInfo mEquipInfo;
    private Hero.EquipInfoBefore mEquipInfoBefore = new Hero.EquipInfoBefore();
    public static int strengType = 0;// 0 - 强化，1- 精炼 2 - 晋级（觉醒）  3 - 秘宝
    private static int equipType = 0;// 0 - 装备，1- 宝物
    private int canAwakeBaoWuPos = 5;//可晋级的饰品，有红点的饰品位置
    private bool IsOnlyAfterBaoWuUp = false;
    //private bool IsNeedResetHedIndexAfterSortHero = false;
    private BetterList<Hero.EquipInfo> myHeroEquipList;


    void OnEnable()
    {
        SortHeroListByForce();
    }
    // Use this for initialization
    void Start()
    {

        currentTabName = "SpriteTab1";
        UIManager.instance.CountSystem(UIManager.Systems.装备);
        UIManager.instance.UpdateSystems(UIManager.Systems.装备);

        if (CharacterRecorder.instance.isNeedRecordStrengTabType == false && StrengEquipWindow.IsEnterEquipUIFromGrabGoods == false)
        {
            strengType = 0;
        }
        if (!IsEnterEquipUIFromGrabGoods)
        {
            PlayAniOrRotation.IsNeedAutoPlayAni = true;
            //Debug.LogError("HeroClick");
            //IsNeedResetHedIndexAfterSortHero = true;
            InitHeroList();
            AddDaoJuEffect(EquipIcon);
            /* for (int i = 0; i < ListEquip.Count;i++ )
             {
                 UIEventListener.Get(ListEquip[i]).onClick = ClickListEquip;
             }*/
            for (int i = 0; i < ListTabs.Count; i++)
            {
                UIEventListener.Get(ListTabs[i]).onClick += ClickListTabs;
            }
            InitEquipItemOpenState();
            SetTab(strengType);
        }
        if (CharacterRecorder.instance.setEquipTableIndex != 0 && CharacterRecorder.instance.setEquipTableIndex < ListTabs.Count)
        {
            Invoke("ShowTables", 0.2f);

        }
        UIEventListener.Get(MasterBtn).onClick += delegate(GameObject go)
        {
            if (CharacterRoleID != 0)
            {
                UIManager.instance.CountSystem(UIManager.Systems.强化大师);
                UIManager.instance.UpdateSystems(UIManager.Systems.强化大师);
                UIManager.instance.OpenSinglePanel("StrengthenMasterWindow", false);
                NetworkHandler.instance.SendProcess(string.Format("3401#{0};", CharacterRoleID.ToString()));
            }
        };
        IsEnterEquipUIFromGrabGoods = false;
    }

    void ShowTables()
    {
        if (CharacterRecorder.instance.setEquipTableIndex != 0 && CharacterRecorder.instance.setEquipTableIndex < ListTabs.Count)
        {
            ClickListTabs(ListTabs[CharacterRecorder.instance.setEquipTableIndex]);
            CharacterRecorder.instance.setEquipTableIndex = 0;
        }
    }
    void InitOpenState(int _equipPos)
    {
        for (int i = 0; i < ListTabs.Count; i++)
        {
            //UIEventListener.Get(ListTabs[i]).onClick += ClickListTabs;
            switch (ListTabs[i].name)
            {
                case "SpriteTab1": break;
                case "SpriteTab2":
                    /*  if (_equipPos < 5)
                      {
                          //if (CharacterRecorder.instance.level >= 31)
                          if (CharacterRecorder.instance.lastGateID > 10082)
                          {
                              ListTabs[i].GetComponent<UIToggle>().enabled = true;
                              ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(false);
                          }
                          else
                          {
                              ListTabs[i].GetComponent<UIToggle>().value = false;
                              StartCoroutine(DelaySetUIToggleFalse(ListTabs[i].GetComponent<UIToggle>()));
                              ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(true);
                          }
                      }
                      else
                      {
                          ListTabs[i].GetComponent<UIToggle>().enabled = true;
                          ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(false);
                      }*/
                    if (CharacterRecorder.instance.lastGateID > TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.shipingjinglian).Level)
                    {
                        ListTabs[i].GetComponent<UIToggle>().enabled = true;
                        ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(false);
                    }
                    else
                    {
                        ListTabs[i].GetComponent<UIToggle>().value = false;
                        StartCoroutine(DelaySetUIToggleFalse(ListTabs[i].GetComponent<UIToggle>()));
                        ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(true);
                    }
                    break;
                case "SpriteTab3":
                    //if (CharacterRecorder.instance.level >= 16)
                    if (CharacterRecorder.instance.lastGateID > 10022)
                    {
                        ListTabs[i].GetComponent<UIToggle>().enabled = true;
                        ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(false);
                    }
                    else
                    {
                        ListTabs[i].GetComponent<UIToggle>().value = false;
                        StartCoroutine(DelaySetUIToggleFalse(ListTabs[i].GetComponent<UIToggle>()));
                        ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(true);
                    }
                    break;
                case "SpriteTab4":
                    if (CharacterRecorder.instance.lastGateID >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zuduifuben).Level)// && CharacterRecorder.instance.level >= 24 
                    {
                        ListTabs[i].GetComponent<UIToggle>().enabled = true;
                        ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(false);
                    }
                    else
                    {
                        ListTabs[i].GetComponent<UIToggle>().value = false;
                        StartCoroutine(DelaySetUIToggleFalse(ListTabs[i].GetComponent<UIToggle>()));
                        ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(true);
                    }
                    break;
                case "SpriteTab5":
                    if (CharacterRecorder.instance.lastGateID >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zuojia).Level)// && CharacterRecorder.instance.level >= 24 
                    {
                        ListTabs[i].GetComponent<UIToggle>().enabled = true;
                        ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(false);
                    }
                    else
                    {
                        ListTabs[i].GetComponent<UIToggle>().value = false;
                        StartCoroutine(DelaySetUIToggleFalse(ListTabs[i].GetComponent<UIToggle>()));
                        ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(true);
                    }
                    break;
                case "SpriteTab6":
                    if (GetHeWuQiCount() > 0 || CharacterRecorder.instance.lastGateID >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.hewuqi).Level)// && CharacterRecorder.instance.level >= 24 
                    {
                        ListTabs[i].GetComponent<UIToggle>().enabled = true;
                        ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(false);
                    }
                    else
                    {
                        ListTabs[i].GetComponent<UIToggle>().value = false;
                        StartCoroutine(DelaySetUIToggleFalse(ListTabs[i].GetComponent<UIToggle>()));
                        ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(true);
                    }
                    break;
            }
        }

    }

    public int GetHeWuQiCount()
    {
        int count = 0;
        if (TextTranslator.instance.GetItemCountByID(85001) >= 40
            || TextTranslator.instance.GetItemCountByID(85101) >= 40
            || TextTranslator.instance.GetItemCountByID(85102) >= 40
            || TextTranslator.instance.GetItemCountByID(85103) >= 40
            || TextTranslator.instance.GetItemCountByID(85201) >= 40
            || TextTranslator.instance.GetItemCountByID(85202) >= 40
            || TextTranslator.instance.GetItemCountByID(85203) >= 40
            || TextTranslator.instance.GetItemCountByID(85204) >= 40
            || TextTranslator.instance.GetItemCountByID(85205) >= 40
            || TextTranslator.instance.GetItemCountByID(85206) >= 40
            || TextTranslator.instance.GetItemCountByID(85207) >= 40
            || TextTranslator.instance.GetItemCountByID(85208) >= 40
            || GetWeaponList() || CharacterRecorder.instance.IsOpenWeapon)
        {
            count = 1;
        }
        return count;
    }

    private bool GetWeaponList()
    {
        bool isTrue = false;
        if (!CharacterRecorder.instance.IsOpenWeapon)
        {
            foreach (var item in CharacterRecorder.instance.ownedHeroList)
            {
                for (int i = 0; i < item.WeaponList.size; i++)
                {
                    if (item.WeaponList[i].WeaponClass != 0 || item.WeaponList[i].WeaponStar != 0)
                    {
                        CharacterRecorder.instance.IsOpenWeapon = true;
                        break;
                    }
                }
            }
        }

        for (int i = 0; i < mHero.WeaponList.size; i++)
        {
            if (mHero.WeaponList[i].WeaponClass != 0 ||
                // mHero.WeaponList[i].WeaponID != 0 ||
                mHero.WeaponList[i].WeaponStar != 0)
            {
                isTrue = true;
                break;
            }
        }
        return isTrue;
    }
    private void ClickListTabs(GameObject go)
    {
        CharacterRecorder.instance.isOneKeyState = false;
        if (currentTabName == go.name)
        {
            return;
        }
        currentTabName = go.name;
        switch (go.name)
        {
            case "SpriteTab1": SetTab(0); go.GetComponent<UIToggle>().value = true; break;
            case "SpriteTab2":
                /* if (ClickIndex < 5)
                 {
                     //if (CharacterRecorder.instance.level >= 31)
                     if(CharacterRecorder.instance.lastGateID > 10082)
                     {
                         SetTab(1);
                         go.GetComponent<UIToggle>().value = true;
                     }
                     else
                     {
                         go.GetComponent<UIToggle>().value = false;
                         UIManager.instance.OpenPromptWindow(string.Format("{0}关开放装备精炼", 82), PromptWindow.PromptType.Hint, null, null);
                     }
                 }
                 else
                 {
                     SetTab(1);
                     go.GetComponent<UIToggle>().value = true;
                 }*/
                if (CharacterRecorder.instance.lastGateID > 10052)
                {
                    SetTab(1);
                    go.GetComponent<UIToggle>().value = true;
                }
                else
                {
                    go.GetComponent<UIToggle>().value = false;
                    UIManager.instance.OpenPromptWindow(string.Format("{0}关开放装备饰品精炼", 82), PromptWindow.PromptType.Hint, null, null);
                }
                if (CharacterRecorder.instance.GuideID[34] == 3)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
                break;
            case "SpriteTab3":
                // if (CharacterRecorder.instance.level >= 16)
                if (CharacterRecorder.instance.lastGateID > 10022)
                {
                    go.GetComponent<UIToggle>().value = true;
                    OnClickListTabAwakeButton();
                }
                else
                {
                    UIManager.instance.OpenPromptWindow(string.Format("{0}关开放饰品晋级", 22), PromptWindow.PromptType.Hint, null, null);
                }
                return;
            case "SpriteTab4":
                if (CharacterRecorder.instance.lastGateID > 10048)  //&& CharacterRecorder.instance.level >= 24
                {
                    go.GetComponent<UIToggle>().value = true;
                    OnClickListTabSecretStone();
                }
                else
                {
                    go.GetComponent<UIToggle>().value = false;
                    UIManager.instance.OpenPromptWindow(string.Format("通关{0}开放秘宝", 48), PromptWindow.PromptType.Hint, null, null);
                }
                return;
            case "SpriteTab5":
                if (CharacterRecorder.instance.lastGateID > 10066)  //&& CharacterRecorder.instance.level >= 24
                {
                    UIManager.instance.CountSystem(UIManager.Systems.座驾);
                    UIManager.instance.UpdateSystems(UIManager.Systems.座驾);
                    gameObject.transform.Find("All/RoleCamera").gameObject.SetActive(false);
                    OnClickListTabWeaponWindow(1);
                    go.GetComponent<UIToggle>().value = true;
                }
                else
                {
                    go.GetComponent<UIToggle>().value = false;
                    UIManager.instance.OpenPromptWindow(string.Format("通关{0}开放座驾", 66), PromptWindow.PromptType.Hint, null, null);
                }
                return;
            case "SpriteTab6":
                if (GetHeWuQiCount() > 0 || CharacterRecorder.instance.lastGateID > TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.hewuqi).Level)  //&& CharacterRecorder.instance.level >= 24
                {
                    UIManager.instance.CountSystem(UIManager.Systems.神器);
                    UIManager.instance.UpdateSystems(UIManager.Systems.神器);
                    gameObject.transform.Find("All/RoleCamera").gameObject.SetActive(false);
                    OnClickListTabWeaponWindow(2);
                    go.GetComponent<UIToggle>().value = true;
                }
                else
                {
                    go.GetComponent<UIToggle>().value = false;
                    UIManager.instance.OpenPromptWindow(string.Format("通关{0}开放核武器", TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.hewuqi).Level - 10000), PromptWindow.PromptType.Hint, null, null);
                }
                return;
        }
        InitEquipItemOpenState();
    }
    IEnumerator DelaySetUIToggleFalse(UIToggle _UIToggle)
    {
        yield return new WaitForSeconds(0.5f);
        _UIToggle.enabled = false;
    }
    void InitEquipItemOpenState()
    {
        for (int i = 0; i < ListEquip.Count; i++)
        {
            UIEventListener.Get(ListEquip[i]).onClick = ClickListEquip;
            if (ListEquip[i].transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName != "add")
            {
                switch (ListEquip[i].name)
                {
                    case "Equip1":
                    case "Equip2":
                    case "Equip3":
                    case "Equip4":
                        GameObject _Lock = ListEquip[i].transform.FindChild("Lock").gameObject;
                        GameObject _UpJianTou = ListEquip[i].transform.FindChild("UpJianTou").gameObject;
                        //Debug.LogError("clickTab hou  strengType....." + strengType);
                        if (strengType == 1)
                        {
                            //if (CharacterRecorder.instance.level < 31)
                            if (CharacterRecorder.instance.lastGateID <= 10082)
                            {
                                ListEquip[i].GetComponent<UIToggle>().enabled = false;
                                _Lock.SetActive(true);
                                _UpJianTou.SetActive(false);
                            }
                            else
                            {
                                ListEquip[i].GetComponent<UIToggle>().enabled = true;
                                _Lock.SetActive(false);
                            }
                        }
                        else
                        {
                            ListEquip[i].GetComponent<UIToggle>().enabled = false;
                            _Lock.SetActive(false);
                            //Debug.LogError(ListEquip[i].GetComponent<EquipItemInStreng>().IsUpJianTouActive + ".._UpJianTouSpriteState.." + _UpJianTou.activeSelf);
                            if (ListEquip[i].GetComponent<EquipItemInStreng>().IsUpJianTouActive)
                            {
                                _UpJianTou.SetActive(true);
                            }
                        }
                        break;
                    case "Equip5":
                    case "Equip6":
                        GameObject _Lock2 = ListEquip[i].transform.FindChild("Lock").gameObject;
                        if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.duobao).Level)
                        // if (CharacterRecorder.instance.lastGateID <= 10022)
                        {
                            ListEquip[i].GetComponent<UIToggle>().enabled = false;
                            _Lock2.SetActive(true);
                        }
                        else
                        {
                            _Lock2.SetActive(false);
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
    public void ClickListEquip(GameObject go)
    {
        if (go != null)
        {
            if (go.transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName == "add")
            {
                return;
            }
            //Debug.LogError("strengType.." + strengType);
            CharacterRecorder.instance.isOneKeyState = false;
            switch (go.name)
            {
                case "Equip1":
                    if (strengType == 1 && CharacterRecorder.instance.lastGateID <= 10082)// && CharacterRecorder.instance.level < 31)
                    {
                        UIManager.instance.OpenPromptWindow(string.Format("{0}关开放装备精炼", 82), PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        ClickIndex = 1; equipType = 0;
                    }
                    /* if (strengType == 0)
                     {
                         ClickIndex = 1; equipType = 0;
                     }
                     else if (strengType == 1)
                     {
                         if (CharacterRecorder.instance.level < 31)
                         {
                             UIManager.instance.OpenPromptWindow(string.Format("{0}级开放装备精炼", 31), PromptWindow.PromptType.Hint, null, null);
                         }
                         else
                         {
                             ClickIndex = 1; equipType = 0;
                         }
                     }*/
                    break;
                case "Equip2":
                    if (strengType == 1 && CharacterRecorder.instance.lastGateID <= 10082)//&& CharacterRecorder.instance.level < 31)
                    {
                        UIManager.instance.OpenPromptWindow(string.Format("{0}关开放装备精炼", 82), PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        ClickIndex = 2; equipType = 0;
                    }
                    /* if(strengType == 0)
                     {
                         ClickIndex = 2; equipType = 0; 
                     }
                     else if (strengType == 1)
                     {
                         if (CharacterRecorder.instance.level < 31)
                         {
                             UIManager.instance.OpenPromptWindow(string.Format("{0}级开放装备精炼", 31), PromptWindow.PromptType.Hint, null, null);
                         }
                     }*/
                    break;
                case "Equip3":
                    if (strengType == 1 && CharacterRecorder.instance.lastGateID <= 10082)// && CharacterRecorder.instance.level < 31)
                    {
                        UIManager.instance.OpenPromptWindow(string.Format("{0}关开放装备精炼", 82), PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        ClickIndex = 3; equipType = 0;
                    }
                    /*  if (strengType == 0)
                      {
                          ClickIndex = 3; equipType = 0; 
                      }
                      else if (strengType == 1)
                      {
                          if (CharacterRecorder.instance.level < 31)
                          {
                              UIManager.instance.OpenPromptWindow(string.Format("{0}级开放装备精炼", 31), PromptWindow.PromptType.Hint, null, null);
                          }
                      }*/
                    break;
                case "Equip4":
                    if (strengType == 1 && CharacterRecorder.instance.lastGateID <= 10082)// && CharacterRecorder.instance.level < 31)
                    {
                        UIManager.instance.OpenPromptWindow(string.Format("{0}关开放装备精炼", 82), PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        ClickIndex = 4; equipType = 0;
                    }
                    /*  if (strengType == 0)
                      {
                          ClickIndex = 4; equipType = 0; 
                      }
                      else if (strengType == 1)
                      {
                          if (CharacterRecorder.instance.level < 31)
                          {
                              UIManager.instance.OpenPromptWindow(string.Format("{0}级开放装备精炼", 31), PromptWindow.PromptType.Hint, null, null);
                          }
                      }*/
                    break;
                case "Equip5":
                    if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.duobao).Level)
                    {
                        UIManager.instance.OpenPromptWindow(string.Format("{0}关开放饰品强化精炼晋级", 22), PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        ClickIndex = 5; equipType = 1;
                    }
                    if (CharacterRecorder.instance.GuideID[34] == 4)
                    {
                        SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                    }
                    break;
                case "Equip6":
                    if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.duobao).Level)
                    {
                        UIManager.instance.OpenPromptWindow(string.Format("{0}关开放饰品强化精炼晋级", 22), PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        ClickIndex = 6; equipType = 1;
                    }
                    break;
            }
            SetEquipInfo();
        }
    }
    void AddDaoJuEffect(UISprite icon1Obj)
    {
        //Debug.LogError("加道具特效");
        GameObject _ToolEquipEffect = GameObject.Instantiate(Resources.Load("Prefab/Effect/WF_ui_DaoJu", typeof(GameObject)), icon1Obj.transform.position, Quaternion.identity) as GameObject;
        _ToolEquipEffect.transform.parent = icon1Obj.transform;
        _ToolEquipEffect.transform.localScale = Vector3.one;
        TweenPosition _tp1 = icon1Obj.GetComponent<TweenPosition>();
        if (_tp1 == null)
        {
            _tp1 = icon1Obj.gameObject.AddComponent<TweenPosition>();

        }
        else { }
        _tp1.from = new Vector3(0, 40f, 0);
        _tp1.to = new Vector3(0, 30f, 0);
        _tp1.duration = 1.0f;
        _tp1.style = UITweener.Style.PingPong;
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
    public void InitHeroList()
    {
        for (int j = ListHero.Count - 1; j >= 0; j--)
        {
            DestroyImmediate(ListHero[j]);
        }
        ListHero.Clear();
        int i = 0;
        foreach (var h in CharacterRecorder.instance.ownedHeroList)
        {
            if (CharacterRoleID == 0)
            {
                CharacterRoleID = h.characterRoleID;
                RoleID = h.cardID;
            }
            GameObject go = NGUITools.AddChild(MyGrid, MyHeroItem);
            go.name = "HeroItem" + h.cardID;
            go.AddComponent<RoleHeroItem>();
            go.GetComponent<RoleHeroItem>().Init(true, RoleRedPointType.EquipAdvanceRedPint, h.cardID, h.characterRoleID, i);
            go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("ScrollView").GetComponent<UIScrollView>();

            ListHero.Add(go);
            i++;
        }
        MyGrid.GetComponent<UIGrid>().Reposition();
        if (CharacterRecorder.instance.IsNeedResetHedIndexAfterSortHero)
        {
            for (int j = 0; j < CharacterRecorder.instance.ownedHeroList.size; j++)
            {
                if (RoleWindow.targetRoleCardId == CharacterRecorder.instance.ownedHeroList[j].cardID)
                {
                    TextTranslator.instance.HeadIndex = j;
                    break;
                }
            }
            CharacterRecorder.instance.IsNeedResetHedIndexAfterSortHero = false;
        }
        SetHeroClick(TextTranslator.instance.HeadIndex);
    }
    public void SetEuipList(BetterList<Hero.EquipInfo> _EquipList)
    {
        myHeroEquipList = _EquipList;
        foreach (var _OneEquipInfo in _EquipList)
        {
            //TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(_OneEquipInfo.equipCode);
            //Debug.LogError(mitemInfo.itemGrade);
            SetEquip(_OneEquipInfo);
        }
        //        strengType = 0;
        SetEquipInfo();

        if (IsOnlyAfterBaoWuUp)
        {
            OpenAdvanceWindow(AdvanceWindowType.BaoWuUp);
            IsOnlyAfterBaoWuUp = false;
        }
    }
    //设置装备
    private void SetEquip(Hero.EquipInfo _OneEquipInfo)
    {
        GameObject go = ListEquip[_OneEquipInfo.equipPosition - 1];
        //go.GetComponent<EquipItemInStreng>().SetEquipItem(mHero, _OneEquipInfo);
        if (_OneEquipInfo.equipCode != 0)
        {
            string frameName = "";
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_OneEquipInfo.equipCode);
            frameName = string.Format("Grade{0}", mItemInfo.itemGrade);
            go.GetComponent<UISprite>().spriteName = frameName;
            go.transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName = _OneEquipInfo.equipCode.ToString();
            go.transform.Find("Label").gameObject.GetComponent<UILabel>().text = _OneEquipInfo.equipLevel.ToString();
            if (_OneEquipInfo.equipPosition < 5)
            {
                go.GetComponent<EquipItemInStreng>().SetEquipItemInStrengh(mHero, _OneEquipInfo.equipPosition);
                SetEquipStrenghRedPoint(_OneEquipInfo, go);
            }
            else
            {
                GameObject _RedPoint = go.transform.FindChild("RedPoint").gameObject;
                _RedPoint.SetActive(IsCanAwake(_OneEquipInfo.equipPosition));
            }
        }
        else
        {
            go.GetComponent<UISprite>().spriteName = "Grade1";
            go.transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName = "";
            go.transform.Find("Label").gameObject.GetComponent<UILabel>().text = "";
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
            if (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zhuangbeijinglian).Level < CharacterRecorder.instance.lastGateID)
            {
                _RedPoint.SetActive(true);
                _UpJianTou.SetActive(false);
                go.GetComponent<EquipItemInStreng>().IsUpJianTouActive = false;
            }
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
            go.GetComponent<EquipItemInStreng>().IsUpJianTouActive = false;
        }
    }
    bool IsAdvanceMaterailEnough(Hero.EquipInfo _OneEquipInfo)
    {
        EquipStrongQuality esq = TextTranslator.instance.GetEquipStrongQualityByIDAndColor(_OneEquipInfo.equipCode, _OneEquipInfo.equipColorNum, mHeroInfo.heroRace);
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
    bool IsAdvanceStateAndCanAdvance(Hero.EquipInfo _OneEquipInfo)
    {
        int _EquipPosition = _OneEquipInfo.equipPosition;
        int _EquipLevel = _OneEquipInfo.equipLevel;
        int _EquipColorNum = _OneEquipInfo.equipColorNum;
        EquipStrongQuality esq = TextTranslator.instance.GetEquipStrongQualityByIDAndColor(mHero.equipList[_EquipPosition - 1].equipCode, mHero.equipList[_EquipPosition - 1].equipColorNum, mHeroInfo.heroRace);
        if (IsAdvanceState(_EquipLevel, _EquipColorNum) && CharacterRecorder.instance.gold >= esq.Money)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool IsEnoughToUpGrade(Hero.EquipInfo _OneEquipInfo)
    {
        //HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(h.cardID)
        int needMoney = TextTranslator.instance.GetEquipStrongCostByID(_OneEquipInfo.equipLevel, mHeroInfo.heroRarity, _OneEquipInfo.equipPosition);
        if (CharacterRecorder.instance.gold >= needMoney)
        {
            return true;
        }
        return false;
    }
    bool IsEnoughToAdvance(Hero.EquipInfo _OneEquipInfo)
    {
        int _EquipPosition = _OneEquipInfo.equipPosition;
        EquipStrongQuality esq = TextTranslator.instance.GetEquipStrongQualityByIDAndColor(mHero.equipList[_EquipPosition - 1].equipCode, mHero.equipList[_EquipPosition - 1].equipColorNum, mHeroInfo.heroRace);
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
    int ExchangeStateUpOrAdvance(Hero.EquipInfo mEquipInfo)
    {
        if (IsAdvanceState(mEquipInfo.equipLevel, mEquipInfo.equipColorNum))
        {
            return 0;//升品 ——> 升级 （右边 等级不变，加颜色）
        }
        else if (mEquipInfo.equipLevel == (mEquipInfo.equipColorNum - 1) * 5 + 4)
        {
            return 2;//升级——> 升品 （右边 加等级，颜色不变）
        }
        else return 1;//升级——> 升级  (右边 加等级，颜色不变)

    }
    #region//强化、精炼属性描述（返回值是 描述、数据不分开的）
    string GetStrenghUpOrAdvanceStr(Hero.EquipInfo mEquipInfo, bool useNext)
    {
        string desStr = "";
        int usefulClass = 0;
        int usefulColorNum = 0;
        int changeState = ExchangeStateUpOrAdvance(mEquipInfo);
        EquipStrong es;
        if (changeState == 0)
        {
            usefulClass = mEquipInfo.equipLevel;
            if (useNext)
            {
                usefulColorNum = mEquipInfo.equipColorNum + 1;
            }
            else
            {
                usefulColorNum = mEquipInfo.equipColorNum;
            }
            es = TextTranslator.instance.GetEquipStrongByPositionRaceColor(mEquipInfo.equipPosition, mHeroInfo.heroRace, usefulColorNum);//mEquipInfo.equipColorNum
        }
        else
        {
            usefulColorNum = mEquipInfo.equipColorNum;
            if (useNext)
            {
                usefulClass = mEquipInfo.equipLevel + 1;
            }
            else
            {
                usefulClass = mEquipInfo.equipLevel;
            }
            es = TextTranslator.instance.GetEquipStrongByID(mEquipInfo.equipCode, mEquipInfo.equipColorNum);
        }
        desStr = "等级:" + usefulClass.ToString() + "\n";
        InfoDesStr = "等级:" + "\n";
        DataStr = usefulClass.ToString() + "\n";
        if (es != null)
        {
            if (es.Hp > 0)
            {
                string usefulNumStr = "";
                if (es.Hp * usefulClass < 1 && es.Hp * usefulClass > 0)
                {
                    usefulNumStr = ChangeDecimalToPercent(es.Hp * usefulClass);
                }
                else
                {
                    usefulNumStr = (es.Hp * usefulClass).ToString();
                }
                desStr += "生命:" + usefulNumStr + "\n";
                InfoDesStr += "生命:" + "\n";
                DataStr += usefulNumStr + "\n";
            }
            if (es.Atk > 0)
            {
                string usefulNumStr = "";
                if (es.Atk * usefulClass < 1 && es.Atk * usefulClass > 0)
                {
                    usefulNumStr = ChangeDecimalToPercent(es.Atk * usefulClass);
                }
                else
                {
                    usefulNumStr = (es.Atk * usefulClass).ToString();
                }
                desStr += "攻击:" + usefulNumStr + "\n";
                InfoDesStr += "攻击:" + "\n";
                DataStr += usefulNumStr + "\n";
            }
            if (es.Def > 0)
            {
                string usefulNumStr = "";
                if (es.Def * usefulClass < 1 && es.Def * usefulClass > 0)
                {
                    usefulNumStr = ChangeDecimalToPercent(es.Def * usefulClass);
                }
                else
                {
                    usefulNumStr = (es.Def * usefulClass).ToString();
                }
                desStr += "防御:" + usefulNumStr + "\n";
                InfoDesStr += "防御:" + "\n";
                DataStr += usefulNumStr + "\n";
            }
            if (es.Hit > 0)
            {
                string usefulNumStr = "";
                if (es.Hit * usefulClass < 1 && es.Hit * usefulClass > 0)
                {
                    usefulNumStr = ChangeDecimalToPercent(es.Hit * usefulClass);
                }
                else
                {
                    usefulNumStr = (es.Hit * usefulClass).ToString();
                }
                desStr += "命中:" + usefulNumStr + "\n";
                InfoDesStr += "命中:" + "\n";
                DataStr += usefulNumStr + "\n";
            }
            if (es.NoHit > 0)
            {
                string usefulNumStr = "";
                if (es.NoHit * usefulClass < 1 && es.NoHit * usefulClass > 0)
                {
                    usefulNumStr = ChangeDecimalToPercent(es.NoHit * usefulClass);
                }
                else
                {
                    usefulNumStr = (es.NoHit * usefulClass).ToString();
                }
                desStr += "闪避:" + usefulNumStr + "\n";
                InfoDesStr += "闪避:" + "\n";
                DataStr += usefulNumStr + "\n";
            }
            if (es.Crit > 0)
            {
                string usefulNumStr = "";
                if (es.Crit * usefulClass < 1 && es.Crit * usefulClass > 0)
                {
                    usefulNumStr = ChangeDecimalToPercent(es.Crit * usefulClass);
                }
                else
                {
                    usefulNumStr = (es.Crit * usefulClass).ToString();
                }
                desStr += "暴击:" + usefulNumStr + "\n";
                InfoDesStr += "暴击:" + "\n";
                DataStr += usefulNumStr + "\n";
            }
            if (es.NoCrit > 0)
            {
                string usefulNumStr = "";
                if (es.NoCrit * usefulClass < 1 && es.NoCrit * usefulClass > 0)
                {
                    usefulNumStr = ChangeDecimalToPercent(es.NoCrit * usefulClass);
                }
                else
                {
                    usefulNumStr = (es.NoCrit * usefulClass).ToString();
                }
                desStr += "抗暴:" + usefulNumStr + "\n";
                InfoDesStr += "抗暴:" + "\n";
                DataStr += usefulNumStr + "\n";
            }
            if (es.DmgBonus > 0)
            {
                string usefulNumStr = "";
                if (es.DmgBonus * usefulClass < 1 && es.DmgBonus * usefulClass > 0)
                {
                    usefulNumStr = ChangeDecimalToPercent(es.DmgBonus * usefulClass);
                }
                else
                {
                    usefulNumStr = (es.DmgBonus * usefulClass).ToString();
                }
                desStr += "伤害加成:" + usefulNumStr + "\n";
                InfoDesStr += "加成:" + "\n";
                DataStr += usefulNumStr + "\n";
            }
            if (es.DmgReduction > 0)
            {
                string usefulNumStr = "";
                if (es.DmgReduction * usefulClass < 1 && es.DmgReduction * usefulClass > 0)
                {
                    usefulNumStr = ChangeDecimalToPercent(es.DmgReduction * usefulClass);
                }
                else
                {
                    usefulNumStr = (es.DmgReduction * usefulClass).ToString();
                }
                desStr += "伤害减免:" + usefulNumStr + "\n";
                InfoDesStr += "减免:" + "\n";
                DataStr += usefulNumStr + "\n";
            }
        }
        return desStr;
    }
    string GetStrenghStr(Hero.EquipInfo mEquipInfo, bool useNext)
    {
        string desStr = "";
        //mEquipInfo = mHero.equipList[ClickIndex - 1];
        Debug.Log("equipCode....." + mEquipInfo.equipCode + ".....equipColorNum..." + mEquipInfo.equipColorNum);
        //EquipStrong es = TextTranslator.instance.GetEquipStrongByID(mEquipInfo.equipCode, mEquipInfo.equipColorNum);
        EquipStrong es;
        if (mEquipInfo.equipPosition < 5)
        {
            es = TextTranslator.instance.GetEquipStrongByID(mEquipInfo.equipCode, mEquipInfo.equipColorNum);
        }
        else
        {
            es = TextTranslator.instance.GetEquipStrongByID(mEquipInfo.equipCode);
        }

        int usefulClass = 0;
        if (!useNext)
        {
            usefulClass = mEquipInfo.equipLevel;
        }
        else
        {
            usefulClass = mEquipInfo.equipLevel + 1;
        }
        desStr = "等级:" + usefulClass.ToString() + "\n";
        InfoDesStr = "等级:" + "\n";
        DataStr = usefulClass.ToString() + "\n";
        if (es.Hp > 0)
        {
            string usefulNumStr = "";
            if (es.Hp * usefulClass < 1 && es.Hp * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Hp * usefulClass);
            }
            else
            {
                usefulNumStr = (es.Hp * usefulClass).ToString();
            }
            desStr += "生命:" + usefulNumStr + "\n";
            InfoDesStr += "生命:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.Atk > 0)
        {
            string usefulNumStr = "";
            if (es.Atk * usefulClass < 1 && es.Atk * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Atk * usefulClass);
            }
            else
            {
                usefulNumStr = (es.Atk * usefulClass).ToString();
            }
            desStr += "攻击:" + usefulNumStr + "\n";
            InfoDesStr += "攻击:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.Def > 0)
        {
            string usefulNumStr = "";
            if (es.Def * usefulClass < 1 && es.Def * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Def * usefulClass);
            }
            else
            {
                usefulNumStr = (es.Def * usefulClass).ToString();
            }
            desStr += "防御:" + usefulNumStr + "\n";
            InfoDesStr += "防御:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.Hit > 0)
        {
            string usefulNumStr = "";
            if (es.Hit * usefulClass < 1 && es.Hit * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Hit * usefulClass);
            }
            else
            {
                usefulNumStr = (es.Hit * usefulClass).ToString();
            }
            desStr += "命中:" + usefulNumStr + "\n";
            InfoDesStr += "命中:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.NoHit > 0)
        {
            string usefulNumStr = "";
            if (es.NoHit * usefulClass < 1 && es.NoHit * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.NoHit * usefulClass);
            }
            else
            {
                usefulNumStr = (es.NoHit * usefulClass).ToString();
            }
            desStr += "闪避:" + usefulNumStr + "\n";
            InfoDesStr += "闪避:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.Crit > 0)
        {
            string usefulNumStr = "";
            if (es.Crit * usefulClass < 1 && es.Crit * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Crit * usefulClass);
            }
            else
            {
                usefulNumStr = (es.Crit * usefulClass).ToString();
            }
            desStr += "暴击:" + usefulNumStr + "\n";
            InfoDesStr += "暴击:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.NoCrit > 0)
        {
            string usefulNumStr = "";
            if (es.NoCrit * usefulClass < 1 && es.NoCrit * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.NoCrit * usefulClass);
            }
            else
            {
                usefulNumStr = (es.NoCrit * usefulClass).ToString();
            }
            desStr += "抗暴:" + usefulNumStr + "\n";
            InfoDesStr += "抗暴:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.DmgBonus > 0)
        {
            string usefulNumStr = "";
            if (es.DmgBonus * usefulClass < 1 && es.DmgBonus * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.DmgBonus * usefulClass);
            }
            else
            {
                usefulNumStr = (es.DmgBonus * usefulClass).ToString();
            }
            desStr += "伤害加成:" + usefulNumStr + "\n";
            InfoDesStr += "加成:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.DmgReduction > 0)
        {
            string usefulNumStr = "";
            if (es.DmgReduction * usefulClass < 1 && es.DmgReduction * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.DmgReduction * usefulClass);
            }
            else
            {
                usefulNumStr = (es.DmgReduction * usefulClass).ToString();
            }
            desStr += "伤害减免:" + usefulNumStr + "\n";
            InfoDesStr += "减免:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        return desStr;
    }
    string GetJingLianStr(Hero.EquipInfo mEquipInfo, bool useNext)
    {
        string desStr = "";
        Debug.Log(mEquipInfo.equipCode);
        EquipDetailInfo es = TextTranslator.instance.GetEquipInfoByID(mEquipInfo.equipCode);
        int usefulClass = 0;
        if (!useNext)
        {
            usefulClass = mEquipInfo.equipClass;
        }
        else
        {
            usefulClass = mEquipInfo.equipClass + 1;
        }
        desStr = "精炼:" + usefulClass.ToString() + "\n";
        InfoDesStr = "精炼:" + "\n";
        DataStr = usefulClass.ToString() + "\n";
        if (es.HP > 0)
        {
            string usefulNumStr = "";
            if (es.HP * usefulClass < 1 && es.HP * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.HP * usefulClass);
            }
            else
            {
                usefulNumStr = (es.HP * usefulClass).ToString();
            }
            desStr += "生命:" + usefulNumStr + "\n";
            InfoDesStr += "生命:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.ADAttack > 0)
        {
            string usefulNumStr = "";
            if (es.ADAttack * usefulClass < 1 && es.ADAttack * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.ADAttack * usefulClass);
            }
            else
            {
                usefulNumStr = (es.ADAttack * usefulClass).ToString();
            }
            desStr += "攻击:" + usefulNumStr + "\n";//物攻
            InfoDesStr += "攻击:" + "\n";           //物攻
            DataStr += usefulNumStr + "\n";
            //desStr += "物攻:" + (es.ADAttack * usefulClass).ToString() + "\n";
        }
        if (es.APAttack > 0)
        {
            string usefulNumStr = "";
            if (es.APAttack * usefulClass < 1 && es.APAttack * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.APAttack * usefulClass);
            }
            else
            {
                usefulNumStr = (es.APAttack * usefulClass).ToString();
            }
            desStr += "法攻:" + usefulNumStr + "\n";
            InfoDesStr += "法攻:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.ADDenfance > 0)
        {
            string usefulNumStr = "";
            if (es.ADDenfance * usefulClass < 1 && es.ADDenfance * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.ADDenfance * usefulClass);
            }
            else
            {
                usefulNumStr = (es.ADDenfance * usefulClass).ToString();
            }
            desStr += "防御:" + usefulNumStr + "\n";//物防
            InfoDesStr += "防御:" + "\n";           //物防
            DataStr += usefulNumStr + "\n";
        }
        if (es.APDenfance > 0)
        {
            string usefulNumStr = "";
            if (es.APDenfance * usefulClass < 1 && es.APDenfance * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.APDenfance * usefulClass);
            }
            else
            {
                usefulNumStr = (es.APDenfance * usefulClass).ToString();
            }
            desStr += "法防:" + usefulNumStr + "\n";
            InfoDesStr += "法防:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.Hit > 0)
        {
            string usefulNumStr = "";
            if (es.Hit * usefulClass < 1 && es.Hit * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Hit * usefulClass);
            }
            else
            {
                usefulNumStr = (es.Hit * usefulClass).ToString();
            }
            desStr += "命中:" + usefulNumStr + "\n";
            InfoDesStr += "命中:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.NoHit > 0)
        {
            string usefulNumStr = "";
            if (es.NoHit * usefulClass < 1 && es.NoHit * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.NoHit * usefulClass);
            }
            else
            {
                usefulNumStr = (es.NoHit * usefulClass).ToString();
            }
            desStr += "闪避:" + usefulNumStr + "\n";
            InfoDesStr += "闪避:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.Crit > 0)
        {
            string usefulNumStr = "";
            if (es.Crit * usefulClass < 1 && es.Crit * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Crit * usefulClass);
            }
            else
            {
                usefulNumStr = (es.Crit * usefulClass).ToString();
            }
            desStr += "暴击:" + usefulNumStr + "\n";
            InfoDesStr += "暴击:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.NoCrit > 0)
        {
            string usefulNumStr = "";
            if (es.NoCrit * usefulClass < 1 && es.NoCrit * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.NoCrit * usefulClass);
            }
            else
            {
                usefulNumStr = (es.NoCrit * usefulClass).ToString();
            }
            desStr += "抗暴:" + usefulNumStr + "\n";
            InfoDesStr += "抗暴:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.DmgBonus > 0)
        {
            string usefulNumStr = "";
            if (es.DmgBonus * usefulClass < 1 && es.DmgBonus * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.DmgBonus * usefulClass);
            }
            else
            {
                usefulNumStr = (es.DmgBonus * usefulClass).ToString();
            }
            desStr += "伤害加成:" + usefulNumStr + "\n";
            InfoDesStr += "加成:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.DmgReduction > 0)
        {
            string usefulNumStr = "";
            if (es.DmgReduction * usefulClass < 1 && es.DmgReduction * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.DmgReduction * usefulClass);
            }
            else
            {
                usefulNumStr = (es.DmgReduction * usefulClass).ToString();
            }
            desStr += "伤害减免:" + usefulNumStr + "\n";
            InfoDesStr += "减免:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        return desStr;
    }
    #endregion

    string ChangeDecimalToPercent(float _Decimal)
    {
        return string.Format("{0}%", Math.Round(_Decimal * 100, 1));//_Decimal * 100
    }
    private void SetCurSelectEquipItem(int _pos)
    {
        for (int i = 0; i < ListEquip.Count; i++)
        {
            UIToggle _myUIToggle = ListEquip[i].GetComponent<UIToggle>();
            if (i + 1 != _pos)
            {
                _myUIToggle.startsActive = false;
                _myUIToggle.activeSprite.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0);
            }
            else
            {
                _myUIToggle.startsActive = true;
                _myUIToggle.activeSprite.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
            }
        }
    }

    void RecordEquipInfoBefore(Hero.EquipInfo mEquipInfo)
    {
        mEquipInfoBefore.equipCode = mEquipInfo.equipCode;
        mEquipInfoBefore.equipClass = mEquipInfo.equipClass;
        mEquipInfoBefore.equipLevel = mEquipInfo.equipLevel;
        mEquipInfoBefore.equipExp = mEquipInfo.equipExp;
        mEquipInfoBefore.equipPosition = mEquipInfo.equipPosition;
        mEquipInfoBefore.equipID = mEquipInfo.equipID;
        mEquipInfoBefore.equipColorNum = mEquipInfo.equipColorNum;
    }
    public void RefineBaoWuResult()
    {
        StartCoroutine(OnEquipDataUpEffect(LeftInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>()));
        StartCoroutine(OnEquipDataUpEffect(RightInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>()));

        OpenAdvanceWindow(AdvanceWindowType.EquipRefine);
        SetEquipInfo();
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
        _myLabel.color = new Color(36 / 255f, 155 / 255f, 210 / 255f, 255 / 255f);
    }
    public void SetEquipInfo()
    {
        if (ClickIndex > 4)
        {
            equipType = 1;
        }
        else
        {
            equipType = 0;
        }
        SetCurSelectEquipItem(ClickIndex);
        InitOpenState(ClickIndex);
        //如果当前是晋级（觉醒）Tab，强制跳转强化Tab
        //Debug.LogError("strengType..........." + strengType);        
        if (strengType == 0)
        {
            //旧的不区分升级、升品
            // LeftInfoLabel.text = GetStrenghStr(mHero.equipList[ClickIndex - 1], false);
            // RightInfoLabel.text = GetStrenghStr(mHero.equipList[ClickIndex - 1], true);
            //区分升级、升品
            if (equipType == 0)
            {
                //文字、数字 不分开
                //   LeftInfoLabel.text = GetStrenghUpOrAdvanceStr(mHero.equipList[ClickIndex - 1], false);
                //   RightInfoLabel.text = GetStrenghUpOrAdvanceStr(mHero.equipList[ClickIndex - 1], true);

                GetStrenghUpOrAdvanceStr(mHero.equipList[ClickIndex - 1], false);
                LeftInfoLabelNew.text = InfoDesStr;
                LeftInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>().text = DataStr;

                GetStrenghUpOrAdvanceStr(mHero.equipList[ClickIndex - 1], true);
                RightInfoLabelNew.text = InfoDesStr;
                RightInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>().text = DataStr;
            }
            else
            {
                //文字、数字 不分开
                //   LeftInfoLabel.text = GetStrenghStr(mHero.equipList[ClickIndex - 1], false);
                //   RightInfoLabel.text = GetStrenghStr(mHero.equipList[ClickIndex - 1], true);

                GetStrenghStr(mHero.equipList[ClickIndex - 1], false);
                LeftInfoLabelNew.text = InfoDesStr;
                LeftInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>().text = DataStr;

                GetStrenghStr(mHero.equipList[ClickIndex - 1], true);
                RightInfoLabelNew.text = InfoDesStr;
                RightInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>().text = DataStr;
            }
        }
        else
        {
            //文字、数字 不分开
            //  LeftInfoLabel.text = GetJingLianStr(mHero.equipList[ClickIndex - 1], false);
            //  RightInfoLabel.text = GetJingLianStr(mHero.equipList[ClickIndex - 1], true);

            GetJingLianStr(mHero.equipList[ClickIndex - 1], false);
            LeftInfoLabelNew.text = InfoDesStr;
            LeftInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>().text = DataStr;

            GetJingLianStr(mHero.equipList[ClickIndex - 1], true);
            RightInfoLabelNew.text = InfoDesStr;
            RightInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>().text = DataStr;
        }

        mEquipInfo = mHero.equipList[ClickIndex - 1];
        Debug.Log("mEquipInfo.level..." + mEquipInfo.equipLevel);
        //饰品晋级（觉醒）红点(旧的，对单个饰品)
        /* if (ClickIndex == 5 || ClickIndex == 6)
         {
             SetRedPointStateOfBaoWuAwake();
         }*/

        EquipIcon.spriteName = mHero.equipList[ClickIndex - 1].equipCode.ToString();

        EquipNameLabel.text = GetItemName(mHero.equipList[ClickIndex - 1].equipCode, mHero.equipList[ClickIndex - 1].equipClass, mHero.equipList[ClickIndex - 1].equipColorNum);
        //Debug.LogError("strengType。。。" + strengType);
        SetCurrentPart(strengType, equipType, TextTranslator.instance.HeadIndex, ClickIndex);
    }
    #region 以下只为宝物选择装备后预览
    public void SetEquipInfoOnlyForBaoWuStreng(int curEquipLv)
    {
        /*  int curEquipLv = mEquipInfo.equipLevel;
          //Debug.Log("curEquipLv.." + curEquipLv + "...heroRarity..." + mHeroInfo.heroRarity);
          int curNeedEXp = TextTranslator.instance.GetEquipStrongCostDataByID(curEquipLv, mHeroInfo.heroRarity).NeedExp;
          while (curSumAddEXP - curNeedEXp >= 0)
          {
              curSumAddEXP -= curNeedEXp;
              curEquipLv += 1;
              curNeedEXp = TextTranslator.instance.GetEquipStrongCostDataByID(curEquipLv, mHeroInfo.heroRarity).NeedExp;
          }
          //Debug.LogError("预览等级.." + curEquipLv);*/
        GetStrenghStr(mHero.equipList[ClickIndex - 1], curEquipLv);
        RightInfoLabelNew.text = InfoDesStr;
        RightInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>().text = DataStr;
    }
    string GetStrenghStr(Hero.EquipInfo mEquipInfo, int useNextLevel)
    {
        string desStr = "";
        EquipStrong es = TextTranslator.instance.GetEquipStrongByID(mEquipInfo.equipCode, mEquipInfo.equipColorNum);
        int usefulClass = 0;

        usefulClass = useNextLevel;

        desStr = "等级:" + usefulClass.ToString() + "\n";
        InfoDesStr = "等级:" + "\n";
        DataStr = usefulClass.ToString() + "\n";
        if (es.Hp > 0)
        {
            string usefulNumStr = "";
            if (es.Hp * usefulClass < 1 && es.Hp * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Hp * usefulClass);
            }
            else
            {
                usefulNumStr = (es.Hp * usefulClass).ToString();
            }
            desStr += "生命:" + usefulNumStr + "\n";
            InfoDesStr += "生命:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.Atk > 0)
        {
            string usefulNumStr = "";
            if (es.Atk * usefulClass < 1 && es.Atk * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Atk * usefulClass);
            }
            else
            {
                usefulNumStr = (es.Atk * usefulClass).ToString();
            }
            desStr += "攻击:" + usefulNumStr + "\n";
            InfoDesStr += "攻击:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.Def > 0)
        {
            string usefulNumStr = "";
            if (es.Def * usefulClass < 1 && es.Def * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Def * usefulClass);
            }
            else
            {
                usefulNumStr = (es.Def * usefulClass).ToString();
            }
            desStr += "防御:" + usefulNumStr + "\n";
            InfoDesStr += "防御:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.Hit > 0)
        {
            string usefulNumStr = "";
            if (es.Hit * usefulClass < 1 && es.Hit * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Hit * usefulClass);
            }
            else
            {
                usefulNumStr = (es.Hit * usefulClass).ToString();
            }
            desStr += "命中:" + usefulNumStr + "\n";
            InfoDesStr += "命中:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.NoHit > 0)
        {
            string usefulNumStr = "";
            if (es.NoHit * usefulClass < 1 && es.NoHit * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.NoHit * usefulClass);
            }
            else
            {
                usefulNumStr = (es.NoHit * usefulClass).ToString();
            }
            desStr += "闪避:" + usefulNumStr + "\n";
            InfoDesStr += "闪避:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.Crit > 0)
        {
            string usefulNumStr = "";
            if (es.Crit * usefulClass < 1 && es.Crit * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Crit * usefulClass);
            }
            else
            {
                usefulNumStr = (es.Crit * usefulClass).ToString();
            }
            desStr += "暴击:" + usefulNumStr + "\n";
            InfoDesStr += "暴击:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.NoCrit > 0)
        {
            string usefulNumStr = "";
            if (es.NoCrit * usefulClass < 1 && es.NoCrit * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.NoCrit * usefulClass);
            }
            else
            {
                usefulNumStr = (es.NoCrit * usefulClass).ToString();
            }
            desStr += "抗暴:" + usefulNumStr + "\n";
            InfoDesStr += "抗暴:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.DmgBonus > 0)
        {
            string usefulNumStr = "";
            if (es.DmgBonus * usefulClass < 1 && es.DmgBonus * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.DmgBonus * usefulClass);
            }
            else
            {
                usefulNumStr = (es.DmgBonus * usefulClass).ToString();
            }
            desStr += "伤害加成:" + usefulNumStr + "\n";
            InfoDesStr += "加成:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.DmgReduction > 0)
        {
            string usefulNumStr = "";
            if (es.DmgReduction * usefulClass < 1 && es.DmgReduction * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.DmgReduction * usefulClass);
            }
            else
            {
                usefulNumStr = (es.DmgReduction * usefulClass).ToString();
            }
            desStr += "伤害减免:" + usefulNumStr + "\n";
            InfoDesStr += "减免:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        return desStr;
    }
    #endregion
    string GetItemName(int mEquipCode, int _classNumber, int _colorNum)//_colorNum: 1 - 21
    {
        TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(mEquipCode);
        string _nameColor = "";
        switch (mItemInfo.itemGrade)
        {
            case 1: _nameColor = "[ffffff]"; break;
            case 2: _nameColor = "[3ee817]"; break;
            case 3: _nameColor = "[8ccef2]"; break;
            case 4: _nameColor = "[bb44ff]"; break;
            case 5: _nameColor = "[ff8c04]"; break;
            case 6: _nameColor = "[fb2d50]"; break;
        }
        int _NumAdd = GetNumAdd(mItemInfo.itemGrade, _colorNum);
        if (mItemInfo != null && _NumAdd == 0)
        {
            return string.Format("{0}{1}", _nameColor, mItemInfo.itemName);
        }
        else if (mItemInfo != null && _NumAdd >= 1)
        {
            return string.Format("{0}{1} + {2}", _nameColor, mItemInfo.itemName, _NumAdd);
        }
        else return "";
    }
    int GetNumAdd(int _itemGrade, int _colorNum)
    {
        int addNum = 0;
        switch (_itemGrade)
        {
            case 1:
                switch (_colorNum)
                {
                    case 0: addNum = 0; break;
                    case 1: addNum = 0; break;
                    case 2: addNum = 1; break;
                }
                break;
            case 2:
                switch (_colorNum)
                {
                    case 3: addNum = 0; break;
                    case 4: addNum = 1; break;
                    case 5: addNum = 2; break;
                }
                break;
            case 3:
                switch (_colorNum)
                {
                    case 6: addNum = 0; break;
                    case 7: addNum = 1; break;
                    case 8: addNum = 2; break;
                    case 9: addNum = 3; break;
                }
                break;
            case 4:
                switch (_colorNum)
                {
                    case 10: addNum = 0; break;
                    case 11: addNum = 1; break;
                    case 12: addNum = 2; break;
                    case 13: addNum = 3; break;
                    case 14: addNum = 4; break;
                }
                break;
            case 5:
                switch (_colorNum)
                {
                    case 15: addNum = 0; break;
                    case 16: addNum = 1; break;
                    case 17: addNum = 2; break;
                    case 18: addNum = 3; break;
                    case 19: addNum = 4; break;
                    case 20: addNum = 5; break;
                }
                break;
            case 6:
                switch (_colorNum)
                {
                    case 20: addNum = 0; break;
                    case 21: addNum = 1; break;
                    case 22: addNum = 2; break;
                }
                break;
        }
        return addNum;
    }
    public void SetEquipPosition(int position)
    {
        ClickIndex = position;
        if (CharacterRecorder.instance.isNeedRecordStrengTabType == false && StrengEquipWindow.IsEnterEquipUIFromGrabGoods == false)
        {
            strengType = 0;
            equipType = 0;
        }
        if (ClickIndex > 4)
        {
            equipType = 1;
        }
        else
        {
            equipType = 0;
        }
    }
    void SetTab(int _strengType)
    {
        Debug.Log("_strengType..." + _strengType);
        for (int i = 0; i < ListEquip.Count; i++)
        {
            ListEquip[i].SetActive(true);
        }
        topRightObj.SetActive(true);
        equipStrengPart.SetActive(true);
        baoWuStrengPart.SetActive(true);
        equipRefinePart.SetActive(true);
        baoWuRefinePart.SetActive(true);
        awakeWindowPart.SetActive(false);
        secretStoneWindowPart.SetActive(false);
        NuclearWeaponWindow.SetActive(false);
        LuxuryCarWindow.SetActive(false);
        strengType = _strengType;
        SetEquipInfo();
        ListTabs[_strengType].GetComponent<UIToggle>().value = true;
        gameObject.transform.Find("All/RoleCamera").gameObject.SetActive(true);
        gameObject.transform.Find("All/Left").gameObject.SetActive(true);
    }
    /* public void SetTab1()
     {
         //if (UIToggle.current.value)
         {
             strengType = 0;
             SetEquipInfo();
         }
     }

     public void SetTab2()
     {
         //if (UIToggle.current.value)
         {
             strengType = 1;
             SetEquipInfo();
         }
     }*/

    public void SetHeroClick(int Index)
    {
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
    void SetCurrentPart(int _strengType, int _equipType, int _heroIndex, int _equipPos)
    {
        if (_strengType == 0 && _equipType == 0)
        {
            equipStrengPart.SetActive(true);
            baoWuStrengPart.SetActive(false);
            equipRefinePart.SetActive(false);
            baoWuRefinePart.SetActive(false);
            equipStrengPart.GetComponent<EquipStrengerPart>().StarPart(mHero, CharacterRoleID, mHeroInfo, _equipPos);
        }
        else if (_strengType == 0 && _equipType == 1)
        {
            equipStrengPart.SetActive(false);
            baoWuStrengPart.SetActive(true);
            equipRefinePart.SetActive(false);
            baoWuRefinePart.SetActive(false);
            baoWuStrengPart.GetComponent<BaoWuStrengPart>().StarPart(mHero, CharacterRoleID, mHeroInfo, _equipPos);
        }
        else if (_strengType == 1 && _equipType == 0)
        {
            equipStrengPart.SetActive(false);
            baoWuStrengPart.SetActive(false);
            equipRefinePart.SetActive(true);
            baoWuRefinePart.SetActive(false);
            equipRefinePart.GetComponent<EquipRefinePart>().SetInfo(CharacterRoleID, _equipPos);
        }
        else if (_strengType == 1 && _equipType == 1)
        {
            equipStrengPart.SetActive(false);
            baoWuStrengPart.SetActive(false);
            equipRefinePart.SetActive(false);
            baoWuRefinePart.SetActive(true);
            baoWuRefinePart.GetComponent<RefineWindow>().StartPart(CharacterRoleID, _equipPos, mEquipInfo);
            //baoWuRefinePart.GetComponent<RefineWindow>().SetInfo(CharacterRoleID, _equipPos);
        }
        else if (_strengType == 2)
        {
            OnClickListTabAwakeButton();
        }
        else if (_strengType == 3)
        {
            OnClickListTabSecretStone();
        }
    }




    //设置英雄信息
    public void SetHero(int heroIndex)
    {
        Hero h = CharacterRecorder.instance.ownedHeroList[heroIndex];
        HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(h.cardID);

        mHero = h;

        mHeroInfo = hinfo;
        CharacterRoleID = h.characterRoleID;
        Debug.Log(CharacterRoleID + "       ()()()()");
        RoleID = h.cardID;


        // Debug.LogError(_heroInfo.heroRarity);
        switch (hinfo.heroRarity)
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


        PictureCreater.instance.DestroyAllComponent();
        int i = PictureCreater.instance.CreateRole(h.cardID, "Model", 18, Color.red, 0, 0, 1, 2f, false, false, 1, 1.5f, 0.2f, "Model", 0, 10, 10, 2, 2, 0, 0, 1001, 1002, h.WeaponList[0].WeaponClass, 1, h.WeaponList[0].WeaponClass, 1, 1, 0, "");
        PictureCreater.instance.ListRolePicture[0].RoleObject.transform.parent = gameObject.transform.FindChild("All");
        int modelPositionY = 182;
        /* if (h.cardID == 60201 || h.cardID == 60200)////-1370 飞机的话
         {
             modelPositionY = -100;
         }*/
        if ((Screen.width * 1f / Screen.height) > 1.7f)
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = new Vector3(8513 - 636 - 92 - 1085 - 170, modelPositionY, 13339);
        }
        else if ((Screen.width * 1f / Screen.height) < 1.4f)
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = new Vector3(8413 - 636 - 92 - 1085, modelPositionY, 13339);
        }
        else
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = new Vector3(8313 - 636 - 92 - 1085, modelPositionY, 13339);
        }
        //PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localScale = new Vector3(260, 260, 260) * TextTranslator.instance.GetHeroInfoByHeroID(PictureCreater.instance.ListRolePicture[0].RoleID).heroScale;
        //PictureCreater.instance.ListRolePicture[0].RoleObject.transform.Rotate(new Vector3(0, 65, 0));
        PictureCreater.instance.ListRolePicture[0].RolePictureObject.transform.Rotate(new Vector3(0, 65, 0));
        Vector3 modelScale = new Vector3(400, 400, 400);
        if (h.cardID == 60201 || h.cardID == 60200)////-1370 飞机的话
        {
            modelScale = new Vector3(300, 300, 300);
        }
        PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localScale = modelScale;
        //角色旋转、播动画
        GameObject _StrengEquipWindow = GameObject.Find("StrengEquipWindow");
        if (_StrengEquipWindow != null)
        {
            _StrengEquipWindow.GetComponent<PlayAniOrRotation>().ResetTargetRole(PictureCreater.instance.ListRolePicture[0].RoleObject.transform.FindChild("Role").gameObject, h);
        }
        if (strengType == 3)
        {
            OnClickListTabSecretStone();
        }
        else if (strengType == 4)
        {
            OnClickListTabWeaponWindow(1);
        }
        else if (strengType == 5)
        {
            OnClickListTabWeaponWindow(2);
        }
        //else 
        {
            NetworkHandler.instance.SendProcess("3001#" + CharacterRoleID + ";");
        }
        if (GameObject.Find("LuxuryCarWindow") == null)
        {
            NetworkHandler.instance.SendProcess("3201#" + CharacterRoleID + ";");
        }
        NetworkHandler.instance.SendProcess(string.Format("3101#{0};", CharacterRoleID));
        SetRedPointStateOfBaoWuAwake();
        WeaponRedPoint();
    }
    public void OnEquipDataUpEffect()
    {
        StartCoroutine(OnEquipDataUpEffect(LeftInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>()));
        StartCoroutine(OnEquipDataUpEffect(RightInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>()));
    }
    public void ResetEquipData(int _CharacterRoleID, int _Index, int _EquipID, int _Class, int _Lv, int _Exp)
    {
        StartCoroutine(OnEquipDataUpEffect(LeftInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>()));
        StartCoroutine(OnEquipDataUpEffect(RightInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>()));

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
    public void SetEquipIcon(int _equipPosition, int _equipClass)
    {
        Debug.Log("_equipPosition.." + _equipPosition + "_equipClass.." + _equipClass + "_heroRace.." + mHeroInfo.heroRace);
        EquipStrongQuality esq = TextTranslator.instance.GetEquipStrongQualityByID(_equipClass, mHeroInfo.heroRace, _equipPosition);
        //Debug.LogError(esq);
        if (esq != null)
        {
            EquipIcon.spriteName = esq.EquipCode.ToString();
        }
    }
    public void SetEquipIcon(int _equipCode)
    {
        EquipIcon.spriteName = _equipCode.ToString();
    }
    void OpenAdvanceWindow(AdvanceWindowType _AdvanceWindowType)
    {
        switch (_AdvanceWindowType)
        {
            case AdvanceWindowType.EquipJinJie:

                break;
            case AdvanceWindowType.EquipRefine:
                if (RefineWindow.mEquipInfoBefore.equipClass == mEquipInfo.equipClass)
                {
                    return;
                }
                break;
            case AdvanceWindowType.BaoWuUp:
                if (BaoWuStrengPart.mEquipInfoBefore.equipLevel == mEquipInfo.equipLevel)
                {
                    return;
                }
                break;
            case AdvanceWindowType.BaoWuAwake:
                /* if (AwakeWindow.mEquipInfoBefore.equipLevel == mEquipInfo.equipLevel)
                 {
                     return;
                 }*/
                break;
        }
        UIManager.instance.OpenSinglePanel("AdvanceWindow", false);
        GameObject.Find("AdvanceWindow").layer = 11;
        AdvanceWindow aw = GameObject.Find("AdvanceWindow").GetComponent<AdvanceWindow>();

        switch (_AdvanceWindowType)
        {
            case AdvanceWindowType.EquipJinJie: aw.SetInfo(_AdvanceWindowType, null, null, EquipStrengerPart.mEquipInfoBefore, mEquipInfo, null); break;
            case AdvanceWindowType.EquipRefine: aw.SetInfo(_AdvanceWindowType, null, null, RefineWindow.mEquipInfoBefore, mEquipInfo, null); break;
            case AdvanceWindowType.BaoWuUp: aw.SetInfo(_AdvanceWindowType, null, null, BaoWuStrengPart.mEquipInfoBefore, mEquipInfo, null); break;
            //   case AdvanceWindowType.BaoWuAwake: aw.SetInfo(_AdvanceWindowType, null, null, AwakeWindow.mEquipInfoBefore, mEquipInfo, null); break;
            case AdvanceWindowType.BaoWuAwake: aw.SetInfo(_AdvanceWindowType, null, null, AwakeWindow.mEquipInfoBefore, awakeWindowPart.GetComponent<AwakeWindow>().mCurEquipInfo, null); break;
            case AdvanceWindowType.AllEquipJinJie: aw.SetInfo(_AdvanceWindowType, equipStrengPart.GetComponent<EquipStrengerPart>().mEquipInfoBeforeList, mHero.equipList); break;
        }

    }
    public void SetEquipColor(int _CharacterRoleID, int _EquipCode, int _EquipPos, int _colorNum)
    {
        StartCoroutine(OnEquipDataUpEffect(LeftInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>()));
        StartCoroutine(OnEquipDataUpEffect(RightInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>()));
        if (!CharacterRecorder.instance.isOneKeyState)
        {
            OpenAdvanceWindow(AdvanceWindowType.EquipJinJie);
        }

        //Debug.LogError(",,,,,,,,,,,,,,," + _EquipPos + ".................................." + _colorNum);
        string frameName = "";
        TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_EquipCode);
        frameName = string.Format("Grade{0}", mItemInfo.itemGrade);

        ListEquip[_EquipPos - 1].transform.FindChild("Icon").GetComponent<UISprite>().spriteName = _EquipCode.ToString();
        ListEquip[_EquipPos - 1].GetComponent<UISprite>().spriteName = frameName;
        ListEquip[_EquipPos - 1].GetComponent<EquipItemInStreng>().SetEquipItemInStrengh(mHero, _EquipPos);
        //Debug.LogError("...ccccc..." + _EquipPos + ".................................." + _colorNum);
        SetEquipInfo();
    }
    public void SetEquipColorForAll(int _CharacterRoleID, int _EquipCode, int _EquipPos, int _colorNum)
    {
        StartCoroutine(OnEquipDataUpEffect(LeftInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>()));
        StartCoroutine(OnEquipDataUpEffect(RightInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>()));

        OpenAdvanceWindow(AdvanceWindowType.AllEquipJinJie);


        //Debug.LogError(",,,,,,,,,,,,,,," + _EquipPos + ".................................." + _colorNum);
        string frameName = "";
        TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_EquipCode);
        frameName = string.Format("Grade{0}", mItemInfo.itemGrade);

        ListEquip[_EquipPos - 1].transform.FindChild("Icon").GetComponent<UISprite>().spriteName = _EquipCode.ToString();
        ListEquip[_EquipPos - 1].GetComponent<UISprite>().spriteName = frameName;
        ListEquip[_EquipPos - 1].GetComponent<EquipItemInStreng>().SetEquipItemInStrengh(mHero, _EquipPos);
        //Debug.LogError("...ccccc..." + _EquipPos + ".................................." + _colorNum);
        SetEquipInfo();
    }
    //以下唤醒
    public void ScuccedAwakeResult(int _charactorID, int _EquipPositionID, int _itemid, int _equipClass, int _equipLevel, int _equipExp)
    {
        OpenAdvanceWindow(AdvanceWindowType.BaoWuAwake);

        ListEquip[_EquipPositionID - 1].transform.FindChild("Icon").GetComponent<UISprite>().spriteName = _itemid.ToString();
        ListEquip[_EquipPositionID - 1].transform.FindChild("Label").GetComponent<UILabel>().text = _equipLevel.ToString();
        string frameName = "";
        TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_itemid);
        frameName = string.Format("Grade{0}", mItemInfo.itemGrade);
        ListEquip[_EquipPositionID - 1].GetComponent<UISprite>().spriteName = frameName;
        ClickIndex = _EquipPositionID;
        equipRefinePart.GetComponent<EquipRefinePart>().SetInfo(_charactorID, _EquipPositionID);
        SetEquipInfo();

        awakeWindowPart.GetComponent<AwakeWindow>().SetAwakeWindow(CharacterRoleID, ClickIndex, mEquipInfo);
    }

    //以下宝物强化
    public void RestSetBaoWuLevel(int _CharacterRoleID, int _Pos, int _EquipID, int _JingLianLv, int _Lv, int _ExpLv)
    {
        StartCoroutine(OnEquipDataUpEffect(LeftInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>()));
        StartCoroutine(OnEquipDataUpEffect(RightInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>()));

        GameObject go = NGUITools.AddChild(ListEquip[_Pos - 1], LevelUpEffect);
        go.name = "WearEquipEffect";
        go.transform.localScale = new Vector3(500, 500, 500);

        //mEquipInfo = mHero.equipList[_Pos - 1];
        //EquipStrong es = TextTranslator.instance.GetEquipStrongByID(mEquipInfo.equipCode);
        //SetEquipInfo();
        foreach (var h in CharacterRecorder.instance.ownedHeroList)
        {
            if (h.characterRoleID == _CharacterRoleID)
            {
                foreach (var e in h.equipList)
                {
                    if (e.equipID == _EquipID)
                    {
                        e.equipLevel = _Lv;
                        e.equipClass = _JingLianLv;
                        e.equipExp = _ExpLv;
                        break;
                    }
                }
                break;
            }
        }
        baoWuStrengPart.GetComponent<BaoWuStrengPart>().RestSetBaoWuLevel(_CharacterRoleID, _Pos, _EquipID, _JingLianLv, _Lv, _ExpLv);
        IsOnlyAfterBaoWuUp = true;
        //OpenAdvanceWindow(AdvanceWindowType.BaoWuUp);
    }

    //以下装备精炼
    public void PlayEquipRefineDataLabelEffect()
    {
        StartCoroutine(OnEquipDataUpEffect(LeftInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>()));
        StartCoroutine(OnEquipDataUpEffect(RightInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>()));
    }

    #region 饰品觉醒小红点 (新逻辑)
    void SetRedPointStateOfBaoWuAwake()
    {
        // bool canAwakeState = IsCanAwake();
        bool canAwakeState = IsCanAwake(5) || IsCanAwake(6);
        ListTabs[2].transform.FindChild("RedPoint").gameObject.SetActive(canAwakeState);
    }

    /// <summary>
    /// 秘宝小红点
    /// </summary>
    /// <param name="hero"></param>
    public void SetRedPointRareStone(Hero hero)
    {
        ListTabs[3].transform.FindChild("RedPoint").gameObject.SetActive(false);
        List<Item> mItemList = new List<Item>();
        int itemGrade = 0;
        //有没有秘宝
        for (int i = 0; i < TextTranslator.instance.bagItemList.size; i++)
        {
            if (TextTranslator.instance.bagItemList[i].itemCode > 40000 && TextTranslator.instance.bagItemList[i].itemCode < 42000 && TextTranslator.instance.bagItemList[i].itemCount > 0)
            {
                mItemList.Add(TextTranslator.instance.bagItemList[i]);
                if (TextTranslator.instance.bagItemList[i].itemGrade > itemGrade)
                {
                    itemGrade = TextTranslator.instance.bagItemList[i].itemGrade;
                }
            }
        }
        //没有秘宝直接返回
        if (mItemList.Count <= 0)
        {
            return;
        }
        foreach (var i in hero.rareStoneList)
        {
            if (i.stoneId == 0
                && TextTranslator.instance.RareTreasureOpenDic[i.stonePosition].openLevel <= CharacterRecorder.instance.level
                && CharacterRecorder.instance.lastGateID > 10048
                && TextTranslator.instance.RareTreasureOpenDic[i.stonePosition].state == 1)
            {
                ListTabs[3].transform.FindChild("RedPoint").gameObject.SetActive(true);
                break;
            }
            if (i.stoneId != 0
                && TextTranslator.instance.GetItemByItemCode(i.stoneId).itemGrade < itemGrade
                && CharacterRecorder.instance.lastGateID > 10048
                && TextTranslator.instance.RareTreasureOpenDic[i.stonePosition].state == 1)
            {
                ListTabs[3].transform.FindChild("RedPoint").gameObject.SetActive(true);
                break;
            }
        }
    }

    bool IsCanAwake(int equipPos)
    {
        if (CharacterRecorder.instance.level < 16)
        {
            return false;
        }
        Hero.EquipInfo mEquipInfo = mHero.equipList[equipPos - 1];
        TextTranslator.ItemInfo _itemInfo = TextTranslator.instance.GetItemByItemCode(mEquipInfo.equipCode);
        if (_itemInfo != null)
        {
            /* foreach (var _hero in CharacterRecorder.instance.ownedHeroList)
             {
                 if (CharacterRoleID == _hero.characterRoleID)
                 {
                     mHero = _hero;
                     break;
                 }
             }*/
            mHeroInfo = TextTranslator.instance.GetHeroInfoByHeroID(mHero.cardID);
            List<Item> _mList = new List<Item>();
            _mList = TextTranslator.instance.GetAllBaoWuInBag();
            for (int i = _mList.Count - 1; i >= 0; i--)
            {
                EquipStrong _myEquipStrong = TextTranslator.instance.GetEquipStrongByID(_mList[i].itemCode);
                //Debug.Log(_myEquipStrong.Part);
                if (_myEquipStrong == null || _mList[i].itemGrade <= _itemInfo.itemGrade || _myEquipStrong.Race != mHeroInfo.heroRace || _myEquipStrong.Part != mEquipInfo.equipPosition)
                {
                    _mList.Remove(_mList[i]);
                }
            }
            List<Item> usefulList = new List<Item>();
            for (int i = _mList.Count - 1; i >= 0; i--)
            {
                if (!usefulList.Contains(_mList[i]) && _mList[i].itemCount > 0)
                {
                    usefulList.Add(_mList[i]);
                }
            }
            if (usefulList.Count > 0)
            {
                canAwakeBaoWuPos = equipPos;
                return true;
            }
            else
            {
                //UIManager.instance.OpenPromptWindow("没有可更换的饰品", PromptWindow.PromptType.Hint, null, null);
                return false;
            }
        }
        else
        {
            //UIManager.instance.OpenPromptWindow("此装备不是Item", PromptWindow.PromptType.Hint, null, null);
            return false;
        }
    }
    #endregion

    #region 饰品觉醒小红点(新逻辑，有一个饰品可晋级，就有红点)
    /*  void SetRedPointStateOfBaoWuAwake()
    {
        bool canAwakeState = IsCanAwake(5) || IsCanAwake(6);
        ListTabs[2].transform.FindChild("RedPoint").gameObject.SetActive(canAwakeState);
    }

    bool IsCanAwake(int equipPos)
    {
        bool IsBook = false;
        int itemCode = int.Parse(ListEquip[equipPos - 1].transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName);
        if (itemCode / 10 % 2 == 1)//勋章
        {
            IsBook = false;
        }
        else
        {
            IsBook = true;
        }
        for (int i = 0; i < CharacterRecorder.instance.ownedHeroList.size;i++ )
        {

        }
    }*/
    #endregion

    #region 新的晋级（觉醒）UI
    void OnClickListTabAwakeButton()
    {
        for (int i = 0; i < ListEquip.Count; i++)
        {
            ListEquip[i].SetActive(false);
        }
        topRightObj.SetActive(false);
        equipStrengPart.SetActive(false);
        baoWuStrengPart.SetActive(false);
        equipRefinePart.SetActive(false);
        baoWuRefinePart.SetActive(false);
        awakeWindowPart.SetActive(true);
        secretStoneWindowPart.SetActive(false);
        NuclearWeaponWindow.SetActive(false);
        LuxuryCarWindow.SetActive(false);

        strengType = 2;
        //如果当前不是饰品，强制到饰品的晋级
        Debug.LogError("strengType..........." + strengType);
        if (equipType != 1)
        {
            equipType = 1;
            ClickIndex = canAwakeBaoWuPos;
            mEquipInfo = mHero.equipList[ClickIndex - 1];
        }


        GameObject _AwakeWindow = GameObject.Find("AwakeWindow");
        _AwakeWindow.GetComponent<AwakeWindow>().SetAwakeWindow(CharacterRoleID, ClickIndex, mEquipInfo);
        if (myHeroEquipList != null)
        {
            _AwakeWindow.GetComponent<AwakeWindow>().SetEuipList(CharacterRoleID, myHeroEquipList);
        }
        gameObject.transform.Find("All/RoleCamera").gameObject.SetActive(true);

        /*  _AwakeWindow.layer = 11;
          foreach (Component c in _AwakeWindow.GetComponentsInChildren(typeof(Transform), true))
          {
              c.gameObject.layer = 11;
          }*/
    }
    #endregion
    #region 豪车小红点
    public void SuperCarRedPoint(string[] dataSplit)
    {
        int CarID = 0;

        //好车没开放直接返回
        if (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zuojia).Level > CharacterRecorder.instance.lastGateID)
        {
            return;
        }
        int count = 0;
        for (int i = 0; i < dataSplit.Length - 2; i++)
        {
            string[] SuccseID = dataSplit[i].Split('$');
            if (SuccseID[1] == "0")
            {
                CarID = 42001 + i;
                SuperCar CarItem = TextTranslator.instance.GetSuperCarByID(CarID);
                if (TextTranslator.instance.GetItemCountByID(CarID) >= CarItem.NeedDebris && CharacterRecorder.instance.level >= CarItem.NeedLevel)
                {
                    count++;
                    ListTabs[4].transform.Find("RedPoint").gameObject.SetActive(true);
                    break;
                }
            }
        }
        if (count == 0)
        {
            ListTabs[4].transform.Find("RedPoint").gameObject.SetActive(false);
        }
    }
    #endregion

    #region 核武器

    #endregion

    #region 秘宝UI
    void OnClickListTabSecretStone()
    {
        for (int i = 0; i < ListEquip.Count; i++)
        {
            ListEquip[i].SetActive(false);
        }
        topRightObj.SetActive(false);
        equipStrengPart.SetActive(false);
        baoWuStrengPart.SetActive(false);
        equipRefinePart.SetActive(false);
        baoWuRefinePart.SetActive(false);
        awakeWindowPart.SetActive(false);
        NuclearWeaponWindow.SetActive(false);
        LuxuryCarWindow.SetActive(false);
        secretStoneWindowPart.SetActive(true);
        strengType = 3;
        GameObject _AwakeWindow = GameObject.Find("SecretStoneWindow");
        _AwakeWindow.GetComponent<SecretStoneWindow>().SetSecretStoneWindow(mHero, ClickIndex, mEquipInfo);
        gameObject.transform.Find("All/RoleCamera").gameObject.SetActive(true);
    }
    #endregion

    #region 核武器UI 车库
    void OnClickListTabWeaponWindow(int id)
    {
        for (int i = 0; i < ListEquip.Count; i++)
        {
            ListEquip[i].SetActive(false);
        }
        topRightObj.SetActive(false);
        equipStrengPart.SetActive(false);
        baoWuStrengPart.SetActive(false);
        equipRefinePart.SetActive(false);
        baoWuRefinePart.SetActive(false);
        awakeWindowPart.SetActive(false);
        secretStoneWindowPart.SetActive(false);
        NuclearWeaponWindow.SetActive(false);
        LuxuryCarWindow.SetActive(false);
        if (id == 1)
        {
            LuxuryCarWindow.SetActive(true);
            gameObject.transform.Find("All/Left").gameObject.SetActive(false);
            strengType = 4;
            GameObject _CarWindow = GameObject.Find("LuxuryCarWindow");
            _CarWindow.GetComponent<LuxuryCarWindow>().HeroIDInfo(mHero);
            NetworkHandler.instance.SendProcess(string.Format("3201#{0};", mHero.characterRoleID));
        }
        else if (id == 2)
        {
            MasterBtn.SetActive(false);
            NuclearWeaponWindow.SetActive(true);
            strengType = 5;
            GameObject _WeaponWindow = GameObject.Find("NuclearWeaponWindow");
            _WeaponWindow.GetComponent<NuclearWeaponWindow>().HeroIDInfo(mHero);
            NetworkHandler.instance.SendProcess("3301#" + mHero.characterRoleID + ";1;");
        }
    }
    #endregion
    #region 夺宝 夺到后 直接调到 晋级UI
    public void EnterEquipAwakeUIFromGrabGoods(int position)
    {
        if (IsEnterEquipUIFromGrabGoods)
        {
            PlayAniOrRotation.IsNeedAutoPlayAni = true;
            //Debug.LogError("HeroClick");
            //IsNeedResetHedIndexAfterSortHero = true;
            InitHeroList();
            AddDaoJuEffect(EquipIcon);
            /* for (int i = 0; i < ListEquip.Count;i++ )
             {
                 UIEventListener.Get(ListEquip[i]).onClick = ClickListEquip;
             }*/
            for (int i = 0; i < ListTabs.Count; i++)
            {
                UIEventListener.Get(ListTabs[i]).onClick += ClickListTabs;
                if (i == 2)
                {
                    ListTabs[i].GetComponent<UIToggle>().startsActive = true;
                    ListTabs[i].GetComponent<UIToggle>().value = true;
                }
                else
                {
                    ListTabs[i].GetComponent<UIToggle>().startsActive = false;
                }
            }
            //Debug.LogError("HeroClick" + TextTranslator.instance.HeadIndex);
            //SetHeroClick(TextTranslator.instance.HeadIndex);
            InitEquipItemOpenState();

            ClickIndex = position; //equipType = 1;
            //  Invoke("DelayOnClickListTabAwakeButton",0.3f);
            SetEquipInfo();
            OnClickListTabAwakeButton();
        }
    }
    void DelayOnClickListTabAwakeButton()
    {
        SetEquipInfo();
        OnClickListTabAwakeButton();
        IsEnterEquipUIFromGrabGoods = false;
    }
    #endregion

    #region 强化大师升级

    public void MasterUpgrade(string RecvString)
    {
        masterSucess.SetActive(true);


        string[] dataSplit = RecvString.Split(';');

        masterSucess.GetComponent<StrengthenMasterPart>().SetStrengthenMasterInfo(int.Parse(dataSplit[1]), int.Parse(dataSplit[2]), int.Parse(dataSplit[3]), int.Parse(dataSplit[4]));
    }

    #endregion

    #region 核武器红点
    public void WeaponRedPoint()
    {
        //Debug.LogError("sssssssssssss1");
        if (CharacterRecorder.instance.lastGateID + 1 > TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.hewuqi).Level &&
            CharacterRecorder.instance.WeaponMainWindow(mHero.cardID, mHero.WeaponList[0].WeaponID, mHero.WeaponList[0].WeaponClass, mHero.WeaponList[0].WeaponStar))
        {
            //  Debug.LogError("sssssssssssss3");
            ListTabs[5].transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else
        {
            ListTabs[5].transform.Find("RedPoint").gameObject.SetActive(false);
        }
        //Debug.LogError("sssssssssssss2");
    }
    #endregion
}
