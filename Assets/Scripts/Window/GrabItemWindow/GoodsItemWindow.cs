using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GoodsItemWindow : MonoBehaviour
{
    public GameObject ItemOne;
    public GameObject ItemTwo;
    public GameObject ItemThree;
    public GameObject ItemFour;
    public GameObject ItemFive;
    public GameObject ItemSix;
    //public GameObject ItemSeven;
    public GameObject Details;
    public GameObject RobberyHeroListObj;
    public GameObject OneKeyButton;
    public GameObject MessageEngerObj;
    public bool isOneKey = false;
    public TextTranslator.ItemInfo ItemCentreDetails;
    public List<GameObject> ItemList = new List<GameObject>();
    public int Id;
    private int GrabId;
    public string itemName;
    public string color;
    public GameObject ButtonEffec;
    public GameObject SynthesisEffect;
    private int isSynthesis = 0;
    public bool isChangeName = false;
    public string SynsNumber;
    //被夺记录
    public GameObject LoseGrabMessageObj;
    public GameObject LoseMessageButton;
    // Use this for initialization
    //使用小精力药水
    public GameObject SmallSpriteButton;
    //一键夺宝
    public List<int> OneKeyGetId = new List<int>();
    public bool isOneKeyCantFinish = false;
    void Start()
    {
        if (UIEventListener.Get(GameObject.Find("ItemCentre")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("ItemCentre")).onClick += delegate(GameObject obj)
            {
                Details.SetActive(true);
                Details.GetComponent<GrabGoodsDetails>().SetInfo(ItemCentreDetails);
            };
        }
        if (UIEventListener.Get(GameObject.Find("ItemThree")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("ItemThree")).onClick = RobberyHeroListInfo;

        }
        if (UIEventListener.Get(GameObject.Find("ItemTwo")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("ItemTwo")).onClick = RobberyHeroListInfo;
        }
        UIEventListener.Get(GameObject.Find("SynthesisButton")).onClick += delegate(GameObject obj)
        {
            if (CharacterRecorder.instance.GuideID[10] != 13)
            {
                GameObject gw = GameObject.Find("GrabItemWindow");
                for (int i = 0; i < gw.GetComponent<GrabWindow>().FinishItemList.Count; i++)
                {
                    if (GrabId == gw.GetComponent<GrabWindow>().FinishItemList[i].itemCode - 30000)
                    {
                        isChangeName = true;
                        gw.GetComponent<GrabWindow>().FinishItemList.RemoveAt(i);
                        //UIManager.instance.OpenPromptWindow("获得 " + SynsNumber+" 个" + color + itemName + "[-]", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                if (CharacterRecorder.instance.GuideID[10] == 12)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
                NetworkHandler.instance.SendProcess("1404#" + GrabId + ";");
                CharacterRecorder.instance.ItemCentreDetails = ItemCentreDetails;
            }
        };

        if (UIEventListener.Get(OneKeyButton).onClick == null)
        {
            UIEventListener.Get(OneKeyButton).onClick += delegate(GameObject obj)
            {
                if (CharacterRecorder.instance.Vip >= 5)
                {
                    MessageEngerObj.SetActive(true);
                    MessageEngerEvent();
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("VIP等级大于5开放", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        UIEventListener.Get(LoseMessageButton).onClick += delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("1408#");
            LoseGrabMessageObj.SetActive(true);
            //发送被夺协议
        };
        //小体力
        UIEventListener.Get(SmallSpriteButton).onClick += delegate(GameObject go)
        {
            if (TextTranslator.instance.GetItemCountByID(10701) == 0)
            {
                UIManager.instance.OpenPromptWindow("您没有小精力药水", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
                GameObject.Find("EmployPropsWindow").GetComponent<EmployPropsWindow>().SetPropsInfo(10701);
            }
        };
        //单次抢夺失败后再继续抢夺
        if (CharacterRecorder.instance.isFailed == true)
        {
            Id = CharacterRecorder.instance.FailedID;
            NetworkHandler.instance.SendProcess("1401#" + CharacterRecorder.instance.FailedID + ";");
            SetRobberyHeroList();
        }
    }


    public void SetReset()
    {
        SetInfo(TextTranslator.instance.GetItemByItemCode(GrabId));
    }
    public void SetInfo(TextTranslator.ItemInfo ItemInfo)
    {
        for (int i = 0; i < ItemList.Count; i++)
        {
            ItemList[i].GetComponent<GrabFragment>().Isbol = false;
        }
        int Number = ItemInfo.itemGrade;
        ShowCentreItem(ItemInfo);
        GrabId = ItemInfo.itemCode;
        switch (Number)
        {
            case 1:
                GoodsItemShow();
                ItemSix.SetActive(true);
                if (UIEventListener.Get(GameObject.Find("ItemSix")).onClick == null)
                {
                    UIEventListener.Get(GameObject.Find("ItemSix")).onClick = RobberyHeroListInfo;

                }
                ItemTwo.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30000);
                ItemThree.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30001);
                ItemSix.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30002);
                break;
            case 2:
                GoodsItemShow();
                ItemSix.SetActive(true);
                if (UIEventListener.Get(GameObject.Find("ItemSix")).onClick == null)
                {
                    UIEventListener.Get(GameObject.Find("ItemSix")).onClick = RobberyHeroListInfo;

                }
                ItemTwo.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30000);
                ItemThree.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30001);
                ItemSix.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30002);
                break;
            case 3:
                GoodsItemShow();
                ItemSix.SetActive(true);
                if (UIEventListener.Get(GameObject.Find("ItemSix")).onClick == null)
                {
                    UIEventListener.Get(GameObject.Find("ItemSix")).onClick = RobberyHeroListInfo;

                }
                ItemTwo.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30000);
                ItemThree.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30001);
                ItemSix.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30002);
                break;
            case 4:
                GoodsItemShow();
                ItemFive.SetActive(true);
                ItemFour.SetActive(true);
                ItemTwo.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30000);
                ItemThree.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30001);
                ItemFour.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30002);
                ItemFive.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30003);
                FiveSixEventButton();
                break;
            case 5:
                GoodsItemShow();
                ItemFive.SetActive(true);
                ItemFour.SetActive(true);
                FiveSixEventButton();
                ItemSix.SetActive(true);
                if (UIEventListener.Get(GameObject.Find("ItemSix")).onClick == null)
                {
                    UIEventListener.Get(GameObject.Find("ItemSix")).onClick = RobberyHeroListInfo;

                }
                ItemTwo.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30000);
                ItemThree.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30001);
                ItemFour.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30002);
                ItemFive.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30003);
                ItemSix.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30004);
                break;
            case 6:
                GoodsItemShow();
                ItemOne.SetActive(true);
                ItemFour.SetActive(true);
                ItemFive.SetActive(true);
                ItemSix.SetActive(true);
                ItemTwo.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30000);
                ItemThree.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30001);
                ItemFour.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30002);
                ItemFive.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30003);
                ItemOne.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30005);
                ItemSix.GetComponent<GrabFragment>().UpdateItemShow(ItemInfo.itemCode, 30004);
                AllEventButton();
                break;
        }
        isSynthesisEffect(Number);
    }
    private void GoodsItemShow()
    {
        ItemOne.SetActive(false);
        ItemFour.SetActive(false);
        ItemFive.SetActive(false);
        ItemSix.SetActive(false);
    }
    private void ShowCentreItem(TextTranslator.ItemInfo ItemInfo)
    {
        if (GameObject.Find("ItemCentre") != null)
        {
            GameObject obj = GameObject.Find("ItemCentre");
            obj.GetComponent<UISprite>().spriteName = "Grade" + (ItemInfo.itemGrade);
            color = GameObject.Find("GrabItemWindow").GetComponent<GrabWindow>().ItemNameColor(ItemInfo.itemGrade);
            itemName = ItemInfo.itemName;
            obj.transform.Find("Label").GetComponent<UILabel>().text = color + itemName + "[-]";
            obj.transform.Find("NameLabel").GetComponent<UILabel>().text = color + itemName + "[-]";
            obj.transform.Find("Icon").GetComponent<UISprite>().spriteName = ItemInfo.itemCode.ToString();
            ItemCentreDetails = ItemInfo;
        }
    }

    private void FiveSixEventButton()
    {
        if (UIEventListener.Get(GameObject.Find("ItemFive")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("ItemFive")).onClick = RobberyHeroListInfo;

        }
        if (UIEventListener.Get(GameObject.Find("ItemFour")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("ItemFour")).onClick = RobberyHeroListInfo;
        }
    }
    private void AllEventButton()
    {
        if (UIEventListener.Get(GameObject.Find("ItemFive")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("ItemFive")).onClick = RobberyHeroListInfo;

        }
        if (UIEventListener.Get(GameObject.Find("ItemSix")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("ItemSix")).onClick = RobberyHeroListInfo;

        }
        if (UIEventListener.Get(GameObject.Find("ItemOne")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("ItemOne")).onClick = RobberyHeroListInfo;

        }
        if (UIEventListener.Get(GameObject.Find("ItemFour")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("ItemFour")).onClick = RobberyHeroListInfo;
        }
    }
    private void RobberyHeroListInfo(GameObject obj)
    {
        if (!obj.GetComponent<GrabFragment>().Isbol)
        {
            if (CharacterRecorder.instance.sprite < 2)
            {
                UIManager.instance.OpenPromptWindow("长官精力不足", PromptWindow.PromptType.Hint, null, null);
                UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
                NetworkHandler.instance.SendProcess("5012#10702;");
                return;
            }
            if (CharacterRecorder.instance.GuideID[10] == 8)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
            Id = obj.GetComponent<GrabFragment>().Id;
            CharacterRecorder.instance.FailedID = Id;
            NetworkHandler.instance.SendProcess("1401#" + obj.GetComponent<GrabFragment>().Id + ";");
        }
    }
    /// <summary>
    /// 打开界面并调用方法
    /// </summary>
    public void SetRobberyHeroList()
    {
        if (isOneKey == false)
        {
            RobberyHeroListObj.SetActive(true);
            RobberyHeroListObj.GetComponent<RobberyHeroList>().SetInfo();
        }
    }
    //**********************************一键夺宝以下************************************
    public void MessageEngerEvent()
    {
        if (UIEventListener.Get(MessageEngerObj.transform.Find("All/DemButton").gameObject).onClick == null)
        {
            UIEventListener.Get(MessageEngerObj.transform.Find("All/DemButton").gameObject).onClick += delegate(GameObject go)
            {
                //UIManager.instance.OpenPromptWindow("暂未开放", PromptWindow.PromptType.Alert, null, null);
                isOneKey = true;
                if (MessageEngerObj.transform.Find("All/SpriteCheck").GetComponent<UIToggle>().value == false && CharacterRecorder.instance.sprite < 2)
                {
                    UIManager.instance.OpenPromptWindow("精力不足", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    OneKeyEvent();
                }
            };
        }
        if (UIEventListener.Get(MessageEngerObj.transform.Find("All/CancleButton").gameObject).onClick == null)
        {
            UIEventListener.Get(MessageEngerObj.transform.Find("All/CancleButton").gameObject).onClick += delegate(GameObject go)
            {
                MessageEngerObj.SetActive(false);
                isOneKey = false;
            };
        }
        if (UIEventListener.Get(MessageEngerObj.transform.Find("All/CloseButton").gameObject).onClick == null)
        {
            UIEventListener.Get(MessageEngerObj.transform.Find("All/CloseButton").gameObject).onClick += delegate(GameObject go)
            {
                MessageEngerObj.SetActive(false);
                isOneKey = false;
            };
        }
        MessageEngerObj.transform.Find("All/LootName").GetComponent<UILabel>().text = "消耗大量精力，直至获得" + color + itemName + "[-]" + "全部碎片时[fb2d50]停止夺宝[-]，是否继续？？";
    }
    //一键夺宝效果
    public void OneKeyEvent()
    {
        GameObject grabResult = GameObject.Find("GrabResult");
        if (isOneKey)
        {
            OneKeyGetId.Clear();
            //NetworkHandler.instance.SendProcess("1403#" + OneKeyGetId[0] + ";9169;" + OneKeyGetId[0] % 10);
            for (int i = 0; i < ItemList.Count; i++)
            {
                //Debug.LogError("aaaaa   " + ItemList[i].GetComponent<GrabFragment>().Id);
                if (ItemList[i].GetComponent<GrabFragment>().Isbol == false && ItemList[i].GetComponent<GrabFragment>().Id != 0 && (ItemList[i].GetComponent<GrabFragment>().Id / 10 * 10 - 30000 == GrabId))
                {
                    //Debug.LogError("ssss   " + ItemList[i].GetComponent<GrabFragment>().Id);
                    OneKeyGetId.Add(ItemList[i].GetComponent<GrabFragment>().Id);
                }
            }
            if (OneKeyGetId.Count != 0)
            {
                MessageEngerObj.SetActive(false);
                if (CharacterRecorder.instance.sprite >= 2)
                {
                    NetworkHandler.instance.SendProcess("1403#" + OneKeyGetId[0] + ";9169;" + OneKeyGetId[0] % 10);
                }
                else if (MessageEngerObj.transform.Find("All/SpriteCheck").GetComponent<UIToggle>().value == true)
                {

                    if (TextTranslator.instance.GetItemCountByID(10703) > 0)
                    {
                        NetworkHandler.instance.SendProcess("5002#10703;1;");
                    }
                    else if (TextTranslator.instance.GetItemCountByID(10702) > 0)
                    {
                        NetworkHandler.instance.SendProcess("5002#10702;1;");
                    }
                    else if (TextTranslator.instance.GetItemCountByID(10701) > 0)
                    {
                        if (TextTranslator.instance.GetItemCountByID(10701) >= 2)
                        {
                            NetworkHandler.instance.SendProcess("5002#10701;2;");
                        }
                        else if (TextTranslator.instance.GetItemCountByID(10701) == 1)
                        {
                            NetworkHandler.instance.SendProcess("5002#10701;1;");
                        }
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("没有精力药水可使用", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                //else
                //{
                //    if (grabResult != null)
                //    {
                //        isOneKeyCantFinish = true;
                //    }
                //}
                // Debug.LogError("一键夺宝发送ID+" + OneKeyGetId[0] + "      " + OneKeyGetId[0] % 10);
            }
            else
            {
                UIManager.instance.OpenPromptWindow("碎片已满", PromptWindow.PromptType.Hint, null, null);
            }
        }
        //StartCoroutine(OneKeySendProcess());
    }

    IEnumerator OneKeySendProcess()
    {
        if (OneKeyGetId.Count != 0)
        {
            NetworkHandler.instance.SendProcess("1403#" + OneKeyGetId[0] + ";1;" + OneKeyGetId[0] % 10);
            Debug.LogError("一键夺宝发送ID+" + OneKeyGetId[0] + "      " + OneKeyGetId[0] % 10);
            yield return new WaitForSeconds(1f);
        }
    }



    //**********************************一键夺宝以上************************************
    public void ShowItemEffect()
    {
        SynthesisEffect.SetActive(true);
        AudioEditer.instance.PlayOneShot("ui_duobao");
        StartCoroutine(DelayItemEffect());
    }
    IEnumerator DelayItemEffect()
    {
        yield return new WaitForSeconds(0.34f);
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i].GetComponent<GrabFragment>().Isbol)
            {
                string AniName = "";
                switch (i)
                {
                    case 0:
                        AniName = "Grab1Animation";
                        //ItemList[i].transform.Find("WF_ui_JiNeng_ShengJi03_DuoBao").gameObject.SetActive(true);
                        ItemList[i].GetComponent<GrabFragment>().EquipIcon.GetComponent<Animator>().Play(AniName);
                        break;
                    case 1:
                        AniName = "Grab2Animation";
                        ItemList[i].transform.Find("WF_ui_JiNeng_ShengJi03_DuoBao").gameObject.SetActive(true);
                        ItemList[i].GetComponent<GrabFragment>().EquipIcon.GetComponent<Animator>().Play(AniName);
                        break;
                    case 2:
                        AniName = "Grab3Animation";
                        //ItemList[i].transform.Find("WF_ui_JiNeng_ShengJi03_DuoBao").gameObject.SetActive(true);
                        ItemList[i].GetComponent<GrabFragment>().EquipIcon.GetComponent<Animator>().Play(AniName);
                        break;
                    case 3:
                        AniName = "Grab4Animation";
                        //ItemList[i].transform.Find("WF_ui_JiNeng_ShengJi03_DuoBao").gameObject.SetActive(true);
                        ItemList[i].GetComponent<GrabFragment>().EquipIcon.GetComponent<Animator>().Play(AniName);
                        break;
                    case 4:
                        AniName = "Grab5Animation";
                        //ItemList[i].transform.Find("WF_ui_JiNeng_ShengJi03_DuoBao").gameObject.SetActive(true);
                        ItemList[i].GetComponent<GrabFragment>().EquipIcon.GetComponent<Animator>().Play(AniName);
                        break;
                    case 5:
                        AniName = "Grab6Animation";
                        //ItemList[i].transform.Find("WF_ui_JiNeng_ShengJi03_DuoBao").gameObject.SetActive(true);
                        ItemList[i].GetComponent<GrabFragment>().EquipIcon.GetComponent<Animator>().Play(AniName);
                        break;
                }
            }
        }
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i].GetComponent<GrabFragment>().Isbol)
            {
                ItemList[i].GetComponent<GrabFragment>().EquipIcon.GetComponent<Animator>().Play("Idle");
                ItemList[i].transform.Find("WF_ui_JiNeng_ShengJi03_DuoBao").gameObject.SetActive(false);
            }
        }
        if (GameObject.Find("GrabItemWindow") != null)
        {
            GameObject.Find("GrabItemWindow").GetComponent<GrabWindow>().SetUpdate();
        }
        yield return new WaitForSeconds(0.5f);
        Details.SetActive(true);
        SynthesisEffect.SetActive(false);
        //GameObject.Find("GrabItemWindow").transform.Find("ALL/Text_HeChengCG").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Details.GetComponent<GrabGoodsDetails>().SetInfo(CharacterRecorder.instance.ItemCentreDetails);
        //yield return new WaitForSeconds(1f);
        //GameObject.Find("GrabItemWindow").transform.Find("ALL/Text_HeChengCG").gameObject.SetActive(false);
    }

    public void isSynthesisEffect(int num)
    {
        //合成提示特效
        for (int i = 0; i < ItemList.Count; i++)
        {
            ItemList[i].transform.Find("WF_ui_JiNeng_ShengJi03_DuoBao").gameObject.SetActive(false);
            if (ItemList[i].GetComponent<GrabFragment>().Isbol)
            {
                isSynthesis++;
            }
        }
        if (isSynthesis == num)
        {
            for (int i = 0; i < ItemList.Count; i++)
            {
                if (ItemList[i].GetComponent<GrabFragment>().Isbol)
                {
                    ItemList[i].transform.Find("WF_ui_JiNeng_ShengJi03_DuoBao").gameObject.SetActive(true);
                }
            }
            ShowSynsName();
            ButtonEffec.SetActive(true);
        }
        else
        {
            transform.Find("SynthesisButton/Label").GetComponent<UILabel>().text = "合  成";
            ButtonEffec.SetActive(false);
        }
        isSynthesis = 0;
    }
    public void ShowSynsName()
    {
        int SynsNumber = 0;
        List<int> num = new List<int>();
        num.Clear();
        for (int i = 0; i < ItemList.Count; i++)
        {
            if (ItemList[i].GetComponent<GrabFragment>().Isbol)
            {
                num.Add(int.Parse(ItemList[i].GetComponent<GrabFragment>().ItemNumber.GetComponent<UILabel>().text));
            }
        }
        for (int i = 0; i < num.Count - 1; i++)
        {
            for (int j = 1; j < num.Count; j++)
            {
                if (num[i] > num[j])
                {
                    SynsNumber = num[j];
                    num[j] = num[i];
                    num[i] = SynsNumber;
                }
            }
        }
        if (num[0] > 1)
        {
            transform.Find("SynthesisButton/Label").GetComponent<UILabel>().text = "全部合成";
        }
        else
        {
            transform.Find("SynthesisButton/Label").GetComponent<UILabel>().text = "合  成";
        }
    }
}
