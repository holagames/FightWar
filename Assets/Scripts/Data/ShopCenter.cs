using UnityEngine;
using System.Collections;


/// <summary>
/// 商城
/// </summary>
public class ShopCenter {
    /// <summary>
    /// 商城ID
    /// </summary>
    public int ShopCenterID { get; private set; }

    /// <summary>
    /// 商城窗口号
    /// </summary>
    public int WindowID { get; private set; }

    /// <summary>
    /// 物品ID
    /// </summary>
    public int ItemID { get; private set; }

    /// <summary>
    /// 物品数量
    /// </summary>
    public int ItemNum { get; private set; }

    /// <summary>
    /// vip等级
    /// </summary>
    public int Vip { get; private set; }

    /// <summary>
    /// 购买次数
    /// </summary>
    public int BuyCount { get; private set; }

    /// <summary>
    /// 物品异价ID组
    /// </summary>
    public int PeculiarID { get; private set; }


    public ShopCenter(int ShopCenterID, int WindowID, int ItemID, int ItemNum, int Vip, int BuyCount, int PeculiarID) 
    {
        this.ShopCenterID = ShopCenterID;
        this.WindowID = WindowID;
        this.ItemID = ItemID;
        this.ItemNum = ItemNum;
        this.Vip = Vip;
        this.BuyCount = BuyCount;
        this.PeculiarID = PeculiarID;
    }
}
