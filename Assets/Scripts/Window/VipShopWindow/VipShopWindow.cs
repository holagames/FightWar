using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VipShopWindow : MonoBehaviour
{

    public UILabel TopLabelGrade;//等级
    public UILabel TopLabelNum;//需要充值的钻石数
    public UILabel TopLabelDenji;//可成为的等级

    public GameObject VipShopScrollView;
    public GameObject VipPrivilegeScrollView;
    public GameObject UiGridShop;
    public GameObject UiGridPrivilege;
    public GameObject VipPrivilegeItem;
    public GameObject LeftButton;
    public GameObject RightButton;

    public int BuyVipIndex = 0;//当前购买vip的等级

    public UICenterOnChild uicenter;
    private int Index;
    private int TypeWindow;
    private int EnterNum = 0;
    List<GameObject> ListVipShopItem = new List<GameObject>();
    List<GameObject> ListPrivilegeItem = new List<GameObject>();
    void Start()
    {

		#if UNITY_IOSOFFCIAL
		this.gameObject.AddComponent<VipUnilbil>();
#endif
        UIManager.instance.CountActivitys(UIManager.Activitys.Vip系统);
        UIManager.instance.UpdateActivitys(UIManager.Activitys.Vip系统);

        TypeWindow = 1;
        SetTopMation();
        Tosureishaveoneyuan();
        //LeftButton.SetActive(false);
        //RightButton.SetActive(false);
        if (UIEventListener.Get(LeftButton).onClick == null)
        {
            UIEventListener.Get(LeftButton).onClick += delegate(GameObject go)
            {
                SetLeftPosition();
            };
        }
        if (UIEventListener.Get(RightButton).onClick == null)
        {
            UIEventListener.Get(RightButton).onClick += delegate(GameObject go)
            {
                SetRightPosition();
            };
        }
        if (UIEventListener.Get(GameObject.Find("TopButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("TopButton")).onClick += delegate(GameObject go)
            {
                ChangingOver();
            };
        }


    }
    void ChangingOver() //切换
    {
        if (TypeWindow == 0)
        {
            GameObject.Find("TopButton").transform.Find("Label").GetComponent<UILabel>().text = "V特权";
            VipShopScrollView.SetActive(true);
            VipPrivilegeScrollView.SetActive(false);
            //LeftButton.SetActive(false);
            //RightButton.SetActive(false);
            TypeWindow = 1;
        }
        else if (TypeWindow == 1 && EnterNum == 0)
        {
            GameObject.Find("TopButton").transform.Find("Label").GetComponent<UILabel>().text = "充 值";
            VipShopScrollView.SetActive(false);
            VipPrivilegeScrollView.SetActive(true);
            AddPrivilegeItem();
            //LeftButton.SetActive(true);
            //RightButton.SetActive(true);
            TypeWindow = 0;
            EnterNum = 1;
        }
        else if (TypeWindow == 1 && EnterNum == 1)
        {
            GameObject.Find("TopButton").transform.Find("Label").GetComponent<UILabel>().text = "充 值";
            VipShopScrollView.SetActive(false);
            VipPrivilegeScrollView.SetActive(true);
            AddPrivilegeItem();
            //LeftButton.SetActive(true);
            //RightButton.SetActive(true);
            //StartCoroutine(SetCenterPosition());
            TypeWindow = 0;
        }
    }
    void SetRightPosition()//向右点击
    {
        if (TypeWindow == 1)
        {
            UiGridShop.transform.parent.localPosition = new Vector3(-305f, -72f, 0);
            UiGridShop.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2(305f, 0);
        }
        else if (TypeWindow == 0)
        {
            if (Index < 14)//向左移动一
            {
                Transform traobj = uicenter.transform.GetChild(Index + 1);
                uicenter.CenterOn(traobj);
                Index += 1;
            }
        }
    }
    void SetLeftPosition() //左按钮
    {
        if (TypeWindow == 1)
        {
            UiGridShop.transform.parent.localPosition = new Vector3(0, -72f, 0);
            UiGridShop.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2(0, 0);
        }
        else if (TypeWindow == 0)
        {
            if (Index > 0)//向右移动一
            {
                Transform traobj = uicenter.transform.GetChild(Index - 1);
                uicenter.CenterOn(traobj);
                Index -= 1;
            }
        }
    }
    void DestroyGride()
    {
        ListPrivilegeItem.Clear();
        for (int i = UiGridPrivilege.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(UiGridPrivilege.transform.GetChild(i).gameObject);
        }
    }
    IEnumerator SetCenterPosition() //初始化时确定中间特权等级
    {
        int VipNum = CharacterRecorder.instance.Vip;
        //Transform traobj = uicenter.transform.GetChild(VipNum);
        yield return new WaitForSeconds(0.1f);
        if (VipNum == 0)
        {
            Transform traobj = uicenter.transform.GetChild(VipNum);
            uicenter.CenterOn(traobj);
            Index = 0;
        }
        else
        {
            Transform traobj = uicenter.transform.GetChild(VipNum - 1);
            uicenter.CenterOn(traobj);
            Index = VipNum - 1;
        }
    }
    void AddPrivilegeItem() //添加特权预制
    {
        DestroyGride();
        for (int i = 0; i < 15; i++)
        {
            Vip vipinfo = TextTranslator.instance.GetVipDicByID(i + 1);
            GameObject PrivilegeObj = NGUITools.AddChild(UiGridPrivilege, VipPrivilegeItem);
            PrivilegeObj.SetActive(true);
            PrivilegeObj.transform.localPosition = new Vector3(980 * i, 0, 0);
            PrivilegeObj.GetComponent<VipPrivilegeItem>().SetIndexInfo(vipinfo);
            PrivilegeObj.name = (i + 1).ToString();
            ListPrivilegeItem.Add(PrivilegeObj);
            //PrivilegeObj.transform.Find("LabelExplain").GetComponent<UILabel>().text = "V" + vipinfo.VipID.ToString() + "特权";
        }
        StartCoroutine(SetCenterPosition());
    }


    public void SetTopMation() //顶部信息
    {
        int VipNum = CharacterRecorder.instance.Vip;//当前Vip等级
        int TheDiamondCount = CharacterRecorder.instance.PayDiamond;//当前已经冲的钻石数量;
        Debug.Log("VipNum" + VipNum);
        //if (VipNum == 15)  //8.10修改
        //{
        //    int NextDiamondCount = TextTranslator.instance.GetVipDicByID(VipNum).TradingDiamondCount;
        //    TopLabelGrade.text = "V" + VipNum.ToString();
        //    TopLabelNum.text = "0";
        //    TopLabelDenji.text = "V15";
        //    GameObject.Find("BackSlider").GetComponent<UISlider>().value = 1f;
        //    GameObject.Find("BackSlider").transform.Find("Label").GetComponent<UILabel>().text = TheDiamondCount.ToString() + "/" + NextDiamondCount.ToString();
        //}
        //else 
        //{
        //    int NextDiamondCount = TextTranslator.instance.GetVipDicByID(VipNum + 1).TradingDiamondCount;
        //    TopLabelGrade.text = "V" + VipNum.ToString();
        //    TopLabelNum.text = (NextDiamondCount - CharacterRecorder.instance.VipExp).ToString();
        //    TopLabelDenji.text = "V" + (VipNum + 1).ToString();
        //    GameObject.Find("BackSlider").GetComponent<UISlider>().value = (float)CharacterRecorder.instance.VipExp / NextDiamondCount;
        //    GameObject.Find("BackSlider").transform.Find("Label").GetComponent<UILabel>().text = CharacterRecorder.instance.VipExp.ToString() + "/" + NextDiamondCount.ToString();
        //}

        if (VipNum >= 15)  //8.10修改
        {
            if (VipNum == 15)
            {
                int NextDiamondCount = TextTranslator.instance.GetVipDicByID(VipNum).TradingDiamondCount;
                TopLabelGrade.text = "V" + VipNum.ToString();
                TopLabelNum.transform.parent.gameObject.SetActive(false);
                GameObject.Find("BackSlider").GetComponent<UISlider>().value = 1f;
                GameObject.Find("BackSlider").transform.Find("Label").GetComponent<UILabel>().text = CharacterRecorder.instance.VipExp.ToString() + "/" + NextDiamondCount.ToString();
            }
            else
            {

                int NextDiamondCount = TextTranslator.instance.GetVipDicByID(VipNum + 1).TradingDiamondCount;


                TopLabelGrade.text = "V" + VipNum.ToString();

                TopLabelNum.transform.parent.gameObject.SetActive(false);

                GameObject.Find("BackSlider").GetComponent<UISlider>().value = (float)CharacterRecorder.instance.VipExp / NextDiamondCount;
                GameObject.Find("BackSlider").transform.Find("Label").GetComponent<UILabel>().text = CharacterRecorder.instance.VipExp.ToString() + "/" + NextDiamondCount.ToString();
            }
        }
        else
        {
            int NextDiamondCount = TextTranslator.instance.GetVipDicByID(VipNum + 1).TradingDiamondCount;
            TopLabelGrade.text = "V" + VipNum.ToString();
            TopLabelNum.text = (NextDiamondCount - CharacterRecorder.instance.VipExp).ToString();
            TopLabelDenji.text = "V" + (VipNum + 1).ToString();
            GameObject.Find("BackSlider").GetComponent<UISlider>().value = (float)CharacterRecorder.instance.VipExp / NextDiamondCount;
            GameObject.Find("BackSlider").transform.Find("Label").GetComponent<UILabel>().text = CharacterRecorder.instance.VipExp.ToString() + "/" + NextDiamondCount.ToString();
        }
    }

    public void RefreshItem()//购买礼包成功后刷新
    {
        int VipId = BuyVipIndex;
        Debug.LogError("vipnum" + BuyVipIndex.ToString());
        if (BuyVipIndex > 1)
        {
            //List<Item> _itemList = new List<Item>();
            Vip vipinfo = TextTranslator.instance.GetVipDicByID(VipId);
            CharacterRecorder.instance.BuyGiftBag[BuyVipIndex - 1] = "1";
            //_itemList.Add(new Item(22000 + BuyVipIndex, 1));

            //UIManager.instance.OpenSinglePanel("AdvanceWindow", false);
            //GameObject.Find("AdvanceWindow").GetComponent<AdvanceWindow>().SetInfo(AdvanceWindowType.GainResult, null, null, null, null, _itemList);
            int num = 22000 + BuyVipIndex;
            NetworkHandler.instance.SendProcess("5002#" + num.ToString() + ";1;");
        }
        else
        {
            NetworkHandler.instance.SendProcess("5002#22001;1;");
        }
        //UpDateTopContentData(_itemList);
        NetworkHandler.instance.SendProcess("9201#;");

        foreach (var obj in ListPrivilegeItem)
        {
            if (obj.name == VipId.ToString())
            {
                obj.GetComponent<VipPrivilegeItem>().SpriteCost.SetActive(true);
                obj.GetComponent<VipPrivilegeItem>().BuyButton.SetActive(false);
                obj.GetComponent<VipPrivilegeItem>().HaveButton.SetActive(true);
                break;
            }
        }
    }

    void UpDateTopContentData(List<Item> _itemList)
    {
        for (int i = 0; i < _itemList.Count; i++)
        {
            switch (_itemList[i].itemCode)
            {
                case 90001: CharacterRecorder.instance.gold += _itemList[i].itemCount; break;
                case 90002: CharacterRecorder.instance.lunaGem += _itemList[i].itemCount; break;
                case 90003: CharacterRecorder.instance.HonerValue += _itemList[i].itemCount; break;
                case 90004: CharacterRecorder.instance.TrialCurrency += _itemList[i].itemCount; break;
                case 90005: CharacterRecorder.instance.ArmyGroup += _itemList[i].itemCount; break;
                case 90006: break;
                case 90007: CharacterRecorder.instance.stamina += _itemList[i].itemCount; break;
                case 90008: CharacterRecorder.instance.sprite += _itemList[i].itemCount; break;
                default: //TextTranslator.instance.SetItemCountAddByID(_itemList[i].itemCode, _itemList[i].itemCount);
                    break;
            }
        }
        if (GameObject.Find("TopContent") != null)
        {
            GameObject.Find("TopContent").GetComponent<TopContent>().Reset();
        }
    }

    public void SetChangeButton() //其他窗口切换到特权界面
    {
        ChangingOver();
    }
    public void SetChangeShop()
    {
        TypeWindow = 0;
        ChangingOver(); //切换
    }

    public void SetIsFristDiamond() //判定是否第一次充值
    {
        for (int i = 0; i < UiGridShop.transform.childCount; i++)
        {
            UiGridShop.transform.GetChild(i).GetComponent<VipShopItem>().SetItemInfo();
        }
    }

    private void Tosureishaveoneyuan()
    {
        for (int i = 0; i < TextTranslator.instance.chargeItemList.size; i++)
        {
            Exchange mItem = TextTranslator.instance.GetExchangeById((i + 1));
            if (mItem.cash == 1)
            {
                UiGridShop.transform.GetChild(UiGridShop.transform.childCount - 1).gameObject.SetActive(true);
                break;
            }
        }
    }
}
