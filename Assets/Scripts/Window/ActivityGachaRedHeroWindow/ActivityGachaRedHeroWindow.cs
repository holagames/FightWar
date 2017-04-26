using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ActivityGachaRedHeroWindow : MonoBehaviour
{
    
    //button
    public GameObject QuestionButton;
    public GameObject OnceButton;
    public GameObject TenButton;
    public GameObject CloseButton;
    public List<GameObject> IntegraReward = new List<GameObject>();
    //hero
    public UILabel HeroName;
    public UITexture HeroTexture;
    public int HeroID;
    //RankMessage
    public GameObject RankItem;
    public UIGrid RankGrid;
    //RankIntegra
    public GameObject RankIntegraItem;
    public UIGrid RankIntegraGrid;
    //selfInfo
    public UILabel SelfRank;
    public UILabel SelfIntegra;
    public UILabel HaveDiamond;
    public bool isFreeNumber;
    //slider
    public UISlider IntegraSlider;
    //window
    public GameObject QuestionWindow;
    public GameObject RewardInfoWindow;
    public int OpenID;
    //atlas
    public UIAtlas HeroAtlas;
    public UIAtlas ItemAtlas;
    //时间
    public int Timer;
    public UILabel TimeLabel;
    public UILabel DayLabel;
    public TweenScale Guozhantweenscale;
    public TweenAlpha Guozhantweenalpha;
    public GameObject GuozhanEffectObj;
    //活动ActivityID
    int ActivityID = 0;
    // Use this for initialization
    void Start()
    {
        Animation();
        CharacterRecorder.instance.isRedHeroWindowFirst = true;
        UIEventListener.Get(QuestionButton).onClick = delegate(GameObject go)
        {
            QuestionWindow.SetActive(true);
            QuestionWindowMessage();
        };
        UIEventListener.Get(OnceButton).onClick = delegate(GameObject go)
       {
           if (isFreeNumber)
           {
               Debug.LogError("免费一次");
               CharacterRecorder.instance.heroIdList.Clear();
               foreach (var heroItem in CharacterRecorder.instance.ownedHeroList)
               {
                   CharacterRecorder.instance.heroIdList.Add(heroItem.cardID);
               }
               NetworkHandler.instance.SendProcess("5201#6;");
               DestroyImmediate(this.gameObject);
           }
           else
           {
               Debug.LogError("一次");
               if (CharacterRecorder.instance.lunaGem >= 240)
               {
                   CharacterRecorder.instance.heroIdList.Clear();
                   foreach (var heroItem in CharacterRecorder.instance.ownedHeroList)
                   {
                       CharacterRecorder.instance.heroIdList.Add(heroItem.cardID);
                   }
                   NetworkHandler.instance.SendProcess("5201#7;");
                   DestroyImmediate(this.gameObject);
               }
               else
               {
                   UIManager.instance.OpenPromptWindow("钻石不足", PromptWindow.PromptType.Hint, null, null);
               }
           }
       };
        UIEventListener.Get(TenButton).onClick = delegate(GameObject go)
        {
            Debug.LogError("十次");

            if (CharacterRecorder.instance.lunaGem >= 2280)
            {
                CharacterRecorder.instance.heroIdList.Clear();
                foreach (var heroItem in CharacterRecorder.instance.ownedHeroList)
                {
                    CharacterRecorder.instance.heroIdList.Add(heroItem.cardID);
                }
                NetworkHandler.instance.SendProcess("5202#8;");
                DestroyImmediate(this.gameObject);
            }
            else
            {
                UIManager.instance.OpenPromptWindow("钻石不足", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(CloseButton).onClick = delegate(GameObject go)
        {
            //UIManager.instance.BackUI();
            DestroyImmediate(this.gameObject);
            CharacterRecorder.instance.isRedHeroWindowFirst = false;
        };
        UIEventListener.Get(IntegraReward[0]).onClick += delegate(GameObject go)
        {
            RewardInfoWindow.SetActive(true);

            RewardInfoWindowMessage(0);
        };
        UIEventListener.Get(IntegraReward[1]).onClick += delegate(GameObject go)
        {
            RewardInfoWindow.SetActive(true);
            RewardInfoWindowMessage(1);
        };
        UIEventListener.Get(IntegraReward[2]).onClick += delegate(GameObject go)
        {
            RewardInfoWindow.SetActive(true);
            RewardInfoWindowMessage(2);
        };
        UIEventListener.Get(IntegraReward[3]).onClick += delegate(GameObject go)
        {
            RewardInfoWindow.SetActive(true);
            RewardInfoWindowMessage(3);
        };
        UIEventListener.Get(IntegraReward[4]).onClick += delegate(GameObject go)
        {
            RewardInfoWindow.SetActive(true);
            RewardInfoWindowMessage(4);
        };
        UIEventListener.Get(IntegraReward[5]).onClick += delegate(GameObject go)
        {
            RewardInfoWindow.SetActive(true);
            RewardInfoWindowMessage(5);
        };
        UIEventListener.Get(IntegraReward[6]).onClick += delegate(GameObject go)
        {
            RewardInfoWindow.SetActive(true);
            RewardInfoWindowMessage(6);
        };
        UIEventListener.Get(HeroTexture.gameObject).onClick += delegate(GameObject go)
        {
            this.gameObject.SetActive(false);
            UIManager.instance.OpenSinglePanel("CardDetailInfoWindow", false);
            GameObject.Find("CardDetailInfoWindow").GetComponent<CardDetailInfoWindow>().SetDetaiInfo(HeroID);
        };
       
    }

    /// <summary>
    /// 提醒
    /// </summary>
    void QuestionWindowMessage()
    {
        UIEventListener.Get(QuestionWindow.transform.Find("Question/CloseButton").gameObject).onClick = delegate(GameObject go)
        {
            QuestionWindow.SetActive(false);
        };
        QuestionWindow.transform.Find("Question/Message/Scroll View/Grid/1").GetComponent<UILabel>().text = "1.所有角色均可参与，活动期间内，限时抽奖可获得[ff0000]传说红将--" + TextTranslator.instance.GetItemByItemCode(HeroID).itemName + "[-]。";
        QuestionWindow.transform.Find("Question/Message/Scroll View/Grid/2").GetComponent<UILabel>().text = "2.每次十连抽，必出[ff0000]英雄[-]" + "或[ff0000]万能碎片[-]。";//TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetActivityGachaHeroRankDicByID(1).BonusID1).itemName

    }
    /// <summary>
    /// 宝箱信息
    /// </summary>
    /// <param name="id"></param>


    void RewardInfoWindowMessage(int id)
    {
        GameObject item = RewardInfoWindow.transform.Find("Window/Scroll View/Item").gameObject;
        UIGrid grid = RewardInfoWindow.transform.Find("Window/Scroll View/Grid").GetComponent<UIGrid>();
        List<GameObject> itemlist = new List<GameObject>();
        item.SetActive(false);
        for (int i = 0; i < 8; i++)
        {
            GameObject go = Instantiate(item) as GameObject;
            go.transform.parent = grid.transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.SetActive(true);
            itemlist.Add(go);
        }
        ActivityGachaHeroPoint gachaPoint = TextTranslator.instance.GetActivityGachaHeroPointDicByID(id + 1, ActivityID);
        for (int i = 0; i < itemlist.Count; i++)
        {
            switch (i)
            {
                case 0:
                    ItemInfo(gachaPoint.BonusID1, gachaPoint.BonusNum1, itemlist[i]);
                    break;
                case 1:
                    ItemInfo(gachaPoint.BonusID2, gachaPoint.BonusNum2, itemlist[i]);
                    break;
                case 2:
                    ItemInfo(gachaPoint.BonusID3, gachaPoint.BonusNum3, itemlist[i]);
                    break;
                case 3:
                    ItemInfo(gachaPoint.BonusID4, gachaPoint.BonusNum4, itemlist[i]);
                    break;
                case 4:
                    ItemInfo(gachaPoint.BonusID5, gachaPoint.BonusNum5, itemlist[i]);
                    break;
                case 5:
                    ItemInfo(gachaPoint.BonusID6, gachaPoint.BonusNum6, itemlist[i]);
                    break;
                case 6:
                    ItemInfo(gachaPoint.BonusID7, gachaPoint.BonusNum7, itemlist[i]);
                    break;
                case 7:
                    ItemInfo(gachaPoint.BonusID8, gachaPoint.BonusNum8, itemlist[i]);
                    break;
            }
        }
        grid.repositionNow = true;
        UIEventListener.Get(RewardInfoWindow.transform.Find("Window/CancleButton").gameObject).onClick = delegate(GameObject go)
       {
           RewardInfoWindow.SetActive(false);
           for (int i = 0; i < itemlist.Count; i++)
           {
               DestroyImmediate(itemlist[i]);
           }
           itemlist.Clear();
       };
        UIEventListener.Get(RewardInfoWindow.transform.Find("Window/SureButton").gameObject).onClick = delegate(GameObject go)
        {
            RewardInfoWindow.SetActive(false);
            for (int i = 0; i < itemlist.Count; i++)
            {
                DestroyImmediate(itemlist[i]);
            }
            itemlist.Clear();
            NetworkHandler.instance.SendProcess("9713#" + (id + 1).ToString() + ";");
            Debug.LogError("发送协议");
        };
        UIEventListener.Get(itemlist[0]).onClick = delegate(GameObject go)
        {
            TextTranslator.instance.ItemDescription(itemlist[0], TextTranslator.instance.GetItemByItemCode(gachaPoint.BonusID1).picID, 0);
        };
        UIEventListener.Get(itemlist[1]).onClick = delegate(GameObject go)
        {
            TextTranslator.instance.ItemDescription(itemlist[1], TextTranslator.instance.GetItemByItemCode(gachaPoint.BonusID2).picID, 0);
        };
        UIEventListener.Get(itemlist[2]).onClick = delegate(GameObject go)
        {
            TextTranslator.instance.ItemDescription(itemlist[2], TextTranslator.instance.GetItemByItemCode(gachaPoint.BonusID3).picID, 0);
        };
        UIEventListener.Get(itemlist[3]).onClick = delegate(GameObject go)
        {
            TextTranslator.instance.ItemDescription(itemlist[3], TextTranslator.instance.GetItemByItemCode(gachaPoint.BonusID4).picID, 0);
        };
        UIEventListener.Get(itemlist[4]).onClick = delegate(GameObject go)
        {
            TextTranslator.instance.ItemDescription(itemlist[4], TextTranslator.instance.GetItemByItemCode(gachaPoint.BonusID5).picID, 0);
        };
        UIEventListener.Get(itemlist[5]).onClick = delegate(GameObject go)
        {
            TextTranslator.instance.ItemDescription(itemlist[5], TextTranslator.instance.GetItemByItemCode(gachaPoint.BonusID6).picID, 0);
        };
        UIEventListener.Get(itemlist[6]).onClick = delegate(GameObject go)
        {
            TextTranslator.instance.ItemDescription(itemlist[6], TextTranslator.instance.GetItemByItemCode(gachaPoint.BonusID7).picID, 0);
        };
        UIEventListener.Get(itemlist[7]).onClick = delegate(GameObject go)
        {
            TextTranslator.instance.ItemDescription(itemlist[7], TextTranslator.instance.GetItemByItemCode(gachaPoint.BonusID8).picID, 0);
        };


        if (IntegraReward[id].transform.Find("BoxClose1").gameObject.activeSelf)
        {
            RewardInfoWindow.transform.Find("Window/SureButton").GetComponent<UISprite>().spriteName = "buttonHui";
            RewardInfoWindow.transform.Find("Window/SureButton/Label").GetComponent<UILabel>().text = "不可领";
            RewardInfoWindow.transform.Find("Window/SureButton/Label").GetComponent<UILabel>().effectStyle = UILabel.Effect.None;
        }
        else if (IntegraReward[id].transform.Find("WF_HeZi_UIeff").gameObject.activeSelf)
        {
            RewardInfoWindow.transform.Find("Window/SureButton").GetComponent<UISprite>().spriteName = "ui2_button4";
            RewardInfoWindow.transform.Find("Window/SureButton/Label").GetComponent<UILabel>().text = "领 取";
            RewardInfoWindow.transform.Find("Window/SureButton/Label").GetComponent<UILabel>().effectStyle = UILabel.Effect.Outline;
        }
        else if (IntegraReward[id].transform.Find("BoxOpen1").gameObject.activeSelf)
        {
            RewardInfoWindow.transform.Find("Window/SureButton").GetComponent<UISprite>().spriteName = "buttonHui";
            RewardInfoWindow.transform.Find("Window/SureButton/Label").GetComponent<UILabel>().text = "已 领";
            RewardInfoWindow.transform.Find("Window/SureButton/Label").GetComponent<UILabel>().effectStyle = UILabel.Effect.None;
        }
    }
    void ItemInfo(int BonusID, int BonusNum, GameObject go)
    {
        if (BonusID == 0)
        {
            go.SetActive(false);
        }
        else
        {
            go.SetActive(true);
            go.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(BonusID).itemGrade.ToString();
            go.transform.Find("Icon").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(BonusID).picID.ToString();
            if (BonusNum < 10000)
            {
                go.transform.Find("Label").GetComponent<UILabel>().text = BonusNum.ToString();
            }
            else
            {
                go.transform.Find("Label").GetComponent<UILabel>().text = ((float)BonusNum / 10000).ToString("f1") + "w";
            }
            if (60000 < BonusID && BonusID < 70000)
            {
                go.transform.Find("Icon").GetComponent<UISprite>().atlas = HeroAtlas;
            }
            else if (70000 < BonusID && BonusID < 79999)
            {
                go.transform.Find("Icon").GetComponent<UISprite>().atlas = HeroAtlas;
            }
            else
            {
                go.transform.Find("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            }
            if (70000 < BonusID && BonusID < 79999)
            {
                go.transform.Find("Framgent").gameObject.SetActive(true);
            }
            else if (BonusID == 70000 || BonusID == 79999) 
            {
                go.transform.Find("Framgent").gameObject.SetActive(true);
            }
            else if (80000 < BonusID && BonusID < 99999)
            {
                go.transform.Find("Framgent").gameObject.SetActive(true);
            }
            else
            {
                go.transform.Find("Framgent").gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// 奖励预览
    /// </summary>

    void RankMessageInfo()
    {
        if (RankGrid.transform.childCount != 0)
        {
            Debug.LogError("ssssssss");
        }
        int length = TextTranslator.instance.GetActivityGachaHeroRankDicLength(ActivityID);
        RankItem.SetActive(false);
        for (int i = 1; i < length; i++)
        {
            GameObject go = Instantiate(RankItem) as GameObject;
            go.transform.parent = RankGrid.transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.SetActive(true);
            ActivityGachaHeroRank HeroRank = TextTranslator.instance.GetActivityGachaHeroRankDicByID(i, ActivityID);
            go.GetComponent<UILabel>().text = "第" + RankLabelInfo(i) + "名\n"
                                                 + RankItemLabelInfo(HeroRank.BonusID2) + " " + TextTranslator.instance.GetItemByItemCode(HeroRank.BonusID2).itemName + " " + ReturnNumber(HeroRank.BonusNum2)
                                                 + RankItemLabelInfo(HeroRank.BonusID3) + " " + TextTranslator.instance.GetItemByItemCode(HeroRank.BonusID3).itemName + " " + ReturnNumber(HeroRank.BonusNum3)
                                                  + RankItemLabelInfo(HeroRank.BonusID4) + " " + TextTranslator.instance.GetItemByItemCode(HeroRank.BonusID4).itemName + " " + ReturnNumber(HeroRank.BonusNum4)
                                                  + RankItemLabelInfo(HeroRank.BonusID5) + " " + TextTranslator.instance.GetItemByItemCode(HeroRank.BonusID5).itemName + " " + ReturnNumber(HeroRank.BonusNum5)
                                                  + RankItemLabelInfo(HeroRank.BonusID6) + " " + TextTranslator.instance.GetItemByItemCode(HeroRank.BonusID6).itemName + " " + ReturnNumber(HeroRank.BonusNum6)
                                                  + RankItemLabelInfo(HeroRank.BonusID7) + " " + TextTranslator.instance.GetItemByItemCode(HeroRank.BonusID7).itemName + " " + ReturnNumber(HeroRank.BonusNum7)
                                                  + RankItemLabelInfo(HeroRank.BonusID8) + " " + TextTranslator.instance.GetItemByItemCode(HeroRank.BonusID8).itemName + " " + ReturnNumber(HeroRank.BonusNum8);
            if (i < 4)
            {
                go.transform.Find("Label").GetComponent<UILabel>().text = RankItemLabelInfo(HeroRank.BonusID1) + TextTranslator.instance.GetItemByItemCode(HeroRank.BonusID1).itemName + " " + ReturnNumber(HeroRank.BonusNum1);
            }
        }
        HeroID = TextTranslator.instance.GetItemByItemCode(TextTranslator.instance.GetActivityGachaHeroRankDicByID(1, ActivityID).BonusID1).picID;
        HeroName.text = TextTranslator.instance.GetItemByItemCode(HeroID).itemName;
        SetRarityIcon(TextTranslator.instance.GetHeroInfoByHeroID(HeroID).heroRarity, HeroName.transform.Find("Type").GetComponent<UISprite>());
        HeroName.transform.Find("Kind").GetComponent<UISprite>().spriteName = "heroType" + TextTranslator.instance.GetHeroInfoByHeroID(HeroID).heroCarrerType.ToString();
        HeroTexture.mainTexture = Resources.Load("Loading/" + HeroID.ToString()) as Texture;
        HeroTexture.MakePixelPerfect();
        RankGrid.repositionNow = true;
       
    }
    string RankLabelInfo(int RankID)
    {
        string label = "";
        if (RankID < 4)
        {
            label = "[d28e47]" + RankID + "[-]";
        }
        else if (RankID > 10)
        {
            label = "[3a6a85]" + RankID + "[-]";
        }
        else
        {
            label = "[bb44ff]" + RankID + "[-]";
        }
        return label;
    }
    string RankItemLabelInfo(int itemid)
    {
        string NameColor = "";
        switch (TextTranslator.instance.GetItemByItemCode(itemid).itemGrade)
        {
            case 1:
                NameColor = "[-][B3B3B3]";
                break;
            case 2:
                NameColor = "[-][28DF5E]";
                break;
            case 3:
                NameColor = "[-][12A7B8]";
                break;
            case 4:
                NameColor = "[-][842DCE]";
                break;
            case 5:
                NameColor = "[-][DC582D]";
                break;
            case 6:
                NameColor = "[-][D9181E]";
                break;
        }
        return NameColor;
    }
    void SetRarityIcon(int id, UISprite go)
    {
        // Debug.LogError(_heroInfo.heroRarity);
        switch (id)
        {
            case 1:
                go.spriteName = "word4";
                break;
            case 2:
                go.spriteName = "word5";
                break;
            case 3:
                go.spriteName = "word6";
                break;
            case 4:
                go.spriteName = "word7";
                break;
            case 5:
                go.spriteName = "word8";
                break;
            default:
                break;
        }
    }
    string ReturnNumber(int num)
    {
        if (num > 0)
        {
            return num.ToString();
        }
        return "";
    }
    /// <summary>
    /// 积分排行
    /// </summary>

    public void RankIntegraInfo(string _RankIntegra, string _SelfIntegra, string _SelfRank, string timer, int _ActivityID)
    {
        ActivityID = _ActivityID;
        string[] ranklist = _RankIntegra.Split('!');

        SelfIntegra.text = _SelfIntegra;
        RankIntegraItem.SetActive(false);
        if (RankIntegraGrid.transform.childCount != 0)
        {
            Debug.LogError("ssssssss");
        }
        for (int i = 0; i < ranklist.Length - 1; i++)
        {
            GameObject go = Instantiate(RankIntegraItem) as GameObject;
            go.transform.parent = RankIntegraGrid.transform;
            go.transform.localPosition = Vector3.zero;
            go.transform.localScale = Vector3.one;
            go.SetActive(true);
            go.transform.Find("RankLabel").gameObject.SetActive(false);
            go.transform.Find("RankSprite").gameObject.SetActive(false);
            //if (int.Parse(ranklist[i].Split('$')[0]) < 4)
            //{
            //    go.transform.Find("RankSprite").gameObject.SetActive(true);
            //    go.transform.Find("RankSprite").GetComponent<UISprite>().spriteName = "word" + ranklist[i].Split('$')[0];
            //}
            //else
            {
                go.transform.Find("RankLabel").gameObject.SetActive(true);
                go.transform.Find("RankLabel").GetComponent<UILabel>().text = RankLabelInfo(int.Parse(ranklist[i].Split('$')[0]));
            }
            go.transform.Find("Name").GetComponent<UILabel>().text = "[3a6a85]" + ranklist[i].Split('$')[1];
            go.transform.Find("Point").GetComponent<UILabel>().text = "[3a6a85]" + ranklist[i].Split('$')[2];
        }
        if (int.Parse(_SelfRank) <= 20)
        {
            SelfRank.text = "[3a6a85]第[-] [3ee817]" + (_SelfRank) + "[-] [3a6a85]名[-]";
        }
        else
        {
            SelfRank.text = "未上榜";
        }
        Timer = int.Parse(timer);
        CancelInvoke("UpdateTime");
        InvokeRepeating("UpdateTime", 0, 1.0f);
        HaveDiamond.text = CharacterRecorder.instance.lunaGem.ToString();
        for (int i = 1; i < 8; i++)
        {
            if (int.Parse(_SelfIntegra) >= TextTranslator.instance.GetActivityGachaHeroPointDicByID(i, ActivityID).Point)
            {
                if (i != 7)
                {
                    float morePoint = (float)(int.Parse(_SelfIntegra) - TextTranslator.instance.GetActivityGachaHeroPointDicByID(i, ActivityID).Point);
                    float PointValue = (float)(TextTranslator.instance.GetActivityGachaHeroPointDicByID(i + 1, ActivityID).Point - TextTranslator.instance.GetActivityGachaHeroPointDicByID(i, ActivityID).Point);
                    IntegraSlider.value = 0.143f * (i + morePoint / PointValue);
                }
                else
                {
                    IntegraSlider.value = 1f;
                }
            }
            else if (int.Parse(_SelfIntegra) < TextTranslator.instance.GetActivityGachaHeroPointDicByID(1, ActivityID).Point)
            {
                //float morePoint = (float)(TextTranslator.instance.GetActivityGachaHeroPointDicByID(1).Point - int.Parse(_SelfIntegra));
                IntegraSlider.value = 0.143f * (float.Parse(_SelfIntegra) / TextTranslator.instance.GetActivityGachaHeroPointDicByID(1, ActivityID).Point);
            }
        }
        RankIntegraGrid.repositionNow = true;
        RankMessageInfo();
    }
    /// <summary>
    /// 抽奖和宝箱信息
    /// </summary>
    public void SetGachaInfo(string[] rewardSplit)
    {
        if (CharacterRecorder.instance.GachaMore == 1)
        {
            OnceButton.transform.Find("OnceCost").gameObject.SetActive(false);
            OnceButton.transform.Find("FreeNumber").gameObject.SetActive(true);
            isFreeNumber = true;
        }
        else
        {
            OnceButton.transform.Find("OnceCost").gameObject.SetActive(true);
            OnceButton.transform.Find("FreeNumber").gameObject.SetActive(false);
            isFreeNumber = false;
        }

        for (int i = 0; i < 7; i++)
        {
            Debug.LogError(rewardSplit[i]);
            IntegraReward[i].transform.Find("Label").GetComponent<UILabel>().text = TextTranslator.instance.GetActivityGachaHeroPointDicByID(i + 1, ActivityID).Point.ToString();
            IntegraReward[i].transform.Find("BoxClose1").gameObject.SetActive(false);
            IntegraReward[i].transform.Find("BoxOpen1").gameObject.SetActive(false);
            IntegraReward[i].transform.Find("BoxOpen1Bg").gameObject.SetActive(false);
            IntegraReward[i].transform.Find("WF_HeZi_UIeff").gameObject.SetActive(false);
            if (rewardSplit[i + 7 * ActivityID] == "0")
            {
                IntegraReward[i].transform.Find("BoxClose1").gameObject.SetActive(true);
            }
            else if (rewardSplit[i + 7 * ActivityID] == "1")
            {
                //IntegraReward[i].transform.Find("BoxClose1").gameObject.SetActive(true);
                IntegraReward[i].transform.Find("WF_HeZi_UIeff").gameObject.SetActive(true);
            }
            else
            {
                IntegraReward[i].transform.Find("BoxOpen1").gameObject.SetActive(true);
            }
        }
    }
    void UpdateTime()
    {
        if (Timer > 0)
        {
            string day = (Timer / 3600 / 24).ToString();
            string houre = (Timer / 3600 % 24).ToString();
            string min = (Timer % 3600 / 60).ToString("d2");
            string scond = (Timer % 60).ToString("d2");
            Timer -= 1;
            TimeLabel.text = houre + ":" + min + ":" + scond;
            DayLabel.text = day;
        }
    }
    void Animation()
    {
        StopCoroutine("IESetGuozhanEffect");
        StartCoroutine("IESetGuozhanEffect");
    }
    IEnumerator IESetGuozhanEffect()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1F);

            int num = 0;
            while (num < 12)
            {
                num++;
                GuozhanEffectObj.SetActive(true);
                Guozhantweenalpha.from = 0.5f;
                Guozhantweenalpha.to = 1f;
                Guozhantweenalpha.duration = 0.1f;
                Guozhantweenalpha.delay = 0f;

                Guozhantweenscale.from = new Vector3(0.5f, 0.5f, 0.5f);
                Guozhantweenscale.to = new Vector3(1f, 1f, 1f);
                Guozhantweenscale.duration = 0.1f;
                Guozhantweenscale.delay = 0f;

                Guozhantweenalpha.ResetToBeginning();
                Guozhantweenscale.ResetToBeginning();
                Guozhantweenalpha.PlayForward();
                Guozhantweenscale.PlayForward();
                yield return new WaitForSeconds(2f);

                Guozhantweenalpha.from = 1f;
                Guozhantweenalpha.to = 0.5f;
                Guozhantweenalpha.duration = 0.1f;
                Guozhantweenalpha.delay = 0f;

                Guozhantweenscale.from = new Vector3(1f, 1f, 1f);
                Guozhantweenscale.to = new Vector3(0.5f, 0.5f, 0.5f);
                Guozhantweenscale.duration = 0.1f;
                Guozhantweenscale.delay = 0f;

                Guozhantweenalpha.ResetToBeginning();
                Guozhantweenscale.ResetToBeginning();
                Guozhantweenalpha.PlayForward();
                Guozhantweenscale.PlayForward();

                //tweenalpha.PlayReverse();
                //tweenscale.PlayReverse();
                yield return new WaitForSeconds(0.1f);
                GuozhanEffectObj.SetActive(false);
            }
        }

    }
}
