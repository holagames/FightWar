using UnityEngine;
using System.Collections;

public class GateChapterItem : MonoBehaviour {

    int ChapterID;
    int Level;
    int Index;

    public GameObject LabelName;
    public GameObject LabelLevel;
    public GameObject LabelStar;
    public GameObject LabelState;

	// Use this for initialization
	void Start () {
        if (UIEventListener.Get(gameObject).onClick == null)
        {
            UIEventListener.Get(gameObject).onClick += delegate(GameObject go)
            {
                Debug.Log("BBB");
                SceneTransformer.instance.NowChapterID = ChapterID;
                //GameObject.Find("RoleWindow").GetComponent<RoleWChapterIDindow>().SetHeroClick(Index);
                NetworkHandler.instance.SendProcess("2001#" + ChapterID + ";");
                if (GameObject.Find("GateWindow") != null)
                {
                    GameObject.Find("GateWindow").GetComponent<GateWindow>().SetChapter(ChapterID);
                }
                UIManager.instance.BackUI();
            };
        }
	}

    public void Init(int _ChapterID, int _Level, string ChapterName, int _Index)
    {
        ChapterID = _ChapterID;
        Level = _Level;
        Index = _Index;

        LabelName.GetComponent<UILabel>().text = "第" + ChapterID.ToString() + "章\n" + ChapterName;
        LabelState.GetComponent<UILabel>().text = "已开启";
        LabelLevel.GetComponent<UILabel>().text = Level.ToString() + "级开启";
    }
}
