using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoleEquipInfoWindow : MonoBehaviour
{

    int CharacterRoleID = 0;
    int ClickIndex = 1;
    int itemID = 0;

    public GameObject SpriteEquipInfo;
    public GameObject LabelEquipCount;
    public GameObject LabelEquip;
    public GameObject LabelStrongAll;
    public GameObject LabelStrongAllMoney;
    public GameObject LabelStrongMoney;
    public UISlider ProgressBar;
    public UILabel NameLabel;
    public UILabel LevelLabel;
    public UILabel MaxStrongLabel;
    public UILabel AllStrongMoneyLabel;
    public UILabel OneStrongMoneyLabel;

    void Start()
    {
        //SetStrongMonet(0, 1);
        if (UIEventListener.Get(GameObject.Find("EquipInfoCloseButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("EquipInfoCloseButton")).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }

        if (UIEventListener.Get(GameObject.Find("ChangeButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("ChangeButton")).onClick += delegate(GameObject go)
            {
                if (GameObject.Find("RoleWindow") != null)
                {
                    RoleWindow rw = GameObject.Find("RoleWindow").GetComponent<RoleWindow>();
                    rw.ClickIndex = ClickIndex;
                }
                PictureCreater.instance.DestroyAllComponent();
                UIManager.instance.OpenPanel("RoleEquipWindow", false);
                string spriteName = SpriteEquipInfo.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName;
                RoleEquipWindow reiw = GameObject.Find("RoleEquipWindow").GetComponent<RoleEquipWindow>();
                if (spriteName.Substring(1, 1) == "0")
                {
                    reiw.SetInfo(1, 1, int.Parse(spriteName.Substring(2, 1)));
                }
                else if (spriteName.Substring(1, 1) == "3")
                {
                    reiw.SetInfo(2, 1, int.Parse(spriteName.Substring(2, 1)));
                }
                else if (spriteName.Substring(1, 1) == "1")
                {
                    reiw.SetInfo(1, 2, int.Parse(spriteName.Substring(2, 1)));
                }
                else if (spriteName.Substring(1, 1) == "4")
                {
                    reiw.SetInfo(2, 2, int.Parse(spriteName.Substring(2, 1)));
                }
                else if (spriteName.Substring(1, 1) == "2")
                {
                    reiw.SetInfo(1, 3, int.Parse(spriteName.Substring(2, 1)));
                }
                else if (spriteName.Substring(1, 1) == "5")
                {
                    reiw.SetInfo(2, 3, int.Parse(spriteName.Substring(2, 1)));
                }
            };
        }

        if (UIEventListener.Get(GameObject.Find("StrongAllButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("StrongAllButton")).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("3009#" + CharacterRoleID.ToString() + ";" + ClickIndex.ToString() + ";");
            };
        }

        if (UIEventListener.Get(GameObject.Find("StrongButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("StrongButton")).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("3005#" + CharacterRoleID.ToString() + ";" + ClickIndex.ToString() + ";");
            };
        }

        if (UIEventListener.Get(GameObject.Find("RefineButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("RefineButton")).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPanel("RefineWindow", false);
                RefineWindow rw = GameObject.Find("RefineWindow").GetComponent<RefineWindow>();
                rw.SetInfo(CharacterRoleID, ClickIndex);//, itemID);
            };
        }
    }

    public void Init(int _Index, int _CharacterRoleID, int _ItemCode, int _Grade, int _NowLevel)
    {
        //   Debug.LogError(1111111111);
        CharacterRoleID = _CharacterRoleID;
        ClickIndex = _Index;
        itemID = _ItemCode;
        SpriteEquipInfo.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = _ItemCode.ToString();
        // Debug.LogError(2222222222);
        SpriteEquipInfo.transform.Find("Star1").gameObject.SetActive(false);
        SpriteEquipInfo.transform.Find("Star2").gameObject.SetActive(false);
        SpriteEquipInfo.transform.Find("Star3").gameObject.SetActive(false);
        SpriteEquipInfo.transform.Find("Star4").gameObject.SetActive(false);
        SpriteEquipInfo.transform.Find("Star5").gameObject.SetActive(false);

        TextTranslator.ItemInfo item = TextTranslator.instance.GetItemByItemCode(_ItemCode);

        switch (item.itemGrade)
        {
            case 1:
                SpriteEquipInfo.GetComponent<UISprite>().spriteName = "Grade2";
                //SpriteEquipInfo.transform.Find("Star1").gameObject.SetActive(true);
                break;
            case 2:
                SpriteEquipInfo.GetComponent<UISprite>().spriteName = "Grade3";
                //SpriteEquipInfo.transform.Find("Star2").gameObject.SetActive(true);
                break;
            case 3:
                SpriteEquipInfo.GetComponent<UISprite>().spriteName = "Grade4";
                //SpriteEquipInfo.transform.Find("Star3").gameObject.SetActive(true);
                break;
            case 4:
                SpriteEquipInfo.GetComponent<UISprite>().spriteName = "Grade5";
                //SpriteEquipInfo.transform.Find("Star4").gameObject.SetActive(true);
                break;
            default:
                break;
        }
        //        Debug.LogError(33333333333);

        NameLabel.text = TextTranslator.instance.GetItemNameByItemCode(_ItemCode);
        MaxStrongLabel.text = "一键强化到" + CharacterRecorder.instance.level + "级";
        LevelLabel.text = _NowLevel.ToString();

        //LabelEquip.GetComponent<UILabel>().text = "0/1";
        //LabelStrongAll.GetComponent<UILabel>().text = "0/1";
        //LabelStrongAllMoney.GetComponent<UILabel>().text = "0/1";
        //LabelStrongMoney.GetComponent<UILabel>().text = "0/1";
        //Debug.LogError(_ItemCode + "      " + _Grade);
        //EquipClassUp ecp = TextTranslator.instance.GetEquipClassUpInfoByID(_ItemCode, _Grade+1);
        //Debug.LogError(ecp);
        //int haveNumber = 0;
        //foreach (var item in TextTranslator.instance.bagItemList)
        //{
        //    //Debug.LogError(item.itemCode + "  ???   " + item.itemGrade + " =====-----====   " + ecp.NeedItemID + "  ???  " + ecp.NeedItemClass);
        //    if (item.itemCode == ecp.NeedItemID && item.itemGrade == ecp.NeedItemClass)
        //    {
        //        haveNumber += item.itemCount;
        //    }
        //}
        //ProgressBar.value = (float)haveNumber / (float)ecp.NeedItemNumber;
        //Debug.LogError(ProgressBar.value + "    " + haveNumber + "       " + ecp.NeedItemNumber);
        //ProgressBar.transform.FindChild("LabelEquipCount").GetComponent<UILabel>().text = string.Format("{0}/{1}", haveNumber, ecp.NeedItemNumber);
        //if (ProgressBar.value >= 1)
        //{
        //    GameObject.Find("SpriteEquipAddButton").transform.FindChild("SpriteSprite").GetComponent<UISprite>().spriteName = "ui2_button8";
        //    UIEventListener.Get(GameObject.Find("SpriteEquipAddButton")).onClick = delegate(GameObject go)
        //    {
        //        NetworkHandler.instance.SendProcess("3004#" + CharacterRoleID + ";" + ClickIndex + ";");
        //        //UIManager.instance.OpenPanel("CombineWindow", false);
        //    };
        //}
        //else
        //{
        //    GameObject.Find("SpriteEquipAddButton").transform.FindChild("SpriteSprite").GetComponent<UISprite>().spriteName = "ui2_button7";
        //    UIEventListener.Get(GameObject.Find("SpriteEquipAddButton")).onClick = delegate(GameObject go)
        //    {

        //    };
        //}
        //Debug.LogError(4444444444444);
    }

    public void SetEquipInfo(int uHP, int uStrength, int uIntelligence, int uAgile, int uPhysicalDefense, int uArmor, int uMagicDefense, int uMagicResistance, int uPhysicalCrit, int uMagicCrit, int uAttackSpeed, int uMove,
        int uHPAdded, int uStrengthAdded, int uIntelligenceAdded, int uAgileAdded, int uPhysicalDefenseAdded, int uArmorAdded, int uMagicDefenseAdded, int uMagicResistanceAdded, int uPhysicalCritAdded, int uMagicCritAdded, int uAttackSpeedAdded, int uMoveAdded)
    {
        LabelEquip.GetComponent<UILabel>().text = "";
        if (uHP >= 0 && uHPAdded > 0)
        {
            LabelEquip.GetComponent<UILabel>().text += "生命+" + uHP.ToString() + "[0eb373](下一级+" + uHPAdded.ToString() + ")[-]\n";
        }
        if (uStrength >= 0 && uStrengthAdded > 0)
        {
            LabelEquip.GetComponent<UILabel>().text += "物攻+" + uStrength.ToString() + "[0eb373](下一级+" + uStrengthAdded.ToString() + ")[-]\n";
        }
        if (uIntelligence >= 0 && uIntelligenceAdded > 0)
        {
            LabelEquip.GetComponent<UILabel>().text += "特攻+" + uIntelligence.ToString() + "[0eb373](下一级+" + uIntelligenceAdded.ToString() + ")[-]\n";
        }
        if (uPhysicalDefense >= 0 && uPhysicalDefenseAdded > 0)
        {
            LabelEquip.GetComponent<UILabel>().text += "物防+" + uPhysicalDefense.ToString() + "[0eb373](下一级+" + uPhysicalDefenseAdded.ToString() + ")[-]\n";
        }
        if (uMagicDefense >= 0 && uMagicDefenseAdded > 0)
        {
            LabelEquip.GetComponent<UILabel>().text += "特防+" + uMagicDefense.ToString() + "[0eb373](下一级+" + uMagicDefenseAdded.ToString() + ")[-]\n";
        }
        if (uPhysicalCrit >= 0 && uPhysicalCritAdded > 0)
        {
            LabelEquip.GetComponent<UILabel>().text += "暴击+" + uPhysicalCrit.ToString() + "[0eb373](下一级+" + uPhysicalCritAdded.ToString() + ")[-]\n";
        }
        if (uAttackSpeed >= 0 && uAttackSpeedAdded > 0)
        {
            LabelEquip.GetComponent<UILabel>().text += "攻速+" + uAttackSpeed.ToString() + "[0eb373](下一级+" + uAttackSpeedAdded.ToString() + ")[-]\n";
        }
    }

    private int SetStrongMonet(int Number, int Type)
    {
        int money = 0;
        if (Type == 1)
        {
            money = Mathf.Max(Number - 10, 0) * Mathf.Max(Number - 10, 0) * 10 + Mathf.Max(Number - 20, 0) * Mathf.Max(Number - 20, 0) * 50 + Mathf.Max(Number - 30, 0) * Mathf.Max(Number - 30, 0) * 100 + Number * 100;
            return money;
        }
        if (Type == 2)
        {
            for (int i = Number; i <= CharacterRecorder.instance.level; i++)
            {
                money += (Mathf.Max(i - 10, 0) * Mathf.Max(i - 10, 0) * 10 + Mathf.Max(i - 20, 0) * Mathf.Max(i - 20, 0) * 50 + Mathf.Max(i - 30, 0) * Mathf.Max(i - 30, 0) * 100 + i * 100);
                //Debug.LogError(money);
            }
            return money;
        }
        return money;
    }


}
