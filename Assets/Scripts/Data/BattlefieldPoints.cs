using UnityEngine;
using System.Collections;

public class BattlefieldPoints 
{
    public int ID { get; private set; }
    public int Points { get; private set; }

    public BetterList<Item> ItemList = new BetterList<Item>();

    public BattlefieldPoints(int ID, int Points, int ItemID1, int ItemNum1, int ItemID2, int ItemNum2, int ItemID3, int ItemNum3, int ItemID4, int ItemNum4, int ItemID5, int ItemNum5, int ItemID6, int ItemNum6, int ItemID7, int ItemNum7, int ItemID8, int ItemNum8, int ItemID9, int ItemNum9, int ItemID10, int ItemNum10) 
    {
        this.ID = ID;
        this.Points = Points;
        ItemList.Add(new Item(ItemID1, ItemNum1));
        ItemList.Add(new Item(ItemID2, ItemNum2));
        ItemList.Add(new Item(ItemID3, ItemNum3));
        ItemList.Add(new Item(ItemID4, ItemNum4));
        ItemList.Add(new Item(ItemID5, ItemNum5));
        ItemList.Add(new Item(ItemID6, ItemNum6));
        ItemList.Add(new Item(ItemID7, ItemNum7));
        ItemList.Add(new Item(ItemID8, ItemNum8));
        ItemList.Add(new Item(ItemID9, ItemNum9));
        ItemList.Add(new Item(ItemID10, ItemNum10));
    }
}
