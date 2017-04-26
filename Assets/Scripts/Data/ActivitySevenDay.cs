using UnityEngine;
using System.Collections;

/// <summary>
/// 七日活动表
/// </summary>

public class ActivitySevenDay {

    /// <summary>
    /// 活动id
    /// </summary>
    public int ActivitySevenDayID { get; private set; }

    /// <summary>
    /// 活动组
    /// </summary>
    public int DayCount { get; private set; }

    /// <summary>
    /// 奖励组
    /// </summary>
    public int BonusGroupID { get; private set; }

    /// <summary>
    /// 奖励1
    /// </summary>
    public int ItemID1 { get; private set; }
    public int ItemNum1 { get; private set; }

    /// <summary>
    /// 奖励2
    /// </summary>
    public int ItemID2 { get; private set; }
    public int ItemNum2 { get; private set; }

    /// <summary>
    /// 奖励3
    /// </summary>
    public int ItemID3 { get; private set; }
    public int ItemNum3 { get; private set; }

    /// <summary>
    /// 奖励4
    /// </summary>
    public int ItemID4 { get; private set; }
    public int ItemNum4 { get; private set; }

    /// <summary>
    /// 限制类型
    /// </summary>
    public int LimitType { get; private set; }

    /// <summary>
    /// 限制条件
    /// </summary>
    public string LimitTerm { get; private set; }

    /// <summary>
    /// 是否已领取
    /// </summary>
    public int CompleteState { get; private set; }

    /// <summary>
    /// 已完成的数量
    /// </summary>
    public int CompleteCount { get; private set; }

    public ActivitySevenDay(int _ActivitySevenDayID, int _DayCount, int _BonusGroupID, int _ItemID1, int _ItemNum1,
                            int _ItemID2, int _ItemNum2, int _ItemID3, int _ItemNum3, int _ItemID4, int _ItemNum4, int _LimitType, string _LimitTerm)
    {
        this.ActivitySevenDayID = _ActivitySevenDayID;
        this.DayCount = _DayCount;
        this.BonusGroupID = _BonusGroupID;

        this.ItemID1 = _ItemID1;
        this.ItemNum1 = _ItemNum1;

        this.ItemID2 = _ItemID2;
        this.ItemNum2 = _ItemNum2;

        this.ItemID3 = _ItemID3;
        this.ItemNum3 = _ItemNum3;

        this.ItemID4 = _ItemID4;
        this.ItemNum4 = _ItemNum4;

        this.LimitType = _LimitType;
        this.LimitTerm = _LimitTerm;

        this.CompleteCount = 0;
    }

    public void SetCompletState(int gotState, int _CompleteCount)
    {
        this.CompleteState = gotState;
        this.CompleteCount = _CompleteCount;
    }
}
