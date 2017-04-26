using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipJinJieResultPart: MonoBehaviour
{
    [SerializeField]
    private GameObject uiGride;
    [SerializeField]
    private GameObject equipJinJieResultItem;
    //-----------界面basicInfo
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
    public void SetEquipJinJieResultPart(Hero.EquipInfoBefore _mEquipInfoBefore,Hero.EquipInfo mEquipInfo)//, HeroNewData _HeroNewData)
    {
        StartCoroutine(DelayShow(_mEquipInfoBefore, mEquipInfo));
    }
    IEnumerator DelayShow(Hero.EquipInfoBefore _mEquipInfoBefore, Hero.EquipInfo mEquipInfo)
    {
        frame1.spriteName = "";
        frame2.spriteName = "";
        icon1.spriteName = "";
        icon2.spriteName = "";
        nameLabel1.text = "";
        nameLabel2.text = "";
        yield return new WaitForSeconds(1.0f);
        Debug.Log("_mEquipInfoBefore" + _mEquipInfoBefore.equipCode + ".." + _mEquipInfoBefore.equipClass + "。。" + _mEquipInfoBefore.equipColorNum + "..equipLevel.." + _mEquipInfoBefore.equipLevel);
        frame1.spriteName = GetFrameStr(_mEquipInfoBefore.equipCode);
        frame2.spriteName = GetFrameStr(mEquipInfo.equipCode);
        icon1.spriteName = _mEquipInfoBefore.equipCode.ToString();
        icon2.spriteName = mEquipInfo.equipCode.ToString();
        nameLabel1.text = GetItemName(_mEquipInfoBefore.equipCode, _mEquipInfoBefore.equipClass, _mEquipInfoBefore.equipColorNum);
        Debug.Log("mEquipInfo.." + mEquipInfo.equipCode + "..equipClass.." + mEquipInfo.equipClass + "..equipColorNum.." + mEquipInfo.equipColorNum + "..equipLevel.." + mEquipInfo.equipLevel);
        nameLabel2.text = GetItemName(mEquipInfo.equipCode, mEquipInfo.equipClass, mEquipInfo.equipColorNum);
        //   fightLabel1.text = GetStrenghStr(mEquipInfo,false);//string.Format("战斗力：{0}", mEquipInfo.);
        //   fightLabel2.text = GetStrenghStr(mEquipInfo, true);//string.Format("{0}", _HeroNewData.force);
        //GetDataList(_mEquipInfoBefore);
        GetLeftDataList(_mEquipInfoBefore);
        GetRightDataList(mEquipInfo);
        yield return new WaitForSeconds(0.2f);
        Debug.Log("count..." + leftDataList.Count);
        for (int i = 0; i < leftDataList.Count; i++)
        {
            GameObject obj = NGUITools.AddChild(uiGride, equipJinJieResultItem) as GameObject;
            //Debug.Log(desList[i]);
            //Debug.Log(leftDataList[i]);
            //Debug.Log(rightDataList[i]);
            obj.GetComponent<EquipJinJieResultItem>().SetEquipJinJieResultItem(desList[i], leftDataList[i], rightDataList[i]);
        }
        for (int i = 0; i < leftDataList2.Count; i++)
        {
            GameObject obj = NGUITools.AddChild(uiGride, equipJinJieResultItem) as GameObject;
            obj.GetComponent<EquipJinJieResultItem>().SetEquipJinJieResultItem(desList[leftDataList.Count + i], leftDataList2[i], rightDataList2[i]);
        }
        uiGride.GetComponent<UIGrid>().Reposition();
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
    void GetLeftHeroDataList()
    {
        
        
    }
    void GetLeftDataList(Hero.EquipInfoBefore mEquipInfo)
    {
        desList.Clear();
        leftDataList.Clear();
        leftDataList2.Clear();
        string desStr = "";
        EquipStrong es = TextTranslator.instance.GetEquipStrongByID(mEquipInfo.equipCode,mEquipInfo.equipColorNum);
        int usefulClass = mEquipInfo.equipLevel;
        //暂时去掉等级数据
        /*        leftDataList.Add(usefulClass);
                rightDataList.Add(usefulClassRight);
                desStr = "等级: " + usefulClass.ToString() + "\n";
                desList.Add("等级: "); */
        if (TextTranslator.instance.heroNow.force != TextTranslator.instance.heroBefore.force)
        {
            desStr += "战斗力: " + (TextTranslator.instance.heroBefore.force).ToString() + "\n";
            desList.Add("战斗力: ");
            leftDataList.Add(TextTranslator.instance.heroBefore.force);
        }
        if (es == null)
        {
            return;
        }
        if (es.Hp > 0)
        {
            desStr += "生命: " + (es.Hp * usefulClass).ToString() + "\n";
            desList.Add("生命: ");
            leftDataList.Add(es.Hp * usefulClass);
        }
        if (es.Atk > 0)
        {
            desStr += "攻击: " + (es.Atk * usefulClass).ToString() + "\n";
            desList.Add("攻击: ");
            leftDataList.Add(es.Atk * usefulClass);
        }
        if (es.Def > 0)
        {
            desStr += "防御: " + (es.Def * usefulClass).ToString() + "\n";
            desList.Add("防御: ");
            leftDataList.Add(es.Def * usefulClass);
        }
        if (es.Hit > 0)
        {
            desStr += "命中: " + (es.Hit * usefulClass).ToString() + "\n";
            desList.Add("命中: ");
            leftDataList2.Add(es.Hit * usefulClass);
        }
        if (es.NoHit > 0)
        {
            desStr += "闪避: " + (es.NoHit * usefulClass).ToString() + "\n";
            desList.Add("闪避: ");
            leftDataList2.Add(es.NoHit * usefulClass);
        }
        if (es.Crit > 0)
        {
            desStr += "暴击: " + (es.Crit * usefulClass).ToString() + "\n";
            desList.Add("暴击: ");
            leftDataList2.Add(es.Crit * usefulClass);
        }
        if (es.NoCrit > 0)
        {
            desStr += "抗暴: " + (es.NoCrit * usefulClass).ToString() + "\n";
            desList.Add("抗暴: ");
            leftDataList2.Add(es.NoCrit * usefulClass);
        }
        if (es.DmgBonus > 0)
        {
            desStr += "伤害加成: " + (es.DmgBonus * usefulClass).ToString() + "\n";
            desList.Add("伤害加成: ");
            leftDataList2.Add(es.DmgBonus * usefulClass);
        }
        if (es.DmgReduction > 0)
        {
            desStr += "伤害减免: " + (es.DmgReduction * usefulClass).ToString() + "\n";
            desList.Add("伤害减免: ");
            leftDataList2.Add(es.DmgReduction * usefulClass);
        }
    }
    void GetRightDataList(Hero.EquipInfo mEquipInfo)
    {
        rightDataList.Clear();
        rightDataList2.Clear();
        Debug.Log("equipCode...." + mEquipInfo.equipCode + "..equipColorNum..." + mEquipInfo.equipColorNum + "..equipLevel.." + mEquipInfo.equipLevel);
        EquipStrong es;
        if (mEquipInfo.equipPosition < 5)
        {
            es = TextTranslator.instance.GetEquipStrongByID(mEquipInfo.equipCode, mEquipInfo.equipColorNum);
        }
        else
        {
            es = TextTranslator.instance.GetEquipStrongByID(mEquipInfo.equipCode);
        }
        int usefulClass = mEquipInfo.equipLevel;
        //暂时去掉等级数据
        /*        leftDataList.Add(usefulClass);
                rightDataList.Add(usefulClassRight);
                desStr = "等级: " + usefulClass.ToString() + "\n";
                desList.Add("等级: "); */
        Debug.Log("右边的..." + es);
        if (TextTranslator.instance.heroNow.force != TextTranslator.instance.heroBefore.force)
        {
            rightDataList.Add(TextTranslator.instance.heroNow.force);
        }
        if (es == null)
        {
            return;
        }
        if (es.Hp > 0)
        {
            rightDataList.Add(es.Hp * usefulClass);
        }
        if (es.Atk > 0)
        {
            rightDataList.Add(es.Atk * usefulClass);
        }
        if (es.Def > 0)
        {
            rightDataList.Add(es.Def * usefulClass);
        }
        if (es.Hit > 0)
        {
            rightDataList2.Add(es.Hit * usefulClass);
        }
        if (es.NoHit > 0)
        {
            rightDataList2.Add(es.NoHit * usefulClass);
        }
        if (es.Crit > 0)
        {
            rightDataList2.Add(es.Crit * usefulClass);
        }
        if (es.NoCrit > 0)
        {
            rightDataList2.Add(es.NoCrit * usefulClass);
        }
        if (es.DmgBonus > 0)
        {
            rightDataList2.Add(es.DmgBonus * usefulClass);
        }
        if (es.DmgReduction > 0)
        {
            rightDataList2.Add(es.DmgReduction * usefulClass);
        }
    }
    void GetDataList(Hero.EquipInfoBefore mEquipInfo)
    {
        desList.Clear();
        leftDataList.Clear();
        rightDataList.Clear();
        leftDataList2.Clear();
        rightDataList2.Clear();
        string desStr = "";
        EquipStrong es = TextTranslator.instance.GetEquipStrongByID(mEquipInfo.equipCode,mEquipInfo.equipColorNum);
        int usefulClass = mEquipInfo.equipLevel;
        int usefulClassRight = mEquipInfo.equipLevel + 1;
        //暂时去掉等级数据
/*        leftDataList.Add(usefulClass);
        rightDataList.Add(usefulClassRight);
        desStr = "等级: " + usefulClass.ToString() + "\n";
        desList.Add("等级: "); */

        Debug.Log(es);
        if (es == null)
        {
            return;
        }

        if (es.Hp > 0)
        {
            desStr += "生命: " + (es.Hp * usefulClass).ToString() + "\n";
            desList.Add("生命: ");
            leftDataList.Add(es.Hp * usefulClass);
            rightDataList.Add(es.Hp * usefulClassRight);
        }
        if (es.Atk > 0)
        {
            desStr += "攻击: " + (es.Atk * usefulClass).ToString() + "\n";
            desList.Add("攻击: ");
            leftDataList.Add(es.Atk * usefulClass);
            rightDataList.Add(es.Atk * usefulClassRight);
        }
        if (es.Def > 0)
        {
            desStr += "防御: " + (es.Def * usefulClass).ToString() + "\n";
            desList.Add("防御: ");
            leftDataList.Add(es.Def * usefulClass);
            rightDataList.Add(es.Def * usefulClassRight);
        }
        if (es.Hit > 0)
        {
            desStr += "命中: " + (es.Hit * usefulClass).ToString() + "\n";
            desList.Add("命中: ");
            leftDataList2.Add(es.Hit * usefulClass);
            rightDataList2.Add(es.Hit * usefulClassRight);
        }
        if (es.NoHit > 0)
        {
            desStr += "闪避: " + (es.NoHit * usefulClass).ToString() + "\n";
            desList.Add("闪避: ");
            leftDataList2.Add(es.NoHit * usefulClass);
            rightDataList2.Add(es.NoHit * usefulClassRight);
        }
        if (es.Crit > 0)
        {
            desStr += "暴击: " + (es.Crit * usefulClass).ToString() + "\n";
            desList.Add("暴击: ");
            leftDataList2.Add(es.Crit * usefulClass);
            rightDataList2.Add(es.Crit * usefulClassRight);
        }
        if (es.NoCrit > 0)
        {
            desStr += "抗暴: " + (es.NoCrit * usefulClass).ToString() + "\n";
            desList.Add("抗暴: ");
            leftDataList2.Add(es.NoCrit * usefulClass);
            rightDataList2.Add(es.NoCrit * usefulClassRight);
        }
        if (es.DmgBonus > 0)
        {
            desStr += "伤害加成: " + (es.DmgBonus * usefulClass).ToString() + "\n";
            desList.Add("伤害加成: ");
            leftDataList2.Add(es.DmgBonus * usefulClass);
            rightDataList2.Add(es.DmgBonus * usefulClassRight);
        }
        if (es.DmgReduction > 0)
        {
            desStr += "伤害减免: " + (es.DmgReduction * usefulClass).ToString() + "\n";
            desList.Add("伤害减免: ");
            leftDataList2.Add(es.DmgReduction * usefulClass);
            rightDataList2.Add(es.DmgReduction * usefulClassRight);
        }
    }
    string GetStrenghStr(Hero.EquipInfo mEquipInfo, bool useNext)
    {
        string desStr = "";
        //mEquipInfo = mHero.equipList[ClickIndex - 1];
        EquipStrong es = TextTranslator.instance.GetEquipStrongByID(mEquipInfo.equipCode,mEquipInfo.equipColorNum);
        int usefulClass = 0;
        if (!useNext)
        {
            usefulClass = mEquipInfo.equipLevel;
        }
        else
        {
            usefulClass = mEquipInfo.equipLevel + 1;
        }
        desStr = "等级: " + usefulClass.ToString() + "\n";
        if (es.Hp > 0)
        {
            desStr += "生命: " + (es.Hp * usefulClass).ToString() + "\n";
        }
        if (es.Atk > 0)
        {
            desStr += "攻击: " + (es.Atk * usefulClass).ToString() + "\n";
        }
        if (es.Def > 0)
        {
            desStr += "防御: " + (es.Def * usefulClass).ToString() + "\n";
        }
        if (es.Hit > 0)
        {
            desStr += "命中: " + (es.Hit * usefulClass).ToString() + "\n";
        }
        if (es.NoHit > 0)
        {
            desStr += "闪避: " + (es.NoHit * usefulClass).ToString() + "\n";
        }
        if (es.Crit > 0)
        {
            desStr += "暴击: " + (es.Crit * usefulClass).ToString() + "\n";
        }
        if (es.NoCrit > 0)
        {
            desStr += "抗暴: " + (es.NoCrit * usefulClass).ToString() + "\n";
        }
        if (es.DmgBonus > 0)
        {
            desStr += "伤害加成: " + (es.DmgBonus * usefulClass).ToString() + "\n";
        }
        if (es.DmgReduction > 0)
        {
            desStr += "伤害减免: " + (es.DmgReduction * usefulClass).ToString() + "\n";
        }
        return desStr;
    }
    
    
}
