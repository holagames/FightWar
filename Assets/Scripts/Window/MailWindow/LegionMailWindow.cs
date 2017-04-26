using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionMailWindow: MonoBehaviour 
{
    public GameObject closeButton;
    public GameObject mailSendButton;
    public UILabel friendNameLabel;
    public UIInput input;
    public List<UIToggle> TypeList = new List<UIToggle>();
    public FriendItemData selectFriendData;
	// Use this for initialization
	void Start () 
    {
        if (UIEventListener.Get(closeButton).onClick == null)
        {
            UIEventListener.Get(closeButton).onClick += delegate(GameObject obj)
            {
                Destroy(this.gameObject);
            };
        }
        if (UIEventListener.Get(friendNameLabel.gameObject).onClick == null)
        {
            UIEventListener.Get(friendNameLabel.gameObject).onClick += delegate(GameObject obj)
            {
                if (TypeList[1].value)
                {
                    UIManager.instance.OpenPanel("LegionFriendWindow", false);
                }
            };
        }
        if (UIEventListener.Get(mailSendButton).onClick == null)
        {
            UIEventListener.Get(mailSendButton).onClick += delegate(GameObject go)
            {
                string text = input.value;
                if (!string.IsNullOrEmpty(text))
                {
                    if (TypeList[0].value)
                    {
                        NetworkHandler.instance.SendProcess(string.Format("9006#{0};{1};", CharacterRecorder.instance.legionID, text));
                        input.value = null;
                        Destroy(this.gameObject);
                    }
                    else if (selectFriendData != null)
                    {
                        NetworkHandler.instance.SendProcess(string.Format("9007#{0};{1};", selectFriendData.userId, text));
                        input.value = null;
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("请选择一个好友", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("没有邮件内容", PromptWindow.PromptType.Hint, null, null);
                }
                
            };
        }
	}
	
	
}
