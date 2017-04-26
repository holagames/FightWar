using UnityEngine;
using System.Collections;

public class HarvestWindow : MonoBehaviour
{

    public UILabel BuildLevel;
    public UILabel Money;
    public UILabel Timer;
    public UILabel PlayerLevel;
    public UILabel IconNumber;
    public UILabel AddBuff;
    public UILabel MosterMoney;
    public UILabel NowHaveMoney;
    public GameObject GetButton;
    public GameObject UpButton;
    public GameObject CantGetButton;
    public GameObject CantUpButton;
    //加成显示
    public UILabel ConquerListLabel;
    public UILabel FriendListLabel;
    public UILabel SelfListLabel;
    public UILabel SelfTwoListLabel;
    //item
    public GameObject Pit1;
    public GameObject Pit2;
    public GameObject Pit3;
    public GameObject Pit4;
    public GameObject Pit11;
    public GameObject Pit12;
    public GameObject Pit13;
    public GameObject Pit14;
    //
    public int TabeID = 1;
    public int KengID = 0;
    //
    public int AllTime = 0;
    public float NowMoney = 0;
    public Fortress FortressInfo;
    public Fortress LevelInfo;
    //   
    public GameObject ConquerListWindow;
    public GameObject FriendInfoWindow;
    public GameObject ConquerWindow;
    //
    public int AddBuffOne;
    public int AddBuffTwo;
    public int AddBuffThree;
    public int AddBuffFour;
    public int AddBuff11;
    public int AddBuff12;
    public int AddBuff13;
    public int AddBuff14;
    public float AllAddBuff;
    public UILabel AddBuffOneEarnings;
    public UILabel AddBuffTwoEarnings;
    public UILabel AddBuffThreeEarnings;
    public UILabel AddBuffFourEarnings;
    //
    public GameObject ItemInfoGo;
    public UITexture BulidTexture;
    //升级特效
    public GameObject UpEffect1;
    public GameObject UpEffect2;
    //生产特效
    public GameObject WorkingEffect1;
    public GameObject WorkingEffect2;
    // Use this for initialization
    void Start()
    {
        UIEventListener.Get(GetButton).onClick += delegate(GameObject go)
        {
            //if (AllAddBuff == 0)
            //{
            //    UIManager.instance.OpenPromptWindow("未上阵英雄", PromptWindow.PromptType.Hint, null, null);
            //}
            //if (AllTime != 0)
            //{
            //    UIManager.instance.OpenPromptWindow("时间未满6小时,生产结束后可领取", PromptWindow.PromptType.Hint, null, null);
            //}
            //else

            NetworkHandler.instance.SendProcess("6503#" + CharacterRecorder.instance.TabeID + ";");
            CantGetButton.SetActive(true);
            GetButton.SetActive(false);

        };
        UIEventListener.Get(CantGetButton).onClick += delegate(GameObject go)
       {
           UIManager.instance.OpenPromptWindow("时间未满6小时,生产结束后可领取", PromptWindow.PromptType.Hint, null, null);

       };
        UIEventListener.Get(UpButton).onClick += delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("6505#" + CharacterRecorder.instance.TabeID + ";");
            if (CharacterRecorder.instance.TabeID == 1)
            {
                UpEffect1.SetActive(true);
            }
            else if (CharacterRecorder.instance.TabeID == 2)
            {
                UpEffect2.SetActive(true);
            }

        };
        UIEventListener.Get(CantUpButton).onClick += delegate(GameObject go)
      {
          if (LevelInfo.Level > CharacterRecorder.instance.level)
          {
              UIManager.instance.OpenPromptWindow("等级不足", PromptWindow.PromptType.Hint, null, null);
          }
          else if (CharacterRecorder.instance.gold < LevelInfo.NeedItemNum)
          {
              if (CharacterRecorder.instance.TabeID == 1)
              {
                  UIManager.instance.OpenPromptWindow("金币不足", PromptWindow.PromptType.Hint, null, null);
              }

          }
          else if (CharacterRecorder.instance.GoldBar < LevelInfo.NeedItemNum)
          {
              if (CharacterRecorder.instance.TabeID == 2)
              {
                  UIManager.instance.OpenPromptWindow("金条不足", PromptWindow.PromptType.Hint, null, null);
              }
          }
          else
          {
              UIManager.instance.OpenPromptWindow("条件不足", PromptWindow.PromptType.Hint, null, null);
          }
      };
        UIEventListener.Get(Pit1).onClick += delegate(GameObject go)
        {
            if (CharacterRecorder.instance.GuideID[58] == 6)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
            KengID = 1;
            UIManager.instance.OpenSinglePanel("CheckSelfHeroWindow", false);
        };
        UIEventListener.Get(Pit2).onClick += delegate(GameObject go)
        {
            KengID = 2;
            UIManager.instance.OpenSinglePanel("CheckSelfHeroWindow", false);
        };
        UIEventListener.Get(Pit3).onClick += delegate(GameObject go)
        {

            if (CharacterRecorder.instance.level >= 40)
            {
                KengID = 3;
                UIManager.instance.OpenSinglePanel("CheckSelfHeroWindow", false);
            }
            else
            {
                UIManager.instance.OpenPromptWindow("等级40级开放", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(Pit4).onClick += delegate(GameObject go)
        {

            if (CharacterRecorder.instance.level >= 40)
            {
                KengID = 4;
                UIManager.instance.OpenSinglePanel("CheckSelfHeroWindow", false);
            }
            else
            {
                UIManager.instance.OpenPromptWindow("等级40级开放", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(Pit11).onClick += delegate(GameObject go)
        {
            KengID = 11;
            NetworkHandler.instance.SendProcess("7101#");
            FriendInfoWindow.SetActive(true);
        };
        UIEventListener.Get(Pit12).onClick += delegate(GameObject go)
        {
            if (CharacterRecorder.instance.Vip >= 3)
            {
                KengID = 12;
                NetworkHandler.instance.SendProcess("7101#");
                FriendInfoWindow.SetActive(true);
            }
            else
            {
                UIManager.instance.OpenPromptWindow("Vip3开放", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(Pit13).onClick += delegate(GameObject go)
        {

            KengID = 13;
            NetworkHandler.instance.SendProcess("6015#");
            //NetworkHandler.instance.SendProcess("6002#");
            ConquerListWindow.SetActive(true);
        };
        UIEventListener.Get(Pit14).onClick += delegate(GameObject go)
        {
            if (CharacterRecorder.instance.Vip >= 3)
            {
                KengID = 14;
                NetworkHandler.instance.SendProcess("6015#");
                //NetworkHandler.instance.SendProcess("6002#");
                ConquerListWindow.SetActive(true);
            }
            else
            {
                UIManager.instance.OpenPromptWindow("Vip3开放", PromptWindow.PromptType.Hint, null, null);
            }
        };
        switch (CharacterRecorder.instance.TabeID)
        {
            case 1:
                BulidTexture.mainTexture = Resources.Load("Game/jianzhu_icon1") as Texture;
                WorkingEffect1.SetActive(true);
                break;
            case 2:
                BulidTexture.mainTexture = Resources.Load("Game/jianzhu_icon2") as Texture;
                WorkingEffect2.SetActive(true);
                break;
            case 3:
                BulidTexture.mainTexture = Resources.Load("Game/jianzhu_icon3") as Texture;
                break;

        }
        if (CharacterRecorder.instance.Vip >= 3)
        {
            Pit14.transform.Find("Message").gameObject.SetActive(false);
            Pit12.transform.Find("Message").gameObject.SetActive(false);
        }
        if (CharacterRecorder.instance.level >= 40)
        {
            Pit3.transform.Find("Message").gameObject.SetActive(false);
            Pit4.transform.Find("Message").gameObject.SetActive(false);
        }

    }


    public void ShowEffect(int ID)
    {
        switch (ID)
        {
            case 1:
            case 2:
            case 3:
            case 4:
                StartCoroutine(OnEquipDataUpEffect(AddBuffOneEarnings));
                break;
            case 11:
            case 12:
                StartCoroutine(OnEquipDataUpEffect(AddBuffThreeEarnings));
                break;
            case 13:
            case 14:
                StartCoroutine(OnEquipDataUpEffect(AddBuffFourEarnings));
                break;
        }
    }

    public void SetInfo(string[] dateSplit)
    {
        ShowItemTime(int.Parse(dateSplit[2]));
        string[] RewarItem = dateSplit[0].Split('!');
        string[] IconItem = RewarItem[0].Split('$');
        BuildLevel.text = dateSplit[1];
        AddBuff.text = dateSplit[3] + "%";
        AllAddBuff = int.Parse(dateSplit[3]);
        NowMoney = float.Parse(IconItem[1]);
        string[] SelfItem = dateSplit[4].Split('!');
        for (int i = 0; i < SelfItem.Length - 1; i++)
        {
            if (SelfItem[i] != null && SelfItem[i] != "0")
            {
                string[] SelfInfoItem = SelfItem[i].Split('$');
                SelfInfo(int.Parse(SelfInfoItem[0]), int.Parse(SelfInfoItem[1]), int.Parse(SelfInfoItem[2]), 0);
            }
        }
        string[] FriendItem = dateSplit[5].Split('!');
        for (int i = 0; i < FriendItem.Length - 1; i++)
        {
            if (FriendItem[i] != null && FriendItem[i] != "0")
            {
                string[] FriendInfoItem = FriendItem[i].Split('$');
                SelfInfo(int.Parse(FriendInfoItem[0]), int.Parse(FriendInfoItem[1]), int.Parse(FriendInfoItem[3]), int.Parse(FriendInfoItem[2]));
            }
        }
        LevelUpInfo(int.Parse(dateSplit[1]), false);

    }
    public void LevelUpInfo(int Level, bool IsShow)
    {
        BuildLevel.text = "LV." + Level.ToString();
        FortressInfo = TextTranslator.instance.GetFortressByListId(Level, CharacterRecorder.instance.TabeID);
        if (FortressInfo != null)
        {
            Money.transform.Find("Sprite").GetComponent<UISprite>().spriteName = FortressInfo.BonusItemId.ToString();
            MosterMoney.transform.Find("Sprite").GetComponent<UISprite>().spriteName = FortressInfo.BonusItemId.ToString();
            NowHaveMoney.transform.Find("Sprite").GetComponent<UISprite>().spriteName = FortressInfo.BonusItemId.ToString();
            // Money.text = (FortressInfo.BonusItemNum * 6 * (1 + AllAddBuff/100)).ToString() + "/小时";
            Money.text = (FortressInfo.BonusItemNum * 6).ToString() + "/小时";
        }
        LevelInfo = TextTranslator.instance.GetFortressByListId(Level, CharacterRecorder.instance.TabeID);
        if (LevelInfo != null)
        {
            if (CharacterRecorder.instance.level >= LevelInfo.Level)
            {
                PlayerLevel.text = LabelColor(1) + LevelInfo.Level.ToString() + "";
            }
            else
            {
                PlayerLevel.text = LabelColor(2) + LevelInfo.Level.ToString() + "";
            }
            if (CharacterRecorder.instance.gold >= LevelInfo.NeedItemNum)
            {
                IconNumber.text = LabelColor(1) + LevelInfo.NeedItemNum.ToString();
            }
            else
            {
                IconNumber.text = LabelColor(2) + LevelInfo.NeedItemNum.ToString();
            }
            if (CharacterRecorder.instance.TabeID == 1)
            {
                if (CharacterRecorder.instance.level >= LevelInfo.Level && CharacterRecorder.instance.gold >= LevelInfo.NeedItemNum)
                {
                    UpButton.SetActive(true);
                    CantUpButton.SetActive(false);
                }
                else
                {
                    CantUpButton.SetActive(true);
                    UpButton.SetActive(false);
                }
            }
            else
            {
                if (CharacterRecorder.instance.level >= LevelInfo.Level && CharacterRecorder.instance.GoldBar >= LevelInfo.NeedItemNum)
                {
                    UpButton.SetActive(true);
                    CantUpButton.SetActive(false);
                }
                else
                {
                    CantUpButton.SetActive(true);
                    UpButton.SetActive(false);
                }
            }
            IconNumber.transform.Find("Icon").GetComponent<UISprite>().spriteName = LevelInfo.NeedItemId.ToString();
        }

        if (IsShow)
        {
            StartCoroutine(OnEquipDataUpEffect(Money));
            StartCoroutine(OnEquipDataUpEffect(MosterMoney));
        }
        AddBuffOneEarnings.text = "加成" + (AddBuffOne + AddBuffTwo + AddBuffThree + AddBuffFour).ToString() + "%";
        //AddBuffTwoEarnings.text = "+" + ().ToString() + "%";
        AddBuffThreeEarnings.text = "加成" + (AddBuff11 + AddBuff12) + "%";
        AddBuffFourEarnings.text = "加成" + (AddBuff13 + AddBuff14) + "%";
        MosterMoney.text = (FortressInfo.BonusItemNum * 6 * 22).ToString();
        NowHaveMoney.text = NowMoney.ToString();
        StartCoroutine(DelayEffect());
    }
    IEnumerator DelayEffect()
    {
        WorkingEffect1.SetActive(false);
        WorkingEffect2.SetActive(false);
        yield return new WaitForSeconds(1f);
        UpEffect1.SetActive(false);
        UpEffect2.SetActive(false);
        switch (CharacterRecorder.instance.TabeID)
        {
            case 1:
                WorkingEffect1.SetActive(true);
                break;
            case 2:
                WorkingEffect2.SetActive(true);
                break;
            case 3:
                break;
        }
    }
    void SelfInfo(int ID, int CharRoleID, int AddBuff, int Level)
    {
        switch (ID)
        {
            case 1:
                ItemInfoGo = Pit1;
                AddBuffOne = AddBuff;
                ItemInfoGo.transform.Find("Clock").gameObject.SetActive(false);
                break;
            case 2:
                ItemInfoGo = Pit2;
                AddBuffTwo = AddBuff;
                ItemInfoGo.transform.Find("Clock").gameObject.SetActive(false);
                break;
            case 3:
                ItemInfoGo = Pit3;
                AddBuffThree = AddBuff;
                if (CharacterRecorder.instance.level >= 40)
                {
                    ItemInfoGo.transform.Find("Clock").gameObject.SetActive(false);
                    ItemInfoGo.transform.Find("Add").gameObject.SetActive(true);
                }
                else
                {
                    ItemInfoGo.transform.Find("Clock").gameObject.SetActive(true);
                    ItemInfoGo.transform.Find("Add").gameObject.SetActive(false);
                }
                break;
            case 4:
                ItemInfoGo = Pit4;
                AddBuffFour = AddBuff;
                if (CharacterRecorder.instance.level >= 40)
                {
                    ItemInfoGo.transform.Find("Clock").gameObject.SetActive(false);
                    ItemInfoGo.transform.Find("Add").gameObject.SetActive(true);
                }
                else
                {
                    ItemInfoGo.transform.Find("Clock").gameObject.SetActive(true);
                    ItemInfoGo.transform.Find("Add").gameObject.SetActive(false);
                }
                break;
            case 11:
                ItemInfoGo = Pit11;
                AddBuff11 = AddBuff;
                ItemInfoGo.transform.Find("Clock").gameObject.SetActive(false);
                break;
            case 12:
                ItemInfoGo = Pit12;
                AddBuff12 = AddBuff;
                if (CharacterRecorder.instance.Vip < 3)
                {
                    ItemInfoGo.transform.Find("Clock").gameObject.SetActive(true);
                    ItemInfoGo.transform.Find("Add").gameObject.SetActive(false);
                    ItemInfoGo.transform.Find("Message").gameObject.SetActive(true);
                }
                else
                {
                    ItemInfoGo.transform.Find("Clock").gameObject.SetActive(false);
                    ItemInfoGo.transform.Find("Add").gameObject.SetActive(true);
                    ItemInfoGo.transform.Find("Message").gameObject.SetActive(false);
                }

                break;
            case 13:
                ItemInfoGo = Pit13;
                AddBuff13 = AddBuff;
                ItemInfoGo.transform.Find("Clock").gameObject.SetActive(false);
                break;
            case 14:
                ItemInfoGo = Pit14;
                AddBuff14 = AddBuff;
                if (CharacterRecorder.instance.Vip < 3)
                {
                    ItemInfoGo.transform.Find("Clock").gameObject.SetActive(true);
                    ItemInfoGo.transform.Find("Add").gameObject.SetActive(false);
                    ItemInfoGo.transform.Find("Message").gameObject.SetActive(true);
                }
                else
                {
                    ItemInfoGo.transform.Find("Clock").gameObject.SetActive(false);
                    ItemInfoGo.transform.Find("Add").gameObject.SetActive(true);
                    ItemInfoGo.transform.Find("Message").gameObject.SetActive(false);
                }

                break;
        }
        ItemInfoGo.transform.Find("HeroGrade").gameObject.SetActive(true);
        ItemInfoGo.transform.Find("HeroGrade").gameObject.GetComponent<UISprite>().spriteName = "yxdi" + (TextTranslator.instance.GetItemByItemCode(CharRoleID).itemGrade - 1).ToString();
        ItemInfoGo.transform.Find("HeroGrade/Sprite").gameObject.GetComponent<UISprite>().spriteName = CharRoleID.ToString();
    }

    void ShowItemTime(int Time)
    {
        CancelInvoke("TimeShowLose");
        AllTime = Time;
        InvokeRepeating("TimeShowLose", 0, 1f);
    }
    void TimeShowLose()
    {
        if (AllTime > 0)
        {
            Timer.gameObject.SetActive(true);
            string houreStr = (AllTime / 3600).ToString("00");
            string miniteStr = (AllTime % 3600 / 60).ToString("00");
            string secondStr = (AllTime % 60).ToString("00");
            Timer.text = string.Format("{0}:{1}:{2}", houreStr, miniteStr, secondStr);
            AllTime -= 1;
            CantGetButton.SetActive(true);
            GetButton.SetActive(false);
        }
        else
        {
            CantGetButton.SetActive(false);
            GetButton.SetActive(true);
            Timer.text = "00:00:00";
        }

    }
    public string LabelColor(int GradeID)
    {
        string LabelColor = "";
        switch (GradeID)
        {
            case 1:
                LabelColor = "[-][28DF5E]";//绿色
                break;
            case 2:
                LabelColor = "[-][FB2D50]";//红色
                break;
            case 3:
                LabelColor = "[-][FFFFFF]";//白色
                break;
            case 4:
                LabelColor = "[-][969696]";//灰色
                break;
        }
        return LabelColor;
    }
    //int HeroLevel(int level)// 1 42 55 70 84 100
    //{
    //    int Level = 0;
    //    if (level < 42)
    //    {
    //        Level = 1;
    //    }
    //    else if (level >= 42 && level < 55)
    //    {
    //        Level = 42;
    //    }
    //    else if (level >= 55 && level < 70)
    //    {
    //        Level = 55;
    //    }
    //    else if (level >= 70 && level < 84)
    //    {
    //        Level = 70;
    //    }
    //    else if (level >= 84 && level < 100)
    //    {
    //        Level = 84;
    //    }
    //    else
    //    {
    //        Level = 100;
    //    }
    //    return Level;
    //}

    public IEnumerator OnEquipDataUpEffect(UILabel _myLabel)
    {
        Color NewColor = _myLabel.color;
        yield return new WaitForSeconds(0.2f);
        _myLabel.color = new Color(62 / 255f, 232 / 255f, 23 / 255f, 255 / 255f);
        TweenScale _TweenScale = _myLabel.gameObject.GetComponent<TweenScale>();
        if (_TweenScale == null)
        {
            _TweenScale = _myLabel.gameObject.AddComponent<TweenScale>();
        }
        _TweenScale.from = Vector3.one;
        _TweenScale.to = 1.2f * Vector3.one;
        _TweenScale.duration = 0.3f;
        _TweenScale.PlayForward();
        yield return new WaitForSeconds(0.3f);
        _TweenScale.duration = 0.1f;
        _TweenScale.PlayReverse();
        _myLabel.color = NewColor;
    }
}
