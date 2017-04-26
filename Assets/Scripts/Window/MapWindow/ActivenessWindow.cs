using UnityEngine;
using System.Collections;

public class ActivenessWindow : MonoBehaviour {

    public GameObject uigrid;
    public GameObject awardItem;
    public GameObject GetButton;
    public GameObject GetedButton;
    public GameObject Mask;
    public UILabel titleDes;
    public UILabel LabelDes;
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;

	void Start () {
        if (UIEventListener.Get(Mask).onClick == null)
        {
            UIEventListener.Get(Mask).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }
	}

    public void SetHappyBoxIsOpen(int HappyBoxID,int BoxGate) 
    {
        if (BoxGate == 0) 
        {
            GetButton.SetActive(false);
            GetedButton.SetActive(true);
            GetedButton.transform.Find("LabelGet").GetComponent<UILabel>().text = "领取";
        }
        else if (BoxGate == 1) 
        {
            GetButton.SetActive(true);
            GetedButton.SetActive(false);
            if (UIEventListener.Get(GetedButton).onClick == null) 
            {
                UIEventListener.Get(GetedButton).onClick += delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess(string.Format("1205#{0};",HappyBoxID));
                };
            }
        }
        else if (BoxGate == 2) 
        {
            GetButton.SetActive(false);
            GetedButton.SetActive(true);
            GetedButton.transform.Find("LabelGet").GetComponent<UILabel>().text = "已领取";
        }

        if (HappyBoxID == 1) 
        {
            LabelDes.text = "积分达到10积分可以领取";
        }
        else if (HappyBoxID == 2) 
        {
            LabelDes.text = "积分达到40积分可以领取";
        }
        else if (HappyBoxID == 3) 
        {
            LabelDes.text = "积分达到70积分可以领取";
        }
        else if (HappyBoxID == 4) 
        {
            LabelDes.text = "积分达到100积分可以领取";
        }

        TextTranslator.ItemInfo _ItemInfo1 = TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetHappyBoxListByID(HappyBoxID).ItemID1);
        GameObject item1 = NGUITools.AddChild(uigrid, awardItem);
        item1.SetActive(true);
        item1.transform.Find("GoodsBG").GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo1.itemGrade.ToString();
        //item1.transform.Find("GoodsBG").transform.Find("GoodsIcon").GetComponent<UISprite>().spriteName = _ItemInfo1.itemCode.ToString();
        SetItemIcon(item1.transform.Find("GoodsBG").transform.Find("GoodsIcon").GetComponent<UISprite>(), _ItemInfo1.itemCode);
        item1.transform.Find("GoodsBG").transform.Find("LabelNum").GetComponent<UILabel>().text = TextTranslator.instance.GetHappyBoxListByID(HappyBoxID).ItemNum1.ToString();
        item1.transform.Find("GoodsName").GetComponent<UILabel>().text = _ItemInfo1.itemName;
        item1.GetComponent<ItemExplanation>().SetAwardItem(_ItemInfo1.itemCode, 0);

        TextTranslator.ItemInfo _ItemInfo2 = TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetHappyBoxListByID(HappyBoxID).ItemID2);
        GameObject item2 = NGUITools.AddChild(uigrid, awardItem);
        item2.SetActive(true);
        item2.transform.Find("GoodsBG").GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo2.itemGrade.ToString();
        //item2.transform.Find("GoodsBG").transform.Find("GoodsIcon").GetComponent<UISprite>().spriteName = _ItemInfo2.itemCode.ToString();
        SetItemIcon(item2.transform.Find("GoodsBG").transform.Find("GoodsIcon").GetComponent<UISprite>(), _ItemInfo2.itemCode);
        item2.transform.Find("GoodsBG").transform.Find("LabelNum").GetComponent<UILabel>().text = TextTranslator.instance.GetHappyBoxListByID(HappyBoxID).ItemNum2.ToString();
        item2.transform.Find("GoodsName").GetComponent<UILabel>().text = _ItemInfo2.itemName;
        item2.GetComponent<ItemExplanation>().SetAwardItem(_ItemInfo2.itemCode, 0);

        TextTranslator.ItemInfo _ItemInfo3 = TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetHappyBoxListByID(HappyBoxID).ItemID3);
        GameObject item3 = NGUITools.AddChild(uigrid, awardItem);
        item3.SetActive(true);
        item3.transform.Find("GoodsBG").GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo3.itemGrade.ToString();
        //item3.transform.Find("GoodsBG").transform.Find("GoodsIcon").GetComponent<UISprite>().spriteName = _ItemInfo3.itemCode.ToString();
        SetItemIcon(item3.transform.Find("GoodsBG").transform.Find("GoodsIcon").GetComponent<UISprite>(), _ItemInfo3.itemCode);
        item3.transform.Find("GoodsBG").transform.Find("LabelNum").GetComponent<UILabel>().text = TextTranslator.instance.GetHappyBoxListByID(HappyBoxID).ItemNum3.ToString();
        item3.transform.Find("GoodsName").GetComponent<UILabel>().text = _ItemInfo3.itemName;
        item3.GetComponent<ItemExplanation>().SetAwardItem(_ItemInfo3.itemCode, 0);

        TextTranslator.ItemInfo _ItemInfo4 = TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetHappyBoxListByID(HappyBoxID).ItemID4);
        GameObject item4 = NGUITools.AddChild(uigrid, awardItem);
        item4.SetActive(true);
        item4.transform.Find("GoodsBG").GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo4.itemGrade.ToString();
        //item4.transform.Find("GoodsBG").transform.Find("GoodsIcon").GetComponent<UISprite>().spriteName = _ItemInfo4.itemCode.ToString();
        SetItemIcon(item4.transform.Find("GoodsBG").transform.Find("GoodsIcon").GetComponent<UISprite>(), _ItemInfo4.itemCode);
        item4.transform.Find("GoodsBG").transform.Find("LabelNum").GetComponent<UILabel>().text = TextTranslator.instance.GetHappyBoxListByID(HappyBoxID).ItemNum4.ToString();
        item4.transform.Find("GoodsName").GetComponent<UILabel>().text = _ItemInfo4.itemName;
        item4.GetComponent<ItemExplanation>().SetAwardItem(_ItemInfo4.itemCode, 0);

        if (TextTranslator.instance.GetHappyBoxListByID(HappyBoxID).ItemID5 != 0) 
        {
            TextTranslator.ItemInfo _ItemInfo5 = TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetHappyBoxListByID(HappyBoxID).ItemID5);
            GameObject item5 = NGUITools.AddChild(uigrid, awardItem);
            item5.SetActive(true);
            item5.transform.Find("GoodsBG").GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo5.itemGrade.ToString();
            //item4.transform.Find("GoodsBG").transform.Find("GoodsIcon").GetComponent<UISprite>().spriteName = _ItemInfo4.itemCode.ToString();
            SetItemIcon(item5.transform.Find("GoodsBG").transform.Find("GoodsIcon").GetComponent<UISprite>(), _ItemInfo5.itemCode);
            item5.transform.Find("GoodsBG").transform.Find("LabelNum").GetComponent<UILabel>().text = TextTranslator.instance.GetHappyBoxListByID(HappyBoxID).ItemNum5.ToString();
            item5.transform.Find("GoodsName").GetComponent<UILabel>().text = _ItemInfo5.itemName;
            item5.GetComponent<ItemExplanation>().SetAwardItem(_ItemInfo4.itemCode, 0);
        }
        if (TextTranslator.instance.GetHappyBoxListByID(HappyBoxID).ItemID6 != 0)
        {
            TextTranslator.ItemInfo _ItemInfo6 = TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetHappyBoxListByID(HappyBoxID).ItemID6);
            GameObject item6 = NGUITools.AddChild(uigrid, awardItem);
            item6.SetActive(true);
            item6.transform.Find("GoodsBG").GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo6.itemGrade.ToString();
            //item4.transform.Find("GoodsBG").transform.Find("GoodsIcon").GetComponent<UISprite>().spriteName = _ItemInfo4.itemCode.ToString();
            SetItemIcon(item6.transform.Find("GoodsBG").transform.Find("GoodsIcon").GetComponent<UISprite>(), _ItemInfo6.itemCode);
            item6.transform.Find("GoodsBG").transform.Find("LabelNum").GetComponent<UILabel>().text = TextTranslator.instance.GetHappyBoxListByID(HappyBoxID).ItemNum6.ToString();
            item6.transform.Find("GoodsName").GetComponent<UILabel>().text = _ItemInfo6.itemName;
            item6.GetComponent<ItemExplanation>().SetAwardItem(_ItemInfo6.itemCode, 0);
        }
        uigrid.GetComponent<UIGrid>().Reposition();
    }
    public void SetLegionContributeBoxIsOpen(int legionLevel,int HappyBoxID, int BoxGate)
    {
        titleDes.text = "军团捐献奖励";
        if (BoxGate == 0)
        {
            GetButton.SetActive(false);
            GetedButton.SetActive(true);
            GetedButton.transform.Find("LabelGet").GetComponent<UILabel>().text = "领取";
        }
        else if (BoxGate == 1)
        {
            GetButton.SetActive(true);
            GetedButton.SetActive(false);
           /* if (UIEventListener.Get(GetedButton).onClick == null)
            {
                UIEventListener.Get(GetedButton).onClick += delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess(string.Format("1205#{0};", HappyBoxID));
                };
            }*/
        }
        else if (BoxGate == 2)
        {
            GetButton.SetActive(false);
            GetedButton.SetActive(true);
            GetedButton.transform.Find("LabelGet").GetComponent<UILabel>().text = "已领取";
        }

        if (HappyBoxID == 1)
        {
            LabelDes.text = "贡献进度达到10可以领取";
        }
        else if (HappyBoxID == 2)
        {
            LabelDes.text = "贡献进度达到20可以领取";
        }
        else if (HappyBoxID == 3)
        {
            LabelDes.text = "贡献进度达到30可以领取";
        }
        else if (HappyBoxID == 4)
        {
            LabelDes.text = "贡献进度达到50可以领取";
        }
        Legion _myLegion = TextTranslator.instance.GetLegionByID(legionLevel);
        TextTranslator.ItemInfo _ItemInfo1 = TextTranslator.instance.GetItemByItemCode(_myLegion.BoxAwardList[HappyBoxID - 1].itemCode);
        GameObject item1 = NGUITools.AddChild(uigrid, awardItem);
        item1.SetActive(true);
        item1.transform.Find("GoodsBG").GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo1.itemGrade.ToString();
        SetItemIcon(item1.transform.Find("GoodsBG").transform.Find("GoodsIcon").GetComponent<UISprite>(), _ItemInfo1.itemCode);
        item1.transform.Find("GoodsBG").transform.Find("LabelNum").GetComponent<UILabel>().text = _myLegion.BoxAwardList[HappyBoxID - 1].itemCount.ToString();
        item1.transform.Find("GoodsName").GetComponent<UILabel>().text = _ItemInfo1.itemName;
        item1.GetComponent<ItemExplanation>().SetAwardItem(_ItemInfo1.itemCode, 0);

       /* TextTranslator.ItemInfo _ItemInfo2 = TextTranslator.instance.GetItemByItemCode(_myLegion.BoxAwardList[1].itemCode);
        GameObject item2 = NGUITools.AddChild(uigrid, awardItem);
        item2.SetActive(true);
        item2.transform.Find("GoodsBG").GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo2.itemGrade.ToString();
        SetItemIcon(item2.transform.Find("GoodsBG").transform.Find("GoodsIcon").GetComponent<UISprite>(), _ItemInfo2.itemCode);
        item2.transform.Find("GoodsBG").transform.Find("LabelNum").GetComponent<UILabel>().text = _myLegion.BoxAwardList[1].itemCount.ToString();
        item2.transform.Find("GoodsName").GetComponent<UILabel>().text = _ItemInfo2.itemName;
        item2.GetComponent<ItemExplanation>().SetAwardItem(_ItemInfo2.itemCode, 0);

        TextTranslator.ItemInfo _ItemInfo3 = TextTranslator.instance.GetItemByItemCode(_myLegion.BoxAwardList[2].itemCode);
        GameObject item3 = NGUITools.AddChild(uigrid, awardItem);
        item3.SetActive(true);
        item3.transform.Find("GoodsBG").GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo3.itemGrade.ToString();
        SetItemIcon(item3.transform.Find("GoodsBG").transform.Find("GoodsIcon").GetComponent<UISprite>(), _ItemInfo3.itemCode);
        item3.transform.Find("GoodsBG").transform.Find("LabelNum").GetComponent<UILabel>().text = _myLegion.BoxAwardList[2].itemCount.ToString();
        item3.transform.Find("GoodsName").GetComponent<UILabel>().text = _ItemInfo3.itemName;
        item3.GetComponent<ItemExplanation>().SetAwardItem(_ItemInfo3.itemCode, 0);

        TextTranslator.ItemInfo _ItemInfo4 = TextTranslator.instance.GetItemByItemCode(_myLegion.BoxAwardList[3].itemCode);
        GameObject item4 = NGUITools.AddChild(uigrid, awardItem);
        item4.SetActive(true);
        item4.transform.Find("GoodsBG").GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo4.itemGrade.ToString();
        SetItemIcon(item4.transform.Find("GoodsBG").transform.Find("GoodsIcon").GetComponent<UISprite>(), _ItemInfo4.itemCode);
        item4.transform.Find("GoodsBG").transform.Find("LabelNum").GetComponent<UILabel>().text = _myLegion.BoxAwardList[3].itemCount.ToString();
        item4.transform.Find("GoodsName").GetComponent<UILabel>().text = _ItemInfo4.itemName;
        item4.GetComponent<ItemExplanation>().SetAwardItem(_ItemInfo4.itemCode, 0);*/

        UIGrid _UIGrid = uigrid.GetComponent<UIGrid>();
        _UIGrid.Reposition(); 
        //_UIGrid.sorting = UIGrid.Sorting.Horizontal;
        //_UIGrid.pivot = UIWidget.Pivot.Center;
        //_UIGrid.Reposition(); 
        //_UIGrid.animateSmoothly = true;
    }
    void SetItemIcon(UISprite spriteIcon, int ItemCode) 
    {
        if (ItemCode.ToString()[0] == '4')
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = (ItemCode - 10000).ToString();
        }
        else if (ItemCode == 70000 || ItemCode == 79999)
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
        }
        else if (ItemCode.ToString()[0] == '7' && ItemCode > 70000)
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = (ItemCode - 10000).ToString();
        }
        else if (ItemCode.ToString()[0] == '8')
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = (ItemCode - 30000).ToString();
        }
        else if (ItemCode.ToString()[0] == '2')
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
            spriteIcon.spriteName = mItemInfo.picID.ToString();
        }
        else
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
        }
    }
}
