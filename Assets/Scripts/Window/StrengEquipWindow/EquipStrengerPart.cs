using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipStrengerPart : MonoBehaviour
{
    public UILabel moneyOneKeyUpNeedLabel;
    public UILabel moneyUpNeed;
    public UILabel moneyAdvaceNeed;
    public GameObject takeOnesButton;
    public GameObject OneKeyButton;
    public GameObject OnKeyDesObj;
    public GameObject AllUpButton;
    public GameObject AllAdvanceButton;
    public GameObject advanceButton;
    public List<GameObject> materailItemObjList = new List<GameObject>();
    private List<int> materailItemCodeList = new List<int>();
    //public GameObject advanceUiGride;
    //public GameObject advanceMaterailItem;

    private HeroInfo mHeroInfo;
    private Hero mHero;
    private Hero.EquipInfo mEquipInfo;
    [HideInInspector]
    public static Hero.EquipInfoBefore mEquipInfoBefore = new Hero.EquipInfoBefore();
    public BetterList<Hero.EquipInfoBefore> mEquipInfoBeforeList = new BetterList<Hero.EquipInfoBefore>();
    private int CharacterRoleID = 0;
    private int advanceNeedMoney = 0;

    private StrengEquipWindow SEW;

    // Use this for initialization
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.强化);
        UIManager.instance.UpdateSystems(UIManager.Systems.强化);

        for (int i = 0; i < materailItemObjList.Count; i++)
        {
            UIEventListener.Get(materailItemObjList[i]).onClick = ClikItemListButton;
        }
        UIEventListener.Get(AllUpButton).onClick += delegate(GameObject go)
        {
            if (CharacterRecorder.instance.GuideID[28] == 4)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
            if (IsAllUpButtonUseful())
            {
                NetworkHandler.instance.SendProcess("3020#" + CharacterRoleID.ToString() + ";");
                AllUpButton.GetComponent<BoxCollider>().enabled = false;
                Invoke("DelayAllUpBoxColliderTrue",0.5f);
            }
            else
            {
                UIManager.instance.OpenPromptWindow("装备等级不能超过战队等级", 11, false, PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(AllAdvanceButton).onClick = delegate(GameObject go)
        {
            TextTranslator.instance.heroBefore.SetHeroCardID(mHero.cardID);
            TextTranslator.instance.heroBefore.SetHeroProperty(mHero.force, mHero.level, mHero.exp, mHero.maxExp, mHero.rank, mHero.rare, mHero.classNumber, mHero.skillLevel, 
                mHero.skillNumber, mHero.HP, mHero.strength, mHero.physicalDefense, mHero.physicalCrit,mHero.UNphysicalCrit, mHero.dodge, mHero.hit, mHero.moreDamige,
                mHero.avoidDamige, mHero.aspd, mHero.move, mHero.HPAdd, mHero.strengthAdd, mHero.physicalDefenseAdd, mHero.physicalCritAdd, mHero.UNphysicalCritAdd,
                mHero.dodgeAdd, mHero.hitAdd, mHero.moreDamigeAdd, mHero.avoidDamigeAdd, 0);
            mEquipInfoBeforeList.Clear();
            for (int i = 0; i < mHero.equipList.size;i++ )
            {
                if(i < 4)
                {
                    Hero.EquipInfoBefore mEquipInfoBefore = new Hero.EquipInfoBefore();
                    mEquipInfoBefore.equipCode = mHero.equipList[i].equipCode;
                    mEquipInfoBefore.equipClass = mHero.equipList[i].equipClass;
                    mEquipInfoBefore.equipLevel = mHero.equipList[i].equipLevel;
                    mEquipInfoBefore.equipExp = mHero.equipList[i].equipExp;
                    mEquipInfoBefore.equipPosition = mHero.equipList[i].equipPosition;
                    mEquipInfoBefore.equipID = mHero.equipList[i].equipID;
                    mEquipInfoBefore.equipColorNum = mHero.equipList[i].equipColorNum;
                    mEquipInfoBeforeList.Add(mEquipInfoBefore);
                }
                Debug.LogError("mEquipInfoBeforeList:::::::: " + mEquipInfoBeforeList.size);
            }
            NetworkHandler.instance.SendProcess("3021#" + CharacterRoleID.ToString() + ";");
            AllAdvanceButton.GetComponent<BoxCollider>().enabled = false;
            Invoke("DelayAllAdvanceColliderTrue", 0.5f);
        };
        UIEventListener.Get(takeOnesButton).onClick += delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("3005#" + CharacterRoleID.ToString() + ";" + StrengEquipWindow.ClickIndex.ToString() + ";");
        };
        UIEventListener.Get(OneKeyButton).onClick += delegate(GameObject go)
        {
            if (CharacterRecorder.instance.GuideID[28] == 4)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
            if (!advanceButton.activeSelf)
            {
                int needMoney = TextTranslator.instance.GetEquipStrongCostByID(mEquipInfo.equipLevel, mHeroInfo.heroRarity, StrengEquipWindow.ClickIndex);
                if (CharacterRecorder.instance.gold >= needMoney && mEquipInfo.equipLevel < CharacterRecorder.instance.level)
                {
                    //CharacterRecorder.instance.isOneKeyState = true;

                    NetworkHandler.instance.SendProcess("3009#" + CharacterRoleID.ToString() + ";" + StrengEquipWindow.ClickIndex.ToString() + ";");
                    OneKeyButton.GetComponent<BoxCollider>().enabled = false;
                    Invoke("DelayOneKeyBoxColliderTrue",0.5f);
                    /* OneKeyButton.GetComponent<UIButton>().isEnabled = false;
                    StrengEquipWindow esp = GameObject.Find("StrengEquipWindow").GetComponent<StrengEquipWindow>();
                     for (int i = 0; i < esp.ListEquip.Count; i++)
                     {
                         esp.ListEquip[i].GetComponent<BoxCollider>().enabled = false;
                     }
                     for (int i = 0; i < esp.ListHero.Count; i++)
                     {
                         esp.ListHero[i].GetComponent<BoxCollider>().enabled = false;
                     }*/
                }
                else if (CharacterRecorder.instance.gold >= needMoney)
                {
                    UIManager.instance.OpenPromptWindow("装备等级不能超过战队等级", 11, false, PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("金币不足", PromptWindow.PromptType.Hint, null, null);
                }
            }
        };
        UIEventListener.Get(advanceButton).onClick += delegate(GameObject go)
        {
            if (CharacterRecorder.instance.GuideID[28] == 5)
            {
                CharacterRecorder.instance.GuideID[28] += 1;
                StartCoroutine(SceneTransformer.instance.NewbieGuide());
            }
            TextTranslator.instance.heroBefore.SetHeroCardID(mHero.cardID);
            TextTranslator.instance.heroBefore.SetHeroProperty(mHero.force, mHero.level, mHero.exp, mHero.maxExp, mHero.rank, mHero.rare, mHero.classNumber, mHero.skillLevel, mHero.skillNumber, mHero.HP, mHero.strength, mHero.physicalDefense, mHero.physicalCrit,
                mHero.UNphysicalCrit, mHero.dodge, mHero.hit, mHero.moreDamige, mHero.avoidDamige, mHero.aspd, mHero.move, mHero.HPAdd, mHero.strengthAdd, mHero.physicalDefenseAdd, mHero.physicalCritAdd, mHero.UNphysicalCritAdd,
                mHero.dodgeAdd, mHero.hitAdd, mHero.moreDamigeAdd, mHero.avoidDamigeAdd, 0);
            RecordEquipInfoBefore(mEquipInfo);
            CharacterRecorder.instance.isOneKeyState = false;

            if (CharacterRecorder.instance.gold >= advanceNeedMoney && IsAdvanceMaterailEnough())
            {
                NetworkHandler.instance.SendProcess("3019#" + CharacterRoleID + ";" + StrengEquipWindow.ClickIndex + ";");
                advanceButton.GetComponent<BoxCollider>().enabled = false;
                Invoke("DelayAdvanceBoxColliderTrue", 1.0f);
                //CharacterRecorder.instance.IsNeedOpenFight = false;
                //Invoke("DelayIsNeedOpenFightTrue", 3.0f);
            }
            else if (CharacterRecorder.instance.gold >= advanceNeedMoney)
            {
                UIManager.instance.OpenPromptWindow("材料不足", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                UIManager.instance.OpenPromptWindow("金币不足", PromptWindow.PromptType.Hint, null, null);
            }
        };
    }
    void DelayAllUpBoxColliderTrue()
    {
        AllUpButton.GetComponent<BoxCollider>().enabled = true;
    }
    void DelayAllAdvanceColliderTrue()
    {
        AllAdvanceButton.GetComponent<BoxCollider>().enabled = true;
    }
    void DelayAdvanceBoxColliderTrue()
    {
        advanceButton.GetComponent<BoxCollider>().enabled = true;
    }
    void DelayOneKeyBoxColliderTrue()
    {
        OneKeyButton.GetComponent<BoxCollider>().enabled = true;
    }
    void DelayIsNeedOpenFightTrue()
    {
        CharacterRecorder.instance.IsNeedOpenFight = true;
    }
    #region 一键升级到最高级
  /*  void OnKeyUpToMaxLevel()
    {
        UIEventListener.Get(OneKeyButton).onClick += delegate(GameObject go)
        {
            if (CharacterRecorder.instance.GuideID[28] == 4)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
            if (!advanceButton.activeSelf)
            {
                int needMoney = TextTranslator.instance.GetEquipStrongCostByID(mEquipInfo.equipLevel, mHeroInfo.heroRarity, StrengEquipWindow.ClickIndex);
                if (CharacterRecorder.instance.gold >= needMoney && mEquipInfo.equipLevel < CharacterRecorder.instance.level)
                {
                    CharacterRecorder.instance.isOneKeyState = true;
                    NetworkHandler.instance.SendProcess("3009#" + CharacterRoleID.ToString() + ";" + StrengEquipWindow.ClickIndex.ToString() + ";");
                    OneKeyButton.GetComponent<UIButton>().isEnabled = false;
                    StrengEquipWindow esp = GameObject.Find("StrengEquipWindow").GetComponent<StrengEquipWindow>();
                    for (int i = 0; i < esp.ListEquip.Count; i++)
                    {
                        esp.ListEquip[i].GetComponent<BoxCollider>().enabled = false;
                    }
                    for (int i = 0; i < esp.ListHero.Count; i++)
                    {
                        esp.ListHero[i].GetComponent<BoxCollider>().enabled = false;
                    }
                }
                else if (CharacterRecorder.instance.gold >= needMoney)
                {
                    UIManager.instance.OpenPromptWindow("装备等级不能超过战队等级", 11, false, PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("金币不足", PromptWindow.PromptType.Hint, null, null);
                }
            }
            else
            {
                //if (CharacterRecorder.instance.GuideID[28] == 5)
                //{
                //    CharacterRecorder.instance.GuideID[28] += 1;
                //    StartCoroutine(SceneTransformer.instance.NewbieGuide());
                //}
                TextTranslator.instance.heroBefore.SetHeroCardID(mHero.cardID);
                TextTranslator.instance.heroBefore.SetHeroProperty(mHero.force, mHero.level, mHero.exp, mHero.maxExp, mHero.rank, mHero.rare, mHero.classNumber, mHero.skillLevel, mHero.skillNumber, mHero.HP, mHero.strength, mHero.physicalDefense, mHero.physicalCrit,
                    mHero.UNphysicalCrit, mHero.dodge, mHero.hit, mHero.moreDamige, mHero.avoidDamige, mHero.aspd, mHero.move, mHero.HPAdd, mHero.strengthAdd, mHero.physicalDefenseAdd, mHero.physicalCritAdd, mHero.UNphysicalCritAdd,
                    mHero.dodgeAdd, mHero.hitAdd, mHero.moreDamigeAdd, mHero.avoidDamigeAdd, 0);
                RecordEquipInfoBefore(mEquipInfo);
                CharacterRecorder.instance.isOneKeyState = false;

                if (CharacterRecorder.instance.gold >= advanceNeedMoney && IsAdvanceMaterailEnough())
                {
                    CharacterRecorder.instance.isOneKeyState = true;
                    NetworkHandler.instance.SendProcess("3019#" + CharacterRoleID + ";" + StrengEquipWindow.ClickIndex + ";");
                    OneKeyButton.GetComponent<UIButton>().isEnabled = false;
                    StrengEquipWindow esp = GameObject.Find("StrengEquipWindow").GetComponent<StrengEquipWindow>();
                    for (int i = 0; i < esp.ListEquip.Count; i++)
                    {
                        esp.ListEquip[i].GetComponent<BoxCollider>().enabled = false;
                    }
                    for (int i = 0; i < esp.ListHero.Count; i++)
                    {
                        esp.ListHero[i].GetComponent<BoxCollider>().enabled = false;
                    }
                    advanceButton.GetComponent<BoxCollider>().enabled = false;
                    Invoke("DelayAdvanceBoxColliderTrue", 1.0f);
                    CharacterRecorder.instance.IsNeedOpenFight = false;
                    Invoke("DelayIsNeedOpenFightTrue", 3.0f);
                }
                else if (CharacterRecorder.instance.gold >= advanceNeedMoney)
                {
                    UIManager.instance.OpenPromptWindow("材料不足", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("金币不足", PromptWindow.PromptType.Hint, null, null);
                }
            }
        };
    } */
    #endregion
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
        CheakEquipLevel(mEquipInfo.equipLevel, mEquipInfo.equipColorNum);
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
    }
    bool IsAllUpButtonUseful()
    {
        for (int i = 0; i < mHero.equipList.size; i++)
        {
            if (i < 4)
            {
                mEquipInfo = mHero.equipList[i];
                if (mEquipInfo.equipLevel < CharacterRecorder.instance.level && !IsAdvanceState(mEquipInfo.equipLevel, mEquipInfo.equipColorNum))
                {
                    return true;
                }
            }
        }
        return false;
    }
    bool IsAdvanceState(int _EquipLevel, int _EquipColorNum)
    {
        if (_EquipColorNum * 5 == _EquipLevel)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    //判断强化OR进阶
    public void CheakEquipLevel(int _EquipLevel, int _EquipColorNum)
    {
        if (IsAdvanceState(_EquipLevel, _EquipColorNum))
        {
            OnKeyDesObj.SetActive(false);
            OneKeyButton.SetActive(false);
            takeOnesButton.SetActive(false);
            advanceButton.SetActive(true);
            AllUpButton.SetActive(false);
            AllAdvanceButton.SetActive(true);
            Debug.Log(mHero.equipList[StrengEquipWindow.ClickIndex - 1].equipCode + ".." + mHero.equipList[StrengEquipWindow.ClickIndex - 1].equipColorNum + ".." + mHeroInfo.heroRace);
            EquipStrongQuality esq = TextTranslator.instance.GetEquipStrongQualityByIDAndColor(mHero.equipList[StrengEquipWindow.ClickIndex - 1].equipCode, mHero.equipList[StrengEquipWindow.ClickIndex - 1].equipColorNum, mHeroInfo.heroRace);
            //Debug.LogError(esq);

            /* for (int i = advanceUiGride.transform.childCount - 1; i >= 0;i-- )
             {
                 DestroyImmediate(advanceUiGride.transform.GetChild(i).gameObject);
             }
             for (int i = 0; i < esq.materialItemList.size;i++ )
             {
                 GameObject obj = NGUITools.AddChild(advanceUiGride, advanceMaterailItem);
             }
             advanceUiGride.GetComponent<UIGrid>().repositionNow = true; */

            for (int i = 0; i < materailItemObjList.Count; i++)
            {
                if (i < esq.materialItemList.size)
                {
                    materailItemObjList[i].SetActive(true);
                    int itemCode = esq.materialItemList[i].itemCode;
                    materailItemCodeList.Clear();
                    materailItemCodeList.Add(itemCode);
                    int itemCountInBag = TextTranslator.instance.GetItemCountByID(itemCode);
                    TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(itemCode);
                    //Debug.LogError(esq.materialItemList[i].itemCode + "..." + _ItemInfo + "..." +  _ItemInfo.itemGrade);
                    materailItemObjList[i].GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo.itemGrade;
                    materailItemObjList[i].transform.FindChild("Icon").GetComponent<UISprite>().spriteName = itemCode.ToString();
                    string colorStr = itemCountInBag >= esq.materialItemList[i].itemCount ? "[ffffff]" : "[ff0000]";
                    materailItemObjList[i].transform.FindChild("Count").GetComponent<UILabel>().text = string.Format("{0}{1}[-]/{2}", colorStr, itemCountInBag, esq.materialItemList[i].itemCount);
                    GameObject _mask = materailItemObjList[i].transform.FindChild("Mask").gameObject;
                    switch (colorStr)
                    {
                        case "[ff0000]": _mask.SetActive(true); break;
                        default: _mask.SetActive(false); break;
                    }
                }
                else
                {
                    materailItemObjList[i].SetActive(false);
                }
            }

           // moneyAdvaceNeed.text = esq.Money.ToString();
            advanceNeedMoney = esq.Money;
            if (CharacterRecorder.instance.gold >= esq.Money)
            {
                moneyAdvaceNeed.text = esq.Money.ToString();
            }
            else
            {
                moneyAdvaceNeed.color = Color.white;
                moneyAdvaceNeed.text = "[ff0000]" + esq.Money.ToString();
            }
            GameObject _RedPoint = advanceButton.transform.FindChild("RedPoint").gameObject;
            if (CharacterRecorder.instance.gold >= esq.Money && IsAdvanceMaterailEnough())
            {
                _RedPoint.SetActive(true);
            }
            else
            {
                _RedPoint.SetActive(false);
            }
        }
        else
        {
            OnKeyDesObj.SetActive(true);
            OneKeyButton.SetActive(true);
         //   takeOnesButton.SetActive(true);
            advanceButton.SetActive(false);
            AllUpButton.SetActive(true);
            AllAdvanceButton.SetActive(false);
            for (int i = 0; i < materailItemObjList.Count; i++)
            {
                materailItemObjList[i].SetActive(false);
            }

            takeOnesButton.GetComponent<UIButton>().isEnabled = true;
            OneKeyButton.GetComponent<UIButton>().isEnabled = true;

           // moneyUpNeed.text = TextTranslator.instance.GetEquipStrongCostByID(_EquipLevel, mHeroInfo.heroRarity, StrengEquipWindow.ClickIndex).ToString();
            int needMoney = TextTranslator.instance.GetEquipStrongCostByID(_EquipLevel, mHeroInfo.heroRarity, StrengEquipWindow.ClickIndex);
            if (CharacterRecorder.instance.gold >= needMoney)
            {
                moneyUpNeed.text = needMoney.ToString();
            }
            else
            {
                moneyUpNeed.color = Color.white;
                moneyUpNeed.text = "[ff0000]" + needMoney.ToString();
            }

            int equipLevelInFuture = GetEquipLevelInFuture(CharacterRecorder.instance.gold, _EquipLevel);
            int moneyOneKeyUpNeed = 0;
            if (equipLevelInFuture - _EquipLevel >= 4)
            {
                for (int i = _EquipLevel; i < equipLevelInFuture;i++ )
                {
                    moneyOneKeyUpNeed += TextTranslator.instance.GetEquipStrongCostByID(i, mHeroInfo.heroRarity, StrengEquipWindow.ClickIndex);
                }
            }
            else if (equipLevelInFuture - _EquipLevel > 0)
            {
                for (int i = _EquipLevel; i < equipLevelInFuture; i++)
                {
                    moneyOneKeyUpNeed += TextTranslator.instance.GetEquipStrongCostByID(i, mHeroInfo.heroRarity, StrengEquipWindow.ClickIndex);
                }
            }
            else
            {
                moneyOneKeyUpNeed = TextTranslator.instance.GetEquipStrongCostByID(_EquipLevel, mHeroInfo.heroRarity, StrengEquipWindow.ClickIndex);
            }
            moneyOneKeyUpNeedLabel.text = CharacterRecorder.instance.gold >= moneyOneKeyUpNeed ? moneyOneKeyUpNeed.ToString() : "[ff0000]" + moneyOneKeyUpNeed.ToString();
        }
        if (_EquipLevel >= CharacterRecorder.instance.level && IsOtherCanAdvance())
        {
            AllUpButton.SetActive(false);
            AllAdvanceButton.SetActive(true);
        }
    }
    //一次升5级 升后可以达到多少级
    int GetEquipLevelInFuture(int myGold, int myEquipLevel)
    {
        int curEquipLv = myEquipLevel;//mEquipInfo.equipLevel;
        //Debug.Log("curEquipLv.." + curEquipLv + "...heroRarity..." + mHeroInfo.heroRarity);
        int curNeedMoney = TextTranslator.instance.GetEquipStrongCostByID(curEquipLv, mHeroInfo.heroRarity, StrengEquipWindow.ClickIndex);
        while (myGold - curNeedMoney >= 0 && curEquipLv - myEquipLevel < 5 && curEquipLv < CharacterRecorder.instance.level)
        {
            myGold -= curNeedMoney;
            curEquipLv += 1;
            if (curEquipLv % 5 == 0)
            {
                break;
            }
            curNeedMoney = TextTranslator.instance.GetEquipStrongCostByID(curEquipLv, mHeroInfo.heroRarity, StrengEquipWindow.ClickIndex);
        }
        //Debug.LogError("预览等级.." + curEquipLv);
        return curEquipLv;
    }
    bool IsOtherCanAdvance()
    {
        for (int i = 0; i < mHero.equipList.size;i++ )
        {
            if(i < 4)
            {
                mEquipInfo = mHero.equipList[i];
                if (IsAdvanceState(mEquipInfo.equipLevel, mEquipInfo.equipColorNum))
                {
                    return true;
                }
            }
        }
        return false;
    }
    bool IsAdvanceMaterailEnough()
    {
        EquipStrongQuality esq = TextTranslator.instance.GetEquipStrongQualityByIDAndColor(mHero.equipList[StrengEquipWindow.ClickIndex - 1].equipCode, mHero.equipList[StrengEquipWindow.ClickIndex - 1].equipColorNum, mHeroInfo.heroRace);
        for (int i = 0; i < esq.materialItemList.size; i++)
        {
            int itemCode = esq.materialItemList[i].itemCode;
            int itemCountInBag = TextTranslator.instance.GetItemCountByID(itemCode);
            if (esq.materialItemList[i].itemCount > itemCountInBag)
            {
                return false;
            }
        }
        return true;
    }

    void ClikItemListButton(GameObject go)
    {
       // UIManager.instance.OpenPanel("WayWindow", false);
        UIManager.instance.OpenSinglePanel("WayWindow", false);
        int code = int.Parse(go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName);
        GameObject _WayWindow = GameObject.Find("WayWindow");
        EquipStrongQuality esq = TextTranslator.instance.GetEquipStrongQualityByIDAndColor(mHero.equipList[StrengEquipWindow.ClickIndex - 1].equipCode, mHero.equipList[StrengEquipWindow.ClickIndex - 1].equipColorNum, mHeroInfo.heroRace);
        //Debug.LogError(esq);
        for (int i = 0; i < esq.materialItemList.size; i++)
        {
            if (code == esq.materialItemList[i].itemCode)
            {
                WayWindow.NeedItemCount = esq.materialItemList[i].itemCount;
            }
        }
        _WayWindow.GetComponent<WayWindow>().SetWayInfo(code);
        _WayWindow.layer = 11;
        foreach (Component c in _WayWindow.GetComponentsInChildren(typeof(Transform), true))
        {
            c.gameObject.layer = 11;
        }
        CharacterRecorder.instance.SweptIconID = code;
    }

}
