using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OpenServiceWindow : MonoBehaviour {

    public GameObject FinalAwardButton;
    public GameObject FirstButton;
    public GameObject SecondButton;
    public GameObject ThirddayButton;
    public GameObject ForthButton;
    public GameObject FifthButton;
    public GameObject TabButton1;
    public GameObject TabButton2;
    public GameObject TabButton3;
    public GameObject TabButton4;
    public GameObject CloseButton;
    public GameObject uiGrid;

    public GameObject OpenServiceItem;
    public GameObject FinalAwardWindow;

    public UISprite dayTime;
    public UILabel labelTime;

    private int Time=0;
    private int LockDay=0;//锁

    [HideInInspector]
    public int curleftIndex = 0;
    [HideInInspector]
    public int currightIndex = 0;

    public List<GameObject> RedPointList =new List<GameObject>();
    public List<GameObject> FunctionLock = new List<GameObject>();

    private string[] RedStr = { "0", "0", "0", "0", "0", "0", "0", "0", "0" };
    private Dictionary<int, ActivitySevenDay>.ValueCollection valueColl ;
    private Vector3 ScrollView;

    private int CanGetFinalAward = 0;//最终奖励可领取状态，0不可领，1，可领，2已经领过

    void OnEnable() 
    {
        curleftIndex = 1;
        currightIndex = 1;
        SetLeftPart(1);
        SetRightPart(1);
        valueColl=TextTranslator.instance.ActivitySevenDayDic.Values;
        NetworkHandler.instance.SendProcess("9121#;");
    }
	void Start () {
        UIManager.instance.CountActivitys(UIManager.Activitys.大放送);
        UIManager.instance.UpdateActivitys(UIManager.Activitys.大放送);

        //ScrollView = uiGrid.transform.parent.localPosition;
        if (UIEventListener.Get(FinalAwardButton).onClick == null) 
        {
            UIEventListener.Get(FinalAwardButton).onClick += delegate(GameObject go)
            {
                FinalAwardWindow.SetActive(true);
                Debug.LogError("CanGetFinalAward " + CanGetFinalAward);
                FinalAwardWindow.GetComponent<FinalAwardWindow>().Getactivityfinal(Time, CanGetFinalAward);//Getactivityfinal(Time - 86400);
            };
        }
        if (UIEventListener.Get(FirstButton).onClick == null) 
        {
            UIEventListener.Get(FirstButton).onClick += delegate(GameObject go)
            {
                if (LockDay < 1)
                {
                    UIManager.instance.OpenPromptWindow("活动未开放", PromptWindow.PromptType.Hint, null, null);
                }
                else 
                {
                    curleftIndex = 1;
                    currightIndex = 1;
                    SetButtonNum();
                    SetRightPart(1);
                    GetInfo();
                    GetRedPoint();
                }
            };
        }
        if (UIEventListener.Get(SecondButton).onClick == null)
        {
            UIEventListener.Get(SecondButton).onClick += delegate(GameObject go)
            {
                if (LockDay < 2)
                {
                    UIManager.instance.OpenPromptWindow("活动未开放", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    curleftIndex = 2;
                    currightIndex = 1;
                    SetButtonNum();
                    SetRightPart(1);
                    SetLeftPart(2);
                    GetInfo();
                    GetRedPoint();
                }

            };
        }
        if (UIEventListener.Get(ThirddayButton).onClick == null)
        {
            UIEventListener.Get(ThirddayButton).onClick += delegate(GameObject go)
            {
                if (LockDay < 3)
                {
                    UIManager.instance.OpenPromptWindow("活动未开放", PromptWindow.PromptType.Hint, null, null);
                }
                else 
                {
                    curleftIndex = 3;
                    currightIndex = 1;
                    SetButtonNum();
                    SetRightPart(1);
                    SetLeftPart(3);
                    GetInfo();
                    GetRedPoint();
                }
            };
        }
        if (UIEventListener.Get(ForthButton).onClick == null)
        {
            UIEventListener.Get(ForthButton).onClick += delegate(GameObject go)
            {
                if (LockDay < 4)
                {
                    UIManager.instance.OpenPromptWindow("活动未开放", PromptWindow.PromptType.Hint, null, null);
                }
                else 
                {
                    curleftIndex = 4;
                    currightIndex = 1;
                    SetButtonNum();
                    SetRightPart(1);
                    SetLeftPart(4);
                    GetInfo();
                    GetRedPoint();
                }
            };
        }
        if (UIEventListener.Get(FifthButton).onClick == null)
        {
            UIEventListener.Get(FifthButton).onClick += delegate(GameObject go)
            {
                if (LockDay < 5)
                {
                    UIManager.instance.OpenPromptWindow("活动未开放", PromptWindow.PromptType.Hint, null, null);
                }
                else 
                {
                    curleftIndex = 5;
                    currightIndex = 1;
                    SetButtonNum();
                    SetRightPart(1);
                    SetLeftPart(5);
                    GetInfo();
                    GetRedPoint();
                }
            };
        }

        if (UIEventListener.Get(TabButton1).onClick == null)
        {
            UIEventListener.Get(TabButton1).onClick += delegate(GameObject go)
            {
                currightIndex = 1;
                GetInfo();
            };
        }
        if (UIEventListener.Get(TabButton2).onClick == null)
        {
            UIEventListener.Get(TabButton2).onClick += delegate(GameObject go)
            {
                currightIndex = 2;
                GetInfo();
            };
        }
        if (UIEventListener.Get(TabButton3).onClick == null)
        {
            UIEventListener.Get(TabButton3).onClick += delegate(GameObject go)
            {
                currightIndex = 3;
                GetInfo();
            };
        }
        if (UIEventListener.Get(TabButton4).onClick == null)
        {
            UIEventListener.Get(TabButton4).onClick += delegate(GameObject go)
            {
                currightIndex = 4;
                GetInfo();
            };
        }
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                if (GameObject.Find("MainWindow") != null) 
                {
                    //GameObject.Find("MainWindow").GetComponent<MainWindow>().SetSevenDayRedPoint();
                    CharacterRecorder.instance.SetSevenDayRedPoint();
                }
                //UIManager.instance.BackUI();
                Destroy(this.gameObject);
            };
        }
        SetButtonNum();
        //FinallRewardRed();
        //GetRedPoint();
        //GetRedPoint();
	}

    public void SetInfo(string StringAward) 
    {
        string[] dataSplit = StringAward.Split(';');
        Time = int.Parse(dataSplit[1]);
        LockDay = int.Parse(dataSplit[0]);
        CanGetFinalAward = int.Parse(dataSplit[dataSplit.Length - 2]);
        for (int i = 0; i < FunctionLock.Count; i++) 
        {
            if (i <= LockDay - 1)
            {
                FunctionLock[i].SetActive(false);
            }
            else 
            {
                FunctionLock[i].SetActive(true);
            }
        }
        //for (int i = 2; i < dataSplit.Length - 3; i++)
        //{
        //     string[] secSplit = dataSplit[i].Split('$');
        //     int _id = int.Parse(secSplit[0]);
        //     int _gotState = int.Parse(secSplit[1]);
        //     int _completeCount = int.Parse(secSplit[2]);
        //     ActivitySevenDay _ActivitySevenDay = TextTranslator.instance.GetActivitySevenDayByID(_id);
        //     _ActivitySevenDay.SetCompletState(_gotState, _completeCount);
        //}
        int daytime = Time / 86400;
        if (daytime == 0) { dayTime.spriteName = "Serviceword0"; }
        else if (daytime == 1) { dayTime.spriteName = "Serviceword1"; }
        else if (daytime == 2) { dayTime.spriteName = "Serviceword2"; }
        else if (daytime == 3) { dayTime.spriteName = "Serviceword3"; }
        else if (daytime == 4) { dayTime.spriteName = "Serviceword4"; }
        else if (daytime == 5) { dayTime.spriteName = "Serviceword5"; }
        else if (daytime == 6) { dayTime.spriteName = "Serviceword6"; }
        else if (daytime == 7) { dayTime.spriteName = "Serviceword7"; }
        CancelInvoke();
        InvokeRepeating("UpdateTime", 0, 1.0f);
        GetInfo();
        GetRedPoint();
        FinallRewardRed(CanGetFinalAward);//最终奖励红点
    }

    void UpdateTime()
    {
        if (Time > 0)
        {
            if (Time > 86400) { }
            string houreStr = (Time % 86400 / 3600).ToString("00");
            string miniteStr = (Time % 3600 / 60).ToString("00");
            string secondStr = (Time % 60).ToString("00");
            labelTime.text = string.Format("{0}:{1}:{2}", houreStr, miniteStr, secondStr);
            Time -= 1;
        }
    }

    void SetButtonNum() 
    {
        switch (curleftIndex) 
        {
            case 1:
                TabButton3.SetActive(true);
                TabButton4.SetActive(false);
                TabButton1.transform.Find("Label1").GetComponent<UILabel>().text = "等级放送";
                TabButton1.transform.Find("Sprite").transform.Find("Label").GetComponent<UILabel>().text = "等级放送";

                TabButton2.transform.Find("Label2").GetComponent<UILabel>().text = "无往不利";
                TabButton2.transform.Find("Sprite").transform.Find("Label").GetComponent<UILabel>().text = "无往不利";

                TabButton3.transform.Find("Label3").GetComponent<UILabel>().text = "精英之路";
                TabButton3.transform.Find("Sprite").transform.Find("Label").GetComponent<UILabel>().text = "精英之路";
                break;
            case 2:
                TabButton3.SetActive(true);
                TabButton4.SetActive(true);
                TabButton4.GetComponent<UIToggle>().value = false;
                TabButton4.GetComponent<UIToggle>().startsActive = false;
                TabButton1.transform.Find("Label1").GetComponent<UILabel>().text = "英雄升品";
                TabButton1.transform.Find("Sprite").transform.Find("Label").GetComponent<UILabel>().text = "英雄升品";

                TabButton2.transform.Find("Label2").GetComponent<UILabel>().text = "英雄升星";
                TabButton2.transform.Find("Sprite").transform.Find("Label").GetComponent<UILabel>().text = "英雄升星";

                TabButton3.transform.Find("Label3").GetComponent<UILabel>().text = "技能升级";
                TabButton3.transform.Find("Sprite").transform.Find("Label").GetComponent<UILabel>().text = "技能升级";

                TabButton4.transform.Find("Label4").GetComponent<UILabel>().text = "战力提升";
                TabButton4.transform.Find("Sprite").transform.Find("Label").GetComponent<UILabel>().text = "战力提升";
                break;
            case 3:
                TabButton3.SetActive(true);
                TabButton4.SetActive(true);
                TabButton4.GetComponent<UIToggle>().value = false;
                TabButton4.GetComponent<UIToggle>().startsActive = false;
                TabButton1.transform.Find("Label1").GetComponent<UILabel>().text = "装备升级";
                TabButton1.transform.Find("Sprite").transform.Find("Label").GetComponent<UILabel>().text = "装备升级";

                TabButton2.transform.Find("Label2").GetComponent<UILabel>().text = "装备精炼";
                TabButton2.transform.Find("Sprite").transform.Find("Label").GetComponent<UILabel>().text = "装备精炼";

                TabButton3.transform.Find("Label3").GetComponent<UILabel>().text = "饰品强化";
                TabButton3.transform.Find("Sprite").transform.Find("Label").GetComponent<UILabel>().text = "饰品强化";

                TabButton4.transform.Find("Label4").GetComponent<UILabel>().text = "饰品精炼";
                TabButton4.transform.Find("Sprite").transform.Find("Label").GetComponent<UILabel>().text = "饰品精炼";
                break;
            case 4:
                TabButton3.SetActive(true);
                TabButton4.SetActive(false);
                TabButton1.transform.Find("Label1").GetComponent<UILabel>().text = "最高排名";
                TabButton1.transform.Find("Sprite").transform.Find("Label").GetComponent<UILabel>().text = "最高排名";

                TabButton2.transform.Find("Label2").GetComponent<UILabel>().text = "竞技积分";
                TabButton2.transform.Find("Sprite").transform.Find("Label").GetComponent<UILabel>().text = "竞技积分";

                TabButton3.transform.Find("Label3").GetComponent<UILabel>().text = "饰品合成";
                TabButton3.transform.Find("Sprite").transform.Find("Label").GetComponent<UILabel>().text = "饰品合成";
                break;
            case 5:
                TabButton3.SetActive(false);
                TabButton4.SetActive(false);
                TabButton1.transform.Find("Label1").GetComponent<UILabel>().text = "最高层数";
                TabButton1.transform.Find("Sprite").transform.Find("Label").GetComponent<UILabel>().text = "最高层数";

                TabButton2.transform.Find("Label2").GetComponent<UILabel>().text = "累计积分";
                TabButton2.transform.Find("Sprite").transform.Find("Label").GetComponent<UILabel>().text = "累计积分";
                break;
        }
    }
    public void SetLeftPart(int id) 
    {
        if (id != 1)
        {
            FirstButton.GetComponent<UIToggle>().value = false;
            FirstButton.GetComponent<UIToggle>().startsActive = false;
            if (id == 2)
            {
                SecondButton.GetComponent<UIToggle>().value = true;
                SecondButton.GetComponent<UIToggle>().enabled = true;
                SecondButton.transform.Find("ChangeButton").gameObject.SetActive(true);
            }
            else if (id == 3)
            {
                ThirddayButton.GetComponent<UIToggle>().value = true;
                ThirddayButton.GetComponent<UIToggle>().enabled = true;
                ThirddayButton.transform.Find("ChangeButton").gameObject.SetActive(true);
            }
            else if (id == 4)
            {
                ForthButton.GetComponent<UIToggle>().value = true;
                ForthButton.GetComponent<UIToggle>().enabled = true;
                ForthButton.transform.Find("ChangeButton").gameObject.SetActive(true);
            }
            else if (id == 5)
            {
                FifthButton.GetComponent<UIToggle>().value = true;
                FifthButton.GetComponent<UIToggle>().enabled = true;
                FifthButton.transform.Find("ChangeButton").gameObject.SetActive(true);
            }
        }
        else 
        {
            FirstButton.GetComponent<UIToggle>().value = true;
            FirstButton.GetComponent<UIToggle>().startsActive = true;
        }
    }
    public void SetRightPart(int id) 
    {
        if (id != 1)
        {
            TabButton1.GetComponent<UIToggle>().value = false;
            TabButton1.GetComponent<UIToggle>().startsActive = false;
            if (id == 2)
            {
                TabButton2.GetComponent<UIToggle>().value = true;
            }
            else if (id == 3)
            {
                TabButton3.GetComponent<UIToggle>().value = true;
            }
            else if (id == 4)
            {
                TabButton4.GetComponent<UIToggle>().value = true;
            }
        }
        else 
        {
            TabButton1.GetComponent<UIToggle>().value = true;
            TabButton1.GetComponent<UIToggle>().startsActive = true;
        }
    }

    public void GetInfo() //排序
    {
        for (int i = uiGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGrid.transform.GetChild(i).gameObject);
        }
        //uiGrid.transform.parent.GetComponent<UIScrollView>().UpdatePosition();
        uiGrid.transform.parent.localPosition = new Vector3(234f, -40f, 0);
        uiGrid.transform.parent.GetComponent<UIPanel>().clipOffset = Vector2.zero;
        //Dictionary<int, ActivitySevenDay>.ValueCollection valueColl = TextTranslator.instance.ActivitySevenDayDic.Values;
        foreach (ActivitySevenDay _Act in valueColl)
        {
            if (_Act.DayCount == curleftIndex && _Act.BonusGroupID == currightIndex && _Act.CompleteState == 1)
            {
                GameObject go = NGUITools.AddChild(uiGrid, OpenServiceItem);
                go.SetActive(true);
                go.name = _Act.ActivitySevenDayID.ToString();
                go.GetComponent<OpenServiceItem>().SetInfo(_Act);
            }
        }
        foreach (ActivitySevenDay _Act in valueColl) 
        {
            if (_Act.DayCount == curleftIndex && _Act.BonusGroupID == currightIndex && _Act.CompleteState == 0)
            {
                GameObject go = NGUITools.AddChild(uiGrid, OpenServiceItem);
                go.SetActive(true);
                go.name = _Act.ActivitySevenDayID.ToString();
                go.GetComponent<OpenServiceItem>().SetInfo(_Act);
            }
        }
        foreach (ActivitySevenDay _Act in valueColl)
        {
            if (_Act.DayCount == curleftIndex && _Act.BonusGroupID == currightIndex && _Act.CompleteState == 2)
            {
                GameObject go = NGUITools.AddChild(uiGrid, OpenServiceItem);
                go.SetActive(true);
                go.name = _Act.ActivitySevenDayID.ToString();
                go.GetComponent<OpenServiceItem>().SetInfo(_Act);
            }
        }
        uiGrid.GetComponent<UIGrid>().Reposition();
    }

    public void SetUpdate(int ActId) //领取成功刷新
    {
        TextTranslator.instance.GetActivitySevenDayByID(ActId).SetCompletState(2, 1);
        GetInfo();
        GetRedPoint();
    }

    void GetRedPoint() //红点判断
    {
        for (int i = 0; i < RedStr.Length; i++) 
        {
            RedStr[i] = "0";
        }

        foreach (ActivitySevenDay _Act in valueColl)
        {
            if (_Act.DayCount == curleftIndex)
            {
               if (_Act.BonusGroupID == 1 && _Act.CompleteState == 1 && RedStr[5] == "0") { RedStr[5] = "1"; }
               if (_Act.BonusGroupID == 2 && _Act.CompleteState == 1 && RedStr[6] == "0") { RedStr[6] = "1"; }
               if (_Act.BonusGroupID == 3 && _Act.CompleteState == 1 && RedStr[7] == "0") { RedStr[7] = "1"; }
               if (_Act.BonusGroupID == 4 && _Act.CompleteState == 1 && RedStr[8] == "0") { RedStr[8] = "1"; }
            }

            if (_Act.DayCount == 1 && _Act.CompleteState == 1 && RedStr[0] == "0") { RedStr[0] = "1"; }
            if (_Act.DayCount == 2 && _Act.CompleteState == 1 && RedStr[1] == "0") { RedStr[1] = "1"; }
            if (_Act.DayCount == 3 && _Act.CompleteState == 1 && RedStr[2] == "0") { RedStr[2] = "1"; }
            if (_Act.DayCount == 4 && _Act.CompleteState == 1 && RedStr[3] == "0") { RedStr[3] = "1"; }
            if (_Act.DayCount == 5 && _Act.CompleteState == 1 && RedStr[4] == "0") { RedStr[4] = "1"; }
        }
        for (int i = 0; i < 5; i++) 
        {
            if (i > LockDay - 1) { RedStr[i] = "0"; }
        }
        for (int i = 0; i < 9; i++)
        {
            if (RedStr[i] == "1")
            {
                RedPointList[i].SetActive(true);
            }
            else
            {
                RedPointList[i].SetActive(false);
            }
        }
    }

    public void FinallRewardRed(int Cannum) //最终奖励红点
    {
        CanGetFinalAward = Cannum;
        if (CanGetFinalAward == 1)
        {
            FinalAwardButton.transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else 
        {
            FinalAwardButton.transform.Find("RedPoint").gameObject.SetActive(false);
        }

        FinalAwardWindow FW = FinalAwardWindow.GetComponent<FinalAwardWindow>();
        if (FW != null) 
        {
            FW.Getactivityfinal(Time, CanGetFinalAward);
        }
        
        //if (CanGetFinalAward == 1)
        //{
        //    FinalAwardWindow.transform.Find("FinalScrollView/Allparent/GetButton").GetComponent<UISprite>().spriteName = "ui2_button4";
        //    FinalAwardWindow.transform.Find("FinalScrollView/Allparent/GetButton").transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(211 / 255f, 88 / 255f, 13 / 255f, 255 / 255f);
        //}
        //else
        //{
        //    FinalAwardWindow.transform.Find("FinalScrollView/Allparent/GetButton").GetComponent<UISprite>().spriteName = "buttonHui";
        //    FinalAwardWindow.transform.Find("FinalScrollView/Allparent/GetButton").transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(150 / 255f, 150 / 255f, 150 / 255f, 255 / 255f);
        //}
    }


    public bool IsCanGetReward()
    {
        bool IsRed = false;
        int _Currentprogress = 0;
        int _Currentcollar = 0;
        int _Nextcollar = 0;
        Dictionary<int, ActivitySevenDay>.ValueCollection valueColl = TextTranslator.instance.ActivitySevenDayDic.Values;
        foreach (ActivitySevenDay _Act in valueColl)
        {
            if (_Act.CompleteState == 2)
            {
                _Currentprogress += 1;
            }
        }
        _Currentcollar = GetLevel(_Currentprogress);
        if (_Currentcollar != 130)
        {
            _Nextcollar = _Currentcollar + 10;
        }
        else
        {
            _Nextcollar = 130;
        }

        if (Time - 86400 <= 0 || _Currentprogress == 130)
        {
            IsRed = true;
            return IsRed;
        }
        else 
        {
            return IsRed;
        }
    }
    int GetLevel(int num)
    {
        if (num < 10) { num = 0; }
        else if (num >= 10 && num < 20) { num = 10; }
        else if (num >= 20 && num < 30) { num = 20; }
        else if (num >= 30 && num < 40) { num = 30; }
        else if (num >= 40 && num < 50) { num = 40; }
        else if (num >= 50 && num < 60) { num = 50; }
        else if (num >= 60 && num < 70) { num = 60; }
        else if (num >= 70 && num < 80) { num = 70; }
        else if (num >= 80 && num < 90) { num = 80; }
        else if (num >= 90 && num < 100) { num = 90; }
        else if (num >= 100 && num < 110) { num = 100; }
        else if (num >= 110 && num < 120) { num = 110; }
        else if (num >= 120 && num < 130) { num = 120; }
        else if (num == 130) { num = 130; }
        return num;
    }
}
