using UnityEngine;
using System.Collections;

public class WorldBoss {

    public int MonsterLevel { get; private set; }
    public int Icon{ get; private set; }
    public float Blood{ get; private set; }
    public int LuckyBoxID{ get; private set; }
    public float LuckyBoxRank { get; private set; }

    public WorldBoss(int _MonsterLevel, int _Icon, float _Blood, int _LuckyBoxID, float _LuckyBoxRank) 
    {
        this.MonsterLevel = _MonsterLevel;
        this.Icon = _Icon;
        this.Blood = _Blood;
        this.LuckyBoxID = _LuckyBoxID;
        this.LuckyBoxRank = _LuckyBoxRank;
    }
}
