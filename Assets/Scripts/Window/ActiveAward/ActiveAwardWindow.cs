using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public enum ActiveAwardTabType
{
    ActiveAward = 0,//0
    FightAward = 1,//1
    ActiveRank = 2,//2
    HeroIntroduce = 3,//3
    ActiveRlue = 4,//4
}
public class ActiveAwardWindow : MonoBehaviour 
{
    public GameObject CloseButton;
    public UISprite dayTime;
    public UILabel labelTime;
    [SerializeField]
    private List<UIToggle> ListTabs = new List<UIToggle>();
    //不同Part
    [SerializeField]
    private List<GameObject> ObjListOfTabType;

    private List<FriendItemData> curFriendList = new List<FriendItemData>();//当前列表
    BetterList<ActiveAwardItemData> mList = new BetterList<ActiveAwardItemData>();
    private int curTabIndex = 0;
    private int Time = 0;
	// Use this for initialization
	void Start () 
    {
        UIManager.instance.CountActivitys(UIManager.Activitys.战力排行);
        UIManager.instance.UpdateActivitys(UIManager.Activitys.战力排行);

        NetworkHandler.instance.SendProcess("9121#;");
        NetworkHandler.instance.SendProcess("6004#3;");
        //SendToSeverToGetList(0);
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }
        for (int i = 0; i < ListTabs.Count; i++)
        {
            ListTabs[i].gameObject.name = i.ToString();
            UIEventListener.Get(ListTabs[i].gameObject).onClick = ClickListTabs;
        }
        //CreatDataForTheMoment();
	}
    void CreatDataForTheMoment()
    {
        mList.Clear();
        for (int i = 0; i < 10;i++ )
        {
            BetterList<AwardItem> mAwardList = new BetterList<AwardItem>();
            for (int j = 0; j < 3;j++ )
            {
                 mAwardList.Add(new AwardItem((9001 + j), (20 + j)));
            }
            ActiveAwardItemData _ActiveAwardItemData = new ActiveAwardItemData((i + 1), mAwardList);
            mList.Add(_ActiveAwardItemData);
        }
    }
    public void GetItemListData(BetterList<ActiveAwardItemData> mMyList)
    {
        //Debug.LogError("协议..." + mMyList.size);
        mList.Clear();
        for (int i = 0; i < mMyList.size; i++)
        {
            if(i < 10)
            {
                BetterList<AwardItem> mAwardList = new BetterList<AwardItem>();
                for (int j = 0; j < 3; j++)
                {
                    mAwardList.Add(new AwardItem((9001 + j), (20 + j)));
                }
                //ActiveAwardItemData _ActiveAwardItemData = new ActiveAwardItemData(curTabIndex, (i + 1), mAwardList);
                ActiveAwardItemData _ActiveAwardItemData = mMyList[i];
                //_ActiveAwardItemData.ActiveAwardTabType = curTabIndex;
                mList.Add(_ActiveAwardItemData);
            }
        }
        SendToSeverToGetList(0);
    }
    void SendToSeverToGetList(int tabIndex)
    {
        curTabIndex = tabIndex;
        //CreatDataForTheMoment();
        for (int i = 0; i < ObjListOfTabType.Count;i++ )
        {
            if (i == tabIndex)
            {
                ObjListOfTabType[i].SetActive(true);
                switch(i)
                {
                    case 0: ObjListOfTabType[0].GetComponent<ActiveAwardPart>().SetActiveAwardPart(tabIndex, TextTranslator.instance.ActivitySevenHeroList); break;
                    case 1: ObjListOfTabType[1].GetComponent<FightAwardPart>().SetActiveAwardPart(tabIndex, TextTranslator.instance.ActivitySevenRankList); break;
                    case 2: ObjListOfTabType[2].GetComponent<ActiveRankPart>().SetActiveAwardPart(tabIndex, mList); break;
                }
            }
            else
            {
                ObjListOfTabType[i].SetActive(false);
            }
        }
       /* switch (tabIndex)
        {
            case 0: NetworkHandler.instance.SendProcess("7101#;"); break;
            case 1: NetworkHandler.instance.SendProcess("7102#;"); break;
            case 2: NetworkHandler.instance.SendProcess("7103#;"); break;
        }*/
    }
    public void SetFriendWindow(int tabIndex, List<FriendItemData> _FriendItemDataList)//tabIndex 0,1,2
    {
        curFriendList.Clear();
        curFriendList.AddRange(_FriendItemDataList);
        for (int i = 0; i < ObjListOfTabType.Count; i++)
        {
            if (i == (int)tabIndex)
            {
                ObjListOfTabType[i].SetActive(true);
            }
            else
            {
                ObjListOfTabType[i].SetActive(false);
            }
        }
    }
    public void SetInfo(string StringAward)
    {
        string[] dataSplit = StringAward.Split(';');
        Time = int.Parse(dataSplit[1]);

        Time -= (86400);
        /*LockDay = int.Parse(dataSplit[0]);
        for (int i = 0; i < FunctionLock.Count; i++)
        {
            if (i <= LockDay - 1)
            {
                FunctionLock[i].SetActive(false);
            }
            else
            {
                FunctionLock[i].SetActive(true);
            }
        }*/
        for (int i = 2; i < dataSplit.Length - 3; i++)
        {
            string[] secSplit = dataSplit[i].Split('$');
            int _id = int.Parse(secSplit[0]);
            int _gotState = int.Parse(secSplit[1]);
            int _completeCount = int.Parse(secSplit[2]);
            ActivitySevenDay _ActivitySevenDay = TextTranslator.instance.GetActivitySevenDayByID(_id);
            _ActivitySevenDay.SetCompletState(_gotState, _completeCount);
        }
        int daytime = Time / 86400;
        if (daytime == 0) { dayTime.spriteName = "Serviceword0"; }
        else if (daytime == 1) { dayTime.spriteName = "Serviceword1"; }
        else if (daytime == 2) { dayTime.spriteName = "Serviceword2"; }
        else if (daytime == 3) { dayTime.spriteName = "Serviceword3"; }
        else if (daytime == 4) { dayTime.spriteName = "Serviceword4"; }
        else if (daytime == 5) { dayTime.spriteName = "Serviceword5"; }
        else if (daytime == 6) { dayTime.spriteName = "Serviceword6"; }
        else if (daytime == 7) { dayTime.spriteName = "Serviceword7"; }
        CancelInvoke();
        InvokeRepeating("UpdateTime", 0, 1.0f);
        //GetInfo();
        //GetItemListData(mList);
    }

    void UpdateTime()
    {
        if (Time > 0)
        {
            if (Time > 86400) { }
            string houreStr = (Time % 86400 / 3600).ToString("00");
            string miniteStr = (Time % 3600 / 60).ToString("00");
            string secondStr = (Time % 60).ToString("00");
            labelTime.text = string.Format("{0}:{1}:{2}", houreStr, miniteStr, secondStr);
            Time -= 1;
        }
    }
    void ClickListTabs(GameObject go)
    {
        if (go != null)
        {
            //SetFriendWindow(int.Parse(go.name));
            if (curTabIndex != int.Parse(go.name))
            {
                SendToSeverToGetList(int.Parse(go.name));
            }
        }
    }
}
