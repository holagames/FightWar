using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UsePropsWindow : MonoBehaviour {

    public GameObject PropsIcon;
    public UILabel PropsName;
    public UILabel HaveNumber;
    public UILabel BuyPropsLabel;
    public UILabel ExplainLabel;
    public GameObject CutTenButton;
    public GameObject CutOneButton;
    public GameObject AddOneButton;
    public GameObject AddTenButton;
    public GameObject MaxButton;
    public GameObject CloseButton;
    public GameObject SureButton;
    public GameObject CancelButton;

    public UILabel Labelleft;
    public UILabel LabelMoney;
    private int PropsNum;
    private int ItemID;
    private int BuyCount;
    private int CanBuyCount;
    private int GroupID;
    private int Sellprice;

	void Start () {
        if (GameObject.Find("RoleWindow") != null || GameObject.Find("GachaWindow") != null)
        {
            this.gameObject.layer = 11;
        }
        else if (GameObject.Find("LegionTeamWindow") != null)
        {
            foreach (Transform tran in GetComponentsInChildren<Transform>())
            {
                tran.gameObject.layer = 9;
            }
        }
        else
        {
            this.gameObject.layer = 5;
        }
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                //DestroyImmediate(this.gameObject);
                DestroyImmediate(this.gameObject);
            };
        }
        if (UIEventListener.Get(CutTenButton).onClick == null)
        {
            UIEventListener.Get(CutTenButton).onClick += delegate(GameObject go)
            {
                if (CanBuyCount == 0)
                {
                    //UIManager.instance.OpenPromptWindow("请提升Vip等级!", PromptWindow.PromptType.Hint, null, null);
                    if (CharacterRecorder.instance.Vip < 15)
                    {
                        UIManager.instance.OpenPromptWindow("请提升Vip等级,增加购买次数!", PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("今天已达最高购买次数，明天再来哦!", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else 
                {
                    if (PropsNum >= 11)
                    {
                        PropsNum -= 10;
                        BuyPropsLabel.text = PropsNum.ToString();
                    }
                    else if (PropsNum < 11)
                    {
                        PropsNum = 1;
                        BuyPropsLabel.text = PropsNum.ToString();
                    }
                    GetAllmoney(PropsNum); 
                }
            };
        }
        if (UIEventListener.Get(CutOneButton).onClick == null)
        {
            UIEventListener.Get(CutOneButton).onClick += delegate(GameObject go)
            {
                if (CanBuyCount == 0)
                {
                    if (CharacterRecorder.instance.Vip < 15)
                    {
                        UIManager.instance.OpenPromptWindow("请提升Vip等级,增加购买次数!", PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("今天已达最高购买次数，明天再来哦!", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else 
                {
                    if (PropsNum > 1)
                    {
                        PropsNum -= 1;
                        BuyPropsLabel.text = PropsNum.ToString();
                    }
                    else if (PropsNum == 1)
                    {
                        PropsNum = 1;
                        BuyPropsLabel.text = PropsNum.ToString();
                    }
                    GetAllmoney(PropsNum); 
                }
            };
        }
        if (UIEventListener.Get(AddOneButton).onClick == null)
        {
            UIEventListener.Get(AddOneButton).onClick += delegate(GameObject go)
            {
                if (CanBuyCount == 0)
                {
                    if (CharacterRecorder.instance.Vip < 15)
                    {
                        UIManager.instance.OpenPromptWindow("请提升Vip等级,增加购买次数!", PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("今天已达最高购买次数，明天再来哦!", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else
                {
                    if (PropsNum < CanBuyCount)
                    {
                        PropsNum += 1;
                        BuyPropsLabel.text = PropsNum.ToString();
                    }
                    else if (PropsNum == CanBuyCount)
                    {
                        PropsNum = CanBuyCount;
                        BuyPropsLabel.text = PropsNum.ToString();
                        if (CharacterRecorder.instance.Vip < 15)
                        {
                            UIManager.instance.OpenPromptWindow("请提升Vip等级,增加购买次数!", PromptWindow.PromptType.Hint, null, null);
                        }
                        else
                        {
                            UIManager.instance.OpenPromptWindow("今天已达最高购买次数，明天再来哦!", PromptWindow.PromptType.Hint, null, null);
                        }
                    }
                    GetAllmoney(PropsNum);
                }
 
            };
        }
        if (UIEventListener.Get(AddTenButton).onClick == null)
        {
            UIEventListener.Get(AddTenButton).onClick += delegate(GameObject go)
            {
                if (CanBuyCount == 0)
                {
                    if (CharacterRecorder.instance.Vip < 15)
                    {
                        UIManager.instance.OpenPromptWindow("请提升Vip等级,增加购买次数!", PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("今天已达最高购买次数，明天再来哦!", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else 
                {
                    if (PropsNum + 10 <= CanBuyCount)
                    {
                        PropsNum += 10;
                        BuyPropsLabel.text = PropsNum.ToString();
                    }
                    else //if (PropsNum == CanBuyCount)
                    {
                        PropsNum = CanBuyCount;
                        BuyPropsLabel.text = PropsNum.ToString();
                        if (CharacterRecorder.instance.Vip < 15)
                        {
                            UIManager.instance.OpenPromptWindow("请提升Vip等级,增加购买次数!", PromptWindow.PromptType.Hint, null, null);
                        }
                        else
                        {
                            UIManager.instance.OpenPromptWindow("今天已达最高购买次数，明天再来哦!", PromptWindow.PromptType.Hint, null, null);
                        }
                    }
                    GetAllmoney(PropsNum);
                }

            };
        }
        if (UIEventListener.Get(MaxButton).onClick == null)
        {
            UIEventListener.Get(MaxButton).onClick += delegate(GameObject go)
            {
                //PropsNum = CanBuyCount;
                //BuyPropsLabel.text = PropsNum.ToString();
                //GetAllmoney(PropsNum);
                if (CanBuyCount == 0)
                {
                    if (CharacterRecorder.instance.Vip < 15)
                    {
                        UIManager.instance.OpenPromptWindow("请提升Vip等级,增加购买次数!", PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("今天已达最高购买次数，明天再来哦!", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else
                {
                    PropsNum = CanBuyCount;
                    BuyPropsLabel.text = PropsNum.ToString();
                    GetAllmoney(PropsNum);
                }
            };
        }
        if (UIEventListener.Get(CancelButton).onClick == null)
        {
            UIEventListener.Get(CancelButton).onClick += delegate(GameObject go)
            {
                DestroyImmediate(this.gameObject);
            };
        }

        if (UIEventListener.Get(SureButton).onClick == null)
        {
            UIEventListener.Get(SureButton).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.lunaGem >= Sellprice&&CanBuyCount>0) 
                {
                    NetworkHandler.instance.SendProcess(string.Format("5010#{0};{1};", GroupID, PropsNum));
                    if(GroupID > 31 && GroupID < 48)
                    {
                        PlayerPrefs.SetInt("ShopBuy", 0);
                    }
                    DestroyImmediate(this.gameObject);
                }
                else if (CharacterRecorder.instance.lunaGem < Sellprice) 
                {
                    UIManager.instance.OpenPromptWindow("钻石不足", PromptWindow.PromptType.Hint, null, null);
                }
                else if (CanBuyCount == 0) 
                {
                    if (CharacterRecorder.instance.Vip < 15)
                    {
                        UIManager.instance.OpenPromptWindow("请提升Vip等级,增加购买次数!", PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("今天已达最高购买次数，明天再来哦!", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                //else if (PropsNum == 0) 
                //{
                //    UIManager.instance.OpenPromptWindow("选择购买数量", PromptWindow.PromptType.Hint, null, null);
                //}
            };
        }

        //if (UIEventListener.Get(GameObject.Find("MaskButton")).onClick == null)
        //{
        //    UIEventListener.Get(GameObject.Find("MaskButton")).onClick += delegate(GameObject go)
        //    {
        //        DestroyImmediate(this.gameObject);
        //    };
        //}
	}

    public void SetPropsInfo(int ItemId,int BuyCount,int CanBuyCount,int GroupID,int _OnePrice)
    {
        if (CanBuyCount == 0)
        {
            this.PropsNum = 0;
        }
        else 
        {
            this.PropsNum = 1;
        }

        this.ItemID = ItemId;
        this.BuyCount = BuyCount;
        this.CanBuyCount = CanBuyCount;
        this.GroupID = GroupID;
        this.Sellprice = _OnePrice;
        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(ItemId);
        PropsIcon.GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo.itemGrade.ToString();

        TextTranslator.instance.ItemDescription(PropsIcon, ItemId, 0);

        if (ItemID.ToString()[0] == '4')
        {
            if (ItemID.ToString()[1] == '0')
            {
                PropsIcon.transform.Find("SuiPian").gameObject.SetActive(false);
            }
            else
            {
                PropsIcon.transform.Find("SuiPian").gameObject.SetActive(true);
            }
        }

        if (ItemID.ToString()[0] == '2')
        {
            PropsIcon.transform.Find("ItemIcon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(ItemID).picID.ToString();
        }
        else 
        {
            PropsIcon.transform.Find("ItemIcon").GetComponent<UISprite>().spriteName = ItemId.ToString();
        }

        PropsName.text = _ItemInfo.itemName;
        //HaveNumber.text = TextTranslator.instance.GetItemCountByID(ItemId).ToString();
        HaveNumberWithAny(ItemId);
        BuyPropsLabel.text = PropsNum.ToString();

        Labelleft.text = "今日可买" + CanBuyCount.ToString() + "次";

        if (CanBuyCount == 0)
        {
            LabelMoney.text = "0";
        }
        else 
        {
            LabelMoney.text = _OnePrice.ToString();
        }
        //LabelMoney.text = _OnePrice.ToString();//Sellprice.ToString();

    }

    void GetAllmoney(int AddNum) 
    {
        Sellprice = 0;
        Dictionary<int, ShopCenterPeculiar>.ValueCollection valueColl2 = TextTranslator.instance.ShopCenterPeculiarDic.Values;
        for (int i = 1; i <= AddNum; i++)
        {
            Sellprice = Sellprice + GetSinglePrice(valueColl2, i);
        }
        LabelMoney.text = Sellprice.ToString();

        //for (int i = Ma + 1; i < Mb; i++) //中间价格
        //{
        //    Sellprice += TextTranslator.instance.GetShopCenterPeculiarByID(i).PriceDiamond * (TextTranslator.instance.GetShopCenterPeculiarByID(i).BuyCount - TextTranslator.instance.GetShopCenterPeculiarByID(i - 1).BuyCount);
        //}

        //Sellprice = Sellprice + TextTranslator.instance.GetShopCenterPeculiarByID(Mb).PriceDiamond * (BuyCount + AddNum - TextTranslator.instance.GetShopCenterPeculiarByID(Mb).BuyCount) +
        //            TextTranslator.instance.GetShopCenterPeculiarByID(Ma).PriceDiamond * (TextTranslator.instance.GetShopCenterPeculiarByID(Ma).BuyCount - BuyCount);
        //LabelMoney.text = Sellprice.ToString();
    }

    int GetSinglePrice(Dictionary<int, ShopCenterPeculiar>.ValueCollection valueColl,int OneBuy) 
    {
        int OnePice=0;
        foreach (ShopCenterPeculiar _Shop in valueColl)//获取当前价格段位
        {
            if (GroupID == _Shop.PeculiarID)
            {
                if (_Shop.BuyCount >= BuyCount+OneBuy)
                {
                    OnePice = _Shop.PriceDiamond;
                    break;
                }
            }
        }
        return OnePice;
    }

    public void SetSpecialPropsInfo(int GroupID,int BuyCount, int CanBuyCount)//体力精力Topcontent界面
    {
        this.PropsNum = 1;
        //this.ItemID = ItemId;
        this.BuyCount = BuyCount;
        this.CanBuyCount = CanBuyCount;
        this.GroupID = GroupID;
        //this.Sellprice = _OnePrice;

        Dictionary<int, ShopCenter>.ValueCollection valueColl = TextTranslator.instance.ShopCenterDic.Values;
        foreach (ShopCenter _Shop in valueColl)//获取当前窗口位置的物品id;
        {
            if (_Shop.WindowID == GroupID)
            {
                this.ItemID = _Shop.ItemID;//物品id
                break;
            }
        }


        int OnePrice = 0;
        Dictionary<int, ShopCenterPeculiar>.ValueCollection valueColl2 = TextTranslator.instance.ShopCenterPeculiarDic.Values;
        foreach (ShopCenterPeculiar _Shop2 in valueColl2)//获取当前价格段位
        {
            if (_Shop2.PeculiarID == GroupID)
            {
                if (_Shop2.BuyCount >= BuyCount)
                {
                    OnePrice = _Shop2.PriceDiamond;
                    break;
                }
            }
        }
        this.Sellprice = OnePrice;

        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(ItemID);
        PropsIcon.GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo.itemGrade.ToString();

        if (ItemID.ToString()[0] == '2')
        {
            PropsIcon.transform.Find("ItemIcon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(ItemID).picID.ToString();
        }
        else
        {
            PropsIcon.transform.Find("ItemIcon").GetComponent<UISprite>().spriteName = ItemID.ToString();
        }

        if (ItemID.ToString()[0] == '4')
        {
            if (ItemID.ToString()[1] == '0')
            {
                PropsIcon.transform.Find("SuiPian").gameObject.SetActive(false);
            }
            else
            {
                PropsIcon.transform.Find("SuiPian").gameObject.SetActive(true);
            }
        }
        PropsName.text = _ItemInfo.itemName;
        //HaveNumber.text = TextTranslator.instance.GetItemCountByID(ItemID).ToString();
        HaveNumberWithAny(ItemID);
        BuyPropsLabel.text = PropsNum.ToString();

        Labelleft.text = "今日可买" + CanBuyCount.ToString() + "次";

        LabelMoney.text = OnePrice.ToString();//Sellprice.ToString();

    }

    private void HaveNumberWithAny(int ItemCode)
    {
        int myOwnCount = 0;
        switch (ItemCode)
        {
            case 90001: myOwnCount = CharacterRecorder.instance.gold; break;
            case 90002: myOwnCount = CharacterRecorder.instance.lunaGem; break;
            case 90003: myOwnCount = CharacterRecorder.instance.HonerValue; break;
            case 90004: myOwnCount = CharacterRecorder.instance.TrialCurrency; break;
            case 90005: myOwnCount = CharacterRecorder.instance.ArmyGroup; break;
            case 90006: myOwnCount = CharacterRecorder.instance.GoldBar; break; break;
            case 90007: myOwnCount = CharacterRecorder.instance.stamina; break;
            case 90008: myOwnCount = CharacterRecorder.instance.sprite; break;
            case 90009: myOwnCount = CharacterRecorder.instance.exp; break;
            case 10104: myOwnCount = CharacterRecorder.instance.techPoint; break;
            default: myOwnCount = TextTranslator.instance.GetItemCountByID(ItemCode); break;
        }
        HaveNumber.text = myOwnCount.ToString();
    }
}
