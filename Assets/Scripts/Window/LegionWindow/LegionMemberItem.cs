using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LegionMemberItem: MonoBehaviour 
{
    //基本信息
    [SerializeField]
    private UITexture icon;
    [SerializeField]
    private UILabel levelLabel;
    [SerializeField]
    private UILabel vipLabel;
    [SerializeField]
    private UILabel nameLabel;
    [SerializeField]
    private UILabel contributeLabel;
    [SerializeField]
    private UILabel lastLoignLabel;
    [SerializeField]
    private UILabel positionLabel;
    [SerializeField]
    private UILabel todayLabel;

    public GameObject agreeButton;
    public GameObject refuseButton;

    public LegionMemberData OneLegionMemberData;
	// Use this for initialization
	void Start () 
    {
        if (UIEventListener.Get(this.gameObject).onClick == null)
        {
            UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("1020#" + OneLegionMemberData.uId + ";");
                UIManager.instance.OpenPanel("LegionMemberItemDetail",false);
                GameObject.Find("LegionMemberItemDetail").GetComponent<LegionMemberItemDetail>().SetLegionMemberItemDetail(this.OneLegionMemberData);
            };
        }
	}

    public void SetLegionMemberItem(LegionMemberData _OneLegionMemberData)
    {
        this.OneLegionMemberData = _OneLegionMemberData;
        icon.mainTexture = Resources.Load(string.Format("Head/{0}", _OneLegionMemberData.iconHead), typeof(Texture)) as Texture;
        nameLabel.text = _OneLegionMemberData.name;
        vipLabel.text = "VIP" + _OneLegionMemberData.vip.ToString();
        levelLabel.text = "Lv." + _OneLegionMemberData.level.ToString();
        contributeLabel.gameObject.SetActive(true);
        positionLabel.gameObject.SetActive(true);
        todayLabel.gameObject.SetActive(true);
        agreeButton.SetActive(false);
        refuseButton.SetActive(false);
        contributeLabel.text = _OneLegionMemberData.contribute.ToString();
        positionLabel.text = string.Format("{0}", TextTranslator.instance.GetLegionPoisitionByPosId(_OneLegionMemberData.officialPosition));//_OneLegionMemberData.officialPosition);
        todayLabel.text = _OneLegionMemberData.todayContribution.ToString();
        //Debug.LogError(_OneLegionMemberData.lastLoginTime);
        if (_OneLegionMemberData.lastLoginTime == 0)
        {
            //lastLoignLabel.text = "在线";
            lastLoignLabel.text = "不在线";
        }
        else
        {
            //lastLoignLabel.text = string.Format("{0}前", _OneLegionMemberData.lastLoginTime / 3600 > 0 ? _OneLegionMemberData.lastLoginTime / 3600 + "小时" : _OneLegionMemberData.lastLoginTime / 60 > 0 ? _OneLegionMemberData.lastLoginTime / 60 + "分钟" : _OneLegionMemberData.lastLoginTime + "秒");
            lastLoignLabel.text = "在线";
        }
        lastLoignLabel.transform.localPosition = new Vector3(97,0,0);
    }
    public void ResetLegionMemberItem(int uid,string position)
    {
        if (OneLegionMemberData.uId != uid)
        {
            return;
        }
        int _officialPosition = 0;
        switch (position)
        {
            case "0": _officialPosition = 0; break;
            case "1": _officialPosition = 1; break;
            case "2": _officialPosition = 2; break;
            case "3": _officialPosition = 3; break;
        }
        OneLegionMemberData.officialPosition = _officialPosition;
        positionLabel.text = string.Format("{0}", TextTranslator.instance.GetLegionPoisitionByPosId(OneLegionMemberData.officialPosition));//OneLegionMemberData.officialPosition);
    }
    public void ResetLegionMemberItem(string position)
    {
        int _officialPosition = 0;
        switch (position)
        {
            case "0": _officialPosition = 0; break;
            //case "1": _officialPosition = "精英"; break;
            //case "2": _officialPosition = "副团长"; break;
            //case "3": _officialPosition = "团长"; break;
        }
        OneLegionMemberData.officialPosition = _officialPosition;
        positionLabel.text = string.Format("{0}", TextTranslator.instance.GetLegionPoisitionByPosId(OneLegionMemberData.officialPosition));//OneLegionMemberData.officialPosition);
    }
    public void SetLegionMemberItem(bool isAgreeOrRefuseState,LegionMemberData _OneLegionMemberData)
    {
        this.OneLegionMemberData = _OneLegionMemberData;
        icon.mainTexture = Resources.Load(string.Format("Head/{0}", _OneLegionMemberData.iconHead), typeof(Texture)) as Texture;
        nameLabel.text = _OneLegionMemberData.name;
        vipLabel.text = "VIP" + _OneLegionMemberData.vip.ToString();
        levelLabel.text = "Lv." + _OneLegionMemberData.level.ToString();
        //contributeLabel.text = _OneLegionMemberData.contribute.ToString();
        contributeLabel.gameObject.SetActive(false);
        positionLabel.gameObject.SetActive(false);
        todayLabel.gameObject.SetActive(false);
        agreeButton.SetActive(true);
        refuseButton.SetActive(true);
        //positionLabel.text = string.Format("{0}", _OneLegionMemberData.officialPosition);
        if (_OneLegionMemberData.lastLoginTime == 0)
        {
            //lastLoignLabel.text = "在线";
            lastLoignLabel.text = "不在线";
        }
        else
        {
            //lastLoignLabel.text = string.Format("{0}前", _OneLegionMemberData.lastLoginTime / 3600 > 0 ? _OneLegionMemberData.lastLoginTime / 3600 + "小时" : _OneLegionMemberData.lastLoginTime / 60 > 0 ? _OneLegionMemberData.lastLoginTime / 60 + "分钟" : _OneLegionMemberData.lastLoginTime + "秒");
            lastLoignLabel.text = "在线";
        }
        lastLoignLabel.transform.localPosition = new Vector3(25, 0, 0);

        if (UIEventListener.Get(agreeButton).onClick == null)
        {
            UIEventListener.Get(agreeButton).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess(string.Format("8013#{0};{1};{2};", this.OneLegionMemberData.uId, 1, CharacterRecorder.instance.legionID));
            };
        }
        if (UIEventListener.Get(refuseButton).onClick == null)
        {
            UIEventListener.Get(refuseButton).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess(string.Format("8013#{0};{1};{2};", this.OneLegionMemberData.uId, 2, CharacterRecorder.instance.legionID));
            };
        }
    }
}
