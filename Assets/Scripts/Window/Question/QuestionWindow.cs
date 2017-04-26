using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class QuestionWindow: MonoBehaviour 
{
    public GameObject closeButton;
    public GameObject sureButton;
    public GameObject AwardsObj;
    public List<GameObject> awardItemList = new List<GameObject>(); 
    public List<GameObject> OptionItemList = new List<GameObject>(); 
    public UILabel quesNumLabel;
    public UILabel quesDesLabel;
    private UILabel buttonLabel;
    private Question MyCurQuestion;
    private int MyState = 0;
    private int MyQuestionID;
	// Use this for initialization
	void Start () 
    {
        NetworkHandler.instance.SendProcess("9151#1;");
        buttonLabel = sureButton.transform.FindChild("Label").GetComponent<UILabel>();
        if (UIEventListener.Get(sureButton).onClick == null)
        {
            UIEventListener.Get(sureButton).onClick += delegate(GameObject go)
            {
                ClickSureButton();
            };
        }
        if (UIEventListener.Get(closeButton).onClick == null)
        {
            UIEventListener.Get(closeButton).onClick += delegate(GameObject go)
            {
                //UIManager.instance.BackUI();
                DestroyImmediate(this.gameObject);
            };
        }
	}

    public void SetQuestionWindow(int state, int questionID)
    {
        MyState = state;
        MyQuestionID = questionID;
        Question _myQuestion = TextTranslator.instance.GetQuestionByID(questionID);

        MyCurQuestion = _myQuestion;
        SetQuestionInfo(state, _myQuestion);
    }
    public void SetQuestionWindow(int questionID)
    {
        MyQuestionID = questionID;
        Question _myQuestion = TextTranslator.instance.GetQuestionByID(questionID);
        int state;
        if (_myQuestion != null)
        {
            state = 0;
        }
        else
        {
            state = 1;
        }
        MyState = state;
        MyCurQuestion = _myQuestion;
        SetQuestionInfo(state, _myQuestion);
    }
    void SetQuestionInfo(int state, Question _myQuestion)
    {

        if (_myQuestion == null)
        {
            for (int i = 0; i < OptionItemList.Count; i++)
            {
                OptionItemList[i].SetActive(false);
            }
            AwardsObj.SetActive(true);
            for (int i = 0; i < awardItemList.Count;i++ )
            {
                int itemCode = 0;
                switch(i)
                {
                    case 0: itemCode = 90002; break;
                    case 1: itemCode = 21056; break;
                }
                TextTranslator.instance.ItemDescription(awardItemList[i], itemCode, 0);
            }
            if(state == 0)
            {
                state = 1;
                MyState = state;
            }

            quesNumLabel.text = "";
            
            switch (state)
            {
                case 0: buttonLabel.text = "下一步"; break;
                case 1: buttonLabel.text = "领 取"; quesDesLabel.text = "恭喜获得以下答卷奖励,快快领取吧"; break;
                case 2: buttonLabel.text = "已领取"; buttonLabel.effectColor = Color.grey; sureButton.GetComponent<UIButton>().isEnabled = false; quesDesLabel.text = ""; break;
            }
            return;
        }
        
        AwardsObj.SetActive(false);
        switch (state)
        {
            case 0: buttonLabel.text = "下一步"; break;
            case 1: buttonLabel.text = "领 取"; break;
        }
        string NameStr = "";
        switch (_myQuestion.Option)
        {
            case 1: NameStr = "【单选】"; break;
            case 2:
            default: NameStr = "【多选】"; break;
        }
        quesNumLabel.text = string.Format("{0}.", _myQuestion.QuestionID);
        quesDesLabel.text = _myQuestion.Name + NameStr;
        for (int i = 0; i < OptionItemList.Count; i++)
        {
            UIToggle _UIToggle = OptionItemList[i].GetComponent<UIToggle>();
            _UIToggle.group = 0;
            _UIToggle.startsActive = false;
            _UIToggle.value = false;
            //Debug.LogError(_UIToggle.value);
        }
        for (int i = 0; i < OptionItemList.Count; i++)
        {
            UIToggle _UIToggle = OptionItemList[i].GetComponent<UIToggle>();
            _UIToggle.startsActive = false;
            _UIToggle.value = false;
            //Debug.Log(_UIToggle.value);
            if (i < _myQuestion.SelectionList.size && _myQuestion.SelectionList[i] != "")
            {
                OptionItemList[i].SetActive(true);
                string LabelNumStr = "";
                switch (i)
                {
                    case 0: LabelNumStr = "A."; break;
                    case 1: LabelNumStr = "B."; break;
                    case 2: LabelNumStr = "C."; break;
                    case 3: LabelNumStr = "D."; break;
                    case 4: LabelNumStr = "E."; break;
                    case 5: LabelNumStr = "F."; break;
                    case 6: LabelNumStr = "G."; break;
                    case 7: LabelNumStr = "H."; break;
                    case 8: LabelNumStr = "I."; break;
                }
                
                switch (_myQuestion.Option)
                {
                    case 1: _UIToggle.group = _myQuestion.SelectionList.size; break;
                    case 2:
                    default: _UIToggle.group = 0; break;
                }
                OptionItemList[i].transform.FindChild("LabelNum").GetComponent<UILabel>().text = LabelNumStr;
                OptionItemList[i].transform.FindChild("LabelName").GetComponent<UILabel>().text = _myQuestion.SelectionList[i];
            }
            else
            {
                OptionItemList[i].SetActive(false);
            }
        }
    }
    public void OpenGainWindow(List<Item> _itemList)
    {
        SetQuestionInfo(2,null);
        UIManager.instance.OpenSinglePanel("AdvanceWindow", false);
        GameObject.Find("AdvanceWindow").layer = 11;
        AdvanceWindow aw = GameObject.Find("AdvanceWindow").GetComponent<AdvanceWindow>();
        aw.SetInfo(AdvanceWindowType.GainResult, null, null, null, null, _itemList);
    }
    void ClickSureButton()
    {
        switch (MyState)
        {
            case 0:
                string answer = "";
                for(int i = 0; i < MyCurQuestion.SelectionList.size;i++)
                {
                    if(OptionItemList[i].GetComponent<UIToggle>().value)
                    {
                        switch(i)
                        {
                            case 0:answer += "A$";break;
                            case 1:answer += "B$";break;
                            case 2:answer += "C$";break;
                            case 3:answer += "D$";break;
                            case 4:answer += "E$";break;
                            case 5:answer += "F$";break;
                            case 6:answer += "G$";break;
                            case 7:answer += "H$";break;
                            case 8:answer += "I$";break;
                        }
                    }
                }
                answer = answer.Remove(answer.Length - 1);
                NetworkHandler.instance.SendProcess(string.Format("9152#{0};{1};{2};", 1, MyCurQuestion.QuestionID, answer));
                break;
            case 1: NetworkHandler.instance.SendProcess("9153#1;"); break;
            case 2: UIManager.instance.OpenPromptWindow("已经领过问卷奖励啦",PromptWindow.PromptType.Hint,null,null); break;
        }
    }
}
