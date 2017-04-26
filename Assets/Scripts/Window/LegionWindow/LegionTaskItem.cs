using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionTaskItem: MonoBehaviour 
{
    public UIAtlas itemAtlas;
    public UIAtlas AvatarAtlas;
    public UISprite icon;
    public UILabel nameLabel;
    public UILabel countLabel;
    public GameObject goButton;
    public List<GameObject> starObjList = new List<GameObject>();
    public List<GameObject> awardObjList = new List<GameObject>();
    private LegionTask _MyLegionTaskData;
    private int ownCount;
    // Use this for initialization
    void Start()
    {
        UIEventListener.Get(goButton).onClick += delegate(GameObject go)
        {
            ClickGoButton();
        };
    }
    public void SetLegionTaskItem(LegionTask _HelperItemData)
    {
        _MyLegionTaskData = _HelperItemData;
        if (_HelperItemData.Param1.ToString()[0] == '6')
        {
            icon.atlas = AvatarAtlas;
        }
        else
        {
            icon.atlas = itemAtlas;
        }
        icon.spriteName = _HelperItemData.Param1.ToString();
        nameLabel.text = _HelperItemData.Name;
        ownCount = TextTranslator.instance.GetItemCountByID(_HelperItemData.Param1);
        if (ownCount >= _HelperItemData.ParamVal1)
        {
            countLabel.color = Color.green;
            goButton.transform.FindChild("Label").GetComponent<UILabel>().text = "完成";
        }
        else
        {
            countLabel.color = Color.white;
            goButton.transform.FindChild("Label").GetComponent<UILabel>().text = "前往";
        }
        countLabel.text = string.Format("{0}/{1}", ownCount, _HelperItemData.ParamVal1);
        for (int i = 0; i < starObjList.Count;i++ )
        {
            if (i < _HelperItemData.Color + 1)
            {
                starObjList[i].SetActive(true);
            }
            else
            {
                starObjList[i].SetActive(false);
            }
        }
        for (int i = 0; i < awardObjList.Count;i++ )
        {
            if (i < _HelperItemData.BoxAwardList.size + 1)
            {
                awardObjList[i].SetActive(true);
                switch(i)
                {
                    case 0: awardObjList[i].transform.FindChild("LabelAward").GetComponent<UILabel>().text = string.Format("×{0}", _HelperItemData.ExpBonus); break;
                    default:
                        if (_HelperItemData.BoxAwardList[i - 1].itemCode == 0)
                        {
                            awardObjList[i].SetActive(false);
                        }
                        else
                        {
                            if (_HelperItemData.BoxAwardList[i - 1].itemCode.ToString()[0] != '7' && _HelperItemData.BoxAwardList[i - 1].itemCode.ToString()[0] != '8')
                            {
                                awardObjList[i].transform.FindChild("SuiPian").gameObject.SetActive(false);
                            }
                            awardObjList[i].GetComponent<UISprite>().spriteName = _HelperItemData.BoxAwardList[i - 1].itemCode.ToString();
                            awardObjList[i].transform.FindChild("LabelAward").GetComponent<UILabel>().text = string.Format("×{0}", _HelperItemData.BoxAwardList[i - 1].itemCount); 
                        }
                        break;
                }
            }
            else
            {
                awardObjList[i].SetActive(false);
            }
        }

    }
    void ClickGoButton()
    {
        int needCount = _MyLegionTaskData.ParamVal1;
        int itemCode = _MyLegionTaskData.Param1;
        if (ownCount >= needCount)
        {
            NetworkHandler.instance.SendProcess(string.Format("8402#{0};", _MyLegionTaskData.LegionTaskID));
            return;
        }
        UIManager.instance.OpenSinglePanel("WayWindow", false);
        GameObject _WayWindow = GameObject.Find("WayWindow");
        WayWindow.NeedItemCount = _MyLegionTaskData.ParamVal1;
        
        _WayWindow.GetComponent<WayWindow>().SetWayInfo(itemCode);
        _WayWindow.layer = 11;

        CharacterRecorder.instance.SweptIconID = itemCode;
        CharacterRecorder.instance.SweptIconNum = 0;

        foreach (Component c in _WayWindow.GetComponentsInChildren(typeof(Transform), true))
        {
            c.gameObject.layer = 11;
        }
    }
	
}
