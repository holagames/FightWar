using UnityEngine;
using System.Collections;


/// <summary>
/// PVP积分奖励类
/// </summary>
public class PvpPointShop{

    public int PvpPointShopID { get; private set; }
    public int Point { get; private set; }
    public int RYCoin { get; private set; }
    public int Gold { get; private set; }
    public int itemID { get; private set; }
    public int itemNum { get; private set; }

    public PvpPointShop(int PvpPointShopID, int Point, int RYCoin, int Gold, int itemID, int itemNum) 
    {
        this.PvpPointShopID = PvpPointShopID;
        this.Point = Point;
        this.RYCoin = RYCoin;
        this.Gold = Gold;
        this.itemID = itemID;
        this.itemNum = itemNum;
    }
}
