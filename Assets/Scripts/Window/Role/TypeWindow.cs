using UnityEngine;
using System.Collections;

public class TypeWindow : MonoBehaviour
{
    public UISprite ItemSprite;
    public UILabel NoeLable;
    public UILabel NextLable;
    public UILabel ItemNum;
    public UILabel MoneyNum;
    public UILabel DowerNum;
    public UILabel TypeName;
    public UILabel Name;
    public GameObject All;
    public GameObject Text;
    public GameObject SureButten;
    public GameObject CloseButten;
    GameObject fo;
    int type;
    int Num = 0;
    int num1;
    // Use this for initialization
    void Start()
    {

        UIEventListener.Get(SureButten).onClick += delegate (GameObject go)
        {
            if (num1 >= 10)
            {
                UIManager.instance.OpenPromptWindow("此天赋点已满！", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {

                //if (CharacterRecorder.instance.DowerNum <= 0)
                //{
                //    UIManager.instance.OpenPromptWindow("天赋点不足！", PromptWindow.PromptType.Hint, null, null);
                //}
                //else
                //{ 
               
                    NetworkHandler.instance.SendProcess("1622#" + HeroDowerPart.instance.hero .characterRoleID+ ";" + int.Parse(fo.name)+";"+(Num+1 )+";");
             //   }

            }

        };
        UIEventListener.Get(CloseButten).onClick += delegate (GameObject go)
        {
            UIManager.instance.BackUI();
        };
    }
    void OnEnable()
    {
      num1 = 0; 
    }
    public void NotWidow(GameObject go)
    {
        All.transform.FindChild("Dower").gameObject.SetActive(false);
        All.transform.FindChild("Glod").gameObject.SetActive(false);
        All.transform.FindChild("SureButten").gameObject.SetActive(false);
        All.transform.FindChild("TextLable").gameObject.SetActive(true);
        ItemNum.text = 0 + "/10";
        NoeLable.gameObject.SetActive(false);
        NextLable.text = TextTranslator.instance.GetInnatesByTwo(int.Parse(go.name), 1).Des;
        if ((int.Parse(go.name)<4)|| (7<int.Parse(go.name) && int.Parse(go.name) < 10)|| (13 < int.Parse(go.name) && int.Parse(go.name) < 16))
        {
            All.transform.FindChild("TextLable").GetComponent<UILabel>().text = "上一层天赋应升至5级";
        }
        else
        {
            All.transform.FindChild("TextLable").GetComponent<UILabel>().text = "上一层天赋应升至8级";
        }
    }

    public void SetWindow(GameObject go,int num)
    {
        fo = go;
        
           /// num = int.Parse(fo.transform.FindChild("Num").GetComponent<UILabel>().text);
       

        type = HeroDowerPart.instance.Type;
        if (HeroDowerPart.instance.Type == 1)
        {
            TypeName.text = "攻击";
        }
        else if (HeroDowerPart.instance.Type == 2)
        {
            TypeName.text = "生存";
        }
        else if (HeroDowerPart.instance.Type == 3)
        {
            TypeName.text = "战意";
        }



        ItemNum.text = (num ).ToString()+"/10";
        Num = num;
        ItemSprite.spriteName = go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName;

        if (num < 10)
        {
            if (num == 0)
            {
                Name.text = TextTranslator.instance.GetInnatesByTwo(int.Parse(go.name), num + 1).Name;
                NoeLable.gameObject.SetActive(false);
            }
            else
            {
                NoeLable.gameObject.SetActive(false);
                Name.text = TextTranslator.instance.GetInnatesByTwo(int.Parse(go.name), num).Name;
                NoeLable.text = TextTranslator.instance.GetInnatesByTwo(int.Parse(go.name), num).Des;
                NoeLable.gameObject.SetActive(true);
            }
            DowerNum.text = ((TextTranslator.instance.GetInnatesByTwo(int.Parse(go.name), num + 1).TalentCost <= HeroDowerPart.instance.HeroUse) ? "[FFFFFFFF]" : "[FB3535FF]") + HeroDowerPart.instance.HeroUse + "[-]/" + TextTranslator.instance.GetInnatesByTwo(int.Parse(go.name), num + 1).TalentCost;
            NextLable.text = TextTranslator.instance.GetInnatesByTwo(int.Parse(go.name), num + 1).Des;
            MoneyNum.text = TextTranslator.instance.GetInnatesByTwo(int.Parse(go.name), num + 1).CostValue.ToString();
            Text.SetActive(false);
            All.SetActive(true);
        }
        else
        {
            NoeLable.text = TextTranslator.instance.GetInnatesByTwo(int.Parse(go.name), num).Des;
            NoeLable.gameObject.SetActive(true);
            Name.text = TextTranslator.instance.GetInnatesByTwo(int.Parse(go.name), num).Name;
            All.SetActive(false);
            Text.SetActive(true);
           
        }
        if (num!=0)
        {
            CharacterRecorder.instance.gold -= TextTranslator.instance.GetInnatesByTwo(int.Parse(go.name), num).CostValue;
        }
        
    }
    public void AddDower()
    {
        

        if (Num < 10)
        {

            HeroDowerPart.instance.dsdsdsdsd();
            Num = Num + 1;
            num1 = Num;
          
            SetWindow(fo, num1);
           
        }

    }

}
