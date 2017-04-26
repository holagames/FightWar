using UnityEngine;
using System.Collections;

/// <summary>
/// 七日活动表
/// </summary>

public class ActivitySevenRank {

    /// <summary>
    /// 活动id
    /// </summary>
    public int ActivitySevenRankID { get; private set; }

    /// <summary>
    /// 活动组
    /// </summary>
    public int Force { get; private set; }

    /// <summary>
    /// 奖励1
    /// </summary>
    public int BonusID1 { get; private set; }
    public int BonusNum1 { get; private set; }

    /// <summary>
    /// 奖励2
    /// </summary>
    public int BonusID2 { get; private set; }
    public int BonusNum2 { get; private set; }

    /// <summary>
    /// 奖励3
    /// </summary>
    public int BonusID3 { get; private set; }
    public int BonusNum3 { get; private set; }
    public BetterList<AwardItem> mAwardList = new BetterList<AwardItem>();

    public ActivitySevenRank(int _ActivitySevenRankID, int _Force, BetterList<AwardItem> mAwardList)
    {
        this.ActivitySevenRankID = _ActivitySevenRankID;
        this.Force = _Force;

        this.mAwardList = mAwardList;
    }

    public ActivitySevenRank(int _ActivitySevenRankID, int _Force, int _BonusID1, int _BonusNum1,
                           int _BonusID2, int _BonusNum2, int _BonusID3, int _BonusNum3, BetterList<AwardItem> mAwardList)
    {
        this.ActivitySevenRankID = _ActivitySevenRankID;
        this.Force = _Force;

        this.BonusID1 = _BonusID1;
        this.BonusNum1 = _BonusNum1;

        this.BonusID2 = _BonusID2;
        this.BonusNum2 = _BonusNum2;

        this.BonusID3 = _BonusID3;
        this.BonusNum3 = _BonusNum3;

        this.mAwardList = mAwardList;
    }
    
}
