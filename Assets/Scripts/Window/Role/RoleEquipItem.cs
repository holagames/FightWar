using UnityEngine;
using System.Collections;

public class RoleEquipItem : MonoBehaviour
{

    int ItemCode;
    int ItemCount;
    int ItemGrade;
    int ItemEquipID;
    int Index;

    RoleWindow rw;
    // Use this for initialization
    void Start()
    {
        rw = GameObject.Find("RoleWindow").GetComponent<RoleWindow>();

        if (UIEventListener.Get(gameObject).onClick == null)
        {
            UIEventListener.Get(gameObject).onClick += delegate(GameObject go)
            {
                Debug.Log("BBB" + ItemCode);
                //GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetEquip(ItemCode, ItemGrade, Index);
                NetworkHandler.instance.SendProcess("3007#" + rw.CharacterRoleID + ";" + ItemCode + ";" + ItemEquipID + ";" + rw.ClickIndex + ";");

                UIManager.instance.BackUI();
            };
        }
    }

    public void Init(int _ItemCode, int _ItemGrade, int _ItemCount, int _ItemEquipID, int _Index,Hero.EquipInfo equipInfo=null)
    {
        ItemCode = _ItemCode;
        ItemCount = _ItemCount;
        ItemGrade = _ItemGrade;
        ItemEquipID = _ItemEquipID;
        Index = _Index;
        int level = 1;
        int classLevel=0;
        Item mItem = TextTranslator.instance.GetItemByID(_ItemCode);
        TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(_ItemCode);
        if (mItem != null)
        {
            level = mItem.itemLevel;
            classLevel=mItem.itemClassLevel;
        }
        else
        {
            level = equipInfo.equipLevel;
        }

        gameObject.transform.Find("SpriteGrade").Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = ItemCode.ToString();
        gameObject.transform.Find("SpriteGrade").gameObject.GetComponent<UISprite>().spriteName = "Grade" + (_ItemGrade+1).ToString();
        gameObject.transform.Find("LabelName").gameObject.GetComponent<UILabel>().text = mitemInfo.itemName;
        gameObject.transform.Find("LabelDesc").gameObject.GetComponent<UILabel>().text = "";

        EquipClassUp edi = TextTranslator.instance.GetEquipClassUpInfoByID(ItemCode, ItemGrade);
        EquipDetailInfo equipDel = TextTranslator.instance.GetEquipInfoByID(_ItemCode);
        if (edi.ADAttack != 0)
        {
            gameObject.transform.Find("LabelDesc").gameObject.GetComponent<UILabel>().text += string.Format("物攻+{0}\n", edi.ADAttack + (level) * edi.add_ADAttack+classLevel*equipDel.ADAttack);
        }
        if (edi.APAttack != 0)
        {
            gameObject.transform.Find("LabelDesc").gameObject.GetComponent<UILabel>().text += string.Format("特攻+{0}\n", edi.APAttack + (level) * edi.add_APAttack+classLevel*equipDel.APAttack);
        }
        if (edi.ADDenfance != 0)
        {
            gameObject.transform.Find("LabelDesc").gameObject.GetComponent<UILabel>().text += string.Format("物防+{0}\n", edi.ADDenfance + (level) * edi.add_ADDenfance+classLevel*equipDel.ADDenfance);
        }
        if (edi.APDenfance != 0)
        {
            gameObject.transform.Find("LabelDesc").gameObject.GetComponent<UILabel>().text += string.Format("特防+{0}\n", edi.APDenfance + (level) * edi.add_APDenfance+classLevel*equipDel.APDenfance);
        }
        if (edi.HP != 0)
        {
            gameObject.transform.Find("LabelDesc").gameObject.GetComponent<UILabel>().text += string.Format("生命+{0}", edi.HP + (level) * edi.add_HP+classLevel*equipDel.HP);
        }
    }
}
