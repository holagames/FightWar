using UnityEngine;
using System.Collections;

public class WeaponWindow : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (UIEventListener.Get(GameObject.Find("WeaponCloseButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("WeaponCloseButton")).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }

        if (UIEventListener.Get(GameObject.Find("Mask")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("Mask")).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
