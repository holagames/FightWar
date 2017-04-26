using UnityEngine;
using System.Collections;

public class Vip
{
    public int VipID { get; private set; }
    public int TradingDiamondCount { get; private set; }
    public int GateRushState10 { get; private set; }
    public int DayCoinTreeCount { get; private set; }
    public float CoinTreeBooRate { get; private set; }
    public int ResetMasterGateCount { get; private set; }

    public int OnceIndianaState { get; private set; }
    public int TowerFightSkipState { get; private set; }
    public int Indiana5State { get; private set; }
    public int ClubMoneyState { get; private set; }
    public int RoleWashState { get; private set; }
    public float PvpCdTime { get; private set; }
    public string NewTaskID { get; private set; }
    public float TowerPointAddRate { get; private set; }
    public int PvpFightSkipState { get; private set; }
    public int GateActivityGoldCdTime { get; private set; }

    public int GateActivityRwdCdTime { get; private set; }

    public string VipGift { get; private set; }
    public int BuyDiamondPrice { get; private set; }
    public int BuyDiamondSale { get; private set; }
    public string Des { get; private set; }

    public float GoldBooRateX1 { get; private set; }
    public float GoldBooRateX2 { get; private set; }
    public float GoldBooRateX3 { get; private set; }
    public float GoldBooRateX4 { get; private set; }
    public float GoldBooRateX5 { get; private set; }
    public float GoldBooRateX10 { get; private set; }
    //GoldBooRateX1="0.1" GoldBooRateX2="0.15" GoldBooRateX3="0.15" GoldBooRateX4="0.15" GoldBooRateX5="0.2" GoldBooRateX10="0.2
    public Vip(int VipID, int TradingDiamondCount, int GateRushState10 ,int DayCoinTreeCount, int CoinTreeBooRate,
        int ResetMasterGateCount, int Indiana5State, int RoleWashState, int PvpCdTime,
        string NewTaskID, float TowerPointAddRate, int PvpFightSkipState, int GateActivityGoldCdTime, int GateActivityRwdCdTime, string VipGift, int BuyDiamondPrice, int BuyDiamondSale, string Des
        , float _GoldBooRateX1, float _GoldBooRateX2, float _GoldBooRateX3, float _GoldBooRateX4, float _GoldBooRateX5, float _GoldBooRateX10)
    {
        this.VipID = VipID;
        this.TradingDiamondCount = TradingDiamondCount;
        this.GateRushState10 = GateRushState10;
        this.DayCoinTreeCount = DayCoinTreeCount;
        this.CoinTreeBooRate = CoinTreeBooRate;
        this.ResetMasterGateCount = ResetMasterGateCount;
        //this.OnceIndianaState = OnceIndianaState;
        //this.TowerFightSkipState = TowerFightSkipState;
        this.Indiana5State = Indiana5State;
        //this.ClubMoneyState = ClubMoneyState;
        this.RoleWashState = RoleWashState;
        this.PvpCdTime = PvpCdTime;
        this.NewTaskID = NewTaskID;
        this.TowerPointAddRate = TowerPointAddRate;
        this.PvpFightSkipState = PvpFightSkipState;
        this.GateActivityGoldCdTime = GateActivityGoldCdTime;
        this.GateActivityRwdCdTime = GateActivityRwdCdTime;
        this.VipGift = VipGift;
        this.BuyDiamondPrice = BuyDiamondPrice;
        this.BuyDiamondSale = BuyDiamondSale;
        this.Des = Des;
        this.GoldBooRateX1 = _GoldBooRateX1;
        this.GoldBooRateX2 = _GoldBooRateX2;
        this.GoldBooRateX3 = _GoldBooRateX3;
        this.GoldBooRateX4 = _GoldBooRateX4;
        this.GoldBooRateX5 = _GoldBooRateX5;
        this.GoldBooRateX10 = _GoldBooRateX10;
    }
}
