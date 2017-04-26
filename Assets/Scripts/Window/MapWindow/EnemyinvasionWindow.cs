using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyinvasionWindow : MonoBehaviour {


    public UILabel HeroName;
    public UILabel HeroLevel;
    public UILabel HeroPower;
    public UILabel LabelVictory1;
    public UIGrid MyGrid;
    public GameObject EnemyItem;
    public UITexture EnemyIcon;
    public GameObject StartButton;
    public GameObject SpriteArrow1;
    public GameObject SpriteArrow2;
    public GameObject SpriteArrow3;
    public GameObject SpriteArrow4;
    public GameObject SpriteArrow5;
    public GameObject SpriteArrow6;
    public GameObject SpriteArrow7;
    public GameObject SpriteArrow8;
    public GameObject BgColider;

    public GameObject ItemObj;
    public UIGrid ItemGrid;
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;

    int LoadSize = 15;
    int MapId = 0;

    private List<int> ItemIdList = new List<int>();
    private List<int> ItemNumList = new List<int>();
    private List<GameObject> ItemObjList = new List<GameObject>();
	// Use this for initialization
	void Start () {
        UIManager.instance.CountSystem(UIManager.Systems.世界事件);
        UIManager.instance.UpdateSystems(UIManager.Systems.世界事件);

        UIEventListener.Get(BgColider).onClick = delegate(GameObject go)
        {
            gameObject.SetActive(false);
            GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().CloseTopContent();
            if (GameObject.Find("MapMask") != null)
            {
                GameObject.Find("MapMask").SetActive(false);
            }
        };
        UIEventListener.Get(StartButton).onClick = delegate(GameObject go)
        {
            if (CharacterRecorder.instance.WorldEventFightCount >= 10)
            {
                UIManager.instance.OpenPromptWindow("今天的世界事件完成达到上限，明天再来哦", PromptWindow.PromptType.Hint, null, null);
                return;
            }
            if (CharacterRecorder.instance.sprite < 2)
            {
                UIManager.instance.OpenPromptWindow("精力不足，是否购买精力药剂?", PromptWindow.PromptType.Confirm, OpenBuyWindow, null);
                return;
            }
            if (CharacterRecorder.instance.EnemyInfoStr == "" || CharacterRecorder.instance.EnemyInfoStr == null || MapId == 0)
            {
                UIManager.instance.OpenPromptWindow("请重新打开该页面！", PromptWindow.PromptType.Hint, null, null);
                return;
            }
            UIManager.instance.OpenPanel("LoadingWindow", true);
            if (GameObject.Find("MapObject") != null)
            {
                DestroyImmediate(GameObject.Find("MapObject"));
            }
            UIManager.instance.CurGateId = (MapId).ToString();
            UIManager.instance.StartGate(UIManager.GateAnchorName.gateFight);
            UIManager.instance.MapGateInfoLoading = true;
            PictureCreater.instance.StartFight();
            PictureCreater.instance.FightStyle = 4;
        };
	}

    void OpenBuyWindow()
    {
        UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
        NetworkHandler.instance.SendProcess("5012#10702;");
    }

    public void Init(int _MapId)
    {
        GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().ShowTopContent();
        MapId = TextTranslator.instance.GetWorldEventByID(_MapId).GatePoint;
        //if (CharacterRecorder.instance.EnemyInfoStr != null && CharacterRecorder.instance.EnemyInfoStr != "")
        //{
            
        //}
        StartCoroutine(delateHandle());
    }

    public IEnumerator delateHandle()
    {
        yield return new WaitForSeconds(0.1f);
        if (CharacterRecorder.instance.EnemyInfoStr == null)
        {
            StartCoroutine(delateHandle());
        }
        else if(CharacterRecorder.instance.EnemyInfoStr == "")
        {
            StartCoroutine(delateHandle());
        }
        else
        {
            SceneTransformer.instance.NowGateID = MapId;
            string[] dataSplit = CharacterRecorder.instance.EnemyInfoStr.Split(';');
            HeroName.text = dataSplit[0];
            HeroLevel.text = "Lv." + dataSplit[1];
            HeroPower.text = dataSplit[3];
            EnemyIcon.mainTexture = Resources.Load("Loading/" + dataSplit[2]) as Texture;
            EnemyIcon.MakePixelPerfect();
            EnemyIcon.width = EnemyIcon.width / 20 * LoadSize;
            EnemyIcon.height = EnemyIcon.height / 20 * LoadSize;
            int _deleteNum = MyGrid.transform.childCount;
            for (int i = 0; i < _deleteNum; i++)
            {
                DestroyImmediate(MyGrid.transform.GetChild(0).gameObject);
            }
            for (int i = 4; i < dataSplit.Length - 1; i++)
            {
                SetEnemyTeam(dataSplit[i]);
            }
            InitItemList(MapId);
            SetItemInfo();
            MyGrid.Reposition();
            StartCoroutine(ShowDescription());
        }
    }

    void SetEnemyTeam(string _info)
    {
        string[] dataSplit = _info.Split('$');
        CreateEnemyItem(int.Parse(dataSplit[0]),int.Parse(dataSplit[14]));
    }

    void CreateEnemyItem(int HeroId,int Color)
    {
        GameObject go = NGUITools.AddChild(MyGrid.gameObject, EnemyItem);
        go.name = "EnemyItem" + HeroId;
        go.SetActive(true);
        SetEnemyItem(go,HeroId,Color);
    }

    void SetEnemyItem(GameObject go, int HeroId, int Color)
    {
        //go.transform.Find("Bg").GetComponent<UISprite>().spriteName = "Grade" + Color;
        go.transform.Find("Icon").GetComponent<UISprite>().spriteName = HeroId.ToString();
        SetHeroNameColor(go.transform.Find("Bg").GetComponent<UISprite>(), Color);
    }

    public void SetArrow(int Direction)
    {
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

    void SetHeroNameColor(UISprite frame, int ClassNumber)
    {
        switch (ClassNumber)
        {
            case 1:
                //白色
                frame.spriteName = "yxdi0";
                break;
            case 2:
            //绿色
            //frame.spriteName = "yxdi1";
            //break;
            case 3:
            //frame.spriteName = "yxdi1";
            //break;
            case 4:
                frame.spriteName = "yxdi1";
                break;
            case 5:
            //蓝色
            case 6:
            case 7:
            case 8:
                frame.spriteName = "yxdi2";
                break;
            case 9:
            //紫色
            case 10:
            case 11:
            case 12:
            case 13:
                frame.spriteName = "yxdi3";
                break;
            case 14:
            //橙色
            case 15:
            case 16:
            case 17:
            case 18:
                frame.spriteName = "yxdi4";
                break;
            case 19:
            //红色
            case 20:
                frame.spriteName = "yxdi5";
                break;
            default:
                break;
        }
    }

    ///////////////////////////////以下设置奖励/////////////////////////
    void SetItemInfo()
    {
        for(int i = 0;i< ItemIdList.Count;i++)
        {
            GameObject go = NGUITools.AddChild(ItemGrid.gameObject, ItemObj) as GameObject;
            go.name = "Item" + (i + 1);
            go.transform.localScale = Vector3.one;
            go.transform.localPosition = Vector3.zero;
            go.SetActive(true);

            go.GetComponent<ItemExplanation>().SetAwardItem(ItemIdList[i], 0);

            ItemObjList.Add(go);
            ShowItemIcon(go, ItemIdList[i], ItemNumList[i]);
        }
    }

    void InitItemList(int id)
    {
        TextTranslator.Gate _gate = TextTranslator.instance.GetGateByID(id);
        string[] _itemNum = null;
        if (_gate.itemID1 != 0 && _gate.itemID1 != null)
        {
            _itemNum = _gate.itemNum1.Split('$');
            if(_itemNum != null && int.Parse(_itemNum[1]) > 0)
            {
                ItemIdList.Add(_gate.itemID1);
                ItemNumList.Add(int.Parse(_itemNum[1]));
            }
        }
        if (_gate.itemID2 != 0 && _gate.itemID2 != null)
        {
            _itemNum = _gate.itemNum2.Split('$');
            if (_itemNum != null && int.Parse(_itemNum[1]) > 0)
            {
                ItemIdList.Add(_gate.itemID2);
                ItemNumList.Add(int.Parse(_itemNum[1]));
            }
        }
        if (_gate.itemID3 != 0 && _gate.itemID3 != null)
        {
            _itemNum = _gate.itemNum3.Split('$');
            if (_itemNum != null && int.Parse(_itemNum[1]) > 0)
            {
                ItemIdList.Add(_gate.itemID3);
                ItemNumList.Add(int.Parse(_itemNum[1]));
            }
        }
        if (_gate.itemID4 != 0 && _gate.itemID4 != null)
        {
            _itemNum = _gate.itemNum4.Split('$');
            if (_itemNum != null && int.Parse(_itemNum[1]) > 0)
            {
                ItemIdList.Add(_gate.itemID4);
                ItemNumList.Add(int.Parse(_itemNum[1]));
            }
        }
        if (_gate.itemID5 != 0 && _gate.itemID5 != null)
        {
            _itemNum = _gate.itemNum5.Split('$');
            if (_itemNum != null && int.Parse(_itemNum[1]) > 0)
            {
                ItemIdList.Add(_gate.itemID5);
                ItemNumList.Add(int.Parse(_itemNum[1]));
            }
        }
        if (_gate.itemID6 != 0 && _gate.itemID6 != null)
        {
            _itemNum = _gate.itemNum6.Split('$');
            if (_itemNum != null && int.Parse(_itemNum[1]) > 0)
            {
                ItemIdList.Add(_gate.itemID6);
                ItemNumList.Add(int.Parse(_itemNum[1]));
            }
        }
    }

    private void ShowItemIcon(GameObject obj, int itemId, int _itemNum)
    {
        TextTranslator.ItemInfo item = TextTranslator.instance.GetItemByItemCode(itemId);
        if (itemId > 40000 && itemId < 50000)
        {
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (itemId - 10000).ToString();
            obj.transform.Find("Number").gameObject.GetComponent<UILabel>().text = _itemNum.ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(true);
        }
        else if (itemId > 80000)
        {
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (itemId - 30000).ToString();
            obj.transform.Find("Number").gameObject.GetComponent<UILabel>().text = _itemNum.ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(true);
        }
        else if (itemId > 70000 && itemId < 79999)
        {
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (itemId - 10000).ToString();
            obj.transform.Find("Number").gameObject.GetComponent<UILabel>().text = _itemNum.ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(true);
        }
        else
        {
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            if(item.FuncType == 17)
            {
                obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = item.picID.ToString();
            }
            else
            {
                obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = itemId.ToString();
            }
            obj.transform.Find("Number").gameObject.GetComponent<UILabel>().text = _itemNum.ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(false);
        }

        SetColor(obj.transform.Find("Bg").gameObject, item.itemGrade);
    }

    void SetColor(GameObject go, int color)
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

    IEnumerator ShowDescription()
    {
        LabelVictory1.text = "";
        //LabelVictory2.text = "";
        //LabelVictory3.text = "";
        //LabelVictory.text = "";

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
        //LabelVictory.text = "";

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
        //foreach (var c in LabelVictoryText)
        //{
        //    LabelVictory.text += c;
        //    yield return new WaitForSeconds(0.02f);
        //}
        if (GameObject.Find("AutoButton/Sprite/SpriteXianding") != null)
        {
            GameObject.Find("AutoButton/Sprite/SpriteXianding").GetComponent<TweenScale>().enabled = true;
            GameObject.Find("AutoButton/Sprite/SpriteXianding").GetComponent<TweenScale>().ResetToBeginning();
        }
    }
}
