using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpdateMapName : MonoBehaviour
{

    public Camera mainCamera;
    public Camera uiCamera;
    private GameObject obj;
    private Vector3 m_position;
    float HalfHeight;
    float HalfWidth;
    private bool IsUpdate = false;
    private int TimerUpdate = 0;
    public List<GameObject> StarList = new List<GameObject>();
    float MoveY = 15f;
    float MoveX = 0f;
    public UIAtlas CommontAtlas;
    public int worldEventId;
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
    public void SetGameObj(GameObject obj, string Name, int StarNumber)
    {
        CancelInvoke("UpdateTime");
        this.obj = obj;
        IsUpdate = true;
        //if (this.transform.Find("Label") != null)
        //{
        //    this.transform.Find("Label").gameObject.SetActive(false);
        //}
        this.transform.Find("Label").GetComponent<UILabel>().text = Name;
        //ShowStar(StarNumber);
    }
    public void SetGameObj(GameObject obj, string Name,string StarNum)
    {
        CancelInvoke("UpdateTime");
        this.obj = obj;
        IsUpdate = true;
        //if (this.transform.Find("Label") != null)
        //{
        //    this.transform.Find("Label").gameObject.SetActive(false);
        //}
        MoveY = 0f;
        if(Name.Substring(0,2) == "JY")
        {
            this.transform.Find("Label").GetComponent<UILabel>().color = new Color(255 / 255f, 79 / 255f, 255 / 255f);
            //this.transform.Find("Label").GetComponent<UILabel>().effectStyle = UILabel.Effect.None;
            this.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(0 / 255f, 0 / 255f, 0 / 255f); ; 
            Name = Name.Substring(2);
            this.transform.GetChild(4).GetComponent<UISprite>().spriteName = "JY_star5";
        }
        this.transform.Find("Label").GetComponent<UILabel>().text = Name;
        this.transform.Find("Bg").gameObject.SetActive(false);
        this.transform.Find("Label").localPosition = new Vector3(0, -40);
        this.transform.GetChild(4).gameObject.SetActive(true);
        this.transform.GetChild(4).localPosition = new Vector3(-44, -38);
        this.transform.GetChild(4).GetComponent<UISprite>().depth = 1;
        if(StarNum == "1")
        {
            UISprite sprite = NGUITools.AddSprite(this.gameObject, CommontAtlas, "message");
            sprite.MakePixelPerfect();
            sprite.depth = 0;
            sprite.transform.localPosition = new Vector3(5, 13);
        }
        //ShowStar(StarNumber);
    }
    public void SetGameObj(GameObject obj)
    {
        CancelInvoke("UpdateTime");
        this.obj = obj;
        IsUpdate = true;
        //if (this.transform.Find("Label") != null)
        //{
        //    this.transform.Find("Label").gameObject.SetActive(false);
        //}
        MoveY = 13f;
        MoveX = 5f;
        UISprite sprite = NGUITools.AddSprite(this.gameObject, CommontAtlas, "message");
        sprite.MakePixelPerfect();
        sprite.depth = 0;
        this.transform.Find("Bg").gameObject.SetActive(false);
        this.transform.Find("Label").gameObject.SetActive(false);
        //ShowStar(StarNumber);
    }
    public void SetWorldEvenIconPos(GameObject obj,int IconName)
    {
        CancelInvoke("UpdateTime");

        this.obj = obj;
        IsUpdate = true;
        MoveX = 0;
        MoveY = 80;
        this.gameObject.transform.GetChild(0).GetChild(0).GetComponent<UISprite>().spriteName = IconName.ToString();
    }
    void UpdateNamePosition()
    {
        m_position = obj.transform.position;
        //  m_position = mainCamera.WorldToScreenPoint(m_position + mainCamera.transform.position);


        Vector3 pos = mainCamera.WorldToScreenPoint(m_position);

        pos = new Vector3((pos.x - HalfWidth) * 800f / Screen.height *UIRootExtend.instance.isUiRootRatio, (pos.y - HalfHeight) * 800f / Screen.height *UIRootExtend.instance.isUiRootRatio, 0);
        pos.x = Mathf.FloorToInt(pos.x + MoveX);
        pos.y = Mathf.FloorToInt(pos.y + MoveY);
        pos.z = 0f;
        this.gameObject.transform.localPosition = pos;
        // m_position = uiCamera.ScreenToWorldPoint(m_position);



        //  this.gameObject.transform.position = m_position;
        //Vector3 pt = Camera.main.WorldToScreenPoint(m_position);
        //Vector3 ff = UICamera.currentCamera.ScreenToWorldPoint(pt);
    }
    private void ShowStar(int Number)
    {
        switch (Number)
        {
            case 0:
                StarList[0].GetComponent<UISprite>().spriteName = "star4";
                StarList[1].GetComponent<UISprite>().spriteName = "star4";
                StarList[2].GetComponent<UISprite>().spriteName = "star4";
                break;
            case 1:
                StarList[0].GetComponent<UISprite>().spriteName = "star3";
                StarList[1].GetComponent<UISprite>().spriteName = "star4";
                StarList[2].GetComponent<UISprite>().spriteName = "star4";
                break;
            case 2:
                StarList[0].GetComponent<UISprite>().spriteName = "star3";
                StarList[1].GetComponent<UISprite>().spriteName = "star3";
                StarList[2].GetComponent<UISprite>().spriteName = "star4";
                break;
            case 3:
                StarList[0].GetComponent<UISprite>().spriteName = "star3";
                StarList[1].GetComponent<UISprite>().spriteName = "star3";
                StarList[2].GetComponent<UISprite>().spriteName = "star3";
                break;
        }
    }
}
