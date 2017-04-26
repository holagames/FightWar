using UnityEngine;
using System.Collections;

public class ActionEvent{

    public int ActionEventID;
    public int GroupID;
    public int GateID;
    public string GateName;
    public int NeedLevel;
    public int ForGateID;
    public int EnableDays;
    public int EnergyCost;
    public int TalkID;
    public int BonusID1;
    public int BonusNum1;
    public int BonusID2;
    public int BonusNum2;
    public int BonusID3;
    public int BonusNum3;
    public int BonusID4;
    public int BonusNum4;

    public ActionEvent(int _ActionEventID, int _GroupID, int _GateID, string _GateName, int _NeedLevel, int _EnableDays, int _EnergyCost, int _TalkID, int _BonusID1, int _BonusNum1, int _BonusID2, int _BonusNum2, int _BonusID3, int _BonusNum3, int _BonusID4, int _BonusNum4, int _ForGateID)
    {
        this.ActionEventID = _ActionEventID;
        this.GroupID = _GroupID;
        this.GateID = _GateID;
        this.GateName = _GateName;
        this.NeedLevel = _NeedLevel;
        this.ForGateID = _ForGateID;
        this.EnableDays = _EnableDays;
        this.EnergyCost = _EnergyCost;
        this.TalkID = _TalkID;
        this.BonusID1 = _BonusID1;
        this.BonusNum1 = _BonusNum1;
        this.BonusID2 = _BonusID2;
        this.BonusNum2 = _BonusNum2;
        this.BonusID3 = _BonusID3;
        this.BonusNum3 = _BonusNum3;
        this.BonusID4 = _BonusID4;
        this.BonusNum4 = _BonusNum4;
    }
}