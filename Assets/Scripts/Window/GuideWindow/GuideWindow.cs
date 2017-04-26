using UnityEngine;
using System.Collections;

public class GuideWindow : MonoBehaviour {

    

	// Use this for initialization
	void Start () {
	
	}

    public void SetBoxCollider(Vector3 ButtonPosition,Vector3 BoxSize)
    {
        GameObject.Find("GuidePartTop").transform.localPosition=new Vector3(0, ButtonPosition.y + (BoxSize.y / 2));
        GameObject.Find("GuidePartBottom").transform.localPosition = new Vector3(0, ButtonPosition.y - (BoxSize.y / 2));
        GameObject.Find("GuidePartLeft").transform.localPosition = new Vector3(ButtonPosition.x - (BoxSize.x / 2), 0);
        GameObject.Find("GuidePartLeft").transform.localPosition = new Vector3(ButtonPosition.x + (BoxSize.x / 2), 0);
        
    }
}
