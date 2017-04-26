using UnityEngine;
using System.Collections;

public class EventWindow : MonoBehaviour {

    public GameObject SevendayWindow;
    public GameObject PayButton;
    public GameObject DiscountButton;
    public GameObject DrawButoton;
    public GameObject GoldButoton;
    public GameObject NoActivity;
	// Use this for initialization
	void Start () {        
        NetworkHandler.instance.SendProcess("9133#;");
        UIEventListener.Get(DiscountButton).onClick += delegate(GameObject go)
        {
            Debug.Log("七日");
            NetworkHandler.instance.SendProcess("9133#;");
            SevendayWindow.SetActive(true);
        };
        UIEventListener.Get(PayButton).onClick += delegate(GameObject go)
        {
            Debug.Log("消费");
        };
        UIEventListener.Get(DrawButoton).onClick += delegate(GameObject go)
        {
            Debug.Log("招募");
        };
        UIEventListener.Get(GoldButoton).onClick += delegate(GameObject go)
        {
            Debug.Log("金币");
        };
	}


}
