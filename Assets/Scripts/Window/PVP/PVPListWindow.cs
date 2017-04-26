using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PVPListWindow : MonoBehaviour {

    public UIGrid MyGrid;
    public GameObject PVPListItem;
    public GameObject CloseBtn;
    public UILabel BoardLabel;
    public UILabel RankingLabel;
    public UILabel PowerLabel;
    private List<GameObject> PVPListObj = new List<GameObject>();

    void Start()
    {
        this.gameObject.transform.localPosition = new Vector3(0, -40, 0);
        NetworkHandler.instance.SendProcess("6004#1;");
        //NetworkHandler.instance.SendProcess("6004#3;");
        if (UIEventListener.Get(CloseBtn).onClick == null)
        {
            UIEventListener.Get(CloseBtn).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }
        SetMyselfInfo();
    }
    public void SetListItem(int _Ranking,int _Level,string _Name,int _RoleID)
    {
        GameObject go = NGUITools.AddChild(MyGrid.gameObject, PVPListItem);
        go.GetComponent<PVPListItem>().Init(_Ranking, _Level, _Name, _RoleID);
        MyGrid.Reposition();
    }

    public void SetMyselfInfo()
    {
        switch (CharacterRecorder.instance.RankNumber)
        {
            case 0: BoardLabel.text = "未上榜"; break;
            default: BoardLabel.text = CharacterRecorder.instance.RankNumber.ToString(); break;
        }
        BoardLabel.text = "";
        RankingLabel.text = GameObject.Find("PVPWindow").GetComponent<PVPWindow>().mRankNumber.text;
        PowerLabel.text = CharacterRecorder.instance.Fight.ToString();
        if (int.Parse(RankingLabel.text) > 100)
        {
            RankingLabel.text = "未上榜";
        }
        else
        {
            RankingLabel.text = RankingLabel.text;
        }
    }
}
