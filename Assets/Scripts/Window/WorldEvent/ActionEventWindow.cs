using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ActionEventWindow : MonoBehaviour {

    int EventId = 0;
    int timer = 0;
    List<int> itemList = new List<int>();
    List<int> itemNum = new List<int>();

    public UITexture heroIcon;
    public UILabel labelEventName;
    public UILabel labelEventDes;
    public UILabel TimerLabel;
    public UILabel LabelVictory1;
    public UILabel LabelVictory;
    public GameObject FightPrefab;
    public GameObject ItemPrefab;
    public UIGrid FightGrid;
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;
    public UILabel ChallengNum;
    public GameObject BgColider;
    public GameObject StartButton;
    public int CurEventId;

    public GameObject SpriteArrow1;
    public GameObject SpriteArrow2;
    public GameObject SpriteArrow3;
    public GameObject SpriteArrow4;
    public GameObject SpriteArrow5;
    public GameObject SpriteArrow6;
    public GameObject SpriteArrow7;
    public GameObject SpriteArrow8;

    public UILabel PowerNum;
	// Use this for initialization
	void Start () {
        UIManager.instance.CountSystem(UIManager.Systems.精英世界事件);
        UIManager.instance.UpdateSystems(UIManager.Systems.精英世界事件);

        UIEventListener.Get(BgColider).onClick = delegate(GameObject go)
        {
            this.gameObject.SetActive(false);
            GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().CloseTopContent();
            GameObject _mask = GameObject.Find("MapMask");
            if(_mask!=null)
            {
                _mask.SetActive(false);
            }
        };

        UIEventListener.Get(StartButton).onClick = delegate(GameObject go)
        {
            if (CharacterRecorder.instance.sprite < 2)
            {
                UIManager.instance.OpenPromptWindow("精力不足，是否购买精力药剂?", PromptWindow.PromptType.Confirm, OpenBuyWindow, null);
                return;
            }
            UIManager.instance.OpenPanel("LoadingWindow", true);
            if (GameObject.Find("MapObject") != null)
            {
                DestroyImmediate(GameObject.Find("MapObject"));
            }

            UIManager.instance.CurGateId = CurEventId.ToString();
            UIManager.instance.StartGate(UIManager.GateAnchorName.gateFight);
            UIManager.instance.MapGateInfoLoading = true;

            SceneTransformer.instance.NowGateID = CurEventId;
            PictureCreater.instance.FightStyle = 0;
            PictureCreater.instance.StartFight();
        };
	}

    void OpenBuyWindow()
    {
        UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
        NetworkHandler.instance.SendProcess("5012#10702;");
    }

    public void InitActionEvent(int _EventId)
    {
        GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().ShowTopContent();
        EventId = _EventId;
        ClearGridChild();
        GetTimer();
        CancelInvoke("UpdateTime");
        InvokeRepeating("UpdateTime", 0, 1f);
        Debug.Log("千里走单骑的ID：" + _EventId);
        ActionEvent _actionEvent = TextTranslator.instance.GetActionEventById(_EventId);
        CurEventId = _actionEvent.GateID;
        SceneTransformer.instance.NowGateID = CurEventId;
        TextTranslator.Gate _gate = TextTranslator.instance.GetGateByID(_actionEvent.GateID);
        PowerNum.text = _gate.needForce.ToString();
        if (_gate.icon > 65000)
        {
            heroIcon.mainTexture = Resources.Load("Loading/" + (_gate.icon - 5000)) as Texture;
        }
        else
        {
            heroIcon.mainTexture = Resources.Load("Loading/" + _gate.icon) as Texture;
        }
        heroIcon.GetComponent<UITexture>().MakePixelPerfect();
        heroIcon.GetComponent<UITexture>().width = heroIcon.GetComponent<UITexture>().width / 20 * 17;
        heroIcon.GetComponent<UITexture>().height = heroIcon.GetComponent<UITexture>().height / 20 * 17;
        labelEventName.text = _actionEvent.GateName;
        labelEventDes.text = TextTranslator.instance.GetGateByID(_actionEvent.GateID).description;
        LoadReward(_actionEvent);
        FightGrid.Reposition();
        StartCoroutine(ShowDescription());
    }

    void GetTimer()
    {
        string[] dataSplit = CharacterRecorder.instance.worldEventList.Split(';');
        string[] _dataInfo = dataSplit[3].Split('$');
        if (_dataInfo[1] != null && _dataInfo[1] != "")
        {
            timer = int.Parse(_dataInfo[1]);
        }
    }

    void UpdateTime()
    {
        if (timer > 0)
        {
            int fenNum = (int)((timer / 60) % 60);
            int miao = (int)(timer % 60);
            int day = (int)((timer / 60) / 60);
            TimerLabel.text = ((day < 10) ? "0" : "") + day.ToString() + ":" + ((fenNum < 10) ? "0" : "") + fenNum.ToString() + ":" + ((miao < 10) ? "0" : "") + miao.ToString();
            timer -= 1;

        }

    }

    IEnumerator ShowDescription()
    {
        LabelVictory1.text = "";
        //LabelVictory2.text = "";
        //LabelVictory3.text = "";
        LabelVictory.text = "";

        string LabelVictoryText = "";
        string LabelVictoryText1 = "";
        //string LabelVictoryText2 = "";
        //string LabelVictoryText3 = "";

        TextTranslator.Gate g = TextTranslator.instance.GetGateByID(SceneTransformer.instance.NowGateID);
        if (g.scriptID1 > 0)
        {
            GateLimit gl = TextTranslator.instance.GetGateLimitDicByID(g.scriptID1);
            if (gl.LimitTerm == 0)
            {
                //GameObject.Find("AutoButton/Sprite/SpriteXianding").SetActive(false);
            }
            yield return new WaitForSeconds(0.5f);

            switch (gl.LimitTerm) //限制条件
            {
                case 1:
                    LabelVictoryText = "所有单位血量为" + gl.LimitParam1;
                    break;
                case 2:
                    LabelVictoryText = "限制攻击类职业上阵";
                    break;
                case 3:
                    LabelVictoryText = "限制防御类职业上阵";
                    break;
                case 4:
                    LabelVictoryText = "限制特殊类职业上阵";
                    break;
                case 5:
                    LabelVictoryText = "限制攻击类职业无法上阵";
                    break;
                case 6:
                    LabelVictoryText = "限制防御类职业无法上阵";
                    break;
                case 7:
                    LabelVictoryText = "限制特殊类职业无法上阵";
                    break;
                case 8:
                    LabelVictoryText = "限制只能上阵" + gl.LimitParam1 + "名英雄";
                    break;
                case 9:
                    LabelVictoryText = "己方开场随机获得BUFF";
                    break;
                case 10:
                    LabelVictoryText = "敌方开场随机获得BUFF";
                    break;
                case 11:
                    LabelVictoryText = "己方开场随机受到DEBUFF";
                    break;
                case 12:
                    if (TextTranslator.instance.GetHeroInfoByHeroID(gl.LimitParam1) != null)
                    {
                        LabelVictoryText = "限制" + TextTranslator.instance.GetHeroInfoByHeroID(gl.LimitParam1).heroName + "必须上阵";
                    }
                    break;
                case 13:
                    if (TextTranslator.instance.GetHeroInfoByHeroID(gl.LimitParam1) != null)
                    {
                        LabelVictoryText = "限制" + TextTranslator.instance.GetHeroInfoByHeroID(gl.LimitParam1).heroName + "无法上阵";
                    }
                    break;
            }

            switch (gl.WinTerm) //1星条件
            {
                case 1:
                    LabelVictoryText1 = "敌全灭";
                    break;
                case 2:
                    for (int j = 0; j < TextTranslator.instance.enemyInfoList.Count; j++)
                    {
                        if (gl.WinParam1 == TextTranslator.instance.enemyInfoList[j].enemyID)
                        {
                            LabelVictoryText1 = "击败敌人" + TextTranslator.instance.enemyInfoList[j].name;
                            break;
                        }
                    }
                    break;
                case 3:
                    LabelVictoryText1 = "己方人质存活并且全灭敌方";
                    break;
                case 4:
                    LabelVictoryText1 = "存活至第" + gl.WinParam1 + "次步数";
                    break;
                case 5:
                    LabelVictoryText1 = "限定" + gl.WinParam1.ToString() + "步数内全灭敌人";
                    break;
            }

            //switch (gl.Star2Term) //2星条件
            //{
            //    case 1:
            //        if (gl.Star2Param1 == 0)
            //        {
            //            LabelVictoryText2 = "全员存活通关";
            //        }
            //        else if (gl.Star2Param1 == 0)
            //        {
            //            LabelVictoryText2 = "一人死亡通关";
            //        }
            //        else
            //        {
            //            LabelVictoryText2 = "两人以上死亡通关";
            //        }
            //        break;
            //    case 2:
            //        LabelVictoryText2 = "上阵" + gl.Star2Param1 + "人通关";
            //        break;
            //    case 3:
            //        LabelVictoryText2 = gl.Star2Param1.ToString() + "步数内通关";
            //        break;
            //}

            //switch (gl.Star3Term) //3星条件
            //{
            //    case 1:
            //        if (gl.Star3Param1 == 0)
            //        {
            //            LabelVictoryText3 = "全员存活通关";
            //        }
            //        else if (gl.Star3Param1 == 0)
            //        {
            //            LabelVictoryText3 = "一人死亡通关";
            //        }
            //        else
            //        {
            //            LabelVictoryText3 = "两人以上死亡通关";
            //        }
            //        break;
            //    case 2:
            //        LabelVictoryText3 = "上阵" + gl.Star3Param1 + "人通关";
            //        break;
            //    case 3:
            //        LabelVictoryText3 = gl.Star3Param1.ToString() + "步数内通关";
            //        break;
            //}
        }
        else
        {
            LabelVictoryText1 = "两人以上死亡通关";
            //LabelVictoryText2 = "一人死亡通关";
            //LabelVictoryText3 = "全员存活通关";
            LabelVictoryText = "";
            //GameObject.Find("AutoButton/Sprite/SpriteXianding").SetActive(false);
        }
        LabelVictory1.text = "";
        LabelVictory.text = "";

        foreach (var c in LabelVictoryText1)
        {
            LabelVictory1.text += c;
            yield return new WaitForSeconds(0.02f);
        }
        //foreach (var c in LabelVictoryText2)
        //{
        //    LabelVictory2.text += c;
        //    yield return new WaitForSeconds(0.02f);
        //}
        //foreach (var c in LabelVictoryText3)
        //{
        //    LabelVictory3.text += c;
        //    yield return new WaitForSeconds(0.02f);
        //}
        foreach (var c in LabelVictoryText)
        {
            LabelVictory.text += c;
            yield return new WaitForSeconds(0.02f);
        }
        if (GameObject.Find("AutoButton/Sprite/SpriteXianding") != null)
        {
            GameObject.Find("AutoButton/Sprite/SpriteXianding").GetComponent<TweenScale>().enabled = true;
            GameObject.Find("AutoButton/Sprite/SpriteXianding").GetComponent<TweenScale>().ResetToBeginning();
        }
    }

    void LoadReward(ActionEvent _CurAction)
    {
        List<ActionEvent> actionEventList = TextTranslator.instance.GetActionEventsByGroup(_CurAction.GroupID);
        for(int i = 0;i<actionEventList.Count;i++)
        {
            GameObject go = NGUITools.AddChild(FightGrid.gameObject,FightPrefab);
            go.SetActive(true);
            go.name = "Fight" + (i+1);
            LoadRewardItem(actionEventList[i], i + 1);
            if(actionEventList[i] == _CurAction)
            {
                GameObject _alreadyHave = GameObject.Find(go.name + "/SpriteBG/SpriteAlready");
                if(_alreadyHave!=null)
                {
                    _alreadyHave.SetActive(false);
                }
                ChallengNum.text = (actionEventList.Count - i).ToString();
                //return;
            }
            if (actionEventList[i].ActionEventID > _CurAction.ActionEventID)
            {
                GameObject _alreadyHave = GameObject.Find(go.name + "/SpriteBG/SpriteAlready");
                if (_alreadyHave != null)
                {
                    _alreadyHave.SetActive(false);
                }
            }
        }
    }

    void LoadRewardItem(ActionEvent _actionEvent, int num)
    {
        GameObject go = GameObject.Find("Fight" + num);
        if(go!=null)
        {
            go.transform.Find("LabelFall").GetComponent<UILabel>().text = "第"+ num +"战奖励：";
            itemList.Clear();
            itemNum.Clear();
            InitItemList(_actionEvent);
            for(int i = 0;i< itemList.Count;i++)
            {
                GameObject item = NGUITools.AddChild(go.transform.Find("Grid").gameObject, ItemPrefab);
                item.transform.localScale = Vector3.one;
                item.SetActive(true);
                item.name = "item" + (i + 1);
                //Debug.Log("第"+ num +"战奖励:");
                //Debug.Log("物品Id:" + itemList[i]);
                //Debug.Log("物品SHULIANG:" + itemNum[i]);
                InitItemInfo(item,itemList[i],itemNum[i]);
                item.GetComponent<ItemExplanation>().SetAwardItem(itemList[i], 0);
            }
            go.transform.Find("Grid").GetComponent<UIGrid>().Reposition();
        }
    }
    void InitItemList(ActionEvent actionEvent)
    {
        if (actionEvent.BonusID1 != null && actionEvent.BonusID1!= 0)
        {
            itemList.Add(actionEvent.BonusID1);
            itemNum.Add(actionEvent.BonusNum1);
        }
        if (actionEvent.BonusID2 != null && actionEvent.BonusID2 != 0)
        {
            itemList.Add(actionEvent.BonusID2);
            itemNum.Add(actionEvent.BonusNum2);
        }
        if (actionEvent.BonusID3 != null && actionEvent.BonusID3 != 0)
        {
            itemList.Add(actionEvent.BonusID3);
            itemNum.Add(actionEvent.BonusNum3);
        }
        if (actionEvent.BonusID4 != null && actionEvent.BonusID4 != 0)
        {
            itemList.Add(actionEvent.BonusID4);
            itemNum.Add(actionEvent.BonusNum4);
        }
    }

    void InitItemInfo(GameObject obj,int goodsId,int num)
    {
        if (goodsId > 40000 && goodsId < 50000)
        {
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (goodsId - 10000).ToString();
            TextTranslator.instance.GetItemByItemCode(goodsId - 10000);
            obj.transform.Find("Number").GetComponent<UILabel>().text = num.ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(true);
        }
        else if (goodsId > 80000 && goodsId <90000)
        {
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = ((goodsId - 30000) / 10 * 10).ToString();
            obj.transform.Find("Number").GetComponent<UILabel>().text = num.ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(true);
        }
        else if (goodsId > 70000 && goodsId < 79999)
        {
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (goodsId - 10000).ToString();
            obj.transform.Find("Number").GetComponent<UILabel>().text = num.ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(true);
        }
        else
        {
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = goodsId.ToString();
            obj.transform.Find("Number").GetComponent<UILabel>().text = num.ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(false);
        }
        TextTranslator.ItemInfo _item = TextTranslator.instance.GetItemByItemCode(goodsId);
        SetGrade(obj.transform.Find("Bg").gameObject,_item.itemGrade);
    }

    void SetGrade(GameObject go, int color)
    {
        switch (color)
        {
            case 1:
                go.GetComponent<UISprite>().spriteName = "Grade1";
                break;
            case 2:
                go.GetComponent<UISprite>().spriteName = "Grade2";
                break;
            case 3:
                go.GetComponent<UISprite>().spriteName = "Grade3";
                break;
            case 4:
                go.GetComponent<UISprite>().spriteName = "Grade4";
                break;
            case 5:
                go.GetComponent<UISprite>().spriteName = "Grade5";
                break;
            case 6:
                go.GetComponent<UISprite>().spriteName = "Grade6";
                break;
            case 7:
                go.GetComponent<UISprite>().spriteName = "Grade7";
                break;
        }
    }

    void ClearGridChild()
    {
        int _deleteNum = FightGrid.transform.childCount;
        for (int i = 0; i < _deleteNum; i++)
        {
            DestroyImmediate(FightGrid.transform.GetChild(0).gameObject);
        }
    }
    public void SetArrow(int Direction)
    {
        CharacterRecorder.instance.Direction_Back = Direction;
        switch (Direction)
        {
            case 1:
                SpriteArrow1.SetActive(true);
                SpriteArrow2.SetActive(false);
                SpriteArrow3.SetActive(false);
                SpriteArrow4.SetActive(false);
                SpriteArrow5.SetActive(false);
                SpriteArrow6.SetActive(false);
                SpriteArrow7.SetActive(false);
                SpriteArrow8.SetActive(false);
                break;
            case 2:
                SpriteArrow1.SetActive(false);
                SpriteArrow2.SetActive(true);
                SpriteArrow3.SetActive(false);
                SpriteArrow4.SetActive(false);
                SpriteArrow5.SetActive(false);
                SpriteArrow6.SetActive(false);
                SpriteArrow7.SetActive(false);
                SpriteArrow8.SetActive(false);
                break;
            case 3:
                SpriteArrow1.SetActive(false);
                SpriteArrow2.SetActive(false);
                SpriteArrow3.SetActive(true);
                SpriteArrow4.SetActive(false);
                SpriteArrow5.SetActive(false);
                SpriteArrow6.SetActive(false);
                SpriteArrow7.SetActive(false);
                SpriteArrow8.SetActive(false);
                break;
            case 4:
                SpriteArrow1.SetActive(false);
                SpriteArrow2.SetActive(false);
                SpriteArrow3.SetActive(false);
                SpriteArrow4.SetActive(true);
                SpriteArrow5.SetActive(false);
                SpriteArrow6.SetActive(false);
                SpriteArrow7.SetActive(false);
                SpriteArrow8.SetActive(false);
                break;
            case 5:
                SpriteArrow1.SetActive(false);
                SpriteArrow2.SetActive(false);
                SpriteArrow3.SetActive(false);
                SpriteArrow4.SetActive(false);
                SpriteArrow5.SetActive(true);
                SpriteArrow6.SetActive(false);
                SpriteArrow7.SetActive(false);
                SpriteArrow8.SetActive(false);
                break;
            case 6:
                SpriteArrow1.SetActive(false);
                SpriteArrow2.SetActive(false);
                SpriteArrow3.SetActive(false);
                SpriteArrow4.SetActive(false);
                SpriteArrow5.SetActive(false);
                SpriteArrow6.SetActive(true);
                SpriteArrow7.SetActive(false);
                SpriteArrow8.SetActive(false);
                break;
            case 7:
                SpriteArrow1.SetActive(false);
                SpriteArrow2.SetActive(false);
                SpriteArrow3.SetActive(false);
                SpriteArrow4.SetActive(false);
                SpriteArrow5.SetActive(false);
                SpriteArrow6.SetActive(false);
                SpriteArrow7.SetActive(true);
                SpriteArrow8.SetActive(false);
                break;
            case 8:
                SpriteArrow1.SetActive(false);
                SpriteArrow2.SetActive(false);
                SpriteArrow3.SetActive(false);
                SpriteArrow4.SetActive(false);
                SpriteArrow5.SetActive(false);
                SpriteArrow6.SetActive(false);
                SpriteArrow7.SetActive(false);
                SpriteArrow8.SetActive(true);
                break;
        }
    }
}
