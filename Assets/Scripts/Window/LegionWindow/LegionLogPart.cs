using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionLogPart: MonoBehaviour 
{
    public GameObject legionLogItem;
    public GameObject datePartObj;
    public UIGrid uiGride;
    public UILabel NoneMemberNeedCheckLabel;

    private List<LegionLogItemData> _LegionLogItemList;

    void OnEnable()
    {
        NetworkHandler.instance.SendProcess("8014#;");
    }
	// Use this for initialization
	void Start () 
    {
        
	}
    public void SetLegionLogPart(List<LegionLogItemData> _LegionLogItemList)
    {
        ClearUIGride();
        this._LegionLogItemList = _LegionLogItemList;
        if (_LegionLogItemList.Count == 0)
        {
            NoneMemberNeedCheckLabel.text = "[249bd2]目前没有任何日志哦 [3ee817]招募团员[-] ";
            NoneMemberNeedCheckLabel.gameObject.SetActive(true);
            datePartObj.SetActive(false);
        }
        else
        {
            NoneMemberNeedCheckLabel.gameObject.SetActive(false);
            datePartObj.SetActive(true);
        }
        for (int i = 0; i < _LegionLogItemList.Count; i++)
        {
            GameObject obj = NGUITools.AddChild(uiGride.gameObject, legionLogItem);
            obj.GetComponent<LegionLogItem>().SetLegionLogItem(_LegionLogItemList[i]);
        }
        uiGride.GetComponent<UIGrid>().Reposition();
    }
    void ClearUIGride()
    {
        for (int i = uiGride.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGride.transform.GetChild(i).gameObject);
        }
    }
	
}
