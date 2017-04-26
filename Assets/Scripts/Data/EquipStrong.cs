using UnityEngine;
using System.Collections;

public class EquipStrong
{
    public int EquipId { get; private set; }
    public string Name { get; private set; }
    public int Race { get; private set; }
    public int Part { get; private set; }
    public int EquipColor { get; private set; }
    public int Icon { get; private set; }
    public int LvMax { get; private set; }
    public int Hp { get; private set; }
    public int Atk { get; private set; }
    public int Def { get; private set; }
    public float Hit { get; private set; }
    public float NoHit { get; private set; }
    public float Crit { get; private set; }
    public float NoCrit { get; private set; }
    public float DmgBonus { get; private set; }
    public float DmgReduction { get; private set; }

    public EquipStrong(int _EquipId, string _Name, int _Race, int _Part, int _EquipColor, int _Icon, int _LvMax,
                        int _Hp, int _Atk, int _Def, float _Hit, float _NoHit, float _Crit, float _NoCrit, float _DmgBonus, float _DmgReduction)
    {
        EquipId = _EquipId;
        Name = _Name;
        Race = _Race;
        Part = _Part;
        EquipColor = _EquipColor;
        Icon = _Icon;
        LvMax = _LvMax;
        Hp = _Hp;
        Atk = _Atk;
        Def = _Def;
        Hit = _Hit;
        NoHit = _NoHit;
        Crit = _Crit;
        NoCrit = _NoCrit;
        DmgBonus = _DmgBonus;
        DmgReduction = _DmgReduction;
    }
}
