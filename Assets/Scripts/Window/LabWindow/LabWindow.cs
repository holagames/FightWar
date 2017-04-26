using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LabWindow: MonoBehaviour 
{
    public List<GameObject> ListTabs = new List<GameObject>();
    public UILabel topAddPercentLabel;
    public UILabel topDesLabel;
    public UILabel bottomDesLabel;

    public GameObject DoubtButton;
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject DoubtObj;
    public GameObject DoubtObjCloseButton;

    private UICenterOnChild myUICenterOnChild;
    public GameObject uiGride;
    public GameObject trainingGroundItem;

    public static List<Hero> mOnLineTrainingHeroList = new List<Hero>();
    public List<GameObject> itemObjList = new List<GameObject>();
    private int tabNum = -1;
    private bool IsOnputOrRemove = false;//上下实验室   
  /*  void OnDestroy()
    {
        if(IsOnputOrRemove)
        {
          //  NetworkHandler.instance.SendProcess(string.Format("1005#{0};", "0"));
            NetworkHandler.instance.SendProcess("1005#0;");
        }
    }*/
	// Use this for initialization
	void Start () 
    {
        UIManager.instance.CountSystem(UIManager.Systems.实验室);
        UIManager.instance.UpdateSystems(UIManager.Systems.实验室);

        //StartCoroutine(AutoUpdateLegionData());
        DoubtObj.SetActive(false);
        if (UIEventListener.Get(DoubtButton).onClick == null)
        {
            UIEventListener.Get(DoubtButton).onClick += delegate(GameObject go)
            {
                DoubtObj.SetActive(true);
            };
        }
        if (UIEventListener.Get(leftButton).onClick == null)
        {
            UIEventListener.Get(leftButton).onClick += delegate(GameObject go)
            {
                myUICenterOnChild.enabled = true;
                int curIndex = int.Parse(myUICenterOnChild.centeredObject.name);
                if(curIndex > 0 + 1) //0 + 2
                {
                    curIndex -= 1;
                    myUICenterOnChild.CenterOn(uiGride.transform.GetChild(curIndex));
                }
                myUICenterOnChild.enabled = false;
            };
        }
        if (UIEventListener.Get(rightButton).onClick == null)
        {
            UIEventListener.Get(rightButton).onClick += delegate(GameObject go)
            {
                myUICenterOnChild.enabled = true;
                int curIndex = int.Parse(myUICenterOnChild.centeredObject.name);
                if (curIndex < itemObjList.Count - 1 - 1)//itemObjList.Count - 1 - 2
                {
                    curIndex += 1;
                    myUICenterOnChild.CenterOn(uiGride.transform.GetChild(curIndex));
                }
                myUICenterOnChild.enabled = false;
            };
        }
        if (UIEventListener.Get(DoubtObjCloseButton).onClick == null)
        {
            UIEventListener.Get(DoubtObjCloseButton).onClick += delegate(GameObject go)
            {
                DoubtObj.SetActive(false);
            };
        }
        InitOpenState();
        SetTab(1);
	}
    #region Tab处理
    void InitOpenState()
    {
        for (int i = 0; i < ListTabs.Count; i++)
        {
            UIEventListener.Get(ListTabs[i]).onClick = ClickListTabs;
            int roleType = int.Parse(ListTabs[i].name[9].ToString());

            ReformLabItemData _targetLabItemData = TextTranslator.instance.GetOneLabsItemByRoleTypeAndPosNum(roleType, 1);
            if (CharacterRecorder.instance.level < _targetLabItemData.NeedLevel)
            {
                ListTabs[i].GetComponent<UIToggle>().value = false;
                StartCoroutine(DelaySetUIToggleFalse(ListTabs[i].GetComponent<UIToggle>()));
                ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(true);
            }
            else
            {
                ListTabs[i].GetComponent<UIToggle>().value = true;
                ListTabs[i].transform.FindChild("Lock").gameObject.SetActive(false);
            }
        }
    }
    IEnumerator DelaySetUIToggleFalse(UIToggle _UIToggle)
    {
        yield return new WaitForSeconds(0.5f);
        _UIToggle.enabled = false;
    }
    private void ClickListTabs(GameObject go)
    {
        int roleType = int.Parse(go.name[9].ToString());
        if (this.tabNum == roleType)
        {
            return;
        }
        ReformLabItemData _targetLabItemData = TextTranslator.instance.GetOneLabsItemByRoleTypeAndPosNum(roleType, 1);
        if (CharacterRecorder.instance.level < _targetLabItemData.NeedLevel)
        {
            go.GetComponent<UIToggle>().value = false;
            UIManager.instance.OpenPromptWindow(string.Format("{0}级开放", _targetLabItemData.NeedLevel),PromptWindow.PromptType.Hint, null, null);
        }
        else
        {
            go.GetComponent<UIToggle>().value = true;
            SetTab(roleType);
        }
    }
    void SetTab(int tabNum)
    {
        this.tabNum = tabNum;
        TextTranslator.instance.roleType = tabNum;
        SendToSeverToGetList(tabNum);
    }
    void SendToSeverToGetList(int tabNum)
    {
        CharacterRecorder.instance.IsNeedAddEmpetyCount = false;
        switch (tabNum)
        {
            case 1: 
            case 2:
            case 3:
            case 4:
            case 5:
            case 6:
            case 7: NetworkHandler.instance.SendProcess(string.Format("1801#{0};", tabNum)); break;
        }
    }
    #endregion
    public void SetReformLabWindow(BetterList<ReformLabItemData> mList)
    {
        CleraUIGride();
        float sumAddPercent = 0;
        for (int i = 0; i < mList.size; i++)
        {
            if (mList[i].mHero != null && mList[i].state == 2)
            {
                sumAddPercent += mList[i].mHero.addPercent;
            }
            GameObject obj = NGUITools.AddChild(uiGride, trainingGroundItem);
            obj.name = i.ToString();
            obj.GetComponent<ReformLabItem>().SetReformLabItemInfo(mList[i]);
            itemObjList.Add(obj);
        }
      /*  int objNameIndex = 0;
        for (int i = 0; i < mList.size; i++)
        {
            if (mList[i].VipUnLock == 0 && CharacterRecorder.instance.Vip >= mList[i].VipUnLock)
            {
                GameObject obj = NGUITools.AddChild(uiGride, trainingGroundItem);
                obj.name = objNameIndex.ToString();
                objNameIndex += 1;
                obj.GetComponent<ReformLabItem>().SetReformLabItemInfo(mList[i]);
                itemObjList.Add(obj);
            }
        }
        for (int i = 0; i < mList.size; i++)
        {
            if (mList[i].VipUnLock == 0 && CharacterRecorder.instance.Vip >= mList[i].VipUnLock)
            {
                continue;
            }
            GameObject obj = NGUITools.AddChild(uiGride, trainingGroundItem);
            obj.name = objNameIndex.ToString();
            objNameIndex += 1;
            obj.GetComponent<ReformLabItem>().SetReformLabItemInfo(mList[i]);
            itemObjList.Add(obj);
        }*/
        uiGride.GetComponent<UIGrid>().repositionNow = true;
        Invoke("DelayUICenterOnChild",1.0f);
        topAddPercentLabel.text = string.Format("+{0}%", Math.Round(sumAddPercent, 1));//sumAddPercent);
        string typeStr = "攻击";
        switch (tabNum)
        {
            case 1: typeStr = "攻击"; break;
            case 2: typeStr = "防御"; break;
            case 3: typeStr = "特殊"; break;
            case 4: typeStr = "近战"; break;
            case 5: typeStr = "远程"; break;
            case 6: typeStr = "男击"; break;
            case 7: typeStr = "女击"; break;
        }
        topDesLabel.text = string.Format("{0}型英雄攻击、防御、生命加成", typeStr);
        bottomDesLabel.text = string.Format("[919090]1.英雄等级达到30级   2.英雄类型为[ff0000]{0}类  [-]3.英雄品质升至[009cff]蓝色[-]以上", typeStr);
    }
    void DelayUICenterOnChild()
    {
        myUICenterOnChild = uiGride.GetComponent<UICenterOnChild>();
        myUICenterOnChild.enabled = true;
        int curIndex = 1;//2
        myUICenterOnChild.CenterOn(uiGride.transform.GetChild(curIndex));
    }
    //上阵
    public void ResetLegionTrainingGroundWindow(int roleType, int LabItemPosNum, int characterId)
    {
        CharacterRecorder.instance.IsOnputOrRemove = true;
        CharacterRecorder.instance.IsNeedAddEmpetyCount = false;
        NetworkHandler.instance.SendProcess(string.Format("1801#{0};", tabNum)); 
        for (int i = 0; i < itemObjList.Count; i++)
        {
            if (itemObjList[i].GetComponent<ReformLabItem>().mItemData.LabItemPosNum == LabItemPosNum)
            {

                ResetOnLineTrainingHeroDataList(characterId, itemObjList[i].GetComponent<ReformLabItem>().mItemData.mHero);
                ReformLabItemData _LabItemData = TextTranslator.instance.GetOneLabsItemByRoleTypeAndPosNum(roleType, LabItemPosNum);
                _LabItemData.state = 2;
                _LabItemData.SetReformLabItemData(characterId, 0, 0);

                itemObjList[i].GetComponent<ReformLabItem>().ResetLabItemInfo(_LabItemData);
            }
        }
    }
    //下阵
    public void RemoveLabHeroWindow(int roleType, int LabItemPosNum, int fight)
    {
        CharacterRecorder.instance.IsOnputOrRemove = true;
        for (int i = 0; i < itemObjList.Count; i++)
        {
            if (itemObjList[i].GetComponent<ReformLabItem>().mItemData.LabItemPosNum == LabItemPosNum)
            {
                Hero oldHeroOnLine = itemObjList[i].GetComponent<ReformLabItem>().mItemData.mHero;
                mOnLineTrainingHeroList.Remove(oldHeroOnLine);
                //Debug.LogError("移除后count..." + mOnLineTrainingHeroList.Count);
                ReformLabItemData _LabItemData = TextTranslator.instance.GetOneLabsItemByRoleTypeAndPosNum(roleType, LabItemPosNum);
                _LabItemData.mHero = null;
                _LabItemData.state = 1;
                oldHeroOnLine = null;
                _LabItemData.SetReformLabItemData(0, 0, 0);
                itemObjList[i].GetComponent<ReformLabItem>().ResetLabItemInfo(_LabItemData);
            }
        }
    }

    //开启
    public void ResetLabWindow(int roleType, int LabItemPosNum)
    {
        if (this.tabNum != roleType)
        {
            return;
        }
        for (int i = 0; i < itemObjList.Count; i++)
        {
            if (itemObjList[i].GetComponent<ReformLabItem>().mItemData.LabItemPosNum == LabItemPosNum)
            {
                ReformLabItemData _LabItemData = TextTranslator.instance.GetOneLabsItemByRoleTypeAndPosNum(roleType,LabItemPosNum);
                _LabItemData.state = 1;
                itemObjList[i].GetComponent<ReformLabItem>().ResetLabItemInfo(_LabItemData);
            }
        }
    }

    void ResetOnLineTrainingHeroDataList(int characterId, Hero oldHeroOnLine)
    {
        if (oldHeroOnLine != null && mOnLineTrainingHeroList.Contains(oldHeroOnLine))
        {
            mOnLineTrainingHeroList.Remove(oldHeroOnLine);
        }
        foreach (var h in CharacterRecorder.instance.ownedHeroList)
        {
            if (h.characterRoleID == characterId)
            {
                mOnLineTrainingHeroList.Add(h);
                break;
            }
        }
    }
    void CleraUIGride()
    {
        itemObjList.Clear();
        for (int i = uiGride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGride.transform.GetChild(i).gameObject);
        }
    }

}
