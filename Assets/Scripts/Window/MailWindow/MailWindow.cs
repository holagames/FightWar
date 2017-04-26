using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MailWindow : MonoBehaviour
{
    [SerializeField]
    private UILabel mailCountLabel;
    public GameObject uiGrid;
    public GameObject MailItemParfeb;
    [SerializeField]
    private GameObject OneKeyGetButton;
    public GameObject LegionMailButton;
    //public GameObject MailInfoBoard;
    public static bool isFirstRead = false;
    private List<GameObject> MailItemList = new List<GameObject>();
    private List<MailItemData> mailDataList = new List<MailItemData>();
    private int unReadCount = 0;
    // Use this for initialization
    void OnEnable()
    {

    }

    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.邮件);
        UIManager.instance.UpdateSystems(UIManager.Systems.邮件);

        if (CharacterRecorder.instance.isLegionChairman)
        {
            LegionMailButton.SetActive(true);
            if (UIEventListener.Get(LegionMailButton).onClick == null)
            {
                UIEventListener.Get(LegionMailButton).onClick += delegate(GameObject obj)
                {
                    UIManager.instance.OpenSinglePanel("LegionMailWindow", false);
                };
            }
        }
        else
        {
            LegionMailButton.SetActive(false);
        }
        NetworkHandler.instance.SendProcess("9001#;");
        if (UIEventListener.Get(GameObject.Find("MailCloseButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("MailCloseButton")).onClick += delegate(GameObject obj)
            {
                GameObject main = GameObject.Find("MainWindow");
                if (main != null)
                {
                    main.GetComponent<MainWindow>().UpdateRedPoint(8);
                }
                UIManager.instance.BackUI();
            };
        }
        if (UIEventListener.Get(OneKeyGetButton).onClick == null)
        {
            UIEventListener.Get(OneKeyGetButton).onClick += delegate(GameObject obj)
            {
                //StartCoroutine(ClickOneKeyGetButtton());
                NetworkHandler.instance.SendProcess("9004#;");
            };
        }

    }
    public void OpenGainWindow(List<Item> _itemList)
    {
        UIManager.instance.OpenSinglePanel("AdvanceWindow", false);
        GameObject.Find("AdvanceWindow").layer = 11;
        AdvanceWindow aw = GameObject.Find("AdvanceWindow").GetComponent<AdvanceWindow>();
        aw.SetInfo(AdvanceWindowType.GainResult, null, null, null, null, _itemList);
    }
    public void UpdateMail()
    {
        for (int i = 0; i < MailItemList.Count; i++)
        {
            DestroyImmediate(MailItemList[i]);
        }
        MailItemList.Clear();
        NetworkHandler.instance.SendProcess("9001#;");
    }
    public void SetMailWindowInfo(List<MailItemData> _mailList)
    {
        unReadCount = 0;
        mailDataList = _mailList;
        ClearUiGride();
        for (int i = 0; i < _mailList.Count; i++)
        {
            GameObject go = NGUITools.AddChild(uiGrid, MailItemParfeb);
            go.GetComponent<MailItem>().SetMailItemInfo(_mailList[i]);
            MailItemList.Add(go);
            if (_mailList[i].isRead == 0)
            {
                unReadCount += 1;
            }
        }
        uiGrid.GetComponent<UIGrid>().Reposition();
        mailCountLabel.text = string.Format("{0}/{1}", unReadCount, _mailList.Count);

        if (unReadCount == 0)
        {
            OneKeyGetButton.GetComponent<UIButton>().isEnabled = false;
            OneKeyGetButton.transform.FindChild("Label").GetComponent<UILabel>().effectColor = Color.grey;
        }
        if (_mailList.Count > 0)
        {
            CharacterRecorder.instance.SetRedPoint(8, true);
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(8, false);
        }
    }
    void ClearUiGride()
    {
        for (int i = uiGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGrid.transform.GetChild(i).gameObject);
        }
    }
    IEnumerator ClickOneKeyGetButtton()
    {
        if (GetOneKeyButtonUsefullState() == true)
        {
            for (int i = 0; i < mailDataList.Count; i++)
            {
                if (mailDataList[i].itemIcon != 0 && mailDataList[i].isGet == 0)
                {
                    NetworkHandler.instance.SendProcess("9003#" + mailDataList[i].mailID + ";");
                    yield return new WaitForSeconds(0.01f);
                }
            }
        }
        else
        {
            UIManager.instance.OpenPromptWindow("没有附件可领取", PromptWindow.PromptType.Hint, null, null);
        }
    }
    bool GetOneKeyButtonUsefullState()
    {
        for (int i = 0; i < mailDataList.Count; i++)
        {
            if (mailDataList[i].itemIcon != 0 && mailDataList[i].isGet == 0)
            {
                return true;
            }
        }
        return false;
    }
    public static DateTime UnixTimestampToDateTime(string timeStemp)
    {
        DateTime dtstart = TimeZone.CurrentTimeZone.ToLocalTime(new DateTime(1970, 1, 1));
        long timestamp = long.Parse(timeStemp + "0000000");
        TimeSpan toNow = new TimeSpan(timestamp);
        return dtstart.Add(toNow);
    }

    void SetColor(GameObject go, int color)
    {
        switch (color)
        {
            case 0:
                go.GetComponent<UISprite>().spriteName = "Grade1";
                break;
            case 1:
                go.GetComponent<UISprite>().spriteName = "Grade2";
                break;
            case 2:
                go.GetComponent<UISprite>().spriteName = "Grade3";
                break;
            case 3:
                go.GetComponent<UISprite>().spriteName = "Grade4";
                break;
            case 4:
                go.GetComponent<UISprite>().spriteName = "Grade5";
                break;
        }
    }

}
