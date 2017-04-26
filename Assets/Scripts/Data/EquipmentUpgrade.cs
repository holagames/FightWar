using UnityEngine;
using System.Collections;

public class EquipmentUpgrade  {

    public static EquipmentUpgrade currentEu;
    public const int MAX_MATERIAL_COUNT = 4; 

    /// <summary>
    /// 装备ID
    /// </summary>
    public int equipmentID { get; private set; }
    /// <summary>
    /// 装备名字
    /// </summary>
    public string equipmentName { get; private set; }
    /// <summary>
    /// 所需星币
    /// </summary>
    public int needCoin { get; private set; }
    /// <summary>
    /// 品阶
    /// </summary>
    public int equipmentClass { get; private set; }
    /// <summary>
    /// 进阶后品阶
    /// </summary>
    public int equipmentUpgradeClass { get; private set; }
    /// <summary>
    /// 当前等级
    /// </summary>
    public int equipmentLevel { get; private set; }
    /// <summary>
    /// 当前最大等级
    /// </summary>
    public int equipmentLevelCap { get; private set; }
    /// <summary>
    /// 进阶后最大等级
    /// </summary>
    public int equipmentUpgradeLevelCap { get; private set; }

    public int[] itemID = new int[MAX_MATERIAL_COUNT];                   //合成物品ID
    public int[] itemQuantity = new int[MAX_MATERIAL_COUNT];                   //合成物品数量


    public EquipmentUpgrade(int _equipmentID, int _needCoin, int _equipmentClass, int _equipmentLevel, int _equipmentLevelCap, 
        int _equipmentUpgradeLevelCap, int _equipmentUpgradeClass, int _item1ID, int _item1Quantity, int _item2ID, 
        int _item2Quantity, int _item3ID, int _item3Quantity, int _item4ID, int _item4Quantity)
    {
        EquipDetailInfo ei = TextTranslator.instance.GetEquipInfoByID(_equipmentID);
        //if (ei != null)
        //{
        //    this.equipmentName = ei.equipName;
        //}

        this.equipmentID = _equipmentID;
        this.needCoin = _needCoin;
        this.equipmentClass = _equipmentClass;
        this.equipmentLevel = _equipmentLevel;
        this.equipmentLevelCap = _equipmentLevelCap;
        this.equipmentUpgradeClass = _equipmentUpgradeClass;
        this.equipmentUpgradeLevelCap = _equipmentUpgradeLevelCap;
        this.itemID[0] = _item1ID;
        this.itemQuantity[0] = _item1Quantity;
        this.itemID[1] = _item2ID;
        this.itemQuantity[1] = _item2Quantity;
        this.itemID[2] = _item3ID;
        this.itemQuantity[2] = _item3Quantity;
        this.itemID[3] = _item4ID;
        this.itemQuantity[3] = _item4Quantity;

    }
}
