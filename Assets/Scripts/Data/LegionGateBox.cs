using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionGateBox
{
    public int LegionGateBoxID;
    public string GateName { get; set; }
    public int GateBoxID;
    public int ItemID;
    public int ItemNum;
    public int CreateBoxNum;
    public int PropNum;
    public LegionGateBox(int LegionGateBoxID, string GateName, int GateBoxID, int ItemID, int ItemNum,int CreateBoxNum, int PropNum)
    {
        this.LegionGateBoxID = LegionGateBoxID;
        this.GateName = GateName;
        this.GateBoxID = GateBoxID;
        this.ItemID = ItemID;
        this.ItemNum = ItemNum;
        this.CreateBoxNum = CreateBoxNum;
        this.PropNum = PropNum;
    }
    
}
