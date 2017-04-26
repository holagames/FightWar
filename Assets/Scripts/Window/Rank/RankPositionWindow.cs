using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RankPositionWindow : MonoBehaviour
{

    public GameObject uiGrid;
    public GameObject HeroHeadPrafeb;
    public GameObject closeButton;
    public GameObject AKeyButton;
    public GameObject[] SetPointList;

    List<GameObject> headlist = new List<GameObject>();
    List<GameObject> Captainlist = new List<GameObject>();
    // Use this for initialization

    void Start()
    {
        SetHeroListInfo();

        if (GameObject.Find("LegionWarWindow") != null)
        {
            SetPosition2();
        }
        else if (GameObject.Find("PVPWindow") != null)
        {
            SetPosition();
        }
        this.transform.Find("All/DownContent/DownChild").GetComponent<TweenScale>().ResetToBeginning();
        this.transform.Find("All/DownContent/DownChild").GetComponent<TweenScale>().PlayForward();
        if (UIEventListener.Get(closeButton).onClick == null)
        {
            UIEventListener.Get(closeButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        };
        if (UIEventListener.Get(GameObject.Find("SaveButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("SaveButton")).onClick += delegate(GameObject go)
            {
                string TeamID = "";
                string CaptainName="";
                int CaptionPosition=0;

                for (int i = 0; i < SetPointList.Length; i++)
                {
                    if (SetPointList[i].transform.childCount != 0)
                    { 
                        //Debug.LogError("id号:    "+SetPointList[i].transform.GetChild(0).name);
                        int position;                      
                        switch (i)
                        {/*
                            case 0: position = 8+26; break;
                            case 1: position = 9+26; break;
                            case 2: position = 12+26; break;
                            case 3: position = 13+26; break;
                            case 4: position = 13+26; break;
                            case 5: position = 17+26; break;
                            case 6: position = 18+26; break;
                            default: position = 18+26; break;*/
                            case 0: position = 41; break;   //对应镜像位置
                            case 1: position = 36; break;
                            case 2: position = 31; break;
                            case 3: position = 37; break;
                            case 4: position = 32; break;
                            case 5: position = 38; break;
                            case 6: position = 33; break;
                            case 7: position = 28; break;
                            case 8: position = 34; break;
                            case 9: position = 29; break;
                            case 10: position = 35; break;
                            case 11: position = 30; break;
                            default: position = 25; break;
                        }
                        if (SetPointList[i].transform.GetChild(0).transform.Find("LabelCaptain").gameObject.activeSelf == false)
                        {
                            if (TeamID == "")
                            {
                                TeamID = string.Format("{0}${1}!", SetPointList[i].transform.GetChild(0).name, position);//i+2);
                            }
                            else
                            {
                                TeamID += string.Format("{0}${1}!", SetPointList[i].transform.GetChild(0).name, position);// i+2);
                            }
                        }
                        else 
                        {
                            CaptainName = SetPointList[i].transform.GetChild(0).name;
                            CaptionPosition = position;
                        }
                    }
                }
                if (CaptainName != "") 
                {
                    TeamID += string.Format("{0}${1}!", CaptainName,CaptionPosition);
                }
                Debug.LogError(TeamID);

                if (GameObject.Find("PVPWindow") != null)
                {
                    NetworkHandler.instance.SendProcess("6007#6;" + TeamID + ";");
                }
                else if (GameObject.Find("LegionWarWindow") != null)
                {
                    NetworkHandler.instance.SendProcess("8617#;" + TeamID + ";");
                }
                UIManager.instance.BackUI();
            };
        }
        PlayerPrefs.SetInt("PvpRankPodition_" + PlayerPrefs.GetString("ServerID") + "_" + PlayerPrefs.GetInt("UserID"), 1);
        if (GameObject.Find("PVPWindow") != null)
        {
            GameObject.Find("PVPWindow").GetComponent<PVPWindow>().PositionButtonRedPoint();
        }


        #region AKeyButton
        if (UIEventListener.Get(AKeyButton).onClick == null)
        {
            UIEventListener.Get(AKeyButton).onClick += delegate(GameObject go)
            {
                Debug.LogError("一键上阵按钮触发事件函数");

                int PositionCount = 0;
                List<int> PositionFList = new List<int>();
                List<int> PositionSList = new List<int>();
                List<int> CleanHeroIDList = new List<int>();
                BetterList<Hero> NewdHeroList = new BetterList<Hero>();

                PositionFList.Add(28);
                PositionFList.Add(31);
                PositionFList.Add(25);
                PositionFList.Add(32);
                PositionFList.Add(29);

                PositionSList.Add(33);
                PositionSList.Add(36);
                PositionSList.Add(30);
                PositionSList.Add(37);
                PositionSList.Add(34);

                for (int i = 0; i < SetPointList.Length; i++)
                {
                    if (SetPointList[i].transform.childCount > 0)//判断蒙版下面是否拥有hero位置
                    {
                        CleanHeroIDList.Add(int.Parse(SetPointList[i].transform.GetChild(0).gameObject.name));
                        PositionCount++;
                        switch (i)
                        {
                            case 7:
                                PositionFList.Remove(28);
                                break;
                            case 2:
                                PositionFList.Remove(31);
                                break;
                            case 12:
                                PositionFList.Remove(25);
                                break;
                            case 4:
                                PositionFList.Remove(32);
                                break;
                            case 9:
                                PositionFList.Remove(29);
                                break;
                            case 6:
                                PositionSList.Remove(33);
                                break;
                            case 1:
                                PositionSList.Remove(36);
                                break;
                            case 11:
                                PositionSList.Remove(30);
                                break;
                            case 3:
                                PositionSList.Remove(37);
                                break;
                            case 8:
                                PositionSList.Remove(34);
                                break;
                        }                   
                    }
                }
              
                foreach (var item in CharacterRecorder.instance.ownedHeroList)
                {
                    bool isHeroStay = false;
                    for (int j = 0; j < CleanHeroIDList.Count; j++) 
                    {
                        if (CleanHeroIDList[j] == item.characterRoleID) 
                        {
                            isHeroStay = true;
                            break;
                        }
                    }
                    if (isHeroStay == false) 
                    {
                        NewdHeroList.Add(item);
                    }
                }

                for (int i = 0; i < NewdHeroList.size; i++)//排序
                {
                    for (var j = NewdHeroList.size - 1; j > i; j--)
                    {
                        Hero heroA = NewdHeroList[i];
                        Hero heroB = NewdHeroList[j];
                        if (heroA.force < heroB.force)
                        {
                            var temp = NewdHeroList[i];
                            NewdHeroList[i] = NewdHeroList[j];
                            NewdHeroList[j] = temp;
                        }
                    }
                }

                if (CharacterRecorder.instance.level < 25)
                {
                    PositionCount = 5 - PositionCount;
                }
                else 
                {
                    PositionCount = 6 - PositionCount;
                }
                for (int i = 0; i < PositionCount; i++)
                {
                    HeroInfo hi = TextTranslator.instance.GetHeroInfoByHeroID(NewdHeroList[i].cardID);
                    if (hi.heroArea == 1)
                    {
                        OneKeyCleanHeroOnBottom(NewdHeroList[i].characterRoleID, PositionFList[0]);
                        PositionFList.RemoveAt(0);
                    }
                    else 
                    {
                        OneKeyCleanHeroOnBottom(NewdHeroList[i].characterRoleID, PositionSList[0]);
                        PositionSList.RemoveAt(0);
                    }
                }
            };
        }
        #endregion
    }


    void OneKeyCleanHeroOnBottom(int NewdHeroListcharacterRoleID, int _CharacterPosition) 
    {
        for (int i = 0; i < headlist.Count; i++)
        {
            if (headlist[i].name == NewdHeroListcharacterRoleID.ToString())
            {
                headlist[i].transform.FindChild("Mask").gameObject.SetActive(true);
                GameObject go = headlist[i].transform.FindChild(headlist[i].name).gameObject;
                int _pointIndex;
                switch (_CharacterPosition)
                {
                    case 31: _pointIndex = 2; break;//拖曳到阵型对应的位置编号
                    case 36: _pointIndex = 1; break;
                    case 41: _pointIndex = 0; break;
                    case 32: _pointIndex = 4; break;
                    case 37: _pointIndex = 3; break;
                    case 28: _pointIndex = 7; break;
                    case 33: _pointIndex = 6; break;
                    case 38: _pointIndex = 5; break;
                    case 29: _pointIndex = 9; break;
                    case 34: _pointIndex = 8; break;
                    case 25: _pointIndex = 12; break;
                    case 30: _pointIndex = 11; break;
                    //case 35: _pointIndex = 10; break;
                    default: _pointIndex = 10; break;
                }
                GameObject clone = NGUITools.AddChild(SetPointList[_pointIndex], go);  //初始化时对应坐标的显示在蒙版上，下面hero显示暗色
                //GameObject clone = NGUITools.AddChild(SetPointList[item._CharacterPosition-2], go);
                clone.transform.localPosition = new Vector3(0, 0, 0);
                clone.transform.localScale = Vector3.one;
                clone.name = headlist[i].name;
                clone.GetComponent<UISprite>().enabled = true;
                clone.GetComponent<BoxCollider>().enabled = true;
                clone.GetComponent<BoxCollider>().size = new Vector3(120, 120, 0);
                clone.GetComponent<MyDragObj>().enabled = true;
                clone.GetComponent<MyDragObj>().cloneOnDrag = false;
                clone.tag = "lineup";
                headlist[i].transform.FindChild(headlist[i].name).GetComponent<UISprite>().color = Color.gray;//灰色
                headlist[i].transform.FindChild(headlist[i].name).GetComponent<BoxCollider>().size = Vector3.zero;
            }
        }
    }




    //所有拥有的英雄信息
    void SetHeroListInfo()
    {
        headlist.Clear();
        int HeroNum = CharacterRecorder.instance.ownedHeroList.size;
        foreach (var item in CharacterRecorder.instance.ownedHeroList)
        {
            Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(item.characterRoleID);
            GameObject obj = NGUITools.AddChild(uiGrid, HeroHeadPrafeb);
            if (HeroNum >= 9) 
            {
                obj.GetComponent<UIDragScrollView>().enabled = true;
            }
            obj.GetComponent<RankPositionHeadItem>().SetRankPositionHeadItem(item);
            obj.transform.FindChild(item.characterRoleID.ToString()).gameObject.GetComponent<MyDragObj>().CharacterRoleID = item.characterRoleID;
            obj.transform.FindChild(item.characterRoleID.ToString()).GetComponent<UISprite>().spriteName = item.cardID.ToString();
            uiGrid.GetComponent<UIGrid>().repositionNow = true;
            headlist.Add(obj);
        }
    }
    void SetPosition()      
    {
        for (int i = 0; i < SetPointList.Length; i++)
        {
            if (SetPointList[i].transform.childCount > 0)//判断蒙版下面是否拥有hero位置
            {
                //Debug.LogError(11);
                GameObject go = SetPointList[i].transform.GetChild(0).gameObject;
                DestroyImmediate(go);
            }
        }
        PVPWindow rw = GameObject.Find("PVPWindow").GetComponent<PVPWindow>();
        foreach (var item in rw.mTeamPosition) 
        {
            for (int i = 0; i < headlist.Count; i++)
            {
                if (headlist[i].name == item._CharacterID.ToString())
                {
                    headlist[i].transform.FindChild("Mask").gameObject.SetActive(true);
                    GameObject go=headlist[i].transform.FindChild(headlist[i].name).gameObject;
                    
                    int _pointIndex;
                    switch (item._CharacterPosition)
                    {
                        //case 38: _pointIndex = 2; break;
                        //case 43: _pointIndex = 5; break;
                        //case 34: _pointIndex = 0; break;
                        //case 39: _pointIndex = 3; break;
                        //case 44: _pointIndex = 6; break;
                        //case 35: _pointIndex = 1; break;
                        //case 40: _pointIndex = 4; break;
                        //default: _pointIndex = 6; break;

                        case 31: _pointIndex = 2; break;//拖曳到阵型对应的位置编号
                        case 36: _pointIndex = 1; break;
                        case 41: _pointIndex = 0; break;
                        case 32: _pointIndex = 4; break;
                        case 37: _pointIndex = 3; break;
                        case 28: _pointIndex = 7; break;
                        case 33: _pointIndex = 6; break;
                        case 38: _pointIndex = 5; break;
                        case 29: _pointIndex = 9; break;
                        case 34: _pointIndex = 8; break;
                        case 25: _pointIndex = 12; break;
                        case 30: _pointIndex = 11; break;
                        //case 35: _pointIndex = 10; break;
                        default: _pointIndex = 10; break;
                    }
                    GameObject clone = NGUITools.AddChild(SetPointList[_pointIndex], go);  //初始化时对应坐标的显示在蒙版上，下面hero显示暗色
                    //GameObject clone = NGUITools.AddChild(SetPointList[item._CharacterPosition-2], go);
                    clone.transform.localPosition = new Vector3(0,0,0);
                    clone.transform.localScale = Vector3.one;
                    clone.name = headlist[i].name;
                    clone.GetComponent<UISprite>().enabled = true;
                    clone.GetComponent<BoxCollider>().enabled = true;
                    clone.GetComponent<BoxCollider>().size = new Vector3(120, 120, 0);
                    clone.GetComponent<MyDragObj>().enabled = true;
                    clone.GetComponent<MyDragObj>().cloneOnDrag = false;
                    clone.tag = "lineup";
                    headlist[i].transform.FindChild(headlist[i].name).GetComponent<UISprite>().color = Color.gray;//灰色
                    headlist[i].transform.FindChild(headlist[i].name).GetComponent<BoxCollider>().size = Vector3.zero;
                    if (clone.name == rw.mTeamPosition[rw.mTeamPosition.Count - 1]._CharacterID.ToString()) 
                    {
                        clone.transform.Find("LabelCaptain").gameObject.SetActive(true);
                    }
                }
            }
            //GameObject obj=NGUITools.AddChild(SetPointList[item._CharacterPosition-10],)
        }

    }

    void SetPosition2()
    {
        for (int i = 0; i < SetPointList.Length; i++)
        {
            if (SetPointList[i].transform.childCount > 0)//判断蒙版下面是否拥有hero位置
            {
                //Debug.LogError(11);
                GameObject go = SetPointList[i].transform.GetChild(0).gameObject;
                DestroyImmediate(go);
            }
        }
        //if (GameObject.Find("LegionWarWindow") != null) 
        //{
        //    LegionWarWindow rw = GameObject.Find("LegionWarWindow").GetComponent<LegionWarWindow>();
        //}
        //else if (GameObject.Find("PVPWindow") != null) 
        //{
        //    PVPWindow rw = GameObject.Find("PVPWindow").GetComponent<PVPWindow>();
        //}
        LegionWarWindow rw = GameObject.Find("LegionWarWindow").GetComponent<LegionWarWindow>();
        foreach (var item in rw.mTeamPosition)
        {
            for (int i = 0; i < headlist.Count; i++)
            {
                if (headlist[i].name == item._CharacterID.ToString())
                {
                    headlist[i].transform.FindChild("Mask").gameObject.SetActive(true);
                    GameObject go = headlist[i].transform.FindChild(headlist[i].name).gameObject;

                    int _pointIndex;
                    switch (item._CharacterPosition)
                    {

                        case 31: _pointIndex = 2; break;//拖曳到阵型对应的位置编号
                        case 36: _pointIndex = 1; break;
                        case 41: _pointIndex = 0; break;
                        case 32: _pointIndex = 4; break;
                        case 37: _pointIndex = 3; break;
                        case 28: _pointIndex = 7; break;
                        case 33: _pointIndex = 6; break;
                        case 38: _pointIndex = 5; break;
                        case 29: _pointIndex = 9; break;
                        case 34: _pointIndex = 8; break;
                        case 25: _pointIndex = 12; break;
                        case 30: _pointIndex = 11; break;
                        //case 35: _pointIndex = 10; break;
                        default: _pointIndex = 10; break;
                    }
                    GameObject clone = NGUITools.AddChild(SetPointList[_pointIndex], go);  //初始化时对应坐标的显示在蒙版上，下面hero显示暗色
                    //GameObject clone = NGUITools.AddChild(SetPointList[item._CharacterPosition-2], go);
                    clone.transform.localPosition = new Vector3(0, 0, 0);
                    clone.transform.localScale = Vector3.one;
                    clone.name = headlist[i].name;
                    clone.GetComponent<UISprite>().enabled = true;
                    clone.GetComponent<BoxCollider>().enabled = true;
                    clone.GetComponent<BoxCollider>().size = new Vector3(120, 120, 0);
                    clone.GetComponent<MyDragObj>().enabled = true;
                    clone.GetComponent<MyDragObj>().cloneOnDrag = false;
                    clone.tag = "lineup";
                    headlist[i].transform.FindChild(headlist[i].name).GetComponent<UISprite>().color = Color.gray;//灰色
                    headlist[i].transform.FindChild(headlist[i].name).GetComponent<BoxCollider>().size = Vector3.zero;
                    if (clone.name == rw.mTeamPosition[rw.mTeamPosition.Count - 1]._CharacterID.ToString())
                    {
                        clone.transform.Find("LabelCaptain").gameObject.SetActive(true);
                    }
                }
            }
        }

    }


    public void SetCaptain(string Heroname) //设置队长
    {
        Captainlist.Clear();
        for (int i = 0; i < SetPointList.Length; i++)
        {
            if (SetPointList[i].transform.childCount > 0)//判断蒙版下面是否拥有hero位置
            {
                GameObject go = SetPointList[i].transform.GetChild(0).gameObject;
                Captainlist.Add(go);
            }
        }
        for (int i = 0; i < Captainlist.Count; i++) 
        {
            if (Captainlist[i].name == Heroname) 
            {
                Captainlist[i].transform.Find("LabelCaptain").gameObject.SetActive(true);
            }
            else
            {
                Captainlist[i].transform.Find("LabelCaptain").gameObject.SetActive(false);
            }
            //Debug.LogError(Captainlist[i].name);
            //if (UIEventListener.Get(Captainlist[i]).onClick == null) 
            //{
            //    Debug.LogError("////////" + i);

            //    UIEventListener.Get(Captainlist[i]).onClick += delegate(GameObject go)
            //    {
            //        Debug.LogError("********" + i);

            //        SetOtherCaptainOff(i);
            //    };
            //}
        }
    }

    private void ClickShopTypeButton(GameObject go,int num)
    {
        SetOtherCaptainOff(num);
    }
    void SetOtherCaptainOff(int num) 
    {
        for (int i = 0; i < Captainlist.Count; i++) 
        {
            if (i == num)
            {
                Captainlist[i].transform.Find("LabelCaptain").gameObject.SetActive(true);
            }
            else 
            {
                Captainlist[i].transform.Find("LabelCaptain").gameObject.SetActive(false);
            }
        }
    }
}
