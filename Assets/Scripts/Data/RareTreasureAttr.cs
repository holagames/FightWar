using UnityEngine;
using System.Collections;

public class RareTreasureAttr
{
    public int RareTreasureID { get;  set; }
    public int Color { get;  set; }

    public int Hp { get; set; }
    public int Atk { get; set; }
    public int Def { get; set; }
    public float Hit { get; set; }
    public float NoHit { get; set; }
    public float Crit { get; set; }
    public float NoCrit { get; set; }
    public float DmgBonus { get; set; }
    public float DmgReduction { get; set; }
    public RareTreasureAttr(int RareTreasureID, int Color, int Hp, int Atk, int Def, float Hit, float NoHit, float Crit, float NoCrit, float DmgBonus, float DmgReduction)
    {
        this.RareTreasureID = RareTreasureID;
        this.Color = Color;
        this.Hp = Hp;
        this.Atk = Atk;
        this.Def = Def;
        this.Hit = Hit;
        this.NoHit = NoHit;
        this.Crit = Crit;
        this.NoCrit = NoCrit;
        this.DmgBonus = DmgBonus;
        this.DmgReduction = DmgReduction;
    }
    
}
