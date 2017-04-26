using UnityEngine;
using System.Collections;

public class TowerData  {
    public int FloorID { get; private set; }
    public int Type { get; private set; }
    public int GatePoint1 { get; private set; }
    public int GatePoint2 { get; private set; }
    public int GatePoint3 { get; private set; }
    public float GateStarRate1 { get; private set; }
    public float GateStarRate2 { get; private set; }
    public float GateStarRate3 { get; private set; }
    public int GateStarCost1 { get; private set; }
    public int GateStarCost2 { get; private set; }
    public int GateStarCost3 { get; private set; }
    public TowerData(int FloorID, int Type, int GatePoint1, int GatePoint2, int GatePoint3, float GateStarRate1, float GateStarRate2, float GateStarRate3, int GateStarCost1, int GateStarCost2, int GateStarCost3)
    {
        this.FloorID = FloorID;
        this.Type = Type;
        this.GatePoint1 = GatePoint1;
        this.GatePoint2 = GatePoint2;
        this.GatePoint3 = GatePoint3;
        this.GateStarRate1 = GateStarRate1;
        this.GateStarRate2 = GateStarRate2;
        this.GateStarRate3 = GateStarRate3;
        this.GateStarCost1 = GateStarCost1;
        this.GateStarCost2 = GateStarCost2;
        this.GateStarCost3 = GateStarCost3;
    }
}
