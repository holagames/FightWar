using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AwakeWindow: MonoBehaviour 
{
    public List<GameObject> ListEquip = new List<GameObject>();
    public UILabel EquipNameLabel;
    public UILabel desLabel;
    public GameObject desAwakeMaxObj;
    public GameObject topInfoObj;
    [SerializeField]
    private GameObject leftFrame;
    [SerializeField]
    private GameObject rightFrame;
    public GameObject SpriteUpJianTou;
    [SerializeField]
    private GameObject closeButton;
    [SerializeField]
    private GameObject awakeButton;
    [SerializeField]
    private GameObject uiGride;
    [SerializeField]
    private GameObject awakeItem;
    private List<GameObject> awakeItemList = new List<GameObject>();
    private int characterRoleID;
    public static int position;
    private Hero mCurHero;
    private HeroInfo heroInfo;
    public Hero.EquipInfo mCurEquipInfo;
    private int mCurEquipGrade;
    private int selectId;
    private int canAwakeBaoWuPos = 5;//可晋级的饰品，有红点的饰品位置
    [HideInInspector]
    public static Hero.EquipInfoBefore mEquipInfoBefore = new Hero.EquipInfoBefore();
	// Use this for initialization
	void Start () 
    {
        UIManager.instance.CountSystem(UIManager.Systems.饰品晋级);
        UIManager.instance.UpdateSystems(UIManager.Systems.饰品晋级);
      /*  //if (UIEventListener.Get(closeButton).onClick == null)
        {
            UIEventListener.Get(closeButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }*/
        InitEquipItemOpenState();
        
        //if (UIEventListener.Get(awakeButton).onClick == null)
        {
            UIEventListener.Get(awakeButton).onClick += delegate(GameObject go)
            {
              /*  RecordEquipInfoBefore(mCurEquipInfo);
                NetworkHandler.instance.SendProcess("3018#" + characterRoleID + ";" + position + ";" + selectId + ";");
                UIManager.instance.BackUI();*/
                if (CharacterRecorder.instance.GuideID[10] == 14)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
                if (awakeItemList.Count == 0)
                {
                    PictureCreater.instance.DestroyAllComponent();
                    UIManager.instance.OpenPanel("GrabItemWindow", true);
                }
                else
                {
                    TextTranslator.instance.heroBefore.SetHeroCardID(mCurHero.cardID);
                    TextTranslator.instance.heroBefore.SetHeroProperty(mCurHero.force, mCurHero.level, mCurHero.exp, mCurHero.maxExp, mCurHero.rank, mCurHero.rare, mCurHero.classNumber, mCurHero.skillLevel, mCurHero.skillNumber, mCurHero.HP, mCurHero.strength, mCurHero.physicalDefense, mCurHero.physicalCrit,
                        mCurHero.UNphysicalCrit, mCurHero.dodge, mCurHero.hit, mCurHero.moreDamige, mCurHero.avoidDamige, mCurHero.aspd, mCurHero.move, mCurHero.HPAdd, mCurHero.strengthAdd, mCurHero.physicalDefenseAdd, mCurHero.physicalCritAdd, mCurHero.UNphysicalCritAdd,
                        mCurHero.dodgeAdd, mCurHero.hitAdd, mCurHero.moreDamigeAdd, mCurHero.avoidDamigeAdd, 0);
                    RecordEquipInfoBefore(mCurEquipInfo);
                    NetworkHandler.instance.SendProcess("3018#" + characterRoleID + ";" + position + ";" + selectId + ";");
                    awakeButton.GetComponent<BoxCollider>().enabled = false;
                    Invoke("DelayColliderTrue", 1.0f);
                    CharacterRecorder.instance.IsNeedOpenFight = false;
                    Invoke("DelayIsNeedOpenFightTrue", 3.0f);
                }
            };
        }
	}
    void DelayColliderTrue()
    {
        awakeButton.GetComponent<BoxCollider>().enabled = true;
    }
    void DelayIsNeedOpenFightTrue()
    {
        CharacterRecorder.instance.IsNeedOpenFight = true;
    }
	// Update is called once per frame
	void Update () 
    {
	
	}
    public void SetAwakeWindow(int _charactorID,int _position, Hero.EquipInfo mCurEquipInfo)
    {
        SetCurSelectEquipItem(_position);
        this.mCurEquipInfo = mCurEquipInfo;
        characterRoleID = _charactorID;
        //Debug.LogError(_charactorID);
        
        foreach (var _hero in CharacterRecorder.instance.ownedHeroList)
        {
            if (_charactorID == _hero.characterRoleID)
            {
                mCurHero = _hero;
            }
         }
        heroInfo = TextTranslator.instance.GetHeroInfoByHeroID(mCurHero.cardID);
        position = _position;
        TextTranslator.ItemInfo _itemInfo = TextTranslator.instance.GetItemByItemCode(mCurEquipInfo.equipCode);
        mCurEquipGrade = _itemInfo.itemGrade;
        SetTopFrameAndIcon(mCurEquipInfo.equipCode, _itemInfo.itemGrade, leftFrame.transform.FindChild("Icon").GetComponent<UISprite>(), leftFrame.GetComponent<UISprite>(), leftFrame.transform.FindChild("Label").GetComponent<UILabel>());
        if (EquipNameLabel != null)
        {
            EquipNameLabel.text = GetItemName(mCurHero.equipList[_position - 1].equipCode, mCurHero.equipList[_position - 1].equipClass, mCurHero.equipList[_position - 1].equipColorNum);
        }

        if (_position < 5)
        {
            desLabel.text = "80级开放装备晋级";
            awakeButton.SetActive(false);
            return;
        }
        if (_itemInfo.itemGrade == 6)
        {
            desAwakeMaxObj.SetActive(true);
            topInfoObj.SetActive(false);
        }
        else
        {
            desAwakeMaxObj.SetActive(false);
            topInfoObj.SetActive(true);
        }
        GetAllBaoWuInBag();
    }
    void SetAwakeWindow(int _charactorID, int _position)
    {
        SetCurSelectEquipItem(_position);
        
        characterRoleID = _charactorID;
        foreach (var _hero in CharacterRecorder.instance.ownedHeroList)
        {
            if (_charactorID == _hero.characterRoleID)
            {
                mCurHero = _hero;
            }
        }
        this.mCurEquipInfo = mCurHero.equipList[_position - 1];
        heroInfo = TextTranslator.instance.GetHeroInfoByHeroID(mCurHero.cardID);
        position = _position;
        TextTranslator.ItemInfo _itemInfo = TextTranslator.instance.GetItemByItemCode(mCurEquipInfo.equipCode);
        mCurEquipGrade = _itemInfo.itemGrade;
        SetTopFrameAndIcon(mCurEquipInfo.equipCode, _itemInfo.itemGrade, leftFrame.transform.FindChild("Icon").GetComponent<UISprite>(), leftFrame.GetComponent<UISprite>(), leftFrame.transform.FindChild("Label").GetComponent<UILabel>());
        if (EquipNameLabel != null)
        {
            EquipNameLabel.text = GetItemName(mCurHero.equipList[_position - 1].equipCode, mCurHero.equipList[_position - 1].equipClass, mCurHero.equipList[_position - 1].equipColorNum);
        }

        if (_position < 5)
        {
            desLabel.text = "80级开放装备晋级";
            awakeButton.SetActive(false);
            return;
        }
        if (_itemInfo.itemGrade == 6)
        {
            desAwakeMaxObj.SetActive(true);
            topInfoObj.SetActive(false);
        }
        else
        {
            desAwakeMaxObj.SetActive(false);
            topInfoObj.SetActive(true);
        }
        GetAllBaoWuInBag();
    }
    public void SetEuipList(int charaterId,BetterList<Hero.EquipInfo> _EquipList)
    {
        
        SetAwakeWindow(charaterId, position);
        foreach (var _OneEquipInfo in _EquipList)
        {
            if (_OneEquipInfo.equipPosition >= 5)
            {
                SetEquip(_OneEquipInfo);
            }
        }
    }
    void InitEquipItemOpenState()
    {
        for (int i = 0; i < ListEquip.Count; i++)
        {
            if (i >= 4)
            {
                UIEventListener.Get(ListEquip[i]).onClick = ClickListEquip;
                if (ListEquip[i].transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName != "add")
                {
                    switch (ListEquip[i].name)
                    {
                        case "Equip5":
                        case "Equip6":
                            GameObject _Lock2 = ListEquip[i].transform.FindChild("Lock").gameObject;
                            if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.duobao).Level)
                            {
                                ListEquip[i].GetComponent<UIToggle>().enabled = false;
                                _Lock2.SetActive(true);
                            }
                            else
                            {
                                _Lock2.SetActive(false);
                            }
                            break;
                    }
                }
                else
                {
                    GameObject _Lock = ListEquip[i].transform.FindChild("Lock").gameObject;
                    _Lock.SetActive(false);
                }
            }
        }
    }
    void ClickListEquip(GameObject go)
    {
        if (go != null)
        {
            if (go.transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName == "add")
            {
                return;
            }
            switch (go.name)
            {
                case "Equip5":
                    if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.duobao).Level)
                    {
                        UIManager.instance.OpenPromptWindow(string.Format("{0}关开放饰品强化精炼晋级", (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.duobao).Level-10000)), PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        position = 5; //equipType = 1;
                    }
                    break;
                case "Equip6":
                    if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.duobao).Level)
                    {
                        UIManager.instance.OpenPromptWindow(string.Format("{0}关开放饰品强化精炼晋级", (TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.duobao).Level - 10000)), PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        position = 6; //equipType = 1;
                    }
                    break;
            }
            SetEquipInfo();
        }
    }
    public void SetEquipInfo()
    {
        

        SetAwakeWindow(characterRoleID, position, mCurHero.equipList[position - 1]);
    }
    private void SetCurSelectEquipItem(int _pos)
    {
        for (int i = 0; i < ListEquip.Count; i++)
        {
            if(i + 1 >= 5)
            {
                UIToggle _myUIToggle = ListEquip[i].GetComponent<UIToggle>();
                if (i + 1 != _pos)
                {
                    _myUIToggle.startsActive = false;
                    _myUIToggle.activeSprite.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0);
                }
                else
                {
                    _myUIToggle.startsActive = true;
                    _myUIToggle.activeSprite.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
                }
            }
        }
    }
    //设置装备
    private void SetEquip(Hero.EquipInfo _OneEquipInfo)
    {
        GameObject go = ListEquip[_OneEquipInfo.equipPosition - 1];
        //go.GetComponent<EquipItemInStreng>().SetEquipItem(mHero, _OneEquipInfo);
        if (_OneEquipInfo.equipCode != 0)
        {
            string frameName = "";
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_OneEquipInfo.equipCode);
            frameName = string.Format("Grade{0}", mItemInfo.itemGrade);
            go.GetComponent<UISprite>().spriteName = frameName;
            go.transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName = _OneEquipInfo.equipCode.ToString();
            go.transform.Find("Label").gameObject.GetComponent<UILabel>().text = _OneEquipInfo.equipLevel.ToString();
            if (_OneEquipInfo.equipPosition >= 5)
            {
                GameObject _RedPoint = go.transform.FindChild("RedPoint").gameObject;
                _RedPoint.SetActive(IsCanAwake(_OneEquipInfo.equipPosition));
            }
            
        }
        else
        {
            go.GetComponent<UISprite>().spriteName = "Grade1";
            go.transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName = "";
            go.transform.Find("Label").gameObject.GetComponent<UILabel>().text = "";
        }
    }
    bool IsCanAwake(int equipPos)
    {
        if (CharacterRecorder.instance.lastGateID < TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.duobao).Level)
        {
            return false;
        }
        Hero.EquipInfo mEquipInfo = mCurHero.equipList[equipPos - 1];
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
            heroInfo = TextTranslator.instance.GetHeroInfoByHeroID(mCurHero.cardID);
            List<Item> _mList = new List<Item>();
            _mList = TextTranslator.instance.GetAllBaoWuInBag();
            for (int i = _mList.Count - 1; i >= 0; i--)
            {
                EquipStrong _myEquipStrong = TextTranslator.instance.GetEquipStrongByID(_mList[i].itemCode);
                //Debug.Log(_myEquipStrong.Part);
                if (_myEquipStrong == null || _mList[i].itemGrade <= _itemInfo.itemGrade || _myEquipStrong.Race != heroInfo.heroRace || _myEquipStrong.Part != mEquipInfo.equipPosition)
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
            if (usefulList.Count > 0)
            {
                canAwakeBaoWuPos = equipPos;
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
    void GetAllBaoWuInBag()
    {
        ClearUIGride();
        List<Item> _mList = new List<Item>();
        _mList = TextTranslator.instance.GetAllBaoWuInBag();
        //Debug.LogError(mCurHero.cardID);
        //Debug.LogError(_mList.Count + "..当前grade.." + mCurEquipGrade + "..heroCarrerType.." + heroInfo.heroCarrerType);
        for (int i = _mList.Count -1; i >= 0; i--)
        {
            EquipStrong _myEquipStrong = TextTranslator.instance.GetEquipStrongByID(_mList[i].itemCode);
            //Debug.Log(_myEquipStrong.Part);
            if (_myEquipStrong == null || _mList[i].itemGrade <= mCurEquipGrade || _myEquipStrong.Race != heroInfo.heroRace || _myEquipStrong.Part != position)
            {
                _mList.Remove(_mList[i]);
            }
        }
        List<Item> usefulList = new List<Item>();
        //Dictionary<int,Item> _usefulDic = new Dictionary<int, Item>();
        for (int i = _mList.Count - 1; i >= 0; i--)
        {
            if (!usefulList.Contains(_mList[i]) && _mList[i].itemCount > 0)
            {
                usefulList.Add(_mList[i]);
            }
        }
        //Debug.LogError(usefulList.Count);
        awakeButton.SetActive(true);
        if (usefulList.Count == 0)
        {
            awakeButton.transform.FindChild("Label").GetComponent<UILabel>().text = "去夺宝";
            desLabel.text = "没有可更换的饰品";
            SpriteUpJianTou.SetActive(false);
            return;
        }
        awakeButton.transform.FindChild("Label").GetComponent<UILabel>().text = "晋 级";
        desLabel.text = "选择你要晋级后的装饰";
        selectId = usefulList[0].itemCode;
        SpriteUpJianTou.SetActive(true);

        SetTopFrameAndIcon(usefulList[0].itemCode, usefulList[0].itemGrade, rightFrame.transform.FindChild("Icon").GetComponent<UISprite>(), rightFrame.GetComponent<UISprite>(), rightFrame.transform.FindChild("Label").GetComponent<UILabel>());
        for (int i = 0; i < usefulList.Count; i++)
        {
            if (uiGride.transform.FindChild(usefulList[i].itemCode.ToString()) != null)
            {
                continue;
            }
            EquipStrong _myEquipStrong = TextTranslator.instance.GetEquipStrongByID(usefulList[i].itemCode);
            //Debug.Log("过滤后...Item...Part..." + _myEquipStrong.Part);
            GameObject obj = NGUITools.AddChild(uiGride, awakeItem) as GameObject;
            awakeItemList.Add(obj);
            obj.SetActive(true);
            obj.name = usefulList[i].itemCode.ToString();
            if(i == 0)
            {
                obj.transform.FindChild("SelectEffect").gameObject.SetActive(true);
            }
            SetFrameAndIcon(usefulList[i].itemCode, usefulList[i].itemGrade, obj.transform.FindChild("Icon").GetComponent<UISprite>(), obj.GetComponent<UISprite>());
            int itemClassLevel = usefulList[i].itemGrade;
            if (UIEventListener.Get(obj).onClick == null)
            {
                UIEventListener.Get(obj).onClick = delegate(GameObject go)
                {
                    if (int.Parse(obj.name) != selectId)
                    {
                        //Debug.Log(int.Parse(obj.name));
                        selectId = int.Parse(obj.name);
                        //Debug.Log(selectId);
                        for (int j = 0; j < awakeItemList.Count;j++ )
                        {
                            awakeItemList[j].transform.FindChild("SelectEffect").gameObject.SetActive(false);
                        }
                        obj.transform.FindChild("SelectEffect").gameObject.SetActive(true);
                        // SetFrameAndIcon(_usefulDic[i].itemCode, _usefulDic[i].itemClassLevel, rightFrame.transform.FindChild("Icon").GetComponent<UISprite>(), rightFrame.GetComponent<UISprite>());
                        SetTopFrameAndIcon(selectId, itemClassLevel, rightFrame.transform.FindChild("Icon").GetComponent<UISprite>(), rightFrame.GetComponent<UISprite>(), rightFrame.transform.FindChild("Label").GetComponent<UILabel>());
                    }
                };
            }
        }
        uiGride.GetComponent<UIGrid>().Reposition();
    }
    void ClearUIGride()
    {
        awakeItemList.Clear();
        for (int i = uiGride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGride.transform.GetChild(i).gameObject);
        }
        SetTopFrameAndIcon(0, 1, rightFrame.transform.FindChild("Icon").GetComponent<UISprite>(), rightFrame.GetComponent<UISprite>(), rightFrame.transform.FindChild("Label").GetComponent<UILabel>());
    }
    void ResetRightInfo()
    {
        
    }
    void SetTopFrameAndIcon(int _ItemCode,int _ItemGrade,UISprite icon,UISprite frame,UILabel _numAddLabel)
    {
        if (_ItemCode == 0)
        {
            icon.spriteName = "";//"add";
            frame.spriteName = "Grade1";
            _numAddLabel.text = "";
            return;
        }
        icon.spriteName = _ItemCode.ToString();
        frame.spriteName = "Grade" + _ItemGrade.ToString();
        _numAddLabel.text = GetStrenghStr(_ItemCode);//GetJingLianStr(_ItemCode);
    }
    void SetFrameAndIcon(int _ItemCode, int _ItemGrade, UISprite icon, UISprite frame)
    {
        icon.spriteName = _ItemCode.ToString();
        frame.spriteName = "Grade" + _ItemGrade.ToString();
    }
    string GetStrenghStr(int equipCode)//, bool useNext)
    {
        string desStr = "";
        EquipStrong es = TextTranslator.instance.GetEquipStrongByID(equipCode);
        int usefulClass = this.mCurEquipInfo.equipLevel;
        /* int usefulClass = 0;
         if (!useNext)
         {
             usefulClass = mEquipInfo.equipLevel;
         }
         else
         {
             usefulClass = mEquipInfo.equipLevel + 1;
         }*/
        desStr = "等级:" + usefulClass.ToString() + "\n";
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
        }
        if (es.Def > 0)
        {
            string usefulNumStr = "";
            if (es.Def * usefulClass < 1 && es.Def * usefulClass > 0)
            {
                usefulNumStr = ChangeDecimalToPercent(es.Def * usefulClass);
            }
            else
            {
                usefulNumStr = (es.Def * usefulClass).ToString();
            }
            desStr += "防御:" + usefulNumStr + "\n";
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
    string GetJingLianStr(int itemCode)//, bool useNext)
    {
        string desStr = "";
        EquipDetailInfo es = TextTranslator.instance.GetEquipInfoByID(itemCode);
        int usefulClass = this.mCurEquipInfo.equipClass;
      /*   if (!useNext)
         {
             usefulClass = mEquipInfo.equipClass;
         }
         else
         {
             usefulClass = mEquipInfo.equipClass + 1;
         }
         desStr = "精炼:" + usefulClass.ToString() + "\n";*/
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
       // desStr.Remove(desStr.Length - 2);
       // Debug.Log(desStr);
        return desStr;
    }
    string ChangeDecimalToPercent(float _Decimal)
    {
        return string.Format("{0}%", Math.Round(_Decimal * 100, 1) );// _Decimal * 100
    }
    #region 装备名字
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
    #endregion
}
