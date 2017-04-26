using UnityEngine;
using System.Collections;

public class BagDetailInfoBoard : MonoBehaviour {

    public UILabel itemDes;
    public UILabel itemDetailInfo;
    public UILabel classifyLabel;
    public GameObject itemIcon;
    public UILabel itemName;

	// Use this for initialization
	void Start () {

	}

    public void SetInfo(int itemid)
    {
        TextTranslator.ItemInfo miteminfo = TextTranslator.instance.GetItemByItemCode(itemid);
        itemDes.text = miteminfo.itemDescription;
        itemDetailInfo.text = miteminfo.itemDescription;
        itemName.text = miteminfo.itemName;
        if (miteminfo.itemCode.ToString()[0] == '5')
        {
            itemIcon.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = miteminfo.itemCode.ToString().Substring(0, 4) + "1";
        }
        else
        {
            itemIcon.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = miteminfo.itemCode.ToString();
        }
        SetColor(itemIcon, miteminfo.itemGrade);
        if (itemid.ToString().Substring(0, 1) == "5")
        {
            classifyLabel.text = "可装备的英雄：";
        }
        else if (itemid.ToString().Substring(0, 1) == "3")
        {
            classifyLabel.text = "获取途径：";
        }
        else
        {
            classifyLabel.text = "获取途径：";
        }

        if (UIEventListener.Get(GameObject.Find("Closebutton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("Closebutton")).onClick += delegate(GameObject go)
            {
                this.gameObject.SetActive(false);
            };
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
