using UnityEngine;
using System.Collections;

public class SuperCarItem : MonoBehaviour
{

    public GameObject FramgeSlider;
    public GameObject CarIcon;
    public GameObject SynthesisButton;
    public UILabel Name;
    public UILabel One;
    public UILabel Two;
    public UILabel Three;
    public UILabel Four;
    public GameObject SuccessLabel;
    public bool isSuccessSyn;
    public bool GoToTower = false;
    public int NeedLevel;
    public int HeroID;
    public int HeroCarID;
    public GameObject ItemGrade;
    public GameObject SynthesisEffect;
    // Use this for initialization
    void Start()
    {
        SynthesisEffect.SetActive(false);
        UIEventListener.Get(SynthesisButton).onClick += delegate(GameObject go)
        {

            if (CharacterRecorder.instance.level >= NeedLevel)
            {
                if (GoToTower)
                {
                    PictureCreater.instance.DestroyAllComponent();//先删除用kino方法克隆出现的方式
                    NetworkHandler.instance.SendProcess("1501#;");
                }
                else
                {
                    if (isSuccessSyn)
                    {
                        AudioEditer.instance.PlayOneShot("ui_levelup");
                        NetworkHandler.instance.SendProcess("3202#" + HeroID.ToString() + ";" + HeroCarID + ";");
                        SynthesisEffect.SetActive(true);
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("碎片不足", PromptWindow.PromptType.Hint, null, null);
                    }
                }
            }
            else
            {
                UIManager.instance.OpenPromptWindow(NeedLevel.ToString() + "级开启", PromptWindow.PromptType.Hint, null, null);
            }

        };
    }
    public void SetInfo(int CarID, int _isSuccess, int _HeroID, int i)
    {
        HeroID = _HeroID;
        HeroCarID = i + 1;
        CarInfo(CarID, _isSuccess);
    }
    public void CarInfo(int CarID, int isSuccess)
    {
        TextTranslator.instance.ItemDescription(ItemGrade.gameObject, CarID, 0);
        SuperCar CarItem = TextTranslator.instance.GetSuperCarByID(CarID);
        ItemGrade.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(CarID).itemGrade;
        ItemGrade.transform.Find("Icon").GetComponent<UISprite>().spriteName = CarID.ToString();
        CarIcon.GetComponent<UITexture>().mainTexture = Resources.Load("Game/" + CarID.ToString()) as Texture;
        SynthesisEffect.GetComponent<Renderer>().material.mainTexture = Resources.Load("Game/" + CarID.ToString()) as Texture;
        NeedLevel = CarItem.NeedLevel;
        if (CarItem == null)
        {
            return;
        }
        if (isSuccess == 1)
        {
            ItemGrade.SetActive(false);
            SynthesisButton.SetActive(false);
            FramgeSlider.SetActive(false);
            SuccessLabel.SetActive(true);
            Name.text = ItemNameColor(CarID) + CarItem.Name;
            One.text = LabelColor(3) + "速度+" + CarItem.Speed;
            Two.text = LabelColor(3) + "血量+" + CarItem.Hp;
            Three.text = LabelColor(3) + "攻击+" + CarItem.Atk;
            Four.text = LabelColor(3) + "防御+" + CarItem.Def;
            CarIcon.GetComponent<UITexture>().color = new Color(1, 1, 1);
        }
        else
        {
            Name.text = ItemNameColor(1) + CarItem.Name;
            SynthesisButton.SetActive(true);
            FramgeSlider.SetActive(true);
            SuccessLabel.SetActive(false);
            float IconNum = TextTranslator.instance.GetItemCountByID(CarID);
            CarIcon.GetComponent<UITexture>().color = new Color(0.2f, 0.2f, 0.2f);

            if (IconNum >= CarItem.NeedDebris)
            {
                isSuccessSyn = true;
                FramgeSlider.GetComponent<UISlider>().value = 1;
                FramgeSlider.transform.Find("Label").GetComponent<UILabel>().text = LabelColor(1) + IconNum.ToString() + "[-]/" + CarItem.NeedDebris;            
                if (CharacterRecorder.instance.level >= CarItem.NeedLevel)
                {
                    SynthesisButton.transform.Find("Label").GetComponent<UILabel>().text = "合 成";
                    SynthesisButton.transform.Find("Red").gameObject.SetActive(true);
                    SynthesisButton.GetComponent<UISprite>().color = Color.white;
                    SynthesisButton.transform.Find("Label").GetComponent<UILabel>().color = Color.white;
                }
                else
                {
                    SynthesisButton.transform.Find("Label").GetComponent<UILabel>().text = string.Format("需要{0}级", CarItem.NeedLevel);
                    SynthesisButton.GetComponent<UISprite>().color = Color.gray;
                    SynthesisButton.transform.Find("Label").GetComponent<UILabel>().color = Color.gray;
                    SynthesisButton.transform.Find("Red").gameObject.SetActive(false);
                }
            }
            else
            {
                SuccessLabel.SetActive(false);
                isSuccessSyn = false;
                FramgeSlider.GetComponent<UISlider>().value = IconNum / CarItem.NeedDebris;
                FramgeSlider.transform.Find("Label").GetComponent<UILabel>().text = LabelColor(2) + IconNum.ToString() + "[-]/" + CarItem.NeedDebris;
                GoToTower = true;
                SynthesisButton.transform.Find("Label").GetComponent<UILabel>().text = "收 集";
                SynthesisButton.transform.Find("Red").gameObject.SetActive(false);
            }
            One.text = LabelColor(4) + "速度+" + CarItem.Speed;
            Two.text = LabelColor(4) + "血量+" + CarItem.Hp;
            Three.text = LabelColor(4) + "攻击+" + CarItem.Atk;
            Four.text = LabelColor(4) + "防御+" + CarItem.Def;

        }
    }
    //item字体颜色
    public string ItemNameColor(int GradeID)
    {
        string NameColor = "";
        switch (GradeID)
        {
            case 42001:
                NameColor = "[-][FFFFFF]";
                break;
            case 42002:
                NameColor = "[-][28DF5E]";
                break;
            case 42003:
                NameColor = "[-][12A7B8]";
                break;
            case 42004:
                NameColor = "[-][842DCE]";
                break;
            case 42005:
                NameColor = "[-][DC582D]";
                break;
            case 42006:
                NameColor = "[-][D9181E]";
                break;
            default:
                NameColor = "[-][B3B3B3]";
                break;

        }
        return NameColor;
    }
    //绿色和红色
    public string LabelColor(int GradeID)
    {
        string LabelColor = "";
        switch (GradeID)
        {
            case 1:
                LabelColor = "[-][28DF5E]";//绿色
                break;
            case 2:
                LabelColor = "[-][FB2D50]";//红色
                break;
            case 3:
                LabelColor = "[-][FFFFFF]";//白色
                break;
            case 4:
                LabelColor = "[-][969696]";//灰色
                break;
        }
        return LabelColor;
    }

}
