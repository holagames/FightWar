using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LabsPoint
{
    public int LabsPointType { get; set; }
    public int RankPoint { get; set; }
    public LabsPoint(int LabsPointType, int RankPoint)
    {
        this.LabsPointType = LabsPointType;
        this.RankPoint = RankPoint;
    }
}
