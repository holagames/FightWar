using UnityEngine;
using System.Collections;

/// <summary>
/// 敢死队排行奖励类
/// </summary>
public class TowerRankReward {

    public int TowerRankRewardID { get; private set; }
    public int RankID { get; private set; }
    public int Gold { get; private set; }

    public int SLCoin { get; private set; }


    public TowerRankReward(int TowerRankRewardID, int RankID, int Gold, int SLCoin)
    {
        this.TowerRankRewardID = TowerRankRewardID;
        this.RankID = RankID;
        this.Gold = Gold;
        this.SLCoin = SLCoin;

    }
}
