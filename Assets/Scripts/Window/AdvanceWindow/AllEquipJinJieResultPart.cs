using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AllEquipJinJieResultPart: MonoBehaviour
{
    [SerializeField]
    private GameObject uiGride;
    [SerializeField]
    private GameObject equipJinJieResultItem;
    //-----------界面basicInfo
    [SerializeField]
    private List<GameObject> equipList;
    [SerializeField]
    private List<UISprite> frame1;
    [SerializeField]
    private List<UISprite> frame2;
    [SerializeField]
    private List<UISprite> icon1;
    [SerializeField]
    private List<UISprite> icon2;
   /* [SerializeField]
    private List<UILabel> nameLabel1;
    [SerializeField]
    private List<UILabel> nameLabel2;*/

    //------------数据
    private List<string> desList = new List<string>();
    private List<int> leftDataList = new List<int>();
    private List<int> rightDataList = new List<int>();
    private List<float> leftDataList2 = new List<float>();
    private List<float> rightDataList2 = new List<float>();

    public void SetEquipJinJieResultPart(BetterList<Hero.EquipInfoBefore> _mEquipInfoBefore, BetterList<Hero.EquipInfo> mEquipInfo)
    {
        StartCoroutine(DelayShow(_mEquipInfoBefore, mEquipInfo));
    }
    IEnumerator DelayShow(BetterList<Hero.EquipInfoBefore> _mEquipInfoBefore, BetterList<Hero.EquipInfo> mEquipInfo)
    {
        for (int i = 0; i < frame1.Count;i++ )
        {
            frame1[i].spriteName = "";
            frame2[i].spriteName = "";
            icon1[i].spriteName = "";
            icon2[i].spriteName = "";
        }
        yield return new WaitForSeconds(1.0f);
        for (int i = 0; i < frame1.Count; i++)
        {
            if (_mEquipInfoBefore[i].equipColorNum != mEquipInfo[i].equipColorNum)
            {
                frame1[i].spriteName = GetFrameStr(_mEquipInfoBefore[i].equipCode);
                frame2[i].spriteName = GetFrameStr(mEquipInfo[i].equipCode);
                icon1[i].spriteName = _mEquipInfoBefore[i].equipCode.ToString();
                icon2[i].spriteName = mEquipInfo[i].equipCode.ToString();
            }
            else
            {
                equipList[i].SetActive(false);
            }
        }
       // nameLabel1.text = GetItemName(_mEquipInfoBefore.equipCode, _mEquipInfoBefore.equipClass, _mEquipInfoBefore.equipColorNum);
        //Debug.Log("mEquipInfo.." + mEquipInfo.equipCode + "..equipClass.." + mEquipInfo.equipClass + "..equipColorNum.." + mEquipInfo.equipColorNum + "..equipLevel.." + mEquipInfo.equipLevel);
      //  nameLabel2.text = GetItemName(mEquipInfo.equipCode, mEquipInfo.equipClass, mEquipInfo.equipColorNum);
        //   fightLabel1.text = GetStrenghStr(mEquipInfo,false);//string.Format("战斗力：{0}", mEquipInfo.);
        //   fightLabel2.text = GetStrenghStr(mEquipInfo, true);//string.Format("{0}", _HeroNewData.force);
        //GetDataList(_mEquipInfoBefore);
        GetLeftDataList();
        GetRightDataList();
        yield return new WaitForSeconds(0.2f);
        Debug.Log("count..." + leftDataList.Count);
        for (int i = 0; i < leftDataList.Count; i++)
        {
            GameObject obj = NGUITools.AddChild(uiGride, equipJinJieResultItem) as GameObject;
            obj.GetComponent<EquipJinJieResultItem>().SetEquipJinJieResultItem(desList[i], leftDataList[i], rightDataList[i]);
        }
        for (int i = 0; i < leftDataList2.Count; i++)
        {
            GameObject obj = NGUITools.AddChild(uiGride, equipJinJieResultItem) as GameObject;
            obj.GetComponent<EquipJinJieResultItem>().SetEquipJinJieResultItem(desList[leftDataList.Count + i], leftDataList2[i], rightDataList2[i]);
        }
        uiGride.GetComponent<UIGrid>().Reposition();
    }
    
    string GetFrameStr(int _itemCode)
    {
        string _frameStr = "";
        TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_itemCode);
        _frameStr = string.Format("Grade{0}", mItemInfo.itemGrade);
        return _frameStr;
    }
    void GetLeftDataList()
    {
        desList.Clear();
        leftDataList.Clear();
        leftDataList2.Clear();
        string desStr = "";
        if (TextTranslator.instance.heroNow.force != TextTranslator.instance.heroBefore.force)
        {
            desStr += "战斗力: " + (TextTranslator.instance.heroBefore.force).ToString() + "\n";
            desList.Add("战斗力: ");
            leftDataList.Add(TextTranslator.instance.heroBefore.force);
        }
    }
    void GetRightDataList()
    {
        rightDataList.Clear();
        rightDataList2.Clear();
        if (TextTranslator.instance.heroNow.force != TextTranslator.instance.heroBefore.force)
        {
            rightDataList.Add(TextTranslator.instance.heroNow.force);
        }
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
