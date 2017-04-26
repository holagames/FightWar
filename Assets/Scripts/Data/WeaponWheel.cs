using UnityEngine;
using System.Collections;

public class WeaponWheel 
{
    //<WeaponWheel ID="1" ItemID="55101" ItemNum="5" Rand="100" />
    public int ID { get;private set ;}
    public int ItemID { get; private set; }
    public int ItemNum { get; private set; }
    public int Rand { get; private set; }


    public WeaponWheel(int _ID,int _ItemID,int _ItemNum,int _Rand)
    {
        this.ID = _ID;
        this.ItemID = _ItemID;
        this.ItemNum = _ItemNum;
        this.Rand = _Rand;
    }
}
