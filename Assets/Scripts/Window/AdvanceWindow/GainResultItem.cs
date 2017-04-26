using UnityEngine;
using System.Collections;

public class GainResultItem : MonoBehaviour
{
    [SerializeField]
    private GameObject suiPian;
    [SerializeField]
    private UISprite frameSprite;
    [SerializeField]
    private UISprite iconSprite;
    [SerializeField]
    private UILabel nameLabel;
    [SerializeField]
    public UILabel numberLabel;
    public UISprite sgSprite;
    public UIAtlas RoleAtlas;
    public UIAtlas ItemAtlas;
    // Use this for initialization
    void Start()
    {

    }


    public void SetGainResultItem(Item _myItem)
    {
        frameSprite.spriteName = string.Format("Grade{0}", _myItem.itemGrade);
        if (_myItem.itemCode > 70000 && _myItem.itemCode < 79999)
        {
            iconSprite.atlas = RoleAtlas;
            iconSprite.spriteName = (_myItem.itemCode - 10000).ToString();
            suiPian.SetActive(true);
        }
        else if (_myItem.itemCode.ToString()[0] == '2')
        {
            iconSprite.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_myItem.itemCode);
            iconSprite.spriteName = mItemInfo.picID.ToString();
            suiPian.SetActive(false);
        }
        else if (_myItem.itemCode.ToString()[0] == '6')
        {
            iconSprite.atlas = RoleAtlas;
            iconSprite.spriteName = _myItem.itemCode.ToString();
            suiPian.SetActive(false);

            if (GameObject.Find("FirstRechargeWindow") == null && GameObject.Find("VIPShopWindow") == null && GameObject.Find("LoginSignWindow") == null) //此处防止5002充值获得英雄重复弹7002 9的公告
            {
                NetworkHandler.instance.SendProcess(string.Format("7002#{0};{1};{2};{3}", 7, CharacterRecorder.instance.characterName, _myItem.itemName, _myItem.itemGrade));    
            }
            //NetworkHandler.instance.SendProcess(string.Format("7002#{0};{1};{2};{3}", 7, CharacterRecorder.instance.characterName, _myItem.itemName, _myItem.itemGrade));
        }
        else if (_myItem.itemCode.ToString()[0] == '8')
        {
            iconSprite.atlas = ItemAtlas;
            iconSprite.spriteName = TextTranslator.instance.GetItemByItemCode(_myItem.itemCode).picID.ToString();
            suiPian.SetActive(true);
        }
        else if (_myItem.itemCode / 1000 == 42)//
        {
            iconSprite.atlas = ItemAtlas;
            iconSprite.spriteName = _myItem.itemCode.ToString();
            suiPian.SetActive(true);
        }
        else
        {
            iconSprite.atlas = ItemAtlas;
            iconSprite.spriteName = _myItem.itemCode.ToString();
            if (iconSprite.spriteName == "10104")
            {
                NetworkHandler.instance.SendProcess("1601#");
            }
            suiPian.SetActive(false);
        }

        nameLabel.text = GetItemName(_myItem.itemCode);
        if (_myItem.itemCount <= 9999)
        {
            numberLabel.text = _myItem.itemCount.ToString();
        }
        else
        {
            numberLabel.text = (_myItem.itemCount / 10000.0f).ToString("f1") + "W";
        }
        StartCoroutine(SgTime());
    }
    IEnumerator SgTime()
    {
        yield return new WaitForSeconds(0.5f);
        sgSprite.gameObject.SetActive(false);
    }

    string GetItemName(int mEquipCode)
    {
        TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(mEquipCode);
        string _nameColor = "";

        switch (mItemInfo.itemGrade)
        {
            case 1: _nameColor = "[ffffff]"; break;
            case 2: _nameColor = "[3ee817]"; break;
            case 3: _nameColor = "[8ccef2]"; break;
            case 4: _nameColor = "[bb44ff]"; break;
            case 5: _nameColor = "[ff8c04]"; break;
            case 6: _nameColor = "[fb2d50]"; break;
        }
        return string.Format("{0}{1}", _nameColor, mItemInfo.itemName);
    }
}
