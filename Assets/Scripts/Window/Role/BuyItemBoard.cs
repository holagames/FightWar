using UnityEngine;
using System.Collections;

public class BuyItemBoard : MonoBehaviour
{
    public UISlider UiSlider;
    public UISprite priceTypeSprite;
    public GameObject ItemIcon;
    public UILabel ItemName;
    //public UILabel ItemCount;
    public UILabel EXPLabel;
    public UILabel SellCountLabel;
    public UILabel needSumMomeyLabel;
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;
    //[SerializeField]
    //private GameObject CutButton;
    //[SerializeField]
    //private GameObject AddButton;
    //[SerializeField]
    //private GameObject MaxButton;
    //[SerializeField]
    //private GameObject SureButton;
    //[SerializeField]
    //private GameObject CloseButton;


    [SerializeField]
    private GameObject CutTenButton;
    [SerializeField]
    private GameObject CutOneButton;
    [SerializeField]
    private GameObject AddOneButton;
    [SerializeField]
    private GameObject AddTenButton;
    [SerializeField]
    private GameObject SureButton;
    [SerializeField]
    private GameObject CloseButton;


    public int curItemID = 0;
    private int sellCount = 1;
    private int maxCount = 99;
    private int perPrice = 10;
    private bool isDragToChangeSlider = true;
    void OnEnable()
    {
        curItemID = 0;
        sellCount = 1;
        maxCount = 99;
        perPrice = 10;
        UiSlider.value = 0;
    }
    // Use this for initialization
    void Start()
    {
       /* if (UIEventListener.Get(GameObject.Find("MinButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("MinButton")).onClick += delegate(GameObject go)
            {
                sellCount = 1;
                SellCountLabel.text = "1";
                needSumMomeyLabel.text = (sellCount * perPrice).ToString();
                UiSlider.value = ((float)1 / maxCount);
            };
        }
        if (UIEventListener.Get(GameObject.Find("MaxButtonNew")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("MaxButtonNew")).onClick += delegate(GameObject go)
            {
                sellCount = maxCount;
                SellCountLabel.text = maxCount.ToString();
                needSumMomeyLabel.text = (sellCount * perPrice).ToString();
                UiSlider.value = 1f;
            };
        }*/

        //if (UIEventListener.Get(GameObject.Find("OneButton")).onClick == null)
        //{
        //    UIEventListener.Get(GameObject.Find("OneButton")).onClick += delegate(GameObject go)
        //    {
        //        SellCount.text = "1";
        //    };
        //}

/*        if (UIEventListener.Get(CutButton).onClick == null)
        {
            UIEventListener.Get(CutButton).onClick += delegate(GameObject go)
            {
                isDragToChangeSlider = false;
                if (SellCountLabel.text == "1")
                {
                    sellCount = 1;
                    SellCountLabel.text = "1";
                    needSumMomeyLabel.text = (sellCount * perPrice).ToString();
                    UiSlider.value = ((float)sellCount / maxCount);
                    
                }
                else
                {
                    sellCount -= 1;
                    SellCountLabel.text = (int.Parse(SellCountLabel.text) - 1).ToString();
                    needSumMomeyLabel.text = (sellCount * perPrice).ToString();
                    //UiSlider.value -= 1.0f/(float)maxCount;
                    UiSlider.value = ((float)sellCount / maxCount);
                }
                isDragToChangeSlider = true;
            };
        }

        if (UIEventListener.Get(AddButton).onClick == null)
        {
            UIEventListener.Get(AddButton).onClick += delegate(GameObject go)
            {
                isDragToChangeSlider = false;
                if (SellCountLabel.text == maxCount.ToString())
                {
                    sellCount = maxCount;
                    SellCountLabel.text = maxCount.ToString();
                    needSumMomeyLabel.text = (sellCount * perPrice).ToString();
                    UiSlider.value = ((float)sellCount / maxCount);
                }
                else
                {
                    sellCount += 1;
                    SellCountLabel.text = (int.Parse(SellCountLabel.text) + 1).ToString();
                    needSumMomeyLabel.text = (sellCount * perPrice).ToString();
                    //UiSlider.value += 1.0f / (float)maxCount;
                    UiSlider.value = ((float)sellCount / maxCount);
                }
                isDragToChangeSlider = true;
            };
        }

        if (UIEventListener.Get(MaxButton).onClick == null)
        {
            UIEventListener.Get(MaxButton).onClick += delegate(GameObject go)
            {
                sellCount = maxCount;
                SellCountLabel.text = maxCount.ToString();
                needSumMomeyLabel.text = (sellCount * perPrice).ToString();
            };
        }

        if (UIEventListener.Get(SureButton).onClick == null)
        {
            UIEventListener.Get(SureButton).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("5013#" + curItemID + ";" + SellCountLabel.text + ";");
                this.gameObject.SetActive(false);
            };
        }

        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                this.gameObject.SetActive(false);
            };
        }

*/

        if (UIEventListener.Get(CutTenButton).onClick == null)
        {
            UIEventListener.Get(CutTenButton).onClick += delegate(GameObject go)
            {
                if (sellCount > 10)
                {
                    sellCount = sellCount - 10;
                }
                else 
                {
                    sellCount = 1;
                }
                SellCountLabel.text = sellCount.ToString();
                needSumMomeyLabel.text = (sellCount * perPrice).ToString();
            };
        }

        if (UIEventListener.Get(CutOneButton).onClick == null)
        {
            UIEventListener.Get(CutOneButton).onClick += delegate(GameObject go)
            {
                if (sellCount > 1)
                {
                    sellCount = sellCount - 1;
                }
                else
                {
                    sellCount = 1;
                }
                SellCountLabel.text = sellCount.ToString();
                needSumMomeyLabel.text = (sellCount * perPrice).ToString();
            };
        }

        if (UIEventListener.Get(AddOneButton).onClick == null)
        {
            UIEventListener.Get(AddOneButton).onClick += delegate(GameObject go)
            {
                if (sellCount<maxCount)
                {
                    sellCount = sellCount + 1;
                }
                else
                {
                    sellCount = maxCount;
                }
                SellCountLabel.text = sellCount.ToString();
                needSumMomeyLabel.text = (sellCount * perPrice).ToString();
            };
        }

        if (UIEventListener.Get(AddTenButton).onClick == null)
        {
            UIEventListener.Get(AddTenButton).onClick += delegate(GameObject go)
            {
                if (sellCount <= maxCount-10)
                {
                    sellCount = sellCount + 10;
                }
                else
                {
                    sellCount = maxCount;
                }
                SellCountLabel.text = sellCount.ToString();
                needSumMomeyLabel.text = (sellCount * perPrice).ToString();
            };
        }

        if (UIEventListener.Get(SureButton).onClick == null)
        {
            UIEventListener.Get(SureButton).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("5013#" + curItemID + ";" + sellCount + ";");
                this.gameObject.SetActive(false);
            };
        }

        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                this.gameObject.SetActive(false);
            };
        }
    }


    public void SetBuyItemBoardInfo(int itemid)
    {
        curItemID = itemid;
        switch(itemid)
        {
            case 10001: EXPLabel.text = "EXP+100"; break;
            case 10002: EXPLabel.text = "EXP+300"; break;
            case 10003: EXPLabel.text = "EXP+1000"; break;
            case 10004: EXPLabel.text = "EXP+3000"; break;
            case 10005: EXPLabel.text = "EXP+10000"; break;
            case 10006: EXPLabel.text = "EXP+30000"; break;
            default: EXPLabel.text = ""; break;
        }
        TextTranslator.ItemInfo miteminfo = TextTranslator.instance.GetItemByItemCode(itemid);
        SetPriceAndMaxCount(miteminfo);
        priceTypeSprite.spriteName = "icon" + miteminfo.sellType.ToString();
        SetColor(ItemIcon, miteminfo.itemGrade);
        UISprite _IconSprite = ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>();
        if (miteminfo.itemCode.ToString()[0] == '5')
        {
            _IconSprite.atlas = ItemAtlas;
            _IconSprite.spriteName = miteminfo.itemCode.ToString().Substring(0, 4) + "1";
        }
        else if (miteminfo.itemCode.ToString()[0] == '7')
        {
            _IconSprite.atlas = RoleAtlas;
            _IconSprite.spriteName = (miteminfo.itemCode - 10000).ToString();
        }
        else
        {
            _IconSprite.atlas = ItemAtlas;
            _IconSprite.spriteName = miteminfo.itemCode.ToString();
        }
        ItemName.text = miteminfo.itemName;
        //ItemCount.text = TextTranslator.instance.GetItemCountByID(itemid).ToString()+"个";
        needSumMomeyLabel.text = (sellCount * perPrice).ToString();
        SellCountLabel.text = sellCount.ToString();
    }
    void SetPriceAndMaxCount(TextTranslator.ItemInfo miteminfo)
    {
        int myMoney = 0;
        switch (miteminfo.sellType)
        {
            case 1: myMoney = CharacterRecorder.instance.gold; break;
            case 2: myMoney = CharacterRecorder.instance.lunaGem; break;
        }
        perPrice = miteminfo.sellPrice;
        //Debug.LogError("perPrice.." + miteminfo.sellPrice);
        int canBuyCount = myMoney / perPrice;
        if (canBuyCount < maxCount)
        {
            maxCount = canBuyCount;
        }
    }
    public void OnSliderValueChanged()
    {
        if(isDragToChangeSlider == false)
        {
            return;
        }
        if (UiSlider.value == 0)
        {
            sellCount = 1;
            UiSlider.value = ((float)1 / maxCount);
            SellCountLabel.text = sellCount.ToString();
            needSumMomeyLabel.text = (sellCount * perPrice).ToString();
        }
        else if (UiSlider.value == 1)
        {
            sellCount = maxCount;
            UiSlider.value = ((float)sellCount / maxCount);
            SellCountLabel.text = sellCount.ToString();
            needSumMomeyLabel.text = (sellCount * perPrice).ToString();
        }
        else
        {
            sellCount = (int)(UiSlider.value * maxCount);
            SellCountLabel.text = sellCount.ToString();
            needSumMomeyLabel.text = (sellCount * perPrice).ToString();
        }
    }
    void SetColor(GameObject go, int color)
    {
        switch (color)
        {
            case 1:
                go.GetComponent<UISprite>().spriteName = "Grade1";
                break;
            case 2:
                go.GetComponent<UISprite>().spriteName = "Grade2";
                break;
            case 3:
                go.GetComponent<UISprite>().spriteName = "Grade3";
                break;
            case 4:
                go.GetComponent<UISprite>().spriteName = "Grade4";
                break;
            case 5:
                go.GetComponent<UISprite>().spriteName = "Grade5";
                break;
        }
    }
}
