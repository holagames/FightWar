using UnityEngine;
using System.Collections;

public class Resource {
    public int ResourceId { get; private set; }
    public int MapId { get;private set; }
    public int ChapterId { get; private set; }
    public int EnergyCost { get; private set; }
    public int ResTime { get; private set; }
    public string RoleDebrisNum { get; private set; }
    public string DiaBonus { get; private set; }
    public string DiaRand { get; private set; }
    public string GoldBonus { get; private set; }
    public string GoldRand { get; private set; }
    public int DebrisId1 { get; private set; }
    public string DebrisNum1 { get; private set; }
    public int DebrisIdRand1 { get; private set; }
    public int DebrisId2 { get; private set; }
    public string DebrisNum2 { get; private set; }
    public int DebrisIdRand2 { get; private set; }
    public int DebrisId3 { get; private set; }
    public string DebrisNum3 { get; private set; }
    public int DebrisIdRand3 { get; private set; }
    public int DebrisId4 { get; private set; }
    public string DebrisNum4 { get; private set; }
    public int DebrisIdRand4 { get; private set; }
    public int ItemId1 { get; private set; }
    public string ItemNum1 { get; private set; }
    public int ItemRand1 { get; private set; }
    public int ItemId2 { get; private set; }
    public string ItemNum2 { get; private set; }
    public int ItemRand2 { get; private set; }
    public int ItemId3 { get; private set; }
    public string ItemNum3 { get; private set; }
    public int ItemRand3 { get; private set; }
    public int ItemId4 { get; private set; }
    public string ItemNum4 { get; private set; }
    public int ItemRand4 { get; private set; }
    public int ItemId5 { get; private set; }
    public string ItemNum5 { get; private set; }
    public int ItemRand5 { get; private set; }
    public Resource(int ResourceId,int MapId,int ChapterId,int EnergyCost,int ResTime,string RoleDebrisNum,string DiaBonus,string DiaRand,string GoldBonus,string GoldRand,int DebrisId1,
        string DebrisNum1,int DebrisIdRand1,int DebrisId2,string DebrisNum2,int DebrisIdRand2,int DebrisId3,string DebrisNum3,int DebrisIdRand3,int DebrisId4,string DebrisNum4,int DebrisIdRand4,
        int ItemId1,string ItemNum1,int ItemRand1,int ItemId2,string ItemNum2,int ItemRand2,int ItemId3,string ItemNum3,int ItemRand3,int ItemId4,string ItemNum4,int ItemRand4,int ItemId5,string ItemNum5,int ItemRand5) {
            this.ResourceId = ResourceId;
            this.MapId = MapId;
            this.ChapterId = ChapterId;
            this.EnergyCost = EnergyCost;
            this.ResTime = ResTime;
            this.RoleDebrisNum = RoleDebrisNum;
            this.DiaBonus = DiaBonus;
            this.DiaRand = DiaRand;
            this.GoldBonus = GoldBonus;
            this.GoldRand = GoldRand;
            this.DebrisId1 = DebrisId1;
            this.DebrisNum1 = DebrisNum1;
            this.DebrisIdRand1 = DebrisIdRand1;
            this.DebrisId2 = DebrisId2;
            this.DebrisNum2 = DebrisNum2;
            this.DebrisIdRand2 = DebrisIdRand2;
            this.DebrisId3 = DebrisId3;
            this.DebrisNum3 = DebrisNum3;
            this.DebrisIdRand3 = DebrisIdRand3;
            this.DebrisId4 = DebrisId4;
            this.DebrisNum4 = DebrisNum4;
            this.DebrisIdRand4 = DebrisIdRand4;
            this.ItemId1 = ItemId1;
            this.ItemNum1 = ItemNum1;
            this.ItemRand1 = ItemRand1;
            this.ItemId2 = ItemId2;
            this.ItemNum2 = ItemNum2;
            this.ItemRand2 = ItemRand2;
            this.ItemId3 = ItemId3;
            this.ItemNum3 = ItemNum3;
            this.ItemRand3 = ItemRand3;
            this.ItemId4 = ItemId4;
            this.ItemNum4 = ItemNum4;
            this.ItemRand4 = ItemRand4;
            this.ItemId5 = ItemId5;
            this.ItemNum5 = ItemNum5;
            this.ItemRand5 = ItemRand5;
    }
}
