using UnityEngine;
using System.Collections;

public class FoundationItem : MonoBehaviour
{
    // Use this for initialization
    public UISprite ItemGrade;
    public UISprite Icon;
    public GameObject Framge;
    public UILabel Des;
    public GameObject GetedObj;
    public UILabel IconNumber;
    public int Type;
    public UIAtlas ItemAtlas;
    public UIAtlas HeroAtlas;
    public int Index;
    public int BuyNumber;
    public GameObject RedPoint;
    void Start()
    {

        UIEventListener.Get(gameObject).onClick = delegate(GameObject go)
        {
            if (Type == 1)
            {
                if (TextTranslator.instance.GetActivityGrowthFundDicByID(Index).Condition <= CharacterRecorder.instance.level)
                {
                    NetworkHandler.instance.SendProcess("9609#" + Index + ";");
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("等级不足" + TextTranslator.instance.GetActivityGrowthFundDicByID(Index).Condition + "级", PromptWindow.PromptType.Hint, null, null);
                }


            }
            else
            {
                if (TextTranslator.instance.GetActivityGrowthFundDicByID(Index).Condition <= BuyNumber)
                {
                    NetworkHandler.instance.SendProcess("9611#" + Index + ";");
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("人数不足" + TextTranslator.instance.GetActivityGrowthFundDicByID(Index).Condition, PromptWindow.PromptType.Hint, null, null);
                }

            }
        };
    }

    public void SetInfo(int _ID, int _Number, string _Des, int _itemtype, int _Index, int _BuyNumber, int OpenType)
    {
        Des.text = _Des;
        BuyNumber = _BuyNumber;
        IconNumber.text = _Number.ToString();
        Type = OpenType;
        Index = _Index;
        if (_itemtype == 1)
        {
            GetedObj.SetActive(true);
        }
        else
        {

            GetedObj.SetActive(false);
        }
        Icon.spriteName = TextTranslator.instance.GetItemByItemCode(_ID).picID.ToString();
        //if (TextTranslator.instance.GetItemByItemCode(_ID).Stact == 9999)
        //{
        //    Framge.SetActive(true);
        //}
        //else
        //{
        //    Framge.SetActive(false);
        //}
        if (60000 < _ID && _ID < 70000)
        {
            Icon.atlas = HeroAtlas;
        }
        else if (70000 < _ID && _ID < 79999)
        {
            Icon.atlas = HeroAtlas;
        }
        else
        {
            Icon.atlas = ItemAtlas;
        }
        if (70000 < _ID && _ID < 79999)
        {
            Framge.SetActive(true);
        }
        else if (70000 < _ID && _ID < 79999)
        {
            Framge.SetActive(true);
        }
        else
        {
            Framge.SetActive(false);
        }
        ItemGrade.spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(_ID).itemGrade.ToString();
        TextTranslator.instance.ItemDescription(this.gameObject, _ID, 0);
        if (Type == 1)
        {
            if (TextTranslator.instance.GetActivityGrowthFundDicByID(Index).Condition <= CharacterRecorder.instance.level && _itemtype != 1 && CharacterRecorder.instance.isBuyTheFoundation)
            {
                RedPoint.SetActive(true);
            }
            else
            {
                RedPoint.SetActive(false);
            }
        }
        else
        {
            if (TextTranslator.instance.GetActivityGrowthFundDicByID(Index).Condition <= BuyNumber && _itemtype != 1)
            {
                RedPoint.SetActive(true);
            }
            else
            {
                RedPoint.SetActive(false);
            }
        }
    }

}
