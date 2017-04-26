using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroMapWindow : MonoBehaviour
{
    public GameObject cardItemGrid;//当前面板角色列表
    public GameObject cardItemNotGrid;//未拥有角色列表
    public GameObject cardItemPrefeb;

    public List<GameObject> TabList = new List<GameObject>();

    int tabIndex = 0;

    int heroCount = 0;
    float heroPositionX = 0;
    int heroNotCount = 0;
    List<GameObject> ListHeroMap = new List<GameObject>();
    List<HeroInfo> ListHaveHeroMap = new List<HeroInfo>();
    List<HeroInfo> ListNotHaveHeroMap = new List<HeroInfo>();

    List<HeroInfo> allListHeroMap = new List<HeroInfo>();
    void OnEnable()
    {
        bool isUnOwn = true;       
        ListHaveHeroMap.Clear();
        ListNotHaveHeroMap.Clear();
        allListHeroMap.Clear();
        heroCount = 0;
        heroPositionX = 0;
        heroNotCount = 0;

        tabIndex = 0;
        foreach (var h in TextTranslator.instance.heroInfoList)
        {
            isUnOwn = true;
            if (h.heroID < 65000)
            {
                foreach (var item in CharacterRecorder.instance.ownedHeroList)
                {
                    if (h.heroID == item.cardID)
                    {
                        ListHaveHeroMap.Add(h);
                        isUnOwn = false;
                        break;
                    }
                }
                if (isUnOwn)
                {
                    if (TextTranslator.instance.GetItemCountByID(h.heroID + 10000) >= h.heroPiece)
                    {
                        allListHeroMap.Add(h);
                    }
                    else
                    {
                        ListNotHaveHeroMap.Add(h);
                    }
                }
            }
        }
    }
    public void UpdatePageInfo()
    {
        //OnEnable();
        SelectTab(CharacterRecorder.instance.HeroMapIndex);
    }
    // Use this for initialization
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.图鉴);
        UIManager.instance.UpdateSystems(UIManager.Systems.图鉴);

        ListHeroMap.Clear();

        if (UIEventListener.Get(TabList[0]).onClick == null)
        {
            UIEventListener.Get(TabList[0]).onClick += delegate(GameObject go)
            {
                SelectTab(0);
            };
        }

        if (UIEventListener.Get(TabList[1]).onClick == null)
        {
            UIEventListener.Get(TabList[1]).onClick += delegate(GameObject go)
            {
                SelectTab(1);
            };
        }

        if (UIEventListener.Get(TabList[2]).onClick == null)
        {
            UIEventListener.Get(TabList[2]).onClick += delegate(GameObject go)
            {
                SelectTab(2);
            };
        }

        if (UIEventListener.Get(TabList[3]).onClick == null)
        {
            UIEventListener.Get(TabList[3]).onClick += delegate(GameObject go)
            {
                SelectTab(3);
            };
        }
        SelectTab(CharacterRecorder.instance.HeroMapIndex);
        //SelectTab(0);
    }

    /// <summary>
    /// 转换面板销毁所有的子对象
    /// </summary>
    void DestroyListItem()
    {
        for (int i = 0; i < ListHeroMap.Count; i++)
        {
            DestroyImmediate(ListHeroMap[i]);
        }
        ListHeroMap.Clear();
        cardItemGrid.GetComponent<UIGrid>().Reposition();
    }

    public void SetCardInfo(int _RoleID)
    {
        UIManager.instance.OpenSinglePanel("CardWindow", false);
        GameObject.Find("CardWindow").GetComponent<CardWindow>().SetCardInfo(_RoleID);
    }

    /// <summary>
    /// 实例化全部的角色
    /// </summary>
    public void DisplayAllHero()
    {
        foreach (HeroInfo item in allListHeroMap)
        {
            GameObject obj = NGUITools.AddChild(cardItemGrid, cardItemPrefeb);
            //GameObject obj = NGUITools.AddChild(cardItemNotGrid, cardItemPrefeb);
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.name = "HeroMapItem" + item.heroID;
            obj.GetComponent<HeroMapItem>().SetHeroMapInfo(item, false);
            //obj.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("MapScrollView").GetComponent<UIScrollView>();
            ListHeroMap.Add(obj);
            heroNotCount++;
        }
        foreach (HeroInfo item in ListHaveHeroMap)
        {
            GameObject obj = NGUITools.AddChild(cardItemGrid, cardItemPrefeb);
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.name = "HeroMapItem" + item.heroID;
            obj.GetComponent<HeroMapItem>().SetHeroMapInfo(item, true);
            //obj.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("MapScrollView").GetComponent<UIScrollView>();
            ListHeroMap.Add(obj);
            heroCount++;
        }
        foreach (HeroInfo item in ListNotHaveHeroMap)
        {
            GameObject obj = NGUITools.AddChild(cardItemGrid, cardItemPrefeb);
            //GameObject obj = NGUITools.AddChild(cardItemNotGrid, cardItemPrefeb);
            obj.transform.localScale = new Vector3(1, 1, 1);
            obj.name = "HeroMapItem" + item.heroID;
            obj.GetComponent<HeroMapItem>().SetHeroMapInfo(item, false);
            //obj.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("MapScrollView").GetComponent<UIScrollView>();
            ListHeroMap.Add(obj);
            heroNotCount++;
        }
    }
    /// <summary>
    /// 显示攻击角色
    /// </summary>
    public void DisplayAttackHero()
    {
        foreach (HeroInfo item in allListHeroMap)
        {
            if (item.heroCarrerType == 1)
            {
                GameObject obj = NGUITools.AddChild(cardItemGrid, cardItemPrefeb);
                //GameObject obj = NGUITools.AddChild(cardItemNotGrid, cardItemPrefeb);
                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.name = "HeroMapItem" + item.heroID;
                obj.GetComponent<HeroMapItem>().SetHeroMapInfo(item, false);
                //obj.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("MapScrollView").GetComponent<UIScrollView>();
                ListHeroMap.Add(obj);
                heroNotCount++;
            }
        }
        foreach (HeroInfo item in ListHaveHeroMap)
        {
            if (item.heroCarrerType == 1)
            {
                GameObject obj = NGUITools.AddChild(cardItemGrid, cardItemPrefeb);
                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.name = "HeroMapItem" + item.heroID;
                obj.GetComponent<HeroMapItem>().SetHeroMapInfo(item, true);
                //obj.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("MapScrollView").GetComponent<UIScrollView>();
                ListHeroMap.Add(obj);
                heroCount++;
            }
        }
        foreach (HeroInfo item in ListNotHaveHeroMap)
        {
            if (item.heroCarrerType == 1)
            {
                GameObject obj = NGUITools.AddChild(cardItemGrid, cardItemPrefeb);
                //GameObject obj = NGUITools.AddChild(cardItemNotGrid, cardItemPrefeb);
                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.name = "HeroMapItem" + item.heroID;
                obj.GetComponent<HeroMapItem>().SetHeroMapInfo(item, false);
                //obj.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("MapScrollView").GetComponent<UIScrollView>();
                ListHeroMap.Add(obj);
                heroNotCount++;
            }
        }
    }
    /// <summary>
    /// 显示防守角色
    /// </summary>
    public void DisplayDefendHero()
    {
        foreach (HeroInfo item in allListHeroMap)
        {
            if (item.heroCarrerType == 2)
            {
                GameObject obj = NGUITools.AddChild(cardItemGrid, cardItemPrefeb);
                //GameObject obj = NGUITools.AddChild(cardItemNotGrid, cardItemPrefeb);
                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.name = "HeroMapItem" + item.heroID;
                obj.GetComponent<HeroMapItem>().SetHeroMapInfo(item, false);
                //obj.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("MapScrollView").GetComponent<UIScrollView>();
                ListHeroMap.Add(obj);
                heroNotCount++;
            }
        }
        foreach (HeroInfo item in ListHaveHeroMap)
        {
            if (item.heroCarrerType == 2)
            {
                GameObject obj = NGUITools.AddChild(cardItemGrid, cardItemPrefeb);
                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.name = "HeroMapItem" + item.heroID;
                obj.GetComponent<HeroMapItem>().SetHeroMapInfo(item, true);
                //obj.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("MapScrollView").GetComponent<UIScrollView>();
                ListHeroMap.Add(obj);
                heroCount++;
            }
        }
        foreach (HeroInfo item in ListNotHaveHeroMap)
        {
            if (item.heroCarrerType == 2)
            {
                GameObject obj = NGUITools.AddChild(cardItemGrid, cardItemPrefeb);
                //GameObject obj = NGUITools.AddChild(cardItemNotGrid, cardItemPrefeb);
                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.name = "HeroMapItem" + item.heroID;
                obj.GetComponent<HeroMapItem>().SetHeroMapInfo(item, false);
                //obj.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("MapScrollView").GetComponent<UIScrollView>();
                ListHeroMap.Add(obj);
                heroNotCount++;
            }
        }
    }
    /// <summary>
    /// 显示特殊角色
    /// </summary>
    public void DisplaySpecial()
    {
        foreach (HeroInfo item in allListHeroMap)
        {
            if (item.heroCarrerType == 3)
            {
                GameObject obj = NGUITools.AddChild(cardItemGrid, cardItemPrefeb);
                //GameObject obj = NGUITools.AddChild(cardItemNotGrid, cardItemPrefeb);
                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.name = "HeroMapItem" + item.heroID;
                obj.GetComponent<HeroMapItem>().SetHeroMapInfo(item, false);
                //obj.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("MapScrollView").GetComponent<UIScrollView>();
                ListHeroMap.Add(obj);
                heroNotCount++;
            }
        }
        foreach (HeroInfo item in ListHaveHeroMap)
        {
            if (item.heroCarrerType == 3)
            {
                GameObject obj = NGUITools.AddChild(cardItemGrid, cardItemPrefeb);
                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.name = "HeroMapItem" + item.heroID;
                obj.GetComponent<HeroMapItem>().SetHeroMapInfo(item, true);
                //obj.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("MapScrollView").GetComponent<UIScrollView>();
                ListHeroMap.Add(obj);
                heroCount++;
            }
        }
        foreach (HeroInfo item in ListNotHaveHeroMap)
        {
            if (item.heroCarrerType == 3)
            {
                GameObject obj = NGUITools.AddChild(cardItemGrid, cardItemPrefeb);
                //GameObject obj = NGUITools.AddChild(cardItemNotGrid, cardItemPrefeb);
                obj.transform.localScale = new Vector3(1, 1, 1);
                obj.name = "HeroMapItem" + item.heroID;
                obj.GetComponent<HeroMapItem>().SetHeroMapInfo(item, false);
                //obj.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("MapScrollView").GetComponent<UIScrollView>();
                ListHeroMap.Add(obj);
                heroNotCount++;
            }
        }
    }

    public void GetAllHeroMapList()
    {
        OnEnable();
        FightForcePaixu();
        NotHaneHeroPaiXu();
        AllNotHaneHeroPaiXu();
        //allListHeroMap.Clear();
        //for (int i = 0; i < ListNotHaveHeroMap.Count; i++)
        //{
        //    HeroInfo heroInfo = ListNotHaveHeroMap[i];
        //    if (TextTranslator.instance.GetItemCountByID(heroInfo.heroID + 10000) >= heroInfo.heroPiece)
        //    {
        //        allListHeroMap.Add(heroInfo);
        //        ListNotHaveHeroMap.Remove(heroInfo);
        //    }
        //}
    }
    /// <summary>
    /// 没有获取的角色排序
    /// </summary>
    public void NotHaneHeroPaiXu()
    {
        ListNotHaveHeroMap.Sort(delegate(HeroInfo x, HeroInfo y)
        {
            if (x != null && y != null && x.heroState == 1 && y.heroState == 1)
            {
                return TextTranslator.instance.GetHeroInfoByHeroID(y.heroID).powerSort.CompareTo(TextTranslator.instance.GetHeroInfoByHeroID(x.heroID).powerSort);
            }
            else if (x != null && x.heroState == 1)
            {
                return 0.CompareTo(TextTranslator.instance.GetHeroInfoByHeroID(x.heroID) == null ? 0 : TextTranslator.instance.GetHeroInfoByHeroID(x.heroID).powerSort);
            }
            else if (y.heroState == 1)
            {
                return (TextTranslator.instance.GetHeroInfoByHeroID(y.heroID) == null ? 0 : TextTranslator.instance.GetHeroInfoByHeroID(y.heroID).powerSort).CompareTo(0);
            }
            return 0;
        });
    }
    public void AllNotHaneHeroPaiXu()
    {
        allListHeroMap.Sort(delegate(HeroInfo x, HeroInfo y)
        {
            if (x != null && y != null && x.heroState == 1 && y.heroState == 1)
            {
                return TextTranslator.instance.GetHeroInfoByHeroID(y.heroID).powerSort.CompareTo(TextTranslator.instance.GetHeroInfoByHeroID(x.heroID).powerSort);
            }
            else if (x != null && x.heroState == 1)
            {
                return 0.CompareTo(TextTranslator.instance.GetHeroInfoByHeroID(x.heroID) == null ? 0 : TextTranslator.instance.GetHeroInfoByHeroID(x.heroID).powerSort);
            }
            else if (y.heroState == 1)
            {
                return (TextTranslator.instance.GetHeroInfoByHeroID(y.heroID) == null ? 0 : TextTranslator.instance.GetHeroInfoByHeroID(y.heroID).powerSort).CompareTo(0);
            }
            return 0;
        });
    }
    /// <summary>
    /// 角色按照战斗力排序
    /// </summary>
    public void FightForcePaixu()
    {
        ListHaveHeroMap.Sort(delegate(HeroInfo x, HeroInfo y)
        {
            if (x != null && y != null)
            {
                return CharacterRecorder.instance.GetHeroByRoleID(y.heroID).force.CompareTo(CharacterRecorder.instance.GetHeroByRoleID(x.heroID).force);
            }
            else if (x != null)
            {
                return 0.CompareTo(CharacterRecorder.instance.GetHeroByRoleID(x.heroID) == null ? 0 : CharacterRecorder.instance.GetHeroByRoleID(x.heroID).force);
            }
            else
            {
                return (CharacterRecorder.instance.GetHeroByRoleID(y.heroID) == null ? 0 : CharacterRecorder.instance.GetHeroByRoleID(y.heroID).force).CompareTo(0);
            }

        });
    }
    /// <summary>
    /// 切换Tab
    /// </summary>
    /// <param name="tab_index">TAB索引从0开始</param>
    public void SelectTab(int tab_index)
    {
        //CharacterRecorder.instance.CompositeSuipian = -1;
        StopAllCoroutines();
        tabIndex = tab_index;
        GameObject scrollView = GameObject.Find("MapScrollView");
        scrollView.transform.localPosition = Vector3.zero;
        scrollView.transform.GetComponent<UIPanel>().clipOffset = Vector2.zero;
        if (scrollView.GetComponent<SpringPanel>() != null)
        {
            scrollView.GetComponent<SpringPanel>().target = Vector3.zero;
        }
        DestroyListItem();
        CharacterRecorder.instance.HeroMapIndex = tab_index;
        GetAllHeroMapList();
        SetBtnTab(tab_index);
        switch (tab_index)
        {
            case 0://全部角色
                DisplayAllHero();
                break;
            case 1://攻击角色
                DisplayAttackHero();
                break;
            case 2://防御角色
                DisplayDefendHero();
                break;
            case 3://特殊角色
                DisplaySpecial();
                break;
            default:
                Debug.LogError("图鉴选择面板Error!");
                break;
        }
        //cardItemGrid.GetComponent<UIGrid>().Reposition();
        cardItemGrid.transform.localPosition = SetGridPosition();
        cardItemGrid.GetComponent<UIGrid>().Reposition();
        //Debug.LogError("总角色个数：   " + (heroCount + heroNotCount));
        heroCount = 0;
        heroNotCount = 0;
        GameObject.Find("MapScrollView").transform.localPosition = Vector3.zero;
        GameObject.Find("MapScrollView").transform.GetComponent<UIPanel>().clipOffset = Vector2.zero;
    }
    /// <summary>
    /// 设置Grid的本地坐标
    /// </summary>
    /// <returns></returns>
    public Vector3 SetGridPosition()
    {
        Vector3 position = Vector3.zero;
        float pX = 0;
        pX = heroCount * cardItemGrid.GetComponent<UIGrid>().cellWidth / 2.0f;
        heroPositionX = pX;
        Debug.LogError(pX);
        position = new Vector3(-290.0f, 60.0f, 0);
        return position;
    }
    /// <summary>
    /// 设置按钮的显示
    /// </summary>
    /// <param name="tabIndex">按钮的序号</param>
    public void SetBtnTab(int tabIndex)
    {
        switch (tabIndex)
        {
            case 0:
                TabList[0].transform.GetChild(0).gameObject.SetActive(true);
                TabList[1].transform.GetChild(0).gameObject.SetActive(false);
                TabList[2].transform.GetChild(0).gameObject.SetActive(false);
                TabList[3].transform.GetChild(0).gameObject.SetActive(false);
                break;
            case 1:
                TabList[1].transform.GetChild(0).gameObject.SetActive(true);
                TabList[0].transform.GetChild(0).gameObject.SetActive(false);
                TabList[2].transform.GetChild(0).gameObject.SetActive(false);
                TabList[3].transform.GetChild(0).gameObject.SetActive(false);
                break;
            case 2:
                TabList[2].transform.GetChild(0).gameObject.SetActive(true);
                TabList[1].transform.GetChild(0).gameObject.SetActive(false);
                TabList[0].transform.GetChild(0).gameObject.SetActive(false);
                TabList[3].transform.GetChild(0).gameObject.SetActive(false);
                break;
            case 3:
                TabList[3].transform.GetChild(0).gameObject.SetActive(true);
                TabList[1].transform.GetChild(0).gameObject.SetActive(false);
                TabList[2].transform.GetChild(0).gameObject.SetActive(false);
                TabList[0].transform.GetChild(0).gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
