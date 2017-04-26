using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Skill2InfoBoard: MonoBehaviour 
{
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
    private UILabel labelDes;
    private Vector3 initScrollViewPos = new Vector3 (0,0,0);
    private Vector2 initOffeset = new Vector2(0,0);
	// Use this for initialization
	void Start () 
    {
        UIEventListener.Get(closeButton).onClick = ClickCloseButton;
        UIEventListener.Get(maskButton).onClick = ClickCloseButton;
	}

    public void SetSkillInfoBoard(HeroInfo mHeroInfo)
    {

        SkillIcon.mainTexture = Resources.Load(string.Format("Skill/{0}", TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[1], 1).skillID), typeof(Texture)) as Texture;

        labelName.text = TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[1], 1).skillName.ToString();
        labelDes.text = "[93d8f3]" + TextTranslator.instance.GetSkillByID(mHeroInfo.heroSkillList[1], 1).description;

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
