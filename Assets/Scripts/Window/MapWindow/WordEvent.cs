using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WordEvent : MonoBehaviour {
    public UILabel TimerLabel;
    public int OneTime; 
    public GameObject Grid;
    public GameObject EventItem;
    private List<GameObject> ItemList = new List<GameObject>();
    public GameObject WordEventIdObj;
    public int WorldId = 0;
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;
    public UIAtlas GrabAtlas;
    public int ActionEventId;

    public UILabel LabelRefreshCost;
    public UILabel FightCount;

    int HighQCMissionNum = 0;
    bool IsPlay = true;
	// Use this for initialization
	void Start () {
        //if (UIEventListener.Get(GameObject.Find("WordEvent").transform.Find("WordWindow").Find("Right").Find("FightButton").gameObject).onClick == null)
        //{
        //    UIEventListener.Get(GameObject.Find("WordEvent").transform.Find("WordWindow").Find("Right").Find("FightButton").gameObject).onClick += delegate(GameObject obj)
        //    {
        //        UIManager.instance.OpenPanel("LoadingWindow", true);
        //        if (GameObject.Find("MapObject") != null)
        //        {
        //            DestroyImmediate(GameObject.Find("MapObject"));
        //        }
        //        PictureCreater.instance.StartFight();
        //        string HeroPosition = "";
        //        foreach (var h in CharacterRecorder.instance.ownedHeroList)
        //        {
        //            if (h.position > 0)
        //            {
        //                HeroPosition += h.characterRoleID.ToString() + "$";
        //            }
        //        }
        //        NetworkHandler.instance.SendProcess("2203#"+WorldId+";"+HeroPosition+";");
        //    };
        //}
        CountWordListPos();     //世界时间列表自适应
        Transform _updateButton = this.transform.FindChild("EventList/UpdateButton");
        if (UIEventListener.Get(_updateButton.gameObject).onClick == null)
        {
            UIEventListener.Get(_updateButton.gameObject).onClick += delegate(GameObject obj)
            {
                if (HighQCMissionNum > 0)
                {
                    UIManager.instance.OpenPromptWindow("有高品质任务，是否刷新世界事件？", PromptWindow.PromptType.Confirm, UpdateWorldEvent, null);
                }
                else
                    UpdateWorldEvent();
            };
        }

        //if (UIEventListener.Get(GameObject.Find("WordEvent").transform.Find("WorldEvenWindow").Find("BottomContect").Find("StartBtn").gameObject).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("WordEvent").transform.Find("WorldEvenWindow").Find("BottomContect").Find("StartBtn").gameObject).onClick += delegate(GameObject obj)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_3304);
                if (CharacterRecorder.instance.WorldEventFightCount>=10)
                {
                    UIManager.instance.OpenPromptWindow("今天的世界事件完成达到上限，明天再来哦", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                if(CharacterRecorder.instance.sprite < 2)
                {
                    UIManager.instance.OpenPromptWindow("精力不足，是否购买精力药剂?", PromptWindow.PromptType.Confirm, OpenBuyWindow, null);
                    return;
                }
                UIManager.instance.OpenPanel("LoadingWindow", true);
                if (GameObject.Find("MapObject") != null)
                {
                    DestroyImmediate(GameObject.Find("MapObject"));
                }
                if (CharacterRecorder.instance.GuideID[18] == 4)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
                UIManager.instance.CurGateId = (WorldId).ToString();
                UIManager.instance.StartGate(UIManager.GateAnchorName.gateFight);
                UIManager.instance.MapGateInfoLoading = true;
                PictureCreater.instance.StartFight();
                PictureCreater.instance.FightStyle = 4;
            };
        }
	}

    void CountWordListPos()
    {
        UIRoot root = GameObject.FindObjectOfType<UIRoot>();
        if (root != null)
        {
            Transform _eventList = this.transform.Find("EventList");
            float s = (float)root.activeHeight / Screen.height;
            int width = Mathf.CeilToInt(Screen.width * s);

            _eventList.localPosition = new Vector3(width / 2f - _eventList.localPosition.x + 40, _eventList.localPosition.y);
        }
    }

    void OpenBuyWindow()
    {
        UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
        NetworkHandler.instance.SendProcess("5012#10702;");
    }
	
    public void AddSignEventItem(int _eventID)
    {
        GameObject obj = Instantiate(EventItem) as GameObject;
        obj.name = "WorldEvent_Item";
        obj.transform.parent = Grid.transform;
        obj.transform.localScale = new Vector3(1.2f, 1.2f);
        obj.transform.localPosition = Vector3.zero;
        ActionEvent _item = TextTranslator.instance.GetActionEventById(_eventID);
        obj.transform.Find("Label").GetComponent<UILabel>().text = _item.GateName;
        if (_item.BonusID1 > 40000 && _item.BonusID1 < 50000)
        {
            obj.transform.Find("HeroIcon").Find("Sprite").GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.Find("HeroIcon").Find("Sprite").GetComponent<UISprite>().spriteName = (_item.BonusID1 - 10000).ToString();
            obj.transform.Find("HeroIcon").Find("SuiPian").gameObject.SetActive(false);
        }
        else if (_item.BonusID1 > 80000 && _item.BonusID1 <90000)
        {
            obj.transform.Find("HeroIcon").Find("Sprite").GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.Find("HeroIcon").Find("Sprite").GetComponent<UISprite>().spriteName = ((_item.BonusID1 - 30000) / 10 * 10).ToString();
            obj.transform.Find("HeroIcon").Find("SuiPian").gameObject.SetActive(true);
        }
        else if (_item.BonusID1 > 70000 && _item.BonusID1 < 79999)
        {
            obj.transform.Find("HeroIcon").Find("Sprite").GetComponent<UISprite>().atlas = RoleAtlas;

            obj.transform.Find("HeroIcon").Find("Sprite").GetComponent<UISprite>().spriteName = (_item.BonusID1 - 10000).ToString();
            obj.transform.Find("HeroIcon").Find("SuiPian").gameObject.SetActive(true);
        }
        else
        {
            obj.transform.Find("HeroIcon").Find("Sprite").GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.Find("HeroIcon").Find("Sprite").GetComponent<UISprite>().spriteName = _item.BonusID1.ToString();
            obj.transform.Find("HeroIcon").Find("SuiPian").gameObject.SetActive(false);
        }
        obj.GetComponent<WordEventItem>().WorldId = _eventID;
        obj.transform.Find("HeroIcon").gameObject.transform.parent.Find("Label").GetComponent<UILabel>().color = new Color(147 / 255f, 216 / 255f, 243 / 255f);
        obj.transform.Find("HeroIcon").gameObject.transform.parent.Find("Sprite").GetComponent<UITexture>().mainTexture = Resources.Load("Game/SJ_daditu_4") as Texture;
        obj.transform.Find("HeroIcon").gameObject.transform.parent.Find("Sprite").GetComponent<UITexture>().MakePixelPerfect();
        obj.transform.Find("HeroIcon").gameObject.transform.parent.Find("Sprite").GetComponent<UITexture>().width = 234;

        //添加金框
        UITexture _BgKuang = NGUITools.AddWidget<UITexture>(obj.transform.Find("HeroIcon").gameObject.transform.parent.gameObject);
        _BgKuang.name = "BgKuang";
        _BgKuang.mainTexture = Resources.Load("Game/SJ_daditu_5") as Texture;
        _BgKuang.MakePixelPerfect();
        _BgKuang.depth = 3;
        _BgKuang.transform.localPosition = new Vector3(16f,3f);
        
        TextTranslator.ItemInfo _temp = TextTranslator.instance.GetItemByItemCode(_item.BonusID1);
        SetGrade(obj.transform.Find("HeroIcon").gameObject, _temp.itemGrade);
        SetColor(obj.transform.Find("HeroIcon").gameObject, 6);
        ItemList.Add(obj);
        UIEventListener.Get(obj).onClick = ActionButtonClick;
        //Grid.GetComponent<UIGrid>().Reposition();
    }

    public void setInfo(WorldEventList worldlits)
    {
        ChangeLabelColor();
        LabelRefreshCost.text = CharacterRecorder.instance.WorldEventRefreshCost.ToString();
        FightCount.text = (10 - CharacterRecorder.instance.WorldEventFightCount).ToString();
        WorldEventList worldList;
        worldList = worldlits;// GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().worldList;
        OneTime = worldList.TimerNumber;
        CancelInvoke("UpdateTime");
        InvokeRepeating("UpdateTime", 0, 1f);
        ClearWorldEventList();
        if (ActionEventId != 0)
        {
            AddSignEventItem(ActionEventId);
        }
        for (int i = 0; i < worldList.WorldEventInfo.Count; i++) {
            GameObject obj = Instantiate(EventItem) as GameObject;
            obj.name = "WorldEvent_Item" + i;
            obj.transform.parent = Grid.transform;
            obj.transform.localScale = new Vector3(1.2f,1.2f);
            obj.transform.localPosition = Vector3.zero;
            //obj.GetComponent<TweenPosition>().from = new Vector3(200,);
            //obj.SetActive(true);

            obj.transform.Find("Label").GetComponent<UILabel>().text = TextTranslator.instance.GetWorldEventNameByID(worldList.WorldEventInfo[i].WorldId);
            //obj.transform.Find("Label").GetComponent<UILabel>().color = 
            if (worldList.WorldEventInfo[i].WorldItem > 40000 && worldList.WorldEventInfo[i].WorldItem < 50000)
            {
                obj.transform.Find("HeroIcon").Find("Sprite").GetComponent<UISprite>().atlas = ItemAtlas; 
                obj.transform.Find("HeroIcon").Find("Sprite").GetComponent<UISprite>().spriteName = (worldList.WorldEventInfo[i].WorldItem - 10000).ToString();
                obj.transform.Find("HeroIcon").Find("SuiPian").gameObject.SetActive(false);
            }
            else if (worldList.WorldEventInfo[i].WorldItem > 80000)
            {
                obj.transform.Find("HeroIcon").Find("Sprite").GetComponent<UISprite>().atlas = ItemAtlas;
                obj.transform.Find("HeroIcon").Find("Sprite").GetComponent<UISprite>().spriteName = ((worldList.WorldEventInfo[i].WorldItem - 30000)/10 * 10).ToString();
                obj.transform.Find("HeroIcon").Find("SuiPian").gameObject.SetActive(true);
            }
            else if (worldList.WorldEventInfo[i].WorldItem > 70000 && worldList.WorldEventInfo[i].WorldItem < 79999)
            {
                obj.transform.Find("HeroIcon").Find("Sprite").GetComponent<UISprite>().atlas = RoleAtlas;
          
                obj.transform.Find("HeroIcon").Find("Sprite").GetComponent<UISprite>().spriteName = (worldList.WorldEventInfo[i].WorldItem - 10000).ToString();
                obj.transform.Find("HeroIcon").Find("SuiPian").gameObject.SetActive(true);
            }
            else if(worldList.WorldEventInfo[i].WorldItem > 20000 && worldList.WorldEventInfo[i].WorldItem < 30000)
            {
                obj.transform.Find("HeroIcon").Find("Sprite").GetComponent<UISprite>().atlas = ItemAtlas;
                obj.transform.Find("HeroIcon").Find("Sprite").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(worldList.WorldEventInfo[i].WorldItem).picID.ToString();
                obj.transform.Find("HeroIcon").Find("SuiPian").gameObject.SetActive(false);
            }
            else
            {
                obj.transform.Find("HeroIcon").Find("Sprite").GetComponent<UISprite>().atlas = ItemAtlas;
                obj.transform.Find("HeroIcon").Find("Sprite").GetComponent<UISprite>().spriteName = worldList.WorldEventInfo[i].WorldItem.ToString();
                obj.transform.Find("HeroIcon").Find("SuiPian").gameObject.SetActive(false);
            }
            
            obj.GetComponent<WordEventItem>().WorldId =worldList.WorldEventInfo[i].WorldId;
            //if (i == 0)
            //{
            //    //obj.transform.Find("HeroIcon").gameObject.transform.parent.Find("Label").GetComponent<UILabel>().color = new Color(80/255f,232/255f,255/255f);
            //    //obj.transform.Find("HeroIcon").gameObject.transform.parent.Find("Sprite").GetComponent<UISprite>().spriteName = "ui_shijie_di2 ";
            //}
            //else {
            obj.transform.Find("HeroIcon").gameObject.transform.parent.Find("Label").GetComponent<UILabel>().color =new Color(147/255f,216/255f,243/255f);
            obj.transform.Find("HeroIcon").gameObject.transform.parent.Find("Sprite").GetComponent<UITexture>().mainTexture = Resources.Load("Game/SJ_daditu_2") as Texture;
            //}
            TextTranslator.ItemInfo item = TextTranslator.instance.GetItemByItemCode(worldList.WorldEventInfo[i].WorldItem);
            if (worldList.WorldEventInfo[i].WorldItem != 0)
            {
                SetColor(obj.transform.Find("HeroIcon").gameObject, worldList.WorldEventInfo[i].WorldColor);
                SetGrade(obj.transform.Find("HeroIcon").gameObject, item.itemGrade);
            }
            else
            {
                SetGrade(obj.transform.Find("HeroIcon").gameObject, 5);
                obj.transform.Find("HeroIcon").Find("Sprite").GetComponent<UISprite>().atlas = GrabAtlas;
                obj.transform.Find("HeroIcon").Find("Sprite").GetComponent<UISprite>().spriteName = "randomButton";
            }
            ItemList.Add(obj);
            UIEventListener.Get(obj).onClick = ButtonOnClick;
        }
        Grid.GetComponent<UIGrid>().Reposition();
        StartCoroutine(ShowEventList());
    }

    void ChangeLabelColor()
    {
        if(CharacterRecorder.instance.WorldEventFightCount < 10)
        {
            FightCount.color = new Color(0 / 255f, 255 / 255f, 76 / 255f);
        }
        else
        {
            FightCount.color = new Color(255 / 255f, 0 / 255f, 0 / 255f);
        }
    }

    IEnumerator ShowEventList()
    {
        for (int i = 0; i < Grid.transform.childCount; i++)
        {
            if(IsPlay)
            {
                Transform child = Grid.transform.GetChild(i);
                child.GetComponent<TweenPosition>().from = new Vector3(200, child.localPosition.y);
                child.GetComponent<TweenPosition>().to = new Vector3(0, child.localPosition.y);
                child.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                Transform child = Grid.transform.GetChild(i);
                child.GetComponent<TweenPosition>().enabled = false;
                child.gameObject.SetActive(true);
            }
            
        }
        IsPlay = true;
    }
    public void ClearWorldEventList()
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            DestroyImmediate(ItemList[i]);
        }
        ItemList.Clear();
    }
    void UpdateTime()
    {
        if (OneTime > 0)
        {
            int fenNum = (int)((OneTime / 60) % 60);
            int miao = (int)(OneTime % 60);
            int day = (int)((OneTime / 60) / 60);
            TimerLabel.text = ((day < 10) ? "0" : "") + day.ToString() + ":" + ((fenNum < 10) ? "0" : "") + fenNum.ToString() + ":" + ((miao < 10) ? "0" : "") + miao.ToString();
            //WorldEvenTime(TimerLabel.text);
            OneTime -= 1;

        }

    }

    void WorldEvenTime(string timer)
    {
        if (GameObject.Find("WorldEvenWindow") != null)
        {
            GameObject.Find("WorldEvenWindow").transform.FindChild("BottomContect").FindChild("Timer").GetComponent<UILabel>().text = timer;
        }
    }

    public void EscOnClick() {
        CancelInvoke("UpdateTime");
    }
    public void ButtonOnClick(GameObject obj) {
        SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
        WorldId = obj.GetComponent<WordEventItem>().WorldId;
        int worldGateID = TextTranslator.instance.GetWorldEventByID(WorldId).GatePoint;
        if (worldGateID != 89998 && worldGateID != 89999)
        {
            
            GameObject.Find("MainCamera").GetComponent<MouseClick>().IsWorldEvev = true;
            GameObject.Find("MainCamera").GetComponent<MouseClick>().IsEnemy = false;
            GameObject.Find("MainCamera").GetComponent<MouseClick>().IsAction = false;
            GameObject.Find("MainCamera").GetComponent<MouseClick>().IsResource = false;
        }
        else
        {
            string _statue = "";
            if (worldGateID == 89998)
            {
                _statue = "2";
            }
            else
            {
                _statue = "1";
            }
            GameObject.Find("MainCamera").GetComponent<MouseClick>().IsEnemy = true;
            GameObject.Find("MainCamera").GetComponent<MouseClick>().IsWorldEvev = false;
            GameObject.Find("MainCamera").GetComponent<MouseClick>().IsAction = false;
            GameObject.Find("MainCamera").GetComponent<MouseClick>().IsResource = false;
            NetworkHandler.instance.SendProcess("2206#" + _statue +";");
        }
        if (WordEventIdObj.activeSelf)
        {
            WordEventIdObj.SetActive(false);
        }
        
        SceneTransformer.instance.NowWorldEventID = WorldId;

        CharacterRecorder.instance.gotoGateID = TextTranslator.instance.GetGateByTypeGroup(1, TextTranslator.instance.GetWorldEventByID(WorldId).GateID).id;
        GameObject go = GameObject.Find("MapObject");
        if (go != null)
        {
            GameObject.Find("MapCon").GetComponent<MapWindow>().WorldEventId = WorldId;
            go.transform.FindChild("MapCon").GetComponent<MapWindow>().SetMapTypeUpdate();
        }
        if (GameObject.Find("MapUiWindow/All/WordEvent") != null)
        {
            GameObject.Find("MapUiWindow/All/WordEvent/EventList").SetActive(false);
            //GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().ResourceBtn.GetComponent<TweenPosition>().PlayReverse();
            //GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().IsMove = false;
        }
        

    }
    public void ActionButtonClick(GameObject obj)
    {
        if (CharacterRecorder.instance.GuideID[18] == 3)
        {
            SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
        }
        WorldId = obj.GetComponent<WordEventItem>().WorldId;
        GameObject _mapUi = GameObject.Find("MapUiWindow");
        if (_mapUi != null)
        {
            CharacterRecorder.instance.gotoGateID = TextTranslator.instance.GetGateByTypeGroup(1, TextTranslator.instance.GetActionEventById(WorldId).ForGateID).id;
            GameObject.Find("MainCamera").GetComponent<MouseClick>().IsAction = true;
            GameObject go = GameObject.Find("MapObject");
            if (go != null)
            {
                go.transform.FindChild("MapCon").GetComponent<MapWindow>().WorldEventId = WorldId;
                go.transform.FindChild("MapCon").GetComponent<MapWindow>().SetMapTypeUpdate();
            }
            //_mapUi.transform.Find("All/ActionEventWindow").gameObject.SetActive(true);
            //GameObject.Find("ActionEventWindow").GetComponent<ActionEventWindow>().InitActionEvent(obj.GetComponent<WordEventItem>().WorldId);
        }
        else
        {
            Debug.Log("MapUiWindow没有打开！---ActionButtonClick");
        }
        
        if (GameObject.Find("MapUiWindow/All/WordEvent") != null)
        {
            GameObject.Find("MapUiWindow/All/WordEvent").SetActive(false);
        }
    }

    void UpdateWorldEvent()
    {
        //GameObject go = GameObject.Find("ConfirmButton");
        Transform _updateButton = this.transform.FindChild("EventList/UpdateButton");
        //if(go!=null)
        {
           // UIEventListener.Get(go).onClick = delegate(GameObject obj)
            {
                _updateButton.GetComponent<TweenScale>().enabled = true;
                _updateButton.GetComponent<TweenScale>().from = new Vector3(1.1f, 1.1f);
                _updateButton.GetComponent<TweenScale>().to = new Vector3(1f, 1f);
                _updateButton.GetComponent<TweenScale>().ResetToBeginning();
                NetworkHandler.instance.SendProcess("2204#;");
                IsPlay = false;
                HighQCMissionNum = 0;
            };
        }
    }

    void SetColor(GameObject go, int color)
    {
        switch (color)
        {
            case 1:
                go.transform.parent.Find("Label").GetComponent<UILabel>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                break;
            case 2:
                //go.GetComponent<UISprite>().spriteName = "Grade1";
                go.transform.parent.Find("Label").GetComponent<UILabel>().color = new Color(0 / 255f, 255f / 255f, 44f / 255f);
                break;
            case 3:
                //go.GetComponent<UISprite>().spriteName = "Grade2";
                go.transform.parent.Find("Label").GetComponent<UILabel>().color = new Color( 116/ 255f, 249 / 255f, 243 / 255f);
                break;
            case 4:
                //go.GetComponent<UISprite>().spriteName = "Grade3";
                go.transform.parent.Find("Label").GetComponent<UILabel>().color = new Color(83f/255f,0f/255f,255f/255f);
                break;
            case 5:
                //go.GetComponent<UISprite>().spriteName = "Grade4";
                go.transform.parent.Find("Label").GetComponent<UILabel>().color = new Color(255f / 255f, 121f / 255f, 0 / 255);
                break;
            case 6:
                //go.GetComponent<UISprite>().spriteName = "Grade5";
                go.transform.parent.Find("Label").GetComponent<UILabel>().color = new Color(255 / 255, 0 / 255, 0 / 255);
                break;
        }
        if(color >= 5)
        {
            HighQCMissionNum += 1;
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
   
}
