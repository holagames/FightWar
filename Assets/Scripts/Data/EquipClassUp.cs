using UnityEngine;
using System.Collections;

public class EquipClassUp
{
    public int EquipID { get; private set; }
    public string Name { get; private set; }
    public int ClassLevel { get; private set; }
    public int NeedItemID { get; private set; }
    public int NeedItemNumber { get; private set; }
    public int NeedItemClass { get; private set; }
    public int NeedCoin { get; private set; }
    public int ADAttack { get; private set; }
    public int APAttack { get; private set; }
    public int ADDenfance { get; private set; }
    public int APDenfance { get; private set; }
    public int HP { get; private set; }
    public int add_ADAttack { get; private set; }
    public int add_APAttack { get; private set; }
    public int add_ADDenfance { get; private set; }
    public int add_APDenfance { get; private set; }
    public int add_HP { get; private set; }

    public EquipClassUp(int _equipID, string _name, int _classLevel, int _needitemID, int _needitemNumber, int _needitemClass, int _needCoin,
                        int _adattack, int _apattack, int _addenfance, int _apdenfance, int _hp, int _add_adattack, int _add_apattack, int _add_addenfance
        , int _add_apdenfance, int _add_hp)
    {
        EquipID = _equipID;
        Name = _name;
        ClassLevel = _classLevel;
        NeedItemID = _needitemID;
        NeedItemNumber = _needitemNumber;
        NeedItemClass = _needitemClass;
        NeedCoin = _needCoin;
        ADAttack = _adattack;
        APAttack = _apattack;
        ADDenfance = _addenfance;
        APDenfance = _apdenfance;
        HP = _hp;
        add_ADAttack = _add_adattack;
        add_APAttack = _add_apattack;
        add_ADDenfance = _add_addenfance;
        add_APDenfance = _add_apdenfance;
        add_HP = _add_hp;
    }
}
