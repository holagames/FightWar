using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WoodsTheExpendables : MonoBehaviour
{
    public GameObject PopBuffInfoObj;
    public GameObject tansuoObj;
    public GameObject RightObj;
    public GameObject BuffWindow;
    public GameObject RulesWindow;
    public GameObject FightWindow;
    public GameObject TreasureWindow;
    public GameObject TreasureOpenWindow;
    public GameObject SuccessfulWindow;
    public GameObject GetReward;
    public GameObject TopContent;
    public GameObject TowerCamera;
    public GameObject RankWindow;
    public UILabel StarLabel;
    public UILabel IntegralLabel;
    public List<UILabel> BuffLabelList = new List<UILabel>();
    public GameObject BuffLabelObj;
    public GameObject WoodsTheExpendablesMapList;
    public List<GameObject> InstarObj = new List<GameObject>();
    public GameObject JinDuPosition;
    public Vector3 JinDuPositionStart;
    public GameObject ItemNumber;
    public GameObject WoodObject;
    public GameObject GroundObj;
    public GameObject HeroSelfObj;
    public bool isUpFloor = false;
    //-----------------------------数据
    public int CurFloor { get; private set; } //现在楼层
    public int CurStar { get; private set; } // 现在星数
    public int CurIntegral { get; private set; } // 现在积分
    public bool CurIsjump { get; private set; } //今日是否跳过
    public List<BuffData> BuffList = new List<BuffData>(); //Buff数据
    private int BuffWindowMaxMin = 40; //Buff窗口大小值
    private int BuffStartMaxMin = 75; // Buff窗口初始大小
    public int CurIntegralId { get; private set; } //当前积分购买的最大积分
    public float BuffPower = 0; //buff加成后战斗力
    //-----------------------------数据
    // Use this for initialization
    public int CanRewardID;
    public GameObject RedMessage;

    public int OpenIENum = 0;//枚举数，不同则关闭之前的协程
    public GameObject AutomaticbrushButton;
    public GameObject AutomaticbrushfieldWindow;
    void Start()
    {
        PictureCreater.instance.DestroyAllComponent();
        UpdateWindow(WoodsTheExpendablesWindowType.Right);
        JinDuPositionStart = JinDuPosition.transform.localPosition;
        if (UIEventListener.Get(GameObject.Find("BuffButton")).onPress == null)
        {
            UIEventListener.Get(GameObject.Find("BuffButton")).onPress += delegate(GameObject obj, bool bo)
            {
                if (bo)
                {
                    PopBuffInfoObj.SetActive(true);
                }
                else
                {
                    PopBuffInfoObj.SetActive(false);
                }
            };
        }
        if (UIEventListener.Get(GameObject.Find("RankingButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("RankingButton")).onClick += delegate(GameObject obj)
            {
                UpdateWindow(WoodsTheExpendablesWindowType.RankWindow);
            };
        }
        if (UIEventListener.Get(GameObject.Find("ShopButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("ShopButton")).onClick += delegate(GameObject obj)
            {
                GameObject.Find("WoodsTheExpendables").transform.Find("TopContent").gameObject.SetActive(false);
                UIManager.instance.OpenPanel("RankShopWindow", false);
                //NetworkHandler.instance.SendProcess("5105#10004;");  
            };
        }
        if (UIEventListener.Get(GameObject.Find("QuesButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("QuesButton")).onClick += delegate(GameObject obj)
            {
                UIManager.instance.OpenPanel("AnnouncementWindow", false);
                GameObject.Find("AnnouncementWindow").GetComponent<AnnouncementWindow>().SetTexture("Announcement2");
            };
        }
        if (UIEventListener.Get(GameObject.Find("RewardButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("RewardButton")).onClick += delegate(GameObject obj)
            {
                UpdateWindow(WoodsTheExpendablesWindowType.GetReward);
            };
        }
        if (UIEventListener.Get(GameObject.Find("RuleButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("RuleButton")).onClick += delegate(GameObject obj)
            {
                UpdateWindow(WoodsTheExpendablesWindowType.RulesWindow);
            };
        }

        if (GameObject.Find("WoodsTheExpendablesMapList") == null)
        {

            WoodObject = Instantiate(WoodsTheExpendablesMapList) as GameObject;
            //WoodObject.transform.position = new Vector3(100, 0, 0);
            WoodObject.transform.position = new Vector3(100, 13, 0);
            WoodObject.transform.localScale = Vector3.one;
            WoodObject.name = "WoodsTheExpendablesMapList";
            InstarObj.Add(WoodObject);
        }
        SceneTransformer.instance.ShowMainScene(false);

        GameObject tansuo = Instantiate(tansuoObj) as GameObject;
        tansuo.transform.position = new Vector3(100, 0, 0);
        tansuo.transform.localScale = new Vector3(2, 2, 2);
        tansuo.name = "tansuo";
        GroundObj = tansuo.transform.Find("RotationGround").gameObject;
        int num;
        if (isUpFloor == false)
        {
            num = (CurFloor + 1) % 6;
        }
        else
        {
            num = (CurFloor) % 6;
        }
        GroundObj.transform.Rotate(new Vector3(-60 * num, 0, 0));
        InstarObj.Add(tansuo);

        InstantiateHeroSelf(CharacterRecorder.instance.headIcon.ToString(), new Vector3(0, 13.1f, -7.6f));//人物模型
        HeroSelfObj = GameObject.Find(CharacterRecorder.instance.headIcon.ToString());


        LightMapAsset lightAsset = Resources.Load("LightMapAsset/lightMapAsset_999") as LightMapAsset;
        int count = lightAsset.lightmapFar.Length;
        LightmapData[] lightmapDatas = new LightmapData[count];
        for (int i = 0; i < count; i++)
        {
            LightmapData lightmapData = new LightmapData();
            lightmapData.lightmapFar = lightAsset.lightmapFar[i];
            lightmapData.lightmapNear = lightAsset.lightmapNear[i];
            lightmapDatas[i] = lightmapData;
        }
        LightmapSettings.lightmaps = lightmapDatas;
        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = 15.4f;
        RenderSettings.fogEndDistance = 139.6f;
        RenderSettings.fogDensity = 1;
        RenderSettings.fogColor = new Color32((byte)64, (byte)168, (byte)255, 255);
        RenderSettings.ambientLight = new Color32((byte)58, (byte)116, (byte)150, 255);

        Destroy(GameObject.Find("FightScene"));
        if (CharacterRecorder.instance.GuideID[12] == 7)
        {
            StartCoroutine(SceneTransformer.instance.NewbieGuide());
        }

        IsOpenAutoButton();
        UIEventListener.Get(AutomaticbrushButton).onClick = delegate(GameObject go)
        {
            if (AutomaticbrushButton.transform.Find("Checkmark").gameObject.activeSelf)
            {
                AutomaticbrushButton.transform.Find("Checkmark").gameObject.SetActive(false);
                PlayerPrefs.SetInt("Automaticbrushfield" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"),0);
            }
            else 
            {
                AutomaticbrushfieldWindow.SetActive(true);
            }
        };        
    }

    /// 生成主角模型
    private void InstantiateHeroSelf(string Id, Vector3 ObjPosition)
    {
        GameObject Obj1 = GameObject.Instantiate(transform.Find("HeroObj").gameObject) as GameObject;
        GameObject obj = GameObject.Instantiate(Resources.Load("Prefab/Role/" + Id, typeof(GameObject))) as GameObject;
        Obj1.SetActive(true);
        Obj1.transform.parent = GameObject.Find("tansuo").transform;
        Obj1.transform.localPosition = ObjPosition;
        Obj1.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);        
        Obj1.name = Id;
        obj.transform.parent = Obj1.transform;
        obj.name = Id;
        obj.transform.localScale = new Vector3(2f, 2f, 2f);
        HeroInfo hi = TextTranslator.instance.GetHeroInfoByHeroID(int.Parse(Id));
        Obj1.transform.localScale *= hi.heroScale;
        //obj.transform.localPosition = new Vector3(0f, -11.4f, -24f);
        obj.transform.localPosition = new Vector3(0f, 0f, 0f);
        if (CurFloor < 50)
        {
            obj.transform.localRotation = new Quaternion(0, 0, 0, 0);
            if (isUpFloor)
            {

                StartCoroutine(StopHeroMove(obj));
            }
        }
        else
        {
            obj.transform.localRotation = new Quaternion(0, 180, 0, 0);
            obj.GetComponent<Animator>().SetFloat("ft", 1);
        }
    }
    //停止动画
    IEnumerator StopHeroMove(GameObject obj)
    {
        yield return new WaitForSeconds(1f);
        obj.GetComponent<Animator>().SetFloat("ft", 0);
        yield return new WaitForSeconds(1.9f);
        obj.GetComponent<Animator>().SetFloat("ft", 2);
    }
    //更新界面显示
    public void UpdateWindow(WoodsTheExpendablesWindowType Type)
    {
        Debug.Log("哪个界面打开" + Type);
        RightObj.SetActive(false);
        BuffWindow.SetActive(false);
        RulesWindow.SetActive(false);
        FightWindow.SetActive(false);
        GetReward.SetActive(false);
        TreasureWindow.SetActive(false);
        TreasureOpenWindow.SetActive(false);
        RankWindow.SetActive(false);
        switch (Type)
        {
            case WoodsTheExpendablesWindowType.Right:
                RightObj.SetActive(true);
                TopContent.SetActive(true);
                GameObject.Find("TopContent").GetComponent<TopContent>().Reset();

                OpenIENum=1;
                StartCoroutine(AutomaticbrushOnEveryWindow(WoodsTheExpendablesWindowType.Right, SomeConditionCloseWindow.Nothing));
                break;


            case WoodsTheExpendablesWindowType.BuffWindow:
                BuffWindow.SetActive(true);
                TopContent.SetActive(false);

                OpenIENum=2;
                StartCoroutine(AutomaticbrushOnEveryWindow(WoodsTheExpendablesWindowType.BuffWindow, SomeConditionCloseWindow.Nothing));
                break;


            case WoodsTheExpendablesWindowType.FightWindow:
                FightWindow.SetActive(true);
                TopContent.SetActive(false);
                StartCoroutine(FightWindow.GetComponent<TowerFight>().DelayShowItem());
                FightWindow.GetComponent<TowerFight>().SetInfo();

                OpenIENum=3;
                StartCoroutine(AutomaticbrushOnEveryWindow(WoodsTheExpendablesWindowType.FightWindow, SomeConditionCloseWindow.Nothing));
                break;


            case WoodsTheExpendablesWindowType.GetReward:
                GetReward.SetActive(true);
                TopContent.SetActive(false);
                OpenIENum = 4;
                break;
            case WoodsTheExpendablesWindowType.RulesWindow:
                RulesWindow.SetActive(true);
                TopContent.SetActive(false);
                OpenIENum = 5;
                break;
            case WoodsTheExpendablesWindowType.TreasureWindow:
                TreasureWindow.SetActive(true);
                TreasureWindow.GetComponent<TowerTreasure>().SetInfo();
                TopContent.SetActive(false);
                break;
            case WoodsTheExpendablesWindowType.TreasureOpenWindow:
                TreasureOpenWindow.SetActive(true);
                TopContent.SetActive(false);
                break;
            case WoodsTheExpendablesWindowType.RankWindow:
                NetworkHandler.instance.SendProcess("1512#;");
                // RankWindow.SetActive(true);
                TopContent.SetActive(true);
                GameObject.Find("TopContent").GetComponent<TopContent>().Reset();
                OpenIENum = 8;
                break;
        }
    }

    //各个界面自动选择点击事件
    public IEnumerator AutomaticbrushOnEveryWindow(WoodsTheExpendablesWindowType Type, SomeConditionCloseWindow Condition)//枚举数，打开协程的编号，不同则关闭
    {
        Debug.Log("进入协程" + Type);
        if (PlayerPrefs.GetInt("Automaticbrushfield" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID")) == 1) 
        {
            yield return new WaitForSeconds(3f);
            switch (Type)
            {
                case WoodsTheExpendablesWindowType.Right:
                    if (OpenIENum == 1) 
                    {
                        WoodObject.GetComponent<WoodsTheExpendablesMapList>().NotGetMouseToChooseWindow();
                    }
                    break;

                case WoodsTheExpendablesWindowType.BuffWindow:
                    if (OpenIENum == 2)
                    {
                        GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().BuffWindow.GetComponent<TowerBuff>().EscOnClick();
                    }
                    break;

                case WoodsTheExpendablesWindowType.FightWindow:
                    if (OpenIENum == 3) 
                    {
                        //FightWindow.GetComponent<TowerFight>().NotClickButtonToFightWindow();
                        AutomaticbrushButton.SetActive(false);
                        PlayerPrefs.SetInt("Automaticbrushfield" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"),0);
                    }
                    break;

                case WoodsTheExpendablesWindowType.GetReward:
                    if (OpenIENum == 4)
                    {
                        
                    }
                    break;

                case WoodsTheExpendablesWindowType.RulesWindow:
                    if (OpenIENum == 5)
                    {

                    }
                    break;

                case WoodsTheExpendablesWindowType.TreasureWindow:
                    if (GameObject.Find("AdvanceWindow") != null) 
                    {
                        GameObject.Find("AdvanceWindow").GetComponent<AdvanceWindow>().ClickSureButton(true);
                    }
                    if (OpenIENum == 6)
                    {
                        if (Condition == SomeConditionCloseWindow.Nothing) 
                        {
                            TreasureWindow.GetComponent<TowerTreasure>().NotClickButtonCloseWindow("OpenButton");
                        }
                        else if (Condition==SomeConditionCloseWindow.Diamondproblem) 
                        {
                            TreasureWindow.GetComponent<TowerTreasure>().NotClickButtonCloseWindow("Button");
                        }
                        else if (Condition == SomeConditionCloseWindow.OpenTreasurefull) 
                        {
                            TreasureWindow.GetComponent<TowerTreasure>().NotClickButtonCloseWindow("Button");
                        }
                    }
                    break;

                case WoodsTheExpendablesWindowType.TreasureOpenWindow:
                    if (GameObject.Find("AdvanceWindow") != null)
                    {
                        GameObject.Find("AdvanceWindow").GetComponent<AdvanceWindow>().ClickSureButton(true);
                    }
                    if (OpenIENum == 7)
                    {
                        if (Condition == SomeConditionCloseWindow.Nothing)
                        {
                            TreasureOpenWindow.GetComponent<TowerTreasureOpen>().NotClickButtonCloseWindow("OpenButton");
                        }
                        else if (Condition == SomeConditionCloseWindow.Diamondproblem)
                        {
                            TreasureOpenWindow.GetComponent<TowerTreasureOpen>().NotClickButtonCloseWindow("Button");
                        }
                        else if (Condition == SomeConditionCloseWindow.OpenTreasurefull)
                        {
                            TreasureOpenWindow.GetComponent<TowerTreasureOpen>().NotClickButtonCloseWindow("Button");
                        }
                    }
                    break;

                case WoodsTheExpendablesWindowType.RankWindow:
                    if (OpenIENum == 8)
                    {

                    }
                    GameObject.Find("TopContent").GetComponent<TopContent>().Reset();
                    break;

                case WoodsTheExpendablesWindowType.AdvanceWindow:
                    if (OpenIENum == 9)
                    {
                        GameObject adv = GameObject.Find("AdvanceWindow");
                        if (adv != null) 
                        {
                            adv.GetComponent<AdvanceWindow>().ClickSureButton(true);
                            yield return new WaitForSeconds(2f);
                            if (TreasureWindow.activeSelf) 
                            {
                                TreasureWindow.GetComponent<TowerTreasure>().NotClickButtonCloseWindow("OpenButton");
                            }
                        }
                    }
                    break;
            }
        }
    }


    public void SetInfo(string RecvString)
    {
        BuffList.Clear();
        string[] dataSplit = RecvString.Split(';');
        CurFloor = int.Parse(dataSplit[0]);
        //Debug.LogError("CurFloor" + CurFloor);
        CurStar = int.Parse(dataSplit[1]);
        CurIntegral = int.Parse(dataSplit[2]);

        CurIsjump = int.Parse(dataSplit[6]) == 0 ? false : true;
        CurIntegralId = int.Parse(dataSplit[5]);

        //string[] SecSplit = dataSplit[3].Split('!');
        //for (int i = 0; i < SecSplit.Length - 1; i++) {
        //    string[] dataValueSplit = SecSplit[i].Split('$');
        //    BuffData buff = new BuffData();
        //    buff.Type = dataValueSplit[0];
        //    buff.Value = float.Parse(dataValueSplit[1]);
        //    BuffList.Add(buff);
        //}
        UpdateBuffList(dataSplit[3]);
        if (CurFloor >= 50)
        {
            SuccessfulWindow.SetActive(true);
        }
        UpdateWindowLabel();
    }

    public void MoveToFloor(int nowFloor)
    {
        if (CurFloor < 50)
        {
            if (isUpFloor)
            {
                StartCoroutine(ShowMoveRotation(nowFloor));
            }
            JinDuPosition.transform.localPosition -= new Vector3(0, (nowFloor + 1) * 10f, 0);
        }

    }
    //地图旋转
    IEnumerator ShowMoveRotation(int nowFloor)
    {
        //GameObject go = GameObject.Instantiate(Resources.Load("Prefab/Effect/XiaoShi_JueSe", typeof(GameObject))) as GameObject;
        //go.transform.localPosition = new Vector3(100f, 27f,0f);
        yield return new WaitForSeconds(1f);
        float num = 0;
        while (num < 60)
        {
            num += 1;
            yield return new WaitForSeconds(0.02f);
            if (num >= 60)
            {
                num = 60;
            }
            GroundObj.transform.Rotate(-1, 0, 0, Space.Self);
        }
        if (TextTranslator.instance.GetTowerByID(nowFloor + 1).Type == 1 && nowFloor + 1 <= 50)
        {
            ItemNumber.SetActive(true);
        }
        isUpFloor = false;
    }

    //---可以删除内容
    IEnumerator SlowMoveToFloor(int nowFloor, float newPosition)
    {
        yield return new WaitForSeconds(0.01f);

        if (TowerCamera == null)
        {
            TowerCamera = GameObject.Find("TowerCamera");
        }

        if (TowerCamera.transform.localPosition.x + 2 < newPosition)
        {
            TowerCamera.transform.localPosition += new Vector3(newPosition - 2, 0, 0);
            JinDuPosition.transform.localPosition -= new Vector3(0, (newPosition - 2) * 2.35f, 0);
        }

        for (int i = 1; i <= nowFloor; i++)
        {
            if (GameObject.Find("WoodObject").transform.Find(i.ToString()) != null)
            {
                GameObject go = GameObject.Instantiate(Resources.Load("Prefab/Effect/XiaoShi_JueSe", typeof(GameObject)), new Vector3(GameObject.Find("WoodObject").transform.Find(i.ToString()).position.x, 1, -10), Quaternion.identity) as GameObject;
            }
        }
        yield return new WaitForSeconds(0.3f);
        for (int i = 1; i <= nowFloor; i++)
        {
            if (GameObject.Find("WoodObject").transform.Find(i.ToString()) != null)
            {
                DestroyImmediate(GameObject.Find("WoodObject").transform.Find(i.ToString()).gameObject);
            }
            DestroyImmediate(GameObject.Find("JianTou" + i.ToString()));
        }


        while (TowerCamera.transform.localPosition.x != newPosition)
        {
            if (Mathf.Abs(TowerCamera.transform.localPosition.x - newPosition) < 0.2f)
            {
                TowerCamera.transform.localPosition = new Vector3(newPosition, TowerCamera.transform.localPosition.y, TowerCamera.transform.localPosition.z);
            }
            else
            {
                if (TowerCamera.transform.localPosition.x < newPosition)
                {
                    TowerCamera.transform.localPosition += new Vector3(0.2f, 0, 0);
                    JinDuPosition.transform.localPosition -= new Vector3(0, 0.47f, 0);
                }
                else if (TowerCamera.transform.localPosition.x > newPosition)
                {
                    TowerCamera.transform.localPosition -= new Vector3(0.2f, 0, 0);
                    JinDuPosition.transform.localPosition += new Vector3(0, 0.47f, 0);
                }
            }
            yield return new WaitForSeconds(0.01f);
        }
        yield return 0;
    }

    //积分奖励购买
    public void UpdateIntegralId(int Id)
    {
        CurIntegralId = Id;
        if (GameObject.Find("GetReward") != null)
        {
            GameObject.Find("GetReward").GetComponent<TowerGetReward>().updateItemlistShow(CurIntegralId);
        }
        ShowButtonMessage(Id);
    }
    //buffinfo按钮信息
    public void UpdateBuffList(string Data)
    {

        BuffList.Clear();
        string[] SecSplit = Data.Split('!');
        for (int i = 0; i < SecSplit.Length - 1; i++)
        {
            string[] dataValueSplit = SecSplit[i].Split('$');
            BuffData buff = new BuffData();
            buff.Type = dataValueSplit[0];
            if (dataValueSplit[0] == "7")
            {
                PlayerPrefs.SetInt("StormBuff" + "_" + PlayerPrefs.GetString("ServerID") + "_" + PlayerPrefs.GetInt("UserID"), 1);
            }
            else if (dataValueSplit[0] == "8")
            {
                PlayerPrefs.SetInt("ThreatBuff" + "_" + PlayerPrefs.GetString("ServerID") + "_" + PlayerPrefs.GetInt("UserID"), 1);
            }
            buff.Value = float.Parse(dataValueSplit[1]);
            BuffList.Add(buff);
        }
        ResPower();
    }
    private void UpdateWindowLabel()
    {
        StarLabel.text = CurStar.ToString();
        IntegralLabel.text = CurIntegral.ToString();
        UpdateBuffLabel();
    }
    /// <summary>
    /// 更新窗口显示的信息
    /// </summary>
    private void UpdateBuffLabel()
    {

        int CurBuffWindowMaxMin = BuffStartMaxMin;
        for (int i = 0; i < BuffList.Count; i++)
        {
            //BuffLabelList[i].text = BuffTypeName(int.Parse(BuffList[i].Type)) + "\n" + BuffValueName(int.Parse(BuffList[i].Type), BuffList[i].Value);
            BuffLabelList[int.Parse(BuffList[i].Type)-1].text = BuffTypeName(int.Parse(BuffList[i].Type)) + "\n[-][28DF5E]" + BuffValueName(int.Parse(BuffList[i].Type), BuffList[i].Value);
            //BuffLabelList[i].gameObject.SetActive(true);
            if (i > 0)
            {

                CurBuffWindowMaxMin += BuffWindowMaxMin;
            }

        }
        //BuffLabelObj.GetComponent<UISprite>().height = CurBuffWindowMaxMin;

    }
    public void UpdateDate(int CurFloor, int CurStar, int CurIntegral)
    {
        this.CurFloor = CurFloor;
        this.CurIntegral = CurIntegral;
        this.CurStar = CurStar;
        UpdateWindowLabel();
    }
    /// <summary>
    /// 返回buff名字
    /// </summary>
    /// <param name="Number"></param>
    /// <returns></returns>
    private string BuffTypeName(int Number)
    {
        string name = "";
        switch (Number)
        {
            case 1:
                name = "减伤";
                break;
            case 2:
                name = "暴击";
                break;
            case 3:
                name = "伤害";
                break;
            case 4:
                name = "抗暴";
                break;
            case 5:
                name = "闪避";
                break;
            case 6:
                name = "命中";
                break;
            case 7:
                name = "强攻";
                break;
            case 8:
                name = "威吓";
                break;
        }
        return name;
    }
    private string BuffValueName(int Number, float Value)
    {
        string name = "";
        switch (Number)
        {
            case 1:
                name = "+" + (Value * 100).ToString() + "%";
                break;
            case 2:
                name = "+" + (Value * 100).ToString() + "%";
                break;
            case 3:
                name = "+" + (Value * 100).ToString() + "%";
                break;
            case 4:
                name = "+" + (Value * 100).ToString() + "%";
                break;
            case 5:
                name = "+" + (Value * 100).ToString() + "%";
                break;
            case 6:
                name = "+" + (Value * 100).ToString() + "%";
                break;
            case 7:
                name = Value+"次";
                break;
            case 8:
                name = Value + "次";
                break;
        }
        return name;
    }
    //战斗力刷新
    public void ResPower()
    {
        for (int i = 0; i < BuffList.Count; i++)
        {
            switch (int.Parse(BuffList[i].Type))
            {
                case 1:// "减伤";
                    BuffPower += (BuffList[i].Value) * CharacterRecorder.instance.Fight * 0.012f;
                    break;
                case 2:  //"暴击";
                    BuffPower += (BuffList[i].Value) * CharacterRecorder.instance.Fight * 0.004f;
                    break;
                case 3: //"伤害";
                    BuffPower += (BuffList[i].Value) * CharacterRecorder.instance.Fight * 0.004f;
                    break;
                case 4: //"抗暴";
                    BuffPower += (BuffList[i].Value) * CharacterRecorder.instance.Fight * 0.003f;
                    break;
                case 5://"闪避";
                    BuffPower += (BuffList[i].Value) * CharacterRecorder.instance.Fight * 0.0060f;
                    break;
                case 6: //"命中";
                    BuffPower += (BuffList[i].Value) * CharacterRecorder.instance.Fight * 0.004f;
                    break;
                case 7: //"初始怒气";
                    break;
                case 8:  //"全体生命";
                    break;
            }
        }
        BuffPower += CharacterRecorder.instance.Fight;
        //Debug.LogError("sssssssss       " + BuffPower);
    }
    public void SetCurFloor(int Number)
    {
        CurFloor = Number;
    }
    public void OpenTreasureWindow()
    {
        UpdateWindow(WoodsTheExpendablesWindowType.TreasureWindow);
    }
    public void BackButtonClick()
    {
        if (GameObject.Find("RoleEquipInfoWindow") != null)
        {
            UIManager.instance.BackUI();
        }
        else
        {
            StartCoroutine(WaitForSencond());
        }
    }

    void OnDestroy()
    {
        for (int i = 0; i < InstarObj.Count; i++)
        {
            DestroyImmediate(InstarObj[i]);
        }
    }
    IEnumerator WaitForSencond()
    {
        yield return new WaitForSeconds(0.01f);
        for (int i = 0; i < InstarObj.Count; i++)
        {
            DestroyImmediate(InstarObj[i]);
        }
        InstarObj.Clear();
        PictureCreater.instance.DestroyAllComponent();
        UIManager.instance.BackUI();
    }
    public void UpdatePosition(float Positionx)
    {
        JinDuPosition.transform.localPosition = new Vector3(JinDuPosition.transform.localPosition.x, JinDuPositionStart.y + (Positionx * 2.6f), JinDuPosition.transform.localPosition.z);
    }

    //关卡数
    public void ShowNumber(int Number, GameObject obj)
    {
        if (isUpFloor == false)
        {
            ItemNumber.SetActive(true);
        }
        ItemNumber.transform.Find("Label").GetComponent<UILabel>().text = Number.ToString();
    }
    //奖励按钮红点是否开启
    public void ShowButtonMessage(int id)
    {
        if (CanRewardID > id)
        {
            RedMessage.SetActive(true);
        }
        else
        {
            RedMessage.SetActive(false);
        }
    }

    /// <summary>
    /// 会否出现一键按钮
    /// </summary>
    public void IsOpenAutoButton() 
    {
        //Debug.Log("CanMoveLayer " + CharacterRecorder.instance.CanMoveLayer);
        //Debug.Log("CurFloor " + CurFloor);
        //Debug.Log("NowFloor " + CharacterRecorder.instance.NowFloor);
        if (CharacterRecorder.instance.isSkip && CharacterRecorder.instance.Vip >= 7 && CharacterRecorder.instance.CanMoveLayer > CurFloor)
        {
            AutomaticbrushButton.SetActive(true);
            if (PlayerPrefs.GetInt("Automaticbrushfield" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID")) == 1)
            {
                AutomaticbrushButton.transform.Find("Checkmark").gameObject.SetActive(true);
            }
            else
            {
                AutomaticbrushButton.transform.Find("Checkmark").gameObject.SetActive(false);
            }
        }
        else 
        {
            AutomaticbrushButton.SetActive(false);
            PlayerPrefs.SetInt("Automaticbrushfield" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"),0);
        }
    }

}
public enum WoodsTheExpendablesWindowType
{
    Right, //右侧
    BuffWindow,//buff界面
    RulesWindow,//规则界面
    FightWindow,//战斗选择界面
    GetReward,  //积分奖励界面
    TreasureWindow, // 宝箱界面
    TreasureOpenWindow,// 宝箱开启界面
    RankWindow,//排行榜界面
    AdvanceWindow,//第一次宝箱奖励窗口
};


public enum SomeConditionCloseWindow
{
    Nothing,//无需其他条件判断，直接关闭窗口
    Diamondproblem,//宝箱钻石不足
    OpenTreasurefull,//宝箱次数足够了
};