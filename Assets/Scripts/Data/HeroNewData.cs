using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroNewData
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
    public int force { get; set; }               // 战斗力
    public int level { get; set; }               // 等级
    public int maxLevel { get; private set; }            // 等级上限
    public int exp { get; set; }                 // 当前经验
    public int maxExp { get; set; }              // 当前最大经验
    public int skillLevel { get; set; }             //天命等级
    public int skillNumber { get; set; }            //天命值
    /// <summary>
    /// 军衔。分1-6星，用C,UC,R,SR,SSR,LE等来对应表示
    /// </summary>
    public int rank { get; set; }

    /// <summary>
    /// 颜色。决定角色当前的等级上限，达到上限时可通过进阶道具和游戏货币来进阶突破上限
    /// </summary>
    public int classNumber { get;  set; }

    /// <summary>
    /// 稀有度。决定角色当前的等级上限，达到上限时可通过进阶道具和游戏货币来进阶突破上限
    /// </summary>
    public int rare { get; private set; }

    /// <summary>
    /// 生命
    /// </summary>
    public int HP { get;  set; }

    /// <summary>
    /// 力量
    /// </summary>
    public int strength { get; set; }

    /// <summary>
    /// 智力
    /// </summary>
    public int intelligence { get; private set; }

    /// <summary>
    /// 敏捷
    /// </summary>
    public int agile { get; private set; }

    /// <summary>
    /// 物防
    /// </summary>
    public int physicalDefense { get; set; }

    /// <summary>
    /// 护甲 float
    /// </summary>
    public int armor { get; private set; }

    /// <summary>
    /// 魔防
    /// </summary>
    public int magicDefense { get; private set; }

    /// <summary>
    /// 魔抗 float
    /// </summary>
    public int magicResistance { get; private set; }

    /// <summary>
    /// 物暴 float
    /// </summary>
    public float physicalCrit { get; private set; }

    /// <summary>
    /// 抗物暴 float
    /// </summary>
    public float UNphysicalCrit { get; private set; }

    /// <summary>
    /// 魔暴 float
    /// </summary>
    public int magicCrit { get; private set; }

    /// <summary>
    /// 闪避 float
    /// </summary>
    public float dodge { get; private set; }

    /// <summary>
    /// 命中 float
    /// </summary>
    public float hit { get; private set; }

    /// <summary>
    /// 伤害加成 float
    /// </summary>
    public float moreDamige { get; private set; }

    /// <summary>
    /// 伤害减免 float
    /// </summary>
    public float avoidDamige { get; private set; }


    /// <summary>
    /// 生命
    /// </summary>
    public int HPAdd { get; private set; }
    /// <summary>
    /// 力量
    /// </summary>
    public int strengthAdd { get; private set; }
    /// <summary>
    /// 智力
    /// </summary>
    public int intelligenceAdd { get; private set; }
    /// <summary>
    /// 敏捷
    /// </summary>
    public int agileAdd { get; private set; }
    /// <summary>
    /// 物防
    /// </summary>
    public int physicalDefenseAdd { get; private set; }
    /// <summary>
    /// 护甲 float
    /// </summary>
    public int armorAdd { get; private set; }
    /// <summary>
    /// 魔防
    /// </summary>
    public int magicDefenseAdd { get; private set; }
    /// <summary>
    /// 魔抗 float
    /// </summary>
    public int magicResistanceAdd { get; private set; }
    /// <summary>
    /// 物暴 float
    /// </summary>
    public float physicalCritAdd { get; private set; }
    /// <summary>
    /// 抗物暴 float
    /// </summary>
    public float UNphysicalCritAdd { get; private set; }
    /// <summary>
    /// 魔暴 float
    /// </summary>
    public int magicCritAdd { get; private set; }
    /// <summary>
    /// 闪避 float
    /// </summary>
    public float dodgeAdd { get; private set; }
    /// <summary>
    /// 命中 float
    /// </summary>
    public float hitAdd { get; private set; }

    /// <summary>
    /// 伤害加成 float
    /// </summary>
    public float moreDamigeAdd { get; private set; }

    /// <summary>
    /// 伤害减免 float
    /// </summary>
    public float avoidDamigeAdd { get; private set; }


    public List<RoleTalent> heroTalentList = new List<RoleTalent>();

    public int aspd { get; private set; }       //攻速
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

    public class EquipInfo
    {
        public int equipPosition;
        public int equipClass;
        public int equipLevel;
        public int equipCode;
        public int equipID;
        public int equipExp;

        /// 装备孔位需要的class
        public int[] holeReqClass = new int[5];

        public EquipInfo(int newEquipPosition, int newEquipCode, int newEquipID, int newEquipClass, int newEquipLevel, int newEquipExp)
        {
            this.equipPosition = newEquipPosition;
            this.equipClass = newEquipClass;
            this.equipLevel = newEquipLevel;
            this.equipCode = newEquipCode;
            this.equipID = newEquipID;
            this.equipExp = newEquipExp;
        }
    }
    public HeroNewData(int newRoleID, int newCardID)
    {
        this.characterRoleID = newRoleID;
        this.cardID = newCardID;

        HeroInfo hi = TextTranslator.instance.GetHeroInfoByHeroID(newCardID);
        this.area = hi.heroArea;
        this.ai = hi.heroAi;
        this.bio = hi.heroBio;
        this.name = hi.heroName;
    }

    public HeroNewData(int newRoleID, int newCardID, int newTalent1, int newTalent2, int newTalent3, int newTalent4, int newTalent5, int newTalent6)
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

    public HeroNewData(int newRoleID, int newCardID, string newName, int newSex, int newType, int newBio,
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

    public HeroNewData() { }


    public void SetHeroProperty(int newForce, int newLevel,
        int newExp,int newMaxExp, int newRank, int newRare, int newClassNumber, int newSkillLevel,int newSkillNumber, int hpValue,
        int strengthValue, int physicalDefenseValue, float physicalCritValue,
        float UNphysicalCritValue, float dodgeValue, float hitValue, float moreDamigeValue, float avoidDamigeValue, int newAspd, int newMove, int hpValueAdd,
        int strengthValueAdd, int physicalDefenseValueAdd, float physicalCritValueAdd, float UNphysicalCritValueAdd,
        float dodgeValueAdd, float hitValueAdd, float moreDamigeAdd, float avoidDamigeAdd)
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

        this.HP = hpValue+hpValueAdd;
        this.strength = strengthValue+strengthValueAdd;
        this.physicalDefense = physicalDefenseValue+physicalDefenseValueAdd;
        this.physicalCrit = physicalCritValue + physicalCritValueAdd;
        this.UNphysicalCrit = UNphysicalCritValue + UNphysicalCritValueAdd;
        this.dodge = dodgeValue + dodgeValueAdd;
        this.hit = hitValue + hitValueAdd;
        this.moreDamige = moreDamigeValue + moreDamigeAdd;
        this.avoidDamige = moreDamigeValue + moreDamigeAdd;
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

    }


    public void SetTeamPosition(int partyIndex, int position)
    {
        teamPosition[partyIndex] = position;
    }

    public void SetHeroEquip(BetterList<EquipInfo> equipList)
    {
        this.equipList = equipList;
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
                Skill s = TextTranslator.instance.GetSkillByID(skill[i],skillLevel);

                if (s != null && s.attribute != 3)
                {
                    skillList.Add(s);
                }
            }
        }
        return skillList;
    }
}
