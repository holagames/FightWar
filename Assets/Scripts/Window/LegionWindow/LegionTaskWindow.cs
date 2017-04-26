using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionTaskWindow : MonoBehaviour
{
    public GameObject CloseButton;
    public GameObject RefreshButton;
    public GameObject uiGride;
    public GameObject taskItem;
    public UILabel leftFreeRefreshLabel;
    public UILabel costGoldLabel;
    public UILabel LeftComplayTimes;
    private int maxFreeTimes = 3;//3次免费刷新次数
    private int refreshTimes = 0;//刷新次数
    private int todayLeftTimes;//今日可完成次数
    private int costDiomond;//刷新花费钻石

    private bool hasFiveStar = false;
    // Use this for initialization
    void Start()
    {
        //UIManager.instance.CountSystem(UIManager.Systems.军团捐献);
        //UIManager.instance.UpdateSystems(UIManager.Systems.军团捐献);

        NetworkHandler.instance.SendProcess("8401#;");

        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }
        if (UIEventListener.Get(RefreshButton).onClick == null)
        {
            UIEventListener.Get(RefreshButton).onClick += delegate(GameObject go)
            {
                //NetworkHandler.instance.SendProcess("8403#;");
                //int RefreshCost = 500;
                //UIManager.instance.OpenPromptWindowWithCost("是否刷新军团任务？", 11, RefreshCost, PromptWindow.PromptType.Confirm, ConfirmButtonClick, null);
                Market _myMarket = TextTranslator.instance.GetMarketByBuyCount(refreshTimes + 1);
                costDiomond = _myMarket == null ? 5000 : _myMarket.LegionTaskRefreshDiamond;
                //Debug.Log(costDiomond + "...." + CharacterRecorder.instance.gold);
                if (todayLeftTimes <= 0)
                {
                    UIManager.instance.OpenPromptWindow("今日任务次数已用完", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                if (hasFiveStar)
                {
                    UIManager.instance.OpenPromptWindow("请先完成5星任务", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                if (costDiomond > 0 && CharacterRecorder.instance.gold > costDiomond)
                {
                    ConfirmButtonClick();
                    //UIManager.instance.OpenPromptWindow(string.Format("是否花费{0}金币刷新一次\n军团任务？", costDiomond), PromptWindow.PromptType.Confirm, ConfirmButtonClick, null);
                }
                else if (costDiomond == 0)
                {
                    NetworkHandler.instance.SendProcess("8403#;");
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("金币不足", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }

        // SetBasicInfo();
    }

    public void SetLegionTaskWindow(int todayLeftTimes, int refreshTimes, BetterList<int> taskIdList)
    {
        ClearUIGride();
        this.todayLeftTimes = todayLeftTimes;
        this.refreshTimes = refreshTimes;
        if (refreshTimes < maxFreeTimes)
        {
            leftFreeRefreshLabel.gameObject.SetActive(true);
            costGoldLabel.gameObject.SetActive(false);
            leftFreeRefreshLabel.text = (maxFreeTimes - refreshTimes).ToString();
        }
        else
        {
            leftFreeRefreshLabel.gameObject.SetActive(false);
            costGoldLabel.gameObject.SetActive(true);
            Market _myMarket = TextTranslator.instance.GetMarketByBuyCount(refreshTimes + 1);
            costDiomond = _myMarket == null ? 5000 : _myMarket.LegionTaskRefreshDiamond;
            costGoldLabel.text = costDiomond.ToString();
        }
        LeftComplayTimes.text = todayLeftTimes.ToString();
         int starCount = 0;
        for (int i = 0; i < taskIdList.size; i++)
        {
            if (taskIdList[i] == 0)
            {
                taskIdList[i] += (i + 1);
            }
            LegionTask _myLegionTask = TextTranslator.instance.GetLegionTaskByID(taskIdList[i]);
            GameObject obj = NGUITools.AddChild(uiGride, taskItem);
            obj.GetComponent<LegionTaskItem>().SetLegionTaskItem(_myLegionTask);
            if (_myLegionTask.Color >= 4)
            {
                //有5星任务。
                starCount++;
            }
        }
        if (starCount>0)
        {
            this.hasFiveStar = true;
        }
        else
        {
            this.hasFiveStar = false;
        }
        uiGride.GetComponent<UIGrid>().repositionNow = true;
    }
    public void OpenGainWindow(List<Item> _itemList)
    {
        UIManager.instance.OpenSinglePanel("AdvanceWindow", false);
        GameObject.Find("AdvanceWindow").layer = 11;
        AdvanceWindow aw = GameObject.Find("AdvanceWindow").GetComponent<AdvanceWindow>();
        aw.SetInfo(AdvanceWindowType.GainResult, null, null, null, null, _itemList);
    }
    void ClearUIGride()
    {
        for (int i = uiGride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGride.transform.GetChild(i).gameObject);
        }
    }

    void ConfirmButtonClick()
    {
        NetworkHandler.instance.SendProcess("8403#;");
    }
}
