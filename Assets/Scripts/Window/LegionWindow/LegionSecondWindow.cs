using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionSecondWindow: MonoBehaviour
{
    public static bool enterFromLegionCopy = false;
    [HideInInspector]
    public static string curSelectTabName = "Tab1";
    public List<GameObject> tabsList = new List<GameObject>();
    public List<GameObject> partsList = new List<GameObject>();
    
    void OnEnable()
    {
        
    }
    // Use this for initialization
    void Start()
    {
        NetworkHandler.instance.SendProcess("8010#0;");
        if (!CharacterRecorder.instance.isLegionChairman && CharacterRecorder.instance.myLegionPosition < 2)
        {
            Destroy(tabsList[2].gameObject);
            Invoke("DalayTabGridReposition", 0.1f);
        }
        else
        {
            SetRedPointOfCheckTab();
        }
        for (int i = 0; i < tabsList.Count; i++)
        {
            UIEventListener.Get(tabsList[i]).onClick = ClickTabsButton;
        }
        if (!enterFromLegionCopy)
        {
            SetButtonType(tabsList[0]);
        }
        else
        {
            SetButtonType(tabsList[1]);
        }
    }
    void DalayTabGridReposition()
    {
        UIGrid _tabGride = tabsList[0].transform.parent.GetComponent<UIGrid>();
        _tabGride.enabled = true;
        _tabGride.repositionNow = true;
    }
    public void ShowCheckPartOrNot()
    {
        if (!CharacterRecorder.instance.isLegionChairman && CharacterRecorder.instance.myLegionPosition < 2)
        {
            Destroy(tabsList[2].gameObject);
            Invoke("DalayTabGridReposition", 0.1f);
        }
    }
    public void SetLegionSecondWindow(List<ShopItemData> _ShopItemList)
    {
        
    }
    public void SetRedPointOfCheckTab()
    {
        bool _boolState2 = CharacterRecorder.instance.needChairmanDealCount > 0 ? true : false;
        tabsList[2].transform.FindChild("RedPoint").gameObject.SetActive(_boolState2);
    }

    private void ClickTabsButton(GameObject go)
    {
        int level = CharacterRecorder.instance.level;
        if (go != null && curSelectTabName != go.name)
        {
            curSelectTabName = go.name;
            //NetworkHandler.instance.SendProcess(string.Format("5102#{0};", curSelectShopType));
        }
        SetButtonType(go);
    }
    #region 没用到
    public void SetTab(int tabType)
    {
        for (int i = 0; i < 4; i++)
        {
            if (tabsList[i] == null)
            {
                continue;
            }
            if (tabsList[i].name[3] != tabType)
            {
                tabsList[i].GetComponent<UISprite>().spriteName = "tab3";
                tabsList[i].transform.Find("Label").GetComponent<UILabel>().color = new Color(147 / 255f, 216 / 255f, 243 / 255f, 255 / 255f);
            }
            else
            {
                tabsList[i].GetComponent<UISprite>().spriteName = "tab4";
                tabsList[i].transform.Find("Label").GetComponent<UILabel>().color = new Color(56 / 255f, 95 / 255f, 135 / 255f, 255 / 255f);
            }
        }
        int tabIndex = 0;
        switch (tabType)
        {
            case 1: tabIndex = 0; break;
            case 2: tabIndex = 1; break;
            case 3: tabIndex = 2; break;
            case 4: tabIndex = 3; break;
            default: UIManager.instance.OpenPromptWindow("暂未开启", PromptWindow.PromptType.Hint, null, null); break;
        }
        for (int i = 0; i < partsList.Count; i++)
        {
            if (i == tabIndex)
            {
                partsList[i].SetActive(true);
                switch (i)
                {
                    case 0:
                        //partsList[0].GetComponent<LegionBasicInfoPart>().SetLegionBasicInfoPart(tabIndex, TextTranslator.instance.ActivitySevenHeroList); 
                        break;
                    case 1:
                        NetworkHandler.instance.SendProcess("8010#" + CharacterRecorder.instance.legionCountryID + ";");
                        //partsList[1].GetComponent<LegionRankListPart>().SetLegionRankListPart(tabIndex, TextTranslator.instance.ActivitySevenRankList); 
                        break;
                    case 2: //partsList[2].GetComponent<ActiveRankPart>().SetActiveAwardPart(tabIndex, mList); break;
                        break;
                    case 3:
                        break;
                }
            }
            else
            {
                partsList[i].SetActive(false);
            }
        }
    }
    #endregion
    private void SetButtonType(GameObject ButtonType)
    {
        for (int i = 0; i < 4; i++)
        {
            if (tabsList[i] == null)
            {
                continue;
            }
            if (tabsList[i].name != ButtonType.name)
            {
                tabsList[i].GetComponent<UISprite>().spriteName = "tab3";
                tabsList[i].transform.Find("Label").GetComponent<UILabel>().color = new Color(147 / 255f, 216 / 255f, 243 / 255f, 255 / 255f);
            }
            else
            {
                tabsList[i].GetComponent<UISprite>().spriteName = "tab4";
                tabsList[i].transform.Find("Label").GetComponent<UILabel>().color = new Color(56 / 255f, 95 / 255f, 135 / 255f, 255 / 255f);
            }
        }
        int tabIndex = 0;
        switch (ButtonType.name)
        {
            case "Tab1": tabIndex = 0; break;
            case "Tab2": tabIndex = 1; break;
            case "Tab3": tabIndex = 2; break;
            case "Tab4": tabIndex = 3; break;
            default: UIManager.instance.OpenPromptWindow("暂未开启", PromptWindow.PromptType.Hint, null, null); break;
        }
        for (int i = 0; i < partsList.Count; i++)
        {
            if (i == tabIndex)
            {
                partsList[i].SetActive(true);
                switch (i)
                {
                    case 0:
                        //partsList[0].GetComponent<LegionBasicInfoPart>().SetLegionBasicInfoPart(tabIndex, TextTranslator.instance.ActivitySevenHeroList); 
                        break;
                    case 1:
                        NetworkHandler.instance.SendProcess("8010#0;");
                        //partsList[1].GetComponent<LegionRankListPart>().SetLegionRankListPart(tabIndex, TextTranslator.instance.ActivitySevenRankList); 
                        break;
                    case 2: //partsList[2].GetComponent<ActiveRankPart>().SetActiveAwardPart(tabIndex, mList); break;
                        break;
                    case 3:
                        break;
                }
            }
            else
            {
                partsList[i].SetActive(false);
            }
        }
    }
    
}
