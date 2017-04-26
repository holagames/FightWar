using UnityEngine;
using System.Collections;

public class TeamBrowseItemDate 
{
    public int copyNumber { get; set; }//
    public int myTeamId { get; set; }


    public int teamId { get; set; }
    public int teamstate { get; set; }
    public string name { get; set; }
    public int level { get; set; }
    public string icon { get; set; }
    public int vipLv { get; set; }
    public int fight { get; set; }
    public int peopleNum { get; set; }
    public string condition1 { get; set; }
    public string condition2 { get; set; }
    public string condition3 { get; set; }

    public TeamBrowseItemDate(int _copyNumber,int _myTeamId,int _teamId, int _teamstate, string _name, int _level,string _icon, int _vipLv, int _fight, int _peopleNum, string _condition1, string _condition2, string _condition3) 
    {
        this.copyNumber = _copyNumber;
        this.myTeamId = _myTeamId;
        this.teamId = _teamId;
        this.teamstate = _teamstate;
        this.name = _name;
        this.level = _level;
        this.icon = _icon;
        this.vipLv = _vipLv;
        this.fight = _fight;
        this.peopleNum = _peopleNum;
        this.condition1 = _condition1;
        this.condition2 = _condition2;
        this.condition3 = _condition3;
    }
}
public class TeamBrowseItem : MonoBehaviour {
    public UITexture IconSprite;
    
    public UILabel vipLabel;
    public UILabel copyNameLabel;
    public UILabel characterNameLabel;
    public UILabel characterLevelLabel;
    public UILabel limitLabel;
    public UILabel fightLabel;
    public UILabel peopleNumlabel;

    public GameObject Lock;
    public GameObject JoinButton;

    public int teamId;
    public int peopleNum;
    public string condition2;

    private int[] Levellimit = new int[] { 0, 20, 30, 40, 50, 60, 70 };
    private TeamBrowseItemDate OneTeamBrowseItemDate;


	void Start () {
        if (UIEventListener.Get(JoinButton).onClick == null)
        {
            UIEventListener.Get(JoinButton).onClick += delegate(GameObject go)
            {
                if (OneTeamBrowseItemDate.teamstate == 1) 
                {
                    UIManager.instance.OpenPromptWindow("副本进行中", PromptWindow.PromptType.Hint, null, null);
                }
                else if (OneTeamBrowseItemDate.peopleNum == 5)
                {
                    UIManager.instance.OpenPromptWindow("房间已满", PromptWindow.PromptType.Hint, null, null);
                }
                else if (IsReachLevel()==false)
                {
                    UIManager.instance.OpenPromptWindow("等级不足", PromptWindow.PromptType.Hint, null, null);
                }
                else if (IsMember() != 0) 
                {
                    if (IsMember() == 2)
                    {
                        UIManager.instance.OpenPromptWindow("不是好友，无法加入房间", PromptWindow.PromptType.Hint, null, null);
                    }
                    else 
                    {
                        UIManager.instance.OpenPromptWindow("不是同一军团，无法加入房间", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else if (IsPassWord())
                {
                    GameObject.Find("TeamBrowseWindow").transform.Find("EnterPasswordWindow").gameObject.SetActive(true);
                    GameObject.Find("TeamBrowseWindow").transform.Find("EnterPasswordWindow").gameObject.GetComponent<EnterPasswordWindow>().SetEnterPasswordWindow(OneTeamBrowseItemDate.teamId, OneTeamBrowseItemDate.condition2);
                }
                else
                {
                    NetworkHandler.instance.SendProcess("6103#" + OneTeamBrowseItemDate.teamId + ";");
                }
            };
        }
	}

    public void SetTeamBrowse(TeamBrowseItemDate _OneTeamBrowseItemDate) 
    {
        this.OneTeamBrowseItemDate = _OneTeamBrowseItemDate;
        this.teamId = _OneTeamBrowseItemDate.teamId;
        this.peopleNum = _OneTeamBrowseItemDate.peopleNum;
        this.condition2 = _OneTeamBrowseItemDate.condition2;

        IconSprite.mainTexture = Resources.Load(string.Format("Head/{0}", _OneTeamBrowseItemDate.icon), typeof(Texture)) as Texture;
        vipLabel.text = _OneTeamBrowseItemDate.vipLv.ToString();
        //copyNameLabel.text = "";
        characterNameLabel.text = _OneTeamBrowseItemDate.name;
        characterLevelLabel.text ="Lv."+_OneTeamBrowseItemDate.level.ToString();
        if (_OneTeamBrowseItemDate.condition3 == "0")
        {
            limitLabel.text = "无限制";
        }
        else 
        {
            limitLabel.text = Levellimit[int.Parse(_OneTeamBrowseItemDate.condition3)].ToString()+"级";
        }
        //limitLabel.text = _OneTeamBrowseItemDate.condition3;
        fightLabel.text = _OneTeamBrowseItemDate.fight.ToString();
        peopleNumlabel.text = _OneTeamBrowseItemDate.peopleNum.ToString() + "/5";

        foreach (var item in TextTranslator.instance.TeamGateList) 
        {
            if (item.GroupID == _OneTeamBrowseItemDate.copyNumber) 
            {
                copyNameLabel.text = item.Name;
                break;
            }
        }
        if (_OneTeamBrowseItemDate.condition2 == "0")
        {
            Lock.SetActive(false);
        }
        else 
        {
            Lock.SetActive(true);
        }
    }

    bool IsPassWord() //是否密码
    {
        if (OneTeamBrowseItemDate.condition2 == "0")
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    bool IsReachLevel() //是否等级
    {
        if (CharacterRecorder.instance.level < Levellimit[int.Parse(OneTeamBrowseItemDate.condition3)])
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    int IsMember() //是否军团好友无限制
    {
        string[] trcSplit=OneTeamBrowseItemDate.condition1.Split('-');
        int num = 0;
        if (trcSplit[0]=="0") 
        {
            num=0;
        }
        else if (trcSplit[0]=="2")
        {
            if(!CharacterRecorder.instance.MyFriendUIDList.Contains(int.Parse(trcSplit[1])))
            {
                num = 2;
            }              
        }
        else if (trcSplit[0]=="1") 
        {
            if (trcSplit[1] != "0"&&CharacterRecorder.instance.legionID.ToString()!=trcSplit[1]) 
            {
                num = 1;
            }
        }
        return num;
    }
}
