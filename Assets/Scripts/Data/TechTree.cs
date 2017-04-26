using UnityEngine;
using System.Collections;

/// <summary>
/// 情报类
/// </summary>
public class TechTree {
    public int TechTreeID { get; private set; }//情报id
    public int Depth{ get; private set; }//深度
    public int EffectType { get; private  set; }//效果类型 1-25 （百分比）
    public float EffectVal1 { get; private  set; }// 1 6 9 攻击  16 攻击百分比 23无视防御触发概率   24 反伤触发概率 25 双倍伤害触发概率
    public float EffectVal2 { get; private set; }// 1 6 9血量   16 血量百分比 23无视防御百分比  24 反伤百分比 25 双倍伤害（2）
    public int Level{ get; private set; }//等级
    public int UnLockPreID{ get; private set; }//解锁先有ID
    public int UnLockNeedLevel{ get; private set; }//解锁需要等级
    public int UnLockPreDepthPoint { get; private set; }//解锁需要情报点数
    public int LevelUpNeedPoint{ get; private set; }//升级需要点数
    public string Name { get; private set; }//情报名字

    public int Icon { get; private set; }//物品的编号
    public string Des { get; private set; }//物品的描述
    public int GoldCost { get; private set; }//升级消耗金币

    public string NoOpenDescription { get; private set; }//未开放提示
    public TechTree(int TechTreeID, int Depth, int EffectType, float EffectVal1, float EffectVal2, int Level, int UnLockPreID,
                      int UnLockNeedLevel, int UnLockPreDepthPoint, int LevelUpNeedPoint, string Name, int Icon, string Des, int GoldCost,string NoOpenDescription)
    {
        this.TechTreeID = TechTreeID;
        this.Depth = Depth;
        this.EffectType = EffectType;
        this.EffectVal1 = EffectVal1;
        this.EffectVal2 = EffectVal2;
        this.Level = Level;
        this.UnLockPreID = UnLockPreID;
        this.UnLockNeedLevel = UnLockNeedLevel;
        this.UnLockPreDepthPoint = UnLockPreDepthPoint;
        this.LevelUpNeedPoint = LevelUpNeedPoint;
        this.Name = Name;
        this.Icon = Icon;
        this.Des = Des;
        this.GoldCost = GoldCost;
        this.NoOpenDescription = NoOpenDescription;
    }

}
