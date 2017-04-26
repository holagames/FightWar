using UnityEngine;
using System.Collections;

public class BonusVipItem : MonoBehaviour {
    public GameObject GetButton;
    public GameObject GetedButton;
    public GameObject  Item1;
     public GameObject  Item2;
     public GameObject  Item3;
     public GameObject  Item4;
     public UIAtlas ItemAtlas;
     public UIAtlas HeroAtlas;
     public UILabel Des;
     public UILabel DayInfo;
     public int Index;
	// Use this for initialization
	void Start () {
        UIEventListener.Get(GetButton).onClick = delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("9613#" + Index + ";");
        };
	}
    public void SetInfo(int index,int DayNumber,int ItemStyle)
    {
        Index = index;
        ActivityBonusVip vipitem = TextTranslator.instance.GetActivityBonusVipDicByID(index);
        GetButton.SetActive(false);
        GetedButton.SetActive(false);
        SetFalse(Item1, vipitem.ItemID1,vipitem.ItemNum1);
        SetFalse(Item2, vipitem.ItemID2, vipitem.ItemNum2);
        SetFalse(Item3, vipitem.ItemID3, vipitem.ItemNum3);
        SetFalse(Item4, vipitem.ItemID4, vipitem.ItemNum4);
        if (ItemStyle == 0)
        {
            GetedButton.SetActive(true);
            GetedButton.GetComponent<UILabel>().text = "不可领";
        }
        else if(ItemStyle == 1)        
        {
            GetButton.SetActive(true);
        }
        else
        {
            GetedButton.SetActive(true);
            GetedButton.GetComponent<UILabel>().text = "已领取";
        }
        Des.text = vipitem.Des;
        DayInfo.text = "[28DF5E]"+DayNumber + "[-]/" + vipitem.Param1;
    }

    void SetFalse(GameObject go, int id,int number)
    {
        if (id == 0)
        {
            go.SetActive(false);
        }
        else 
        {
            go.SetActive(true);
            go.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(id).itemGrade.ToString();
            go.transform.Find("Label").GetComponent<UILabel>().text = number.ToString();
            UISprite Icon = go.transform.Find("Icon").GetComponent<UISprite>();
            GameObject Framge = go.transform.Find("Framge").gameObject;
            Icon.spriteName = TextTranslator.instance.GetItemByItemCode(id).picID.ToString();
            if (60000 < id && id < 70000)
            {
                Icon.atlas = HeroAtlas;
            }
            else if (70000 < id && id < 79999)
            {
                Icon.atlas = HeroAtlas;
            }
            else
            {
                Icon.atlas = ItemAtlas;
            }
            if (70000 < id && id < 79999)
            {
                Framge.SetActive(true);
            }
            else if (70000 < id && id < 79999)
            {
                Framge.SetActive(true);
            }
            else
            {
                Framge.SetActive(false);
            }
        }
        TextTranslator.instance.ItemDescription(go, TextTranslator.instance.GetItemByItemCode(id).picID, 0);
    }

}
