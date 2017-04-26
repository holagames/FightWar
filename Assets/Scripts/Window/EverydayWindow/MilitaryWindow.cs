using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MilitaryWindow : MonoBehaviour
{


    public GameObject ImpregnableButton;
    public GameObject AttackDefButton;
    public GameObject StormButton;
    public GameObject QusButton;

    public GameObject HuterMessageObj;
    public int ResidueNumber;
    public GameObject SelectDifficultyWindow;
    public int CheckID;
    public GameObject EverydayWindow;
    public int tabe;
    //奖励物品
    public UIGrid ImpregnableGrid;
    public UIGrid AttackDefGrid;
    public UIGrid StormGrid;
    //活动最高难度
    public int ImpregnableID;
    public int AttackDefID;
    public int StormID;
    //开放时间
    public bool ImpregnableAwake;
    public bool AttackDefAwake;
    public bool StormAwake;
    //剩余次数
    public int ImpregnableResidueNumber;
    public int AttackDefResidueNumber;
    public int StormResidueNumber;
    // Use this for initialization

    public EverydayActivity ImpregnableItem;
    public EverydayActivity AttackDefItem;
    public EverydayActivity StormItem;
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.花式军演);
        UIManager.instance.UpdateSystems(UIManager.Systems.花式军演);
        //ImpregnableItem = TextTranslator.instance.GetEverydayActivityDicByID(13);
        //AttackDefItem = TextTranslator.instance.GetEverydayActivityDicByID(19);
        //StormItem = TextTranslator.instance.GetEverydayActivityDicByID(25);
        UIEventListener.Get(ImpregnableButton).onClick += delegate(GameObject go)
        {
            if (ImpregnableResidueNumber == 0)
            {
                UIManager.instance.OpenPromptWindow("次数用完了,明天再来吧", PromptWindow.PromptType.Hint, null, null);
            }
            else if (ImpregnableAwake == false)
            {
                UIManager.instance.OpenPromptWindow("副本开放时间未到", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                tabe = 1;
                SelectDifficultyWindow.SetActive(true);
                SelectDifficultyWindow.GetComponent<SelectDifficultyWindow>().FinishDifficulty(ImpregnableID, tabe);
            }
        };
        UIEventListener.Get(AttackDefButton).onClick += delegate(GameObject go)
        {
            if (AttackDefResidueNumber == 0)
            {
                UIManager.instance.OpenPromptWindow("次数用完了,明天再来吧", PromptWindow.PromptType.Hint, null, null);
            }
            else if (AttackDefAwake == false)
            {
                UIManager.instance.OpenPromptWindow("副本开放时间未到", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                tabe = 2;
                SelectDifficultyWindow.SetActive(true);
                SelectDifficultyWindow.GetComponent<SelectDifficultyWindow>().FinishDifficulty(AttackDefID, tabe);
            }
        };
        UIEventListener.Get(StormButton).onClick += delegate(GameObject go)
        {
            if (StormResidueNumber == 0)
            {
                UIManager.instance.OpenPromptWindow("次数用完了,明天再来吧", PromptWindow.PromptType.Hint, null, null);
            }
            else if (StormAwake == false)
            {
                UIManager.instance.OpenPromptWindow("副本开放时间未到", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                tabe = 3;
                SelectDifficultyWindow.SetActive(true);
                SelectDifficultyWindow.GetComponent<SelectDifficultyWindow>().FinishDifficulty(StormID, tabe);
            }
        };
        if (UIEventListener.Get(QusButton).onClick == null)
        {
            UIEventListener.Get(QusButton).onClick += delegate(GameObject go)
            {
                HuterMessageObj.SetActive(true);
                HouterMessage();
            };
        }
        ShowRewardList();
    }

    //活动1 
    public void SetImpregnableInfo(int Residue, int DifficultyID, int ResidueTime, int OpenAwake)
    {
        ImpregnableResidueNumber = Residue;
        string[] openday = ImpregnableItem.OpenWeek.Split('$');
        for (int i = 0; i < openday.Length; i++)
        {
            if (OpenAwake == int.Parse(openday[i]))
            {
                ImpregnableAwake = true;
                ImpregnableButton.transform.Find("Residue").gameObject.SetActive(true);
                ImpregnableButton.transform.Find("Black").gameObject.SetActive(false);
                //ImpregnableButton.GetComponent<UITexture>().color = new Color(1f, 1f, 1f, 1f);
                if (CharacterRecorder.instance.Vip < 13)
                {
                    ImpregnableButton.transform.Find("Residue").GetComponent<UILabel>().text = "剩余次数:" + Residue + "/2";
                }
                else
                {
                    ImpregnableButton.transform.Find("Residue").GetComponent<UILabel>().text = "剩余次数:" + Residue + "/3";
                }
                break;
            }
            else
            {
                ImpregnableAwake = false;
                ImpregnableButton.GetComponent<UIButton>().enabled = false;
                ImpregnableButton.transform.Find("Black").gameObject.SetActive(true);
                //ImpregnableButton.GetComponent<UITexture>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
                ImpregnableButton.transform.Find("Residue").gameObject.SetActive(false);
            }
        }
        ImpregnableID = DifficultyID;
        if (gameObject.transform.Find("SelectWindow") != null && Residue==0)
        {
            SelectDifficultyWindow.SetActive(false);
        }
    }
    //活动2
    public void SetAttackDefInfo(int Residue, int DifficultyID, int ResidueTime, int OpenAwake)
    {
        AttackDefResidueNumber = Residue;
        string[] openday = AttackDefItem.OpenWeek.Split('$');
        for (int i = 0; i < openday.Length; i++)
        {
            if (OpenAwake == int.Parse(openday[i]))
            {
                AttackDefAwake = true;
                AttackDefButton.transform.Find("Residue").gameObject.SetActive(true);
                AttackDefButton.transform.Find("Black").gameObject.SetActive(false);
                //AttackDefButton.GetComponent<UITexture>().color = new Color(1f, 1f, 1f, 1f);
                // AttackDefButton.transform.Find("Residue").GetComponent<UILabel>().text = "剩余次数:" + Residue + "/2";
                if (CharacterRecorder.instance.Vip < 13)
                {
                    AttackDefButton.transform.Find("Residue").GetComponent<UILabel>().text = "剩余次数:" + Residue + "/2";
                }
                else
                {
                    AttackDefButton.transform.Find("Residue").GetComponent<UILabel>().text = "剩余次数:" + Residue + "/3";
                }
                break;
            }
            else
            {
                AttackDefAwake = false;
                AttackDefButton.GetComponent<UIButton>().enabled = false;
                AttackDefButton.transform.Find("Black").gameObject.SetActive(true);
                //AttackDefButton.GetComponent<UITexture>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
                AttackDefButton.transform.Find("Residue").gameObject.SetActive(false);
            }
        }
        AttackDefID = DifficultyID;
        if (gameObject.transform.Find("SelectWindow") != null && Residue == 0)
        {
            SelectDifficultyWindow.SetActive(false);
        }
    }
    //活动3
    public void SetStormInfo(int Residue, int DifficultyID, int ResidueTime, int OpenAwake)
    {
        StormResidueNumber = Residue;
        string[] openday = StormItem.OpenWeek.Split('$');
        for (int i = 0; i < openday.Length; i++)
        {
            if (OpenAwake == int.Parse(openday[i]))
            {
                StormAwake = true;
                StormButton.transform.Find("Residue").gameObject.SetActive(true);
                StormButton.transform.Find("Black").gameObject.SetActive(false);
                //StormButton.GetComponent<UITexture>().color = new Color(1f, 1f, 1f, 1f);
                if (CharacterRecorder.instance.Vip<13)
                {
                    StormButton.transform.Find("Residue").GetComponent<UILabel>().text = "剩余次数:" + Residue + "/2";
                }
                else
                {
                    StormButton.transform.Find("Residue").GetComponent<UILabel>().text = "剩余次数:" + Residue + "/3";
                }
                
                break;
            }
            else
            {
                StormAwake = false;
                StormButton.GetComponent<UIButton>().enabled = false;
                StormButton.transform.Find("Black").gameObject.SetActive(true);
                //StormButton.GetComponent<UITexture>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
                StormButton.transform.Find("Residue").gameObject.SetActive(false);
            }
        }
        StormID = DifficultyID;
        if (gameObject.transform.Find("SelectWindow") != null && Residue == 0)
        {
            SelectDifficultyWindow.SetActive(false);
        }
    }

    // Update is called once per frame
    public void HouterMessage()
    {
        HuterMessageObj.transform.Find("Message/ResultLabel").GetComponent<UILabel>().text = "";
        //EverydayActivity every = TextTranslator.instance.GetEverydayActivityDicByID(7);
        //HuterMessageObj.transform.Find("TitleLabel").GetComponent<UILabel>().text = "花式军演";
        //string[] openday = every.OpenWeek.Split('$');
        //for (int i = 0; i < openday.Length; i++)
        //{
        //    if (i != openday.Length - 1)
        //    {
        //        OpenDayinfo += CheckNumShow(openday[i]) + "、";
        //    }
        //    else
        //    {
        //        OpenDayinfo += CheckNumShow(openday[i]) + "。";
        //    }
        //}
        HuterMessageObj.transform.Find("Message/TitleLabel").GetComponent<UILabel>().text = "花式军演";
        //HuterMessageObj.transform.Find("Message/OpenTime").GetComponent<UILabel>().text = "每周一、二、三、四、五、六、日";
        HuterMessageObj.transform.Find("Message/OpenTime").GetComponent<UILabel>().text = "每天";
        HuterMessageObj.transform.Find("Message/ResultLabel").GetComponent<UILabel>().text = "1.通关90关可以参加，每天2次，每天凌晨5点重置挑战次数。\n2.每个关卡限制一种类型的英雄无法上场。\n3.固如金汤主要掉落脑丶白金，可用于技能升级。\n4.攻守兼备主要掉落培养药剂，可用于角色培养。\n5.弃防强攻主要掉落精炼石，可用于饰品精炼。";

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
    }
    public void ShowRewardList()
    {
        ImpregnableButton.transform.Find("OpenLabel").GetComponent<UILabel>().text = "开放时间：";
        AttackDefButton.transform.Find("OpenLabel").GetComponent<UILabel>().text = "开放时间：";
        StormButton.transform.Find("OpenLabel").GetComponent<UILabel>().text = "开放时间：";
        ImpregnableGrid.transform.Find("AwardGradeOne").GetComponent<UISprite>().spriteName = "Grade" + (TextTranslator.instance.GetItemByItemCode(ImpregnableItem.ShowBonusID1).itemGrade);
        ImpregnableGrid.transform.Find("AwardGradeOne/Awaditem").GetComponent<UISprite>().spriteName = ImpregnableItem.ShowBonusID1.ToString();
        ImpregnableGrid.transform.Find("AwardGradeTwo").GetComponent<UISprite>().spriteName = "Grade" + (TextTranslator.instance.GetItemByItemCode(ImpregnableItem.ShowBonusID2).itemGrade);
        ImpregnableGrid.transform.Find("AwardGradeTwo/Awaditem").GetComponent<UISprite>().spriteName = ImpregnableItem.ShowBonusID2.ToString();
        ImpregnableGrid.transform.Find("AwardGradeThree").GetComponent<UISprite>().spriteName = "Grade" + (TextTranslator.instance.GetItemByItemCode(ImpregnableItem.ShowBonusID3).itemGrade);
        ImpregnableGrid.transform.Find("AwardGradeThree/Awaditem").GetComponent<UISprite>().spriteName = ImpregnableItem.ShowBonusID3.ToString();
        string[] openday = ImpregnableItem.OpenWeek.Split('$');
        for (int i = 0; i < openday.Length; i++)
        {
            if (i != openday.Length - 1)
            {
                ImpregnableButton.transform.Find("OpenLabel").GetComponent<UILabel>().text += CheckNumShow(openday[i]) + "、";
            }
            else
            {
                ImpregnableButton.transform.Find("OpenLabel").GetComponent<UILabel>().text += CheckNumShow(openday[i]);
            }
        }

        AttackDefGrid.transform.Find("AwardGradeOne").GetComponent<UISprite>().spriteName = "Grade" + (TextTranslator.instance.GetItemByItemCode(AttackDefItem.ShowBonusID1).itemGrade);
        AttackDefGrid.transform.Find("AwardGradeOne/Awaditem").GetComponent<UISprite>().spriteName = AttackDefItem.ShowBonusID1.ToString();
        AttackDefGrid.transform.Find("AwardGradeTwo").GetComponent<UISprite>().spriteName = "Grade" + (TextTranslator.instance.GetItemByItemCode(AttackDefItem.ShowBonusID2).itemGrade);
        AttackDefGrid.transform.Find("AwardGradeTwo/Awaditem").GetComponent<UISprite>().spriteName = AttackDefItem.ShowBonusID2.ToString();
        AttackDefGrid.transform.Find("AwardGradeThree").GetComponent<UISprite>().spriteName = "Grade" + (TextTranslator.instance.GetItemByItemCode(AttackDefItem.ShowBonusID3).itemGrade);
        AttackDefGrid.transform.Find("AwardGradeThree/Awaditem").GetComponent<UISprite>().spriteName = AttackDefItem.ShowBonusID3.ToString();
        string[] AttackDefopenday = AttackDefItem.OpenWeek.Split('$');
        for (int i = 0; i < AttackDefopenday.Length; i++)
        {
            if (i != AttackDefopenday.Length - 1)
            {
                AttackDefButton.transform.Find("OpenLabel").GetComponent<UILabel>().text += CheckNumShow(AttackDefopenday[i]) + "、";
            }
            else
            {
                AttackDefButton.transform.Find("OpenLabel").GetComponent<UILabel>().text += CheckNumShow(AttackDefopenday[i]);
            }
        }

        StormGrid.transform.Find("AwardGradeOne").GetComponent<UISprite>().spriteName = "Grade" + (TextTranslator.instance.GetItemByItemCode(StormItem.ShowBonusID1).itemGrade);
        StormGrid.transform.Find("AwardGradeOne/Awaditem").GetComponent<UISprite>().spriteName = StormItem.ShowBonusID1.ToString();
        StormGrid.transform.Find("AwardGradeTwo").GetComponent<UISprite>().spriteName = "Grade" + (TextTranslator.instance.GetItemByItemCode(StormItem.ShowBonusID2).itemGrade);
        StormGrid.transform.Find("AwardGradeTwo/Awaditem").GetComponent<UISprite>().spriteName = StormItem.ShowBonusID2.ToString();
        StormGrid.transform.Find("AwardGradeThree").GetComponent<UISprite>().spriteName = "Grade" + (TextTranslator.instance.GetItemByItemCode(StormItem.ShowBonusID3).itemGrade);
        StormGrid.transform.Find("AwardGradeThree/Awaditem").GetComponent<UISprite>().spriteName = StormItem.ShowBonusID3.ToString();
        string[] Stromopenday = StormItem.OpenWeek.Split('$');
        for (int i = 0; i < Stromopenday.Length; i++)
        {
            if (i != Stromopenday.Length - 1)
            {
                StormButton.transform.Find("OpenLabel").GetComponent<UILabel>().text += CheckNumShow(Stromopenday[i]) + "、";
            }
            else
            {
                StormButton.transform.Find("OpenLabel").GetComponent<UILabel>().text += CheckNumShow(Stromopenday[i]);
            }
        }
        ImpregnableGrid.GetComponent<UIGrid>().repositionNow = true;
        AttackDefGrid.GetComponent<UIGrid>().repositionNow = true;
        StormGrid.GetComponent<UIGrid>().repositionNow = true;
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
}

