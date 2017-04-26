using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum AdvanceWindowType
{
    HeroShengPin = 0,//角色升品
    HeroUpJunXian = 1,//角色升军衔
    HeroSkillUp = 2,//英雄技能升级
    EquipJinJie = 3,//装备进阶、升品
    EquipRefine = 4,//精炼
    GainResult = 5,//恭喜获得
    BaoWuUp = 6,//饰品升级
    BaoWuAwake = 7,//饰品觉醒（晋级）
    AllEquipJinJie = 8,//四件装备全部升品
    WeaponUpStart = 9,//神器
};
public class AdvanceWindow : MonoBehaviour
{
    [SerializeField]
    private GameObject ShengPinResultPart;
    [SerializeField]
    private GameObject HeroUpJunXianPart;
    [SerializeField]
    private GameObject HeroSkillUpPart;
    [SerializeField]
    private GameObject EquipJinJieResultPart;
    [SerializeField]
    private GameObject AllEquipJinJieResultPart;
    [SerializeField]
    private GameObject EquipRefineResultPart;
    [SerializeField]
    private GameObject GainResultPart;
    [SerializeField]
    private GameObject BaoWuUpResultPart;
    [SerializeField]
    private GameObject BaoWuAwakeResultPart;
    [SerializeField]
    private GameObject WeaponUpStarResultPart;

    public GameObject FlyLight;
    public GameObject HappyGet;
    public GameObject BlueSun;
    public GameObject BG;
    public GameObject BGLight;
    public GameObject Line;

    public GameObject Ying1;
    public GameObject Ying2;

    public GameObject Label1;
    public GameObject Label2;

    public GameObject SureButton;
    public GameObject DesObj;
    public GameObject MaskButton;

    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;

    Hero mHero;
    List<Item> _ItemList = new List<Item>();
    public bool IsWoodsWindow = false;//在从林冒险界面奖励是否为其它的（抢红包）,不是弹出宝箱
    // Use this for initialization
    void Start()
    {
        /*  if (UIEventListener.Get(SureButton).onClick == null)
          {
              UIEventListener.Get(SureButton).onClick += delegate(GameObject go)
              {
                  UIManager.instance.BackUI();
              };
          }*/
        //   UIEventListener.Get(SureButton).onClick = ClickSureButton;
        if (GameObject.Find("StrengthenSuccess") != null)
        {
            gameObject.GetComponent<UIPanel>().depth = 3;
        }
        else if (GameObject.Find("TaskWindow") != null)
        {
            gameObject.GetComponent<UIPanel>().depth = 3;
        }
        else 
        {
            gameObject.GetComponent<UIPanel>().depth = 0;
        }
        if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 5 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 2)
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_902);
        }
        if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 6)
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_904);
        }
        UIEventListener.Get(MaskButton).onClick = ClickSureButton;
    }
    void OnDestroy()
    {
        CharacterRecorder.instance.IsNeedOpenFight = true;
    }
    public void SetInfo(AdvanceWindowType _AdvanceWindowType, Hero _hero, Hero _HeroNewData, Hero.EquipInfoBefore _mEquipInfoBefore, Hero.EquipInfo _EquipInfo, List<Item> _ItemList)
    {
        if (GameObject.Find("LevelUpWindow") == null)
        {
            this.gameObject.layer = 11;
        }
        else
        {
            this.gameObject.layer = 5;
        }
        foreach (Component c in this.GetComponentsInChildren(typeof(Transform), true))
        {
            c.gameObject.layer = 11;
        }


        //Debug.LogError(_AdvanceWindowType);
        switch (_AdvanceWindowType)
        {
            case AdvanceWindowType.HeroShengPin:
                AudioEditer.instance.PlayOneShot("ui_shengpin");
                mHero = _hero;
                //  StartCoroutine(SetEffect());
                StartCoroutine(DelayShowButton());
                ShengPinResultPart.SetActive(true);
                ShengPinResultPart.GetComponent<HeroShengPinResultPart>().SetHeroShengPinResultPart(_hero, _HeroNewData);
                break;
            case AdvanceWindowType.HeroUpJunXian:
                AudioEditer.instance.PlayOneShot("ui_jinsheng");
                mHero = _hero;
                StartCoroutine(DelayShowButton());
                HeroUpJunXianPart.SetActive(true);
                HeroUpJunXianPart.GetComponent<HeroUpJunXianPart>().SetHeroUpJunXianPart(_hero, _HeroNewData);
                break;
            case AdvanceWindowType.HeroSkillUp:
                AudioEditer.instance.PlayOneShot("ui_levelUp");
                mHero = _hero;
                StartCoroutine(DelayShowButton());
                HeroSkillUpPart.SetActive(true);
                HeroSkillUpPart.GetComponent<HeroSkillUpResultPart>().SetHeroSkillUpResultPart(_hero);
                break;
            case AdvanceWindowType.EquipJinJie:
                AudioEditer.instance.PlayOneShot("ui_powerbig");
                mHero = _hero;
                //StartCoroutine(SetEffect());
                StartCoroutine(DelayShowButton());
                EquipJinJieResultPart.SetActive(true);
                EquipJinJieResultPart.GetComponent<EquipJinJieResultPart>().SetEquipJinJieResultPart(_mEquipInfoBefore, _EquipInfo);
                break;
            case AdvanceWindowType.EquipRefine:
                AudioEditer.instance.PlayOneShot("ui_jinglian");
                mHero = _hero;
                //StartCoroutine(SetEffect());
                StartCoroutine(DelayShowButton());
                EquipRefineResultPart.SetActive(true);
                EquipRefineResultPart.GetComponent<EquipRefineResultPart>().SetEquipRefineResultPart(_mEquipInfoBefore, _EquipInfo);
                break;
            case AdvanceWindowType.GainResult:
                AudioEditer.instance.PlayOneShot("ui_recieve");
                StartCoroutine(DelayShowButton());
                GainResultPart.SetActive(true);
                //    BG.SetActive(true);
                GainResultPart.GetComponent<GainResultPart>().SetGainResultPart(_ItemList);
                this._ItemList = _ItemList;
                break;

            case AdvanceWindowType.BaoWuUp:
                //AudioEditer.instance.PlayOneShot("ui_jinglian");
                AudioEditer.instance.PlayOneShot("ui_powerbig");
                mHero = _hero;
                //StartCoroutine(SetEffect());
                StartCoroutine(DelayShowButton());
                BaoWuUpResultPart.SetActive(true);
                BaoWuUpResultPart.GetComponent<EquipJinJieResultPart>().SetEquipJinJieResultPart(_mEquipInfoBefore, _EquipInfo);
                break;
            case AdvanceWindowType.BaoWuAwake:
                //AudioEditer.instance.PlayOneShot("ui_jinglian");
                AudioEditer.instance.PlayOneShot("ui_powerbig");
                mHero = _hero;
                //StartCoroutine(SetEffect());
                StartCoroutine(DelayShowButton());
                BaoWuAwakeResultPart.SetActive(true);
                //BaoWuAwakeResultPart.GetComponent<EquipRefineResultPart>().SetEquipRefineResultPart(_mEquipInfoBefore, _EquipInfo);//精炼数据
                BaoWuAwakeResultPart.GetComponent<EquipJinJieResultPart>().SetEquipJinJieResultPart(_mEquipInfoBefore, _EquipInfo);//觉醒保持强化数据
                break;

        }
    }
    public void SetInfo(AdvanceWindowType _AdvanceWindowType, BetterList<Hero.EquipInfoBefore> mEquipInfoBeforeList, BetterList<Hero.EquipInfo> mEquipInfoList)
    {
        if (GameObject.Find("LevelUpWindow") == null)
        {
            this.gameObject.layer = 11;
        }
        else
        {
            this.gameObject.layer = 5;
        }
        foreach (Component c in this.GetComponentsInChildren(typeof(Transform), true))
        {
            c.gameObject.layer = 11;
        }


        //Debug.LogError(_AdvanceWindowType);
        switch (_AdvanceWindowType)
        {
            case AdvanceWindowType.AllEquipJinJie:
                AudioEditer.instance.PlayOneShot("ui_powerbig");
                //StartCoroutine(SetEffect());
                StartCoroutine(DelayShowButton());
                AllEquipJinJieResultPart.SetActive(true);
                AllEquipJinJieResultPart.GetComponent<AllEquipJinJieResultPart>().SetEquipJinJieResultPart(mEquipInfoBeforeList, mEquipInfoList);
                break;
        }
    }

    public void SetInfo(AdvanceWindowType _AdvanceWindowType, int WeaponID, int WeaponClass, int WeaponStar, string BeforeFight, string AfterFight)
    {
        Debug.LogError("sssss  " + WeaponID + "   " + WeaponClass + "  " + WeaponStar);
        if (GameObject.Find("LevelUpWindow") == null)
        {
            this.gameObject.layer = 11;
        }
        else
        {
            this.gameObject.layer = 5;
        }
        foreach (Component c in this.GetComponentsInChildren(typeof(Transform), true))
        {
            c.gameObject.layer = 11;

        }
        switch (_AdvanceWindowType)
        {
            case AdvanceWindowType.WeaponUpStart:
                AudioEditer.instance.PlayOneShot("ui_powerbig");
                //StartCoroutine(SetEffect());
                StartCoroutine(DelayShowButton());
                WeaponUpStarResultPart.SetActive(true);
                WeaponUpStarResultPart.GetComponent<WeaponUpStarResultPart>().SetInfo(WeaponID, WeaponClass, WeaponStar, BeforeFight, AfterFight);
                break;
        }
    }
    IEnumerator DelayShowButton()
    {
        yield return new WaitForSeconds(1.0f);
        //     SureButton.SetActive(true);
        DesObj.SetActive(true);
    }
    IEnumerator SetEffect()
    {
        yield return new WaitForSeconds(0.05f);
        FlyLight.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        //     BG.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        HappyGet.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        //     SureButton.SetActive(true);
        DesObj.SetActive(true);
        Ying1.SetActive(true);
        Ying2.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        BlueSun.SetActive(true);
        BGLight.SetActive(true);
        /*  yield return new WaitForSeconds(0.2f);
          Label1.SetActive(true);
          yield return new WaitForSeconds(0.2f);
          Line.SetActive(true);
          yield return new WaitForSeconds(0.2f);
          Label2.SetActive(true);
          yield return new WaitForSeconds(0.2f);*/
        //SureButton.SetActive(true);

    }

    void SetIcon(GameObject go, Item item)
    {
        go.transform.FindChild("SuiPian").gameObject.SetActive(false);
        if (item.itemCode.ToString()[0] == '4')
        {
            go.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(item.itemCode - 10000).picID.ToString();
            go.transform.FindChild("SuiPian").gameObject.SetActive(true);
        }
        else if (item.itemCode == 70000 || item.itemCode == 79999) 
        {
            go.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = item.itemCode.ToString();
            go.transform.FindChild("SuiPian").gameObject.SetActive(true);
        }
        else if (item.itemCode.ToString()[0] == '7')
        {
            go.transform.FindChild("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
            go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(item.itemCode - 10000).picID.ToString();
            go.transform.FindChild("SuiPian").gameObject.SetActive(true);
        }
        else if (item.itemCode.ToString()[0] == '8')
        {
            go.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode((item.itemCode / 10) * 10 - 30000).picID.ToString();
            go.transform.FindChild("SuiPian").gameObject.SetActive(true);
        }
        else if (item.itemCode.ToString()[0] == '6')
        {
            go.transform.FindChild("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
            go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(item.itemCode - 10000).picID.ToString();
        }
        else
        {
            go.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(item.itemCode - 10000).picID.ToString();
        }
        go.transform.FindChild("Count").GetComponent<UILabel>().text = item.itemCount.ToString();
        go.transform.FindChild("Name").GetComponent<UILabel>().text = item.itemName.ToString();
    }

    void SetColor(GameObject go, int color)
    {
        switch (color)
        {
            case 1:
                go.GetComponent<UISprite>().spriteName = "Grade1";
                break;
            case 2:
                go.GetComponent<UISprite>().spriteName = "Grade2";
                break;
            case 3:
                go.GetComponent<UISprite>().spriteName = "Grade3";
                break;
            case 4:
                go.GetComponent<UISprite>().spriteName = "Grade4";
                break;
            case 5:
                go.GetComponent<UISprite>().spriteName = "Grade5";
                break;
            case 6:
                go.GetComponent<UISprite>().spriteName = "Grade6";
                break;
            case 7:
                go.GetComponent<UISprite>().spriteName = "Grade7";
                break;
        }
    }
    void ClickSureButton(GameObject go)
    {
        if (go != null)
        {
            if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) >= 18)
            {
                if (CharacterRecorder.instance.GuideID[32] == 7)
                {
                    CharacterRecorder.instance.GuideID[32] += 1;
                }
                else if (CharacterRecorder.instance.GuideID[28] == 9)
                {
                    //CharacterRecorder.instance.GuideID[28] += 1;
                }
            }
            for (int i = 0; i < this._ItemList.Count; i++)
            {
                if (this._ItemList[i].itemCode > 60000 && this._ItemList[i].itemCode < 70000)
                {
                    Debug.Log("奖励窗口1：" + this._ItemList[i].itemCode);
                    //StartCoroutine(SetCardInfos(this._ItemList[i].itemCode));
                    SetCardInfos(this._ItemList[i].itemCode);
                }
            }
            GameObject _gameObj = GameObject.Find("AdvanceWindow");
            if (_gameObj != null)
            {
                if (GameObject.Find("WoodsTheExpendablesMapList") != null && IsWoodsWindow==false)
                {
                    if (GameObject.Find("GetReward") == null && GameObject.Find("treasureOpendWindow") == null)
                    {
                        GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().OpenTreasureWindow();
                        CharacterRecorder.instance.isOpenAdvance = true;
                    }

                }
                GameObject goldEgg = GameObject.Find("GoldenEggActivity");
                if (goldEgg != null)
                {
                    GoldenEggActivityWindow goldenEggActivity = goldEgg.GetComponent<GoldenEggActivityWindow>();
                    goldenEggActivity.isBreak = true;
                    goldenEggActivity.InitBreakCount();
                }
                DestroyImmediate(_gameObj);
            }
        }
    }
    void SetCardInfos(int _RoleID)
    {
        UIManager.instance.OpenSinglePanel("CardWindow", false);
        GameObject _CardWindow = GameObject.Find("CardWindow");
        foreach (var heroItem in CharacterRecorder.instance.ownedHeroList)
        {
            CharacterRecorder.instance.heroIdList.Add(heroItem.cardID);
        }
        if (_CardWindow != null)
        {
            _CardWindow.GetComponent<CardWindow>().SetCardInfo(_RoleID);
        }

    }

    /// <summary>
    /// 丛林冒险自动点击事件
    /// </summary>
    /// <param name="IsWoods"></param>
    public void ClickSureButton(bool IsWoods)
    {
        if (IsWoods)
        {
            if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) >= 18)
            {
                if (CharacterRecorder.instance.GuideID[32] == 7)
                {
                    CharacterRecorder.instance.GuideID[32] += 1;
                }
                else if (CharacterRecorder.instance.GuideID[28] == 9)
                {
                    //CharacterRecorder.instance.GuideID[28] += 1;
                }
            }
            for (int i = 0; i < this._ItemList.Count; i++)
            {
                if (this._ItemList[i].itemCode > 60000 && this._ItemList[i].itemCode < 70000)
                {
                    Debug.Log("奖励窗口1：" + this._ItemList[i].itemCode);
                    //StartCoroutine(SetCardInfos(this._ItemList[i].itemCode));
                    SetCardInfos(this._ItemList[i].itemCode);
                }
            }
            GameObject _gameObj = GameObject.Find("AdvanceWindow");
            if (_gameObj != null)
            {
                if (GameObject.Find("WoodsTheExpendablesMapList") != null && IsWoodsWindow == false)
                {
                    if (GameObject.Find("GetReward") == null && GameObject.Find("treasureOpendWindow") == null)
                    {
                        GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().OpenTreasureWindow();
                        CharacterRecorder.instance.isOpenAdvance = true;
                    }

                }
                GameObject goldEgg = GameObject.Find("GoldenEggActivity");
                if (goldEgg != null)
                {
                    GoldenEggActivityWindow goldenEggActivity = goldEgg.GetComponent<GoldenEggActivityWindow>();
                    goldenEggActivity.isBreak = true;
                    goldenEggActivity.InitBreakCount();
                }
                DestroyImmediate(_gameObj);
            }
        }
    }
}
