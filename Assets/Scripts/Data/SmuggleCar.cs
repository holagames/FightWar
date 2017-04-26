using UnityEngine;
using System.Collections;

public class SmuggleCar
{
   
    public int SmuggleCarID { get; private set; }
    public int Speed { get; private set; }
    public float RobbedBonusRatio { get; private set; }
    public float Rand { get; private set; }
    public int Bonus1 { get; private set; }
    public int BonusNum1 { get; private set; }
    public int Bonus2 { get; private set; }
    public int BonusNum2 { get; private set; }
    public int Bonus3 { get; private set; }
    public int BonusNum3 { get; private set; }

    public SmuggleCar(int _SmuggleCarID, int _Speed, float _RobbedBonusRatio, float _Rand, int _Bonus1, int _BonusNum1, int _Bonus2, int _BonusNum2, int _Bonus3, int _BonusNum3)
    {
        this.SmuggleCarID = _SmuggleCarID;
        this.Speed = _Speed;
        this.RobbedBonusRatio = _RobbedBonusRatio;
        this.Rand = _Rand;
        this.Bonus1 = _Bonus1;
        this.BonusNum1 = _BonusNum1;
        this.Bonus2 = _Bonus2;
        this.BonusNum2 = _BonusNum2;
        this.Bonus3 = _Bonus3;
        this.BonusNum3 = _BonusNum3;
    }
}
