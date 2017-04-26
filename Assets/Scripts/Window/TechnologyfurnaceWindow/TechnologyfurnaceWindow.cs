using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class FurnaceItemInfo
{
    public int ItemID;
    public int ItemCount;
    public int SellGoldNum;         //售卖单价
    //public int SellAllGoldNum;      //售卖总价
    public int ColorNum;            //颜色
    public int type;                //1 勋章，2 核武器 3 英雄

    public bool IsSell = false;    //是否售卖 true为兑换，列表item不显示

    public FurnaceItemInfo(int ItemID,int ItemCount,int SellGoldNum,int ColorNum,int type) 
    {
        this.ItemID = ItemID;
        this.ItemCount = ItemCount;
        this.SellGoldNum = SellGoldNum;
        //this.SellAllGoldNum = SellAllGoldNum;
        this.ColorNum = ColorNum;
        this.type = type;
    }

    public void SetFurnaceItemInfo(int ItemID, int ItemCount, int SellGoldNum, int ColorNum, int type)
    {
        this.ItemID = ItemID;
        this.ItemCount = ItemCount;
        this.SellGoldNum = SellGoldNum;
        //this.SellAllGoldNum = SellAllGoldNum;
        this.ColorNum = ColorNum;
        this.type = type;
    }
}

public class TechnologyfurnaceWindow : MonoBehaviour
{
    public GameObject Tab1Button;
    public GameObject Tab2Button;
    public GameObject Tab3Button;
    public GameObject Tab4Button;

    public GameObject leftGrid;
    public GameObject rightGrid;
    public GameObject BagItem;
    public GameObject rightBagItem;
    public GameObject TopItem;
    public UILabel OneGoldNumber;
    public UILabel AllGoldNumber;
    public UILabel HaveGoldNumber;
    public UILabel ItemName;

    public GameObject SellWindow;
    public GameObject HunterMessage;
    public GameObject propsIcon;
    public GameObject closeButton;
    public GameObject sureButtonyel;
    public GameObject cancleButton;
    public GameObject sureButtonBul;
    public GameObject shopButton;
    public GameObject QuestionButton;
    public GameObject QuestCloseButton;

    [SerializeField]
    private UIAtlas ItemAtlas;
    [SerializeField]
    private UIAtlas RoleAtlas;


    List<FurnaceItemInfo> leftXunZhanglist = new List<FurnaceItemInfo>();
    List<FurnaceItemInfo> leftHeWulist = new List<FurnaceItemInfo>();
    List<FurnaceItemInfo> leftYinXionglist = new List<FurnaceItemInfo>();

    List<FurnaceItemInfo> rightItemlist = new List<FurnaceItemInfo>();

    //List<GameObject> rightItemObjlist = new List<GameObject>();
    private int Index = 0;

	void Start () {
        FirstInitEveryItem();
        UIEventListener.Get(Tab1Button).onClick = delegate(GameObject go)
        {
            Index = 0;
            clickIndexOneButton();
            leftGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        };

        UIEventListener.Get(Tab2Button).onClick = delegate(GameObject go)
        {
            Index = 1;
            clickIndexOneButton();
            leftGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        };

        UIEventListener.Get(Tab3Button).onClick = delegate(GameObject go)
        {
            Index = 2;
            clickIndexOneButton();
            leftGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        };

        UIEventListener.Get(Tab4Button).onClick = delegate(GameObject go)
        {
            Index = 3;
            clickIndexOneButton();
            leftGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        };

        UIEventListener.Get(closeButton).onClick = delegate(GameObject go)
        {
            SellWindow.SetActive(false);
        };

        UIEventListener.Get(sureButtonyel).onClick = delegate(GameObject go)
        {
            SellWindow.SetActive(false);
            ItemExchange();
        };
        UIEventListener.Get(cancleButton).onClick = delegate(GameObject go)
        {
            SellWindow.SetActive(false);
        };

        UIEventListener.Get(sureButtonBul).onClick = delegate(GameObject go)
        {
            clickSureButton();
        };

        UIEventListener.Get(shopButton).onClick = delegate(GameObject go)
        {
            CharacterRecorder.instance.RankShopType = 10006;
            UIManager.instance.OpenPanel("RankShopWindow", true);

        };

        UIEventListener.Get(QuestionButton).onClick = delegate(GameObject go)
        {
            HunterMessage.SetActive(true);
        };

        UIEventListener.Get(QuestCloseButton).onClick = delegate(GameObject go)
        {
            HunterMessage.SetActive(false);
        };

        HaveGoldNumber.text = CharacterRecorder.instance.GoldBar.ToString();
	}

    private int GetItemTypeByItemCode(int itemCode) //1勋章 2 核武器 3 英雄
    {
        int itemType = 0;

        if (itemCode.ToString()[0] == '8' && itemCode.ToString()[1] != '5')
        {
            itemType = 1;
        }
        else if (itemCode/1000==85)
        {
            itemType = 2;
        }
        else if (itemCode > 70000 && itemCode<79999)
        {
            itemType = 3;
        }
        return itemType;
    }

    private void FirstInitEveryItem() //初始化背包列表
    {
        BetterList<Item> _itemList = TextTranslator.instance.bagItemList;  //背包物品

        for (int i = TextTranslator.instance.itemSortList.Count-1; i>=0;i--)  //排序表
        {
            for (int j = 0; j < _itemList.size; j++)
            {
                if (TextTranslator.instance.itemSortList[i].ItemID == _itemList[j].itemCode && _itemList[j].itemCount>0)//5005刷新没有消除为0的物品 
                {
                    TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(_itemList[j].itemCode); //item表

                    int itemType = GetItemTypeByItemCode(_itemList[j].itemCode);
                    if (itemType == 1) 
                    {
                        FurnaceItemInfo _furnaceItemInfo = new FurnaceItemInfo(_itemList[j].itemCode, _itemList[j].itemCount, mitemInfo.ExchangeGold, mitemInfo.itemGrade, itemType);
                        leftXunZhanglist.Add(_furnaceItemInfo);
                    }
                    else if(itemType==2)
                    {
                        FurnaceItemInfo _furnaceItemInfo = new FurnaceItemInfo(_itemList[j].itemCode, _itemList[j].itemCount, mitemInfo.ExchangeGold, mitemInfo.itemGrade, itemType);
                        leftHeWulist.Add(_furnaceItemInfo);
                    }
                    else if (itemType == 3)
                    {
                        FurnaceItemInfo _furnaceItemInfo = new FurnaceItemInfo(_itemList[j].itemCode, _itemList[j].itemCount, mitemInfo.ExchangeGold, mitemInfo.itemGrade, itemType);
                        leftYinXionglist.Add(_furnaceItemInfo);
                    }
                }
            }
        }

        //int count = leftXunZhanglist.Count + leftHeWulist.Count + leftYinXionglist.Count;

        //for (int i = 0; i < count; i++) 
        //{
        //    GameObject go = NGUITools.AddChild(rightGrid, rightBagItem);
        //    FurnaceItemInfo _furnaceItemInfo = new FurnaceItemInfo(0, 0, 0, 0, 0);
        //    rightItemlist.Add(_furnaceItemInfo);
        //    rightItemObjlist.Add(go);
        //}
        //rightGrid.GetComponent<UIGrid>().Reposition();
        clickIndexOneButton();
    }

    private void clickIndexOneButton() //点击单个按钮
    {
        for (int i = leftGrid.transform.childCount - 1; i >= 0; i--) 
        {
            DestroyImmediate(leftGrid.transform.GetChild(i).gameObject);
        }

        if (Index == 0) 
        {
            for (int i = 0; i < leftXunZhanglist.Count; i++) //勋章
            {
                if (leftXunZhanglist[i].IsSell == false) 
                {
                    GameObject go = NGUITools.AddChild(leftGrid, BagItem);
                    SetBagItemInfo(leftXunZhanglist[i], go);
                    ClickOneItemButton(leftXunZhanglist[i], go);
                }               
            }

            for (int i = 0; i < leftHeWulist.Count; i++) //核武
            {
                if (leftHeWulist[i].IsSell == false)
                {
                    GameObject go = NGUITools.AddChild(leftGrid, BagItem);
                    SetBagItemInfo(leftHeWulist[i], go);
                    ClickOneItemButton(leftHeWulist[i], go);
                }
            }

            for (int i = 0; i < leftYinXionglist.Count; i++) //英雄
            {
                if (leftYinXionglist[i].IsSell == false)
                {
                    GameObject go = NGUITools.AddChild(leftGrid, BagItem);
                    SetBagItemInfo(leftYinXionglist[i], go);
                    ClickOneItemButton(leftYinXionglist[i], go);
                }
            }
        }
        else if (Index == 1) 
        {
            for (int i = 0; i < leftXunZhanglist.Count; i++) //勋章
            {
                if (leftXunZhanglist[i].IsSell == false)
                {
                    GameObject go = NGUITools.AddChild(leftGrid, BagItem);
                    SetBagItemInfo(leftXunZhanglist[i], go);
                    ClickOneItemButton(leftXunZhanglist[i], go);
                }
            }
        }
        else if (Index == 2)
        {
            for (int i = 0; i < leftHeWulist.Count; i++) //核武
            {
                if (leftHeWulist[i].IsSell == false)
                {
                    GameObject go = NGUITools.AddChild(leftGrid, BagItem);
                    SetBagItemInfo(leftHeWulist[i], go);
                    ClickOneItemButton(leftHeWulist[i], go);
                }
            }
        }
        else if (Index == 3)
        {
            for (int i = 0; i < leftYinXionglist.Count; i++) //英雄
            {
                if (leftYinXionglist[i].IsSell == false)
                {
                    GameObject go = NGUITools.AddChild(leftGrid, BagItem);
                    SetBagItemInfo(leftYinXionglist[i], go);
                    ClickOneItemButton(leftYinXionglist[i], go);
                }
            }
        }
        //leftGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        leftGrid.GetComponent<UIGrid>().Reposition();
    }

    private void ClickOneItemButton(FurnaceItemInfo _FurnaceItemInfo,GameObject item) //点击单个item,添加到兑换数组中
    {
        UIEventListener.Get(item).onClick = delegate(GameObject go)
        {
            //for (int i = 0; i < rightItemlist.Count; i++) 
            //{
            //    if (rightItemlist[i].IsSell == false) 
            //    {
            //        rightItemlist[i].SetFurnaceItemInfo(_FurnaceItemInfo.ItemID,_FurnaceItemInfo.ItemCount,_FurnaceItemInfo.SellGoldNum,_FurnaceItemInfo.ColorNum,_FurnaceItemInfo.type);
            //        rightItemlist[i].IsSell = true;
            //        _FurnaceItemInfo.IsSell = true;
            //        RushRightItemInfo();
            //        ClickOneRightButton(i);
            //        clickIndexOneButton();
            //        OneGoldNumber.text = (rightItemlist[i].SellGoldNum * rightItemlist[i].ItemCount).ToString();
            //        SetBagItemInfo(rightItemlist[i], TopItem);
            //        break;
            //    }
            //}

            FurnaceItemInfo _furnaceItemInfo = new FurnaceItemInfo(_FurnaceItemInfo.ItemID, _FurnaceItemInfo.ItemCount, _FurnaceItemInfo.SellGoldNum, _FurnaceItemInfo.ColorNum, _FurnaceItemInfo.type);
            _furnaceItemInfo.IsSell = true;
            _FurnaceItemInfo.IsSell = true;
            rightItemlist.Add(_furnaceItemInfo);
            clickIndexOneButton();
            RushRightItemInfo();
            ClickOneRightButton(_furnaceItemInfo);
            SetBagItemInfo(_furnaceItemInfo, TopItem);
            SetTopItemInfo(_furnaceItemInfo, TopItem);
        };
    }

    private void RushRightItemInfo() //刷新右边
    {
        int allgoldNum = 0;

        for (int i = rightGrid.transform.childCount - 1; i >= 0; i--) 
        {
            DestroyImmediate(rightGrid.transform.GetChild(i).gameObject);
        }
        rightGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        for (int i = rightItemlist.Count - 1; i >= 0; i--) 
        {
            GameObject go = NGUITools.AddChild(rightGrid, rightBagItem);

            SetBagItemInfo(rightItemlist[i], go);

            //rightItemObjlist[i].transform.Find("BagItem").gameObject.SetActive(true);
            //TextTranslator.instance.ItemDescription(go, rightItemlist[i].ItemID, 0);

            int num = i;
            UIEventListener.Get(go).onClick = delegate(GameObject ob)
            {
                ClickOneRightButton(rightItemlist[num]);
                //OneGoldNumber.text = (rightItemlist[num].SellGoldNum * rightItemlist[num].ItemCount).ToString();
                SetBagItemInfo(rightItemlist[num], TopItem);
                SetTopItemInfo(rightItemlist[num], TopItem);
            };

            UIEventListener.Get(go.transform.Find("CutButton").gameObject).onClick = delegate(GameObject ob)
            {
                Debug.Log("cutdownid " + rightItemlist[num].type + "  " + rightItemlist[num].ItemID);
                RushDestoryOneItem(rightItemlist[num].type, rightItemlist[num].ItemID);
                rightItemlist[num].IsSell = false;
                rightItemlist.RemoveAt(num);
                RushRightItemInfo();
            };

            allgoldNum += rightItemlist[i].SellGoldNum * rightItemlist[i].ItemCount;
        }
        AllGoldNumber.text = allgoldNum.ToString();
        rightGrid.GetComponent<UIGrid>().Reposition();

        if (rightItemlist.Count == 0)
        {
            OneGoldNumber.text = "0";
            TopItem.transform.Find("SpriteItem").gameObject.SetActive(false);
            TopItem.transform.Find("SpritePiece").gameObject.SetActive(false);
            TopItem.transform.Find("LabelCount").gameObject.SetActive(false);
            TopItem.GetComponent<UISprite>().spriteName = "Grade1";
            ItemName.text = "物品名字";
            TopItem.GetComponent<BoxCollider>().enabled = false;
        }

        #region before
        //for (int i = 0; i < rightItemlist.Count; i++) 
        //{
        //    if (rightItemlist[i].IsSell)
        //    {
        //        rightItemObjlist[i].transform.Find("BagItem").gameObject.SetActive(true);

        //        SetBagItemInfoTwo(rightItemlist[i], rightItemObjlist[i].transform.Find("BagItem").gameObject);
        //        TextTranslator.instance.ItemDescription(rightItemObjlist[i].transform.Find("BagItem").gameObject,rightItemlist[i].ItemID, 0);

        //        int num = i;
        //        UIEventListener.Get(rightItemObjlist[i].transform.Find("BagItem").gameObject).onClick = delegate(GameObject go)
        //        {
        //            Debug.LogError("iiii =" + i);
        //            ClickOneRightButton(num);
        //            OneGoldNumber.text = (rightItemlist[num].SellGoldNum * rightItemlist[num].ItemCount).ToString();
        //            SetBagItemInfo(rightItemlist[num], TopItem);
        //        };

        //        UIEventListener.Get(rightItemObjlist[i].transform.Find("BagItem/CutButton").gameObject).onClick = delegate(GameObject go)
        //        {
        //            RushDestoryOneItem(rightItemlist[num].type, rightItemlist[num].ItemID);
        //            rightItemlist[num].IsSell = false;
        //            RushRightItemInfo();
        //        };

        //        allgoldNum += rightItemlist[i].SellGoldNum * rightItemlist[i].ItemCount;
        //    }
        //    else 
        //    {
        //        rightItemObjlist[i].transform.Find("BagItem").gameObject.SetActive(false);
        //    }
        //}
        //AllGoldNumber.text = allgoldNum.ToString();
        #endregion
    }


    //private void ClickOneRightButton(int num) //点击右边单个item
    private void ClickOneRightButton(FurnaceItemInfo _furnaceItemInfo)
    {
        //for (int i = 0; i < rightItemObjlist.Count; i++) 
        //{
        //    rightItemObjlist[i].transform.Find("BagItem/yellowLine").gameObject.SetActive(false);
        //}
        //rightItemObjlist[num].transform.Find("BagItem/yellowLine").gameObject.SetActive(true);

        if (rightGrid.transform.childCount > 0) 
        {
            for (int i = 0; i < rightGrid.transform.childCount; i++) 
            {
                if (rightGrid.transform.GetChild(i).name == _furnaceItemInfo.ItemID.ToString())
                {
                    rightGrid.transform.GetChild(i).transform.Find("yellowLine").gameObject.SetActive(true);
                }
                else 
                {
                    rightGrid.transform.GetChild(i).transform.Find("yellowLine").gameObject.SetActive(false);
                }
                
            }
        }
    }


    public void RushDestoryOneItem(int _type,int itemId)  //去掉单个物品
    {
        switch (_type) 
        {
            case 1:
                for (int i = leftXunZhanglist.Count - 1; i >=0; i--) 
                {
                    if (leftXunZhanglist[i].ItemID == itemId) 
                    {
                        leftXunZhanglist[i].IsSell = false;
                        break;
                    }
                }
                break;
            case 2:
                for (int i = leftHeWulist.Count - 1; i >=0; i--)
                {
                    if (leftHeWulist[i].ItemID == itemId)
                    {
                        leftHeWulist[i].IsSell = false;
                        break;
                    }
                }
                break;
            case 3:
                for (int i = leftYinXionglist.Count - 1; i >=0; i--)
                {
                    if (leftYinXionglist[i].ItemID == itemId)
                    {
                        leftYinXionglist[i].IsSell = false;
                        break;
                    }
                }
                break;
        }
        clickIndexOneButton();
    }

    private void clickSureButton() //确定按钮
    {
        if (rightItemlist.Count > 0)
        {
            bool Iscolor = false;
            for (int i = 0; i < rightItemlist.Count; i++)
            {
                if (rightItemlist[i].ColorNum == 6)
                {
                    Iscolor = true;
                    break;
                }
            }

            if (Iscolor)
            {
                SellWindow.SetActive(true);
                propsIcon.transform.Find("Number").GetComponent<UILabel>().text = AllGoldNumber.text;
            }
            else
            {
                ItemExchange();
            }
        }
        else 
        {
            UIManager.instance.OpenPromptWindow("没有兑换物品", PromptWindow.PromptType.Hint, null, null);
        }
    }

    private void ItemExchange() //5401
    {
        string itemstr = "";
        for (int i = 0; i < rightItemlist.Count; i++) 
        {
            itemstr += rightItemlist[i].ItemID + "$" + rightItemlist[i].ItemCount + "!";
        }

        if (itemstr != "") 
        {
            NetworkHandler.instance.SendProcess("5401#" + itemstr + ";");
        }
    }


    public void GetResultItemExchange() 
    {
        leftXunZhanglist.Clear();
        leftHeWulist.Clear();
        leftYinXionglist.Clear();
        rightItemlist.Clear();

        FirstInitEveryItem();//初始化
        RushRightItemInfo();

        HaveGoldNumber.text = CharacterRecorder.instance.GoldBar.ToString();
    }
    private void SetBagItemInfo(FurnaceItemInfo _FurnaceItemInfo, GameObject item) //单个物品信息
    {
        UISprite spriteIcon = item.transform.Find("SpriteItem").GetComponent<UISprite>();
        GameObject suiPian = item.transform.Find("SpritePiece").gameObject;
        item.GetComponent<UISprite>().spriteName = "Grade" + _FurnaceItemInfo.ColorNum.ToString();
        item.transform.Find("LabelCount").GetComponent<UILabel>().text = _FurnaceItemInfo.ItemCount.ToString();
        item.name = _FurnaceItemInfo.ItemID.ToString();
        if (_FurnaceItemInfo.ItemID.ToString()[0] == '7' && _FurnaceItemInfo.ItemID > 70000 && _FurnaceItemInfo.ItemID != 79999)
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = (_FurnaceItemInfo.ItemID - 10000).ToString();
            suiPian.SetActive(true);
        }
        else if (_FurnaceItemInfo.ItemID.ToString()[0] == '8')
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_FurnaceItemInfo.ItemID);
            spriteIcon.spriteName = mItemInfo.picID.ToString();
            suiPian.SetActive(true);
        }
        else
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = _FurnaceItemInfo.ItemID.ToString();
            suiPian.SetActive(false);
        }
    }

    private void SetTopItemInfo(FurnaceItemInfo _FurnaceItemInfo, GameObject item) 
    {
        if (rightItemlist.Count > 0)
        {
            item.transform.Find("SpriteItem").gameObject.SetActive(true);
            item.transform.Find("SpritePiece").gameObject.SetActive(true);
            item.transform.Find("LabelCount").gameObject.SetActive(true);
            item.GetComponent<UISprite>().spriteName = "Grade" + _FurnaceItemInfo.ColorNum.ToString();
            OneGoldNumber.text = (_FurnaceItemInfo.SellGoldNum * _FurnaceItemInfo.ItemCount).ToString();
            ItemName.text = TextTranslator.instance.GetItemNameByItemCode(_FurnaceItemInfo.ItemID);
            TextTranslator.instance.ItemDescription(item, _FurnaceItemInfo.ItemID, 0);
            item.GetComponent<BoxCollider>().enabled = true;
        }
        else 
        {
            OneGoldNumber.text = "0";
            item.transform.Find("SpriteItem").gameObject.SetActive(false);
            item.transform.Find("SpritePiece").gameObject.SetActive(false);
            item.transform.Find("LabelCount").gameObject.SetActive(false);
            item.GetComponent<UISprite>().spriteName = "Grade1";
            ItemName.text = "物品名字";
            item.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
