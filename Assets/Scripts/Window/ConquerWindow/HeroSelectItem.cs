using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HeroSelectItem : MonoBehaviour
{

    public GameObject trainButton;
    public GameObject Grid;

    public GameObject DeatilButton;
    public GameObject GetButton;
    public UILabel PowerLabel;
    public UILabel AddBuff;
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

    public int Tabe = 0; //1是征服
    public bool isFirstTime = false;
    public GameObject UpOnLabel;//已上阵
    // Use this for initialization
    void Start()
    {
        HarvestWindow Harvest = GameObject.Find("HarvestWindow").GetComponent<HarvestWindow>();
        if (UIEventListener.Get(trainButton).onClick == null)
        {
            UIEventListener.Get(trainButton).onClick = delegate(GameObject go)
            {
                if (Tabe == 1)
                {
                    if (isFirstTime)
                    {
                        UIManager.instance.OpenPromptWindow("已经上阵", PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        if (CharacterRecorder.instance.GuideID[58] == 7)
                        {
                            SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                        }
                        CharacterRecorder.instance.KengID = Harvest.KengID;
                        NetworkHandler.instance.SendProcess(string.Format("6504#{0};{1};{2};", CharacterRecorder.instance.TabeID, Harvest.KengID, this.mHero.characterRoleID));
                    }
                }
                //NetworkHandler.instance.SendProcess(string.Format("8019#{0};{1};", TrainingGroundItem.curClickTrainingGroundItemId, this.mHero.characterRoleID));
                DestroyImmediate(GameObject.Find("CheckSelfHeroWindow"));
            };
        }

    }
    public void SetHeroSelectItem(Hero mHero, int _Tabe)
    {
        mySlider.gameObject.SetActive(false);
        trainButton.SetActive(false);
        DeatilButton.SetActive(false);
        GetButton.SetActive(false);
        Tabe = _Tabe;
        this.mHero = mHero;
        HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(mHero.cardID);
        _heroInfo = hinfo;
        LabelName.text = hinfo.heroName;
        levelLabel.text = mHero.level.ToString();
        SpriteAvatar.spriteName = mHero.cardID.ToString();
        PowerLabel.text = mHero.force.ToString();
        AddBuff.text = (10 + 2 * (mHero.force / 10000)) + "%";
        mySlider.value = (float)mHero.exp / (float)mHero.maxExp;
        sliderLabel.text = string.Format("{0}/{1}", mHero.exp, mHero.maxExp);
        if (_Tabe == 1)
        {
            trainButton.SetActive(true);
            UpOnLabel.SetActive(false);
            List<int> HeroId = GameObject.Find("ConquerWindow").GetComponent<ConquerWindow>().HeroSelfList;
            foreach (int Hid in HeroId)
            {
                if (mHero.characterRoleID == Hid)
                {

                    isFirstTime = true;
                    trainButton.SetActive(false);
                    UpOnLabel.SetActive(true);
                    trainButton.transform.Find("Label").GetComponent<UILabel>().text = "已上阵";
                    break;
                }
                else
                {
                    isFirstTime = false;
                }
            }
            if (isFirstTime == false)
            {
                trainButton.transform.Find("Label").GetComponent<UILabel>().text = "征 战";
            }
        }
        else
        {
            trainButton.SetActive(true);
            mySlider.gameObject.SetActive(true);
            trainButton.transform.Find("Label").GetComponent<UILabel>().text = "训 练";
        }
        SetRarityIcon();
        SetHeroType(hinfo.heroCarrerType);

        DestroyGrid();
        int addNum = TextTranslator.instance.SetHeroNameColor(frame, pinJieSprite, mHero.classNumber);
        for (int i = 0; i < addNum; i++)
        {
            GameObject obj = NGUITools.AddChild(Grid, pinJieSprite.gameObject);
            obj.SetActive(true);
        }
        UIGrid _UIGrid = Grid.GetComponent<UIGrid>();
        _UIGrid.sorting = UIGrid.Sorting.Horizontal;
        _UIGrid.pivot = UIWidget.Pivot.Center;
        _UIGrid.animateSmoothly = true;
    }
    void DestroyGrid()
    {
        for (int i = Grid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(Grid.transform.GetChild(i).gameObject);
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
