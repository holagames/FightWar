using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionFightWindow : MonoBehaviour {

    public GameObject FightScene;
    public GameObject FightWindow;
    public GameObject BackButton;
    public GameObject AutoTroopButton;
    public GameObject AutoLifeButton;

    public GameObject LeftTopTexture;
    public GameObject RightTopTexture;

    public GameObject ResurrectionWindow;
    public GameObject EmbattleWindow;
    public GameObject CheckButton;
    public GameObject CancelButton;
    public GameObject LifeButton;
    public GameObject CloseButton;
    public UISprite ItemIcon;
    public UILabel ItemNumber;
    public GameObject[] TowerButton;
    
    private GameObject LegionWarScene;


    public GameObject OneRoad;
    public GameObject TwoRoad;
    public GameObject ThreeRoad;

    public GameObject GainResultPart;
    public GameObject EffectCG;
    public GameObject EffectSB;
    public GameObject EffectBZ;
    public GameObject EffectGuang;


    public GameObject BloodPart;
    public GameObject BloodItem;


    public GameObject LegionInfoItem;
    public GameObject LegionLeftInfo;
    public GameObject MaskLeft;
    public GameObject UiGridLeft;

    public GameObject LegionRightInfo;
    public GameObject MaskRight;
    public GameObject UiGridRight;

    //private List<LegionFightHeroInfo> FightHeroInfoList = new List<LegionFightHeroInfo>();
    public int LegionPoint = 0;
    public int ClickTowerNum = 0;

    private int ShouNum = 0;//守军数量
    private List<string> AllHeroNameList = new List<string>();

	void Start () {
        CharacterRecorder.instance.IsfirstEntLegion = true;
        NetworkHandler.instance.SendProcess("8608#" + CharacterRecorder.instance.LegionHarasPoint + ";");
        AddLegionFightScene();
        if (UIEventListener.Get(BackButton).onClick == null)
        {
            UIEventListener.Get(BackButton).onClick = delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("8620#" + CharacterRecorder.instance.LegionFestPosition + ";");
                PictureCreater.instance.DestroyAllComponent();
                UIManager.instance.BackUI();
            };
        }

        if (UIEventListener.Get(AutoTroopButton).onClick == null)
        {
            UIEventListener.Get(AutoTroopButton).onClick = delegate(GameObject go)
            {
                if (AutoTroopButton.transform.Find("Checkmark").gameObject.activeSelf)
                {
                    NetworkHandler.instance.SendProcess("8604#0;");
                }
                else 
                {
                    NetworkHandler.instance.SendProcess("8604#1;");
                }
            };
        }

        if (UIEventListener.Get(AutoLifeButton).onClick == null)
        {
            UIEventListener.Get(AutoLifeButton).onClick = delegate(GameObject go)
            {
                if (AutoLifeButton.transform.Find("Checkmark").gameObject.activeSelf)
                {
                    NetworkHandler.instance.SendProcess("8605#0;");
                }
                else
                {
                    NetworkHandler.instance.SendProcess("8605#1;");
                }
            };
        }

        if (UIEventListener.Get(LeftTopTexture).onClick == null)
        {
            UIEventListener.Get(LeftTopTexture).onClick = delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("8628#"+CharacterRecorder.instance.LegionHarasPoint+";"+"1;");
            };
        }
        if (UIEventListener.Get(RightTopTexture).onClick == null)
        {
            UIEventListener.Get(RightTopTexture).onClick = delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("8628#" + CharacterRecorder.instance.LegionHarasPoint + ";" + "0;");
            };
        }

        if (UIEventListener.Get(MaskLeft).onClick == null)
        {
            UIEventListener.Get(MaskLeft).onClick = delegate(GameObject go)
            {
                LegionLeftInfo.SetActive(false);
            };
        }

        if (UIEventListener.Get(MaskRight).onClick == null)
        {
            UIEventListener.Get(MaskRight).onClick = delegate(GameObject go)
            {
                LegionRightInfo.SetActive(false);
            };
        }
	}


    public void LegionWarAutofight(int isOpen) //8604 
    {
        if (isOpen == 1)
        {
            AutoTroopButton.transform.Find("Checkmark").gameObject.SetActive(true);
        }
        else
        {
            AutoTroopButton.transform.Find("Checkmark").gameObject.SetActive(false);
        }
    }

    public void LegionWarAutorevive(int isOpen) //8605
    {
        if (isOpen == 1)
        {
            AutoLifeButton.transform.Find("Checkmark").gameObject.SetActive(true);
        }
        else
        {
            AutoLifeButton.transform.Find("Checkmark").gameObject.SetActive(false);
        }
    }

    private void SetResurrectionWindow() 
    {
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick = delegate(GameObject go)
            {
                ResurrectionWindow.SetActive(false);
            };
        }
        if (UIEventListener.Get(CancelButton).onClick == null)//占领
        {
            UIEventListener.Get(CancelButton).onClick = delegate(GameObject go)
            {

            };
        }

        if (UIEventListener.Get(LifeButton).onClick == null)//占领
        {
            UIEventListener.Get(LifeButton).onClick = delegate(GameObject go)
            {

            };
        }

        if (UIEventListener.Get(CheckButton).onClick == null)//占领
        {
            UIEventListener.Get(CheckButton).onClick = delegate(GameObject go)
            {
                if (CheckButton.transform.Find("Checkmark").gameObject.activeSelf == false)
                {
                    CheckButton.transform.Find("Checkmark").gameObject.SetActive(true);
                }
                else 
                {
                    CheckButton.transform.Find("Checkmark").gameObject.SetActive(false);
                }
            };
        }
    }

    private void AddLegionFightScene() //加载战斗场景
    {
        LegionWarScene = Instantiate(Resources.Load("Prefab/Scene/Map_LegionWar")) as GameObject;
        LegionWarScene.transform.parent = FightScene.transform;
        LegionWarScene.transform.Rotate(new Vector3(0, 0f, 0));
        LegionWarScene.transform.localScale = new Vector3(400f, 400f, 400f);
        LegionWarScene.transform.localPosition = new Vector3(0, 0, 0);
        RenderSettings.fog = false;
        LightMapAsset lightAsset = Resources.Load("LightMapAsset/lightMapAsset_10") as LightMapAsset;
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
    }


    private void SetFightMapInfo(int position)//position 0gong  1守 //int _LegionPoint,
    {
        if (position == 0)
        {
            UIEventListener.Get(TowerButton[0]).onClick = delegate(GameObject go)
            {
                ClickTowerNum = 1;
                NetworkHandler.instance.SendProcess("8623#;");
            };
            UIEventListener.Get(TowerButton[1]).onClick = delegate(GameObject go)
            {
                ClickTowerNum = 2;
                NetworkHandler.instance.SendProcess("8623#;");
            };
            UIEventListener.Get(TowerButton[2]).onClick = delegate(GameObject go)
            {
                ClickTowerNum = 3;
                NetworkHandler.instance.SendProcess("8623#;");
            };
        }
        else 
        {
            UIEventListener.Get(TowerButton[3]).onClick = delegate(GameObject go)
            {
                ClickTowerNum = 4;
                NetworkHandler.instance.SendProcess("8623#;");
            };
            UIEventListener.Get(TowerButton[4]).onClick = delegate(GameObject go)
            {
                ClickTowerNum = 5;
                NetworkHandler.instance.SendProcess("8623#;");
            };
            UIEventListener.Get(TowerButton[5]).onClick = delegate(GameObject go)
            {
                ClickTowerNum = 6;
                NetworkHandler.instance.SendProcess("8623#;");
            };
        }
    }

    private void SetAnyRoadLegionNum(int Num1,int Num2,int Num3,int Num4,int Num5,int Num6) //每一条路的军团数量
    {
        TowerButton[0].transform.Find("Label").GetComponent<UILabel>().text = Num1.ToString();
        TowerButton[1].transform.Find("Label").GetComponent<UILabel>().text = Num2.ToString();
        TowerButton[2].transform.Find("Label").GetComponent<UILabel>().text = Num3.ToString();
        TowerButton[3].transform.Find("Label").GetComponent<UILabel>().text = Num4.ToString();
        TowerButton[4].transform.Find("Label").GetComponent<UILabel>().text = Num5.ToString();
        TowerButton[5].transform.Find("Label").GetComponent<UILabel>().text = Num6.ToString();
    }


    public void LegionWarGetWarNoFight(string Recving) //8608 非战斗中进入场景，宣战状态下进入
    {
        string[] dataSplit = Recving.Split(';');

        string[] trcSplit4 = dataSplit[3].Split('$');
        string[] trcSplit5 = dataSplit[4].Split('$');



        LeftTopTexture.transform.Find("LeftLegionName").GetComponent<UILabel>().text = trcSplit4[0];
        LeftTopTexture.transform.Find("LeftLegionNum").GetComponent<UILabel>().text = "守备军：" + (int.Parse(trcSplit4[1]) + int.Parse(trcSplit4[2]) + int.Parse(trcSplit4[3])).ToString();

        RightTopTexture.transform.Find("RightLegionName").GetComponent<UILabel>().text = trcSplit5[0];
        RightTopTexture.transform.Find("RightLegionNum").GetComponent<UILabel>().text = "守备军：" + (int.Parse(trcSplit5[1]) + int.Parse(trcSplit5[2]) + int.Parse(trcSplit5[3])).ToString();

        ShouNum = int.Parse(trcSplit5[1]) + int.Parse(trcSplit5[2]) + int.Parse(trcSplit5[3]);
        if (CharacterRecorder.instance.myLegionData.legionName == trcSplit4[0])
        {
            SetFightMapInfo(0);
        }
        else if (CharacterRecorder.instance.myLegionData.legionName == trcSplit5[0])
        {
            SetFightMapInfo(1);
        }

        SetAnyRoadLegionNum(int.Parse(trcSplit4[1]), int.Parse(trcSplit4[2]), int.Parse(trcSplit4[3]), int.Parse(trcSplit5[1]), int.Parse(trcSplit5[2]), int.Parse(trcSplit5[3]));

        //AutoTroopButton.GetComponent<BoxCollider>().enabled = false;
        //AutoLifeButton.GetComponent<BoxCollider>().enabled = false;

    }




    public void LegionWarGetWar(string Recving) //8608 第一次进入，强制显示位置
    {
        //Recving = "1$ceshi1$60017$you$60020;2$ceshi1$60018$you$60021;3$ceshi1$60019$you$60022;juntuan1$10;juntuan2$10;";
        CharacterRecorder.instance.IsfirstEntLegion = false;
        string[] dataSplit = Recving.Split(';');

        string[] trcSplit1 = dataSplit[0].Split('$');
        string[] trcSplit2 = dataSplit[1].Split('$');
        string[] trcSplit3 = dataSplit[2].Split('$');
        string[] trcSplit4 = dataSplit[3].Split('$');
        string[] trcSplit5 = dataSplit[4].Split('$');

        if (trcSplit1[1] != "") 
        {
            AddHeroByRoleID(0, 1, int.Parse(trcSplit1[0]), trcSplit1[1], int.Parse(trcSplit1[2]), -1,int.Parse(trcSplit1[3]),int.Parse(trcSplit1[4]));//1路攻
        }
        if (trcSplit1[5] != "")
        {
            AddHeroByRoleID(1, 1, int.Parse(trcSplit1[0]), trcSplit1[5], int.Parse(trcSplit1[6]), -1, int.Parse(trcSplit1[7]), int.Parse(trcSplit1[8]));//1路防
        }



        if (trcSplit2[1] != "")
        {
            AddHeroByRoleID(0, 2, int.Parse(trcSplit2[0]), trcSplit2[1], int.Parse(trcSplit2[2]), -1, int.Parse(trcSplit2[3]), int.Parse(trcSplit2[4]));//2路攻
        }
        if (trcSplit2[5] != "")
        {
            AddHeroByRoleID(1, 2, int.Parse(trcSplit2[0]), trcSplit2[5], int.Parse(trcSplit2[6]), -1, int.Parse(trcSplit2[7]), int.Parse(trcSplit2[8]));//2路防
        }


        if (trcSplit3[1] != "")
        {
            AddHeroByRoleID(0, 3, int.Parse(trcSplit3[0]), trcSplit3[1], int.Parse(trcSplit3[2]), -1, int.Parse(trcSplit3[3]), int.Parse(trcSplit3[4]));//3路攻
        }
        if (trcSplit3[5] != "")
        {
            AddHeroByRoleID(1, 3, int.Parse(trcSplit3[0]), trcSplit3[5], int.Parse(trcSplit3[6]), -1, int.Parse(trcSplit3[7]), int.Parse(trcSplit3[8]));//3路防
        }


        int leftnum = int.Parse(trcSplit4[1]) + int.Parse(trcSplit4[2]) + int.Parse(trcSplit4[3]);
        int rightnum = int.Parse(trcSplit5[1]) + int.Parse(trcSplit5[2]) + int.Parse(trcSplit5[3]);

        LeftTopTexture.transform.Find("LeftLegionName").GetComponent<UILabel>().text = trcSplit4[0];
        LeftTopTexture.transform.Find("LeftLegionNum").GetComponent<UILabel>().text = "守备军：" + leftnum.ToString();

        RightTopTexture.transform.Find("RightLegionName").GetComponent<UILabel>().text = trcSplit5[0];
        RightTopTexture.transform.Find("RightLegionNum").GetComponent<UILabel>().text = "守备军：" + rightnum.ToString();

        ShouNum = rightnum;
        if (CharacterRecorder.instance.myLegionData.legionName== trcSplit4[0])
        {
            SetFightMapInfo(0);
        }
        else if (CharacterRecorder.instance.myLegionData.legionName == trcSplit5[0])
        {
            SetFightMapInfo(1);
        }
        else//观战，不能点击自动复活，战斗
        {
            AutoTroopButton.GetComponent<BoxCollider>().enabled = false;
            AutoLifeButton.GetComponent<BoxCollider>().enabled = false;
        }

        SetAnyRoadLegionNum(int.Parse(trcSplit4[1]), int.Parse(trcSplit4[2]), int.Parse(trcSplit4[3]), int.Parse(trcSplit5[1]), int.Parse(trcSplit5[2]), int.Parse(trcSplit5[3]));

        if (leftnum == 0 || rightnum == 0)
        {
            GetWhoStartWar(leftnum, rightnum, trcSplit4[0], trcSplit5[0]);
        }
    }

    public void LegionWarGetWarMoce(string Recving) //8608 多次进入
    {
        string[] dataSplit = Recving.Split(';');

        string[] trcSplit1 = dataSplit[0].Split('$');
        string[] trcSplit2 = dataSplit[1].Split('$');
        string[] trcSplit3 = dataSplit[2].Split('$');
        string[] trcSplit4 = dataSplit[3].Split('$');
        string[] trcSplit5 = dataSplit[4].Split('$');

        #region    强制删除8609可能没有删除的在场英雄

        AllHeroNameList.Clear();
        AllHeroNameList.Add((trcSplit1[1] + trcSplit1[2]));
        AllHeroNameList.Add((trcSplit1[5] + trcSplit1[6]));

        AllHeroNameList.Add((trcSplit2[1] + trcSplit2[2]));
        AllHeroNameList.Add((trcSplit2[5] + trcSplit2[6]));

        AllHeroNameList.Add((trcSplit3[1] + trcSplit3[2]));
        AllHeroNameList.Add((trcSplit3[5] + trcSplit3[6]));

        for (int i = PictureCreater.instance.ListRolePicture.Count - 1; i >= 0; i--)
        {
            if (PictureCreater.instance.ListRolePicture[i].RoleObject.name != null) 
            {
                string name = PictureCreater.instance.ListRolePicture[i].RoleObject.name;
                string name2 = name + "Blood";
                if (AllHeroNameList.Contains(name) == false)
                {
                    Debug.Log("删除+" + name);
                    if (BloodPart.transform.Find(name2) != null) 
                    {
                        DestroyImmediate(BloodPart.transform.Find(name2).gameObject);
                    }
                    PictureCreater.instance.ListRolePicture[i].RoleFightIndex.Clear();
                    PictureCreater.instance.ListRolePicture[i].RolePictureTraceLoop = false;
                    DestroyImmediate(PictureCreater.instance.ListRolePicture[i].RolePictureObject);
                    DestroyImmediate(PictureCreater.instance.ListRolePicture[i].RoleTargetObject);
                    DestroyImmediate(PictureCreater.instance.ListRolePicture[i].RoleNameObject);
                    DestroyImmediate(PictureCreater.instance.ListRolePicture[i].RoleTaskObject);
                    DestroyImmediate(PictureCreater.instance.ListRolePicture[i].RoleBlackBloodObject);
                    DestroyImmediate(PictureCreater.instance.ListRolePicture[i].RoleRedBloodObject);
                    if (PictureCreater.instance.ListRolePicture[i].RoleObject.GetComponent<ColliderDisplayText>() != null)
                    {
                        DestroyImmediate(PictureCreater.instance.ListRolePicture[i].RoleObject.GetComponent<ColliderDisplayText>().mText);
                    }
                    DestroyImmediate(PictureCreater.instance.ListRolePicture[i].RoleObject);
                    PictureCreater.instance.ListRolePicture.RemoveAt(i);
                }
            }
        }

        #endregion


        if (trcSplit1[1] != "") //&&trcSplit5[1]!="0"
        {
            if (HeroRoleidIsExist(trcSplit1[1], int.Parse(trcSplit1[2]))) // 1路攻
            {
                StartCoroutine(HeroTomove(0, 1, int.Parse(trcSplit1[0]), trcSplit1[1], int.Parse(trcSplit1[2]), int.Parse(trcSplit1[3]), int.Parse(trcSplit1[4])));
                Debug.Log("1路攻活着,移动英雄:" + trcSplit1[1] + trcSplit1[2]);
            }
            else
            {
                AddHeroByRoleID(0, 1, 1, trcSplit1[1], int.Parse(trcSplit1[2]), -1, int.Parse(trcSplit1[3]), int.Parse(trcSplit1[4]));
                StartCoroutine(HeroTomove(0, 1, int.Parse(trcSplit1[0]), trcSplit1[1], int.Parse(trcSplit1[2]), int.Parse(trcSplit1[3]), int.Parse(trcSplit1[4])));
                Debug.Log("1路攻死了,创建新英雄:" + trcSplit1[1] + trcSplit1[2]);
            }
        }

        if (trcSplit1[5] != "")// && trcSplit4[1] != "0")
        {
            if (HeroRoleidIsExist(trcSplit1[5], int.Parse(trcSplit1[6]))) // 1路防
            {
                StartCoroutine(HeroTomove(1, 1, int.Parse(trcSplit1[0]), trcSplit1[5], int.Parse(trcSplit1[6]), int.Parse(trcSplit1[7]), int.Parse(trcSplit1[8])));
                Debug.Log("1路防活着,移动英雄:" + trcSplit1[5] + trcSplit1[6]);
            }
            else
            {
                AddHeroByRoleID(1, 1, 3, trcSplit1[5], int.Parse(trcSplit1[6]), -1, int.Parse(trcSplit1[7]), int.Parse(trcSplit1[8]));
                StartCoroutine(HeroTomove(1, 1, int.Parse(trcSplit1[0]), trcSplit1[5], int.Parse(trcSplit1[6]), int.Parse(trcSplit1[7]), int.Parse(trcSplit1[8])));
                Debug.Log("1路防死了,创建新英雄:" + trcSplit1[5] + trcSplit1[6]);
            }
        }



        if (trcSplit2[1] != "")//&&trcSplit5[2]!="0") 
        {
            if (HeroRoleidIsExist(trcSplit2[1], int.Parse(trcSplit2[2]))) // 2路攻
            {
                StartCoroutine(HeroTomove(0, 2, int.Parse(trcSplit2[0]), trcSplit2[1], int.Parse(trcSplit2[2]), int.Parse(trcSplit2[3]), int.Parse(trcSplit2[4])));
                Debug.Log("2路攻活着,移动英雄:" + trcSplit2[1] + trcSplit2[2]);
            }
            else if (trcSplit2[1] != "")
            {
                AddHeroByRoleID(0, 2, 1, trcSplit2[1], int.Parse(trcSplit2[2]), -1, int.Parse(trcSplit2[3]), int.Parse(trcSplit2[4]));
                StartCoroutine(HeroTomove(0, 2, int.Parse(trcSplit2[0]), trcSplit2[1], int.Parse(trcSplit2[2]), int.Parse(trcSplit2[3]), int.Parse(trcSplit2[4])));
                Debug.Log("2路攻死了,创建新英雄:" + trcSplit2[1] + trcSplit2[2]);
            }
        }

        if (trcSplit2[5] != "") //&& trcSplit4[2] != "0"
        {
            if (HeroRoleidIsExist(trcSplit2[5], int.Parse(trcSplit2[6]))) // 2路防
            {
                StartCoroutine(HeroTomove(1, 2, int.Parse(trcSplit2[0]), trcSplit2[5], int.Parse(trcSplit2[6]), int.Parse(trcSplit2[7]), int.Parse(trcSplit2[8])));
                Debug.Log("2路防活着,移动英雄:" + trcSplit2[5] + trcSplit2[6]);
            }
            else if (trcSplit2[5] != "")
            {
                AddHeroByRoleID(1, 2, 3, trcSplit2[5], int.Parse(trcSplit2[6]), -1, int.Parse(trcSplit2[7]), int.Parse(trcSplit2[8]));
                StartCoroutine(HeroTomove(1, 2, int.Parse(trcSplit2[0]), trcSplit2[5], int.Parse(trcSplit2[6]), int.Parse(trcSplit2[7]), int.Parse(trcSplit2[8])));
                Debug.Log("2路防死了,创建新英雄:" + trcSplit2[5] + trcSplit2[6]);
            }
        }


        if (trcSplit3[1] != "")// && trcSplit5[3] != "0") 
        {
            if (HeroRoleidIsExist(trcSplit3[1], int.Parse(trcSplit3[2]))) // 3路攻
            {
                StartCoroutine(HeroTomove(0, 3, int.Parse(trcSplit3[0]), trcSplit3[1], int.Parse(trcSplit3[2]), int.Parse(trcSplit3[3]), int.Parse(trcSplit3[4])));
                Debug.Log("3路攻活着,移动英雄:" + trcSplit3[1] + trcSplit3[2]);
            }
            else if (trcSplit3[1] != "")
            {
                AddHeroByRoleID(0, 3, 1, trcSplit3[1], int.Parse(trcSplit3[2]), -1, int.Parse(trcSplit3[3]), int.Parse(trcSplit3[4]));
                StartCoroutine(HeroTomove(0, 3, int.Parse(trcSplit3[0]), trcSplit3[1], int.Parse(trcSplit3[2]), int.Parse(trcSplit3[3]), int.Parse(trcSplit3[4])));
                Debug.Log("3路攻死了,创建新英雄:" + trcSplit3[1] + trcSplit3[2]);
            }
        }


        if (trcSplit3[5] != "")// && trcSplit4[3] != "0")
        {
            if (HeroRoleidIsExist(trcSplit3[5], int.Parse(trcSplit3[6]))) // 3路防
            {
                StartCoroutine(HeroTomove(1, 3, int.Parse(trcSplit3[0]), trcSplit3[5], int.Parse(trcSplit3[6]), int.Parse(trcSplit3[7]), int.Parse(trcSplit3[8])));
                Debug.Log("3路防活着,移动英雄:" + trcSplit3[5] + trcSplit3[6]);
            }
            else if (trcSplit3[5] != "")
            {
                AddHeroByRoleID(1, 3, 3, trcSplit3[5], int.Parse(trcSplit3[6]), -1, int.Parse(trcSplit3[7]), int.Parse(trcSplit3[8]));
                StartCoroutine(HeroTomove(1, 3, int.Parse(trcSplit3[0]), trcSplit3[5], int.Parse(trcSplit3[6]), int.Parse(trcSplit3[7]), int.Parse(trcSplit3[8])));
                Debug.Log("3路防死了,创建新英雄:" + trcSplit3[5] + trcSplit3[6]);
            }
        }

        int leftnum = int.Parse(trcSplit4[1]) + int.Parse(trcSplit4[2]) + int.Parse(trcSplit4[3]);
        int rightnum = int.Parse(trcSplit5[1]) + int.Parse(trcSplit5[2]) + int.Parse(trcSplit5[3]);

        LeftTopTexture.transform.Find("LeftLegionName").GetComponent<UILabel>().text = trcSplit4[0];
        LeftTopTexture.transform.Find("LeftLegionNum").GetComponent<UILabel>().text = "守备军：" + leftnum.ToString();

        RightTopTexture.transform.Find("RightLegionName").GetComponent<UILabel>().text = trcSplit5[0];
        RightTopTexture.transform.Find("RightLegionNum").GetComponent<UILabel>().text = "守备军：" + rightnum.ToString();
        ShouNum = rightnum;

        SetAnyRoadLegionNum(int.Parse(trcSplit4[1]), int.Parse(trcSplit4[2]), int.Parse(trcSplit4[3]), int.Parse(trcSplit5[1]), int.Parse(trcSplit5[2]), int.Parse(trcSplit5[3]));
        if (leftnum == 0 || rightnum == 0)
        {
            GetWhoStartWar(leftnum, rightnum, trcSplit4[0], trcSplit5[0]);
        }
    }


    private IEnumerator HeroTomove(int IsAtack, int WitchRoad, int RoadPosition, string CharacterName, int RoleID, int BloodNum, int MaxBloodNum) //移动英雄
    {

        GameObject go = null;// new GameObject();
        GameObject goBlood = null;
        PictureCreater.RolePicture RP = new PictureCreater.RolePicture();
        LegionFightHeroInfo LH;
        int index = 0;
        for (int i = 0; i < PictureCreater.instance.ListRolePicture.Count; i++) 
        {
            if (PictureCreater.instance.ListRolePicture[i].RoleObject.name == (CharacterName + RoleID.ToString())) 
            {
                LH=PictureCreater.instance.ListRolePicture[i].RoleObject.GetComponent<LegionFightHeroInfo>();
                LH.SetHeroPosition(RoadPosition);//下次相遇的点
                go = PictureCreater.instance.ListRolePicture[i].RoleObject;
                goBlood = LH.BloodInfo;
                RP = PictureCreater.instance.ListRolePicture[i];
                index = i;
                break;
            }
        }

        string PositionStr = WitchRoad.ToString() + RoadPosition.ToString();

        Vector3 VecPosition = HeroPosition(IsAtack,WitchRoad,RoadPosition);
        Vector3 BloodPosition = HeroBloodPosition(IsAtack, WitchRoad, RoadPosition);
       
        float num=go.transform.localPosition.x - VecPosition.x;
        float num2 = goBlood.transform.localPosition.x - BloodPosition.x;

        if (num < 0 && IsAtack == 0) //攻向右移动
        {
            RP.RolePictureObject.GetComponent<Animator>().SetFloat("ft", 0);
            float stepNum = (-num / 150);
            float stepNum2 = (-num2 / 150);
            while (go.transform.localPosition.x < VecPosition.x)
            {
                go.transform.localPosition = new Vector3(go.transform.localPosition.x + stepNum, VecPosition.y, VecPosition.z);
                goBlood.transform.localPosition = new Vector3(goBlood.transform.localPosition.x + stepNum2, BloodPosition.y, BloodPosition.z);

                if (go.transform.localPosition.x >= VecPosition.x)
                {
                    go.transform.localPosition = VecPosition;
                    goBlood.transform.localPosition = BloodPosition;
                    RP.RolePictureObject.GetComponent<Animator>().SetFloat("ft", 2);
                    RP.RolePictureObject.GetComponent<Animator>().Play("attack");
                    FightMotion fm2 = TextTranslator.instance.fightMotionDic[RP.RoleID * 10 + 1];
                    PictureCreater.instance.PlayEffect(PictureCreater.instance.ListRolePicture, index, fm2);     
                    break;
                }
                yield return new WaitForSeconds(0.01f);
            }
        }
        else if (num > 0 && IsAtack == 1)//守向左移动
        {
            RP.RolePictureObject.GetComponent<Animator>().SetFloat("ft", 0);
            float stepNum = num / 150;
            float stepNum2 = num2 / 150;

            while (go.transform.localPosition.x > VecPosition.x)
            {
                go.transform.localPosition = new Vector3(go.transform.localPosition.x - stepNum, VecPosition.y, VecPosition.z);
                goBlood.transform.localPosition = new Vector3(goBlood.transform.localPosition.x - stepNum2, BloodPosition.y, BloodPosition.z);


                if (go.transform.localPosition.x <= VecPosition.x)
                {
                    go.transform.localPosition = VecPosition;
                    goBlood.transform.localPosition = BloodPosition;

                    RP.RolePictureObject.GetComponent<Animator>().SetFloat("ft", 2);
                    RP.RolePictureObject.GetComponent<Animator>().Play("attack");
                    FightMotion fm2 = TextTranslator.instance.fightMotionDic[RP.RoleID * 10 + 1];
                    PictureCreater.instance.PlayEffect(PictureCreater.instance.ListRolePicture, index, fm2);    
                    break;
                }
                yield return new WaitForSeconds(0.01f);
            }
        }
        else if (num == 0) //原地等待攻击
        {
            yield return new WaitForSeconds(1.7f);
            RP.RolePictureObject.GetComponent<Animator>().SetFloat("ft", 2);
            RP.RolePictureObject.GetComponent<Animator>().Play("attack");
            FightMotion fm2 = TextTranslator.instance.fightMotionDic[RP.RoleID * 10 + 1];
            PictureCreater.instance.PlayEffect(PictureCreater.instance.ListRolePicture, index, fm2);    
        }
        
    }

    private void AddHeroByRoleID(int IsAtack, int WitchRoad, int RoadPosition, string CharacterName, int RoleID, int IsWin,int BloodNum,int MaxBloodNum) //创建位置
    {
        int j = PictureCreater.instance.CreateRole(RoleID, "Model", 18, Color.red, 0, 0, 1, 2f, false, false, 1, 1.5f, 0.2f, "Model", 0, 10, 10, 2, 2, 0, 0, 1001, 1002, 4, 1, 4, 1, 1, 0, "");
        GameObject go = PictureCreater.instance.ListRolePicture[j].RoleObject;
        PictureCreater.instance.ListRolePicture[j].RoleObject.name = CharacterName + RoleID.ToString();

        GameObject goBlood = NGUITools.AddChild(BloodPart, BloodItem);
        goBlood.transform.Find("BackBround").GetComponent<UISlider>().value = (float)BloodNum / MaxBloodNum;
        goBlood.transform.Find("Name").GetComponent<UILabel>().text = CharacterName;
        goBlood.transform.localPosition = HeroBloodPosition(IsAtack, WitchRoad, RoadPosition);
        goBlood.name = CharacterName + RoleID.ToString() + "Blood";


        string PositionStr=WitchRoad.ToString()+RoadPosition.ToString();
        Vector3 VecPosition = HeroPosition(IsAtack, WitchRoad, RoadPosition);
        Transform parent = null;

        if (WitchRoad == 1) 
        {
            parent = OneRoad.transform;
        }
        else if (WitchRoad == 2) 
        {
            parent = TwoRoad.transform;
        }
        else if (WitchRoad == 3) 
        {
            parent = ThreeRoad.transform;
        }

        PictureCreater.instance.ListRolePicture[j].RoleObject.transform.parent = parent;
        PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = VecPosition;
        PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localScale = new Vector3(400f, 400f, 400f);

        if (IsAtack == 0)
        {
            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.Rotate(new Vector3(0, 0, 0));
        }
        else 
        {
            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.Find("Role").transform.Rotate(new Vector3(0, -180, 0));
        }

        PictureCreater.instance.ListRolePicture[j].RoleObject.AddComponent<LegionFightHeroInfo>();
        PictureCreater.instance.ListRolePicture[j].RoleObject.GetComponent<LegionFightHeroInfo>().SetLegionFightHeroInfo(IsAtack, WitchRoad, RoadPosition, CharacterName, RoleID, -1, BloodNum, MaxBloodNum, goBlood);


        //PictureCreater.instance.ListRolePicture[j].RoleObject.transform.FindChild("Role").transform.LookAt(BossParent.transform.localPosition);
    }

    public void LegionWarGetWarResult(int win1,int win2,int win3) //8609
    {
        //for (int i = 0; i < PictureCreater.instance.ListRolePicture.Count; i++) //
        //{
        //    LegionFightHeroInfo LH = PictureCreater.instance.ListRolePicture[i].RoleObject.GetComponent<LegionFightHeroInfo>();
        //    PictureCreater.instance.ListRolePicture[i].RoleObject.transform.localPosition = HeroPosition(LH.IsAtack, LH.WitchRoad, LH.RoadPosition);
        //    //PictureCreater.instance.ListRolePicture[i].RolePictureObject.GetComponent<Animator>().SetFloat("ft", 2);
        //    PictureCreater.instance.ListRolePicture[i].RolePictureObject.GetComponent<Animator>().Play("attack");
        //    FightMotion fm2 = TextTranslator.instance.fightMotionDic[PictureCreater.instance.ListRolePicture[i].RoleID * 10 + 1];
        //    PictureCreater.instance.PlayEffect(PictureCreater.instance.ListRolePicture, i, fm2);
        //}

        StartCoroutine(IELegionWarGetWarResult(win1, win2, win3));
    }


    IEnumerator IELegionWarGetWarResult(int win1, int win2, int win3) 
    {
        if (win1 == 0)
        {
            for (int i = 0; i < PictureCreater.instance.ListRolePicture.Count; i++)
            {
                LegionFightHeroInfo LH = PictureCreater.instance.ListRolePicture[i].RoleObject.GetComponent<LegionFightHeroInfo>();
                if (LH != null && LH.WitchRoad == 1 && LH.IsAtack == 0)
                {
                    if (LH.BloodInfo != null)
                    {
                        DestroyImmediate(LH.BloodInfo);
                    }
                    PictureCreater.instance.DestroyComponentByName((LH.CharacterName + LH.RoleID.ToString()));
                    Debug.LogError("删除1功");
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < PictureCreater.instance.ListRolePicture.Count; i++)
            {
                LegionFightHeroInfo LH = PictureCreater.instance.ListRolePicture[i].RoleObject.GetComponent<LegionFightHeroInfo>();
                if (LH != null && LH.WitchRoad == 1 && LH.IsAtack == 1)
                {
                    if (LH.BloodInfo != null)
                    {
                        DestroyImmediate(LH.BloodInfo);
                    }
                    PictureCreater.instance.DestroyComponentByName((LH.CharacterName + LH.RoleID.ToString()));
                    Debug.LogError("删除1守");
                    break;
                }
            }
        }

        if (win2 == 0)
        {
            for (int i = 0; i < PictureCreater.instance.ListRolePicture.Count; i++)
            {
                LegionFightHeroInfo LH = PictureCreater.instance.ListRolePicture[i].RoleObject.GetComponent<LegionFightHeroInfo>();
                if (LH != null && LH.WitchRoad == 2 && LH.IsAtack == 0)
                {
                    if (LH.BloodInfo != null)
                    {
                        DestroyImmediate(LH.BloodInfo);
                    }
                    PictureCreater.instance.DestroyComponentByName((LH.CharacterName + LH.RoleID.ToString()));
                    Debug.LogError("删除2功");
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < PictureCreater.instance.ListRolePicture.Count; i++)
            {
                LegionFightHeroInfo LH = PictureCreater.instance.ListRolePicture[i].RoleObject.GetComponent<LegionFightHeroInfo>();
                if (LH != null && LH.WitchRoad == 2 && LH.IsAtack == 1)
                {
                    if (LH.BloodInfo != null)
                    {
                        DestroyImmediate(LH.BloodInfo);
                    }
                    PictureCreater.instance.DestroyComponentByName((LH.CharacterName + LH.RoleID.ToString()));
                    Debug.LogError("删除2守");
                    break;
                }
            }
        }

        if (win3 == 0)
        {
            for (int i = 0; i < PictureCreater.instance.ListRolePicture.Count; i++)
            {
                LegionFightHeroInfo LH = PictureCreater.instance.ListRolePicture[i].RoleObject.GetComponent<LegionFightHeroInfo>();
                if (LH != null && LH.WitchRoad == 3 && LH.IsAtack == 0)
                {
                    if (LH.BloodInfo != null)
                    {
                        DestroyImmediate(LH.BloodInfo);
                    }
                    PictureCreater.instance.DestroyComponentByName((LH.CharacterName + LH.RoleID.ToString()));
                    Debug.LogError("删除3功");
                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < PictureCreater.instance.ListRolePicture.Count; i++)
            {
                LegionFightHeroInfo LH = PictureCreater.instance.ListRolePicture[i].RoleObject.GetComponent<LegionFightHeroInfo>();
                if (LH != null && LH.WitchRoad == 3 && LH.IsAtack == 1)
                {
                    if (LH.BloodInfo != null)
                    {
                        DestroyImmediate(LH.BloodInfo);
                    }
                    PictureCreater.instance.DestroyComponentByName((LH.CharacterName + LH.RoleID.ToString()));
                    Debug.LogError("删除3守");
                    break;
                }
            }
        }

        yield return new WaitForSeconds(0.5f);
        NetworkHandler.instance.SendProcess("8608#" + CharacterRecorder.instance.LegionHarasPoint + ";");
    }



    private Vector3 HeroPosition(int IsAtack, int WitchRoad, int RoadPosition)
    {
        string PositionStr = WitchRoad.ToString() + RoadPosition.ToString();
        Vector3 VecPosition = new Vector3();
        if (IsAtack == 0)
        {
            switch (PositionStr)
            {
                case "11":
                    VecPosition = new Vector3(-1840f, 3876f, 1232f);
                    break;
                case "12":
                    VecPosition = new Vector3(-270f, 3876f, 1232f);
                    break;
                case "13":
                    VecPosition = new Vector3(1334f, 3876f, 1232f);
                    break;
                case "21":
                    VecPosition = new Vector3(-1840f, 3876f, 1232f);
                    break;
                case "22":
                    VecPosition = new Vector3(-270f, 3876f, 1232f);
                    break;
                case "23":
                    VecPosition = new Vector3(1334f, 3876f, 1232f);
                    break;
                case "31":
                    VecPosition = new Vector3(-1840f, 3876f, 1232f);
                    break;
                case "32":
                    VecPosition = new Vector3(-270f, 3876f, 1232f);
                    break;
                case "33":
                    VecPosition = new Vector3(1334f, 3876f, 1232f);
                    break;
            }
        }
        else
        {
            switch (PositionStr)
            {
                case "11":
                    VecPosition = new Vector3(-1351f, 3876f, 1232f);
                    break;
                case "12":
                    VecPosition = new Vector3(207f, 3876f, 1232f);
                    break;
                case "13":
                    VecPosition = new Vector3(1825f, 3876f, 1232f);
                    break;
                case "21":
                    VecPosition = new Vector3(-1351f, 3876f, 1232f);
                    break;
                case "22":
                    VecPosition = new Vector3(207f, 3876f, 1232f);
                    break;
                case "23":
                    VecPosition = new Vector3(1825f, 3876f, 1232f);
                    break;
                case "31":
                    VecPosition = new Vector3(-1351f, 3876f, 1232f);
                    break;
                case "32":
                    VecPosition = new Vector3(207f, 3876f, 1232f);
                    break;
                case "33":
                    VecPosition = new Vector3(1825f, 3876f, 1232f);
                    break;
            }
        }

        return VecPosition;
    }

    private Vector3 HeroBloodPosition(int IsAtack, int WitchRoad, int RoadPosition)
    {
        string PositionStr = WitchRoad.ToString() + RoadPosition.ToString();
        Vector3 VecPosition = new Vector3();
        if (IsAtack == 0)
        {
            switch (PositionStr)
            {
                case "11":
                    VecPosition = new Vector3(-280f, 220f, 0);
                    break;
                case "12":
                    VecPosition = new Vector3(-45f, 220f, 0);
                    break;
                case "13":
                    VecPosition = new Vector3(186f, 220f, 0);
                    break;
                case "21":
                    VecPosition = new Vector3(-314f, 95f, 0);
                    break;
                case "22":
                    VecPosition = new Vector3(-45f, 95f, 0);
                    break;
                case "23":
                    VecPosition = new Vector3(220f, 95f, 0);
                    break;
                case "31":
                    VecPosition = new Vector3(-367f, -80f, 0);
                    break;
                case "32":
                    VecPosition = new Vector3(-45f, -80f, 0);
                    break;
                case "33":
                    VecPosition = new Vector3(273f, -80f, 0);
                    break;
            }
        }
        else
        {
            switch (PositionStr)
            {
                case "11":
                    VecPosition = new Vector3(-194.2f, 220f, 0);
                    break;
                case "12":
                    VecPosition = new Vector3(40f, 220f, 0);
                    break;
                case "13":
                    VecPosition = new Vector3(271f, 220f, 0);
                    break;
                case "21":
                    VecPosition = new Vector3(-229f, 95f, 0);
                    break;
                case "22":
                    VecPosition = new Vector3(40f, 95f, 0);
                    break;
                case "23":
                    VecPosition = new Vector3(305f, 95f, 0);
                    break;
                case "31":
                    VecPosition = new Vector3(-282f, -80f, 0);
                    break;
                case "32":
                    VecPosition = new Vector3(40f, -80f, 0);
                    break;
                case "33":
                    VecPosition = new Vector3(358f, -80f, 0);
                    break;
            }
        }

        return VecPosition;
    }


    private bool HeroRoleidIsExist(string characterName,int roleid) //判断英雄是否存在
    {
        string name = characterName + roleid.ToString();
        for (int i = 0; i < PictureCreater.instance.ListRolePicture.Count; i++)
        {
            if (PictureCreater.instance.ListRolePicture[i].RoleObject.name == name)
            {
                return true;
            }
        }
        return false;
    }

    private void GetWhoStartWar(int leftNum, int rightNum, string LeftName, string RightName) 
    {
        if (leftNum == 0 && CharacterRecorder.instance.myLegionData.legionName == LeftName) //me gong shu
        {
            LegionWarStartWar(false);
            GainResultPart.transform.Find("Message").GetComponent<UILabel>().text = "您的军团被[ff9900]" + RightName + "[-]击败,[00e900]未能占领该据点[-]";
        }
        else if (leftNum == 0 && CharacterRecorder.instance.myLegionData.legionName == RightName) //me shou ying
        {
            LegionWarStartWar(true);
            GainResultPart.transform.Find("Message").GetComponent<UILabel>().text = "您的军团成功抵御[ff9900]" + LeftName + "[-]的进攻,[00e900]成功防守该据点[-]";
        }
        else if (rightNum == 0 && CharacterRecorder.instance.myLegionData.legionName == LeftName) //me gong ying
        {
            LegionWarStartWar(true);
            GainResultPart.transform.Find("Message").GetComponent<UILabel>().text = "您的军团成功击败了[ff9900]" + RightName + "[-],[00e900]成功占领该据点[-]";
        }
        else if (rightNum == 0 && CharacterRecorder.instance.myLegionData.legionName == RightName) //me shou shu
        {
            LegionWarStartWar(false);
            GainResultPart.transform.Find("Message").GetComponent<UILabel>().text = "您的军团被[ff9900]" + LeftName + "[-]击败,[00e900]失去该据点[-]";
        }
        else if (leftNum == 0 && CharacterRecorder.instance.myLegionData.legionName != LeftName && CharacterRecorder.instance.myLegionData.legionName != RightName) 
        {
            LegionWarStartWar(false);
            GainResultPart.transform.Find("Message").GetComponent<UILabel>().text ="[ff9900]"+LeftName+"[-]军团攻击"+"[ff9900]" + LeftName + "[-]军团失败";
        }
        else if (rightNum == 0 && CharacterRecorder.instance.myLegionData.legionName != LeftName && CharacterRecorder.instance.myLegionData.legionName != RightName)
        {
            LegionWarStartWar(true);
            GainResultPart.transform.Find("Message").GetComponent<UILabel>().text = "[ff9900]" + LeftName + "[-]军团攻击" + "[ff9900]" + LeftName + "[-]军团成功";
        }
    }

    private void LegionWarStartWar(bool IsWin) //结算界面  0攻，1守，2观战   iswin  0攻防
    {
        GainResultPart.SetActive(true);

        UIEventListener.Get(GainResultPart.transform.Find("KnowButton").gameObject).onClick = delegate(GameObject go)
        {
            GainResultPart.SetActive(false);
            NetworkHandler.instance.SendProcess("8620#" + CharacterRecorder.instance.LegionFestPosition + ";");
            PictureCreater.instance.DestroyAllComponent();
            UIManager.instance.BackUI();

        };


        if (IsWin)
        {
            GameObject effectcg = Instantiate(EffectCG) as GameObject;
            effectcg.name = "effectcg";
            effectcg.transform.parent = GainResultPart.transform;
            effectcg.transform.localPosition = new Vector3(0, 260, 15);
            effectcg.transform.localScale = new Vector3(300, 300, 300);
            effectcg.SetActive(true);

            GameObject effectguang = Instantiate(EffectGuang) as GameObject;
            effectguang.name = "effectguang";
            effectguang.transform.parent = GainResultPart.transform;
            effectguang.transform.localPosition = new Vector3(0, 370, 0);
            effectguang.transform.localScale = new Vector3(400, 400, 400);
            effectguang.SetActive(true);

            TweenScale TweenBg = GainResultPart.transform.Find("SpriteBg").GetComponent<TweenScale>();
            TweenBg.from = new Vector3(1f, 0, 1);
            TweenBg.to = new Vector3(1f, 1f, 1);
            TweenBg.ResetToBeginning();
            TweenBg.PlayForward();

        }
        else
        {
            //GainResultPart.transform.Find("WinParent").gameObject.SetActive(false);
            //GainResultPart.transform.Find("LoseParent").gameObject.SetActive(true);
            GameObject effectsb = Instantiate(EffectSB) as GameObject;
            effectsb.name = "effectsb";
            effectsb.transform.parent = GainResultPart.transform;
            effectsb.transform.localPosition = new Vector3(0, 260, 15);
            effectsb.transform.localScale = new Vector3(300, 300, 300);
            effectsb.SetActive(true);

            GameObject effectguang = Instantiate(EffectGuang) as GameObject;
            effectguang.name = "effectguang";
            effectguang.transform.parent = GainResultPart.transform;
            effectguang.transform.localPosition = new Vector3(0, 370, 0);
            effectguang.transform.localScale = new Vector3(400, 400, 400);
            effectguang.SetActive(true);

            TweenScale TweenBg = GainResultPart.transform.Find("SpriteBg").GetComponent<TweenScale>();
            TweenBg.from = new Vector3(1f, 0, 1);
            TweenBg.to = new Vector3(1f, 1f, 1);
            TweenBg.ResetToBeginning();
            TweenBg.PlayForward();
        }
    }


    public void LegionWarinfo(string Recving)
    {
        string[] dataSplit = Recving.Split(';');
        if (dataSplit[1] != "")
        {
            if (dataSplit[0] == "1") //攻方
            {
                if (LegionLeftInfo.activeSelf == false)
                {
                    LegionLeftInfo.SetActive(true);
                    for (int i = UiGridLeft.transform.childCount - 1; i >= 0; i--)
                    {
                        DestroyImmediate(UiGridLeft.transform.GetChild(i).gameObject);
                    }

                    for (int i = 1; i < dataSplit.Length - 1; i++)
                    {
                        string[] trcSplit = dataSplit[i].Split('$');
                        GameObject go = NGUITools.AddChild(UiGridLeft, LegionInfoItem);
                        go.transform.Find("Name").GetComponent<UILabel>().text = trcSplit[0];
                        go.transform.Find("Num").GetComponent<UILabel>().text = trcSplit[2] + "队";
                    }
                    UiGridLeft.GetComponent<UIGrid>().Reposition();
                    UiGridLeft.transform.parent.GetComponent<UIScrollView>().ResetPosition();
                }
            }
            else
            {
                if (LegionRightInfo.activeSelf == false)
                {
                    int num = 0;
                    LegionRightInfo.SetActive(true);
                    for (int i = UiGridRight.transform.childCount - 1; i >= 0; i--)
                    {
                        DestroyImmediate(UiGridRight.transform.GetChild(i).gameObject);
                    }

                    for (int i = 1; i < dataSplit.Length - 1; i++)
                    {
                        num++;
                        string[] trcSplit = dataSplit[i].Split('$');
                        GameObject go = NGUITools.AddChild(UiGridRight, LegionInfoItem);
                        go.transform.Find("Name").GetComponent<UILabel>().text = trcSplit[0];
                        go.transform.Find("Num").GetComponent<UILabel>().text = trcSplit[2] + "队";
                    }
                    UiGridRight.GetComponent<UIGrid>().Reposition();
                    UiGridRight.transform.parent.GetComponent<UIScrollView>().ResetPosition();

                    if (ShouNum > num)
                    {
                        LegionRightInfo.transform.Find("YuanJunLabel").gameObject.SetActive(true);
                        string str = (ShouNum - num).ToString();
                        LegionRightInfo.transform.Find("YuanJunLabel").GetComponent<UILabel>().text = "获得[f2ff24]" + str + "[-]队援助军的帮助";
                    }
                    else 
                    {
                        LegionRightInfo.transform.Find("YuanJunLabel").gameObject.SetActive(false);
                    }
                }
            }
        }
        else 
        {
            UIManager.instance.OpenPromptWindow("还没有上阵成员呦", PromptWindow.PromptType.Hint, null, null);
        }
    }

}


//public class LegionFightHeroInfo
//{
//    public GameObject HeroObj;
//    public int IsAtack;//0  攻击，1守护
//    public int WitchRoad;//1,2,3条路
//    public int RoadPosition;//每一条路对应位置
//    public string CharacterName;
//    public int RoleID;
//    public bool IsWin;//0输1赢


//    public LegionFightHeroInfo(GameObject HeroObj, int IsAtack, int WitchRoad, int RoadPosition, string CharacterName, int RoleID, bool IsWin) 
//    {
//        this.HeroObj = HeroObj;
//        this.IsAtack = IsAtack;
//        this.WitchRoad = WitchRoad;
//        this.RoadPosition = RoadPosition;
//        this.CharacterName = CharacterName;
//        this.RoleID = RoleID;
//        this.IsWin = IsWin;
//    }
//}
