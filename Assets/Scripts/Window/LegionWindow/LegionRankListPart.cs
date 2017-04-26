using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionRankListPart : MonoBehaviour
{
    public GameObject CloseButton;
    public GameObject legionItem;
    public UIGrid uiGride;

    private LegionDepth legionDepth = 0;
    private List<LegionItemData> legionItemList = new List<LegionItemData>();
    void Awake()
    {
        NetworkHandler.instance.SendProcess("8010#0;");
    }

    // Use this for initialization
    void Start()
    {
        
        if (CloseButton != null)
        {
            if (UIEventListener.Get(CloseButton).onClick == null)
            {
                UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
                {
                    UIManager.instance.BackUI();
                };
            }
        }
    }
    public void SetLegionRankListPart(LegionDepth _LegionDepth, List<LegionItemData> _LegionItemList)
    {
        ClearUIGride();
        for (int i = 0; i < _LegionItemList.Count; i++)
        {
            GameObject obj = NGUITools.AddChild(uiGride.gameObject, legionItem);
            obj.GetComponent<LegionItem>().SetLegionItemInfo(_LegionDepth, _LegionItemList[i]);
        }
        uiGride.GetComponent<UIGrid>().Reposition();
    }
    public void ScueedApplayingResult()
    {
        for (int i = uiGride.transform.childCount - 1; i >= 0; i--)
        {
            uiGride.transform.GetChild(i).GetComponent<LegionItem>().ScueedApplayingResult();
        }
    }
    void ClearUIGride()
    {
        if (uiGride.transform.childCount > 0)
        {
            for (int i = uiGride.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(uiGride.transform.GetChild(i).gameObject);
            }
        }
    }

}
