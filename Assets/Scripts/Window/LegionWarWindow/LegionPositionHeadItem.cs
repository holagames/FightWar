using UnityEngine;
using System.Collections;

public class LegionPositionHeadItem : MonoBehaviour
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

    [SerializeField]
    private UISprite ManyStateSprite;

    public GameObject ADD1;
    public GameObject ADD2;
    public GameObject ADD3;

    public GameObject RankIcon;

    public void SetRankPositionHeadItem(Hero _oneRoleInfo)
    {
        this.name = _oneRoleInfo.characterRoleID.ToString();
        icon.name = _oneRoleInfo.characterRoleID.ToString();
        icon.spriteName = _oneRoleInfo.characterRoleID.ToString();
        levelLabel.text = _oneRoleInfo.level.ToString();
        SetRankInfo(_oneRoleInfo.rank, junXianSprite, junXuanLabel);

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
    }
    public void SetLookInfoItem(RoleInfoForRank _oneRoleInfo)
    {
        this.name = _oneRoleInfo.roleId;
        icon.spriteName = _oneRoleInfo.roleId;
        levelLabel.text = _oneRoleInfo.roleLevel;
        //Debug.LogError(_oneRoleInfo.roleJunXian);
        junXianSprite.spriteName = string.Format("rank0{0}", int.Parse(_oneRoleInfo.roleJunXian) + 1);
        frame.spriteName = string.Format("yxdi{0}", int.Parse(_oneRoleInfo.roleColor) - 1); //"yxdi0";//品质

    }

    public void SetItemState(int num) //状态，0死亡，1活着
    {
        ManyStateSprite.gameObject.SetActive(true);
        this.gameObject.transform.Find(this.gameObject.name).GetComponent<BoxCollider>().enabled = false;

        if (num == 0)
        {
            if (GameObject.Find("LegionWarWindow") != null)
            {
                ManyStateSprite.spriteName = "ui_shengping_icon2";
                UIEventListener.Get(this.gameObject).onClick = delegate(GameObject go)
                {
                    UIManager.instance.OpenPromptWindow("不可重复上阵", PromptWindow.PromptType.Hint, null, null);
                };
            }
            else
            {
                ManyStateSprite.spriteName = "ui_shengping_icon2";
                UIEventListener.Get(this.gameObject).onClick = delegate(GameObject go)
                {
                    if (ManyStateSprite.gameObject.activeSelf == true)
                    {                       
                        NetworkHandler.instance.SendProcess("8607#" + this.gameObject.name + ";");
                    }
                };
            }
        }
        else 
        {
            if (GameObject.Find("LegionWarWindow") != null)
            {
                ManyStateSprite.spriteName = "ui_shengping_icon1";
                UIEventListener.Get(this.gameObject).onClick = delegate(GameObject go)
                {
                    UIManager.instance.OpenPromptWindow("不可重复上阵", PromptWindow.PromptType.Hint, null, null);
                };
            }
            else
            {
                ManyStateSprite.spriteName = "ui_shengping_icon1";
                UIEventListener.Get(this.gameObject).onClick = delegate(GameObject go)
                {
                    if (ManyStateSprite.gameObject.activeSelf == true)
                    {
                        UIManager.instance.OpenPromptWindow("不可重复上阵", PromptWindow.PromptType.Hint, null, null);
                    }
                };
            }
        }
    }

    public void GetHeroRevive()//复活成功 
    {
        ManyStateSprite.gameObject.SetActive(false);
        this.gameObject.transform.Find(this.gameObject.name).GetComponent<BoxCollider>().enabled = true;
    }
}
