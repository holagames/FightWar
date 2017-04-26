using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GaChaAwardWindow : MonoBehaviour {

    public List<GameObject> TabList = new List<GameObject>();
    public GameObject uiGrid0;
    public GameObject uiGrid;
    public GameObject uiGrid2;
    public GameObject CloseButton;
    public GameObject HeroItem;
    public GameObject IconItem;
    public GameObject TopSpriteItem;
    public GameObject QuanGrid;
    public GameObject ScrollView0;
    public GameObject ScrollView1;
    public GameObject ScrollView2;

    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;

    public UILabel LevelInfoLabel;

    private int GachaType = 1;  //金币1，钻石2.
    private List<int> HeroOpenLevelList = new List<int>();

    private List<HeroInfo> AllHeroInfoList = new List<HeroInfo>();
    private List<HeroInfo> HeroInfoList = new List<HeroInfo>();

    private BetterList<GachaPreview> NewGachaPreviewList = new BetterList<GachaPreview>();//单个类别用

    private BetterList<GachaPreview> NewPreviewList = new BetterList<GachaPreview>();//单个攻防特用

    public GameObject ScrollBar0;
    public GameObject ScrollBar1;
    public GameObject ScrollBar2;

	void Start () {

        UIEventListener.Get(TabList[0]).onClick = delegate(GameObject go)
        {
            //DisplayHero(0);
            DisplayAllHero();
        };

        UIEventListener.Get(TabList[1]).onClick = delegate(GameObject go)
        {
            DisplayHero(1);
        };

        UIEventListener.Get(TabList[2]).onClick = delegate(GameObject go)
        {
            DisplayHero(2);
        };

        UIEventListener.Get(TabList[3]).onClick = delegate(GameObject go)
        {
            DisplayHero(3);
        };

        UIEventListener.Get(TabList[4]).onClick = delegate(GameObject go)
        {
            DisplayOther();
        };

        UIEventListener.Get(CloseButton).onClick = delegate(GameObject go)
        {
            UIManager.instance.BackUI();
        };
        LevelInfoLabel.text = CharacterRecorder.instance.level + "级解锁";
	}

    public void ChangeChoseTabNum(int id)
    {
        if (id != 0)
        {
            TabList[0].GetComponent<UIToggle>().value = false;
            TabList[0].GetComponent<UIToggle>().startsActive = false;


            TabList[id].GetComponent<UIToggle>().value = true;
            TabList[id].GetComponent<UIToggle>().enabled = true;
            TabList[id].transform.Find("ChangeButton").gameObject.SetActive(true);
        }
        else
        {
            TabList[0].GetComponent<UIToggle>().value = true;
            TabList[0].GetComponent<UIToggle>().startsActive = true;
        }
    }

    public void SetGachaType(int _GachaType) //开始确定类别;
    {
        this.GachaType = _GachaType;
        ChangeChoseTabNum(0);
        HeroOpenLevelList.Clear();
        //ScrollView1.SetActive(true);
        foreach (var item in TextTranslator.instance.GachaPreviewList) 
        {
            if (item.UIType == _GachaType) 
            {
                if (HeroOpenLevelList.Contains(item.Level) == false) 
                {
                    HeroOpenLevelList.Add(item.Level); //等级划分
                }
                NewGachaPreviewList.Add(item);  //新的类别
            }
        }
        DisplayAllHero();
        //int listCont = HeroOpenLevelList.Count;
        //for (int m = 0; m < listCont; m++)  //等级从小到大
        //{
        //    for (var n = listCont - 1; n > m; n--)
        //    {
        //        int levelA = HeroOpenLevelList[m];
        //        int levelB = HeroOpenLevelList[n];
        //        if (levelA > levelB)
        //        {
        //            var temp = HeroOpenLevelList[m];
        //            HeroOpenLevelList[m] = HeroOpenLevelList[n];
        //            HeroOpenLevelList[n] = temp;
        //        }
        //    }
        //}


        //for (int i = 0; i < HeroOpenLevelList.Count; i++) //第一个标签全部英雄划分
        //{
        //    HeroInfoList.Clear();
        //    foreach (var item in NewGachaPreviewList)
        //    {
        //        if (item.Level == HeroOpenLevelList[i])
        //        {
        //            if (item.ItemID.ToString()[0] == '6')
        //            {
        //                HeroInfo heroinfo = TextTranslator.instance.GetHeroInfoByHeroID(item.ItemID);
        //                HeroInfoList.Add(heroinfo);
        //                AllHeroInfoList.Add(heroinfo);
        //            }
        //        }
        //    }

        //    int listSize = HeroInfoList.Count;
        //    for (int m = 0; m < listSize; m++)
        //    {
        //        for (var n = listSize - 1; n > m; n--)
        //        {
        //            int heroA = HeroInfoList[m].heroRarity;
        //            int heroB = HeroInfoList[n].heroRarity;
        //            if (heroA < heroB)
        //            {
        //                var temp = HeroInfoList[m];
        //                HeroInfoList[m] = HeroInfoList[n];
        //                HeroInfoList[n] = temp;
        //            }
        //        }
        //    }

        //    for (int x = 0; x < HeroInfoList.Count; x++) 
        //    {
        //        GameObject go = NGUITools.AddChild(uiGrid, HeroItem);
        //        go.SetActive(true);
        //        go.name = HeroInfoList[x].heroID.ToString();
        //        SetRarityIcon(go.transform.Find("SpriteRare").GetComponent<UISprite>(), HeroInfoList[x].heroRarity);
        //        go.transform.Find("HeroIcon").GetComponent<UISprite>().spriteName = HeroInfoList[x].heroID.ToString();
        //        go.transform.Find("HeroName").GetComponent<UILabel>().text = HeroInfoList[x].heroName;
        //        UIEventListener.Get(go).onClick = delegate(GameObject obj)
        //        {
        //            UIManager.instance.OpenSinglePanel("CardDetailInfoWindow", false);
        //            GameObject.Find("CardDetailInfoWindow").GetComponent<CardDetailInfoWindow>().SetDetaiInfo(int.Parse(go.name));
        //        };
        //    }
        //}
        //uiGrid.GetComponent<UIGrid>().Reposition();
    }


    private void DisplayAllHero()// 0全
    {
        HeroOpenLevelList.Clear();
        NewPreviewList.Clear();
        ScrollView0.GetComponent<UIScrollView>().ResetPosition();
        for (int i = ScrollView0.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(ScrollView0.transform.GetChild(i).gameObject);
        }
        ScrollBar0.SetActive(true);
        ScrollBar1.SetActive(false);
        ScrollBar2.SetActive(false);
        ScrollBar0.GetComponent<UIScrollBar>().value = 0;

        ScrollView0.SetActive(true);
        ScrollView1.SetActive(false);
        ScrollView2.SetActive(false);

        ScrollView0.GetComponent<UIScrollView>().ResetPosition();

        foreach (var item in NewGachaPreviewList)
        {
            if (item.ItemID.ToString()[0] == '6')
            {
                NewPreviewList.Add(item);
            }
        }

        foreach (var item in NewPreviewList)
        {
            if (HeroOpenLevelList.Contains(item.Level) == false)
            {
                HeroOpenLevelList.Add(item.Level); //等级划分
            }
        }

        int listCont = HeroOpenLevelList.Count;
        for (int m = 0; m < listCont; m++)  //等级从小到大
        {
            for (var n = listCont - 1; n > m; n--)
            {
                int levelA = HeroOpenLevelList[m];
                int levelB = HeroOpenLevelList[n];
                if (levelA > levelB)
                {
                    var temp = HeroOpenLevelList[m];
                    HeroOpenLevelList[m] = HeroOpenLevelList[n];
                    HeroOpenLevelList[n] = temp;
                }
            }
        }


        for (int i = 0; i < HeroOpenLevelList.Count; i++) //对应标签英雄划分
        {
            HeroInfoList.Clear();
            foreach (var item in NewPreviewList)
            {
                if (item.Level == HeroOpenLevelList[i])
                {
                    HeroInfo heroinfo = TextTranslator.instance.GetHeroInfoByHeroID(item.ItemID);
                    HeroInfoList.Add(heroinfo);
                }
            }

            int listSize = HeroInfoList.Count;
            for (int m = 0; m < listSize; m++)
            {
                for (var n = listSize - 1; n > m; n--)
                {
                    int heroA = HeroInfoList[m].heroRarity;
                    int heroB = HeroInfoList[n].heroRarity;
                    if (heroA < heroB)
                    {
                        var temp = HeroInfoList[m];
                        HeroInfoList[m] = HeroInfoList[n];
                        HeroInfoList[n] = temp;
                    }
                }
            }

            if (i == 0)
            {
                GameObject TopItem = NGUITools.AddChild(ScrollView0, TopSpriteItem);
                TopItem.SetActive(true);
                TopItem.transform.localPosition = new Vector3(-368f, 230f, 0f);
                TopItem.transform.Find("SuoLabe").GetComponent<UILabel>().text = HeroOpenLevelList[0] + "级解锁";
                TopItem.name="TopItem"+i.ToString();

                GameObject quanGrid = NGUITools.AddChild(ScrollView0, QuanGrid);
                quanGrid.SetActive(true);
                quanGrid.transform.localPosition = new Vector3(-340f, TopItem.transform.localPosition.y - 140f, 0);
                quanGrid.name="quanGrid"+i.ToString();

                for (int x = 0; x < HeroInfoList.Count; x++)
                {
                    GameObject go = NGUITools.AddChild(quanGrid, HeroItem);
                    go.SetActive(true);
                    go.name = HeroInfoList[x].heroID.ToString();
                    SetRarityIcon(go.transform.Find("SpriteRare").GetComponent<UISprite>(), HeroInfoList[x].heroRarity);
                    go.transform.Find("HeroIcon").GetComponent<UISprite>().spriteName = HeroInfoList[x].heroID.ToString();
                    go.transform.Find("HeroName").GetComponent<UILabel>().text = HeroInfoList[x].heroName;

                    UIEventListener.Get(go).onClick = delegate(GameObject obj)
                    {
                        UIManager.instance.OpenSinglePanel("CardDetailInfoWindow", false);
                        GameObject.Find("CardDetailInfoWindow").GetComponent<CardDetailInfoWindow>().SetDetaiInfo(int.Parse(go.name));
                    };
                }
                quanGrid.GetComponent<UIGrid>().Reposition();
            }
            else 
            {
                Transform Qt = ScrollView0.transform.Find(("quanGrid" + (i - 1).ToString()));
                Debug.Log(Qt.name);
                float positiony = Qt.localPosition.y - ((Qt.childCount / 5 + 1) * 220f)+90f;

                GameObject TopItem = NGUITools.AddChild(ScrollView0, TopSpriteItem);
                TopItem.SetActive(true);
                TopItem.transform.localPosition = new Vector3(-368f, positiony, 0f);
                TopItem.transform.Find("SuoLabe").GetComponent<UILabel>().text = HeroOpenLevelList[i] + "级解锁";
                TopItem.name = "TopItem" + i.ToString();

                GameObject quanGrid = NGUITools.AddChild(ScrollView0, QuanGrid);
                quanGrid.SetActive(true);
                quanGrid.transform.localPosition = new Vector3(-340f, TopItem.transform.localPosition.y - 140f, 0);
                quanGrid.name = "quanGrid" + i.ToString();
                for (int x = 0; x < HeroInfoList.Count; x++)
                {
                    GameObject go = NGUITools.AddChild(quanGrid, HeroItem);
                    go.SetActive(true);
                    go.name = HeroInfoList[x].heroID.ToString();
                    SetRarityIcon(go.transform.Find("SpriteRare").GetComponent<UISprite>(), HeroInfoList[x].heroRarity);
                    go.transform.Find("HeroIcon").GetComponent<UISprite>().spriteName = HeroInfoList[x].heroID.ToString();
                    go.transform.Find("HeroName").GetComponent<UILabel>().text = HeroInfoList[x].heroName;

                    UIEventListener.Get(go).onClick = delegate(GameObject obj)
                    {
                        UIManager.instance.OpenSinglePanel("CardDetailInfoWindow", false);
                        GameObject.Find("CardDetailInfoWindow").GetComponent<CardDetailInfoWindow>().SetDetaiInfo(int.Parse(go.name));
                    };
                }
                quanGrid.GetComponent<UIGrid>().Reposition();
            }
        }
    }





    private void DisplayHero(int _heroCarrerType) // 1 功 2 防 3特 
    {
        HeroOpenLevelList.Clear();
        NewPreviewList.Clear();
        ScrollView1.GetComponent<UIScrollView>().ResetPosition();
        for (int i = uiGrid.transform.childCount - 1; i >= 0; i--) 
        {
            DestroyImmediate(uiGrid.transform.GetChild(i).gameObject);
        }
        ScrollView0.SetActive(false);
        ScrollView1.SetActive(true);
        ScrollView2.SetActive(false);

        ScrollBar0.SetActive(false);
        ScrollBar1.SetActive(true);
        ScrollBar2.SetActive(false);
        ScrollBar1.GetComponent<UIScrollBar>().value = 0;
        //ScrollView1.GetComponent<UIScrollView>().ResetPosition();

        if (_heroCarrerType == 0) //全部
        {
            foreach (var item in NewGachaPreviewList)
            {
                if (item.ItemID.ToString()[0] == '6') 
                {
                    NewPreviewList.Add(item);
                }
            }
        }
        else  // 1 功 2 防 3特 
        {
            foreach (var item in NewGachaPreviewList)
            {
                if (item.ItemID.ToString()[0] == '6')
                {
                    HeroInfo heroinfo = TextTranslator.instance.GetHeroInfoByHeroID(item.ItemID);
                    if (heroinfo.heroCarrerType == _heroCarrerType)
                    {
                        NewPreviewList.Add(item);
                    }
                }
            }
        }

        foreach (var item in NewPreviewList)
        {
            if (HeroOpenLevelList.Contains(item.Level) == false)
            {
                HeroOpenLevelList.Add(item.Level); //等级划分
            }
        }

        int listCont = HeroOpenLevelList.Count;
        for (int m = 0; m < listCont; m++)  //等级从小到大
        {
            for (var n = listCont - 1; n > m; n--)
            {
                int levelA = HeroOpenLevelList[m];
                int levelB = HeroOpenLevelList[n];
                if (levelA > levelB)
                {
                    var temp = HeroOpenLevelList[m];
                    HeroOpenLevelList[m] = HeroOpenLevelList[n];
                    HeroOpenLevelList[n] = temp;
                }
            }
        }


        for (int i = 0; i < HeroOpenLevelList.Count; i++) //对应标签英雄划分
        {
            HeroInfoList.Clear();
            foreach (var item in NewPreviewList)
            {
                if (item.Level == HeroOpenLevelList[i])
                {
                    HeroInfo heroinfo = TextTranslator.instance.GetHeroInfoByHeroID(item.ItemID);
                    HeroInfoList.Add(heroinfo);
                }
            }

            int listSize = HeroInfoList.Count;
            for (int m = 0; m < listSize; m++)
            {
                for (var n = listSize - 1; n > m; n--)
                {
                    int heroA = HeroInfoList[m].heroRarity;
                    int heroB = HeroInfoList[n].heroRarity;
                    if (heroA < heroB)
                    {
                        var temp = HeroInfoList[m];
                        HeroInfoList[m] = HeroInfoList[n];
                        HeroInfoList[n] = temp;
                    }
                }
            }


            for (int x = 0; x < HeroInfoList.Count; x++)
            {
                GameObject go = NGUITools.AddChild(uiGrid, HeroItem);
                go.SetActive(true);
                go.name = HeroInfoList[x].heroID.ToString();
                SetRarityIcon(go.transform.Find("SpriteRare").GetComponent<UISprite>(), HeroInfoList[x].heroRarity);
                go.transform.Find("HeroIcon").GetComponent<UISprite>().spriteName = HeroInfoList[x].heroID.ToString();
                go.transform.Find("HeroName").GetComponent<UILabel>().text = HeroInfoList[x].heroName;

                UIEventListener.Get(go).onClick = delegate(GameObject obj)
                {
                    UIManager.instance.OpenSinglePanel("CardDetailInfoWindow", false);
                    GameObject.Find("CardDetailInfoWindow").GetComponent<CardDetailInfoWindow>().SetDetaiInfo(int.Parse(go.name));
                };
            }
        }
        uiGrid.GetComponent<UIGrid>().Reposition();
    }

    private void DisplayOther() 
    {
        HeroOpenLevelList.Clear();
        NewPreviewList.Clear();

        for (int i = uiGrid2.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGrid2.transform.GetChild(i).gameObject);
        }
        ScrollView0.SetActive(false);
        ScrollView1.SetActive(false);
        ScrollView2.SetActive(true);
        ScrollView2.GetComponent<UIScrollView>().ResetPosition();

        ScrollBar0.SetActive(false);
        ScrollBar1.SetActive(false);
        ScrollBar2.SetActive(true);
        ScrollBar2.GetComponent<UIScrollBar>().value = 0;

        foreach (var item in NewGachaPreviewList)
        {
            if (item.ItemID.ToString()[0] != '6') 
            {
                NewPreviewList.Add(item);
            }
        }

        foreach (var item in NewPreviewList)
        {
            if (HeroOpenLevelList.Contains(item.Level) == false)
            {
                HeroOpenLevelList.Add(item.Level); //等级划分
            }
        }

        int listCont = HeroOpenLevelList.Count;
        for (int m = 0; m < listCont; m++)  //等级从小到大
        {
            for (var n = listCont - 1; n > m; n--)
            {
                int levelA = HeroOpenLevelList[m];
                int levelB = HeroOpenLevelList[n];
                if (levelA > levelB)
                {
                    var temp = HeroOpenLevelList[m];
                    HeroOpenLevelList[m] = HeroOpenLevelList[n];
                    HeroOpenLevelList[n] = temp;
                }
            }
        }

        for (int i = 0; i < HeroOpenLevelList.Count; i++) //对应标签英雄划分
        {
            foreach (var item in NewPreviewList)
            {
                if (item.Level == HeroOpenLevelList[i])
                {
                    GameObject go = NGUITools.AddChild(uiGrid2, IconItem);
                    go.SetActive(true);
                    go.name = item.ItemID.ToString();
                    SetItemIcon(item.ItemID, go);
                    TextTranslator.instance.ItemDescription(go, item.ItemID,0);
                    if (GachaType == 2 && item.ItemID == 70030) 
                    {
                        go.transform.Find("TenInfo").gameObject.SetActive(true);
                    }
                }
            }
        }
        uiGrid2.GetComponent<UIGrid>().Reposition();
    }

    /// <summary>
    ///  设置稀有度
    /// </summary>
    /// <param name="_heroRarity"></param>
    void SetRarityIcon(UISprite rareSprite, int _heroRarity)
    {
        switch (_heroRarity)
        {
            case 1:
                rareSprite.spriteName = "word4";
                break;
            case 2:
                rareSprite.spriteName = "word5";
                break;
            case 3:
                rareSprite.spriteName = "word6";
                break;
            case 4:
                rareSprite.spriteName = "word7";
                break;
            case 5:
                rareSprite.spriteName = "word8";
                break;
            case 6:
                rareSprite.spriteName = "word9";
                break;
            default:
                break;
        }
    }


    void SetItemIcon(int _itemId,GameObject ItemObj)
    {
        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(_itemId);
        ItemObj.GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo.itemGrade.ToString();

        UISprite spriteIcon = ItemObj.transform.Find("Icon").GetComponent<UISprite>();
        GameObject suiPian = ItemObj.transform.Find("SuiPian").gameObject;
        ItemObj.transform.Find("ItemName").GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(_itemId);

        if (_itemId.ToString()[0] == '4')
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = _itemId.ToString();
            suiPian.SetActive(false);
        }
        else if (_itemId.ToString()[0] == '6')
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = _itemId.ToString();
            suiPian.SetActive(false);
        }
        else if (_itemId == 70000 || _itemId == 79999)
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = _itemId.ToString();
            suiPian.SetActive(true);
        }
        else if (_itemId.ToString()[0] == '7' && _itemId > 70000 && _itemId != 79999)
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = (_itemId - 10000).ToString();
            suiPian.SetActive(true);
        }
        else if (_itemId.ToString()[0] == '8')
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_itemId);
            //spriteIcon.spriteName = (ItemCode - 30000).ToString();
            spriteIcon.spriteName = mItemInfo.picID.ToString();
            suiPian.SetActive(true);
        }
        else if (_itemId.ToString()[0] == '2')
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_itemId);
            spriteIcon.spriteName = mItemInfo.picID.ToString();
            suiPian.SetActive(false);
        }
        else
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = _itemId.ToString();
            suiPian.SetActive(false);
        }
    }

}



