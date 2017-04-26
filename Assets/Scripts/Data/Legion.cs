using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Legion
{
    public int Level;//{ get; set; }
    public int NeedExp;//{ get; set; }
    public int LimitNum;// { get; set; }
    public int CrapsPlayNum;
    public int CrapsChangeNum;
    public int ViceKing;
    public int CreamNum;
    public BetterList<int> ScheduleValList = new BetterList<int>();
    public BetterList<AwardItem> BoxAwardList = new BetterList<AwardItem>();
    public Legion(int Level, int NeedExp, int LimitNum, BetterList<int> _ScheduleValList, BetterList<AwardItem> _BoxAwardList, int _CrapsPlayNum, int _CrapsChangeNum, int _ViceKing, int _CreamNum)
    {
        this.Level = Level;
        this.NeedExp = NeedExp;
        this.LimitNum = LimitNum;
        this.ScheduleValList = _ScheduleValList;
        this.BoxAwardList = _BoxAwardList;
        this.CrapsPlayNum = _CrapsPlayNum;
        this.CrapsChangeNum=_CrapsChangeNum;
        this.ViceKing = _ViceKing;
        this.CreamNum = _CreamNum;
    }
    
}
