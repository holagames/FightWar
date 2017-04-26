using UnityEngine;
using System.Collections;

public class RebirthHeroItem : MonoBehaviour
{
    public GameObject heroItemKuang;
    public GameObject headSprite;
    public GameObject rankIcon;
    public GameObject selectIcon;
    public UILabel level;
    public UISprite pinjieSprite;
    public GameObject myGrid;
    /// <summary>
    /// 设置重生角色框的信息
    /// </summary>
    /// <param name="heroId"></param>
    public void SetRebirthHeroIntemInfo(int heroId)
    {
        Hero hero = CharacterRecorder.instance.GetHeroByRoleID(heroId);
        //设置头像图片
        //Debug.LogError("角色ID： " + gameObject.name);
        headSprite.GetComponent<UISprite>().spriteName = gameObject.name;

        //设置军衔
        rankIcon.transform.GetChild(hero.rank).gameObject.SetActive(true);
        level.text = hero.level.ToString();
        //设置品阶图像       
        int addNum = TextTranslator.instance.SetHeroNameColor(this.gameObject.GetComponent<UISprite>(), pinjieSprite, hero.classNumber);
        DestroyGridItem(myGrid);
        for (int i = 0; i < addNum; i++)
        {
            GameObject obj = NGUITools.AddChild(myGrid, pinjieSprite.gameObject);
            obj.SetActive(true);
        }
        UIGrid _UIGrid = myGrid.GetComponent<UIGrid>();
        _UIGrid.sorting = UIGrid.Sorting.Horizontal;
        _UIGrid.pivot = UIWidget.Pivot.Center;
        _UIGrid.animateSmoothly = true;
        if (gameObject.GetComponent<UIToggle>() == null)
        {
            gameObject.AddComponent<UIToggle>();
        }
        UIToggle checkStatus = gameObject.GetComponent<UIToggle>();
        //给控件绑定选择及取消选择事件
        EventDelegate.Add(checkStatus.onChange, UIToggleOnChanged);
    }

    public void DestroyGridItem(GameObject grid)
    {
        for (int i = 0; i < grid.transform.childCount; i++)
        {
            DestroyImmediate(grid.transform.GetChild(i).gameObject);
        }
    }
    /// <summary>
    /// UIToggle点击改变的时候触发事件
    /// </summary>
    public void UIToggleOnChanged()
    {
        int roleId = 0;
        if (UIToggle.current.value)
        {
            selectIcon.SetActive(true);
            roleId = int.Parse(UIToggle.current.name);
        }
        else
        {
            selectIcon.SetActive(false);
        }
        GameObject rebirthWindow = GameObject.Find("RebirthWindow");
        if (rebirthWindow != null && roleId != 0)
        {
            RebirthWindow window = rebirthWindow.GetComponent<RebirthWindow>();
            CharacterRecorder.instance.RebirthRoleId = roleId;
            window.SetRoleInfoPanel(roleId);
        }
    }
}
