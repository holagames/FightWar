using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipSelectWindow : MonoBehaviour
{
    public UIGrid EquipGrid;
    public UIScrollView EquipScrollView;
    public UILabel LabelCount;
    public GameObject EquipSelectItem;
    public List<GameObject> ListEquipSelect = new List<GameObject>();
    //public List<Item> listClickEquipItem = new List<Item>();
    //public List<Item> listClickEquipItemNowAdd = new List<Item>();
    public List<SelectItemCodeAndIndex> listSelectItemCodeAndIndex = new List<SelectItemCodeAndIndex>();//数据B
    public List<SelectItemCodeAndIndex> listSelectItemCodeAndIndexNowAdd = new List<SelectItemCodeAndIndex>();//数据C
    public int ClickCount = 0;
    public int TotoleCount = 0;
    [SerializeField]
    private GameObject SubmitButton;
    public BetterList<Item> usefulItemList = new BetterList<Item>();
    // Use this for initialization
    void Start()
    {
        BaoWuStrengPart _BaoWuStrengPart = GameObject.Find("BaoWuStrengPart").GetComponent<BaoWuStrengPart>();
        ListEquipSelect.Clear();

        listSelectItemCodeAndIndex = _BaoWuStrengPart.listSelectItemCodeAndIndex;
        listSelectItemCodeAndIndexNowAdd.AddRange(listSelectItemCodeAndIndex);

        ClickCount = listSelectItemCodeAndIndex.Count;
        TextTranslator.instance.IsNeedUpdateUsefulItemList = ClickCount > 0 ? false : true;

        SetEquipSelectWindow();

        if (UIEventListener.Get(GameObject.Find("EquipCloseButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("EquipCloseButton")).onClick += delegate(GameObject go)
            {
                //Debug.LogError("关闭时count。。。" + listSelectItemCodeAndIndex.Count + "..." + _BaoWuStrengPart.listSelectItemCodeAndIndex.Count);
                UIManager.instance.BackUI();
            };
        }

        if (UIEventListener.Get(SubmitButton).onClick == null)
        {
            UIEventListener.Get(SubmitButton).onClick += delegate(GameObject go)
            {
                if (TotoleCount > 0)
                {
                    listSelectItemCodeAndIndex = listSelectItemCodeAndIndexNowAdd;
                    _BaoWuStrengPart.listSelectItemCodeAndIndex = listSelectItemCodeAndIndex;
                    //Debug.LogError("确定时count。。。" + listSelectItemCodeAndIndex.Count + "...." + listSelectItemCodeAndIndexNowAdd.Count);
                    _BaoWuStrengPart.ShowSelectEquipItems(listSelectItemCodeAndIndex);
                    UIManager.instance.BackUI();
                }
                else
                {
                    PictureCreater.instance.DestroyAllComponent();
                    UIManager.instance.OpenPanel("GrabItemWindow", true);
                }
            };
        }
    }
    private void SetEquipSelectWindow()
    {

        //旧的，没有排序
        /*  TotoleCount = 0;
          foreach (var item in TextTranslator.instance.bagItemList)
          {
              if (item.itemCode > 51010 && item.itemCode < 60000 && item.itemCount > 0)
              {
                  GameObject go = NGUITools.AddChild(EquipGrid.gameObject, EquipSelectItem);
                  go.name = "EquipSelectItem" + item.itemCode;
                  go.GetComponent<EquipSelectItem>().Init(item, listClickEquipItem);
                  ListEquipSelect.Add(go);
                  TotoleCount++;
              }
          }*/
        CreatItemInSort();

        EquipGrid.GetComponent<UIGrid>().Reposition();
        UILabel _buttonLabel = SubmitButton.transform.FindChild("Label").GetComponent<UILabel>();
        if (TotoleCount > 0)
        {
            //旧的分母显示总数
            //LabelCount.text = ClickCount.ToString() + "/" + TotoleCount.ToString();
            LabelCount.text = ClickCount.ToString() + "/5";
            LabelCount.transform.localPosition = new Vector3(351, 232, 0);
            _buttonLabel.text = "确 定";
        }
        else
        {
            LabelCount.text = "当前分类里没有对应的道具";
            LabelCount.transform.localPosition = Vector3.zero;
            _buttonLabel.text = "去夺宝";
        }
    }
    void CreatItemInSort()
    {
        if (TextTranslator.instance.IsNeedUpdateItemInBag)
        {
            TextTranslator.instance.usefulItemList.Clear();
            for (int i = 0; i < TextTranslator.instance.bagItemList.size; i++)
            {
                if (TextTranslator.instance.bagItemList[i].itemCode > 51010 && TextTranslator.instance.bagItemList[i].itemCode < 60000 && TextTranslator.instance.bagItemList[i].itemCount > 0)
                {
                    TextTranslator.instance.usefulItemList.Add(TextTranslator.instance.bagItemList[i]);
                }
            }
            TextTranslator.instance.usefulItemList = GetSumFinalInBag(TextTranslator.instance.usefulItemList);
        }
        else if (TextTranslator.instance.IsNeedUpdateUsefulItemList)
        {
            TextTranslator.instance.usefulItemList.Clear();
            for (int i = 0; i < TextTranslator.instance.bagItemList.size; i++)
            {
                if (TextTranslator.instance.bagItemList[i].itemCode > 51010 && TextTranslator.instance.bagItemList[i].itemCode < 60000 && TextTranslator.instance.bagItemList[i].itemCount > 0)
                {
                    TextTranslator.instance.usefulItemList.Add(TextTranslator.instance.bagItemList[i]);
                }
            }
            TextTranslator.instance.usefulItemList = GetSumFinalInBag(TextTranslator.instance.usefulItemList);
        }
        else if (TextTranslator.instance.usefulItemList.size == 0)
        {
            TextTranslator.instance.usefulItemList.Clear();
            for (int i = 0; i < TextTranslator.instance.bagItemList.size; i++)
            {
                if (TextTranslator.instance.bagItemList[i].itemCode > 51010 && TextTranslator.instance.bagItemList[i].itemCode < 60000 && TextTranslator.instance.bagItemList[i].itemCount > 0)
                {
                    TextTranslator.instance.usefulItemList.Add(TextTranslator.instance.bagItemList[i]);
                }
            }
            TextTranslator.instance.usefulItemList = GetSumFinalInBag(TextTranslator.instance.usefulItemList);
        }
        usefulItemList = TextTranslator.instance.usefulItemList;
        usefulItemListSort(usefulItemList);
        for (int i = usefulItemList.size - 1; i >= 0; i--)
        {
            int _count = usefulItemList[i].itemCount >= 5 ? 5 : usefulItemList[i].itemCount;
            for (int j = 0; j < _count; j++)
            {
                CreatOneItem(usefulItemList[i], j);
            }
        }
        ////Debug.LogError (TextTranslator.instance.usefulItemList.size + "...." + usefulItemList.size);
        //for (int i = usefulItemList.size - 1; i >= 0; i--)
        //{
        //    if ( usefulItemList[i].itemCode == 59000)//usefulItemList[i].itemGrade == 3 &&
        //    {
        //        int _count = usefulItemList[i].itemCount >= 5 ? 5 : usefulItemList[i].itemCount;
        //        for (int j = 0; j < _count; j++)
        //        {
        //            CreatOneItem(usefulItemList[i],j);
        //        }
        //    }
        //}
        //for (int i = usefulItemList.size - 1; i >= 0; i--)
        //{
        //    if (usefulItemList[i].itemGrade == 3 && usefulItemList[i].itemCode != 59000)
        //    {
        //        int _count = usefulItemList[i].itemCount >= 5 ? 5 : usefulItemList[i].itemCount;
        //        for (int j = 0; j < _count; j++)
        //        {
        //            CreatOneItem(usefulItemList[i],j);
        //        }
        //    }
        //}
        //for (int i = usefulItemList.size - 1; i >= 0; i--)
        //{
        //    if (usefulItemList[i].itemCode == 59010)//usefulItemList[i].itemGrade == 3 && 
        //    {
        //        int _count = usefulItemList[i].itemCount >= 5 ? 5 : usefulItemList[i].itemCount;
        //        for (int j = 0; j < _count; j++)
        //        {
        //            CreatOneItem(usefulItemList[i],j);
        //        }
        //    }
        //}
        //for (int i = usefulItemList.size - 1; i >= 0; i--)
        //{
        //    if (usefulItemList[i].itemCode != 59000 && usefulItemList[i].itemCode != 59010 && usefulItemList[i].itemGrade != 3 )
        //    {
        //        int _count = usefulItemList[i].itemCount >= 5 ? 5 : usefulItemList[i].itemCount;
        //        for (int j = 0; j < _count; j++)
        //        {
        //            CreatOneItem(usefulItemList[i],j);
        //        }
        //    }
        //}
    }
    /// <summary>
    /// 升序
    /// </summary>
    /// <param name="itemList"></param>
    private void usefulItemListSort(BetterList<Item> itemList)
    {
        for (int i = 0; i < itemList.size; i++)
        {
            for (int j = itemList.size - 1; j > i; j--)
            {
                if (itemList[j].itemGrade > itemList[j - 1].itemGrade)
                {
                    Item temp = itemList[j];
                    itemList[j] = itemList[j - 1];
                    itemList[j - 1] = temp;
                }
            }
        }
    }

    void CreatOneItem(Item _oneItem, int index)
    {
        GameObject go = NGUITools.AddChild(EquipGrid.gameObject, EquipSelectItem);
        go.name = "EquipSelectItem" + _oneItem.itemCode;
        go.GetComponent<EquipSelectItem>().Init(_oneItem, index, listSelectItemCodeAndIndex);
        ListEquipSelect.Add(go);
        TotoleCount++;
    }
    int _index = 0;
    BetterList<Item> GetSumFinalInBag(BetterList<Item> _bagItemList)
    {
        BetterList<Item> _itemList = new BetterList<Item>();
        for (int i = 0; i < _bagItemList.size; i++)
        {
            int itemCode = _bagItemList[i].itemCode;
            if (IsAwardListAreadyContainsThisCode(_itemList, itemCode))
            {
                _itemList[_index] = new Item(itemCode, _itemList[_index].itemCount + _bagItemList[i].itemCount);
            }
            else
            {
                _itemList.Add(new Item(itemCode, _bagItemList[i].itemCount));
            }
        }
        return _itemList;
    }
    bool IsAwardListAreadyContainsThisCode(BetterList<Item> _itemList, int _code)
    {
        for (int i = 0; i < _itemList.size; i++)
        {
            if (_itemList[i].itemCode == _code)
            {
                _index = i;
                return true;
            }
        }
        return false;
    }
}
