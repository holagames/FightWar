using UnityEngine;
using System.Collections;

public class SevenDayWindow : MonoBehaviour
{

    public GameObject TimeObj;
    public GameObject GeneralGoods;
    public GameObject VipGoods;
    public GameObject Oneday;
    public GameObject Twoday;
    public GameObject Threeday;
    public GameObject Fourday;
    public GameObject Fiveday;
    public GameObject Sixday;
    public GameObject Sevenday;
    public UIAtlas HeroAtlas;
    public UIAtlas itemAtlas;
    public int AllTime;
    public GameObject GeneralButton;
    public GameObject VipButton;
    public GameObject GeneralGetButton;
    public GameObject VipGetButton;
    public void SetSevendayShow(string[] dataSplit)
    {
        string[] goodsSplit = dataSplit[0].Split('!');
        ShowTime(dataSplit[1]);
        switch ((goodsSplit.Length - 1) / 2)
        {
            case 1:
                ShowBuyGeneralItem(goodsSplit[0]);
                ShowBuyVipItem(goodsSplit[1]);
                Oneday.GetComponent<UIToggle>().value = true;
                break;
            case 2:
                ShowBuyGeneralItem(goodsSplit[2]);
                ShowBuyVipItem(goodsSplit[3]);
                Twoday.GetComponent<UIToggle>().enabled = true;
                Twoday.transform.Find("Sprite").gameObject.SetActive(true);
                Twoday.GetComponent<UIToggle>().value = true;
                break;
            case 3:
                ShowBuyGeneralItem(goodsSplit[4]);
                ShowBuyVipItem(goodsSplit[5]);
                Threeday.GetComponent<UIToggle>().enabled = true;
                Threeday.transform.Find("Sprite").gameObject.SetActive(true);
                Threeday.GetComponent<UIToggle>().value = true;
                break;
            case 4:
                ShowBuyGeneralItem(goodsSplit[6]);
                ShowBuyVipItem(goodsSplit[7]);
                Fourday.GetComponent<UIToggle>().enabled = true;
                Fourday.transform.Find("Sprite").gameObject.SetActive(true);
                Fourday.GetComponent<UIToggle>().value = true;
                break;
            case 5:
                ShowBuyGeneralItem(goodsSplit[8]);
                ShowBuyVipItem(goodsSplit[9]);
                Fiveday.GetComponent<UIToggle>().enabled = true;
                Fiveday.transform.Find("Sprite").gameObject.SetActive(true);
                Fiveday.GetComponent<UIToggle>().value = true;
                break;
            case 6:
                ShowBuyGeneralItem(goodsSplit[10]);
                ShowBuyVipItem(goodsSplit[11]);
                Sixday.GetComponent<UIToggle>().enabled = true;
                Sixday.transform.Find("Sprite").gameObject.SetActive(true);
                Sixday.GetComponent<UIToggle>().value = true;
                break;
            case 7:
                ShowBuyGeneralItem(goodsSplit[12]);
                ShowBuyVipItem(goodsSplit[13]);
                Sevenday.GetComponent<UIToggle>().enabled = true;
                Sevenday.transform.Find("Sprite").gameObject.SetActive(true);
                Sevenday.GetComponent<UIToggle>().value = true;
                break;
        }
        CheckDayShow(goodsSplit);
    }
    public void CheckDayShow(string[] dataSplit)
    {
        UIEventListener.Get(Oneday).onClick += delegate(GameObject go)
        {
            ShowBuyGeneralItem(dataSplit[0]);
            ShowBuyVipItem(dataSplit[1]);
        };
        UIEventListener.Get(Twoday).onClick += delegate(GameObject go)
        {
            if ((dataSplit.Length - 1) / 2 >= 2)
            {
                Twoday.GetComponent<UIToggle>().enabled = true;
                Twoday.transform.Find("Sprite").gameObject.SetActive(true);
                Twoday.GetComponent<UIToggle>().value = true;
                ShowBuyGeneralItem(dataSplit[2]);
                ShowBuyVipItem(dataSplit[3]);
            }
            else
            {
                UIManager.instance.OpenPromptWindow("活动时间未到", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(Threeday).onClick += delegate(GameObject go)
        {
            if ((dataSplit.Length - 1) / 2 >= 3)
            {
                Threeday.GetComponent<UIToggle>().enabled = true;
                Threeday.transform.Find("Sprite").gameObject.SetActive(true);
                Threeday.GetComponent<UIToggle>().value = true;
                ShowBuyGeneralItem(dataSplit[4]);
                ShowBuyVipItem(dataSplit[5]);
            }
            else
            {
                UIManager.instance.OpenPromptWindow("活动时间未到", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(Fourday).onClick += delegate(GameObject go)
        {
            if ((dataSplit.Length - 1) / 2 >= 4)
            {
                Fourday.GetComponent<UIToggle>().enabled = true;
                Fourday.transform.Find("Sprite").gameObject.SetActive(true);
                Fourday.GetComponent<UIToggle>().value = true;
                ShowBuyGeneralItem(dataSplit[6]);
                ShowBuyVipItem(dataSplit[7]);
            }
            else
            {
                UIManager.instance.OpenPromptWindow("活动时间未到", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(Fiveday).onClick += delegate(GameObject go)
        {
            if ((dataSplit.Length - 1) / 2 >= 5)
            {
                Fiveday.GetComponent<UIToggle>().enabled = true;
                Fiveday.transform.Find("Sprite").gameObject.SetActive(true);
                Fiveday.GetComponent<UIToggle>().value = true;
                ShowBuyGeneralItem(dataSplit[8]);
                ShowBuyVipItem(dataSplit[9]);
            }
            else
            {
                UIManager.instance.OpenPromptWindow("活动时间未到", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(Sixday).onClick += delegate(GameObject go)
        {
            if ((dataSplit.Length - 1) / 2 >= 6)
            {
                Sixday.GetComponent<UIToggle>().enabled = true;
                Sixday.transform.Find("Sprite").gameObject.SetActive(true);
                Sixday.GetComponent<UIToggle>().value = true;
                ShowBuyGeneralItem(dataSplit[10]);
                ShowBuyVipItem(dataSplit[11]);
            }
            else
            {
                UIManager.instance.OpenPromptWindow("活动时间未到", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(Sevenday).onClick += delegate(GameObject go)
        {
            if ((dataSplit.Length - 1) / 2 >= 7)
            {
                Sevenday.GetComponent<UIToggle>().enabled = true;
                Sevenday.transform.Find("Sprite").gameObject.SetActive(true);
                Sevenday.GetComponent<UIToggle>().value = true;
                ShowBuyGeneralItem(dataSplit[12]);
                ShowBuyVipItem(dataSplit[13]);
            }
            else
            {
                UIManager.instance.OpenPromptWindow("活动时间未到", PromptWindow.PromptType.Hint, null, null);
            }
        };
    }
    public void ShowBuyGeneralItem(string dataSplit)
    {
        UIEventListener.Get(GeneralButton).onClick = null;
        string[] GeneralSplit = dataSplit.Split('$');
        ActivityHalfLimitBuy Generalitem = TextTranslator.instance.GetActivityHalfLimitBuyByID(int.Parse(GeneralSplit[0]));
        GeneralGoods.transform.Find("OriginalPrice/Label").GetComponent<UILabel>().text = (Generalitem.Price).ToString();
        GeneralGoods.transform.Find("CurrentPrice/Label").GetComponent<UILabel>().text = (Generalitem.NowPrice).ToString();
        GeneralGoods.transform.Find("Item/number").GetComponent<UILabel>().text = (Generalitem.ItemNum).ToString();
        GeneralGoods.transform.Find("Item").GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(Generalitem.ItemID).itemGrade;
        TextTranslator.ItemInfo item = TextTranslator.instance.GetItemByItemCode(Generalitem.ItemID);
        GeneralGoods.transform.Find("Item/icon").GetComponent<UISprite>().spriteName = (item.picID).ToString();
        GeneralGoods.transform.Find("Item").GetComponent<ItemExplanation>().SetAwardItem(item.itemCode, 0);//yy
        GeneralGoods.transform.Find("BuyGeneralNumber").GetComponent<UILabel>().text = "仅限" + (Generalitem.LimitBuyNum).ToString() + "人购买（剩余" + GeneralSplit[2] + "件)";
        if (GeneralSplit[1] == "1")
        {
            GeneralGetButton.SetActive(true);
            GeneralButton.SetActive(false);
        }
        else
        {
            GeneralGetButton.SetActive(false);
            GeneralButton.SetActive(true);
        }
        UIEventListener.Get(GeneralButton).onClick += delegate(GameObject go)
        {
            if (AllTime <= 0)
            {
                UIManager.instance.OpenPromptWindow("活动时间已经结束", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {

                    NetworkHandler.instance.SendProcess("9134#" + GeneralSplit[0] + ";");             
            }
        };
        UIEventListener.Get(GeneralGetButton).onClick += delegate(GameObject go)
        {
            UIManager.instance.OpenPromptWindow("已经购买", PromptWindow.PromptType.Hint, null, null);
          
        };


    }
    public void ShowBuyVipItem(string dataSplit)
    {
        UIEventListener.Get(VipButton).onClick = null;
        string[] VipSplit = dataSplit.Split('$');
        ActivityHalfLimitBuy Vipitem = TextTranslator.instance.GetActivityHalfLimitBuyByID(int.Parse(VipSplit[0]));
        VipGoods.transform.Find("OriginalPrice/Label").GetComponent<UILabel>().text = (Vipitem.Price).ToString();
        VipGoods.transform.Find("CurrentPrice/Label").GetComponent<UILabel>().text = (Vipitem.NowPrice).ToString();
        VipGoods.transform.Find("Item/number").GetComponent<UILabel>().text = (Vipitem.ItemNum).ToString(); ;
        VipGoods.transform.Find("Item").GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(Vipitem.ItemID).itemGrade;
        TextTranslator.ItemInfo item = TextTranslator.instance.GetItemByItemCode(Vipitem.ItemID);
        VipGoods.transform.Find("Item/icon").GetComponent<UISprite>().spriteName = (item.picID).ToString();
        VipGoods.transform.Find("Item").GetComponent<ItemExplanation>().SetAwardItem(item.itemCode, 0);//yy
        if (60000 < item.picID && item.picID < 70000)
        {
            VipGoods.transform.Find("Item/icon").GetComponent<UISprite>().atlas = HeroAtlas;
        }
        else
        {
            VipGoods.transform.Find("Item/icon").GetComponent<UISprite>().atlas = itemAtlas;
        }
        VipGoods.transform.Find("BuyVipNumber").GetComponent<UILabel>().text = "仅限" + (Vipitem.LimitBuyNum).ToString() + "人购买（剩余" + VipSplit[2] + "件)";
        VipGoods.transform.Find("VipLabel").GetComponent<UILabel>().text = "V" + Vipitem.LimitVip.ToString();
        if (VipSplit[1] == "1")
        {
            VipGetButton.SetActive(true);
            VipButton.SetActive(false);
        }
        else
        {
            VipGetButton.SetActive(false);
            VipButton.SetActive(true);
        }
        UIEventListener.Get(VipButton).onClick += delegate(GameObject go)
        {
            if (AllTime <= 0)
            {
                UIManager.instance.OpenPromptWindow("活动时间已经结束", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                if (CharacterRecorder.instance.Vip >= Vipitem.LimitVip)
                {
                    {
                        NetworkHandler.instance.SendProcess("9134#" + VipSplit[0] + ";");
                    }
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("您的VIP等级不足，无法购买", PromptWindow.PromptType.Hint, null, null);
                }
            }
        };
        UIEventListener.Get(VipGetButton).onClick += delegate(GameObject go)
        {
            UIManager.instance.OpenPromptWindow("已经购买", PromptWindow.PromptType.Hint, null, null);
        };

    }
    public void ShowTime(string dataSplit)
    {
        CancelInvoke("UpdateProtectTime");
        AllTime = int.Parse(dataSplit);
        InvokeRepeating("UpdateProtectTime", 0, 1.0f);
    }
    public void UpdateProtectTime()
    {
        string dayStr = (AllTime / 86400).ToString();
        string houreStr = (AllTime / 3600 % 24).ToString("00");
        string miniteStr = (AllTime % 3600 / 60).ToString("00");
        string secondStr = (AllTime % 60).ToString("00");
        GameObject.Find("TimeTexture/DayLabel").GetComponent<UILabel>().text = string.Format("{0}", dayStr) + "天";
        GameObject.Find("TimeTexture/TimeLabel").GetComponent<UILabel>().text = string.Format("{0}:{1}:{2}", houreStr, miniteStr, secondStr);
        AllTime -= 1;
    }
}
