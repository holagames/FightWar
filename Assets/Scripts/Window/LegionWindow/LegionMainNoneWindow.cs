using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionMainNoneWindow : MonoBehaviour
{
    [HideInInspector]
    public static string curSelectTabName = "Tab1";
    public GameObject MyGrid;
    public GameObject MyRankShopItem;
    public List<GameObject> tabsList = new List<GameObject>();
    public List<GameObject> partsList = new List<GameObject>();
    List<LegionItemData> _mList = new List<LegionItemData>();
    Color tab4Color = new Color(99 / 255f, 212 / 255f, 241 / 255f, 255 / 255f);
    Color tab4OutlineColor = new Color(38 / 255f, 123 / 255f, 146 / 255f, 255 / 255f);
    Color tab3Color = new Color(50 / 255f, 152 / 255f, 179 / 255f, 255 / 255f);
    Color tab3OutlineColor = new Color(16 / 255f, 97 / 255f, 120 / 255f, 255 / 255f);
    void OnEnable()
    {

    }
    // Use this for initialization
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.军团);
        UIManager.instance.UpdateSystems(UIManager.Systems.军团);

        NetworkHandler.instance.SendProcess("8010#" + CharacterRecorder.instance.legionCountryID + ";");
        for (int i = 0; i < tabsList.Count; i++)
        {
            UIEventListener.Get(tabsList[i]).onClick = ClickTabsButton;
        }
        //SetButtonType(tabsList[0]);
    }
    public void SetLegionSecondWindow(List<LegionItemData> _mList)
    {
        this._mList = _mList;
        SetButtonType(tabsList[0]);
        /* DestroyGride();
       
         for (int i = 0; i < _ShopItemList.Count; i++)
         {
             GameObject go = NGUITools.AddChild(MyGrid, MyRankShopItem);
             go.GetComponent<RankShopItem>().Init(_ShopItemList[i]);
         }
         MyGrid.GetComponent<UIGrid>().Reposition();*/

    }
    void DestroyGride()
    {
        for (int i = MyGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(MyGrid.transform.GetChild(i).gameObject);
        }
    }
    private void ClickTabsButton(GameObject go)
    {
        int level = CharacterRecorder.instance.level;
        if (go != null && curSelectTabName != go.name)
        {
            curSelectTabName = go.name;
            //NetworkHandler.instance.SendProcess(string.Format("5102#{0};", curSelectShopType));
            if (go.name == "Tab1")
            {
                NetworkHandler.instance.SendProcess("8010#" + CharacterRecorder.instance.legionCountryID + ";");
                return;
            }
            SetButtonType(go);
        }
    }
    private void SetButtonType(GameObject ButtonType)
    {

        for (int i = 0; i < tabsList.Count; i++)
        {
            if (tabsList[i].name != ButtonType.name)
            {
                tabsList[i].GetComponent<UISprite>().spriteName = "tab3";
                UILabel _myLabel = tabsList[i].transform.Find("Label").GetComponent<UILabel>();
                _myLabel.color = tab3Color;
                _myLabel.effectColor = tab3OutlineColor;
            }
            else
            {
                tabsList[i].GetComponent<UISprite>().spriteName = "tab4";
                UILabel _myLabel = tabsList[i].transform.Find("Label").GetComponent<UILabel>();
                _myLabel.color = tab4Color;
                _myLabel.effectColor = tab4OutlineColor;
            }
        }
        int tabIndex = 0;
        switch (ButtonType.name)
        {
            case "Tab1": tabIndex = 0; break;
            case "Tab2": tabIndex = 1; break;
            //case "LegionCreatWindow": tabIndex = 1; break;
            case "Tab3": tabIndex = 2; break;
            //case "LegionJoinWindow": tabIndex = 2; break;
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
                        //NetworkHandler.instance.SendProcess("8010#" + CharacterRecorder.instance.legionCountryID + ";");
                        SetLegionRankListPart(LegionDepth.LegionMain, this._mList);
                        break;
                    case 1:
                        partsList[1].GetComponent<LegionCreatWindow>().RandomFlagHead();
                        //partsList[1].GetComponent<LegionRankListPart>().SetLegionRankListPart(tabIndex, TextTranslator.instance.ActivitySevenRankList); 
                        break;
                    case 2:
                        //Debug.LogError("LegionJoinWindow....count..." + this._mList.Count);
                        //   partsList[2].GetComponent<LegionJoinWindow>().SetLegionJoinWindow(LegionDepth.LegionMain, this._mList);//查找 军团 默认不显示列表
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
    void ClearUIGride()
    {
        if (MyGrid.transform.childCount > 0)
        {
            for (int i = MyGrid.transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(MyGrid.transform.GetChild(i).gameObject);
            }
        }
    }
    public void SetLegionRankListPart(LegionDepth _LegionDepth, List<LegionItemData> _LegionItemList)
    {
        ClearUIGride();
        for (int i = 0; i < _LegionItemList.Count; i++)
        {
            GameObject obj = NGUITools.AddChild(MyGrid.gameObject, MyRankShopItem);
            obj.GetComponent<LegionItem>().SetLegionItemInfo(_LegionDepth, _LegionItemList[i]);
        }
        MyGrid.GetComponent<UIGrid>().Reposition();
    }

}
