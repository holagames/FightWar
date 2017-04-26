using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MeritAwardWindow : MonoBehaviour {


    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;

    public GameObject SpriteTab1;
    public GameObject SpriteTab2;
    public GameObject SpriteTab3;

    public GameObject TabPart1;
    public GameObject TabPart2;
    public GameObject TabPart3;

    public GameObject ItemAward;
    public GameObject Box1;
    public GameObject Box2;
    public GameObject Box3;
    public GameObject Box4;
    public GameObject Box5;

    public GameObject Integral1;
    public GameObject Integral2;
    public GameObject Integral3;
    public GameObject Integral4;
    public GameObject Integral5;

    public GameObject JungongGrid;
    public GameObject SanjiaoSprite;
    public GameObject GetButton;
    public UILabel JungongNumber;
    public UISlider uiSlider;


    public GameObject JunxianGrid;
    public GameObject JunxianItem;

    public GameObject ShadiGrid;
    public GameObject ShadiItem;

    public GameObject CloseButton;
    private int IsBox1 = 0;
    private int IsBox2 = 0;
    private int IsBox3 = 0;
    private int IsBox4 = 0;
    private int IsBox5 = 0;

    private int Box1Points = 0;
    private int Box2Points = 0;
    private int Box3Points = 0;
    private int Box4Points = 0;
    private int Box5Points = 0;

    private int JunxianWeizhi = 0;  //位置，军衔图片用
    Dictionary<int, int> JunxianDic = new Dictionary<int, int>();

    void OnEnable()
    {
        //uiSlider.value = 0f;
        //NetworkHandler.instance.SendProcess("8630#;");
    }

	void Start () {
        foreach (Transform tran in GetComponentsInChildren<Transform>())
        {
            tran.gameObject.layer = 9;
        }
        Box1Points = TextTranslator.instance.GetBattlefieldPointsByID(1).Points;
        Box2Points = TextTranslator.instance.GetBattlefieldPointsByID(2).Points;
        Box3Points = TextTranslator.instance.GetBattlefieldPointsByID(3).Points;
        Box4Points = TextTranslator.instance.GetBattlefieldPointsByID(4).Points;
        Box5Points = TextTranslator.instance.GetBattlefieldPointsByID(5).Points;
        uiSlider.value = 0f;
        NetworkHandler.instance.SendProcess("8630#;");
        UIEventListener.Get(SpriteTab1).onClick = delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("8630#;");
        };

        UIEventListener.Get(SpriteTab2).onClick = delegate(GameObject go)
        {
            SetJunXianAward();
        };

        UIEventListener.Get(SpriteTab3).onClick = delegate(GameObject go)
        {
            SetShadiAward();
        };

        UIEventListener.Get(CloseButton).onClick = delegate(GameObject go)
        {
            DestroyImmediate(this.gameObject);
        };

        #region  宝箱点击
        if (UIEventListener.Get(Box1).onClick == null)
        {
            UIEventListener.Get(Box1).onClick += delegate(GameObject go)
            {
                Debug.Log("baoxiang1");

                if (IsBox1 == 1)
                {
                    NetworkHandler.instance.SendProcess("8631#" + 1 + ";");
                }
                else 
                {
                    SetAwardLockInfo(1);
                    SetGetButtonType(1);
                }
            };
        }

        //if (UIEventListener.Get(Integral1).onClick == null)
        //{
        //    UIEventListener.Get(Integral1).onClick += delegate(GameObject go)
        //    {
        //        SetAwardLockInfo(1);
        //        SetGetButtonType(1);
        //    };
        //}
        if (UIEventListener.Get(Box2).onClick == null)
        {
            UIEventListener.Get(Box2).onClick += delegate(GameObject go)
            {
                if (IsBox2 == 1)
                {
                    NetworkHandler.instance.SendProcess("8631#" + 2 + ";");
                }
                else 
                {

                    SetAwardLockInfo(2);
                    SetGetButtonType(2);
                }
            };
        }

        //if (UIEventListener.Get(Integral2).onClick == null)
        //{
        //    UIEventListener.Get(Integral2).onClick += delegate(GameObject go)
        //    {
        //        SetAwardLockInfo(2);
        //        SetGetButtonType(2);
        //    };
        //}
        if (UIEventListener.Get(Box3).onClick == null)
        {
            UIEventListener.Get(Box3).onClick += delegate(GameObject go)
            {

                if (IsBox3 == 1)
                {
                    NetworkHandler.instance.SendProcess("8631#" + 3 + ";");
                }
                else 
                {
                    SetAwardLockInfo(3);
                    SetGetButtonType(3);
                }
            };
        }

        //if (UIEventListener.Get(Integral3).onClick == null)
        //{
        //    UIEventListener.Get(Integral3).onClick += delegate(GameObject go)
        //    {
        //        SetAwardLockInfo(3);
        //        SetGetButtonType(3);
        //    };
        //}
        if (UIEventListener.Get(Box4).onClick == null)
        {
            UIEventListener.Get(Box4).onClick += delegate(GameObject go)
            {

                if (IsBox4 == 1)
                {
                    NetworkHandler.instance.SendProcess("8631#" + 4 + ";");
                }
                else 
                {
                    SetAwardLockInfo(4);
                    SetGetButtonType(4);
                }
            };
        }
        //if (UIEventListener.Get(Integral4).onClick == null)
        //{
        //    UIEventListener.Get(Integral4).onClick += delegate(GameObject go)
        //    {
        //        SetAwardLockInfo(4);
        //        SetGetButtonType(4);
        //    };
        //}
        if (UIEventListener.Get(Box5).onClick == null)
        {
            UIEventListener.Get(Box5).onClick += delegate(GameObject go)
            {
                if (IsBox5 == 1)
                {
                    NetworkHandler.instance.SendProcess("8631#" + 5 + ";");
                }
                else 
                {
                    SetAwardLockInfo(5);
                    SetGetButtonType(5);
                }
            };
        }
        //if (UIEventListener.Get(Integral5).onClick == null)
        //{
        //    UIEventListener.Get(Integral5).onClick += delegate(GameObject go)
        //    {
        //        SetAwardLockInfo(5);
        //        SetGetButtonType(5);
        //    };
        //}
        #endregion
    }

    public void SetHappyBoxInfo()//string Receive
    {
        TabPart1.SetActive(true);
        TabPart2.SetActive(false);
        TabPart3.SetActive(false);
        string Receive = CharacterRecorder.instance.MilitaryExploitInfo;
        string[] dataSplit = Receive.Split(';');
        string[] RewardSplit = dataSplit[1].Split('$');

        IsBox1 = int.Parse(RewardSplit[0]);
        IsBox2 = int.Parse(RewardSplit[1]);
        IsBox3 = int.Parse(RewardSplit[2]);
        IsBox4 = int.Parse(RewardSplit[3]);
        IsBox5 = int.Parse(RewardSplit[4]);

        JungongNumber.text = dataSplit[0];
        #region 宝箱状态
        if (IsBox1 != 2)
        {
            Box1.transform.Find("BoxOpen1").gameObject.SetActive(false);
            if (IsBox1 == 1)
            {
                Box1.transform.Find("WF_HeZi_UIeff").gameObject.SetActive(true);
                Box1.transform.Find("BoxClose1").gameObject.SetActive(false);
            }
            else
            {
                Box1.transform.Find("BoxClose1").gameObject.SetActive(true);
            }
        }
        else
        {
            Box1.transform.Find("BoxClose1").gameObject.SetActive(false);
            Box1.transform.Find("BoxOpen1").gameObject.SetActive(true);
            Box1.transform.Find("WF_HeZi_UIeff").gameObject.SetActive(false);
        }

        if (IsBox2 != 2)
        {
            Box2.transform.Find("BoxOpen2").gameObject.SetActive(false);
            if (IsBox2 == 1)
            {
                Box2.transform.Find("WF_HeZi_UIeff").gameObject.SetActive(true);
                Box2.transform.Find("BoxClose2").gameObject.SetActive(false);
            }
            else
            {
                Box2.transform.Find("BoxClose2").gameObject.SetActive(true);
            }
        }
        else
        {
            Box2.transform.Find("BoxClose2").gameObject.SetActive(false);
            Box2.transform.Find("BoxOpen2").gameObject.SetActive(true);
            Box2.transform.Find("WF_HeZi_UIeff").gameObject.SetActive(false);
        }

        if (IsBox3 != 2)
        {
            Box3.transform.Find("BoxOpen3").gameObject.SetActive(false);
            if (IsBox3 == 1)
            {
                Box3.transform.Find("WF_HeZi_UIeff").gameObject.SetActive(true);
                Box3.transform.Find("BoxClose3").gameObject.SetActive(false);
            }
            else
            {
                Box3.transform.Find("BoxClose3").gameObject.SetActive(true);
            }
        }
        else
        {
            Box3.transform.Find("BoxClose3").gameObject.SetActive(false);
            Box3.transform.Find("BoxOpen3").gameObject.SetActive(true);
            Box3.transform.Find("WF_HeZi_UIeff").gameObject.SetActive(false);
        }

        if (IsBox4 != 2)
        {
            Box4.transform.Find("BoxOpen4").gameObject.SetActive(false);
            if (IsBox4 == 1)
            {
                Box4.transform.Find("WF_HeZi_UIeff").gameObject.SetActive(true);
                Box4.transform.Find("BoxClose4").gameObject.SetActive(false);
            }
            else
            {
                Box4.transform.Find("BoxClose4").gameObject.SetActive(true);
            }
        }
        else
        {
            Box4.transform.Find("BoxClose4").gameObject.SetActive(false);
            Box4.transform.Find("BoxOpen4").gameObject.SetActive(true);
            Box4.transform.Find("WF_HeZi_UIeff").gameObject.SetActive(false);
        }

        if (IsBox5 != 2)
        {
            Box5.transform.Find("BoxOpen5").gameObject.SetActive(false);
            if (IsBox5 == 1)
            {
                Box5.transform.Find("WF_HeZi_UIeff").gameObject.SetActive(true);
                Box5.transform.Find("BoxClose5").gameObject.SetActive(false);
            }
            else
            {
                Box5.transform.Find("BoxClose5").gameObject.SetActive(true);
            }
        }
        else
        {
            Box5.transform.Find("BoxClose5").gameObject.SetActive(false);
            Box5.transform.Find("BoxOpen5").gameObject.SetActive(true);
            Box5.transform.Find("WF_HeZi_UIeff").gameObject.SetActive(false);
        }

        #endregion


        int jungongNum=int.Parse(dataSplit[0]);  //滑动条分级滑动
        if (jungongNum <= Box1Points) 
        {
            uiSlider.value = 0.2f / Box1Points * jungongNum;
        }
        else if (jungongNum <= Box2Points) 
        {
            uiSlider.value = 0.2f + 0.2f / (Box2Points - Box1Points) * (jungongNum - Box1Points);
        }
        else if (jungongNum <= Box3Points)
        {
            uiSlider.value = 0.4f + 0.2f / (Box3Points - Box2Points) * (jungongNum - Box2Points);
        }
        else if (jungongNum <= Box4Points)
        {
            uiSlider.value = 0.6f + 0.2f / (Box4Points - Box3Points) * (jungongNum - Box3Points);
        }
        else if (jungongNum <= Box5Points)
        {
            uiSlider.value = 0.8f + 0.2f / (Box5Points - Box4Points) * (jungongNum - Box4Points);
        }
        else if (jungongNum > Box5Points) 
        {
            uiSlider.value = 1f;
        }


        if (IsBox1 == 1)   //按钮颜色
        {
            Integral1.GetComponent<UISprite>().spriteName = "guozhantiao2";
            Integral1.transform.Find("JungongNum").GetComponent<UILabel>().text = Box1Points.ToString();
            Integral1.transform.Find("JungongNum").GetComponent<UILabel>().effectColor = new Color(185 / 255f, 87 / 255f, 8 / 255f, 255 / 255f);
        }
        else 
        {
            Integral1.GetComponent<UISprite>().spriteName = "guozhantiao3";
            Integral1.transform.Find("JungongNum").GetComponent<UILabel>().text = Box1Points.ToString();
            Integral1.transform.Find("JungongNum").GetComponent<UILabel>().effectColor = new Color(90 / 255f, 90 / 255f, 90 / 255f, 255 / 255f);
        }

        if (IsBox2 == 1)
        {
            Integral2.GetComponent<UISprite>().spriteName = "guozhantiao2";
            Integral2.transform.Find("JungongNum").GetComponent<UILabel>().text = Box2Points.ToString();
            Integral2.transform.Find("JungongNum").GetComponent<UILabel>().effectColor = new Color(185 / 255f, 87 / 255f, 8 / 255f, 255 / 255f);
        }
        else
        {
            Integral2.GetComponent<UISprite>().spriteName = "guozhantiao3";
            Integral2.transform.Find("JungongNum").GetComponent<UILabel>().text = Box2Points.ToString();
            Integral2.transform.Find("JungongNum").GetComponent<UILabel>().effectColor = new Color(90 / 255f, 90 / 255f, 90 / 255f, 255 / 255f);
        }

        if (IsBox3 == 1)
        {
            Integral3.GetComponent<UISprite>().spriteName = "guozhantiao2";
            Integral3.transform.Find("JungongNum").GetComponent<UILabel>().text = Box3Points.ToString();
            Integral3.transform.Find("JungongNum").GetComponent<UILabel>().effectColor = new Color(185 / 255f, 87 / 255f, 8 / 255f, 255 / 255f);
        }
        else
        {
            Integral3.GetComponent<UISprite>().spriteName = "guozhantiao3";
            Integral3.transform.Find("JungongNum").GetComponent<UILabel>().text = Box3Points.ToString();
            Integral3.transform.Find("JungongNum").GetComponent<UILabel>().effectColor = new Color(90 / 255f, 90 / 255f, 90 / 255f, 255 / 255f);
        }


        if (IsBox4 == 1)
        {
            Integral4.GetComponent<UISprite>().spriteName = "guozhantiao2";
            Integral4.transform.Find("JungongNum").GetComponent<UILabel>().text = Box4Points.ToString();
            Integral4.transform.Find("JungongNum").GetComponent<UILabel>().effectColor = new Color(185 / 255f, 87 / 255f, 8 / 255f, 255 / 255f);
        }
        else
        {
            Integral4.GetComponent<UISprite>().spriteName = "guozhantiao3";
            Integral4.transform.Find("JungongNum").GetComponent<UILabel>().text = Box4Points.ToString();
            Integral4.transform.Find("JungongNum").GetComponent<UILabel>().effectColor = new Color(90 / 255f, 90 / 255f, 90 / 255f, 255 / 255f);
        }

        if (IsBox5 == 1)
        {
            Integral5.GetComponent<UISprite>().spriteName = "guozhantiao2";
            Integral5.transform.Find("JungongNum").GetComponent<UILabel>().text = Box5Points.ToString();
            Integral5.transform.Find("JungongNum").GetComponent<UILabel>().effectColor = new Color(185 / 255f, 87 / 255f, 8 / 255f, 255 / 255f);
        }
        else
        {
            Integral5.GetComponent<UISprite>().spriteName = "guozhantiao3";
            Integral5.transform.Find("JungongNum").GetComponent<UILabel>().text = Box5Points.ToString();
            Integral5.transform.Find("JungongNum").GetComponent<UILabel>().effectColor = new Color(90 / 255f, 90 / 255f, 90 / 255f, 255 / 255f);
        }

        if (IsBox1 == 1)   //开始时对应奖励预览
        {
            SetAwardLockInfo(1);
            SetGetButtonType(1);
        }
        else if (IsBox2 == 1) 
        {
            SetAwardLockInfo(2);
            SetGetButtonType(2);
        }
        else if (IsBox3 == 1) 
        {
            SetAwardLockInfo(3);
            SetGetButtonType(3);
        }
        else if (IsBox4 == 1) 
        {
            SetAwardLockInfo(4);
            SetGetButtonType(4);
        }
        else if (IsBox5 == 1)
        {
            SetAwardLockInfo(5);
            SetGetButtonType(5);
        }
        else 
        {
            SetAwardLockInfo(1);
            SetGetButtonType(1);
        }
    }

    private void SetGetButtonType(int num) //按钮颜色状态
    {
        int Type=0;
        if (num == 1) 
        {
            Type = IsBox1;
        }
        else if (num == 2) 
        {
            Type = IsBox2;
        }
        else if (num == 3)
        {
            Type = IsBox3;
        }
        else if (num == 4)
        {
            Type = IsBox4;
        }
        else if (num == 5)
        {
            Type = IsBox5;
        }

        if (Type == 1)
        {
            GetButton.GetComponent<UISprite>().spriteName = "ui2_button4";
            GetButton.GetComponent<UIButton>().normalSprite = "ui2_button4";
            GetButton.GetComponent<UIButton>().hoverSprite = "ui2_button4";
            GetButton.GetComponent<UIButton>().pressedSprite = "ui2_button4_an";
            GetButton.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(211 / 255f, 88 / 255f, 13 / 255f, 255 / 255f);
        }
        else 
        {
            GetButton.GetComponent<UISprite>().spriteName = "buttonHui";
            GetButton.GetComponent<UIButton>().normalSprite = "buttonHui";
            GetButton.GetComponent<UIButton>().hoverSprite = "buttonHui";
            GetButton.GetComponent<UIButton>().pressedSprite = "buttonHui_an";
            GetButton.transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(150 / 255f, 150 / 255f, 150 / 255f, 255 / 255f);
        }

        UIEventListener.Get(GetButton).onClick = delegate(GameObject go)
        {
            if (Type == 0) 
            {
                UIManager.instance.OpenPromptWindow("未达到对应军功!", PromptWindow.PromptType.Hint, null, null);
            }
            else if (Type == 2)
            {
                UIManager.instance.OpenPromptWindow("已经领过该档奖励!", PromptWindow.PromptType.Hint, null, null);
            }
            else 
            {
                NetworkHandler.instance.SendProcess("8631#" + num + ";");
            }
        };

    }

    private void SetAwardLockInfo(int num)   //浮标位置以及对应奖励
    {
        for (int i = JungongGrid.transform.childCount - 1; i >= 0; i--) 
        {
            DestroyImmediate(JungongGrid.transform.GetChild(i).gameObject);
        }

        switch (num) 
        {
            case 1:
                SanjiaoSprite.transform.localPosition = new Vector3(-275f, 68.8f, 0);
                break;
            case 2:
                SanjiaoSprite.transform.localPosition = new Vector3(-110f, 68.8f, 0);
                break;
            case 3:
                SanjiaoSprite.transform.localPosition = new Vector3(45f, 68.8f, 0);
                break;
            case 4:
                SanjiaoSprite.transform.localPosition = new Vector3(200f, 68.8f, 0);
                break;
            case 5:
                SanjiaoSprite.transform.localPosition = new Vector3(360f, 68.8f, 0);
                break;
        }

        BetterList<Item> ItemList=TextTranslator.instance.GetBattlefieldPointsByID(num).ItemList;
        for (int i = 0; i < ItemList.size; i++) 
        {
            if (ItemList[i].itemCode > 0) 
            {
                GameObject go = NGUITools.AddChild(JungongGrid, ItemAward);
                go.SetActive(true);
                SetItemDetail(ItemList[i].itemCode, ItemList[i].itemCount, go);
            }
        }

        JungongGrid.GetComponent<UIGrid>().Reposition();
    }



    public void SetItemDetail(int _itemId, int _itemCount, GameObject ItemObj)
    {       
        UISprite spriteFrame = ItemObj.GetComponent<UISprite>();
        UISprite spriteIcon = ItemObj.transform.Find("Icon").GetComponent<UISprite>();
        GameObject suiPian = ItemObj.transform.Find("suiPian").gameObject;
        ItemObj.transform.Find("Number").GetComponent<UILabel>().text = _itemCount.ToString();
        TextTranslator.ItemInfo _ItemInfo = TextTranslator.instance.GetItemByItemCode(_itemId);
        spriteFrame.spriteName = "Grade" + _ItemInfo.itemGrade.ToString();
        TextTranslator.instance.ItemDescription(ItemObj, _itemId, _itemCount);
        
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


    private void SetJunXianAward() 
    {
        JunxianWeizhi = 0;
        for(int i=JunxianGrid.transform.childCount-1;i>=0;i--)
        {
            DestroyImmediate(JunxianGrid.transform.GetChild(i).gameObject);
        }
        TabPart1.SetActive(false);
        TabPart2.SetActive(true);
        TabPart3.SetActive(false);
        JunxianDic.Clear();
        int country = CharacterRecorder.instance.legionCountryID;
        foreach (var item in TextTranslator.instance.NationList)
        {
            if (item.NationType == country) 
            {
                if (!JunxianDic.ContainsKey(item.Condition)) 
                {
                    JunxianDic.Add(item.Condition, item.ID);
                }
            }
        }

        foreach (int value in JunxianDic.Values) 
        {
            JunxianWeizhi++;
            GameObject go = NGUITools.AddChild(JunxianGrid, JunxianItem);
            go.SetActive(true);
            BetterList<Item> NationItemList=TextTranslator.instance.GetNationByID(value).NationItemList;
            int num = 0;
            for (int i = 0; i < NationItemList.size; i++) 
            {
                if (NationItemList[i].itemCode > 0) 
                {
                    num++;
                    switch (num) 
                    {
                        case 1:
                            GameObject item1 = go.transform.Find("Item1").gameObject;
                            item1.SetActive(true);
                            SetItemDetail(NationItemList[i].itemCode, NationItemList[i].itemCount, item1);
                            break;
                        case 2:
                            GameObject item2 = go.transform.Find("Item2").gameObject;
                            item2.SetActive(true);
                            SetItemDetail(NationItemList[i].itemCode, NationItemList[i].itemCount, item2);
                            break;
                        case 3:
                            GameObject item3 = go.transform.Find("Item3").gameObject;
                            item3.SetActive(true);
                            SetItemDetail(NationItemList[i].itemCode, NationItemList[i].itemCount, item3);
                            break;
                        case 4:
                            GameObject item4 = go.transform.Find("Item4").gameObject;
                            item4.SetActive(true);
                            SetItemDetail(NationItemList[i].itemCode, NationItemList[i].itemCount, item4);
                            break;
                    }
                }
            }
            GameObject RankIcon = go.transform.Find("RankIcon").gameObject;
            RankIcon.SetActive(true);
            JunxianIcon(RankIcon, value);
        }

        JunxianGrid.GetComponent<UIGrid>().Reposition();
    }



    private void SetShadiAward()
    {
        for (int i = ShadiGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(ShadiGrid.transform.GetChild(i).gameObject);
        }
        TabPart1.SetActive(false);
        TabPart2.SetActive(false);
        TabPart3.SetActive(true);
        for (int i = 0; i < TextTranslator.instance.BattlefieldKillList.size; i++) 
        {
            GameObject go = NGUITools.AddChild(ShadiGrid, ShadiItem);
            go.SetActive(true);
            BetterList<Item> BattlefieldKillList = TextTranslator.instance.GetBattlefieldKillByID(i+1).BattlefieldKillList;
            int ranknum = TextTranslator.instance.GetBattlefieldKillByID(i + 1).Rank;
            int num = 0;

            for (int j = 0; j < BattlefieldKillList.size; j++)
            {
                if (BattlefieldKillList[j].itemCode > 0)
                {
                    num++;
                    switch (num)
                    {
                        case 1:
                            GameObject item1 = go.transform.Find("Item1").gameObject;
                            item1.SetActive(true);
                            SetItemDetail(BattlefieldKillList[j].itemCode, BattlefieldKillList[j].itemCount, item1);
                            break;
                        case 2:
                            GameObject item2 = go.transform.Find("Item2").gameObject;
                            item2.SetActive(true);
                            SetItemDetail(BattlefieldKillList[j].itemCode, BattlefieldKillList[j].itemCount, item2);
                            break;
                        case 3:
                            GameObject item3 = go.transform.Find("Item3").gameObject;
                            item3.SetActive(true);
                            SetItemDetail(BattlefieldKillList[j].itemCode, BattlefieldKillList[j].itemCount, item3);
                            break;
                        case 4:
                            GameObject item4 = go.transform.Find("Item4").gameObject;
                            item4.SetActive(true);
                            SetItemDetail(BattlefieldKillList[j].itemCode, BattlefieldKillList[j].itemCount, item4);
                            break;
                    }
                }
            }
            if (ranknum == 1) 
            {
                go.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite").GetComponent<UISprite>().spriteName = "u32_icon4";
                go.transform.Find("LabelRankSprite/LabelRankSprite").GetComponent<UISprite>().spriteName = "word1";
            }
            else if (ranknum == 2) 
            {
                go.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite").GetComponent<UISprite>().spriteName = "u32_icon3";
                go.transform.Find("LabelRankSprite/LabelRankSprite").GetComponent<UISprite>().spriteName = "word2";
            }
            else if (ranknum == 3)
            {
                go.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                go.transform.Find("LabelRankSprite").GetComponent<UISprite>().spriteName = "u32_icon5";
                go.transform.Find("LabelRankSprite/LabelRankSprite").GetComponent<UISprite>().spriteName = "word3";
            }
            else 
            {
                go.transform.Find("LabelRank").gameObject.SetActive(true);
                go.transform.Find("LabelRank").GetComponent<UILabel>().text = ranknum.ToString();
            }

        }
        ShadiGrid.GetComponent<UIGrid>().Reposition();
    }

    private void JunxianIcon(GameObject go,int Id) 
    {
        UISprite Rankicon = go.GetComponent<UISprite>();
        if (JunxianWeizhi == 1) 
        {
            Rankicon.spriteName = "guozhanjunxian1";
        }
        else if (JunxianWeizhi == 2) 
        {
            Rankicon.spriteName = "guozhanjunxian2";
        }
        else if (JunxianWeizhi == 3)
        {
            Rankicon.spriteName = "guozhanjunxian3";
        }
        else 
        {
            Rankicon.spriteName = "guozhanjunxian4";
        }

        go.transform.Find("Name").GetComponent<UILabel>().text = TextTranslator.instance.GetNationByID(Id).OfficeName;
    }
}
