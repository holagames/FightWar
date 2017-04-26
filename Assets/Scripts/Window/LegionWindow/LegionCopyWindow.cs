using UnityEngine;
using System.Collections;

public class LegionCopyWindow : MonoBehaviour 
{
    public static int curGateGroupID;
    public GameObject doubltButton;
    public GameObject DoubltObjCloseButton;
    public GameObject DoubltObj;
    [SerializeField]
    private GameObject legionCopyItem;
    [SerializeField]
    private GameObject uiGride;
    public GameObject legionRankButton;
    public GameObject addButton;
    public GameObject leftButton;
    public GameObject rightButton;
    public UICenterOnChild myUICenterOnChild;
    public UILabel leftFightTimesLabel;
    private int buyTimes = 0;
	// Use this for initialization
	void Start () 
    {
        UIManager.instance.CountSystem(UIManager.Systems.军团副本);
        UIManager.instance.UpdateSystems(UIManager.Systems.军团副本);

        NetworkHandler.instance.SendProcess("8201#;");
       // SetLegionCopyWindow(TextTranslator.instance.LegionGateList);
        if (UIEventListener.Get(doubltButton).onClick == null)
        {
            UIEventListener.Get(doubltButton).onClick += delegate(GameObject go)
            {
                DoubltObj.SetActive(true);
            };
        }
        if (UIEventListener.Get(DoubltObjCloseButton).onClick == null)
        {
            UIEventListener.Get(DoubltObjCloseButton).onClick += delegate(GameObject go)
            {
                DoubltObj.SetActive(false);
            };
        }
        if (UIEventListener.Get(leftButton).onClick == null)
        {
            UIEventListener.Get(leftButton).onClick += delegate(GameObject go)
            {
                int curIndex = int.Parse(myUICenterOnChild.centeredObject.name);
                Debug.Log("curIndex。。。" + curIndex);
                if (curIndex > 1 + 1)
                {
                    curIndex -= 1;
                    myUICenterOnChild.CenterOn(uiGride.transform.GetChild(curIndex - 1));
                }
            };
        }
        if (UIEventListener.Get(rightButton).onClick == null)
        {
            UIEventListener.Get(rightButton).onClick += delegate(GameObject go)
            {
                Debug.Log("rightButton。。。");
                int curIndex = int.Parse(myUICenterOnChild.centeredObject.name);
                if (curIndex < uiGride.transform.childCount - 1)
                {
                    curIndex += 1;
                    myUICenterOnChild.CenterOn(uiGride.transform.GetChild(curIndex - 1));
                }
            };
        }
        if (UIEventListener.Get(legionRankButton).onClick == null)
        {
            UIEventListener.Get(legionRankButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPanel("LegionSecondWindow", true);
                LegionSecondWindow.enterFromLegionCopy = true;
            };
        }
        if (UIEventListener.Get(addButton).onClick == null)
        {
            UIEventListener.Get(addButton).onClick += delegate(GameObject go)
            {
                Market _myMarket = TextTranslator.instance.GetMarketByBuyCount(buyTimes + 1);
                int costDiomond = _myMarket == null ? 50 : _myMarket.LegionGateBuyPrice;
                UIManager.instance.OpenPromptWindow(string.Format("是否花费{0}钻石购买一次\n军团副本挑战次数？", costDiomond), PromptWindow.PromptType.Confirm, ConfirmBuy, null);
                //UIManager.instance.OpenPromptWindow("即将开放", PromptWindow.PromptType.Alert, null, null);
            };
        }
	}



    void SetLegionCopyWindow(BetterList<LegionGate> _fubenItemList)
    {
        DestroyGride();
        for (int i = 0; i < _fubenItemList.size; i++)
        {
            if (uiGride.transform.FindChild(_fubenItemList[i].GateGroupID.ToString()) == null)
            {
                GameObject obj = NGUITools.AddChild(uiGride, legionCopyItem);
                obj.name = _fubenItemList[i].GateGroupID.ToString();
                obj.GetComponent<LegionCopyItem>().SetLegionCopyItem(_fubenItemList[i]);
            }
        }
        uiGride.GetComponent<UIGrid>().Reposition();
        
    }
    public void SetLegionCopyWindow(int leftFightTimes, int GateGroupID,int buyTimes)
    {
        this.buyTimes = buyTimes;
        curGateGroupID = GateGroupID;
        leftFightTimesLabel.text = string.Format("{0}/{1}",leftFightTimes,3);
        SetLegionCopyWindow(TextTranslator.instance.LegionGateList);
        Invoke("DelayCenterOn", 0.2f);
    }
    public void ResetLegionFightTimes(int leftFightTimes,int buyTimes)
    {
        this.buyTimes = buyTimes;
        //leftFightTimesLabel.text = string.Format("{0}/{1}", leftFightTimes, 3 + buyTimes);
        leftFightTimesLabel.text = string.Format("{0}/{1}", leftFightTimes, 3);
    }
    void DelayCenterOn()
    {
        int curIndex = curGateGroupID < 2 ? 2 : curGateGroupID;//curIndex = 2;
        myUICenterOnChild.CenterOn(uiGride.transform.GetChild(curIndex - 1));
    }
    void DestroyGride()
    {
        for (int i = uiGride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGride.transform.GetChild(i).gameObject);
        }
    }
    void ConfirmBuy()
    {
        NetworkHandler.instance.SendProcess("8308#;");
    }
}
