using UnityEngine;
using System.Collections;

public class CreateTeamWindow : MonoBehaviour {

    public GameObject Toggle1;
    public GameObject Toggle2;
    public GameObject Toggle3;
    public GameObject Toggle4;

    public GameObject LeftButton;
    public GameObject RightButton;
    public GameObject CreateButton;
    public GameObject CloseButton;
    public GameObject SureButton;

    public UIInput uiInput;
    public UILabel RankLabel;//限制条件

    protected int SettingCounter;//队伍设置状态 1-任何人 2-军团 3-好友
    private string SettingStr;//"0/0","1/characterId","2/legionID"
    //private string[] PlayerSet = new string[] { "任何人", "军团成员", "仅限好友" };

    protected bool PasswordCounter;//密码状态 true开启
    private string wordValue;//密码值

    private int LevelCounter = 0;//计数器
    //private string[] Levellimit = new string[] { "无限制", "等级30", "等级40", "等级50" };

    private int[] Levellimit = new int[] { 0, 20, 30, 40, 50, 60, 70 };


    //string limitone;
    //string limittwo;
    //string limitthree;
    void OnEnable()
    {
        //SettingCounter = 0;
        SettingStr = "0-0";
        //limitone = "0-0";
        //limitthree = "0";
        PasswordCounter = false;
    }

	void Start () {
        //SetCreateTeamWindowInfo(limitone, limittwo, limitthree);
        if (UIEventListener.Get(Toggle1).onClick == null) 
        {
            UIEventListener.Get(Toggle1).onClick += delegate(GameObject go)
            {
                //SettingCounter = 0;
                SettingStr = "0-0";
            };
        }
        if (UIEventListener.Get(Toggle2).onClick == null)
        {
            UIEventListener.Get(Toggle2).onClick += delegate(GameObject go)
            {
                //SettingCounter = 1;
                SettingStr = "1-" + CharacterRecorder.instance.legionID.ToString();//CharacterRecorder.CharacterID.ToString();
            };
        }
        if (UIEventListener.Get(Toggle3).onClick == null)
        {
            UIEventListener.Get(Toggle3).onClick += delegate(GameObject go)
            {
                //SettingCounter = 2;
                SettingStr = "2-" + CharacterRecorder.instance.userId.ToString();//CharacterRecorder.instance.legionID.ToString();
            };
        }
        if (UIEventListener.Get(Toggle4).onClick == null)
        {
            UIEventListener.Get(Toggle4).onClick += delegate(GameObject go)
            {
                if (PasswordCounter)
                {
                    PasswordCounter = false;
                }
                else 
                {
                    PasswordCounter = true;
                }

                if (PasswordCounter)
                {
                    Toggle4.transform.Find("Checkmark").gameObject.SetActive(true);
                    uiInput.GetComponent<BoxCollider>().enabled = true;
                }
                else 
                {
                    Toggle4.transform.Find("Checkmark").gameObject.SetActive(false);
                    uiInput.GetComponent<BoxCollider>().enabled = false;
                }
            };
        }

        if (UIEventListener.Get(LeftButton).onClick == null) 
        {
            UIEventListener.Get(LeftButton).onClick += delegate(GameObject go)
            {
                if (LevelCounter > 0)
                {
                    LevelCounter -= 1;
                }
                else 
                {
                    LevelCounter = 0;
                }
                if (LevelCounter == 0)
                {
                    RankLabel.text = "无限制";
                }
                else 
                {
                    RankLabel.text = "等级"+Levellimit[LevelCounter].ToString();
                }
            };
        }
        if (UIEventListener.Get(RightButton).onClick == null)
        {
            UIEventListener.Get(RightButton).onClick += delegate(GameObject go)
            {
                if (LevelCounter < Levellimit.Length-1)
                {
                    LevelCounter += 1;
                }
                else 
                {
                    LevelCounter = Levellimit.Length-1;
                }
                if (LevelCounter == 0)
                {
                    RankLabel.text = "无限制";
                }
                else
                {
                    RankLabel.text = "等级" + Levellimit[LevelCounter].ToString();
                }
            };
        }

        if (UIEventListener.Get(CreateButton).onClick == null)
        {
            UIEventListener.Get(CreateButton).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.GuideID[38] == 14)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
                if (PasswordCounter)
                {
                    wordValue = uiInput.GetComponent<UIInput>().value;
                    if (wordValue == "")
                    {
                        UIManager.instance.OpenPromptWindow("请输入房间密码", PromptWindow.PromptType.Hint, null, null);
                    }
                    else 
                    {
                        if (wordValue.Length < 4)
                        {
                            UIManager.instance.OpenPromptWindow("密码长度不足4位", PromptWindow.PromptType.Hint, null, null);
                        }
                        else 
                        {                            
                            //NetworkHandler.instance.SendProcess("6102#" + CharacterRecorder.instance.CopyNumber + ";" + SettingCounter + ";" + wordValue + ";" + LevelCounter + ";" + "1;");
                            NetworkHandler.instance.SendProcess("6102#" + CharacterRecorder.instance.CopyNumber + ";" + SettingStr + ";" + wordValue + ";" + LevelCounter + ";" + "1;");
                            this.gameObject.SetActive(false);
                        }                       
                    }
                }
                else 
                {
                    NetworkHandler.instance.SendProcess("6102#" + CharacterRecorder.instance.CopyNumber + ";" + SettingStr + ";" + "0" + ";" + LevelCounter + ";" + "1;");
                    this.gameObject.SetActive(false);
                }
            };
        }
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                //UIManager.instance.BackUI();
                this.gameObject.SetActive(false);
            };
        }
	}

    public void OnTeamInvitationWindow() 
    {
        CreateButton.SetActive(false);
        SureButton.SetActive(true);
        if (UIEventListener.Get(SureButton).onClick == null)
        {
            UIEventListener.Get(SureButton).onClick += delegate(GameObject go)
            {
                if (PasswordCounter)
                {
                    wordValue = uiInput.GetComponent<UIInput>().value;
                    if (wordValue.Length < 4)
                    {
                        UIManager.instance.OpenPromptWindow("密码长度不足4位", PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        NetworkHandler.instance.SendProcess("6102#" + CharacterRecorder.instance.CopyNumber + ";" + SettingStr + ";" + wordValue + ";" + LevelCounter + ";" + "2;");
                        this.gameObject.SetActive(false);
                    }                     
                }
                else
                {
                    NetworkHandler.instance.SendProcess("6102#" + "1;" + SettingStr + ";" + "0" + ";" + LevelCounter + ";" + "2");
                    this.gameObject.SetActive(false);
                }
                
            };
        }
    }


    public void SetCreateTeamWindowInfo(string limitone, string limittwo, string limitthree) 
    {
        string[] trcSplit = limitone.Split('-');
        Debug.Log("limitone " + limitone);
        if (trcSplit[0] == "0") 
        {
            Toggle1.GetComponent<UIToggle>().startsActive = true;
            Toggle1.GetComponent<UIToggle>().value = true;
            SettingStr = "0-0";
            Debug.Log("1trcSplit[0] " + trcSplit[0]);
        }
        else if (trcSplit[0] == "1") 
        {
            Toggle1.GetComponent<UIToggle>().startsActive = false;
            Toggle1.GetComponent<UIToggle>().value = false;
            Toggle3.GetComponent<UIToggle>().startsActive = false;
            Toggle3.GetComponent<UIToggle>().value = false;
            Toggle2.GetComponent<UIToggle>().startsActive = true;
            Toggle2.GetComponent<UIToggle>().value = true;
            SettingStr = "1-" + CharacterRecorder.instance.legionID.ToString();
            Debug.Log("2trcSplit[0] " + trcSplit[0]);
        }
        else if (trcSplit[0] == "2") 
        {
            Toggle1.GetComponent<UIToggle>().startsActive = false;
            Toggle1.GetComponent<UIToggle>().value = false;
            Toggle2.GetComponent<UIToggle>().startsActive = false;
            Toggle2.GetComponent<UIToggle>().value = false;
            Toggle3.GetComponent<UIToggle>().startsActive = true;
            Toggle3.GetComponent<UIToggle>().value = true;
            SettingStr = "2-" + CharacterRecorder.instance.userId.ToString();
            Debug.Log("3trcSplit[0] " + trcSplit[0]);
        }

        if (limitthree == "0")
        {
            LevelCounter = 0;
            RankLabel.text = "无限制";
        }
        else
        {
            LevelCounter = int.Parse(limitthree);
            RankLabel.text = "等级" + Levellimit[int.Parse(limitthree)].ToString();
        }

    }   
}
