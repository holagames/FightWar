using UnityEngine;
using System.Collections;

public class TreeItem : MonoBehaviour
{

    public GameObject Item;
    public GameObject RewardWindow;
    public GameObject FinishPoint;
    public bool isLock = false;
    public int PointLevel;
    private TechTree TechTreeDic;
    public GameObject NextOpen;
    public GameObject NextClose;
    public GameObject NextUpOpen;
    public GameObject NextUpClose;
    public GameObject NextDownOpen;
    public GameObject NextDownCLose;
    private string color;

    void Start()
    {
        Item.transform.Find("item").GetComponent<UISprite>().spriteName = Item.name;
        FinishPointShow(PointLevel);
        if (UIEventListener.Get(Item).onClick == null)
        {
            UIEventListener.Get(Item).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.GuideID[11] == 6)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
                RewardWindow.SetActive(true);
                RewardWindow.GetComponent<RewardWindow>().GetTreeInfo(isLock, int.Parse(gameObject.name), PointLevel,0);
            };
        }
        ResetItemShow();
    }
    public void ResetItemShow()
    {
        TechTreeDic = TextTranslator.instance.GetTechTreerByIcon(int.Parse(Item.name), PointLevel);
        if (int.Parse(Item.name) >= 300)
        {
            if (GameObject.Find("TechWindow").GetComponent<TechWindow>().IsTruing)
            {
                TruingItemUnLock(TechTreeDic);
            }
            else
            {
                Item.transform.Find("lock").gameObject.SetActive(true);
                isLock = true;
                Item.transform.Find("item").gameObject.SetActive(false);
                FinishPoint.SetActive(false);
            }

        }
        else if (int.Parse(Item.name) < 200)
        {
            if (GameObject.Find("TechWindow").GetComponent<TechWindow>().IsElementary)
            {
                ElementaryItemUnLock(TechTreeDic);
            }
            else
            {
                Item.transform.Find("lock").gameObject.SetActive(true);
                isLock = true;
                Item.transform.Find("item").gameObject.SetActive(false);
                FinishPoint.SetActive(false);
            }
        }
        else
        {
            if (GameObject.Find("TechWindow").GetComponent<TechWindow>().IsIntermediate)
            {
                IntermediateItemUnLock(TechTreeDic);
            }
            else
            {
                Item.transform.Find("lock").gameObject.SetActive(true);
                isLock = true;
                Item.transform.Find("item").gameObject.SetActive(false);
                FinishPoint.SetActive(false);
            }

        }





    }
    public void FinishPointShow(int pointLevel)
    {
        if (PointLevel == 10)
        {
            FinishPoint.transform.Find("PointsgBG").GetComponent<UISprite>().spriteName = "kuang2";
            color = "[FE7968]";
        }
        else
        {
            FinishPoint.transform.Find("PointsgBG").GetComponent<UISprite>().spriteName = "kuang1";
            color = "[00FD86]";
        }
        FinishPoint.GetComponent<UILabel>().text = "[-]" + color + PointLevel.ToString() + "/10[-]";
        if (pointLevel >= 7)
        {
            if (Item.name == "200" || Item.name == "300")
            {
                NextOpen.SetActive(true);
                NextClose.SetActive(false);
                NextUpOpen.SetActive(true);
                NextUpClose.SetActive(false);
                NextDownOpen.SetActive(true);
                NextDownCLose.SetActive(false);
            }
            else if (Item.name == "104" || Item.name == "207" || Item.name == "208" || Item.name == "209" || Item.name == "307" || Item.name == "308" || Item.name == "309")
            {
                return;
            }
            else
            {
                NextOpen.SetActive(true);
                NextClose.SetActive(false);
            }
        }
        else
        {
            if (Item.name == "200" || Item.name == "300")
            {
                NextOpen.SetActive(false);
                NextClose.SetActive(true);
                NextUpOpen.SetActive(false);
                NextUpClose.SetActive(true);
                NextDownOpen.SetActive(false);
                NextDownCLose.SetActive(true);
            }
            else if (Item.name == "104" || Item.name == "207" || Item.name == "208" || Item.name == "209" || Item.name == "307" || Item.name == "308" || Item.name == "309")
            {
                return;
            }
            else
            {
                NextOpen.SetActive(false);
                NextClose.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    public void ElementaryItemUnLock(TechTree TechTreeDic)
    {
        if ((Item.name == "100"))
        {
            Item.transform.Find("lock").gameObject.SetActive(false);
            isLock = false;
            Item.transform.Find("item").gameObject.SetActive(true);
            FinishPoint.SetActive(true);
        }
        else
        {
            Item.transform.Find("lock").gameObject.SetActive(true);
            isLock = true;
            Item.transform.Find("item").gameObject.SetActive(false);
            FinishPoint.SetActive(false);
        }
    }
    public void IntermediateItemUnLock(TechTree TechTreeDic)
    {

        if ((Item.name == "200"))
        {
            Item.transform.Find("lock").gameObject.SetActive(false);
            isLock = false;
            Item.transform.Find("item").gameObject.SetActive(true);
            FinishPoint.SetActive(true);
        }
        else
        {
            Item.transform.Find("lock").gameObject.SetActive(true);
            isLock = true;
            Item.transform.Find("item").gameObject.SetActive(false);
            FinishPoint.SetActive(false);
        }
    }
    public void TruingItemUnLock(TechTree TechTreeDic)
    {
        if ((Item.name == "300"))
        {
            Item.transform.Find("lock").gameObject.SetActive(false);
            isLock = false;
            Item.transform.Find("item").gameObject.SetActive(true);
            FinishPoint.SetActive(true);
        }
        else
        {
            Item.transform.Find("lock").gameObject.SetActive(true);
            isLock = true;
            Item.transform.Find("item").gameObject.SetActive(false);
            FinishPoint.SetActive(false);
        }
    }
}
