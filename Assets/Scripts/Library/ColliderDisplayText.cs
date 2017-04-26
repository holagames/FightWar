//--------------------------------------------
//            NGUI: HUD Text
// Copyright � 2012 Tasharen Entertainment
//--------------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Mono.Xml;
using System.IO;

/// <summary>
/// Example script that displays text above the collider when the collider is hovered over or clicked.
/// </summary>

public class ColliderDisplayText : MonoBehaviour
{
    // The UI prefab that is going to be instantiated above the player
    public HUDText mText = null;
    public UISlider mSlider = null;
    public UISlider mSkillSlider = null;
    public UILabel mNumber = null;
    public GameObject gameBoss = null;
    public GameObject gameCaptain = null;
    public GameObject gameWonder = null;

    public GameObject DetailInfo;
    UILabel LabelName;
    UILabel LabelLevel;
    UILabel LabelCareer;
    UILabel LabelInfo;
    public UILabel LabelTalk;

    public GameObject SpriteButton;
    public GameObject CaptainButton;
    public GameObject SpriteTarget;
    public GameObject SpriteSkill;
    public GameObject SpriteTalk;
    public GameObject SpriteNum;


    public GameObject SpriteRage;

    UISprite Buff1;
    UISprite Buff2;
    UISprite Buff3;
    UISprite Buff4;
    UISprite BuffText;
    public UISprite BuffNum;
    public UISprite SpriteHeroType;

    public UISprite BuffBoss;
    public UISprite BuffCaptain;
    public UISprite BuffWonder;

    public Camera gameCamera;
    public Camera uiCamera;

    public UISprite[] mSprite = new UISprite[8];

    public float MoveOffset = 65;

    Vector3 Pos1 = Vector3.zero;
    Vector3 Pos2 = Vector3.zero;

    Transform target;

    float HalfHeight;
    float HalfWidth;

    float Timer = 0;

    public void Init(Transform _target)
    {
        // We need the HUD object to know where in the hierarchy to put the element
        if (HUDRoot.go == null)
        {
            GameObject.Destroy(this);
            return;
        }
        HalfHeight = Screen.height / 2;
        HalfWidth = Screen.width / 2;

        GameObject childDamige = NGUITools.AddChild(HUDRoot.go, GameObject.Find("FightWindow").GetComponent<FightWindow>().prefabDamige);
        mText = childDamige.GetComponentInChildren<HUDText>();

        // Make the UI follow the target
        gameCamera = PictureCreater.instance.MyCamera.GetComponent<Camera>();
        uiCamera = UICamera.currentCamera;
        target = _target;

        GameObject childBlood = NGUITools.AddChild(HUDRoot.go, GameObject.Find("FightWindow").GetComponent<FightWindow>().prefabBlood);
        mSlider = childBlood.GetComponent<UISlider>();
        mSkillSlider = childBlood.transform.Find("Skill").Find("SpriteSkillBar").gameObject.GetComponent<UISlider>();
        SpriteRage = childBlood.transform.Find("SpriteSkill").gameObject;
        SpriteRage.SetActive(false);

        //mSprite[0] = childBlood.transform.Find("Skill").Find("SpriteSkill1").Find("Sprite").gameObject.GetComponent<UISprite>();
        //mSprite[1] = childBlood.transform.Find("Skill").Find("SpriteSkill2").Find("Sprite").gameObject.GetComponent<UISprite>();
        //mSprite[2] = childBlood.transform.Find("Skill").Find("SpriteSkill3").Find("Sprite").gameObject.GetComponent<UISprite>();
        //mSprite[3] = childBlood.transform.Find("Skill").Find("SpriteSkill4").Find("Sprite").gameObject.GetComponent<UISprite>();
        //mSprite[4] = childBlood.transform.Find("Skill").Find("SpriteSkill5").Find("Sprite").gameObject.GetComponent<UISprite>();
        //mSprite[5] = childBlood.transform.Find("Skill").Find("SpriteSkill6").Find("Sprite").gameObject.GetComponent<UISprite>();
        //mSprite[6] = childBlood.transform.Find("Skill").Find("SpriteSkill7").Find("Sprite").gameObject.GetComponent<UISprite>();
        //mSprite[7] = childBlood.transform.Find("Skill").Find("SpriteSkill8").Find("Sprite").gameObject.GetComponent<UISprite>();

        Buff1 = childBlood.transform.Find("SpriteBuff1").gameObject.GetComponent<UISprite>();
        Buff2 = childBlood.transform.Find("SpriteBuff2").gameObject.GetComponent<UISprite>();
        Buff3 = childBlood.transform.Find("SpriteBuff3").gameObject.GetComponent<UISprite>();
        Buff4 = childBlood.transform.Find("SpriteBuff4").gameObject.GetComponent<UISprite>();
        BuffText = childBlood.transform.Find("SpriteBuff").gameObject.GetComponent<UISprite>();

        Buff1.gameObject.SetActive(false);
        Buff2.gameObject.SetActive(false);
        Buff3.gameObject.SetActive(false);
        Buff4.gameObject.SetActive(false);

        SpriteSkill = childBlood.transform.Find("Skill").Find("SpriteSkill").gameObject;
        SpriteTalk = childBlood.transform.Find("Talk").gameObject;
        LabelTalk = childBlood.transform.Find("Talk").Find("LabelTalk").gameObject.GetComponent<UILabel>();
        SpriteSkill.SetActive(false);
        SpriteTalk.SetActive(false);


        Buff1.alpha = 0;
        Buff2.alpha = 0;
        Buff3.alpha = 0;
        Buff4.alpha = 0;
        BuffText.alpha = 0;


        SetSkillPoint(0);

        SpriteNum = NGUITools.AddChild(HUDRoot.go, GameObject.Find("FightWindow").GetComponent<FightWindow>().prefabSequence);
        mNumber = SpriteNum.transform.Find("LabelNum").GetComponent<UILabel>();
        BuffNum = mNumber.gameObject.transform.Find("SpriteNum").GetComponent<UISprite>();
        SpriteHeroType = mNumber.gameObject.transform.Find("SpriteNum/HeroType").GetComponent<UISprite>();
        if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 10 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 4)
        {
            BuffNum.depth = 30;
        }
        else
        {
            BuffNum.depth = 6;
        }
        SpriteButton = SpriteNum.transform.Find("SpriteButton").gameObject;
        CaptainButton = SpriteNum.transform.Find("CaptainButton").gameObject;
        SpriteTarget = SpriteNum.transform.Find("SpriteTarget").gameObject;
        SpriteTarget.SetActive(false);
        CaptainButton.SetActive(false);
        SetSequence(0, 1);

        UIEventListener.Get(CaptainButton).onClick += delegate(GameObject go)
        {
            Debug.Log(target.name);
            PictureCreater.instance.SetCaptain(target.name);
        };

        DetailInfo = SpriteNum.transform.Find("DetailInfo").gameObject;
        LabelName = DetailInfo.transform.Find("LabelName").gameObject.GetComponent<UILabel>();
        LabelLevel = DetailInfo.transform.Find("LabelLevel").gameObject.GetComponent<UILabel>();
        LabelCareer = DetailInfo.transform.Find("LabelCareer").gameObject.GetComponent<UILabel>();
        LabelInfo = DetailInfo.transform.Find("LabelInfo").gameObject.GetComponent<UILabel>();

        DetailInfo.SetActive(false);
        SpriteButton.SetActive(false);

        gameBoss = NGUITools.AddChild(HUDRoot.go, GameObject.Find("FightWindow").GetComponent<FightWindow>().prefabBoss);
        BuffBoss = gameBoss.GetComponent<UISprite>();
        BuffBoss.alpha = 0;

        gameCaptain = NGUITools.AddChild(HUDRoot.go, GameObject.Find("FightWindow").GetComponent<FightWindow>().prefabCaptain);
        BuffCaptain = gameCaptain.GetComponent<UISprite>();
        BuffCaptain.alpha = 0;

        gameWonder = NGUITools.AddChild(HUDRoot.go, GameObject.Find("FightWindow").GetComponent<FightWindow>().prefabWonder);
        BuffWonder = gameWonder.transform.Find("Sprite").GetComponent<UISprite>();
        BuffWonder.alpha = 0;
    }

    public void SetButton(bool IsVisible)
    {
        if (IsVisible)
        {
            if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 3 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 14)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_708);
                PlayerPrefs.SetInt(LuaDeliver.instance.GetGuideSubStateName(), PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) + 1);
                LuaDeliver.instance.UseGuideStation();
            }
            SpriteButton.GetComponent<UISprite>().alpha = 0;
            SpriteButton.SetActive(true);
            AudioEditer.instance.PlayOneShot("ui_recieve");
            StartCoroutine(ShowCaptain());
        }
        else
        {
            SpriteButton.SetActive(false);
            mNumber.transform.localPosition = new Vector3(0, 0, 0);
        }

    }

    public void AddRage()
    {
        SpriteRage.SetActive(true);
        SpriteRage.GetComponent<TweenPosition>().enabled = true;
        SpriteRage.GetComponent<TweenAlpha>().enabled = true;
        SpriteRage.GetComponent<TweenPosition>().ResetToBeginning();
        SpriteRage.GetComponent<TweenAlpha>().ResetToBeginning();
    }

    IEnumerator ShowCaptain()
    {
        for (int i = 0; i < 15; i++)
        {
            mNumber.transform.localPosition = new Vector3(i * 2, 0, 0);
            yield return new WaitForSeconds(0.01f);
        }

        for (int i = 0; i < 25; i++)
        {
            SpriteButton.GetComponent<UISprite>().alpha = i / 25f;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void SetDetailInfo(int EnemyID)
    {
        //DetailInfo.SetActive(true);

        for (int j = 0; j < TextTranslator.instance.enemyInfoList.Count; j++)
        {
            if (EnemyID == TextTranslator.instance.enemyInfoList[j].enemyID)
            {
                HeroInfo hi = TextTranslator.instance.GetHeroInfoByHeroID(TextTranslator.instance.enemyInfoList[j].pic);
                LabelName.text = hi.heroName;
                LabelLevel.text = "LV" + TextTranslator.instance.enemyInfoList[j].lv.ToString();
                LabelCareer.text = TextTranslator.instance.enemyInfoList[j].name + "(" + TextTranslator.instance.GetArmor(TextTranslator.instance.enemyInfoList[j].ai) + ")\n" + hi.heroInfo;
                LabelInfo.text = TextTranslator.instance.GetArmorInfo(TextTranslator.instance.enemyInfoList[j].ai);
            }
        }
    }

    public void SetBoss()
    {
        BuffBoss.alpha = 1;
    }


    public void SetBuff(int Buff)
    {
        if (Buff1.alpha == 0)
        {
            Buff1.spriteName = Buff.ToString();
            Buff1.gameObject.SetActive(true);
            Buff1.MakePixelPerfect();
            Buff1.width = Buff1.width * 7 / 10;
            Buff1.height = Buff1.height * 7 / 10;
            Buff1.GetComponent<TweenPosition>().enabled = true;
            Buff1.GetComponent<TweenAlpha>().enabled = true;
            Buff1.GetComponent<TweenPosition>().ResetToBeginning();
            Buff1.GetComponent<TweenAlpha>().ResetToBeginning();
        }
        else if (Buff2.alpha == 0 && Buff1.spriteName != Buff.ToString())
        {
            Buff2.spriteName = Buff.ToString();
            Buff2.gameObject.SetActive(true);
            Buff2.MakePixelPerfect();
            Buff2.width = Buff2.width * 7 / 10;
            Buff2.height = Buff2.height * 7 / 10;
            Buff2.GetComponent<TweenPosition>().enabled = true;
            Buff2.GetComponent<TweenAlpha>().enabled = true;
            Buff2.GetComponent<TweenPosition>().ResetToBeginning();
            Buff2.GetComponent<TweenAlpha>().ResetToBeginning();
        }
        else if (Buff3.alpha == 0 && Buff1.spriteName != Buff.ToString() && Buff2.spriteName != Buff.ToString())
        {
            Buff3.spriteName = Buff.ToString();
            Buff3.gameObject.SetActive(true);
            Buff3.MakePixelPerfect();
            Buff3.width = Buff3.width * 7 / 10;
            Buff3.height = Buff3.height * 7 / 10;
            Buff3.GetComponent<TweenPosition>().enabled = true;
            Buff3.GetComponent<TweenAlpha>().enabled = true;
            Buff3.GetComponent<TweenPosition>().ResetToBeginning();
            Buff3.GetComponent<TweenAlpha>().ResetToBeginning();
        }
        else if (Buff4.alpha == 0 && Buff1.spriteName != Buff.ToString() && Buff2.spriteName != Buff.ToString() && Buff3.spriteName != Buff.ToString())
        {
            Buff4.spriteName = Buff.ToString();
            Buff4.gameObject.SetActive(true);
            Buff4.MakePixelPerfect();
            Buff4.width = Buff4.width * 7 / 10;
            Buff4.height = Buff4.height * 7 / 10;
            Buff4.GetComponent<TweenPosition>().enabled = true;
            Buff4.GetComponent<TweenAlpha>().enabled = true;
            Buff4.GetComponent<TweenPosition>().ResetToBeginning();
            Buff4.GetComponent<TweenAlpha>().ResetToBeginning();
        }
    }


    public void RemoveSetBuff(int Buff)
    {
        if (Buff1.spriteName == Buff.ToString())
        {
            Buff1.alpha = 0;
            Buff1.spriteName = "";
        }
        else if (Buff2.spriteName == Buff.ToString())
        {
            Buff2.alpha = 0;
            Buff2.spriteName = "";
        }
        else if (Buff3.spriteName == Buff.ToString())
        {
            Buff3.alpha = 0;
            Buff3.spriteName = "";
        }
        else if (Buff4.spriteName == Buff.ToString())
        {
            Buff4.alpha = 0;
            Buff4.spriteName = "";
        }
    }

    public void SetBuffText(int Buff)
    {
        BuffText.spriteName = "h" + Buff.ToString();
        BuffText.alpha = 1;
        BuffText.GetComponent<TweenAlpha>().enabled = true;
        BuffText.GetComponent<TweenPosition>().enabled = true;
        BuffText.GetComponent<TweenScale>().enabled = true;

        BuffText.GetComponent<TweenAlpha>().ResetToBeginning();
        BuffText.GetComponent<TweenPosition>().ResetToBeginning();
        BuffText.GetComponent<TweenScale>().ResetToBeginning();
    }

    public void RemoveBuffText()
    {
        BuffText.alpha = 0;
    }


    public void RemoveAllBuff()
    {
        Buff1.alpha = 0;
        Buff2.alpha = 0;
        Buff3.alpha = 0;
        Buff4.alpha = 0;
    }

    public void RemoveBuff(int Buff)
    {
        if (Buff1.alpha == 1)
        {
            Buff1.alpha = 0;
        }
        else if (Buff2.alpha == 1)
        {
            Buff2.alpha = 0;
        }
        else if (Buff3.alpha == 1)
        {
            Buff3.alpha = 0;
        }
        else if (Buff4.alpha == 1)
        {
            Buff4.alpha = 0;
        }
    }

    public void ShowTarget(int Type)
    {
        BuffNum.gameObject.SetActive(false);
        SpriteNum.SetActive(true);
        SpriteTarget.GetComponent<UISprite>().spriteName = "target" + Type.ToString();
        SpriteTarget.SetActive(true);
    }

    public void HideTarget()
    {
        SpriteNum.SetActive(false);
        SpriteTarget.SetActive(false);
    }


    public void SetSequence(int Num, int HeroType)
    {
        BuffNum.spriteName = Num.ToString();
        //mNumber.text = Num.ToString();
        mNumber.text = "";
        if (Num == 0)
        {
            SpriteNum.SetActive(false);
        }
        else
        {
            SpriteNum.SetActive(true);
        }

        if (HeroType == 1)
        {
            SpriteHeroType.spriteName = "heroType1";
        }
        else if (HeroType == 2)
        {
            SpriteHeroType.spriteName = "heroType2";
        }
        else if (HeroType == 3)
        {
            SpriteHeroType.spriteName = "heroType3";
        }
    }

    public void SetSkillPoint(int SkillPoint)
    {
        //for (int i = 0; i < 8; i++)
        //{
        //    if (SkillPoint > i)
        //    {
        //        mSprite[i].alpha = 1;
        //    }
        //    else
        //    {
        //        mSprite[i].alpha = 0;
        //    }
        //}

        mSkillSlider.value = SkillPoint / 1000f;
    }

    void FixedUpdate()
    {
        if (mText != null)
        {
            Vector3 pos = gameCamera.WorldToScreenPoint(target.position);

            pos = new Vector3((pos.x - HalfWidth) * 800f / Screen.height, (pos.y - HalfHeight) * 800f / Screen.height, 0);
            pos.x = Mathf.FloorToInt(pos.x * UIRootExtend.instance.isUiRootRatio);
            pos.y = Mathf.FloorToInt(pos.y * UIRootExtend.instance.isUiRootRatio + 180);
            pos.z = 0f;
            mText.transform.localPosition = pos;
        }

        if (mSlider != null)
        {
            Vector3 pos = gameCamera.WorldToScreenPoint(target.position);

            pos = new Vector3((pos.x - HalfWidth) * 800f / Screen.height, (pos.y - HalfHeight) * 800f / Screen.height, 0);
            pos.x = Mathf.FloorToInt(pos.x * UIRootExtend.instance.isUiRootRatio);
            pos.y = Mathf.FloorToInt(pos.y * UIRootExtend.instance.isUiRootRatio + 140);
            pos.z = 0f;
            mSlider.transform.localPosition = pos;
        }

        if (mNumber != null)
        {
            Vector3 pos = gameCamera.WorldToScreenPoint(target.position);

            pos = new Vector3((pos.x - HalfWidth) * 800f / Screen.height, (pos.y - HalfHeight) * 800f / Screen.height, 0);
            pos.x = Mathf.FloorToInt(pos.x * UIRootExtend.instance.isUiRootRatio - 15);
            pos.y = Mathf.FloorToInt(pos.y * UIRootExtend.instance.isUiRootRatio + 90);
            pos.z = 0f;
            SpriteNum.transform.localPosition = pos;
        }

        if (gameBoss != null)
        {
            Vector3 pos = gameCamera.WorldToScreenPoint(target.position);

            pos = new Vector3((pos.x - HalfWidth) * 800f / Screen.height, (pos.y - HalfHeight) * 800f / Screen.height, 0);
            pos.x = Mathf.FloorToInt(pos.x * UIRootExtend.instance.isUiRootRatio);
            pos.y = Mathf.FloorToInt(pos.y * UIRootExtend.instance.isUiRootRatio + 130);
            pos.z = 0f;
            gameBoss.transform.localPosition = pos;
        }

        if (gameCaptain != null)
        {
            Vector3 pos = gameCamera.WorldToScreenPoint(target.position);

            pos = new Vector3((pos.x - HalfWidth) * 800f / Screen.height, (pos.y - HalfHeight) * 800f / Screen.height, 0);
            pos.x = Mathf.FloorToInt(pos.x * UIRootExtend.instance.isUiRootRatio);
            pos.y = Mathf.FloorToInt(pos.y * UIRootExtend.instance.isUiRootRatio + 170);
            pos.z = 0f;
            gameCaptain.transform.localPosition = pos;
        }

        if (gameWonder != null)
        {
            Vector3 pos = gameCamera.WorldToScreenPoint(target.position);

            pos = new Vector3((pos.x - HalfWidth) * 800f / Screen.height, (pos.y - HalfHeight) * 800f / Screen.height, 0);
            pos.x = Mathf.FloorToInt(pos.x * UIRootExtend.instance.isUiRootRatio);
            pos.y = Mathf.FloorToInt(pos.y * UIRootExtend.instance.isUiRootRatio + 170);
            pos.z = 0f;
            gameWonder.transform.localPosition = pos;
        }
    }
}
