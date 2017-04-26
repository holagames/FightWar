using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GainResultPart: MonoBehaviour
{
    [SerializeField]
    private GameObject uiGride;
    public GameObject uiScrollView;
    [SerializeField]
    private GameObject gainResultItem;
    [SerializeField]
    private UISprite wordSprite;
    public GameObject UI_Huode;
    public GameObject UI_Huode2;
    //-----------以下是数据Info

    //------------数据
    private List<Item> gainResultList = new List<Item>();

	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}

    public void ChangeAwardWindowTitle()
    {
        UI_Huode.SetActive(false);
        UI_Huode2.SetActive(true);
        UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1503);
    }

    public void SetGainResultPart(List<Item> _itemList)
    {
        gainResultList = _itemList;
        StartCoroutine(CreatItems());
    }
    IEnumerator CreatItems()
    {
        if (gainResultList.Count == 1)
        {
            yield return new WaitForSeconds(0.5f);
            GameObject obj = NGUITools.AddChild(uiGride, gainResultItem) as GameObject;
            obj.transform.localPosition += new Vector3(280, 0, 0);
            obj.GetComponent<GainResultItem>().SetGainResultItem(gainResultList[0]);
            TextTranslator.instance.ItemDescription(obj, gainResultList[0].itemCode, 0);
            uiGride.GetComponent<UIGrid>().enabled = false;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < gainResultList.Count; i++)
            {
                //yield return new WaitForSeconds(0.2f);
                GameObject obj = NGUITools.AddChild(uiGride, gainResultItem) as GameObject;
                obj.GetComponent<UIDragScrollView>().scrollView = uiScrollView.GetComponent<UIScrollView>();
                obj.transform.localPosition += new Vector3(140 * i, 0, 0);
                obj.GetComponent<GainResultItem>().SetGainResultItem(gainResultList[i]);
                TextTranslator.instance.ItemDescription(obj, gainResultList[i].itemCode, 0);
                uiGride.GetComponent<UIGrid>().Reposition();
            }
        }
    }
    string GetItemName(int mEquipCode, int _classNumber, int _colorNum)//_colorNum: 1 - 21
    {
        TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(mEquipCode);
        string _nameColor = "";

        switch (mItemInfo.itemGrade)
        {
            case 1: _nameColor = "[ffffff]"; break;
            case 2: _nameColor = "[3ee817]"; break;
            case 3: _nameColor = "[8ccef2]"; break;
            case 4: _nameColor = "[bb44ff]"; break;
            case 5: _nameColor = "[ff8c04]"; break;
            case 6: _nameColor = "[fb2d50]"; break;
        }
        int _NumAdd = GetNumAdd(mItemInfo.itemGrade, _colorNum);
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
            case 1: addNum = 0; break;
            case 2:
                switch (_colorNum)
                {
                    case 1: addNum = 0; break;
                    case 2: addNum = 1; break;
                    case 3: addNum = 2; break;
                    case 4: addNum = 3; break;
                }
                break;
            case 3:
                switch (_colorNum)
                {
                    case 5: addNum = 0; break;
                    case 6: addNum = 1; break;
                    case 7: addNum = 2; break;
                    case 8: addNum = 3; break;
                }
                break;
            case 4:
                switch (_colorNum)
                {
                    case 9: addNum = 0; break;
                    case 10: addNum = 1; break;
                    case 11: addNum = 2; break;
                    case 12: addNum = 3; break;
                    case 13: addNum = 4; break;
                }
                break;
            case 5:
                switch (_colorNum)
                {
                    case 14: addNum = 0; break;
                    case 15: addNum = 1; break;
                    case 16: addNum = 2; break;
                    case 17: addNum = 3; break;
                    case 18: addNum = 4; break;
                    case 19: addNum = 5; break;
                }
                break;
            case 6:
                switch (_colorNum)
                {
                    case 20: addNum = 0; break;
                    case 21: addNum = 1; break;
                }
                break;
        }
        return addNum;
    }
    string GetFrameStr(int _itemCode)
    {
        string _frameStr = "";

        TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_itemCode);
        _frameStr = string.Format("Grade{0}", mItemInfo.itemGrade);
        return _frameStr;
    }
    
    string GetJingLianBeforeStr(Hero.EquipInfoBefore mEquipInfo, bool useNext)
    {
        string desStr = "";
        EquipDetailInfo es = TextTranslator.instance.GetEquipInfoByID(mEquipInfo.equipCode);
        int usefulClass = 0;
        if (!useNext)
        {
            usefulClass = mEquipInfo.equipClass;
        }
        else
        {
            usefulClass = mEquipInfo.equipClass + 1;
        }
        desStr = "精炼:" + usefulClass.ToString() + "\n";
        if (es.HP > 0)
        {
            string usefulNumStr = "";
            if (es.HP * usefulClass < 1 && es.HP * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.HP * usefulClass);
            }
            else
            {
                usefulNumStr = (es.HP * usefulClass).ToString();
            }
            desStr += "生命:" + usefulNumStr + "\n";
        }
        if (es.ADAttack > 0)
        {
            string usefulNumStr = "";
            if (es.ADAttack * usefulClass < 1 && es.ADAttack * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.ADAttack * usefulClass);
            }
            else
            {
                usefulNumStr = (es.ADAttack * usefulClass).ToString();
            }
            desStr += "物攻:" + usefulNumStr + "\n";
            //desStr += "物攻:" + (es.ADAttack * usefulClass).ToString() + "\n";
        }
        if (es.APAttack > 0)
        {
            string usefulNumStr = "";
            if (es.APAttack * usefulClass < 1 && es.APAttack * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.APAttack * usefulClass);
            }
            else
            {
                usefulNumStr = (es.APAttack * usefulClass).ToString();
            }
            desStr += "法攻:" + usefulNumStr + "\n";
        }
        if (es.ADDenfance > 0)
        {
            string usefulNumStr = "";
            if (es.ADDenfance * usefulClass < 1 && es.ADDenfance * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.ADDenfance * usefulClass);
            }
            else
            {
                usefulNumStr = (es.ADDenfance * usefulClass).ToString();
            }
            desStr += "物防:" + usefulNumStr + "\n";
        }
        if (es.APDenfance > 0)
        {
            string usefulNumStr = "";
            if (es.APDenfance * usefulClass < 1 && es.APDenfance * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.APDenfance * usefulClass);
            }
            else
            {
                usefulNumStr = (es.APDenfance * usefulClass).ToString();
            }
            desStr += "法防:" + usefulNumStr + "\n";
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
        }
        return desStr;
    }
    string GetJingLianStr(Hero.EquipInfo mEquipInfo, bool useNext)
    {
        string desStr = "";
        EquipDetailInfo es = TextTranslator.instance.GetEquipInfoByID(mEquipInfo.equipCode);
        int usefulClass = 0;
        if (!useNext)
        {
            usefulClass = mEquipInfo.equipClass;
        }
        else
        {
            usefulClass = mEquipInfo.equipClass + 1;
        }
        desStr = "精炼:" + usefulClass.ToString() + "\n";
        if (es.HP > 0)
        {
            string usefulNumStr = "";
            if (es.HP * usefulClass < 1 && es.HP * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.HP * usefulClass);
            }
            else
            {
                usefulNumStr = (es.HP * usefulClass).ToString();
            }
            desStr += "生命:" + usefulNumStr + "\n";
        }
        if (es.ADAttack > 0)
        {
            string usefulNumStr = "";
            if (es.ADAttack * usefulClass < 1 && es.ADAttack * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.ADAttack * usefulClass);
            }
            else
            {
                usefulNumStr = (es.ADAttack * usefulClass).ToString();
            }
            desStr += "物攻:" + usefulNumStr + "\n";
            //desStr += "物攻:" + (es.ADAttack * usefulClass).ToString() + "\n";
        }
        if (es.APAttack > 0)
        {
            string usefulNumStr = "";
            if (es.APAttack * usefulClass < 1 && es.APAttack * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.APAttack * usefulClass);
            }
            else
            {
                usefulNumStr = (es.APAttack * usefulClass).ToString();
            }
            desStr += "法攻:" + usefulNumStr + "\n";
        }
        if (es.ADDenfance > 0)
        {
            string usefulNumStr = "";
            if (es.ADDenfance * usefulClass < 1 && es.ADDenfance * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.ADDenfance * usefulClass);
            }
            else
            {
                usefulNumStr = (es.ADDenfance * usefulClass).ToString();
            }
            desStr += "物防:" + usefulNumStr + "\n";
        }
        if (es.APDenfance > 0)
        {
            string usefulNumStr = "";
            if (es.APDenfance * usefulClass < 1 && es.APDenfance * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.APDenfance * usefulClass);
            }
            else
            {
                usefulNumStr = (es.APDenfance * usefulClass).ToString();
            }
            desStr += "法防:" + usefulNumStr + "\n";
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
        }
        return desStr;
    }
    string ChangeDecimalToPercent(float _Decimal)
    {
        return string.Format("{0}%", _Decimal * 100);
    }
    
}
