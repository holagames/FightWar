using UnityEngine;
using System.Collections;

public class FightTalk
{
   
    public int FightTalkID { get; private set; }
    public int GateID { get; private set; }
    public int RoleID { get; private set; }
    public int TalkType { get; private set; }
    public int TalkKind { get; private set; }
    public int TalkRound { get; private set; }
    public string Talk { get; private set; }

    public FightTalk(int _FightTalkID, int _GateID, int _RoleID, int _Type, int _Kind, int _Round, string _Talk)
    {
        this.FightTalkID = _FightTalkID;
        this.GateID = _GateID;
        this.RoleID = _RoleID;
        this.TalkType = _Type;
        this.TalkKind = _Kind;
        this.Talk = _Talk;
        this.TalkRound = _Round;
    }
}
