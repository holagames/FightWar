using UnityEngine;
using System.Collections;

public class LookInfoItem : MonoBehaviour 
{
    [SerializeField]
    private UISprite frame;
    [SerializeField]
    private UISprite icon;
    [SerializeField]
    private UISprite junXianSprite;
    [SerializeField]
    private UILabel levelLabel;
    [SerializeField]
    private UILabel junXianLabel;
    [SerializeField]
    private GameObject gride;
    [SerializeField]
    private UISprite pinJieSprite;
    private int addNum;
	// Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    public void SetLookInfoItem(RoleInfoForRank _oneRoleInfo)
    {
        this.name = _oneRoleInfo.roleId;
        icon.spriteName = _oneRoleInfo.roleId;
        levelLabel.text = _oneRoleInfo.roleLevel;
        //Debug.LogError(_oneRoleInfo.roleJunXian);
       // junXianSprite.spriteName = string.Format("rank0{0}", int.Parse(_oneRoleInfo.roleJunXian) + 1);
        SetRankIcon(int.Parse(_oneRoleInfo.roleJunXian));
       // frame.spriteName = string.Format("yxdi{0}",int.Parse(_oneRoleInfo.roleColor) - 1); //"yxdi0";//品质
        //旧的
    /*    GradeColor(int.Parse(_oneRoleInfo.roleColor));
        for (int i = 0; i < addNum; i++)
        {
            GameObject obj = NGUITools.AddChild(gride, pinJieSprite.gameObject);
            obj.SetActive(true);
        }
        UIGrid _UIGrid = gride.GetComponent<UIGrid>();
        _UIGrid.sorting = UIGrid.Sorting.Horizontal;
        _UIGrid.pivot = UIWidget.Pivot.Center;
        _UIGrid.animateSmoothly = true; */
        DestroyGride();
        int addNum = TextTranslator.instance.SetHeroNameColor(frame, pinJieSprite, int.Parse(_oneRoleInfo.roleColor));
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
    //设置军衔
    void SetRankIcon(int _rankNum)
    {
        junXianSprite.GetComponent<UISprite>().spriteName = string.Format("rank{0}", _rankNum.ToString("00"));
        switch (_rankNum)
        {
            case 1:
                junXianLabel.text = "下士";
                break;
            case 2:
                junXianLabel.text = "中士";
                break;
            case 3:
                junXianLabel.text = "上士";
                break;
            case 4:
                junXianLabel.text = "少尉";
                break;
            case 5:
                junXianLabel.text = "中尉";
                break;
            case 6:
                junXianLabel.text = "上尉";
                break;
            case 7:
                junXianLabel.text = "少校";
                break;
            case 8:
                junXianLabel.text = "中校";
                break;
            case 9:
                junXianLabel.text = "上校";
                break;
            case 10:
                junXianLabel.text = "少将";
                break;
            case 11:
                junXianLabel.text = "中将";
                break;
            case 12:
                junXianLabel.text = "上将";
                break;
            default:
                break;
        }
    }
    void GradeColor(int Num)
    {
        switch (Num)
        {
            case 1:
                frame.spriteName = "yxdi0";
                break;
            case 2:
                frame.spriteName = "yxdi1";
                break;
            case 3:
                frame.spriteName = "yxdi1";
                addNum = 1;
                pinJieSprite.spriteName = "zbkuang2";
                break;
            case 4:
                frame.spriteName = "yxdi2";
                break;
            case 5:
                frame.spriteName = "yxdi2";
                addNum = 1;
                pinJieSprite.spriteName = "zbkuang3";
                break;
            case 6:
                frame.spriteName = "yxdi2";
                addNum = 2;
                pinJieSprite.spriteName = "zbkuang3";
                break;
            case 7:
                frame.spriteName = "yxdi3";
                break;
            case 8:
                frame.spriteName = "yxdi3";
                addNum = 1;
                pinJieSprite.spriteName = "zbkuang4";
                break;
            case 9:
                frame.spriteName = "yxdi3";
                addNum = 2;
                pinJieSprite.spriteName = "zbkuang4";
                break;
            case 10:
                frame.spriteName = "yxdi3";
                addNum = 3;
                pinJieSprite.spriteName = "zbkuang4";
                break;
            case 11:
                frame.spriteName = "yxdi4";
                break;
            case 12:
                frame.spriteName = "yxdi4";
                addNum = 1;
                pinJieSprite.spriteName = "zbkuang5";
                break;
            default:
                break;
        }
    }
}
