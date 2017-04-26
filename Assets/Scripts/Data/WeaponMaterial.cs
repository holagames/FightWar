using UnityEngine;
using System.Collections;

public class WeaponMaterial {

    public int WeaponID { get; private set; }
    public int NeedDebris { get; private set; }

    public WeaponMaterial(int _WeaponID, int _NeedDebris) 
    {
        this.WeaponID = _WeaponID;
        this.NeedDebris = _NeedDebris;

    }
}
