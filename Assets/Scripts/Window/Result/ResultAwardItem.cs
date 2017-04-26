using UnityEngine;
using System.Collections;

public class ResultAwardItem : MonoBehaviour {

    int ItemCode;
    int ItemCount;

    // Use this for initialization
    void Start()
    {
        if (UIEventListener.Get(gameObject).onClick == null)
        {
            UIEventListener.Get(gameObject).onClick += delegate(GameObject go)
            {
                if (GameObject.Find("ItemExplanationWindow") != null)
                {
                    //UIManager.instance.ClosePanel("ItemExplanationWindow");
                    UIManager.instance.BackUI();
                }
                else
                {
                    UIManager.instance.OpenPanel("ItemExplanationWindow", false);
                    //Debug.LogError("22" + ItemCode.ToString());
                    GameObject.Find("ItemExplanationWindow").GetComponent<ItemExplanationWindow>().SetItemDetail(ItemCode, ItemCount, this.gameObject);
                }
            };
        }
    }
    public void init(int itemId,int itemCount) 
    {
        this.ItemCode = itemId;
        this.ItemCount = itemCount;
    }
}
