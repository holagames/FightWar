using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerGetReward : MonoBehaviour {
    public UILabel CurIntegral;
    public GameObject Computational;
    private GameObject WoodsTheExpendablesObj;
    public GameObject Grid;
    public GameObject Item;
    private List<GameObject> ItemList = new List<GameObject>();
    private List<TowerShop> ShopList = new List<TowerShop>();
    public UILabel Item1Label;
    public UILabel Item2Label;
    public UILabel Item3Label;
    public UILabel Item4Label;
    public int Item1number;
    public int Item2number;
    public int Item3number;
    public int Item4number;
    public GameObject MoveScrollBar;
	// Use this for initialization
	void Start () {
        if (this.gameObject.transform.Find("EscButton") != null) {
            if (UIEventListener.Get(this.gameObject.transform.Find("EscButton").gameObject).onClick == null) {
                UIEventListener.Get(this.gameObject.transform.Find("EscButton").gameObject).onClick += delegate(GameObject obj)
                {
                    GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().UpdateWindow(WoodsTheExpendablesWindowType.Right);
                };
            }
        }
        if (GameObject.Find("WoodsTheExpendables") != null) {
            WoodsTheExpendablesObj = GameObject.Find("WoodsTheExpendables");
        }
        UIEventListener.Get(CurIntegral.gameObject).onPress += delegate(GameObject Obj,bool bo)
        {
            if (bo)
            {
                Computational.SetActive(true);
            }
            else
            {
                Computational.SetActive(false);
            }
        };
        CurIntegral.text = WoodsTheExpendablesObj.GetComponent<WoodsTheExpendables>().CurIntegral.ToString();
        SetInfo();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void SetInfo() {
        for (int i = 0; i < ItemList.Count; i++) {
            DestroyImmediate(ItemList[i].gameObject);
        }
        ItemList.Clear();
        ShopList = TextTranslator.instance.getAllTowerShop();
        for (int i = 0; i < ShopList.Count; i++) {
            InstantiateItem(ShopList[i].ItemID1, ShopList[i].ItemNum1, ShopList[i].ItemID2, ShopList[i].ItemNum2, ShopList[i].ItemID3, ShopList[i].ItemNum3, ShopList[i].ItemID4, ShopList[i].ItemNum4, ShopList[i].Point, ShopList[i].TowerShopID, ShopList[i].ColorUI,i);
        }
        Item1Label.parent.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(ShopList[0].ItemID1-10000).itemGrade.ToString();
        Item2Label.parent.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(ShopList[0].ItemID2).itemGrade.ToString();
        Item3Label.parent.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(ShopList[0].ItemID3).itemGrade.ToString();
        Item4Label.parent.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(ShopList[0].ItemID4).itemGrade.ToString();
        Item1Label.parent.transform.Find("Icon").GetComponent<UISprite>().spriteName=(ShopList[0].ItemID1-10000).ToString();
        Item2Label.parent.transform.Find("Icon").GetComponent<UISprite>().spriteName=ShopList[0].ItemID2.ToString();
        Item3Label.parent.transform.Find("Icon").GetComponent<UISprite>().spriteName=ShopList[0].ItemID3.ToString();
        Item4Label.parent.transform.Find("Icon").GetComponent<UISprite>().spriteName=ShopList[0].ItemID4.ToString();
        Grid.GetComponent<UIGrid>().repositionNow = true;
        int id = GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().CurIntegralId;
        Grid.transform.localPosition -= new Vector3(320 * id, 0, 0);
        updateItemlistShow(id);
    }
    public void InstantiateItem(int id1,int count1,int id2,int count2,int id3,int count3,int id4,int count4,int Point,int ItemID,int colorUI,int i) {
        GameObject obj = Instantiate(Item) as GameObject;
        obj.transform.parent = Grid.transform;
        obj.SetActive(true);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.GetComponent<TowerRewardItem>().setInfo(id1, count1, id2, count2, id3, count3, id4, count4, Point, ItemID, int.Parse(GameObject.Find("GetReward").GetComponent<TowerGetReward>().CurIntegral.text),colorUI);
        obj.name = (i+1).ToString();
        ItemList.Add(obj);
    }
    /// <summary>
    /// 更新购买标示的显示
    /// </summary>
    public void updateItemlistShow(int Id) {
        Item1number = 0;
        Item2number = 0;
        Item3number = 0;
        Item4number = 0;
        for (int i = 0; i < Id; i++) {
            ItemList[i].GetComponent<TowerRewardItem>().SetIsGouMai(true);
        }
    }
}
