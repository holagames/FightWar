using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class NuclearwarWindow : MonoBehaviour
{
    public UILabel NeedNumber;
    public UILabel NowNumber;
    public UILabel WillNumber;
    public UILabel BuyNumb;//huangei
    public UILabel Text;
    public UILabel ItemNumber;
    public UILabel ItemNextNumb;
    public GameObject BuyButten;
    public GameObject butten;
    public GameObject ButenNot;
    public GameObject Challenge;
    public GameObject Help;
    public GameObject BuyWindow;
    public GameObject StateWindow;
    public UILabel GK;//关卡
    public GameObject Cancel;//取消
    public GameObject Ensure;//确定
    public GameObject HerTexture;
    public List<GameObject> Item1list = new List<GameObject>();
    public List<GameObject> Item2list = new List<GameObject>();
    int LevelNum;
    string istr;
    // Use this for initialization
    void Awake()
    {
        NetworkHandler.instance.SendProcess("6601#;");

    }
    void Start()
    {
        UIEventListener.Get(Ensure).onClick += delegate(GameObject go)
          {
              NetworkHandler.instance.SendProcess("6603#;");
              CloseBuyWindow();
          };
        UIEventListener.Get(butten).onClick += delegate(GameObject go)
          {
              NetworkHandler.instance.SendProcess("6604#" + (LevelNum - 1) + ";");
          };
        UIEventListener.Get(Challenge).onClick += delegate(GameObject go)
        {
            if (int.Parse(NeedNumber.text) <= int.Parse(NowNumber.text))//战力是否符合
            {

                if (istr == "1")
                {
                    UIManager.instance.OpenPromptWindow("奖励尚未领取，请先领取奖励", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    //NetworkHandler.instance.SendProcess("6602#" + LevelNum + ";"); Debug.LogError("挑战");
                    UIManager.instance.OpenPanel("LoadingWindow", true);
                    CharacterRecorder.instance.NuclearLevel = LevelNum;
                    SceneTransformer.instance.NowGateID = 89000;
                    PictureCreater.instance.StartFight();
                    PictureCreater.instance.FightStyle = 17;
                }
            }
            else
            {
                UIManager.instance.OpenPromptWindow("当前战力不足,请先提升战力", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(Help).onClick += delegate(GameObject go)
        {
            StateWindow.SetActive(true);
        };
       //TextTranslator.instance.ItemDescription()
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void CloseStateWindow()
    {
        StateWindow.SetActive(false);
    }
    public void OpenStateWindow()
    {
        StateWindow.SetActive(true);
    }
    public void CloseBuyWindow()
    {
        BuyWindow.SetActive(false);
    }
    public void OpenBuyWindow()
    {
        BuyWindow.SetActive(true);
    }
    public void SetState(int Level, int BuyNumber, string isGet)//6601
    {
        istr = isGet;
        if (Level < 1)
        {
            Level = 1;
        }
        LevelNum = Level;
        int AddFoce;
        if (TextTranslator.instance.GetNuclearPlanByID(BuyNumber) != null)
        {
            AddFoce = TextTranslator.instance.GetNuclearPlanByID(BuyNumber).AddForce;
        }
        else
        {
            AddFoce = 0;
        }
        NeedNumber.text = (TextTranslator.instance.GetGateByID(89000).needForce * Level).ToString(); //(Level* 10000).ToString();
        GK.text = "-当前关卡数" + (Level).ToString() + "-";
        HerTexture.GetComponent<UITexture>().mainTexture = Resources.Load(string.Format("Loading/{0}", ChangeHero(Level))) as Texture;
        HerTexture.GetComponent<UITexture>().MakePixelPerfect();
        HerTexture.GetComponent<UITexture>().width = (int)(HerTexture.GetComponent<UITexture>().width * 0.8f);
        HerTexture.GetComponent<UITexture>().height = (int)(HerTexture.GetComponent<UITexture>().height * 0.8f);
        //  HerTexture.transform.localPosition = new Vector3(-17, 29, 0);
        // HerTexture.GetComponent<UITexture>(). mainTexture = Resources.Load(string.Format("Loading /{0}", ChangeHero(Level))) as Texture;
        BuyNumb.text = TextTranslator.instance.GetNuclearPlanByID(BuyNumber + 1).DiamondsValue.ToString();
        BuyButten.transform.Find("Numb").GetComponent<UILabel>().text = TextTranslator.instance.GetNuclearPlanByID(BuyNumber + 1).DiamondsValue.ToString();
        NowNumber.text = (CharacterRecorder.instance.Fight + AddFoce).ToString();
        WillNumber.text = AddFoce.ToString();
        TextTranslator.instance.ItemDescription(Item1list[0],10106,0);
        TextTranslator.instance.ItemDescription(Item1list[1], 79999, 0);
        TextTranslator.instance.ItemDescription(Item2list[0], 10106, 0);
        TextTranslator.instance.ItemDescription(Item2list[1], 79999, 0);
        ChangeColor(NeedNumber, NowNumber);
        if (Level == 1)
        {
            ItemNumber.text = "2";
            ItemNextNumb.text = "2";
        }
        else
        {
            ItemNumber.text = ((Level - 1) * 2).ToString();
            ItemNextNumb.text = (Level * 2).ToString();
        }
        if (isGet == "0")
        {
            ButenNot.SetActive(true);
            ButenNot.transform.Find("Label").GetComponent<UILabel>().text = "未挑战";
            butten.SetActive(false);
        }
        else if (isGet == "1")
        {
            ButenNot.SetActive(false);
            butten.SetActive(true);
        }
        else if (isGet == "2")
        {
            ButenNot.transform.Find("Label").GetComponent<UILabel>().text = "已领取";
            ButenNot.SetActive(true);
            butten.SetActive(false);
        }
        GameObject.Find("TopContent").GetComponent<TopContent>().Reset();
    }
    public void ChangeColor(UILabel item, UILabel iten1)
    {
        if (int.Parse(item.text) > int.Parse(iten1.text))
        {
            iten1.color = new Color(1, 0, 0);
        }
        else
        {
            iten1.color = new Color(147 / 255f, 188 / 255f, 188 / 255f);
        }
        GameObject.Find("TopContent").GetComponent<TopContent>().Reset();
    }
    public void SetLastState(string IsFill, int Level, int BuyNumber, string isGet)//6602
    {
        GameObject.Find("TopContent").GetComponent<TopContent>().Reset();
        istr = isGet;
        if (IsFill == "1")
        {
            LevelNum = Level;
            int AddFoce;
            if (TextTranslator.instance.GetNuclearPlanByID(BuyNumber) != null)
            {
                AddFoce = TextTranslator.instance.GetNuclearPlanByID(BuyNumber).AddForce;
            }
            else
            {
                AddFoce = 0;
            }
            NeedNumber.text = (TextTranslator.instance.GetGateByID(89000).needForce * Level).ToString();
            GK.text = "-当前关卡数" + (Level).ToString() + "-";
            HerTexture.GetComponent<UITexture>().mainTexture = Resources.Load(string.Format("Loading/{0}", ChangeHero(Level))) as Texture;
            HerTexture.GetComponent<UITexture>().MakePixelPerfect();
            HerTexture.GetComponent<UITexture>().width = (int)(HerTexture.GetComponent<UITexture>().width * 0.8f);
            HerTexture.GetComponent<UITexture>().height = (int)(HerTexture.GetComponent<UITexture>().height * 0.8f);
            //HerTexture.transform.localPosition = new Vector3(-17,29,0);
            BuyNumb.text = TextTranslator.instance.GetNuclearPlanByID(BuyNumber + 1).DiamondsValue.ToString();
            BuyButten.transform.Find("Numb").GetComponent<UILabel>().text = TextTranslator.instance.GetNuclearPlanByID(BuyNumber + 1).DiamondsValue.ToString();
            NowNumber.text = (CharacterRecorder.instance.Fight + AddFoce).ToString();
            WillNumber.text = AddFoce.ToString();
            ChangeColor(NeedNumber, NowNumber);
            if (Level == 1)
            {
                ItemNumber.text = "2";
                ItemNextNumb.text = "2";
            }
            else
            {
                ItemNumber.text = ((Level - 1) * 2).ToString();
                ItemNextNumb.text = (Level * 2).ToString();
            }
            if (isGet == "0")
            {
                ButenNot.SetActive(true);
                ButenNot.transform.Find("Label").GetComponent<UILabel>().text = "未挑战";
                butten.SetActive(false);
            }
            else if (isGet == "1")
            {
                ButenNot.SetActive(false);
                butten.SetActive(true);
            }
            else if (isGet == "2")
            {
                ButenNot.transform.Find("Label").GetComponent<UILabel>().text = "已领取";
                ButenNot.SetActive(true);
                butten.SetActive(false);
            }
        }
        else
        {
            print("失败");
        }
    }
    public void BuyVigor(string istrue, int num)//6003
    {
        CharacterRecorder.instance.lunaGem -= TextTranslator.instance.GetNuclearPlanByID(num).DiamondsValue;

        GameObject.Find("TopContent").GetComponent<TopContent>().Reset();
        if (istrue == "0")
        {
            print("购买失败"); UIManager.instance.OpenPromptWindow("购买失败", PromptWindow.PromptType.Hint, null, null);
        }
        else
        {
            NowNumber.text = (CharacterRecorder.instance.Fight + TextTranslator.instance.GetNuclearPlanByID(num).AddForce).ToString();
            WillNumber.text = TextTranslator.instance.GetNuclearPlanByID(num).AddForce.ToString();
            BuyNumb.text = TextTranslator.instance.GetNuclearPlanByID(num + 1).DiamondsValue.ToString();
            BuyButten.transform.Find("Numb").GetComponent<UILabel>().text = TextTranslator.instance.GetNuclearPlanByID(num + 1).DiamondsValue.ToString();
            ChangeColor(NeedNumber, NowNumber);
            UIManager.instance.OpenPromptWindow("购买成功，已服用兴奋剂；", PromptWindow.PromptType.Hint, null, null);
        }

    }

    public void SetLastItem(string istrue)
    {
        GameObject.Find("TopContent").GetComponent<TopContent>().Reset();
        if (istrue == "1")
        {
            istr = "0";
            butten.SetActive(false);
            ButenNot.SetActive(true);
        }
        else
        {
            UIManager.instance.OpenPromptWindow("领取失败", PromptWindow.PromptType.Hint, null, null);
        }
    }
    public void SetNextItem(string Item1, string Item2)//6004
    {
        GameObject.Find("TopContent").GetComponent<TopContent>().Reset();
        //Item2list[1].GetComponent<NuclearwarItem>().Init(int.Parse(Item2.Split('$')[0]), int.Parse(Item2.Split('$')[1]));
        //Item2list[0].GetComponent<NuclearwarItem>().Init(int.Parse(Item1.Split('$')[0]), int.Parse(Item1.Split('$')[1]));

    }

    public string ChangeHero(int num)
    {
        string hero = "60010";
        switch (num % 10)
        {
            case 0: hero = "60010"; break;
            case 1: hero = "60001"; break;
            case 2: hero = "60002"; break;
            case 3: hero = "60003"; break;
            case 4: hero = "60004"; break;
            case 5: hero = "60005"; break;
            case 6: hero = "60006"; break;
            case 7: hero = "60007"; break;
            case 8: hero = "60008"; break;
            case 9: hero = "60009"; break;
            default:
                break;
        }
        return hero;
    }

}
