using UnityEngine;
using System.Collections;

public class RoleGrow
{
    public int RoleID { get; private set; }

    public string Name { get; private set; }

    public int Rarity { get; private set; }

    public int Star { get; private set; }

    public int Hp { get; private set; }

    public int Atk { get; private set; }

    public int Def { get; private set; }

    public int Hit { get; private set; }

    public int NoHit { get; private set; }

    public int Crit { get; private set; }

    public int NoCrit { get; private set; }

    public int DmgBonus { get; private set; }

    public int DmgReduction { get; private set; }

    public RoleGrow(int _roleId, string _name, int _rarity, int _star, int _hp, int _atk, int _def, int _hit, int _noHit, int _crit, int _noCrit, int _dmgBonus, int _dmgReduction)
    {
        this.RoleID = _roleId;
        this.Name = _name;
        this.Rarity = _rarity;
        this.Star = _star;
        this.Hp = _hp;
        this.Atk = _atk;
        this.Def = _def;
        this.Hit = _hit;
        this.NoHit = _noHit;
        this.Crit = _crit;
        this.NoCrit = _noCrit;
        this.DmgBonus = _dmgBonus;
        this.DmgReduction = _dmgReduction;
    }
}
