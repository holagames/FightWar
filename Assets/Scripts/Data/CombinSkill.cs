using UnityEngine;
using System.Collections;

public class CombinSkill {
    public int CombinSkillID { get; private set; }
    public string SkillName { get; private set; }
    public string SkillDes { get; private set; }
    public int HeroId1 { get; private set; }
    public int HeroId2 { get; private set; }
    public int HeroId3 { get; private set; }
    public int HeroId4 { get; private set; }
    public int HeroId5 { get; private set; }
    public int HeroId6 { get; private set; }
    public int BuffId1 { get; private set; }

    public float BuffRand1 { get; private set; }
    public float Param1 { get; private set; }
    public int BuffId2 { get; private set; }
    public float BuffRand2 { get; private set; }
    public float Param2 { get; private set; }
    public int BuffId3 { get; private set; }
    public float BuffRand3 { get; private set; }
    public float Param3 { get; private set; }
    public int BuffId4 { get; private set; }
    public float BuffRand4 { get; private set; }
    public float Param4 { get; private set; }

    public CombinSkill(int _CombinSkillID, string _SkillName, string _SkillDes, int _HeroId1,int _HeroId2,int _HeroId3,int _HeroId4,int _HeroId5,int _HeroId6,
        int _BuffId1, float _BuffRand1, float _Param1, int _BuffId2, float _BuffRand2, float _Param2, int _BuffId3, float _BuffRand3, float _Param3, int _BuffId4, float _BuffRand4, float _Param4)
    {
        this.CombinSkillID = _CombinSkillID;
        this.SkillName = _SkillName;
        this.SkillDes = _SkillDes;
        this.HeroId1 = _HeroId1;
        this.HeroId2 = _HeroId2;
        this.HeroId3 = _HeroId3;
        this.HeroId4 = _HeroId4;
        this.HeroId5 = _HeroId5;
        this.HeroId6 = _HeroId6;
        this.BuffId1 = _BuffId1;
        this.BuffRand1 = _BuffRand1;
        this.BuffId2 = _BuffId2;
        this.Param1 = _Param1;
        this.BuffRand2 = _BuffRand2;
        this.Param2 = _Param2;
        this.BuffId3 = _BuffId3;
        this.BuffRand3 = _BuffRand3;
        this.Param3 = _Param3;
        this.BuffId4 = _BuffId4;
        this.BuffRand4 = _BuffRand4;
        this.Param4 = _Param4;
    }

}
