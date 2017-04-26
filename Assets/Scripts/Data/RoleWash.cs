using UnityEngine;
using System.Collections;

public class RoleWash{

    public int RoleID { get; private set; }
    public int HpMax { get; private set; }
    public int AtkMax { get; private set; }
    public int DefMax { get; private set; }
    public float CriMax { get; private set; }
    public float UnCriMax { get; private set; }

    public RoleWash(int _roleid, int _hpmax, int _atkmax, int _defmax, float _crimax, float _uncrimax)
    {
        this.RoleID = _roleid;
        this.HpMax = _hpmax;
        this.AtkMax = _atkmax;
        this.DefMax = _defmax;
        this.CriMax = _crimax;
        this.UnCriMax = _uncrimax;
    }

}
