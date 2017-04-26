using UnityEngine;
using System.Collections;

public class RankShopItem : MonoBehaviour
{
    [SerializeField]
    private UISprite spriteIcon;
    [SerializeField]
    private UISprite spriteFrame;
    [SerializeField]
    private UISprite spriteType;
    [SerializeField]
    private UILabel LabelName;
    [SerializeField]
    private UILabel labelCount;
    [SerializeField]
    private UILabel labelPrice;
    [SerializeField]
    private GameObject suiPian;
    [SerializeField]
    private GameObject CanSignEffect;

    public GameObject GreenPoint;
    private int ItemCode;
    private int ItemCount;
    private int ItemGrade;
    private int Index;
    private int GoldPrice;
    private int DiamondPrice;
    private int HonourPrice;
    private ShopItemData oneShopItemData;
    public GameObject TextureMask;
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;
	// Use this for initialization
	void Start () 
    {
        if (UIEventListener.Get(gameObject).onClick == null)
        {
            UIEventListener.Get(gameObject).onClick += delegate(GameObject go)
            {
                //Debug.Log("BBB" + ItemCode);
                //GameObject.Find("RankShopWindow").GetComponent<RankShopWindow>().BuyClick(Index);
                if (ItemCount > 0)
                {
                   // NetworkHandler.instance.SendProcess("5103#1001;" + Index + ";");
                    UIManager.instance.OpenPanel("ShopItemDetailWindow",false);
                    GameObject.Find("ShopItemDetailWindow").GetComponent<ShopItemDetailWindow>().SetShopItemDetail(oneShopItemData);
                }
            };
        }
	}
    public void SetItemCount() 
    {
        this.ItemCount = 0;
        labelCount.text = "0";
    }
    public void Init(ShopItemData _oneShopItemData)
    {
        oneShopItemData = _oneShopItemData;
        this.name = _oneShopItemData.index.ToString();
        ItemCode = _oneShopItemData.Id;
        ItemCount = _oneShopItemData.count;
        ItemGrade = _oneShopItemData.grade;
        Index = _oneShopItemData.index;
        string ItemNameStr = TextTranslator.instance.GetItemNameByItemCode(ItemCode);

        TextTranslator.instance.ItemDescription(spriteFrame.gameObject, ItemCode, 0);

        UIEventListener.Get(spriteFrame.gameObject).onClick += delegate(GameObject go)
        {
            if (ItemCount > 0)
            {
                UIManager.instance.OpenPanel("ShopItemDetailWindow", false);
                GameObject.Find("ShopItemDetailWindow").GetComponent<ShopItemDetailWindow>().SetShopItemDetail(oneShopItemData);
            }
        };


        if (ItemCount == 0)
        {

            TextureMask.SetActive(true);
            //TextureMask.mainTexture = Resources.Load("Game/sq", typeof(Texture)) as Texture;
        }
        //if (ItemGrade == 0) 
        //{
        //    spriteFrame.spriteName = "Grade1";
        //}
        //else
        spriteFrame.spriteName = "Grade" + ItemGrade.ToString();

        //if (ItemCode.ToString()[0] == '4')
        //{
        //    spriteIcon.atlas = ItemAtlas;
        //    spriteIcon.spriteName = (ItemCode - 10000).ToString();
        //    suiPian.SetActive(true);
        //}
        //else 
        if (ItemCode.ToString()[0] == '4') 
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            if (ItemCode.ToString()[1] == '0')
            {
                suiPian.SetActive(false);
            }
            else
            {
                suiPian.SetActive(true);
            }   
        }
        else if (ItemCode == 70000 || ItemCode == 79999)
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            suiPian.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '7')
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = (ItemCode - 10000).ToString();
            suiPian.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '8')
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = (ItemCode - 30000).ToString();
            suiPian.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '6')
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            suiPian.SetActive(false);
            CanSignEffect.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '2') 
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
            spriteIcon.spriteName = mItemInfo.picID.ToString();
            suiPian.SetActive(false);
        }
        else
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            suiPian.SetActive(false);
        }
        if (ItemCount >= 10000)
        {
            int a = ItemCount / 10000;
            int b = ItemCount % 10000 / 1000;
            labelCount.text = a.ToString()+"W";
            //labelCount.text = a.ToString() + "." + b.ToString() + "W";
        }
        else 
        {
            labelCount.text = ItemCount.ToString();
        }
        //labelCount.text = ItemCount.ToString();
        LabelName.text = ItemNameStr;
        switch (_oneShopItemData.priceType)
        {
            case 1: spriteType.spriteName = "90001"; break;
            case 3: spriteType.spriteName = "90003"; break;
            case 4: spriteType.spriteName = "90004"; break;
            case 7: spriteType.spriteName = "90010"; break;
            default: spriteType.spriteName = "9000" + _oneShopItemData.priceType.ToString(); break;
        }
        labelPrice.text = _oneShopItemData.priceValue.ToString();
    }
    public void Init(int _Index, int _ItemCode, int _ItemGrade, int _ItemCount, int _GoldPrice, int _DiamondPrice, int _HonourPrice)
    {
        ItemCode = _ItemCode;
        ItemCount = _ItemCount;
        ItemGrade = _ItemGrade;

        GoldPrice = _GoldPrice;
        DiamondPrice = _DiamondPrice;
        HonourPrice = _HonourPrice;
        string ItemName = TextTranslator.instance.GetItemNameByItemCode(_ItemCode);

        if (ItemCount == 0)
        {

            TextureMask.SetActive(true);
            //TextureMask.mainTexture = Resources.Load("Game/sq", typeof(Texture)) as Texture;
        }

        Index = _Index;
        gameObject.transform.Find("SpriteAvatarBG").gameObject.GetComponent<UISprite>().spriteName = ("Grade" + (_ItemGrade+1)).ToString();
        if (ItemCode.ToString()[0] == '4')
        {
            gameObject.transform.Find("SpriteAvatarBG").Find("SpriteAvatar").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            gameObject.transform.Find("SpriteAvatarBG").Find("SpriteAvatar").gameObject.GetComponent<UISprite>().spriteName = (ItemCode-10000).ToString();
            gameObject.transform.Find("SpriteAvatarBG").Find("SuiPian").gameObject.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '7')
        {
            gameObject.transform.Find("SpriteAvatarBG").Find("SpriteAvatar").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
            gameObject.transform.Find("SpriteAvatarBG").Find("SpriteAvatar").gameObject.GetComponent<UISprite>().spriteName = (ItemCode - 10000).ToString();
            gameObject.transform.Find("SpriteAvatarBG").Find("SuiPian").gameObject.SetActive(true);
        }
        else if (ItemCode.ToString()[0] == '8')
        {
            gameObject.transform.Find("SpriteAvatarBG").Find("SpriteAvatar").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            gameObject.transform.Find("SpriteAvatarBG").Find("SpriteAvatar").gameObject.GetComponent<UISprite>().spriteName = (ItemCode - 30000).ToString();
            gameObject.transform.Find("SpriteAvatarBG").Find("SuiPian").gameObject.SetActive(true);
        }
        else
        {
            gameObject.transform.Find("SpriteAvatarBG").Find("SpriteAvatar").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
            gameObject.transform.Find("SpriteAvatarBG").Find("SpriteAvatar").gameObject.GetComponent<UISprite>().spriteName = ItemCode.ToString();
            gameObject.transform.Find("SpriteAvatarBG").Find("SuiPian").gameObject.SetActive(false);
        }
        gameObject.transform.Find("SpriteAvatarBG").Find("LabelCount").gameObject.GetComponent<UILabel>().text = _ItemCount.ToString();
        gameObject.transform.Find("SpriteType").gameObject.GetComponent<UISprite>().spriteName = "icon3";
        gameObject.transform.Find("SpriteType").Find("LabelHonour").gameObject.GetComponent<UILabel>().text = _HonourPrice.ToString();
        gameObject.transform.Find("LabelName").gameObject.GetComponent<UILabel>().text = ItemName;

        if (_GoldPrice > 0)
        {
            gameObject.transform.Find("SpriteType").GetComponent<UISprite>().spriteName = "icon1";
            gameObject.transform.Find("SpriteType").Find("LabelHonour").gameObject.GetComponent<UILabel>().text = _GoldPrice.ToString();
        }
        else if (_DiamondPrice > 0)
        {
            gameObject.transform.Find("SpriteType").GetComponent<UISprite>().spriteName = "icon4";
            gameObject.transform.Find("SpriteType").Find("LabelHonour").gameObject.GetComponent<UILabel>().text = _DiamondPrice.ToString();
        }
        else
        {
            gameObject.transform.Find("SpriteType").GetComponent<UISprite>().spriteName = "icon3";
            gameObject.transform.Find("SpriteType").Find("LabelHonour").gameObject.GetComponent<UILabel>().text = _HonourPrice.ToString();
        }
    }
}
