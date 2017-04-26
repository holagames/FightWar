using UnityEngine;
using System.Collections;

public class EmploymentItem : MonoBehaviour
{
    public UISprite IconGrade;
    public UISprite Icon;
    public UILabel HeroName;
    public UILabel FightLabel;
    public UILabel CostLabel;
    public GameObject GetButton;
    public string HeroUid;
    public TeamInvitationWindow TeamInvitationWindowinfo;
    public GameObject EmploymentWindow;
    // Use this for initialization
    void Start()
    {
        UIEventListener.Get(GetButton).onClick += delegate(GameObject go)
        {
            TeamInvitationWindowinfo.isEmployment = true;          
            NetworkHandler.instance.SendProcess("6112#" + TeamInvitationWindowinfo.TeamID+";" + HeroUid + ";" + FightLabel.text + ";");
            TeamInvitationWindowinfo.EmploymentButton.transform.Find("Label").GetComponent<UILabel>().text = "取消";
            TeamInvitationWindowinfo.EmploymentButton.transform.Find("Message").GetComponent<UILabel>().text = "消耗" + FightLabel.text+"金币";
            EmploymentWindow.SetActive(false);
        };
    }
    public void SetInfo(string RankType, string _RankNumber, string _uId, string _Name, string _HeadIcon, string CurLevel, string Fight)
    {
        HeroUid = _uId;
        HeroName.text = _Name;
        FightLabel.text =(float.Parse(Fight)/6).ToString("f0");
        CostLabel.text = (float.Parse(Fight) / 6).ToString("f0");
        Icon.spriteName = _HeadIcon;
        IconGrade.spriteName = "yxdi" + (TextTranslator.instance.GetItemByItemCode(int.Parse(_HeadIcon)).itemGrade - 1).ToString();

    }

}
