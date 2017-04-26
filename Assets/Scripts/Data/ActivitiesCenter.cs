using UnityEngine;
using System.Collections;

/// <summary>
/// 活动开放列表类
/// </summary>
public class ActivitiesCenter
{
    public int ActivitiesCenterID { get; private set; }
    public int ActivityID { get; private set; }
    public string Name { get; private set; }

    public int Type { get; private set; }
    public int TimeType { get; private set; }
    public int DayCount { get; private set; }
    public int TimeCycle { get; private set; }
    public string StartDate { get; private set; }
    public string EndDate { get; private set; }
    public string StartTime { get; private set; }
    public string EndTime { get; private set; }
    public string SpacialTime { get; private set; }
    public string RewardEndTime { get; private set; }

    public int IsPicture { get; private set; }
    public int ShowTimeType { get; private set; }
    public string Title { get; private set; }
    public string Des { get; private set; }
    public int NewServiceActivites { get; private set; }
    public ActivitiesCenter(int _ActivitiesCenterID, int _ActivityID, string _Name, int _Type, int _TimeType, int _DayCount, int _TimeCycle
        , string _StartDate, string _EndDate, string _StartTime, string _EndTime, string _SpacialTime, string _RewardEndTime, int _IsPicture, int _ShowTimeType, string _Title, string _Des, int _NewServiceActivites)
    {
        this.ActivitiesCenterID = _ActivitiesCenterID;
        this.ActivityID = _ActivityID;
        this.Name = _Name;
        this.Type = _Type;
        this.TimeType = _TimeType;
        this.DayCount = _DayCount;
        this.TimeCycle = _TimeCycle;
        this.StartDate = _StartDate;
        this.EndDate = _EndDate;
        this.StartTime = _StartTime;
        this.EndTime = _EndTime;
        this.SpacialTime = _SpacialTime;
        this.RewardEndTime = _RewardEndTime;
        this.IsPicture = _IsPicture;
        this.ShowTimeType = _ShowTimeType;
        this.Title = _Title;
        this.Des = _Des;
        this.NewServiceActivites = _NewServiceActivites;
    }
}
