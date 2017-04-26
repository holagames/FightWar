using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class IntegrationWindow : MonoBehaviour
{
    public UILabel Integration;
    public GameObject InteItem;
    public GameObject uiGrid;
    public GameObject CloseButton;
    private Vector3 ScrollViewPosition;
    private UICenterOnChild uiCenter;
    private List<GameObject> ItemList = new List<GameObject>();
    int GetLayer;
    private string RewardItem;

    public UILabel LabelCondition;
    public UILabel LabelDescribe;
    void Start()
    {
        if (CharacterRecorder.instance.GrabIntegrationOpen == false)
        {
            GetInfo();
        }
        //NetworkHandler.instance.SendProcess("6001#;");
        //NetworkHandler.instance.SendProcess("6009#;");
        uiCenter = uiGrid.transform.GetComponent<UICenterOnChild>();
        //uiCenter.enabled = false;
        //RewardItem="90003$60!90001$15000!10002$15;90003$60!90001$18000!10002$15;" +
        //           "90003$60!90001$21000!10002$15;90003$80!90001$24000!10002$20;" +
        //           "90003$100!90001$30000!10104$1;90003$60!90001$15000!10002$15;" +
        //           "90003$60!90001$18000!10002$15;90003$60!90001$21000!10002$15;" +
        //           "90003$80!90001$24000!10002$20;90003$100!90001$30000!70016$1;";
        ScrollViewPosition = uiGrid.transform.parent.localPosition;
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick = delegate(GameObject go)
            {
                CharacterRecorder.instance.GrabIntegrationOpen = false;
                UIManager.instance.BackUI();
            };
        }
    }
    void Update()
    {
        if (uiGrid.transform.parent.localPosition != ScrollViewPosition)
        {
            if (GameObject.Find("ItemExplanationWindow") != null)
            {
                //UIManager.instance.BackUI();
                UIManager.instance.DestroyPanel("ItemExplanationWindow");
            }
            ScrollViewPosition = uiGrid.transform.parent.localPosition;
        }
    }

    public void GetInfo()
    {
        GetLayer = CharacterRecorder.instance.GetPointLayer;
        Integration.text = CharacterRecorder.instance.PvpPoint.ToString();
        Dictionary<int, PvpPointShop>.ValueCollection valueColl = TextTranslator.instance.PvpPointShopDic.Values;
        //string[] stritem = RewardItem.Split(';');
        foreach (PvpPointShop _PvpPointShop in valueColl)
        {
            GameObject go = NGUITools.AddChild(uiGrid, InteItem);
            go.transform.GetComponent<IntegrationItem>().Init(_PvpPointShop, GetLayer);//_PvpPointShop.PvpPointShopID, stritem[i], GetLayer
            ItemList.Add(go);
            uiGrid.transform.GetComponent<UIGrid>().Reposition();
        }
        StartCoroutine(SetCenterOnChild());
    }

    public void GrabIntegrationInfo()
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            DestroyImmediate(ItemList[i]);
        }
        ItemList.Clear();
        LabelCondition.text = "五次夺宝：积分+5   单次夺宝：积分+1";
        LabelDescribe.text = "进行夺宝即可获得积分，每天五点重置";
        GetLayer = CharacterRecorder.instance.GrabIntegrationGetPointLayer;
        Integration.text = CharacterRecorder.instance.GrabIntegrationPoint.ToString();
        Dictionary<int, IndianaPoint>.ValueCollection valueColl = TextTranslator.instance.IndianaPointDic.Values;
        foreach (IndianaPoint IndianaPointDic in valueColl)
        {
            GameObject go = NGUITools.AddChild(uiGrid, InteItem);
            go.transform.GetComponent<IntegrationItem>().InitInfo(IndianaPointDic, GetLayer);//_PvpPointShop.PvpPointShopID, stritem[i], GetLayer
            ItemList.Add(go);
            uiGrid.transform.GetComponent<UIGrid>().Reposition();
        }
        GameObject mapScrollView = gameObject.transform.Find("All/SpriteBG/MapScrollView").gameObject;
        mapScrollView.GetComponent<UIPanel>().clipOffset = new Vector3(0, 0 - 146 * (GetLayer), 0);
        mapScrollView.transform.localPosition = new Vector3(0, -42 + 146 * (GetLayer), 0);


        StartCoroutine(SetCenterOnChild());
    }

    IEnumerator SetCenterOnChild()
    {
        yield return new WaitForSeconds(0.2f);
        if (GetLayer > 1 && CharacterRecorder.instance.PvpPoint >= GetLayer * 2 + 2 && GetLayer < 9) //GetLayer > 2 && GetLayer < 9
        {
            uiCenter.enabled = true;
            Transform traobj = uiCenter.transform.GetChild(GetLayer + 1);
            uiCenter.CenterOn(traobj);
        }
        uiCenter.enabled = false;

    }
    public void updateItemlistShow(int _getlayer, string ItemAward, int id) //id==1竞技场 2夺宝
    {
        List<Item> itemlist = new List<Item>();
        string[] secSplit = ItemAward.Split('!');
        for (int i = 0; i < secSplit.Length - 1; i++)
        {
            string[] ticSplit = secSplit[i].Split('$');
            Item _item = new Item(int.Parse(ticSplit[0]), int.Parse(ticSplit[1]));
            itemlist.Add(_item);
        }
        foreach (GameObject ob in ItemList)
        {
            if (id == 1)
            {
                if (ob.GetComponent<IntegrationItem>()._Layer / 2 == _getlayer)
                {
                    ob.GetComponent<IntegrationItem>().SetInfo();
                }
            }
            else
            {
                if (ob.GetComponent<IntegrationItem>().Grablayer == _getlayer)
                {
                    ob.GetComponent<IntegrationItem>().SetInfo();
                }
            }
        }
        UIManager.instance.OpenSinglePanel("AdvanceWindow", false);
        GameObject.Find("AdvanceWindow").GetComponent<AdvanceWindow>().SetInfo(AdvanceWindowType.GainResult, null, null, null, null, itemlist);
    }
}
