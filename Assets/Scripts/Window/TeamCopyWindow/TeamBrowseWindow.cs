using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamBrowseWindow : MonoBehaviour {

	public GameObject JoinButton;
    public GameObject FindButton;
    public GameObject ButtonRefresh;

    public GameObject TeamBrowseItem;
    public GameObject uiGrid;
    public GameObject InfoLabel;

    public GameObject EnterPasswordWindow;
    public GameObject SearchTeamsWindow;

    private int CopyNumber;

	void Start () {

        if (UIEventListener.Get(JoinButton).onClick == null)
        {
            UIEventListener.Get(JoinButton).onClick += delegate(GameObject go)
            {
                //this.gameObject.SetActive(false);
                FindRoomIsSure();
            };
        }
        if (UIEventListener.Get(FindButton).onClick == null)
        {
            UIEventListener.Get(FindButton).onClick += delegate(GameObject go)
            {
                //this.gameObject.SetActive(false);
                //NetworkHandler.instance.SendProcess("6109#" + CopyNumber + ";");
                SearchTeamsWindow.SetActive(true);
            };
        }
        if (UIEventListener.Get(ButtonRefresh).onClick == null)
        {
            UIEventListener.Get(ButtonRefresh).onClick += delegate(GameObject go)
            {
                Debug.Log("刷新副本的标号" + CharacterRecorder.instance.CopyNumber);
                NetworkHandler.instance.SendProcess("6101#" + CharacterRecorder.instance.CopyNumber+ ";");
            };
        }
        CancelInvoke();
        InvokeRepeating("RefushTime", 0, 5f);
	}

    public void SetTeamBrowse(List<TeamBrowseItemDate> _TeamBrowseItemDate)
    {
        DestroyGride();
        uiGrid.transform.parent.GetComponent<UIPanel>().clipOffset = Vector2.zero;
        uiGrid.transform.parent.localPosition = new Vector3(9,-44f,0);
        InfoLabel.SetActive(false);
        CopyNumber = _TeamBrowseItemDate[0].copyNumber;
        for (int i = 0; i < _TeamBrowseItemDate.Count; i++) 
        {
            if (_TeamBrowseItemDate[i].name != CharacterRecorder.instance.characterName) 
            {
                GameObject go = NGUITools.AddChild(uiGrid, TeamBrowseItem);
                go.SetActive(true);
                go.name = _TeamBrowseItemDate[i].teamId.ToString();
                go.GetComponent<TeamBrowseItem>().SetTeamBrowse(_TeamBrowseItemDate[i]);
            }
        }
        uiGrid.GetComponent<UIGrid>().Reposition();
        if (uiGrid.transform.childCount == 0)
        {
            InfoLabel.SetActive(true);
            InfoLabel.GetComponent<UILabel>().text = "现在没有可加入的队伍，你可以去创建队伍";
        }
        else 
        {
            InfoLabel.SetActive(false);
        }
    }

    void RefushTime() 
    {
        NetworkHandler.instance.SendProcess("6101#" + CharacterRecorder.instance.CopyNumber + ";");
    }

    public void SetNoTeamBrowse() 
    {
        DestroyGride();
        InfoLabel.SetActive(true);
        InfoLabel.GetComponent<UILabel>().text = "现在没有可加入的队伍，你可以去创建队伍";
    }

    public void SetNoTeamBrowse2()
    {
        DestroyGride();
        InfoLabel.SetActive(true);
        InfoLabel.GetComponent<UILabel>().text = "没有此ID的房间";

    }

    public void FindRoomIsSure() 
    {
        if (uiGrid.transform.childCount == 0)
        {
            InfoLabel.SetActive(true);
            InfoLabel.GetComponent<UILabel>().text = "现在没有可加入的队伍，你可以去创建队伍";
        }
        else 
        {
            int t = 0;
            for (int i = uiGrid.transform.childCount - 1; i >= 0; i--)
            {
                TeamBrowseItem _TeamBrowseItem=uiGrid.transform.GetChild(i).GetComponent<TeamBrowseItem>();
                if (_TeamBrowseItem.peopleNum < 5 && _TeamBrowseItem.condition2 == "0")
                {
                    NetworkHandler.instance.SendProcess("6103#" + _TeamBrowseItem.teamId + ";");
                }
                else 
                {
                    t += 1;
                }
            }

            if (t == uiGrid.transform.childCount) 
            {
                InfoLabel.GetComponent<UILabel>().text = "没有符合要求的队伍房间";
            }
        }
    }

    void DestroyGride()
    {
        //ListRankShopItem.Clear();
        for (int i = uiGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGrid.transform.GetChild(i).gameObject);
        }
    }
}
