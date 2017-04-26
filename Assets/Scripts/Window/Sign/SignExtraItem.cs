using UnityEngine;
using System.Collections;

public class SignExtraItem : MonoBehaviour
{
    public static int curClickSignId;
    public UIAtlas RoleAtlas;
    //[SerializeField]
    private GameObject vipObj;
    //[SerializeField]
    [SerializeField]
    private GameObject CanGetSignExtraAwardEffect;
    private UILabel vipNumLabel;
    [SerializeField]
    private UILabel countLabel;
    [SerializeField]
    private UISprite icon;
    [SerializeField]
    private GameObject suiPian;
    [SerializeField]
    private UISprite frame;
    //[SerializeField]
    private GameObject frameSelect;
    //[SerializeField]
    private UISprite bigFrame;
    private int signItemState;
    private int signID;
    private SignExtraItemData _SignAward;
    // Use this for initialization
    void Start()
    {
        if (UIEventListener.Get(this.gameObject).onClick == null)
        {
            UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
            {
                ClickSignItem();
            };
        }
    }


    public void SetSignExtraItem(SignExtraItemData _SignAward)
    {
        this._SignAward = _SignAward;
        signID = _SignAward.ItemID;
        /* signItemState = _SignAward.state;
         if (_SignAward.vipLv == 0)
         {
             vipObj.SetActive(false);
         }
         else
         {
             vipObj.SetActive(true);
             vipNumLabel.text = string.Format("V{0}", _SignAward.vipLv.ToString());
         }*/
        if (_SignAward.ItemNum <= 9999)
        {
            countLabel.text = _SignAward.ItemNum.ToString();
        }
        else
        {
            countLabel.text = _SignAward.ItemNum / 10000.0f + "W";
        }
        if (_SignAward.ItemID == 70000 || _SignAward.ItemID == 79999)
        {
            suiPian.SetActive(true);
            icon.spriteName = _SignAward.ItemID.ToString();
        }
        else if (_SignAward.ItemID.ToString()[0] == '7')
        {
            icon.atlas = RoleAtlas;
            suiPian.SetActive(true);
            icon.spriteName = (_SignAward.ItemID - 10000).ToString();
        }
        else
        {
            suiPian.SetActive(false);
            icon.spriteName = _SignAward.ItemID.ToString();
        }
        SetItemFrame(_SignAward.ItemID);

        //CanGetSignExtraAwardEffect.SetActive(SignWindow.CanGetSignExtraAward);
        ResetSignExtraItemEffectState();
    }
    public void ResetSignExtraItemEffectState()
    {
        CanGetSignExtraAwardEffect.SetActive(SignWindow.CanGetSignExtraAward);
    }
    public void ResetSignItem()
    {
        if (signID == curClickSignId)
        {
            signItemState = 2;
        }
        else
        {
            return;
        }
    }
    void SetItemFrame(int _itemId)
    {
        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(_itemId);
        //Debug.LogError("_itemId.." + _itemId + "..Item.." + _ItemInfo);
        frame.spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
    }
    void ClickSignItem()
    {
        //Debug.LogError("Click ExtraSignItem");
        UIManager.instance.OpenPanel("SignItemDetail", false);
        GameObject.Find("SignItemDetail").GetComponent<SignItemDetail>().SetSignItemDetail(this._SignAward);
        /* switch (signItemState)
         {
             case 0:
                 UIManager.instance.OpenPanel("SignItemDetail",false);
                 GameObject.Find("SignItemDetail").GetComponent<SignItemDetail>().SetSignItemDetail(this._SignAward);
                 break;
             case 1:
                 NetworkHandler.instance.SendProcess("9102#;");
                 curClickSignId = signID;
                 //NetworkHandler.instance.SendProcess(string.Format("9102#{0};", signID)); 
                 break;
             case 2: UIManager.instance.OpenPromptWindow("已经签过啦", PromptWindow.PromptType.Hint, null, null); break;
             default: break;
         }*/
    }
}
