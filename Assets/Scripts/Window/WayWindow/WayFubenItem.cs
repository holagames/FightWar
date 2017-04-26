using UnityEngine;
using System.Collections;

public class WayFubenItem : MonoBehaviour {

    public UISprite FuBenIcon;
    public UILabel Name;
    public GameObject Lock;

    int curFubenID=0;
	// Use this for initialization
	void Start () {

        if (UIEventListener.Get(GameObject.Find("WayCloseButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("WayCloseButton")).onClick += delegate(GameObject go)
            {
                Destroy(this.gameObject);
            };
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetInfo(int fubenID)
    {
        curFubenID=fubenID;
        TextTranslator.Gate _gate = TextTranslator.instance.GetGateByID(fubenID);
        Name.text = _gate.name;
        Lock.SetActive(false);
        switch (int.Parse(fubenID.ToString().Substring(2, 1)))
        {
            case 1:
                FuBenIcon.spriteName = "1";
                break;
            case 2:
                FuBenIcon.spriteName = "2";
                break;
            case 3:
                FuBenIcon.spriteName = "3";
                break;
            case 4:
                FuBenIcon.spriteName = "4";
                break;
            default:
                break;
        }

        UIEventListener.Get(this.gameObject).onClick = delegate(GameObject go)
        {
            UIManager.instance.OpenPanel("GateWindow", true);
            if (GameObject.Find("GateWindow") != null)
            {
                Debug.LogError(fubenID.ToString().Substring(2, 1) + "    *****     " + fubenID);
                GateWindow GW = GameObject.Find("GateWindow").GetComponent<GateWindow>();
                GW.IsJump = true;
                GW.SetJumpWindowToGate(int.Parse(fubenID.ToString().Substring(2, 1)),fubenID);
            }
        };

    }

    public void SetLock(string data)
    {
        string[] dataSplit = data.Split(';');
        string[] secSplit = dataSplit[int.Parse(curFubenID.ToString().Substring(0, 1)) - 1].Split('$');
        if (secSplit[int.Parse(curFubenID.ToString().Substring(4, 1)) - 1] == "3")
        {
            Lock.SetActive(false);
        }
        else
        {
            Lock.SetActive(true);
        }
    }
}
