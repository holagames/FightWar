using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GrabHeroItem : MonoBehaviour
{
    public GameObject OneOneButton;
    public GameObject FiveButton;
    public GameObject HeroName;
    public GameObject Level;
    public GameObject HeroItem;
    public GameObject Grid;
    public GameObject PopProtectStatusObj;
    private List<GameObject> Item = new List<GameObject>();
    private int RoleId = 0;
    public GameObject HighProgbability;
    public GameObject LowProbability;
    public GameObject GeneralProgbability;
    public GameObject SuperiorProgbability;
    public bool isRole = false;
    // Use this for initialization
    void Start()
    {

        UIEventListener.Get(OneOneButton).onClick += delegate(GameObject obj)
        {
            if (GameObject.Find("GrabItemWindow").GetComponent<GrabWindow>().AllProtectTime > 0)
            {
                EventPopStatus(1);
            }
            else if (CharacterRecorder.instance.sprite < 2)
            {
                UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
                NetworkHandler.instance.SendProcess("5012#10702;");
            }
            else
            {

                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                PictureCreater.instance.GrabID = GameObject.Find("GoodsItemObj").GetComponent<GoodsItemWindow>().Id;
                PictureCreater.instance.PVPRank = RoleId;
                CharacterRecorder.instance.OnceSuceessID = GameObject.Find("GoodsItemObj").GetComponent<GoodsItemWindow>().Id / 10 * 10 - 30000;
                Debug.LogError("sss       " + CharacterRecorder.instance.OnceSuceessID);
                NetworkHandler.instance.SendProcess("1402#" + RoleId + ";");
            }
        };

    }
    public void SetInfo(Getgemlist Getgem)
    {
        for (int i = 0; i < Item.Count; i++)
        {
            DestroyImmediate(Item[i]);
        }
        Item.Clear();
        RoleId = Getgem.Id;
        if (!Getgem.IsRole)
        {
            isRole = false;
            HeroName.GetComponent<UILabel>().text = "[-][E600ff]" + Getgem.Name + "[-]";
        }
        else
        {
            isRole = true;
            HeroName.GetComponent<UILabel>().text = "[-][FE8742]" + Getgem.Name + "[-]";
        }
        Level.GetComponent<UILabel>().text = "Lv." + Getgem.level;
        if (Getgem.IsRole)
        {
            switch (TextTranslator.instance.GetItemByItemCode(GameObject.Find("GoodsItemObj").GetComponent<GoodsItemWindow>().Id).itemGrade)
            {
                case 3:
                    HighProgbability.SetActive(true);
                    LowProbability.SetActive(false);
                    GeneralProgbability.SetActive(false);
                    SuperiorProgbability.SetActive(false);
                    break;
                case 4:
                    HighProgbability.SetActive(true);
                    LowProbability.SetActive(false);
                    GeneralProgbability.SetActive(false);
                    SuperiorProgbability.SetActive(false);
                    break;
                case 5:
                    HighProgbability.SetActive(true);
                    LowProbability.SetActive(false);
                    GeneralProgbability.SetActive(false);
                    SuperiorProgbability.SetActive(false);
                    break;
                case 6:
                    SuperiorProgbability.SetActive(true);
                    LowProbability.SetActive(false);
                    GeneralProgbability.SetActive(false);
                    HighProgbability.SetActive(false);
                    break;
            }


        }
        else
        {
            switch (TextTranslator.instance.GetItemByItemCode(GameObject.Find("GoodsItemObj").GetComponent<GoodsItemWindow>().Id).itemGrade)
            {

                case 3:
                    HighProgbability.SetActive(true);
                    LowProbability.SetActive(false);
                    GeneralProgbability.SetActive(false);
                    SuperiorProgbability.SetActive(false);
                    break;
                case 4:
                    GeneralProgbability.SetActive(true);
                    LowProbability.SetActive(false);
                    SuperiorProgbability.SetActive(false);
                    HighProgbability.SetActive(false);

                    break;
                case 5:
                    LowProbability.SetActive(true);
                    GeneralProgbability.SetActive(false);
                    SuperiorProgbability.SetActive(false);
                    HighProgbability.SetActive(false);
                    break;
                case 6:
                    LowProbability.SetActive(true);
                    GeneralProgbability.SetActive(false);
                    SuperiorProgbability.SetActive(false);
                    HighProgbability.SetActive(false);
                    break;
            }
        }
        for (int i = 0; i < Getgem.RoleId.Count; i++)
        {
            GameObject obj = Instantiate(HeroItem) as GameObject;
            obj.transform.parent = Grid.transform;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            obj.transform.Find("Icon").GetComponent<UISprite>().spriteName = Getgem.RoleId[i].ToString();
            obj.transform.Find("Bg").GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(Getgem.RoleId[i]).itemGrade;
            obj.SetActive(true);
            Item.Add(obj);
        }
        Grid.GetComponent<UIGrid>().repositionNow = true;
        if (Getgem.IsRole)
        {
            OneOneButton.SetActive(true);
            FiveButton.SetActive(false);

        }
        else
        {
            OneOneButton.SetActive(true);
            FiveButton.SetActive(true);
            UIEventListener.Get(FiveButton).onClick += delegate(GameObject obj)
            {
                if (CharacterRecorder.instance.level >= 25 || CharacterRecorder.instance.Vip >= 2)
                {
                    if (GameObject.Find("GrabItemWindow").GetComponent<GrabWindow>().AllProtectTime > 0)
                    {
                        EventPopStatus(10);
                    }
                    else if (CharacterRecorder.instance.sprite < 10)
                    {
                        UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
                        NetworkHandler.instance.SendProcess("5012#10702;");
                    }
                    else
                    {
                        if (CharacterRecorder.instance.isFiveButtonOnce == false)
                        {
                            NetworkHandler.instance.SendProcess("1403#" + GameObject.Find("GoodsItemObj").GetComponent<GoodsItemWindow>().Id + ";" + RoleId + ";" + 0 + ";");
                        }
                    }
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("战队等级大于25级或者VIP2开启五次夺宝", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }

    }
    public void EventPopStatus(int Number)
    {
        if (isRole)
        {
            PopProtectStatusObj.SetActive(true);
            ProtectStatusPopEvent(Number);
        }
        else
        {
            if (Number == 1)
            {
                if (CharacterRecorder.instance.sprite < 2)
                {
                    UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
                    NetworkHandler.instance.SendProcess("5012#10702;");
                }
                else
                {
                    PictureCreater.instance.GrabID = GameObject.Find("GoodsItemObj").GetComponent<GoodsItemWindow>().Id;
                    PictureCreater.instance.PVPRank = RoleId;
                    NetworkHandler.instance.SendProcess("1402#" + RoleId + ";");
                }
            }
            else
            {
                if (CharacterRecorder.instance.sprite < 10)
                {
                    UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
                    NetworkHandler.instance.SendProcess("5012#10702;");
                }
                else
                {
                    if (CharacterRecorder.instance.isFiveButtonOnce == false)
                    {
                        NetworkHandler.instance.SendProcess("1403#" + GameObject.Find("GoodsItemObj").GetComponent<GoodsItemWindow>().Id + ";" + RoleId + ";" + 0 + ";");
                    }
                }
            }
        }

    }

    //免战状态是否取消
    public void ProtectStatusPopEvent(int Number)
    {
        //退出按钮
        if (UIEventListener.Get(PopProtectStatusObj.transform.FindChild("EscButton").gameObject).onClick == null)
        {
            UIEventListener.Get(PopProtectStatusObj.transform.FindChild("EscButton").gameObject).onClick += delegate(GameObject obj)
            {
                PopProtectStatusObj.SetActive(false);
            };
        }
        //取消按钮
        if (UIEventListener.Get(PopProtectStatusObj.transform.FindChild("CancelButton").gameObject).onClick == null)
        {
            UIEventListener.Get(PopProtectStatusObj.transform.FindChild("CancelButton").gameObject).onClick += delegate(GameObject obj)
            {
                PopProtectStatusObj.SetActive(false);
            };
        }
        //确认按钮
        if (UIEventListener.Get(PopProtectStatusObj.transform.FindChild("DetermineButton").gameObject).onClick == null)
        {
            UIEventListener.Get(PopProtectStatusObj.transform.FindChild("DetermineButton").gameObject).onClick += delegate(GameObject obj)
            {
                GameObject.Find("GrabItemWindow").GetComponent<GrabWindow>().SetProtectTimeInfo(0);
                if (Number == 1)
                {
                    PictureCreater.instance.GrabID = GameObject.Find("GoodsItemObj").GetComponent<GoodsItemWindow>().Id;
                    PictureCreater.instance.PVPRank = RoleId;
                    NetworkHandler.instance.SendProcess("1402#" + RoleId + ";");
                }
                else
                {
                    if (CharacterRecorder.instance.isFiveButtonOnce == false)
                    {
                        NetworkHandler.instance.SendProcess("1403#" + GameObject.Find("GoodsItemObj").GetComponent<GoodsItemWindow>().Id + ";" + RoleId + ";" + 0 + ";");
                    }
                }
                PopProtectStatusObj.SetActive(false);
            };
        }
    }
}
