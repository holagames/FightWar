using UnityEngine;
using System.Collections;

/// <summary>
/// 成就类
/// </summary>
public class Achievement {

    /// <summary>
    /// 成就id
    /// </summary>
    public int achievementID { get; private set;}

    /// <summary>
    /// 前置成就id
    /// </summary>
    public int preAchievement { get; private set; }

    /// <summary>
    /// 名字
    /// </summary>
    public string achievementName { get; private set; }

    /// <summary>
    /// 详细
    /// </summary>
    public string achievementDetail { get; private set; }

    /// <summary>
    /// 图片icon
    /// </summary>
    public int picCode { get; private set; }

    /// <summary>
    /// 奖励经验
    /// </summary>
    public int bonusExp { get; private set; }
    /// <summary>
    /// 奖励Gold
    /// </summary>
    public int bonusGold { get; private set; }
    /// <summary>
    /// 奖励砖石
    /// </summary>
    public int bonusDiamond { get; private set; }

    /// <summary>
    /// 类型 1：日常 0：成就
    /// </summary>
    public int resetTime { get; private set; }

    /// <summary>
    /// 其它奖励
    /// </summary>
    public BetterList<Item> bonusOther { get; private set; }

    /// <summary>
    /// 已完成的数量
    /// </summary>
    public int completeCount { get; private set; }

    /// <summary>
    /// 需要完成的总数
    /// </summary>
    public int totalCount { get; private set; }

    /// <summary>
    /// 是否已领取
    /// </summary>
    //public bool got { get; private set; }
    public int completeState { get; private set; }

    /// <summary>
    /// 是否达到等级
    /// </summary>
    //public bool got { get; private set; }
    public int OpenLevel { get; private set; }

    /// <summary>
    /// 是否达到关卡等级
    /// </summary>
    //public bool got { get; private set; }
    public int OpenGate { get; private set; }

    /// <summary>
    /// 是否达到Vip等级
    /// </summary>
    //public bool got { get; private set; }
    public int OpenVip { get; private set; }


    /// <summary>
    /// 活跃度
    /// </summary>
    public int HappyPoint { get; private set; }

    /// <summary>
    /// 成就/任务类型计数
    /// </summary>
    public int Type { get; private set; }


    /// <summary>
    /// 成就/任务完成条件，关卡用，totalCount设置1
    /// </summary>
    public int GatetotalCount { get; private set; }

    public Achievement(int newID, string newName, string newDetail,
        int newPicCode, int newAchievementDetail, int newBonusExp, int bonusGold, int bonusDiamond, BetterList<Item> newBonusOther, int newResetTime, int totalCount, int _OpenLevel,int _OpenGate,int _OpenVip,int _HappyPoint,int _Type)//, int _OpenLevel
    {
        this.achievementID = newID;
        this.achievementName = newName;
        this.achievementDetail = newDetail;
        this.picCode = newPicCode;
        this.preAchievement = newAchievementDetail;
        this.bonusExp = newBonusExp;
        this.bonusGold = bonusGold;
        this.bonusDiamond = bonusDiamond;
        this.bonusOther = newBonusOther;
        this.resetTime = newResetTime;

        this.completeCount = 0;
        this.completeState = 0;
        //this.totalCount = totalCount;

        this.OpenLevel = _OpenLevel;
        this.OpenGate = _OpenGate;
        this.OpenVip = _OpenVip;
        this.HappyPoint = _HappyPoint;
        this.Type = _Type;
        this.GatetotalCount = totalCount;
        if (Type == 20 || Type == 42)
        {
            this.totalCount = 1;
        }
        else 
        {
            this.totalCount = totalCount;
        }
    }


    public void SetCompletCount(int setCompleteCount, int setTotalCount, int setState,int type)
    {
        if ((type == 20 || type == 42) && setState == 1)
        {
            this.completeCount = 1;
            this.totalCount = 1;
        }
        else if ((type == 20 || type == 42) && setState == 0) 
        {
            this.completeCount = 0;
            this.totalCount = 1;
        }
        else
        {
            this.completeCount = setCompleteCount;
            this.totalCount = setTotalCount;
        }
        //this.completeCount = setCompleteCount;
        //this.totalCount = setTotalCount;
        this.completeState = setState;
    }
    public void SetCompletState(int gotState)
    {
        this.completeState = gotState;
        //switch (gotState)
        //{
        //    case 1: got = true; break;
        //    default: got = false; break;
        //}
    }
}
