using UnityEngine;
using System.Collections;

public class ResetTechWindow : MonoBehaviour {
    public GameObject CancelButton;
    public GameObject DetermineButton;
    public GameObject CloseButton;
    private GameObject ResetTechObj;
    public UILabel ReturnCost;
    public UILabel CostDiamond;

	// Use this for initialization
	void Start () {
        if (GameObject.Find("ResetTechWindow") != null)
        {
            ResetTechObj = GameObject.Find("ResetTechWindow").gameObject;
        }
        if (UIEventListener.Get(DetermineButton).onClick == null)
        {
            UIEventListener.Get(DetermineButton).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("1603#");
                Debug.Log("确定后发送情报点和50%金币");
                CharacterRecorder.instance.ChangeAttribute = true;
                ResetTechObj.SetActive(false);
            };
        }
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                ResetTechObj.SetActive(false);
            };
        }
        if (UIEventListener.Get(CancelButton).onClick == null)
        {
            UIEventListener.Get(CancelButton).onClick += delegate(GameObject go)
            {
                ResetTechObj.SetActive(false);
            };
        }
        LabelShow();
	}
    public void LabelShow()
    {
        ReturnCost.text = "重置后将返还"+GameObject.Find("TechWindow").GetComponent<TechWindow>().CostPonitLabel.text+"情报点以及所消耗的金币50%";
        CostDiamond.text = "500";
    }
	// Update is called once per frame
	void Update () {
	
	}
}
