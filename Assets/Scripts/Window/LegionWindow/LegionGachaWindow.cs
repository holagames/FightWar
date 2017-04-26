using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionGachaWindow: MonoBehaviour 
{
    public static int curGateBoxID = 1;//小关 1- 5
    public static BetterList<int> gachaState = new BetterList<int>();////小关 1- 5 抽奖状态
    public List<GameObject> ConfirmLegionItemList = new List<GameObject>();
    public List<GameObject> ConfirmAwardsItemList = new List<GameObject>();
    public GameObject backButton;
    public GameObject closeButton;
    public GameObject uiGrideLeft;
    public GameObject uiGrideRight;
    public GameObject itemRight;
    public GameObject itemLeft;
    
	// Use this for initialization
	void Start () 
    {
        if (UIEventListener.Get(backButton).onClick == null)
        {
            UIEventListener.Get(backButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }
        if (UIEventListener.Get(closeButton).onClick == null)
        {
            UIEventListener.Get(closeButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }
        DestroyGride(uiGrideRight);
        //ClickConfirmLegionItem(ConfirmLegionItemList[0]);
        ClickConfirmLegionItem(ConfirmLegionItemList[curGateBoxID - 1]);
        
        for (int i = 0; i < ConfirmLegionItemList.Count; i++)
        {
            UIEventListener.Get(ConfirmLegionItemList[i]).onClick += ClickConfirmLegionItem;
        }
       /* if (UIEventListener.Get(legionRankButton).onClick == null)
        {
            UIEventListener.Get(legionRankButton).onClick += delegate(GameObject go)
            {
                //UIManager.instance.OpenPanel("LegionSecondWindow", true);
                //LegionSecondWindow.enterFromLegionCopy = true;
                UIManager.instance.OpenPromptWindow("即将开放", PromptWindow.PromptType.Alert, null, null);
            };
        }*/
        
	}

    public void SetLegionGachaWindow(BetterList<int> gachaState,BetterList<LegionGachaItemData> _GachaItemList)
    {
        LegionGachaWindow.gachaState = gachaState;
        for (int i = 0; i < ConfirmLegionItemList.Count; i++)
        {
            Transform _mask = ConfirmLegionItemList[i].transform.FindChild("HadGachaSprite");            
            switch (gachaState[i])
            {
                case 2:
                    _mask.gameObject.SetActive(true);
                    _mask.GetComponent<UISprite>().spriteName = "hadGacha";
                    break;
                case 3:
                    _mask.gameObject.SetActive(true);
                    _mask.GetComponent<UISprite>().spriteName = "hadGacha2";
                    break;
                default: _mask.gameObject.SetActive(false);
                    break;
            }
        }
        //DestroyGride(uiGrideRight);
        for (int i = 0; i < 48; i++)
        {
            GameObject obj = NGUITools.AddChild(uiGrideRight, itemRight);
            if (i < _GachaItemList.size)
            {
                obj.GetComponent<LegionGachaItem>().SetLegionGachaItem(_GachaItemList[i]);
            }
            else
            {
                LegionGachaItemData _LegionGachaItemData = new LegionGachaItemData(i + 1);
                obj.GetComponent<LegionGachaItem>().SetLegionGachaItem(_LegionGachaItemData);
            }
        }
        uiGrideRight.GetComponent<UIGrid>().Reposition();

        SetBottomAwardsInfo();
    }

    /// <summary>
    /// 刮奖后 更新
    /// </summary>
    public void ResetLegionGachaWindow(int bigGate, int smallGate, int position, int awardNum)
    {
        LegionGachaWindow.gachaState[smallGate - 1] = 2;
        Transform _mask = ConfirmLegionItemList[smallGate - 1].transform.FindChild("HadGachaSprite");
        _mask.gameObject.SetActive(true);
        _mask.GetComponent<UISprite>().spriteName = "hadGacha";
        //if (LegionCopyItem.curClickLegionGate.GateGroupID == bigGate && curGateBoxID == smallGate)//旧的UI
        if (LegionCopyWindowNew.curSelectGateGroupID == bigGate && curGateBoxID == smallGate)
        {
            for (int i = 0; i < uiGrideRight.transform.childCount;i++ )
            {
                uiGrideRight.transform.GetChild(i).GetComponent<LegionGachaItem>().ResetLegionGachaItem(position, awardNum);
              /*  if (uiGrideRight.transform.GetChild(i).GetComponent<LegionGachaItem>()._LegionGachaItemData.positionNum == position)
                {

                }*/
            }
        }
    }

    /// <summary>
    /// 左边小关卡 百分比
    /// </summary>
    public void SetLegionGachaPercentList(BetterList<int> percentList)
    {
        BetterList<LegionGate> mList = TextTranslator.instance.GetLegionSmallGateListByGateGroupID(LegionCopyWindowNew.curSelectGateGroupID);
        for (int i = 0; i < ConfirmLegionItemList.Count; i++)
        {
            ConfirmLegionItemList[i].transform.FindChild("PercentLabel").GetComponent<UILabel>().text = string.Format("{0}%", percentList[i]);
            ConfirmLegionItemList[i].transform.FindChild("Icon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetGateByID(mList[i].NextGateID).icon.ToString(); ;
        }
    }

    /// <summary>
    /// 底部 奖励预览
    /// </summary>
    void SetBottomAwardsInfo()
    {
        List<LegionGateBox> _LegionGateBoxList = TextTranslator.instance.GetLegionGateBoxByGateBoxID(curGateBoxID);
        List<Item> _itemList = new List<Item>();
        for (int i = 0; i < _LegionGateBoxList.Count;i++ )
        {
            _itemList.Add(new Item(_LegionGateBoxList[i].ItemID, _LegionGateBoxList[i].ItemNum));
        }
        for (int i = 0; i < ConfirmAwardsItemList.Count;i++ )
        {
            if (i < _itemList.Count)
            {
                ConfirmAwardsItemList[i].GetComponent<UISprite>().spriteName = _itemList[i].itemCode.ToString();
                ConfirmAwardsItemList[i].transform.FindChild("CountLabel").GetComponent<UILabel>().text = string.Format("×{0}", _itemList[i].itemCount);
            }
        }
    }

    void DestroyGride(GameObject uiGride)
    {
        for (int i = uiGride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGride.transform.GetChild(i).gameObject);
        }
    }
    void ClickConfirmLegionItem(GameObject go)
    {
        if (go != null)
        {
            //int GateGroupID = 0;//大关
            DestroyGride(uiGrideRight);
            switch (go.name)
            {
                case "ConfirmLegionItem1": curGateBoxID = 1; break;
                case "ConfirmLegionItem2": curGateBoxID = 2; break;
                case "ConfirmLegionItem3": curGateBoxID = 3; break;
                case "ConfirmLegionItem4": curGateBoxID = 4; break;
                case "ConfirmLegionItem5": curGateBoxID = 5; break;
            }
            for (int i = 0; i < ConfirmLegionItemList.Count; i++)
            {
                if (i == curGateBoxID - 1)
                {
                    ConfirmLegionItemList[i].GetComponent<UIToggle>().startsActive = true;
                }
                else
                {
                    ConfirmLegionItemList[i].GetComponent<UIToggle>().startsActive = false;
                }
            }
           // NetworkHandler.instance.SendProcess(string.Format("8303#{0};{1};", LegionCopyItem.curClickLegionGate.GateGroupID, curGateBoxID));//旧的UI
            NetworkHandler.instance.SendProcess(string.Format("8303#{0};{1};", LegionCopyWindowNew.curSelectGateGroupID, curGateBoxID));
        }
    }
}
