using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
public class InfoWindow : MonoBehaviour
{
    public UITexture headIcon;
    public UILabel labelName;
    public UILabel LabelLevel;
    public UILabel PowerNumber;
    public UILabel labelBlood;
    public UILabel labelStamina;
    public UILabel labelExp;
    public UISlider sliderBlood;
    public UISlider sliderStamina;
    public UISlider sliderExp;
    public UILabel StaminaOneTimeLabel;
    public UILabel StaminaAllTimeLabel;
    public UILabel BloodOneTimeLabel;
    public UILabel BloodAllTimeLabel;
    public UILabel IDlabel;
    public UILabel JunTuanLabel;
    public GameObject StayCountryName;
    public GameObject guojias;
    public GameObject guojianame;
    public UILabel SignLabel;
    public UILabel VipLabel;
    public GameObject ChangeNameButton;
    public GameObject ChangeHardButton;
    public GameObject ChangeSignButton;

    public GameObject InputObject;
    public GameObject SendButton;

    public GameObject closeBtn;

    public UISprite JunTuanSprite;

    int OneTime = 300;
    int AllTime = 900;
    int BloodOneTime = 900;
    int BloodAllTime = 1800;
    int Stamina = 0;
    int Blood = 0;
    bool isUpdateStamina = false;
    bool isUpdateBlood = false;
    // Use this for initialization

    //修改名字
    public GameObject ChangeNameWindow;
    public GameObject RandomButton;
    public GameObject NameCancelButton;
    public GameObject NameSureButton;
    public GameObject InputName;
    string PlayerName;
    //个性签名  
    public GameObject ChangeSignWindow;
    public GameObject SignCancelButton;
    public GameObject SignSureButton;
    public GameObject InputSign;
    string PlayerSign;
    public UILabel InfoSign;
    void Start()
    {
        if (CharacterRecorder.instance.legionID != 0)
        {
            NetworkHandler.instance.SendProcess(string.Format("8004#{0};", CharacterRecorder.instance.legionID));
        }
        Stamina = CharacterRecorder.instance.staminaCap - CharacterRecorder.instance.stamina;
        Blood = CharacterRecorder.instance.spriteCap - CharacterRecorder.instance.sprite;
        NetworkHandler.instance.SendProcess("1107#;");
        if (UIEventListener.Get(GameObject.Find("CloseButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("CloseButton")).onClick += delegate(GameObject go)
            {
                if (ObscuredPrefs.GetString("Account").IndexOf("test_") > -1)
                {
                    NetworkHandler.instance.SendProcess("9301#2;0;");
                    UIManager.instance.OpenPromptWindow("地图全开", PromptWindow.PromptType.Hint, null, null);
                }
                
            };
        }


        if (UIEventListener.Get(GameObject.Find("Close1Button")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("Close1Button")).onClick += delegate(GameObject go)
            {
                if (ObscuredPrefs.GetString("Account").IndexOf("test_") > -1)
                {
                    InputObject.SetActive(true);
                    SendButton.SetActive(true);
                }
                
            };
        }

        if (UIEventListener.Get(SendButton).onClick == null)
        {
            UIEventListener.Get(SendButton).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("9301#1;" + InputObject.GetComponent<UIInput>().value + ";");
                UIManager.instance.OpenPromptWindow("设置等级为" + InputObject.GetComponent<UIInput>().value, PromptWindow.PromptType.Hint, null, null);
            };
        }

        if (UIEventListener.Get(closeBtn).onClick == null)
        {
            UIEventListener.Get(closeBtn).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }

        if (UIEventListener.Get(GameObject.Find("CDkey")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("CDkey")).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenSinglePanel("PackageExchangeWindow", false);
                //UIManager.instance.OpenPromptWindow("即将开放", PromptWindow.PromptType.Alert, null, null);
                Debug.Log("礼包兑换");
            };
        }

        if (UIEventListener.Get(GameObject.Find("Setting")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("Setting")).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPanel("SettingWindow", false);
                Debug.Log("设置");
            };
        }

        //if (UIEventListener.Get(GameObject.Find("SpriteMask")).onClick == null)
        //{
        //    UIEventListener.Get(GameObject.Find("SpriteMask")).onClick += delegate(GameObject go)
        //    {
        //        UIManager.instance.BackUI();
        //    };
        //}
        if (UIEventListener.Get(ChangeNameButton).onClick == null)
        {
            UIEventListener.Get(ChangeNameButton).onClick += delegate(GameObject go)
            {
                ChangeNameWindow.SetActive(true);
                SetNameWindowInfo();
            };
        }
        if (UIEventListener.Get(ChangeHardButton).onClick == null)
        {
            UIEventListener.Get(ChangeHardButton).onClick += delegate(GameObject go)
            {
                //UIManager.instance.OpenPromptWindow("即将开放", PromptWindow.PromptType.Alert, null, null);
                UIManager.instance.OpenPanel("HeadSettingWindow", false);
            };
        }
        if (UIEventListener.Get(ChangeSignButton).onClick == null)
        {
            UIEventListener.Get(ChangeSignButton).onClick += delegate(GameObject go)
            {
                ChangeSignWindow.SetActive(true);
                SetSignWindowInfo();
            };
        }
        headIcon.mainTexture = Resources.Load(string.Format("Head/{0}", CharacterRecorder.instance.headIcon), typeof(Texture)) as Texture;
        labelName.text = CharacterRecorder.instance.characterName;
        LabelLevel.text = string.Format("Lv.{0}", CharacterRecorder.instance.level.ToString());
        PowerNumber.text = CharacterRecorder.instance.Fight.ToString();
        labelStamina.text = CharacterRecorder.instance.stamina.ToString() + "/" + CharacterRecorder.instance.staminaCap.ToString();
        //labelBlood.text = CharacterRecorder.instance.staminaCap.ToString() + "/" + CharacterRecorder.instance.staminaCap.ToString();
        labelBlood.text = CharacterRecorder.instance.sprite.ToString() + "/" + CharacterRecorder.instance.spriteCap.ToString();
        labelExp.text = CharacterRecorder.instance.exp.ToString() + "/" + CharacterRecorder.instance.expMax.ToString();
        IDlabel.text = CharacterRecorder.instance.userId.ToString();//"7569481";
        JunTuanLabel.text = CharacterRecorder.instance.legionID == 0 ? "无" : CharacterRecorder.instance.legionName;//"土豪军团";
        SignLabel.text = "无";
        VipLabel.text = "VIP" + CharacterRecorder.instance.Vip.ToString();
        //sliderBlood.value= CharacterRecorder.instance.blood * 1f / CharacterRecorder.instance.bloodMax;
        //sliderStamina.value = CharacterRecorder.instance.stamina * 1f / CharacterRecorder.instance.staminaCap;
        sliderExp.value = CharacterRecorder.instance.exp * 1f / CharacterRecorder.instance.expMax;

        //InvokeRepeating("UpdateTime", 0, 1f);
        Invoke("SetLegionName", 0.2f);
        SetLegionSprite();
        MyLegionIsWitchCountry();
    }

    /// <summary>
    /// 自己所属同盟或帝国显示
    /// </summary>
    void MyLegionIsWitchCountry()
    {
        if (CharacterRecorder.instance.legionCountryID == 0)
        {
            StayCountryName.SetActive(false);
        }
        else if (CharacterRecorder.instance.legionCountryID == 1)
        {
            StayCountryName.SetActive(true);
            guojias.GetComponent<UISprite>().spriteName = "guozhanicon1";
            guojianame.GetComponent<UILabel>().text = "同盟";
        }
        else
        {
            StayCountryName.SetActive(true);
            guojias.GetComponent<UISprite>().spriteName = "guozhanicon2";
            guojianame.GetComponent<UILabel>().text = "帝国";
        }
    }


    public void SetLegionName()
    {
        JunTuanLabel.text = CharacterRecorder.instance.legionID == 0 ? "无" : CharacterRecorder.instance.legionName;//"土豪军团";
        
       // SetLegionSprite();
    }
    //军团图标获得
    public void SetLegionSprite()
    {
        if (CharacterRecorder.instance.legionID == 0)
        {
            JunTuanSprite.gameObject.SetActive(false);
        }
        else
        {
            JunTuanSprite.spriteName= string.Format("legionFlag{0}", CharacterRecorder.instance.legionFlag);
        }
    }
    //void UpdateTime()
    //{
    //    if (OneTime > 0)
    //    {
    //        string fen = (OneTime / 60).ToString();
    //        string miao = (OneTime % 60).ToString();
    //        StaminaOneTimeLabel.text = ((OneTime / 60)).ToString() + ":" + (OneTime % 60).ToString();
    //        BloodOneTimeLabel.text = ((OneTime / 60)).ToString() + ":" + (OneTime % 60).ToString();
    //        OneTime -= 1;
    //    }
    //    if (AllTime > 0)
    //    {
    //        StaminaAllTimeLabel.text = ((AllTime / 60)).ToString() + ":" + (AllTime % 60).ToString();
    //        BloodAllTimeLabel.text = ((AllTime / 60)).ToString() + ":" + (AllTime % 60).ToString();
    //        AllTime -= 1;
    //    }
    //}
    void UpdateTime()
    {
        if (OneTime > 0)
        {
            if (isUpdateStamina)
            {
                StaminaOneTimeLabel.text = ((OneTime / 60)).ToString("00") + ":" + (OneTime % 60).ToString("00");
                labelStamina.text = CharacterRecorder.instance.stamina.ToString() + "/" + CharacterRecorder.instance.staminaCap.ToString();
                isUpdateStamina = false;
            }
            string fen = (OneTime / 60).ToString();
            string miao = (OneTime % 60).ToString();
            StaminaOneTimeLabel.text = ((OneTime / 60)).ToString("00") + ":" + (OneTime % 60).ToString("00");
            OneTime -= 1;
        }
        else if (OneTime == 0)
        {
            if (Stamina > 0)
            {
                labelStamina.text = CharacterRecorder.instance.stamina.ToString() + "/" + CharacterRecorder.instance.staminaCap.ToString();
                isUpdateStamina = true;
                OneTime = 300 - 1;
            }
        }
        if (BloodOneTime > 0)
        {
            if (isUpdateBlood)
            {
                BloodOneTimeLabel.text = ((BloodOneTime / 60)).ToString("00") + ":" + (BloodOneTime % 60).ToString("00");
                labelBlood.text = CharacterRecorder.instance.sprite.ToString() + "/" + CharacterRecorder.instance.spriteCap.ToString();
                isUpdateBlood = false;
            }
            string bloodFen = (OneTime / 60).ToString();
            string bloodMiao = (OneTime % 60).ToString();
            BloodOneTimeLabel.text = ((BloodOneTime / 60)).ToString("00") + ":" + (BloodOneTime % 60).ToString("00");
            BloodOneTime -= 1;
        }
        else if (BloodOneTime == 0)
        {
            if (Blood > 0)
            {
                isUpdateBlood = true;
                BloodOneTime = 900 - 1;
                labelBlood.text = CharacterRecorder.instance.sprite.ToString() + "/" + CharacterRecorder.instance.spriteCap.ToString();
            }
        }
        if (AllTime > 0)
        {
            string houreStr = (AllTime / 3600).ToString("00");
            string miniteStr = (AllTime % 3600 / 60).ToString("00");
            string secondStr = (AllTime % 60).ToString("00");
            StaminaAllTimeLabel.text = string.Format("{0}:{1}:{2}", houreStr, miniteStr, secondStr);
            AllTime -= 1;
        }
        else
        {
            StaminaAllTimeLabel.text = "已满";
        }
        if (BloodAllTime > 0)
        {
            string houreStr = (BloodAllTime / 3600).ToString("00");
            string miniteStr = (BloodAllTime % 3600 / 60).ToString("00");
            string secondStr = (BloodAllTime % 60).ToString("00");
            BloodAllTimeLabel.text = string.Format("{0}:{1}:{2}", houreStr, miniteStr, secondStr);
            BloodAllTime -= 1;
        }
        else
        {
            BloodAllTimeLabel.text = "已满";
        }
    }
    void StaminaTimeCount()
    {
        if (Stamina > 1)
        {
            AllTime = (Stamina - 1) * 300 + OneTime;

        }
        else if (Stamina == 1)
        {
            AllTime = OneTime;
        }
        else
        {
            OneTime = 0;
            AllTime = 0;
        }
        if (Blood > 1)
        {
            BloodAllTime = (Blood - 1) * 900 + BloodOneTime;
        }
        else if (Blood == 1)
        {
            BloodAllTime = Blood;
        }
        else
        {
            BloodOneTime = 0;
            BloodAllTime = 0;
        }
    }
    public void SetInfoWindowTime(int _staminaOneTime, int _bloodOneTime)
    {
        OneTime = _staminaOneTime;
        BloodOneTime = _bloodOneTime;
        StaminaTimeCount();
        CancelInvoke();
        InvokeRepeating("UpdateTime", 0, 1.0f);
    }

    #region 修改名字
    public void SetNameWindowInfo()
    {
        PlayerName = RandomName();
        UpdateName(PlayerName);
        UIEventListener.Get(NameCancelButton).onClick += delegate(GameObject go)
        {
            ChangeNameWindow.SetActive(false);
        };
        UIEventListener.Get(NameSureButton).onClick = delegate(GameObject go)
        {
            PlayerName = InputName.GetComponent<UIInput>().value;
            int num = 0;
            bool NameIsOK = false;
            foreach (var str in PlayerName)
            {
                num++;
            }
            if (num <= 6)
            {
                NameIsOK = true;
            }
            if (NameIsOK)
            {
                ChangeNameWindow.SetActive(false);
                NetworkHandler.instance.SendProcess("1041#" + PlayerName + ";");
            }
            else
            {
                UIManager.instance.OpenPromptWindow("名字上限六个字，请重新输入", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(RandomButton).onClick += delegate(GameObject obj)
        {
            PlayerName = RandomName();
            UpdateName(PlayerName);
        };

    }



    /// <summary>
    /// 点击随机后调用这个修改显示名字
    /// </summary>
    public void UpdateName(string name)
    {
        if (GameObject.Find("InputName") != null)
        {
            InputName.GetComponent<UIInput>().value = name;
            InputName.transform.Find("Name").GetComponent<UILabel>().text = name;
        }
    }
    string RandomName()
    {
        string[] firstName = new string[]{ "传说","春野","大爱","大和","单身","巅峰","独恋","繁华","主流","古墓","哈利","黑骑","黑色","红尘","红人","华丽",
                                                     "幻想","皇族","辉煌","回忆","记忆","寂寞","酱油","经典","精彩","精英","绝版","可爱","浪漫","冷","恋","灵魂","龙之","萌",
                                                     "梦之","迷糊","慕容","纳兰","南宫","欧阳","平凡","旗木","千语","倾城","荣耀","柔情","善良","伤心","上帝","神圣","守护","司马",
                                                     "天道","天使","颓废","完美","王者","微笑","温柔","无情","无忧","午夜","西门","夏侯","潇洒","笑傲","旋律","漩涡","寻梦","演绎",
                                                     "优雅","挚爱","诸葛","专属","自由","左岸","樱花","梧桐","月影","晨曦","如梦"
                                                  };
        string[] lastName = new string[]{ "比尤莱","埃达","阿芙拉","阿加莎","布莱兹","布伦达","布里奇特","布鲁克","谢莉尔","克里斯汀","艾莉丝","爱玛","阿尔瓦","阿米莉娅",
                                                    "艾米","安德烈亚","克莱尔","德伯拉","多洛雷斯","多明尼卡","朵拉","桃乐丝","伊迪斯","艾伦","艾尔玛","艾西","安娜","安娜贝尔","安东尼",
                                                    "艾普利","爱勒贝拉","艾琳娜","亚特兰特","阿西娜","奥德利","奥劳拉","芭芭拉","贝琳达","艾尔维拉","弗朗西斯","弗里达","盖尔","乔治亚","杰拉尔丁",
                                                    "葛罗瑞亚","格拉迪斯","汉娜","海柔尔","海瑟尔","赫达","海伦","海洛伊丝","胡尔达","艾比盖","艾达","伊莎贝尔","贝拉","百丽儿","贝斯","贝芙","布兰奇",
                                                    "卡米拉","坎迪斯","卡拉","卡洛","卡罗琳","凯瑟琳","凯斯","夏洛特","克洛伊","克莱曼婷","康斯坦丝","艾莎","巴特勒","拜伦","波克","伯恩","伯恩斯","勃特勒",
                                                    "布尔维尔","达芙妮","黛安娜","黛西","丹尼斯","迪莉娅","多丽丝","朵拉","哈里","哈里曼","哈里森","哈林顿","哈罗德","汉萨","汉森","吉蒂","杰奎琳","卡尔",
                                                    "卡罗尔","卡玛","凯雷","凯伦","凯伊","坎普","科拉","科妮莉亚","劳伦","李","莉迪亚","林赛","路易斯","罗兰","罗瑞尔","玛佩尔","彭斯","乔", "乔伊","琼",
                                                    "琼斯","唐娜","哈维","霍索恩","海登","朱迪斯","贾德森","茱莉亚","伊甸","朱恩","凯利","凯洛格","凯尔森","凯尔文","凯南","肯尼迪","凯佩尔","凯特","凯瑟琳",
                                                    "卡特琳","朱莉安娜","朱丽叶","莎曼撤","珊多拉","仙蒂","莎拉","赛拉","萨琳娜","希拉","雪莉","希贝儿","玛拉","蜜尔娜","南希","纳塔利","妮可","妮可拉","妮娜",
                                                    "诺拉","诺维雅","妮蒂亚","欧尔佳"
                                                 };
        Random.seed = System.Environment.TickCount;
        string firstname = firstName[Random.Range(0, firstName.Length)];
        string lastname = lastName[Random.Range(0, lastName.Length)];
        string name = firstname + lastname;
        return name;
    }
    #endregion

    #region 个性签名
    public void SetSignWindowInfo()
    {
        UIEventListener.Get(SignCancelButton).onClick += delegate(GameObject go)
        {
            ChangeSignWindow.SetActive(false);
        };
        UIEventListener.Get(SignSureButton).onClick += delegate(GameObject go)
        {
            PlayerSign = InputSign.GetComponent<UIInput>().value;
            int num = 0;
            foreach (var str in PlayerSign)
            {
                num++;
            }
            if (num <= 20)
            {
                ChangeSignWindow.SetActive(false);
                NetworkHandler.instance.SendProcess("1042#" + PlayerSign + ";");
            }
            else
            {
                UIManager.instance.OpenPromptWindow("签名上限二十个字，请重新输入", PromptWindow.PromptType.Hint, null, null);
            }

        };
    }

    #endregion
}
