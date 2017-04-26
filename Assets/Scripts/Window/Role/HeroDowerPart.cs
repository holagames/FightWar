using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroDowerPart : MonoBehaviour
{
    public static HeroDowerPart instance;
    public GameObject AttackButten;//gong
    public GameObject ExistButten;//fang
    public GameObject SurgeButten;//zhan
    public GameObject RefreshWindow;
    public GameObject ExchangeWindow;
    public GameObject TypeWindow;
    public GameObject HelpButten;
    public GameObject RefeButten;//shua
    public GameObject AddDowerButten;
    public GameObject One;
    public GameObject Two;
    public GameObject Three;
    public GameObject All;
    public int Type = 1;//天赋类型

    public UILabel Money;
    public UILabel LableNum;
    public  Hero hero;
    /// <summary>
    /// 剩余的天赋点
    /// </summary>
    public int HeroUse=0;
    public Innates HeroInnate;
   
    // Use this for initialization
    void Start()
    {

        instance = this;
        OpenOne(1);
        UIEventListener.Get(AttackButten).onClick += delegate (GameObject go)
        {
            Type = 1; OpenOne(1);
            SetItemList(hero);
        };
        UIEventListener.Get(ExistButten).onClick += delegate (GameObject go)
        {
            Type = 2;
            OpenOne(2);
            SetItemList(hero);
        };
        UIEventListener.Get(SurgeButten).onClick += delegate (GameObject go)
        {
            Type = 3;
            OpenOne(3);
            SetItemList(hero);

        };
        UIEventListener.Get(HelpButten).onClick += delegate (GameObject go)
        {

        };
        UIEventListener.Get(AddDowerButten).onClick += delegate (GameObject go)
        {
            UIManager.instance.OpenSinglePanel("ExchangeWindow",false);
        };
        UIEventListener.Get(RefeButten).onClick += delegate (GameObject go)
        {
            //RefreshWindow.SetActive(true);
            //if (RefreshWindow.activeSelf == true)
            //{
            //    RefreshWindow.GetComponent<RefreshWindow>().SetWindow("2112", "500");
            //}
            UIManager.instance.OpenPromptWindow("您是否重置天赋？", PromptWindow.PromptType.Traitor, RestDower, null); ;//无间道
           
        };

    }
   
    void RestDower()
    {
        NetworkHandler.instance.SendProcess("1623#" + HeroDowerPart.instance.hero.characterRoleID + ";");
    }
    void OpenOne(int i)
    {
        if (i == 1)
        {
            AttackButten.GetComponent<UITexture>().color = new Color(1,1,1);
            ExistButten.GetComponent<UITexture>().color = new Color(139 / (float)255, 139 / (float)255, 139 / (float)255);
            SurgeButten.GetComponent<UITexture>().color = new Color(139 / (float)255, 139 / (float)255, 139 / (float)255);
            One.SetActive(true);
            Two.SetActive(false);
            Three.SetActive(false);
        }
        else if (i == 2)
        {
            AttackButten.GetComponent<UITexture>().color = new Color(139 / (float)255, 139 / (float)255, 139 / (float)255);
            ExistButten.GetComponent<UITexture>().color = new Color(1,1,1);
            SurgeButten.GetComponent<UITexture>().color = new Color(139 / (float)255, 139 / (float)255, 139 / (float)255);
            One.SetActive(false);
            Two.SetActive(true);
            Three.SetActive(false);;
        }
        else if (i == 3)
        {
            AttackButten.GetComponent<UITexture>().color = new Color(139 / (float)255, 139 / (float)255, 139 / (float)255);
            ExistButten.GetComponent<UITexture>().color = new Color(139 / (float)255, 139 / (float)255, 139 / (float)255);
            SurgeButten.GetComponent<UITexture>().color = new Color(1,1,1);
            One.SetActive(false);
            Two.SetActive(false);
            Three.SetActive(true);;
        }
    }
   
    public string[] heroDower;
    public void SetItemList(Hero num)
    {
      
        hero = num;
       

        dsdsdsdsd();
    }
  public  void dsdsdsdsd()
    {
       
        HeroUse = 0;
       
        heroDower = TextTranslator.instance.GetDowerByheroID(hero.characterRoleID);

      //  CharacterRecorder.instance.DowerNum = int.Parse(heroDower[18]);
        //for (int i = 0; i < 18; i++)
        //{
        //    HeroUse +=int.Parse( heroDower[i]);
        //}
        HeroUse = TextTranslator.instance.GetNowNum(hero.characterRoleID);
        All.GetComponent<DowerWindow>().SetAllItem(heroDower);
        LableNum.text = HeroUse.ToString();
    }
   
    public int GetNowNum()
    {
        int num=0;
        for (int i = 1; i < 19; i++)
        {
            for (int j = 0; j <= int.Parse(heroDower[i-1]); j++)
            {
               
                if (j!=0)
                {
                    num += TextTranslator.instance.GetInnatesByTwo(i, j).TalentCost;
                  //  print(TextTranslator.instance.GetInnatesByTwo(i, j).TalentCost + ":" + i + "o" + j);
                }
                
            }
        }
        return HeroUse+num;
    }
}
