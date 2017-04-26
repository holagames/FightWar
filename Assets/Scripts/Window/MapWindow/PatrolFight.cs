using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolFight : MonoBehaviour {

    public List<GameObject> EnemyList = new List<GameObject>();
    private GameObject PatrolFightObj;
    public int FbId = 0;
    private List<int> MapListId = new List<int>();
    public GameObject BaseButton;
    public GameObject ChallengeButton;
    private List<int> MapItemList = new List<int>();
    private List<int> MapColorList = new List<int>();
    private List<GameObject> MapListObj = new List<GameObject>();
    public GameObject ItemObj;
    public GameObject Grid;
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;
    public UILabel MapName;
    public UITexture EnemyIcon;
    public GameObject ArrowLeft;
    public GameObject ArrowBottom;
    public GameObject ArrowRight;
    public GameObject ArrowTop;

    public GameObject BgCollider;
    public UILabel LabelDes;
    public UILabel PowerNum;
	// Use this for initialization
	void Start () {
      //  AddMapListId();
        PatrolFightObj = GameObject.Find("PatrolFight");
        //if (UIEventListener.Get(PatrolFightObj.transform.Find("FightButton").gameObject).onClick == null) 
        {
            UIEventListener.Get(PatrolFightObj.transform.Find("FightButton").gameObject).onClick = delegate(GameObject obj)
            {
               // NetworkHandler.instance.SendProcess("2004#" + MapListId[FbId - 90001] + ";" + 0 + ";");
                //Debug.LogError("AAAAAAAADDDAAAA" + FbId);
                //if (CharacterRecorder.instance.sprite < 2)
                //{
                //    UIManager.instance.OpenPromptWindow("精力不足，是否购买精力药剂?", PromptWindow.PromptType.Confirm, OpenBuyWindow, null);
                //    return;
                //}
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_3403);
                if (CharacterRecorder.instance.GuideID[19] == 3)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
                UIManager.instance.CurGateId = FbId.ToString();
                UIManager.instance.StartGate(UIManager.GateAnchorName.gateFight);
                UIManager.instance.MapGateInfoLoading = true;
                SceneTransformer.instance.NowGateID = FbId;
                GameObject.Find("MainCamera").GetComponent<MouseClick>().IsResource = true;
                if (GameObject.Find("MapWindow(Clone)") != null)
                {
                    DestroyImmediate(GameObject.Find("MapWindow(Clone)"));
                }
                if (GameObject.Find("MapUiWindow") != null)
                {
                    DestroyImmediate(GameObject.Find("MapUiWindow"));
                }
                if (GameObject.Find("MapObject") != null)
                {
                    DestroyImmediate(GameObject.Find("MapObject"));
                }
                UIManager.instance.OpenPanel("LoadingWindow", true);                
                PictureCreater.instance.StartFight();
                PictureCreater.instance.FightStyle = 5;
            };
        }
        if (UIEventListener.Get(PatrolFightObj.transform.Find("EscButton").gameObject).onClick == null)
        {
            UIEventListener.Get(PatrolFightObj.transform.Find("EscButton").gameObject).onClick += delegate(GameObject obj)
            {
                this.gameObject.SetActive(false);
                BaseButton.SetActive(true);
                ChallengeButton.SetActive(true);
            };
        }
        if (UIEventListener.Get(BgCollider).onClick == null)
        {
            UIEventListener.Get(BgCollider).onClick = delegate(GameObject go)
            {
                //if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) > 12)
                {
                    gameObject.SetActive(false);
                    GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().CloseTopContent();
                }
            };
        }
	}

    void OpenBuyWindow()
    {
        UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
        NetworkHandler.instance.SendProcess("5012#10702;");
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
    public void SeInfo(int Id) {
        GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().ShowTopContent();
        FbId = Id;
        MapItemList.Clear();
        MapColorList.Clear();
        TextTranslator.Gate gateMap = TextTranslator.instance.GetGateByID(Id);
        MapName.text = gateMap.name;
        LabelDes.text = gateMap.description;
        PowerNum.text = gateMap.needForce.ToString();
        EnemyIcon.mainTexture = Resources.Load("Game/ziyuan_0" + (Id - 90000)) as Texture;
        EnemyIcon.MakePixelPerfect();
        //EnemyIcon.GetComponent<UITexture>().width = EnemyIcon.GetComponent<UITexture>().width / 20 * 17;
        //EnemyIcon.GetComponent<UITexture>().height = EnemyIcon.GetComponent<UITexture>().height / 20 * 17;
        SetEnemy();
        if (gateMap.itemID1 != 0) {
            MapItemList.Add(gateMap.itemID1);
            TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(gateMap.itemID1);
            MapColorList.Add(mitemInfo.itemGrade);
        }
        if (gateMap.itemID2 != 0)
        {
            MapItemList.Add(gateMap.itemID2);
            TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(gateMap.itemID2);
            MapColorList.Add(mitemInfo.itemGrade);
        }
        if (gateMap.itemID3 != 0)
        {
            MapItemList.Add(gateMap.itemID3);
            TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(gateMap.itemID3);
            MapColorList.Add(mitemInfo.itemGrade);
        }
        if (gateMap.itemID4 != 0)
        {
            MapItemList.Add(gateMap.itemID4);
            TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(gateMap.itemID4);
            MapColorList.Add(mitemInfo.itemGrade);
        }
        if (gateMap.itemID5 != 0)
        {
            MapItemList.Add(gateMap.itemID5);
            TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(gateMap.itemID5);
            MapColorList.Add(mitemInfo.itemGrade);
        }
        if (gateMap.itemID6 != 0)
        {
            MapItemList.Add(gateMap.itemID6);
            TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(gateMap.itemID6);
            MapColorList.Add(mitemInfo.itemGrade);
        }
        if (MapItemList.Count > 0) {
            ItemList();
        }
    }
    public void ItemList() {
        for (int i = 0; i < MapListObj.Count; i++) {
            DestroyImmediate(MapListObj[i]);
        }
        MapListObj.Clear();
        for (int i = 0; i < MapItemList.Count; i++) {
          
            GameObject obj = Instantiate(ItemObj) as GameObject;
            obj.transform.parent = Grid.transform;
            obj.transform.localScale = Vector3.one;
            obj.transform.localPosition = Vector3.zero;
            obj.SetActive(true);

            obj.GetComponent<ItemExplanation>().SetAwardItem(MapItemList[i], 0);

            MapListObj.Add(obj);
            SetColor(obj.transform.Find("Bg").gameObject, MapColorList[i]);
            ShowItemIcon(obj, MapItemList[i]);
            
        }
        Grid.GetComponent<UIGrid>().repositionNow = true;
    }
    /// <summary>
    /// 给显示的图片赋值
    /// </summary>
    private void ShowItemIcon(GameObject obj, int itemId)
    {
        if (itemId > 40000 && itemId < 50000)
        {
            obj.transform.Find("Icon").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName = (itemId - 10000).ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(true);
        }
        else if (itemId > 80000 && itemId < 90000)
        {
            obj.transform.Find("Icon").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName = (itemId - 30000).ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(true);
        }
        else if (itemId > 70000 && itemId < 79999)
        {
            obj.transform.Find("Icon").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
            obj.transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName = (itemId - 10000).ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(true);
        }
        else
        {
            obj.transform.Find("Icon").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName = itemId.ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(false);
        }
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
        }
    }

    void SetHeroGrade(GameObject go, int _ClassNumber)
    {
        switch (_ClassNumber)
        {
            case 1:
                go.GetComponent<UISprite>().spriteName = "yxdi0";
                break;
            case 2:
                go.GetComponent<UISprite>().spriteName = "yxdi1";
                break;
            case 3:
                go.GetComponent<UISprite>().spriteName = "yxdi2";
                break;
            case 4:
                go.GetComponent<UISprite>().spriteName = "yxdi3";
                break;
            case 5:
                go.GetComponent<UISprite>().spriteName = "yxdi4";
                break;
            case 6:
                go.GetComponent<UISprite>().spriteName = "yxdi5";
                break;
            case 7:
                go.GetComponent<UISprite>().spriteName = "yxdi6";
                break;
            default:
                break;
        }
    }

    void SetEnemy()
    {
        List<GateMap> ListGateMap = TextTranslator.instance.GetGateMapByID(FbId);
        int _countNum = 0;
        for (int i = 0; i < ListGateMap.Count; i++)
        {
            for (int j = 0; j < TextTranslator.instance.enemyInfoList.Count; j++)
            {
                if (ListGateMap[i].MonsterID == TextTranslator.instance.enemyInfoList[j].enemyID)
                {
                    if (ListGateMap[i].PosID > 0)
                    {
                        EnemyList[_countNum].transform.FindChild("Icon").GetComponent<UISprite>().spriteName = TextTranslator.instance.enemyInfoList[j].roleID.ToString();
                        SetHeroGrade(EnemyList[_countNum].gameObject, TextTranslator.instance.enemyInfoList[j].type);
                        _countNum++;
                        
                        if(_countNum >= 6)
                        {
                            break;
                        }
                    }
                }
            }
        }

        for(int i = _countNum;i<EnemyList.Count;i++)
        {
            EnemyList[i].SetActive(false);
        }
    }

    
}
