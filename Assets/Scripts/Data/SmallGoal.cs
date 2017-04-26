using UnityEngine;
using System.Collections;

public class SmallGoal
{
    public int SmallGoalID { get; private set; }
    public int Type { get; private set; }
    public int Quest { get; private set; }
    public int ItemID { get; private set; }
    public int ItemNum { get; private set; }

    public SmallGoal(int SmallGoalID, int Type, int Quest, int ItemID, int ItemNum) 
    {
        this.SmallGoalID = SmallGoalID;
        this.Type = Type;
        this.Quest = Quest;
        this.ItemID = ItemID;
        this.ItemNum = ItemNum;
    }
}
