using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FriendItemSmall: MonoBehaviour 
{
    //基本信息
    [SerializeField]
    private UITexture icon;
    [SerializeField]
    private UILabel vipLabel;
    [SerializeField]
    private UILabel nameLabel;
    [SerializeField]
    private UILabel fightLabel;
    [SerializeField]
    private UILabel lastLoignLabel;
    public GameObject selectButton;
    public FriendItemData OneFriendItemData;
	// Use this for initialization
	void Start () 
    {
        if (UIEventListener.Get(this.gameObject).onClick == null)
        {
            UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("1020#" + OneFriendItemData.userId + ";");
                UIManager.instance.OpenPanel("LegionMemberItemDetail", false);
            };
        }
        if (UIEventListener.Get(selectButton).onClick == null)
        {
            UIEventListener.Get(selectButton).onClick += delegate(GameObject go)
            {
                LegionMailWindow _LegionMailWindow = GameObject.Find("LegionMailWindow").GetComponent<LegionMailWindow>();
                _LegionMailWindow.selectFriendData = OneFriendItemData;
                _LegionMailWindow.friendNameLabel.text = OneFriendItemData.name;
                UIManager.instance.BackUI();
            };
        }
       
	}
	
    public void SetFriendItem(FriendItemData _OneFriendItemData)
    {
        this.OneFriendItemData = _OneFriendItemData;
        icon.mainTexture = Resources.Load(string.Format("Head/{0}", _OneFriendItemData.icon), typeof(Texture)) as Texture;
        nameLabel.text = _OneFriendItemData.name + "  Lv." + _OneFriendItemData.level.ToString();
        vipLabel.text = "VIP" + _OneFriendItemData.vipLv.ToString();
        fightLabel.text = string.Format("战斗力: {0}", _OneFriendItemData.fight.ToString());
        if (_OneFriendItemData.lastLoginTime == 0)
        {
           // lastLoignLabel.text = "在线";
            lastLoignLabel.text = "不在线";
        }
        else
        {
           // lastLoignLabel.text = string.Format("登陆:{0}之前", _OneFriendItemData.lastLoginTime / 3600 > 0 ? _OneFriendItemData.lastLoginTime / 3600 + "小时" : _OneFriendItemData.lastLoginTime / 60 + "分钟");
            lastLoignLabel.text = "在线";
        }
        //lastLoignLabel.text = string.Format("登陆:{0}之前",_OneFriendItemData.lastLoginTime/3600 > 0 ? _OneFriendItemData.lastLoginTime/3600 + "小时": _OneFriendItemData.lastLoginTime/60 + "分钟");
    }
}
