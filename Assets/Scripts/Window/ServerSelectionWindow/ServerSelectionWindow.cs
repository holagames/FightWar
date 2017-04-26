using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ServerSelectionWindow : MonoBehaviour {

    public GameObject ServerButtonItem;
    public GameObject ServerItem;
    public GameObject gridLeft;
    public GameObject Recommendation;
    public GameObject Parentobj;
    //public GameObjectScrollViewTwo;
    public GameObject ScrollViewOther;
    public GameObject gridOne;
    public GameObject gridTwo;
    public GameObject gridOther;

    private List<string> GroupList = new List<string>();
    private List<GameObject> ButtonList=new List<GameObject>();
    private string ServerInfo;
    private int token = 0;

	void Start () {
        //this.gameObject.transform.localPosition = new Vector3(0, -10f, -100f);
        if (UIEventListener.Get(GameObject.Find("CloseButton")).onClick == null) 
        {
            UIEventListener.Get(GameObject.Find("CloseButton")).onClick += delegate(GameObject go)
            {
                //if (GameObject.Find("LoginWindow") != null) 
                //{
                //    //GameObject.Find("LoginWindow").GetComponent<LoginWindow>().DaZhanLue.SetActive(true);
                //    //GameObject.Find("LoginWindow").GetComponent<LoginWindow>().DaZhanLue02.SetActive(true);
                //    //GameObject.Find("LoginWindow").GetComponent<LoginWindow>().LOGO_di2.SetActive(true);
                //}
                UIManager.instance.BackUI();
            };
        }
        if (UIEventListener.Get(Recommendation).onClick == null)
        {
            UIEventListener.Get(Recommendation).onClick += delegate(GameObject go)
            {
                token = 0;
                SetRightPart1("0");
            };
        }
        SetServerInfo1();
        SetRightPart1("0");
        //this.gameObject.transform.localPosition = new Vector3(0, 0f, -100f);
	}
    public void SetServerInfo1() 
    {
        foreach (var item in TextTranslator.instance.ServerLists)
        {
            if (!GroupList.Contains(item.GroupID)) 
            {
                GroupList.Add(item.GroupID);
            }
        }

        for (int i = 0; i < GroupList.Count; i++) 
        {
            GameObject go = NGUITools.AddChild(gridLeft, ServerButtonItem);
            go.SetActive(true);
            go.name = GroupList[i];
            go.transform.Find("NormalButton").transform.Find("label").GetComponent<UILabel>().text = GroupList[i];
            go.transform.Find("ChangeButton").transform.Find("label").GetComponent<UILabel>().text = GroupList[i];
            if (UIEventListener.Get(go).onClick == null)
            {
                UIEventListener.Get(go).onClick += delegate(GameObject obj)
                {
                    token = 1;
                    SetRightPart1(go.name);
                };
            }
        }
    }

    void SetRightPart1(string GroupID) 
    {
        if (token == 0)
        {
            RecommendServer();
        }
        else
        {
            Parentobj.SetActive(false);
            ScrollViewOther.SetActive(true);
            gridOther.transform.parent.localPosition = new Vector3(238f, -97f, 0);
            gridOther.transform.parent.GetComponent<UIPanel>().clipOffset = Vector2.zero;
            DestroyGride();
            //int serverid = int.Parse(obj.name);
            //for (int i = 0; i < OneServerNum; i++)
            //{
            //    GameObject go = NGUITools.AddChild(gridOther, ServerItem);
            //    go.SetActive(true);
            //    go.GetComponent<ServerItem>().SetInfo(serverid * 10 - 9 + i);
            //}
            foreach (var item in TextTranslator.instance.ServerLists)
            {
                if (item.GroupID==GroupID)
                {
                    GameObject go = NGUITools.AddChild(gridOther, ServerItem);
                    go.SetActive(true);
                    go.GetComponent<ServerItem>().SetInfo(item);
                }
            }
            gridOther.GetComponent<UIGrid>().Reposition();
        }
    }

/*
    public void SetServerInfo() 
    {
        int ServerNum = TextTranslator.instance.ServerLists.size;
        Debug.Log("-----------" + TextTranslator.instance.ServerLists.size);
        if (ServerNum <= 10) 
        {
            GameObject go = NGUITools.AddChild(gridLeft, ServerButtonItem);
            go.SetActive(true);
            go.name = "1";
            go.transform.Find("NormalButton").transform.Find("label").GetComponent<UILabel>().text = "1-" + ServerNum.ToString() + "区";
            go.transform.Find("ChangeButton").transform.Find("label").GetComponent<UILabel>().text = "1-" + ServerNum.ToString() + "区";
            if (UIEventListener.Get(go).onClick == null)
            {
                UIEventListener.Get(go).onClick += delegate(GameObject obj)
                {
                    token = 1;
                    SetRightPart(go,ServerNum);
                };
            }
        }
        else if (ServerNum % 10 > 0&&ServerNum>10) 
        {
            int HowServer = ServerNum / 10;
            int LastServer = ServerNum - HowServer * 10;
            for (int i = 0; i < HowServer; i++)
            {
                GameObject go = NGUITools.AddChild(gridLeft, ServerButtonItem);
                go.SetActive(true);
                go.name = (i + 1).ToString();
                go.transform.Find("NormalButton").transform.Find("label").GetComponent<UILabel>().text = ((i + 1) * 10 - 9).ToString() + "-" + ((i + 1) * 10).ToString() + "区";
                go.transform.Find("ChangeButton").transform.Find("label").GetComponent<UILabel>().text = ((i + 1) * 10 - 9).ToString() + "-" + ((i + 1) * 10).ToString() + "区";
                if (UIEventListener.Get(go).onClick == null)
                {
                    UIEventListener.Get(go).onClick += delegate(GameObject obj)
                    {
                        token = 1;
                        SetRightPart(go,10);
                    };
                }
            }
            GameObject gab = NGUITools.AddChild(gridLeft, ServerButtonItem);
            gab.SetActive(true);
            gab.name = (HowServer + 1).ToString();
            gab.transform.Find("NormalButton").transform.Find("label").GetComponent<UILabel>().text = (HowServer * 10 + 1).ToString() + "-" + ServerNum.ToString() + "区";
            gab.transform.Find("ChangeButton").transform.Find("label").GetComponent<UILabel>().text = (HowServer * 10 + 1).ToString() + "-" + ServerNum.ToString() + "区";
            if (UIEventListener.Get(gab).onClick == null)
            {
                UIEventListener.Get(gab).onClick += delegate(GameObject obj)
                {
                    token = 1;
                    SetRightPart(gab, LastServer);
                };
            }
        }
        else if (ServerNum % 10 == 0 && ServerNum > 10) 
        {
            int Servertab = ServerNum / 10;
            for (int i = 0; i < Servertab; i++)
            {
                GameObject go = NGUITools.AddChild(gridLeft, ServerButtonItem);
                go.SetActive(true);
                go.name = (i + 1).ToString();
                go.transform.Find("NormalButton").transform.Find("label").GetComponent<UILabel>().text = ((i + 1) * 10 - 9).ToString() + "-" + ((i + 1) * 10).ToString() + "区";
                go.transform.Find("ChangeButton").transform.Find("label").GetComponent<UILabel>().text = ((i + 1) * 10 - 9).ToString() + "-" + ((i + 1) * 10).ToString() + "区";
                if (UIEventListener.Get(go).onClick == null)
                {
                    UIEventListener.Get(go).onClick += delegate(GameObject obj)
                    {
                        token = 1;
                        SetRightPart(go,10);
                    };
                }
            }
        }
        gridLeft.GetComponent<UIGrid>().Reposition();
        Recommendation.GetComponent<UIToggle>().value = true;
        Recommendation.GetComponent<UIToggle>().startsActive = true;
    }

    void SetRightPart(GameObject obj,int OneServerNum) 
    {
        if (token == 0)
        {
            RecommendServer();
        }
        else 
        {
            Parentobj.SetActive(false);
            ScrollViewOther.SetActive(true);
            gridOther.transform.parent.localPosition = new Vector3(238f, -97f, 0);
            gridOther.transform.parent.GetComponent<UIPanel>().clipOffset = Vector2.zero;
            DestroyGride();
            int serverid = int.Parse(obj.name);
            for (int i = 0; i < OneServerNum; i++) 
            {
                GameObject go = NGUITools.AddChild(gridOther, ServerItem);
                go.SetActive(true);
                go.GetComponent<ServerItem>().SetInfo(serverid*10-9+i);
            }
            gridOther.GetComponent<UIGrid>().Reposition();
        }
    }
    */
    void DestroyGride()
    {
        for (int i = gridOther.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(gridOther.transform.GetChild(i).gameObject);
        }
    }
    void DestroyGrideOne()
    {
        for (int i = gridOne.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(gridOne.transform.GetChild(i).gameObject);
        }
    }
    void DestroyGrideTwo()
    {
        for (int i = gridTwo.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(gridTwo.transform.GetChild(i).gameObject);
        }
    }
    void RecommendServer() 
    {
        string[] dataSplit = TextTranslator.instance.ServerInfo.Split('$');//RecvingStr.Split(';');     
        ScrollViewOther.SetActive(false);
        Parentobj.SetActive(true);
        DestroyGrideOne();
        DestroyGrideTwo();

        gridOne.transform.parent.localPosition = new Vector3(238f, 22f, 0);
        gridOne.transform.parent.GetComponent<UIPanel>().clipOffset = Vector2.zero;

        gridTwo.transform.parent.localPosition = new Vector3(238f, -259f, 0);
        gridTwo.transform.parent.GetComponent<UIPanel>().clipOffset = Vector2.zero;

        if (PlayerPrefs.GetString("ServerID") != "0") 
        {
            for (int i = 0; i < dataSplit.Length - 1; i++)
            {
                bool isHave = false;
                foreach (var item in TextTranslator.instance.ServerLists)
                {
                    if (item.ServerTag == dataSplit[i])
                    {
                        isHave = true;
                        break;
                    }
                }

                if (isHave) //yy  11.23
                {
                    GameObject go = NGUITools.AddChild(gridOne, ServerItem);
                    go.SetActive(true);
                    go.GetComponent<ServerItem>().SetInfo(TextTranslator.instance.GetServerListsByID(dataSplit[i]));
                }
            }
            gridOne.GetComponent<UIGrid>().Reposition();
        }
        //for (int i = 0; i < trcSplit.Length - 1; i++) 
        //{
        //    GameObject go = NGUITools.AddChild(gridOne, ServerItem);
        //    go.SetActive(true);
        //    go.GetComponent<ServerItem>().SetInfo(int.Parse(trcSplit[i]));
        //}
        //gridOne.GetComponent<UIGrid>().Reposition();

        //for (int i = 0; i < drcSplit.Length - 1; i++)
        //{
        //    GameObject go = NGUITools.AddChild(gridTwo, ServerItem);
        //    go.SetActive(true);
        //    go.GetComponent<ServerItem>().SetInfo(int.Parse(drcSplit[i]));
        //}
        foreach (var item in TextTranslator.instance.ServerLists)
        {
            if (item.Type==2)
            {
                GameObject go = NGUITools.AddChild(gridTwo, ServerItem);
                go.SetActive(true);
                go.GetComponent<ServerItem>().SetInfo(item);
            }
        }
        gridTwo.GetComponent<UIGrid>().Reposition();
    }
}
