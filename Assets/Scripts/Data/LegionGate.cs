using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionGate
{
    public int LegionGateID;
    public int GateGroupID { get; set; }
    public int NextGateID;
    public int GateBoxID;
    public BetterList<int> BossBloodList = new BetterList<int>();
    public LegionGate(int LegionGateID, int GateGroupID, int NextGateID, int GateBoxID, BetterList<int> BossBloodList)
    {
        this.LegionGateID = LegionGateID;
        this.GateGroupID = GateGroupID;
        this.NextGateID = NextGateID;
        this.GateBoxID = GateBoxID;
        this.BossBloodList = BossBloodList;
    }
    
}
