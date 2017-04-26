using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Buff
{
    public int buffID { get; private set; }

    public int action { get; private set; }
    public int buffType { get; private set; }


    public int stack { get; private set; }
    public int start { get; private set; }
    public int target { get; private set; }

    public string buffName { get; private set; }

    public int round { get; set; }

    public float parameter1 { get; set; }
    public float parameter2 { get; set; }
    public int buffIcon { get; private set; }
    public string effectName { get; private set; }
    public string startEffect { get; private set; }

    public GameObject effectObject = null;

    /// <summary>
    /// 技能描述
    /// </summary>
    public string des { get; private set; }


    ///////////////////////Buff属性(以上)//////////////////////////
    public int BuffFightSpeed;
    public int BuffFightAttack;
    public int BuffFightAttackPercent;
    public int BuffFightDefend;
    public int BuffFightDefendPercent;
    public int BuffHp;
    public int BuffHpPercent;

    public int BuffHit;
    public int BuffNotHit;
    public int BuffCrit;
    public int BuffNoCrit;
    public int BuffDamigeAdd;
    public int BuffDamigeReduce;
    ///////////////////////Buff属性(以上)//////////////////////////

    public Buff(int _buffID, int _action, int _type, int _stack, int _start, int _target, string _buffName, int _round, float _parameter1, float _parameter2, int _buffIcon, string _effectName, string _startEffect, string _des)
    {
        this.buffID = _buffID;
        this.action = _action;
        this.buffType = _type;
        this.stack = _stack;
        this.start = _start;
        this.target = _target;
        this.buffName = _buffName;
        this.round = _round;
        this.parameter1 = _parameter1;
        this.parameter2 = _parameter2;
        this.buffIcon = _buffIcon;
        this.effectName = _effectName;
        this.startEffect = _startEffect;
        this.des = _des;
    }

    public Buff(Buff newBuff)
    {
        this.buffID = newBuff.buffID;
        this.action = newBuff.action;
        this.buffType = newBuff.buffType;
        this.stack = newBuff.stack;
        this.start = newBuff.start;
        this.target = newBuff.target;
        this.buffName = newBuff.buffName;
        this.round = newBuff.round;
        this.parameter1 = newBuff.parameter1;
        this.parameter2 = newBuff.parameter2;
        this.buffIcon = newBuff.buffIcon;
        this.effectName = newBuff.effectName;
        this.startEffect = newBuff.startEffect;
        this.des = newBuff.des;
    }

    public void AddBuff(Buff newBuff)
    {
        this.round += newBuff.round;
        this.parameter1 += newBuff.parameter1;
        this.parameter2 += newBuff.parameter2;
    }

    public void ResetBuff(Buff newBuff)
    {
        this.round = newBuff.round;
        this.parameter1 = newBuff.parameter1;
        this.parameter2 = newBuff.parameter2;
    }
}
