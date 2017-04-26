using UnityEngine;
using System.Collections;

public class Mail {

    /// <summary>
    /// 邮件ID
    /// </summary>
    public int mailID { get; private set; } 

    /// <summary>
    /// 右边标题
    /// </summary>
    public string mailTitle { get; private set; } 

    /// <summary>
    /// 邮件内容
    /// </summary>
    public string mailContent { get; set; } 

    /// <summary>
    /// 邮件图标id
    /// </summary>
    public int mailPicCode { get; private set; } 

    /// <summary>
    /// 附件列表
    /// </summary>
    public BetterList<Item> itemList { get; private set; } 

    /// <summary>
    /// 邮件时间
    /// </summary>
    public double mailTime { get; private set; } 

    /// <summary>
    /// 发信人名字
    /// </summary>
    public string mailSender { get; private set; } 

    /// <summary>
    /// 已读
    /// </summary>
    public bool markRead { get; private set; } 

    /// <summary>
    /// 是否有附件
    /// </summary>
    public bool haveAttach { get; private set; } 

    /// <summary>
    /// 已领取附件
    /// </summary>
    public bool received { get; private set; } 

    public Mail(int newMailID, string newTitle, string newSender, int newTime, 
        int newMailPicCode, bool newMarkRead, bool newReceived)
    {
        this.mailID = newMailID;
        this.mailTitle = newTitle;
        this.mailSender = newSender;
        this.mailTime = newTime;
        this.mailPicCode = newMailPicCode;
        this.markRead = newMarkRead;
        this.received = newReceived;

        haveAttach = (mailPicCode != 0);
        mailContent = "";
        itemList = new BetterList<Item>();
    }

    public void SetMailReceived()
    {
        received = true;
    }

    public void SetMailRead()
    {
        markRead = true;
    }
}
