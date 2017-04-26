using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LoseGrabWindow : MonoBehaviour {

    public GameObject CloseButton;
    public GameObject Item;
    public GameObject Grid;
    public List<GameObject> MessageLsit = new List<GameObject>();
 	// Use this for initialization
	void Start () {
        UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
        {
            this.gameObject.SetActive(false);
        }; 
	}

    public void LoseMessage(string dataSplit)
    {
        for (int i = 0; i < MessageLsit.Count; i++)
        {
            DestroyImmediate(MessageLsit[i]);
        }
        MessageLsit.Clear();
        string []messageStr = dataSplit.Split('!');
        for (int i = 0; i < messageStr.Length - 1; i++)
        {
            string []infoStr = messageStr[i].Split('$');
            GameObject go = Instantiate(Item) as GameObject;
            go.transform.parent = Grid.transform;
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.SetActive(true);
            TextTranslator.ItemInfo ItemInfo = TextTranslator.instance.GetItemByItemCode(int.Parse(infoStr[1]));
            string Timer;
            if (int.Parse(infoStr[0])>=3600)
            {
                Timer = (int.Parse(infoStr[0]) / 3600).ToString() + "小时前";
            }
            else
            {
                Timer = (int.Parse(infoStr[0]) / 60).ToString() + "分钟前";
            }
            go.GetComponent<UILabel>().text = Timer + "," + "[ff0000]"+infoStr[2]+"[-]" + "夺取了您的" + GameObject.Find("GrabItemWindow").GetComponent<GrabWindow>().ItemNameColor(TextTranslator.instance.GetItemByItemCode(ItemInfo.itemCode).itemGrade) + ItemInfo.itemName + "[-]"; ;
            MessageLsit.Add(go);
        }
        Grid.GetComponent<UIGrid>().repositionNow = true;
    }
}
