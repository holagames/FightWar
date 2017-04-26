using UnityEngine;
using System.Collections;

public class RankingRewardItem : MonoBehaviour {
    public UILabel LabelRank;
    //public GameObject Icon1;
    public GameObject Number1;
   // public GameObject Icon2;
    public GameObject Number2;
   // public GameObject Icon3;
    public GameObject Number3;
   // public GameObject Icon4;
    public GameObject Number4;
    //public GameObject GoumaiObj;
    public GameObject IconBg;
    public GameObject IconBg3;
    public GameObject HeadRank;

    public GameObject PaiMing_ui;

    public int Switch; //0未领取，1领取过
    public int Layer;   //当前层级
    public int GetLayer;//取得的层级
    int HaveLayer;//能取得的层级

    void Start()
    {
        if (UIEventListener.Get(this.gameObject).onClick == null)
        {
            UIEventListener.Get(this.gameObject).onClick += delegate(GameObject obj)
            {
                if (Layer>HaveLayer&&Switch==0)
                {
                    //UIManager.instance.OpenPromptWindow("层级不足，无法领取", PromptWindow.PromptType.Hint, null, null);
                }
                else if (GetLayer < HaveLayer&&(Layer>GetLayer+1) && Switch == 0) 
                {
                    UIManager.instance.OpenPromptWindow("请先领取前面层级奖励", PromptWindow.PromptType.Hint, null, null);
                }
                else if (Switch == 1)
                {
                    //UIManager.instance.OpenPromptWindow("已经领取奖励", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    NetworkHandler.instance.SendProcess("6012#" + Layer + ";");
                }
            };
        }
        if (UIEventListener.Get(Number1).onClick == null) 
        {
             UIEventListener.Get(Number1).onClick += delegate(GameObject go)
             {
                 if (Layer > HaveLayer && Switch == 0)
                 {
                     //UIManager.instance.OpenPromptWindow("层级不足，无法领取", PromptWindow.PromptType.Hint, null, null);
                 }
                 else if (GetLayer < HaveLayer && (Layer > GetLayer + 1) && Switch == 0)
                 {
                     UIManager.instance.OpenPromptWindow("请先领取前面层级奖励", PromptWindow.PromptType.Hint, null, null);
                 }
                 else if (Switch == 1)
                 {
                     //UIManager.instance.OpenPromptWindow("已经领取奖励", PromptWindow.PromptType.Hint, null, null);
                 }
                 else
                 {
                     NetworkHandler.instance.SendProcess("6012#" + Layer + ";");
                 }
             };
        }

        if (UIEventListener.Get(Number2).onClick == null)
        {
            UIEventListener.Get(Number2).onClick += delegate(GameObject go)
            {
                if (Layer > HaveLayer && Switch == 0)
                {
                    //UIManager.instance.OpenPromptWindow("层级不足，无法领取", PromptWindow.PromptType.Hint, null, null);
                }
                else if (GetLayer < HaveLayer && (Layer > GetLayer + 1) && Switch == 0)
                {
                    UIManager.instance.OpenPromptWindow("请先领取前面层级奖励", PromptWindow.PromptType.Hint, null, null);
                }
                else if (Switch == 1)
                {
                    //UIManager.instance.OpenPromptWindow("已经领取奖励", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    NetworkHandler.instance.SendProcess("6012#" + Layer + ";");
                }
            };
        }
        if (UIEventListener.Get(Number3).onClick == null)
        {
            UIEventListener.Get(Number3).onClick += delegate(GameObject go)
            {
                if (Layer > HaveLayer && Switch == 0)
                {
                    //UIManager.instance.OpenPromptWindow("层级不足，无法领取", PromptWindow.PromptType.Hint, null, null);
                }
                else if (GetLayer < HaveLayer && (Layer > GetLayer + 1) && Switch == 0)
                {
                    UIManager.instance.OpenPromptWindow("请先领取前面层级奖励", PromptWindow.PromptType.Hint, null, null);
                }
                else if (Switch == 1)
                {
                    //UIManager.instance.OpenPromptWindow("已经领取奖励", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    NetworkHandler.instance.SendProcess("6012#" + Layer + ";");
                }
            };
        }
        if (UIEventListener.Get(Number4).onClick == null)
        {
            UIEventListener.Get(Number4).onClick += delegate(GameObject go)
            {
                if (Layer > HaveLayer && Switch == 0)
                {
                    //UIManager.instance.OpenPromptWindow("层级不足，无法领取", PromptWindow.PromptType.Hint, null, null);
                }
                else if (GetLayer < HaveLayer && (Layer > GetLayer + 1) && Switch == 0)
                {
                    UIManager.instance.OpenPromptWindow("请先领取前面层级奖励", PromptWindow.PromptType.Hint, null, null);
                }
                else if (Switch == 1)
                {
                    //UIManager.instance.OpenPromptWindow("已经领取奖励", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    NetworkHandler.instance.SendProcess("6012#" + Layer + ";");
                }
            };
        }
    }

    public void GetInfo(int AllMoneyNum) //领取成功时获得总收益
    {
            IconBg3.SetActive(false);
            PaiMing_ui.SetActive(false);
            IconBg.SetActive(true); 
            // GameObject.Find("Bg/BgTop").GetComponent<UISprite>().spriteName = "PVPranking6";
            //this.gameObject.transform.Find("Bg/BgTop").GetComponent<UISprite>().spriteName = "PVPranking1";
            //GameObject.Find("GetRankingReward/Title/IconBg1/Number1").GetComponent<UILabel>().text = (int.Parse(Number1.GetComponent<UILabel>().text) + int.Parse(GameObject.Find("GetRankingReward/Title/IconBg1/Number1").GetComponent<UILabel>().text)).ToString();
            //GameObject.Find("GetRankingReward/Title/IconBg2/Number2").GetComponent<UILabel>().text = (int.Parse(Number2.GetComponent<UILabel>().text) + int.Parse(GameObject.Find("GetRankingReward/Title/IconBg2/Number2").GetComponent<UILabel>().text)).ToString();
            //GameObject.Find("GetRankingReward/Title/IconBg3/Number3").GetComponent<UILabel>().text = (int.Parse(Number3.GetComponent<UILabel>().text) + int.Parse(GameObject.Find("GetRankingReward/Title/IconBg3/Number3").GetComponent<UILabel>().text)).ToString();
            //GameObject.Find("GetRankingReward/Title/IconBg4/Number4").GetComponent<UILabel>().text = (int.Parse(Number4.GetComponent<UILabel>().text) + int.Parse(GameObject.Find("GetRankingReward/Title/IconBg4/Number4").GetComponent<UILabel>().text)).ToString();
            //TextTranslator.instance.SetItemCountAddByID(9002,int.Parse(Number1.GetComponent<UILabel>().text));//背包增加奖励物品
            //TextTranslator.instance.SetItemCountAddByID(9001, int.Parse(Number2.GetComponent<UILabel>().text));

            GameObject.Find("GetRankingReward/Title/IconBg1/Number1").GetComponent<UILabel>().text = (int.Parse(Number1.transform.Find("Number1").GetComponent<UILabel>().text) + int.Parse(GameObject.Find("GetRankingReward/Title/IconBg1/Number1").GetComponent<UILabel>().text)).ToString();
            GameObject.Find("GetRankingReward/Title/IconBg3/Number3").GetComponent<UILabel>().text = (int.Parse(Number3.transform.Find("Number3").GetComponent<UILabel>().text) + int.Parse(GameObject.Find("GetRankingReward/Title/IconBg3/Number3").GetComponent<UILabel>().text)).ToString();
            GameObject.Find("GetRankingReward/Title/IconBg4/Number4").GetComponent<UILabel>().text = (int.Parse(Number4.transform.Find("Number4").GetComponent<UILabel>().text) + int.Parse(GameObject.Find("GetRankingReward/Title/IconBg4/Number4").GetComponent<UILabel>().text)).ToString();

            //int GetMoneyNum = AllMoneyNum+int.Parse(Number2.GetComponent<UILabel>().text);
            int GetMoneyNum = AllMoneyNum + int.Parse(Number2.transform.Find("Number2").GetComponent<UILabel>().text);
            
            GameObject.Find("GetRankingReward").GetComponent<GetRankingReward>().MoneyNum = GetMoneyNum;
            if (GetMoneyNum > 10000)
            {
                int a = GetMoneyNum / 10000;
                int b = GetMoneyNum / 1000 % 10;
                GameObject.Find("GetRankingReward/Title/IconBg2/Number2").GetComponent<UILabel>().text = a.ToString() + "." + b.ToString() + "W";// + c.ToString() + d.ToString() + e.ToString();
            }
            else 
            {
                GameObject.Find("GetRankingReward/Title/IconBg2/Number2").GetComponent<UILabel>().text = GetMoneyNum.ToString();
            }

            //TextTranslator.instance.SetItemCountAddByID(10101, int.Parse(GameObject.Find("GetRankingReward/Title/IconBg3/Number3").GetComponent<UILabel>().text));
            //TextTranslator.instance.SetItemCountAddByID(10401, int.Parse(GameObject.Find("GetRankingReward/Title/IconBg4/Number4").GetComponent<UILabel>().text));
            CharacterRecorder.instance.AddMoney(int.Parse(Number2.transform.Find("Number2").GetComponent<UILabel>().text));
            CharacterRecorder.instance.AddLunaGem(int.Parse(Number1.transform.Find("Number1").GetComponent<UILabel>().text));
            GameObject.Find("GetRankingReward/TopContent").GetComponent<TopContent>().Reset();
    }
    public void SetIconMation() //显示可领取图标
    {
        IconBg3.SetActive(true);
        PaiMing_ui.SetActive(true);
    }
    public void setInfo(int _Layer, int Rank, int Diamond, int Money, int ItemID1, int ItemNum1, int ItemID2, int ItemNum2, int _GetLayer, int _HaveLayer)//对应排名可领取的信息
    {
        Layer=_Layer;
        GetLayer = _GetLayer;
        HaveLayer = _HaveLayer;
        LabelRank.text = Rank.ToString();

        Number1.transform.Find("Number1").GetComponent<UILabel>().text = Diamond.ToString();
        Number1.GetComponent<ItemExplanation>().SetAwardItem(90002, 0);

        Number2.transform.Find("Number2").GetComponent<UILabel>().text = Money.ToString();
        Number2.GetComponent<ItemExplanation>().SetAwardItem(90001, 0);

        Number3.transform.Find("Icon").GetComponent<UISprite>().spriteName = ItemID1.ToString();
        Number3.transform.Find("Number3").GetComponent<UILabel>().text = ItemNum1.ToString();
        Number3.GetComponent<ItemExplanation>().SetAwardItem(ItemID1, 0);

        Number4.transform.Find("Icon").GetComponent<UISprite>().spriteName = ItemID2.ToString();
        Number4.transform.Find("Number4").GetComponent<UILabel>().text = ItemNum2.ToString();
        Number4.GetComponent<ItemExplanation>().SetAwardItem(ItemID2, 0);

        RankColor();
        if (_GetLayer >= _Layer) 
        {
            IconBg.SetActive(true);
            //this.gameObject.transform.Find("Bg/BgTop").GetComponent<UISprite>().spriteName = "PVPranking1";//已经领取的颜色
            Switch = 1;
        }
        if (Layer <= HaveLayer && Layer > GetLayer)//Layer == GetLayer + 1&&Layer<=HaveLayer  2016/6/8
        {
            IconBg3.SetActive(true);
            PaiMing_ui.SetActive(true);
        }
    }

    
    void RankColor() //框体颜色范围限定12.31
    {
        int Num=int.Parse(LabelRank.text);
        if (Num<= 6) 
        {
            this.gameObject.transform.Find("Bg/BgTop").GetComponent<UISprite>().spriteName = "PVPranking6";
        }
        else if (Num > 6 && Num <= 70) 
        {
            this.gameObject.transform.Find("Bg/BgTop").GetComponent<UISprite>().spriteName = "PVPranking5";
        }
        else if (Num > 70 && Num <= 600) 
        {
            this.gameObject.transform.Find("Bg/BgTop").GetComponent<UISprite>().spriteName = "PVPranking4";
        }
        else if (Num > 600 && Num <= 4000)
        {
            this.gameObject.transform.Find("Bg/BgTop").GetComponent<UISprite>().spriteName = "PVPranking3";
        }
        else if (Num > 4000 && Num <= 7000)
        {
            this.gameObject.transform.Find("Bg/BgTop").GetComponent<UISprite>().spriteName = "PVPranking2";
        }
        else
        {
            this.gameObject.transform.Find("Bg/BgTop").GetComponent<UISprite>().spriteName = "PVPranking1";
        }
    }
}
