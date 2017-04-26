using UnityEngine;
using System.Collections;

public class FirstRechargeWindow : MonoBehaviour
{
    public GameObject SpriteAward1;
    public GameObject SpriteAward2;
    public GameObject SpriteAward3;
    public GameObject SpriteAward4;

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

	void Start () {
        UIManager.instance.CountActivitys(UIManager.Activitys.首充);
        UIManager.instance.UpdateActivitys(UIManager.Activitys.首充);

        parent = GameObject.Find(UI_ROOT);
        if (UIEventListener.Get(GameObject.Find("CloseButton")).onClick == null) 
        {
            UIEventListener.Get(GameObject.Find("CloseButton")).onClick += delegate(GameObject go)
            {
                //UIManager.instance.BackUI();
                NetworkHandler.instance.SendProcess("9201#;");
                Destroy(this.gameObject);
            };
        }


        string StrAward = TextTranslator.instance.GetVipDicByID(1).VipGift;
        string[] dataSplit = StrAward.Split('!');



        UIEventListener.Get(SpriteAward1).onPress += delegate(GameObject go, bool isPressed)
        {
            string[] ItemSplit = dataSplit[0].Split('$');

            AwardObj = SpriteAward1;
            ItemId = int.Parse(ItemSplit[0]);
            ItemCount = int.Parse(ItemSplit[1]);
            OnPress(isPressed);
        };
        UIEventListener.Get(SpriteAward2).onPress += delegate(GameObject go, bool isPressed)
        {
            string[] ItemSplit = dataSplit[1].Split('$');
            AwardObj = SpriteAward2;
            ItemId = 90007;
            ItemCount = 0;
            OnPress(isPressed);
        };
        UIEventListener.Get(SpriteAward3).onPress += delegate(GameObject go, bool isPressed)
        {
            string[] ItemSplit = dataSplit[2].Split('$');
            AwardObj = SpriteAward3;
            ItemId = 10002;
            ItemCount = 0;
            OnPress(isPressed);
        };
        UIEventListener.Get(SpriteAward4).onPress += delegate(GameObject go, bool isPressed)
        {
            string[] ItemSplit = dataSplit[3].Split('$');
            AwardObj = SpriteAward4;
            ItemId = 90001;
            ItemCount = 0;
            OnPress(isPressed);
        };
        SetVipOneState();
	}

    void Update()
    {
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
                isPress = false;
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

    public void SetInfo() 
    {
        GameObject.Find("SpriteLibao").transform.Find("Labelgive").GetComponent<UILabel>().text = "6480";
        GameObject.Find("SpriteLibao").transform.Find("Labelbuy").GetComponent<UILabel>().text = "2880";
    }

    public void  SetVipOneState()
    {
        Vip vipOne = TextTranslator.instance.GetVipDicByID(1);
        if (CharacterRecorder.instance.Vip<1)//CharacterRecorder.instance.PayDiamond < vipOne.TradingDiamondCount
        {
            BuyButton.SetActive(true);
            TakeButton.SetActive(false);
            HaveButton.SetActive(false);
            UIEventListener.Get(BuyButton).onClick = delegate(GameObject go)
            {
                //UIManager.instance.OpenSinglePanel("VIPShopWindow", false);
                UIManager.instance.OpenPanel("VIPShopWindow", true);
            };
        }
        else if (CharacterRecorder.instance.BuyGiftBag[0] == "0") 
        {
            BuyButton.SetActive(false);
            TakeButton.SetActive(true);
            HaveButton.SetActive(false);
            UIEventListener.Get(TakeButton).onClick = delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess(string.Format("5013#{0};{1};", 22001, 1));
            };
        }
        else if (CharacterRecorder.instance.BuyGiftBag[0] == "1")
        {
            BuyButton.SetActive(false);
            TakeButton.SetActive(false);
            HaveButton.SetActive(true);           
        }
    }

    public void RefreshItem()//购买礼包成功后刷新
    {
        NetworkHandler.instance.SendProcess("5002#22001;1;");
    }
}
