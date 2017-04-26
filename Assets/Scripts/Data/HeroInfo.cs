using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroInfo
{

    public int heroID { get; private set; }
    public int heroCarrerType { get; private set; }
    public string heroName { get; private set; }
    public string heroCarrer { get; private set; }
    public string heroInfo { get; private set; }
    public string heroDescription { get; private set; }
    public string careerShow { get; private set; }
    public float heroScale { get; private set; }
    public int heroBio { get; private set; }
    public int heroAtkType { get; private set; }
    public int heroDefType { get; private set; }
    public int heroRace { get; private set; }
    public int heroRarity { get; private set; }
    public int heroState { get; private set; }
    public int heroFly { get; private set; }
    public int heroAtkFly { get; private set; }
    public int heroAtkGround { get; private set; }
    public int heroToSort { get; private set; }
    public int heroArea { get; private set; }
    public int heroAi { get; private set; }
    public int heroClass { get; private set; }
    public int heroHP { get; private set; }
    public int heroStrong { get; private set; }
    public int heroIntell { get; private set; }
    public int heroAgile { get; private set; }
    public int heroPhyDef { get; private set; }
    public int heroPhyRed { get; private set; }
    public float heroDamigeAdd { get; private set; }
    public float heroDamigeReduce { get; private set; }
    public float heroHit { get; private set; }
    public float heroNoHit { get; private set; }
    public float heroPcritical { get; private set; }
    public float heroNoCri { get; private set; }
    public int heroMcritical { get; private set; }
    public int heroSpeed { get; private set; }
    public int heroMove { get; private set; }
    public string heroPetPhrase { get; private set; }
    public string InForces { get; private set; }
    public int heroPiece { get; private set; }
    public int powerSort { get; private set; }
    public int sex { get; private set; }
    public int weaponID { get; private set; }
    public List<int> heroSkillList = new List<int>();

    public int AtkScore{ get; private set; }
    public int DefScore{ get; private set; }
    public int  HpScore{ get; private set; }
    public int SkillScore { get; private set; }

    public HeroInfo(int newHeroID,int heroCarrerType,string careerShow, string newHeroName,string newHeroCarrer, string newHeroDescription, string newHeroInfo, int newHeroScale, int newHeroBio, int newHeroAtkType,
        int newHeroDefType,int newHeroRace, int newHeroRarity, int newHeroState, int newHeroAtkFly, int newHeroToSort, int newHeroArea, int newHeroAi, int newHeroClass,
        int newHeroHP, int newHeroStrong, int newHeroIntell, int newHeroAgile, int newHeroPhyDef, int newHeroPhyRed, float newHeroDamigeAdd,
        float newHeroDamigeReduce, float newHeroHit, float newHeroNoHit, float newHeroPcritical, float newHeroNoCri, int newHeroMcritical, int newHeroSpeed, int newHeroMove,
        int Skill1, int Skill2, int Skill3, int Skill4, int newHeroPiece, string heroPetPhrase, string InForces, int powerSort, int Sex, int WeaponID
        , int _AtkScore, int _DefScore, int _HpScore, int _SkillScore)
    {
        this.heroID = newHeroID;
        this.heroCarrerType = heroCarrerType;
        this.careerShow = careerShow;
        this.heroName = newHeroName;
        this.heroDescription = newHeroDescription;
        this.heroCarrer = newHeroCarrer;
        this.heroInfo = newHeroInfo;
        this.heroScale = newHeroScale / 100f;
        this.heroBio = newHeroBio;
        this.heroAtkType = newHeroAtkType;
        this.heroDefType = newHeroDefType;
        this.heroRace = newHeroRace;
        this.heroRarity = newHeroRarity;
        this.heroState = newHeroState;
        this.heroAtkFly = newHeroAtkFly;
        this.heroToSort = newHeroToSort;
        this.heroArea = newHeroArea;
        this.heroAi = newHeroAi;
        this.heroClass = newHeroClass;
        this.heroHP = newHeroHP;
        this.heroStrong = newHeroStrong;
        this.heroIntell = newHeroIntell;
        this.heroAgile = newHeroAgile;
        this.heroPhyDef = newHeroPhyDef;
        this.heroPhyRed = newHeroPhyRed;
        
        this.heroMcritical = newHeroMcritical;
        this.heroSpeed = newHeroSpeed;
        this.heroMove = newHeroMove;
        this.heroPiece = newHeroPiece;

        this.heroHit = newHeroHit;
        this.heroNoHit = newHeroNoHit;
        this.heroPcritical = newHeroPcritical;
        this.heroNoCri = newHeroNoCri;
        this.heroDamigeAdd = newHeroDamigeAdd;
        this.heroDamigeReduce = newHeroDamigeReduce;
        this.heroPetPhrase = heroPetPhrase;
        this.InForces = InForces;
        this.powerSort = powerSort;
        this.sex = Sex;
        this.weaponID = WeaponID;
        this.AtkScore= _AtkScore;
        this.DefScore = _DefScore;
        this.HpScore = _HpScore;
        this.SkillScore = _SkillScore;
        if (Skill1 > 0)
        {
            heroSkillList.Add(Skill1);
        }
        if (Skill2 > 0)
        {
            heroSkillList.Add(Skill2);
        }
        if (Skill3 > 0)
        {

            heroSkillList.Add(Skill3);
        }
        if (Skill4 > 0)
        {

            heroSkillList.Add(Skill4);
        }
    }

}
