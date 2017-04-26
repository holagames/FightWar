using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WorldBossFightWindow : MonoBehaviour
{
    public UILabel MyFightNumber;
    public UILabel MyRankNumber;
    public GameObject AwardItem;
    public GameObject RankItem;
    public GameObject SpriteBlood;
    public GameObject uiGrid1;
    public GameObject uiGrid2;
    public GameObject uiGrid3;
    public GameObject HandButton;
    public GameObject AutoLifeButton;
    public GameObject StartButton;
    public GameObject ResurgenceButton;
    public GameObject GlodButton;
    public GameObject DiamondButton;
    public GameObject AllDB;
    public GameObject GlodDB;
    public GameObject DiamondDB;
    public GameObject AutoLifeTime;
    public GameObject AutoLifeMessage;
    public UILabel GlodCost;
    public UILabel DiamondCost;
    public UISlider BossHP;

    public GameObject SureButton;
    public GameObject CancelButton;
    public GameObject CloseButton;

    public UISprite KongBai;
    public UIAtlas RoleAtlas;
    public UIAtlas ItemAtlas;
    private int TimeCD = 0;//复活倒计时
    private bool IsAutoFight = false;
    private bool IsAutoResur = false;


    public GameObject FightScence;
    private GameObject HeroParent;
    private GameObject MyHeroParent;
    private GameObject BossParent;

    private int glodSpireNum = 0;//鼓舞金额
    private int diamondSpireNum = 0;//鼓舞金额
    private float AllSpire = 0;//鼓舞总百分比
    private int ClearBossNum = 0;//复活次数
    private int ClearBossDiamond = 0;//复活需要的钻石

    private List<Vector3> HeroVecList = new List<Vector3>();
    //private GameObject[] HeroArr = new GameObject[10];
    //private int[] HeroIsStaty = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    //private List<GameObject> HeroParentList = new List<GameObject>();

    public GameObject[] HeroName = new GameObject[10];

    private int MyPositionNum = 0;
    private int BeforeBossBlood =0;//前一次boss血量
    public HUDText mText;

    private GameObject EffectYan;
    private GameObject EffectHuo;
    private GameObject EffectBaozha;
    private GameObject EffectBuff;


    void OnEnable()
    {
        GameObject.Find("MainCamera").GetComponent<Camera>().enabled = false;
        SetHeroPosition();
        SetFightBossScence();
        ClearBossNum = CharacterRecorder.instance.ClearBossNum;
        ClearBossDiamond = TextTranslator.instance.GetMarketByID(TextTranslator.instance.GetMarketIDByBuyCount(ClearBossNum + 1)).WorldBossClearCDDiamondCost;
        NetworkHandler.instance.SendProcess("6202#;");
        NetworkHandler.instance.SendProcess("6203#;");
        NetworkHandler.instance.SendProcess("6205#;");
        //NetworkHandler.instance.SendProcess("6208#;");
        NetworkHandler.instance.SendProcess("6212#;");
    }

    void Awake()//精准取得屏幕分辨率，非gameview
    {
        Debug.LogError("切换图片" + System.Convert.ToSingle(Screen.height) / Screen.width + "          " + System.Convert.ToSingle(600) / 900);
        if (System.Convert.ToSingle(Screen.height) / Screen.width >= System.Convert.ToSingle(600) / 900 - 0.02f && System.Convert.ToSingle(Screen.height) / Screen.width < System.Convert.ToSingle(1536) / 2048)
        {
            KongBai.width = 1225;
        }
        else if (System.Convert.ToSingle(Screen.height) / Screen.width >= System.Convert.ToSingle(1536) / 2048)
        {
            KongBai.width = 1422;
        }
        else
        {
            Debug.LogError("ss " + (System.Convert.ToSingle(Screen.width) / Screen.height) + "   " + (System.Convert.ToSingle(960) / 640));
            KongBai.width = (int)(1200 * (System.Convert.ToSingle(Screen.width) / Screen.height) / (System.Convert.ToSingle(960) / 640));
        }
        Debug.LogError("图片的长度：" + KongBai.width + "界面 " + Screen.width);
    }

    void Start()
    {
        //StartCoroutine(SendBossRankIE());
        //if (UIEventListener.Get(StartButton).onClick == null)
        {
            UIEventListener.Get(StartButton).onClick = delegate(GameObject go)
            {
                if (TimeCD == 0)
                {
                    StartButton.SetActive(false);
                    StartCoroutine(AttackToBoss());

                }
                else
                {
                    UIManager.instance.OpenPromptWindow("还在CD时间", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        //if (UIEventListener.Get(ResurgenceButton).onClick == null)
        {
            UIEventListener.Get(ResurgenceButton).onClick = delegate(GameObject go)
            {
                if (CharacterRecorder.instance.lunaGem < 30)
                {
                    UIManager.instance.OpenPromptWindow("钻石不足,无法复活", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    NetworkHandler.instance.SendProcess("6207#;");//清cd
                }
            };
        }
        //if (UIEventListener.Get(HandButton).onClick == null)
        {
            UIEventListener.Get(HandButton).onClick = delegate(GameObject go)
            {
                if (!IsAutoFight)
                {
                    if (CharacterRecorder.instance.Vip < 1&&CharacterRecorder.instance.level<25)
                    {
                        UIManager.instance.OpenPromptWindow("VIP1或者等级达到25级可以使用自动战斗", PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {                      
                        HandButton.GetComponent<UISprite>().spriteName = "12button";

                        IsAutoFight = true;
                        StartCoroutine(SetAutoFight());
                        PlayerPrefs.SetInt("IsOpenAutoFight", 1);
                    }
                }
                else
                {
                    IsAutoFight = false;
                    HandButton.GetComponent<UISprite>().spriteName = "11button";
                    PlayerPrefs.SetInt("IsOpenAutoFight", 0);
                }

            };
        }
        //if (UIEventListener.Get(AutoLifeButton).onClick == null)
        {
            UIEventListener.Get(AutoLifeButton).onClick = delegate(GameObject go)
            {
                if (!IsAutoResur)
                {
                    if (CharacterRecorder.instance.Vip < 9)
                    {
                        UIManager.instance.OpenPromptWindow("VIP9以上可以使用自动复活", PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        //IsAutoFight = true;
                        AutoLifeMessage.SetActive(true);
                        SetAutoLifeMessWindow();
                    }
                }
                else
                {
                    IsAutoResur = false;
                    AutoLifeButton.GetComponent<UISprite>().spriteName = "11button";
                }
            };
        }

        //if (UIEventListener.Get(GlodButton).onClick == null)
        {
            UIEventListener.Get(GlodButton).onClick = delegate(GameObject go)
            {
                if (CharacterRecorder.instance.gold < glodSpireNum)
                {
                    UIManager.instance.OpenPromptWindow("金币不足,无法鼓舞", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    NetworkHandler.instance.SendProcess("6204#1;");
                }
            };
        }
        //if (UIEventListener.Get(DiamondButton).onClick == null)
        {
            UIEventListener.Get(DiamondButton).onClick = delegate(GameObject go)
            {
                if (CharacterRecorder.instance.lunaGem < diamondSpireNum)
                {
                    UIManager.instance.OpenPromptWindow("钻石不足,无法鼓舞", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    NetworkHandler.instance.SendProcess("6204#2;");
                }
            };
        }
        if (CharacterRecorder.instance.Vip >= 9)
        {
            AutoLifeButton.transform.Find("Lock").gameObject.SetActive(false);
        }
        else
        {
            AutoLifeButton.transform.Find("Lock").gameObject.SetActive(true);
        }

        if (CharacterRecorder.instance.Vip >= 1||CharacterRecorder.instance.level>=25)
        {
            HandButton.transform.Find("Lock").gameObject.SetActive(false);
        }
        else
        {
            HandButton.transform.Find("Lock").gameObject.SetActive(true);
        }

        IsTimeToOpenBoss();
        ToSureIsOpenAutoFight();
    }

    private void ToSureIsOpenAutoFight() 
    {
        if (PlayerPrefs.GetInt("IsOpenAutoFight")==1)
        {
            if (CharacterRecorder.instance.Vip >= 1 || CharacterRecorder.instance.level >= 25)
            {
                HandButton.GetComponent<UISprite>().spriteName = "12button";
                IsAutoFight = true;
                StartCoroutine(SetAutoFight());
            }
            else 
            {
                IsAutoFight = false;
                HandButton.GetComponent<UISprite>().spriteName = "11button";
            }
        }
        else
        {
            IsAutoFight = false;
            HandButton.GetComponent<UISprite>().spriteName = "11button";
        }
    }

    IEnumerator AttackToBoss() //3秒空格攻击boss
    {
        CancelInvoke("UpdateTime");
        TimeCD = 15;
        for (int i = 0; i < 3; i++) 
        {
            PictureCreater.instance.ListRolePicture[MyPositionNum].RoleObject.transform.FindChild("Role").GetComponent<Animator>().Play("attack");
            FightMotion fm2 = TextTranslator.instance.fightMotionDic[CharacterRecorder.instance.headIcon * 10 + 1];
            PictureCreater.instance.PlayEffect(PictureCreater.instance.ListRolePicture, MyPositionNum, fm2);
            yield return new WaitForSeconds(0.5f);

            BossParent.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("attack");
            GameObject skill = Instantiate(Resources.Load("Prefab/Effect/chaojitanke_BOSS_attack")) as GameObject;
            skill.transform.position = new Vector3(0.5f, 0.9f, 2.77f);
            skill.transform.Rotate(new Vector3(0, -180f, 0));
            Destroy(skill, 5);
            yield return new WaitForSeconds(0.5f);
        }
        NetworkHandler.instance.SendProcess("6206#;");
        NetworkHandler.instance.SendProcess("6209#");
    }


    private void SetAutoLifeMessWindow() //自动复活窗口
    {
        if (UIEventListener.Get(SureButton).onClick == null)
        {
            UIEventListener.Get(SureButton).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.lunaGem < ClearBossDiamond)
                {
                    UIManager.instance.OpenPromptWindow("钻石不足,无法自动复活", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    IsAutoResur = true;
                    AutoLifeButton.GetComponent<UISprite>().spriteName = "12button";
                    StartCoroutine(SetAutoResurgence());
                    AutoLifeMessage.SetActive(false);
                }
            };
        }
        if (UIEventListener.Get(CancelButton).onClick == null)
        {
            UIEventListener.Get(CancelButton).onClick += delegate(GameObject go)
            {
                AutoLifeMessage.SetActive(false);
            };
        }
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                AutoLifeMessage.SetActive(false);
            };
        }
    }

    IEnumerator SendBossRankIE() //发送6209，取得自己和前10名排名
    {
        while (GameObject.Find("WorldBossFightWindow")!=null)
        {
            NetworkHandler.instance.SendProcess("6209#");
            yield return new WaitForSeconds(10f);
        }
    }
    public void GetBossblood(float _BossHP) //6202
    {
        BossHP.value = _BossHP / CharacterRecorder.instance.BossBlood;
        if (BeforeBossBlood != 0) 
        {
            mText.Add(_BossHP - BeforeBossBlood, Color.red, 0f);
            //BeforeBossBlood =(int)_BossHP;
        }
        BeforeBossBlood = (int)_BossHP;
        //if (_BossHP <= 0)
        //{
        //    BossParent.transform.GetChild(0).GetComponent<Animator>().Play("dead");
        //}
        if (BossHP.value > 0.4 && BossHP.value <= 0.8)
        {
            if (BossParent.transform.Find("EffectYan") == null)
            {
                EffectYan = Instantiate(Resources.Load("Prefab/Effect/ShiJieBoss_hurt01")) as GameObject;
                EffectYan.name = "EffectYan";
                EffectYan.transform.parent = BossParent.transform;
                EffectYan.transform.localPosition = new Vector3(0, 0, 0);
                EffectYan.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if (BossHP.value > 0 && BossHP.value <= 0.4)
        {
            if (BossParent.transform.Find("EffectYan") == null)
            {
                EffectYan = Instantiate(Resources.Load("Prefab/Effect/ShiJieBoss_hurt01")) as GameObject;
                EffectYan.name = "EffectYan";
                EffectYan.transform.parent = BossParent.transform;
                EffectYan.transform.localPosition = new Vector3(0, 0, 0);
                EffectYan.transform.localScale = new Vector3(1, 1, 1);
            }
            if (BossParent.transform.Find("EffectHuo") == null) 
            {
                EffectHuo = Instantiate(Resources.Load("Prefab/Effect/ShiJieBoss_hurt02")) as GameObject;
                EffectHuo.name = "EffectHuo";
                EffectHuo.transform.parent = BossParent.transform;
                EffectHuo.transform.localPosition = new Vector3(0, 0, 0);
                EffectHuo.transform.localScale = new Vector3(1, 1, 1);
            }
        }
        else if (_BossHP <= 0) 
        {
            BossParent.transform.GetChild(0).GetComponent<Animator>().Play("dead");
            EffectBaozha = Instantiate(Resources.Load("Prefab/Effect/ShiJieBoss_hurt03")) as GameObject;
            EffectBaozha.transform.parent = BossParent.transform;
            EffectBaozha.transform.localPosition = new Vector3(0, 0, 0);
            EffectBaozha.transform.localScale = new Vector3(1, 1, 1);          
        }
    }
    public void GetBossinSpire(int GlodSprieNum, float GlodSprie, int DiamondNum, float DiamondSprie)//取得世界鼓舞  6203
    {
        AllSpire = GlodSprie + DiamondSprie;
        AllDB.transform.Find("Label").GetComponent<UILabel>().text = AllSpire + "%";

        glodSpireNum = TextTranslator.instance.MarketDic[TextTranslator.instance.GetMarketIDByBuyCount(GlodSprieNum + 1)].WorldBossGoldCost;
        diamondSpireNum = TextTranslator.instance.MarketDic[TextTranslator.instance.GetMarketIDByBuyCount(DiamondNum + 1)].WorldBossDiamondCost;

        GlodCost.text = glodSpireNum.ToString();
        DiamondCost.text = diamondSpireNum.ToString();
    }
    public void SetBossinSpire(int GlodSprieNum, float GlodSprie, int DiamondNum, float DiamondSprie)//鼓舞结果 6204 
    {
        AllDB.SetActive(true);
        AllSpire = GlodSprie + DiamondSprie;
        AllDB.transform.Find("Label").GetComponent<UILabel>().text = AllSpire + "%";
        Debug.Log("金币 " + TextTranslator.instance.GetMarketIDByBuyCount(GlodSprieNum + 1));
        glodSpireNum = TextTranslator.instance.MarketDic[TextTranslator.instance.GetMarketIDByBuyCount(GlodSprieNum + 1)].WorldBossGoldCost;
        diamondSpireNum = TextTranslator.instance.MarketDic[TextTranslator.instance.GetMarketIDByBuyCount(DiamondNum + 1)].WorldBossDiamondCost;

        GlodCost.text = glodSpireNum.ToString();
        DiamondCost.text = diamondSpireNum.ToString();

        EffectBuff = Instantiate(Resources.Load("Prefab/Effect/JiaBuff_Gong")) as GameObject;       
        EffectBuff.transform.parent = MyHeroParent.transform;
        EffectBuff.transform.localPosition = new Vector3(0, 0, 0);
        EffectBuff.transform.localScale = new Vector3(1, 1, 1);
    }

    public void GetBossCD(int timecd, int killNum) //6205   初始进入界面getCD
    {
        this.TimeCD = timecd;
        MyFightNumber.text = killNum.ToString();
        if (timecd > 0)
        {
            StartButton.SetActive(false);
            ResurgenceButton.SetActive(true);
            AutoLifeTime.SetActive(true);
            AutoLifeTime.transform.Find("Time").GetComponent<UILabel>().text = timecd.ToString();
            AutoLifeTime.transform.Find("ClearBossDiamond").GetComponent<UILabel>().text = ClearBossDiamond.ToString();
            PictureCreater.instance.ListRolePicture[MyPositionNum].RoleObject.transform.FindChild("Role").GetComponent<Animator>().Play("dead");
            InvokeRepeating("UpdateTime", 0, 1.0f);
        }
        else
        {
            CancelInvoke("UpdateTime");
            ResurgenceButton.SetActive(false);
            AutoLifeTime.SetActive(false);
            StartButton.SetActive(true);
            PictureCreater.instance.ListRolePicture[MyPositionNum].RoleObject.transform.FindChild("Role").GetComponent<Animator>().Play("idle");
        }
    }
    public void BossFight(int timecd, int killNum) //6206 getCD
    {
        //ClearBossNum += 1;
        this.TimeCD = timecd;
        StartButton.SetActive(false);
        ResurgenceButton.SetActive(true);
        AutoLifeTime.SetActive(true);
        PictureCreater.instance.ListRolePicture[MyPositionNum].RoleObject.transform.FindChild("Role").GetComponent<Animator>().Play("dead");
        //ClearBossDiamond = TextTranslator.instance.GetMarketByID(TextTranslator.instance.GetMarketIDByBuyCount(ClearBossNum)).WorldBossClearCDDiamondCost;
        AutoLifeTime.transform.Find("Time").GetComponent<UILabel>().text = timecd.ToString();
        AutoLifeTime.transform.Find("ClearBossDiamond").GetComponent<UILabel>().text = ClearBossDiamond.ToString();
        MyFightNumber.text = killNum.ToString();

        CancelInvoke("UpdateTime");
        InvokeRepeating("UpdateTime", 0, 1.0f);
    }


    public void IsTimeToOpenBoss() //战斗开始挂机
    {
        if (CharacterRecorder.instance.IsTimeToOpen)
        {
            //StartCoroutine(BossAttack(gameObject.transform.Find("TakeBoss").gameObject));
            StartCoroutine(SendBossRankIE());
            if (IsAutoFight)
            {
                StartCoroutine(SetAutoFight());
            }

            if (IsAutoResur)
            {
                StartCoroutine(SetAutoResurgence());
            }

            UIEventListener.Get(StartButton).onClick = delegate(GameObject go)
            {
                if (TimeCD == 0)
                {
                    StartButton.SetActive(false);
                    StartCoroutine(AttackToBoss());

                }
                else
                {
                    UIManager.instance.OpenPromptWindow("还在CD时间", PromptWindow.PromptType.Hint, null, null);
                }
            };
            UIEventListener.Get(ResurgenceButton).onClick = delegate(GameObject go)
            {
                if (CharacterRecorder.instance.lunaGem < 30)
                {
                    UIManager.instance.OpenPromptWindow("钻石不足,无法复活", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    NetworkHandler.instance.SendProcess("6207#;");//清cd
                }
            };
        }
        else 
        {

            UIEventListener.Get(StartButton).onClick = delegate(GameObject go)
            {
                UIManager.instance.OpenPromptWindow("还未到开始时间", PromptWindow.PromptType.Hint, null, null);
            };
            UIEventListener.Get(ResurgenceButton).onClick = delegate(GameObject go)
            {
                UIManager.instance.OpenPromptWindow("还未到开始时间", PromptWindow.PromptType.Hint, null, null);
            };
        }
    }
   
    IEnumerator SetAutoFight() //自动战斗
    {
        Debug.Log("是否进入自动战斗");
        while (IsAutoFight && CharacterRecorder.instance.IsTimeToOpen)
        {
            Debug.Log("进入自动战斗");
            Debug.Log("时间Time "+TimeCD);
            yield return new WaitForSeconds(1f);
            if (TimeCD == 0)
            {
                ResurgenceButton.SetActive(false);
                AutoLifeTime.SetActive(false);
                AutoLifeTime.transform.Find("Time").GetComponent<UILabel>().text = TimeCD.ToString();
                CancelInvoke("UpdateTime");
                TimeCD = 15;
                yield return new WaitForSeconds(0.2f);
                StartButton.SetActive(false);
                for (int i = 0; i < 3; i++)
                {
                    PictureCreater.instance.ListRolePicture[MyPositionNum].RoleObject.transform.FindChild("Role").GetComponent<Animator>().Play("attack");
                    FightMotion fm2 = TextTranslator.instance.fightMotionDic[CharacterRecorder.instance.headIcon * 10 + 1];
                    PictureCreater.instance.PlayEffect(PictureCreater.instance.ListRolePicture, MyPositionNum, fm2);
                    yield return new WaitForSeconds(0.5f);

                    BossParent.transform.GetChild(0).gameObject.GetComponent<Animator>().Play("attack");
                    GameObject skill = Instantiate(Resources.Load("Prefab/Effect/chaojitanke_BOSS_attack")) as GameObject;
                    skill.transform.position = new Vector3(0.5f, 0.9f, 2.77f);
                    skill.transform.Rotate(new Vector3(0, -180f, 0));
                    Destroy(skill, 5);
                    yield return new WaitForSeconds(0.5f);
                }
                NetworkHandler.instance.SendProcess("6206#;");
                NetworkHandler.instance.SendProcess("6209#");
            }
        }
    }

    IEnumerator SetAutoResurgence() //自动复活
    {
        while (IsAutoResur && CharacterRecorder.instance.IsTimeToOpen)
        {
            if (TimeCD > 0 && CharacterRecorder.instance.lunaGem > ClearBossDiamond)
            {
                NetworkHandler.instance.SendProcess("6207#;");
            }
            else if (CharacterRecorder.instance.lunaGem < ClearBossDiamond)
            {
                IsAutoResur = false;
                ResurgenceButton.GetComponent<UISprite>().spriteName = "11button";
            }
            yield return new WaitForSeconds(1f);
        }
    }

    void UpdateTime()//刷新时间
    {
        if (TimeCD > 0)
        {
            AutoLifeTime.transform.Find("Time").GetComponent<UILabel>().text = TimeCD.ToString();
            TimeCD--;
        }
        else if (TimeCD == 0)
        {
            AutoLifeTime.transform.Find("Time").GetComponent<UILabel>().text = TimeCD.ToString();
            ResurgenceButton.SetActive(false);
            AutoLifeTime.SetActive(false);
            StartButton.SetActive(true);
            PictureCreater.instance.ListRolePicture[MyPositionNum].RoleObject.transform.FindChild("Role").GetComponent<Animator>().Play("idle");
            CancelInvoke("UpdateTime");
        }
    }

    public void BossClearbossCD() //6207   清cd
    {
        this.TimeCD = 0;
        ClearBossNum += 1;
        Debug.LogError("清CD次数  " + ClearBossNum);
        ClearBossDiamond = TextTranslator.instance.GetMarketByID(TextTranslator.instance.GetMarketIDByBuyCount(ClearBossNum + 1)).WorldBossClearCDDiamondCost;
        Debug.LogError("复活所需钻石  " + ClearBossDiamond);
        //AutoLifeTime.transform.Find("ClearBossDiamond").GetComponent<UILabel>().text = ClearBossDiamond.ToString();
        CancelInvoke("UpdateTime");
        ResurgenceButton.SetActive(false);
        AutoLifeTime.SetActive(false);
        StartButton.SetActive(true);
        PictureCreater.instance.ListRolePicture[MyPositionNum].RoleObject.transform.FindChild("Role").GetComponent<Animator>().Play("idle");
    }

    public void GetBossBloodAward(string BloodAward) //6208  
    {
        string[] dataSplit = BloodAward.Split(';');
        int num = int.Parse(dataSplit[0]);
        if (num > 60 && num <= 80)
        {
            dataSplit[0] = "80";
        }
        else if (num > 40 && num <= 60)
        {
            dataSplit[0] = "60";
        }
        else if (num > 20 && num <= 40)
        {
            dataSplit[0] = "40";
        }
        else if (num > 0 && num <= 20)
        {
            dataSplit[0] = "20";
        }
        else
        {
            dataSplit[0] = "0";
        }
        if (CharacterRecorder.instance.characterName != dataSplit[1])
        {
            GameObject go = NGUITools.AddChild(uiGrid3, SpriteBlood);
            go.SetActive(true);
            go.transform.Find("LabelBlood").GetComponent<UILabel>().text = dataSplit[1] + "\n额外获得" + dataSplit[0] + "%伤害奖励宝箱";//"额外获得" + dataSplit[0] + "%伤害奖励宝箱"
            uiGrid3.GetComponent<UIGrid>().Reposition();
        }
        else
        {
            SpriteBlood.SetActive(true);
            SpriteBlood.transform.Find("LabelBlood").GetComponent<UILabel>().text = "额外获得" + dataSplit[0] + "%伤害奖励宝箱";
            string[] trcSplit = dataSplit[2].Split('!');
            for (int i = 0; i < trcSplit.Length - 1; i++)
            {
                string[] prcSplit = trcSplit[i].Split('$');
                GameObject go = NGUITools.AddChild(uiGrid1, AwardItem);
                go.SetActive(true);
                SetColor(int.Parse(prcSplit[0]), int.Parse(prcSplit[1]), go);
            }
            uiGrid1.GetComponent<UIGrid>().Reposition();
        }
    }

    public void GetBossRank(string GetBossStr) //接受6209协议
    {
        string[] dataSplit = GetBossStr.Split(';');
        string[] trcSplit = dataSplit[2].Split('!');
        MyFightNumber.text = dataSplit[1];
        if (int.Parse(dataSplit[0]) > 10)
        {
            MyRankNumber.text = "未上榜";
        }
        else
        {
            MyRankNumber.text = dataSplit[0];
        }
        MyRankNumber.text = dataSplit[0];
        if (dataSplit[0] != "0")
        {
            int worldbossId = 0;
            for (int i = 0; i < TextTranslator.instance.WorldBossRewardList.size; i++)
            {
                if (TextTranslator.instance.WorldBossRewardList[i].WorldBossRank >= int.Parse(dataSplit[0]))
                {
                    worldbossId = TextTranslator.instance.WorldBossRewardList[i].WorldBossID;
                    break;
                }
            }
            Debug.Log("worldbossId" + worldbossId);
            for (int i = uiGrid1.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(uiGrid1.transform.GetChild(i).gameObject);
            }
            for (int i = 0; i < TextTranslator.instance.GetWorldBossRewardListID(worldbossId).RewardList.Count; i++)
            {
                GameObject go = NGUITools.AddChild(uiGrid1, AwardItem);//个人奖励 
                go.SetActive(true);
                int ItemCode = TextTranslator.instance.GetWorldBossRewardListID(worldbossId).RewardList[i].itemCode;
                int ItemNum = TextTranslator.instance.GetWorldBossRewardListID(worldbossId).RewardList[i].itemCount;
                SetColor(ItemCode, ItemNum, go);
                TextTranslator.instance.ItemDescription(go, ItemCode, ItemNum);
            }
            uiGrid1.GetComponent<UIGrid>().Reposition();
        }
        Destory();
        for (int i = 0; i < trcSplit.Length - 1; i++)
        {
            string[] prcSplit = trcSplit[i].Split('$');
            GameObject go = NGUITools.AddChild(uiGrid2, RankItem);
            go.SetActive(true);
            go.transform.Find("RankNumber").GetComponent<UILabel>().text = prcSplit[0];
            go.transform.Find("Name").GetComponent<UILabel>().text = prcSplit[1];
            go.transform.Find("HurtNumber").GetComponent<UILabel>().text = prcSplit[2];
        }
        uiGrid2.GetComponent<UIGrid>().Reposition();
    }

    public void JoinBoss(string Name, string HeadIcon, int _MyLocation)//6210 
    {
        if (CharacterRecorder.instance.characterName == Name)
        {
            MyPositionNum = PictureCreater.instance.CreateRole(CharacterRecorder.instance.headIcon, "Model", 18, Color.red, 0, 0, 1, 2f, false, false, 1, 1.5f, 0.2f, "Model", 0, 10, 10, 2, 2, 0, 0, 1001, 1, 1002, 1, 0, 1, 1, 0, "");
            PictureCreater.instance.ListRolePicture[MyPositionNum].RoleObject.transform.parent = MyHeroParent.transform;
            PictureCreater.instance.ListRolePicture[MyPositionNum].RoleObject.transform.localPosition = new Vector3(0, 0, 0);
            PictureCreater.instance.ListRolePicture[MyPositionNum].RoleObject.transform.FindChild("Role").transform.LookAt(BossParent.transform.localPosition);
            CharacterRecorder.instance.MyLocationInWorldBoss = _MyLocation;
        }
        else if (CharacterRecorder.instance.IsTimeToOpen)
        {
            for (int i = 0; i < 10; i++)
            {
                int num = 0;
                for (int j = 0; j < 10; j++)
                {
                    if (HeroName[j].activeSelf == true && HeroName[j].transform.Find("LabelName").GetComponent<UILabel>().text == Name)
                    {
                        num = 1;
                    }
                }
                if (HeroName[i].activeSelf == false && num == 0)//HeroArr[i] == null
                {
                    HeroName[i].SetActive(true);
                    HeroName[i].transform.Find("LabelName").GetComponent<UILabel>().text = Name;
                    StartCoroutine(HeroFriendsToFight(Name, int.Parse(HeadIcon), i));
                    break;
                }
            }
        }
    }

    IEnumerator HeroFriendsToFight(string Name, int HeadIcon, int num)
    {
        int a = PictureCreater.instance.CreateRole(HeadIcon, "Model", 18, Color.red, 0, 0, 1, 2f, false, false, 1, 1.5f, 0.2f, "Model", 0, 10, 10, 2, 2, 0, 0, 1001, 1, 1002, 1, 0, 1, 1, 0, "");
        GameObject go = PictureCreater.instance.ListRolePicture[a].RoleObject;
        PictureCreater.instance.ListRolePicture[a].RoleObject.name = Name;
        PictureCreater.instance.ListRolePicture[a].RoleObject.transform.parent = HeroParent.transform;
        PictureCreater.instance.ListRolePicture[a].RoleObject.transform.localPosition = HeroVecList[num];
        PictureCreater.instance.ListRolePicture[a].RoleObject.transform.FindChild("Role").transform.LookAt(BossParent.transform.localPosition);
        while (HeroName[num].transform.Find("LabelName").GetComponent<UILabel>().text == Name)
        {
            if (go != null)
            {
                a = PictureCreater.instance.GetIndexByName(PictureCreater.instance.ListRolePicture, Name);
                go.transform.FindChild("Role").GetComponent<Animator>().Play("attack");
                FightMotion fm = TextTranslator.instance.fightMotionDic[HeadIcon * 10 + 1];
                PictureCreater.instance.PlayEffect(PictureCreater.instance.ListRolePicture, a, fm);
                yield return new WaitForSeconds(UnityEngine.Random.Range(4, 8));
            }
            else 
            {
                break;
            }
        }
    }
    public void LeaveBoss(string Name, string HeadIcon) //6211 某人离开
    {
        for (int i = 0; i < 10; i++)
        {
            if (HeroName[i].transform.Find("LabelName").GetComponent<UILabel>().text == Name)
            {
                HeroName[i].SetActive(false);
                for (int j = 0; j < PictureCreater.instance.ListRolePicture.Count; j++)
                {
                    if (PictureCreater.instance.ListRolePicture[j].RoleObject.name == Name)
                    {
                        PictureCreater.instance.DestroyComponentByName(Name);

                        break;
                    }
                }
                break;
            }
        }
        //if (CharacterRecorder.instance.characterName == Name)
        //{
        //    GameObject.Find("MainCamera").GetComponent<Camera>().enabled = true;
        //    while (GameObject.Find("FightScence") != null)
        //    {
        //        DestroyImmediate(GameObject.Find("FightScence"));
        //    }
        //    while (GameObject.Find("WorldBossFightWindow") != null)
        //    {
        //        DestroyImmediate(GameObject.Find("WorldBossFightWindow"));
        //    }
        //    UIManager.instance.OpenPanel("MainWindow", true);
        //}
        //else
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        if (HeroName[i].transform.Find("LabelName").GetComponent<UILabel>().text == Name)
        //        {
        //            HeroName[i].SetActive(false);
        //            for (int j = 0; j < PictureCreater.instance.ListRolePicture.Count; j++)
        //            {
        //                if (PictureCreater.instance.ListRolePicture[j].RoleObject.name == Name)
        //                {
        //                    PictureCreater.instance.DestroyComponentByName(Name);

        //                    break;
        //                }
        //            }
        //            break;
        //        }
        //    }
        //}
    }


    public void JoinBossPlayer(string Recving) //6212
    {
        string[] dataSplit = Recving.Split(';');
        string[] trcSplit = dataSplit[0].Split('!');
        for (int i = 0; i < trcSplit.Length - 1; i++) 
        {
            string[] prcSplit = trcSplit[i].Split('$');
            int num=0;
            if (prcSplit[0] != CharacterRecorder.instance.characterName && num==0)
            {
                JoinBoss(prcSplit[0], prcSplit[1], 0);
            }
        }
    }


    void Destory()
    {
        for (int i = uiGrid2.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGrid2.transform.GetChild(i).gameObject);
        }
    }
    void SetColor(int ItemCode, int ItemNum, GameObject go)
    {
        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
        go.GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
        if (ItemCode.ToString()[0] == '6')
        {
            go.transform.Find("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
            go.transform.Find("Icon").GetComponent<UISprite>().spriteName = ItemCode.ToString();
            //go.transform.Find("Label").GetComponent<UILabel>().text = ItemNum.ToString();
            go.transform.Find("Fragment").gameObject.SetActive(false);
        }
        else if (ItemCode == 70000 || ItemCode == 79999)
        {
            go.transform.Find("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.Find("Icon").GetComponent<UISprite>().spriteName = ItemCode.ToString();
            //go.transform.Find("Label").GetComponent<UILabel>().text = ItemNum.ToString();
            go.transform.Find("Fragment").gameObject.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '7' && ItemCode > 70000)
        {
            go.transform.Find("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
            go.transform.Find("Icon").GetComponent<UISprite>().spriteName = (ItemCode - 10000).ToString();
            //go.transform.Find("Label").GetComponent<UILabel>().text = ItemNum.ToString();
            go.transform.Find("Fragment").gameObject.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '8')
        {
            go.transform.Find("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.Find("Icon").GetComponent<UISprite>().spriteName = (ItemCode - 30000).ToString();
            //go.transform.Find("Label").GetComponent<UILabel>().text = ItemNum.ToString();
            go.transform.Find("Fragment").gameObject.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '2')
        {
            go.transform.Find("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.Find("Icon").GetComponent<UISprite>().spriteName = _ItemInfo.picID.ToString();
            //go.transform.Find("Label").GetComponent<UILabel>().text = ItemNum.ToString();
            go.transform.Find("Fragment").gameObject.SetActive(false);
        }
        else
        {
            go.transform.Find("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.Find("Icon").GetComponent<UISprite>().spriteName = ItemCode.ToString();
            //go.transform.Find("Label").GetComponent<UILabel>().text = ItemNum.ToString();
            go.transform.Find("Fragment").gameObject.SetActive(false);
        }
        if (ItemNum >= 10000)
        {
            int a = ItemNum /10000;
            int b = ItemNum % 10000 / 1000;
            go.transform.Find("Label").GetComponent<UILabel>().text = a.ToString()+"."+b.ToString()+"W";
        }
        else
        {
            go.transform.Find("Label").GetComponent<UILabel>().text = ItemNum.ToString();
        }
    }

    private void SetBossPosition()
    {
        GameObject obj = GameObject.Instantiate(Resources.Load("Prefab/Role/" + TextTranslator.instance.GetWorldBossByID(CharacterRecorder.instance.BossLevel).Icon, typeof(GameObject))) as GameObject;
        obj.transform.parent = BossParent.transform;
        obj.transform.localScale = new Vector3(3f, 3f, 3f);
        obj.transform.localPosition = new Vector3(22f, 1f, -13f);
        //obj.transform.LookAt(BossParent.transform.localPosition);
    }
    private void SetHeroPosition() //其他战队位置
    {
        HeroVecList.Add(new Vector3(29.1f, 0f, 1.7f));
        HeroVecList.Add(new Vector3(28f, 0f, 0.97f));
        HeroVecList.Add(new Vector3(26.9f, 0f, 0.46f));
        HeroVecList.Add(new Vector3(25.6f, 0f, 0.15f));
        HeroVecList.Add(new Vector3(24.3f, 0f, 0f));
        HeroVecList.Add(new Vector3(21.7f, 0f, 0f));
        HeroVecList.Add(new Vector3(20.4f, 0f, 0.15f));
        HeroVecList.Add(new Vector3(19.1f, 0f, 0.46f));
        HeroVecList.Add(new Vector3(18f, 0f, 0.97f));
        HeroVecList.Add(new Vector3(16.9f, 0f, 1.7f));
    }
    public void SetFightBossScence() //加载场景
    {
        SceneTransformer.instance.ShowMainScene(false);
        {
            GameObject tansuo = Instantiate(FightScence) as GameObject;
            tansuo.transform.position = new Vector3(0, 0, 0);
            tansuo.transform.localScale = new Vector3(1, 1, 1);
            tansuo.name = "FightScence";
            HeroParent = tansuo.transform.Find("HeroParent").gameObject;
            MyHeroParent = tansuo.transform.Find("MyHeroParent").gameObject;
            BossParent = tansuo.transform.Find("BossParent").gameObject;
            BossParent.transform.Rotate(new Vector3(0, 0, 0));
        }
        LightMapAsset lightAsset = Resources.Load("LightMapAsset/lightMapAsset_20") as LightMapAsset;
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
        RenderSettings.fog = false;
        GameObject obj = GameObject.Instantiate(Resources.Load("Prefab/Role/" + 65402, typeof(GameObject))) as GameObject;
        //GameObject obj = GameObject.Instantiate(Resources.Load("Prefab/Role/" + TextTranslator.instance.GetWorldBossByID(CharacterRecorder.instance.BossLevel).Icon, typeof(GameObject))) as GameObject;
        obj.transform.parent = BossParent.transform;
        obj.transform.localScale = new Vector3(3f, 3f, 3f);
        obj.transform.localPosition = new Vector3(0f, 0f, 0f);
        obj.transform.Rotate(new Vector3(0, 180, 0));
        obj.name = "TakeBoss";
        StartCoroutine(BossAttack(obj));
        //if (CharacterRecorder.instance.IsTimeToOpen) 
        //{
        //    StartCoroutine(BossAttack(obj));
        //}
    }

    IEnumerator BossAttack(GameObject obj) 
    {
        while (BossHP.value > 0 && CharacterRecorder.instance.IsTimeToOpen)
        {
            obj.GetComponent<Animator>().Play("attack");
            GameObject skill = Instantiate(Resources.Load("Prefab/Effect/chaojitanke_BOSS_attack")) as GameObject;
            skill.transform.position = new Vector3(0.5f, 0.9f, 2.77f);
            skill.transform.Rotate(new Vector3(0, -180f, 0));
            //EffectYan.transform.localScale = new Vector3(1, 1, 1);
            Destroy(skill, 5);
            yield return new WaitForSeconds(5f);
        }
    }
    public void LiveWorldBossFightWindow() //boss死亡离开此场景,进入结算查看界面
    {
        GameObject.Find("MainCamera").GetComponent<Camera>().enabled = true;
        PictureCreater.instance.DestroyAllComponent();
        UIManager.instance.BackUI();
        while (GameObject.Find("FightScence") != null)
        {
            DestroyImmediate(GameObject.Find("FightScence"));
        }
        while (GameObject.Find("WorldBossFightWindow") != null)
        {
            DestroyImmediate(GameObject.Find("WorldBossFightWindow"));
        }
        //UIManager.instance.OpenSinglePanel("WorldBossWindow", true);
    }

}
