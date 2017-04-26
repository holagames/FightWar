using UnityEngine;
using System.Collections;

public class OpenServiceItem : MonoBehaviour {

    public UILabel Des;
    public UILabel CompleteCount;
    public GameObject[] AwardList;
    public GameObject GoToButton;
    public GameObject AwardButton;
    public GameObject HaveButton;
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;
    public UIAtlas CommonAtlas;
    
	void Start () {
	
	}

    public void SetInfo(ActivitySevenDay Act) 
    {
        if (Act.CompleteState == 0) 
        {
            AwardButton.SetActive(false);
            HaveButton.SetActive(false);
            GoToButton.SetActive(true);
            //CompleteCount.gameObject.SetActive(true);
            TopMessage(Act);
            //CompleteCount.text = Act.CompleteCount.ToString() + "/" + Act.LimitTerm;//
            UIEventListener.Get(GoToButton).onClick += delegate(GameObject go)
            {
                Debug.Log("前往");
                if (CharacterRecorder.instance.ActivityTime < 86400)
                {
                    UIManager.instance.OpenPromptWindow("活动已经结束", PromptWindow.PromptType.Hint, null, null);
                }
                else 
                {
                    GotoOtherWindow(Act); 
                }
            };
        }
        else if (Act.CompleteState == 1) 
        {
            GoToButton.SetActive(false);
            HaveButton.SetActive(false);
            AwardButton.SetActive(true);
            //CompleteCount.gameObject.SetActive(false);
            TopMessage(Act);
            UIEventListener.Get(AwardButton).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.ActivityTime < 86400)
                {
                    UIManager.instance.OpenPromptWindow("活动已经结束", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    NetworkHandler.instance.SendProcess(string.Format("9122#{0};", Act.ActivitySevenDayID));
                }
                
            };
        }
        else if (Act.CompleteState == 2) 
        {
            GoToButton.SetActive(false);
            AwardButton.SetActive(false);
            HaveButton.SetActive(true);
            TopMessage(Act);
            //CompleteCount.gameObject.SetActive(false);
        }

        int j = 0;
        if (Act.ItemID1 > 0) 
        {
            AwardList[j].SetActive(true);
            SetColor(AwardList[j], Act.ItemID1, Act.ItemNum1);
            AwardList[j].GetComponent<ItemExplanation>().SetAwardItem(Act.ItemID1, Act.ItemNum1);
            j++;
        }
        if (Act.ItemID2 > 0)
        {
            AwardList[j].SetActive(true);
            SetColor(AwardList[j], Act.ItemID2, Act.ItemNum2);
            AwardList[j].GetComponent<ItemExplanation>().SetAwardItem(Act.ItemID2, Act.ItemNum2);
            j++;
        }
        if (Act.ItemID3 > 0)
        {
            AwardList[j].SetActive(true);
            SetColor(AwardList[j], Act.ItemID3, Act.ItemNum3);
            AwardList[j].GetComponent<ItemExplanation>().SetAwardItem(Act.ItemID3, Act.ItemNum3);
            j++;
        }
        if (Act.ItemID4 > 0)
        {
            AwardList[j].SetActive(true);
            SetColor(AwardList[j], Act.ItemID4, Act.ItemNum4);
            AwardList[j].GetComponent<ItemExplanation>().SetAwardItem(Act.ItemID4, Act.ItemNum4);
            j++;
        }
    }
    void SetColor(GameObject Awardobj,int ItemId,int ItemNum) 
    {
        if (ItemId.ToString()[0] == '3') 
        {
            Awardobj.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(ItemId).itemGrade.ToString();
            Awardobj.transform.Find("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            Awardobj.transform.Find("Icon").GetComponent<UISprite>().spriteName = ItemId.ToString();
            Awardobj.transform.Find("Num").GetComponent<UILabel>().text = ItemNum.ToString();
        }
        else if (ItemId.ToString()[0] == '5') 
        {
            Awardobj.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(ItemId).itemGrade.ToString();
            Awardobj.transform.Find("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            Awardobj.transform.Find("Icon").GetComponent<UISprite>().spriteName = ItemId.ToString();
            Awardobj.transform.Find("Num").GetComponent<UILabel>().text = ItemNum.ToString();
        }
        else if (ItemId.ToString()[0] == '7') 
        {
            Awardobj.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(ItemId).itemGrade.ToString();
            Awardobj.transform.Find("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
            Awardobj.transform.Find("Icon").GetComponent<UISprite>().spriteName = (ItemId-10000).ToString();
            Awardobj.transform.Find("Num").GetComponent<UILabel>().text = ItemNum.ToString();
        }
        else if (ItemId.ToString()[0] == '2') 
        {
            Awardobj.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(ItemId).itemGrade.ToString();
            Awardobj.transform.Find("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            Awardobj.transform.Find("Icon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(ItemId).picID.ToString();
            Awardobj.transform.Find("Num").GetComponent<UILabel>().text = ItemNum.ToString();
        }
        else
        {
            Awardobj.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(ItemId).itemGrade.ToString();
            Awardobj.transform.Find("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            Awardobj.transform.Find("Icon").GetComponent<UISprite>().spriteName = ItemId.ToString();
            Awardobj.transform.Find("Num").GetComponent<UILabel>().text = ItemNum.ToString();
        }
    }
    void TopMessage(ActivitySevenDay _Act) 
    {
        switch (_Act.LimitType) 
        {
            case 1:
                Des.text = "战队等级达到" + _Act.LimitTerm;
                break;
            case 2:
                if (int.Parse(_Act.LimitTerm) < 20000)
                {
                    Des.text = "完成关卡:" + TextTranslator.instance.GetGateByID(int.Parse(_Act.LimitTerm)).name;
                }
                else
                {
                    Des.text = "完成精英关卡:" + TextTranslator.instance.GetGateByID(int.Parse(_Act.LimitTerm)).name;
                }
                break;
            case 3:
                string[] tecSplit1 = _Act.LimitTerm.Split('$');
                int t1 = int.Parse(tecSplit1[1]);
                if (t1 == 1) { Des.text = tecSplit1[0] + "个英雄升级为白色品质"; }
                else if (t1 == 2) { Des.text = tecSplit1[0] + "个英雄升级为绿色品质"; }
                else if (t1 == 3) { Des.text = tecSplit1[0] + "个英雄升级为绿色+1品质"; }
                else if (t1 == 4) { Des.text = tecSplit1[0] + "个英雄升级为绿色+2品质"; }
                else if (t1 == 5) { Des.text = tecSplit1[0] + "个英雄升级为蓝色品质"; }
                else if (t1 == 6) { Des.text = tecSplit1[0] + "个英雄升级为蓝色+1品质"; }
                else if (t1 == 7) { Des.text = tecSplit1[0] + "个英雄升级为蓝色+2品质"; }
                else if (t1 == 8) { Des.text = tecSplit1[0] + "个英雄升级为蓝色+3品质"; }
                else if (t1 == 9) { Des.text = tecSplit1[0] + "个英雄升级为紫色品质"; }
                //Des.text = tecSplit1[0] + "英雄升级为" + tecSplit1[1] + "品质";
                break;
            case 4:
                string[] tecSplit2 = _Act.LimitTerm.Split('$');
                int t2 = int.Parse(tecSplit2[1]);
                if (t2 == 2) { Des.text = tecSplit2[0] + "个英雄升级为2星"; }
                else if (t2 == 3) { Des.text = tecSplit2[0] + "个英雄升级为3星"; }
                else if (t2 == 4) { Des.text = tecSplit2[0] + "个英雄升级为4星"; }
                else if (t2 == 5) { Des.text = tecSplit2[0] + "个英雄升级为5星"; }
                else if (t2 == 6) { Des.text = tecSplit2[0] + "个英雄升级为6星"; }
                //Des.text = tecSplit2[0] + "英雄升级为" + tecSplit2[1] + "军衔";
                break;
            case 5:
                string[] tecSplit3 = _Act.LimitTerm.Split('$');
                Des.text = tecSplit3[0] + "个英雄技能升级到技能等级" + tecSplit3[1];
                break;
            case 6:
                string[] tecSplit4 = _Act.LimitTerm.Split('$');
                Des.text = tecSplit4[0] + "个装备达到装备等级" + tecSplit4[1];
                break;
            case 7:
                string[] tecSplit5 = _Act.LimitTerm.Split('$');
                Des.text = tecSplit5[0] + "个装备达到装备精炼等级" + tecSplit5[1];
                break;
            case 8:
                string[] tecSplit6 = _Act.LimitTerm.Split('$');
                Des.text = tecSplit6[0] + "个饰品达到饰品强化等级" + tecSplit6[1];
                break;
            case 9:
                string[] tecSplit7 = _Act.LimitTerm.Split('$');
                Des.text = tecSplit7[0] + "个饰品达到饰品精炼等级" + tecSplit7[1];
                break;
            case 10:
                Des.text = "竞技场排名达到" + _Act.LimitTerm;
                break;
            case 11:
                Des.text = "竞技场累计积分" + _Act.LimitTerm;
                break;
            case 12:
                string[] tecSplit8 = _Act.LimitTerm.Split('$');
                int t3 = int.Parse(tecSplit8[0]);
                if (t3 == 3) 
                {
                    Des.text = "合成" + tecSplit8[1] + "个品质蓝色的饰品";
                }
                else if (t3 == 4) 
                {
                    Des.text = "合成" + tecSplit8[1] + "个品质紫色的饰品";
                }
                else if (t3 == 5)
                {
                    Des.text = "合成" + tecSplit8[1] + "个品质橙色的饰品";
                }
                else if (t3 == 6)
                {
                    Des.text = "合成" + tecSplit8[1] + "个品质红色的饰品";
                }
                //Des.text = tecSplit8[1] + "饰品合成为" + tecSplit8[0] + "品质";
                //Des.text = "合成" + tecSplit8[1] + "个品质" + tecSplit8[0] + "的饰品";
                break;
            case 13:
                Des.text = "丛林冒险最高层数达到" + _Act.LimitTerm;
                break;
            case 14:
                Des.text = "丛林冒险累计积分达到" + _Act.LimitTerm;
                break;
            case 15:
                Des.text = "战力达到" + _Act.LimitTerm;
                break;
            default:
                break;

        }
    }
    void HowWaytoWindow(int mapId) 
    {
        if (mapId < 20000 && mapId <= CharacterRecorder.instance.lastGateID) 
        {
            CharacterRecorder.instance.gotoGateID = mapId;
            UIManager.instance.OpenPanel("MapUiWindow", true);
        }
        else if (mapId > 20000)
        {
            if (CharacterRecorder.instance.lastGateID > TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.jingyingguangka).Level)//CharacterRecorder.instance.level >= 8
            {
                if (mapId <= CharacterRecorder.instance.lastCreamGateID)
                {
                    CharacterRecorder.instance.gotoGateID = mapId;
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
                UIManager.instance.OpenPromptWindow("关卡未开放", PromptWindow.PromptType.Hint, null, null);
            }
        }
        else 
        {
            UIManager.instance.OpenPromptWindow("关卡未开放", PromptWindow.PromptType.Hint, null, null);
        }
    }
    void GotoOtherWindow(ActivitySevenDay _Act) 
    {
        switch (_Act.LimitType)
        {
            case 1:
                UIManager.instance.OpenPanel("MapUiWindow", true);
                break;
            case 2:
                //HowWaytoWindow(int.Parse(_Act.LimitTerm));  //现在改成无往不利和精英之路的“前往”按钮前往到精英关卡最远处  8.16
                if (CharacterRecorder.instance.lastCreamGateID != 0)
                {
                    CharacterRecorder.instance.gotoGateID = CharacterRecorder.instance.lastCreamGateID;
                    CharacterRecorder.instance.IsOpenCreamCN = true;
                    UIManager.instance.OpenPanel("MapUiWindow", true);//最新精英关卡
                }
                else 
                {
                    UIManager.instance.OpenPanel("MapUiWindow", true);//精英关卡未开放，直接前往大地图
                }
                break;
            case 3:

                CharacterRecorder.instance.RoleTabIndex = 2;
                UIManager.instance.OpenPanel("RoleWindow", true);
                break;
            case 4:
                CharacterRecorder.instance.RoleTabIndex = 5;
                UIManager.instance.OpenPanel("RoleWindow", true);
                break;
            case 5:
                
                 CharacterRecorder.instance.RoleTabIndex = 6;
                 UIManager.instance.OpenPanel("RoleWindow", true);
                break;
            case 6:
                UIManager.instance.OpenPanel("StrengEquipWindow", true);
                break;
            case 7:
                UIManager.instance.OpenPanel("StrengEquipWindow", true);
                break;
            case 8:
                UIManager.instance.OpenPanel("GrabItemWindow", true);
                break;
            case 9:
                UIManager.instance.OpenPanel("GrabItemWindow", true);
                break;
            case 10:
                if (CharacterRecorder.instance.lastGateID > TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.jingjichang).Level)
                {
                    UIManager.instance.OpenPanel("PVPWindow", true);
                }
                else 
                {
                    UIManager.instance.OpenPromptWindow("竞技场未开放", PromptWindow.PromptType.Hint, null, null);
                }               
                break;
            case 11:
                if (CharacterRecorder.instance.lastGateID > TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.jingjichang).Level)
                {
                    UIManager.instance.OpenPanel("PVPWindow", true);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("竞技场未开放", PromptWindow.PromptType.Hint, null, null);
                }      
                break;
            case 12:
                if (CharacterRecorder.instance.lastGateID > TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.duobao).Level)
                {
                    UIManager.instance.OpenPanel("GrabItemWindow", true);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("夺宝奇兵未开放", PromptWindow.PromptType.Hint, null, null);
                } 
                //UIManager.instance.OpenPanel("GrabItemWindow", true);
                break;
            case 13:
                if (CharacterRecorder.instance.lastGateID > TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.conglingmaoxian).Level)
                {
                    NetworkHandler.instance.SendProcess("1501#;");
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("丛林冒险未开放", PromptWindow.PromptType.Hint, null, null);
                }           
                break;
            case 14:
                if (CharacterRecorder.instance.lastGateID > TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.conglingmaoxian).Level)
                {
                    NetworkHandler.instance.SendProcess("1501#;");
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("丛林冒险未开放", PromptWindow.PromptType.Hint, null, null);
                } 
                break;
            case 15:
                {
                    UIManager.instance.OpenSinglePanel("LittleHelperWindow", false);
                }
                break;
            default:
                break;
        }
    }
}
