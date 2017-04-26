using UnityEngine;
using System.Collections;

public class TechWindow : MonoBehaviour
{

    public GameObject TechWindowObj;
    public GameObject ElementaryButton;
    public GameObject IntermediateButton;
    public GameObject TruingButton;
    public GameObject ResetButton;
    public GameObject ElementaryShowObj;
    public GameObject IntermediateShowObj;
    public GameObject TruingShowObj;
    public GameObject ResetTechWindow;
    public GameObject QuestionButton;
    public GameObject QuestionWindow;
    public GameObject Item200;
    public GameObject Item300;
    public bool IsElementary = false;
    public bool IsIntermediate = false;
    public bool IsTruing = false;

    public UILabel HavePonitLabel;
    public UILabel CostPonitLabel;
    public int TreeTechID = 0;
    public int Depth=1;
    // Use this for initialization
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.情报);
        UIManager.instance.UpdateSystems(UIManager.Systems.情报);

        ElementaryShowObj.SetActive(true);
        NetworkHandler.instance.SendProcess("1601#");
        EventListener();
        UnClockButton();
    }
    public void EventListener()
    {
        if (UIEventListener.Get(ElementaryButton).onClick == null)
        {
            UIEventListener.Get(ElementaryButton).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("1601#");
                ElementaryShowObj.SetActive(true);
                TruingShowObj.SetActive(false);
                IntermediateShowObj.SetActive(false);
            };
        }
        if (UIEventListener.Get(IntermediateButton).onClick == null)
        {
            UIEventListener.Get(IntermediateButton).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("1601#");
                IntermediateShowObj.SetActive(true);
                ElementaryShowObj.SetActive(false);
                TruingShowObj.SetActive(false);
            };
        }
        if (UIEventListener.Get(TruingButton).onClick == null)
        {
            UIEventListener.Get(TruingButton).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("1601#");
                TruingShowObj.SetActive(true);
                IntermediateShowObj.SetActive(false);
                ElementaryShowObj.SetActive(false);
            };
        }
        if (UIEventListener.Get(ResetButton).onClick == null)
        {
            UIEventListener.Get(ResetButton).onClick += delegate(GameObject go)
            {
                if (int.Parse(CostPonitLabel.text) > 0)
                {
                    ResetTechWindow.SetActive(true);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("未消耗情报点,不能重置",PromptWindow.PromptType.Hint,null ,null);
                }
            };
        }
        if (UIEventListener.Get(QuestionButton).onClick == null)
        {
            UIEventListener.Get(QuestionButton).onClick += delegate(GameObject go)
            {
                QuestionWindow.SetActive(true);
            };
        }
        if (QuestionWindow)
        {
            if (UIEventListener.Get(QuestionWindow.transform.Find("Question/CloseButton").gameObject).onClick == null)
            {
                UIEventListener.Get(QuestionWindow.transform.Find("Question/CloseButton").gameObject).onClick += delegate(GameObject go)
                {
                    QuestionWindow.SetActive(false);
                };
            }
        }
        
    }
    public void UnClockButton()
    {
        int Ponit = int.Parse(CostPonitLabel.text);
        if (CharacterRecorder.instance.level >= 45 && 
            GameObject.Find("TechTree").GetComponent<TechTreeList>().InterList[7].GetComponent<TreeItem>().PointLevel >= 7 &&
            GameObject.Find("TechTree").GetComponent<TechTreeList>().InterList[8].GetComponent<TreeItem>().PointLevel >= 7 &&
            GameObject.Find("TechTree").GetComponent<TechTreeList>().InterList[9].GetComponent<TreeItem>().PointLevel >= 7 && Depth == 3)
        {
           
            TruingButton.transform.Find("Clock").gameObject.SetActive(false);
            IntermediateButton.transform.Find("Clock").gameObject.SetActive(false);
            ElementaryButton.transform.Find("Clock").gameObject.SetActive(false);
            IsElementary = true;
            IsIntermediate = true;
            IsTruing = true;
            TruingShowObj.transform.Find("Label").gameObject.SetActive(false);
            Item300.GetComponent<TreeItem>().ResetItemShow();
        }
        else if (CharacterRecorder.instance.level >= 28 && GameObject.Find("TechTree").GetComponent<TechTreeList>().ElemList[4].GetComponent<TreeItem>().PointLevel >= 7 && Depth == 2)
        {
            IntermediateButton.transform.Find("Clock").gameObject.SetActive(false);
            ElementaryButton.transform.Find("Clock").gameObject.SetActive(false);
            IsElementary = true;
            IsIntermediate = true;
            IntermediateShowObj.transform.Find("Label").gameObject.SetActive(false);
            Item200.GetComponent<TreeItem>().ResetItemShow();
        }
        else if (CharacterRecorder.instance.lastGateID >= 10027&&Depth==1)
        {
            ElementaryButton.transform.Find("Clock").gameObject.SetActive(false);
            IsElementary = true;
        }

    }
}
