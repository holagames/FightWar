using UnityEngine;
using System.Collections;

public class SignItemDetail:MonoBehaviour 
{
    [SerializeField]
    private GameObject closeButton;
    [SerializeField]
    private GameObject suiPian;
    [SerializeField]
    private UISprite spriteIcon;
    [SerializeField]
    private UISprite spriteFrame;
    [SerializeField]
    private UILabel labelName;
    [SerializeField]
    private UILabel labelCountDes;
    [SerializeField]
    private UILabel labelDes;
    [SerializeField]
    private UIAtlas ItemAtlas;
    [SerializeField]
    private UIAtlas RoleAtlas;
    private int itemCount;
	// Use this for initialization
	void Start () 
    {
        UIEventListener.Get(closeButton).onClick = ClickCloseButton;
	}
    void SetItemFrame(int _itemId)
    {
        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(_itemId);
        //Debug.LogError("_itemId.." + _itemId + "..Item.." + _ItemInfo);
        spriteFrame.spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
    }
    public void SetComanItemDetail(int _ItemCode)
    {
        int ItemCode = _ItemCode;//物品id

        SetItemFrame(_ItemCode);
        string ItemNameStr = TextTranslator.instance.GetItemNameByItemCode(_ItemCode);//道具名字
        string ItemDesStr = TextTranslator.instance.GetItemDescriptionByItemCode(ItemCode);//物品描述

        if (ItemCode.ToString()[0] == '4')
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = (ItemCode - 10000).ToString();
            suiPian.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '6')
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            suiPian.SetActive(false);
        }
        else if (ItemCode == 70000 || ItemCode==79999)
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            suiPian.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '7' && ItemCode > 70000)
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = (ItemCode - 10000).ToString();
            suiPian.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '8')
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = (ItemCode - 30000).ToString();
            suiPian.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '2')
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
            spriteIcon.spriteName = mItemInfo.picID.ToString();
            suiPian.SetActive(false);
        }
        else
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            suiPian.SetActive(false);
        }
        int myOwnCount = 0;
        switch (ItemCode)
        {
            case 90001: myOwnCount = CharacterRecorder.instance.gold; break;
            case 90002: myOwnCount = CharacterRecorder.instance.lunaGem; break;
            case 90003: myOwnCount = CharacterRecorder.instance.HonerValue; break;
            case 90004: break;
            case 90005: break;
            case 90006: break;
            case 90007: myOwnCount = CharacterRecorder.instance.stamina; break;
            case 90008: myOwnCount = CharacterRecorder.instance.sprite; break;
            default: myOwnCount = TextTranslator.instance.GetItemCountByID(ItemCode); break;
        }
        labelCountDes.text = string.Format("{0}拥有: {1}{2}", "[ffffff]", "[93d8f3]", myOwnCount);

        labelName.text = ItemNameStr;
        labelDes.text = ItemDesStr;

    }
    public void SetSignItemDetail(Sign _SignAward)
    {
        //this.name = _oneShopItemData.index.ToString();
        int ItemCode = _SignAward.itemId;//物品id
        itemCount = _SignAward.itemCount;//数量
        SetItemFrame(_SignAward.itemId);
        string ItemNameStr = TextTranslator.instance.GetItemNameByItemCode(ItemCode);//道具名字
        string ItemDesStr = TextTranslator.instance.GetItemDescriptionByItemCode(ItemCode);//物品描述

        if (ItemCode.ToString()[0] == '4')
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = (ItemCode - 10000).ToString();
            suiPian.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '7')
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = (ItemCode - 10000).ToString();
            suiPian.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '8')
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = (ItemCode - 30000).ToString();
            suiPian.SetActive(true);
        }
        else
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            suiPian.SetActive(false);
        }
        //labelCountDes.text = string.Format("{0}拥有: {1}{2}","[ffffff]","[93d8f3]", itemCount);
        int myOwnCount = 0;
        switch (ItemCode)
        {
            case 90001: myOwnCount = CharacterRecorder.instance.gold; break;
            case 90002: myOwnCount = CharacterRecorder.instance.lunaGem; break;
            case 90003: myOwnCount = CharacterRecorder.instance.HonerValue; break;
            case 90004: break;
            case 90005: break;
            case 90006: break;
            case 90007: myOwnCount = CharacterRecorder.instance.stamina; break;
            case 90008: myOwnCount = CharacterRecorder.instance.sprite; break;
            default: myOwnCount = TextTranslator.instance.GetItemCountByID(ItemCode); break;
        }
        labelCountDes.text = string.Format("{0}拥有: {1}{2}", "[ffffff]", "[93d8f3]", myOwnCount);
        
        labelName.text = ItemNameStr;
        labelDes.text = ItemDesStr;

    }
    public void SetSignItemDetail(SignExtraItemData _SignAward)
    {
        //this.name = _oneShopItemData.index.ToString();
        int ItemCode = _SignAward.ItemID;//物品id
        itemCount = _SignAward.ItemNum;//数量
        SetItemFrame(_SignAward.ItemID);
        string ItemNameStr = TextTranslator.instance.GetItemNameByItemCode(ItemCode);//道具名字
        string ItemDesStr = TextTranslator.instance.GetItemDescriptionByItemCode(ItemCode);//物品描述

        if (ItemCode.ToString()[0] == '4')
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = (ItemCode - 10000).ToString();
            suiPian.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '7')
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = (ItemCode - 10000).ToString();
            suiPian.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '8')
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = (ItemCode - 30000).ToString();
            suiPian.SetActive(true);
        }
        else
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            suiPian.SetActive(false);
        }
        //labelCountDes.text = string.Format("{0}拥有: {1}{2}", "[ffffff]", "[93d8f3]", itemCount);
        int myOwnCount = 0;
        switch (ItemCode)
        {
            case 90001: myOwnCount = CharacterRecorder.instance.gold; break;
            case 90002: myOwnCount = CharacterRecorder.instance.lunaGem; break;
            case 90003: myOwnCount = CharacterRecorder.instance.HonerValue; break;
            case 90004: break;
            case 90005: break;
            case 90006: break;
            case 90007: myOwnCount = CharacterRecorder.instance.stamina; break;
            case 90008: myOwnCount = CharacterRecorder.instance.sprite; break;
            default: myOwnCount = TextTranslator.instance.GetItemCountByID(ItemCode); break;
        }
        labelCountDes.text = string.Format("{0}拥有: {1}{2}", "[ffffff]", "[93d8f3]", myOwnCount);

        labelName.text = ItemNameStr;
        labelDes.text = ItemDesStr;

    }
    private void ClickCloseButton(GameObject go)
    {
        if(go != null)
        {
            UIManager.instance.BackUI();
        }
    }
}
