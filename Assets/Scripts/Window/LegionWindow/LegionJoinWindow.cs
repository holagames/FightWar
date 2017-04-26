using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionJoinWindow : MonoBehaviour 
{
    public UIInput input;
    public GameObject FindButton;
    public GameObject CreatLegionButton;
    public GameObject uiGrid;
    public GameObject ItemPrafeb;

    private Vector3 scrollViewInitPos = new Vector3(0, -17.2f, 0);
    private Vector2 scrollViewInitOffect = new Vector2(0, 0);
	// Use this for initialization
	void Start () 
    {
        //NetworkHandler.instance.SendProcess("8010#;");

        if (UIEventListener.Get(FindButton).onClick == null)
        {
            UIEventListener.Get(FindButton).onClick += delegate(GameObject go)
            {
                Debug.Log("军团名字:  " + input.value);
                //UIManager.instance.OpenPromptWindow("即将开放", PromptWindow.PromptType.Alert, null, null);
                if (input.value != "")
                {
                    NetworkHandler.instance.SendProcess(string.Format("8011#{0};", input.value));
                }
            };
        }

        if (UIEventListener.Get(CreatLegionButton).onClick == null)
        {
            UIEventListener.Get(CreatLegionButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
                UIManager.instance.OpenPanel("LegionCreatWindow", false);
            };
        }

       /* if (UIEventListener.Get(GameObject.Find("LegionJoinCloseButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("LegionJoinCloseButton")).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }*/

      /*  for (int i = 0; i < 20; i++)
        {
            GameObject go = NGUITools.AddChild(uiGrid, ItemPrafeb);
            uiGrid.GetComponent<UIGrid>().Reposition();
            go.GetComponent<LegionItem>().SetInfo(i, "德玛西亚哦", i + 10, "卡特琳娜", i, i + 13);
        }*/
	}
    public void SetLegionJoinWindow(LegionDepth _LegionDepth,List<LegionItemData> _mlList)
    {
        ClearUIGride();
        uiGrid.transform.parent.GetComponent<UIPanel>().clipOffset = scrollViewInitOffect;
        uiGrid.transform.parent.localPosition = scrollViewInitPos;   
        for (int i = 0; i < _mlList.Count; i++)
        {
            GameObject go = NGUITools.AddChild(uiGrid, ItemPrafeb);
            go.GetComponent<LegionItem>().SetLegionItemInfo(_LegionDepth, _mlList[i]);
        }
        uiGrid.GetComponent<UIGrid>().Reposition();
    }
    public void SetLegionJoinWindow(List<LegionItemData> _mlList)
    {
        ClearUIGride();
        uiGrid.transform.parent.GetComponent<UIPanel>().clipOffset = scrollViewInitOffect;
        uiGrid.transform.parent.localPosition = scrollViewInitPos;        
        for (int i = 0; i < _mlList.Count; i++)
        {
            GameObject go = NGUITools.AddChild(uiGrid, ItemPrafeb);
            go.GetComponent<LegionItem>().SetLegionItemInfo(LegionDepth.LegionMain, _mlList[i]);
        }
        uiGrid.GetComponent<UIGrid>().Reposition();
    }
    void ClearUIGride()
    {
        for (int i = uiGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGrid.transform.GetChild(i).gameObject);
        }
    }
}
