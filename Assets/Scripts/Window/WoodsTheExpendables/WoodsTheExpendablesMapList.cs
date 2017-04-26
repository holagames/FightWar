using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WoodsTheExpendablesMapList : MonoBehaviour
{
    public GameObject MapListObj;
    private List<GameObject> MapList = new List<GameObject>();
    public GameObject JianTouObj;
    public GameObject BuffObj;
    private GameObject WoodsTheExpendables;
    public GameObject BaoWu;
    public GameObject HeroObj;
    public Vector3 InstantiateItemPosition = new Vector3(0f, 23, 11);
    public Camera CameraObj;
    private string CurDownName = "";
    public GameObject MoveObj;
    private bool DownMoveandUI = false;
    private float LeftMoveMax = 0.6f;
    //----------------------数据
    private int HeroDistance = 0;
    private int ArrowDistance = 0;
    public int CurFoor = 0;
    public Vector3 StartPosition = new Vector3(0f, 23, 11);
    private Vector3 DownPosition = new Vector3();
    private Vector3 DownItemParentPosition = new Vector3();
    public GameObject SkipWindow;
    public int halfFloor = 0;
    //----------------------数据
    private GameObject TansuoGroundObj;
    public bool isClickHero = false;

    private int CurFoorNum=0;//诞生的最新物品楼层ID
    // Use this for initialization
    void Start()
    {
        if (UIRootExtend.instance.isUiRootRatio != 1f)
        {
            CameraObj.GetComponent<Camera>().fieldOfView = 29 *UIRootExtend.instance.isUiRootRatio;
        }
        else 
        {
            CameraObj.GetComponent<Camera>().fieldOfView = 29;
        }
        SkipWindow = GameObject.Find("WoodsTheExpendables").transform.Find("SkipWindow").gameObject;
        if (GameObject.Find("WoodsTheExpendables") != null)
        {
            WoodsTheExpendables = GameObject.Find("WoodsTheExpendables");
            CurFoor = WoodsTheExpendables.GetComponent<WoodsTheExpendables>().CurFloor;
        }

        if (WoodsTheExpendables.GetComponent<WoodsTheExpendables>().isUpFloor == false)
        {
            //StartInstantiate(false);
            newStartInstantiate(false);
        }
        else
        {
            //StartInstantiate(true);
            newStartInstantiate(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(0))
        //{
        //    if (UICamera.hoveredObject.name == "UIRoot")
        //    {
        //        Ray ray = CameraObj.ScreenPointToRay(Input.mousePosition);
        //        RaycastHit hit;
        //        if (Physics.Raycast(ray, out hit))
        //        {
        //            if (hit.collider.gameObject.name == "JianTouObj" || MoveListItem(hit.collider.gameObject.name))
        //            {

        //                CurDownName = hit.collider.gameObject.name;
        //                DownItemParentPosition = MoveObj.transform.localPosition;
        //                DownPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z);
        //                DownMoveandUI = true;
        //            }
        //            else
        //            {

        //                DownMoveandUI = false;
        //            }
        //        }
        //    }
        //    else
        //    {
        //        DownMoveandUI = false;
        //    }
        //}
        //if (Input.GetMouseButton(0))
        //{
        //    if (UICamera.hoveredObject.name == "UIRoot" && DownMoveandUI)
        //    {

        //        if (Vector2.Distance(Input.mousePosition, DownPosition) > 5f)
        //        {
        //            MoveItemVector();
        //        }

        //    }
        //    else
        //    {
        //        DownMoveandUI = false;
        //    }
        //}
        if (Input.GetMouseButtonDown(0))
        {
            if (CharacterRecorder.instance.isOpenAdvance == false)
            {
                DownMoveandUI = false;
                if (UICamera.hoveredObject != null)
                {
                    if (UICamera.hoveredObject.name == "UIRoot")
                    {
                        Ray ray = CameraObj.ScreenPointToRay(Input.mousePosition);
                        RaycastHit[] hit;
                        hit = Physics.RaycastAll(ray);
                        //if (Physics.Raycast(ray, out hit))
                        for (int i = 0; i < hit.Length; i++)
                        {
                            Debug.LogError("1231223   " + hit[i].collider.gameObject.name + "     " + CurDownName);
                            if (MoveListItem(hit[i].collider.gameObject.name))
                            {
                                if (GameObject.Find("WoodsTheExpendables") != null)
                                {
                                    WoodsTheExpendables = GameObject.Find("WoodsTheExpendables");
                                    CurFoor = WoodsTheExpendables.GetComponent<WoodsTheExpendables>().CurFloor;
                                }
                                CharacterRecorder.instance.NowFloor = CurFoor;
                                Debug.LogError("1231223   " + hit[i].collider.gameObject.name + "     " + CurFoor);
                                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                                if (hit[i].collider.gameObject.name == (CurFoor + 1).ToString())
                                {
                                    if (GameObject.Find("WoodsTheExpendables") != null)
                                    {
                                        if (TextTranslator.instance.GetTowerByID(int.Parse(hit[i].collider.gameObject.name)) != null)
                                        {
                                            if (isClickHero == false)
                                            {
                                                WindowOpen(TextTranslator.instance.GetTowerByID(int.Parse(hit[i].collider.gameObject.name)).Type);
                                            }
                                        }
                                    }
                                }
                            }

                        }
                    }
                }

            }
        }
    }
    /// <summary>
    /// 判断点击的是不是可滑动的列表
    /// </summary>
    private bool MoveListItem(string name)
    {
        for (int i = 0; i < MapList.Count; i++)
        {
            if (MapList[i] != null)
            {
                if (name == MapList[i].name)
                {
                    return true;
                }
            }
        }
        return false;
    }

    /// <summary>
    /// 自动过关卡点击事件
    /// </summary>
    public void NotGetMouseToChooseWindow() 
    {
        //if (CharacterRecorder.instance.isOpenAdvance == false)
        //{
        //    DownMoveandUI = false;
        //    if (UICamera.hoveredObject != null)
        //    {
        //        if (UICamera.hoveredObject.name == "UIRoot")
        //        {
        //            if (MoveListItem(CurFoorNum.ToString()))
        //            {
        //                if (GameObject.Find("WoodsTheExpendables") != null)
        //                {
        //                    WoodsTheExpendables = GameObject.Find("WoodsTheExpendables");
        //                    CurFoor = WoodsTheExpendables.GetComponent<WoodsTheExpendables>().CurFloor;
        //                }
        //                CharacterRecorder.instance.NowFloor = CurFoor;
        //                if (CurFoorNum.ToString() == (CurFoor + 1).ToString())
        //                {
        //                    if (GameObject.Find("WoodsTheExpendables") != null)
        //                    {
        //                        if (TextTranslator.instance.GetTowerByID(CurFoorNum) != null)
        //                        {
        //                            if (isClickHero == false)
        //                            {
        //                                WindowOpen(TextTranslator.instance.GetTowerByID(CurFoorNum).Type);
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //}

        if (CharacterRecorder.instance.isOpenAdvance == false)
        {
            DownMoveandUI = false;
            if (GameObject.Find("WoodsTheExpendables") != null)
            {
                WoodsTheExpendables = GameObject.Find("WoodsTheExpendables");
                CurFoor = WoodsTheExpendables.GetComponent<WoodsTheExpendables>().CurFloor;
            }
            CharacterRecorder.instance.NowFloor = CurFoor;
            if (CurFoorNum.ToString() == (CurFoor + 1).ToString())
            {
                if (GameObject.Find("WoodsTheExpendables") != null)
                {
                    if (TextTranslator.instance.GetTowerByID(CurFoorNum) != null)
                    {
                        if (isClickHero == false)
                        {
                            WindowOpen(TextTranslator.instance.GetTowerByID(CurFoorNum).Type);
                        }
                    }
                }
            }

        }
    }

    ///// <summary>
    ///// 计算摄像机需要移动的位置  ---可以删除内容
    ///// </summary>
    //private void MoveItemVector()
    //{
    //    //float x = Input.mousePosition.x - DownPosition.x;
    //    //float Positionx = ((float)Screen.width / 1422) * 0.01f;
    //    //MoveItemPosition(Positionx * x);
    //}
    ///// <summary>
    ///// 做摄像机的移动 ---可以删除内容
    ///// </summary>
    //private void MoveItemPosition(float PositionX)
    //{


    //    float x = DownItemParentPosition.x + PositionX;
    //    //if (DownItemParentPosition.x + PositionX > 0.4)
    //    //{
    //    //    x = 0.4f;
    //    //}
    //    //if (DownItemParentPosition.x + PositionX < -(MapList.Count - 3) * 3)
    //    //{
    //    //    x = -(MapList.Count - 3) * 3;
    //    //}
    //    MoveObj.transform.localPosition = new Vector3(x, MoveObj.transform.localPosition.y, MoveObj.transform.localPosition.z);
    //    if (WoodsTheExpendables != null)
    //    {
    //        WoodsTheExpendables.GetComponent<WoodsTheExpendables>().UpdatePosition(x);
    //    }
    //}
    /// <summary>
    /// 选择开启那个界面
    /// </summary>
    private void WindowOpen(int Type)
    {
        switch (Type)
        {
            case 1:
                //PictureCreater.instance.IsWood = true;
                if (CharacterRecorder.instance.GuideID[12] == 10)
                {
                    isClickHero = true;
                    GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().UpdateWindow(WoodsTheExpendablesWindowType.FightWindow);
                }
                else
                {
                    if (CharacterRecorder.instance.HistoryFloor == 50)
                    {
                        halfFloor = 30;
                        if (CharacterRecorder.instance.Vip >= 5)
                        {
                            halfFloor = 40;
                        }
                        if (CharacterRecorder.instance.Vip >= 9)
                        {
                            halfFloor = 45;
                        }
                    }
                    else
                    {
                        halfFloor = CharacterRecorder.instance.HistoryFloor / 2;
                    }
                    if (halfFloor < 10)
                    {
                        halfFloor = 10;
                    }
                    if (halfFloor >= CurFoor + 1 && halfFloor != 0)
                    {
                        if (CharacterRecorder.instance.isSkip == false || GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().CurIsjump == false)
                        {
                            SkipWindow.SetActive(true);
                            SendSkipInfo();
                        }
                        else
                        {
                            NetworkHandler.instance.SendProcess("1503#" + (CurFoor + 1) + ";3;3;");
                        }
                    }
                    else if (PlayerPrefs.GetInt("StormBuff" + "_" + PlayerPrefs.GetString("ServerID") + "_" + PlayerPrefs.GetInt("UserID")) == 1)
                    {
                        NetworkHandler.instance.SendProcess("1503#" + (CurFoor + 1) + ";3;3;");
                        PlayerPrefs.SetInt("StormBuff" + "_" + PlayerPrefs.GetString("ServerID") + "_" + PlayerPrefs.GetInt("UserID"), 0);
                    }
                    else
                    {
                        isClickHero = true;
                        GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().UpdateWindow(WoodsTheExpendablesWindowType.FightWindow);
                    }
                    //CharacterRecorder.instance.CanMoveLayer = halfFloor;
                }
                break;
            case 2:
                NetworkHandler.instance.SendProcess("1504#" + (CurFoor + 1) + ";");
                break;
            case 3:
                NetworkHandler.instance.SendProcess("1502#" + (CurFoor + 1) + ";");
                //GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().UpdateWindow(WoodsTheExpendablesWindowType.FightWindow);
                break;
        }
    }

 
    /// <summary>
    /// 生成箭头
    /// </summary>
    private void InstantiateJianTou(int Number, Vector3 ObjPosition)
    {
        GameObject obj = GameObject.Instantiate(JianTouObj) as GameObject;
        obj.SetActive(true);

        obj.transform.parent = MapListObj.transform;
        obj.transform.localScale = Vector3.one;
        obj.transform.localPosition = ObjPosition;
        obj.name = "JianTou" + Number.ToString();
        MapList.Add(obj);
    }
    //坐标计算
    public void ItemPosition(int Num)
    {
        switch (Num)
        {
            case 0:
                InstantiateItemPosition = TansuoGroundObj.transform.Find("Point0").transform.localPosition;
                break;
            case 1:
                InstantiateItemPosition = TansuoGroundObj.transform.Find("Point1").transform.localPosition;
                break;
            case 2:
                InstantiateItemPosition = TansuoGroundObj.transform.Find("Point2").transform.localPosition;
                break;
            case 3:
                InstantiateItemPosition = TansuoGroundObj.transform.Find("Point3").transform.localPosition;
                break;
            case 4:
                InstantiateItemPosition = TansuoGroundObj.transform.Find("Point4").transform.localPosition;
                break;
            case 5:
                InstantiateItemPosition = TansuoGroundObj.transform.Find("Point5").transform.localPosition;
                break;
        }
    }
    //跳过战斗
    public void SendSkipInfo()
    {
        SkipWindow.transform.Find("FloorNumber").GetComponent<UILabel>().text = halfFloor.ToString() + "层";
        UIEventListener.Get(SkipWindow.transform.Find("EscButton").gameObject).onClick += delegate(GameObject go)
        {
            SkipWindow.SetActive(false);
        };
        UIEventListener.Get(SkipWindow.transform.Find("NoSkipButton").gameObject).onClick += delegate(GameObject go)
        {
            GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().UpdateWindow(WoodsTheExpendablesWindowType.FightWindow);
            SkipWindow.SetActive(false);
        };
        UIEventListener.Get(SkipWindow.transform.Find("SkipButton").gameObject).onClick += delegate(GameObject go)
        {
            if (SkipWindow.transform.Find("CheckMessage").GetComponent<UIToggle>().value == true)
            {
                NetworkHandler.instance.SendProcess("1507#");
                CharacterRecorder.instance.isSkip = true;
                GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().IsOpenAutoButton();
            }
            NetworkHandler.instance.SendProcess("1503#" + (CurFoor + 1) + ";3;3;");
            SkipWindow.SetActive(false);
        };

    }


    /// <summary>
    /// 新生成场景
    /// </summary>
    public void newStartInstantiate(bool isNextFloor)
    {
        TansuoGroundObj = GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().GroundObj;
        int nowfloor = 0;
        if (WoodsTheExpendables.GetComponent<WoodsTheExpendables>().CurFloor == 50)
        {
            nowfloor = WoodsTheExpendables.GetComponent<WoodsTheExpendables>().CurFloor;
        }
        else
        {
            nowfloor = WoodsTheExpendables.GetComponent<WoodsTheExpendables>().CurFloor + 1;

            int Num = nowfloor % 6;
            if (TextTranslator.instance.GetTowerByID(nowfloor).Type == 1)
            {
                if (isNextFloor == false)
                {
                    newInstantiateHero("6000" + Random.Range(1, 6).ToString(), nowfloor, Num, isNextFloor);
                }
                else
                {
                    newInstantiateHero("6000" + Random.Range(1, 6).ToString(), nowfloor, Num, isNextFloor);
                    GameObject go = GameObject.Instantiate(Resources.Load("Prefab/Effect/XiaoShi_JueSe", typeof(GameObject))) as GameObject;
                    int pointid;
                    if (Num == 0)
                    {
                        pointid = 5;
                    }
                    else
                    {
                        pointid = Num - 1;
                    }
                    go.transform.parent = GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().GroundObj.transform.Find("Point" + pointid.ToString()).transform;
                    go.transform.localPosition = new Vector3(0.4f, 0, 0);
                }
            }
            else if (TextTranslator.instance.GetTowerByID(nowfloor).Type == 2)
            {
                if (isNextFloor == false)
                {
                    newInstantiateItem(TansuoGroundObj.transform.Find("Point" + (Num).ToString()).transform.Find("BuffObJ").gameObject, nowfloor, Num, isNextFloor);
                }
                else
                {
                    newInstantiateItem(TansuoGroundObj.transform.Find("Point" + (Num).ToString()).transform.Find("BuffObJ").gameObject, nowfloor, Num, isNextFloor);
                    int pointid;
                    if (Num == 0)
                    {
                        pointid = 5;
                    }
                    else
                    {
                        pointid = Num - 1;
                    }
                    GameObject go = GameObject.Instantiate(Resources.Load("Prefab/Effect/XiaoShi_JueSe", typeof(GameObject))) as GameObject;
                    go.transform.parent = GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().GroundObj.transform.Find("Point" + pointid.ToString()).transform;
                    go.transform.localPosition = new Vector3(0.4f, 0, 0);
                }

            }
            else if (TextTranslator.instance.GetTowerByID(nowfloor).Type == 3)
            {
                GameObject baowu = TansuoGroundObj.transform.Find("Point" + (Num).ToString()).transform.Find("BaoWuObj").gameObject;
                if (isNextFloor == false)
                {
                    newInstantiateItem(baowu, nowfloor, Num, isNextFloor);
                }
                else
                {
                    newInstantiateItem(baowu, nowfloor, Num, isNextFloor);
                    GameObject go = GameObject.Instantiate(Resources.Load("Prefab/Effect/XiaoShi_JueSe", typeof(GameObject))) as GameObject;
                    int pointid;
                    if (Num == 0)
                    {
                        pointid = 5;
                    }
                    else
                    {
                        pointid = Num - 1;
                    }
                    go.transform.parent = GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().GroundObj.transform.Find("Point" + pointid.ToString()).transform;
                    go.transform.localPosition = new Vector3(0.4f, 0, 0);
                }
                if (nowfloor % 10 == 0)
                {
                    baowu.transform.Find("GameObject/ChongTu_BaoXiang_Jin").gameObject.SetActive(true);
                }
                else if (nowfloor % 10 == 8)
                {
                    baowu.transform.Find("GameObject/ChongTu_BaoXiang_Yin").gameObject.SetActive(true);
                }
                else if (nowfloor % 10 == 4)
                {
                    baowu.transform.Find("GameObject/ChongTu_BaoXiang_Tong").gameObject.SetActive(true);
                }
            }
        }
    }
    private void newInstantiateHero(string Id, int Number, int Num, bool isNextFloor)
    {
        CurFoorNum = Number;
        GameObject obj = GameObject.Instantiate(Resources.Load("Prefab/Role/" + Id, typeof(GameObject))) as GameObject;
        GameObject Obj1 = GameObject.Find("Point" + Num.ToString()).transform.Find("HeroObj").gameObject;
        if (Num == 3)
        {
            Obj1.transform.localPosition = new Vector3(0.4f, 0.393f, 0);
        }
        else if (Num == 5)
        {
            Obj1.transform.localPosition = new Vector3(0.4f, 0.09f, 0.05f);
        }
        else if (Num == 1)
        {
            Obj1.transform.localPosition = new Vector3(0.4f, 0.23f, 0.01f);
        }
        else
        {
            Obj1.transform.localPosition = new Vector3(0.4f, 0.225f, 0);
        }
        Obj1.SetActive(true);
        Obj1.name = Number.ToString();
        obj.transform.parent = Obj1.transform;
        obj.transform.localScale = new Vector3(3f, 3f, 3f);
        obj.transform.localPosition = new Vector3(0, 0f, 0f);
        obj.transform.localRotation = new Quaternion(0, 0, 0, 0);
        obj.transform.Rotate(0, 180, 0);
        obj.name = Number.ToString();
        MapList.Add(obj);        
        if (GameObject.Find("WoodsTheExpendables") != null)
        {
            GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().ShowNumber(Number, obj);
        }
    }

    /// <summary>
    /// 生成其他的模型
    /// </summary>
    private void newInstantiateItem(GameObject obj, int Number, int Num, bool isNextFloor)
    {
        CurFoorNum = Number;
        obj.SetActive(true);
        obj.transform.localScale = Vector3.one;
        if (Number != 0)
        {
            obj.name = Number.ToString();
            MapList.Add(obj);
        }
    }
}
