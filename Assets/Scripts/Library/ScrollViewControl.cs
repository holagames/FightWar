using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ScrollViewControl : MonoBehaviour
{
    List<GameObject> ListBag = new List<GameObject>();
    public GameObject BagWindow;
    public GameObject Grid;
    float num;
    int sds ;
    float CellHeight;
    int LineNum;
    // Use this for initialization
    void Start()
    {
        sds = 2;
        CellHeight = Grid.GetComponent<UIGrid>().cellHeight;
        LineNum = Grid.GetComponent<UIGrid>().maxPerLine;
        foreach (Transform item in Grid.transform)
        {
            ListBag.Add(item.gameObject);
        }
    }
    void OnEnable()
    {
        if (sds>2)
        {
            foreach (Transform item in Grid.transform)
            {
                ListBag.Add(item.gameObject);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        num = this.transform.localPosition.y;
        set(num);
    }
    void set(float num)
    {
        int n = (int)(num / CellHeight);
        for (int i = 0; i < ListBag.Count; i++)
        {
           
                if (n * LineNum <= i && i < n * LineNum + 44)
                {
                   ListBag[i].SetActive(true);
                }
                else
            {
                
                    ListBag[i].SetActive(false);
               
            }

        }

    }
    void OnDisable()
    {
        sds++;
        ListBag.Clear();
    }
}
