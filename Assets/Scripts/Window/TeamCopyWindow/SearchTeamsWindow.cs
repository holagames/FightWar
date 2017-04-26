using UnityEngine;
using System.Collections;

public class SearchTeamsWindow : MonoBehaviour {

    public GameObject CloseButton;
    public GameObject SearchButton;
    public UIInput Input;
    private int teamid;
	void Start () {
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                this.gameObject.SetActive(false);
            };
        }
        if (UIEventListener.Get(SearchButton).onClick == null)
        {
            UIEventListener.Get(SearchButton).onClick += delegate(GameObject go)
            {
                string TeamId = Input.value;
                if (TeamId != "")
                {
                    NetworkHandler.instance.SendProcess("6109#" + TeamId + ";");
                    this.gameObject.SetActive(false);
                }
                else 
                {
                    UIManager.instance.OpenPromptWindow("请输入房间号！", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
	}

    public void SetSearchTeamsWindow() 
    {
        
    }   
	

}
