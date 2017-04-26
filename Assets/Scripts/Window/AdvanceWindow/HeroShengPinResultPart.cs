using UnityEngine;
using System.Collections;

public class HeroShengPinResultPart: MonoBehaviour
{
    //-----------以下是界面Basic
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
    private UILabel nameLabel1;
    [SerializeField]
    private UILabel nameLabel2;
    [SerializeField]
    private UILabel fightLabel1;
    [SerializeField]
    private UILabel fightLabel2;
    [SerializeField]
    private UILabel fighDesLabel;
	// Use this for initialization
	void Start () 
    {
//        UIEventListener.Get(thisButton).onClick = ClickSureButton;
	}
    void OnDestroy()
    {
//        UIEventListener.Get(thisButton).onClick -= ClickSureButton;
    }
	// Update is called once per frame
	void Update () 
    {
        
	}
    public void SetHeroShengPinResultPart(Hero _hero, Hero _HeroNewData)
    {
        //Debug.LogError(_hero.force + "..." + _HeroNewData.force);
        StartCoroutine(DelayShowInfo(_hero, _HeroNewData));
        //fightLabel1.text = string.Format("战斗力：{0}", _hero.force);
        //fightLabel2.text = string.Format("{0}", _HeroNewData.force);
       
    }
    IEnumerator DelayShowInfo(Hero _hero, Hero _HeroNewData)
    {
        frame1.spriteName = "";
        frame2.spriteName = "";
        icon1.spriteName = "";
        icon2.spriteName = "";
        nameLabel1.text = "";
        nameLabel2.text = "";
        fightLabel1.text = "";
        fightLabel2.text = "";
        fighDesLabel.text = "";
        yield return new WaitForSeconds(1.0f);
        //旧的
       /* frame1.spriteName = GetFrameStr(_hero);
        frame2.spriteName = GetFrameStr(_HeroNewData.classNumber);
        icon1.spriteName = _hero.cardID.ToString();
        icon2.spriteName = _hero.cardID.ToString();
        HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(_hero.cardID);
        nameLabel1.text = hinfo.heroName;
        nameLabel2.text = hinfo.heroName;
        SetNameColor(nameLabel1, _hero.classNumber);
        SetNameColor(nameLabel2, _HeroNewData.classNumber);*/
        icon1.spriteName = _hero.cardID.ToString();
        icon2.spriteName = _hero.cardID.ToString();
        HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(_hero.cardID);
        TextTranslator.instance.SetHeroNameColor(nameLabel1, frame1, hinfo.heroName, _hero.classNumber);
        TextTranslator.instance.SetHeroNameColor(nameLabel2, frame2, hinfo.heroName, _HeroNewData.classNumber);
        yield return new WaitForSeconds(0.2f);
        //Debug.LogError(_hero.force + ".._HeroNewData.." + _HeroNewData.force);
        fighDesLabel.text = "战斗力：";
  //      StartCoroutine(AddFightNum(fightLabel1, _hero.force));
        fightLabel1.text = _hero.force.ToString();
        StartCoroutine(AddFightNum(fightLabel2, _HeroNewData.force));
    }
    //设置名字颜色
    void SetNameColor(UILabel _nameLabel, int classNumber)
    {
        switch (classNumber)
        {
            case 1:
                _nameLabel.color = Color.white;
                break;
            case 2:
                _nameLabel.text = "[3ee817]" + _nameLabel.text + "[-]";
                break;
            case 3:
                _nameLabel.text = "[3ee817]" + _nameLabel.text + "+1[-]";
                break;
            case 4:
                _nameLabel.text = "[249bd2]" + _nameLabel.text + "[-]";
                break;
            case 5:
                _nameLabel.text = "[249bd2]" + _nameLabel.text + "+1[-]";
                break;
            case 6:
                _nameLabel.text = "[249bd2]" + _nameLabel.text + "+2[-]";
                break;
            case 7:
                _nameLabel.text = "[bb44ff]" + _nameLabel.text + "[-]";
                break;
            case 8:
                _nameLabel.text = "[bb44ff]" + _nameLabel.text + "+1[-]";
                break;
            case 9:
                _nameLabel.text = "[bb44ff]" + _nameLabel.text + "+2[-]";
                break;
            case 10:
                _nameLabel.text = "[bb44ff]" + _nameLabel.text + "+3[-]";
                break;
            case 11:
                _nameLabel.text = "[ff8c04]" + _nameLabel.text + "[-]";
                break;
            case 12:
                _nameLabel.text = "[ff8c04]" + _nameLabel.text + "+1[-]";
                break;
            default:
                break;
        }
    }
    IEnumerator AddFightNum(UILabel label, int MaxFightNum)
    {
        //Debug.Log("MaxFightNum:    " + MaxFightNum);
        label.text = "";
        yield return new WaitForSeconds(0.5f);
        int count = 0;
        while (count < MaxFightNum)
        {
            count += 100;
            yield return new WaitForSeconds(0.01f);
            if (count >= MaxFightNum)
            {
                count = MaxFightNum;
            }
            label.text = string.Format("{0}", count);
        }
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
        if (go != null)
        {
            Debug.Log("Click this button");
          //  UIManager.instance.BackUI();
            GameObject _gameObj = GameObject.Find("AdvanceWindow");
            if (_gameObj != null)
            {
                DestroyImmediate(_gameObj);
            }
        }
    }
}
