using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class StoneInfoPart : MonoBehaviour 
{
    public UISlider _Slider;
    public UISlider _SliderAfterAdd;
    public UILabel LabelExp;
    public UILabel needMoneyLabel;
    public UILabel stoneNameLabel;
    public UILabel conditionLabel;
    public UISprite stoneIcon;
    public GameObject moveAwayButton;
    public GameObject conditionButton;
    public GameObject strengButton;
    public GameObject autoSelectButton;
    public UILabel LeftInfoLabelNew;
    public UILabel RightInfoLabelNew;
    public List<GameObject> ExpItemObjList;

    public List<Item> listSelectEquipItem = new List<Item>();
    public List<SelectItemCodeAndIndex> listSelectItemCodeAndIndex = new List<SelectItemCodeAndIndex>();//数据A

    private string InfoDesStr;
    private string DataStr;
    private int conditionGrade = 3;//默认蓝色以下
    private int curStoneGrade = 0;
    private int strengNeedMoney = 0;
    [HideInInspector]
    public static Hero.EquipInfoBefore mEquipInfoBefore = new Hero.EquipInfoBefore();
	// Use this for initialization
	void Start () 
    {
        UIEventListener.Get(moveAwayButton).onClick += delegate(GameObject go)
        {
         /*   UIManager.instance.OpenPromptWindowWithCost(String.Format("是否取下{0}?\n取下的秘宝将返还至邮箱,并包含所有已消耗的资源", GetItemName(SecretStoneWindow.curRareStone.stoneId,false)), 11, 500,
                PromptWindow.PromptType.Confirm, ConfirmMoveAwayStone, null);*/
            SecretStoneWindow _SecretStoneWindow = this.transform.parent.GetComponent<SecretStoneWindow>();
            _SecretStoneWindow.selectStonePart.SetActive(true);
            _SecretStoneWindow.secretStoneInfoPart.SetActive(false);
            _SecretStoneWindow.selectStonePart.GetComponent<SelectStonePart>().SetSelectStonePart(curStoneGrade);
            this.gameObject.SetActive(false);
        };
        UIEventListener.Get(autoSelectButton).onClick += delegate(GameObject go)
        {
            ClickAutoSelectButton();
        };
        UIEventListener.Get(conditionButton).onClick += delegate(GameObject go)
        {
            UIManager.instance.OpenPanel("SecretStoneConditionWindow", false);
            GameObject.Find("SecretStoneConditionWindow").layer = 11;
        };
        UIEventListener.Get(strengButton).onClick = delegate(GameObject go)
        {
            StrenghBaoWu();
        };
        for (int i = 0; i < ExpItemObjList.Count; i++)
        {
            if (UIEventListener.Get(ExpItemObjList[i]).onClick == null)
            {
                UIEventListener.Get(ExpItemObjList[i]).onClick += delegate(GameObject go)
                {
                    UIManager.instance.OpenPanel("EquipSelectWindowForStone", false);
                    GameObject _EquipSelectWindow = GameObject.Find("EquipSelectWindowForStone");
                    _EquipSelectWindow.layer = 11;
                    foreach (Component c in _EquipSelectWindow.GetComponentsInChildren(typeof(Transform), true))
                    {
                        c.gameObject.layer = 11;
                    }
                    return;
                };
            }
        }
       // SetStoneInfoPart();
	}
    public void SetStoneInfoPart()
    {
        stoneIcon.spriteName = SecretStoneWindow.curRareStone.stoneId.ToString();
        stoneIcon.transform.parent.GetComponent<UISprite>().spriteName = GetItemFrameName(SecretStoneWindow.curRareStone.stoneId);
        ClearSelectEquipItems();
        stoneNameLabel.text = GetItemName(SecretStoneWindow.curRareStone.stoneId,true);//, mHero.equipList[ClickIndex - 1].equipClass, mHero.equipList[ClickIndex - 1].equipColorNum);
        GetStrenghStr(SecretStoneWindow.curRareStone, false);
        LeftInfoLabelNew.text = InfoDesStr;
        LeftInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>().text = DataStr;

        GetStrenghStr(SecretStoneWindow.curRareStone, true);
        RightInfoLabelNew.text = InfoDesStr;
        RightInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>().text = DataStr;

        SetExpSlider(SecretStoneWindow.curRareStone);
    }
    string GetItemFrameName(int _itemId)
    {
        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(_itemId);
        //Debug.LogError("_itemId.." + _itemId + "..Item.." + _ItemInfo);
        curStoneGrade = _ItemInfo.itemGrade;
        return "Grade" + _ItemInfo.itemGrade.ToString();
    }
    void ClearSelectEquipItems()
    {
        listSelectEquipItem.Clear();
        listSelectItemCodeAndIndex.Clear();
        for (int i = 0; i < ExpItemObjList.Count; i++)
        {
            ExpItemObjList[i].GetComponent<UISprite>().spriteName = "Grade1";
            ExpItemObjList[i].transform.FindChild("Icon").GetComponent<UISprite>().spriteName = "add";
        }
    }
    #region 获得秘宝等级属性
    string GetStrenghStr(RareTreasureOpen curRareStone, bool useNext)
    {
        string desStr = "";
        RareTreasureAttr es = TextTranslator.instance.GetRareTreasureAttrByID(curRareStone.stoneId);

        int usefulClass = 0;
        if (!useNext)
        {
            usefulClass = curRareStone.stoneLevel;
        }
        else
        {
            usefulClass = curRareStone.stoneLevel + 1;
        }
        desStr = "等级:" + usefulClass.ToString() + "\n";
        InfoDesStr = "等级:" + "\n";
        DataStr = usefulClass.ToString() + "\n";
        if (es.Hp > 0)
        {
            string usefulNumStr = "";
            if (es.Hp * usefulClass < 1 && es.Hp * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Hp * usefulClass);
            }
            else
            {
                usefulNumStr = (es.Hp * usefulClass).ToString();
            }
            desStr += "生命:" + usefulNumStr + "\n";
            InfoDesStr += "生命:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.Atk > 0)
        {
            string usefulNumStr = "";
            if (es.Atk * usefulClass < 1 && es.Atk * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Atk * usefulClass);
            }
            else
            {
                usefulNumStr = (es.Atk * usefulClass).ToString();
            }
            desStr += "攻击:" + usefulNumStr + "\n";
            InfoDesStr += "攻击:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.Def > 0)
        {
            string usefulNumStr = "";
            if (es.Def * usefulClass/4 < 1 && es.Def * usefulClass/4 > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Def * usefulClass/4.0f);
            }
            else
            {
                usefulNumStr = (es.Def * usefulClass/4).ToString();
            }
            desStr += "防御:" + usefulNumStr + "\n";
            InfoDesStr += "防御:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.Hit > 0)
        {
            string usefulNumStr = "";
            if (es.Hit * usefulClass < 1 && es.Hit * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Hit * usefulClass);
            }
            else
            {
                usefulNumStr = (es.Hit * usefulClass).ToString();
            }
            desStr += "命中:" + usefulNumStr + "\n";
            InfoDesStr += "命中:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.NoHit > 0)
        {
            string usefulNumStr = "";
            if (es.NoHit * usefulClass < 1 && es.NoHit * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.NoHit * usefulClass);
            }
            else
            {
                usefulNumStr = (es.NoHit * usefulClass).ToString();
            }
            desStr += "闪避:" + usefulNumStr + "\n";
            InfoDesStr += "闪避:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.Crit > 0)
        {
            string usefulNumStr = "";
            if (es.Crit * usefulClass < 1 && es.Crit * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Crit * usefulClass);
            }
            else
            {
                usefulNumStr = (es.Crit * usefulClass).ToString();
            }
            desStr += "暴击:" + usefulNumStr + "\n";
            InfoDesStr += "暴击:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.NoCrit > 0)
        {
            string usefulNumStr = "";
            if (es.NoCrit * usefulClass < 1 && es.NoCrit * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.NoCrit * usefulClass);
            }
            else
            {
                usefulNumStr = (es.NoCrit * usefulClass).ToString();
            }
            desStr += "抗暴:" + usefulNumStr + "\n";
            InfoDesStr += "抗暴:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.DmgBonus > 0)
        {
            string usefulNumStr = "";
            if (es.DmgBonus * usefulClass < 1 && es.DmgBonus * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.DmgBonus * usefulClass);
            }
            else
            {
                usefulNumStr = (es.DmgBonus * usefulClass).ToString();
            }
            desStr += "伤害加成:" + usefulNumStr + "\n";
            InfoDesStr += "加成:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.DmgReduction > 0)
        {
            string usefulNumStr = "";
            if (es.DmgReduction * usefulClass < 1 && es.DmgReduction * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.DmgReduction * usefulClass);
            }
            else
            {
                usefulNumStr = (es.DmgReduction * usefulClass).ToString();
            }
            desStr += "伤害减免:" + usefulNumStr + "\n";
            InfoDesStr += "减免:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        return desStr;
    }
    string ChangeDecimalToPercent(float _Decimal)
    {
        return string.Format("{0}%", Math.Round(_Decimal * 100, 1));//_Decimal * 100
    }
    #endregion

    #region 以下只为宝石选择装备后预览
    public void SetEquipInfoOnlyForBaoWuStreng(int curEquipLv)
    {
        GetStrenghStr(SecretStoneWindow.curRareStone, curEquipLv);
        RightInfoLabelNew.text = InfoDesStr;
        RightInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>().text = DataStr;
    }
    string GetStrenghStr(RareTreasureOpen curRareStone, int useNextLevel)
    {
        string desStr = "";
        //EquipStrong es = TextTranslator.instance.GetEquipStrongByID(mEquipInfo.equipCode, mEquipInfo.equipColorNum);
        RareTreasureAttr es = TextTranslator.instance.GetRareTreasureAttrByID(curRareStone.stoneId);
        int usefulClass = 0;

        usefulClass = useNextLevel;

        desStr = "等级:" + usefulClass.ToString() + "\n";
        InfoDesStr = "等级:" + "\n";
        DataStr = usefulClass.ToString() + "\n";
        if (es.Hp > 0)
        {
            string usefulNumStr = "";
            if (es.Hp * usefulClass < 1 && es.Hp * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Hp * usefulClass);
            }
            else
            {
                usefulNumStr = (es.Hp * usefulClass).ToString();
            }
            desStr += "生命:" + usefulNumStr + "\n";
            InfoDesStr += "生命:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.Atk > 0)
        {
            string usefulNumStr = "";
            if (es.Atk * usefulClass < 1 && es.Atk * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Atk * usefulClass);
            }
            else
            {
                usefulNumStr = (es.Atk * usefulClass).ToString();
            }
            desStr += "攻击:" + usefulNumStr + "\n";
            InfoDesStr += "攻击:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.Def > 0)
        {
            string usefulNumStr = "";
            if (es.Def * usefulClass/4 < 1 && es.Def * usefulClass/4.0f > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Def * usefulClass/4.0f);
            }
            else
            {
                usefulNumStr = (es.Def * usefulClass/4).ToString();
            }
            desStr += "防御:" + usefulNumStr + "\n";
            InfoDesStr += "防御:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.Hit > 0)
        {
            string usefulNumStr = "";
            if (es.Hit * usefulClass < 1 && es.Hit * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Hit * usefulClass);
            }
            else
            {
                usefulNumStr = (es.Hit * usefulClass).ToString();
            }
            desStr += "命中:" + usefulNumStr + "\n";
            InfoDesStr += "命中:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.NoHit > 0)
        {
            string usefulNumStr = "";
            if (es.NoHit * usefulClass < 1 && es.NoHit * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.NoHit * usefulClass);
            }
            else
            {
                usefulNumStr = (es.NoHit * usefulClass).ToString();
            }
            desStr += "闪避:" + usefulNumStr + "\n";
            InfoDesStr += "闪避:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.Crit > 0)
        {
            string usefulNumStr = "";
            if (es.Crit * usefulClass < 1 && es.Crit * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Crit * usefulClass);
            }
            else
            {
                usefulNumStr = (es.Crit * usefulClass).ToString();
            }
            desStr += "暴击:" + usefulNumStr + "\n";
            InfoDesStr += "暴击:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.NoCrit > 0)
        {
            string usefulNumStr = "";
            if (es.NoCrit * usefulClass < 1 && es.NoCrit * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.NoCrit * usefulClass);
            }
            else
            {
                usefulNumStr = (es.NoCrit * usefulClass).ToString();
            }
            desStr += "抗暴:" + usefulNumStr + "\n";
            InfoDesStr += "抗暴:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.DmgBonus > 0)
        {
            string usefulNumStr = "";
            if (es.DmgBonus * usefulClass < 1 && es.DmgBonus * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.DmgBonus * usefulClass);
            }
            else
            {
                usefulNumStr = (es.DmgBonus * usefulClass).ToString();
            }
            desStr += "伤害加成:" + usefulNumStr + "\n";
            InfoDesStr += "加成:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        if (es.DmgReduction > 0)
        {
            string usefulNumStr = "";
            if (es.DmgReduction * usefulClass < 1 && es.DmgReduction * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.DmgReduction * usefulClass);
            }
            else
            {
                usefulNumStr = (es.DmgReduction * usefulClass).ToString();
            }
            desStr += "伤害减免:" + usefulNumStr + "\n";
            InfoDesStr += "减免:" + "\n";
            DataStr += usefulNumStr + "\n";
        }
        return desStr;
    }
    #endregion

    #region 获得秘宝名字
    string GetItemName(int mEquipCode,bool IsNeedNameColor)//, int _classNumber, int _colorNum)//_colorNum: 1 - 21
    {
        TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(mEquipCode);
        string _nameColor = "";
        if (IsNeedNameColor)
        {
            switch (mItemInfo.itemGrade)
            {
                case 1: _nameColor = "[ffffff]"; break;
                case 2: _nameColor = "[3ee817]"; break;
                case 3: _nameColor = "[8ccef2]"; break;
                case 4: _nameColor = "[bb44ff]"; break;
                case 5: _nameColor = "[ff8c04]"; break;
                case 6: _nameColor = "[fb2d50]"; break;
            }
        }
        int _NumAdd = 0;//GetNumAdd(mItemInfo.itemGrade, _colorNum);
        if (mItemInfo != null && _NumAdd == 0)
        {
            return string.Format("{0}{1}", _nameColor, mItemInfo.itemName);
        }
        else if (mItemInfo != null && _NumAdd >= 1)
        {
            return string.Format("{0}{1} + {2}", _nameColor, mItemInfo.itemName, _NumAdd);
        }
        else return "";
    }
    int GetNumAdd(int _itemGrade, int _colorNum)
    {
        int addNum = 0;
        switch (_itemGrade)
        {
            case 1:
                switch (_colorNum)
                {
                    case 0: addNum = 0; break;
                    case 1: addNum = 0; break;
                    case 2: addNum = 1; break;
                }
                break;
            case 2:
                switch (_colorNum)
                {
                    case 3: addNum = 0; break;
                    case 4: addNum = 1; break;
                    case 5: addNum = 2; break;
                }
                break;
            case 3:
                switch (_colorNum)
                {
                    case 6: addNum = 0; break;
                    case 7: addNum = 1; break;
                    case 8: addNum = 2; break;
                    case 9: addNum = 3; break;
                }
                break;
            case 4:
                switch (_colorNum)
                {
                    case 10: addNum = 0; break;
                    case 11: addNum = 1; break;
                    case 12: addNum = 2; break;
                    case 13: addNum = 3; break;
                    case 14: addNum = 4; break;
                }
                break;
            case 5:
                switch (_colorNum)
                {
                    case 15: addNum = 0; break;
                    case 16: addNum = 1; break;
                    case 17: addNum = 2; break;
                    case 18: addNum = 3; break;
                    case 19: addNum = 4; break;
                    case 20: addNum = 5; break;
                }
                break;
            case 6:
                switch (_colorNum)
                {
                    case 21: addNum = 0; break;
                    case 22: addNum = 1; break;
                }
                break;
        }
        return addNum;
    }
    #endregion

    #region 自动选择条件设置
    public void ResetAutoSelectCondition(int _conditionGrade)
    {
        conditionGrade = _conditionGrade;
        switch (_conditionGrade)
        {
            case 1: conditionLabel.text = "白色品质"; break;
            case 2: conditionLabel.text = "绿色及以下品质"; break;
            case 3: conditionLabel.text = "蓝色及以下品质"; break;
            case 4: conditionLabel.text = "紫色及以下品质"; break;
            //case 5: conditionLabel.text = "橙色及以下品质"; break;
            //case 6: conditionLabel.text = "红色及以下品质"; break;
        }
    }
    #endregion

    #region 自动添加按钮功能
    void ClickAutoSelectButton()
    {
        int TotalCount = 0;
        ClearSelectEquipItems();
        listSelectEquipItem.Clear();
        listSelectItemCodeAndIndex.Clear();
        BetterList<Item> usefulItemList = new BetterList<Item>();
        for (int i = 0; i < TextTranslator.instance.bagItemList.size; i++)
        {
            //选择装备材料
            if (TextTranslator.instance.bagItemList[i].itemCode >= 40000 && TextTranslator.instance.bagItemList[i].itemCode < 42000 && TextTranslator.instance.bagItemList[i].itemCount > 0)
            {
                usefulItemList.Add(TextTranslator.instance.bagItemList[i]);
            }
        }
        usefulItemList = GetSumFinalInBag(usefulItemList);
        for (int i = usefulItemList.size - 1; i >= 0; i--)
        {
            if (TotalCount < 5 && usefulItemList[i].itemGrade <= conditionGrade)
            {
                int _count = usefulItemList[i].itemCount >= 5 ? 5 : usefulItemList[i].itemCount;
                for (int j = 0; j < _count; j++)
                {
                    if (TotalCount < 5)
                    {
                        TotalCount++;
                        Debug.Log(usefulItemList[i].itemGrade);
                        GameObject.Find("EXPItem" + TotalCount.ToString()).GetComponent<UISprite>().spriteName = "Grade" + usefulItemList[i].itemGrade;
                        GameObject.Find("EXPItem" + TotalCount.ToString()).transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName = usefulItemList[i].itemCode.ToString();
                        listSelectItemCodeAndIndex.Add(new SelectItemCodeAndIndex(usefulItemList[i].itemCode, j));
                        listSelectEquipItem.Add(usefulItemList[i]);
                    }
                }
               // usefulItemList.Remove(usefulItemList[i]);
            }
        }

        //for (int i = usefulItemList.size - 1; i >= 0; i--)
        //{
        //    if (TotalCount < 5 && usefulItemList[i].itemGrade != 3 && usefulItemList[i].itemGrade <= conditionGrade
        //        && usefulItemList[i].itemGrade != 5 && usefulItemList[i].itemGrade != 6)//自动选择 去掉橙色、红色
        //    {
        //        int _count = usefulItemList[i].itemCount >= 5 ? 5 : usefulItemList[i].itemCount;
        //        for (int j = 0; j < _count; j++)
        //        {
        //            if (TotalCount < 5)
        //            {
        //                TotalCount++;
        //                GameObject.Find("EXPItem" + TotalCount.ToString()).GetComponent<UISprite>().spriteName = "Grade" + usefulItemList[i].itemGrade;
        //                GameObject.Find("EXPItem" + TotalCount.ToString()).transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName = usefulItemList[i].itemCode.ToString();
        //                listSelectItemCodeAndIndex.Add(new SelectItemCodeAndIndex(usefulItemList[i].itemCode, j));
        //                listSelectEquipItem.Add(usefulItemList[i]);
        //            }
        //        }
        //    }
        //    else
        //    {

        //    }
        //}
        if (TotalCount == 0)
        {
            UIManager.instance.OpenPromptWindow("当前没有满足条件的宝石\n请前往组队副本收集或手动添加", 11, false, PromptWindow.PromptType.Hint, null, null);
        }
        int curSumAddEXP = 0;
        for (int i = 0; i < listSelectEquipItem.Count; i++)
        {
            curSumAddEXP += int.Parse(listSelectEquipItem[i].feedExp);
        }
        //SetExpSliderOnSelect(SecretStoneWindow.mHero.characterRoleID, SecretStoneWindow.curRareStone.posId, curSumAddEXP);
        SetExpSliderOnSelect(SecretStoneWindow.curRareStone, curSumAddEXP);
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
    //不用
    void SetExpSliderOnSelect(int _CharacterRoleID, int _equipPosition, int curSumAddEXP)
    {
        HeroInfo mHeroInfo = TextTranslator.instance.GetHeroInfoByHeroID(SecretStoneWindow.mHero.cardID);
        foreach (var h in CharacterRecorder.instance.ownedHeroList)
        {
            if (_CharacterRoleID == h.characterRoleID)
            {
                Hero curHero = h;
                Hero.EquipInfo _equipInfo = curHero.equipList[_equipPosition - 1];
                int curExp = curHero.equipList[_equipPosition - 1].equipExp;// +curSumAddEXP;
                int curExpAfterAdd = curHero.equipList[_equipPosition - 1].equipExp + curSumAddEXP;
                int maxEXp = TextTranslator.instance.GetEquipStrongCostDataByID(_equipInfo.equipLevel, mHeroInfo.heroRarity).NeedExp;
                _Slider.value = (float)curExp / (float)maxEXp;
                _SliderAfterAdd.value = (float)curExpAfterAdd / (float)maxEXp;
                LabelExp.text = curExpAfterAdd.ToString() + "/" + maxEXp.ToString();

                int equipLevelInFuture = GetEquipLevelInFuture(curExpAfterAdd);
                SetEquipInfoOnlyForBaoWuStreng(equipLevelInFuture);
                /*      int sumMoneyCostInFuture = 0;
                      for (int i = _equipInfo.equipLevel; i < equipLevelInFuture; i++)
                      {
                          Debug.Log("level..." + i + "..heroRarity..." + mHeroInfo.heroRarity + "....pos..." + SecretStoneWindow.curRareStone.posId);
                          sumMoneyCostInFuture += TextTranslator.instance.GetEquipStrongCostByID(i, mHeroInfo.heroRarity, SecretStoneWindow.curRareStone.posId);
                      }
                      strengNeedMoney = sumMoneyCostInFuture;
                      string moneyLabelColor = "";
                      if (CharacterRecorder.instance.gold < strengNeedMoney)
                      {
                          moneyLabelColor = "[ff0000]";
                      }
                      needMoneyLabel.text = moneyLabelColor + sumMoneyCostInFuture.ToString(); */
                break;
            }
        }
    }
    //秘宝经验
    void SetExpSliderOnSelect(RareTreasureOpen _curStone, int curSumAddEXP)
    {
        int curExp = _curStone.stoneExp;
        int curExpAfterAdd = curExp + curSumAddEXP;
        TextTranslator.ItemInfo _myItemInfo = TextTranslator.instance.GetItemByItemCode(_curStone.stoneId);
        int maxEXp = TextTranslator.instance.GetRareTreasureExpByID(_curStone.stoneLevel, _myItemInfo.itemGrade).NeedExp;
        _Slider.value = (float)curExp / (float)maxEXp;
        _SliderAfterAdd.value = (float)curExpAfterAdd / (float)maxEXp;
        LabelExp.text = curExpAfterAdd.ToString() + "/" + maxEXp.ToString();

        int equipLevelInFuture = GetEquipLevelInFuture(curExpAfterAdd);
        SetEquipInfoOnlyForBaoWuStreng(equipLevelInFuture);
    }
    int GetEquipLevelInFuture(int curSumAddEXP)
    {
        HeroInfo mHeroInfo = TextTranslator.instance.GetHeroInfoByHeroID(SecretStoneWindow.mHero.cardID);
        int curStoneLevel = SecretStoneWindow.curRareStone.stoneLevel;//mEquipInfo.equipLevel;
        TextTranslator.ItemInfo _myItemInfo = TextTranslator.instance.GetItemByItemCode(SecretStoneWindow.curRareStone.stoneId);
        //Debug.Log("curEquipLv.." + curEquipLv + "...heroRarity..." + mHeroInfo.heroRarity);
        int curNeedEXp = TextTranslator.instance.GetRareTreasureExpByID(curStoneLevel, _myItemInfo.itemGrade).NeedExp;
        while (curSumAddEXP - curNeedEXp >= 0)
        {
            curSumAddEXP -= curNeedEXp;
            curStoneLevel += 1;
            curNeedEXp = TextTranslator.instance.GetRareTreasureExpByID(curStoneLevel, _myItemInfo.itemGrade).NeedExp;
        }
        //Debug.LogError("预览等级.." + curStoneLevel);
        return curStoneLevel;
    }
    #endregion

    #region 显示已选择的装备材料
    public void ShowSelectEquipItems(List<Item> _mList)
    {
        int curSumAddEXP = 0;
        listSelectEquipItem = _mList;
        /* for (int i = 0; i < ExpItemObjList.Count; i++)
         {
             if (i <= _mList.Count)
             {
                 ExpItemObjList[i].GetComponent<UISprite>().spriteName = "Grade" + _mList[i].itemGrade;
                 ExpItemObjList[i].transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName = _mList[i].itemCode.ToString();
             }
             else
             {
                 ExpItemObjList[i].GetComponent<UISprite>().spriteName = "Grade1";
                 ExpItemObjList[i].transform.FindChild("Icon").GetComponent<UISprite>().spriteName = "";
             }
         }*/
        for (int i = 0; i < ExpItemObjList.Count; i++)
        {
            ExpItemObjList[i].GetComponent<UISprite>().spriteName = "Grade1";
            ExpItemObjList[i].transform.FindChild("Icon").GetComponent<UISprite>().spriteName = "add";
        }
        int TotalCount = 0;

        for (int i = 0; i < _mList.Count; i++)
        {
            if (TotalCount < 5)
            {
                TotalCount++;
                Debug.Log(_mList[i].itemGrade);
                ExpItemObjList[TotalCount - 1].GetComponent<UISprite>().spriteName = "Grade" + _mList[i].itemGrade;
                ExpItemObjList[TotalCount - 1].transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName = _mList[i].itemCode.ToString();
                curSumAddEXP += int.Parse(_mList[i].feedExp);
            }
        }
        //SetExpSliderOnSelect(SecretStoneWindow.mHero.characterRoleID, SecretStoneWindow.curRareStone.posId, curSumAddEXP);
        SetExpSliderOnSelect(SecretStoneWindow.curRareStone, curSumAddEXP);
    }

    public void ShowSelectEquipItems(List<SelectItemCodeAndIndex> _mList)
    {
        int curSumAddEXP = 0;
        listSelectItemCodeAndIndex = _mList;
        listSelectEquipItem.Clear();
        /*  for (int i = 0; i < _mList.Count;i++ )
          {
              listSelectEquipItem.Add(TextTranslator.instance.GetItemByID(_mList[i].code));
          }*/

        for (int i = 0; i < ExpItemObjList.Count; i++)
        {
            ExpItemObjList[i].GetComponent<UISprite>().spriteName = "Grade1";
            ExpItemObjList[i].transform.FindChild("Icon").GetComponent<UISprite>().spriteName = "add";
        }
        int TotalCount = 0;

        for (int i = 0; i < _mList.Count; i++)
        {
            if (TotalCount < 5)
            {
                listSelectEquipItem.Add(TextTranslator.instance.GetItemByID(_mList[i].code));
                TotalCount++;
                //Debug.Log("grade..." + listSelectEquipItem[i].itemGrade + "...code..." + listSelectEquipItem[i].itemCode);
                ExpItemObjList[TotalCount - 1].GetComponent<UISprite>().spriteName = "Grade" + listSelectEquipItem[i].itemGrade;
                ExpItemObjList[TotalCount - 1].transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName = listSelectEquipItem[i].itemCode.ToString();

                curSumAddEXP += int.Parse(listSelectEquipItem[i].feedExp);
            }
        }
        SetExpSliderOnSelect(SecretStoneWindow.curRareStone, curSumAddEXP);
    }
    #endregion

    #region 强化
    private void StrenghBaoWu()
    {
        if (listSelectEquipItem.Count > 0 && CharacterRecorder.instance.gold >= strengNeedMoney)
        {
            string idListStr = "";
            for (int i = 0; i < listSelectEquipItem.Count; i++)
            {
                idListStr += listSelectEquipItem[i].itemCode + "$";
            }
            Hero mHero = SecretStoneWindow.mHero;
            TextTranslator.instance.heroBefore.SetHeroCardID(mHero.cardID);
            TextTranslator.instance.heroBefore.SetHeroProperty(mHero.force, mHero.level, mHero.exp, mHero.maxExp, mHero.rank, mHero.rare, mHero.classNumber, mHero.skillLevel, mHero.skillNumber, mHero.HP, mHero.strength, mHero.physicalDefense, mHero.physicalCrit,
                mHero.UNphysicalCrit, mHero.dodge, mHero.hit, mHero.moreDamige, mHero.avoidDamige, mHero.aspd, mHero.move, mHero.HPAdd, mHero.strengthAdd, mHero.physicalDefenseAdd, mHero.physicalCritAdd, mHero.UNphysicalCritAdd,
                mHero.dodgeAdd, mHero.hitAdd, mHero.moreDamigeAdd, mHero.avoidDamigeAdd, 0);
 //           RecordEquipInfoBefore(this.mEquipInfo);
            NetworkHandler.instance.SendProcess("3104#" + mHero.characterRoleID.ToString() + ";" + SecretStoneWindow.curRareStone.posId.ToString() + ";" + idListStr + ";");
            //CharacterRecorder.instance.IsNeedOpenFight = false;
            Invoke("DelayIsNeedOpenFightTrue", 3.0f);
        }
        else if (listSelectEquipItem.Count == 0)
        {
            UIManager.instance.OpenPromptWindow("请选择装备", PromptWindow.PromptType.Hint, null, null);
        }
        else if (CharacterRecorder.instance.gold < strengNeedMoney)
        {
            UIManager.instance.OpenPromptWindow("金币不足", PromptWindow.PromptType.Hint, null, null);
        }
    }
    void DelayIsNeedOpenFightTrue()
    {
        CharacterRecorder.instance.IsNeedOpenFight = true;
    }
    void RecordEquipInfoBefore(Hero.EquipInfo mEquipInfo)
    {
        mEquipInfoBefore.equipCode = mEquipInfo.equipCode;
        mEquipInfoBefore.equipClass = mEquipInfo.equipClass;
        mEquipInfoBefore.equipLevel = mEquipInfo.equipLevel;
        mEquipInfoBefore.equipExp = mEquipInfo.equipExp;
        mEquipInfoBefore.equipPosition = mEquipInfo.equipPosition;
        mEquipInfoBefore.equipID = mEquipInfo.equipID;
        mEquipInfoBefore.equipColorNum = mEquipInfo.equipColorNum;
        Debug.Log("记录旧的code.." + mEquipInfoBefore.equipCode + "..class.." + mEquipInfoBefore.equipClass + "..equipColorNum.." + mEquipInfoBefore.equipColorNum);
    }
    #endregion 

    #region InitExpSlider
    void SetExpSlider(RareTreasureOpen _curStone)
    {
        int curExp = _curStone.stoneExp;
        TextTranslator.ItemInfo _myItemInfo = TextTranslator.instance.GetItemByItemCode(_curStone.stoneId);
        int maxEXp = TextTranslator.instance.GetRareTreasureExpByID(_curStone.stoneLevel, _myItemInfo.itemGrade).NeedExp;
        _Slider.value = (float)curExp / (float)maxEXp;
        _SliderAfterAdd.value = 0;
        LabelExp.text = curExp.ToString() + "/" + maxEXp.ToString();
    }
    #endregion

    void ConfirmMoveAwayStone()
    {
        NetworkHandler.instance.SendProcess(string.Format("3103#{0};{1};", SecretStoneWindow.mHero.characterRoleID, SecretStoneWindow.curRareStone.posId));
    }
}
