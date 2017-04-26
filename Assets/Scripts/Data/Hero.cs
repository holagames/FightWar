using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;

public class Hero
{

    public const int MAX_SKILL_COUNT = 6;

    public int position { get; set; }            // 位置。我方布阵共有12个位置，0则代表未上阵
    public int characterRoleID { get; private set; }              // 角色id
    public int cardID { get; private set; }              // 卡牌id
    public string name { get; private set; }             // 姓名
    public int sex { get; private set; }                 // 男，女，无
    public int type { get; private set; }                // 力，智，敏3个类别，分别对应近战，魔法，远程3类
    public int bio { get; private set; }              // 种族。人，哥布林，精灵，兽人，龙等
    public int species { get; private set; }             // 档次。士兵，英雄2个档次
    public int cost { get; private set; }                // 消费。所有上阵角色的COST之和不可超过战队COST
    public int sort { get; private set; }                // 排序
    public int area { get; private set; }                // 攻击范围
    public int ai { get; private set; }                  // 攻击顺序
    public int force { get; private set; }               // 战斗力
    public int level { get; set; }               // 等级
    public int maxLevel { get; private set; }            // 等级上限
    public int exp { get; set; }                 // 当前经验
    public int maxExp { get; set; }              // 当前最大经验
    public int skillLevel { get; set; }             //天命等级
    public int skillNumber { get; set; }            //天命值
    public int skillPoint { get; set; }            //怒气

    public int score = 0;// 实验室评分
    public float addPercent = 0;//加成

    /// <summary>
    /// 军衔。分1-6星，用C,UC,R,SR,SSR,LE等来对应表示
    /// </summary>
    public int rank { get; private set; }

    /// <summary>
    /// 颜色。决定角色当前的等级上限，达到上限时可通过进阶道具和游戏货币来进阶突破上限
    /// </summary>
    public int classNumber { get; private set; }

    /// <summary>
    /// 稀有度。决定角色当前的等级上限，达到上限时可通过进阶道具和游戏货币来进阶突破上限
    /// </summary>
    public int rare { get; private set; }

    /// <summary>
    /// 生命
    /// </summary>
    public ObscuredInt HP { get; private set; }

    /// <summary>
    /// 力量
    /// </summary>
    public ObscuredInt strength { get; private set; }

    /// <summary>
    /// 智力
    /// </summary>
    public ObscuredInt intelligence { get; private set; }

    /// <summary>
    /// 敏捷
    /// </summary>
    public ObscuredInt agile { get; private set; }

    /// <summary>
    /// 物防
    /// </summary>
    public ObscuredInt physicalDefense { get; private set; }

    /// <summary>
    /// 护甲 float
    /// </summary>
    public ObscuredInt armor { get; private set; }

    /// <summary>
    /// 魔防
    /// </summary>
    public ObscuredInt magicDefense { get; private set; }

    /// <summary>
    /// 魔抗 float
    /// </summary>
    public ObscuredInt magicResistance { get; private set; }

    /// <summary>
    /// 物暴 float
    /// </summary>
    public ObscuredFloat physicalCrit { get; private set; }

    /// <summary>
    /// 抗物暴 float
    /// </summary>
    public ObscuredFloat UNphysicalCrit { get; private set; }

    /// <summary>
    /// 魔暴 float
    /// </summary>
    public ObscuredFloat magicCrit { get; private set; }

    /// <summary>
    /// 闪避 float
    /// </summary>
    public ObscuredFloat dodge { get; private set; }

    /// <summary>
    /// 命中 float
    /// </summary>
    public ObscuredFloat hit { get; private set; }

    /// <summary>
    /// 伤害加成 float
    /// </summary>
    public ObscuredFloat moreDamige { get; private set; }

    /// <summary>
    /// 伤害减免 float
    /// </summary>
    public ObscuredFloat avoidDamige { get; private set; }


    /// <summary>
    /// 生命
    /// </summary>
    public ObscuredInt HPAdd { get; private set; }
    /// <summary>
    /// 力量
    /// </summary>
    public ObscuredInt strengthAdd { get; private set; }
    /// <summary>
    /// 智力
    /// </summary>
    public ObscuredInt intelligenceAdd { get; private set; }
    /// <summary>
    /// 敏捷
    /// </summary>
    public ObscuredInt agileAdd { get; private set; }
    /// <summary>
    /// 物防
    /// </summary>
    public ObscuredInt physicalDefenseAdd { get; private set; }
    /// <summary>
    /// 护甲 float
    /// </summary>
    public ObscuredInt armorAdd { get; private set; }
    /// <summary>
    /// 魔防
    /// </summary>
    public ObscuredInt magicDefenseAdd { get; private set; }
    /// <summary>
    /// 魔抗 float
    /// </summary>
    public ObscuredInt magicResistanceAdd { get; private set; }
    /// <summary>
    /// 物暴 float
    /// </summary>
    public ObscuredFloat physicalCritAdd { get; private set; }
    /// <summary>
    /// 抗物暴 float
    /// </summary>
    public ObscuredFloat UNphysicalCritAdd { get; private set; }
    /// <summary>
    /// 魔暴 float
    /// </summary>
    public ObscuredFloat magicCritAdd { get; private set; }
    /// <summary>
    /// 闪避 float
    /// </summary>
    public ObscuredFloat dodgeAdd { get; private set; }
    /// <summary>
    /// 命中 float
    /// </summary>
    public ObscuredFloat hitAdd { get; private set; }

    /// <summary>
    /// 伤害加成 float
    /// </summary>
    public ObscuredFloat moreDamigeAdd { get; private set; }

    /// <summary>
    /// 伤害减免 float
    /// </summary>
    public ObscuredFloat avoidDamigeAdd { get; private set; }


    public List<RoleTalent> heroTalentList = new List<RoleTalent>();

    public ObscuredInt aspd { get; private set; }       //攻速
    public int move { get; private set; }       //移动

    public int[] teamPosition = new int[4];  //四个队伍的位置

    public int[] skill = new int[MAX_SKILL_COUNT];  //技能
    public int Skill1 = 0;              //主动技能
    public int Skill2 = 0;              //被动技能
    //public int[] skillLevel = new int[MAX_SKILL_COUNT];  //技能等级
    public Skill[] skillArray = new Skill[MAX_SKILL_COUNT];  //技能

    /// <summary>
    /// 进阶碎片最大值
    /// </summary>
    public int advanceCap;

    public BetterList<EquipInfo> equipList = new BetterList<EquipInfo>();
    public BetterList<RareStoneInfo> rareStoneList = new BetterList<RareStoneInfo>();
    public BetterList<SuperCarInfo> SuperCarList = new BetterList<SuperCarInfo>();
    public BetterList<WeaponInfo> WeaponList = new BetterList<WeaponInfo>();
    public class RareStoneInfo
    {
        public int stonePosition;
        public int stoneId;
        public int stoneLevel;
        public int stoneExp;

        public RareStoneInfo(int stonePosition, int stoneId, int stoneLevel, int stoneExp)
        {
            this.stonePosition = stonePosition;
            this.stoneId = stoneId;
            this.stoneLevel = stoneLevel;
            this.stoneExp = stoneExp;
        }
    }
    public class SuperCarInfo
    {
        public int SuperCarId1;
        public int SuperCarType1;
        public int SuperCarId2;
        public int SuperCarType2;
        public int SuperCarId3;
        public int SuperCarType3;
        public int SuperCarId4;
        public int SuperCarType4;
        public int SuperCarId5;
        public int SuperCarType5;
        public int SuperCarId6;
        public int SuperCarType6;
        public SuperCarInfo(int SuperCarId1, int SuperCarType1
            , int SuperCarId2, int SuperCarType2,
            int SuperCarId3, int SuperCarType3,
            int SuperCarId4, int SuperCarType4,
            int SuperCarId5, int SuperCarType5,
            int SuperCarId6, int SuperCarType6)
        {
            this.SuperCarId1 = SuperCarId1;
            this.SuperCarType1 = SuperCarType1;
            this.SuperCarId2 = SuperCarId2;
            this.SuperCarType2 = SuperCarType2;
            this.SuperCarId3 = SuperCarId3;
            this.SuperCarType3 = SuperCarType3;
            this.SuperCarId4 = SuperCarId4;
            this.SuperCarType4 = SuperCarType4;
            this.SuperCarId5 = SuperCarId5;
            this.SuperCarType5 = SuperCarType5;
            this.SuperCarId6 = SuperCarId6;
            this.SuperCarType6 = SuperCarType6;
        }
    }
    public class WeaponInfo
    {
        public int WeaponID;
        public int WeaponStar;
        public int WeaponClass;

        public WeaponInfo(int WeaponID, int WeaponClass, int WeaponStar, int CardID)
        {
            if (WeaponID == 0)
            {
                this.WeaponID = TextTranslator.instance.GetHeroInfoByHeroID(CardID).weaponID;
            }
            else
            {
                this.WeaponID = WeaponID;
            }
            this.WeaponClass = WeaponClass;
            this.WeaponStar = WeaponStar;
        }
    }
    public class EquipInfo
    {
        public int equipPosition;
        public int equipClass;
        public int equipLevel;
        public int equipCode;
        public int equipID;
        public int equipExp;
        public int equipColorNum;//颜色 1 ~ 21

        /// 装备孔位需要的class
        public int[] holeReqClass = new int[5];

        public EquipInfo(int newEquipPosition, int newEquipCode, int newEquipID, int newEquipClass, int newEquipLevel, int newEquipExp, int equipColorNum)
        {
            this.equipPosition = newEquipPosition;
            this.equipClass = newEquipClass;
            this.equipLevel = newEquipLevel;
            this.equipCode = newEquipCode;
            this.equipID = newEquipID;
            this.equipExp = newEquipExp;
            this.equipColorNum = equipColorNum;
        }
    }
    public class EquipInfoBefore
    {
        public int equipPosition;
        public int equipClass;
        public int equipLevel;
        public int equipCode;
        public int equipID;
        public int equipExp;
        public int equipColorNum;//颜色 1 ~ 21

        /// 装备孔位需要的class
        public int[] holeReqClass = new int[5];
        public EquipInfoBefore()
        { }
        public EquipInfoBefore(int newEquipPosition, int newEquipCode, int newEquipID, int newEquipClass, int newEquipLevel, int newEquipExp, int equipColorNum)
        {
            this.equipPosition = newEquipPosition;
            this.equipClass = newEquipClass;
            this.equipLevel = newEquipLevel;
            this.equipCode = newEquipCode;
            this.equipID = newEquipID;
            this.equipExp = newEquipExp;
            this.equipColorNum = equipColorNum;
        }
    }
    public Hero(int newRoleID, int newCardID)
    {
        this.characterRoleID = newRoleID;
        this.cardID = newCardID;

        HeroInfo hi = TextTranslator.instance.GetHeroInfoByHeroID(newCardID);
        this.area = hi.heroArea;
        this.ai = hi.heroAi;
        this.bio = hi.heroBio;
        this.name = hi.heroName;
    }

    public Hero(int newRoleID, int newCardID, int newTalent1, int newTalent2, int newTalent3, int newTalent4, int newTalent5, int newTalent6)
    {

        this.characterRoleID = newRoleID;
        this.cardID = newCardID;

        HeroInfo hi = TextTranslator.instance.GetHeroInfoByHeroID(newCardID);
        this.area = hi.heroArea;
        this.ai = hi.heroAi;
        this.bio = hi.heroBio;
        this.name = hi.heroName;

        heroTalentList.Clear();

        if (newTalent1 != 0)
        {
            RoleTalent ta = TextTranslator.instance.GetTalentByID(newTalent1);
            heroTalentList.Add(ta);
        }
        if (newTalent2 != 0)
        {
            RoleTalent ta = TextTranslator.instance.GetTalentByID(newTalent2);
            heroTalentList.Add(ta);
        }
        if (newTalent3 != 0)
        {
            RoleTalent ta = TextTranslator.instance.GetTalentByID(newTalent3);
            heroTalentList.Add(ta);
        }
        if (newTalent4 != 0)
        {
            RoleTalent ta = TextTranslator.instance.GetTalentByID(newTalent4);
            heroTalentList.Add(ta);
        }
        if (newTalent5 != 0)
        {
            RoleTalent ta = TextTranslator.instance.GetTalentByID(newTalent5);
            heroTalentList.Add(ta);
        }
        if (newTalent6 != 0)
        {
            RoleTalent ta = TextTranslator.instance.GetTalentByID(newTalent6);
            heroTalentList.Add(ta);
        }
    }

    public Hero(int newRoleID, int newCardID, string newName, int newSex, int newType, int newBio,
        int newSpecies, int newCost, int newSort, int newAdvanceCap)
    {
        this.characterRoleID = newRoleID;
        this.cardID = newCardID;
        this.name = newName;
        this.sex = newSex;
        this.type = newType;
        this.bio = newBio;
        this.species = newSpecies;
        this.cost = newCost;
        //this.position = newPosition;
        this.sort = newSort;
        this.advanceCap = newAdvanceCap;
    }

    public Hero() { }

    public void SetHeroCardID(int cardId)
    {
        this.cardID = cardId;
    }
    public void SetHeroProperty(int newForce, int newLevel,
        int newExp, int newMaxExp, int newRank, int newRare, int newClassNumber, int newSkillLevel, int newSkillNumber, int hpValue,
        int strengthValue, int physicalDefenseValue, float physicalCritValue,
        float UNphysicalCritValue, float dodgeValue, float hitValue, float moreDamigeValue, float avoidDamigeValue, int newAspd, int newMove, int hpValueAdd,
        int strengthValueAdd, int physicalDefenseValueAdd, float physicalCritValueAdd, float UNphysicalCritValueAdd,
        float dodgeValueAdd, float hitValueAdd, float moreDamigeAdd, float avoidDamigeAdd, int newSkillPoint)
    {
        this.force = newForce;
        this.level = newLevel;
        this.exp = newExp;
        this.maxExp = newMaxExp;
        this.rank = newRank;
        this.rare = newRare;
        this.classNumber = newClassNumber;
        this.skillLevel = newSkillLevel;
        this.skillNumber = newSkillNumber;

        this.HP = hpValue + hpValueAdd;
        this.strength = strengthValue + strengthValueAdd;
        this.physicalDefense = physicalDefenseValue + physicalDefenseValueAdd;
        this.physicalCrit = physicalCritValue + physicalCritValueAdd;
        this.UNphysicalCrit = UNphysicalCritValue + UNphysicalCritValueAdd;
        this.dodge = dodgeValue + dodgeValueAdd;
        this.hit = hitValue + hitValueAdd;
        this.moreDamige = moreDamigeValue + moreDamigeAdd;
        this.avoidDamige = avoidDamigeValue + avoidDamigeAdd;
        this.aspd = newAspd;
        this.move = newMove;

        this.HPAdd = hpValueAdd;
        this.strengthAdd = strengthValueAdd;
        this.physicalDefenseAdd = physicalDefenseValueAdd;
        this.physicalCritAdd = physicalCritValueAdd;
        this.UNphysicalCritAdd = UNphysicalCritValueAdd;
        this.dodgeAdd = dodgeValueAdd;
        this.hitAdd = hitValueAdd;
        this.moreDamigeAdd = moreDamigeAdd;
        this.avoidDamigeAdd = moreDamigeAdd;
        this.skillPoint = newSkillPoint;
    }


    public void SetTeamPosition(int partyIndex, int position)
    {
        teamPosition[partyIndex] = position;
    }

    public void SetHeroEquip(BetterList<EquipInfo> equipList)
    {
        this.equipList = equipList;
    }
    public void SetHeroRareStone(BetterList<RareStoneInfo> stoneList)
    {
        this.rareStoneList = stoneList;
    }
    public void SetSuperCarInfo(BetterList<SuperCarInfo> carinfoList)
    {
        this.SuperCarList = carinfoList;
    }
    public void SetWeaponInfo(BetterList<WeaponInfo> weaponinfoList)
    {
        this.WeaponList = weaponinfoList;
    }
    /// <summary>
    /// 得到主动技能
    /// </summary>
    public BetterList<Skill> GetFightSkill()
    {
        BetterList<Skill> skillList = new BetterList<Skill>();
        for (int i = 0; i < skill.Length; i++)
        {
            if (skill[i] > 100)
            {
                Skill s = TextTranslator.instance.GetSkillByID(skill[i], skillLevel);

                if (s != null && s.attribute != 3)
                {
                    skillList.Add(s);
                }
            }
        }
        return skillList;
    }
}
