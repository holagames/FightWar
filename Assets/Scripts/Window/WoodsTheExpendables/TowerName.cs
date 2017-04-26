using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerName : MonoBehaviour {
    public Camera mainCamera;
    public Camera uiCamera;
    private GameObject obj;
    private Vector3 m_position;
    float HalfHeight;
    float HalfWidth;
    private bool IsUpdate = false;
    private int TimerUpdate = 0;
    public List<GameObject> StarList = new List<GameObject>();
	// Use this for initialization
	void Start () {
        HalfHeight = Screen.height / 2;
        HalfWidth = Screen.width / 2;
        GameObject MainC = GameObject.Find("TowerCamera");
        GameObject UIC = GameObject.Find("Camera");
        mainCamera = MainC.GetComponent<Camera>();
        uiCamera = UIC.GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        UpdateNamePosition();
	}
    public void SetGameObj(GameObject obj, int Name)
    {
        CancelInvoke("UpdateTime");
        this.obj = obj;
        IsUpdate = true;
        this.transform.Find("Label").GetComponent<UILabel>().text = Name.ToString();
        
    }
    void UpdateNamePosition()
    {
        if (obj != null)
        {
            m_position = obj.transform.position;
            //  m_position = mainCamera.WorldToScreenPoint(m_position + mainCamera.transform.position);


            Vector3 pos = mainCamera.WorldToScreenPoint(m_position);

            pos = new Vector3((pos.x - HalfWidth) * 800f / Screen.height, (pos.y - HalfHeight) * 800f / Screen.height, 0);
            pos.x = Mathf.FloorToInt(pos.x);
            pos.y = Mathf.FloorToInt(pos.y + 270f);
            pos.z = 0f;
            this.gameObject.transform.localPosition = pos;

            // m_position = uiCamera.ScreenToWorldPoint(m_position);



            //  this.gameObject.transform.position = m_position;
            //Vector3 pt = Camera.main.WorldToScreenPoint(m_position);
            //Vector3 ff = UICamera.currentCamera.ScreenToWorldPoint(pt);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
