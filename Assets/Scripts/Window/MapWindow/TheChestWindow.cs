using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TheChestWindow : MonoBehaviour {

    public GameObject Mask;
    public GameObject GetBtn;
    public GameObject GetedBtn;
    public UIGrid MyGrid;
    public GameObject TheChestItem;
    public UIAtlas RoleAtlas;
    public UIAtlas ItemAtlas;
    public UILabel NeedStar;
    public UILabel CurStar;
    public UILabel GateName;

    bool IsShowNeedStar = true;
    List<Item> itemList = new List<Item>();
    int GroupId;
    int Statue;
	// Use this for initialization
	void Start () {
        if(GameObject.Find("GateInfoWindow")!= null)
        {
            string[] starNum = null;
            if(GameObject.Find("GateInfoWindow/CenterContent/CreamSprite") == null)
            {
                starNum = GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().LabelCurStar.text.Split('/');
            }
            else
            {
                starNum = GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().LabelCreamStar.text.Split('/');
            }
            
            if(IsShowNeedStar)
            {
                NeedStar.text = "领取奖励需要 " + starNum[1];
                CurStar.text = "当前拥有 " + starNum[0];
            }
        }

        if (UIEventListener.Get(Mask).onClick == null)
        {
            UIEventListener.Get(Mask).onClick = delegate(GameObject go)
            {
                //if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 14 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 2)
                //{
                //    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1312);
                //}
                //UIManager.instance.BackUI();
                DestroyImmediate(this.gameObject);
            };
        }

        if (UIEventListener.Get(GetBtn).onClick == null)
        {
            UIEventListener.Get(GetBtn).onClick = delegate(GameObject go)
            {
                //if(Statue != 0)
                //{
                //    NetworkHandler.instance.SendProcess("2014#" + GroupId + ";" + Statue + ";");
                //}
                //else
                //{
                //    NetworkHandler.instance.SendProcess("2015#" + GroupId + ";");
                //}
                //NetworkHandler.instance.SendProcess("2016#" + GroupId + ";");
            };
        }
        //GameObject giw = GameObject.Find("GateInfoWindow");
        //if(giw!= null)
        //{
        //    GateName.text = giw.GetComponent<MapGateInfoWindow>().LabelName.GetComponent<UILabel>().text;
        //}

	}
    

    public void InitGetPassReward(int _GroupId)
    {

        Debug.Log("打开TheChestWindow时的_GroupId:" + _GroupId);
        Debug.Log("打开TheChestWindow时的PassStatueList[_GroupId - 1]:" + GameObject.Find("MapObject/MapCon").GetComponent<MapWindow>().PassStatueList.Count);
        GroupId = _GroupId;
        itemList.Clear();
        GateName.text = "通关奖励";
        GameObject baoxiang = GameObject.Find("TheChestWindow");
        baoxiang.transform.Find("CenterContent/Star").gameObject.SetActive(false);
        CurStar.gameObject.SetActive(false);
        NeedStar.text = "完成关卡获得以下奖励";
        NeedStar.transform.localPosition = new Vector3(25, NeedStar.transform.localPosition.y);
        IsShowNeedStar = false;
        GateCompleteBox gcb = TextTranslator.instance.GetGateCompleteBox(_GroupId);
        if(gcb.ItemID1 != 0)
        {
            Item _item = new Item(gcb.ItemID1,gcb.ItemNum1);
            itemList.Add(_item);
        }
        if(gcb.ItemID2 != 0)
        {
            Item _item = new Item(gcb.ItemID2, gcb.ItemNum2);
            itemList.Add(_item);
        }
        if(gcb.ItemID3 != 0)
        {
            Item _item = new Item(gcb.ItemID3, gcb.ItemNum3);
            itemList.Add(_item);
        }
        if(gcb.ItemID4 != 0)
        {
            Item _item = new Item(gcb.ItemID4,gcb.ItemNum4);
            itemList.Add(_item);
        }
        if(gcb.ItemNum5 != 0)
        {
            Item _item = new Item(gcb.ItemID5,gcb.ItemNum5);
            itemList.Add(_item);
        }
        if(gcb.ItemID6 != 0)
        {
            Item _item = new Item(gcb.ItemID6,gcb.ItemNum6);
            itemList.Add(_item);
        }
        MyGridClear();
        foreach(Item item in itemList)
        {
            GameObject go = NGUITools.AddChild(MyGrid.gameObject,TheChestItem);
            go.SetActive(true);
            go.transform.localEulerAngles = Vector3.zero;
            go.transform.localPosition = Vector3.zero;
            SetItemInfo(go, item.itemCode.ToString(), item.itemCount.ToString());
            go.GetComponent<ItemExplanation>().SetAwardItem(item.itemCode, 0);
        }
        if(GameObject.Find("GateInfoWindow")!= null)
        {
            if(GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().boxStatue[2] == 1)
            {
                GetBtn.SetActive(true);
                GetedBtn.SetActive(false);
            }
            else if (GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().boxStatue[2] == 2)
            {
                GetBtn.SetActive(false);
                GetedBtn.SetActive(true);
                GetedBtn.transform.GetChild(0).GetComponent<UILabel>().text = "已领取";
            }
            else
            {
                GetBtn.SetActive(false);
                GetedBtn.SetActive(true);
                GetedBtn.transform.GetChild(0).GetComponent<UILabel>().text = "领 取";
            }
        }
        if(GameObject.Find("TheChestWindow")!=null)
        {
            GameObject go = GameObject.Find("MapObject/MapCon");
            if (go.GetComponent<MapWindow>().PassStatueList[TextTranslator.instance.GetGateCompleteBox(_GroupId).GateCompleteBoxID - 1] == 2)
            {
                GetBtn.SetActive(false);
                GetedBtn.SetActive(true);
                GetedBtn.transform.GetChild(0).GetComponent<UILabel>().text = "已领取";
            }
            else
            {
                GetBtn.SetActive(false);
                GetedBtn.SetActive(true);
                GetedBtn.transform.GetChild(0).GetComponent<UILabel>().text = "领 取";
            }
        }
    }

    public void InitGetStarReward(int _Statue,int _GroupId)
    {
        GroupId = 0;
        Statue = 0;
        GroupId = _GroupId;
        Statue = _Statue;
        GateName.text = "评星奖励";
        itemList.Clear();
        GateRankBox grb = TextTranslator.instance.GetGateRankBox(_GroupId, _Statue);
        if (grb.ItemID1 != 0)
        {
            Item _item = new Item(grb.ItemID1, grb.ItemNum1);
            itemList.Add(_item);
        }
        if (grb.ItemID2 != 0)
        {
            Item _item = new Item(grb.ItemID2, grb.ItemNum2);
            itemList.Add(_item);
        }
        if (grb.ItemID3 != 0)
        {
            Item _item = new Item(grb.ItemID3, grb.ItemNum3);
            itemList.Add(_item);
        }
        if (grb.ItemID4 != 0)
        {
            Item _item = new Item(grb.ItemID4, grb.ItemNum4);
            itemList.Add(_item);
        }
        if (grb.ItemNum5 != 0)
        {
            Item _item = new Item(grb.ItemID5, grb.ItemNum5);
            itemList.Add(_item);
        }
        if (grb.ItemID6 != 0)
        {
            Item _item = new Item(grb.ItemID6, grb.ItemNum6);
            itemList.Add(_item);
        }
        MyGridClear();
        foreach (Item item in itemList)
        {
            GameObject go = NGUITools.AddChild(MyGrid.gameObject,TheChestItem);
            go.SetActive(true);
            go.transform.localEulerAngles = Vector3.zero;
            go.transform.localPosition = Vector3.zero;
            SetItemInfo(go, item.itemCode.ToString(), item.itemCount.ToString());
            go.GetComponent<ItemExplanation>().SetAwardItem(item.itemCode, 0);
        }
        if (GameObject.Find("GateInfoWindow") != null)
        {
            if (GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().boxStatue[_Statue - 1] == 1)
            {
                GetBtn.SetActive(true);
                GetedBtn.SetActive(false);
            }
            else if (GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().boxStatue[_Statue - 1] == 2)
            {
                GetBtn.SetActive(false);
                GetedBtn.SetActive(true);
                GateName.text = "宝箱已领取";
                GetedBtn.transform.GetChild(0).GetComponent<UILabel>().text = "已领取";
            }
            else
            {
                GetBtn.SetActive(false);
                GetedBtn.SetActive(true);
                GetedBtn.transform.GetChild(0).GetComponent<UILabel>().text = "领 取";
            }
        }
    }

    void SetItemInfo(GameObject go, string itemCode,string itemCount)
    {
        TextTranslator.ItemInfo miteminfo = TextTranslator.instance.GetItemByItemCode(int.Parse(itemCode));
        SetColor(go, miteminfo.itemGrade);
        if (int.Parse(itemCount) > 9999)
        {
            itemCount = int.Parse(itemCount) / 10000 + "W";
        }
        if (int.Parse(itemCode) > 40000 && int.Parse(itemCode) < 50000)
        {
            go.transform.Find("GoodsBG/GoodsIcon").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.Find("GoodsBG/GoodsIcon").gameObject.GetComponent<UISprite>().spriteName = (int.Parse(itemCode) - 10000).ToString();
            go.transform.Find("GoodsName").gameObject.GetComponent<UILabel>().text = miteminfo.itemName;
            //go.transform.Find("SpritePiece").gameObject.SetActive(true);
        }
        else if (int.Parse(itemCode) > 80000 && int.Parse(itemCode) < 90000)
        {
            go.transform.Find("GoodsBG/GoodsIcon").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.Find("GoodsBG/GoodsIcon").gameObject.GetComponent<UISprite>().spriteName = (int.Parse(itemCode) - 30000).ToString();
            go.transform.Find("GoodsName").gameObject.GetComponent<UILabel>().text = miteminfo.itemName;
            //go.transform.Find("SpritePiece").gameObject.SetActive(true);
        }
        else if (int.Parse(itemCode) > 70000 && int.Parse(itemCode) < 79999)
        {
            go.transform.Find("GoodsBG/GoodsIcon").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
            go.transform.Find("GoodsBG/GoodsIcon").gameObject.GetComponent<UISprite>().spriteName = (int.Parse(itemCode) - 10000).ToString();
            go.transform.Find("GoodsName").gameObject.GetComponent<UILabel>().text = miteminfo.itemName;
            //go.transform.Find("SpritePiece").gameObject.SetActive(true);
        }
        else if (int.Parse(itemCode) > 60000 && int.Parse(itemCode) < 70000)
        {
            go.transform.Find("GoodsBG/GoodsIcon").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
            go.transform.Find("GoodsBG/GoodsIcon").gameObject.GetComponent<UISprite>().spriteName = (int.Parse(itemCode)).ToString();
            go.transform.Find("GoodsName").gameObject.GetComponent<UILabel>().text = miteminfo.itemName;
            //go.transform.Find("SpritePiece").gameObject.SetActive(true);
        }
        else
        {
            go.transform.Find("GoodsBG/GoodsIcon").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.Find("GoodsBG/GoodsIcon").gameObject.GetComponent<UISprite>().spriteName = itemCode;
            go.transform.Find("GoodsName").gameObject.GetComponent<UILabel>().text = miteminfo.itemName;
            //go.transform.Find("SpritePiece").gameObject.SetActive(false);
        }
        go.transform.Find("GoodsBG/LabelNum").gameObject.GetComponent<UILabel>().text = itemCount;
    }

    void MyGridClear()
    {
        int deleteNum = MyGrid.transform.childCount;
        for (int i = 0; i < deleteNum; i++)
        {
            DestroyImmediate(MyGrid.transform.GetChild(0).gameObject);
        }
    }

    void SetColor(GameObject go,int color)
    {
        switch (color)
        {
            case 1:
                go.transform.Find("GoodsBG").GetComponent<UISprite>().spriteName = "Grade1";
                break;
            case 2:
                go.transform.Find("GoodsBG").GetComponent<UISprite>().spriteName = "Grade2";
                break;
            case 3:
                go.transform.Find("GoodsBG").GetComponent<UISprite>().spriteName = "Grade3";
                break;
            case 4:
                go.transform.Find("GoodsBG").GetComponent<UISprite>().spriteName = "Grade4";
                break;
            case 5:
                go.transform.Find("GoodsBG").GetComponent<UISprite>().spriteName = "Grade5";
                break;
        }
    }
}
