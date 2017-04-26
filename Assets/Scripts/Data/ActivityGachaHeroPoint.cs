using UnityEngine;
using System.Collections;

public class ActivityGachaHeroPoint 
{  public int ID { get; private set; }
    public int ActivityID { get; private set; }
    public int Rank { get; private set; }
    public int Point { get; private set; }
    public int BonusID1 { get; private set; }
    public int BonusNum1 { get; private set; }
    public int BonusID2 { get; private set; }
    public int BonusNum2 { get; private set; }
    public int BonusID3 { get; private set; }
    public int BonusNum3 { get; private set; }

    public int BonusID4 { get; private set; }
    public int BonusNum4 { get; private set; }
    public int BonusID5 { get; private set; }
    public int BonusNum5 { get; private set; }

    public int BonusID6 { get; private set; }
    public int BonusNum6 { get; private set; }
    public int BonusID7 { get; private set; }
    public int BonusNum7 { get; private set; }
    public int BonusID8 { get; private set; }
    public int BonusNum8 { get; private set; }

    public ActivityGachaHeroPoint(int _ID, int _ActivityID,int _Rank, int _Point, int _BonusID1, int _BonusNum1, int _BonusID2, int _BonusNum2, int _BonusID3, int _BonusNum3, int _BonusID4, int _BonusNum4,
        int _BonusID5, int _BonusNum5, int _BonusID6, int _BonusNum6, int _BonusID7, int _BonusNum7, int _BonusID8, int _BonusNum8)
    {
        this.ID = _ID;
        this.ActivityID = _ActivityID;
        this.Rank = _Rank;
        this.Point = _Point;
        this.BonusID1 = _BonusID1;
        this.BonusNum1 = _BonusNum1;
        this.BonusID2 = _BonusID2;
        this.BonusNum2 = _BonusNum2;
        this.BonusID3 = _BonusID3;
        this.BonusNum3 = _BonusNum3;
        this.BonusID4 = _BonusID4;
        this.BonusNum4 = _BonusNum4;
        this.BonusID5 = _BonusID5;
        this.BonusNum5 = _BonusNum5;
        this.BonusID6 = _BonusID6;
        this.BonusNum6 = _BonusNum6;
        this.BonusID7 = _BonusID7;
        this.BonusNum7 = _BonusNum7;
        this.BonusID8 = _BonusID8;
        this.BonusNum8 = _BonusNum8;
    }
   
}
