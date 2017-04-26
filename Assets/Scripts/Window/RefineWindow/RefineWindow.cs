using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RefineWindow : MonoBehaviour
{
    public GameObject jingLianEffect;
    public GameObject jingLianEffect2;

    //public List<GameObject> ListEquip = new List<GameObject>();

    int CharacterRoleID;
    int equipPosition;
    Hero mHero;
    HeroInfo mHeroInfo;

    public UILabel LabelRefineName1;
    public UILabel LabelRefineName2;
    public UILabel LabelRefineCount1;
    public UILabel LabelRefineCount2;

    public UISprite EquipSprite1;
    public UISprite EquipSprite2;
    public UISprite EquipGradeSprite1;
    public UISprite EquipGradeSprite2;
    [SerializeField]
    private GameObject awakeButton;
    [SerializeField]
    private GameObject TakeOnesButton;
    public GameObject GotoGrabButton;

    private Hero.EquipInfo mEquipInfo;
    private TextTranslator.ItemInfo mItemInfo;
    [HideInInspector]
    public static Hero.EquipInfoBefore mEquipInfoBefore = new Hero.EquipInfoBefore();

    private bool canAwakeState = false;

    void OnEnable()
    {
        //PictureCreater.instance.DestroyAllComponent();
        
    }
    void Start()
    {
        //AddDaoJuEffect(EquipRightEquipIcon, BaoWuRightEquipIcon);
        if (UIEventListener.Get(TakeOnesButton).onClick == null)
        {
            UIEventListener.Get(TakeOnesButton).onClick = delegate(GameObject go)
            {
              /*  if (this.mItemInfo.itemGrade == 2)
                {
                    PictureCreater.instance.DestroyAllComponent();
                    UIManager.instance.OpenPanel("GrabItemWindow", true);
                }
                else*/
                {
                    TextTranslator.instance.heroBefore.SetHeroCardID(mHero.cardID);
                    TextTranslator.instance.heroBefore.SetHeroProperty(mHero.force, mHero.level, mHero.exp, mHero.maxExp, mHero.rank, mHero.rare, mHero.classNumber, mHero.skillLevel, mHero.skillNumber, mHero.HP, mHero.strength, mHero.physicalDefense, mHero.physicalCrit,
                        mHero.UNphysicalCrit, mHero.dodge, mHero.hit, mHero.moreDamige, mHero.avoidDamige, mHero.aspd, mHero.move, mHero.HPAdd, mHero.strengthAdd, mHero.physicalDefenseAdd, mHero.physicalCritAdd, mHero.UNphysicalCritAdd,
                        mHero.dodgeAdd, mHero.hitAdd, mHero.moreDamigeAdd, mHero.avoidDamigeAdd, 0);
                    RecordEquipInfoBefore(mEquipInfo);
                    NetworkHandler.instance.SendProcess("3017#" + CharacterRoleID + ";" + equipPosition + ";");
                    CharacterRecorder.instance.IsNeedOpenFight = false;
                    Invoke("DelayIsNeedOpenFightTrue", 3.0f);
                }
                if (CharacterRecorder.instance.GuideID[34] == 5)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
                
            };
        }
        //if (UIEventListener.Get(awakeButton).onClick == null)
        {
            UIEventListener.Get(awakeButton).onClick += delegate(GameObject go)
            {
                if (canAwakeState)
                {
                    UIManager.instance.OpenPanel("AwakeWindow", false);
                    GameObject _AwakeWindow = GameObject.Find("AwakeWindow");
                    _AwakeWindow.GetComponent<AwakeWindow>().SetAwakeWindow(CharacterRoleID, equipPosition, mEquipInfo);
                    _AwakeWindow.layer = 11;
                    foreach (Component c in _AwakeWindow.GetComponentsInChildren(typeof(Transform), true))
                    {
                        c.gameObject.layer = 11;
                    }
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("没有可更换的饰品", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        if (UIEventListener.Get(GotoGrabButton).onClick == null)
        {
            UIEventListener.Get(GotoGrabButton).onClick = delegate(GameObject go)
            {
                //NetworkHandler.instance.SendProcess("3018#" + CharacterRoleID + ";" + equipPosition + ";51070;");
                PictureCreater.instance.DestroyAllComponent();
                UIManager.instance.OpenPanel("GrabItemWindow", true);
            };
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
    }
    void AddDaoJuEffect(UISprite icon1Obj,UISprite icon2Obj)
    {
        //Debug.LogError("加道具特效");
        GameObject _ToolEquipEffect = GameObject.Instantiate(Resources.Load("Prefab/Effect/WF_ui_DaoJu", typeof(GameObject)), icon1Obj.transform.position, Quaternion.identity) as GameObject;
        _ToolEquipEffect.transform.parent = icon1Obj.transform.parent.transform;

        GameObject _ToolBaoWuEffect = GameObject.Instantiate(Resources.Load("Prefab/Effect/WF_ui_DaoJu", typeof(GameObject)), icon2Obj.transform.position, Quaternion.identity) as GameObject;
        _ToolBaoWuEffect.transform.parent = icon2Obj.transform.parent.transform;

        TweenPosition _tp1 = icon1Obj.GetComponent<TweenPosition>();
        if (_tp1 == null)
        {
            _tp1 = icon1Obj.gameObject.AddComponent<TweenPosition>();
            
        }
        else { }
        _tp1.from = new Vector3(0,40f,0);
        _tp1.to = new Vector3(0,30f, 0);
        _tp1.style = UITweener.Style.PingPong;

        TweenPosition _tp2 = icon2Obj.GetComponent<TweenPosition>();
        if (_tp2 == null)
        {
            _tp2 = icon2Obj.gameObject.AddComponent<TweenPosition>();
        }
        else 
        {
        }
        _tp2.from = new Vector3(0, 40f, 0);
        _tp2.to = new Vector3(0, 30f, 0);
        _tp2.style = UITweener.Style.PingPong;
    }
    
    // Use this for initialization
    public void StartPart(int _charactorID, int _EquipPositionID,Hero.EquipInfo _mEquipInfo)
    {
        mEquipInfo = _mEquipInfo;
        this.mItemInfo = TextTranslator.instance.GetItemByItemCode(_mEquipInfo.equipCode);
        UILabel _TakeOnesButtonLabel = TakeOnesButton.transform.FindChild("Label").GetComponent<UILabel>();
      /*  if (this.mItemInfo.itemGrade == 2)
        {
            _TakeOnesButtonLabel.text = "去夺宝";
            TakeOnesButton.transform.FindChild("MoneyRefineNeed").gameObject.SetActive(false);
        }
        else */
        {
            _TakeOnesButtonLabel.text = "精 炼";
            TakeOnesButton.transform.FindChild("MoneyRefineNeed").gameObject.SetActive(true);
        }
        SetInfo(_charactorID, _EquipPositionID);
        SetRedPointStateOfBaoWu();
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

    public void SetInfo(int _charactorID, int _EquipPositionID)//, int _itemid)
    {
        Debug.Log(_charactorID);
        Debug.Log(_EquipPositionID);
        //Debug.Log(_itemid);
        CharacterRoleID = _charactorID;
        if (_EquipPositionID > 0)
        {
            equipPosition = _EquipPositionID;
        }
        else
        {
            equipPosition = 1;
        }

        //SetCurSelectEquipItem(_EquipPositionID);

        //SetEquipInfo();
        
        if (_EquipPositionID < 5)
        {
        }
        else
        {
            //EquipRight.SetActive(false);
            //BaoWuRight.SetActive(true);
            foreach (var h in CharacterRecorder.instance.ownedHeroList)
            {
                if (_charactorID == h.characterRoleID)
                {
                    Hero curHero = h;
                    EquipRefineCost erc = TextTranslator.instance.GetEquipRefineCostByID(curHero.equipList[equipPosition - 1].equipClass + 1);
                   // TakeOnesButton.transform.FindChild("MoneyRefineNeed").GetComponent<UILabel>().text = erc.DecoGold.ToString();
                    UILabel _moneyLabel = TakeOnesButton.transform.FindChild("MoneyRefineNeed").GetComponent<UILabel>();
                    if (CharacterRecorder.instance.gold >= erc.DecoGold)
                    {
                        _moneyLabel.text = erc.DecoGold.ToString();
                    }
                    else
                    {
                        _moneyLabel.color = Color.white;
                        _moneyLabel.text = "[ff0000]" + erc.DecoGold.ToString();
                    }
                    int StoneCount = TextTranslator.instance.GetItemCountByID(10103);
                    LabelRefineCount1.text = (StoneCount >= erc.DecoStoneNum ? StoneCount.ToString() : "[ff0000]" + StoneCount.ToString() + "[-]") + "/" + erc.DecoStoneNum.ToString();
                    EquipSprite1.spriteName = "10103";
                    EquipGradeSprite1.spriteName = "Grade4";
                    //if (UIEventListener.Get(EquipGradeSprite1.gameObject).onClick == null)
                    {
                        UIEventListener.Get(EquipGradeSprite1.gameObject).onClick = delegate(GameObject go)
                        {
                            ClikItemButton(10103, erc.DecoStoneNum);
                        };
                    }
                    if (erc.PieceNum > 0)
                    {
                        EquipGradeSprite2.gameObject.SetActive(true);

                        int BWCount = TextTranslator.instance.GetItemCountByID(curHero.equipList[equipPosition - 1].equipCode);
                        //Debug.LogError("code.." + curHero.equipList[equipPosition - 1].equipCode + "..BWCount.." + BWCount);
                        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(curHero.equipList[equipPosition - 1].equipCode);
                        LabelRefineCount2.text = (BWCount >= erc.PieceNum ? BWCount.ToString() : "[ff0000]" + BWCount.ToString() + "[-]") + "/" + erc.PieceNum.ToString();
                        //Debug.LogError(_ItemInfo);
                        LabelRefineName2.text = _ItemInfo.itemName;
                        EquipSprite2.spriteName = _ItemInfo.itemCode.ToString();
                        EquipGradeSprite2.spriteName = "Grade" + _ItemInfo.itemGrade;//curHero.equipList[equipPosition - 1].equipClass.ToString();

                        //if (UIEventListener.Get(EquipGradeSprite2.gameObject).onClick == null)
                        {
                            UIEventListener.Get(EquipGradeSprite2.gameObject).onClick = delegate(GameObject go)
                            {
                                ClikItemButton(_ItemInfo.itemCode, erc.PieceNum);
                            };
                        }
                    }
                    else
                    {
                        EquipGradeSprite2.gameObject.SetActive(false);
                        LabelRefineName2.text = "";
                        LabelRefineCount2.text = "";
                    }
                    break;
                }
            }

        }
    }
    void ClikItemButton(int code,int needCount)
    {
        //UIManager.instance.OpenPanel("WayWindow", false);
        UIManager.instance.OpenSinglePanel("WayWindow", false);
        GameObject _WayWindow = GameObject.Find("WayWindow");
        WayWindow.NeedItemCount = needCount;
        _WayWindow.GetComponent<WayWindow>().SetWayInfo(code);
        _WayWindow.layer = 11;
        foreach (Component c in _WayWindow.GetComponentsInChildren(typeof(Transform), true))
        {
            c.gameObject.layer = 11;
        }
    }
    #region 饰品觉醒小红点
    void SetRedPointStateOfBaoWu()
    {
        canAwakeState = IsCanAwake();
        awakeButton.transform.FindChild("RedPoint").gameObject.SetActive(canAwakeState);
    }

    bool IsCanAwake()
    {
        TextTranslator.ItemInfo _itemInfo = TextTranslator.instance.GetItemByItemCode(mEquipInfo.equipCode);
        if (_itemInfo != null)
        {
            foreach (var _hero in CharacterRecorder.instance.ownedHeroList)
            {
                if (CharacterRoleID == _hero.characterRoleID)
                {
                    mHero = _hero;
                    break;
                }
            }
            mHeroInfo = TextTranslator.instance.GetHeroInfoByHeroID(mHero.cardID);
            List<Item> _mList = new List<Item>();
            _mList = TextTranslator.instance.GetAllBaoWuInBag();
            //Debug.Log(equipPosition);
            for (int i = _mList.Count - 1; i >= 0; i--)
            {
                EquipStrong _myEquipStrong = TextTranslator.instance.GetEquipStrongByID(_mList[i].itemCode);
                //Debug.Log("..." + _myEquipStrong.Part);
                //Debug.Log(_mList[i].itemGrade);
                //Debug.Log(_itemInBag.itemGrade);
                //Debug.Log("#############" + mHeroInfo.heroRace + "Race.." + _myEquipStrong.Race);
                //Debug.Log(mHeroInfo.heroCarrerType);
                if (_myEquipStrong == null || _mList[i].itemGrade <= _itemInfo.itemGrade || _myEquipStrong.Race != mHeroInfo.heroRace || _myEquipStrong.Part != equipPosition)
                {
                    _mList.Remove(_mList[i]);
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
}
