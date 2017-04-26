using UnityEngine;
using System.Collections;


/// <summary>
/// 活跃度宝箱
/// </summary>
public class HappyBox
{
    public int HappyBoxID { get; private set; }
    public int Point { get; private set; }

    public int ItemID1 { get; private set; }
    public int ItemNum1 { get; private set; }

    public int ItemID2 { get; private set; }
    public int ItemNum2 { get; private set; }

    public int ItemID3 { get; private set; }
    public int ItemNum3 { get; private set; }

    public int ItemID4 { get; private set; }
    public int ItemNum4 { get; private set; }

    public int ItemID5 { get; private set; }
    public int ItemNum5 { get; private set; }

    public int ItemID6 { get; private set; }
    public int ItemNum6 { get; private set; }


    public HappyBox(int _HappyBoxID, int _Point, int _ItemID1, int _ItemNum1, int _ItemID2, int _ItemNum2, int _ItemID3, int _ItemNum3, int _ItemID4, int _ItemNum4, int _ItemID5, int _ItemNum5, int _ItemID6, int _ItemNum6) 
    {
        this.HappyBoxID = _HappyBoxID;
        this.Point = _Point;

        this.ItemID1 = _ItemID1;
        this.ItemNum1 = _ItemNum1;

        this.ItemID2 = _ItemID2;
        this.ItemNum2 = _ItemNum2;

        this.ItemID3 = _ItemID3;
        this.ItemNum3 = _ItemNum3;

        this.ItemID4 = _ItemID4;
        this.ItemNum4 = _ItemNum4;

        this.ItemID5 = _ItemID5;
        this.ItemNum5 = _ItemNum5;

        this.ItemID6 = _ItemID6;
        this.ItemNum6 = _ItemNum6;
    }

}
