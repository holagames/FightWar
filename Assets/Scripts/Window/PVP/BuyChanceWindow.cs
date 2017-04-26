using UnityEngine;
using System.Collections;

public class BuyChanceWindow : MonoBehaviour {

    public UILabel CostNumber;
    public UILabel CostDiamond;
    //public UILabel BuyLabel;
    public GameObject UseButton;
    public GameObject CloseButton;
    // Use this for initialization
    void Start()
    {
        //NetworkHandler.instance.SendProcess("6013#");
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                //UIManager.instance.BackUI();
                UIManager.instance.DestroyPanel("BuyChanceWindow");
            };
        }
        if (UIEventListener.Get(UseButton).onClick == null)
        {
            UIEventListener.Get(UseButton).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("6014#");
            };
        }
    }

    public void GetCostDiamond(int BuyCount,int DiamondCost)
    {
        CostNumber.text = "(可购买次数" + BuyCount.ToString()+")";
        CostDiamond.text = DiamondCost.ToString();  
    }
}
