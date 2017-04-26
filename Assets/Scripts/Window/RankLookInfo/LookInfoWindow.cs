using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LookInfoWindow : MonoBehaviour 
{
    [SerializeField]
    private UITexture icon;
    [SerializeField]
    private UILabel vipLabel;

    [SerializeField]
    private UILabel name;
    [SerializeField]
    private UILabel level;
    [SerializeField]
    private UILabel rank;
    [SerializeField]
    private UILabel fight;
    [SerializeField]
    private UILabel gongHui;
    [SerializeField]
    private UISprite avatar;
    [SerializeField]
    private UISprite vNumber;
    [SerializeField]
    private GameObject closeButton;
    [SerializeField]
    private GameObject lookInfoItem;
    [SerializeField]
    private GameObject uiGride;


    [SerializeField]
    private UILabel TopVip;

	// Use this for initialization
	void Start () 
    {
        UIEventListener.Get(closeButton).onClick = ClickCloseButton;
	}
    void OnEnable()
    {
        //NetworkHandler.instance.SendProcess("6008#3;");
    }
	// Update is called once per frame
	void Update () 
    {
	
	}
    public void SetLookInfoWindow(string _rankType, string _playerId, string _name,string _icon,string _VIPLv,string _level, string _fight, string _gongHuiName, string _levelRank, string _fightRank, List<RoleInfoForRank> _roleList)
    {
        name.text = _name;
        level.text = _level;
        fight.text = _fight;
        string _gongHuiStr = "";
        switch (_gongHuiName)
        {
            case "": _gongHuiStr = "无"; break;
            default: _gongHuiStr = _gongHuiName; break;
        }
        gongHui.text = _gongHuiStr;
        avatar.spriteName = _icon;
        icon.mainTexture = Resources.Load(string.Format("Head/{0}", _icon), typeof(Texture)) as Texture;
        vipLabel.text = "VIP" + _VIPLv;
        TopVip.text = "V" + _VIPLv;
        vNumber.spriteName = _VIPLv;
        //排行类型是3，战力排行
        if (_rankType == "3")
        {
            switch(_fightRank)
            {
                case "0": rank.text = "未上榜"; break;
                default: rank.text = _fightRank; break;
            }
        }
        else
        {
            switch (_levelRank)
            {
                case "0": rank.text = "未上榜"; break;
                //default: rank.text = _levelRank; break;//等级排行
                default: rank.text = _fightRank; break;//战力排行
            }
        }
        DestroyGride();
        for (int i = 0; i < _roleList.Count;i++ )
        {
            GameObject obj = NGUITools.AddChild(uiGride, lookInfoItem);
            obj.GetComponent<LookInfoItem>().SetLookInfoItem(_roleList[i]);
        }
        uiGride.GetComponent<UIGrid>().Reposition();
    }
    void DestroyGride()
    {
        for (int i = uiGride.transform.childCount -1; i > 0; i--)
        {
            DestroyImmediate(uiGride.transform.GetChild(i).gameObject);
        }
    }
    private void ClickCloseButton(GameObject go)
    {
        if(go != null)
        {
            UIManager.instance.BackUI();
        }
    }
}
