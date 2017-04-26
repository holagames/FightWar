using UnityEngine;
using System.Collections;

public class WeaponUpStar {

    public int ID { get; private set; }
    public int WeaponID { get; private set; }
    public int Color { get; private set; }
    public int Star { get; private set; }
    public float Hp { get; private set; }
    public float Atk { get; private set; }
    public float Def { get; private set; }
    public float UpStarRand { get; private set; }
    public int NeedItemID1 { get; private set; }
    public int NeddItemNum1 { get; private set; }
    public WeaponUpStar(int _ID, int _WeaponID,  int _Color, int _Star, float _Hp, float _Atk, float _Def, float _UpStarRand, int _NeedItemID1, int _NeddItemNum1) 
    {
        this.ID = _ID;
        this.WeaponID = _WeaponID;
        this.Color = _Color;
        this.Star = _Star;
        this.Hp = _Hp;
        this.Atk = _Atk;
        this.Def = _Def;
        this.UpStarRand = _UpStarRand;
        this.NeedItemID1 = _NeedItemID1;
        this.NeddItemNum1 = _NeddItemNum1;
    }
}
