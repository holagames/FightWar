using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrabWindow : MonoBehaviour
{
    public GameObject ProtectButton;
    public GameObject ProtectTime;
    public GameObject ProtectMessage;
    public GameObject LeftObjItem;
    public List<Getgemlist> Getgemlist = new List<Getgemlist>();
    //弹出的界面Obj
    public GameObject PopGrabResultObj;
    public GameObject PopDetailsObj;
    public GameObject PopProtectFightObj;
    public GameObject PopProtectfightShopObj;
    public GameObject PopWindowObj;

    public List<FragmentItemData> FragmentItemData = new List<FragmentItemData>();
    //
    private List<TextTranslator.ItemInfo> GrabList = new List<TextTranslator.ItemInfo>();
    private List<int> GrabIdList = new List<int>();
    private List<GameObject> ItemList = new List<GameObject>();
    private int NowNumber = 0;
    private int BigNumber = 0;
    private int SmallNumber = 0;
    public int AllProtectTime = 0;
    public int ProtectFixedTime = 0;
    private int ProtectID = 0;

    private int ListIndex = 0;
    public string GetItemId;

    public List<Item> FinishItemList;

    public bool isTwokey = false;
    public bool isThreekey = false;
    public bool isFourkey = false;
    public bool isFivekey = false;
    public bool isSixkey = false;
    // Use this for initialization
    public GameObject NewBieObJ;
    public GameObject IntegralButton;
    void Start()
    {
        UpdateList();
        if (CharacterRecorder.instance.OnceSuceessID == 0)
        {
            GrabItemButton(ItemList[0]);
        }
        else
        {
            for (int i = 0; i < ItemList.Count; i++)
            {
                if (ItemList[i] != null)
                {
                    if (ItemList[i].GetComponent<GrabItem>().ItemInfo.itemCode == CharacterRecorder.instance.OnceSuceessID)
                    {
                        GrabItemButton(ItemList[i]);
                        ItemList[i].GetComponent<GrabItem>().Bg.SetActive(true);
                    }
                    else
                    {
                        ItemList[i].GetComponent<GrabItem>().Bg.SetActive(false);
                    }
                }
            }
        }
        NetworkHandler.instance.SendProcess("1407#");
        NetworkHandler.instance.SendProcess("1409#;");
        UIEventListener.Get(IntegralButton).onClick = delegate(GameObject go)
        {
            Debug.LogError("积分界面打开");
            NetworkHandler.instance.SendProcess("1409#;");
            CharacterRecorder.instance.GrabIntegrationOpen = true;
            GameObject IntWindow=UIManager.instance.OpenPanel("IntegrationWindow", true);
            IntWindow.GetComponent<IntegrationWindow>().GrabIntegrationInfo();
        };
    }
    //刷新抢夺列表
    public void UpdateList()
    {
        // ShowSetItem(CharacterRecorder.instance.ClickItemID);
        GetBagItemList();
        EventLisTener();
        GrabItemlist();
    }
    /// <summary>
    /// 合成结束刷新界面
    /// </summary>
    public void SetUpdate()
    {
        GetBagItemList();
        GrabItemlist();
        GrabItemButton(ItemList[ListIndex]);
    }

    /// <summary>
    /// 生成可抢夺物品列表
    /// </summary>
    public void GrabItemlist()
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            DestroyImmediate(ItemList[i]);
        }
        ItemList.Clear();
        //int Temp;
        for (int i = 0; i < TextTranslator.instance.itemSortList.Count; i++)
        {
            for (int j = 0; j < GrabIdList.Count; j++)
            {
                if (GrabIdList[j] == TextTranslator.instance.itemSortList[i].ItemID)
                {
                    GameObject obj = Instantiate(LeftObjItem) as GameObject;
                    obj.transform.parent = GameObject.Find("LeftObj").transform.Find("ScrollView").transform.Find("Grid");
                    obj.transform.localScale = Vector3.one;
                    obj.transform.localPosition = Vector3.zero;
                    obj.SetActive(true);
                    obj.transform.GetComponent<GrabItem>().GrabNumber = TextTranslator.instance.GetItemByItemCode(GrabIdList[j]).itemGrade;
                    obj.transform.Find("ItemIcon").GetComponent<UISprite>().spriteName = GrabIdList[j].ToString();
                    if (GrabIdList[j] == 51030)
                    {
                        NewBieObJ = obj;
                    }
                    obj.transform.GetComponent<GrabItem>().ItemInfo = TextTranslator.instance.GetItemByItemCode(GrabIdList[j]);
                    obj.transform.Find("Sprite").GetComponent<UISprite>().spriteName = "Grade" + (TextTranslator.instance.GetItemByItemCode(GrabIdList[j]).itemGrade);
                    obj.transform.Find("RedMessgae").gameObject.SetActive(false);
                    ItemList.Add(obj);
                    UIEventListener.Get(obj).onClick = GrabItemButton;
                    break;
                }
            }
        }
        foreach (GameObject item in ItemList)
        {
            RedMessageIsShow(item);
        }
        DestoryOtherItem();
        GameObject.Find("LeftObj").transform.Find("ScrollView").transform.Find("Grid").GetComponent<UIGrid>().repositionNow = true;
    }

    //碎片满后加入list（测试输出用）
    public List<Item> GetFinishItem(List<Item> GrabList)
    {
        List<Item> FinishList = new List<Item>();
        int num = 0;
        foreach (Item item in GrabList)
        {

            for (int i = 0; i < item.itemGrade; i++)
            {
                for (int j = 0; j < GrabList.Count; j++)
                {
                    if (item.itemCount != 0)
                    {
                        if (item.itemCode % 10 == 0)
                        {
                            if (item.itemCode + i == GrabList[j].itemCode)
                            {
                                if (num == GrabList[j].itemCode % 10)
                                {
                                    num++;
                                }
                            }
                        }
                    }
                }
            }
            if (num == item.itemGrade)
            {
                for (int j = 0; j < GrabList.Count; j++)
                {
                    if (item.itemCount != 0 && GrabList[j].itemCount != 0)
                    {
                        if (item.itemCode + 1 == GrabList[j].itemCode)
                        {
                            isTwokey = true;
                        }
                        else if (item.itemCode + 2 == GrabList[j].itemCode)
                        {
                            isThreekey = true;
                        }
                        else if (item.itemCode + 3 == GrabList[j].itemCode)
                        {
                            isFourkey = true;
                        }
                        else if (item.itemCode + 4 == GrabList[j].itemCode)
                        {
                            isFivekey = true;
                        }
                        else if (item.itemCode + 5 == GrabList[j].itemCode)
                        {
                            isSixkey = true;
                        }
                    }
                }
                switch (item.itemGrade)
                {
                    case 3:
                        if (isTwokey != false && isThreekey != false)
                        {
                            FinishList.Add(item);
                        }
                        break;
                    case 4:
                        if (isTwokey != false && isThreekey != false && isFourkey != false)
                        {
                            FinishList.Add(item);
                        }
                        break;
                    case 5:
                        if (isTwokey != false && isThreekey != false && isFourkey != false && isFivekey != false)
                        {
                            FinishList.Add(item);
                        }
                        break;
                    case 6:
                        if (isTwokey != false && isThreekey != false && isFourkey != false && isFivekey != false && isSixkey != false)
                        {
                            FinishList.Add(item);
                        }
                        break;

                }
                isTwokey = false;
                isThreekey = false;
                isFourkey = false;
                isFivekey = false;
                isSixkey = false;
                num = 0;
            }
            //num = 0;
        }
        return FinishList;
    }
    //合成后删除非预设
    public void DestoryOtherItem()
    {
        int num = 0;
        foreach (GameObject item in ItemList)
        {
            int id = int.Parse(item.transform.Find("ItemIcon").GetComponent<UISprite>().spriteName);
            List<Item> Item = TextTranslator.instance.GetAllGrabInBag();
            if (id != 51030 && id != 51040 && id != 52030 && id != 52040 && id != 53030 && id != 53040)
            {
                for (int i = 0; i < Item.Count; i++)
                {
                    for (int j = 0; j < Item[i].itemGrade; j++)
                    {
                        if (id + j + 30000 == Item[i].itemCode)
                        {
                            if (Item[i].itemCount != 0)
                            {
                                num += 1;
                                //Debug.Log("存在id:" + id + "grade:" + Item[i].itemGrade + ";j;" + j + "num:" + num + "code~:" + Item[i].itemCode + Item[i].itemName + "数量" + Item[i].itemCount);
                            }
                        }
                    }
                }
                if (num == 0)
                {
                    ItemList.Remove(item);
                    DestroyImmediate(item);
                    return;
                    //GrabItemlist();
                }
            }
            num = 0;
        }
    }

    //红点提示
    public void RedMessageIsShow(GameObject item)
    {

        if (FinishItemList.Count > 0)
        {
            for (int j = 0; j < FinishItemList.Count; j++)
            {
                item.transform.Find("RedMessgae").gameObject.SetActive(false);
            }
            for (int j = 0; j < FinishItemList.Count; j++)
            {
                if (int.Parse(item.transform.Find("ItemIcon").GetComponent<UISprite>().spriteName) == FinishItemList[j].itemCode - 30000)
                {
                    // Debug.Log("cccccc" + FinishItemList[j].itemCode + ":::" + int.Parse(item.transform.Find("ItemIcon").GetComponent<UISprite>().spriteName));
                    item.transform.Find("RedMessgae").gameObject.SetActive(true);
                }
            }
        }
        else
        {
            item.transform.Find("RedMessgae").gameObject.SetActive(false);
        }

    }

    /// <summary>
    /// 通过碎片读取合成后的宝物，去掉重复的碎片
    /// </summary>
    private void GetBagItemList()
    {
        GrabIdList.Clear();
        GrabIdList.Add(51030);
        GrabIdList.Add(51040);
        GrabIdList.Add(52030);
        GrabIdList.Add(52040);
        GrabIdList.Add(53030);
        GrabIdList.Add(53040);
        //GrabIdList.Add(59000);
        List<Item> ItemList = new List<Item>();
        ItemList = TextTranslator.instance.GetAllGrabInBag();
        FinishItemList = new List<Item>();
        FinishItemList = TextTranslator.instance.GetFinishItem(ItemList);
        //FinishItemList = GetFinishItem(ItemList);
        for (int i = 0; i < ItemList.Count; i++)
        {
            int LastNum = int.Parse((ItemList[i].itemCode).ToString().Remove(0, (ItemList[i].itemCode).ToString().Length - 1));
            int CurNum = (ItemList[i].itemCode - LastNum) - 30000;
            if (GrabIdList.Count != 0)
            {
                bool IsBool = false;
                for (int j = 0; j < GrabIdList.Count; j++)
                {
                    if (CurNum == GrabIdList[j])
                    {
                        IsBool = true;
                    }
                }
                if (!IsBool)
                {
                    GrabIdList.Add(CurNum);
                }
            }
            else
            {
                GrabIdList.Add(CurNum);
            }
        }
    }


    /// <summary>
    /// 左面有材料的宝物点击事件
    /// </summary>
    /// <param name="obj"></param>
    public void GrabItemButton(GameObject obj)
    {
        if (obj != null)
        {
            GameObject.Find("GoodsItemObj").GetComponent<GoodsItemWindow>().SetInfo(obj.GetComponent<GrabItem>().ItemInfo);
            GetItemId = obj.transform.Find("ItemIcon").GetComponent<UISprite>().spriteName;
            ShowSetItem(obj.GetComponent<GrabItem>().ItemInfo.itemCode);
        }

    }
    /// <summary>
    /// 显示选择的是那个
    /// </summary>
    public void ShowSetItem(int id)
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i] != null)
            {
                if (ItemList[i].GetComponent<GrabItem>().ItemInfo.itemCode == id)
                {
                    ListIndex = i;
                    ItemList[i].GetComponent<GrabItem>().Bg.SetActive(true);
                }
                else
                {
                    ItemList[i].GetComponent<GrabItem>().Bg.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// 添加点击事件
    /// </summary>
    private void EventLisTener()
    {
        //免战按钮
        if (UIEventListener.Get(gameObject.transform.Find("ALL/protectfight/protectButton").gameObject).onClick == null)
        {
            UIEventListener.Get(gameObject.transform.Find("ALL/protectfight/protectButton").gameObject).onClick += delegate(GameObject obj)
            {
                PopProtectFightObj.SetActive(true);
                ProtectFightPopEvent();
            };
        }
    }


    //添加选择大小免战时间的点击事件
    private void ProtectFightPopEvent()
    {
        SmallNumber = TextTranslator.instance.GetItemCountByID(10901);
        BigNumber = TextTranslator.instance.GetItemCountByID(10902);
        GameObject.Find("BigNumber").GetComponent<UILabel>().text = BigNumber.ToString();
        GameObject.Find("SmallNumber").GetComponent<UILabel>().text = SmallNumber.ToString();
        if (UIEventListener.Get(PopProtectFightObj.transform.Find("EscButton").gameObject).onClick == null)
        {
            UIEventListener.Get(PopProtectFightObj.transform.Find("EscButton").gameObject).onClick += delegate(GameObject obj)
            {
                PopProtectFightObj.SetActive(false);
            };
        }
        //使用大免战按钮
        if (UIEventListener.Get(PopProtectFightObj.transform.Find("BigProtect").gameObject).onClick == null)
        {
            UIEventListener.Get(PopProtectFightObj.transform.Find("BigProtect").gameObject).onClick += delegate(GameObject obj)
            {
                ProtectID = 10902;
                PopProtectFightObj.SetActive(false);
                //UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
                NetworkHandler.instance.SendProcess("5012#10902;");

            };
        }
        //使用小免战按钮
        if (UIEventListener.Get(PopProtectFightObj.transform.Find("SmallProtect").gameObject).onClick == null)
        {
            UIEventListener.Get(PopProtectFightObj.transform.Find("SmallProtect").gameObject).onClick += delegate(GameObject obj)
            {
                ProtectID = 10901;
                PopProtectFightObj.SetActive(false);
                //UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
                NetworkHandler.instance.SendProcess("5012#10901;");

            };
        }

    }


    //免战时间的显示
    public void SetProtectTimeInfo(int NowTime)
    {
        CancelInvoke("UpdateProtectTime");
        AllProtectTime = NowTime;
        InvokeRepeating("UpdateProtectTime", 0, 1.0f);

    }
    public void UpdateProtectTime()
    {
        if (AllProtectTime > 0)
        {
            ProtectTime.SetActive(true);
            string houreStr = (AllProtectTime / 3600).ToString("00");
            string miniteStr = (AllProtectTime % 3600 / 60).ToString("00");
            string secondStr = (AllProtectTime % 60).ToString("00");
            GameObject.Find("NowProtectTime").GetComponent<UILabel>().text = string.Format("{0}:{1}:{2}", houreStr, miniteStr, secondStr);
            ProtectButton.SetActive(false);
            ProtectMessage.SetActive(true);
            AllProtectTime -= 1;
        }
        if (AllProtectTime <= 1)
        {
            ProtectButton.SetActive(true);
            ProtectMessage.SetActive(false);
            ProtectTime.SetActive(false);
        }
    }
    //抢夺界面
    public void SetPopGrabResultObj()
    {
        if (GameObject.Find("GrabResult") == null)
        {
            PopGrabResultObj.SetActive(true);
            PopGrabResultObj.GetComponent<GrabResult>().GrabResultObj.transform.Find("ScrollView").GetComponent<UIPanel>().clipOffset = new Vector3(-20, 34, 0);
            PopGrabResultObj.GetComponent<GrabResult>().GrabResultObj.transform.Find("ScrollView").transform.localPosition = new Vector3(20, 0, 0);
        }
        if (GameObject.Find("RobberyHeroList") != null)
        {
            GameObject.Find("RobberyHeroList").SetActive(false);
        }
    }
    //购买成功or失败
    public void ShoppingSuccess(int ShopSucces)
    {
        if (ShopSucces == 1)
        {
            PopProtectfightShopObj.transform.Find("BuySucces").gameObject.SetActive(true);
        }
        else if (ShopSucces == 2)
        {
            PopProtectfightShopObj.transform.Find("LackOfMoney").gameObject.SetActive(true);
        }
        StartCoroutine(UpdateSuccessShow());
    }
    IEnumerator UpdateSuccessShow()
    {
        yield return new WaitForSeconds(1f);
        PopProtectfightShopObj.transform.Find("LackOfMoney").gameObject.SetActive(false);
        PopProtectfightShopObj.transform.Find("BuySucces").gameObject.SetActive(false);
    }
    //item字体颜色
    public string ItemNameColor(int GradeID)
    {
        string NameColor = "";
        switch (GradeID)
        {
            case 1:
                NameColor = "[-][B3B3B3]";
                break;
            case 2:
                NameColor = "[-][28DF5E]";
                break;
            case 3:
                NameColor = "[-][12A7B8]";
                break;
            case 4:
                NameColor = "[-][842DCE]";
                break;
            case 5:
                NameColor = "[-][DC582D]";
                break;
            case 6:
                NameColor = "[-][D9181E]";
                break;
        }
        return NameColor;
    }

}
