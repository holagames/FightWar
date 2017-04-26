using UnityEngine;
using System.Collections;

public class NewOpenWindow : MonoBehaviour {

	// Use this for initialization
	void Start () {

        if (UIEventListener.Get(GameObject.Find("ComeformButtom")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("ComeformButtom")).onClick = delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }

        if (UIEventListener.Get(GameObject.Find("PromptCloseButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("PromptCloseButton")).onClick = delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }

	}
	

}
