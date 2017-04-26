using UnityEngine;
using System.Collections;
using Holagames;

public class VipShopItem : MonoBehaviour
{
    public UILabel LabelMoney;
    public UISprite SpriteGroom;//推荐
    public UISprite SpriteDiamond;//物品图片
    public UILabel LabelExplain;//额外说明
    public GameObject SellOut;
    public UILabel LabelPrice;
    public UILabel LabelDay;

    public int echargeId;
    private int Money;
    void Start()
    {
        SetItemInfo();
        if (UIEventListener.Get(gameObject).onClick == null)
        {
            UIEventListener.Get(gameObject).onClick += delegate(GameObject go)
            {

                //UIManager.instance.OpenPromptWindow("暂未开通支付！", PromptWindow.PromptType.Hint, null, null);
                SetItemInfo();
                Debug.Log("ceshi" + echargeId + " " + Money);


                TextTranslator.instance.Recharge(echargeId);
            };
        }
    }

    public void SetItemInfo()
    {
        Exchange mItem = TextTranslator.instance.GetExchangeById(echargeId);

        if (mItem != null)
        {
            if (echargeId == 7)
            {
                if (CharacterRecorder.instance.MonthCardDay > 0)
                {
                    LabelDay.text = "剩余[ff0000]" + (31 - CharacterRecorder.instance.MonthCardDay).ToString() + "[-]日";
                }
            }
            if (mItem.firstDiamond > 0 && mItem.isfristDiamond == false)
            {
                LabelExplain.text = string.Format("首次购买额外赠送{0}钻石", mItem.firstDiamond);
                LabelExplain.gameObject.SetActive(true);
            }
            //else if (mItem.firstDiamond > 0 && mItem.isfristDiamond) 
            //{
            //    LabelExplain.gameObject.SetActive(false);
            //}
            else if (mItem.extDiamond > 0 && mItem.isfristDiamond)
            {
                LabelExplain.text = string.Format("购买额外赠送{0}钻石", mItem.extDiamond);
                LabelExplain.gameObject.SetActive(true);
            }
            else if (mItem.type == 2)
            {
                if (echargeId == 7) 
                {
                    LabelExplain.text = string.Format("每日额外赠送100钻石", mItem.diamond);
                }
                else if (echargeId == 8) 
                {
                    LabelExplain.text = string.Format("每日额外赠送200钻石", mItem.diamond);
                }
                //LabelExplain.text = string.Format("每日额外赠送{0}钻石", mItem.diamond);
                LabelExplain.gameObject.SetActive(true);
            }
            else
            {
                LabelExplain.gameObject.SetActive(false);
            }

            LabelMoney.text = "￥" + mItem.cash.ToString();

            Money = mItem.cash;
        }
    }



}
