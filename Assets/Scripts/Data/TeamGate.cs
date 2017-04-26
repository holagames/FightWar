using UnityEngine;
using System.Collections;

public class TeamGate {
    public int TeamGateID { get; private set; }
    public int GroupID { get; private set; }
    public string Name { get; private set; }
    public int NeedLevel { get; private set; }
    public int Force { get; private set; }
    public int BossID { get; private set; }
    public int ItemID1 { get; private set; }
    public int ItemID2 { get; private set; }
    public int ItemID3 { get; private set; }
    public int ItemID4 { get; private set; }
    public int ItemID5 { get; private set; }

    public TeamGate(int TeamGateID, int GroupID, string Name, int NeedLevel, int Force, int BossID, int ItemID1, int ItemID2, int ItemID3, int ItemID4, int ItemID5) 
    {
        this.TeamGateID = TeamGateID;
        this.GroupID = GroupID;
        this.Name = Name;
        this.NeedLevel = NeedLevel;
        this.Force = Force;
        this.BossID = BossID;
        this.ItemID1 = ItemID1;
        this.ItemID2 = ItemID2;
        this.ItemID3 = ItemID3;
        this.ItemID4 = ItemID4;
        this.ItemID5 = ItemID5;
    }
}
