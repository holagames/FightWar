using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordEventFight : MonoBehaviour
{
    public UILabel Gatename;
    public GameObject UiGird;
    public GameObject Item;
    private List<GameObject> ItemList = new List<GameObject>();
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;
    public UIAtlas GrabAtlas;

    public GameObject SpriteArrow1;
    public GameObject SpriteArrow2;
    public GameObject SpriteArrow3;
    public GameObject SpriteArrow4;
    public GameObject SpriteArrow5;
    public GameObject SpriteArrow6;
    public GameObject SpriteArrow7;
    public GameObject SpriteArrow8;

    public GameObject EnemyIcon;

    public UILabel LabelVictory1;
    //public UILabel LabelVictory2;
    //public UILabel LabelVictory3;
    public UILabel LabelVictory;

    public GameObject BgCollider;
    public UILabel TimerLabel;
    public UILabel PowerNum;
    public UILabel LabelPrompt;
    int OneTime;

    private List<int> ItemIdList = new List<int>();

    private List<string> ItemIdNum = new List<string>();
    // Use this for initialization
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.世界事件);
        UIManager.instance.UpdateSystems(UIManager.Systems.世界事件);

        NetworkHandler.instance.SendProcess("2201#;");
        if (UIEventListener.Get(BgCollider).onClick == null)
        {
            UIEventListener.Get(BgCollider).onClick = delegate(GameObject go)
            {
                //if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) > 12)
                {
                    gameObject.SetActive(false);
                    GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().CloseTopContent();
                    if (GameObject.Find("MapMask") != null)
                    {
                        GameObject.Find("MapMask").SetActive(false);
                    }
                }
            };
        }
    }

    public void SetInfo(int Id, List<WordItem> WordItem, int color)
    {
        GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().ShowTopContent();
        AddItemIdList(TextTranslator.instance.GetGateByID(Id));
        SceneTransformer.instance.NowGateID = Id;
        GameObject.Find("WordEvent").GetComponent<WordEvent>().WorldId = Id;
        LabelPrompt.gameObject.SetActive(false);

        GameObject.Find("WordEvent").transform.Find("WordWindow").transform.Find("UpLabe").transform.Find("Label").GetComponent<UILabel>().text = TextTranslator.instance.GetWorldEventNameByID(Id);
        Gatename.text = TextTranslator.instance.GetWorldEventNameByID(TextTranslator.instance.GetWorldEventByGateID(Id).WorldEventId);
        TextTranslator.Gate _gate = TextTranslator.instance.GetGateByID(Id);
        PowerNum.text = _gate.needForce.ToString();
        //GameObject.Find("WordEvent").transform.Find("WorldEvenWindow").transform.Find("LabelTerm").transform.Find("Term1").GetComponent<UILabel>().text = "1." + _gate.description;
        for (int i = 0; i < ItemList.Count; i++)
        {
            DestroyImmediate(ItemList[i]);
        }
        ItemList.Clear();
        EnemyIcon.GetComponent<UITexture>().mainTexture = Resources.Load("Loading/" + _gate.icon.ToString()) as Texture;
        EnemyIcon.GetComponent<UITexture>().MakePixelPerfect();
        EnemyIcon.GetComponent<UITexture>().width = EnemyIcon.GetComponent<UITexture>().width / 20 * 17;
        EnemyIcon.GetComponent<UITexture>().height = EnemyIcon.GetComponent<UITexture>().height / 20 * 17;
        //for (int i = 0; i < WordItem.Count; i++)
        //{
        //    GameObject obj = Instantiate(Item) as GameObject;
        //    obj.transform.parent = UiGird.transform;
        //    obj.transform.localScale = Vector3.one;
        //    obj.transform.localPosition = Vector3.zero;
        //    obj.SetActive(true);
        //    obj.GetComponent<ItemExplanation>().SetAwardItem(WordItem[i].ItemId, 0);
        //    if (WordItem[i].ItemId > 40000 && WordItem[i].ItemId < 50000)
        //    {
        //        obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
        //        obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (WordItem[i].ItemId - 10000).ToString();
        //        TextTranslator.instance.GetItemByItemCode(WordItem[i].ItemId - 10000);
        //        obj.transform.Find("Number").GetComponent<UILabel>().text = WordItem[i].ItemNumber.ToString();
        //        obj.transform.Find("SuiPian").gameObject.SetActive(true);
        //    }
        //    else if (WordItem[i].ItemId > 80000)
        //    {
        //        obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
        //        obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = ((WordItem[i].ItemId - 30000) / 10 * 10).ToString();
        //        obj.transform.Find("Number").GetComponent<UILabel>().text = WordItem[i].ItemNumber.ToString();
        //        obj.transform.Find("SuiPian").gameObject.SetActive(true);
        //    }
        //    else if (WordItem[i].ItemId > 70000 && WordItem[i].ItemId < 79999)
        //    {
        //        obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
        //        obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (WordItem[i].ItemId - 10000).ToString();
        //        obj.transform.Find("Number").GetComponent<UILabel>().text = WordItem[i].ItemNumber.ToString();
        //        obj.transform.Find("SuiPian").gameObject.SetActive(true);
        //    }
        //    else
        //    {
        //        obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
        //        obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = WordItem[i].ItemId.ToString();
        //        obj.transform.Find("Number").GetComponent<UILabel>().text = WordItem[i].ItemNumber.ToString();
        //        obj.transform.Find("SuiPian").gameObject.SetActive(false);
        //    }
        //    TextTranslator.ItemInfo item = TextTranslator.instance.GetItemByItemCode(WordItem[i].ItemId);
        //    if (WordItem[i].ItemId != 0)
        //    {
        //        SetGrade(obj.transform.FindChild("Bg").gameObject, item.itemGrade);
        //    }
        //    else
        //    {
        //        //SetColor(obj.transform.FindChild("Bg").gameObject, 5);
        //        SetGrade(obj.transform.FindChild("Bg").gameObject, 5);
        //        obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = GrabAtlas;
        //        obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = "randomButton";
        //    }
        //    UiGird.GetComponent<UIGrid>().repositionNow = true;
        //    ItemList.Add(obj);
        //}
        SetItemInfo(WordItem);
        SetColor(transform.Find("LeftContent").transform.Find("HeroIcon").gameObject, color);
        StartCoroutine(ShowDescription());
    }

    private void AddItemIdList(TextTranslator.Gate MapInfo)
    {
        ItemIdList.Clear();
        ItemIdNum.Clear();
        if (MapInfo.itemID1 != 0)
        {
            ItemIdList.Add(MapInfo.itemID1);
            ItemIdNum.Add(MapInfo.itemNum1);
        }
        if (MapInfo.itemID2 != 0)
        {
            ItemIdList.Add(MapInfo.itemID2);
            ItemIdNum.Add(MapInfo.itemNum2);
        }
        if (MapInfo.itemID3 != 0)
        {
            ItemIdList.Add(MapInfo.itemID3);
            ItemIdNum.Add(MapInfo.itemNum3);
        }
        if (MapInfo.itemID4 != 0)
        {
            ItemIdList.Add(MapInfo.itemID4);
            ItemIdNum.Add(MapInfo.itemNum4);
        }
        if (MapInfo.itemID5 != 0)
        {
            ItemIdList.Add(MapInfo.itemID5);
            ItemIdNum.Add(MapInfo.itemNum5);
        }
    }

    void SetItemInfo(List<WordItem> WordItem)
    {
        string _num = "0";
        for (int i = 0; i < ItemIdList.Count; i++)
        {
            GameObject obj = Instantiate(Item) as GameObject;
            obj.transform.parent = UiGird.transform;
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = Vector3.zero;
            obj.SetActive(true);
            
            _num = ItemIdNum[i].Split('$')[1];
            if (ItemIdList[i] < 10000)
            {
                ItemIdList[i] = WordItem[i].ItemId;
                obj.GetComponent<ItemExplanation>().SetAwardItem(WordItem[i].ItemId, 0);
            }
            else
            {
                obj.GetComponent<ItemExplanation>().SetAwardItem(ItemIdList[i], 0);
            }
            if (ItemIdList[i] > 40000 && ItemIdList[i] < 50000)
            {
                obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (ItemIdList[i] - 10000).ToString();
                TextTranslator.instance.GetItemByItemCode(ItemIdList[i] - 10000);
                obj.transform.Find("Number").GetComponent<UILabel>().text = _num;
                obj.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (ItemIdList[i] > 80000 && ItemIdList[i] < 90000)
            {
                obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = ((ItemIdList[i] - 30000) / 10 * 10).ToString();
                obj.transform.Find("Number").GetComponent<UILabel>().text = _num;
                obj.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (ItemIdList[i] > 70000 && ItemIdList[i] < 79999)
            {
                obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
                obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (ItemIdList[i] - 10000).ToString();
                obj.transform.Find("Number").GetComponent<UILabel>().text = _num;
                obj.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else
            {
                obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = ItemIdList[i].ToString();
                obj.transform.Find("Number").GetComponent<UILabel>().text = _num;
                obj.transform.Find("SuiPian").gameObject.SetActive(false);
            }
            TextTranslator.ItemInfo item = TextTranslator.instance.GetItemByItemCode(ItemIdList[i]);
            if (ItemIdList[i] != 0)
            {
                SetGrade(obj.transform.FindChild("Bg").gameObject, item.itemGrade);
            }
            else
            {
                //SetColor(obj.transform.FindChild("Bg").gameObject, 5);
                SetGrade(obj.transform.FindChild("Bg").gameObject, 5);
                obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = GrabAtlas;
                obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = "randomButton";
            }

            if(WordItem[0].ItemId == 10906)
            {
                LabelPrompt.gameObject.SetActive(true);
            }

            UiGird.GetComponent<UIGrid>().repositionNow = true;
            ItemList.Add(obj);
        }
    }

    void SetColor(GameObject go, int color)
    {
        switch (color)
        {
            //case 0:
            //    go.GetComponent<UISprite>().spriteName = "";
            //    break;
            case 1:
                go.transform.Find("HeroName").GetComponent<UILabel>().color = new Color(128f / 255f, 128f / 255f, 128f / 255f);
                break;
            case 2:
                //go.GetComponent<UISprite>().spriteName = "Grade1";
                go.transform.Find("HeroName").GetComponent<UILabel>().color = new Color(0 / 255, 255f / 255f, 44f / 255f);
                break;
            case 3:
                //go.GetComponent<UISprite>().spriteName = "Grade2";
                go.transform.Find("HeroName").GetComponent<UILabel>().color = new Color(0 / 255, 128f / 255f, 255f / 255f);
                break;
            case 4:
                //go.GetComponent<UISprite>().spriteName = "Grade3";
                go.transform.Find("HeroName").GetComponent<UILabel>().color = new Color(83f / 255f, 0f / 255f, 255f / 255f);
                break;
            case 5:
                //go.GetComponent<UISprite>().spriteName = "Grade4";
                go.transform.Find("HeroName").GetComponent<UILabel>().color = new Color(255f / 255f, 121f / 255f, 0 / 255);
                break;
            case 6:
                //go.GetComponent<UISprite>().spriteName = "Grade5";
                go.transform.Find("HeroName").GetComponent<UILabel>().color = new Color(255 / 255, 0 / 255, 0 / 255);
                break;
        }
    }

    void SetGrade(GameObject go, int color)
    {
        switch (color)
        {
            case 1:
                go.GetComponent<UISprite>().spriteName = "Grade1";
                //go.transform.parent.Find("Label").GetComponent<UILabel>().color = Color.green;
                break;
            case 2:
                go.GetComponent<UISprite>().spriteName = "Grade2";
                //go.transform.parent.Find("Label").GetComponent<UILabel>().color = Color.blue;
                break;
            case 3:
                go.GetComponent<UISprite>().spriteName = "Grade3";
                //go.transform.parent.Find("Label").GetComponent<UILabel>().color = new Color(255 / 255, 0 / 255, 255 / 255);
                break;
            case 4:
                go.GetComponent<UISprite>().spriteName = "Grade4";
                //go.transform.parent.Find("Label").GetComponent<UILabel>().color = Color.yellow;
                break;
            case 5:
                go.GetComponent<UISprite>().spriteName = "Grade5";
                //go.transform.parent.Find("Label").GetComponent<UILabel>().color = Color.red;
                break;
            case 6:
                go.GetComponent<UISprite>().spriteName = "Grade6";
                //go.transform.parent.Find("Label").GetComponent<UILabel>().color = Color.red;
                break;
            case 7:
                go.GetComponent<UISprite>().spriteName = "Grade7";
                //go.transform.parent.Find("Label").GetComponent<UILabel>().color = Color.red;
                break;
        }
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

    public void SetFightTime()
    {
        string[] dataInfo = CharacterRecorder.instance.worldEventList.Split(';');
        OneTime = int.Parse(dataInfo[1]);
        CancelInvoke("UpdateTime");
        InvokeRepeating("UpdateTime",0,1.0f);
    }

    void UpdateTime()
    {
        if (OneTime > 0)
        {
            int fenNum = (int)((OneTime / 60) % 60);
            int miao = (int)(OneTime % 60);
            int day = (int)((OneTime / 60) / 60);
            TimerLabel.text = ((day < 10) ? "0" : "") + day.ToString() + ":" + ((fenNum < 10) ? "0" : "") + fenNum.ToString() + ":" + ((miao < 10) ? "0" : "") + miao.ToString();
            OneTime -= 1;

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
}
