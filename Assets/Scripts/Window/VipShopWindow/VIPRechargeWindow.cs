using UnityEngine;
using System.Collections;

public class VIPRechargeWindow : MonoBehaviour {

    public GameObject SpriteAward1;
    public GameObject SpriteAward2;
    public GameObject SpriteAward3;
    public GameObject SpriteAward4;
    public GameObject closeButton;

    private bool isPress = false;
    private float datatime = 0;
    private int ItemId;
    private int ItemCount;
    private GameObject AwardObj;

    const string UI_ROOT = "UIRoot";
    private GameObject parent;


    public GameObject BuyButton;
    public GameObject TakeButton;
    public GameObject HaveButton;

    void Start()
    {
       //UIManager.instance.CountActivitys(UIManager.Activitys.活动 );
       //UIManager.instance.UpdateActivitys(UIManager.Activitys.活动);
        parent = GameObject.Find(UI_ROOT);
        if (UIEventListener.Get(closeButton).onClick == null)
        {
            UIEventListener.Get(closeButton).onClick += delegate (GameObject go)
            {
                //UIManager.instance.BackUI();
                NetworkHandler.instance.SendProcess("9201#;");//刷新
                Destroy(this.gameObject);
                print("关闭并刷新");
            };
        }


        string StrAward = TextTranslator.instance.GetVipDicByID(2).VipGift;
        string[] dataSplit = StrAward.Split('!');


        //物品介绍
        UIEventListener.Get(SpriteAward1).onPress += delegate (GameObject go, bool isPressed)
        {
            
            string[] ItemSplit = dataSplit[0].Split('$');
            AwardObj = SpriteAward1;
            ItemId = int.Parse(ItemSplit[0]);
            ItemCount = int.Parse(ItemSplit[1]);
            OnPress(isPressed);
            print("物品1");
        };
        UIEventListener.Get(SpriteAward2).onPress += delegate (GameObject go, bool isPressed)
        {
            string[] ItemSplit = dataSplit[1].Split('$');
            AwardObj = SpriteAward2;
            ItemId = int.Parse(ItemSplit[0]);
            ItemCount = int.Parse(ItemSplit[1]);
            OnPress(isPressed);
            Debug.Log("物品2");
        };
        UIEventListener.Get(SpriteAward3).onPress += delegate (GameObject go, bool isPressed)
        {
            string[] ItemSplit = dataSplit[2].Split('$');
            AwardObj = SpriteAward3;
            ItemId = int.Parse(ItemSplit[0]);
            ItemCount = int.Parse(ItemSplit[1]);
            OnPress(isPressed);
            Debug.Log("物品3");
        };
        UIEventListener.Get(SpriteAward4).onPress += delegate (GameObject go, bool isPressed)
        {
            string[] ItemSplit = dataSplit[3].Split('$');
            AwardObj = SpriteAward4;
            ItemId = int.Parse(ItemSplit[0]);
            ItemCount = int.Parse(ItemSplit[1]);
            OnPress(isPressed);
            Debug.Log("物品4");
        };
        //充值
        SetVipOneState();
    }

    void Update()
    {
        //物品介绍界面的加载
        if (isPress)
        {
            datatime += Time.deltaTime;
            if (datatime > 0.2f)
            {
                //UIManager.instance.OpenPanel("ItemExplanationWindow", false);
                GameObject go = (GameObject)Instantiate(Resources.Load("GUI/ItemExplanationWindow"));
                Transform t = go.transform;
                t.parent = parent.transform;
                t.localPosition = Vector3.zero;
                t.localRotation = Quaternion.identity;
                t.localScale = Vector3.one;
                go.layer = parent.layer;
                go.name = "ItemExplanationWindow";
                GameObject.Find("ItemExplanationWindow").GetComponent<ItemExplanationWindow>().SetItemDetail(ItemId, ItemCount, AwardObj);
                isPress =false;
            }
        }
    }

    void OnPress(bool isPressed)
    {
        if (isPressed)
        {
            isPress = true;
        }
        else
        {
            isPress = false;
            if (GameObject.Find("ItemExplanationWindow") != null)
            {
                //UIManager.instance.ClosePanel("ItemExplanationWindow");
                Destroy(GameObject.Find("ItemExplanationWindow"));
                //UIManager.instance.BackUI();
                //UIManager.instance.DestroyPanel("ItemExplanationWindow");
            }
            datatime = 0;
        }
    }

    //public void SetInfo()
    //{
    //    GameObject.Find("SpriteLibao").transform.Find("Labelgive").GetComponent<UILabel>().text = "6480";
    //    GameObject.Find("SpriteLibao").transform.Find("Labelbuy").GetComponent<UILabel>().text = "2880";
    //}

    public void SetVipOneState()
    {
        Vip vipOne = TextTranslator.instance.GetVipDicByID(1);//?
        if (CharacterRecorder.instance.Vip < 2)//CharacterRecorder.instance.PayDiamond < vipOne.TradingDiamondCount
        {
            BuyButton.SetActive(true);
            TakeButton.SetActive(false);
            HaveButton.SetActive(false);
            UIEventListener.Get(BuyButton).onClick = delegate (GameObject go)
            {
                //UIManager.instance.OpenSinglePanel("VIPShopWindow", false);
                //原代码
               // UIManager.instance.OpenPanel("VIPShopWindow", true);
                Debug.Log("购买");
                TextTranslator.instance.Recharge(7);
            };
        }
        else if (CharacterRecorder.instance.BuyGiftBag[1] == "0")
        {
            BuyButton.SetActive(false);
            TakeButton.SetActive(true);
            HaveButton.SetActive(false);
            UIEventListener.Get(TakeButton).onClick = delegate (GameObject go)
            {
                NetworkHandler.instance.SendProcess(string.Format("5013#{0};{1};", 22002, 1));
                Debug.Log("领取");
            };
        }
        else if (CharacterRecorder.instance.BuyGiftBag[1] == "1")
        {
            BuyButton.SetActive(false);
            TakeButton.SetActive(false);
            HaveButton.SetActive(true);
        }
    }

    public void RefreshItem()//购买礼包成功后刷新
    {
        NetworkHandler.instance.SendProcess("5002#22002;1;");
        Destroy(gameObject);
    }
}
