using UnityEngine;
using System.Collections;

public class HeroMapItem : MonoBehaviour
{
    public UITexture cardItemTexture;//角色边框
    public UITexture cardItemBgTexture;//角色背景图片
    public UITexture heroSprite;//角色图片
    public UISprite rareSprite;//角色图片类型
    public UISprite heroType;//角色类型
    public UILabel labelName;//角色名称
    public UISlider heroHPSlider;
    public UILabel heroHPLabel;
    public UILabel labelFocre;//角色战力

    public GameObject heroBtn;
    public UILabel heroBtnLabel;
    public GameObject redPoint;

    int SuiPianMax = 0;
    int heroID = 0;
    /// <summary>
    /// 设置图片卡片的信息
    /// </summary>
    /// <param name="heroInfo">角色信息</param>
    /// <param name="isOwn">是否拥有</param>
    public void SetHeroMapInfo(HeroInfo heroInfo, bool isOwn)
    {
        heroID = heroInfo.heroID;
        Hero hero = CharacterRecorder.instance.GetHeroByRoleID(heroInfo.heroID);
        //角色图片
        heroSprite.mainTexture = Resources.Load("Loading/" + heroInfo.heroID, typeof(Texture)) as Texture;
        heroSprite.MakePixelPerfect();
        heroSprite.transform.localScale = new Vector3(1, 1, 1) * 0.5f * 0.85f;
        //设置稀有度
        SetRarityIcon(heroInfo.heroRarity);
        //设置角色的边框背景
        SetFrame(heroInfo.heroRarity);
        //角色类型
        heroType.GetComponent<UISprite>().spriteName = "heroType" + heroInfo.heroCarrerType;
        //角色名称
        labelName.text = heroInfo.heroName;
        //计算heroType的本地坐标
        float typeX = ((55.0f + labelName.localSize.x) / 4.0f + 6.0f) * -1.0f;
        heroType.transform.localPosition = new Vector3(typeX, -125.0f, 0);


        //角色碎片
        SuiPianMax = heroInfo.heroPiece;
        UpdateNotHaveItemInfo(heroInfo.heroID);
        //角色战力
        if (isOwn)
        {
            labelFocre.text = hero.force.ToString();
            TextTranslator.instance.SetHeroNameColor(labelName, heroInfo.heroName, hero.classNumber);
            //设置按钮            
            heroBtnLabel.text = "获 取";
            heroBtn.GetComponent<UISprite>().spriteName = "button1";
            heroBtn.GetComponent<UIButton>().hoverSprite = "button1";
            heroBtn.GetComponent<UIButton>().pressedSprite = "button1_an";
            redPoint.SetActive(false);
        }
        else
        {
            labelFocre.transform.parent.gameObject.SetActive(false);
            heroSprite.shader = Shader.Find("Unlit/GrayShader");
            //labelFocre.text = "???";
            //设置按钮
            if (TextTranslator.instance.GetItemCountByID(heroInfo.heroID + 10000) >= SuiPianMax)
            {
                heroBtnLabel.text = "合 成";
                heroBtn.GetComponent<UISprite>().spriteName = "ui2_button4";
                heroBtn.GetComponent<UIButton>().hoverSprite = "ui2_button4";
                heroBtn.GetComponent<UIButton>().pressedSprite = "ui2_button4_an";
                redPoint.SetActive(true);
            }
            else
            {
                heroBtnLabel.text = "获 取";
                heroBtn.GetComponent<UISprite>().spriteName = "button1";
                heroBtn.GetComponent<UIButton>().hoverSprite = "button1";
                heroBtn.GetComponent<UIButton>().pressedSprite = "button1_an";
                redPoint.SetActive(false);
            }
        }
        //设置图鉴角色点击事件初始化
        InitBtnOnClick(heroInfo.heroID);
    }
    /// <summary>
    /// 设置图鉴角色点击事件初始化
    /// </summary>
    /// <param name="roleID"></param>
    public void InitBtnOnClick(int roleID)
    {
        //角色详情点击事件
        if (UIEventListener.Get(this.gameObject).onClick == null)
        {
            UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
            {
                CharacterRecorder.instance.CompositeSuipian = 0;
                UIManager.instance.OpenSinglePanel("CardDetailInfoWindow", false);
                GameObject.Find("CardDetailInfoWindow").GetComponent<CardDetailInfoWindow>().SetDetaiInfo(heroID);
            };
        }
        //按钮的点击事件
        if (UIEventListener.Get(heroBtn).onClick == null)
        {
            UIEventListener.Get(heroBtn).onClick += delegate(GameObject go)
            {
                if (heroBtnLabel.text == "详 情")
                {
                    UIManager.instance.OpenSinglePanel("CardDetailInfoWindow", false);
                    GameObject.Find("CardDetailInfoWindow").GetComponent<CardDetailInfoWindow>().SetDetaiInfo(roleID);
                }
                else if (heroBtnLabel.text == "获 取")
                {
                    UIManager.instance.OpenSinglePanel("WayWindow", false);
                    WayWindow.NeedItemCount = SuiPianMax;
                    GameObject.Find("WayWindow").GetComponent<WayWindow>().SetWayInfo(roleID + 10000);
                }
                else if (heroBtnLabel.text == "合 成")
                {
                    // CharacterRecorder.instance.CompositeSuipian = 0;
                    NetworkHandler.instance.SendProcess("1021#" + (roleID + 10000) + ";");
                }
            };
        }
    }
    /// <summary>
    /// 显示角色的碎片
    /// </summary>
    /// <param name="_RoleID">角色的ID号</param>
    public void UpdateNotHaveItemInfo(int _RoleID)
    {
        heroHPSlider.GetComponent<UISlider>().value = (float)TextTranslator.instance.GetItemCountByID(_RoleID + 10000) / SuiPianMax;
        //IsHavedBG.spriteName = "di3";
        //Hero myHero = CharacterRecorder.instance.GetHeroByRoleID(_RoleID);
        heroHPLabel.text = TextTranslator.instance.GetItemCountByID(_RoleID + 10000).ToString() + "/" + SuiPianMax.ToString();
    }
    /// <summary>
    ///  设置稀有度
    /// </summary>
    /// <param name="_heroRarity"></param>
    void SetRarityIcon(int _heroRarity)
    {
        switch (_heroRarity)
        {
            case 1:
                rareSprite.spriteName = "word4";
                break;
            case 2:
                rareSprite.spriteName = "word5";
                break;
            case 3:
                rareSprite.spriteName = "word6";
                break;
            case 4:
                rareSprite.spriteName = "word7";
                break;
            case 5:
                rareSprite.spriteName = "word8";
                break;
            case 6:
                rareSprite.spriteName = "word9";
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// 设置边框
    /// </summary>
    /// <param name="_heroRarity"></param>
    void SetFrame(int _heroRarity)
    {
        string bgName = null;
        string frameName = null;
        switch (_heroRarity)
        {
            case 1:
                bgName = "carddi0";
                frameName = "kuang0";
                break;
            case 2:
                bgName = "carddi1";
                frameName = "kuang1";
                break;
            case 3:
                bgName = "carddi2";
                frameName = "kuang2";
                break;
            case 4:
                bgName = "carddi3";
                frameName = "kuang3";
                break;
            case 5:
                bgName = "carddi4";
                frameName = "kuang4";
                break;
            case 6:
                bgName = "carddi5";
                frameName = "kuang5";
                break;
        }
        cardItemTexture.mainTexture = Resources.Load("Game/" + frameName) as Texture;
        cardItemBgTexture.mainTexture = Resources.Load("Game/" + bgName) as Texture;
    }
}
