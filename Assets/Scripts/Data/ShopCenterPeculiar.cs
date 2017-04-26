using UnityEngine;
using System.Collections;


/// <summary>
/// 商城异价ID组
/// </summary>
public class ShopCenterPeculiar {

    /// <summary>
    /// 商城异价ID组ID
    /// </summary>
    public int ShopCenterPeculiarID { get; private set; }

    /// <summary>
    /// 商城异价ID组
    /// </summary>
    public int PeculiarID { get; private set; }

    /// <summary>
    /// 商城异价ID组购买次数
    /// </summary>
    public int BuyCount { get; private set; }

    /// <summary>
    /// 商城异价ID组钻石价格
    /// </summary>
    public int PriceDiamond { get; private set; }


    public ShopCenterPeculiar(int ShopCenterPeculiarID, int PeculiarID, int BuyCount, int PriceDiamond) 
    {
        this.ShopCenterPeculiarID = ShopCenterPeculiarID;
        this.PeculiarID = PeculiarID;
        this.BuyCount = BuyCount;
        this.PriceDiamond = PriceDiamond;
    }
}
