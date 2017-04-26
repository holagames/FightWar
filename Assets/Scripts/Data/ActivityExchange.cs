using UnityEngine;
using System.Collections;

/// <summary>
/// 充值活动类
/// </summary>
public class ActivityExchange 
{
    public int ExchangeID { get; private set; }
    public int ActivityID { get; private set; }

    public int GroupID { get; private set; }
    public string Des {  get; private set; }
    public int Type { get; private set; }
    public int Condition { get; private set; }
    public int Point { get; private set; }
    public int ItemID1 { get; private set; }
    public int ItemNum1 { get; private set; }
    public int ItemID2 { get; private set; }
    public int ItemNum2 {  get; private set; }
    public int ItemID3 { get; private set; }
    public int ItemNum3 { get; private set; }
    public int ItemID4 { get; private set; }
    public int ItemNum4 { get; private set; }

    public ActivityExchange(int _ExchangeID, int _ActivityID, int _GroupID, string _Des, int _Type, int _Condition, int _Point,
        int _ItemID1, int _ItemNum1, int _ItemID2, int _ItemNum2, int _ItemID3, int _ItemNum3, int _ItemID4, int _ItemNum4)
    {
        this.ExchangeID = _ExchangeID;
        this.ActivityID = _ActivityID;
        this.GroupID = _GroupID;
        this.Des = _Des;
        this.Type = _Type;
        this.Condition = _Condition;
        this.Point = _Point;
        this.ItemID1 = _ItemID1;
        this.ItemNum1 = _ItemNum1;
        this.ItemID2 = _ItemID2;
        this.ItemNum2 = _ItemNum2;
        this.ItemID3 = _ItemID3;
        this.ItemNum3 = _ItemNum3;
        this.ItemID4 = _ItemID4;
        this.ItemNum4 = _ItemNum4;
    }
}
