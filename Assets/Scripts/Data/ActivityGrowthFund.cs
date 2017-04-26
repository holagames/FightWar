using UnityEngine;
using System.Collections;

/// <summary>
/// 全民基金类
/// </summary>
public class ActivityGrowthFund  
{
    public int GrowthFundID { get; private set; }
    public int Type { get; private set; }
    public int Condition { get; private set; }
    public string Des { get; private set; }
    public int ItemID { get; private set; }
    public int ItemNum { get; private set; }

    public ActivityGrowthFund(int _GrowthFundID, int _Type, int _Condition, string _Des, int _ItemID, int _ItemNum)
    {
        this.GrowthFundID = _GrowthFundID;
        this.Type = _Type;
        this.Condition = _Condition;
        this.Des = _Des;
        this.ItemID = _ItemID;
        this.ItemNum = _ItemNum;
    }
}
