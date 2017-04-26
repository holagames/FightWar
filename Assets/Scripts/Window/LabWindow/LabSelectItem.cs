using UnityEngine;
using System.Collections;
using System;

public class LabSelectItem : MonoBehaviour
{
    public GameObject trainButton;
    public GameObject gride;

    public UISprite pinJieSprite;
    public UISprite SpriteAvatar;
    public UISprite heroTypeIcon;
    public UISprite RarityIcon;
    public UISprite frame;

    public UISlider mySlider;
    public UILabel sliderLabel;
    public UILabel LabelName;
    public UILabel levelLabel;
    public UILabel scoreLabel;
    public UILabel conditionLabel;

    private Hero mHero;
    private HeroInfo _heroInfo;
    // Use this for initialization
    void Start()
    {

        if (UIEventListener.Get(trainButton).onClick == null)
        {
            UIEventListener.Get(trainButton).onClick = delegate(GameObject go)
            {
                RemoveHero();
                //NetworkHandler.instance.SendProcess(string.Format("8019#{0};{1};", TrainingGroundItem.curClickTrainingGroundItemId, this.mHero.characterRoleID));
                NetworkHandler.instance.SendProcess(string.Format("1802#{0};{1};{2};", TextTranslator.instance.roleType, ReformLabItem.curClickTrainingGroundItemId, this.mHero.characterRoleID));
                Destroy(GameObject.Find("LabSelectWindow"));
            };
        }

    }
    void RemoveHero()
    {
        LabWindow _LabWindow = GameObject.Find("LabWindow").GetComponent<LabWindow>();
        for (int i = 0; i < _LabWindow.itemObjList.Count; i++)
        {
            if (_LabWindow.itemObjList[i].GetComponent<ReformLabItem>().mItemData.LabItemPosNum == ReformLabItem.curClickTrainingGroundItemId && _LabWindow.itemObjList[i].GetComponent<ReformLabItem>().mItemData.state == 2)
            {
                NetworkHandler.instance.SendProcess(string.Format("1803#{0};{1};", TextTranslator.instance.roleType, ReformLabItem.curClickTrainingGroundItemId));
            }
        }
    }
    public void SetLabSelectItem(Hero mHero)
    {
        this.mHero = mHero;
        this.mHero.score = GetScoreOfOneHero(mHero);
        HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(mHero.cardID);
        _heroInfo = hinfo;
        LabelName.text = hinfo.heroName;
        TextTranslator.instance.SetHeroNameColor(LabelName, hinfo.heroName, mHero.classNumber);
        levelLabel.text = mHero.level.ToString();
        SpriteAvatar.spriteName = mHero.cardID.ToString();

        mySlider.value = (float)mHero.exp / (float)mHero.maxExp;
        sliderLabel.text = string.Format("{0}/{1}", mHero.exp, mHero.maxExp);
        mySlider.gameObject.SetActive(false);
        scoreLabel.text = string.Format("{0}%", Math.Round(mHero.score / 3000f, 1));//mHero.score.ToString();

        SetRarityIcon();
        SetHeroType(hinfo.heroCarrerType);

        DestroyGride();
        int addNum = TextTranslator.instance.SetHeroNameColor(frame, pinJieSprite, mHero.classNumber);
        for (int i = 0; i < addNum; i++)
        {
            GameObject obj = NGUITools.AddChild(gride, pinJieSprite.gameObject);
            obj.SetActive(true);
        }
        UIGrid _UIGrid = gride.GetComponent<UIGrid>();
        _UIGrid.sorting = UIGrid.Sorting.Horizontal;
        _UIGrid.pivot = UIWidget.Pivot.Center;
        _UIGrid.animateSmoothly = true;

        if (mHero.level >= 30 && mHero.classNumber >= 5)
        {
            trainButton.SetActive(true);
            conditionLabel.gameObject.SetActive(false);
        }
        else
        {
            trainButton.SetActive(false);
            conditionLabel.gameObject.SetActive(true);
        }
    }
    int GetScoreOfOneHero(Hero mHero)
    {
        float heroNum = 0;
        switch (mHero.rare)
        {
            case 1: heroNum = 1; break;
            case 2: heroNum = 1.1f; break;
            case 3: heroNum = 1.2f; break;
            case 4: heroNum = 1.4f; break;
            case 5: heroNum = 1.8f; break;
        }
        int score1 = (int)(mHero.level * TextTranslator.instance.GetLabsPointByID(1).RankPoint * heroNum);
        int score2 = (int)(mHero.classNumber * TextTranslator.instance.GetLabsPointByID(2).RankPoint * heroNum);
        int score3 = (int)((mHero.rank - 1) * TextTranslator.instance.GetLabsPointByID(3).RankPoint * heroNum);
        int score4 = 0;
        int _rank4Point = TextTranslator.instance.GetLabsPointByID(4).RankPoint;
        int score5 = 0;
        int _rank5Point = TextTranslator.instance.GetLabsPointByID(5).RankPoint;
        int score6 = 0;
        int _rank6Point = TextTranslator.instance.GetLabsPointByID(6).RankPoint;
        int score7 = 0;
        int _rank7Point = TextTranslator.instance.GetLabsPointByID(7).RankPoint;
        for (int i = 0; i < mHero.equipList.size; i++)
        {
            if (i < 4)
            {
                score4 += mHero.equipList[i].equipColorNum * _rank4Point;
                score5 += mHero.equipList[i].equipClass * _rank5Point;
            }
            else
            {
                int grade = TextTranslator.instance.GetItemByItemCode(mHero.equipList[i].equipCode).itemGrade;
                int myNum = 0;
                switch (grade)
                {
                    case 1: myNum = 0; break;
                    case 2: myNum = 1; break;
                    case 3: myNum = 2; break;
                    case 4: myNum = 3; break;
                    case 5: myNum = 4; break;
                    case 6: myNum = 5; break;
                }
                score6 += mHero.equipList[i].equipLevel * _rank6Point * myNum;
                score7 += mHero.equipList[i].equipClass * _rank7Point * myNum;
            }
        }
        int score8 = (int)(mHero.skillLevel * TextTranslator.instance.GetLabsPointByID(8).RankPoint * heroNum);
        int score9 = 0;
        int score10 = 0;
        int _rank9Point = TextTranslator.instance.GetLabsPointByID(9).RankPoint;
        for (int i = 0; i < mHero.rareStoneList.size; i++)
        {
            int grade = TextTranslator.instance.GetItemByItemCode(mHero.rareStoneList[i].stoneId).itemGrade;
            int myNum = 0;
            switch (grade)
            {
                case 1: myNum = 1; break;
                case 2: myNum = 2; break;
                case 3: myNum = 3; break;
                case 4: myNum = 4; break;
                case 5: myNum = 5; break;
                case 6: myNum = 6; break;
            }
            score9 += mHero.rareStoneList[i].stoneLevel * _rank9Point * myNum;
        }

        if (mHero.SuperCarList != null)
        {
            if (mHero.SuperCarList.size > 0)
            {
                score10 += mHero.SuperCarList[0].SuperCarType1 * TextTranslator.instance.GetLabsPointByID(10).RankPoint;
                score10 += mHero.SuperCarList[0].SuperCarType2 * TextTranslator.instance.GetLabsPointByID(10).RankPoint * 2;
                score10 += mHero.SuperCarList[0].SuperCarType3 * TextTranslator.instance.GetLabsPointByID(10).RankPoint * 3;
                score10 += mHero.SuperCarList[0].SuperCarType4 * TextTranslator.instance.GetLabsPointByID(10).RankPoint * 4;
                score10 += mHero.SuperCarList[0].SuperCarType5 * TextTranslator.instance.GetLabsPointByID(10).RankPoint * 5;
                score10 += mHero.SuperCarList[0].SuperCarType6 * TextTranslator.instance.GetLabsPointByID(10).RankPoint * 6;
            }
        }

        int score11 = 0;
        if (mHero.WeaponList[0].WeaponClass > 0)
        {
            score11 = mHero.WeaponList[0].WeaponClass * TextTranslator.instance.GetLabsPointByID(12).RankPoint + (((mHero.WeaponList[0].WeaponClass - 1) * 5) + mHero.WeaponList[0].WeaponStar) * TextTranslator.instance.GetLabsPointByID(11).RankPoint;
        }
        

        //Debug.LogError(score1 + " " + score2 + " " + score3 + " " + score4 + " " + score5 + " " + score6 + " " + score7 + " " + score8 + " " + score9);
        return score1 + score2 + score3 + score4 + score5 + score6 + score7 + score8 + score9 + score10 + score11;
    }
    public void SetLegionTrainingSelectItem(Hero mHero)
    {
        this.mHero = mHero;
        HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(mHero.cardID);
        _heroInfo = hinfo;
        LabelName.text = hinfo.heroName;
        LabelName.color = Utils.GetColorByGrade(mHero.classNumber);
        Debug.LogError(Utils.GetColorByGrade(mHero.classNumber).r + "," + Utils.GetColorByGrade(mHero.classNumber).g + "," + Utils.GetColorByGrade(mHero.classNumber).b);
        levelLabel.text = mHero.level.ToString();
        SpriteAvatar.spriteName = mHero.cardID.ToString();

        mySlider.value = (float)mHero.exp / (float)mHero.maxExp;
        sliderLabel.text = string.Format("{0}/{1}", mHero.exp, mHero.maxExp);

        SetRarityIcon();
        SetHeroType(hinfo.heroCarrerType);

        DestroyGride();
        int addNum = TextTranslator.instance.SetHeroNameColor(frame, pinJieSprite, mHero.classNumber);
        for (int i = 0; i < addNum; i++)
        {
            GameObject obj = NGUITools.AddChild(gride, pinJieSprite.gameObject);
            obj.SetActive(true);
        }
        UIGrid _UIGrid = gride.GetComponent<UIGrid>();
        _UIGrid.sorting = UIGrid.Sorting.Horizontal;
        _UIGrid.pivot = UIWidget.Pivot.Center;
        _UIGrid.animateSmoothly = true;
    }
    void DestroyGride()
    {
        for (int i = gride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(gride.transform.GetChild(i).gameObject);
        }
    }
    //设置稀有度
    void SetRarityIcon()
    {
        // Debug.LogError(_heroInfo.heroRarity);
        switch (_heroInfo.heroRarity)
        {
            case 1:
                RarityIcon.spriteName = "word4";
                break;
            case 2:
                RarityIcon.spriteName = "word5";
                break;
            case 3:
                RarityIcon.spriteName = "word6";
                break;
            case 4:
                RarityIcon.spriteName = "word7";
                break;
            case 5:
                RarityIcon.spriteName = "word8";
                break;
            case 6:
                RarityIcon.spriteName = "word9";
                break;
            default:
                break;
        }
    }
    //设置英雄职业
    void SetHeroType(int _herotype)
    {
        if (_herotype == 1 || _herotype == 2 || _herotype == 3)
        {
            heroTypeIcon.spriteName = string.Format("heroType{0}", _herotype);
        }
        else
        {
            Debug.LogError("英雄职业类型参数错误" + _herotype);
        }
    }
}
