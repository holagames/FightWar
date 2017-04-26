using UnityEngine;
using System.Collections;

public class SmallGoalWindow : MonoBehaviour
{

    public GameObject uiGrid;
    public GameObject Item;
    public GameObject CloseButton;
    public GameObject AwardButton;
    public UILabel ExplainLabel;
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;
    private int smallGoalID = 0;
    /// <summary>
    /// 设置是否领取
    /// </summary>
    private bool isGetItem = false;
    void Start()
    {

        UIEventListener.Get(CloseButton).onClick = delegate(GameObject go)
        {
            DestroyImmediate(this.gameObject);
        };

        UIEventListener.Get(AwardButton).onClick = delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("1922#" + smallGoalID + ";");
        };
        //NetworkHandler.instance.SendProcess("1921#;");
        ShowInfoLastGateIDAward();
    }

    //public void ShowInfoLastGateIDAward(int _smallGoalID, int quest)
    //{
    //    ExplainLabel.text = "第" + (quest - 10000).ToString() + "关达到目标获得物品:";
    //    int smallGoalID = 0;
    //    for (int i = 0; i < TextTranslator.instance.SmallGoalList.size; i++)
    //    {
    //        if (TextTranslator.instance.SmallGoalList[i].Quest == CharacterRecorder.instance.lastGateID)
    //        {
    //            smallGoalID = TextTranslator.instance.SmallGoalList[i].SmallGoalID;
    //        }
    //    }
    //    for (int i = uiGrid.transform.childCount - 1; i >= 0; i--)
    //    {
    //        DestroyImmediate(uiGrid.transform.GetChild(i).gameObject);
    //    }
    //    this.smallGoalID = _smallGoalID;
    //    if (smallGoalID != 0)
    //    {
    //        SmallGoal smallGoal = TextTranslator.instance.GetSmallGoalByID(smallGoalID);
    //        GameObject go = NGUITools.AddChild(uiGrid, Item);
    //        go.SetActive(true);
    //        //SetItemDetail(smallGoal.ItemID, smallGoal.ItemNum, go, quest);
    //        uiGrid.GetComponent<UIGrid>().Reposition();
    //    }
    //}
    /// <summary>
    /// 设置小目标的Item对象
    /// </summary>
    public void ShowInfoLastGateIDAward()
    {
        for (int i = uiGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGrid.transform.GetChild(i).gameObject);
        }
        for (int i = 0; i < TextTranslator.instance.SmallGoalList.size; i++)
        {
            SmallGoal smallGoal = TextTranslator.instance.SmallGoalList[i];
            GameObject go = NGUITools.AddChild(uiGrid, Item);
            go.SetActive(true);
            //UIEventListener.Get(go).onClick = (GameObject goObj) =>
            //    {
            //        if (goObj == null)
            //        {
            //            return;
            //        }
            //        bool isGetItemBool = CharacterRecorder.instance.lastGateID >= smallGoal.Quest ? true : false;
            //        int goalId = PlayerPrefs.GetInt("SmallGoalGetID", 0);
            //        if (!isGetItemBool)
            //        {
            //            UIManager.instance.OpenPromptWindow("不满足领取条件", PromptWindow.PromptType.Hint, null, null);
            //            return;
            //        }
            //        if (goalId >= smallGoal.Quest)
            //        {
            //            UIManager.instance.OpenPromptWindow("奖励已领取", PromptWindow.PromptType.Hint, null, null);
            //            return;
            //        }
            //        NetworkHandler.instance.SendProcess("1922#" + smallGoal.SmallGoalID + ";");
            //    };
            SetItemDetail(smallGoal, go);
        }
        uiGrid.GetComponent<UIGrid>().Reposition();
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="_itemId"></param>
    /// <param name="_itemCount"></param>
    /// <param name="ItemObj"></param>
    private void SetItemDetail(SmallGoal _smallGoal, GameObject ItemObj)
    {
        UILabel itemGuanLabel = ItemObj.transform.Find("GuanLabel").GetComponent<UILabel>();
        UILabel itemGetLabel = ItemObj.transform.Find("IsGetLabel").GetComponent<UILabel>();
        UISprite spriteFrame = ItemObj.transform.Find("ItemKuangSprite").GetComponent<UISprite>();
        UISprite spriteIcon = ItemObj.transform.Find("Icon").GetComponent<UISprite>();
        GameObject suiPian = ItemObj.transform.Find("suiPian").gameObject;
        ItemObj.transform.Find("Number").GetComponent<UILabel>().text = _smallGoal.ItemNum.ToString();
        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(_smallGoal.ItemID);
        spriteFrame.spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
        TextTranslator.instance.ItemDescription(ItemObj, _smallGoal.ItemID, _smallGoal.ItemNum);
        //设置关数
        itemGuanLabel.text = string.Format("第{0}关", _smallGoal.Quest - 10000);
        int goalId = PlayerPrefs.GetInt("SmallGoalGetID", 0);
        //Debug.LogError("SmallGoalGetID::" + goalId);
        //设置是否获取
        //Debug.LogError("SmallGoalGetID:" + CharacterRecorder.instance.lastGateID);
        if (CharacterRecorder.instance.lastGateID > _smallGoal.Quest)
        {
            if (_smallGoal.SmallGoalID > goalId)
            {
                itemGetLabel.text = "[00eb00]可获得";
            }
            else
                //已经领取
                itemGetLabel.text = "[00eb00]已获得";

        }
        else
        {
            //未领取
            itemGetLabel.text = "[ff3d23]通关获得";
        }
        int _itemId = _smallGoal.ItemID;
        TextTranslator.instance.ItemDescription(spriteFrame.gameObject, _itemId, 0);
        if (_itemId.ToString()[0] == '4')
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = _itemId.ToString();
            suiPian.SetActive(false);
        }
        else if (_itemId.ToString()[0] == '6')
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = _itemId.ToString();
            suiPian.SetActive(false);
        }
        else if (_itemId == 70000 || _itemId == 79999)
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = _itemId.ToString();
            suiPian.SetActive(true);
        }
        else if (_itemId.ToString()[0] == '7' && _itemId > 70000)
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = (_itemId - 10000).ToString();
            suiPian.SetActive(true);
        }
        else if (_itemId.ToString()[0] == '8')
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_itemId);
            //spriteIcon.spriteName = (ItemCode - 30000).ToString();
            spriteIcon.spriteName = mItemInfo.picID.ToString();
            suiPian.SetActive(true);
        }
        else if (_itemId.ToString()[0] == '2')
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_itemId);
            spriteIcon.spriteName = mItemInfo.picID.ToString();
            suiPian.SetActive(false);
        }
        else
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = _itemId.ToString();
            suiPian.SetActive(false);
        }
    }
}
