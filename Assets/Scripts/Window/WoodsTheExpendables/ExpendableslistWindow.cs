using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ExpendableslistWindow : MonoBehaviour
{

    public GameObject RankListItem;
    public GameObject BackButton;
    private GameObject WoodsTheExpendables;
    public UILabel SelfRank;
    public UILabel SelfName;
    public UILabel SelfScore;
    public UILabel SelfGate;
    public UIGrid Grid;
    public List<GameObject> ItemList = new List<GameObject>();
    public bool isInRank = false;
    // Use this for initialization
    void Start()
    {
        if (GameObject.Find("WoodsTheExpendables") != null)
        {
            WoodsTheExpendables = GameObject.Find("WoodsTheExpendables");
        }
        if (UIEventListener.Get(BackButton).onClick == null)
        {
            UIEventListener.Get(BackButton).onClick += delegate(GameObject obj)
            {
                WoodsTheExpendables.GetComponent<WoodsTheExpendables>().UpdateWindow(WoodsTheExpendablesWindowType.Right);
            };
        }
        //SetMyselfInfo();
    }


    public void ShowList(string[] dataSplit)
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            DestroyImmediate(ItemList[i]);
        }
        ItemList.Clear();
        isInRank = false;
        for (int i = 0; i < dataSplit.Length - 1; i++)
        {
            string[] secSplit = dataSplit[i].Split('$');
            if (GameObject.Find("ExpendableslistWindow") != null)
            {
                if (secSplit[2] == CharacterRecorder.instance.characterName)
                {
                    isInRank = true;
                    SetMyselfInfo(int.Parse(secSplit[0]));
                }
                SetListInfo(int.Parse(secSplit[0]), int.Parse(secSplit[1]), secSplit[2], int.Parse(secSplit[3]), int.Parse(secSplit[4]), int.Parse(secSplit[5]), int.Parse(secSplit[6]));
            }
        }
        if (isInRank == false)
        {
            SetMyselfInfo(100);
        }

    }
    public void SetListInfo(int Rank, int HeroID, string Name, int ItemID, int Level, int Score, int Floor)
    {
        GameObject item = Instantiate(RankListItem)as GameObject;
        item.transform.parent = Grid.transform;
        item.transform.localScale = Vector3.one;
        item.transform.localPosition = Vector3.zero;
        ItemList.Add(item);
        item.GetComponent<WoodsListItem>().Initem(Rank, HeroID, Name, ItemID, Level, Score, Floor);
        Grid.Reposition();
    }

    public void SetMyselfInfo(int Rank)
    {
        if (Rank > 50 || Rank == 0)
        {
            SelfRank.text = "未上榜";
        }
        else
        {
            SelfRank.text = Rank.ToString();
        }
        SelfGate.text = CharacterRecorder.instance.HistoryFloor.ToString();
        SelfName.text = CharacterRecorder.instance.characterName.ToString();
        SelfScore.text = GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().CurIntegral.ToString();
    }
}
