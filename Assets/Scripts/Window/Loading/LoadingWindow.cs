using UnityEngine;
using System.Collections;

public class LoadingWindow : MonoBehaviour
{
    public UISlider loadSlider;
    public UISprite BackdropSprite;
    public GameObject SpriteAttack;
    public GameObject LabelPercent;
    public GameObject SpriteLogin;
    public GameObject IntroBoard;
    public UITexture loadingTexture;

    public UILabel NameLabel;
    public UILabel HeroName;
    public UILabel DesLabel;
    private string DesString;
    string TalkString;

    public float timeer;
    public bool IsClose = false;
    public bool IsAuto = false;

    float Timer = 0;
    int DivNum = 0;
    int LoadNum = 0;
    int SpritFrame = 1;
    int GCCount = 0;
    float DivTime = 2.5f;
    // Use this for initialization

    AsyncOperation async;
    public GameObject LeftButton;
    public GameObject RightButton;

    void OnEnable()
    {
        timeer = 0f;
        IsClose = false;
        IsAuto = true;
    }

    void Start()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        if (UIManager.instance.MapGateInfoLoading)
        {
            UIManager.instance.StartGate(UIManager.GateAnchorName.loading);
            UIManager.instance.MapGateInfoLoading = false;
        }
        UIEventListener.Get(SpriteLogin).onClick = delegate(GameObject go)
        {
            GameObject.Find("MainCamera").GetComponent<HuaWeiGameCenter>().StartSDK();
        };
        UIEventListener.Get(LeftButton).onClick = delegate(GameObject go)
        {
            ShowHero(Random.Range(0, 39),false);
        };
        UIEventListener.Get(RightButton).onClick = delegate(GameObject go)
        {
            ShowHero(Random.Range(0, 39),false);
        };
        if (CharacterRecorder.instance != null)
        {
            if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) >= 18)
            {
                if (CharacterRecorder.instance.GuideID.Count > 10)
                {
                    if (CharacterRecorder.instance.GuideID[9] == 12)
                    {
                        CharacterRecorder.instance.GuideID[9] += 1;
                    }
                }
            }
            if (Application.loadedLevelName != "Downloader")
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_101);
            }
            if (CharacterRecorder.instance.lastGateID == 10001)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_705);
            }
            else if (CharacterRecorder.instance.lastGateID == 10002)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1003);
            }
            else if (CharacterRecorder.instance.lastGateID == 10003)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1203);
            }
            else if (CharacterRecorder.instance.lastGateID == 10004)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1302);
            }
            else if (CharacterRecorder.instance.lastGateID == 10005)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1603);
            }
            else if (CharacterRecorder.instance.lastGateID == 10006)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1802);
            }
            else if (CharacterRecorder.instance.lastGateID == 10007)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1901);
            }
            else if (CharacterRecorder.instance.lastGateID == 10008)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2102);
            }
            else if (CharacterRecorder.instance.lastGateID == 10009)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2302);
            }
            else if (CharacterRecorder.instance.lastGateID == 10010)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2401);
            }
            else if (CharacterRecorder.instance.lastCreamGateID == 20001)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2502);
            }
            else if (CharacterRecorder.instance.lastGateID == 10011)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2602);
            }
            if (PictureCreater.instance.FightStyle == 6)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2808);
            }
            else if (PictureCreater.instance.FightStyle == 3)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_3007);
            }
            else if (PictureCreater.instance.FightStyle == 4)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_3305);
            }
            else if (PictureCreater.instance.FightStyle == 5)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_3404);
            }
            else if (PictureCreater.instance.FightStyle == 2)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_3607);
            }
        }

        int number = Random.Range(0, 39);
        int labelnumber = Random.Range(1, 22);

        if (SystemInfo.systemMemorySize < 1000)
        {
            number = Random.Range(0, 5);
        }

        if (!NetworkHandler.instance.IsFirst || NetworkHandler.instance.IsCreate)
        {
#if UNITY_EDITOR
#else
            DivTime = 12;
#endif
            labelnumber = 0;
            NetworkHandler.instance.IsFirst = true;
        }

        ShowText(labelnumber);
        ShowHero(number,true);

        //TextTranslator.instance.ReloadCount++;
        //Debug.LogError(TextTranslator.instance.ReloadCount);
        //if (TextTranslator.instance.ReloadCount > 2)
        //{
        //    SceneTransformer.instance.ShowMainScene(true);
        //    UIManager.instance.OpenSinglePanel("PreloadCommon", false);
        //    UIManager.instance.OpenSinglePanel("PreloadNormal", false);
        //    UIManager.instance.OpenSinglePanel("PreloadWood", false);
        //    UIManager.instance.OpenSinglePanel("PreloadSkill", false);
        //    UIManager.instance.OpenSinglePanel("PreloadItem", false);
        //    UIManager.instance.OpenSinglePanel("PreloadAvatar", false);
        //    UIManager.instance.OpenSinglePanel("PreloadCloud", false);

        //    TextTranslator.instance.ReloadCount = 1;
        //    Resources.UnloadUnusedAssets();
        //    System.GC.Collect();

        //    SceneTransformer.instance.ShowMainScene(true);
        //}

        TextTranslator.instance.ReloadCount++;
        //Debug.LogError(TextTranslator.instance.ReloadCount);
        //if (TextTranslator.instance.ReloadCount > 3)
        //{
        //    SceneTransformer.instance.ShowMainScene(true);
        //    TextTranslator.instance.ReloadCount = 1;
        //    //if (SystemInfo.systemMemorySize < 700)
        //    {
        //        Resources.UnloadUnusedAssets();
        //    }
        //    System.GC.Collect();
        //    SceneTransformer.instance.ShowMainScene(false);
        //}
    }

    void ShowText(int labelnumber)
    {
        switch (labelnumber)
        {
            case 0:
                DesLabel.text = "正在解压缩资源，请耐心等候，解压缩资源不消耗流量";
                break;
            case 1:
                DesLabel.text = "每天的中午12:00和晚上18:00、21:00都可以领取额外体力";
                break;
            case 2:
                DesLabel.text = "使用战术[d35818]手榴弹[-]可以炸掉关卡中的地雷";
                break;
            case 3:
                DesLabel.text = "[d35818]夺宝[-]中可随机获得大量珍稀资源，有多余精力时别忘了去夺宝";
                break;
            case 4:
                DesLabel.text = "所有已培养的英雄都能藉由[d35818]实验室[-]的改造大幅提升全队战力";
                break;
            case 5:
                DesLabel.text = "冲突里的[d35818]丛林冒险[-]走的越远奖励越好，还能找到大量金币";
                break;
            case 6:
                DesLabel.text = "[d35818]王者之路[-]产出的王者币能换取大量[d35818]传说英雄碎片[-]";
                break;
            case 7:
                DesLabel.text = "[d35818]边境走私[-]每次20分钟，运输工具越高级奖励[d35818]金条[-]也会大幅提升哦";
                break;
            case 8:
                DesLabel.text = "[d35818]战前布阵[-]是决定胜负的关键之一，实力相当的pk，合理的布阵能加大获胜机率";
                break;
            case 9:
                DesLabel.text = "[d35818]丛林冒险[-]可以体验纯手动战棋玩法，操作越好能通关越多";
                break;
            case 10:
                DesLabel.text = "战术[d35818]集火[-]可以快速消灭躲在后方的脆皮";
                break;
            case 11:
                DesLabel.text = "在头像设置里可以设定省电模式，也可以让游戏更流畅哦";
                break;
            case 12:
                DesLabel.text = "记得对残血的己方英雄使用战术[d35818]紧急治疗[-]";
                break;
            case 13:
                DesLabel.text = "[d35818]军团战[-]让你能在全服的瞩目下，展现以一挡百的威力";
                break;
            case 14:
                DesLabel.text = "当英雄装备上[d35818]核武器[-]后，技能效果能进一步提升，属性也能大幅提升";
                break;
            case 15:
                DesLabel.text = "[d35818]世界Boss[-]排名奖励有机会拿到[d35818]红色饰品碎片[-]哦";
                break;
            case 16:
                DesLabel.text = "[d35818]竟技场[-]为大神们点赞能获得钻石奖励";
                break;
            case 17:
                DesLabel.text = "普通关卡第一次过关不需要体力，即使没体力了也能放心推图哦";
                break;
            case 18:
                DesLabel.text = "[d35818]征服[-]里能召集好友和抓俘虏来提升资源产量";
                break;
            case 19:
                DesLabel.text = "通关[d35818]86关[-]开放[d35818]英雄重生[-]，妈妈再也不用担心我缺资源了";
                break;
            case 20:
                DesLabel.text = "[d35818]25[-]级开放[d35818]第6个英雄上阵[-]";
                break;
            case 21:
                DesLabel.text = "一般关卡战斗中点击英雄可随时调整走位及攻击目标";
                break;
            default:
                break;
        }
    }

    public void ShowHero(int number,bool isStart)
    {
        int[] LoadingPic = { 600141,600251,600271,600311,
                             600141,600251,600271,600311,
                             600141,600251,600271,600311,
                             600251,60016,60020,60013,
                             60017,60014,60022,60012,
                             60021,60019,600271,600311,
                             600141,60200,60024,60026,
                             60027,60028,60025,60029,
                             60023,60018,60015,60011,
                             60030,60031,60032
                            };
        loadingTexture.mainTexture = Resources.Load("Loading/" + LoadingPic[number].ToString()) as Texture;
        loadingTexture.MakePixelPerfect();
        DivNum = 0;
        IntroBoard.SetActive(true);
        if (isStart)
        {
            loadSlider.value = 0;
            LoadNum = 0;
            timeer = 0;
        }
        switch (number)
        {
            case 0:
                IntroBoard.SetActive(false);
                HeroName.text = "博斯科夫：";
                TalkString = "战斗民族的楷模，铁血无情的刽子手。退役后仍然怀念战场的岁月，对他来说放下枪就和死了没两样。";
                break;
            case 1:
                IntroBoard.SetActive(false);
                HeroName.text = "布兰德：";
                TalkString = "酷爱吃烤鸡，特别是浇上萨尔萨辣酱的墨西哥风味。其实他不太能吃辣，所以要借助焚烧来发泄。";
                break;
            case 2:
                IntroBoard.SetActive(false);
                HeroName.text = "瑞恩：";
                TalkString = "敢死队的老团员，专业吐槽三十年。虽然是慢性子，但是很容易激动。有着不为人知的复杂经历。";
                break;
            case 3:
                IntroBoard.SetActive(false);
                HeroName.text = "辛吉德：";
                TalkString = "年轻的时候曾混迹于多个雇佣兵部队，鲜有人知道他的过去，只知道惹过他的人都没好下场。";
                break;
            case 4:
                IntroBoard.SetActive(false);
                HeroName.text = "乔治·雷德：";
                TalkString = "凶狠毒辣的代名词，为达目的不择手段。不过士兵都愿意追随他，因为有他的指挥，无往而不利。";
                break;
            case 5:
                IntroBoard.SetActive(false);
                HeroName.text = "瓦西里：";
                TalkString = "典型的人来疯，平时沉默寡言，一旦有感兴趣的话题就会滔滔不绝。而且只要认真起来连他自己都怕。";
                break;
            case 6:
                IntroBoard.SetActive(false);
                HeroName.text = "霹雳游侠：";
                TalkString = "视车如命，绝对不会让人坐上自己的副驾驶。据说他的车会说话，也有人说是他自己在拟声。";
                break;
            case 7:
                IntroBoard.SetActive(false);
                HeroName.text = "T.N.T：";
                TalkString = "全名Theodore·Nicholas·Teddy，总是丢三落四，曾经失手炸掉了自家的火药库。";
                break;
            case 8:
                IntroBoard.SetActive(false);
                HeroName.text = "贝塔：";
                TalkString = "曾经是107连长，患有极度恐猫症，哪怕是看到云的形状像猫也受不了，所以总是躲在坦克里。";
                break;
            case 9:
                IntroBoard.SetActive(false);
                HeroName.text = "格罗布鲁斯：";
                TalkString = "没人敢得罪格罗布鲁斯，谁知道明天的早餐里会被放进什么奇怪的佐料，或者被他遍布世界的朋友人肉搜索。";
                break;
            case 10:
                IntroBoard.SetActive(false);
                HeroName.text = "教授：";
                TalkString = "经常乐呵呵的在危险的处境讲冷笑话，被男枪称呼为“辅助”，还总是请他去“插眼”。";
                break;
            case 11:
                IntroBoard.SetActive(false);
                HeroName.text = "布雷斯塔：";
                TalkString = "号称一旦狂奔就停不下来的闪电。因为他只发挥了“豹的速度”，做人总要留一手。";
                break;
            case 12:
                IntroBoard.SetActive(false);
                HeroName.text = "舒克：";
                TalkString = "敢死队的第一话唠，最爱吃花生米。让他当飞行员的理由就是，扔到天上就不那么吵了。";
                break;
            case 13:
                HeroName.text = "男枪：";
                TalkString = "越狱出来偶遇敢死队，蹭在队伍里主要是为了寻找陷害自己的仇人。顺便打发无聊的时间。";
                break;
            case 14:
                HeroName.text = "金泽丝：";
                TalkString = "天生的破坏狂人，仅仅因为有趣就会大肆破坏一座城市。为了更多的爆炸加入敢死队。";
                break;
            case 15:///
                HeroName.text = "古烈";
                TalkString = "原是美军上尉，在执行了数个卑劣的任务后对自己的信念产生动摇，开始追寻结束战争的方法。";
                break;
            case 16:
                HeroName.text = "雷电：";
                TalkString = "幼年时期就踏上战场的特种兵，被改造为生化忍者后逐渐觉醒了人性，与斯内克一同执行隐秘任务。";
                break;
            case 17:
                HeroName.text = "斯内克：";
                TalkString = "间谍组织FOXHOUND出身，传说中的英雄。精通各类枪械以及格斗术，但是厌恶杀戮和战争。纸箱爱好者。";
                break;
            case 18:
                HeroName.text = "里昂：";
                TalkString = "从第一天上班开始就霉运不断的打丧尸系列游戏主角。终于从丧尸危机中解脱却卷入了混乱的世界大战。";
                break;
            case 19:
                HeroName.text = "凯特丽娜：";
                TalkString = "和好友一起追捕金泽斯的途中巧遇敢死队，为了亲手给她带上手铐而一直等待机会。";
                break;
            case 20:
                HeroName.text = "艾达·王：";
                TalkString = "被谜团包围的性感美女。作为威斯格的间谍暗中监视里昂，却总在关键时刻给予援手。";
                break;
            case 21:
                HeroName.text = "威斯克：";
                TalkString = "总是躲在幕后的大头目，获得了超级病毒的变身能力，为了利益挑起各国之间的纷争。";
                break;
            case 22:
                IntroBoard.SetActive(false);
                HeroName.text = "捣蛋：";
                TalkString = "怯懦的捣蛋总是与人保持距离，以致于佣军团的很多队友都没见过他的容貌。";
                break;
            case 23:
                IntroBoard.SetActive(false);
                HeroName.text = "正恩：";
                TalkString = "曾经是朝鲜的军官，但是由于手段过于偏激而被辞退。为了继续打击罪恶选择加入雇佣兵部队。";
                break;
            case 24:
                IntroBoard.SetActive(false);
                HeroName.text = "二锤：";
                TalkString = "因为骨瘦如柴过不了军校体检，于是拼命学习军事知识，最终由于对炮击的专业知识被107连破格录取。";
                break;
            case 25:
                HeroName.text = "唐纳德：";
                TalkString = "美军首屈一指的王牌飞行员，号称“金色雄鹰”，不过他一上天就容易high，没人敢跟他组队。";
                break;
            case 26:
                HeroName.text = "劳拉：";
                TalkString = "对于冒险和考古有着非比寻常的热爱。为了创作最新的探险旅行书籍而加入了敢死队。";
                break;
            case 27:
                HeroName.text = "圣诞：";
                TalkString = "敢死队元老成员，英国特种部队退役老兵，作战技巧一流，善使飞刀。性格热血奔放，重情重义。";
                break;
            case 28:
                HeroName.text = "阴阳：";
                TalkString = "来自于东方的黄种人，曾服役于亚洲某神秘部队。虽然个头不大却有着惊人的战斗力。";
                break;
            case 29:
                HeroName.text = "茱莉亚：";
                TalkString = "自从加入了佣军团，团里生病的人越来越多，大家都排队等着茱莉亚给他们打上一针。";
                break;
            case 30:
                HeroName.text = "春丽：";
                TalkString = "有着“世界最强女性”称号的女孩，愿望是回老家当一个普通人，不过每次当不了多久又会跑去打架。";
                break;
            case 31:
                HeroName.text = "战壕：";
                TalkString = "巴尼的老对头和曾经的战友，擅长使用重型武器打击敌人，经常率领自己的佣军团协助巴尼作战。";
                break;
            case 32:
                HeroName.text = "巴尼：";
                TalkString = "敢死队的领袖，身经百战，手枪连发令敌人猝不及防。人格魅力十足，非常有人缘。";
                break;
            case 33:
                HeroName.text = "奥巴马：";
                TalkString = "曾经在暗影岛失去了自己心爱的女人，他发誓要用自己的圣枪净化所有邪恶亡灵。";
                break;
            case 34:
                HeroName.text = "阿卡琳：";
                TalkString = "暗影之拳并非一直生活在阴影之中，谁能想象到她的真面目居然是白衣天使……希望你们喜欢。";
                break;
            case 35:
                HeroName.text = "EZ：";
                TalkString = "不想当探险家的魔法师都不是好科学家，天才EZ热爱流浪探险，在敢死队找到了志同道合的同伴。";
                break;
            case 36:
                HeroName.text = "维嘉：";
                TalkString = "对于他来说，无论是在街霸还是在敢死队的世界，永远都脱离不了反派最终BOSS的设定。";
                break;
            case 37:
                HeroName.text = "暴走斯坦森：";
                TalkString = "战斗输出已经爆表了，无论是枪术、功夫还是肌肉都是爆炸式的存在。到底是谁惹怒了斯坦森？";
                break;
            case 38:
                HeroName.text = "麦琪：";
                TalkString = "天才计算机解码专家，聪明灵透，在加入敢死队后和队长巴尼之间产生了微妙的化学反应。";
                break;
            default:
                HeroName.text = "介绍：";
                TalkString = "还没有对应英雄的Loading介绍";
                break;
        }
        NameLabel.text = "";
    }

    IEnumerator UpdateName()
    {

        Debug.Log("左对齐显示一定距离的空白格");
        for (int i = 0; i < DesString.Length; i++)
        {
            DesLabel.text += DesString.Substring(i, 1);
            yield return new WaitForSeconds(0.04f);
        }

        //DesLabel.text += "\n\n";

        //for (int i = 0; i < DesString.Length; i++)
        //{
        //    DesLabel.text += DesString.Substring(i, 1);
        //    yield return new WaitForSeconds(0.1f);
        //}
    }
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > 0.05f && DivNum < TalkString.Length)
        {
            Timer -= 0.05f;

            NameLabel.text += TalkString.Substring(DivNum, 1);
            DivNum++;
            LoadNum++;
        }

        //if (NetworkHandler.instance.IsCreate)
        //{
        //    if (VersionChecker.instance.Count == 0 && VersionChecker.instance.TotalCount > 0)
        //    {
        //        if (LoadNum == 5)
        //        {
        //            GameObject go1 = Resources.Load("Prefab/Effect/jinx_skill") as GameObject;
        //            GameObject go2 = Resources.Load("Prefab/Effect/jinx_skill_02") as GameObject;
        //            GameObject go3 = Resources.Load("Prefab/Effect/jinx_skill_02_bf") as GameObject;

        //            GameObject go4 = Resources.Load("Prefab/Effect/Trench_skill") as GameObject;
        //            GameObject go5 = Resources.Load("Prefab/Effect/Trench_skill_pre") as GameObject;
        //        }
        //        else if (LoadNum == 10)
        //        {
        //            GameObject go6 = Resources.Load("Prefab/Scene/Map02") as GameObject;
        //            GameObject go7 = Resources.Load("LightMapAsset/lightMapAsset_20") as GameObject;
        //        }
        //        else if (LoadNum == 15)
        //        {
        //            PictureCreater.instance.CreateRole(60018, "Enemy", 0, Color.red, 50000, 50000, 1000, 107.1f, true, false, 1, 1.5f, 0, "", 0, 6854, 0, 0, 0, 0, 0, 1030, 2028, 0, 1, 0, 2, 2, 1000);
        //            PictureCreater.instance.CreateRole(60030, "Enemy", 0, Color.red, 98447, 98447, 1000, 100.0f, true, false, 1, 1.5f, 0, "", 0, 99999, 0, 0, 0, 0, 0, 1004, 2028, 0, 1, 0, 2, 2, 1000);
        //            PictureCreater.instance.CreateRole(60012, "Enemy", 0, Color.red, 120000, 120000, 1000, 108.9f, true, false, 1, 1.5f, 0, "", 0, 6454, 0, 0, 0, 0, 0, 1010, 2028, 0, 1, 0, 2, 2, 1000);
        //            PictureCreater.instance.CreateRole(60020, "Enemy", 0, Color.red, 120000, 120000, 1000, 108.9f, true, false, 1, 1.5f, 0, "", 0, 6454, 0, 0, 0, 0, 0, 1010, 2028, 0, 1, 0, 2, 2, 1000);

        //        }
        //        else if (LoadNum == 20)
        //        {
        //            PictureCreater.instance.CreateRole(65400, "Enemy", 0, Color.red, 60000, 60000, 1000, 6.9f, true, false, 1, 1.5f, 0, "", 0, 6088, 0, 0, 0, 0, 0, 1021, 2028, 0, 1, 0, 2, 2, 1000);
        //            PictureCreater.instance.CreateRole(65501, "Enemy", 0, Color.red, 60000, 60000, 1000, 6.9f, true, false, 1, 1.5f, 0, "", 0, 6088, 0, 0, 0, 0, 0, 1021, 2028, 0, 1, 0, 2, 2, 1000);
        //        }
        //        else if (LoadNum == 25)
        //        {
        //            PictureCreater.instance.CreateRole(60023, "Enemy", 0, Color.cyan, 50000, 50000, 1000, 108.0f, false, false, 1, 1.5f, 0, "", 0, 6933, 0, 2000, 0, 0, 0, 1025, 2028, 0, 1, 0, 1, 2, 1000);
        //            PictureCreater.instance.CreateRole(60029, "Enemy", 0, Color.cyan, 50000, 50000, 1000, 109.8f, false, false, 1, 1.5f, 0, "", 0, 6987, 0, 2000, 0, 0, 0, 1036, 2028, 0, 1, 0, 1, 2, 1000);
        //            PictureCreater.instance.CreateRole(60027, "Enemy", 0, Color.cyan, 50000, 50000, 1000, 105.8f, false, false, 1, 1.5f, 0, "", 0, 6254, 0, 2000, 0, 0, 0, 1035, 2028, 0, 1, 0, 1, 2, 1000);
        //            PictureCreater.instance.CreateRole(60032, "Enemy", 0, Color.cyan, 45000, 45000, 1000, 105.5f, false, false, 1, 1.5f, 0, "", 0, 6741, 0, 2000, 0, 0, 0, 1029, 2028, 0, 1, 0, 1, 2, 1000);
        //            PictureCreater.instance.CreateRole(60026, "Enemy", 0, Color.cyan, 50000, 50000, 1000, 106.3f, false, false, 1, 1.5f, 0, "", 0, 6344, 0, 2000, 0, 0, 0, 1034, 2028, 0, 1, 0, 1, 2, 1000);

        //        }
        //    }
        //}

        if (IsAuto)
        {
            if (loadSlider.value == 1)
            {
                if (IsClose)
                {
                    IsAuto = false;
                    Destroy(GameObject.Find("StartWindow"));
                    Destroy(GameObject.Find("LoadingWindow"));
                }
            }
            //else if (IsClose && TextTranslator.instance.ReloadCount != 0)
            //{
            //    timeer = DivTime;
            //    loadSlider.value = 1;
            //    BackdropSprite.fillAmount = 1;
            //}
            //else if (timeer / DivTime > 0.9f && !IsClose && TextTranslator.instance.ReloadCount == 0)
            //{
            //    TextTranslator.instance.ReloadCount++;
            //    loadSlider.value = 0;
            //    BackdropSprite.fillAmount = 0;
            //    timeer = 0;

            //    int number = Random.Range(0, 34);
            //    int labelnumber = Random.Range(1, 10);
            //    ShowText(labelnumber);
            //    ShowHero(number);
            //}
            else if (timeer / DivTime > 0.9f && !IsClose)
            {
                loadSlider.value = 0.9f;
                BackdropSprite.fillAmount = 0.9f;
            }
            else if (gameObject.name != "StartWindow" && (Application.loadedLevelName != "Downloader" || (VersionChecker.instance.Count == 0 && VersionChecker.instance.TotalCount > 0)))
            {
                timeer += Time.deltaTime;
                loadSlider.value = timeer / DivTime;
                BackdropSprite.fillAmount = timeer / DivTime;
            }

            if (loadSlider.value > 1)
            {
                loadSlider.value = 1;
                BackdropSprite.fillAmount = 1;
            }




            //if (IsGC)
            //{
            //    if (timeer / DivTime > 0.9f)
            //    {
            //        loadSlider.value = 1;
            //        BackdropSprite.fillAmount = 1;
            //        timeer = 0;
            //        GCCount++;
            //        switch(GCCount)
            //        {
            //            case 1:
            //                UIManager.instance.OpenSinglePanel("PreloadCommon", false);
            //                break;
            //            case 2:
            //                UIManager.instance.OpenSinglePanel("PreloadNormal", false);
            //                break;
            //            case 3:
            //                UIManager.instance.OpenSinglePanel("PreloadWood", false);
            //                break;
            //            case 4:
            //                UIManager.instance.OpenSinglePanel("PreloadSkill", false);
            //                break;
            //            case 5:
            //                UIManager.instance.OpenSinglePanel("PreloadItem", false);
            //                break;
            //            case 6:
            //                UIManager.instance.OpenSinglePanel("PreloadAvatar", false);
            //                break;
            //            case 7:
            //                UIManager.instance.OpenSinglePanel("PreloadCloud", false);
            //                break;
            //            case 8:
            //                Texture2D Preload = (Texture2D)Resources.Load("MapTexture/ddt");
            //                break;
            //            default:
            //                IsGC = false;
            //                break;
            //        }
            //    }
            //}
        }
    }
}
