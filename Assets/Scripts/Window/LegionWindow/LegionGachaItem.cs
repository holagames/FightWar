using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionGachaItem : MonoBehaviour 
{
    public GameObject SpriteMask;
    public UILabel AwardLevelLabel;
    public UILabel AwardNameLabel;
    private LegionGachaItemData _LegionGachaItemData;
	// Use this for initialization
	void Start () 
    {
        if (UIEventListener.Get(this.gameObject).onClick == null)
        {
            UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
            {
                switch (LegionGachaWindow.gachaState[LegionGachaWindow.curGateBoxID - 1])
                {
                    case 0: UIManager.instance.OpenPromptWindow("打过当前副本后才可刮奖", PromptWindow.PromptType.Hint, null, null); break;
                    case 1:
                        // NetworkHandler.instance.SendProcess(string.Format("8304#{0};{1};{2};", LegionCopyItem.curClickLegionGate.GateGroupID, LegionGachaWindow.curGateBoxID, this._LegionGachaItemData.positionNum));//旧的UI
                        NetworkHandler.instance.SendProcess(string.Format("8304#{0};{1};{2};", LegionCopyWindowNew.curSelectGateGroupID, LegionGachaWindow.curGateBoxID, this._LegionGachaItemData.positionNum));
                    //SpriteMask.SetActive(false);
                        break;
                    case 2: UIManager.instance.OpenPromptWindow("当前副本已经刮过奖了", PromptWindow.PromptType.Hint, null, null); break;
                    case 3: UIManager.instance.OpenPromptWindow("军团新成员重置军团副本后才可以领取", PromptWindow.PromptType.Hint, null, null); break;
                }
                
            };
        }
        //InitMyData();
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    public void SetLegionGachaItem(LegionGachaItemData _LegionGachaItemData)
    {
        this._LegionGachaItemData = _LegionGachaItemData;
        string bigNumStr = "一";
        switch (_LegionGachaItemData.awardNum)
        {
            case 1: bigNumStr = "一"; break;
            case 2: bigNumStr = "二"; break;
            case 3: bigNumStr = "三"; break;
            case 4: bigNumStr = "四"; break;
            case 5: bigNumStr = "五"; break;
            case 6: bigNumStr = "六"; break;
            case 7: bigNumStr = "七"; break;
        }
        AwardLevelLabel.text = string.Format("{0}等奖:", bigNumStr);
        AwardNameLabel.text = string.Format(_LegionGachaItemData.name);
        if (_LegionGachaItemData.name != "")
        {
            SpriteMask.SetActive(false);
        }
    }
    public void ResetLegionGachaItem(int position, int awardNum)
    {
        if (this._LegionGachaItemData.positionNum != position)
        {
            return;
        }
        SpriteMask.SetActive(false);
        LegionGachaItemData _LegionGachaItemData = new LegionGachaItemData(this._LegionGachaItemData.positionNum, CharacterRecorder.instance.characterName, awardNum);
        SetLegionGachaItem(_LegionGachaItemData);

        int _LegionGateBoxID = (LegionGachaWindow.curGateBoxID - 1) * 5 + awardNum;
        LegionGateBox _LegionGateBox = TextTranslator.instance.GetLegionGateBoxByID(_LegionGateBoxID);
        List<Item> _itemList = new List<Item>();
        _itemList.Add(new Item(_LegionGateBox.ItemID, _LegionGateBox.ItemNum));
        OpenGainWindow(_itemList);
        UpDateTopContentData(_itemList);
        if (awardNum == 1) 
        {
            NetworkHandler.instance.SendProcess(string.Format("7002#{0};{1};{2};{3}", 21, CharacterRecorder.instance.characterName, TextTranslator.instance.GetItemNameByItemCode(_LegionGateBox.ItemID), _LegionGateBox.ItemNum));
        }       
    }
    void OpenGainWindow(List<Item> _itemList)
    {
        UIManager.instance.OpenSinglePanel("AdvanceWindow", false);
        GameObject.Find("AdvanceWindow").layer = 11;
        AdvanceWindow aw = GameObject.Find("AdvanceWindow").GetComponent<AdvanceWindow>();
        aw.SetInfo(AdvanceWindowType.GainResult, null, null, null, null, _itemList);
    }
    void UpDateTopContentData(List<Item> _itemList)
    {
        for (int i = 0; i < _itemList.Count; i++)
        {
            switch (_itemList[i].itemCode)
            {
                case 90001: CharacterRecorder.instance.gold += _itemList[i].itemCount; break;
                case 90002: CharacterRecorder.instance.lunaGem += _itemList[i].itemCount; break;
                case 90003: CharacterRecorder.instance.HonerValue += _itemList[i].itemCount; break;
                case 90004: break;
                case 90005: break;
                case 90006: break;
                case 90007: CharacterRecorder.instance.stamina += _itemList[i].itemCount; break;
                case 90008: CharacterRecorder.instance.sprite += _itemList[i].itemCount;
                    if (GameObject.Find("RobberyHeroList") != null)
                    {
                        GameObject.Find("RobberyHeroList").GetComponent<RobberyHeroList>().NowValue.GetComponent<UILabel>().text = CharacterRecorder.instance.sprite.ToString();
                    }
                    break;
                default: //TextTranslator.instance.SetItemCountAddByID(_itemList[i].itemCode, _itemList[i].itemCount);
                    break;
            }
        }
        if (GameObject.Find("TopContent") != null)
        {
            GameObject.Find("TopContent").GetComponent<TopContent>().Reset();
        }
    }
    void InitMyData()
    {
        int awardLevel = Random.Range(1,7);
        string bigNumStr = "一";
        switch (awardLevel)
        {
            case 1: bigNumStr = "一"; break;
            case 2: bigNumStr = "二"; break;
            case 3: bigNumStr = "三"; break;
            case 4: bigNumStr = "四"; break;
            case 5: bigNumStr = "五"; break;
            case 6: bigNumStr = "六"; break;
            case 7: bigNumStr = "七"; break;
        }
        AwardLevelLabel.text = string.Format("{0}等奖:", bigNumStr);
    }
}
public class LegionGachaItemData
{
    public int positionNum { get; set; }
    public string name { get; set; }
    public int awardNum { get; set; }
    public LegionGachaItemData(int positionNum, string name, int awardNum)
    {
        this.positionNum = positionNum;
        this.name = name;
        this.awardNum = awardNum;
    }
    public LegionGachaItemData(int positionNum)
    {
        this.positionNum = positionNum;
    }
}
