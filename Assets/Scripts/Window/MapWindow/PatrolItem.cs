using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PatrolItem : MonoBehaviour
{
    private int id;
    public GameObject Grid;
    public GameObject ItemIcon;
    public GameObject BaseButton;
    public GameObject ChallengeButton;
    public UISprite HeroIcon;
    private List<GameObject> ItemObjList = new List<GameObject>();
    private List<int> ItemIdList = new List<int>();
    private List<int> ItemCountList = new List<int>();
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;
    public GameObject ArrowLeft;
    public GameObject ArrowBottom;
    public GameObject ArrowRight;
    public GameObject ArrowTop;
    public GameObject LabelPercentage;

    public GameObject BgCollider;
    // Use this for initialization
    void Start()
    {
        //if (UIEventListener.Get(GameObject.Find("PatrolItem").transform.Find("EscButton").gameObject).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("PatrolItem").transform.Find("EscButton").gameObject).onClick += delegate(GameObject obj)
            {
                this.gameObject.SetActive(false);
                BaseButton.SetActive(true);
                ChallengeButton.SetActive(true);
            };
        }
        //if (UIEventListener.Get(GameObject.Find("PatrolItem").transform.Find("Button").gameObject).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("PatrolItem").transform.Find("Button").gameObject).onClick += delegate(GameObject obj)
            {
                NetworkHandler.instance.SendProcess("1133#" + id + ";");
                GameObject.Find("MapObject").transform.Find("MapCon").GetComponent<MapWindow>().HeroId = id;
                this.gameObject.SetActive(false);
                //BaseButton.SetActive(true);
                //ChallengeButton.SetActive(true);
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
    public void SetInfo(int Id, int HeroIcon, string ItemList,int gold,int lun, string percentage, string dayCnt)
    {
        this.id = Id;
        this.HeroIcon.spriteName = HeroIcon.ToString();
        Debug.Log("string    " + ItemList);
        string[] dataSplit = ItemList.Split('!');
        if (ItemIdList.Count>0)
        {
            ItemIdList.Clear();
            ItemCountList.Clear();
        }
        ItemIdList.Add(90002);
        ItemIdList.Add(90001);
        ItemCountList.Add(lun);
        ItemCountList.Add(gold);

        ChangeLabelPercentage(LabelPercentage, dayCnt, percentage);

        for (int i = 0; i < dataSplit.Length - 1; i++)
        {

            string[] dataSplitTwo = dataSplit[i].Split('$');
            //for (int j = 0; j < dataSplitTwo.Length - 1; j++)
            //{

            //}
            ItemCountList.Add(int.Parse(dataSplitTwo[1]));
            ItemIdList.Add(int.Parse(dataSplitTwo[0]));
        }
        Debug.Log("itemidlist" + ItemIdList.Count);
        InstantiateItem();
    }
    public void InstantiateItem()
    {
        if (ItemObjList.Count > 0)
        {
            for (int i = 0; i < ItemObjList.Count; i++)
            {
                DestroyImmediate(ItemObjList[i]);
            }

        }
        ItemObjList.Clear();
        for (int i = 0; i < ItemIdList.Count; i++)
        {
            GameObject obj = Instantiate(ItemIcon) as GameObject;
            obj.transform.parent = Grid.transform;
            obj.transform.localScale = new Vector3(1.2f,1.2f);
            obj.transform.localPosition = Vector3.zero;
            obj.SetActive(true);
            ItemObjList.Add(obj);

            obj.GetComponent<ItemExplanation>().SetAwardItem(ItemIdList[i],0);
            TextTranslator.ItemInfo item = TextTranslator.instance.GetItemByItemCode(ItemIdList[i]);
            SetGrade(obj.transform.Find("Bg").gameObject, item.itemGrade);
            ShowItemIcon(obj, item,ItemCountList[i]);
        }
        Grid.GetComponent<UIGrid>().repositionNow = true;
    }

    void ChangeLabelPercentage(GameObject _obj ,string _dayCnt, string _percentage)
    {
        _obj.GetComponent<UILabel>().text = string.Format("完成{0}件暴动事件，获得奖励加成{1}%", _dayCnt, _percentage);
    }

    /// <summary>
    /// 给显示的图片赋值
    /// </summary>
    private void ShowItemIcon(GameObject obj, TextTranslator.ItemInfo item,int count)
    {
        if (item.itemCode > 40000 && item.itemCode < 50000)
        {
            obj.transform.Find("Icon").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName = (item.itemCode - 10000).ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(true);
        }
        else if (item.itemCode > 80000 && item.itemCode<90000)
        {
            obj.transform.Find("Icon").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName = (item.itemCode - 30000).ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(true);
        }
        else if (item.itemCode > 70000 && item.itemCode < 79999)
        {
            obj.transform.Find("Icon").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
            obj.transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName = (item.itemCode - 10000).ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(true);
        }
        else
        {
            obj.transform.Find("Icon").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName = item.itemCode.ToString();
            obj.transform.Find("SuiPian").gameObject.SetActive(false);
        }
        obj.transform.Find("Number").gameObject.SetActive(true);
        obj.transform.Find("Number").GetComponent<UILabel>().text = count.ToString();
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
}
