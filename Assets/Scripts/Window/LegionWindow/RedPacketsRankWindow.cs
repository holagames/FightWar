using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RedPacketsRankWindow : MonoBehaviour 
{

    public GameObject GiveRedPart;
    public GameObject GetRedPart;
    public GameObject TuhaoGiveRedPart;
    public GameObject ChongzhiGiveRedPart;
    public GameObject RankListPart;
    public GameObject ChoseRedPart;

    public GameObject GiveRedItem;
    public GameObject GiveGrid;
    public UILabel GiveMyRedNum;
    public UILabel GiveMyRedValue;

    public GameObject GetRedItem;
    public GameObject GetGrid;
    public UILabel GetMyRedNum;
    public UILabel GetMyRedValue;


    public GameObject TuhaoRedItem;
    public GameObject TuhaoGrid;
    public UILabel TuhaoMyRedNum;
    public UILabel TuhaoMyRedValue;

    public GameObject ChongzhiRedItem;
    public GameObject ChongzhiGrid;
    public UILabel ChongzhiMyRedNum;
    public UILabel ChongzhiMyRedValue;

    public GameObject RankListItem;
    public GameObject RankGrid;
    public UILabel RanktMyRedNum;

    public GameObject ChoseItem;
    public GameObject ChoseGrid;

    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;

    public GameObject CloseButton;
    private List<ChoseRedItem> ChoseRedItemList = new List<ChoseRedItem>();
	void Start () {

        UIEventListener.Get(CloseButton).onClick = delegate(GameObject go)
        {
            UIManager.instance.BackUI();
        };
	}

    public void SetChoseRedPartOpen(int RedType)
    {
        ChoseRedPart.SetActive(true);
        ChoseRedItemList.Clear();
        Debug.LogError("RedType" + RedType);
        foreach (var Item in TextTranslator.instance.LegionRedBagList) 
        {
            if (Item.RedBagType == 300001 && Item.SendBagType == RedType) 
            {
                Debug.LogError("LegionRedID" + Item.LegionRedID);
                GameObject go = NGUITools.AddChild(ChoseGrid, ChoseItem);
                go.SetActive(true);
                ChoseRedItem CR = new ChoseRedItem();
                LegionRedBag LR = TextTranslator.instance.GetLegionRedBagByID(Item.LegionRedID);
                CR.SetChoseRedItemInfo(go, LR.LegionRedID, LR.VipLimit, LR.ItemName, LR.ItemIcon, LR.Level, LR.RedBagNum, LR.RewardItemId1, LR.RewardItemNum1, LR.RewardItemId2, LR.RewardItemNum2, LR.GetRewardItemId1, LR.GetRewardItemNum1, LR.Diamond);
            }
        }
    }

    public void SetGiveRedPart(string Recving) //发红包排行
    {
        GiveRedPart.SetActive(true);
        string[] dataSplit = Recving.Split(';');
        GiveMyRedNum.text = dataSplit[0];
        GiveMyRedValue.text = dataSplit[1];

        for (int i = 2; i < dataSplit.Length - 1; i++) 
        {
            string[] trcSplit = dataSplit[i].Split('$');
            GameObject go = NGUITools.AddChild(GiveGrid, GiveRedItem);
            go.SetActive(true);
            if (trcSplit[0]=="1") 
            {
                go.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite/RankSprite").GetComponent<UISprite>().spriteName = "word1";
            }
            else if (trcSplit[0] == "2") 
            {
                go.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite/RankSprite").GetComponent<UISprite>().spriteName = "word2";
            }
            else if (trcSplit[0] == "3")
            {
                go.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite/RankSprite").GetComponent<UISprite>().spriteName = "word3";
            }
            else 
            {
                go.transform.Find("RankNumber").gameObject.SetActive(true);
                go.transform.Find("RankNumber").GetComponent<UILabel>().text = trcSplit[0];
            }

            go.transform.Find("HeroGrade/HeroIcon").GetComponent<UISprite>().spriteName = trcSplit[1];

            go.transform.Find("CharacterName").GetComponent<UILabel>().text = trcSplit[2];
            go.transform.Find("RedNum").GetComponent<UILabel>().text = trcSplit[3];
            go.transform.Find("RedValue").GetComponent<UILabel>().text = trcSplit[4];
        }

        GiveGrid.GetComponent<UIGrid>().Reposition();
    }

    public void SetGetRedPart(string Recving) //抢红包排行
    {
        GetRedPart.SetActive(true);
        string[] dataSplit = Recving.Split(';');
        GetMyRedNum.text = dataSplit[0];
        GetMyRedValue.text = dataSplit[1];
        for (int i = 2; i < dataSplit.Length - 1; i++)
        {
            string[] trcSplit = dataSplit[i].Split('$');
            GameObject go = NGUITools.AddChild(GetGrid, GetRedItem);
            go.SetActive(true);
            if (trcSplit[0] == "1")
            {
                go.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite/RankSprite").GetComponent<UISprite>().spriteName = "word1";
            }
            else if (trcSplit[0] == "2")
            {
                go.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite/RankSprite").GetComponent<UISprite>().spriteName = "word2";
            }
            else if (trcSplit[0] == "3")
            {
                go.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite/RankSprite").GetComponent<UISprite>().spriteName = "word3";
            }
            else
            {
                go.transform.Find("RankNumber").gameObject.SetActive(true);
                go.transform.Find("RankNumber").GetComponent<UILabel>().text = trcSplit[0];
            }

            go.transform.Find("HeroGrade/HeroIcon").GetComponent<UISprite>().spriteName = trcSplit[1];

            go.transform.Find("CharacterName").GetComponent<UILabel>().text = trcSplit[2];
            go.transform.Find("RedNum").GetComponent<UILabel>().text = trcSplit[3];
            go.transform.Find("RedValue").GetComponent<UILabel>().text = trcSplit[4];
        }

        GetGrid.GetComponent<UIGrid>().Reposition();
    }

    public void SetTuhaoGiveRedPart(string Recving) //土豪发红包排行
    {
        TuhaoGiveRedPart.SetActive(true);
        string[] dataSplit = Recving.Split(';');
        TuhaoMyRedNum.text = dataSplit[0];
        TuhaoMyRedValue.text = dataSplit[1];
        for (int i = 2; i < dataSplit.Length - 1; i++)
        {
            string[] trcSplit = dataSplit[i].Split('$');
            GameObject go = NGUITools.AddChild(TuhaoGrid, TuhaoRedItem);
            go.SetActive(true);
            if (trcSplit[0] == "1")
            {
                go.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite/RankSprite").GetComponent<UISprite>().spriteName = "word1";
            }
            else if (trcSplit[0] == "2")
            {
                go.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite/RankSprite").GetComponent<UISprite>().spriteName = "word2";
            }
            else if (trcSplit[0] == "3")
            {
                go.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite/RankSprite").GetComponent<UISprite>().spriteName = "word3";
            }
            else
            {
                go.transform.Find("RankNumber").gameObject.SetActive(true);
                go.transform.Find("RankNumber").GetComponent<UILabel>().text = trcSplit[0];
            }

            go.transform.Find("HeroGrade/HeroIcon").GetComponent<UISprite>().spriteName = trcSplit[1];

            go.transform.Find("CharacterName").GetComponent<UILabel>().text = trcSplit[2];
            go.transform.Find("RedNum").GetComponent<UILabel>().text = trcSplit[3];
            go.transform.Find("RedValue").GetComponent<UILabel>().text = trcSplit[4];
        }

        TuhaoGrid.GetComponent<UIGrid>().Reposition();
    }

    public void SetChongzhiGiveRedPart(string Recving) //充值发红包排行
    {
        ChongzhiGiveRedPart.SetActive(true);
        string[] dataSplit = Recving.Split(';');
        ChongzhiMyRedNum.text = dataSplit[0];
        ChongzhiMyRedValue.text = dataSplit[1];
        for (int i = 2; i < dataSplit.Length - 1; i++)
        {
            string[] trcSplit = dataSplit[i].Split('$');
            GameObject go = NGUITools.AddChild(ChongzhiGrid, ChongzhiRedItem);
            go.SetActive(true);
            if (trcSplit[0] == "1")
            {
                go.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite/RankSprite").GetComponent<UISprite>().spriteName = "word1";
            }
            else if (trcSplit[0] == "2")
            {
                go.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite/RankSprite").GetComponent<UISprite>().spriteName = "word2";
            }
            else if (trcSplit[0] == "3")
            {
                go.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite/RankSprite").GetComponent<UISprite>().spriteName = "word3";
            }
            else
            {
                go.transform.Find("RankNumber").gameObject.SetActive(true);
                go.transform.Find("RankNumber").GetComponent<UILabel>().text = trcSplit[0];
            }

            go.transform.Find("HeroGrade/HeroIcon").GetComponent<UISprite>().spriteName = trcSplit[1];

            go.transform.Find("CharacterName").GetComponent<UILabel>().text = trcSplit[2];
            go.transform.Find("RedNum").GetComponent<UILabel>().text = trcSplit[3];
            go.transform.Find("RedValue").GetComponent<UILabel>().text = trcSplit[4];
        }

        ChongzhiGrid.GetComponent<UIGrid>().Reposition();
    }

    public void SetRankListPart(string Recving) //抢红包单个排行
    {
        RankListPart.SetActive(true);
        string[] dataSplit = Recving.Split(';');
        RanktMyRedNum.text = dataSplit[0];
        for (int i = 1; i < dataSplit.Length - 1; i++)
        {
            string[] trcSplit = dataSplit[i].Split('$');
            GameObject go = NGUITools.AddChild(RankGrid, RankListItem);
            go.SetActive(true);
            if (trcSplit[0] == "1")
            {
                go.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite/RankSprite").GetComponent<UISprite>().spriteName = "word1";
            }
            else if (trcSplit[0] == "2")
            {
                go.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite/RankSprite").GetComponent<UISprite>().spriteName = "word2";
            }
            else if (trcSplit[0] == "3")
            {
                go.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite/RankSprite").GetComponent<UISprite>().spriteName = "word3";
            }
            else
            {
                go.transform.Find("RankNumber").gameObject.SetActive(true);
                go.transform.Find("RankNumber").GetComponent<UILabel>().text = trcSplit[0];
            }

            go.transform.Find("HeroGrade/HeroIcon").GetComponent<UISprite>().spriteName = trcSplit[1];

            go.transform.Find("CharacterName").GetComponent<UILabel>().text = trcSplit[2];
            //go.transform.Find("RedNum").GetComponent<UILabel>().text = trcSplit[3];
            go.transform.Find("RedValue").GetComponent<UILabel>().text = trcSplit[3];
        }

        RankGrid.GetComponent<UIGrid>().Reposition();
    }
}


public class ChoseRedItem 
{
    public int RedID;
    public int NeedVipNum;
    public string TopName;
    public int IconId;
    public int TitleType;
    public int RedBagNum;
    public int ItemId1;
    public int ItemCount1;
    public int ItemId2;
    public int ItemCount2;
    public int ItemId3;
    public int ItemCount3;
    public int DiamondNum;

    public GameObject Obj;
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;

    public void SetChoseRedItemInfo(GameObject go, int RedID,int NeedVipNum, string TopName, int IconId, int TitleType, int RedBagNum,int ItemId1, int ItemCount1, int ItemId2, int ItemCount2, int ItemId3, int ItemCount3, int DiamondNum)
    {
        this.Obj=go;
        this.RedID = RedID;
        this.NeedVipNum=NeedVipNum;
        this.TopName=TopName;
        this.RedBagNum = RedBagNum;
        this.IconId=IconId;
        this.TitleType = TitleType;
        this.ItemId1=ItemId1;
        this.ItemCount1=ItemCount1;
        this.ItemId2=ItemId2;
        this.ItemCount2=ItemCount2;
        this.ItemId3=ItemId3;
        this.ItemCount3=ItemCount3;
        this.DiamondNum=DiamondNum;

        RedPacketsRankWindow RP= GameObject.Find("RedPacketsRankWindow").GetComponent<RedPacketsRankWindow>();
        if (RP != null) 
        {
            this.ItemAtlas = RP.ItemAtlas;
            this.RoleAtlas = RP.RoleAtlas;
        }

        if(UIEventListener.Get(Obj.transform.Find("SendRedButton").gameObject).onClick==null)
        {
            UIEventListener.Get(Obj.transform.Find("SendRedButton").gameObject).onClick=delegate(GameObject go2)
            {
                if (CharacterRecorder.instance.lunaGem < DiamondNum)
                {
                    UIManager.instance.OpenPromptWindow("钻石不足，请充值", PromptWindow.PromptType.Hint, null, null);
                }
                else if (CharacterRecorder.instance.Vip < NeedVipNum) 
                {
                    UIManager.instance.OpenPromptWindow("VIP等级不足", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    NetworkHandler.instance.SendProcess("8704#" + TextTranslator.instance.GetLegionRedBagByID(RedID).SendBagType + ";" + TitleType + ";");
                }
            };
        }
        ChangeChoseRedItem();
    }

    public void ChangeChoseRedItem()
    {
        Obj.transform.Find("StateSprite/VipNum").GetComponent<UILabel>().text="V"+NeedVipNum.ToString();
        Obj.transform.Find("LeftSprite/topLabel").GetComponent<UILabel>().text="红包";
        Obj.transform.Find("LeftSprite/Icon").GetComponent<UISprite>().spriteName=IconId.ToString();


        if (TitleType == 1)
        {
            Obj.transform.Find("TitleLabel").GetComponent<UILabel>().text = "高级" + TopName + RedBagNum+"个";
        }
        else 
        {
            Obj.transform.Find("TitleLabel").GetComponent<UILabel>().text = "至尊" + TopName + RedBagNum + "个";
        }

        if(ItemId1!=0)
        {
            Obj.transform.Find("Item1").gameObject.SetActive(true);
            //Obj.transform.Find("Item1").GetComponent<UISprite>().spriteName=ItemId1.ToString();
            SetItemDetail(ItemId1, Obj.transform.Find("Item1").GetComponent<UISprite>(), Obj.transform.Find("Item1/suiPian").gameObject);
            Obj.transform.Find("Item1/num").GetComponent<UILabel>().text="x"+ItemCount1.ToString();
        }
        else
        {
            Obj.transform.Find("Item1").gameObject.SetActive(false);
        }

        if(ItemId2!=0)
        {
            Obj.transform.Find("Item2").gameObject.SetActive(true);
            //Obj.transform.Find("Item2").GetComponent<UISprite>().spriteName=ItemId2.ToString();
            SetItemDetail(ItemId2, Obj.transform.Find("Item2").GetComponent<UISprite>(), Obj.transform.Find("Item2/suiPian").gameObject);
            Obj.transform.Find("Item2/num").GetComponent<UILabel>().text="x"+ItemCount2.ToString();
        }
        else
        {
            Obj.transform.Find("Item2").gameObject.SetActive(false);
        }

        if(ItemId3!=0)
        {
            Obj.transform.Find("Item3").gameObject.SetActive(true);
            //Obj.transform.Find("Item3").GetComponent<UISprite>().spriteName=ItemId3.ToString();
            SetItemDetail(ItemId3, Obj.transform.Find("Item3").GetComponent<UISprite>(), Obj.transform.Find("Item3/suiPian").gameObject);
            Obj.transform.Find("Item3/num").GetComponent<UILabel>().text="x"+ItemCount3.ToString();
        }
        else
        {
            Obj.transform.Find("Item3").gameObject.SetActive(false);
        }

        Obj.transform.Find("Diamond/num").GetComponent<UILabel>().text=DiamondNum.ToString();
    }



    private void SetItemDetail(int _itemId, UISprite spriteIcon, GameObject suiPian)
    {
        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(_itemId);

        //if (_itemId.ToString()[0] == '8')
        //{
        //    TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_itemId);
        //    spriteIcon.spriteName = mItemInfo.picID.ToString();
        //}
        //else if (_itemId.ToString()[0] == '2')
        //{
        //    TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_itemId);
        //    spriteIcon.spriteName = mItemInfo.picID.ToString();
        //}
        //else
        //{
        //    spriteIcon.spriteName = _itemId.ToString();
        //}

        if (_itemId.ToString()[0] == '4')
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = _itemId.ToString();
            suiPian.SetActive(false);
        }
        else if (_itemId.ToString()[0] == '6')
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = _itemId.ToString();
            suiPian.SetActive(false);
        }
        else if (_itemId == 70000 || _itemId == 79999)
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = _itemId.ToString();
            suiPian.SetActive(true);
        }
        else if (_itemId.ToString()[0] == '7' && _itemId > 70000)
        {
            spriteIcon.atlas = RoleAtlas;
            spriteIcon.spriteName = (_itemId - 10000).ToString();
            suiPian.SetActive(true);
        }
        else if (_itemId.ToString()[0] == '8')
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_itemId);
            //spriteIcon.spriteName = (ItemCode - 30000).ToString();
            spriteIcon.spriteName = mItemInfo.picID.ToString();
            suiPian.SetActive(true);
        }
        else if (_itemId.ToString()[0] == '2')
        {
            spriteIcon.atlas = ItemAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(_itemId);
            spriteIcon.spriteName = mItemInfo.picID.ToString();
            suiPian.SetActive(false);
        }
        else
        {
            spriteIcon.atlas = ItemAtlas;
            spriteIcon.spriteName = _itemId.ToString();
            suiPian.SetActive(false);
        }

    }
}