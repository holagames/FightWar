using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SweptWindow : MonoBehaviour
{
    public GameObject uiGrid;
    public GameObject ItemPrafeb;
    public GameObject CompleteSprite;
    public GameObject SweptAgain;
    public GameObject SweptComfine;
    public UIPanel UIpanel;

    public GameObject MessageWindow;
    public GameObject SweptNumLabel;


    private Vector3 PanelView;
    int MapId;
    int RushNum;
    float TimeDate;
    int Switch;
    private string[] NumChineseCharacter = new string[] { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };

    private int ResetChallageNum;//重置次数
    private int GateID;


    private bool IsCanAgainButton = false;//是否可以重新刷新再来一次


    void OnEnable() 
    {
        TimeDate=0.2f;
        Switch = 0;
    }
    void Start()
    {
        PanelView= new Vector3(0, uiGrid.transform.parent.localPosition.y,0f);
        UIEventListener.Get(GameObject.Find("SweptAgainButton")).onClick = delegate(GameObject go)
        {
            if (IsCanAgainButton) 
            {
                SweptNumLabel.SetActive(false);
                if (GateID > 20000)
                {
                    if (RushNum * 12 > CharacterRecorder.instance.stamina)
                    {
                        string resetMessage = "体力不足，是否购买体力药剂";
                        UIManager.instance.OpenPromptWindow(resetMessage, PromptWindow.PromptType.Confirm, ResetBtnClick, null);
                    }
                    else
                    {
                        //NetworkHandler.instance.SendProcess("2017#" + MapId + ";");
                        SetMessageWindow();
                    }
                }
                else
                {
                    if (RushNum * 6 > CharacterRecorder.instance.stamina)
                    {
                        string resetMessage = "体力不足，是否购买体力药剂";
                        UIManager.instance.OpenPromptWindow(resetMessage, PromptWindow.PromptType.Confirm, ResetBtnClick, null);
                    }
                    else
                    {
                        SetAgainRush();
                    }
                    //SetAgainRush();
                }
            }
        };
        UIEventListener.Get(GameObject.Find("SweptComfineButton")).onClick = delegate(GameObject go)
        {
            //Destroy(gameObject);
            if (Switch == 0)
            {
                TimeDate = 0;
                Switch = 1;
            }
            else if (Switch == 1)
            {
                Destroy(gameObject);
                CharacterRecorder.instance.SweptIconID = 0;//装备材料设置为0；
                CharacterRecorder.instance.SweptIconNum = 0;
            }
        };
        UIEventListener.Get(GameObject.Find("SweptCloseButton")).onClick = delegate(GameObject go)
        {
            if (Switch == 0) 
            {
                TimeDate = 0;
                Switch = 1;
            }
            else if (Switch == 1) 
            {
                Destroy(gameObject);
                CharacterRecorder.instance.SweptIconID = 0;//装备材料设置为0；
                CharacterRecorder.instance.SweptIconNum = 0;
            }
        };
        this.ResetChallageNum = GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().ResetChallageNum;//剩余次数
        this.GateID = GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().GateID;

    }


    void ResetBtnClick()
    {
        //UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
        NetworkHandler.instance.SendProcess("5012#10602;");
    }

    void Update()
    {
        if (uiGrid.transform.parent.GetComponent<UIPanel>().clipOffset.y != -uiGrid.transform.parent.localPosition.y+20)
        {
            uiGrid.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2(0, -uiGrid.transform.parent.localPosition.y+20); 
        }
    }
    public void GetMessage(int _Result)//返回信息
    {
        if (_Result == 1)
        {
            UIManager.instance.OpenPromptWindow("体力不足", PromptWindow.PromptType.Alert, null, null);
        }
        else if (_Result == 3) 
        {
            UIManager.instance.OpenPromptWindow("无扫荡次数", PromptWindow.PromptType.Alert, null, null);
        }
    }

    void SetAgainRush()      //再次扫荡
    {
        Switch = 0;
        CharacterRecorder.instance.SweptIconNum = 0;
        if (RushNum == 1) 
        {
            NetworkHandler.instance.SendProcess("2012#" + MapId + ";1;");
        }
        else if(RushNum>1)
        {
            NetworkHandler.instance.SendProcess("2012#" + MapId + ";" + RushNum + ";");
        }
        IsCanAgainButton = false;
    }

    void InitScrollview()
    {
        Transform _scrollViewTF = uiGrid.transform.parent;
        _scrollViewTF.localPosition = new Vector3(0, 20, 0);
        _scrollViewTF.GetComponent<UIPanel>().clipOffset = new Vector2(0, 0);
    }
    public void SetTenRush(string String,int _MapId,int _RushNum)//Recving数据，副本编号，扫荡次数
    {
        InitScrollview();
        this.MapId = _MapId;
        this.RushNum = _RushNum;
        if (_RushNum == 1) 
        {
            for (int i = uiGrid.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(uiGrid.transform.GetChild(i).gameObject);
            }
            Switch = 1;
            //SweptAgain.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
            //SweptComfine.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
            uiGrid.transform.localPosition = new Vector3(0, 60, 0);
            StartCoroutine(CreatItemOneRush(String));
        }
        else if (_RushNum > 1) 
        {
            for (int i = uiGrid.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(uiGrid.transform.GetChild(i).gameObject);
            }
            TimeDate = 0.2f;
            Switch = 0;
            //SweptAgain.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
            //SweptComfine.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 0 / 255f);
            uiGrid.transform.localPosition = new Vector3(0, 145f, 0);
            StartCoroutine(CreatItemTenRushNew(String));
        }
    }
    IEnumerator CreatItemOneRush(string String)//扫荡一次
    {
        IsCanAgainButton = false;
        yield return new WaitForSeconds(1);
        string[] dataSplit = String.Split(';');
        for (int i = 6; i < dataSplit.Length - 1; i++)
        {
            string[] secSplit = dataSplit[i].Split(':');
            GameObject go = NGUITools.AddChild(uiGrid, ItemPrafeb);
            go.GetComponent<SweptItem>().SetInfo(int.Parse(secSplit[1]), int.Parse(secSplit[0]), secSplit[2], "第一战");
            TweenPosition _TweenPosition = go.GetComponent<TweenPosition>();
            _TweenPosition.enabled = true;
            _TweenPosition.from = new Vector3(0, 60 - 100, 0);
            _TweenPosition.to = new Vector3(0, 60, 0);
            _TweenPosition.duration = 0.3f;
            _TweenPosition.PlayForward();
        }
        uiGrid.transform.localPosition = new Vector3(0, 60, 0);
        if (CharacterRecorder.instance.SweptIconID != 0)
        {
            SweptNumLabel.SetActive(true);
            SweptNumLabel.GetComponent<UILabel>().text = "扫荡1次,得到[00ff00]" + TextTranslator.instance.GetItemNameByItemCode(CharacterRecorder.instance.SweptIconID) + "[-]" + CharacterRecorder.instance.SweptIconNum + "个";
        }

        IsCanAgainButton = true;

        //SweptAgain.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        //SweptComfine.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
    }
    IEnumerator CreatItemTenRushNew(string String)//扫荡多次
    {
        IsCanAgainButton = false;
        string[] dataSplit = String.Split(';');
        for (int i = 6; i < dataSplit.Length - 1; i++)
        {
            string[] secSplit = dataSplit[i].Split(':');
            yield return new WaitForSeconds(TimeDate);
            GameObject go = NGUITools.AddChild(uiGrid, ItemPrafeb);
            int Index = i - 6;
            go.transform.localPosition = new Vector3(0, -190 * Index, 0);
            TweenPosition _TweenPosition = go.GetComponent<TweenPosition>();
            _TweenPosition.enabled = true;
            _TweenPosition.from = go.transform.localPosition + new Vector3(0, -100, 0);
            _TweenPosition.to = go.transform.localPosition;
            _TweenPosition.duration = 0.3f;
            _TweenPosition.PlayForward();
            string name = "";
            if (Index <= 9) 
            {
                name = "第" + NumChineseCharacter[Index] + "战";
            }
            else if (Index > 9 && Index <= 19) 
            {
                int b = Index % 10;
                if (b == 9)
                {
                    name = "第二十战";
                }
                else 
                {
                    name = "第十" + NumChineseCharacter[b] + "战";
                }
            }
            else if (Index >19 && Index <99) 
            {
                int a = Index / 10;
                int b = Index % 10;
                if (b == 9)
                {
                    name = "第" + NumChineseCharacter[a] + "十战";
                }
                else 
                {
                    name = "第" + NumChineseCharacter[a - 1] + "十" + NumChineseCharacter[b] + "战";
                }
            }
            else if (Index == 99) 
            {
                name = "第一百战";
            }
            else if (Index > 99) 
            {
                int a = (Index + 1) / 100;
                int b = (Index + 1) / 10 % 10;
                int c = (Index + 1) % 100%10;
                if (b == 0) 
                {
                    name = "第" + NumChineseCharacter[a - 1] + "百零" + NumChineseCharacter[c - 1] + "战";
                }
                else if (c == 0)
                {
                    name = "第" + NumChineseCharacter[a - 1] + "百" + NumChineseCharacter[b - 1] + "十战";
                }
                else 
                {
                    name = "第" + NumChineseCharacter[a - 1] + "百" + NumChineseCharacter[b - 1] + "十"+NumChineseCharacter[c - 1]+"战";
                }
            }
            go.GetComponent<SweptItem>().SetInfo(int.Parse(secSplit[1]), int.Parse(secSplit[0]), secSplit[2], name);

            if (TimeDate == 0)
            {
                SpringPanel.Begin(uiGrid.transform.parent.gameObject, new Vector3(0, PanelView.y + 190f * RushNum - 470f, 0), 5.0f);
            }           
            if (Index == 2 && TimeDate == 0.2f)
            {
                SpringPanel.Begin(uiGrid.transform.parent.gameObject, new Vector3(0, uiGrid.transform.parent.localPosition.y + 100f, 0), 5.0f);//190
            }
            else if (Index >= 3 && TimeDate == 0.2f)
            {
                SpringPanel.Begin(uiGrid.transform.parent.gameObject, new Vector3(0, (Index-2)*190f+100f, 0), 5.0f);//uiGrid.transform.parent.localPosition.y + 190f
            }
            //else if (TimeDate == 0)
            //{
            //    SpringPanel.Begin(uiGrid.transform.parent.gameObject, new Vector3(0, uiGrid.transform.parent.localPosition.y + 190f*RushNum, 0), 5.0f);             
            //}
            yield return new WaitForSeconds(TimeDate);
            if (Index >= (RushNum - 1))
            {
                TimeDate = 0.2f;
                Switch = 1;
                yield return new WaitForSeconds(0.2f);
                //if (CharacterRecorder.instance.SweptIconID != 0) 
                //{
                //    SweptNumLabel.SetActive(true);
                //    SweptNumLabel.GetComponent<UILabel>().text = "扫荡" + RushNum + "次,得到[00ff00]" + TextTranslator.instance.GetItemNameByItemCode(CharacterRecorder.instance.SweptIconID) +"[-]"+ CharacterRecorder.instance.SweptIconNum + "个";
                //}
                //SweptAgain.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
                //SweptComfine.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
            }
        }
        if (CharacterRecorder.instance.SweptIconID != 0)
        {
            int _ItemID = CharacterRecorder.instance.SweptIconID;
            TextTranslator.ItemInfo itemInfo = TextTranslator.instance.GetItemByItemCode(_ItemID);
            //if (_ItemID.ToString()[0] == '6')
            //{
            //    _ItemID += 10000;
            //}
            //else if (_ItemID.ToString()[0] == '7')
            //{
            //    _ItemID -= 10000;
            //}

            SweptNumLabel.SetActive(true);
            SweptNumLabel.GetComponent<UILabel>().text = "扫荡" + RushNum + "次,得到[00ff00]" + TextTranslator.instance.GetItemNameByItemCode(_ItemID) + "[-]" + CharacterRecorder.instance.SweptIconNum + "个";
        }

        IsCanAgainButton = true;
    }
    IEnumerator CreatItemTenRush(string String)//扫荡多次
    {
        string[] dataSplit = String.Split(';');
        for (int i = 6; i < dataSplit.Length - 1; i++)
        {
            string[] secSplit = dataSplit[i].Split(':');
            yield return new WaitForSeconds(TimeDate);
            GameObject go = NGUITools.AddChild(uiGrid, ItemPrafeb);
            //uiGrid.GetComponent<UIGrid>().Reposition();
            //go.GetComponent<UIDragObject>().target = uiGrid.transform.parent;
            //uiGrid.GetComponent<UIGrid>().repositionNow = true;
            int Index = i - 6;
            string name = "";
            switch (Index)
            {
                case 0:
                    name = "第一战";
                    break;
                case 1:
                    name = "第二战";
                    break;
                case 2:
                    name = "第三战";
                    break;
                case 3:
                    name = "第四战";
                    break;
                case 4:
                    name = "第五战";
                    break;
                case 5:
                    name = "第六战";
                    break;
                case 6:
                    name = "第七战";
                    break;
                case 7:
                    name = "第八战";
                    break;
                case 8:
                    name = "第九战";
                    break;
                case 9:
                    name = "第十战";
                    break;
                default:
                    break;
            }
            go.GetComponent<SweptItem>().SetInfo(int.Parse(secSplit[1]), int.Parse(secSplit[0]), secSplit[2],name);
/*          if (Index > 2)
            {
                GameObject.Find("All/Scroll View").GetComponent<UIScrollView>().UpdatePosition();
                GameObject.Find("All/Vertical Scroll Bar").GetComponent<UIScrollBar>().value += 1f;
            }
            if (Index >= (RushNum- 1)) 
            {
                TimeDate = 1f;
                yield return new WaitForSeconds(0.5f);
                SweptAgain.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
                SweptComfine.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
            }
            uiGrid.GetComponent<UIGrid>().repositionNow = true;*/
            if (Index == 2&&TimeDate==0.3f)
            {
                while (true)
                {
                    uiGrid.transform.localPosition = new Vector3(0, uiGrid.transform.localPosition.y + 3, 0);
                    if ((uiGrid.transform.localPosition.y + 5) >= 255)
                    {
                        uiGrid.transform.localPosition = new Vector3(0, 255, 0);
                        break;
                    }
                    yield return new WaitForSeconds(0.01f);
                }
            }
            else if (Index >= 3 && TimeDate == 0.3f)
            {
                while (true)
                {
                    uiGrid.transform.localPosition = new Vector3(0, uiGrid.transform.localPosition.y + 5, 0);
                    UIpanel.clipOffset = new Vector2(0, 6.6f);
                    if ((uiGrid.transform.localPosition.y + 5) >= (255 + (Index - 2) * uiGrid.GetComponent<UIGrid>().cellHeight))
                    {
                        uiGrid.transform.localPosition = new Vector3(0, 255 + (Index - 2) * uiGrid.GetComponent<UIGrid>().cellHeight, 0);
                        //if ((uiGrid.transform.localPosition.y + 5) >= (255 + (Index - 3) * uiGrid.GetComponent<UIGrid>().cellHeight + uiGrid.GetComponent<UIGrid>().cellHeight / 2))
                        //{
                        //    //this.gameObject.transform.Find("ScrollViewBG").GetComponent<UIPanel>().clipOffset = new Vector2(0,6.5f);
                        //    //UIpanel.clipOffset = new Vector2(0, 6.5f);
                        //}
                        UIpanel.clipOffset = new Vector2(0, 6.5f);
                        break;
                    }
                    yield return new WaitForSeconds(0.01f);
                }
            }
            else if (TimeDate == 0) 
            {
                uiGrid.transform.localPosition = new Vector3(0,255 + (Index - 2) * uiGrid.GetComponent<UIGrid>().cellHeight,0);
            }
            if (Index >= (RushNum - 1))
            {
                TimeDate = 0.3f;
                Switch = 1;
                yield return new WaitForSeconds(0.5f);
                SweptAgain.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
                SweptComfine.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
            }
            uiGrid.GetComponent<UIGrid>().repositionNow = true;
            UIpanel.clipOffset = new Vector2(0, 6.5f);
        }
    }

    public void SetMessageWindow() 
    {
        Debug.Log("进入");
        this.ResetChallageNum = GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().ResetChallageNum;
        int ChallengCount=GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().ChallengeCount;
        //int Diamond = TextTranslator.instance.GetMarketByID(ResetChallageNum + 1).ResetMstGateDiamondCost;
        Debug.Log("ChallengCount "+ChallengCount);
        Debug.Log("ResetChallageNum " + ResetChallageNum);
        int CanResetChallengeCount=TextTranslator.instance.GetVipDicByID(CharacterRecorder.instance.Vip).ResetMasterGateCount;
        Debug.Log("CanResetChallengeCount " + CanResetChallengeCount);
        int Diamond = TextTranslator.instance.GetMarketByID(CanResetChallengeCount - ResetChallageNum+1).ResetMstGateDiamondCost;

        Debug.LogError(Diamond+"  ***");
        if (ResetChallageNum>0 && ChallengCount == 0)
        {
            MessageWindow.SetActive(true);
            MessageWindow.transform.Find("BG").transform.Find("CostNumber").GetComponent<UILabel>().text = (ResetChallageNum).ToString();
            MessageWindow.transform.Find("BG").transform.Find("CostDiamond").GetComponent<UILabel>().text = Diamond.ToString();
            UIEventListener.Get(MessageWindow.transform.Find("BG").transform.Find("UseButton").gameObject).onClick = delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("2010#" + GateID + ";");
                MessageWindow.SetActive(false);
            };
            UIEventListener.Get(MessageWindow.transform.Find("BG").transform.Find("CloseButton").gameObject).onClick = delegate(GameObject go)
            {
                MessageWindow.SetActive(false);
            };
        }
        else if (ResetChallageNum == 0 && ChallengCount == 0)
        {
            //MessageWindow.transform.Find("BG").transform.Find("CostNumber").GetComponent<UILabel>().color = Color.red;
            UIManager.instance.OpenPromptWindow("今天的重置次数用完，明天再来哦", PromptWindow.PromptType.Alert, null, null);
        }
        else 
        {
            SetAgainRush();
        }
    }
}




