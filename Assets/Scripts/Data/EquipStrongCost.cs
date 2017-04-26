using UnityEngine;
using System.Collections;

public class EquipStrongCost
{
    public int StrongLevel { get; private set; }
    public int RoleRarity { get; private set; }
    public int NeedExp { get; private set; }
    public BetterList<int> CostList = new BetterList<int>();

    public EquipStrongCost(int _strongLevel,int _RoleRarity,int NeedExp, BetterList<int> _cost)
    {
        this.StrongLevel = _strongLevel;
        this.RoleRarity = _RoleRarity;
        this.NeedExp = NeedExp;
        this.CostList = _cost;
    }
}
