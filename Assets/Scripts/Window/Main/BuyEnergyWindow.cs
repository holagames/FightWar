
#region 旧的
//using UnityEngine;
//using System.Collections;

//public class BuyEnergyWindow: MonoBehaviour
//{

//    public UILabel CostNumber;
//    public UILabel CostDiamond;
//    public UILabel BuyLabel;
//    public GameObject UseButton;
//    public GameObject CloseButton;
//    public GameObject BuyEnergyWindowObj;
//    private Market MarketDic;
//    private int DicNum;
//    private int DayBuyEnergyCount;
//    private int BuyCounts;
//    // Use this for initialization
//    void Start () {
//        if (GameObject.Find("RoleWindow") != null || GameObject.Find("GachaWindow") != null)
//        {
//            BuyEnergyWindowObj.layer = 11;
//        }
//        else
//        {
//            BuyEnergyWindowObj.layer = 5;
//        }
//        NetworkHandler.instance.SendProcess("1106#");
//        if (UIEventListener.Get(CloseButton).onClick == null)
//        {
//            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
//            {
//                UIManager.instance.BackUI();
//                /*if (GameObject.Find("TaskWindow") != null)
//                {
//                    UIManager.instance.DestroyPanel(this.gameObject.name);
//                }
//                else
//                {
//                    UIManager.instance.BackUI();
//                }*/
//            };
//        }
//        if (UIEventListener.Get(UseButton).onClick == null)
//        {
//            UIEventListener.Get(UseButton).onClick += delegate(GameObject go)
//            {
//                //NetworkHandler.instance.SendProcess("1105#1");
//                if (BuyCounts >= DayBuyEnergyCount)
//                {
//                    UIManager.instance.OpenPromptWindow("达到上限", PromptWindow.PromptType.Hint, null, null);
//                }
//                else 
//                {
//                    NetworkHandler.instance.SendProcess("1105#1");
//                }
//            };
//        }
//        if (UIEventListener.Get(GameObject.Find("MaskButton")).onClick == null)
//        {
//            UIEventListener.Get(GameObject.Find("MaskButton")).onClick += delegate(GameObject go)
//            {
//                //DestroyImmediate(this.gameObject);
//               /* if (GameObject.Find("TaskWindow") != null)
//                {
//                    UIManager.instance.DestroyPanel(this.gameObject.name);
//                }
//                else
//                {
//                    UIManager.instance.BackUI();
//                }*/
//                UIManager.instance.BackUI();
//            };
//        }
//    }

//    public void GetCostDiamond(int BuyCount)
//    {
//        //this.DayBuyEnergyCount = TextTranslator.instance.GetVipDicByID(CharacterRecorder.instance.Vip).DayBuyEnergyCount;
//        this.BuyCounts = BuyCount;

//        CostNumber.text = "(今日已用" + BuyCount.ToString() + "/"+DayBuyEnergyCount.ToString()+")";

//        int MarketID = BuyCount + 1;
//        if (MarketID <= 20)
//        {
//            MarketID = BuyCount + 1;
//        }
//        else if (MarketID > 20 && MarketID <= 50)
//        {
//            MarketID = 21;
//        }
//        else if (MarketID > 50 && MarketID <= 100)
//        {
//            MarketID = 22;
//        }
//        else
//        {
//            MarketID = 23;
//        }

//        //MarketDic = TextTranslator.instance.GetMarketByID(BuyCount);
//        //CostDiamond.text = MarketDic.EnergyPrice.ToString();
//        //BuyLabel.text = "[-][89CEF3]购买[ffffff]" + MarketDic.EnergyNum + "[89CEF3]点精力需[-]";

//        Market MarkItem = TextTranslator.instance.GetMarketByID(MarketID);
//        CostDiamond.text = MarkItem.EnergyPrice.ToString();
//        BuyLabel.text = "[-][89CEF3]购买[ffffff]" + MarkItem.EnergyNum + "[89CEF3]点精力需[-]";
//    }
	
//}
#endregion


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuyEnergyWindow: MonoBehaviour
{

    public UILabel CostNumber;
    public UILabel CostDiamond;
    public UILabel BuyLabel;
    public GameObject UseButton;
    public GameObject CloseButton;
    public GameObject BuyEnergyWindowObj;
    private Market MarketDic;
    private int DicNum;
    private int DayBuyEnergyCount;
    private int BuyCounts;
	// Use this for initialization


    private int GroupID = 0;
    private int BuyCount = 0;
    private int CanBuyCount = 0;

	void Start () {
        if (GameObject.Find("RoleWindow") != null || GameObject.Find("GachaWindow") != null || GameObject.Find("ConquerWindow") != null)
        {
            BuyEnergyWindowObj.layer = 11;
        }
        else
        {
            BuyEnergyWindowObj.layer = 5;
        }
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }
        //if (UIEventListener.Get(UseButton).onClick == null)
        //{
        //    UIEventListener.Get(UseButton).onClick += delegate(GameObject go)
        //    {
        //        //NetworkHandler.instance.SendProcess("1105#1");
        //        if (BuyCounts >= DayBuyEnergyCount)
        //        {
        //            UIManager.instance.OpenPromptWindow("达到上限", PromptWindow.PromptType.Hint, null, null);
        //        }
        //        else 
        //        {
        //            NetworkHandler.instance.SendProcess("1105#1");
        //        }
        //    };
        //}
        //if (UIEventListener.Get(GameObject.Find("MaskButton")).onClick == null)
        //{
        //    UIEventListener.Get(GameObject.Find("MaskButton")).onClick += delegate(GameObject go)
        //    {
        //        UIManager.instance.BackUI();
        //    };
        //}
	}

    public void GetCostDiamond(int BuyCount)
    {
        //this.DayBuyEnergyCount = TextTranslator.instance.GetVipDicByID(CharacterRecorder.instance.Vip).DayBuyEnergyCount;
        this.BuyCounts = BuyCount;

        CostNumber.text = "(今日已用" + BuyCount.ToString() + "/"+DayBuyEnergyCount.ToString()+")";

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

        //MarketDic = TextTranslator.instance.GetMarketByID(BuyCount);
        //CostDiamond.text = MarketDic.EnergyPrice.ToString();
        //BuyLabel.text = "[-][89CEF3]购买[ffffff]" + MarketDic.EnergyNum + "[89CEF3]点精力需[-]";

        Market MarkItem = TextTranslator.instance.GetMarketByID(MarketID);
        CostDiamond.text = MarkItem.EnergyPrice.ToString();
        BuyLabel.text = "[-][89CEF3]购买[ffffff]" + MarkItem.EnergyNum + "[89CEF3]点精力需[-]";
    }


    public void SetSpecialInfo(int GroupID, int BuyCount, int CanBuyCount) //TopContent界面 使用体力和精力的情况
    {
        this.GroupID = GroupID;
        this.BuyCount = BuyCount;
        this.CanBuyCount = CanBuyCount;
        int ItemID=0;
        int useNum = BuyCount / 4;
        int allNum = (BuyCount + CanBuyCount) / 4;

        Debug.Log("GroupID " + GroupID);
        Debug.Log("BuyCount " + BuyCount);
        Debug.Log("CanBuyCount " + CanBuyCount);
        Debug.Log("useNum " + useNum);
        Debug.Log("allNum " + allNum);

        CostNumber.text = "(今日已用" + useNum + "/" + allNum + ")";

        BuyLabel.text = "[-][89CEF3]购买[ffffff]120[89CEF3]点体力需[-]";

        Dictionary<int, ShopCenter>.ValueCollection valueColl = TextTranslator.instance.ShopCenterDic.Values;
        foreach (ShopCenter _Shop in valueColl)//获取当前窗口位置的物品id;
        {
            if (_Shop.WindowID == GroupID)
            {
                ItemID = _Shop.ItemID;//物品id
                break;
            }
        }
        UIEventListener.Get(UseButton).onClick = delegate(GameObject go)
        {
            if (useNum == allNum)
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
                NetworkHandler.instance.SendProcess(string.Format("5014#{0};{1};{2};", ItemID, 4, GroupID));
                UIManager.instance.BackUI();
            }
        };
        if (useNum == allNum)
        {
            CostDiamond.text = "0";
        }
        else 
        {
            GetAllmoney(4);
        }
    }

    void GetAllmoney(int AddNum)
    {
        int Sellprice = 0;
        Dictionary<int, ShopCenterPeculiar>.ValueCollection valueColl2 = TextTranslator.instance.ShopCenterPeculiarDic.Values;
        for (int i = 1; i <= AddNum; i++)
        {
            Sellprice = Sellprice + GetSinglePrice(valueColl2, i);
        }
        CostDiamond.text = Sellprice.ToString();
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
        Debug.Log("OnePice" + OnePice);
        return OnePice;
    }
}