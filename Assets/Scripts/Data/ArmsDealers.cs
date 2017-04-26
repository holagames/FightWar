using UnityEngine;
using System.Collections;

public class ArmsDealers {

    public int ArmsDealerID { get; private set; }
    public int WinPosID { get; private set; }
    public int ItemID { get; private set; }
    public int ItemNum { get; private set; }
    public int DiamondsTotalValue { get; private set; }
    public int VipBuy { get; private set; }
    public int BuyTime { get; private set; }
    public int MinimumDiscount { get; private set; }

    public ArmsDealers(int ArmsDealerID, int WinPosID, int ItemID, int ItemNum, int DiamondsTotalValue, int VipBuy, int BuyTime, int MinimumDiscount) 
    {
        this.ArmsDealerID = ArmsDealerID;
        this.WinPosID = WinPosID;
        this.ItemID = ItemID;
        this.ItemNum = ItemNum;
        this.DiamondsTotalValue = DiamondsTotalValue;
        this.VipBuy = VipBuy;
        this.BuyTime = BuyTime;
        this.MinimumDiscount = MinimumDiscount;
    }
}
