using UnityEngine;
using System.Collections;

public class LegionRank
{
    public int RankID { get; set; }
    public int RankNum { get; set; }
    public BetterList<AwardItem> AwardItemList = new BetterList<AwardItem>();
    public LegionRank(int RankID, int RankNum, BetterList<AwardItem> AwardItemList)
    {
        this.RankID = RankID;
        this.RankNum = RankNum;
        this.AwardItemList = AwardItemList;
    }
}


