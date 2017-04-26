using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerTreasureOpen : MonoBehaviour
{
    public GameObject Item;
    public GameObject Grid;
    private List<GameObject> ItemList = new List<GameObject>();
    private int CurNumber = 1;

    public UIAtlas HeroAtlas;
    public UIAtlas ItemAtlas;
    public GameObject MyEffect;

    // Use this for initialization
    void Start()
    {
        if (UIEventListener.Get(GameObject.Find("treasureOpendWindow").transform.Find("Button").gameObject).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("treasureOpendWindow").transform.Find("Button").gameObject).onClick += delegate(GameObject obj)
            {
                CurNumber = 1;
                GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().UpdateWindow(WoodsTheExpendablesWindowType.Right);
                NetworkHandler.instance.SendProcess("1509#;");
            };
        }
        //if (UIEventListener.Get(GameObject.Find("treasureOpendWindow").transform.Find("EscButton").gameObject).onClick == null)
        //{
        //    UIEventListener.Get(GameObject.Find("treasureOpendWindow").transform.Find("EscButton").gameObject).onClick += delegate(GameObject obj)
        //    {
        //        CurNumber = 1;
        //        GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().UpdateWindow(WoodsTheExpendablesWindowType.Right);
        //    };
        //}
        if (UIEventListener.Get(GameObject.Find("treasureOpendWindow").transform.Find("OpenButton").gameObject).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("treasureOpendWindow").transform.Find("OpenButton").gameObject).onClick += delegate(GameObject obj)
            {
                CharacterRecorder.instance.OpenTreasureNum = CurNumber;
                NetworkHandler.instance.SendProcess("1508#" + CurNumber + ";");
            };
        }
    }


    public void setInfo(string Data)
    {
        CurNumber++;
        for (int i = 0; i < ItemList.Count; i++)
        {
            DestroyImmediate(ItemList[i]);
        }
        ItemList.Clear();
        UILabel costlabel = GameObject.Find("treasureOpendWindow").transform.Find("Star").transform.Find("Label").GetComponent<UILabel>();
        if (CurNumber > 50 && CurNumber <= 100)
        {
            costlabel.text = TextTranslator.instance.GetTowerBoxCostByID(100).Diamond.ToString();
        }
        else if (CurNumber > 100 && CurNumber <= 500)
        {
            costlabel.text = TextTranslator.instance.GetTowerBoxCostByID(500).Diamond.ToString();
        }
        else if (CurNumber > 500)
        {
            costlabel.text = TextTranslator.instance.GetTowerBoxCostByID(999999).Diamond.ToString();
        }
        else
        {
            costlabel.text = TextTranslator.instance.GetTowerBoxCostByID(CurNumber).Diamond.ToString();
        }
        string[] SecData = Data.Split(';');
        string[] ItemData = SecData[3].Split('!');
        for (int i = 0; i < ItemData.Length - 1; i++)
        {
            string[] StringData = ItemData[i].Split('$');
            InstantiateItem(int.Parse(StringData[0]), int.Parse(StringData[1]));
        }
        Grid.GetComponent<UIGrid>().repositionNow = true;
    }
    private void InstantiateItem(int IconId, int count)
    {
        if ((70000 < IconId && IconId < 79999) || (42000 < IconId && IconId < 40007))
        {
            Item.transform.Find("Spri").gameObject.SetActive(true);
        }
        else
        {
            Item.transform.Find("Spri").gameObject.SetActive(false);
        }
        GameObject obj = Instantiate(Item) as GameObject;
        obj.GetComponent<ItemExplanation>().SetAwardItem(IconId, 0);
        obj.transform.parent = Grid.transform;
        obj.SetActive(true);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.GetComponent<ItemExplanation>().SetAwardItem(IconId, 0);
        if (70000 < IconId && IconId < 79999)
        {
            obj.transform.Find("Icon").GetComponent<UISprite>().atlas = HeroAtlas;
            obj.transform.Find("Icon").GetComponent<UISprite>().spriteName = (IconId - 10000).ToString();
        }
        else if (60000 < IconId && IconId < 70000)
        {
            obj.transform.Find("Icon").GetComponent<UISprite>().atlas = HeroAtlas;
            obj.transform.Find("Icon").GetComponent<UISprite>().spriteName = (IconId).ToString();
        }
        else
        {
            obj.transform.Find("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.Find("Icon").GetComponent<UISprite>().spriteName = IconId.ToString();
        }

        int Number = TextTranslator.instance.GetItemByItemCode(IconId).itemGrade;
        obj.transform.Find("Bg").GetComponent<UISprite>().spriteName = "Grade" + Number.ToString();
        if (count >= 1000)
        {
            float num = count / 10000.0f;
            obj.transform.Find("Number").GetComponent<UILabel>().text = num.ToString() + "W";
        }
        else
        {
            obj.transform.Find("Number").GetComponent<UILabel>().text = count.ToString();
        }
        GameObject childBlood = NGUITools.AddChild(obj, MyEffect);
        ItemList.Add(obj);
    }


    public void NotClickButtonCloseWindow(string ButtonName)
    {
        if (ButtonName == "Button")
        {
            CurNumber = 1;
            GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().UpdateWindow(WoodsTheExpendablesWindowType.Right);
            NetworkHandler.instance.SendProcess("1509#;");
        }
        else if (ButtonName == "OpenButton")
        {
            CharacterRecorder.instance.OpenTreasureNum = CurNumber;
            NetworkHandler.instance.SendProcess("1508#" + CurNumber + ";");
        }
    }
}
