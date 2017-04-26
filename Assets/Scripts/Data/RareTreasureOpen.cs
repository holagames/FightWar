using UnityEngine;
using System.Collections;

public class RareTreasureOpen
{
    public int posId { get;  set; }
    public int openLevel { get;  set; }

    public int state { get; set; }// 0 - 未解锁； 1 - 解锁，未装备； 2 - 已装备
    public int stoneId { get; set; }
    public int stoneLevel { get; set; }
    public int stoneExp { get; set; }
    public RareTreasureOpen(int posId, int openLevel)
    {
        this.posId = posId;
        this.openLevel = openLevel;
    }
    public void SetRareTreasureOpen(int stoneId, int stoneLevel, int stoneExp)
    {
        this.stoneId = stoneId;
        this.stoneLevel = stoneLevel;
        this.stoneExp = stoneExp;
        if (CharacterRecorder.instance.level < openLevel)
        {
            state = 0;
        }
        else if (stoneId != 0)
        {
            state = 2;
        }
        else
        {
            state = 1;
        }
        //Debug.LogError("state....." + state);
    }
    public void RemoveRareTreasure(int posId)
    {
        this.stoneId = 0;
        this.stoneLevel = 0;
        this.stoneExp = 0;
        this.state = 1;
        //Debug.LogError("state....." + state);
    }
}
