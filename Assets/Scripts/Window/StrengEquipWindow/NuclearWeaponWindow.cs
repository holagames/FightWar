using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class NuclearWeaponWindow : MonoBehaviour
{

    public GameObject SynthesisWindow;
    public GameObject UpStarWindow;
    public GameObject UpClassWindow;

    public UILabel SkillName;
    public UILabel SkillMessage;
    public UISprite SkillIcon;
    public UILabel AttributeOne;
    public UILabel AttributeTwo;
    public UILabel AttributeThree;
    public UILabel AttributeFour;
    public UILabel AttributeFive;
    int num=42001;

    public bool isLoseGold = false;
    public Hero HeroID;
    public HeroInfo HeroInfo;
    public int WeaponItemID;
    public WeaponMaterial ItemMaterial;
    public WeaponUpClass ItemUpClass;
    public WeaponUpStar ItemUpStar;
    public TextTranslator.ItemInfo WeaponItem;
    public GameObject TrunTableButton;
    public int NowWeaponStar;
    public int NowWeaponClass;

    //万能碎片
    public int Item2Code;
    public int Item2OwnedCount;
    public int Item2NeedCount;

    public GameObject BgTexture;
    //
    private GameObject loseEffect;
    private List<GameObject> EffectList = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        loseEffect = gameObject.transform.Find("RightSprite/UpStarWindow/Text_ShenXingSB").gameObject;
        UIEventListener.Get(SynthesisWindow.transform.Find("SynButton").gameObject).onClick += delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("3302#" + HeroID.characterRoleID + ";" + WeaponItemID + ";" + "1;");
        };
        UIEventListener.Get(SynthesisWindow.transform.Find("AddButton").gameObject).onClick += delegate(GameObject go)
        {
            PictureCreater.instance.DestroyAllComponent();
            UIManager.instance.OpenPanel("TurntableWindow", true);
            CharacterRecorder.instance.heroPresentWeapon = WeaponItemID + 30000;
            //NetworkHandler.instance.SendProcess("3306#" + CharacterRecorder.instance.heroPresentWeapon.ToString());
            //NetworkHandler.instance.SendProcess("3305#");

        };
        UIEventListener.Get(TrunTableButton).onClick += delegate(GameObject go)
        {
            PictureCreater.instance.DestroyAllComponent();
            UIManager.instance.OpenPanel("TurntableWindow", true);
            CharacterRecorder.instance.heroPresentWeapon = WeaponItemID + 30000;
            //NetworkHandler.instance.SendProcess("3306#" + (WeaponItemID + 30000).ToString());
            //NetworkHandler.instance.SendProcess("3305#");
        };
        UIEventListener.Get(UpStarWindow.transform.Find("SynButton").gameObject).onClick += delegate(GameObject go)
        {
            if (NowWeaponStar < 5)
            {
                NetworkHandler.instance.SendProcess("3304#" + HeroID.characterRoleID + ";" + "1;");
            }
        };
        UIEventListener.Get(UpClassWindow.transform.Find("SynButton").gameObject).onClick += delegate(GameObject go)
        {
            if (isLoseGold)
            {
                UIManager.instance.OpenPromptWindow("所需神器碎片不足，点击“+”可快速获取", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                NetworkHandler.instance.SendProcess("3303#" + HeroID.characterRoleID + ";" + "1;");
            }
        };
        UIEventListener.Get(UpClassWindow.transform.Find("WanNengButton").gameObject).onClick += delegate(GameObject go)
       {
           if (TextTranslator.instance.GetItemCountByID(HeroID.cardID + 10000) >= ItemUpClass.RoleDebrisNum)
           {
               UIManager.instance.OpenPromptWindow("碎片充足", PromptWindow.PromptType.Hint, null, null);
           }
           else
           {
               UIManager.instance.OpenSinglePanel("PieceStoneExChangeBoard", false);
               PieceStoneExChangeBoard.IsAfterChanged = false;
               GameObject.Find("PieceStoneExChangeBoard").GetComponent<PieceStoneExChangeBoard>().SetPieceStoneExChangeBoardInfo(Item2Code, Item2OwnedCount, Item2NeedCount);
           }
       };
        if (CharacterRecorder.instance.isWeaponGachaFree)
        {
            TrunTableButton.transform.Find("RedPoint").gameObject.SetActive(true);
        }
        else
        {
            TrunTableButton.transform.Find("RedPoint").gameObject.SetActive(false);
        }
    }
    public void HeroIDInfo(Hero mCurHero)
    {
        HeroID = mCurHero;
        CharacterRecorder.instance.HeroWeaponID = TextTranslator.instance.GetHeroInfoByHeroID(HeroID.cardID).weaponID;
    }
    public void SetInfo(int WeaponID, int WeaponClass, int WeaponStar)
    {
        WeaponItemID = WeaponID;
        NowWeaponStar = WeaponStar;
        NowWeaponClass = WeaponClass;
        SynthesisWindow.SetActive(false);
        UpStarWindow.SetActive(false);
        UpClassWindow.SetActive(false);
        WeaponDic(WeaponID, WeaponClass, WeaponStar);
        if (WeaponClass == 0 && WeaponStar == 0)
        {
            SynthesisWindow.SetActive(true);
        }
        else if (WeaponStar == 5 || (WeaponClass == 5 && WeaponStar == 5))
        {
            UpClassWindow.SetActive(true);
        }
        else
        {
            UpStarWindow.SetActive(true);
        }
       // SkillName.text = ItemNameColor(num)+WeaponItem.itemName;//物品的名字
        SkillMessage.text = WeaponItem.itemDescription;
        SkillIcon.spriteName = WeaponID.ToString();

        if (GameObject.Find("SynthesisWindow") != null)
        {
            SynthesisWindowInfo(WeaponID, WeaponClass, WeaponStar);
        }
        else if (GameObject.Find("UpStarWindow") != null)
        {
            UpStarWindowInfo(WeaponID, WeaponClass, WeaponStar);
        }
        else
        {
            UpClassWindowInfo(WeaponID, WeaponClass, WeaponStar);
        }
    }
    void WeaponDic(int WeaponID, int WeaponClass, int WeaponStar)
    {
        print(WeaponID +"fd"+ WeaponStar+"casa"+ WeaponClass);
        WeaponItemID = WeaponID;
        NowWeaponStar = WeaponStar;
        NowWeaponClass = WeaponClass;
        ItemMaterial = TextTranslator.instance.GetWeaponMaterialByID(WeaponID);
        if (WeaponStar == 0)
        {
            if (WeaponClass == 0)
            {
                ItemUpClass = TextTranslator.instance.GetWeaponUpClassByID(WeaponID, 1);
                ItemUpStar = TextTranslator.instance.GetWeaponUpStarByID(WeaponID, 1, 1);
            }
            else
            {
                ItemUpClass = TextTranslator.instance.GetWeaponUpClassByID(WeaponID, WeaponClass + 1);
                ItemUpStar = TextTranslator.instance.GetWeaponUpStarByID(WeaponID, WeaponClass, 1);
            }

        }
        else
        {
            if (WeaponClass != 5)
            {
                ItemUpClass = TextTranslator.instance.GetWeaponUpClassByID(WeaponID, WeaponClass + 1);
            }
            else
            {
                ItemUpClass = TextTranslator.instance.GetWeaponUpClassByID(WeaponID, 5);
            }
           ItemUpStar = TextTranslator.instance.GetWeaponUpStarByID(WeaponID, WeaponClass, WeaponStar == 5 ? 5 : WeaponStar);
        }
        WeaponItem = TextTranslator.instance.GetItemByItemCode(WeaponID);
        LeftInfo(WeaponID, WeaponClass);
    }
    void LeftInfo(int _WeaponID, int ColorID)
    {
        AttributeOne.GetComponent<UILabel>().text = LabelColor(6) + "蓝色品质:";
        AttributeTwo.GetComponent<UILabel>().text = LabelColor(6) + "紫色品质:";
        AttributeThree.GetComponent<UILabel>().text = LabelColor(6) + "橙色品质:";
        AttributeFour.GetComponent<UILabel>().text = LabelColor(6) + "红色品质:";
        AttributeFive.GetComponent<UILabel>().text = LabelColor(6) + "绿色品质:";
        AttributeFive.transform.Find("Label").GetComponent<UILabel>().text = LabelColor(6) + "ＨＰ+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 1).Hp + "\n攻击+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 1).Atk + "\n防御+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 1).Def;
        AttributeOne.transform.Find("Label").GetComponent<UILabel>().text = LabelColor(6) + "ＨＰ+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 2).Hp + "\n攻击+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 2).Atk + "\n防御+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 2).Def;
        AttributeTwo.transform.Find("Label").GetComponent<UILabel>().text = LabelColor(6) + "ＨＰ+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 3).Hp + "\n攻击+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 3).Atk + "\n防御+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 3).Def;
        AttributeThree.transform.Find("Label").GetComponent<UILabel>().text = LabelColor(6) + "ＨＰ+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 4).Hp + "\n攻击+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 4).Atk + "\n防御+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 4).Def;
        AttributeFour.transform.Find("Label").GetComponent<UILabel>().text = LabelColor(6) + "ＨＰ+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 5).Hp + "\n攻击+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 5).Atk + "\n防御+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 5).Def;
        if (ColorID < 1)
        {
            num = 42001;
        }
        if (ColorID >= 1)
        {
            BgTexture.SetActive(true);
            AttributeFive.transform.Find("Label").GetComponent<UILabel>().text = LabelColor(3) + "ＨＰ+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 1).Hp + "\n攻击+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 1).Atk + "\n防御+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 1).Def;
            AttributeFive.GetComponent<UILabel>().text = LabelColor(3) + "绿色品质:";
            num = 42002;
        }
        if (ColorID >= 2)
        {
           
            AttributeOne.transform.Find("Label").GetComponent<UILabel>().text = LabelColor(3) + "ＨＰ+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 2).Hp + "\n攻击+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 2).Atk + "\n防御+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 2).Def;
            AttributeOne.GetComponent<UILabel>().text = LabelColor(3) + "蓝色品质:";
            num = 42003;
        }
        if (ColorID >= 3)
        {
            AttributeTwo.transform.Find("Label").GetComponent<UILabel>().text = LabelColor(3) + "ＨＰ+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 3).Hp + "\n攻击+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 3).Atk + "\n防御+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 3).Def;
            AttributeTwo.GetComponent<UILabel>().text = LabelColor(3) + "紫色品质:";
            num = 42004;
        }
        if (ColorID >= 4)
        {
            AttributeThree.transform.Find("Label").GetComponent<UILabel>().text = LabelColor(3) + "ＨＰ+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 4).Hp + "\n攻击+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 4).Atk + "\n防御+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 4).Def;
            AttributeThree.GetComponent<UILabel>().text = LabelColor(3) + "橙色品质:";
            num = 42005;
        }
        if (ColorID >= 5)
        {
            AttributeFour.transform.Find("Label").GetComponent<UILabel>().text = LabelColor(3) + "ＨＰ+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 5).Hp + "\n攻击+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 5).Atk + "\n防御+" + TextTranslator.instance.GetWeaponUpClassByID(_WeaponID, 5).Def;
            AttributeFour.GetComponent<UILabel>().text = LabelColor(3) + "红色品质:";
            num = 42006;

        }
        SkillName.text = ItemNameColor(num) + WeaponItem.itemName;//物品的名字
    }
    public void SynthesisWindowInfo(int WeaponID, int WeaponClass, int WeaponStar)
    {
        SynthesisWindow.transform.Find("CantSynButton").gameObject.SetActive(false);
        SynthesisWindow.transform.Find("SynButton").gameObject.SetActive(false);
        SynthesisWindow.transform.Find("WeaponIcon").GetComponent<UISprite>().spriteName = ItemMaterial.WeaponID.ToString();
        //SynthesisWindow.transform.Find("WeaponName").GetComponent<UILabel>().text = WeaponItem.itemName;
        SynthesisWindow.transform.Find("WeaponName").GetComponent<UILabel>().text = ItemNameColor(num) + WeaponItem.itemName;
        SynthesisWindow.transform.Find("Slider/Label").GetComponent<UILabel>().text = (float)TextTranslator.instance.GetItemCountByID(ItemMaterial.WeaponID + 30000) + "/" + ItemMaterial.NeedDebris;
        SynthesisWindow.transform.Find("Slider").GetComponent<UISlider>().value = (float)TextTranslator.instance.GetItemCountByID(ItemMaterial.WeaponID + 30000) / ItemMaterial.NeedDebris;
        SynthesisWindow.transform.Find("WeaponcFrame").GetComponent<UISprite>().spriteName = "Grade" + ItemUpStar.Color.ToString();
        SynthesisWindow.transform.Find("WeaponcFrame/Icon").GetComponent<UISprite>().spriteName = ItemMaterial.WeaponID.ToString();
        SynthesisWindow.transform.Find("NeedMoney").gameObject.SetActive(false);
        //if (CharacterRecorder.instance.gold < 2000)
        //{
        //    isLoseGold = true;
        //    SynthesisWindow.transform.Find("NeedMoney").GetComponent<UILabel>().text = LabelColor(1) + "200" + "[-]/" + "300";//绿色
        //}
        //else
        //{
        //    isLoseGold = false;
        //    SynthesisWindow.transform.Find("NeedMoney").GetComponent<UILabel>().text = LabelColor(2) + "200" + "[-]/" + "300";//红色
        //}
        if (SynthesisWindow.transform.Find("Slider").GetComponent<UISlider>().value == 1)
        {
            SynthesisWindow.transform.Find("SynButton").gameObject.SetActive(true);
        }
        else
        {
            SynthesisWindow.transform.Find("CantSynButton").gameObject.SetActive(true);
        }
    }
    public void UpStarWindowInfo(int WeaponID, int WeaponClass, int WeaponStar)
    {
        WeaponDic(WeaponID, WeaponClass, WeaponStar);
        for (int i = 0; i < EffectList.Count; i++)
        {
            DestroyImmediate(EffectList[i]);
        }
        EffectList.Clear();
        UpStarWindow.transform.Find("CantSynButton").gameObject.SetActive(false);
        UpStarWindow.transform.Find("SynButton").gameObject.SetActive(false);
        UpStarWindow.transform.Find("WeaponIcon").GetComponent<UISprite>().spriteName = ItemMaterial.WeaponID.ToString();
        // UpStarWindow.transform.Find("WeaponName").GetComponent<UILabel>().text = WeaponItem.itemName;
        UpStarWindow.transform.Find("WeaponName").GetComponent<UILabel>().text = ItemNameColor(num) + WeaponItem.itemName;
        StarNumber(WeaponStar);
        UpStarWindow.transform.Find("MaterialGrade").gameObject.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(ItemUpStar.NeedItemID1).itemGrade.ToString();
        UpStarWindow.transform.Find("MaterialGrade/MaterialIcon").gameObject.GetComponent<UISprite>().spriteName = ItemUpStar.NeedItemID1.ToString();
        ResetItem10105();
        if (WeaponStar != 5)
        {
            UpStarWindow.transform.Find("Sucess").gameObject.GetComponent<UILabel>().text = "成功率：" + TextTranslator.instance.GetWeaponUpStarByID(WeaponID, WeaponClass, WeaponStar + 1).UpStarRand * 100 + "%";
        }
        else
        {
            UpStarWindow.transform.Find("Sucess").gameObject.GetComponent<UILabel>().text = " ";
        }

        if (WeaponStar == 0)
        {
            UpStarWindow.transform.Find("BeforeUpGrid/Three").gameObject.GetComponent<UILabel>().text = "攻击：" + TextTranslator.instance.GetWeaponUpStarByID(WeaponID, WeaponClass, 0).Atk * 100 + "%";
            UpStarWindow.transform.Find("BeforeUpGrid/Two").gameObject.GetComponent<UILabel>().text = "防御：" + TextTranslator.instance.GetWeaponUpStarByID(WeaponID, WeaponClass, 0).Def * 100 + "%";
            UpStarWindow.transform.Find("BeforeUpGrid/One").gameObject.GetComponent<UILabel>().text = "生命：" + TextTranslator.instance.GetWeaponUpStarByID(WeaponID, WeaponClass, 0).Hp * 100 + "%";
            UpStarWindow.transform.Find("AfterUpGrid/Three").gameObject.GetComponent<UILabel>().text = LabelColor(4) + "攻击：[-]" + LabelColor(7) + TextTranslator.instance.GetWeaponUpStarByID(WeaponID, WeaponClass, WeaponStar + 1).Atk * 100 + "%";
            UpStarWindow.transform.Find("AfterUpGrid/Two").gameObject.GetComponent<UILabel>().text = LabelColor(4) + "防御：[-]" + LabelColor(7) + TextTranslator.instance.GetWeaponUpStarByID(WeaponID, WeaponClass, WeaponStar + 1).Def * 100 + "%";
            UpStarWindow.transform.Find("AfterUpGrid/One").gameObject.GetComponent<UILabel>().text = LabelColor(4) + "生命：[-]" + LabelColor(7) + TextTranslator.instance.GetWeaponUpStarByID(WeaponID, WeaponClass, WeaponStar + 1).Hp * 100 + "%";
        }
        else if (WeaponStar != 5)
        {
            UpStarWindow.transform.Find("BeforeUpGrid/Three").gameObject.GetComponent<UILabel>().text = LabelColor(4) + "攻击：[-]" + LabelColor(7) + ItemUpStar.Atk * 100 + "%";
            UpStarWindow.transform.Find("BeforeUpGrid/Two").gameObject.GetComponent<UILabel>().text = LabelColor(4) + "防御：[-]" + LabelColor(7) + ItemUpStar.Def * 100 + "%";
            UpStarWindow.transform.Find("BeforeUpGrid/One").gameObject.GetComponent<UILabel>().text = LabelColor(4) + "生命：[-]" + LabelColor(7) + ItemUpStar.Hp * 100 + "%";
            UpStarWindow.transform.Find("AfterUpGrid/Three").gameObject.GetComponent<UILabel>().text = LabelColor(4) + "攻击：[-]" + LabelColor(7) + TextTranslator.instance.GetWeaponUpStarByID(WeaponID, WeaponClass, WeaponStar + 1).Atk * 100 + "%";
            UpStarWindow.transform.Find("AfterUpGrid/Two").gameObject.GetComponent<UILabel>().text = LabelColor(4) + "防御：[-]" + LabelColor(7) + TextTranslator.instance.GetWeaponUpStarByID(WeaponID, WeaponClass, WeaponStar + 1).Def * 100 + "%";
            UpStarWindow.transform.Find("AfterUpGrid/One").gameObject.GetComponent<UILabel>().text = LabelColor(4) + "生命：[-]" + LabelColor(7) + TextTranslator.instance.GetWeaponUpStarByID(WeaponID, WeaponClass, WeaponStar + 1).Hp * 100 + "%";
        }
        else
        {
            UpClassWindowInfo(WeaponID, WeaponClass, WeaponStar);
            SynthesisWindow.SetActive(false);
            UpStarWindow.SetActive(false);
            UpClassWindow.SetActive(true);
            //UpStarWindow.transform.Find("BeforeUpGrid/Three").gameObject.GetComponent<UILabel>().text = "1";
            //UpStarWindow.transform.Find("BeforeUpGrid/Two").gameObject.GetComponent<UILabel>().text = "1";
            //UpStarWindow.transform.Find("BeforeUpGrid/One").gameObject.GetComponent<UILabel>().text = "1";
            //UpStarWindow.transform.Find("BeforeUpGrid/Three").gameObject.GetComponent<UILabel>().text = "";
            //UpStarWindow.transform.Find("BeforeUpGrid/Two").gameObject.GetComponent<UILabel>().text = "";
            //UpStarWindow.transform.Find("BeforeUpGrid/One").gameObject.GetComponent<UILabel>().text = "";
        }
        UpStarWindow.transform.Find("NeedMoney").gameObject.SetActive(false);
        if (TextTranslator.instance.GetItemCountByID(ItemUpStar.NeedItemID1) >= ItemUpStar.NeddItemNum1)
        {
            UpStarWindow.transform.Find("SynButton").gameObject.SetActive(true);
        }
        else
        {
            UpStarWindow.transform.Find("CantSynButton").gameObject.SetActive(true);
        }
    }
    public void UpClassWindowInfo(int WeaponID, int WeaponClass, int WeaponStar)
    {
        WeaponDic(WeaponID, WeaponClass, WeaponStar);
        UpClassWindow.transform.Find("UPItem1Grade").gameObject.SetActive(true);
        UpClassWindow.transform.Find("UPItem2Grade").gameObject.SetActive(true);
        UpClassWindow.transform.Find("WanNengButton").gameObject.SetActive(true);
        UpClassWindow.transform.Find("NeedMoney").gameObject.SetActive(true);
        UpClassWindow.transform.Find("CantSynButton").gameObject.SetActive(false);
        UpClassWindow.transform.Find("SynButton").gameObject.SetActive(false);
        UpClassWindow.transform.Find("WeaponIcon").GetComponent<UISprite>().spriteName = ItemMaterial.WeaponID.ToString();
        UpClassWindow.transform.Find("WeaponName").GetComponent<UILabel>().text = ItemNameColor(num)+WeaponItem.itemName;//

        UISprite Star1 = UpClassWindow.transform.Find("WeaponIcon/StarGrid/Star1").gameObject.GetComponent<UISprite>();
        UISprite Star2 = UpClassWindow.transform.Find("WeaponIcon/StarGrid/Star2").gameObject.GetComponent<UISprite>();
        UISprite Star3 = UpClassWindow.transform.Find("WeaponIcon/StarGrid/Star3").gameObject.GetComponent<UISprite>();
        UISprite Star4 = UpClassWindow.transform.Find("WeaponIcon/StarGrid/Star4").gameObject.GetComponent<UISprite>();
        UISprite Star5 = UpClassWindow.transform.Find("WeaponIcon/StarGrid/Star5").gameObject.GetComponent<UISprite>();
        Star1.spriteName = "Resultxing";
        Star2.spriteName = "Resultxing";
        Star3.spriteName = "Resultxing";
        Star4.spriteName = "Resultxing";
        Star5.spriteName = "Resultxing";
        UpClassWindow.transform.Find("UPItem1Grade").gameObject.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(10105).itemGrade;
        UpClassWindow.transform.Find("UPItem1Grade/MaterialIcon").gameObject.GetComponent<UISprite>().spriteName = "10105";
        string LackColor = "[ffffff]";
        if (TextTranslator.instance.GetItemCountByID(10105) < ItemUpClass.StoneNeedNum)
        {
            LackColor = "[ff0000]";
        }
        UpClassWindow.transform.Find("UPItem1Grade/Number").gameObject.GetComponent<UILabel>().text = LackColor + TextTranslator.instance.GetItemCountByID(10105) + "[-]/" + ItemUpClass.StoneNeedNum;
        UpClassWindow.transform.Find("UPItem2Grade").gameObject.GetComponent<UISprite>().spriteName = "yxdi" + (TextTranslator.instance.GetItemByItemCode(HeroID.cardID).itemGrade - 1);
        UpClassWindow.transform.Find("UPItem2Grade/MaterialIcon").gameObject.GetComponent<UISprite>().spriteName = HeroID.cardID.ToString();
        LackColor = "[ffffff]";
        if (TextTranslator.instance.GetItemCountByID(HeroID.cardID + 10000) < ItemUpClass.RoleDebrisNum)
        {
            LackColor = "[ff0000]";
        }
        UpClassWindow.transform.Find("UPItem2Grade/Number").gameObject.GetComponent<UILabel>().text = LackColor + TextTranslator.instance.GetItemCountByID(HeroID.cardID + 10000) + "[-]/" + ItemUpClass.RoleDebrisNum;
        if (WeaponClass != 5 && WeaponClass != 0)
        {
            UpClassWindow.transform.Find("Reward").GetComponent<UILabel>().text = "进阶后激活属性：" + LabelColor(3) + "HP+" + TextTranslator.instance.GetWeaponUpClassByID(WeaponID, WeaponClass + 1).Hp + "\t攻击+" + TextTranslator.instance.GetWeaponUpClassByID(WeaponID, WeaponClass + 1).Atk + "\t防御+" + TextTranslator.instance.GetWeaponUpClassByID(WeaponID, WeaponClass + 1).Def; ;
        }
        else if (WeaponClass == 5)
        {
            UpClassWindow.transform.Find("Reward").GetComponent<UILabel>().text = "神器已经满阶";
            UpClassWindow.transform.Find("UPItem1Grade").gameObject.SetActive(false);
            UpClassWindow.transform.Find("UPItem2Grade").gameObject.SetActive(false);
            UpClassWindow.transform.Find("WanNengButton").gameObject.SetActive(false);
            UpClassWindow.transform.Find("NeedMoney").gameObject.SetActive(false);
        }
        if (CharacterRecorder.instance.gold > ItemUpClass.NeedGold)
        {
            UpClassWindow.transform.Find("NeedMoney").GetComponent<UILabel>().text = LabelColor(1) + "2000" + "[-]";//绿色
        }
        else
        {
            UpClassWindow.transform.Find("NeedMoney").GetComponent<UILabel>().text = LabelColor(2) + "2000" + "[-]";//红色
        }
        if (CharacterRecorder.instance.gold > ItemUpClass.NeedGold && TextTranslator.instance.GetItemCountByID(10105) >= ItemUpClass.StoneNeedNum && TextTranslator.instance.GetItemCountByID(HeroID.cardID + 10000) >= ItemUpClass.RoleDebrisNum)
        {
            isLoseGold = false;
            if (WeaponClass != 5)
            {
                UpClassWindow.transform.Find("SynButton").gameObject.SetActive(true);
            }
        }
        else
        {
            isLoseGold = true;
            if (WeaponClass != 5)
            {
                UpClassWindow.transform.Find("CantSynButton").gameObject.SetActive(true);
            }
        }
        if (WeaponStar == 5)
        {
            UpClassWindow.transform.Find("AfterUpGrid/Three").gameObject.GetComponent<UILabel>().text = LabelColor(4) + "攻击：[-]" + LabelColor(7) + ItemUpStar.Atk * 100 + "%";
            UpClassWindow.transform.Find("AfterUpGrid/Two").gameObject.GetComponent<UILabel>().text = LabelColor(4) + "防御：[-]" + LabelColor(7) + ItemUpStar.Def * 100 + "%";
            UpClassWindow.transform.Find("AfterUpGrid/One").gameObject.GetComponent<UILabel>().text = LabelColor(4) + "生命：[-]" + LabelColor(7) + ItemUpStar.Hp * 100 + "%";
        }
        RoleBreach rb = TextTranslator.instance.GetRoleBreachByID(HeroID.cardID, HeroID.rank);
        Item2OwnedCount = TextTranslator.instance.GetItemCountByID(rb.roleId + 10000);
        Item2NeedCount = ItemUpClass.RoleDebrisNum;
        Item2Code = rb.roleId + 10000;
    }
    public void ResetItem10105()
    {
        WeaponUpStar _WeaponUpStar = TextTranslator.instance.GetWeaponUpStarByID(WeaponItemID, NowWeaponClass, NowWeaponStar);
        WeaponUpStar _WeaponUpStar2 = TextTranslator.instance.GetWeaponUpStarByWeaponUpStarID(_WeaponUpStar.ID + 1);
        if (_WeaponUpStar.ID + 1 == TextTranslator.instance.WeaponUpStarDic.size) 
        {
            _WeaponUpStar2 = _WeaponUpStar;
        }
        if (TextTranslator.instance.GetItemCountByID(ItemUpStar.NeedItemID1) >= ItemUpStar.NeddItemNum1)
        {
            UpStarWindow.transform.Find("MaterialGrade/Number").gameObject.GetComponent<UILabel>().text = "[ffffff]" + TextTranslator.instance.GetItemCountByID(_WeaponUpStar2.NeedItemID1) + "[-]/" + _WeaponUpStar2.NeddItemNum1;
        }
        else
        {
            UpStarWindow.transform.Find("MaterialGrade/Number").gameObject.GetComponent<UILabel>().text = "[ff0000]" + TextTranslator.instance.GetItemCountByID(_WeaponUpStar2.NeedItemID1) + "[-]/" + _WeaponUpStar2.NeddItemNum1;
        }
        //if (TextTranslator.instance.GetItemCountByID(ItemUpStar.NeedItemID1) >= ItemUpStar.NeddItemNum1)
        //{
        //    UpStarWindow.transform.Find("MaterialGrade/Number").gameObject.GetComponent<UILabel>().text = "[ffffff]" + TextTranslator.instance.GetItemCountByID(ItemUpStar.NeedItemID1) + "[-]/" + TextTranslator.instance.GetWeaponUpStarByWeaponUpStarID(ItemUpStar.ID + 1).NeddItemNum1;
        //}
        //else
        //{
        //    UpStarWindow.transform.Find("MaterialGrade/Number").gameObject.GetComponent<UILabel>().text = "[ff0000]" + TextTranslator.instance.GetItemCountByID(ItemUpStar.NeedItemID1) + "[-]/" + TextTranslator.instance.GetWeaponUpStarByWeaponUpStarID(ItemUpStar.ID + 1).NeddItemNum1;
        //}
    }

    public void ShowShengXingFailed()
    {
        StartCoroutine(ShowShenXingSB());
    }

    IEnumerator ShowShenXingSB()
    {
        GameObject go = Instantiate(loseEffect) as GameObject;
        go.transform.parent = UpStarWindow.transform;
        go.transform.localPosition = Vector3.zero;
        go.transform.localScale = new Vector3(200, 200, 200);
        go.SetActive(true);
        EffectList.Add(go);
        yield return new WaitForSeconds(1f);
        for (int i = 0; i < EffectList.Count; i++)
        {
            DestroyImmediate(EffectList[i]);
        }
        EffectList.Clear();
    }
    void StarNumber(int WeaponStar)
    {
        UISprite Star1 = UpStarWindow.transform.Find("WeaponIcon/StarGrid/Star1").gameObject.GetComponent<UISprite>();
        UISprite Star2 = UpStarWindow.transform.Find("WeaponIcon/StarGrid/Star2").gameObject.GetComponent<UISprite>();
        UISprite Star3 = UpStarWindow.transform.Find("WeaponIcon/StarGrid/Star3").gameObject.GetComponent<UISprite>();
        UISprite Star4 = UpStarWindow.transform.Find("WeaponIcon/StarGrid/Star4").gameObject.GetComponent<UISprite>();
        UISprite Star5 = UpStarWindow.transform.Find("WeaponIcon/StarGrid/Star5").gameObject.GetComponent<UISprite>();
        Star1.spriteName = "Resultxingdi";
        Star2.spriteName = "Resultxingdi";
        Star3.spriteName = "Resultxingdi";
        Star4.spriteName = "Resultxingdi";
        Star5.spriteName = "Resultxingdi";
        switch (WeaponStar)
        {
            case 1:
                Star1.spriteName = "Resultxing";
                break;
            case 2:
                Star1.spriteName = "Resultxing";
                Star2.spriteName = "Resultxing";

                break;
            case 3:
                Star1.spriteName = "Resultxing";
                Star2.spriteName = "Resultxing";
                Star3.spriteName = "Resultxing";
                break;
            case 4:
                Star1.spriteName = "Resultxing";
                Star2.spriteName = "Resultxing";
                Star3.spriteName = "Resultxing";
                Star4.spriteName = "Resultxing";

                break;
            case 5:
                Star1.spriteName = "Resultxing";
                Star2.spriteName = "Resultxing";
                Star3.spriteName = "Resultxing";
                Star4.spriteName = "Resultxing";
                Star5.spriteName = "Resultxing";
                break;
        }
    }
    //item字体颜色
    public string ItemNameColor(int GradeID)
    {
        string NameColor = "";
        switch (GradeID)
        {
            case 42001:
                NameColor = "[-][B3B3B3]";
                break;
            case 42002:
                NameColor = "[-][28DF5E]";
                break;
            case 42003:
                NameColor = "[-][12A7B8]";
                break;
            case 42004:
                NameColor = "[-][842DCE]";
                break;
            case 42005:
                NameColor = "[-][DC582D]";
                break;
            case 42006:
                NameColor = "[-][D9181E]";
                break;
        }
        return NameColor;
    }
    public void UpDataItemStateAfterExChange()
    {
        RoleBreach rb = TextTranslator.instance.GetRoleBreachByID(HeroID.cardID, HeroID.rank);
        string _labelColorStr = "";
        if (TextTranslator.instance.GetItemCountByID(HeroID.cardID + 10000) == null)
        {
            _labelColorStr = "[ff0000]";
            //UpClassWindow.transform.Find("UPItem2Grade").gameObject.transform.FindChild("Mask").gameObject.SetActive(true);
        }
        else
        {
            if (TextTranslator.instance.GetItemCountByID(HeroID.cardID + 10000) >= rb.debrisNum)
            {
                _labelColorStr = "[3ee817]";
            }
            else
            {
                _labelColorStr = "[ff0000]";
            }
            //Item2.transform.FindChild("Mask").gameObject.SetActive(false);
        }
        Item2OwnedCount = TextTranslator.instance.GetItemCountByID(HeroID.cardID + 10000);
        //Item2.transform.FindChild("Label").GetComponent<UILabel>().text = string.Format(_labelColorStr + "{0}[-]/{1}", TextTranslator.instance.GetItemCountByID(rb.roleId + 10000) != null ? TextTranslator.instance.GetItemCountByID(rb.roleId + 10000) : 0,
        //   rb.debrisNum);
        UpClassWindow.transform.Find("UPItem2Grade/Number").gameObject.GetComponent<UILabel>().text = TextTranslator.instance.GetItemCountByID(HeroID.cardID + 10000) + "/" + ItemUpClass.RoleDebrisNum;
        Item2NeedCount = ItemUpClass.RoleDebrisNum;
        PieceStoneExChangeBoard.IsAfterChanged = true;
        GameObject.Find("PieceStoneExChangeBoard").GetComponent<PieceStoneExChangeBoard>().SetPieceStoneExChangeBoardInfo(Item2Code, Item2OwnedCount, Item2NeedCount);
    }
    //绿色和红色
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
                LabelColor = "[-][ff8304]";//橙色 248 145 19
                break;
            case 4:
                LabelColor = "[-][249BD2]";//蓝色 36 155  210
                break;
            case 5:
                LabelColor = "[-][FFFFFF]";//白色
                break;
            case 6:
                LabelColor = "[-][75a3b8]";//灰色
                break;
            case 7:
                LabelColor = "[-][8ccef2]";//浅蓝 140 206 242
                break;
        }
        return LabelColor;
    }
}
