using UnityEngine;
using System.Collections;

public class LegionWindow : MonoBehaviour {

    public GameObject CreatButton;
    public GameObject JoinButton;

	// Use this for initialization
	void Start () {

        if (UIEventListener.Get(CreatButton).onClick == null)
        {
            UIEventListener.Get(CreatButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPanel("LegionCreatWindow", false);
            };
        }

        if (UIEventListener.Get(JoinButton).onClick == null)
        {
            UIEventListener.Get(JoinButton).onClick += delegate(GameObject go)
            {
                //UIManager.instance.OpenPanel("LegionJoinWindow", false);
                UIManager.instance.OpenPanel("LegionSecondWindow", true);
            };
        }

        if (UIEventListener.Get(GameObject.Find("LegionCloseButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("LegionCloseButton")).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }

	}
	

}
