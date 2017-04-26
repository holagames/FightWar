
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DiamondShopWindow : MonoBehaviour
{
    public GameObject MyGrid;
    public GameObject MyShopItem;
    public GameObject TabButton1;
    public GameObject TabButton2;
    public GameObject TabButton3;
    public GameObject TabButton4;
    public GameObject TabButton5;

    public GameObject LeftButton;
    public GameObject RightButton;

    [HideInInspector]
    public List<GameObject> ListRankShopItem = new List<GameObject>();
    private List<DiamondShopItemData> ShopItemList = new List<DiamondShopItemData>();


    public UICenterOnChild uicenter;
    private int TabCounter;
    private int Index;//翻页次数
    //
    void OnEnable()
    {
        NetworkHandler.instance.SendProcess("5012#0;");
        TabCounter = 1;
        Index = 2;
    }

    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.商城);
        UIManager.instance.UpdateSystems(UIManager.Systems.商城);

        if (UIEventListener.Get(TabButton1).onClick == null)
        {
            UIEventListener.Get(TabButton1).onClick += delegate(GameObject go)
            {
                TabCounter = 1;
                SetShopWindowTab();
            };
        }
        if (UIEventListener.Get(TabButton2).onClick == null)
        {
            UIEventListener.Get(TabButton2).onClick += delegate(GameObject go)
            {
                TabCounter = 2;
                SetShopWindowTab();
            };
        }
        if (UIEventListener.Get(TabButton3).onClick == null)
        {
            UIEventListener.Get(TabButton3).onClick += delegate(GameObject go)
            {
                TabCounter = 3;
                SetShopWindowTab();
            };
        }
        if (UIEventListener.Get(TabButton4).onClick == null)
        {
            UIEventListener.Get(TabButton4).onClick += delegate(GameObject go)
            {
                TabCounter = 4;
                SetShopWindowTab();
            };
        }
        if (UIEventListener.Get(TabButton5).onClick == null)
        {
            UIEventListener.Get(TabButton5).onClick += delegate(GameObject go)
            {
                TabCounter = 5;
                SetShopWindowTab();
            };
        }
        if (UIEventListener.Get(LeftButton).onClick == null)
        {
            UIEventListener.Get(LeftButton).onClick += delegate(GameObject go)
            {
                if (ListRankShopItem.Count > 6)
                {
                    CenterOnChildLeft();
                }

            };
        }
        if (UIEventListener.Get(RightButton).onClick == null)
        {
            UIEventListener.Get(RightButton).onClick += delegate(GameObject go)
            {
                if (ListRankShopItem.Count > 6)
                {
                    CenterOnChildRight();
                }
            };
        }
    }

    public void SetShopVipWindow()
    {

    }

    private bool ShowRedPoint(List<DiamondShopItemData> _ShopItemList)
    {
        int count = 0;
        for (int i = 0; i < _ShopItemList.Count; i++)
        {
            if (ShopItemList[i].itemId / 1000 == 23)
            {
                if (CharacterRecorder.instance.Vip - count == 0)
                {
                    if (ShopItemList[i].canBuyCount > 0)
                    {
                        return true;
                    }
                }
                count++;
            }
        }
        return false;
    }

    public void SetShopWindow(List<DiamondShopItemData> _ShopItemList)
    {
        uicenter.enabled = false;
        DestroyGride();
        ShopItemList.Clear();
        ListRankShopItem.Clear();
        ShopItemList = _ShopItemList;

        TabButton5.transform.Find("RedPoint").gameObject.SetActive(ShowRedPoint(_ShopItemList));

        int count = 0;
        int next = 0;
        for (int i = 0; i < _ShopItemList.Count; i++)
        {
            if (TabCounter == 5)
            {
                if (ShopItemList[i].itemId / 1000 == 23)
                {
                    if (next == 1)
                    {
                        GameObject go = NGUITools.AddChild(MyGrid, MyShopItem);
                        go.SetActive(true);
                        go.GetComponent<DiamondShopItem>().Init(ShopItemList[i]);
                        ListRankShopItem.Add(go);
                        break;
                    }
                    if (CharacterRecorder.instance.Vip - count == 0)
                    {
                        next = 1;
                        GameObject go = NGUITools.AddChild(MyGrid, MyShopItem);
                        go.SetActive(true);
                        go.GetComponent<DiamondShopItem>().Init(ShopItemList[i]);
                        ListRankShopItem.Add(go);
                    }
                    count++;
                }
            }
            else
            {
                if (ShopItemList[i].itemId / 10000 == TabCounter)
                {
                    if (ShopItemList[i].itemId / 1000 != 23)
                    {
                        GameObject go = NGUITools.AddChild(MyGrid, MyShopItem);
                        go.SetActive(true);
                        go.GetComponent<DiamondShopItem>().Init(ShopItemList[i]);
                        ListRankShopItem.Add(go);
                    }
                }
            }
            //if (_ShopItemList[i].itemId / 10000 == TabCounter)
            //{
            //    GameObject go = NGUITools.AddChild(MyGrid, MyShopItem);
            //    go.SetActive(true);
            //    go.GetComponent<DiamondShopItem>().Init(_ShopItemList[i]);
            //    ListRankShopItem.Add(go);
            //}
        }
        //MyGrid.GetComponent<UIGrid>().Reposition();
        //uicenter.enabled = false;
        //uicenter.enabled = true;
        //MyGrid.transform.parent.localPosition = new Vector3(0, -60f, 0);
        //MyGrid.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2(0, 0);
        //MyGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        //MyGrid.GetComponent<UIGrid>().Reposition();
        //uicenter.enabled = false;
        //uicenter.enabled = true;
        //uicenter.enabled = false;
        StartCoroutine(SetShopWindowIE());
    }

    IEnumerator SetShopWindowIE()
    {
        MyGrid.GetComponent<UIGrid>().Reposition();
        uicenter.enabled = true;
        yield return new WaitForSeconds(0.1f);
        MyGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
    }
    void SetShopWindowTab()//商城类型
    {
        uicenter.enabled = false;
        DestroyGride();
        ListRankShopItem.Clear();
        int count = 0;
        int next = 0;
        for (int i = 0; i < ShopItemList.Count; i++)
        {
            if (TabCounter == 5)
            {
                if (ShopItemList[i].itemId / 1000 == 23)
                {
                    if (next == 1)
                    {
                        GameObject go = NGUITools.AddChild(MyGrid, MyShopItem);
                        go.SetActive(true);
                        go.GetComponent<DiamondShopItem>().Init(ShopItemList[i]);
                        ListRankShopItem.Add(go);
                        break;
                    }
                    if (CharacterRecorder.instance.Vip - count == 0)
                    {
                        next = 1;
                        GameObject go = NGUITools.AddChild(MyGrid, MyShopItem);
                        go.SetActive(true);
                        go.GetComponent<DiamondShopItem>().Init(ShopItemList[i]);
                        ListRankShopItem.Add(go);
                    }
                    count++;
                }
            }
            else
            {
                if (ShopItemList[i].itemId / 10000 == TabCounter)
                {
                    if (ShopItemList[i].itemId / 1000 != 23)
                    {
                        GameObject go = NGUITools.AddChild(MyGrid, MyShopItem);
                        go.SetActive(true);
                        go.GetComponent<DiamondShopItem>().Init(ShopItemList[i]);
                        ListRankShopItem.Add(go);
                    }
                }
            }

        }
        MyGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        MyGrid.GetComponent<UIGrid>().Reposition();
        //uicenter.enabled = true;
    }

    void DestroyGride()
    {
        for (int i = MyGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(MyGrid.transform.GetChild(i).gameObject);
        }
    }

    private void ClickShopTypeButton(GameObject go)
    {
        //if (go != null && SelectShopType != go.name)
        //{
        //    SelectShopType = go.name;
        //    NetworkHandler.instance.SendProcess(string.Format("5102#{0};", SelectShopType));
        //}
    }

    public void RefreshShopItem(int _WindowID, int _BuyCount, int _CanBuyCount)
    {
        for (int i = 0; i < ListRankShopItem.Count; i++)
        {
            if (ListRankShopItem[i].name == _WindowID.ToString())
            {
                ListRankShopItem[i].GetComponent<DiamondShopItem>().Init(new DiamondShopItemData(_WindowID, _BuyCount, _CanBuyCount));
            }
        }
    }
    void CenterOnChildRight()
    {
        //uicenter.enabled = false;
        uicenter.enabled = true;
        for (int i = 0; i < MyGrid.transform.childCount; i++)
        {
            if (uicenter.centeredObject.name == MyGrid.transform.GetChild(i).gameObject.name)
            {
                Index = i;
            }
        }
        if (ListRankShopItem.Count - Index >= 5)
        {
            Transform traobj = uicenter.transform.GetChild(Index + 2);
            uicenter.CenterOn(traobj);
            Index += 2;
        }
        //uicenter.enabled = false;
    }
    void CenterOnChildLeft()
    {
        //uicenter.enabled = false;
        uicenter.enabled = true;
        for (int i = 0; i < MyGrid.transform.childCount; i++)
        {
            if (uicenter.centeredObject.name == MyGrid.transform.GetChild(i).gameObject.name)
            {
                Index = i;
            }
        }
        if (Index - 2 > 0)
        {
            Transform traobj = uicenter.transform.GetChild(Index - 2);
            uicenter.CenterOn(traobj);
            Index -= 2;
        }
        //uicenter.enabled = false;
    }
}
