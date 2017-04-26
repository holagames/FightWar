using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill
{

    public int skillID { get; private set; }

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

    /// <summary>
    /// 等级上限
    /// </summary>
    public int levelCap { get; private set; }

    /// <summary>
    /// 消耗的AP
    /// </summary>
    public int costAP { get; private set; }

    /// <summary>
    /// CD时间
    /// </summary>
    public float coolDown { get; private set; }

    /// <summary>
    /// 技能品质
    /// </summary>
    public int color { get; private set; }

    /// <summary>
    /// 特殊发动条件
    /// </summary>
    public string activeCondition { get; private set; }

    /// <summary>
    /// 天赋技能。1是，0不是。是的话代表该技能无法被覆盖更换。
    /// </summary>
    public int skillFixed { get; private set; }

    /// <summary>
    /// 作用属性。0物理，1魔法，2特殊，3被动
    /// </summary>
    public int attribute { get; private set; }

    /// <summary>
    /// 主要效果。用于所对应的参数类别：0输出，1治疗，2妨害/增益，3被动，4特殊
    /// </summary>
    public int mainEffect { get; private set; }

    /// <summary>
    /// 作用目标
    /// </summary>
    public int targetRule { get; private set; }

    /// <summary>
    /// 作用范围
    /// </summary>
    public int area { get; private set; }

    /// <summary>
    /// 复数分配
    /// </summary>
    public int distribute { get; private set; }

    /// <summary>
    /// 天气元素
    /// </summary>
    public int weather { get; private set; }

    /// <summary>
    /// 连锁
    /// </summary>
    public List<int> chain { get; private set; }

    /// <summary>
    /// 命中率
    /// </summary>
    public float hitRatio { get; private set; }

    public int parameter1 { get; private set; }
    public int parameter2 { get; private set; }
    public int parameter3 { get; private set; }

    public int GP1 { get; private set; }
    public int GP2 { get; private set; }
    public int GP3 { get; private set; }
    public int GP4 { get; private set; }

    /// <summary>
    /// 吟唱时间
    /// </summary>
    public float singTime { get; private set; }

    /// <summary>
    /// 技能等级
    /// </summary>
    public int skillLevel { get; set; }

    public int skillVal1 { get; set; }
    public int skillVal2 { get; set; }
    public int skillVal3 { get; set; }

    public int skillDuration1 { get; set; }
    public int skillDuration2 { get; set; }
    public int skillDuration3 { get; set; }

    public int BuffID { get; set; }

    /// <summary>
    /// 开始CD计时
    /// </summary>
    public float startCDTime { get; private set; }

    public Skill(int id, string name)
    {
        skillID = id;
        skillName = name;
    }


    public Skill(int newSkillID, string newSkillName, float newSingTime, float newSkillCDTime, int newCostAP)
    {
        this.skillID = newSkillID;
        this.skillName = newSkillName;
        this.singTime = newSingTime;
        this.coolDown = newSkillCDTime;
        this.costAP = newCostAP;
        this.startCDTime = 0 - coolDown;
    }

    public Skill(int newSkillID, string newSkillName,int newType,int newLevel, string newDescription, int newPosition, int newLevelCap,
        int newCoolDown, int newCostAP, int newColor, string newActiveCondition, int newSkillFixed,
        int newAttribute, int newMainEffect, int newTargetRule, int newArea, int newDistribute, int newWeather,
        List<int> newChain, int newHitRatio, int newParameter1, int newVal1, int newDuration1, int newParameter2, int newVal2, int newDuration2, int newParameter3,
        int newVal3, int newDuration3, int newBuffID)
    {
        this.skillID = newSkillID;
        this.skillName = newSkillName;
        this.Type = newType;
        this.skillLevel = newLevel;
        this.description = newDescription;
        this.position = newPosition;
        this.levelCap = newLevelCap;
        this.costAP = newCostAP;
        this.coolDown = newCoolDown;
        this.color = newColor;
        this.activeCondition = newActiveCondition;
        this.skillFixed = newSkillFixed;
        this.attribute = newAttribute;
        this.mainEffect = newMainEffect;
        this.targetRule = newTargetRule;
        this.area = newArea;
        this.distribute = newDistribute;
        this.weather = newWeather;
        this.chain = newChain;
        this.hitRatio = newHitRatio;
        this.parameter1 = newParameter1;
        this.parameter2 = newParameter2;
        this.parameter3 = newParameter3;

        this.skillVal1 = newVal1;
        this.skillVal2 = newVal2;
        this.skillVal3 = newVal3;

        this.skillDuration1 = newDuration1;
        this.skillDuration2 = newDuration2;
        this.skillDuration3 = newDuration3;

        this.BuffID = newBuffID;

        singTime = position * 0.5f - 0.25f;
        this.startCDTime = 0 - coolDown;
    }

    /// <summary>
    /// 拷贝构造函数
    /// </summary>
    /// <param name="s"></param>
    public Skill(Skill s)
    {
        this.skillID = s.skillID;
        this.skillName = s.skillName;
        this.description = s.description;
        this.position = s.position;
        this.levelCap = s.levelCap;
        this.costAP = s.costAP;
        this.coolDown = s.coolDown;
        this.color = s.color;
        this.activeCondition = s.activeCondition;
        this.skillFixed = s.skillFixed;
        this.attribute = s.attribute;
        this.mainEffect = s.mainEffect;
        this.targetRule = s.targetRule;
        this.area = s.area;
        this.distribute = s.distribute;
        this.weather = s.weather;
        this.chain = s.chain;
        this.hitRatio = s.hitRatio;
        this.parameter1 = s.parameter1;
        this.parameter2 = s.parameter2;
        this.parameter3 = s.parameter3;

        singTime = position * 0.5f - 0.25f;
        this.startCDTime = 0 - coolDown;
    }


    public float GetCDTime()
    {
        float t = Time.time - startCDTime;

        return t >= coolDown ? coolDown : t;
    }

    public bool CDEnd()
    {
        return GetCDTime() == coolDown;
    }


    /// <summary>
    /// 得到技能时主动还是被动
    /// </summary>
    /// <returns></returns>
    public bool GetInitiative()
    {
        return attribute != 3;
    }

    public void SetStartCDTime()
    {
        startCDTime = Time.time;
    }
}
