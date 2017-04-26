using UnityEngine;
using System.Collections;
using System.Text;
using CodeStage.AntiCheat.ObscuredTypes;

public class KingRoadWindow : MonoBehaviour {

    public GameObject ChallengeTab;
    public GameObject KingRoadTab;
    public GameObject RankTab;
    public GameObject KingShopTab;

    public GameObject Button1;
    public GameObject Button2;
    public GameObject Button3;
    public GameObject Button4;

    public GameObject KuangBg1;
    public GameObject KunagBg2;
    //1
    public GameObject ChallengeItem;
    public GameObject ChallengeUigrid;
    public GameObject MessageItem;
    public GameObject ChallengeRightUigrid;
    public UILabel LeftTime;
    public UILabel GroupLabel;
    public UILabel ChallengeNumber;
    public GameObject AddButton;
    public GameObject ManInfoLabel;

    //2
    public GameObject[] DanButtonArr = new GameObject[6];
    public GameObject[] AwardArr = new GameObject[7];

    //3
    public GameObject RankItem;
    public GameObject RankUigrid;

    //4
    public GameObject HunterMessage;
    public GameObject QuestionButton;

    public GameObject ChangeResultWindow;
    public GameObject BackButton;
    public GameObject EffectGuang;
    public GameObject EffectTZCG;



    private int kingNum;//段位
    private int CostDaimond = 0;
	void Start () {
        if (UIEventListener.Get(Button1).onClick == null) 
        {
            UIEventListener.Get(Button1).onClick += delegate(GameObject go)
            {
                KuangBg1.SetActive(true);
                KunagBg2.SetActive(false);
                ChallengeTab.SetActive(true);
                KingRoadTab.SetActive(false);
                RankTab.SetActive(false);
                KingShopTab.SetActive(false);
                NetworkHandler.instance.SendProcess("6401#;");
            };
        }
        if (UIEventListener.Get(Button2).onClick == null)
        {
            UIEventListener.Get(Button2).onClick += delegate(GameObject go)
            {
                KuangBg1.SetActive(false);
                KunagBg2.SetActive(true);
                ChallengeTab.SetActive(false);
                KingRoadTab.SetActive(true);
                RankTab.SetActive(false);
                KingShopTab.SetActive(false);
                GetKingRankBonus();
            };
        }
        if (UIEventListener.Get(Button3).onClick == null)
        {
            UIEventListener.Get(Button3).onClick += delegate(GameObject go)
            {
                KuangBg1.SetActive(false);
                KunagBg2.SetActive(true);
                ChallengeTab.SetActive(false);
                KingRoadTab.SetActive(false);
                RankTab.SetActive(true);
                KingShopTab.SetActive(false);
                NetworkHandler.instance.SendProcess("6402#;");
            };
        }
        if (UIEventListener.Get(Button4).onClick == null)
        {
            UIEventListener.Get(Button4).onClick += delegate(GameObject go)
            {
                KuangBg1.SetActive(false);
                KunagBg2.SetActive(true);
                ChallengeTab.SetActive(false);
                KingRoadTab.SetActive(false);
                RankTab.SetActive(false);
                KingShopTab.SetActive(true);
                CharacterRecorder.instance.RankShopType = 10007;
                UIManager.instance.OpenPanel("RankShopWindow", true);
            };
        }

        if (UIEventListener.Get(AddButton).onClick == null)
        {
            UIEventListener.Get(AddButton).onClick += delegate(GameObject go)
            {
                //NetworkHandler.instance.SendProcess("6404#;");
                if (CharacterRecorder.instance.lunaGem < CostDaimond)
                {
                    UIManager.instance.OpenPromptWindow("钻石不足", PromptWindow.PromptType.Hint, null, null);
                }
                else 
                {
                    UIManager.instance.OpenPromptWindow("是否花费" + CostDaimond + "钻石购买次数", PromptWindow.PromptType.Confirm, ResetBtnClick, null);
                }
            };
        }
        if (UIEventListener.Get(QuestionButton).onClick == null)
        {
            UIEventListener.Get(QuestionButton).onClick += delegate(GameObject go)
            {
                HunterMessage.SetActive(true);
                if (UIEventListener.Get(HunterMessage.transform.Find("CloseButton").gameObject).onClick == null)
                {
                    UIEventListener.Get(HunterMessage.transform.Find("CloseButton").gameObject).onClick += delegate(GameObject other)
                    {
                        HunterMessage.SetActive(false);
                    };
                }
            };
        }

        Button1.GetComponent<UIToggle>().value = true;
        Button1.GetComponent<UIToggle>().startsActive = true;
        KuangBg1.SetActive(true);
        KunagBg2.SetActive(false);
        ChallengeTab.SetActive(true);
        KingRoadTab.SetActive(false);
        RankTab.SetActive(false);
        KingShopTab.SetActive(false);
        NetworkHandler.instance.SendProcess("6401#;");
	}
    void ResetBtnClick() 
    {
        NetworkHandler.instance.SendProcess("6404#;");
    }
    public void GetKingInfo(int _kingRank, int _challengeNum, int _BuyCopyCount,int _residueTime) 
    {
        ManInfoLabel.SetActive(true);
        this.kingNum = _kingRank;
        ChallengeNumber.text = _challengeNum.ToString();
        string dayStr = (_residueTime / 86400).ToString();
        string houreStr = (_residueTime % 86400 / 3600).ToString();
        string miniteStr = (_residueTime % 3600/60).ToString();
        LeftTime.text = dayStr + "天" + houreStr + "时" + miniteStr + "分";
        CharacterRecorder.instance.KingRank = _kingRank;
        switch (_kingRank)
        {
            case 1:
                GroupLabel.text = "青铜组";
                break;
            case 2:
                GroupLabel.text = "白银组";
                break;
            case 3:
                GroupLabel.text = "黄金组";
                break;
            case 4:
                GroupLabel.text = "铂金组";
                break;
            case 5:
                GroupLabel.text = "钻石组";
                break;
            case 6:
                GroupLabel.text = "王者组";
                break;
            default:
                break;
        }
    }
    public void GetKingInfo(int _kingRank, int _challengeNum, int _BuyCopyCount,int _residueTime, string _rankInfo, string _battleInfo) //6401
    {
        this.kingNum = _kingRank;
        ManInfoLabel.SetActive(false);
        CostDaimond = TextTranslator.instance.GetMarketByID(TextTranslator.instance.GetMarketIDByBuyCount(_BuyCopyCount + 1)).KingRoadFightDiamond;
        //ChallengeUigrid.transform.parent.localPosition = new Vector3(-190, -113, 0);
        //ChallengeUigrid.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2(0, 0);
        //ChallengeUigrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        for (int i = ChallengeUigrid.transform.childCount - 1; i >= 0; i--) 
        {
            DestroyImmediate(ChallengeUigrid.transform.GetChild(i).gameObject);
        }
        CharacterRecorder.instance.KingRank = _kingRank;
        switch (_kingRank)
        {
            case 1:
                GroupLabel.text = "青铜组";
                break;
            case 2:
                GroupLabel.text = "白银组";
                break;
            case 3:
                GroupLabel.text = "黄金组";
                break;
            case 4:
                GroupLabel.text = "铂金组";
                break;
            case 5:
                GroupLabel.text = "钻石组";
                break;
            case 6:
                GroupLabel.text = "王者组";
                break;
            default:
                break;
        }

        ChallengeNumber.text = _challengeNum.ToString();

        string dayStr = (_residueTime / 86400).ToString();
        string houreStr = (_residueTime % 86400 / 3600).ToString();
        string miniteStr = (_residueTime % 3600 / 60).ToString();
        LeftTime.text = dayStr + "天" + houreStr + "时" + miniteStr + "分";

        string[] RankSplit = _rankInfo.Split('!');
        int MyRank = 0; //int.Parse(RankSplit[RankSplit.Length - 2].Split('$')[0]); //我的排名
        int RobotNum = 0;//机器人数量
        int LeapfrogFightRank = 0;
        int PositionNum = 0;
        for (int i = 0; i < RankSplit.Length - 1; i++)
        {
            string[] trcSplit = RankSplit[i].Split('$');
            if (CharacterRecorder.instance.characterName == trcSplit[2])
            {
                PositionNum = i + 1;
                MyRank = int.Parse(trcSplit[0]); //我的排名
                CharacterRecorder.instance.KingMyNum = MyRank;
                LeapfrogFightRank = TextTranslator.instance.GetKingRoadByID(_kingRank).LeapforgFightRank;//可以挑战的位数
                RobotNum = TextTranslator.instance.GetKingRoadByID(_kingRank).RobotNum;
                break;
            }
        }

        //int LeapfrogFightRank = TextTranslator.instance.GetKingRoadByID(_kingRank).LeapforgFightRank;//可以挑战的位数

        for (int i = 0; i < RankSplit.Length - 1; i++) 
        {
            string[] trcSplit=RankSplit[i].Split('$');
            GameObject go = NGUITools.AddChild(ChallengeUigrid, ChallengeItem);
            go.SetActive(true);
            if (trcSplit[0]=="1") 
            {
                go.transform.Find("RankLabel").gameObject.SetActive(false);
                go.transform.Find("RankSprite").gameObject.SetActive(true);
                go.transform.Find("RankSprite").GetComponent<UISprite>().spriteName = "word1";
            }
            else if (trcSplit[0] == "2") 
            {
                go.transform.Find("RankLabel").gameObject.SetActive(false);
                go.transform.Find("RankSprite").gameObject.SetActive(true);
                go.transform.Find("RankSprite").GetComponent<UISprite>().spriteName = "word2";
            }
            else if (trcSplit[0] == "3")
            {
                go.transform.Find("RankLabel").gameObject.SetActive(false);
                go.transform.Find("RankSprite").gameObject.SetActive(true);
                go.transform.Find("RankSprite").GetComponent<UISprite>().spriteName = "word3";
            }
            else 
            {
                go.transform.Find("RankLabel").gameObject.SetActive(true);
                go.transform.Find("RankSprite").gameObject.SetActive(false);
                go.transform.Find("RankLabel").GetComponent<UILabel>().text = trcSplit[0];
            }
            go.transform.Find("Name").GetComponent<UILabel>().text = trcSplit[2];
            //go.transform.Find("Servicer").GetComponent<UILabel>().text = trcSplit[4];
            go.transform.Find("Fight").GetComponent<UILabel>().text = trcSplit[3];

            string servername = ObscuredPrefs.GetString("ObServerName");
            string str = servername.Replace(" ", ",");
            string[] qrcSplit = str.Split(',');
            go.transform.Find("Servicer").GetComponent<UILabel>().text = qrcSplit[0];



            //ASCIIEncoding ascii = new ASCIIEncoding();
            //byte[] bytestr = ascii.GetBytes(trcSplit[4]);
            //if (bytestr.Length >= 3)
            //{
            //    if (bytestr[bytestr.Length - 1] > 48 && bytestr[bytestr.Length - 1] < 57 && bytestr[bytestr.Length - 2] > 48 && bytestr[bytestr.Length - 2] < 57 && bytestr[bytestr.Length - 3] > 48 && bytestr[bytestr.Length - 3] < 57)
            //    {
            //        go.transform.Find("Servicer").GetComponent<UILabel>().text = trcSplit[4][trcSplit[4].Length - 3]+trcSplit[4][trcSplit[4].Length - 2] + trcSplit[4][trcSplit[4].Length - 1] + "服";
            //    }
            //    else if (bytestr[bytestr.Length - 1] > 48 && bytestr[bytestr.Length - 1] < 57 && bytestr[bytestr.Length - 2] > 48 && bytestr[bytestr.Length - 2] < 57)
            //    {
            //        go.transform.Find("Servicer").GetComponent<UILabel>().text = trcSplit[4][trcSplit[4].Length - 2] + trcSplit[4][trcSplit[4].Length - 1] + "服";
            //    }
            //    else if (bytestr[bytestr.Length - 1] > 48 && bytestr[bytestr.Length - 1] < 57)
            //    {
            //        go.transform.Find("Servicer").GetComponent<UILabel>().text = trcSplit[4][trcSplit[4].Length - 1] + "服";
            //    }
            //    else
            //    {
            //        go.transform.Find("Servicer").GetComponent<UILabel>().text = "1服";//trcSplit[4];
            //    }
            //}
            //else 
            //{
            //    go.transform.Find("Servicer").GetComponent<UILabel>().text = "1服";//trcSplit[4];
            //}



            if (CharacterRecorder.instance.characterName == trcSplit[2])
            {
                if (MyRank <= RobotNum)
                {
                    go.transform.Find("ChallengeButton").gameObject.SetActive(false);
                }
                else 
                {
                    DestroyImmediate(go);
                }
            }
            else
            {
                if (MyRank <= RobotNum) //是否在排名内
                {
                    if (MyRank - int.Parse(trcSplit[0]) <= LeapfrogFightRank)
                    {
                        go.transform.Find("ChallengeButton").GetComponent<UISprite>().spriteName = "ui2_button4";
                        go.transform.Find("ChallengeButton").transform.Find("yelloLabel").gameObject.SetActive(true);
                        go.transform.Find("ChallengeButton").transform.Find("huiLabel").gameObject.SetActive(false);
                        UIEventListener.Get(go.transform.Find("ChallengeButton").gameObject).onClick = delegate(GameObject obj)
                        {
                            //UIManager.instance.OpenPanel("KingRoadFightWindow", true);
                            if (ChallengeNumber.text != "0")
                            {
                                CharacterRecorder.instance.KingServer = trcSplit[4];
                                CharacterRecorder.instance.KingEnemyID = int.Parse(trcSplit[1]);
                                CharacterRecorder.instance.KingEnemyNum = int.Parse(trcSplit[0]);

                                if (CharacterRecorder.instance.Fight > int.Parse(trcSplit[3]))
                                {
                                    NetworkHandler.instance.SendProcess("6405#" + CharacterRecorder.instance.KingServer + ";" + CharacterRecorder.instance.KingEnemyID + ";" + 1 + ";");                                 
                                }
                                else 
                                {
                                    NetworkHandler.instance.SendProcess("6403#" + trcSplit[4] + ";" + trcSplit[1] + ";");
                                }
                            }
                            else
                            {
                                UIManager.instance.OpenPromptWindow("无挑战次数", PromptWindow.PromptType.Hint, null, null);
                            }

                        };
                    }
                    else
                    {
                        go.transform.Find("ChallengeButton").GetComponent<UISprite>().spriteName = "buttonHui";
                        go.transform.Find("ChallengeButton").transform.Find("yelloLabel").gameObject.SetActive(false);
                        go.transform.Find("ChallengeButton").transform.Find("huiLabel").gameObject.SetActive(true);
                        UIEventListener.Get(go.transform.Find("ChallengeButton").gameObject).onClick = delegate(GameObject obj)
                        {
                            UIManager.instance.OpenPromptWindow("段位不足,无法挑战", PromptWindow.PromptType.Hint, null, null);
                        };
                    }
                }
                else 
                {
                    if (int.Parse(trcSplit[0]) +LeapfrogFightRank>RobotNum)
                    {
                        go.transform.Find("ChallengeButton").GetComponent<UISprite>().spriteName = "ui2_button4";
                        go.transform.Find("ChallengeButton").transform.Find("yelloLabel").gameObject.SetActive(true);
                        go.transform.Find("ChallengeButton").transform.Find("huiLabel").gameObject.SetActive(false);
                        UIEventListener.Get(go.transform.Find("ChallengeButton").gameObject).onClick = delegate(GameObject obj)
                        {
                            //UIManager.instance.OpenPanel("KingRoadFightWindow", true);
                            if (ChallengeNumber.text != "0")
                            {
                                CharacterRecorder.instance.KingServer = trcSplit[4];
                                CharacterRecorder.instance.KingEnemyID = int.Parse(trcSplit[1]);
                                CharacterRecorder.instance.KingEnemyNum = int.Parse(trcSplit[0]);
                                if (CharacterRecorder.instance.Fight > int.Parse(trcSplit[3]))
                                {
                                    NetworkHandler.instance.SendProcess("6405#" + CharacterRecorder.instance.KingServer + ";" + CharacterRecorder.instance.KingEnemyID + ";" + 1 + ";");
                                }
                                else
                                {
                                    NetworkHandler.instance.SendProcess("6403#" + trcSplit[4] + ";" + trcSplit[1] + ";");
                                }
                            }
                            else
                            {
                                UIManager.instance.OpenPromptWindow("无挑战次数", PromptWindow.PromptType.Hint, null, null);
                            }

                        };
                    }
                    else
                    {
                        go.transform.Find("ChallengeButton").GetComponent<UISprite>().spriteName = "buttonHui";
                        go.transform.Find("ChallengeButton").transform.Find("yelloLabel").gameObject.SetActive(false);
                        go.transform.Find("ChallengeButton").transform.Find("huiLabel").gameObject.SetActive(true);
                        UIEventListener.Get(go.transform.Find("ChallengeButton").gameObject).onClick = delegate(GameObject obj)
                        {
                            UIManager.instance.OpenPromptWindow("段位不足,无法挑战", PromptWindow.PromptType.Hint, null, null);
                        };
                    }
                }
            }
        }
        ChallengeUigrid.GetComponent<UIGrid>().Reposition();

        if (MyRank <= RobotNum)  //定位
        {
            if (PositionNum - 4 > 0)
            {
                ChallengeUigrid.transform.parent.localPosition = new Vector3(-190f, -113 + (PositionNum - 4) * 95f + 30f, 0);
                ChallengeUigrid.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2(0, -(PositionNum - 4) * 95f - 30f);
            }
            else
            {
                ChallengeUigrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
            }
        }
        else 
        {
            PositionNum = ChallengeUigrid.transform.childCount;
            if (PositionNum - 4 > 0)
            {
                ChallengeUigrid.transform.parent.localPosition = new Vector3(-190f, -113 + (PositionNum - 4) * 95f + 30f, 0);
                ChallengeUigrid.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2(0, -(PositionNum - 4) * 95f - 30f);
            }
            else
            {
                ChallengeUigrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
            }
        }

        if (_battleInfo != "") //战报
        {
            ChallengeRightUigrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
            for (int i = ChallengeRightUigrid.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(ChallengeRightUigrid.transform.GetChild(i).gameObject);
            }
            string[] BattleSplit = _battleInfo.Split('!');
            for (int i = BattleSplit.Length - 2;i>=0; i--)
            {
                string[] trcSplit = BattleSplit[i].Split('$');
                if (trcSplit[3] != "0")
                {
                    GameObject go = NGUITools.AddChild(ChallengeRightUigrid, MessageItem);
                    go.SetActive(true);
                    if (trcSplit[2] == "1")
                    {
                        go.GetComponent<UILabel>().text = trcSplit[0] + "挑战战队" + trcSplit[1] + "胜利,排名上升" + trcSplit[3] + "名";
                    }
                    else
                    {
                        go.GetComponent<UILabel>().text = trcSplit[0] + "挑战战队" + trcSplit[1] + "失败,排名下降" + trcSplit[3] + "名";
                    }
                }
                else 
                {
                    GameObject go = NGUITools.AddChild(ChallengeRightUigrid, MessageItem);
                    go.SetActive(true);
                    if (trcSplit[2] == "1")
                    {
                        go.GetComponent<UILabel>().text = trcSplit[0] + "挑战战队" + trcSplit[1] + "胜利";
                    }
                    else
                    {
                        go.GetComponent<UILabel>().text = trcSplit[0] + "挑战战队" + trcSplit[1] + "失败";
                    }
                }
            }
            ChallengeRightUigrid.GetComponent<UIGrid>().Reposition();
        }
    }


    private void GetKingRankBonus() //王者之路
    {
        for (int i = 0; i < 6; i++)
        {
            int j = i + 1;
            UIEventListener.Get(DanButtonArr[i]).onClick = delegate(GameObject go)
            {
                SetOneKingRankBonus(j);
            };
        }

        for (int i = 0; i < DanButtonArr.Length; i++) 
        {
            if ((kingNum - 1) == i)
            {
                DanButtonArr[kingNum - 1].GetComponent<UIToggle>().value = true;
                DanButtonArr[kingNum - 1].GetComponent<UIToggle>().startsActive = true;
            }
            else 
            {
                DanButtonArr[i].GetComponent<UIToggle>().value = false;
                DanButtonArr[i].GetComponent<UIToggle>().startsActive = false;
            }
        }
        SetOneKingRankBonus(kingNum);
    }

    private void SetOneKingRankBonus(int num)
    {
        Debug.Log("num" + num);
        KingRoad _kingRoad = TextTranslator.instance.GetKingRoadByID(num);
        for (int i = 0; i < 7; i++)
        {
            switch (i + 1)
            {
                case 1:
                    string[] trcSplit1 = _kingRoad.RankScope1.Split('$');
                    if (trcSplit1[0] != trcSplit1[1])
                    {
                        AwardArr[6].transform.Find("topLabel").GetComponent<UILabel>().text = "第"+trcSplit1[0] + "-" + trcSplit1[1]+"名";
                    }
                    else 
                    {
                        AwardArr[6].transform.Find("topLabel").GetComponent<UILabel>().text = "第" + trcSplit1[0] + "名";
                    }
                    AwardArr[6].transform.Find("Item1").transform.Find("Icon").GetComponent<UISprite>().spriteName = _kingRoad.Rank1Bonus1.ToString();
                    AwardArr[6].transform.Find("Item1").transform.Find("Number").GetComponent<UILabel>().text = _kingRoad.Rank1BonusNum1.ToString();
                    AwardArr[6].transform.Find("Item2").transform.Find("Icon").GetComponent<UISprite>().spriteName = _kingRoad.Rank1Bonus2.ToString();
                    AwardArr[6].transform.Find("Item2").transform.Find("Number").GetComponent<UILabel>().text = _kingRoad.Rank1BonusNum2.ToString();
                    break;
                case 2:
                    string[] trcSplit2 = _kingRoad.RankScope2.Split('$');
                    if (trcSplit2[0] != trcSplit2[1])
                    {
                        AwardArr[5].transform.Find("topLabel").GetComponent<UILabel>().text = "第" + trcSplit2[0] + "-" + trcSplit2[1] + "名";
                    }
                    else
                    {
                        AwardArr[5].transform.Find("topLabel").GetComponent<UILabel>().text = "第" + trcSplit2[0] + "名";
                    }
                    AwardArr[5].transform.Find("Item1").transform.Find("Icon").GetComponent<UISprite>().spriteName = _kingRoad.Rank2Bonus1.ToString();
                    AwardArr[5].transform.Find("Item1").transform.Find("Number").GetComponent<UILabel>().text = _kingRoad.Rank2BonusNum1.ToString();
                    AwardArr[5].transform.Find("Item2").transform.Find("Icon").GetComponent<UISprite>().spriteName = _kingRoad.Rank2Bonus2.ToString();
                    AwardArr[5].transform.Find("Item2").transform.Find("Number").GetComponent<UILabel>().text = _kingRoad.Rank2BonusNum2.ToString();
                    break;
                case 3:
                    string[] trcSplit3 = _kingRoad.RankScope3.Split('$');
                    if (trcSplit3[0] != trcSplit3[1])
                    {
                        AwardArr[4].transform.Find("topLabel").GetComponent<UILabel>().text = "第" + trcSplit3[0] + "-" + trcSplit3[1] + "名";
                    }
                    else
                    {
                        AwardArr[4].transform.Find("topLabel").GetComponent<UILabel>().text = "第" + trcSplit3[0] + "名";
                    }
                    AwardArr[4].transform.Find("Item1").transform.Find("Icon").GetComponent<UISprite>().spriteName = _kingRoad.Rank3Bonus1.ToString();
                    AwardArr[4].transform.Find("Item1").transform.Find("Number").GetComponent<UILabel>().text = _kingRoad.Rank3BonusNum1.ToString();
                    AwardArr[4].transform.Find("Item2").transform.Find("Icon").GetComponent<UISprite>().spriteName = _kingRoad.Rank3Bonus2.ToString();
                    AwardArr[4].transform.Find("Item2").transform.Find("Number").GetComponent<UILabel>().text = _kingRoad.Rank3BonusNum2.ToString();
                    break;
                case 4:
                    string[] trcSplit4 = _kingRoad.RankScope4.Split('$');
                    if (trcSplit4[0] != trcSplit4[1])
                    {
                        AwardArr[3].transform.Find("topLabel").GetComponent<UILabel>().text = "第" + trcSplit4[0] + "-" + trcSplit4[1] + "名";
                    }
                    else
                    {
                        AwardArr[3].transform.Find("topLabel").GetComponent<UILabel>().text = "第" + trcSplit4[0] + "名";
                    }
                    AwardArr[3].transform.Find("Item1").transform.Find("Icon").GetComponent<UISprite>().spriteName = _kingRoad.Rank4Bonus1.ToString();
                    AwardArr[3].transform.Find("Item1").transform.Find("Number").GetComponent<UILabel>().text = _kingRoad.Rank4BonusNum1.ToString();
                    AwardArr[3].transform.Find("Item2").transform.Find("Icon").GetComponent<UISprite>().spriteName = _kingRoad.Rank4Bonus2.ToString();
                    AwardArr[3].transform.Find("Item2").transform.Find("Number").GetComponent<UILabel>().text = _kingRoad.Rank4BonusNum2.ToString();
                    break;
                case 5:
                    string[] trcSplit5 = _kingRoad.RankScope5.Split('$');
                    if (trcSplit5[0] != trcSplit5[1])
                    {
                        AwardArr[2].transform.Find("topLabel").GetComponent<UILabel>().text = "第" + trcSplit5[0] + "-" + trcSplit5[1] + "名";
                    }
                    else
                    {
                        AwardArr[2].transform.Find("topLabel").GetComponent<UILabel>().text = "第" + trcSplit5[0] + "名";
                    }
                    AwardArr[2].transform.Find("Item1").transform.Find("Icon").GetComponent<UISprite>().spriteName = _kingRoad.Rank5Bonus1.ToString();
                    AwardArr[2].transform.Find("Item1").transform.Find("Number").GetComponent<UILabel>().text = _kingRoad.Rank5BonusNum1.ToString();
                    AwardArr[2].transform.Find("Item2").transform.Find("Icon").GetComponent<UISprite>().spriteName = _kingRoad.Rank5Bonus2.ToString();
                    AwardArr[2].transform.Find("Item2").transform.Find("Number").GetComponent<UILabel>().text = _kingRoad.Rank5BonusNum2.ToString();
                    break;
                case 6:
                    string[] trcSplit6 = _kingRoad.RankScope6.Split('$');
                    if (trcSplit6[0] != trcSplit6[1])
                    {
                        AwardArr[1].transform.Find("topLabel").GetComponent<UILabel>().text = "第" + trcSplit6[0] + "-" + trcSplit6[1] + "名";
                    }
                    else
                    {
                        AwardArr[1].transform.Find("topLabel").GetComponent<UILabel>().text = "第" + trcSplit6[0] + "名";
                    }
                    AwardArr[1].transform.Find("Item1").transform.Find("Icon").GetComponent<UISprite>().spriteName = _kingRoad.Rank6Bonus1.ToString();
                    AwardArr[1].transform.Find("Item1").transform.Find("Number").GetComponent<UILabel>().text = _kingRoad.Rank6BonusNum1.ToString();
                    AwardArr[1].transform.Find("Item2").transform.Find("Icon").GetComponent<UISprite>().spriteName = _kingRoad.Rank6Bonus2.ToString();
                    AwardArr[1].transform.Find("Item2").transform.Find("Number").GetComponent<UILabel>().text = _kingRoad.Rank6BonusNum2.ToString();
                    break;
                case 7:
                    string[] trcSplit7 = _kingRoad.RankScope7.Split('$');
                    if (trcSplit7[0] != trcSplit7[1])
                    {
                        AwardArr[0].transform.Find("topLabel").GetComponent<UILabel>().text = "第" + trcSplit7[0] + "-" + trcSplit7[1] + "名";
                    }
                    else
                    {
                        AwardArr[0].transform.Find("topLabel").GetComponent<UILabel>().text = "第" + trcSplit7[0] + "名";
                    }
                    AwardArr[0].transform.Find("Item1").transform.Find("Icon").GetComponent<UISprite>().spriteName = _kingRoad.Rank7Bonus1.ToString();
                    AwardArr[0].transform.Find("Item1").transform.Find("Number").GetComponent<UILabel>().text = _kingRoad.Rank7BonusNum1.ToString();
                    AwardArr[0].transform.Find("Item2").transform.Find("Icon").GetComponent<UISprite>().spriteName = _kingRoad.Rank7Bonus2.ToString();
                    AwardArr[0].transform.Find("Item2").transform.Find("Number").GetComponent<UILabel>().text = _kingRoad.Rank7BonusNum2.ToString();
                    break;
            }
        }
    }

    public void GetKingRank(string Recving) //6402 
    {
        string[] dataSplit = Recving.Split(';');
        RankUigrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        for (int i = RankUigrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(RankUigrid.transform.GetChild(i).gameObject);
        }

        string[] trcSplit = dataSplit[0].Split('!');
        for (int i = 0; i < trcSplit.Length - 1; i++) 
        {
            string[] prcSplit = trcSplit[i].Split('$');

            GameObject go = NGUITools.AddChild(RankUigrid, RankItem);
            go.SetActive(true);
            if (prcSplit[0] == "1")
            {
                go.transform.Find("RankNumber").gameObject.SetActive(false);
                go.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite").GetComponent<UISprite>().spriteName = "u32_icon4";
                go.transform.Find("LabelRankSprite").transform.Find("RankSprite").GetComponent<UISprite>().spriteName = "word1";
            }
            else if (prcSplit[0] == "2")
            {
                go.transform.Find("RankNumber").gameObject.SetActive(false);
                go.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite").GetComponent<UISprite>().spriteName = "u32_icon3";
                go.transform.Find("LabelRankSprite").transform.Find("RankSprite").GetComponent<UISprite>().spriteName = "word2";
            }
            else if (prcSplit[0] == "3")
            {
                go.transform.Find("RankNumber").gameObject.SetActive(false);
                go.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite").GetComponent<UISprite>().spriteName = "u32_icon5";
                go.transform.Find("LabelRankSprite").transform.Find("RankSprite").GetComponent<UISprite>().spriteName = "word3";
            }
            else
            {
                go.transform.Find("RankNumber").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite").gameObject.SetActive(false);
                go.transform.Find("RankNumber").GetComponent<UILabel>().text = prcSplit[0];
            }
            go.transform.Find("RankName").GetComponent<UILabel>().text = prcSplit[2];
            //go.transform.Find("Servicer").GetComponent<UILabel>().text = prcSplit[4];

            string servername = ObscuredPrefs.GetString("ObServerName");
            string str = servername.Replace(" ", ",");
            string[] qrcSplit = str.Split(',');
            go.transform.Find("Servicer").GetComponent<UILabel>().text = qrcSplit[0];

            //ASCIIEncoding ascii = new ASCIIEncoding();
            //byte[] bytestr = ascii.GetBytes(prcSplit[4]);
            //if (bytestr.Length >= 3)
            //{
            //    if (bytestr[bytestr.Length - 1] > 48 && bytestr[bytestr.Length - 1] < 57 && bytestr[bytestr.Length - 2] > 48 && bytestr[bytestr.Length - 2] < 57 && bytestr[bytestr.Length - 3] > 48 && bytestr[bytestr.Length - 3] < 57)
            //    {
            //        go.transform.Find("Servicer").GetComponent<UILabel>().text = prcSplit[4][prcSplit[4].Length - 3] + prcSplit[4][prcSplit[4].Length - 2] + prcSplit[4][prcSplit[4].Length - 1] + "服";
            //    }
            //    else if (bytestr[bytestr.Length - 1] > 48 && bytestr[bytestr.Length - 1] < 57 && bytestr[bytestr.Length - 2] > 48 && bytestr[bytestr.Length - 2] < 57)
            //    {
            //        go.transform.Find("Servicer").GetComponent<UILabel>().text =prcSplit[4][prcSplit[4].Length - 2] + prcSplit[4][prcSplit[4].Length - 1] + "服";
            //    }
            //    else if (bytestr[bytestr.Length - 1] > 48 && bytestr[bytestr.Length - 1] < 57)
            //    {
            //        go.transform.Find("Servicer").GetComponent<UILabel>().text =prcSplit[4][prcSplit[4].Length - 1] + "服";
            //    }
            //    else
            //    {
            //        go.transform.Find("Servicer").GetComponent<UILabel>().text = "1服";//prcSplit[4];
            //    }
            //}
            //else
            //{
            //    go.transform.Find("Servicer").GetComponent<UILabel>().text = "1服";//prcSplit[4];
            //}

            if (CharacterRecorder.instance.characterName == prcSplit[2])
            {
                go.transform.Find("FightNumber").GetComponent<UISprite>().spriteName = "diPvp5";
                go.transform.Find("FightNumber").transform.Find("yellowLabel").gameObject.SetActive(false);
                go.transform.Find("FightNumber").transform.Find("greenLabel").gameObject.SetActive(true);
                go.transform.Find("FightNumber").transform.Find("greenLabel").GetComponent<UILabel>().text = prcSplit[3];
            }
            else
            {
                go.transform.Find("FightNumber").GetComponent<UISprite>().spriteName = "diPvp6";
                go.transform.Find("FightNumber").transform.Find("yellowLabel").gameObject.SetActive(true);
                go.transform.Find("FightNumber").transform.Find("greenLabel").gameObject.SetActive(false);
                go.transform.Find("FightNumber").transform.Find("yellowLabel").GetComponent<UILabel>().text = prcSplit[3];
            }
        }
        RankUigrid.GetComponent<UIGrid>().Reposition();
    }

    public void KingBuyCount(int _challageNum,int _BuyCount) 
    {
        int CostDaminondNum = TextTranslator.instance.MarketDic[TextTranslator.instance.GetMarketIDByBuyCount(_BuyCount + 1)].KingRoadFightDiamond;
        CostDaimond = CostDaminondNum;
        ChallengeNumber.text = _challageNum.ToString();
    }


    /// <summary>
    /// 战力比自己低的直接挑战成功
    /// </summary>
    public void LookChangeResultWindow() 
    {
        ChangeResultWindow.SetActive(true);
        BackButton.SetActive(true);
        ChangeResultWindow.transform.Find("SpriteBg").gameObject.SetActive(true);
        UIEventListener.Get(BackButton).onClick = delegate(GameObject go)
        {
            ChangeResultWindow.SetActive(false);
        };

        while (ChangeResultWindow.transform.Find("effecttzcg") != null) 
        {
            DestroyImmediate(ChangeResultWindow.transform.Find("effecttzcg").gameObject);
        }
        while (ChangeResultWindow.transform.Find("effectguang") != null)
        {
            DestroyImmediate(ChangeResultWindow.transform.Find("effectguang").gameObject);
        }
        GameObject effecttzcg = Instantiate(EffectTZCG) as GameObject;
        effecttzcg.name = "effecttzcg";
        effecttzcg.transform.parent = ChangeResultWindow.transform;
        effecttzcg.transform.localPosition = new Vector3(0, 0, 0);
        effecttzcg.transform.localScale = new Vector3(300, 300, 300);
        effecttzcg.SetActive(true);

        GameObject effectguang = Instantiate(EffectGuang) as GameObject;
        effectguang.name = "effectguang";
        effectguang.transform.parent = ChangeResultWindow.transform;
        effectguang.transform.localPosition = new Vector3(0, 370, 0);
        effectguang.transform.localScale = new Vector3(400, 400, 400);
        effectguang.SetActive(true);

        TweenScale TweenBg = ChangeResultWindow.transform.Find("SpriteBg").GetComponent<TweenScale>();
        TweenBg.from = new Vector3(1f, 0, 1);
        TweenBg.to = new Vector3(1f, 1f, 1);
        TweenBg.ResetToBeginning();
        TweenBg.PlayForward();

        NetworkHandler.instance.SendProcess("6401#;");
    }
}
