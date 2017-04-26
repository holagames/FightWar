using UnityEngine;
using System.Collections;

public class NuclearPowerPlan
{
    public int EncouragingTimestId { get; private set; }
    public int DiamondsValue { get; private set; }
    public int AddForce { get; private set; }
    public int AddOneHeroAtk { get; private set; }
    public int AddOneHeroHp { get; private set; }
    public NuclearPowerPlan(int EncouragingTimestId, int DiamondsValue, int AddForce, int AddOneHeroAtk, int AddOneHeroHp)
    {
        this.EncouragingTimestId = EncouragingTimestId;
        this.DiamondsValue = DiamondsValue;
        this.AddForce = AddForce;
        this.AddOneHeroAtk = AddOneHeroAtk;
        this.AddOneHeroHp = AddOneHeroHp;
    }
}
