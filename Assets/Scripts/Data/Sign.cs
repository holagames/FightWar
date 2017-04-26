using UnityEngine;
using System.Collections;

public class Sign
{
    //public int month { get; set; }
    public int signID { get; set; }
    public int itemId { get; set; }
    public int itemCount { get; set; }
    public int vipLv { get; set; }
    public int state { get; set; }
    public Sign(int signID, int itemId, int itemCount, int vipLv)
    {
        this.signID = signID;
        this.itemId = itemId;
        this.itemCount = itemCount;
        this.vipLv = vipLv;
    }
}
