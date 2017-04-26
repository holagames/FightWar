using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShopItemDetailWindow : MonoBehaviour 
{
    [SerializeField]
    private GameObject closeButton;
    [SerializeField]
    private GameObject buyButton;
    [SerializeField]
    private GameObject suiPian;
    [SerializeField]
    private UISprite spriteIcon;
    [SerializeField]
    private UISprite spriteFrame;
    [SerializeField]
    private UISprite spriteType;
    [SerializeField]
    private UILabel labelName;
    [SerializeField]
    private UILabel labelCountDes;
    [SerializeField]
    private UILabel labelDes;
    [SerializeField]
    private UILabel labelBuyDes;
    [SerializeField]
    private UILabel labelTotleCost;
    [SerializeField]
    private UIAtlas ItemAtlas;
    [SerializeField]
    private UIAtlas RoleAtlas;

    public GameObject[] HeroArr = new GameObject[5];

    private int index;
    private int itemCount;
    private int itemId;
	// Use this for initialization
	void Start () 
    {
        UIEventListener.Get(closeButton).onClick = ClickCloseButton;
        UIEventListener.Get(buyButton).onClick = ClickBuyButton;
	}
	
	// Update is called once per frame
    //void Update () 
    //{
	
    //}
    public void SetShopItemDetail(ShopItemData _oneShopItemData)
    {
        //this.name = _oneShopItemData.index.ToString();
        int ItemCode = _oneShopItemData.Id;//物品id
        itemId = ItemCode;
        itemCount = _oneShopItemData.count;//数量
        int ItemGrade = _oneShopItemData.grade;//级别
        index = _oneShopItemData.index;//索引
        string ItemNameStr = TextTranslator.instance.GetItemNameByItemCode(ItemCode);//道具名字
        string ItemDesStr = TextTranslator.instance.GetItemDescriptionByItemCode(ItemCode);//物品描述
        //Debug.LogError(ItemCode);
       /* if (ItemCount == 0)
        {
            TextureMask.SetActive(true);
        }*/

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
        else if (ItemCode.ToString()[0] == '6')
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = ItemCode.ToString();
            suiPian.SetActive(false);
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
        //labelCountDes.text = string.Format("{0}拥有件数:{1}{2}","[ffffff]","[93d8f3]", itemCount);
        if (ItemCode == 90001)
        {
            labelCountDes.text = "[00FFFF]拥有件数：[-][ffffff]" + GameObject.Find("RankShopWindow/TopContent/MoneyButton/Label").GetComponent<UILabel>().text;
        }
        else
        {
            labelCountDes.text = "[00FFFF]拥有件数：[-][ffffff]" + TextTranslator.instance.GetItemCountByID(ItemCode).ToString();
        }
        //labelCountDes.text = "拥有件数:"+TextTranslator.instance.GetItemCountByID(ItemCode).ToString();////////
        //labelCountDes.text = string.Format("拥有件数:{0}{1}","[93d8f3]", itemCount);
        GameObject.Find("All/FrameBg/Frame/LabelCount").GetComponent<UILabel>().text = itemCount.ToString();
        labelName.text = ItemNameStr;
        labelDes.text = ItemDesStr;
        switch (_oneShopItemData.priceType)
        {
            case 1: spriteType.spriteName = "90001"; break;
            case 3: spriteType.spriteName = "90003"; break;
            case 4: spriteType.spriteName = "90004"; break;
            case 7: spriteType.spriteName = "90010"; break;
            default: spriteType.spriteName = "9000" + _oneShopItemData.priceType.ToString(); break;
        }
        //labelBuyDes.text = string.Format("购买{0}件:", itemCount);
        //labelTotleCost.text = (itemCount * _oneShopItemData.priceValue).ToString();
        //labelBuyDes.text = "购买金额:";
        if (_oneShopItemData.priceValue > CharacterRecorder.instance.gold) 
        {
            labelTotleCost.color = Color.red;
        }
        labelTotleCost.text = (_oneShopItemData.priceValue).ToString();

        WhichHeroNeedItem();
    }
    private void ClickCloseButton(GameObject go)
    {
        if(go != null)
        {
            UIManager.instance.BackUI();
        }
    }
    private void ClickBuyButton(GameObject go)
    {
        if (go != null && itemCount > 0)
        {
            //NetworkHandler.instance.SendProcess(string.Format("5103#{0};{1};", RankShopWindow.curSelectShopType,index));
            if (GameObject.Find("RankShopWindow") != null) 
            {
                NetworkHandler.instance.SendProcess(string.Format("5103#{0};{1};", RankShopWindow.curSelectShopType, index));
                //NetworkHandler.instance.SendProcess(string.Format("5102#{0};", RankShopWindow.curSelectShopType));
            }
            if (GameObject.Find("SecretShopWindow") != null)
            {
                NetworkHandler.instance.SendProcess(string.Format("5103#10006;{0};",index));
                //NetworkHandler.instance.SendProcess(string.Format("5102#{0};", SecretShopWindow.SelectShopType));
            }

            //NetworkHandler.instance.SendProcess(string.Format("5102#{0};", RankShopWindow.curSelectShopType));
            //if (GameObject.Find("RankShopWindow") != null)
            //{
            //    RankShopWindow rw = GameObject.Find("RankShopWindow").GetComponent<RankShopWindow>();
            //    if(rw.spriteType.spriteName=="icon2")
            //        NetworkHandler.instance.SendProcess("5102#10001;");
            //    if(rw.spriteType.spriteName=="icon3")
            //        NetworkHandler.instance.SendProcess("5102#10003;");
            //    if(rw.spriteType.spriteName=="icon4")
            //        NetworkHandler.instance.SendProcess("5102#10004;");
            //    if(rw.spriteType.spriteName=="icon5")
            //        NetworkHandler.instance.SendProcess("5102#10005;");
            //    if(rw.spriteType.spriteName=="icon6")
            //        NetworkHandler.instance.SendProcess("5102#10002;");
            //}
            UIManager.instance.BackUI();
        }
    }

    private void WhichHeroNeedItem() 
    {
        int listSize = CharacterRecorder.instance.ownedHeroList.size;
        int m = 0;
        for (int i = 0; i < listSize; i++) 
        {
            if (i < 5) 
            {
                List<Item> NeedItemList = new List<Item>();

                Hero mHero = CharacterRecorder.instance.ownedHeroList[i];
                RoleClassUp rcu = TextTranslator.instance.GetRoleClassUpInfoByID(mHero.cardID, mHero.classNumber);
                List<Item> classUpList = new List<Item>();
                for (int j = 0; j < rcu.NeedItemList.Count; j++)//升品需要的物品
                {
                    if (rcu.NeedItemList[j].itemCode != 0)
                    {
                        classUpList.Add(rcu.NeedItemList[j]);
                    }
                }
                for (int x = 0; x < classUpList.Count; x++)//背包中对应的物品数量是否足够
                {
                    int bagItemCount1 = TextTranslator.instance.GetItemCountByID(classUpList[x].itemCode);
                    if (bagItemCount1 < classUpList[x].itemCount)
                    {
                        int IsHave = 0;
                        for (int y = 0; y < NeedItemList.Count; y++)
                        {
                            if (classUpList[x].itemCode == NeedItemList[y].itemCode)
                            {
                                IsHave = 1;
                                break;
                            }
                        }
                        if (IsHave == 0)
                        {
                            NeedItemList.Add(classUpList[x]);
                            Debug.Log("需要的Item  " + classUpList[x].itemCode);
                        }
                    }
                }

                if (CharacterRecorder.instance.lastGateID > 10007) //是否开放强化
                {
                    HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(mHero.cardID);
                    for (int j = 0; j < 4; j++)
                    {
                        Hero.EquipInfo mEquipInfo = mHero.equipList[j];
                        EquipStrongQuality esq = TextTranslator.instance.GetEquipStrongQualityByIDAndColor(mHero.equipList[j].equipCode, mHero.equipList[j].equipColorNum, hinfo.heroRace);
                        if (IsAdvanceState(mEquipInfo.equipLevel, mEquipInfo.equipColorNum))//单个装备是否能够强化
                        {
                            for (int x = 0; x < 4; x++) //单个装备强化需要的材料
                            {
                                if (x < esq.materialItemList.size)
                                {
                                    int itemCode = esq.materialItemList[x].itemCode;
                                    int itemCountInBag = TextTranslator.instance.GetItemCountByID(itemCode);
                                    //Item _Item = TextTranslator.instance.GetItemByID(itemCode);//TextTranslator.instance.GetItemByID(itemCode);
                                    if (itemCountInBag < esq.materialItemList[x].itemCount)
                                    {
                                        Item _Item = new Item(itemCode, esq.materialItemList[x].itemCount - itemCountInBag);
                                        //if (!NeedItemList.Contains(_Item))
                                        //{
                                        //    NeedItemList.Add(_Item);
                                        //    Debug.LogError(_Item.itemCode);
                                        //}
                                        int IsHave = 0;
                                        for (int y = 0; y < NeedItemList.Count; y++)
                                        {
                                            if (itemCode == NeedItemList[y].itemCode)
                                            {
                                                IsHave = 1;
                                                break;
                                            }
                                        }
                                        if (IsHave == 0)
                                        {
                                            NeedItemList.Add(_Item);
                                            Debug.Log("需要的Item  " + _Item.itemCode);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }

                if (NeedItemList.Count > 0) 
                {
                    for (int j = 0; j < NeedItemList.Count; j++) 
                    {
                        if (NeedItemList[j].itemCode == itemId)
                        {
                            HeroArr[m].SetActive(true);
                            HeroArr[m].transform.Find("heroicon").GetComponent<UISprite>().spriteName = mHero.cardID.ToString();
                            m++;
                            break;
                        }
                    }
                }
            }
        }
    }


    bool IsAdvanceState(int _EquipLevel, int _EquipColorNum)////是否能够强化
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
}
