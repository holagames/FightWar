using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GetRankingReward : MonoBehaviour {

    public UILabel HighRanking;
    public GameObject Grid;
    public GameObject Item;
    public GameObject CloseButton;

    private Vector3 ScrollViewPosition;
    private int RankNum;
    private List<GameObject> ItemList = new List<GameObject>();

    private string RankReward;
    private int GetLayer;   //领到第几层
    private int HaveLayer;  //可以领到层数
    public  int MoneyNum;
    private int DiamondNum=0;
    private int Item1Num=0;
    private int Item2Num=0;

    private bool isPress = false;
    private float datatime = 0;
    private int ItemId;
    private int ItemCount;
    GameObject ItemObj;

    private UICenterOnChild uicenter;

    void Start()
    {
        uicenter = Grid.GetComponent<UICenterOnChild>();
        ScrollViewPosition = Grid.transform.parent.localPosition;
        GetPVPRank();
        //if (UIEventListener.Get(GameObject.Find("GetRankingReward/Title/IconBg1")).onPress == null)
        //{
        //    UIEventListener.Get(GameObject.Find("GetRankingReward/Title/IconBg1")).onPress = delegate(GameObject go, bool isPressed)
        //    {
        //        this.ItemId = 90002;
        //        this.ItemCount = 0;
        //        ItemObj=GameObject.Find("GetRankingReward/Title/IconBg1");
        //        OnPress(isPressed);
        //    };
        //}
        //if (UIEventListener.Get(GameObject.Find("GetRankingReward/Title/IconBg2")).onPress == null)
        //{
        //    UIEventListener.Get(GameObject.Find("GetRankingReward/Title/IconBg2")).onPress = delegate(GameObject go, bool isPressed)
        //    {
        //        this.ItemId = 90001;
        //        this.ItemCount = 0;
        //        ItemObj=GameObject.Find("GetRankingReward/Title/IconBg2");
        //        OnPress(isPressed);
        //    };
        //}
        //if (UIEventListener.Get(GameObject.Find("GetRankingReward/Title/IconBg3")).onPress == null)
        //{
        //    UIEventListener.Get(GameObject.Find("GetRankingReward/Title/IconBg3")).onPress = delegate(GameObject go, bool isPressed)
        //    {
        //        this.ItemId = 10003;
        //        this.ItemCount = 0;
        //        ItemObj=GameObject.Find("GetRankingReward/Title/IconBg3");
        //        OnPress(isPressed);
        //    };
        //}
        //if (UIEventListener.Get(GameObject.Find("GetRankingReward/Title/IconBg4")).onPress == null)
        //{
        //    UIEventListener.Get(GameObject.Find("GetRankingReward/Title/IconBg4")).onPress = delegate(GameObject go, bool isPressed)
        //    {
        //        this.ItemId = 59010;
        //        this.ItemCount = 0;
        //        ItemObj = GameObject.Find("GetRankingReward/Title/IconBg4");
        //        OnPress(isPressed);
        //    };
        //}
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick = delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }
    }

    void OnPress(bool isPressed)
    {
        if (isPressed)
        {
            isPress = true;
        }
        else
        {
            isPress = false;
            if (GameObject.Find("ItemExplanationWindow") != null)
            {
                UIManager.instance.DestroyPanel("ItemExplanationWindow");
            }
            datatime = 0;
        }
    }

    void GetAllAward() //初始时获得总收益
    {
        for (int i = 1; i <=GetLayer; i++)
        {
            PvpOnceReward RewardItem = TextTranslator.instance.GetPvpOnceRewardByID(i);
            DiamondNum += RewardItem.Diamond;
            MoneyNum += RewardItem.Gold;
            Item1Num += RewardItem.ItemNum1;
            Item2Num += RewardItem.ItemNum2;
        }
        GameObject.Find("GetRankingReward/Title/IconBg1/Number1").GetComponent<UILabel>().text = DiamondNum.ToString();
        //GameObject.Find("GetRankingReward/Title/IconBg2/Number2").GetComponent<UILabel>().text = MoneyNum.ToString();
        GameObject.Find("GetRankingReward/Title/IconBg3/Number3").GetComponent<UILabel>().text = Item1Num.ToString();
        GameObject.Find("GetRankingReward/Title/IconBg4/Number4").GetComponent<UILabel>().text = Item2Num.ToString();
        if (MoneyNum > 10000)
        {
            int a = MoneyNum / 10000;
            int b = MoneyNum / 1000 % 10;
            GameObject.Find("GetRankingReward/Title/IconBg2/Number2").GetComponent<UILabel>().text = a.ToString() + "." + b.ToString() + "W"; //+ c.ToString() + d.ToString() + e.ToString();
        }
        else 
        {
            GameObject.Find("GetRankingReward/Title/IconBg2/Number2").GetComponent<UILabel>().text = MoneyNum.ToString();
        }
    }

    void SetInfo()
    {
/*        for (int i = 0; i < ItemList.Count - 1; i++) //判断排名进入范围，颜色置为紫色
        {
            if (HaveLayer == ItemList[i].GetComponent<RankingRewardItem>().Layer)
            {
                ItemList[i].transform.Find("Bg/BgTop").GetComponent<UISprite>().spriteName = "PVPranking3";
                //ItemList[i].GetComponent<RankingRewardItem>().HeadRank.gameObject.SetActive(true);//当前名次显示头像
                //ItemList[i].transform.Find("MyRankSprite/TexturePlayHead").GetComponent<UITexture>().mainTexture = (Texture2D)Resources.Load("Head/60011");
                //ItemList[i].transform.Find("MyRankSprite/LabelVipNum").GetComponent<UILabel>().text ="Vip"+ CharacterRecorder.instance.Vip.ToString();
                //ItemList[i].transform.Find("MyRankSprite/LabelName").GetComponent<UILabel>().text = CharacterRecorder.instance.characterName;
                //ItemList[i].transform.Find("MyRankSprite/LabelFriendNum").GetComponent<UILabel>().text = "测试好友数量";
            }
        }*/
        for (int i = 1; i <= TextTranslator.instance.PvpOnceRewardDic.Count; i++) 
        {
            PvpOnceReward RewardItem=TextTranslator.instance.GetPvpOnceRewardByID(i);
            InstantiateItem(RewardItem.PvpOnceRewardID,RewardItem.PvpRankID,RewardItem.Diamond,RewardItem.Gold,RewardItem.ItemID1,RewardItem.ItemNum1,RewardItem.ItemID2,RewardItem.ItemNum2);
        }
        Grid.GetComponent<UIGrid>().repositionNow = true;
    }
    void InstantiateItem(int Layer, int Rank, int Diamond, int Money, int ItemID1, int ItemNum1, int ItemID2,int ItemNum2)//克隆RewardItem
    {
        GameObject obj = Instantiate(Item) as GameObject;
        obj.transform.parent = Grid.transform;
        obj.SetActive(true);
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
        obj.GetComponent<RankingRewardItem>().setInfo(Layer, Rank, Diamond, Money, ItemID1, ItemNum1, ItemID2,ItemNum2,GetLayer, HaveLayer);
        ItemList.Add(obj);
        StartCoroutine(SetOncenter());
        //Transform traobj = uicenter.transform.GetChild(GetLayer);
        //uicenter.CenterOn(traobj);

    }

    IEnumerator SetOncenter() 
    {
        yield return new WaitForSeconds(0.1f);
        if (GetLayer > 2 && GetLayer < 25)
        {
            Transform traobj = uicenter.transform.GetChild(GetLayer);
            uicenter.CenterOn(traobj);
            //uicenter.enabled = false;
        }
        else if (GetLayer >= 25)
        {
            Transform traobj = uicenter.transform.GetChild(25);
            uicenter.CenterOn(traobj);
            //uicenter.enabled = false;
        }
        uicenter.enabled = false;
    }

    public void updateItemlistShow(int _getlayer)//唯一名次奖励，奖励之后置为1；
    {
        GetLayer = _getlayer;
        foreach (GameObject go in ItemList)
        {
            go.GetComponent<RankingRewardItem>().GetLayer=_getlayer;
            if (go.GetComponent<RankingRewardItem>().Layer == _getlayer)
            {
                go.GetComponent<RankingRewardItem>().Switch = 1;
                go.GetComponent<RankingRewardItem>().GetInfo(MoneyNum); 
            }
            if (go.GetComponent<RankingRewardItem>().Layer == _getlayer + 1 && go.GetComponent<RankingRewardItem>().Layer <= HaveLayer) 
            {
                go.GetComponent<RankingRewardItem>().SetIconMation();
            }
        }
    }

    void GetPVPRank() 
    {
        GetLayer = CharacterRecorder.instance.GetRankLayer;
        HaveLayer = CharacterRecorder.instance.HaveRankLayer;
        HighRanking.text = CharacterRecorder.instance.MaxRankNumber.ToString();
        SetInfo();
        if (GetLayer > 0)
        {
            GetAllAward();
        }
        else
        {
            GameObject.Find("GetRankingReward/Title/IconBg1/Number1").GetComponent<UILabel>().text = DiamondNum.ToString();
            GameObject.Find("GetRankingReward/Title/IconBg2/Number2").GetComponent<UILabel>().text = MoneyNum.ToString();
            GameObject.Find("GetRankingReward/Title/IconBg3/Number3").GetComponent<UILabel>().text = Item1Num.ToString();
            GameObject.Find("GetRankingReward/Title/IconBg4/Number4").GetComponent<UILabel>().text = Item2Num.ToString();
        }
    }
/*
    public void GetPVPRank(int _GetLayer, int _HaveLayer) //获得的层级，可获得层级
    {
        GetLayer = _GetLayer;
        HaveLayer = _HaveLayer;
        SetInfo();
        if (GetLayer > 0)
        {
            GetAllAward();
        }
        else
        {
            GameObject.Find("GetRankingReward/Title/IconBg1/Number1").GetComponent<UILabel>().text = DiamondNum.ToString();
            GameObject.Find("GetRankingReward/Title/IconBg2/Number2").GetComponent<UILabel>().text = MoneyNum.ToString();
            GameObject.Find("GetRankingReward/Title/IconBg3/Number3").GetComponent<UILabel>().text = Item1Num.ToString();
            GameObject.Find("GetRankingReward/Title/IconBg4/Number4").GetComponent<UILabel>().text = Item2Num.ToString();
        }
    }*/
}
