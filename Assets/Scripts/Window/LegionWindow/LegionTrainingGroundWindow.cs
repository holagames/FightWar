using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionTrainingGroundWindow : MonoBehaviour
{
    public GameObject CloseButton;
    public GameObject DoubtButton;
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject DoubtObj;
    public GameObject DoubtObjCloseButton;

    private UICenterOnChild myUICenterOnChild;
    public GameObject uiGride;
    public GameObject trainingGroundItem;

    public static List<Hero> mOnLineTrainingHeroList = new List<Hero>();
    private List<GameObject> itemObjList = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.军团训练场);
        UIManager.instance.UpdateSystems(UIManager.Systems.军团训练场);

        NetworkHandler.instance.SendProcess(string.Format("8018#{0};", ""));
        StartCoroutine(AutoUpdateLegionData());
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
                Debug.Log("leftButton。。。");
                myUICenterOnChild.enabled = true;
                int curIndex;
                if (myUICenterOnChild.centeredObject == null)
                {
                    curIndex = 2;
                }
                else
                {
                    curIndex = int.Parse(myUICenterOnChild.centeredObject.name);
                }
                if (curIndex > 0 + 2)
                {
                    curIndex -= 1;
                    myUICenterOnChild.CenterOn(uiGride.transform.GetChild(curIndex));
                }
                //  myUICenterOnChild.enabled = false;
            };
        }
        if (UIEventListener.Get(rightButton).onClick == null)
        {
            UIEventListener.Get(rightButton).onClick += delegate(GameObject go)
            {
                Debug.Log("rightButton。。。");
                myUICenterOnChild.enabled = true;
                int curIndex;
                if (myUICenterOnChild.centeredObject == null)
                {
                    curIndex = 1;
                }
                else
                {
                    curIndex = int.Parse(myUICenterOnChild.centeredObject.name);
                }
                if (curIndex < itemObjList.Count - 1 - 2)
                {
                    curIndex += 1;
                    myUICenterOnChild.CenterOn(uiGride.transform.GetChild(curIndex));
                }
                Debug.Log("curIndex......" + curIndex);
                //  myUICenterOnChild.enabled = false;
            };
        }
        if (UIEventListener.Get(DoubtObjCloseButton).onClick == null)
        {
            UIEventListener.Get(DoubtObjCloseButton).onClick += delegate(GameObject go)
            {
                DoubtObj.SetActive(false);
            };
        }
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }
        //InitUIData();
    }

    public void SetLegionTrainingGroundWindow(List<LegionTrain> mList)
    {
        CleraUIGride();
        /*for (int i = 0; i < mList.Count; i++)
        {
            GameObject obj = NGUITools.AddChild(uiGride, trainingGroundItem);
            obj.name = i.ToString();
            obj.GetComponent<TrainingGroundItem>().SetTrainingGroundItemInfo(mList[i]);
            itemObjList.Add(obj);
        }*/
        int objNameIndex = 0;
        mList = mListSort(mList);
        for (int i = 0; i < mList.Count; i++)
        {

            GameObject obj = NGUITools.AddChild(uiGride, trainingGroundItem);
            obj.name = objNameIndex.ToString();
            objNameIndex += 1;
            obj.GetComponent<TrainingGroundItem>().SetTrainingGroundItemInfo(mList[i]);
            itemObjList.Add(obj);

        }
        //for (int i = 0; i < mList.Count; i++)
        //{
        //    if (mList[i].VipUnLock < 99 && CharacterRecorder.instance.Vip >= mList[i].VipUnLock)
        //    {
        //        continue;
        //    }
        //    GameObject obj = NGUITools.AddChild(uiGride, trainingGroundItem);
        //    obj.name = objNameIndex.ToString();
        //    objNameIndex += 1;
        //    obj.GetComponent<TrainingGroundItem>().SetTrainingGroundItemInfo(mList[i]);
        //    itemObjList.Add(obj);
        //}
        uiGride.GetComponent<UIGrid>().repositionNow = true;
        // Invoke("DelayUICenterOnChild",1.0f);
        myUICenterOnChild = uiGride.GetComponent<UICenterOnChild>();
    }

    /// <summary>
    /// 排序
    /// </summary>
    /// <param name="mList"></param>
    /// <returns></returns>
    private List<LegionTrain> mListSort(List<LegionTrain> mList)
    {
        List<LegionTrain> mListOne = new List<LegionTrain>();
        mListOne.Clear();
        for (int i = 0; i < mList.Count; i++)
        {
            for (int j = mList.Count - 1; j > i; j--)
            {
                if (mList[j].TrainID < mList[j - 1].TrainID)
                {
                    LegionTrain temp = mList[j];
                    mList[j] = mList[j - 1];
                    mList[j - 1] = temp;
                }
            }
        }

        for (int i = 0; i < mList.Count; i++)
        {
            if (mList[i].VipUnLock == 99 && CharacterRecorder.instance.level >= mList[i].NeedLevel)//需要等级开放
            {
                mListOne.Add(mList[i]);
            }
        }
        for (int i = 0; i < mList.Count; i++)
        {
            if (CharacterRecorder.instance.Vip >= mList[i].VipUnLock)//需要VIP等级开放
            {
                mListOne.Add(mList[i]);
            }
        }
        for (int i = 0; i < mList.Count; i++)
        {
            if (!mListOne.Contains(mList[i]))//未开放的
            {
                mListOne.Add(mList[i]);
            }
        }
        return mListOne;
    }
    void DelayUICenterOnChild()
    {
        myUICenterOnChild = uiGride.GetComponent<UICenterOnChild>();
        myUICenterOnChild.enabled = true;
        int curIndex = 2;
        myUICenterOnChild.CenterOn(uiGride.transform.GetChild(curIndex));
    }
    //上阵
    public void ResetLegionTrainingGroundWindow(int trainingGroundItemID, int characterId, int exp)
    {
        for (int i = 0; i < itemObjList.Count; i++)
        {
            if (itemObjList[i].GetComponent<TrainingGroundItem>().mTrainingGroundItemData.TrainID == trainingGroundItemID)
            {

                ResetOnLineTrainingHeroDataList(characterId, itemObjList[i].GetComponent<TrainingGroundItem>().mTrainingGroundItemData.mHero);
                LegionTrain _TrainingGroundItemData = TextTranslator.instance.GetLegionTrainByID(trainingGroundItemID);
                _TrainingGroundItemData.SetLegionTrainSeverData(2, characterId, exp);

                itemObjList[i].GetComponent<TrainingGroundItem>().ResetTrainingGroundItemInfo(_TrainingGroundItemData);
            }
        }
    }

    //开启
    public void ResetLegionTrainingGroundWindow(int trainingGroundItemID)
    {
        for (int i = 0; i < itemObjList.Count; i++)
        {
            if (itemObjList[i].GetComponent<TrainingGroundItem>().mTrainingGroundItemData.TrainID == trainingGroundItemID)
            {
                LegionTrain _TrainingGroundItemData = TextTranslator.instance.GetLegionTrainByID(trainingGroundItemID);
                _TrainingGroundItemData.SetLegionTrainSeverData(1, 0, 0);
                itemObjList[i].GetComponent<TrainingGroundItem>().ResetTrainingGroundItemInfo(_TrainingGroundItemData);
            }
        }
    }
    IEnumerator AutoUpdateLegionData()
    {
        yield return new WaitForSeconds(60.0f);
        NetworkHandler.instance.SendProcess(string.Format("8018#{0};", ""));
        StartCoroutine(AutoUpdateLegionData());
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
    void ConfirmExitLegion()
    {
        NetworkHandler.instance.SendProcess("8009#;");
    }
    void ClickLegionTrainingItem(GameObject go)
    {

    }
}
