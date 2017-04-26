using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Net;
public class BuyGlodWindow : MonoBehaviour {

    public GameObject CloseButton;
    public GameObject OneButton;
    public GameObject TenButton;
    public UILabel CostNumber;
    public UILabel CostDiamond;
    public UILabel GetGold;
    public GameObject BuyGoldWindowObj;
    private Market MarketDic;
    private int DicNum;

    //private int DayBuyDamiondCount;
    private int DayCoinTreeCount;
    private int BuyCounts;

	// Use this for initialization
	void Start () {
        //if (GameObject.Find("RoleWindow") != null || GameObject.Find("GachaWindow") != null || GameObject.Find("StrengEquipWindow"))
        //{
        //    BuyGoldWindowObj.layer = 11;
        //}
        //else
        //{
        //    BuyGoldWindowObj.layer = 5;
        //}
        BuyGoldWindowObj.layer = 11;
        NetworkHandler.instance.SendProcess("1104#");
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                //UIManager.instance.BackUI()
                //if (GameObject.Find("TaskWindow") != null)
                //{
                //    UIManager.instance.DestroyPanel(this.gameObject.name);
                //}
                //else
                //{
                //    UIManager.instance.BackUI();
                //}
                //UIManager.instance.DestroyPanel(this.gameObject.name);
              //  DestroyImmediate(this.gameObject);
                UIManager.instance.BackUI();
            };
        }
        if (UIEventListener.Get(OneButton).onClick == null)
        {
            UIEventListener.Get(OneButton).onClick += delegate(GameObject go)
            {
                if (BuyCounts >= DayCoinTreeCount)
                {
                    UIManager.instance.OpenPromptWindow("达到上限", PromptWindow.PromptType.Hint, null, null);
                }
                else 
                {
                    CharacterRecorder.instance.BuyGold = CharacterRecorder.instance.gold;
                    NetworkHandler.instance.SendProcess("1103#1");
                    Debug.Log("给钱给钱，买一次了");
                }
                //CharacterRecorder.instance.BuyGold = CharacterRecorder.instance.gold;
                //NetworkHandler.instance.SendProcess("1103#1");
                //Debug.Log("给钱给钱，买一次了");
            };
        }
        if (UIEventListener.Get(TenButton).onClick == null)
        {
            UIEventListener.Get(TenButton).onClick += delegate(GameObject go)
            {
                if (DayCoinTreeCount - BuyCounts < 10 && DayCoinTreeCount - BuyCounts > 0)
                {
                    //UIManager.instance.OpenPromptWindow("次数不足", PromptWindow.PromptType.Hint, null, null);
                    CharacterRecorder.instance.BuyGold = CharacterRecorder.instance.gold;
                    NetworkHandler.instance.SendProcess(string.Format("1103#{0};", DayCoinTreeCount - BuyCounts));
                }
                else if (BuyCounts >= DayCoinTreeCount)
                {
                    UIManager.instance.OpenPromptWindow("达到上限", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    CharacterRecorder.instance.BuyGold = CharacterRecorder.instance.gold;
                    NetworkHandler.instance.SendProcess("1103#10");
                    Debug.Log("给钱给钱，买10次了");
                }
            };
        }
        if (UIEventListener.Get(GameObject.Find("MaskButton")).onClick == null) 
        {
            UIEventListener.Get(GameObject.Find("MaskButton")).onClick += delegate(GameObject go)
            {
               // DestroyImmediate(this.gameObject);
                UIManager.instance.BackUI();
            };
        }
	}
    public void GetCostDiamond(int BuyCount,int BuyCostDiamond)
    {
        //this.DayBuyDamiondCount = TextTranslator.instance.GetVipDicByID(CharacterRecorder.instance.Vip).DayBuyStaminaCount;
        this.DayCoinTreeCount = TextTranslator.instance.GetVipDicByID(CharacterRecorder.instance.Vip).DayCoinTreeCount;
        this.BuyCounts = BuyCount;
        CostNumber.text = "(今日已用" + BuyCount.ToString() + "/" + DayCoinTreeCount.ToString() + ")";

        CostDiamond.text = BuyCostDiamond.ToString();
        int Level=CharacterRecorder.instance.level;
        if(Level<=15)
        {
            GetGold.text = "30000";
        }
        else if(Level>15&&Level<=20)
        {
            GetGold.text = "32000";
        }
        else if(Level>20&&Level<=30)
        {
            GetGold.text = "35000";
        }
        else if (Level > 30 && Level <= 40) 
        {
            GetGold.text = "38000";
        }
        else if (Level > 40 && Level <= 49) 
        {
            GetGold.text = "41000";
        }
        else if (Level >=50 && Level <= 59)
        {
            GetGold.text = "44000";
        }
        else if (Level >=60&& Level <= 69)
        {
            GetGold.text = "47000";
        }
        else if (Level >= 70 && Level <= 80)
        {
            GetGold.text = "50000";
        }
        //if (BuyCount >= 20 && BuyCount < 50)
        //{
        //    BuyCount = 20;
        //}
        //else if (BuyCount == 0)
        //{
        //    BuyCount = 1;
        //}
        //else if (BuyCount >= 50 && BuyCount < 9999)
        //{
        //    BuyCount = 21;
        //}
        //else if (BuyCount > 9999)
        //{
        //    BuyCount = 22;
        //}
        //MarketDic = TextTranslator.instance.GetMarketByID(BuyCount);
        //CostDiamond.text = MarketDic.GoldPrice.ToString();

        //string[] data = (MarketDic.GoldNum).Split('$');
        //GetGold.text = (int.Parse(data[0]) + int.Parse(data[1]) * CharacterRecorder.instance.level).ToString();//CharacterRecorder.instance.Vip;
        
    }

    public void GetMarket(int MarketID, int StaminaNum)
    {
        
        if (MarketID >= 20 && MarketID < 50)
        {
            MarketID = 20;
        }
        else if (MarketID == 0)
        {
            MarketID = 1;
        }
        else if (MarketID >= 50 && MarketID < 9999)
        {
            MarketID = 21;
        }
        else if (MarketID > 9999)
        {
            MarketID = 22;
        }
        MarketDic = TextTranslator.instance.GetMarketByID(MarketID);
        //CostDiamond.text = MarketDic.GoldPrice.ToString();
        CostDiamond.text = StaminaNum.ToString();
        //string[] data = (MarketDic.GoldNum).Split('$');
        //GetGold.text = (int.Parse(data[0]) + int.Parse(data[1]) * CharacterRecorder.instance.level).ToString();
        int NowLevel = CharacterRecorder.instance.level;
        //GetGold.text = (int.Parse(data[0]) + (int.Parse(data[1]) * NowLevel)).ToString();
    }
}
