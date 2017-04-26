using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DiamondShopItemData
{
    public int index { get; private set; }
    public int itemId { get; private set; }
    //public int count { get; private set; }
    //public int grade { get; private set; }
    //public int priceType { get; private set; }
    //public int priceValue { get; private set; }
    public int buyCount { get; private set; }
    public int canBuyCount { get; private set; }

    public DiamondShopItemData(int index, int BuyCount, int CanBuyCount)//, int Id, int count, int grade, int priceType, int priceValue,
    {
        this.index = index;
        
        //this.count = count;
        //this.grade = grade;
        //this.priceType = priceType;
        //this.priceValue = priceValue;
        this.buyCount = BuyCount;
        this.canBuyCount = CanBuyCount;
        Dictionary<int, ShopCenter>.ValueCollection valueColl = TextTranslator.instance.ShopCenterDic.Values;
        foreach (ShopCenter _Shop in valueColl)//获取当前窗口位置的物品id;
        {
            if (_Shop.WindowID == index)
            {
                this.itemId = _Shop.ItemID;//物品id
                break;
            }
        }
    }
}


public class DiamondShopItem : MonoBehaviour {

    [SerializeField]
    private UISprite spriteIcon;
    [SerializeField]
    private UISprite spriteFrame;
    [SerializeField]
    private UISprite spriteType;
    [SerializeField]
    private UILabel LabelName;
    [SerializeField]
    private UILabel labelNumber;
    [SerializeField]
    private UILabel labelPrice;
    [SerializeField]
    private GameObject suiPian;

    private int ItemCode;
    private int Index;
    private int Sellprice;
    private int BuyNumber;
    private int CanBuyNumber;


    //private int GoldPrice;
    //private int DiamondPrice;
    //private int HonourPrice;
    private DiamondShopItemData oneShopItemData;
    public GameObject TextureMask;
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;

    void Start()
    {
        if (UIEventListener.Get(gameObject).onClick == null)
        {
            UIEventListener.Get(gameObject).onClick += delegate(GameObject go)
            {
                //if (ItemCount > 0)
                {
                    UIManager.instance.OpenSinglePanel("UsePropsWindow", false);                 
                    GameObject.Find("UsePropsWindow").GetComponent<UsePropsWindow>().SetPropsInfo(ItemCode, BuyNumber, CanBuyNumber, Index, Sellprice);
                }
            };
        }
        
    }
    public void SetItemCount()
    {
        //this.ItemCount = 0;
    }
    public void Init(DiamondShopItemData _oneShopItemData)
    {
        Dictionary<int, ShopCenter>.ValueCollection valueColl = TextTranslator.instance.ShopCenterDic.Values;
        foreach (ShopCenter _Shop in valueColl)//获取当前窗口位置的物品id;
        {
            if (_Shop.WindowID == _oneShopItemData.index) 
            {
                ItemCode = _Shop.ItemID;//物品id
                break;
            }
        }
        TextTranslator.instance.ItemDescription(spriteFrame.gameObject, ItemCode, 0);

        UIEventListener.Get(spriteFrame.gameObject).onClick += delegate(GameObject go)
        {
            //if (ItemCount > 0)
            {
                UIManager.instance.OpenSinglePanel("UsePropsWindow", false);
                GameObject.Find("UsePropsWindow").GetComponent<UsePropsWindow>().SetPropsInfo(ItemCode, BuyNumber, CanBuyNumber, Index, Sellprice);
            }
        };
        Dictionary<int, ShopCenterPeculiar>.ValueCollection valueColl2 = TextTranslator.instance.ShopCenterPeculiarDic.Values;
        foreach (ShopCenterPeculiar _Shop2 in valueColl2)//获取当前价格段位
        {
            if (_Shop2.PeculiarID == _oneShopItemData.index) 
            {
                if (_Shop2.BuyCount >= _oneShopItemData.buyCount+1)
                {
                    Sellprice = _Shop2.PriceDiamond;
                    break;
                }
            }
        }

        Index = _oneShopItemData.index;
        BuyNumber = _oneShopItemData.buyCount;
        CanBuyNumber = _oneShopItemData.canBuyCount;//可购买次数

        this.name = _oneShopItemData.index.ToString();
        LabelName.text = TextTranslator.instance.GetItemNameByItemCode(ItemCode);//道具名字
        labelNumber.text = "[95deff]今日可买[-][fbb00f]" + CanBuyNumber.ToString() + "[-][95deff]次[-]";
        labelPrice.text = Sellprice.ToString();

      
        if (ItemCode.ToString()[0] == '2')
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
            spriteFrame.spriteName = "Grade" + mItemInfo.itemGrade.ToString();
            spriteIcon.spriteName = mItemInfo.picID.ToString();
            suiPian.SetActive(false);
        }
        else if (ItemCode.ToString()[0] == '4')
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
            spriteFrame.spriteName = "Grade" + mItemInfo.itemGrade.ToString();
            spriteIcon.spriteName = mItemInfo.picID.ToString();
            if (ItemCode.ToString()[1] == '0')
            {
                suiPian.SetActive(false);
            }
            else
            {
                suiPian.SetActive(true);
            }
        }
        else 
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
            spriteFrame.spriteName = "Grade" + mItemInfo.itemGrade.ToString();
            spriteIcon.spriteName = ItemCode.ToString();
            suiPian.SetActive(false);
        }

        //oneShopItemData = _oneShopItemData;
        //this.name = _oneShopItemData.index.ToString();
        //Index = _oneShopItemData.index;
        ////ItemCode = _oneShopItemData.Id;
        ////ItemCount = _oneShopItemData.count;
        ////ItemGrade = _oneShopItemData.grade;
        ////Pricetype = _oneShopItemData.priceType;
        ////Sellprice = _oneShopItemData.priceValue;
        //BuyNumber = _oneShopItemData.buyCount;
        //CanBuyNumber = _oneShopItemData.canBuyCount;

        //string ItemNameStr = TextTranslator.instance.GetItemNameByItemCode(ItemCode);

        //if (ItemCount == 0)
        //{
        //    TextureMask.SetActive(true);           
        //}
        //spriteFrame.spriteName = "Grade" + ItemGrade.ToString();

        //if (ItemCode.ToString()[0] == '4')
        //{
        //    spriteIcon.atlas = ItemAtlas;
        //    spriteIcon.spriteName = (ItemCode - 10000).ToString();
        //    suiPian.SetActive(true);
        //}
        //else if (ItemCode.ToString()[0] == '7')
        //{
        //    spriteIcon.atlas = RoleAtlas;
        //    spriteIcon.spriteName = (ItemCode - 10000).ToString();
        //    suiPian.SetActive(true);
        //}
        //else if (ItemCode.ToString()[0] == '8')
        //{
        //    spriteIcon.atlas = ItemAtlas;
        //    spriteIcon.spriteName = (ItemCode - 30000).ToString();
        //    suiPian.SetActive(true);
        //}
        //else
        //{
        //    spriteIcon.atlas = ItemAtlas;
        //    spriteIcon.spriteName = ItemCode.ToString();
        //    suiPian.SetActive(false);
        //}
        //labelCount.text = ItemCount.ToString();
        //LabelName.text = ItemNameStr;
        //switch (_oneShopItemData.priceType)
        //{
        //    case 1: spriteType.spriteName = "icon1"; break;
        //    case 3: spriteType.spriteName = "icon3"; break;
        //    case 4: spriteType.spriteName = "icon4"; break;
        //    default: spriteType.spriteName = "icon" + _oneShopItemData.priceType.ToString(); break;
        //}
        //labelPrice.text = _oneShopItemData.priceValue.ToString();
    }
/*

    public void Init(int _Index, int _ItemCode, int _ItemGrade, int _ItemCount, int _GoldPrice, int _DiamondPrice, int _HonourPrice)
    {
        ItemCode = _ItemCode;
        ItemCount = _ItemCount;
        ItemGrade = _ItemGrade;

        GoldPrice = _GoldPrice;
        DiamondPrice = _DiamondPrice;
        HonourPrice = _HonourPrice;
        string ItemName = TextTranslator.instance.GetItemNameByItemCode(_ItemCode);

        if (ItemCount == 0)
        {

            TextureMask.SetActive(true);
        }

        Index = _Index;
        gameObject.transform.Find("SpriteAvatarBG").gameObject.GetComponent<UISprite>().spriteName = ("Grade" + (_ItemGrade + 1)).ToString();
        if (ItemCode.ToString()[0] == '4')
        {
            gameObject.transform.Find("SpriteAvatarBG").Find("SpriteAvatar").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            gameObject.transform.Find("SpriteAvatarBG").Find("SpriteAvatar").gameObject.GetComponent<UISprite>().spriteName = (ItemCode - 10000).ToString();
            gameObject.transform.Find("SpriteAvatarBG").Find("SuiPian").gameObject.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '7')
        {
            gameObject.transform.Find("SpriteAvatarBG").Find("SpriteAvatar").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
            gameObject.transform.Find("SpriteAvatarBG").Find("SpriteAvatar").gameObject.GetComponent<UISprite>().spriteName = (ItemCode - 10000).ToString();
            gameObject.transform.Find("SpriteAvatarBG").Find("SuiPian").gameObject.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '8')
        {
            gameObject.transform.Find("SpriteAvatarBG").Find("SpriteAvatar").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            gameObject.transform.Find("SpriteAvatarBG").Find("SpriteAvatar").gameObject.GetComponent<UISprite>().spriteName = (ItemCode - 30000).ToString();
            gameObject.transform.Find("SpriteAvatarBG").Find("SuiPian").gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.Find("SpriteAvatarBG").Find("SpriteAvatar").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            gameObject.transform.Find("SpriteAvatarBG").Find("SpriteAvatar").gameObject.GetComponent<UISprite>().spriteName = ItemCode.ToString();
            gameObject.transform.Find("SpriteAvatarBG").Find("SuiPian").gameObject.SetActive(false);
        }
        gameObject.transform.Find("SpriteAvatarBG").Find("LabelCount").gameObject.GetComponent<UILabel>().text = _ItemCount.ToString();
        gameObject.transform.Find("SpriteType").gameObject.GetComponent<UISprite>().spriteName = "icon3";
        gameObject.transform.Find("SpriteType").Find("LabelHonour").gameObject.GetComponent<UILabel>().text = _HonourPrice.ToString();
        gameObject.transform.Find("LabelName").gameObject.GetComponent<UILabel>().text = ItemName;

        if (_GoldPrice > 0)
        {
            gameObject.transform.Find("SpriteType").GetComponent<UISprite>().spriteName = "icon1";
            gameObject.transform.Find("SpriteType").Find("LabelHonour").gameObject.GetComponent<UILabel>().text = _GoldPrice.ToString();
        }
        else if (_DiamondPrice > 0)
        {
            gameObject.transform.Find("SpriteType").GetComponent<UISprite>().spriteName = "icon4";
            gameObject.transform.Find("SpriteType").Find("LabelHonour").gameObject.GetComponent<UILabel>().text = _DiamondPrice.ToString();
        }
        else
        {
            gameObject.transform.Find("SpriteType").GetComponent<UISprite>().spriteName = "icon3";
            gameObject.transform.Find("SpriteType").Find("LabelHonour").gameObject.GetComponent<UILabel>().text = _HonourPrice.ToString();
        }
    }*/
	
}
