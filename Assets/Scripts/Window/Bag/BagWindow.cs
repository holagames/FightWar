using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BagWindow : MonoBehaviour
{
    public GameObject ScrollViewlist;
    public static GameObject SelectedItem;
    public GameObject MyGrid;
    public GameObject MyBagItem;
    public UILabel BagCount;

    public UILabel ItemName;
    public UILabel ItemCount;
    public UILabel ItemSellNumber;
    public UILabel ItemDes;
    public GameObject ItemIcon;
    public GameObject DetailItemBoard;
    public GameObject SellItemBoard;

    public GameObject SellMoney;
    public GameObject SellButton;
    public GameObject CombineButton;
    public GameObject UseButton;
    public GameObject BigUseButton;
    public GameObject GetOnButton;
    public GameObject SuiPian;

    public UIAtlas RoleAtlas;
    public UIAtlas ItemAtlas;

    public GameObject NewHeroBoard;
    int CurItemNeedPic = 0;

    List<GameObject> ListBag = new List<GameObject>();

    int ClickIndex = 0;
    int _next_item_index = 0;
    //private Vector3 scrollViewInitPos = new Vector3(-125, -30, 0);//new Vector3(0, 10, 0);
    private Vector3 scrollViewInitPos = new Vector3(-112, -80, 0);
    [HideInInspector]
    public int curItemID = 0;


    void OnEnable()
    {
        if (TextTranslator.instance.isUpdateBag)
        {
            NetworkHandler.instance.SendProcess("5001#;");
        }
        else
        {

        }
    }

    public void UpDataBag()
    {
        //SetTab(ClickIndex);
        SetTab(CharacterRecorder.instance.BagIndex);
        //UIManager.instance.OpenPanel("CardWindow", true);
        //GameObject.Find("CardWindow").GetComponent<CardWindow>().SetCardInfo(RoleID);
    }

    public void SetTab(int Index)  //
    {
        Debug.Log(CharacterRecorder.instance.BagIndex + "   ()()()   " + Index);
        int newGuidLock = 0;
        SpringPanel _SpringPanel = MyGrid.transform.parent.GetComponent<SpringPanel>();
        if (_SpringPanel != null)
        {
            _SpringPanel.enabled = false;
        }

        CharacterRecorder.instance.BagIndex = Index;

        if (Index != 1)
        {
            GameObject.Find("Tab1Button").GetComponent<UIToggle>().value = false;
            GameObject.Find("Tab1Button").GetComponent<UIToggle>().startsActive = false;
            GameObject.Find("Tab" + CharacterRecorder.instance.BagIndex + "Button").GetComponent<UIToggle>().value = true;
        }
        else
        {
            GameObject.Find("Tab1Button").GetComponent<UIToggle>().value = true;
            GameObject.Find("Tab1Button").GetComponent<UIToggle>().startsActive = true;
        }
        if (ClickIndex != Index)
        {
            ClickIndex = Index;
            _next_item_index = 0;
        }
        foreach (var h in ListBag)
        {
            DestroyImmediate(h);
        }
        ListBag.Clear();

        if (Index == 4)
        {
            SellMoney.SetActive(false);
            SellButton.SetActive(false);
            CombineButton.SetActive(true);

            if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 15)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1402);
                newGuidLock = 1;
                SetBagClick(70028);
            }
        }
        else
        {
            SellMoney.SetActive(true);
            SellButton.SetActive(true);
            CombineButton.SetActive(false);
        }

        int i = 0;
        ScrollViewlist.GetComponent<ScrollViewControl>().enabled = false;
        int count = BagSortShowItem(Index);
        StartCoroutine(wait());
        //foreach (var h in TextTranslator.instance.bagItemList)
        //{
        //    TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(h.itemCode);
        //    mitemInfo.itemType = TextTranslator.instance.GetItemTypeByItemCode(h.itemCode);
        //    count++;
        //    if (Index == 2)
        //    {
        //        if (mitemInfo.itemType != 5)
        //        {
        //            continue;
        //        }
        //    }
        //    else if (Index == 3)
        //    {
        //        if (mitemInfo.itemType != 3)
        //        {
        //            continue;
        //        }
        //    }
        //    else if (Index == 4)
        //    {
        //        if (mitemInfo.itemType != 7)
        //        {
        //            continue;
        //        }
        //    }

        //    if (h.itemCount > 0)
        //    {
        //        GameObject go = NGUITools.AddChild(MyGrid, MyBagItem);
        //        go.name = h.itemCode.ToString();
        //      //  go.AddComponent<BagItem>();
        //        go.GetComponent<BagItem>().RoleAtlas = RoleAtlas;
        //        go.GetComponent<BagItem>().Init(h.itemCode, h.itemGrade, h.itemCount, h.equipID, i);
        //        go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("ScrollView").GetComponent<UIScrollView>();
        //        ListBag.Add(go);
        //        i++;
        //    }
        //}

        //if (curItemID == 0 && ListBag.Count > 0)
        //{
        //    SelectedItem = ListBag[0];
        //    //curItemID = int.Parse(ListBag[0].name);
        //}
        if (newGuidLock != 1)
        {
            if (ListBag.Count > _next_item_index)
            {
                SelectedItem = ListBag[_next_item_index];
                curItemID = int.Parse(ListBag[_next_item_index].name);
            }
            else
            {
                curItemID = 0;
                SellMoney.SetActive(false);
                SellButton.SetActive(false);
                CombineButton.SetActive(false);
                UseButton.SetActive(false);
                BigUseButton.SetActive(false);
                GetOnButton.SetActive(false);
                initItemInfo();
            }
        }

        if (curItemID > 0)
        {
            SetBagClick(curItemID);
            BagCount.text = string.Format("背包上限:[bafffe]{0}{1}/{2}", "[00eaff]", count, 999);
            MyGrid.GetComponent<UIGrid>().Reposition();
        }

    }

    int BagSortShowItem(int _index)
    {
        BetterList<Item> _itemList = TextTranslator.instance.bagItemList;
        int count = 0;

        foreach (var item in TextTranslator.instance.itemSortList)
        {
            for (int i = 0; i < _itemList.size; i++)
            {
                if (item.ItemID == _itemList[i].itemCode)
                {
                    TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(_itemList[i].itemCode);
                    mitemInfo.itemType = TextTranslator.instance.GetItemTypeByItemCode(_itemList[i].itemCode);
                    count++; 
                    if (_index == 2)
                    {
                        if (mitemInfo.itemType != 5)
                        {
                            continue; 
                        }
                    }
                    else if (_index == 3)
                    {
                        if (mitemInfo.itemType != 3)
                        {
                            continue; 
                        }
                    }
                    else if (_index == 4)
                    {
                        if (mitemInfo.itemType != 7)
                        {
                            continue;
                        }
                    }
                    else if (_index == 5)
                    {
                        if (mitemInfo.FuncType != 19)
                        {
                            continue;
                        }
                    }
                    else if (_index == 6 && (mitemInfo.itemType == 5 || mitemInfo.itemType == 7 || mitemInfo.itemType == 3 || mitemInfo.FuncType == 19))
                    {
                        continue; ;
                    }
                    if (_itemList[i].itemCount > 0)
                    {
                        GameObject go = NGUITools.AddChild(MyGrid, MyBagItem);
                        go.name = item.ItemID.ToString();
                        //  go.AddComponent<BagItem>();
                        go.GetComponent<BagItem>().RoleAtlas = RoleAtlas;
                        go.GetComponent<BagItem>().Init(_itemList[i].itemCode, _itemList[i].itemGrade, _itemList[i].itemCount, _itemList[i].equipID, i);
                        go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("ScrollView").GetComponent<UIScrollView>();
                        ListBag.Add(go);
                    }
                    //break;
                }
            }
        }
        return count;
    }

    void initItemInfo()
    {
        ItemName.text = "????";
        ItemCount.text = "[93d8f3]数量:[fa9222]?";
        ItemDes.text = "";

        SetColor(ItemIcon, 1);
        ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
        ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = "0";
        //ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().MakePixelPerfect();
        SuiPian.SetActive(false);

    }

    public void SetBagClick(int itemID)
    {
        if (itemID == 0)
            return;
        curItemID = itemID;
        TextTranslator.instance.ItemDescription(ItemIcon.gameObject, itemID, 0);
        Item mItem = TextTranslator.instance.GetItemByID(itemID);
        if (mItem == null)
        {
            mItem = new Item(itemID, 0);
        }
        if (SelectedItem != null)
        {
            SelectedItem.transform.FindChild("yellowLine").gameObject.SetActive(true);
        }
        TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(itemID);
        ItemName.text = mItem.itemName;

        if (mItemInfo.sellType == 1)
        {
            ItemSellNumber.transform.parent.GetComponent<UISprite>().spriteName = "icon1";
            ItemSellNumber.text = mItemInfo.sellPrice.ToString();
        }
        else if (mItemInfo.sellType == 2)
        {
            ItemSellNumber.transform.parent.GetComponent<UISprite>().spriteName = "icon4";
            ItemSellNumber.text = mItemInfo.sellPrice.ToString();
        }
        else
        {
            ItemSellNumber.transform.parent.GetComponent<UISprite>().spriteName = "";
            ItemSellNumber.text = "";
        }
        ItemDes.text = TextTranslator.instance.GetItemDescriptionByItemCode(itemID);
        if (mItem.itemCode.ToString()[0] == '5')
        {
            ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = mItem.itemCode.ToString();
        }
        else if (mItem.itemCode.ToString()[0] == '7')
        {
            if (mItem.itemCode != 70000 && mItem.itemCode != 79999)
            {
                ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
                ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = (mItem.itemCode - 10000).ToString();
            }
            else
            {
                ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
                ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = (mItem.itemCode).ToString();
                SuiPian.SetActive(false);
            }
        }
        else if (mItem.itemCode.ToString()[0] == '4')
        {
            ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = (mItem.itemCode).ToString();
            if (mItem.itemCode.ToString()[1] == '2')
            {
                SuiPian.SetActive(true);
            }
            else
            {
                SuiPian.SetActive(false);
            }
        }
        else if (mItem.itemCode.ToString()[0] == '8')
        {
            ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = mItemInfo.picID.ToString();

        }
        else if (mItem.itemCode.ToString()[0] == '2')
        {
            ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = mItemInfo.picID.ToString();
        }
        else
        {
            ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = mItem.itemCode.ToString();
        }

        SetColor(ItemIcon, mItem.itemGrade);
        TextTranslator.ItemInfo _info = TextTranslator.instance.GetItemByItemCode(mItem.itemCode);
        if (_info.FuncType == 16 || _info.FuncType == 17)
        {
            SellMoney.SetActive(false);
            SellButton.SetActive(false);
            if (TextTranslator.instance.GetItemCountByID(curItemID) > 1 && _info.Stact != 1)
            {
                BigUseButton.SetActive(true);
                UseButton.SetActive(false);
            }
            else
            {
                UseButton.SetActive(true);
                BigUseButton.SetActive(false);
            }
            CombineButton.SetActive(false);
            SuiPian.SetActive(false);
            GetOnButton.SetActive(false);
        }
        else if ((mItem.itemCode >= 10001 && mItem.itemCode <= 10006) || mItemInfo.FuncType == 4 || mItemInfo.FuncType == 5)
        {
            SellMoney.SetActive(false);
            SellButton.SetActive(false);
            UseButton.SetActive(true);
            BigUseButton.SetActive(false);
            CombineButton.SetActive(false);
            SuiPian.SetActive(false);
            GetOnButton.SetActive(false);
        }
        else if(_info.FuncType ==90)
        {
            SellMoney.SetActive(false);
            SellButton.SetActive(false);
            UseButton.SetActive(true);
            BigUseButton.SetActive(false);
            CombineButton.SetActive(false);
            SuiPian.SetActive(false);
            GetOnButton.SetActive(false);
        }
        else if (mItem.itemCode.ToString()[0] == '7' || mItem.itemCode.ToString()[0] == '8' || _info.FuncType == 0)//yy
        {
            //CombineButton.transform.FindChild("Label").GetComponent<UILabel>().text = "合 成";
            SellMoney.SetActive(false);
            SellButton.SetActive(false);
            UseButton.SetActive(false);
            BigUseButton.SetActive(false);
            CombineButton.SetActive(true);
            SuiPian.SetActive(true);
            if (mItem.itemCode.ToString()[0] == '8')
            {
                SetSuiPianIcon(mItem.itemCode.ToString()[4]);
            }
            else
            {
                SuiPian.GetComponent<UISprite>().spriteName = "grab_ui4_icon5";
            }
            GetOnButton.SetActive(false);
            if (mItem.itemCode / 1000 == 42)
            {

            }
            else if (_info.FuncType == 0)
            {
                SuiPian.SetActive(false);
            }

        }
        else if (_info.FuncType == 8)
        {
            //CombineButton.transform.FindChild("Label").GetComponent<UILabel>().text = "合 成";
            SellMoney.SetActive(false);
            SellButton.SetActive(false);
            UseButton.SetActive(false);
            BigUseButton.SetActive(false);
            CombineButton.SetActive(false);
            SuiPian.SetActive(false);
            GetOnButton.SetActive(true);
        }
        else if (_info.sellType != 1 && _info.sellType != 2)
        {
            SellMoney.SetActive(false);
            SellButton.SetActive(false);
            UseButton.SetActive(false);
            BigUseButton.SetActive(false);
            CombineButton.SetActive(true);
            //SuiPian.SetActive(true);
            GetOnButton.SetActive(false);
            if (_info.FuncType == 19)
            {
                SuiPian.SetActive(false);
            }
        }
        else
        {
            //CombineButton.transform.FindChild("Label").GetComponent<UILabel>().text = "售 出";
            SellMoney.SetActive(true);
            SellButton.SetActive(true);
            CombineButton.SetActive(false);
            BigUseButton.SetActive(false);
            UseButton.SetActive(false);
            GetOnButton.SetActive(false);
        }
        if (mItem.itemCode.ToString().Substring(0, 1) == "7")
        {
            //万能碎片特殊处理 || 材料
            if (mItem.itemCode == 79999 || mItem.itemCode == 70000 || _info.FuncType == 0)
            {
                ItemCount.text = string.Format("{0}数量:{1}{2}", "[93d8f3]", "[fa9222]", mItem.itemCount);
                //CurItemNeedPic = mhero.heroPiece;
                CombineButton.transform.FindChild("Label").GetComponent<UILabel>().text = "获 取";
            }
            else
            {
                HeroInfo mhero = TextTranslator.instance.GetHeroInfoByHeroID(mItem.itemCode - 10000);
                //RoleRankNeedInfo mRoleRankNeed = TextTranslator.instance.GetRoleRankNeedByRankLevel(mhero.heroRarity);
                Debug.Log("万能碎片测试中：" + mItem.itemCount + "----------" + mhero.heroPiece);
                ItemCount.text = string.Format("{0}数量:{1}{2}/{3} ", "[93d8f3]", "[fa9222]", mItem.itemCount, mhero.heroPiece);
                CurItemNeedPic = mhero.heroPiece;
                //SellMoney.SetActive(false);
                //SellButton.SetActive(false);
                //CombineButton.SetActive(true);
                //SuiPian.SetActive(true);
                if (mItem.itemCount < mhero.heroPiece)//yy
                {
                    CombineButton.transform.FindChild("Label").GetComponent<UILabel>().text = "获 取";
                }
                else
                {
                    CombineButton.transform.FindChild("Label").GetComponent<UILabel>().text = "合 成";
                    foreach (Hero h in CharacterRecorder.instance.ownedHeroList)
                    {
                        if ((h.cardID + 10000) == mItem.itemCode)
                        {
                            CombineButton.transform.FindChild("Label").GetComponent<UILabel>().text = "获 取";
                        }
                    }

                }
            }
        }
        else if (mItem.itemCode.ToString().Substring(0, 1) == "8")
        {
            //ItemCount.text = string.Format("数量:{0}/{1} ", mItem.itemCount, mItemInfo.ComposeNumber);

            CurItemNeedPic = mItemInfo.ComposeNumber;
            //   GameObject.Find("InfoButton").transform.FindChild("Label").GetComponent<UILabel>().text = "合 成";
            SuiPian.SetActive(true);
            if (mItem.itemCode.ToString().Substring(0, 1) == "8")
            {
                CombineButton.transform.FindChild("Label").GetComponent<UILabel>().text = "获 取";
                ItemCount.text = string.Format("{0}数量:{1}{2} ", "[93d8f3]", "[fa9222]", mItem.itemCount);
                CurItemNeedPic = 0;
            }
            else
            {
                ItemCount.text = string.Format("{0}数量:{1}{2}/{3} ", "[93d8f3]", "[fa9222]", mItem.itemCount, mItemInfo.ComposeNumber);
            }
        }
        else if (_info.FuncType == 8)
        {
            if (mItem.itemCode == 59000 || mItem.itemCode == 59010)
            {
                GetOnButton.transform.FindChild("Label").GetComponent<UILabel>().text = "强化";
            }
            else
            {
                GetOnButton.transform.FindChild("Label").GetComponent<UILabel>().text = "装备";
            }
            ItemCount.text = string.Format("{0}数量:{1}{2}", "[93d8f3]", "[fa9222]", mItem.itemCount);
        }
        else if (_info.FuncType == 0 || (_info.sellType != 1 && _info.sellType != 2))
        {
            ItemCount.text = string.Format("{0}数量:{1}{2}", "[93d8f3]", "[fa9222]", mItem.itemCount);
            CurItemNeedPic = 0;
            CombineButton.transform.FindChild("Label").GetComponent<UILabel>().text = "获 取";
        }
        else
        {
            // ItemCount.text = string.Format("数量:{0}", mItem.itemCount);
            //int _itemCount = TextTranslator.instance.GetItemCountByID(mItem.itemCode);
            ItemCount.text = string.Format("{0}数量:{1}{2}", "[93d8f3]", "[fa9222]", mItem.itemCount);
            CurItemNeedPic = mItem.itemCount;
            //    GameObject.Find("InfoButton").transform.FindChild("Label").GetComponent<UILabel>().text = "详 情";
            SuiPian.SetActive(false);
        }
        //if(mItem.itemCount <= 0)
        //{
        //    Debug.Log("111----ca" + _next_item_id);
        //    SetBagClick(_next_item_id);
        //}
    }
    public void SetSuiPianIcon(char charIcon)
    {
        switch (charIcon)
        {
            case '0':
                //SuiPian.GetComponent<UISprite>().spriteName = "0";
                SuiPian.GetComponent<UISprite>().spriteName = "grab_ui4_icon2";
                break;
            case '1':
                SuiPian.GetComponent<UISprite>().spriteName = "grab_ui4_icon3";
                break;
            case '2':
                SuiPian.GetComponent<UISprite>().spriteName = "grab_ui4_icon4";
                break;
            case '3':
                SuiPian.GetComponent<UISprite>().spriteName = "grab_ui4_icon5";
                break;
            case '4':
                SuiPian.GetComponent<UISprite>().spriteName = "grab_ui4_icon1";
                break;
            case '5':
                SuiPian.GetComponent<UISprite>().spriteName = "grab_ui4_icon6";
                break;
            default:
                SuiPian.GetComponent<UISprite>().spriteName = "grab_ui4_icon5";
                break;
        }
    }
    public void CountNextItemIndex()
    {
        for (int i = 0; i < ListBag.Count; i++)
        {
            if (ListBag[i].name == SelectedItem.name)
            {
                _next_item_index = i;
                break;
            }
        }
    }
    void dsf()
    {
        StartCoroutine(One());
    }
    IEnumerator One()
    {
        yield return new WaitForSeconds(0.3f);
        UIManager.instance.OpenPromptWindow(string.Format("您将会由[FF0000]{0}[-]变为[08ff00]{1}[-]，是否使用？", CharacterRecorder.instance.legionCountryID==1?"同盟": "帝国", CharacterRecorder.instance.legionCountryID == 1 ? "帝国" : "同盟"), PromptWindow.PromptType.Traitor, Two, null); ;//无间道

    }
    void Two()
    {
        NetworkHandler.instance.SendProcess("8105#;");
    }
    public void SetCardInfo(int _RoleID)
    {
        UIManager.instance.OpenSinglePanel("CardWindow", false);
        GameObject.Find("CardWindow").GetComponent<CardWindow>().SetCardInfo(_RoleID);
    }

    // Use this for initialization
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.背包);
        UIManager.instance.UpdateSystems(UIManager.Systems.背包);

        if (UIEventListener.Get(GameObject.Find("Tab1Button")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("Tab1Button")).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.BagIndex == 1)
                {
                    return;
                }
                MyGrid.transform.parent.GetComponent<UIPanel>().clipOffset = Vector2.zero;
                MyGrid.transform.parent.localPosition = scrollViewInitPos;
                SetTab(1);
            };
        }

        if (UIEventListener.Get(GameObject.Find("Tab2Button")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("Tab2Button")).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.BagIndex == 2)
                {
                    return;
                }
                MyGrid.transform.parent.GetComponent<UIPanel>().clipOffset = Vector2.zero;
                MyGrid.transform.parent.localPosition = scrollViewInitPos;
                SetTab(2);
            };
        }

        if (UIEventListener.Get(GameObject.Find("Tab3Button")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("Tab3Button")).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.BagIndex == 3)
                {
                    return;
                }
                MyGrid.transform.parent.GetComponent<UIPanel>().clipOffset = Vector2.zero;
                MyGrid.transform.parent.localPosition = scrollViewInitPos;
                SetTab(3);
            };
        }

        if (UIEventListener.Get(GameObject.Find("Tab4Button")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("Tab4Button")).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.BagIndex == 4)
                {
                    return;
                }
                MyGrid.transform.parent.GetComponent<UIPanel>().clipOffset = Vector2.zero;
                MyGrid.transform.parent.localPosition = scrollViewInitPos;

                SetTab(4);
                if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 15 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 3)
                {
                    //UIManager.instance.NewGuideAnchor("151");
                    PlayerPrefs.SetInt(LuaDeliver.instance.GetGuideSubStateName(), 4);
                    LuaDeliver.instance.UseGuideStation();
                }
            };
        }

        if (UIEventListener.Get(GameObject.Find("Tab5Button")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("Tab5Button")).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.BagIndex == 5)
                {
                    return;
                }
                MyGrid.transform.parent.GetComponent<UIPanel>().clipOffset = Vector2.zero;
                MyGrid.transform.parent.localPosition = scrollViewInitPos;
                SetTab(5);
            };
        }
        if (UIEventListener.Get(GameObject.Find("Tab6Button")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("Tab6Button")).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.BagIndex == 6)
                {
                    return;
                }
                MyGrid.transform.parent.GetComponent<UIPanel>().clipOffset = Vector2.zero;
                MyGrid.transform.parent.localPosition = scrollViewInitPos;
                SetTab(6);
            };
        }
        /*   if (UIEventListener.Get(GameObject.Find("InfoButton")).onClick == null)
           {
               UIEventListener.Get(GameObject.Find("InfoButton")).onClick += delegate(GameObject go)
               {
                   if (curItemID.ToString().Substring(0, 1) == "5" || curItemID.ToString().Substring(0, 1) == "3" || curItemID < 40000)
                   {
                       DetailItemBoard.SetActive(true);
                       DetailItemBoard.GetComponent<BagDetailInfoBoard>().SetInfo(curItemID);
                   }
                   else if (curItemID.ToString().Substring(0, 1) == "7")
                   {
                       Debug.LogError("碎片碎片");
                       NetworkHandler.instance.SendProcess("1021#" + curItemID + ";");
                   }
                   else if (curItemID.ToString().Substring(0, 1) == "4")
                   {
                       NetworkHandler.instance.SendProcess("5008#" + curItemID + ";");
                   }
                   else if (curItemID.ToString().Substring(0, 1) == "8")
                   {
                       NetworkHandler.instance.SendProcess("5008#" + curItemID + ";");
                   }
               };
           }*/

        if (UIEventListener.Get(SellButton).onClick == null)
        {
            UIEventListener.Get(SellButton).onClick += delegate(GameObject go)
            {
                Item mItem = TextTranslator.instance.GetItemByID(curItemID);
                if (mItem.itemGrade >= 3)
                {
                    UIManager.instance.OpenPromptWindow("这是一个高品质物品，是否确认卖出", PromptWindow.PromptType.Confirm, OpenSellItemBoard, null);
                }
                else
                {
                    if (ItemCount.text == "1")
                    {
                        NetworkHandler.instance.SendProcess("5003#" + curItemID + ";1;0;");
                    }
                    else
                    {
                        OpenSellItemBoard();
                    }
                }
            };
        }

        //if (UIEventListener.Get(CombineButton).onClick == null)
        {
            UIEventListener.Get(CombineButton).onClick += delegate(GameObject go)
            {
                if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 15 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 4)
                {
                    SceneTransformer.instance.NewGuideButtonClick();
                }
                //NetworkHandler.instance.SendProcess("1021#" + curItemID + ";");
                if (CombineButton.transform.FindChild("Label").GetComponent<UILabel>().text == "获 取")
                {
                    // UIManager.instance.OpenPanel("WayWindow", false);
                    UIManager.instance.OpenSinglePanel("WayWindow", false);
                    WayWindow.NeedItemCount = CurItemNeedPic;
                    GameObject.Find("WayWindow").GetComponent<WayWindow>().SetWayInfo(curItemID);
                }
                else
                {
                    NetworkHandler.instance.SendProcess("1021#" + curItemID + ";");
                }
            };
        }
        if (UIEventListener.Get(UseButton).onClick == null)
        {
            UIEventListener.Get(UseButton).onClick += delegate(GameObject go)
            {
                if (curItemID == 20002 || curItemID == 20003 || curItemID == 20009 || curItemID == 20010 || curItemID == 20011)
                {
                    CharacterRecorder.instance.BagItemCode = curItemID;
                }
                if (curItemID==10107)
                {
                    if (CharacterRecorder.instance.legionCountryID==1|| CharacterRecorder.instance.legionCountryID==2)
                    {
                        UIManager.instance.OpenPromptWindow("使用此道具将会改变你的国家阵营，是否使用？", PromptWindow.PromptType.Confirm, dsf, null); ;//无间道
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("长官，您尚未加入任何阵营！", PromptWindow.PromptType.Hint, null, null);

                    }
                   
                }
               else if (curItemID >= 10001 && curItemID <= 10006)
                {
                    UIManager.instance.OpenPanel("RoleWindow", true);
                    GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(2);
                }
                else if (TextTranslator.instance.GetItemCountByID(curItemID) >= 1)
                {
                    CharacterRecorder.instance.BagItemCode = curItemID;
                    NetworkHandler.instance.SendProcess("5002#" + curItemID + ";1;");
                    //CountPacks(curItemID);
                }
                //else if (TextTranslator.instance.GetItemCountByID(curItemID) > 1)
                //{

                //}
                else
                {
                    UIManager.instance.OpenPromptWindow("长官，您没有该物品！", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        UIEventListener.Get(BigUseButton).onClick += delegate(GameObject go)
        {

            UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
            GameObject.Find("EmployPropsWindow").GetComponent<EmployPropsWindow>().SetPropsInfo(curItemID);
        };

        UIEventListener.Get(GetOnButton).onClick += delegate(GameObject go)
        {
            bool IsBook = false;
            int ItemCode = curItemID;
            int hintState = 0;  //1- 勋章装备 2- 手册装备 3- 经验勋章

            if (ItemCode / 10 % 2 == 1) //勋章
            {
                IsBook = false;
            }
            else //书
            {
                IsBook = true;
            }

            for (int i = 0; i < CharacterRecorder.instance.ownedHeroList.size; i++)
            {
                if (ItemCode == 59000 || ItemCode == 59010)
                {
                    EquipStrong _myEquipStrong = TextTranslator.instance.GetEquipStrongByID(ItemCode);
                    HeroInfo mHeroInfo = TextTranslator.instance.GetHeroInfoByHeroID(CharacterRecorder.instance.ownedHeroList[i].cardID);
                    CharacterRecorder.instance.setEquipTableIndex = 0;
                    UIManager.instance.OpenPanel("StrengEquipWindow", true);
                    GameObject _go = GameObject.Find("StrengEquipWindow");
                    _go.GetComponent<StrengEquipWindow>().ClickListEquip(_go.GetComponent<StrengEquipWindow>().ListEquip[4]);
                    Debug.LogError(CharacterRecorder.instance.ownedHeroList[i].equipList[4].equipCode + "HeroClick" + TextTranslator.instance.HeadIndex);
                    hintState = 3;
                    break;
                }
                else if (!IsBook)
                {
                    EquipStrong _myEquipStrong = TextTranslator.instance.GetEquipStrongByID(ItemCode);
                    HeroInfo mHeroInfo = TextTranslator.instance.GetHeroInfoByHeroID(CharacterRecorder.instance.ownedHeroList[i].cardID);
                    if (ItemCode > CharacterRecorder.instance.ownedHeroList[i].equipList[4].equipCode && _myEquipStrong.Race == mHeroInfo.heroRace)
                    {
                        TextTranslator.instance.HeadIndex = i;
                        StrengEquipWindow.IsEnterEquipUIFromGrabGoods = true;
                        CharacterRecorder.instance.isNeedRecordStrengTabType = false;
                        UIManager.instance.OpenPanel("StrengEquipWindow", true);
                        GameObject.Find("StrengEquipWindow").GetComponent<StrengEquipWindow>().EnterEquipAwakeUIFromGrabGoods(4 + 1);
                        Debug.LogError(CharacterRecorder.instance.ownedHeroList[i].equipList[4].equipCode + "HeroClick" + TextTranslator.instance.HeadIndex);
                        hintState = 1;
                        break;
                    }
                }
                else
                {
                    EquipStrong _myEquipStrong = TextTranslator.instance.GetEquipStrongByID(ItemCode);
                    HeroInfo mHeroInfo = TextTranslator.instance.GetHeroInfoByHeroID(CharacterRecorder.instance.ownedHeroList[i].cardID);
                    if (ItemCode > CharacterRecorder.instance.ownedHeroList[i].equipList[5].equipCode && _myEquipStrong.Race == mHeroInfo.heroRace)
                    {
                        TextTranslator.instance.HeadIndex = i;
                        StrengEquipWindow.IsEnterEquipUIFromGrabGoods = true;
                        CharacterRecorder.instance.isNeedRecordStrengTabType = false;
                        UIManager.instance.OpenPanel("StrengEquipWindow", true);
                        GameObject.Find("StrengEquipWindow").GetComponent<StrengEquipWindow>().EnterEquipAwakeUIFromGrabGoods(5 + 1);
                        Debug.LogError(CharacterRecorder.instance.ownedHeroList[i].equipList[5].equipCode + "HeroClick" + TextTranslator.instance.HeadIndex);
                        hintState = 2;
                        break;
                    }
                }
            }

            if (hintState == 0)
            {
                if (IsBook)
                {
                    UIManager.instance.OpenPromptWindow("没有角色可以装备该手册", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("没有角色可以装备该勋章", PromptWindow.PromptType.Hint, null, null);
                }
            }
        };

        if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 10)
        {
            SellMoney.SetActive(false);
            SellButton.SetActive(false);
            CombineButton.SetActive(true);
        }
        else
        {
            CombineButton.SetActive(false);
        }
        if (CharacterRecorder.instance.BagIndex != 1)
        {
            SetTab(CharacterRecorder.instance.BagIndex);
        }
        else
        {
            SetTab(1);
        }

    }

    void OpenSellItemBoard()
    {
        SellItemBoard.SetActive(true);
        SellItemBoard.GetComponent<BagSellItemBoard>().SetInfo(curItemID, int.Parse(SellMoney.transform.Find("Number").GetComponent<UILabel>().text));
    }

    void SetColor(GameObject go, int color)
    {
        switch (color)
        {
            case 1:
                go.GetComponent<UISprite>().spriteName = "Grade1";
                break;
            case 2:
                go.GetComponent<UISprite>().spriteName = "Grade2";
                break;
            case 3:
                go.GetComponent<UISprite>().spriteName = "Grade3";
                break;
            case 4:
                go.GetComponent<UISprite>().spriteName = "Grade4";
                break;
            case 5:
                go.GetComponent<UISprite>().spriteName = "Grade5";
                break;
            case 6:
                go.GetComponent<UISprite>().spriteName = "Grade6";
                break;
            case 7:
                go.GetComponent<UISprite>().spriteName = "Grade7";
                break;
        }
    }

    void CountPacks(int _itemId)
    {
        TextTranslator.ItemInfo _info = TextTranslator.instance.GetItemByItemCode(_itemId);
        if (_info.FuncType == 16)
        {
            string[] dataSplit = _info.ToValue.Split('!');
            string[] valueSplit;
            for (int i = 0; i < dataSplit.Length - 1; i++)
            {
                valueSplit = dataSplit[i].Split('$');
                switch (valueSplit[0])
                {
                    case "90001":
                        CharacterRecorder.instance.AddMoney(int.Parse(valueSplit[1]));
                        break;
                    case "90002":
                        CharacterRecorder.instance.AddLunaGem(int.Parse(valueSplit[1]));
                        break;
                }
            }
            this.transform.Find("TopContent").GetComponent<TopContent>().Reset();
        }
    }
    IEnumerator wait()
    {
        yield return new WaitForSeconds(0.1f);
        ScrollViewlist.GetComponent<ScrollViewControl>().enabled = true;
    }
}
