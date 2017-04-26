using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SelectItemCodeAndIndex
{
    public int code;
    public int idex;
    public SelectItemCodeAndIndex(int code,int index)
    {
        this.code = code;
        this.idex = index;
    }
}
public class EquipSelectItem : MonoBehaviour 
{
    public UIToggle ToggleChecked;
    public UILabel LabelName;
    public UILabel LabelExp;
    public UISprite SpriteEquip;
    public UISprite SpriteGrade;
    private Item oneItem;
    private SelectItemCodeAndIndex _SelectItemCodeAndIndex;
    private int index;

	// Use this for initialization
	void Start () 
    {

        if (UIEventListener.Get(ToggleChecked.gameObject).onClick == null)
        {
            UIEventListener.Get(ToggleChecked.gameObject).onClick += delegate(GameObject go)
            {
                GameObject _EquipSelectWindow = GameObject.Find("EquipSelectWindow");
                GameObject _EquipSelectWindowForStone = GameObject.Find("EquipSelectWindowForStone");
                if (_EquipSelectWindow != null)
                {
                    EquipSelectWindow esw = _EquipSelectWindow.GetComponent<EquipSelectWindow>();
                    if (ToggleChecked.value)
                    {
                        if (esw.listSelectItemCodeAndIndexNowAdd.Count < 5)
                        {
							if(_SelectItemCodeAndIndex == null)
							{
                                _SelectItemCodeAndIndex = new SelectItemCodeAndIndex(oneItem.itemCode, index);
                                esw.listSelectItemCodeAndIndexNowAdd.Add(_SelectItemCodeAndIndex);
							}
							else 
							{
								esw.listSelectItemCodeAndIndexNowAdd.Add(_SelectItemCodeAndIndex);
							}
						}
                        else
                        {
                            ToggleChecked.value = false;
                            UIManager.instance.OpenPromptWindow("最多可选5个", 11, false, PromptWindow.PromptType.Hint, null, null);
                        }
                    }
                    else
                    {
						esw.listSelectItemCodeAndIndexNowAdd.Remove(_SelectItemCodeAndIndex);
                    }
                    //旧的分母显示总数
                    // esw.LabelCount.text = esw.ClickCount.ToString() + "/" + esw.TotoleCount.ToString();
                    esw.LabelCount.text = esw.listSelectItemCodeAndIndexNowAdd.Count.ToString() + "/5";
                }
                else if (_EquipSelectWindowForStone != null)
                {
                    EquipSelectWindowForStone esw = _EquipSelectWindowForStone.GetComponent<EquipSelectWindowForStone>();
                    if (ToggleChecked.value)
                    {
                        if (esw.listSelectItemCodeAndIndexNowAdd.Count < 5)
                        {
                            if (_SelectItemCodeAndIndex == null)
                            {
                                _SelectItemCodeAndIndex = new SelectItemCodeAndIndex(oneItem.itemCode, index);
                                esw.listSelectItemCodeAndIndexNowAdd.Add(_SelectItemCodeAndIndex);
                            }
                            else
                            {
                                esw.listSelectItemCodeAndIndexNowAdd.Add(_SelectItemCodeAndIndex);
                            }
                        }
                        else
                        {
                            ToggleChecked.value = false;
                            UIManager.instance.OpenPromptWindow("最多可选5个", 11, false, PromptWindow.PromptType.Hint, null, null);
                        }
                    }
                    else
                    {
                        esw.listSelectItemCodeAndIndexNowAdd.Remove(_SelectItemCodeAndIndex);
                    }
                    //旧的分母显示总数
                    // esw.LabelCount.text = esw.ClickCount.ToString() + "/" + esw.TotoleCount.ToString();
                    esw.LabelCount.text = esw.listSelectItemCodeAndIndexNowAdd.Count.ToString() + "/5";
                }
            };
        }
	}
	public void Init(Item _oneItem,int index,List<SelectItemCodeAndIndex> listSelectItemCodeAndIndex)
	{
		oneItem = _oneItem;
		this.index = index;
		LabelName.text = _oneItem.itemName;
		LabelExp.text = "EXP + " + _oneItem.feedExp;
		SpriteEquip.spriteName = _oneItem.itemCode.ToString();
		SpriteGrade.spriteName = "Grade" + _oneItem.itemGrade.ToString();
		bool myValue = GetToggleValue(_oneItem.itemCode,index,_oneItem,listSelectItemCodeAndIndex);
        if (myValue)
        {
            ToggleChecked.value = true;
        }
        else
        {
            ToggleChecked.value = false;
        }
		ToggleChecked.gameObject.layer = 11;
		foreach (Component c in ToggleChecked.GetComponentsInChildren(typeof(Transform), true))
		{
			c.gameObject.layer = 11;
		}
		return;
	}
    bool GetToggleValue(int code, int index, Item _item, List<SelectItemCodeAndIndex> listSelectItemCodeAndIndex)
    {

        for (int i = 0; i < listSelectItemCodeAndIndex.Count; i++)
        {
            if (listSelectItemCodeAndIndex[i].code == code && listSelectItemCodeAndIndex[i].idex == index)
            {
                oneItem = _item;
                _SelectItemCodeAndIndex = listSelectItemCodeAndIndex[i];
                return true;
            }
        }
        return false;
    }

    #region 没用到
    bool GetToggleValue(List<Item> _selectList, int index)
    {
        if (_selectList.Contains(oneItem) && this.index == index)
        {
            return true;
        }
        return false;
    }
    public void Init(Item _oneItem,List<Item> _selectList)
    {
        oneItem = _oneItem;
        this.index = index;
        LabelName.text = _oneItem.itemName;
        LabelExp.text = "EXP + " + _oneItem.feedExp;
        SpriteEquip.spriteName = _oneItem.itemCode.ToString();
        SpriteGrade.spriteName = "Grade" + _oneItem.itemGrade.ToString();
		Debug.Log ("select list =====>" + _selectList.Count);
		for (int i=0; i<_selectList.Count; i++) {
			Debug.Log("selct list =====>"+_selectList[i].GetHashCode() +"one item ====>"+_oneItem.GetHashCode());
		}
        if (_selectList.Contains(_oneItem))
        
        {
            ToggleChecked.value = true;
        }
        else
        {
            ToggleChecked.value = false;
        }
      /*  bool myValue = GetToggleValue(_oneItem.itemCode, index, _oneItem);
        if (myValue)
        {
            ToggleChecked.value = true;
        }
        else
        {
            ToggleChecked.value = false;
        }*/
        ToggleChecked.gameObject.layer = 11;
        foreach (Component c in ToggleChecked.GetComponentsInChildren(typeof(Transform), true))
        {
            c.gameObject.layer = 11;
        }
        return;
    }
    #endregion
    
}
