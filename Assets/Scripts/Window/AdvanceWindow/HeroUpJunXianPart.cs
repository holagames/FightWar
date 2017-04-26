using UnityEngine;
using System.Collections;
using System;

public class HeroUpJunXianPart:MonoBehaviour
{
    [SerializeField]
    private UIAtlas commanAtlas;
    [SerializeField]
    private UIAtlas uiAddAtlas;
    //-----------以下是界面Basic
    [SerializeField]
    private GameObject bgSpriteObj;
    [SerializeField]
    private GameObject titleComanObj;
    [SerializeField]
    private GameObject commanButton;
    [SerializeField]
    private GameObject thisButton;
    //-----------以下是数据Info
    [SerializeField]
    private UISprite frame1;
    [SerializeField]
    private UISprite frame2;
    [SerializeField]
    private UISprite icon1;
    [SerializeField]
    private UISprite icon2;
    [SerializeField]
    private UISprite rankIcon1;
    [SerializeField]
    private UISprite rankIcon2;
    public GameObject rankObj1;
    public GameObject rankObj2;
    [SerializeField]
    private UILabel nameLabel1;
    [SerializeField]
    private UILabel nameLabel2;
    [SerializeField]
    private UILabel fightLabel1;
    [SerializeField]
    private UILabel fightLabel2;
    [SerializeField]
    private UILabel desLabel;
    [SerializeField]
    private UILabel talentNameLabel;
	// Use this for initialization
	void Start () 
    {
        UIEventListener.Get(thisButton).onClick = ClickSureButton;
	}
    void OnDestroy()
    {
        UIEventListener.Get(thisButton).onClick -= ClickSureButton;
    }
	// Update is called once per frame
	void Update () 
    {
	
	}
    public void SetHeroUpJunXianPart(Hero _hero, Hero _HeroNewData)
    {
      //  bgSprite.atlas = uiAddAtlas;
      //  bgSprite.spriteName = "di2";
        bgSpriteObj.SetActive(false);
        titleComanObj.SetActive(false);
        commanButton.SetActive(false);
        thisButton.SetActive(false);
        StartCoroutine(DelayShowInfo(_hero, _HeroNewData));
     //   fightLabel1.text = string.Format("战斗力：{0}", _hero.force);
     //   fightLabel1.text = string.Format("{0}", _hero.force);
     //   fightLabel2.text = string.Format("{0}", _HeroNewData.force);
        
    }
    IEnumerator DelayShowInfo(Hero _hero, Hero _HeroNewData)
    {
        frame1.spriteName = "";
        frame2.spriteName = "";
        icon1.spriteName = "";
        icon2.spriteName = "";
        rankIcon1.spriteName = "";
        rankIcon2.spriteName = "";
        nameLabel1.text = "";
        nameLabel2.text = "";
        fightLabel1.text = "";
        fightLabel2.text = "";
        desLabel.text = "";
        talentNameLabel.text = "";
        yield return new WaitForSeconds(1.0f);
        //旧的
       /* frame1.spriteName = GetFrameStr(_hero);
        frame2.spriteName = GetFrameStr(_HeroNewData.classNumber);*/
        TextTranslator.instance.SetHeroNameColor(frame1, _hero.classNumber);
        TextTranslator.instance.SetHeroNameColor(frame2, _HeroNewData.classNumber);
        icon1.spriteName = _hero.cardID.ToString();
        icon2.spriteName = _hero.cardID.ToString();
      //  SetRankInfo(_hero.rank, rankIcon1, nameLabel1);
      //  SetRankInfo(_HeroNewData.rank, rankIcon2, nameLabel2);
        rankObj1.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(_hero.rank);
        rankObj2.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(_HeroNewData.rank);
        yield return new WaitForSeconds(0.2f);
        desLabel.text = "战斗力：";
    //    StartCoroutine(AddFightNum(fightLabel1, _hero.force));
        fightLabel1.text = _hero.force.ToString();
        //StartCoroutine(AddFightNum(fightLabel2, _HeroNewData.force));
        StartCoroutine(AddFightNumInConfirmTime(fightLabel2, _HeroNewData.force));
        yield return new WaitForSeconds(0.5f);
        talentNameLabel.text = GetActiveTalentName(_HeroNewData);
        yield return new WaitForSeconds(1.0f);
  //      thisButton.SetActive(true);
    }
    IEnumerator AddFightNum(UILabel label, int MaxFightNum)
    {
        //Debug.Log("MaxFightNum:    " + MaxFightNum);
        label.text = "";
        int count = 0;
        while (count < MaxFightNum)
        {
            count += 50;
            yield return new WaitForSeconds(0.01f);
            if (count >= MaxFightNum)
            {
                count = MaxFightNum;
            }
            label.text = string.Format("{0}",count);
        }
    }
    IEnumerator AddFightNumInConfirmTime(UILabel label, int MaxFightNum)
    {
        //Debug.Log("MaxFightNum:    " + MaxFightNum);
        label.text = "";
        int count = 0;
        while (count < MaxFightNum)
        {
            count += (int)Math.Ceiling(Convert.ToDouble(MaxFightNum / 20.0f));
            yield return new WaitForSeconds(0.01f);
            if (count >= MaxFightNum)
            {
                count = MaxFightNum;
            }
            label.text = string.Format("{0}", count);
        }
    }
    //设置军衔
    void SetRankInfo(int _RankJunXian, UISprite _RankIcon,UILabel _RankLabel)
    {
        _RankIcon.GetComponent<UISprite>().spriteName = string.Format("rank{0}", _RankJunXian.ToString("00"));
        switch (_RankJunXian)
        {
            case 1:
                _RankLabel.text = "下士";
                break;
            case 2:
                _RankLabel.text = "中士";
                break;
            case 3:
                _RankLabel.text = "上士";
                break;
            case 4:
                _RankLabel.text = "少尉";
                break;
            case 5:
                _RankLabel.text = "中尉";
                break;
            case 6:
                _RankLabel.text = "上尉";
                break;
            case 7:
                _RankLabel.text = "少校";
                break;
            case 8:
                _RankLabel.text = "中校";
                break;
            case 9:
                _RankLabel.text = "上校";
                break;
            case 10:
                _RankLabel.text = "少将";
                break;
            case 11:
                _RankLabel.text = "中将";
                break;
            case 12:
                _RankLabel.text = "上将";
                break;
            default:
                break;
        }
    }
    string GetActiveTalentName(Hero _myHero)
    {
        _myHero.heroTalentList = TextTranslator.instance.GetHeroTalentListByHeroID(_myHero.cardID);
        for (int i = 0; i < _myHero.heroTalentList.Count; i++)
        {
            if (_myHero.rank == _myHero.heroTalentList[i].BreachLevel)
            {
                return string.Format("{0}", _myHero.heroTalentList[i].Name);//[74e5f3]
            }
        }
        return "";
    }
    string GetFrameStr(Hero _hero)
    {
        string _frameStr = "";
        switch (_hero.classNumber)
        {
            case 1:
                _frameStr = "yxdi0";
                break;
            case 2:
                _frameStr = "yxdi1";
                break;
            case 3:
                _frameStr = "yxdi1";
                //ADD1.SetActive(true);
                break;
            case 4:
                _frameStr = "yxdi2";
                break;
            case 5:
                _frameStr = "yxdi2";
                //ADD1.SetActive(true);
                break;
            case 6:
                _frameStr = "yxdi2";
                //ADD2.SetActive(true);
                break;
            case 7:
                _frameStr = "yxdi3";
                break;
            case 8:
                _frameStr = "yxdi3";
                //ADD1.SetActive(true);
                break;
            case 9:
                _frameStr = "yxdi3";
                //ADD2.SetActive(true);
                break;
            case 10:
                _frameStr = "yxdi3";
                //ADD3.SetActive(true);
                break;
            case 11:
                _frameStr = "yxdi4";
                break;
            case 12:
                _frameStr = "yxdi4";
                //ADD1.SetActive(true);
                break;
            default: _frameStr = "yxdi0";
                break;
        }
        return _frameStr;
    }
    string GetFrameStr(int _classNumber)
    {
        string _frameStr = "";
        switch (_classNumber)
        {
            case 1:
                _frameStr = "yxdi0";
                break;
            case 2:
                _frameStr = "yxdi1";
                break;
            case 3:
                _frameStr = "yxdi1";
                //ADD1.SetActive(true);
                break;
            case 4:
                _frameStr = "yxdi2";
                break;
            case 5:
                _frameStr = "yxdi2";
                //ADD1.SetActive(true);
                break;
            case 6:
                _frameStr = "yxdi2";
                //ADD2.SetActive(true);
                break;
            case 7:
                _frameStr = "yxdi3";
                break;
            case 8:
                _frameStr = "yxdi3";
                //ADD1.SetActive(true);
                break;
            case 9:
                _frameStr = "yxdi3";
                //ADD2.SetActive(true);
                break;
            case 10:
                _frameStr = "yxdi3";
                //ADD3.SetActive(true);
                break;
            case 11:
                _frameStr = "yxdi4";
                break;
            case 12:
                _frameStr = "yxdi4";
                //ADD1.SetActive(true);
                break;
            default: _frameStr = "yxdi0";
                break;
        }
        return _frameStr;
    }
    void ClickSureButton(GameObject go)
    {
        GameObject _gameObj = GameObject.Find("AdvanceWindow");
        if (_gameObj != null)
        {
            DestroyImmediate(_gameObj);
        }
    }
}
