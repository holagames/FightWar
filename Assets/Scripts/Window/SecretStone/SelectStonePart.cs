using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class SelectStonePart : MonoBehaviour
{
    public GameObject uiGride;
    public GameObject stoneItem;
    public GameObject moveOnButton;
    public GameObject BottomInfoObj;
    public UILabel desLabel;
    public UILabel LeftInfoLabelNew;
    public UISprite stoneIcon;
    private BetterList<GameObject> itemListForSelect = new BetterList<GameObject>();
    private string InfoDesStr;
    private string DataStr;
    private int stoneIdWillMoveOn;
    /// <summary>
    /// 记录当前所有的已拥有的秘宝
    /// </summary>
    private List<Item> allItemList = new List<Item>();
    // Use this for initialization
    void Start()
    {
        //SetSelectStonePart();
        if (UIEventListener.Get(moveOnButton).onClick == null)
        {
            UIEventListener.Get(moveOnButton).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.GuideID[38] == 7)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
                NetworkHandler.instance.SendProcess(string.Format("3102#{0};{1};{2};", SecretStoneWindow.mHero.characterRoleID, SecretStoneWindow.curRareStone.posId, stoneIdWillMoveOn));
            };
        }
    }

    public void SetSelectStonePart(int conditionGrade)
    {
        ClearUIGride();
        allItemList.Clear();//清空当前的记录的秘宝列表
        HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(SecretStoneWindow.mHero.cardID);
        for (int i = 0; i < TextTranslator.instance.bagItemList.size; i++)
        {
            //Debug.Log("allCode..." + TextTranslator.instance.bagItemList[i].itemCode);
            if (TextTranslator.instance.bagItemList[i].itemCode > 40000 && TextTranslator.instance.bagItemList[i].itemCode < 42000 && TextTranslator.instance.bagItemList[i].itemGrade >= conditionGrade && TextTranslator.instance.bagItemList[i].itemCount > 0)
            {
                //Debug.Log("code..." + TextTranslator.instance.bagItemList[i].itemCode + "," + SecretStoneWindow.curRareStone.stoneId);
                if ((TextTranslator.instance.bagItemList[i].itemGrade >= 5 && hinfo.heroRarity <= 3) ||
                    SecretStoneWindow.curRareStone.stoneId == TextTranslator.instance.bagItemList[i].itemCode)
                {
                    //过滤掉 金色以上秘宝 不能装 紫色以下 武将身上 以及相同的秘宝
                }
                else
                {
                    allItemList.Add(TextTranslator.instance.bagItemList[i]);
                    //GameObject obj = NGUITools.AddChild(uiGride, stoneItem);
                    //obj.SetActive(true);
                    //obj.name = TextTranslator.instance.bagItemList[i].itemCode.ToString();
                    //obj.GetComponent<UISprite>().spriteName = GetItemFrameName(TextTranslator.instance.bagItemList[i].itemCode);
                    //obj.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = TextTranslator.instance.bagItemList[i].itemCode.ToString();
                    //itemListForSelect.Add(obj);
                    //UIEventListener.Get(obj).onClick = ClickStoneToSelect;
                }
            }
        }
        //实例化出来秘宝
        if (allItemList.Count > 0)
        {
            CreateItemStone();
        }
        uiGride.GetComponent<UIGrid>().repositionNow = true;
        if (itemListForSelect.size == 0 && conditionGrade == 0)
        {
            desLabel.text = "没有可用的秘宝，可以前往组队副本收集";
            desLabel.transform.localPosition = Vector3.zero;
            BottomInfoObj.SetActive(false);
        }
        else if (itemListForSelect.size == 0)
        {
            desLabel.text = "没有更高级的秘宝，可以前往组队副本收集";
            desLabel.transform.localPosition = Vector3.zero;
            BottomInfoObj.SetActive(false);
        }
        else
        {
            BottomInfoObj.SetActive(true);
            desLabel.text = "";//"请选择你要使用的秘宝";
            //desLabel.transform.localPosition = new Vector3(0,-262,0);
            SetStoneInfoPart(int.Parse(itemListForSelect[0].name));
            itemListForSelect[0].GetComponent<UIToggle>().startsActive = true;
        }
    }
    /// <summary>
    /// 实例化生成秘宝
    /// </summary>
    void CreateItemStone()
    {
        PaiXuItemLsit();
        for (int i = 0; i < allItemList.Count; i++)
        {
            
            Item item = allItemList[i];
            GameObject obj = NGUITools.AddChild(uiGride, stoneItem);
            obj.SetActive(true);
            obj.name = item.itemCode.ToString();
            obj.GetComponent<UISprite>().spriteName = GetItemFrameName(item.itemCode);
            obj.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = item.itemCode.ToString();
            itemListForSelect.Add(obj);
            UIEventListener.Get(obj).onClick = ClickStoneToSelect;
        }
    }
    /// <summary>
    /// 秘宝排序
    /// </summary>
    void PaiXuItemLsit()
    {
        allItemList.Sort(delegate(Item x, Item y)
        {
            if (x != null && y != null)
            {
                return TextTranslator.instance.GetItemSortByItemCode(x.itemCode).ItemSortID.CompareTo(TextTranslator.instance.GetItemSortByItemCode(y.itemCode).ItemSortID);
            }
            else if (x!=null)
            {
                return 0.CompareTo(TextTranslator.instance.GetItemSortByItemCode(x.itemCode).ItemSortID) == null ? 0 : TextTranslator.instance.GetItemSortByItemCode(x.itemCode).ItemSortID;
            }
            else
            {
                return (TextTranslator.instance.GetItemSortByItemCode(y.itemCode).ItemSortID) == null ? 0 : TextTranslator.instance.GetItemSortByItemCode(y.itemCode).ItemSortID;
            }
        });
    }
    void ClickStoneToSelect(GameObject go)
    {
        if (go != null)
        {
            // NetworkHandler.instance.SendProcess(string.Format("3102#{0};{1};{2};", SecretStoneWindow.mHero.characterRoleID, SecretStoneWindow.curRareStone.posId, go.name));
            SetStoneInfoPart(int.Parse(go.name));
            if (CharacterRecorder.instance.GuideID[38] == 6)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
        }
    }
    void ClearUIGride()
    {
        itemListForSelect.Clear();
        for (int i = uiGride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGride.transform.GetChild(i).gameObject);
        }
    }
    string GetItemFrameName(int _itemId)
    {
        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(_itemId);
        //Debug.LogError("_itemId.." + _itemId + "..Item.." + _ItemInfo);
        return "Grade" + _ItemInfo.itemGrade.ToString();
    }
    #region 安装宝石预览属性
    void SetStoneInfoPart(int stoneId)
    {
        stoneIdWillMoveOn = stoneId;
        stoneIcon.spriteName = stoneId.ToString();
        stoneIcon.transform.parent.GetComponent<UISprite>().spriteName = GetItemFrameName(stoneId);
        // stoneNameLabel.text = GetItemName(SecretStoneWindow.curRareStone.stoneId);//, mHero.equipList[ClickIndex - 1].equipClass, mHero.equipList[ClickIndex - 1].equipColorNum);
        GetStrenghStr(stoneId, false);
        LeftInfoLabelNew.text = InfoDesStr;
        LeftInfoLabelNew.transform.FindChild("DataLabel").GetComponent<UILabel>().text = DataStr;

        /* for (int i = 0; i < itemListForSelect.size;i++ )
         {
             if (itemListForSelect[i].name != stoneId.ToString())
             {
                 itemListForSelect[i].transform.FindChild("SelectEffect").gameObject.SetActive(false);
             }
             else
             {
                 itemListForSelect[i].transform.FindChild("SelectEffect").gameObject.SetActive(true);
             }
         }*/
    }
    #endregion

    #region 获得秘宝等级属性
    string GetStrenghStr(int stoneId, bool useNext)
    {
        string desStr = "";
        RareTreasureAttr es = TextTranslator.instance.GetRareTreasureAttrByID(stoneId);

        int usefulClass = 0;
        if (!useNext)
        {
            usefulClass = 1;//curRareStone.stoneLevel;
        }
        else
        {
            usefulClass = 1 + 1;
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
            if (es.Def * usefulClass / 4 < 1 && es.Def * usefulClass / 4 > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Def * usefulClass / 4.0f);
            }
            else
            {
                usefulNumStr = (es.Def * usefulClass / 4).ToString();
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
}
