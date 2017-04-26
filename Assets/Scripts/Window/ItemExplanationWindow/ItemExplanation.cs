using UnityEngine;
using System.Collections;

public class ItemExplanation : MonoBehaviour
{
    private bool isPress = false;
    private float datatime = 0;
    private int ItemId;
    private int ItemCount;

    void Start()
    {
        if (UIEventListener.Get(this.gameObject).onPress == null)
        {
            UIEventListener.Get(this.gameObject).onPress = delegate(GameObject go, bool isPressed)
            {
                OnPress(isPressed);
            };
        }
        if (UIEventListener.Get(this.gameObject).onClick == null)
        {
            UIEventListener.Get(this.gameObject).onClick = delegate(GameObject go)
            {
                CharacterRecorder.instance.isGaChaFromItemClick = true;
                if (GameObject.Find("AdvanceWindow") == null)
                {
                    if (60000 < ItemId && ItemId < 70000)
                    {
                        UIManager.instance.OpenSinglePanel("CardIntroduceWindow", false);
                        GameObject obj = GameObject.Find("CardIntroduceWindow");
                        if (obj != null)
                        {
                            CardIntroduceWindow _card = obj.GetComponent<CardIntroduceWindow>();
                            _card.SetIntroduceInfo(ItemId);

                        }
                    }
                    else if (70000 < ItemId && ItemId < 79999)
                    {
                        UIManager.instance.OpenSinglePanel("CardIntroduceWindow", false);
                        GameObject obj = GameObject.Find("CardIntroduceWindow");
                        if (obj != null)
                        {
                            CardIntroduceWindow _card = obj.GetComponent<CardIntroduceWindow>();
                            _card.SetIntroduceInfo(ItemId - 10000);

                        }
                    }
                }
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
                UIManager.instance.OpenSinglePanel("ItemExplanationWindow", false);
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
            while (GameObject.Find("ItemExplanationWindow") != null)
            {
                DestroyImmediate(GameObject.Find("ItemExplanationWindow"));
            }
            datatime = 0;
        }
    }

    public void SetAwardItem(int itemId, int itemCount)
    {
        this.ItemId = itemId;
        this.ItemCount = itemCount;
    }
}
