using UnityEngine;
using System.Collections;

public class ActivityHalfLimitBuy
{
    public int ActivityHalfLimitBuyID { get; set; }
    public int OpenDayCount { get; set; }
    public int ItemID { get; set; }
    public int ItemNum { get; set; }
    public int Price { get; set; }
    public int NowPrice { get; set; }
    public int ReBate { get; set; }
    public int LimitBuyNum { get; set; }
    public int LimitVip { get; set; }

    // <ActivityHalfLimitBuy ActivityHalfLimitBuyID="1" OpenDayCount="1" ItemID="90001" ItemNum="1000" Price="300" NowPrice="60" ReBate="2" LimitBuyNum="4000" LimitVip="0" />
    public ActivityHalfLimitBuy(int ActivityHalfLimitBuyID, int OpenDayCount, int ItemID, int ItemNum, int Price, int NowPrice, int ReBate, int LimitBuyNum, int LimitVip)
    {
        this.ActivityHalfLimitBuyID = ActivityHalfLimitBuyID;
        this.OpenDayCount = OpenDayCount;
        this.ItemID = ItemID;
        this.ItemNum = ItemNum;
        this.Price = Price;
        this.NowPrice = NowPrice;
        this.ReBate = ReBate;
        this.LimitBuyNum = LimitBuyNum;
        this.LimitVip =LimitVip;
    }
}
