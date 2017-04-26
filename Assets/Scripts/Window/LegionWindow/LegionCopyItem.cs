using UnityEngine;
using System.Collections;

public class LegionCopyItem: MonoBehaviour 
{
    public static LegionGate curClickLegionGate;
    public GameObject effectObj;
    public GameObject lockObj;
    public UITexture fubenTexture;
    public UILabel fubenNumber;
    public UILabel fubenName;
    private LegionGate myLegionGate;
	// Use this for initialization
	void Start () 
    {
        if (UIEventListener.Get(this.gameObject).onClick == null)
        {
            UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
            {
              //  UIManager.instance.OpenPromptWindow("即将开放", PromptWindow.PromptType.Alert, null, null);
                if (myLegionGate.GateGroupID <= LegionCopyWindow.curGateGroupID)
                {
                    UIManager.instance.OpenPanel("LegionGachaEnterWindow", true);
                    curClickLegionGate = myLegionGate;
                    //NetworkHandler.instance.SendProcess(string.Format("8202#{0};", myLegionGate.GateGroupID));
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("长官，请先通过上一关", PromptWindow.PromptType.Alert, null, null);
                }
            };
        }
	}
    public void SetLegionCopyItem(LegionGate _LegionCopyItemData)
    {
        myLegionGate = _LegionCopyItemData;
        fubenTexture.mainTexture = Resources.Load(string.Format("LegionCopy/fuben_tu{0}", _LegionCopyItemData.GateGroupID % 5 == 0 ? 5:_LegionCopyItemData.GateGroupID % 5), typeof(Texture)) as Texture;
        fubenNumber.text = _LegionCopyItemData.GateGroupID.ToString();
        TextTranslator.Gate _gate = TextTranslator.instance.GetGateByID(_LegionCopyItemData.NextGateID);
        fubenName.text = _gate.name;
        if (myLegionGate.GateGroupID <= LegionCopyWindow.curGateGroupID)
        {
            fubenNumber.transform.parent.GetComponent<UISprite>().spriteName = "fuben_di2";
            lockObj.SetActive(false);
            if (myLegionGate.GateGroupID == LegionCopyWindow.curGateGroupID)
            {
                effectObj.SetActive(true);
            }
            else
            {
                effectObj.SetActive(false);
            }
        }
        else
        {
            fubenNumber.transform.parent.GetComponent<UISprite>().spriteName = "fuben_di3";
            lockObj.SetActive(true);
            effectObj.SetActive(false);
        }
    }
	
}
/*public class LegionCopyItemData
{
    public int fubenNumber { get; set; }
    public string fubenName { get; set; }
    public LegionCopyItemData(int fubenNumber, string fubenName)
    {
        this.fubenNumber = fubenNumber;
        this.fubenName = fubenName;
    }
}*/
