using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum RoleRedPointType
{
    HeroTabsRedPint = 0,//角色Tabs小红点
    EquipAdvanceRedPint = 1,//装备界面小红点
    Both = 3,//角色Tabs小红点 或 装备小红点
}
public class RoleHeroItem : MonoBehaviour
{

    int RoleID;
    int CharacterRoleID;
    int Index;
    Hero mHero;
    HeroInfo mHeroInfo;

    public GameObject gride;
    public UISprite pinJieSprite;
    GameObject ADD1;
    GameObject ADD2;
    GameObject ADD3;
    GameObject RankIcon;
    [HideInInspector]
    public GameObject Select;
    public GameObject HeroHp;
    // Use this for initialization
    void Start()
    {
        if (UIEventListener.Get(gameObject).onClick == null)
        {
            UIEventListener.Get(gameObject).onClick += delegate(GameObject go)
            {
                if (gameObject.name == "HeroItem60016")
                {
                    if (SceneTransformer.instance.CheckGuideIsFinish() == false)
                    {
                        SceneTransformer.instance.NewGuideButtonClick();
                    }
                }
                if (CheckToShowTipsOfLeavingSkill(Index, CharacterRecorder.instance.RoleTabIndex))
                {

                }
                else
                {
                    //Debug.LogError("HeroClick");
                    if (GameObject.Find("RoleWindow") != null)
                    {
                        // PlayAniOrRotation.IsNeedAutoPlayAni = true;//切换英雄不播动画
                        if (Index != TextTranslator.instance.HeadIndex)//过滤一直点击当前选中的Role
                        {
                            RoleWindow.targetRoleCardId = mHero.cardID;
                            GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetHeroClick(Index);
                            // PictureCreater.instance.PlayRoleSound(mHero.cardID);//切换hero不播放口头禅
                        }

                        /* if (GameObject.Find("RoleEquipInfoWindow") != null)
                         {
                             UIManager.instance.BackUI();
                         }*/
                    }
                }


                if (GameObject.Find("StrengEquipWindow") != null)
                {
                    //  PlayAniOrRotation.IsNeedAutoPlayAni = true;//切换英雄不播动画
                    if (Index != TextTranslator.instance.HeadIndex)//过滤一直点击当前选中的Role
                    {
                        CharacterRecorder.instance.isOneKeyState = false;
                        GameObject.Find("StrengEquipWindow").GetComponent<StrengEquipWindow>().SetHeroClick(Index);
                    }
                }
                if (GameObject.Find("RoleCombosWindow") != null)
                {
                    GameObject CombosWindow = GameObject.Find("RoleCombosWindow");
                    if (mHero.rare >= 4)
                    {
                        CombosWindow.GetComponent<RoleCombosWindow>().SetHeroInfo(mHero.cardID);
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("橙色品质以上才可以作为队长", PromptWindow.PromptType.Hint, null, null);
                        DestroyImmediate(CombosWindow);
                    }
                }
                //if (GameObject.Find("FightWindow") != null)
                //{
                //    foreach (var h in PictureCreater.instance.ListRolePicture)
                //    {
                //        Debug.Log(h.RoleID);
                //        if (h.RoleID == RoleID)
                //        {
                //            if (h.RolePosition == 0)
                //            {
                //                PictureCreater.instance.SetPosition(h.RoleObject.name, PictureCreater.instance.SelectPosition);
                //            }
                //            else
                //            {
                //                PictureCreater.instance.SetPosition(h.RoleObject.name, 0);
                //            }
                //            PictureCreater.instance.SetSequence();
                //        }
                //    }
                //    PictureCreater.instance.IsOut = true;
                //    Debug.LogError("BBBBBB");
                //}
            };
        }

        if (UIEventListener.Get(gameObject).onPress == null)
        {
            UIEventListener.Get(gameObject).onPress = delegate(GameObject go, bool IsPress)
            {
                if (GameObject.Find("FightWindow") != null)
                {
                    Debug.Log("CCCCCCCC" + RoleID);
                    foreach (var h in PictureCreater.instance.ListRolePicture)
                    {
                        Debug.Log(h.RoleID);
                        if (h.RoleID == RoleID)
                        {
                            GameObject.Find("MainCamera").GetComponent<MouseClick>().DragObject = h.RoleObject;
                            h.RoleObject.SetActive(true);
                            break;
                        }
                    }
                }
            };
        }
    }
    public void Init(bool IsNeedShowRedPoint, RoleRedPointType _RoleRedPointType, int _RoleID, int _CharacterRoleID, int _Index)
    {
        Init(_RoleID, _CharacterRoleID, _Index);
        if (IsNeedShowRedPoint)
        {
            SetHeroItemRedPoint(_RoleRedPointType);
        }
    }
    public void Init(int _RoleID, int _CharacterRoleID, int _Index)
    {
        RoleID = _RoleID;
        CharacterRoleID = _CharacterRoleID;
        Index = _Index;
        Hero h = CharacterRecorder.instance.GetHeroByRoleID(_RoleID);
        mHero = h;
        HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(h.cardID);
        mHeroInfo = hinfo;
        gameObject.transform.Find("SpriteHead").gameObject.GetComponent<UISprite>().spriteName = RoleID.ToString();
        gameObject.transform.FindChild("Level").GetComponent<UILabel>().text = h.level.ToString();

        ADD1 = this.gameObject.transform.FindChild("ADD1").gameObject;
        ADD2 = this.gameObject.transform.FindChild("ADD2").gameObject;
        ADD3 = this.gameObject.transform.FindChild("ADD3").gameObject;
        RankIcon = this.gameObject.transform.FindChild("RankIcon").gameObject;
        Select = this.gameObject.transform.FindChild("Select").gameObject;

        HeroHp = this.gameObject.transform.FindChild("HeroHP").gameObject;
        ADD1.SetActive(false);
        ADD2.SetActive(false);
        ADD3.SetActive(false);
        Select.SetActive(false);
        SetRankIcon();
        //旧的
        /*   switch (h.classNumber)
           {
               case 1:
                   this.gameObject.GetComponent<UISprite>().spriteName = "yxdi0";
                   break;
               case 2:
                   this.gameObject.GetComponent<UISprite>().spriteName = "yxdi1";
                   break;
               case 3:
                   this.gameObject.GetComponent<UISprite>().spriteName = "yxdi1";
                   ADD1.SetActive(true);
                   ADD1.GetComponent<UISprite>().spriteName = "zbkuang2";
                   break;
               case 4:
                   this.gameObject.GetComponent<UISprite>().spriteName = "yxdi2";
                   break;
               case 5:
                   this.gameObject.GetComponent<UISprite>().spriteName = "yxdi2";
                   ADD1.SetActive(true);
                   ADD1.GetComponent<UISprite>().spriteName = "zbkuang3";
                   break;
               case 6:
                   this.gameObject.GetComponent<UISprite>().spriteName = "yxdi2";
                   ADD2.SetActive(true);
                   ADD2.GetComponent<UISprite>().spriteName = "zbkuang3";
                   ADD2.transform.Find("ADDBg").GetComponent<UISprite>().spriteName = "zbkuang3";
                   //ADD1.GetComponent<UISprite>().spriteName = "zbkuang3";
                   break;
               case 7:
                   this.gameObject.GetComponent<UISprite>().spriteName = "yxdi3";
                   break;
               case 8:
                   this.gameObject.GetComponent<UISprite>().spriteName = "yxdi3";
                   ADD1.SetActive(true);
                   ADD1.GetComponent<UISprite>().spriteName = "zbkuang4";
                   break;
               case 9:
                   this.gameObject.GetComponent<UISprite>().spriteName = "yxdi3";
                   ADD2.SetActive(true);
                   ADD2.GetComponent<UISprite>().spriteName = "zbkuang4";
                   ADD2.transform.Find("ADDBg").GetComponent<UISprite>().spriteName = "zbkuang4";
                   //ADD1.GetComponent<UISprite>().spriteName = "zbkuang4";
                   break;
               case 10:
                   this.gameObject.GetComponent<UISprite>().spriteName = "yxdi3";
                   ADD3.SetActive(true);
                   ADD3.GetComponent<UISprite>().spriteName = "zbkuang4";
                   ADD3.transform.Find("ADDBg1").GetComponent<UISprite>().spriteName = "zbkuang4";
                   ADD3.transform.Find("ADDBg2").GetComponent<UISprite>().spriteName = "zbkuang4";
                   //ADD1.GetComponent<UISprite>().spriteName = "zbkuang4";
                   break;
               case 11:
                   this.gameObject.GetComponent<UISprite>().spriteName = "yxdi4";
                   break;
               case 12:
                   this.gameObject.GetComponent<UISprite>().spriteName = "yxdi4";
                   ADD1.SetActive(true);
                   ADD1.GetComponent<UISprite>().spriteName = "zbkuang5";
                   break;
               default:
                   break;
           } */
        gride = this.gameObject.transform.FindChild("Grid").gameObject;
        pinJieSprite = this.gameObject.transform.FindChild("SpritePinJie").GetComponent<UISprite>();
        DestroyGride();
        int addNum = TextTranslator.instance.SetHeroNameColor(this.gameObject.GetComponent<UISprite>(), pinJieSprite, mHero.classNumber);
        for (int i = 0; i < addNum; i++)
        {
            GameObject obj = NGUITools.AddChild(gride, pinJieSprite.gameObject);
            obj.SetActive(true);
        }
        UIGrid _UIGrid = gride.GetComponent<UIGrid>();
        _UIGrid.sorting = UIGrid.Sorting.Horizontal;
        _UIGrid.pivot = UIWidget.Pivot.Center;
        _UIGrid.animateSmoothly = true;
    }
    void DestroyGride()
    {
        for (int i = gride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(gride.transform.GetChild(i).gameObject);
        }
    }
    private void SetHeroItemRedPoint(RoleRedPointType _RoleRedPointType)
    {
        GameObject _RedPoint = gameObject.transform.FindChild("RedPoint").gameObject;
        if ((_RoleRedPointType == RoleRedPointType.HeroTabsRedPint &&GetHeroRedPointStateOfHeroTabs(mHero)) ||
            (_RoleRedPointType == RoleRedPointType.EquipAdvanceRedPint && (GetHeroRedPointStateOfEquip(mHero) || SetRedPointStateOfBaoWuAwake()) )|| GetRedPintDowerTabs(mHero))
        {
            _RedPoint.SetActive(true);
        }
        else
        {
            _RedPoint.SetActive(false);
        }
    }

    bool GetRedPintDowerTabs(Hero _dowerHero)//天赋红点
    {
        //string[] ite = TextTranslator.instance.GetDowerByheroID(_dowerHero.characterRoleID);

        //    for (int i = 1; i < 19; i++)
        //    {
        //        for (int j = 0; j < ite.Length; j++)
        //        {
        //            if (TextTranslator.instance.GetInnatesByTwo(i, int.Parse(ite[j])).TalentCost<CharacterRecorder.instance.level)
        //            {
        //                return true;
        //            }
        //        }
        //    }
        if (TextTranslator.instance.GetNowNum(_dowerHero.characterRoleID)>0)
        {
            return true;
        }
        return false;
    }
    
    bool GetHeroRedPointStateOfHeroTabs(Hero _curHero)
    {
        //升品
        RoleClassUp rcu = TextTranslator.instance.GetRoleClassUpInfoByID(_curHero.cardID, _curHero.classNumber);
        if (_curHero.level > rcu.Levelcap && GetShengPinOneContionState(rcu.NeedItemList) == true)
        {
            return true;
        }
        //军衔
        RoleBreach rb = TextTranslator.instance.GetRoleBreachByID(_curHero.cardID, _curHero.rank);
        int item1Count = TextTranslator.instance.GetItemCountByID(10102);
        int item2Count = TextTranslator.instance.GetItemCountByID(rb.roleId + 10000);
        if (CharacterRecorder.instance.lastGateID > 10008 && _curHero.level >= rb.levelCup && item1Count >= rb.stoneNeed && item2Count >= rb.debrisNum)
        {
            return true;
        }
        return false;
    }
    bool GetShengPinOneContionState(List<Item> classUpList)
    {
        for (int i = 0; i < classUpList.Count; i++)
        {
            int bagItemCount1 = TextTranslator.instance.GetItemCountByID(classUpList[i].itemCode);
            if (bagItemCount1 < classUpList[i].itemCount)
            {
                return false;
            }
        }
        return true;
    }
    bool GetHeroRedPointStateOfEquip(Hero mHero)
    {
        foreach (var _OneEquipInfo in mHero.equipList)
        {
            if (_OneEquipInfo.equipPosition < 5)
            {
                GameObject _RedPoint = this.transform.FindChild("RedPoint").gameObject;
                //Debug.LogError(_OneEquipInfo.equipColorNum + "..." + IsAdvanceState(_OneEquipInfo.equipLevel, _OneEquipInfo.equipColorNum));
                int _EquipLevel = _OneEquipInfo.equipLevel;
                int _EquipColorNum = _OneEquipInfo.equipColorNum;
                if (IsAdvanceState(_EquipLevel, _EquipColorNum) && IsEnoughToAdvance(_OneEquipInfo) && IsAdvanceMaterailEnough(_OneEquipInfo))//升品
                {
                    return true;
                }
                else if (IsAdvanceState(_EquipLevel, _EquipColorNum) == false && IsEnoughToUpGrade(_OneEquipInfo) && _OneEquipInfo.equipLevel < CharacterRecorder.instance.level)//升级
                {
                    return true;
                }
                else if(CharacterRecorder.instance.WeaponMainWindow(mHero.cardID,mHero.WeaponList[0].WeaponID,mHero.WeaponList[0].WeaponClass,mHero.WeaponList[0].WeaponStar))
                {
                    return true;
                }
            }
        }
        return false;
    }
    bool IsEnoughToUpGrade(Hero.EquipInfo _OneEquipInfo)
    {
        int needMoney = TextTranslator.instance.GetEquipStrongCostByID(_OneEquipInfo.equipLevel, mHeroInfo.heroRarity, _OneEquipInfo.equipPosition);
        if (CharacterRecorder.instance.gold >= needMoney)
        {
            return true;
        }
        return false;
    }
    bool IsAdvanceMaterailEnough(Hero.EquipInfo _OneEquipInfo)
    {
        EquipStrongQuality esq = TextTranslator.instance.GetEquipStrongQualityByIDAndColor(_OneEquipInfo.equipCode, _OneEquipInfo.equipColorNum, mHeroInfo.heroRace);
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
    bool IsEnoughToAdvance(Hero.EquipInfo _OneEquipInfo)
    {
        int _EquipPosition = _OneEquipInfo.equipPosition;
        EquipStrongQuality esq = TextTranslator.instance.GetEquipStrongQualityByIDAndColor(mHero.equipList[_EquipPosition - 1].equipCode, mHero.equipList[_EquipPosition - 1].equipColorNum, mHeroInfo.heroRace);
        if (CharacterRecorder.instance.gold >= esq.Money)
        {
            return true;
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
    #region 饰品觉醒小红点 (新逻辑)
    bool SetRedPointStateOfBaoWuAwake()
    {
        // bool canAwakeState = IsCanAwake();
        bool canAwakeState = IsCanAwake(5) || IsCanAwake(6);
        return canAwakeState;
    }

    bool IsCanAwake(int equipPos)
    {
        if (CharacterRecorder.instance.level < 16)
        {
            return false;
        }
        Hero.EquipInfo mEquipInfo = mHero.equipList[equipPos - 1];
        TextTranslator.ItemInfo _itemInfo = TextTranslator.instance.GetItemByItemCode(mEquipInfo.equipCode);
        if (_itemInfo != null)
        {
            /* foreach (var _hero in CharacterRecorder.instance.ownedHeroList)
             {
                 if (CharacterRoleID == _hero.characterRoleID)
                 {
                     mHero = _hero;
                     break;
                 }
             }*/
            mHeroInfo = TextTranslator.instance.GetHeroInfoByHeroID(mHero.cardID);
            List<Item> _mList = new List<Item>();
            _mList = TextTranslator.instance.GetAllBaoWuInBag();
            for (int i = _mList.Count - 1; i >= 0; i--)
            {
                EquipStrong _myEquipStrong = TextTranslator.instance.GetEquipStrongByID(_mList[i].itemCode);
                //Debug.Log(_myEquipStrong.Part);
                if (_myEquipStrong == null || _mList[i].itemGrade <= _itemInfo.itemGrade || _myEquipStrong.Race != mHeroInfo.heroRace || _myEquipStrong.Part != mEquipInfo.equipPosition)
                {
                    _mList.Remove(_mList[i]);
                }
            }
            List<Item> usefulList = new List<Item>();
            for (int i = _mList.Count - 1; i >= 0; i--)
            {
                if (!usefulList.Contains(_mList[i]) && _mList[i].itemCount > 0)
                {
                    usefulList.Add(_mList[i]);
                }
            }
            if (_mList.Count > 0)
            {
                return true;
            }
            else
            {
                //UIManager.instance.OpenPromptWindow("没有可更换的饰品", PromptWindow.PromptType.Hint, null, null);
                return false;
            }
        }
        else
        {
            //UIManager.instance.OpenPromptWindow("此装备不是Item", PromptWindow.PromptType.Hint, null, null);
            return false;
        }
    }
    #endregion
    //设置军衔
    void SetRankIcon()
    {
        //RankIcon.GetComponent<UISprite>().spriteName = string.Format("rank{0}", (mHero.rank + 1).ToString("00"));
        //RankIcon.GetComponent<UISprite>().spriteName = string.Format("rank{0}", mHero.rank.ToString("00"));
        RankIcon.transform.GetChild(mHero.rank).gameObject.SetActive(true);
        switch (mHero.rank)
        {
            case 1:
                RankIcon.transform.FindChild("Label").GetComponent<UILabel>().text = "下士";
                break;
            case 2:
                RankIcon.transform.FindChild("Label").GetComponent<UILabel>().text = "中士";
                break;
            case 3:
                RankIcon.transform.FindChild("Label").GetComponent<UILabel>().text = "上士";
                break;
            case 4:
                RankIcon.transform.FindChild("Label").GetComponent<UILabel>().text = "少尉";
                break;
            case 5:
                RankIcon.transform.FindChild("Label").GetComponent<UILabel>().text = "中尉";
                break;
            case 6:
                RankIcon.transform.FindChild("Label").GetComponent<UILabel>().text = "上尉";
                break;
            case 7:
                RankIcon.transform.FindChild("Label").GetComponent<UILabel>().text = "少校";
                break;
            case 8:
                RankIcon.transform.FindChild("Label").GetComponent<UILabel>().text = "中校";
                break;
            case 9:
                RankIcon.transform.FindChild("Label").GetComponent<UILabel>().text = "上校";
                break;
            case 10:
                RankIcon.transform.FindChild("Label").GetComponent<UILabel>().text = "少将";
                break;
            case 11:
                RankIcon.transform.FindChild("Label").GetComponent<UILabel>().text = "中将";
                break;
            case 12:
                RankIcon.transform.FindChild("Label").GetComponent<UILabel>().text = "上将";
                break;
            default:
                break;
        }
    }

    //英雄血条开启
    public void OpenHeroHp(bool isOpen)
    {
        if (isOpen)
        {
            HeroHp.SetActive(true);
        }
        else
        {
            HeroHp.SetActive(false);
        }
    }

    public void SetHeroHpInfo(float NowHp, float AllHp, int NowAger)
    {
        HeroHp.SetActive(true);
        HeroHp.transform.Find("Blood").GetComponent<UISlider>().value = NowHp / AllHp;
        if (NowAger > 1000)
        {
            NowAger = 1000;
        }
        HeroHp.transform.Find("Skill").GetComponent<UISlider>().value = NowAger / 1000f;
        //for (int i = 0; i < NowAger; i++)
        //{
        //    HeroHp.transform.Find("Skill/SpriteSkill" + (i + 1).ToString() + "/Anger").gameObject.SetActive(true);
        //}
        //if (NowAger == 5)
        //{
        //    HeroHp.transform.Find("Skill/SpriteSkill5").gameObject.SetActive(true);
        //}
    }

    int clickRoleIndex;
    int clickTabNum;
    #region 提示是否确认离开技能界面
    bool CheckToShowTipsOfLeavingSkill(int clickRoleIndex, int clickTabNum)
    {
        if (GameObject.Find("RoleWindow") == null)
        {
            return false;
        }
        Hero curHero = CharacterRecorder.instance.ownedHeroList[TextTranslator.instance.HeadIndex];
        this.clickRoleIndex = clickRoleIndex;
        this.clickTabNum = clickTabNum;
        RoleDestiny Old_rd = TextTranslator.instance.GetRoleDestinyByID(curHero.cardID, curHero.skillLevel);
        float sliderValue = 1 - (float)curHero.skillNumber / (float)Old_rd.MaxExp;
        //Debug.Log("clickTabNum..." + clickTabNum + "..sliderValue.." + sliderValue);
        if (CharacterRecorder.instance.RoleTabIndex == 6 && sliderValue < 1.0f && TextTranslator.instance.HeadIndex != clickRoleIndex)
        {
            //Debug.LogError("弹出提示");
            UIManager.instance.OpenPromptWindow("当前技能槽内还有经验，每天凌晨5点清空，建议完成升级，避免损失。", PromptWindow.PromptType.Confirm, CheckLeavingSkillUI, null);
            return true;
        }
        return false;
    }
    void CheckLeavingSkillUI()
    {
        CharacterRecorder.instance.RoleTabIndex = clickTabNum;
        TextTranslator.instance.HeadIndex = clickRoleIndex;
        PlayAniOrRotation.IsNeedAutoPlayAni = true;
        //Debug.LogError("HeroClick");
        if (GameObject.Find("RoleWindow") != null)
        {
            GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetHeroClick(Index);
        }
    }
    #endregion
}
