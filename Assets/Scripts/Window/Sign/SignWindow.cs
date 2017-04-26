using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SignWindow: MonoBehaviour 
{
    //--------Title
    [SerializeField]
    private GameObject CloseButton;
    [SerializeField]
    private GameObject GetButton;
    [SerializeField]
    private UILabel titleLabel;
    //-------TotleSignPart
    [SerializeField]
    private GameObject TotleSignItem;
    [SerializeField]
    private GameObject TotleSignGride;
    [SerializeField]
    private UILabel TotleSignCountLabel;
    //-------NormalSignPart
    [SerializeField]
    private GameObject signItem;
    [SerializeField]
    private GameObject uiGride;
    [SerializeField]
    private UILabel signCountLabel;
    //--------数据
    private int signTotleCount;
    private int signExtraTotleCount;
    private List<GameObject> signItemList = new List<GameObject>();
    private List<GameObject> totleSignItemList = new List<GameObject>();
    private int SignExtraIDCanGet = 0;
    private Sign todaySign;//今天的数据
    public static bool CanGetSignExtraAward = false;
	// Use this for initialization
	void Start () 
    {
        UIManager.instance.CountActivitys(UIManager.Activitys.签到);
        UIManager.instance.UpdateActivitys(UIManager.Activitys.签到);

        NetworkHandler.instance.SendProcess("9101#;");
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }
        if (UIEventListener.Get(GetButton).onClick == null)
        {
            UIEventListener.Get(GetButton).onClick += delegate(GameObject go)
            {
                //Debug.LogError("Click GetButton..." + TextTranslator.instance.SignExtraIDHadGet + "..." + SignExtraIDCanGet);
                if (TextTranslator.instance.SignExtraIDHadGet <= SignExtraIDCanGet)
                {
                    NetworkHandler.instance.SendProcess("9103#;");
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("累计签到奖励已领完", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    public void SetSignData(int _signId, int _state, int _signExtraID1, int _signExtraID2)
    {
        //正常签到数据
        TextTranslator.instance.signPerMonthList = TextTranslator.instance.GetSignPerMonthListBySignID(_signId);
        signTotleCount = _signId - TextTranslator.instance.signPerMonthList[0].signID;
        signExtraTotleCount = _signId;
        for (int i = 0; i < TextTranslator.instance.signPerMonthList.size;i++ )
        {
            Sign _Sign = TextTranslator.instance.signPerMonthList[i];
            if (_Sign.signID == _signId)
            {
                switch (_state)
                {
                    case 0: _Sign.state = 1; break;
                    case 1: _Sign.state = 2;
                        signTotleCount += 1;
                        break;
                    default: Debug.LogError("参数错误"); break;
                }
                todaySign = _Sign;
            }
            else if (_Sign.signID < _signId)
            {
                _Sign.state = 2;
            }
            else
            {
                _Sign.state = 0;
            }
        }
        SetExtraSignData(_signId, _state, _signExtraID1, _signExtraID2);
        //正常签到界面
        SetSignWindow(TextTranslator.instance.signPerMonthList);
       
    }
    //累计签到数据
    public void SetExtraSignData(int _signId, int _state, int _signExtraID1, int _signExtraID2)
    {
        //旧的，第二天显示第二层累计奖励
      /*  SignExtraIDCanGet = _signExtraID2;
        //TextTranslator.instance.signExtraPerPageList = TextTranslator.instance.GetSignExtraPerPageListBySignExtraID(_signExtraID1, _signExtraID2);
        //累计签到界面
        SetSignExtraWindow(_signExtraID1, _signExtraID2,TextTranslator.instance.signExtraPerPageList); */
        //新的，领取后显示下一层奖励
        SignExtraIDCanGet = _signExtraID2;
        TextTranslator.instance.signExtraPerPageList = TextTranslator.instance.GetSignExtraPerPageListBySignExtraID(_signId, _state,_signExtraID1, _signExtraID2);
        //累计签到界面
        SetSignExtraWindow(_signExtraID1, _signExtraID2,TextTranslator.instance.signExtraPerPageList);
    }
    public void ResetSignData(List<Item> _itemList)
    {
        OpenGainWindow(_itemList);
        TextTranslator.instance.GetSignBySignID(SignItem.curClickSignId).state = 2;
        todaySign = TextTranslator.instance.GetSignBySignID(SignItem.curClickSignId);
        signTotleCount += 1;
        SetSignWindowInfo();
        for (int i = 0; i < signItemList.Count;i++ )
        {
            signItemList[i].GetComponent<SignItem>().ResetSignItem();
        }
        if (signExtraTotleCount >= TextTranslator.instance.SignExtraRealNumForRight)
        {
            UILabel _GetButtonLabel = GetButton.transform.FindChild("Label").GetComponent<UILabel>();
            GetButton.GetComponent<UIButton>().isEnabled = true;
            _GetButtonLabel.effectStyle = UILabel.Effect.Outline;
            CanGetSignExtraAward = true;
        }
        for (int i = 0; i < totleSignItemList.Count;i++ )
        {
            totleSignItemList[i].GetComponent<SignExtraItem>().ResetSignExtraItemEffectState();
        }
    }
    void OpenGainWindow(List<Item> _itemList)
    {
        UIManager.instance.OpenSinglePanel("AdvanceWindow", false);
        GameObject.Find("AdvanceWindow").layer = 11;
        AdvanceWindow aw = GameObject.Find("AdvanceWindow").GetComponent<AdvanceWindow>();
        aw.SetInfo(AdvanceWindowType.GainResult, null, null, null, null, _itemList);
    }
    public void SetSignWindow(BetterList<Sign> _signItemList)
    {
        titleLabel.text = string.Format("签到");
  //      SetSignWindowInfo();
      //  Debug.LogError(_signItemList.size);
        for (int i = 0; i < _signItemList.size;i++ )
        {
            GameObject obj = NGUITools.AddChild(uiGride, signItem) as GameObject;
            obj.GetComponent<SignItem>().SetSignItem(_signItemList[i]);
            signItemList.Add(obj);
        }
        uiGride.GetComponent<UIGrid>().Reposition();
    }
    void SetSignWindowInfo()
    {
        if (signTotleCount < 0)
        {
            signCountLabel.text = "30";
        }
        else
        {
            signCountLabel.text = signTotleCount.ToString();
        }
        //TotleSignCountLabel.text = string.Format("{0}/30", signTotleCount);
       // Debug.LogError(TextTranslator.instance.SignExtraNumForRight);
   //     TotleSignCountLabel.text = string.Format("{0}/{1}", signTotleCount, TextTranslator.instance.SignExtraNumForRight);
        TotleSignCountLabel.text = string.Format("{0}/{1}", signExtraTotleCount, TextTranslator.instance.SignExtraRealNumForRight);
    }
    void ClearUIGride()
    {
        //Debug.LogError(totleSignItemList.Count);
        for (int i = totleSignItemList.Count - 1; i >= 0;i-- )
        {
            DestroyImmediate(totleSignItemList[i]);
        }
        totleSignItemList.Clear();
    }
    public void SetSignExtraWindow(int _signExtraID1, int _signExtraID2, BetterList<SignExtra> _signExtraItemList)
    {
        SetSignWindowInfo();
        ClearUIGride();
        for (int i = 0; i < _signExtraItemList.size; i++)
        {
            if(i == 0)
            {
                for (int j = 0; j < _signExtraItemList[i].SignExtraItemList.size;j++ )
                {
                    GameObject obj = NGUITools.AddChild(TotleSignGride, TotleSignItem) as GameObject;
                    obj.GetComponent<SignExtraItem>().SetSignExtraItem(_signExtraItemList[i].SignExtraItemList[j]);
                    totleSignItemList.Add(obj);
                }
            }
        }
        TotleSignGride.GetComponent<UIGrid>().Reposition();
        UILabel _GetButtonLabel = GetButton.transform.FindChild("Label").GetComponent<UILabel>();
     //   if (_signExtraID1 >= _signExtraID2 || signTotleCount < _signExtraID2)//或没签到 signTotleCount < 1
        if (_signExtraID1 >= _signExtraID2 || signExtraTotleCount < TextTranslator.instance.SignExtraRealNumForRight)
        {
            GetButton.GetComponent<UIButton>().isEnabled = false;
            _GetButtonLabel.effectStyle = UILabel.Effect.None;
            CanGetSignExtraAward = false;
            for (int i = 0; i < totleSignItemList.Count; i++)
            {
                totleSignItemList[i].GetComponent<SignExtraItem>().ResetSignExtraItemEffectState();
            }
        }
        else
        {
            GetButton.GetComponent<UIButton>().isEnabled = true;
            _GetButtonLabel.effectStyle = UILabel.Effect.Outline;
            CanGetSignExtraAward = true;
            for (int i = 0; i < totleSignItemList.Count; i++)
            {
                totleSignItemList[i].GetComponent<SignExtraItem>().ResetSignExtraItemEffectState();
            }
        }
    }
    public void ResetSignExtraData(List<Item> _itemList)
    {
        OpenGainWindow(_itemList);
        //Debug.LogError(TextTranslator.instance.SignExtraIDHadGet);
        SetExtraSignData(todaySign.signID, todaySign.state,TextTranslator.instance.SignExtraIDHadGet, SignExtraIDCanGet);
    }
}
