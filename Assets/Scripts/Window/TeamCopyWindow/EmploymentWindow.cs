using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class EmploymentWindow : MonoBehaviour
{
    public GameObject HeroItem;
    public UIGrid Grid;
    public GameObject CancleButton;
    public List<GameObject> ItemList=new List<GameObject>();
    int number = 0;
    // Use this for initialization
    void Start()
    {
        UIEventListener.Get(CancleButton).onClick += delegate(GameObject go)
        {
            gameObject.SetActive(false);
        };
        for (int i = 0; i < ItemList.Count; i++)
        {
            DestroyImmediate(ItemList[i]);
        }
        ItemList.Clear();
    }

    public void SetInfo(string RankType, string RankNumber, string uId, string Name, string HeroIcon, string HeroLevel, string PowerNumber, string JiFen, string sumPraise, string LegionName)
    {
        if (ItemList.Count<10)
        {
            GameObject go = NGUITools.AddChild(Grid.transform.gameObject, HeroItem);
            go.SetActive(true);
            go.GetComponent<EmploymentItem>().SetInfo(RankType, RankNumber, uId, Name, HeroIcon, HeroLevel, PowerNumber);
            Grid.Reposition();
            ItemList.Add(go);
        }
    }

}
