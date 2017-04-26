using UnityEngine;
using System.Collections;

public class FriendListItem : MonoBehaviour {

    //基本信息
    [SerializeField]
    private UITexture textureBg;
    [SerializeField]
    private UITexture icon;
    [SerializeField]
    private UILabel vipLabel;
    [SerializeField]
    private UILabel nameLabel;
    [SerializeField]
    private UILabel fightLabel;
    [SerializeField]
    private UILabel onlineLabel;

    public GameObject InviteButton;

    public UIAtlas NormalAtlas;
    public UIAtlas CommonAtlas;

    private FriendItemData OneFriendItemData;
    private string TeamName;
    //private float datatime = 0;
	void Start () {
        if (UIEventListener.Get(InviteButton).onClick == null) 
        {
            UIEventListener.Get(InviteButton).onClick += delegate(GameObject go)
            {
                TeamInvitationWindow TI = GameObject.Find("TeamInvitationWindow").GetComponent<TeamInvitationWindow>();
                if (TI != null && TI.datatime >= 30)
                {
                    TI.datatime = 0;
                    GetTeamName();
                    int num = GameObject.Find("TeamInvitationWindow").GetComponent<TeamInvitationWindow>().TeamPeopleNum();
                    //NetworkHandler.instance.SendProcess("7001#" + "1;" + CharacterRecorder.instance.characterName + "组织新冒险," + TeamName + ",大家快来参加! 房间号：" + CharacterRecorder.instance.TeamID + ";" + CharacterRecorder.instance.Vip + "$" + CharacterRecorder.instance.headIcon + "$0;");
                    NetworkHandler.instance.SendProcess("7001#" + "6;组织新冒险," + TeamName + ",大家快来参加! 房间号：" + CharacterRecorder.instance.TeamID + ";" + CharacterRecorder.instance.Vip + "$" + CharacterRecorder.instance.headIcon + "$" + CharacterRecorder.instance.legionID + "$" + CharacterRecorder.instance.NationID + "$" + CharacterRecorder.instance.userId + "$" + CharacterRecorder.instance.legionCountryID + ";");
                    NetworkHandler.instance.SendProcess("7002#10;" + CharacterRecorder.instance.characterName + ";" + "组织新冒险," + TeamName + "(人数 " + num + ")" + ",大家快来参加! 房间号：" + CharacterRecorder.instance.TeamID + ";" + CharacterRecorder.instance.TeamID + ";");
                }
                else if (TI != null && TI.datatime<30)
                {
                    UIManager.instance.OpenPromptWindow("请在冷却时间过后再邀请", PromptWindow.PromptType.Hint, null, null);
                }
                //GetTeamName();
                ////NetworkHandler.instance.SendProcess("7001#" + "1;" + "组队邀请! " + OneFriendItemData.ToString()+";"+ CharacterRecorder.instance.Vip + "$" + CharacterRecorder.instance.headIcon+";");
                //NetworkHandler.instance.SendProcess("7001#" + "1;" + CharacterRecorder.instance.characterName + "组织新冒险," + TeamName + ",大家快来参加! 房间号:" + CharacterRecorder.instance.TeamID + ";" + CharacterRecorder.instance.Vip + "$" + CharacterRecorder.instance.headIcon + ";");
                //NetworkHandler.instance.SendProcess("7002#10;" + CharacterRecorder.instance.characterName + ";" + "组织新冒险," + TeamName + ",大家快来参加! 房间号:" + CharacterRecorder.instance.TeamID + ";" + "0;");
            };
        }

        //CancelInvoke();
        //InvokeRepeating("UpdateTime", 0, 1.0f);
	}

    
    //void UpdateTime()
    //{
    //    datatime += 1;
    //    if (datatime >= 30)
    //    {
    //        datatime = 30;
    //    }
    //}
    void GetTeamName()
    {
        foreach (var Item in TextTranslator.instance.TeamGateList)
        {
            if (Item.GroupID == CharacterRecorder.instance.CopyNumber)
            {
                this.TeamName = Item.Name;
                break;
            }
        }
    }
    public void GetFriendListItem(FriendItemData _OneFriendItemData) 
    {
        this.OneFriendItemData = _OneFriendItemData;
        if (_OneFriendItemData.lastLoginTime > 0)
        {
            icon.mainTexture = Resources.Load(string.Format("Head/{0}", _OneFriendItemData.icon), typeof(Texture)) as Texture;
            vipLabel.text = "VIP" + _OneFriendItemData.vipLv.ToString();
            nameLabel.text = _OneFriendItemData.name;
            onlineLabel.text = "在线";
            fightLabel.text = _OneFriendItemData.fight.ToString();
        }
        else 
        {
            this.gameObject.GetComponent<UISprite>().atlas = CommonAtlas;
            this.gameObject.GetComponent<UISprite>().spriteName = "zudui_di";
            textureBg.mainTexture = Resources.Load("Game/zudui1", typeof(Texture)) as Texture;
            icon.mainTexture = Resources.Load(string.Format("Head/{0}", _OneFriendItemData.icon), typeof(Texture)) as Texture;
            vipLabel.text = "VIP" + _OneFriendItemData.vipLv.ToString();
            nameLabel.text = _OneFriendItemData.name;
            onlineLabel.text = "不在线";
            fightLabel.text = _OneFriendItemData.fight.ToString();
        }
    }
}
