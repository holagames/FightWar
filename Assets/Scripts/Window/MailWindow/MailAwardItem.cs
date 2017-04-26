using UnityEngine;
using System.Collections;

public class MailAwardItem: MonoBehaviour 
{
    public UIAtlas ItemAtlas;
    public UIAtlas AvatarAtlas;

    [SerializeField]
    private UISprite frame;

    private AwardItem _AwardItem;

    private bool isPress = false;
    private float datatime = 0;
    private int ItemId;
    private int ItemCount;
    private GameObject AwardObj;
	// Use this for initialization
	void Start () 
    {
       /* if (UIEventListener.Get(this.gameObject).onClick == null)
        {
            UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPanel("SignItemDetail", false);
                GameObject.Find("SignItemDetail").GetComponent<SignItemDetail>().SetComanItemDetail(this._AwardItem.itemCode);
            };
        }*/
        UIEventListener.Get(this.gameObject).onPress += delegate(GameObject go, bool isPressed)
        {
            AwardObj = this.gameObject;
            ItemId = this._AwardItem.itemCode;
            ItemCount = this._AwardItem.itemCount;
          //  OnPress(isPressed);
            OnPressAwardItem(isPressed);
        };
	}
	
	// Update is called once per frame
	void Update () 
    {
      /*  if (isPress)
        {
            datatime += Time.deltaTime;
            if (datatime > 0.2f)
            {
                UIManager.instance.OpenPanel("ItemExplanationWindow", false);
                GameObject.Find("ItemExplanationWindow").GetComponent<ItemExplanationWindow>().SetItemDetail(ItemId, ItemCount, AwardObj);
                isPress = false;
            }
        }*/
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
                UIManager.instance.BackUI();
            }
            datatime = 0;
        }
    }
    void OnPressAwardItem(bool isPressed)
    {
        if (isPress)
        {
            UIManager.instance.OpenPanel("ItemExplanationWindow", false);
            GameObject.Find("ItemExplanationWindow").GetComponent<ItemExplanationWindow>().SetItemDetail(ItemId, ItemCount, AwardObj);
        }
        else
        {
            if (GameObject.Find("ItemExplanationWindow") != null)
            {
                //UIManager.instance.BackUI();
                Destroy(GameObject.Find("ItemExplanationWindow"));
            }
        }
    }
    public void SetMailAwardItem(AwardItem _AwardItem)
    {
        this._AwardItem = _AwardItem;
        TextTranslator.ItemInfo _iteminfo = TextTranslator.instance.GetItemByItemCode(_AwardItem.itemCode);
        SetColor(frame, _iteminfo.itemGrade);
        //go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = _AwardItem.itemCode.ToString();
        this.transform.FindChild("Count").GetComponent<UILabel>().text = _AwardItem.itemCount.ToString();
        if (_AwardItem.itemCode.ToString()[0] == '4')
        {
            this.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            this.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = _AwardItem.itemCode.ToString();
            this.transform.FindChild("SuiPian").gameObject.SetActive(false);
        }
        else if (_AwardItem.itemCode.ToString() == "70000")
        {
            this.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            this.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = _AwardItem.itemCode.ToString();
            this.transform.FindChild("SuiPian").gameObject.SetActive(true);
        }
        else if (_AwardItem.itemCode.ToString() == "79999")
        {
            this.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            this.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = _AwardItem.itemCode.ToString();
            this.transform.FindChild("SuiPian").gameObject.SetActive(true);
        }
        else if (_AwardItem.itemCode.ToString()[0] == '7')
        {
            this.transform.FindChild("Icon").GetComponent<UISprite>().atlas = AvatarAtlas;
            this.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = (_AwardItem.itemCode-10000).ToString();
            this.transform.FindChild("SuiPian").gameObject.SetActive(true);
        }
        else if (_AwardItem.itemCode.ToString()[0] == '6')
        {
            this.transform.FindChild("Icon").GetComponent<UISprite>().atlas = AvatarAtlas;
            this.transform.FindChild("SuiPian").gameObject.SetActive(false);
            this.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = _AwardItem.itemCode.ToString();
        }
        else if (_AwardItem.itemCode.ToString()[0] == '8')
        {
            this.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            this.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode((_AwardItem.itemCode / 10) * 10 - 30000).picID.ToString();
            this.transform.FindChild("SuiPian").gameObject.SetActive(true);
        }
        else if (_AwardItem.itemCode.ToString()[0] == '2')
        {
            this.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_AwardItem.itemCode);
            this.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = mItemInfo.picID.ToString();
            this.transform.FindChild("SuiPian").gameObject.SetActive(false);
        }
        else
        {
            this.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            this.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = _AwardItem.itemCode.ToString();
            this.transform.FindChild("SuiPian").gameObject.SetActive(false);
        }

        this.GetComponent<UIDragScrollView>().scrollView = this.transform.parent.parent.GetComponent<UIScrollView>();


       /* UIEventListener.Get(this.gameObject).onPress = delegate(GameObject obj, bool isPress)
        {
            if (isPress)
            {
                this.transform.FindChild("ItemBoard").gameObject.SetActive(true);
                this.transform.FindChild("ItemBoard").FindChild("Item").FindChild("Icon").GetComponent<UISprite>().spriteName = _AwardItem.itemCode.ToString();
                this.transform.FindChild("ItemBoard").FindChild("ItemName").GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(_AwardItem.itemCode);
                this.transform.FindChild("ItemBoard").FindChild("ItemDes").GetComponent<UILabel>().text = TextTranslator.instance.GetItemDescriptionByItemCode(_AwardItem.itemCode);
                SetColor(this.transform.FindChild("ItemBoard").FindChild("Item").gameObject.GetComponent<UISprite>(), _iteminfo.itemGrade);
            }
            else
            {
                this.transform.FindChild("ItemBoard").gameObject.SetActive(false);
            }
        };*/
    }
    void SetColor(UISprite go, int color)
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
            case 6:
                go.GetComponent<UISprite>().spriteName = "Grade6";
                break;
            case 7:
                go.GetComponent<UISprite>().spriteName = "Grade7";
                break;
        }
    }
}
public class AwardItem
{
    public int itemCode { get; private set; }

    public int itemCount { get; private set; }

    public AwardItem(int itemCode, int itemCount)
    {
        this.itemCode = itemCode;
        this.itemCount = itemCount;
    }
}
