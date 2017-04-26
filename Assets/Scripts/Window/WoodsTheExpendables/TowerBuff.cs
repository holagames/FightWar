using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerBuff : MonoBehaviour
{
    public GameObject BuffWindow;
    private int CurFloor = 0;
    public List<GameObject> BuffList = new List<GameObject>();
    public List<GameObject> GouMaiList = new List<GameObject>();
    //当前点击的Buff是第几个
    private int CurBuffNumber = 0;
    //是否可以提示
    public bool isOpenMessage = false;
    // Use this for initialization
    private bool oneBuff = false;
    private bool secondBuff = false;
    private bool thirdBuff = false;
    public int NowStar = 0;

    private List<int> NowBuffId = new List<int>();
    void Start()
    {
        if (UIEventListener.Get(BuffWindow.transform.Find("EscButton").gameObject).onClick == null)
        {

            UIEventListener.Get(BuffWindow.transform.Find("EscButton").gameObject).onClick += delegate(GameObject obj)
            {
                bool IsOpen = false;
                foreach (var g in GouMaiList)
                {
                    if (!g.activeSelf)
                    {
                        if (isOpenMessage == false)
                        {
                            UIManager.instance.OpenPromptWindow("您有未购买的BUFF，是否要退出？", PromptWindow.PromptType.Confirm, EscOnClick, null);
                            IsOpen = true;
                        }
                    }
                }
                if (!IsOpen)
                {

                    EscOnClick();
                }

                //if (GameObject.Find("WoodsTheExpendables") != null)
                //{
                //    GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().UpdateWindow(WoodsTheExpendablesWindowType.Right);
                //}
            };
        }
        CurFloor = (GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().CurFloor + 1);

        if (BuffWindow.transform.Find("DownBg") != null)
        {
            BuffWindow.transform.Find("DownBg").transform.Find("LabelNumber").transform.Find("Label").GetComponent<UILabel>().text = GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().CurStar.ToString();
        }


        if (UIEventListener.Get(BuffWindow.transform.Find("One").gameObject).onClick == null)
        {
            UIEventListener.Get(BuffWindow.transform.Find("One").gameObject).onClick += delegate(GameObject obj)
            {
                //NetworkHandler.instance.SendProcess("1505#" + (CurFloor) + ";" + 1 + ";");
                CurBuffNumber = 1;
            };
        }
        if (UIEventListener.Get(BuffWindow.transform.Find("Two").gameObject).onClick == null)
        {

            UIEventListener.Get(BuffWindow.transform.Find("Two").gameObject).onClick += delegate(GameObject obj)
            {
                //NetworkHandler.instance.SendProcess("1505#" + (CurFloor) + ";" + 2 + ";");
                CurBuffNumber = 2;

            };
        }
        if (UIEventListener.Get(BuffWindow.transform.Find("Three").gameObject).onClick == null)
        {

            UIEventListener.Get(BuffWindow.transform.Find("Three").gameObject).onClick += delegate(GameObject obj)
            {
                //NetworkHandler.instance.SendProcess("1505#" + (CurFloor) + ";" + 3 + ";");
                CurBuffNumber = 3;
            };
        }


        if (UIEventListener.Get(GameObject.Find("Buff1Button")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("Buff1Button")).onClick += delegate(GameObject obj)
            {
                NetworkHandler.instance.SendProcess("1505#" + (CurFloor) + ";" + 1 + ";");
                CurBuffNumber = 1;
            };
        }
        if (UIEventListener.Get(GameObject.Find("Buff2Button")).onClick == null)
        {

            UIEventListener.Get(GameObject.Find("Buff2Button")).onClick += delegate(GameObject obj)
            {
                NetworkHandler.instance.SendProcess("1505#" + (CurFloor) + ";" + 2 + ";");
                CurBuffNumber = 2;

            };
        }
        if (UIEventListener.Get(GameObject.Find("Buff3Button")).onClick == null)
        {

            UIEventListener.Get(GameObject.Find("Buff3Button")).onClick += delegate(GameObject obj)
            {
                NetworkHandler.instance.SendProcess("1505#" + (CurFloor) + ";" + 3 + ";");
                CurBuffNumber = 3;
            };
        }
        NowStar = int.Parse(GameObject.Find("DownBg").transform.Find("LabelNumber").transform.Find("Label").GetComponent<UILabel>().text);
        UpdateMyBuffLabel();
    }

    public void SetInfo(string RecvString)
    {
        string[] dataSplit = RecvString.Split('!');
        NowBuffId.Clear();
        for (int i = 0; i < dataSplit.Length - 1; i++)
        {
            string[] secSplit = dataSplit[i].Split('$');
            BuffList[i].transform.Find("Name").GetComponent<UILabel>().text = BuffTypeName(int.Parse(secSplit[0]));
            BuffList[i].transform.Find("Value").GetComponent<UILabel>().text = BuffValueName(int.Parse(secSplit[0]), float.Parse(secSplit[1]));
            BuffList[i].transform.Find("Icon").GetComponent<UITexture>().mainTexture = Resources.Load("Game/" + BuffTypeIcon(int.Parse(secSplit[0]))) as Texture;
            BuffList[i].transform.Find("Info").GetComponent<UILabel>().text = BuffTypeInfo(int.Parse(secSplit[0])) + BuffValueName(int.Parse(secSplit[0]), float.Parse(secSplit[1]));

            NowBuffId.Add(int.Parse(secSplit[0]));
            if (int.Parse(secSplit[2]) == 1)
            {
                GouMaiList[i].SetActive(false);
            }
            else
            {
                GouMaiList[i].SetActive(true);
                if (i == 0)
                {
                    GameObject.Find("Buff1Button").GetComponent<UISprite>().spriteName = "button1_an";
                }
                else if (i == 1)
                {
                    GameObject.Find("Buff2Button").GetComponent<UISprite>().spriteName = "button1_an";
                }
                else if (i == 2)
                {
                    GameObject.Find("Buff3Button").GetComponent<UISprite>().spriteName = "button1_an";
                }
            }
        }
        CurFloor = (GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().CurFloor + 1);

        BuffList[0].transform.Find("StarNumber").GetComponent<UILabel>().text = TextTranslator.instance.GetTowerByID(CurFloor).GateStarCost1.ToString();
        BuffList[1].transform.Find("StarNumber").GetComponent<UILabel>().text = TextTranslator.instance.GetTowerByID(CurFloor).GateStarCost2.ToString();
        BuffList[2].transform.Find("StarNumber").GetComponent<UILabel>().text = TextTranslator.instance.GetTowerByID(CurFloor).GateStarCost3.ToString();

        if (PlayerPrefs.GetInt("Automaticbrushfield" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID")) == 1) //自动点击buff
        {
            for (int i = 0; i < CharacterRecorder.instance.AutomaticBuffList.Count; i++) 
            {
                for (int j = 0; j < NowBuffId.Count; j++) 
                {
                    if (CharacterRecorder.instance.AutomaticBuffList[i] == NowBuffId[j]) 
                    {
                        if (j == 0) 
                        {
                            NetworkHandler.instance.SendProcess("1505#" + (CurFloor) + ";" + 1 + ";");
                            CurBuffNumber = 1;
                        }
                        else if (j == 1) 
                        {
                            NetworkHandler.instance.SendProcess("1505#" + (CurFloor) + ";" + 2 + ";");
                            CurBuffNumber = 2;
                        }
                        else if (j == 2) 
                        {
                            NetworkHandler.instance.SendProcess("1505#" + (CurFloor) + ";" + 3 + ";");
                            CurBuffNumber = 3;
                        }
                    }
                }
            }

            StartCoroutine(GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().AutomaticbrushOnEveryWindow(WoodsTheExpendablesWindowType.BuffWindow, SomeConditionCloseWindow.Nothing));
        }

    }
    public void SetInfoUpate(string RecvString)
    {
        if (CurBuffNumber > 0)
        {

            GouMaiList[CurBuffNumber - 1].SetActive(true);

            if (CurBuffNumber == 1)
            {
                GameObject.Find("Buff1Button").GetComponent<UISprite>().spriteName = "button1_an";
                oneBuff = true;
            }
            else if (CurBuffNumber == 2)
            {
                GameObject.Find("Buff2Button").GetComponent<UISprite>().spriteName = "button1_an";
                secondBuff = true;
            }
            else if (CurBuffNumber == 3)
            {
                GameObject.Find("Buff3Button").GetComponent<UISprite>().spriteName = "button1_an";
                thirdBuff = true;
            }
            UpdateMyBuffLabel();
        }
    }
    private void UpdateMyBuffLabel()
    {
        if (GameObject.Find("WoodsTheExpendables") != null)
        {
            if (GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().BuffList.Count != 0)
            {
                BuffWindow.transform.Find("DownBg").transform.Find("Label").GetComponent<UILabel>().text = "";
                List<BuffData> BuffList = GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().BuffList;
                for (int i = 0; i < BuffList.Count; i++)
                {
                    if (int.Parse(BuffList[i].Type) != 7 && int.Parse(BuffList[i].Type) != 8)
                    {
                        BuffWindow.transform.Find("DownBg").transform.Find("Label").GetComponent<UILabel>().text += BuffTypeName(int.Parse(BuffList[i].Type)) + "   " + BuffValueName(int.Parse(BuffList[i].Type), BuffList[i].Value) + "   ";
                    }
                }
            }
        }
        GanBuyBuffMessage();

    }
    public void GanBuyBuffMessage()
    {

        if (oneBuff || (oneBuff && thirdBuff) || thirdBuff)
        {
            if (NowStar >= int.Parse(BuffList[1].transform.Find("StarNumber").GetComponent<UILabel>().text) && secondBuff == false)
            {
                isOpenMessage = false;
            }
            else
            {
                isOpenMessage = true;
            }
        }
        if (secondBuff || (oneBuff && secondBuff))
        {
            if (NowStar >= int.Parse(BuffList[2].transform.Find("StarNumber").GetComponent<UILabel>().text) && thirdBuff == false)
            {
                isOpenMessage = false;
            }
            else
            {
                isOpenMessage = true;
            }
        }
        if (secondBuff && thirdBuff)
        {
            if (NowStar >= int.Parse(BuffList[0].transform.Find("StarNumber").GetComponent<UILabel>().text) && thirdBuff == false)
            {
                isOpenMessage = false;
            }
            else
            {
                isOpenMessage = true;
            }
        }
        if (oneBuff && secondBuff && thirdBuff)
        {
            isOpenMessage = true;
        }
    }
    /// <summary>
    /// 返回buff名字
    /// </summary>
    /// <param name="Number"></param>
    /// <returns></returns>
    private string BuffTypeName(int Number)
    {
        string name = "";
        switch (Number)
        {
            case 1:
                name = "减伤";
                break;
            case 2:
                name = "暴击";
                break;
            case 3:
                name = "伤害";
                break;
            case 4:
                name = "抗暴";
                break;
            case 5:
                name = "闪避";
                break;
            case 6:
                name = "命中";
                break;
            case 7:
                name = "强攻";
                break;
            case 8:
                name = "威吓";
                break;
        }
        return name;
    }

    private string BuffValueName(int Number, float Value)
    {

        string name = "";
        switch (Number)
        {
            case 1:
                name = (Value * 100).ToString() + "%";
                break;
            case 2:
                name = (Value * 100).ToString() + "%";
                break;
            case 3:
                name = (Value * 100).ToString() + "%";
                break;
            case 4:
                name = (Value * 100).ToString() + "%";
                break;
            case 5:
                name = (Value * 100).ToString() + "%";
                break;
            case 6:
                name = (Value * 100).ToString() + "%";
                break;
            case 7:
                name = "";
                break;
            case 8:
                name = "";
                break;
        }
        return name;
    }
    /// <summary>
    /// 返回buff图标
    /// </summary>
    /// <param name="Number"></param>
    /// <returns></returns>
    private string BuffTypeIcon(int Number)
    {
        string Icon = "";
        switch (Number)
        {
            case 1:
                Icon = "buff2";
                break;
            case 2:
                Icon = "buff3";
                break;
            case 3:
                Icon = "buff1";
                break;
            case 4:
                Icon = "buff10";
                break;
            case 5:
                Icon = "buff11";
                break;
            case 6:
                Icon = "buff12";
                break;
            case 7:
                Icon = "buff8";
                break;
            case 8:
                Icon = "buff9";
                break;
        }
        return Icon;
    }
    /// <summary>
    /// 返回buff说明
    /// </summary>
    /// <param name="Number"></param>
    /// <returns></returns>
    private string BuffTypeInfo(int Number)
    {
        string Info = "";
        switch (Number)
        {
            case 1:
                Info = "全体己方减伤增加";
                break;
            case 2:
                Info = "全体己方暴击增加";
                break;
            case 3:
                Info = "全体己方伤害增加";
                break;
            case 4:
                Info = "全体己方抗暴增加";
                break;
            case 5:
                Info = "全体己方闪避增强";
                break;
            case 6:
                Info = "全体己方提高命中";
                break;
            case 7:
                Info = "直接通关下一关，评分按照最高难度计算";
                break;
            case 8:
                Info = "敌方随机逃跑英雄2个";
                break;
        }
        return Info;
    }
    public void EscOnClick()
    {
        if (GameObject.Find("WoodsTheExpendables") != null)
        {
            GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().UpdateWindow(WoodsTheExpendablesWindowType.Right);
        }
        NetworkHandler.instance.SendProcess("1505#" + 99 + ";" + 0 + ";");
        NetworkHandler.instance.SendProcess("1501#;");
    }
}
