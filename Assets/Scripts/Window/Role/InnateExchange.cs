using UnityEngine;
using System.Collections;

public class InnateExchange {
    public int TalentExchangeID { get; set; }
    public int TalentPoint { get; set; }
    public int NeedPlayLevel { get; set; }
    public int ConsumeHeroesFragment { get; set; }
    public int ConsumeResources1 { get; set; }
    public int ResourcesNum1 { get; set; }
    public int ConsumeResources2 { get; set; }
    public int ResourcesNum2 { get; set; }
    public InnateExchange(int TalentExchangeID, int TalentPoint, int NeedPlayLevel, int ConsumeHeroesFragment, int ConsumeResources1, int ResourcesNum1, int ConsumeResources2, int ResourcesNum2)
    {
        this.TalentExchangeID = TalentExchangeID;
        this.TalentPoint = TalentPoint;
        this.NeedPlayLevel = NeedPlayLevel;
        this.ConsumeHeroesFragment = ConsumeHeroesFragment;
        this.ConsumeResources1 = ConsumeResources1;
        this.ResourcesNum1 = ResourcesNum1;
        this.ConsumeResources2 = ConsumeResources2;
        this.ResourcesNum2 = ResourcesNum2;
    }
}


