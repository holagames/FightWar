using UnityEngine;
using System.Collections;

public class SelectRoleWindow : MonoBehaviour {

    public GameObject CloseButton;
    public GameObject uiGrid;
    public GameObject SelectRoleItem;
    private int TeamID;
    private int Index;
    //void OnEnable()
    //{
    //    NetworkHandler.instance.SendProcess("7101#;");
    //}
	void Start () {

        if (UIEventListener.Get(CloseButton).onClick == null) 
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                //UIManager.instance.BackUI();
                Destroy(this.gameObject);
            };
        }
	}

    public void SetRoleInfo(int _teamid,int _index) 
    {
        this.TeamID = _teamid;
        this.Index = _index;
        DestroyGride();
        foreach (var item in CharacterRecorder.instance.ownedHeroList)
        {
            GameObject go = NGUITools.AddChild(uiGrid, SelectRoleItem);
            go.SetActive(true);
            go.name = "RoleItem" + item.cardID;
            go.GetComponent<SelectRoleItem>().Init(item.cardID, item.name, TeamID, _index);
        }
        uiGrid.GetComponent<UIGrid>().Reposition();
    }

    void DestroyGride()
    {
        for (int i = uiGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGrid.transform.GetChild(i).gameObject);
        }
    }
}
