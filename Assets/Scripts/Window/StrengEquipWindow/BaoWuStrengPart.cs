using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BaoWuStrengPart: MonoBehaviour
{
    public UISlider _Slider;
    public UISlider _SliderAfterAdd;
    public UILabel LabelExp;

    public UILabel BaoWuRightMoney;
    private int baoWuStrengNeedMoney = 0;
    
    public GameObject BaoWuRightOneStrengButton;
    public GameObject BaoWuRightAutoSelectButton;
    public GameObject LevelUpEffect;

    public List<GameObject> ExpItemObjList;

    private List<Item> listSelectEquipItem = new List<Item>();
    public  List<SelectItemCodeAndIndex> listSelectItemCodeAndIndex = new List<SelectItemCodeAndIndex>();//数据A
    private HeroInfo mHeroInfo;
    private Hero mHero;
    private Hero.EquipInfo mEquipInfo;
    private int CharacterRoleID = 0;
    
    private StrengEquipWindow SEW;
    [HideInInspector]
    public static Hero.EquipInfoBefore mEquipInfoBefore = new Hero.EquipInfoBefore();
    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < ExpItemObjList.Count; i++)
        {
            if (UIEventListener.Get(ExpItemObjList[i]).onClick == null)
            {
                UIEventListener.Get(ExpItemObjList[i]).onClick += delegate(GameObject go)
                {
                    UIManager.instance.OpenPanel("EquipSelectWindow", false);
                    GameObject _EquipSelectWindow = GameObject.Find("EquipSelectWindow");
                    _EquipSelectWindow.layer = 11;
                    foreach (Component c in _EquipSelectWindow.GetComponentsInChildren(typeof(Transform), true))
                    {
                        c.gameObject.layer = 11;
                    }
                    return;
                };
            }
        }
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
        SetExpSliderOnSelect(CharacterRoleID, mEquipInfo.equipPosition, curSumAddEXP);
    }
    void SetExpSliderOnSelect(int _CharacterRoleID, int _equipPosition,int curSumAddEXP)
    {
        if (_equipPosition >= 5)
        {
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
                    GameObject.Find("StrengEquipWindow").GetComponent<StrengEquipWindow>().SetEquipInfoOnlyForBaoWuStreng(equipLevelInFuture);
                    int sumMoneyCostInFuture = 0;
                    for (int i = _equipInfo.equipLevel; i < equipLevelInFuture;i++ )
                    {
                        Debug.Log("level..." + i + "..heroRarity..." + mHeroInfo.heroRarity + "....pos..." + StrengEquipWindow.ClickIndex);
                        sumMoneyCostInFuture += TextTranslator.instance.GetEquipStrongCostByID(i, mHeroInfo.heroRarity, StrengEquipWindow.ClickIndex);
                    }
                    baoWuStrengNeedMoney = sumMoneyCostInFuture;
                    if (sumMoneyCostInFuture == 0)
                    {
                        BaoWuRightMoney.gameObject.SetActive(false);
                    }
                    else
                    {
                        BaoWuRightMoney.gameObject.SetActive(true);
                    }
                    string moneyLabelColor = "";
                    if(CharacterRecorder.instance.gold < baoWuStrengNeedMoney)
                    {
                        moneyLabelColor = "[ff0000]";
                    }
                    BaoWuRightMoney.text = moneyLabelColor + sumMoneyCostInFuture.ToString();
                    break;
                }
            }
        }
    }
    int GetEquipLevelInFuture(int curSumAddEXP)
    {
        int curEquipLv = mEquipInfo.equipLevel;
        //Debug.Log("curEquipLv.." + curEquipLv + "...heroRarity..." + mHeroInfo.heroRarity);
        int curNeedEXp = TextTranslator.instance.GetEquipStrongCostDataByID(curEquipLv, mHeroInfo.heroRarity).NeedExp;
        while (curSumAddEXP - curNeedEXp >= 0)
        {
            curSumAddEXP -= curNeedEXp;
            curEquipLv += 1;
            curNeedEXp = TextTranslator.instance.GetEquipStrongCostDataByID(curEquipLv, mHeroInfo.heroRarity).NeedExp;
        }
        //Debug.LogError("预览等级.." + curEquipLv);
        return curEquipLv;
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
    private void StrenghBaoWu()
    {
        if (listSelectEquipItem.Count > 0 && CharacterRecorder.instance.gold >= baoWuStrengNeedMoney)
        {
            string idListStr = "";
            for (int i = 0; i < listSelectEquipItem.Count; i++)
            {
                idListStr += listSelectEquipItem[i].itemCode + "$";
            }
            TextTranslator.instance.heroBefore.SetHeroCardID(mHero.cardID);
            TextTranslator.instance.heroBefore.SetHeroProperty(mHero.force, mHero.level, mHero.exp, mHero.maxExp, mHero.rank, mHero.rare, mHero.classNumber, mHero.skillLevel, mHero.skillNumber, mHero.HP, mHero.strength, mHero.physicalDefense, mHero.physicalCrit,
                mHero.UNphysicalCrit, mHero.dodge, mHero.hit, mHero.moreDamige, mHero.avoidDamige, mHero.aspd, mHero.move, mHero.HPAdd, mHero.strengthAdd, mHero.physicalDefenseAdd, mHero.physicalCritAdd, mHero.UNphysicalCritAdd,
                mHero.dodgeAdd, mHero.hitAdd, mHero.moreDamigeAdd, mHero.avoidDamigeAdd, 0);
            RecordEquipInfoBefore(this.mEquipInfo);
            NetworkHandler.instance.SendProcess("3016#" + CharacterRoleID.ToString() + ";" + StrengEquipWindow.ClickIndex.ToString() + ";" + idListStr + ";");
            CharacterRecorder.instance.IsNeedOpenFight = false;
            Invoke("DelayIsNeedOpenFightTrue", 3.0f);
        }
        else if (listSelectEquipItem.Count == 0)
        {
            UIManager.instance.OpenPromptWindow("请选择装备", PromptWindow.PromptType.Hint, null, null);
        }
        else if(CharacterRecorder.instance.gold < baoWuStrengNeedMoney)
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
    //启动
    public void StarPart(Hero _hero, int _CharacterRoleID, HeroInfo _heroinfo, int _equipPosition)
    {
        mHero = _hero;
        CharacterRoleID = _CharacterRoleID;
        mHeroInfo = _heroinfo;
        SEW = GameObject.Find("StrengEquipWindow").GetComponent<StrengEquipWindow>();
        StrengEquipWindow.ClickIndex = _equipPosition;
        //SetCurSelectEquipItem(_equipPosition);
        Debug.Log("********    " + _CharacterRoleID);
        mEquipInfo = mHero.equipList[_equipPosition - 1];
       /* foreach (var e in mHero.equipList)
        {
            TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(e.equipCode);
            //SetEquip(mHero.characterRoleID, e.equipCode, e.equipID, e.equipPosition, e.equipClass, e.equipLevel);
        }*/
        ClearSelectEquipItems();
        baoWuStrengNeedMoney = TextTranslator.instance.GetEquipStrongCostByID(mEquipInfo.equipLevel, mHeroInfo.heroRarity, _equipPosition);
        string moneyLabelColor = "";
        if (CharacterRecorder.instance.gold < baoWuStrengNeedMoney)
        {
            moneyLabelColor = "[ff0000]";
        }
        if (baoWuStrengNeedMoney == 0)
        {
            BaoWuRightMoney.gameObject.SetActive(false);
        }
        else
        {
            BaoWuRightMoney.gameObject.SetActive(true);
        }
        BaoWuRightMoney.text = moneyLabelColor + baoWuStrengNeedMoney.ToString();// TextTranslator.instance.GetEquipStrongCostByID(mEquipInfo.equipLevel, mHeroInfo.heroRarity, _equipPosition).ToString();
        SetExpSlider(_CharacterRoleID, _equipPosition);
        UIEventListener.Get(BaoWuRightOneStrengButton).onClick = delegate(GameObject go)
        {
            StrenghBaoWu();
        };


        UIEventListener.Get(BaoWuRightAutoSelectButton).onClick = delegate(GameObject go)
        {
            ClickAutoSelectButton();
        };

    }
    void ClickAutoSelectButton()
    {
        int TotalCount = 0;
        listSelectEquipItem.Clear();
        listSelectItemCodeAndIndex.Clear();
        BetterList<Item> usefulItemList = new BetterList<Item>();
        for (int i = 0; i < TextTranslator.instance.bagItemList.size;i++ )
        {
            if (TextTranslator.instance.bagItemList[i].itemCode > 51010 && TextTranslator.instance.bagItemList[i].itemCode < 60000 && TextTranslator.instance.bagItemList[i].itemCount > 0)
            {
                usefulItemList.Add(TextTranslator.instance.bagItemList[i]);
            }
        }
        usefulItemList = GetSumFinalInBag(usefulItemList);
        for (int i = usefulItemList.size - 1; i >= 0; i--)
        {
            if (TotalCount < 5 && usefulItemList[i].itemCode == 59000)
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
        for (int i = usefulItemList.size - 1; i >= 0; i--)
        {
            if (TotalCount < 5 && usefulItemList[i].itemGrade == 3 && usefulItemList[i].itemCode != 59000)
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
        for (int i = usefulItemList.size - 1; i >= 0; i--)
        {
            if (TotalCount < 5  && usefulItemList[i].itemCode == 59010)
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
        for (int i = usefulItemList.size - 1; i >= 0; i--)
        {
            if (TotalCount < 5 && usefulItemList[i].itemCode != 59000 && usefulItemList[i].itemCode != 59010 && usefulItemList[i].itemGrade != 3
                && usefulItemList[i].itemGrade != 5 && usefulItemList[i].itemGrade != 6)//自动选择 去掉橙色、红色
            {
                int _count = usefulItemList[i].itemCount >= 5 ? 5 : usefulItemList[i].itemCount;
                for (int j = 0; j < _count; j++)
                {
                    if (TotalCount < 5)
                    {
                        TotalCount++;
                        Debug.Log(usefulItemList[i].itemGrade + "..TotalCount.." + TotalCount);
                        GameObject.Find("EXPItem" + TotalCount.ToString()).GetComponent<UISprite>().spriteName = "Grade" + usefulItemList[i].itemGrade;
                        GameObject.Find("EXPItem" + TotalCount.ToString()).transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName = usefulItemList[i].itemCode.ToString();
                        listSelectItemCodeAndIndex.Add(new SelectItemCodeAndIndex(usefulItemList[i].itemCode, j));
                        listSelectEquipItem.Add(usefulItemList[i]);
                    }
                }
                
            }
        }
        if (TotalCount == 0)
        {
            UIManager.instance.OpenPromptWindow("当前没有紫色或其以下品质的饰品\n请夺宝或手动添加", 11, false, PromptWindow.PromptType.Hint, null, null);
        }
        int curSumAddEXP = 0;
        for (int i = 0; i < listSelectEquipItem.Count;i++ )
        {
            curSumAddEXP += int.Parse(listSelectEquipItem[i].feedExp);
        }
        SetExpSliderOnSelect(CharacterRoleID, mEquipInfo.equipPosition, curSumAddEXP);
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
    private void ClickLastTwoEquipItem(int _IndexInList, GameObject go)
    {
        if (_IndexInList != 4 && _IndexInList != 5)
        {
            return;
        }
        StrengEquipWindow.ClickIndex = _IndexInList + 1;
        GameObject.Find("StrengEquipWindow").GetComponent<StrengEquipWindow>().SetEquipPosition(StrengEquipWindow.ClickIndex);

        if (go.transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName == "add")
        {

        }
        else
        {
            mEquipInfo = mHero.equipList[_IndexInList];

            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(mHero.equipList[_IndexInList].equipCode);

            //SetEquipInfo();
        }
    }
    void SetExpSlider(int _CharacterRoleID, int _equipPosition)
    {
        if (_equipPosition >= 5)
        {
            foreach (var h in CharacterRecorder.instance.ownedHeroList)
            {
                if (_CharacterRoleID == h.characterRoleID)
                {
                    Hero curHero = h;
                    Hero.EquipInfo _equipInfo = curHero.equipList[_equipPosition - 1];
                    int curExp = curHero.equipList[_equipPosition - 1].equipExp;
                    int maxEXp = TextTranslator.instance.GetEquipStrongCostDataByID(_equipInfo.equipLevel,mHeroInfo.heroRarity).NeedExp;
                    _Slider.value = (float)curExp / (float)maxEXp;
                    _SliderAfterAdd.value = 0;
                    LabelExp.text = curExp.ToString() + "/" + maxEXp.ToString();
                    break;
                }
            }
        }
    }
    //以下宝物强化
    public void RestSetBaoWuLevel(int _CharacterRoleID, int _equipPosition, int _EquipID, int _JingLianLv, int _Lv, int _ExpLv)
    {
        baoWuStrengNeedMoney = TextTranslator.instance.GetEquipStrongCostByID(_Lv, mHeroInfo.heroRarity, StrengEquipWindow.ClickIndex);
        string moneyLabelColor = "";
        if (CharacterRecorder.instance.gold < baoWuStrengNeedMoney)
        {
            moneyLabelColor = "[ff0000]";
        }
        if (baoWuStrengNeedMoney == 0)
        {
            BaoWuRightMoney.gameObject.SetActive(false);
        }
        else
        {
            BaoWuRightMoney.gameObject.SetActive(true);
        }
        BaoWuRightMoney.text = moneyLabelColor + baoWuStrengNeedMoney.ToString();//TextTranslator.instance.GetEquipStrongCostByID(_Lv, mHeroInfo.heroRarity, SEW.ClickIndex).ToString();
        SetExpSlider(_CharacterRoleID, _equipPosition);
    }
    public void SetEquipLevel(int _CharacterRoleID, int _Index, int _EquipID, int _Class, int _Lv, int _Exp)
    {
        foreach (var h in CharacterRecorder.instance.ownedHeroList)
        {
            if (h.characterRoleID == _CharacterRoleID)
            {
                foreach (var e in h.equipList)
                {
                    if (e.equipID == _EquipID)
                    {
                        e.equipLevel = _Lv;
                        e.equipClass = _Class;
                        e.equipExp = _Exp;
                        break;
                    }
                }
                break;
            }
        }
    }

    public void SetEquipIcon(int _equipPosition, int _equipClass)
    {
        Debug.Log(_equipPosition + "      " + _equipClass);
        int number = 0;
        if (mHeroInfo.heroBio == 1)
        {
            number = 1;
        }
        else
        {
            if (mHeroInfo.heroFly == 0)
            {
                number = 2;
            }
            else
            {
                number = 3;
            }
        }
        //SetCurSelectEquipItem(_equipPosition);
        EquipStrongQuality esq = TextTranslator.instance.GetEquipStrongQualityByID(_equipClass, number, _equipPosition);
        //if (EquipRight.activeSelf)
        {
            // EquipRightEquipIcon.spriteName = esq.EquipCode.ToString();


            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(mHero.equipList[StrengEquipWindow.ClickIndex - 1].equipCode);

        }
        // else if (BaoWuRight.activeSelf)
        {
            // BaoWuRightEquipIcon.spriteName = esq.EquipCode.ToString();
            baoWuStrengNeedMoney = esq.Money;
            if (baoWuStrengNeedMoney == 0)
            {
                BaoWuRightMoney.gameObject.SetActive(false);
            }
            else
            {
                BaoWuRightMoney.gameObject.SetActive(true);
            }
            BaoWuRightMoney.text = esq.Money.ToString();

            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(mHero.equipList[0].equipCode);

        }

    }

}
