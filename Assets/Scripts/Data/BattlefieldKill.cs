using UnityEngine;
using System.Collections;

public class BattlefieldKill
{
    public int ID { get; private set; }
    public int Rank { get; private set; }

    public BetterList<Item> BattlefieldKillList = new BetterList<Item>();

    public BattlefieldKill(int ID, int Rank, int ItemID1, int ItemNum1, int ItemID2, int ItemNum2, int ItemID3, int ItemNum3, int ItemID4, int ItemNum4)
    {
        this.ID = ID;
        this.Rank = Rank;
        BattlefieldKillList.Add(new Item(ItemID1, ItemNum1));
        BattlefieldKillList.Add(new Item(ItemID2, ItemNum2));
        BattlefieldKillList.Add(new Item(ItemID3, ItemNum3));
        BattlefieldKillList.Add(new Item(ItemID4, ItemNum4));
    }
}
