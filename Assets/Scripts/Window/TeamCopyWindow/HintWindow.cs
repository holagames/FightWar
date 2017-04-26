using UnityEngine;
using System.Collections;

public class HintWindow : MonoBehaviour {

    public GameObject CloseButton;
    public GameObject CancelButton;
    public GameObject SureButton;
    public UILabel PromptLabel;
    private int TeamID;
    private int MyPosition;
	void Start () {
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {               
                this.gameObject.SetActive(false);
            };
        }
        if (UIEventListener.Get(CancelButton).onClick == null)
        {
            UIEventListener.Get(CancelButton).onClick += delegate(GameObject go)
            {               
                this.gameObject.SetActive(false);
            };
        }
        if (UIEventListener.Get(SureButton).onClick == null)
        {
            UIEventListener.Get(SureButton).onClick += delegate(GameObject go)
            {
                //this.gameObject.SetActive(false);
                NetworkHandler.instance.SendProcess("6105#" + TeamID + ";"+MyPosition+";");
                this.gameObject.SetActive(false);
            };
        }
	}

    public void SetHintWindow(int _teamid,int _myposition) 
    {
        this.TeamID = _teamid;
        this.MyPosition = _myposition;
        if (MyPosition == 1)
        {
            PromptLabel.text = "确定解散队伍？";
        }
        else 
        {
            PromptLabel.text = "确定离开队伍？";
        }
    }
}
