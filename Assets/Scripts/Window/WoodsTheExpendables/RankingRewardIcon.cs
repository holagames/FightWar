using UnityEngine;
using System.Collections;

public class RankingRewardIcon : MonoBehaviour {

    private bool isPress = false;
    private float datatime = 0;
    private int ItemId;
    private int ItemCount;

	void Start () {
        if (this.gameObject.name == "Icon1") 
        {
            this.ItemId = 90002;
            this.ItemCount = 0;
        }
        else if(this.gameObject.name == "Icon2")
        {
            this.ItemId = 90001;
            this.ItemCount = 0;
        }
        else if (this.gameObject.name == "Icon3")
        {
            this.ItemId = 10101;
            this.ItemCount = 0;
        }
        else if (this.gameObject.name == "Icon4")
        {
            this.ItemId = 10401;
            this.ItemCount = 0;
        }
        if (UIEventListener.Get(this.gameObject).onPress == null)
        {
            UIEventListener.Get(this.gameObject).onPress = delegate(GameObject go, bool isPressed)
            {
                OnPress(isPressed);
            };
        }
	}
    void Update()
    {
        if (isPress)
        {
            datatime += Time.deltaTime;
            if (datatime > 0.2f)
            {
                UIManager.instance.OpenPanel("ItemExplanationWindow", false);
                GameObject.Find("ItemExplanationWindow").GetComponent<ItemExplanationWindow>().SetItemDetail(ItemId, ItemCount, this.gameObject);
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
                //DestroyImmediate(GameObject.Find("ItemExplanationWindow"));
                UIManager.instance.BackUI();
            }
            datatime = 0;
        }
    }
    //public void SetIconInfo(int _itemId,int _itemCount)
    //{
    //    this.ItemId = _itemId;
    //    this.ItemCount = _itemCount;
    //}
}
