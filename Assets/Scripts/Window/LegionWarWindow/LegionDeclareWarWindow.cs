using UnityEngine;
using System.Collections;

public class LegionDeclareWarWindow : MonoBehaviour {

    public UILabel LeftLegionName;
    public UILabel RightLegionName;
    public UILabel SoldierNumber;
    public UILabel SoldierNumberNew;
    public GameObject BackButton;
    public GameObject teamTex;
    public GameObject uiGrid;
    public GameObject TeamButton1;
    public GameObject TeamButton2;
    public GameObject TeamButton3;

    public GameObject ToggleButton1;
    public GameObject ToggleButton2;
    public GameObject ToggleButton3;

    private bool IsFuhuo1 = false; //记录点击toggle按钮之前的按钮状态，恢复用
    private bool IsFuhuo2 = false;
    private bool IsFuhuo3 = false;
    private bool IsFang = false;
    private LegionwarGetnode legionwarGetnode;
    private LegionCity legionCity;
    private int clickNum = 0;
	void Start () {
        NetworkHandler.instance.SendProcess("8640#" + CharacterRecorder.instance.LegionHarasPoint + ";");
        //NetworkHandler.instance.SendProcess("8636#;");
        //NetworkHandler.instance.SendProcess("8602#" + CharacterRecorder.instance.LegionHarasPoint+";");
        UIEventListener.Get(BackButton).onClick = delegate(GameObject go)
        {
            UIManager.instance.BackUI();
        };

        UIEventListener.Get(TeamButton1.transform.Find("ShangzhenButton").gameObject).onClick = delegate(GameObject go)
        {
            CharacterRecorder.instance.MarinesTabe = 1;
            NetworkHandler.instance.SendProcess("8635#1;" + CharacterRecorder.instance.LegionHarasPoint + ";");
        };
        UIEventListener.Get(TeamButton1.transform.Find("FuhuoButton").gameObject).onClick = delegate(GameObject go)
        {
            CharacterRecorder.instance.MarinesTabe = 1;
            UIManager.instance.OpenSinglePanel("LegionTeamWindow", false);
        };



        UIEventListener.Get(TeamButton2.transform.Find("ShangzhenButton").gameObject).onClick = delegate(GameObject go)
        {
            CharacterRecorder.instance.MarinesTabe = 2;
            NetworkHandler.instance.SendProcess("8635#2;" + CharacterRecorder.instance.LegionHarasPoint + ";");
        };
        UIEventListener.Get(TeamButton2.transform.Find("FuhuoButton").gameObject).onClick = delegate(GameObject go)
        {
            CharacterRecorder.instance.MarinesTabe = 2;
            UIManager.instance.OpenSinglePanel("LegionTeamWindow", false);
        };



        UIEventListener.Get(TeamButton3.transform.Find("ShangzhenButton").gameObject).onClick = delegate(GameObject go)
        {
            CharacterRecorder.instance.MarinesTabe = 3;
            NetworkHandler.instance.SendProcess("8635#3;" + CharacterRecorder.instance.LegionHarasPoint + ";");
        };
        UIEventListener.Get(TeamButton3.transform.Find("FuhuoButton").gameObject).onClick = delegate(GameObject go)
        {
            CharacterRecorder.instance.MarinesTabe = 3;
            UIManager.instance.OpenSinglePanel("LegionTeamWindow", false);
        };

        UIEventListener.Get(ToggleButton1).onClick = delegate(GameObject go)
        {
            if (IsFuhuo1)
            {
                UIManager.instance.OpenPromptWindow("请先复活", PromptWindow.PromptType.Hint, null, null);
            }
            else if (PlayerPrefs.GetInt("LegionTeamChooseNum")!=1)
            {
                clickNum = 1;
                SureAutolifeWindow();
            }
            else 
            {
                NetworkHandler.instance.SendProcess("8641#1;");
            }
        };

        UIEventListener.Get(ToggleButton2).onClick = delegate(GameObject go)
        {
            if (IsFuhuo2)
            {
                UIManager.instance.OpenPromptWindow("请先复活", PromptWindow.PromptType.Hint, null, null);
            }
            else if (PlayerPrefs.GetInt("LegionTeamChooseNum") != 2)
            {
                clickNum = 2;
                SureAutolifeWindow();
            }
            else
            {
                NetworkHandler.instance.SendProcess("8641#2;");
            }
        };

        UIEventListener.Get(ToggleButton3).onClick = delegate(GameObject go)
        {
            if (IsFuhuo3)
            {
                UIManager.instance.OpenPromptWindow("请先复活", PromptWindow.PromptType.Hint, null, null);
            }
            else if (PlayerPrefs.GetInt("LegionTeamChooseNum") != 3)
            {
                clickNum = 3;
                SureAutolifeWindow();
            }
            else
            {
                NetworkHandler.instance.SendProcess("8641#3;");
            }
        };

        SetEveryTeamInfo();
        CancelInvoke();
        InvokeRepeating("UpdateSendProcess", 0, 10f);
	}


    /// <summary>
    /// 每10秒发送一次协议8640
    /// </summary>
    private void UpdateSendProcess() 
    {
        NetworkHandler.instance.SendProcess("8640#" + CharacterRecorder.instance.LegionHarasPoint + ";");
    }

    /// <summary>
    /// 取得攻守双方的军团名称
    /// </summary>
    public void GetTopLegionName(string leftName,string rightName,int soldierNum) 
    {
        LeftLegionName.text = rightName;
        RightLegionName.text = leftName;
        SoldierNumber.text = "守卫军数量:" + soldierNum.ToString();
    }

    /// <summary>
    /// 取得攻守双方的军团名称,新
    /// </summary>
    public void GetTopLegionNameNew(string leftName, string rightName, int soldierNum) 
    {
        SoldierNumberNew.text = soldierNum.ToString() + "队";
    }

    /// <summary>
    /// 守和攻双方队伍的详细信息
    /// </summary>
    /// <param name="leftName"></param>
    /// <param name="rightName"></param>
    /// <param name="soldierNum"></param>
    /// <param name="teamInfo"></param>
    public void GetSoldiersInfo(string Recving) 
    {
        string[] dataSplit = Recving.Split(';');       
        for (int i = uiGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGrid.transform.GetChild(i).gameObject);
        }
        if (dataSplit[0] != "")
        {
            //GetTopLegionName(dataSplit[0], dataSplit[1], int.Parse(dataSplit[2]));
            GetTopLegionNameNew(dataSplit[0], dataSplit[1], int.Parse(dataSplit[2]));
            if (dataSplit[3] != "")
            {
                bool isEffect = false;
                int cityType = TextTranslator.instance.GetLegionCityByID(CharacterRecorder.instance.LegionHarasPoint).CityType;
                if (cityType < 5)
                {
                    LegionwarGetnode LG = null;
                    for (int i = 0; i < CharacterRecorder.instance.LegionwarGetnodeList.Count; i++)
                    {
                        if (CharacterRecorder.instance.LegionwarGetnodeList[i].LegionPoint == int.Parse(dataSplit[2]))
                        {
                            LG = CharacterRecorder.instance.LegionwarGetnodeList[i];
                            break;
                        }
                    }
                    if (LG != null)
                    {
                        if (LG.IsAvoidWar == 3)
                        {
                            isEffect = true;
                        }
                    }
                }
                else
                {
                    isEffect = true;
                }

                for (int i = 3; i < dataSplit.Length - 1; i++)
                {
                    string[] trcSplit = dataSplit[i].Split('!');
                    GameObject go = NGUITools.AddChild(uiGrid, teamTex);
                    go.SetActive(true);
                    int num = 0;
                    if (trcSplit[0] != "")
                    {
                        string[] prcSplit = trcSplit[0].Split('$');
                        if (prcSplit[0] != "" && prcSplit[0] != "0")
                        {
                            GameObject teamshou = go.transform.Find("teamShou").gameObject;
                            teamshou.SetActive(true);
                            teamshou.transform.Find("characterName").GetComponent<UILabel>().text = prcSplit[0];
                            if (int.Parse(prcSplit[3]) > 0)
                            {
                                teamshou.transform.Find("Junxian").GetComponent<UILabel>().text = "军衔: " + TextTranslator.instance.GetNationByID(int.Parse(prcSplit[3])).OfficeName;
                            }
                            else
                            {
                                teamshou.transform.Find("Junxian").GetComponent<UILabel>().text = "军衔: " + "无";
                            }
                            teamshou.transform.Find("Xuruodu").GetComponent<UILabel>().text = "虚弱: " + float.Parse(prcSplit[4]) + "%";
                            teamshou.transform.Find("Dengji").GetComponent<UILabel>().text = "Lv." + prcSplit[1];

                            teamshou.transform.Find("SpriteFight/LabelFight").GetComponent<UILabel>().text = prcSplit[5];

                            teamshou.transform.Find("HeroGrade/HeroIcon").GetComponent<UISprite>().spriteName = prcSplit[2];
                            teamshou.transform.Find("HeroGrade/BloodNum").GetComponent<UILabel>().text = float.Parse(prcSplit[6]) + "%";
                            teamshou.transform.Find("HeroGrade/ProgressBar").GetComponent<UISlider>().value = float.Parse(prcSplit[6])/100;
                            num++;
                        }
                    }

                    if (trcSplit[1] != "")
                    {
                        string[] prcSplit = trcSplit[1].Split('$');
                        if (prcSplit[0] != "" && prcSplit[0]!="0") 
                        {
                            GameObject teamgong = go.transform.Find("teamGong").gameObject;
                            teamgong.SetActive(true);
                            teamgong.transform.Find("characterName").GetComponent<UILabel>().text = prcSplit[0];

                            if (int.Parse(prcSplit[3]) > 0)
                            {
                                teamgong.transform.Find("Junxian").GetComponent<UILabel>().text = "军衔: " + TextTranslator.instance.GetNationByID(int.Parse(prcSplit[3])).OfficeName;
                            }
                            else
                            {
                                teamgong.transform.Find("Junxian").GetComponent<UILabel>().text = "军衔: " + "无";
                            }

                            teamgong.transform.Find("Xuruodu").GetComponent<UILabel>().text = "虚弱: " + float.Parse(prcSplit[4]) + "%";
                            teamgong.transform.Find("Dengji").GetComponent<UILabel>().text = "Lv." + prcSplit[1];

                            teamgong.transform.Find("SpriteFight/LabelFight").GetComponent<UILabel>().text = prcSplit[5];

                            teamgong.transform.Find("HeroGrade/HeroIcon").GetComponent<UISprite>().spriteName = prcSplit[2];
                            teamgong.transform.Find("HeroGrade/BloodNum").GetComponent<UILabel>().text = float.Parse(prcSplit[6]) + "%";
                            teamgong.transform.Find("HeroGrade/ProgressBar").GetComponent<UISlider>().value = float.Parse(prcSplit[6]) / 100;
                            num++;
                        }
                        
                    }

                    if (num == 2 && isEffect)
                    {
                        go.transform.Find("EffectJian").gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                //GetTopLegionName(dataSplit[0], dataSplit[1], int.Parse(dataSplit[2]));
            }
        }
        else 
        {
            //GetTopLegionName("", "", 0);
            GetTopLegionNameNew("", "", 0);
        }
        uiGrid.GetComponent<UIGrid>().Reposition();
    }

    /// <summary>
    /// 8639刷新通知取得底部信息
    /// </summary>
    public void SetEveryTeamInfo() 
    {
        for (int i = 0; i < CharacterRecorder.instance.LegionwarGetnodeList.Count; i++)
        {
            if (CharacterRecorder.instance.LegionwarGetnodeList[i].LegionPoint == CharacterRecorder.instance.LegionHarasPoint)
            {
                legionwarGetnode = CharacterRecorder.instance.LegionwarGetnodeList[i];
                legionCity = TextTranslator.instance.GetLegionCityByID(CharacterRecorder.instance.LegionHarasPoint);
                break;
            }
        }
        if (CharacterRecorder.instance.MarinesInfomation1.CityId != 0)
        {
            string[] trcSplit = CharacterRecorder.instance.MarinesInfomation1.BloodNumber.Split('!');
            string[] CaptainStr = trcSplit[5].Split('$');

            TeamButton1.SetActive(true);

            Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(int.Parse(CaptainStr[0]));
            TeamButton1.transform.Find("HeroIcon").GetComponent<UISprite>().spriteName = mhero.cardID.ToString();

            float allbloodnum = 0f;
            float allmaxbloodnum = 0f;

            for (int i = 0; i < trcSplit.Length - 1; i++)
            {
                string[] oneCaptainStr = trcSplit[i].Split('$');
                allbloodnum += float.Parse(oneCaptainStr[1]);
                allmaxbloodnum += float.Parse(oneCaptainStr[2]);
            }
            float bloodnum = allbloodnum / allmaxbloodnum;
            TeamButton1.transform.Find("BloodNum").GetComponent<UILabel>().text = (bloodnum * 100).ToString("f2") + "%";
            TeamButton1.transform.Find("ProgressBar").GetComponent<UISlider>().value = bloodnum;

            if (allbloodnum ==0)//血量为0，复活按钮    allbloodnum < allmaxbloodnum
            {
                TeamButton1.transform.Find("BloodSprite").gameObject.SetActive(true);
                float num = 103f * (1 - bloodnum);
                TeamButton1.transform.Find("BloodSprite").GetComponent<UISprite>().height = (int)num;
                TeamButton1.transform.Find("FuhuoButton").gameObject.SetActive(true);
                TeamButton1.transform.Find("ShangzhenButton").gameObject.SetActive(false);
                IsFuhuo1 = true;
            }
            else
            {
                float num = 103f * (1 - bloodnum);
                TeamButton1.transform.Find("BloodSprite").GetComponent<UISprite>().height = (int)num;
                TeamButton1.transform.Find("BloodSprite").gameObject.SetActive(false);
                TeamButton1.transform.Find("FuhuoButton").gameObject.SetActive(false);
                TeamButton1.transform.Find("ShangzhenButton").gameObject.SetActive(true);
                SetEveryTeamButtonChangeColor(TeamButton1.transform.Find("ShangzhenButton").gameObject);
                IsFuhuo1 = false;
            }
            TeamButton1.transform.Find("DuiwuNum").GetComponent<UILabel>().text = "队伍1";
        }
        else
        {
            TeamButton1.SetActive(false);
        }

        if (CharacterRecorder.instance.MarinesInfomation2.CityId != 0)
        {
            string[] trcSplit = CharacterRecorder.instance.MarinesInfomation2.BloodNumber.Split('!');
            string[] CaptainStr = trcSplit[5].Split('$');

            //HeroInfo HI = TextTranslator.instance.GetHeroInfoByHeroID(int.Parse(CaptainStr[0]));
            TeamButton2.SetActive(true);

            Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(int.Parse(CaptainStr[0]));
            TeamButton2.transform.Find("HeroIcon").GetComponent<UISprite>().spriteName = mhero.cardID.ToString();

            float allbloodnum = 0f;
            float allmaxbloodnum = 0f;

            for (int i = 0; i < trcSplit.Length - 1; i++)
            {
                string[] oneCaptainStr = trcSplit[i].Split('$');
                allbloodnum += float.Parse(oneCaptainStr[1]);
                allmaxbloodnum += float.Parse(oneCaptainStr[2]);
            }
            float bloodnum = allbloodnum / allmaxbloodnum;
            TeamButton2.transform.Find("BloodNum").GetComponent<UILabel>().text = (bloodnum * 100).ToString("f2") + "%";
            TeamButton2.transform.Find("ProgressBar").GetComponent<UISlider>().value = bloodnum;


            if (allbloodnum ==0)
            {
                TeamButton2.transform.Find("BloodSprite").gameObject.SetActive(true);
                float num = 103f * (1 - bloodnum);
                TeamButton2.transform.Find("BloodSprite").GetComponent<UISprite>().height = (int)num;
                TeamButton2.transform.Find("FuhuoButton").gameObject.SetActive(true);
                TeamButton2.transform.Find("ShangzhenButton").gameObject.SetActive(false);
                IsFuhuo2 = true;
            }
            else
            {
                float num = 103f * (1 - bloodnum);
                TeamButton2.transform.Find("BloodSprite").GetComponent<UISprite>().height = (int)num;
                TeamButton2.transform.Find("BloodSprite").gameObject.SetActive(false);
                TeamButton2.transform.Find("FuhuoButton").gameObject.SetActive(false);
                TeamButton2.transform.Find("ShangzhenButton").gameObject.SetActive(true);
                SetEveryTeamButtonChangeColor(TeamButton2.transform.Find("ShangzhenButton").gameObject);
                IsFuhuo2 = false;
            }
            TeamButton2.transform.Find("DuiwuNum").GetComponent<UILabel>().text = "队伍2";
        }
        else
        {
            TeamButton2.SetActive(false);
        }

        if (CharacterRecorder.instance.MarinesInfomation3.CityId != 0)
        {
            string[] trcSplit = CharacterRecorder.instance.MarinesInfomation3.BloodNumber.Split('!');
            string[] CaptainStr = trcSplit[5].Split('$');

            TeamButton3.SetActive(true);

            Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(int.Parse(CaptainStr[0]));
            TeamButton3.transform.Find("HeroIcon").GetComponent<UISprite>().spriteName = mhero.cardID.ToString();

            float allbloodnum = 0f;
            float allmaxbloodnum = 0f;

            for (int i = 0; i < trcSplit.Length - 1; i++)
            {
                string[] oneCaptainStr = trcSplit[i].Split('$');
                allbloodnum += float.Parse(oneCaptainStr[1]);
                allmaxbloodnum += float.Parse(oneCaptainStr[2]);
            }
            float bloodnum = allbloodnum / allmaxbloodnum;
            TeamButton3.transform.Find("BloodNum").GetComponent<UILabel>().text = (bloodnum * 100).ToString("f2") + "%";
            TeamButton3.transform.Find("ProgressBar").GetComponent<UISlider>().value = bloodnum;


            if (allbloodnum ==0)
            {
                TeamButton3.transform.Find("BloodSprite").gameObject.SetActive(true);
                float num = 103f * (1 - bloodnum);
                TeamButton3.transform.Find("BloodSprite").GetComponent<UISprite>().height = (int)num;
                TeamButton3.transform.Find("FuhuoButton").gameObject.SetActive(true);
                TeamButton3.transform.Find("ShangzhenButton").gameObject.SetActive(false);
                IsFuhuo3 = true;
            }
            else
            {
                float num = 103f * (1 - bloodnum);
                TeamButton3.transform.Find("BloodSprite").GetComponent<UISprite>().height = (int)num;
                TeamButton3.transform.Find("BloodSprite").gameObject.SetActive(false);
                TeamButton3.transform.Find("FuhuoButton").gameObject.SetActive(false);
                TeamButton3.transform.Find("ShangzhenButton").gameObject.SetActive(true);
                SetEveryTeamButtonChangeColor(TeamButton3.transform.Find("ShangzhenButton").gameObject);
                IsFuhuo3 =false;
            }
            TeamButton3.transform.Find("DuiwuNum").GetComponent<UILabel>().text = "队伍3";
        }
        else
        {
            TeamButton3.SetActive(false);
        }

        CancelInvoke();
        InvokeRepeating("UpdateTime", 0, 1.0f);
        AutomaticDefense();
    }


    /// <summary>
    /// 队伍编队刷新倒计时
    /// </summary>
    void UpdateTime()
    {
        if (CharacterRecorder.instance.MarinesInfomation1.timeNum > 0)
        {
            //string miniteStr = (CharacterRecorder.instance.MarinesInfomation1.timeNum % 3600 / 60).ToString("00");
            //string secondStr = (CharacterRecorder.instance.MarinesInfomation1.timeNum % 60).ToString("00");
            //TeamButton1.transform.Find("TimeLabel").GetComponent<UILabel>().text = string.Format("{0}:{1}", miniteStr, secondStr);
            CharacterRecorder.instance.MarinesInfomation1.timeNum -= 1;
        }

        if (CharacterRecorder.instance.MarinesInfomation2.timeNum > 0)
        {
            //string miniteStr = (CharacterRecorder.instance.MarinesInfomation2.timeNum % 3600 / 60).ToString("00");
            //string secondStr = (CharacterRecorder.instance.MarinesInfomation2.timeNum % 60).ToString("00");
            //TeamButton2.transform.Find("TimeLabel").GetComponent<UILabel>().text = string.Format("{0}:{1}", miniteStr, secondStr);
            CharacterRecorder.instance.MarinesInfomation2.timeNum -= 1;
        }

        if (CharacterRecorder.instance.MarinesInfomation3.timeNum > 0)
        {
            //string miniteStr = (CharacterRecorder.instance.MarinesInfomation3.timeNum % 3600 / 60).ToString("00");
            //string secondStr = (CharacterRecorder.instance.MarinesInfomation3.timeNum % 60).ToString("00");
            //TeamButton3.transform.Find("TimeLabel").GetComponent<UILabel>().text = string.Format("{0}:{1}", miniteStr, secondStr);
            CharacterRecorder.instance.MarinesInfomation3.timeNum -= 1;
        }
    }


    /// <summary>
    /// 上阵和防守按钮各种情况下颜色变化
    /// </summary>
    /// <param name="button"></param>
    private void SetEveryTeamButtonChangeColor(GameObject button) 
    {
        int cityType = legionCity.CityType;

        if (cityType < 5)//军团点
        {
            if (CharacterRecorder.instance.legionID != 0)//有军团
            {
                if (CharacterRecorder.instance.myLegionData.legionLevel >= legionCity.LegionNeedLevel)//军团等级达标
                {
                    if (legionwarGetnode.LegionID != 0)//有帮会占领据点
                    {
                        if (legionwarGetnode.LegionID == CharacterRecorder.instance.legionID) //我军团占领
                        {
                            button.GetComponent<UISprite>().spriteName = "12button";
                            button.GetComponent<UIButton>().normalSprite = "12button";
                            button.transform.Find("Label").GetComponent<UILabel>().text = "防 守";
                            button.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(0f, 122 / 255f, 245 / 255f);
                            IsFang = true;
                        }
                        else //其它军团占领
                        {
                            if (legionwarGetnode.DeclareLegionName == CharacterRecorder.instance.myLegionData.legionName) //我军团对其宣战过
                            {
                                if (legionwarGetnode.IsAvoidWar == 3) //军团战时间开始 8.30以后
                                {
                                    button.GetComponent<UISprite>().spriteName = "12button";
                                    button.GetComponent<UIButton>().normalSprite = "12button";
                                    button.transform.Find("Label").GetComponent<UILabel>().text = "进 攻";
                                    button.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(0f, 122 / 255f, 245 / 255f);    
                                }
                                else 
                                {
                                    button.GetComponent<UISprite>().spriteName = "11button";
                                    button.GetComponent<UIButton>().normalSprite = "11button";
                                    button.transform.Find("Label").GetComponent<UILabel>().text = "进 攻";
                                    button.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(162 / 255f, 162 / 255f, 162 / 255f);
                                }
                            }
                            else 
                            {
                                button.GetComponent<UISprite>().spriteName = "11button";
                                button.GetComponent<UIButton>().normalSprite = "11button";
                                button.transform.Find("Label").GetComponent<UILabel>().text = "进 攻";
                                button.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(162 / 255f, 162 / 255f, 162 / 255f);
                                //UIManager.instance.OpenPromptWindow("尚未宣战", PromptWindow.PromptType.Hint, null, null);
                            }
                        }
                    }
                    else //没有帮会占领据点
                    {
                        button.GetComponent<UISprite>().spriteName = "12button";
                        button.GetComponent<UIButton>().normalSprite = "12button";
                        button.transform.Find("Label").GetComponent<UILabel>().text = "进 攻";
                        button.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(0f, 122 / 255f, 245 / 255f); 
                    }
                }
                else 
                {
                    button.GetComponent<UISprite>().spriteName = "11button";
                    button.GetComponent<UIButton>().normalSprite = "11button";
                    button.transform.Find("Label").GetComponent<UILabel>().text = "进 攻";
                    button.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(162 / 255f, 162 / 255f, 162 / 255f);
                    //UIManager.instance.OpenPromptWindow("军团等级不足", PromptWindow.PromptType.Hint, null, null);
                }
            }
            else
            {
                button.GetComponent<UISprite>().spriteName = "11button";
                button.GetComponent<UIButton>().normalSprite = "11button";
                button.transform.Find("Label").GetComponent<UILabel>().text = "进 攻";
                button.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(162 / 255f, 162 / 255f, 162 / 255f);

                //UIManager.instance.OpenPromptWindow("没有军团", PromptWindow.PromptType.Hint, null, null);
            }
        }
        else //骚扰点
        {
            if (CharacterRecorder.instance.legionID != 0 && legionwarGetnode.LegionID == CharacterRecorder.instance.legionID) 
            {
                button.GetComponent<UISprite>().spriteName = "12button";
                button.GetComponent<UIButton>().normalSprite = "12button";
                button.transform.Find("Label").GetComponent<UILabel>().text = "防 守";
                button.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(0f, 122 / 255f, 245 / 255f);
                IsFang = true;
            }
            //else if (legionwarGetnode.LegionID != 0 && legionwarGetnode.LegionID != CharacterRecorder.instance.legionID && legionwarGetnode.CountryID == CharacterRecorder.instance.legionCountryID) //同属国家的据点  yytest 2017/4/11
            //{
            //    button.GetComponent<UISprite>().spriteName = "11button";
            //    button.GetComponent<UIButton>().normalSprite = "11button";
            //    button.transform.Find("Label").GetComponent<UILabel>().text = "进 攻";
            //    button.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(162 / 255f, 162 / 255f, 162 / 255f);
            //}
            else 
            {
                button.GetComponent<UISprite>().spriteName = "12button";
                button.GetComponent<UIButton>().normalSprite = "12button";
                button.transform.Find("Label").GetComponent<UILabel>().text = "进 攻";
                button.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(0f, 122 / 255f, 245 / 255f); 
            }
        }
    }


    /// <summary>
    /// 初始进入   勾选按钮是否开启
    /// </summary>
    private void AutomaticDefense() 
    {
        if (CharacterRecorder.instance.Vip >= 11 && IsFang)
        {
            ToggleButton1.SetActive(true);
            ToggleButton2.SetActive(true);
            ToggleButton3.SetActive(true);
            ClickOneToggleButton();
        }
        else 
        {
            ToggleButton1.SetActive(false);
            ToggleButton2.SetActive(false);
            ToggleButton3.SetActive(false);
        }          
    }

    /// <summary>
    /// 8641 刷新判断
    /// </summary>
    public void ClickOneToggleButton() 
    {
        if (CharacterRecorder.instance.Vip >= 11 && IsFang) 
        {
            HuiFuLastChange();
            ClickButtonChangeInfo();
        }
    }

    /// <summary>
    /// 勾选一个按钮后，当前按钮强制为防守按钮
    /// </summary>
    private void ClickButtonChangeInfo() 
    {
        switch (PlayerPrefs.GetInt("LegionTeamChooseNum"))
        {
            case 0:
                ToggleButton1.transform.Find("Checkmark").gameObject.SetActive(false);
                ToggleButton2.transform.Find("Checkmark").gameObject.SetActive(false);
                ToggleButton3.transform.Find("Checkmark").gameObject.SetActive(false);
                break;
            case 1:
                ToggleButton1.transform.Find("Checkmark").gameObject.SetActive(true);
                ToggleButton2.transform.Find("Checkmark").gameObject.SetActive(false);
                ToggleButton3.transform.Find("Checkmark").gameObject.SetActive(false);
                //TeamButton1.transform.Find("FuhuoButton").gameObject.SetActive(false);
                //TeamButton1.transform.Find("ShangzhenButton").gameObject.SetActive(true);
                if (IsFuhuo1)
                {
                    NetworkHandler.instance.SendProcess("8641#1;");
                }
                break;
            case 2:
                ToggleButton1.transform.Find("Checkmark").gameObject.SetActive(false);
                ToggleButton2.transform.Find("Checkmark").gameObject.SetActive(true);
                ToggleButton3.transform.Find("Checkmark").gameObject.SetActive(false);
                if (IsFuhuo2)
                {
                    NetworkHandler.instance.SendProcess("8641#2;");
                }
                //TeamButton2.transform.Find("FuhuoButton").gameObject.SetActive(false);
                //TeamButton2.transform.Find("ShangzhenButton").gameObject.SetActive(true);
                break;
            case 3:
                ToggleButton1.transform.Find("Checkmark").gameObject.SetActive(false);
                ToggleButton2.transform.Find("Checkmark").gameObject.SetActive(false);
                ToggleButton3.transform.Find("Checkmark").gameObject.SetActive(true);
                if (IsFuhuo3)
                {
                    NetworkHandler.instance.SendProcess("8641#3;");
                }
                //TeamButton3.transform.Find("FuhuoButton").gameObject.SetActive(false);
                //TeamButton3.transform.Find("ShangzhenButton").gameObject.SetActive(true);
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// 勾选一个按钮后，其他的恢复原来按钮状态
    /// </summary>
    private void HuiFuLastChange() 
    {
        //if (IsFuhuo1)
        //{
        //    TeamButton1.transform.Find("FuhuoButton").gameObject.SetActive(true);
        //    TeamButton1.transform.Find("ShangzhenButton").gameObject.SetActive(false);
        //}
        //else
        //{
        //    TeamButton1.transform.Find("FuhuoButton").gameObject.SetActive(false);
        //    TeamButton1.transform.Find("ShangzhenButton").gameObject.SetActive(true);
        //}
        //if (IsFuhuo2)
        //{
        //    TeamButton2.transform.Find("FuhuoButton").gameObject.SetActive(true);
        //    TeamButton2.transform.Find("ShangzhenButton").gameObject.SetActive(false);
        //}
        //else
        //{
        //    TeamButton2.transform.Find("FuhuoButton").gameObject.SetActive(false);
        //    TeamButton2.transform.Find("ShangzhenButton").gameObject.SetActive(true);
        //}
        //if (IsFuhuo3)
        //{
        //    TeamButton3.transform.Find("FuhuoButton").gameObject.SetActive(true);
        //    TeamButton3.transform.Find("ShangzhenButton").gameObject.SetActive(false);
        //}
        //else
        //{
        //    TeamButton3.transform.Find("FuhuoButton").gameObject.SetActive(false);
        //    TeamButton3.transform.Find("ShangzhenButton").gameObject.SetActive(true);
        //}
    }

    private void SureAutolifeWindow() 
    {
        string resetMessage = "自动防守每次复活将会消耗复活石,是否确认？";
        UIManager.instance.OpenPromptWindow(resetMessage, PromptWindow.PromptType.Confirm, ResetBtnClick, null);
    }

    void ResetBtnClick() 
    {
        if (clickNum == 1) 
        {
            NetworkHandler.instance.SendProcess("8641#1;");
            CharacterRecorder.instance.MarinesTabe = 1;
            NetworkHandler.instance.SendProcess("8635#1;" + CharacterRecorder.instance.LegionHarasPoint + ";");
        }
        else if (clickNum == 2) 
        {
            NetworkHandler.instance.SendProcess("8641#2;");
            CharacterRecorder.instance.MarinesTabe = 2;
            NetworkHandler.instance.SendProcess("8635#2;" + CharacterRecorder.instance.LegionHarasPoint + ";");
        }
        else if (clickNum == 3)
        {
            NetworkHandler.instance.SendProcess("8641#3;");
            CharacterRecorder.instance.MarinesTabe = 3;
            NetworkHandler.instance.SendProcess("8635#3;" + CharacterRecorder.instance.LegionHarasPoint + ";");
        }
        else
        {
            NetworkHandler.instance.SendProcess("8641#0;");
        }
    }
}
