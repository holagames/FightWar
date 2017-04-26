using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolLong : MonoBehaviour {
    public UILabel PayrolString;
    public UILabel CurTimer;
    private int OneTime;
    public UITexture HeroIcon;
    public GameObject BaseButton;
    public GameObject ChallengeButton;
    public UILabel ResourceName;
    public GameObject ArrowLeft;
    public GameObject ArrowBottom;
    public GameObject ArrowRight;
    public GameObject ArrowTop;

    public GameObject BgCollider;
    public GameObject ItemObj;
    public GameObject LabelPercentage;

    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;

    public UITexture ResourceIcon;
    public UILabel LabelDes;
    public UIGrid ResourceGrid;

    private List<int> ItemIdList = new List<int>();
    private List<GameObject> ItemObjList = new List<GameObject>();
    int LoadingSize = 11;
    int MyheroID = 0;
	// Use this for initialization
	void Start () {
        if (UIEventListener.Get(GameObject.Find("PatrolLong").transform.Find("EscButton").gameObject).onClick == null) {
            UIEventListener.Get(GameObject.Find("PatrolLong").transform.Find("EscButton").gameObject).onClick += delegate(GameObject obj)
            {
                this.gameObject.SetActive(false);
                BaseButton.SetActive(true);
                ChallengeButton.SetActive(true);
                CancelInvoke("UpdateTime");
                
            };
        }
        if (UIEventListener.Get(BgCollider).onClick == null)
        {
            UIEventListener.Get(BgCollider).onClick = delegate(GameObject go)
            {
                //if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) > 12)
                {
                    gameObject.SetActive(false);
                }
            };
        }
	}
	
    public void setInfo(int heroID,int CurTimer,int MapId, string _percentage, string _dayCnt) {
        MyheroID = CharacterRecorder.instance.GetHeroByCharacterRoleID(heroID).cardID;
        string Heroname = TextTranslator.instance.GetHeroInfoByHeroID(MyheroID).heroName;
        CancelInvoke("UpdateTime");
        PayrolString.text = Heroname;
        TextTranslator.Gate _gate = TextTranslator.instance.GetGateByID(MapId);
        ResourceName.text = _gate.name;
        LabelDes.text = _gate.description;
        ChangeLabelPercentage(LabelPercentage, _dayCnt, _percentage);
        ResourceIcon.mainTexture = Resources.Load("Game/ziyuan_0" + (MapId - 90000)) as Texture;
        ResourceIcon.MakePixelPerfect();
        OneTime = CurTimer;
        HeroIcon.mainTexture = Resources.Load("Loading/" + MyheroID.ToString()) as Texture;
        HeroIcon.GetComponent<UITexture>().MakePixelPerfect();
        HeroIcon.GetComponent<UITexture>().width = HeroIcon.GetComponent<UITexture>().width / 20 * LoadingSize;
        HeroIcon.GetComponent<UITexture>().height = HeroIcon.GetComponent<UITexture>().height / 20 * LoadingSize;

        Resource MapInfo = TextTranslator.instance.GetResourceMapId(MapId);
        AddItemIdList(MapInfo);
        InstantiateItem();

        InvokeRepeating("UpdateTime", 0, 1f);
    }

    void ChangeLabelPercentage(GameObject _obj, string _dayCnt, string _percentage)
    {
        _obj.GetComponent<UILabel>().text = string.Format("完成{0}件暴动事件，获得奖励加成{1}%", _dayCnt, _percentage);
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
    void UpdateTime()
    {
        if (OneTime > 0)
        {
            int fenNum = (int)((OneTime / 60) % 60);
            int miao = (int)(OneTime % 60);
            int day = (int)((OneTime / 60) / 60);
            CurTimer.text = ((day < 10) ? "0" : "") + day.ToString() + ":" + ((fenNum < 10) ? "0" : "") + fenNum.ToString() + ":" + ((miao < 10) ? "0" : "") + miao.ToString();
            OneTime -= 1;
           
        }
       
    }

    /// <summary>
    /// 给显示的图片赋值
    /// </summary>
    private void ShowItemIcon(GameObject obj, int itemId)
    {
        Debug.Log("为Item设置图片：" + obj + " ID:" + itemId);
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
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (itemId - 10000).ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(true);
        }
        else
        {
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = itemId.ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(false);
        }
        TextTranslator.ItemInfo item = TextTranslator.instance.GetItemByItemCode(itemId);
        Debug.Log("item:" + item.itemGrade);
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

    private void AddItemIdList(Resource MapInfo)
    {
        ItemIdList.Clear();
        if (MapInfo.DebrisId1 != 0)
        {
            ItemIdList.Add(MapInfo.DebrisId1);
        }
        if (MapInfo.DebrisId2 != 0)
        {
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
        }
        if (MapInfo.ItemId2 != 0)
        {
            ItemIdList.Add(MapInfo.ItemId2);
        }
        if (MapInfo.ItemId3 != 0)
        {
            ItemIdList.Add(MapInfo.ItemId3);
        }
        if (MapInfo.ItemId4 != 0)
        {
            ItemIdList.Add(MapInfo.ItemId4);
        }
        if (MapInfo.ItemId5 != 0)
        {
            ItemIdList.Add(MapInfo.ItemId5);
        }
    }

    private void InstantiateItem()
    {
        if (ItemObjList.Count > 0)
        {
            for (int i = 0; i < ItemObjList.Count; i++)
            {
                DestroyImmediate(ItemObjList[i]);
            }

        }
        ItemObjList.Clear();
        GameObject obj = Instantiate(ItemObj) as GameObject;
        obj.transform.parent = ResourceGrid.transform;
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = Vector3.zero;
        obj.SetActive(true);

        ItemObjList.Add(obj);
        ShowItemIcon(obj, MyheroID + 10000);
        for (int i = 0; i < ItemIdList.Count; i++)
        {
            GameObject objn = Instantiate(ItemObj) as GameObject;
            objn.transform.parent = ResourceGrid.transform;
            objn.transform.localScale = Vector3.one;
            objn.transform.localPosition = Vector3.zero;
            objn.SetActive(true);
            ItemObjList.Add(objn);
            ShowItemIcon(objn, ItemIdList[i]);
        }
        ResourceGrid.GetComponent<UIGrid>().repositionNow = true;

    }
}
