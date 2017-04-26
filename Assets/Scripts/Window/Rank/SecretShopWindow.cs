using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SecretShopWindow: MonoBehaviour
{
    public static string SelectShopType = "10006";
    public GameObject MyGrid;
    public GameObject MyRankShopItem;
    public UILabel HonerNumber;
    public UILabel labelLeftTime;
    public UILabel labelNextRefreshCost;

    [HideInInspector]
    public List<GameObject> ListRankShopItem = new List<GameObject>();
    [SerializeField]
    private List<GameObject> shopTypeList = new List<GameObject>();
    [SerializeField]
    private GameObject buttonRefresh;
   // [SerializeField]
   // private GameObject buttonRefresh;
    private int leftTime = 0;
    private int refreshCost = 0;
    void OnEnable()
    {
        NetworkHandler.instance.SendProcess("5102#10006;");
    }
    // Use this for initialization
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.黑市);
        UIManager.instance.UpdateSystems(UIManager.Systems.黑市);

        for (int i = 0; i < shopTypeList.Count; i++)
        {
            UIEventListener.Get(shopTypeList[i]).onClick = ClickShopTypeButton;
        }

        UIEventListener.Get(buttonRefresh).onClick = ClickRefreshButton;
    }
    public void SetShopWindow(int _leftTime,int _nextRefreshCost,List<ShopItemData> _ShopItemList,int NowMoney)
    {
        DestroyGride();
        leftTime = _leftTime;
        refreshCost = _nextRefreshCost;
        labelNextRefreshCost.text = _nextRefreshCost.ToString();
        HonerNumber.text = NowMoney.ToString() + " 金条,用于黑市的购买";
        CancelInvoke();
        InvokeRepeating("UpdateTime",0,1.0f);
        for (int i = 0; i < _ShopItemList.Count;i++ )
        {
            GameObject go = NGUITools.AddChild(MyGrid, MyRankShopItem);
            go.GetComponent<RankShopItem>().Init(_ShopItemList[i]);
            ListRankShopItem.Add(go);
        }
        MyGrid.GetComponent<UIGrid>().Reposition();
    }

    public void SetGoldBarReset() //重置金条
    {
        HonerNumber.text = CharacterRecorder.instance.GoldBar.ToString() + " 金条,用于黑市的购买";
    }
    void DestroyGride()
    {
        ListRankShopItem.Clear();
        for (int i = MyGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(MyGrid.transform.GetChild(i).gameObject);
        }
    }
    void UpdateTime()
    {
        if (leftTime > 0)
        {
            string houreStr = (leftTime / 3600).ToString("00");
            string miniteStr = (leftTime % 3600 / 60).ToString("00");
            string secondStr = (leftTime % 60).ToString("00");
            //labelLeftTime.text = ((leftTime / 60)).ToString() + ":" + (leftTime % 60).ToString();
            labelLeftTime.text = string.Format("{0}:{1}:{2}", houreStr, miniteStr, secondStr);
            leftTime -= 1;
        }
    }
    private void ClickShopTypeButton(GameObject go)
    {
        if (go != null && SelectShopType != go.name)
        {
            SelectShopType = go.name;
            NetworkHandler.instance.SendProcess(string.Format("5102#{0};", SelectShopType));
        }
    }

    private void ClickRefreshButton(GameObject go)
    {
        if (go != null)
        {
            UIManager.instance.OpenPromptWindow(string.Format("是否消耗{0}{1}刷新？", refreshCost, "钻石"), PromptWindow.PromptType.Confirm, ConfirmResfresh, null);
        }
    }
    private void ConfirmResfresh()
    {
        NetworkHandler.instance.SendProcess("5105#10006;");
    }
}
