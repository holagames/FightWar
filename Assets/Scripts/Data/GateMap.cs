using UnityEngine;
using System.Collections;

public class GateMap
{
    public int GateMapId;
    public int GateID;
    public string Name;
    public int MonsterID;
    public int MonsterLevel;


    public int MonsterRarity;
    public int SkillLevel;
    public int InitialRage;
    public int Scale;
    public int HpRate;
    public int AtkRate;
    public int DefRate;
    public int HitVal;
    public int NoHitVal;
    public int CritVal;
    public int NoCritVal;
    public int DmgBonusVal;
    public int DmgReductionVal;
    public int PosID;


    public GateMap(int _GateMapId, string _Name, int _GateID, int _MonsterID, int _Scale, int _MonsterLevel, int _MonsterRarity, int _SkillLevel, int _InitialRage, int _HpRate, int _AtkRate, int _DefRate, int _HitVal, int _NoHitVal, int _CritVal, int _NoCritVal, int _DmgBonusVal, int _DmgReductionVal, int _PosID)
    {
        this.GateMapId = _GateMapId;
        this.Name = _Name;
        this.GateID = _GateID;
        this.MonsterID = _MonsterID;
        this.Scale = _Scale;
        this.MonsterLevel = _MonsterLevel;
        this.MonsterRarity = _MonsterRarity;
        this.SkillLevel = _SkillLevel;
        this.InitialRage = _InitialRage;
        this.HpRate = _HpRate;
        this.AtkRate = _AtkRate;
        this.DefRate = _DefRate;
        this.HitVal = _HitVal;
        this.NoHitVal = _NoHitVal;
        this.CritVal = _CritVal;
        this.NoCritVal = _NoCritVal;
        this.DmgBonusVal = _DmgBonusVal;
        this.DmgReductionVal = _DmgReductionVal;
        this.PosID = _PosID;
    }
}
