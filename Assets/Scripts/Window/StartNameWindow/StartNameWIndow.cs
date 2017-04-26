using UnityEngine;
using System.Collections;
using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections.Generic;

public class StartNameWIndow : MonoBehaviour
{
    public UILabel DesLabel;
    public GameObject myTexture;
    public GameObject nameSureButton;

    public GameObject Scene;

    public GameObject ShaiZi;

    string PlayerName;
    string desString = "请给你的敢死队取个名字吧！";

    int heroCount = 0;
    List<Vector3> mVector3List = new List<Vector3>();
    List<PictureCreater.RolePicture> HeroTeam = new List<PictureCreater.RolePicture>();
    List<TeamMumberPosition> mTeamPosition = new List<TeamMumberPosition>();

    int PositionCount = 0;

    private Dictionary<int, Hero> ReccordHeroCardIdDic = new Dictionary<int, Hero>();

    GameObject zhantai;
    GameObject zhantai1;
    GameObject zhantai2;

    public bool IsMoveScence = false;

    public int position = 520000;

    public int moveSpeed = 2650;

    private string systemInfoString = string.Empty;
    public Camera RoleCamera;
    // Use this for initialization
    void Start()
    {
        //0--设备类型
        //1--设备的模型
        //2--操作系统的名称
        //3--处理器的名称
        //4--一个唯一的设备标识符 
        //5--当前显存的大小
        //7--显卡的名字
        //8--当前系统内存大小
        systemInfoString = string.Format("{0}${1}${2}${3}${4}${5}${6}${7}",
                                        SystemInfo.deviceType.ToString(),//设备类型
                                        SystemInfo.deviceModel,//设备的模型
                                        SystemInfo.operatingSystem,//操作系统的名称
                                        SystemInfo.processorType,//处理器的名称
                                        SystemInfo.deviceUniqueIdentifier,//一个唯一的设备标识符
                                        SystemInfo.graphicsMemorySize.ToString(),//当前显存的大小(M  为单位)
                                        SystemInfo.graphicsDeviceName,//显卡的名字
                                        SystemInfo.systemMemorySize.ToString()//当前系统内存大小(M  为单位)
                                        );

        StartCoroutine(PlayShaiZiAnimation());
        PlayAnimatorEffectBySort();
        PlayerName = RandomName();
        UpdateName(PlayerName);
        if (UIEventListener.Get(GameObject.Find("StartNameButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("StartNameButton")).onClick += delegate(GameObject obj)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_400);
                PlayerName = RandomName();
                UpdateName(PlayerName);
            };
        }
        if (UIEventListener.Get(nameSureButton).onClick == null)
        {
            UIEventListener.Get(nameSureButton).onClick += delegate(GameObject obj)
            {
                PlayerName = GameObject.Find("HeroNameLabel").GetComponent<UILabel>().text;
                int num = 0;
                foreach (var str in PlayerName)
                {
                    num++;
                }
                if (num <= 6)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_401);

                    NetworkHandler.instance.SendProcess("1002#" + ObscuredPrefs.GetString("Account") + ";" + PlayerName + ";" + systemInfoString + ";");

                }
                else
                {
                    UIManager.instance.OpenPromptWindow("名字上限六个字，请重新输入", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        AddScene();
        if (UIRootExtend.instance.isUiRootRatio != 1f)
        {
            RoleCamera.fieldOfView = 29f * 1.3f;
        }
        else
        {
            RoleCamera.fieldOfView = 29f;
        }

        Invoke("CreatHero", 1);
        //CreatHero();
        //Debug.LogError("EDEEEEEEEEEEAAAAAAAaE");
        //StartCoroutine(DelayShowHero());
        //AddStartNameScence();
    }


    Vector3 HeroPosition(int count)
    {
        Vector3 heroPosition = Vector3.zero;
        switch (count)
        {
            case 1:
                heroPosition = new Vector3(1750, 10820, 93860);
                break;
            case 2:
                heroPosition = new Vector3(886, 10820, 93860);
                break;
            case 3:
                heroPosition = new Vector3(22, 10820, 93860);
                break;
            case 4:
                heroPosition = new Vector3(-842, 10820, 93860);
                break;
            case 5:
                heroPosition = new Vector3(-1706, 10820, 93860);
                break;
            default:
                Debug.Log("角色的位置设置失败!");
                break;
        }
        return heroPosition;
    }

    public void CreatHero()
    {
        for (int i = 0; i < 5; i++)
        {
            if (heroCount < 5)
            {
                switch (heroCount)
                {
                    case 0:
                        GameObject clone = Resources.Load("Prefab/Role/60032") as GameObject;
                        GameObject go = Instantiate(clone) as GameObject;
                        go.name = "60032";
                        go.transform.parent = Scene.transform;
                        go.transform.localPosition = new Vector3(-25755, -5868, 88340);
                        go.transform.Rotate(new Vector3(0, 90, 0));
                        go.transform.localScale = new Vector3(19000, 19000, 19000);
                        go.transform.localScale *= 0.8f;
                        go.GetComponent<Animator>().SetFloat("ft", 0);
                        BiYan(go);
                        break;
                    case 1:
                        GameObject clone1 = Resources.Load("Prefab/Role/60027") as GameObject;
                        GameObject go1 = Instantiate(clone1) as GameObject;
                        go1.name = "60027";
                        go1.transform.parent = Scene.transform;
                        go1.transform.localPosition = new Vector3(-14787, -5868, 88340);
                        go1.transform.Rotate(new Vector3(0, 90, 0));
                        go1.transform.localScale = new Vector3(16000, 16000, 16000);
                        go1.transform.localScale *= 0.8f;
                        go1.GetComponent<Animator>().SetFloat("ft", 0);
                        BiYan(go1);
                        break;
                    case 2:
                        GameObject clone2 = Resources.Load("Prefab/Role/60029") as GameObject;
                        GameObject go2 = Instantiate(clone2) as GameObject;
                        go2.name = "60029";
                        go2.transform.parent = Scene.transform;
                        go2.transform.localPosition = new Vector3(-1461, -5868, 88340);
                        go2.transform.Rotate(new Vector3(0, 90, 0));
                        go2.transform.localScale = new Vector3(15500, 15500, 15500);
                        go2.transform.localScale *= 0.9f;
                        go2.GetComponent<Animator>().SetFloat("ft", 0);
                        BiYan(go2);
                        break;
                    case 3:
                        GameObject clone3 = Resources.Load("Prefab/Role/60031") as GameObject;
                        GameObject go3 = Instantiate(clone3) as GameObject;
                        go3.name = "60031";
                        go3.transform.parent = Scene.transform;
                        go3.transform.localPosition = new Vector3(9499, -5868, 88340);
                        go3.transform.Rotate(new Vector3(0, 90, 0));
                        go3.transform.localScale = new Vector3(16500, 16500, 16500);
                        go3.transform.localScale *= 0.8f;
                        go3.GetComponent<Animator>().SetFloat("ft", 0);
                        BiYan(go3);
                        break;
                    case 4:
                        GameObject clone4 = Resources.Load("Prefab/Role/60023") as GameObject;
                        GameObject go4 = Instantiate(clone4) as GameObject;
                        go4.name = "60023";
                        go4.transform.parent = Scene.transform;
                        go4.transform.localPosition = new Vector3(22561, -5868, 88340);
                        go4.transform.Rotate(new Vector3(0, 90, 0));
                        go4.transform.localScale = new Vector3(15500, 15500, 15500);
                        go4.transform.localScale *= 0.8f;
                        go4.GetComponent<Animator>().SetFloat("ft", 0);
                        BiYan(go4);
                        break;
                    default:
                        break;
                }
            }
            heroCount++;
            if (heroCount >= 5)
            {
                break;
            }
        }
    }

    void BiYan(GameObject heroObj)
    {
        foreach (Component c in heroObj.GetComponentsInChildren(typeof(MeshRenderer), true))
        {
            if (c.name.IndexOf("biyan") > -1)
            {
                // SetPicture[RoleIndex].RoleCloseEye = false;
                c.gameObject.SetActive(false);
                break;
            }
        }
    }
    /// <summary>
    /// 加载场景的3D模型
    /// </summary>
    public void AddScene()
    {
        zhantai = Instantiate(Resources.Load("Prefab/Scene/zhantai")) as GameObject;
        zhantai.transform.parent = Scene.transform;
        zhantai.transform.localPosition = new Vector3(-519672f, -157530, 705793);
        zhantai.transform.localScale = new Vector3(20000f, 20000f, 20000f);
        zhantai.name = "zhantai";

        zhantai1 = Instantiate(Resources.Load("Prefab/Scene/zhantai")) as GameObject;
        zhantai1.transform.parent = Scene.transform;
        zhantai1.transform.localPosition = new Vector3(0, -157530, 705793);
        zhantai1.transform.localScale = new Vector3(20000f, 20000f, 20000f);
        zhantai1.name = "Zhujiemian2";

        zhantai2 = Instantiate(Resources.Load("Prefab/Scene/zhantai")) as GameObject;
        zhantai2.transform.parent = Scene.transform;
        zhantai2.transform.localPosition = new Vector3(519672f, -157530, 705793);
        zhantai2.transform.localScale = new Vector3(20000f, 20000f, 20000f);
        zhantai2.name = "Zhujiemian3";

        RenderSettings.fog = false;
        LightMapAsset lightAsset = Resources.Load("LightMapAsset/lightMapAsset_132") as LightMapAsset;
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
        IsMoveScence = true;
        StartCoroutine("SetScenceTomove");

    }

    IEnumerator SetScenceTomove()
    {
        while (true)
        {
            while (IsMoveScence)
            {
                zhantai.transform.localPosition = new Vector3(zhantai.transform.localPosition.x - moveSpeed, -157530, 705793);
                zhantai1.transform.localPosition = new Vector3(zhantai1.transform.localPosition.x - moveSpeed, -157530, 705793);
                zhantai2.transform.localPosition = new Vector3(zhantai2.transform.localPosition.x - moveSpeed, -157530, 705793);

                if (zhantai.transform.localPosition.x <= -510000)//15705
                {
                    //zhujiemian1.transform.localPosition = new Vector3(15705, 0, 0);
                    if (zhantai1.transform.localPosition.x > 0)
                    {
                        zhantai.transform.localPosition = new Vector3(zhantai1.transform.localPosition.x + position, -157530, 705793);
                    }
                    else if (zhantai2.transform.localPosition.x > 0)
                    {
                        zhantai.transform.localPosition = new Vector3(zhantai2.transform.localPosition.x + position, -157530, 705793);
                    }
                    else
                    {//15750
                        zhantai.transform.localPosition = new Vector3(519672, 0, 0);
                    }
                }
                else if (zhantai1.transform.localPosition.x <= -510000)
                {
                    //zhujiemian2.transform.localPosition = new Vector3(15705, 0, 0);
                    if (zhantai.transform.localPosition.x > 0)
                    {
                        zhantai1.transform.localPosition = new Vector3(zhantai.transform.localPosition.x + position, -157530, 705793);
                    }
                    else if (zhantai2.transform.localPosition.x > 0)
                    {
                        zhantai1.transform.localPosition = new Vector3(zhantai2.transform.localPosition.x + position, -157530, 705793);
                    }
                    else
                    {
                        zhantai1.transform.localPosition = new Vector3(519672, 0, 0);
                    }
                }
                else if (zhantai2.transform.localPosition.x <= -510000)
                {
                    //zhujiemian3.transform.localPosition = new Vector3(15705, 0, 0);
                    if (zhantai.transform.localPosition.x > 0)
                    {
                        zhantai2.transform.localPosition = new Vector3(zhantai.transform.localPosition.x + position, -157530, 705793);
                    }
                    else if (zhantai1.transform.localPosition.x > 0)
                    {
                        zhantai2.transform.localPosition = new Vector3(zhantai1.transform.localPosition.x + position, -157530, 705793);
                    }
                    else
                    {
                        zhantai2.transform.localPosition = new Vector3(519672, -157530, 705793);
                    }
                    yield return new WaitForSeconds(0.01f);
                }
                yield return new WaitForSeconds(0.01f);
            }
        }
    }

    void PlayAnimatorEffectBySort()
    {
        DesLabel.text = "";
        nameSureButton.SetActive(false);
        myTexture.transform.localScale = new Vector3(0, 0, 0);
        TweenScale _TweenScale = myTexture.AddComponent<TweenScale>();
        _TweenScale.from = new Vector3(0, 0, 0);
        _TweenScale.to = new Vector3(1, 1, 1);
        _TweenScale.duration = 0.4f;
        _TweenScale.delay = 0.2f;
        StartCoroutine(UpdateDesLabel());
    }
    IEnumerator UpdateDesLabel()
    {
        nameSureButton.SetActive(true);
        yield return new WaitForSeconds(1.0f);

        for (int i = 0; i < desString.Length; i++)
        {
            DesLabel.text += desString.Substring(i, 1);
            yield return new WaitForSeconds(0.1f);
        }

        yield return new WaitForSeconds(0.5f);

        //DesLabel.text += "\n\n";

        //for (int i = 0; i < DesString.Length; i++)
        //{
        //    DesLabel.text += DesString.Substring(i, 1);
        //    yield return new WaitForSeconds(0.1f);
        //}
    }

    IEnumerator PlayShaiZiAnimation()
    {
        ShaiZi.GetComponent<UISpriteAnimation>().Play();
        yield return new WaitForSeconds(1.5f);
        StartCoroutine(PlayShaiZiAnimation());
    }

    /// <summary>
    /// 点击随机后调用这个修改显示名字
    /// </summary>
    public void UpdateName(string name)
    {
        if (GameObject.Find("HeroNameLabel") != null)
        {
            GameObject.Find("Input").GetComponent<UIInput>().value = name;
            GameObject.Find("HeroNameLabel").GetComponent<UILabel>().text = name;
        }
    }
    string RandomName()
    {
        string[] firstName = new string[]{ "暗夜","暴风","暴躁","悲伤","变异","逆转","博学","财迷","彩虹","晨曦","传奇","传说","传统","春野","大爱","大和","呆板","呆滞","单身"
            ,"刀剑","低调","巅峰","喋血","东方","动感","逗比","毒舌","独恋","独眼","繁华","非常","废柴","风暴","风云","敷衍","高大",
            "高冷","个性","功夫","孤独","古板","古怪","古墓","诡辩","黑骑","红尘","红人","华丽","怀旧","欢乐","幻想","皇室","皇族","辉煌","回忆"
            ,"活力","激动","疾风","记忆","寂寞","焦虑","狡猾","金发","谨慎","经典","惊天","精彩","精明","精英","狷狂","绝版","可爱","可怕","可疑",
            "浪漫","冷之","冷漠","恋之","灵魂","灵敏","琉璃","龙之","乱世","玫瑰","萌","梦幻","梦之","迷糊","迷恋","迷失","妙手","敏捷","慕容","暮光"
            ,"纳兰","南宫","难过","年迈","破晓","奇怪","旗木","千语","浅笑","强悍","倾城","热血","荣耀","柔情","睿智","杀戮","闪电","善变","善良","伤心",
            "神秘","神圣","失落","守护","天道","天使","甜蜜","调皮","完美","王牌","王者","温柔","无敌","无情","无忧","武装","西门","潇洒","笑傲"
            ,"斜眼","心机","感动","旋律","漩涡","寻梦","演绎","阳光","妖娆","妖艳","野心","疑惑","异能","樱花","优雅","幽默","傲慢"
            ,"月影","挚爱","专属","自由","左岸","强硬","重生","神奇","无赖","全能","红色","蓝色","绿色","急速","新","风流"
                                                  };
        string[] SecondName = new string[] {"灬丿","丶巛","丶灬","丨丶","灬E", "①","②","③","④","⑤","⑥","⑦","⑧","⑨",
              "勇士","兄弟","孤狼","狮子","老虎","爆炸","钻石","流星","飞龙","怪兽","战神","地主","快递","花花","火鸟","英雄"};
        string[] lastName = new string[]{ "组合","组","联盟","盟","战队","队","帮","派","联合","家族","族","一族","一家",
               "连","世家","力量","团体","会","营","学院","社团","堂","阁","社","集团","先锋"

                                                 };
        Random.seed = System.Environment.TickCount;
        string firstname = firstName[Random.Range(0, firstName.Length)];
        string secondName = SecondName[Random.Range(0, SecondName.Length)];
        string lastname = lastName[Random.Range(0, lastName.Length)];
        string name = firstname + secondName + lastname;
        return name;
    }

}
