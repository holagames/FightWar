using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SuperArmsStoreWindow : MonoBehaviour {

    public GameObject CloseButton;
    public GameObject QuestionButton;    
    public GameObject ShopItemTex;
    public GameObject HunterMessage;
    public GameObject uiGrid;
    public UILabel TimeLabel;
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;

    private List<SuperArmsStoreItem> SuperArmsStoreItemList = new List<SuperArmsStoreItem>();

	void Start () 
    {
        AddSuperArmsStoreItemList();
        NetworkHandler.instance.SendProcess("5301#;");
        UIEventListener.Get(CloseButton).onClick = delegate(GameObject go)
        {
            DestroyImmediate(this.gameObject);
        };

        UIEventListener.Get(QuestionButton).onClick = delegate(GameObject go)
        {
            HunterMessage.SetActive(true);
        };

        UIEventListener.Get(HunterMessage.transform.Find("CloseButton").gameObject).onClick = delegate(GameObject go)
        {
            HunterMessage.SetActive(false);
        };
        //NetworkHandler.instance.SendProcess("5301#;");
	}

    private void AddSuperArmsStoreItemList() 
    {
        foreach(var item in TextTranslator.instance.ArmsDealersList)
        {
            if (CharacterRecorder.instance.Vip == item.VipBuy) 
            {
                GameObject go = NGUITools.AddChild(uiGrid, ShopItemTex);
                go.SetActive(true);
                SuperArmsStoreItem superArmsStoreItem = new SuperArmsStoreItem();
                superArmsStoreItem.ItemAtlas = ItemAtlas;
                superArmsStoreItem.RoleAtlas = RoleAtlas;
                superArmsStoreItem.StoreItem = go;
                superArmsStoreItem.armsDealerID = item.ArmsDealerID;
                SuperArmsStoreItemList.Add(superArmsStoreItem);
            }
        }
        uiGrid.GetComponent<UIGrid>().Reposition();

        if (CharacterRecorder.instance.Vip >= 10) 
        {
            TimeLabel.transform.parent.gameObject.SetActive(false);
        }
    }

    public void GetarmsItemInfo(int num1,int num2,int num3,int num4) 
    {
        SuperArmsStoreItemList[0].NowBuyNum = num1;
        SuperArmsStoreItemList[1].NowBuyNum = num2;
        SuperArmsStoreItemList[2].NowBuyNum = num3;
        SuperArmsStoreItemList[3].NowBuyNum = num4;
        SuperArmsStoreItemList[0].SetSuperArmsStoreItem();
        SuperArmsStoreItemList[1].SetSuperArmsStoreItem();
        SuperArmsStoreItemList[2].SetSuperArmsStoreItem();
        SuperArmsStoreItemList[3].SetSuperArmsStoreItem();

        CancelInvoke("UpdateArmsStoreTime");
        InvokeRepeating("UpdateArmsStoreTime", 0, 1f);
    }


    void UpdateArmsStoreTime()
    {
        if (CharacterRecorder.instance.Vip < 10) 
        {
            if (CharacterRecorder.instance.HoldJunhuoTime > 0)
            {
                string houreStr = (CharacterRecorder.instance.HoldJunhuoTime / 3600).ToString("00");
                string miniteStr = (CharacterRecorder.instance.HoldJunhuoTime % 3600 / 60).ToString("00");
                string secondStr = (CharacterRecorder.instance.HoldJunhuoTime % 60).ToString("00");
                TimeLabel.text = string.Format("{0}:{1}:{2}", houreStr, miniteStr, secondStr);
            }
            else
            {
                CancelInvoke("UpdateArmsStoreTime");
                if (GameObject.Find("MainWindow") != null)
                {
                    GameObject.Find("MainWindow").GetComponent<MainWindow>().SureJunhuokuIsOpen();
                }
            }
        }
    }
}


public class SuperArmsStoreItem
{
    public GameObject StoreItem;
    public int armsDealerID = 0;  //流水号
    public int NowBuyNum = 0;      //当前流水号已买次数

    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;

    public void SetSuperArmsStoreItem() 
    {
        ArmsDealers armsDealers = TextTranslator.instance.GetArmsDealersByID(armsDealerID);
        StoreItem.transform.Find("TopName").GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(armsDealers.ItemID);

        int num = NowBuyNum / armsDealers.BuyTime;
        SetZhekouSprite(armsDealers.MinimumDiscount + num, armsDealers);
        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(armsDealers.ItemID);
        StoreItem.transform.Find("IconBg").GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
        SetItemDetail(armsDealers.ItemID, armsDealers.ItemNum);
        TextTranslator.instance.ItemDescription(StoreItem.transform.Find("IconBg").gameObject, armsDealers.ItemID, 0);

        UIEventListener.Get(StoreItem.transform.Find("BuyButton").gameObject).onClick = delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("5302#" + armsDealers.WinPosID + ";");
        };

    }

    private void SetZhekouSprite(int zhekouNum, ArmsDealers armsDealers) 
    {
        UISprite ZhekouSpr = StoreItem.transform.Find("ZhekouSpr").GetComponent<UISprite>();
        UILabel CanBuyNumber = StoreItem.transform.Find("CanBuyNumber").GetComponent<UILabel>();
        GameObject YuanjiaSpr = StoreItem.transform.Find("YuanjiaSpr").gameObject;
        GameObject XianjiaSpr = StoreItem.transform.Find("XianjiaSpr").gameObject;
        switch (zhekouNum) 
        {
            case 1:
                ZhekouSpr.spriteName = "junhuoshang_icon1";
                break;
            case 2:
                ZhekouSpr.spriteName = "junhuoshang_icon2";
                break;
            case 3:
                ZhekouSpr.spriteName = "junhuoshang_icon3";
                break;
            case 4:
                ZhekouSpr.spriteName = "junhuoshang_icon4";
                break;
            case 5:
                ZhekouSpr.spriteName = "junhuoshang_icon5";
                break;
            case 6:
                ZhekouSpr.spriteName = "junhuoshang_icon6";
                break;
            case 7:
                ZhekouSpr.spriteName = "junhuoshang_icon7";
                break;
            case 8:
                ZhekouSpr.spriteName = "junhuoshang_icon8";
                break;
            case 9:
                ZhekouSpr.spriteName = "junhuoshang_icon9";
                break;
        }

        if (zhekouNum < 10)
        {
            ZhekouSpr.gameObject.SetActive(true);
            CanBuyNumber.gameObject.SetActive(true);
            YuanjiaSpr.SetActive(true);
            XianjiaSpr.SetActive(true);
            YuanjiaSpr.transform.Find("yuanjiaNum").GetComponent<UILabel>().text = armsDealers.DiamondsTotalValue.ToString();
            XianjiaSpr.transform.Find("xianjiaNum").GetComponent<UILabel>().text = (armsDealers.DiamondsTotalValue * zhekouNum / 10).ToString();
            CanBuyNumber.text = "[61629f]当前折扣可买[-][f2f03f]" + (armsDealers.BuyTime - (NowBuyNum - armsDealers.BuyTime * (zhekouNum - armsDealers.MinimumDiscount))).ToString() + "[-][61629f]次[-]";
        }
        else 
        {
            ZhekouSpr.gameObject.SetActive(false);
            CanBuyNumber.gameObject.SetActive(false);
            YuanjiaSpr.SetActive(false);
            XianjiaSpr.SetActive(true);
            XianjiaSpr.transform.Find("xianjiaNum").GetComponent<UILabel>().text = armsDealers.DiamondsTotalValue.ToString();
        }      
    }

    public void SetItemDetail(int ItemCode, int itemCount)
    {
        UISprite spriteIcon = StoreItem.transform.Find("IconBg/Icon").GetComponent<UISprite>();
        GameObject suiPian = StoreItem.transform.Find("IconBg/suiPian").gameObject;
        if (ItemCode.ToString()[0] == '4')
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            if (ItemCode.ToString()[1] == '0')
            {
                suiPian.SetActive(false);
            }
            else
            {
                suiPian.SetActive(true);
            }
        }
        else if (ItemCode.ToString()[0] == '6')
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            suiPian.SetActive(false);
        }
        else if (ItemCode == 70000 || ItemCode == 79999)
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            suiPian.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '7' && ItemCode > 70000 && ItemCode != 79999)
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = (ItemCode - 10000).ToString();
            suiPian.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '8')
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
            //spriteIcon.spriteName = (ItemCode - 30000).ToString();
            spriteIcon.spriteName = mItemInfo.picID.ToString();
            suiPian.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '2')
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
            spriteIcon.spriteName = mItemInfo.picID.ToString();
            suiPian.SetActive(false);
        }
        else
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            suiPian.SetActive(false);
        }

        StoreItem.transform.Find("IconBg/Number").GetComponent<UILabel>().text = itemCount.ToString();
    }
}
