using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoleClassUp
{

    public int ID { get; private set; }
    public string Name { get; private set; }
    public int Levelcap { get; private set; }               //限制等级
    public int NeedMoney { get; private set; }              //金币
    public int Color { get; private set; }                  //品质等级
    public List<Item> NeedItemList = new List<Item>();

    public RoleClassUp(int _id, string _name, int _levelcup, int _needMoney,int _color, int itemID1, int itemCount1, int itemID2, int itemCount2, int itemID3, int itemCount3, int itemID4, int itemCount4)
    {
        ID = _id;
        Name = _name;
        Levelcap = _levelcup;
        NeedMoney = _needMoney;
        Color = _color;
        Item item1 = new Item(itemID1, itemCount1);
        NeedItemList.Add(item1);
        Item item2 = new Item(itemID2, itemCount2);
        NeedItemList.Add(item2);
        Item item3 = new Item(itemID3, itemCount3);
        NeedItemList.Add(item3);
        Item item4 = new Item(itemID4, itemCount4);
        NeedItemList.Add(item4);
    }

}
