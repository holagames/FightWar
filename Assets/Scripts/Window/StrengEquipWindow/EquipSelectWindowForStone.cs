using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipSelectWindowForStone : MonoBehaviour
{
    public UIGrid EquipGrid;
    public UIScrollView EquipScrollView;
    public UILabel LabelCount;
    public GameObject EquipSelectItem;
    public List<GameObject> ListEquipSelect = new List<GameObject>();
    public List<Item> listClickEquipItem = new List<Item>();
    public List<Item> listClickEquipItemNowAdd = new List<Item>();
    public static List<SelectItemCodeAndIndex> listSelectItemCodeAndIndex = new List<SelectItemCodeAndIndex>();
    public List<SelectItemCodeAndIndex> listSelectItemCodeAndIndexNowAdd = new List<SelectItemCodeAndIndex>();
    public int ClickCount = 0;
    public int TotoleCount = 0;
    public BetterList<Item> usefulItemList = new BetterList<Item>();
    [SerializeField]
    private GameObject SubmitButton;
    // Use this for initialization
    void Start()
    {
        StoneInfoPart _BaoWuStrengPart = GameObject.Find("StoneInfoPart").GetComponent<StoneInfoPart>();
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

        CreatItemInSortForRareStone();

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

    #region 秘宝强化 选择
    void CreatItemInSortForRareStone()
    {
        TotoleCount = 0;
        if (TextTranslator.instance.IsNeedUpdateItemInBag)
        {
            //Debug.LogError("IsNeedUpdateItemInBag=========");
            TextTranslator.instance.usefulItemList.Clear();
            for (int i = 0; i < TextTranslator.instance.bagItemList.size; i++)
            {
                if (TextTranslator.instance.bagItemList[i].itemCode >= 40000 && TextTranslator.instance.bagItemList[i].itemCode < 42000 && TextTranslator.instance.bagItemList[i].itemCount > 0)
                {
                    TextTranslator.instance.usefulItemList.Add(TextTranslator.instance.bagItemList[i]);
                }
            }
            TextTranslator.instance.usefulItemList = GetSumFinalInBag(TextTranslator.instance.usefulItemList);
        }
        else if (TextTranslator.instance.IsNeedUpdateUsefulItemList)
        {
            //Debug.LogError("IsNeedUpdateUsefulItemList=========");
            TextTranslator.instance.usefulItemList.Clear();
            for (int i = 0; i < TextTranslator.instance.bagItemList.size; i++)
            {
                if (TextTranslator.instance.bagItemList[i].itemCode >= 40000 && TextTranslator.instance.bagItemList[i].itemCode < 42000 && TextTranslator.instance.bagItemList[i].itemCount > 0)
                {
                    TextTranslator.instance.usefulItemList.Add(TextTranslator.instance.bagItemList[i]);
                }
            }
            TextTranslator.instance.usefulItemList = GetSumFinalInBag(TextTranslator.instance.usefulItemList);
        }
        else if (TextTranslator.instance.usefulItemList.size == 0)
        {
            //Debug.LogError("usefulItemList=========");
            TextTranslator.instance.usefulItemList.Clear();
            for (int i = 0; i < TextTranslator.instance.bagItemList.size; i++)
            {
                if (TextTranslator.instance.bagItemList[i].itemCode >= 40000 && TextTranslator.instance.bagItemList[i].itemCode < 42000 && TextTranslator.instance.bagItemList[i].itemCount > 0)
                {
                    TextTranslator.instance.usefulItemList.Add(TextTranslator.instance.bagItemList[i]);
                }
            }
            TextTranslator.instance.usefulItemList = GetSumFinalInBag(TextTranslator.instance.usefulItemList);
        }
        //DestroyAllStortStone();
        usefulItemList = TextTranslator.instance.usefulItemList;
        SortStoneList();
        //Debug.LogError(usefulItemList.size  + "...." + TextTranslator.instance.usefulItemList.size);
        for (int i = usefulItemList.size - 1; i >= 0; i--)
        {
            //if (usefulItemList[i].itemGrade == 3)
            //{
            int _count = usefulItemList[i].itemCount >= 5 ? 5 : usefulItemList[i].itemCount;
            for (int j = 0; j < _count; j++)
            {
                CreatOneItem(usefulItemList[i], j);
            }
            //}
        }
        //for (int i = usefulItemList.size - 1; i >= 0; i--)
        //{
        //    if (usefulItemList[i].itemGrade != 3)
        //    {
        //        int _count = usefulItemList[i].itemCount >= 5 ? 5 : usefulItemList[i].itemCount;
        //        for (int j = 0; j < _count; j++)
        //        {
        //            CreatOneItem(usefulItemList[i], j);
        //        }
        //    }
        //}
    }

    void DestroyAllStortStone()
    {
        for (int i = 0; i < ListEquipSelect.Count; i++)
        {
            DestroyImmediate(ListEquipSelect[i]);
        }
        ListEquipSelect.Clear();
    }
    private void SortStoneList()
    {
        for (var i = 0; i < usefulItemList.size; i++)
        {            
            for (var j = usefulItemList.size - 1; j > i; j--)
            {
                var _item1 = usefulItemList[i];
                var _item2 = usefulItemList[j];
                if (_item1.itemGrade < _item2.itemGrade)
                {
                    usefulItemList[i] = _item2;
                    usefulItemList[j] = _item1;
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
    #endregion
}
