using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectItemBoard : MonoBehaviour {

    public GameObject SureButton;
    public GameObject ExitButton;
    public GameObject uiGrid;
    public GameObject ItemPrafeb;

    [HideInInspector]
    public List<GameObject> PrafebList = new List<GameObject>();
	// Use this for initialization
	void Start () {
	
	}

    public void StartPart()
    {
        UIEventListener.Get(SureButton).onClick = delegate(GameObject go)
        {
            this.gameObject.SetActive(false);
        };

        UIEventListener.Get(ExitButton).onClick = delegate(GameObject go)
        {
            this.gameObject.SetActive(false);
        };

        for (int i = 0; i < 8; i++)
        {
            GameObject obj = NGUITools.AddChild(uiGrid, ItemPrafeb);
            obj.GetComponent<StrengerSelectItem>().SetInfo("宝物 " + i, "宝物经验+300");
            PrafebList.Add(obj);
        }
        
        
        
    }
}
