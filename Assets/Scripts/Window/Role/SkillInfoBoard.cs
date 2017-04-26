using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SkillInfoBoard: MonoBehaviour 
{
    [SerializeField]
    private GameObject skillDesItem;
    [SerializeField]
    private GameObject uiGride;
    [SerializeField]
    private GameObject closeButton;
    [SerializeField]
    private GameObject maskButton;
    [SerializeField]
    private UITexture SkillIcon;
    [SerializeField]
    private UISprite spriteFrame;
    [SerializeField]
    private UILabel labelName;
    [SerializeField]
    private UILabel labelLevel;

    private Vector3 initScrollViewPos = new Vector3 (0,5,0);
    private Vector2 initOffeset = new Vector2(0,-10);
	// Use this for initialization
	void Start () 
    {
        UIEventListener.Get(closeButton).onClick = ClickCloseButton;
        UIEventListener.Get(maskButton).onClick = ClickCloseButton;
	}
    /// <summary>
    /// 图鉴中主动技能面板的显示
    /// </summary>
    /// <param name="mHeroInfo">当前英雄的信息</param>
    /// <param name="mHero">当前的英雄</param>
    public void SetSkillInfoBoard(HeroInfo mHeroInfo,Hero mHero)
    {
        for (int i = uiGride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGride.transform.GetChild(i).gameObject);
        }
        uiGride.transform.parent.localPosition = initScrollViewPos;
        uiGride.transform.parent.GetComponent<UIPanel>().clipOffset = initOffeset;
        if (mHero != null)
        {
            SkillIcon.mainTexture = Resources.Load(string.Format("Skill/{0}", TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[0], mHero.skillLevel).skillID), typeof(Texture)) as Texture;
            //labelName.text = string.Format("{0}\nLv.{1}", TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[0], mHero.skillLevel).skillName.ToString(), mHero.skillLevel); 
            labelName.text = TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[0], mHero.skillLevel).skillName.ToString();
            labelLevel.text = string.Format("Lv.{0}", mHero.skillLevel);
            //labelDes.text = GetAllSkillDes(mHeroInfo, mHero);
            List<Skill> heroSkillList = TextTranslator.instance.GetHeroSkillListByID(mHeroInfo.heroSkillList[0]);
            int height = 0;
            for (int i = 0; i < heroSkillList.Count; i++)
            {
                string colorDesStr = "[919090]";
                if (heroSkillList[i].skillLevel < mHero.skillLevel)
                {
                    colorDesStr = "[93d8f3]";
                }
                else if (heroSkillList[i].skillLevel == mHero.skillLevel)
                {
                    colorDesStr = "[3ee817]";
                }
                GameObject obj = NGUITools.AddChild(uiGride, skillDesItem) as GameObject;
                obj.SetActive(true);
                UILabel _LabelDes = obj.transform.FindChild("LabelDes").GetComponent<UILabel>();
                _LabelDes.text = string.Format("{0}{1}", colorDesStr, heroSkillList[i].description);
                obj.transform.FindChild("LabelLv").GetComponent<UILabel>().text = string.Format("{0}Lv.{1}", colorDesStr, heroSkillList[i].skillLevel);
                obj.transform.localPosition = new Vector3(0, -height, 0);
                obj.GetComponent<BoxCollider>().size = new Vector3(550, _LabelDes.height, 0);
                obj.GetComponent<BoxCollider>().center = new Vector3(250, -_LabelDes.height / 2.0f, 0);
                height += _LabelDes.height;
            }
        }
        else
        {
            SkillIcon.mainTexture = Resources.Load(string.Format("Skill/{0}", TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[0], 1).skillID), typeof(Texture)) as Texture;
            labelName.text = TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[0], 1).skillName.ToString();
            labelLevel.text = string.Format("Lv.{0}", 1);
            List<Skill> heroSkillList = TextTranslator.instance.GetHeroSkillListByID(mHeroInfo.heroSkillList[0]);
            int height = 0;
            for (int i = 0; i < heroSkillList.Count; i++)
            {
                string colorDesStr = "[919090]";
                if (heroSkillList[i].skillLevel < 1)
                {
                    colorDesStr = "[93d8f3]";
                }
                else if (heroSkillList[i].skillLevel == 1)
                {
                    colorDesStr = "[3ee817]";
                }
                GameObject obj = NGUITools.AddChild(uiGride, skillDesItem) as GameObject;
                obj.SetActive(true);
                UILabel _LabelDes = obj.transform.FindChild("LabelDes").GetComponent<UILabel>();
                _LabelDes.text = string.Format("{0}{1}", colorDesStr, heroSkillList[i].description);
                obj.transform.FindChild("LabelLv").GetComponent<UILabel>().text = string.Format("{0}Lv.{1}", colorDesStr, heroSkillList[i].skillLevel);
                obj.transform.localPosition = new Vector3(0, -height, 0);
                obj.GetComponent<BoxCollider>().size = new Vector3(550, _LabelDes.height, 0);
                obj.GetComponent<BoxCollider>().center = new Vector3(250, -_LabelDes.height / 2.0f, 0);
                height += _LabelDes.height;
            }
        }
    }

    string GetAllSkillDes(HeroInfo mHeroInfo, Hero mHero)
    {
        string _DesStr = "";
        string _NamesStr = "";
        List<Skill> heroSkillList = TextTranslator.instance.GetHeroSkillListByID(mHeroInfo.heroSkillList[0]);
       /* for (int i = 0; i < mHeroInfo.heroSkillList.Count; i++)
        {
            Debug.LogError(mHeroInfo.heroSkillList[i]);
        }*/
        for (int i = 0; i < heroSkillList.Count; i++)
        {
            string colorStr = "[919090]";
            if (heroSkillList[i].skillLevel <= mHero.skillLevel)
            {
                colorStr = "[93d8f3]";
            }
           // _DesStr += string.Format("{0}{1}\n",colorStr ,heroSkillList[i].description);
            _DesStr += string.Format("{0}Lv.{1}  {2}\n", colorStr,heroSkillList[i].skillLevel, heroSkillList[i].description);
            //labelNames.text += string.Format("{0}Lv.{1}{2}\n", "[ffffff]", heroSkillList[i].skillName, heroSkillList[i].skillLevel);
            //labelNames.text += string.Format("{0}Lv.{1}\n", "[ffffff]", heroSkillList[i].skillLevel);
        }
        return _DesStr;
    }
    private void ClickCloseButton(GameObject go)
    {
        if(go != null)
        {
            //UIManager.instance.BackUI();
            this.gameObject.SetActive(false);
        }
    }
}
