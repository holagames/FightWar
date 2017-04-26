using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipRefineResultPart:MonoBehaviour
{
    [SerializeField]
    private GameObject uiGride;
    [SerializeField]
    private GameObject equipJinJieResultItem;
    [SerializeField]
    private UISprite wordSprite;
    //-----------以下是数据Info
    [SerializeField]
    private UISprite frame1;
    [SerializeField]
    private UISprite frame2;
    [SerializeField]
    private UISprite icon1;
    [SerializeField]
    private UISprite icon2;
    [SerializeField]
    private UILabel nameLabel1;
    [SerializeField]
    private UILabel nameLabel2;
    //[SerializeField]
    //private UILabel fightLabel1;
    //[SerializeField]
    //private UILabel fightLabel2;
    //------------数据
    private List<string> desList = new List<string>();
    private List<int> leftDataList = new List<int>();
    private List<int> rightDataList = new List<int>();
    private List<float> leftDataList2 = new List<float>();
    private List<float> rightDataList2 = new List<float>();
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    public void SetEquipRefineResultPart(Hero.EquipInfoBefore _mEquipInfoBefore, Hero.EquipInfo mEquipInfo)//, HeroNewData _HeroNewData)
    {
        //wordSprite.spriteName = "word4";
        StartCoroutine(DelayShowInfo(_mEquipInfoBefore, mEquipInfo));
    }
    IEnumerator DelayShowInfo(Hero.EquipInfoBefore _mEquipInfoBefore, Hero.EquipInfo mEquipInfo)
    {
        frame1.spriteName = "";
        frame2.spriteName = "";
        icon1.spriteName = "";
        icon2.spriteName = "";
        nameLabel1.text = "";
        nameLabel2.text = "";
        yield return new WaitForSeconds(1.0f);

        GetDataList(_mEquipInfoBefore);

        frame1.spriteName = GetFrameStr(_mEquipInfoBefore.equipCode);
        frame2.spriteName = GetFrameStr(mEquipInfo.equipCode);
        icon1.spriteName = _mEquipInfoBefore.equipCode.ToString();
        icon2.spriteName = mEquipInfo.equipCode.ToString();
        nameLabel1.text = GetItemName(_mEquipInfoBefore.equipCode, _mEquipInfoBefore.equipClass, _mEquipInfoBefore.equipColorNum) + "\n[-]" + leftDataList[0] + "阶";//GetItemName(_mEquipInfoBefore.equipCode, _mEquipInfoBefore.equipClass);
        nameLabel2.text = GetItemName(mEquipInfo.equipCode, mEquipInfo.equipClass, mEquipInfo.equipColorNum) + "\n[-]" + rightDataList[0] + "阶";//GetItemName(mEquipInfo.equipCode, mEquipInfo.equipClass);

        //  fightLabel1.text = GetJingLianBeforeStr(_mEquipInfoBefore, false);//GetJingLianStr(mEquipInfo, false);
        //  fightLabel2.text = GetJingLianBeforeStr(_mEquipInfoBefore,true);//GetJingLianStr(mEquipInfo, true);
        
        yield return new WaitForSeconds(0.2f);
        //去掉精炼等级，放到name处,加上 战斗力
        //for (int i = 1; i < leftDataList.Count; i++)
        //{
        //    GameObject obj = NGUITools.AddChild(uiGride, equipJinJieResultItem) as GameObject;
        //    obj.GetComponent<EquipJinJieResultItem>().SetEquipJinJieResultItem(desList[i], leftDataList[i], rightDataList[i]);
        //}
        for (int i = 0; i < leftDataList2.Count; i++)
        {
            GameObject obj = NGUITools.AddChild(uiGride, equipJinJieResultItem) as GameObject;
            obj.GetComponent<EquipJinJieResultItem>().SetEquipJinJieResultItem(desList[leftDataList.Count + i], leftDataList2[i], rightDataList2[i]);
        }
        uiGride.GetComponent<UIGrid>().Reposition();
    }
    string GetItemName(int mEquipCode,int _classNumber)
    {
        TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(mEquipCode);
        string _nameColor = "";
        if (_classNumber <= 1)
        {
            _nameColor = "[3ee817]";
        }
        else if (_classNumber >= 2 && _classNumber <= 5)
        {
            _nameColor = "[8ccef2]";
        }
        else if (_classNumber >= 6 && _classNumber <= 11)
        {
            _nameColor = "[bb44ff]";
        }
        else if (_classNumber >= 12 && _classNumber <= 17)
        {
            _nameColor = "[ff8c04]";
        }
        else if (_classNumber >= 18 && _classNumber <= 20)
        {
            _nameColor = "[fb2d50]";
        }
        if (mItemInfo != null)
        {
            return _nameColor + mItemInfo.itemName;
        }
        else return "";
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
    string GetFrameStr(int _itemCode)
    {
        string _frameStr = "";

        TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_itemCode);
        _frameStr = string.Format("Grade{0}", mItemInfo.itemGrade);
        return _frameStr;
    }
    void GetDataList(Hero.EquipInfoBefore mEquipInfo)
    {
        desList.Clear();
        leftDataList.Clear();
        rightDataList.Clear();
        leftDataList2.Clear();
        rightDataList2.Clear();

        string desStr = "";

        int usefulClass = mEquipInfo.equipClass;
        int usefulClassRight = mEquipInfo.equipClass + 1;
        //去掉精炼等级，放到name处
        desList.Add("精炼: ");
        leftDataList.Add(usefulClass);
        rightDataList.Add(usefulClassRight); 
        if (TextTranslator.instance.heroNow.force != TextTranslator.instance.heroBefore.force)
        {
            desList.Add("战斗力: ");
            leftDataList.Add(TextTranslator.instance.heroBefore.force);
            rightDataList.Add(TextTranslator.instance.heroNow.force);
        }
        
        EquipDetailInfo es = TextTranslator.instance.GetEquipInfoByID(mEquipInfo.equipCode);
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
            desList.Add("生命: ");
            leftDataList2.Add(es.HP * usefulClass);
            rightDataList2.Add(es.HP * usefulClassRight);
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
            desList.Add("物攻: ");
            leftDataList2.Add(es.ADAttack * usefulClass);
            rightDataList2.Add(es.ADAttack * usefulClassRight);
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
            desList.Add("法攻: ");
            leftDataList2.Add(es.APAttack * usefulClass);
            rightDataList2.Add(es.APAttack * usefulClassRight);
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
            desList.Add("物防: ");
            leftDataList2.Add(es.ADDenfance * usefulClass);
            rightDataList2.Add(es.ADDenfance * usefulClassRight);
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
            desList.Add("法防: ");
            leftDataList2.Add(es.APDenfance * usefulClass);
            rightDataList2.Add(es.APDenfance * usefulClassRight);
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
            desList.Add("命中: ");
            leftDataList2.Add(es.Hit * usefulClass);
            rightDataList2.Add(es.Hit * usefulClassRight);
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
            desList.Add("闪避: ");
            leftDataList2.Add(es.NoHit * usefulClass);
            rightDataList2.Add(es.NoHit * usefulClassRight);
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
            desList.Add("暴击: ");
            leftDataList2.Add(es.Crit * usefulClass);
            rightDataList2.Add(es.Crit * usefulClassRight);
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
            desList.Add("抗暴: ");
            leftDataList2.Add(es.NoCrit * usefulClass);
            rightDataList2.Add(es.NoCrit * usefulClassRight);
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
            desList.Add("伤害加成: ");
            leftDataList2.Add(es.DmgBonus * usefulClass);
            rightDataList2.Add(es.DmgBonus * usefulClassRight);
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
            desList.Add("伤害减免: ");
            leftDataList2.Add(es.DmgReduction * usefulClass);
            rightDataList2.Add(es.DmgReduction * usefulClassRight);
        }
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
