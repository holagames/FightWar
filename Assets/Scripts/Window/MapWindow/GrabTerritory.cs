using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrabTerritory : MonoBehaviour
{
    private GameObject GrabTerritoryObj;
    public List<GameObject> DuiHao = new List<GameObject>();
    public GameObject Grid;
    public GameObject ItemObj;
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;
    private int SendTimer = 0;
    private int MapId = 0;
    private List<GameObject> ItemObjList = new List<GameObject>();
    private int MapUID = 0;
    private string ItemNumebr = "";
    private List<int> ItemIdList = new List<int>();

    private List<string> ItemIdNum = new List<string>();

    private int HeroId = 0;
    private List<int> HeroCurIdList = new List<int>();
    public GameObject HeroObj;
    public GameObject GridTwo;
    private int CurGrabTimer = 0;
    private List<GameObject> DowHeroList = new List<GameObject>();
    public UITexture HeroIcon;
    public GameObject HeroBlackBg;
    public UISprite HeroBg;
    public int FightHeroId = 0;
    public GameObject BaseButton;
    public GameObject ChallengeButton;
    public GameObject ChallengeBtnInfo;
    public GameObject DowHeroListObj;
    public UILabel LabelDes;
    public GameObject ArrowLeft;
    public GameObject ArrowBottom;
    public GameObject ArrowRight;
    public GameObject ArrowTop;
    public GameObject LabelPercentage;

    public GameObject LabelTxtDes;

    public GameObject BgCollider;

    public UILabel ResourceName;
    public UILabel HeroName;
    public UISprite BgMask;


    int LoadingSize = 9;
    private GameObject HeroGameObj;
	// Use this for initialization
	void Start () {
        UIManager.instance.CountSystem(UIManager.Systems.资源占领);
        UIManager.instance.UpdateSystems(UIManager.Systems.资源占领);
           
       
        GrabTerritoryObj = GameObject.Find("GrabTerritory");
        if (UIEventListener.Get(GrabTerritoryObj.transform.Find("GameObject").Find("EscButton").gameObject).onClick == null)
        {
            UIEventListener.Get(GrabTerritoryObj.transform.Find("GameObject").Find("EscButton").gameObject).onClick = EscButtonOnClick;
        }
        GameObject ChoiceObj = GrabTerritoryObj.transform.Find("GameObject").Find("Right").Find("Choice").gameObject;
        if (UIEventListener.Get(ChoiceObj.transform.Find("One").gameObject).onClick == null)
        {
            UIEventListener.Get(ChoiceObj.transform.Find("One").gameObject).onClick += delegate(GameObject obj)
            {
                SendTimer = 0;
                ShowChoiceTimer(SendTimer);
            };
        }
        if (UIEventListener.Get(ChoiceObj.transform.Find("Two").gameObject).onClick == null)
        {
            UIEventListener.Get(ChoiceObj.transform.Find("Two").gameObject).onClick += delegate(GameObject obj)
            {
                SendTimer = 1;
                ShowChoiceTimer(SendTimer);
            };
        }
        if (UIEventListener.Get(ChoiceObj.transform.Find("Three").gameObject).onClick == null)
        {
            UIEventListener.Get(ChoiceObj.transform.Find("Three").gameObject).onClick += delegate(GameObject obj)
            {
                SendTimer = 2;
                ShowChoiceTimer(SendTimer);
            };
        }
        //if (UIEventListener.Get(ChoiceObj.transform.parent.Find("FightButton").gameObject).onClick == null)
        {
            UIEventListener.Get(ChallengeButton).onClick = FightButton;
        }

        {
            UIEventListener.Get(HeroBlackBg).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.GuideID[17] == 3)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
                DowHeroListObj.transform.GetChild(0).GetComponent<TweenScale>().ResetToBeginning();
                DowHeroListObj.transform.GetChild(0).GetComponent<TweenPosition>().ResetToBeginning();
                DowHeroListObj.transform.GetChild(0).GetComponent<TweenScale>().PlayForward();
                DowHeroListObj.transform.GetChild(0).GetComponent<TweenPosition>().PlayForward();
                BgMask.gameObject.SetActive(true);
                HeroBlackBg.GetComponent<BoxCollider>().enabled = false;
            };
        }
        //List<int> HeroIdList = new List<int>();
        //HeroIdList = GameObject.Find("MapObject").transform.Find("MapCon").GetComponent<MapWindow>().HeroIdList;
        //for (int i = 0; i <HeroIdList.Count ; i++)
        //{
        //    for (int j = 0; j <HeroCurIdList.Count ; j++) {
        //        if (HeroIdList[i] == HeroCurIdList[j]) {
        //            HeroCurIdList.RemoveAt(j);
        //         }
        //     }
        //}
        //for (int i = 0; i < HeroCurIdList.Count; i++) {
        //    GameObject obj = Instantiate(HeroObj) as GameObject;
        //    obj.transform.parent = GridTwo.transform;
        //    obj.transform.localPosition = Vector3.zero;
        //    obj.transform.localScale = Vector3.one;
        //    obj.SetActive(true);
        //    int HeroId = CharacterRecorder.instance.GetHeroByCharacterRoleID(HeroCurIdList[i]).cardID;
        //    obj.transform.Find("heroicon").GetComponent<UISprite>().spriteName = HeroId.ToString();
        //    obj.transform.Find("SuiPian").GetComponent<UISprite>().spriteName = HeroCurIdList[i].ToString();
        //    UIEventListener.Get(obj).onClick = ButtonHero;
            
        //    DowHeroList.Add(obj);
        //}
       // InstaHero();
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
	
    public void InstaHero() {
        HeroCurIdList.Clear();
        foreach (var h in CharacterRecorder.instance.ownedHeroList)
        {

            HeroCurIdList.Add(h.characterRoleID);

        }

        for (int i = 0; i < DowHeroList.Count; i++)
        {
            DestroyImmediate(DowHeroList[i]);
        }
        DowHeroList.Clear();
        List<int> HeroIdList = new List<int>();
        HeroIdList = GameObject.Find("MapObject").transform.Find("MapCon").GetComponent<MapWindow>().HeroIdList;
        for (int i = 0; i < HeroIdList.Count; i++)
        {
            for (int j = 0; j < HeroCurIdList.Count; j++)
            {
                if (HeroIdList[i] == HeroCurIdList[j])
                {
                    HeroCurIdList.RemoveAt(j);
                }
            }
        }
        for (int i = 0; i < HeroCurIdList.Count; i++)
        {
            GameObject obj = Instantiate(HeroObj) as GameObject;
            obj.transform.parent = GridTwo.transform;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = new Vector3(1.4f, 1.4f);
            obj.SetActive(true);
            int HeroId = CharacterRecorder.instance.GetHeroByCharacterRoleID(HeroCurIdList[i]).cardID;
            Hero h = CharacterRecorder.instance.GetHeroByRoleID(HeroId);
            if (CharacterRecorder.instance.ownedHeroList.Contains(h))
            {
                SetHeroNameColor(obj.transform.Find("Bg").GetComponent<UISprite>(), CharacterRecorder.instance.ownedHeroList[CharacterRecorder.instance.ownedHeroList.IndexOf(h)].classNumber - 1);
                //obj.transform.Find("Bg").GetComponent<UISprite>().spriteName = "yxdi" + (CharacterRecorder.instance.ownedHeroList[CharacterRecorder.instance.ownedHeroList.IndexOf(h)].classNumber - 1);
            }
            obj.transform.Find("heroicon").GetComponent<UISprite>().spriteName = HeroId.ToString();
            obj.transform.Find("SuiPian").GetComponent<UISprite>().spriteName = HeroCurIdList[i].ToString();
            obj.name = "Hero" + HeroId;
            UIEventListener.Get(obj).onClick = ButtonHero;

            DowHeroList.Add(obj);
            GridTwo.GetComponent<UIGrid>().repositionNow = true;
        }
    }
    private void ClearRewardGrid()
    {
        int deleteNum = Grid.transform.childCount;
        for(int i = 0;i<deleteNum;i++)
        {
            Destroy(Grid.transform.GetChild(0).gameObject);
        }
    }
    public void ButtonHero(GameObject obj) {

        GameObject _heroObj = null;
        //if (HeroIcon.spriteName == "0")
        if (HeroIcon.mainTexture == null)
        {
            if (CharacterRecorder.instance.GuideID[17] == 4)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
            ClearRewardGrid();
            GameObject _obj = Instantiate(ItemObj) as GameObject;
            _obj.transform.parent = Grid.transform;
            _obj.transform.localScale = Vector3.one;
            _obj.transform.localPosition = Vector3.zero;
            _obj.name = "Item0";
            _obj.gameObject.SetActive(true);
            _heroObj = _obj;
            InstantiateItem();
            ChallengeButton.SetActive(true);
            ChallengeBtnInfo.SetActive(true);
        }
        else
        {
            _heroObj = Grid.transform.GetChild(0).gameObject;
        }

        HeroBg.spriteName = obj.transform.Find("Bg").GetComponent<UISprite>().spriteName;
        //HeroIcon.spriteName = obj.transform.Find("heroicon").GetComponent<UISprite>().spriteName;
        int heroID = int.Parse(obj.transform.Find("heroicon").GetComponent<UISprite>().spriteName);
        HeroIcon.mainTexture = Resources.Load("Loading/" + heroID) as Texture;
        HeroIcon.GetComponent<UITexture>().MakePixelPerfect();
        HeroIcon.GetComponent<UITexture>().width = HeroIcon.GetComponent<UITexture>().width / 20 * LoadingSize;
        HeroIcon.GetComponent<UITexture>().height = HeroIcon.GetComponent<UITexture>().height / 20 * LoadingSize;
        LabelTxtDes.SetActive(false);
        FightHeroId = int.Parse(obj.transform.Find("SuiPian").GetComponent<UISprite>().spriteName);
        ShowHeroPicse(_heroObj, heroID);
        HeroBlackBg.GetComponent<UITexture>().enabled = false;
        HeroBlackBg.transform.GetChild(0).gameObject.SetActive(false);
        BgMask.alpha = 0;

        HeroGameObj = _heroObj;

        Grid.GetComponent<UIGrid>().Reposition();

        UpInfo(SendTimer);
    }
    //接收过来的信息
    public void SetInfo(int MapId, string _percentage, string _dayCnt) {
        GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().ShowTopContent();
        this.MapId = MapId;
        //初始化相关信息
        ClearRewardGrid();
        ChallengeButton.SetActive(false);
        ChallengeBtnInfo.SetActive(false);
        HeroIcon.mainTexture = null;
        HeroBlackBg.GetComponent<UITexture>().enabled = true;
        HeroBlackBg.GetComponent<BoxCollider>().enabled = true;
        HeroBlackBg.transform.GetChild(0).gameObject.SetActive(true);
        BgMask.gameObject.SetActive(false);
        BgMask.alpha = 120 / 255f;
        DowHeroListObj.transform.GetChild(0).GetComponent<TweenScale>().ResetToBeginning();
        DowHeroListObj.transform.GetChild(0).GetComponent<TweenPosition>().ResetToBeginning();
        HeroName.gameObject.SetActive(false);
        LabelTxtDes.SetActive(true);
        ChangeLabelPercentage(LabelPercentage, _dayCnt, _percentage);

        Resource MapInfo = TextTranslator.instance.GetResourceMapId(MapId);
        MapUID = MapInfo.ResourceId;
        TextTranslator.Gate _gate = TextTranslator.instance.GetGateByID(MapId);
        LabelDes.text = _gate.description;
        ResourceName.text = _gate.name;
        AddItemIdList(MapInfo);
        CurGrabTimer = MapInfo.ResTime;
        string[] dataSplit = MapInfo.RoleDebrisNum.Split('$');
        if (dataSplit.Length > 1) {
            if (int.Parse(dataSplit[0]) != int.Parse(dataSplit[1]))
            {
                ItemNumebr = dataSplit[0];
            }
            else {
                ItemNumebr = dataSplit[0] + "~" + dataSplit[1];
            }
        }
        InstantiateItem();
        LabelTxtDes.SetActive(true);

        ShowChoiceTimer(0);
     }
    //存入可能出现的所有数
    private void AddItemIdList(Resource MapInfo) {
        ItemIdList.Clear();
        ItemIdNum.Clear();
        if (MapInfo.DebrisId1 != 0) {
            ItemIdList.Add(MapInfo.DebrisId1);
        }
        if (MapInfo.DebrisId2 != 0) {
            ItemIdList.Add(MapInfo.DebrisId2);
        }
        if (MapInfo.DebrisId3 != 0)
        {
            ItemIdList.Add(MapInfo.DebrisId3);
        }
        if (MapInfo.DebrisId4 != 0)
        {
            ItemIdList.Add(MapInfo.DebrisId4);
        }
        if (MapInfo.ItemId1 != 0)
        {
            ItemIdList.Add(MapInfo.ItemId1);
            ItemIdNum.Add(MapInfo.ItemNum1);
        }
        if (MapInfo.ItemId2 != 0)
        {
            ItemIdList.Add(MapInfo.ItemId2);
            ItemIdNum.Add(MapInfo.ItemNum2);
        }
        if (MapInfo.ItemId3 != 0)
        {
            ItemIdList.Add(MapInfo.ItemId3);
            ItemIdNum.Add(MapInfo.ItemNum3);
        }
        if (MapInfo.ItemId4 != 0)
        {
            ItemIdList.Add(MapInfo.ItemId4);
            ItemIdNum.Add(MapInfo.ItemNum4);
        }
        if (MapInfo.ItemId5 != 0)
        {
            ItemIdList.Add(MapInfo.ItemId5);
            ItemIdNum.Add(MapInfo.ItemNum5);
        }
    }
    private void InstantiateItem() {
        if (ItemObjList.Count > 0)
        {
            for (int i = 0; i < ItemObjList.Count; i++)
            {
                DestroyImmediate(ItemObjList[i]);
            }

        }
        ItemObjList.Clear();
        if (HeroId == 0)
        {
            
            for (int i = 0; i < ItemIdList.Count; i++) {
                GameObject obj = Instantiate(ItemObj) as GameObject;
                obj.transform.parent = Grid.transform;
                obj.transform.localScale = Vector3.one;
                obj.transform.localPosition = Vector3.zero;
                obj.name = "Item" + (i + 1);
                if (i != -1)
                {
                    obj.SetActive(true);
                    ShowItemIcon(obj, ItemIdList[i]);

                    string[] itemNums = ItemIdNum[i].Split('$');
                    if(itemNums.Length > 1)
                    //{
                    //    obj.transform.FindChild("SpriteMustHave").gameObject.SetActive(true);
                    //    obj.transform.FindChild("Number").GetComponent<UILabel>().text = "x" + itemNums[0];
                    //}
                    //else
                    {
                        obj.transform.FindChild("Number").GetComponent<UILabel>().text = "x" + itemNums[1];
                    }

                    TextTranslator.ItemInfo item = TextTranslator.instance.GetItemByItemCode(ItemIdList[i]);
                    SetGrade(obj.transform.Find("Bg").gameObject, item.itemGrade);
                }
                ItemObjList.Add(obj);
            }
            //if(HeroIcon.spriteName != "0")
            //{
            //    ShowHeroPicse();
            //    Grid.GetComponent<UIGrid>().
            //    Grid.GetComponent<UIGrid>().Reposition();
            //}
        }
        else {
            GameObject obj = Instantiate(ItemObj) as GameObject;
            obj.transform.parent = Grid.transform;
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = Vector3.zero;
            obj.SetActive(true);
            obj.transform.Find("Number").gameObject.SetActive(true);
            obj.transform.Find("Number").GetComponent<UILabel>().text = ItemNumebr;
            
            ItemObjList.Add(obj);
            ShowItemIcon(obj, HeroId);
            for (int i = 0; i < ItemIdList.Count; i++) {
                GameObject objn = Instantiate(ItemObj) as GameObject;
                objn.transform.parent = Grid.transform;
                objn.transform.localScale = Vector3.one;
                objn.transform.localPosition = Vector3.zero;
                objn.SetActive(true);
                ItemObjList.Add(objn);
                ShowItemIcon(objn, ItemIdList[i]);
            }
        }
        Grid.GetComponent<UIGrid>().repositionNow =true;
        
    }

    void ChangeLabelPercentage(GameObject _obj, string _dayCnt, string _percentage)
    {
        _obj.GetComponent<UILabel>().text = string.Format("完成{0}件暴动事件，获得奖励加成{1}%", _dayCnt, _percentage);
    }

    /// <summary>
    /// 给显示的图片赋值
    /// </summary>
    private void ShowItemIcon(GameObject obj,int itemId) {
        obj.GetComponent<ItemExplanation>().SetAwardItem(itemId, 0);
        if (itemId > 40000 && itemId < 50000)
        {
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (itemId - 10000).ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(true);
        }
        else if (itemId > 80000)
        {
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (itemId - 30000).ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(true);
        }
        else if (itemId > 70000 && itemId < 79999)
        {
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (itemId  - 10000).ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(true);
        }
        else
        {
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = itemId.ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// 开始巡逻按钮
    /// </summary>
    /// <param name="obj"></param>
    public void FightButton(GameObject obj) {
        //if (int.Parse(HeroIcon.spriteName) != 0)
        if (HeroIcon.mainTexture != null)
        {
            if (CharacterRecorder.instance.GuideID[17] == 5)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
            if (CharacterRecorder.instance.sprite < int.Parse(GameObject.Find("Valuenumber").GetComponent<UILabel>().text))
            {
                UIManager.instance.OpenPromptWindow("精力不足，是否购买精力药剂?", PromptWindow.PromptType.Confirm, OpenBuyWindow, null);
                return;
            }
            List<int> HeroId = new List<int>();
            foreach (var item in CharacterRecorder.instance.ownedHeroList)
            {
                HeroId.Add(item.cardID);
            }
            NetworkHandler.instance.SendProcess("1132#" + MapId + ";" + FightHeroId + ";" + CurGrabTimer + ";" + 1 + ";");
            CharacterRecorder.instance.sprite -= int.Parse(GameObject.Find("Valuenumber").GetComponent<UILabel>().text);
           // GameObject.Find("MapObject").transform.Find("MapCon").GetComponent<MapWindow>().HeroIdList.Remove(FightHeroId);
          //  GameObject.Find("MapObject").transform.Find("MapCon").GetComponent<MapWindow>().HeroId = FightHeroId;
            this.gameObject.SetActive(false);
            GameObject _mapUiWindow = GameObject.Find("MapUiWindow");
            if (_mapUiWindow != null)
            {
                _mapUiWindow.GetComponent<MapUiWindow>().CloseTopContent();
            }
            //BaseButton.SetActive(true);
            //ChallengeButton.SetActive(true);
        }
        else {
            UIManager.instance.OpenPromptWindow("请选择英雄", PromptWindow.PromptType.Alert, null, null);
        }
    }

    void OpenBuyWindow()
    {
        UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
        NetworkHandler.instance.SendProcess("5012#10702;");
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

    void SetHeroNameColor(UISprite frame, int ClassNumber)
    {
        switch (ClassNumber + 1)
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

    void ShowHeroPicse(GameObject _heroObj,int heroID)
    {
        //int itemIndex = 0;
        //ItemObjList[itemIndex].SetActive(true);
        //ShowItemIcon(ItemObjList[itemIndex], int.Parse(HeroIcon.spriteName) + 10000);
        //TextTranslator.ItemInfo item = TextTranslator.instance.GetItemByItemCode(int.Parse(HeroIcon.spriteName));
        //SetGrade(ItemObjList[itemIndex].transform.Find("Bg").gameObject, item.itemGrade);
        //Debug.Log(" 克隆出来的英雄碎片：" + _heroObj);
        //Debug.Log(" 英雄ID：" + heroID);
        ShowItemIcon(_heroObj, heroID + 10000);
        _heroObj.transform.FindChild("SpriteMustHave").gameObject.SetActive(true);
        TextTranslator.ItemInfo item = TextTranslator.instance.GetItemByItemCode(heroID);
        HeroName.gameObject.SetActive(true);
        HeroName.text = TextTranslator.instance.GetHeroInfoByHeroID(heroID).heroName;
        SetGrade(_heroObj.transform.Find("Bg").gameObject, item.itemGrade);
    }
    //关闭资源界面
   public void  EscButtonOnClick(GameObject obj){
       this.gameObject.SetActive(false);
       BaseButton.SetActive(true);
       //ChallengeButton.SetActive(true);
    }
    public void SetArrow(int Direction)
    {
        switch (Direction)
        {
            case 1:
                ArrowLeft.SetActive(true);
                ArrowBottom.SetActive(false);
                ArrowRight.SetActive(false);
                ArrowTop.SetActive(false);
                break;
            case 2:
                ArrowLeft.SetActive(false);
                ArrowBottom.SetActive(true);
                ArrowRight.SetActive(false);
                ArrowTop.SetActive(false);
                break;
            case 3:
                ArrowLeft.SetActive(false);
                ArrowBottom.SetActive(false);
                ArrowRight.SetActive(true);
                ArrowTop.SetActive(false);
                break;
            case 4:
                ArrowLeft.SetActive(false);
                ArrowBottom.SetActive(false);
                ArrowRight.SetActive(false);
                ArrowTop.SetActive(true);
                break;
            case 0:
                ArrowLeft.SetActive(false);
                ArrowBottom.SetActive(false);
                ArrowRight.SetActive(false);
                ArrowTop.SetActive(false);
                break;
        }
    }
    /// <summary>
    /// 显示界面上的时间选择框
    /// </summary>
    /// <param name="number"></param>
   private void ShowChoiceTimer(int number) {
       for (int i = 0; i < DuiHao.Count; i++) {
           if (number == i)
           {
               DuiHao[i].SetActive(true);
               UpInfo(i);
               
           }
           else {
               DuiHao[i].SetActive(false);
           }
       }
   }
   private void UpInfo(int i) {

       Resource MapInfo = TextTranslator.instance.GetResourceByID(MapUID + i);
       AddItemIdList(MapInfo);
       string[] dataSplit = MapInfo.RoleDebrisNum.Split('$');
       if (dataSplit.Length > 1)
       {
           if (int.Parse(dataSplit[0]) == int.Parse(dataSplit[1]))
           {
               ItemNumebr = "x" + dataSplit[0];
           }
           else
           {
               ItemNumebr = "x" + dataSplit[0] + "~" + dataSplit[1];
           }
           if(HeroGameObj != null)
           {
               HeroGameObj.transform.Find("Number").GetComponent<UILabel>().text = ItemNumebr;
           }
       }
       InstantiateItem();
       CurGrabTimer = MapInfo.ResTime;
       ChallengeBtnInfo.transform.Find("Valuenumber").GetComponent<UILabel>().text = MapInfo.EnergyCost.ToString();
       InstaHero();
   }
    /// <summary>
    /// 改成给服务器发送的格式
    /// </summary>
    /// <param name="number"></param>
   private int SnedTimerValuer(int number) {
       switch (number) { 
           case 0:
               return 4;
               break;
           case 1:
               return 8;
               break;
           case 2:
               return 12;
               break;
           default:
               return 4;
               break;
       }
   }
}
