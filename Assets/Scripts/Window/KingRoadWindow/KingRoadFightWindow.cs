using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KingEnemyInfo
{
    public int roleID{ get; set;}
    public int fightNumber{ get; set;}
    public int level{ get;  set;}
    public int rank{ get;  set;}
    public int classNumber{ get; set;}

    public KingEnemyInfo(int _roleID, int _fightNumber, int _level, int _rank, int _classNumber) 
    {
        this.roleID = _roleID;
        this.fightNumber = _fightNumber;
        this.level = _level;
        this.rank = _rank;
        this.classNumber = _classNumber;
    }
}

public class KingRoadFightWindow : MonoBehaviour
{

    public GameObject HeadItem;
    public GameObject HeadGrid;

    public GameObject ForbiddenHeroTabe;
    public GameObject ChooseBattleTabe;
    public GameObject GainResultPart;

    public GameObject ForbiddenItem;
    public GameObject BanGrid;
    public GameObject BanLeftGrid;
    public GameObject BanRightGrid;
    public UILabel LabelNum;
    public UILabel TimeLabel;
    public UILabel MyRankLabel;
    public GameObject MyRankParent;
    public GameObject BackButton;

    private int EnemyID;
    private string EnemyName;
    private string EnemyHeroinfo;
    private int EnemyAllFight = 0;
    private int MyAllFight = 0;
    private bool IsStart = false;  //0zuo,1you
    private int position = 0;//开放

    public GameObject ChoseLabel;
    public GameObject[] MyHeroArr = new GameObject[5];
    public GameObject[] OtherHeroArr = new GameObject[5];

    public GameObject[] MyFightArr = new GameObject[5];
    public GameObject[] OtherFightArr = new GameObject[5];
    public GameObject[] EffectMid = new GameObject[5];
    public GameObject EffectKaizhan;
    public GameObject EffectWin;
    public GameObject EffectLost;
    //public GameObject WinParent;
    //public GameObject LoseParent;
    public GameObject AutoButton;

    public UILabel Timedown;

    public List<GameObject> HeadHeroList = new List<GameObject>();
    private List<KingEnemyInfo> KingEnemyList = new List<KingEnemyInfo>();
    private List<Hero> KingMyHeroList = new List<Hero>();

    private int BanHeroNum = 0;
    private int BanAllNum = 0;
    private int time = 10;

    bool IsRobot = false;//是否机器人

    void Start()
    {
        if (UIEventListener.Get(BackButton).onClick == null) 
        {
            UIEventListener.Get(BackButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPromptWindow("直接退出战斗则本次挑战失败", PromptWindow.PromptType.Confirm, ResetBtnClick, null);           
            };
        }
    }

    void ResetBtnClick() 
    {
        NetworkHandler.instance.SendProcess("6405#" + CharacterRecorder.instance.KingServer + ";" + CharacterRecorder.instance.KingEnemyID + ";" + 0 + ";");
        UIManager.instance.BackUI();
    }
    public void GetKingFight(int enemyId, string enemyName, string enemyHeroinfo)
    {
        this.EnemyID = enemyId;
        this.EnemyName = enemyName;
        this.EnemyHeroinfo = enemyHeroinfo;
        if (CharacterRecorder.instance.KingRank <= 3) 
        {
            BanHeroNum = 1;
            ForbiddenHeroTabe.transform.Find("ButtomBg1").gameObject.SetActive(true);
        }
        else if (CharacterRecorder.instance.KingRank > 3 && CharacterRecorder.instance.KingRank <= 5)
        {
            BanHeroNum = 2;
            ForbiddenHeroTabe.transform.Find("ButtomBg1").gameObject.SetActive(true);
            ForbiddenHeroTabe.transform.Find("ButtomBg2").gameObject.SetActive(true);
        }
        else 
        {
            BanHeroNum = 3;
            ForbiddenHeroTabe.transform.Find("ButtomBg1").gameObject.SetActive(true);
            ForbiddenHeroTabe.transform.Find("ButtomBg2").gameObject.SetActive(true);
            ForbiddenHeroTabe.transform.Find("ButtomBg3").gameObject.SetActive(true);
        }
        BanAllNum = BanHeroNum;
        LabelNum.text = "(0/" + BanHeroNum + ")";
        string[] dataSplit = enemyHeroinfo.Split('!');
        if (dataSplit.Length - 1 < 8)
        {
            KingEnemyList.Add(new KingEnemyInfo(60016, 0, 0, 0, 0));//添加机器人
            KingEnemyList.Add(new KingEnemyInfo(60026, 0, 0, 0, 0));
            KingEnemyList.Add(new KingEnemyInfo(60028, 0, 0, 0, 0));
            IsRobot = true;
            for (int i = 0; i < dataSplit.Length - 1; i++)
            {
                string[] trcSplit = dataSplit[i].Split('$');
                KingEnemyList.Add(new KingEnemyInfo(int.Parse(trcSplit[0]), int.Parse(trcSplit[1]), int.Parse(trcSplit[2]), int.Parse(trcSplit[3]), int.Parse(trcSplit[4])));
                EnemyAllFight += int.Parse(trcSplit[1]);
            }
        }
        else 
        {
            for (int i = 0; i < dataSplit.Length - 1; i++)
            {
                string[] trcSplit = dataSplit[i].Split('$');
                KingEnemyList.Add(new KingEnemyInfo(int.Parse(trcSplit[0]), int.Parse(trcSplit[1]), int.Parse(trcSplit[2]), int.Parse(trcSplit[3]), int.Parse(trcSplit[4])));
                EnemyAllFight += int.Parse(trcSplit[1]);
            }
        }

        int listSize = CharacterRecorder.instance.ownedHeroList.size; //降序排列
        for (int i = 0; i < listSize; i++)
        {
            for (var j = listSize - 1; j > i; j--)
            {
                Hero heroA = CharacterRecorder.instance.ownedHeroList[i];
                Hero heroB = CharacterRecorder.instance.ownedHeroList[j];
                if (heroA.force < heroB.force)
                {
                    var temp = CharacterRecorder.instance.ownedHeroList[i];
                    CharacterRecorder.instance.ownedHeroList[i] = CharacterRecorder.instance.ownedHeroList[j];
                    CharacterRecorder.instance.ownedHeroList[j] = temp;
                }
            }
        }
        foreach (var hero in CharacterRecorder.instance.ownedHeroList) //新建我的herolist
        {
            KingMyHeroList.Add(hero);
        }
        for (int i = 0; i < BanHeroNum; i++) //电脑ban人
        {
            BanMyHero(Random.Range(0, 5));
        }

        for (int i = 0; i < KingEnemyList.Count; i++)
        {
            GameObject go = NGUITools.AddChild(BanGrid, ForbiddenItem);
            go.SetActive(true);
            go.name=KingEnemyList[i].roleID.ToString();
            go.transform.Find("SpriteHead").GetComponent<UISprite>().spriteName = KingEnemyList[i].roleID.ToString();
            UIEventListener.Get(go).onClick = delegate(GameObject other)
            {
                if (BanHeroNum<=0)
                {
                    //UIManager.instance.OpenPromptWindow("只能禁止" + BanHeroNum + "英雄", PromptWindow.PromptType.Hint, null, null);
                }
                else if (go.transform.Find("Cha").gameObject.activeSelf==false)
                {
                    BanAIHero(go);
                }
            };
        }
        CancelInvoke();
        InvokeRepeating("UpdateTime", 0, 1.0f);
    }
    void UpdateTime()//刷新时间
    {
        if (time == 0)
        {
            BanHeroNum--;
            if (BanHeroNum == 0)
            {
                //ForbiddenHeroTabe.SetActive(false);
                //ChooseBattleTabe.SetActive(true);
                StartCoroutine(StaySomeTime());
            }
            else 
            {
                time = 10;
            }
        }
        else 
        {
            time--;
            TimeLabel.text = time.ToString()+"秒";
        }
    }
    void BanAIHero(GameObject go) //ban  AI
    {
        for (int i = KingEnemyList.Count - 1; i >= 0; i--) 
        {
            if (KingEnemyList[i].roleID.ToString() == go.name) 
            {
                if (KingEnemyList[i].roleID != 60016 && KingEnemyList[i].roleID != 60026 && KingEnemyList[i].roleID != 60028 && IsRobot) 
                {
                    for (int j = 0; j < KingEnemyList.Count; j++) 
                    {
                        if (KingEnemyList[j].roleID == 60016 && KingEnemyList[j].fightNumber == 0) 
                        {
                            KingEnemyList[j].fightNumber = KingEnemyList[i].fightNumber;
                            KingEnemyList[j].level = KingEnemyList[i].level;
                            KingEnemyList[j].rank = KingEnemyList[i].rank;
                            KingEnemyList[j].classNumber = KingEnemyList[i].classNumber;
                            break;
                        }
                        else if (KingEnemyList[j].roleID == 60026 && KingEnemyList[j].fightNumber == 0) 
                        {
                            KingEnemyList[j].fightNumber = KingEnemyList[i].fightNumber;
                            KingEnemyList[j].level = KingEnemyList[i].level;
                            KingEnemyList[j].rank = KingEnemyList[i].rank;
                            KingEnemyList[j].classNumber = KingEnemyList[i].classNumber;
                            break;
                        }
                        else if (KingEnemyList[j].roleID == 60028 && KingEnemyList[j].fightNumber == 0) 
                        {
                            KingEnemyList[j].fightNumber = KingEnemyList[i].fightNumber;
                            KingEnemyList[j].level = KingEnemyList[i].level;
                            KingEnemyList[j].rank = KingEnemyList[i].rank;
                            KingEnemyList[j].classNumber = KingEnemyList[i].classNumber;
                            break;
                        }
                    }                   
                }
                go.transform.Find("Cha").gameObject.SetActive(true);
                go.transform.Find("Mask").gameObject.SetActive(true);
                GameObject clone = NGUITools.AddChild(BanLeftGrid, ForbiddenItem);
                clone.SetActive(true);
                clone.name = KingEnemyList[i].roleID.ToString();
                clone.transform.Find("SpriteHead").GetComponent<UISprite>().spriteName = KingEnemyList[i].roleID.ToString();
                BanLeftGrid.GetComponent<UIGrid>().Reposition();
                LabelNum.text = "(" + BanLeftGrid.transform.childCount+ "/" + BanAllNum + ")";
                KingEnemyList.RemoveAt(i);
                break;
            }
        }
        BanHeroNum--;
        if (BanHeroNum == 0)
        {
            //ForbiddenHeroTabe.SetActive(false);
            //ChooseBattleTabe.SetActive(true);
            StartCoroutine(StaySomeTime());
        }
        else 
        {
            CancelInvoke();
            InvokeRepeating("UpdateTime", 0, 1.0f);
        }
    }

    void BanMyHero(int num) 
    {
        GameObject clone = NGUITools.AddChild(BanRightGrid, ForbiddenItem);
        clone.SetActive(true);
        clone.name = KingMyHeroList[num].cardID.ToString();
        clone.transform.Find("SpriteHead").GetComponent<UISprite>().spriteName = KingMyHeroList[num].cardID.ToString();
        BanRightGrid.GetComponent<UIGrid>().Reposition();
        KingMyHeroList.RemoveAt(num);
    }

    IEnumerator StaySomeTime() 
    {
        CancelInvoke();
        TimeLabel.text = "";
        time = 10;
        yield return new WaitForSeconds(1f);
        ForbiddenHeroTabe.SetActive(false);
        ChooseBattleTabe.SetActive(true);
        SetRankPositionHeadItem();//初始化状态，确定先手后手
        if (CharacterRecorder.instance.Vip >= 3)
        {
            AutoButton.SetActive(true);
            if (UIEventListener.Get(AutoButton).onClick == null)
            {
                UIEventListener.Get(AutoButton).onClick = delegate(GameObject go)
                {
                    AutoButton.GetComponent<BoxCollider>().enabled = false;
                    AutoClickToFight();
                };
            }
        }
        else
        {
            AutoButton.SetActive(false);
        }
    }

    public void SetRankPositionHeadItem()
    {
        for (int i = 0; i < KingEnemyList.Count; i++)//AI战力排序
        {
            for (var j = KingEnemyList.Count - 1; j > i; j--)
            {
                KingEnemyInfo heroA = KingEnemyList[i];
                KingEnemyInfo heroB = KingEnemyList[j];
                if (heroA.fightNumber < heroB.fightNumber)
                {
                    var temp = KingEnemyList[i];
                    KingEnemyList[i] = KingEnemyList[j];
                    KingEnemyList[j] = temp;
                }
            }
        }
        for (int i = KingEnemyList.Count - 1; i >= 5; i--) //Ai取前五个最高战力List
        {
            KingEnemyList.RemoveAt(i);
        }
        for (int i = 0; i < 5; i++)///Ai取前五个最高战力
        {
            EnemyAllFight += KingEnemyList[i].fightNumber;
        }

        List<Hero> NewKingMyHeroList = new List<Hero>();
        for (int i = 0; i < KingMyHeroList.Count; i++)//取前五个战力最高的
        {
            if (i < 5)
            {
                //Hero _oneRoleInfo = CharacterRecorder.instance.ownedHeroList[i];
                MyAllFight += KingMyHeroList[i].force;
                GameObject go = NGUITools.AddChild(HeadGrid, HeadItem);
                go.SetActive(true);
                go.name = KingMyHeroList[i].cardID.ToString();//_oneRoleInfo.characterRoleID.ToString();
                go.transform.Find("HerofightSprite").gameObject.SetActive(true);
                go.transform.Find("HerofightSprite").transform.Find("FightNumber").GetComponent<UILabel>().text = KingMyHeroList[i].force.ToString();
                //go.transform.Find("SpriteHead").name = _oneRoleInfo.cardID.ToString();//_oneRoleInfo.characterRoleID.ToString();
                go.transform.Find("SpriteHead").GetComponent<UISprite>().spriteName = KingMyHeroList[i].cardID.ToString();//_oneRoleInfo.characterRoleID.ToString();
                go.transform.Find("SpriteHead").name = KingMyHeroList[i].cardID.ToString();
                go.transform.Find("Level").GetComponent<UILabel>().text = KingMyHeroList[i].level.ToString();
                SetRankInfo(KingMyHeroList[i].rank, go.transform.Find("JunXianSprite").GetComponent<UISprite>(), go.transform.Find("JunXianLv").GetComponent<UILabel>());

                int addNum = TextTranslator.instance.SetHeroNameColor(go.GetComponent<UISprite>(), go.transform.Find("SpritePinJie").GetComponent<UISprite>(), KingMyHeroList[i].classNumber);
                for (int j = 0; j < addNum; j++)
                {
                    GameObject obj = NGUITools.AddChild(go.transform.Find("Grid").gameObject, go.transform.Find("SpritePinJie").gameObject);
                    obj.SetActive(true);
                }
                UIGrid _UIGrid = go.transform.Find("Grid").GetComponent<UIGrid>();
                _UIGrid.sorting = UIGrid.Sorting.Horizontal;
                _UIGrid.pivot = UIWidget.Pivot.Center;
                _UIGrid.animateSmoothly = true;
                HeadHeroList.Add(go);
                UIEventListener.Get(go).onClick = delegate(GameObject other)
                {
                    OnClickToChooseHero(go);
                };
                NewKingMyHeroList.Add(KingMyHeroList[i]);
            }
        }


        KingMyHeroList.Clear();
        KingMyHeroList = NewKingMyHeroList; //删除后面多余的英雄数量，为了自动战斗比较用
        HeadGrid.GetComponent<UIGrid>().Reposition();

        Debug.LogError("MyAllFight  " + MyAllFight);
        Debug.LogError("EnemyAllFight  " + EnemyAllFight);
        if (MyAllFight > EnemyAllFight)
        {
            IsStart = true;
            OpenPosition(0);
        }
        else 
        {
            IsStart = false;
            SetAIOrder(0);
            OpenPosition(0);
            OpenPosition(1);
        }
        CancelInvoke();
        InvokeRepeating("UpdateTimeTwo", 0, 1.0f);
    }

    void UpdateTimeTwo()//倒计时
    {
        if (time == 0)
        {
            if (HeadGrid.transform.childCount != 0) 
            {
                int num = Random.Range(0, HeadGrid.transform.childCount);
                Debug.LogError("num  " + num);
                OnClickToChooseHero(HeadGrid.transform.GetChild(num).gameObject);
            }
        }
        else
        {
            time--;
            Timedown.text = time.ToString();
        }
    }


    private void OpenPosition(int position)
    {
        MyHeroArr[position].transform.Find("SpriteItem").transform.Find("LockSprite").gameObject.SetActive(false);
        MyHeroArr[position].transform.Find("SpriteItem").transform.Find("BlueSprite").gameObject.SetActive(true);
        MyHeroArr[position].transform.Find("SpriteItem").transform.Find("Label").gameObject.SetActive(true);
    }
    private void OnClickToChooseHero(GameObject go)     //点击头像生成
    {
        Debug.Log(go.name);
        for (int i = 0; i < 5; i++) 
        {
            if (MyHeroArr[i].transform.Find("SpriteItem").transform.Find("LockSprite").gameObject.activeSelf == false && MyHeroArr[i].transform.Find("HeadItem").gameObject.activeSelf == false) 
            {
                for (int j = 0; j < KingMyHeroList.Count; j++) 
                {
                    if (KingMyHeroList[j].cardID.ToString() == go.name) 
                    {
                        MyFightArr[i].SetActive(true);
                        MyFightArr[i].transform.Find("PowerNumber").GetComponent<UILabel>().text = KingMyHeroList[j].force.ToString();
                        MyHeroArr[i].transform.Find("HeadItem").gameObject.SetActive(true);
                        MyHeroArr[i].transform.Find("HeadIcon").gameObject.SetActive(true);
                        MyHeroArr[i].transform.Find("HeadIcon").GetComponent<UISprite>().spriteName = KingMyHeroList[j].cardID.ToString();
                        //MyHeroArr[i].transform.Find("HeadIcon").gameObject.name = KingMyHeroList[j].cardID.ToString();

                        MyHeroArr[i].transform.Find("HeadItem").transform.Find("Level").GetComponent<UILabel>().text = KingMyHeroList[j].level.ToString();
                        SetRankInfo(KingMyHeroList[j].rank, MyHeroArr[i].transform.Find("HeadItem").transform.Find("JunXianSprite").GetComponent<UISprite>(), MyHeroArr[i].transform.Find("HeadItem").transform.Find("JunXianLv").GetComponent<UILabel>());

                        int addNum = TextTranslator.instance.SetHeroNameColor(MyHeroArr[i].transform.Find("HeadItem").GetComponent<UISprite>(), MyHeroArr[i].transform.Find("HeadItem").transform.Find("SpritePinJie").GetComponent<UISprite>(), KingMyHeroList[j].classNumber);
                        for (int x = 0; x < addNum; x++)
                        {
                            GameObject obj = NGUITools.AddChild(MyHeroArr[i].transform.Find("HeadItem").transform.Find("Grid").gameObject, MyHeroArr[i].transform.Find("HeadItem").transform.Find("SpritePinJie").gameObject);
                            obj.SetActive(true);
                        }
                        UIGrid _UIGrid = MyHeroArr[i].transform.Find("HeadItem").transform.Find("Grid").GetComponent<UIGrid>();
                        _UIGrid.sorting = UIGrid.Sorting.Horizontal;
                        _UIGrid.pivot = UIWidget.Pivot.Center;
                        _UIGrid.animateSmoothly = true;
                        DestroyImmediate(go);
                        KingMyHeroList.RemoveAt(j);
                        break;
                    }
                }              
                position = i + 1;
                SavePosition();
                time = 10;
                CancelInvoke();
                InvokeRepeating("UpdateTimeTwo", 0, 1.0f);
                break;
            }
        }
        //for (int i = HeadGrid.transform.childCount-1; i>=0; i--) 
        //{
        //    if (HeadGrid.transform.GetChild(i).name == go.name) 
        //    {
        //        Destroy(HeadGrid.transform.GetChild(i));
        //        break;
        //    }
        //}
    }

    private void AutoClickToFight() //自动战斗
    {
        while (KingMyHeroList.Count > 0 && GetHeroNum()<5)
        {
            int num = 0; //Random.Range(0, HeadGrid.transform.childCount);
            //Debug.LogError("num  " + num);
            for (int i = 0; i < 5; i++)
            {
                if (MyHeroArr[i].transform.Find("SpriteItem").transform.Find("LockSprite").gameObject.activeSelf == false && MyHeroArr[i].transform.Find("HeadItem").gameObject.activeSelf == false) 
                {
                    if (OtherFightArr[i].activeSelf == false)
                    {
                        num = KingMyHeroList.Count - 1;
                    }
                    else if (OtherFightArr[i].activeSelf == true)
                    {
                        bool ishave = false;
                        int enemyforce = int.Parse(OtherFightArr[i].transform.Find("PowerNumber").GetComponent<UILabel>().text);//ai战力


                        for (int j = KingMyHeroList.Count - 1; j >= 0; j--)
                        {
                            if (KingMyHeroList[j].force >= enemyforce)
                            {
                                num = j;
                                ishave = true;
                                break;
                            }
                        }
                        if (ishave == false)
                        {
                            num = KingMyHeroList.Count - 1;
                        }
                    }
                    Debug.LogError("自动选择的位置  " + num);
                    OnClickToChooseHero(HeadGrid.transform.GetChild(num).gameObject);
                    break;
                }
            }
        }
    }

    private int GetHeroNum() //取得上阵英雄数量
    {
        int num = 0;
        for (int i = 0; i < 5; i++) 
        {
            if (MyHeroArr[i].transform.Find("HeadItem").gameObject.activeSelf == true) 
            {
                num++;
            }
        }
        return num;
    }
    private void SetAIOrder(int Aiposition) //从零开始
    {
        int Num = 0;
        if (MyFightArr[Aiposition].activeSelf==true)
        {
            int fightnum = int.Parse(MyFightArr[Aiposition].transform.Find("PowerNumber").GetComponent<UILabel>().text);
            for (int i = KingEnemyList.Count - 1; i >= 0; i--)
            {
                if (KingEnemyList[i].fightNumber > fightnum)
                {
                    Num = i;
                    break;
                }
            }
        }
        else
        {
            Num = KingEnemyList.Count - 1;
        }
        OtherFightArr[Aiposition].SetActive(true);
        OtherFightArr[Aiposition].transform.Find("PowerNumber").GetComponent<UILabel>().text = KingEnemyList[Num].fightNumber.ToString();

        OtherHeroArr[Aiposition].transform.Find("HeadItem").gameObject.SetActive(true);
        OtherHeroArr[Aiposition].transform.Find("HeadIcon").gameObject.SetActive(true);
        OtherHeroArr[Aiposition].transform.Find("HeadIcon").GetComponent<UISprite>().spriteName = KingEnemyList[Num].roleID.ToString();
        //OtherHeroArr[Aiposition].transform.Find("HeadIcon").gameObject.name = KingEnemyList[Num].roleID.ToString();

        OtherHeroArr[Aiposition].transform.Find("HeadItem").transform.Find("Level").GetComponent<UILabel>().text = KingEnemyList[Num].level.ToString();
        SetRankInfo(KingEnemyList[Num].rank, OtherHeroArr[Aiposition].transform.Find("HeadItem").transform.Find("JunXianSprite").GetComponent<UISprite>(), OtherHeroArr[Aiposition].transform.Find("HeadItem").transform.Find("JunXianLv").GetComponent<UILabel>());

        int addNum = TextTranslator.instance.SetHeroNameColor(OtherHeroArr[Aiposition].transform.Find("HeadItem").GetComponent<UISprite>(), OtherHeroArr[Aiposition].transform.Find("HeadItem").transform.Find("SpritePinJie").GetComponent<UISprite>(), KingEnemyList[Num].classNumber);
        for (int x = 0; x < addNum; x++)
        {
            GameObject obj = NGUITools.AddChild(OtherHeroArr[Aiposition].transform.Find("HeadItem").transform.Find("Grid").gameObject, OtherHeroArr[Aiposition].transform.Find("HeadItem").transform.Find("SpritePinJie").gameObject);
            obj.SetActive(true);
        }
        UIGrid _UIGrid = OtherHeroArr[Aiposition].transform.Find("HeadItem").transform.Find("Grid").GetComponent<UIGrid>();
        _UIGrid.sorting = UIGrid.Sorting.Horizontal;
        _UIGrid.pivot = UIWidget.Pivot.Center;
        _UIGrid.animateSmoothly = true;
        KingEnemyList.RemoveAt(Num);
    }

    private void SavePosition()
    {
        if (IsStart)
        {
            if (position == 1 && OtherFightArr[0].activeSelf == false)
            {
                SetAIOrder(0);
                SetAIOrder(1);
                OpenPosition(1);
                OpenPosition(2);
            }
            else if (position == 3 && OtherFightArr[2].activeSelf == false)
            {
                SetAIOrder(2);
                SetAIOrder(3);
                OpenPosition(3);
                OpenPosition(4);
            }
            else if (position == 5 && OtherFightArr[4].activeSelf == false)
            {
                SetAIOrder(4);
                BackButton.SetActive(false);
                CancelInvoke("UpdateTimeTwo");
                time =-1;
                ChoseLabel.gameObject.SetActive(false);
                Timedown.gameObject.SetActive(false);
                StartCoroutine(PlayEffectKaizhan());
            }
        }
        else 
        {
            if (position == 2 && OtherFightArr[1].activeSelf == false)
            {
                SetAIOrder(1);
                SetAIOrder(2);
                OpenPosition(2);
                OpenPosition(3);
            }
            else if (position == 4 && OtherFightArr[3].activeSelf == false)
            {
                SetAIOrder(3);
                SetAIOrder(4);
                OpenPosition(4);
            }
            else if (position == 5) 
            {
                BackButton.SetActive(false);
                CancelInvoke("UpdateTimeTwo");
                time = -1;
                ChoseLabel.gameObject.SetActive(false);
                Timedown.gameObject.SetActive(false);
                StartCoroutine(PlayEffectKaizhan());
            }
        }
    }

    IEnumerator PlayEffectKaizhan()
    {
        EffectKaizhan.SetActive(true);
        int[] Towin = new int[] { 0, 0, 0, 0, 0 };
        yield return new WaitForSeconds(3f);
        for(int i=0;i<5;i++)
        {
            StartCoroutine(PVEfight(i,Towin));
            yield return new WaitForSeconds(1f);
        }
        yield return new WaitForSeconds(1f);
        int AddNum = 0;
        for (int i = 0; i < 5; i++) 
        {
            AddNum += Towin[i];
        }

        ChooseBattleTabe.SetActive(false);
        if (AddNum >= 3)
        {
            EffectWin.SetActive(true);
            NetworkHandler.instance.SendProcess("6405#" + CharacterRecorder.instance.KingServer + ";" + CharacterRecorder.instance.KingEnemyID + ";" + 1 + ";");
            //yield return new WaitForSeconds(2f);
            //EffectWin.SetActive(false);
            GainResultPart.SetActive(true);
            GainResultPart.transform.Find("SpriteAward").gameObject.SetActive(true);
            GainResultPart.transform.Find("SpriteAwardBg").gameObject.SetActive(true);
            //NetworkHandler.instance.SendProcess("6405#" + CharacterRecorder.instance.KingServer + ";" + CharacterRecorder.instance.KingEnemyID + ";" + 1 + ";");
            UIEventListener.Get(GainResultPart.transform.Find("LiveButton").gameObject).onClick = delegate(GameObject go)
            {
                //NetworkHandler.instance.SendProcess("6405#" + CharacterRecorder.instance.KingServer + ";" + CharacterRecorder.instance.KingEnemyID + ";" + 1 + ";");
                UIManager.instance.BackUI();
            };
            if (CharacterRecorder.instance.KingMyNum < CharacterRecorder.instance.KingEnemyNum)
            {
                MyRankParent.SetActive(false);
                MyRankLabel.gameObject.SetActive(true);
                MyRankLabel.text = "本次排名未发生变化";
            }
            else 
            {
                MyRankParent.SetActive(true);
                MyRankLabel.gameObject.SetActive(false);
                MyRankParent.transform.Find("BeforeRank").GetComponent<UILabel>().text = CharacterRecorder.instance.KingMyNum.ToString();
                MyRankParent.transform.Find("AfterRank").GetComponent<UILabel>().text = CharacterRecorder.instance.KingEnemyNum.ToString();
            }
        }
        else 
        {
            EffectLost.SetActive(true);
            NetworkHandler.instance.SendProcess("6405#" + CharacterRecorder.instance.KingServer + ";" + CharacterRecorder.instance.KingEnemyID + ";" + 0 + ";");
            //yield return new WaitForSeconds(2f);
            //EffectLost.SetActive(false);
            GainResultPart.SetActive(true);
            GainResultPart.transform.Find("SpriteAward").gameObject.SetActive(false);
            GainResultPart.transform.Find("SpriteAwardBg").gameObject.SetActive(false);
            //NetworkHandler.instance.SendProcess("6405#" + CharacterRecorder.instance.KingServer + ";" + CharacterRecorder.instance.KingEnemyID + ";" + 0 + ";");
            UIEventListener.Get(GainResultPart.transform.Find("LiveButton").gameObject).onClick = delegate(GameObject go)
            {
                UIManager.instance.BackUI();
                //NetworkHandler.instance.SendProcess("6405#" + CharacterRecorder.instance.KingServer + ";" + CharacterRecorder.instance.KingEnemyID + ";" + 0 + ";");
            };
            MyRankParent.SetActive(false);
            MyRankLabel.gameObject.SetActive(true);
            MyRankLabel.text = "本次排名未发生变化";
        }
        UISprite RankSprite = GainResultPart.transform.Find("KingRankSprite").GetComponent<UISprite>();

        Debug.Log("段位+" + CharacterRecorder.instance.KingRank);
        switch (CharacterRecorder.instance.KingRank)
        {
            case 1:
                RankSprite.spriteName = "wangzhe_icon1";
                break;
            case 2:
                RankSprite.spriteName = "wangzhe_icon5";
                break;
            case 3:
                RankSprite.spriteName = "wangzhe_icon6";
                break;
            case 4:
                RankSprite.spriteName = "wangzhe_icon2";
                break;
            case 5:
                RankSprite.spriteName = "wangzhe_icon3";
                break;
            case 6:
                RankSprite.spriteName = "wangzhe_icon4";
                break;
            default:
                break;
        }
    }

    //public void SetFightResult() 
    //{
        
    //}

    IEnumerator PVEfight(int num, int[] Towin) 
    {
        int myfight = int.Parse(MyFightArr[num].transform.Find("PowerNumber").GetComponent<UILabel>().text);
        int otherfight = int.Parse(OtherFightArr[num].transform.Find("PowerNumber").GetComponent<UILabel>().text);
        if (myfight >= otherfight)
        {
            Towin[num] = 1;
            MyHeroArr[num].GetComponent<NcCurveAnimation>().enabled = true;
            OtherHeroArr[num].GetComponent<NcCurveAnimation>().enabled = true;
            OtherHeroArr[num].transform.Find("HeadIcon").GetComponent<NcCurveAnimation>().enabled = true;
            EffectMid[num].SetActive(true);
            yield return new WaitForSeconds(1.67f);
            OtherHeroArr[num].transform.Find("BaoZha").gameObject.SetActive(true);
            OtherHeroArr[num].transform.Find("HeadIcon").gameObject.SetActive(false);
            //OtherHeroArr[num].transform.Find("HeadItem").transform.Find("JunXianSprite").gameObject.SetActive(false);
            //OtherHeroArr[num].transform.Find("HeadItem").transform.Find("JunXianLv").gameObject.SetActive(false);
            OtherHeroArr[num].transform.Find("HeadItem").transform.Find("Level").gameObject.SetActive(false);
            OtherHeroArr[num].transform.Find("HeadItem").transform.Find("RankIcon").gameObject.SetActive(false);
        }
        else 
        {
            Towin[num] = 0;
            MyHeroArr[num].GetComponent<NcCurveAnimation>().enabled = true;
            OtherHeroArr[num].GetComponent<NcCurveAnimation>().enabled = true;
            MyHeroArr[num].transform.Find("HeadIcon").GetComponent<NcCurveAnimation>().enabled = true;
            EffectMid[num].SetActive(true);
            yield return new WaitForSeconds(1.67f);
            MyHeroArr[num].transform.Find("BaoZha").gameObject.SetActive(true);
            MyHeroArr[num].transform.Find("HeadIcon").gameObject.SetActive(false);
            //MyHeroArr[num].transform.Find("HeadItem").transform.Find("JunXianSprite").gameObject.SetActive(false);
            //MyHeroArr[num].transform.Find("HeadItem").transform.Find("JunXianLv").gameObject.SetActive(false);
            MyHeroArr[num].transform.Find("HeadItem").transform.Find("Level").gameObject.SetActive(false);
            MyHeroArr[num].transform.Find("HeadItem").transform.Find("RankIcon").gameObject.SetActive(false);
        }
    }


    IEnumerator FinallyPk()
    {
        int[] Towin = new int[] { 0, 0, 0, 0, 0 };
        int[] myheroId = new int[5];
        int[] enemyHeroId = new int[5];
        ChoseLabel.SetActive(false);
        if (IsStart == false)
        {
            for (int i = 0; i < 5; i++)
            {
                MyHeroArr[i].GetComponent<TweenPosition>().from = MyHeroArr[i].transform.localPosition;
                MyHeroArr[i].GetComponent<TweenPosition>().to = new Vector3(-190f, MyHeroArr[i].transform.localPosition.y, 0);
                MyHeroArr[i].GetComponent<TweenPosition>().ResetToBeginning();
                MyHeroArr[i].GetComponent<TweenPosition>().PlayForward();

                OtherHeroArr[i].GetComponent<TweenPosition>().from = OtherHeroArr[i].transform.localPosition;
                OtherHeroArr[i].GetComponent<TweenPosition>().to = new Vector3(190f, OtherHeroArr[i].transform.localPosition.y, 0);
                OtherHeroArr[i].GetComponent<TweenPosition>().ResetToBeginning();
                OtherHeroArr[i].GetComponent<TweenPosition>().PlayForward();

                if (int.Parse(MyFightArr[i].transform.Find("PowerNumber").GetComponent<UILabel>().text) - int.Parse(OtherFightArr[i].transform.Find("PowerNumber").GetComponent<UILabel>().text) > 0)
                {
                    Towin[i] = 1;
                }
                myheroId[i] = int.Parse(MyHeroArr[i].transform.GetChild(3).name);
                enemyHeroId[i] = int.Parse(OtherHeroArr[i].transform.GetChild(3).name);
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(0.5f);
            UIManager.instance.OpenSinglePanel("KingRoadResultWindow", true);
            GameObject.Find("KingRoadResultWindow").GetComponent<KingRoadResultWindow>().SetFightResult(EnemyName, CharacterRecorder.instance.KingServer, CharacterRecorder.instance.KingEnemyID, Towin, myheroId, enemyHeroId, EnemyHeroinfo);
        }
        else
        {
            for (int i = 0; i < 5; i++)
            {
                MyHeroArr[i].GetComponent<TweenPosition>().from = MyHeroArr[i].transform.localPosition;
                MyHeroArr[i].GetComponent<TweenPosition>().to = new Vector3(190f, MyHeroArr[i].transform.localPosition.y, 0);
                MyHeroArr[i].GetComponent<TweenPosition>().ResetToBeginning();
                MyHeroArr[i].GetComponent<TweenPosition>().PlayForward();

                OtherHeroArr[i].GetComponent<TweenPosition>().from = OtherHeroArr[i].transform.localPosition;
                OtherHeroArr[i].GetComponent<TweenPosition>().to = new Vector3(-190f, OtherHeroArr[i].transform.localPosition.y, 0);
                OtherHeroArr[i].GetComponent<TweenPosition>().ResetToBeginning();
                OtherHeroArr[i].GetComponent<TweenPosition>().PlayForward();

                if (int.Parse(MyFightArr[i].transform.Find("PowerNumber").GetComponent<UILabel>().text) - int.Parse(OtherFightArr[i].transform.Find("PowerNumber").GetComponent<UILabel>().text) > 0)
                {
                    Towin[i] = 1;
                }
                myheroId[i] = int.Parse(MyHeroArr[i].transform.GetChild(3).name);
                enemyHeroId[i] = int.Parse(OtherHeroArr[i].transform.GetChild(3).name);
                yield return new WaitForSeconds(0.2f);
            }

            yield return new WaitForSeconds(0.5f);
            UIManager.instance.OpenSinglePanel("KingRoadResultWindow", true);
            GameObject.Find("KingRoadResultWindow").GetComponent<KingRoadResultWindow>().SetFightResult(EnemyName, CharacterRecorder.instance.KingServer, CharacterRecorder.instance.KingEnemyID, Towin, myheroId, enemyHeroId, EnemyHeroinfo);


        }

    }
    //void DestroyGride()
    //{
    //    for (int i = gride.transform.childCount - 1; i >= 0; i--)
    //    {
    //        DestroyImmediate(gride.transform.GetChild(i).gameObject);
    //    }
    //}


    //设置军衔

    void SetRankInfo(int _rank, UISprite _UISprite, UILabel _myUILabel)
    {
        _UISprite.spriteName = string.Format("rank{0}", _rank.ToString("00"));
        _myUILabel.parent.transform.Find("RankIcon").transform.GetChild(_rank).gameObject.SetActive(true);
        //switch (_rank)
        //{
        //    case 1:
        //        _myUILabel.text = "下士";
        //        break;
        //    case 2:
        //        _myUILabel.text = "中士";
        //        break;
        //    case 3:
        //        _myUILabel.text = "上士";
        //        break;
        //    case 4:
        //        _myUILabel.text = "少尉";
        //        break;
        //    case 5:
        //        _myUILabel.text = "中尉";
        //        break;
        //    case 6:
        //        _myUILabel.text = "上尉";
        //        break;
        //    case 7:
        //        _myUILabel.text = "少校";
        //        break;
        //    case 8:
        //        _myUILabel.text = "中校";
        //        break;
        //    case 9:
        //        _myUILabel.text = "上校";
        //        break;
        //    case 10:
        //        _myUILabel.text = "少将";
        //        break;
        //    case 11:
        //        _myUILabel.text = "中将";
        //        break;
        //    case 12:
        //        _myUILabel.text = "上将";
        //        break;
        //    default:
        //        break;
        //}
    }
}


#region 旧的王者之路
//public class KingRoadFightWindow : MonoBehaviour {

//    public GameObject HeadItem;
//    public GameObject HeadGrid;

//    public GameObject ChoseLabel;
//    public GameObject[] MyHeroArr = new GameObject[5];
//    public GameObject[] OtherHeroArr = new GameObject[5];

//    public GameObject[] MyFightArr = new GameObject[5];
//    public GameObject[] OtherFightArr = new GameObject[5];

//    public List<GameObject> HeadHeroList= new List<GameObject>();
//    private List<KingEnemyInfo> KingEnemyList= new List<KingEnemyInfo>();

//    public GameObject SaveButton;

//    private int EnemyID;
//    private string EnemyName;
//    private string EnemyHeroinfo;
//    private int EnemyAllFight = 0;
//    private int MyAllFight = 0;
//    private int location = 0;  //0zuo,1you
//    void Start () {
//        //SetRankPositionHeadItem();
//        //GetKingFight(50, "diren", "60010$500$20$1$1!60011$600$25$2$2!60012$1300$30$3$3!60013$2000$20$1$1!60014$1000$20$6$1!");

//        if (UIEventListener.Get(SaveButton).onClick == null) 
//        {
//            UIEventListener.Get(SaveButton).onClick = delegate(GameObject go)
//            {
//                SavePosition();
//            };
//        }
//    }
//    public void GetKingFight(int enemyId,string enemyName,string enemyHeroinfo) 
//    {
//        this.EnemyID = enemyId;
//        this.EnemyName = enemyName;
//        this.EnemyHeroinfo = enemyHeroinfo;
//        //SetRankPositionHeadItem();
//        string[] dataSplit = enemyHeroinfo.Split('!');
//        for (int i = 0; i < dataSplit.Length - 1; i++) 
//        {
//            string[] trcSplit = dataSplit[i].Split('$');
//            KingEnemyList.Add(new KingEnemyInfo(int.Parse(trcSplit[0]), int.Parse(trcSplit[1]), int.Parse(trcSplit[2]), int.Parse(trcSplit[3]), int.Parse(trcSplit[4])));
//            EnemyAllFight += int.Parse(trcSplit[1]);
//        }

//        KingEnemyList.Add(new KingEnemyInfo(60016, 0, 0, 0, 0));//添加机器人
//        KingEnemyList.Add(new KingEnemyInfo(60026, 0, 0, 0, 0));
//        KingEnemyList.Add(new KingEnemyInfo(60028, 0, 0, 0, 0));

//        for (int i = 0; i < KingEnemyList.Count; i++)//战力排序
//        {
//            for (var j = KingEnemyList.Count - 1; j > i; j--)
//            {
//                KingEnemyInfo heroA = KingEnemyList[i];
//                KingEnemyInfo heroB = KingEnemyList[j];
//                if (heroA.fightNumber < heroB.fightNumber)
//                {
//                    var temp = KingEnemyList[i];
//                    KingEnemyList[i] = KingEnemyList[j];
//                    KingEnemyList[j] = temp;
//                }
//            }
//        }



//        if (MyAllFight >= EnemyAllFight)
//        {
//            location = 0;
//            for (int i = 0; i < 5; i++)
//            {
//                MyHeroArr[i] = GameObject.Find("SpriteItem" + (i + 1));
//                MyHeroArr[i].tag = "CanMove";
//                OtherHeroArr[i] = GameObject.Find("RivalItem" + (i + 1));

//                MyFightArr[i] = GameObject.Find("KingRoadFightWindow").transform.Find("All").transform.Find("LeftGrid").transform.Find("Leftfight" + (i + 1)).gameObject;
//                OtherFightArr[i] = GameObject.Find("KingRoadFightWindow").transform.Find("All").transform.Find("RightGrid").transform.Find("Rightfight" + (i + 1)).gameObject;
//            }
//            //MyHeroArr[0].transform.Find("BlueSprite").gameObject.SetActive(true);
//            //MyHeroArr[0].transform.Find("LockSprite").gameObject.SetActive(false);
//            //MyHeroArr[0].transform.Find("Label").gameObject.SetActive(true);
//            SetPkOrder(0);
//        }
//        else 
//        {
//            location = 1;
//            for (int i = 0; i < 5; i++)
//            {
//                MyHeroArr[i] = GameObject.Find("RivalItem" + (i + 1));
//                MyHeroArr[i].tag = "CanMove";
//                OtherHeroArr[i] = GameObject.Find("SpriteItem" + (i + 1));
//                MyFightArr[i] = GameObject.Find("KingRoadFightWindow").transform.Find("All").transform.Find("RightGrid").transform.Find("Rightfight" + (i + 1)).gameObject;
//                OtherFightArr[i] = GameObject.Find("KingRoadFightWindow").transform.Find("All").transform.Find("LeftGrid").transform.Find("Leftfight" + (i + 1)).gameObject;
//            }
//            SetAIOrder(0);
//            SetPkOrder(0);
//            SetPkOrder(1);
//        }
//    }


//    public void SetRankPositionHeadItem()
//    {
//        int listSize = CharacterRecorder.instance.ownedHeroList.size;////战力降序排序
//        for (int i = 0; i < listSize; i++)
//        {
//            for (var j = listSize - 1; j > i; j--)
//            {
//                Hero heroA = CharacterRecorder.instance.ownedHeroList[i];
//                Hero heroB = CharacterRecorder.instance.ownedHeroList[j];
//                if (heroA.force < heroB.force)
//                {
//                    var temp = CharacterRecorder.instance.ownedHeroList[i];
//                    CharacterRecorder.instance.ownedHeroList[i] = CharacterRecorder.instance.ownedHeroList[j];
//                    CharacterRecorder.instance.ownedHeroList[j] = temp;
//                }
//            }
//        }
//        for (int i = 0; i < listSize; i++)//取前五个战力最高的
//        {
//            if (i < 5)
//            {
//                Hero _oneRoleInfo = CharacterRecorder.instance.ownedHeroList[i];
//                MyAllFight += _oneRoleInfo.force;
//                GameObject go = NGUITools.AddChild(HeadGrid, HeadItem);
//                go.SetActive(true);
//                go.name = _oneRoleInfo.cardID.ToString();//_oneRoleInfo.characterRoleID.ToString();
//                //go.transform.Find("SpriteHead").name = _oneRoleInfo.cardID.ToString();//_oneRoleInfo.characterRoleID.ToString();
//                go.transform.Find("SpriteHead").GetComponent<UISprite>().spriteName = _oneRoleInfo.cardID.ToString();//_oneRoleInfo.characterRoleID.ToString();
//                go.transform.Find("SpriteHead").name = _oneRoleInfo.cardID.ToString();
//                go.transform.Find("Level").GetComponent<UILabel>().text = _oneRoleInfo.level.ToString();
//                SetRankInfo(_oneRoleInfo.rank, go.transform.Find("JunXianSprite").GetComponent<UISprite>(), go.transform.Find("JunXianLv").GetComponent<UILabel>());

//                int addNum = TextTranslator.instance.SetHeroNameColor(go.GetComponent<UISprite>(), go.transform.Find("SpritePinJie").GetComponent<UISprite>(), _oneRoleInfo.classNumber);
//                for (int j = 0; j < addNum; j++)
//                {
//                    GameObject obj = NGUITools.AddChild(go.transform.Find("Grid").gameObject, go.transform.Find("SpritePinJie").gameObject);
//                    obj.SetActive(true);
//                }
//                UIGrid _UIGrid = go.transform.Find("Grid").GetComponent<UIGrid>();
//                _UIGrid.sorting = UIGrid.Sorting.Horizontal;
//                _UIGrid.pivot = UIWidget.Pivot.Center;
//                _UIGrid.animateSmoothly = true;
//                HeadHeroList.Add(go);
//            }
//        }
//        HeadGrid.GetComponent<UIGrid>().Reposition();
//    }

//    private void SetPkOrder(int position) 
//    {
//        if (position == 0)
//        {
//            MyHeroArr[0].GetComponent<BoxCollider>().enabled = true;
//            MyHeroArr[0].transform.Find("BlueSprite").gameObject.SetActive(true);
//            MyHeroArr[0].transform.Find("Label").gameObject.SetActive(true);
//            MyHeroArr[0].transform.Find("LockSprite").gameObject.SetActive(false);
//        }
//        else if (position == 1) 
//        {
//            MyHeroArr[1].GetComponent<BoxCollider>().enabled = true;
//            MyHeroArr[1].transform.Find("BlueSprite").gameObject.SetActive(true);
//            MyHeroArr[1].transform.Find("Label").gameObject.SetActive(true);
//            MyHeroArr[1].transform.Find("LockSprite").gameObject.SetActive(false);

//        }
//        else if (position == 2)
//        {
//            MyHeroArr[2].GetComponent<BoxCollider>().enabled = true;
//            MyHeroArr[2].transform.Find("BlueSprite").gameObject.SetActive(true);
//            MyHeroArr[2].transform.Find("Label").gameObject.SetActive(true);
//            MyHeroArr[2].transform.Find("LockSprite").gameObject.SetActive(false);
//        }
//        else if (position == 3)
//        {
//            MyHeroArr[3].GetComponent<BoxCollider>().enabled = true;
//            MyHeroArr[3].transform.Find("BlueSprite").gameObject.SetActive(true);
//            MyHeroArr[3].transform.Find("Label").gameObject.SetActive(true);
//            MyHeroArr[3].transform.Find("LockSprite").gameObject.SetActive(false);
//        }
//        else 
//        {
//            MyHeroArr[4].GetComponent<BoxCollider>().enabled = true;
//            MyHeroArr[4].transform.Find("BlueSprite").gameObject.SetActive(true);
//            MyHeroArr[4].transform.Find("Label").gameObject.SetActive(true);
//            MyHeroArr[4].transform.Find("LockSprite").gameObject.SetActive(false);
//        }
//    }

//    private void SetAIOrder(int Aiposition) //从零开始
//    {
//        int Num =0;
//        if (MyHeroArr[Aiposition].transform.childCount==4)
//        {
//            int fightnum = int.Parse(MyFightArr[Aiposition].transform.Find("PowerNumber").GetComponent<UILabel>().text);
//            for (int i = KingEnemyList.Count - 1; i >= 0; i--)
//            {
//                if (KingEnemyList[i].fightNumber > fightnum)
//                {
//                    Num = i;
//                    break;
//                }
//            }
//        }
//        else 
//        {
//            Num = KingEnemyList.Count - 1;
//        }
//        GameObject go = NGUITools.AddChild(OtherHeroArr[Aiposition], HeadItem);
//        go.SetActive(true);
//        go.transform.localPosition = Vector3.zero;
//        go.transform.localScale = Vector3.one;
//        go.SetActive(true);
//        go.name = KingEnemyList[Num].roleID.ToString();
//        go.transform.Find("SpriteHead").GetComponent<UISprite>().spriteName = KingEnemyList[Num].roleID.ToString();
//        go.transform.Find("SpriteHead").name = KingEnemyList[Num].roleID.ToString();
//        go.transform.Find("Level").GetComponent<UILabel>().text = KingEnemyList[Num].level.ToString();
//        go.transform.Find(KingEnemyList[Num].roleID.ToString()).GetComponent<MyKingDragObj>().enabled = false;
//        SetRankInfo(KingEnemyList[Num].rank, go.transform.Find("JunXianSprite").GetComponent<UISprite>(), go.transform.Find("JunXianLv").GetComponent<UILabel>());

//        int addNum = TextTranslator.instance.SetHeroNameColor(go.GetComponent<UISprite>(), go.transform.Find("SpritePinJie").GetComponent<UISprite>(), KingEnemyList[Num].classNumber);
//        for (int j = 0; j < addNum; j++)
//        {
//            GameObject obj = NGUITools.AddChild(go.transform.Find("Grid").gameObject, go.transform.Find("SpritePinJie").gameObject);
//            obj.SetActive(true);
//        }
//        UIGrid _UIGrid = go.transform.Find("Grid").GetComponent<UIGrid>();
//        _UIGrid.sorting = UIGrid.Sorting.Horizontal;
//        _UIGrid.pivot = UIWidget.Pivot.Center;
//        _UIGrid.animateSmoothly = true;

//        OtherHeroArr[Aiposition].transform.Find("BlueSprite").gameObject.SetActive(true);
//        //OtherHeroArr[Aiposition].transform.Find("fight").gameObject.SetActive(true);
//        //OtherHeroArr[Aiposition].transform.Find("fight").transform.Find("PowerNumber").GetComponent<UILabel>().text = KingEnemyList[Num].fightNumber.ToString();
//        OtherFightArr[Aiposition].SetActive(true);
//        OtherFightArr[Aiposition].transform.Find("PowerNumber").GetComponent<UILabel>().text = KingEnemyList[Num].fightNumber.ToString();
//        KingEnemyList.RemoveAt(Num);
//    }

//    private void SavePosition() 
//    {
//        int indexer = 0;
//        for (int i = 0; i < MyHeroArr.Length; i++)
//        {
//            if (MyHeroArr[i].transform.Find("BlueSprite").gameObject.activeSelf == true && MyHeroArr[i].transform.childCount==3) 
//            {
//                indexer = 1;
//                break;
//            }
//        }
//        if (indexer == 1)
//        {
//            UIManager.instance.OpenPromptWindow("有英雄未上阵", PromptWindow.PromptType.Hint, null, null);
//        }
//        else 
//        {
//            if (location == 0)
//            {
//                if (5 - HeadHeroList.Count == 1 && OtherHeroArr[0].transform.childCount == 3) 
//                {
//                    SetAIOrder(0);
//                    SetAIOrder(1);

//                    SetPkOrder(1);
//                    SetPkOrder(2);
//                }
//                else if (5 - HeadHeroList.Count == 3 && OtherHeroArr[2].transform.childCount == 3) 
//                {
//                    SetAIOrder(2);
//                    SetAIOrder(3);

//                    SetPkOrder(3);
//                    SetPkOrder(4);
//                }
//                else if (5 - HeadHeroList.Count == 5 && OtherHeroArr[4].transform.childCount == 3)
//                {
//                    SetAIOrder(4);
//                }
//                else 
//                {
//                    SaveButton.GetComponent<BoxCollider>().enabled = false;
//                    StartCoroutine(FinallyPk());
//                }
//            }
//            else 
//            {
//                if (5 - HeadHeroList.Count == 2 && OtherHeroArr[1].transform.childCount == 3) 
//                {
//                    SetAIOrder(1);
//                    SetAIOrder(2);

//                    SetPkOrder(2);
//                    SetPkOrder(3);
//                }
//                else if (5 - HeadHeroList.Count == 4 && OtherHeroArr[3].transform.childCount == 3) 
//                {
//                    SetAIOrder(3);
//                    SetAIOrder(4);

//                    SetPkOrder(4);
//                }
//                else if (5 - HeadHeroList.Count == 5) 
//                {
//                    SaveButton.GetComponent<BoxCollider>().enabled = false;
//                    StartCoroutine(FinallyPk());
//                }
//            }
//        }
//    }

//    IEnumerator FinallyPk() 
//    {
//        int[] Towin = new int[] { 0, 0, 0, 0, 0 };
//        int[] myheroId = new int[5];
//        int[] enemyHeroId = new int[5];
//        ChoseLabel.SetActive(false);
//        if (location == 1)
//        {
//            for (int i = 0; i < 5; i++)
//            {
//                MyHeroArr[i].GetComponent<TweenPosition>().from = MyHeroArr[i].transform.localPosition;
//                MyHeroArr[i].GetComponent<TweenPosition>().to = new Vector3(-190f, MyHeroArr[i].transform.localPosition.y, 0);
//                MyHeroArr[i].GetComponent<TweenPosition>().ResetToBeginning();
//                MyHeroArr[i].GetComponent<TweenPosition>().PlayForward();

//                OtherHeroArr[i].GetComponent<TweenPosition>().from = OtherHeroArr[i].transform.localPosition;
//                OtherHeroArr[i].GetComponent<TweenPosition>().to = new Vector3(190f, OtherHeroArr[i].transform.localPosition.y, 0);
//                OtherHeroArr[i].GetComponent<TweenPosition>().ResetToBeginning();
//                OtherHeroArr[i].GetComponent<TweenPosition>().PlayForward();

//                if (int.Parse(MyFightArr[i].transform.Find("PowerNumber").GetComponent<UILabel>().text) - int.Parse(OtherFightArr[i].transform.Find("PowerNumber").GetComponent<UILabel>().text) > 0) 
//                {
//                    Towin[i] = 1;
//                }
//                myheroId[i] = int.Parse(MyHeroArr[i].transform.GetChild(3).name);
//                enemyHeroId[i] = int.Parse(OtherHeroArr[i].transform.GetChild(3).name);
//                yield return new WaitForSeconds(0.2f);
//            }

//            yield return new WaitForSeconds(0.5f);
//            UIManager.instance.OpenSinglePanel("KingRoadResultWindow", true);
//            GameObject.Find("KingRoadResultWindow").GetComponent<KingRoadResultWindow>().SetFightResult(EnemyName, CharacterRecorder.instance.KingServer, CharacterRecorder.instance.KingEnemyID, Towin, myheroId, enemyHeroId, EnemyHeroinfo);
//        }
//        else 
//        {
//            for (int i = 0; i < 5; i++)
//            {
//                MyHeroArr[i].GetComponent<TweenPosition>().from = MyHeroArr[i].transform.localPosition;
//                MyHeroArr[i].GetComponent<TweenPosition>().to = new Vector3(190f, MyHeroArr[i].transform.localPosition.y, 0);
//                MyHeroArr[i].GetComponent<TweenPosition>().ResetToBeginning();
//                MyHeroArr[i].GetComponent<TweenPosition>().PlayForward();

//                OtherHeroArr[i].GetComponent<TweenPosition>().from = OtherHeroArr[i].transform.localPosition;
//                OtherHeroArr[i].GetComponent<TweenPosition>().to = new Vector3(-190f, OtherHeroArr[i].transform.localPosition.y, 0);
//                OtherHeroArr[i].GetComponent<TweenPosition>().ResetToBeginning();
//                OtherHeroArr[i].GetComponent<TweenPosition>().PlayForward();

//                if (int.Parse(MyFightArr[i].transform.Find("PowerNumber").GetComponent<UILabel>().text) - int.Parse(OtherFightArr[i].transform.Find("PowerNumber").GetComponent<UILabel>().text) > 0)
//                {
//                    Towin[i] = 1;
//                }
//                myheroId[i] = int.Parse(MyHeroArr[i].transform.GetChild(3).name);
//                enemyHeroId[i] = int.Parse(OtherHeroArr[i].transform.GetChild(3).name);
//                yield return new WaitForSeconds(0.2f);
//            }

//            yield return new WaitForSeconds(0.5f);
//            UIManager.instance.OpenSinglePanel("KingRoadResultWindow", true);
//            GameObject.Find("KingRoadResultWindow").GetComponent<KingRoadResultWindow>().SetFightResult(EnemyName, CharacterRecorder.instance.KingServer, CharacterRecorder.instance.KingEnemyID, Towin, myheroId, enemyHeroId, EnemyHeroinfo);

           
//        }

//    }
//    //void DestroyGride()
//    //{
//    //    for (int i = gride.transform.childCount - 1; i >= 0; i--)
//    //    {
//    //        DestroyImmediate(gride.transform.GetChild(i).gameObject);
//    //    }
//    //}


//    //设置军衔

//    void SetRankInfo(int _rank, UISprite _UISprite, UILabel _myUILabel)
//    {
//        _UISprite.spriteName = string.Format("rank{0}", _rank.ToString("00"));
//        switch (_rank)
//        {
//            case 1:
//                _myUILabel.text = "下士";
//                break;
//            case 2:
//                _myUILabel.text = "中士";
//                break;
//            case 3:
//                _myUILabel.text = "上士";
//                break;
//            case 4:
//                _myUILabel.text = "少尉";
//                break;
//            case 5:
//                _myUILabel.text = "中尉";
//                break;
//            case 6:
//                _myUILabel.text = "上尉";
//                break;
//            case 7:
//                _myUILabel.text = "少校";
//                break;
//            case 8:
//                _myUILabel.text = "中校";
//                break;
//            case 9:
//                _myUILabel.text = "上校";
//                break;
//            case 10:
//                _myUILabel.text = "少将";
//                break;
//            case 11:
//                _myUILabel.text = "中将";
//                break;
//            case 12:
//                _myUILabel.text = "上将";
//                break;
//            default:
//                break;
//        }
//    }
//}
#endregion