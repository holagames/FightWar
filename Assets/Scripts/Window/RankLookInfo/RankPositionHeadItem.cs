using UnityEngine;
using System.Collections;

public class RankPositionHeadItem: MonoBehaviour 
{
    [SerializeField]
    private UISprite frame;
    [SerializeField]
    private UISprite icon;
    [SerializeField]
    private UISprite junXianSprite;
    [SerializeField]
    private UILabel junXuanLabel;
    [SerializeField]
    private UILabel levelLabel;
    [SerializeField]
    private GameObject gride;
    [SerializeField]
    private UISprite pinJieSprite;
	// Use this for initialization

    public GameObject ADD1;
    public GameObject ADD2;
    public GameObject ADD3;
    public GameObject RankIcon;
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    public void SetRankPositionHeadItem(Hero _oneRoleInfo)
    {
        this.name = _oneRoleInfo.characterRoleID.ToString();
        icon.name = _oneRoleInfo.characterRoleID.ToString();
        icon.spriteName = _oneRoleInfo.characterRoleID.ToString();
        levelLabel.text = _oneRoleInfo.level.ToString();
        //Debug.LogError(_oneRoleInfo.rank);//军衔
        //junXianSprite.spriteName = string.Format("rank0{0}", _oneRoleInfo.rank + 1);
        SetRankInfo(_oneRoleInfo.rank, junXianSprite, junXuanLabel);
       // GradeColor(_oneRoleInfo.classNumber);//旧的

        DestroyGride();
        int addNum = TextTranslator.instance.SetHeroNameColor(this.gameObject.GetComponent<UISprite>(), pinJieSprite, _oneRoleInfo.classNumber);
        for (int i = 0; i < addNum; i++)
        {
            GameObject obj = NGUITools.AddChild(gride, pinJieSprite.gameObject);
            obj.SetActive(true);
        }
        UIGrid _UIGrid = gride.GetComponent<UIGrid>();
        _UIGrid.sorting = UIGrid.Sorting.Horizontal;
        _UIGrid.pivot = UIWidget.Pivot.Center;
        _UIGrid.animateSmoothly = true;
 /*       frame.spriteName = string.Format("yxdi{0}", _oneRoleInfo.classNumber - 1); //"yxdi0";//品质
        if (_oneRoleInfo.classNumber > 3)
        {
            frame.spriteName = "yxdi" + (_oneRoleInfo.classNumber - 2).ToString();
        }
        for (int i = 0; i < _oneRoleInfo.classNumber; i++)
        {
            GameObject obj = NGUITools.AddChild(gride, pinJieSprite.gameObject);
            obj.SetActive(true);
            obj.GetComponent<UISprite>().spriteName = string.Format("zbkuang{0}", _oneRoleInfo.classNumber);
        }
        UIGrid _UIGrid = gride.GetComponent<UIGrid>();
        _UIGrid.sorting = UIGrid.Sorting.Horizontal;
        _UIGrid.pivot = UIWidget.Pivot.Center;
        //_UIGrid.Reposition(); 
        _UIGrid.animateSmoothly = true;*/
    }
    void DestroyGride()
    {
        for (int i = gride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(gride.transform.GetChild(i).gameObject);
        }
    }
    //设置军衔
    void SetRankInfo(int _rank, UISprite _UISprite, UILabel _myUILabel)
    {
        _UISprite.spriteName = string.Format("rank{0}", _rank.ToString("00"));
        RankIcon.transform.GetChild(_rank).gameObject.SetActive(true);
        //switch (_rank)
        //{
        //    case 1:
        //        _myUILabel.text = "下士";
        //        break;
        //    case 2:
        //        _myUILabel.text = "中士";
        //        break;
        //    case 3:
        //        _myUILabel.text = "上士";
        //        break;
        //    case 4:
        //        _myUILabel.text = "少尉";
        //        break;
        //    case 5:
        //        _myUILabel.text = "中尉";
        //        break;
        //    case 6:
        //        _myUILabel.text = "上尉";
        //        break;
        //    case 7:
        //        _myUILabel.text = "少校";
        //        break;
        //    case 8:
        //        _myUILabel.text = "中校";
        //        break;
        //    case 9:
        //        _myUILabel.text = "上校";
        //        break;
        //    case 10:
        //        _myUILabel.text = "少将";
        //        break;
        //    case 11:
        //        _myUILabel.text = "中将";
        //        break;
        //    case 12:
        //        _myUILabel.text = "上将";
        //        break;
        //    default:
        //        break;
        //}
    }
    public void SetLookInfoItem(RoleInfoForRank _oneRoleInfo)
    {
        this.name = _oneRoleInfo.roleId;
        icon.spriteName = _oneRoleInfo.roleId;
        levelLabel.text = _oneRoleInfo.roleLevel;
        //Debug.LogError(_oneRoleInfo.roleJunXian);
        junXianSprite.spriteName = string.Format("rank0{0}", int.Parse(_oneRoleInfo.roleJunXian) + 1);
        frame.spriteName = string.Format("yxdi{0}", int.Parse(_oneRoleInfo.roleColor) - 1); //"yxdi0";//品质


        //frame.spriteName = "yxdi" + (int.Parse(_oneRoleInfo.roleColor) - 2).ToString();
 /*       for (int i = 0; i < int.Parse(_oneRoleInfo.roleColor);i++ )
        {
            GameObject obj = NGUITools.AddChild(gride, pinJieSprite.gameObject);
            obj.SetActive(true);
            obj.GetComponent<UISprite>().spriteName = string.Format("zbkuang{0}",_oneRoleInfo.roleColor);
        }
        UIGrid _UIGrid = gride.GetComponent<UIGrid>();
        _UIGrid.sorting = UIGrid.Sorting.Horizontal;
        _UIGrid.pivot = UIWidget.Pivot.Center;
        //_UIGrid.Reposition(); 
        _UIGrid.animateSmoothly = true;*/
    }
    void GradeColor(int Num)
    {
        switch (Num)
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
                break;
            case 10:
                this.gameObject.GetComponent<UISprite>().spriteName = "yxdi3";
                ADD3.SetActive(true);
                ADD3.GetComponent<UISprite>().spriteName = "zbkuang4";
                ADD3.transform.Find("ADDBg1").GetComponent<UISprite>().spriteName = "zbkuang4";
                ADD3.transform.Find("ADDBg2").GetComponent<UISprite>().spriteName = "zbkuang4";
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
        }
    }
}
