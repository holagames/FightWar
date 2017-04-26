using UnityEngine;
using System.Collections;

/// <summary>
/// 登陆送Vip类
/// </summary>
public class ActivityBonusVip 
{
    public int BonusVipID { get; private set; }
    public int ActivityID { get; private set; }
    public int CovertType { get; private set; }

    public int Param1 { get; private set; }
    public int Param2 { get; private set; }
    public string Des { get; private set; }
    public int CovertCount { get; private set; }


    public int ItemID1 { get; private set; }
    public int ItemNum1 { get; private set; }
    public int ItemID2 { get; private set; }
    public int ItemNum2 { get; private set; }
    public int ItemID3 { get; private set; }
    public int ItemNum3 { get; private set; }
    public int ItemID4 { get; private set; }
    public int ItemNum4 { get; private set; }

    public ActivityBonusVip(int _BonusVipID, int _ActivityID, int _CovertType, int _Param1, int _Param2, string _Des, int _CovertCount
        , int _ItemID1, int _ItemNum1,  int _ItemID2, int _ItemNum2, int _ItemID3, int _ItemNum3, int _ItemID4, int _ItemNum4)
    {
        this.BonusVipID = _BonusVipID;
        this.ActivityID = _ActivityID;
        this.CovertType = _CovertType;
        this.Param1 = _Param1;
        this.Param2 = _Param2;
        this.Des = _Des;
        this.CovertCount = _CovertCount;
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
