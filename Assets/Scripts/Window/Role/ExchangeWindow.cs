using UnityEngine;
using System.Collections;

public class ExchangeWindow : MonoBehaviour
{
    public GameObject SureButten;
    public GameObject Closebutten;
    public GameObject One;
    public GameObject Two;
    public GameObject Three;
    public GameObject DowerN;
    int num;
    int Num2;
    int Num3;
    // Use this for initialization
    void Start()
    {
        UIEventListener.Get(SureButten).onClick += delegate (GameObject go)
        {
            if (CharacterRecorder.instance.DowerNum < CharacterRecorder.instance.level)
            {
                NetworkHandler.instance.SendProcess("1624#" + HeroDowerPart.instance.hero.characterRoleID + ";");
            }
            else
            {
                UIManager.instance.OpenPromptWindow("长官，请先提升您的等级！", PromptWindow.PromptType.Hint, null, null);
            }

        };
        UIEventListener.Get(Closebutten).onClick += delegate (GameObject go)
        {
            DestroyImmediate(this.gameObject);
        };
        SetWindow();

        // TextTranslator.instance.ItemDescription(One, 10108, 0);
        // TextTranslator.instance.ItemDescription(Two, HeroDowerPart.instance.hero.characterRoleID + 10000, 0);
        //TextTranslator.instance.ItemDescription(Three, Num3, 0);

    }

    void OnEnable()
    {
        num = 0;
    }
    public void SetWindow()
    {
        HeroDowerPart.instance.dsdsdsdsd();
        // print(TextTranslator.instance.GetNowNum(HeroDowerPart.instance.hero.characterRoleID)+"+" + HeroDowerPart.instance.GetNowNum());
        int NowNum = HeroDowerPart.instance.GetNowNum();
        num = NowNum + 1;//下一级的天赋
                         //   print(NowNum / (float)CharacterRecorder.instance.level+"  ds");

        if (num > 100)
            num = 100;
        TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(HeroDowerPart.instance.hero.cardID + 10000);
       // print(HeroDowerPart.instance.hero.characterRoleID + "  " + mItemInfo.itemCode + " " + (100 / 250));
        Item mItem = TextTranslator.instance.GetItemByID(mItemInfo.itemCode);//HeroDowerPart.instance.hero.characterRoleID+10000);

        DowerN.GetComponent<UISlider>().value = NowNum / (float)CharacterRecorder.instance.level; //CharacterRecorder.instance.DowerNum / 100;
        DowerN.transform.FindChild("Label").GetComponent<UILabel>().text = NowNum + "/" + CharacterRecorder.instance.level;
        if (mItem == null)
            Num2 = 0;
        else
            Num2 = mItem.itemCount;

        int mItem1 = TextTranslator.instance.GetItemCountByID(10108);
        One.transform.FindChild("GiftNum").GetComponent<UILabel>().text = ((mItem1 >= TextTranslator.instance.GetInnaByTalent(num).ResourcesNum1) ? "[FFFFFFFF]" : "[FB3535FF]") + mItem1 + "[-]/" + TextTranslator.instance.GetInnaByTalent(num).ResourcesNum1.ToString();
        Two.transform.FindChild("Sprite").GetComponent<UISprite>().spriteName = HeroDowerPart.instance.hero.cardID.ToString();

        SetColor(Two, mItemInfo.itemGrade);
        Two.transform.FindChild("HeroNum").GetComponent<UILabel>().text = ((TextTranslator.instance.GetInnaByTalent(num).ConsumeHeroesFragment <= Num2) ? "[FFFFFFFF]" : "[FB3535FF]") + Num2 + "[-]/" + TextTranslator.instance.GetInnaByTalent(num).ConsumeHeroesFragment;
        //Three.transform.FindChild("DowerNum").GetComponent<UILabel>().text = "1";

    }
    void SetColor(GameObject go, int color)
    {
        print(go.name + color);
        switch (color)
        {
            case 1:
                go.GetComponent<UISprite>().spriteName = "Grade1";
                break;
            case 2:
                go.GetComponent<UISprite>().spriteName = "Grade2";
                break;
            case 3:
                go.GetComponent<UISprite>().spriteName = "Grade3";
                break;
            case 4:
                go.GetComponent<UISprite>().spriteName = "Grade4";
                break;
            case 5:
                go.GetComponent<UISprite>().spriteName = "Grade5";
                break;
            case 6:
                go.GetComponent<UISprite>().spriteName = "Grade6";
                break;
            case 7:
                go.GetComponent<UISprite>().spriteName = "Grade7";
                break;
        }
    }
}
