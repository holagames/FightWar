using UnityEngine;
using System.Collections;

public class WeaponUpClass{

    public int ID { get; private set; }
    public int WeaponID { get; private set; }
    public int UpClassType { get; private set; }
    public int Hp { get; private set; }
    public int Atk { get; private set; }
    public int Def { get; private set; }
    public int NeedGold { get; private set; }
    public int StoneNeedNum { get; private set; }
    public int RoleDebrisNum { get; private set; }

    public WeaponUpClass(int _ID, int _WeaponID, int _UpClassType, int _Hp, int _Atk, int _Def, int _NeedGold, int _StoneNeedNum, int _RoleDebrisNum) 
    {
        this.ID = _ID;
        this.WeaponID = _WeaponID;
        this.UpClassType = _UpClassType;
        this.Hp = _Hp;
        this.Atk = _Atk;
        this.Def = _Def;
        this.NeedGold = _NeedGold;
        this.StoneNeedNum = _StoneNeedNum;
        this.RoleDebrisNum = _RoleDebrisNum;
    }
}
