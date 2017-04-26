using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatChanelChoseWindow: MonoBehaviour 
{
    public GameObject closeButton;
    public GameObject sureButton;
    public List<UIToggle> toggleList = new List<UIToggle>();
	// Use this for initialization
	void Start () 
    {
        if (UIEventListener.Get(closeButton).onClick == null)
        {
            UIEventListener.Get(closeButton).onClick += delegate(GameObject go)
            {
                DestroyImmediate(this.gameObject);
            };
        }
        if (UIEventListener.Get(sureButton).onClick == null)
        {
            UIEventListener.Get(sureButton).onClick += delegate(GameObject go)
            {
                for (int i = 0; i < toggleList.Count;i++ )
                {
                    if (toggleList[i].value == false)
                    {
                        PlayerPrefs.SetInt(string.Format("ChatChanel{0}", i + 1), 1);//默认 0  开启，1 关闭
                    }
                    else
                    {
                        PlayerPrefs.SetInt(string.Format("ChatChanel{0}", i + 1), 0);
                    }
                }
                DestroyImmediate(this.gameObject);
            };
        }
        SetToggleState();
	}
    void SetToggleState()
    {
        for (int i = 1; i < 5; i++)
        {
            int chanel = PlayerPrefs.GetInt(string.Format("ChatChanel{0}", i));
            if (chanel == 1)
            {
                toggleList[i - 1].startsActive = false;
            }
            else
            {
                toggleList[i - 1].startsActive = true;
            }
        }
    }
}
