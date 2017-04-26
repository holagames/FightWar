using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipRefinePart: MonoBehaviour
{
    public UISlider _Slider;
    public UILabel LabelExp;

    public GameObject jingLianEffect;
    public GameObject jingLianEffect2;

    public GameObject StrongItem1;
    public GameObject StrongItem2;
    public GameObject StrongItem3;
    public GameObject StrongItem4;

    int CharacterRoleID;
    Hero mHero;
    HeroInfo mHeroInfo;
    private int equipPosition;
    [HideInInspector]
    public GameObject curClickStrongItemIcon;
    private Hero.EquipInfo mEquipInfo;
    private bool isOnPress;
    private float timer = 0;
    private float pressTimer = 0;
    void OnEnable()
    {
        //PictureCreater.instance.DestroyAllComponent();

    }
    // Use this for initialization
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.精炼);
        UIManager.instance.UpdateSystems(UIManager.Systems.精炼);

        UIEventListener.Get(StrongItem1).onPress = PressEXPitemListButton;
        UIEventListener.Get(StrongItem2).onPress = PressEXPitemListButton;
        UIEventListener.Get(StrongItem3).onPress = PressEXPitemListButton;
        UIEventListener.Get(StrongItem4).onPress = PressEXPitemListButton;
        if (UIEventListener.Get(StrongItem1).onClick == null)
        {
            UIEventListener.Get(StrongItem1).onClick = delegate(GameObject go)
            {
                if (CharacterRecorder.instance.lastGateID <= 10082)
                {
                    UIManager.instance.OpenPromptWindow(string.Format("{0}关开放装备精炼", 82), PromptWindow.PromptType.Hint, null, null);
                    return;
                }   
                if (TextTranslator.instance.GetItemCountByID(10011) == 0)
                {
                    OpenWayWindow(10011);
                    return;
                }                           

                NetworkHandler.instance.SendProcess("3010#" + CharacterRoleID + ";" + equipPosition + ";10011;1");
                curClickStrongItemIcon = StrongItem1.transform.FindChild("SpriteEXP").gameObject;
              //  CharacterRecorder.instance.IsNeedOpenFight = false;
              //  Invoke("DelayIsNeedOpenFightTrue", 3.0f);
            };
        }
        //if (UIEventListener.Get(StrongItem2).onClick == null)
        {
            UIEventListener.Get(StrongItem2).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.lastGateID <= 10082)
                {
                    UIManager.instance.OpenPromptWindow(string.Format("{0}关开放装备精炼", 82), PromptWindow.PromptType.Hint, null, null);
                    return;
                } 
                if (TextTranslator.instance.GetItemCountByID(10012) == 0)
                {
                    OpenWayWindow(10012);
                    return;
                }               
                NetworkHandler.instance.SendProcess("3010#" + CharacterRoleID + ";" + equipPosition + ";10012;1");
                curClickStrongItemIcon = StrongItem2.transform.FindChild("SpriteEXP").gameObject;
             //   CharacterRecorder.instance.IsNeedOpenFight = false;
             //   Invoke("DelayIsNeedOpenFightTrue", 3.0f);
                //if (CharacterRecorder.instance.GuideID[34] == 5)
                //{
                //    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                //}
            };
        }
        if (UIEventListener.Get(StrongItem3).onClick == null)
        {
            UIEventListener.Get(StrongItem3).onClick = delegate(GameObject go)
            {
                if (CharacterRecorder.instance.lastGateID <= 10082)
                {
                    UIManager.instance.OpenPromptWindow(string.Format("{0}关开放装备精炼", 82), PromptWindow.PromptType.Hint, null, null);
                    return;
                } 
                if (TextTranslator.instance.GetItemCountByID(10013) == 0)
                {
                    OpenWayWindow(10013);
                    return;
                }                
                NetworkHandler.instance.SendProcess("3010#" + CharacterRoleID + ";" + equipPosition + ";10013;1");
                curClickStrongItemIcon = StrongItem3.transform.FindChild("SpriteEXP").gameObject;
              //  CharacterRecorder.instance.IsNeedOpenFight = false;
              //  Invoke("DelayIsNeedOpenFightTrue", 3.0f);
            };
        }
        if (UIEventListener.Get(StrongItem4).onClick == null)
        {
            UIEventListener.Get(StrongItem4).onClick = delegate(GameObject go)
            {
                if (CharacterRecorder.instance.lastGateID <= 10082)
                {
                    UIManager.instance.OpenPromptWindow(string.Format("{0}关开放装备精炼", 82), PromptWindow.PromptType.Hint, null, null);
                    return;
                } 
                if (TextTranslator.instance.GetItemCountByID(10014) == 0)
                {
                    OpenWayWindow(10014);
                    return;
                }
                NetworkHandler.instance.SendProcess("3010#" + CharacterRoleID + ";" + equipPosition + ";10014;1");
                curClickStrongItemIcon = StrongItem4.transform.FindChild("SpriteEXP").gameObject;
              //  CharacterRecorder.instance.IsNeedOpenFight = false;
              //  Invoke("DelayIsNeedOpenFightTrue", 3.0f);
            };
        }
    }
    void OpenWayWindow(int code)
    {
        UIManager.instance.OpenSinglePanel("WayWindow", false);
        GameObject _WayWindow = GameObject.Find("WayWindow");
        //WayWindow.NeedItemCount = esq.materialItemList[i].itemCount;
        _WayWindow.GetComponent<WayWindow>().SetWayInfo(code);
        _WayWindow.layer = 11;
        foreach (Component c in _WayWindow.GetComponentsInChildren(typeof(Transform), true))
        {
            c.gameObject.layer = 11;
        }
    }
    void DelayIsNeedOpenFightTrue()
    {
        CharacterRecorder.instance.IsNeedOpenFight = true;
    }
    void OnDestroy()
    {
        UIEventListener.Get(StrongItem1).onPress -= PressEXPitemListButton;
        UIEventListener.Get(StrongItem2).onPress -= PressEXPitemListButton;
        UIEventListener.Get(StrongItem3).onPress -= PressEXPitemListButton;
        UIEventListener.Get(StrongItem4).onPress -= PressEXPitemListButton;
    }
    bool sendOnlyOnce = true;
   /* void OnMouseUp()
    {
        sendOnlyOnce = true;
    }*/
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            sendOnlyOnce = true;
        }
        if (isOnPress && CharacterRecorder.instance.lastGateID > 10082)
        {
            timer += Time.deltaTime;
            pressTimer += Time.deltaTime;
            int _senderNum = int.Parse(curClickStrongItemIcon.transform.parent.name);
            if (timer >= 0.2f && TextTranslator.instance.GetItemCountByID(_senderNum) > 0)
            {
                //int _senderNum = int.Parse(curClickStrongItemIcon.transform.parent.name);
                int _useCount = 1 + (int)pressTimer;
                //Debug.LogError("_useCount.........." + _useCount);
                NetworkHandler.instance.SendProcess(string.Format("3010#{0};{1};{2};{3};", CharacterRoleID, equipPosition, _senderNum, _useCount));
                timer = 0;
            }
            else if (sendOnlyOnce && timer >= 0.2f && TextTranslator.instance.GetItemCountByID(_senderNum) == 0)
            {
                int _useCount = 1 + (int)pressTimer;
                NetworkHandler.instance.SendProcess(string.Format("3010#{0};{1};{2};{3};", CharacterRoleID, equipPosition, _senderNum, _useCount));
                timer = 0;
                sendOnlyOnce = false;
            }
        }
    }
    void PressEXPitemListButton(GameObject go, bool isPress)
    {
        if (go != null)
        {
            if (isPress)
            {
                isOnPress = isPress;
                curClickStrongItemIcon = go.transform.FindChild("SpriteEXP").gameObject;
                CharacterRecorder.instance.IsNeedOpenFight = false;
            }
            else
            {
                isOnPress = isPress;
                timer = 0;
                pressTimer = 0;
                CharacterRecorder.instance.IsNeedOpenFight = true;
            }
        }
    }
    public void SetEquipLevel(int _CharacterRoleID, int _Index, int _EquipID, int _Class, int _Lv, int _Exp)
    {
        equipPosition = _Index;
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
    public void ScuccedJingLianSliderEffect()
    {
        //GameObject go = GameObject.Instantiate(Resources.Load("Prefab/Effect/JingYanTiao", typeof(GameObject)), _Slider.transform.position + new Vector3(0.7f, 0.01f, 0), Quaternion.identity) as GameObject;
        AudioEditer.instance.PlayOneShot("ui_levelup");
        GameObject obj = NGUITools.AddChild(_Slider.gameObject, jingLianEffect) as GameObject;
        obj.transform.localPosition = Vector3.zero;
    }
    IEnumerator ShowEffectInSort()
    {
        GameObject objIcon = NGUITools.AddChild(_Slider.transform.parent.gameObject, curClickStrongItemIcon) as GameObject;
        TweenPosition _TweenPosition = objIcon.AddComponent<TweenPosition>();
        objIcon.transform.position = curClickStrongItemIcon.transform.position;
        _TweenPosition.from = objIcon.transform.localPosition;
        _TweenPosition.to = _Slider.transform.localPosition;
        _TweenPosition.duration = 0.3f;
        _TweenPosition.ResetToBeginning();

        TweenScale _TweenScale = objIcon.AddComponent<TweenScale>();
        _TweenScale.from = objIcon.transform.localScale;
        _TweenScale.to = Vector3.zero;
        _TweenScale.delay = 0.2f;
        _TweenScale.duration = 0.2f;
        _TweenScale.ResetToBeginning();

        yield return new WaitForSeconds(0.3f);

        GameObject obj = NGUITools.AddChild(_Slider.gameObject, jingLianEffect2) as GameObject;

        obj.transform.localPosition = new Vector3(0, 0, -20f);

    }

    public void HalfScuccedJingLianSliderEffect()
    {
        StartCoroutine(ShowEffectInSort());
    }
    public void SetInfo(int _charactorID, int _EquipPositionID)//, int _itemid)
    {
        Debug.Log("SetInfo=="+_charactorID);
        Debug.Log(_EquipPositionID);
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

        if (_EquipPositionID < 5)
        {
            foreach (var h in CharacterRecorder.instance.ownedHeroList)
            {
                if (_charactorID == h.characterRoleID)
                {
                    Hero curHero = h;
                    _Slider.value = (float)curHero.equipList[equipPosition - 1].equipExp / (float)TextTranslator.instance.GetEquipRefineCostByID(curHero.equipList[equipPosition - 1].equipClass + 1).Exp;
                    LabelExp.text = curHero.equipList[equipPosition - 1].equipExp.ToString() + "/" + TextTranslator.instance.GetEquipRefineCostByID(curHero.equipList[equipPosition - 1].equipClass + 1).Exp.ToString();
                    break;
                }
            }

            TextTranslator.ItemInfo mitemInfo1 = TextTranslator.instance.GetItemByItemCode(10011);
            StrongItem1.transform.FindChild("Number").GetComponent<UILabel>().text = "+" + mitemInfo1.feedExp.ToString();
            StrongItem1.transform.FindChild("Count").GetComponent<UILabel>().text = TextTranslator.instance.GetItemCountByID(10011).ToString();
            TextTranslator.ItemInfo mitemInfo2 = TextTranslator.instance.GetItemByItemCode(10012);
            StrongItem2.transform.FindChild("Number").GetComponent<UILabel>().text = "+" + mitemInfo2.feedExp.ToString();
            StrongItem2.transform.FindChild("Count").GetComponent<UILabel>().text = TextTranslator.instance.GetItemCountByID(10012).ToString();
            TextTranslator.ItemInfo mitemInfo3 = TextTranslator.instance.GetItemByItemCode(10013);
            StrongItem3.transform.FindChild("Number").GetComponent<UILabel>().text = "+" + mitemInfo3.feedExp.ToString();
            StrongItem3.transform.FindChild("Count").GetComponent<UILabel>().text = TextTranslator.instance.GetItemCountByID(10013).ToString();
            TextTranslator.ItemInfo mitemInfo4 = TextTranslator.instance.GetItemByItemCode(10014);
            StrongItem4.transform.FindChild("Number").GetComponent<UILabel>().text = "+" + mitemInfo4.feedExp.ToString();
            StrongItem4.transform.FindChild("Count").GetComponent<UILabel>().text = TextTranslator.instance.GetItemCountByID(10014).ToString();
            //  Debug.LogError(mitemInfo1.feedExp + "    " + mitemInfo2.feedExp + "    " + mitemInfo3.feedExp + "     " + mitemInfo4.feedExp);
        }
        
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
            desStr += "生命:" + (es.HP * usefulClass).ToString() + "\n";
        }
        if (es.ADAttack > 0)
        {
            desStr += "物攻:" + (es.ADAttack * usefulClass).ToString() + "\n";
        }
        if (es.APAttack > 0)
        {
            desStr += "法攻:" + (es.APAttack * usefulClass).ToString() + "\n";
        }
        if (es.ADDenfance > 0)
        {
            desStr += "物防:" + (es.ADDenfance * usefulClass).ToString() + "\n";
        }
        if (es.APDenfance > 0)
        {
            desStr += "法防:" + (es.APDenfance * usefulClass).ToString() + "\n";
        }
        if (es.Hit > 0)
        {
            desStr += "命中:" + (es.Hit * usefulClass).ToString() + "\n";
        }
        if (es.NoHit > 0)
        {
            desStr += "闪避:" + (es.NoHit * usefulClass).ToString() + "\n";
        }
        if (es.Crit > 0)
        {
            desStr += "暴击:" + (es.Crit * usefulClass).ToString() + "\n";
        }
        if (es.NoCrit > 0)
        {
            desStr += "抗暴:" + (es.NoCrit * usefulClass).ToString() + "\n";
        }
        if (es.DmgBonus > 0)
        {
            desStr += "伤害加成:" + (es.DmgBonus * usefulClass).ToString() + "\n";
        }
        if (es.DmgReduction > 0)
        {
            desStr += "伤害减免:" + (es.DmgReduction * usefulClass).ToString() + "\n";
        }
        return desStr;
    }
   
}
