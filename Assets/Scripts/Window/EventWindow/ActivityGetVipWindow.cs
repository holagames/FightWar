using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ActivityGetVipWindow : MonoBehaviour {
    public GameObject Item;
    public UIGrid ItemGrid;
    public List<GameObject> ItemList = new List<GameObject>();
    public UILabel DayNumberLabel;
	// Use this for initialization
	void Start () {
        
	}
	
    public void  SetInfo(int DayNumber,List<int> ItemStyle){
        for (int i = 0; i < ItemList.Count; i++)
        {
            DestroyImmediate(ItemList[i]);
        }
        ItemList.Clear();
        //int index = 0;
        for (int i = 0; i < ItemStyle.Count; i++)
        {
            if (ItemStyle[i] != 2)
            {
                GameObject go = Instantiate(Item) as GameObject;
                go.SetActive(true);
                go.transform.parent = ItemGrid.transform;
                go.transform.localScale = new Vector3(1, 1, 1);
                go.transform.localPosition = new Vector3(0, 0, 0);
                go.GetComponent<BonusVipItem>().SetInfo(i + 1, DayNumber, ItemStyle[i]);
                //if (ItemStyle[i] == 2)
                //{
                //    index = i + 1;
                //}
                ItemList.Add(go);
            }
        }
        //ItemGrid.transform.parent.GetComponent<UIPanel>().clipOffset=new Vector3(0,0-136*index,0);
        //ItemGrid.transform.parent.localPosition = new Vector3(171, -28+136*index, 0);
        ItemGrid.repositionNow = true;
        DayNumberLabel.text = DayNumber.ToString();
    }
}
