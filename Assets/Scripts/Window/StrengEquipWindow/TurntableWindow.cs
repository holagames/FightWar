using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TurntableWindow : MonoBehaviour
{

    public GameObject QuestionButton;
    public List<GameObject> ItemList = new List<GameObject>();
    public GameObject OneButton;
    public GameObject TenButton;
    public GameObject RefreshButton;
    public GameObject Slider;
    public float EffectID = 0;
    public float Timer = 0f;
    public int index = 5;

    public List<GameObject> LineEffectList = new List<GameObject>();
    public GameObject HeWuQiCJ;
    public List<GameObject> ItemEffectList = new List<GameObject>();
    public GameObject EffectBG;

    public int RoleFrameID = 0; //碎片ID

    public GameObject FramgeWindow; //碎片窗口
    public GameObject FramgeItem; //碎片item
    public GameObject FramgeGrid;
    public GameObject FramgeCloseButton;
    public GameObject LeftButton;
    public GameObject RightButton;
    public List<GameObject> FramgeList = new List<GameObject>();
    public GameObject FramgeScrollView;
    public int MoveItemID = 1;

    public UICenterOnChild uicenter;
    private int Index = 0;//翻页次数
    public GameObject BackButton;
    public GameObject QusWindow;
    public GameObject QusClose;

    public GameObject OneButtonCost;
    public UILabel FreeNumber;
    //特效
    public GameObject LineEffect;
    public GameObject ItemEffect;
    //
    public bool IsButtonFinish;
    public UILabel SliderLabel;
    // Use this for initialization
    //现拥有碎片数量
    public UILabel FramgeItemNumber;
    //按钮特效
    public GameObject OneEffect;
    public GameObject TenEffect;
    void Start()
    {
        // CloseEffect();
        
        NetworkHandler.instance.SendProcess("3306#" + CharacterRecorder.instance.heroPresentWeapon.ToString());
        NetworkHandler.instance.SendProcess("3305#");

        EffectBG.SetActive(true);
        UIEventListener.Get(QuestionButton).onClick += delegate(GameObject go)
        {
            QusWindow.SetActive(true);
            QusInfo();
        };
        UIEventListener.Get(BackButton).onClick += delegate(GameObject go)
        {
            UIManager.instance.BackUI();
            CharacterRecorder.instance.setEquipTableIndex = 5;
        };
        UIEventListener.Get(OneButton).onClick += delegate(GameObject go)
        {
            if (IsButtonFinish == false)
            {
                IsButtonFinish = true;
                AudioEditer.instance.PlayOneShot("nuclear");
                NetworkHandler.instance.SendProcess("3307#");
            }
        };
        UIEventListener.Get(TenButton).onClick += delegate(GameObject go)
        {
            if (IsButtonFinish == false)
            {
                IsButtonFinish = true;
                AudioEditer.instance.PlayOneShot("nuclear");
                NetworkHandler.instance.SendProcess("3308#");
            }
        };
        UIEventListener.Get(RefreshButton).onClick += delegate(GameObject go)
        {
            FramgeWindow.SetActive(true);
            FrameInfoWindow();
        };
    }
    void QusInfo()
    {
        UIEventListener.Get(QusClose).onClick += delegate(GameObject go)
        {
            QusWindow.SetActive(false);
        };
    }
    #region 转盘效果
    //旋转特效
    //void ItemEffect()
    //{
    //    if (Timer < 0.8)
    //    {

    //        StartCoroutine(DelayEffect(index, 10));
    //    }
    //    else
    //    {
    //        //Debug.LogError("结束了");
    //        StartCoroutine(FinishDelayEffect(5));
    //    }

    //}
    //IEnumerator DelayEffect(int Index,int Number)
    //{
    //    while (true)
    //    {
    //        if (Index < Number)
    //        {
    //            ItemList[Index].transform.Find("Effect").gameObject.SetActive(true);
    //            yield return new WaitForSeconds(Timer);
    //            Index++;
    //            CloseEffect();           
    //        }
    //        else
    //        {
    //            break;
    //        }
    //    }
    //    Timer *= 2;
    //    index = 0;
    //    ItemEffect();
    //}
    //IEnumerator FinishDelayEffect(int ID)
    //{
    //    int num = 0;
    //    while (true)
    //    {
    //        if (num <= ID)
    //        {
    //            ItemList[num].transform.Find("Effect").gameObject.SetActive(true);
    //            Timer += 0.1f;
    //            yield return new WaitForSeconds(Timer);
    //            if (num != ID)
    //            {
    //                CloseEffect();
    //            } 
    //            num++;            
    //        }
    //        else
    //        {
    //            break;
    //        }
    //    }
    //    Timer = 0;
    //}
    //void CloseEffect()
    //{
    //    for (int i = 0; i < 10; i++)
    //    {
    //        ItemList[i].transform.Find("Effect").gameObject.SetActive(false);
    //    }
    //}
    #endregion

    //积分信息
    public void SetInfo(int RoleFrameID, int IntegralID, int GaChaNumber)
    {
        HeWuQiCJ.SetActive(false);
        HeroInfo(RoleFrameID);
        //Slider.GetComponent<UISpriteAnimation>().enabled = false;
        for (int i = 1; i < 10; i++)
        {
            WeaponWheel WheelInfo = TextTranslator.instance.GetWeaponWheelByID(i + 1);
            ItemList[i].transform.Find("Icon").GetComponent<UISprite>().spriteName = (WheelInfo.ItemID).ToString();
            ItemList[i].GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(WheelInfo.ItemID).itemGrade;
            if ((WheelInfo.ItemID > 60000 && WheelInfo.ItemID < 70000) || (WheelInfo.ItemID > 80000 && WheelInfo.ItemID < 90000))
            {
                ItemList[i].transform.Find("Framge").gameObject.SetActive(true);
            }
            else
            {
                ItemList[i].transform.Find("Framge").gameObject.SetActive(false);
            }
            ItemList[i].transform.Find("Label").GetComponent<UILabel>().text = WheelInfo.ItemNum.ToString();
            TextTranslator.instance.ItemDescription(ItemList[i], WheelInfo.ItemID, WheelInfo.ItemNum);
            //TextTranslator.instance.ItemDescription(ItemList[i], int.Parse(ItemList[i].transform.Find("Icon").GetComponent<UISprite>().spriteName), int.Parse(ItemList[i].transform.Find("Label").GetComponent<UILabel>().text));
        }
        FreeNumberInfo(GaChaNumber);       
        IntegralNumber(IntegralID);    
    }

    public void IntegralNumber(int IntegralID)
    {
        float amount = (float)IntegralID / 30000;
        Slider.GetComponent<UISprite>().fillAmount = amount;
        SliderLabel.text = IntegralID.ToString();
    }
    //设置碎片
    public void FramgeSetInfo(int RoleFrameID)
    {
        Debug.LogError("sss" + RoleFrameID);
        HeroInfo(RoleFrameID);
    }
    void HeroInfo(int RoleFrameID)
    {
        CharacterRecorder.instance.heroPresentWeapon = RoleFrameID; 
        ItemList[0].transform.Find("Icon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(RoleFrameID).picID.ToString();
        ItemList[0].GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(RoleFrameID).itemGrade;
        ItemList[0].transform.Find("Framge").gameObject.SetActive(true);
        ItemList[0].transform.Find("Label").GetComponent<UILabel>().text = TextTranslator.instance.GetWeaponWheelByID(1).ItemNum.ToString();
        TextTranslator.instance.ItemDescription(ItemList[0], RoleFrameID - 30000, TextTranslator.instance.GetWeaponWheelByID(1).ItemNum);
        FramgeItemNumber.text = "现拥有:[ffff00]" + TextTranslator.instance.GetItemByItemCode(RoleFrameID).itemName + "[-]" + TextTranslator.instance.GetItemCountByID(RoleFrameID) + "个";
    }
    //单抽
    public void OnceGaChaInfo(List<Item> itemlist, int IntegralID, List<int> RewardID, int GaChaNumber)
    {
        HeWuQiCJ.SetActive(true);
        Debug.LogError("ssss   " + RewardID[0]);
        OneEffect.SetActive(true);
        if (itemlist.Count == 1)
        {
            ShowEffectInfo(RewardID[0] - 1);
            //LineEffectList[RewardID[0] - 1].SetActive(true);
            //ItemEffectList[RewardID[0] - 1].SetActive(true);
        }
        StartCoroutine(EffectGaCha(itemlist));
        IntegralNumber(IntegralID);  
        FreeNumberInfo(GaChaNumber);
    }

    void ShowEffectInfo(int id)
    {
        GameObject lineItem = Instantiate(LineEffect) as GameObject;
        lineItem.transform.parent = gameObject.transform;
        lineItem.transform.localPosition = new Vector3(0, 0, 0);
        lineItem.transform.localRotation = new Quaternion(0, 0, 0, 0);
        lineItem.transform.Rotate(0, 0, 180 - 36 * (id));
        lineItem.transform.localScale = new Vector3(393, 393, 393);
        lineItem.SetActive(true);
        GameObject Item = Instantiate(ItemEffect) as GameObject;
        Item.transform.parent = ItemList[id].transform;
        Item.transform.localPosition = new Vector3(0, 0, 0);
        Item.transform.localRotation = new Quaternion(0, 0, 0, 0);
        Item.transform.localScale = new Vector3(300, 300, 300);
        Item.SetActive(true);
        LineEffectList.Add(lineItem);
        ItemEffectList.Add(Item);
    }
    void FreeNumberInfo(int GaChaNumber)
    {
        OneButtonCost.SetActive(false);
        FreeNumber.gameObject.SetActive(false);
        OneButton.transform.Find("RedPoint").gameObject.SetActive(true);
        switch (GaChaNumber)
        {
            case 3:
                FreeNumber.gameObject.SetActive(true);
                FreeNumber.text = "免费次数3/3";
                break;
            case 2:
                FreeNumber.gameObject.SetActive(true);
                FreeNumber.text = "免费次数2/3";
                break;
            case 1:
                FreeNumber.gameObject.SetActive(true);
                FreeNumber.text = "免费次数1/3";
                break;
            default:
                OneButton.transform.Find("RedPoint").gameObject.SetActive(false);
                CharacterRecorder.instance.isWeaponGachaFree = false;
                OneButtonCost.SetActive(true);
                break;

        }
    }
    //十连抽
    public void TenGaChaInfo(List<Item> itemlist, int IntegralID, List<int> RewardID)
    {
        HeWuQiCJ.SetActive(true);
        TenEffect.SetActive(true);
        {

            for (int i = 0; i < RewardID.Count - 1; i++)
            {
                for (int j = 1; j < RewardID.Count; j++)
                {
                    if (RewardID[i] == RewardID[j])
                    {
                        RewardID.Remove(RewardID[j]);
                    }
                }
            }
            for (int i = 0; i < RewardID.Count; i++)
            {
                ShowEffectInfo(RewardID[i] - 1);
            }
        }
        StartCoroutine(EffectGaCha(itemlist));
        IntegralNumber(IntegralID);  
    }

    IEnumerator EffectGaCha(List<Item> itemlist)
    {
        //Slider.GetComponent<UISpriteAnimation>().enabled = true;
        yield return new WaitForSeconds(3.5f);
        //Slider.GetComponent<UISpriteAnimation>().enabled = false;
        for (int i = 0; i < LineEffectList.Count; i++)
        {
            DestroyImmediate(LineEffectList[i]);
            DestroyImmediate(ItemEffectList[i]);
        }
        LineEffectList.Clear();
        ItemEffectList.Clear();
        HeWuQiCJ.SetActive(false);
        EffectBG.SetActive(true);
        UIManager.instance.OpenSinglePanel("AdvanceWindow", false);
        GameObject.Find("AdvanceWindow").GetComponent<AdvanceWindow>().SetInfo(AdvanceWindowType.GainResult, null, null, null, null, itemlist);
        FramgeItemNumber.text = "现拥有:[ffff00]" + TextTranslator.instance.GetItemByItemCode(CharacterRecorder.instance.heroPresentWeapon).itemName + "[-]" + TextTranslator.instance.GetItemCountByID(CharacterRecorder.instance.heroPresentWeapon) + "个";
        OneEffect.SetActive(false);
        TenEffect.SetActive(false);
        IsButtonFinish = false;
    }

    //选择碎片
    void FrameInfoWindow()
    {

        for (int i = 0; i < FramgeList.Count; i++)
        {
            DestroyImmediate(FramgeList[i]);
        }
        FramgeList.Clear();
        List<TextTranslator.ItemInfo> WeaponItem = TextTranslator.instance.GetItemByFuncType(23);
        for (int i = 0; i < WeaponItem.Count; i++)
        {
            GameObject go = Instantiate(FramgeItem) as GameObject;
            go.SetActive(true);
            go.transform.parent = FramgeGrid.transform;
            go.transform.localScale = new Vector3(1, 1, 1);
            if (i % 2 == 0)
            {
                go.transform.localPosition = new Vector3(300 * i, 0, 0);
            }
            else
            {
                go.transform.localPosition = new Vector3(300 * (i / 2), -255, 0);
            }
            go.name = i.ToString();
            go.GetComponent<FramgeItem>().SetInfo(WeaponItem[WeaponItem.Count - 1 - i].itemCode, WeaponItem[WeaponItem.Count - 1 - i].itemName, WeaponItem[WeaponItem.Count - 1 - i].itemDescription, WeaponItem[WeaponItem.Count - 1 - i].itemGrade);
            FramgeList.Add(go);
        }
        FramgeGrid.GetComponent<UIGrid>().repositionNow = true;
        UIEventListener.Get(FramgeCloseButton).onClick += delegate(GameObject go)
        {
            FramgeWindow.SetActive(false);
        };
        UIEventListener.Get(LeftButton).onClick += delegate(GameObject go)
        {
            CenterOnChildLeft();
        };
        UIEventListener.Get(RightButton).onClick += delegate(GameObject go)
        {
            CenterOnChildRight();

        };

    }
    void CenterOnChildRight()
    {
        Debug.LogError("sssssssss");
        uicenter.enabled = false;
        uicenter.enabled = true;
        for (int i = 0; i < FramgeGrid.transform.childCount; i++)
        {
            if (uicenter.centeredObject.name == FramgeGrid.transform.GetChild(i).gameObject.name)
            {
                Index = i;
            }
        }
        if (FramgeList.Count - Index >= 6)
        {
            Transform traobj = uicenter.transform.GetChild(Index + 2);
            uicenter.CenterOn(traobj);
            Index += 2;
        }
        //uicenter.enabled = false;
    }
    void CenterOnChildLeft()
    {
        Debug.LogError("sssssssss2");
        uicenter.enabled = false;
        uicenter.enabled = true;
        for (int i = 0; i < FramgeGrid.transform.childCount; i++)
        {
            if (uicenter.centeredObject.name == FramgeGrid.transform.GetChild(i).gameObject.name)
            {
                Index = i;
            }
        }
        if (Index - 2 > 0)
        {
            Transform traobj = uicenter.transform.GetChild(Index - 2);
            uicenter.CenterOn(traobj);
            Index -= 2;
        }
        //uicenter.enabled = false;
    }

}
