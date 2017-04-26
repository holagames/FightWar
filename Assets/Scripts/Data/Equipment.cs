using System.Collections;

public class Equipment
{

    public int equipmentID { get; private set; }             //装备ID
    public int iconID { get; private set; }                 //ICON ID
    public string name { get; private set; }                 //名字
    public int equipmentClass { get; private set; }         //装备品阶
    public int equipmentLevel { get; private set; }          //装备等级
    public int equipmentLevelCap { get; private set; }       //装备等级上限
    public int equipmentExp { get; private set; }            //装备经验
    public int equipmentExpCap { get; private set; }         //装备经验上限
    public int hp { get; private set; }                      //生命
    public int strength { get; private set; }	            //力量
    public int intelligence { get; private set; } 	        //智力
    public int agile { get; private set; }                   //敏捷
    public int physicalDefense { get; private set; }         //物防
    public int armor { get; private set; }                 //护甲
    public int magicDefense { get; private set; }            //魔防
    public int magicResistance { get; private set; }       //魔抗
    public int physicalCrit { get; private set; }          //物暴
    public int magicCrit { get; private set; }             //魔暴
    public int attackSpeed { get; private set; }             //攻速
    public int move { get; private set; }                    //移动
    public int hpPlus { get; private set; }                  //生命附加
    public int strengthPlus { get; private set; }	        //力量附加
    public int intelligencePlus { get; private set; } 	    //智力附加
    public int agilePlus { get; private set; }              //敏捷附加
    public int physicalDefensePlus { get; private set; }     //物防附加
    public int armorPlus { get; private set; }              //护甲附加
    public int magicDefensePlus { get; private set; }        //魔防附加
    public int magicResistancePlus { get; private set; }   //魔抗附加
    public int physicalCritPlus { get; private set; }      //物暴附加
    public int magicCritPlus { get; private set; }         //魔暴附加
    public int attackSpeedPlus { get; private set; }         //攻速附加
    public int movePlus { get; private set; }               //移动附加


    public Equipment(int _equipmentID, int _equipmentClass, int _equipmentLevel, int _equipmentLevelCap, int _equipmentExp, int _equipmentExpCap, int _hp, int _strength, int _intelligence, int _agile,
        int _physicalDefense, int _armor, int _magicDefense, int _magicResistance, int _physicalCrit, int _magicCrit, int _attackSpeed, int _move, int _hpPlus, int _strengthPlus, int _intelligencePlus,
        int _agilePlus, int _physicalDefensePlus, int _armorPlus, int _magicDefensePlus, int _magicResistancePlus, int _physicalCritPlus, int _magicCritPlus, int _attackSpeedPlus, int _movePlus)
    {
        EquipDetailInfo ei = TextTranslator.instance.GetEquipInfoByID(_equipmentID);
        //if (ei != null)
        //{
        //    this.iconID = ei.equipPicID;
        //    this.name = ei.equipName;
        //}

        this.equipmentID = _equipmentID;
        this.equipmentClass = _equipmentClass;
        this.equipmentLevel = _equipmentLevel;
        this.equipmentLevelCap = _equipmentLevelCap;
        this.equipmentExp = _equipmentExp;
        this.equipmentExpCap = _equipmentExpCap;
        this.hp = _hp;
        this.strength = _strength;
        this.intelligence = _intelligence;
        this.agile = _agile;
        this.physicalDefense = _physicalDefense;
        this.armor = _armor;
        this.magicDefense = _magicDefense;
        this.magicResistance = _magicResistance;
        this.physicalCrit = _physicalCrit;
        this.magicCrit = _magicCrit;
        this.attackSpeed = _attackSpeed;
        this.move = _move;
        this.hpPlus = _hpPlus;
        this.strengthPlus = _strengthPlus;
        this.intelligencePlus = _intelligencePlus;
        this.agilePlus = _agilePlus;
        this.physicalDefensePlus = _physicalDefensePlus;
        this.armorPlus = _armorPlus;
        this.magicDefensePlus = _magicDefensePlus;
        this.magicResistancePlus = _magicResistancePlus;
        this.physicalCritPlus = _physicalCritPlus;
        this.magicCritPlus = _magicCritPlus;
        this.attackSpeedPlus = _attackSpeedPlus;
        this.movePlus = _movePlus;
    }

    /// <summary>
    /// 得到装备升级进度
    /// </summary>
    /// <returns></returns>
    public float GetEquipUpgradePrograssValue()
    {
        if (equipmentExpCap != 0)
        {
            return (float)equipmentExp / equipmentExpCap;
        }
        return 1;
    }

    public void AddExp(int exp)
    {
        equipmentExp += exp;
    }
}
