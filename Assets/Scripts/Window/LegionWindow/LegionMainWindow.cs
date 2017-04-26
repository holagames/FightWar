using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamMumberPositionTwo
{
    internal GameObject _Character;
    internal int _CharacterPosition = 0;
    internal int _CharacterRoleID = 0;
}

public class LegionMainWindow: MonoBehaviour
{
    public GameObject MovePart;
    public Camera RoleCamera;
    public GameObject CloudWindow;
    public GameObject MapWindowObj;

    public GameObject Hero1;
    public GameObject Hero2;
    public GameObject Hero3;
    public GameObject Hero4;
    public GameObject Hero5;
    private Dictionary<int, Hero> ReccordHeroCardIdDic = new Dictionary<int, Hero>();

    public UILabel LabelName1;
    public UILabel LabelName2;
    public UILabel LabelName3;
    public UILabel LabelName4;
    public UILabel LabelName5;

    public UILabel LabelLevel1;
    public UILabel LabelLevel2;
    public UILabel LabelLevel3;
    public UILabel LabelLevel4;
    public UILabel LabelLevel5;

    public UISprite SpriteJX1;
    public UISprite SpriteJX2;
    public UISprite SpriteJX3;
    public UISprite SpriteJX4;
    public UISprite SpriteJX5;
    public GameObject SpriteJX1Obj;
    public GameObject SpriteJX2Obj;
    public GameObject SpriteJX3Obj;
    public GameObject SpriteJX4Obj;
    public GameObject SpriteJX5Obj;

    [HideInInspector]
    public List<TeamMumberPositionTwo> mTeamPosition = new List<TeamMumberPositionTwo>();


    bool isOnPress = false;
    float oldPosition = 0;
    float ClickPosition = 0;
    float newPosition = 0;
    float cutPosition = 0;
    //int ShiftPosition = 0;
    public int PositionCount = 0;
    List<Vector3> mVector3List = new List<Vector3>();
    int staminTime = 0;
    int spriteTime = 0;
    void Start()
    {
        AudioEditer.instance.PlayLoop("bgm_battlepreparing");

        RenderSettings.fog = true;
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = 10;
        RenderSettings.fogEndDistance = 20;
        RenderSettings.fogDensity = 0.1f;
        RenderSettings.fogColor = new Color32((byte)11, (byte)42, (byte)51, 255);
        RenderSettings.ambientLight = new Color32((byte)192, (byte)163, (byte)121, 255);

        PositionCount = 0;
        SetTeamInfo();
        if (UIRootExtend.instance.isUiRootRatio != 1f)
        {
            RoleCamera.fieldOfView = 42.4f * 1.29f;
        }
        else
        {
            RoleCamera.fieldOfView = 42.4f;
        }

    }

    void OnDestroy()
    {
        PictureCreater.instance.DestroyAllComponent();
    }
    void ClickHeroToEnterRolewindow()
    {
        UIEventListener.Get(Hero1).onClick += delegate(GameObject go)
        {
            RoleWindow.targetRoleCardId = ReccordHeroCardIdDic[1].cardID;
            CharacterRecorder.instance.enterRoleFromMain = true;
            UIManager.instance.OpenPanel("RoleWindow", true);
        };
        UIEventListener.Get(Hero2).onClick += delegate(GameObject go)
        {
            RoleWindow.targetRoleCardId = ReccordHeroCardIdDic[2].cardID;
            CharacterRecorder.instance.enterRoleFromMain = true;
            UIManager.instance.OpenPanel("RoleWindow", true);
        };
        UIEventListener.Get(Hero3).onClick += delegate(GameObject go)
        {
            RoleWindow.targetRoleCardId = ReccordHeroCardIdDic[3].cardID;
            CharacterRecorder.instance.enterRoleFromMain = true;
            UIManager.instance.OpenPanel("RoleWindow", true);
        };
        UIEventListener.Get(Hero4).onClick += delegate(GameObject go)
        {
            RoleWindow.targetRoleCardId = ReccordHeroCardIdDic[4].cardID;
            CharacterRecorder.instance.enterRoleFromMain = true;
            UIManager.instance.OpenPanel("RoleWindow", true);
        };
        UIEventListener.Get(Hero5).onClick += delegate(GameObject go)
        {
            RoleWindow.targetRoleCardId = ReccordHeroCardIdDic[5].cardID;
            CharacterRecorder.instance.enterRoleFromMain = true;
            UIManager.instance.OpenPanel("RoleWindow", true);  
        };
    }
    void MoveBG(GameObject go, bool isPress)
    {
        isOnPress = isPress;
        if (isPress)
        {
            oldPosition = Input.mousePosition.x;
            newPosition = MovePart.transform.localPosition.x;
        }
        else
        {
            StartCoroutine(moveTeamMumber());
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (UICamera.hoveredObject != null)
            {
                if (UICamera.hoveredObject.name == "UIRoot")
                {
                    oldPosition = Input.mousePosition.x;
                    ClickPosition = Input.mousePosition.x;
                }
            }
        }
        if (Input.GetMouseButton(0))
        {
            if (UICamera.hoveredObject != null)
            {
                if (UICamera.hoveredObject.name == "UIRoot")
                {
                    //Ray ray = RoleCamera.ScreenPointToRay(Input.mousePosition);
                    //RaycastHit hit;

                    float MainPosition = (Input.mousePosition.x - oldPosition);
                    float clickPosition = (Input.mousePosition.x - ClickPosition);
                    //Debug.Log(Input.mousePosition.x + "    -------   " + oldPosition);
                    if (clickPosition < 0)
                    {
                        if (Input.mousePosition.x < oldPosition)
                        {
                            //Debug.Log(MainPosition + "    ---------");
                            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[0]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[0]._CharacterPosition + 1], (Mathf.Abs(MainPosition) / 200));
                            if (PictureCreater.instance.ListRolePicture.Count > 1)
                            {
                                PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[1]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[1]._CharacterPosition + 1], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 2)
                            {
                                PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[2]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[2]._CharacterPosition + 1], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 3)
                            {
                                PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[3]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[3]._CharacterPosition + 1], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 4)
                            {
                                PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[4]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[4]._CharacterPosition + 1], (Mathf.Abs(MainPosition) / 200));
                            }
                            oldPosition = Input.mousePosition.x;
                            cutPosition = MainPosition;
                            ResetHero(false);
                        }
                        else if (Input.mousePosition.x > oldPosition)
                        {
                            //Debug.Log(MainPosition + "      +++++++");
                            //Debug.Log((mTeamPosition[0]._CharacterPosition - 1));
                            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition, mVector3List[mTeamPosition[0]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            if (PictureCreater.instance.ListRolePicture.Count > 1)
                            {
                                PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition, mVector3List[mTeamPosition[1]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 2)
                            {
                                PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition, mVector3List[mTeamPosition[2]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 3)
                            {
                                PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition, mVector3List[mTeamPosition[3]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 4)
                            {
                                PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition, mVector3List[mTeamPosition[4]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            }
                            oldPosition = Input.mousePosition.x;
                            cutPosition = MainPosition;
                            ResetHero(false);
                        }
                    }
                    if (clickPosition > 0)
                    {
                        if (Input.mousePosition.x < oldPosition)
                        {
                            //Debug.Log(MainPosition + "    ---------");
                            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition, mVector3List[mTeamPosition[0]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            if (PictureCreater.instance.ListRolePicture.Count > 1)
                            {
                                PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition, mVector3List[mTeamPosition[1]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 2)
                            {
                                PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition, mVector3List[mTeamPosition[2]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 3)
                            {
                                PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition, mVector3List[mTeamPosition[3]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 4)
                            {
                                PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition, mVector3List[mTeamPosition[4]._CharacterPosition], (Mathf.Abs(MainPosition) / 200));
                            }
                            oldPosition = Input.mousePosition.x;
                            cutPosition = MainPosition;
                            ResetHero(false);
                        }
                        else if (Input.mousePosition.x > oldPosition)
                        {
                            //Debug.Log(MainPosition + "      +++++++");
                            //Debug.Log((mTeamPosition[0]._CharacterPosition - 1));
                            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[0]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[0]._CharacterPosition - 1], (Mathf.Abs(MainPosition) / 200));
                            if (PictureCreater.instance.ListRolePicture.Count > 1)
                            {
                                PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[1]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[1]._CharacterPosition - 1], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 2)
                            {
                                PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[2]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[2]._CharacterPosition - 1], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 3)
                            {
                                PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[3]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[3]._CharacterPosition - 1], (Mathf.Abs(MainPosition) / 200));
                            }
                            if (PictureCreater.instance.ListRolePicture.Count > 4)
                            {
                                PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[4]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[4]._CharacterPosition - 1], (Mathf.Abs(MainPosition) / 200));
                            }
                            oldPosition = Input.mousePosition.x;
                            cutPosition = MainPosition;
                            ResetHero(false);
                        }
                    }
                }
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            //Debug.LogError(UICamera.hoveredObject);
            if (UICamera.hoveredObject != null)
            {
                if (UICamera.hoveredObject.name == "UIRoot")
                {
                    StartCoroutine(moveTeamMumber());
                }
            }
        }

    }

    void ResetHero(bool IsVisible)
    {
        if (!IsVisible)
        {
            Hero1.SetActive(IsVisible);
            Hero2.SetActive(IsVisible);
            Hero3.SetActive(IsVisible);
            Hero4.SetActive(IsVisible);
            Hero5.SetActive(IsVisible);
        }
        else
        {
            foreach (var m in mTeamPosition)
            {
                for (int i = 0; i < CharacterRecorder.instance.ownedHeroList.size; i++)
                {
                    //Debug.LogError(CharacterRecorder.instance.ownedHeroList[i].characterRoleID + "        " + mCharacterID);
                    if (CharacterRecorder.instance.ownedHeroList[i].characterRoleID == m._CharacterRoleID)
                    {
                        PositionCount++;

                        int ShowNumber = 1;
                        if (PositionCount % PictureCreater.instance.ListRolePicture.Count == 0)
                        {
                            ShowNumber = PictureCreater.instance.ListRolePicture.Count;
                        }
                        else
                        {
                            ShowNumber = PositionCount % PictureCreater.instance.ListRolePicture.Count;
                        }
                        switch (ShowNumber)
                        {
                            case 1:
                                if (ReccordHeroCardIdDic.ContainsKey(1))
                                {
                                    ReccordHeroCardIdDic[1] = CharacterRecorder.instance.ownedHeroList[i];
                                }
                                else
                                {
                                    ReccordHeroCardIdDic.Add(1, CharacterRecorder.instance.ownedHeroList[i]);
                                }
                                SetNameColor(false, Hero1, LabelName1, LabelLevel1, SpriteJX1, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                                SpriteJX1Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                                break;
                            case 2:
                                if (ReccordHeroCardIdDic.ContainsKey(2))
                                {
                                    ReccordHeroCardIdDic[2] = CharacterRecorder.instance.ownedHeroList[i];
                                }
                                else
                                {
                                    ReccordHeroCardIdDic.Add(2, CharacterRecorder.instance.ownedHeroList[i]);
                                }
                                SetNameColor(false, Hero2, LabelName2, LabelLevel2, SpriteJX2, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                                SpriteJX2Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                                break;
                            case 3:
                                if (ReccordHeroCardIdDic.ContainsKey(4))
                                {
                                    ReccordHeroCardIdDic[4] = CharacterRecorder.instance.ownedHeroList[i];
                                }
                                else
                                {
                                    ReccordHeroCardIdDic.Add(4, CharacterRecorder.instance.ownedHeroList[i]);
                                }
                                SetNameColor(false, Hero4, LabelName4, LabelLevel4, SpriteJX4, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                                SpriteJX4Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                                break;
                            case 4:
                                if (ReccordHeroCardIdDic.ContainsKey(5))
                                {
                                    ReccordHeroCardIdDic[5] = CharacterRecorder.instance.ownedHeroList[i];
                                }
                                else
                                {
                                    ReccordHeroCardIdDic.Add(5, CharacterRecorder.instance.ownedHeroList[i]);
                                }
                                SetNameColor(false, Hero5, LabelName5, LabelLevel5, SpriteJX5, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                                SpriteJX5Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                                break;
                            case 5:
                                if (ReccordHeroCardIdDic.ContainsKey(3))
                                {
                                    ReccordHeroCardIdDic[3] = CharacterRecorder.instance.ownedHeroList[i];
                                }
                                else
                                {
                                    ReccordHeroCardIdDic.Add(3, CharacterRecorder.instance.ownedHeroList[i]);
                                }
                                SetNameColor(false, Hero3, LabelName3, LabelLevel3, SpriteJX3, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                                SpriteJX3Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                                break;
                        }
                    }
                }
            }
        }
    }

    IEnumerator moveTeamMumber()
    {
        if (cutPosition > 0)
        {
            for (int i = 1; i < 11; i++)
            {
                PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[0]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[0]._CharacterPosition - 1], i / 10f);
                if (PictureCreater.instance.ListRolePicture.Count > 1)
                {
                    PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[1]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[1]._CharacterPosition - 1], i / 10f);
                }
                if (PictureCreater.instance.ListRolePicture.Count > 2)
                {
                    PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[2]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[2]._CharacterPosition - 1], i / 10f);
                }
                if (PictureCreater.instance.ListRolePicture.Count > 3)
                {
                    PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[3]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[3]._CharacterPosition - 1], i / 10f);
                }
                if (PictureCreater.instance.ListRolePicture.Count > 4)
                {
                    PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[4]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[4]._CharacterPosition - 1], i / 10f);
                }
                if (i == 10)
                {
                    cutPosition = 0;
                    mTeamPosition[0]._CharacterPosition = (mTeamPosition[0]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[0]._CharacterPosition - 1;
                    if (PictureCreater.instance.ListRolePicture.Count > 1)
                    {
                        mTeamPosition[1]._CharacterPosition = (mTeamPosition[1]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[1]._CharacterPosition - 1;
                    }
                    if (PictureCreater.instance.ListRolePicture.Count > 2)
                    {
                        mTeamPosition[2]._CharacterPosition = (mTeamPosition[2]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[2]._CharacterPosition - 1;
                    }
                    if (PictureCreater.instance.ListRolePicture.Count > 3)
                    {
                        mTeamPosition[3]._CharacterPosition = (mTeamPosition[3]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[3]._CharacterPosition - 1;
                    }
                    if (PictureCreater.instance.ListRolePicture.Count > 4)
                    {
                        mTeamPosition[4]._CharacterPosition = (mTeamPosition[4]._CharacterPosition - 1) == -1 ? PictureCreater.instance.ListRolePicture.Count - 1 : mTeamPosition[4]._CharacterPosition - 1;
                    }
                }
                yield return new WaitForSeconds(0.01f);
            }
            PositionCount--;
        }
        else if (cutPosition < 0)
        {
            for (int i = 1; i < 11; i++)
            {
                PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[0]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[0]._CharacterPosition + 1], i / 10f);
                if (PictureCreater.instance.ListRolePicture.Count > 1)
                {
                    PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[1].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[1]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[1]._CharacterPosition + 1], i / 10f);
                }
                if (PictureCreater.instance.ListRolePicture.Count > 2)
                {
                    PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[2].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[2]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[2]._CharacterPosition + 1], i / 10f);
                }
                if (PictureCreater.instance.ListRolePicture.Count > 3)
                {
                    PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[3].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[3]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[3]._CharacterPosition + 1], i / 10f);
                }
                if (PictureCreater.instance.ListRolePicture.Count > 4)
                {
                    PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition = Vector3.Lerp(PictureCreater.instance.ListRolePicture[4].RoleObject.transform.localPosition, mVector3List[(mTeamPosition[4]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[4]._CharacterPosition + 1], i / 10f);
                }
                if (i == 10)
                {
                    cutPosition = 0;
                    mTeamPosition[0]._CharacterPosition = (mTeamPosition[0]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[0]._CharacterPosition + 1;
                    if (PictureCreater.instance.ListRolePicture.Count > 1)
                    {
                        mTeamPosition[1]._CharacterPosition = (mTeamPosition[1]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[1]._CharacterPosition + 1;
                    }
                    if (PictureCreater.instance.ListRolePicture.Count > 2)
                    {
                        mTeamPosition[2]._CharacterPosition = (mTeamPosition[2]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[2]._CharacterPosition + 1;
                    }
                    if (PictureCreater.instance.ListRolePicture.Count > 3)
                    {
                        mTeamPosition[3]._CharacterPosition = (mTeamPosition[3]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[3]._CharacterPosition + 1;
                    }
                    if (PictureCreater.instance.ListRolePicture.Count > 4)
                    {
                        mTeamPosition[4]._CharacterPosition = (mTeamPosition[4]._CharacterPosition + 1) == PictureCreater.instance.ListRolePicture.Count ? 0 : mTeamPosition[4]._CharacterPosition + 1;
                    }
                }
                yield return new WaitForSeconds(0.01f);
            }
            PositionCount++;
        }
        ResetHero(true);
    }

    public void SetTeamInfo()
    {
        List<int> ListForce = new List<int>();
        for (int i = 0; i < CharacterRecorder.instance.ownedHeroList.size; i++)
        {
            ListForce.Add(CharacterRecorder.instance.ownedHeroList[i].force);
        }

        ListForce.Sort();
        int MinForce = 0;
        if (ListForce.Count > 5)
        {
            MinForce = ListForce[ListForce.Count - 6];
        }
        int ShowCount = 5;
        int TeamNumber = 5;

        for (int i = 0; i < CharacterRecorder.instance.ownedHeroList.size; i++)
        {
            //Debug.LogError(CharacterRecorder.instance.ownedHeroList[i].characterRoleID + "        " + mCharacterID);
            if (CharacterRecorder.instance.ownedHeroList[i].force >= MinForce && ShowCount > 0)
            {
                if (CharacterRecorder.instance.ownedHeroList[i].cardID == 60016 && CharacterRecorder.instance.level < 2)
                {
                    continue;
                }
                ShowCount--;
                int j = PictureCreater.instance.CreateRole(CharacterRecorder.instance.ownedHeroList[i].cardID, "Model", 18, Color.red, 0, 0, 1, 2f, false, false, 1, 1.5f, 0.2f, "Model", 0, 10, 10, 2, 2, 0, 0, 1001, 1002, CharacterRecorder.instance.ownedHeroList[i].WeaponList[0].WeaponClass, 1, CharacterRecorder.instance.ownedHeroList[i].WeaponList[0].WeaponClass, 1, 1, 0, "");
                PictureCreater.instance.ListRolePicture[j].RoleObject.transform.parent = gameObject.transform;
                PictureCreater.instance.ListRolePicture[j].RolePictureObject.GetComponent<Animator>().Play("idle2");
                PictureCreater.instance.ListRolePicture[j].RolePictureObject.GetComponent<Animator>().SetFloat("id", 0);

                if (TeamNumber == 4)
                {
                    if (j == 0)
                    {
                        if ((Screen.width * 1f / Screen.height) > 1.7f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(9370, -400, 12077);
                        }
                        else if ((Screen.width * 1f / Screen.height) < 1.4f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(9370, -400, 12077);
                        }
                        else
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(9370, -400, 12077);
                        }
                    }
                    else if (j == 1)
                    {
                        if ((Screen.width * 1f / Screen.height) > 1.7f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(7904, -400, 13065);
                        }
                        else if ((Screen.width * 1f / Screen.height) < 1.4f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(7904, -400, 13065);
                        }
                        else
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(7904, -400, 13065);
                        }
                    }
                    else if (j == 2)
                    {
                        if ((Screen.width * 1f / Screen.height) > 1.7f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(10927, -400, 13065);
                        }
                        else if ((Screen.width * 1f / Screen.height) < 1.4f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(10927, -400, 13065);
                        }
                        else
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(10927, -400, 13065);
                        }
                    }
                    else if (j == 3)
                    {
                        if ((Screen.width * 1f / Screen.height) > 1.7f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(9381, -400, 14208);
                        }
                        else if ((Screen.width * 1f / Screen.height) < 1.4f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(9381, -400, 14208);
                        }
                        else
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(9381, -400, 14208);
                        }
                    }
                }

                if (TeamNumber == 5)
                {
                    if (j == 0)
                    {
                        if ((Screen.width * 1f / Screen.height) > 1.7f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(9370, -400, 12077);
                        }
                        else if ((Screen.width * 1f / Screen.height) < 1.4f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(9370, -400, 12077);
                        }
                        else
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(9370, -400, 12077);
                        }
                    }
                    else if (j == 1)
                    {
                        if ((Screen.width * 1f / Screen.height) > 1.7f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(7683, -400, 12900);
                        }
                        else if ((Screen.width * 1f / Screen.height) < 1.4f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(7683, -400, 12900);
                        }
                        else
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(7683, -400, 12900);
                        }
                    }
                    else if (j == 2)
                    {
                        if ((Screen.width * 1f / Screen.height) > 1.7f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(8536, -400, 14203);
                        }
                        else if ((Screen.width * 1f / Screen.height) < 1.4f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(8536, -400, 14203);
                        }
                        else
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(8536, -400, 14203);
                        }
                    }
                    else if (j == 3)
                    {
                        if ((Screen.width * 1f / Screen.height) > 1.7f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(10137, -400, 14203);
                        }
                        else if ((Screen.width * 1f / Screen.height) < 1.4f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(10137, -400, 14203);
                        }
                        else
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(10137, -400, 14203);
                        }
                    }
                    else if (j == 4)
                    {
                        if ((Screen.width * 1f / Screen.height) > 1.7f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(10824, -400, 12900);
                        }
                        else if ((Screen.width * 1f / Screen.height) < 1.4f)
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(10824, -400, 12900);
                        }
                        else
                        {
                            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(10824, -400, 12900);
                        }
                    }
                    mVector3List.Add(PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition);
                }


                TeamMumberPositionTwo mTeam = new TeamMumberPositionTwo();
                mTeam._Character = PictureCreater.instance.ListRolePicture[j].RoleObject;
                mTeam._CharacterPosition = j;
                mTeam._CharacterRoleID = CharacterRecorder.instance.ownedHeroList[i].characterRoleID;
                mTeamPosition.Add(mTeam);
                PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localScale = new Vector3(800, 800, 800);
                PictureCreater.instance.ListRolePicture[j].RoleObject.transform.Rotate(new Vector3(0, 90, 0));

                PositionCount++;

                switch (PositionCount)
                {
                    case 1:
                        ReccordHeroCardIdDic.Add(1, CharacterRecorder.instance.ownedHeroList[i]);
                        SetNameColor(true, Hero1, LabelName1, LabelLevel1, SpriteJX1, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                        SpriteJX1Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                        break;
                    case 2:
                        ReccordHeroCardIdDic.Add(2, CharacterRecorder.instance.ownedHeroList[i]);
                        SetNameColor(true, Hero2, LabelName2, LabelLevel2, SpriteJX2, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                        SpriteJX2Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                        break;
                    case 3:
                        ReccordHeroCardIdDic.Add(4, CharacterRecorder.instance.ownedHeroList[i]);
                        SetNameColor(true, Hero4, LabelName4, LabelLevel4, SpriteJX4, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                        SpriteJX4Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                        break;
                    case 4:
                        ReccordHeroCardIdDic.Add(5, CharacterRecorder.instance.ownedHeroList[i]);
                        SetNameColor(true, Hero5, LabelName5, LabelLevel5, SpriteJX5, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                        SpriteJX5Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                        break;
                    case 5:
                        ReccordHeroCardIdDic.Add(3, CharacterRecorder.instance.ownedHeroList[i]);
                        SetNameColor(true,Hero3, LabelName3, LabelLevel3, SpriteJX3, CharacterRecorder.instance.ownedHeroList[i].name, CharacterRecorder.instance.ownedHeroList[i].classNumber, CharacterRecorder.instance.ownedHeroList[i].level, CharacterRecorder.instance.ownedHeroList[i].rank);
                        SpriteJX3Obj.GetComponent<HeroRankHorizontal>().SetHeroRankHorizontal(CharacterRecorder.instance.ownedHeroList[i].rank);
                        break;
                }
            }
        }
    }


    void SetNameColor(bool isFirstEnterMain, GameObject go, UILabel NameColor, UILabel LabelLevel, UISprite SpriteJX, string Name, int ClassNumber, int Level, int Rank)
    {
        go.SetActive(true);
        LabelLevel.text = "Lv." + Level.ToString();
        SpriteJX.spriteName = "rank" + (Rank).ToString("00");
        TextTranslator.instance.SetHeroNameColor(NameColor, Name, ClassNumber);
        //StartCoroutine(SetHeroItemRedPoint(isFirstEnterMain,go, RoleRedPointType.HeroTabsRedPint));//主界面Models红点
    }

    public void SetTopContent(int _staminaOneTime, int _bloodOneTime)
    {
        TopContent tp = GameObject.Find("TopContent").GetComponent<TopContent>();
        tp.Reset();
        staminTime = _staminaOneTime;
        spriteTime = _bloodOneTime;
        CancelInvoke();
        InvokeRepeating("UpdateTime", 0, 1.0f);
    }
    void UpdateTime()
    {
        if (staminTime > 0)
        {
            staminTime -= 1;
        }
        else
        {
            staminTime = 300 - 1;
            if (CharacterRecorder.instance.staminaCap - CharacterRecorder.instance.stamina > 0)
            {
                CharacterRecorder.instance.AddStamina(1);
                if (GameObject.Find("MainWindow") != null)
                {
                    TopContent tp = GameObject.Find("TopContent").GetComponent<TopContent>();
                    tp.Reset();
                }
            }
        }
        if (spriteTime > 0)
        {
            spriteTime -= 1;
        }
        else
        {
            spriteTime = 900 - 1;
            if (CharacterRecorder.instance.spriteCap - CharacterRecorder.instance.sprite > 0)
            {
                CharacterRecorder.instance.sprite += 1;
                if (GameObject.Find("MainWindow") != null)
                {
                    TopContent tp = GameObject.Find("TopContent").GetComponent<TopContent>();
                    tp.Reset();
                }
            }
        }
    }

}
