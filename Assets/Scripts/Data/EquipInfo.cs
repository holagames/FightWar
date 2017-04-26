using UnityEngine;
using System.Collections;

public class EquipDetailInfo
{

    /// <summary>
    /// 物攻
    /// </summary>
    public float ADAttack { get; private set; }

    /// <summary>
    /// 法攻
    /// </summary>
    public int APAttack { get; private set; }

    /// <summary>
    /// 物防
    /// </summary>
    public int ADDenfance { get; private set; }

    /// 法防
    public int APDenfance { get; private set; }

    /// <summary>
    /// HP
    /// </summary>
    public float HP;

    /// <summary>
    /// ID
    /// </summary>
    public int equipID { get; private set; }

    /// <summary>
    /// 最大品质
    /// </summary>
    public int LevelMax { get; private set; }
    /// <summary>
    /// 命中
    /// </summary>
    public float Hit { get; private set; }
    /// <summary>
    /// 闪避
    /// </summary>
    public float NoHit { get; private set; }
    /// <summary>
    /// 暴击
    /// </summary>
    public float Crit { get; private set; }
    /// <summary>
    /// 暴抗
    /// </summary>
    public float NoCrit { get; private set; }

    /// <summary>
    /// 伤害加成
    /// </summary>
    public float DmgBonus { get; private set; }
    /// <summary>
    /// 伤害减免
    /// </summary>
    public float DmgReduction { get; private set; }
    public EquipDetailInfo(int LevelMax, float newHP, float newADAttack, int newADDenfance, float newHit, float newNoHit, float newCrit, float newNoCrit, float newDmgBouns, float newDmgReduction, int newequipID)
    {
        this.LevelMax = LevelMax;
        this.ADAttack = newADAttack;
        this.APAttack = 0;
        this.ADDenfance = newADDenfance;
        this.APDenfance = 0;
        this.HP = newHP;
        this.equipID = newequipID;
        this.Hit = newHit;
        this.NoHit = newNoHit;
        this.Crit = newCrit;
        this.NoCrit = newNoCrit;
        this.DmgBonus = newDmgBouns;
        this.DmgReduction = newDmgReduction;
        //this.holeReqClass[0] = hole1Req;
        //this.holeReqClass[1] = hole2Req;
        //this.holeReqClass[2] = hole3Req;
        //this.holeReqClass[3] = hole4Req;
        //this.holeReqClass[4] = hole5Req;
    }
}
