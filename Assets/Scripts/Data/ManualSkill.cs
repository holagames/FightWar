using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ManualSkill : IComparable
{

    public int skillID { get; private set; }

    public int skillType { get; private set; }

    public int Camp { get; private set; }//1 - 自己的，2 - 敌人的

    public string skillName { get; private set; }

    /// <summary>
    /// 技能描述
    /// </summary>
    public string description { get; private set; }

    /// <summary>
    /// 技能位置
    /// </summary>
    public int position { get; private set; }

    public int Type { get; private set; }
    public int round { get; private set; }

    public int coolDown { get; private set; }

    /// <summary>
    /// 技能等级
    /// </summary>
    public int skillLevel { get; set; }

    public float skillVal1 { get; set; }
    public float skillVal2 { get; set; }
    public float skillVal3 { get; set; }
    public float skillVal4 { get; set; }
    public float skillVal5 { get; set; }
    public int buffID { get; set; }

    public ManualSkill(int newSkillID, int newSkillType, string newSkillName, string newDescription, int newCoolDown, int newLevel, int newRound, int newType,
        float newVal1, float newVal2, float newVal3, float newVal4, float newVal5, int newBuffID, int Camp)
    {
        this.skillID = newSkillID;
        this.skillType = newSkillType;
        this.skillName = newSkillName;
        this.Type = newType;
        this.skillLevel = newLevel;
        this.description = newDescription;
        this.round = newRound;
        this.coolDown = newCoolDown;
        this.skillVal1 = newVal1;
        this.skillVal2 = newVal2;
        this.skillVal3 = newVal3;
        this.skillVal4 = newVal4;
        this.skillVal5 = newVal5;
        this.buffID = newBuffID;
        this.Camp = Camp;
    }
    public int CompareTo(object x1)
    {
        ManualSkill x = x1 as ManualSkill;
        if (this.skillLevel < x.skillLevel)
        {
            return -1;
        }
        else if (this.skillLevel == x.skillLevel)
        {
            return 0;
        }
        else return 1;
    }
}
