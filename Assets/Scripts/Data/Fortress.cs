using UnityEngine;
using System.Collections;

public class Fortress
{
    public int ID {  get; private set; }
    public int Type { get; private set; }
    public int Level { get; private set; }
    public int BulidLevel { get; private set; }
    public int NeedItemId { get; private set; }
    public int NeedItemNum { get; private set; }
    public int BonusItemId { get; private set; }
    public float BonusItemNum { get; private set; }
    public float FriendBase { get; private set; }
    public float FriendGrow { get; private set; }
    public Fortress(int _ID, int _Type, int _Level, int _BulidLevel, int _NeedItemID, int _NeddItemNum, int _BonusItemId, float _BonusItemNum, float _FriendBase, float _FriendGrow)
    {
        this.ID = _ID;
        this.Type = _Type;
        this.Level = _Level;
        this.BulidLevel = _BulidLevel;
        this.NeedItemId = _NeedItemID;
        this.NeedItemNum = _NeddItemNum;
        this.BonusItemId = _BonusItemId;
        this.BonusItemNum = _BonusItemNum;
        this.FriendBase = _FriendBase;
        this.FriendGrow = _FriendGrow;
    }

}
