using UnityEngine;
using System.Collections;

public class SuccessfuWindow : MonoBehaviour {

    public GameObject sureButton;
    public GameObject Close;
	// Use this for initialization
	void Start () {
            UIEventListener.Get(sureButton).onClick += delegate(GameObject obj)
            {
                //UIManager.instance.OpenPanel("ChallengeWindow", true);
                DestroyImmediate(this.gameObject);
            };
            UIEventListener.Get(Close).onClick += delegate(GameObject obj)
            {
                //UIManager.instance.OpenPanel("ChallengeWindow", true);
                DestroyImmediate(this.gameObject);
            };
	}
	
}
