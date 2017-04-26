using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuyPropsWindow : MonoBehaviour{

    public UILabel PropsMessage;
    public GameObject PropsIcon;
    public UILabel PropsAddNum;
    public UILabel DiamondCost;
    public UILabel BuyNumber;
    public UILabel HaveNumber;
    public GameObject BuyButton;
    public GameObject UseButton;
    public GameObject CloseButton;
    public UILabel Title;

    private int ItemID;
    private int ShopWindowId;      //商店
    private int BuyCount;
    private int CanBuyCount;
    private int DiamondPrice;
    public int UserNum=0;
    void Start()
    {
        if (GameObject.Find("RoleWindow") != null || GameObject.Find("GachaWindow") != null)
        {
            this.gameObject.layer = 11;
        }
        else
        {
            this.gameObject.layer = 5;
        }

        //NetworkHandler.instance.SendProcess("1102#");

        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }
        if (UIEventListener.Get(BuyButton).onClick == null)
        {
            UIEventListener.Get(BuyButton).onClick += delegate(GameObject go)
            {
                if (BuyNumber.text=="0")
                {
                    UIManager.instance.OpenPromptWindow("达到购买上限", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    UIManager.instance.OpenSinglePanel("UsePropsWindow", false);
                    GameObject.Find("UsePropsWindow").GetComponent<UsePropsWindow>().SetPropsInfo(ItemID, BuyCount, CanBuyCount, ShopWindowId, DiamondPrice);
                    //NetworkHandler.instance.SendProcess(string.Format("5010#{0};{1};", ShopWindowId, "1"));
                    //NetworkHandler.instance.SendProcess("1101#1;");
                    //NetworkHandler.instance.SendProcess(string.Format("5103#{0};{1};", , index));
                    Debug.Log("掏钱买体力 哇卡卡");
                }
            };
        }
        if (UIEventListener.Get(UseButton).onClick == null)
        {
            UIEventListener.Get(UseButton).onClick += delegate(GameObject go)
            {
                if (HaveNumber.text == "0")
                {
                    UIManager.instance.OpenPromptWindow("此物品数量不足", PromptWindow.PromptType.Hint, null, null);
                }
                else 
                {
                    if (ItemID == 10901)
                    {
                        NetworkHandler.instance.SendProcess("1406#1;");
                    }
                    else if (ItemID == 10902)
                    {
                        NetworkHandler.instance.SendProcess("1406#10;");
                    }
                    else
                    {
                        //NetworkHandler.instance.SendProcess(string.Format("5002#{0};{1};", ItemID, "1"));
                        UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
                        GameObject.Find("EmployPropsWindow").GetComponent<EmployPropsWindow>().SetPropsInfo(ItemID);
                    }
                }
                //NetworkHandler.instance.SendProcess(string.Format("5002#{0};{1};", ItemID,"1"));
                //UIManager.instance.OpenSinglePanel("UsePropsWindow", false);
                //GameObject.Find("UsePropsWindow").GetComponent<UsePropsWindow>().SetPropsInfo(ItemID);
            };
        }

    }
    public void GetCostDiamond(DiamondShopItemData _oneShopItemDate)//int ShopWindowId, int BuyCount, int CanBuyCount
    {
        //this.ItemID = ItemId;
        this.ShopWindowId = _oneShopItemDate.index;
        this.BuyCount = _oneShopItemDate.buyCount;
        this.CanBuyCount = _oneShopItemDate.canBuyCount;

        Dictionary<int, ShopCenter>.ValueCollection valueColl = TextTranslator.instance.ShopCenterDic.Values;
        foreach (ShopCenter _Shop in valueColl)//获取当前窗口位置的物品id;
        {
            if (_Shop.WindowID == ShopWindowId)
            {
                this.ItemID = _Shop.ItemID;//物品id
                break;
            }
        }

        if (ItemID == 10602)
        {
            Title.text = "购买体力";
            PropsMessage.text = "长官,你体力不支了,体力回复剂可以快速回复体力";
            TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(ItemID);
            PropsIcon.GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
            PropsIcon.transform.Find("ItemIcon").GetComponent<UISprite>().spriteName = "10602";
            PropsAddNum.text = "体力 +30";

            Dictionary<int, ShopCenterPeculiar>.ValueCollection valueColl2 = TextTranslator.instance.ShopCenterPeculiarDic.Values;
            foreach (ShopCenterPeculiar _Shop2 in valueColl2)//获取当前价格段位
            {
                if (_Shop2.PeculiarID == ShopWindowId)
                {
                    if (_Shop2.BuyCount >= BuyCount)
                    {
                        DiamondPrice = _Shop2.PriceDiamond;
                        break;
                    }
                }
            }
            DiamondCost.text = DiamondPrice.ToString();
            BuyNumber.text = CanBuyCount.ToString();
            HaveNumber.text = TextTranslator.instance.GetItemCountByID(ItemID).ToString();

            //BuyNumber.text = (TextTranslator.instance.GetVipDicByID(CharacterRecorder.instance.Vip).DayBuyStaminaCount - BuyCount).ToString();
            //HaveNumber.text = TextTranslator.instance.GetItemCountByID(ItemId).ToString();
            //int MarketID = GetMarketID(BuyCount);
            //Market MarkItem = TextTranslator.instance.GetMarketByID(MarketID);
            //DiamondCost.text = MarkItem.StaminaPrice.ToString();
        }
        else if (ItemID == 10702)
        {
            Title.text = "购买精力";
            PropsMessage.text = "长官,你精力不支了,精力回复剂可以快速回复体力";
            TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(ItemID);
            PropsIcon.GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
            PropsIcon.transform.Find("ItemIcon").GetComponent<UISprite>().spriteName = "10702";
            PropsAddNum.text = "精力 +15";

            Dictionary<int, ShopCenterPeculiar>.ValueCollection valueColl2 = TextTranslator.instance.ShopCenterPeculiarDic.Values;
            foreach (ShopCenterPeculiar _Shop2 in valueColl2)//获取当前价格段位
            {
                if (_Shop2.PeculiarID == ShopWindowId)
                {
                    if (_Shop2.BuyCount >= BuyCount)
                    {
                        DiamondPrice = _Shop2.PriceDiamond;
                        break;
                    }
                }
            }
            DiamondCost.text = DiamondPrice.ToString();
            BuyNumber.text = CanBuyCount.ToString();
            HaveNumber.text = TextTranslator.instance.GetItemCountByID(ItemID).ToString();


            //BuyNumber.text = (TextTranslator.instance.GetVipDicByID(CharacterRecorder.instance.Vip).DayBuyEnergyCount - BuyCount).ToString();
            //HaveNumber.text = TextTranslator.instance.GetItemCountByID(ItemId).ToString();
            //int MarketID = GetMarketID(BuyCount);
            //Market MarkItem = TextTranslator.instance.GetMarketByID(MarketID);
            //DiamondCost.text = MarkItem.EnergyPrice.ToString();
        }
        else if (ItemID == 10901||ItemID == 10902)
        {
            Title.text = "购买免战牌";
            PropsMessage.text = "长官,你免战牌数量不足,请购买免战牌";
            TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(ItemID);
            PropsIcon.GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
            PropsIcon.transform.Find("ItemIcon").GetComponent<UISprite>().spriteName = ItemID.ToString();
            if (ItemID == 10901)
            {
                PropsAddNum.text = "免战1小时";
            }
            else
            {
                PropsAddNum.text = "免战10小时";
            }
            Dictionary<int, ShopCenterPeculiar>.ValueCollection valueColl2 = TextTranslator.instance.ShopCenterPeculiarDic.Values;
            foreach (ShopCenterPeculiar _Shop2 in valueColl2)//获取当前价格段位
            {
                if (_Shop2.PeculiarID == ShopWindowId)
                {
                    if (_Shop2.BuyCount >= BuyCount)
                    {
                        DiamondPrice = _Shop2.PriceDiamond;
                        break;
                    }
                }
            }
            DiamondCost.text = DiamondPrice.ToString();
            BuyNumber.text = CanBuyCount.ToString();
            HaveNumber.text = TextTranslator.instance.GetItemCountByID(ItemID).ToString();


            //BuyNumber.text = (TextTranslator.instance.GetVipDicByID(CharacterRecorder.instance.Vip).DayBuyEnergyCount - BuyCount).ToString();
            //HaveNumber.text = TextTranslator.instance.GetItemCountByID(ItemId).ToString();
            //int MarketID = GetMarketID(BuyCount);
            //Market MarkItem = TextTranslator.instance.GetMarketByID(MarketID);
            //DiamondCost.text = MarkItem.EnergyPrice.ToString();
        }
    }

    public void BuyPropsSucess() 
    {
        //BuyNumber.text = (CanBuyCount - 1).ToString();
        //HaveNumber.text = (int.Parse(HaveNumber.text) + 1).ToString();
        NetworkHandler.instance.SendProcess(string.Format("5012#{0};", ItemID));
    }

    public void UsePropsSucess() 
    {
        HaveNumber.text = (int.Parse(HaveNumber.text) - UserNum).ToString();
    }
    int GetMarketID(int BuyCount)
    {
        int MarketID = BuyCount + 1;
        if (MarketID <= 20)
        {
            MarketID = BuyCount + 1;
        }
        else if (MarketID > 20 && MarketID <= 50)
        {
            MarketID = 21;
        }
        else if (MarketID > 50 && MarketID <= 100)
        {
            MarketID = 22;
        }
        else
        {
            MarketID = 23;
        }
        return MarketID;
    }
}
