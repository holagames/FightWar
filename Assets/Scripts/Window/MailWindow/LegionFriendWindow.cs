using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionFriendWindow: MonoBehaviour 
{
    [SerializeField]
    private UIInput myUIInput;
    [SerializeField]
    private GameObject MyGrid;
    [SerializeField]
    private GameObject friendItem;
    public GameObject findButton;
    public GameObject closeButton;
	// Use this for initialization
	void Start () 
    {
        NetworkHandler.instance.SendProcess("7101#;");
        if (UIEventListener.Get(findButton).onClick == null)
        {
            UIEventListener.Get(findButton).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("7110#" + myUIInput.value + ";");
            };
        }
        if (UIEventListener.Get(closeButton).onClick == null)
        {
            UIEventListener.Get(closeButton).onClick += delegate(GameObject obj)
            {
                UIManager.instance.BackUI();
            };
        }
	}
    public void SetFriendWindow(List<FriendItemData> _FriendItemDataList)//tabIndex 0,1,2
    {
        MyGrid.transform.parent.GetComponent<UIPanel>().clipOffset = Vector2.zero;

        for (int i = MyGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(MyGrid.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < _FriendItemDataList.Count; i++)
        {
            GameObject go = NGUITools.AddChild(MyGrid, friendItem);
            go.name = _FriendItemDataList[i].userId.ToString();
            go.GetComponent<FriendItemSmall>().SetFriendItem(_FriendItemDataList[i]);
        }
        MyGrid.GetComponent<UIGrid>().Reposition();
    }
	
}
