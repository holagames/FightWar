using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WayWindow : MonoBehaviour
{
    public static int NeedItemCount;
    public GameObject uiGrid;
    public GameObject ItemPrafeb;
    public GameObject HeadItem;
    public GameObject CountLabel;
    public GameObject DesLabel;
    public GameObject NameLabel;
    public GameObject ComposeButton;

    public GameObject GaChaItem1;
    public GameObject ShopItem1;

    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;

    public GameObject SpritePiece;

    public UISprite GoodsBG;
    public UISprite GoodsFG;
    public UILabel LabelName;
    public UILabel LabelNum;
    public UILabel LabelWait;



    int curItem = 0;
    public  int itemid = 0;
    List<GameObject> fubenItemList = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        UIEventListener.Get(GameObject.Find("WayCloseButton")).onClick += delegate(GameObject go)
        {
            //GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetHeroClick(-1);
            //UIManager.instance.BackUI();
            Destroy(this.gameObject);
        };
        //UIEventListener.Get(GaChaItem1).onClick += delegate(GameObject go)
        //{
        //    UIManager.instance.OpenPanel("GachaWindow", false);
        //};

        //UIEventListener.Get(ShopItem1).onClick += delegate(GameObject go)
        //{
        //    UIManager.instance.OpenPanel("RankShopWindow", false);
        //};
    }

    public void UpdateItemInfo()
    {
        TextTranslator.ItemInfo itemInfo = TextTranslator.instance.GetItemByItemCode(itemid);
        int count = TextTranslator.instance.GetItemCountByID(itemid);
        if (itemInfo.ComposeNumber == 0)
        {
            CountLabel.GetComponent<UILabel>().text = string.Format("拥有： + {0}", count);
        }
        else
            CountLabel.GetComponent<UILabel>().text = string.Format("拥有： + {0}/{1}", count, itemInfo.ComposeNumber);
    }

    public void SetWayInfo(int _ItemID)
    {
        itemid = _ItemID;
        /*  if (_ItemID > 60000 && _ItemID < 70000)
          {
              _ItemID += 10000;
              GoodsFG.atlas = RoleAtlas;
          }
          else
          {
              GoodsFG.atlas = ItemAtlas;
          }*/
        int _isShowNum = 0;
        TextTranslator.ItemInfo itemInfo = TextTranslator.instance.GetItemByItemCode(_ItemID);
        if (_ItemID.ToString()[0] == '5')
        {
            GoodsFG.atlas = ItemAtlas;
            GoodsFG.spriteName = _ItemID.ToString();
        }
        else if (_ItemID.ToString()[0] == '6')
        {
            _ItemID += 10000;
            GoodsFG.atlas = RoleAtlas;
        }
        else if (_ItemID.ToString()[0] == '7')
        {
            // int _id = 70000;
            if (_ItemID != 70000 && _ItemID != 79999)
            {
                _ItemID = _ItemID - 10000;
                GoodsFG.atlas = RoleAtlas;
            }
            else
            {
                GoodsFG.atlas = ItemAtlas;
            }
            GoodsFG.spriteName = (_ItemID).ToString();
            SpritePiece.SetActive(true);
            SpritePiece.GetComponent<UISprite>().spriteName = "grab_ui4_icon5";
            _isShowNum = 1;
        }
        else if (_ItemID.ToString()[0] == '4')
        {
            GoodsFG.atlas = ItemAtlas;
            GoodsFG.spriteName = (_ItemID).ToString();
            //SpritePiece.SetActive(true);
        }
        else if (_ItemID.ToString()[0] == '8')
        {
            GoodsFG.atlas = ItemAtlas;
            GoodsFG.spriteName = TextTranslator.instance.GetItemByItemCode(_ItemID).picID.ToString();// (((_ItemID - 30000) / 10) * 10).ToString();
            SpritePiece.SetActive(true);
            SetSuiPianIcon(_ItemID.ToString()[4]);
        }
        else if (_ItemID.ToString()[0] == '2')
        {
            GoodsFG.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_ItemID);
            GoodsFG.spriteName = mItemInfo.picID.ToString();
        }
        else
        {
            GoodsFG.atlas = ItemAtlas;
            GoodsFG.spriteName = _ItemID.ToString();
        }
        //   SetNewColor(GoodsBG.gameObject,itemInfo.itemGrade - 1);
        GoodsBG.spriteName = GetFrameSpriteName(_ItemID);
        LabelName.text = itemInfo.itemName;
        int count = TextTranslator.instance.GetItemCountByID(_ItemID);
        if (NeedItemCount == 0)
        {

            LabelNum.text = "获取：" + count;
        }
        else
            LabelNum.text = "获取：" + count + "/" + NeedItemCount.ToString();

        if (itemInfo.Source1 != "0")
        {
            AddWayItem(itemInfo.Source1, 1, _isShowNum);
        }
        if (itemInfo.Source2 != "0")
        {
            //核武器
            if (itemInfo.Source2 == "22")
            {
                if (itemInfo.itemCode / 10000 == 5)
                {
                    CharacterRecorder.instance.heroPresentWeapon = itemInfo.itemCode + 30000;
                }
                else
                {
                    CharacterRecorder.instance.heroPresentWeapon = itemInfo.itemCode;
                }
                Debug.LogError("heroPresentWeapon::" + CharacterRecorder.instance.heroPresentWeapon);
            }
            AddWayItem(itemInfo.Source2, 2);
        }
        if (itemInfo.Source3 != "0")
        {
        }

        //if (itemInfo.Source1 != "0")
        //{
        //    //GameObject go = NGUITools.AddChild(uiGrid, ItemPrafeb);
        //    ////go.GetComponent<WayFubenItem>().SetInfo(itemInfo.Source1);
        //    //go.GetComponent<WayItem>().Init(itemInfo.Source1, 1);
        //    //go.name = "WayFubenItem1";

        //    //fubenItemList.Add(go);

        //    AddWayItem(itemInfo.Source1,1);
        //}
        //if (itemInfo.Source2 != "0")
        //{
        //    //GameObject go = NGUITools.AddChild(uiGrid, ItemPrafeb);
        //    ////go.GetComponent<WayFubenItem>().SetInfo(itemInfo.Source2);
        //    //go.GetComponent<WayItem>().Init(itemInfo.Source2, 2);
        //    //go.name = "WayFubenItem2";
        //    //fubenItemList.Add(go);

        //    AddWayItem(itemInfo.Source2, 2);
        //}
        //if (itemInfo.Source3 != "0")
        //{
        //    //GameObject go = NGUITools.AddChild(uiGrid, ItemPrafeb);
        //    ////go.GetComponent<WayFubenItem>().SetInfo(itemInfo.Source3);
        //    //go.name = "WayFubenItem3";
        //    //fubenItemList.Add(go);
        //}

    }

    public void SetSuiPianIcon(char charIcon)
    {
        switch (charIcon)
        {
            case '0':
                //SuiPian.GetComponent<UISprite>().spriteName = "0";
                SpritePiece.GetComponent<UISprite>().spriteName = "grab_ui4_icon2";
                break;
            case '1':
                SpritePiece.GetComponent<UISprite>().spriteName = "grab_ui4_icon3";
                break;
            case '2':
                SpritePiece.GetComponent<UISprite>().spriteName = "grab_ui4_icon4";
                break;
            case '3':
                SpritePiece.GetComponent<UISprite>().spriteName = "grab_ui4_icon5";
                break;
            case '4':
                SpritePiece.GetComponent<UISprite>().spriteName = "grab_ui4_icon1";
                break;
            case '5':
                SpritePiece.GetComponent<UISprite>().spriteName = "grab_ui4_icon6";
                break;
            default:
                SpritePiece.GetComponent<UISprite>().spriteName = "grab_ui4_icon5";
                break;
        }
    }
    /// <summary>
    /// 添加前往Item
    /// </summary>
    /// <param name="source"></param>
    /// <param name="sourceNum"></param>
    /// <param name="isShowNum">0- 不显示次数 1- 显示次数</param>
    void AddWayItem(string source, int sourceNum, int isShowNum = 0)
    {
        if (CharacterRecorder.instance.bagItemOpenWindows.ContainsKey(itemid.ToString()))
        {
            CharacterRecorder.instance.bagItemOpenWindows.Remove(itemid.ToString());
        }
        CharacterRecorder.instance.bagItemOpenWindows.Add(itemid.ToString(), itemid);

        string[] SourceIDs = source.Split('$');
        if (sourceNum == 1)
        {
            foreach (var id in SourceIDs)
            {
                GameObject go = NGUITools.AddChild(uiGrid, ItemPrafeb);
                go.GetComponent<WayItem>().Init(id, 1, isShowNum);
                go.name = "WayFubenItem1_" + id;
            }
        }
        else if (sourceNum == 2)
        {
            foreach (var num in SourceIDs)
            {
                if (num != "20")
                {

                    GameObject go = NGUITools.AddChild(uiGrid, ItemPrafeb);
                    go.GetComponent<WayItem>().Init(num, sourceNum);
                    go.name = "WayFubenItem2";
                }
                else
                {
                    LabelWait.gameObject.SetActive(true);
                }
            }
        }
    }
    #region 没有用
    public void SetInfo(int ItemID)
    {
        itemid = ItemID;
        /* if (ItemID > 30000 && ItemID < 40000)
         {
             curItem = ItemID + 10000;
             HeadItem.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
         }
         else if (ItemID > 60000 && ItemID < 70000)
         {
             curItem = ItemID + 10000;
             HeadItem.transform.FindChild("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
         }
         else if (ItemID > 50000 && ItemID < 60000)
         {
             curItem = ItemID + 30000;
             HeadItem.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
         }
         else
         {
             curItem = ItemID;
         }*/
        if (ItemID.ToString()[0] == '5')
        {
            GoodsFG.spriteName = ItemID.ToString();
        }
        else if (ItemID.ToString()[0] == '7')
        {
            GoodsFG.atlas = RoleAtlas;
            GoodsFG.spriteName = (ItemID - 10000).ToString();
            SpritePiece.SetActive(true);
        }
        else if (ItemID.ToString()[0] == '4')
        {
            GoodsFG.spriteName = (ItemID - 10000).ToString();
            SpritePiece.SetActive(true);
        }
        else if (ItemID.ToString()[0] == '8')
        {
            GoodsFG.spriteName = (((ItemID - 30000) / 10) * 10).ToString();
            SpritePiece.SetActive(true);
        }
        else if (ItemID.ToString()[0] == '2')
        {
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(ItemID);
            GoodsFG.spriteName = mItemInfo.picID.ToString();
        }
        else
        {
            GoodsFG.spriteName = ItemID.ToString();
        }
        TextTranslator.ItemInfo itemInfo = TextTranslator.instance.GetItemByItemCode(ItemID);
        DesLabel.GetComponent<UILabel>().text = itemInfo.itemDescription;
        NameLabel.GetComponent<UILabel>().text = itemInfo.itemName;
        SetColor(HeadItem, itemInfo.itemGrade);
        GoodsFG.spriteName = ItemID.ToString();
        int count = TextTranslator.instance.GetItemCountByID(ItemID);
        CountLabel.GetComponent<UILabel>().text = string.Format("拥有{0}件", count);

        if (UIEventListener.Get(ComposeButton).onClick == null)
        {
            UIEventListener.Get(ComposeButton).onClick += delegate(GameObject go)
            {
                //NetworkHandler.instance.SendProcess("5008#" + curItem + ";");
            };
        }

        if (fubenItemList.Count > 0)
        {
            for (int i = 0; i < fubenItemList.Count; i++)
            {
                DestroyImmediate(fubenItemList[i]);
            }
            fubenItemList.Clear();
        }

        Debug.LogError(itemInfo.Source1);
        if (itemInfo.Source1 != "0")
        {
            GameObject go = NGUITools.AddChild(uiGrid, ItemPrafeb);
            //go.GetComponent<WayFubenItem>().SetInfo(itemInfo.Source1);
            go.name = "WayFubenItem1";
            fubenItemList.Add(go);
        }
        if (itemInfo.Source2 != "0")
        {
            GameObject go = NGUITools.AddChild(uiGrid, ItemPrafeb);
            //go.GetComponent<WayFubenItem>().SetInfo(itemInfo.Source2);
            go.name = "WayFubenItem2";
            fubenItemList.Add(go);
        }
        if (itemInfo.Source3 != "0")
        {
            GameObject go = NGUITools.AddChild(uiGrid, ItemPrafeb);
            //go.GetComponent<WayFubenItem>().SetInfo(itemInfo.Source3);
            go.name = "WayFubenItem3";
            fubenItemList.Add(go);
        }
        NetworkHandler.instance.SendProcess("5009#" + ItemID + ";");
    }
    void SetColor(GameObject obj, int number)
    {
        switch (number)
        {
            case 1:
                obj.GetComponent<UISprite>().spriteName = "Grade1";
                break;
            case 2:
                obj.GetComponent<UISprite>().spriteName = "Grade2";
                break;
            case 3:
                obj.GetComponent<UISprite>().spriteName = "Grade3";
                break;
            case 4:
                obj.GetComponent<UISprite>().spriteName = "Grade4";
                break;
            case 5:
                obj.GetComponent<UISprite>().spriteName = "Grade5";
                break;
            default:
                break;
        }
    }
    #endregion

    public void SetOpenFuben(string Recive)
    {
        string[] dataSplit = Recive.Split(';');
        if (dataSplit[0] == "0")
        {
            WayFubenItem wfi = GameObject.Find("WayFubenItem1").GetComponent<WayFubenItem>();
            wfi.Lock.SetActive(true);
            wfi.FuBenIcon.spriteName = wfi.FuBenIcon.spriteName.Substring(0, 1) + "_hui";
            GameObject.Find("WayFubenItem1").GetComponent<BoxCollider>().size = Vector3.zero;
        }
        if (dataSplit[1] == "0")
        {
            WayFubenItem wfi = GameObject.Find("WayFubenItem2").GetComponent<WayFubenItem>();
            wfi.Lock.SetActive(true);
            wfi.FuBenIcon.spriteName = wfi.FuBenIcon.spriteName.Substring(0, 1) + "_hui";
            GameObject.Find("WayFubenItem2").GetComponent<BoxCollider>().size = Vector3.zero;
        }
        if (dataSplit[2] == "0")
        {
            WayFubenItem wfi = GameObject.Find("WayFubenItem3").GetComponent<WayFubenItem>();
            wfi.Lock.SetActive(true);
            wfi.FuBenIcon.spriteName = wfi.FuBenIcon.spriteName.Substring(0, 1) + "_hui";
            GameObject.Find("WayFubenItem3").GetComponent<BoxCollider>().size = Vector3.zero;
        }
    }


    void SetNewColor(GameObject obj, int number)
    {
        switch (number)
        {
            case 0:
                obj.GetComponent<UISprite>().spriteName = "yxdi0";
                break;
            case 1:
                obj.GetComponent<UISprite>().spriteName = "yxdi1";
                break;
            case 2:
                obj.GetComponent<UISprite>().spriteName = "yxdi2";
                break;
            case 3:
                obj.GetComponent<UISprite>().spriteName = "yxdi3";
                break;
            case 4:
                obj.GetComponent<UISprite>().spriteName = "yxdi4";
                break;
            case 5:
                obj.GetComponent<UISprite>().spriteName = "yxdi5";
                break;
            case 6:
                obj.GetComponent<UISprite>().spriteName = "yxdi6";
                break;
            default:
                break;
        }
    }
    string GetFrameSpriteName(int _ItemID)
    {
        string frameSpriteName = "";
        TextTranslator.ItemInfo itemInfo = TextTranslator.instance.GetItemByItemCode(_ItemID);
        if (_ItemID.ToString()[0] == '6')
        {
            switch (itemInfo.itemGrade - 1)
            {
                case 0:
                    frameSpriteName = "yxdi0";
                    break;
                case 1:
                    frameSpriteName = "yxdi1";
                    break;
                case 2:
                    frameSpriteName = "yxdi2";
                    break;
                case 3:
                    frameSpriteName = "yxdi3";
                    break;
                case 4:
                    frameSpriteName = "yxdi4";
                    break;
                case 5:
                    frameSpriteName = "yxdi5";
                    break;
                case 6:
                    frameSpriteName = "yxdi6";
                    break;
                default:
                    break;
            }
        }
        else
        {
            frameSpriteName = string.Format("Grade{0}", itemInfo.itemGrade);
        }
        return frameSpriteName;
    }
}
