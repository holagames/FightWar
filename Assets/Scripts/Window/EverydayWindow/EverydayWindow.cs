using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EverydayWindow : MonoBehaviour
{

    public GameObject HunterButton;
    public GameObject ThousandButton;
    public GameObject ChallengeButton;
    public GameObject MilitaryButton;
    public GameObject HunterWindow;
    public GameObject MilitaryWindow;
    //public int SetTabel = 1;
    void Start()
    {
        HunterWindow.SetActive(true);
        EventLisTener();
        HunterButton.transform.Find("HunterLock").gameObject.SetActive(false);
        UnLock();
        if (CharacterRecorder.instance.bagItemOpenWindows.ContainsKey("10011")
            || CharacterRecorder.instance.bagItemOpenWindows.ContainsKey("10012")
            || CharacterRecorder.instance.bagItemOpenWindows.ContainsKey("10013")
            || CharacterRecorder.instance.bagItemOpenWindows.ContainsKey("10014"))
        {
            //CharacterRecorder.instance.EverydayTab = 4;
            //极限挑战
            ChallengeButton_OnClick();
            RemoveKeys();
        }
        else if (CharacterRecorder.instance.bagItemOpenWindows.ContainsKey("10500"))
        {
            //花式军演
            MilitaryButton_OnClick();
            RemoveKeys();
        }
        else
        {
            SetTableWindow(CharacterRecorder.instance.EverydayTab);
        }
        
    }
    /// <summary>
    /// 移除包含的键值
    /// </summary>
    void RemoveKeys()
    {
        //小能源石
        if (CharacterRecorder.instance.bagItemOpenWindows.ContainsKey("10011"))
        {
            CharacterRecorder.instance.bagItemOpenWindows.Remove("10011");
        }
        //中能源石
        if(CharacterRecorder.instance.bagItemOpenWindows.ContainsKey("10012"))
        {
            CharacterRecorder.instance.bagItemOpenWindows.Remove("10012");
        }
        //高能源石
        if(CharacterRecorder.instance.bagItemOpenWindows.ContainsKey("10013"))
        {
            CharacterRecorder.instance.bagItemOpenWindows.Remove("10013");
        }
        //超级能源石
        if (CharacterRecorder.instance.bagItemOpenWindows.ContainsKey("10014"))
        {
            CharacterRecorder.instance.bagItemOpenWindows.Remove("10014");
        }
        //培养药剂
        if (CharacterRecorder.instance.bagItemOpenWindows.ContainsKey("10500"))
        {
            CharacterRecorder.instance.bagItemOpenWindows.Remove("10500");
        }
    }
    // Update is called once per frame
    public void EventLisTener()
    {
        if (UIEventListener.Get(HunterButton).onClick == null)
        {
            UIEventListener.Get(HunterButton).onClick += delegate(GameObject obj)
            {
                HunterButton_OnClick();
                //SetTableWindow(1);
                //if (CharacterRecorder.instance.GuideID[9] == 7)
                //{
                //    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                //}
                /////////////////////////进行赏金猎人///////////////////////
                //UIManager.instance.OpenPanel("LoadingWindow", true);
                //SceneTransformer.instance.NowGateID = 30001;
                //PictureCreater.instance.StartFight();
                //PictureCreater.instance.FightStyle = 6;
                /////////////////////////进行赏金猎人///////////////////////
            };
        }
        if (UIEventListener.Get(ThousandButton).onClick == null)
        {
            UIEventListener.Get(ThousandButton).onClick += delegate(GameObject obj)
            {
                ThousandButton_OnClick();
                //if (GameObject.Find("ThousandLock"))
                //{
                //    UIManager.instance.OpenPromptWindow("长官，通关72关开放", PromptWindow.PromptType.Hint, null, null);
                //}
                //else
                //{
                //    SetTableWindow(2);
                //    if (CharacterRecorder.instance.GuideID[13] == 7)
                //    {
                //        SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                //    }
                //    /////////////////////////进行千锤百炼///////////////////////
                //    //UIManager.instance.OpenPanel("LoadingWindow", true);
                //    //SceneTransformer.instance.NowGateID = 30011;
                //    //PictureCreater.instance.StartFight();
                //    //PictureCreater.instance.FightStyle = 7;
                //    /////////////////////////进行千锤百炼///////////////////////
                //}
            };
        }
        if (UIEventListener.Get(ChallengeButton).onClick == null)
        {
            UIEventListener.Get(ChallengeButton).onClick += delegate(GameObject obj)
            {
                //UIManager.instance.OpenPromptWindow("暂未开启", PromptWindow.PromptType.Alert, null, null);

                ChallengeButton_OnClick();

                //if (GameObject.Find("ChallengeLock"))
                //{
                //    UIManager.instance.OpenPromptWindow("长官，通关96关开放", PromptWindow.PromptType.Hint, null, null);
                //}
                //else
                //{
                //    SetTableWindow(3);
                //    if (CharacterRecorder.instance.GuideID[36] == 7)
                //    {
                //        SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                //    }
                //    //UIManager.instance.OpenPanel("LimitWindow", true);
                //}
            };
        }
        if (UIEventListener.Get(MilitaryButton).onClick == null)
        {
            UIEventListener.Get(MilitaryButton).onClick += delegate(GameObject obj)
            {
                // UIManager.instance.OpenPromptWindow("暂未开启", PromptWindow.PromptType.Alert, null, null);
                MilitaryButton_OnClick();
                //if (GameObject.Find("MilitaryLock"))
                //{
                //    UIManager.instance.OpenPromptWindow("长官，通关90关开放", PromptWindow.PromptType.Hint, null, null);
                //}
                //else
                //{
                //    SetTableWindow(4);
                //    if (CharacterRecorder.instance.GuideID[14] == 7)
                //    {
                //        SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                //    }
                //}
            };
        }
    }
    /// <summary>
    /// 进行赏金猎人
    /// </summary>
    void HunterButton_OnClick()
    {
        SetTableWindow(1);
        if (CharacterRecorder.instance.GuideID[9] == 7)
        {
            SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
        }
    }
    /// <summary>
    /// 进行千锤百炼
    /// </summary>
    void ThousandButton_OnClick()
    {
        if (GameObject.Find("ThousandLock"))
        {
            UIManager.instance.OpenPromptWindow("长官，通关72关开放", PromptWindow.PromptType.Hint, null, null);
        }
        else
        {
            SetTableWindow(2);
            if (CharacterRecorder.instance.GuideID[13] == 7)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
            /////////////////////////进行千锤百炼///////////////////////
            //UIManager.instance.OpenPanel("LoadingWindow", true);
            //SceneTransformer.instance.NowGateID = 30011;
            //PictureCreater.instance.StartFight();
            //PictureCreater.instance.FightStyle = 7;
            /////////////////////////进行千锤百炼///////////////////////
        }
    }
    /// <summary>
    /// 花式军演
    /// </summary>
    void MilitaryButton_OnClick()
    {
        if (GameObject.Find("MilitaryLock"))
        {
            UIManager.instance.OpenPromptWindow("长官，通关90关开放", PromptWindow.PromptType.Hint, null, null);
        }
        else
        {
            SetTableWindow(4);
            if (CharacterRecorder.instance.GuideID[14] == 7)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
        }
    }
    /// <summary>
    /// 极限挑战
    /// </summary>
    void ChallengeButton_OnClick()
    {
        if (GameObject.Find("ChallengeLock"))
        {
            UIManager.instance.OpenPromptWindow("长官，通关96关开放", PromptWindow.PromptType.Hint, null, null);
        }
        else
        {
            SetTableWindow(3);
            if (CharacterRecorder.instance.GuideID[36] == 7)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
            //UIManager.instance.OpenPanel("LimitWindow", true);
        }
    }

    public void SetTableWindow(int id)
    {
        if (id != 1)
        {
            GameObject.Find("BountyHunter").GetComponent<UIToggle>().value = false;
            GameObject.Find("BountyHunter").GetComponent<UIToggle>().startsActive = false;
            if (id == 2)
            {
                GameObject.Find("Thousand").GetComponent<UIToggle>().enabled = true;
                GameObject.Find("Thousand").GetComponent<UIToggle>().value = true;
                GameObject.Find("Thousand").transform.Find("ChangeButton").gameObject.SetActive(true);
            }
            else if (id == 3)
            {
                GameObject.Find("Challenge").GetComponent<UIToggle>().enabled = true;
                GameObject.Find("Challenge").GetComponent<UIToggle>().value = true;
                GameObject.Find("Challenge").transform.Find("ChangeButton").gameObject.SetActive(true);
            }
            else if (id == 4)
            {
                GameObject.Find("MilitaryExercise").GetComponent<UIToggle>().enabled = true;
                GameObject.Find("MilitaryExercise").GetComponent<UIToggle>().value = true;
                GameObject.Find("MilitaryExercise").transform.Find("ChangeButton").gameObject.SetActive(true);
            }
        }
        else
        {
            GameObject.Find("BountyHunter").GetComponent<UIToggle>().value = true;
            GameObject.Find("BountyHunter").GetComponent<UIToggle>().startsActive = true;
        }
        switch (id)
        {
            case 1:
                NetworkHandler.instance.SendProcess("1701#1;");
                HunterWindow.SetActive(true);
                HunterWindow.GetComponent<HunterWindow>().IsShowInfo(id);
                MilitaryWindow.SetActive(false);
                break;
            case 2:
                NetworkHandler.instance.SendProcess("1701#2;");
                HunterWindow.SetActive(true);
                HunterWindow.GetComponent<HunterWindow>().IsShowInfo(id);
                MilitaryWindow.SetActive(false);
                break;
            case 3:
                NetworkHandler.instance.SendProcess("1701#6;");
                HunterWindow.SetActive(true);
                HunterWindow.GetComponent<HunterWindow>().IsShowInfo(id);
                MilitaryWindow.SetActive(false);
                break;
            case 4:
                HunterWindow.SetActive(false);
                MilitaryWindow.SetActive(true);
                MilitaryWindow.GetComponent<MilitaryWindow>().ImpregnableItem = TextTranslator.instance.GetEverydayActivityDicByID(13);
                MilitaryWindow.GetComponent<MilitaryWindow>().AttackDefItem = TextTranslator.instance.GetEverydayActivityDicByID(19);
                MilitaryWindow.GetComponent<MilitaryWindow>().StormItem = TextTranslator.instance.GetEverydayActivityDicByID(25);
                NetworkHandler.instance.SendProcess("1701#3;");
                NetworkHandler.instance.SendProcess("1701#4;");
                NetworkHandler.instance.SendProcess("1701#5;");
                break;
        }
        CharacterRecorder.instance.EverydayTab = id;
    }

    public void UnLock()
    {
        if (CharacterRecorder.instance.lastGateID>=10073)
        {
            ThousandButton.transform.Find("ThousandLock").gameObject.SetActive(false);
        }
        if (CharacterRecorder.instance.lastGateID >= 10091)
        {
            MilitaryButton.transform.Find("MilitaryLock").gameObject.SetActive(false);
        }
        if (CharacterRecorder.instance.lastGateID >= 10097)
        {
            ChallengeButton.transform.Find("ChallengeLock").gameObject.SetActive(false);

        }
    }
}
