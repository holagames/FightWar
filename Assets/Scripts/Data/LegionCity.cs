using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// 军团战城市类型资源
/// </summary>
public class LegionCity{
    public int CityID { get; private set; }
    public int CityType { get; private set; }
    public int LegionNeedLevel { get; private set; }
    public int ArmyNum { get; private set; }

    public int ItemID1 { get; private set; }
    public int ItemNum1 { get; private set; }
    public int ItemID2 { get; private set; }
    public int ItemNum2 { get; private set; }
    public int ItemID3 { get; private set; }
    public int ItemNum3 { get; private set; }


    public int MonsterHp { get; private set; }
    public int MonsterAtk { get; private set; }
    public int MonsterDef { get; private set; }

    public float MonsterHit { get; private set; }
    public float MonsterNoHit { get; private set; }
    public float MonsterCrit { get; private set; }
    public float MonsterNoCrit { get; private set; }
    public float MonsterDmgBonus { get; private set; }
    public float MonsterDmgReduction { get; private set; }
    public string CityName { get; private set; }
    //public List<Item> AwardList = new List<Item>();

    public LegionCity(int CityID, int CityType, int LegionNeedLevel, int ArmyNum, int ItemID1, int ItemNum1, int ItemID2, int ItemNum2, int ItemID3, int ItemNum3,
                        int MonsterHp, int MonsterAtk, int MonsterDef, float MonsterHit, float MonsterNoHit, float MonsterCrit, float MonsterNoCrit, float MonsterDmgBonus, float MonsterDmgReduction, string CityName) 
    {
        this.CityID = CityID;
        this.CityType = CityType;
        this.LegionNeedLevel = LegionNeedLevel;
        this.ArmyNum = ArmyNum;

        this.ItemID1 = ItemID1;
        this.ItemNum1 = ItemNum1;
        this.ItemID2 = ItemID2;
        this.ItemNum2 = ItemNum2;
        this.ItemID3 = ItemID3;
        this.ItemNum3 = ItemNum3;

        this.MonsterHp = MonsterHp;
        this.MonsterAtk = MonsterAtk;
        this.MonsterDef = MonsterDef;

        this.MonsterHit = MonsterHit;
        this.MonsterNoHit = MonsterNoHit;
        this.MonsterCrit = MonsterCrit;
        this.MonsterNoCrit = MonsterNoCrit;
        this.MonsterDmgBonus = MonsterDmgBonus;
        this.MonsterDmgReduction = MonsterDmgReduction;
        this.CityName = CityName;

        //AwardList.Add(new Item(ItemID1, ItemNum1));
        //AwardList.Add(new Item(ItemID2, ItemNum2));
        //AwardList.Add(new Item(ItemID3, ItemNum3));
    }
}
