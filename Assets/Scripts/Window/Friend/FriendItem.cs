using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class FriendItemData
{
    public int tabIndex { get; set; }

    public int userId { get; set; }
    public string name { get; set; }
    public int level { get; set; }
    public int fight { get; set; }
    public string icon { get; set; }
    public int vipLv { get; set; }
    public int lastLoginTime { get; set; }

    public int canGetSpriteState { get; set; }//好友送我体力
    public int giveSpriteState { get; set; }//我送好友体力

    public string LoginName { get; set; }
    public int CapturedNumber { get; set; }

    public FriendItemData(int tabIndex, int userId, string name, int level, int fight, string icon, int vipLv, int lastLoginTime, int canGetSpriteNum, int giveSpriteNum, string _LoginName, int _CapturedNumber)
    {
        this.tabIndex = tabIndex;
        this.userId = userId;
        this.name = name;
        this.level = level;
        this.fight = fight;
        this.icon = icon;
        this.vipLv = vipLv;
        this.lastLoginTime = lastLoginTime;
        this.canGetSpriteState = canGetSpriteNum;
        this.giveSpriteState = giveSpriteNum;
        this.LoginName = _LoginName;
        this.CapturedNumber = _CapturedNumber;
    }
    public FriendItemData(int tabIndex, int userId, string name, int level, int fight, string icon, int vipLv, int lastLoginTime)
    {
        this.tabIndex = tabIndex;
        this.userId = userId;
        this.name = name;
        this.level = level;
        this.fight = fight;
        this.icon = icon;
        this.vipLv = vipLv;
        this.lastLoginTime = lastLoginTime;
        this.canGetSpriteState = 0;
        this.giveSpriteState = 0;
    }
}
public class FriendItem: MonoBehaviour 
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
    //TabIndex 0 按钮
    public GameObject GetButton;
    public GameObject GiveButton;
    //TabIndex 1 按钮
    public GameObject ApplayButton;
    //TabIndex 2 按钮
    public GameObject RefuseButton;
    public GameObject AgreeButton;
    //不同Part
    [SerializeField]
    private List<GameObject> ObjListOfTabType;

    public FriendItemData OneFriendItemData;
    private int tabIndex;
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
        if (UIEventListener.Get(GetButton).onClick == null)
        {
            UIEventListener.Get(GetButton).onClick += delegate(GameObject go)
            {
                switch (OneFriendItemData.canGetSpriteState)
                {
                    case 1: NetworkHandler.instance.SendProcess("7108#" + OneFriendItemData.userId + ";"); break;
                    case 2: UIManager.instance.OpenPromptWindow("已经领过啦", PromptWindow.PromptType.Hint, null, null); break;
                }
                //NetworkHandler.instance.SendProcess("7108#" + OneFriendItemData.userId + ";");
            };
        }
        if (UIEventListener.Get(GiveButton).onClick == null)
        {
            UIEventListener.Get(GiveButton).onClick += delegate(GameObject go)
            {
                if (tabIndex != 3)
                {
                    NetworkHandler.instance.SendProcess("7107#" + OneFriendItemData.userId + ";");
                }
                else
                {
                    //NetworkHandler.instance.SendProcess("7111#" + OneFriendItemData.userId + ";");
                    UIManager.instance.OpenPromptWindowWithCost(String.Format("自己可以获得 {0} ×1\n对方可以获得 {1} ×1", TextTranslator.instance.GetItemNameByItemCode(20014),
                    TextTranslator.instance.GetItemNameByItemCode(20015)), 5, 50, PromptWindowWithCost.PromptType.Confirm, Confirm, null);
                }
            };
        }
        if (UIEventListener.Get(ApplayButton).onClick == null)
        {
            UIEventListener.Get(ApplayButton).onClick += delegate(GameObject go)
            {
                if (OneFriendItemData.userId != CharacterRecorder.instance.userId)
                {
                    NetworkHandler.instance.SendProcess("7104#" + OneFriendItemData.userId + ";");
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("不可添加自己为好友", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        if (UIEventListener.Get(RefuseButton).onClick == null)
        {
            UIEventListener.Get(RefuseButton).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("7106#" + OneFriendItemData.userId + ";");
            };
        }
        if (UIEventListener.Get(AgreeButton).onClick == null)
        {
            UIEventListener.Get(AgreeButton).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("7105#" + OneFriendItemData.userId + ";");
            };
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    public void SetFriendItem(int _tabIndex, FriendItemData _OneFriendItemData)
    {
        tabIndex = _tabIndex;
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
        if (_tabIndex == 3)
        {
            ObjListOfTabType[0].SetActive(true);
        }
        else
        {
            for (int i = 0; i < ObjListOfTabType.Count; i++)
            {
                if (i == (int)_tabIndex)
                {
                    ObjListOfTabType[i].SetActive(true);
                }
                else
                {
                    ObjListOfTabType[i].SetActive(false);
                }
            }
        }
        switch (_tabIndex)
        {
            case 0:
                switch (_OneFriendItemData.canGetSpriteState)
                {
                    case 0: GetButton.SetActive(false); break;
                    case 1: GetButton.SetActive(true); GetButton.GetComponent<UISprite>().spriteName = "buttonGet"; break;
                    case 2: GetButton.GetComponent<UISprite>().spriteName = "PVPIntegration1"; break;
                }
                switch (_OneFriendItemData.giveSpriteState)
                {
                    case 0: GiveButton.GetComponent<UISprite>().spriteName = "buttonGive0"; break;
                    case 1: GiveButton.GetComponent<UISprite>().spriteName = "buttonGive1"; GiveButton.GetComponent<BoxCollider>().enabled = false; break;
                    //case 2: GiveButton.GetComponent<UISprite>().spriteName = "buttonGive1"; GiveButton.GetComponent<BoxCollider>().enabled = false; break;
                }
                break;
            case 1: ApplayButton.SetActive(true); break;
            case 3:
                GetButton.SetActive(false);
                GiveButton.GetComponent<UISprite>().spriteName = "buttonGive0"; break;
        }
    }
    void Confirm()
    {
        NetworkHandler.instance.SendProcess("7111#" + OneFriendItemData.userId + ";");
    }
}
