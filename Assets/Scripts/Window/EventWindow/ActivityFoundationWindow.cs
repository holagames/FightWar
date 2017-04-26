using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActivityFoundationWindow : MonoBehaviour
{
    public GameObject PayMoneyButton;
    public GameObject BuyButton;
    public GameObject FoundationButton;
    public GameObject BenefitButton;
    public GameObject HadButton;
    public GameObject BuyMessageWindow;
    public GameObject Item;
    public GameObject FoundaitonGrid;
    public UILabel BuyNumberLabel;
    public List<GameObject> ItemList = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        NetworkHandler.instance.SendProcess("9608#;");
        UIEventListener.Get(PayMoneyButton).onClick += delegate(GameObject go)
        {
            UIManager.instance.OpenPanel("VIPShopWindow", true);
        };
        UIEventListener.Get(BuyButton).onClick += delegate(GameObject go)
        {
            BuyMessageWindow.SetActive(true);
            ActivityFoundationBuyWindowInfo();
        };
        UIEventListener.Get(FoundationButton).onClick += delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("9608#;");
        };
        UIEventListener.Get(BenefitButton).onClick += delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("9610#;");
        };
        if (CharacterRecorder.instance.Vip >= 2 && CharacterRecorder.instance.isBuyTheFoundation == false)
        {
            BuyButton.transform.Find("RedMessage").gameObject.SetActive(true);
        }
    }

    void ActivityFoundationBuyWindowInfo()
    {
        UIEventListener.Get(BuyMessageWindow.transform.Find("Window/SureButton").gameObject).onClick += delegate(GameObject go)
        {
            BuyMessageWindow.SetActive(false);
            if (CharacterRecorder.instance.lunaGem >= 980 && CharacterRecorder.instance.Vip >= 2)
            {
                NetworkHandler.instance.SendProcess("9607#;");
            }
            else
            {
                UIManager.instance.OpenPromptWindow("VIP等级or钻石不足，请充值", PromptWindow.PromptType.Hint, null, null);
                UIManager.instance.OpenPanel("VIPShopWindow", true);
            }

        };
        UIEventListener.Get(BuyMessageWindow.transform.Find("Window/CancleButton").gameObject).onClick += delegate(GameObject go)
        {
            BuyMessageWindow.SetActive(false);
        };
        UIEventListener.Get(BuyMessageWindow.transform.Find("Window/CloseButton").gameObject).onClick += delegate(GameObject go)
        {
            BuyMessageWindow.SetActive(false);
        };
    }

    public void ItemInfo(int BuyType, int BuyNumber, List<int> ItemType, int OpenType)
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            DestroyImmediate(ItemList[i]);
        }
        ItemList.Clear();
        BuyButton.SetActive(false);
        HadButton.SetActive(false);
        if (BuyType == 1)
        {
            HadButton.SetActive(true);
        }
        else
        {
            BuyButton.SetActive(true);
        }
        int length1 = TextTranslator.instance.GetActivityGrowthFundDicLengthByID(1);
        int length2 = TextTranslator.instance.GetActivityGrowthFundDicLengthByID(2);
        ActivityGrowthFund item;
        BuyNumberLabel.text = BuyNumber.ToString();
        for (int i = 1; i < ItemType.Count + 1; i++)
        {
            GameObject go = Instantiate(Item) as GameObject;
            go.SetActive(true);
            go.transform.parent = FoundaitonGrid.transform;
            go.transform.localScale = new Vector3(1, 1, 1);
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.name = i.ToString();
            if (OpenType == 1)
            {
                item = TextTranslator.instance.GetActivityGrowthFundDicByID(i);
                go.GetComponent<FoundationItem>().SetInfo(item.ItemID, item.ItemNum, item.Des, ItemType[i - 1], i, BuyNumber, OpenType);
            }
            else
            {
                item = TextTranslator.instance.GetActivityGrowthFundDicByID(length1 + i);
                go.GetComponent<FoundationItem>().SetInfo(item.ItemID, item.ItemNum, item.Des, ItemType[i - 1], length1 + i, BuyNumber, OpenType);
            }

            ItemList.Add(go);
            if (i % 2 == 0)
            {
                go.transform.localPosition = new Vector3(70 * (i - 2), -144, 0);
            }
            else
            {
                go.transform.localPosition = new Vector3(70 * (i - 1), 0, 0);
            }

        }
        if (CharacterRecorder.instance.isFoundationPoint && CharacterRecorder.instance.isBuyTheFoundation)
        {
            FoundationButton.transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else
        {
            FoundationButton.transform.Find("RedPoint").gameObject.SetActive(false);
        }
        if (CharacterRecorder.instance.isBenifPoint)
        {
            BenefitButton.transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else
        {
            BenefitButton.transform.Find("RedPoint").gameObject.SetActive(false);
        }
    }

}
