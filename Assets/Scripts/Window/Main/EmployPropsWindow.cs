using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmployPropsWindow : MonoBehaviour {

    public GameObject PropsIcon;
    public UILabel PropsName;
    public UILabel HaveNumber;
    public UILabel BuyPropsLabel;
    public UILabel LabelMoney;
    public UILabel LabelAddPower;
    public GameObject CanBuyCountLabel;
    //public UILabel ExplainLabel;
    public GameObject CutTenButton;
    public GameObject CutOneButton;
    public GameObject AddOneButton;
    public GameObject AddTenButton;
    public GameObject MaxButton;
    public GameObject CloseButton;
    public GameObject SureButton;
    public GameObject CancelButton;
    public GameObject UseButton;
    public GameObject Labelright;
    //public UILabel Labelleft;
    //public UILabel LabelMoney;
    private int PropsNum;
    private int ItemID;
    private int UseCount=0;
    private int BuyCount;
    private int CanBuyCount=0;
    private int GroupID;
    //private int BuyCount;
    //private int CanBuyCount;
    //private int GroupID;
    //private int Sellprice;

    void Start()
    {
        //if (GameObject.Find("RoleWindow") != null || GameObject.Find("GachaWindow") != null)
        //{
        //    this.gameObject.layer = 11;
        //}
        //else
        //{
        //    this.gameObject.layer = 5;
        //}
        this.gameObject.layer = 11;
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
                if (UseCount + CanBuyCount == 0)
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
                else if (ItemID == 10901 || ItemID == 10902)
                {
                    PropsNum = 1;
                }
                else 
                {
                    if (PropsNum >= 11)
                    {
                        PropsNum -= 10;
                    }
                    else if (PropsNum < 11)
                    {
                        PropsNum = 1;
                    }
                }

                if (PropsNum > UseCount)
                {
                    GetAllmoney(PropsNum - UseCount);
                }
                else
                {
                    LabelMoney.text = "0";
                }
                BuyPropsLabel.text = PropsNum.ToString();
                SetAddPower();
                SetDiamondColor();
            };
        }
        if (UIEventListener.Get(CutOneButton).onClick == null)
        {
            UIEventListener.Get(CutOneButton).onClick += delegate(GameObject go)
            {
                if (UseCount + CanBuyCount == 0)
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
                else if (ItemID == 10901 || ItemID == 10902)
                {
                    PropsNum = 1;
                }
                else
                {
                    if (PropsNum > 1)
                    {
                        PropsNum -= 1;
                    }
                    else if (PropsNum == 1)
                    {
                        PropsNum = 1;
                    }
                }

                if (PropsNum > UseCount)
                {
                    GetAllmoney(PropsNum - UseCount);
                }
                else
                {
                    LabelMoney.text = "0";
                }
                BuyPropsLabel.text = PropsNum.ToString();
                SetAddPower();
                SetDiamondColor();
                //GetAllmoney(PropsNum);
            };
        }
        if (UIEventListener.Get(AddOneButton).onClick == null)
        {
            UIEventListener.Get(AddOneButton).onClick += delegate(GameObject go)
            {
                if (UseCount + CanBuyCount == 0)
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
                else if (ItemID == 10901 || ItemID == 10902)
                {
                    PropsNum = 1;
                }
                else
                {
                    if (PropsNum < UseCount + CanBuyCount)
                    {
                        PropsNum += 1;
                    }
                    else if (PropsNum == UseCount + CanBuyCount)
                    {
                        PropsNum = UseCount + CanBuyCount;
                    }
                }

                if (PropsNum > UseCount)
                {
                    GetAllmoney(PropsNum - UseCount);
                }
                else
                {
                    LabelMoney.text = "0";
                }
                BuyPropsLabel.text = PropsNum.ToString();
                SetAddPower();
                SetDiamondColor();
                //GetAllmoney(PropsNum);
            };
        }
        if (UIEventListener.Get(MaxButton).onClick == null)
        {
            UIEventListener.Get(MaxButton).onClick += delegate(GameObject go)
            {
                PropsNum = UseCount;
                BuyPropsLabel.text = PropsNum.ToString();
                // GetAllmoney(PropsNum);
                if (UseCount + CanBuyCount == 0)
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
                else if (ItemID == 10901 || ItemID == 10902)
                {
                    PropsNum = 1;
                }
                else
                {
                    PropsNum = UseCount + CanBuyCount;
                }
                if (PropsNum > UseCount)
                {
                    GetAllmoney(PropsNum - UseCount);
                }
                else
                {
                    LabelMoney.text = "0";
                }
                BuyPropsLabel.text = PropsNum.ToString();
                SetAddPower();
                SetDiamondColor();
            };
        }
        if (UIEventListener.Get(AddTenButton).onClick == null)
        {
            UIEventListener.Get(AddTenButton).onClick += delegate(GameObject go)
            {
                if (UseCount + CanBuyCount == 0)
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
                else if (ItemID == 10901 || ItemID == 10902)
                {
                    PropsNum = 1;
                }
                else
                {
                    if (PropsNum + 10 <= UseCount + CanBuyCount)
                    {
                        PropsNum += 10;
                    }
                    else
                    {
                        PropsNum = UseCount + CanBuyCount;
                    }
                }
                if (PropsNum > UseCount)
                {
                    GetAllmoney(PropsNum - UseCount);
                }
                else
                {
                    LabelMoney.text = "0";
                }
                BuyPropsLabel.text = PropsNum.ToString();
                SetAddPower();
                SetDiamondColor();
                // GetAllmoney(PropsNum);
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
                if (ItemID == 20002 || ItemID == 20003 || ItemID == 20009 || ItemID == 20010 || ItemID == 20011) //批量使用，公告用
                {
                    CharacterRecorder.instance.BagItemCode = ItemID;
                }
                NetworkHandler.instance.SendProcess(string.Format("5002#{0};{1};", ItemID, PropsNum));
                if (GameObject.Find("BuyPropsWindow") != null) 
                {
                    GameObject.Find("BuyPropsWindow").GetComponent<BuyPropsWindow>().UserNum = PropsNum;
                }
                DestroyImmediate(this.gameObject);
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
/*
    public void SetPropsInfo(int ItemId, int BuyCount, int CanBuyCount, int GroupID, int _OnePrice)
    {
        this.PropsNum = 1;
        this.ItemID = ItemId;
        this.BuyCount = BuyCount;
        this.CanBuyCount = CanBuyCount;
        this.GroupID = GroupID;
        this.Sellprice = _OnePrice;
        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(ItemId);
        PropsIcon.GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo.itemGrade.ToString();

        if (ItemID.ToString()[0] == '2')
        {
            PropsIcon.transform.Find("ItemIcon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(ItemID).picID.ToString();
        }
        else
        {
            PropsIcon.transform.Find("ItemIcon").GetComponent<UISprite>().spriteName = ItemId.ToString();
        }

        PropsName.text = _ItemInfo.itemName;
        HaveNumber.text = TextTranslator.instance.GetItemCountByID(ItemId).ToString();
        BuyPropsLabel.text = PropsNum.ToString();

        Labelleft.text = "今日可买" + CanBuyCount.ToString() + "次";

        LabelMoney.text = _OnePrice.ToString();//Sellprice.ToString();

    }*/

    public void SetPropsInfo(int ItemId) 
    {
        this.PropsNum = 1;
        this.ItemID = ItemId;
        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(ItemId);
        PropsIcon.GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
        if (ItemID.ToString()[0] == '2')
        {
            PropsIcon.transform.Find("ItemIcon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(ItemID).picID.ToString();
        }
        else
        {
            PropsIcon.transform.Find("ItemIcon").GetComponent<UISprite>().spriteName = ItemId.ToString();
        }

        UseCount = TextTranslator.instance.GetItemCountByID(ItemId);
        PropsName.text = _ItemInfo.itemName;
        HaveNumber.text = UseCount.ToString();
        BuyPropsLabel.text = PropsNum.ToString();
    }

    public void SetSpecialInfo(int GroupID, int BuyCount, int CanBuyCount) //TopContent界面 使用体力和精力的情况
    {
        this.PropsNum = 1;
        //this.ItemID = ItemId;

        this.BuyCount = BuyCount;
        this.CanBuyCount = CanBuyCount;
        this.GroupID = GroupID;
        CancelButton.SetActive(false);
        SureButton.SetActive(false);
        UseButton.SetActive(true);
        ///LabelMoney.gameObject.SetActive(true);
        Labelright.SetActive(true);
        LabelAddPower.gameObject.SetActive(true);
        CanBuyCountLabel.SetActive(true);
        if (CanBuyCount == 0)
        {
            CanBuyCountLabel.transform.Find("CanBuyLabel").GetComponent<UILabel>().text = CanBuyCount.ToString();
            CanBuyCountLabel.transform.Find("CanBuyLabel").GetComponent<UILabel>().color = Color.red;
        }
        else
        {
            CanBuyCountLabel.transform.Find("CanBuyLabel").GetComponent<UILabel>().text = CanBuyCount.ToString();
        }

        Dictionary<int, ShopCenter>.ValueCollection valueColl = TextTranslator.instance.ShopCenterDic.Values;
        foreach (ShopCenter _Shop in valueColl)//获取当前窗口位置的物品id;
        {
            if (_Shop.WindowID == GroupID)
            {
                this.ItemID = _Shop.ItemID;//物品id
                break;
            }
        }
        if (UIEventListener.Get(UseButton).onClick == null)
        {
            UIEventListener.Get(UseButton).onClick += delegate(GameObject go)
            {
                //if (PropsNum > UseCount)
                //{
                //    UIManager.instance.OpenPromptWindow("背包数量不足，无法使用", PromptWindow.PromptType.Hint, null, null);
                //}
                //else 
                //
                if (PropsNum != 0)
                {
                    NetworkHandler.instance.SendProcess(string.Format("5014#{0};{1};{2};", ItemID, PropsNum, GroupID));
                    DestroyImmediate(this.gameObject);
                }
                else 
                {
                    DestroyImmediate(this.gameObject);
                }

                //}
            };
        }

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

        UseCount = TextTranslator.instance.GetItemCountByID(ItemID);
        PropsName.text = _ItemInfo.itemName;
        HaveNumber.text = UseCount.ToString();
        if (UseCount + CanBuyCount == 0)
        {
            this.PropsNum = 0;
        }
        BuyPropsLabel.text = PropsNum.ToString();

        int OnePrice = 0;
        if (UseCount > 0 || UseCount + CanBuyCount == 0)
        {
            LabelMoney.text = "0";
        }
        else 
        {
            Dictionary<int, ShopCenterPeculiar>.ValueCollection valueColl2 = TextTranslator.instance.ShopCenterPeculiarDic.Values;
            foreach (ShopCenterPeculiar _Shop2 in valueColl2)//获取当前价格段位
            {
                if (_Shop2.PeculiarID == GroupID)
                {
                    if (_Shop2.BuyCount >= BuyCount+1)
                    {
                        OnePrice = _Shop2.PriceDiamond;
                        break;
                    }
                }
            }
            LabelMoney.text = OnePrice.ToString();
        }
        SetAddPower();
        SetDiamondColor();
    }

    void GetAllmoney(int AddNum)
    {
        int Sellprice = 0;
        Dictionary<int, ShopCenterPeculiar>.ValueCollection valueColl2 = TextTranslator.instance.ShopCenterPeculiarDic.Values;
        for (int i = 1; i <= AddNum; i++)
        {
            Sellprice = Sellprice + GetSinglePrice(valueColl2, i);
        }
        LabelMoney.text = Sellprice.ToString();
    }

    int GetSinglePrice(Dictionary<int, ShopCenterPeculiar>.ValueCollection valueColl, int OneBuy)
    {
        int OnePice = 0;
        foreach (ShopCenterPeculiar _Shop in valueColl)//获取当前价格段位
        {
            if (GroupID == _Shop.PeculiarID)
            {
                if (_Shop.BuyCount >= BuyCount + OneBuy)
                {
                    OnePice = _Shop.PriceDiamond;
                    break;
                }
            }
        }
        return OnePice;
    }

    void SetAddPower() //物品的作用
    {
        if (ItemID == 10602)
        {
            LabelAddPower.text = "回复体力:" + (PropsNum * 30).ToString();
        }
        else if (ItemID == 10702)
        {
            LabelAddPower.text = "回复精力:" + (PropsNum * 15).ToString();
        }
                else if (ItemID == 10901)
        {
            LabelAddPower.text = "免战:" + (PropsNum * 1).ToString() + "小时";
        }
        else if (ItemID == 10902)
        {
            LabelAddPower.text = "免战:" + (PropsNum * 10).ToString() + "小时";
        }
    }

    void SetDiamondColor() //设置钻石颜色
    {
        if (int.Parse(LabelMoney.text) > CharacterRecorder.instance.lunaGem)
        {
            LabelMoney.color = Color.red;
        }
        else 
        {
            LabelMoney.color = new Color(137 / 255f, 206 / 255f, 243 / 255f, 255 / 255f);
        }
    }
}
