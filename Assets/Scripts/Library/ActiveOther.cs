using UnityEngine;
using System.Collections;

public class ActiveOther : MonoBehaviour {

    public GameObject OtherObject;
    public float ActiveTimer = 0;
    float timer = 0;
    bool isActive = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (!isActive)
        {
            if (timer > ActiveTimer)
            {
                OtherObject.SetActive(true);
                isActive = true;
            }
            else
            {
                timer += Time.deltaTime;
            }
        }
	}
}
