using UnityEngine;
using System.Collections;
using System;
public class StrengthenMaster
{

    public int id
    {
        get;
        private set;
    }

    /// <summary>
    /// 类型
    /// </summary>
    public int type
    {
        get;
        private set;
    }

    /// <summary>
    /// 等级需求
    /// </summary>
    public int needLevel
    {
        get;
        private set;
    }

    /// <summary>
    /// 血
    /// </summary>
    public int hp
    {
        get;
        private set;
    }

    /// <summary>
    /// 攻击
    /// </summary>
    public int attack
    {
        get;
        private set;
    }

    /// <summary>
    /// 防御
    /// </summary>
    public int defense
    {
        get;
        private set;
    }

    /// <summary>
    ///构造函数
    /// </summary>
    /// <param name="mHash"></param>
    public StrengthenMaster(Hashtable mHash)
    {
        id          = Convert.ToInt32(mHash["ID"]);
        type        = Convert.ToInt32(mHash["Type"]);
        needLevel   = Convert.ToInt32(mHash["NeedLevel"]);
        hp          = Convert.ToInt32(mHash["Hp"]);
        attack      = Convert.ToInt32(mHash["Att"]);
        defense     = Convert.ToInt32(mHash["Def"]);
        
    }

}
