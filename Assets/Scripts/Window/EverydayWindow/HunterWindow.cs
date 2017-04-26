using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HunterWindow : MonoBehaviour
{

    public GameObject QusButton;
    public GameObject HuterMessageObj;
    public GameObject JoinButton;
    public int ResidueNumber;
    public int NowTimer;
    private GameObject SelectDifficultyWindow;
    public List<GameObject> CheckDifficulty = new List<GameObject>();
    public UIScrollBar MoveCheckDifficulty;
    public int CheckID;
    public GameObject EverydayWindow;
    private int tabe;
    public GameObject DropItem;
    public GameObject DropGrid;
    public GameObject DropScrollView;
    public List<GameObject> ListItem;
    public string OpenDayinfo = "";
    //极限挑战
    public GameObject BlackButton;
    public GameObject JixianActive;
    public GameObject BuffItem;
    public GameObject NameSprite;
    public List<GameObject> BuffItemList;
    public UIGrid ChallengeGrid;
    public bool isOpenDay = false;
    public UILabel OpenLabel;
    int WeekDay = 0;
    // Use this for initialization
    void Start()
    {
        BlackButton.SetActive(false);
        UIEventListener.Get(QusButton).onClick = delegate(GameObject go)
        {
            HuterMessageObj.SetActive(true);
            HouterMessage();
        };
        if (UIEventListener.Get(HuterMessageObj.transform.Find("Message/CloseButton").gameObject).onClick == null)
        {
            UIEventListener.Get(HuterMessageObj.transform.Find("Message/CloseButton").gameObject).onClick += delegate(GameObject go)
            {
                HuterMessageObj.SetActive(false);
            };
        }
        if (UIEventListener.Get(HuterMessageObj.transform.Find("Mask").gameObject).onClick == null)
        {
            UIEventListener.Get(HuterMessageObj.transform.Find("Mask").gameObject).onClick += delegate(GameObject go)
            {
                HuterMessageObj.SetActive(false);
            };
        }
        if (UIEventListener.Get(JoinButton).onClick == null)
        {
            UIEventListener.Get(JoinButton).onClick += delegate(GameObject go)
            {
                if (ResidueNumber == 0)
                {
                    UIManager.instance.OpenPromptWindow("次数用完了,明天再来吧", PromptWindow.PromptType.Hint, null, null);
                }
                else if (NowTimer > 0)
                {
                    UIManager.instance.OpenPromptWindow("长官,请稍等片刻哟", PromptWindow.PromptType.Hint, null, null);
                }
                else if (isOpenDay)
                {
                    UIManager.instance.OpenPromptWindow("长官,还未到开放时间", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    if (CharacterRecorder.instance.GuideID[9] == 8 || CharacterRecorder.instance.GuideID[13] == 8)
                    {
                        SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                    }
                    this.gameObject.transform.Find("SelectDifficultyWindow").gameObject.SetActive(true);
                    EventDifficulty();
                }
            };
        }
        UIEventListener.Get(BlackButton).onClick += delegate(GameObject go)
        {
            UIManager.instance.OpenPromptWindow("长官,还未到开放时间", PromptWindow.PromptType.Hint, null, null);
        };
    }

    public void IsShowInfo(int Tabe)
    {
        tabe = Tabe;
        NameSprite.SetActive(false);
        JixianActive.SetActive(false);
        BlackButton.SetActive(false);
        JoinButton.SetActive(true);
        transform.Find("ActivityName").gameObject.SetActive(true);
        if (Tabe == 1)
        {
            UIManager.instance.CountSystem(UIManager.Systems.赏金猎人);
            UIManager.instance.UpdateSystems(UIManager.Systems.赏金猎人);

            transform.Find("Bg").GetComponent<UITexture>().mainTexture = Resources.Load("Game/dajintiao") as Texture;
            transform.Find("ActivityName").GetComponent<UISprite>().spriteName = "tu3";
            ShowRewardList(1, Tabe);
            isOpenDay = false;
            OpenLabel.gameObject.SetActive(false);
        }
        else if (Tabe == 2)
        {
            UIManager.instance.CountSystem(UIManager.Systems.千锤百炼);
            UIManager.instance.UpdateSystems(UIManager.Systems.千锤百炼);

            transform.Find("Bg").GetComponent<UITexture>().mainTexture = Resources.Load("Game/dayou") as Texture;
            transform.Find("ActivityName").GetComponent<UISprite>().spriteName = "tu4";
            ShowRewardList(6, Tabe);
            isOpenDay = false;
            OpenLabel.gameObject.SetActive(false);
        }
        else if (Tabe == 3)
        {
            UIManager.instance.CountSystem(UIManager.Systems.极限挑战);
            UIManager.instance.UpdateSystems(UIManager.Systems.极限挑战);

            transform.Find("Bg").GetComponent<UITexture>().mainTexture = Resources.Load("Game/challenge") as Texture;
            transform.Find("ActivityName").gameObject.SetActive(false);
            NameSprite.SetActive(true);
            JixianActive.SetActive(true);
            ShowRewardList(1, Tabe);
            OpenLabel.gameObject.SetActive(true);
            OpenLabel.text = "开放时间;每周一、三、五、日。";
        }
    }
    // Update is called once per frame
    public void HouterMessage()
    {
        if (tabe == 1)
        {
            EverydayActivity every = TextTranslator.instance.GetEverydayActivityDicByID(1);
            HuterMessageObj.transform.Find("Message/TitleLabel").GetComponent<UILabel>().text = "赏金猎人";
            string[] openday = every.OpenWeek.Split('$');
            for (int i = 0; i < openday.Length; i++)
            {
                if (i != openday.Length - 1)
                {
                    OpenDayinfo += CheckNumShow(openday[i]) + "、";
                }
                else
                {
                    OpenDayinfo += CheckNumShow(openday[i]) + "。";
                }
            }
            if (openday.Length < 7)
            {
                HuterMessageObj.transform.Find("Message/OpenTime").GetComponent<UILabel>().text = "每周" + OpenDayinfo;
            }
            else
            {
                HuterMessageObj.transform.Find("Message/OpenTime").GetComponent<UILabel>().text = "每天";
            }
            HuterMessageObj.transform.Find("Message/ResultLabel").GetComponent<UILabel>().text = "1.通关17关以上可以参加,每天两次，每天凌晨五点重置挑战次数。\n2.胜利获得大量金币，难度越高，获得的奖励越多。";
        }
        else if (tabe == 2)
        {
            EverydayActivity every = TextTranslator.instance.GetEverydayActivityDicByID(7);
            HuterMessageObj.transform.Find("Message/TitleLabel").GetComponent<UILabel>().text = "千锤百炼";
            string[] openday = every.OpenWeek.Split('$');
            for (int i = 0; i < openday.Length; i++)
            {
                if (i != openday.Length - 1)
                {
                    OpenDayinfo += CheckNumShow(openday[i]) + "、";
                }
                else
                {
                    OpenDayinfo += CheckNumShow(openday[i]) + "。";
                }
            }
            if (openday.Length < 7)
            {
                HuterMessageObj.transform.Find("Message/OpenTime").GetComponent<UILabel>().text = "每周" + OpenDayinfo;
            }
            else
            {
                HuterMessageObj.transform.Find("Message/OpenTime").GetComponent<UILabel>().text = "每天";
            }
            HuterMessageObj.transform.Find("Message/ResultLabel").GetComponent<UILabel>().text = "1.通关72关可以参加,每天两次，每天凌晨五点重置挑战次数。\n2.胜利获得大量军粮，难度越高，获得的奖励越多。";
        }
        else if (tabe == 3)
        {
            EverydayActivity every = TextTranslator.instance.GetEverydayActivityDicByID(31);
            HuterMessageObj.transform.Find("Message/TitleLabel").GetComponent<UILabel>().text = "极限挑战";
            string[] openday = every.OpenWeek.Split('$');
            for (int i = 0; i < openday.Length; i++)
            {
                if (i != openday.Length - 1)
                {
                    OpenDayinfo += CheckNumShow(openday[i]) + "、";
                }
                else
                {
                    OpenDayinfo += CheckNumShow(openday[i]) + "。";
                }
            }

            if (openday.Length < 7)
            {
                HuterMessageObj.transform.Find("Message/OpenTime").GetComponent<UILabel>().text = "每周" + OpenDayinfo;
            }
            else
            {
                HuterMessageObj.transform.Find("Message/OpenTime").GetComponent<UILabel>().text = "每天";
            }
            HuterMessageObj.transform.Find("Message/ResultLabel").GetComponent<UILabel>().text = "1.通关96关可以参加，每天2次，每天凌晨5点重置挑战次数。";
        }
        OpenDayinfo = "";
    }
    void EventDifficulty()
    {
        SelectDifficultyWindow = this.gameObject.transform.Find("SelectDifficultyWindow").gameObject;
        if (UIEventListener.Get(SelectDifficultyWindow.transform.Find("window/LeftButton").gameObject).onClick == null)
        {
            UIEventListener.Get(SelectDifficultyWindow.transform.Find("window/LeftButton").gameObject).onClick += delegate(GameObject go)
            {
                MoveCheckDifficulty.GetComponent<UIScrollBar>().value -= 1;
            };
        }
        if (UIEventListener.Get(SelectDifficultyWindow.transform.Find("window/RightButton").gameObject).onClick == null)
        {
            UIEventListener.Get(SelectDifficultyWindow.transform.Find("window/RightButton").gameObject).onClick += delegate(GameObject go)
            {
                MoveCheckDifficulty.GetComponent<UIScrollBar>().value += 1;
            };
        }
        if (UIEventListener.Get(SelectDifficultyWindow.transform.Find("window/CloseButton").gameObject).onClick == null)
        {
            UIEventListener.Get(SelectDifficultyWindow.transform.Find("window/CloseButton").gameObject).onClick += delegate(GameObject go)
            {
                SelectDifficultyWindow.SetActive(false);
            };
        }

    }
    public void SetHunterInfo(int Residue, int DifficultyID, int ResidueTime)
    {
        CancelInvoke("UpdateTime");
        ResidueNumber = Residue;
        if (CharacterRecorder.instance.Vip<13)
        {
            this.gameObject.transform.Find("Residue/ResidueNumber").GetComponent<UILabel>().text = Residue + "/2";
        }
        else
        {
            this.gameObject.transform.Find("Residue/ResidueNumber").GetComponent<UILabel>().text = Residue + "/3";
        }
        
        NowTimer = ResidueTime;
        if (DifficultyID != 0)
        {
            FinishDifficulty(DifficultyID);
        }
        if (Residue > 0)
        {
            InvokeRepeating("UpdateTime", 0, 1f);
        }
        else
        {
            this.gameObject.transform.Find("Residue/Timer").gameObject.SetActive(false);
        }
    }

    public void UpdateTime()
    {
        if (NowTimer > 0)
        {
            this.gameObject.transform.Find("Residue/Timer").gameObject.SetActive(true);
            string miniteStr = (NowTimer / 60).ToString("00");
            string secondStr = (NowTimer % 60).ToString("00");
            this.gameObject.transform.Find("Residue/Timer").GetComponent<UILabel>().text = miniteStr + ":" + secondStr;
            NowTimer -= 1;
        }
        else
        {
            this.gameObject.transform.Find("Residue/Timer").gameObject.SetActive(false);
        }
    }

    public void FinishDifficulty(int Difficultyid)
    {
        for (int i = 0; i < 5; i++)
        {
            UIEventListener.Get(CheckDifficulty[i]).onClick = null;
            UIEventListener.Get(CheckDifficulty[i].transform.Find("Mop-up").gameObject).onClick = null;
            CheckDifficulty[i].transform.Find("Mop-up").gameObject.SetActive(false);
            if (i >= 1)
            {
                CheckDifficulty[i].transform.Find("Deblocking").GetComponent<UISprite>().spriteName = "icon2";
            }
        }
        if (Difficultyid != 0 && Difficultyid<6)
        {
            for (int i = Difficultyid; i < CheckDifficulty.Count; i++)
            {
                int level = 0;
                CheckDifficulty[i].transform.Find("Deblocking").GetComponent<UISprite>().spriteName = "icon2";
                if (tabe == 1)
                {
                    EverydayActivity every = TextTranslator.instance.GetEverydayActivityDicByID(i + 1);
                    level = every.NeedLevel;
                }
                else if (tabe == 2)
                {
                    EverydayActivity every = TextTranslator.instance.GetEverydayActivityDicByID(i + 7);
                    level = every.NeedLevel;

                }
                else if (tabe == 3)
                {
                    EverydayActivity every = TextTranslator.instance.GetEverydayActivityDicByID(i + 31);
                    level = every.NeedLevel;
                }
                UIEventListener.Get(CheckDifficulty[i]).onClick += delegate(GameObject go)
                {
                    UIManager.instance.OpenPromptWindow("长官，请先通过上一个难度并且等级达到" + level, PromptWindow.PromptType.Hint, null, null);
                };
            }
        }
        if (Difficultyid != 6)
        {
            for (int i = 1; i < Difficultyid; i++)
            {
                int level = 0;
                if (tabe == 1)
                {
                    EverydayActivity every = TextTranslator.instance.GetEverydayActivityDicByID(i + 1);
                    level = every.NeedLevel;
                }
                else if (tabe == 2)
                {
                    EverydayActivity every = TextTranslator.instance.GetEverydayActivityDicByID(i + 7);
                    level = every.NeedLevel;

                }
                else if (tabe == 3)
                {
                    EverydayActivity every = TextTranslator.instance.GetEverydayActivityDicByID(i + 31);
                    level = every.NeedLevel;
                }
                if (CharacterRecorder.instance.level >= level)
                {
                    CheckDifficulty[i].transform.Find("Deblocking").GetComponent<UISprite>().spriteName = "icon1";
                }
            }
            for (int i = 0; i < Difficultyid; i++)
            {
                if (i >= 1)
                {
                    if (Difficultyid > 1)
                    {
                        if (tabe != 3)
                        {
                            CheckDifficulty[i - 1].transform.Find("Mop-up").gameObject.SetActive(true);
                        }
                    }
                }
                if (tabe == 1)
                {
                    SetDifOnclick(i, 6, i + 1);
                }
                else if (tabe == 2)
                {
                    SetDifOnclick(i, 7, i + 7);

                }
                else if (tabe == 3)
                {
                    SetDifOnclick(i, 8, i + 31);
                }

                if (CheckDifficulty[i].transform.Find("Mop-up"))
                {
                    SetMopUpOnClick(i, tabe);
                }
            }
        }
        else
        {
            if (tabe != 3)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (tabe != 3)
                    {
                        CheckDifficulty[i].transform.Find("Mop-up").gameObject.SetActive(true);
                    }
                    if (tabe == 1)
                    {
                        SetDifOnclick(i, 6, i + 1);
                    }
                    else if (tabe == 2)
                    {
                        SetDifOnclick(i, 7, i + 7);

                    }
                    else if (tabe == 3)
                    {
                        SetDifOnclick(i, 8, i + 31);
                    }
                    if (CheckDifficulty[i].transform.Find("Mop-up"))
                    {
                        SetMopUpOnClick(i, tabe);
                    }
                }

            }
        }
    }
    //界面奖励物品
    public void ShowRewardList(int Number, int checkid)
    {
        DropGrid.transform.parent.GetComponent<UIPanel>().clipOffset = Vector2.zero;
        DropGrid.transform.parent.localPosition = new Vector3(-269, -36, 0);
        for (int i = 0; i < ListItem.Count; i++)
        {
            DestroyImmediate(ListItem[i]);
        }
        ListItem.Clear();
        int itemid = 0;
        for (int i = 1; i < Number + 1; i++)
        {
            GameObject obj = Instantiate(DropItem) as GameObject;
            obj.transform.parent = DropGrid.transform;
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = Vector3.zero;
            obj.SetActive(true);
            if (checkid == 1)
            {
                itemid = 90001;
            }
            else if (checkid == 2)
            {
                itemid = 10000 + i;
            }
            else if (checkid == 3)
            {
                itemid = 10014;
            }
            obj.GetComponent<UISprite>().spriteName = "Grade" + (TextTranslator.instance.GetItemByItemCode(itemid).itemGrade);
            obj.transform.Find("icon").GetComponent<UISprite>().spriteName = itemid.ToString();
            ListItem.Add(obj);
        }
        // Debug.LogError(DropScrollView.transform.position);
        // Debug.LogError(DropScrollView.transform.localPosition);
        // DropScrollView.GetComponent<UIPanel>().clipOffset = new Vector2(0, 0);
        DropGrid.GetComponent<UIGrid>().Reposition();
    }

    //数字大小写转换
    public string CheckNumShow(string id)
    {
        string num = "";
        switch (id)
        {
            case "1":
                num = "一";
                break;
            case "2":
                num = "二";
                break;
            case "3":
                num = "三";
                break;
            case "4":
                num = "四";
                break;
            case "5":
                num = "五";
                break;
            case "6":
                num = "六";
                break;
            case "7":
                num = "日";
                break;
        }
        return num;
    }

    //极限挑战英雄列表显示
    public void IntOpenDay(int TheDay, int DifID)
    {
        if (TheDay == 1 || TheDay == 3 || TheDay == 5 || TheDay == 7)
        {
            for (int i = 0; i < BuffItemList.Count; i++)
            {
                DestroyImmediate(BuffItemList[i]);
            }
            BuffItemList.Clear();
            int QuestID = 30000 + 100 * DifID + TheDay;
            WeekDay = TheDay;
            Debug.LogError("QuestID" + QuestID);
            TextTranslator.Gate GateItem = TextTranslator.instance.GetGateByID(QuestID);
            for (int i = 0; i < 5; i++)
            {
                GameObject obj = Instantiate(BuffItem) as GameObject;
                obj.transform.parent = ChallengeGrid.transform;
                obj.transform.localScale = Vector3.one;
                obj.transform.localPosition = Vector3.zero;
                obj.SetActive(true);
                string grade = "";
                string itemid = "";
                if (i == 0)
                {
                    grade = "Grade" + (TextTranslator.instance.GetItemByItemCode(GateItem.itemID1).itemGrade);
                    itemid = (GateItem.itemID1 - 10000).ToString();
                }
                else if (i == 1)
                {
                    grade = "Grade" + (TextTranslator.instance.GetItemByItemCode(GateItem.itemID2).itemGrade);
                    itemid = (GateItem.itemID2 - 10000).ToString();
                }
                else if (i == 2)
                {
                    grade = "Grade" + (TextTranslator.instance.GetItemByItemCode(GateItem.itemID3).itemGrade);
                    itemid = (GateItem.itemID3 - 10000).ToString();
                }
                else if (i == 3)
                {
                    grade = "Grade" + (TextTranslator.instance.GetItemByItemCode(GateItem.itemID4).itemGrade);
                    itemid = (GateItem.itemID4 - 10000).ToString();
                }
                else if (i == 4)
                {
                    grade = "Grade" + (TextTranslator.instance.GetItemByItemCode(GateItem.itemID5).itemGrade);
                    itemid = (GateItem.itemID5 - 10000).ToString();
                }
                obj.GetComponent<UISprite>().spriteName = grade;
                obj.transform.Find("Item").GetComponent<UISprite>().spriteName = itemid;
                BuffItemList.Add(obj);
            }
            ChallengeGrid.GetComponent<UIGrid>().repositionNow = true;
        }
        else
        {
            JoinButton.SetActive(false);
            BlackButton.SetActive(true);
            isOpenDay = true;
        }
    }

    //选择难度添加事件
    public void SetDifOnclick(int i, int FightStyle, int DicID)
    {
        EverydayActivity every = TextTranslator.instance.GetEverydayActivityDicByID(DicID);
        if (i == 0)
        {
            UIEventListener.Get(CheckDifficulty[0]).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.GuideID[9] == 10 || CharacterRecorder.instance.GuideID[13] == 10)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
                /////////////////////进行赏金猎人///////////////////////
                CharacterRecorder.instance.EveryDiffID = 1;
                UIManager.instance.OpenPanel("LoadingWindow", true);
                if (tabe != 3)
                {
                    SceneTransformer.instance.NowGateID = every.GateID;
                }
                else
                {
                    SceneTransformer.instance.NowGateID = every.GateID / 10 * 10 + WeekDay;
                }

                Debug.LogError(SceneTransformer.instance.NowGateID);
                PictureCreater.instance.StartFight();
                PictureCreater.instance.FightStyle = FightStyle;
                /////////////////////进行赏金猎人///////////////////////
            };
        }
        else if (i == 1)
        {
            UIEventListener.Get(CheckDifficulty[1]).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.level >= every.NeedLevel)
                {
                    /////////////////////进行赏金猎人///////////////////////
                    CharacterRecorder.instance.EveryDiffID = 2;
                    UIManager.instance.OpenPanel("LoadingWindow", true);
                    if (tabe != 3)
                    {
                        SceneTransformer.instance.NowGateID = every.GateID;
                    }
                    else
                    {
                        SceneTransformer.instance.NowGateID = every.GateID / 10 * 10 + WeekDay;
                    }
                    PictureCreater.instance.StartFight();
                    PictureCreater.instance.FightStyle = FightStyle;
                    /////////////////////进行赏金猎人///////////////////////
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("长官，请先通过上一个难度并且等级达到" + every.NeedLevel, PromptWindow.PromptType.Hint, null, null);
                }
            };
        }

        else if (i == 2)
        {
            UIEventListener.Get(CheckDifficulty[2]).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.level >= every.NeedLevel)
                {
                    /////////////////////进行赏金猎人///////////////////////
                    CharacterRecorder.instance.EveryDiffID = 3;
                    UIManager.instance.OpenPanel("LoadingWindow", true);
                    if (tabe != 3)
                    {
                        SceneTransformer.instance.NowGateID = every.GateID;
                    }
                    else
                    {
                        SceneTransformer.instance.NowGateID = every.GateID / 10 * 10 + WeekDay;
                    }
                    PictureCreater.instance.StartFight();
                    PictureCreater.instance.FightStyle = FightStyle;
                    /////////////////////进行赏金猎人///////////////////////
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("长官，请先通过上一个难度并且等级达到" + every.NeedLevel, PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        else if (i == 3)
        {
            UIEventListener.Get(CheckDifficulty[3]).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.level >= every.NeedLevel)
                {
                    /////////////////////进行赏金猎人///////////////////////
                    CharacterRecorder.instance.EveryDiffID = 4;
                    UIManager.instance.OpenPanel("LoadingWindow", true);
                    if (tabe != 3)
                    {
                        SceneTransformer.instance.NowGateID = every.GateID;
                    }
                    else
                    {
                        SceneTransformer.instance.NowGateID = every.GateID / 10 * 10 + WeekDay;
                    }
                    PictureCreater.instance.StartFight();
                    PictureCreater.instance.FightStyle = FightStyle;
                    /////////////////////进行赏金猎人///////////////////////
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("长官，请先通过上一个难度并且等级达到" + every.NeedLevel, PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        else if (i == 4)
        {
            UIEventListener.Get(CheckDifficulty[4]).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.level >= every.NeedLevel)
                {
                    /////////////////////进行赏金猎人///////////////////////
                    CharacterRecorder.instance.EveryDiffID = 5;
                    UIManager.instance.OpenPanel("LoadingWindow", true);
                    if (tabe != 3)
                    {
                        SceneTransformer.instance.NowGateID = every.GateID;
                    }
                    else
                    {
                        SceneTransformer.instance.NowGateID = every.GateID / 10 * 10 + WeekDay;
                    }
                    PictureCreater.instance.StartFight();
                    PictureCreater.instance.FightStyle = FightStyle;
                    /////////////////////进行赏金猎人///////////////////////
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("长官，请先通过上一个难度并且等级达到" + every.NeedLevel, PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        else if (i == 5)
        {
            UIEventListener.Get(CheckDifficulty[5]).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.level >= every.NeedLevel)
                {
                    /////////////////////进行赏金猎人///////////////////////
                    CharacterRecorder.instance.EveryDiffID = 6;
                    UIManager.instance.OpenPanel("LoadingWindow", true);
                    if (tabe != 3)
                    {
                        SceneTransformer.instance.NowGateID = every.GateID;
                    }
                    else
                    {
                        SceneTransformer.instance.NowGateID = every.GateID / 10 * 10 + WeekDay;
                    }
                    PictureCreater.instance.StartFight();
                    PictureCreater.instance.FightStyle = FightStyle;
                    /////////////////////进行赏金猎人///////////////////////
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("长官，请先通过上一个难度并且等级达到" + every.NeedLevel, PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
    }

    //扫荡难度选择
    public void SetMopUpOnClick(int i, int tabe)
    {
        string SendInfo = "";
        if (tabe == 1)
        {
            SendInfo = "1702#";
        }
        else if (tabe == 2)
        {
            SendInfo = "1703#";

        }
        else if (tabe == 3)
        {
            SendInfo = "1704#";
        }
        switch (i)
        {
            case 0:
                UIEventListener.Get(CheckDifficulty[0].transform.Find("Mop-up").gameObject).onClick += delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess(SendInfo + 1 + ";" + "100" + ";");
                };
                break;
            case 1:
                UIEventListener.Get(CheckDifficulty[1].transform.Find("Mop-up").gameObject).onClick += delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess(SendInfo + 2 + ";" + "100" + ";");
                };
                break;
            case 2:
                UIEventListener.Get(CheckDifficulty[2].transform.Find("Mop-up").gameObject).onClick += delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess(SendInfo + 3 + ";" + "100" + ";");
                };
                break;
            case 3:
                UIEventListener.Get(CheckDifficulty[3].transform.Find("Mop-up").gameObject).onClick += delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess(SendInfo + 4 + ";" + "100" + ";");
                };
                break;
            case 4:
                UIEventListener.Get(CheckDifficulty[4].transform.Find("Mop-up").gameObject).onClick += delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess(SendInfo + 5 + ";" + "100" + ";");
                };
                break;
            case 5:
                UIEventListener.Get(CheckDifficulty[5].transform.Find("Mop-up").gameObject).onClick += delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess(SendInfo + 6 + ";" + "100" + ";");
                };
                break;
        }
        if (ResidueNumber <= 0)
        {
            this.gameObject.transform.Find("SelectDifficultyWindow").gameObject.SetActive(false);
        }
    }
}
