using UnityEngine;
using System.Collections;
using System;

public class MailItem : MonoBehaviour
{
    public UIAtlas itemAtlas;
    public UIAtlas commanAtlas;
    public UIAtlas AvatarAtlas;
    public GameObject SuiPian;
    public UILabel awardNameLabel;
    public UILabel awardTimeLabel;
    public UILabel mailSenderLabel;
    public UISprite readSprite;
    public UISprite icon;
    private MailItemData oneMailItemData;
    // Use this for initialization
    void Start()
    {

        UIEventListener.Get(this.gameObject).onClick += delegate(GameObject obj)
        {
            UIManager.instance.OpenPanel("MailInfoBoard", false);
            //GameObject mailInfoBoard = GameObject.Find("MailWindow").GetComponent<MailWindow>().MailInfoBoard;
            //mailInfoBoard.SetActive(true);
            GameObject mailInfoBoard = GameObject.Find("MailInfoBoard");
            mailInfoBoard.GetComponent<MailInfoBoard>().SetInfo(oneMailItemData);
        };
    }


    public void SetMailItemInfo(MailItemData _oneMailItemData)
    {
        //Debug.LogError(_oneMailItemData.isRead + "icon" +  _oneMailItemData.itemIcon);
        oneMailItemData = _oneMailItemData;
        this.name = _oneMailItemData.mailID.ToString();
        awardNameLabel.text = _oneMailItemData.mailName;
        awardTimeLabel.text = UnixTimestampToDateTime(_oneMailItemData.mailDate).ToString();
        mailSenderLabel.text = string.Format("{0}", _oneMailItemData.mailSender);
        TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(_oneMailItemData.itemIcon);
        SetColor(this.transform.FindChild("AwardItem").gameObject, mitemInfo.itemGrade);
        if (_oneMailItemData.isRead == 1)
        {
            //haveReadLabel.text = "已 读";
            readSprite.spriteName = String.Format("read{0}", _oneMailItemData.isRead);
        }
        else
        {
            //haveReadLabel.text = "未 读";
            readSprite.spriteName = String.Format("read{0}", _oneMailItemData.isRead);
        }
        switch (_oneMailItemData.itemIcon)
        {
            case 0:
                icon.atlas = commanAtlas;
                icon.spriteName = "mailIcon";
                SuiPian.SetActive(false);
                break;
            default:
                // icon.atlas = itemAtlas;
                // icon.spriteName = _oneMailItemData.itemIcon.ToString();
                SetMailIcon(icon, SuiPian, _oneMailItemData.itemIcon);
                break;
        }
        icon.MakePixelPerfect();

    }
    void SetMailIcon(UISprite iconSprite, GameObject SuiPian, int itemCode)
    {
        if (itemCode.ToString()[0] == '4')
        {
            iconSprite.atlas = itemAtlas;
            iconSprite.spriteName = itemCode.ToString();
            SuiPian.SetActive(false);
        }
        else if (itemCode.ToString() == "70000" || itemCode.ToString() == "79999")
        {
            iconSprite.atlas = itemAtlas;
            iconSprite.spriteName = itemCode.ToString();
            SuiPian.SetActive(true);
        }
        else if (itemCode.ToString()[0] == '7')
        {
            iconSprite.atlas = AvatarAtlas;
            iconSprite.spriteName = (itemCode - 10000).ToString();
            SuiPian.SetActive(true);
        }
        else if (itemCode.ToString()[0] == '6')
        {
            icon.atlas = AvatarAtlas;
            SuiPian.SetActive(false);
            icon.spriteName = itemCode.ToString();
        }
        else if (itemCode.ToString()[0] == '8')
        {
            iconSprite.atlas = itemAtlas;
            iconSprite.spriteName = TextTranslator.instance.GetItemByItemCode((itemCode / 10) * 10 - 30000).picID.ToString();
            SuiPian.SetActive(true);
        }
        else if (itemCode.ToString()[0] == '2')
        {
            iconSprite.atlas = itemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(itemCode);
            iconSprite.spriteName = mItemInfo.picID.ToString();
            SuiPian.SetActive(false);
        }
        else
        {
            iconSprite.atlas = itemAtlas;
            iconSprite.spriteName = itemCode.ToString();
            SuiPian.SetActive(false);
        }
    }
    void SetColor(GameObject go, int color)
    {
        switch (color)
        {
            case 0:
                go.GetComponent<UISprite>().spriteName = "Grade1";
                break;
            case 1:
                go.GetComponent<UISprite>().spriteName = "Grade2";
                break;
            case 2:
                go.GetComponent<UISprite>().spriteName = "Grade3";
                break;
            case 3:
                go.GetComponent<UISprite>().spriteName = "Grade4";
                break;
            case 4:
                go.GetComponent<UISprite>().spriteName = "Grade5";
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
    private DateTime UnixTimestampToDateTime(string timeStemp)
    {
        DateTime dtstart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long timestamp = long.Parse(timeStemp + "0000000");
        TimeSpan toNow = new TimeSpan(timestamp);
        return dtstart.Add(toNow);
    }
}
public class MailItemData
{
    public int mailID;
    public string mailName;
    public string mailSender;
    public string mailDate;
    public int itemIcon;
    public int isRead;
    public int isGet;
    public MailItemData(int _MailID, string _MailName, string _MailSender, string _MailDate, int _ItemIcon, int isRead, int isGet)
    {
        this.mailID = _MailID;
        this.mailName = _MailName;
        this.mailSender = _MailSender;
        this.mailDate = _MailDate;
        this.itemIcon = _ItemIcon;
        this.isRead = isRead;
        this.isGet = isGet;
    }
}
