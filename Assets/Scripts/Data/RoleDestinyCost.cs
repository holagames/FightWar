using UnityEngine;
using System.Collections;

public class RoleDestinyCost
{
    public int RoleDestinyLevel { get; set; }
    public int StoneMax { get; set; }
    public int StoneCost
    {
        get;
        set;
    }
    //public List<int> percentList = new List<int>();
    public BetterList<int> percentList = new BetterList<int>();
    public RoleDestinyCost(int level,int StoneMax, int stoneCost, BetterList<int> percentList)
    {
        this.RoleDestinyLevel = level;
        this.StoneMax = StoneMax;
        this.StoneCost = stoneCost;
        for (int i = 0; i < percentList.size;i++ )
        {
            this.percentList.Add(percentList[i]);
        }
        
    }
}
