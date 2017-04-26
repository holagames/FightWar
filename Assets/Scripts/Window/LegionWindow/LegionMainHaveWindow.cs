using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionMainHaveWindow : MonoBehaviour
{
    public List<UILabel> AnouncedLabelList = new List<UILabel>();
    public UILabel AnouncedLabelInfo;
    public UILabel legionNameLabel;
    public UILabel chairmanNameLabel;
    public UILabel memberCountLabel;
    public UILabel legionLevelLabel;
    public UILabel legionIDLabel;
    public UILabel legionRankLabel;
    public GameObject AnouncedButton;
    public GameObject ChangeAnounceButton;
    public GameObject legionFlagButton;
    public GameObject AnouncedObj;
    public GameObject ChangeAnounceObj;
    public GameObject AnouncedMask;
    public GameObject ChangeAnounceMask;
    public GameObject AnouncedObjCloseButton;
    public GameObject ChangeAnouncObjCloseButton;
    public GameObject ChangeAnouncSureButton;
    public UIInput input;
    public List<GameObject> LegionHomeObjList = new List<GameObject>();
    public GameObject QipaoAnounced;

    private LegionItemData _MyLegionItemData;
    // Use this for initialization
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.军团);
        UIManager.instance.UpdateSystems(UIManager.Systems.军团);
        if (CharacterRecorder.instance.legionID == 0)
        {
            UIManager.instance.OpenPromptWindow("您已经被踢出该军团.", PromptWindow.PromptType.Confirm, () =>
            {
                UIManager.instance.OpenPanel("MainWindow", true);
            }, () =>
            {
                UIManager.instance.OpenPanel("MainWindow", true);
            });
        }
        //else if (CharacterRecorder.instance.myLegionData.legionLevel >= 3) 
        //{
        //    NetworkHandler.instance.SendProcess("8701#;");
        //    NetworkHandler.instance.SendProcess("8703#;");
        //    NetworkHandler.instance.SendProcess("8705#;");
        //    NetworkHandler.instance.SendProcess("8707#;");
        //    NetworkHandler.instance.SendProcess("8708#;");
        //}

        NetworkHandler.instance.SendProcess(string.Format("8004#{0};", CharacterRecorder.instance.legionID));
        NetworkHandler.instance.SendProcess("8401#;");
        NetworkHandler.instance.SendProcess("8006#;");
        NetworkHandler.instance.SendProcess(string.Format("8018#{0};", ""));
        NetworkHandler.instance.SendProcess("8501#;");
        NetworkHandler.instance.SendProcess("8010#0;");

        AnouncedObj.SetActive(false);
        if (UIEventListener.Get(AnouncedButton).onClick == null)
        {
            UIEventListener.Get(AnouncedButton).onClick += delegate(GameObject go)
            {
                //NetworkHandler.instance.SendProcess("8022#;");
                //AnouncedObj.SetActive(true);
                UIManager.instance.OpenPanel("LegionRankListWindow", true);
            };
        }
        if (UIEventListener.Get(ChangeAnounceButton).onClick == null)
        {
            UIEventListener.Get(ChangeAnounceButton).onClick += delegate(GameObject go)
            {
                ChangeAnounceObj.SetActive(true);
            };
        }
        if (UIEventListener.Get(legionFlagButton).onClick == null)
        {
            UIEventListener.Get(legionFlagButton).onClick += delegate(GameObject go)
            {
                //Debug.Log("军团旗帜。。。");
                //UIManager.instance.OpenPromptWindow("暂未开启", PromptWindow.PromptType.Hint, null, null);
                PlayerPrefs.SetInt(string.Format("LegionAnounced{0}", CharacterRecorder.instance.userId), 1);
                NetworkHandler.instance.SendProcess("8022#;");
                AnouncedObj.SetActive(true);
                legionFlagButton.transform.FindChild("RedPoint").gameObject.SetActive(false);
            };
        }
        if (UIEventListener.Get(AnouncedObjCloseButton).onClick == null)
        {
            UIEventListener.Get(AnouncedObjCloseButton).onClick += delegate(GameObject go)
            {
                AnouncedObj.SetActive(false);
            };
        }
        if (UIEventListener.Get(ChangeAnouncObjCloseButton).onClick == null)
        {
            UIEventListener.Get(ChangeAnouncObjCloseButton).onClick += delegate(GameObject go)
            {
                ChangeAnounceObj.SetActive(false);
            };
        }
        if (UIEventListener.Get(AnouncedMask).onClick == null)
        {
            UIEventListener.Get(AnouncedMask).onClick += delegate(GameObject go)
            {
                AnouncedObj.SetActive(false);
            };
        }
        if (UIEventListener.Get(ChangeAnounceMask).onClick == null)
        {
            UIEventListener.Get(ChangeAnounceMask).onClick += delegate(GameObject go)
            {
                ChangeAnounceObj.SetActive(false);
            };
        }
        if (UIEventListener.Get(ChangeAnouncSureButton).onClick == null)
        {
            UIEventListener.Get(ChangeAnouncSureButton).onClick += delegate(GameObject go)
            {
                //Debug.Log("Click SureButton");
                string text = input.value;
                if (!string.IsNullOrEmpty(text))
                {
                    NetworkHandler.instance.SendProcess(string.Format("8023# {0};", text));
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("没有修改公告", PromptWindow.PromptType.Hint, null, null);
                }
                //ChangeAnounceObj.SetActive(false);
            };
        }
        for (int i = 0; i < LegionHomeObjList.Count; i++)
        {
            UIEventListener.Get(LegionHomeObjList[i]).onClick = ClickLegionHomeObj;
        }

        //AnouncedButton.SetActive(false); //kino temp

        SetRedPoint();
        NetworkHandler.instance.SendProcess("8201#;");
        OpenQipaoAnouncedWindow();
    }

    public void SetRedMain()
    {
        int count = 0;
        for (int i = 0; i < LegionHomeObjList.Count; i++)
        {
            if (LegionHomeObjList[i].transform.FindChild("RedPoint") != null)
            {
                if (LegionHomeObjList[i].transform.FindChild("RedPoint").gameObject.activeSelf)
                {
                    count++;
                    break;
                }
            }
        }
        if (count == 0)
        {
            CharacterRecorder.instance.SetRedPoint(20, false);
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(20, true);
        }
    }

    public void SetLegionRank(List<LegionItemData> _mlList)
    {
        if (_mlList.Count > 0)
        {
            for (int i = 0; i < _mlList.Count; i++)
            {
                if (CharacterRecorder.instance.legionID == _mlList[i].legionId)
                {
                    legionRankLabel.text = string.Format("第{0}名", i + 1);
                    break;
                }
                else
                {
                    legionRankLabel.text = string.Format("第{0}名", 1);
                }
            }
        }
        else
        {
            legionRankLabel.text = string.Format("第{0}名", 1);
        }

    }
    public void SetLegionBasicInfoPart(LegionItemData _MyLegionItemData)//, LegionMemberData _MyLegionMemberData)//, List<LegionMemberData> _LegionMemberDataList
    {
        this._MyLegionItemData = _MyLegionItemData;
        legionNameLabel.text = _MyLegionItemData.legionName;

        if (TextTranslator.instance.GetLegionByID(_MyLegionItemData.legionLevel).LimitNum <= _MyLegionItemData.MemberNumber)
        {
            memberCountLabel.text = string.Format("[-][FF0000]{0}/{1}[-]", _MyLegionItemData.MemberNumber, TextTranslator.instance.GetLegionByID(_MyLegionItemData.legionLevel).LimitNum);
        }
        else
        {
            memberCountLabel.text = string.Format("[-][33d56a]{0}[-]/{1}", _MyLegionItemData.MemberNumber, TextTranslator.instance.GetLegionByID(_MyLegionItemData.legionLevel).LimitNum);//_MyLegionItemData.MemberNumber.ToString();
        }
        legionLevelLabel.text = string.Format("Lv.{0}", _MyLegionItemData.legionLevel);
        legionIDLabel.text = _MyLegionItemData.legionId.ToString();
        //legionRankLabel.text = string.Format("第{0}名", _MyLegionItemData.legionId);//_MyLegionItemData.legionId.ToString();

        legionFlagButton.GetComponent<UISprite>().spriteName = string.Format("legionFlag{0}", CharacterRecorder.instance.legionFlag);
        if (PlayerPrefs.GetInt(string.Format("LegionAnounced{0}", CharacterRecorder.instance.userId)) == 1)
        {
            legionFlagButton.transform.FindChild("RedPoint").gameObject.SetActive(false);
        }
        chairmanNameLabel.text = _MyLegionItemData.legionChairmanName;
        if (_MyLegionItemData.legionChairmanName == CharacterRecorder.instance.characterName || CharacterRecorder.instance.myLegionPosition >= 2)
        {
            CharacterRecorder.instance.isLegionChairman = true;
            ChangeAnounceButton.SetActive(true);
        }
        else
        {
            ChangeAnounceButton.SetActive(false);
        }
        //AnouncedLabelList[0].text = string.Format("欢迎来到 {0}。", _MyLegionItemData.legionName);
        //AnouncedLabelList[1].text = string.Format("欢迎来到 {0}，军团管理可以随时修改公告。", _MyLegionItemData.legionName);
        AnouncedLabelInfo.text = string.Format(string.Format("欢迎来到 {0}，军团管理可以随时修改公告。", _MyLegionItemData.legionName));//"欢迎来到 {0}。", _MyLegionItemData.legionName) + 
    }

    /// <summary>1
    /// 设置捐献红点
    /// </summary>
    /// <param name="leftTimes"></param>
    public void SetContributeRedPoint(int leftTimes)
    {
        if (leftTimes > 0)
        {
            CharacterRecorder.instance.SetRedPoint(29, true);
            LegionHomeObjList[4].transform.FindChild("RedPoint").gameObject.SetActive(CharacterRecorder.instance.GetRedPoint(29));
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(29, false);
            LegionHomeObjList[4].transform.FindChild("RedPoint").gameObject.SetActive(CharacterRecorder.instance.GetRedPoint(29));
        }
    }

    /// <summary>1
    /// 设置酒吧红点
    /// </summary>
    /// <param name="leftTimes"></param>
    public void SetDiceRedPoint(int leftTimes)
    {
        if (leftTimes > 0)
        {
            CharacterRecorder.instance.SetRedPoint(32, true);
            LegionHomeObjList[6].transform.FindChild("RedPoint").gameObject.SetActive(CharacterRecorder.instance.GetRedPoint(32));
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(32, false);
            LegionHomeObjList[6].transform.FindChild("RedPoint").gameObject.SetActive(CharacterRecorder.instance.GetRedPoint(32));
        }
    }
    /// <summary>1
    /// 设置训练红点
    /// </summary>
    /// <param name="leftTimes"></param>
    public void SetTrainRedPoint()
    {
        int leftTimes = 0;

        for (var i = 0; i < TextTranslator.instance.LegionTrainList.Count; i++)
        {
            LegionTrain train = TextTranslator.instance.LegionTrainList[i];
            //Debug.LogError(TextTranslator.instance.LegionTrainList[i].characteroleId + "   " + TextTranslator.instance.LegionTrainList[i].state);
            if (train.mHero != null)
            {
                if (train.mHero.exp == train.mHero.maxExp - 1)
                {
                    leftTimes++;
                    break;
                }
            }
            else if (train.state == 1)
            {
                leftTimes++;
                break;
            }
            else if (train.DiamondCost <= CharacterRecorder.instance.lunaGem)
            {
                if (train.VipUnLock == 99 && train.NeedLevel <= CharacterRecorder.instance.level)
                {
                    leftTimes++;
                    break;
                }
                else if (train.VipUnLock <= CharacterRecorder.instance.Vip)
                {
                    leftTimes++;
                    break;
                }
            }
            //if (TextTranslator.instance.LegionTrainList[i].state == 1)
            //{
            //    leftTimes++;
            //    break;
            //}
        }
        if (leftTimes > 0)
        {
            CharacterRecorder.instance.SetRedPoint(28, true);
            LegionHomeObjList[3].transform.FindChild("RedPoint").gameObject.SetActive(CharacterRecorder.instance.GetRedPoint(28));
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(28, false);
            LegionHomeObjList[3].transform.FindChild("RedPoint").gameObject.SetActive(CharacterRecorder.instance.GetRedPoint(28));
        }
    }

    /// <summary>
    /// 设置任务红点
    /// </summary>
    /// <param name="leftTimes"></param>
    public void SetTaskRedPoint(int leftTimes)
    {
        if (leftTimes > 0)
        {
            CharacterRecorder.instance.SetRedPoint(30, true);
            LegionHomeObjList[5].transform.FindChild("RedPoint").gameObject.SetActive(CharacterRecorder.instance.GetRedPoint(30));
        }
        else
        {
            CharacterRecorder.instance.SetRedPoint(30, false);
            LegionHomeObjList[5].transform.FindChild("RedPoint").gameObject.SetActive(CharacterRecorder.instance.GetRedPoint(30));
        }
    }
    void SetRedPoint()
    {
        /*  bool _boolState0 = CharacterRecorder.instance.needChairmanDealCount > 0 ?true : false;
          if (_boolState0)
          {
              LegionHomeObjList[0].transform.FindChild("RedPoint").gameObject.SetActive(_boolState0);
          }

          bool _boolState1 = CharacterRecorder.instance.legionFightLeftTimes > 0 ? true : false;
          if (_boolState1)
          {
              LegionHomeObjList[1].transform.FindChild("RedPoint").gameObject.SetActive(_boolState1);
          }*/
        List<bool> boolStateList = new List<bool>();
        bool _boolState0 = CharacterRecorder.instance.needChairmanDealCount > 0 ? true : false;
        bool _boolState1 = CharacterRecorder.instance.legionFightLeftTimes > 0 ? true : false;
        boolStateList.Add(_boolState0);
        boolStateList.Add(_boolState1);
        for (int i = 0; i < LegionHomeObjList.Count; i++)
        {
            if (i < boolStateList.Count)
            {
                if (i == 0)
                {
                    CharacterRecorder.instance.SetRedPoint(27, boolStateList[i]);
                    LegionHomeObjList[i].transform.FindChild("RedPoint").gameObject.SetActive(CharacterRecorder.instance.GetRedPoint(27));
                }
                else if (i == 1)
                {
                    CharacterRecorder.instance.SetRedPoint(26, boolStateList[i]);
                    LegionHomeObjList[i].transform.FindChild("RedPoint").gameObject.SetActive(CharacterRecorder.instance.GetRedPoint(26));
                }
            }
        }
        LegionPacketPoint();
    }
    /// <summary>
    /// 设置副本是否有红点
    /// </summary>
    /// <param name="state">true表示有红点，false表示没有</param>
    public void SetRedPoint(bool state)
    {
        if (GameObject.Find("LegionCopyWindowNew") != null)
        {
            GameObject.Find("LegionCopyWindowNew").GetComponent<LegionCopyWindowNew>().SetLegionCopyRedPoint();
        }
        if (!LegionHomeObjList[1].transform.FindChild("RedPoint").gameObject.activeSelf)
        {
            CharacterRecorder.instance.SetRedPoint(26, state);
            LegionHomeObjList[1].transform.FindChild("RedPoint").gameObject.SetActive(CharacterRecorder.instance.GetRedPoint(26));
        }
        SetRedMain();
    }
    /// <summary>
    /// 获取大关并取得当前节点是否已通关
    /// </summary>
    /// <param name="GateBox"></param>
    /// <param name="percentList"></param>
    public void GetAllNodeState(int GateBox, BetterList<int> percentList)
    {
        for (int i = 0; i < percentList.size; i++)
        {
            if (percentList[i] == 100)//表示通过
            {
                NetworkHandler.instance.SendProcess(string.Format("8303#{0};{1};", GateBox, i + 1));
            }
        }
    }
    public void GetBigState(int GateBox)
    {
        NetworkHandler.instance.SendProcess(string.Format("8202#{0};", GateBox));
    }
    void ConfirmExitLegion()
    {
        NetworkHandler.instance.SendProcess("8009#;");
    }
    void ClickLegionHomeObj(GameObject go)
    {
        switch (go.name)
        {
            case "LegionHomeItem1": UIManager.instance.OpenPanel("LegionSecondWindow", true);
                GameObject.Find("LegionSecondWindow").GetComponent<LegionSecondWindow>().partsList[0].GetComponent<LegionBasicInfoPart>().SetLegionBasicInfoPart(this._MyLegionItemData);
                break;
            case "LegionHomeItem2":
                //UIManager.instance.OpenPanel("LegionCopyWindow", true);//旧的 副本两个界面
                UIManager.instance.OpenPanel("LegionCopyWindowNew", true);//新的 副本一个界面
                //UIManager.instance.OpenPromptWindow("暂未开放", PromptWindow.PromptType.Confirm, null, null);
                break;
            case "LegionHomeItem3":
                //UIManager.instance.OpenPromptWindow("暂未开启", PromptWindow.PromptType.Hint, null, null); 
                CharacterRecorder.instance.RankShopType = 10005;
                UIManager.instance.OpenPanel("RankShopWindow", true);
                break;
            case "LegionHomeItem4":
                //UIManager.instance.OpenPromptWindow("暂未开启", PromptWindow.PromptType.Hint, null, null); 
                UIManager.instance.OpenPanel("LegionTrainingGroundWindow", true);
                break;
            case "LegionHomeItem5":
                //UIManager.instance.OpenPromptWindow("暂未开启", PromptWindow.PromptType.Hint, null, null); 
                UIManager.instance.OpenPanel("LegionContributeWindow", true);
                break;
            case "LegionHomeItem6":
                //UIManager.instance.OpenPromptWindow("暂未开放", PromptWindow.PromptType.Confirm, null, null); 
                UIManager.instance.OpenPanel("LegionTaskWindow", true);
                break;
            case "LegionHomeItem7":
                UIManager.instance.OpenPanel("LegionDiceWindow", true);
                break;
            case "LegionHomeItem8":
                UIManager.instance.CountSystem(UIManager.Systems.战场);
                UIManager.instance.UpdateSystems(UIManager.Systems.战场);
                UIManager.instance.OpenPanel("LegionWarWindow", true);
                break;
            case "LegionHomeItem9":
                if (CharacterRecorder.instance.myLegionData.legionLevel >= 3)
                {
                    UIManager.instance.OpenPanel("LegionRedPacketWindow", true);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("军团等级不足，需3级军团开启红包！", PromptWindow.PromptType.Hint, null, null);
                }
                break;
        }
    }

    #region 获取公告
    public void SetAnouncedInfo(string _Anouncement)
    {
        if (_Anouncement != "")
        {
            AnouncedLabelInfo.text = _Anouncement;
            input.value = _Anouncement;
            ChangeQipaoAnouncedWindowInfo();
        }
    }
    #endregion

    #region 修改公告

    public void ResetAnouncedInfo(string _Anouncement)
    {
        ChangeAnounceObj.SetActive(false);
        SetAnouncedInfo(_Anouncement);
    }
    #endregion

    /// <summary>
    /// 军团红包红点
    /// </summary>
    private void LegionPacketPoint() 
    {
        if (CharacterRecorder.instance.legionID != 0)
        {
            if (CharacterRecorder.instance.myLegionData.legionLevel >= 3)
            {
                if (CharacterRecorder.instance.isOpenRechargeRed && CharacterRecorder.instance.isRechargeRedPoint || CharacterRecorder.instance.isLegionRedPoint || CharacterRecorder.instance.isRichRedneckPoint || CharacterRecorder.instance.isGrabRedPoint)
                {
                    LegionHomeObjList[8].transform.Find("RedPoint").gameObject.SetActive(true);
                }
                else 
                {
                    LegionHomeObjList[8].transform.Find("RedPoint").gameObject.SetActive(false);
                }
            }
            else 
            {
                LegionHomeObjList[8].transform.Find("RedPoint").gameObject.SetActive(false);
            }
        }
        else 
        {
            LegionHomeObjList[8].transform.Find("RedPoint").gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// 军团气泡公告
    /// </summary>
    private void OpenQipaoAnouncedWindow() 
    {
        CancelInvoke("InvokeOpenQipaoAnouncedWindow");
        Invoke("InvokeOpenQipaoAnouncedWindow", 0.5f);
    }

    private void InvokeOpenQipaoAnouncedWindow() 
    {
        //if(CharacterRecorder.instance)
        if (CharacterRecorder.instance.LegionAnouncement == "") 
        {
            CharacterRecorder.instance.LegionAnouncement = "这个军团长很懒，什么都没交待";
        }
        if (CharacterRecorder.instance.LegionAnouncement != "")
        {
            QipaoAnounced.SetActive(true);
            QipaoAnounced.transform.Find("ScrollView/Label").GetComponent<UILabel>().text = CharacterRecorder.instance.LegionAnouncement;
            QipaoAnounced.transform.Find("ScrollView").localPosition = new Vector3(0, 0, 0);
            QipaoAnounced.transform.Find("ScrollView").GetComponent<UIPanel>().clipOffset = new Vector2(0, 0);
            QipaoAnounced.transform.Find("ScrollView/Label").localPosition = new Vector3(0, 43f, 0);
        }
        else 
        {
            QipaoAnounced.SetActive(false);
        }
    }

    /// <summary>
    /// 修改公告时改变气泡文字
    /// </summary>
    private void ChangeQipaoAnouncedWindowInfo() 
    {
        QipaoAnounced.transform.Find("ScrollView/Label").GetComponent<UILabel>().text = CharacterRecorder.instance.LegionAnouncement;
        QipaoAnounced.transform.Find("ScrollView").localPosition = new Vector3(0, 0, 0);
        QipaoAnounced.transform.Find("ScrollView").GetComponent<UIPanel>().clipOffset = new Vector2(0, 0);
        QipaoAnounced.transform.Find("ScrollView/Label").localPosition = new Vector3(0, 43f, 0);
    }
}
