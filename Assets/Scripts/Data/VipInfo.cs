using UnityEngine;
using System.Collections;

public class VipInfo {

    public int vipLevel { get; private set; }

    public int VipNeedLunaGem { get; private set; }

    public string VipDes { get; private set; } 

    public VipInfo(int level, int needLunaGem, string des)
    {
        vipLevel = level;
        VipNeedLunaGem = needLunaGem;
        VipDes = des.Replace("@", "\n");
    }
}
