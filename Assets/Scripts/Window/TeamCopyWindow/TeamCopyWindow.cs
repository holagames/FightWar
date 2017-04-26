using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamCopyWindow : MonoBehaviour {

    public GameObject CreateButton;
    public GameObject JoinButton;
    public GameObject QuestionButton;

    public GameObject AwardGrid;//奖励grid;
    public GameObject ItemAward;
    public UILabel ResidueNumber;//剩余次数
    public UILabel ResidueTimer;//冷却时间;
    public UILabel MessageLabel;
    private int time;

    public GameObject uigrid;
    public GameObject ChoseButtonItem;//按钮预制
   

    public GameObject CreateTeamWindow;//创建队伍窗口

    private List<int> ChoseButtonId=new List<int>();//副本名称
    private List<GameObject> ChoseButtonList=new List<GameObject>();


    private int CopyNumber=1;//当前副本编号
    private int ChallengeNum;//剩余次数;
    private int NeedLevel=70;//需要等级;

    private bool ClickIndex=false;//当前副本是否满足进入条件
    //private int ClickIndex = 1;//当前点击的计数
    //private int CanClickIndex = 1;//当前可点击的计数

    int RoomNum = 0;
    private int[] Levellimit = new int[] { 0, 20, 30, 40, 50, 60, 70 };
    public GameObject AutoStartButton;//自动加入战斗
    public bool IsOpenAutoStartButton = false;
    void OnEnable() 
    {
        CharacterRecorder.instance.CopyNumber = 1;
        NetworkHandler.instance.SendProcess("6108#;");
    }
	void Start () {
        //Debug.LogError(CharacterRecorder.instance.CopyNumber);
        AddChoseButtonList();
        SetChoseButtonChange();
        if (UIEventListener.Get(CreateButton).onClick == null) //创建次数
        {
            UIEventListener.Get(CreateButton).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.GuideID[38] == 13)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
                if (ChallengeNum == 0)
                {
                    //CreateTeamWindow.SetActive(true);
                    UIManager.instance.OpenPromptWindow("挑战次数不足", PromptWindow.PromptType.Hint, null, null);
                }
                else if (NeedLevel > CharacterRecorder.instance.level)//ClickIndex == false
                {
                    UIManager.instance.OpenPromptWindow("等级不足", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    Debug.Log("创建副本的标号" + CharacterRecorder.instance.CopyNumber);
                    CreateTeamWindow.SetActive(true);
                }
                //CreateTeamWindow.SetActive(true);
            };
        }

        if (UIEventListener.Get(JoinButton).onClick == null) 
        {
            UIEventListener.Get(JoinButton).onClick += delegate(GameObject go)
            {
                //if (ChallengeNum == 0)
                //{
                //    UIManager.instance.OpenPromptWindow("挑战次数不足", PromptWindow.PromptType.Hint, null, null);
                //}
                //else 
                if (ClickIndex == false)
                {
                    UIManager.instance.OpenPromptWindow("等级不足", PromptWindow.PromptType.Hint, null, null);
                }
                else if (ChallengeNum>0)
                {
                    Debug.Log("浏览副本的标号" + CharacterRecorder.instance.CopyNumber);
                    UIManager.instance.OpenSinglePanel("TeamBrowseWindow", true);
                    NetworkHandler.instance.SendProcess("6101#" + CharacterRecorder.instance.CopyNumber + ";");
                    //CharacterRecorder.instance.CopyNumber = CopyNumber;                   
                }
                else if (ChallengeNum == 0 && time > 0) 
                {
                    UIManager.instance.OpenPromptWindow("还在CD时间", PromptWindow.PromptType.Hint, null, null);
                }
                else if (ChallengeNum == 0 && time <= 0) 
                {
                    Debug.Log("浏览副本的标号" + CharacterRecorder.instance.CopyNumber);
                    UIManager.instance.OpenPromptWindow("参与副本帮打可以获得热心助人宝箱！", PromptWindow.PromptType.Hint, null, null);
                    UIManager.instance.OpenSinglePanel("TeamBrowseWindow", true);
                    NetworkHandler.instance.SendProcess("6101#" + CharacterRecorder.instance.CopyNumber + ";");
                }
                //UIManager.instance.OpenPanel("TeamBrowseWindow", true);
                //NetworkHandler.instance.SendProcess("6101#" + CopyNumber + ";");
                //CharacterRecorder.instance.CopyNumber = CopyNumber;
            };
        }

        UIEventListener.Get(AutoStartButton).onClick = delegate(GameObject go)
        {
            if (AutoStartButton.transform.Find("Checkmark").gameObject.activeSelf)
            {
                AutoStartButton.transform.Find("Checkmark").gameObject.SetActive(false);
                IsOpenAutoStartButton = false;
                PlayerPrefs.SetInt("AutoEnterTeamCopy", 0);
            }
            else 
            {
                AutoStartButton.transform.Find("Checkmark").gameObject.SetActive(true);
                IsOpenAutoStartButton = true;
                PlayerPrefs.SetInt("AutoEnterTeamCopy", 1);
            }
        };

        if (PlayerPrefs.GetInt("AutoEnterTeamCopy") == 1)
        {
            AutoStartButton.transform.Find("Checkmark").gameObject.SetActive(true);
            IsOpenAutoStartButton = true;
        }
        else 
        {
            AutoStartButton.transform.Find("Checkmark").gameObject.SetActive(false);
            IsOpenAutoStartButton = false;
        }

        GameCenter.leavelName = "TeamCopyWindow";
	}


    void AddChoseButtonList() //左边按钮设置
    {
        ChoseButtonList.Clear();
        ChoseButtonId.Clear();
        foreach (var item in TextTranslator.instance.TeamGateList)//判断副本数量
        {
            if (!ChoseButtonId.Contains(item.GroupID))
            {
                //ChoseButtonId.Add(item.GroupID);
                GameObject go = NGUITools.AddChild(uigrid, ChoseButtonItem);
                go.SetActive(true);
                go.name = item.GroupID.ToString();
                go.transform.Find("NormalButton/label").GetComponent<UILabel>().text = item.Name;
                go.transform.Find("ChangeButton/label").GetComponent<UILabel>().text = item.Name;
                if (item.NeedLevel > CharacterRecorder.instance.level) 
                {
                    go.transform.Find("Lock").gameObject.SetActive(true);
                    go.GetComponent<UIToggle>().enabled = false;
                    //go.GetComponent<UIToggle>().value = false;
                    go.transform.Find("ChangeButton").gameObject.SetActive(false);
                    Debug.Log("未开放等级"+item.NeedLevel);
                }
                //if (UIEventListener.Get(go).onClick == null)
                //{
                //    UIEventListener.Get(go).onClick = delegate(GameObject obj)
                //    {
                //        Debug.LogError(item.NeedLevel);
                //        if (item.NeedLevel > CharacterRecorder.instance.level)
                //        {
                //            ClickIndex = false;
                //            UIManager.instance.OpenPromptWindow(item.NeedLevel + "级开放此副本", PromptWindow.PromptType.Hint, null, null);
                //        }
                //        else 
                //        {
                //            ClickIndex = true;
                //            CopyNumber = item.GroupID;
                //            NeedLevel = item.NeedLevel;
                //            CharacterRecorder.instance.CopyNumber = CopyNumber;
                //        }
                //    };
                //}               
                //go.GetComponent<ServerItem>().SetInfo(item);
                ChoseButtonList.Add(go);
                ChoseButtonId.Add(item.GroupID);
            }
        }
        uigrid.GetComponent<UIGrid>().Reposition();
        ChoseButtonList[0].GetComponent<UIToggle>().value = true;
        ChoseButtonList[0].GetComponent<UIToggle>().startsActive = true;
        foreach (var Buttonitem in ChoseButtonList)//监听按钮
        {
            if (UIEventListener.Get(Buttonitem).onClick == null)
            {
                TeamGate _OneTeamGate = null;
                foreach (var TeamGate in TextTranslator.instance.TeamGateList)
                {
                    if (TeamGate.GroupID == int.Parse(Buttonitem.name))
                    {
                        _OneTeamGate = TeamGate;
                        break;
                    }
                }
                UIEventListener.Get(Buttonitem).onClick += delegate(GameObject obj)
                {
                    //TeamGate _OneTeamGate = null;
                    //foreach (var TeamGate in TextTranslator.instance.TeamGateList)
                    //{
                    //    if (TeamGate.GroupID == int.Parse(Buttonitem.name))
                    //    {
                    //        _OneTeamGate = TeamGate;
                    //        break;
                    //    }
                    //}
                    Debug.Log("副本编号"+_OneTeamGate.GroupID);
                    Debug.Log("副本等级"+_OneTeamGate.NeedLevel);

                    if (_OneTeamGate.NeedLevel > CharacterRecorder.instance.level)
                    {
                        ClickIndex = false;
                        UIManager.instance.OpenPromptWindow(_OneTeamGate.NeedLevel + "级开放此副本", PromptWindow.PromptType.Hint, null, null);
                        
                    }
                    else
                    {
                        ClickIndex = true;
                        CopyNumber = _OneTeamGate.GroupID;
                        NeedLevel = _OneTeamGate.NeedLevel;
                        CharacterRecorder.instance.CopyNumber = CopyNumber;
                    }
                };
            }
        }

        for (int i = ChoseButtonList.Count - 1; i >= 0; i--) //确定初始副本按钮位置
        {
            if (ChoseButtonList[i].transform.Find("Lock").gameObject.activeSelf == false) 
            {
                TeamGate _OneTeamGate = null;
                foreach (var TeamGate in TextTranslator.instance.TeamGateList)
                {
                    if (TeamGate.GroupID == int.Parse(ChoseButtonList[i].name))
                    {
                        _OneTeamGate = TeamGate;
                        break;
                    }
                }
                Debug.Log("初始副本编号" + _OneTeamGate.GroupID);
                Debug.Log("初始副本等级" + _OneTeamGate.NeedLevel);
                ClickIndex = true;
                CopyNumber = _OneTeamGate.GroupID;
                NeedLevel = _OneTeamGate.NeedLevel;
                CharacterRecorder.instance.CopyNumber = CopyNumber;
                SetTableWindow(i);
                break;
            }
        }
    }

    public void SetTableWindow(int id)
    {
        if (id != 0)
        {
            ChoseButtonList[0].GetComponent<UIToggle>().value = false;
            ChoseButtonList[0].GetComponent<UIToggle>().startsActive = false;

            ChoseButtonList[id].GetComponent<UIToggle>().enabled = true;
            ChoseButtonList[id].GetComponent<UIToggle>().value = true;
            ChoseButtonList[id].transform.Find("ChangeButton").gameObject.SetActive(true);
        }
        else
        {
            ChoseButtonList[0].GetComponent<UIToggle>().value = true;
            ChoseButtonList[0].GetComponent<UIToggle>().startsActive = true;
        }
    }

    public void SetResidueNumber(int _ResidueNumber,int _CopyNumber,int _ResidueTimer) 
    {
        int maxNum = 0;
        if (CharacterRecorder.instance.Vip >= 6)
        {
            maxNum = 3;
        }
        else 
        {
            maxNum = 2;
        }
        ResidueNumber.text = _ResidueNumber.ToString() + "/" + maxNum.ToString();
        this.CopyNumber = _CopyNumber;
        this.time = _ResidueTimer;
        ChallengeNum = _ResidueNumber;
        if (_ResidueNumber > 0)
        {
            ResidueTimer.gameObject.SetActive(false);
        }
        else if (_ResidueNumber == 0 && _ResidueTimer == 0) 
        {
            Debug.Log("次数" + _ResidueNumber);
            Debug.Log("时间" + _ResidueTimer);
            MessageLabel.gameObject.SetActive(true);
        }
        else if (_ResidueTimer > 0)
        {
            ResidueTimer.gameObject.SetActive(true);
            CancelInvoke();
            InvokeRepeating("UpdateTime", 0, 1.0f);
        }
    }
    void UpdateTime() 
    {
        if (time >= 0) 
        {
            string miniteStr = (time / 60).ToString("00");
            string secondStr = (time % 60).ToString("00");
            ResidueTimer.text = miniteStr + ":" + secondStr;         
            if (time == 0) 
            {
                ResidueTimer.gameObject.SetActive(false);
                MessageLabel.gameObject.SetActive(true);
            }
            time--;
        }
    }

    void SetChoseButtonChange() //加载对应副本内容
    {
        //掉落 剩余次数
        foreach (var item in TextTranslator.instance.itemInfoList) 
        {
            if (item.itemCode/1000==40&&item.itemCode!=40000) 
            {
                GameObject go = NGUITools.AddChild(AwardGrid, ItemAward);
                go.SetActive(true);
                go.name = item.itemName;
                go.GetComponent<UISprite>().spriteName = "Grade" + item.itemGrade.ToString();
                go.transform.Find("Icon").GetComponent<UISprite>().spriteName = item.itemCode.ToString();
                TextTranslator.instance.ItemDescription(go, item.itemCode, 0);
            }
        }
        AwardGrid.GetComponent<UIGrid>().Reposition();
    }

    public void IsAutoEnterCopyWindow(int _RoomNum) //是否自动进入组队
    {
        if (IsOpenAutoStartButton) 
        {
            RoomNum = _RoomNum;
            StopCoroutine("StaySomeTimeToEnterCopy");
            StartCoroutine("StaySomeTimeToEnterCopy");
        }
    }

    IEnumerator StaySomeTimeToEnterCopy()
    {
        int time = Random.Range(2, 5);
        yield return new WaitForSeconds(time);
        //IsAutonEnterCopyTeam = true;
        NetworkHandler.instance.SendProcess("6108#;");
        NetworkHandler.instance.SendProcess("6109#" + RoomNum + ";");
    }

    public void JoinTeamCondition(TeamBrowseItemDate _oneTeamBrowsItemDate)//查找组队信息
    {
        int OpenGate = TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zuduifuben).Level;
        if (CharacterRecorder.instance.lastGateID > OpenGate)
        {
            StartCoroutine(StatyTimeAutoEnter(_oneTeamBrowsItemDate));
        }
        else
        {
            //UIManager.instance.OpenPromptWindow("通过48关解锁组队副本", PromptWindow.PromptType.Hint, null, null);
        }
    }


    IEnumerator StatyTimeAutoEnter(TeamBrowseItemDate _oneTeamBrowsItemDate)
    {
        Levellimit = new int[] { 0, 20, 30, 40, 50, 60, 70 };
        yield return new WaitForSeconds(0.4f);
        string[] trcSplit = _oneTeamBrowsItemDate.condition1.Split('-');

        int OpenLevel = 0;
        for (int i = 0; i < TextTranslator.instance.TeamGateList.size; i++)
        {
            if (TextTranslator.instance.TeamGateList[i].GroupID == _oneTeamBrowsItemDate.copyNumber)
            {
                Debug.Log("关卡开放等级" + TextTranslator.instance.TeamGateList[i].NeedLevel);
                OpenLevel = TextTranslator.instance.TeamGateList[i].NeedLevel;
                break;
            }
        }
        Debug.Log("此队伍需要的等级" + Levellimit[int.Parse(_oneTeamBrowsItemDate.condition3)]);
        Debug.Log("帮打时间" + CharacterRecorder.instance.TeamHelpTime);

        if (CharacterRecorder.instance.level < Levellimit[int.Parse(_oneTeamBrowsItemDate.condition3)])
        {
            Debug.Log("等级不足");
        }
        else if (trcSplit[0] != "0")
        {
            if (trcSplit[0] == "2")
            {
                if (!CharacterRecorder.instance.MyFriendUIDList.Contains(int.Parse(trcSplit[1])))
                {
                    Debug.Log("仅限队长好友可加入");
                }
            }
            else
            {
                if (trcSplit[1] != "0" && CharacterRecorder.instance.legionID.ToString() != trcSplit[1])
                {
                    Debug.Log("仅限队长同军团成员可加入");
                }
            }
        }
        else if (CharacterRecorder.instance.TeamHelpTime > 0 && CharacterRecorder.instance.TeamFightNum == 0)
        {
            Debug.Log("还在CD时间");
        }
        else if (CharacterRecorder.instance.level < OpenLevel)
        {
            Debug.Log("等级不足，尚未解锁该难度");
        }
        else if (_oneTeamBrowsItemDate.teamstate == 1)
        {
            Debug.Log("副本进行中");
        }
        else
        {
            NetworkHandler.instance.SendProcess("6103#" + _oneTeamBrowsItemDate.teamId + ";");
        }
    }
}
