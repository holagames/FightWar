using UnityEngine;
using System.Collections;

public class ActivitySevenLogin
{
    public int Day { get; set; }
    public int status = 0;
    public BetterList<AwardItem> AwardItemList = new BetterList<AwardItem>();
    public ActivitySevenLogin(int Day, BetterList<AwardItem> AwardItemList)
    {
        this.Day = Day;
        this.AwardItemList = AwardItemList;
    }
}
