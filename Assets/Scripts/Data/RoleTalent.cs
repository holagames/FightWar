using UnityEngine;
using System.Collections;

public class RoleTalent
{
    public int RoleTalentID { get; private set; }

    public int RoleID { get; private set; }

    public string Name { get; private set; }

    public string Des { get; private set; }

    public int BreachLevel { get; private set; }

    public int ValType { get; private set; }

    public int Area { get; private set; }

    public int Angry { get; private set; }

    public float Hp { get; private set; }

    public float Atk { get; private set; }

    public float Def { get; private set; }

    public float Hit { get; private set; }

    public float NoHit { get; private set; }

    public float Crit { get; private set; }

    public float NoCrit { get; private set; }

    public float DmgBonus { get; private set; }

    public float DmgReduction { get; private set; }

    public RoleTalent(int RoleTalentID,int RoleID, string Name, string Des, int BreachLevel, int ValType, int Area, int Angry, float Hp, float Atk, float Def, float Hit, float NoHit
        , float Crit, float NoCrit, float DmgBonus, float DmgReduction)
    {
        this.RoleTalentID = RoleTalentID;
        this.RoleID = RoleID;
        this.Name = Name;
        this.Des = Des;
        this.BreachLevel = BreachLevel;
        this.ValType = ValType;
        this.Area = Area;
        this.Angry = Angry;
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