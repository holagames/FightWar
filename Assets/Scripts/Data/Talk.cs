using UnityEngine;
using System.Collections;

/// <summary>
/// 世界事件对话类
/// </summary>
public class Talk
{

    public int TalkID { get; private set; }

    public int InTurnID { get; private set; }
    public int LeftType { get; private set; }
    public string LeftDialog { get; private set; }
    public int RightType { get; private set; }
    public string RightDialog { get; private set; }

    public Talk(int _TalkID, int _InTurnID, int _LeftType, string _LeftDialog, int _RightType, string _RightDialog)
    {
        this.TalkID = _TalkID;
        this.InTurnID = _InTurnID;
        this.LeftType = _LeftType;
        this.LeftDialog = _LeftDialog;
        this.RightType = _RightType;
        this.RightDialog = _RightDialog;
    }
}
