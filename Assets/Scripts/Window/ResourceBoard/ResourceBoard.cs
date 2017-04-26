using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResourceBoard : MonoBehaviour {

    public UIScrollView MySV;
    public GameObject ResourceItem;
    public UIGrid MyGrid;
    public GameObject EnemyinvasionWindow;
    public List<Resource> ResourceList = new List<Resource>();

    public GameObject ReceiveButton;
    public GameObject PatrolButton;
    public GameObject holdButton;

    int resStatue = 0;
    int priority = 1;//优先级 1-显示占领Btn 2-显示巡逻Btn 3- 显示领取Btn
    TextTranslator.Gate _gate;
    int CurMapId = 0;
    bool IsPlay = true;


    void Start()
    {
        if (UIEventListener.Get(transform.Find("ResourcesList/ReceiveButton").gameObject).onClick == null)
        {
            UIEventListener.Get(transform.Find("ResourcesList/ReceiveButton").gameObject).onClick += delegate(GameObject go)
            {
                ReceiveReward();
                MyGridClear();
                NetworkHandler.instance.SendProcess("1135#;");
            };
        }
        if (UIEventListener.Get(transform.Find("ResourcesList/PatrolButton").gameObject).onClick == null)
        {
            UIEventListener.Get(transform.Find("ResourcesList/PatrolButton").gameObject).onClick += delegate(GameObject go)
            {
                MapWindow mw = GameObject.Find("MapObject").transform.FindChild("MapCon").GetComponent<MapWindow>();
                GameObject.Find("MainCamera").GetComponent<MouseClick>().IsResource = true;
                CharacterRecorder.instance.gotoGateID = mw.ResourceDot[CurMapId - 90001];
                mw.ResourceMapId = CurMapId;
                mw.SetMapTypeUpdate();
                mw.ResItemStatue = 2;
            };
        }
        if (UIEventListener.Get(transform.Find("ResourcesList/holdButton").gameObject).onClick == null)
        {
            UIEventListener.Get(transform.Find("ResourcesList/holdButton").gameObject).onClick += delegate(GameObject go)
            {
                if(CurMapId != 0)
                {
                    MapWindow mw = GameObject.Find("MapObject").transform.FindChild("MapCon").GetComponent<MapWindow>();
                    GameObject.Find("MainCamera").GetComponent<MouseClick>().IsResource = true;
                    CharacterRecorder.instance.gotoGateID = mw.ResourceDot[CurMapId - 90001];
                    mw.ResourceMapId = CurMapId;
                    mw.SetMapTypeUpdate();
                    mw.ResItemStatue = 1;
                }
            };
        }
        
        
    }

    public void InitResourceList()
    {
        for (int i = 0; i < 6; i++)
        {
            Resource rs = TextTranslator.instance.GetResourceMapId(90001 + i);
            ResourceList.Add(rs);
        }
    }

    public void InitList(List<Mapgetreslist> map)
    {
        if (ResourceList.Count == 0)
        {
            InitResourceList();
        }
        if (_gate == null)
        {
            _gate = TextTranslator.instance.GetGateByID(CharacterRecorder.instance.lastGateID);
        }
        for (int i = 0; i < ResourceList.Count; i++)
        {
            if (ResourceList[i].ChapterId <= _gate.chapter && CharacterRecorder.instance.mapID >= ResourceList[i].ChapterId)
            {
                GameObject go = NGUITools.AddChild(MyGrid.gameObject, ResourceItem);
                //go.name = (i + 1).ToString();
                go.name = "Resource_Item" + i;
                go.transform.localScale = new Vector3(1f,1f);
                if (i >= map.Count)
                {
                    resStatue = 1;
                }
                else
                {
                    SetResStatue(map[i]);
                }
                TextTranslator.Gate mapInfo = TextTranslator.instance.GetGateByID(ResourceList[i].MapId);
                switch (resStatue)
                {
                    case 1:
                        TextTranslator.Gate gate1 = TextTranslator.instance.GetGateByID(ResourceList[i].MapId);
                        go.GetComponent<ResourceItem>().Init("yxdi0", mapInfo.icon.ToString(), gate1.name, gate1.needForce, resStatue, gate1.id);
                        break;
                    case 2:
                        TextTranslator.Gate gate2 = TextTranslator.instance.GetGateByID(map[i].GetresId);
                        go.GetComponent<ResourceItem>().Init("yxdi0", mapInfo.icon.ToString(), gate2.name, gate2.needForce, resStatue, gate2.id);
                        break;
                    case 3:
                        TextTranslator.Gate gate3 = TextTranslator.instance.GetGateByID(map[i].GetresId);
                        go.GetComponent<ResourceItem>().Init("yxdi0", mapInfo.icon.ToString(), gate3.name, map[i].Timer, resStatue, gate3.id);
                        break;
                    case 4:
                        TextTranslator.Gate gate4 = TextTranslator.instance.GetGateByID(map[i].GetresId);
                        go.GetComponent<ResourceItem>().Init("yxdi0", mapInfo.icon.ToString(), gate4.name, map[i].Timer, resStatue, gate4.id);
                        break;
                }
            }
        }
        MyGrid.GetComponent<UIGrid>().Reposition();
        StartCoroutine(ShowResourceList());
        //ShowButton();
        SetSelectKuang("0");
    }

    IEnumerator ShowResourceList()
    {
        for (int i = 0; i < MyGrid.transform.childCount; i++)
        {
            if (IsPlay)
            {
                Transform child = MyGrid.transform.GetChild(i);
                child.GetComponent<TweenPosition>().from = new Vector3(200, child.localPosition.y);
                child.GetComponent<TweenPosition>().to = new Vector3(0, child.localPosition.y);
                child.gameObject.SetActive(true);
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                Transform child = MyGrid.transform.GetChild(i);
                child.GetComponent<TweenPosition>().enabled = false;
                child.gameObject.SetActive(true);
            }
        }
        IsPlay = true;
    }

    void ShowButton()
    {
        int num = MyGrid.transform.childCount;
        for (int i = num -1; i >= 0; i--)
        {
            switch(MyGrid.GetChild(i).GetComponent<ResourceItem>().Statue)
            {
                case 1:
                    priority = 1;
                    CurMapId = MyGrid.GetChild(i).GetComponent<ResourceItem>().MapId;
                    break;
                case 2:
                    priority = 2;
                    CurMapId = MyGrid.GetChild(i).GetComponent<ResourceItem>().MapId;
                    break;
                case 4:
                    priority = 3;
                    break;
            }
        }
        if(priority == 1)
        {
            ReceiveButton.SetActive(false);
            PatrolButton.SetActive(false);
            holdButton.SetActive(true);
        }
        else if(priority == 2)
        {
            ReceiveButton.SetActive(false);
            PatrolButton.SetActive(true);
            holdButton.SetActive(false);
        }
        else if(priority == 3)
        {
            ReceiveButton.SetActive(true);
            PatrolButton.SetActive(false);
            holdButton.SetActive(false);
        }
    }

    public void ReceiveReward()
    {
        int num = MyGrid.transform.childCount;
        int id = 0;
        for (int i = 0; i < num;i++ )
        {
            if(MyGrid.GetChild(i).GetComponent<ResourceItem>().Statue == 4)
            {
                id = MyGrid.GetChild(i).GetComponent<ResourceItem>().MapId;
                NetworkHandler.instance.SendProcess("1133#" + id + ";");
            }
        }
        //GameObject.Find("MapObject").transform.Find("MapCon").GetComponent<MapWindow>().HeroId = id;
    }

    void SetResStatue(Mapgetreslist _map)
    {
        if (_map.GetresId == 0)
        {
            resStatue = 1;
        }
        else if (_map.HeroId == 0)
        {
            resStatue = 2;
        }
        else if (_map.Timer > 0)
        {
            resStatue = 3;
        }
        else
        {
            resStatue = 4;
        }
    }
    public void MyGridClear()
    {
        for (int i = 0; i < MyGrid.transform.childCount; i++)
        {
            Destroy(MyGrid.transform.GetChild(i).gameObject);
        }
        MyGrid.GetComponent<UIGrid>().Reposition();
    }
    public void SetSelectKuang(string curNum)
    {
        for (int i = 0; i < MyGrid.transform.childCount; i++)
        {
            if ("Resource_Item" + curNum == MyGrid.GetChild(i).name)
            {
                MyGrid.GetChild(i).Find("HeroIcon/SelectKuang").gameObject.SetActive(true);
            }
            else
            {
                MyGrid.GetChild(i).Find("HeroIcon/SelectKuang").gameObject.SetActive(false);
            }
        }
    }

    //public void ShowEnemyinvasionWindow(string _CityName,int _MapId)
    //{
    //    EnemyinvasionWindow.SetActive(true);
    //    EnemyinvasionWindow.GetComponent<EnemyinvasionWindow>().Init(_CityName,_MapId);
    //    if (UIEventListener.Get(EnemyinvasionWindow.transform.Find("BgCollider").gameObject).onClick == null)
    //    {
    //        UIEventListener.Get(EnemyinvasionWindow.transform.Find("BgCollider").gameObject).onClick = delegate(GameObject go)
    //        {
    //            EnemyinvasionWindow.SetActive(false);
    //        };
    //    }
    //}
}
