using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FriendListWindow : MonoBehaviour {

    public GameObject friendlistItem;
    public GameObject uiGrid;
    public GameObject CloseButton;
    public UILabel MessageLabel;
    public int Limitlevel = 0;
    void OnEnable()
    {
        NetworkHandler.instance.SendProcess("7101#;");
    }
	void Start () {

        if (UIEventListener.Get(CloseButton).onClick == null) 
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                this.gameObject.SetActive(false);
            };
        }
	}

    public void GetFriendList(List<FriendItemData> FriendItemData) 
    {
        DestroyGride();
        Limitlevel=GameObject.Find("TeamInvitationWindow").GetComponent<TeamInvitationWindow>().Limitlevel;
        //foreach (var _OneFriendItemData in FriendItemData) //不在线
        //{
        //    if (_OneFriendItemData.level >= Limitlevel && _OneFriendItemData.lastLoginTime == 0) 
        //    {
        //        GameObject go = NGUITools.AddChild(uiGrid, friendlistItem);
        //        go.SetActive(true);
        //        go.name = _OneFriendItemData.userId.ToString();
        //        go.GetComponent<FriendListItem>().GetFriendListItem(_OneFriendItemData);
        //    }
        //}

        foreach (var _OneFriendItemData in FriendItemData)
        {
            if (_OneFriendItemData.level >= Limitlevel && _OneFriendItemData.lastLoginTime > 0)//在线
            {
                GameObject go = NGUITools.AddChild(uiGrid, friendlistItem);
                go.SetActive(true);
                go.name = _OneFriendItemData.userId.ToString();
                go.GetComponent<FriendListItem>().GetFriendListItem(_OneFriendItemData);
            }
        }
        uiGrid.GetComponent<UIGrid>().Reposition();

        if (uiGrid.transform.childCount == 0) 
        {
            MessageLabel.gameObject.SetActive(true);
            MessageLabel.text = "无好友在线";
        }
    }
    void DestroyGride()
    {
        for (int i = uiGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGrid.transform.GetChild(i).gameObject);
        }
    }
}
