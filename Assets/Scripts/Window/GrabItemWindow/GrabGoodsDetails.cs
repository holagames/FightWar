using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrabGoodsDetails : MonoBehaviour
{
    public GameObject InformationObj;
    public GameObject Name;
    public UILabel MessageName;
    public GameObject IconGrade;
    public GameObject IconItem;
    public EquipStrong GrabDetail;
    public GameObject basicinfoItem;
    public GameObject refineInfoItem;
    public GameObject ItemMessage;
    public GameObject SureButton;
    public GameObject GotoButton;
    public GameObject BlackButton;
    public GameObject BasicsGrid;
    public GameObject RefineGrid;
    public List<string> ListMation = new List<string>();
    public List<string> ListMationAdd = new List<string>();
    public List<string> ListRefine = new List<string>();
    public List<string> ListRefineAdd = new List<string>();
    public List<GameObject> MationLabel = new List<GameObject>();
    public List<GameObject> RefineLabel = new List<GameObject>();
    public UILabel KindLabel;
    public int IconID;
    public bool isGotoButton;
    // Use this for initialization
    void Start()
    {
        if (GameObject.Find("GuideArrow") == null)
        {
            gameObject.GetComponent<UIPanel>().depth = 22;
        }
        UIEventListener.Get(SureButton).onClick += delegate(GameObject obj)
        {
            if (CharacterRecorder.instance.GuideID[10] != 13)
            {
                ItemMessage.SetActive(false);
                GameObject.Find("GoodsItemObj").GetComponent<GoodsItemWindow>().isChangeName = false;
                GameObject.Find("GrabItemWindow").transform.Find("ALL/Text_HeChengCG").gameObject.SetActive(false);
            }
        };
        UIEventListener.Get(GotoButton).onClick += delegate(GameObject obj)//晋级
        {
            ItemMessage.SetActive(false);
            if (isGotoButton)
            {
                if (CharacterRecorder.instance.GuideID[10] == 13)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
                GameObject.Find("GoodsItemObj").GetComponent<GoodsItemWindow>().isChangeName = false;
                bool IsBook = false;
                int ItemCode = int.Parse(IconItem.GetComponent<UISprite>().spriteName);
                if (ItemCode / 10 % 2 == 1) //勋章
                {
                    IsBook = false;
                }
                else //书
                {
                    IsBook = true;
                }
                CharacterRecorder.instance.isToQualify = true;
                for (int i = 0; i < CharacterRecorder.instance.ownedHeroList.size; i++)
                {
                    if (!IsBook)
                    {
                        EquipStrong _myEquipStrong = TextTranslator.instance.GetEquipStrongByID(ItemCode);
                        HeroInfo mHeroInfo = TextTranslator.instance.GetHeroInfoByHeroID(CharacterRecorder.instance.ownedHeroList[i].cardID);
                        if (ItemCode > CharacterRecorder.instance.ownedHeroList[i].equipList[4].equipCode && _myEquipStrong.Race == mHeroInfo.heroRace)
                        {
                            TextTranslator.instance.HeadIndex = i;
                            //StrengEquipWindow.IsEnterEquipUIFromGrabGoods = true;
                            //CharacterRecorder.instance.isNeedRecordStrengTabType = false;
                            UIManager.instance.OpenPanel("StrengEquipWindow", true);
                            GameObject.Find("StrengEquipWindow").GetComponent<StrengEquipWindow>().EnterEquipAwakeUIFromGrabGoods(4 + 1);
                            CharacterRecorder.instance.setEquipTableIndex = 2;
                            //Debug.LogError(CharacterRecorder.instance.ownedHeroList[i].equipList[4].equipCode + "HeroClick" + TextTranslator.instance.HeadIndex);
                            break;
                        }
                        else
                        {
                            UIManager.instance.OpenPromptWindow("没有此类英雄", PromptWindow.PromptType.Hint, null, null);
                        }
                    }
                    else
                    {
                        EquipStrong _myEquipStrong = TextTranslator.instance.GetEquipStrongByID(ItemCode);
                        HeroInfo mHeroInfo = TextTranslator.instance.GetHeroInfoByHeroID(CharacterRecorder.instance.ownedHeroList[i].cardID);
                        if (ItemCode > CharacterRecorder.instance.ownedHeroList[i].equipList[5].equipCode && _myEquipStrong.Race == mHeroInfo.heroRace)
                        {
                            TextTranslator.instance.HeadIndex = i;
                            //StrengEquipWindow.IsEnterEquipUIFromGrabGoods = true;
                            //CharacterRecorder.instance.isNeedRecordStrengTabType = false;
                            UIManager.instance.OpenPanel("StrengEquipWindow", true);
                            GameObject.Find("StrengEquipWindow").GetComponent<StrengEquipWindow>().EnterEquipAwakeUIFromGrabGoods(5 + 1);
                            CharacterRecorder.instance.setEquipTableIndex = 2;
                            //Debug.LogError(CharacterRecorder.instance.ownedHeroList[i].equipList[5].equipCode + "HeroClick" + TextTranslator.instance.HeadIndex);
                            break;
                        }
                        else
                        {
                            UIManager.instance.OpenPromptWindow("没有此类英雄", PromptWindow.PromptType.Hint, null, null);
                        }
                    }

                }
            }
            else
            {
                UIManager.instance.OpenPanel("StrengEquipWindow", true);
                CharacterRecorder.instance.setEquipTableIndex = 0;
            }
        };

    }

    public void SetInfo(TextTranslator.ItemInfo ItemInfo)
    {
        //obtainObj.SetActive(false);
        InformationObj.SetActive(true);
        GotoButton.SetActive(false);
        //GotoButton.SetActive(false);
        BlackButton.SetActive(false);
        isGotoButton = false;
        //ShowButtonLabel(1);
        Name.GetComponent<UILabel>().text = GameObject.Find("GrabItemWindow").GetComponent<GrabWindow>().ItemNameColor(TextTranslator.instance.GetItemByItemCode(ItemInfo.itemCode).itemGrade) + ItemInfo.itemName + "[-]";
        IconItem.GetComponent<UISprite>().spriteName = ItemInfo.itemCode.ToString();
        if (GameObject.Find("GoodsItemObj").GetComponent<GoodsItemWindow>().isChangeName == true)
        {
            MessageName.text = "合成成功";
            StartCoroutine(DelayEffect());
            if (ItemInfo.itemCode != 59000 && ItemInfo.itemCode != 59010)
            {
                //GotoButton.SetActive(true);
                isGotoButton = true;
                GotoButton.transform.Find("Label").GetComponent<UILabel>().text = "去晋级";
            }
            else
            {
                //GotoButton.SetActive(true);
                GotoButton.transform.Find("Label").GetComponent<UILabel>().text = "去强化";
            }
        }
        else
        {
            MessageName.text = "基础信息";
            BlackButton.SetActive(true);
            if (ItemInfo.itemCode != 59000 && ItemInfo.itemCode != 59010)
            {
                BlackButton.transform.Find("Label").GetComponent<UILabel>().text = "去晋级";
            }
            else
            {
                BlackButton.transform.Find("Label").GetComponent<UILabel>().text = "去强化";
            }
        }
        IconGrade.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(ItemInfo.itemCode).itemGrade;
        GrabDetail = TextTranslator.instance.GetEquipStrongByID(ItemInfo.itemCode);
        EquipDetailInfo equipDel = TextTranslator.instance.GetEquipInfoByID(ItemInfo.itemCode);
        UpdateEquipstrong(GrabDetail, 1);
        UpdateRefineInfo(equipDel, 1);
        if (ItemInfo.itemCode >= 59000)
        {
            KindLabel.text = "强化专用";
        }
        else
        {
            switch (GrabDetail.Race)
            {
                case 1:
                    KindLabel.text = "步兵专用";
                    break;
                case 2:
                    KindLabel.text = "车辆专用";
                    break;
                case 3:
                    KindLabel.text = "飞机专用";
                    break;
            }
        }
    }

    IEnumerator DelayEffect()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject.Find("GrabItemWindow").transform.Find("ALL/Text_HeChengCG").gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        GotoButton.SetActive(true);
        if (GameObject.Find("GrabItemWindow").transform.Find("ALL/Text_HeChengCG").gameObject != null)
        {
            GameObject.Find("GrabItemWindow").transform.Find("ALL/Text_HeChengCG").gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// 读取强化基础信息
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="es"></param>
    /// <param name="level"></param>
    public void UpdateEquipstrong(EquipStrong es, int level)
    {
        for (int i = 0; i < MationLabel.Count; i++)
        {
            DestroyImmediate(MationLabel[i]);
        }
        ListMation.Clear();
        ListMationAdd.Clear();
        MationLabel.Clear();
        string info;
        string addinfo;
        if (es.Hp > 0)
        {
            info = "生命:";
            addinfo = "+" + (es.Hp * level).ToString();
            ListMation.Add(info);
            ListMationAdd.Add(addinfo);
        }
        if (es.Atk > 0)
        {
            info = "攻击:";
            addinfo = "+" + UpdateShowLabelPercent(es.Atk * level);
            ListMation.Add(info);
            ListMationAdd.Add(addinfo);
        }
        if (es.Def > 0)
        {
            info = "防御:";
            addinfo = "+" + UpdateShowLabelPercent(es.Def * level);
            ListMation.Add(info);
            ListMationAdd.Add(addinfo);
        }
        if (es.Hit > 0)
        {
            info = "命中: ";
            addinfo = "+" + UpdateShowLabelPercent(es.Hit * level);
            ListMation.Add(info);
            ListMationAdd.Add(addinfo);
        }
        if (es.NoHit > 0)
        {
            info = "闪避:";
            addinfo = "+" + UpdateShowLabelPercent(es.NoHit * level);
            ListMation.Add(info);
            ListMationAdd.Add(addinfo);

        }
        if (es.Crit > 0)
        {
            info = "暴击:";
            addinfo = "+" + UpdateShowLabelPercent(es.Crit * level);
            ListMation.Add(info);
            ListMationAdd.Add(addinfo);
        }
        if (es.NoCrit > 0)
        {
            info = "抗暴:";
            addinfo = "+" + UpdateShowLabelPercent(es.NoCrit * level);
            ListMation.Add(info);
            ListMationAdd.Add(addinfo);
        }
        if (es.DmgBonus > 0)
        {
            info = "伤害加成:";
            addinfo = "+" + UpdateShowLabelPercent(es.DmgBonus * level);
            ListMation.Add(info);
            ListMationAdd.Add(addinfo);
        }
        if (es.DmgReduction > 0)
        {
            info = "伤害减免:";
            addinfo = "+" + UpdateShowLabelPercent(es.DmgReduction * level);
            ListMation.Add(info);
            ListMationAdd.Add(addinfo);
        }
        for (int i = 0; i < ListMation.Count + 1; i++)
        {
            GameObject LabelObj = Instantiate(basicinfoItem) as GameObject;
            LabelObj.transform.parent = BasicsGrid.transform;
            LabelObj.transform.localScale = Vector3.one;
            LabelObj.transform.localPosition = Vector3.zero;
            LabelObj.SetActive(true);
            MationLabel.Add(LabelObj);
            if (i == 0 && i - 1 != 0)
            {
                LabelObj.GetComponent<UILabel>().text = "强化等级：";
                LabelObj.transform.Find("AddInfo").GetComponent<UILabel>().text = "1/" + es.LvMax;
            }
            else
            {
                LabelObj.GetComponent<UILabel>().text = ListMation[i - 1];
                LabelObj.transform.Find("AddInfo").GetComponent<UILabel>().text = ListMationAdd[i - 1];
            }
        }
        BasicsGrid.GetComponent<UIGrid>().Reposition();
    }
    public void UpdateRefineInfo(EquipDetailInfo equipDel, int level)
    {
        for (int i = 0; i < RefineLabel.Count; i++)
        {
            DestroyImmediate(RefineLabel[i]);
        }
        ListRefineAdd.Clear();
        ListRefine.Clear();
        RefineLabel.Clear();
        string info;
        string addinfo;
        if (equipDel.ADAttack != 0)
        {
            info = "攻击:";
            addinfo = "+" + UpdateShowLabelPercent(equipDel.ADAttack * level);
            ListRefine.Add(info);
            ListRefineAdd.Add(addinfo);
        }
        if (equipDel.Crit != 0)
        {
            info = "暴击:";
            addinfo = "+" + UpdateShowLabelPercent(equipDel.Crit * level);
            ListRefine.Add(info);
            ListRefineAdd.Add(addinfo);
        }
        if (equipDel.ADDenfance != 0)
        {
            info = "防御:";
            addinfo = "+" + UpdateShowLabelPercent(equipDel.ADDenfance * level);
            ListRefine.Add(info);
            ListRefineAdd.Add(addinfo);
        }
        if (equipDel.NoCrit != 0)
        {
            info = "暴抗:";
            addinfo = "+" + UpdateShowLabelPercent(equipDel.NoCrit * level);
            ListRefine.Add(info);
            ListRefineAdd.Add(addinfo);
        }
        if (equipDel.HP != 0)
        {
            info = "生命:";
            addinfo = "+" + UpdateShowLabelPercent(equipDel.HP * level);
            ListRefine.Add(info);
            ListRefineAdd.Add(addinfo);
        }
        if (equipDel.Hit != 0)
        {
            info = "命中:";
            addinfo = "+" + UpdateShowLabelPercent(equipDel.Hit * level);
            ListRefine.Add(info);
            ListRefineAdd.Add(addinfo);
        }
        if (equipDel.NoHit != 0)
        {
            info = "闪避:";
            addinfo = "+" + UpdateShowLabelPercent(equipDel.NoHit * level);
            ListRefine.Add(info);
            ListRefineAdd.Add(addinfo);
        }
        if (equipDel.DmgBonus != 0)
        {
            info = "伤害加成:";
            addinfo = "+" + UpdateShowLabelPercent(equipDel.DmgBonus * level);
            ListRefine.Add(info);
            ListRefineAdd.Add(addinfo);
        }
        if (equipDel.DmgReduction != 0)
        {
            info = "伤害减免:";
            addinfo = "+" + UpdateShowLabelPercent(equipDel.DmgReduction * level);
            ListRefine.Add(info);
            ListRefineAdd.Add(addinfo);
        }
        for (int i = 0; i < ListRefine.Count + 1; i++)
        {
            GameObject LabelObj = Instantiate(basicinfoItem) as GameObject;
            LabelObj.transform.parent = RefineGrid.transform;
            LabelObj.transform.localScale = Vector3.one;
            LabelObj.transform.localPosition = Vector3.zero;
            LabelObj.SetActive(true);
            RefineLabel.Add(LabelObj);
            if (i == 0 && i - 1 != 0)
            {
                LabelObj.GetComponent<UILabel>().text = "精炼等级：";
                LabelObj.transform.Find("AddInfo").GetComponent<UILabel>().text = "1/" + equipDel.LevelMax;
            }
            else
            {
                LabelObj.GetComponent<UILabel>().text = ListRefine[i - 1];
                LabelObj.transform.Find("AddInfo").GetComponent<UILabel>().text = ListRefineAdd[i - 1];
            }
        }
        RefineGrid.GetComponent<UIGrid>().Reposition();
    }
    private string UpdateShowLabelPercent(float Numebr)
    {
        if (Numebr > 0 && Numebr < 1)
        {
            return (Numebr * 100) + "%";
        }
        else
        {
            return Numebr.ToString();
        }
    }

}
