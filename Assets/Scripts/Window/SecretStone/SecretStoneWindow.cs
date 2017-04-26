using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SecretStoneWindow : MonoBehaviour 
{
    public static Hero mHero;
    public List<GameObject> listStone = new List<GameObject>();
    public GameObject selectStonePart;
    public GameObject secretStoneInfoPart;
    public static RareTreasureOpen curRareStone;
    public int curRareStonePos = 1;
    private int curStoneOpenState = 0;// 0 - 锁， 1- 开，未装备  2 - 已装备
	// Use this for initialization
	void Start () 
    {
        UIManager.instance.CountSystem(UIManager.Systems.秘宝);
        UIManager.instance.UpdateSystems(UIManager.Systems.秘宝);
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    public void SetSecretStoneWindow(Hero mCurHero, int _position, Hero.EquipInfo mCurEquipInfo)
    {
        mHero = mCurHero;
        NetworkHandler.instance.SendProcess(string.Format("3101#{0};", mCurHero.characterRoleID));
    }

    public void InitEquipItemOpenState()
    {
        curRareStone = TextTranslator.instance.RareTreasureOpenDic[curRareStonePos];
        for (int i = 0; i < listStone.Count; i++)
        {
            UIEventListener.Get(listStone[i]).onClick = ClickListEquip;
            GameObject _Lock = listStone[i].transform.FindChild("Lock").gameObject;
            Transform iconTrans = listStone[i].transform.FindChild("Icon");
            Transform levelLabelTrans = listStone[i].transform.FindChild("Label");
            //Debug.LogError(TextTranslator.instance.RareTreasureOpenDic[i + 1].state);
            switch (TextTranslator.instance.RareTreasureOpenDic[i + 1].state)
            {
                case 0: listStone[i].GetComponent<UIToggle>().enabled = false;
                    _Lock.SetActive(true);
                    iconTrans.gameObject.SetActive(false);
                    levelLabelTrans.gameObject.SetActive(false);
                    listStone[i].GetComponent<UISprite>().spriteName = "Grade1";
                    break;
                case 1:
                    iconTrans.gameObject.SetActive(true);
                    iconTrans.GetComponent<UISprite>().spriteName = "add";
                    _Lock.SetActive(false);
                    levelLabelTrans.gameObject.SetActive(false);
                    listStone[i].GetComponent<UISprite>().spriteName = "Grade1";
                    break;
                case 2:
                    iconTrans.gameObject.SetActive(true);
                    _Lock.SetActive(false);
                    iconTrans.GetComponent<UISprite>().spriteName = TextTranslator.instance.RareTreasureOpenDic[i + 1].stoneId.ToString();
                    listStone[i].GetComponent<UISprite>().spriteName = GetItemFrameName(TextTranslator.instance.RareTreasureOpenDic[i + 1].stoneId);
                    levelLabelTrans.gameObject.SetActive(true);
                    levelLabelTrans.GetComponent<UILabel>().text = TextTranslator.instance.RareTreasureOpenDic[i + 1].stoneLevel.ToString();
                    break;
            }
        }
        ClickListEquip(curRareStonePos);
    }
    string GetItemFrameName(int _itemId)
    {
        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(_itemId);
        //Debug.LogError("_itemId.." + _itemId + "..Item.." + _ItemInfo);
        return "Grade" + _ItemInfo.itemGrade.ToString();
    }
    void ClickListEquip(GameObject go)
    {
        if (go != null)
        {
            switch (go.name)
            {
                case "Equip1": ClickListEquip(1);
                    break;
                case "Equip2": ClickListEquip(2);
                    break;
                case "Equip3": ClickListEquip(3);
                    break;
                case "Equip4": ClickListEquip(4);
                    break;
                case "Equip5": ClickListEquip(5);
                    break;
                case "Equip6": ClickListEquip(6);
                    break;
                case "Equip7": ClickListEquip(7);
                    break;
            }
        }
    }
    void ClickListEquip(int _position)
    {
        switch (TextTranslator.instance.RareTreasureOpenDic[_position].state)
        {
            case 0:
                UIManager.instance.OpenPromptWindow(string.Format("{0}级开放秘宝", TextTranslator.instance.RareTreasureOpenDic[_position].openLevel), PromptWindow.PromptType.Hint, null, null);
                break;
            case 1:
                /*if (curStonePos == _position)
                {
                    return;
                }*/
               // curRareStone.posId = _position;
                curRareStonePos = _position;
                curRareStone = TextTranslator.instance.RareTreasureOpenDic[_position];
                //Debug.LogError(curRareStone.posId);
               /* for (int i = 0; i < listStone.Count;i++ )
                {
                    if (i + 1 == _position)
                    {
                        listStone[i].GetComponent<UIToggle>().value = true;
                    }
                    else
                    {
                        listStone[i].GetComponent<UIToggle>().value = false;
                    }
                }*/
                selectStonePart.SetActive(true);
                secretStoneInfoPart.SetActive(false);
                selectStonePart.GetComponent<SelectStonePart>().SetSelectStonePart(0);
                
                break;
            case 2:
               // curRareStone.posId = _position;
                curRareStonePos = _position;
                curRareStone = TextTranslator.instance.RareTreasureOpenDic[_position];
                //Debug.LogError(curRareStone.posId);
                selectStonePart.SetActive(false);
                secretStoneInfoPart.SetActive(true);
                secretStoneInfoPart.GetComponent<StoneInfoPart>().SetStoneInfoPart();
                break;
        }
    }
}
