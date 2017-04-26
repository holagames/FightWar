using UnityEngine;
using System.Collections;

public class Nation
{
    public int ID { get; private set; }
    public int NationType { get; private set; }
    public string OfficeName { get; private set; }
    public int RobotID { get; private set; }
    public int Condition { get; private set; }
    public int EveryDayCost { get; private set; }



    public int ItemID1 { get; private set; }
    public int ItemNum1 { get; private set; }
    public int ItemID2 { get; private set; }
    public int ItemNum2 { get; private set; }
    public int ItemID3 { get; private set; }
    public int ItemNum3 { get; private set; }
    public int ItemID4 { get; private set; }
    public int ItemNum4 { get; private set; }
    public int ItemID5 { get; private set; }
    public int ItemNum5 { get; private set; }
    public int ItemID6 { get; private set; }
    public int ItemNum6 { get; private set; }

    public float BattlefieldDamageBonus { get; private set; }
    public BetterList<Item> NationItemList=new BetterList<Item>();
    public Nation(int ID, int NationType, string OfficeName, int RobotID, int Condition, int EveryDayCost, int ItemID1, int ItemNum1, int ItemID2, int ItemNum2,
                    int ItemID3, int ItemNum3, int ItemID4, int ItemNum4, int ItemID5, int ItemNum5, int ItemID6, int ItemNum6, float BattlefieldDamageBonus) 
    {
        this.ID = ID;
        this.NationType = NationType;
        this.OfficeName = OfficeName;
        this.RobotID = RobotID;
        this.Condition = Condition;
        this.EveryDayCost = EveryDayCost;

        NationItemList.Add(new Item(ItemID1, ItemNum1));
        NationItemList.Add(new Item(ItemID2, ItemNum2));
        NationItemList.Add(new Item(ItemID3, ItemNum3));
        NationItemList.Add(new Item(ItemID4, ItemNum4));
        NationItemList.Add(new Item(ItemID5, ItemNum5));
        NationItemList.Add(new Item(ItemID6, ItemNum6));

        this.BattlefieldDamageBonus = BattlefieldDamageBonus;
    }
}
