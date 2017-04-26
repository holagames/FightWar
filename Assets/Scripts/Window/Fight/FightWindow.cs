using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;

public class FightWindow : MonoBehaviour
{

    //public UITexture Skill1Mask;
    //public UITexture Skill2Mask;
    //public UITexture Skill3Mask;

    public GameObject TeamObject;
    public GameObject Team1;
    public GameObject Team2;
    public GameObject Team3;
    public GameObject Team4;
    public GameObject Team5;
    public Camera TeamCamera;
    public GameObject FightMask;

    public GameObject prefabDamige;
    public GameObject prefabBlood;
    public GameObject prefabSequence;
    public GameObject prefabBoss;
    public GameObject prefabCaptain;
    public GameObject prefabWonder;

    public GameObject SkillEffect;

    public GameObject LabelCombo;
    public GameObject EnemyManualSkill;
    public UILabel LabelEnemyManualSkill;

    public UISlider MyUISlider;

    public UILabel LabelVictory1;
    public UILabel LabelVictory2;
    public UILabel LabelVictory3;
    public UILabel LabelVictory;
    public UILabel LabelChange;

    public GameObject HandSkillButton1;
    public GameObject HandSkillButton2;
    public GameObject HandCancelButton;
    public GameObject HandSkillEffect;

    public GameObject SkillButton1;
    public GameObject SkillButton2;
    public GameObject SkillButton3;

    public GameObject SkillUI1;
    public GameObject SkillUI2;
    public GameObject SkillUI3;

    public GameObject SkillMask1;
    public GameObject SkillMask2;
    public GameObject SkillMask3;

    public GameObject SpeedButton;
    public GameObject HandButton;
    public GameObject BackButton;
    public GameObject ExitButton;
    public GameObject SkipButton;

    public GameObject HandSkill;
    public GameObject Tactics;
    public GameObject TacticsInfo;
    public UILabel LabelSkillName;
    public UILabel LabelSkillDes;

    public GameObject SkillEffect1;
    public GameObject SkillEffect2;
    public GameObject SkillEffect3;

    public GameObject SkillEffectCD;

    public GameObject SkillEffectOK1;
    public GameObject SkillEffectOK2;
    public GameObject SkillEffectOK3;

    public GameObject LabelSkill2;
    public GameObject LabelSkill3;

    public GameObject Kill1;
    public GameObject Kill2;
    public GameObject Kill3;
    public GameObject Kill4;
    public GameObject Kill5;
    public GameObject Kill6;
    public GameObject KillHero;

    public float SkillCD1;
    public float SkillCD2;
    public float SkillCD3;

    public float SkillUpCD1;
    public float SkillUpCD2;
    public float SkillUpCD3;
    float Timer = 0;

    GameObject[] RoleObject = new GameObject[6];
    List<GameObject> ListHero = new List<GameObject>();

    public GameObject MyGrid;
    public GameObject MyHeroItem;

    public UISprite MySequence;

    GameObject MyCamera;
    bool IsRolePress = false;

    public GameObject Pause;

    // Use this for initialization

    Vector3 MouseClickPostion = new Vector3();

    MouseClick mc;

    Vector3 TouchPosition;
    Vector3 MovePosition;

    public GameObject TextureTouch;


    public UILabel KindName;
    public UILabel KindDes;
    public UITexture SkillSprite;
    public UITexture SkillSprite2;
    public UILabel SkillDes;
    public GameObject HeroInfoObj;
    public UITexture HeroTexture;
    public UILabel HeroName;
    public GameObject CloseInfoButton;
    float ScreenHeight;
    float ScreenWidth;

    Camera FightCamera;

    string SkillName2 = "0";
    string SkillName3 = "0";
    //三星提示
    public GameObject GateDescription;
    public GameObject InfoBoard;
    public GameObject ShrinkMessage;
    public GameObject GameTest;
    //日常副本BOSS
    public GameObject FourSlider;
    public GameObject ThreeSlider;
    public GameObject TwoSlider;
    public GameObject OneSlider;
    public GameObject EverydayBossobj;
    public bool isClose = false;
    private int HPNum;
    public UILabel NowTimer;
    public UISprite TimeSprite;
    public UILabel HistoryClickNumber;
    public UILabel NowClickNumber;
    public int ClickNumber;
    //提醒字段
    public GameObject RedMessage;
    public UILabel RedLabel;
    //是否开始战斗
    public bool isStartFight = false;
    //是否显示redmessage
    public bool isRedMessage = false;
    public GameObject MessageEffect;
    public string RedLabelText = "";
    public bool isFinishMessge = false;
    public UIAtlas NomalAtlas;
    //弹幕
    public GameObject AllBulletLabel;
    public GameObject BulletLabel;
    private int BulletNumber = 1;
    public Queue<GameObject> BulletList = new Queue<GameObject>();
    public List<GameObject> BulletInfoList = new List<GameObject>();
    public bool isOpenBullet = false;
    public GameObject isOpenBulletButton;
    public GameObject isWriteBulletButton;
    public GameObject BulletInputWindow;
    public string SelfBulletLabel;
    public GameObject InputSetButton;
    public GameObject InputLabel;
    //连赢场数、虚弱度、守军数量
    public Transform winStreakSprite;//连赢场数
    public Transform weakSprite;//虚弱度
    public Transform garrisonNumSprite;//守军数量
    public Transform nameSprite;//守军姓名

    public string FightPosition = "";

    public GameObject Repress;
    public GameObject Repress1;
    public GameObject Repress2;
    public GameObject Repress3;
    public GameObject Repress4;
    public GameObject Repress5;

    public UISprite KongBai;
    void Awake()
    {
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
            KongBai.width = (int)(1200 * (System.Convert.ToSingle(Screen.width) / Screen.height) / (System.Convert.ToSingle(960) / 640));
        }
    }
    void Start()
    {
        GameObject.Find("MainCamera").GetComponent<Camera>().clearFlags = CameraClearFlags.Skybox;
        GameObject.Find("UIRoot").transform.Find("SelfAdaptionBlackWindow/Up").gameObject.SetActive(true);
        SkillCD1 = 0.1f;
        SkillCD2 = 5;
        SkillCD3 = 10;
        //MySequence.alpha = 0;
        SkillUpCD1 = 0.1f;
        SkillUpCD2 = 5;
        SkillUpCD3 = 10;

        ScreenHeight = Screen.height / 2;
        ScreenWidth = Screen.width / 2;

        MyUISlider = GameObject.Find("CameraSlider").GetComponent<UISlider>();
        MyCamera = GameObject.Find("MainCamera");

        // isOpenStarMessage();//三星信息显示;

        //得到连赢场次
        //winStreakSprite = GameObject.Find("WinStreakSprite").transform;
        ////得到虚弱度
        //weakSprite = GameObject.Find("WeakSprite").transform  ;

        mc = MyCamera.GetComponent<MouseClick>();

        //////////////////////GameTest//////////////////////////
        #region SpriteTest1
        if (UIEventListener.Get(GameObject.Find("SpriteTest1")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("SpriteTest1")).onClick += delegate(GameObject go)
            {
                foreach (var p in PictureCreater.instance.ListRolePicture)
                {
                    p.RoleNowBlood = 99999;
                    p.RoleMaxBlood = 99999;
                }
                foreach (var p in PictureCreater.instance.ListEnemyPicture)
                {
                    p.RoleNowBlood = 99999;
                    p.RoleMaxBlood = 99999;
                }
                UIManager.instance.OpenPromptWindow("敌我双方血量放大", PromptWindow.PromptType.Hint, null, null);
            };
        }
        #endregion
        #region SpriteTest2
        if (UIEventListener.Get(GameObject.Find("SpriteTest2")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("SpriteTest2")).onClick += delegate(GameObject go)
            {
                foreach (var p in PictureCreater.instance.ListRolePicture)
                {
                    p.RoleNowBlood = 100;
                    p.RoleMaxBlood = 100;
                }
                foreach (var p in PictureCreater.instance.ListEnemyPicture)
                {
                    p.RoleNowBlood = 100;
                    p.RoleMaxBlood = 100;
                }
                UIManager.instance.OpenPromptWindow("敌我双方血量缩小", PromptWindow.PromptType.Hint, null, null);
            };
        }
        #endregion
        #region SpriteTest3
        if (UIEventListener.Get(GameObject.Find("SpriteTest3")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("SpriteTest3")).onClick += delegate(GameObject go)
            {
                if (PictureCreater.instance.IsLock)
                {
                    PictureCreater.instance.IsLock = false;
                    GameObject.Find("SpriteTest3").transform.Find("Label").gameObject.GetComponent<UILabel>().text = "上锁";
                }
                else
                {
                    PictureCreater.instance.IsLock = true;
                    GameObject.Find("SpriteTest3").transform.Find("Label").gameObject.GetComponent<UILabel>().text = "解锁";
                }
            };
        }
        #endregion
        GameTest.SetActive(false);
        //////////////////////GameTest//////////////////////////

        if (UIEventListener.Get(GameObject.Find("ExitButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("ExitButton")).onClick += delegate(GameObject go)
            {
                if (SceneTransformer.instance.CheckGuideIsFinish())
                {
                    PictureCreater.instance.IsFight = false;
                    Pause.SetActive(true);
                    if (!PictureCreater.instance.IsRemember)
                    {
                        Pause.transform.FindChild("GoButton/Sprite").gameObject.GetComponent<UISprite>().spriteName = "zhandou_word3";
                    }

                    Time.timeScale = 0;
                }
            };
        }

        if (UIEventListener.Get(SkipButton).onClick == null)
        {
            UIEventListener.Get(SkipButton).onClick += delegate(GameObject go)
            {
                //StartCoroutine(PictureCreater.instance.ShowWin(PictureCreater.instance.ListRolePicture));
                PictureCreater.instance.IsSkip = true;
                SkipButton.SetActive(false);
            };
        }

        if (UIEventListener.Get(GameObject.Find("GoButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("GoButton")).onClick += delegate(GameObject go)
            {
                if (PictureCreater.instance.IsRemember)
                {
                    PictureCreater.instance.IsFight = true;
                    Pause.SetActive(false);
                    Time.timeScale = PictureCreater.instance.HardLevel;
                }
                else
                {
                    PictureCreater.instance.IsRemember = false;
                    PictureCreater.instance.IsSkip = false;
                    PictureCreater.instance.ActObject.SetActive(false);
                    PictureCreater.instance.RememberCount = 0;                    
                    if (PictureCreater.instance.FightStyle == 0)
                    {
                        PictureCreater.instance.StartFight();
                        UIManager.instance.DestroyAllPanel();
                    }
                    else
                    {
                        UIManager.instance.DestroyPanel("FightWindow");
                        UIManager.instance.PopUI();
                        PictureCreater.instance.StartPVP(PictureCreater.instance.PVPString);
                    }                    
                }
            };
        }
        #region CancelButton
        if (UIEventListener.Get(GameObject.Find("CancelButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("CancelButton")).onClick += delegate(GameObject go)
            {
                SkillEffect1.SetActive(false);
                SkillEffect2.SetActive(false);
                SkillEffect3.SetActive(false);
                TacticsInfo.SetActive(false);
                //Tactics.SetActive(true);
                PictureCreater.instance.IsLock = false;
                PictureCreater.instance.MyBases.SetActive(false);
                PictureCreater.instance.MyPositions.SetActive(true);
                Tactics.transform.localPosition = new Vector3(Tactics.transform.localPosition.x, -10, Tactics.transform.localPosition.z);
                if (PictureCreater.instance.SkillFire1 == 1)
                {
                    PictureCreater.instance.SkillFire1 = 0;
                }
                if (PictureCreater.instance.SkillFire2 == 1)
                {
                    PictureCreater.instance.SkillFire2 = 0;
                }
                if (PictureCreater.instance.SkillFire3 == 1)
                {
                    PictureCreater.instance.SkillFire3 = 0;
                }

                for (int j = 0; j < PictureCreater.instance.ListRolePicture.Count; j++)
                {
                    //PictureCreater.instance.ListRolePicture[j].RolePictureObject.layer = 8;
                    PictureCreater.instance.ListRolePicture[j].RoleObject.GetComponent<ColliderDisplayText>().HideTarget();
                    PictureCreater.instance.ListRolePicture[j].RoleRedBloodObject.SetActive(false);
                    //foreach (var c in PictureCreater.instance.ListRolePicture[j].RoleObject.GetComponentsInChildren(typeof(SkinnedMeshRenderer), true))
                    //{
                    //    c.gameObject.layer = 8;
                    //}

                    //foreach (var c in PictureCreater.instance.ListRolePicture[j].RoleObject.GetComponentsInChildren(typeof(MeshRenderer), true))
                    //{
                    //    c.gameObject.layer = 8;
                    //}
                }

                for (int j = 0; j < PictureCreater.instance.ListEnemyPicture.Count; j++)
                {
                    //PictureCreater.instance.ListEnemyPicture[j].RolePictureObject.layer = 8;
                    PictureCreater.instance.ListEnemyPicture[j].RoleObject.GetComponent<ColliderDisplayText>().HideTarget();
                    PictureCreater.instance.ListEnemyPicture[j].RoleRedBloodObject.SetActive(false);
                    //foreach (var c in PictureCreater.instance.ListEnemyPicture[j].RoleObject.GetComponentsInChildren(typeof(SkinnedMeshRenderer), true))
                    //{
                    //    c.gameObject.layer = 8;
                    //}

                    //foreach (var c in PictureCreater.instance.ListEnemyPicture[j].RoleObject.GetComponentsInChildren(typeof(MeshRenderer), true))
                    //{
                    //    c.gameObject.layer = 8;
                    //}
                }

                foreach (Component c in PictureCreater.instance.MyMoves.GetComponentsInChildren(typeof(MeshRenderer), true))
                {
                    c.gameObject.renderer.material = PictureCreater.instance.BlankMaterial;
                }
            };
        }
        #endregion
        #region SkillButton1
        if (UIEventListener.Get(GameObject.Find("SkillButton1")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("SkillButton1")).onClick += delegate(GameObject go)
            {

                if (SkillCD1 == 0)
                {
                    //Debug.LogError(PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) + " " + PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()));
                    if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 12 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) < 11)
                    {

                    }
                    else if (PictureCreater.instance.SkillFire1 == 0)
                    {
                        if (SceneTransformer.instance.CheckGuideIsFinish() == false)
                        {
                            if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 12 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 11)
                            {
                                SceneTransformer.instance.NewGuideButtonClick();
                            }
                        }
                        PictureCreater.instance.SkillFire1 = 1;
                        SkillEffect1.SetActive(true);

                        TacticsInfo.SetActive(true);
                        //Tactics.SetActive(false);
                        PictureCreater.instance.IsLock = true;

                        PictureCreater.instance.MyBases.SetActive(true);
                        PictureCreater.instance.MyPositions.SetActive(false);
                        Tactics.GetComponent<TweenPosition>().enabled = true;
                        Tactics.GetComponent<TweenPosition>().ResetToBeginning();


                        ManualSkill ms = TextTranslator.instance.GetManualSkillByID(PictureCreater.instance.FightSkill1);
                        if (ms.Type == 1 || ms.Type == 2)
                        {
                            for (int j = 0; j < PictureCreater.instance.ListRolePicture.Count; j++)
                            {
                                //PictureCreater.instance.ListRolePicture[j].RolePictureObject.layer = 10;
                                if (PictureCreater.instance.ListRolePicture[j].RoleNowBlood > 0 && PictureCreater.instance.ListRolePicture[j].RolePosition > 0)
                                {
                                    PictureCreater.instance.ListRolePicture[j].RoleObject.GetComponent<ColliderDisplayText>().ShowTarget(3);
                                    PictureCreater.instance.ListRolePicture[j].RoleRedBloodObject.SetActive(true);
                                }

                                //foreach (var c in PictureCreater.instance.ListRolePicture[j].RoleObject.GetComponentsInChildren(typeof(SkinnedMeshRenderer), true))
                                //{
                                //    c.gameObject.layer = 10;
                                //}

                                //foreach (var c in PictureCreater.instance.ListRolePicture[j].RoleObject.GetComponentsInChildren(typeof(MeshRenderer), true))
                                //{
                                //    if (c.name != "Shadow")
                                //    {
                                //        c.gameObject.layer = 10;
                                //    }
                                //}
                            }
                        }
                        else if (ms.Type == 3 || ms.Type == 4)
                        {
                            for (int j = 0; j < PictureCreater.instance.ListEnemyPicture.Count; j++)
                            {
                                //PictureCreater.instance.ListEnemyPicture[j].RolePictureObject.layer = 10;
                                if (PictureCreater.instance.ListEnemyPicture[j].RoleNowBlood > 0 && PictureCreater.instance.ListEnemyPicture[j].RolePosition > 0)
                                {
                                    PictureCreater.instance.ListEnemyPicture[j].RoleObject.GetComponent<ColliderDisplayText>().ShowTarget(2);
                                    PictureCreater.instance.ListEnemyPicture[j].RoleRedBloodObject.SetActive(true);
                                }

                                //foreach (var c in PictureCreater.instance.ListEnemyPicture[j].RoleObject.GetComponentsInChildren(typeof(SkinnedMeshRenderer), true))
                                //{
                                //    c.gameObject.layer = 10;
                                //}

                                //foreach (var c in PictureCreater.instance.ListEnemyPicture[j].RoleObject.GetComponentsInChildren(typeof(MeshRenderer), true))
                                //{
                                //    if (c.name != "Shadow")
                                //    {
                                //        c.gameObject.layer = 10;
                                //    }
                                //}
                            }
                        }
                        else
                        {
                            foreach (Component c in PictureCreater.instance.MyMoves.GetComponentsInChildren(typeof(MeshRenderer), true))
                            {
                                int Position = int.Parse(c.name.Replace("Move", ""));
                                bool IsRepeat = false;
                                for (int j = 0; j < PictureCreater.instance.ListRolePicture.Count; j++)
                                {
                                    if (Position == PictureCreater.instance.ListRolePicture[j].RolePosition)
                                    {
                                        IsRepeat = true;
                                        break;
                                    }
                                }

                                for (int j = 0; j < PictureCreater.instance.ListEnemyPicture.Count; j++)
                                {
                                    if (Position == PictureCreater.instance.ListEnemyPicture[j].RolePosition)
                                    {
                                        IsRepeat = true;
                                        break;
                                    }
                                }

                                for (int j = 0; j < PictureCreater.instance.ListTerrainPosition.Count; j++)
                                {
                                    if (Position == PictureCreater.instance.ListTerrainPosition[j])
                                    {
                                        IsRepeat = true;
                                        break;
                                    }
                                }

                                if (!IsRepeat)
                                {
                                    c.gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("Game/attackposition");
                                }
                            }
                        }


                        SkillEffect.transform.Find("TextureAvatar").gameObject.GetComponent<TweenPosition>().ResetToBeginning();

                        LabelSkillName.text = ms.skillName;
                        LabelSkillDes.text = ms.description.Replace("%d", (ms.skillVal1 / 100f * CharacterRecorder.instance.level).ToString() + "%");
                        LabelSkillDes.text = LabelSkillDes.text.Replace("%s", (ms.skillVal2 + ms.skillVal1 * CharacterRecorder.instance.level).ToString());
                        LabelSkillDes.text = LabelSkillDes.text.Replace("[$]", "\n");
                    }
                }
            };
        }
        #endregion
        #region SkillButton2
        if (UIEventListener.Get(GameObject.Find("SkillButton2")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("SkillButton2")).onClick += delegate(GameObject go)
            {
                if (SkillCD2 == 0)
                {
                    if (SkillName2 != "0")
                    {
                        if (PictureCreater.instance.SkillFire2 == 0)
                        {
                            PictureCreater.instance.SkillFire2 = 1;
                            SkillEffect1.SetActive(true);

                            TacticsInfo.SetActive(true);
                            //Tactics.SetActive(false);
                            PictureCreater.instance.IsLock = true;

                            PictureCreater.instance.MyBases.SetActive(true);
                            PictureCreater.instance.MyPositions.SetActive(false);
                            Tactics.GetComponent<TweenPosition>().enabled = true;
                            Tactics.GetComponent<TweenPosition>().ResetToBeginning();

                            ManualSkill ms = TextTranslator.instance.GetManualSkillByID(PictureCreater.instance.FightSkill2);
                            if (ms.Type == 1 || ms.Type == 2)
                            {
                                for (int j = 0; j < PictureCreater.instance.ListRolePicture.Count; j++)
                                {
                                    //PictureCreater.instance.ListRolePicture[j].RolePictureObject.layer = 10;
                                    if (PictureCreater.instance.ListRolePicture[j].RoleNowBlood > 0 && PictureCreater.instance.ListRolePicture[j].RolePosition > 0)
                                    {
                                        PictureCreater.instance.ListRolePicture[j].RoleObject.GetComponent<ColliderDisplayText>().ShowTarget(3);
                                        PictureCreater.instance.ListRolePicture[j].RoleRedBloodObject.SetActive(true);
                                    }

                                    //foreach (var c in PictureCreater.instance.ListRolePicture[j].RoleObject.GetComponentsInChildren(typeof(SkinnedMeshRenderer), true))
                                    //{
                                    //    c.gameObject.layer = 10;
                                    //}

                                    //foreach (var c in PictureCreater.instance.ListRolePicture[j].RoleObject.GetComponentsInChildren(typeof(MeshRenderer), true))
                                    //{
                                    //    if (c.name != "Shadow")
                                    //    {
                                    //        c.gameObject.layer = 10;
                                    //    }
                                    //}
                                }
                            }
                            else if (ms.Type == 3 || ms.Type == 4)
                            {
                                for (int j = 0; j < PictureCreater.instance.ListEnemyPicture.Count; j++)
                                {
                                    //PictureCreater.instance.ListEnemyPicture[j].RolePictureObject.layer = 10;
                                    if (PictureCreater.instance.ListEnemyPicture[j].RoleNowBlood > 0 && PictureCreater.instance.ListEnemyPicture[j].RolePosition > 0)
                                    {
                                        PictureCreater.instance.ListEnemyPicture[j].RoleObject.GetComponent<ColliderDisplayText>().ShowTarget(2);
                                        PictureCreater.instance.ListEnemyPicture[j].RoleRedBloodObject.SetActive(true);
                                    }

                                    //foreach (var c in PictureCreater.instance.ListEnemyPicture[j].RoleObject.GetComponentsInChildren(typeof(SkinnedMeshRenderer), true))
                                    //{
                                    //    c.gameObject.layer = 10;
                                    //}

                                    //foreach (var c in PictureCreater.instance.ListEnemyPicture[j].RoleObject.GetComponentsInChildren(typeof(MeshRenderer), true))
                                    //{
                                    //    if (c.name != "Shadow")
                                    //    {
                                    //        c.gameObject.layer = 10;
                                    //    }
                                    //}
                                }
                            }
                            else
                            {
                                foreach (Component c in PictureCreater.instance.MyMoves.GetComponentsInChildren(typeof(MeshRenderer), true))
                                {
                                    int Position = int.Parse(c.name.Replace("Move", ""));
                                    bool IsRepeat = false;
                                    for (int j = 0; j < PictureCreater.instance.ListRolePicture.Count; j++)
                                    {
                                        if (Position == PictureCreater.instance.ListRolePicture[j].RolePosition)
                                        {
                                            IsRepeat = true;
                                            break;
                                        }
                                    }

                                    for (int j = 0; j < PictureCreater.instance.ListEnemyPicture.Count; j++)
                                    {
                                        if (Position == PictureCreater.instance.ListEnemyPicture[j].RolePosition)
                                        {
                                            IsRepeat = true;
                                            break;
                                        }
                                    }

                                    for (int j = 0; j < PictureCreater.instance.ListTerrainPosition.Count; j++)
                                    {
                                        if (Position == PictureCreater.instance.ListTerrainPosition[j])
                                        {
                                            IsRepeat = true;
                                            break;
                                        }
                                    }

                                    if (!IsRepeat)
                                    {
                                        c.gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("Game/attackposition");
                                    }
                                }
                            }

                            LabelSkillName.text = ms.skillName;
                            LabelSkillDes.text = ms.description.Replace("%d", (ms.skillVal1 / 100f * CharacterRecorder.instance.level).ToString() + "%");
                        }
                    }
                }
            };
        }
        #endregion
        #region SkillButton3
        if (UIEventListener.Get(GameObject.Find("SkillButton3")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("SkillButton3")).onClick += delegate(GameObject go)
            {
                if (SkillCD3 == 0)
                {
                    if (SkillName3 != "0")
                    {
                        if (PictureCreater.instance.SkillFire3 == 0)
                        {
                            PictureCreater.instance.SkillFire3 = 1;
                            SkillEffect3.SetActive(true);
                            TacticsInfo.SetActive(true);
                            //Tactics.SetActive(false);
                            PictureCreater.instance.IsLock = true;

                            PictureCreater.instance.MyBases.SetActive(true);
                            PictureCreater.instance.MyPositions.SetActive(false);
                            Tactics.GetComponent<TweenPosition>().enabled = true;
                            Tactics.GetComponent<TweenPosition>().ResetToBeginning();
                            ManualSkill ms = TextTranslator.instance.GetManualSkillByID(PictureCreater.instance.FightSkill3);
                            if (ms.Type == 1 || ms.Type == 2)
                            {
                                for (int j = 0; j < PictureCreater.instance.ListRolePicture.Count; j++)
                                {
                                    //PictureCreater.instance.ListRolePicture[j].RolePictureObject.layer = 10;
                                    if (PictureCreater.instance.ListRolePicture[j].RoleNowBlood > 0 && PictureCreater.instance.ListRolePicture[j].RolePosition > 0)
                                    {
                                        PictureCreater.instance.ListRolePicture[j].RoleObject.GetComponent<ColliderDisplayText>().ShowTarget(3);
                                        PictureCreater.instance.ListRolePicture[j].RoleRedBloodObject.SetActive(true);
                                    }

                                    //foreach (var c in PictureCreater.instance.ListRolePicture[j].RoleObject.GetComponentsInChildren(typeof(SkinnedMeshRenderer), true))
                                    //{
                                    //    c.gameObject.layer = 10;
                                    //}

                                    //foreach (var c in PictureCreater.instance.ListRolePicture[j].RoleObject.GetComponentsInChildren(typeof(MeshRenderer), true))
                                    //{
                                    //    if (c.name != "Shadow")
                                    //    {
                                    //        c.gameObject.layer = 10;
                                    //    }
                                    //}
                                }
                            }
                            else if (ms.Type == 3 || ms.Type == 4)
                            {
                                for (int j = 0; j < PictureCreater.instance.ListEnemyPicture.Count; j++)
                                {
                                    //PictureCreater.instance.ListEnemyPicture[j].RolePictureObject.layer = 10;
                                    if (PictureCreater.instance.ListEnemyPicture[j].RoleNowBlood > 0 && PictureCreater.instance.ListEnemyPicture[j].RolePosition > 0)
                                    {
                                        PictureCreater.instance.ListEnemyPicture[j].RoleObject.GetComponent<ColliderDisplayText>().ShowTarget(2);
                                        PictureCreater.instance.ListEnemyPicture[j].RoleRedBloodObject.SetActive(true);
                                    }

                                    //foreach (var c in PictureCreater.instance.ListEnemyPicture[j].RoleObject.GetComponentsInChildren(typeof(SkinnedMeshRenderer), true))
                                    //{
                                    //    c.gameObject.layer = 10;
                                    //}

                                    //foreach (var c in PictureCreater.instance.ListEnemyPicture[j].RoleObject.GetComponentsInChildren(typeof(MeshRenderer), true))
                                    //{
                                    //    if (c.name != "Shadow")
                                    //    {
                                    //        c.gameObject.layer = 10;
                                    //    }
                                    //}
                                }
                            }
                            else
                            {
                                foreach (Component c in PictureCreater.instance.MyMoves.GetComponentsInChildren(typeof(MeshRenderer), true))
                                {
                                    int Position = int.Parse(c.name.Replace("Move", ""));
                                    bool IsRepeat = false;
                                    for (int j = 0; j < PictureCreater.instance.ListRolePicture.Count; j++)
                                    {
                                        if (Position == PictureCreater.instance.ListRolePicture[j].RolePosition)
                                        {
                                            IsRepeat = true;
                                            break;
                                        }
                                    }

                                    for (int j = 0; j < PictureCreater.instance.ListEnemyPicture.Count; j++)
                                    {
                                        if (Position == PictureCreater.instance.ListEnemyPicture[j].RolePosition)
                                        {
                                            IsRepeat = true;
                                            break;
                                        }
                                    }

                                    for (int j = 0; j < PictureCreater.instance.ListTerrainPosition.Count; j++)
                                    {
                                        if (Position == PictureCreater.instance.ListTerrainPosition[j])
                                        {
                                            IsRepeat = true;
                                            break;
                                        }
                                    }

                                    if (!IsRepeat)
                                    {
                                        c.gameObject.renderer.material.mainTexture = (Texture2D)Resources.Load("Game/attackposition");
                                    }
                                }
                            }

                            LabelSkillName.text = ms.skillName;
                            LabelSkillDes.text = ms.description.Replace("%d", (ms.skillVal1 / 100f * CharacterRecorder.instance.level).ToString() + "%");
                            LabelSkillDes.text = ms.description.Replace("%s", (ms.skillVal2 + ms.skillVal1 * CharacterRecorder.instance.level).ToString());
                        }
                    }
                }
            };
        }
        #endregion
        #region  HandCancelButton
        if (UIEventListener.Get(GameObject.Find("HandCancelButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("HandCancelButton")).onClick += delegate(GameObject go)
            {
                if (!PictureCreater.instance.ListSequence[0].IsMonster)
                {
                    if (PictureCreater.instance.ListRolePicture[PictureCreater.instance.ListSequence[0].RoleIndex].RoleSkillPoint >= 1000)
                    {
                        PictureCreater.instance.IsSkill = false;
                        HandSkillButton2.GetComponent<UISprite>().spriteName = "buttonskill";
                        HandCancelButton.SetActive(false);
                    }
                }
            };
        }
        #endregion
        #region HandSkillButton2
        if (UIEventListener.Get(GameObject.Find("HandSkillButton2")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("HandSkillButton2")).onClick += delegate(GameObject go)
            {
                if (!PictureCreater.instance.ListSequence[0].IsMonster)
                {
                    if (PictureCreater.instance.ListRolePicture[PictureCreater.instance.ListSequence[0].RoleIndex].RoleSkillPoint >= 1000)
                    {
                        PictureCreater.instance.IsSkill = true;
                        HandSkillButton2.GetComponent<UISprite>().spriteName = "buttonskillan";
                        HandSkillEffect.SetActive(false);
                        HandCancelButton.SetActive(true);
                    }
                }
            };
        }
        #endregion
        #region HandSkillButton1
        if (UIEventListener.Get(GameObject.Find("HandSkillButton1")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("HandSkillButton1")).onClick += delegate(GameObject go)
            {
                if (!PictureCreater.instance.ListSequence[0].IsMonster)
                {
                    if (PictureCreater.instance.IsLock)
                    {
                        Buff NewBuff = TextTranslator.instance.GetBuffByID(728); //丛林冒险防御
                        PictureCreater.instance.RoleAddBuff(PictureCreater.instance.ListRolePicture, PictureCreater.instance.ListSequence[0].RoleIndex, NewBuff);
                        Destroy(GameObject.Find("WalkObject"));
                        PictureCreater.instance.ListRolePicture[PictureCreater.instance.ListSequence[0].RoleIndex].RoleSkillPoint += 300; //按防御
                        PictureCreater.instance.ListRolePicture[PictureCreater.instance.ListSequence[0].RoleIndex].RoleObject.GetComponent<ColliderDisplayText>().SetSkillPoint(PictureCreater.instance.ListRolePicture[PictureCreater.instance.ListSequence[0].RoleIndex].RoleSkillPoint);
                        PictureCreater.instance.IsLock = false;
                        PictureCreater.instance.AddSequence();
                    }
                }
            };
        }
        #endregion
        #region QuitButton
        if (UIEventListener.Get(GameObject.Find("QuitButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("QuitButton")).onClick += delegate(GameObject go)
            {
                PictureCreater.instance.ResetRandom();
                Time.timeScale = 1;
                //if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == -1)
                {
                    if (PictureCreater.instance.FightStyle == 0)
                    {
                        PictureCreater.instance.StopFight(true);
                        UIManager.instance.BackTwoUI("MapUiWindow");
                    }
                    else if (PictureCreater.instance.FightStyle == 2)
                    {
                        NetworkHandler.instance.SendProcess("1501#;");
                        //UIManager.instance.BackUI();
                        PictureCreater.instance.StopFight(false);
                    }
                    else if (PictureCreater.instance.FightStyle == 15)
                    {
                        PictureCreater.instance.StopFight(true);
                        UIManager.instance.BackTwoUI("ConquerWindow");
                        if (GameObject.Find("ConquerWindow") != null && CharacterRecorder.instance.TabeID != 0)
                        {
                            NetworkHandler.instance.SendProcess("6502#" + CharacterRecorder.instance.TabeID + ";");
                            GameObject.Find("ConquerWindow").GetComponent<ConquerWindow>().CheckGateWindow.SetActive(false);
                            GameObject.Find("ConquerWindow").GetComponent<ConquerWindow>().HarvestWindow.SetActive(true);
                        }
                        else
                        {
                            NetworkHandler.instance.SendProcess("6501#;");
                        }
                    }
                    else
                    {
                        UIManager.instance.BackUI();
                        PictureCreater.instance.StopFight(true);
                    }

                }
            };
        }
        #endregion
        Pause.SetActive(false);
        #region BackButton
        if (UIEventListener.Get(GameObject.Find("BackButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("BackButton")).onClick += delegate(GameObject go)
            {
                PictureCreater.instance.ResetRandom();
                bool isBack = false;
                SceneTransformer.instance.NewGuideButtonClick();
                if (SceneTransformer.instance.CheckGuideIsFinish())
                {
                    if (CharacterRecorder.instance.GuideID[0] == 3)
                    {
                        isBack = true;
                    }
                }
                if ((PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) != 3 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) != 18) && (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) != 6 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) != 12) && isBack == false)
                {
                    if (PictureCreater.instance.FightStyle == 0)
                    {
                        //  UIManager.instance.OpenPanel("MapUiWindow", true);
                        //  CharacterRecorder.instance.enterMapFromMain = true; 
                        for (int i = 0; i < 2 * CharacterRecorder.instance.isReStartTimes + 1; i++)
                        {
                            UIManager.instance.BackUI();
                        }
                        CharacterRecorder.instance.isReStartTimes = 0;
                        PictureCreater.instance.StopFight(true);
                    }
                    else if (PictureCreater.instance.FightStyle == 2)
                    {
                        NetworkHandler.instance.SendProcess("1501#;");
                        //UIManager.instance.BackUI();
                        PictureCreater.instance.StopFight(false);
                    }
                    else if (PictureCreater.instance.FightStyle == 12)
                    {
                        for (int i = 0; i < 2 * CharacterRecorder.instance.isReStartTimes + 1 + 1; i++)
                        {
                            UIManager.instance.BackUI();
                        }
                        CharacterRecorder.instance.isReStartTimes = 0;
                        PictureCreater.instance.StopFight(true);
                    }
                    else if (PictureCreater.instance.FightStyle == 15)
                    {
                        PictureCreater.instance.StopFight(true);
                        UIManager.instance.BackTwoUI("ConquerWindow");
                        if (GameObject.Find("ConquerWindow") != null && CharacterRecorder.instance.TabeID != 0)
                        {
                            NetworkHandler.instance.SendProcess("6502#" + CharacterRecorder.instance.TabeID + ";");
                            GameObject.Find("ConquerWindow").GetComponent<ConquerWindow>().CheckGateWindow.SetActive(false);
                            GameObject.Find("ConquerWindow").GetComponent<ConquerWindow>().HarvestWindow.SetActive(true);
                        }
                        else
                        {
                            NetworkHandler.instance.SendProcess("6501#;");
                        }
                    }
                    else
                    {
                        //   UIManager.instance.BackUI();
                        for (int i = 0; i < 2 * CharacterRecorder.instance.isReStartTimes + 1; i++)
                        {
                            UIManager.instance.BackUI();
                        }
                        CharacterRecorder.instance.isReStartTimes = 0;
                        PictureCreater.instance.StopFight(true);
                    }

                }
            };
        }
        #endregion
        #region ChangeButton
        if (UIEventListener.Get(GameObject.Find("ChangeButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("ChangeButton")).onClick += delegate(GameObject go)
            {
                if (PictureCreater.instance.IsAuto)
                {
                    PictureCreater.instance.IsAuto = false;
                    if (PictureCreater.instance.FightStyle == 2)
                    {
                        LabelChange.text = "手动操作";
                    }
                    else
                    {
                        LabelChange.text = "手动战术";
                    }
                }
                else
                {
                    PictureCreater.instance.IsAuto = true;
                    if (PictureCreater.instance.FightStyle == 2)
                    {
                        LabelChange.text = "自动操作";
                    }
                    else
                    {
                        LabelChange.text = "自动战术";
                    }
                }
            };
        }
        #endregion
        //Debug.LogError(PlayerPrefs.GetInt("Hand"));
        //Debug.LogError(PlayerPrefs.GetInt("Speed"));
        if (PlayerPrefs.GetInt("Hand") == 0)
        {
            PictureCreater.instance.IsHand = true;
            if (PictureCreater.instance.FightStyle == 2)
            {
                PictureCreater.instance.IsLock = true;
            }
            HandButton.GetComponent<UISprite>().spriteName = "11button";
            if (PictureCreater.instance.FightStyle == 2)
            {
                LabelChange.text = "手动操作";
            }
            else
            {
                LabelChange.text = "手动战术";
            }
        }
        else
        {
            PictureCreater.instance.IsHand = false;
            if (PictureCreater.instance.FightStyle == 2)
            {
                PictureCreater.instance.IsLock = false;
            }
            HandButton.GetComponent<UISprite>().spriteName = "12button";
            if (PictureCreater.instance.FightStyle == 2)
            {
                LabelChange.text = "自动操作";
            }
            else
            {
                LabelChange.text = "自动战术";
            }
        }

        if (PlayerPrefs.GetInt("Speed") == 0)
        {
            PictureCreater.instance.HardLevel = 1;
            SpeedButton.GetComponent<UISprite>().spriteName = "11button";
            GameObject.Find("LabelSpeed").GetComponent<UILabel>().text = "X1";
        }
        else
        {
            PictureCreater.instance.HardLevel = 1.7f;
            SpeedButton.GetComponent<UISprite>().spriteName = "12button";
            GameObject.Find("LabelSpeed").GetComponent<UILabel>().text = "X2";
        }
        #region SpeedButton
        UIEventListener.Get(SpeedButton).onClick += delegate(GameObject go)
        {
            if (PictureCreater.instance.FightStyle == 6 || PictureCreater.instance.FightStyle == 7)
            {
                return;
            }
            if (CharacterRecorder.instance.lastGateID > 10005 && CharacterRecorder.instance.lastGateID != 99999)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                
                if (PictureCreater.instance.HardLevel > 1)
                {
                    PictureCreater.instance.HardLevel = 1;
                }
                else
                {
                    PictureCreater.instance.HardLevel = 1.7f;
                }
                Time.timeScale = PictureCreater.instance.HardLevel;

                if (PictureCreater.instance.HardLevel == 1)
                {
                    GameObject.Find("LabelSpeed").GetComponent<UILabel>().text = "X1";
                    SpeedButton.GetComponent<UISprite>().spriteName = "11button";
                    PlayerPrefs.SetInt("Speed", 0);
                }
                else
                {
                    GameObject.Find("LabelSpeed").GetComponent<UILabel>().text = "X2";
                    SpeedButton.GetComponent<UISprite>().spriteName = "12button";
                    PlayerPrefs.SetInt("Speed", 1);
                }
            }
            else
            {
                UIManager.instance.OpenPromptWindow("战斗加速通关第6关后开启", PromptWindow.PromptType.Hint, null, null);
            }
        };
        #endregion
        #region HandButton
        UIEventListener.Get(HandButton).onClick += delegate(GameObject go)
        {
            if (PictureCreater.instance.FightStyle == 1 || PictureCreater.instance.FightStyle == 3 || PictureCreater.instance.FightStyle == 6 || PictureCreater.instance.FightStyle == 7 || PictureCreater.instance.FightStyle == 12 || PictureCreater.instance.FightStyle == 13 || PictureCreater.instance.FightStyle == 14 || PictureCreater.instance.FightStyle == 15 || PictureCreater.instance.FightStyle == 16)
            {
                return;
            }
            if (CharacterRecorder.instance.lastGateID > 10005)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                if (PictureCreater.instance.IsHand)
                {
                    PictureCreater.instance.IsHand = false;
                    if (PictureCreater.instance.FightStyle == 2)
                    {
                        PictureCreater.instance.IsLock = false;
                    }
                    HandButton.GetComponent<UISprite>().spriteName = "12button";
                    PlayerPrefs.SetInt("Hand", 1);

                    if (PictureCreater.instance.FightStyle == 2)
                    {
                        LabelChange.text = "自动操作";
                    }
                    else
                    {
                        LabelChange.text = "自动战术";
                    }
                }
                else
                {
                    PictureCreater.instance.IsHand = true;
                    if (PictureCreater.instance.FightStyle == 2)
                    {
                        if (!PictureCreater.instance.ListSequence[0].IsMonster)
                        {
                            PictureCreater.instance.IsLock = true;
                        }
                    }
                    HandButton.GetComponent<UISprite>().spriteName = "11button";
                    PlayerPrefs.SetInt("Hand", 0);

                    if (PictureCreater.instance.FightStyle == 2)
                    {
                        LabelChange.text = "手动操作";

                        PictureCreater.instance.ShowXingDong();
                    }
                    else
                    {
                        LabelChange.text = "手动战术";
                    }
                }
            }
            else
            {
                UIManager.instance.OpenPromptWindow("自动战术通关第6关后开启", PromptWindow.PromptType.Hint, null, null);
            }
        };
        #endregion
        #region AutoButton
        UIEventListener.Get(GameObject.Find("AutoButton")).onClick += delegate(GameObject go)
        {
            if (CharacterRecorder.instance.lastGateID == 10006)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1803);
            }
            else if (CharacterRecorder.instance.lastGateID == 10007)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1903);
            }
            else if (CharacterRecorder.instance.lastGateID == 10008)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2104);
            }
            else if (CharacterRecorder.instance.lastGateID == 10010)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2402);
            }
            else if (CharacterRecorder.instance.lastGateID == 10011 && CharacterRecorder.instance.lastCreamGateID >= 20002)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2604);
            }
            else if (CharacterRecorder.instance.lastGateID == 10021)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_3801);
            }
            else if (PictureCreater.instance.FightStyle == 6)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2809);
            }
            else if (PictureCreater.instance.FightStyle == 4)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_3306);
            }
            else if (PictureCreater.instance.FightStyle == 5)
            {
                UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_3405);
            }
            UIManager.instance.StartGate(UIManager.GateAnchorName.startFight);
            UIManager.instance.UmengStartLevel(SceneTransformer.instance.NowGateID.ToString());
            if (SceneTransformer.instance.CheckGuideIsFinish() == false)
            {
                if ((PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 0 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 3)
                    || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 3 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 15)
                    || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 6 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 6)
                    || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 10 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 5)
                    || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 12 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 7)
                    )
                {
                    SceneTransformer.instance.NewGuideButtonClick();
                }
            }
            else
            {
                if (CharacterRecorder.instance.GuideID[0] == 3 || CharacterRecorder.instance.GuideID[35] == 2 || CharacterRecorder.instance.GuideID[27] == 3 || CharacterRecorder.instance.GuideID[37] == 2 || CharacterRecorder.instance.GuideID[10] == 10)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
            }
            bool isStart = false;
            if (SceneTransformer.instance.CheckGuideIsFinish())
            {
                if (CharacterRecorder.instance.GuideID[21] == 9)
                {
                    CharacterRecorder.instance.GuideID[21] += 1;
                }
                else if (CharacterRecorder.instance.GuideID[9] == 13)
                {
                    CharacterRecorder.instance.GuideID[9] = 14;
                }
                if (CharacterRecorder.instance.GuideID[0] == 3)
                {
                    isStart = true;
                }
            }
            if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 3 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 13)
            {

            }
            else if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 6 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 6)
            {

            }
            else if (isStart == true)
            {

            }
            else
            {
                StartCoroutine(StartFight());
                if (PictureCreater.instance.FightStyle == 0 && !NetworkHandler.instance.IsCreate)
                {
                    //ShrinkMessage.SetActive(true);
                    //GateDescription.GetComponent<TweenPosition>().PlayForward();
                    ShrinkMessage.SetActive(false);
                }
            }

            if (ObscuredPrefs.GetString("Account").IndexOf("test_") > -1)
            {
                GameTest.SetActive(true);
            }
        };
        #endregion

        #region AKeyButton
        if (UIEventListener.Get(GameObject.Find("AKeyButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("AKeyButton")).onClick += delegate(GameObject go)
            {
                if (SceneTransformer.instance.CheckGuideIsFinish() == false)
                {

                }
                else
                {
                    bool IsCaptain = false;
                    for (int i = 0; i < PictureCreater.instance.ListRolePicture.Count; i++)
                    {
                        if (PictureCreater.instance.ListRolePicture[i].RolePosition > 0)
                        {
                            IsCaptain = true;
                        }
                    }

                    int PositionCount = 0;
                    List<int> PositionFList = new List<int>();
                    List<int> PositionSList = new List<int>();

                    PositionFList.Add(18);
                    PositionFList.Add(21);
                    PositionFList.Add(15);
                    PositionFList.Add(17);
                    PositionFList.Add(14);
                    PositionFList.Add(13);
                    PositionFList.Add(16);
                    PositionFList.Add(10);
                    PositionFList.Add(12);
                    PositionFList.Add(9);

                    PositionSList.Add(13);
                    PositionSList.Add(16);
                    PositionSList.Add(10);
                    PositionSList.Add(12);
                    PositionSList.Add(9);
                    PositionSList.Add(18);
                    PositionSList.Add(21);
                    PositionSList.Add(15);
                    PositionSList.Add(17);
                    PositionSList.Add(14);
                    for (int i = 0; i < PictureCreater.instance.ListRolePicture.Count; i++)
                    {
                        if (PictureCreater.instance.ListRolePicture[i].RolePosition > 0 && !PictureCreater.instance.ListRolePicture[i].RolePictureNPC)
                        {
                            PositionCount++;
                            switch (PictureCreater.instance.ListRolePicture[i].RolePosition)
                            {
                                case 18:
                                    PositionFList.Remove(18);
                                    PositionSList.Remove(18);
                                    break;
                                case 21:
                                    PositionFList.Remove(21);
                                    PositionSList.Remove(21);
                                    break;
                                case 15:
                                    PositionFList.Remove(15);
                                    PositionSList.Remove(15);
                                    break;
                                case 17:
                                    PositionFList.Remove(17);
                                    PositionSList.Remove(17);
                                    break;
                                case 14:
                                    PositionFList.Remove(14);
                                    PositionSList.Remove(14);
                                    break;
                                case 13:
                                    PositionSList.Remove(13);
                                    PositionFList.Remove(13);
                                    break;
                                case 16:
                                    PositionSList.Remove(16);
                                    PositionFList.Remove(16);
                                    break;
                                case 10:
                                    PositionSList.Remove(10);
                                    PositionFList.Remove(10);
                                    break;
                                case 12:
                                    PositionSList.Remove(12);
                                    PositionFList.Remove(12);
                                    break;
                                case 9:
                                    PositionSList.Remove(9);
                                    PositionFList.Remove(9);
                                    break;
                            }
                        }
                    }

                    PositionCount = PictureCreater.instance.LimitHeroCount == 0 ? (CharacterRecorder.instance.level < 25 ? 5 : 6) - PositionCount : PictureCreater.instance.LimitHeroCount - PositionCount;

                    List<int> RemoveList = new List<int>();
                    for (int i = 0; i < PositionCount; i++)
                    {
                        //if (ListHero[i].name == RoleID.ToString())

                        for (int j = 0; j < PictureCreater.instance.ListRolePicture.Count; j++)
                        {
                            if (ListHero.Count > i)
                            {
                                if (PictureCreater.instance.ListRolePicture[j].RoleID.ToString() == ListHero[i].name)
                                {

                                    //if (hi.heroCarrerType == PictureCreater.instance.LimitType)
                                    //{
                                    //    continue;
                                    //}
                                    //if (hi.heroCarrerType != PictureCreater.instance.PermissionType && PictureCreater.instance.PermissionType != 0)
                                    //{
                                    //    continue;
                                    //}
                                    //if (h.cardID == PictureCreater.instance.LimitHeroID)
                                    //{
                                    //    continue;
                                    //}
                                    if (PictureCreater.instance.ListRolePicture[j].RoleNowBlood > 0 && PictureCreater.instance.LimitType != PictureCreater.instance.ListRolePicture[j].RoleCareer)
                                    {
                                        PictureCreater.instance.ListRolePicture[j].RoleObject.SetActive(true);
                                        if (PictureCreater.instance.ListRolePicture[j].RoleArea == 1)
                                        {
                                            PictureCreater.instance.ListRolePicture[j].RolePosition = PositionFList[0];
                                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.position = PictureCreater.instance.ListPosition[PositionFList[0]];
                                            PositionFList.RemoveAt(0);
                                        }
                                        else
                                        {
                                            PictureCreater.instance.ListRolePicture[j].RolePosition = PositionSList[0];
                                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.position = PictureCreater.instance.ListPosition[PositionSList[0]];
                                            PositionSList.RemoveAt(0);
                                        }
                                        RemoveList.Add(i);
                                    }
                                    else
                                    {
                                        PositionCount++;
                                    }
                                    break;
                                }
                            }
                        }
                    }

                    for (int i = RemoveList.Count - 1; i >= 0; i--)
                    {
                        DestroyImmediate(ListHero[RemoveList[i]]);
                        ListHero.RemoveAt(RemoveList[i]);
                    }



                    if (!IsCaptain)
                    {
                        for (int i = 0; i < PictureCreater.instance.ListRolePicture.Count; i++)
                        {
                            if (PictureCreater.instance.ListRolePicture[i].RolePosition == 18)
                            {
                                PictureCreater.instance.SetCaptain(PictureCreater.instance.ListRolePicture[i].RoleObject.name);
                                PictureCreater.instance.SelectPosition = 18;
                                break;
                            }
                        }
                    }
                    MyGrid.GetComponent<UIGrid>().Reposition();
                    PictureCreater.instance.SetSequence();
                }
            };
        }
        #endregion

        ExitButton.SetActive(false);
        GameObject.Find("ChangeButton").SetActive(false);
        GameObject.Find("HandCancelButton").SetActive(false);
        #region   NetworkHandler.instance.IsCreate
        if (!NetworkHandler.instance.IsCreate)
        {
            SkillButton1.transform.Find("Sprite").gameObject.GetComponent<UISprite>().spriteName = CharacterRecorder.instance.ListManualSkillId[0].ToString();
            SkillButton2.transform.Find("Sprite").gameObject.GetComponent<UISprite>().spriteName = CharacterRecorder.instance.ListManualSkillId[1].ToString();
            SkillButton3.transform.Find("Sprite").gameObject.GetComponent<UISprite>().spriteName = CharacterRecorder.instance.ListManualSkillId[2].ToString();

            SkillButton1.SetActive(false);
            SkillButton2.SetActive(false);
            SkillButton3.SetActive(false);
            TacticsInfo.SetActive(false);

            int i = 0;
            int k = 0;
            TextTranslator.Gate g = TextTranslator.instance.GetGateByID(SceneTransformer.instance.NowGateID);
            int RoleIndex = -1;
            if (PictureCreater.instance.FightStyle == 0 || PictureCreater.instance.FightStyle == 4 || PictureCreater.instance.FightStyle == 5 || PictureCreater.instance.FightStyle == 6 || PictureCreater.instance.FightStyle == 7 || PictureCreater.instance.FightStyle == 8 || PictureCreater.instance.FightStyle == 9 || PictureCreater.instance.FightStyle == 10 || PictureCreater.instance.FightStyle == 11)
            {
                if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) < 4)
                {
                    Hero h = new Hero(-1, 60016, 0, 0, 0, 0, 0, 0);
                    h.SetHeroProperty(500, 1, 0, 0, 0, 0, 1, 1, 0, 3000, 200, 100, 0, 0, 0, 1000, 0, 0, 115, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0);
                    CharacterRecorder.instance.ownedHeroList.Add(h);
                }

                foreach (var h in CharacterRecorder.instance.ownedHeroList)
                {
                    HeroInfo hi = TextTranslator.instance.GetHeroInfoByHeroID(h.cardID);
                    if (hi.heroCarrerType == PictureCreater.instance.LimitType)
                    {
                        continue;
                    }
                    if (hi.heroCarrerType != PictureCreater.instance.PermissionType && PictureCreater.instance.PermissionType != 0)
                    {
                        continue;
                    }
                    if (h.cardID == PictureCreater.instance.LimitHeroID)
                    {
                        continue;
                    }


                    string[] dataSplit = PictureCreater.instance.PVEPosition.Split('!');
                    bool IsPosition = false;
                    bool IsRepeat = false;
                    int Position = 0;
                    for (int j = 0; j < dataSplit.Length - 1; j++)
                    {
                        string[] secSplit = dataSplit[j].Split('$');
                        if (int.Parse(secSplit[0]) == h.characterRoleID)
                        {
                            IsPosition = true;
                            Position = int.Parse(secSplit[1]);

                            if (j == dataSplit.Length - 2)
                            {
                                PictureCreater.instance.SelectPosition = Position;
                            }
                        }
                    }

                    ////////////////////////新手引导(以下)////////////////////////
                    if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) < 4)
                    {
                        if (h.cardID == 60026)
                        {
                            IsPosition = true;
                            Position = 14;
                        }
                        else if (h.cardID == 60032)
                        {
                            IsPosition = true;
                            Position = 17;
                        }
                        else if (h.cardID == 60016)
                        {
                            IsPosition = false;
                            Position = 0;
                        }
                    }
                    else if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) < 7)
                    {

                        if (h.cardID == 60026)
                        {
                            IsPosition = true;
                            Position = 14;
                        }
                        else if (h.cardID == 60032)
                        {
                            IsPosition = true;
                            Position = 17;
                        }
                        else if (h.cardID == 60016)
                        {
                            IsPosition = true;
                            Position = 18;
                        }

                    }
                    else if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) < 9)
                    {
                        if (h.cardID == 60026)
                        {
                            IsPosition = true;
                            Position = 14;
                        }
                        else if (h.cardID == 60032)
                        {
                            IsPosition = true;
                            Position = 21;
                        }
                        else if (h.cardID == 60016)
                        {
                            IsPosition = true;
                            Position = 18;
                        }
                    }
                    else if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) < 13)
                    {
                        if (h.cardID == 60026)
                        {
                            IsPosition = true;
                            Position = 14;
                        }
                        else if (h.cardID == 60032)
                        {
                            IsPosition = true;
                            Position = 21;
                        }
                        else if (h.cardID == 60016)
                        {
                            IsPosition = true;
                            Position = 18;
                        }
                    }
                    else if (CharacterRecorder.instance.GuideID[0] == 0 && SceneTransformer.instance.NowGateID == 10005)
                    {
                        if (h.cardID == 60026)
                        {
                            IsPosition = true;
                            Position = 14;
                        }
                        else if (h.cardID == 60032)
                        {
                            IsPosition = true;
                            Position = 21;
                        }
                        else if (h.cardID == 60016)
                        {
                            IsPosition = true;
                            Position = 18;
                        }
                        else if (h.cardID == 60028)
                        {
                            IsPosition = true;
                            Position = 13;
                        }
                        else
                        {
                            IsPosition = false;
                            Position = 0;
                        }
                    }
                    else if (PictureCreater.instance.FightStyle == 8)
                    {
                        IsPosition = false;
                        if (g.itemID1 == h.cardID + 10000)
                        {
                            IsRepeat = true;
                            k++;
                        }
                        else if (g.itemID2 == h.cardID + 10000)
                        {
                            IsRepeat = true;
                            k++;
                        }
                        else if (g.itemID3 == h.cardID + 10000)
                        {
                            IsRepeat = true;
                            k++;
                        }
                        else if (g.itemID4 == h.cardID + 10000)
                        {
                            IsRepeat = true;
                            k++;
                        }
                        else if (g.itemID5 == h.cardID + 10000)
                        {
                            IsRepeat = true;
                            k++;
                        }
                        else
                        {
                            Position = 0;
                        }
                        if (IsRepeat)
                        {
                            if (k == 1)
                            {
                                Position = 18;
                            }
                            else if (k == 2)
                            {
                                Position = 14;
                            }
                            else if (k == 3)
                            {
                                Position = 17;
                            }
                            else if (k == 4)
                            {
                                Position = 21;
                            }
                            else if (k == 5)
                            {
                                Position = 15;
                            }
                        }
                    }
                    ////////////////////////新手引导(以上)////////////////////////

                    if (PictureCreater.instance.LimitHeroCount > 0)
                    {
                        Position = 0;
                        IsPosition = false;
                    }

                    int SkillID = TextTranslator.instance.GetHeroInfoByHeroID(h.cardID).heroSkillList[0];
                    int SkillID2 = TextTranslator.instance.GetHeroInfoByHeroID(h.cardID).heroSkillList[1];
                    if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 3 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 11)//判断用于新手引导第一关没有男枪时候的问题
                    {
                        PictureCreater.instance.CreateRole(h.cardID, h.name, Position, Color.cyan, (PictureCreater.instance.LimitBlood > 0) ? PictureCreater.instance.LimitBlood : (h.HP + h.HPAdd), (PictureCreater.instance.LimitBlood > 0) ? PictureCreater.instance.LimitBlood : (h.HP + h.HPAdd), h.hit + h.hitAdd, h.aspd / 10f, false, false, 1, 1.5f, h.dodge + h.dodgeAdd, "R" + h.characterRoleID.ToString(), h.characterRoleID, (int)((h.strength + h.strengthAdd)), h.physicalCrit + h.physicalCritAdd, (h.physicalDefense + h.physicalDefenseAdd) / 4f, h.UNphysicalCrit + h.UNphysicalCritAdd, h.moreDamige + h.moreDamigeAdd, h.avoidDamige + h.avoidDamigeAdd, SkillID, SkillID2, h.force, h.skillLevel, 0, h.area, h.move, h.skillPoint, "");
                    }
                    else
                    {
                        PictureCreater.instance.CreateRole(h.cardID, h.name, Position, Color.cyan, (PictureCreater.instance.LimitBlood > 0) ? PictureCreater.instance.LimitBlood : (h.HP + h.HPAdd), (PictureCreater.instance.LimitBlood > 0) ? PictureCreater.instance.LimitBlood : (h.HP + h.HPAdd), h.hit + h.hitAdd, h.aspd / 10f, false, false, 1, 1.5f, h.dodge + h.dodgeAdd, "R" + h.characterRoleID.ToString(), h.characterRoleID, (int)((h.strength + h.strengthAdd)), h.physicalCrit + h.physicalCritAdd, (h.physicalDefense + h.physicalDefenseAdd) / 4f, h.UNphysicalCrit + h.UNphysicalCritAdd, h.moreDamige + h.moreDamigeAdd, h.avoidDamige + h.avoidDamigeAdd, SkillID, SkillID2, h.force, h.skillLevel, h.WeaponList[0].WeaponClass, h.area, h.move, h.skillPoint, TextTranslator.instance.HeroInnateList[h.characterRoleID]);
                    }
                    h.position = Position;
                    if (IsPosition || IsRepeat)
                    {
                        //SetRoleVisible(h.cardID, false);
                        if (Position == PictureCreater.instance.SelectPosition)
                        {
                            RoleIndex = i;
                            PictureCreater.instance.ListRolePicture[RoleIndex].RoleCaptain = true;
                        }
                    }
                    else
                    {
                        GameObject go = NGUITools.AddChild(MyGrid, MyHeroItem);
                        go.name = h.cardID.ToString();
                        go.AddComponent<RoleHeroItem>();
                        go.GetComponent<RoleHeroItem>().Init(h.cardID, h.characterRoleID, i);
                        go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("ScrollView").GetComponent<UIScrollView>();

                        ListHero.Add(go);
                        //SetRoleVisible(h.cardID, true);
                    }
                    i++;
                }
            }
            else if (PictureCreater.instance.FightStyle != 2 && PictureCreater.instance.FightStyle != 14)
            {
                string HeroPosition = "";
                if (PictureCreater.instance.FightStyle == 1 || PictureCreater.instance.FightStyle == 3 || PictureCreater.instance.FightStyle == 13 || PictureCreater.instance.FightStyle == 16 || PictureCreater.instance.FightStyle == 17)
                {
                    if (PictureCreater.instance.PVPPosition.Split('!').Length <= 2)
                    {
                        //PictureCreater.instance.PVPPosition = PictureCreater.instance.PVEPosition;
                        PictureCreater.instance.PVPPosition = "";
                    }
                    else
                    {
                        PictureCreater.instance.ChangePvpPosition(false);
                    }
                    HeroPosition = PictureCreater.instance.PVPPosition;
                }
                else if (PictureCreater.instance.FightStyle == 12)
                {
                    HeroPosition = PictureCreater.instance.InstancePosition;
                }
                else if (PictureCreater.instance.FightStyle == 14)
                {
                    HeroPosition = PictureCreater.instance.LegionPosition;
                }

                int j = 0;
                foreach (var h in CharacterRecorder.instance.ownedHeroList)
                {
                    string[] dataSplit = HeroPosition.Split('!');
                    bool IsPosition = false;
                    int Position = 0;
                    for (i = 0; i < dataSplit.Length - 1; i++)
                    {
                        string[] secSplit = dataSplit[i].Split('$');
                        //rw.SetTeamInfo(i, int.Parse(secSplit[0]), int.Parse(secSplit[1]));
                        if (int.Parse(secSplit[0]) == h.characterRoleID)
                        {
                            IsPosition = true;
                            Position = int.Parse(secSplit[1]);

                            if (i == dataSplit.Length - 2)
                            {
                                PictureCreater.instance.SelectPosition = Position;
                            }
                        }
                    }

                    int Area = h.area;
                    if (PictureCreater.instance.FightStyle == 6 || PictureCreater.instance.FightStyle == 7)
                    {
                        if (Area != 1022)
                        {
                            Area = 99;
                        }
                    }

                    int SkillID = TextTranslator.instance.GetHeroInfoByHeroID(h.cardID).heroSkillList[0];
                    int SkillID2 = TextTranslator.instance.GetHeroInfoByHeroID(h.cardID).heroSkillList[1];
                    PictureCreater.instance.CreateRole(h.cardID, h.name, Position, Color.cyan, (h.HP + h.HPAdd), (h.HP + h.HPAdd), h.hit + h.hitAdd, h.aspd / 10f, false, false, 1, 1.5f, h.dodge + h.dodgeAdd, "R" + h.characterRoleID.ToString(), h.characterRoleID, (int)((h.strength + h.strengthAdd)), h.physicalCrit + h.physicalCritAdd, (h.physicalDefense + h.physicalDefenseAdd) / 4f, h.UNphysicalCrit + h.UNphysicalCritAdd, h.moreDamige + h.moreDamigeAdd, h.avoidDamige + h.avoidDamigeAdd, SkillID, SkillID2, h.force, h.skillLevel, h.WeaponList[0].WeaponClass, Area, h.move, h.skillPoint, "");
                    h.position = Position;


                    if (Position == PictureCreater.instance.SelectPosition)
                    {
                        RoleIndex = j;
                        PictureCreater.instance.ListRolePicture[RoleIndex].RoleCaptain = true;
                    }

                    if (!IsPosition)
                    {
                        GameObject go = NGUITools.AddChild(MyGrid, MyHeroItem);
                        go.name = h.cardID.ToString();
                        go.AddComponent<RoleHeroItem>();
                        go.GetComponent<RoleHeroItem>().Init(h.cardID, h.characterRoleID, i);
                        go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("ScrollView").GetComponent<UIScrollView>();

                        ListHero.Add(go);
                    }
                    j++;
                }
            }
        #endregion

            //if (Count >= 5)
            //{
            //    PictureCreater.instance.SelectPosition = PictureCreater.instance.ListRolePicture[RoleIndex].RolePosition;
            //    PictureCreater.instance.SelectName = PictureCreater.instance.ListRolePicture[RoleIndex].RoleObject.name;
            if (RoleIndex != -1)
            {
                PictureCreater.instance.ShowPosition(PictureCreater.instance.ListRolePicture[RoleIndex].RoleObject.name, Vector3.zero, PictureCreater.instance.ListRolePicture[RoleIndex].RolePosition, false);
                PictureCreater.instance.SetCaptain(PictureCreater.instance.ListRolePicture[RoleIndex].RoleObject.name);
                //PictureCreater.instance.ListRolePicture[RoleIndex].RoleObject.GetComponent<ColliderDisplayText>().SetButton(true);
            }
            //}
            //else
            //{
            //    PictureCreater.instance.SelectPosition = 11;
            //    while (PictureCreater.instance.CheckPosition(PictureCreater.instance.SelectPosition))
            //    {
            //        PictureCreater.instance.SelectPosition--;
            //    }
            //    PictureCreater.instance.ShowPosition("", Vector3.zero, PictureCreater.instance.SelectPosition, false);
            //}
            MyGrid.GetComponent<UIGrid>().Reposition();

            if (PictureCreater.instance.FightStyle == 2)
            {
                GateDescription.SetActive(false);
            }
            //星级显示
            if (PictureCreater.instance.FightStyle == 0 || PictureCreater.instance.FightStyle == 4 || PictureCreater.instance.FightStyle == 5)
            {
                AudioEditer.instance.PlayLoop("Fight" + Random.Range(1, 5).ToString());
                if (PictureCreater.instance.IsRemember)
                {
                    StartCoroutine(ShowDescription());
                }
            }
            else if (PictureCreater.instance.FightStyle == 6 || PictureCreater.instance.FightStyle == 7)
            {
                GateDescription.SetActive(false);
                AudioEditer.instance.PlayLoop("Car");
            }
            else if (PictureCreater.instance.FightStyle == 2)
            {
                GateDescription.SetActive(false);
                AudioEditer.instance.PlayLoop("Wood");
            }
            else
            {
                GateDescription.SetActive(false);
                AudioEditer.instance.PlayLoop("PVP");
            }
        }
        HandSkill.SetActive(false);
        Tactics.SetActive(false);
        TacticsInfo.SetActive(false);

        if (CharacterRecorder.instance.lastGateID < 10008)
        {
            BackButton.SetActive(false);
        }
        if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 10 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 4)
        {
            FightMask.SetActive(true);
            StartCoroutine(ShowSequenceNum());
        }
        else
        {
            FightMask.SetActive(false);
        }
        if (SceneTransformer.instance.CheckGuideIsFinish())
        {
            StartCoroutine(SceneTransformer.instance.NewbieGuide());// 新手引导;
        }
        if (PictureCreater.instance.FightStyle == 1 || PictureCreater.instance.FightStyle == 3 || PictureCreater.instance.FightStyle == 6 || PictureCreater.instance.FightStyle == 7 || PictureCreater.instance.FightStyle == 8 || PictureCreater.instance.FightStyle == 9 || PictureCreater.instance.FightStyle == 10 || PictureCreater.instance.FightStyle == 11 || PictureCreater.instance.FightStyle == 12 || PictureCreater.instance.FightStyle == 13 || PictureCreater.instance.FightStyle == 14 || PictureCreater.instance.FightStyle == 15 || PictureCreater.instance.FightStyle == 16 || PictureCreater.instance.FightStyle == 17 || CharacterRecorder.instance.lastGateID < 10006)
        {
            UISprite _obj = NGUITools.AddSprite(HandButton, NomalAtlas, "zhandoux1x2_icon1");
            PictureCreater.instance.IsHand = true;
            HandButton.GetComponent<UISprite>().spriteName = "11button";
            _obj.MakePixelPerfect();
            _obj.transform.localPosition = new Vector3(-40, 14, 0);


        }

        if (PictureCreater.instance.FightStyle == 6 || PictureCreater.instance.FightStyle == 7 || CharacterRecorder.instance.lastGateID < 10006)
        {
            UISprite _obj1 = NGUITools.AddSprite(SpeedButton, NomalAtlas, "zhandoux1x2_icon1");
            SpeedButton.GetComponent<UISprite>().spriteName = "11button";
            _obj1.MakePixelPerfect();
            _obj1.transform.localPosition = new Vector3(-40, 14, 0);
        }

        if (SkillName2 == "0")
        {
            Tactics.transform.GetChild(1).GetChild(6).gameObject.SetActive(false);
        }
        if (SkillName3 == "0")
        {
            Tactics.transform.GetChild(0).GetChild(6).gameObject.SetActive(false);
        }


        NGUITools.SetLayer(SkillEffect, 11);
        if (!PictureCreater.instance.IsRemember)
        {
            GameObject.Find("Bottom").SetActive(false);
            ExitButton.SetActive(false);
            GameObject.Find("AutoButton").SetActive(false);
            BackButton.SetActive(false);
        }
    }


    public void SetTerrainInfo(string Name, string Desc, string Icon)
    {
        InfoBoard.transform.Find("LabelName").gameObject.GetComponent<UILabel>().text = Name;
        InfoBoard.transform.Find("LabelDes").gameObject.GetComponent<UILabel>().text = Desc;
        InfoBoard.transform.Find("SpriteIcon").gameObject.GetComponent<UISprite>().spriteName = Icon;
    }

    IEnumerator ShowDescription()
    {
        LabelVictory1.text = "";
        LabelVictory2.text = "";
        LabelVictory3.text = "";
        LabelVictory.text = "";
        RedLabel.text = "";
        string LabelVictoryText = "";
        string LabelVictoryText1 = "";
        string LabelVictoryText2 = "";
        string LabelVictoryText3 = "";
        TextTranslator.Gate g = TextTranslator.instance.GetGateByID(SceneTransformer.instance.NowGateID);
        if (g.scriptID1 > 0)
        {
            GateLimit gl = TextTranslator.instance.GetGateLimitDicByID(g.scriptID1);
            if (gl.LimitTerm == 0)
            {
                GateDescription.transform.Find("SpriteXianding").gameObject.SetActive(false);
            }
            yield return new WaitForSeconds(1.5f);

            if (gl.Highlight == 1)
            {
                isRedMessage = true;
                LabelVictory1.gameObject.GetComponent<TweenColor>().enabled = true;
            }
            else if (gl.Highlight == 2)
            {
                isRedMessage = true;
                LabelVictory2.gameObject.GetComponent<TweenColor>().enabled = true;
            }
            else if (gl.Highlight == 3)
            {
                isRedMessage = true;
                LabelVictory3.gameObject.GetComponent<TweenColor>().enabled = true;
            }
            switch (gl.LimitTerm) //限制条件
            {
                case 1:
                    LabelVictoryText = "所有单位血量为" + gl.LimitParam1;
                    break;
                case 2:
                    LabelVictoryText = "限制攻击类职业上阵";
                    break;
                case 3:
                    LabelVictoryText = "限制防御类职业上阵";
                    break;
                case 4:
                    LabelVictoryText = "限制特殊类职业上阵";
                    break;
                case 5:
                    LabelVictoryText = "限制攻击类职业无法上阵";
                    break;
                case 6:
                    LabelVictoryText = "限制防御类职业无法上阵";
                    break;
                case 7:
                    LabelVictoryText = "限制特殊类职业无法上阵";
                    break;
                case 8:
                    LabelVictoryText = "限制只能上阵" + gl.LimitParam1 + "名英雄";
                    break;
                case 9:
                    LabelVictoryText = "己方开场随机获得BUFF";
                    break;
                case 10:
                    LabelVictoryText = "敌方开场随机获得BUFF";
                    break;
                case 11:
                    LabelVictoryText = "己方开场随机受到DEBUFF";
                    break;
                case 12:
                    if (TextTranslator.instance.GetHeroInfoByHeroID(gl.LimitParam1) != null)
                    {
                        LabelVictoryText = "限制" + TextTranslator.instance.GetHeroInfoByHeroID(gl.LimitParam1).heroName + "必须上阵";
                    }
                    break;
                case 13:
                    if (TextTranslator.instance.GetHeroInfoByHeroID(gl.LimitParam1) != null)
                    {
                        LabelVictoryText = "限制" + TextTranslator.instance.GetHeroInfoByHeroID(gl.LimitParam1).heroName + "无法上阵";
                    }
                    break;
            }

            switch (gl.WinTerm) //1星条件
            {
                case 1:
                    LabelVictoryText1 = "全灭敌人";
                    break;
                case 2:
                    for (int j = 0; j < TextTranslator.instance.enemyInfoList.Count; j++)
                    {
                        if (gl.WinParam1 == TextTranslator.instance.enemyInfoList[j].enemyID)
                        {
                            LabelVictoryText1 = "击败敌人" + TextTranslator.instance.enemyInfoList[j].name;
                            break;
                        }
                    }
                    break;
                case 3:
                    LabelVictoryText1 = "己方人质存活并且全灭敌方";
                    break;
                case 4:
                    LabelVictoryText1 = "存活至第" + gl.WinParam1 + "次步数";
                    break;
                case 5:
                    LabelVictoryText1 = "限定" + gl.WinParam1.ToString() + "步数内全灭敌人";
                    break;
                case 6:
                    LabelVictoryText1 = "全灭敌人";
                    break;
            }

            switch (gl.Star2Term) //2星条件
            {
                case 1:
                    if (gl.Star2Param1 == 0)
                    {
                        LabelVictoryText2 = "全员存活通关";
                    }
                    else if (gl.Star2Param1 == 1)
                    {
                        LabelVictoryText2 = "阵亡不超过1人";
                    }
                    else
                    {
                        LabelVictoryText2 = "阵亡不超过2人";
                    }
                    break;
                case 2:
                    LabelVictoryText2 = "上阵" + gl.Star2Param1 + "人通关";
                    break;
                case 3:
                    LabelVictoryText2 = gl.Star2Param1.ToString() + "步数内通关";
                    break;
            }

            switch (gl.Star3Term) //3星条件
            {
                case 1:
                    if (gl.Star3Param1 == 0)
                    {
                        LabelVictoryText3 = "全员存活通关";
                    }
                    else if (gl.Star3Param1 == 1)
                    {
                        LabelVictoryText3 = "阵亡不超过1人";
                    }
                    else
                    {
                        LabelVictoryText3 = "阵亡不超过2人";
                    }
                    break;
                case 2:
                    LabelVictoryText3 = "上阵" + gl.Star3Param1 + "人通关";
                    break;
                case 3:
                    LabelVictoryText3 = gl.Star3Param1.ToString() + "步数内通关";
                    break;
            }
            if (isRedMessage)
            {
                switch (gl.Highlight)
                {
                    case 1:
                        RedLabelText = "目标:" + LabelVictoryText1;
                        break;
                    case 2:
                        RedLabelText = "目标:" + LabelVictoryText2;
                        break;
                    case 3:
                        RedLabelText = "目标:" + LabelVictoryText3;
                        break;
                }
            }
        }
        else
        {
            LabelVictoryText1 = "全灭敌人";
            LabelVictoryText2 = "阵亡不超过1人";
            LabelVictoryText3 = "全员存活通关";
            LabelVictoryText = "";
            GameObject.Find("AutoButton/Sprite/SpriteXianding").SetActive(false);
        }

        if (PictureCreater.instance.FightStyle == 0 || PictureCreater.instance.FightStyle == 4 || PictureCreater.instance.FightStyle == 5)
        {
            if (g != null)
            {
                if (g.roleID > 0)
                {
                    SetIHeroInfo(g.roleID);
                }
            }
        }

        if (GameObject.Find("HeroInfo") == null)
        {
            StartCoroutine(ShowRedMessage());
        }

        foreach (var c in LabelVictoryText1)
        {
            LabelVictory1.text += c;
            yield return new WaitForSeconds(0.02f);
        }
        foreach (var c in LabelVictoryText2)
        {
            LabelVictory2.text += c;
            yield return new WaitForSeconds(0.02f);
        }
        foreach (var c in LabelVictoryText3)
        {
            LabelVictory3.text += c;
            yield return new WaitForSeconds(0.02f);
        }
        foreach (var c in LabelVictoryText)
        {
            LabelVictory.text += c;
            yield return new WaitForSeconds(0.02f);
        }

        if (GameObject.Find("FightWindow/Sprite/SpriteXianding") != null)
        {
            GameObject.Find("FightWindow/Sprite/SpriteXianding").GetComponent<TweenScale>().enabled = true;
            GameObject.Find("FightWindow/Sprite/SpriteXianding").GetComponent<TweenScale>().ResetToBeginning();
        }
    }

    //本关目标提示
    IEnumerator ShowRedMessage()
    {
        if (PlayerPrefs.GetInt(SceneTransformer.instance.GetGuideStateName()) == 18 && isFinishMessge == false)
        {
            if (PictureCreater.instance.FightStyle == 0 || PictureCreater.instance.FightStyle == 4 || PictureCreater.instance.FightStyle == 5)
            {
                isFinishMessge = true;
                MessageEffect.SetActive(true);
                yield return new WaitForSeconds(0.2f);
                if (RedLabelText != "")
                {
                    AudioEditer.instance.PlayOneShot("typing");
                    foreach (var c in RedLabelText)
                    {
                        RedLabel.text += c;
                        yield return new WaitForSeconds(0.1f);
                    }
                }

                yield return new WaitForSeconds(2f);
                List<int> ListRoleID = new List<int>();
                bool IsNPC = false;
                foreach (var r in PictureCreater.instance.ListRolePicture)
                {
                    if (r.RoleObject.name.IndexOf("NPC") > -1)
                    {
                        ListRoleID.Clear();
                        ListRoleID.Add(r.RoleID);
                        IsNPC = true;
                        break;
                    }
                    else if (r.RolePosition > 0)
                    {
                        ListRoleID.Add(r.RoleID);
                    }
                }
                if (ListRoleID.Count > 0)
                {
                    int RoleID = ListRoleID[Random.Range(0, ListRoleID.Count)];
                    List<FightTalk> _ListFightTalk = TextTranslator.instance.GetFightTalkByRoleID(RoleID, 1, IsNPC ? 3 : 1);


                    if (IsNPC)
                    {
                        foreach (var r in PictureCreater.instance.ListRolePicture)
                        {
                            if (r.RoleObject.name.IndexOf("NPC") > -1)
                            {
                                r.RoleRedBloodObject.SetActive(true);
                                r.RoleObject.GetComponent<ColliderDisplayText>().SpriteTalk.SetActive(true);
                                r.RoleObject.GetComponent<ColliderDisplayText>().LabelTalk.text = _ListFightTalk[0].Talk;
                                break;
                            }
                        }
                    }
                    else
                    {
                        foreach (var r in PictureCreater.instance.ListRolePicture)
                        {
                            if (r.RoleID == RoleID)
                            {
                                r.RoleRedBloodObject.SetActive(true);
                                r.RoleObject.GetComponent<ColliderDisplayText>().SpriteTalk.SetActive(true);
                                r.RoleObject.GetComponent<ColliderDisplayText>().LabelTalk.text = _ListFightTalk[Random.Range(0, _ListFightTalk.Count)].Talk;
                                break;
                            }
                        }
                    }
                    yield return new WaitForSeconds(3.5f);
                    foreach (var r in PictureCreater.instance.ListRolePicture)
                    {
                        r.RoleObject.GetComponent<ColliderDisplayText>().SpriteTalk.SetActive(false);
                        r.RoleRedBloodObject.SetActive(false);
                    }
                }
            }
        }
    }


    public void BeginFight()
    {
        UIManager.instance.StartGate(UIManager.GateAnchorName.comeGatePeop);
        PictureCreater.instance.ActObject.SetActive(false);
        PictureCreater.instance.SetSequence();
        Time.timeScale = PictureCreater.instance.HardLevel;
        PictureCreater.instance.IsFight = true;
        PictureCreater.instance.SetListPosition(PictureCreater.instance.PositionRow, 2);
        StartCoroutine(PictureCreater.instance.ShowFight());
        //GameObject.Find("Bottom").SetActive(false);
        //GameObject.Find("AutoButton").SetActive(false);
        //GameObject.Find("LabelCombo").GetComponent<UILabel>().text = "";

        if (PictureCreater.instance.IsRemember)
        {
            if (CharacterRecorder.instance.level > 5)
            {
                ExitButton.SetActive(true);
            }
            BackButton.SetActive(false);
            GateDescription.GetComponent<TweenPosition>().PlayForward();
            string PositionString = "";
            foreach (var h in CharacterRecorder.instance.ownedHeroList)
            {
                if (h.position > 0)
                {
                    PositionString += h.characterRoleID.ToString() + "$" + h.position.ToString() + "!";
                }
            }
            BulletScreenInfo();//弹幕
        }
    }

    public void SetWoodFight(string FightString)
    {
        int i = 0;
        float AttackBuff = 0;
        float BuffCrit = 0;
        float BuffDefend = 0;
        float BuffDodge = 0;
        float BuffNoCrit = 0;
        float BuffHit = 0;
        string[] SecSplit = PictureCreater.instance.WoodBuff.Split('!');
        for (i = 0; i < SecSplit.Length - 1; i++)
        {
            string[] dataValueSplit = SecSplit[i].Split('$');
            switch (dataValueSplit[0])
            {
                case "1":
                    BuffDefend += float.Parse(dataValueSplit[1]) * 1000f;
                    break;
                case "2":
                    BuffCrit += float.Parse(dataValueSplit[1]) * 1000f;
                    break;
                case "3":
                    AttackBuff += float.Parse(dataValueSplit[1]) * 1000f;
                    break;
                case "4":
                    BuffNoCrit += float.Parse(dataValueSplit[1]) * 1000f;
                    break;
                case "5":
                    BuffDodge += float.Parse(dataValueSplit[1]) * 1000f;
                    break;
                case "6":
                    BuffHit += float.Parse(dataValueSplit[1]) * 1000f;
                    break;
            }
        }

        foreach (var h in CharacterRecorder.instance.ownedHeroList)
        {
            string[] dataSplit = PictureCreater.instance.WoodPosition.Split('!');
            bool IsPosition = false;
            int Position = 0;
            for (i = 0; i < dataSplit.Length - 1; i++)
            {
                string[] secSplit = dataSplit[i].Split('$');
                //rw.SetTeamInfo(i, int.Parse(secSplit[0]), int.Parse(secSplit[1]));
                if (int.Parse(secSplit[0]) == h.characterRoleID)
                {
                    IsPosition = true;
                    Position = int.Parse(secSplit[1]);

                    if (i == dataSplit.Length - 2)
                    {
                        PictureCreater.instance.SelectPosition = Position;
                    }
                }
            }

            int Blood = 0;
            int SkillPoint = 0;
            int IsDead = 0;

            string[] fightSplits = FightString.Split('!');
            for (int j = 0; j < fightSplits.Length - 1; j++)
            {
                string[] fightSplit = fightSplits[j].Split('$');
                if (fightSplit[0] == h.characterRoleID.ToString())
                {
                    Blood = (int)System.Math.Floor(double.Parse(fightSplit[1]));
                    SkillPoint = int.Parse(fightSplit[2]);
                    IsDead = int.Parse(fightSplit[3]);
                    break;
                }
            }

            if (IsDead == 1)
            {
                Position = 0;
                IsPosition = false;
            }

            int SkillID = TextTranslator.instance.GetHeroInfoByHeroID(h.cardID).heroSkillList[0];
            int SkillID2 = TextTranslator.instance.GetHeroInfoByHeroID(h.cardID).heroSkillList[1];

            PictureCreater.instance.CreateRole(h.cardID, h.name, Position, Color.cyan, (h.HP + h.HPAdd), (h.HP + h.HPAdd) - Blood, h.hit + h.hitAdd + BuffHit, h.aspd / 10f, false, false, 1, 1.5f, h.dodge + h.dodgeAdd + BuffDodge, "R" + h.characterRoleID.ToString(), h.characterRoleID, (int)((h.strength + h.strengthAdd)), h.physicalCrit + h.physicalCritAdd + BuffCrit, ((h.physicalDefense + h.physicalDefenseAdd) / 4f), h.UNphysicalCrit + h.UNphysicalCritAdd + BuffNoCrit, h.moreDamige + h.moreDamigeAdd + AttackBuff, h.avoidDamige + h.avoidDamigeAdd + BuffDefend, SkillID, SkillID2, h.force, h.skillLevel, h.WeaponList[0].WeaponClass, h.area, h.move, SkillPoint, "");

            if (!IsPosition)
            {
                GameObject go = NGUITools.AddChild(MyGrid, MyHeroItem);
                go.name = h.cardID.ToString();
                go.AddComponent<RoleHeroItem>();
                go.GetComponent<RoleHeroItem>().Init(h.cardID, h.characterRoleID, i);
                go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("ScrollView").GetComponent<UIScrollView>();

                if (PictureCreater.instance.FightStyle == 2)
                {
                    go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                    go.GetComponent<RoleHeroItem>().SetHeroHpInfo((h.HP + h.HPAdd) - Blood, (h.HP + h.HPAdd), SkillPoint);
                    MyGrid.GetComponent<UIGrid>().cellWidth = 100;
                    if (IsDead == 1)
                    {
                        go.transform.Find("SpriteHead").gameObject.GetComponent<UISprite>().spriteName = h.cardID.ToString() + "1";
                        go.GetComponent<RoleHeroItem>().enabled = false;
                    }
                }

                ListHero.Add(go);
            }
        }
        PictureCreater.instance.SelectPosition = 0;
        MyGrid.GetComponent<UIGrid>().Reposition();
        PictureCreater.instance.SetSequence();
        //StartCoroutine(AutoWoodFight());
    }

    public void CloseHeroInfo()
    {
        HeroInfoObj.SetActive(false);
        CloseInfoButton.SetActive(false);
        StartCoroutine(ShowRedMessage());
    }


    void Update()
    {
        if (PictureCreater.instance.IsFight && !PictureCreater.instance.IsLock)
        {
            if (SkillCD1 > 0)
            {
                if (!TacticsInfo.activeSelf)
                {
                    Tactics.transform.GetChild(2).GetChild(5).gameObject.SetActive(true);
                    SkillCD1 -= Time.deltaTime;
                    Tactics.transform.GetChild(2).GetChild(5).GetComponent<UILabel>().text = string.Format("{0:0}", SkillCD1);
                }
                SkillMask1.GetComponent<UISprite>().fillAmount = SkillCD1 / SkillUpCD1;
                SkillEffectOK1.SetActive(false);
            }
            else if (SkillCD1 < 0)
            {
                SkillCD1 = 0;
                SkillMask1.SetActive(false);
                SkillMask1.GetComponent<UISprite>().fillAmount = 0;
                SkillEffectOK1.SetActive(true);
                SkillEffect1.SetActive(false);
                PictureCreater.instance.SkillFire1 = 0;
                AudioEditer.instance.PlayOneShot("ui_skillready");
                NGUITools.AddChild(SkillButton1, SkillEffectCD);
                if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 12 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) < 10)
                {
                    PictureCreater.instance.IsLock = true;
                }
                Tactics.transform.GetChild(2).GetChild(5).gameObject.SetActive(false);
            }
            else if (SkillCD1 == 0 && !PictureCreater.instance.IsHand)
            {
                if (PictureCreater.instance.FightStyle == 0 || PictureCreater.instance.FightStyle == 4 || PictureCreater.instance.FightStyle == 5)
                {
                    StartCoroutine(PictureCreater.instance.FireAutoSkill(CharacterRecorder.instance.ListManualSkillId[0], PictureCreater.instance.ListRolePicture, PictureCreater.instance.ListEnemyPicture));
                    PictureCreater.instance.CloseSkill(PictureCreater.instance.ListRolePicture, 1);
                }
                Tactics.transform.GetChild(2).GetChild(5).gameObject.SetActive(false);
            }

            if (SkillName2 != "0")
            {
                if (SkillCD2 > 0)
                {
                    if (!TacticsInfo.activeSelf)
                    {
                        Tactics.transform.GetChild(1).GetChild(6).gameObject.SetActive(true);
                        SkillCD2 -= Time.deltaTime;
                        Tactics.transform.GetChild(1).GetChild(6).GetComponent<UILabel>().text = string.Format("{0:0}", SkillCD2);
                    }
                    SkillMask2.GetComponent<UISprite>().fillAmount = SkillCD2 / SkillUpCD2;
                    SkillEffectOK2.SetActive(false);
                }
                else if (SkillCD2 < 0)
                {
                    SkillMask2.GetComponent<UISprite>().fillAmount = 0;
                    SkillEffectOK2.SetActive(true);
                    SkillEffect2.SetActive(false);
                    SkillMask2.SetActive(false);
                    SkillCD2 = 0;
                    PictureCreater.instance.SkillFire2 = 0;
                    AudioEditer.instance.PlayOneShot("ui_skillready");
                    NGUITools.AddChild(SkillButton2, SkillEffectCD);

                    Tactics.transform.GetChild(1).GetChild(6).gameObject.SetActive(false);
                }
                else if (SkillCD2 == 0 && !PictureCreater.instance.IsHand)
                {
                    if (PictureCreater.instance.FightStyle == 0 || PictureCreater.instance.FightStyle == 4 || PictureCreater.instance.FightStyle == 5)
                    {
                        StartCoroutine(PictureCreater.instance.FireAutoSkill(CharacterRecorder.instance.ListManualSkillId[1], PictureCreater.instance.ListRolePicture, PictureCreater.instance.ListEnemyPicture));
                        PictureCreater.instance.CloseSkill(PictureCreater.instance.ListRolePicture, 2);
                    }
                    Tactics.transform.GetChild(1).GetChild(6).gameObject.SetActive(false);
                }
            }

            if (SkillName3 != "0")
            {
                if (SkillCD3 > 0)
                {
                    if (!TacticsInfo.activeSelf)
                    {
                        Tactics.transform.GetChild(0).GetChild(6).gameObject.SetActive(true);
                        SkillCD3 -= Time.deltaTime;
                        Tactics.transform.GetChild(0).GetChild(6).GetComponent<UILabel>().text = string.Format("{0:0}", SkillCD3);
                    }
                    SkillMask3.GetComponent<UISprite>().fillAmount = SkillCD3 / SkillUpCD3;
                    SkillEffectOK3.SetActive(false);
                }
                else if (SkillCD3 < 0)
                {
                    SkillCD3 = 0;
                    SkillMask3.SetActive(false);
                    SkillMask3.GetComponent<UISprite>().fillAmount = 0;
                    SkillEffectOK3.SetActive(true);
                    SkillEffect3.SetActive(false);
                    PictureCreater.instance.SkillFire3 = 0;
                    AudioEditer.instance.PlayOneShot("ui_skillready");
                    NGUITools.AddChild(SkillButton3, SkillEffectCD);
                    Tactics.transform.GetChild(0).GetChild(6).gameObject.SetActive(false);
                }
                else if (SkillCD3 == 0 && !PictureCreater.instance.IsHand)
                {
                    if (PictureCreater.instance.FightStyle == 0 || PictureCreater.instance.FightStyle == 4 || PictureCreater.instance.FightStyle == 5)
                    {
                        StartCoroutine(PictureCreater.instance.FireAutoSkill(CharacterRecorder.instance.ListManualSkillId[2], PictureCreater.instance.ListRolePicture, PictureCreater.instance.ListEnemyPicture));
                        PictureCreater.instance.CloseSkill(PictureCreater.instance.ListRolePicture, 3);
                    }
                    Tactics.transform.GetChild(0).GetChild(6).gameObject.SetActive(false);
                }
            }
        }
        if (PictureCreater.instance.IsFight)
        {
            if (PictureCreater.instance.FightStyle == 6 || PictureCreater.instance.FightStyle == 7)
            {
                EverydayBossobj.SetActive(true);
                NowTimer.text = (PictureCreater.instance.LimitTime - PictureCreater.instance.LimitTimer).ToString("00");
                //TimeSprite.fillAmount = (PictureCreater.instance.LimitTime - PictureCreater.instance.LimitTimer) / PictureCreater.instance.LimitTime;
                NowClickNumber.text = ClickNumber.ToString();
                //  HistoryClickNumber.text = "最佳成绩:"+"1000";
                if (PictureCreater.instance.FightStyle == 6)
                {
                    EverydayBossobj.transform.Find("Boss").GetComponent<UISprite>().spriteName = "yxdi" + TextTranslator.instance.GetItemByItemCode(65390).itemGrade;
                    EverydayBossobj.transform.Find("Boss/Sprite").GetComponent<UISprite>().spriteName = "65391";
                }
                else
                {
                    EverydayBossobj.transform.Find("Boss").GetComponent<UISprite>().spriteName = "yxdi" + TextTranslator.instance.GetItemByItemCode(65391).itemGrade;
                    EverydayBossobj.transform.Find("Boss/Sprite").GetComponent<UISprite>().spriteName = "65390";
                }
                if (!isClose && (CharacterRecorder.instance.GuideID[23] >= 2 || CharacterRecorder.instance.GuideID[24] >= 2))
                {
                    EverydayBossobj.transform.Find("Message").gameObject.SetActive(true);
                    EverydayBossobj.transform.Find("MouseMessage").gameObject.SetActive(true);
                    StartCoroutine(DelayEveydayMouse());
                }
                float Each = PictureCreater.instance.ListEnemyPicture[0].RoleMaxBlood / 4;
                float LoseHp = PictureCreater.instance.ListEnemyPicture[0].RoleMaxBlood - PictureCreater.instance.ListEnemyPicture[0].RoleNowBlood;
                if (LoseHp <= Each)
                {
                    HPNum = 4;
                    FourSlider.SetActive(true);
                    ThreeSlider.SetActive(false);
                    TwoSlider.SetActive(false);
                    OneSlider.SetActive(false);
                    FourSlider.GetComponent<UISlider>().value = 1 - (LoseHp / Each);
                }
                else if (LoseHp - Each <= Each)
                {
                    HPNum = 3;
                    FourSlider.SetActive(false);
                    ThreeSlider.SetActive(true);
                    TwoSlider.SetActive(false);
                    OneSlider.SetActive(false);
                    ThreeSlider.GetComponent<UISlider>().value = 1 - ((LoseHp - Each) / Each);
                }
                else if (LoseHp - Each * 2 <= Each)
                {
                    HPNum = 2;
                    FourSlider.SetActive(false);
                    ThreeSlider.SetActive(false);
                    TwoSlider.SetActive(true);
                    OneSlider.SetActive(false);
                    TwoSlider.GetComponent<UISlider>().value = 1 - ((LoseHp - Each * 2) / Each);

                }
                else if (LoseHp - Each * 3 <= Each)
                {
                    HPNum = 1;
                    FourSlider.SetActive(false);
                    ThreeSlider.SetActive(false);
                    TwoSlider.SetActive(false);
                    OneSlider.SetActive(true);
                    OneSlider.GetComponent<UISlider>().value = 1 - ((LoseHp - Each * 3) / Each);
                }
                //else if (LoseHp >= PictureCreater.instance.ListEnemyPicture[0].RoleMaxBlood)
                //{
                //    Debug.LogError("sssssssssssssssssssss");
                //    HPNum = 0;
                //    FourSlider.SetActive(false);
                //    ThreeSlider.SetActive(false);
                //    TwoSlider.SetActive(false);
                //    OneSlider.SetActive(false);
                //}
                EverydayBossobj.transform.Find("HPNum").GetComponent<UILabel>().text = "X" + HPNum.ToString();
            }
        }
    }

    /// <summary>
    /// 设置连赢场数 和 守军数量 和 虚弱度 的Label值
    /// </summary>
    /// <param name="winNum">连赢场数</param>
    /// <param name="garrisonNum">守军数量</param>
    /// <param name="weakNum">虚弱度</param>
    public void SetWintreakAndWeakAndGarrisonLabelValue(string Name)
    {
        winStreakSprite.gameObject.SetActive(true);
        garrisonNumSprite.gameObject.SetActive(true);
        weakSprite.gameObject.SetActive(true);
        nameSprite.gameObject.SetActive(true);
        //连赢场数
        winStreakSprite.GetChild(2).GetComponent<UILabel>().text = PictureCreater.instance.ContinueWin.ToString();
        //守军数量
        garrisonNumSprite.GetChild(1).GetComponent<UILabel>().text = CharacterRecorder.instance.ReinforcementNum.ToString();
        //虚弱度
        weakSprite.GetChild(2).GetComponent<UILabel>().text = (PictureCreater.instance.WeakNum == 0 ? "0%" : (100 - PictureCreater.instance.WeakNum * 100).ToString("#0") + "%");
        //守军姓名
        nameSprite.GetChild(0).GetComponent<UILabel>().text = Name;
    }

    //日常副本鼠标
    IEnumerator DelayEveydayMouse()
    {
        yield return new WaitForSeconds(0.5f);
        EverydayBossobj.transform.Find("MouseMessage").transform.Find("MouseGuang").gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        EverydayBossobj.transform.Find("MouseMessage").gameObject.SetActive(false);
        EverydayBossobj.transform.Find("MouseMessage").transform.Find("MouseGuang").gameObject.SetActive(false);
        isClose = true;

    }
    //日常提示信息显示
    public IEnumerator ShowMessage()
    {
        EverydayBossobj.transform.Find("BigMessage").GetComponent<UILabel>().text = "";
        string label = "点击屏幕可增加攻击输出";
        foreach (var c in label)
        {
            EverydayBossobj.transform.Find("BigMessage").GetComponent<UILabel>().text += c;
            yield return new WaitForSeconds(0.2f);
        }
    }
    public IEnumerator StartFight()
    {        
        FightPosition = "";
        FightMask.SetActive(false);//关闭遮罩
        //if (isRedMessage)
        //{
        //    RedMessage.SetActive(true);
        //    isRedMessage = false;
        //}
        if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) >= 18)//日常副本新手引导
        {
            isStartFight = true;
            FightMask.GetComponent<BoxCollider>().enabled = true;
            if (CharacterRecorder.instance.GuideID[24] < 2 || CharacterRecorder.instance.GuideID[23] < 2)
            {
                StartCoroutine(SceneTransformer.instance.NewbieGuide());
            }
        }
        if (PictureCreater.instance.IsRemember)
        {
            GameObject.Find("AutoButton").transform.FindChild("FightSprite").GetComponent<TweenScale>().enabled = true;
            gameObject.transform.FindChild("WF_KaiZhan_AnNiu_eff").gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.6f);

        SkillName2 = SkillButton2.transform.Find("Sprite").gameObject.GetComponent<UISprite>().spriteName;
        SkillName3 = SkillButton3.transform.Find("Sprite").gameObject.GetComponent<UISprite>().spriteName;
        if (SkillName2 != "0")
        {
            LabelSkill2.SetActive(false);
        }
        else
        {
            if (CharacterRecorder.instance.lastGateID > 10020)
            {
                LabelSkill2.GetComponent<UILabel>().text = "";
                SkillButton2.transform.Find("Sprite").gameObject.GetComponent<UISprite>().spriteName = "1";
            }
        }
        if (SkillName3 != "0")
        {
            LabelSkill3.SetActive(false);
        }
        else
        {
            if (CharacterRecorder.instance.lastGateID > 10075)
            {
                LabelSkill3.GetComponent<UILabel>().text = "";
                SkillButton3.transform.Find("Sprite").gameObject.GetComponent<UISprite>().spriteName = "1";
            }
        }

       
        if (PictureCreater.instance.FightStyle == 0)
        {
            bool IsFight = false;
            foreach (var h in PictureCreater.instance.ListRolePicture)
            {
                if (h.RolePosition > 0)
                {
                    Invoke("ShowSkipButton", 1);
                    IsFight = true;
                    if (PictureCreater.instance.IsRemember)
                    {
                        GameObject.Find("Bottom").SetActive(false);
                        GameObject.Find("AutoButton").SetActive(false);
                    }
                    LabelCombo.SetActive(false);
                    SkillButton1.SetActive(true);
                    SkillButton2.SetActive(true);
                    SkillButton3.SetActive(true);

                    string PVEPosition = "";

                    foreach (var p in PictureCreater.instance.ListRolePicture)
                    {
                        if (p.RolePosition != PictureCreater.instance.SelectPosition && p.RolePosition > 0)
                        {
                            PVEPosition += p.RoleCharacterRoleID.ToString() + "$" + p.RolePosition.ToString() + "!";
                        }
                    }

                    foreach (var p in PictureCreater.instance.ListRolePicture)
                    {
                        if (p.RolePosition == PictureCreater.instance.SelectPosition && p.RolePosition > 0)
                        {
                            PVEPosition += p.RoleCharacterRoleID.ToString() + "$" + p.RolePosition.ToString() + "!";
                        }
                    }

                    if (PictureCreater.instance.IsRemember)
                    {
                        if (PVEPosition != PictureCreater.instance.PVEPosition)
                        {
                            NetworkHandler.instance.SendProcess("6007#1;" + PVEPosition + ";");
                        }
                        NetworkHandler.instance.SendProcess("2004#" + SceneTransformer.instance.NowGateID.ToString() + ";0;");
                    }
                    foreach (var p in PictureCreater.instance.ListRolePicture)
                    {
                        if (p.RolePosition > 0)
                        {
                            FightPosition += p.RoleID + "$" + p.RolePosition.ToString() + "!";
                        }
                    }

                    if (CharacterRecorder.instance.level > 20)
                    {
                        ExitButton.SetActive(true);
                    }

                    DestroyImmediate(GameObject.Find("MyPath"));
                    StopAllCoroutines();
                    break;
                }
            }
            if (!IsFight)
            {
                UIManager.instance.OpenPromptWindow("请先上阵英雄", PromptWindow.PromptType.Hint, null, null);
            }
        }
        else
        {
            bool IsFight = false;
            foreach (var h in PictureCreater.instance.ListRolePicture)
            {
                if (h.RolePosition > 0)
                {
                    Invoke("ShowSkipButton", 1);
                    IsFight = true;
                    LabelCombo.SetActive(false);
                    SkillButton1.SetActive(true);
                    SkillButton2.SetActive(true);
                    SkillButton3.SetActive(true);
                    GateDescription.SetActive(false);

                    string WoodPosition = "";

                    foreach (var p in PictureCreater.instance.ListRolePicture)
                    {
                        if (p.RolePosition != PictureCreater.instance.SelectPosition && p.RolePosition > 0)
                        {
                            WoodPosition += p.RoleCharacterRoleID.ToString() + "$" + p.RolePosition.ToString() + "!";
                        }
                    }

                    foreach (var p in PictureCreater.instance.ListRolePicture)
                    {
                        if (p.RolePosition == PictureCreater.instance.SelectPosition && p.RolePosition > 0)
                        {
                            WoodPosition += p.RoleCharacterRoleID.ToString() + "$" + p.RolePosition.ToString() + "!";
                        }
                    }


                    if (PictureCreater.instance.IsRemember)
                    {
                        if (PictureCreater.instance.FightStyle == 1 || PictureCreater.instance.FightStyle == 3 || PictureCreater.instance.FightStyle == 13 || PictureCreater.instance.FightStyle == 15 || PictureCreater.instance.FightStyle == 16)
                        {
                            if (WoodPosition != PictureCreater.instance.PVPPosition)
                            {
                                PictureCreater.instance.PVPPosition = WoodPosition;
                                PictureCreater.instance.ChangePvpPosition(true);
                                NetworkHandler.instance.SendProcess("6007#6;" + PictureCreater.instance.PVPPosition + ";");
                            }
                        }
                        else if (PictureCreater.instance.FightStyle == 12)
                        {
                            if (WoodPosition != PictureCreater.instance.InstancePosition)
                            {
                                PictureCreater.instance.InstancePosition = WoodPosition;
                                NetworkHandler.instance.SendProcess("6007#4;" + PictureCreater.instance.InstancePosition + ";");
                            }
                        }
                        else if (PictureCreater.instance.FightStyle == 14)
                        {
                            if (WoodPosition != PictureCreater.instance.LegionPosition)
                            {
                                PictureCreater.instance.LegionPosition = WoodPosition;
                                NetworkHandler.instance.SendProcess("6007#3;" + PictureCreater.instance.LegionPosition + ";");
                            }

                            NetworkHandler.instance.SendProcess("8624#" + CharacterRecorder.instance.LegionHarasPoint + ";");//发起骚扰
                        }
                        else
                        {
                            if (WoodPosition != PictureCreater.instance.WoodPosition)
                            {
                                NetworkHandler.instance.SendProcess("6007#2;" + WoodPosition + ";");
                            }
                        }
                    }

                    foreach (var p in PictureCreater.instance.ListRolePicture)
                    {
                        if (p.RolePosition > 0)
                        {
                            FightPosition += p.RoleID + "$" + p.RolePosition.ToString() + "!";
                        }
                    }

                    //if (PictureCreater.instance.ListWinRecord.ContainsKey(PictureCreater.instance.PVPRank))
                    //{
                    //    if (PictureCreater.instance.PVPRank > 0)
                    //    {
                    //        if (PictureCreater.instance.ListWinRecord[PictureCreater.instance.PVPRank] == WoodPosition)
                    //        {
                    //            SkipButton.SetActive(true);
                    //        }
                    //    }
                    //}



                    StopAllCoroutines();
                    DestroyImmediate(GameObject.Find("MyPath"));
                    Time.timeScale = PictureCreater.instance.HardLevel;
                    PictureCreater.instance.IsFight = true;
                    StartCoroutine(PictureCreater.instance.ShowFight());
                    if (PictureCreater.instance.IsRemember)
                    {
                        GameObject.Find("Bottom").SetActive(false);
                        GameObject.Find("AutoButton").SetActive(false);
                    }
                    if (PictureCreater.instance.FightStyle != 12 && PictureCreater.instance.FightStyle != 14 && PictureCreater.instance.FightStyle != 1)
                    {
                        ExitButton.SetActive(true);
                    }
                    BackButton.SetActive(false);
                    break;
                }
            }
            if (!IsFight)
            {
                UIManager.instance.OpenPromptWindow("请先上阵英雄", PromptWindow.PromptType.Hint, null, null);
            }
            if (PictureCreater.instance.FightStyle == 2)
            {
                PictureCreater.instance.WoodSequence();
            }
        }
        yield return 0;
    }

    public void ShowSkipButton()
    {
        if (PictureCreater.instance.FightStyle == 0 || PictureCreater.instance.FightStyle == 4 || PictureCreater.instance.FightStyle == 5 || PictureCreater.instance.FightStyle == 8 || PictureCreater.instance.FightStyle == 9 || PictureCreater.instance.FightStyle == 10 || PictureCreater.instance.FightStyle == 11 || PictureCreater.instance.FightStyle == 12)
        {
            if (CharacterRecorder.instance.Vip > 6)
            {
                SkipButton.SetActive(true);
            }
        }
        else if (PictureCreater.instance.FightStyle == 1 || PictureCreater.instance.FightStyle == 3 || PictureCreater.instance.FightStyle == 13 || PictureCreater.instance.FightStyle == 15 || PictureCreater.instance.FightStyle == 16 || PictureCreater.instance.FightStyle == 17)
        {
            SkipButton.SetActive(true);
        }
    }

    public void SetRoleVisible(int RoleID, bool IsVisible, int NowBlood, int MaxBlood, int SkillPoint)
    {

        if (IsVisible)
        {
            bool IsNew = false;

            //for (int i = 0; i < ListHero.Count; i++)
            //{
            //    DestroyImmediate(ListHero[i]);
            //}
            //ListHero.Clear();
            int ListHeroCount = 0;
            if (!IsNew)
            {
                foreach (var h in CharacterRecorder.instance.ownedHeroList)
                {
                    HeroInfo hi = TextTranslator.instance.GetHeroInfoByHeroID(h.cardID);
                    if (hi.heroCarrerType == PictureCreater.instance.LimitType)
                    {
                        continue;
                    }
                    if (hi.heroCarrerType != PictureCreater.instance.PermissionType && PictureCreater.instance.PermissionType != 0)
                    {
                        continue;
                    }
                    if (h.cardID == PictureCreater.instance.LimitHeroID)
                    {
                        continue;
                    }

                    bool IsRepeat = false;
                    foreach (var g in ListHero)
                    {
                        if (g.name == h.cardID.ToString())
                        {
                            Transform t = MyGrid.transform.Find(h.cardID.ToString());
                            t.parent = MyGrid.transform.parent;
                            t.parent = MyGrid.transform;
                            IsRepeat = true;
                            ListHeroCount++;
                            break;
                        }
                    }

                    if (!IsRepeat)
                    {
                        if (h.position == 0 && h.cardID == RoleID)
                        {
                            GameObject go = NGUITools.AddChild(MyGrid, MyHeroItem);
                            go.name = h.cardID.ToString();
                            go.AddComponent<RoleHeroItem>();
                            go.GetComponent<RoleHeroItem>().Init(h.cardID, 0, 0);
                            go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("ScrollView").GetComponent<UIScrollView>();
                            if (PictureCreater.instance.FightStyle == 2)
                            {
                                go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
                                go.GetComponent<RoleHeroItem>().SetHeroHpInfo(NowBlood, MaxBlood, SkillPoint);
                                MyGrid.GetComponent<UIGrid>().cellWidth = 100;
                            }
                            ListHero.Insert(ListHeroCount, go);
                            //ListHero.Add(go);
                        }
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < ListHero.Count; i++)
            {
                if (ListHero[i].name == RoleID.ToString())
                {
                    //h.SetActive(false);
                    //h.transform.Find("SpriteHead").gameObject.GetComponent<UISprite>().spriteName = RoleID.ToString() + "1";
                    //h.GetComponent<UISprite>().spriteName = "gray";
                    //h.GetComponent<UIButton>().hoverSprite = "gray";
                    //h.GetComponent<UIButton>().normalSprite = "gray";

                    DestroyImmediate(ListHero[i]);
                    ListHero.RemoveAt(i);
                    break;
                }
            }
        }


        MyGrid.GetComponent<UIGrid>().Reposition();
    }

    public void SetCamera()
    {
        if (PictureCreater.instance.MyUISliderValue > 0f)
        {
            MyUISlider.value = PictureCreater.instance.MyUISliderValue;
            PictureCreater.instance.CameraY = 7 * (PictureCreater.instance.MyUISliderValue + 0.3f);
            //MyCamera.GetComponent<Camera>().fieldOfView = 15 + (PictureCreater.instance.FieldView - 15) * (PictureCreater.instance.MyUISliderValue + 0.3f);
            MyCamera.transform.position = new Vector3(MyCamera.transform.position.x, PictureCreater.instance.CameraY, MyCamera.transform.position.z);
            MyCamera.transform.rotation = new Quaternion(0, 0, 0, 0);
            MyCamera.transform.Rotate(39.52f * (PictureCreater.instance.MyUISliderValue + 0.3f), 0, 0);
            MyCamera.transform.LookAt(new Vector3(0, 0, 0));
        }
        if (UIRootExtend.instance.isUiRootRatio != 1f)
        {
            MyCamera.GetComponent<Camera>().fieldOfView = 60 * 1.2f;
        }
        else
        {
            MyCamera.GetComponent<Camera>().fieldOfView = 60;
        }
    }

    public void SetCameraValue()
    {
        PictureCreater.instance.MyUISliderValue = MyUISlider.value;
        SetCamera();
    }

    public IEnumerator ShowSkillEffect(int RoleID, int SkillID, int RoleIndex, bool IsEnemy)
    {
        //if (!IsEnemy)
        //{
        //    PictureCreater.instance.ListRolePicture[RoleIndex].RolePictureObject.layer = 10;
        //    foreach (Component c in PictureCreater.instance.ListRolePicture[RoleIndex].RoleObject.GetComponentsInChildren(typeof(SkinnedMeshRenderer), true))
        //    {
        //        c.gameObject.layer = 10;
        //    }

        //    foreach (Component c in PictureCreater.instance.ListRolePicture[RoleIndex].RoleObject.GetComponentsInChildren(typeof(MeshRenderer), true))
        //    {
        //        if (c.name != "Shadow")
        //        {
        //            c.gameObject.layer = 10;
        //        }
        //    }
        //}
        //else
        //{
        //    PictureCreater.instance.ListEnemyPicture[RoleIndex].RolePictureObject.layer = 10;
        //    foreach (Component c in PictureCreater.instance.ListEnemyPicture[RoleIndex].RoleObject.GetComponentsInChildren(typeof(SkinnedMeshRenderer), true))
        //    {
        //        c.gameObject.layer = 10;
        //    }

        //    foreach (Component c in PictureCreater.instance.ListEnemyPicture[RoleIndex].RoleObject.GetComponentsInChildren(typeof(MeshRenderer), true))
        //    {
        //        if (c.name != "Shadow")
        //        {
        //            c.gameObject.layer = 10;
        //        }
        //    }
        //}
        //FightMask.SetActive(true);

        //////////////////////小队技能//////////////////////
        CombinSkill CombineSkill = TextTranslator.instance.GetCombinSkillByID(RoleID);
        bool IsCombineSkill = false;
        int RandomSkill = 0;
        int HeroNum = 0;



        if ((!IsEnemy || NetworkHandler.instance.IsCreate || PictureCreater.instance.PVPString != "") && CombineSkill != null)
        {
            if (IsEnemy)
            {
                //Debug.LogError(PictureCreater.instance.PVPString);
                string[] dataSplit = PictureCreater.instance.PVPString.Split(';');
                for (int i = 0; i < dataSplit.Length - 1; i++)
                {
                    string[] secSplit = dataSplit[i].Split('$');
                    int cardID = int.Parse(secSplit[0]);
                    if (cardID == CombineSkill.HeroId2 ||
                        cardID == CombineSkill.HeroId3 ||
                        cardID == CombineSkill.HeroId4 ||
                        cardID == CombineSkill.HeroId5 ||
                        cardID == CombineSkill.HeroId6)
                    {
                        HeroNum++;
                        RandomSkill += 20;
                        //switch (HeroNum)
                        //{
                        //    case 1:
                        //        SkillEffect.transform.Find("SpriteBG/SpriteHero1").gameObject.GetComponent<UISprite>().spriteName = cardID.ToString();
                        //        break;
                        //    case 2:
                        //        SkillEffect.transform.Find("SpriteBG/SpriteHero2").gameObject.GetComponent<UISprite>().spriteName = cardID.ToString();
                        //        break;
                        //    case 3:
                        //        SkillEffect.transform.Find("SpriteBG/SpriteHero3").gameObject.GetComponent<UISprite>().spriteName = cardID.ToString();
                        //        break;
                        //    case 4:
                        //        SkillEffect.transform.Find("SpriteBG/SpriteHero4").gameObject.GetComponent<UISprite>().spriteName = cardID.ToString();
                        //        break;
                        //    case 5:
                        //        SkillEffect.transform.Find("SpriteBG/SpriteHero5").gameObject.GetComponent<UISprite>().spriteName = cardID.ToString();
                        //        break;
                        //}
                    }
                }
            }
            else
            {
                string[] dataSplit = FightPosition.Split('!');
                for (int i = 0; i < dataSplit.Length - 1; i++)
                {
                    string[] secSplit = dataSplit[i].Split('$');
                    int cardID = int.Parse(secSplit[0]);
                    if (cardID == CombineSkill.HeroId2 ||
                        cardID == CombineSkill.HeroId3 ||
                        cardID == CombineSkill.HeroId4 ||
                        cardID == CombineSkill.HeroId5 ||
                        cardID == CombineSkill.HeroId6)
                    {
                        HeroNum++;
                        RandomSkill += 20;
                        //switch (HeroNum)
                        //{
                        //case 1:
                        //    SkillEffect.transform.Find("SpriteBG/SpriteHero1").gameObject.GetComponent<UISprite>().spriteName = cardID.ToString();
                        //    break;
                        //case 2:
                        //    SkillEffect.transform.Find("SpriteBG/SpriteHero2").gameObject.GetComponent<UISprite>().spriteName = cardID.ToString();
                        //    break;
                        //case 3:
                        //    SkillEffect.transform.Find("SpriteBG/SpriteHero3").gameObject.GetComponent<UISprite>().spriteName = cardID.ToString();
                        //    break;
                        //case 4:
                        //    SkillEffect.transform.Find("SpriteBG/SpriteHero4").gameObject.GetComponent<UISprite>().spriteName = cardID.ToString();
                        //    break;
                        //case 5:
                        //    SkillEffect.transform.Find("SpriteBG/SpriteHero5").gameObject.GetComponent<UISprite>().spriteName = cardID.ToString();
                        //    break;
                        //}
                    }
                }
            }
            if (RandomSkill > 0 && (PictureCreater.instance.GetRandom(RoleID) < RandomSkill || NetworkHandler.instance.IsCreate || CharacterRecorder.instance.level <= 6))
            {
                /////////////////////////////小队buff/////////////////////////////
                float Random1 = PictureCreater.instance.GetRandom(RoleID) / 100f;
                float Random2 = PictureCreater.instance.GetRandom(RoleID) / 100f;
                float Random3 = PictureCreater.instance.GetRandom(RoleID) / 100f;
                float Random4 = PictureCreater.instance.GetRandom(RoleID) / 100f;

                List<PictureCreater.RolePicture> SetPicture = null;
                if (IsEnemy)
                {
                    SetPicture = PictureCreater.instance.ListEnemyPicture;
                }
                else
                {
                    if (PictureCreater.instance.HardLevel == 1)
                    {
                        PictureCreater.instance.IsCombineSkill = true;
                    }
                    SetPicture = PictureCreater.instance.ListRolePicture;
                }

                for (int i = 0; i < SetPicture.Count; i++)
                {
                    if (SetPicture[i].RolePosition > 0 && SetPicture[i].RoleNowBlood > 0)
                    {
                        if (SetPicture[i].RoleID == CombineSkill.HeroId1 ||
                            SetPicture[i].RoleID == CombineSkill.HeroId2 ||
                            SetPicture[i].RoleID == CombineSkill.HeroId3 ||
                            SetPicture[i].RoleID == CombineSkill.HeroId4 ||
                            SetPicture[i].RoleID == CombineSkill.HeroId5 ||
                            SetPicture[i].RoleID == CombineSkill.HeroId6)
                        {
                            if (!PictureCreater.instance.IsSkip)
                            {
                                GameObject Walk = GameObject.Instantiate(Resources.Load("Prefab/Effect/XingDong02", typeof(GameObject))) as GameObject;
                                Walk.transform.parent = SetPicture[i].RoleObject.transform;
                                Walk.transform.localPosition = Vector3.zero;
                                Walk.name = "TeamObject";
                            }
                        }
                    }
                }

                if (Random1 < CombineSkill.BuffRand1)
                {
                    if (CombineSkill.BuffId1 > 649)
                    {
                        Debug.LogError(CombineSkill.BuffId1);
                        Buff NewBuff = TextTranslator.instance.GetBuffByID(CombineSkill.BuffId1);
                        PictureCreater.instance.SetListBuff.Add(NewBuff);
                    }
                    else
                    {
                        if (CombineSkill.Param1 == 0)
                        {
                            PictureCreater.instance.SkillAddRoleBuff(IsEnemy, RoleIndex, CombineSkill.BuffId1);
                        }
                        else
                        {
                            if (IsEnemy)
                            {
                                for (int i = 0; i < PictureCreater.instance.ListEnemyPicture.Count; i++)
                                {
                                    PictureCreater.instance.SkillAddRoleBuff(IsEnemy, i, CombineSkill.BuffId1);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < PictureCreater.instance.ListRolePicture.Count; i++)
                                {
                                    PictureCreater.instance.SkillAddRoleBuff(IsEnemy, i, CombineSkill.BuffId1);
                                }
                            }
                        }
                    }
                }
                if (Random2 < CombineSkill.BuffRand2)
                {
                    if (CombineSkill.BuffId2 > 649)
                    {
                        Buff NewBuff = TextTranslator.instance.GetBuffByID(CombineSkill.BuffId2);
                        PictureCreater.instance.SetListBuff.Add(NewBuff);
                    }
                    else
                    {
                        if (CombineSkill.Param2 == 0)
                        {
                            PictureCreater.instance.SkillAddRoleBuff(IsEnemy, RoleIndex, CombineSkill.BuffId2);
                        }
                        else
                        {
                            if (IsEnemy)
                            {
                                for (int i = 0; i < PictureCreater.instance.ListEnemyPicture.Count; i++)
                                {
                                    PictureCreater.instance.SkillAddRoleBuff(IsEnemy, i, CombineSkill.BuffId2);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < PictureCreater.instance.ListRolePicture.Count; i++)
                                {
                                    PictureCreater.instance.SkillAddRoleBuff(IsEnemy, i, CombineSkill.BuffId2);
                                }
                            }
                        }
                    }
                }
                if (Random3 < CombineSkill.BuffRand3)
                {
                    if (CombineSkill.BuffId3 > 649)
                    {
                        Buff NewBuff = TextTranslator.instance.GetBuffByID(CombineSkill.BuffId3);
                        PictureCreater.instance.SetListBuff.Add(NewBuff);
                    }
                    else
                    {
                        if (CombineSkill.Param3 == 0)
                        {
                            PictureCreater.instance.SkillAddRoleBuff(IsEnemy, RoleIndex, CombineSkill.BuffId3);
                        }
                        else
                        {
                            if (IsEnemy)
                            {
                                for (int i = 0; i < PictureCreater.instance.ListEnemyPicture.Count; i++)
                                {
                                    PictureCreater.instance.SkillAddRoleBuff(IsEnemy, i, CombineSkill.BuffId3);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < PictureCreater.instance.ListRolePicture.Count; i++)
                                {
                                    PictureCreater.instance.SkillAddRoleBuff(IsEnemy, i, CombineSkill.BuffId3);
                                }
                            }
                        }
                    }
                }
                if (Random4 < CombineSkill.BuffRand4)
                {
                    if (CombineSkill.BuffId4 > 649)
                    {
                        Buff NewBuff = TextTranslator.instance.GetBuffByID(CombineSkill.BuffId4);
                        PictureCreater.instance.SetListBuff.Add(NewBuff);
                    }
                    else
                    {
                        if (CombineSkill.Param4 == 0)
                        {
                            PictureCreater.instance.SkillAddRoleBuff(IsEnemy, RoleIndex, CombineSkill.BuffId4);
                        }
                        else
                        {
                            if (IsEnemy)
                            {
                                for (int i = 0; i < PictureCreater.instance.ListEnemyPicture.Count; i++)
                                {
                                    PictureCreater.instance.SkillAddRoleBuff(IsEnemy, i, CombineSkill.BuffId4);
                                }
                            }
                            else
                            {
                                for (int i = 0; i < PictureCreater.instance.ListRolePicture.Count; i++)
                                {
                                    PictureCreater.instance.SkillAddRoleBuff(IsEnemy, i, CombineSkill.BuffId4);
                                }
                            }
                        }
                    }
                }



                /////////////////////////////小队buff/////////////////////////////

                IsCombineSkill = true;
                SkillID = CombineSkill.CombinSkillID;

                if (HeroNum > 0)
                {
                    SkillEffect.transform.Find("SpriteBG/SpriteHero1").gameObject.SetActive(true);
                }
                if (HeroNum > 1)
                {
                    SkillEffect.transform.Find("SpriteBG/SpriteHero2").gameObject.SetActive(true);
                }
                if (HeroNum > 2)
                {
                    SkillEffect.transform.Find("SpriteBG/SpriteHero3").gameObject.SetActive(true);
                }
                if (HeroNum > 3)
                {
                    SkillEffect.transform.Find("SpriteBG/SpriteHero4").gameObject.SetActive(true);
                }
                if (HeroNum > 4)
                {
                    SkillEffect.transform.Find("SpriteBG/SpriteHero5").gameObject.SetActive(true);
                }
            }
        }

        //////////////////////小队技能//////////////////////
        if (!PictureCreater.instance.IsSkip)
        {
            if (IsCombineSkill)
            {
                SkillEffect.SetActive(true);
                NGUITools.SetLayer(SkillEffect, 11);
                SkillEffect.transform.Find("TextureAvatar").gameObject.GetComponent<UITexture>().mainTexture = Resources.Load("Loading/" + (RoleID > 65000 ? (RoleID - 5000).ToString() : RoleID.ToString()), typeof(Texture)) as Texture;
                SkillEffect.transform.Find("TextureAvatar").gameObject.GetComponent<UITexture>().MakePixelPerfect();
                SkillEffect.transform.Find("TextureAvatar").gameObject.GetComponent<UITexture>().width = SkillEffect.transform.Find("TextureAvatar").gameObject.GetComponent<UITexture>().width * 8 / 10;
                SkillEffect.transform.Find("TextureAvatar").gameObject.GetComponent<UITexture>().height = SkillEffect.transform.Find("TextureAvatar").gameObject.GetComponent<UITexture>().height * 8 / 10;
                SkillEffect.transform.Find("SpriteBG/SpriteText").gameObject.GetComponent<UISprite>().spriteName = SkillID.ToString();
                SkillEffect.transform.Find("SpriteBG/LabelDesc").gameObject.GetComponent<UILabel>().text = CombineSkill.SkillDes.Replace("支援小队成员", "").Replace("并且", "").Replace("，", " ");
                SkillEffect.transform.Find("TextureAvatar").gameObject.GetComponent<TweenPosition>().enabled = true;
                //SkillEffect.transform.Find("SpriteText").gameObject.GetComponent<TweenPosition>().enabled = true;
                SkillEffect.transform.Find("SpriteBG").gameObject.GetComponent<TweenPosition>().enabled = true;
                SkillEffect.transform.Find("SpriteFire").gameObject.GetComponent<TweenPosition>().enabled = true;

                SkillEffect.transform.Find("TextureAvatar").gameObject.GetComponent<TweenPosition>().ResetToBeginning();
                //SkillEffect.transform.Find("SpriteText").gameObject.GetComponent<TweenPosition>().ResetToBeginning();
                SkillEffect.transform.Find("SpriteBG").gameObject.GetComponent<TweenPosition>().ResetToBeginning();
                SkillEffect.transform.Find("SpriteFire").gameObject.GetComponent<TweenPosition>().ResetToBeginning();
                //SkillEffect.transform.Find("SpriteBG/SpriteText").gameObject.GetComponent<UISprite>().MakePixelPerfect();

                GameObject go = GameObject.Instantiate(Resources.Load("Prefab/Effect/WF_UI_JiNengShiFang02_bf", typeof(GameObject)), Vector3.zero, Quaternion.identity) as GameObject;
                go.transform.parent = SkillEffect.transform.Find("SpriteBG").transform;
                go.transform.localPosition = Vector3.zero;
                go.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
                NGUITools.SetLayer(SkillEffect, 11);
                if (!IsEnemy && PictureCreater.instance.HardLevel == 1)
                {
                    HeroNum = 0;

                    GameObject Hero1 = GameObject.Instantiate(Resources.Load("Prefab/Role/" + CombineSkill.HeroId1.ToString(), typeof(GameObject))) as GameObject;
                    Hero1.name = "Hero1";
                    Hero1.transform.parent = Team1.transform;
                    HeroInfo hi = TextTranslator.instance.GetHeroInfoByHeroID(CombineSkill.HeroId1);
                    Hero1.transform.localScale = new Vector3(hi.heroScale, hi.heroScale, hi.heroScale);
                    Hero1.transform.localRotation = Quaternion.identity;
                    Hero1.transform.localPosition = Vector3.zero;
                    Hero1.GetComponent<Animator>().Play("idle");
                    foreach (Component c in Hero1.GetComponentsInChildren(typeof(MeshRenderer), true))
                    {
                        if (c.name.IndexOf("biyan") > -1)
                        {
                            c.gameObject.SetActive(false);
                            break;
                        }
                    }

                    string[] dataSplit = FightPosition.Split('!');
                    for (int i = 0; i < dataSplit.Length - 1; i++)
                    {
                        string[] secSplit = dataSplit[i].Split('$');
                        int cardID = int.Parse(secSplit[0]);
                        if (cardID == CombineSkill.HeroId2 ||
                            cardID == CombineSkill.HeroId3 ||
                            cardID == CombineSkill.HeroId4 ||
                            cardID == CombineSkill.HeroId5 ||
                            cardID == CombineSkill.HeroId6)
                        {
                            if (GameObject.Find("Role" + cardID.ToString()) == null)
                            {
                                continue;
                            }

                            HeroNum++;
                            GameObject HeroObject = null;
                            switch (HeroNum)
                            {
                                case 1:
                                    HeroObject = GameObject.Instantiate(Resources.Load("Prefab/Role/" + cardID.ToString(), typeof(GameObject))) as GameObject;
                                    HeroObject.name = "Hero2";
                                    HeroObject.transform.parent = Team2.transform;
                                    break;
                                case 2:
                                    HeroObject = GameObject.Instantiate(Resources.Load("Prefab/Role/" + cardID.ToString(), typeof(GameObject))) as GameObject;
                                    HeroObject.name = "Hero3";
                                    HeroObject.transform.parent = Team3.transform;

                                    break;
                                case 3:
                                    HeroObject = GameObject.Instantiate(Resources.Load("Prefab/Role/" + cardID.ToString(), typeof(GameObject))) as GameObject;
                                    HeroObject.name = "Hero4";
                                    HeroObject.transform.parent = Team4.transform;

                                    break;
                                case 4:
                                    HeroObject = GameObject.Instantiate(Resources.Load("Prefab/Role/" + cardID.ToString(), typeof(GameObject))) as GameObject;
                                    HeroObject.name = "Hero5";
                                    HeroObject.transform.parent = Team5.transform;

                                    break;
                            }

                            if (HeroObject != null)
                            {
                                hi = TextTranslator.instance.GetHeroInfoByHeroID(cardID);
                                HeroObject.transform.localScale = new Vector3(hi.heroScale, hi.heroScale, hi.heroScale);
                                HeroObject.transform.localRotation = Quaternion.identity;
                                HeroObject.transform.localPosition = Vector3.zero;
                                HeroObject.GetComponent<Animator>().Play("idle");
                                foreach (Component c in HeroObject.GetComponentsInChildren(typeof(MeshRenderer), true))
                                {
                                    if (c.name.IndexOf("biyan") > -1)
                                    {
                                        c.gameObject.SetActive(false);
                                        break;
                                    }
                                }
                            }
                        }
                    }



                    yield return new WaitForSeconds(0.5f);

                    bool IsShowExit = ExitButton.activeSelf;
                    ExitButton.SetActive(false);

                    TeamObject.SetActive(true);

                    PictureCreater.instance.PlayRoleSound(CombineSkill.HeroId1);

                    Hero1.GetComponent<Animator>().Play("attack");
                    //int Sound = UnityEngine.Random.Range(0, 99);
                    //if (Sound % 3 == 0)
                    //{
                    //    AudioEditer.instance.PlayOneShot("moveoutkeepfiring");
                    //}
                    //else if (Sound % 3 == 1)
                    //{
                    //    AudioEditer.instance.PlayOneShot("fireinthehole");
                    //}
                    //else
                    //{
                    //    AudioEditer.instance.PlayOneShot("coveringfire");
                    //}

                    yield return new WaitForSeconds(1.5f);
                    DestroyImmediate(GameObject.Find("Hero1"));
                    DestroyImmediate(GameObject.Find("Hero2"));
                    DestroyImmediate(GameObject.Find("Hero3"));
                    DestroyImmediate(GameObject.Find("Hero4"));
                    DestroyImmediate(GameObject.Find("Hero5"));
                    TeamObject.SetActive(false);

                    ExitButton.SetActive(IsShowExit);
                }
                else
                {
                    if (!IsEnemy)
                    {
                        GameObject go1 = GameObject.Instantiate(Resources.Load("Prefab/Effect/WF_Skill_Light", typeof(GameObject)), PictureCreater.instance.ListRolePicture[RoleIndex].RoleObject.transform.position, Quaternion.identity) as GameObject;
                    }
                    else
                    {
                        GameObject go1 = GameObject.Instantiate(Resources.Load("Prefab/Effect/WF_Skill_Light", typeof(GameObject)), PictureCreater.instance.ListEnemyPicture[RoleIndex].RoleObject.transform.position, Quaternion.identity) as GameObject;
                    }
                }
            }
            else
            {
                if (!IsEnemy)
                {
                    GameObject go1 = GameObject.Instantiate(Resources.Load("Prefab/Effect/WF_Skill_Light", typeof(GameObject)), PictureCreater.instance.ListRolePicture[RoleIndex].RoleObject.transform.position, Quaternion.identity) as GameObject;
                }
                else
                {
                    GameObject go1 = GameObject.Instantiate(Resources.Load("Prefab/Effect/WF_Skill_Light", typeof(GameObject)), PictureCreater.instance.ListEnemyPicture[RoleIndex].RoleObject.transform.position, Quaternion.identity) as GameObject;
                }
            }


            if (!IsEnemy && PictureCreater.instance.ListRolePicture[RoleIndex].RoleStage > 1)
            {
                yield return new WaitForSeconds(1f);
            }
            else if (IsEnemy && PictureCreater.instance.ListEnemyPicture[RoleIndex].RoleStage > 1)
            {
                yield return new WaitForSeconds(1f);
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    yield return new WaitForSeconds(0.01f);
                    if (!IsEnemy)
                    {
                        PictureCreater.instance.ListRolePicture[RoleIndex].RoleObject.transform.localScale += new Vector3(0.06f, 0.06f, 0.06f);
                    }
                    else
                    {
                        PictureCreater.instance.ListEnemyPicture[RoleIndex].RoleObject.transform.localScale += new Vector3(0.06f, 0.06f, 0.06f);
                    }
                }
                for (int i = 0; i < 1; i++)
                {
                    yield return new WaitForSeconds(0.01f);
                    if (!IsEnemy)
                    {
                        PictureCreater.instance.ListRolePicture[RoleIndex].RoleObject.transform.localScale -= new Vector3(0.06f, 0.06f, 0.06f);
                    }
                    else
                    {
                        PictureCreater.instance.ListEnemyPicture[RoleIndex].RoleObject.transform.localScale -= new Vector3(0.06f, 0.06f, 0.06f);
                    }
                }
            }

            //yield return new WaitForSeconds(0.4f);
            //FightMask.SetActive(false);
            //if (!IsEnemy)
            //{
            //    PictureCreater.instance.ListRolePicture[RoleIndex].RolePictureObject.layer = 8;
            //    foreach (Component c in PictureCreater.instance.ListRolePicture[RoleIndex].RoleObject.GetComponentsInChildren(typeof(SkinnedMeshRenderer), true))
            //    {
            //        c.gameObject.layer = 8;
            //    }

            //    foreach (Component c in PictureCreater.instance.ListRolePicture[RoleIndex].RoleObject.GetComponentsInChildren(typeof(MeshRenderer), true))
            //    {
            //        c.gameObject.layer = 8;
            //    }
            //}
            //else
            //{
            //    PictureCreater.instance.ListEnemyPicture[RoleIndex].RolePictureObject.layer = 8;
            //    foreach (Component c in PictureCreater.instance.ListEnemyPicture[RoleIndex].RoleObject.GetComponentsInChildren(typeof(SkinnedMeshRenderer), true))
            //    {
            //        c.gameObject.layer = 8;
            //    }

            //    foreach (Component c in PictureCreater.instance.ListEnemyPicture[RoleIndex].RoleObject.GetComponentsInChildren(typeof(MeshRenderer), true))
            //    {
            //        c.gameObject.layer = 8;
            //    }
            //}
            ////////////////////////////////////威思恪////////////////////////////////////
            if (!IsEnemy)
            {
                if (PictureCreater.instance.ListRolePicture[RoleIndex].RoleID == 60019)
                {
                    yield return new WaitForSeconds(0.6f);
                    Destroy(PictureCreater.instance.ListRolePicture[RoleIndex].RolePictureObject);
                    PictureCreater.instance.ListRolePicture[RoleIndex].RolePictureObject = GameObject.Instantiate(Resources.Load("Prefab/Role/65901", typeof(GameObject))) as GameObject;
                    PictureCreater.instance.ListRolePicture[RoleIndex].RolePictureObject.transform.parent = PictureCreater.instance.ListRolePicture[RoleIndex].RoleObject.transform;
                    PictureCreater.instance.ListRolePicture[RoleIndex].RolePictureObject.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
                    PictureCreater.instance.ListRolePicture[RoleIndex].RolePictureObject.transform.localPosition = Vector3.zero;
                    PictureCreater.instance.ListRolePicture[RoleIndex].RolePictureObject.transform.Rotate(0, 90, 0);
                    PictureCreater.instance.ListRolePicture[RoleIndex].RolePictureObject.name = "Role";
                    yield return new WaitForSeconds(0.2f);
                    PictureCreater.instance.ListRolePicture[RoleIndex].RolePictureObject.GetComponent<Animator>().Play("skill");
                    PictureCreater.instance.ListRolePicture[RoleIndex].RolePictureObject.transform.Find("Wesker").gameObject.GetComponent<Animator>().Play("skill");
                }
            }
            else
            {
                if (PictureCreater.instance.ListEnemyPicture[RoleIndex].RoleID == 60019)
                {
                    yield return new WaitForSeconds(0.6f);
                    Destroy(PictureCreater.instance.ListEnemyPicture[RoleIndex].RolePictureObject);
                    PictureCreater.instance.ListEnemyPicture[RoleIndex].RolePictureObject = GameObject.Instantiate(Resources.Load("Prefab/Role/65901", typeof(GameObject))) as GameObject;
                    PictureCreater.instance.ListEnemyPicture[RoleIndex].RolePictureObject.transform.parent = PictureCreater.instance.ListEnemyPicture[RoleIndex].RoleObject.transform;
                    PictureCreater.instance.ListEnemyPicture[RoleIndex].RolePictureObject.transform.localScale = new Vector3(1.6f, 1.6f, 1.6f);
                    PictureCreater.instance.ListEnemyPicture[RoleIndex].RolePictureObject.transform.localPosition = Vector3.zero;
                    PictureCreater.instance.ListEnemyPicture[RoleIndex].RolePictureObject.transform.Rotate(0, -90, 0);
                    PictureCreater.instance.ListEnemyPicture[RoleIndex].RolePictureObject.name = "Role";
                    yield return new WaitForSeconds(0.2f);
                    PictureCreater.instance.ListEnemyPicture[RoleIndex].RolePictureObject.GetComponent<Animator>().Play("skill");
                    PictureCreater.instance.ListEnemyPicture[RoleIndex].RolePictureObject.transform.Find("Wesker").gameObject.GetComponent<Animator>().Play("skill");
                }
            }
            ////////////////////////////////////威思恪////////////////////////////////////

            yield return new WaitForSeconds(1);

            if (IsCombineSkill)
            {
                SkillEffect.SetActive(false);
                SkillEffect.transform.Find("SpriteBG/SpriteHero1").gameObject.SetActive(false);
                SkillEffect.transform.Find("SpriteBG/SpriteHero2").gameObject.SetActive(false);
                SkillEffect.transform.Find("SpriteBG/SpriteHero3").gameObject.SetActive(false);
                SkillEffect.transform.Find("SpriteBG/SpriteHero4").gameObject.SetActive(false);
                SkillEffect.transform.Find("SpriteBG/SpriteHero5").gameObject.SetActive(false);
            }

            yield return new WaitForSeconds(0.7f);

            if (!IsEnemy && PictureCreater.instance.ListRolePicture[RoleIndex].RoleStage > 1)
            {
                yield return new WaitForSeconds(1f);
            }
            else if (IsEnemy && PictureCreater.instance.ListEnemyPicture[RoleIndex].RoleStage > 1)
            {
                yield return new WaitForSeconds(1f);
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    yield return new WaitForSeconds(0.01f);
                    if (!IsEnemy)
                    {
                        PictureCreater.instance.ListRolePicture[RoleIndex].RoleObject.transform.localScale -= new Vector3(0.06f, 0.06f, 0.06f);
                    }
                    else
                    {
                        PictureCreater.instance.ListEnemyPicture[RoleIndex].RoleObject.transform.localScale -= new Vector3(0.06f, 0.06f, 0.06f);
                    }
                }
            }
            if (!IsEnemy)
            {
                PictureCreater.instance.ListRolePicture[RoleIndex].RoleRedBloodObject.SetActive(false);
            }
            else
            {
                PictureCreater.instance.ListEnemyPicture[RoleIndex].RoleRedBloodObject.SetActive(false);
            }


            ////////////////////////////////////威思恪////////////////////////////////////
            if (!IsEnemy)
            {
                if (PictureCreater.instance.ListRolePicture[RoleIndex].RoleID == 60019)
                {
                    yield return new WaitForSeconds(0.5f);
                    Destroy(PictureCreater.instance.ListRolePicture[RoleIndex].RolePictureObject);
                    PictureCreater.instance.ListRolePicture[RoleIndex].RolePictureObject = GameObject.Instantiate(Resources.Load("Prefab/Role/60019", typeof(GameObject))) as GameObject;
                    PictureCreater.instance.ListRolePicture[RoleIndex].RolePictureObject.transform.parent = PictureCreater.instance.ListRolePicture[RoleIndex].RoleObject.transform;
                    PictureCreater.instance.ListRolePicture[RoleIndex].RolePictureObject.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
                    PictureCreater.instance.ListRolePicture[RoleIndex].RolePictureObject.transform.localPosition = Vector3.zero;
                    PictureCreater.instance.ListRolePicture[RoleIndex].RolePictureObject.transform.Rotate(0, 90, 0);
                    PictureCreater.instance.ListRolePicture[RoleIndex].RolePictureObject.name = "Role";
                }
            }
            else
            {
                if (PictureCreater.instance.ListEnemyPicture[RoleIndex].RoleID == 60019)
                {
                    yield return new WaitForSeconds(0.5f);
                    Destroy(PictureCreater.instance.ListEnemyPicture[RoleIndex].RolePictureObject);
                    PictureCreater.instance.ListEnemyPicture[RoleIndex].RolePictureObject = GameObject.Instantiate(Resources.Load("Prefab/Role/60019", typeof(GameObject))) as GameObject;
                    PictureCreater.instance.ListEnemyPicture[RoleIndex].RolePictureObject.transform.parent = PictureCreater.instance.ListEnemyPicture[RoleIndex].RoleObject.transform;
                    PictureCreater.instance.ListEnemyPicture[RoleIndex].RolePictureObject.transform.localScale = new Vector3(1.7f, 1.7f, 1.7f);
                    PictureCreater.instance.ListEnemyPicture[RoleIndex].RolePictureObject.transform.localPosition = Vector3.zero;
                    PictureCreater.instance.ListEnemyPicture[RoleIndex].RolePictureObject.transform.Rotate(0, -90, 0);
                    PictureCreater.instance.ListEnemyPicture[RoleIndex].RolePictureObject.name = "Role";
                }
            }
            ////////////////////////////////////威思恪////////////////////////////////////
        }
    }

    //点击显示HeroInfo 传入heroid
    public void SetIHeroInfo(int _RoleID)
    {
        if (CharacterRecorder.instance.IsShowHeroInfo == false)
        {
            CloseInfoButton.SetActive(true);
            HeroInfoObj.SetActive(true);
            HeroInfoObj.GetComponent<TweenPosition>().PlayForward();
            HeroInfo mheroInfo = TextTranslator.instance.GetHeroInfoByHeroID(_RoleID);
            HeroTexture.mainTexture = Resources.Load("Loading/" + _RoleID.ToString()) as Texture;
            HeroTexture.MakePixelPerfect();
            HeroTexture.GetComponent<UITexture>().width = HeroTexture.GetComponent<UITexture>().width * 2 / 3;
            HeroTexture.GetComponent<UITexture>().height = HeroTexture.GetComponent<UITexture>().height * 2 / 3;
            //SkillSprite.spriteName = mheroInfo.heroSkillList[0].ToString();
            SkillSprite.mainTexture = Resources.Load(string.Format("Skill/{0}", mheroInfo.heroSkillList[0]), typeof(Texture)) as Texture;
            SkillSprite2.mainTexture = Resources.Load(string.Format("Skill/{0}", mheroInfo.heroSkillList[1]), typeof(Texture)) as Texture;
            KindName.text = mheroInfo.careerShow;
            HeroName.text = mheroInfo.heroName;
            SkillDes.text = TextTranslator.instance.GetSkillByID(mheroInfo.heroSkillList[0], 1).description;
            KindDes.text = TextTranslator.instance.GetSkillByID(mheroInfo.heroSkillList[1], 1).description;
            StartCoroutine(ShowHeroInfo());
        }
    }
    public IEnumerator ShowHeroInfo()
    {
        yield return new WaitForSeconds(2f);
        UIEventListener.Get(CloseInfoButton).onClick += delegate(GameObject go)
        {
            CloseHeroInfo();
        };
        yield return new WaitForSeconds(3f);
        if (GameObject.Find("NewHeroInfo") != null)
        {
            CloseHeroInfo();
        }
    }
    public IEnumerator ShowEnemySkill(string SkillDes)
    {
        EnemyManualSkill.SetActive(true);
        LabelEnemyManualSkill.text = "";
        AudioEditer.instance.PlayOneShot("alarm");
        EnemyManualSkill.transform.Find("Icon").gameObject.GetComponent<UITexture>().mainTexture = (Texture2D)Resources.Load("GuideTexture/Victor1");
        yield return new WaitForSeconds(0.1f);
        EnemyManualSkill.transform.Find("Icon").gameObject.GetComponent<UITexture>().mainTexture = (Texture2D)Resources.Load("GuideTexture/Victor2");
        yield return new WaitForSeconds(0.1f);
        EnemyManualSkill.transform.Find("Icon").gameObject.GetComponent<UITexture>().mainTexture = (Texture2D)Resources.Load("GuideTexture/Victor3");
        EnemyManualSkill.transform.Find("SpriteKuang").gameObject.SetActive(true);
        LabelEnemyManualSkill.text = SkillDes;
        yield return new WaitForSeconds(2);
        EnemyManualSkill.transform.Find("SpriteKuang").gameObject.SetActive(false);
        EnemyManualSkill.SetActive(false);
    }

    public IEnumerator ShowHeroSkill(string HeroID)
    {
        KillHero.SetActive(true);
        KillHero.GetComponent<UITexture>().mainTexture = Resources.Load("Head/" + HeroID) as Texture;
        yield return new WaitForSeconds(1.5f);
        KillHero.GetComponent<UITexture>().mainTexture = Resources.Load("Game/blank") as Texture;
        KillHero.SetActive(false);
    }

    //#region  三星提示信息--关闭
    //public void isOpenStarMessage()
    //{
    //    if (UIEventListener.Get(GateDescription.transform.Find("UP").gameObject).onClick == null)
    //    {
    //        UIEventListener.Get(GateDescription.transform.Find("UP").gameObject).onClick += delegate(GameObject go)
    //        {
    //            ShrinkMessage.SetActive(true);
    //            GateDescription.GetComponent<TweenPosition>().PlayForward();
    //        };
    //    }
    //    if (UIEventListener.Get(ShrinkMessage.transform.Find("Down").gameObject).onClick == null)
    //    {
    //        UIEventListener.Get(ShrinkMessage.transform.Find("Down").gameObject).onClick += delegate(GameObject go)
    //        {
    //            ShrinkMessage.SetActive(false);
    //            GateDescription.GetComponent<TweenPosition>().PlayReverse();
    //        };
    //    }
    //    ShrinkMessage.gameObject.SetActive(false);
    //}

    //#endregion

    //新手引导数字高亮
    IEnumerator ShowSequenceNum()
    {
        while (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 10 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 4)
        {
            foreach (var p in PictureCreater.instance.ListRolePicture)
            {
                p.RoleObject.GetComponent<ColliderDisplayText>().BuffNum.alpha = 0.5f;
            }
            foreach (var p in PictureCreater.instance.ListEnemyPicture)
            {
                p.RoleObject.GetComponent<ColliderDisplayText>().BuffNum.alpha = 0.5f;
            }
            yield return new WaitForSeconds(0.3f);
            foreach (var p in PictureCreater.instance.ListRolePicture)
            {
                p.RoleObject.GetComponent<ColliderDisplayText>().BuffNum.alpha = 1f;
            }
            foreach (var p in PictureCreater.instance.ListEnemyPicture)
            {
                p.RoleObject.GetComponent<ColliderDisplayText>().BuffNum.alpha = 1f;
            }
            yield return new WaitForSeconds(0.3f);
        }

        while (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 10 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 5)
        {
            FightMask.SetActive(false);
            Repress.transform.FindChild("Sprite").gameObject.GetComponent<UISprite>().alpha = 0.5f;
            Repress4.GetComponent<UISprite>().alpha = 0.5f;
            yield return new WaitForSeconds(0.3f);
            Repress.transform.FindChild("Sprite").gameObject.GetComponent<UISprite>().alpha = 1;
            Repress4.GetComponent<UISprite>().alpha = 1;
            yield return new WaitForSeconds(0.3f);
        }
    }


    //弹幕君
    public void BulletScreenInfo()
    {
        UIEventListener.Get(isOpenBulletButton).onClick += delegate(GameObject go)
        {
            if (isOpenBullet)//isOpenBullet=true 弹幕关闭
            {
                BulletButtonInfo(true);
                CloseScreenInfo(true);
                PlayerPrefs.SetInt("BulletInfo" + PlayerPrefs.GetString("ServerID") + "_" + PlayerPrefs.GetInt("UserID"), 0);
            }
            else
            {
                BulletButtonInfo(false);
                PlayerPrefs.SetInt("BulletInfo" + PlayerPrefs.GetString("ServerID") + "_" + PlayerPrefs.GetInt("UserID"), 1);
                CloseScreenInfo(false);
            }
        };
        UIEventListener.Get(isWriteBulletButton).onClick += delegate(GameObject go)
        {
            if (!PictureCreater.instance.IsLock)
            {
                PictureCreater.instance.IsLock = true;
                BulletInputWindow.SetActive(true);
                BulletInputWindow.transform.Find("InputLabel").GetComponent<UIInput>().value = "";

            }
            else
            {
                PictureCreater.instance.IsLock = false;
                BulletInputWindow.SetActive(false);
            }

        };
        BulletInputWindowInfo();
        int BarrageCount = TextTranslator.instance.GetBarrageByCount(SceneTransformer.instance.NowGateID);
        if (BarrageCount > 1)
        {
            for (int i = 0; i < BarrageCount; i++)
            {
                GameObject label = Instantiate(BulletLabel) as GameObject;
                label.SetActive(false);
                label.transform.parent = gameObject.transform.Find("BulletScreenLabel").transform;
                label.transform.localScale = new Vector3(1, 1, 1);
                BulletList.Enqueue(label);
            }
            StartCoroutine(ScreenInfo());
            if (SceneTransformer.instance.CheckGuideIsFinish() && CharacterRecorder.instance.lastGateID >= 10005)
            {
                isOpenBulletButton.SetActive(true);
                if (PlayerPrefs.GetInt("BulletInfo" + PlayerPrefs.GetString("ServerID") + "_" + PlayerPrefs.GetInt("UserID")) != 1)//为1的时候弹幕关闭  0打开
                {
                    BulletButtonInfo(true);
                    CloseScreenInfo(true);
                }
                else
                {
                    BulletButtonInfo(false);
                    CloseScreenInfo(false);
                }
            }
        }
    }
    void BulletButtonInfo(bool isOpen)//弹幕按钮切换
    {
        isOpenBulletButton.transform.Find("BlackBulletButton").gameObject.SetActive(false);
        isOpenBulletButton.transform.Find("CloseBulletButton").gameObject.SetActive(false);
        if (isOpen)
        {
            isOpenBulletButton.GetComponent<UISprite>().spriteName = "danmu_buttondi1";
            isOpenBulletButton.transform.Find("CloseBulletButton").gameObject.SetActive(true);
            isWriteBulletButton.SetActive(true);
        }
        else
        {
            isOpenBulletButton.GetComponent<UISprite>().spriteName = "danmu_buttondi2";
            isOpenBulletButton.transform.Find("BlackBulletButton").gameObject.SetActive(true);
            isWriteBulletButton.SetActive(false);
        }
    }
    void BulletInputWindowInfo()
    {
        UIEventListener.Get(InputSetButton).onClick += delegate(GameObject go)
        {
            SelfBulletLabel = InputLabel.GetComponent<UILabel>().text;
            int num = 0;
            foreach (var str in SelfBulletLabel)
            {
                num++;
            }
            if (SelfBulletLabel == "")
            {
                UIManager.instance.OpenPromptWindow("弹幕不能为空", PromptWindow.PromptType.Hint, null, null);
            }
            else if (num > 20)
            {
                UIManager.instance.OpenPromptWindow("弹幕长度不能超过20个字", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                BulletInputWindow.SetActive(false);
                NetworkHandler.instance.SendProcess("7003#" + SelfBulletLabel + " ;");

            }
        };
    }
    public void SetSelfBulletInfo(string SelfBulletLabel)
    {
        PictureCreater.instance.IsLock = false;
        GameObject label = Instantiate(BulletLabel) as GameObject;
        label.SetActive(false);
        label.transform.parent = gameObject.transform.Find("BulletScreenLabel").transform;
        label.transform.localScale = new Vector3(1, 1, 1);
        label.SetActive(true);
        label.GetComponent<TweenPosition>().duration = Random.Range(8, 12);
        label.GetComponent<TweenPosition>().from = new Vector3(0, -560, 0);
        label.GetComponent<TweenPosition>().to = new Vector3(-2500, -560, 0);
        label.GetComponent<UILabel>().text = LabelColor(4) + SelfBulletLabel;
        BulletInfoList.Add(label);
        InputLabel.GetComponent<UILabel>().text = "请输入弹幕!";
    }
    IEnumerator ScreenInfo()
    {
        BulletInfoList.Clear();
        while (true)
        {
            GameObject item = BulletList.Dequeue();
            Barrage LabelItem = TextTranslator.instance.GetBarrageById(SceneTransformer.instance.NowGateID, BulletNumber);
            if (LabelItem == null)
            {
                break;
            }
            if (item)
            {
                item.SetActive(true);
                item.GetComponent<TweenPosition>().delay = LabelItem.OutTime;
                item.GetComponent<TweenPosition>().duration = Random.Range(8, 12);
                item.GetComponent<TweenPosition>().from = new Vector3(0, 0 - 50 * (LabelItem.Position % 10), 0);
                item.GetComponent<TweenPosition>().to = new Vector3(-2500, 0 - 50 * (LabelItem.Position % 10), 0);
                item.GetComponent<UILabel>().text = LabelColor(LabelItem.Color) + LabelItem.Des;
                BulletList.Enqueue(item);
                BulletInfoList.Add(item);
                BulletNumber++;
            }
        }
        yield return new WaitForSeconds(1f);

    }

    //打开关闭弹幕
    public void CloseScreenInfo(bool isOpen)
    {
        if (isOpen)//true打开弹幕
        {
            AllBulletLabel.SetActive(true);
            for (int i = 0; i < BulletInfoList.Count; i++)
            {
                BulletInfoList[i].GetComponent<UILabel>().alpha = 1;
            }
            isOpenBullet = false;

        }
        else
        {
            //AllBulletLabel.SetActive(false);
            for (int i = 0; i < BulletInfoList.Count; i++)
            {
                BulletInfoList[i].GetComponent<UILabel>().alpha = 0;
            }
            isOpenBullet = true;

        }
    }
    public string LabelColor(int GradeID)
    {
        string LabelColor = "";
        switch (GradeID)
        {
            case 1:
                LabelColor = "[-][FFFFFF]";//白色          
                break;
            case 2:
                LabelColor = "[-][28DF5E]";//绿色          
                break;
            case 3:
                LabelColor = "[-][FB2D50]";//红色
                break;
            case 4:
                LabelColor = "[-][FD792A]";//黄色 253 121 42
                break;
            case 5:
                LabelColor = "[-][969696]";//灰色
                break;
        }
        return LabelColor;
    }

    /// <summary>
    /// 丛林冒险自动战斗
    /// </summary>
    private IEnumerator AutoWoodFight() 
    {
        yield return new WaitForSeconds(2f);
        if (PlayerPrefs.GetInt("Automaticbrushfield" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID")) == 1) 
        {
            AutoClickAkeyButton();
            yield return new WaitForSeconds(1f);

            if (PictureCreater.instance.IsHand)//变成自动操作
            {
                PictureCreater.instance.IsHand = false;
                if (PictureCreater.instance.FightStyle == 2)
                {
                    PictureCreater.instance.IsLock = false;
                }
                HandButton.GetComponent<UISprite>().spriteName = "12button";
                PlayerPrefs.SetInt("Hand", 1);

                if (PictureCreater.instance.FightStyle == 2)
                {
                    LabelChange.text = "自动操作";
                }
                else
                {
                    LabelChange.text = "自动战术";
                }
            }
            yield return new WaitForSeconds(1f);

            AutoClickFightButton();
        }
    }

    /// <summary>
    /// 如果自动丛林战斗，开始时防止没上编队，一键自动上
    /// </summary>
    private void AutoClickAkeyButton() 
    {
        if (SceneTransformer.instance.CheckGuideIsFinish() == false)
        {

        }
        else
        {
            bool IsCaptain = false;
            for (int i = 0; i < PictureCreater.instance.ListRolePicture.Count; i++)
            {
                if (PictureCreater.instance.ListRolePicture[i].RolePosition > 0)
                {
                    IsCaptain = true;
                }
            }

            int PositionCount = 0;
            List<int> PositionFList = new List<int>();
            List<int> PositionSList = new List<int>();

            PositionFList.Add(18);
            PositionFList.Add(21);
            PositionFList.Add(15);
            PositionFList.Add(17);
            PositionFList.Add(14);
            PositionFList.Add(13);
            PositionFList.Add(16);
            PositionFList.Add(10);
            PositionFList.Add(12);
            PositionFList.Add(9);

            PositionSList.Add(13);
            PositionSList.Add(16);
            PositionSList.Add(10);
            PositionSList.Add(12);
            PositionSList.Add(9);
            PositionSList.Add(18);
            PositionSList.Add(21);
            PositionSList.Add(15);
            PositionSList.Add(17);
            PositionSList.Add(14);
            for (int i = 0; i < PictureCreater.instance.ListRolePicture.Count; i++)
            {
                if (PictureCreater.instance.ListRolePicture[i].RolePosition > 0 && !PictureCreater.instance.ListRolePicture[i].RolePictureNPC)
                {
                    PositionCount++;
                    switch (PictureCreater.instance.ListRolePicture[i].RolePosition)
                    {
                        case 18:
                            PositionFList.Remove(18);
                            PositionSList.Remove(18);
                            break;
                        case 21:
                            PositionFList.Remove(21);
                            PositionSList.Remove(21);
                            break;
                        case 15:
                            PositionFList.Remove(15);
                            PositionSList.Remove(15);
                            break;
                        case 17:
                            PositionFList.Remove(17);
                            PositionSList.Remove(17);
                            break;
                        case 14:
                            PositionFList.Remove(14);
                            PositionSList.Remove(14);
                            break;
                        case 13:
                            PositionSList.Remove(13);
                            PositionFList.Remove(13);
                            break;
                        case 16:
                            PositionSList.Remove(16);
                            PositionFList.Remove(16);
                            break;
                        case 10:
                            PositionSList.Remove(10);
                            PositionFList.Remove(10);
                            break;
                        case 12:
                            PositionSList.Remove(12);
                            PositionFList.Remove(12);
                            break;
                        case 9:
                            PositionSList.Remove(9);
                            PositionFList.Remove(9);
                            break;
                    }
                }
            }

            PositionCount = PictureCreater.instance.LimitHeroCount == 0 ? (CharacterRecorder.instance.level < 25 ? 5 : 6) - PositionCount : PictureCreater.instance.LimitHeroCount - PositionCount;

            List<int> RemoveList = new List<int>();
            for (int i = 0; i < PositionCount; i++)
            {
                for (int j = 0; j < PictureCreater.instance.ListRolePicture.Count; j++)
                {
                    if (ListHero.Count > i)
                    {
                        if (PictureCreater.instance.ListRolePicture[j].RoleID.ToString() == ListHero[i].name)
                        {
                            if (PictureCreater.instance.ListRolePicture[j].RoleNowBlood > 0 && PictureCreater.instance.LimitType != PictureCreater.instance.ListRolePicture[j].RoleCareer)
                            {
                                PictureCreater.instance.ListRolePicture[j].RoleObject.SetActive(true);
                                if (PictureCreater.instance.ListRolePicture[j].RoleArea == 1)
                                {
                                    PictureCreater.instance.ListRolePicture[j].RolePosition = PositionFList[0];
                                    PictureCreater.instance.ListRolePicture[j].RoleObject.transform.position = PictureCreater.instance.ListPosition[PositionFList[0]];
                                    PositionFList.RemoveAt(0);
                                }
                                else
                                {
                                    PictureCreater.instance.ListRolePicture[j].RolePosition = PositionSList[0];
                                    PictureCreater.instance.ListRolePicture[j].RoleObject.transform.position = PictureCreater.instance.ListPosition[PositionSList[0]];
                                    PositionSList.RemoveAt(0);
                                }
                                RemoveList.Add(i);
                            }
                            else
                            {
                                PositionCount++;
                            }
                            break;
                        }
                    }
                }
            }

            for (int i = RemoveList.Count - 1; i >= 0; i--)
            {
                DestroyImmediate(ListHero[RemoveList[i]]);
                ListHero.RemoveAt(RemoveList[i]);
            }



            if (!IsCaptain)
            {
                for (int i = 0; i < PictureCreater.instance.ListRolePicture.Count; i++)
                {
                    if (PictureCreater.instance.ListRolePicture[i].RolePosition == 18)
                    {
                        PictureCreater.instance.SetCaptain(PictureCreater.instance.ListRolePicture[i].RoleObject.name);
                        PictureCreater.instance.SelectPosition = 18;
                        break;
                    }
                }
            }
            MyGrid.GetComponent<UIGrid>().Reposition();
            PictureCreater.instance.SetSequence();
        }
    }

    private void AutoClickFightButton() 
    {
        if (CharacterRecorder.instance.lastGateID == 10006)
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1803);
        }
        else if (CharacterRecorder.instance.lastGateID == 10007)
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1903);
        }
        else if (CharacterRecorder.instance.lastGateID == 10008)
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2104);
        }
        else if (CharacterRecorder.instance.lastGateID == 10010)
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2402);
        }
        else if (CharacterRecorder.instance.lastGateID == 10011 && CharacterRecorder.instance.lastCreamGateID >= 20002)
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2604);
        }
        else if (CharacterRecorder.instance.lastGateID == 10021)
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_3801);
        }
        else if (PictureCreater.instance.FightStyle == 6)
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2809);
        }
        else if (PictureCreater.instance.FightStyle == 4)
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_3306);
        }
        else if (PictureCreater.instance.FightStyle == 5)
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_3405);
        }
        UIManager.instance.StartGate(UIManager.GateAnchorName.startFight);
        UIManager.instance.UmengStartLevel(SceneTransformer.instance.NowGateID.ToString());
        if (SceneTransformer.instance.CheckGuideIsFinish() == false)
        {
            if ((PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 0 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 3)
                || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 3 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 15)
                || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 6 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 6)
                || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 10 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 5)
                || (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 12 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 7)
                )
            {
                SceneTransformer.instance.NewGuideButtonClick();
            }
        }
        else
        {
            if (CharacterRecorder.instance.GuideID[0] == 4 || CharacterRecorder.instance.GuideID[35] == 2 || CharacterRecorder.instance.GuideID[27] == 3 || CharacterRecorder.instance.GuideID[37] == 2 || CharacterRecorder.instance.GuideID[10] == 10)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
        }
        bool isStart = false;
        if (SceneTransformer.instance.CheckGuideIsFinish())
        {
            if (CharacterRecorder.instance.GuideID[21] == 9)
            {
                CharacterRecorder.instance.GuideID[21] += 1;
            }
            else if (CharacterRecorder.instance.GuideID[9] == 13)
            {
                CharacterRecorder.instance.GuideID[9] = 14;
            }
            if (CharacterRecorder.instance.GuideID[0] == 3)
            {
                isStart = true;
            }
        }
        if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 3 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 13)
        {

        }
        else if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 6 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 6)
        {

        }
        else if (isStart == true)
        {

        }
        else
        {
            StartCoroutine(StartFight());
            if (PictureCreater.instance.FightStyle == 0 && !NetworkHandler.instance.IsCreate)
            {
                //ShrinkMessage.SetActive(true);
                //GateDescription.GetComponent<TweenPosition>().PlayForward();
                ShrinkMessage.SetActive(false);
            }
        }

        if (ObscuredPrefs.GetString("Account").IndexOf("test_") > -1)
        {
            GameTest.SetActive(true);
        }
    }
}
