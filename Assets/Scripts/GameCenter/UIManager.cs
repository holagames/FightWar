using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Umeng;
using CodeStage.AntiCheat.ObscuredTypes;

public class UIManager : MonoBehaviour
{
    public Dictionary<string, GameObject> panelList = new Dictionary<string, GameObject>();

    public static UIManager instance;
    const string UI_ROOT = "UIRoot";
    private GameObject parent;
    public bool OpenNewGuideAnchor = true;      //是否统计数据
    public bool MapGateInfoLoading = false;
    public string CurGateId = "";
    Stack<string> StackWindow = new Stack<string>();
    Stack<bool> StackCloseWindow = new Stack<bool>();

    public string[] newGuideList = null;
    public string[] SystemProcedure = null;
    public string[] ActivityProcedure = null;
    public enum NewGuidState
    {
        START = 0,
        END = 1
    }
    // Use this for initialization
    void Start()
    {
        instance = this;

        panelList.Clear();
        parent = GameObject.Find(UI_ROOT);
        //testCount();          //测试
        //testNewGuideAnchor();   //测试
        InitSystems();
        InitActivitys();
        InitPlayerNewGuideList();
        if (Application.loadedLevelName == "Downloader")
        {
            OpenPanel("InitWindow");
        }
    }

    public GameObject OpenPanel(string _name)
    {
        if (panelList.ContainsKey(_name))
        {
#if DEVELOP_MODE
            Debug.Log("Panel " + _name + " already added!");
#endif
            if (panelList[_name] != null)
            {
                panelList[_name].SetActive(true);
            }
            else
            {
                GameObject panel = (GameObject)Instantiate(Resources.Load("GUI/" + _name));
                Transform tt = panel.transform;
                tt.parent = parent.transform;
                tt.localPosition = Vector3.zero;
                tt.localRotation = Quaternion.identity;
                tt.localScale = Vector3.one;
                panel.layer = parent.layer;
                panel.name = _name;
                panelList[_name] = panel;
            }
            LuaDeliver.instance.UILoadLua(_name);
            return panelList[_name];
        }
        //Debug.LogError(_name);
        GameObject go = (GameObject)Instantiate(Resources.Load("GUI/" + _name));
        Transform t = go.transform;
        t.parent = parent.transform;
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
        go.layer = parent.layer;
        go.name = _name;
        panelList.Add(_name, go);
        LuaDeliver.instance.UILoadLua(_name);
        return go;
    }

    /// <summary>
    /// Open one panel and close all others
    /// </summary>
    public GameObject OpenPanel(string _name, bool closeOtherPanel)
    {
        if (closeOtherPanel)
        {
            DestroyAllPanel();
            //CloseAllPanel();
        }

        if (_name != "LoadWindow" && _name != "LoadingWindow" && _name != "LevelUpWindow")
        {
            if (_name == "MainWindow")
            {
                closeOtherPanel = true;
                _name = "MainWindow";
            }

            if (GameObject.Find(_name) == null)
            {
                StackWindow.Push(_name);
                StackCloseWindow.Push(closeOtherPanel);
            }
        }
        return OpenPanel(_name);
    }

    public GameObject OpenSinglePanel(string _name, bool closeOtherPanel)
    {
        if (closeOtherPanel)
        {
            DestroyAllPanel();
            //CloseAllPanel();
        }

        if (_name != "LoadWindow" && _name != "LoadingWindow" && _name != "LevelUpWindow")
        {
            if (_name == "MainWindow")
            {
                closeOtherPanel = true;
                _name = "MainWindow";
            }
        }
        return OpenPanel(_name);
    }

    public GameObject OpenPanelOther(string _name, bool closeOtherPanel)
    {
        if (closeOtherPanel)
        {
            DestroyOtherPanel(_name);
            //CloseAllPanel();
        }

        if (_name != "LoadWindow" && _name != "LoadingWindow" && _name != "LevelUpWindow")
        {
            if (_name == "MainWindow")
            {
                closeOtherPanel = true;
            }

            StackWindow.Push(_name);
            StackCloseWindow.Push(closeOtherPanel);
        }
        return OpenPanel(_name);
    }

    public void BackUI()
    {
        GameObject vip = GameObject.Find("VIPShopWindow");
        if (vip != null)
        {
            GameObject vipPrivilege = vip.transform.FindChild("All/VipPrivilegeScrollView").gameObject;
            if (vipPrivilege.activeSelf)
            {
                vip.GetComponent<VipShopWindow>().SetChangeShop();
                return;
            }
        }
        string LastWindow = StackWindow.Pop();
        if (!StackCloseWindow.Pop())
        {
            DestroyPanel(LastWindow);
        }
        
        if (StackWindow.Count > 0)
        {
            string NowWindow = StackWindow.Pop();
            bool IsCloseOther = StackCloseWindow.Pop();


            if (NowWindow == "FightWindow")
            {
                NowWindow = StackWindow.Pop();
                IsCloseOther = StackCloseWindow.Pop();
            }

            OpenPanelOther(NowWindow, IsCloseOther);
        }
        else
        {
            OpenPanelOther("MainWindow", true);
        }
    }

    public void BackTargetUI(string TargetWindow)
    {
        string LastWindow = StackWindow.Pop();
        if (!StackCloseWindow.Pop())
        {
            DestroyPanel(LastWindow);
        }

        if (StackWindow.Count > 0)
        {
            string NowWindow = StackWindow.Pop();
            bool IsCloseOther = StackCloseWindow.Pop();


            if (NowWindow == "FightWindow")
            {
                NowWindow = StackWindow.Pop();
                IsCloseOther = StackCloseWindow.Pop();
            }

            Debug.Log("Pop!!!!!" + NowWindow + " " + IsCloseOther);

            while (NowWindow != TargetWindow)
            {
                if (StackWindow.Count > 0)
                {
                    if (!IsCloseOther)
                    {
                        DestroyPanel(NowWindow);
                    }
                    NowWindow = StackWindow.Pop();
                    IsCloseOther = StackCloseWindow.Pop();
                }
                else
                {
                    OpenPanelOther("MainWindow", true);
                }
            }

            OpenPanelOther(NowWindow, IsCloseOther);
        }
        else
        {
            OpenPanelOther("MainWindow", true);
        }
    }

    public bool TheWindowIsStay(string name)
    {
        if (StackWindow.Contains(name))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void PopTowUI()
    {
        StackWindow.Pop();
        StackWindow.Pop();
        StackCloseWindow.Pop();
        StackCloseWindow.Pop();
    }

    public void PopUI()
    {
        StackWindow.Pop();
        StackCloseWindow.Pop();
    }
    public void BackTwoUI(string NextWindow)
    {
        if (StackWindow.Count > 2)
        {
            string LastWindow = StackWindow.Pop();
            if (!StackCloseWindow.Pop())
            {
                DestroyPanel(LastWindow);
            }
        }

        if (StackWindow.Count > 1)
        {
            string LastWindow = StackWindow.Pop();
            if (!StackCloseWindow.Pop())
            {
                DestroyPanel(LastWindow);
            }
        }

        if (StackWindow.Count > 0)
        {
            string NowWindow = StackWindow.Pop();
            bool IsCloseOther = StackCloseWindow.Pop();

            if (NowWindow != NextWindow)
            {
                OpenPanelOther(NextWindow, true);
            }
            else
            {
                OpenPanelOther(NowWindow, IsCloseOther);
            }
        }
        else
        {
            OpenPanelOther(NextWindow, true);
        }

    }

    /// <summary>
    /// Open one panel and close all others
    /// </summary>
    public T OpenPanel<T>(string _name, bool closeOtherPanel) where T : Component
    {
        if (closeOtherPanel)
        {
            DestroyAllPanel();
            //CloseAllPanel();
        }
        return OpenPanel(_name).GetComponent<T>();
    }

    public T OpenPanel<T>(string _name) where T : Component
    {
        return OpenPanel(_name).GetComponent<T>();
    }


    public void DestroyAllPanel()
    {
        foreach (GameObject go in panelList.Values)
        {
            if (go != null)
            {
                DestroyImmediate(go);
            }
        }

        panelList.Clear();
        //Resources.UnloadUnusedAssets();
    }

    public void DestroyOtherPanel(string _name)
    {
        foreach (GameObject go in panelList.Values)
        {
            if (go != null && go.name != _name)
            {
                DestroyImmediate(go);
            }
        }

        panelList.Clear();
        panelList.Add(_name, GameObject.Find(_name));
        //Resources.UnloadUnusedAssets();
    }

    public void DestroyPanel(string _name)
    {
        if (panelList.ContainsKey(_name))
        {
            if (panelList[_name] != null)
            {
                Destroy(panelList[_name]);
            }

            return;
        }
        //Resources.UnloadUnusedAssets();
    }

    public void ClosePanel(string _name)
    {
        if (panelList.ContainsKey(_name))
        {
            if (panelList[_name] != null)
            {
                panelList[_name].SetActive(false);
            }
        }
    }

    public void CloseAllPanel()
    {
        foreach (GameObject go in panelList.Values)
        {
            if (go != null)
            {
                if (go.activeSelf)
                {
                    go.SetActive(false);
                }
            }
        }
    }

    public void OpenGuideWindow()
    {
        StartCoroutine(IOpenGuideWindow());
    }

    IEnumerator IOpenGuideWindow()
    {
        OpenPanel("NewGuideWindow");
        yield return new WaitForSeconds(0.2f);
        //LuaDeliver.instance.LoadGuide();
    }

    /// <summary>
    /// open prompt window
    /// </summary>
    /// <param name="message">prompt messages</param>
    /// <param name="type">type of prompt window, alert or confirm</param>
    /// <param name="onConfirm"></param>
    /// <param name="onCancel"></param>
    public void OpenPromptWindow(string message, PromptWindow.PromptType type,
        PromptWindow.OnConfirm onConfirm, PromptWindow.OnCancel onCancel)
    {
        if (type == PromptWindow.PromptType.Hint)
        {
            UIManager.instance.CreateLabelEffect(message, new Vector3(0, 180, 0), new Vector3(0, 320, 0));
        }
        else if (type == PromptWindow.PromptType.Popup)
        {
            //PopupWindow po = OpenPanel<PopupWindow>("PopupWindow");
            //po.SetLabelOneMessage(message);
            //CreatePopup();
            UIManager.instance.CreatePopup();
        }
        else
        {
            PromptWindow pw = OpenPanel<PromptWindow>("PromptWindow");
            pw.SetPromptWindow(message, type, onConfirm, onCancel);
            pw.GetComponent<UIPanel>().depth = GetNextDepth();
        }
    }
    /// <summary>
    /// open prompt window
    /// </summary>
    /// <param name="message">prompt messages</param>
    /// <param name="type">type of prompt window, alert or confirm</param>
    /// <param name="onConfirm"></param>
    /// <param name="onCancel"></param>
    public void OpenPromptWindowNoTitle(string message, PromptWindow.PromptType type,
        PromptWindow.OnConfirm onConfirm, PromptWindow.OnCancel onCancel)
    {
        if (type == PromptWindow.PromptType.Hint)
        {
            UIManager.instance.CreateLabelEffect(message, new Vector3(0, 180, 0), new Vector3(0, 320, 0));
        }
        else
        {
            PromptWindow pw = OpenPanel<PromptWindow>("PromptWindowNoTitle");
            pw.SetPromptWindow(message, type, onConfirm, onCancel);
            pw.GetComponent<UIPanel>().depth = GetNextDepth();
        }
    }
    /// <summary>
    /// open prompt window
    /// </summary>
    /// <param name="message">prompt messages</param>
    /// <param name="type">type of prompt window, alert or confirm</param>
    /// <param name="onConfirm"></param>
    /// <param name="onCancel"></param>
    public void OpenPromptWindowNoTitle(string message, int layer, PromptWindow.PromptType type,
        PromptWindow.OnConfirm onConfirm, PromptWindow.OnCancel onCancel)
    {
        if (type == PromptWindow.PromptType.Hint)
        {
            UIManager.instance.CreateLabelEffect(message, new Vector3(0, 180, 0), new Vector3(0, 320, 0));
        }
        else
        {
            PromptWindow pw = OpenPanel<PromptWindow>("PromptWindowNoTitle");
            //pw.GetComponent<UIPanel>().depth = GetNextDepth();
            pw.SetPromptWindow(message, layer, type, onConfirm, onCancel);
        }
    }
    /// <summary>
    /// open prompt window
    /// </summary>
    /// <param name="message">prompt messages</param>
    /// <param name="type">type of prompt window, alert or confirm</param>
    /// <param name="onConfirm"></param>
    /// <param name="onCancel"></param>
    public void OpenPromptWindowWithCost(string message, int layer, int DiamondCost, PromptWindowWithCost.PromptType type,
        PromptWindowWithCost.OnConfirm onConfirm, PromptWindowWithCost.OnCancel onCancel)
    {
        if (type == PromptWindowWithCost.PromptType.Hint)
        {
            UIManager.instance.CreateLabelEffect(message, new Vector3(0, 180, 0), new Vector3(0, 320, 0));
        }
        else
        {
            PromptWindowWithCost pw = OpenPanel<PromptWindowWithCost>("PromptWindowWithCost");
            //pw.GetComponent<UIPanel>().depth = GetNextDepth();
            //pw.SetPromptWindow()
            //SetPromptWindow(string message, int layer, int DiamondCost, PromptType type, OnConfirm confirmCallback, OnCancel cancelCallBack)
            pw.SetPromptWindow(message, layer, DiamondCost, type, onConfirm, onCancel);
        }
    }
    /// <summary>
    /// open prompt window
    /// </summary>
    /// <param name="message">prompt messages</param>
    /// <param name="type">type of prompt window, alert or confirm</param>
    /// <param name="onConfirm"></param>
    /// <param name="onCancel"></param>
    public void OpenPromptWindow(string message, int Layer, bool isNeedUp, PromptWindow.PromptType type,
        PromptWindow.OnConfirm onConfirm, PromptWindow.OnCancel onCancel)
    {
        if (type == PromptWindow.PromptType.Hint)
        {
            UIManager.instance.CreateLabelEffect(message, Layer, isNeedUp, new Vector3(0, 180, 0), new Vector3(0, 320, 0));
        }
        else
        {
            PromptWindow pw = OpenPanel<PromptWindow>("PromptWindow");
            pw.SetPromptWindow(message, type, onConfirm, onCancel);
            pw.GetComponent<UIPanel>().depth = GetNextDepth();
        }
    }
    /// <summary>
    /// open prompt window
    /// </summary>
    /// <param name="message">prompt messages</param>
    /// <param name="type">type of prompt window, alert or confirm</param>
    /// <param name="onConfirm"></param>
    /// <param name="onCancel"></param>
    public void OpenPromptWindow(string message, int Layer, bool isNeedUp, Vector3 from, Vector3 to, PromptWindow.PromptType type,
        PromptWindow.OnConfirm onConfirm, PromptWindow.OnCancel onCancel)
    {
        if (type == PromptWindow.PromptType.Hint)
        {
            UIManager.instance.CreateLabelEffect(message, Layer, isNeedUp, from, to);
        }
        else
        {
            PromptWindow pw = OpenPanel<PromptWindow>("PromptWindow");
            pw.SetPromptWindow(message, type, onConfirm, onCancel);
            pw.GetComponent<UIPanel>().depth = GetNextDepth();
        }
    }
    /// <summary>
    /// 提示label
    /// </summary>
    /// <param name="effectLabel">提示信息</param>
    /// <param name="from">初始位置</param>
    /// <param name="to">最后位置</param>
    public void CreateLabelEffect(string effectLabel, Vector3 from, Vector3 to)
    {
        GameObject go = NGUITools.AddChild(parent, PictureCreater.instance.MyCamera.GetComponent<MouseClick>().LabelEffectPrefab);
        go.GetComponent<UILabelEffect>().SetLabelNormal(effectLabel, from, to);
        go.AddComponent<DestroySelf>();
    }

    //跑马灯界面
    public void CreatePopup()
    {
        //GameObject panel = (GameObject)Instantiate(Resources.Load("GUI/" + "PopupWindow"));
        GameObject go = (GameObject)Instantiate(Resources.Load("GUI/PopupWindow"));//NGUITools.AddChild(parent, panel);
        go.name = "PopupWindow";
        Transform t = go.transform;
        t.parent = parent.transform;
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
        go.transform.localPosition = new Vector3(0, 350f, 0);
        go.GetComponent<PopupWindow>().SetlabelmoreMessage();
    }
    //战力变更界面
    public void CreateForceChange(int Forcebefore, int ForceNow)
    {
        //GameObject panel = (GameObject)Instantiate(Resources.Load("GUI/" + "ForceChangesWindow"));
        GameObject go = (GameObject)Instantiate(Resources.Load("GUI/ForceChangesWindow"));//NGUITools.AddChild(parent, Resources.Load("GUI/" + "ForceChangesWindow"));
        go.name = "ForceChangesWindow";
        Transform t = go.transform;
        t.parent = parent.transform;
        t.localPosition = Vector3.zero;
        t.localRotation = Quaternion.identity;
        t.localScale = Vector3.one;
        go.transform.localPosition = new Vector3(0, 280f, 0);
        go.GetComponent<ForceChangesWindow>().LookForceChange(Forcebefore, ForceNow);
    }
    /// <summary>
    /// 提示label
    /// </summary>
    /// <param name="effectLabel">提示信息</param>
    /// <param name="from">初始位置</param>
    /// <param name="to">最后位置</param>
    public void CreateLabelEffect(string effectLabel, int layer, bool isNeedUp, Vector3 from, Vector3 to)
    {
        GameObject go = NGUITools.AddChild(parent, PictureCreater.instance.MyCamera.GetComponent<MouseClick>().LabelEffectPrefab);
        go.GetComponent<UILabelEffect>().SetLabelNormal(effectLabel, isNeedUp, from, to);
        go.AddComponent<DestroySelf>();
        go.layer = layer;
        foreach (Component c in go.GetComponentsInChildren(typeof(Transform), true))
        {
            c.gameObject.layer = layer;
        }
    }

    ///// <summary>
    ///// 打开获得物品的窗口
    ///// </summary>
    //public void OpenGetItemsWindow(BetterList<Item> itemList)
    //{
    //    GetItemsWindow giw = OpenPanel<GetItemsWindow>("GetItemsWindow");
    //    giw.SetItems(itemList);
    //    giw.GetComponent<UIPanel>().depth = GetNextDepth();
    //}

    ///// <summary>
    ///// 打开单抽抽奖获得物品的窗口
    ///// </summary>
    //public void OpenGetItemsCapsuleWindow(BetterList<Item> itemlist)
    //{
    //    GetItemCapsuleWindow gicw = OpenPanel<GetItemCapsuleWindow>("GetItemCapsuleWindow");
    //    GameObject.Find("UIRoot/GetItemCapsuleWindow").GetComponent<GetItemCapsuleWindow>().SetItems(itemlist);
    //    gicw.GetComponent<UIPanel>().depth = GetNextDepth();
    //}

    ///// <summary>
    ///// 打开十抽抽奖获得物品的窗口
    ///// </summary>
    //public void OpenGetItemsCapsuleWindowTen(BetterList<Item> itemlist)
    //{
    //    GetItemCapsuleWindowTen gicw = OpenPanel<GetItemCapsuleWindowTen>("GetItemCapsuleWindowTen");
    //    //OpenPanel("GetItemCapsuleWindowTen");
    //    GameObject.Find("UIRoot/GetItemCapsuleWindowTen").GetComponent<GetItemCapsuleWindowTen>().SetItems(itemlist);
    //    gicw.GetComponent<UIPanel>().depth = GetNextDepth();
    //}

    ///// <summary>
    ///// 打开编队界面
    ///// </summary>
    //public NewTeamWindow OpenTeamWindow(NewTeamWindow.OpenType openType, bool closeOtherPanel)
    //{
    //    NewTeamWindow teamWindow = OpenPanel<NewTeamWindow>("NewTeamWindow", closeOtherPanel);
    //    teamWindow.SetOpenType(openType);
    //    return teamWindow;
    //}

    ///// <summary>
    ///// 提示label
    ///// </summary>
    ///// <param name="effectLabel">提示信息</param>
    ///// <param name="from">初始位置</param>
    ///// <param name="to">最后位置</param>
    //public void CreateLabelEffect(string effectLabel, Vector3 from, Vector3 to)
    //{
    //    GameObject go = NGUITools.AddChild(parent, labelEffectPrefab);
    //    go.GetComponent<UILabelEffect>().SetLabelNormal(effectLabel, from, to);
    //}


    //public void OpenDialogWindow(string dialogID, DialogWindow.OnFinish onFinish)
    //{
    //    DialogWindow dw = OpenPanel<DialogWindow>("DialogWindow");
    //    dw.SetDialog(dialogID.ToString(), onFinish);
    //    dw.GetComponent<UIPanel>().depth = GetNextDepth();
    //}


    /// <summary>
    /// 得到下一个depth
    /// </summary>
    /// <returns></returns>
    public int GetNextDepth()
    {
        int highest = 0;
        foreach (GameObject go in panelList.Values)
        {
            if (go != null)
            {
                if (go.name != "LoadWindow" && go.name != "LoadingWindow" && go.name != "LevelUpWindow")
                {
                    if (highest < go.GetComponent<UIPanel>().depth)
                    {
                        highest = go.GetComponent<UIPanel>().depth;
                    }
                }
            }
        }
        return highest + 1;
    }

    ///// <summary>
    ///// 打开获得物品的窗口
    ///// </summary>
    public void OpenAwardWindow(BetterList<Item> itemlist, AwardWindow.SureOnClick sureOnClick)
    {
        AwardWindow aw = OpenPanel<AwardWindow>("AwardWindow");
        aw.SetInfo(itemlist, sureOnClick);
        aw.GetComponent<UIPanel>().depth = GetNextDepth();
    }
    ///////////////////////////////////////////新手引导锚点（以下）//////////////////////////////////////
    public void NewGuideAnchor(AnchorIndex _Index)
    {
        if (OpenNewGuideAnchor)
        {
            if (newGuideList[(int)_Index - 1] == ((int)NewGuidState.START).ToString())
            {
                if (newGuideList != null)
                {
                    Debug.LogError("设置新手引导锚点测试编号1：" + newGuideList[(int)_Index - 1]);
                    newGuideList[(int)_Index - 1] = ((int)NewGuidState.END).ToString();

                    SetPlayerNewGuideList();
                }
                string test_str = "Hola52_";
                Dictionary<string, string> sendInfos = new Dictionary<string, string>();
                string _index_name = System.Enum.GetName(typeof(AnchorIndex), _Index).Replace("index_", "");
                Debug.LogError("新手引导锚点测试编号2：" + _index_name);
                if (CharacterRecorder.instance == null)
                {
                    sendInfos[test_str + "Anchor"] = _index_name;
                    sendInfos[test_str + "level"] = "0";
                    GA.Event("NewGuideAnchor", sendInfos);
                    return;
                }
                sendInfos[test_str + "Anchor"] = _index_name;
                sendInfos[test_str + "level"] = CharacterRecorder.instance.level.ToString();

                //sendInfo = (int)_Index + "$" + CharacterRecorder.instance.level + "$" + CharacterRecorder.instance.lunaGem + "$" + CharacterRecorder.instance.gold;
                GA.Event("NewGuideAnchor", sendInfos);
            }
        }
    }

    public void InitPlayerNewGuideList()
    {
        //Debug.LogWarning("初始化新手引导" + guid);
        string playerKey = "Procedure";
        int _len = System.Enum.GetNames(typeof(AnchorIndex)).GetLength(0);
        //Debug.LogWarning("初始化新手引导锚点列表：" + guid + " " + newGuideList.Length + " " + _len);

        bool isInitAnchorList = false;
        if (PlayerPrefs.HasKey(playerKey))
        {
            newGuideList = PlayerPrefs.GetString(playerKey).Split(';');

            if (newGuideList.Length < _len + 1)
            {
                isInitAnchorList = true;
            }
        }
        else
        {
            isInitAnchorList = true;
        }

        if (isInitAnchorList)
        {
            newGuideList = new string[_len + 1];
            for (int i = 0; i < _len; i++)
            {
                newGuideList[i] = ((int)NewGuidState.START).ToString();
            }
            SetPlayerNewGuideList();
        }
    }

    public void SetPlayerNewGuideList()
    {
        //Debug.LogWarning("设置新手引导去重列表中的GUID：" + ObscuredPrefs.GetInt("CharacterID"));
        string playerKey = "Procedure";
        string setNewGuidStr = null;
        int _len = System.Enum.GetNames(typeof(AnchorIndex)).GetLength(0);
        //Debug.LogWarning("设置新手引导去重列表中：" +newGuideList.Length + " " + _len);
        if (newGuideList.Length >= _len)
        {
            for (int i = 0; i < _len; i++)
            {
                setNewGuidStr = setNewGuidStr + newGuideList[i].ToString() + ";";
            }
            PlayerPrefs.SetString(playerKey, setNewGuidStr);
            Debug.Log(setNewGuidStr);
        }
    }

    public enum AnchorIndex
    {
        index_100 = 1,
        index_101 = 2,
        index_102 = 3,
        index_200 = 4,
        index_201 = 5,
        index_300 = 6,
        index_301 = 7,
        index_302 = 8,
        index_303 = 9,
        index_304 = 10,
        index_305 = 11,
        index_306 = 12,
        index_400 = 13,
        index_401 = 14,
        index_500 = 15,
        index_501 = 16,
        index_502 = 17,
        index_503 = 18,
        index_504 = 19,
        index_600 = 20,
        index_601 = 21,
        index_602 = 22,
        index_603 = 23,
        index_604 = 24,
        index_605 = 25,
        index_700 = 26,
        index_701 = 27,
        index_702 = 28,
        index_703 = 29,
        index_704 = 30,
        index_705 = 31,
        index_706 = 32,
        index_707 = 33,
        index_708 = 34,
        index_709 = 35,
        index_710 = 36,
        index_711 = 37,
        index_712 = 38,
        index_800 = 39,
        index_801 = 40,
        index_802 = 41,
        index_803 = 42,
        index_804 = 43,
        index_805 = 44,
        index_806 = 45,
        index_807 = 46,
        index_900 = 47,
        index_901 = 48,
        index_902 = 49,
        index_903 = 50,
        index_904 = 51,
        index_1000 = 52,
        index_1001 = 53,
        index_1002 = 54,
        index_1003 = 55,
        index_1004 = 56,
        index_1005 = 57,
        index_1006 = 58,
        index_1007 = 59,
        index_1008 = 60,
        index_1009 = 61,
        index_1100 = 62,
        index_1101 = 63,
        index_1102 = 64,
        index_1103 = 65,
        index_1104 = 66,
        index_1105 = 67,
        index_1106 = 68,
        index_1107 = 69,
        index_1108 = 70,
        index_1109 = 71,
        index_1200 = 72,
        index_1201 = 73,
        index_1202 = 74,
        index_1203 = 75,
        index_1204 = 76,
        index_1205 = 77,
        index_1206 = 78,
        index_1207 = 79,
        index_1300 = 80,
        index_1301 = 81,
        index_1302 = 82,
        index_1303 = 83,
        index_1304 = 84,
        index_1305 = 85,
        index_1306 = 86,
        index_1307 = 87,
        index_1308 = 88,
        index_1309 = 89,
        index_1310 = 90,
        index_1400 = 91,
        index_1401 = 92,
        index_1402 = 93,
        index_1403 = 94,
        index_1404 = 95,
        index_1405 = 96,
        index_1406 = 97,
        index_1407 = 98,
        index_1500 = 99,
        index_1501 = 100,
        index_1502 = 101,
        index_1503 = 102,
        index_1600 = 103,
        index_1601 = 104,
        index_1602 = 105,
        index_1603 = 106,
        index_1604 = 107,
        index_1605 = 108,
        index_1606 = 109,
        index_1607 = 110,
        index_1608 = 111,
        index_1609 = 112,
        index_1610 = 113,
        index_1700 = 114,
        index_1701 = 115,
        index_1702 = 116,
        index_1703 = 117,
        index_1704 = 118,
        index_1705 = 119,
        index_1706 = 120,
        index_1707 = 121,
        index_1800 = 122,
        index_1801 = 123,
        index_1802 = 124,
        index_1803 = 125,
        index_1804 = 126,
        index_1900 = 127,
        index_1901 = 128,
        index_1902 = 129,
        index_1903 = 130,
        index_1904 = 131,
        index_1905 = 132,
        index_2000 = 133,
        index_2001 = 134,
        index_2002 = 135,
        index_2003 = 136,
        index_2100 = 137,
        index_2101 = 138,
        index_2102 = 139,
        index_2103 = 140,
        index_2104 = 141,
        index_2105 = 142,
        index_2200 = 143,
        index_2201 = 144,
        index_2202 = 145,
        index_2203 = 146,
        index_2204 = 147,
        index_2205 = 148,
        index_2300 = 149,
        index_2301 = 150,
        index_2302 = 151,
        index_2303 = 152,
        index_2304 = 153,
        index_2305 = 154,
        index_2306 = 155,
        index_2400 = 156,
        index_2401 = 157,
        index_2402 = 158,
        index_2403 = 159,
        index_2404 = 160,
        index_2500 = 161,
        index_2501 = 162,
        index_2502 = 163,
        index_2503 = 164,
        index_2504 = 165,
        index_2600 = 166,
        index_2601 = 167,
        index_2602 = 168,
        index_2603 = 169,
        index_2604 = 170,
        index_2605 = 171,
        index_2700 = 172,
        index_2701 = 173,
        index_2702 = 174,
        index_2703 = 175,
        index_2704 = 176,
        index_2800 = 177,
        index_2801 = 178,
        index_2802 = 179,
        index_2803 = 180,
        index_2804 = 181,
        index_2805 = 182,
        index_2806 = 183,
        index_2807 = 184,
        index_2808 = 185,
        index_2809 = 186,
        index_2810 = 187,
        index_2811 = 188,
        index_2900 = 189,
        index_2901 = 190,
        index_2902 = 191,
        index_2903 = 192,
        index_3800 = 193,
        index_3801 = 194,
        index_3802 = 195,
        index_3803 = 196,
        index_3000 = 197,
        index_3001 = 198,
        index_3002 = 199,
        index_3003 = 200,
        index_3004 = 201,
        index_3005 = 202,
        index_3006 = 203,
        index_3007 = 204,
        index_3008 = 205,
        index_3009 = 206,
        index_3010 = 207,
        index_3011 = 208,
        index_3012 = 209,
        index_3013 = 210,
        index_3100 = 211,
        index_3101 = 212,
        index_3102 = 213,
        index_3103 = 214,
        index_3104 = 215,
        index_3200 = 216,
        index_3201 = 217,
        index_3202 = 218,
        index_3203 = 219,
        index_3204 = 220,
        index_3300 = 221,
        index_3301 = 222,
        index_3302 = 223,
        index_3303 = 224,
        index_3304 = 225,
        index_3305 = 226,
        index_3306 = 227,
        index_3307 = 228,
        index_3400 = 229,
        index_3401 = 230,
        index_3402 = 231,
        index_3403 = 232,
        index_3404 = 233,
        index_3405 = 234,
        index_3406 = 235,
        index_3407 = 236,
        index_3408 = 237,
        index_3409 = 238,
        index_3410 = 239,
        index_3411 = 240,
        index_3412 = 241,//index_3500 = 238,//index_3501 = 239,
        index_3502 = 242,
        index_3503 = 243,
        index_3600 = 244,
        index_3601 = 245,
        index_3602 = 246,
        index_3603 = 247,
        index_3604 = 248,
        index_3605 = 249,
        index_3606 = 250,
        index_3607 = 251,
        index_3608 = 252,
        index_3700 = 253,
        index_3701 = 254,
        index_3702 = 255,
        index_3703 = 256,
        index_3704 = 257,
        index_3705 = 258,
        index_3706 = 259,
        index_3900 = 260,
        index_3901 = 261,
        index_3902 = 262,
        index_3903 = 263,
        index_3904 = 264,
        index_3905 = 265,
        index_4000 = 266,
        index_4001 = 267,
        index_4002 = 268,
        index_4003 = 269,
        index_4004 = 270,
        index_4005 = 271,
        index_4006 = 272,
        index_4007 = 273
    }
    ///////////////////////////////////////////新手引导锚点（以上）//////////////////////////////////////
    public void StartGate(GateAnchorName AnchorName)
    {
        if (OpenNewGuideAnchor)
        {
            string test_str = "Test_";
            Dictionary<string, string> gateInfo = new Dictionary<string, string>();
            //gateInfo[test_str + "GateID"] = CurGateId;
            //gateInfo[test_str + "level"] = CharacterRecorder.instance.level.ToString();
            gateInfo[test_str + AnchorName.ToString()] = CurGateId + "$" + CharacterRecorder.instance.level.ToString() + "$" + AnchorName.ToString();
            GA.Event("GateAnchor", gateInfo);
        }
    }

    public enum GateAnchorName
    {
        gateFight,
        loading,
        startFight,
        finishFight,
        comeGatePeop,
        passNum,
        threeStarNum
    }

    public void CountLevel()
    {
        string test_str = "Test_";
        Dictionary<string, string> sendInfos = new Dictionary<string, string>();
        sendInfos[test_str + "lunaGem"] = CharacterRecorder.instance.lunaGem.ToString();
        sendInfos[test_str + "gold"] = CharacterRecorder.instance.gold.ToString();
        GA.Event("NewGuideAnchor", sendInfos);
    }

    public void UmengCountLevel(int _level)
    {
        Debug.Log("Umeng:" + _level);
        GA.SetUserLevel(_level);
    }

    public void UmengStartLevel(string id)
    {
        GA.StartLevel(id);
    }
    public void UmengFailLevel(string id)
    {
        GA.FailLevel(id);
    }
    public void UmengFinishLevel(string id)
    {
        GA.FinishLevel(id);
    }
    //////////////////////////////////////关卡统计（以上）//////////////////////////////////
    public enum Systems
    {
        英雄 = 1,
        英雄榜 = 2,
        装备 = 3,
        招募 = 4,
        背包 = 5,
        商城 = 6,
        任务 = 7,
        世界战斗 = 8,
        竞技场 = 9,
        王者之路 = 10,
        丛林冒险 = 11,
        夺宝奇兵 = 12,
        日常副本 = 13,
        赏金猎人 = 14,
        千锤百炼 = 15,
        花式军演 = 16,
        极限挑战 = 17,
        组队副本 = 18,
        边境走私 = 19,
        图鉴 = 20,
        战术 = 21,
        商店 = 22,
        情报 = 23,
        军团 = 24,
        军团副本 = 25,
        军团训练场 = 26,
        军团捐献 = 27,
        实验室 = 28,
        邮件 = 29,
        好友 = 30,
        聊天 = 31,
        黑市 = 32,
        升级系统 = 33,
        培养 = 34,
        升品 = 35,
        军衔 = 36,
        技能 = 37,
        强化 = 38,
        精炼 = 39,
        饰品晋级 = 40,
        秘宝 = 41,
        世界boss = 42,
        资源占领 = 43,
        世界事件 = 44,
        小贴士 = 45,
        精英世界事件 = 46,
        征服 = 47,
        座驾 = 48,
        神器 = 49,
        战场 = 50,
        小队技能 = 51,
        强化大师 = 52
    }

    public void CountSystem(Systems label)
    {
        if (OpenNewGuideAnchor)
        {
            GA.Event("系统模块", label.ToString());
        }
    }

    void CountSystemUseNum(Systems label)
    {
        if (OpenNewGuideAnchor)
        {
            Debug.LogError("哪个系统模块2：" + label.ToString());
            GA.Event("系统模块人数", label.ToString() + "使用人数");
        }
    }

    public void InitSystems()
    {
        string playerKey = "Systems";
        int _len = System.Enum.GetNames(typeof(Systems)).GetLength(0);

        bool isInitSystemList = false;
        if (PlayerPrefs.HasKey(playerKey))
        {
            SystemProcedure = PlayerPrefs.GetString(playerKey).Split(';');

            if (SystemProcedure.Length < _len + 1)
            {
                isInitSystemList = true;
            }
        }
        else
        {
            isInitSystemList = true;
        }

        if (isInitSystemList)
        {
            SystemProcedure = new string[_len + 1];
            for (int i = 0; i < _len; i++)
            {
                SystemProcedure[i] = ((int)NewGuidState.START).ToString();
            }
            SetSystems();
        }
    }

    void SetSystems()
    {
        string playerKey = "Systems";
        string setNewGuidStr = null;
        int _len = System.Enum.GetNames(typeof(Systems)).GetLength(0);
        if (SystemProcedure.Length >= _len)
        {
            for (int i = 0; i < _len; i++)
            {
                setNewGuidStr = setNewGuidStr + SystemProcedure[i] + ";";
            }
            PlayerPrefs.SetString(playerKey, setNewGuidStr);
        }
    }

    public void UpdateSystems(Systems _label)
    {
        if (OpenNewGuideAnchor)
        {
            if (SystemProcedure[(int)_label - 1] == ((int)NewGuidState.START).ToString())
            {
                CountSystemUseNum(_label);
                SystemProcedure[(int)_label - 1] = ((int)NewGuidState.END).ToString();
                SetSystems();
            }
        }
    }
    //////////////////////////////////////系统统计（以上）//////////////////////////////////

    public enum Activitys
    {
        签到 = 1,
        登录奖励 = 2,
        大放送 = 3,
        首充 = 4,
        活动 = 5,
        战力排行 = 6,
        Vip系统 = 7,
        月卡 = 8,
        十连送橙将 = 9,
        每日折扣 = 10,
        领取体力 = 11
    }

    public void CountActivitys(Activitys label)
    {
        if (OpenNewGuideAnchor)
        {
            Debug.LogError("哪个活动模块1：" + label.ToString());
            GA.Event("活动模块", label.ToString());
        }
    }

    void CountActivitysUseNum(Activitys label)
    {
        if (OpenNewGuideAnchor)
        {
            Debug.LogError("哪个活动模块2：" + label.ToString());
            GA.Event("活动模块人数", label.ToString() + "使用人数");
        }
    }

    public void InitActivitys()
    {
        string playerKey = "Activitys";
        int _len = System.Enum.GetNames(typeof(Activitys)).GetLength(0);


        bool isInitActiveList = false;
        if (PlayerPrefs.HasKey(playerKey))
        {
            ActivityProcedure = PlayerPrefs.GetString(playerKey).Split(';');

            if (ActivityProcedure.Length < _len + 1)
            {
                isInitActiveList = true;
            }
        }
        else
        {
            isInitActiveList = true;
        }

        if (isInitActiveList)
        {
            ActivityProcedure = new string[_len + 1];
            for (int i = 0; i < _len; i++)
            {
                ActivityProcedure[i] = ((int)NewGuidState.START).ToString();
            }
            SetActivitys();
        }
    }

    void SetActivitys()
    {
        string playerKey = "Activitys";
        string setNewGuidStr = null;
        int _len = System.Enum.GetNames(typeof(Activitys)).GetLength(0);
        if (ActivityProcedure.Length >= _len)
        {
            for (int i = 0; i < _len; i++)
            {
                setNewGuidStr = setNewGuidStr + ActivityProcedure[i] + ";";
            }
            PlayerPrefs.SetString(playerKey, setNewGuidStr);
        }
        else
        {
            Debug.LogError("设置活动模块失败！ " + ActivityProcedure.Length + " " + _len);
        }
    }

    public void UpdateActivitys(Activitys _label)
    {
        if (OpenNewGuideAnchor)
        {
            if (ActivityProcedure[(int)_label - 1] == ((int)NewGuidState.START).ToString())
            {
                CountActivitysUseNum(_label);
                ActivityProcedure[(int)_label - 1] = ((int)NewGuidState.END).ToString();
                SetActivitys();
            }
        }
    }

    /// <summary>
    /// 用于测试系统和活动人数统计
    /// </summary>
    void testCount()
    {
        string playerKey1 = "Activitys";
        string playerKey2 = "Systems";
        PlayerPrefs.DeleteKey(playerKey1);
        PlayerPrefs.DeleteKey(playerKey2);
    }

    void testNewGuideAnchor()
    {
        string playerKey = "Procedure";
        PlayerPrefs.DeleteKey(playerKey);
    }
}
