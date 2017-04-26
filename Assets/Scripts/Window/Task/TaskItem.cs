using UnityEngine;
using System.Collections;

public class TaskItem : MonoBehaviour
{

    public UILabel Name;
    public UILabel Des;
    public UILabel CompleteCount;
    public UILabel HappyBox;

    public GameObject ItemIcon;
    public GameObject[] AwardList;
    public GameObject GetButton;
    public GameObject SpriteGuangbiao;
    public GameObject TaskWindowObj;
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;
    public UIAtlas CommonAtlas;
    public UIAtlas NormalAtlas;

    #region 旧的脚本，暂不用
    // Use this for initialization
    /*
        public void SetInfo(Achievement mAchievement, int State,int SetNum,int Type)//state 0:未完成  1：可领取  2：已领取,type 0-任务1，-成就：
        {
            switch (State)
            {
                case 0:
                    CompleteCount.text = string.Format("{0}/{1}", mAchievement.completeCount, mAchievement.totalCount);
                    if (mAchievement.achievementName != "钻石返利" && mAchievement.achievementName != "超级钻石返利" && mAchievement.achievementName != "精力充沛" && mAchievement.achievementName != "情报力量" && mAchievement.achievementName != "能不能万能")
                    {
                        GetButton.SetActive(true);
                        UIEventListener.Get(GetButton).onClick += delegate(GameObject go)
                        {
                            //Debug.LogError("前往");
                            GoAchievement(mAchievement);
                        };
                    }
                    //UIEventListener.Get(GetButton).onClick = delegate(GameObject go)
                    //{
                    //    //Debug.LogError("前往");
                    //    GoAchievement(mAchievement);
                    //};
                    break;
                case 1:
                    //CompleteCount.text = string.Format("{0}/{1}", mAchievement.totalCount, mAchievement.totalCount);
                    GameObject.Find("LabelCount").SetActive(false);
                    this.gameObject.GetComponent<UISprite>().atlas = NormalAtlas;
                    this.gameObject.GetComponent<UISprite>().spriteName = "yuandi30";
                    SpriteGuangbiao.SetActive(true);
                    UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
                    {
                        NetworkHandler.instance.SendProcess("1202#" + ((mAchievement.resetTime == 1) ? "1" : "2") + ";" + this.gameObject.name + ";");
                        GameObject.Find("TaskWindow").GetComponent<TaskWindow>().DestyItem(this.gameObject);                   
                    };
                    break;
                case 2:
                    CompleteCount.text = string.Format("{0}/{1}", mAchievement.totalCount, mAchievement.totalCount);
                    GetButton.SetActive(false);
                    break;
                default:
                    break;
            }
            if (Type == 0) 
            {
                HappyBox.gameObject.SetActive(true);
                HappyBox.text = "活跃度积分:" + mAchievement.HappyPoint.ToString();
            }
            //if (mAchievement.achievementName == "钻石返利" && State != 1)
            //{
            //    //GetButton.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
            //}
            //else if (mAchievement.achievementName == "钻石返利" && State == 1) 
            //{
            //    GetButton.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
            //}

            Name.text = mAchievement.achievementName;
            Des.text = mAchievement.achievementDetail;
            if (SetNum == 1)
            {
                ItemIcon.transform.GetComponent<UISprite>().spriteName = "Grade5";
            }
            else
            {
                ItemIcon.transform.GetComponent<UISprite>().spriteName = "Grade"+TextTranslator.instance.GetItemByItemCode(mAchievement.picCode).itemGrade.ToString();
            }
            ItemIcon.transform.FindChild("SpriteAvatar").GetComponent<UISprite>().spriteName = mAchievement.picCode.ToString();

            for (int i = 0; i < AwardList.Length; i++)
            {
                AwardList[i].SetActive(false);
            }

            int j = 0;
            if (mAchievement.bonusGold > 0)
            {
                AwardList[j].SetActive(true);
                AwardList[j].GetComponent<UISprite>().atlas = ItemAtlas;
                AwardList[j].GetComponent<UISprite>().spriteName = "90001";
                AwardList[j].transform.FindChild("SuiPian").gameObject.SetActive(false);
                AwardList[j].transform.FindChild("LabelAward").GetComponent<UILabel>().text = "x" + mAchievement.bonusGold.ToString();
                j++;
            }
            if (mAchievement.bonusDiamond > 0)
            {
                AwardList[j].SetActive(true);
                AwardList[j].GetComponent<UISprite>().atlas = ItemAtlas;
                AwardList[j].GetComponent<UISprite>().spriteName = "90002";
                AwardList[j].transform.FindChild("SuiPian").gameObject.SetActive(false);
                AwardList[j].transform.FindChild("LabelAward").GetComponent<UILabel>().text = "x" + mAchievement.bonusDiamond.ToString();
                j++;
            }
            if (mAchievement.bonusExp > 0)
            {
                AwardList[j].SetActive(true);
                AwardList[j].GetComponent<UISprite>().atlas = ItemAtlas;
                AwardList[j].GetComponent<UISprite>().spriteName = "90009";
                AwardList[j].transform.FindChild("SuiPian").gameObject.SetActive(false);
                AwardList[j].transform.FindChild("LabelAward").GetComponent<UILabel>().text = "x" + mAchievement.bonusExp.ToString();
                j++;
            }

            foreach (var item in mAchievement.bonusOther)
            {
                AwardList[j].SetActive(true);
                if (item.itemCode.ToString()[0] == '3')
                {
                    AwardList[j].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    AwardList[j].GetComponent<UISprite>().atlas = ItemAtlas;
                    AwardList[j].GetComponent<UISprite>().spriteName = item.itemCode.ToString();
                    AwardList[j].transform.FindChild("SuiPian").gameObject.SetActive(false);
                    //SetColor(AwardList[j], item.itemGrade);
                }
                else if (item.itemCode.ToString()[0] == '4')
                {
                    AwardList[j].GetComponent<UISprite>().atlas = ItemAtlas;
                    AwardList[j].GetComponent<UISprite>().spriteName = (item.itemCode - 10000).ToString();
                    AwardList[j].transform.FindChild("SuiPian").gameObject.SetActive(true);
                    //SetColor(AwardList[j], item.itemGrade);
                }
                else if (item.itemCode.ToString()[0] == '5')
                {
                    AwardList[j].GetComponent<UISprite>().atlas = ItemAtlas;
                    AwardList[j].GetComponent<UISprite>().spriteName = item.itemCode.ToString();
                    AwardList[j].transform.FindChild("SuiPian").gameObject.SetActive(false);
                    //SetColor(AwardList[j], item.itemGrade);
                }
                else if (item.itemCode.ToString()[0] == '6')
                {
                    AwardList[j].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    AwardList[j].GetComponent<UISprite>().atlas = RoleAtlas;
                    AwardList[j].GetComponent<UISprite>().spriteName = item.itemCode.ToString();
                    AwardList[j].transform.FindChild("SuiPian").gameObject.SetActive(false);
                    //SetColor(AwardList[j], item.itemGrade);
                }
                else if (item.itemCode.ToString()[0] == '7')
                {
                    AwardList[j].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                    AwardList[j].GetComponent<UISprite>().atlas = RoleAtlas;
                    AwardList[j].GetComponent<UISprite>().spriteName = (item.itemCode - 10000).ToString();
                    AwardList[j].transform.FindChild("SuiPian").gameObject.SetActive(true);
                    //SetColor(AwardList[j], item.itemGrade);
                }
                else if (item.itemCode.ToString()[0] == '8')
                {
                    AwardList[j].GetComponent<UISprite>().atlas = ItemAtlas;
                    AwardList[j].GetComponent<UISprite>().spriteName = (item.itemCode / 10 * 10 - 30000).ToString();//(item.itemCode - 30000).ToString();
                    AwardList[j].transform.FindChild("SuiPian").gameObject.SetActive(true);
                    //SetColor(AwardList[j], item.itemGrade);
                }
                else 
                {
                    AwardList[j].GetComponent<UISprite>().atlas = ItemAtlas;
                    AwardList[j].GetComponent<UISprite>().spriteName = item.itemCode.ToString();
                    AwardList[j].transform.FindChild("SuiPian").gameObject.SetActive(false);
                    //SetColor(AwardList[j], item.itemGrade);
                }
                //AwardList[j].GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(item.itemCode).itemGrade.ToString();
                AwardList[j].transform.FindChild("LabelAward").GetComponent<UILabel>().text = "x" + item.itemCount.ToString();
                j++;
            }

            if (mAchievement.achievementName == "豪华午餐" || mAchievement.achievementName == "豪华晚餐") 
            {
                AwardList[0].SetActive(true);
                AwardList[0].GetComponent<UISprite>().atlas = ItemAtlas;
                AwardList[0].GetComponent<UISprite>().spriteName = "90007";
                AwardList[0].transform.FindChild("SuiPian").gameObject.SetActive(false);
                AwardList[0].transform.FindChild("LabelAward").GetComponent<UILabel>().text = "x60";
            }
            else if (mAchievement.achievementName == "能不能万能") 
            {
                AwardList[0].SetActive(true);
                AwardList[0].GetComponent<UISprite>().atlas = ItemAtlas;
                AwardList[0].GetComponent<UISprite>().spriteName = "70000";
                AwardList[0].transform.FindChild("SuiPian").gameObject.SetActive(false);
            }
        }

        void SetColor(GameObject go, int color)
        {
            switch (color)
            {
                case 1:
                    go.GetComponent<UISprite>().spriteName = "Grade2";
                    break;
                case 2:
                    go.GetComponent<UISprite>().spriteName = "Grade3";
                    break;
                case 3:
                    go.GetComponent<UISprite>().spriteName = "Grade4";
                    break;
                case 4:
                    go.GetComponent<UISprite>().spriteName = "Grade5";
                    break;
                //case 5:
                //    go.GetComponent<UISprite>().spriteName = "Grade5";
                //    break;
            }
        }
        void GoAchievement(Achievement mAchievement) 
        {
            string AchName = mAchievement.achievementName;
            switch (AchName)
            {
                case "好战分子": 
                    UIManager.instance.OpenPanel("MapUiWindow", true);//最新普通关卡
                    //UIManager.instance.OpenSinglePanel("MapUiWindow", true);
                    break;
                case "超级好战分子":
                    UIManager.instance.OpenPanel("MapUiWindow", true);//最新精英关卡
                    break;
                case "竞技爱好者":
                    UIManager.instance.OpenPanel("PVPWindow", true);
                    break;
                case "试炼达人":
                    //UIManager.instance.OpenPanel("WoodsTheExpendables", true);
                    NetworkHandler.instance.SendProcess("1501#;");
                    break;
                case "赚钱小能手":
                    UIManager.instance.OpenPanel("EverydayWindow", true);
                    break;
                case "更强的英雄":
                    UIManager.instance.OpenPanel("RoleWindow", true);//英雄升级界面
                    GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(2);
                    break;
                case "每天进步一点点":
                    UIManager.instance.OpenPanel("RoleWindow", true);//升品界面
                    GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(4);
                    break;
                case "钻石返利":
                    break;
                case "招募英雄":
                    UIManager.instance.OpenPanel("GachaWindow", true);//抽卡界面
                    break;
                case "高价回收钻石": 
                    UIManager.instance.OpenPanel("BuyGlodWindow", false);//弹出点金
                    break;
                case "补充体力": 
                    UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
                    NetworkHandler.instance.SendProcess("5012#10602;");//弹出购买体力
                    break;
                case "军团贡献":
                    //UIManager.instance.OpenPromptWindow("暂未开放！", PromptWindow.PromptType.Hint, null, null);
                    if (CharacterRecorder.instance.legionID == 0)
                    {
                        UIManager.instance.OpenPromptWindow("暂未加入任何军团，无法捐献！", PromptWindow.PromptType.Hint, null, null);
                    }
                    else 
                    {
                        UIManager.instance.OpenPanel("LegionMainHaveWindow", true);
                        UIManager.instance.OpenPanel("LegionContributeWindow", false);
                    }
                    break;
                case "每天再进步一点点":
                    UIManager.instance.OpenPanel("RoleWindow", true);//培养界面
                    GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(3);
                    break;
                case "占领资源": 
                    UIManager.instance.OpenPanel("MapUiWindow", true);//资源点占领
                    break;
                case "世界事件":
                    if (CharacterRecorder.instance.level < 5 || CharacterRecorder.instance.lastGateID < 10034)
                    {
                        UIManager.instance.OpenPromptWindow("长官，通关33关开放该功能！", PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        UIManager.instance.OpenPanel("MapUiWindow", true);//世界事件
                        GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().TheWorldBtnClick();
                    }
                    //UIManager.instance.OpenPanel("MapUiWindow", true);//世界事件
        
                    break;
                case "合成大师":
                    UIManager.instance.OpenPanel("GrabItemWindow", true);
                    break;
                case "战场争锋":

                    if (mAchievement.totalCount + 10000 <= CharacterRecorder.instance.lastGateID)
                    {
                        Debug.LogError(mAchievement.totalCount + 10000);
                        CharacterRecorder.instance.gotoGateID = mAchievement.totalCount + 10000;
                        UIManager.instance.OpenPanel("MapUiWindow", true);
                        //CharacterRecorder.instance.gotoGateID = mAchievement.totalCount+10000;
                    }
                    else 
                    {
                        UIManager.instance.OpenPromptWindow("关卡未开放", PromptWindow.PromptType.Hint, null, null);
                    }

                    //UIManager.instance.OpenPanel("MapUiWindow", true);//最新可挑战关卡
                    break;
                case "成长回报":
                    UIManager.instance.OpenPanel("MapUiWindow", true);//最新可挑战关卡
                    break;
                case "招募达人":
                    UIManager.instance.OpenPanel("GachaWindow", true);
                    break;
                case "万人敬仰":
                    UIManager.instance.OpenPanel("RoleWindow", true);//战斗力，英雄界面
                    break;
                case "星光闪烁":
                    UIManager.instance.OpenPanel("MapUiWindow", true);//最新可挑战关卡
                    break;
                case "兢兢业业":
                    UIManager.instance.OpenPanel("MapUiWindow", true);//前往资源占领
                    break;
                case "精益求精":
                    UIManager.instance.OpenPanel("RoleWindow", true);//前往升品界面
                    GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(4);
                    break;
                case "月卡":
                    //UIManager.instance.OpenPanel("EventWindow", true);
                    UIManager.instance.OpenSinglePanel("VIPShopWindow", false);
                    break;
                case "至尊月卡":
                    //UIManager.instance.OpenPanel("VIPShopWindow", true);
                    UIManager.instance.OpenSinglePanel("VIPShopWindow", false);
                    break;
                case "精锐部队":
                    if (CharacterRecorder.instance.lastGateID > 10011) 
                    {
                        if (mAchievement.totalCount <= CharacterRecorder.instance.lastCreamGateID)
                        {
                            UIManager.instance.OpenPanel("MapUiWindow", true);
                            CharacterRecorder.instance.gotoGateID = mAchievement.totalCount;
                            CharacterRecorder.instance.IsOpenCreamCN = true;
                        }
                        else 
                        {
                            UIManager.instance.OpenPromptWindow("关卡未开放", PromptWindow.PromptType.Hint, null, null);
                        }
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("精英关卡未开放", PromptWindow.PromptType.Hint, null, null);
                    }
                    break;
                default:
                    Debug.Log("aa");
                    break;
            }
        }
        */

    #endregion
    public void SetInfo(Achievement mAchievement, int State, int SetNum, int Type)//state 0:未完成  1：可领取  2：已领取,type 0-任务1，-成就：
    {
        switch (State)
        {
            case 0:
                CompleteCount.text = string.Format("{0}/{1}", mAchievement.completeCount, mAchievement.totalCount);
                if (mAchievement.Type != 29 && mAchievement.Type != 39)
                {
                    GetButton.SetActive(true);
                    UIEventListener.Get(GetButton).onClick += delegate(GameObject go)
                    {
                        //Debug.LogError("前往");
                        GoAchievement(mAchievement);
                    };
                }
                break;
            case 1:
                GameObject.Find("LabelCount").SetActive(false);
                this.gameObject.GetComponent<UISprite>().atlas = NormalAtlas;
                this.gameObject.GetComponent<UISprite>().spriteName = "yuandi30";
                SpriteGuangbiao.SetActive(true);
                UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
                {
                    if (CharacterRecorder.instance.GuideID[20] == 5)
                    {
                        SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                    }
                    NetworkHandler.instance.SendProcess("1202#" + ((mAchievement.resetTime == 1) ? "1" : "2") + ";" + this.gameObject.name + ";");
                    GameObject.Find("TaskWindow").GetComponent<TaskWindow>().DestyItem(this.gameObject);
                };
                break;
            case 2:
                CompleteCount.text = string.Format("{0}/{1}", mAchievement.totalCount, mAchievement.totalCount);
                GetButton.SetActive(false);
                break;
            default:
                break;
        }
        if (Type == 0)
        {
            HappyBox.gameObject.SetActive(true);
            HappyBox.text = "活跃度积分:" + mAchievement.HappyPoint.ToString();
        }

        Name.text = mAchievement.achievementName;
        Des.text = mAchievement.achievementDetail;
        if (SetNum == 1)
        {
            ItemIcon.transform.GetComponent<UISprite>().spriteName = "Grade5";
        }
        else
        {
            ItemIcon.transform.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(mAchievement.picCode).itemGrade.ToString();
        }
        ItemIcon.transform.FindChild("SpriteAvatar").GetComponent<UISprite>().spriteName = mAchievement.picCode.ToString();

        for (int i = 0; i < AwardList.Length; i++)
        {
            AwardList[i].SetActive(false);
        }

        int j = 0;
        if (mAchievement.bonusGold > 0)
        {
            AwardList[j].SetActive(true);
            AwardList[j].GetComponent<UISprite>().atlas = ItemAtlas;
            AwardList[j].GetComponent<UISprite>().spriteName = "90001";
            AwardList[j].transform.FindChild("SuiPian").gameObject.SetActive(false);
            AwardList[j].transform.FindChild("LabelAward").GetComponent<UILabel>().text = "x" + mAchievement.bonusGold.ToString();
            j++;
        }
        if (mAchievement.bonusDiamond > 0)
        {
            AwardList[j].SetActive(true);
            AwardList[j].GetComponent<UISprite>().atlas = ItemAtlas;
            AwardList[j].GetComponent<UISprite>().spriteName = "90002";
            AwardList[j].transform.FindChild("SuiPian").gameObject.SetActive(false);
            AwardList[j].transform.FindChild("LabelAward").GetComponent<UILabel>().text = "x" + mAchievement.bonusDiamond.ToString();
            j++;
        }
        if (mAchievement.bonusExp > 0)
        {
            AwardList[j].SetActive(true);
            AwardList[j].GetComponent<UISprite>().atlas = ItemAtlas;
            AwardList[j].GetComponent<UISprite>().spriteName = "90009";
            AwardList[j].transform.FindChild("SuiPian").gameObject.SetActive(false);
            AwardList[j].transform.FindChild("LabelAward").GetComponent<UILabel>().text = "x" + mAchievement.bonusExp.ToString();
            j++;
        }

        foreach (var item in mAchievement.bonusOther)
        {
            AwardList[j].SetActive(true);
            if (item.itemCode.ToString()[0] == '3')
            {
                AwardList[j].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                AwardList[j].GetComponent<UISprite>().atlas = ItemAtlas;
                AwardList[j].GetComponent<UISprite>().spriteName = item.itemCode.ToString();
                AwardList[j].transform.FindChild("SuiPian").gameObject.SetActive(false);
                //SetColor(AwardList[j], item.itemGrade);
            }
            else if (item.itemCode.ToString()[0] == '4')
            {
                AwardList[j].GetComponent<UISprite>().atlas = ItemAtlas;
                AwardList[j].GetComponent<UISprite>().spriteName = item.itemCode.ToString();
                AwardList[j].transform.FindChild("SuiPian").gameObject.SetActive(false);
                //SetColor(AwardList[j], item.itemGrade);
            }
            else if (item.itemCode.ToString()[0] == '5')
            {
                AwardList[j].GetComponent<UISprite>().atlas = ItemAtlas;
                AwardList[j].GetComponent<UISprite>().spriteName = item.itemCode.ToString();
                AwardList[j].transform.FindChild("SuiPian").gameObject.SetActive(false);
                //SetColor(AwardList[j], item.itemGrade);
            }
            else if (item.itemCode.ToString()[0] == '6')
            {
                AwardList[j].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                AwardList[j].GetComponent<UISprite>().atlas = RoleAtlas;
                AwardList[j].GetComponent<UISprite>().spriteName = item.itemCode.ToString();
                AwardList[j].transform.FindChild("SuiPian").gameObject.SetActive(false);
                //SetColor(AwardList[j], item.itemGrade);
            }
            else if (item.itemCode == 70000 || item.itemCode == 79999)
            {
                AwardList[j].GetComponent<UISprite>().atlas = ItemAtlas;
                AwardList[j].GetComponent<UISprite>().spriteName = item.itemCode.ToString();//(item.itemCode - 30000).ToString();
                AwardList[j].transform.FindChild("SuiPian").gameObject.SetActive(true);
            }
            else if (item.itemCode.ToString()[0] == '7')
            {
                AwardList[j].transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
                AwardList[j].GetComponent<UISprite>().atlas = RoleAtlas;
                AwardList[j].GetComponent<UISprite>().spriteName = (item.itemCode - 10000).ToString();
                AwardList[j].transform.FindChild("SuiPian").gameObject.SetActive(true);
                //SetColor(AwardList[j], item.itemGrade);
            }
            else if (item.itemCode.ToString()[0] == '8')
            {
                AwardList[j].GetComponent<UISprite>().atlas = ItemAtlas;
                AwardList[j].GetComponent<UISprite>().spriteName = (item.itemCode / 10 * 10 - 30000).ToString();//(item.itemCode - 30000).ToString();
                AwardList[j].transform.FindChild("SuiPian").gameObject.SetActive(true);
                //SetColor(AwardList[j], item.itemGrade);
            }
            else
            {
                AwardList[j].GetComponent<UISprite>().atlas = ItemAtlas;
                AwardList[j].GetComponent<UISprite>().spriteName = item.itemCode.ToString();
                AwardList[j].transform.FindChild("SuiPian").gameObject.SetActive(false);
                //SetColor(AwardList[j], item.itemGrade);
            }
            //AwardList[j].GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(item.itemCode).itemGrade.ToString();
            AwardList[j].transform.FindChild("LabelAward").GetComponent<UILabel>().text = "x" + item.itemCount.ToString();
            j++;
        }

        if (mAchievement.Type == 38)
        {
            AwardList[0].SetActive(true);
            AwardList[0].GetComponent<UISprite>().atlas = ItemAtlas;
            AwardList[0].GetComponent<UISprite>().spriteName = "90007";
            AwardList[0].transform.FindChild("SuiPian").gameObject.SetActive(false);
            AwardList[0].transform.FindChild("LabelAward").GetComponent<UILabel>().text = "x60";
        }
        //else if (mAchievement.achievementName == "能不能万能")
        //{
        //    AwardList[0].SetActive(true);
        //    AwardList[0].GetComponent<UISprite>().atlas = ItemAtlas;
        //    AwardList[0].GetComponent<UISprite>().spriteName = "79999";
        //    //AwardList[0].transform.FindChild("SuiPian").gameObject.SetActive(false);
        //}
    }

    void SetColor(GameObject go, int color)
    {
        switch (color)
        {
            case 1:
                go.GetComponent<UISprite>().spriteName = "Grade2";
                break;
            case 2:
                go.GetComponent<UISprite>().spriteName = "Grade3";
                break;
            case 3:
                go.GetComponent<UISprite>().spriteName = "Grade4";
                break;
            case 4:
                go.GetComponent<UISprite>().spriteName = "Grade5";
                break;
            //case 5:
            //    go.GetComponent<UISprite>().spriteName = "Grade5";
            //    break;
        }
    }

    void GoAchievement(Achievement mAchievement)
    {
        int type = mAchievement.Type;
        switch (type)
        {
            case 22:
                CharacterRecorder.instance.gotoGateID = CharacterRecorder.instance.lastGateID;
                UIManager.instance.OpenPanel("MapUiWindow", true);//最新普通关卡
                break;
            case 23:

                CharacterRecorder.instance.gotoGateID = CharacterRecorder.instance.lastCreamGateID;
                CharacterRecorder.instance.IsOpenCreamCN = true;
                UIManager.instance.OpenPanel("MapUiWindow", true);//最新精英关卡
                break;
            case 24:
                if (CharacterRecorder.instance.lastGateID >= 10011)
                {
                    UIManager.instance.OpenPanel("PVPWindow", true);
                    UIManager.instance.CountSystem(UIManager.Systems.竞技场);
                    UIManager.instance.UpdateSystems(UIManager.Systems.竞技场);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("通过11关开放", PromptWindow.PromptType.Hint, null, null);
                }
                break;
            case 25:
                //UIManager.instance.OpenPanel("WoodsTheExpendables", true);
                //if (CharacterRecorder.instance.lastGateID >= 66)    //敢死队
                //{
                //    NetworkHandler.instance.SendProcess("1501#;");
                //}
                //else
                //{
                //    UIManager.instance.OpenPromptWindow("通过66关开放", PromptWindow.PromptType.Hint, null, null);
                //}
                NetworkHandler.instance.SendProcess("1501#;");
                CharacterRecorder.instance.isTaskGoto = true;
                UIManager.instance.CountSystem(UIManager.Systems.丛林冒险);
                UIManager.instance.UpdateSystems(UIManager.Systems.丛林冒险);
                break;
            case 26:
                //if (CharacterRecorder.instance.lastGateID >= 16)
                //{
                //    UIManager.instance.OpenPanel("EverydayWindow", true);   //日常副本
                //}
                //else
                //{
                //    UIManager.instance.OpenPromptWindow("通过16关开放", PromptWindow.PromptType.Hint, null, null);
                //}

                //Debug.LogError("日常副本.....");
                if (transform.Find("LabelTask").GetComponent<UILabel>().text.Contains("赏金猎人"))
                {
                    CharacterRecorder.instance.EverydayTab = 1;
                }

                UIManager.instance.OpenPanel("EverydayWindow", true);
                UIManager.instance.CountSystem(UIManager.Systems.日常副本);
                UIManager.instance.UpdateSystems(UIManager.Systems.日常副本);
                break;
            case 27:
                UIManager.instance.OpenPanel("RoleWindow", true);//英雄升级界面
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(2);
                break;
            case 28:
                UIManager.instance.OpenPanel("RoleWindow", true);//升品界面
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(4);
                break;
            case 29:
                break;
            case 30:
                UIManager.instance.OpenPanel("GachaWindow", true);//抽卡界面
                break;
            case 31:
                if (CharacterRecorder.instance.level >= 15)
                {
                    UIManager.instance.OpenPanel("BuyGlodWindow", false);//弹出点金
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("15级开放", PromptWindow.PromptType.Hint, null, null);
                }
                break;
            case 32:
                //UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
                NetworkHandler.instance.SendProcess("5012#10602;");//弹出购买体力
                break;
            case 33:
                if (CharacterRecorder.instance.legionID != 0)//CharacterRecorder.instance.level >= 25 && 
                {
                    //UIManager.instance.OpenPanel("LegionMainHaveWindow", true);
                    UIManager.instance.OpenPanel("LegionContributeWindow", true);
                }
                //else if (CharacterRecorder.instance.level < 25)
                //{
                //    UIManager.instance.OpenPromptWindow("25级开放！", PromptWindow.PromptType.Hint, null, null);
                //}
                else if (CharacterRecorder.instance.legionID == 0)
                {
                    UIManager.instance.OpenPromptWindow("暂未加入任何军团，无法捐献！", PromptWindow.PromptType.Hint, null, null);
                }
                break;
            case 34:
                UIManager.instance.OpenPanel("RoleWindow", true);//培养界面
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(3);
                break;
            case 35:
                UIManager.instance.OpenPanel("MapUiWindow", true);//资源点占领
                break;
            case 36:
                //if (CharacterRecorder.instance.level < 5 || CharacterRecorder.instance.lastGateID < 10034)
                //{
                //    UIManager.instance.OpenPromptWindow("长官，通关33关开放该功能！", PromptWindow.PromptType.Hint, null, null);
                //}
                //else
                //{
                //    UIManager.instance.OpenPanel("MapUiWindow", true);//世界事件
                //    GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().TheWorldBtnClick();
                //}
                UIManager.instance.OpenPanel("MapUiWindow", true);//世界事件
                GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().DeleteTime();
                //StartCoroutine(OpenWorldThing());

                break;
            case 37:
                //if (CharacterRecorder.instance.lastGateID >= 22)
                //{
                //    UIManager.instance.OpenPanel("GrabItemWindow", true);//饰品合成
                //}
                //else
                //{
                //    UIManager.instance.OpenPromptWindow("22级开放", PromptWindow.PromptType.Hint, null, null);
                //}
                UIManager.instance.OpenPanel("GrabItemWindow", true);//饰品合成
                UIManager.instance.CountSystem(UIManager.Systems.夺宝奇兵);
                UIManager.instance.UpdateSystems(UIManager.Systems.夺宝奇兵);
                break;
            case 40:
                //UIManager.instance.OpenSinglePanel("VIPShopWindow", false);
                UIManager.instance.OpenPanel("VIPShopWindow", true);
                break;
            case 41:
                //UIManager.instance.OpenSinglePanel("VIPShopWindow", false);
                UIManager.instance.OpenPanel("VIPShopWindow", true);
                break;
            case 20://普通关卡

                if (mAchievement.GatetotalCount + 10000 <= CharacterRecorder.instance.lastGateID)
                {
                    Debug.LogError(mAchievement.GatetotalCount + 10000);
                    CharacterRecorder.instance.gotoGateID = (mAchievement.GatetotalCount + 10000);
                    UIManager.instance.OpenPanel("MapUiWindow", true);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("关卡未开放", PromptWindow.PromptType.Hint, null, null);
                }

                //UIManager.instance.OpenPanel("MapUiWindow", true);//最新可挑战关卡
                break;
            case 42:
                if (CharacterRecorder.instance.lastGateID > TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.jingyingguangka).Level)
                {
                    if (mAchievement.GatetotalCount <= CharacterRecorder.instance.lastCreamGateID)
                    {
                        CharacterRecorder.instance.gotoGateID = mAchievement.GatetotalCount;
                        CharacterRecorder.instance.IsOpenCreamCN = true;
                        UIManager.instance.OpenPanel("MapUiWindow", true);
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("关卡未开放", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("精英关卡未开放", PromptWindow.PromptType.Hint, null, null);
                }
                break;
            case 43:
                CharacterRecorder.instance.FriendListTab = 0;
                UIManager.instance.OpenPanel("FriendWindow", true);//精力赠送好友
                break;
            case 44:
                UIManager.instance.OpenPanel("TeamCopyWindow", true);//组队
                UIManager.instance.CountSystem(UIManager.Systems.组队副本);
                UIManager.instance.UpdateSystems(UIManager.Systems.组队副本);
                break;
            case 45:
                UIManager.instance.OpenPanel("SmuggleWindow", true);//走私
                UIManager.instance.CountSystem(UIManager.Systems.边境走私);
                UIManager.instance.UpdateSystems(UIManager.Systems.边境走私);
                break;
            case 46:
                if (CharacterRecorder.instance.legionID != 0)//CharacterRecorder.instance.level >= 25 && 
                {
                    UIManager.instance.OpenPanel("LegionTaskWindow", true);//军团任务
                }
                else if (CharacterRecorder.instance.legionID == 0)
                {
                    UIManager.instance.OpenPromptWindow("暂未加入任何军团", PromptWindow.PromptType.Hint, null, null);
                }
                break;
            case 1:
                UIManager.instance.OpenPanel("MapUiWindow", true);//最新可挑战关卡
                break;
            case 2:
                UIManager.instance.OpenPanel("GachaWindow", true);
                break;
            case 3:
                UIManager.instance.OpenPanel("RoleWindow", true);//战斗力，英雄界面
                break;
            case 4:
                UIManager.instance.OpenPanel("MapUiWindow", true);//最新可挑战关卡
                break;
            case 5:
                UIManager.instance.OpenPanel("MapUiWindow", true);//前往资源占领
                break;
            case 6://精益求精
                UIManager.instance.OpenPanel("RoleWindow", true);//前往升品界面
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(4);
                break;
            case 7://精益求精
                UIManager.instance.OpenPanel("RoleWindow", true);//前往升品界面
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(4);
                break;
            case 8://精益求精
                UIManager.instance.OpenPanel("RoleWindow", true);//前往升品界面
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(4);
                break;
            case 9://精益求精
                UIManager.instance.OpenPanel("RoleWindow", true);//前往升品界面
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(4);
                break;
            case 10://精益求精
                UIManager.instance.OpenPanel("RoleWindow", true);//前往升品界面
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(4);
                break;
            case 11://精益求精
                UIManager.instance.OpenPanel("RoleWindow", true);//前往升品界面
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(4);
                break;
            case 12://精益求精
                UIManager.instance.OpenPanel("RoleWindow", true);//前往升品界面
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(4);
                break;
            case 13://精益求精
                UIManager.instance.OpenPanel("RoleWindow", true);//前往升品界面
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(4);
                break;
            case 14://精益求精
                UIManager.instance.OpenPanel("RoleWindow", true);//前往升品界面
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(4);
                break;
            case 15://精益求精
                UIManager.instance.OpenPanel("RoleWindow", true);//前往升品界面
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(4);
                break;
            case 16://精益求精
                UIManager.instance.OpenPanel("RoleWindow", true);//前往升品界面
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(4);
                break;
            case 17://精益求精
                UIManager.instance.OpenPanel("RoleWindow", true);//前往升品界面
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(4);
                break;
            case 18://精益求精
                UIManager.instance.OpenPanel("RoleWindow", true);//前往升品界面
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(4);
                break;
            case 19://精益求精
                UIManager.instance.OpenPanel("RoleWindow", true);//前往升品界面
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(4);
                break;
            default:
                Debug.Log("aa");
                break;
        }
    }
    //IEnumerator OpenSelectAddFriend()
    //{
    //    yield return null;
    //    if (GameObject.Find("FriendWindow")==null)
    //    {
    //        StartCoroutine("OpenSelectAddFriend");
    //    }
    //    else
    //    {
    //        GameObject.Find("FriendWindow").GetComponent<FriendWindow>().SendToSeverToGetList(1);
    //        StopCoroutine("OpenSelectAddFriend");
    //    }
    //}
}
