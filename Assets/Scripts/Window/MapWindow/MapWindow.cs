using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapWindow : MonoBehaviour
{
    public float Speed = 1f;
    public Camera MapCamera;
    public Vector3 StartMapPosition = Vector3.zero;
    public float StartMousePositionX;
    public float StartMousePositionY;
    public Vector3 MapPosition = Vector3.zero;
    public List<GameObject> MapList = new List<GameObject>();
    public List<Mapgetreslist> getreslist = new List<Mapgetreslist>();
    private bool IsMovesNoMap = true;
    private string CurButtonName = "";
    private int CurHeroPosition = 0;
    private List<int> MoveListNumber = new List<int>();
    private bool IsMoveHeroPosition = false;
    public GameObject HeroObj;
    private int CurButtonNumber = 0;
    private Vector3 velocity = Vector3.zero;
    private bool IsMoveCenert = false;
    public GameObject Dian;
    private List<GameObject> MovePointList = new List<GameObject>();
    private bool IsMoveHero = true;
    private Vector3 EachMoveHeroPosition = Vector3.zero;
    private int MoveHeroNumber = 0;
    public List<GameObject> PositionDianList = new List<GameObject>();
    private GameObject PointObj;
    private List<GameObject> PositionPositioninfo = new List<GameObject>();
    public List<GameObject> MapItemList = new List<GameObject>();
    public List<int> HeroIdList = new List<int>();
    private List<int> MapListId = new List<int>();
    float[] SimpleStar = new float[6];
    float[] MasterStar = new float[6];
    float[] ChallengeStar = new float[6];
    private GameObject GrabTerritoryObj;
    private GameObject PatrolLongObj;
    private GameObject PatrolItemObj;
    private GameObject MapUiWindowObj;


    private float EndMousePositionX;
    private float EndMousePositionY;
    private bool IsMove = false;
    private List<Vector3> BeforeLastMousePositionList = new List<Vector3>();

    List<int> ListGate = new List<int>();
    List<int> ListLevel = new List<int>();
    public List<TextTranslator.Gate> myCreamGate = new List<TextTranslator.Gate>();
    public GameObject Mask;
    public GameObject Arrow;
    public List<GameObject> getresListItem = new List<GameObject>();
    private string CloudName = "1";
    public int HeroId = 0;
    public Texture icon5;
    public Texture icon6;
    public Texture icon1;
    public Texture icon2;
    public Texture icon3;
    public Texture icon4;
    public List<GameObject> CloudList = new List<GameObject>();

    public Texture hezi;
    public Texture hezi2;
    public GameObject PassBox_efect;
    bool IsPlayBox_efect = true;

    bool IsWorldEvev = false;
    bool IsResource = false;
    bool IsEnemy = false;
    bool IsAction = false;
    bool IsDouble = false;
    //宝箱的状态
    public List<int> TheChestStatueList = new List<int>();
    public List<int> PassStatueList = new List<int>();
    public List<int> CreamStatueList = new List<int>();
    //可领取宝箱数量
    public int ReceiveNum = 0;
    //资源占领移动的点
    public int[] ResourceDot = { 10019, 10029, 10046, 10057, 10072, 10083 };
    private List<int> CloudMapList = new List<int> { 10009, 10017, 10025, 10033, 10040, 10049, 10056, 10063, 10072, 10081, 10090 };
    //判断是否点击的是ui
    private bool IsUiButtonDown = false;
    //获得当前点击的Ui的名字
    private string UiCamerName = "";
    private string DownName = "";
    //左右滑动移动速度
    float xSpeed = 250.0f;
    float ySpeed = 120.0f;
    //缩放限制系数
    float yMinLimit = -20f;
    float yMaxLimit = 80f;
    //摄像头的位置
    float x = 0.0f;
    float y = 0.0f;
    //记录上一次手机触摸位置判断用户是在左放大还是缩小手势
    Vector2 oldPosition1;
    Vector2 oldPosition2;
    //判断是否要移动地图摄像机
    bool IsMoveForCamera = true;


    List<MapNode> ListMapNode = new List<MapNode>();
    public List<GameObject> CouldList = new List<GameObject>();
    public List<GameObject> PassBoxList = new List<GameObject>();
    private int MapMaxCount = 98;
    private bool IsShowName = true;
    public List<int> MapSatrList = new List<int>();
    public List<int> CreamSatrList = new List<int>();
    public Texture yuan;
    public Texture yuan1;
    public Texture yuan2;
    public Texture yuan3;

    public int ResItemStatue = 0;
    public int ResourceMapId = 0;
    public int WorldEventId;
    public bool IsClickLeiDaLock = false;   //点击雷达发协议的锁

    public GameObject Soldier;
    float SoldierTimer;
    int SoldierIndex = 0;
    int SoldierType = 1;
    int SoldierDirection = 4;
    int SoldierFace = 1;
    Vector2 CameraPosition;
    int GoGateID;
    bool IsOpen;
    int NowGateID;
    int JumpPointNum = 2;       //超过几个点，进行闪现

    //资源对应的ID
    private List<int> ResourceMapID = new List<int> { 90001, 90002, 90003, 90004, 90005, 90006 };
    class MapNode
    {
        public int GateID;
        public int NodeState;
        public GameObject Node;
        public GameObject Dun;
        public GameObject Base;
    }
    //摄像机对应比例
    float CameraorthographicSize = 0.48f;
    // Use this for initialization
    void Start()
    {
        if (UIRootExtend.instance.isUiRootRatio != 1f)
        {
            CameraorthographicSize = 0.48f * UIRootExtend.instance.isUiRootRatio;
        }
        else
        {
            CameraorthographicSize = 0.48f;
        }
        MapCamera.orthographicSize = CameraorthographicSize;
        for (int i = 0; i < 22; i++)
        {
            ListGate.Add(TextTranslator.instance.GetChapterMaxGate(i + 1));
            if (i > 0)
                ListLevel.Add(TextTranslator.instance.GetChapterMaxLevel(i + 1));
        }
        StartMapPosition = MapCamera.transform.localPosition;
        //NetworkHandler.instance.SendProcess("1131#90001;");
        //NetworkHandler.instance.SendProcess("1131#90002;");
        //NetworkHandler.instance.SendProcess("1131#90003;");
        AddMapListId();

        myCreamGate = TextTranslator.instance.GetGateByType(2);
        MapUiWindowObj = GameObject.Find("MapUiWindow");
    }

    //i的最大值是强制引导最大的Group值
    void NewGuideHandle()
    {
        for (int i = 0; i < 4; i++)
        {
            if (TheChestStatueList[i] == 2)
            {
                IsOpen = false;
                break;
            }
        }
    }

    private void AddMapListId()
    {
        List<int> MapList = new List<int>();
        MapList = TextTranslator.instance.GetGateMapList(20000);
        for (int i = 0; i < MapList.Count; i++)
        {
            MapListId.Add(MapList[i]);
        }

        //    MapListId.Add(10101);
        //MapListId.Add(10102);
        //MapListId.Add(10103);
        //MapListId.Add(10104);
        //MapListId.Add(10105);
        //MapListId.Add(10106);
        //MapListId.Add(10201);
        //MapListId.Add(10202);
        //MapListId.Add(10203);
        //MapListId.Add(10204);
        //MapListId.Add(10205);
        //MapListId.Add(10206);
        //MapListId.Add(10301);
        //MapListId.Add(10302);
        //MapListId.Add(10303);
        //MapListId.Add(10304);
        //MapListId.Add(10305);
        //MapListId.Add(10306);
        //MapListId.Add(10401);
        //MapListId.Add(10402);
        //MapListId.Add(10403);
        //MapListId.Add(10404);
        //MapListId.Add(10406);

        for (int i = 0; i < MapMaxCount; i++)
        {
            MapNode mp = new MapNode();
            mp.GateID = MapListId[i];

            mp.NodeState = 0;
            mp.Base = MapItemList[i];
            mp.Dun = MapItemList[i].transform.Find("Dun").gameObject;
            mp.Dun.SetActive(false);
            ListMapNode.Add(mp);
        }


        //SetMapTypeUpdate();



    }

    void ShowWorldRedPoint()
    {
        GameObject go = GameObject.Find("MainWindow");
        if (go == null)
            return;
        if (ReceiveNum > 0)
        {
            //世界地图有可领取宝箱
            go.transform.Find("ButtonParent/Sprite/WorldButton/SpriteRedPoint").gameObject.SetActive(true);
        }
        else
        {
            go.transform.Find("ButtonParent/Sprite/WorldButton/SpriteRedPoint").gameObject.SetActive(false);
        }
    }

    public void InitTheChestStatue(string _StarStatue, string _PassStatue, string _CreamStatue)
    {
        TheChestStatueList.Clear();
        PassStatueList.Clear();
        CreamStatueList.Clear();
        ReceiveNum = 0;
        string[] statues = _StarStatue.Split('$');
        for (int i = 0; i < statues.Length - 1; i++)
        {
            TheChestStatueList.Add(int.Parse(statues[i]));
            //if (statues[i] == "1")
            //{
            //    ReceiveNum++;
            //}
        }
        statues = _PassStatue.Split('$');
        for (int i = 0; i < statues.Length - 1; i++)
        {
            PassStatueList.Add(int.Parse(statues[i]));
            if (statues[i] == "1")
            {
                ReceiveNum++;
            }
        }
        statues = _CreamStatue.Split('$');
        for (int i = 0; i < statues.Length - 1; i++)
        {
            CreamStatueList.Add(int.Parse(statues[i]));
            if (statues[i] == "1")
            {
                ReceiveNum++;
            }
        }
        ShowWorldRedPoint();
    }

    public bool IsLengthClick()
    {
        if (IsMoveHeroPosition)
        {
            return true;
        }
        return false;
    }

    public void ShowWorldEventIcon(int _gateID, int _Color)
    {
        WorldEvent _temp = TextTranslator.instance.GetWorldEventByID(_gateID);
        TextTranslator.Gate _gate = TextTranslator.instance.GetGateByID(_temp.GatePoint);


        GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().ShowWorldEventIcon(MapItemList[TextTranslator.instance.GetWorldEventByID(_gateID).GateID - 1], _gate.icon, _gateID, 1, _Color);
    }

    public void ShowActionEventIcon(int _actionID)
    {
        ActionEvent _action = TextTranslator.instance.GetActionEventById(_actionID);
        TextTranslator.Gate _gate = TextTranslator.instance.GetGateByID(_action.GateID);
        GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().ShowWorldEventIcon(MapItemList[_action.ForGateID - 1], _gate.icon, _actionID, 2, 6);
    }

    public bool IsOpenCream(int _group)
    {
        if (_group < 0 || _group >= 99)
        {
            Debug.Log("精英本Group参数有误！" + _group);
            return false;
        }
        int _CurGateStarNum = 0;
        List<TextTranslator.Gate> myGate = TextTranslator.instance.GetGroupGate(_group, 1);
        for (int i = 0; i < myGate.Count; i++)
        {
            if (MapSatrList[myGate[i].id - 10001] == 3)
            {
                _CurGateStarNum += 3;
            }
        }
        if (_CurGateStarNum == myGate.Count * 3)
        {
            return true;
        }
        //Debug.Log("是否打开精英关判断：" + PassStatueList[_group - 1].ToString() + "-group- " + _group.ToString());
        return false;
    }
    //参数为点的位置
    public void CreamNewGuidePosition(int pointIndex)
    {
        if (pointIndex < 1 || pointIndex > ListMapNode.Count)
        {
            Debug.LogError("CreamNewGuidePosition方法参数传递错误！" + pointIndex);
            return;
        }
        StartCoroutine(ChangeCamera(new Vector2(ListMapNode[pointIndex - 1].Base.transform.position.x, ListMapNode[pointIndex - 1].Base.transform.position.z), pointIndex + 10000, false));
    }

    public int UpdateLeiDaState(bool isDel = false)
    {
        //Debug.LogError("wenzhen----------------------------- " + CharacterRecorder.instance.lastGateID + "    " + TextTranslator.instance.GetChapterMaxGate(CharacterRecorder.instance.mapID));
        int _nowGateID = CharacterRecorder.instance.lastGateID;
        if (_nowGateID > TextTranslator.instance.GetChapterMaxGate(CharacterRecorder.instance.mapID))
        {
            //Debug.LogError("wenzhen----------------------------- " + CharacterRecorder.instance.level + "    " + CharacterRecorder.instance.mapID + "   " + ListLevel[CharacterRecorder.instance.mapID - 1]);
            if (CharacterRecorder.instance.level >= ListLevel[CharacterRecorder.instance.mapID - 1])
            {
                _nowGateID = TextTranslator.instance.GetChapterMaxGate(CharacterRecorder.instance.mapID);
                CloudList[CharacterRecorder.instance.mapID - 1].transform.GetChild(0).gameObject.renderer.material.mainTexture = (Texture)Resources.Load("MapTexture/MapCloud/leida1");
                GameObject go = GameObject.Instantiate(Resources.Load("Prefab/Effect/wf_LeiDa_icon_eff", typeof(GameObject)), CloudList[CharacterRecorder.instance.mapID - 1].transform.GetChild(0).position, Quaternion.identity) as GameObject;
                go.name = "wf_LeiDa_icon_eff";
                AudioEditer.instance.PlayOneShot("ui_unlock");
                GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().SetInstarMapName(CloudList[CharacterRecorder.instance.mapID - 1].transform.GetChild(0).gameObject, "点击开启", 0, "ClickOpen");

                if (isDel)
                {
                    GameObject _obj = GameObject.Find("Level" + ListLevel[CharacterRecorder.instance.mapID - 1]);
                    if (_obj != null)
                    {
                        DestroyImmediate(_obj);
                    }
                }
            }
            if (isDel)
            {
                GameObject _obj = GameObject.Find("Gate" + TextTranslator.instance.GetChapterMaxGate(CharacterRecorder.instance.mapID));
                if (_obj != null)
                {
                    DestroyImmediate(_obj);
                }
            }
        }

        return _nowGateID;
    }

    public void SetMapTypeUpdate()
    {
        if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) >= 18)
        {
            GameObject _mainCamera = GameObject.Find("MainCamera");
            if (_mainCamera.GetComponent<MouseClick>().MapCameraPosition != Vector3.zero)
            {
                MapCamera.transform.position = _mainCamera.GetComponent<MouseClick>().MapCameraPosition;
                IsMoveForCamera = false;
            }
        }
        else
        {
            Debug.Log("进入新手引导！" + MapCamera.transform.position);
        }
        ShowCloud();
        if (NowGateID != CharacterRecorder.instance.lastGateID)
        {
            AudioEditer.instance.PlayOneShot("ui_unlock");
        }
        //NowGateID = CharacterRecorder.instance.lastGateID;

        DestroyImmediate(GameObject.Find("wf_LeiDa_icon_eff"));
        NowGateID = UpdateLeiDaState();

        //Debug.LogError(NowGateID);

        if (NowGateID == 0)
        {
            NowGateID = 10001;
        }
        else
        {
            TextTranslator.Gate _gate = TextTranslator.instance.GetGateByID(CharacterRecorder.instance.lastGateID);
            NowGateID = _gate.group + 10000;
        }

        if (ListMapNode.Count > 0)
        {
            for (int i = 0; i < MapMaxCount; i++)
            {
                //if (MapListId[i] == ListMapNode[i].GateID)
                //{

                if (NowGateID == ListMapNode[i].GateID)
                {
                    if (i > 0)
                    {
                        if (CharacterRecorder.instance.standGateID == -1)
                        {
                            int _heroPosIndex = 0;
                            for (int j = 0; j < CloudList.Count; j++)
                            {
                                if (CloudList[j].activeSelf)
                                {
                                    Debug.Log("判断是否开云了：" + j);
                                    //地图点是否开放
                                    if (CharacterRecorder.instance.lastGateID > ListGate[j])
                                    {
                                        Debug.Log("判断是否开启了对应关卡：" + ListGate[j]);
                                        if (i - 1 >= 0)
                                        {
                                            _heroPosIndex = i - 1;
                                        }
                                        else
                                        {
                                            _heroPosIndex = 0;
                                        }

                                    }
                                    else
                                    {
                                        _heroPosIndex = i;
                                    }
                                    break;
                                }
                            }
                            HeroObj.transform.localPosition = new Vector3(MapItemList[_heroPosIndex].transform.localPosition.x, MapItemList[_heroPosIndex].transform.localPosition.y, -0.32f);
                            CurHeroPosition = _heroPosIndex;
                            CharacterRecorder.instance.standGateID = _heroPosIndex;
                        }
                        else
                        {
                            HeroObj.transform.localPosition = new Vector3(MapItemList[CharacterRecorder.instance.standGateID].transform.localPosition.x, MapItemList[CharacterRecorder.instance.standGateID].transform.localPosition.y, -0.32f);
                            CurHeroPosition = CharacterRecorder.instance.standGateID + 1;
                        }
                    }
                    else
                    {
                        HeroObj.transform.localPosition = new Vector3(MapItemList[0].transform.localPosition.x, MapItemList[0].transform.localPosition.y, -0.32f);
                        CurHeroPosition = 1;
                    }
                }

                if (NowGateID >= ListMapNode[i].GateID)
                {
                    ListMapNode[i].Base.GetComponent<MeshCollider>().enabled = true;
                    //Debug.LogError("Dun" + MapList[CharacterRecorder.instance.mapID - 1].activeSelf + " " + CharacterRecorder.instance.mapID );
                    if (NowGateID == ListMapNode[i].GateID)
                    {
                        //Debug.LogError("Dun" + TextTranslator.instance.GetGateCharterByGroupID(NowGateID));
                        if (TextTranslator.instance.GetGateCharterByGroupID(NowGateID - 10000) <= CharacterRecorder.instance.mapID)
                            ListMapNode[i].Dun.SetActive(true);
                        Arrow.transform.parent = ListMapNode[i].Dun.transform;
                        Arrow.SetActive(true);
                        Arrow.transform.localPosition = Vector3.zero;
                    }
                    else
                    {
                        ListMapNode[i].Dun.SetActive(false);
                    }


                    if (MapItemList[i].transform.FindChild("Bg") != null)
                    {
                        ListMapNode[i].Base.renderer.material.mainTexture = yuan1;
                        //  ListMapNode[i].Dun.SetActive(true);
                    }
                    else
                    {
                        //  ListMapNode[i].Dun.SetActive(true);
                        ListMapNode[i].Base.renderer.material.mainTexture = yuan3;
                    }
                }
                else
                {
                    ListMapNode[i].Base.GetComponent<MeshCollider>().enabled = false;
                }
                //}
            }

            if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 12 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 4)
            {
                foreach (var mn in ListMapNode)
                {
                    if (mn.GateID == 10003)
                    {
                        StartCoroutine(ChangeCamera(new Vector2(mn.Base.transform.position.x, mn.Base.transform.position.z), 10003, false));
                        break;
                    }
                }
            }
            else
            {
                if (CharacterRecorder.instance.cameraGateID != 0)//启动精英关新手引导
                {
                    IsMoveForCamera = true;
                    CreamNewGuidePosition(CharacterRecorder.instance.cameraGateID);
                    GameObject _mapUiWindow = GameObject.Find("MapUiWindow");
                    if (_mapUiWindow != null)
                    {
                        _mapUiWindow.GetComponent<MapUiWindow>().ChoiceWindow(7);
                    }

                    StartCoroutine(SceneTransformer.instance.NewbieGuide());
                    CharacterRecorder.instance.cameraGateID = 0;
                }
                else
                {
                    foreach (var mn in ListMapNode)
                    {
                        if (mn.GateID == NowGateID)
                        {
                            StartCoroutine(ChangeCamera(new Vector2(mn.Base.transform.position.x, mn.Base.transform.position.z), NowGateID, false));
                            break;
                        }
                    }
                }
            }
            if (CharacterRecorder.instance.gotoGateID != -1)
            {
                StopAllCoroutines();
                //int GoID = CharacterRecorder.instance.gotoGateID % 10000 - 1;

                if (MapSatrList.Count < CharacterRecorder.instance.lastGateID - 10000)
                {
                    if (CharacterRecorder.instance.lastGateID == CharacterRecorder.instance.gotoGateID)
                    {
                        CharacterRecorder.instance.gotoGateID -= 1;
                    }
                }

                int GoID = TextTranslator.instance.GetGateByID(CharacterRecorder.instance.gotoGateID).group - 1;
                if (CharacterRecorder.instance.gotoGateID > 20000)
                {
                    if (CharacterRecorder.instance.lastCreamGateID == CharacterRecorder.instance.gotoGateID)
                    {
                        //if (MapSatrList.Count < (CharacterRecorder.instance.lastGateID - 10000))
                        {
                            int NewGoID = TextTranslator.instance.GetGateByID(CharacterRecorder.instance.lastGateID - 1).group - 1;
                            if (GoID > NewGoID)
                            {
                                CharacterRecorder.instance.gotoGateID -= 1;
                                GoID = TextTranslator.instance.GetGateByID(CharacterRecorder.instance.gotoGateID).group - 1;
                            }
                        }
                    }
                }

                ButtonOnlick(MapList[GoID], GoID + 1);
                if (CharacterRecorder.instance.gotoGateID > 20000)
                {
                    //GoGateID = (CharacterRecorder.instance.gotoGateID - 10000);
                    //GoGateID = TextTranslator.instance.GetGateByID(CharacterRecorder.instance.gotoGateID).group + 10000;
                    List<TextTranslator.Gate> myGate = TextTranslator.instance.GetGroupGate(TextTranslator.instance.GetGateByID(CharacterRecorder.instance.gotoGateID).group, 2);
                    for (int i = 0; i < myGate.Count; i++)
                    {
                        if (CharacterRecorder.instance.gotoGateID == myGate[i].id)
                        {
                            if (!IsWorldEvev && !IsAction && !IsResource && !IsEnemy)
                            {
                                CharacterRecorder.instance.InitSelectCreamGate = i + 1;
                            }
                        }
                    }
                    GoGateID = TextTranslator.instance.GetGateByID(CharacterRecorder.instance.gotoGateID).group + 10000;
                }
                else
                {
                    if (CharacterRecorder.instance.gotoGateID > 10000 && CharacterRecorder.instance.gotoGateID < 20000)
                    {

                        List<TextTranslator.Gate> myGate = TextTranslator.instance.GetGroupGate(TextTranslator.instance.GetGateByID(CharacterRecorder.instance.gotoGateID).group, 1);
                        for (int i = 0; i < myGate.Count; i++)
                        {
                            if (CharacterRecorder.instance.gotoGateID == myGate[i].id)
                            {
                                if (!IsWorldEvev && !IsAction && !IsResource && !IsEnemy)
                                {
                                    CharacterRecorder.instance.InitSelectGate = i + 1;
                                }
                            }
                        }
                    }

                    GoGateID = TextTranslator.instance.GetGateByID(CharacterRecorder.instance.gotoGateID).group + 10000;

                }
                CameraPosition = new Vector2(MapList[GoID].transform.position.x, MapList[GoID].transform.position.z);
                IsOpen = true;
                if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 5 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 2)
                {
                    Debug.LogError("HHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHHH");
                    IsOpen = false;
                }
            }
        }
    }
    //函数返回真为放大，返回假为缩小
    bool isEnlarge(Vector2 oP1, Vector2 oP2, Vector2 nP1, Vector2 nP2)
    {
        //函数传入上一次触摸两点的位置与本次触摸两点的位置计算出用户的手势
        if (oP1.x != 0 || oP1.y != 0 || oP2.x != 0 || oP2.y != 0)
        {
            float leng1 = Mathf.Sqrt((oP1.x - oP2.x) * (oP1.x - oP2.x) + (oP1.y - oP2.y) * (oP1.y - oP2.y));
            float leng2 = Mathf.Sqrt((nP1.x - nP2.x) * (nP1.x - nP2.x) + (nP1.y - nP2.y) * (nP1.y - nP2.y));
            if (Math.Abs(leng1 - leng2) > 10f)
            {
                if (leng1 < leng2)
                {
                    //放大手势
                    if (MapCamera.orthographicSize > CameraorthographicSize)
                    {
                        MapCamera.orthographicSize -= 0.02f;
                        MoveCamreVector();
                    }
                    return true;
                }
                else
                {
                    if (MapCamera.orthographicSize < 0.8f)
                    {
                        MapCamera.orthographicSize += 0.02f;
                        MoveCamreVector();
                    }

                    //缩小手势
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    IEnumerator ChangeCamera(Vector2 CameraPosition, int GateID, bool IsOpenGate)
    {
        GameObject _mainCamera = GameObject.Find("MainCamera");
        IsWorldEvev = _mainCamera.GetComponent<MouseClick>().IsWorldEvev;
        IsResource = _mainCamera.GetComponent<MouseClick>().IsResource;
        IsEnemy = _mainCamera.GetComponent<MouseClick>().IsEnemy;
        IsAction = _mainCamera.GetComponent<MouseClick>().IsAction;
        int Direction = 0;

        if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) < 18)
        {
            NewGuideHandle();
        }

        if (IsOpenGate && !IsWorldEvev && !IsResource && !IsEnemy && !IsAction)
        {
            if (CameraPosition.y <= -0.95f && CameraPosition.x >= 0.8f)
            {
                CameraPosition.y += 0.33f;
                CameraPosition.x -= 0.485f;
                Direction = 6;
            }
            else if (CameraPosition.y >= 0.8f && CameraPosition.x >= 0.8f)
            {
                CameraPosition.y -= 0.33f;
                CameraPosition.x -= 0.485f;
                Direction = 5;
            }
            else if (CameraPosition.y >= 0.8f && CameraPosition.x <= 0.8f)
            {
                CameraPosition.y -= 0.33f;
                CameraPosition.x += 0.485f;
                Direction = 8;
            }
            else if (CameraPosition.y <= -0.95f && CameraPosition.x <= 0.8f)
            {
                CameraPosition.y += 0.33f;
                CameraPosition.x += 0.485f;
                Direction = 7;
            }
            else if (CameraPosition.y <= -0.95f)
            {
                CameraPosition.y += 0.38f;
                Direction = 2;
            }
            else if (CameraPosition.x <= -0.8f)
            {
                CameraPosition.x += 0.525f;
                Direction = 1;
            }
            else if (CameraPosition.x >= 0.8f)
            {
                CameraPosition.x -= 0.525f;
                Direction = 3;
            }
            else if (CameraPosition.y >= 0.8f)
            {
                CameraPosition.y -= 0.38f;
                Direction = 4;
            }
            else
            {
                Direction = 1;
                CameraPosition.x += 0.525f;
            }
        }
        //else if (IsEnemy)
        //{
        //    if (CameraPosition.y <= -0.8f)
        //    {
        //        CameraPosition.y += 0.34f;
        //        Direction = 2;
        //    }
        //    else if (CameraPosition.x <= -0.8f)
        //    {
        //        CameraPosition.x += 0.46f;
        //        Direction = 1;
        //    }
        //    else if (CameraPosition.x >= 0.8f)
        //    {
        //        CameraPosition.x -= 0.46f;
        //        Direction = 3;
        //    }
        //    else if (CameraPosition.y >= 0.8f)
        //    {
        //        CameraPosition.y -= 0.34f;
        //        Direction = 4;
        //    }
        //    else
        //    {
        //        Direction = 1;
        //        CameraPosition.x += 0.46f;
        //    }
        //}
        else if (IsResource || IsWorldEvev || IsAction || IsEnemy)
        {
            if (CameraPosition.y <= -0.95f && CameraPosition.x >= 0.8f)
            {
                CameraPosition.y += 0.34f;
                CameraPosition.x -= CameraorthographicSize;
                Direction = 6;
            }
            else if (CameraPosition.y >= 0.8f && CameraPosition.x >= 0.8f)
            {
                CameraPosition.y -= 0.34f;
                CameraPosition.x -= CameraorthographicSize;
                Direction = 5;
            }
            else if (CameraPosition.y >= 0.8f && CameraPosition.x <= 0.8f)
            {
                CameraPosition.y -= 0.34f;
                CameraPosition.x += CameraorthographicSize;
                Direction = 8;
            }
            else if (CameraPosition.y <= -0.95f && CameraPosition.x <= 0.8f)
            {
                CameraPosition.y += 0.34f;
                CameraPosition.x += CameraorthographicSize;
                Direction = 7;
            }
            else if (CameraPosition.y <= -0.8f)
            {
                CameraPosition.y += 0.43f;
                Direction = 2;
            }
            else if (CameraPosition.x <= -0.8f)
            {
                CameraPosition.x += 0.555f;
                Direction = 1;
            }
            else if (CameraPosition.x >= 0.8f)
            {
                CameraPosition.x -= 0.55f;
                Direction = 3;
            }
            else if (CameraPosition.y >= 0.8f)
            {
                CameraPosition.y -= 0.42f;
                Direction = 4;
            }
            else
            {
                Direction = 1;
                CameraPosition.x += 0.555f;
            }
        }
        else
        {
            if (CameraPosition.y <= -0.95f && CameraPosition.x >= 0.8f)
            {
                CameraPosition.y += 0.33f;
                CameraPosition.x -= 0.485f;
                Direction = 6;
            }
            else if (CameraPosition.y >= 0.8f && CameraPosition.x >= 0.8f)
            {
                CameraPosition.y -= 0.33f;
                CameraPosition.x -= 0.485f;
                Direction = 5;
            }
            else if (CameraPosition.y >= 0.8f && CameraPosition.x <= 0.8f)
            {
                CameraPosition.y -= 0.33f;
                CameraPosition.x += 0.485f;
                Direction = 8;
            }
            else if (CameraPosition.y <= -0.95f && CameraPosition.x <= 0.8f)
            {
                CameraPosition.y += 0.33f;
                CameraPosition.x += 0.485f;
                Direction = 7;
            }
            else if (CameraPosition.y <= -0.95f)
            {
                CameraPosition.y += 0.38f;
                Direction = 2;
            }
            else if (CameraPosition.x <= -0.8f)
            {
                CameraPosition.x += 0.525f;
                Direction = 1;
            }
            else if (CameraPosition.x >= 0.8f)
            {
                CameraPosition.x -= 0.525f;
                Direction = 3;
            }
            else if (CameraPosition.y >= 0.8f)
            {
                CameraPosition.y -= 0.38f;
                Direction = 4;
            }
            else
            {
                Direction = 1;
                CameraPosition.x += 0.525f;
            }
        }

        if (!IsMoveForCamera)
        {
            IsMoveForCamera = true;
        }
        else
        {
            Vector2 AddPosition = new Vector2((MapCamera.transform.position.x - CameraPosition.x) / 10f, (MapCamera.transform.position.z - CameraPosition.y) / 10f);
            for (int i = 0; i < 10; i++)
            {
                if (MapCamera.orthographicSize > CameraorthographicSize + 0.1f)
                {
                    MapCamera.orthographicSize -= 0.1f;
                }
                MapCamera.transform.position = new Vector3(MapCamera.transform.position.x - AddPosition.x, 0.8f, MapCamera.transform.position.z - AddPosition.y);
                yield return new WaitForSeconds(0.01f);
            }
            MapCamera.orthographicSize = CameraorthographicSize;
        }
        StartMapPosition = MapCamera.transform.localPosition;


        foreach (var mn in ListMapNode)
        {
            //  Debug.Log(mn.GateID + " " + GateID);
            if (mn.GateID == GateID)
            {
                mn.Base.transform.localPosition = new Vector3(mn.Base.transform.localPosition.x, mn.Base.transform.localPosition.y, -0.3f);
            }
            else
            {
                mn.Base.transform.localPosition = new Vector3(mn.Base.transform.localPosition.x, mn.Base.transform.localPosition.y, -0.1f);
            }
        }
        if (!IsOpenGate)
        {
            yield return 0;
        }
        if (!IsAction)
        {
            if (!IsEnemy)
            {
                if (!IsWorldEvev)
                {
                    if (!IsResource)
                    {
                        if (IsOpenGate)
                        {
                            {
                                GameObject _mapUiWindow = GameObject.Find("MapUiWindow");
                                if (_mapUiWindow != null)
                                {
                                    _mapUiWindow.GetComponent<MapUiWindow>().SetArrow(Direction);
                                    _mapUiWindow.GetComponent<MapUiWindow>().ChoiceWindow(4);
                                    if (_mapUiWindow.transform.Find("All").transform.Find("GateInfoWindow") != null)
                                    {
                                        //Debug.LogError(GateID);
                                        if (GateID == 10001 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 3 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 9)
                                        {
                                            PlayerPrefs.SetInt(LuaDeliver.instance.GetGuideSubStateName(), 10);
                                            LuaDeliver.instance.UseGuideStation();
                                        }
                                        else if (GateID == 10002 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 6 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 4)
                                        {
                                            PlayerPrefs.SetInt(LuaDeliver.instance.GetGuideSubStateName(), 5);
                                            LuaDeliver.instance.UseGuideStation();
                                            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1001);
                                        }

                                        else if (GateID == 10003 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 10 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 2)
                                        {
                                            PlayerPrefs.SetInt(LuaDeliver.instance.GetGuideSubStateName(), 3);
                                            LuaDeliver.instance.UseGuideStation();
                                            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1201);
                                        }

                                        else if (GateID == 10004 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 17 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 3)
                                        {
                                            PlayerPrefs.SetInt(LuaDeliver.instance.GetGuideSubStateName(), 4);
                                            LuaDeliver.instance.UseGuideStation();
                                            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1601);
                                        }
                                        if (CharacterRecorder.instance.lastGateID == 10001)
                                        {
                                            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_703);
                                        }
                                        else if (CharacterRecorder.instance.lastGateID == 10006 && CurButtonName == "5")
                                        {
                                            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1800);
                                        }
                                        else if (CharacterRecorder.instance.lastGateID == 10008 && CurButtonName == "6")
                                        {
                                            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2100);
                                        }
                                        else if (CharacterRecorder.instance.lastGateID == 10009 && CurButtonName == "7")
                                        {
                                            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2300);
                                        }
                                        else if (CharacterRecorder.instance.lastGateID == 10011 && CharacterRecorder.instance.lastCreamGateID >= 20002 && CurButtonName == "1")
                                        {
                                            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2600);
                                        }


                                        int num = GateID - 10001;
                                        //Debug.LogError(MapSatrList.Count + " " + num);

                                        int IndexMapStart = MapSatrList[num];

                                        _mapUiWindow.transform.Find("All").transform.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().Init(GateID, 0, SimpleStar, MasterStar, ChallengeStar, IndexMapStart);
                                        Mask.SetActive(true);
                                        if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) >= 18)
                                        {
                                            CharacterRecorder.instance.gotoGateID = -1;
                                        }
                                    }
                                }
                            }

                        }
                    }
                    else
                    {
                        if (GameObject.Find("ResourcesBoard") != null)
                        {
                            GameObject _mapUiWindow = GameObject.Find("MapUiWindow");
                            if (ResItemStatue != 1)
                            {
                                NetworkHandler.instance.SendProcess("1134#" + ResourceMapId + ";");
                            }
                            else
                            {
                                _mapUiWindow.GetComponent<MapUiWindow>().ChoiceWindow(5);
                                _mapUiWindow.transform.Find("All").transform.Find("PatrolFight").GetComponent<PatrolFight>().SeInfo(ResourceMapId);
                                _mapUiWindow.transform.Find("All").transform.Find("PatrolFight").GetComponent<PatrolFight>().SetArrow(Direction);
                            }
                            if (ResItemStatue == 2)
                            {
                                _mapUiWindow.transform.Find("All").transform.Find("GrabTerritory").GetComponent<GrabTerritory>().SetArrow(Direction);
                            }
                            else if (ResItemStatue == 3)
                            {
                                _mapUiWindow.transform.Find("All").transform.Find("PatrolLong").GetComponent<PatrolLong>().SetArrow(Direction);
                            }
                            else if (ResItemStatue == 4)
                            {
                                _mapUiWindow.transform.Find("All").transform.Find("PatrolItem").GetComponent<PatrolItem>().SetArrow(Direction);
                            }
                        }
                    }
                }
                else
                {
                    GameObject _wordEvent = GameObject.Find("WordEvent");
                    if (_wordEvent == null)
                    {
                        Transform _wordList = GameObject.Find("MapUiWindow").transform.Find("All/WordEvent");
                        _wordList.gameObject.SetActive(true);
                        _wordList.Find("EventList").gameObject.SetActive(false);
                    }
                    //if (_wordEvent != null)
                    {
                        int num = WorldEventId;
                        ////NetworkHandler.instance.SendProcess("2202#" + num + ";");
                        NetworkHandler.instance.SendProcess("2202#" + num + ";");
                        GameObject.Find("MapUiWindow").transform.Find("All").transform.Find("WordEvent").Find("WorldEvenWindow").GetComponent<WordEventFight>().SetArrow(Direction);
                    }
                }
            }
            else
            {
                GameObject _mapUiWindow = GameObject.Find("MapUiWindow");
                if (_mapUiWindow != null)
                    _mapUiWindow.GetComponent<MapUiWindow>().ChoiceWindow(7);
                Transform _Enemy = _mapUiWindow.transform.Find("All/EnemyinvasionWindow");
                if (_Enemy != null)
                {
                    if (WorldEventId != 0)
                    {
                        _Enemy.gameObject.SetActive(true);
                        _Enemy.GetComponent<EnemyinvasionWindow>().SetArrow(Direction);
                        _Enemy.GetComponent<EnemyinvasionWindow>().Init(WorldEventId);
                    }

                }
                //_mainCamera.GetComponent<MouseClick>().IsEnemy = false;
            }
        }
        else
        {
            GameObject _mapUi = GameObject.Find("MapUiWindow");
            if (_mapUi != null)
            {
                _mapUi.transform.Find("All/ActionEventWindow").GetComponent<ActionEventWindow>().SetArrow(Direction);
                _mapUi.GetComponent<MapUiWindow>().ChoiceWindow(7);
                if (WorldEventId != 0)
                {
                    _mapUi.transform.Find("All/ActionEventWindow").gameObject.SetActive(true);
                    GameObject.Find("ActionEventWindow").GetComponent<ActionEventWindow>().InitActionEvent(WorldEventId);
                }
                else
                {
                    Debug.Log("千里走单骑MapWindowID出错:" + WorldEventId);
                }
            }
        }
        yield return 0;
    }

    // Update is called once per frame
    void Update()
    {
#if UNITY_EDITOR
#else
        //判断触摸数量为多点触摸
        if (Input.touchCount == 0)
        {
            IsDouble = false;
            IsMovesNoMap = false;
            StartMapPosition = MapCamera.transform.localPosition;
            oldPosition1 = Vector2.zero;
            oldPosition2 = Vector2.zero;
        }
#endif
        if (Input.touchCount > 1 && CharacterRecorder.instance.lastGateID >= 10040)
        {
            //前两只手指触摸类型都为移动触摸
            if (Input.GetTouch(0).phase == TouchPhase.Moved || Input.GetTouch(1).phase == TouchPhase.Moved)
            {
                IsDouble = true;
                //计算出当前两点触摸点的位置
                var tempPosition1 = Input.GetTouch(0).position;
                var tempPosition2 = Input.GetTouch(1).position;
                //函数返回真为放大，返回假为缩小
                if (isEnlarge(oldPosition1, oldPosition2, tempPosition1, tempPosition2))
                {
                    //放大系数超过3以后不允许继续放大
                    //这里的数据是根据我项目中的模型而调节的，大家可以自己任意修改
                }
                else
                {
                    //缩小洗漱返回18.5后不允许继续缩小
                    //这里的数据是根据我项目中的模型而调节的，大家可以自己任意修改
                }
                //备份上一次触摸点的位置，用于对比
                oldPosition1 = tempPosition1;
                oldPosition2 = tempPosition2;
            }
        }
        else
        {
            if (!IsDouble)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (GameObject.Find("MapUiWindow") != null)
                    //if (GameObject.Find("GrabTerritory") == null && GameObject.Find("PatrolLong") == null && GameObject.Find("PatrolItem") == null && GameObject.Find("GateInfoWindow") == null && GameObject.Find("PatrolFight") == null && GameObject.Find("WordWindow") == null)
                    {
                        IsMove = false;//停止一切滑动
                        if (UICamera.hoveredObject != null)
                        {
                            if (UICamera.hoveredObject.name == "UIRoot")
                            {
                                StartMapPosition = MapCamera.transform.localPosition;
                                UiCamerName = UICamera.hoveredObject.name;
                                IsUiButtonDown = false;
                                Ray ray = MapCamera.ScreenPointToRay(Input.mousePosition);
                                RaycastHit hit;
                                if (Physics.Raycast(ray, out hit))
                                {
                                    if (hit.collider.gameObject.name == "Map" || hit.collider.gameObject.name.IndexOf("Cloud") > -1)
                                    {
                                        StartMousePositionX = Input.mousePosition.x;
                                        StartMousePositionY = Input.mousePosition.y;
                                        CurButtonName = "";
                                        IsMovesNoMap = true;
                                        if (hit.collider.gameObject.name == "cloud")
                                        {
                                            CloudName = hit.collider.gameObject.GetComponent<CloudLeida>().obj.name;

                                        }
                                        else
                                        {
                                            CloudName = "";
                                        }
                                    }
                                    else
                                    {
                                        IsMovesNoMap = false;
                                        CurButtonName = hit.collider.gameObject.name;
                                    }
                                }
                            }
                            else
                            {
                                IsUiButtonDown = true;
                                UiCamerName = UICamera.hoveredObject.name;
                            }
                        }
                    }
                    //else
                    //{
                    //    DownName = UICamera.hoveredObject.name;
                    //}
                }

                if (Input.GetMouseButton(0))
                {
                    if (GameObject.Find("MapUiWindow") != null)
                    {
                        if (GameObject.Find("GrabTerritory") == null && GameObject.Find("PatrolLong") == null && GameObject.Find("PatrolItem") == null && GameObject.Find("GateInfoWindow") == null && GameObject.Find("PatrolFight") == null && GameObject.Find("WordWindow") == null)
                        {
                            if (UiCamerName == "UIRoot")
                            {
                                if (IsMovesNoMap)
                                {
                                    AddBeforeLastMousePositionList();
                                    MoveCamreVector();
                                }
                            }
                        }
                    }
                }
                //点击资源获得资源信息
                if (Input.GetMouseButtonUp(0))
                {
                    if (GameObject.Find("MapUiWindow") != null)
                    {
                        if (GameObject.Find("GrabTerritory") == null && GameObject.Find("PatrolLong") == null && GameObject.Find("PatrolItem") == null && GameObject.Find("GateInfoWindow") == null && GameObject.Find("PatrolFight") == null && GameObject.Find("WordWindow") == null)
                        {
                            //  Debug.Log("uicamername" + UiCamerName + "     " + UICamera.hoveredObject.name);
                            if (!IsUiButtonDown)
                            {
                                if (UICamera.hoveredObject != null)
                                {
                                    if (UICamera.hoveredObject.name == "UIRoot")
                                    {
                                        if (IsMovesNoMap)//yy
                                        {
                                            StartMapPosition = MapCamera.transform.localPosition;
                                            EndMousePositionX = Input.mousePosition.x;//yy
                                            EndMousePositionY = Input.mousePosition.y;//yy
                                            IsMove = true;
                                        }
                                        Ray ray = MapCamera.ScreenPointToRay(Input.mousePosition);
                                        RaycastHit hit;
                                        if (Physics.Raycast(ray, out hit))
                                        {
                                            if (CurButtonName == hit.collider.gameObject.name)
                                            {
                                                for (int i = 0; i < MapItemList.Count; i++)
                                                {
                                                    if (hit.collider.gameObject.name == MapItemList[i].name)
                                                    {
                                                        if (i < MapListId.Count)
                                                        {
                                                            CameraPosition = new Vector2(hit.collider.gameObject.transform.position.x, hit.collider.gameObject.transform.position.z);
                                                            GoGateID = MapListId[i];
                                                            IsOpen = true;
                                                            GameObject _mainCamera = GameObject.Find("MainCamera");
                                                            _mainCamera.GetComponent<MouseClick>().IsResource = false;
                                                            _mainCamera.GetComponent<MouseClick>().IsWorldEvev = false;
                                                            _mainCamera.GetComponent<MouseClick>().IsEnemy = false;
                                                            _mainCamera.GetComponent<MouseClick>().IsAction = false;

                                                            hit.collider.gameObject.GetComponent<TweenScale>().ResetToBeginning();
                                                            hit.collider.gameObject.GetComponent<TweenScale>().from = new Vector3(hit.collider.gameObject.transform.localScale.x, hit.collider.gameObject.transform.localScale.y, hit.collider.gameObject.transform.localScale.z);
                                                            hit.collider.gameObject.GetComponent<TweenScale>().to = new Vector3((hit.collider.gameObject.transform.localScale.x * 1.5f), (hit.collider.gameObject.transform.localScale.y * 1.5f), hit.collider.gameObject.transform.localScale.z);
                                                            hit.collider.gameObject.GetComponent<TweenScale>().duration = 0.2f;
                                                            hit.collider.gameObject.GetComponent<TweenScale>().PlayForward();
                                                            hit.collider.gameObject.GetComponent<TweenScale>().ResetToBeginning();
                                                            for (int j = 0; j < MapList.Count; j++)
                                                            {
                                                                if (hit.collider.gameObject.name == MapList[i].name)
                                                                {
                                                                    CharacterRecorder.instance.gotoGateID = -1;
                                                                    ButtonOnlick(MapList[i], i + 1);
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            Debug.Log("超过地图总数量");
                                                        }
                                                        return;
                                                        //NetworkHandler.instance.SendProcess("1131#1;");
                                                    }
                                                }
                                                for (int i = 0; i < getresListItem.Count; i++)
                                                {

                                                    if (hit.collider.gameObject.name == getresListItem[i].name)
                                                    {
                                                        if (CharacterRecorder.instance.lastGateID < 10038)
                                                        {
                                                            UIManager.instance.OpenPromptWindow("长官，通38关开放该功能！", PromptWindow.PromptType.Hint, null, null);
                                                            return;
                                                        }
                                                        int Number = int.Parse(getresListItem[i].name) - 100;
                                                        bool IsGetres = false;
                                                        for (int j = 0; j < getreslist.Count; j++)
                                                        {

                                                            if (TextTranslator.instance.GetResourceByID(Number).MapId == getreslist[j].GetresId)
                                                            {
                                                                IsGetres = true;
                                                            }
                                                        }
                                                        if (IsGetres)
                                                        {

                                                            NetworkHandler.instance.SendProcess("1134#" + TextTranslator.instance.GetResourceByID(Number).MapId + ";");
                                                            GameObject _mapUiWindow = GameObject.Find("MapUiWindow");
                                                            _mapUiWindow.transform.Find("All").transform.Find("GrabTerritory").GetComponent<GrabTerritory>().SetArrow(0);
                                                            _mapUiWindow.transform.Find("All").Find("PatrolItem").GetComponent<PatrolItem>().SetArrow(0);
                                                            _mapUiWindow.transform.Find("All").Find("PatrolLong").GetComponent<PatrolLong>().SetArrow(0);

                                                            float size_x = hit.collider.gameObject.transform.localScale.x;
                                                            float size_y = hit.collider.gameObject.transform.localScale.y;
                                                            hit.collider.gameObject.transform.localScale = new Vector3(size_x, size_y, hit.collider.gameObject.transform.localScale.z);
                                                            hit.collider.gameObject.GetComponent<TweenScale>().ResetToBeginning();
                                                            hit.collider.gameObject.GetComponent<TweenScale>().from = new Vector3(size_x, size_y, hit.collider.gameObject.transform.localScale.z);
                                                            hit.collider.gameObject.GetComponent<TweenScale>().to = new Vector3((size_x * 1.5f), (size_y * 1.5f), hit.collider.gameObject.transform.localScale.z);
                                                            hit.collider.gameObject.GetComponent<TweenScale>().duration = 0.2f;
                                                            hit.collider.gameObject.GetComponent<TweenScale>().PlayForward();
                                                            hit.collider.gameObject.GetComponent<TweenScale>().ResetToBeginning();
                                                            hit.collider.gameObject.transform.localScale = new Vector3(size_x, size_y, hit.collider.gameObject.transform.localScale.z);
                                                        }
                                                        else
                                                        {
                                                            GameObject _mapUiWindow = GameObject.Find("MapUiWindow");
                                                            _mapUiWindow.GetComponent<MapUiWindow>().ChoiceWindow(5);
                                                            _mapUiWindow.transform.Find("All").transform.Find("PatrolFight").GetComponent<PatrolFight>().SetArrow(0);
                                                            _mapUiWindow.transform.Find("All").transform.Find("PatrolFight").GetComponent<PatrolFight>().SeInfo(TextTranslator.instance.GetResourceByID(Number).MapId);
                                                            hit.collider.gameObject.GetComponent<TweenScale>().ResetToBeginning();
                                                            hit.collider.gameObject.GetComponent<TweenScale>().to = new Vector3((hit.collider.gameObject.transform.localScale.x * 1.5f), (hit.collider.gameObject.transform.localScale.y * 1.5f), hit.collider.gameObject.transform.localScale.z);
                                                            hit.collider.gameObject.GetComponent<TweenScale>().duration = 0.2f;
                                                            hit.collider.gameObject.GetComponent<TweenScale>().PlayForward();
                                                            hit.collider.gameObject.GetComponent<TweenScale>().ResetToBeginning();
                                                        }
                                                        break;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                GameObject _MapUiWindow = GameObject.Find("MapUiWindow");
                                                if (CharacterRecorder.instance.GuideID[19] != 2 && CharacterRecorder.instance.GuideID[19] != 3 && CharacterRecorder.instance.GuideID[17] != 1
                                                  && CharacterRecorder.instance.GuideID[17] != 2 && CharacterRecorder.instance.GuideID[18] != 3 && CharacterRecorder.instance.GuideID[18] != 2 && CharacterRecorder.instance.GuideID[18] != 4 && (CharacterRecorder.instance.GuideID[27] != 1))
                                                {
                                                    _MapUiWindow.GetComponent<MapUiWindow>().ChoiceWindow(7);
                                                }

                                            }
                                            for (int i = 0; i < CouldList.Count; i++)
                                            {
                                                //  Debug.LogError("zxcccccccccc"+hit.collider.gameObject.name +"   "+CouldList[i].name);
                                                if (hit.collider.gameObject.name == CouldList[i].name)
                                                {
                                                    GameObject go = GameObject.Find("MapUiWindow");

                                                    if (CharacterRecorder.instance.level < ListLevel[CharacterRecorder.instance.mapID - 1])
                                                    {
                                                        UIManager.instance.OpenPromptWindow("等级达到" + ListLevel[i] + "解锁", PromptWindow.PromptType.Hint, null, null);
                                                    }
                                                    else
                                                    {
                                                        if (CharacterRecorder.instance.lastGateID > TextTranslator.instance.GetChapterMaxGate(i + 1))
                                                        {
                                                            if (!IsClickLeiDaLock)  //防止连点
                                                            {
                                                                NetworkHandler.instance.SendProcess("1016#" + (i + 2) + ";");
                                                                IsClickLeiDaLock = true;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            UIManager.instance.OpenPromptWindow("通关第" + (ListGate[i] - 10000).ToString() + "关解锁", PromptWindow.PromptType.Hint, null, null);
                                                        }
                                                    }

                                                }
                                            }
                                            for (int i = 0; i < PassBoxList.Count; i++)
                                            {
                                                if (hit.collider.gameObject.name == PassBoxList[i].name)
                                                {
                                                    if (!SceneTransformer.instance.CheckGuideIsFinish())
                                                    {
                                                        if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 5 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) >= 2)
                                                        {
                                                            TheBoxOnClick(TextTranslator.instance.GetGateCompleteBoxGroup(i + 1), i);
                                                            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_903);
                                                            StartCoroutine(ChangeCamera(new Vector2(ListMapNode[1].Base.transform.position.x, ListMapNode[1].Base.transform.position.z), 10002, false));
                                                            PlayerPrefs.SetInt(LuaDeliver.instance.GetGuideStateName(), PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) + 1);
                                                            LuaDeliver.instance.UseGuideStation();
                                                        }
                                                    }
                                                    else
                                                    {
                                                        TheBoxOnClick(TextTranslator.instance.GetGateCompleteBoxGroup(i + 1), i);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else //点击非ui yy
                                {
                                    IsMove = false;
                                }
                            }
                        }
                        else
                        {
                            if (UICamera.hoveredObject != null && UICamera.hoveredObject.name == "BgCollider" && UICamera.hoveredObject.name == DownName)
                            {
                                UICamera.hoveredObject.transform.parent.gameObject.SetActive(false);
                                GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().ChoiceWindow(7);
                                GameObject _mask = GameObject.Find("Mask");
                                if (_mask != null)
                                {
                                    _mask.SetActive(false);
                                }
                            }
                            else if (UICamera.hoveredObject != null && UICamera.hoveredObject.name == "BgCollider1")
                            {
                                UICamera.hoveredObject.transform.parent.gameObject.SetActive(false);
                            }

                        }
                    }
                }


                //移动角色位置
                if (IsMoveHeroPosition)
                {
                    if (PositionPositioninfo.Count != 0)
                    {
                        if (IsMoveHero)
                        {
                            MoveHeroNumberPosition(HeroObj.transform.localPosition, PositionPositioninfo[0].transform.localPosition, 30);
                            //MoveHeroNumberPosition(HeroObj.transform.localPosition, PositionPositioninfo[0].transform.localPosition, 1);
                            IsMoveHero = false;
                        }
                        //  HeroObj.transform.localPosition = Vector3.SmoothDamp(HeroObj.transform.localPosition, MapList[MoveListNumber[0]].transform.localPosition, ref velocity, 0.2f);
                        IsMoveCenert = true;
                        if (MovePointList.Count != 0)
                        {
                            //   Debug.Log("距离" + Vector2.Distance(HeroObj.transform.position, MovePointList[0].transform.position));
                            if (Vector2.Distance(HeroObj.transform.position, MovePointList[0].transform.position) <= 0.1f)
                            {
                                DestroyImmediate(MovePointList[0]);
                                MovePointList.RemoveAt(0);
                            }
                        }


                        if (HeroObj.transform.localPosition == new Vector3(PositionPositioninfo[0].transform.localPosition.x, PositionPositioninfo[0].transform.localPosition.y, -0.32f))
                        {
                            CurHeroPosition = int.Parse(PositionPositioninfo[0].name);
                            CharacterRecorder.instance.standGateID = CurHeroPosition - 1;
                            PositionPositioninfo.RemoveAt(0);
                            IsMoveCenert = false;
                            IsMoveHero = true;
                        }
                    }
                    else
                    {
                        IsMove = false;
                        StartCoroutine(ChangeCamera(CameraPosition, GoGateID, IsOpen));
                        IsMoveHeroPosition = false;
                        //IsMove = false;
                        SoldierType = 1;
                    }
                }
                MoveHeroPosition();


                if (IsMove) //拖动后滑动
                {
                    if (GameObject.Find("MapUiWindow") != null)
                    {
                        if (GameObject.Find("GrabTerritory") == null && GameObject.Find("PatrolLong") == null && GameObject.Find("PatrolItem") == null && GameObject.Find("GateInfoWindow") == null && GameObject.Find("PatrolFight") == null && GameObject.Find("WordWindow") == null)
                        {
                            if (UICamera.hoveredObject.name == "UIRoot")
                            {
                                EndMoveCamreVector();
                            }
                        }
                    }

                }
            }
        }


        SoldierTimer += Time.deltaTime;
        if (SoldierTimer > 0.15f)
        {

            SoldierIndex++;
            if (SoldierType == 1)
            {
                SoldierTimer -= 0.15f;
                if (SoldierIndex > 3)
                {
                    SoldierIndex = 0;
                }
                Soldier.renderer.material.mainTexture = (Texture2D)Resources.Load("MapTexture/Soldier/idle" + SoldierDirection.ToString() + "_" + SoldierIndex.ToString());
            }
            else
            {
                SoldierTimer -= 0.08f;
                if (SoldierIndex > 7)
                {
                    SoldierIndex = 0;
                }
                Soldier.renderer.material.mainTexture = (Texture2D)Resources.Load("MapTexture/Soldier/run" + SoldierDirection.ToString() + "_" + SoldierIndex.ToString());
            }

            Soldier.transform.localScale = new Vector3(0.6f * SoldierFace, 0.45f, 1);
        }
    }

    public void NewGuideHandle1() //kino
    {
        Debug.LogError("BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBb");
        StopAllCoroutines();
        StartCoroutine(ChangeCamera(new Vector2(ListMapNode[1].Base.transform.position.x, ListMapNode[1].Base.transform.position.z), 10002, false));
    }

    void TheBoxOnClick(int _Group, int _index)
    {
        //if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 14 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 2)
        //{
        //    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1312);
        //    PlayerPrefs.SetInt(LuaDeliver.instance.GetGuideSubStateName(), 3);
        //    LuaDeliver.instance.UseGuideStation();
        //}
        //装备升级
        if (PassStatueList[_index] == 1)
        {
            if (MapUiWindowObj == null)
            {
                MapUiWindowObj = GameObject.Find("MapUiWindow");
            }
            MapUiWindowObj.GetComponent<MapUiWindow>().GetRewardGroupId = _Group;
            CancelBox_Efect(_Group);
            NetworkHandler.instance.SendProcess("2015#" + _Group + ";");
            NetworkHandler.instance.SendProcess("2016#" + _Group + ";");
        }
        else
        {
            UIManager.instance.OpenSinglePanel("TheChestWindow", false);
            TheChestWindow tcw = GameObject.Find("TheChestWindow").GetComponent<TheChestWindow>();
            tcw.InitGetPassReward(_Group);
        }
    }

    void CancelBox_Efect(int _Group)
    {
        PassBoxList[TextTranslator.instance.GetGateCompleteBox(_Group).GateCompleteBoxID - 1].gameObject.SetActive(false);
    }


    /// <summary>
    /// 松开鼠标计算摄像机需要移动的位置
    /// </summary>
    private void EndMoveCamreVector()
    {
        if (BeforeLastMousePositionList.Count > 2)
        {
            float x = (EndMousePositionX - BeforeLastMousePositionList[BeforeLastMousePositionList.Count - 2].x) * 10 * MapCamera.orthographicSize / 0.5f;
            float y = (EndMousePositionY - BeforeLastMousePositionList[BeforeLastMousePositionList.Count - 2].y) * 10 * MapCamera.orthographicSize / 0.5f;
            //Debug.LogError(BeforeLastMousePositionList[0]);
            //Debug.LogError(new Vector3(EndMousePositionX, EndMousePositionY,0));
            float Mapx = (1f / Screen.height);
            Vector3 MousePosition = new Vector3(-x * Mapx, 0.8f, -y * Mapx);

            if (CharacterRecorder.instance.level > 5)
            {
                EndMoveCamrePosition(MousePosition);
            }
        }
    }

    public void EndMoveCamrePosition(Vector3 SetMapPosition)
    {
        float Number = (float)Screen.width / (float)Screen.height;
        float MaxX = 0;
        float Minx = 0;
        float MaxY = 0;
        float Miny = 0;
        if (Number > 1.7f)
        {
            MaxX = 1.3f * 0.48f / MapCamera.orthographicSize;
            Minx = -1.3f * 0.48f / MapCamera.orthographicSize;
            MaxY = 0.95f * 0.48f / MapCamera.orthographicSize;
            Miny = -0.95f * 0.48f / MapCamera.orthographicSize;
        }
        else if (Number <= 1.7f && Number > 1.6f)
        {
            MaxX = 1.22f * 0.48f / MapCamera.orthographicSize;
            Minx = -1.22f * 0.48f / MapCamera.orthographicSize;
            MaxY = 0.8f * 0.48f / MapCamera.orthographicSize;
            Miny = -0.8f * 0.48f / MapCamera.orthographicSize;
        }
        else if (Number >= 1.5f && Number <= 1.6f)
        {
            MaxX = 1.27f * 0.48f / MapCamera.orthographicSize;
            Minx = -1.27f * 0.48f / MapCamera.orthographicSize;
            MaxY = 0.8f * 0.48f / MapCamera.orthographicSize;
            Miny = -0.8f * 0.48f / MapCamera.orthographicSize;
        }
        else
        {
            MaxX = 1.37f * 0.48f / MapCamera.orthographicSize;
            Minx = -1.38f * 0.48f / MapCamera.orthographicSize;
            MaxY = 0.8f * 0.48f / MapCamera.orthographicSize;
            Miny = -0.8f * 0.48f / MapCamera.orthographicSize;
        }

        //Debug.Log("ssss" + Screen.height + Screen.width);
        //if (this.MapPosition != SetMapPosition)
        {

            float x = 0;
            float y = 0;
            if (StartMapPosition.x + SetMapPosition.x > MaxX)
            {
                x = MaxX;
            }
            else if (StartMapPosition.x + SetMapPosition.x < Minx)
            {
                x = Minx;
            }
            else
            {
                x = StartMapPosition.x + SetMapPosition.x;
            }


            if (StartMapPosition.z + SetMapPosition.z > MaxY)
            {
                y = MaxY;
            }
            else if (StartMapPosition.z + SetMapPosition.z < Miny)
            {
                y = Miny;
            }
            else
            {
                y = StartMapPosition.z + SetMapPosition.z;
            }

            float step = Speed * Time.deltaTime;
            //StartMapPosition = new Vector3(x, 0.8f, y);
            MapCamera.transform.localPosition = new Vector3(Mathf.Lerp(MapCamera.transform.localPosition.x, x, step), Mathf.Lerp(MapCamera.transform.localPosition.y, 0.8f, step), Mathf.Lerp(MapCamera.transform.localPosition.z, y, step));
            this.MapPosition = SetMapPosition;
            //StartMapPosition = MapCamera.transform.localPosition;
        }
    }

    /// <summary>
    /// 保存松开鼠标最后5个像素点
    /// </summary>
    private void AddBeforeLastMousePositionList()
    {
        if (BeforeLastMousePositionList.Count < 5)
        {
            BeforeLastMousePositionList.Add(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        }
        else
        {
            BeforeLastMousePositionList.RemoveAt(0);
            BeforeLastMousePositionList.Add(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        }
    }


    /// <summary>
    /// 计算摄像机需要移动的位置
    /// </summary>
    public void MoveCamreVector()
    {
        float x = (Input.mousePosition.x - StartMousePositionX) * MapCamera.orthographicSize / 0.5f;
        float y = (Input.mousePosition.y - StartMousePositionY) * MapCamera.orthographicSize / 0.5f;
        //StartMousePositionX = Input.mousePosition.x;
        //StartMousePositionY = Input.mousePosition.y;

        float Mapx = (1f / Screen.height);
        Vector3 MousePosition = new Vector3(-x * Mapx, 0.8f, -y * Mapx);

        if (CharacterRecorder.instance.level > 5)
        {
            MoveCamrePosition(MousePosition);
        }
    }
    //做摄像机的移动
    public void MoveCamrePosition(Vector3 SetMapPosition)
    {
        float Number = (float)Screen.width / (float)Screen.height;
        float MaxX = 0;
        float Minx = 0;
        float MaxY = 0;
        float Miny = 0;
        if (Number > 1.7f)
        {
            MaxX = 1.3f * CameraorthographicSize / MapCamera.orthographicSize;
            Minx = -1.3f * CameraorthographicSize / MapCamera.orthographicSize;
            MaxY = 0.95f * CameraorthographicSize / MapCamera.orthographicSize;
            Miny = -0.95f * CameraorthographicSize / MapCamera.orthographicSize;
        }
        else if (Number <= 1.7f && Number > 1.6f)
        {
            MaxX = 1.22f * CameraorthographicSize / MapCamera.orthographicSize;
            Minx = -1.22f * CameraorthographicSize / MapCamera.orthographicSize;
            MaxY = 0.8f * CameraorthographicSize / MapCamera.orthographicSize;
            Miny = -0.8f * CameraorthographicSize / MapCamera.orthographicSize;
        }
        else if (Number >= 1.5f && Number <= 1.6f)
        {
            MaxX = 1.27f * CameraorthographicSize / MapCamera.orthographicSize;
            Minx = -1.27f * CameraorthographicSize / MapCamera.orthographicSize;
            MaxY = 0.8f * CameraorthographicSize / MapCamera.orthographicSize;
            Miny = -0.8f * CameraorthographicSize / MapCamera.orthographicSize;
        }
        else
        {
            MaxX = 1.37f * CameraorthographicSize / MapCamera.orthographicSize;
            Minx = -1.38f * CameraorthographicSize / MapCamera.orthographicSize;
            MaxY = 0.8f * CameraorthographicSize / MapCamera.orthographicSize;
            Miny = -0.8f * CameraorthographicSize / MapCamera.orthographicSize;
        }

        //Debug.Log("ssss" + Screen.height + Screen.width);
        if (this.MapPosition != SetMapPosition)
        {

            float x = 0;
            float y = 0;
            if (StartMapPosition.x + SetMapPosition.x > MaxX)
            {
                x = MaxX;
            }
            else if (StartMapPosition.x + SetMapPosition.x < Minx)
            {
                x = Minx;
            }
            else
            {
                x = StartMapPosition.x + SetMapPosition.x;
            }


            if (StartMapPosition.z + SetMapPosition.z > MaxY)
            {
                y = MaxY;
            }
            else if (StartMapPosition.z + SetMapPosition.z < Miny)
            {
                y = Miny;
            }
            else
            {
                y = StartMapPosition.z + SetMapPosition.z;
            }


            //StartMapPosition = new Vector3(x, 0.8f, y);
            MapCamera.transform.localPosition = new Vector3(x, 0.8f, y);
            this.MapPosition = SetMapPosition;
        }
    }
    /// <summary>
    /// 记录要移动的个数
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="MaplistNumber"></param>
    private void ButtonOnlick(GameObject obj, int MaplistNumber)
    {
        //Debug.LogError("asdasd" + CurButtonNumber + "sda" + MaplistNumber + "s    " + CurHeroPosition);

        //if (CurButtonNumber != MaplistNumber)
        {
            //ListMapNode[MaplistNumber].Dun.SetActive(true);
            if (CurButtonNumber == MaplistNumber && SoldierType != 1)
            {
                return;
            }
            if (CharacterRecorder.instance.GuideID[27] == 0 && CharacterRecorder.instance.lastGateID == 10011)
            {
                CharacterRecorder.instance.GuideID[27] = 1;
                StartCoroutine(SceneTransformer.instance.NewbieGuide());
            }
            //CharacterRecorder.instance.IsOpenCreamCN = false;
            CurButtonNumber = MaplistNumber;
            PositionPositioninfo.Clear();
            for (int i = 0; i < MovePointList.Count; i++)
            {
                DestroyImmediate(MovePointList[i]);

            }
            IsMoveHero = true;
            MovePointList.Clear();
            bool IsCurheroPositionJuDian = false;

            int Number = 0;
            if (CurHeroPosition >= 200)
            {
                CurHeroPosition = int.Parse(PointObj.transform.GetComponent<MapPoint>().Pointinfo.name);
                IsCurheroPositionJuDian = true;
            }
            if (MaplistNumber >= 200)
            {
                Number = int.Parse(obj.transform.GetComponent<MapPoint>().Pointinfo.name);
                PointObj = obj;
            }
            else
            {
                Number = MaplistNumber;
            }

            if (CurHeroPosition < Number)
            {
                if (IsCurheroPositionJuDian)
                {
                    CurHeroPosition -= 1;
                }
                if (IsMoveCenert)
                {
                    if (CurHeroPosition > 1)
                    {
                        CurHeroPosition -= 1;
                    }
                }


                for (int i = CurHeroPosition; i < Number; i++)
                {
                    PositionPositioninfo.Add(MapList[i]);
                }
                if (MaplistNumber >= 200)
                {
                    PositionPositioninfo.Add(PointObj);
                }
            }
            else
            {
                //if (IsMoveCenert)
                //{
                //    CurHeroPosition  += 1;
                //}
                for (int i = CurHeroPosition - 1; i >= Number - 1; i--)
                {
                    PositionPositioninfo.Add(MapList[i]);
                }
                if (MaplistNumber >= 200)
                {
                    PositionPositioninfo.Add(PointObj);
                }
            }


            IsMoveHeroPosition = true;
        }
        //  Debug.Log(Vector2.Distance(new Vector2(-1.3f, HeroObj.transform.position.y), new Vector2(MapList[MoveListNumber[0]].transform.position.x, MapList[MoveListNumber[0]].transform.position.y)));
        // ShowDian();

    }

    /// <summary>
    /// 计算点的位置和生成点
    /// </summary>
    private void ShowDian()
    {
        for (int i = 0; i < PositionPositioninfo.Count; i++)
        {
            if (i == 0)
            {
                //Debug.Log("  MapList " + MapList[MoveListNumber[i]].transform.position + "   HeroObj" + HeroObj.transform.position + "   y" + HeroObj.transform.position.y);

                float nns = Vector2.Distance(new Vector2(HeroObj.transform.position.x, HeroObj.transform.position.y), new Vector2(PositionPositioninfo[i].transform.position.x, PositionPositioninfo[i].transform.position.y)) / 0.05f;

                float x = (float)((PositionPositioninfo[i].transform.position.x - HeroObj.transform.position.x) / nns);
                float y = (float)((PositionPositioninfo[i].transform.position.z - HeroObj.transform.position.z) / nns);
                //Debug.Log("x  " + (MapList[MoveListNumber[i]].transform.position.x - HeroObj.transform.position.x));
                //Debug.Log("y  " + (MapList[MoveListNumber[i]].transform.position.y - HeroObj.transform.position.y));
                int xx = (int)nns;
                //Debug.Log("距离" + nns + "   x" + x + " y " + y + "  MapList " + MapList[MoveListNumber[i]].transform.position + "   HeroObj" + HeroObj.transform.position);
                // Debug.Log(Vector2.Distance(new Vector2(HeroObj.transform.position.x, 0.1f), new Vector2(-1.0f, 0.1f)));
                for (int j = 0; j < xx; j++)
                {
                    GameObject on = Instantiate(Dian) as GameObject;

                    on.transform.localScale = Dian.transform.localScale;
                    //Debug.Log("asdasd" + ((y * (j + 1)) + HeroObj.transform.position.y) + "   y" + HeroObj.transform.position.z + "      " + (y * (j + 1)));
                    on.transform.parent = Dian.transform.parent;
                    on.transform.localRotation = new Quaternion(0, 0, 0, 0);
                    float xs = (x * (j + 1)) + HeroObj.transform.position.x;
                    float ys = (y * (j + 1)) + HeroObj.transform.position.z;
                    //   Debug.Log("ys " + ys);
                    on.transform.position = new Vector3(xs, 0.1f, ys);
                    on.SetActive(true);
                    MovePointList.Add(on);
                }
                //Vector2 nn = new Vector2(x, y);
                // Debug.Log("asdasd nn" +nn+"   "+ i + "   " + HeroObj.transform.position.x + "  " + Vector2.Distance(new Vector2(HeroObj.transform.position.x, HeroObj.transform.position.y), new Vector2(MapList[MoveListNumber[i]].transform.position.x, MapList[MoveListNumber[i]].transform.position.y)) / 0.1);
            }
            else
            {
                double nns = Vector2.Distance(new Vector2(PositionPositioninfo[i - 1].transform.position.x, PositionPositioninfo[i - 1].transform.position.y), new Vector2(PositionPositioninfo[i].transform.position.x, PositionPositioninfo[i].transform.position.y)) / 0.05f;
                float x = (float)((PositionPositioninfo[i].transform.position.x - PositionPositioninfo[i - 1].transform.position.x) / nns);
                float y = (float)((PositionPositioninfo[i].transform.position.z - PositionPositioninfo[i - 1].transform.position.z) / nns);
                int xx = (int)nns;
                for (int j = 0; j < xx; j++)
                {
                    GameObject on = Instantiate(Dian) as GameObject;

                    on.transform.localScale = Dian.transform.localScale;
                    //Debug.Log("asdasd" + ((y * (j + 1)) + HeroObj.transform.position.y) + "   y" + HeroObj.transform.position.z + "      " + (y * (j + 1)));
                    on.transform.parent = Dian.transform.parent;
                    on.transform.localRotation = new Quaternion(0, 0, 0, 0);
                    float xs = (x * (j + 1)) + PositionPositioninfo[i - 1].transform.position.x;
                    float ys = (y * (j + 1)) + PositionPositioninfo[i - 1].transform.position.z;
                    //   Debug.Log("ys " + ys);
                    on.transform.position = new Vector3(xs, 0.1f, ys);
                    on.SetActive(true);
                    MovePointList.Add(on);
                }
                // Vector2 nn = new Vector2(x, y);
                //   Debug.Log("asdasd nn"+nn+"  " + i + "  " + Vector2.Distance(new Vector2(MapList[MoveListNumber[i - 1]].transform.position.x, MapList[MoveListNumber[i-1]].transform.position.y), new Vector2(MapList[MoveListNumber[i]].transform.position.x, MapList[MoveListNumber[i]].transform.position.y))/0.1);
            }

        }
    }
    /// <summary>
    /// 移动英雄位置
    /// </summary>
    private void MoveHeroPosition()
    {

        if (!IsMoveHero)
        {
            if (PositionPositioninfo.Count > JumpPointNum || CharacterRecorder.instance.IsJumpMove)
            {
                StopAllCoroutines();
                CharacterRecorder.instance.IsJumpMove = false;
                SoldierType = 1;
                MoveHeroNumber = 0;
                CurHeroPosition = int.Parse(PositionPositioninfo[PositionPositioninfo.Count - 1].name);
                CharacterRecorder.instance.standGateID = CurHeroPosition - 1;
                HeroObj.transform.localPosition = new Vector3(PositionPositioninfo[PositionPositioninfo.Count - 1].transform.localPosition.x, PositionPositioninfo[PositionPositioninfo.Count - 1].transform.localPosition.y, -0.32f);
                GameObject _temp = GameObject.Find("motuo_ShanXian");
                if (_temp != null)
                {
                    DestroyImmediate(_temp);
                }
                GameObject go = GameObject.Instantiate(Resources.Load("Prefab/Effect/motuo_ShanXian", typeof(GameObject)), HeroObj.transform.GetChild(0).position, Quaternion.identity) as GameObject;
                go.name = "motuo_ShanXian";
                PositionPositioninfo.Clear();
                MovePointList.Clear();
                IsMoveHero = true;
                IsMoveHeroPosition = false;
                int _groupID = CurHeroPosition + 10000;
                StartCoroutine(ChangeCamera(new Vector2(ListMapNode[CurHeroPosition - 1].Base.transform.position.x, ListMapNode[CurHeroPosition - 1].Base.transform.position.z), _groupID, true));
                return;
            }
            MoveHeroNumber--;
            if (MoveHeroNumber > 0)
            {
                if (EachMoveHeroPosition.x > 0)
                {
                    SoldierFace = 1;
                }
                else
                {
                    SoldierFace = -1;
                }



                if (EachMoveHeroPosition.x > 0 && EachMoveHeroPosition.y > 0)
                {
                    SoldierDirection = 2;
                }
                else if (EachMoveHeroPosition.x > 0 && EachMoveHeroPosition.y == 0)
                {
                    SoldierDirection = 3;
                }
                else if (EachMoveHeroPosition.x > 0 && EachMoveHeroPosition.y < 0)
                {
                    SoldierDirection = 4;
                }
                else if (EachMoveHeroPosition.x < 0 && EachMoveHeroPosition.y > 0)
                {
                    SoldierDirection = 2;
                }
                else if (EachMoveHeroPosition.x < 0 && EachMoveHeroPosition.y == 0)
                {
                    SoldierDirection = 3;
                }
                else if (EachMoveHeroPosition.x < 0 && EachMoveHeroPosition.y < 0)
                {
                    SoldierDirection = 4;
                }


                //else
                //{
                HeroObj.transform.localPosition = new Vector3(HeroObj.transform.localPosition.x + EachMoveHeroPosition.x, HeroObj.transform.localPosition.y + EachMoveHeroPosition.y, -0.32f);
                SoldierType = 2;
                //}
            }
            else
            {
                HeroObj.transform.localPosition = new Vector3(PositionPositioninfo[0].transform.localPosition.x, PositionPositioninfo[0].transform.localPosition.y, -0.32f);
            }
        }
    }

    /// <summary>
    /// 计算每次移动的距离
    /// </summary>
    /// <param name="StartPosition"></param>
    /// <param name="EndPosition"></param>
    /// <param name="Number"></param>
    private void MoveHeroNumberPosition(Vector3 StartPosition, Vector3 EndPosition, int Number)
    {
        MoveHeroNumber = Number;
        float x = (EndPosition.x - StartPosition.x) / MoveHeroNumber;
        float y = (EndPosition.y - StartPosition.y) / MoveHeroNumber;
        EachMoveHeroPosition = new Vector3(x, y, EndPosition.z);
    }
    public void UpdateGetres(List<Mapgetreslist> mapget)
    {
        for (int i = 0; i < mapget.Count; i++)
        {
            Debug.Log("值" + mapget[i].GetresId + "   " + mapget[i].HeroId + "   " + mapget[i].Timer);
        }
    }
    /// <summary>
    /// 切换显示的类型
    /// </summary>
    public void ShowPatrol()
    {
        for (int i = 0; i < getresListItem.Count; i++)
        {
            getresListItem[i].transform.Find("Don").renderer.material.mainTexture = icon1;
            getresListItem[i].renderer.material.mainTexture = icon6;
            getresListItem[i].transform.localScale = new Vector3(0.03442635f, 0.05537f, 0.333333f);
        }


        for (int i = 0; i < getreslist.Count; i++)
        {
            int _index = 0;
            for (int j = 0; j < ResourceMapID.Count; j++)
            {
                if (getreslist[i].GetresId == ResourceMapID[j])
                {
                    _index = j;
                    break;
                }
            }
            //  Debug.LogError("         " + getreslist[i].Timer + "     " + getreslist[i].HeroId);
            if (getreslist[i].Timer == 0 && getreslist[i].HeroId > 0)
            {
                getresListItem[_index].transform.Find("Don").renderer.material.mainTexture = icon3;
                getresListItem[_index].renderer.material.mainTexture = icon5;
                getresListItem[_index].transform.Find("Don1").gameObject.SetActive(false);
                getresListItem[_index].transform.localScale = new Vector3(0.044667f, 0.05907f, 0.333333f);
            }
            else
                if (getreslist[i].Timer > 0 && getreslist[i].HeroId > 0)
                {
                    GameObject _mapUiWindow = GameObject.Find("MapUiWindow");
                    if (_mapUiWindow != null)
                    {
                        _mapUiWindow.GetComponent<MapUiWindow>().SetinstarTimerObj(getresListItem[_index], getreslist[i].Timer);
                        getresListItem[_index].transform.Find("Don").renderer.material.mainTexture = icon2;
                        getresListItem[_index].renderer.material.mainTexture = icon5;
                        getresListItem[_index].transform.Find("Don1").gameObject.SetActive(false);
                        getresListItem[_index].transform.localScale = new Vector3(0.044667f, 0.05907f, 0.333333f);
                    }
                }
                else
                    if (getreslist[i].Timer == 0 && getreslist[i].HeroId == 0)
                    {
                        getresListItem[_index].transform.Find("Don1").gameObject.SetActive(false);
                        getresListItem[_index].transform.localScale = new Vector3(0.03442635f, 0.05537f, 0.333333f);
                    }
        }
        GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().IsClearResourceTimes = true;

    }
    //更新云显示多少
    public void ShowCloud()
    {
        //  Debug.LogError("asdasdsdasdasdasdasdasd" + CharacterRecorder.instance.mapID);
        for (int i = 1; i < CharacterRecorder.instance.mapID; i++)
        {
            CloudList[i - 1].SetActive(false);
        }
    }
    public void SetCloud(int number)
    {
        GameObject go = GameObject.Instantiate(Resources.Load("Prefab/Effect/YunSan", typeof(GameObject)), CloudList[number - 2].transform.position, Quaternion.identity) as GameObject;
        StartCoroutine(CloudAlpha(number));
    }

    IEnumerator CloudAlpha(int number)
    {
        CloudList[number - 2].transform.GetChild(0).gameObject.SetActive(false);
        for (int i = 50; i >= 0; i--)
        {
            CloudList[number - 2].renderer.material.SetColor("_TintColor", new Color(0.5f, 0.5f, 0.5f, i / 100f));
            yield return new WaitForSeconds(0.01f);
        }
        CloudList[number - 2].SetActive(false);
        yield return 0;
    }

    public void SetMapName()
    {
        //StartCoroutine("UpdateMapName");
        Invoke("UpdateMapName", 0);
    }
    void UpdateMapName()
    {
        GameObject go = GameObject.Find("MapUiWindow");
        if (go != null)
        {
            MapUiWindow mw = go.GetComponent<MapUiWindow>();
            //for (int i = 0; i < TheChestStatueList.Count; i++)
            //{
            //    if (TheChestStatueList[i] != 2)
            //    {
            //        if (i + 1 > CharacterRecorder.instance.lastGateID - 10001)
            //        {
            //            break;
            //        }
            //        List<TextTranslator.Gate> myGate = TextTranslator.instance.GetGroupGate(i + 1, 1);
            //        int _StarCount = CountGroupStar(myGate);
            //        if (_StarCount != 0)
            //        {
            //            go.GetComponent<MapUiWindow>().SetTheChestPrompt(MapItemList[i].transform.gameObject, _StarCount + "/" + myGate.Count * 3, (i + 1).ToString(), (_StarCount / (myGate.Count * 3)).ToString());
            //        }
            //    }
            //}
            //int LastGroup = 0;
            for (int i = 0; i < PassBoxList.Count; i++)
            {
                //if (LastGroup != myCreamGate[i].group)
                //{
                //    LastGroup = myCreamGate[i].group;
                //}
                //else
                //{
                //    continue;
                //}
                //if (PassStatueList[myCreamGate[i].group - 1] == 2)
                if (PassStatueList[i] == 2)
                {
                    PassBoxList[i].SetActive(false);
                }
                //else if (PassStatueList[myCreamGate[i].group - 1] == 1)
                else if (PassStatueList[i] == 1)
                {
                    IsPlayBox_efect = true;
                    PassBoxList[i].renderer.material.mainTexture = Resources.Load("Game/blank") as Texture;
                    //StartCoroutine("PassBox_Efect", PassBoxList[i]);
                    PassBoxList[i].transform.Find("WF_HeZi_zuan").gameObject.SetActive(true); ;
                }
            }
            //for (int j = 0; j < CreamStatueList.Count; j++)

            for (int j = 0; j < CreamStatueList.Count; j++)
            {

                //if (TheChestStatueList[TextTranslator.instance.GetGateCompleteBoxGroup(j + 1) - 1] == 2 && CreamStatueList[j] != 2 && CharacterRecorder.instance.lastGateID > 10010)
                if (CreamStatueList[j] != 2 && CharacterRecorder.instance.lastGateID > 10010)
                {
                    //List<TextTranslator.Gate> myGate1 = new List<TextTranslator.Gate>();
                    int group = TextTranslator.instance.GetGateCompleteBoxGroup(j + 1);
                    List<TextTranslator.Gate> myGate1 = TextTranslator.instance.GetGroupGate(group, 2);
                    if (myGate1[0].id <= CharacterRecorder.instance.lastCreamGateID)
                    {
                        List<TextTranslator.Gate> _nomalGate = TextTranslator.instance.GetGroupGate(group, 1);
                        int _nomalGateStar = CountGroupStar(_nomalGate);
                        int _StarCount = CountGroupStar(myGate1);
                        //if (_StarCount != 0)
                        if (_nomalGateStar == (_nomalGate.Count * 3))
                        {
                            go.GetComponent<MapUiWindow>().SetTheChestPrompt(MapItemList[group - 1].transform.gameObject, "JY" + _StarCount + "/" + myGate1.Count * 3,
                                group.ToString(), (_StarCount / (myGate1.Count * 3)).ToString());
                        }
                    }
                }
            }
            for (int i = 0; i < CloudList.Count; i++)
            {
                if (CloudList[i].activeSelf)
                {
                    //Debug.Log(CharacterRecorder.instance.lastGateID + "  " + ListGate[i]);
                    //if (CharacterRecorder.instance.lastGateID > ListGate[i])

                    if (CharacterRecorder.instance.level < ListLevel[i])
                    {
                        go.GetComponent<MapUiWindow>().SetInstarMapName(CloudList[i].transform.GetChild(0).gameObject, "等级达到" + ListLevel[i] + "解锁", 0, "Level" + ListLevel[i].ToString());
                    }
                    else
                    {
                        if (CharacterRecorder.instance.lastGateID <= ListGate[i])
                        {
                            go.GetComponent<MapUiWindow>().SetInstarMapName(CloudList[i].transform.GetChild(0).gameObject, "通关第" + (ListGate[i] - 10000).ToString() + "关解锁", 0, "Gate" + ListGate[i].ToString());
                        }
                    }


                    //yield return new WaitForSeconds(0.01f);
                }
            }
        }

    }

    public int CountGroupStar(List<TextTranslator.Gate> myGate)
    {
        int allStarNum = 0;

        for (int i = 0; i < myGate.Count; i++)
        {
            if (myGate[i].id % 10000 - 1 < MapSatrList.Count)
            {
                if (myGate[i].id > 20000)
                {
                    for (int j = 0; j < myCreamGate.Count; j++)
                    {
                        if (myCreamGate[j].id == myGate[i].id)
                        {
                            if (j < CreamSatrList.Count)
                            {
                                allStarNum += CreamSatrList[j];
                            }
                            break;
                        }
                    }
                }
                else
                {
                    allStarNum += MapSatrList[myGate[i].id - 10001];
                }
            }
        }
        return allStarNum;
    }
    /// <summary>
    /// 指定位置显示8位地图
    /// </summary>
    public void SetMapIdShowName(int id)
    {
        GameObject _MapUiWindow = GameObject.Find("MapUiWindow");
        if (_MapUiWindow != null)
        {
            for (int i = SetShowMapNameNumber(id - 1); i < SetShowMapNameNumber(id); i++)
            {
                _MapUiWindow.GetComponent<MapUiWindow>().SetInstarMapName(MapItemList[i], TextTranslator.instance.GetGateByID(ListMapNode[i].GateID + 10000).name, MapSatrList[i]);
            }
        }
    }
    /// <summary>
    /// 计算地图上显示的名字数量
    /// </summary>
    /// <param name="CurNumber"></param>
    /// <returns></returns>
    private int SetShowMapNameNumber(int CurNumber)
    {
        int CurMapNumber = 0;
        for (int i = 0; i < CurNumber; i++)
        {
            CurMapNumber += MapNumber[i];
        }
        return CurMapNumber;

    }
    /// <summary>
    /// 计算已经打过的关卡的数。
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    private int SetCurShowMapName(int Id)
    {
        int Number = 0;
        for (int i = 0; i < MapMaxCount; i++)
        {
            Number++;
            if (ListMapNode[i].GateID == Id)
            {
                break;
            }
        }

        if (Number == 0)
        {
            return Number;
        }
        else
        {
            return Number - 1;
        }
    }
    /// <summary>
    /// 每一章的关卡数
    /// </summary>
    private List<int> MapNumber = new List<int> { 3, 8, 7, 9, 7, 8, 9, 9, 9, 9, 8, 9, 3 };
    /// <summary>
    /// 开关地图的摄像机
    /// </summary>
    /// <param name="IsOpen"></param>
    public void SetCamera(bool IsOpen)
    {
        if (IsOpen)
        {
            MapCamera.gameObject.SetActive(true);
        }
        else
        {
            MapCamera.gameObject.SetActive(false);
        }
    }
}
