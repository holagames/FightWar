using UnityEngine;
using System.Collections;

public class BuyPowerWindow : MonoBehaviour {

    public UILabel CostNumber;
    public UILabel CostDiamond;
    public GameObject UseButton;
    public GameObject CloseButton;
    public GameObject BuyPowerWindowObj;
    private Market MarketDic;
    private int DicNum;
    public UILabel BuyLabel;
    private int DayBuyStaminaCount;
    private int BuyCounts;
	// Use this for initialization
	void Start () {
        if (GameObject.Find("RoleWindow") != null || GameObject.Find("GachaWindow") != null)
        {
            BuyPowerWindowObj.layer = 11;
        }
        else
        {
            BuyPowerWindowObj.layer = 5;
        }
        NetworkHandler.instance.SendProcess("1102#");
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
              /*  if (GameObject.Find("TaskWindow") != null)
                {
                    UIManager.instance.DestroyPanel(this.gameObject.name);
                }
                else 
                {
                    UIManager.instance.BackUI();
                }*/
            };
        }
        if (UIEventListener.Get(UseButton).onClick == null)
        {
            UIEventListener.Get(UseButton).onClick += delegate(GameObject go)
            {
                if (BuyCounts >= DayBuyStaminaCount)
                {
                    UIManager.instance.OpenPromptWindow("达到上限", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    NetworkHandler.instance.SendProcess("1101#1");
                    Debug.Log("掏钱买体力 哇卡卡");
                }
            };
        }
        if (UIEventListener.Get(GameObject.Find("MaskButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("MaskButton")).onClick += delegate(GameObject go)
            {
                //DestroyImmediate(this.gameObject);
               /* if (GameObject.Find("TaskWindow") != null)
                {
                    UIManager.instance.DestroyPanel(this.gameObject.name);
                }
                else
                {
                    UIManager.instance.BackUI();
                }*/
                UIManager.instance.BackUI();
            };
        }
 
	}
    public void GetCostDiamond(int BuyCount)
    {
        //this.DayBuyStaminaCount = TextTranslator.instance.GetVipDicByID(CharacterRecorder.instance.Vip).DayBuyStaminaCount;
        this.BuyCounts = BuyCount;

        CostNumber.text = "(今日已用" + BuyCount.ToString() + "/"+DayBuyStaminaCount.ToString()+")";

        int MarketID = BuyCount+1;
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

        Market MarkItem = TextTranslator.instance.GetMarketByID(MarketID);
        CostDiamond.text = MarkItem.StaminaPrice.ToString();
        BuyLabel.text = "[-][89CEF3]购买[ffffff]" + MarkItem.StaminaNum + "[89CEF3]点体力力需[-]";

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
        //CostDiamond.text = MarketDic.StaminaPrice.ToString();
        //BuyLabel.text = "[-][89CEF3]购买[ffffff]" + MarketDic.StaminaNum + "[89CEF3]点体力力需[-]";
    }
}
