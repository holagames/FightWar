using UnityEngine;
using System.Collections;

public class LegionLogItem : MonoBehaviour 
{
    [SerializeField]
    private UILabel myLogLabel;
	// Use this for initialization
	void Start () 
    {
	
	}
    public void SetLegionLogItem(LegionLogItemData _LegionLogItemData)
    {
        myLogLabel.color = Color.white;
        string nameColor = "[ff8c04]";
        string desColor = "[249bd2]";
        switch (_LegionLogItemData.logType)
        {
            case 1: myLogLabel.text = string.Format("{0}{1} {2}加入军团。", nameColor, _LegionLogItemData.name, desColor); break;
            case 2: myLogLabel.text = string.Format("{0}{1} {2}离开军团。", nameColor, _LegionLogItemData.name, desColor); break;
            case 3: myLogLabel.text = string.Format("{0}{1} {2}进行了超级捐献,使军团经验增加200。", nameColor, _LegionLogItemData.name, desColor); break;
            case 4: myLogLabel.text = string.Format("{0}{1} {2}被任命为军团长。", nameColor, _LegionLogItemData.name, desColor); break;
            case 5: myLogLabel.text = string.Format("{0}{1} {2}被任命为副团长。", nameColor, _LegionLogItemData.name, desColor); break;
            case 6: myLogLabel.text = string.Format("{0}{1} {2}被任命为精英。", nameColor, _LegionLogItemData.name, desColor); break;
        }
    }
    
}

public class LegionLogItemData
{
    public string name { get; set; }
    public int logType { get; set; }
    public LegionLogItemData(string name, int logType)
    {
        this.name = name;
        this.logType = logType;
    }
}
