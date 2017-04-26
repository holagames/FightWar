using UnityEngine;
using System.Collections;

public class SuperCar
{

    public int SuperCarID { get; private set; }
    public int Color { get; private set; }
    public int NeedLevel { get; private set; }
    public int NeedDebris { get; private set; }
    public int Speed { get; private set; }
    public int Hp { get; private set; }
    public int Atk { get; private set; }
    public int Def { get; private set; }

    public string Name { get; private set; }
    public SuperCar(int _SuperCarID, int _Color, int _NeedLevel, int _NeedDebris, int _Speed, int _Hp, int _Atk, int _Def,string _Name)
    {
        this.SuperCarID = _SuperCarID;
        this.Color = _Color;
        this.NeedLevel = _NeedLevel;
        this.NeedDebris = _NeedDebris;
        this.Speed = _Speed;
        this.Hp = _Hp;
        this.Atk = _Atk;
        this.Def = _Def;
        this.Name = _Name;     
    }
}
