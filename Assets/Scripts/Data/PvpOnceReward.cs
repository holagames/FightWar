using UnityEngine;
using System.Collections;

/// <summary>
/// PVP一次奖励类
/// </summary>
public class PvpOnceReward {

    public int PvpOnceRewardID { get; private set; }
    public int PvpRankID { get; private set; }
    public int Diamond { get; private set; }
    public int Gold { get; private set; }
    public int ItemID1 { get; private set; }
    public int ItemNum1 { get; private set; }
    public int ItemID2 { get; private set; }
    public int ItemNum2 { get; private set; }

    public PvpOnceReward(int _PvpOnceRewardID, int _PvpRankID, int _Diamond, int _Gold, int _ItemID1, int _ItemNum1, int _ItemID2,int _ItemNum2) 
    {
        this.PvpOnceRewardID = _PvpOnceRewardID;
        this.PvpRankID = _PvpRankID;
        this.Diamond = _Diamond;
        this.Gold = _Gold;
        this.ItemID1 = _ItemID1;
        this.ItemNum1 = _ItemNum1;
        this.ItemID2 = _ItemID2;
        this.ItemNum2 = _ItemNum2;
    }
}
