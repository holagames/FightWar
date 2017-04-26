using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GateChapterWindow : MonoBehaviour
{
    public GameObject MyGrid;
    public GameObject MyGateChapterItem;

    int Chapter = 1;
    // Use this for initialization
    List<GameObject> ListChapter = new List<GameObject>();
    void OnEnable()
    {
        int i = 0;
        Debug.Log("!!!" + TextTranslator.instance.listChapter.Count);
        foreach (var h in TextTranslator.instance.listChapter)
        {
            GameObject go = NGUITools.AddChild(MyGrid, MyGateChapterItem);
            go.name = "GateChapterItem" + h.chapterID;
            go.GetComponent<GateChapterItem>().Init(h.chapterID, h.level, h.name, i);
            //go.GetComponent<RoleHeroItem>().CharacterRoleID = h.characterRoleID;
            //go.GetComponent<RoleHeroItem>().Init(h.cardID, h.characterRoleID, i);
            go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("MapScrollView").GetComponent<UIScrollView>();

            ListChapter.Add(go);
            i++;
        }

        MyGrid.GetComponent<UIGrid>().Reposition();
    }

    void Start()
    {
        if (UIEventListener.Get(GameObject.Find("CloseButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("CloseButton")).onClick += delegate(GameObject go)
            {
                Debug.Log("AAA");
                UIManager.instance.BackUI();
            };
        }
    }
}
