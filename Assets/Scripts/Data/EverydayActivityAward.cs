using UnityEngine;
using System.Collections;

public class EverydayActivityAward
{
    public int GateActivityRewardID { get; private set; }
    public int GroupID { get; private set; }
    public int Depth { get; private set; }
    public int ActionTerm { get; private set; }
    public float ActionVal { get; private set; }
    public int ItemID1 { get; private set; }
    public int ItemNum1 { get; private set; }
    public int ItemID2 { get; private set; }
    public int ItemNum2 { get; private set; }
    public int ItemID3 { get; private set; }
    public int ItemNum3 { get; private set; }
    public int ItemID4 { get; private set; }
    public int ItemNum4 { get; private set; }

    public  EverydayActivityAward(int GateActivityRewardID, int GroupID, int Depth, int ActionTerm, float ActionVal, int ItemID1, int ItemNum1,
        int ItemID2, int ItemNum2, int ItemID3, int ItemNum3, int ItemID4, int ItemNum4)
    {
        this.GateActivityRewardID = GateActivityRewardID;
        this.GroupID = GroupID;
        this.Depth = Depth;
        this.ActionTerm = ActionTerm;
        this.ActionVal = ActionVal;
        this.ItemID1 = ItemID1;
        this.ItemNum1 = ItemNum1;
        this.ItemID2 = ItemID2;
        this.ItemNum2 = ItemNum2;
        this.ItemID3 = ItemID3;
        this.ItemNum3 = ItemNum3;
        this.ItemID4 = ItemID4;
        this.ItemNum4 = ItemNum4;
    }
}
