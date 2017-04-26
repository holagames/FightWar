using UnityEngine;
using System.Collections;

public class LegionTeamWindow : MonoBehaviour {

	// Use this for initialization
    public GameObject Uigrid;
    public GameObject TeamButton1;
    public GameObject TeamButton2;
    public GameObject TeamButton3;

    public GameObject RoleItem;

    public GameObject GotoButton;
    public GameObject NewModelButton;
    public GameObject CompulsorysoldierButton;
    public GameObject AkeysupplementButton;
    public GameObject DismissButton;
    public GameObject CloseButton;

    public GameObject CompulsorysoldierWindow;
    public GameObject CompulsoryCloseButton;
    public GameObject CompulsorySureButton;
    public GameObject CompulsoryCancelButton;


    private MarinesInfomation MaIn1;
    private MarinesInfomation MaIn2;
    private MarinesInfomation MaIn3;

	void Start () {
        foreach (Transform tran in GetComponentsInChildren<Transform>())
        {
            tran.gameObject.layer = 9;
        }
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                DestroyImmediate(this.gameObject);
            };
        }

        if (UIEventListener.Get(TeamButton1).onClick == null)
        {
            UIEventListener.Get(TeamButton1).onClick += delegate(GameObject go)
            {
                //CharacterRecorder.instance.MarinesTabe = 1;
                if (TeamButton1.transform.Find("HeroGrade").gameObject.activeSelf)
                {
                    CharacterRecorder.instance.MarinesTabe = 1;
                    LockOneTeamInfo(1);
                }
                else if (TeamButton1.transform.Find("TimeLabel").gameObject.activeSelf)
                {
                    //NetworkHandler.instance.SendProcess("8617#;");
                    UIManager.instance.OpenPromptWindow("还在CD时间!", PromptWindow.PromptType.Hint, null, null);
                }
                else if (CharacterRecorder.instance.LegionActionPoint <= 0)
                {
                    UIManager.instance.OpenPromptWindow("行动力不足!", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    CharacterRecorder.instance.MarinesTabe = 1;
                    for (int i = Uigrid.transform.childCount - 1; i >= 0; i--)
                    {
                        DestroyImmediate(Uigrid.transform.GetChild(i).gameObject);
                    }
                    UIManager.instance.OpenSinglePanel("LegionPositionWindow", false);
                    SetHeroEffect(1);
                }
            };
        }


        if (UIEventListener.Get(TeamButton2).onClick == null)
        {
            UIEventListener.Get(TeamButton2).onClick += delegate(GameObject go)
            {
                
                if (TeamButton2.transform.Find("HeroGrade").gameObject.activeSelf)
                {
                    CharacterRecorder.instance.MarinesTabe = 2;
                    LockOneTeamInfo(2);
                }
                else if (TeamButton2.transform.Find("TimeLabel").gameObject.activeSelf)
                {
                    //NetworkHandler.instance.SendProcess("8617#;");
                    UIManager.instance.OpenPromptWindow("还在CD时间!", PromptWindow.PromptType.Hint, null, null);
                }
                else if (CharacterRecorder.instance.LegionActionPoint <= 0)
                {
                    UIManager.instance.OpenPromptWindow("行动力不足!", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    CharacterRecorder.instance.MarinesTabe = 2;
                    for (int i = Uigrid.transform.childCount - 1; i >= 0; i--)
                    {
                        DestroyImmediate(Uigrid.transform.GetChild(i).gameObject);
                    }
                    UIManager.instance.OpenSinglePanel("LegionPositionWindow", false);
                    SetHeroEffect(2);
                }              
            };
        }

        if (UIEventListener.Get(TeamButton3).onClick == null)
        {
            UIEventListener.Get(TeamButton3).onClick += delegate(GameObject go)
            {
                
                if (TeamButton3.transform.Find("HeroGrade").gameObject.activeSelf)
                {
                    CharacterRecorder.instance.MarinesTabe = 3;
                    LockOneTeamInfo(3);
                }
                else if (TeamButton3.transform.Find("TimeLabel").gameObject.activeSelf)
                {
                    //NetworkHandler.instance.SendProcess("8617#;");
                    UIManager.instance.OpenPromptWindow("还在CD时间!", PromptWindow.PromptType.Hint, null, null);
                }
                else if (CharacterRecorder.instance.LegionActionPoint <= 0)
                {
                    UIManager.instance.OpenPromptWindow("行动力不足!", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    CharacterRecorder.instance.MarinesTabe = 3;
                    for (int i = Uigrid.transform.childCount - 1; i >= 0; i--)
                    {
                        DestroyImmediate(Uigrid.transform.GetChild(i).gameObject);
                    }
                    UIManager.instance.OpenSinglePanel("LegionPositionWindow", false);
                    SetHeroEffect(3);
                }
            };
        }

        if (UIEventListener.Get(GotoButton).onClick == null)
        {
            UIEventListener.Get(GotoButton).onClick += delegate(GameObject go)
            {
                DingweiButton();
            };
        }

        if (UIEventListener.Get(NewModelButton).onClick == null)
        {
            UIEventListener.Get(NewModelButton).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.MarinesTabe == 1) //点了按钮1
                {
                    if (CharacterRecorder.instance.MarinesInfomation1.CityId != 0)
                    {
                        //UIManager.instance.OpenSinglePanel("LegionPositionWindow", false);
                        SetCompulsorysoldierWindow(1);
                    }
                    else 
                    {
                        //UIManager.instance.OpenPromptWindow("行动力不足!", PromptWindow.PromptType.Hint, null, null);
                        Debug.Log("1还没有编队");
                    }
                }
                else if (CharacterRecorder.instance.MarinesTabe == 2) //点了按钮2
                {
                    if (CharacterRecorder.instance.MarinesInfomation2.CityId != 0)
                    {
                        //UIManager.instance.OpenSinglePanel("LegionPositionWindow", false);
                        SetCompulsorysoldierWindow(1);
                    }
                    else 
                    {
                        Debug.Log("2还没有编队");
                    }
                }
                else if (CharacterRecorder.instance.MarinesTabe == 3) //点了按钮2
                {
                    if (CharacterRecorder.instance.MarinesInfomation3.CityId != 0)
                    {
                        //UIManager.instance.OpenSinglePanel("LegionPositionWindow", false);
                        SetCompulsorysoldierWindow(1);
                    }
                    else
                    {
                        Debug.Log("3还没有编队");
                    }
                }
            };
        }

        if (UIEventListener.Get(CompulsorysoldierButton).onClick == null)
        {
            UIEventListener.Get(CompulsorysoldierButton).onClick += delegate(GameObject go)
            {
                CompulsorysoldierButtonOnclick();
            };
        }
        if (UIEventListener.Get(AkeysupplementButton).onClick == null)
        {
            UIEventListener.Get(AkeysupplementButton).onClick += delegate(GameObject go)
            {
                AkeysupplementButtonOnclick();
            };
        }
        if (UIEventListener.Get(DismissButton).onClick == null)
        {
            UIEventListener.Get(DismissButton).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.LegionActionPoint > 0)
                {
                    JieSanTeam();
                }
                else 
                {
                    UIManager.instance.OpenPromptWindow("行动力不足!", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }

        SetTeamItemButtonInfo();
        //LockOneTeamInfo(CharacterRecorder.instance.MarinesTabe);
	}

    private void DingweiButton() //定位
    {
        int CityId = 0;
        if (CharacterRecorder.instance.MarinesTabe == 1) //点了按钮1
        {
            if (CharacterRecorder.instance.MarinesInfomation1.CityId != 0)
            {
                CityId = CharacterRecorder.instance.MarinesInfomation1.CityId;
            }
            else
            {
                Debug.Log("1还没有编队");
            }
        }
        else if (CharacterRecorder.instance.MarinesTabe == 2) //点了按钮2
        {
            if (CharacterRecorder.instance.MarinesInfomation2.CityId != 0)
            {
                CityId = CharacterRecorder.instance.MarinesInfomation2.CityId;
            }
            else
            {
                Debug.Log("2还没有编队");
            }
        }
        else if (CharacterRecorder.instance.MarinesTabe == 3) //点了按钮2
        {
            if (CharacterRecorder.instance.MarinesInfomation3.CityId != 0)
            {
                CityId = CharacterRecorder.instance.MarinesInfomation3.CityId;
            }
            else
            {
                Debug.Log("3还没有编队");
            }
        }
        if (CityId != 0) 
        {
            if (GameObject.Find("LegionWarWindow") != null)
            {
                GameObject.Find("LegionWarWindow").GetComponent<LegionWarWindow>().GotoCityNum(CityId);
                DestroyImmediate(this.gameObject);
            }
            else 
            {
                UIManager.instance.OpenPromptWindow("请在战场界面使用此功能!", PromptWindow.PromptType.Hint, null, null);
            }
        }
    }

    private void JieSanTeam() 
    {
        if (CharacterRecorder.instance.MarinesTabe == 1) //点了按钮1
        {
            if (CharacterRecorder.instance.MarinesInfomation1.CityId != 0)
            {
                NetworkHandler.instance.SendProcess("8634#" + 1 + ";");
                DestroyImmediate(this.gameObject);
            }
            else
            {
                Debug.Log("1还没有编队");
            }
        }
        else if (CharacterRecorder.instance.MarinesTabe == 2) //点了按钮2
        {
            if (CharacterRecorder.instance.MarinesInfomation2.CityId != 0)
            {
                NetworkHandler.instance.SendProcess("8634#" + 2 + ";");
                DestroyImmediate(this.gameObject);
            }
            else
            {
                Debug.Log("2还没有编队");
            }
        }
        else if (CharacterRecorder.instance.MarinesTabe == 3) //点了按钮2
        {
            if (CharacterRecorder.instance.MarinesInfomation3.CityId != 0)
            {
                NetworkHandler.instance.SendProcess("8634#" + 3 + ";");
                DestroyImmediate(this.gameObject);
            }
            else
            {
                Debug.Log("3还没有编队");
            }
        }
    }

    private void CompulsorysoldierButtonOnclick() //强制补兵
    {
        if (CharacterRecorder.instance.MarinesTabe == 1) //点了按钮1
        {
            if (CharacterRecorder.instance.MarinesInfomation1.CityId != 0)
            {
                int num = 0;
                string[] dataSplit = CharacterRecorder.instance.MarinesInfomation1.BloodNumber.Split('!');
                for (int i = 0; i < dataSplit.Length - 1; i++) 
                {
                    string[] trcSplit = dataSplit[i].Split('$');
                    if (int.Parse(trcSplit[1]) < int.Parse(trcSplit[2])) 
                    {
                        num++;
                        break;
                    }
                }
                if (num > 0)
                {
                    SetCompulsorysoldierWindow(2);
                }
                else 
                {
                    UIManager.instance.OpenPromptWindow("士兵已满，无需补兵!", PromptWindow.PromptType.Hint, null, null);
                }
            }
            else
            {
                Debug.Log("1还没有编队");
            }
        }
        else if (CharacterRecorder.instance.MarinesTabe == 2) //点了按钮2
        {
            if (CharacterRecorder.instance.MarinesInfomation2.CityId != 0)
            {
                int num = 0;
                string[] dataSplit = CharacterRecorder.instance.MarinesInfomation2.BloodNumber.Split('!');
                for (int i = 0; i < dataSplit.Length - 1; i++)
                {
                    string[] trcSplit = dataSplit[i].Split('$');
                    if (int.Parse(trcSplit[1]) < int.Parse(trcSplit[2]))
                    {
                        num++;
                        break;
                    }
                }
                if (num > 0)
                {
                    SetCompulsorysoldierWindow(2);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("士兵已满，无需补兵!", PromptWindow.PromptType.Hint, null, null);
                }
            }
            else
            {
                Debug.Log("2还没有编队");
            }
        }
        else if (CharacterRecorder.instance.MarinesTabe == 3) //点了按钮2
        {
            if (CharacterRecorder.instance.MarinesInfomation3.CityId != 0)
            {
                int num = 0;
                string[] dataSplit = CharacterRecorder.instance.MarinesInfomation3.BloodNumber.Split('!');
                for (int i = 0; i < dataSplit.Length - 1; i++)
                {
                    string[] trcSplit = dataSplit[i].Split('$');
                    if (int.Parse(trcSplit[1]) < int.Parse(trcSplit[2]))
                    {
                        num++;
                        break;
                    }
                }
                if (num > 0)
                {
                    SetCompulsorysoldierWindow(2);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("士兵已满，无需补兵!", PromptWindow.PromptType.Hint, null, null);
                }
            }
            else
            {
                Debug.Log("3还没有编队");
            }
        }
    }



    private void AkeysupplementButtonOnclick() //一键补兵
    {
        if (CharacterRecorder.instance.MarinesTabe == 1) //点了按钮1
        {
            if (CharacterRecorder.instance.MarinesInfomation1.CityId != 0)
            {
                int num = 0;
                string[] dataSplit = CharacterRecorder.instance.MarinesInfomation1.BloodNumber.Split('!');
                for (int i = 0; i < dataSplit.Length - 1; i++)
                {
                    string[] trcSplit = dataSplit[i].Split('$');
                    if (int.Parse(trcSplit[1]) < int.Parse(trcSplit[2]))
                    {
                        num++;
                        break;
                    }
                }
                if (num > 0)
                {
                    SetCompulsorysoldierWindow(3);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("士兵已满，无需补兵!", PromptWindow.PromptType.Hint, null, null);
                }
            }
            else
            {
                Debug.Log("1还没有编队");
            }
        }
        else if (CharacterRecorder.instance.MarinesTabe == 2) //点了按钮2
        {
            if (CharacterRecorder.instance.MarinesInfomation2.CityId != 0)
            {
                int num = 0;
                string[] dataSplit = CharacterRecorder.instance.MarinesInfomation2.BloodNumber.Split('!');
                for (int i = 0; i < dataSplit.Length - 1; i++)
                {
                    string[] trcSplit = dataSplit[i].Split('$');
                    if (int.Parse(trcSplit[1]) < int.Parse(trcSplit[2]))
                    {
                        num++;
                        break;
                    }
                }
                if (num > 0)
                {
                    SetCompulsorysoldierWindow(3);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("士兵已满，无需补兵!", PromptWindow.PromptType.Hint, null, null);
                }
            }
            else
            {
                Debug.Log("2还没有编队");
            }
        }
        else if (CharacterRecorder.instance.MarinesTabe == 3) //点了按钮2
        {
            if (CharacterRecorder.instance.MarinesInfomation3.CityId != 0)
            {
                int num = 0;
                string[] dataSplit = CharacterRecorder.instance.MarinesInfomation3.BloodNumber.Split('!');
                for (int i = 0; i < dataSplit.Length - 1; i++)
                {
                    string[] trcSplit = dataSplit[i].Split('$');
                    if (int.Parse(trcSplit[1]) < int.Parse(trcSplit[2]))
                    {
                        num++;
                        break;
                    }
                }
                if (num > 0)
                {
                    SetCompulsorysoldierWindow(3);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("士兵已满，无需补兵!", PromptWindow.PromptType.Hint, null, null);
                }
            }
            else
            {
                Debug.Log("3还没有编队");
            }
        }
    }
    private void SetCompulsorysoldierWindow(int type)//1 重编  2，强制补兵 3，一键补兵 
    {
        CompulsorysoldierWindow.SetActive(true);
        CompulsorysoldierWindow.transform.Find("All/Item").GetComponent<BoxCollider>().enabled = false;
        if (type == 1) 
        {
            CompulsorysoldierWindow.transform.Find("All/TimeLabel").GetComponent<UILabel>().text = "重编需要消耗1点行动力";
            CompulsorysoldierWindow.transform.Find("All/Item/ItemIcon").GetComponent<UISprite>().spriteName = "90093";
            CompulsorysoldierWindow.transform.Find("All/Item/ItemIcon/LabelNumber").GetComponent<UILabel>().text = "x1";
        }
        else if (type == 2) 
        {
            CompulsorysoldierWindow.transform.Find("All/Item").GetComponent<BoxCollider>().enabled = true;
            CompulsorysoldierWindow.transform.Find("All/TimeLabel").GetComponent<UILabel>().text = "是否消耗6个复活石立即补满兵力";
            CompulsorysoldierWindow.transform.Find("All/Item/ItemIcon").GetComponent<UISprite>().spriteName = "10801";
            //CompulsorysoldierWindow.transform.Find("All/Item/ItemIcon/LabelNumber").GetComponent<UILabel>().text = "x6";
            TextTranslator.instance.ItemDescription(CompulsorysoldierWindow.transform.Find("All/Item").gameObject, 10801, 0);

            int num = TextTranslator.instance.GetItemCountByID(10801);
            if (num >= 6)
            {
                CompulsorysoldierWindow.transform.Find("All/Item/ItemIcon/LabelNumber").GetComponent<UILabel>().text = "[ffffff]x" + num.ToString() + "[-]";
            }
            else 
            {
                CompulsorysoldierWindow.transform.Find("All/Item/ItemIcon/LabelNumber").GetComponent<UILabel>().text = "[ff0000]x" + num.ToString() + "[-]";
            }
        }
        else if (type == 3) 
        {
            CompulsorysoldierWindow.transform.Find("All/TimeLabel").GetComponent<UILabel>().text = "是否消耗1点行动力立即补满兵力";
            CompulsorysoldierWindow.transform.Find("All/Item/ItemIcon").GetComponent<UISprite>().spriteName = "90093";
            CompulsorysoldierWindow.transform.Find("All/Item/ItemIcon/LabelNumber").GetComponent<UILabel>().text = "x1";
        }

        UIEventListener.Get(CompulsorySureButton).onClick = delegate(GameObject go)
        {
            if (type == 1) 
            {              
                if (CharacterRecorder.instance.LegionActionPoint > 0)
                {
                    CompulsorysoldierWindow.SetActive(false);
                    UIManager.instance.OpenSinglePanel("LegionPositionWindow", false);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("行动力不足!", PromptWindow.PromptType.Hint, null, null);
                }
            }
            else if (type == 2) 
            {
                if (TextTranslator.instance.GetItemCountByID(10801) >= 6)
                {
                    NetworkHandler.instance.SendProcess("8637#" + CharacterRecorder.instance.MarinesTabe + ";" + "2;");
                    CompulsorysoldierWindow.SetActive(false);
                }
                else
                {
                    NetworkHandler.instance.SendProcess("5012#10801;");//复活石购买;
                    Debug.Log("复活石数量不足");
                }

                //if (TextTranslator.instance.GetItemCountByID(10801) >= 6)
                //{
                //    NetworkHandler.instance.SendProcess("8637#" + CharacterRecorder.instance.MarinesTabe + ";" + "2;");
                //    CompulsorysoldierWindow.SetActive(false);
                //}
                //else
                //{
                //    UIManager.instance.OpenPromptWindow("复活石数量不足!", PromptWindow.PromptType.Hint, null, null);
                //    Debug.Log("复活石数量不足");
                //}
            }
            else if (type == 3) 
            {
                if (CharacterRecorder.instance.LegionActionPoint > 0)
                {
                    CompulsorysoldierWindow.SetActive(false);
                    NetworkHandler.instance.SendProcess("8637#" + CharacterRecorder.instance.MarinesTabe + ";" + "1;");
                }
                else 
                {
                    UIManager.instance.OpenPromptWindow("行动力不足!", PromptWindow.PromptType.Hint, null, null);
                }
            }
        };

        UIEventListener.Get(CompulsoryCancelButton).onClick = delegate(GameObject go)
        {
            CompulsorysoldierWindow.SetActive(false);
        };
        UIEventListener.Get(CompulsoryCloseButton).onClick = delegate(GameObject go)
        {
            CompulsorysoldierWindow.SetActive(false);
        };
    }
    public void LockOneTeamInfo(int num) 
    {
        for (int i = Uigrid.transform.childCount - 1; i >= 0; i--) 
        {
            DestroyImmediate(Uigrid.transform.GetChild(i).gameObject);
        }
        string[] BloodStr = null; 
        if (num == 1) 
        {
            BloodStr = CharacterRecorder.instance.MarinesInfomation1.BloodNumber.Split('!');
        }
        else if (num == 2) 
        {
            BloodStr = CharacterRecorder.instance.MarinesInfomation2.BloodNumber.Split('!');
        }
        else if (num == 3) 
        {
            BloodStr = CharacterRecorder.instance.MarinesInfomation3.BloodNumber.Split('!');
        }
        for (int i = 0; i < BloodStr.Length - 1; i++) 
        {
            string[] trcSplit = BloodStr[i].Split('$');
            GameObject go = NGUITools.AddChild(Uigrid, RoleItem);
            go.SetActive(true);
            Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(int.Parse(trcSplit[0]));
            HeroInfo mheroInfo = TextTranslator.instance.GetHeroInfoByHeroID(mhero.cardID);
            SetHeroType(mhero, mheroInfo, go, float.Parse(trcSplit[1]), float.Parse(trcSplit[2]));
        }
        //Uigrid.transform.parent.transform.localPosition = new Vector3(106f, 0, 0);
        //Uigrid.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2(0, 0);
        Uigrid.GetComponent<UIGrid>().Reposition();
        SetHeroEffect(num);
    }

    private void SetHeroEffect(int num) 
    {
        if (num == 1) 
        {
            TeamButton1.transform.Find("Select").gameObject.SetActive(true);
            TeamButton2.transform.Find("Select").gameObject.SetActive(false);
            TeamButton3.transform.Find("Select").gameObject.SetActive(false);
        }
        else if (num == 2)
        {
            TeamButton1.transform.Find("Select").gameObject.SetActive(false);
            TeamButton2.transform.Find("Select").gameObject.SetActive(true);
            TeamButton3.transform.Find("Select").gameObject.SetActive(false);
        }
        else 
        {
            TeamButton1.transform.Find("Select").gameObject.SetActive(false);
            TeamButton2.transform.Find("Select").gameObject.SetActive(false);
            TeamButton3.transform.Find("Select").gameObject.SetActive(true);
        }
    }


    public void SetTeamItemButtonInfo() //设置左边icon
    {
        MaIn1 = CharacterRecorder.instance.MarinesInfomation1;
        MaIn2 = CharacterRecorder.instance.MarinesInfomation2;
        MaIn3 = CharacterRecorder.instance.MarinesInfomation3;
        if (CharacterRecorder.instance.MarinesInfomation1.CityId != 0)
        {
            string[] trcSplit = CharacterRecorder.instance.MarinesInfomation1.BloodNumber.Split('!');
            string[] CaptainStr = trcSplit[5].Split('$');
            //HeroInfo HI=TextTranslator.instance.GetHeroInfoByHeroID(int.Parse(CaptainStr[0]));

            TeamButton1.transform.Find("HeroGrade").gameObject.SetActive(true);
            TeamButton1.transform.Find("TimeLabel").gameObject.SetActive(false);
            TeamButton1.transform.Find("AddSprite").gameObject.SetActive(false);

            Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(int.Parse(CaptainStr[0]));
            TeamButton1.transform.Find("HeroGrade/HeroIcon").GetComponent<UISprite>().spriteName = mhero.cardID.ToString();

            float allbloodnum = 0f;
            float allmaxbloodnum = 0f;

            for (int i = 0; i < trcSplit.Length - 1; i++)
            {
                string[] oneCaptainStr = trcSplit[i].Split('$');
                allbloodnum += float.Parse(oneCaptainStr[1]);
                allmaxbloodnum += float.Parse(oneCaptainStr[2]);
            }
            float bloodnum = allbloodnum / allmaxbloodnum;
            TeamButton1.transform.Find("HeroGrade/BloodNum").GetComponent<UILabel>().text = (bloodnum * 100).ToString("f2") + "%";
            TeamButton1.transform.Find("HeroGrade/ProgressBar").GetComponent<UISlider>().value = bloodnum;



            if (allbloodnum < allmaxbloodnum)
            {
                TeamButton1.transform.Find("HeroGrade/BloodSprite").gameObject.SetActive(true);
                float num = 103f * (1 - bloodnum);
                TeamButton1.transform.Find("HeroGrade/BloodSprite").GetComponent<UISprite>().height = (int)num;
            }
            else
            {
                TeamButton1.transform.Find("HeroGrade/BloodSprite").gameObject.SetActive(false);
            }
        }
        else if (CharacterRecorder.instance.MarinesInfomation1.CityId == 0 && CharacterRecorder.instance.MarinesInfomation1.timeNum > 0)
        {
            TeamButton1.transform.Find("HeroGrade").gameObject.SetActive(false);
            TeamButton1.transform.Find("TimeLabel").gameObject.SetActive(true);
            TeamButton1.transform.Find("AddSprite").gameObject.SetActive(false);
        }
        else
        {
            TeamButton1.transform.Find("HeroGrade").gameObject.SetActive(false);
            TeamButton1.transform.Find("TimeLabel").gameObject.SetActive(false);
            TeamButton1.transform.Find("AddSprite").gameObject.SetActive(true);
        }

        if (CharacterRecorder.instance.MarinesInfomation2.CityId != 0)
        {
            string[] trcSplit = CharacterRecorder.instance.MarinesInfomation2.BloodNumber.Split('!');
            string[] CaptainStr = trcSplit[5].Split('$');

            //HeroInfo HI = TextTranslator.instance.GetHeroInfoByHeroID(int.Parse(CaptainStr[0]));
            TeamButton2.transform.Find("HeroGrade").gameObject.SetActive(true);
            TeamButton2.transform.Find("TimeLabel").gameObject.SetActive(false);
            TeamButton2.transform.Find("AddSprite").gameObject.SetActive(false);

            Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(int.Parse(CaptainStr[0]));
            TeamButton2.transform.Find("HeroGrade/HeroIcon").GetComponent<UISprite>().spriteName = mhero.cardID.ToString();

            float allbloodnum = 0f;
            float allmaxbloodnum = 0f;

            for (int i = 0; i < trcSplit.Length - 1; i++)
            {
                string[] oneCaptainStr = trcSplit[i].Split('$');
                allbloodnum += float.Parse(oneCaptainStr[1]);
                allmaxbloodnum += float.Parse(oneCaptainStr[2]);
            }
            float bloodnum = allbloodnum / allmaxbloodnum;
            TeamButton2.transform.Find("HeroGrade/BloodNum").GetComponent<UILabel>().text = (bloodnum * 100).ToString("f2") + "%";
            TeamButton2.transform.Find("HeroGrade/ProgressBar").GetComponent<UISlider>().value = bloodnum;


            if (allbloodnum < allmaxbloodnum)
            {
                TeamButton2.transform.Find("HeroGrade/BloodSprite").gameObject.SetActive(true);
                float num = 103f * (1 - bloodnum);
                TeamButton2.transform.Find("HeroGrade/BloodSprite").GetComponent<UISprite>().height = (int)num;
            }
            else
            {
                TeamButton2.transform.Find("HeroGrade/BloodSprite").gameObject.SetActive(false);
            }
        }
        else if (CharacterRecorder.instance.MarinesInfomation2.CityId == 0 && CharacterRecorder.instance.MarinesInfomation2.timeNum > 0)
        {
            TeamButton2.transform.Find("HeroGrade").gameObject.SetActive(false);
            TeamButton2.transform.Find("TimeLabel").gameObject.SetActive(true);
            TeamButton2.transform.Find("AddSprite").gameObject.SetActive(false);
        }
        else
        {
            TeamButton2.transform.Find("HeroGrade").gameObject.SetActive(false);
            TeamButton2.transform.Find("TimeLabel").gameObject.SetActive(false);
            TeamButton2.transform.Find("AddSprite").gameObject.SetActive(true);
        }

        if (CharacterRecorder.instance.MarinesInfomation3.CityId != 0)
        {
            string[] trcSplit = CharacterRecorder.instance.MarinesInfomation3.BloodNumber.Split('!');
            string[] CaptainStr = trcSplit[5].Split('$');

            TeamButton3.transform.Find("HeroGrade").gameObject.SetActive(true);
            TeamButton3.transform.Find("TimeLabel").gameObject.SetActive(false);
            TeamButton3.transform.Find("AddSprite").gameObject.SetActive(false);

            Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(int.Parse(CaptainStr[0]));
            TeamButton3.transform.Find("HeroGrade/HeroIcon").GetComponent<UISprite>().spriteName = mhero.cardID.ToString();

            float allbloodnum = 0f;
            float allmaxbloodnum = 0f;

            for (int i = 0; i < trcSplit.Length - 1; i++)
            {
                string[] oneCaptainStr = trcSplit[i].Split('$');
                allbloodnum += float.Parse(oneCaptainStr[1]);
                allmaxbloodnum += float.Parse(oneCaptainStr[2]);
            }
            float bloodnum = allbloodnum / allmaxbloodnum;
            TeamButton3.transform.Find("HeroGrade/BloodNum").GetComponent<UILabel>().text = (bloodnum * 100).ToString("f2") + "%";
            TeamButton3.transform.Find("HeroGrade/ProgressBar").GetComponent<UISlider>().value = bloodnum;


            if (allbloodnum < allmaxbloodnum)
            {
                TeamButton3.transform.Find("HeroGrade/BloodSprite").gameObject.SetActive(true);
                float num = 103f * (1 - bloodnum);
                TeamButton3.transform.Find("HeroGrade/BloodSprite").GetComponent<UISprite>().height = (int)num;
            }
            else
            {
                TeamButton3.transform.Find("HeroGrade/BloodSprite").gameObject.SetActive(false);
            }
        }
        else if (CharacterRecorder.instance.MarinesInfomation3.CityId == 0 && CharacterRecorder.instance.MarinesInfomation3.timeNum > 0)
        {
            TeamButton3.transform.Find("HeroGrade").gameObject.SetActive(false);
            TeamButton3.transform.Find("TimeLabel").gameObject.SetActive(true);
            TeamButton3.transform.Find("AddSprite").gameObject.SetActive(false);
        }
        else
        {
            TeamButton3.transform.Find("HeroGrade").gameObject.SetActive(false);
            TeamButton3.transform.Find("TimeLabel").gameObject.SetActive(false);
            TeamButton3.transform.Find("AddSprite").gameObject.SetActive(true);
        }

        CancelInvoke();
        InvokeRepeating("UpdateTime", 0, 1.0f);
        LockOneTeamInfo(CharacterRecorder.instance.MarinesTabe);
    }

    void UpdateTime()
    {
        if (MaIn1.timeNum >= 0)
        {
            //string houreStr = (leftTime / 3600).ToString("00");
            string miniteStr = (MaIn1.timeNum % 3600 / 60).ToString("00");
            string secondStr = (MaIn1.timeNum % 60).ToString("00");
            TeamButton1.transform.Find("TimeLabel").GetComponent<UILabel>().text = string.Format("{0}:{1}", miniteStr, secondStr);
            //MaIn1.timeNum -= 1;
            if (MaIn1.timeNum == 0)
            {
                TeamButton1.transform.Find("TimeLabel").gameObject.SetActive(false);
                TeamButton1.transform.Find("AddSprite").gameObject.SetActive(true);
            }
        }

        if (MaIn2.timeNum >= 0)
        {
            //string houreStr = (leftTime / 3600).ToString("00");
            string miniteStr = (MaIn2.timeNum % 3600 / 60).ToString("00");
            string secondStr = (MaIn2.timeNum % 60).ToString("00");
            TeamButton2.transform.Find("TimeLabel").GetComponent<UILabel>().text = string.Format("{0}:{1}", miniteStr, secondStr);
            //MaIn2.timeNum -= 1;
            if (MaIn2.timeNum == 0)
            {
                TeamButton2.transform.Find("TimeLabel").gameObject.SetActive(false);
                TeamButton2.transform.Find("AddSprite").gameObject.SetActive(true);
            }
        }

        if (MaIn3.timeNum >= 0)
        {
            //string houreStr = (leftTime / 3600).ToString("00");
            string miniteStr = (MaIn3.timeNum % 3600 / 60).ToString("00");
            string secondStr = (MaIn3.timeNum % 60).ToString("00");
            TeamButton3.transform.Find("TimeLabel").GetComponent<UILabel>().text = string.Format("{0}:{1}", miniteStr, secondStr);
            //MaIn3.timeNum -= 1;
            if (MaIn3.timeNum == 0)
            {
                TeamButton3.transform.Find("TimeLabel").gameObject.SetActive(false);
                TeamButton3.transform.Find("AddSprite").gameObject.SetActive(true);
            }
        }
    }

    //设置英雄信息
    void SetHeroType(Hero mhero, HeroInfo mheroInfo, GameObject go, float NowbloodNum, float MaxbloodNum)
    {
        if (mheroInfo.heroCarrerType == 1 || mheroInfo.heroCarrerType == 2 || mheroInfo.heroCarrerType == 3)
        {
            go.transform.Find("HeroType").GetComponent<UISprite>().spriteName = string.Format("heroType{0}", mheroInfo.heroCarrerType);
            //go.transform.Find("HeroType/HeroRarity").GetComponent<UISprite>().spriteName = "word" + (mheroInfo.heroRarity + 3);
        }
        else
        {
            Debug.LogError("英雄职业类型参数错误" + mheroInfo.heroCarrerType);
        }
        go.transform.Find("HeroLogo/Sprite").GetComponent<UISprite>().spriteName = mheroInfo.heroID.ToString();
        go.transform.Find("HeroNameLabel").GetComponent<UILabel>().text = mheroInfo.heroName;
        go.transform.Find("SpriteFight/LabelFight").GetComponent<UILabel>().text = mhero.force.ToString();
        Hero myHero = CharacterRecorder.instance.GetHeroByRoleID(mheroInfo.heroID);
        TextTranslator.instance.SetHeroNameColor(go.transform.Find("HeroLogo").GetComponent<UISprite>(), myHero.classNumber);

        float bloodnum = NowbloodNum / MaxbloodNum;

        go.transform.Find("ProgressBar").GetComponent<UISlider>().value = bloodnum;
        go.transform.Find("ProgressBar/Label").GetComponent<UILabel>().text = (bloodnum * 100).ToString("f2") + "%";
    }
}
