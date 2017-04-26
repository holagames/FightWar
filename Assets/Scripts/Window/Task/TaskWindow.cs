using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TaskWindow : MonoBehaviour
{
    public GameObject uiGrid;
    public GameObject uiScrollView;
    public GameObject uiGrid1;
    public GameObject uiScrollView1;
    public GameObject HappyBoxItem;

    public GameObject Content1;
    public GameObject Content2;


    public GameObject itemPrafeb;
    public GameObject RedPoint;
    public GameObject RedPoint1;
    public List<string> FinishItemName = new List<string>();
    List<GameObject> TaskItemList = new List<GameObject>();
    [HideInInspector]
    public int curIndex = 0;
    private Vector3 scrollViewInitPos = new Vector3(0, -41.7f, 0);
    private Vector3 scrollViewInitPos1 = new Vector3(0, -122f, 0);

    void OnEnable()
    {
        curIndex = 1;
        NetworkHandler.instance.SendProcess("1201#1;");
        NetworkHandler.instance.SendProcess("1204#;");
        if (GameObject.Find("MapObject") != null) 
        {
            GameObject.Find("MapObject").SetActive(false);
        }
        //SetPart(1);
    }

    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.任务);
        UIManager.instance.UpdateSystems(UIManager.Systems.任务);

        if (UIEventListener.Get(GameObject.Find("SpriteTab1")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("SpriteTab1")).onClick += delegate(GameObject go)
            {
                curIndex = 1;
                //SetPart(1);//日常
                NetworkHandler.instance.SendProcess("1201#1");
                //SetPart(1);//日常
            };
        }

        if (UIEventListener.Get(GameObject.Find("SpriteTab2")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("SpriteTab2")).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("1201#2;");
                curIndex = 2;
                //SetPart(0);//成就
            };
        }
    }

    #region
    public void SetPart(int SetNum) //yy string[] dataSplit, int SetNum,string RecvString, 
    {
        int GateId = CharacterRecorder.instance.lastGateID - 10000;
        if (TextTranslator.instance.TaskCompleteNum() > 0)
        {
            RedPoint1.SetActive(true);
        }
        else 
        {
            RedPoint1.SetActive(false);
        }

        if (TextTranslator.instance.AchievementCompleteNum() > 0)
        {
            RedPoint.SetActive(true);
        }
        else 
        {
            RedPoint.SetActive(false);
        }

        if (SetNum == 1)
        {
            Content1.SetActive(true);
            Content2.SetActive(false);
            //HappyBoxItem.SetActive(true);
            //uiScrollView1.SetActive(true);
            //uiScrollView.SetActive(false);

            TaskItemList.Clear();
            for (int i = uiGrid1.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(uiGrid1.transform.GetChild(i).gameObject);
            }

            //uiGrid1.transform.parent.GetComponent<UIPanel>().clipOffset = Vector2.zero;
            //uiGrid1.transform.parent.localPosition = scrollViewInitPos1;

            foreach (var item in TextTranslator.instance.achievementList)
            {
                if (item.resetTime == 1)
                {
                    if (item.completeState == 1)
                    {
                        GameObject go = NGUITools.AddChild(uiGrid1, itemPrafeb);
                        go.name = item.achievementID.ToString();
                        go.GetComponent<TaskItem>().SetInfo(item, 1, 1,0);
                        TaskItemList.Add(go);
                    }
                }

            }
            foreach (var item in TextTranslator.instance.achievementList)
            {
                //if (item.completeState==0 && item.resetTime == 1 && item.achievementName != "豪华午餐" && item.achievementName != "豪华晚餐" && CharacterRecorder.instance.level >= item.OpenLevel && GateId >= item.OpenGate&&CharacterRecorder.instance.Vip>=item.OpenVip)//item.level=true;//item.completeState != 2 && item.completeState != 1 && 
                if (item.completeState == 0 && item.resetTime == 1 && item.Type != 38 && CharacterRecorder.instance.level >= item.OpenLevel && GateId > item.OpenGate && CharacterRecorder.instance.Vip >= item.OpenVip && item.bonusExp>0)
                {
                    GameObject go = NGUITools.AddChild(uiGrid1, itemPrafeb);
                    go.name = item.achievementID.ToString();
                    go.GetComponent<TaskItem>().SetInfo(item, 0, 1,0);
                    TaskItemList.Add(go);
                }
            }
            foreach (var item in TextTranslator.instance.achievementList)
            {
                //if (item.completeState==0 && item.resetTime == 1 && item.achievementName != "豪华午餐" && item.achievementName != "豪华晚餐" && CharacterRecorder.instance.level >= item.OpenLevel && GateId >= item.OpenGate&&CharacterRecorder.instance.Vip>=item.OpenVip)//item.level=true;//item.completeState != 2 && item.completeState != 1 && 
                if (item.completeState == 0 && item.resetTime == 1 && item.Type != 38 && CharacterRecorder.instance.level >= item.OpenLevel && GateId > item.OpenGate && CharacterRecorder.instance.Vip >= item.OpenVip && item.bonusExp == 0)
                {
                    GameObject go = NGUITools.AddChild(uiGrid1, itemPrafeb);
                    go.name = item.achievementID.ToString();
                    go.GetComponent<TaskItem>().SetInfo(item, 0, 1, 0);
                    TaskItemList.Add(go);
                }
            }
            uiGrid1.GetComponent<UIGrid>().Reposition();
            uiGrid1.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        }
        else
        {
            //HappyBoxItem.SetActive(false);
            //uiScrollView1.SetActive(false);
            //uiScrollView.SetActive(true);
            Content1.SetActive(false);
            Content2.SetActive(true);
            TaskItemList.Clear();
            for (int i = uiGrid.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(uiGrid.transform.GetChild(i).gameObject);
            }

            //uiGrid.transform.parent.GetComponent<UIPanel>().clipOffset = Vector2.zero;
            //uiGrid.transform.parent.localPosition = scrollViewInitPos;

            foreach (var item in TextTranslator.instance.achievementList)
            {
                if (item.resetTime == 0)// && item.achievementID < 188
                {
                    //if (item.achievementName != "精益求精")
                    if (item.achievementName != "精益求精")
                    {
                        if (item.completeState == 1 && item.preAchievement == 0)
                        {
                            GameObject go = NGUITools.AddChild(uiGrid, itemPrafeb);
                            go.name = item.achievementID.ToString();
                            go.GetComponent<TaskItem>().SetInfo(item, 1, 2,1);
                            TaskItemList.Add(go);
                        }
                        else if (item.completeState == 1 && item.preAchievement != 0 && TextTranslator.instance.GetAchievementByID(item.preAchievement).completeState != 1)
                        {
                            GameObject go = NGUITools.AddChild(uiGrid, itemPrafeb);
                            go.name = item.achievementID.ToString();
                            go.GetComponent<TaskItem>().SetInfo(item, 1, 2,1);
                            TaskItemList.Add(go);
                        }
                    }
                    else
                    {
                        if (item.preAchievement == 0 && item.completeState == 1) 
                        {
                            GameObject go = NGUITools.AddChild(uiGrid, itemPrafeb);
                            go.name = item.achievementID.ToString();
                            go.GetComponent<TaskItem>().SetInfo(item, 1, 2,1);
                            TaskItemList.Add(go);
                            break;
                        }
                        else if (item.completeState == 1 && TextTranslator.instance.GetAchievementByID(item.preAchievement).completeState == 2) 
                        {
                            GameObject go = NGUITools.AddChild(uiGrid, itemPrafeb);
                            go.name = item.achievementID.ToString();
                            go.GetComponent<TaskItem>().SetInfo(item, 1, 2,1);
                            TaskItemList.Add(go);
                            break;
                        }
                    }
                }
            }
            foreach (var item in TextTranslator.instance.achievementList)
            {
                if (item.resetTime == 0&&item.completeState==0) 
                {
                    if (item.achievementName != "精益求精")
                    {
                        if (item.preAchievement == 0)
                        {
                            GameObject go = NGUITools.AddChild(uiGrid, itemPrafeb);
                            go.name = item.achievementID.ToString();
                            go.GetComponent<TaskItem>().SetInfo(item, 0, 2,1);
                            TaskItemList.Add(go);
                        }
                        else if (item.preAchievement != 0 && TextTranslator.instance.GetAchievementByID(item.preAchievement).completeState == 2)
                        {
                            GameObject go = NGUITools.AddChild(uiGrid, itemPrafeb);
                            go.name = item.achievementID.ToString();
                            go.GetComponent<TaskItem>().SetInfo(item, 0, 2,1);
                            TaskItemList.Add(go);
                        }
                    }
                    else 
                    {
                        if (item.preAchievement == 0) 
                        {
                            GameObject go = NGUITools.AddChild(uiGrid, itemPrafeb);
                            go.name = item.achievementID.ToString();
                            go.GetComponent<TaskItem>().SetInfo(item, 0, 2,1);
                            TaskItemList.Add(go);
                            break;
                        }
                        else if (item.preAchievement != 0 && TextTranslator.instance.GetAchievementByID(item.preAchievement).completeState == 2) 
                        {
                            GameObject go = NGUITools.AddChild(uiGrid, itemPrafeb);
                            go.name = item.achievementID.ToString();
                            go.GetComponent<TaskItem>().SetInfo(item, 0, 2,1);
                            TaskItemList.Add(go);
                            break;
                        }
                    }               
                }
            }
            //uiGrid.transform.parent.GetComponent<UIPanel>().clipOffset = Vector2.zero;
            //uiGrid.transform.parent.localPosition = scrollViewInitPos;
            //uiGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
            uiGrid.GetComponent<UIGrid>().Reposition();
            uiGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        }
    }
    #endregion


    //删除领取的任务
    public void DestyItem(GameObject Finishitem)
    {
        foreach (var item in TextTranslator.instance.achievementList)
        {
            if (Finishitem.name == item.achievementID.ToString())
            {
                //Debug.LogError(item.achievementName);
                item.SetCompletState(2);
                break;
            }
        }
        Destroy(Finishitem);
        if (curIndex == 1)
        {
            NetworkHandler.instance.SendProcess("1201#1;");
            NetworkHandler.instance.SendProcess("1204#;");
        }
        else if (curIndex == 2)
        {
            NetworkHandler.instance.SendProcess("1201#2;");
        }
    }

    public void SetLevelUpRefresh() //1009
    {
        StartCoroutine(LevelUpRefresh());
    }
    IEnumerator LevelUpRefresh() 
    {
        yield return new WaitForSeconds(0.2f);
        NetworkHandler.instance.SendProcess("1201#2;");
        yield return new WaitForSeconds(0.1f);
        NetworkHandler.instance.SendProcess("1201#1;");
    }


    public void SetPartNew(int SetNum,string Recving) 
    {
        //string[] dataSplit = Recving.Split(';');

        int GateId = CharacterRecorder.instance.lastGateID - 10000;
        if (TextTranslator.instance.TaskCompleteNum() > 0)
        {
            RedPoint1.SetActive(true);
        }
        else
        {
            RedPoint1.SetActive(false);
        }

        if (TextTranslator.instance.AchievementCompleteNum() > 0)
        {
            RedPoint.SetActive(true);
        }
        else
        {
            RedPoint.SetActive(false);
        }

        if (SetNum == 1)
        {
            Content1.SetActive(true);
            Content2.SetActive(false);

            TaskItemList.Clear();
            for (int i = uiGrid1.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(uiGrid1.transform.GetChild(i).gameObject);
            }

            foreach (var item in TextTranslator.instance.achievementList)
            {
                if (item.resetTime == 1)
                {
                    if (item.completeState == 1)
                    {
                        GameObject go = NGUITools.AddChild(uiGrid1, itemPrafeb);
                        go.name = item.achievementID.ToString();
                        go.GetComponent<TaskItem>().SetInfo(item, 1, 1, 0);
                        TaskItemList.Add(go);
                    }
                }
            }
            foreach (var item in TextTranslator.instance.achievementList)
            {
                if (item.completeState == 0 && item.resetTime == 1 && item.Type != 38 && CharacterRecorder.instance.level >= item.OpenLevel && GateId > item.OpenGate && CharacterRecorder.instance.Vip >= item.OpenVip&&item.bonusExp>0)
                {
                    GameObject go = NGUITools.AddChild(uiGrid1, itemPrafeb);
                    go.name = item.achievementID.ToString();
                    go.GetComponent<TaskItem>().SetInfo(item, 0, 1, 0);
                    TaskItemList.Add(go);
                }
            }
            foreach (var item in TextTranslator.instance.achievementList)
            {
                if (item.completeState == 0 && item.resetTime == 1 && item.Type != 38 && CharacterRecorder.instance.level >= item.OpenLevel && GateId > item.OpenGate && CharacterRecorder.instance.Vip >= item.OpenVip && item.bonusExp == 0)
                {
                    GameObject go = NGUITools.AddChild(uiGrid1, itemPrafeb);
                    go.name = item.achievementID.ToString();
                    go.GetComponent<TaskItem>().SetInfo(item, 0, 1, 0);
                    TaskItemList.Add(go);
                }
            }
            uiGrid1.GetComponent<UIGrid>().Reposition();
            uiGrid1.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        }
        else
        {
            string[] dataSplit = Recving.Split(';');
            Content1.SetActive(false);
            Content2.SetActive(true);
            TaskItemList.Clear();
            for (int i = uiGrid.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(uiGrid.transform.GetChild(i).gameObject);
            }

            for (int i = 1; i < dataSplit.Length - 1; i++)  //成就按照服务端显示
            {
                string[] secSplit = dataSplit[i].Split('$');
                int _gotState = int.Parse(secSplit[1]);

                if (_gotState == 1) 
                {
                    int _id = int.Parse(secSplit[0]);
                    Achievement _CurAchievement = TextTranslator.instance.GetAchievementByID(_id);

                    GameObject go = NGUITools.AddChild(uiGrid, itemPrafeb);
                    go.name = _CurAchievement.achievementID.ToString();
                    go.GetComponent<TaskItem>().SetInfo(_CurAchievement, 1, 2, 1);
                    TaskItemList.Add(go);
                }
            }

            for (int i = 1; i < dataSplit.Length - 1; i++)  //成就按照服务端显示
            {
                string[] secSplit = dataSplit[i].Split('$');
                int _gotState = int.Parse(secSplit[1]);

                if (_gotState != 1)
                {
                    int _id = int.Parse(secSplit[0]);
                    Achievement _CurAchievement = TextTranslator.instance.GetAchievementByID(_id);

                    GameObject go = NGUITools.AddChild(uiGrid, itemPrafeb);
                    go.name = _CurAchievement.achievementID.ToString();
                    go.GetComponent<TaskItem>().SetInfo(_CurAchievement, 0, 2, 1);
                    TaskItemList.Add(go);
                }
            }

            uiGrid.GetComponent<UIGrid>().Reposition();
            uiGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        }
    }
}
