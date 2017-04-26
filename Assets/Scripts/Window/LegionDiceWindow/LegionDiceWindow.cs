using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionDiceWindow : MonoBehaviour {

    public GameObject[] ArrDiceItem=new GameObject[6];
    public GameObject UiGrid;

    public GameObject AwardParent;
    public GameObject ItemIcon;
    public GameObject AwardUiGrid;
    public GameObject ThrowButton;//投掷
    public GameObject CancelButton;//见好就收
    public GameObject FreeButton;//免费改投
    public GameObject DiamondButton;//钻石改投
    public GameObject BackButton;
    public GameObject RankButton;
    public GameObject HunterMessage;
    public GameObject BangdanTexture;//


    public UILabel CanThrowNumber;
    public UILabel ChangeThrowNumber;
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;
    public UITexture HeroBgTexture;
    public GameObject ShaizhongTex1;
    //public GameObject ShaizhongTex2;
    public GameObject AnimationShaizhong;
    public Camera ScenceCamera;

    public GameObject QuestionButton;

    private List<int> SortList = new List<int>();
    //private int[] ArrSort=new int[5];
    private int State;
    private int CanThrowNum;
    private int ChangeThrowNum;
    private int DiamondChangeThrowNum=0;

    bool IsStart=false;
    bool IsAfter = false;
    void OnEnable()
    {
        NetworkHandler.instance.SendProcess("8501#;");
        NetworkHandler.instance.SendProcess("8506#;");
    }
	void Start () {

        if (UIEventListener.Get(BackButton).onClick == null) 
        {
            UIEventListener.Get(BackButton).onClick = delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }

        if (UIEventListener.Get(QuestionButton).onClick == null)
        {
            UIEventListener.Get(QuestionButton).onClick = delegate(GameObject go)
            {
                HunterMessage.SetActive(true);
            };
        }
        if (UIEventListener.Get(HunterMessage.transform.Find("CloseButton").gameObject).onClick == null)
        {
            UIEventListener.Get(HunterMessage.transform.Find("CloseButton").gameObject).onClick = delegate(GameObject go)
            {
                HunterMessage.SetActive(false);
            };
        }

        if (UIEventListener.Get(RankButton).onClick == null)
        {
            UIEventListener.Get(RankButton).onClick = delegate(GameObject go)
            {
                UIManager.instance.OpenPanel("LegionDiceRankWindow", true);
            };
        }
	}

    public void LegionGetDice(int State, int _CanThrowNum, int _ChangeThrowNum, int _DiamondChangeThrowNum) //8501
    {

        this.State = State;
        this.CanThrowNum = _CanThrowNum;
        this.ChangeThrowNum = _ChangeThrowNum;
        this.DiamondChangeThrowNum = _DiamondChangeThrowNum;
       
        if (CanThrowNum == 0)
        {
            CanThrowNumber.text = "[ff0000]" + CanThrowNum.ToString() + "[-]" + "/" + TextTranslator.instance.GetLegionByID(CharacterRecorder.instance.myLegionData.legionLevel).CrapsPlayNum.ToString();
        }
        else 
        {
            CanThrowNumber.text = CanThrowNum.ToString() + "/" + TextTranslator.instance.GetLegionByID(CharacterRecorder.instance.myLegionData.legionLevel).CrapsPlayNum.ToString();
        }
        ChangeThrowNumber.text = ChangeThrowNum.ToString();

        IsStart = false;
        IsAfter = false;
        StopCoroutine(PlayAfterAnimation());
        StopCoroutine(PlayStartAnimation());

        if (State == 0)
        {
            IsStart = true;
            IsAfter = false;
            StartCoroutine(PlayStartAnimation());
            ThrowButton.SetActive(true);
            ShaizhongTex1.SetActive(true);

            CancelButton.SetActive(false);
            FreeButton.SetActive(false);
            DiamondButton.SetActive(false);
            UIEventListener.Get(ThrowButton).onClick = delegate(GameObject go)
            {
                if (CanThrowNum > 0)
                {
                    NetworkHandler.instance.SendProcess("8502#;");
                    IsStart = false;
                    IsAfter = true;
                    StopCoroutine(PlayStartAnimation());
                    StartCoroutine(PlayAfterAnimation());
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("今日次数已经用光，明天再来哦", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        else 
        {
            IsStart = false;
            IsAfter = true;
            StartCoroutine(PlayAfterAnimation());
            NetworkHandler.instance.SendProcess("8502#;");

            ThrowButton.SetActive(false);
            ShaizhongTex1.SetActive(false);
            CancelButton.SetActive(true);
            UIEventListener.Get(CancelButton).onClick = delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("8504#;");
            };

            if (ChangeThrowNum > 0)
            {
                FreeButton.SetActive(true);
                DiamondButton.SetActive(false);
                UIEventListener.Get(FreeButton).onClick = delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess("8503#;");
                };
            }
            else
            {
                FreeButton.SetActive(false);
                DiamondButton.SetActive(true);

                int diamondNum = TextTranslator.instance.GetMarketByBuyCount(DiamondChangeThrowNum+1).CrapsChangePrice;//**
                DiamondButton.transform.Find("CostLabel").GetComponent<UILabel>().text = diamondNum.ToString();
                UIEventListener.Get(DiamondButton).onClick = delegate(GameObject go)
                {
                    if (diamondNum < CharacterRecorder.instance.lunaGem)
                    {
                        NetworkHandler.instance.SendProcess("8503#;");
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("钻石不足", PromptWindow.PromptType.Hint, null, null);
                    }
                };
            }
        }

        FreeButton.GetComponent<UISprite>().spriteName = "ui2_button4";
        FreeButton.GetComponent<UIButton>().normalSprite = "ui2_button4";
        FreeButton.GetComponent<UIButton>().hoverSprite = "ui2_button4";
        FreeButton.GetComponent<UIButton>().pressedSprite = "ui2_button4_an";
        FreeButton.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(211 / 255f, 88 / 255f, 13 / 255f, 255 / 255f);
        FreeButton.GetComponent<BoxCollider>().enabled = true;
        DiamondButton.GetComponent<UISprite>().spriteName = "ui2_button4";
        DiamondButton.GetComponent<UIButton>().normalSprite = "ui2_button4";
        DiamondButton.GetComponent<UIButton>().hoverSprite = "ui2_button4";
        DiamondButton.GetComponent<UIButton>().pressedSprite = "ui2_button4_an";
        DiamondButton.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(211 / 255f, 88 / 255f, 13 / 255f, 255 / 255f);
        DiamondButton.GetComponent<BoxCollider>().enabled = true;
    }

    public void LegionSetDice(string Recving)//8502
    {
        if (State == 0)
        {
            StartCoroutine(ControlDicMove(Recving));
            CanThrowNum--;
            State = 1;
            ThrowButton.SetActive(false);
            ShaizhongTex1.SetActive(false);
            CancelButton.SetActive(true);
            UIEventListener.Get(CancelButton).onClick = delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("8504#;");
            };

            if (ChangeThrowNum > 0)
            {
                FreeButton.SetActive(true);
                DiamondButton.SetActive(false);
                UIEventListener.Get(FreeButton).onClick = delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess("8503#;");
                };
            }
            else
            {
                FreeButton.SetActive(false);
                DiamondButton.SetActive(true);

                int diamondNum = TextTranslator.instance.GetMarketByBuyCount(DiamondChangeThrowNum + 1).CrapsChangePrice;//**
                DiamondButton.transform.Find("CostLabel").GetComponent<UILabel>().text = diamondNum.ToString();
                UIEventListener.Get(DiamondButton).onClick = delegate(GameObject go)
                {
                    if (diamondNum < CharacterRecorder.instance.lunaGem)
                    {
                        NetworkHandler.instance.SendProcess("8503#;");
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("钻石不足", PromptWindow.PromptType.Hint, null, null);
                    }
                };
            }
        }
        else 
        {
            string[] dataSplit = Recving.Split('$');
            int HeroNum = 0;
            SortList.Clear();
            for (int i = 0; i < dataSplit.Length - 1; i++)
            {
                if (dataSplit[i] == "6")
                {
                    HeroNum++;
                    SortList.Add(int.Parse(dataSplit[i]));
                }
            }

            for (int i = 0; i < dataSplit.Length - 1; i++)
            {
                if (dataSplit[i] != "6")
                {
                    SortList.Add(int.Parse(dataSplit[i]));
                }
            }
            Debug.Log("SortList[0]" + SortList[0]);
            Debug.Log("SortList[1]" + SortList[1]);
            Debug.Log("SortList[2]" + SortList[2]);
            Debug.Log("SortList[3]" + SortList[3]);
            Debug.Log("SortList[4]" + SortList[4]);
            Debug.Log("SortList[5]" + SortList[5]);

            for (int i = 0; i < SortList.Count; i++) //加载筛子
            {
                ArrDiceItem[SortList.Count - 1 - i].SetActive(true);
                DiceVecPosition(ArrDiceItem[SortList.Count - 1 - i], SortList[i]);
            }

            if (HeroNum == 6)
            {
                if (FreeButton.activeSelf == true)
                {
                    FreeButton.GetComponent<UISprite>().spriteName = "buttonHui";
                    FreeButton.GetComponent<UIButton>().normalSprite = "buttonHui";
                    FreeButton.GetComponent<UIButton>().hoverSprite = "buttonHui";
                    FreeButton.GetComponent<UIButton>().pressedSprite = "buttonHui_an";
                    FreeButton.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(150 / 255f, 150 / 255f, 150 / 255f, 255 / 255f);
                    FreeButton.GetComponent<BoxCollider>().enabled = false;
                }
                else if (DiamondButton.activeSelf == true)
                {
                    DiamondButton.GetComponent<UISprite>().spriteName = "buttonHui";
                    DiamondButton.GetComponent<UIButton>().normalSprite = "buttonHui";
                    DiamondButton.GetComponent<UIButton>().hoverSprite = "buttonHui";
                    DiamondButton.GetComponent<UIButton>().pressedSprite = "buttonHui_an";
                    DiamondButton.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(150 / 255f, 150 / 255f, 150 / 255f, 255 / 255f);
                    DiamondButton.GetComponent<BoxCollider>().enabled = false;
                }
            }
            else
            {
                if (FreeButton.activeSelf == true)
                {
                    FreeButton.GetComponent<UISprite>().spriteName = "ui2_button4";
                    FreeButton.GetComponent<UIButton>().normalSprite = "ui2_button4";
                    FreeButton.GetComponent<UIButton>().hoverSprite = "ui2_button4";
                    FreeButton.GetComponent<UIButton>().pressedSprite = "ui2_button4_an";
                    FreeButton.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(211 / 255f, 88 / 255f, 13 / 255f, 255 / 255f);
                    FreeButton.GetComponent<BoxCollider>().enabled = true;
                }
                else if (DiamondButton.activeSelf == true)
                {
                    DiamondButton.GetComponent<UISprite>().spriteName = "ui2_button4";
                    DiamondButton.GetComponent<UIButton>().normalSprite = "ui2_button4";
                    DiamondButton.GetComponent<UIButton>().hoverSprite = "ui2_button4";
                    DiamondButton.GetComponent<UIButton>().pressedSprite = "ui2_button4_an";
                    DiamondButton.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(211 / 255f, 88 / 255f, 13 / 255f, 255 / 255f);
                    DiamondButton.GetComponent<BoxCollider>().enabled = true;
                }
            }
            //加载奖励
            AwardParent.SetActive(true);
            AwardUiGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
            for (int i = AwardUiGrid.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(AwardUiGrid.transform.GetChild(i).gameObject);
            }

            LegionCrap _LegionCrap = TextTranslator.instance.GetLegionCrapByID(HeroNum);
            for (int i = 0; i < _LegionCrap.CrapsAwardList.Count; i++)
            {
                if (_LegionCrap.CrapsAwardList[i].itemCode != 0)
                {
                    GameObject go = NGUITools.AddChild(AwardUiGrid, ItemIcon);
                    go.SetActive(true);
                    if (_LegionCrap.CrapsAwardList[i].itemCode > 70000 && _LegionCrap.CrapsAwardList[i].itemCode < 79999)
                    {
                        go.GetComponent<UISprite>().atlas = RoleAtlas;
                        go.GetComponent<UISprite>().spriteName = (_LegionCrap.CrapsAwardList[i].itemCode - 10000).ToString();
                        go.transform.Find("Suipian").gameObject.SetActive(true);
                    }
                    else if (_LegionCrap.CrapsAwardList[i].itemCode > 60000 && _LegionCrap.CrapsAwardList[i].itemCode < 70000)
                    {
                        go.GetComponent<UISprite>().atlas = RoleAtlas;
                        go.GetComponent<UISprite>().spriteName = _LegionCrap.CrapsAwardList[i].itemCode.ToString();
                    }
                    else
                    {
                        go.GetComponent<UISprite>().atlas = ItemAtlas;
                        go.GetComponent<UISprite>().spriteName = _LegionCrap.CrapsAwardList[i].itemCode.ToString();
                    }
                    go.transform.Find("LabelNumber").GetComponent<UILabel>().text = "X" + _LegionCrap.CrapsAwardList[i].itemCount.ToString();
                }
            }
            AwardUiGrid.GetComponent<UIGrid>().Reposition();
        }

        if (CanThrowNum == 0)
        {
            CanThrowNumber.text = "[ff0000]" + CanThrowNum.ToString() + "[-]" + "/" + TextTranslator.instance.GetLegionByID(CharacterRecorder.instance.myLegionData.legionLevel).CrapsPlayNum.ToString();
        }
        else
        {
            CanThrowNumber.text = CanThrowNum.ToString() + "/" + TextTranslator.instance.GetLegionByID(CharacterRecorder.instance.myLegionData.legionLevel).CrapsPlayNum.ToString();
        }

        //if (FreeButton.activeSelf == true)
        //{
        //    FreeButton.GetComponent<BoxCollider>().enabled = true;
        //}
        //else if (DiamondButton.activeSelf == true)
        //{
        //    DiamondButton.GetComponent<BoxCollider>().enabled = true;
        //} 
    }

    public void LegionChangeDice(string Recving)//8503
    {
        if (ChangeThrowNum > 0)
        {
            ChangeThrowNum--;
        }
        else 
        {
            DiamondChangeThrowNum++;
        }
        if (ChangeThrowNum > 0)
        {
            ChangeThrowNumber.text = ChangeThrowNum.ToString();
            FreeButton.SetActive(true);
            DiamondButton.SetActive(false);
            UIEventListener.Get(FreeButton).onClick = delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("8503#;");
            };
        }
        else
        {
            ChangeThrowNumber.text = ChangeThrowNum.ToString();
            FreeButton.SetActive(false);
            DiamondButton.SetActive(true);

            int diamondNum = TextTranslator.instance.GetMarketByBuyCount(DiamondChangeThrowNum+1).CrapsChangePrice;//**
            DiamondButton.transform.Find("CostLabel").GetComponent<UILabel>().text = diamondNum.ToString();
            UIEventListener.Get(DiamondButton).onClick = delegate(GameObject go)
            {
                if (diamondNum < CharacterRecorder.instance.lunaGem)
                {
                    NetworkHandler.instance.SendProcess("8503#;");
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("钻石不足", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        string[] allSplit = Recving.Split(';');
        SetSort(allSplit[0]);
        if (GameObject.Find("TopContent") != null)
        {
            CharacterRecorder.instance.gold = int.Parse(allSplit[1]);
            CharacterRecorder.instance.lunaGem = int.Parse(allSplit[2]);
            GameObject.Find("TopContent").GetComponent<TopContent>().Reset();
        }
    }

    private void SetSort(string Recving) //排序
    {
        string[] dataSplit = Recving.Split('$');
        int HeroNum = 0;
        SortList.Clear();
        for (int i = 0; i < dataSplit.Length - 1; i++) 
        {
            if (dataSplit[i] == "6") 
            {
                HeroNum++;
                SortList.Add(int.Parse(dataSplit[i]));
            }
        }

        for (int i = 0; i < dataSplit.Length - 1; i++)
        {
            if (dataSplit[i] != "6")
            {
                SortList.Add(int.Parse(dataSplit[i]));
            }
        }
        Debug.Log("SortList[0]" + SortList[0]);
        Debug.Log("SortList[1]" + SortList[1]);
        Debug.Log("SortList[2]" + SortList[2]);
        Debug.Log("SortList[3]" + SortList[3]);
        Debug.Log("SortList[4]" + SortList[4]);
        Debug.Log("SortList[5]" + SortList[5]);

        if (FreeButton.activeSelf == true) 
        {
            FreeButton.GetComponent<BoxCollider>().enabled = false;
        }
        else if (DiamondButton.activeSelf == true) 
        {
            DiamondButton.GetComponent<BoxCollider>().enabled = false;
        }

        for (int i = 0; i < SortList.Count; i++) //加载筛子
        {
            ArrDiceItem[SortList.Count - 1 - i].SetActive(true);
            if (SortList[i] == 6)
            {             
                //ArrDiceItem[SortList.Count - 1 - i].GetComponent<UISprite>().spriteName = "toushazi_shazi7";
                //ArrDiceItem[SortList.Count - 1 - i].transform.Find("HeroIcon").gameObject.SetActive(true);
                DiceVecPosition(ArrDiceItem[SortList.Count - 1 - i],SortList[i]);
            }
            else 
            {
                //ArrDiceItem[SortList.Count - 1 - i].GetComponent<UISprite>().spriteName = "toushazi_shazi" + SortList[i].ToString();
                //ArrDiceItem[SortList.Count - 1 - i].transform.Find("HeroIcon").gameObject.SetActive(false);//播放动画
                StartCoroutine(StayToPlayAnimation(ArrDiceItem[SortList.Count - 1 - i], SortList[i]));
            }
        }
        if (HeroNum == 6)
        {
            if (FreeButton.activeSelf == true)
            {
                FreeButton.GetComponent<UISprite>().spriteName = "buttonHui";
                FreeButton.GetComponent<UIButton>().normalSprite = "buttonHui";
                FreeButton.GetComponent<UIButton>().hoverSprite = "buttonHui";
                FreeButton.GetComponent<UIButton>().pressedSprite = "buttonHui_an";
                FreeButton.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(150 / 255f, 150 / 255f, 150 / 255f, 255 / 255f);
                FreeButton.GetComponent<BoxCollider>().enabled = false;
            }
            else if (DiamondButton.activeSelf == true)
            {
                DiamondButton.GetComponent<UISprite>().spriteName = "buttonHui";
                DiamondButton.GetComponent<UIButton>().normalSprite = "buttonHui";
                DiamondButton.GetComponent<UIButton>().hoverSprite = "buttonHui";
                DiamondButton.GetComponent<UIButton>().pressedSprite = "buttonHui_an";
                DiamondButton.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(150 / 255f, 150 / 255f, 150 / 255f, 255 / 255f);
                DiamondButton.GetComponent<BoxCollider>().enabled = false;
            }
        }
        else 
        {
            if (FreeButton.activeSelf == true)
            {
                FreeButton.GetComponent<UISprite>().spriteName = "ui2_button4";
                FreeButton.GetComponent<UIButton>().normalSprite = "ui2_button4";
                FreeButton.GetComponent<UIButton>().hoverSprite = "ui2_button4";
                FreeButton.GetComponent<UIButton>().pressedSprite = "ui2_button4_an";
                FreeButton.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(211 / 255f, 88 / 255f, 13 / 255f, 255 / 255f);
                FreeButton.GetComponent<BoxCollider>().enabled = true;
            }
            else if (DiamondButton.activeSelf == true)
            {
                DiamondButton.GetComponent<UISprite>().spriteName = "ui2_button4";
                DiamondButton.GetComponent<UIButton>().normalSprite = "ui2_button4";
                DiamondButton.GetComponent<UIButton>().hoverSprite = "ui2_button4";
                DiamondButton.GetComponent<UIButton>().pressedSprite = "ui2_button4_an";
                DiamondButton.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(211 / 255f, 88 / 255f, 13 / 255f, 255 / 255f);
                DiamondButton.GetComponent<BoxCollider>().enabled = true;
            }
        }

        //加载奖励
        AwardParent.SetActive(true);
        AwardUiGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        for (int i = AwardUiGrid.transform.childCount - 1; i >= 0; i--) 
        {
            DestroyImmediate(AwardUiGrid.transform.GetChild(i).gameObject);
        }

        LegionCrap _LegionCrap = TextTranslator.instance.GetLegionCrapByID(HeroNum);
        for (int i = 0; i < _LegionCrap.CrapsAwardList.Count; i++) 
        {
            if (_LegionCrap.CrapsAwardList[i].itemCode != 0) 
            {
                GameObject go = NGUITools.AddChild(AwardUiGrid, ItemIcon);
                go.SetActive(true);
                if (_LegionCrap.CrapsAwardList[i].itemCode > 70000 && _LegionCrap.CrapsAwardList[i].itemCode < 79999)
                {
                    go.GetComponent<UISprite>().atlas = RoleAtlas;
                    go.GetComponent<UISprite>().spriteName = (_LegionCrap.CrapsAwardList[i].itemCode-10000).ToString();
                    go.transform.Find("Suipian").gameObject.SetActive(true);
                }
                else if (_LegionCrap.CrapsAwardList[i].itemCode > 60000 && _LegionCrap.CrapsAwardList[i].itemCode < 70000) 
                {
                    go.GetComponent<UISprite>().atlas = RoleAtlas;
                    go.GetComponent<UISprite>().spriteName = _LegionCrap.CrapsAwardList[i].itemCode.ToString();
                }
                else
                {
                    go.GetComponent<UISprite>().atlas = ItemAtlas;
                    go.GetComponent<UISprite>().spriteName = _LegionCrap.CrapsAwardList[i].itemCode.ToString();
                }
                go.transform.Find("LabelNumber").GetComponent<UILabel>().text = "X" + _LegionCrap.CrapsAwardList[i].itemCount.ToString();
            }
        }
        AwardUiGrid.GetComponent<UIGrid>().Reposition();
    }

    public void ChangeDicAfterGetAward() //领取奖励后重置牌面
    {
        for (int i = 0; i < 6; i++) 
        {
            ArrDiceItem[i].SetActive(false);
        }
        AwardParent.SetActive(false);
        NetworkHandler.instance.SendProcess("8501#;");
        NetworkHandler.instance.SendProcess("8506#;");
    }

    private void DiceVecPosition(GameObject go,int DicNum)//确定3D筛子面
    {
        go.transform.Find("GameObject").GetComponent<NcCurveAnimation>().enabled = false;
        go.transform.Find("GameObject/shaizi").GetComponent<NcCurveAnimation>().enabled = false;
        go.transform.Find("GameObject").transform.Find("shaizi").transform.localRotation = new Quaternion(0, 0, 0, 0);
        Vector3 DicVec = new Vector3();
        switch (DicNum) 
        {
            case 1:
                DicVec = new Vector3(90f, 0f, 180f);
                break;
            case 2:
                DicVec = new Vector3(0f, 0f, 0f);
                break;
            case 3:
                DicVec = new Vector3(0f, 90f, 0f);
                break;
            case 4:
                DicVec = new Vector3(0f, -90f, 0f);
                break;
            case 5:
                DicVec = new Vector3(0f, -180f, 0f);
                break;
            case 6:
                DicVec = new Vector3(-90f, -180f, 0f);
                break;
            default:
                DicVec = new Vector3(0f, 0f, 0f);
                break;
        }

        //while(go.transform.Find("GameObject").transform.localEulerAngles.y)
        go.transform.Find("GameObject").transform.localRotation = new Quaternion(0, 0, 0, 0);
        go.transform.Find("GameObject").transform.Find("shaizi").transform.Rotate(DicVec);

        if (go == ArrDiceItem[0]) 
        {
            if (FreeButton.activeSelf == true)
            {
                FreeButton.GetComponent<BoxCollider>().enabled = true;
            }
            else if (DiamondButton.activeSelf == true)
            {
                DiamondButton.GetComponent<BoxCollider>().enabled = true;
            }
        }
    }

    private IEnumerator StayToPlayAnimation(GameObject go,int num) //改投播放动画
    {
        go.transform.Find("GameObject").GetComponent<NcCurveAnimation>().enabled = true;
        go.transform.Find("GameObject/shaizi").GetComponent<NcCurveAnimation>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        DiceVecPosition(go, num);
    }

    private IEnumerator ControlDicMove(string Recving)
    {
        CancelButton.GetComponent<BoxCollider>().enabled = false;//防止动画没播放完点击按钮
        FreeButton.GetComponent<BoxCollider>().enabled = false;
        DiamondButton.GetComponent<BoxCollider>().enabled = false;
        for (int i = 0; i < 6; i++)
        {
            ArrDiceItem[i].SetActive(false);
            ArrDiceItem[i].transform.Find("GameObject").GetComponent<NcCurveAnimation>().enabled = false;
            ArrDiceItem[i].transform.Find("GameObject/shaizi").GetComponent<NcCurveAnimation>().enabled = false;
            ArrDiceItem[i].transform.Find("GameObject").localRotation = new Quaternion(0, 0, 0, 0);
            ArrDiceItem[i].transform.Find("GameObject/shaizi").localRotation = new Quaternion(0, 0, 0, 0);
        }

        ScenceCamera.transform.localPosition = new Vector3(314f, 1050f, -700f);
        ScenceCamera.transform.localRotation = new Quaternion(0, 0, 0, 0);
        ScenceCamera.transform.Rotate(new Vector3(30, 0, 0));
        ScenceCamera.orthographicSize = 3;

        ArrDiceItem[0].transform.localPosition = new Vector3(123, 0, -200);

        ArrDiceItem[1].transform.localPosition = new Vector3(290, 0, -200);

        ArrDiceItem[2].transform.localPosition = new Vector3(216, 0, -335);

        ArrDiceItem[3].transform.localPosition = new Vector3(200, 0, -20);

        ArrDiceItem[4].transform.localPosition = new Vector3(350, 0, -20);

        ArrDiceItem[5].transform.localPosition = new Vector3(420, 0, -260);

        AnimationShaizhong.SetActive(true);

        yield return new WaitForSeconds(0.3f);
        AudioEditer.instance.PlayOneShot("Rollinair");
        yield return new WaitForSeconds(1.1f);
        AudioEditer.instance.PlayOneShot("Rollputdown");
        yield return new WaitForSeconds(0.5f);

        string[] dataSplit = Recving.Split('$');
        for (int i = 0; i < dataSplit.Length - 1; i++) 
        {
            ArrDiceItem[i].SetActive(true);
            float positionY = 0;

            switch (i) 
            {
                case 1:
                    positionY = 30f;
                    break;
                case 2:
                    positionY = 41f;
                    break;
                case 3:
                    positionY = 62f;
                    break;
                case 4:
                    positionY = 111f;
                    break;
                case 5:
                    positionY = 41f;
                    break;
                case 6:
                    positionY = 119f;
                    break;
            }
            ArrDiceItem[i].transform.Find("GameObject/shaizi").localRotation = new Quaternion(0, 0, 0, 0);

            switch (int.Parse(dataSplit[i]))
            {
                case 1:
                    ArrDiceItem[i].transform.Find("GameObject/shaizi").Rotate(new Vector3(0, positionY, 0));
                    break;
                case 2:
                    ArrDiceItem[i].transform.Find("GameObject/shaizi").Rotate(new Vector3(90, positionY, 0));
                    break;
                case 3:
                    ArrDiceItem[i].transform.Find("GameObject/shaizi").Rotate(new Vector3(0, positionY, 90));
                    break;
                case 4:
                    ArrDiceItem[i].transform.Find("GameObject/shaizi").Rotate(new Vector3(0, positionY, -90));
                    break;
                case 5:
                    ArrDiceItem[i].transform.Find("GameObject/shaizi").Rotate(new Vector3(-90, positionY, 0));
                    break;
                case 6:
                    ArrDiceItem[i].transform.Find("GameObject/shaizi").Rotate(new Vector3(-180, positionY, 0));
                    break;
                default:
                    ArrDiceItem[i].transform.Find("GameObject/shaizi").Rotate(new Vector3(0, positionY, 0));
                    break;
            }
        }

        yield return new WaitForSeconds(1.5f);


        ArrDiceItem[5].transform.localPosition = new Vector3(631.25f, 0, -150f);
        ArrDiceItem[5].transform.Find("GameObject").localRotation = new Quaternion(0, 0, 0, 0);
        ArrDiceItem[5].transform.Find("GameObject/shaizi").localRotation = new Quaternion(0, 0, 0, 0);

        ArrDiceItem[4].transform.localPosition = new Vector3(505f, 0, -150f);
        ArrDiceItem[4].transform.Find("GameObject").localRotation = new Quaternion(0, 0, 0, 0);
        ArrDiceItem[4].transform.Find("GameObject/shaizi").localRotation = new Quaternion(0, 0, 0, 0);

        ArrDiceItem[3].transform.localPosition = new Vector3(378.75f, 0, -150f);
        ArrDiceItem[3].transform.Find("GameObject").localRotation = new Quaternion(0, 0, 0, 0);
        ArrDiceItem[3].transform.Find("GameObject/shaizi").localRotation = new Quaternion(0, 0, 0, 0);

        ArrDiceItem[2].transform.localPosition = new Vector3(252.5f, 0, -150f);
        ArrDiceItem[2].transform.Find("GameObject").localRotation = new Quaternion(0, 0, 0, 0);
        ArrDiceItem[2].transform.Find("GameObject/shaizi").localRotation = new Quaternion(0, 0, 0, 0);

        ArrDiceItem[1].transform.localPosition = new Vector3(126.25f, 0, -150);
        ArrDiceItem[1].transform.Find("GameObject").localRotation = new Quaternion(0, 0, 0, 0);
        ArrDiceItem[1].transform.Find("GameObject/shaizi").localRotation = new Quaternion(0, 0, 0, 0);

        ArrDiceItem[0].transform.localPosition = new Vector3(0, 0, -150);
        ArrDiceItem[0].transform.Find("GameObject").localRotation = new Quaternion(0, 0, 0, 0);
        ArrDiceItem[0].transform.Find("GameObject/shaizi").localRotation = new Quaternion(0, 0, 0, 0);


        ScenceCamera.transform.localPosition = new Vector3(314f, 150f, -700f);
        ScenceCamera.transform.localRotation = new Quaternion(0, 0, 0, 0);
        ScenceCamera.transform.Rotate(new Vector3(0, 0, 0));
        ScenceCamera.orthographicSize = 1.12f;

        int HeroNum = 0;
        SortList.Clear();
        for (int i = 0; i < dataSplit.Length - 1; i++)
        {
            if (dataSplit[i] == "6")
            {
                HeroNum++;
                SortList.Add(int.Parse(dataSplit[i]));
            }
        }

        for (int i = 0; i < dataSplit.Length - 1; i++)
        {
            if (dataSplit[i] != "6")
            {
                SortList.Add(int.Parse(dataSplit[i]));
            }
        }

        if (FreeButton.activeSelf == true)
        {
            FreeButton.GetComponent<BoxCollider>().enabled = false;
        }
        else if (DiamondButton.activeSelf == true)
        {
            DiamondButton.GetComponent<BoxCollider>().enabled = false;
        }

        for (int i = 0; i < SortList.Count; i++) //加载筛子
        {
            ArrDiceItem[SortList.Count - 1 - i].SetActive(true);
            if (SortList[i] == 6)
            {
                DiceVecPosition(ArrDiceItem[SortList.Count - 1 - i], SortList[i]);
            }
            else
            {
                DiceVecPosition(ArrDiceItem[SortList.Count - 1 - i], SortList[i]);
            }
        }
        if (HeroNum == 6)
        {
            if (FreeButton.activeSelf == true)
            {
                FreeButton.GetComponent<UISprite>().spriteName = "buttonHui";
                FreeButton.GetComponent<BoxCollider>().enabled = false;
            }
            else if (DiamondButton.activeSelf == true)
            {
                DiamondButton.GetComponent<UISprite>().spriteName = "buttonHui";
                DiamondButton.GetComponent<BoxCollider>().enabled = false;
            }
        }

        //加载奖励
        AwardParent.SetActive(true);
        AwardUiGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        for (int i = AwardUiGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(AwardUiGrid.transform.GetChild(i).gameObject);
        }

        LegionCrap _LegionCrap = TextTranslator.instance.GetLegionCrapByID(HeroNum);
        for (int i = 0; i < _LegionCrap.CrapsAwardList.Count; i++)
        {
            if (_LegionCrap.CrapsAwardList[i].itemCode != 0)
            {
                GameObject go = NGUITools.AddChild(AwardUiGrid, ItemIcon);
                go.SetActive(true);
                if (_LegionCrap.CrapsAwardList[i].itemCode > 70000 && _LegionCrap.CrapsAwardList[i].itemCode < 79999)
                {
                    go.GetComponent<UISprite>().atlas = RoleAtlas;
                    go.GetComponent<UISprite>().spriteName = (_LegionCrap.CrapsAwardList[i].itemCode - 10000).ToString();
                    go.transform.Find("Suipian").gameObject.SetActive(true);
                }
                else if (_LegionCrap.CrapsAwardList[i].itemCode > 60000 && _LegionCrap.CrapsAwardList[i].itemCode < 70000)
                {
                    go.GetComponent<UISprite>().atlas = RoleAtlas;
                    go.GetComponent<UISprite>().spriteName = _LegionCrap.CrapsAwardList[i].itemCode.ToString();
                }
                else
                {
                    go.GetComponent<UISprite>().atlas = ItemAtlas;
                    go.GetComponent<UISprite>().spriteName = _LegionCrap.CrapsAwardList[i].itemCode.ToString();
                }
                go.transform.Find("LabelNumber").GetComponent<UILabel>().text = "X" + _LegionCrap.CrapsAwardList[i].itemCount.ToString();
            }
        }
        AwardUiGrid.GetComponent<UIGrid>().Reposition();


        AnimationShaizhong.SetActive(false);
        yield return new WaitForSeconds(0.2f);
        CancelButton.GetComponent<BoxCollider>().enabled = true;
        FreeButton.GetComponent<BoxCollider>().enabled = true;
        DiamondButton.GetComponent<BoxCollider>().enabled = true;
    }




    private IEnumerator DicMoveAndrRotate(GameObject diceitem)
    {
        while (diceitem.transform.localPosition.y > 0f)
        {
            diceitem.transform.localPosition = new Vector3(diceitem.transform.localPosition.x, diceitem.transform.localPosition.y - 50, diceitem.transform.localPosition.z);
            if (diceitem.transform.localPosition.y <= 0)
            {
                diceitem.transform.localPosition = new Vector3(diceitem.transform.localPosition.x, 0, diceitem.transform.localPosition.z);
            }
            yield return new WaitForSeconds(0.02f);
        }
    }

    #region 旧动画
    //private IEnumerator ControlDicMove(string Recving)
    //{
    //    for (int i = 0; i < 6; i++) 
    //    {
    //        ArrDiceItem[i].SetActive(true);
    //        ArrDiceItem[i].transform.Find("GameObject").GetComponent<NcCurveAnimation>().enabled = true;
    //        ArrDiceItem[i].transform.Find("GameObject/shaizi").GetComponent<NcCurveAnimation>().enabled = true;
    //        ArrDiceItem[i].transform.localPosition = new Vector3(ArrDiceItem[i].transform.localPosition.x, 440f, ArrDiceItem[i].transform.localPosition.z);
    //    }
    //    StartCoroutine(DicMoveAndrRotate(ArrDiceItem[5]));
    //    yield return new WaitForSeconds(0.2f);
    //    StartCoroutine(DicMoveAndrRotate(ArrDiceItem[4]));
    //    yield return new WaitForSeconds(0.2f);
    //    StartCoroutine(DicMoveAndrRotate(ArrDiceItem[3]));
    //    yield return new WaitForSeconds(0.2f);
    //    StartCoroutine(DicMoveAndrRotate(ArrDiceItem[2]));
    //    yield return new WaitForSeconds(0.2f);
    //    StartCoroutine(DicMoveAndrRotate(ArrDiceItem[1]));
    //    yield return new WaitForSeconds(0.2f);
    //    StartCoroutine(DicMoveAndrRotate(ArrDiceItem[0]));
    //    yield return new WaitForSeconds(0.5f);

    //    SetSort(Recving);
    //}

    //private IEnumerator DicMoveAndrRotate(GameObject diceitem) 
    //{
    //    while (diceitem.transform.localPosition.y > 0f) 
    //    {
    //        diceitem.transform.localPosition = new Vector3(diceitem.transform.localPosition.x, diceitem.transform.localPosition.y - 50, diceitem.transform.localPosition.z);
    //        if (diceitem.transform.localPosition.y <= 0) 
    //        {
    //            diceitem.transform.localPosition = new Vector3(diceitem.transform.localPosition.x, 0, diceitem.transform.localPosition.z);
    //        }
    //        yield return new WaitForSeconds(0.02f);
    //    }   
    //}
    #endregion
    IEnumerator PlayStartAnimation()//加载一开始的眨眼动画
    {
        while(IsStart)
        {
            HeroBgTexture.mainTexture=Resources.Load("GuideTexture/"+"bani_02") as Texture;
            HeroBgTexture.MakePixelPerfect();
            yield return new WaitForSeconds(0.1f);
            HeroBgTexture.mainTexture=Resources.Load("GuideTexture/"+"bani_01") as Texture;
            HeroBgTexture.MakePixelPerfect();
            int num = Random.Range(3, 5);
            yield return new WaitForSeconds(num);
        }
    }

    IEnumerator PlayAfterAnimation()//加载摇色子之后的眨眼动画
    {
        while (IsAfter)
        {
            HeroBgTexture.mainTexture = Resources.Load("GuideTexture/" + "bani_02") as Texture;
            HeroBgTexture.MakePixelPerfect();
            yield return new WaitForSeconds(0.1f);
            HeroBgTexture.mainTexture = Resources.Load("GuideTexture/" + "bani_03") as Texture;
            HeroBgTexture.MakePixelPerfect();
            int num = Random.Range(3, 5);
            yield return new WaitForSeconds(num);
        }
    }

    public void EverydayFirst(string Recving) 
    {
        string[] dataSplit = Recving.Split(';');
        if (dataSplit[0] != "")
        {
            string[] trcSplit = dataSplit[0].Split('$');
            BangdanTexture.SetActive(true);
            BangdanTexture.transform.Find("CharacterName").GetComponent<UILabel>().text = trcSplit[0];
            BangdanTexture.transform.Find("BaNiNum").GetComponent<UILabel>().text = trcSplit[1] + "个巴尼碎片";
        }
        else 
        {
            BangdanTexture.SetActive(false);
        }
    }

}




/*

// 一个为摇色子服务的脚本<br />
// 色子朝上的面默认为世界空间的正方向，只用1，2，3来定义世界空间<br />
// 的向量，比如1代表世界的上，2代表右，3代表前<br />
public delegate void RollCompleteEvent(object sender, int faceUp);
// 这个类代表一个六面色子的行为. 当这个类加载的时候，色子会以机<br />
//作加载在空中。<br />
// 当色子停下后 event RollComplete 会被激活<br />

public sealed class DieBehaviour : MonoBehaviour {

public event RollCompleteEvent RollComplete;

  //色子可能的朝向<br />
  //Vector3.up           1(+) or 6(-)<br />
  //Vector3.right,       2(+) or 5(-)<br />
  //Vector3.forward];    3(+) or 4(-)<br />
  Vector3[] _sides = {Vector3.up, Vector3.right, -Vector3.forward };
  //声明isSleeping变量为否，即开始加载rigidbody给色子<br />
  private bool _isSleeping = false;
//寻找色子哪个面朝上，将结果返回<br />


private int WhichIsUp()
{
  //定义maxY为负无穷<br />
  float maxY = float.NegativeInfinity;
  int result = -1;
  for(int i=0; i < 3; i++) {
  //转换物体朝向到世界空间<br />
    Vector3 worldSpace = transform.TransformDirection(_sides[i]);
    // 测试哪面的y值更高 测正方向的面 1(+) 2(+) 3(+)<br />
    if(worldSpace.y > maxY)
    {
      result = i+1;
      maxY = worldSpace.y;
    }
    // 测试反方向的面 6(-) 5(-) 4(-)<br />
    if(-worldSpace.y > maxY)
    {
      result = 6-i;
      maxY=-worldSpace.y;
    }
  }
  return result;
}
// 查看色子是否停止滚动，使rigidbody睡眠，即暂停<br />
private bool IsAtRest()
{
  _isSleeping = rigidbody.IsSleeping();
  return _isSleeping;
}

#region "Unity Called Methods/Events"<br />
private void Start ()
{
  // 以随机的方法投掷色子<br />
  rigidbody.AddRelativeTorque(Vector3.up * ((UnityEngine.Random.value * 20) + 10));
  rigidbody.AddRelativeTorque(Vector3.forward * ((UnityEngine.Random.value * 20) + 10));
  rigidbody.AddRelativeTorque(Vector3.right * ((UnityEngine.Random.value * 20) + 10));
  rigidbody.AddRelativeForce(Vector3.up * ((UnityEngine.Random.value * 120) + 30));
  rigidbody.AddRelativeForce(Vector3.left * ((UnityEngine.Random.value * 170) + 30));
}
private void Update ()
{
  // 仅仅投掷得到结果1次<br />
  if(!_isSleeping)
  {
    if(IsAtRest())
    {
      if(RollComplete != null)
        RollComplete(this,WhichIsUp());
    }
  }
}
#endregion
} */