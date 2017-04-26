using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SelectDifficultyWindow : MonoBehaviour
{
    public List<GameObject> CheckDifficulty = new List<GameObject>();
    public UIScrollBar MoveCheckDifficulty;
    public GameObject Window;
    // Use this for initialization
    void Start()
    {
        EventDifficulty();
    }

    void EventDifficulty()
    {
        if (UIEventListener.Get(Window.transform.Find("LeftButton").gameObject).onClick == null)
        {
            UIEventListener.Get(Window.transform.Find("LeftButton").gameObject).onClick += delegate(GameObject go)
            {
                MoveCheckDifficulty.GetComponent<UIScrollBar>().value -= 1;
            };
        }
        if (UIEventListener.Get(Window.transform.Find("RightButton").gameObject).onClick == null)
        {
            UIEventListener.Get(Window.transform.Find("RightButton").gameObject).onClick += delegate(GameObject go)
            {
                MoveCheckDifficulty.GetComponent<UIScrollBar>().value += 1;
            };
        }
        if (UIEventListener.Get(Window.transform.Find("CloseButton").gameObject).onClick == null)
        {
            UIEventListener.Get(Window.transform.Find("CloseButton").gameObject).onClick += delegate(GameObject go)
            {
                gameObject.SetActive(false);
            };
        }
    }

    public void FinishDifficulty(int Difficultyid, int tabe)
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
        if (Difficultyid != 0)
        {
            for (int i = Difficultyid; i < CheckDifficulty.Count; i++)
            {
                CheckDifficulty[i].transform.Find("Deblocking").GetComponent<UISprite>().spriteName = "icon2";
                int level = 0;
                if (tabe == 1)
                {
                    EverydayActivity every = TextTranslator.instance.GetEverydayActivityDicByID(i + 13);
                    level = every.NeedLevel;
                }
                else if (tabe == 2)
                {
                    EverydayActivity every = TextTranslator.instance.GetEverydayActivityDicByID(i + 19);
                    level = every.NeedLevel;

                }
                else if (tabe == 3)
                {
                    EverydayActivity every = TextTranslator.instance.GetEverydayActivityDicByID(i + 25);
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
                    EverydayActivity every = TextTranslator.instance.GetEverydayActivityDicByID(i + 13);
                    level = every.NeedLevel;
                }
                else if (tabe == 2)
                {
                    EverydayActivity every = TextTranslator.instance.GetEverydayActivityDicByID(i + 19);
                    level = every.NeedLevel;

                }
                else if (tabe == 3)
                {
                    EverydayActivity every = TextTranslator.instance.GetEverydayActivityDicByID(i + 25);
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
                    if (Difficultyid > 1 && Difficultyid < 6)
                    {
                        CheckDifficulty[i - 1].transform.Find("Mop-up").gameObject.SetActive(true);
                    }
                }
                if (tabe == 1)
                {
                    SetDifOnclick(i, 9, i + 13);
                }
                else if (tabe == 2)
                {
                    SetDifOnclick(i, 10, i + 19);

                }
                else if (tabe == 3)
                {
                    SetDifOnclick(i, 11, i + 25);

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


    //选择难度添加事件
    public void SetDifOnclick(int i, int FightStyle, int DicID)
    {
        EverydayActivity every = TextTranslator.instance.GetEverydayActivityDicByID(DicID);
        if (i == 0)
        {
            UIEventListener.Get(CheckDifficulty[0]).onClick += delegate(GameObject go)
            {
                CharacterRecorder.instance.EveryDiffID = 1;
                UIManager.instance.OpenPanel("LoadingWindow", true);
                SceneTransformer.instance.NowGateID = every.GateID;
                PictureCreater.instance.StartFight();
                PictureCreater.instance.FightStyle = FightStyle;
            };
        }
        else if (i == 1)
        {
            UIEventListener.Get(CheckDifficulty[1]).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.level >= every.NeedLevel)
                {

                    CharacterRecorder.instance.EveryDiffID = 2;
                    UIManager.instance.OpenPanel("LoadingWindow", true);
                    SceneTransformer.instance.NowGateID = every.GateID;
                    PictureCreater.instance.StartFight();
                    PictureCreater.instance.FightStyle = FightStyle;

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

                    CharacterRecorder.instance.EveryDiffID = 3;
                    UIManager.instance.OpenPanel("LoadingWindow", true);
                    SceneTransformer.instance.NowGateID = every.GateID;
                    PictureCreater.instance.StartFight();
                    PictureCreater.instance.FightStyle = FightStyle;

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

                    CharacterRecorder.instance.EveryDiffID = 4;
                    UIManager.instance.OpenPanel("LoadingWindow", true);
                    SceneTransformer.instance.NowGateID = every.GateID;
                    PictureCreater.instance.StartFight();
                    PictureCreater.instance.FightStyle = FightStyle;

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

                    CharacterRecorder.instance.EveryDiffID = 5;
                    UIManager.instance.OpenPanel("LoadingWindow", true);
                    SceneTransformer.instance.NowGateID = every.GateID;
                    PictureCreater.instance.StartFight();
                    PictureCreater.instance.FightStyle = FightStyle;

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

                    CharacterRecorder.instance.EveryDiffID = 6;
                    UIManager.instance.OpenPanel("LoadingWindow", true);
                    SceneTransformer.instance.NowGateID = every.GateID;
                    PictureCreater.instance.StartFight();
                    PictureCreater.instance.FightStyle = FightStyle;
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
            SendInfo = "3;";
        }
        else if (tabe == 2)
        {
            SendInfo = "4;";

        }
        else if (tabe == 3)
        {
            SendInfo = "5;";
        }
        MilitaryWindow mw = GameObject.Find("MilitaryWindow").GetComponent<MilitaryWindow>();
        switch (i)
        {
            case 0:
                UIEventListener.Get(CheckDifficulty[0].transform.Find("Mop-up").gameObject).onClick += delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess("1705#1" + ";" + SendInfo);

                    if ((mw.tabe == 1 && mw.ImpregnableResidueNumber <= 0) || (mw.tabe == 2 && mw.AttackDefResidueNumber <= 0) || (mw.tabe == 3 && mw.StormResidueNumber <= 0))
                    {
                        this.gameObject.SetActive(false);
                    }
                };
                break;
            case 1:
                UIEventListener.Get(CheckDifficulty[1].transform.Find("Mop-up").gameObject).onClick += delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess("1705#2" + ";" + SendInfo);

                    if ((mw.tabe == 1 && mw.ImpregnableResidueNumber <= 0) || (mw.tabe == 2 && mw.AttackDefResidueNumber <= 0) || (mw.tabe == 3 && mw.StormResidueNumber <= 0))
                    {
                        this.gameObject.SetActive(false);
                    }
                };
                break;
            case 2:
                UIEventListener.Get(CheckDifficulty[2].transform.Find("Mop-up").gameObject).onClick += delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess("1705#3" + ";" + SendInfo);

                    if ((mw.tabe == 1 && mw.ImpregnableResidueNumber <= 0) || (mw.tabe == 2 && mw.AttackDefResidueNumber <= 0) || (mw.tabe == 3 && mw.StormResidueNumber <= 0))
                    {
                        this.gameObject.SetActive(false);
                    }
                };
                break;
            case 3:
                UIEventListener.Get(CheckDifficulty[3].transform.Find("Mop-up").gameObject).onClick += delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess("1705#4" + ";" + SendInfo);

                    if ((mw.tabe == 1 && mw.ImpregnableResidueNumber <= 0) || (mw.tabe == 2 && mw.AttackDefResidueNumber <= 0) || (mw.tabe == 3 && mw.StormResidueNumber <= 0))
                    {
                        this.gameObject.SetActive(false);
                    }
                };
                break;
            case 4:
                UIEventListener.Get(CheckDifficulty[4].transform.Find("Mop-up").gameObject).onClick += delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess("1705#5" + ";" + SendInfo);
                    if ((mw.tabe == 1 && mw.ImpregnableResidueNumber <= 0) || (mw.tabe == 2 && mw.AttackDefResidueNumber <= 0) || (mw.tabe == 3 && mw.StormResidueNumber <= 0))
                    {
                        this.gameObject.SetActive(false);
                    }
                };
                break;
            case 5:
                UIEventListener.Get(CheckDifficulty[5].transform.Find("Mop-up").gameObject).onClick += delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess("1705#6" + ";" + SendInfo);
                    if ((mw.tabe == 1 && mw.ImpregnableResidueNumber <= 0) || (mw.tabe == 2 && mw.AttackDefResidueNumber <= 0) || (mw.tabe == 3 && mw.StormResidueNumber <= 0))
                    {
                        this.gameObject.SetActive(false);
                    }
                };
                break;
        }
    }

}
