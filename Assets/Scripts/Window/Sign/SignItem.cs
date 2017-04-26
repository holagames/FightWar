using UnityEngine;
using System.Collections;

public class SignItem: MonoBehaviour 
{
    public static int curClickSignId;
    public UIAtlas RoleAtlas;
    [SerializeField]
    private GameObject hadSignObj;
    [SerializeField]
    private GameObject vipObj;
    [SerializeField]
    private UILabel vipNumLabel;
    [SerializeField]
    private UILabel countLabel;
    [SerializeField]
    private UISprite icon;
    [SerializeField]
    private GameObject suiPian;
    [SerializeField]
    private UISprite frame;
    [SerializeField]
    private GameObject frameSelect;
    [SerializeField]
    private UISprite bigFrame;
    [SerializeField]
    private GameObject canSignEffect;
    [SerializeField]
    private GameObject canSignEffectTexture;
    private int signItemState;
    private int signID;
    private Sign _SignAward;
	// Use this for initialization
	void Start () 
    {
        if (UIEventListener.Get(this.gameObject).onClick == null)
        {
            UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
            {
                ClickSignItem();
            };
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    public void SetSignItem(Sign _SignAward)
    {
        this._SignAward = _SignAward;
        signID = _SignAward.signID;
        signItemState = _SignAward.state;
        if (_SignAward.vipLv == 0)
        {
            vipObj.SetActive(false);
        }
        else
        {
            vipObj.SetActive(true);
            vipNumLabel.text = string.Format("V{0}", _SignAward.vipLv.ToString());
        }
        if (_SignAward.itemCount <= 9999)
        {
            countLabel.text = _SignAward.itemCount.ToString();
        }
        else
        {
            countLabel.text = _SignAward.itemCount / 10000f + "W";
        }
        if (_SignAward.itemId == 70000 || _SignAward.itemId==79999)
        {
            suiPian.SetActive(true);
            icon.spriteName = _SignAward.itemId.ToString();
        }
        else if (_SignAward.itemId.ToString()[0] == '7')
        {
            icon.atlas = RoleAtlas;
            suiPian.SetActive(true);
            icon.spriteName = (_SignAward.itemId - 10000).ToString();
        }
        else
        {
            suiPian.SetActive(false);
            icon.spriteName = _SignAward.itemId.ToString();
        }
        SetSignItemState(_SignAward.state);
        SetItemFrame(_SignAward.itemId);
    }
    public void ResetSignItem()
    {
        if (signID == curClickSignId)
        {
            signItemState = 2;
            SetSignItemState(2);
        }
        else
        {
            return ;
        }
    }
    void SetSignItemState(int _state)// 0 - 不可签,1 - 可签,2 - 已签
    {
        switch(_state)
        {
            case 0: bigFrame.spriteName = "signItemDi1";
                hadSignObj.SetActive(false);
                canSignEffectTexture.SetActive(false);
                break;//不可签
            case 1: 
                //bigFrame.spriteName = "signItemDi2";
                bigFrame.spriteName = "signItemDi1";//不论是否可签，都用signItemDi1
               /* GameObject effectObj = NGUITools.AddChild(this.gameObject, canSignEffect);
                effectObj.transform.localScale = Vector3.one;
                effectObj.name = "CanSignEffect";*/
                canSignEffectTexture.SetActive(true);
                hadSignObj.SetActive(false);
                break;//可签
            case 2: bigFrame.spriteName = "signItemDi1";
                hadSignObj.SetActive(true);
               /* Transform _CanSignEffect = this.transform.FindChild("CanSignEffect");
                if (_CanSignEffect != null)
                {
                    _CanSignEffect.gameObject.SetActive(false);
                }*/
                canSignEffectTexture.SetActive(false);
                break;//已签
            default: break;
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
        //Debug.LogError("Click SignItem");
        switch (signItemState)
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
        }
    }
}
