using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LabsLimit
{
    public int RoleType { get; set; }
    public BetterList<ReformLabItemData> LabItemDataList = new BetterList<ReformLabItemData>();
    public LabsLimit(int RoleType, BetterList<ReformLabItemData> LabItemDataList)
    {
        this.RoleType = RoleType;
        this.LabItemDataList = LabItemDataList;
    }
    
}
