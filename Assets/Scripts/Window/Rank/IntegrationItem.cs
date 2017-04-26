using UnityEngine;
using System.Collections;

public class IntegrationItem : MonoBehaviour
{
    public GameObject SpriteWhite;
    public GameObject SpriteGreed;
    public GameObject SpriteBlue;
    public GameObject FinshBG;
    public GameObject FailBG;
    public GameObject SpriteGetIcon;
    public UIAtlas AtlasItem;
    public UIAtlas NormalAtlas;
    public UIAtlas CommonAtlas;
    //public UIAtlas ItemAtlas;
    public int _Layer;

    bool Switch;
    int _GetLayer;
    int Integration;
    string[] StrSplit;

    private bool isPress = false;
    private float datatime = 0;
    private int ItemId;
    private int ItemIdWhite;
    private int ItemIdGreed;
    private int ItemIdBlue;
    private int ItemCount;
    GameObject ItemObj;
    private int goldNum;
    public UIAtlas RoleAtlas;
    public int Grablayer = 0;
    void Start()
    {
        //Integration = int.Parse(GameObject.Find("IntegrationWindow").GetComponent<IntegrationWindow>().Integration.GetComponent<UILabel>().text);
        if (UIEventListener.Get(this.gameObject).onClick == null)
        {
            UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
            {
                if (Integration < _Layer && Switch == false)
                {
                    UIManager.instance.OpenPromptWindow("积分不足！", PromptWindow.PromptType.Hint, null, null);
                }
                else if (Switch == true)
                {
                    UIManager.instance.OpenPromptWindow("已经领取过！", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    if (CharacterRecorder.instance.GrabIntegrationOpen)
                    {
                        Debug.LogError("发送夺宝协议");
                        NetworkHandler.instance.SendProcess(string.Format("1410#{0};", Grablayer));
                    }
                    else
                    {
                        NetworkHandler.instance.SendProcess(string.Format("6010#{0};", _Layer / 2));
                    }
                }
            };
        }
    }

    public void Init(PvpPointShop _PvpPointShop, int GetLayer)//PvpPointShop _PvpPointShop//int Layer, string StrItem, int GetLayer
    {
        //_Layer = Layer*2;
        //_GetLayer = GetLayer;
        int Layer = _PvpPointShop.PvpPointShopID;
        _Layer = _PvpPointShop.PvpPointShopID * 2;
        _GetLayer = GetLayer;
        goldNum = _PvpPointShop.Gold;
        Integration = int.Parse(GameObject.Find("IntegrationWindow").GetComponent<IntegrationWindow>().Integration.GetComponent<UILabel>().text);

        SpriteWhite.GetComponent<UISprite>().spriteName = "Grade3";
        SpriteWhite.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = "90003";
        SpriteWhite.transform.FindChild("Number").GetComponent<UILabel>().text = _PvpPointShop.RYCoin.ToString();
        SpriteWhite.GetComponent<ItemExplanation>().SetAwardItem(90003, 0);

        SpriteGreed.GetComponent<UISprite>().spriteName = "Grade5";
        SpriteGreed.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = "90001";
        SpriteGreed.transform.FindChild("Number").GetComponent<UILabel>().text = _PvpPointShop.Gold.ToString();
        SpriteGreed.GetComponent<ItemExplanation>().SetAwardItem(90001, 0);

        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(_PvpPointShop.itemID);
        SpriteBlue.GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
        if (_PvpPointShop.itemID > 70000 && _PvpPointShop.itemID < 79999)
        {
            SpriteBlue.transform.FindChild("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
            SpriteBlue.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = (_PvpPointShop.itemID - 10000).ToString();
            SpriteBlue.transform.FindChild("Number").GetComponent<UILabel>().text = _PvpPointShop.itemNum.ToString();
            SpriteBlue.GetComponent<ItemExplanation>().SetAwardItem(_PvpPointShop.itemID, 0);
        }
        else
        {
            SpriteBlue.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = _PvpPointShop.itemID.ToString();
            SpriteBlue.transform.FindChild("Number").GetComponent<UILabel>().text = _PvpPointShop.itemNum.ToString();
            SpriteBlue.GetComponent<ItemExplanation>().SetAwardItem(_PvpPointShop.itemID, 0);
        }
        if (Layer == 1)
        {
            FinshBG.transform.Find("JianTou1").gameObject.SetActive(false);
            FailBG.transform.Find("JianTou2").gameObject.SetActive(false);
        }
        if (Layer > GetLayer)//判断当前层级是否大于已领层级
        {
            FinshBG.transform.FindChild("LabelNum").GetComponent<UILabel>().text = _Layer.ToString();
            //this.gameObject.transform.FindChild("SpriteFinsh").GetComponent<UISprite>().spriteName = "read0";
            Switch = false;
            if (Integration >= _Layer)
            {
                SpriteGetIcon.SetActive(true);
                this.gameObject.GetComponent<UISprite>().atlas = NormalAtlas;
                this.gameObject.GetComponent<UISprite>().spriteName = "yuandi30";
                //this.gameObject.transform.FindChild("SpriteFinsh").GetComponent<UISprite>().spriteName = "PVPIntegration2";
                //this.gameObject.transform.FindChild("SpriteFinsh").GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
            }
        }
        else
        {
            FinshBG.SetActive(false);
            FailBG.SetActive(true);

            if (GameObject.Find("SpriteLeft/FailBG") != null)
            {
                FailBG.transform.FindChild("LabelNum").GetComponent<UILabel>().text = _Layer.ToString();
                this.gameObject.transform.FindChild("SpriteFinsh").GetComponent<UISprite>().spriteName = "PVPIntegration1";
                this.gameObject.transform.FindChild("SpriteFinsh").GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
                Switch = true;
            }
        }
    }

    //夺宝积分显示
    public void InitInfo(IndianaPoint IndianaPointDic, int GetLayer)
    {
        Grablayer = IndianaPointDic.ID;
        _Layer = IndianaPointDic.Point;
        _GetLayer = GetLayer;
        Integration = int.Parse(GameObject.Find("IntegrationWindow").GetComponent<IntegrationWindow>().Integration.GetComponent<UILabel>().text);
        SpriteWhite.SetActive(true);
        SpriteBlue.SetActive(true);
        SpriteGreed.SetActive(true);
        //if (IndianaPointDic.ItemID1 == 0)
        //{
        //    SpriteWhite.SetActive(false);
        //}
        //if (IndianaPointDic.ItemID2 == 0)
        //{
        //    SpriteGreed.SetActive(false);
        //}
        //if (IndianaPointDic.ItemID3 == 0)
        //{
        //    SpriteBlue.SetActive(false);
        //}
        ItemShowInfo(IndianaPointDic.ItemID1, IndianaPointDic.ItemNum1, SpriteWhite);
        ItemShowInfo(IndianaPointDic.ItemID3, IndianaPointDic.ItemNum3, SpriteBlue);
        ItemShowInfo(IndianaPointDic.ItemID2, IndianaPointDic.ItemNum2, SpriteGreed);
        if (Grablayer == 1)
        {
            FinshBG.transform.Find("JianTou1").gameObject.SetActive(false);
            FailBG.transform.Find("JianTou2").gameObject.SetActive(false);
        }
        if (Grablayer > GetLayer)//判断当前层级是否大于已领层级
        {
            FinshBG.transform.FindChild("LabelNum").GetComponent<UILabel>().text = _Layer.ToString();
            Switch = false;
            if (Integration >= _Layer)
            {
                SpriteGetIcon.SetActive(true);
                this.gameObject.GetComponent<UISprite>().atlas = NormalAtlas;
                this.gameObject.GetComponent<UISprite>().spriteName = "yuandi30";
            }
        }
        else
        {
            FinshBG.SetActive(false);
            FailBG.SetActive(true);

            if (GameObject.Find("SpriteLeft/FailBG") != null)
            {
                FailBG.transform.FindChild("LabelNum").GetComponent<UILabel>().text = _Layer.ToString();
                this.gameObject.transform.FindChild("SpriteFinsh").GetComponent<UISprite>().spriteName = "PVPIntegration1";
                this.gameObject.transform.FindChild("SpriteFinsh").GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
                Switch = true;
            }
        }
    }
    void ItemShowInfo(int id, int number, GameObject item)
    {
        if (id != 0)
        {
            item.SetActive(true);
            if (id > 70000 && id < 79999)
            {
                item.transform.FindChild("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
                item.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(id).picID.ToString();
                item.transform.FindChild("Framge").gameObject.SetActive(true);
            }
            else if (id >= 80000 && id < 90000)
            {
                item.transform.FindChild("Icon").GetComponent<UISprite>().atlas = AtlasItem;
                item.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(id).picID.ToString();
                item.transform.FindChild("Framge").gameObject.SetActive(true);
            }
            else
            {
                item.transform.FindChild("Icon").GetComponent<UISprite>().atlas = AtlasItem;
                item.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(id).picID.ToString();
            }
            item.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(id).itemGrade.ToString();
            item.transform.FindChild("Number").GetComponent<UILabel>().text = number.ToString();
            item.GetComponent<ItemExplanation>().SetAwardItem(id, 0);
        }
        else
        {
            item.SetActive(false);
        }
    }
    public void SetInfo() //领取成功时显示成功
    {
        FinshBG.SetActive(false);
        FailBG.SetActive(true);
        SpriteGetIcon.SetActive(false);
        this.gameObject.GetComponent<UISprite>().atlas = CommonAtlas;
        this.gameObject.GetComponent<UISprite>().spriteName = "di3";
        if (this.gameObject.transform.FindChild("SpriteLeft/FailBG") != null)
        {
            FailBG.transform.FindChild("LabelNum").GetComponent<UILabel>().text = _Layer.ToString();
            this.gameObject.transform.FindChild("SpriteFinsh").GetComponent<UISprite>().spriteName = "PVPIntegration1";
            this.gameObject.transform.FindChild("SpriteFinsh").GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
            Switch = true;
        }
        CharacterRecorder.instance.AddMoney(goldNum);
        GameObject.Find("IntegrationWindow/TopContent").GetComponent<TopContent>().Reset();
    }

    void Color(int ItemCode, int ItemNum)
    {

    }
}
