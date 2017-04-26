using UnityEngine;
using System.Collections;

public class ResourceTycoon 
{
    public int ResourceTycoonID { get; private set; }
    public int ActivityID { get; private set; }
    public int Type { get; private set; }
    public int ActivitySheet { get; private set; }
    public int Sort { get; private set; }
    public int CumulativeItemID { get; private set; }
    public int CumulativeItemNum { get; private set; }
    public int CumulativePoints { get; private set; }

    public int ItemID1 { get; private set; }
    public int ItemNum1 { get; private set; }
    public int ItemID2 { get; private set; }
    public int ItemNum2 { get; private set; }
    public int ItemID3 { get; private set; }
    public int ItemNum3 { get; private set; }
    public int ItemID4 { get; private set; }
    public int ItemNum4 { get; private set; }

    public int GetState { get; private set; }
    public int GetNum { get; private set; }

    public ResourceTycoon(int ResourceTycoonID, int ActivityID, int Type, int ActivitySheet,
                          int Sort, int CumulativeItemID, int CumulativeItemNum, int CumulativePoints,
                          int ItemID1, int ItemNum1, int ItemID2, int ItemNum2,
                          int ItemID3, int ItemNum3, int ItemID4, int ItemNum4) 
    {
        this.ResourceTycoonID = ResourceTycoonID;
        this.ActivityID = ActivityID;
        this.Type = Type;
        this.ActivitySheet = ActivitySheet;
        this.Sort = Sort;
        this.CumulativeItemID = CumulativeItemID;
        this.CumulativeItemNum = CumulativeItemNum;
        this.CumulativePoints = CumulativePoints;

        this.ItemID1 = ItemID1;
        this.ItemNum1 = ItemNum1;

        this.ItemID2 = ItemID2;
        this.ItemNum2 = ItemNum2;

        this.ItemID3 = ItemID3;
        this.ItemNum3 = ItemNum3;

        this.ItemID4 = ItemID4;
        this.ItemNum4 = ItemNum4;
    }

    public void AddAwardState(int _GetState, int _GetNum) 
    {
        this.GetState = _GetState;
        this.GetNum = _GetNum;
    }
}
