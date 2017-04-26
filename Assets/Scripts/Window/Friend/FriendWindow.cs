using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public enum FriendWindowTabType
{
    FriendList,//1
    AddFriends,//2
    ApplayList,//3
}
public class FriendWindow : MonoBehaviour
{
    [SerializeField]
    private UIInput myUIInput;
    [SerializeField]
    private GameObject MyGrid;
    [SerializeField]
    private GameObject friendItem;
    [SerializeField]
    private UILabel friendCountLabel;
    [SerializeField]
    private UILabel myUIDLabel;
    [SerializeField]
    private UILabel myFriendLabel;
    [SerializeField]
    private GameObject OneKeyGiveButton;
    [SerializeField]
    private GameObject OneKeyGetButton;
    [SerializeField]
    private GameObject findButton;
    [SerializeField]
    private GameObject changeButton;
    [SerializeField]
    private GameObject AllApplayButton;
    [SerializeField]
    private GameObject AllAgreeButton;
    [SerializeField]
    private GameObject AllRefuseButton;

    private int ClickIndex = 0;
    [SerializeField]
    private List<UIToggle> ListTabs = new List<UIToggle>();
    //不同Part
    [SerializeField]
    private List<GameObject> ObjListOfTabType;

    private List<GameObject> ListFriends = new List<GameObject>();
    private List<FriendItemData> myFriendList = new List<FriendItemData>();//我的好友
    private List<FriendItemData> curFriendList = new List<FriendItemData>();//当前列表
    private Vector3 scrollViewInitPos = new Vector3(0, -108, 0);
    int openVip = 8;//V3 开放高级赠送
    void OnEnable()
    {
        //NetworkHandler.instance.SendProcess("5001#;");
    }
    // Use this for initialization
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.好友);
        UIManager.instance.UpdateSystems(UIManager.Systems.好友);

        SetRedPointOfListTab();
        myUIDLabel.text = string.Format("我的uid:{0}", CharacterRecorder.instance.userId);
        for (int i = 0; i < ListTabs.Count; i++)
        {
            ListTabs[i].gameObject.name = i.ToString();
            UIEventListener.Get(ListTabs[i].gameObject).onClick = ClickListTabs;
        }
        InitOpenState();
        if (UIEventListener.Get(OneKeyGiveButton).onClick == null)
        {
            UIEventListener.Get(OneKeyGiveButton).onClick += delegate(GameObject go)
            {
                string sendUIDListString = "";
                int count = 0;
                for (int i = 0; i < myFriendList.Count; i++)
                {
                    if (myFriendList[i].giveSpriteState == 0)
                    {
                        Debug.Log("一键赠送uid..." + myFriendList[i].userId);
                        sendUIDListString += myFriendList[i].userId + ";";
                        NetworkHandler.instance.SendProcess("7107#" + myFriendList[i].userId + ";");
                        count++;
                    }
                }
                if (count == 0 && myFriendList.Count > 0)//没有可以赠送的好友
                {
                    UIManager.instance.OpenPromptWindow("已赠送", PromptWindow.PromptType.Hint, null, null);
                }
                if (myFriendList.Count <= 0)//没有好友
                {
                    UIManager.instance.OpenPromptWindow("还没有好友，请先添加好友！", PromptWindow.PromptType.Hint, null, null);
                    //ClickListTabs(ListTabs[1].gameObject);
                }
                //NetworkHandler.instance.SendProcess("7107#" + sendUIDListString);
            };
        }
        if (UIEventListener.Get(OneKeyGetButton).onClick == null)
        {
            UIEventListener.Get(OneKeyGetButton).onClick += delegate(GameObject go)
            {
                bool isGet = false;
                string sendUIDListString = "";
                if (myFriendList.Count <= 0)//没有好友
                {
                    UIManager.instance.OpenPromptWindow("还没有好友，请先添加好友！", PromptWindow.PromptType.Hint, null, null);
                    return;
                    //ClickListTabs(ListTabs[1].gameObject);
                }
                for (int i = 0; i < myFriendList.Count; i++)
                {
                    if (myFriendList[i].canGetSpriteState == 1)//可领
                    {
                        isGet = true;
                        break;
                    }
                }
                if (isGet)
                {
                    for (int i = 0; i < myFriendList.Count; i++)
                    {
                        if (myFriendList[i].canGetSpriteState == 1)
                        {
                            sendUIDListString += myFriendList[i].userId + ";";
                            NetworkHandler.instance.SendProcess("7108#" + myFriendList[i].userId + ";");
                        }
                    }
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("没有可以领取的！", PromptWindow.PromptType.Hint, null, null);
                }


                //NetworkHandler.instance.SendProcess("7108#" + sendUIDListString);
            };
        }
        if (UIEventListener.Get(AllApplayButton).onClick == null)
        {
            UIEventListener.Get(AllApplayButton).onClick += delegate(GameObject go)
            {
                string sendUIDListString = "";
                for (int i = 0; i < curFriendList.Count; i++)
                {
                    sendUIDListString += curFriendList[i].userId + ";";
                    NetworkHandler.instance.SendProcess("7104#" + curFriendList[i].userId + ";");
                }
                //NetworkHandler.instance.SendProcess("7104#" + sendUIDListString);
            };
        }
        if (UIEventListener.Get(AllAgreeButton).onClick == null)
        {
            UIEventListener.Get(AllAgreeButton).onClick += delegate(GameObject go)
            {
                string sendUIDListString = "";
                for (int i = 0; i < curFriendList.Count; i++)
                {
                    sendUIDListString += curFriendList[i].userId + ";";
                    NetworkHandler.instance.SendProcess("7105#" + curFriendList[i].userId + ";");
                }
                //NetworkHandler.instance.SendProcess("7105#" + sendUIDListString);
            };
        }
        if (UIEventListener.Get(AllRefuseButton).onClick == null)
        {
            UIEventListener.Get(AllRefuseButton).onClick += delegate(GameObject go)
            {
                string sendUIDListString = "";
                for (int i = 0; i < curFriendList.Count; i++)
                {
                    sendUIDListString += curFriendList[i].userId + ";";
                    NetworkHandler.instance.SendProcess("7106#" + curFriendList[i].userId + ";");
                }
                //NetworkHandler.instance.SendProcess("7106#" + sendUIDListString);
            };
        }
        if (UIEventListener.Get(findButton).onClick == null)
        {
            UIEventListener.Get(findButton).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("7110#" + myUIInput.value + ";");
            };
        }
        if (UIEventListener.Get(changeButton).onClick == null)
        {
            UIEventListener.Get(changeButton).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("7102#;");
            };
        }

        Debug.LogError("好友的Tab++++++++++++++" + CharacterRecorder.instance.FriendListTab);
        if (CharacterRecorder.instance.FriendListTab == 3)
        {
            CharacterRecorder.instance.FriendListTab = 0;
        }
        SendToSeverToGetList(CharacterRecorder.instance.FriendListTab);
        ListTabs[CharacterRecorder.instance.FriendListTab].GetComponent<UIToggle>().startsActive = true;
    }
    void InitOpenState()
    {
        for (int i = 0; i < ListTabs.Count; i++)
        {
            switch (ListTabs[i].name)
            {
                case "0":
                case "1":
                case "2":
                    break;
                case "3":
                    if (CharacterRecorder.instance.Vip >= openVip)
                    {
                        ListTabs[i].GetComponent<UIToggle>().enabled = true;
                        ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(false);
                    }
                    else
                    {
                        ListTabs[i].GetComponent<UIToggle>().value = false;
                        StartCoroutine(DelaySetUIToggleFalse(ListTabs[i].GetComponent<UIToggle>()));
                        ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(true);
                    }
                    break;
            }
        }
    }
    IEnumerator DelaySetUIToggleFalse(UIToggle _UIToggle)
    {
        yield return new WaitForSeconds(0.5f);
        _UIToggle.enabled = false;
    }
    void ClickListTabs(GameObject go)
    {
        if (go != null)
        {
            //SetFriendWindow(int.Parse(go.name));
            SendToSeverToGetList(int.Parse(go.name));
        }
    }
    public void SendToSeverToGetList(int tabIndex)
    {
        switch (tabIndex)
        {
            case 0:
                NetworkHandler.instance.SendProcess("7103#;");
                NetworkHandler.instance.SendProcess("7101#;");
                break;
            case 1: NetworkHandler.instance.SendProcess("7102#;");
                break;
            case 2: NetworkHandler.instance.SendProcess("7103#;");
                break;
            case 3:
                //UIManager.instance.OpenPromptWindow("即将开放！", PromptWindow.PromptType.Hint, null, null);

                if (CharacterRecorder.instance.Vip >= openVip)
                {
                    SetFriendWindow(3, CharacterRecorder.instance.MyFriendList);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow(string.Format("VIP{0}开放高级赠送", openVip), PromptWindow.PromptType.Hint, null, null);
                }
                break;
        }

        CharacterRecorder.instance.FriendListTab = tabIndex;
    }
    public void SetFriendWindow(int tabIndex, List<FriendItemData> _FriendItemDataList)//tabIndex 0,1,2
    {
        curFriendList.Clear();
        curFriendList.AddRange(_FriendItemDataList);
        if (tabIndex == 0 || tabIndex == 3)
        {
            myFriendList.Clear();
            myFriendList.AddRange(_FriendItemDataList);
            myFriendLabel.text = string.Format("好友数量:{0}/50", _FriendItemDataList.Count);
        }
        MyGrid.transform.parent.GetComponent<UIPanel>().clipOffset = Vector2.zero;
        MyGrid.transform.parent.localPosition = scrollViewInitPos;
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

        for (int i = ListFriends.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(ListFriends[i]);
        }
        ListFriends.Clear();

        for (int i = 0; i < _FriendItemDataList.Count; i++)
        {
            GameObject go = NGUITools.AddChild(MyGrid, friendItem);
            go.name = _FriendItemDataList[i].userId.ToString();
            go.GetComponent<FriendItem>().SetFriendItem(tabIndex, _FriendItemDataList[i]);
            //go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("ScrollView").GetComponent<UIScrollView>();
            ListFriends.Add(go);
        }
        MyGrid.GetComponent<UIGrid>().Reposition();

        SetRedPointOfListTab();
    }
    //申请好友7104
    public void ResetFriendWindow(int dealWithSever, string uIDOrName)
    {
        switch (dealWithSever)
        {
            case 7104: //申请好友7104
                if (IsAllNumber(uIDOrName))
                {
                    for (int i = 0; i < ListFriends.Count; i++)
                    {
                        Debug.Log(ListFriends[i].name);
                        if (uIDOrName == ListFriends[i].name)
                        {
                            GameObject _ApplayButton = ListFriends[i].GetComponent<FriendItem>().ApplayButton;
                            //_ApplayButton.GetComponent<UIButton>().enabled = false;
                            //_ApplayButton.GetComponent<UISprite>().color = Color.grey;
                            _ApplayButton.GetComponent<UISprite>().spriteName = "buttonApplay1";
                            _ApplayButton.GetComponent<BoxCollider>().enabled = false;
                            break;
                        }
                    }
                }
                else
                {
                    //Debug.LogError(ListFriends.Count);
                    for (int i = 0; i < ListFriends.Count; i++)
                    {
                        if (uIDOrName == ListFriends[i].GetComponent<FriendItem>().OneFriendItemData.name)
                        {
                            GameObject _ApplayButton = ListFriends[i].GetComponent<FriendItem>().ApplayButton;
                            //_ApplayButton.GetComponent<UIButton>().enabled = false;
                            //_ApplayButton.GetComponent<UISprite>().color = Color.grey;
                            _ApplayButton.GetComponent<UISprite>().spriteName = "buttonApplay1";
                            _ApplayButton.GetComponent<BoxCollider>().enabled = false;
                            break;
                        }
                    }
                }
                break;
            case 7105://通过好友
                SetRedPointOfListTab();
                NetworkHandler.instance.SendProcess("7103#;");
                /*    for (int i = 0; i < ListFriends.Count; i++)
                    {
                        if (uIDOrName == ListFriends[i].name)
                        {
                            GameObject _AgreeButton = ListFriends[i].GetComponent<FriendItem>().AgreeButton;
                            _AgreeButton.GetComponent<UISprite>().color = Color.grey;
                            _AgreeButton.GetComponent<BoxCollider>().enabled = false; 
                            break;
                        }
                    }*/
                break;
            case 7106://拒绝好友
                SetRedPointOfListTab();
                NetworkHandler.instance.SendProcess("7103#;");
                /*  for (int i = 0; i < ListFriends.Count; i++)
                  {
                      if (uIDOrName == ListFriends[i].name)
                      {
                          GameObject _RefuseButton = ListFriends[i].GetComponent<FriendItem>().RefuseButton;
                          _RefuseButton.GetComponent<UISprite>().color = Color.grey;
                          _RefuseButton.GetComponent<BoxCollider>().enabled = false; 
                          break;
                      }
                  }*/
                break;
            case 7107://赠送精力
                for (int i = 0; i < ListFriends.Count; i++)
                {
                    if (uIDOrName == ListFriends[i].name)
                    {
                        ListFriends[i].GetComponent<FriendItem>().OneFriendItemData.giveSpriteState = 1;
                        GameObject _GiveButton = ListFriends[i].GetComponent<FriendItem>().GiveButton;
                        _GiveButton.GetComponent<UISprite>().spriteName = "buttonGive1";
                        _GiveButton.GetComponent<BoxCollider>().enabled = false;
                        break;
                    }
                }
                break;
            case 7108://领取精力
                for (int i = 0; i < ListFriends.Count; i++)
                {
                    if (uIDOrName == ListFriends[i].name)
                    {
                        Vector3 fromPos = new Vector3(0, 80, 0);
                        Vector3 toPos = new Vector3(0, 180, 0);
                        UIManager.instance.OpenPromptWindow("精力+" + 2, 5, true, fromPos, toPos, PromptWindow.PromptType.Hint, null, null);

                        ListFriends[i].GetComponent<FriendItem>().OneFriendItemData.canGetSpriteState = 2;
                        GameObject _GetButton = ListFriends[i].GetComponent<FriendItem>().GetButton;
                        // _GetButton.SetActive(false);
                        _GetButton.GetComponent<UISprite>().spriteName = "PVPIntegration1";
                        //break;
                    }
                    if (i == ListFriends.Count - 1)
                    {
                        CharacterRecorder.instance.SetRedPoint(9, false);
                    }
                }
                break;
        }
        SetRedPointOfListTab();
    }
    bool IsAllNumber(string uIDOrName)
    {
        try
        {
            int n = 0;
            n = int.Parse(uIDOrName);
            return true;
        }
        catch
        {
            return false;
        }
        /*  int i;
          for (i = 0; i < uIDOrName.Length;i++ )
          {
              char tempChar = Convert.ToChar(uIDOrName.Substring(i, 1));
              if (!(tempChar > '0' && tempChar < '9' || tempChar == '0' || tempChar == '9'))
              {
                  break;
              }
          }
          Debug.LogError("....." + i);
          if (i == uIDOrName.Length)
          {
              return true;
          }
          else
          {
              return false;
          } */
    }
    //好友小红点
    void SetRedPointOfListTab()
    {
        if (CharacterRecorder.instance.applayFriendListCount > 0)
        {
            ListTabs[2].transform.FindChild("RedPoint").gameObject.SetActive(true);
            CharacterRecorder.instance.SetRedPoint(9, true);
        }
        else
        {
            ListTabs[2].transform.FindChild("RedPoint").gameObject.SetActive(false);
        }
    }

}
