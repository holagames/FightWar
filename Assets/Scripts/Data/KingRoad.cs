using UnityEngine;
using System.Collections;


/// <summary>
/// 王者之路
/// </summary>
public class KingRoad {

    public int KingRank { get; private set; }
    public int RobotNum { get; private set; }
    public int PlayerNum { get; private set; }
    public int TounamentCycle { get; private set; }
    public int UpRank { get; private set; }
    public int DownRank { get; private set; }
    public int LeapforgFightRank { get; private set; }

    public string RankScope1 { get; private set; }
    public int Rank1Bonus1 { get; private set; }
    public int Rank1BonusNum1 { get; private set; }
    public int Rank1Bonus2 { get; private set; }
    public int Rank1BonusNum2 { get; private set; }


    public string RankScope2 { get; private set; }
    public int Rank2Bonus1 { get; private set; }
    public int Rank2BonusNum1 { get; private set; }
    public int Rank2Bonus2 { get; private set; }
    public int Rank2BonusNum2 { get; private set; }

    public string RankScope3 { get; private set; }
    public int Rank3Bonus1 { get; private set; }
    public int Rank3BonusNum1 { get; private set; }
    public int Rank3Bonus2 { get; private set; }
    public int Rank3BonusNum2 { get; private set; }

    public string RankScope4 { get; private set; }
    public int Rank4Bonus1 { get; private set; }
    public int Rank4BonusNum1 { get; private set; }
    public int Rank4Bonus2 { get; private set; }
    public int Rank4BonusNum2 { get; private set; }

    public string RankScope5 { get; private set; }
    public int Rank5Bonus1 { get; private set; }
    public int Rank5BonusNum1 { get; private set; }
    public int Rank5Bonus2 { get; private set; }
    public int Rank5BonusNum2 { get; private set; }

    public string RankScope6 { get; private set; }
    public int Rank6Bonus1 { get; private set; }
    public int Rank6BonusNum1 { get; private set; }
    public int Rank6Bonus2 { get; private set; }
    public int Rank6BonusNum2 { get; private set; }

    public string RankScope7 { get; private set; }
    public int Rank7Bonus1 { get; private set; }
    public int Rank7BonusNum1 { get; private set; }
    public int Rank7Bonus2 { get; private set; }
    public int Rank7BonusNum2 { get; private set; }

    public KingRoad(int _KingRank, int _RobotNum, int _PlayerNum, int _TounamentCycle, int _UpRank, int _DownRank, int _LeapforgFightRank,
                    string _RankScope1, int _Rank1Bonus1, int _Rank1BonusNum1, int _Rank1Bonus2, int _Rank1BonusNum2,
                    string _RankScope2, int _Rank2Bonus1, int _Rank2BonusNum1, int _Rank2Bonus2, int _Rank2BonusNum2,
                    string _RankScope3, int _Rank3Bonus1, int _Rank3BonusNum1, int _Rank3Bonus2, int _Rank3BonusNum2,
                    string _RankScope4, int _Rank4Bonus1, int _Rank4BonusNum1, int _Rank4Bonus2, int _Rank4BonusNum2,
                    string _RankScope5, int _Rank5Bonus1, int _Rank5BonusNum1, int _Rank5Bonus2, int _Rank5BonusNum2,
                    string _RankScope6, int _Rank6Bonus1, int _Rank6BonusNum1, int _Rank6Bonus2, int _Rank6BonusNum2, 
                    string _RankScope7, int _Rank7Bonus1, int _Rank7BonusNum1, int _Rank7Bonus2, int _Rank7BonusNum2)
    {
        this.KingRank = _KingRank;
        this.RobotNum = _RobotNum;
        this.PlayerNum = _PlayerNum;
        this.TounamentCycle = _TounamentCycle;
        this.UpRank = _UpRank;
        this.DownRank = _DownRank;
        this.LeapforgFightRank = _LeapforgFightRank;

        this.RankScope1 = _RankScope1;
        this.Rank1Bonus1 = _Rank1Bonus1;
        this.Rank1BonusNum1 = _Rank1BonusNum1;
        this.Rank1Bonus2 = _Rank1Bonus2;
        this.Rank1BonusNum2 = _Rank1BonusNum2;

        this.RankScope2 = _RankScope2;
        this.Rank2Bonus1 = _Rank2Bonus1;
        this.Rank2BonusNum1 = _Rank2BonusNum1;
        this.Rank2Bonus2 = _Rank2Bonus2;
        this.Rank2BonusNum2 = _Rank2BonusNum2;

        this.RankScope3 = _RankScope3;
        this.Rank3Bonus1 = _Rank3Bonus1;
        this.Rank3BonusNum1 = _Rank3BonusNum1;
        this.Rank3Bonus2 = _Rank3Bonus2;
        this.Rank3BonusNum2 = _Rank3BonusNum2;

        this.RankScope4 = _RankScope4;
        this.Rank4Bonus1 = _Rank4Bonus1;
        this.Rank4BonusNum1 = _Rank4BonusNum1;
        this.Rank4Bonus2 = _Rank4Bonus2;
        this.Rank4BonusNum2 = _Rank4BonusNum2;

        this.RankScope5 = _RankScope5;
        this.Rank5Bonus1 = _Rank5Bonus1;
        this.Rank5BonusNum1 = _Rank5BonusNum1;
        this.Rank5Bonus2 = _Rank5Bonus2;
        this.Rank5BonusNum2 = _Rank5BonusNum2;

        this.RankScope6 = _RankScope6;
        this.Rank6Bonus1 = _Rank6Bonus1;
        this.Rank6BonusNum1 = _Rank6BonusNum1;
        this.Rank6Bonus2 = _Rank6Bonus2;
        this.Rank6BonusNum2 = _Rank6BonusNum2;

        this.RankScope7 = _RankScope7;
        this.Rank7Bonus1 = _Rank7Bonus1;
        this.Rank7BonusNum1 = _Rank7BonusNum1;
        this.Rank7Bonus2 = _Rank7Bonus2;
        this.Rank7BonusNum2 = _Rank7BonusNum2;
    }
}
