using UnityEngine;
using System.Collections;

public class Innates
{
    public int TalentID { get; set; }
    public int TalentType { get; set; }
    public int Seat { get; set; }
    public int UnlockCondition { get; set; }
    public int EffectType { get; set; }
    public int ValueType1 { get; set; }
    public int Value1 { get; set; }
    public int ValueType2 { get; set; }
    public int Value2 { get; set; }
    public string Name { get; set; }
    public int BackendReading { get; set; }
    public string Des { get; set; }
    public int CostType { get; set; }
    public int CostValue { get; set; }
    public int TalentCost { get; set; }
    public Innates(int TalentID, int TalentType, int Seat, int UnlockCondition, int EffectType, int ValueType1, int Value1, int ValueType2, int Value2, string Name, int BackendReading, string Des, int CostType, int CostValue, int TalentCost)
    {
        this.TalentID = TalentID;
        this.TalentType = TalentType;
        this.Seat = Seat;
        this.UnlockCondition = UnlockCondition;
        this.EffectType = EffectType;
        this.ValueType1 = ValueType1;
        this.Value1 = Value1;
        this.ValueType2 = ValueType2;
        this.Value2 = Value2;
        this.Name = Name;
        this.BackendReading = BackendReading;
        this.Des = Des;
        this.CostType = CostType;
        this.CostValue = CostValue;
        this.TalentCost = TalentCost;
    }
}
