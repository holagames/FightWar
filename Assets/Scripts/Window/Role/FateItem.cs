using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FateItem : MonoBehaviour 
{
    [SerializeField]
    private UILabel fateNameLabel;
    [SerializeField]
    private UILabel numAddLabel;
    [SerializeField]
    private UILabel desLabel;

    [SerializeField]
    private GameObject itemIconWithName;
    [SerializeField]
    private GameObject uiGride;
    private RoleFate myRoleFate;
    private bool isFateOpen;
	// Use this for initialization
	void Start () 
    {
	
	}
    public void SetFateItem(int _HeroId,RoleFate _RoleFate)
    {
        myRoleFate = _RoleFate;
        SetFateName(_HeroId);
  //      fateNameLabel.text = _RoleFate.Name;
        //desLabel.text = _RoleFate.Des;
        //numAddLabel.text = string.Format("[249bd2]生命力[3ee817]+{0}%  [-]攻击力[3ee817]+{1}%", _RoleFate.Hp * 100, _RoleFate.Atk * 100);
        numAddLabel.text = "";
        if (_RoleFate.Hp > 0)
        {
            numAddLabel.text = string.Format("[249bd2]生命增加[3ee817]+{0}%", _RoleFate.Hp * 100);
        }
        if (_RoleFate.Atk > 0)
        {
            numAddLabel.text += string.Format("[249bd2]攻击增加[3ee817]+{0}%", _RoleFate.Atk * 100);
        }
        for (int i = 0; i < _RoleFate.FateIDList.Count;i++ )
        {
            GameObject obj = NGUITools.AddChild(uiGride, itemIconWithName) as GameObject;
            obj.GetComponent<ItemIconWithName>().SetItemIconWithName(isFateOpen,_RoleFate.FateIDList[i]);
        }
        if (GameObject.Find("FateWindow") != null)
        {
            desLabel.text = GetDesStr(_RoleFate.FateIDList) + numAddLabel.text;
        }
        uiGride.GetComponent<UIGrid>().repositionNow = true;
    }
    string GetDesStr(List<int> _itemIdList)
    {
        string _string = "同时拥有 ";
        for (int i = 0; i < _itemIdList.Count;i++ )
        {
            HeroInfo _HeroInfo = TextTranslator.instance.GetHeroInfoByHeroID(_itemIdList[i]);
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_itemIdList[i]);
            if (_HeroInfo != null)
            {
                _string += string.Format("{0},",_HeroInfo.heroName);
            }
            else if (mItemInfo != null)
            {
                _string += string.Format("{0},", mItemInfo.itemName);
            }
        }
        return _string;
    }
    //设置缘分名字
    void SetFateName(int cardID)
    {
        BetterList<RoleFate> MyRoleFateList = TextTranslator.instance.GetMyRoleFateListByRoleID(cardID);
        TextTranslator.instance.MyRoleFateList = MyRoleFateList;
        //Debug.LogError("..roleID.." + roleID + "....MyRoleFateList..." + MyRoleFateList.size);
        string clolrStr = "";
        if (CharacterRecorder.instance.ListRoleFateId.Contains(myRoleFate.RoleFateID))
        {
            isFateOpen = true;
            clolrStr = "[ff8c04]";
        }
        else
        {
            isFateOpen = false;
            clolrStr = "[919090]";
        }
        fateNameLabel.color = Color.white;
        fateNameLabel.text = clolrStr + myRoleFate.Name;
        if(GameObject.Find("CardDetailInfoWindow")!= null)
        {
            fateNameLabel.text = myRoleFate.Name;
        }
    }
}
