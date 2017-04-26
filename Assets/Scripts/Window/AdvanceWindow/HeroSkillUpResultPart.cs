using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroSkillUpResultPart: MonoBehaviour
{
    [SerializeField]
    private GameObject uiGride;
    [SerializeField]
    private GameObject heroSkillUpResultItem;
    //-----------界面basicInfo
    [SerializeField]
    private UISprite frame1;
    [SerializeField]
    private UITexture icon1;
    [SerializeField]
    private UILabel nameLabel1;
    [SerializeField]
    private UILabel nameLabel2;
    //------------数据
    private List<string> desList = new List<string>();
    private List<int> leftDataList = new List<int>();
    private List<int> rightDataList = new List<int>();
    private List<float> leftDataList2 = new List<float>();
    private List<float> rightDataList2 = new List<float>();
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    public void SetHeroSkillUpResultPart(Hero mHero)
    {

        StartCoroutine(DelayShowInfo(mHero));
    }
    IEnumerator DelayShowInfo(Hero mHero)
    {
        frame1.spriteName = "";
        //icon1.spriteName = "";
        icon1.mainTexture = null;
        nameLabel1.text = "";
        nameLabel2.text = "";
        yield return new WaitForSeconds(1.0f);

        HeroInfo mHeroInfo = TextTranslator.instance.GetHeroInfoByHeroID(mHero.cardID);
        Skill mSkill = TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[0], mHero.skillLevel);
        frame1.spriteName = "skillFrame";
        //icon1.spriteName = mSkill.skillID.ToString();
        icon1.mainTexture = Resources.Load(string.Format("Skill/{0}", mSkill.skillID), typeof(Texture)) as Texture;
        nameLabel1.text = string.Format("[{0}]\n等级:{1}", mSkill.skillName, mSkill.skillLevel - 1);
        nameLabel2.text = string.Format("[{0}]\n等级:{1}", mSkill.skillName, mSkill.skillLevel);
        GetDataList(mHero);
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < leftDataList.Count; i++)
        {
            GameObject obj = NGUITools.AddChild(uiGride, heroSkillUpResultItem) as GameObject;
            obj.GetComponent<HeroSkillUpResultItem>().SetHeroSkillUpResultItem(desList[i], leftDataList[i], rightDataList[i]);
        }
        for (int i = 0; i < leftDataList2.Count; i++)
        {
            GameObject obj = NGUITools.AddChild(uiGride, heroSkillUpResultItem) as GameObject;
            obj.GetComponent<HeroSkillUpResultItem>().SetHeroSkillUpResultItem(desList[leftDataList.Count + i], leftDataList2[i], rightDataList2[i]);
        }
        uiGride.GetComponent<UIGrid>().Reposition();
    }
    public void SetEquipJinJieResultPart(Hero.EquipInfoBefore _mEquipInfoBefore,Hero.EquipInfo mEquipInfo)
    {
        for (int i = 0; i < leftDataList.Count; i++)
        {
            GameObject obj = NGUITools.AddChild(uiGride, heroSkillUpResultItem) as GameObject;
            obj.GetComponent<EquipJinJieResultItem>().SetEquipJinJieResultItem(desList[i],leftDataList[i],rightDataList[i]);
        }
        for (int i = 0; i < leftDataList2.Count; i++)
        {
            GameObject obj = NGUITools.AddChild(uiGride, heroSkillUpResultItem) as GameObject;
            obj.GetComponent<EquipJinJieResultItem>().SetEquipJinJieResultItem(desList[leftDataList.Count + i], leftDataList2[i], rightDataList2[i]);
        }
        uiGride.GetComponent<UIGrid>().Reposition();
    }

    void GetDataList(Hero mHero)
    {
        desList.Clear();
        leftDataList2.Clear();
        rightDataList2.Clear();
        int usefulLevel = mHero.skillLevel;
        int usefulLevelLast = mHero.skillLevel - 1;
        RoleDestiny Cur_rd = TextTranslator.instance.GetRoleDestinyByID(mHero.cardID, usefulLevel);
        RoleDestiny Cur_rdLast = TextTranslator.instance.GetRoleDestinyByID(mHero.cardID, usefulLevel - 1);

        //leftDataList.Add(usefulLevel);
        //rightDataList.Add(usefulLevelLast);
        //desList.Add("等级: ");

        if (TextTranslator.instance.heroNow.force != TextTranslator.instance.heroBefore.force)
        {
            desList.Add("战斗力: ");
            leftDataList.Add(TextTranslator.instance.heroBefore.force);
            rightDataList.Add(TextTranslator.instance.heroNow.force);
        }

        if (Cur_rd == null)
        {
            return;
        }

        if (Cur_rd.HP > 0)
        {
            desList.Add("生命: ");
            rightDataList2.Add(Cur_rd.HP);
            leftDataList2.Add(Cur_rdLast.HP);
        }
        if (Cur_rd.ATK > 0)
        {
            desList.Add("攻击: ");
            rightDataList2.Add(Cur_rd.ATK);
            leftDataList2.Add(Cur_rdLast.ATK);
        }
        if (Cur_rd.DEF > 0)
        {
            desList.Add("防御: ");
            rightDataList2.Add(Cur_rd.DEF);
            leftDataList2.Add(Cur_rdLast.DEF);
        }
      /*  if (es.Hit > 0)
        {
            desStr += "命中: " + (es.Hit * usefulClass).ToString() + "\n";
            desList.Add("命中: ");
            //leftDataList.Add(es.Hit * usefulClass);
            //rightDataList.Add(es.Hit * usefulClassRight);
            leftDataList2.Add(es.Hit * usefulClass);
            rightDataList2.Add(es.Hit * usefulClassRight);
        }
        if (es.NoHit > 0)
        {
            desStr += "闪避: " + (es.NoHit * usefulClass).ToString() + "\n";
            desList.Add("闪避: ");
            leftDataList2.Add(es.NoHit * usefulClass);
            rightDataList2.Add(es.NoHit * usefulClassRight);
        }
        if (es.Crit > 0)
        {
            desStr += "暴击: " + (es.Crit * usefulClass).ToString() + "\n";
            desList.Add("暴击: ");
            leftDataList2.Add(es.Crit * usefulClass);
            rightDataList2.Add(es.Crit * usefulClassRight);
        }
        if (es.NoCrit > 0)
        {
            desStr += "抗暴: " + (es.NoCrit * usefulClass).ToString() + "\n";
            desList.Add("抗暴: ");
            leftDataList2.Add(es.NoCrit * usefulClass);
            rightDataList2.Add(es.NoCrit * usefulClassRight);
        }
        if (es.DmgBonus > 0)
        {
            desStr += "伤害加成: " + (es.DmgBonus * usefulClass).ToString() + "\n";
            desList.Add("伤害加成: ");
            leftDataList2.Add(es.DmgBonus * usefulClass);
            rightDataList2.Add(es.DmgBonus * usefulClassRight);
        }
        if (es.DmgReduction > 0)
        {
            desStr += "伤害减免: " + (es.DmgReduction * usefulClass).ToString() + "\n";
            desList.Add("伤害减免: ");
            leftDataList2.Add(es.DmgReduction * usefulClass);
            rightDataList2.Add(es.DmgReduction * usefulClassRight);
        } */
    }
    
}
