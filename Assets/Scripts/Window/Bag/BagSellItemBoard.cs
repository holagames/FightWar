using UnityEngine;
using System.Collections;

public class BagSellItemBoard : MonoBehaviour
{
    public UISlider UiSlider;
    public GameObject ItemIcon;
    public GameObject maxButton;
    public UILabel ItemName;
    public UILabel ItemCount;
    public UILabel SellCount;
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;
    public UILabel LabelCostMoney;
    int SoloSellMoney;
    int curItemID = 0;
    private int maxCount;
    private int sellCount = 1;
    private bool isDragToChangeSlider = true;
    void OnEnable()
    {
        curItemID = 0;
        sellCount = 1;
        maxCount = 99;
        SoloSellMoney = 10;
        UiSlider.value = 0;
    }
    // Use this for initialization
    void Start()
    {
        //if (UIEventListener.Get(GameObject.Find("OneButton")).onClick == null)
        //{
        //    UIEventListener.Get(GameObject.Find("OneButton")).onClick += delegate(GameObject go)
        //    {
        //        SellCount.text = "1";
        //    };
        //}

        if (UIEventListener.Get(GameObject.Find("CutButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("CutButton")).onClick += delegate(GameObject go)
            {
               /* if (SellCount.text == "1")
                {
                    SellCount.text = "1";
                }
                else
                {
                    SellCount.text = (int.Parse(SellCount.text) - 1).ToString();
                    LabelCostMoney.text = (SoloSellMoney * int.Parse(SellCount.text)).ToString();
                }*/
                ClickCutButton();
            };
        }

        if (UIEventListener.Get(GameObject.Find("AddButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("AddButton")).onClick += delegate(GameObject go)
            {
              /*  if (SellCount.text == ItemCount.text)
                {
                    SellCount.text = ItemCount.text;
                }
                else
                {
                    SellCount.text = (int.Parse(SellCount.text) + 1).ToString();
                    LabelCostMoney.text = (SoloSellMoney * int.Parse(SellCount.text)).ToString();
                }*/
                ClickAddButton();
            };
        }

        if (UIEventListener.Get(maxButton).onClick == null)
        {
            UIEventListener.Get(maxButton).onClick += delegate(GameObject go)
            {
                SellCount.text = ItemCount.text;
                LabelCostMoney.text = (SoloSellMoney * int.Parse(SellCount.text)).ToString();
            };
        }

        if (UIEventListener.Get(GameObject.Find("SureButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("SureButton")).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("5003#" + curItemID + ";" + SellCount.text + ";");
                this.gameObject.SetActive(false);
            };
        }

        //if (UIEventListener.Get(GameObject.Find("CancelButton")).onClick == null)
        //{
        //    UIEventListener.Get(GameObject.Find("CancelButton")).onClick += delegate(GameObject go)
        //    {
        //        this.gameObject.SetActive(false);
        //    };
        //}

        if (UIEventListener.Get(GameObject.Find("SellCloseButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("SellCloseButton")).onClick += delegate(GameObject go)
            {
                this.gameObject.SetActive(false);
            };
        }

    }
    void ClickCutButton()
    {
        isDragToChangeSlider = false;
        if (SellCount.text == "1")
        {
            sellCount = 1;
            SellCount.text = "1";
            LabelCostMoney.text = (sellCount * SoloSellMoney).ToString();
            UiSlider.value = ((float)sellCount / maxCount);

        }
        else
        {
            sellCount -= 1;
            SellCount.text = (int.Parse(SellCount.text) - 1).ToString();
            LabelCostMoney.text = (sellCount * SoloSellMoney).ToString();
            UiSlider.value = ((float)sellCount / maxCount);
        }
        isDragToChangeSlider = true;
    }
    void ClickAddButton()
    {
        isDragToChangeSlider = false;
        if (SellCount.text == maxCount.ToString())
        {
            sellCount = maxCount;
            SellCount.text = maxCount.ToString();
            LabelCostMoney.text = (sellCount * SoloSellMoney).ToString();
            UiSlider.value = ((float)sellCount / maxCount);
        }
        else
        {
            sellCount += 1;
            SellCount.text = (int.Parse(SellCount.text) + 1).ToString();
            LabelCostMoney.text = (sellCount * SoloSellMoney).ToString();
            UiSlider.value = ((float)sellCount / maxCount);
        }
        isDragToChangeSlider = true;
    }
    public void OnSliderValueChanged()
    {
        if (isDragToChangeSlider == false)
        {
            return;
        }
        if (UiSlider.value == 0)
        {
            sellCount = 1;
            UiSlider.value = ((float)1 / maxCount);
            SellCount.text = sellCount.ToString();
            LabelCostMoney.text = (sellCount * SoloSellMoney).ToString();
        }
        else if (UiSlider.value == 1)
        {
            sellCount = maxCount;
            UiSlider.value = ((float)sellCount / maxCount);
            SellCount.text = sellCount.ToString();
            LabelCostMoney.text = (sellCount * SoloSellMoney).ToString();
        }
        else
        {
            sellCount = (int)(UiSlider.value * maxCount);
            SellCount.text = sellCount.ToString();
            LabelCostMoney.text = (sellCount * SoloSellMoney).ToString();
        }
    }
    public void SetInfo(int itemid, int _SoloSellMoney)
    {
        curItemID = itemid;
        SoloSellMoney = _SoloSellMoney;
        LabelCostMoney.text = _SoloSellMoney.ToString();
        TextTranslator.ItemInfo miteminfo = TextTranslator.instance.GetItemByItemCode(itemid);
        SetColor(ItemIcon, miteminfo.itemGrade);
        if (miteminfo.itemCode.ToString()[0] == '5')
        {
            ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = miteminfo.itemCode.ToString().Substring(0, 4) + "1";
        }
        else if (miteminfo.itemCode.ToString()[0] == '7')
        {
            ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
            ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = (miteminfo.itemCode - 10000).ToString();
        }
        else
        {
            ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            ItemIcon.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = miteminfo.itemCode.ToString();
        }
        ItemName.text = miteminfo.itemName;
        maxCount = TextTranslator.instance.GetItemCountByID(itemid);
        ItemCount.text = TextTranslator.instance.GetItemCountByID(itemid).ToString();
        SellCount.text = "1";
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
