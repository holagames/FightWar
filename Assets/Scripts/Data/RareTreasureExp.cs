using UnityEngine;
using System.Collections;

public class RareTreasureExp
{
    public int RareTreasureExpID { get; set; }
    public int Color { get;  set; }

    public int Level { get; set; }
    public int NeedExp { get; set; }
    public int NeedLevel { get; set; }

    public RareTreasureExp(int RareTreasureExpID, int Color, int Level, int NeedExp, int NeedLevel)
    {
        this.RareTreasureExpID = RareTreasureExpID;
        this.Color = Color;
        this.Level = Level;
        this.NeedExp = NeedExp;
        this.NeedLevel = NeedLevel;
    }
    
}
