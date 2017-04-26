using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LittleHelperWindow : MonoBehaviour
{
    public List<GameObject> ListTabs = new List<GameObject>();
    public GameObject CloseButton;
    public GameObject uiGride;
    public GameObject helperItem;
    private List<GameObject> itemObjList = new List<GameObject>();
    private int tabNum = 1;
    private Dictionary<int, BetterList<HelperItemData>> HelperWindowDataDic = new Dictionary<int, BetterList<HelperItemData>>();
    private Vector3 scrollViewInitPos = new Vector3(105, -48, 0);//new Vector3(0, -65, 0);

    private List<Hero> myHero = new List<Hero>();

    private float myTrainNumber = 0;

    private int myTrainCount1 = 0;
    private int myTrainCount2 = 0;
    private int myTrainCount3 = 0;

    private int carCount = 0;

    private Dictionary<int, List<SuperCar>> carDic = new Dictionary<int, List<SuperCar>>();

    private int openCarCount = 0;

    private int seedMsgCount = 0;

    private int nuclearWeaponCount = 0;

    private int heroCount = 0;

    private float lligence = 0;
    // Use this for initialization
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.小贴士);
        UIManager.instance.UpdateSystems(UIManager.Systems.小贴士);
        for (int i = uiGride.transform.childCount - 1; i >= 0; i--)
        {
            itemObjList.Add(uiGride.transform.GetChild(i).gameObject);
        }
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                Destroy(this.gameObject);
            };
        }
        for (int i = 0; i < ListTabs.Count; i++)
        {
            UIEventListener.Get(ListTabs[i]).onClick = ClickListTabs;
        }
        AddMyHero();
        for (int i = 0; i < myHero.Count; i++)
        {
            NetworkHandler.instance.SendProcess(string.Format("1025#{0};", myHero[i].characterRoleID));
            NetworkHandler.instance.SendProcess(string.Format("3301#{0};{1};", myHero[i].characterRoleID, 1));

            //if (myHero[i].cardID == 60030)
            //{
            //    for (int j = 0; j < myHero[i].SuperCarList.size; j++)
            //    {
            //        Debug.LogError("-------:   " + myHero[i].SuperCarList[j].SuperCarType1);
            //        Debug.LogError("-------:   " + myHero[i].SuperCarList[j].SuperCarId1);
            //        //Debug.LogError("-------:   " + myHero[i].SuperCarList[j].SuperCarType2);
            //        //Debug.LogError("-------:   " + myHero[i].SuperCarList[j].SuperCarType3);
            //        //Debug.LogError("-------:   " + myHero[i].SuperCarList[j].SuperCarType4);
            //        //Debug.LogError("-------:   " + myHero[i].SuperCarList[j].SuperCarType5);
            //        //Debug.LogError("-------:   " + myHero[i].SuperCarList[j].SuperCarType6);
            //    }
            //}

        }
        SendLaboratoryMsg();
        NetworkHandler.instance.SendProcess("1601#;");
        //InitData();

    }
    /// <summary>
    /// 获取自己角色的前6个
    /// </summary>
    private void AddMyHero()
    {
        //按战力从大到小排序
        CharacterRecorder.instance.SortHeroListByForce();
        for (int i = 0; i < CharacterRecorder.instance.ownedHeroList.size; i++)
        {
            if (i < 6)
            {
                myHero.Add(CharacterRecorder.instance.ownedHeroList[i]);
            }
            else
            {
                break;
            }
        }
    }

    #region Tab处理
    private void ClickListTabs(GameObject go)
    {
        uiGride.transform.parent.GetComponent<UIPanel>().clipOffset = Vector2.zero;
        uiGride.transform.parent.localPosition = scrollViewInitPos;
        SpringPanel _SpringPanel = uiGride.transform.parent.GetComponent<SpringPanel>();
        if (_SpringPanel != null)
        {
            _SpringPanel.enabled = false;
        }

        int roleType = int.Parse(go.name[9].ToString());
        go.GetComponent<UIToggle>().value = true;
        SetTab(roleType);
    }
    void SetTab(int tabNum)
    {
        this.tabNum = tabNum;
        SetReformLabWindow(HelperWindowDataDic[tabNum]);
    }

    #endregion
    void SetReformLabWindow(BetterList<HelperItemData> mList)
    {
        CleraUIGride();
        for (int i = 0; i < mList.size; i++)
        {
            GameObject obj = NGUITools.AddChild(uiGride, helperItem);
            obj.name = mList[i].Icon.ToString() + i;
            obj.GetComponent<LittleHelperItem>().SetLittleHelperItem(mList[i]);
            itemObjList.Add(obj);
            UIEventListener.Get(obj).onClick = delegate(GameObject go)
            {
                if (go.GetComponent<LittleHelperItem>().ThisIsSelectItem)
                {
                    return;
                }
                SetItemSelect(go);
            };
        }
        uiGride.GetComponent<UIGrid>().repositionNow = true;
        if (itemObjList.Count > 0)
        {
            SetItemSelect(itemObjList[0]);
        }

    }
    private void SetItemSelect(GameObject go)
    {
        for (int i = 0; i < this.itemObjList.Count; i++)
        {
            if (go.name == this.itemObjList[i].name)
            {
                this.itemObjList[i].GetComponent<LittleHelperItem>().SetSelectItem(true);
                continue;
            }
            this.itemObjList[i].GetComponent<LittleHelperItem>().SetSelectItem(false);
        }
    }
    /// <summary>
    /// 获取当前等级的百分比
    /// </summary>
    /// <returns></returns>
    private float UpLevel()
    {
        float present = 0;
        int presentLevel = 0;
        int maxLeve = 0;
        for (int i = 0; i < myHero.Count; i++)
        {
            presentLevel += myHero[i].level;
            maxLeve += CharacterRecorder.instance.level;
        }
        present = presentLevel * 1.0f / maxLeve;
        return present;
    }
    /// <summary>
    /// 获取当前装备强化的百分比
    /// </summary>
    /// <returns></returns>
    private float UpEquip()
    {
        float present = 0;
        int presentEquipLevel = 0;
        int maxEquipLevel = 0;
        for (int i = 0; i < myHero.Count; i++)
        {
            for (int j = 0; j < myHero[i].equipList.size; j++)
            {
                if (j < 4)
                {
                    presentEquipLevel += myHero[i].equipList[j].equipLevel;
                    maxEquipLevel += CharacterRecorder.instance.level;
                }
                else
                {
                    break;
                }
            }
        }
        present = presentEquipLevel * 1.0f / maxEquipLevel;
        return present;
    }
    /// <summary>
    /// 获取饰品强化百分比
    /// </summary>
    /// <returns></returns>
    private float UpAccessorieslevel()
    {
        float present = 0;
        int presentEquipLevel = 0;
        int maxEquipLevel = 0;
        for (int i = 0; i < myHero.Count; i++)
        {
            for (int j = 0; j < myHero[i].equipList.size; j++)
            {
                if (j < 4)
                {
                    continue;
                }
                presentEquipLevel += myHero[i].equipList[j].equipLevel;
                maxEquipLevel += CharacterRecorder.instance.level;
            }
        }
        present = presentEquipLevel * 1.0f / maxEquipLevel;
        return present;
    }
    /// <summary>
    /// 获取当前升品的百分比
    /// </summary>
    /// <returns></returns>
    private float UpClassNumber()
    {
        float present = 0;
        int presentClass = 0;
        int maxClass = 0;
        //Debug.LogError("++++++++++::  " + myHero[0].classNumber);
        //Debug.LogError("++++++++++::  " + TextTranslator.instance.GetRoleClassUpInfoByIDAndLevel(myHero[0].cardID, myHero[0].level).Color);
        for (int i = 0; i < myHero.Count; i++)
        {
            //HeroInfo heroInfo = TextTranslator.instance.GetHeroInfoByHeroID(myHero[i].cardID);
            presentClass += myHero[i].classNumber - 1;
            maxClass += TextTranslator.instance.GetRoleClassUpInfoByIDAndLevel(myHero[i].cardID, myHero[i].level).Color;
        }
        present = presentClass * 1.0f / maxClass;
        return present;
    }
    /// <summary>
    /// 获取角色培养的百分比
    /// </summary>
    /// <returns></returns>
    private float UpTrain()
    {
        //Debug.LogError("000000000000:  " + myTrainNumber);
        float present = 0;
        //Debug.LogError("1111:   " + myTrainNumber);
        present = myTrainNumber * 1.0f / myHero.Count;
        return present;
    }
    /// <summary>
    /// 获取角色培养的百分比
    /// </summary>
    /// <param name="_Hp">血量</param>
    /// <param name="_Atk">攻击</param>
    /// <param name="_Def">防御</param>
    /// <param name="_Cri">暴击</param>
    /// <param name="_UnCri">抗暴</param>
    public void SetTrainState(int _Hp, int _Atk, int _Def, float _Cri, float _UnCri)
    {
        //for (int i = 0; i < myHero.Count; i++)
        //{
        //Hero mHero = myHero[i];
        //if (heroCount == i)
        //{
        RoleWash rw = TextTranslator.instance.GetRoleWashByID(myHero[heroCount].cardID);
        float train1 = (float)_Hp / ((float)rw.HpMax * myHero[heroCount].level);
        float train2 = (float)_Atk / ((float)rw.AtkMax * myHero[heroCount].level);
        float train3 = (float)_Def / ((float)rw.DefMax * myHero[heroCount].level);
        //train += _Cri / rw.CriMax;
        //train += _UnCri / rw.UnCriMax;
        myTrainNumber += (train1 + train2 + train3) / 3;
        //}
        heroCount++;
        //}
        //myTrainCount3++;
        //if (myTrainCount3 == myHero.Count * 2 + seedMsgCount + 1)
        //{
        //    myTrainCount3 = 0;
        //    //协议发送完毕，执行继续代码
        //    InitDataThree();
        //}
        myTrainCount1++;
        if (myTrainCount1 == myHero.Count)
        {
            myTrainCount1 = 0;
            //协议发送完毕，执行继续代码
            InitDataOne();
        }
    }
    /// <summary>
    /// 获取角色技能等级的百分比
    /// </summary>
    /// <returns></returns>
    private float UpSkillLevel()
    {
        float present = 0;
        // Debug.LogError(myHero.Count);
        for (int i = 0; i < myHero.Count; i++)
        {
            RoleDestiny Old_rd = TextTranslator.instance.GetRoleDestingByIdAndLevel(myHero[i].cardID, CharacterRecorder.instance.level);
            if (Old_rd == null)
            {
                present += 0;
            }
            else
            {
                // Debug.LogError("++++:   " + Old_rd.Level);
                present += (myHero[i].skillLevel * 1.0f - 1) / Old_rd.Level;
            }
        }
        present /= myHero.Count;
        return present;
    }
    /// <summary>
    /// 获取装备精炼的百分比
    /// </summary>
    /// <returns></returns>
    private float UpEquipRefining()
    {
        float present = 0;
        int presentLevel = 0;
        int maxLevel = 0;
        for (int i = 0; i < myHero.Count; i++)
        {
            for (int j = 0; j < myHero[i].equipList.size; j++)
            {
                Hero.EquipInfo equip = myHero[i].equipList[j];
                if (j < 4)
                {
                    presentLevel += equip.equipClass;
                    if (TextTranslator.instance.GetEquipRefineCostByNeedID(CharacterRecorder.instance.level) != null)
                    {
                        maxLevel += TextTranslator.instance.GetEquipRefineCostByNeedID(CharacterRecorder.instance.level).RefineLevel;
                    }
                    else
                    {
                        //Debug.LogError("000");
                        maxLevel = 100;
                    }

                }
                else
                {
                    break;
                }
            }
        }
        present = presentLevel * 1.0f / maxLevel;
        return present;
    }
    /// <summary>
    /// 获取饰品精炼的百分比
    /// </summary>
    /// <returns></returns>
    private float UpAccessoriesRefining()
    {
        float present = 0;
        int presentLevel = 0;
        int maxLevel = 0;
        for (int i = 0; i < myHero.Count; i++)
        {
            for (int j = 0; j < myHero[i].equipList.size; j++)
            {
                Hero.EquipInfo equip = myHero[i].equipList[j];
                if (j < 4)
                {
                    continue;
                }
                presentLevel += equip.equipClass;
                maxLevel += TextTranslator.instance.GetEquipRefineCostByID(equip.equipLevel).RefineLevel;
            }
        }
        present = presentLevel * 1.0f / maxLevel;
        return present;
    }
    /// <summary>
    /// 获取饰品晋级的百分比
    /// </summary>
    /// <returns></returns>
    private float UpAccessoriesPromotion()
    {
        float present = 0;
        int presentLevel = 0;
        //int maxLevel = 0;
        for (int i = 0; i < myHero.Count; i++)
        {
            for (int j = 0; j < myHero[i].equipList.size; j++)
            {
                if (j < 4)
                {
                    continue;
                }
                presentLevel += myHero[i].equipList[i].equipColorNum;
                //Debug.LogError("......: " + myHero[i].equipList[i].equipColorNum);
            }
        }
        present = (presentLevel * 1.0f - 12) / (2 * myHero.Count * 5);
        return present;
    }
    /// <summary>
    /// 获取秘宝的百分比
    /// </summary>
    /// <returns></returns>
    private float UpSecret()
    {
        float present = 0;
        int presentLevel = 0;
        int maxLevel = 0;
        for (int i = 0; i < TextTranslator.instance.RareTreasureOpenDic.Count; i++)
        {
            RareTreasureOpen hihou = TextTranslator.instance.RareTreasureOpenDic[i + 1];
            if (hihou.state == 2)//已经装
            {
                presentLevel++;
            }
            maxLevel++;//总个数
        }
        if (maxLevel == 0)
        {
            maxLevel = 1;
        }
        present = presentLevel * 1.0f / maxLevel;
        return present;
    }
    /// <summary>
    /// 获取升星的百分比
    /// </summary>
    /// <returns></returns>
    private float UpStar()
    {
        float present = 0;
        int presentLevel = 0;
        int maxLevel = 0;
        int star = 0;
        List<int> list = new List<int>();
        for (int i = 0; i < myHero.Count; i++)
        {
            List<RoleBreach> rbList = TextTranslator.instance.GetRoleBreachByRoleID(myHero[i].cardID);
            for (int j = 0; j < rbList.Count; j++)
            {
                if (CharacterRecorder.instance.level >= rbList[j].levelCup)
                {
                    presentLevel++;
                }
            }
            list.Add(presentLevel);
            presentLevel = 0;
            star += myHero[i].rank - 1;
        }
        for (int i = 0; i < list.Count; i++)
        {
            maxLevel += list[i];
        }
        present = star * 1.0f / maxLevel;
        return present;
    }
    public void GetCarInfo(string[] dataSplit)
    {
        //List<SuperCar> carList = new List<SuperCar>();
        //for (int i = 0; i < dataSplit.Length - 2; i++)
        //{
        //    string[] caritem = dataSplit[i].Split('$');
        //    int id = 42001 + i;
        //    SuperCar CarItem = TextTranslator.instance.GetSuperCarByID(id);
        //    carList.Add(CarItem);
        //}
        //carDic.Add(carCount, carList);
        //carCount++;
        //myTrainCount++;
        //if (myTrainCount == myHero.Count * 2)
        //{
        //    myTrainCount = 0;
        //    //协议发送完毕，执行继续代码
        //    InitData();
        //}
    }
    /// <summary>
    /// 获取座驾的百分比
    /// </summary>
    /// <returns></returns>
    private float UpCar()
    {
        float present = 0;
        int presentLevel = 0;
        int maxLevel = 1;

        for (int i = 0; i < myHero.Count; i++)
        {
            for (int j = 0; j < myHero[i].SuperCarList.size; j++)
            {
                SuperCar CarItem1 = TextTranslator.instance.GetSuperCarByID(myHero[i].SuperCarList[j].SuperCarId1 + 42001);
                if (CharacterRecorder.instance.level >= CarItem1.NeedLevel)
                {
                    maxLevel++;
                }
                if (myHero[i].SuperCarList[j].SuperCarType1 == 1)
                {
                    presentLevel++;
                }
                SuperCar CarItem2 = TextTranslator.instance.GetSuperCarByID(myHero[i].SuperCarList[j].SuperCarId2 + 42000);
                if (CharacterRecorder.instance.level >= CarItem2.NeedLevel)
                {
                    maxLevel++;
                }
                if (myHero[i].SuperCarList[j].SuperCarType2 == 1)
                {
                    presentLevel++;
                }
                SuperCar CarItem3 = TextTranslator.instance.GetSuperCarByID(myHero[i].SuperCarList[j].SuperCarId3 + 42000);
                if (CharacterRecorder.instance.level >= CarItem3.NeedLevel)
                {
                    maxLevel++;
                }
                if (myHero[i].SuperCarList[j].SuperCarType3 == 1)
                {
                    presentLevel++;
                }
                SuperCar CarItem4 = TextTranslator.instance.GetSuperCarByID(myHero[i].SuperCarList[j].SuperCarId4 + 42000);
                if (CharacterRecorder.instance.level >= CarItem4.NeedLevel)
                {
                    maxLevel++;
                }
                if (myHero[i].SuperCarList[j].SuperCarType4 == 1)
                {
                    presentLevel++;
                }
                SuperCar CarItem5 = TextTranslator.instance.GetSuperCarByID(myHero[i].SuperCarList[j].SuperCarId5 + 42000);
                if (CharacterRecorder.instance.level >= CarItem5.NeedLevel)
                {
                    maxLevel++;
                }
                if (myHero[i].SuperCarList[j].SuperCarType5 == 1)
                {
                    presentLevel++;
                }
                SuperCar CarItem6 = TextTranslator.instance.GetSuperCarByID(myHero[i].SuperCarList[j].SuperCarId6 + 42000);
                if (CharacterRecorder.instance.level >= CarItem6.NeedLevel)
                {
                    maxLevel++;
                }
                if (myHero[i].SuperCarList[j].SuperCarType6 == 1)
                {
                    presentLevel++;
                }
            }
        }

        //foreach (KeyValuePair<int, List<SuperCar>> item in carDic)
        //{
        //    for (int i = 0; i < item.Value.Count; i++)
        //    {
        //        if (CharacterRecorder.instance.level >= item.Value[i].NeedLevel)
        //        {
        //            presentLevel++;
        //        }
        //        maxLevel++;
        //    }
        //}
        present = presentLevel * 1.0f / maxLevel;
        return present;
    }
    public void ReceiverMsg_3301(string[] dateSplit)
    {
        if (dateSplit[1] != "0" && dateSplit[2] != "0")
        {
            nuclearWeaponCount++;
        }
        myTrainCount3++;
        if (myTrainCount3 == myHero.Count + seedMsgCount + 1)
        {
            myTrainCount3 = 0;
            //协议发送完毕，执行继续代码
            InitDataThree();
        }
    }
    /// <summary>
    /// 获取核武器的百分比
    /// </summary>
    /// <returns></returns>
    private float UpNuclearWeapon()
    {
        float present = 0;
        present = nuclearWeaponCount * 1.0f / 6;
        return present;
    }

    public void GetIntelligenceMsg_1601(string[] dateSplit)
    {
        int count1 = 0;
        int count2 = 0;
        int count3 = 0;
        if (!string.IsNullOrEmpty(dateSplit[0]))//第一层
        {
            count1 = dateSplit[0].Split('$').Length;
        }
        if (!string.IsNullOrEmpty(dateSplit[1]))//第二层
        {
            count2 = dateSplit[1].Split('$').Length;
        }
        if (!string.IsNullOrEmpty(dateSplit[2]))//第三层
        {
            count3 = dateSplit[2].Split('$').Length;
        }
        int count4 = int.Parse(dateSplit[3]);
        List<int> list = TextTranslator.instance.GetTechListByUnlockLevel(CharacterRecorder.instance.level);
        list.Sort();
        lligence = (count1 + count2 + count3) * 1.0f / list[list.Count - 1];

        myTrainCount3++;
        if (myTrainCount3 == myHero.Count + seedMsgCount + 1)
        {
            myTrainCount3 = 0;
            //协议发送完毕，执行继续代码
            InitDataThree();
        }
    }
    /// <summary>
    /// 获取情报的百分比
    /// </summary>
    /// <returns></returns>
    private float UpIntelligence()
    {
        float present = 0;
        present = lligence;
        return present;
    }
    void SendLaboratoryMsg()
    {
        for (int i = 1; i <= 7; i++)
        {
            ReformLabItemData _targetLabItemData = TextTranslator.instance.GetOneLabsItemByRoleTypeAndPosNum(i, 1);
            if (CharacterRecorder.instance.level >= _targetLabItemData.NeedLevel)
            {
                seedMsgCount++;
                NetworkHandler.instance.SendProcess(string.Format("1801#{0};", i));
            }
        }
    }
    public void ReceiverLaboratoryMsg(int openNum)
    {
        openCarCount += openNum;
        myTrainCount3++;
        if (myTrainCount3 == myHero.Count + seedMsgCount + 1)
        {
            myTrainCount3 = 0;
            //协议发送完毕，执行继续代码
            InitDataThree();
        }
    }
    /// <summary>
    /// 获取实验室的百分比
    /// </summary>
    /// <returns></returns>
    private float UpLaboratory()
    {
        float present = 0;
        present = openCarCount * 1.0f / 35;
        return present;
    }
    /// <summary>
    /// 获取羁绊的百分比
    /// </summary>
    /// <returns></returns>
    private float UpFetters()
    {
        float present = 0;
        int presentLevel = 0;
        int maxLevel = 0;
        for (int i = 0; i < myHero.Count; i++)
        {
            BetterList<RoleFate> MyRoleFateList = TextTranslator.instance.GetMyRoleFateListByRoleID(myHero[i].cardID);
            if (CharacterRecorder.instance.ListRoleFateId.Contains(MyRoleFateList[i].RoleFateID))
            {
                presentLevel++;
            }
            maxLevel++;
        }
        present = presentLevel * 1.0f / maxLevel;
        return present;
    }
    /// <summary>
    /// 获取当前的等级UI
    /// </summary>
    /// <param name="level"></param>
    /// <returns></returns>
    private string GetLevelRole(float level)
    {
        string classNumber = "";
        if (level < 0.2f)
        {
            classNumber = "[fc5858]急需升级";
        }
        else if (level < 0.7f)
        {
            classNumber = "[6accff]勉勉强强";
        }
        else
        {
            classNumber = "[03ee1e]遥遥领先";
        }
        return classNumber;
    }
    private void InitDataOne()
    {
        BetterList<HelperItemData> mList1 = new BetterList<HelperItemData>();
        mList1.Add(new HelperItemData(10003, "英雄升级", string.Format("{0}", GetLevelRole(UpLevel())), UpLevel()));
        mList1.Add(new HelperItemData(30000, "英雄升品", string.Format("{0}", GetLevelRole(UpClassNumber())), UpClassNumber()));
        mList1.Add(new HelperItemData(31005, "装备强化", string.Format("{0}", GetLevelRole(UpEquip())), UpEquip()));
        mList1.Add(new HelperItemData(51030, "饰品强化", string.Format("{0}", GetLevelRole(UpAccessorieslevel())), UpAccessorieslevel()));
        mList1.Add(new HelperItemData(10500, "英雄培养", string.Format("{0}", GetLevelRole(UpTrain())), UpTrain()));
        HelperWindowDataDic.Add(1, mList1);
        SetTab(1);
        InitDataTwo();
    }
    private void InitDataTwo()
    {
        BetterList<HelperItemData> mList2 = new BetterList<HelperItemData>();
        mList2.Add(new HelperItemData(10101, "英雄技能", string.Format("{0}", GetLevelRole(UpSkillLevel())), UpSkillLevel()));
        mList2.Add(new HelperItemData(10012, "装备精炼", string.Format("{0}", GetLevelRole(UpEquipRefining())), UpEquipRefining()));
        mList2.Add(new HelperItemData(10103, "饰品精炼", string.Format("{0}", GetLevelRole(UpAccessoriesRefining())), UpAccessoriesRefining()));
        mList2.Add(new HelperItemData(51030, "饰品晋级", string.Format("{0}", GetLevelRole(UpAccessoriesPromotion())), UpAccessoriesPromotion()));
        mList2.Add(new HelperItemData(40103, "秘宝", string.Format("{0}", GetLevelRole(UpSecret())), UpSecret()));
        HelperWindowDataDic.Add(2, mList2);
    }
    void InitDataThree()
    {
        //Debug.LogError("==================");
        //InitDataOne();

        BetterList<HelperItemData> mList3 = new BetterList<HelperItemData>();
        mList3.Add(new HelperItemData(10102, "英雄升星", string.Format("{0}", GetLevelRole(UpStar())), UpStar()));
        mList3.Add(new HelperItemData(4028, "座驾", string.Format("{0}", GetLevelRole(UpCar())), UpCar()));
        mList3.Add(new HelperItemData(4032, "核武器", string.Format("{0}", GetLevelRole(UpNuclearWeapon())), UpNuclearWeapon()));
        mList3.Add(new HelperItemData(10104, "情报", string.Format("{0}", GetLevelRole(UpIntelligence())), UpIntelligence()));
        mList3.Add(new HelperItemData(50212, "改造实验室", string.Format("{0}", GetLevelRole(UpLaboratory())), UpLaboratory()));
        mList3.Add(new HelperItemData(60016, "羁绊", string.Format("{0}", GetLevelRole(CharacterRecorder.instance.ListRoleFateId.Count / (6f * CharacterRecorder.instance.ownedHeroList.size))), CharacterRecorder.instance.ListRoleFateId.Count / (6f * CharacterRecorder.instance.ownedHeroList.size)));
        HelperWindowDataDic.Add(3, mList3);
    }
    void CleraUIGride()
    {
        for (int i = 0; i < itemObjList.Count; i++)
        {
            DestroyImmediate(itemObjList[i]);
        }
        itemObjList.Clear();
        uiGride.GetComponent<UIGrid>().Reposition();
    }

}
