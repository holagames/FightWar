using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class LuxuryCarWindow : MonoBehaviour
{

    public GameObject CarItem;
    public GameObject LeftButton;
    public GameObject RightButton;
    public GameObject CarGrid;
    public int NowID = 1;
    public int HeroID = 0;
    public List<GameObject> CarList = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        UIEventListener.Get(LeftButton).onClick += delegate(GameObject go)
        {
            if (NowID >= 2)
            {
                MoveCarItem(NowID - 1);
            }
        };
        UIEventListener.Get(RightButton).onClick += delegate(GameObject go)
        {
            if (NowID <= 5)
            {
                MoveCarItem(NowID + 1);
            }
        };
        MoveCarItem(1);
    }

    public void HeroIDInfo(Hero mCurHero)
    {
        HeroID = mCurHero.characterRoleID;
    }
    void MoveCarItem(int num)
    {
        NowID = num;
        switch (num)
        {
            case 1:
                LeftButton.SetActive(false);
                RightButton.SetActive(true);
                break;
            case 2:
            case 3:
            case 4:
            case 5:
                LeftButton.SetActive(true);
                RightButton.SetActive(true);
                break;
            case 6:
                LeftButton.SetActive(true);
                RightButton.SetActive(false);
                break;
            default:
                break;
        }

        GameObject.Find("LuxuryCarWindow/Scroll View").GetComponent<UIPanel>().clipOffset = new Vector3(1000 * (num - 1), 0, 0);
        GameObject.Find("LuxuryCarWindow/Scroll View").transform.localPosition = new Vector3(1000 * (num - 1) * -1, 63, 0) + new Vector3(-175, 0, 0);
    }

    public void SetCarInfo(string[] dataSplit)
    {
        for (int i = 0; i < CarList.Count; i++)
        {
            DestroyImmediate(CarList[i]);
        }
        CarList.Clear();
        for (int i = 0; i < dataSplit.Length - 2; i++)
        {
            string[] caritem = dataSplit[i].Split('$');
            GameObject go = Instantiate(CarItem) as GameObject;
            go.transform.parent = CarGrid.transform;
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.transform.localScale = new Vector3(1, 1, 1);
            int id = 42001 + i;
            go.name = id.ToString();
            go.GetComponent<SuperCarItem>().SetInfo(id, int.Parse(caritem[1]), HeroID, i);
            CarList.Add(go);
        }
        CarGrid.GetComponent<UIGrid>().repositionNow = true;
        StartCoroutine(DelayEffect());
    }

    IEnumerator DelayEffect()
    {
        yield return new WaitForSeconds(0.1f);
        for (int i = 0; i < CarList.Count; i++)
        {
            CarList[i].SetActive(true);
        }
    }
}
