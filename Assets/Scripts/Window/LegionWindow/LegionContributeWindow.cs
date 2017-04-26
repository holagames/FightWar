using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionContributeWindow: MonoBehaviour 
{
    public UISlider progressSlider;//总长度 212
    public UISlider expSlider;
    public UILabel expLabel;
    public UILabel contributeProgressLabel;
    public UILabel memberNumContributeLabel;
    public UILabel legionLevelLabel;

    public UISprite legionFlagIcon;

    public GameObject CloseButton;
    public GameObject SureButton;
    public GameObject doubltButton;
    public GameObject DoubltObj;
    public GameObject DoubltObjCloseButton;
    public List<GameObject> boxButtonList = new List<GameObject>();
    public List<UIToggle> contributeToggleList = new List<UIToggle>();
    private List<int> boxButtonState = new List<int>();
    private int contributeProgress = 0;
    private int memberNumContribute = 0;
    private int curContributeType = 0;
    private int todayLeftTimes;//今日捐献剩余次数
	// Use this for initialization
	void Start () 
    {
        UIManager.instance.CountSystem(UIManager.Systems.军团捐献);
        UIManager.instance.UpdateSystems(UIManager.Systems.军团捐献);

        NetworkHandler.instance.SendProcess("8006#;");//string.Format("8006#{0};", CharacterRecorder.instance.legionID)
        for (int i = 0; i < boxButtonList.Count; i++)
        {
            UIEventListener.Get(boxButtonList[i]).onClick = ClickBoxButton;
        }
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }
        if (UIEventListener.Get(SureButton).onClick == null)
        {
            UIEventListener.Get(SureButton).onClick += delegate(GameObject go)
            {
                int curType = GetCurContributeType();
                curContributeType = curType;
                if (curType == 0)
                {
                    UIManager.instance.OpenPromptWindow("请选择一种捐献类型", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    // if (this.memberNumContribute < 30 + (CharacterRecorder.instance.myLegionData.legionLevel - 1) * 20 && todayLeftTimes > 0)
                    if ( todayLeftTimes > 0)
                    {
                        if (curType == 3 && CharacterRecorder.instance.Vip < 3)
                        {
                            UIManager.instance.OpenPromptWindow("达到VIP3才可进行高级捐献", PromptWindow.PromptType.Hint, null, null);
                        }
                        else
                        {
                            NetworkHandler.instance.SendProcess(string.Format("8007#{0};", curType));
                        }
                    }
                    else if (todayLeftTimes > 0)
                    {
                        UIManager.instance.OpenPromptWindow("捐献人数已达上限", PromptWindow.PromptType.Hint, null, null);
                    }
                    else 
                    {
                        UIManager.instance.OpenPromptWindow("今天已经捐献过啦", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                //UIManager.instance.BackUI();
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
       // SetBasicInfo();
	}

    public void SetLegionContributeWindow(int todayLeftTimes, int legionSumContribute, int upGradeContribute,int contributeProgress,int memberNumContribute ,List<int> boxGetSateList)
    {
        this.todayLeftTimes = todayLeftTimes;
        this.contributeProgress = contributeProgress;
        this.memberNumContribute = memberNumContribute;
        boxButtonState = boxGetSateList;
        legionFlagIcon.spriteName = string.Format("legionFlag{0}", CharacterRecorder.instance.legionFlag);
        contributeProgressLabel.text = contributeProgress.ToString();
        progressSlider.value = (float)contributeProgress / 63;//240
        if(progressSlider.value > 1)
        {
            progressSlider.value = 0;
        }
        int _maxBoxIndex = GetMaxBoxIndex(contributeProgress);
        for (int i = 0; i < boxButtonList.Count;i++ )
        {
            /*if (i <= _maxBoxIndex)
            {
                boxButtonList[i].GetComponent<UISprite>().color = new Color(255 / 255.0f, 255 / 255.0f, 255 / 255.0f,1/255.0f);
                boxButtonList[i].transform.FindChild("WF_HeZi_UIeff").gameObject.SetActive(true);
            }
            else
            {
                boxButtonList[i].GetComponent<UISprite>().color = new Color(255 / 255.0f, 255 / 255.0f, 255 / 255.0f, 255 / 255.0f);
                boxButtonList[i].transform.FindChild("WF_HeZi_UIeff").gameObject.SetActive(false);
            }*/
            switch (boxGetSateList[i])
            {
                case 0:
                    boxButtonList[i].GetComponent<UISprite>().color = new Color(255 / 255.0f, 255 / 255.0f, 255 / 255.0f, 255 / 255.0f);
                    boxButtonList[i].transform.FindChild("WF_HeZi_UIeff").gameObject.SetActive(false);
                    break;
                case 1:
                    boxButtonList[i].GetComponent<UISprite>().color = new Color(255 / 255.0f, 255 / 255.0f, 255 / 255.0f,1/255.0f);
                    boxButtonList[i].transform.FindChild("WF_HeZi_UIeff").gameObject.SetActive(true);
                    break;
                case 2:
                    boxButtonList[i].GetComponent<UISprite>().color = new Color(255 / 255.0f, 255 / 255.0f, 255 / 255.0f, 255 / 255.0f);
                    boxButtonList[i].GetComponent<UISprite>().spriteName = "hezi2";
                    boxButtonList[i].transform.FindChild("WF_HeZi_UIeff").gameObject.SetActive(false);
                    break;
            }
        }
        //memberNumContributeLabel.text = memberNumContribute.ToString();
        Legion _myLegion = TextTranslator.instance.GetLegionByNeedExp(upGradeContribute);
        if (_myLegion != null)
        {
            CharacterRecorder.instance.myLegionData.legionLevel = _myLegion.Level;
            SetBasicInfo();
            memberNumContributeLabel.text = string.Format("{0}/{1}", memberNumContribute, _myLegion.LimitNum);// 30 + (CharacterRecorder.instance.myLegionData.legionLevel - 1) * 2);//_LegionItemData.MemberNumber.ToString();//军团初始等级 1
           }
        else
        {
            Debug.LogError("没有满足 公会升级经验 的 Legion 数据");    
        }
        expSlider.value = (float)legionSumContribute / upGradeContribute;
        expLabel.text = string.Format("{0}/{1}", legionSumContribute,upGradeContribute);
    }
    public void ResetContributeInfo()
    {
        GameObject _ToolEquipUpEffect = GameObject.Instantiate(Resources.Load("Prefab/Effect/JXCG", typeof(GameObject))) as GameObject;
        //_ToolEquipUpEffect.layer = 11;
        _ToolEquipUpEffect.transform.parent = this.transform.FindChild("All");
        ///_ToolEquipUpEffect.GetComponent<VFXRenderQueueSorter>().mTarget = this.transform.Find("All/SpriteBG").GetComponent<UISprite>();
        _ToolEquipUpEffect.transform.localPosition = new Vector3(0, -30f, 0);// Vector3.zero;
        _ToolEquipUpEffect.transform.localScale = new Vector3(300f, 300f, 300f);//Vector3.one;
        //_ToolEquipUpEffect.GetComponent<VFXRenderQueueSorter>().mTarget = this.transform.Find("All/SpriteBG").GetComponent<UISprite>();

        int addContribute = 0;
        switch (curContributeType)
        {
            case 1: addContribute = 1; break;
            case 2: addContribute = 3; break;
            case 3: addContribute = 5; break;
        }
        this.contributeProgress += addContribute;
        this.memberNumContribute += 1;
        contributeProgressLabel.text = this.contributeProgress.ToString();
        progressSlider.value = (float)this.contributeProgress / 63;//240
        if (progressSlider.value > 1)
        {
            progressSlider.value = 1;
        }
        memberNumContributeLabel.text = string.Format("{0}/{1}", this.memberNumContribute, 30 + (CharacterRecorder.instance.myLegionData.legionLevel - 1) * 2);
        NetworkHandler.instance.SendProcess("8006#;");
    }
    void SetBasicInfo()
    {
        legionLevelLabel.text = string.Format("Lv.{0}",CharacterRecorder.instance.myLegionData.legionLevel);
    }
    int GetCurContributeType()
    {
        for (int i = 0; i < contributeToggleList.Count;i++ )
        {
            if (contributeToggleList[i].value)
            {
                return i + 1;
            }
        }
        return 0;
    }
    int GetMaxBoxIndex(int contributeProgress)
    {
        int maxIdex = -1;
        if (contributeProgress >= 50)
        {
            maxIdex = 3;
        }
        else if (contributeProgress >= 30)
        {
            maxIdex = 2;
        }
        else if (contributeProgress >= 20)
        {
            maxIdex = 1;
        }
        else if (contributeProgress >= 10)
        {
            maxIdex = 0;
        }
        return maxIdex;
    }

    void ClickBoxButton(GameObject go)
    {
        switch (boxButtonState[int.Parse(go.name[9].ToString()) - 1])
        {
            case 0: 
               // UIManager.instance.OpenPromptWindow("不可领", PromptWindow.PromptType.Hint, null, null); 
                UIManager.instance.OpenPanel("ActivenessWindow", false);
                GameObject.Find("ActivenessWindow").GetComponent<ActivenessWindow>().SetLegionContributeBoxIsOpen(CharacterRecorder.instance.myLegionData.legionLevel, int.Parse(go.name[9].ToString()), 0);
                break;
            case 1: NetworkHandler.instance.SendProcess(string.Format("8015#{0};", go.name[9])); break;
            case 2: 
                //UIManager.instance.OpenPromptWindow("已经领过奖励啦", PromptWindow.PromptType.Hint, null, null); 
                UIManager.instance.OpenPanel("ActivenessWindow", false);
                GameObject.Find("ActivenessWindow").GetComponent<ActivenessWindow>().SetLegionContributeBoxIsOpen(CharacterRecorder.instance.myLegionData.legionLevel,int.Parse(go.name[9].ToString()), 2);
                break;
        }
    }
}
