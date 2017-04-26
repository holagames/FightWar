using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ChipWindow : MonoBehaviour
{
    public List<GameObject> ButtonList = new List<GameObject>();
    public List<GameObject> ItemList = new List<GameObject>();
    public GameObject SureButton;
    public GameObject BlackButton;
    public GameObject AddInfoButton;
    public GameObject QuesButton;
    public List<int> ItemIDList = new List<int>();
    public int ChipItemColor = 0;
    public int ChipItemID = 0;
    public UILabel CostMessag;
    //属性界面
    public GameObject AttributeWindow;
    public GameObject AttributeWindowEffect;
    public GameObject AttributeWindowEffectClone;
    public List<string> AttributeNameList = new List<string>();
    public List<float> AttributeValueList = new List<float>();
    public List<GameObject> AttributeItemList = new List<GameObject>();
    //说明界面
    public GameObject QuestionWindow;
    //插入特效List
    public List<GameObject> EffectTowList = new List<GameObject>();
    public bool isInsert = false;
    //插入显示圈特效List
    public List<GameObject> EffectOneList = new List<GameObject>();
    //拥有特效List
    public List<GameObject> EffectThreeList = new List<GameObject>();
    public GameObject EffectThreeItem;
    //合成特效
    public GameObject EffectSyns;
    public GameObject Black;
    //
    public bool isOpenFullEffect = false;
    public bool isOpenEffectThree = false;
    public bool isFristOpen = false;
    public int HadFinishID = 0;
    public bool isClickButton = false;
    // Use this for initialization
    void Start()
    {
        NetworkHandler.instance.SendProcess("1611#");
        UIEventListener.Get(ButtonList[0]).onClick += delegate(GameObject go)
        {
            if (ChipItemColor > 1)
            {
                isClickButton = true;
                ItemListInfo(1, 4);
            }
            else if (ChipItemColor == 1)
            {
                isClickButton = true;
                ItemListInfo(1, ChipItemID);
            }
        };
        UIEventListener.Get(ButtonList[1]).onClick += delegate(GameObject go)
        {
            if (ChipItemColor > 2)
            {
                isClickButton = true;
                ItemListInfo(2, 4);
            }
            else if (ChipItemColor == 2)
            {
                isClickButton = true;
                ItemListInfo(2, ChipItemID);
            }
            else
            {
                UIManager.instance.OpenPromptWindow("未开启", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(ButtonList[2]).onClick += delegate(GameObject go)
        {
            if (ChipItemColor > 3)
            {
                isClickButton = true;
                ItemListInfo(3, 4);
            }
            else if (ChipItemColor == 3)
            {
                isClickButton = true;
                ItemListInfo(3, ChipItemID);
            }
            else
            {
                UIManager.instance.OpenPromptWindow("未开启", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(ButtonList[3]).onClick += delegate(GameObject go)
        {
            if (ChipItemColor > 4)
            {
                isClickButton = true;
                ItemListInfo(4, 4);
            }
            else if (ChipItemColor == 4)
            {
                isClickButton = true;
                ItemListInfo(4, ChipItemID);
            }
            else
            {
                UIManager.instance.OpenPromptWindow("未开启", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(ButtonList[4]).onClick += delegate(GameObject go)
        {

            if (ChipItemColor > 5)
            {
                isClickButton = true;
                ItemListInfo(5, 4);
            }
            else if (ChipItemColor == 5)
            {
                isClickButton = true;
                ItemListInfo(5, ChipItemID);
            }
            else
            {
                UIManager.instance.OpenPromptWindow("未开启", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(ButtonList[5]).onClick += delegate(GameObject go)
        {
            if (ChipItemColor > 6)
            {
                isClickButton = true;
                ItemListInfo(6, 4);
            }
            else if (ChipItemColor == 6)
            {
                isClickButton = true;
                ItemListInfo(6, ChipItemID);
            }
            else
            {
                UIManager.instance.OpenPromptWindow("未开启", PromptWindow.PromptType.Hint, null, null);
            }
        };
        UIEventListener.Get(SureButton).onClick += delegate(GameObject go)
        {
            Debug.LogError("发送协议");
            NetworkHandler.instance.SendProcess("1612#");
            isOpenEffectThree = true;
            CharacterRecorder.instance.SBlaoxu = false;
        };
        UIEventListener.Get(AddInfoButton).onClick += delegate(GameObject go)
        {
            Debug.LogError("属性界面");
            AttributeWindow.SetActive(true);
            AttributeWindowInfo();
        };
        UIEventListener.Get(QuesButton).onClick += delegate(GameObject go)
        {
            Debug.LogError("问题");
            QuestionWindow.SetActive(true);
            QuestionWindowInfo();
        };
    }

    public void SetInfo(int color, int id)
    {
        if (id == 4)
        {
            isOpenFullEffect = true;
        }
        else
        {
            isOpenFullEffect = false;
        }
        if (color == 0)
        {
            ChipItemColor = 1;
        }
        else
        {
            ChipItemColor = color;
        }
        ChipItemID = id;
        if (color == 7 && id == 0)
        {
            ChipItemColor = 6;
            ChipItemID = 4;
        }
        if (CharacterRecorder.instance.SBlaoxu)
        {
            if (id == 4)
            {
                if (ChipItemColor < 7)
                {
                    ChipItemColor = ChipItemColor + 1;
                    ChipItemID = 0;
                }
            }
        }
        StopCoroutine("EffectShow");
        if (id > 0 && isInsert)
        {
            StartCoroutine(EffectShow(id));
        }
        ItemListInfo(ChipItemColor, ChipItemID);


    }
    IEnumerator EffectShow(int id)
    {
        EffectTowList[id - 1].SetActive(true);
        yield return new WaitForSeconds(1f);
        EffectTowList[id - 1].SetActive(false);
        isInsert = false;
    }
    void ItemListInfo(int color, int id)
    {
        Debug.LogError("sss " + color + "   " + id);
        if (id == 0 || id == 4 || isFristOpen == false)
        {
            HadFinishID = 0;
        }
        else
        {
            HadFinishID = id - 1;
        }
        SureButton.SetActive(false);
        BlackButton.SetActive(false);
        CostMessag.gameObject.SetActive(false);
        for (int i = 0; i < 6; i++)
        {
            if (i > color - 1)
            {
                ButtonList[i].GetComponent<UIToggle>().enabled = false;
            }
            else
            {
                ButtonList[i].GetComponent<UIToggle>().enabled = true;
            }
            if (i == color - 1 && id != 4)
            {
                ButtonList[i].GetComponent<UIToggle>().value = true;
                if (TextTranslator.instance.GetItemCountByID(90014) > 0)
                {
                    SureButton.SetActive(true);
                    CostMessag.gameObject.SetActive(true);
                    CostMessag.text = "1/" + TextTranslator.instance.GetItemCountByID(90014).ToString();
                }
                else
                {
                    BlackButton.SetActive(true);
                }
            }
        }
        StartCoroutine(CollectionHadItem(color, id));
        if (isOpenFullEffect && CharacterRecorder.instance.SBlaoxu == false)
        {
            StartCoroutine(CollectionFull(color, id));
        }
        for (int i = 0; i < 6; i++)
        {
            ButtonList[i].transform.Find("RedPoint").gameObject.SetActive(false);
            ButtonList[i].transform.Find("Lock").gameObject.SetActive(true);
        }
        if (ChipItemColor != 7)
        {
            if (TextTranslator.instance.GetItemCountByID(90014) > 0)
            {
                ButtonList[ChipItemColor - 1].transform.Find("RedPoint").gameObject.SetActive(true);
            }
            for (int i = 0; i < ChipItemColor; i++)
            {
                ButtonList[i].transform.Find("Lock").gameObject.SetActive(false);
            }
        }
        isFristOpen = true;
    }
    string SpriteNameInfo(int color)
    {
        string name = "";
        switch (color)
        {
            case 1:
                name = "xinpian_bai";
                break;
            case 2:
                name = "xinpian_lv";
                break;
            case 3:
                name = "xinpian_lan";
                break;
            case 4:
                name = "xinpian_zi";
                break;
            case 5:
                name = "xinpian_ju";
                break;
            case 6:
                name = "xinpian_hei";
                break;
            default:
                name = "xinpian_hui";
                break;
        }
        return name;
    }

    IEnumerator CollectionHadItem(int color, int id)
    {
        Debug.LogError("aaaa " + color + "   " + id + "     " + HadFinishID);
        //for (int i = 0; i < 4; i++)
        //{
        //    if (i >= HadFinishID || id == 0)
        //    {
        //        ItemList[i].transform.Find("Icon").GetComponent<UISprite>().spriteName = "";
        //        ItemList[i].transform.Find("AddInfo").GetComponent<UILabel>().text = "";
        //        EffectOneList[i].SetActive(false);
        //    }
        //}
        for (int i = 0; i < 4; i++)
        {
            Chip item = TextTranslator.instance.GetChipListByID(color, i + 1);
            if (i >= HadFinishID || id == 0)
            {
                ItemList[i].transform.Find("AddInfo").GetComponent<TweenScale>().enabled = false;
                ItemList[i].transform.Find("Icon").GetComponent<TweenPosition>().enabled = false;
                EffectOneList[i].SetActive(false);
            }

        }
        for (int i = id; i < 4; i++)
        {
            Chip item = TextTranslator.instance.GetChipListByID(color, i + 1);
            string[] stringlist = item.Des.Split('+');
            ItemList[i].transform.Find("Icon").GetComponent<UISprite>().spriteName = "xinpian_hui" + (i + 1).ToString();
            ItemList[i].transform.Find("AddInfo").GetComponent<UILabel>().text = "[527283]" + stringlist[0] + "[-][187e69]+" + stringlist[1];
        }
        for (int i = 0; i < id; i++)
        {
            Chip item = TextTranslator.instance.GetChipListByID(color, i + 1);
            string[] stringlist = item.Des.Split('+');
            if (i >= HadFinishID || id == 0)
            {
                if (isOpenEffectThree)
                {
                    GameObject go = Instantiate(EffectThreeItem) as GameObject;
                    go.transform.parent = EffectThreeList[i].transform;
                    go.transform.localScale = Vector3.one;
                    go.transform.localPosition = Vector3.zero;
                    go.SetActive(true);
                    go.name = EffectThreeItem.name;

                }
                EffectOneList[i].SetActive(true);
                ItemList[i].transform.Find("Icon").GetComponent<UISprite>().spriteName = SpriteNameInfo(color) + (i + 1).ToString();
                ItemList[i].transform.Find("AddInfo").GetComponent<UILabel>().text = "[bcddf6]" + stringlist[0] + "[-][03ed6b]+" + stringlist[1];
                if (CharacterRecorder.instance.SBlaoxu == false)
                {
                    ItemList[i].transform.Find("AddInfo").GetComponent<TweenScale>().enabled = true;
                }
                ItemList[i].transform.Find("Icon").GetComponent<TweenPosition>().enabled = true;
            }
        }
        HadFinishID = id;
        yield return new WaitForSeconds(0.1f);
        if (isOpenEffectThree)
        {
            AudioEditer.instance.PlayOneShot("ui_recieve");
        }
        isOpenEffectThree = false;
        yield return new WaitForSeconds(0.4f);


        if (isOpenEffectThree)
        {
            for (int i = 0; i < 4; i++)
            {
                if (i < id && id != 0 && EffectThreeList[i].transform.childCount != 0)
                {
                    if (EffectThreeList[i].transform.Find("XinPian03").gameObject != null)
                    {
                        DestroyImmediate(EffectThreeList[i].transform.Find("XinPian03").gameObject);
                    }
                }
            }
        }
    }
    IEnumerator CollectionFull(int color, int id)
    {
        if (id == 4)
        {

            for (int i = 0; i < 4; i++)
            {
                ItemList[i].transform.Find("Icon").gameObject.SetActive(false);
            }
            //EffectSyns.SetActive(true);
            GameObject go = Instantiate(EffectSyns) as GameObject;
            go.SetActive(true);
            go.transform.parent = this.gameObject.transform.Find("Window");
            go.transform.localPosition = new Vector3(-6.2f, 29.3f, 0);
            go.transform.localScale = Vector3.one;
            go.transform.Find("GameObject/GameObject/XinPian_ani/Plane01").GetComponent<Renderer>().material.mainTexture = Resources.Load("Game/" + SpriteNameInfo(color) + "1") as Texture;
            go.transform.Find("GameObject/GameObject/XinPian_ani/Plane02").GetComponent<Renderer>().material.mainTexture = Resources.Load("Game/" + SpriteNameInfo(color) + "2") as Texture;
            go.transform.Find("GameObject/GameObject/XinPian_ani/Plane03").GetComponent<Renderer>().material.mainTexture = Resources.Load("Game/" + SpriteNameInfo(color) + "3") as Texture;
            go.transform.Find("GameObject/GameObject/XinPian_ani/Plane04").GetComponent<Renderer>().material.mainTexture = Resources.Load("Game/" + SpriteNameInfo(color) + "4") as Texture;
            Black.SetActive(true);
            yield return new WaitForSeconds(1f);
            AudioEditer.instance.PlayOneShot("ui_CPU4");
            yield return new WaitForSeconds(2f);
            Black.SetActive(false);
            DestroyImmediate(go);
            for (int i = 0; i < 4; i++)
            {
                ItemList[i].transform.Find("Icon").gameObject.SetActive(true);
            }
            HadFinishID = 0;
            AttributeWindow.SetActive(true);
            AttributeWindowInfo();
        }
    }
    //说明窗口
    void QuestionWindowInfo()
    {
        UIEventListener.Get(QuestionWindow.transform.Find("Question/CloseButton").gameObject).onClick += delegate(GameObject go)
        {
            QuestionWindow.SetActive(false);
        };
    }
    //属性窗口
    void AttributeWindowInfo()
    {
        AttributeNameList.Clear();
        AttributeNameList.Add("全体生命: ");
        AttributeNameList.Add("全体攻击: ");
        AttributeNameList.Add("全体防御: ");
        AttributeNameList.Add("全体生命: ");
        AttributeNameList.Add("全体攻击: ");
        AttributeNameList.Add("全体防御: ");
        AttributeNameList.Add("全体额外伤害加成: ");
        AttributeNameList.Add("全体额外伤害免疫: ");
        AttributeValueList.Clear();
        AttributeWindow.transform.Find("WF_ui_SPChengGong/Attributelist").gameObject.SetActive(false);
        AttributeWindow.transform.Find("WF_ui_SPChengGong/AttributeInfo").gameObject.SetActive(false);
        AttributeWindow.transform.Find("WF_ui_SPChengGong/ClickInfo").gameObject.SetActive(false);
        //AttributeWindow.transform.Find("Black").gameObject.SetActive(false);
        for (int i = 0; i < AttributeNameList.Count; i++)
        {
            AttributeValueList.Add(0);
        }
        UIEventListener.Get(AttributeWindow.transform.Find("Black").gameObject).onClick += delegate(GameObject go)
        {

            AttributeWindow.SetActive(false);
            Destroy(AttributeWindowEffectClone);
            for (int i = 0; i < AttributeItemList.Count; i++)
            {
                DestroyImmediate(AttributeItemList[i].GetComponent<TweenPosition>());
            }
            if (isOpenFullEffect)
            {
                if (ChipItemColor < 6)
                {
                    ChipItemColor = ChipItemColor + 1;
                    ChipItemID = 0;
                    ItemListInfo(ChipItemColor, ChipItemID);
                }
                isOpenFullEffect = false;
            }
        };
        StartCoroutine(InfoEffect());
    }
    IEnumerator InfoEffect()
    {

        AttributeWindow.transform.Find("Black").gameObject.SetActive(true);
        AttributeWindowEffectClone = Instantiate(AttributeWindowEffect) as GameObject;
        AttributeWindowEffectClone.transform.parent = AttributeWindow.transform.Find("WF_ui_SPChengGong").transform;
        AttributeWindowEffectClone.transform.localPosition = Vector3.zero;
        AttributeWindowEffectClone.transform.localScale = new Vector3(400, 400, 400);
        AttributeWindowEffectClone.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        Chip item;
        if (ChipItemID == 0 && ChipItemColor > 1)
        {
            item = TextTranslator.instance.GetChipListByID(ChipItemColor, 1);
        }
        else
        {
            item = TextTranslator.instance.GetChipListByID(ChipItemColor, ChipItemID);
        }
        if (item != null)
        {
            for (int i = 1; i < item.ChipID + 1; i++)
            {
                Chip iteminfo = TextTranslator.instance.GetChipListByChipID(i);
                AttributeInfo(iteminfo.EffectType1, iteminfo.EffectVal1);
                AttributeInfo(iteminfo.EffectType2, iteminfo.EffectVal2);
                AttributeInfo(iteminfo.EffectType3, iteminfo.EffectVal3);
            }
            for (int i = 0; i < AttributeItemList.Count; i++)
            {
                AttributeItemList[i].AddComponent<TweenPosition>();
                AttributeItemList[i].transform.localPosition = new Vector3(-1178, 100 - 40 * (i / 2), 0);
                AttributeItemList[i].GetComponent<TweenPosition>().from = new Vector3(-1178, 100 - 40 * (i / 2), 0);
                if (i % 2 == 1)
                {
                    AttributeItemList[i].GetComponent<TweenPosition>().to = new Vector3(-478, 100 - 40 * (i / 2), 0);
                }
                else
                {
                    AttributeItemList[i].GetComponent<TweenPosition>().to = new Vector3(-0, 100 - 40 * (i / 2), 0);
                }
                AttributeItemList[i].GetComponent<TweenPosition>().duration = 0.5f;
                if (AttributeValueList[i] != 0)
                {
                    AttributeItemList[i].SetActive(true);
                    AttributeItemList[i].GetComponent<UILabel>().text = AttributeNameList[i];
                    AttributeItemList[i].transform.Find("AttributeValue").GetComponent<UILabel>().text = "+";
                }
                else
                {
                    AttributeItemList[i].SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < AttributeItemList.Count; i++)
            {
                AttributeItemList[i].SetActive(false);
            }
        }
        AttributeWindow.transform.Find("WF_ui_SPChengGong/Attributelist").gameObject.SetActive(true);
        AttributeWindow.transform.Find("WF_ui_SPChengGong/AttributeInfo").gameObject.SetActive(true);
        AttributeWindow.transform.Find("WF_ui_SPChengGong/ClickInfo").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        if (ChipItemID == 0 && ChipItemColor == 1)
        {

        }
        else
        {
            for (int i = 0; i < AttributeItemList.Count; i++)
            {
                AttributeItemList[i].GetComponent<TweenPosition>().enabled = false;
            }
            for (int i = 0; i < AttributeItemList.Count; i++)
            {
                if (AttributeValueList[i] != 0)
                {
                    StartCoroutine(AddNumber(i, AttributeValueList[i]));
                }
            }
        }
    }
    IEnumerator AddNumber(int i, float MaxNumber)
    {
        float Number = 0;
        while (Number < MaxNumber)
        {
            yield return new WaitForSeconds(0.1f);
            Number += AttributeValueList[i] / 10f;
            if (Number >= MaxNumber)
            {
                Number = MaxNumber;
            }
            if (MaxNumber > 10)
            {
                AttributeItemList[i].transform.Find("AttributeValue").GetComponent<UILabel>().text = "  +" + Number;
            }
            else
            {
                AttributeItemList[i].transform.Find("AttributeValue").GetComponent<UILabel>().text = "  +" + Number * 100 + "%";
            }
        }
    }
    void AttributeInfo(int Type, float AddInfo)
    {
        switch (Type)
        {
            case 1:
                AttributeValueList[0] += AddInfo;
                break;
            case 2:
                AttributeValueList[1] += AddInfo;
                break;
            case 3:
                AttributeValueList[2] += AddInfo;
                break;
            case 4:
                AttributeValueList[3] += AddInfo;
                break;
            case 5:
                AttributeValueList[4] += AddInfo;
                break;
            case 6:
                AttributeValueList[5] += AddInfo;
                break;
            case 7:
                AttributeValueList[6] += AddInfo;
                break;
            case 8:
                AttributeValueList[7] += AddInfo;
                break;
            default:
                break;
        }
    }
}
