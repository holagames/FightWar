using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrabResult : MonoBehaviour
{
    public GameObject GrabResultObj;
    public GameObject Item;
    public GameObject Grid;
    public List<GameObject> ItemList = new List<GameObject>();
    private bool isGet = false;
    public bool isResultFinish = false;
    public UIAtlas AvatarAtlas;
    public string AwardItemName;
    public GameObject Mask;
    public GameObject SureButton;
    public int listNumber = 1;
    public GoodsItemWindow goodsitemwindow;
    public bool isOneKeyStop = false;//是否一键夺宝
    public GameObject OneKeyLabel;
    // Use this for initialization
    void Start()
    {
        goodsitemwindow = GameObject.Find("GoodsItemObj").GetComponent<GoodsItemWindow>();
        UIEventListener.Get(SureButton).onClick += delegate(GameObject obj)
        {
            for (int i = 0; i < ItemList.Count; i++)
            {
                DestroyImmediate(ItemList[i]);
            }
            ItemList.Clear();
            if (isOneKeyStop == false)
            {
                if (isResultFinish)
                {
                    this.gameObject.SetActive(false);
                    if (isGet)
                    {
                        UIManager.instance.OpenPromptWindow("获得" + AwardItemName, PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        //NetworkHandler.instance.SendProcess("1401#" + CharacterRecorder.instance.FailedID + ";");
                        goodsitemwindow.SetRobberyHeroList();
                    }
                }
            }
            else
            {
                this.gameObject.SetActive(false);
                goodsitemwindow.isOneKey = false;
                GameObject.Find("GrabItemWindow").GetComponent<GrabWindow>().UpdateList();
            }
        };

        //UIEventListener.Get(Mask).onClick += delegate(GameObject obj)
        //{
        //    if (isResultFinish)
        //    {
        //        this.gameObject.SetActive(false);
        //        if (isGet)
        //        {

        //            UIManager.instance.OpenPromptWindow("获得" + AwardItemName, PromptWindow.PromptType.Hint, null, null);
        //        }
        //        else
        //        {
        //            NetworkHandler.instance.SendProcess("1401#" + CharacterRecorder.instance.FailedID + ";");
        //            GameObject.Find("GoodsItemObj").GetComponent<GoodsItemWindow>().SetRobberyHeroList();
        //        }
        //    }
        //};
    }

    // Update is called once per frame
    public void SetInfo(List<FragmentItemData> FragmentItemDate)
    {
        goodsitemwindow = GameObject.Find("GoodsItemObj").GetComponent<GoodsItemWindow>();
        if (goodsitemwindow.isOneKey == false)
        {
            for (int i = 0; i < ItemList.Count; i++)
            {
                DestroyImmediate(ItemList[i]);
            }
            SureButton.SetActive(false);
            OneKeyLabel.SetActive(false);
            ItemList.Clear();
            SureButton.transform.Find("Label").GetComponent<UILabel>().text = "确定";
            isGet = false;
            GrabResultObj.transform.Find("ScrollBar").GetComponent<UIScrollBar>().enabled = true;
            StartCoroutine(DelayShow(FragmentItemDate));
        }
        else
        {
            isGet = false;
            isOneKeyStop = true;
            OneKeyLabel.SetActive(false);
            SureButton.transform.Find("Label").GetComponent<UILabel>().text = "停止夺宝";
            //GrabResultObj.transform.Find("ScrollBar").GetComponent<UIScrollBar>().enabled = false;
            StartCoroutine(OneKeyDelayShow(FragmentItemDate));
        }

    }
    //0.5秒显示一次
    IEnumerator DelayShow(List<FragmentItemData> FragmentItemDate)
    {
        for (int i = ItemList.Count; i < FragmentItemDate.Count; i++)
        {
            if (isGet == false)
            {
                yield return new WaitForSeconds(0.5f);
                GameObject obj = Instantiate(Item) as GameObject;
                obj.transform.parent = Grid.transform;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
                obj.SetActive(true);
                ItemList.Add(obj);
                obj.transform.Find("Name").GetComponent<UILabel>().text = "第" + UpdateNumber(i) + "次";
                int LastNum = int.Parse((FragmentItemDate[i].FiveGrabId).ToString().Remove(0, (FragmentItemDate[i].FiveGrabId).ToString().Length - 1));
                int CurNum;
                obj.transform.Find("Icon").transform.Find("Sprite").gameObject.SetActive(true);
                if (FragmentItemDate[i].FiveGrabId >= 80000 && FragmentItemDate[i].FiveGrabId < 90000)
                {
                    CurNum = (FragmentItemDate[i].FiveGrabId - LastNum) - 30000;
                }
                else if (FragmentItemDate[i].FiveGrabId > 70000 && FragmentItemDate[i].FiveGrabId < 79999)
                {
                    obj.transform.Find("Icon").transform.Find("IconItem").GetComponent<UISprite>().atlas = AvatarAtlas;
                    CurNum = FragmentItemDate[i].FiveGrabId - 10000;
                }
                else
                {
                    obj.transform.Find("Icon").transform.Find("Sprite").gameObject.SetActive(false);
                    CurNum = FragmentItemDate[i].FiveGrabId;
                }
                obj.transform.Find("Icon").GetComponent<UISprite>().spriteName = "Grade" + (TextTranslator.instance.GetItemByItemCode(CurNum).itemGrade);
                obj.transform.Find("Icon").transform.Find("IconItem").GetComponent<UISprite>().spriteName = (TextTranslator.instance.GetItemByItemCode(CurNum).picID).ToString();
                obj.transform.Find("Icon").transform.Find("Number").GetComponent<UILabel>().text = FragmentItemDate[i].FiveGrabNumber.ToString();
                if (FragmentItemDate[i].AwardID != 0)//判断夺宝是否获得
                {
                    obj.transform.Find("Result").GetComponent<UILabel>().text = "抢夺成功，获得物品";
                    Item item = new Item(FragmentItemDate[i].AwardID);
                    string color = GameObject.Find("GrabItemWindow").GetComponent<GrabWindow>().ItemNameColor(TextTranslator.instance.GetItemByItemCode(int.Parse(GameObject.Find("GrabItemWindow").GetComponent<GrabWindow>().GetItemId)).itemGrade);
                    AwardItemName = color + item.itemName + "[-]";
                    obj.transform.Find("ResultItemName").GetComponent<UILabel>().text = AwardItemName;
                    isGet = true;                 
                }
                else
                {
                    obj.transform.Find("Result").GetComponent<UILabel>().text = "抢夺失败，未获得物品";
                    obj.transform.Find("ResultItemName").gameObject.SetActive(false);
                }
                Grid.GetComponent<UIGrid>().Reposition();
                if (i > 2)
                {
                    GrabResultObj.transform.Find("ScrollView").GetComponent<UIScrollView>().UpdatePosition();
                    GrabResultObj.transform.Find("ScrollBar").GetComponent<UIScrollBar>().value += 1;
                }
                if (i == FragmentItemDate.Count)
                {
                    CharacterRecorder.instance.isFiveButtonOnce = true;  
                }
            }

        }
        isResultFinish = true;
        SureButton.SetActive(true);
    }

    IEnumerator OneKeyDelayShow(List<FragmentItemData> FragmentItemDate)
    {
        int id = 0;
        if (ItemList.Count > 0)
        {
            id = ItemList.Count - 1;
        }
        for (int i = id; i < (id + FragmentItemDate.Count); i++)
        {

            if (isGet == false)
            {
                yield return new WaitForSeconds(0.5f);
                GameObject obj = Instantiate(Item) as GameObject;
                obj.transform.parent = Grid.transform;              
                obj.transform.localScale = Vector3.one;
                obj.SetActive(true);
                ItemList.Add(obj);
                if (id == 0)
                {
                    obj.transform.Find("Name").GetComponent<UILabel>().text = "第" + (i + 1) + "次";
                    //obj.transform.localPosition = Vector3.zero + new Vector3(0, -180 * i, 0);
                    //if (i > 3)
                    //{
                    //    GrabResultObj.transform.Find("ScrollView").GetComponent<UIPanel>().clipOffset = new Vector3(-20, 34 - 180 * (i-3), 0);
                    //    GrabResultObj.transform.Find("ScrollView").transform.localPosition = new Vector3(20, 180 * (i-3), 0);
                    //}
                }
                else
                {
                    obj.transform.Find("Name").GetComponent<UILabel>().text = "第" + (i + 2) + "次";
                    //obj.transform.localPosition = Vector3.zero + new Vector3(0, -180 * (i+1), 0);
                    //GrabResultObj.transform.Find("ScrollView").GetComponent<UIPanel>().clipOffset = new Vector3(-20, 34 - 180 * (i+1-3), 0);
                    //GrabResultObj.transform.Find("ScrollView").transform.localPosition = new Vector3(20, 180 * (i+1-3), 0);
                }
                int LastNum = int.Parse((FragmentItemDate[i - id].FiveGrabId).ToString().Remove(0, (FragmentItemDate[i - id].FiveGrabId).ToString().Length - 1));
                int CurNum;
                obj.transform.Find("Icon").transform.Find("Sprite").gameObject.SetActive(true);
                if (FragmentItemDate[i - id].FiveGrabId >= 80000 && FragmentItemDate[i - id].FiveGrabId < 90000)
                {
                    CurNum = (FragmentItemDate[i - id].FiveGrabId - LastNum) - 30000;
                }
                else if (FragmentItemDate[i - id].FiveGrabId > 70000 && FragmentItemDate[i - id].FiveGrabId < 79999)
                {
                    obj.transform.Find("Icon").transform.Find("IconItem").GetComponent<UISprite>().atlas = AvatarAtlas;
                    CurNum = FragmentItemDate[i - id].FiveGrabId - 10000;
                }
                else
                {
                    obj.transform.Find("Icon").transform.Find("Sprite").gameObject.SetActive(false);
                    CurNum = FragmentItemDate[i - id].FiveGrabId;
                }
                obj.transform.Find("Icon").GetComponent<UISprite>().spriteName = "Grade" + (TextTranslator.instance.GetItemByItemCode(CurNum).itemGrade);
                obj.transform.Find("Icon").transform.Find("IconItem").GetComponent<UISprite>().spriteName = (TextTranslator.instance.GetItemByItemCode(CurNum).picID).ToString();
                obj.transform.Find("Icon").transform.Find("Number").GetComponent<UILabel>().text = FragmentItemDate[i - id].FiveGrabNumber.ToString();
                if (FragmentItemDate[i - id].AwardID != 0)//判断夺宝是否获得
                {
                    obj.transform.Find("Result").GetComponent<UILabel>().text = "抢夺成功，获得物品";
                    Item item = new Item(FragmentItemDate[i - id].AwardID);
                    string color = GameObject.Find("GrabItemWindow").GetComponent<GrabWindow>().ItemNameColor(TextTranslator.instance.GetItemByItemCode(int.Parse(GameObject.Find("GrabItemWindow").GetComponent<GrabWindow>().GetItemId)).itemGrade);
                    AwardItemName = color + item.itemName + "[-]";
                    obj.transform.Find("ResultItemName").GetComponent<UILabel>().text = AwardItemName;
                    isGet = true;
                }
                else
                {
                    obj.transform.Find("Result").GetComponent<UILabel>().text = "抢夺失败，未获得物品";
                    obj.transform.Find("ResultItemName").gameObject.SetActive(false);
                }
                Grid.GetComponent<UIGrid>().repositionNow = true;
                if (i > 2)
                {
                    yield return new WaitForSeconds(0.03f);
                    GrabResultObj.transform.Find("ScrollView").GetComponent<UIScrollView>().UpdatePosition();
                    GrabResultObj.transform.Find("ScrollBar").GetComponent<UIScrollBar>().value += 1;
                }
            }

        }
        if (goodsitemwindow.isOneKey)
        {
            yield return new WaitForSeconds(1f);
            goodsitemwindow.OneKeyEvent();
        }
        if (goodsitemwindow.OneKeyGetId.Count == 0)
        {
            yield return new WaitForSeconds(0.5f);
            isOneKeyStop = true;
            SureButton.transform.Find("Label").GetComponent<UILabel>().text = "确定";
            OneKeyLabel.SetActive(true);
            //GrabResultObj.transform.Find("ScrollBar").GetComponent<UIScrollBar>().value += 1;
            OneKeyLabel.transform.Find("Label").GetComponent<UILabel>().text = "[3ee817]已获得全部[-]" + goodsitemwindow.color + goodsitemwindow.itemName + "[-][3ee817]碎片[-]";
        }
        if (CharacterRecorder.instance.sprite < 2)
        {
            if (goodsitemwindow.MessageEngerObj.transform.Find("All/SpriteCheck").GetComponent<UIToggle>().value == true)
            {
                if (TextTranslator.instance.GetItemCountByID(10701) == 0)
                {
                    yield return new WaitForSeconds(0.5f);
                    OneKeyLabel.SetActive(true);
                    isOneKeyStop = true;
                    //GrabResultObj.transform.Find("ScrollBar").GetComponent<UIScrollBar>().value += 1;
                    SureButton.transform.Find("Label").GetComponent<UILabel>().text = "确定";
                    OneKeyLabel.transform.Find("Label").GetComponent<UILabel>().text = "[3ee817]夺宝停止，所需精力&精力恢复剂不足[-]";
                }
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
                OneKeyLabel.SetActive(true);
                isOneKeyStop = true;
                //GrabResultObj.transform.Find("ScrollBar").GetComponent<UIScrollBar>().value += 1;
                SureButton.transform.Find("Label").GetComponent<UILabel>().text = "确定";
                OneKeyLabel.transform.Find("Label").GetComponent<UILabel>().text = "[3ee817]夺宝停止，所需精力&精力恢复剂不足[-]";
            }

        }
    }

    /// <summary>
    /// 更新显示的数字
    /// </summary>
    /// <returns></returns>
    private string UpdateNumber(int Number)
    {
        string NumberString = "";
        if (Number < 10)
        {
            NumberString = NumberInfo(Number);
        }
        else if (Number >= 10 && Number < 100)
        {
            NumberString = NumberInfo((Number / 10) - 1) + "十" + NumberInfo(Number % 10);
        }
        else if (Number >= 100 && Number < 1000)
        {
            NumberString = NumberInfo((Number / 100) - 1) + "百" + NumberInfo(Number / 10 % 10) + "十" + NumberInfo(Number % 10);
        }
        return NumberString;
    }
    string NumberInfo(int Number)
    {
        string NumberString = "";
        switch (Number)
        {
            case 0:
                NumberString = "一";
                break;
            case 1:
                NumberString = "二";
                break;
            case 2:
                NumberString = "三";
                break;
            case 3:
                NumberString = "四";
                break;
            case 4:
                NumberString = "五";
                break;
            case 5:
                NumberString = "六";
                break;
            case 6:
                NumberString = "七";
                break;
            case 7:
                NumberString = "八";
                break;
            case 8:
                NumberString = "九";
                break;
            case 9:
                NumberString = "十";
                break;
        }
        return NumberString;
    }
}
