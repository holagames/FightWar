using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionTask
{
    public int LegionTaskID{ get; set; }
    public string Name{ get; set; }
    public int Color{ get; set; }
    public int Rand { get; set; }
    public int TermType { get; set; }
    public int Param1 { get; set; }
    public int ParamVal1 { get; set; }
    public int ExpBonus { get; set; }
    public BetterList<AwardItem> BoxAwardList = new BetterList<AwardItem>();
    public LegionTask(int LegionTaskID, string Name, int Color, int Rand,int TermType,int Param1,int ParamVal1,int ExpBonus, BetterList<AwardItem> _BoxAwardList)
    {
        this.LegionTaskID = LegionTaskID;
        this.Name = Name;
        this.Color = Color;
        this.Rand = Rand;
        this.TermType = TermType;
        this.Param1 = Param1;
        this.ParamVal1 = ParamVal1;
        this.ExpBonus = ExpBonus;
        this.BoxAwardList = _BoxAwardList;
    }
    
}
