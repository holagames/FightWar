using UnityEngine;
using System.Collections;


/// <summary>
/// PVP奖励类
/// </summary>
public class PVPReward{

    public int PvpRewardID { get; private set; }
    public int RankID{ get; private set; }
    public int DiamondBonus { get; private set; }
    public int GoldBonus { get; private set; }
    public int RYCoinBonus { get; private set; }
    public int ItemID { get; private set; }
    public int ItemNum { get; private set; }

    public PVPReward(int _PvpRewardID, int _RankID, int _DiamondBonus, int _GoldBonus, int _RYCoinBonus, int _ItemID, int _ItemNum) 
    {
        this.PvpRewardID = _PvpRewardID;
        this.RankID = _RankID;
        this.DiamondBonus = _DiamondBonus;
        this.GoldBonus = _GoldBonus;
        this.RYCoinBonus = _RYCoinBonus;
        this.ItemID = _ItemID;
        this.ItemNum = _ItemNum;
    }
}
