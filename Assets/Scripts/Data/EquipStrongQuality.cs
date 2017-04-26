using UnityEngine;
using System.Collections;

public class EquipStrongQuality
{
    public int EquipCode { get; private set; }      //装备ID
    public int Race { get; private set; }           //种族类别
    public int Part { get; private set; }           //部位
    public int Color { get; private set; }          //颜色
    public int Money { get; private set; }          //消耗金钱
    public int LevelCup { get; private set; }       //等级上限
    public BetterList<AwardItem> materialItemList = new BetterList<AwardItem>();

    public EquipStrongQuality(int _EquipCode, int _Race, int _Part, int _Color, int _Money, int _LevelCup, BetterList<AwardItem> materialItemList)
    {
        this.EquipCode = _EquipCode;
        this.Race = _Race;
        this.Part = _Part;
        this.Color = _Color;
        this.Money = _Money;
        this.LevelCup = _LevelCup;
        this.materialItemList = materialItemList;
    }
}
