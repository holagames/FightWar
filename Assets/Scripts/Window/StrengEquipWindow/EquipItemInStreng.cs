using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EquipItemInStreng : MonoBehaviour
{
    public GameObject gride;
    public UISprite pinJieSprite;
    public bool IsUpJianTouActive = false;
    private Hero.EquipInfo mEquipInfo;

    private List<GameObject> pinjieLst = new List<GameObject>();

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    /*  public void SetEquipItem(Hero mHero,Hero.EquipInfo _OneEquipInfo)
      {
          if (_OneEquipInfo.equipCode != 0)
          {
              mEquipInfo = _OneEquipInfo;
              string frameName = "";
              //Debug.LogError("?????????????");
              TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_OneEquipInfo.equipCode);
              frameName = string.Format("Grade{0}", mItemInfo.itemGrade);
              this.GetComponent<UISprite>().spriteName = frameName;
              this.transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName = _OneEquipInfo.equipCode.ToString();
              this.transform.Find("Label").gameObject.GetComponent<UILabel>().text = _OneEquipInfo.equipLevel.ToString();
              if (_OneEquipInfo.equipPosition < 5)
              {
                  SetEquipItemInStrengh(mHero, _OneEquipInfo.equipPosition);
                  //Debug.LogError("?????????????");
                  SetEquipStrenghRedPoint(_OneEquipInfo);
              }
          }
          else
          {
              this.GetComponent<UISprite>().spriteName = "Grade1";
              this.transform.Find("Icon").gameObject.GetComponent<UISprite>().spriteName = "";
              this.transform.Find("Label").gameObject.GetComponent<UILabel>().text = "";
          }
      }*/
    public void SetEquipItemInStrengh(Hero mHero, int _position)
    {
        DestroyGride();
        int mEquipCode = mHero.equipList[_position - 1].equipCode;
        TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(mEquipCode);
        //Debug.LogError("NumAdd...." + GetNumAdd(mItemInfo.itemGrade, mHero.equipList[_position - 1].equipColorNum));
        for (int i = 0; i < GetNumAdd(mItemInfo.itemGrade, mHero.equipList[_position - 1].equipColorNum); i++)
        {
            GameObject obj = NGUITools.AddChild(gride, pinJieSprite.gameObject);
            obj.SetActive(true);
            obj.GetComponent<UISprite>().spriteName = string.Format("zbkuang{0}", mItemInfo.itemGrade);
            pinjieLst.Add(obj);
        }
        UIGrid _UIGrid = gride.GetComponent<UIGrid>();
        _UIGrid.sorting = UIGrid.Sorting.Horizontal;
        _UIGrid.pivot = UIWidget.Pivot.Center;
        //if (_UIGrid.animateSmoothly != true)
        //{
        //    _UIGrid.animateSmoothly = true;
        //}
        _UIGrid.Reposition();
    }
    void SetEquipStrenghRedPoint(Hero.EquipInfo _OneEquipInfo)
    {
        GameObject _RedPoint = this.transform.FindChild("RedPoint").gameObject;
        GameObject _UpJianTou = this.transform.FindChild("UpJianTou").gameObject;
        //Debug.LogError(IsAdvanceState(_OneEquipInfo.equipLevel, _OneEquipInfo.equipColorNum));
        if (IsAdvanceState(_OneEquipInfo.equipLevel, _OneEquipInfo.equipColorNum))
        {
            _RedPoint.SetActive(true);
            _UpJianTou.SetActive(false);
        }
        else
        {
            _RedPoint.SetActive(false);
            if (_OneEquipInfo.equipLevel < CharacterRecorder.instance.level)
            {
                _UpJianTou.SetActive(true);
            }
            else
            {
                _UpJianTou.SetActive(false);
            }
        }
    }
    bool IsAdvanceState(int _EquipLevel, int _EquipColorNum)
    {
        if (_EquipColorNum * 5 == _EquipLevel)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void DestroyGride()
    {
        // Debug.LogError("pinjieLst:" + pinjieLst.Count);
        for (int i = 0; i < pinjieLst.Count; i++)
        {
            DestroyImmediate(pinjieLst[i]);
        }
        pinjieLst.Clear();
        //for (int i = gride.transform.childCount - 1; i >= 0; i--)
        //{
        //    DestroyImmediate(gride.transform.GetChild(i).gameObject);
        //}
    }
    int GetNumAdd(int _itemGrade, int _colorNum)
    {
        int addNum = 0;
        switch (_itemGrade)
        {
            case 1:
                switch (_colorNum)
                {
                    case 0: addNum = 0; break;
                    case 1: addNum = 0; break;
                    case 2: addNum = 1; break;
                }
                break;
            case 2:
                switch (_colorNum)
                {
                    case 3: addNum = 0; break;
                    case 4: addNum = 1; break;
                    case 5: addNum = 2; break;
                }
                break;
            case 3:
                switch (_colorNum)
                {
                    case 6: addNum = 0; break;
                    case 7: addNum = 1; break;
                    case 8: addNum = 2; break;
                    case 9: addNum = 3; break;
                }
                break;
            case 4:
                switch (_colorNum)
                {
                    case 10: addNum = 0; break;
                    case 11: addNum = 1; break;
                    case 12: addNum = 2; break;
                    case 13: addNum = 3; break;
                    case 14: addNum = 4; break;
                }
                break;
            case 5:
                switch (_colorNum)
                {
                    case 15: addNum = 0; break;
                    case 16: addNum = 1; break;
                    case 17: addNum = 2; break;
                    case 18: addNum = 3; break;
                    case 19: addNum = 4; break;
                    case 20: addNum = 5; break;
                }
                break;
            case 6:
                switch (_colorNum)
                {
                    case 20: addNum = 0; break;
                    case 21: addNum = 1; break;
                    case 22: addNum = 2; break;
                }
                break;
        }
        return addNum;
    }
}
