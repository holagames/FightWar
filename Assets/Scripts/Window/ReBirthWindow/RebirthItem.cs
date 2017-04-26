using UnityEngine;
using System.Collections;

public class RebirthItem : MonoBehaviour
{
    public UISprite bgSprite;
    public UISprite resSprite;
    public UILabel resNameLabel;
    public UILabel levelLabel;

    public void SetRebirthItemInfo(int itemCode, int itemCount,string nameType)
    {
        TextTranslator.ItemInfo item = TextTranslator.instance.GetItemByItemCode(itemCode);
        //Item item = TextTranslator.instance.GetItemByID(itemCode);
        if (item != null)
        {
            // TextTranslator.ItemInfo item = TextTranslator.instance.GetItemByItemCode(itemCode);
            //设置背景框
            SetColor(bgSprite.gameObject, item.itemGrade);
            //设置背景图片
            resSprite.spriteName = itemCode.ToString();
            switch (nameType)
            {
                case "RebirthSuccess":
                    //设置物品的名称
                    resNameLabel.text = string.Format("[c1822a]{0}", item.itemName);
                    break;
                case "RebirthGet":
                    //设置物品的名称
                    resNameLabel.text = string.Format("[5b7597]{0}", item.itemName);
                    break;
                default:
                    //设置物品的名称
                    resNameLabel.text = string.Format("{0}", item.itemName);
                    break;
            }
           
            //设置返还的数量
            levelLabel.text = itemCount.ToString();
            TextTranslator.instance.ItemDescription(this.gameObject, itemCode, 0);
            StartCoroutine(OnEquipDataUpEffect(levelLabel));
        }
        else
        {
            Debug.LogError("未找到的Item物品的ID号： "+itemCode);
        }
    }
    /// <summary>
    /// 在GateGiftWindow面板下的设置Item信息
    /// </summary>
    /// <param name="itemCode">itemID号</param>
    /// <param name="itemCount">数量</param>
    public void SetRebirthItemInfo(int itemCode, int itemCount)
    {
        TextTranslator.ItemInfo item = TextTranslator.instance.GetItemByItemCode(itemCode);
        //Item item = TextTranslator.instance.GetItemByID(itemCode);
        if (item != null)
        {
            // TextTranslator.ItemInfo item = TextTranslator.instance.GetItemByItemCode(itemCode);
            //设置背景框
            SetColor(bgSprite.gameObject, item.itemGrade);
            //设置背景图片
            resSprite.spriteName = itemCode.ToString();
            resNameLabel.gameObject.SetActive(false);
            //设置返还的数量
            levelLabel.text = itemCount.ToString();
            TextTranslator.instance.ItemDescription(this.gameObject, itemCode, 0);
        }
        else
        {
            Debug.LogError("未找到的Item物品的ID号： " + itemCode);
        }
    }
    void SetColor(GameObject go, int color)
    {
        switch (color)
        {
            case 1:
                go.GetComponent<UISprite>().spriteName = "Grade1";
                break;
            case 2:
                go.GetComponent<UISprite>().spriteName = "Grade2";
                break;
            case 3:
                go.GetComponent<UISprite>().spriteName = "Grade3";
                break;
            case 4:
                go.GetComponent<UISprite>().spriteName = "Grade4";
                break;
            case 5:
                go.GetComponent<UISprite>().spriteName = "Grade5";
                break;
            case 6:
                go.GetComponent<UISprite>().spriteName = "Grade6";
                break;
            case 7:
                go.GetComponent<UISprite>().spriteName = "Grade7";
                break;
        }
    }

    /// <summary>
    /// 变大醒目提醒
    /// </summary>
    /// <param name="_myLabel">需要变大的UILabel</param>
    /// <returns>协程</returns>
    public IEnumerator OnEquipDataUpEffect(UILabel _myLabel)
    {
        Color myColor = _myLabel.color;
        _myLabel.color = new Color(62 / 255f, 232 / 255f, 23 / 255f, 255 / 255f);
        TweenScale _TweenScale = _myLabel.gameObject.GetComponent<TweenScale>();
        if (_TweenScale == null)
        {
            _TweenScale = _myLabel.gameObject.AddComponent<TweenScale>();
        }
        _TweenScale.from = Vector3.one;
        _TweenScale.to = 1.2f * Vector3.one;
        _TweenScale.duration = 0.3f;
        _TweenScale.PlayForward();
        yield return new WaitForSeconds(0.3f);
        _TweenScale.duration = 0.1f;
        _TweenScale.PlayReverse();
        //Color backColor = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        _myLabel.color = myColor;
    }
}
