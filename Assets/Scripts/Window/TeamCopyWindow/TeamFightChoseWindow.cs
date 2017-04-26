using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamFightChoseWindow : MonoBehaviour {

    //public GameObject HeroMapItem;
    //public GameObject uigrid;
    //public GameObject Box;
    //public UILabel Onelabel;
    //public UILabel Twolabel;
    //public UILabel Threelabel;
    //public UILabel Fourlabel;
    //public UILabel Fivelabel;

    public GameObject AwardItem;
    public GameObject uiGrid;
    public GameObject LastAwardItem;
    public GameObject MiddleGrid;


    public GameObject Value;

    private int TeamID;
    private int CopyNumber;
    private List<int> ChoseButtonId=new List<int>();
    private List<GameObject> HeroItemList= new List<GameObject>();

    public GameObject TeamFightCamer;//摄像机

    public GameObject GainResultPart;
    public GameObject Mission_Failed;

    private List<PictureCreater.RolePicture> HeroTeam = new List<PictureCreater.RolePicture>();
    private List<GameObject> BossTeam=new List<GameObject>();
    public GameObject Scence;
    public GameObject ScenceBeijing;
    public GameObject ScenceBeijing2;
    private GameObject zhujiemian1;
    private GameObject zhujiemian2;
    private GameObject zhujiemian3;

    private GameObject BossHero;//=new GameObject();
    private int StartTeamGateID = 0;//初始化组队teamgateid
    private int GateNum = 0;//第几关
    private bool IsWin = false;



    bool IsMoveScence = false;
    bool IsSendProcess = false;//是否发过公告
    int time = 0;//10秒自动退出


    // Beishu = (float)60 / Application.targetFrameRate;


    //表情聊天一块
    public GameObject ChatControlButton;
    public GameObject ChatPart;
    public GameObject[] ChatButtonArr;
    public GameObject[] FaceInfoList;//= new GameObject[5];
    public GameObject[] HeroNameArr;
    public UILabel Chattime;

    private int WitchScence = 0;//场景模式

	void Start () {

        if (UIEventListener.Get(ChatControlButton).onClick == null)
        {
            UIEventListener.Get(ChatControlButton).onClick += delegate(GameObject go)
            {
                if (ChatPart.activeSelf == false)
                {
                    ChatPart.SetActive(true);
                }
                else
                {
                    ChatPart.SetActive(false);
                }
            };
        }

        for (int i = 0; i < ChatButtonArr.Length; i++) 
        {
            int num=i+1;
            UIEventListener.Get(ChatButtonArr[i]).onClick = delegate(GameObject go)
            {
                if (FaceInfoList[CharacterRecorder.instance.TeamPosition - 1].activeSelf == false)
                {
                    NetworkHandler.instance.SendProcess("6111#" + num + ";" + CharacterRecorder.instance.TeamID + ";" + CharacterRecorder.instance.TeamPosition + ";");
                }
                ChatPart.SetActive(false);
            };
        }

        Chattime.text = "0";
        if (UIRootExtend.instance.isUiRootRatio != 1f)
        {
            TeamFightCamer.GetComponent<Camera>().fieldOfView = 29f *UIRootExtend.instance.isUiRootRatio;
        }
        else
        {
            TeamFightCamer.GetComponent<Camera>().fieldOfView = 29f;
        }
	}

    void FixedUpdate() 
    {
        if (IsMoveScence && WitchScence==1)
        {
            zhujiemian1.transform.localPosition = new Vector3(zhujiemian1.transform.localPosition.x - 60, 0, 0);
            zhujiemian2.transform.localPosition = new Vector3(zhujiemian2.transform.localPosition.x - 60, 0, 0);
            zhujiemian3.transform.localPosition = new Vector3(zhujiemian3.transform.localPosition.x - 60, 0, 0);

            if (zhujiemian1.transform.localPosition.x <= -15705)//15705
            {
                //zhujiemian1.transform.localPosition = new Vector3(15705, 0, 0);
                if (zhujiemian2.transform.localPosition.x > 0)
                {
                    zhujiemian1.transform.localPosition = new Vector3(zhujiemian2.transform.localPosition.x + 10470, 0, 0);
                }
                else if (zhujiemian3.transform.localPosition.x > 0)
                {
                    zhujiemian1.transform.localPosition = new Vector3(zhujiemian3.transform.localPosition.x + 10470, 0, 0);
                }
                else
                {
                    zhujiemian1.transform.localPosition = new Vector3(15705, 0, 0);
                }
            }
            else if (zhujiemian2.transform.localPosition.x <= -15705)
            {
                //zhujiemian2.transform.localPosition = new Vector3(15705, 0, 0);
                if (zhujiemian1.transform.localPosition.x > 0)
                {
                    zhujiemian2.transform.localPosition = new Vector3(zhujiemian1.transform.localPosition.x + 10470, 0, 0);
                }
                else if (zhujiemian3.transform.localPosition.x > 0)
                {
                    zhujiemian2.transform.localPosition = new Vector3(zhujiemian3.transform.localPosition.x + 10470, 0, 0);
                }
                else
                {
                    zhujiemian2.transform.localPosition = new Vector3(15705, 0, 0);
                }
            }
            else if (zhujiemian3.transform.localPosition.x <= -15705)
            {
                //zhujiemian3.transform.localPosition = new Vector3(15705, 0, 0);
                if (zhujiemian1.transform.localPosition.x > 0)
                {
                    zhujiemian3.transform.localPosition = new Vector3(zhujiemian1.transform.localPosition.x + 10470, 0, 0);
                }
                else if (zhujiemian2.transform.localPosition.x > 0)
                {
                    zhujiemian3.transform.localPosition = new Vector3(zhujiemian2.transform.localPosition.x + 10470, 0, 0);
                }
                else
                {
                    zhujiemian3.transform.localPosition = new Vector3(15705, 0, 0);
                }
            }
        }
        else if (IsMoveScence && WitchScence == 2) 
        {
            zhujiemian1.transform.localPosition = new Vector3(zhujiemian1.transform.localPosition.x - 60, 0, 0);
            zhujiemian2.transform.localPosition = new Vector3(zhujiemian2.transform.localPosition.x - 60, 0, 0);

            if (zhujiemian1.transform.localPosition.x <= -31410)
            {
                if (zhujiemian2.transform.localPosition.x > -31410)
                {
                    zhujiemian1.transform.localPosition = new Vector3(zhujiemian2.transform.localPosition.x + 31410, 0, 0);
                }
                else
                {
                    zhujiemian1.transform.localPosition = new Vector3(31410, 0, 0);
                }
            }
            else if (zhujiemian2.transform.localPosition.x <= -31410)
            {
                if (zhujiemian1.transform.localPosition.x > -31410)
                {
                    zhujiemian2.transform.localPosition = new Vector3(zhujiemian1.transform.localPosition.x + 31410, 0, 0);
                }
                else
                {
                    zhujiemian2.transform.localPosition = new Vector3(31410, 0, 0);
                }
            }
        }
    }




    public void SetTeamFightChoseWindow(int Num) 
    {
        StopAllCoroutines();
        AddMainWindowScence();
    }

    private void SendPopupMessage(string getname,string ItemCode) //得到金色秘宝发通告
    {
        if (CharacterRecorder.instance.TeamPosition == 1 && IsSendProcess==false) 
        {
            int itemcode = int.Parse(ItemCode);
            if (CharacterRecorder.instance.CopyNumber <= 2) 
            {
                if (itemcode == 40104) //紫色攻击秘宝
                {
                    NetworkHandler.instance.SendProcess(string.Format("7002#{0};{1};{2};{3}", 13, getname, 40104, 0));
                    IsSendProcess = true;
                }
            }
            else if (CharacterRecorder.instance.CopyNumber > 2 && CharacterRecorder.instance.CopyNumber < 6)
            {
                if (itemcode == 40105)
                {
                    NetworkHandler.instance.SendProcess(string.Format("7002#{0};{1};{2};{3}", 13, getname, 40105, 0));
                    IsSendProcess = true;
                }
            }
            else if (CharacterRecorder.instance.CopyNumber==6)
            {
                if (itemcode == 40106)
                {
                    NetworkHandler.instance.SendProcess(string.Format("7002#{0};{1};{2};{3}", 13, getname, 40106, 0));
                    IsSendProcess = true;
                }
            }
        }
    }

    public void SetTeamAward() //左边奖励窗口
    {
        string[] dataSplit = CharacterRecorder.instance.TeamAwardList.Split('!');
        int Num = 0;
        for (int i = uiGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGrid.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < dataSplit.Length - 1; i++) 
        {
            string[] trcSplit = dataSplit[i].Split('$');
            if (trcSplit[1] != "") 
            {
                Num++;
                GameObject go = NGUITools.AddChild(uiGrid, AwardItem);
                go.SetActive(true);
                TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(int.Parse(trcSplit[1]));
                go.transform.Find("SpriteGrade").GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
                //go.transform.Find("SpriteGrade").transform.Find("Icon").GetComponent<UISprite>().spriteName = trcSplit[1];
                go.transform.Find("characterName").GetComponent<UILabel>().text = trcSplit[0];
                if (trcSplit[1][0] == '2')
                {
                    go.transform.Find("SpriteGrade").transform.Find("Icon").GetComponent<UISprite>().spriteName = _ItemInfo.picID.ToString();
                }
                else
                {
                    go.transform.Find("SpriteGrade").transform.Find("Icon").GetComponent<UISprite>().spriteName = trcSplit[1];
                    SendPopupMessage(trcSplit[0], trcSplit[1]);
                }
                go.transform.Find("ItemName").GetComponent<UILabel>().text = _ItemInfo.itemName;
                if (trcSplit[0] == CharacterRecorder.instance.characterName)
                {
                    if (CharacterRecorder.instance.Vip >= 12)
                    {
                        go.transform.Find("ItemNum").GetComponent<UILabel>().text = "X2";
                        go.transform.Find("ItemNum").GetComponent<UILabel>().color = Color.red;
                    }
                    else
                    {
                        go.transform.Find("ItemNum").GetComponent<UILabel>().text = "X" + trcSplit[2];
                    }  
                }
                else 
                {
                    go.transform.Find("ItemNum").GetComponent<UILabel>().text = "X" + trcSplit[2];
                }              
                SetItemNameColor(_ItemInfo.itemGrade, go);
            }
        }
        uiGrid.GetComponent<UIGrid>().Reposition();
        if (Num >= 6) 
        {
            int num = Num - 5;
            //uiGrid.transform.parent.localPosition = new Vector3(-514f, -17 + 105f * Num, 0);
            //uiGrid.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2(0, -105f * Num);
            StartCoroutine(MoveScrollView(num));
        }
    }

    IEnumerator MoveScrollView(int num) 
    {
        float positiony = -17 + 105f * num;
        while (uiGrid.transform.parent.localPosition.y < positiony) 
        {
            uiGrid.transform.parent.localPosition = new Vector3(0f,uiGrid.transform.parent.localPosition.y + 20f, 0);
            uiGrid.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2(0, uiGrid.transform.parent.GetComponent<UIPanel>().clipOffset.y-20f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    void SetItemNameColor(int num, GameObject go)
    {
        switch (num) 
        {
            case 1:
                go.transform.Find("ItemName").GetComponent<UILabel>().color = Color.white;
                break;
            case 2:
                go.transform.Find("ItemName").GetComponent<UILabel>().color = Color.green;
                break;
            case 3:
                go.transform.Find("ItemName").GetComponent<UILabel>().color = Color.blue;
                break;
            case 4:
                go.transform.Find("ItemName").GetComponent<UILabel>().color = new Color(138 / 255f, 43 / 255f, 226 / 255f);
                break;
            case 5:
                go.transform.Find("ItemName").GetComponent<UILabel>().color = new Color(255 / 255f, 140 / 255f, 0 / 255f);
                break;
            case 6:
                go.transform.Find("ItemName").GetComponent<UILabel>().color = new Color(139 / 255f, 0 / 255f, 0 / 255f);
                break;
        }
    }
    public void SetValuePosition(int num)//设置旗子位置
    {
        Value.transform.localPosition = new Vector3(126f, -29 - 60f * num, 0);
    }


    public void SetLastAwardWindow() //最终奖励窗口
    {
        //string[] dataSplit = CharacterRecorder.instance.TeamAwardList.Split('!');
        int Num = 0;
        if (CharacterRecorder.instance.TeamAwardList != null) 
        {
            string[] dataSplit = CharacterRecorder.instance.TeamAwardList.Split('!');
            for (int i = 0; i < dataSplit.Length - 1; i++)
            {
                string[] trcSplit = dataSplit[i].Split('$');
                if (trcSplit[0] == CharacterRecorder.instance.characterName)
                {
                    if (trcSplit[1] != "") //是否有奖励
                    {
                        Num++;
                        GameObject go = NGUITools.AddChild(MiddleGrid, LastAwardItem);
                        go.SetActive(true);
                        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(int.Parse(trcSplit[1]));
                        go.GetComponent<UISprite>().spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
                        if (trcSplit[1][0] == '2')
                        {
                            go.transform.Find("Icon").GetComponent<UISprite>().spriteName = _ItemInfo.picID.ToString();
                        }
                        else
                        {
                            go.transform.Find("Icon").GetComponent<UISprite>().spriteName = trcSplit[1];
                        }
                        //go.transform.Find("Icon").GetComponent<UISprite>().spriteName = trcSplit[1];
                        go.transform.Find("Name").GetComponent<UILabel>().text = _ItemInfo.itemName;

                        if (CharacterRecorder.instance.Vip >= 12)
                        {
                            GameObject ob = NGUITools.AddChild(MiddleGrid, go);
                        }
                    }
                }
            }
            MiddleGrid.GetComponent<UIGrid>().Reposition();
        }
        if (Num != 0)
        {
            GainResultPart.SetActive(true);
            AudioEditer.instance.PlayOneShot("ui_recieve");
            UIEventListener.Get(GainResultPart.transform.Find("SureButton").gameObject).onClick = delegate(GameObject go)
            {

                PictureCreater.instance.DestroyAllComponent();
                CharacterRecorder.instance.TeamAwardList = null;
                UIManager.instance.OpenSinglePanel("TeamCopyWindow", true);
                if (CharacterRecorder.instance.TeamPosition == 1)
                {
                    NetworkHandler.instance.SendProcess("6105#" + CharacterRecorder.instance.TeamID + ";" + CharacterRecorder.instance.TeamPosition + ";");
                }
            };

            if (PlayerPrefs.GetInt("AutoEnterTeamCopy") == 1) 
            {
                Invoke("CloseAllScence", 20);
            }
        }
        else 
        {
            string resetMessage = "您本次没有获得奖励!";
            UIManager.instance.OpenPromptWindow(resetMessage, PromptWindow.PromptType.Alert, CloseAllScence, null);
            Invoke("CloseAllScence", 20);
        }
    }

    public void MissionWindow() //失败窗口
    {
        //GameObject.Find("TeamFightCamer").GetComponent<TeamFightCamer>().FightChoseWindowMission();
        Mission_Failed.SetActive(true);
        StartCoroutine(SetMission());
    }
    IEnumerator SetMission() 
    {
        yield return new WaitForSeconds(4f);
        if (CharacterRecorder.instance.TeamAwardList != null)
        {
            SetLastAwardWindow();
        }
        else 
        {
            string resetMessage = "您本次没有获得奖励!";
            UIManager.instance.OpenPromptWindow(resetMessage, PromptWindow.PromptType.Alert, CloseAllScence, null);
            Invoke("CloseAllScence", 20);
        }
    }

    void CloseAllScence() 
    {
        PictureCreater.instance.DestroyAllComponent();
        CharacterRecorder.instance.TeamAwardList = null;
        UIManager.instance.OpenSinglePanel("TeamCopyWindow", true);

        if (CharacterRecorder.instance.TeamPosition == 1)
        {
            NetworkHandler.instance.SendProcess("6105#" + CharacterRecorder.instance.TeamID + ";" + CharacterRecorder.instance.TeamPosition + ";");
        }
    }

    private void AddMainWindowScence() //加载主界面场景
    {
        if (CharacterRecorder.instance.CopyNumber==2||CharacterRecorder.instance.CopyNumber==6)//沙漠
        {
            ScenceBeijing.SetActive(false);
            zhujiemian1 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_desert")) as GameObject;
            zhujiemian1.transform.parent = Scence.transform;
            zhujiemian1.transform.localPosition = new Vector3(-10470f, 0, 0);
            zhujiemian1.transform.localScale = new Vector3(-40000f, 40000f, -40000f);

            zhujiemian2 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_desert")) as GameObject;
            zhujiemian2.transform.parent = Scence.transform;
            zhujiemian2.transform.localPosition = new Vector3(0, 0, 0);
            zhujiemian2.transform.localScale = new Vector3(-40000f, 40000f, -40000f);

            zhujiemian3 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_desert")) as GameObject;
            zhujiemian3.transform.parent = Scence.transform;
            zhujiemian3.transform.localPosition = new Vector3(10470f, 0, 0);
            zhujiemian3.transform.localScale = new Vector3(-40000f, 40000f, -40000f);

            RenderSettings.fog = false;
            LightMapAsset lightAsset = Resources.Load("LightMapAsset/lightMapAsset_127") as LightMapAsset;
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
        else if (CharacterRecorder.instance.CopyNumber == 1)//工厂
        {
            ScenceBeijing.SetActive(true);
            zhujiemian1 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian")) as GameObject;
            zhujiemian1.transform.parent = Scence.transform;
            zhujiemian1.transform.localPosition = new Vector3(-10470f, 0, 0);
            zhujiemian1.transform.localScale = new Vector3(-40000f, 40000f, -40000f);

            zhujiemian2 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian")) as GameObject;
            zhujiemian2.transform.parent = Scence.transform;
            zhujiemian2.transform.localPosition = new Vector3(0, 0, 0);
            zhujiemian2.transform.localScale = new Vector3(-40000f, 40000f, -40000f);

            zhujiemian3 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian")) as GameObject;
            zhujiemian3.transform.parent = Scence.transform;
            zhujiemian3.transform.localPosition = new Vector3(10471f, 0, 0);
            zhujiemian3.transform.localScale = new Vector3(-40000f, 40000f, -40000f);

            RenderSettings.fog = false;
            LightMapAsset lightAsset = Resources.Load("LightMapAsset/lightMapAsset_128") as LightMapAsset;
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
        else if (CharacterRecorder.instance.CopyNumber == 3)//海港
        {
            Scence.transform.localPosition = new Vector3(0, 10260, 97420);
            ScenceBeijing.SetActive(false);
            ScenceBeijing2.SetActive(false);
            zhujiemian1 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_harbor")) as GameObject;
            zhujiemian1.transform.parent = Scence.transform;
            zhujiemian1.transform.localPosition = new Vector3(0, 0, 0);
            zhujiemian1.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
            zhujiemian1.name = "Zhujiemian1";

            zhujiemian2 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_harbor")) as GameObject;
            zhujiemian2.transform.parent = Scence.transform;
            zhujiemian2.transform.localPosition = new Vector3(-31410, 0, 0);
            zhujiemian2.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
            zhujiemian2.name = "Zhujiemian2";

            RenderSettings.fog = false;
            LightMapAsset lightAsset = Resources.Load("LightMapAsset/lightMapAsset_131") as LightMapAsset;
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
        else if (CharacterRecorder.instance.CopyNumber == 4)//丛林
        {
            ScenceBeijing.SetActive(false);
            zhujiemian1 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_forest")) as GameObject;
            zhujiemian1.transform.parent = Scence.transform;
            zhujiemian1.transform.localPosition = new Vector3(-10470f, 0, 0);
            zhujiemian1.transform.localScale = new Vector3(-40000f, 40000f, -40000f);

            zhujiemian2 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_forest")) as GameObject;
            zhujiemian2.transform.parent = Scence.transform;
            zhujiemian2.transform.localPosition = new Vector3(0, 0, 0);
            zhujiemian2.transform.localScale = new Vector3(-40000f, 40000f, -40000f);

            zhujiemian3 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_forest")) as GameObject;
            zhujiemian3.transform.parent = Scence.transform;
            zhujiemian3.transform.localPosition = new Vector3(10470f, 0, 0);
            zhujiemian3.transform.localScale = new Vector3(-40000f, 40000f, -40000f);

            RenderSettings.fog = false;
            LightMapAsset lightAsset = Resources.Load("LightMapAsset/lightMapAsset_129") as LightMapAsset;
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
        else if (CharacterRecorder.instance.CopyNumber == 5)//城市
        {
            ScenceBeijing.SetActive(false);
            ScenceBeijing2.SetActive(true);
            zhujiemian1 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_city")) as GameObject;
            zhujiemian1.transform.parent = Scence.transform;
            zhujiemian1.transform.localPosition = new Vector3(-10470f, 0, 0);
            zhujiemian1.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
            zhujiemian1.name = "Zhujiemian1";

            zhujiemian2 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_city")) as GameObject;
            zhujiemian2.transform.parent = Scence.transform;
            zhujiemian2.transform.localPosition = new Vector3(0, 0, 0);
            zhujiemian2.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
            zhujiemian2.name = "Zhujiemian2";

            zhujiemian3 = Instantiate(Resources.Load("Prefab/Scene/zhujiemian_city")) as GameObject;
            zhujiemian3.transform.parent = Scence.transform;
            zhujiemian3.transform.localPosition = new Vector3(10470f, 0, 0);
            zhujiemian3.transform.localScale = new Vector3(-40000f, 40000f, -40000f);
            zhujiemian3.name = "Zhujiemian3";

            RenderSettings.fog = false;
            LightMapAsset lightAsset = Resources.Load("LightMapAsset/lightMapAsset_130") as LightMapAsset;
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

        //foreach (var TeamGate in TextTranslator.instance.TeamGateList)
        //{
        //    if (TeamGate.GroupID == CharacterRecorder.instance.CopyNumber)
        //    {
        //        StartTeamGateID = TeamGate.TeamGateID;
        //        break;
        //    }
        //}
        //if (StartTeamGateID < 1 || StartTeamGateID > 60) //防止没有读取到组队的场景编号，取不到bossid
        //{
        //    StartTeamGateID = 1;
        //}
        InstantiateHero();
    }

    /// <summary>
    /// 生成人物模型
    /// </summary>
    private void InstantiateHero()//string Id, int Number, int Num, bool isNextFloor
    {
        for (int i = 0; i < CharacterRecorder.instance.CopyHeroIconList.Count; i++)
        {
            Debug.Log("加载人物ID" + CharacterRecorder.instance.CopyHeroIconList[i]);
            int j = PictureCreater.instance.CreateRole(CharacterRecorder.instance.CopyHeroIconList[i], "Model", 18, Color.red, 0, 0, 1, 2f, false, false, 1, 1.5f, 0.2f, "Model", 0, 10, 10, 2, 2, 0, 0, 1001, 1, 1002, 1, 0, 1, 1, 0, "");
            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.parent = gameObject.transform;

            if (j == 0)
            {
                if ((Screen.width * 1f / Screen.height) > 1.7f)
                {
                    PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(1325, 10790, 94205);
                }
                else if ((Screen.width * 1f / Screen.height) < 1.4f)
                {
                    PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(1325, 10790, 94205);
                }
                else
                {
                    PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(1325, 10790, 94205);
                }
            }
            else if (j == 1)
            {
                if ((Screen.width * 1f / Screen.height) > 1.7f)
                {
                    PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(485, 10790, 94205);
                }
                else if ((Screen.width * 1f / Screen.height) < 1.4f)
                {
                    PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(485, 10790, 94205);
                }
                else
                {
                    PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(485, 10790, 94205);
                }
            }
            else if (j == 2)
            {
                if ((Screen.width * 1f / Screen.height) > 1.7f)
                {
                    PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(-425, 10790, 94205);
                }
                else if ((Screen.width * 1f / Screen.height) < 1.4f)
                {
                    PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(-425, 10790, 94205);
                }
                else
                {
                    PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(-425, 10790, 94205);
                }
            }
            else if (j == 3)
            {
                if ((Screen.width * 1f / Screen.height) > 1.7f)
                {
                    PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(-1315, 10790, 94205);
                }
                else if ((Screen.width * 1f / Screen.height) < 1.4f)
                {
                    PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(-1315, 10790, 94205);
                }
                else
                {
                    PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(-1315, 10790, 94205);
                }
            }
            else if (j == 4)
            {
                if ((Screen.width * 1f / Screen.height) > 1.7f)
                {
                    PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(-2155, 10790, 94205);//12900
                }
                else if ((Screen.width * 1f / Screen.height) < 1.4f)
                {
                    PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(-2155, 10790, 94205);
                }
                else
                {
                    PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(-2155, 10790, 94205);
                }
            }

            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localScale = new Vector3(500, 500, 500);
            PictureCreater.instance.ListRolePicture[j].RoleObject.transform.Rotate(new Vector3(0, 0, 0));/////yy
            HeroTeam.Add(PictureCreater.instance.ListRolePicture[j]);
        }

        for (int i = 0; i < CharacterRecorder.instance.CopyHeroNameList.Count; i++) 
        {
            HeroNameArr[i].SetActive(true);
            HeroNameArr[i].GetComponent<UILabel>().text = CharacterRecorder.instance.CopyHeroNameList[i];
            if (CharacterRecorder.instance.characterName == CharacterRecorder.instance.CopyHeroNameList[i])
            {
                HeroNameArr[i].GetComponent<UILabel>().color = Color.yellow;
            }
            else 
            {
                HeroNameArr[i].GetComponent<UILabel>().color = Color.green;              
            }
        }
        AddAllBossInfo();

        IsMoveScence = true;

        StartCoroutine(SetHeroTeamToMove());
        if (CharacterRecorder.instance.CopyNumber == 3)
        {
            WitchScence = 2;
            //StartCoroutine(SetHarborScenceTomove());
        }
        else 
        {
            WitchScence = 1;
            //StartCoroutine(SetScenceTomove()); 
        }      
    }


    void AddAllBossInfo() 
    {
        BossTeam.Clear();
        if (CharacterRecorder.instance.CopyNumber == 0) //防止没有读取到组队的场景编号，取不到bossid
        {
            CharacterRecorder.instance.CopyNumber = 1;
        }
        foreach (var TeamGate in TextTranslator.instance.TeamGateList)
        {
            if (TeamGate.GroupID == CharacterRecorder.instance.CopyNumber)
            {
                //StartTeamGateID = TeamGate.TeamGateID;
                int bossID = TextTranslator.instance.GetTeamGateListByID(TeamGate.TeamGateID).BossID;
                int j = PictureCreater.instance.CreateRole(bossID, "Model", 18, Color.red, 0, 0, 1, 2f, false, false, 1, 1.5f, 0.2f, "Model", 0, 10, 10, 2, 2, 0, 0, 1001, 1, 1002, 1, 0, 1, 1, 0, "");
                PictureCreater.instance.ListRolePicture[j].RoleObject.transform.parent = gameObject.transform;
                PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localPosition = new Vector3(9780, 10790, 94205);
                PictureCreater.instance.ListRolePicture[j].RoleObject.transform.localScale = new Vector3(500, 500, 500);
                PictureCreater.instance.ListRolePicture[j].RoleObject.transform.Rotate(new Vector3(0, 0, 0));
                PictureCreater.instance.ListRolePicture[j].RoleObject.transform.Find("Role").transform.localRotation = new Quaternion(0, 0, 0, 0);
                PictureCreater.instance.ListRolePicture[j].RoleObject.transform.Find("Role").transform.Rotate(new Vector3(0, -90f, 0));
                PictureCreater.instance.ListRolePicture[j].RoleObject.name = "Boss" + TeamGate.TeamGateID;
                PictureCreater.instance.ListRolePicture[j].RoleObject.SetActive(false);
                BossTeam.Add(PictureCreater.instance.ListRolePicture[j].RoleObject);
                //PictureCreater.instance.ListRolePicture[j].RoleObject.AddComponent<TeamBossMoveTime>();
                //PictureCreater.instance.ListRolePicture[j].RoleObject.GetComponent<TeamBossMoveTime>().SetBossMoveTime(PictureCreater.instance.ListRolePicture[j].RoleObject, new Vector3(2280, 10790, 94205));
            }
        }
    }



    /// <summary>
    /// 组队移动
    /// </summary>
    IEnumerator SetHeroTeamToMove() //控制英雄移动,第一次攻击
    {
        //int bossID = TextTranslator.instance.GetTeamGateListByID(StartTeamGateID).BossID;
        for (int i = 0; i < HeroTeam.Count; i++)
        {
            HeroTeam[i].RolePictureObject.GetComponent<Animator>().SetFloat("ft", 0);
        }
        IsMoveScence = true;


        BossTeam[0].SetActive(true);
        BossTeam[0].AddComponent<TeamBossMoveTime>();
        BossTeam[0].GetComponent<TeamBossMoveTime>().SetBossMoveTime(BossTeam[0], new Vector3(2280, 10790, 94205));


        while (BossTeam[0].transform.localPosition.x > 2280)//4000
        {
            //BossClone.transform.localPosition = new Vector3(BossClone.transform.localPosition.x - 37.5f, 10790, 94205);
            yield return new WaitForSeconds(0.01f);
        }

        IsMoveScence = false;
        for (int i = 0; i < PictureCreater.instance.ListRolePicture.Count; i++) 
        {
            if (PictureCreater.instance.ListRolePicture[i].RoleObject.name == BossTeam[0].name) 
            {
                PictureCreater.instance.ListRolePicture[i].RoleObject.transform.Find("Role").GetComponent<Animator>().Play("attack");
                FightMotion fm2 = TextTranslator.instance.fightMotionDic[PictureCreater.instance.ListRolePicture[i].RoleID * 10 + 1];
                PictureCreater.instance.PlayEffect(PictureCreater.instance.ListRolePicture, i, fm2);
                break;
            }
        }
        for (int i = 0; i < HeroTeam.Count; i++)
        {
            HeroTeam[i].RolePictureObject.GetComponent<Animator>().SetFloat("ft", 2);
            HeroTeam[i].RolePictureObject.GetComponent<Animator>().Play("attack");
            FightMotion fm2 = TextTranslator.instance.fightMotionDic[HeroTeam[i].RoleID * 10 + 1];
            PictureCreater.instance.PlayEffect(HeroTeam, i, fm2);
        }
        yield return new WaitForSeconds(0.5f);
        if (CharacterRecorder.instance.TeamPosition == 1)
        {
            NetworkHandler.instance.SendProcess("6107#" + CharacterRecorder.instance.TeamID + ";");
        }
        time = 0;
        CancelInvoke("StaySomeTimeTolive");
        InvokeRepeating("StaySomeTimeTolive", 0f, 1f);
    }

    private void StaySomeTimeTolive() //20秒后没有收到协议自动跳出
    {
        if (time == 20)
        {
            if (GainResultPart.activeSelf == false) //是否有结算界面
            {
                MissionWindow();
            }
            time = 30;
            CancelInvoke("StaySomeTimeTolive");
        }
        else 
        {
            time++;
        }
    }


    public void SetKillBoss(bool IsWin,int GateNum)//判断是否失败
    {
        time = 0;
        this.IsWin = IsWin; 
        this.GateNum = GateNum;
        if (IsWin)
        {
            //this.GateNum = GateNum;
        }
        else 
        {
            IsMoveScence = false;
        }
        StopCoroutine("MoveToKillBoss");

        StartCoroutine("MoveToKillBoss");
    }


    /// <summary>
    /// 生成Boss模型,并移动
    /// </summary>
    IEnumerator MoveToKillBoss()//string Id, int Number, int Num, bool isNextFloor
    {
        if (IsWin && GateNum < 10)
        {
            SetValuePosition(GateNum-1);
            yield return new WaitForSeconds(0.2f);


            BossTeam[GateNum - 1].transform.Find("Role").GetComponent<Animator>().Play("dead");
            yield return new WaitForSeconds(1.4f);
            BossTeam[GateNum - 1].SetActive(false);
            BossTeam[GateNum - 1].transform.localPosition = new Vector3(-10000, 10790, 94205);
            for (int i = 0; i < BossTeam.Count; i++) 
            {
                BossTeam[i].SetActive(false);
            }


            IsMoveScence = true;

            for (int i = 0; i < HeroTeam.Count; i++)
            {
                HeroTeam[i].RolePictureObject.GetComponent<Animator>().SetFloat("ft", 0);
            }
            BossTeam[GateNum].SetActive(true);
            BossTeam[GateNum].AddComponent<TeamBossMoveTime>();
            BossTeam[GateNum].GetComponent<TeamBossMoveTime>().SetBossMoveTime(BossTeam[GateNum], new Vector3(2280, 10790, 94205));


            while (BossTeam[GateNum].transform.localPosition.x > 2280)//4000
            {
                //BossClone.transform.localPosition = new Vector3(BossClone.transform.localPosition.x - 37.5f, 10790, 94205);
                yield return new WaitForSeconds(0.01f);
            }

            IsMoveScence = false;
            for (int i = 0; i < PictureCreater.instance.ListRolePicture.Count; i++)
            {
                if (PictureCreater.instance.ListRolePicture[i].RoleObject.name == BossTeam[GateNum].name)
                {
                    PictureCreater.instance.ListRolePicture[i].RoleObject.transform.Find("Role").GetComponent<Animator>().Play("attack");
                    FightMotion fm2 = TextTranslator.instance.fightMotionDic[PictureCreater.instance.ListRolePicture[i].RoleID * 10 + 1];
                    PictureCreater.instance.PlayEffect(PictureCreater.instance.ListRolePicture, i, fm2);
                    break;
                }
            }
            for (int i = 0; i < HeroTeam.Count; i++)
            {
                HeroTeam[i].RolePictureObject.GetComponent<Animator>().SetFloat("ft", 2);
                HeroTeam[i].RolePictureObject.GetComponent<Animator>().Play("attack");
                FightMotion fm2 = TextTranslator.instance.fightMotionDic[HeroTeam[i].RoleID * 10 + 1];
                PictureCreater.instance.PlayEffect(HeroTeam, i, fm2);
                UISlider uislider = HeroNameArr[i].transform.Find("BackSlider").GetComponent<UISlider>();
                //HeroTeam[i].RolePictureObject.GetComponent<Animator>().SetFloat("ft", 0);
                uislider.value = uislider.value - Random.Range(0.05f, 0.1f);
            }

            yield return new WaitForSeconds(0.5f);
            if (CharacterRecorder.instance.TeamPosition == 1)
            {
                NetworkHandler.instance.SendProcess("6107#" + CharacterRecorder.instance.TeamID + ";");
            }

        }
        else if (IsWin && GateNum == 10) 
        {
            SetValuePosition(GateNum - 1);
            yield return new WaitForSeconds(0.2f);
            BossTeam[GateNum - 1].transform.Find("Role").GetComponent<Animator>().Play("dead");
            yield return new WaitForSeconds(1.4f);
            BossTeam[GateNum - 1].SetActive(false);
            BossTeam[GateNum - 1].transform.localPosition = new Vector3(-10000, 10790, 94205);
            for (int i = 0; i < BossTeam.Count; i++)
            {
                BossTeam[i].SetActive(false);
            }

        }
        else
        {
            SetValuePosition(GateNum);
            yield return new WaitForSeconds(0.2f);
            BossTeam[GateNum - 1].transform.Find("Role").GetComponent<Animator>().Play("hurt");
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < HeroTeam.Count; i++)
            {
                HeroTeam[i].RolePictureObject.gameObject.SetActive(false);
                HeroNameArr[i].transform.Find("BackSlider").GetComponent<UISlider>().value = 0;
            }
        }
    }

    IEnumerator SetScenceTomove()
    {
        while (true)
        {
            while (IsMoveScence)
            {
                zhujiemian1.transform.localPosition = new Vector3(zhujiemian1.transform.localPosition.x - 60, 0, 0);
                zhujiemian2.transform.localPosition = new Vector3(zhujiemian2.transform.localPosition.x - 60, 0, 0);
                zhujiemian3.transform.localPosition = new Vector3(zhujiemian3.transform.localPosition.x - 60, 0, 0);

                if (zhujiemian1.transform.localPosition.x <= -15705)//15705
                {
                    //zhujiemian1.transform.localPosition = new Vector3(15705, 0, 0);
                    if (zhujiemian2.transform.localPosition.x > 0)
                    {
                        zhujiemian1.transform.localPosition = new Vector3(zhujiemian2.transform.localPosition.x + 10470, 0, 0);
                    }
                    else if (zhujiemian3.transform.localPosition.x > 0)
                    {
                        zhujiemian1.transform.localPosition = new Vector3(zhujiemian3.transform.localPosition.x + 10470, 0, 0);
                    }
                    else
                    {
                        zhujiemian1.transform.localPosition = new Vector3(15705, 0, 0);
                    }
                }
                else if (zhujiemian2.transform.localPosition.x <= -15705)
                {
                    //zhujiemian2.transform.localPosition = new Vector3(15705, 0, 0);
                    if (zhujiemian1.transform.localPosition.x > 0)
                    {
                        zhujiemian2.transform.localPosition = new Vector3(zhujiemian1.transform.localPosition.x + 10470, 0, 0);
                    }
                    else if (zhujiemian3.transform.localPosition.x > 0)
                    {
                        zhujiemian2.transform.localPosition = new Vector3(zhujiemian3.transform.localPosition.x + 10470, 0, 0);
                    }
                    else
                    {
                        zhujiemian2.transform.localPosition = new Vector3(15705, 0, 0);
                    }
                }
                else if (zhujiemian3.transform.localPosition.x <= -15705)
                {
                    //zhujiemian3.transform.localPosition = new Vector3(15705, 0, 0);
                    if (zhujiemian1.transform.localPosition.x > 0)
                    {
                        zhujiemian3.transform.localPosition = new Vector3(zhujiemian1.transform.localPosition.x + 10470, 0, 0);
                    }
                    else if (zhujiemian2.transform.localPosition.x > 0)
                    {
                        zhujiemian3.transform.localPosition = new Vector3(zhujiemian2.transform.localPosition.x + 10470, 0, 0);
                    }
                    else
                    {
                        zhujiemian3.transform.localPosition = new Vector3(15705, 0, 0);
                    }
                }
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.01f);
        }
    }
    IEnumerator SetHarborScenceTomove()
    {
        while (true)
        {
            while (IsMoveScence)
            {
                zhujiemian1.transform.localPosition = new Vector3(zhujiemian1.transform.localPosition.x - 60, 0, 0);
                zhujiemian2.transform.localPosition = new Vector3(zhujiemian2.transform.localPosition.x - 60, 0, 0);

                if (zhujiemian1.transform.localPosition.x <= -31410)
                {
                    if (zhujiemian2.transform.localPosition.x > -31410)
                    {
                        zhujiemian1.transform.localPosition = new Vector3(zhujiemian2.transform.localPosition.x + 31410, 0, 0);
                    }
                    else
                    {
                        zhujiemian1.transform.localPosition = new Vector3(31410, 0, 0);
                    }
                }
                else if (zhujiemian2.transform.localPosition.x <= -31410)
                {
                    if (zhujiemian1.transform.localPosition.x > -31410)
                    {
                        zhujiemian2.transform.localPosition = new Vector3(zhujiemian1.transform.localPosition.x + 31410, 0, 0);
                    }
                    else
                    {
                        zhujiemian2.transform.localPosition = new Vector3(31410, 0, 0);
                    }
                }
                yield return new WaitForSeconds(0.01f);
            }
            yield return new WaitForSeconds(0.01f);
            //yield return new WaitForSeconds((0.02f / Beishu));
        }
    }


    public void InstanceTalk(int faceNum, int teamid, int mylocation) //6111 表情
    {
        StartCoroutine(IEInstanceTalk(faceNum, teamid, mylocation));
    }

    IEnumerator IEInstanceTalk(int faceNum, int teamid, int mylocation)
    {
        FaceInfoList[mylocation - 1].SetActive(true);
        //ChatButtonColor();
        //Chattime.text = "3";
        for (int i = 0; i < FaceInfoList[mylocation - 1].transform.childCount; i++)
        {
            FaceInfoList[mylocation - 1].transform.GetChild(i).gameObject.SetActive(false);
        }
        FaceInfoList[mylocation - 1].transform.GetChild(faceNum-1).gameObject.SetActive(true);
        SetChatButtonHuiIcon(CharacterRecorder.instance.TeamPosition);
        yield return new WaitForSeconds(3f);
        //Chattime.text = "2";
        //yield return new WaitForSeconds(1f);
        //Chattime.text = "1";
        //yield return new WaitForSeconds(1f);
        //Chattime.text = "0";
        FaceInfoList[mylocation - 1].SetActive(false);
        SetChatButtonHuiIcon(CharacterRecorder.instance.TeamPosition);
        //ChatButtonColor();
    }

    private void SetChatButtonHuiIcon(int mylocation)
    {
        if (FaceInfoList[mylocation - 1].activeSelf == false)
        {
            ChatButtonArr[0].GetComponent<UISprite>().spriteName = "biaoqing1_00";
            ChatButtonArr[1].GetComponent<UISprite>().spriteName = "biaoqing2_00";
            ChatButtonArr[2].GetComponent<UISprite>().spriteName = "biaoqing3_00";
            ChatButtonArr[3].GetComponent<UISprite>().spriteName = "biaoqing4_00";
            ChatButtonArr[4].GetComponent<UISprite>().spriteName = "biaoqing5_00";
            ChatButtonArr[5].GetComponent<UISprite>().spriteName = "biaoqing6_00";
            ChatButtonArr[6].GetComponent<UISprite>().spriteName = "biaoqing7_00";
            ChatButtonArr[7].GetComponent<UISprite>().spriteName = "chuhuole";
            ChatButtonArr[8].GetComponent<UISprite>().spriteName = "zailaiyiju";
            ChatButtonArr[9].GetComponent<UISprite>().spriteName = "xiexiedajia";
        }
        else
        {
            ChatButtonArr[0].GetComponent<UISprite>().spriteName = "bbiaoqing1_07";
            ChatButtonArr[1].GetComponent<UISprite>().spriteName = "bbiaoqing2_05";
            ChatButtonArr[2].GetComponent<UISprite>().spriteName = "bbiaoqing3_04";
            ChatButtonArr[3].GetComponent<UISprite>().spriteName = "bbiaoqing4_09";
            ChatButtonArr[4].GetComponent<UISprite>().spriteName = "bbiaoqing5_08";
            ChatButtonArr[5].GetComponent<UISprite>().spriteName = "bbiaoqing6_06";
            ChatButtonArr[6].GetComponent<UISprite>().spriteName = "bbiaoqing7_14";
            ChatButtonArr[7].GetComponent<UISprite>().spriteName = "chuhuole_2";
            ChatButtonArr[8].GetComponent<UISprite>().spriteName = "zailaiyiju_2";
            ChatButtonArr[9].GetComponent<UISprite>().spriteName = "xiexiedajia_2";
        }
    }


    void ChatButtonColor() //聊天按钮颜色
    {
        if (FaceInfoList[CharacterRecorder.instance.TeamPosition - 1].activeSelf == false)
        {
            ChatControlButton.GetComponent<UISprite>().spriteName = "button1";
            ChatControlButton.GetComponent<UIButton>().normalSprite = "button1";
            ChatControlButton.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(0 / 255f, 122 / 255f, 245 / 255f, 255 / 255f);
        }
        else
        {
            ChatControlButton.GetComponent<UISprite>().spriteName = "buttonHui";
            ChatControlButton.GetComponent<UIButton>().normalSprite = "buttonHui";
            ChatControlButton.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(136 / 255f, 140 / 255f, 144 / 255f, 255 / 255f);
        }
    }
}
