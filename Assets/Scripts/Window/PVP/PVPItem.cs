using UnityEngine;
using System.Collections;
public class PVPItemData
{
    public int playerID{ get; set; }
    public int roleID{ get; set; }
    public string name { get; set; }
    public int level { get; set; }
    public int rankNum { get; set; }
    public int powerNum { get; set; }
    public string LoginName { get; set; }
    public int CapturedNumber { get; set; }
    public PVPItemData(int _playerID, int _roleID, string _name, int _level, int _rankNum, int _powerNum, string _LoginName, int _CapturedNumber)
    {
        this.playerID = _playerID;
        this.roleID = _roleID;
        this.name = _name;
        this.level = _level;
        this.rankNum = _rankNum;
        this.powerNum = _powerNum;
        this.LoginName = _LoginName;
        this.CapturedNumber = _CapturedNumber;
    }
}
public class PVPItem : MonoBehaviour
{
    [SerializeField]
    private UISprite spriteIcon;
    [SerializeField]
    private UILabel labelName;
    [SerializeField]
    private UILabel labelRankNumber;
    [SerializeField]
    private UILabel labelPowerNumber;
    [SerializeField]
    private GameObject buttonFight;
    [SerializeField]
    private GameObject buttonFightHui;

    private PVPItemData mPVPItemData;
    void Start()
    {
        if (UIEventListener.Get(buttonFight).onClick == null)
        {
            UIEventListener.Get(buttonFight).onClick += delegate(GameObject go)
            {
                if (GameObject.Find("PVPWindow").GetComponent<PVPWindow>().leftTime <= 0 && GameObject.Find("PVPWindow").GetComponent<PVPWindow>().leftCount > 0)
                {
                    if (CharacterRecorder.instance.GuideID[21] == 8)
                    {
                        SceneTransformer.instance.TalkButtonClick(CharacterRecorder.instance.NowGuideID);
                    }
                    PictureCreater.instance.PVPRank = int.Parse(this.gameObject.name);
                    PictureCreater.instance.PVPName = transform.Find("Name").gameObject.GetComponent<UILabel>().text;
                    NetworkHandler.instance.SendProcess("6003#" + this.gameObject.name + ";");
                    CharacterRecorder.instance.PVPRankNumber = int.Parse(labelRankNumber.text);
                    CharacterRecorder.instance.PVPRoleID = int.Parse(this.gameObject.name);
                    CharacterRecorder.instance.PVPComeNum = 1;
                }
                else if (GameObject.Find("PVPWindow").GetComponent<PVPWindow>().leftTime > 0)
                {
                    UIManager.instance.OpenPromptWindow("还在CD时间", PromptWindow.PromptType.Hint, null, null);
                }
                else if (GameObject.Find("PVPWindow").GetComponent<PVPWindow>().leftCount == 0)
                {
                    //UIManager.instance.OpenPromptWindow("没有挑战次数", PromptWindow.PromptType.Hint, null, null);
                    NetworkHandler.instance.SendProcess("6013#;");
                }
            };
        }
        if (UIEventListener.Get(buttonFightHui).onClick == null)
        {
            UIEventListener.Get(buttonFightHui).onClick += delegate(GameObject go)
            {
                //UIManager.instance.OpenPromptWindow("您还没有资格挑战该战队，请提升排名到前20再来", PromptWindow.PromptType.Hint, null, null);
                NetworkHandler.instance.SendProcess("1141#" + this.gameObject.name + ";1;");
            };
        }
        UIEventListener.Get(this.gameObject).onClick = ClickThisItem;
    }

    public void SetPVPItemInfo(string PlayerID, int RoleID, string EnemyName, string EnemyLv, string EnemyRankNumber, string EnemyPowerNumber)
    {
        this.gameObject.name = PlayerID;
        labelName.text = EnemyName;
        labelRankNumber.text = EnemyRankNumber;
        labelPowerNumber.text = EnemyPowerNumber;
        spriteIcon.spriteName = RoleID.ToString();
    }
    public void SetPVPItemInfo(PVPItemData _oneData)
    {
        mPVPItemData = _oneData;
        this.gameObject.name = _oneData.playerID.ToString();
        labelName.text = _oneData.name;
        labelRankNumber.text = _oneData.rankNum.ToString();
       //labelPowerNumber.text = _oneData.powerNum.ToString();
       labelPowerNumber.text = CharacterRecorder.instance.ChangeNum(_oneData.powerNum);
        spriteIcon.spriteName = _oneData.roleID.ToString();
    }
    private void ClickThisItem(GameObject go)
    {
        if (go != null)
        {
            //UIManager.instance.OpenPanel("LookInfoWindow", false);
            //NetworkHandler.instance.SendProcess(string.Format("6008#{0};{1};", mPVPItemData.playerID, 3));
            //NetworkHandler.instance.SendProcess(string.Format("1020#{0};", mPVPItemData.playerID));
            NetworkHandler.instance.SendProcess("1020#" + mPVPItemData.playerID + ";");
            UIManager.instance.OpenPanel("LegionMemberItemDetail", false);
        }
    }
}
