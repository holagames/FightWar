using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using CodeStage.AntiCheat.ObscuredTypes;

public class HeroTrainPart : MonoBehaviour
{
    //[HideInInspector]
    public int trainTimes = 1;

    //  public List<GameObject> selectToggle = new List<GameObject>();

    [SerializeField]
    private List<UILabel> trainCostLabelList = new List<UILabel>();
    private List<int> trainOnceCostList = new List<int>();

    public UILabel Name;
    public UISlider HpSlider;
    public UILabel HpSliderLabel;
    public UISlider AtkSlider;
    public UILabel AtkSliderLabel;
    public UISlider DefSlider;
    public UILabel DefSliderLabel;
    public UISlider CriSlider;
    public UILabel CriSliderLabel;
    public UISlider UnCriSlider;
    public UILabel UnCriSliderLabel;

    public GameObject HP;
    public GameObject ATK;
    public GameObject DEF;
    public GameObject CRI;
    public GameObject UNCRI;

    public UISprite RarityIcon;
    public UILabel medicineLabel;

    public GameObject TrainTimesButton;
    public GameObject TrainButton;
    public GameObject ReplaceButton;
    [SerializeField]
    private GameObject ATKtype;
    [SerializeField]
    private GameObject DEFtype;

    [HideInInspector]
    public bool isClean = false;
    int UpType = 1;
    public Hero mHero;
    public bool IsHeroTrained = false;
    HeroInfo mHeroInfo;

    void OnEnable()
    {
        mHero = null;
        IsHeroTrained = false;


    }
    // Use this for initialization
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.培养);
        UIManager.instance.UpdateSystems(UIManager.Systems.培养);
        SetTrainOnceCostList();
        if (PlayerPrefs.GetInt("TrainTimes", 0) == 0)
        {
            ResetTrainTimes(1);
        }
        else
        {
            if (CharacterRecorder.instance.Vip<4)
            {
                ResetTrainTimes(1);
            }
            else if (CharacterRecorder.instance.Vip<6)
            {
                if (int.Parse(PlayerPrefs.GetInt("TrainTimes").ToString()) == 3 || int.Parse(PlayerPrefs.GetInt("TrainTimes").ToString()) == 1)
                {
                    ResetTrainTimes(1);
                }
                else if (int.Parse(PlayerPrefs.GetInt("TrainTimes").ToString()) == 2)
                {
                    ResetTrainTimes(2);
                }
            }
            else
            {
                ResetTrainTimes(int.Parse(PlayerPrefs.GetInt("TrainTimes").ToString()));
            }
        }
        UIEventListener.Get(ATKtype).onPress = ClickAtkOrDefTypeButton;
        UIEventListener.Get(DEFtype).onPress = ClickAtkOrDefTypeButton;
        UIEventListener.Get(TrainTimesButton).onClick = delegate(GameObject go)
        {
            UIManager.instance.OpenPanel("HeroTrainTimesWindow", false);
            GameObject.Find("HeroTrainTimesWindow").layer = 11;
        };
    }
    void SetTrainOnceCostList()
    {
        trainOnceCostList.Add(5);
        trainOnceCostList.Add(5);
        trainOnceCostList.Add(6000);
        trainOnceCostList.Add(5);
        trainOnceCostList.Add(10);
    }
    public void ResetTrainTimes(int _trainTimes)
    {
        trainTimes = _trainTimes;
        for (int i = 0; i < trainCostLabelList.Count; i++)
        {
            trainCostLabelList[i].text = (trainOnceCostList[i] * trainTimes).ToString();
        }
        TrainTimesButton.transform.FindChild("Label").GetComponent<UILabel>().text = string.Format("培养{0}次", trainTimes);
    }

    void ClickAtkOrDefTypeButton(GameObject go, bool isPress)
    {
        GameObject _InfoBoard = go.transform.FindChild("InfoBoard").gameObject;
        if (isPress)
        {
            _InfoBoard.SetActive(true);
        }
        else
        {
            _InfoBoard.SetActive(false);
        }
    }
    public void StartPart(Hero _hero)
    {
        //Debug.LogError("UpType:" + UpType);
        if (PlayerPrefs.GetInt("TrainUpType", 0) == 0)
        {
            UpType = PlayerPrefs.GetInt("TrainUpType", 1);
            gameObject.transform.FindChild("Right/SelectTime/Toggle" + UpType).GetComponent<UIToggle>().value = true;
        }
        else
        {
            UpType = PlayerPrefs.GetInt("TrainUpType");
            gameObject.transform.FindChild("Right/SelectTime/Toggle" + UpType).GetComponent<UIToggle>().value = true;
        }
        //Debug.LogError("UpType:" + UpType);
        mHero = _hero;
        HeroInfo _HeroInfo = TextTranslator.instance.GetHeroInfoByHeroID(mHero.cardID);
        mHeroInfo = _HeroInfo;
        Name.text = mHeroInfo.heroName;
        medicineLabel.text = TextTranslator.instance.GetItemCountByID(10500) != null ? TextTranslator.instance.GetItemCountByID(10500).ToString() : "0";
        SetAtkOrDefTRype();
        SetRarityIcon();
        //SetNameColor();//旧的
        TextTranslator.instance.SetHeroNameColor(Name, mHeroInfo.heroName, mHero.classNumber);

        UIEventListener.Get(TrainButton).onClick = delegate(GameObject go)
        {
            if (CharacterRecorder.instance.lastGateID > 10030)
            {
                if (CharacterRecorder.instance.GuideID[30] == 7)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
                NetworkHandler.instance.SendProcess("1026#" + mHero.characterRoleID + ";" + UpType + ";" + trainTimes + ";");
                IsHeroTrained = true;
            }
            else
            {
                UIManager.instance.OpenPromptWindow(string.Format("{0}关开放培养", 30), 11, false, PromptWindow.PromptType.Hint, null, null);
            }
        };

        UIEventListener.Get(ReplaceButton).onClick = delegate(GameObject go)
        {
            if (CharacterRecorder.instance.GuideID[30] == 8)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
            NetworkHandler.instance.SendProcess("1027#" + mHero.characterRoleID + ";");
        };

        NetworkHandler.instance.SendProcess("1025#" + mHero.characterRoleID + ";");



        //人物角色生成
        PictureCreater.instance.DestroyAllComponent();
        int K = PictureCreater.instance.CreateRole(mHero.cardID, "Model", 18, Color.red, 0, 0, 1, 2f, false, false, 1, 1.5f, 0.2f, "Model", 0, 10, 10, 2, 2, 0, 0, 1001, 1002, mHero.WeaponList[0].WeaponClass, 1, mHero.WeaponList[0].WeaponClass, 1, 1, 0, "");
        PictureCreater.instance.ListRolePicture[0].RoleObject.transform.parent = gameObject.transform;
        //角色旋转、播动画
        GameObject _roleWindow = GameObject.Find("RoleWindow");
        if (_roleWindow != null)
        {
            _roleWindow.GetComponent<PlayAniOrRotation>().ResetTargetRole(PictureCreater.instance.ListRolePicture[0].RoleObject.transform.FindChild("Role").gameObject, _hero);
        }
        int modelPositionY = -950;
        if (_hero.cardID == 60201)// 飞机的话|| _hero.cardID == 60200
        {
            modelPositionY = -1200;
        }
        if ((Screen.width * 1f / Screen.height) > 1.7f)
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = new Vector3(6940, modelPositionY, 13339);
        }
        else if ((Screen.width * 1f / Screen.height) < 1.4f)
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = new Vector3(7749 - 636 + 160 - 92 - 341 + 210, modelPositionY, 13339);
        }
        else
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = new Vector3(7649 - 636 + 160 - 92 - 341 + 210, modelPositionY, 13339);
        }
        //PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localScale = new Vector3(450, 450, 450) * TextTranslator.instance.GetHeroInfoByHeroID(PictureCreater.instance.ListRolePicture[0].RoleID).heroScale;
        //PictureCreater.instance.ListRolePicture[0].RoleObject.transform.Rotate(new Vector3(0, 65, 0));
        PictureCreater.instance.ListRolePicture[0].RolePictureObject.transform.Rotate(new Vector3(0, 65, 0));
        PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localScale = new Vector3(400, 400, 400);
    }

    public void SetTrainState(int _Hp, int _Atk, int _Def, float _Cri, float _UnCri)
    {
        RoleWash rw = TextTranslator.instance.GetRoleWashByID(mHero.cardID);
        //Debug.LogError(rw);
        //Debug.LogError(rw.HpMax);
        //HpSlider.value = (float)_Hp / ((float)rw.HpMax * mHero.level);
        HpSlider.transform.FindChild("Foreground").GetComponent<UISprite>().fillAmount = (float)_Hp / ((float)rw.HpMax * mHero.level);
        HpSliderLabel.text = string.Format("{0}/{1}", _Hp, rw.HpMax * mHero.level);

        //AtkSlider.value = (float)_Atk / ((float)rw.AtkMax * mHero.level);
        AtkSlider.transform.FindChild("Foreground").GetComponent<UISprite>().fillAmount = (float)_Atk / ((float)rw.AtkMax * mHero.level);
        AtkSliderLabel.text = string.Format("{0}/{1}", _Atk, rw.AtkMax * mHero.level);

        //DefSlider.value = (float)_Def / ((float)rw.DefMax * mHero.level);
        DefSlider.transform.FindChild("Foreground").GetComponent<UISprite>().fillAmount = (float)_Def / ((float)rw.DefMax * mHero.level);
        DefSliderLabel.text = string.Format("{0}/{1}", _Def, rw.DefMax * mHero.level);

        //CriSlider.value = _Cri / rw.CriMax;
        CriSlider.transform.FindChild("Foreground").GetComponent<UISprite>().fillAmount = _Cri / rw.CriMax;
        CriSliderLabel.text = string.Format("{0}%/{1}%", Math.Round(_Cri * 100, 1), rw.CriMax * 100);

        //UnCriSlider.value = _UnCri / rw.UnCriMax;
        UnCriSlider.transform.FindChild("Foreground").GetComponent<UISprite>().fillAmount = _UnCri / rw.UnCriMax;
        UnCriSliderLabel.text = string.Format("{0}%/{1}%", Math.Round(_UnCri * 100, 1), rw.UnCriMax * 100);
        medicineLabel.text = TextTranslator.instance.GetItemCountByID(10500) != null ? TextTranslator.instance.GetItemCountByID(10500).ToString() : "0";

        if (isClean)
        {
            HP.transform.FindChild("AddNumber").GetComponent<UILabel>().text = "+0";
            ATK.transform.FindChild("AddNumber").GetComponent<UILabel>().text = "+0";
            DEF.transform.FindChild("AddNumber").GetComponent<UILabel>().text = "+0";
            CRI.transform.FindChild("AddNumber").GetComponent<UILabel>().text = "+0";
            UNCRI.transform.FindChild("AddNumber").GetComponent<UILabel>().text = "+0";

            Transform _UpDownTransformHP = HP.transform.FindChild("UpDown");
            UISprite _UpDownUISpriteHP = _UpDownTransformHP.GetComponent<UISprite>();
            TweenPosition _TweenPositionHp = _UpDownTransformHP.GetComponent<TweenPosition>();
            TweenAlpha _TweenAlphaHp = _UpDownTransformHP.GetComponent<TweenAlpha>();

            Transform _UpDownTransformATK = ATK.transform.FindChild("UpDown");
            UISprite _UpDownUISpriteATK = _UpDownTransformATK.GetComponent<UISprite>();
            TweenPosition _TweenPositionATK = _UpDownTransformATK.GetComponent<TweenPosition>();
            TweenAlpha _TweenAlphaATK = _UpDownTransformATK.GetComponent<TweenAlpha>();

            Transform _UpDownTransformDEF = DEF.transform.FindChild("UpDown");
            UISprite _UpDownUISpriteDEF = _UpDownTransformDEF.GetComponent<UISprite>();
            TweenPosition _TweenPositionDEF = _UpDownTransformDEF.GetComponent<TweenPosition>();
            TweenAlpha _TweenAlphaDEF = _UpDownTransformDEF.GetComponent<TweenAlpha>();

            Transform _UpDownTransformCRI = CRI.transform.FindChild("UpDown");
            UISprite _UpDownUISpriteCRI = _UpDownTransformCRI.GetComponent<UISprite>();
            TweenPosition _TweenPositionCRI = _UpDownTransformCRI.GetComponent<TweenPosition>();
            TweenAlpha _TweenAlphaCRI = _UpDownTransformCRI.GetComponent<TweenAlpha>();

            Transform _UpDownTransformUNCRI = UNCRI.transform.FindChild("UpDown");
            UISprite _UpDownUISpriteUNCRI = _UpDownTransformUNCRI.GetComponent<UISprite>();
            TweenPosition _TweenPositionUNCRI = _UpDownTransformUNCRI.GetComponent<TweenPosition>();
            TweenAlpha _TweenAlphaUNCRI = _UpDownTransformUNCRI.GetComponent<TweenAlpha>();

            _UpDownUISpriteHP.spriteName = "";
            _UpDownUISpriteATK.spriteName = "";
            _UpDownUISpriteDEF.spriteName = "";
            _UpDownUISpriteCRI.spriteName = "";
            _UpDownUISpriteUNCRI.spriteName = "";
            _TweenPositionHp.enabled = false;
            _TweenAlphaHp.enabled = false;
            _TweenPositionATK.enabled = false;
            _TweenAlphaATK.enabled = false;
            _TweenPositionDEF.enabled = false;
            _TweenAlphaDEF.enabled = false;
            _TweenPositionCRI.enabled = false;
            _TweenAlphaCRI.enabled = false;
            _TweenPositionUNCRI.enabled = false;
            _TweenAlphaUNCRI.enabled = false;
            List<float> addNumberList = new List<float>();
            for (int i = 0; i < 5; i++)
            {
                addNumberList.Add(0);
            }
            ShowTrainOrReplaceButton(addNumberList);
            isClean = false;
        }

    }
    int GetShowButtonState(List<float> addNumList)// -1 只有替换，1 只有培养，0 都有
    {
        /*  for (int i = 0; i < addNumList.Count; i++)
          {
              if(addNumList[i] < 0)
              {
                  return 0;
              }
              else if (addNumList[i] > 0)
              {
                  return -1;
              }
              else
              {
                  return 1;
              }
          }
          return -10; */
        for (int i = 0; i < addNumList.Count; i++)
        {
            if (addNumList[i] < 0)
            {
                return 0;
            }
        }
        for (int i = 0; i < addNumList.Count; i++)
        {
            if (addNumList[i] > 0)
            {
                return -1;
            }
        }
        return 1;
    }
    void ShowTrainOrReplaceButton(List<float> addNumList)
    {
        UILabel desLabel = TrainButton.transform.FindChild("DesLabel").GetComponent<UILabel>();
        switch (UpType)
        {
            case 1: desLabel.text = ""; break;
            case 2: desLabel.text = "金帀培养三围不会有负数"; break;
            case 3: desLabel.text = "钻石培养全属性不会有负数"; break;
        }
        if (GetShowButtonState(addNumList) == -1)
        {
            TrainButton.SetActive(false);
            ReplaceButton.SetActive(true);
        }
        else if (GetShowButtonState(addNumList) == 0)
        {
            TrainButton.SetActive(true);
            ReplaceButton.SetActive(true);
        }
        else if (GetShowButtonState(addNumList) == 1)
        {
            TrainButton.SetActive(true);
            ReplaceButton.SetActive(false);
        }
    }
    public void UpdateTrainAdd(int _HpAdd, int _AtkAdd, int _DefAdd, float _CriAdd, float _UnCriAdd)
    {
        //Debug.Log(_HpAdd + "          " + _CriAdd);
        List<float> addNumList = new List<float>();
        addNumList.Add(_HpAdd);
        addNumList.Add(_AtkAdd);
        addNumList.Add(_DefAdd);
        addNumList.Add(_CriAdd);
        addNumList.Add(_UnCriAdd);
        ShowTrainOrReplaceButton(addNumList);
        medicineLabel.text = TextTranslator.instance.GetItemCountByID(10500) != null ? TextTranslator.instance.GetItemCountByID(10500).ToString() : "0";
        Transform _UpDownTransformHP = HP.transform.FindChild("UpDown");
        UISprite _UpDownUISpriteHP = _UpDownTransformHP.GetComponent<UISprite>();
        TweenPosition _TweenPositionHp = _UpDownTransformHP.GetComponent<TweenPosition>();
        TweenAlpha _TweenAlphaHp = _UpDownTransformHP.GetComponent<TweenAlpha>();
        if (_HpAdd > 0)
        {
            HP.transform.FindChild("AddNumber").GetComponent<UILabel>().text = "[3ee817]+" + _HpAdd.ToString();
            _UpDownUISpriteHP.spriteName = "ui_peiyang_icon1";
            _TweenPositionHp.enabled = true;
            _TweenAlphaHp.enabled = true;
        }
        else if (_HpAdd == 0)
        {
            HP.transform.FindChild("AddNumber").GetComponent<UILabel>().text = "+0";
            _UpDownUISpriteHP.spriteName = "+0";
            _TweenPositionHp.enabled = false;
            _TweenAlphaHp.enabled = false;
        }
        else if (_HpAdd < 0)
        {
            HP.transform.FindChild("AddNumber").GetComponent<UILabel>().text = "[ff0000]" + _HpAdd.ToString();//[fb2d50]
            _UpDownUISpriteHP.spriteName = "ui_peiyang_icon2";
            _TweenPositionHp.enabled = false;
            _TweenAlphaHp.enabled = false;
        }

        Transform _UpDownTransformATK = ATK.transform.FindChild("UpDown");
        UISprite _UpDownUISpriteATK = _UpDownTransformATK.GetComponent<UISprite>();
        TweenPosition _TweenPositionATK = _UpDownTransformATK.GetComponent<TweenPosition>();
        TweenAlpha _TweenAlphaATK = _UpDownTransformATK.GetComponent<TweenAlpha>();
        if (_AtkAdd > 0)
        {
            ATK.transform.FindChild("AddNumber").GetComponent<UILabel>().text = "[3ee817]+" + _AtkAdd.ToString();
            _UpDownUISpriteATK.spriteName = "ui_peiyang_icon1";
            _TweenPositionATK.enabled = true;
            _TweenAlphaATK.enabled = true;
        }
        else if (_AtkAdd == 0)
        {
            ATK.transform.FindChild("AddNumber").GetComponent<UILabel>().text = "+0";
            _UpDownUISpriteATK.spriteName = "+0";
            _TweenPositionATK.enabled = false;
            _TweenAlphaATK.enabled = false;
        }
        else if (_AtkAdd < 0)
        {
            ATK.transform.FindChild("AddNumber").GetComponent<UILabel>().text = "[ff0000]" + _AtkAdd.ToString();
            _UpDownUISpriteATK.spriteName = "ui_peiyang_icon2";
            _TweenPositionATK.enabled = false;
            _TweenAlphaATK.enabled = false;
        }

        Transform _UpDownTransformDEF = DEF.transform.FindChild("UpDown");
        UISprite _UpDownUISpriteDEF = _UpDownTransformDEF.GetComponent<UISprite>();
        TweenPosition _TweenPositionDEF = _UpDownTransformDEF.GetComponent<TweenPosition>();
        TweenAlpha _TweenAlphaDEF = _UpDownTransformDEF.GetComponent<TweenAlpha>();
        if (_DefAdd > 0)
        {
            DEF.transform.FindChild("AddNumber").GetComponent<UILabel>().text = "[3ee817]+" + _DefAdd.ToString();
            _UpDownUISpriteDEF.spriteName = "ui_peiyang_icon1";
            _TweenPositionDEF.enabled = true;
            _TweenAlphaDEF.enabled = true;
        }
        else if (_DefAdd == 0)
        {
            DEF.transform.FindChild("AddNumber").GetComponent<UILabel>().text = "+0";
            _UpDownUISpriteDEF.spriteName = "+0";
            _TweenPositionDEF.enabled = false;
            _TweenAlphaDEF.enabled = false;
        }
        else if (_DefAdd < 0)
        {
            DEF.transform.FindChild("AddNumber").GetComponent<UILabel>().text = "[ff0000]" + _DefAdd.ToString();
            _UpDownUISpriteDEF.spriteName = "ui_peiyang_icon2";
            _TweenPositionDEF.enabled = false;
            _TweenAlphaDEF.enabled = false;
        }

        Transform _UpDownTransformCRI = CRI.transform.FindChild("UpDown");
        UISprite _UpDownUISpriteCRI = _UpDownTransformCRI.GetComponent<UISprite>();
        TweenPosition _TweenPositionCRI = _UpDownTransformCRI.GetComponent<TweenPosition>();
        TweenAlpha _TweenAlphaCRI = _UpDownTransformCRI.GetComponent<TweenAlpha>();
        if (_CriAdd > 0)
        {
            CRI.transform.FindChild("AddNumber").GetComponent<UILabel>().text = string.Format("[3ee817]+{0}%", _CriAdd * 100);
            CRI.transform.FindChild("UpDown").GetComponent<UISprite>().spriteName = "ui_peiyang_icon1";
            _TweenPositionCRI.enabled = true;
            _TweenAlphaCRI.enabled = true;
        }
        else if (_CriAdd == 0)
        {
            CRI.transform.FindChild("AddNumber").GetComponent<UILabel>().text = "+0";
            CRI.transform.FindChild("UpDown").GetComponent<UISprite>().spriteName = "";
            _TweenPositionCRI.enabled = false;
            _TweenAlphaCRI.enabled = false;
        }
        else if (_CriAdd < 0)
        {
            CRI.transform.FindChild("AddNumber").GetComponent<UILabel>().text = string.Format("[ff0000]{0}%", _CriAdd * 100);
            CRI.transform.FindChild("UpDown").GetComponent<UISprite>().spriteName = "ui_peiyang_icon2";
            _TweenPositionCRI.enabled = false;
            _TweenAlphaCRI.enabled = false;
        }

        Transform _UpDownTransformUNCRI = UNCRI.transform.FindChild("UpDown");
        UISprite _UpDownUISpriteUNCRI = _UpDownTransformUNCRI.GetComponent<UISprite>();
        TweenPosition _TweenPositionUNCRI = _UpDownTransformUNCRI.GetComponent<TweenPosition>();
        TweenAlpha _TweenAlphaUNCRI = _UpDownTransformUNCRI.GetComponent<TweenAlpha>();
        if (_UnCriAdd > 0)
        {
            UNCRI.transform.FindChild("AddNumber").GetComponent<UILabel>().text = string.Format("[3ee817]+{0}%", _UnCriAdd * 100);
            UNCRI.transform.FindChild("UpDown").GetComponent<UISprite>().spriteName = "ui_peiyang_icon1";
            _TweenPositionUNCRI.enabled = true;
            _TweenAlphaUNCRI.enabled = true;
        }
        else if (_UnCriAdd == 0)
        {
            UNCRI.transform.FindChild("AddNumber").GetComponent<UILabel>().text = "+0";
            UNCRI.transform.FindChild("UpDown").GetComponent<UISprite>().spriteName = "";
            _TweenPositionUNCRI.enabled = false;
            _TweenAlphaUNCRI.enabled = false;
        }
        else if (_UnCriAdd < 0)
        {
            UNCRI.transform.FindChild("AddNumber").GetComponent<UILabel>().text = string.Format("[ff0000]{0}%", _UnCriAdd * 100);
            UNCRI.transform.FindChild("UpDown").GetComponent<UISprite>().spriteName = "ui_peiyang_icon2";
            _TweenPositionUNCRI.enabled = false;
            _TweenAlphaUNCRI.enabled = false;
        }
    }

    public void SetUpType1()
    {
        UpType = 1;
        PlayerPrefs.SetInt("TrainUpType", UpType);
        if (!ReplaceButton.activeSelf)
        {
            SetUpTypeDesLabel();
        }
        //Debug.Log(UpType + "    111111");
    }

    public void SetUpType2()
    {
        UpType = 2;
        PlayerPrefs.SetInt("TrainUpType", UpType);
        if (!ReplaceButton.activeSelf)
        {
            SetUpTypeDesLabel();
        }
        //Debug.Log(UpType + "    222222");
    }

    public void SetUpType3()
    {
        UpType = 3;
        PlayerPrefs.SetInt("TrainUpType", UpType);
        if (!ReplaceButton.activeSelf)
        {
            SetUpTypeDesLabel();
        }
        //Debug.Log(UpType + "    3333333");
    }
    void SetUpTypeDesLabel()
    {
        UILabel desLabel = TrainButton.transform.FindChild("DesLabel").GetComponent<UILabel>();
        switch (UpType)
        {
            case 1: desLabel.text = ""; break;
            case 2: desLabel.text = "金帀培养三围不会有负数"; break;
            case 3: desLabel.text = "钻石培养全属性不会有负数"; break;
        }
    }
    //设置名字颜色
    void SetNameColor()
    {
        switch (mHero.classNumber)
        {
            case 1:
                Name.color = Color.white;
                break;
            case 2:
                Name.text = "[3ee817]" + Name.text + "[-]";
                break;
            case 3:
                Name.text = "[3ee817]" + Name.text + "+1[-]";
                break;
            case 4:
                Name.text = "[249bd2]" + Name.text + "[-]";
                break;
            case 5:
                Name.text = "[249bd2]" + Name.text + "+1[-]";
                break;
            case 6:
                Name.text = "[249bd2]" + Name.text + "+2[-]";
                break;
            case 7:
                Name.text = "[bb44ff]" + Name.text + "[-]";
                break;
            case 8:
                Name.text = "[bb44ff]" + Name.text + "+1[-]";
                break;
            case 9:
                Name.text = "[bb44ff]" + Name.text + "+2[-]";
                break;
            case 10:
                Name.text = "[bb44ff]" + Name.text + "+3[-]";
                break;
            case 11:
                Name.text = "[ff8c04]" + Name.text + "[-]";
                break;
            case 12:
                Name.text = "[ff8c04]" + Name.text + "+1[-]";
                break;
            default:
                break;
        }
    }
    //设置攻击防御类型
    void SetAtkOrDefTRype()
    {
        ATKtype.GetComponent<UISprite>().spriteName = string.Format("atk{0}", mHeroInfo.heroAtkType);
        DEFtype.GetComponent<UISprite>().spriteName = string.Format("def{0}", mHeroInfo.heroDefType);
        Transform _ATKtypeInfoBoard = ATKtype.transform.FindChild("InfoBoard");
        string ATKtypeName = "";
        string ATKtypeDes = "";
        switch (mHeroInfo.heroAtkType)
        {
            case 1: ATKtypeName = "普通枪械"; ATKtypeDes = "对[ff0000]轻甲[-]造成较多伤害，对[ff0000]建筑[-]伤害很弱。"; break;
            case 2: ATKtypeName = "重型枪械"; ATKtypeDes = "对[ff0000]中甲[-]造成较多伤害，对[ff0000]建筑[-]伤害很弱。"; break;
            case 3: ATKtypeName = "炮弹"; ATKtypeDes = "对[ff0000]重装[-]造成较多伤害，对[ff0000]中甲[-]伤害较弱。"; break;
            case 4: ATKtypeName = "穿甲弹"; ATKtypeDes = "对[ff0000]建筑[-]造成大量伤害，对[ff0000]轻甲[-]伤害较弱。"; break;
        }
        _ATKtypeInfoBoard.FindChild("TypeName").GetComponent<UILabel>().text = ATKtypeName;
        _ATKtypeInfoBoard.FindChild("TypeDes").GetComponent<UILabel>().text = ATKtypeDes;

        Transform _DEFtypeInfoBoard = DEFtype.transform.FindChild("InfoBoard");
        string DEFtypeName = "";
        string DEFtypeDes = "";
        switch (mHeroInfo.heroAtkType)
        {
            case 1: DEFtypeName = "轻甲"; DEFtypeDes = "步兵护甲，防护能力较低。"; break;
            case 2: DEFtypeName = "中甲"; DEFtypeDes = "步兵强化护甲，可有效抵御伤害。"; break;
            case 3: DEFtypeName = "重装"; DEFtypeDes = "机械单位护甲，对普通枪械较强。"; break;
            case 4: DEFtypeName = "建筑"; DEFtypeDes = "强固如城壁的护甲，但是惧怕穿甲弹。"; break;
        }
        _DEFtypeInfoBoard.FindChild("TypeName").GetComponent<UILabel>().text = DEFtypeName;
        _DEFtypeInfoBoard.FindChild("TypeDes").GetComponent<UILabel>().text = DEFtypeDes;
    }
    //设置稀有度
    void SetRarityIcon()
    {
        switch (mHeroInfo.heroRarity)
        {
            case 1:
                RarityIcon.spriteName = "word4";
                break;
            case 2:
                RarityIcon.spriteName = "word5";
                break;
            case 3:
                RarityIcon.spriteName = "word6";
                break;
            case 4:
                RarityIcon.spriteName = "word7";
                break;
            case 5:
                RarityIcon.spriteName = "word8";
                break;
            case 6:
                RarityIcon.spriteName = "word9";
                break;
            default:
                break;
        }
    }
}
