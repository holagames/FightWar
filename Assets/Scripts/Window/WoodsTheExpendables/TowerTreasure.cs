using UnityEngine;
using System.Collections;

public class TowerTreasure : MonoBehaviour {
    int CurFloor = 0;
    public UILabel Label;
    public GameObject MyTreasure ;
    public GameObject MyEffect;
	// Use this for initialization
	void Start () {
        
        if (UIEventListener.Get(GameObject.Find("treasureWindow").transform.Find("Button").gameObject).onClick == null) {
            UIEventListener.Get(GameObject.Find("treasureWindow").transform.Find("Button").gameObject).onClick += delegate(GameObject obj)
            {
                NetworkHandler.instance.SendProcess("1509#;");                
            };
        }
        if (UIEventListener.Get(GameObject.Find("treasureWindow").transform.Find("OpenButton").gameObject).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("treasureWindow").transform.Find("OpenButton").gameObject).onClick += delegate(GameObject obj)
            {
                AudioEditer.instance.PlayOneShot("ui_openbox");
                StartCoroutine(ShowTreasure());
            };
        }
        //if (UIEventListener.Get(GameObject.Find("treasureWindow").transform.Find("EscButton").gameObject).onClick == null)
        //{
        //    UIEventListener.Get(GameObject.Find("treasureWindow").transform.Find("EscButton").gameObject).onClick += delegate(GameObject obj)
        //    {
        //        GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().UpdateWindow(WoodsTheExpendablesWindowType.Right);
        //    };
        //}
	}
	

    IEnumerator ShowTreasure()
    {
        yield return new WaitForSeconds(0.1f);
        GameObject childBlood = NGUITools.AddChild(MyTreasure, MyEffect);        
        MyTreasure.GetComponent<UITexture>().mainTexture = Resources.Load("Game/icon5") as Texture;
        yield return new WaitForSeconds(1f);
        CharacterRecorder.instance.OpenTreasureNum = 1;
        NetworkHandler.instance.SendProcess("1508#" + 1 + ";");
        yield return 0;
    }
    public void SetInfo() {
       
        Label.text = TextTranslator.instance.GetTowerBoxCostByID(1).Diamond.ToString();
        
    }


    public void NotClickButtonCloseWindow(string ButtonName) 
    {
        if (ButtonName == "Button") 
        {
            NetworkHandler.instance.SendProcess("1509#;"); 
        }
        else if (ButtonName == "OpenButton") 
        {
            AudioEditer.instance.PlayOneShot("ui_openbox");
            StartCoroutine(ShowTreasure());
        }
    }
}
