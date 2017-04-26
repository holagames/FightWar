using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TechTreeList : MonoBehaviour
{


    public List<GameObject> ElemList = new List<GameObject>();
    public List<GameObject> InterList = new List<GameObject>();
    public List<GameObject> TruList = new List<GameObject>();
    private GameObject nextItem;
    public UIAtlas TechTreeAtlas;
    // Update is called once per frame

    void Start()
    {
        for (int i = 0; i < ElemList.Count; i++)
        {
            ElemList[i].GetComponent<TreeItem>().PointLevel = 0;
            ElemList[i].GetComponent<TreeItem>().FinishPointShow(0);
            if (i != 0)
            {
                ElemList[i].GetComponent<TreeItem>().isLock = true;
                ElemList[i].transform.Find("item").gameObject.SetActive(false);
                ElemList[i].GetComponent<TreeItem>().FinishPoint.SetActive(false);
                ElemList[i].transform.Find("lock").gameObject.SetActive(true);
                ElemList[i].transform.Find("lock").GetComponent<UISprite>().spriteName = "H" + ElemList[i].name;
                ElemList[i].transform.Find("lock").GetComponent<UISprite>().atlas = TechTreeAtlas;
            }
        }
        for (int i = 0; i < InterList.Count; i++)
        {
            InterList[i].GetComponent<TreeItem>().PointLevel = 0;
            InterList[i].GetComponent<TreeItem>().FinishPointShow(0);
            InterList[i].GetComponent<TreeItem>().isLock = true;
            InterList[i].transform.Find("lock").gameObject.SetActive(true);
            InterList[i].transform.Find("lock").GetComponent<UISprite>().spriteName = "H" + InterList[i].name;
            InterList[i].transform.Find("lock").GetComponent<UISprite>().atlas = TechTreeAtlas;
            InterList[i].transform.Find("item").gameObject.SetActive(false);
            InterList[i].GetComponent<TreeItem>().FinishPoint.SetActive(false);

        }

        for (int i = 0; i < TruList.Count; i++)
        {
            TruList[i].GetComponent<TreeItem>().PointLevel = 0;
            TruList[i].GetComponent<TreeItem>().FinishPointShow(0);
            TruList[i].transform.Find("lock").gameObject.SetActive(true);
            TruList[i].transform.Find("lock").GetComponent<UISprite>().spriteName = "H" + TruList[i].name;
            TruList[i].transform.Find("lock").GetComponent<UISprite>().atlas = TechTreeAtlas;
            TruList[i].transform.Find("item").gameObject.SetActive(false);
            TruList[i].GetComponent<TreeItem>().isLock = true;
            TruList[i].GetComponent<TreeItem>().FinishPoint.SetActive(false);


        }

    }
    public void ListItemUnLock(TechTree ListItem)
    {

        int Fnum = ListItem.Icon % 100;
        if (ListItem.Icon < 200)
        {
            for (int i = Fnum; i < 4; i++)
            {
                int icon = int.Parse(ElemList[i + 1].name);
                TechTree OpenItem = TextTranslator.instance.GetTechTreerByIcon(icon, 1);
                if (ListItem.TechTreeID >= OpenItem.UnLockPreID && CharacterRecorder.instance.level>=OpenItem.UnLockNeedLevel)
                {
                    int num = OpenItem.Icon % 100;
                    nextItem = ElemList[num].gameObject;
                    nextItem.transform.Find("lock").gameObject.SetActive(false);
                    nextItem.GetComponent<TreeItem>().isLock = false;
                    nextItem.transform.Find("item").gameObject.SetActive(true);
                    nextItem.GetComponent<TreeItem>().FinishPoint.SetActive(true);
                }
            }
        }
        else if (ListItem.Icon >= 300)
        {
            if (ListItem.Icon == 300)
            {
                for (int i = Fnum; i < 3; i++)
                {
                    int icon = int.Parse(TruList[i + 1].name);
                    TechTree OpenItem = TextTranslator.instance.GetTechTreerByIcon(icon, 1);
                    if (ListItem.TechTreeID >= OpenItem.UnLockPreID && CharacterRecorder.instance.level >= OpenItem.UnLockNeedLevel)
                    {
                        int num = OpenItem.Icon % 100;
                        nextItem = TruList[num].gameObject;
                        nextItem.transform.Find("lock").gameObject.SetActive(false);
                        nextItem.GetComponent<TreeItem>().isLock = false;
                        nextItem.transform.Find("item").gameObject.SetActive(true);
                        nextItem.GetComponent<TreeItem>().FinishPoint.SetActive(true);
                    }
                }
            }
            else if (0 < Fnum && Fnum <= 6)
            {
                for (int i = Fnum; i < 7; i++)
                {
                    int icon = int.Parse(TruList[i + 3].name);
                    TechTree OpenItem = TextTranslator.instance.GetTechTreerByIcon(icon, 1);
                    if (ListItem.TechTreeID >= OpenItem.UnLockPreID && CharacterRecorder.instance.level >= OpenItem.UnLockNeedLevel)
                    {
                        int num = OpenItem.Icon % 100;
                        nextItem = TruList[num].gameObject;
                        nextItem.transform.Find("lock").gameObject.SetActive(false);
                        nextItem.GetComponent<TreeItem>().isLock = false;
                        nextItem.transform.Find("item").gameObject.SetActive(true);
                        nextItem.GetComponent<TreeItem>().FinishPoint.SetActive(true);
                    }
                }
            }
        }
        else if (200 <= ListItem.Icon && ListItem.Icon < 300)
        {
            if (ListItem.Icon == 200)
            {
                for (int i = Fnum; i < 3; i++)
                {
                    int icon = int.Parse(InterList[i + 1].name);
                    TechTree OpenItem = TextTranslator.instance.GetTechTreerByIcon(icon, 1);
                    if (ListItem.TechTreeID >= OpenItem.UnLockPreID && CharacterRecorder.instance.level >= OpenItem.UnLockNeedLevel)
                    {
                        int num = OpenItem.Icon % 100;
                        nextItem = InterList[num].gameObject;
                        nextItem.transform.Find("lock").gameObject.SetActive(false);
                        nextItem.GetComponent<TreeItem>().isLock = false;
                        nextItem.transform.Find("item").gameObject.SetActive(true);
                        nextItem.GetComponent<TreeItem>().FinishPoint.SetActive(true);
                    }
                }
            }
            else if(0<Fnum&&Fnum<=6)
            {
                for (int i = Fnum; i < 7; i++)
                {
                    int icon = int.Parse(InterList[i + 3].name);
                    TechTree OpenItem = TextTranslator.instance.GetTechTreerByIcon(icon, 1);
                    if (ListItem.TechTreeID >= OpenItem.UnLockPreID && CharacterRecorder.instance.level >= OpenItem.UnLockNeedLevel)
                    {
                        int num = OpenItem.Icon % 100;
                        nextItem = InterList[num].gameObject;
                        nextItem.transform.Find("lock").gameObject.SetActive(false);
                        nextItem.GetComponent<TreeItem>().isLock = false;
                        nextItem.transform.Find("item").gameObject.SetActive(true);
                        nextItem.GetComponent<TreeItem>().FinishPoint.SetActive(true);
                    }
                }
            }
        }
        else
        {
            GameObject.Find("TechWindow").GetComponent<TechWindow>().UnClockButton();
        }

    }
    public void ShowItem(TechTree TechTreeItem, int remainder)
    {

        int itemId = TechTreeItem.Icon;
        if (itemId < 200)
        {
            ElemList[itemId % 100].GetComponent<TreeItem>().PointLevel = remainder;
            ElemList[itemId % 100].GetComponent<TreeItem>().FinishPointShow(remainder);
            if (remainder >= 7)
            {
                ListItemUnLock(TechTreeItem);
            }
        }
        else if (itemId >= 300)
        {

            TruList[itemId % 100].GetComponent<TreeItem>().PointLevel = remainder;
            TruList[itemId % 100].GetComponent<TreeItem>().FinishPointShow(remainder);
            if (remainder >= 7)
            {
                ListItemUnLock(TechTreeItem);
            }

        }
        else
        {
            InterList[itemId % 100].GetComponent<TreeItem>().PointLevel = remainder;
            InterList[itemId % 100].GetComponent<TreeItem>().FinishPointShow(remainder);
            if (remainder >= 7)
            {
                ListItemUnLock(TechTreeItem);
            }

        }
    }

    //重置所有
    public void ResetListItem()
    {
        for (int i = 0; i < ElemList.Count; i++)
        {
            ElemList[i].GetComponent<TreeItem>().PointLevel = 0;
            ElemList[i].GetComponent<TreeItem>().FinishPointShow(0);
            if (i != 0)
            {
                ElemList[i].GetComponent<TreeItem>().isLock = true;
                ElemList[i].transform.Find("item").gameObject.SetActive(false);
                ElemList[i].GetComponent<TreeItem>().FinishPoint.SetActive(false);
                ElemList[i].transform.Find("lock").gameObject.SetActive(true);
                ElemList[i].transform.Find("lock").GetComponent<UISprite>().spriteName = "H" + ElemList[i].name;
                ElemList[i].transform.Find("lock").GetComponent<UISprite>().atlas = TechTreeAtlas;
            }
        }
        if (CharacterRecorder.instance.level >= 28 && GameObject.Find("TechWindow").GetComponent<TechWindow>().Depth > 1)
        {
            GameObject.Find("TechWindow").GetComponent<TechWindow>().IsIntermediate = false;
            GameObject.Find("TechWindow").GetComponent<TechWindow>().IntermediateButton.transform.Find("Clock").gameObject.SetActive(true);
            for (int i = 0; i < InterList.Count; i++)
            {
                InterList[i].GetComponent<TreeItem>().PointLevel = 0;
                InterList[i].GetComponent<TreeItem>().FinishPointShow(0);
                InterList[i].GetComponent<TreeItem>().isLock = true;
                InterList[i].transform.Find("lock").gameObject.SetActive(true);
                InterList[i].transform.Find("lock").GetComponent<UISprite>().spriteName = "H" + InterList[i].name;
                InterList[i].transform.Find("lock").GetComponent<UISprite>().atlas = TechTreeAtlas;
                InterList[i].transform.Find("item").gameObject.SetActive(false);
                InterList[i].GetComponent<TreeItem>().FinishPoint.SetActive(false);

            }
        }
        if (CharacterRecorder.instance.level >= 45 && GameObject.Find("TechWindow").GetComponent<TechWindow>().Depth > 2)
        {
            GameObject.Find("TechWindow").GetComponent<TechWindow>().IsTruing = false;
            GameObject.Find("TechWindow").GetComponent<TechWindow>().TruingButton.transform.Find("Clock").gameObject.SetActive(true);
            for (int i = 0; i < TruList.Count; i++)
            {
                TruList[i].GetComponent<TreeItem>().PointLevel = 0;
                TruList[i].GetComponent<TreeItem>().FinishPointShow(0);
                TruList[i].transform.Find("lock").gameObject.SetActive(true);
                TruList[i].transform.Find("lock").GetComponent<UISprite>().spriteName = "H" + TruList[i].name;
                TruList[i].transform.Find("lock").GetComponent<UISprite>().atlas = TechTreeAtlas;
                TruList[i].transform.Find("item").gameObject.SetActive(false);
                TruList[i].GetComponent<TreeItem>().isLock = true;
                TruList[i].GetComponent<TreeItem>().FinishPoint.SetActive(false);


            }
        }
    }
}


