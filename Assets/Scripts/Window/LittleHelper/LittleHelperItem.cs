using UnityEngine;
using System.Collections;

public class LittleHelperItem : MonoBehaviour
{
    public UIAtlas itemAtlas;
    public UIAtlas AvatarAtlas;
    public UISprite icon;
    public UILabel nameLabel;
    public UILabel desLabel;
    public GameObject goButton;

    public UISlider slider;

    public GameObject selectSprete;

    private HelperItemData _MyHelperItemData;

    private bool isSelectItem = false;
    public bool ThisIsSelectItem
    {
        get
        {
            return isSelectItem;
        }
    }
    // Use this for initialization
    void Start()
    {
        UIEventListener.Get(goButton).onClick += delegate(GameObject go)
        {
            ClickGoButton();
        };
    }
    public void SetLittleHelperItem(HelperItemData _HelperItemData)
    {
        _MyHelperItemData = _HelperItemData;
        if (_HelperItemData.Icon == 4028 || _HelperItemData.Icon == 4032)
        {
            if (icon.gameObject.GetComponent<UISprite>() != null)
            {
                icon.gameObject.GetComponent<UISprite>().enabled = false;
            }
            if (icon.gameObject.GetComponent<UITexture>() == null)
            {
                icon.gameObject.AddComponent<UITexture>();
            }
            icon.gameObject.GetComponent<UITexture>().depth = 10;

            icon.gameObject.GetComponent<UITexture>().mainTexture = Resources.Load("NewGuide/" + _HelperItemData.Icon) as Texture;
        }
        else
        {
            if (icon.gameObject.GetComponent<UITexture>() != null)
            {
                icon.gameObject.GetComponent<UITexture>().enabled = false;
            }
            if (icon.gameObject.GetComponent<UISprite>() == null)
            {
                icon.gameObject.AddComponent<UISprite>();
            }
            if (_HelperItemData.Icon.ToString()[0] == '6')
            {
                icon.atlas = AvatarAtlas;
            }
            else
            {
                icon.atlas = itemAtlas;
            }

            icon.spriteName = _HelperItemData.Icon.ToString();
        }
        slider.value = _HelperItemData.SliderCount;
        nameLabel.text = _HelperItemData.Name;
        //desLabel.color = Color.white;
        //desLabel.text = "[3ee817]" + _HelperItemData.Des;//旧的 绿色 
        desLabel.text = _HelperItemData.Des;//新的 蓝色
        goButton.transform.FindChild("BtnWord").GetComponent<UILabel>().text = SetBtnName(_HelperItemData.Icon);
    }
    string SetBtnName(int iconId)
    {
        string name = "";
        switch (iconId)
        {
            case 10003://英雄升级
                name = "升级";
                break;
            case 30000://英雄升品
                name = "升品";
                break;
            case 31005://装备强化  
            case 51030://饰品强化 -- 饰品晋级   
                if (nameLabel.text == "饰品晋级")
                {
                    name = "晋级";
                }
                else
                {
                    name = "强化";
                }
                break;
            case 10500://英雄培养
                name = "培养";
                break;
            case 10012://装备精炼         
            case 10103://饰品精炼
                name = "精炼";
                break;
            case 4028://座驾
                name = "收集";
                break;
            case 10102://英雄升星
            case 4032://核武器
                name = "升星";
                break;
            case 10101://技能
            case 40103://秘宝
            case 10104://情报              
            case 50212://改造实验室
            case 60016://羁绊
                name = "提升";
                break;
            default:
                name = "前往";
                break;
        }
        return name;
    }
    void ClickGoButton()
    {
        switch (_MyHelperItemData.Icon)
        {
            case 10003://英雄升级
                //CharacterRecorder.instance.enterRoleFromMain = false;
                CharacterRecorder.instance.RoleTabIndex = 2;
                UIManager.instance.OpenPanel("RoleWindow", true);
                //GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(2);
                break;
            case 31005://装备强化
                //StrengEquipWindow.strengType = 0;
                CharacterRecorder.instance.isNeedRecordStrengTabType = false;
                StrengEquipWindow.IsEnterEquipUIFromGrabGoods = false;
                StrengEquipWindow.ClickIndex = 1;
                UIManager.instance.OpenPanel("StrengEquipWindow", true);
                break;
            case 30000://英雄升品
                CharacterRecorder.instance.RoleTabIndex = 4;
                UIManager.instance.OpenPanel("RoleWindow", true);
                //GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(4);
                break;
            case 10500://英雄培养
                if (CharacterRecorder.instance.lastGateID > 10030)
                {
                    CharacterRecorder.instance.RoleTabIndex = 3;
                    UIManager.instance.OpenPanel("RoleWindow", true);
                    //GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(3);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow(string.Format("{0}关开放培养", 30), 11, false, PromptWindow.PromptType.Hint, null, null);
                }
                break;

            case 10101://英雄技能
                if (CharacterRecorder.instance.lastGateID <= 10078)
                {
                    UIManager.instance.OpenPromptWindow(string.Format("{0}关开放技能突破", 78), 11, false, PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    CharacterRecorder.instance.RoleTabIndex = 6;
                    UIManager.instance.OpenPanel("RoleWindow", true);
                    //GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(6);
                }
                break;
            case 51030://饰品晋级 -- 饰品强化
                if (nameLabel.text == "饰品强化")
                {
                    CharacterRecorder.instance.isNeedRecordStrengTabType = false;
                    StrengEquipWindow.IsEnterEquipUIFromGrabGoods = false;
                    StrengEquipWindow.ClickIndex = 1;
                    UIManager.instance.OpenPanel("StrengEquipWindow", true);
                }
                //饰品晋级 
                else
                {
                    if (CharacterRecorder.instance.lastGateID > 10022)
                    {
                        CharacterRecorder.instance.isNeedRecordStrengTabType = true;
                        StrengEquipWindow.strengType = 2;
                        UIManager.instance.OpenPanel("StrengEquipWindow", true);
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow(string.Format("{0}关开放饰品晋级", 22), PromptWindow.PromptType.Hint, null, null);
                    }
                }
                break;
            case 10012://装备精炼
                if (CharacterRecorder.instance.lastGateID <= 10082)// && CharacterRecorder.instance.level < 31)
                {
                    UIManager.instance.OpenPromptWindow(string.Format("{0}关开放装备精炼", 82), PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    CharacterRecorder.instance.isNeedRecordStrengTabType = true;
                    StrengEquipWindow.strengType = 1;
                    StrengEquipWindow.ClickIndex = 1;
                    UIManager.instance.OpenPanel("StrengEquipWindow", true);
                }
                break;
            case 10103: //饰品精炼
                if (CharacterRecorder.instance.lastGateID <= 10053)// && CharacterRecorder.instance.level < 31)
                {
                    UIManager.instance.OpenPromptWindow(string.Format("{0}关开放饰品精炼", 53), PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    CharacterRecorder.instance.isNeedRecordStrengTabType = true;
                    StrengEquipWindow.strengType = 1;
                    StrengEquipWindow.ClickIndex = 1;
                    UIManager.instance.OpenPanel("StrengEquipWindow", true);
                }
                break;
            case 4028: //座驾
                if (CharacterRecorder.instance.lastGateID >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zuojia).Level)
                {
                    UIManager.instance.OpenPanel("StrengEquipWindow", true);
                    CharacterRecorder.instance.setEquipTableIndex = 4;
                }
                else
                {
                    UIManager.instance.OpenPromptWindow(string.Format("通关{0}开放座驾", (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zuojia).Level) % 10000).ToString(), PromptWindow.PromptType.Hint, null, null);
                }
                break;
            case 4032: //核武器
                if (CharacterRecorder.instance.lastGateID >= TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.hewuqi).Level)// && CharacterRecorder.instance.level >= 24 
                {
                    UIManager.instance.OpenPanel("StrengEquipWindow", true);
                    CharacterRecorder.instance.setEquipTableIndex = 5;
                }
                else
                {
                    UIManager.instance.OpenPromptWindow(string.Format("通关{0}开放核武器", (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.hewuqi).Level) % 10000).ToString(), PromptWindow.PromptType.Hint, null, null);
                }
                break;
            case 40103://秘宝
                if (CharacterRecorder.instance.lastGateID > 10048)//CharacterRecorder.instance.level >= 24)
                {
                    CharacterRecorder.instance.isNeedRecordStrengTabType = true;
                    StrengEquipWindow.strengType = 3;
                    UIManager.instance.OpenPanel("StrengEquipWindow", true);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow(string.Format("通关{0}开放秘宝", 48), PromptWindow.PromptType.Hint, null, null);
                }
                break;
            case 10104://情报
                if (CharacterRecorder.instance.lastGateID > 10026)
                {
                    UIManager.instance.OpenPanel("TechWindow", true);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow(string.Format("{0}关开放", 26), PromptWindow.PromptType.Hint, null, null);
                }
                break;
            case 60016://羁绊 招募UI
                UIManager.instance.OpenPanel("GachaWindow", true);
                break;
            case 50212://改造实验室
                if (CharacterRecorder.instance.lastGateID > 10093)
                {
                    UIManager.instance.OpenPanel("LabWindow", true);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow(string.Format("通关{0}开放", 93), PromptWindow.PromptType.Hint, null, null);
                }
                break;
            case 10102://军衔
                CharacterRecorder.instance.RoleTabIndex = 5;
                UIManager.instance.OpenPanel("RoleWindow", true);
                //GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(5);
                break;

        }
    }

    public void SetSelectItem(bool _isSelect)
    {
        selectSprete.SetActive(_isSelect);
        this.isSelectItem = _isSelect;
    }
}
public class HelperItemData
{
    public int Icon { get; set; }
    public string Name { get; set; }
    public string Des { get; set; }
    public float SliderCount { get; set; }
    public HelperItemData(int Icon, string Name, string Des, float SliderCount)
    {
        this.Icon = Icon;
        this.Name = Name;
        this.Des = Des;
        this.SliderCount = SliderCount;
    }
}
