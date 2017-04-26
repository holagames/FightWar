using UnityEngine;
using System.Collections;
using System;

public class Exchange  {

    /// <summary>
    /// 充值ID
    /// </summary>
    public int exchangeID
    {
        get;
        private set;
    }

    /// <summary>
    /// 需要的人名币
    /// </summary>
    public int cash
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
    /// 名字
    /// </summary>
    public string exchangeName
    {
        get;
        private set;
    }

    /// <summary>
    /// 钻石
    /// </summary>
    public int diamond
    {
        get;
        private set;
    }

    /// <summary>
    /// 首冲额外钻石
    /// </summary>
    public int firstDiamond
    {
        get;
        private set;
    }

    /// <summary>
    /// 额外钻石
    /// </summary>
    public int extDiamond
    {
        get;
        private set;
    }


    /// <summary>
    /// 是否首冲
    /// </summary>
    public bool isfristDiamond 
    {
        get;
        private set;
    }

    public Exchange(Hashtable mHash)
    {
        exchangeID = Convert.ToInt32(mHash["ExchangeID"]);
        exchangeName = Convert.ToString(mHash["Name"]);
        cash = Convert.ToInt32(mHash["Cash"]);
        type = Convert.ToInt32(mHash["Type"]);
        diamond = Convert.ToInt32(mHash["Diamond"]);
        firstDiamond = Convert.ToInt32(mHash["FirstDiamond"]);
        extDiamond = Convert.ToInt32(mHash["ExtDiamond"]);
        isfristDiamond = false;
    }

    public void SetisfristDiamond(bool isfrist)
    {
        isfristDiamond = isfrist;
    }
}
