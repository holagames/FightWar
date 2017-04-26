using UnityEngine;
using System.Collections;

public class EnterPasswordWindow : MonoBehaviour {

    public GameObject CloseButton;
    public GameObject CancelButton;
    public GameObject SureButton;
    public UIInput uiinput;

    private string Value;
    private int TeamId;
    private string Password;

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
                Value = uiinput.value;
                if (Password == Value)
                {
                    NetworkHandler.instance.SendProcess("6103#" + TeamId + ";");
                }
                else
                {
                    Debug.LogError("密码错误");
                    UIManager.instance.OpenPromptWindow("密码错误", PromptWindow.PromptType.Hint, null, null);
                }
                this.gameObject.SetActive(false);
            };
        }
	}

    public void SetEnterPasswordWindow(int teamId,string password) 
    {
        this.TeamId = teamId;
        this.Password = password;
    }
}
