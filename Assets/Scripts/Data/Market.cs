using UnityEngine;
using System.Collections;

/// <summary>
/// 购买类
/// </summary>
public class Market
{
    public int MarketID { get; private set; }

    public int BuyCount { get; private set; }
    public int StaminaNum { get; private set; }
    public int StaminaPrice { get; private set; }
    public int EnergyNum { get; private set; }
    public int EnergyPrice { get; private set; }
    public int GoldPrice { get; private set; }
    public int ResetMstGateDiamondCost { get; private set; }
    public int PvpBuyCountDiamondCost { get; private set; }
    public int PvpRefreshDiamondCost { get; private set; }
    public int ShopRefreshDiamondCost { get; private set; }
    public int WorldEventRefreshCost { get; private set; }

    public int SmuggleRefreshDaimondCost { get; private set; }
    public int WorldBossGoldCost { get; private set; }
    public int WorldBossDiamondCost { get; private set; }
    public int WorldBossClearCDDiamondCost { get; private set; }
    public int LegionGateBuyPrice { get; private set; }
    public int KingRoadFightDiamond { get; private set; }
    public int LegionTaskRefreshDiamond { get; private set; }
    public int CrapsChangePrice { get; private set; }
    //public string GoldNum { get; private set; }
    //public float GoldBooRateX10 { get; private set; }
    //public float GoldBooRateX6 { get; private set; }
    //public float GoldBooRateX4 { get; private set; }
    //public float GoldBooRateX2 { get; private set; }
    //public int GoldPrice { get; private set; }
            //MarketID="1" BuyCount="1" StaminaNum="120" StaminaPrice="50" EnergyNum="60" EnergyPrice="50" GoldPrice="10" ResetMstGateDiamondCost="30" PvpBuyCountDiamondCost="20" PvpRefreshDiamondCost="0" ShopRefreshDiamondCost="10" WorldEventRefreshCost="10" 
            //SmuggleRefreshDaimondCost="0" WorldBossDiamondCost="50" WorldBossGoldCost="10000" WorldBossClearCDDiamondCost="20" LegionGateBuyPrice="30" KingRoadFightDiamond="10" LegionTaskRefreshDiamond="0" CrapsChangePrice="10" />
    public Market(int MarketID, int BuyCount, int StaminaNum, int StaminaPrice, int EnergyNum, int EnergyPrice, int GoldPrice, int ResetMstGateDiamondCost, int PvpBuyCountDiamondCost, int PvpRefreshDiamondCost, int ShopRefreshDiamondCost, int WorldEventRefreshCost,
        int _SmuggleRefreshDaimondCost, int WorldBossDiamondCost, int WorldBossGoldCost,int WorldBossClearCDDiamondCost, int LegionGateBuyPrice, int KingRoadFightDiamond, int LegionTaskRefreshDiamond, int CrapsChangePrice)
    {
        this.MarketID = MarketID;
        this.BuyCount = BuyCount;
        this.StaminaNum = StaminaNum;
        this.StaminaPrice = StaminaPrice;
        this.EnergyNum = EnergyNum;
        this.EnergyPrice = EnergyPrice;
        this.GoldPrice = GoldPrice;
        this.ResetMstGateDiamondCost = ResetMstGateDiamondCost;
        this.PvpBuyCountDiamondCost = PvpBuyCountDiamondCost;
        this.PvpRefreshDiamondCost = PvpRefreshDiamondCost;
        this.ShopRefreshDiamondCost = ShopRefreshDiamondCost;
        this.WorldEventRefreshCost = WorldEventRefreshCost;
        this.SmuggleRefreshDaimondCost = _SmuggleRefreshDaimondCost;
        this.WorldBossGoldCost = WorldBossGoldCost;
        this.WorldBossDiamondCost = WorldBossDiamondCost;
        this.WorldBossClearCDDiamondCost = WorldBossClearCDDiamondCost;
        this.LegionGateBuyPrice = LegionGateBuyPrice;
        this.KingRoadFightDiamond = KingRoadFightDiamond;
        this.LegionTaskRefreshDiamond = LegionTaskRefreshDiamond;
        this.CrapsChangePrice = CrapsChangePrice;
        //this.GoldNum = GoldNum;
        //this.GoldBooRateX10 = GoldBooRateX10;
        //this.GoldBooRateX6 = GoldBooRateX6;
        //this.GoldBooRateX4 = GoldBooRateX4;
        //this.GoldBooRateX2 = GoldBooRateX2;
        //this.GoldPrice = GoldPrice;
    }
}
