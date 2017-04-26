using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionPositionWindow : MonoBehaviour
{

    public GameObject uiGrid;
    public GameObject HeroHeadPrafeb;
    public GameObject closeButton;
    public GameObject[] SetPointList;
    public GameObject AKeyButton;

    List<GameObject> Captainlist = new List<GameObject>();
    List<GameObject> headlist = new List<GameObject>();

    public List<LegionFightTeamPosition> mTeamPosition = new List<LegionFightTeamPosition>();

    private List<LegionFightTeamPosition> TeamPositionOne = new List<LegionFightTeamPosition>();
    private List<LegionFightTeamPosition> TeamPositionTwo = new List<LegionFightTeamPosition>();
    private List<LegionFightTeamPosition> TeamPositionThree = new List<LegionFightTeamPosition>();

    private int LegionPoint = 0;
    private int CityPoint = 0;
    private int cityType = 0;

    void Start()
    {
        foreach (Transform tran in GetComponentsInChildren<Transform>())
        {
            tran.gameObject.layer = 9;
        }
        //if (CharacterRecorder.instance.MarinesTabe != 0)//点击了非骚扰点
        //{
        //    cityType = 5;
        //}
        //else
        //{
        //    cityType = TextTranslator.instance.GetLegionCityByID(CharacterRecorder.instance.LegionHarasPoint).CityType;
        //}
        cityType = 5;
        Debug.LogError("MarinesTabe " + CharacterRecorder.instance.MarinesTabe);
        Debug.LogError("cityType " + cityType);

        //if (cityType < 5)
        //{
        //    SetHeroListInfo();
        //    SetPosition();
        //}
        //else 
        //{
        //    SetHeroListInfo2();
        //    SetPosition2();
        //}

        SetHeroListInfo2();
        SetPosition2();
        this.transform.Find("All/DownContent/DownChild").GetComponent<TweenScale>().ResetToBeginning();
        this.transform.Find("All/DownContent/DownChild").GetComponent<TweenScale>().PlayForward();
        if (UIEventListener.Get(closeButton).onClick == null)
        {
            UIEventListener.Get(closeButton).onClick += delegate(GameObject go)
            {
                DestroyImmediate(this.gameObject);
            };
        };
        if (UIEventListener.Get(GameObject.Find("SaveButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("SaveButton")).onClick += delegate(GameObject go)
            {
                int HeroNum = 0;
                string TeamID = "";
                string CaptainName="";
                int CaptionPosition = 0;
                for (int i = 0; i < SetPointList.Length; i++)
                {
                    if (SetPointList[i].transform.childCount != 0)
                    {
                        HeroNum++;
                        int position;
                        switch (i)
                        {
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

                        //if (cityType < 5)
                        //{
                        //    if (TeamID == "")
                        //    {
                        //        TeamID = "{\"Ruid\":" + SetPointList[i].transform.GetChild(0).name + "," + "\"Blood\":0," + "\"Pos\":" + position + "}";
                        //    }
                        //    else
                        //    {
                        //        TeamID = TeamID + "," + "{\"Ruid\":" + SetPointList[i].transform.GetChild(0).name + "," + "\"Blood\":0," + "\"Pos\":" + position + "}";
                        //    }
                        //}
                        //else 
                        //{
                        //    if (TeamID == "")
                        //    {
                        //        TeamID = SetPointList[i].transform.GetChild(0).name + "$" + position + "!";
                        //    }
                        //    else
                        //    {
                        //        TeamID = TeamID + SetPointList[i].transform.GetChild(0).name + "$" + position + "!";
                        //    }
                        //}

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

                Debug.LogError(TeamID);

                //if (cityType < 5)
                //{
                //    TeamID = "[" + TeamID + "]";
                //}
                //else 
                //{
                //    //TeamID = TeamID + ";";
                //}

                //int cityType = TextTranslator.instance.GetLegionCityByID(CharacterRecorder.instance.LegionHarasPoint).CityType;


                if (CaptainName != "")
                {
                    TeamID += string.Format("{0}${1}!", CaptainName, CaptionPosition);
                }

                if (cityType==5) //骚扰点
                {
                    if (HeroNum == 6)
                    {
                        NetworkHandler.instance.SendProcess("8633#" + CharacterRecorder.instance.MarinesTabe + ";" + TeamID + ";");
                        DestroyImmediate(this.gameObject);
                        
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("组成国战战队必须满6人才能组队", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else if (cityType<5) 
                {
                    if (GameObject.Find("LegionFightWindow") != null)//在战斗界面布阵
                    {
                        if (HeroNum >= 6)//在战斗界面布阵需要六人
                        {
                            CityPoint = GameObject.Find("LegionFightWindow").GetComponent<LegionFightWindow>().ClickTowerNum;
                            NetworkHandler.instance.SendProcess("8606#" + CharacterRecorder.instance.LegionHarasPoint + ";" + CityPoint + ";" + TeamID + ";");
                            DestroyImmediate(this.gameObject);
                        }
                        else
                        {
                            UIManager.instance.OpenPromptWindow("必须满6人才能组队", PromptWindow.PromptType.Hint, null, null);
                        }
                    }
                }            
            };
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

                for (int i = 0; i < mTeamPosition.Count; i++)//已经上阵和已经死亡和已经布阵的
                {
                    CleanHeroIDList.Add(mTeamPosition[i]._CharacterID);
                }

                foreach (var item in CharacterRecorder.instance.ownedHeroList) //剩下的重新集合
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

                if (NewdHeroList.size >= 6)  //剩余英雄数量需要大于5
                {
                    PositionCount = 6 - PositionCount;
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
                }
                else 
                {
                    UIManager.instance.OpenPromptWindow("必需满6人才能组队", PromptWindow.PromptType.Hint, null, null);
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
                clone.GetComponent<LegionDragObj>().enabled = true;
                clone.GetComponent<LegionDragObj>().cloneOnDrag = false;
                clone.tag = "lineup";
                headlist[i].transform.FindChild(headlist[i].name).GetComponent<UISprite>().color = Color.gray;//灰色
                headlist[i].transform.FindChild(headlist[i].name).GetComponent<BoxCollider>().size = Vector3.zero;
            }
        }
    }

    //所有拥有的英雄信息  军团战点
    void SetHeroListInfo()
    {
        mTeamPosition.Clear();

        if (CharacterRecorder.instance.LegionPositonStr != "") 
        {
            //string[] dataSplit = CharacterRecorder.instance.LegionPositonStr.Split(';');
            string[] dataSplit1 = CharacterRecorder.instance.LegionPositonStr.Split('!');
            for (int i = 0; i < dataSplit1.Length - 1; i++)
            {
                string[] secSplit = dataSplit1[i].Split('$');
                LegionFightTeamPosition mPosition = new LegionFightTeamPosition();
                mPosition._CharacterID = int.Parse(secSplit[0]);
                mPosition._CharacterPosition = int.Parse(secSplit[1]);
                mPosition._CharacterLife = int.Parse(secSplit[2]);
                mTeamPosition.Add(mPosition);
            }
        }
        Debug.Log("mTeamPosition" + mTeamPosition.Count);
        headlist.Clear();
        int HeroNum = CharacterRecorder.instance.ownedHeroList.size;

        foreach (var item in CharacterRecorder.instance.ownedHeroList) //可上阵的
        {
            bool IsHave = false;
            for (int i = 0; i < mTeamPosition.Count; i++)
            {
                if (mTeamPosition[i]._CharacterID == item.characterRoleID)
                {
                    IsHave = true;
                    break;
                }
            }
            if (IsHave == false) 
            {
                Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(item.characterRoleID);
                GameObject obj = NGUITools.AddChild(uiGrid, HeroHeadPrafeb);
                obj.SetActive(true);
                obj.GetComponent<LegionPositionHeadItem>().SetRankPositionHeadItem(item);
                obj.transform.FindChild(item.characterRoleID.ToString()).gameObject.GetComponent<LegionDragObj>().CharacterRoleID = item.characterRoleID;
                obj.transform.FindChild(item.characterRoleID.ToString()).GetComponent<UISprite>().spriteName = item.cardID.ToString();
                headlist.Add(obj);
                if (HeroNum >= 9)
                {
                    obj.GetComponent<UIDragScrollView>().enabled = true;
                }
            }
        }

        foreach (var item in CharacterRecorder.instance.ownedHeroList) //已经上阵的
        {
            for (int i = 0; i < mTeamPosition.Count; i++)
            {
                if (mTeamPosition[i]._CharacterID == item.characterRoleID && mTeamPosition[i]._CharacterLife==1)
                {
                    Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(item.characterRoleID);
                    GameObject obj = NGUITools.AddChild(uiGrid, HeroHeadPrafeb);
                    obj.SetActive(true);
                    obj.GetComponent<LegionPositionHeadItem>().SetRankPositionHeadItem(item);
                    obj.transform.FindChild(item.characterRoleID.ToString()).gameObject.GetComponent<LegionDragObj>().CharacterRoleID = item.characterRoleID;
                    obj.transform.FindChild(item.characterRoleID.ToString()).GetComponent<UISprite>().spriteName = item.cardID.ToString();
                    obj.GetComponent<LegionPositionHeadItem>().SetItemState(mTeamPosition[i]._CharacterLife);
                    headlist.Add(obj);
                    if (HeroNum >= 9)
                    {
                        obj.GetComponent<UIDragScrollView>().enabled = true;
                    }
                    break;
                }
            }
        }

        foreach (var item in CharacterRecorder.instance.ownedHeroList) //已经上阵且死亡的
        {
            for (int i = 0; i < mTeamPosition.Count; i++)
            {
                if (mTeamPosition[i]._CharacterID == item.characterRoleID && mTeamPosition[i]._CharacterLife == 0)
                {
                    Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(item.characterRoleID);
                    GameObject obj = NGUITools.AddChild(uiGrid, HeroHeadPrafeb);
                    obj.SetActive(true);
                    obj.GetComponent<LegionPositionHeadItem>().SetRankPositionHeadItem(item);
                    obj.transform.FindChild(item.characterRoleID.ToString()).gameObject.GetComponent<LegionDragObj>().CharacterRoleID = item.characterRoleID;
                    obj.transform.FindChild(item.characterRoleID.ToString()).GetComponent<UISprite>().spriteName = item.cardID.ToString();
                    obj.GetComponent<LegionPositionHeadItem>().SetItemState(mTeamPosition[i]._CharacterLife);
                    headlist.Add(obj);
                    if (HeroNum >= 9)
                    {
                        obj.GetComponent<UIDragScrollView>().enabled = true;
                    }
                    break;
                }
            }
        }
        uiGrid.GetComponent<UIGrid>().repositionNow = true;


        //foreach (var item in CharacterRecorder.instance.ownedHeroList)
        //{
        //    Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(item.characterRoleID);
        //    GameObject obj = NGUITools.AddChild(uiGrid, HeroHeadPrafeb);
        //    obj.SetActive(true);
        //    if (HeroNum >= 9)
        //    {
        //        obj.GetComponent<UIDragScrollView>().enabled = true;
        //    }
        //    obj.GetComponent<LegionPositionHeadItem>().SetRankPositionHeadItem(item);
        //    obj.transform.FindChild(item.characterRoleID.ToString()).gameObject.GetComponent<LegionDragObj>().CharacterRoleID = item.characterRoleID;
        //    obj.transform.FindChild(item.characterRoleID.ToString()).GetComponent<UISprite>().spriteName = item.cardID.ToString();            
        //    headlist.Add(obj);

        //    for (int i = 0; i < mTeamPosition.Count; i++) 
        //    {
        //        if (mTeamPosition[i]._CharacterID == item.characterRoleID) 
        //        {
        //            Debug.Log("mTeamPosition[i]._CharacterID" + item.characterRoleID);
        //            obj.GetComponent<LegionPositionHeadItem>().SetItemState(mTeamPosition[i]._CharacterLife);
        //            break;
        //        }
        //    }
        //}
        //uiGrid.GetComponent<UIGrid>().repositionNow = true;
    }


    void SetHeroListInfo2()  //骚扰点布阵
    {
        mTeamPosition.Clear();

        if (CharacterRecorder.instance.MarinesInfomation1.TeamNumberId != 0) 
        {
            string[] pataSplit1 = CharacterRecorder.instance.MarinesInfomation1.TeamInformation.Split('!');
            string[] pataSplit2 = CharacterRecorder.instance.MarinesInfomation1.BloodNumber.Split('!');
            for (int i = 0; i < pataSplit1.Length - 1; i++) 
            {
                string[] trcSplit1 = pataSplit1[i].Split('$');
                string[] trcSplit2 = pataSplit2[i].Split('$');

                LegionFightTeamPosition mPosition = new LegionFightTeamPosition();
                mPosition._CharacterID = int.Parse(trcSplit1[0]);
                mPosition._CharacterPosition = int.Parse(trcSplit1[1]);

                int life = 1;
                if (int.Parse(trcSplit2[1]) > 0)
                {
                    life = 1;
                }
                else
                {
                    life = 0;
                }
                mPosition._CharacterLife = life;
                mTeamPosition.Add(mPosition);
                TeamPositionOne.Add(mPosition);
            }
        }

        if (CharacterRecorder.instance.MarinesInfomation2.TeamNumberId != 0)
        {
            string[] pataSplit1 = CharacterRecorder.instance.MarinesInfomation2.TeamInformation.Split('!');
            string[] pataSplit2 = CharacterRecorder.instance.MarinesInfomation2.BloodNumber.Split('!');
            for (int i = 0; i < pataSplit1.Length - 1; i++)
            {
                string[] trcSplit1 = pataSplit1[i].Split('$');
                string[] trcSplit2 = pataSplit2[i].Split('$');

                LegionFightTeamPosition mPosition = new LegionFightTeamPosition();
                mPosition._CharacterID = int.Parse(trcSplit1[0]);
                mPosition._CharacterPosition = int.Parse(trcSplit1[1]);

                int life = 1;
                if (int.Parse(trcSplit2[1]) > 0)
                {
                    life = 1;
                }
                else
                {
                    life = 0;
                }
                mPosition._CharacterLife = life;
                mTeamPosition.Add(mPosition);
                TeamPositionTwo.Add(mPosition);
            }
        }

        if (CharacterRecorder.instance.MarinesInfomation3.TeamNumberId != 0)
        {
            string[] pataSplit1 = CharacterRecorder.instance.MarinesInfomation3.TeamInformation.Split('!');
            string[] pataSplit2 = CharacterRecorder.instance.MarinesInfomation3.BloodNumber.Split('!');
            for (int i = 0; i < pataSplit1.Length - 1; i++)
            {
                string[] trcSplit1 = pataSplit1[i].Split('$');
                string[] trcSplit2 = pataSplit2[i].Split('$');

                LegionFightTeamPosition mPosition = new LegionFightTeamPosition();
                mPosition._CharacterID = int.Parse(trcSplit1[0]);
                mPosition._CharacterPosition = int.Parse(trcSplit1[1]);

                int life = 1;
                if (int.Parse(trcSplit2[1]) > 0)
                {
                    life = 1;
                }
                else
                {
                    life = 0;
                }
                mPosition._CharacterLife = life;
                mTeamPosition.Add(mPosition);
                TeamPositionThree.Add(mPosition);
            }
        }


        Debug.Log("mTeamPosition" + mTeamPosition.Count);
        headlist.Clear();
        int HeroNum = CharacterRecorder.instance.ownedHeroList.size;

        foreach (var item in CharacterRecorder.instance.ownedHeroList) //可上阵的
        {
            bool IsHave = false;
            for (int i = 0; i < mTeamPosition.Count; i++)
            {
                if (mTeamPosition[i]._CharacterID == item.characterRoleID)
                {
                    IsHave = true;
                    break;
                }
            }
            if (IsHave == false)
            {
                Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(item.characterRoleID);
                GameObject obj = NGUITools.AddChild(uiGrid, HeroHeadPrafeb);
                obj.SetActive(true);
                obj.GetComponent<LegionPositionHeadItem>().SetRankPositionHeadItem(item);
                obj.transform.FindChild(item.characterRoleID.ToString()).gameObject.GetComponent<LegionDragObj>().CharacterRoleID = item.characterRoleID;
                obj.transform.FindChild(item.characterRoleID.ToString()).GetComponent<UISprite>().spriteName = item.cardID.ToString();
                headlist.Add(obj);
                if (HeroNum >= 9)
                {
                    obj.GetComponent<UIDragScrollView>().enabled = true;
                }
            }
        }

        foreach (var item in CharacterRecorder.instance.ownedHeroList) //已经上阵的
        {
            for (int i = 0; i < mTeamPosition.Count; i++)
            {
                if (mTeamPosition[i]._CharacterID == item.characterRoleID && mTeamPosition[i]._CharacterLife == 1)
                {
                    Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(item.characterRoleID);
                    GameObject obj = NGUITools.AddChild(uiGrid, HeroHeadPrafeb);
                    obj.SetActive(true);
                    obj.GetComponent<LegionPositionHeadItem>().SetRankPositionHeadItem(item);
                    obj.transform.FindChild(item.characterRoleID.ToString()).gameObject.GetComponent<LegionDragObj>().CharacterRoleID = item.characterRoleID;
                    obj.transform.FindChild(item.characterRoleID.ToString()).GetComponent<UISprite>().spriteName = item.cardID.ToString();
                    obj.GetComponent<LegionPositionHeadItem>().SetItemState(mTeamPosition[i]._CharacterLife);
                    headlist.Add(obj);
                    if (HeroNum >= 9)
                    {
                        obj.GetComponent<UIDragScrollView>().enabled = true;
                    }
                    break;
                }
            }
        }

        foreach (var item in CharacterRecorder.instance.ownedHeroList) //已经上阵且死亡的
        {
            for (int i = 0; i < mTeamPosition.Count; i++)
            {
                if (mTeamPosition[i]._CharacterID == item.characterRoleID && mTeamPosition[i]._CharacterLife == 0)
                {
                    Hero mhero = CharacterRecorder.instance.GetHeroByCharacterRoleID(item.characterRoleID);
                    GameObject obj = NGUITools.AddChild(uiGrid, HeroHeadPrafeb);
                    obj.SetActive(true);
                    obj.GetComponent<LegionPositionHeadItem>().SetRankPositionHeadItem(item);
                    obj.transform.FindChild(item.characterRoleID.ToString()).gameObject.GetComponent<LegionDragObj>().CharacterRoleID = item.characterRoleID;
                    obj.transform.FindChild(item.characterRoleID.ToString()).GetComponent<UISprite>().spriteName = item.cardID.ToString();
                    obj.GetComponent<LegionPositionHeadItem>().SetItemState(mTeamPosition[i]._CharacterLife);
                    headlist.Add(obj);
                    if (HeroNum >= 9)
                    {
                        obj.GetComponent<UIDragScrollView>().enabled = true;
                    }
                    break;
                }
            }
        }
        uiGrid.GetComponent<UIGrid>().repositionNow = true;
    }


    void SetPosition()  //判断蒙版下面是否拥有hero位置
    {
        for (int i = 0; i < SetPointList.Length; i++)
        {
            if (SetPointList[i].transform.childCount > 0)
            {
                GameObject go = SetPointList[i].transform.GetChild(0).gameObject;
                DestroyImmediate(go);
            }
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

        List<LegionFightTeamPosition> TeamPositionObj = new List<LegionFightTeamPosition>();
        if(CharacterRecorder.instance.MarinesTabe==1)
        {
            TeamPositionObj=TeamPositionOne;
        }
        else if(CharacterRecorder.instance.MarinesTabe==2)
        {
            TeamPositionObj = TeamPositionTwo;
        }
        else if(CharacterRecorder.instance.MarinesTabe==3)
        {
            TeamPositionObj = TeamPositionThree;
        }

        if (TeamPositionObj.Count > 0) 
        {
            int nowNum = 0;
            foreach (var item in TeamPositionObj)
            {
                nowNum++;
                for (int i = 0; i < headlist.Count; i++)
                {
                    if (headlist[i].name == item._CharacterID.ToString())
                    {
                        headlist[i].transform.FindChild("Mask").gameObject.SetActive(true);
                        GameObject go = headlist[i].transform.FindChild(headlist[i].name).gameObject;
                        headlist[i].GetComponent<LegionPositionHeadItem>().GetHeroRevive();//复活
                        for (int j = 0; j < mTeamPosition.Count; j++) 
                        {
                            if (headlist[i].name == mTeamPosition[j]._CharacterID.ToString()) 
                            {
                                mTeamPosition[j]._CharacterLife = 1;
                            }
                        }


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
                        clone.GetComponent<LegionDragObj>().enabled = true;
                        clone.GetComponent<LegionDragObj>().cloneOnDrag = false;
                        clone.tag = "lineup";
                        headlist[i].transform.FindChild(headlist[i].name).GetComponent<UISprite>().color = Color.gray;//灰色
                        headlist[i].transform.FindChild(headlist[i].name).GetComponent<BoxCollider>().size = Vector3.zero;


                        if (nowNum == TeamPositionObj.Count) 
                        {
                            clone.transform.Find("LabelCaptain").gameObject.SetActive(true);
                        }
                    }
                }
            }
        }
    }

    public void LegionWarRevive(int _CharacterRoleID) //8607 复活
    {
        for (int i = 0; i < headlist.Count; i++)
        {
            if (_CharacterRoleID==int.Parse(headlist[i].name))
            {
                for (int j = mTeamPosition.Count-1; j >= 0; j--)
                {
                    if (mTeamPosition[j]._CharacterID == _CharacterRoleID)
                    {
                        Debug.Log("mTeamPosition[i]._CharacterID" + _CharacterRoleID);
                        mTeamPosition.RemoveAt(j);
                        break;
                    }
                }
                headlist[i].GetComponent<LegionPositionHeadItem>().GetHeroRevive();
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
        }
    }
}


public class LegionFightTeamPosition
{
    internal int _CharacterID = 0;
    internal int _CharacterPosition = 0;
    internal int _CharacterLife = 0;
}