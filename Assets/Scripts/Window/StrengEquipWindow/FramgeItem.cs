using UnityEngine;
using System.Collections;

public class FramgeItem : MonoBehaviour {

    public UILabel FrameName;
    public UILabel RoleName;
    public UISprite FrameGrade;
    public UISprite SpriteIcon;
    public UISprite FramgeSuiPian;
    public GameObject FramgeWindow;
    public int FrameID = 0;
	// Use this for initialization
	void Start () {
        UIEventListener.Get(gameObject).onClick += delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("3306#" + FrameID+";");
            FramgeWindow.SetActive(false);
        };
        UIEventListener.Get(FrameGrade.gameObject).onClick += delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("3306#" + FrameID + ";");
            FramgeWindow.SetActive(false);
        };
        
	}
    public void SetInfo(int ID,string ItemName,string HeroName,int Grade)
    {
        FrameID = ID+30000;
        TextTranslator.instance.ItemDescription(FrameGrade.gameObject, FrameID, 0);
        RoleName.text = HeroName;
        FrameGrade.spriteName = "Grade" + Grade.ToString();
        SpriteIcon.spriteName = ID.ToString();
        FrameName.text = ItemName;
        FramgeSuiPian.gameObject.SetActive(true);
    }
    
}
