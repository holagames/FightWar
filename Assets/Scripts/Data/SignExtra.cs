using UnityEngine;
using System.Collections;

public class SignExtra
{
    public int SignExtraID { get; set; }
    public int Day { get; set; }
    public BetterList<SignExtraItemData> SignExtraItemList = new BetterList<SignExtraItemData>();
    public int vipLv { get; set; }
    public SignExtra(int SignExtraID, int Day, BetterList<SignExtraItemData> SignExtraItemList, int vipLv)
    {
        this.SignExtraID = SignExtraID;
        this.Day = Day;
        this.SignExtraItemList = SignExtraItemList;
        this.vipLv = vipLv;
    }
}
public class SignExtraItemData
{
    public int ItemID { get; set; }
    public int ItemNum { get; set; }
    public SignExtraItemData(int ItemID, int ItemNum)
    {
        this.ItemID = ItemID;
        this.ItemNum = ItemNum;
    }
}
