using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldBossReward
{
    public int WorldBossID { get; private set; }
    public float BloodPercent { get; private set; }
    public int BloodItem { get; private set; }
    public int BloodItemNum { get; private set; }
    public int WorldBossRank { get; private set; }

    public int RankItem1 { get; private set; }
    public int RankItemNum1 { get; private set; }
    public int RankItem2 { get; private set; }
    public int RankItemNum2 { get; private set; }
    public int RankItem3 { get; private set; }
    public int RankItemNum3 { get; private set; }
    public int RankItem4 { get; private set; }
    public int RankItemNum4 { get; private set; }
    public List<Item> RewardList=new List<Item>(); //{ get;set; }

    public WorldBossReward(int _WorldBossID, float _BloodPercent, int _BloodItem, int _BloodItemNum, int _WorldBossRank,
        int _RankItem1, int _RankItemNum1, int _RankItem2, int _RankItemNum2, int _RankItem3, int _RankItemNum3, int _RankItem4, int _RankItemNum4) 
    {
        this.WorldBossID = _WorldBossID;
        this.BloodPercent = _BloodPercent;
        this.BloodItem = _BloodItem;
        this.BloodItemNum = _BloodItemNum;
        this.WorldBossRank = _WorldBossRank;
        if (_RankItem1 != 0) 
        {
            RewardList.Add(new Item(_RankItem1, _RankItemNum1));
        }
        if (_RankItem2 != 0) 
        {
            RewardList.Add(new Item(_RankItem2, _RankItemNum2));
        }
        if (_RankItem3 != 0) 
        {
            RewardList.Add(new Item(_RankItem3, _RankItemNum3));
        }

        if (_RankItem4 != 0) 
        {
            RewardList.Add(new Item(_RankItem4, _RankItemNum4));
        }
    }
}
