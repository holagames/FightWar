using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyWishWindow : MonoBehaviour
{

    public UILabel activityTime;
    public UILabel laveTime;

    public UIGrid wishGrid;
    public UIGrid treasureGrid;

    public GameObject receiveBtn;

    public GameObject rechargeBtn;
    public GameObject chongZhiInfo;

    public GameObject wishPrefab;

    public UILabel wishCount;

    public GameObject helpBtn;
    public GameObject helpInfo;

    public GameObject ExitBtn;

    public UIAtlas RoleAtlas;
    public UIAtlas ItemAtlas;

    private int activityLaveTime = 1000;
    /// <summary>
    /// 是否可以许愿
    /// </summary>
    private bool isWish = true;
    /// <summary>
    /// 
    /// </summary>
    private bool isReceive = false;
    /// <summary>
    /// 记录当日允许许愿次数
    /// </summary>
    private int laveWishCount = 0;
    /// <summary>
    /// 记录许愿的对象（即：许的什么愿）
    /// </summary>
    private GameObject wishObj;

    private List<GameObject> wishGridList = new List<GameObject>();

    private List<GameObject> treasureGridList = new List<GameObject>();

    void Awake()
    {
        for (int i = 0; i < wishGrid.transform.childCount; i++)
        {
            wishGridList.Add(wishGrid.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < treasureGrid.transform.childCount; i++)
        {
            treasureGridList.Add(treasureGrid.transform.GetChild(i).gameObject);
        }
    }

    // Use this for initialization  9721 
    void Start()
    {
        NetworkHandler.instance.SendProcess("9721#;");


        if (UIEventListener.Get(ExitBtn).onClick == null)
        {
            UIEventListener.Get(ExitBtn).onClick += delegate(GameObject go)
            {
                DestroyImmediate(this.gameObject);
            };
        }
        if (UIEventListener.Get(helpBtn).onClick == null)
        {
            UIEventListener.Get(helpBtn).onClick += delegate(GameObject go)
            {
                helpInfo.SetActive(true);
            };
        }
        GameObject helpExit = helpInfo.transform.Find("All/ExitBtn").gameObject;
        if (UIEventListener.Get(helpExit).onClick == null)
        {
            UIEventListener.Get(helpExit).onClick += delegate(GameObject go)
            {
                helpInfo.SetActive(false);
            };
        }

    }
    /// <summary>
    /// 接收9722返回的信息
    /// </summary>
    /// <param name="data"></param>
    public void ReceiveMsg_9722(string[] data)
    {
        //设定心愿成功
        if (data[0] == "1")
        {
            laveWishCount--;
            wishCount.text = string.Format("今日次数:{0}/5", laveWishCount);
            if (wishObj != null)
            {
                GameObject isWishObj = wishObj.transform.FindChild("IsWish").gameObject;
                isWishObj.SetActive(true);
            }
            string[] data1 = data[1].Split('!');
            SetTreasureInfo(data1);

            if (laveWishCount <= 0)
            {
                GameObject main = GameObject.Find("MainWindow");
                if (main != null)
                {
                    CharacterRecorder.instance.IsWishRedpoint = false;
                    main.GetComponent<MainWindow>().OpenWishActivity(CharacterRecorder.instance.IsWishOpen, CharacterRecorder.instance.IsWishRedpoint);
                }
            }

            //NetworkHandler.instance.SendProcess("9721#;");
        }
        else
        {

        }
        isWish = true;
    }
    /// <summary>
    /// 接收到9723协议的返回的信息
    /// </summary>
    /// <param name="dataSplit"></param>
    public void ReceiveMsg_9723(string[] dataSplit)
    {
        //领取心愿成功 
        if (dataSplit[0] == "1")
        {
            string[] data = dataSplit[1].Split('!');

            List<Item> itemList = new List<Item>();
            itemList.Clear();

            for (int i = 0; i < data.Length - 1; i++)
            {
                string[] data1 = data[i].Split('$');
                Item item = new Item(int.Parse(data1[0]), int.Parse(data1[1]));
                itemList.Add(item);
            }
            UIManager.instance.OpenSinglePanel("AdvanceWindow", false);
            GameObject advan = GameObject.Find("AdvanceWindow");
            advan.layer = 11;
            AdvanceWindow aw = advan.GetComponent<AdvanceWindow>();
            aw.SetInfo(AdvanceWindowType.GainResult, null, null, null, null, itemList);
            isReceive = false;
            NetworkHandler.instance.SendProcess("9721#;");
        }
        else
        {
            isReceive = true;
            UIManager.instance.OpenPromptWindow("领取心愿奖励失败！", PromptWindow.PromptType.Hint, null, null);
        }
    }
    /// <summary>
    /// 接收9721协议的返回信息
    /// </summary>
    /// <param name="dataSplit"></param>
    public void ReceiveMsg_9721(string[] dataSplit)
    {
        string[] data = Utils.GetTime(dataSplit[0]).ToShortDateString().Split('/');
        string[] data1 = Utils.GetTime(dataSplit[1]).ToShortDateString().Split('/');
        string start = data[2] + "年" + data[0] + "月" + data[1] + "日";
        string end = data1[2] + "年" + data1[0] + "月" + data1[1] + "日";
        string time = string.Format("{0}-{1}", start, end);
        //活动时间
        activityTime.text = time;

        activityLaveTime = int.Parse(dataSplit[2]);
        if (!IsInvoking("SetActivityTime"))
        {
            InvokeRepeating("SetActivityTime", 0, 1.0f);
        }

        //许愿池  
        string[] wishItemInfo = dataSplit[4].Split('!');
        SetWishInfo(wishItemInfo);
        //奖励池
        DestryReceiveItemInfo();
        if (dataSplit[3] != null)
        {
            string[] treasureItemInfo = dataSplit[3].Split('!');
            SetTreasureInfo(treasureItemInfo);
        }

        //设置是否可以领取奖励
        if (int.Parse(dataSplit[5]) == 1)
        {
            //receiveBtn.GetComponent<BoxCollider>().enabled = true;
            isReceive = true;
        }
        else
        {
            isReceive = false;
            receiveBtn.GetComponent<UISprite>().spriteName = "buttonHui";
            //receiveBtn.GetComponent<BoxCollider>().enabled = false;
        }
        if (UIEventListener.Get(receiveBtn).onClick == null)
        {
            UIEventListener.Get(receiveBtn).onClick += delegate(GameObject go)
            {
                if (isReceive)
                {
                    NetworkHandler.instance.SendProcess("9723#;");
                    isReceive = false;
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("没有奖励可以领取.", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        //设置今日是否已经充值
        if (int.Parse(dataSplit[6]) == 1)
        {
            rechargeBtn.SetActive(false);
            chongZhiInfo.SetActive(true);
        }
        else
        {
            //rechargeBtn.SetActive(true); kino
            UIEventListener.Get(rechargeBtn).onClick = delegate(GameObject go)
            {
                UIManager.instance.OpenPanel("VIPShopWindow", true);
            };
            chongZhiInfo.SetActive(false);
        }
        //显示今日可许愿次数
        if (int.Parse(dataSplit[7]) <= 0)
        {
            isWish = false;
        }
        else
        {
            isWish = true;
        }
        laveWishCount = int.Parse(dataSplit[7]);
        wishCount.text = string.Format("今日次数:{0}/5", laveWishCount);
    }
    /// <summary>
    /// 设置许愿池的物品信息  4 
    /// </summary>
    /// <param name="wish"> </param>
    void SetWishInfo(string[] wish)
    {
        DestryWishItemInfo();
        for (int i = 0; i < wish.Length - 1; i++)
        {
            string[] wishData = wish[i].Split('$');
            GameObject go = NGUITools.AddChild(wishGrid.gameObject, wishPrefab);
            go.SetActive(true);
            //许愿的编号
            go.name = wishData[0];
            wishGridList.Add(go);
            //添加点击事件
            UIEventListener.Get(go).onClick = delegate(GameObject go1)
            {
                SetWishBtn_OnClick(go1);
            };
            SetItemInfo(0, go, wishData[0], wishData[1]);
        }
        wishGrid.Reposition();
    }
    /// <summary>
    /// 设置许愿的对象
    /// </summary>
    /// <param name="go1">对象</param>
    private void SetWishBtn_OnClick(GameObject go1)
    {
        if (laveWishCount <= 0)
        {
            UIManager.instance.OpenPromptWindow("当日许愿次数已达到上限!", PromptWindow.PromptType.Hint, null, null);
            return;
        }
        if (go1.transform.FindChild("IsWish").gameObject.activeSelf)
        {
            UIManager.instance.OpenPromptWindow("相同的愿望只能许一次.", PromptWindow.PromptType.Hint, null, null);
            return;
        }
        if (isWish && !isReceive)
        {
            wishObj = go1;
            NetworkHandler.instance.SendProcess(string.Format("9722#{0};", go1.name));
        }
        else
        {
            if (!isWish)
            {
                UIManager.instance.OpenPromptWindow("当日许愿次数已达到上限!", PromptWindow.PromptType.Hint, null, null);
            }
            else if (isReceive)
            {
                UIManager.instance.OpenPromptWindow("先领取奖励,才可以许愿.", PromptWindow.PromptType.Hint, null, null);
            }
        }
    }

    /// <summary>
    /// 设置奖励池中的物品信息   3
    /// </summary>
    /// <param name="treasure"></param>
    void SetTreasureInfo(string[] treasure)
    {
        DestryReceiveItemInfo();
        for (int i = 0; i < treasure.Length - 1; i++)
        {
            string[] data = treasure[i].Split('$');
            GameObject go = NGUITools.AddChild(treasureGrid.gameObject, wishPrefab);
            go.SetActive(true);
            go.name = data[0];
            treasureGridList.Add(go);
            SetItemInfo(1, go, data[0], data[1]);
        }
        treasureGrid.Reposition();
    }
    /// <summary>
    /// 设置物品的Info
    /// </summary>
    /// <param name="isTrue">哪一个物品，1是wish  0是treasure</param>
    /// <param name="info"></param>
    void SetItemInfo(int isTrue, GameObject go, string info0, string info1)
    {
        TextTranslator.WishInfo wishInfo = TextTranslator.instance.GetWishInfobyWishID(int.Parse(info0));
        if (wishInfo == null)
        {
            return;
        }

        int ItemCode = wishInfo.ItemID;
        TextTranslator.instance.ItemDescription(go, ItemCode, 0);

        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
        if (_ItemInfo != null)
        {
            go.GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
        }
        //显示物品的框
        //Debug.LogError("wishInfo:: " + ItemCode);

        if (isTrue == 0)//许愿池  4
        {
            if (info1 == "1")//已经许愿
            {
                GameObject isWishObj = go.transform.Find("IsWish").gameObject;
                isWishObj.SetActive(true);
            }
            else//没有许愿
            {
                GameObject isWishObj = go.transform.Find("IsWish").gameObject;
                isWishObj.SetActive(false);
            }
        }
        else if (isTrue == 1)//奖励池  3
        {
            //显示暴击
            GameObject Crit = go.transform.Find("CritLabel").gameObject;
            Crit.SetActive(true);
            Crit.GetComponent<UILabel>().text = string.Format("暴击x{0}", info1);
        }
        //Debug.LogError("itemCount  00  ::" + 556);
        //显示物品的数量
        go.transform.Find("CountLabel").GetComponent<UILabel>().text = wishInfo.ItemNum.ToString();

        //Debug.LogError("itemCount::" + itemCount);
        //显示物品的Icon图标
        UISprite spriteIcon = go.transform.Find("Icon").GetComponent<UISprite>();
        //碎片是否显示
        GameObject suiPian = go.transform.Find("SuiPianSprite").gameObject;

        if (ItemCode.ToString()[0] == '4')
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            suiPian.SetActive(false);
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
        else if (ItemCode.ToString()[0] == '7')
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
    }

    /// <summary>
    /// 更新活动剩余的时间
    /// </summary>
    void SetActivityTime()
    {
        if (activityLaveTime > 0)
        {
            System.TimeSpan ts = new System.TimeSpan(0, 0, System.Convert.ToInt32(activityLaveTime));

            //int day = activityLaveTime / (24 * 3600);
            //int hour = activityLaveTime % 24 / 3600;
            //int min = activityLaveTime % 3600 / 60;
            //int sec = activityLaveTime % 60;
            if (ts.Days <= 0)
            {
                laveTime.text = string.Format("{0}:{1}:{2}", ts.Hours.ToString("00"), ts.Minutes.ToString("00"), ts.Seconds.ToString("00"));
            }
            else
            {
                laveTime.text = string.Format("{0}天-{1}:{2}:{3}", ts.Days, ts.Hours.ToString("00"), ts.Minutes.ToString("00"), ts.Seconds.ToString("00"));
            }
            activityLaveTime--;
        }
        else
        {
            laveTime.text = string.Format("{0}秒", 0);
        }
    }
    /// <summary>
    /// 销毁许愿池内的物品
    /// </summary>
    void DestryWishItemInfo()
    {
        for (int i = 0; i < wishGridList.Count; i++)
        {
            DestroyImmediate(wishGridList[i]);
        }
        wishGridList.Clear();
        wishGrid.Reposition();
    }
    /// <summary>
    /// 销毁奖励池中的物品
    /// </summary>
    void DestryReceiveItemInfo()
    {
        for (int i = 0; i < treasureGridList.Count; i++)
        {
            DestroyImmediate(treasureGridList[i]);
        }
        treasureGridList.Clear();
        treasureGrid.Reposition();
    }


}
