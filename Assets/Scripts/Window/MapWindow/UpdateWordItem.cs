using UnityEngine;
using System.Collections;

public class UpdateWordItem : MonoBehaviour
{
    public Camera mainCamera;
    public Camera uiCamera;
    private GameObject obj;
    private Vector3 m_position;
    float HalfHeight;
    float HalfWidth;
    private bool IsUpdate = false;
    private int TimerUpdate = 0;
    // Use this for initialization
    void Start()
    {

        HalfHeight = Screen.height / 2;
        HalfWidth = Screen.width / 2;
        GameObject MainC = GameObject.Find("MapCamera");
        GameObject UIC = GameObject.Find("Camera");
        mainCamera = MainC.GetComponent<Camera>();
        uiCamera = UIC.GetComponent<Camera>();
       
       
    }

    // Update is called once per frame
    void Update()
    {
        if (IsUpdate)
        {
            UpdateNamePosition();
        }
    }
    public void SetGameObj(GameObject obj,int Timer)
    {
        CancelInvoke("UpdateTime");
        this.obj = obj;
        IsUpdate = true;
        TimerUpdate = Timer;
        InvokeRepeating("UpdateTime", 0, 1f);
    }
    void UpdateNamePosition()
    {
        m_position = obj.transform.position;
      //  m_position = mainCamera.WorldToScreenPoint(m_position + mainCamera.transform.position);

      
        Vector3 pos = mainCamera.WorldToScreenPoint(m_position);
        
        pos = new Vector3((pos.x - HalfWidth) * 800f / Screen.height, (pos.y - HalfHeight) * 800f / Screen.height, 0);
        pos.x = Mathf.FloorToInt(pos.x*UIRootExtend.instance.isUiRootRatio);
        pos.y = Mathf.FloorToInt(pos.y * UIRootExtend.instance.isUiRootRatio + 100f);
        pos.z = 0f;
        this.gameObject.transform.localPosition = pos;

        // m_position = uiCamera.ScreenToWorldPoint(m_position);



        //  this.gameObject.transform.position = m_position;
        //Vector3 pt = Camera.main.WorldToScreenPoint(m_position);
        //Vector3 ff = UICamera.currentCamera.ScreenToWorldPoint(pt);
    }
    void UpdateTime()
    {
        if (TimerUpdate > 0)
        {
            int fenNum = (int)((TimerUpdate / 60) % 60);
            int miao = (int)(TimerUpdate % 60);
            int day = (int)((TimerUpdate / 60) / 60);
            this.gameObject.transform.Find("Label").GetComponent<UILabel>().text = ((day < 10) ? "0" : "") + day.ToString() + ":" + ((fenNum < 10) ? "0" : "") + fenNum.ToString() + ":" + ((miao < 10) ? "0" : "") + miao.ToString();
            TimerUpdate -= 1;

        }
        else {
            CancelInvoke("UpdateTime");
            DestroyImmediate(this.gameObject);
            NetworkHandler.instance.SendProcess("1135#;");
        }

    }
}
