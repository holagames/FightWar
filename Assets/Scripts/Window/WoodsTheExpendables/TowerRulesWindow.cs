using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TowerRulesWindow : MonoBehaviour
{
    public GameObject EscButton;
    private GameObject WoodsTheExpendables;
    public UILabel Rank;
    public UILabel ConditionLabel;
    public List<GameObject> AwardItem = new List<GameObject>();
    public string RankInfo;
    public int RankDicID;
    public GameObject Receive;
    // Use this for initialization
    void Start()
    {
        if (GameObject.Find("WoodsTheExpendables") != null)
        {
            WoodsTheExpendables = GameObject.Find("WoodsTheExpendables");
        }
        if (UIEventListener.Get(EscButton).onClick == null)
        {
            UIEventListener.Get(EscButton).onClick += delegate(GameObject obj)
            {
                WoodsTheExpendables.GetComponent<WoodsTheExpendables>().UpdateWindow(WoodsTheExpendablesWindowType.Right);
            };
        }
        if (UIEventListener.Get(Receive).onClick == null)
        {
            UIEventListener.Get(Receive).onClick += delegate(GameObject obj)
            {
                if (Receive.transform.Find("CanReceive"))
                {
                    UIManager.instance.OpenPromptWindow("已经领取",PromptWindow.PromptType.Hint,null,null);
                }
                else
                {
                  Debug.Log("发送信息并且，切换图片"); 
                }
            };
        }
        if (CharacterRecorder.instance.WoodsRankID != 0)
        {
            Rank.text = CharacterRecorder.instance.WoodsRankID.ToString();
        }
        else
        {
            Rank.text = "10000+"; 
        }
        ShowRankMessge(CharacterRecorder.instance.WoodsRankID);
    }

    public void ShowRankMessge(int NowRankID)
    {
        if (NowRankID != 0)
        {
            GetRankNumber(NowRankID);
            TowerRankReward RankReward = TextTranslator.instance.GetTowerRankRewardByID(RankDicID);
            AwardItem[0].transform.Find("itemNumber").GetComponent<UILabel>().text = RankReward.Gold.ToString();
            AwardItem[1].transform.Find("itemNumber").GetComponent<UILabel>().text = RankReward.SLCoin.ToString();
            ConditionLabel.text = "保持当前排名(" + RankInfo + "名)即可获得如下奖励";
        }
        else
        {
            AwardItem[0].SetActive(false);
            AwardItem[1].SetActive(false);
            ConditionLabel.text = "您还没有进行丛林冒险，无法获得奖励";
        }

    }
    public void GetRankNumber(int RankID)
    {
        if (RankID < 10)
        {
            switch (RankID)
            {
                case 1:
                    RankDicID = 1;
                    RankInfo = "第一";
                    break;
                case 2:
                    RankDicID = 2;
                    RankInfo = "第二";
                    break;
                case 3:
                    RankDicID = 3;
                    RankInfo = "第三";
                    break;
                case 4:
                    RankDicID = 4;
                    RankInfo = "第四";
                    break;
                case 5:
                    RankDicID = 5;
                    RankInfo = "第五";
                    break;
                case 6:
                    RankDicID = 6;
                    RankInfo = "第六";
                    break;
                case 7:
                    RankDicID = 7;
                    RankInfo = "第七";
                    break;
                case 8:
                    RankDicID = 8;
                    RankInfo = "第八";
                    break;
                case 9:
                    RankDicID = 9;
                    RankInfo = "第九";
                    break;
            }
        }
        else if (RankID >= 10 && RankID < 20)
        {
            RankDicID = 10;
            RankInfo = "10-20";
        }
        else if (RankID >= 20 && RankID < 30)
        {
            RankDicID = 11;
            RankInfo = "20-30";
        }
        else if (RankID >= 30 && RankID < 40)
        {
            RankDicID = 12;
            RankInfo = "30-40";
        }
        else if (RankID >= 40 && RankID < 50)
        {
            RankDicID = 13;
            RankInfo = "40-50";
        }
        else if (RankID >= 50 && RankID < 100)
        {
            RankDicID = 14;
            RankInfo = "50-60";
        }
        else if (RankID >= 100 && RankID < 200)
        {
            RankDicID = 15;
            RankInfo = "100-200";
        }
        else if (RankID >= 200 && RankID < 300)
        {
            RankDicID = 16;
            RankInfo = "200-300";
        }
        else if (RankID >= 300 && RankID < 500)
        {
            RankDicID = 17;
            RankInfo = "300-500";
        }
        else if (RankID >= 500 && RankID < 700)
        {
            RankDicID = 18;
            RankInfo = "500-700";
        }
        else if (RankID >= 700 && RankID < 1000)
        {
            RankDicID = 19;
            RankInfo = "700-1000";
        }
        else if (RankID >= 1000 && RankID < 2000)
        {
            RankDicID = 20;
            RankInfo = "1000-2000";
        }
        else if (RankID >= 2000 && RankID < 5000)
        {
            RankDicID = 21;
            RankInfo = "2000-5000";
        }
        else if (RankID >= 5000 && RankID < 10000)
        {
            RankDicID = 22;
            RankInfo = "5000-10000";
        }
        else
        {
            RankInfo = "大于10000";

        }
    }
}
