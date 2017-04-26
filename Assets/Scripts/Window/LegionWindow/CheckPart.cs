using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CheckPart: MonoBehaviour 
{
    public GameObject legionMemberItem;
    public UIGrid uiGride;
    public UILabel NoneMemberNeedCheckLabel;
    public UILabel LeftTimeLabel;
    public UILabel LimitDesLabel;
    public GameObject QuickRefuseButton;
    public GameObject CallButton;
    public GameObject SettingLimitButton;
    public GameObject SettingLimitBordObj;

    private List<LegionMemberData> _LegionItemList;
    public static int leftTime = 0;
    private List<string> stringList1 = new List<string>();
    private List<string> stringList2Two = new List<string>();//设置状态 默认 1_0
    void OnEnable()
    {
        NetworkHandler.instance.SendProcess("8012#;");
        NetworkHandler.instance.SendProcess("8016#;");
    }
    void OnDestroy()
    {
        
    }
	// Use this for initialization
	void Start () 
    {
        //leftTime = SceneTransformer.instance.
        InitStringList();
        if (leftTime == 0)
        {
            CallButton.SetActive(true);
        }
        else
        {
            
            CallButton.SetActive(false);
            LeftTimeLabel.transform.parent.gameObject.SetActive(true);
            CancelInvoke();
            InvokeRepeating("UpdateTime", 0, 1.0f);
        }
        
        if (UIEventListener.Get(QuickRefuseButton).onClick == null)
        {
            UIEventListener.Get(QuickRefuseButton).onClick += delegate(GameObject go)
            {
                //NetworkHandler.instance.SendProcess(string.Format("8013#{0};{1};", this.OneLegionMemberData.uId, 2));
                for (int i = 0; i < this._LegionItemList.Count;i++ )
                {
                    NetworkHandler.instance.SendProcess(string.Format("8013#{0};{1};{2};", this._LegionItemList[i].uId, 2, CharacterRecorder.instance.legionID));
                }
            };
        }
        if (UIEventListener.Get(CallButton).onClick == null)
        {
            UIEventListener.Get(CallButton).onClick += delegate(GameObject go)
            {
               // NetworkHandler.instance.SendProcess("7001#" + "3;军团长 " + CharacterRecorder.instance.characterName + "正在招募军团成员,大家快来参加!军团名字 " + CharacterRecorder.instance.legionName + "军团ID:" + CharacterRecorder.instance.legionID + ";" + CharacterRecorder.instance.Vip + "$" + CharacterRecorder.instance.headIcon + ";");
                NetworkHandler.instance.SendProcess("7001#" + "7;【" + CharacterRecorder.instance.legionName + "】邀请战队加入,一起称霸世界。" + "军团ID:" + CharacterRecorder.instance.legionID + ";" + CharacterRecorder.instance.Vip + "$" + CharacterRecorder.instance.headIcon + "$" + CharacterRecorder.instance.legionID + "$" + CharacterRecorder.instance.NationID + "$" + CharacterRecorder.instance.userId +"$" + CharacterRecorder.instance.legionCountryID+ ";");
                NetworkHandler.instance.SendProcess("7002#19;" + CharacterRecorder.instance.characterName + ";" + CharacterRecorder.instance.legionName + ";" + CharacterRecorder.instance.legionID+";");
                leftTime = 60 * 5;
                CallButton.SetActive(false);
                LeftTimeLabel.transform.parent.gameObject.SetActive(true);
                CancelInvoke();
                InvokeRepeating("UpdateTime", 0, 1.0f);
                StartCoroutine(CharacterRecorder.instance.AutoUpdateLegionCheckPartLeftTime());
            };
        }
        if (UIEventListener.Get(SettingLimitButton).onClick == null)
        {
            UIEventListener.Get(SettingLimitButton).onClick += delegate(GameObject go)
            {
                SettingLimitBordObj.SetActive(true);
            };
        }

        //SettingLimitButton.SetActive(false); //kino temp
	}
    public void SetCheckListPart(List<LegionMemberData> _LegionItemList)
    {
        ClearUIGride();
        this._LegionItemList = _LegionItemList;
        if (_LegionItemList.Count == 0)
        {
            NoneMemberNeedCheckLabel.text = "[249bd2]还没有任何人申请哦，可以通过 [3ee817]招募团员[-] 来邀请其他团员。";
            NoneMemberNeedCheckLabel.gameObject.SetActive(true);
        }
        else
        {
            NoneMemberNeedCheckLabel.gameObject.SetActive(false);
        }
        for (int i = 0; i < _LegionItemList.Count; i++)
        {
            GameObject obj = NGUITools.AddChild(uiGride.gameObject, legionMemberItem);
            obj.GetComponent<LegionMemberItem>().SetLegionMemberItem(true,_LegionItemList[i]);
        }
        uiGride.GetComponent<UIGrid>().Reposition();
    }
    public void SetSettingStates(int stateValue1, int stateValue2)
    {
        int index1; 
        int index2;
        switch (stateValue1)
        {
            case 1: index1 = 0; break;
            case 0: index1 = 1; break;
            case -1: index1 = 2; break;
            default: index1 = 0; break;
        }
        switch (stateValue2)
        {
            case 0: index2 = 0; break;
            default: index2 = stateValue2 - 23; break;
        }
        LimitDesLabel.text = string.Format("申请条件:{0}({1})",FindTargetStringToShow(stringList1, index1),FindTargetStringToShow(stringList2Two, index2));
        SettingLimitBordObj.GetComponent<SettingLimitBord>().SetSettingLimitBord(stateValue1, stateValue2);
    }
    void InitStringList()
    {
        stringList1.Clear();
        stringList1.Add("申请自动加入"); // this.index1 == 0     stateValue1  1
        stringList1.Add("需审核才能加入"); // this.index1 == 1     stateValue1  0
        stringList1.Add("禁止任何人加入"); // this.index1 == 2     stateValue1  -1
        stringList2Two.Clear();
        stringList2Two.Add("无限制");     //this.index2 == 0
        for (int i = 24; i <= 70; i++)
        {
            stringList2Two.Add(i.ToString() + "级");
        }
    }
    string FindTargetStringToShow(List<string> stringList, int index)
    {
        //Debug.LogError(stringList.Count);
        for (int i = 0; i < stringList.Count; i++)
        {
            //Debug.Log(stringList[i]);
            if (i == index)
            {
                return stringList[i];
            }
        }
        return "";
    }
    void ClearUIGride()
    {
        for (int i = uiGride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGride.transform.GetChild(i).gameObject);
        }
    }
    void UpdateTime()
    {
        if (leftTime > 0)
        {
            string houreStr = (leftTime / 3600).ToString("00");
            string miniteStr = (leftTime % 3600 / 60).ToString("00");
            string secondStr = (leftTime % 60).ToString("00");
            LeftTimeLabel.text = string.Format("{0}:{1}:{2}", houreStr, miniteStr, secondStr);
            //leftTime -= 1;
        }
        else
        {
            LeftTimeLabel.transform.parent.gameObject.SetActive(false);
            CallButton.SetActive(true);
        }
    }
}
