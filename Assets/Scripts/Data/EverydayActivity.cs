using UnityEngine;
using System.Collections;

public class EverydayActivity
{
    public int GateActivityID { get; private set; }

    public int Type { get; private set; }

    public int Hard { get; private set; }
    public int GateID { get; private set; }
    public string Des { get; private set; }
    public int TimeCD { get; private set; }
    public int NeedLevel { get; private set; }
    public string OpenWeek { get; private set; }
    public string OpenDate { get; private set; }
    public int LmtFightCount { get; private set; }
    public int PosUI { get; private set; }
    public int RewardGroupID { get; private set; }
    public int ShowBonusID1 { get; private set; }
    public int ShowBonusNum1 { get; private set; }
    public int ShowBonusID2 { get; private set; }
    public int ShowBonusNum2 { get; private set; }
    public int ShowBonusID3 { get; private set; }
    public int ShowBonusNum3 { get; private set; }
    public int ShowBonusID4 { get; private set; }
    public int ShowBonusNum4 { get; private set; }

    public EverydayActivity(int GateActivityID, int Type, int Hard, int GateID, string Des, int TimeCD, int NeedLevel, string OpenWeek, string OpenDate, int LmtFightCount,
       int PosUI, int RewardGroupID, int ShowBonusID1, int ShowBonusNum1, int ShowBonusID2, int ShowBonusNum2, int ShowBonusID3, int ShowBonusNum3, int ShowBonusID4, int ShowBonusNum4)
    {
        this.GateActivityID = GateActivityID;
        this.Type = Type;
        this.Hard = Hard;
        this.GateID = GateID;
        this.Des = Des;
        this.TimeCD = TimeCD;
        this.NeedLevel = NeedLevel;
        this.OpenWeek = OpenWeek;
        this.OpenDate = OpenDate;
        this.LmtFightCount = LmtFightCount;
        this.PosUI = PosUI;
        this.RewardGroupID = RewardGroupID;
        this.ShowBonusID1 = ShowBonusID1;
        this.ShowBonusNum1 = ShowBonusNum1;
        this.ShowBonusID2 = ShowBonusID2;
        this.ShowBonusNum2 = ShowBonusNum2;
        this.ShowBonusID3 = ShowBonusID3;
        this.ShowBonusNum3 = ShowBonusNum3;
        this.ShowBonusID4 = ShowBonusID4;
        this.ShowBonusNum4 = ShowBonusNum4;

    }
}
