using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionBasicInfoPart : MonoBehaviour
{
    public UILabel legionNameLabel;
    public UILabel chairmanNameLabel;
    public UILabel memberCountLabel;
    public UILabel legionLevelLabel;
    public UILabel legionIDLabel;
    public UILabel legionRankLabel;
    public UISprite legionFlagIcon;
    public GameObject exitLegionButton;
    public GameObject dissolveLegionButton;

    public GameObject doubltButton;
    public GameObject DoubltObj;
    public GameObject DoubltObjCloseButton;

    public GameObject changeNameButton;
    public GameObject changeNameObj;
    public GameObject changeNameCloseButton;
    public GameObject changeNameConfirmButton;

    public GameObject changeFlagButton;

    public GameObject mailButton;
    public GameObject mailObj;
    public GameObject mailCloseButton;
    public GameObject mailSendButton;
    public UIInput input;

    public GameObject ChairManShowObj;
    public GameObject uiGride;
    public GameObject LegionMemberItem;

    private LegionItemData legionItemData = null;
    void OnEnable()
    {
        NetworkHandler.instance.SendProcess(string.Format("8004#{0};", CharacterRecorder.instance.legionID));
        NetworkHandler.instance.SendProcess(string.Format("8005#{0};", CharacterRecorder.instance.legionID));
    }
    // Use this for initialization
    void Start()
    {
        DoubltObj.SetActive(false);

        if (UIEventListener.Get(changeFlagButton).onClick == null)
        {
            UIEventListener.Get(changeFlagButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPanel("LegionFlagSettingWindow", false);
            };
        }
        if (UIEventListener.Get(exitLegionButton).onClick == null)
        {
            UIEventListener.Get(exitLegionButton).onClick += delegate(GameObject go)
            {
                //NetworkHandler.instance.SendProcess("8009#;");
                //UIManager.instance.OpenPromptWindow("确定退出军团？", PromptWindow.PromptType.Confirm, ConfirmExitLegion, null);
                UIManager.instance.OpenPromptWindowNoTitle("确定退出军团？\n退出24小时后才可再次加入\n(开服第一天只需1小时)", PromptWindow.PromptType.Confirm, ConfirmExitLegion, null);
            };
        }
        if (UIEventListener.Get(dissolveLegionButton).onClick == null)
        {
            UIEventListener.Get(dissolveLegionButton).onClick += delegate(GameObject go)
            {
                //UIManager.instance.OpenPromptWindow("确定解散军团？", PromptWindow.PromptType.Confirm, ConfirmDissolveLegion, null);
                UIManager.instance.OpenPromptWindowNoTitle("确定解散军团？", PromptWindow.PromptType.Confirm, ConfirmDissolveLegion, null);
            };
        }
        if (UIEventListener.Get(doubltButton).onClick == null)
        {
            UIEventListener.Get(doubltButton).onClick += delegate(GameObject go)
            {
                DoubltObj.SetActive(true);
            };
        }
        if (UIEventListener.Get(DoubltObjCloseButton).onClick == null)
        {
            UIEventListener.Get(DoubltObjCloseButton).onClick += delegate(GameObject go)
            {
                DoubltObj.SetActive(false);
            };
        }

        if (UIEventListener.Get(changeNameButton).onClick == null)
        {
            UIEventListener.Get(changeNameButton).onClick += delegate(GameObject go)
            {
                changeNameObj.SetActive(true);
            };
        }
        if (UIEventListener.Get(changeNameCloseButton).onClick == null)
        {
            UIEventListener.Get(changeNameCloseButton).onClick += delegate(GameObject go)
            {
                changeNameObj.SetActive(false);
            };
        }
        if (UIEventListener.Get(changeNameConfirmButton).onClick == null)
        {
            UIEventListener.Get(changeNameConfirmButton).onClick += delegate(GameObject go)
            {
                //NetworkHandler.instance.SendProcess("8009#;");
                changeNameObj.SetActive(false);
            };
        }

        if (UIEventListener.Get(mailButton).onClick == null)
        {
            UIEventListener.Get(mailButton).onClick += delegate(GameObject go)
            {
                mailObj.SetActive(true);
                //UIManager.instance.OpenPromptWindow("即将开放", PromptWindow.PromptType.Alert, null, null);
            };
        }
        if (UIEventListener.Get(mailCloseButton).onClick == null)
        {
            UIEventListener.Get(mailCloseButton).onClick += delegate(GameObject go)
            {
                mailObj.SetActive(false);
            };
        }
        if (UIEventListener.Get(mailSendButton).onClick == null)
        {
            UIEventListener.Get(mailSendButton).onClick += delegate(GameObject go)
            {
                string text = input.value;
                if (!string.IsNullOrEmpty(text))
                {
                    NetworkHandler.instance.SendProcess(string.Format("9006#{0};{1};", CharacterRecorder.instance.legionID, text));
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("没有邮件内容", PromptWindow.PromptType.Hint, null, null);
                }
                mailObj.SetActive(false);
                input.value = null;
            };
        }


        changeNameButton.SetActive(false); //kino temp
        // dissolveLegionButton.SetActive(false); //kino temp
    }

    public void SetLegionRankListPart(List<LegionItemData> _mlList)
    {
        if (_mlList.Count <= 0)
        {
            legionRankLabel.text = "1";
            return;
        }
        for (int i = 0; i < _mlList.Count; i++)
        {
            if (legionItemData != null)
            {
                if (legionItemData.legionId == _mlList[i].legionId)
                {
                    legionRankLabel.text = (i + 1).ToString();
                    break;
                }

            }
            else
            {
                legionRankLabel.text = "1";
            }
        }
    }

    public void SetLegionBasicInfoPart(LegionItemData _MyLegionItemData)//, LegionMemberData _MyLegionMemberData, List<LegionMemberData> _LegionMemberDataList)
    {
        legionItemData = _MyLegionItemData;
        legionNameLabel.text = _MyLegionItemData.legionName;
        memberCountLabel.text = _MyLegionItemData.MemberNumber.ToString();
        legionLevelLabel.text = string.Format("Lv.{0}", _MyLegionItemData.legionLevel);
        legionIDLabel.text = _MyLegionItemData.legionId.ToString();
        //legionRankLabel.text = _MyLegionItemData.legionId.ToString();
        legionFlagIcon.spriteName = string.Format("legionFlag{0}", CharacterRecorder.instance.legionFlag);

        chairmanNameLabel.text = _MyLegionItemData.legionChairmanName;

        if (CharacterRecorder.instance.isLegionChairman)
        {
            ChairManShowObj.SetActive(true);
            exitLegionButton.SetActive(false);
            if (CharacterRecorder.instance.myLegionPosition == 2)
            {
                //我的军团职位  （副军团长）
                //ChairManShowObj.SetActive(false);
                dissolveLegionButton.SetActive(false);
                exitLegionButton.SetActive(true);
            }
        }
        else
        {
            ChairManShowObj.SetActive(false);
            exitLegionButton.SetActive(true);
        }
    }
    public void SetLegionBasicInfoPart(List<LegionMemberData> _LegionMemberDataList)//, LegionMemberData _MyLegionMemberData, 
    {
        DestroyGride();
        LegionMemberDataListPaiXu(_LegionMemberDataList);
        for (int i = 0; i < _LegionMemberDataList.Count; i++)
        {
            GameObject go = NGUITools.AddChild(uiGride, LegionMemberItem);
            go.GetComponent<LegionMemberItem>().SetLegionMemberItem(_LegionMemberDataList[i]);
        }
        uiGride.GetComponent<UIGrid>().Reposition();
    }
    /// <summary>
    /// 军团成员排序
    /// </summary>
    /// <param name="_LegionMemberDataList"></param>
    public void LegionMemberDataListPaiXu(List<LegionMemberData> _LegionMemberDataList)
    {
        for (int i = 0; i < _LegionMemberDataList.Count; i++)
        {
            for (int j = _LegionMemberDataList.Count - 1; j > i; j--)
            {
                if (_LegionMemberDataList[j].officialPosition > _LegionMemberDataList[j - 1].officialPosition)
                {
                    LegionMemberData temp = _LegionMemberDataList[j];
                    _LegionMemberDataList[j] = _LegionMemberDataList[j - 1];
                    _LegionMemberDataList[j - 1] = temp;
                }
            }
        }
    }
    public void ResetLegionBasicInfoPart(int _uid, string position)
    {
        if (!CharacterRecorder.instance.isLegionChairman)
        {
            ChairManShowObj.SetActive(false);
            exitLegionButton.SetActive(true);
        }
        for (int i = 0; i < uiGride.transform.childCount; i++)
        {
            if (position == "3" && uiGride.transform.GetChild(i).GetComponent<LegionMemberItem>().OneLegionMemberData.officialPosition == 3)
            {
                position = "0";
                uiGride.transform.GetChild(i).GetComponent<LegionMemberItem>().ResetLegionMemberItem(position);
            }
            uiGride.transform.GetChild(i).GetComponent<LegionMemberItem>().ResetLegionMemberItem(_uid, position);
        }
    }
    void DestroyGride()
    {
        for (int i = uiGride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGride.transform.GetChild(i).gameObject);
        }
    }
    void ConfirmExitLegion()
    {
        NetworkHandler.instance.SendProcess("8009#0;");
    }
    void ConfirmDissolveLegion()
    {
        NetworkHandler.instance.SendProcess(string.Format("8002#{0};", CharacterRecorder.instance.legionID));
    }


}
