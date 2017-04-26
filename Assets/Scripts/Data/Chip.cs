using UnityEngine;
using System.Collections;

public class Chip{
    public int ChipID { set; get; }
    public int Color { set; get; }
    public int SlotNum { set; get; }
    public int EffectType1 { set; get; }
    public float EffectVal1 { set; get; }

    public int EffectType2 { set; get; }
    public float EffectVal2 { set; get; }
    public int EffectType3 { set; get; }
    public float EffectVal3 { set; get; }
    public string Des { set; get; }

    public Chip(int _ChipID, int _Color, int _SlotNum, int _EffectType1, float _EffectVal1, int _EffectType2, float _EffectVal2, int _EffectType3, float _EffectVal3, string _Des)
    {
        this.ChipID = _ChipID;
        this.Color = _Color;
        this.SlotNum = _SlotNum;
        this.EffectType1 = _EffectType1;
        this.EffectVal1 = _EffectVal1;
        this.EffectType2 = _EffectType2;
        this.EffectVal2 = _EffectVal2;
        this.EffectType3 = _EffectType3;
        this.EffectVal3 = _EffectVal3;
        this.Des = _Des;
    }
}
