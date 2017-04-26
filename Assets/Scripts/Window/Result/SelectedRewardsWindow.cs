using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectedRewardsWindow : MonoBehaviour
{

    public UILabel TimeLabel;
    public int Time;
    public GameObject Costdiamond;
    // public GameObject RewardItem;
    public GameObject SureButton;
    public GameObject GetButton;
    // public UIGrid RewardGrid;
    public List<GameObject> AwardList = new List<GameObject>();
    public List<int> RandomList = new List<int>() { 0, 1, 2, 3, 4 };
    public Dictionary<int, int> RandomDic = new Dictionary<int, int>();
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;
    public bool isStart = false;
    public int OpenNumber = 0;
    public int NeedDiamond = 0;
    public int StartTime = 15;
    public int OpenTime = 15;
    public int randomFirID = 0;
    public int randomSconID = 0;
    public List<Item> _itemList = new List<Item>();
    // Use this for initialization
    void Start()
    {
        UIEventListener.Get(SureButton).onClick += delegate(GameObject go)
        {
            if (isStart == false)
            {
                StartClick();
            }
            else
            {
                if (OpenNumber >= 2)
                {
                    DestroyImmediate(this.gameObject);
                    PictureCreater.instance.StopFight(true);
                    //for (int i = 0; i < 2 * (CharacterRecorder.instance.isReStartTimes + 1); i++)
                    //for (int i = 0; i < (CharacterRecorder.instance.isReStartTimes + 1); i++)
                    //{
                       
                    //}
                    UIManager.instance.BackUI();
                    CharacterRecorder.instance.isReStartTimes = 0;
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("请翻两张牌", PromptWindow.PromptType.Hint, null, null);
                }
            }
        };
        UIEventListener.Get(GetButton).onClick += delegate(GameObject go)
        {

            if (CharacterRecorder.instance.lunaGem >= 100)
            {
                OpenNumber = 5;
                NetworkHandler.instance.SendProcess("2104#1;");
                for (int i = 0; i < 5; i++)
                {
                    AwardList[i].transform.Find("RewardClose").gameObject.SetActive(false);
                }
                CancelInvoke("UpdateProtectTime");
                TimeLabel.GetComponent<UILabel>().text = string.Format("{0}秒", "00");
                GetButton.SetActive(false);
                isStart = true;
                SureButton.transform.Find("Label").GetComponent<UILabel>().text = "结束抽奖";
                UIManager.instance.OpenSinglePanel("AdvanceWindow", false);
                GameObject.Find("AdvanceWindow").GetComponent<AdvanceWindow>().SetInfo(AdvanceWindowType.GainResult, null, null, null, null, _itemList);
                //StartCoroutine(SetRotation(5));
            }
            else
            {
                UIManager.instance.OpenPromptWindow("钻石不足", PromptWindow.PromptType.Hint, null, null);
            }
        };
        SureButton.transform.Find("Label").GetComponent<UILabel>().text = "开始抽奖";
        OpenNeedDiamond(0);
        UpdateTime(OpenTime);
        GetButton.SetActive(true);
    }

    void StartClick()
    {
        isStart = true;
        for (int i = 0; i < 5; i++)
        {
            AwardList[i].transform.Find("RewardClose").gameObject.SetActive(true);
            OpenItemClick(i);
            if (i != 2)
            {
                AwardList[i].GetComponent<TweenPosition>().PlayForward();
            }
        }
        GetButton.SetActive(false);
        SureButton.transform.Find("Label").GetComponent<UILabel>().text = "结束抽奖";
    }
    IEnumerator SetRotation(int CardID)
    {
        if (CardID != 5)
        {
            AwardList[CardID].AddComponent("TweenRotation");
            TweenRotation awarditem = AwardList[CardID].GetComponent<TweenRotation>();
            awarditem.from = new Vector3(0, -180, 0);
            awarditem.to = Vector3.zero;
            awarditem.duration = 0.3f;
            awarditem.ResetToBeginning();
            awarditem.PlayForward();
            yield return new WaitForSeconds(0.15f);
            awarditem.transform.Find("RewardClose").gameObject.SetActive(false);
            yield return new WaitForSeconds(0.15f);
            AwardList[CardID].transform.Find("WF_ui_FanPai").gameObject.SetActive(true);
        }
    }
    public void SelectInfo(List<Item> Award)
    {
        //for (int i = 0; i < 5; i++)
        //{
        //AwardList[i].SetActive(true);
        //Setitem(AwardList[i], Award[i]);
        //}
        //生成随机数组
        _itemList = Award;
        int temp = 0;
        int length = RandomList.Count;
        for (int i = 0; i < length; i++)
        {
            int num = Random.Range(0, length);
            temp = RandomList[length - 1];
            RandomList[length - 1] = RandomList[num];
            RandomList[num] = temp;
            length -= 1;
        }
        for (int i = 0; i < 5; i++)
        {
            AwardList[i].SetActive(true);
            Setitem(AwardList[i], Award[RandomList[i]]);
        }
    }

    public void OpenRewardItem(int ID, List<Item> Award, int num)
    {
        //int changeID = 0;
        //for (int i = 0; i < RandomList.Count; i++)
        //{
        //    if (RandomList[i] == ID)
        //    {
        //        changeID = i;
        //    }
        //}
        Setitem(AwardList[ID-1], Award[0]);
        OpenNumber = num;
        if (OpenNumber == 1)
        {
            randomFirID = ID-1;
        }
        else if (OpenNumber >= 2)
        {
            CancelInvoke("UpdateProtectTime");
            TimeLabel.GetComponent<UILabel>().text = string.Format("{0}秒", "00");
        }
        StartCoroutine(SetRotation(ID-1));
        //AwardList[ID - 1].transform.Find("RewardClose").gameObject.SetActive(false);
        GetButton.SetActive(false);
        OpenNeedDiamond(num);
    }
    void Setitem(GameObject go, Item item)
    {
        go.transform.FindChild("Fragment").gameObject.SetActive(false);
        if (item.itemCode.ToString()[0] == '4')
        {
            go.transform.FindChild("item").GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.FindChild("item").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(item.itemCode - 10000).picID.ToString();
        }
        else if (item.itemCode.ToString()[0] == '7' && item.itemCode.ToString()[4] != '0')
        {
            go.transform.FindChild("item").GetComponent<UISprite>().atlas = RoleAtlas;
            go.transform.FindChild("item").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(item.itemCode - 10000).picID.ToString();
            go.transform.FindChild("Fragment").gameObject.SetActive(true);
        }
        else if (item.itemCode.ToString()[0] == '8')
        {
            go.transform.FindChild("item").GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.FindChild("item").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode((item.itemCode / 10) * 10 - 30000).picID.ToString();
            go.transform.FindChild("Fragment").gameObject.SetActive(true);
        }
        else if (item.itemCode.ToString()[0] == '6')
        {
            go.transform.FindChild("item").GetComponent<UISprite>().atlas = RoleAtlas;
            go.transform.FindChild("item").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(item.itemCode - 10000).picID.ToString();
        }
        else
        {
            go.transform.FindChild("item").GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.FindChild("item").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(item.itemCode).picID.ToString();
        }
        go.transform.FindChild("ItemGrade").GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(item.itemCode).itemGrade.ToString();
        go.transform.FindChild("itemnumber").GetComponent<UILabel>().text = item.itemCount.ToString();
        TextTranslator.instance.ItemDescription(go.transform.FindChild("item").gameObject, item.itemCode, item.itemCount); //kino

    }

    void OpenItemClick(int ID)
    {
        //int num = RandomList[ID]; 
        switch (ID)
        {
            case 0:
                UIEventListener.Get(AwardList[0].transform.Find("RewardClose").gameObject).onClick += delegate(GameObject go)
                {
                    if (CharacterRecorder.instance.lunaGem >= NeedDiamond)
                    {
                        NetworkHandler.instance.SendProcess("2103#1;1;");
                        AwardList[0].transform.Find("RewardClose").gameObject.GetComponent<BoxCollider>().enabled = false;
                        //StartCoroutine(SetRotation(0));
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("钻石不足", PromptWindow.PromptType.Hint, null, null);
                    }
                };
                break;
            case 1:
                UIEventListener.Get(AwardList[1].transform.Find("RewardClose").gameObject).onClick += delegate(GameObject go)
                {
                    if (CharacterRecorder.instance.lunaGem >= NeedDiamond)
                    {
                        NetworkHandler.instance.SendProcess("2103#1;2;");
                        AwardList[1].transform.Find("RewardClose").gameObject.GetComponent<BoxCollider>().enabled = false;
                        //StartCoroutine(SetRotation(1));
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("钻石不足", PromptWindow.PromptType.Hint, null, null);
                    }
                };
                break;
            case 2:
                UIEventListener.Get(AwardList[2].transform.Find("RewardClose").gameObject).onClick += delegate(GameObject go)
                {
                    if (CharacterRecorder.instance.lunaGem >= NeedDiamond)
                    {
                        NetworkHandler.instance.SendProcess("2103#1;3;");
                        AwardList[2].transform.Find("RewardClose").gameObject.GetComponent<BoxCollider>().enabled = false;
                        //StartCoroutine(SetRotation(2));
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("钻石不足", PromptWindow.PromptType.Hint, null, null);
                    }
                };
                break;
            case 3:
                UIEventListener.Get(AwardList[3].transform.Find("RewardClose").gameObject).onClick += delegate(GameObject go)
                {
                    if (CharacterRecorder.instance.lunaGem >= NeedDiamond)
                    {
                        NetworkHandler.instance.SendProcess("2103#1;4;");
                        AwardList[3].transform.Find("RewardClose").gameObject.GetComponent<BoxCollider>().enabled = false;
                        //StartCoroutine(SetRotation(3));
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("钻石不足", PromptWindow.PromptType.Hint, null, null);
                    }
                };
                break;
            case 4:
                UIEventListener.Get(AwardList[4].transform.Find("RewardClose").gameObject).onClick += delegate(GameObject go)
                {
                    if (CharacterRecorder.instance.lunaGem >= NeedDiamond)
                    {
                        NetworkHandler.instance.SendProcess("2103#1;5;" );
                        AwardList[4].transform.Find("RewardClose").gameObject.GetComponent<BoxCollider>().enabled = false;
                        //StartCoroutine(SetRotation(4));
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("钻石不足", PromptWindow.PromptType.Hint, null, null);
                    }
                };
                break;
        }

    }

    void OpenNeedDiamond(int number)
    {
        if (number >= 2)
        {
            Costdiamond.SetActive(true);
            switch (number)
            {
                case 2:
                    NeedDiamond = 30;
                    break;
                case 3:
                    NeedDiamond = 60;
                    break;
                case 4:
                    NeedDiamond = 60;
                    break;
            }
            Costdiamond.transform.Find("DiamCost").GetComponent<UILabel>().text = NeedDiamond.ToString();
        }
        else
        {
            Costdiamond.SetActive(false);
        }
    }

    void UpdateTime(int timer)
    {
        CancelInvoke("UpdateProtectTime");
        InvokeRepeating("UpdateProtectTime", 0, 1.0f);
    }
    public void UpdateProtectTime()
    {
        string secondStr = "";
        if (isStart == false)
        {
            secondStr = (StartTime).ToString("00");
        }
        else
        {
            secondStr = (OpenTime).ToString("00");
        }
        TimeLabel.GetComponent<UILabel>().text = string.Format("{0}秒", secondStr);
        if (isStart == false)
        {
            StartTime -= 1;
        }
        else
        {
            OpenTime -= 1;
        }

        if (StartTime < 0)
        {
            StartClick();
            StartTime = 15;
            UpdateTime(OpenTime);
        }
        if (OpenTime < 0)
        {
            CancelInvoke("UpdateProtectTime");
            Random.seed = System.Environment.TickCount;
            if (OpenNumber == 1)
            {
                while (randomFirID == randomSconID)
                {
                    randomSconID = Random.Range(0, 5);
                }
                StartCoroutine(SetRotation(randomSconID));
                //AwardList[randomSconID].transform.Find("RewardClose").gameObject.SetActive(false);
                NetworkHandler.instance.SendProcess("2103#1;" + (randomSconID + 1).ToString() + ";");
            }
            else
            {
                while (randomFirID == randomSconID)
                {
                    randomFirID = Random.Range(0, 5);
                    randomSconID = Random.Range(0, 5);
                }
                StartCoroutine(SetRotation(randomFirID));
                //AwardList[randomFirID].transform.Find("RewardClose").gameObject.SetActive(false);
                NetworkHandler.instance.SendProcess("2103#1;" + (randomFirID + 1).ToString() + ";");
                StartCoroutine(SetRotation(randomSconID));
                // AwardList[randomSconID].transform.Find("RewardClose").gameObject.SetActive(false);
                NetworkHandler.instance.SendProcess("2103#1;" + (randomSconID + 1).ToString() + ";");
            }

        }
    }
}
