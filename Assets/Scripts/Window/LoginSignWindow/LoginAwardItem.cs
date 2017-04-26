using UnityEngine;
using System.Collections;

public class LoginAwardItem : MonoBehaviour
{
    public GameObject effectObj;
    public UIAtlas RoleAtlas;
    [SerializeField]
    private UILabel countLabel;
    [SerializeField]
    private UISprite icon;
    [SerializeField]
    private GameObject suiPian;
    [SerializeField]
    private UISprite frame;
    private AwardItem _AwardItem;
    // Use this for initialization
    void Start()
    {
        //if (UIEventListener.Get(this.gameObject).onClick == null)
        //{
        //    UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
        //    {
        //        UIManager.instance.OpenPanel("SignItemDetail", false);
        //        GameObject.Find("SignItemDetail").GetComponent<SignItemDetail>().SetComanItemDetail(this._AwardItem.itemCode);
        //    };
        //}
    }


    public void SetLoginAwardItem(AwardItem _AwardItem)
    {
        /*if (_AwardItem.itemCount <= 9999)
        {
            countLabel.text = _AwardItem.itemCount.ToString();
        }
        else
        {
            countLabel.text = _AwardItem.itemCount / 10000.0f + "W";
        }*/
        this._AwardItem = _AwardItem;
        countLabel.text = _AwardItem.itemCount.ToString();
        if (_AwardItem.itemCode == 79999)
        {
            suiPian.SetActive(true);
            icon.spriteName = _AwardItem.itemCode.ToString();
        }
        else if (_AwardItem.itemCode.ToString()[0] == '7' && _AwardItem.itemCode > 70000 && _AwardItem.itemCode != 79999)
        {
            icon.atlas = RoleAtlas;
            suiPian.SetActive(true);
            icon.spriteName = (_AwardItem.itemCode - 10000).ToString();
        }
        else if (_AwardItem.itemCode == 70000 || _AwardItem.itemCode == 79999)
        {
            suiPian.SetActive(true);
            icon.spriteName = _AwardItem.itemCode.ToString();
        }
        else if (_AwardItem.itemCode.ToString()[0] == '6')
        {
            icon.atlas = RoleAtlas;
            suiPian.SetActive(false);
            icon.spriteName = _AwardItem.itemCode.ToString();
        }
        else if (_AwardItem.itemCode.ToString()[0] == '2')
        {
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_AwardItem.itemCode);
            icon.spriteName = mItemInfo.picID.ToString();
            suiPian.SetActive(false);
        }
        else
        {
            if (_AwardItem.itemCode / 1000 == 42)
            {
                suiPian.SetActive(true);
            }
            else
            {
                suiPian.SetActive(false);
            }
            
            icon.spriteName = _AwardItem.itemCode.ToString();
        }
        SetItemFrame(_AwardItem.itemCode);
    }
    public void SetLoginAwardItem(Item _AwardItem)
    {
        this._AwardItem = new AwardItem(_AwardItem.itemCode, _AwardItem.itemCount);
        countLabel.text = _AwardItem.itemCount.ToString();

        if (_AwardItem.itemCode.ToString()[0] == '7' && _AwardItem.itemCode > 70000 && _AwardItem.itemCode != 79999)
        {
            icon.atlas = RoleAtlas;
            suiPian.SetActive(true);
            icon.spriteName = (_AwardItem.itemCode - 10000).ToString();
        }
        else if (_AwardItem.itemCode == 70000 || _AwardItem.itemCode == 79999)
        {
            suiPian.SetActive(true);
            icon.spriteName = _AwardItem.itemCode.ToString();
        }
        else if (_AwardItem.itemCode.ToString()[0] == '6')
        {
            icon.atlas = RoleAtlas;
            suiPian.SetActive(false);
            icon.spriteName = _AwardItem.itemCode.ToString();
        }
        else if (_AwardItem.itemCode.ToString()[0] == '2')
        {
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_AwardItem.itemCode);
            icon.spriteName = mItemInfo.picID.ToString();
            suiPian.SetActive(false);
        }
        else
        {
            if (_AwardItem.itemCode / 1000 == 42)
            {
                suiPian.SetActive(true);
            }
            else
            {
                suiPian.SetActive(false);
            }
            icon.spriteName = _AwardItem.itemCode.ToString();
        }
        SetItemFrame(_AwardItem.itemCode);
    }
    void SetItemFrame(int _itemId)
    {
        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(_itemId);
        //Debug.LogError("_itemId.." + _itemId + "..Item.." + _ItemInfo);
        frame.spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
    }
}
