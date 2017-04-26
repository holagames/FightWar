using UnityEngine;
using System.Collections;

public class LegionMemberItemDetail : MonoBehaviour
{
    //基本信息
    [SerializeField]
    private UITexture icon;
    [SerializeField]
    private UILabel vipLabel;
    public UILabel playerNameLabel;
    [SerializeField]
    private UILabel contributeLabel;
    [SerializeField]
    private UILabel lastLoignLabel;
    public UILabel levelLabel;
    public UILabel positionLabel;
    public UILabel fightLabel;
    public GameObject deleteFriendButton;
    public GameObject addFriendButton;
    public GameObject chatButton;
    public GameObject givePositionButton;
    public GameObject removeLegionButton;
    public GameObject upPositionButton;
    public GameObject downPositionButton;
    public GameObject closeButton;
    public GameObject sureButton;

    public GameObject YaoQingButton;
    public GameObject SiLiaoButton;

    public GameObject onlyForChairmanPart;
    public GameObject onlyForWithOthersPart;
    public GameObject uiGride;
    public GameObject roleItem;
    public GameObject MaskButton;

    private LegionMemberData OneLegionMemberData;
    // Use this for initialization
    void Start()
    {

        if (UIEventListener.Get(MaskButton).onClick == null)
        {
            UIEventListener.Get(MaskButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }
        if (UIEventListener.Get(deleteFriendButton).onClick == null)
        {
            UIEventListener.Get(deleteFriendButton).onClick += delegate(GameObject go)
            {
                Debug.Log("删除好友");
                NetworkHandler.instance.SendProcess("7109#" + TextTranslator.instance.targetPlayerInfo.uId + ";");
                UIManager.instance.BackUI();
            };
        }
        if (UIEventListener.Get(addFriendButton).onClick == null)
        {
            UIEventListener.Get(addFriendButton).onClick += delegate(GameObject go)
            {
                Debug.Log("加好友");
                NetworkHandler.instance.SendProcess("7104#" + OneLegionMemberData.uId + ";");
            };
        }
        if (UIEventListener.Get(chatButton).onClick == null)
        {
            UIEventListener.Get(chatButton).onClick += delegate(GameObject go)
            {
                Debug.Log("私聊");
            };
        }
        if (UIEventListener.Get(givePositionButton).onClick == null)
        {
            UIEventListener.Get(givePositionButton).onClick += delegate(GameObject go)
            {
                Debug.Log("传职");
                if (CharacterRecorder.instance.myLegionPosition == 2)
                {
                    //副军团长
                    UIManager.instance.OpenPromptWindow("副军团长不能传职。", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                UIManager.instance.OpenPromptWindowNoTitle(string.Format("确定将团长之位传给{0}？传职后你将不再是军团长。", this.OneLegionMemberData.name), PromptWindow.PromptType.Confirm, ConfirmGivePosition, null);
            };
        }
        if (UIEventListener.Get(upPositionButton).onClick == null)
        {
            UIEventListener.Get(upPositionButton).onClick += delegate(GameObject go)
            {
                Debug.Log("传职");

                if (OneLegionMemberData.officialPosition >= 2)
                {
                    UIManager.instance.OpenPromptWindow("职位已经达到最高啦", PromptWindow.PromptType.Hint, null, null);
                    return;
                }//离开军团
                string willBePosDes = TextTranslator.instance.GetLegionPoisitionByPosId(this.OneLegionMemberData.officialPosition + 1);
                UIManager.instance.OpenPromptWindowNoTitle(string.Format("确定将{0}升为 {1}？", this.OneLegionMemberData.name, willBePosDes), PromptWindow.PromptType.Confirm, ConfirmUpPosition, null);
            };
        }
        if (UIEventListener.Get(downPositionButton).onClick == null)
        {
            UIEventListener.Get(downPositionButton).onClick += delegate(GameObject go)
            {
                Debug.Log("降职");
                if (OneLegionMemberData.officialPosition <= 0)
                {
                    UIManager.instance.OpenPromptWindow("职位已经最低啦", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                if (OneLegionMemberData.officialPosition >= CharacterRecorder.instance.myLegionPosition)
                {
                    UIManager.instance.OpenPromptWindow("权限不足", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                string willBePosDes = TextTranslator.instance.GetLegionPoisitionByPosId(this.OneLegionMemberData.officialPosition - 1);
                UIManager.instance.OpenPromptWindowNoTitle(string.Format("确定将{0}降为 {1}？", this.OneLegionMemberData.name, willBePosDes), PromptWindow.PromptType.Confirm, ConfirmDownPosition, null);
            };
        }
        if (UIEventListener.Get(removeLegionButton).onClick == null)
        {
            UIEventListener.Get(removeLegionButton).onClick += delegate(GameObject go)
            {
                Debug.Log("踢出军团");
                if (this.OneLegionMemberData.officialPosition == 3)
                {
                    UIManager.instance.OpenPromptWindow("不能将团长踢除军团。", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                UIManager.instance.OpenPromptWindowNoTitle(string.Format("确定让{0}离开军团？", this.OneLegionMemberData.name), PromptWindow.PromptType.Confirm, ConfirmRemove, null);
            };
        }
        if (UIEventListener.Get(closeButton).onClick == null)
        {
            UIEventListener.Get(closeButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }
        //if (UIEventListener.Get(sureButton).onClick == null)
        //{
        //    UIEventListener.Get(sureButton).onClick += delegate(GameObject go)
        //    {
        //        UIManager.instance.BackUI();
        //    };
        //}
        if (UIEventListener.Get(SiLiaoButton).onClick == null)
        {
            UIEventListener.Get(SiLiaoButton).onClick += delegate(GameObject go)
            {
                //UIManager.instance.OpenPromptWindow("暂未开放", PromptWindow.PromptType.Hint, null, null);
                Debug.Log("私聊");
                if (TextTranslator.instance.targetPlayerInfo.uId != CharacterRecorder.instance.userId)
                {
                    UIManager.instance.OpenPanel("PrivateChatWindow", false);
                    GameObject.Find("PrivateChatWindow").GetComponent<PrivateChatWindow>().AddOneCharacterInfo();
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("您无法和自己私聊", PromptWindow.PromptType.Hint, null, null);
                }
                //NetworkHandler.instance.SendProcess("7104#" + TextTranslator.instance.targetPlayerInfo.uId + ";");
            };
        }
        if (UIEventListener.Get(YaoQingButton).onClick == null)
        {
            UIEventListener.Get(YaoQingButton).onClick += delegate(GameObject go)
            {
                Debug.Log("加好友");
                NetworkHandler.instance.SendProcess("7104#" + TextTranslator.instance.targetPlayerInfo.uId + ";");
            };
        }

        /*  givePositionButton.SetActive(false); //kino temp
          removeLegionButton.SetActive(false); //kino temp
          upPositionButton.SetActive(false); //kino temp
          chatButton.SetActive(false); //kino temp */
    }

    public void SetLegionMemberItemDetail(LegionMemberData _OneLegionMemberData)
    {
        this.OneLegionMemberData = _OneLegionMemberData;

        //icon.mainTexture = Resources.Load(string.Format("Head/{0}", _OneLegionMemberData.iconHead), typeof(Texture)) as Texture;
        playerNameLabel.text = _OneLegionMemberData.name;
        vipLabel.text = "VIP" + _OneLegionMemberData.vip.ToString();
        contributeLabel.text = string.Format("军团贡献:{0}", _OneLegionMemberData.contribute); //_OneLegionMemberData.contribute.ToString();
        //fightLabel.text = string.Format("战斗力:{0}", _OneLegionMemberData.fight);
        fightLabel.text = _OneLegionMemberData.fight.ToString();
        positionLabel.text = string.Format("{0}", TextTranslator.instance.GetLegionPoisitionByPosId(_OneLegionMemberData.officialPosition));//_OneLegionMemberData.officialPosition);
        levelLabel.text = string.Format("Lv.{0}", _OneLegionMemberData.level);
        if (_OneLegionMemberData.lastLoginTime == 0)
        {
            //lastLoignLabel.text = "在线";
            lastLoignLabel.text = "不在线";
        }
        else
        {//LegionMemberItem
            //lastLoignLabel.text = string.Format("最近登陆:{0}前", _OneLegionMemberData.lastLoginTime / 3600 > 0 ? _OneLegionMemberData.lastLoginTime / 3600 + "小时" : _OneLegionMemberData.lastLoginTime / 60 + "分钟");
            //lastLoignLabel.text = string.Format("最近登陆:{0}前", _OneLegionMemberData.lastLoginTime / 3600 > 0 ? _OneLegionMemberData.lastLoginTime / 3600 + "小时" : _OneLegionMemberData.lastLoginTime / 60 > 0 ? _OneLegionMemberData.lastLoginTime / 60 + "分钟" : _OneLegionMemberData.lastLoginTime + "秒");
            lastLoignLabel.text = "在线";
        }

        if (CharacterRecorder.instance.MyFriendUIDList.Contains(_OneLegionMemberData.uId))
        {
            addFriendButton.GetComponent<UIButton>().isEnabled = false;
            addFriendButton.transform.FindChild("Label").GetComponent<UILabel>().text = "已是好友";
        }
        //if (_OneLegionMemberData.name == CharacterRecorder.instance.characterName)// 是团长是自己 或 不是团长是自己
        if (_OneLegionMemberData.name == CharacterRecorder.instance.characterName)//我是团长是自己 或 不是团长是自己
        {
            onlyForChairmanPart.SetActive(false);
            //sureButton.SetActive(true);
            YaoQingButton.SetActive(true);
            SiLiaoButton.SetActive(true);
            //onlyForWithOthersPart.SetActive(false);
        }
        else if (CharacterRecorder.instance.myLegionPosition >= 2)//是团长不是自己
        {
            onlyForChairmanPart.SetActive(true);
            //sureButton.SetActive(false);
            YaoQingButton.SetActive(false);
            SiLiaoButton.SetActive(false);
            //onlyForWithOthersPart.SetActive(true);
        }
        else
        {
            onlyForChairmanPart.SetActive(false);
            //sureButton.SetActive(true);
            YaoQingButton.SetActive(true);
            SiLiaoButton.SetActive(true);
            //onlyForWithOthersPart.SetActive(true);
        }


    }
    public void SetRoleInfoOfPlayer()
    {
        DestroyGride();
        icon.mainTexture = Resources.Load(string.Format("Head/{0}", TextTranslator.instance.targetPlayerInfo.headIcon), typeof(Texture)) as Texture;
        for (int i = 0; i < TextTranslator.instance.targetPlayerInfo.roleList.size; i++)
        {
            GameObject obj = NGUITools.AddChild(uiGride, roleItem);
            obj.transform.localScale = new Vector3(1.1f, 1.1f, 1.1f);
            obj.GetComponent<ItemIconWithName>().SetItemIconWithName(TextTranslator.instance.targetPlayerInfo.roleList[i]);

            obj.name = TextTranslator.instance.targetPlayerInfo.roleList[i].characterId.ToString();

            if (UIEventListener.Get(obj).onClick == null)
            {
                UIEventListener.Get(obj).onClick += delegate(GameObject go)
                {
                    CharacterRecorder.instance.AboutHeroInfoId = int.Parse(go.name);
                    UIManager.instance.OpenSinglePanel("AboutHeroInfoWindow", false);
                };
            }

        }
        uiGride.GetComponent<UIGrid>().repositionNow = true;
        if (this.OneLegionMemberData != null)
        {
            return;
        }
        playerNameLabel.text = TextTranslator.instance.targetPlayerInfo.name;
        vipLabel.text = "VIP" + TextTranslator.instance.targetPlayerInfo.vip.ToString();
        if (TextTranslator.instance.targetPlayerInfo.legionName == "")
        {
            //contributeLabel.text = string.Format("军团贡献:{0}", TextTranslator.instance.targetPlayerInfo.contribute); //_OneLegionMemberData.contribute.ToString();
            //positionLabel.text = string.Format("{0}", TextTranslator.instance.targetPlayerInfo.legionPosition);
            contributeLabel.text = string.Format("军团:{0}", "无");
            positionLabel.text = "";
        }
        else
        {
            contributeLabel.text = string.Format("军团:{0}", TextTranslator.instance.targetPlayerInfo.legionName);
            string _officialPosition = "";
            switch (TextTranslator.instance.targetPlayerInfo.legionPosition)
            {
                case 0: _officialPosition = "士兵"; break;
                case 1: _officialPosition = "精英"; break;
                case 2: _officialPosition = "副团长"; break;
                case 3: _officialPosition = "团长"; break;
            }
            positionLabel.text = string.Format("{0}", _officialPosition);
        }
        //fightLabel.text = string.Format("战斗力:{0}", TextTranslator.instance.targetPlayerInfo.fight);
        fightLabel.text = TextTranslator.instance.targetPlayerInfo.fight.ToString();

        levelLabel.text = string.Format("Lv.{0}", TextTranslator.instance.targetPlayerInfo.level);
        lastLoignLabel.text = "";
        /* if (_OneLegionMemberData.lastLoginTime == 0)
         {
             lastLoignLabel.text = "在线";
         }
         else
         {
             //lastLoignLabel.text = string.Format("最近登陆:{0}前", _OneLegionMemberData.lastLoginTime / 3600 > 0 ? _OneLegionMemberData.lastLoginTime / 3600 + "小时" : _OneLegionMemberData.lastLoginTime / 60 + "分钟");
             lastLoignLabel.text = string.Format("最近登陆:{0}前", _OneLegionMemberData.lastLoginTime / 3600 > 0 ? _OneLegionMemberData.lastLoginTime / 3600 + "小时" : _OneLegionMemberData.lastLoginTime / 60 > 0 ? _OneLegionMemberData.lastLoginTime / 60 + "分钟" : _OneLegionMemberData.lastLoginTime + "秒");
         }*/
        onlyForChairmanPart.SetActive(false);
        YaoQingButton.SetActive(true);
        SiLiaoButton.SetActive(true);
        if (CharacterRecorder.instance.MyFriendUIDList.Contains(TextTranslator.instance.targetPlayerInfo.uId))
        {
            addFriendButton.GetComponent<UIButton>().isEnabled = false;
            addFriendButton.transform.FindChild("Label").GetComponent<UILabel>().text = "已是好友";
            if (GameObject.Find("FriendWindow") != null)
            {
                deleteFriendButton.SetActive(true);
                YaoQingButton.SetActive(false);
                SiLiaoButton.SetActive(true);
            }
            else
            {
                deleteFriendButton.SetActive(false);
            }
        }
        //sureButton.SetActive(true);
    }
    void DestroyGride()
    {
        for (int i = uiGride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGride.transform.GetChild(i).gameObject);
        }
    }

    void ConfirmRemove()
    {
        NetworkHandler.instance.SendProcess("8009#" + OneLegionMemberData.uId + ";");
        UIManager.instance.BackUI();
    }
    void ConfirmGivePosition()
    {
        NetworkHandler.instance.SendProcess(string.Format("8003#{0};{1};", OneLegionMemberData.uId, 3));
        UIManager.instance.BackUI();
    }
    void ConfirmUpPosition()
    {
        NetworkHandler.instance.SendProcess(string.Format("8003#{0};{1};", OneLegionMemberData.uId, (OneLegionMemberData.officialPosition + 1).ToString()));
        UIManager.instance.BackUI();
    }
    void ConfirmDownPosition()
    {
        NetworkHandler.instance.SendProcess(string.Format("8003#{0};{1};", OneLegionMemberData.uId, (OneLegionMemberData.officialPosition - 1).ToString()));
        UIManager.instance.BackUI();
    }
}
