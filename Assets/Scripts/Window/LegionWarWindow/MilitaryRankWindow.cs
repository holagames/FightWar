using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MilitaryRankWindow : MonoBehaviour {

    public GameObject DragItem;
    public GameObject uiGrid;
    public GameObject CloseButton;
    public UILabel JungongLabel;
    public UILabel JunxianLabel;
    private List<int> ConditionList = new List<int>();
    private List<int> NationIDlist = new List<int>();
    private List<membertype> membertypeList = new List<membertype>();

    private int JunxianWeizhi = 0;  //位置，军衔图片用

    void OnEnable()
    {
        NetworkHandler.instance.SendProcess("8104#"+CharacterRecorder.instance.legionCountryID+";");
    }
	void Start () {
        UIEventListener.Get(CloseButton).onClick = delegate(GameObject go)
        {
            //UIManager.instance.BackUI();
            DestroyImmediate(this.gameObject);
        };
        foreach (Transform tran in GetComponentsInChildren<Transform>()) 
        {
            tran.gameObject.layer = 9;
        }
	}

    public void AddNationType(string Recving) 
    {
        ConditionList.Clear();
        NationIDlist.Clear();
        string[] dataSplit = Recving.Split(';');
        int type = int.Parse(dataSplit[0]);
        CharacterRecorder.instance.NationID = int.Parse(dataSplit[3]);  //我的军衔id
        CharacterRecorder.instance.TotalMedals = int.Parse(dataSplit[2]);//我饿累计军功
        JungongLabel.text = CharacterRecorder.instance.TotalMedals.ToString();
        if (CharacterRecorder.instance.NationID != 0)
        {
            JunxianLabel.text = TextTranslator.instance.GetNationByID(CharacterRecorder.instance.NationID).OfficeName;
        }
        else 
        {
            JunxianLabel.text = "无军衔";
        }
        string[] trcSplit = dataSplit[1].Split('!');
        
        for (int i = 0; i < trcSplit.Length - 1; i++) 
        {
            string[] prcSplit = trcSplit[i].Split('$');           
            membertypeList.Add(new membertype(int.Parse(prcSplit[0]), int.Parse(prcSplit[1]), int.Parse(prcSplit[2]), prcSplit[3], int.Parse(prcSplit[4])));
        }


        foreach (var item in TextTranslator.instance.NationList) //确定品阶分类
        {
            if (item.NationType == type) 
            {
                if (!ConditionList.Contains(item.Condition)) 
                {
                    ConditionList.Add(item.Condition);
                    NationIDlist.Add(item.ID);
                }
            }
        }
        for (int i = 0; i < ConditionList.Count; i++) //clone品阶品
        {
            JunxianWeizhi++;
            GameObject go = NGUITools.AddChild(uiGrid, DragItem);
            go.SetActive(true);
            go.name = ConditionList[i].ToString();
            go.transform.Find("TopLabel").GetComponent<UILabel>().text = "";//"第"+(i+1)+"批";


            GameObject RankIcon = go.transform.Find("RankIcon").gameObject;
            JunxianIcon(RankIcon, NationIDlist[i]);


            int num = 0;
            for (int j = 0; j < membertypeList.Count; j++) //对应品阶物品加载人物
            {
                if (membertypeList[j].Condition== ConditionList[i]) 
                {
                    num++;
                    switch (num)
                    {
                        case 1: 
                            GameObject obj=go.transform.Find("HeroItem1").gameObject;
                            obj.SetActive(true);
                            membertypeList[j].AddmembertypeObj(obj);
                            break;
                        case 2:
                            GameObject obj2=go.transform.Find("HeroItem2").gameObject;
                            obj2.SetActive(true);
                            membertypeList[j].AddmembertypeObj(obj2);
                            break;
                        case 3:
                            GameObject obj3=go.transform.Find("HeroItem3").gameObject;
                            obj3.SetActive(true);
                            membertypeList[j].AddmembertypeObj(obj3);
                            break;
                        case 4:
                            GameObject obj4=go.transform.Find("HeroItem4").gameObject;
                            obj4.SetActive(true);
                            membertypeList[j].AddmembertypeObj(obj4);
                            break;

                    }
                }
            }

            if (CharacterRecorder.instance.NationID != 0) //有军衔
            {
                if (TextTranslator.instance.GetNationByID(CharacterRecorder.instance.NationID).Condition == ConditionList[i])
                {
                    go.transform.Find("JunGongSprite").gameObject.SetActive(false);
                    go.transform.Find("AutoJunGongSprite").gameObject.SetActive(true);
                    go.transform.Find("AutoJunGongSprite/JungongLabel").GetComponent<UILabel>().text = TextTranslator.instance.GetNationByID(CharacterRecorder.instance.NationID).EveryDayCost.ToString();
                }
                else
                {
                    go.transform.Find("JunGongSprite").gameObject.SetActive(true);
                    go.transform.Find("AutoJunGongSprite").gameObject.SetActive(false);
                    UILabel ConditionLabel = go.transform.Find("JunGongSprite/JungongLabel").GetComponent<UILabel>();
                    if (CharacterRecorder.instance.TotalMedals >= ConditionList[i])
                    {
                        ConditionLabel.text = "[3ee817]" + ConditionList[i].ToString() + "[-]";
                    }
                    else
                    {
                        ConditionLabel.text = ConditionList[i].ToString();
                    }
                }
            }
            else 
            {
                go.transform.Find("JunGongSprite").gameObject.SetActive(true);
                go.transform.Find("AutoJunGongSprite").gameObject.SetActive(false);
                UILabel ConditionLabel = go.transform.Find("JunGongSprite/JungongLabel").GetComponent<UILabel>();
                ConditionLabel.text = ConditionList[i].ToString();
            }
        }
        uiGrid.GetComponent<UIGrid>().Reposition();
    }

    private void JunxianIcon(GameObject go, int Id)
    {
        UISprite Rankicon = go.GetComponent<UISprite>();
        if (JunxianWeizhi == 1)
        {
            Rankicon.spriteName = "guozhanjunxian1";
        }
        else if (JunxianWeizhi == 2)
        {
            Rankicon.spriteName = "guozhanjunxian2";
        }
        else if (JunxianWeizhi == 3)
        {
            Rankicon.spriteName = "guozhanjunxian3";
        }
        else
        {
            Rankicon.spriteName = "guozhanjunxian4";
        }
        go.transform.Find("HurtNumber").GetComponent<UILabel>().text = "国战伤害加成"+TextTranslator.instance.GetNationByID(Id).BattlefieldDamageBonus*100+"%";
        go.transform.Find("Name").GetComponent<UILabel>().text = TextTranslator.instance.GetNationByID(Id).OfficeName;
    }
    //void JunxianIcon(UILabel JunIcon, int Condition) 
    //{
    //    if (CharacterRecorder.instance.legionCountryID == 1) 
    //    {
    //        switch (Condition)
    //        {
    //            case 
    //        }
    //    }
    //}
}


public class membertype 
{
    public GameObject Obj;
    public int ID;
    public int UID;
    public int HeadIcon;
    public string Name;
    public int FightNumber;
    public int Condition;
    public Nation nation;

    public membertype(int ID, int UID, int HeadIcon, string Name, int FightNumber) 
    {
        this.ID = ID;
        this.UID = UID;
        this.HeadIcon = HeadIcon;
        this.Name = Name;
        this.FightNumber = FightNumber;

        this.nation = TextTranslator.instance.GetNationByID(ID);
        this.Condition = nation.Condition;
    }

    public void AddmembertypeObj(GameObject go) 
    {
        this.Obj = go;
        SetmembertypeObjInfo();
    }

    public void SetmembertypeObjInfo() 
    {
        Obj.name = ID.ToString();
        Obj.transform.Find("HeroIcon").GetComponent<UITexture>().mainTexture = Resources.Load(string.Format("Head/{0}", HeadIcon), typeof(Texture)) as Texture;
        Obj.transform.Find("Name").GetComponent<UILabel>().text = Name;
        Obj.transform.Find("fightall/PowerNumber").GetComponent<UILabel>().text = FightNumber.ToString();
        UIEventListener.Get(Obj).onClick = delegate(GameObject go)
        {
            //UIManager.instance.OpenPanel("LegionMemberItemDetail", false);
            //NetworkHandler.instance.SendProcess("1020#" + UID + ";");
        };


        if (Name == CharacterRecorder.instance.characterName) //此位置为我
        {
            Obj.transform.Find("ChallengeButton").gameObject.SetActive(false);
        }
        else 
        {
            if (CharacterRecorder.instance.NationID == 0) //没有军衔
            {
                if (CharacterRecorder.instance.TotalMedals >= nation.Condition)
                {
                    Obj.transform.Find("ChallengeButton").GetComponent<UISprite>().spriteName = "ui2_button4";
                    UIEventListener.Get(Obj.transform.Find("ChallengeButton").gameObject).onClick = delegate(GameObject go)
                    {
                        CharacterRecorder.instance.EnemyMilitaryRankID = this.ID;
                        PictureCreater.instance.FightStyle = 16;
                        NetworkHandler.instance.SendProcess("6003#" + UID + ";");
                        Debug.LogError("挑战");
                    };
                }
                else
                {
                    Obj.transform.Find("ChallengeButton").GetComponent<UISprite>().spriteName = "buttonHui";
                    UIEventListener.Get(Obj.transform.Find("ChallengeButton").gameObject).onClick = delegate(GameObject go)
                    {
                        Debug.LogError("军功不足，不能挑战");
                        UIManager.instance.OpenPromptWindow("军功不足，不能挑战", PromptWindow.PromptType.Hint, null, null);    
                    };
                }
            }
            else  //有军衔
            {
                if (TextTranslator.instance.GetNationByID(CharacterRecorder.instance.NationID).Condition < nation.Condition)//我的军衔小于挑战对象 
                {
                    if (CharacterRecorder.instance.TotalMedals >= nation.Condition)
                    {
                        Obj.transform.Find("ChallengeButton").GetComponent<UISprite>().spriteName = "ui2_button4";
                        UIEventListener.Get(Obj.transform.Find("ChallengeButton").gameObject).onClick = delegate(GameObject go)
                        {
                            CharacterRecorder.instance.EnemyMilitaryRankID = this.ID;
                            PictureCreater.instance.FightStyle = 16;
                            NetworkHandler.instance.SendProcess("6003#" + UID + ";");
                            Debug.LogError("挑战");
                        };
                    }
                    else
                    {
                        Obj.transform.Find("ChallengeButton").GetComponent<UISprite>().spriteName = "buttonHui";
                        UIEventListener.Get(Obj.transform.Find("ChallengeButton").gameObject).onClick = delegate(GameObject go)
                        {
                            Debug.LogError("军功不足，不能挑战");
                            UIManager.instance.OpenPromptWindow("军功不足，不能挑战", PromptWindow.PromptType.Hint, null, null);    
                        };
                    }
                }
                else 
                {
                    Obj.transform.Find("ChallengeButton").GetComponent<UISprite>().spriteName = "buttonHui";
                    UIEventListener.Get(Obj.transform.Find("ChallengeButton").gameObject).onClick = delegate(GameObject go)
                    {
                        Debug.LogError("您无需挑战比自己军衔等级低的下属!");
                        UIManager.instance.OpenPromptWindow("您无需挑战比自己军衔等级低的下属!", PromptWindow.PromptType.Hint, null, null);    
                    };
                }
            }
        }
    }
}