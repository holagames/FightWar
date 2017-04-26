using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionGachaEnterWindow : MonoBehaviour
{
    public List<GameObject> ConfirmLegionItemList = new List<GameObject>();
    public GameObject backButton;
    public GameObject enterButton;
    public GameObject legionRankButton;
    public GameObject PreviewButton;
    public UISlider mSlider;
    public UILabel mSliderLabel;
    public UILabel mTimeLabel;
    public UILabel mNameLabel;
    private int leftTime;
    private int FightStyle = 12;//军团副本
    private BetterList<int> percentList = new BetterList<int>();
    private bool canGacha = false;
    // Use this for initialization
    void Start()
    {
        NetworkHandler.instance.SendProcess("8201#;");//获取挑战剩余次数
        //NetworkHandler.instance.SendProcess(string.Format("8202#{0};", LegionCopyItem.curClickLegionGate.GateGroupID));//旧的UI
        NetworkHandler.instance.SendProcess(string.Format("8202#{0};", LegionCopyWindowNew.curSelectGateGroupID));
        for (int i = 0; i < ConfirmLegionItemList.Count; i++)
        {
            //UIEventListener.Get(ConfirmLegionItemList[i]).onClick += ClickConfirmLegionItem;
            UIEventListener.Get(ConfirmLegionItemList[i]).onClick += ClickConfirmLegionItemNew;
        }
        /* if (UIEventListener.Get(backButton).onClick == null)
         {
             UIEventListener.Get(backButton).onClick += delegate(GameObject go)
             {
                 UIManager.instance.BackUI();
             };
         }*/
        if (UIEventListener.Get(legionRankButton).onClick == null)
        {
            UIEventListener.Get(legionRankButton).onClick += delegate(GameObject go)
            {
                //UIManager.instance.OpenPanel("LegionSecondWindow", true);
                //LegionSecondWindow.enterFromLegionCopy = true;
                //UIManager.instance.OpenPromptWindow("即将开放", PromptWindow.PromptType.Alert, null, null);
                UIManager.instance.OpenPanel("LegionHertRankWindow", false);

            };
        }
        if (UIEventListener.Get(enterButton).onClick == null)
        {
            UIEventListener.Get(enterButton).onClick += delegate(GameObject go)
            {
                /*  UIManager.instance.OpenPanel("LegionGachaWindow", true);
                  GameObject _LegionGachaWindow = GameObject.Find("LegionGachaWindow");
                  if (_LegionGachaWindow != null)
                  {
                      _LegionGachaWindow.GetComponent<LegionGachaWindow>().SetLegionGachaPercentList(this.percentList);
                  } */
                ClickEnterButton();
            };
        }

        UIEventListener.Get(PreviewButton).onClick = delegate(GameObject go)
        {
            //LegionGachaWindow.curGateBoxID = 1;
            UIManager.instance.OpenPanel("LegionGachaWindow", true);
            GameObject _LegionGachaWindow = GameObject.Find("LegionGachaWindow");
            if (_LegionGachaWindow != null)
            {
                _LegionGachaWindow.GetComponent<LegionGachaWindow>().SetLegionGachaPercentList(this.percentList);
            }
        };
    }

    public void SetLegionGachaEnterWindow(int leftTime, int GateGroupID, BetterList<int> percentList)
    {
        this.leftTime = leftTime;
        this.percentList = percentList;
        CancelInvoke();
        InvokeRepeating("UpdateTime", 0, 1.0f);

        mNameLabel.text = string.Format("第{0}关 {1}", GateGroupID, TextTranslator.instance.GetGateByID(TextTranslator.instance.GetLegionSmallGateListByGateGroupID(GateGroupID)[0].NextGateID).name);

        int sumPercent = 0;
        for (int i = 0; i < ConfirmLegionItemList.Count; i++)
        {
            ConfirmLegionItemList[i].transform.FindChild("Select").gameObject.SetActive(false);
            ConfirmLegionItemList[i].transform.FindChild("PercentLabel").GetComponent<UILabel>().text = string.Format("{0}%", percentList[i]);
            if (percentList[i] == 100)
            {
                ConfirmLegionItemList[i].transform.FindChild("HadPass").gameObject.SetActive(true);
            }
            else
            {
                ConfirmLegionItemList[i].transform.FindChild("HadPass").gameObject.SetActive(false);
            }
            sumPercent += percentList[i];
        }
        mSlider.value = (float)sumPercent / ConfirmLegionItemList.Count / 100;
        mSliderLabel.text = string.Format("{0}%", sumPercent / ConfirmLegionItemList.Count);

       /* canGacha = percentList[0] == 100 ? true : false;
        ConfirmLegionItemList[0].transform.FindChild("Select").gameObject.SetActive(true);*/
        ClickConfirmLegionItemNew(ConfirmLegionItemList[LegionGachaWindow.curGateBoxID - 1]);
    }
    void UpdateTime()
    {
        if (leftTime > 0)
        {
            string houreStr = (leftTime / 3600).ToString("00");
            string miniteStr = (leftTime % 3600 / 60).ToString("00");
            string secondStr = (leftTime % 60).ToString("00");
            mTimeLabel.text = string.Format("{0}:{1}:{2}", houreStr, miniteStr, secondStr);
            leftTime -= 1;
        }
    }
    int NextGateID = 0;//关卡
    int GateBoxID = 1;//小关卡 1- 5
    void ClickConfirmLegionItemNew(GameObject go)
    {
        if (go != null)
        {
            switch (go.name)
            {
                case "ConfirmLegionItem1": GateBoxID = 1; break;
                case "ConfirmLegionItem2": GateBoxID = 2; break;
                case "ConfirmLegionItem3": GateBoxID = 3; break;
                case "ConfirmLegionItem4": GateBoxID = 4; break;
                case "ConfirmLegionItem5": GateBoxID = 5; break;
            }
            LegionGachaWindow.curGateBoxID = GateBoxID;
            UISprite _enterSprite = enterButton.GetComponent<UISprite>();
            for (int i = 0; i < ConfirmLegionItemList.Count; i++)
            {
                if (ConfirmLegionItemList[i].name == go.name)
                {
                    if (percentList[i] == 100)
                    {
                        LegionGachaWindow.curGateBoxID = GateBoxID;
                        _enterSprite.spriteName = "buttonword2";
                        _enterSprite.MakePixelPerfect();
                        canGacha = true;
                    }
                    else
                    {
                        _enterSprite.spriteName = "buttonword";
                        _enterSprite.MakePixelPerfect();
                        canGacha = false;
                    }
                    ConfirmLegionItemList[i].transform.FindChild("Select").gameObject.SetActive(true);
                }
                else
                {
                    ConfirmLegionItemList[i].transform.FindChild("Select").gameObject.SetActive(false);
                }
            }

        }
    }

    void ClickEnterButton()
    {
        if (canGacha)
        {
            UIManager.instance.OpenPanel("LegionGachaWindow", true);
            GameObject _LegionGachaWindow = GameObject.Find("LegionGachaWindow");
            if (_LegionGachaWindow != null)
            {
               _LegionGachaWindow.GetComponent<LegionGachaWindow>().SetLegionGachaPercentList(this.percentList);
            }
        }
        else
        {
            if (percentList[GateBoxID - 1] == 100)
            {
                UIManager.instance.OpenPromptWindow("当前副本已通关", PromptWindow.PromptType.Hint, null, null);
                return;
            }
            if (CharacterRecorder.instance.legionFightLeftTimes <= 0)
            {
                UIManager.instance.OpenPromptWindow("没有副本挑战次数啦", PromptWindow.PromptType.Hint, null, null);
                return;
            }
            //LegionGate _LegionGate = TextTranslator.instance.GetLegionGateByGroupIDAndBoxID(LegionCopyItem.curClickLegionGate.GateGroupID, GateBoxID);//旧的UI
            LegionGate _LegionGate = TextTranslator.instance.GetLegionGateByGroupIDAndBoxID(LegionCopyWindowNew.curSelectGateGroupID, GateBoxID); 

            #region 接入战斗
            NextGateID = _LegionGate.NextGateID;
            UIManager.instance.OpenPanel("LoadingWindow", true);
            SceneTransformer.instance.NowGateID = NextGateID;
            PictureCreater.instance.StartFight();
            PictureCreater.instance.FightStyle = FightStyle;

           // NetworkHandler.instance.SendProcess(string.Format("8305#{0};{1};", LegionCopyItem.curClickLegionGate.GateGroupID, GateBoxID));//旧的UI
            NetworkHandler.instance.SendProcess(string.Format("8305#{0};{1};", LegionCopyWindowNew.curSelectGateGroupID, GateBoxID));
            #endregion
        }
    }

    void ClickConfirmLegionItem(GameObject go)
    {
        if (go != null)
        {
            int NextGateID = 0;//关卡
            int GateBoxID = 0;//小关卡 1- 5
            switch (go.name)
            {
                case "ConfirmLegionItem1": GateBoxID = 1; break;
                case "ConfirmLegionItem2": GateBoxID = 2; break;
                case "ConfirmLegionItem3": GateBoxID = 3; break;
                case "ConfirmLegionItem4": GateBoxID = 4; break;
                case "ConfirmLegionItem5": GateBoxID = 5; break;
            }
            if (percentList[GateBoxID - 1] == 100)
            {
                UIManager.instance.OpenPromptWindow("当前副本已通关", PromptWindow.PromptType.Hint, null, null);
                return;
            }
            if (CharacterRecorder.instance.legionFightLeftTimes <= 0)
            {
                UIManager.instance.OpenPromptWindow("没有副本挑战次数啦", PromptWindow.PromptType.Hint, null, null);
                return;
            }
          //  LegionGate _LegionGate = TextTranslator.instance.GetLegionGateByGroupIDAndBoxID(LegionCopyItem.curClickLegionGate.GateGroupID, GateBoxID);//旧的UI
            LegionGate _LegionGate = TextTranslator.instance.GetLegionGateByGroupIDAndBoxID(LegionCopyWindowNew.curSelectGateGroupID, GateBoxID); 
            #region 临时测试1
            /*  string sendBloodStr = "";
            for (int i = 0; i < _LegionGate.BossBloodList.size;i++ )
            {
                if (_LegionGate.BossBloodList[i] != 0)
                {
                    sendBloodStr += _LegionGate.BossBloodList[i] + "$";
                }
            }
            NetworkHandler.instance.SendProcess(string.Format("8306#{0};{1};{2};",LegionCopyItem.curClickLegionGate.GateGroupID, GateBoxID, sendBloodStr)); */

            //临时测试2
            //   NetworkHandler.instance.SendProcess(string.Format("8305#{0};{1};", LegionCopyItem.curClickLegionGate.GateGroupID, GateBoxID));
            #endregion


            #region 接入战斗
            NextGateID = _LegionGate.NextGateID;
            //CharacterRecorder.instance.EveryDiffID = 1;
            UIManager.instance.OpenPanel("LoadingWindow", true);
            SceneTransformer.instance.NowGateID = NextGateID;
            PictureCreater.instance.StartFight();
            PictureCreater.instance.FightStyle = FightStyle;

            //NetworkHandler.instance.SendProcess(string.Format("8305#{0};{1};", LegionCopyItem.curClickLegionGate.GateGroupID, GateBoxID));//旧的UI
            NetworkHandler.instance.SendProcess(string.Format("8305#{0};{1};", LegionCopyWindowNew.curSelectGateGroupID, GateBoxID));
            #endregion

        }
    }

    /* void DestroyGride()
     {
         for (int i = uiGride.transform.childCount - 1; i >= 0; i--)
         {
             DestroyImmediate(uiGride.transform.GetChild(i).gameObject);
         }
     }*/
    void ConfirmBuy()
    {
        //NetworkHandler.instance.SendProcess("8009#" + OneLegionMemberData.uId + ";");
    }
}
