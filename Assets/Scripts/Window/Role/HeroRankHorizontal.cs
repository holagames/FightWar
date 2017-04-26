using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroRankHorizontal: MonoBehaviour 
{
    public List<GameObject> starObjList = new List<GameObject>();


    [SerializeField]
    private GameObject gride;
    [SerializeField]
    private UISprite pinJieSprite;

    public void SetHeroRankHorizontal(int _TargetHeroRank)
    {
        for (int i = 0; i < starObjList.Count;i++ )
        {
            if (i + 1 == _TargetHeroRank)
            {
                starObjList[i].SetActive(true);
                if (starObjList[i].GetComponent<UIGrid>() != null)
                {
                    if (_TargetHeroRank==6||_TargetHeroRank==12)
                    {
                        starObjList[i].GetComponent<UIGrid>().cellWidth = 25;
                    }
                    else
                    {
                        starObjList[i].GetComponent<UIGrid>().cellWidth = 30;
                    }
                    
                }
            }
            else
            {
                starObjList[i].SetActive(false);
            }
        }
    }

    //军衔 几颗星  动态生成几颗星
  /*  public void SetHeroRankHorizontal(int _TargetHeroRank)
    {
        CleraUIGride();
        int addNum = _TargetHeroRank;
        for (int i = 0; i < addNum; i++)
        {
            GameObject obj = NGUITools.AddChild(gride, pinJieSprite.gameObject);
            obj.SetActive(true);
        }
        UIGrid _UIGrid = gride.GetComponent<UIGrid>();
        _UIGrid.sorting = UIGrid.Sorting.Horizontal;
        _UIGrid.pivot = UIWidget.Pivot.Center;
        _UIGrid.animateSmoothly = true;
        _UIGrid.repositionNow = true;

        SortStarDepth();
    }

    void SortStarDepth()
    {
        for (int i = 0; i < gride.transform.childCount; i++)
        {
            gride.transform.GetChild(i).GetComponent<UISprite>().depth = 50 + i;
        }
    }
    void CleraUIGride()
    {
        for (int i = gride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(gride.transform.GetChild(i).gameObject);
        }
    }*/
}
