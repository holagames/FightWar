using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class VipPrivilegeItem : MonoBehaviour {

    public GameObject GridObj;//说明grid
    //public GameObject PrivilegeItem;//特权预制
    public GameObject ExplainItem;//特权说明预制
    public GameObject AwardItem;//礼包奖励预制

    public UILabel LabelExplain;//第几个特权
    public UILabel UiButtom;//v5超值礼包说明
    public UILabel XianjiaNum;//现价
    public UILabel YuanjiaNum;//原价
    public GameObject GridReward;//礼包奖励grid
    public GameObject SpriteCost;//是否领取图标

    public GameObject BuyButton;
    public GameObject HaveButton;

    public UIAtlas AvatarAtlas;//图集
    public UIAtlas ItemAtlas;

    private int NowBuymoney;
    private int Index;
    void Start() 
    {
        //if (UIEventListener.Get(BuyButton).onClick == null)
        //{
        //    UIEventListener.Get(BuyButton).onClick += delegate(GameObject go)
        //    {
        //        if (CharacterRecorder.instance.Vip < Index) 
        //        {
        //            UIManager.instance.OpenPromptWindow("Vip等级不够", PromptWindow.PromptType.Hint, null, null);
        //        }
        //        else if (CharacterRecorder.instance.lunaGem < NowBuymoney)
        //        {
        //            UIManager.instance.OpenPromptWindow("钻石不够", PromptWindow.PromptType.Hint, null, null);
        //        }
        //        else 
        //        {
        //            Debug.LogError("购买");
        //            GameObject.Find("VIPShopWindow").GetComponent<VipShopWindow>().BuyVipIndex = Index;
        //            NetworkHandler.instance.SendProcess(string.Format("5013#{0};{1};", 22000 + Index, 1));
        //        }
        //    };
        //}
        //if (CharacterRecorder.instance.BuyGiftBag[Index] == "0")
        //{
        //    BuyButton.SetActive(true);
        //    HaveButton.SetActive(false);
        //    if (UIEventListener.Get(BuyButton).onClick == null)
        //    {
        //        UIEventListener.Get(BuyButton).onClick += delegate(GameObject go)
        //        {
        //            if (CharacterRecorder.instance.Vip < Index)
        //            {
        //                UIManager.instance.OpenPromptWindow("Vip等级不够", PromptWindow.PromptType.Hint, null, null);
        //            }
        //            else if (CharacterRecorder.instance.lunaGem < NowBuymoney)
        //            {
        //                UIManager.instance.OpenPromptWindow("钻石不够", PromptWindow.PromptType.Hint, null, null);
        //            }
        //            else
        //            {
        //                Debug.LogError("购买");
        //                GameObject.Find("VIPShopWindow").GetComponent<VipShopWindow>().BuyVipIndex = Index;
        //                NetworkHandler.instance.SendProcess(string.Format("5013#{0};{1};", 22000 + Index, 1));
        //            }
        //        };
        //    }
        //}
        //else
        //{
        //    BuyButton.SetActive(false);
        //    HaveButton.SetActive(true);
        //}
    }
    public void SetIndexInfo(Vip vipinfo)
    {
        this.Index = vipinfo.VipID;
        BuyGiftBagNumber();
        LabelExplain.text = "V" + Index.ToString() + "特权";
        AddExplainItem(vipinfo);
        AddAwardItem(vipinfo);
        //SetBuyMation(vipinfo);
    }
    void AddExplainItem(Vip vipinfo) //添加数据说明
    {
        string text = vipinfo.Des.Replace("\\N", "\n");
        string[] DesSplit = text.Split('\n');

        for (int i = 0; i<DesSplit.Length-1; i++)
        {
            GameObject ExplainObj = NGUITools.AddChild(GridObj, ExplainItem);
            ExplainObj.SetActive(true);
            ExplainObj.transform.Find("LabelMessage").GetComponent<UILabel>().text = DesSplit[i];
        }
/*        GameObject ExplainObj = NGUITools.AddChild(GridObj, ExplainItem);
        ExplainObj.SetActive(true);
        ExplainItem.transform.Find("LabelMessage").GetComponent<UILabel>().text = vipinfo.Des;*/
        GridObj.GetComponent<UIGrid>().repositionNow = true;
    }
    void AddAwardItem(Vip vipinfo) //添加奖励数据
    {
        UiButtom.text = "v" + Index.ToString() + "超值礼包包含以下内容:";
        NowBuymoney = vipinfo.BuyDiamondSale;
        XianjiaNum.text = vipinfo.BuyDiamondSale.ToString();//
        YuanjiaNum.text = vipinfo.BuyDiamondPrice.ToString();//(vipinfo.BuyDiamondPrice * 1.1).ToString();
        //Debug.LogError(TextTranslator.instance.GetItemByItemCode(vipinfo.VipGift).ComposeNumber);

        string StrAward = vipinfo.VipGift;//TextTranslator.instance.GetItemByItemCode(111).ToValue;//vipinfo.VipGift
        string[] dataSplit = StrAward.Split('!');
        for (int i = 0; i < dataSplit.Length - 1;i++ )
        {
            string[] ItemSplit=dataSplit[i].Split('$');
            GameObject AwardObj = NGUITools.AddChild(GridReward, AwardItem);
            AwardObj.SetActive(true);

            int ItemId = int.Parse(ItemSplit[0]);
            int ItemNum = int.Parse(ItemSplit[1]);
            SetAwardItemColor(ItemId, ItemNum, AwardObj);
            AwardObj.GetComponent<ItemExplanation>().SetAwardItem(ItemId, ItemNum);
        }
        GridReward.GetComponent<UIGrid>().repositionNow = true;
    }
    void SetAwardItemColor(int ItemId,int ItemNum, GameObject AwardObj)//奖励对应的图标 
    {
        AwardObj.GetComponent<UISprite>().spriteName = "yxdi" + (TextTranslator.instance.GetItemByItemCode(ItemId).itemGrade-1).ToString();
        if (ItemId.ToString()[0] == '4')
        {
            AwardObj.transform.Find("SpriteHero").GetComponent<UISprite>().atlas = ItemAtlas;
            AwardObj.transform.Find("SpriteHero").GetComponent<UISprite>().spriteName = ItemId.ToString();
            //AwardObj.transform.Find("LabelNum").GetComponent<UILabel>().text = ItemNum.ToString();
            if (ItemId.ToString()[1] == '0')
            {
                AwardObj.transform.Find("SpriteSuipian").gameObject.SetActive(false);
            }
            else 
            {
                AwardObj.transform.Find("SpriteSuipian").gameObject.SetActive(true);
            }           
        }
        else if (ItemId.ToString()[0] == '6')
        {
            AwardObj.transform.Find("SpriteHero").GetComponent<UISprite>().atlas = AvatarAtlas;
            AwardObj.transform.Find("SpriteHero").GetComponent<UISprite>().spriteName = ItemId .ToString();
            //AwardObj.transform.Find("LabelNum").GetComponent<UILabel>().text = ItemNum.ToString();
            AwardObj.transform.Find("SpriteSuipian").gameObject.SetActive(false);
        }
        else if (ItemId == 70000)
        {
            AwardObj.transform.Find("SpriteHero").GetComponent<UISprite>().atlas = ItemAtlas;
            AwardObj.transform.Find("SpriteHero").GetComponent<UISprite>().spriteName = ItemId.ToString();
            //AwardObj.transform.Find("LabelNum").GetComponent<UILabel>().text = ItemNum.ToString();
            AwardObj.transform.Find("SpriteSuipian").gameObject.SetActive(true);
        }
        else if (ItemId == 79999)
        {
            AwardObj.transform.Find("SpriteHero").GetComponent<UISprite>().atlas = ItemAtlas;
            AwardObj.transform.Find("SpriteHero").GetComponent<UISprite>().spriteName = ItemId.ToString();
            //AwardObj.transform.Find("LabelNum").GetComponent<UILabel>().text = ItemNum.ToString();
            AwardObj.transform.Find("SpriteSuipian").gameObject.SetActive(true);
        }
        else if (ItemId.ToString()[0] == '7')
        {
            AwardObj.transform.Find("SpriteHero").GetComponent<UISprite>().atlas = AvatarAtlas;
            AwardObj.transform.Find("SpriteHero").GetComponent<UISprite>().spriteName = (ItemId - 10000).ToString();
            //AwardObj.transform.Find("LabelNum").GetComponent<UILabel>().text = ItemNum.ToString();
            AwardObj.transform.Find("SpriteSuipian").gameObject.SetActive(true);
        }
        else if (ItemId.ToString()[0] == '8')
        {
            AwardObj.transform.Find("SpriteHero").GetComponent<UISprite>().atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(ItemId);
            AwardObj.transform.Find("SpriteHero").GetComponent<UISprite>().spriteName = mItemInfo.picID.ToString();
            //AwardObj.transform.Find("LabelNum").GetComponent<UILabel>().text = ItemNum.ToString();
            AwardObj.transform.Find("SpriteSuipian").gameObject.SetActive(true);
        }
        else if (ItemId.ToString()[0] == '9')
        {
            AwardObj.transform.Find("SpriteHero").GetComponent<UISprite>().atlas = ItemAtlas;
            AwardObj.transform.Find("SpriteHero").GetComponent<UISprite>().spriteName = ItemId.ToString();
            //AwardObj.transform.Find("LabelNum").GetComponent<UILabel>().text = ItemNum.ToString();
            AwardObj.transform.Find("SpriteSuipian").gameObject.SetActive(false);
        }
        else if (ItemId.ToString()[0] == '2') 
        {
            AwardObj.transform.Find("SpriteHero").GetComponent<UISprite>().atlas = ItemAtlas;
            AwardObj.transform.Find("SpriteHero").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(ItemId).picID.ToString();
            //AwardObj.transform.Find("LabelNum").GetComponent<UILabel>().text = ItemNum.ToString();
            AwardObj.transform.Find("SpriteSuipian").gameObject.SetActive(false);
        }
        else
        {
            AwardObj.transform.Find("SpriteHero").GetComponent<UISprite>().atlas = ItemAtlas;
            AwardObj.transform.Find("SpriteHero").GetComponent<UISprite>().spriteName = ItemId.ToString();
            //AwardObj.transform.Find("LabelNum").GetComponent<UILabel>().text = ItemNum.ToString();
            AwardObj.transform.Find("SpriteSuipian").gameObject.SetActive(false);
        }

        if (ItemNum >= 10000)
        {
            int a = ItemNum / 10000;
            int b = ItemNum % 10000 / 1000;
            AwardObj.transform.Find("LabelNum").GetComponent<UILabel>().text = a.ToString() + "." + b.ToString() + "W";
        }
        else
        {
            AwardObj.transform.Find("LabelNum").GetComponent<UILabel>().text = ItemNum.ToString();
        }
    }

    void SetBuyMation(Vip vipinfo)//是否购买 
    {
        if (CharacterRecorder.instance.PayDiamond <=vipinfo.TradingDiamondCount)
        {
            SpriteCost.SetActive(false);
        }
        else 
        {
            SpriteCost.SetActive(true);
        }
    }
    void BuyButtonState() //按钮状态
    {
        if (CharacterRecorder.instance.Vip < Index) 
        {
            BuyButton.GetComponent<UISprite>().spriteName = "buttonHui";
            BuyButton.transform.Find("BlueLabel").gameObject.SetActive(false);
            BuyButton.transform.Find("GrayLabel").gameObject.SetActive(true);
        }
    }
    void BuyGiftBagNumber() 
    {
        
        if (CharacterRecorder.instance.BuyGiftBag[Index-1] == "0")
        {
            BuyButton.SetActive(true);
            HaveButton.SetActive(false);
            SpriteCost.SetActive(false);
            //Debug.LogError("=====:  " + CharacterRecorder.instance.Vip + "---" + Index);
            if (CharacterRecorder.instance.Vip < Index)
            {
                BuyButton.GetComponent<UISprite>().spriteName = "buttonHui";
                BuyButton.transform.Find("BlueLabel").gameObject.SetActive(false);
                BuyButton.transform.Find("GrayLabel").gameObject.SetActive(true);
            }
            else
            {
                BuyButton.GetComponent<UISprite>().spriteName = "button1";
                BuyButton.transform.Find("BlueLabel").gameObject.SetActive(true);
                BuyButton.transform.Find("GrayLabel").gameObject.SetActive(false);
            }

            if (UIEventListener.Get(BuyButton).onClick == null)
            {
                UIEventListener.Get(BuyButton).onClick += delegate(GameObject go)
                {
                    if (CharacterRecorder.instance.Vip < Index)
                    {
                        UIManager.instance.OpenPromptWindow("Vip等级不够", PromptWindow.PromptType.Hint, null, null);
                    }
                    else if (CharacterRecorder.instance.lunaGem < NowBuymoney)
                    {
                        UIManager.instance.OpenPromptWindow("钻石不够", PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        Debug.LogError("购买");
                        GameObject.Find("VIPShopWindow").GetComponent<VipShopWindow>().BuyVipIndex = Index;
                        NetworkHandler.instance.SendProcess(string.Format("5013#{0};{1};", 22000 + Index, 1));
                    }
                };
            }
        }
        else 
        {
            BuyButton.SetActive(false);
            HaveButton.SetActive(true);
            SpriteCost.SetActive(true);
        }
    }
}
