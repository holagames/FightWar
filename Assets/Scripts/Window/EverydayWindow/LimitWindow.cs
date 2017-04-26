using UnityEngine;
using System.Collections;

public class LimitWindow : MonoBehaviour {

    public GameObject FightingButton;
    public GameObject HreoItem;
    public GameObject BattleHeroItem;
    public UIGrid HreoGrid;
    public UIGrid BattleGrid;
    private int BattleNumber = 0;
	// Use this for initialization
	void Start () {
        if (UIEventListener.Get(FightingButton).onClick == null)
        {
            UIEventListener.Get(FightingButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPromptWindow("即将开放", PromptWindow.PromptType.Alert, null, null);
            };
        }
        for (int i = 0; i < 10; i++)
        {
            GetHeroInfo(i, 60001 + i);
        }
	}
	
	// Update is called once per frame
    public void GetHeroInfo(int GradeID, int ItemID)
    {
        GameObject item = NGUITools.AddChild(HreoGrid.gameObject, HreoItem);
        item.GetComponent<UISprite>().spriteName = "yxdi"+GradeID.ToString();
        item.transform.Find("Item").GetComponent<UISprite>().spriteName = ItemID.ToString();
        item.SetActive(true);     
        if (UIEventListener.Get(item).onClick == null)
        {
            UIEventListener.Get(item).onClick += delegate(GameObject go)
            {
                
                if (BattleNumber <5)
                {
                    BattleNumber++;
                    Debug.Log("删除上阵"+BattleNumber);
                    Destroy(item);
                    GetBattleInfo(GradeID, ItemID);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("已经上阵五个英雄", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        HreoGrid.GetComponent<UIGrid>().Reposition();
        BattleGrid.GetComponent<UIGrid>().Reposition();
    }

    public void GetBattleInfo(int GradeID, int ItemID)
    {
        GameObject item = NGUITools.AddChild(BattleGrid.gameObject, BattleHeroItem);
        item.GetComponent<UISprite>().spriteName = "yxdi"+GradeID.ToString();
        item.transform.Find("Item").GetComponent<UISprite>().spriteName = ItemID.ToString();
        item.SetActive(true);
        if (UIEventListener.Get(item).onClick == null)
        {
            UIEventListener.Get(item).onClick += delegate(GameObject go)
            {
                BattleNumber--;
                Debug.Log("删除下阵"+BattleNumber);
                GetHeroInfo(GradeID, ItemID);
                Destroy(item);
            };
        }
        HreoGrid.GetComponent<UIGrid>().Reposition();
        BattleGrid.GetComponent<UIGrid>().Reposition();
    }
}
