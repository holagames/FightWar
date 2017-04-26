using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuySuccessWindow : MonoBehaviour
{
    public GameObject DesObj;
    public GameObject uiGride;
    public GameObject gainResultItem;
    public GameObject uiScrollView;

    private List<Item> _ItemList = new List<Item>();

    private int number = 0;
    private int vipCount = -1;
    // Use this for initialization
    void Start()
    {
        UIEventListener.Get(this.gameObject).onClick = delegate(GameObject go)
        {
            if (GameObject.Find("VipRechargeWindow") != null)
            {
                GameObject.Find("VipRechargeWindow").GetComponent<VIPRechargeWindow>().SetVipOneState();
            }
            else
            {
                if (vipCount != 0)
                {
                    GameObject vipPanel = GameObject.Find("VIPShopWindow");
                    if (vipPanel == null)
                    {
                        //Debug.LogError("vipPanel....12");
                        UIManager.instance.OpenPanel("VIPShopWindow", true);
                        GameObject vipPanelNew = GameObject.Find("VIPShopWindow");
                        if (vipPanelNew != null)
                        {
                            vipPanelNew.GetComponent<VipShopWindow>().SetChangeButton();
                        }
                        return;
                    }
                    else
                    {
                        vipPanel.GetComponent<VipShopWindow>().SetChangeButton();
                    }
                }
            }
            DestroyImmediate(this.gameObject);
        };
    }
    public void ReceiverMsg_9501(int lunaGem, int vip)
    {
        number = lunaGem;
        vipCount = vip;
        //Debug.LogError(".....: " + lunaGem);
        AudioEditer.instance.PlayOneShot("ui_recieve");
        StartCoroutine(DelayShowButton());
        this._ItemList.Clear();
        Item item = new Item(90002, lunaGem);
        _ItemList.Add(item);
        //GainResultPart.SetActive(true);
        //    BG.SetActive(true);
        //GainResultPart.GetComponent<GainResultPart>().SetGainResultPart(_ItemList);
        StartCoroutine(CreatItems());
    }
    IEnumerator DelayShowButton()
    {
        yield return new WaitForSeconds(1.0f);
        //     SureButton.SetActive(true);
        DesObj.SetActive(true);
    }

    IEnumerator CreatItems()
    {
        if (this._ItemList.Count == 1)
        {
            yield return new WaitForSeconds(0.8f);
            GameObject obj = NGUITools.AddChild(uiGride, gainResultItem) as GameObject;
            obj.transform.localPosition += new Vector3(280, 0, 0);
            if (obj.GetComponent<GainResultItem>() == null)
            {
                obj.AddComponent<GainResultItem>();
            }
            //Debug.LogError("++++: " + this._ItemList[0].itemGrade);
            obj.GetComponent<GainResultItem>().SetGainResultItem(this._ItemList[0]);
            TextTranslator.instance.ItemDescription(obj, this._ItemList[0].itemCode, 0);
            // uiGride.GetComponent<UIGrid>().enabled = false;
        }
        else
        {
            yield return new WaitForSeconds(0.8f);
            for (int i = 0; i < this._ItemList.Count; i++)
            {
                //yield return new WaitForSeconds(0.2f);
                GameObject obj = NGUITools.AddChild(uiGride, gainResultItem) as GameObject;
                obj.GetComponent<UIDragScrollView>().scrollView = uiScrollView.GetComponent<UIScrollView>();
                obj.transform.localPosition += new Vector3(140 * i, 0, 0);
                obj.GetComponent<GainResultItem>().SetGainResultItem(this._ItemList[i]);
                TextTranslator.instance.ItemDescription(obj, this._ItemList[i].itemCode, this._ItemList[i].itemCount);
                uiGride.GetComponent<UIGrid>().Reposition();
            }
        }

    }
}
