using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamInvitationWindow : MonoBehaviour
{

    public UILabel teamId;
    public UILabel teamgrade;
    public UILabel teamset;
    public UILabel teamjoin;

    public GameObject InviteWorld;
    public GameObject InviteClan;

    public GameObject SetButton;
    public GameObject StartButton;
    public GameObject DissolveButton;

    public GameObject ReadyButton;
    public GameObject CancelReadyButton;
    public GameObject LevelButton;
    public GameObject ChatCloseButton;

    //public GameObject friendsItem;
    public List<GameObject> PlayerList;//蒙版
    public List<GameObject> PlayerItemList;//战队

    public GameObject FriendListWindow;//好友列表
    public GameObject DetailednessWindow;//详细
    public GameObject HintWindow;//解散队伍
    public GameObject CreateTeamWindow;//队伍设置
    public GameObject AutoStartButton;//自动战斗
    public GameObject AutoInviteButton;//自动邀请
    public GameObject IsNoRomeLabel;//非房主提示
    public int Limitlevel = 0;//限制等级

    private string Condition1;//状态1
    private string Condition2;//是否密码,状态2
    private string Condition3;//限制等级


    public UILabel RefushTimeLabel;
    public UILabel TeamFightLabel;
    public UILabel CopyFightLabel;

    private int TeamFight;
    private int CopyFight;
    private List<int> OneHeroFight = new List<int>();

    private int CopyNumber;
    public int TeamID;//队伍编号
    private bool IsRoom = false;//判断是否房主
    private int Mylocation = -1;//当前位置
    private string TeamName;//副本名称
    public float datatime = 30;

    private string[] PlayerSet = new string[] { "任何人", "军团成员", "仅限好友" };
    private int[] Levellimit = new int[] { 0, 20, 30, 40, 50, 60, 70 };


    //表情聊天一块
    public GameObject ChatControlButton;
    public GameObject ChatPart;
    public UILabel Chattime;//聊天倒计时
    //public GameObject ChatButton1;
    //public GameObject ChatButton2;
    //public GameObject ChatButton3;

    public GameObject[] FaceInfoList;//;= new GameObject[5];
    public GameObject[] ChatButtonArr;
    //雇佣按钮
    public GameObject EmploymentButton;
    public bool isEmployment;
    public GameObject EmploymentWindow;
    //战斗力提醒
    public GameObject FightMessageWindow;
    public GameObject GoToButton;
    public GameObject StartFightButton;
    public GameObject FightCancleButton;
    void Start()
    {
        for (int i = 0; i < PlayerList.Count; i++)
        {
            if (i == Mylocation - 1)
            {
                UIEventListener.Get(PlayerList[i]).onClick += delegate(GameObject go)
                {
                    UIManager.instance.OpenSinglePanel("SelectRoleWindow", false);
                    GameObject.Find("SelectRoleWindow").GetComponent<SelectRoleWindow>().SetRoleInfo(TeamID, Mylocation);
                };
            }
            else
            {
                UIEventListener.Get(PlayerList[i]).onClick += delegate(GameObject go)
                {
                    FriendListWindow.SetActive(true);//邀请好友
                };
            }
        }

        if (UIEventListener.Get(InviteWorld).onClick == null)
        {
            UIEventListener.Get(InviteWorld).onClick += delegate(GameObject go)
            {
                if (datatime >= 30)//30
                {
                    Debug.LogError(Condition1.Split('-')[0]);
                    if (Condition2 == "0" && Condition1.Split('-')[0] != "1")
                    {
                        datatime = 0;
                        CancelInvoke();
                        InvokeRepeating("UpdateTime", 0, 1.0f);
                        GetTeamName();
                        int num = TeamPeopleNum();

                        //NetworkHandler.instance.SendProcess("7001#" + "1;" + CharacterRecorder.instance.characterName + "组织新冒险," + TeamName + ",大家快来参加! 房间号：" + TeamID + ";" + CharacterRecorder.instance.Vip + "$" + CharacterRecorder.instance.headIcon + "$0;");
                        NetworkHandler.instance.SendProcess("7001#" + "6;组织新冒险," + TeamName + ",大家快来参加! 房间号：" + TeamID + ";" + CharacterRecorder.instance.Vip + "$" + CharacterRecorder.instance.headIcon + "$" + CharacterRecorder.instance.legionID + "$" + CharacterRecorder.instance.NationID + "$" + CharacterRecorder.instance.userId + "$" + CharacterRecorder.instance.legionCountryID + ";");
                        NetworkHandler.instance.SendProcess("7002#10;" + CharacterRecorder.instance.characterName + ";" + "组织新冒险," + TeamName + "(人数 " + num + ")" + ",大家快来参加! 房间号：" + TeamID + ";" + TeamID + ";");
                    }
                    else if (Condition1.Split('-')[0] == "1")
                    {
                        UIManager.instance.OpenPromptWindow("军团状态下无法世界邀请", PromptWindow.PromptType.Hint, null, null);
                    }
                    else if (Condition2 != "0")
                    {
                        UIManager.instance.OpenPromptWindow("房间有密码,无法邀请", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("请在冷却时间过后再邀请", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        if (UIEventListener.Get(InviteClan).onClick == null)
        {
            UIEventListener.Get(InviteClan).onClick += delegate(GameObject go)
            {
                if (datatime >= 30)
                {
                    if (Condition2 == "0")
                    {
                        datatime = 0;
                        CancelInvoke();
                        InvokeRepeating("UpdateTime", 0, 1.0f);
                        GetTeamName();
                        int num = TeamPeopleNum();
                        //NetworkHandler.instance.SendProcess("7001#" + "3;" + CharacterRecorder.instance.characterName + "组织新冒险," + TeamName + ",大家快来参加! 房间号：" + TeamID + ";" + CharacterRecorder.instance.Vip + "$" + CharacterRecorder.instance.headIcon + "$" + CharacterRecorder.instance.legionID + ";");
                        NetworkHandler.instance.SendProcess("7001#" + "6;组织新冒险," + TeamName + ",大家快来参加! 房间号：" + TeamID + ";" + CharacterRecorder.instance.Vip + "$" + CharacterRecorder.instance.headIcon + "$" + CharacterRecorder.instance.legionID + "$" + CharacterRecorder.instance.NationID + "$" + CharacterRecorder.instance.userId + "$" + CharacterRecorder.instance.legionCountryID + ";");
                        NetworkHandler.instance.SendProcess("7002#10;" + CharacterRecorder.instance.characterName + ";" + "组织新冒险," + TeamName + "(人数 " + num + ")" + ",大家快来参加! 房间号：" + TeamID + ";" + TeamID + ";");
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("房间有密码,无法邀请", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("请在冷却时间过后再邀请", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        if (UIEventListener.Get(ChatCloseButton).onClick == null)
        {
            UIEventListener.Get(ChatCloseButton).onClick += delegate(GameObject go)
            {
                //UIManager.instance.OpenPanel("ChatWindowNew", false);
                UIManager.instance.OpenSinglePanel("ChatWindowNew", false);
            };
        }
        UIEventListener.Get(EmploymentButton).onClick += delegate(GameObject go)
        {
            if (isEmployment == false)
            {
                NetworkHandler.instance.SendProcess("6004#3;");
                EmploymentWindow.SetActive(true);
            }
            else
            {
                isEmployment = false;
                NetworkHandler.instance.SendProcess("6113#" + TeamID + ";");
                EmploymentButton.transform.Find("Label").GetComponent<UILabel>().text = "雇佣";
                EmploymentButton.transform.Find("Message").GetComponent<UILabel>().text = "1点战斗力消耗1金币";
                EmploymentButton.transform.Find("HeroMessage").GetComponent<UILabel>().text = "雇佣:";
            }
        };
        UIEventListener.Get(StartFightButton).onClick += delegate(GameObject go)
       {
           FightMessageWindow.SetActive(false);
           NetworkHandler.instance.SendProcess("6106#" + TeamID + ";");
       };
        UIEventListener.Get(GoToButton).onClick += delegate(GameObject go)
        {
            FightMessageWindow.SetActive(false);
            NetworkHandler.instance.SendProcess("6004#3;");
            EmploymentWindow.SetActive(true);
        };

        UIEventListener.Get(FightCancleButton).onClick += delegate(GameObject go)
        {
            FightMessageWindow.SetActive(false);
        };

        if (UIEventListener.Get(ChatControlButton).onClick == null)
        {
            UIEventListener.Get(ChatControlButton).onClick += delegate(GameObject go)
            {
                //if (FaceInfoList[Mylocation - 1].activeSelf == false) 
                //{
                //    if (ChatPart.activeSelf == false)
                //    {
                //        ChatPart.SetActive(true);
                //    }
                //    else
                //    {
                //        ChatPart.SetActive(false);
                //    }
                //}
                //else
                //{
                //    UIManager.instance.OpenPromptWindow("还在CD时间", PromptWindow.PromptType.Hint, null, null);
                //}
                if (ChatPart.activeSelf == false)
                {
                    ChatPart.SetActive(true);
                }
                else
                {
                    ChatPart.SetActive(false);
                }
            };
        }

        for (int i = 0; i < ChatButtonArr.Length; i++)
        {
            int num = i + 1;
            UIEventListener.Get(ChatButtonArr[i]).onClick = delegate(GameObject go)
            {
                if (FaceInfoList[Mylocation - 1].activeSelf == false)
                {
                    NetworkHandler.instance.SendProcess("6111#" + num + ";" + TeamID + ";" + Mylocation + ";");
                }
                ChatPart.SetActive(false);
            };
        }
        //RefushTimeLabel.text = "0";
        Chattime.text = "0";
    }

    public void ChangeEmploymentButtonInfo() 
    {
        if (isEmployment == false)
        {
            NetworkHandler.instance.SendProcess("6004#3;");
            EmploymentWindow.SetActive(true);
        }
        else
        {
            isEmployment = false;
            NetworkHandler.instance.SendProcess("6113#" + TeamID + ";");
            EmploymentButton.transform.Find("Label").GetComponent<UILabel>().text = "雇佣";
            EmploymentButton.transform.Find("Message").GetComponent<UILabel>().text = "1点战斗力消耗1金币";
            EmploymentButton.transform.Find("HeroMessage").GetComponent<UILabel>().text = "雇佣:";
        }
    }


    void ChatButtonColor() //聊天按钮颜色
    {
        if (FaceInfoList[Mylocation - 1].activeSelf == false)
        {
            ChatControlButton.GetComponent<UISprite>().spriteName = "button1";
            ChatControlButton.GetComponent<UIButton>().normalSprite = "button1";
            ChatControlButton.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(0 / 255f, 122 / 255f, 245 / 255f, 255 / 255f);
        }
        else
        {
            ChatControlButton.GetComponent<UISprite>().spriteName = "buttonHui";
            ChatControlButton.GetComponent<UIButton>().normalSprite = "buttonHui";
            ChatControlButton.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(136 / 255f, 140 / 255f, 144 / 255f, 255 / 255f);
        }
    }



    void UpdateTime()
    {
        datatime += 1;
        if (datatime >= 30)
        {
            datatime = 30;
        }
        //RefushTimeLabel.text = (30 - datatime).ToString();

        if (datatime == 30)
        {
            //if (AutoStartButton.activeSelf == true && AutoStartButton.transform.Find("Checkmark").gameObject.activeSelf == true&&int.Parse(TeamFightLabel.text) >= int.Parse(CopyFightLabel.text))
            {
                //AutoStartFightWindow();
            }
            //else
            {
                AutoInvitationInfo();
            }
        }
    }
    void GetTeamName()
    {
        foreach (var Item in TextTranslator.instance.TeamGateList)
        {
            if (Item.GroupID == CopyNumber)
            {
                this.TeamName = Item.Name;
                break;
            }
        }
    }
    public void PlayerslimitResufh(int CopyNumber, string limitone, string limittwo, string limitthree, int _teamId, int _teamState) //重置房间条件
    {
        string[] trcSplit = limitone.Split('-');
        teamId.text = _teamId.ToString();
        teamset.text = PlayerSet[int.Parse(trcSplit[0])];//limitone;
        Condition1 = limitone;
        Condition2 = limittwo;
        Condition3 = limitthree;
        if (limitthree == "0")
        {
            teamgrade.text = "无限制";
        }
        else
        {
            teamgrade.text = "等级" + Levellimit[int.Parse(limitthree)].ToString();
        }
        //teamgrade.text = limitthree;
        if (limittwo != "0")
        {
            teamjoin.text = "需要密码";
        }
        else
        {
            teamjoin.text = "无需密码";
        }

        this.TeamID = _teamId;
        this.CopyNumber = CopyNumber;
    }
    public void PlayerslimitSet(int CopyNumber, string limitone, string limittwo, string limitthree, int _teamId, int _teamState) //创建房间初始化数据
    {
        string[] trcSplit = limitone.Split('-');
        this.TeamID = _teamId;
        this.CopyNumber = CopyNumber;

        IsRoom = true;
        Mylocation = 1;
        CharacterRecorder.instance.TeamPosition = Mylocation;
        CharacterRecorder.instance.CopyNumber = CopyNumber;
        CharacterRecorder.instance.TeamID = _teamId;

        teamId.text = _teamId.ToString();
        teamset.text = PlayerSet[int.Parse(trcSplit[0])];//limitone;
        Condition1 = limitone;
        Condition2 = limittwo;
        Condition3 = limitthree;
        if (limitthree == "0")
        {
            teamgrade.text = "无限制";
        }
        else
        {
            teamgrade.text = "等级" + Levellimit[int.Parse(limitthree)].ToString();
        }

        if (limittwo != "0")
        {
            teamjoin.text = "需要密码";
        }
        else
        {
            teamjoin.text = "无需密码";
        }
        AutoStartButton.SetActive(true);
        AutoInviteButton.SetActive(true);
        IsNoRomeLabel.SetActive(false);
        //SetButton.SetActive(true);
        StartButton.SetActive(true);
        DissolveButton.SetActive(true);

        ReadyButton.SetActive(false);
        CancelReadyButton.SetActive(false);
        LevelButton.SetActive(false);

        UIEventListener.Get(AutoStartButton).onClick += delegate(GameObject go)
        {
            if (CharacterRecorder.instance.GuideID[38] == 15)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
            if (AutoStartButton.transform.Find("Checkmark").gameObject.activeSelf == true)
            {
                AutoStartButton.transform.Find("Checkmark").gameObject.SetActive(false);


                AutoInviteButton.transform.Find("Checkmark").gameObject.SetActive(false);//取消自动战斗时也取消自动邀请  11.8
            }
            else
            {
                AutoStartButton.transform.Find("Checkmark").gameObject.SetActive(true);
                AutoStartFightWindow();
            }

        };

        UIEventListener.Get(AutoInviteButton).onClick += delegate(GameObject go)
        {
            if (AutoInviteButton.transform.Find("Checkmark").gameObject.activeSelf == true)
            {
                AutoInviteButton.transform.Find("Checkmark").gameObject.SetActive(false);
            }
            else
            {
                AutoInviteButton.transform.Find("Checkmark").gameObject.SetActive(true);
                if (datatime == 30)
                {
                    AutoInvitationInfo();
                }


                AutoStartButton.transform.Find("Checkmark").gameObject.SetActive(true);//自动邀请是开启自动战斗  11.8
                AutoStartFightWindow();
            }

        };

        UIEventListener.Get(SetButton).onClick += delegate(GameObject go)
        {
            CreateTeamWindow.SetActive(true);
            CreateTeamWindow.GetComponent<CreateTeamWindow>().OnTeamInvitationWindow();
            CreateTeamWindow.GetComponent<CreateTeamWindow>().SetCreateTeamWindowInfo(Condition1, Condition2, Condition3);
        };
        UIEventListener.Get(StartButton).onClick += delegate(GameObject go)
        {
            if (SureCharacterReady())
            {
                //SetHeroIcon();
                //SaveHeroName();
                if (int.Parse(TeamFightLabel.text) < int.Parse(CopyFightLabel.text)&&isEmployment==false)
                {
                    FightMessageWindow.SetActive(true);
                }
                else
                {
                    NetworkHandler.instance.SendProcess("6106#" + TeamID + ";");
                }
            }
            else
            {
                UIManager.instance.OpenPromptWindow("有战队未准备", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(DissolveButton).onClick += delegate(GameObject go)
        {
            HintWindow.SetActive(true);
            HintWindow.GetComponent<HintWindow>().SetHintWindow(TeamID, Mylocation);
        };
        CopyAllFight();
    }

    public void ChoseBattleHero(int herolocation, string playername, int heroid, int herograde, int herofight) //选择上阵英雄，已经进入房间
    {

        //SetButton.SetActive(true);
        StartButton.SetActive(true);
        DissolveButton.SetActive(true);

        ReadyButton.SetActive(false);
        CancelReadyButton.SetActive(false);
        LevelButton.SetActive(false);


        PlayerList[herolocation - 1].transform.Find("name").gameObject.SetActive(false);

        PlayerItemList[herolocation - 1].SetActive(true);
        PlayerItemList[herolocation - 1].transform.Find("name").GetComponent<UILabel>().text = playername;
        PlayerItemList[herolocation - 1].transform.Find("Role").GetComponent<UISprite>().spriteName = heroid.ToString();
        PlayerItemList[herolocation - 1].transform.Find("grade").GetComponent<UILabel>().text = herograde.ToString();

        HeroInfo hero = TextTranslator.instance.GetHeroInfoByHeroID(heroid);//_CharacterRoleID
        PlayerItemList[herolocation - 1].GetComponent<UISprite>().spriteName = "yxdi" + (hero.heroRarity).ToString();

        if (IsRoom)
        {
            PlayerItemList[herolocation - 1].transform.Find("SpriteTop1").gameObject.SetActive(true);
            EmploymentButton.SetActive(true);
        }
        else
        {
            EmploymentButton.SetActive(false);
        }
        if (herolocation == Mylocation)
        {
            UIEventListener.Get(PlayerItemList[herolocation - 1]).onClick = delegate(GameObject go)
            {
                UIManager.instance.OpenSinglePanel("SelectRoleWindow", false); ;//上阵
            };
        }
        else
        {
            UIEventListener.Get(PlayerItemList[herolocation - 1]).onClick = delegate(GameObject go)
            {
                if (IsRoom)
                {
                    DetailednessWindow.SetActive(true);//详情
                }
                else
                {
                    DetailednessWindow.SetActive(true);//详情
                }
            };
        }

    }

    public void ChoseBattleHero(string Recving) //已经进入房间，选择英雄或离队通知 6104
    {
        string[] dataSplit = Recving.Split(';');
        OneHeroFight.Clear();
        for (int i = 0; i < dataSplit.Length - 1; i++)
        {
            string[] trcSplit = dataSplit[i].Split('$');
            if (trcSplit[1] != "")
            {
                if (trcSplit[1] == CharacterRecorder.instance.characterName)
                {
                    Mylocation = int.Parse(trcSplit[0]);
                    CharacterRecorder.instance.TeamPosition = Mylocation;
                    Debug.LogError("我的英雄位置" + Mylocation);
                }
                //if (trcSplit[3] == "0")
                //{
                //    PlayerList[i].transform.Find("name").gameObject.SetActive(true);
                //    //PlayerList[i].transform.Find("name").GetComponent<UILabel>().color = new Color(43 / 255f, 168 / 255f, 110 / 255f, 255 / 255f);
                //    PlayerList[i].transform.Find("name").GetComponent<UILabel>().text = trcSplit[1];
                //    PlayerList[i].transform.Find("name").GetComponent<UILabel>().color = Color.white;
                //}
                //else
                //{
                //TeamFight += int.Parse(dataSplit[5]);
                OneHeroFight.Add(int.Parse(trcSplit[5]));
                PlayerList[i].transform.Find("name").gameObject.SetActive(false);
                PlayerItemList[i].SetActive(true);
                PlayerItemList[i].transform.Find("name").GetComponent<UILabel>().text = trcSplit[1];
                PlayerItemList[i].transform.Find("Role").GetComponent<UISprite>().spriteName = trcSplit[3];
                PlayerItemList[i].transform.Find("grade").GetComponent<UILabel>().text = trcSplit[4];
                HeroInfo hero = TextTranslator.instance.GetHeroInfoByHeroID(int.Parse(trcSplit[3]));//_CharacterRoleID
                PlayerItemList[i].GetComponent<UISprite>().spriteName = "yxdi" + (hero.heroRarity).ToString();
                if (Mylocation - 1 != i)
                {
                    int j = i;
                    UIEventListener.Get(PlayerItemList[i]).onClick = delegate(GameObject go)
                    {
                        DetailednessWindow.SetActive(true);
                        DetailednessWindow.GetComponent<DetailednessWindow>().SetDetailness(IsRoom, TeamID, j + 1, trcSplit[1], int.Parse(trcSplit[2]), int.Parse(trcSplit[3]), int.Parse(trcSplit[4]));

                    };
                }
                else
                {
                    int j = i;
                    UIEventListener.Get(PlayerItemList[i]).onClick = delegate(GameObject go)
                    {
                        if (PlayerItemList[j].transform.Find("SpriteTop2").gameObject.activeSelf == false)
                        {
                            UIManager.instance.OpenSinglePanel("SelectRoleWindow", false); ;
                            GameObject.Find("SelectRoleWindow").GetComponent<SelectRoleWindow>().SetRoleInfo(TeamID, Mylocation);
                        }
                        else
                        {
                            UIManager.instance.OpenPromptWindow("准备状态不能换英雄", PromptWindow.PromptType.Hint, null, null);
                        }
                    };
                }
                if (i == 0)
                {
                    PlayerItemList[0].transform.Find("SpriteTop1").gameObject.SetActive(true);
                }

                if (trcSplit[6] == "0")
                {
                    PlayerItemList[i].transform.Find("SpriteTop2").gameObject.SetActive(false);
                }
                else if (trcSplit[6] == "1" && i != 0)
                {
                    PlayerItemList[i].transform.Find("SpriteTop2").gameObject.SetActive(true);
                }

                //}
            }
            else
            {
                PlayerItemList[i].SetActive(false);
                PlayerList[i].transform.Find("name").gameObject.SetActive(true);
            }
        }

        GetHeroFight();
        AutoStartFightWindow();
    }

    public void ChoseBattleHero2(string Recving) //初始进入房间，确定个人位置 6103
    {
        string[] dataSplit = Recving.Split(';');
        string[] prcSplit = dataSplit[2].Split('-');

        this.TeamID = int.Parse(dataSplit[5]);
        this.CopyNumber = int.Parse(dataSplit[1]);
        CharacterRecorder.instance.CopyNumber = int.Parse(dataSplit[1]);
        CharacterRecorder.instance.TeamID = int.Parse(dataSplit[5]);

        OneHeroFight.Clear();
        teamId.text = dataSplit[5];
        teamset.text = PlayerSet[int.Parse(prcSplit[0])];//limitone;
        Condition1 = dataSplit[2];
        Condition2 = dataSplit[3];
        Condition3 = dataSplit[4];
        if (dataSplit[4] == "0")
        {
            teamgrade.text = "无限制";
        }
        else
        {
            teamgrade.text = "等级" + Levellimit[int.Parse(dataSplit[4])].ToString();
        }

        if (dataSplit[3] != "0")
        {
            teamjoin.text = "需要密码";
        }
        else
        {
            teamjoin.text = "无需密码";
        }

        //SetButton.SetActive(false);
        StartButton.SetActive(false);
        DissolveButton.SetActive(false);

        ReadyButton.SetActive(true);
        CancelReadyButton.SetActive(false);
        LevelButton.SetActive(true);

        string alldataSplit = dataSplit[6] + ";" + dataSplit[7] + ";" + dataSplit[8] + ";" + dataSplit[9] + ";" + dataSplit[10] + ";";
        string[] Split = alldataSplit.Split(';');
        for (int i = 0; i < Split.Length - 1; i++)
        {
            string[] trcSplit = Split[i].Split('$');
            if (trcSplit[1] != "")
            {
                if (trcSplit[1] == CharacterRecorder.instance.characterName)
                {
                    Mylocation = int.Parse(trcSplit[0]);
                    CharacterRecorder.instance.TeamPosition = Mylocation;
                    Debug.LogError("我的初始位置" + Mylocation);
                }
                //if (trcSplit[3] == "0")
                //{
                //    PlayerList[i].transform.Find("name").gameObject.SetActive(true);
                //    //PlayerList[i].transform.Find("name").GetComponent<UILabel>().color = new Color(43 / 255f, 168 / 255f, 110 / 255f, 255 / 255f);
                //    PlayerList[i].transform.Find("name").GetComponent<UILabel>().text = trcSplit[1];
                //    PlayerList[i].transform.Find("name").GetComponent<UILabel>().color = Color.white;
                //}
                //else
                //{
                OneHeroFight.Add(int.Parse(trcSplit[5]));
                PlayerList[i].transform.Find("name").gameObject.SetActive(false);
                PlayerItemList[i].SetActive(true);
                PlayerItemList[i].transform.Find("name").GetComponent<UILabel>().text = trcSplit[1];
                PlayerItemList[i].transform.Find("Role").GetComponent<UISprite>().spriteName = trcSplit[3];
                PlayerItemList[i].transform.Find("grade").GetComponent<UILabel>().text = trcSplit[4];
                HeroInfo hero = TextTranslator.instance.GetHeroInfoByHeroID(int.Parse(trcSplit[3]));//_CharacterRoleID
                PlayerItemList[i].GetComponent<UISprite>().spriteName = "yxdi" + (hero.heroRarity).ToString();
                if (Mylocation - 1 != i)
                {
                    int j = i;
                    UIEventListener.Get(PlayerItemList[i]).onClick = delegate(GameObject go)
                    {
                        DetailednessWindow.SetActive(true);
                        DetailednessWindow.GetComponent<DetailednessWindow>().SetDetailness(IsRoom, TeamID, j + 1, trcSplit[1], int.Parse(trcSplit[2]), int.Parse(trcSplit[3]), int.Parse(trcSplit[4]));
                    };
                }
                else
                {
                    int j = i;
                    UIEventListener.Get(PlayerItemList[i]).onClick = delegate(GameObject go)
                    {
                        if (PlayerItemList[j].transform.Find("SpriteTop2").gameObject.activeSelf == false)
                        {
                            UIManager.instance.OpenSinglePanel("SelectRoleWindow", false); ;
                            GameObject.Find("SelectRoleWindow").GetComponent<SelectRoleWindow>().SetRoleInfo(TeamID, Mylocation);
                        }
                        else
                        {
                            UIManager.instance.OpenPromptWindow("准备状态不能换英雄", PromptWindow.PromptType.Hint, null, null);
                        }
                    };
                }

                if (i == 0)
                {
                    PlayerItemList[0].transform.Find("SpriteTop1").gameObject.SetActive(true);
                }

                if (trcSplit[6] == "0")
                {
                    PlayerItemList[i].transform.Find("SpriteTop2").gameObject.SetActive(false);
                }
                else if (trcSplit[6] == "1" && i != 0)
                {
                    PlayerItemList[i].transform.Find("SpriteTop2").gameObject.SetActive(true);
                }
                //}
            }
            else
            {
                PlayerItemList[i].SetActive(false);
                PlayerList[i].transform.Find("name").gameObject.SetActive(true);
            }
        }

        UIEventListener.Get(ReadyButton).onClick = delegate(GameObject go)
        {
            if (PlayerItemList[Mylocation - 1].activeSelf == true)
            {
                NetworkHandler.instance.SendProcess("6110#" + "1" + ";" + TeamID + ";" + Mylocation + ";");
            }
            else
            {
                UIManager.instance.OpenPromptWindow("请先选择上阵英雄", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(CancelReadyButton).onClick += delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("6110#" + "0" + ";" + TeamID + ";" + Mylocation + ";");
        };
        UIEventListener.Get(LevelButton).onClick = delegate(GameObject go)
        {
            HintWindow.SetActive(true);
            HintWindow.GetComponent<HintWindow>().SetHintWindow(TeamID, Mylocation);
        };
        if (IsRoom)
        {
            EmploymentButton.SetActive(true);
        }
        else
        {
            EmploymentButton.SetActive(false);
        }
        CopyAllFight();
        GetHeroFight();
        StartCoroutine(AutoPlayerSetOut());
    }

    IEnumerator AutoPlayerSetOut() //3秒自动准备
    {
        yield return new WaitForSeconds(3f);
        if (PlayerItemList[Mylocation - 1].activeSelf == true)
        {
            NetworkHandler.instance.SendProcess("6110#" + "1" + ";" + TeamID + ";" + Mylocation + ";");
        }
    }

    public void LeaveTheTeam(int _teamNumber, int leavePosition) //队友离开队伍 no
    {
        PlayerItemList[leavePosition - 1].SetActive(false);
        PlayerList[leavePosition - 1].transform.Find("name").gameObject.SetActive(true);
        PlayerList[leavePosition - 1].transform.Find("name").GetComponent<UILabel>().text = "点击邀请好友";
        PlayerList[leavePosition - 1].transform.Find("name").GetComponent<UILabel>().color = new Color(43 / 255f, 168 / 255f, 110 / 255f, 255 / 255f);
    }

    public void PlayerSetOut(int readycondition, int teamid, int mylocation) //准备状态
    {
        if (readycondition == 1)
        {
            PlayerItemList[mylocation - 1].transform.Find("SpriteTop2").gameObject.SetActive(true);
            ReadyButton.SetActive(false);
            CancelReadyButton.SetActive(true);
            UIEventListener.Get(CancelReadyButton).onClick = delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("6110#" + "0" + ";" + teamid + ";" + mylocation + ";");
            };
            AutoStartFightWindow();
        }
        else
        {
            PlayerItemList[mylocation - 1].transform.Find("SpriteTop2").gameObject.SetActive(false);
            ReadyButton.SetActive(true);
            CancelReadyButton.SetActive(false);
            UIEventListener.Get(CancelReadyButton).onClick = delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("6110#" + "1" + ";" + teamid + ";" + mylocation + ";");
            };
        }
    }

    public void InstanceTalk(int faceNum, int teamid, int mylocation) //6111 表情
    {
        StartCoroutine(IEInstanceTalk(faceNum, teamid, mylocation));
    }

    IEnumerator IEInstanceTalk(int faceNum, int teamid, int mylocation)
    {
        FaceInfoList[mylocation - 1].SetActive(true);
        //Chattime.text = "3";
        //ChatButtonColor();
        for (int i = 0; i < FaceInfoList[mylocation - 1].transform.childCount; i++)
        {
            FaceInfoList[mylocation - 1].transform.GetChild(i).gameObject.SetActive(false);
        }
        FaceInfoList[mylocation - 1].transform.GetChild(faceNum - 1).gameObject.SetActive(true);
        SetChatButtonHuiIcon(Mylocation);
        yield return new WaitForSeconds(3f);
        //Chattime.text = "2";
        //yield return new WaitForSeconds(1f);
        //Chattime.text = "1";
        //yield return new WaitForSeconds(1f);
        //Chattime.text = "0";
        FaceInfoList[mylocation - 1].SetActive(false);
        SetChatButtonHuiIcon(Mylocation);
        //ChatButtonColor();
    }

    private void SetChatButtonHuiIcon(int mylocation)
    {
        if (FaceInfoList[mylocation - 1].activeSelf == false)
        {
            ChatButtonArr[0].GetComponent<UISprite>().spriteName = "biaoqing1_00";
            ChatButtonArr[1].GetComponent<UISprite>().spriteName = "biaoqing2_00";
            ChatButtonArr[2].GetComponent<UISprite>().spriteName = "biaoqing3_00";
            ChatButtonArr[3].GetComponent<UISprite>().spriteName = "biaoqing4_00";
            ChatButtonArr[4].GetComponent<UISprite>().spriteName = "biaoqing5_00";
            ChatButtonArr[5].GetComponent<UISprite>().spriteName = "biaoqing6_00";
            ChatButtonArr[6].GetComponent<UISprite>().spriteName = "biaoqing7_00";
            ChatButtonArr[7].GetComponent<UISprite>().spriteName = "yaoqing";
            ChatButtonArr[8].GetComponent<UISprite>().spriteName = "dengren";
            ChatButtonArr[9].GetComponent<UISprite>().spriteName = "gogo";
        }
        else
        {
            ChatButtonArr[0].GetComponent<UISprite>().spriteName = "bbiaoqing1_07";
            ChatButtonArr[1].GetComponent<UISprite>().spriteName = "bbiaoqing2_05";
            ChatButtonArr[2].GetComponent<UISprite>().spriteName = "bbiaoqing3_04";
            ChatButtonArr[3].GetComponent<UISprite>().spriteName = "bbiaoqing4_09";
            ChatButtonArr[4].GetComponent<UISprite>().spriteName = "bbiaoqing5_08";
            ChatButtonArr[5].GetComponent<UISprite>().spriteName = "bbiaoqing6_06";
            ChatButtonArr[6].GetComponent<UISprite>().spriteName = "bbiaoqing7_14";
            ChatButtonArr[7].GetComponent<UISprite>().spriteName = "yaoqing_2";
            ChatButtonArr[8].GetComponent<UISprite>().spriteName = "dengren_2";
            ChatButtonArr[9].GetComponent<UISprite>().spriteName = "gogo_2";
        }
    }



    public void SetHeroIcon() //保存组队hero Id;
    {
        CharacterRecorder.instance.CopyHeroIconList.Clear();
        CharacterRecorder.instance.CopyHeroNameList.Clear();
        for (int i = 0; i < PlayerItemList.Count; i++)
        {
            if (PlayerItemList[i].activeSelf == true)
            {
                int HeroIcon = int.Parse(PlayerItemList[i].transform.Find("Role").GetComponent<UISprite>().spriteName);
                string HeroName = PlayerItemList[i].transform.Find("name").GetComponent<UILabel>().text;
                CharacterRecorder.instance.CopyHeroIconList.Add(HeroIcon);
                CharacterRecorder.instance.CopyHeroNameList.Add(HeroName);
            }
        }
    }

    public void SaveHeroName() //保存组队hero Id;
    {
        CharacterRecorder.instance.CopyHeroNameList.Clear();
        for (int i = 0; i < PlayerItemList.Count; i++)
        {
            if (PlayerItemList[i].activeSelf == true)
            {
                string HeroName = PlayerItemList[i].transform.Find("name").GetComponent<UILabel>().text;
                CharacterRecorder.instance.CopyHeroNameList.Add(HeroName);
            }
        }
    }

    bool SureCharacterReady() //战队是否都准备好
    {
        for (int i = 1; i < PlayerItemList.Count; i++)
        {
            if (PlayerItemList[i].activeSelf == true && PlayerItemList[i].transform.Find("SpriteTop2").gameObject.activeSelf == false)
            {
                return false;
            }
        }
        return true;
    }

    public void OneHeroLiveRoom(int num) //默认离开
    {
        PlayerItemList[num - 1].SetActive(false);
        OneHeroFight[num - 1] = 0;
        GetHeroFight();
    }

    private void AutoStartFightWindow() //自动开始
    {
        if (AutoStartButton.activeSelf == true && AutoStartButton.transform.Find("Checkmark").gameObject.activeSelf == true)
        {
            if (SureCharacterReady())
            {
                if (int.Parse(TeamFightLabel.text) >= int.Parse(CopyFightLabel.text))
                {
                    //SetHeroIcon();
                    //SaveHeroName();
                    NetworkHandler.instance.SendProcess("6106#" + TeamID + ";");
                }
            }
        }
    }

    private void GetHeroFight() //队伍总战力
    {
        TeamFight = 0;
        for (int i = 0; i < OneHeroFight.Count; i++)
        {
            TeamFight += OneHeroFight[i];
        }
        TeamFightLabel.text = TeamFight.ToString();
    }

    private void CopyAllFight() //副本总战力
    {
        int groupId = 0;
        foreach (var Item in TextTranslator.instance.TeamGateList)
        {
            if (Item.GroupID == CopyNumber)
            {
                groupId = Item.GroupID;
                break;
            }
        }
        this.CopyFight = TextTranslator.instance.GetTeamGateListByID(groupId * 10).Force;
        CopyFightLabel.text = CopyFight.ToString();
    }

    public int TeamPeopleNum() //队伍中人数
    {
        int num = 0;
        for (int i = 0; i < PlayerItemList.Count; i++)
        {
            if (PlayerItemList[i].activeSelf == true)
            {
                num++;
            }
        }
        return num;
    }

    private void AutoInvitationInfo() //房主自动邀请
    {
        if (AutoInviteButton.activeSelf == true && AutoInviteButton.transform.Find("Checkmark").gameObject.activeSelf == true)
        {
            if (Condition2 == "0")//&& Condition1.Split('-')[0] != "1"
            {
                datatime = 0;
                CancelInvoke();
                InvokeRepeating("UpdateTime", 0, 1.0f);
                GetTeamName();
                int num = TeamPeopleNum();
                //NetworkHandler.instance.SendProcess("7001#" + "1;" + CharacterRecorder.instance.characterName + "组织新冒险," + TeamName +",大家快来参加! 房间号：" + TeamID + ";" + CharacterRecorder.instance.Vip + "$" + CharacterRecorder.instance.headIcon + "$0;");
                NetworkHandler.instance.SendProcess("7001#" + "6;组织新冒险," + TeamName + ",大家快来参加! 房间号：" + TeamID + ";" + CharacterRecorder.instance.Vip + "$" + CharacterRecorder.instance.headIcon + "$" + CharacterRecorder.instance.legionID + "$" + CharacterRecorder.instance.NationID + "$" + CharacterRecorder.instance.userId + "$" + CharacterRecorder.instance.legionCountryID + ";");
                NetworkHandler.instance.SendProcess("7002#10;" + CharacterRecorder.instance.characterName + ";" + "组织新冒险," + TeamName + "(人数 " + num + ")" + ",大家快来参加! 房间号：" + TeamID + ";" + TeamID + ";");
            }
        }
    }
}
