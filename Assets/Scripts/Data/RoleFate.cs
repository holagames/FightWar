using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoleFate
{
    public int RoleFateID { get; private set; }
    public int RoleID { get; private set;}
    public string RoleName { get; private set; }
    public string Name { get; private set; }
    public int FateType { get; private set; }
    public string Des { get; private set; }
    public float Hp { get; private set; }
    public float Atk { get; private set; }
    //Def="0" Hit="0" NoHit="0" Crit="0" NoCrit="0" DmgBonus="0" DmgReduction="0"
    public List<int> FateIDList = new List<int>();
    public int FateID1 { get; private set; }
    public int FateID2 { get; private set; }
    public int FateID3 { get; private set; }
    public int FateID4 { get; private set; }
    public int FateID5 { get; private set; }
    public int FateID6 { get; private set; }
    public int FateID7 { get; private set; }
    public int FateID8 { get; private set; }

    public RoleFate(int _roleFateId, int _roleid, string _RoleName, string _Name, int FateType, string Des, float _hp, float _atk,List<int> FateIDList)
    {
        this.RoleFateID = _roleFateId;
        this.RoleID = _roleid;
        this.RoleName = _RoleName;
        this.Name = _Name;
        this.FateType = FateType;
        this.Des = Des;
        this.Hp = _hp;
        this.Atk = _atk;
        this.FateIDList = FateIDList;
      /*  this.FateID1 = FateID1;
        this.FateID2 = FateID2;
        this.FateID3 = FateID3;
        this.FateID4 = FateID4;
        this.FateID5 = FateID5;
        this.FateID6 = FateID6;
        this.FateID7 = FateID7;
        this.FateID8 = FateID8;*/
    }

}
