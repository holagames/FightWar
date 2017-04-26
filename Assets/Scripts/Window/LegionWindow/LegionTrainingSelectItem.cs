using UnityEngine;
using System.Collections;

public class LegionTrainingSelectItem: MonoBehaviour
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

    private Hero mHero;
    private HeroInfo _heroInfo;
	// Use this for initialization
	void Start () 
    {

        if (UIEventListener.Get(trainButton).onClick == null)
        {
            UIEventListener.Get(trainButton).onClick = delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess(string.Format("8019#{0};{1};", TrainingGroundItem.curClickTrainingGroundItemId, this.mHero.characterRoleID));
                UIManager.instance.BackUI();
            };
        }
      
	}
    public void SetLegionTrainingSelectItem(Hero mHero)
    {
        this.mHero = mHero;
        HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(mHero.cardID);
        _heroInfo = hinfo;
        //LabelName.text = hinfo.heroName;
        TextTranslator.instance.SetHeroNameColor(LabelName, hinfo.heroName, mHero.classNumber);
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
