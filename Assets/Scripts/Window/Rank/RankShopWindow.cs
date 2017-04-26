using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RankShopWindow : MonoBehaviour
{
    [HideInInspector]
    public static string curSelectShopType = "10001";
    public GameObject MyGrid;
    public GameObject MyRankShopItem;
    public UISprite spriteType;
    public UILabel HonerNumber;
    public UILabel labelLeftTime;
    public UILabel labelNextRefreshCost;

    public GameObject LeftButton;
    public GameObject RightButton;

    //public UILabel labelGoldCoins;
    //public UISprite spriteCoins;
    public UILabel labelMoney;
    //private string spriteNmae;
    private string StrMoney;

    [HideInInspector]
    public List<GameObject> ListRankShopItem = new List<GameObject>();
    [SerializeField]
    private List<GameObject> shopTypeList = new List<GameObject>();
    [SerializeField]
    private GameObject buttonRefresh;
    private int leftTime = 0;
    private int refreshCost = 0;

    public UICenterOnChild uicenter;
    private int Index=0;//翻页次数

    List<Item> NeedItemList = new List<Item>();
    Color tab4Color = new Color(99 / 255f, 212 / 255f, 241 / 255f, 255 / 255f);
    Color tab4OutlineColor = new Color(38 / 255f, 123 / 255f, 146 / 255f, 255 / 255f);
    Color tab3Color = new Color(50 / 255f, 152 / 255f, 179 / 255f, 255 / 255f);
    Color tab3OutlineColor = new Color(16 / 255f, 97 / 255f, 120 / 255f, 255 / 255f);
    void OnEnable()
    {
        GetHeroNeedItem();
        if (GameObject.Find("WoodsTheExpendables") != null)
        {
            curSelectShopType = "10004";
            NetworkHandler.instance.SendProcess("5102#10004;");
            shopTypeList[2].GetComponent<UISprite>().spriteName = "tab4";
            shopTypeList[2].transform.Find("Label").GetComponent<UILabel>().color = tab4Color;
            shopTypeList[2].transform.Find("Label").GetComponent<UILabel>().effectColor = tab4OutlineColor;
        }
        else if (CharacterRecorder.instance.RankShopType == 10003) 
        {
            curSelectShopType = "10003";
            NetworkHandler.instance.SendProcess("5102#10003;");
            shopTypeList[1].GetComponent<UISprite>().spriteName = "tab4";
            shopTypeList[1].transform.Find("Label").GetComponent<UILabel>().color = tab4Color;
            shopTypeList[1].transform.Find("Label").GetComponent<UILabel>().effectColor = tab4OutlineColor;
            CharacterRecorder.instance.RankShopType = 10001;
        }
        else if (CharacterRecorder.instance.RankShopType == 10004)
        {
            curSelectShopType = "10004";
            NetworkHandler.instance.SendProcess("5102#10004;");
            shopTypeList[2].GetComponent<UISprite>().spriteName = "tab4";
            shopTypeList[2].transform.Find("Label").GetComponent<UILabel>().color = tab4Color;
            shopTypeList[2].transform.Find("Label").GetComponent<UILabel>().effectColor = tab4OutlineColor;
            CharacterRecorder.instance.RankShopType = 10001;
        }
        else if (CharacterRecorder.instance.RankShopType == 10005) 
        {
            curSelectShopType = "10005";
            NetworkHandler.instance.SendProcess("5102#10005;");
            shopTypeList[3].GetComponent<UISprite>().spriteName = "tab4";
            shopTypeList[3].transform.Find("Label").GetComponent<UILabel>().color = tab4Color;
            shopTypeList[3].transform.Find("Label").GetComponent<UILabel>().effectColor = tab4OutlineColor;
            CharacterRecorder.instance.RankShopType = 10001;
        }
        else if (CharacterRecorder.instance.RankShopType == 10006)
        {
            curSelectShopType = "10006";
            NetworkHandler.instance.SendProcess("5102#10006;");
            shopTypeList[5].GetComponent<UISprite>().spriteName = "tab4";
            shopTypeList[5].transform.Find("Label").GetComponent<UILabel>().color = tab4Color;
            shopTypeList[5].transform.Find("Label").GetComponent<UILabel>().effectColor = tab4OutlineColor;
            CharacterRecorder.instance.RankShopType = 10001;
        }
        else if (CharacterRecorder.instance.RankShopType == 10007)
        {
            curSelectShopType = "10007";
            NetworkHandler.instance.SendProcess("5102#10007;");
            shopTypeList[4].GetComponent<UISprite>().spriteName = "tab4";
            shopTypeList[4].transform.Find("Label").GetComponent<UILabel>().color = tab4Color;
            shopTypeList[4].transform.Find("Label").GetComponent<UILabel>().effectColor = tab4OutlineColor;
            CharacterRecorder.instance.RankShopType = 10001;
        }
        else
        {
            curSelectShopType = "10001";
            NetworkHandler.instance.SendProcess("5102#10001;");
            shopTypeList[0].GetComponent<UISprite>().spriteName = "tab4";
            shopTypeList[0].transform.Find("Label").GetComponent<UILabel>().color = tab4Color;
            shopTypeList[0].transform.Find("Label").GetComponent<UILabel>().effectColor = tab4OutlineColor;
        }
    }
    // Use this for initialization
    void Start()
    {
        //OpenShopType();
        UIManager.instance.CountSystem(UIManager.Systems.商店);
        UIManager.instance.UpdateSystems(UIManager.Systems.商店);

        /* if (UIEventListener.Get(GameObject.Find("ShopCloseButton")).onClick == null)
         {
             UIEventListener.Get(GameObject.Find("ShopCloseButton")).onClick += delegate(GameObject go)
             {
                 UIManager.instance.BackUI();
             };
         }

         RankWindow rw = GameObject.Find("RankWindow").GetComponent<RankWindow>();
        
         HonerNumber.text = rw.HonerNumber.ToString(); */
        //HonerNumber.text = CharacterRecorder.instance.HonerValue.ToString();
        for (int i = 0; i < shopTypeList.Count; i++)
        {
            UIEventListener.Get(shopTypeList[i]).onClick = ClickShopTypeButton;
        }
        UIEventListener.Get(buttonRefresh).onClick = ClickRefreshButton;
        //if (CharacterRecorder.instance.level < 23) 
        //{
        //    GameObject.Find("RankShopWindow/All/TopTabs/10005/Lock").GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        //}
        //else if (CharacterRecorder.instance.level >= 23) 
        //{
        //    GameObject.Find("RankShopWindow/All/TopTabs/10005/Lock").GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
        //}

        if (UIEventListener.Get(LeftButton).onClick== null) 
        {
            UIEventListener.Get(LeftButton).onClick += delegate(GameObject go)
            {
                CenterOnChildLeft();
            };
        }
        if (UIEventListener.Get(RightButton).onClick == null)
        {
            UIEventListener.Get(RightButton).onClick += delegate(GameObject go)
            {
                CenterOnChildRight();
            };
        }
    }
    public void SetShopWindow(int _leftTime, int _nextRefreshCost, List<ShopItemData> _ShopItemList, int NowMoney)
    {
        //位置ID$物品ID$数量$星币售价$月石售价$荣耀值售价;
        //int _ItemCode, int _ItemGrade, int _ItemCount, int _Index, int _GoldPrice, int _DiamondPrice, int _HonourPrice
        DestroyGride();
        leftTime = _leftTime;
        refreshCost = _nextRefreshCost;
        labelNextRefreshCost.text = _nextRefreshCost.ToString();
        StrMoney = NowMoney.ToString();
        //spriteType.spriteName = "icon" + _ShopItemList[0].priceType.ToString();
        if (_ShopItemList[_ShopItemList.Count - 1].priceType == 7)
        {
            spriteType.spriteName = "90010";
        }
        else 
        {
            spriteType.spriteName = "9000" + _ShopItemList[_ShopItemList.Count - 1].priceType.ToString();
        }
        //labelMoney.text= TextTranslator.instance.GetItemCountByID(10901).ToString();
        switch (_ShopItemList[_ShopItemList.Count-1].priceType)
        {
            case 1: HonerNumber.text = StrMoney + " 金币,用于普通商店的购买";
                    //spriteType.spriteName = "icon2";
                break;
            case 2: HonerNumber.text = StrMoney + " 钻石,用于VIP商店购买";
                //spriteType.spriteName = "icon2";
                break;
            case 3:
                HonerNumber.text = StrMoney + " 荣誉币,可在竞技场获得";
                break;
            case 4:
                HonerNumber.text = StrMoney + " 试炼币,可在丛林冒险获得";
                break;
            case 5:
                HonerNumber.text = StrMoney + " 军团币,可在军团获得";
                break;
            case 6:
                HonerNumber.text = StrMoney + " 金条,可在边境走私获得";
                break;
            case 7:
                HonerNumber.text = StrMoney + " 王者币,可在王者之路获得";
                break;
            default:
                break;
        }
        CancelInvoke();
        InvokeRepeating("UpdateTime", 0, 1.0f);
        
        for (int i = 0; i < _ShopItemList.Count; i++)
        {           
            GameObject go = NGUITools.AddChild(MyGrid, MyRankShopItem);
            go.GetComponent<RankShopItem>().Init(_ShopItemList[i]);

            for (int j = 0; j < NeedItemList.Count;j++ )
            {
                if (NeedItemList[j].itemCode == _ShopItemList[i].Id && _ShopItemList[i].count!=0) //有且数量不为0；
                {
                    go.GetComponent<RankShopItem>().GreenPoint.SetActive(true);
                    break;
                }
            }
            ListRankShopItem.Add(go);
        }
        MyGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        MyGrid.GetComponent<UIGrid>().Reposition();
    }
    void DestroyGride()
    {
        ListRankShopItem.Clear();
        for (int i = MyGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(MyGrid.transform.GetChild(i).gameObject);
        }
    }
    void UpdateTime()
    {
        if (leftTime > 0)
        {
            string houreStr = (leftTime / 3600).ToString("00");
            string miniteStr = (leftTime % 3600 / 60).ToString("00");
            string secondStr = (leftTime % 60).ToString("00");
            labelLeftTime.text = string.Format("{0}:{1}:{2}", houreStr, miniteStr, secondStr);
            leftTime -= 1;
        }
        else if (leftTime == 0) 
        {
            NetworkHandler.instance.SendProcess(string.Format("5105#{0};", curSelectShopType));
            leftTime -= 1;
        }
    }
    private void ClickShopTypeButton(GameObject go)
    {
        //int level=CharacterRecorder.instance.level;
        //if (go != null && curSelectShopType != go.name&&go.name!="10005")
        //{
        //    curSelectShopType = go.name;
        //    NetworkHandler.instance.SendProcess(string.Format("5102#{0};", curSelectShopType));
        //}
        //else if (go != null && curSelectShopType != go.name && go.name == "10005" && level >= 23)
        //{
        //    curSelectShopType = go.name;
        //    NetworkHandler.instance.SendProcess(string.Format("5102#{0};", curSelectShopType));
        //}
        if (go != null && curSelectShopType != go.name)
        {
            curSelectShopType = go.name;
            NetworkHandler.instance.SendProcess(string.Format("5102#{0};", curSelectShopType));
        }
        SetButtonType(go);
    }
    private void SetButtonType(GameObject ButtonType)
    {
        
        for (int i = 0; i < 7; i++) 
        {
            if (shopTypeList[i].name != ButtonType.name)
            {
                shopTypeList[i].GetComponent<UISprite>().spriteName = "tab3";
                shopTypeList[i].transform.Find("Label").GetComponent<UILabel>().color = tab3Color;
                shopTypeList[i].transform.Find("Label").GetComponent<UILabel>().effectColor = tab3OutlineColor;
            }
            else 
            {
                shopTypeList[i].GetComponent<UISprite>().spriteName = "tab4";
                shopTypeList[i].transform.Find("Label").GetComponent<UILabel>().color = tab4Color;
                shopTypeList[i].transform.Find("Label").GetComponent<UILabel>().effectColor = tab4OutlineColor;
            }
        }
    }
    private void ClickRefreshButton(GameObject go)
    {
        if (go != null)
        {
            // NetworkHandler.instance.SendProcess(string.Format("5105#{0};", curSelectShopType));
            if (refreshCost > 0)
            {
                UIManager.instance.OpenPromptWindow(string.Format("是否消耗{0}{1}刷新？", refreshCost, "钻石"), PromptWindow.PromptType.Confirm, ConfirmResfresh, null);
            }
            else
            {
                ConfirmResfresh();
            }
        }
        //GameObject.Find("TopContent").GetComponent<TopContent>().Reset();
    }
    private void ConfirmResfresh()
    {
        NetworkHandler.instance.SendProcess(string.Format("5105#{0};", curSelectShopType));       
    }

    public void SetMoneyNum(int priceType, int gold, int Diamond, int HonerValue, int TrialCurrency, int ArmyGroup, int GoldBar,int KingCoin)
    {
        //labelMoney.text = NowMoney.ToString();
        //string spriteName = spriteType.spriteName;
        switch (priceType)
        {
            case 10001: HonerNumber.text = gold.ToString() + " 金币,用于普通商店的购买";
                break;
            case 10003: HonerNumber.text = HonerValue.ToString() + " 荣誉币,可在竞技场获得";
                break;
            case 10004: HonerNumber.text = TrialCurrency.ToString() + " 试炼币,可在丛林冒险获得";
                break;
            case 10005: HonerNumber.text = ArmyGroup.ToString() + " 军团币,可在军团获得";
                break;
            case 10006: HonerNumber.text = GoldBar.ToString() + " 金条,可在边境走私获得";
                break;
            case 10007: HonerNumber.text = KingCoin.ToString() + " 王者币,可在王者之路获得";
                break;
            case 10008: HonerNumber.text = Diamond.ToString() + " 钻石,用于VIP商店购买";
                break;
            default:
                break;
        }
    }

    void CenterOnChildRight()
    {
        uicenter.enabled = false;
        uicenter.enabled = true;
        for (int i = 0; i < MyGrid.transform.childCount; i++)
        {
            if (uicenter.centeredObject.name == MyGrid.transform.GetChild(i).gameObject.name)
            {
                Index = i;
            }
        }
        if (ListRankShopItem.Count - Index >= 5)
        {
            Transform traobj = uicenter.transform.GetChild(Index + 2);
            uicenter.CenterOn(traobj);
            Index += 2;
        }
        //uicenter.enabled = false;
    }
    void CenterOnChildLeft()
    {
        uicenter.enabled = false;
        uicenter.enabled = true;
        for (int i = 0; i < MyGrid.transform.childCount; i++)
        {
            if (uicenter.centeredObject.name == MyGrid.transform.GetChild(i).gameObject.name)
            {
                Index = i;
            }
        }
        if (Index - 2 > 0)
        {
            Transform traobj = uicenter.transform.GetChild(Index - 2);
            uicenter.CenterOn(traobj);
            Index -= 2;
        }
        //uicenter.enabled = false;
    }

    void GetHeroNeedItem() //商店绿点
    {
        //BetterList<Hero> NewHeroList = new BetterList<Hero>();//取前五个战力最高的
        int listSize = CharacterRecorder.instance.ownedHeroList.size;////战力降序排序
        //List<Item> NeedItemList = new List<Item>();
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
        for (int i = 0; i < listSize; i++)//取前五个战力最高的
        {
            if (i < 5)
            {
                Hero mHero = CharacterRecorder.instance.ownedHeroList[i];
                RoleClassUp rcu = TextTranslator.instance.GetRoleClassUpInfoByID(mHero.cardID, mHero.classNumber);
                List<Item> classUpList = new List<Item>();
                for (int j = 0; j < rcu.NeedItemList.Count; j++)//升品需要的物品
                {
                    if (rcu.NeedItemList[j].itemCode != 0)
                    {
                        classUpList.Add(rcu.NeedItemList[j]);
                    }
                }
                for (int x = 0; x < classUpList.Count; x++)//背包中对应的物品数量是否足够
                {
                    int bagItemCount1 = TextTranslator.instance.GetItemCountByID(classUpList[x].itemCode);
                    //Debug.Log("bagItemCount1" + bagItemCount1);
                    //Debug.Log("classUpList[i].itemCount" + classUpList[x].itemCount);
                    if (bagItemCount1 < classUpList[x].itemCount)
                    {
                        //if (!NeedItemList.Contains(classUpList[x]))
                        //{
                        //    NeedItemList.Add(classUpList[x]);
                        //    Debug.LogError(classUpList[x].itemCode);
                        //}
                        int IsHave=0;
                        for (int y = 0; y < NeedItemList.Count; y++) 
                        {
                            if (classUpList[x].itemCode == NeedItemList[y].itemCode) 
                            {
                                IsHave = 1;
                                break;
                            }
                        }
                        if (IsHave == 0) 
                        {
                            NeedItemList.Add(classUpList[x]);
                            Debug.Log("需要的Item  "+classUpList[x].itemCode);
                        }
                    }
                }
            }
        }
        if (CharacterRecorder.instance.lastGateID > 10007) //是否开放强化
        {
            for (int i = 0; i < listSize; i++)//强化所需要的材料
            {
                if (i < 5)
                {
                    Hero mHero = CharacterRecorder.instance.ownedHeroList[i];
                    HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(mHero.cardID);
                    for (int j = 0; j < 4; j++)
                    {
                        Hero.EquipInfo mEquipInfo = mHero.equipList[j];
                        EquipStrongQuality esq = TextTranslator.instance.GetEquipStrongQualityByIDAndColor(mHero.equipList[j].equipCode, mHero.equipList[j].equipColorNum, hinfo.heroRace);
                        if (IsAdvanceState(mEquipInfo.equipLevel, mEquipInfo.equipColorNum))//单个装备是否能够强化
                        {
                            for (int x = 0; x < 4; x++) //单个装备强化需要的材料
                            {
                                if (x < esq.materialItemList.size)
                                {
                                    int itemCode = esq.materialItemList[x].itemCode;
                                    int itemCountInBag = TextTranslator.instance.GetItemCountByID(itemCode);
                                    //Item _Item = TextTranslator.instance.GetItemByID(itemCode);//TextTranslator.instance.GetItemByID(itemCode);
                                    if (itemCountInBag < esq.materialItemList[x].itemCount)
                                    {
                                        Item _Item = new Item(itemCode, esq.materialItemList[x].itemCount - itemCountInBag);
                                        //if (!NeedItemList.Contains(_Item))
                                        //{
                                        //    NeedItemList.Add(_Item);
                                        //    Debug.LogError(_Item.itemCode);
                                        //}
                                        int IsHave = 0;
                                        for (int y = 0; y < NeedItemList.Count; y++)
                                        {
                                            if (itemCode == NeedItemList[y].itemCode)
                                            {
                                                IsHave = 1;
                                                break;
                                            }
                                        }
                                        if (IsHave == 0)
                                        {
                                            NeedItemList.Add(_Item);
                                            Debug.Log("需要的Item  " + _Item.itemCode);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        Debug.Log("需要item数量  " + NeedItemList.Count);
    }

    bool IsAdvanceState(int _EquipLevel, int _EquipColorNum)////是否能够强化
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

    private void OpenShopType() 
    {
        Transform parent = this.gameObject.transform;
        if (CharacterRecorder.instance.lastGateID > 10011)
        {
            shopTypeList[1].SetActive(true);
        }
        else 
        {
            shopTypeList[1].transform.parent = parent;
            shopTypeList[1].SetActive(false);
        }

        if (CharacterRecorder.instance.lastGateID > 10057)
        {
            shopTypeList[2].SetActive(true);
        }
        else
        {
            shopTypeList[2].transform.parent = parent;
            shopTypeList[2].SetActive(false);
        }

        if (CharacterRecorder.instance.lastGateID > 10050)
        {
            shopTypeList[3].SetActive(true);
        }
        else
        {
            shopTypeList[3].transform.parent = parent;
            shopTypeList[3].SetActive(false);
        }

        if (CharacterRecorder.instance.lastGateID > 10086)
        {
            shopTypeList[4].SetActive(true);
        }
        else
        {
            shopTypeList[4].transform.parent = parent;
            shopTypeList[4].SetActive(false);
        }

        if (CharacterRecorder.instance.lastGateID > 10074)
        {
            shopTypeList[5].SetActive(true);
        }
        else
        {
            shopTypeList[5].transform.parent = parent;
            shopTypeList[5].SetActive(false);
        }

        shopTypeList[0].transform.parent.GetComponent<UIGrid>().Reposition();
    }
}
