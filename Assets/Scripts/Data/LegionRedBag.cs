using UnityEngine;
using System.Collections;

public class LegionRedBag
{
    public int LegionRedID { get; private set; }
    public int RedBagType { get; private set; }
    public int SendBagType { get; private set; }
    public int ItemIcon { get; private set; }
    public string ItemName { get; private set; }
    public int Level { get; private set; }
    public int VipLimit { get; private set; }
    public int Diamond { get; private set; }
    public int RewardItemId1 { get; private set; }
    public int RewardItemNum1 { get; private set; }
    public int RewardItemId2 { get; private set; }
    public int RewardItemNum2 { get; private set; }
    public int RewardItemId3 { get; private set; }
    public int RewardItemNum3 { get; private set; }

    public int RedBagNum { get; private set; }
    public int LegionLevel { get; private set; }
    public int GetRewardItemId1 { get; private set; }
    public int GetRewardItemNum1 { get; private set; }
    public int GetRewardItemId2 { get; private set; }
    public int GetRewardItemNum2 { get; private set; }
    public int GetRewardItemId3 { get; private set; }
    public int GetRewardItemNum3 { get; private set; }

    public LegionRedBag(int LegionRedID, int RedBagType, int SendBagType, int ItemIcon, string ItemName, int Level, int VipLimit,
                        int Diamond,int RewardItemId1,int RewardItemNum1,int RewardItemId2,int RewardItemNum2,
                        int RewardItemId3,int RewardItemNum3,int RedBagNum,int LegionLevel,int GetRewardItemId1,
                        int GetRewardItemNum1,int GetRewardItemId2,int GetRewardItemNum2,int GetRewardItemId3,int GetRewardItemNum3) 
    {
        this.LegionRedID = LegionRedID;
        this.RedBagType = RedBagType;
        this.SendBagType = SendBagType;
        this.ItemIcon = ItemIcon;
        this.ItemName = ItemName;
        this.Level = Level;
        this.VipLimit = VipLimit;
        this.Diamond = Diamond;

        this.RewardItemId1 = RewardItemId1;
        this.RewardItemNum1 = RewardItemNum1;

        this.RewardItemId2 = RewardItemId2;
        this.RewardItemNum2 = RewardItemNum2;

        this.RewardItemId3 = RewardItemId3;
        this.RewardItemNum3 = RewardItemNum3;

        this.RedBagNum = RedBagNum;
        this.LegionLevel = LegionLevel;

        this.GetRewardItemId1 = GetRewardItemId1;
        this.GetRewardItemNum1 = GetRewardItemNum1;

        this.GetRewardItemId2 = GetRewardItemId2;
        this.GetRewardItemNum2 = GetRewardItemNum2;

        this.GetRewardItemId3 = GetRewardItemId3;
        this.GetRewardItemNum3 = GetRewardItemNum3;
    }
}
