using UnityEngine;
using System.Collections;

public class ActivityWindow : MonoBehaviour
{

    public GameObject RightArrow;
    public GameObject LeftArrow;
    public UICenterOnChild UIOC;
    public GameObject[] ActivityItemList;

    int index = 1;
    // Use this for initialization
    void Start()
    {

        if (UIEventListener.Get(GameObject.Find("ActivityCloseButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("ActivityCloseButton")).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }

        if (UIEventListener.Get(GameObject.Find("StartButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("StartButton")).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPromptWindow("即将开放", PromptWindow.PromptType.Alert, null, null);
            };
        }

        UIEventListener.Get(RightArrow).onClick = delegate(GameObject go)
        {            
            index -= 1;
            if (index < 0)
            {
                index = 0;
            }
            Invoke("CenterOn", 0.1f);
        };

        UIEventListener.Get(LeftArrow).onClick = delegate(GameObject go)
        {
            index += 1;
            if (index > 2)
            {
                index = 2;
            }
            Invoke("CenterOn", 0.1f);
        };

    }

    void CenterOn()
    {
        UIOC.CenterOn(ActivityItemList[index].transform);
    }

}
