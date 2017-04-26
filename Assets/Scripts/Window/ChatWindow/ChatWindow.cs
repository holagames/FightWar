using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CodeStage.AntiCheat.ObscuredTypes;
public class ChatWindow : MonoBehaviour
{
    public TweenPosition tweenposition;
    public UIInput input;

    public UIScrollBar chatScrollBar;

    public GameObject SendMessageButton;

    public GameObject uiGrid;
    public GameObject itemPrafeb;

    public GameObject closeButton;
    public GameObject maskButton;
    public GameObject FaceButton;
    public GameObject FaceBoard;
    [SerializeField]
    private List<GameObject> tabButtonList = new List<GameObject>();
    int channel = 1;
    //[SerializeField]
    private List<GameObject> channelItemList = new List<GameObject>();

    public UILabel ChatSysNum;
    public UILabel ChatWldNum;
    public bool IsShowChatRedDian = false;

    private Transform myTrans;
    private float HeightAll = 0;
    //聊天存储数量
    public int XitongNumber = 0;
    public int WorldNumber = 0;
    public int JuntuanNumber = 0;
    public int TeamNumber = 0;
    // Use this for initialization
    void Start()
    {
        TweenPositionToMove();
        if (GameObject.Find("LegionWarWindow") != null) 
        {
            foreach (Transform tran in GetComponentsInChildren<Transform>())
            {
                tran.gameObject.layer = 9;
            }
        }
        UIManager.instance.CountSystem(UIManager.Systems.聊天);
        UIManager.instance.UpdateSystems(UIManager.Systems.聊天);
        this.channel = CharacterRecorder.instance.ButtonChannel;
        myTrans = uiGrid.transform;
        ChangeButtonState(this.channel - 1);

        if (CharacterRecorder.instance.IsWorldTalk == false)
        {
            SetChatInfoShow();
            CharacterRecorder.instance.IsWorldTalk = true;
        }
       
        if (channel == 2)//世界频道
        {
            SetWordChatItemInChannel();
        }
        else 
        {
            SetChatItemInChannel(this.channel);
        }
        ShowTabLock();//锁
        //ShowTabRedPoint();红点
        UIEventListener.Get(SendMessageButton).onClick = ClickSendButton;
        if (UIEventListener.Get(closeButton).onClick == null)
        {
            UIEventListener.Get(closeButton).onClick += delegate(GameObject obj)
            {
                if (GameObject.Find("TeamInvitationWindow") == null && GameObject.Find("LegionWarWindow") == null)
                {
                    MainWindow mw = GameObject.Find("MainWindow").GetComponent<MainWindow>();
                    DestroyImmediate(this.gameObject);
                    //UIManager.instance.BackUI();
                }
                else
                {
                    DestroyImmediate(this.gameObject);
                }
            };
        }
        if (UIEventListener.Get(maskButton).onClick == null)
        {
            UIEventListener.Get(maskButton).onClick += delegate(GameObject obj)
            {
                if (GameObject.Find("TeamInvitationWindow") == null && GameObject.Find("LegionWarWindow") == null)
                {
                    MainWindow mw = GameObject.Find("MainWindow").GetComponent<MainWindow>();
                    DestroyImmediate(this.gameObject);
                    //UIManager.instance.BackUI();
                }
                else
                {
                    DestroyImmediate(this.gameObject);
                }
            };
        }
        for (int i = 0; i < tabButtonList.Count; i++)
        {
            UIEventListener.Get(tabButtonList[i]).onClick = delegate(GameObject OBJ)
            {
                CharacterRecorder cr = CharacterRecorder.instance;
                Debug.Log("切换按钮chanel " + OBJ.name);
                switch (OBJ.name)
                {
                    case "Tab1":
                        if (channel != 1)
                        {
                            channel = 1;
                            CharacterRecorder.instance.ButtonChannel = 1;
                            if (CharacterRecorder.instance.IsXitongTalk == false) {
                                SetChatInfoShow();
                                CharacterRecorder.instance.IsXitongTalk = true;
                            }

                            SetChatItemInChannel(this.channel);
                            tabButtonList[0].transform.FindChild("SpriteRedDian").gameObject.SetActive(false);
                            cr.Tab_Channel1 = 0;
                            SetMainWindowChatColor();
                        }
                        break;
                    case "Tab2":
                        if (channel != 2)
                        {
                            channel = 2;
                            CharacterRecorder.instance.ButtonChannel = 2;
                            if (CharacterRecorder.instance.IsWorldTalk == false)
                            {
                                SetChatInfoShow();
                                CharacterRecorder.instance.IsWorldTalk = true;
                            }
                            //SetChatItemInChannel(this.channel);
                            SetWordChatItemInChannel();
                            tabButtonList[1].transform.FindChild("SpriteRedDian").gameObject.SetActive(false);
                            cr.Tab_Channel2 = 0;
                            SetMainWindowChatColor();
                        }
                        break;
                    case "Tab3":
                        if (CharacterRecorder.instance.legionID > 0)
                        {
                            if (channel != 3)
                            {
                                channel = 3;
                                CharacterRecorder.instance.ButtonChannel = 3;
                                if (CharacterRecorder.instance.IsJunTuanTalk == false)
                                {
                                    SetChatInfoShow();
                                    CharacterRecorder.instance.IsJunTuanTalk = true;
                                }
                                SetChatItemInChannel(this.channel);
                                tabButtonList[2].transform.FindChild("SpriteRedDian").gameObject.SetActive(false);
                                cr.Tab_Channel3 = 0;
                            }
                            SetMainWindowChatColor();
                        }
                        else
                        {
                            UIManager.instance.OpenPromptWindow("加入军团才可以在军团频道聊天", PromptWindow.PromptType.Alert, null, null);
                        }
                        break;
                    case "Tab4":
                        if (CharacterRecorder.instance.legionCountryID > 0)
                        {
                            if (channel != 4)
                            {
                                channel = 4;
                                CharacterRecorder.instance.ButtonChannel = 4;
                                if (CharacterRecorder.instance.IsTeamTalk == false)
                                {
                                    SetChatInfoShow();
                                    CharacterRecorder.instance.IsTeamTalk = true;
                                }
                                SetChatItemInChannel(this.channel);
                                tabButtonList[3].transform.FindChild("SpriteRedDian").gameObject.SetActive(false);
                                cr.Tab_Channel4 = 0;
                                SetMainWindowChatColor();
                            }
                        }
                        else 
                        {
                            UIManager.instance.OpenPromptWindow("加入国家才可以在国家频道聊天", PromptWindow.PromptType.Alert, null, null);
                        }
                        break;
                }
                if (cr.Tab_Channel1 == 0 && cr.Tab_Channel2 == 0 && cr.Tab_Channel3 == 0 && cr.Tab_Channel4 == 0)
                {
                    IsShowChatRedDian = false;
                }
                else
                {
                    IsShowChatRedDian = true;
                }
            };
        }
        FaceRelativeOnClick();
    }

    /// <summary>
    /// 军团和国家聊天锁判断
    /// </summary>
    void ShowTabLock()
    {
        GameObject legionLock = tabButtonList[2].transform.FindChild("Lock").gameObject;
        GameObject CountryLock = tabButtonList[3].transform.FindChild("Lock").gameObject;
        if (CharacterRecorder.instance.legionID > 0)
        {
            legionLock.SetActive(false);
        }
        else
        {
            legionLock.SetActive(true);
            StartCoroutine(DelaySetUIToggleFalse(tabButtonList[2].GetComponent<UIToggle>()));
        }

        if (CharacterRecorder.instance.legionCountryID > 0)
        {
            CountryLock.SetActive(false);
        }
        else 
        {
            CountryLock.SetActive(true);
            StartCoroutine(DelaySetUIToggleFalse(tabButtonList[3].GetComponent<UIToggle>()));
        }
    }

    IEnumerator DelaySetUIToggleFalse(UIToggle _UIToggle)
    {
        yield return new WaitForSeconds(0.5f);
        _UIToggle.enabled = false;
    }


    void ShowTabRedPoint()
    {
        Transform go = transform.Find("Content/SpriteBg/GridTab");
        CharacterRecorder cr = CharacterRecorder.instance;
        if (cr.Tab_Channel1 > 0 && IsShowChatRedDian)
        {
            if (channel == 1)
            {
                cr.Tab_Channel1 = 0;
                return;
            }
            go.transform.FindChild("Tab1/SpriteRedDian").gameObject.SetActive(true);
            if (cr.Tab_Channel1 > 20 && IsShowChatRedDian)
            {
                cr.Tab_Channel1 = 20;
            }
            go.transform.FindChild("Tab1/SpriteRedDian/ChatNum").GetComponent<UILabel>().text = cr.Tab_Channel1.ToString();
        }
        if (cr.Tab_Channel2 > 0 && IsShowChatRedDian)
        {
            if (channel == 2)
            {
                cr.Tab_Channel2 = 0;
                return;
            }
            go.transform.FindChild("Tab2/SpriteRedDian").gameObject.SetActive(true);
            if (cr.Tab_Channel2 > 20 && IsShowChatRedDian)
            {
                cr.Tab_Channel2 = 20;
            }
            go.transform.FindChild("Tab2/SpriteRedDian/ChatNum").GetComponent<UILabel>().text = cr.Tab_Channel2.ToString();
        }
        if (cr.Tab_Channel3 > 0 && IsShowChatRedDian && CharacterRecorder.instance.legionID > 0)
        {
            if (channel == 3)
            {
                cr.Tab_Channel3 = 0;
                return;
            }
            go.transform.FindChild("Tab3/SpriteRedDian").gameObject.SetActive(true);
            if (cr.Tab_Channel3 > 20 && IsShowChatRedDian)
            {
                cr.Tab_Channel3 = 20;
            }
            go.transform.FindChild("Tab3/SpriteRedDian/ChatNum").GetComponent<UILabel>().text = cr.Tab_Channel3.ToString();
        }
        if (cr.Tab_Channel4 > 0 && IsShowChatRedDian)
        {
            if (channel == 4)
            {
                cr.Tab_Channel4 = 0;
                return;
            }
            go.transform.FindChild("Tab4/SpriteRedDian").gameObject.SetActive(true);
            if (cr.Tab_Channel4 > 20 && IsShowChatRedDian)
            {
                cr.Tab_Channel4 = 20;
            }
            go.transform.FindChild("Tab4/SpriteRedDian/ChatNum").GetComponent<UILabel>().text = cr.Tab_Channel4.ToString();
        }
    }
    //表情相关
    void FaceRelativeOnClick()
    {
        UIEventListener.Get(FaceButton).onClick = delegate(GameObject go)
        {
            FaceBoard.SetActive(true);

            if (GameObject.Find("FaceSprite1") != null)
            {
                UIEventListener.Get(GameObject.Find("FaceSprite1")).onClick = delegate(GameObject OBJ)
                {
                    input.value += "{0}";
                    FaceBoard.SetActive(false);
                };
            }
            if (GameObject.Find("FaceSprite2") != null)
            {
                UIEventListener.Get(GameObject.Find("FaceSprite2")).onClick = delegate(GameObject OBJ)
                {
                    input.value += "{1}";
                    FaceBoard.SetActive(false);
                };
            }
            if (GameObject.Find("FaceSprite3") != null)
            {
                UIEventListener.Get(GameObject.Find("FaceSprite3")).onClick = delegate(GameObject OBJ)
                {
                    input.value += "{2}";
                    FaceBoard.SetActive(false);
                };
            }
            if (GameObject.Find("FaceSprite4") != null)
            {
                UIEventListener.Get(GameObject.Find("FaceSprite4")).onClick = delegate(GameObject OBJ)
                {
                    input.value += "{3}";
                    FaceBoard.SetActive(false);
                };
            }
            if (GameObject.Find("FaceSprite5") != null)
            {
                UIEventListener.Get(GameObject.Find("FaceSprite5")).onClick = delegate(GameObject OBJ)
                {
                    input.value += "{4}";
                    FaceBoard.SetActive(false);
                };
            }
        };
    }

    void ChangeButtonState(int num)
    {
        for (int i = 0; i < tabButtonList.Count; i++)
        {
            if (i == num)
            {
                tabButtonList[i].GetComponent<UIToggle>().startsActive = true;
                tabButtonList[i].GetComponent<UIToggle>().value = true;
            }
            else
            {
                tabButtonList[i].GetComponent<UIToggle>().startsActive = false;
                tabButtonList[i].GetComponent<UIToggle>().value = false;
            }
        }
    }


    /// <summary>
    /// 发送信息
    /// </summary>
    /// <param name="obj"></param>
    void ClickSendButton(GameObject obj)
    {
        //string name = ":" + "悦枫是神";
        if (CharacterRecorder.instance.level >= 25)
        {
            string name = CharacterRecorder.instance.characterName;//"悦枫是神lalala";
            string text = input.value;

            if (!string.IsNullOrEmpty(text))
            {
                if (text.IndexOf("@") == 0)
                {
                    NetworkHandler.instance.SendProcess(text.Remove(0, 1));
                    DestroyImmediate(this.gameObject);
                    //UIManager.instance.BackUI();
                }
                else if(text.Length > 30)
                {
                    UIManager.instance.OpenPromptWindow("亲，聊天字数最多30个字哦", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    //CreatChatItem(channel, name, text.Replace("@", ""));
                    input.value = "";
                    chatScrollBar.value = 1;
                    int Sendchannel = channel;//系统发世界
                    switch (channel)
                    {
                        case 1: Sendchannel = 2; break;
                        default: break;
                    }
                    text += " ";
                    NetworkHandler.instance.SendProcess(string.Format("7001#{0};{1};{2}${3}${4}${5}${6}${7};", Sendchannel, text.Replace("@", "＠").Replace("#", "＃").Replace(";", "；"), CharacterRecorder.instance.Vip, CharacterRecorder.instance.headIcon, CharacterRecorder.instance.legionID, CharacterRecorder.instance.NationID, CharacterRecorder.instance.userId,CharacterRecorder.instance.legionCountryID));
                }
            }
            else
            {
                UIManager.instance.OpenPromptWindow("真的无话可说了吗？", PromptWindow.PromptType.Hint, null, null);
            }
        }
        else
        {
            UIManager.instance.OpenPromptWindow("等级要大于25", PromptWindow.PromptType.Hint, null, null);
        }
    }


    /// <summary>
    /// 单个频道显示信息，世界频道除外
    /// </summary>
    /// <param name="channel"></param>
    void SetChatItemInChannel(int channel)
    {
        ClearChatItemInWindow();
        while (TextTranslator.instance.ChatItemDataList.size > 20)//最多20条信息
        {
            TextTranslator.instance.ChatItemDataList.RemoveAt(0);
        }

        for (int i = 0; i < TextTranslator.instance.ChatItemDataList.size; i++)
        {
            if (channel == TextTranslator.instance.ChatItemDataList[i].channel)
            {
                Debug.Log("this.channel==" + channel);
                CreatChatItem(TextTranslator.instance.ChatItemDataList[i].channel, TextTranslator.instance.ChatItemDataList[i].name, TextTranslator.instance.ChatItemDataList[i].textWords,
                    TextTranslator.instance.ChatItemDataList[i].vip, TextTranslator.instance.ChatItemDataList[i].headIcon, TextTranslator.instance.ChatItemDataList[i].legionID, TextTranslator.instance.ChatItemDataList[i].nationId, TextTranslator.instance.ChatItemDataList[i].useId, TextTranslator.instance.ChatItemDataList[i].legionCountryID);
            }
        }
        UpdateButtomOnChatWindow();
    }

    /// <summary>
    /// 世界频道显示信息,包含军团与国家信息
    /// </summary>
    void SetWordChatItemInChannel()
    {
        ClearChatItemInWindow();
        while (TextTranslator.instance.ChatItemDataList.size > 20)//最多20条信息
        {
            TextTranslator.instance.ChatItemDataList.RemoveAt(0);
        }
        for (int i = 0; i < TextTranslator.instance.ChatItemDataList.size; i++)
        {
            if (TextTranslator.instance.ChatItemDataList[i].channel == 2 || TextTranslator.instance.ChatItemDataList[i].channel == 3 || TextTranslator.instance.ChatItemDataList[i].channel == 4)
            {
                CreatChatItem(TextTranslator.instance.ChatItemDataList[i].channel, TextTranslator.instance.ChatItemDataList[i].name, TextTranslator.instance.ChatItemDataList[i].textWords,
                        TextTranslator.instance.ChatItemDataList[i].vip, TextTranslator.instance.ChatItemDataList[i].headIcon, TextTranslator.instance.ChatItemDataList[i].legionID, TextTranslator.instance.ChatItemDataList[i].nationId, TextTranslator.instance.ChatItemDataList[i].useId, TextTranslator.instance.ChatItemDataList[i].legionCountryID);
            }
        }
        UpdateButtomOnChatWindow();
    }

    /// <summary>
    /// 切换频道时清除下面grid子物体,重置长度坐标
    /// </summary>
    void ClearChatItemInWindow()
    {
        for (int i = uiGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(uiGrid.transform.GetChild(i).gameObject);
        }
        uiGrid.transform.parent.GetComponent<UIScrollView>().ResetPosition();
        HeightAll = 0;
    }


    /// <summary>
    /// 创建聊天信息Item
    /// </summary>
    public void CreatChatItem(int channel, string _Name, string _Text, int vipLevel, int headIcon, int legionId, int nationId, int useId,int legionCountryID)
    {
        GameObject go = NGUITools.AddChild(uiGrid, itemPrafeb);
        go.GetComponent<ChatItem>().SetInfo(_Name, _Text, channel, vipLevel, headIcon, legionId, nationId, useId);
        uiGrid.GetComponent<ChatGrid>().Reposition();
        if (go.transform.FindChild("ApplayButton").gameObject.activeSelf)//yy  判定每个chatitem长度
        {
            HeightAll += go.transform.Find("TextBoard").gameObject.GetComponent<UISprite>().height + 90f;
        }
        else
        {
            HeightAll += go.transform.Find("TextBoard").gameObject.GetComponent<UISprite>().height + 60f;
        }

        Debug.Log("进入创建聊天item");

        #region
        //if (channel != 3)
        //{
        //    GameObject go = NGUITools.AddChild(uiGrid, itemPrafeb);
        //    go.GetComponent<ChatItem>().SetInfo(_Name, _Text, channel, vipLevel, headIcon, legionId, nationId, useId);
        //    uiGrid.GetComponent<ChatGrid>().Reposition();
        //    if (go.transform.FindChild("ApplayButton").gameObject.activeSelf)//yy  判定每个chatitem长度
        //    {
        //        HeightAll += go.transform.Find("TextBoard").gameObject.GetComponent<UISprite>().height + 90f;
        //    }
        //    else
        //    {
        //        HeightAll += go.transform.Find("TextBoard").gameObject.GetComponent<UISprite>().height + 60f;
        //    }
        //}
        //else
        //{
        //    if (legionId > 0 && CharacterRecorder.instance.legionID == legionId) //确认我的军团信息
        //    {
        //        GameObject go = NGUITools.AddChild(uiGrid, itemPrafeb);
        //        go.GetComponent<ChatItem>().SetInfo(_Name, _Text, channel, vipLevel, headIcon, legionId, nationId, useId);
        //        uiGrid.GetComponent<ChatGrid>().Reposition();
        //        if (go.transform.FindChild("ApplayButton").gameObject.activeSelf)//yy  判定每个chatitem长度
        //        {
        //            HeightAll += go.transform.Find("TextBoard").gameObject.GetComponent<UISprite>().height + 90f;
        //        }
        //        else
        //        {
        //            HeightAll += go.transform.Find("TextBoard").gameObject.GetComponent<UISprite>().height + 60f;
        //        }
        //    }
        //}
        //channelItemList.Add(go);
        //UpdateButtomOnChatWindow();
        #endregion
    }


    /// <summary>
    /// 单个频道增加信息时显示
    /// </summary>

    public void CreatOneChatItem(int _channel, string _Name, string _Text, int vipLevel, int headIcon, int legionId, int nationId, int useId, int legionCountryID) //单独频道信息
    {
        if (this.channel == _channel || (this.channel == 2 && _channel == 3) || (this.channel == 2 && _channel == 4))
        {
            while (uiGrid.transform.childCount >= 19)
            {
                DestroyImmediate(uiGrid.transform.GetChild(0).gameObject);
            }

            GameObject go = NGUITools.AddChild(uiGrid, itemPrafeb);
            go.GetComponent<ChatItem>().SetInfo(_Name, _Text, _channel, vipLevel, headIcon, legionId, nationId, useId);
            uiGrid.GetComponent<ChatGrid>().Reposition();
            UpdataSetStringInfo(_channel, _Name, _Text, vipLevel, headIcon, legionId, nationId, useId, legionCountryID);
            HeightAll = 0f;
            for (int i = 0; i < uiGrid.transform.childCount; i++)
            {
                if (uiGrid.transform.GetChild(i).transform.FindChild("ApplayButton").gameObject.activeSelf)//yy  判定每个chatitem长度
                {
                    HeightAll += uiGrid.transform.GetChild(i).transform.Find("TextBoard").gameObject.GetComponent<UISprite>().height + 90f;
                }
                else
                {
                    HeightAll += uiGrid.transform.GetChild(i).transform.Find("TextBoard").gameObject.GetComponent<UISprite>().height + 60f;
                }
            }
            Debug.Log("进入创建聊天单个item");
            UpdateButtomOnChatWindow();
            #region
            //if (channel != 3)
            //{
            //    GameObject go = NGUITools.AddChild(uiGrid, itemPrafeb);
            //    go.GetComponent<ChatItem>().SetInfo(_Name, _Text, channel, vipLevel, headIcon, legionId, nationId, useId);
            //    uiGrid.GetComponent<ChatGrid>().Reposition();
            //    HeightAll = 0f;
            //    UpdataSetStringInfo(channel, _Name, _Text, vipLevel, headIcon, legionId, nationId, useId);
            //    for (int i = 0; i < uiGrid.transform.childCount; i++)
            //    {

            //        if (uiGrid.transform.GetChild(i).transform.FindChild("ApplayButton").gameObject.activeSelf)//yy  判定每个chatitem长度
            //        {
            //            HeightAll += uiGrid.transform.GetChild(i).transform.Find("TextBoard").gameObject.GetComponent<UISprite>().height + 90f;
            //        }
            //        else
            //        {
            //            HeightAll += uiGrid.transform.GetChild(i).transform.Find("TextBoard").gameObject.GetComponent<UISprite>().height + 60f;
            //        }
            //    }
            //}
            //else
            //{
            //    if (legionId > 0 && CharacterRecorder.instance.legionID == legionId) //确认我的军团信息
            //    {
            //        GameObject go = NGUITools.AddChild(uiGrid, itemPrafeb);
            //        go.GetComponent<ChatItem>().SetInfo(_Name, _Text, channel, vipLevel, headIcon, legionId, nationId, useId);
            //        uiGrid.GetComponent<ChatGrid>().Reposition();
            //        UpdataSetStringInfo(channel, _Name, _Text, vipLevel, headIcon, legionId, nationId, useId);
            //        HeightAll = 0f;
            //        for (int i = 0; i < uiGrid.transform.childCount; i++)
            //        {
            //            if (uiGrid.transform.GetChild(i).transform.FindChild("ApplayButton").gameObject.activeSelf)//yy  判定每个chatitem长度
            //            {
            //                HeightAll += uiGrid.transform.GetChild(i).transform.Find("TextBoard").gameObject.GetComponent<UISprite>().height + 90f;
            //            }
            //            else
            //            {
            //                HeightAll += uiGrid.transform.GetChild(i).transform.Find("TextBoard").gameObject.GetComponent<UISprite>().height + 60f;
            //            }
            //        }
            //    }
            //}
            //channelItemList.Add(go);
            #endregion            
        }
    }


    /// <summary>
    /// 刷新列表在最底部
    /// </summary>
    void UpdateButtomOnChatWindow() //刷新列表在最底部
    {
        if (HeightAll > 675f)
        {
            float CuntNum = HeightAll - 675;
            uiGrid.transform.parent.localPosition = new Vector3(65f, -45.7f + CuntNum, 0);
            uiGrid.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2(0, -CuntNum);
        }
    }


    /// <summary>
    /// 保存聊天数据在本地
    /// </summary>
    void SetChatInfoShow()
    {
        string Getstring = "";
        Debug.LogError("显示存入信息  ");
        int length = 0;
        if (channel == 1)
        {
            length = (ObscuredPrefs.GetInt("XitongNumber" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID")));
        }
        else if (channel == 2)
        {
            length = (ObscuredPrefs.GetInt("WorldNumber" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID")));
        }
        else if (channel == 3)
        {
            length = (ObscuredPrefs.GetInt("JunTuanNumber" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID")));
        }
        else if (channel == 4)
        {
            length = (ObscuredPrefs.GetInt("TeamNumber" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID")));
        }

        for (int i = 0; i < length; i++)
        {
            if (channel == 1)
            {
                if (ObscuredPrefs.GetString("XiTongTalk" + i + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID")) != "")
                {
                    Getstring = ObscuredPrefs.GetString("XiTongTalk" + i + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"));
                }
            }
            else if (channel == 2)
            {
                if (ObscuredPrefs.GetString("WorldTalk" + i + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID")) != "")
                {
                    Getstring = ObscuredPrefs.GetString("WorldTalk" + i + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"));
                }
            }
            else if (channel == 3)
            {
                if (ObscuredPrefs.GetString("JuntuanTalk" + i + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID")) != "")
                {
                    Getstring = ObscuredPrefs.GetString("JuntuanTalk" + i + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"));
                }
            }
            else if (channel == 4)
            {
                if (ObscuredPrefs.GetString("TeamTalk" + i + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID")) != "")
                {
                    Getstring = ObscuredPrefs.GetString("TeamTalk" + i + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"));
                }
            }
            if (Getstring != "")
            {
                string[] dataSplit = Getstring.Split(';');
                //(int channel, string name, string textWords, int vip, int headIcon, int legionID, int nationId, int useid)
                Debug.LogError("显示  " + Getstring);
                //for (int j = 0; j < TextTranslator.instance.ChatItemDataList.size; j++)
                {
                    //if(TextTranslator.instance.ChatItemDataList[j].channel!=int.Parse(dataSplit[0])&&
                    //    TextTranslator.instance.ChatItemDataList[j].name!=(dataSplit[1])&&
                    //    TextTranslator.instance.ChatItemDataList[j].textWords!=(dataSplit[2])&&
                    //    TextTranslator.instance.ChatItemDataList[j].vip!=int.Parse(dataSplit[3])&&
                    //    TextTranslator.instance.ChatItemDataList[j].headIcon!=int.Parse(dataSplit[4])&&
                    //    TextTranslator.instance.ChatItemDataList[j].legionID!=int.Parse(dataSplit[5])&&
                    //    TextTranslator.instance.ChatItemDataList[j].nationId!=int.Parse(dataSplit[6])&&
                    //    TextTranslator.instance.ChatItemDataList[j].useId != int.Parse(dataSplit[7]))
                    //if (TextTranslator.instance.ChatItemDataList.size == 0)
                    //{
                    for (int j = 0; j < TextTranslator.instance.ChatItemDataList.size; j++) {
                        if (TextTranslator.instance.ChatItemDataList[j].useId == int.Parse(dataSplit[7])
                            && TextTranslator.instance.ChatItemDataList[j].textWords == (dataSplit[2])
                            && TextTranslator.instance.ChatItemDataList[j].channel == int.Parse(dataSplit[0])                    
                            )
                        {
                            TextTranslator.instance.ChatItemDataList.Remove(TextTranslator.instance.ChatItemDataList[j]);
                        }           
                    }
                
                        ChatItemData _ChatItemData = new ChatItemData(int.Parse(dataSplit[0]), dataSplit[1], dataSplit[2],
                              int.Parse(dataSplit[3]), int.Parse(dataSplit[4]),
                              int.Parse(dataSplit[5]), int.Parse(dataSplit[6]), int.Parse(dataSplit[7]), int.Parse(dataSplit[8]));
                        TextTranslator.instance.AddChatItemData(_ChatItemData);
                        //CreatChatItem(int.Parse(dataSplit[0]), dataSplit[1], dataSplit[2],
                        //              int.Parse(dataSplit[3]), int.Parse(dataSplit[4]),
                        //              int.Parse(dataSplit[5]), int.Parse(dataSplit[6]), int.Parse(dataSplit[7]));

                    //}
                    //else
                    //{
                    //    for (int j = 0; j < TextTranslator.instance.ChatItemDataList.size; j++)
                    //    {
                    //        //        //if (TextTranslator.instance.ChatItemDataList[j].channel != int.Parse(dataSplit[0]) ||
                    //        //        if (
                    //        //            TextTranslator.instance.ChatItemDataList[j].name == (dataSplit[1])
                    //        //         )
                    //        //        //TextTranslator.instance.ChatItemDataList[j].vip != int.Parse(dataSplit[3]) &&
                    //        //        //TextTranslator.instance.ChatItemDataList[j].headIcon != int.Parse(dataSplit[4]) &&
                    //        //        //TextTranslator.instance.ChatItemDataList[j].legionID != int.Parse(dataSplit[5]) &&
                    //        //        //TextTranslator.instance.ChatItemDataList[j].nationId != int.Parse(dataSplit[6]) &&
                    //        //        //TextTranslator.instance.ChatItemDataList[j].useId != int.Parse(dataSplit[7]))
                    //        //        {
                    //        //            if (TextTranslator.instance.ChatItemDataList[j].channel == int.Parse(dataSplit[0]) || TextTranslator.instance.ChatItemDataList[j].textWords == (dataSplit[2]))
                    //        //            {
                    //        //                ChatItemData _ChatItemData = new ChatItemData(int.Parse(dataSplit[0]), dataSplit[1], dataSplit[2],
                    //        //                                            int.Parse(dataSplit[3]), int.Parse(dataSplit[4]),
                    //        //                                            int.Parse(dataSplit[5]), int.Parse(dataSplit[6]), int.Parse(dataSplit[7]));
                    //        //                TextTranslator.instance.AddChatItemData(_ChatItemData);
                    //        //                CreatChatItem(int.Parse(dataSplit[0]), dataSplit[1], dataSplit[2],
                    //        //                              int.Parse(dataSplit[3]), int.Parse(dataSplit[4]),
                    //        //                              int.Parse(dataSplit[5]), int.Parse(dataSplit[6]), int.Parse(dataSplit[7]));
                    //        //            }
                    //        //        }
                    //        //        else
                    //        //        {
                    //        //            Debug.LogError("ssssssadasd");
                    //        //        }
                    //        if (TextTranslator.instance.ChatItemDataList[j].channel != int.Parse(dataSplit[0]) &&
                    //            TextTranslator.instance.ChatItemDataList[j].name != (dataSplit[1]) &&
                    //            TextTranslator.instance.ChatItemDataList[j].textWords != (dataSplit[2]) &&
                    //            TextTranslator.instance.ChatItemDataList[j].vip != int.Parse(dataSplit[3]) &&
                    //            TextTranslator.instance.ChatItemDataList[j].headIcon != int.Parse(dataSplit[4]) &&
                    //            TextTranslator.instance.ChatItemDataList[j].legionID != int.Parse(dataSplit[5]) &&
                    //            TextTranslator.instance.ChatItemDataList[j].nationId != int.Parse(dataSplit[6]) &&
                    //            TextTranslator.instance.ChatItemDataList[j].useId != int.Parse(dataSplit[7]))
                    //        {
                    //            ChatItemData _ChatItemData = new ChatItemData(int.Parse(dataSplit[0]), dataSplit[1], dataSplit[2],
                    //                                                       int.Parse(dataSplit[3]), int.Parse(dataSplit[4]),
                    //                                                       int.Parse(dataSplit[5]), int.Parse(dataSplit[6]), int.Parse(dataSplit[7]));
                    //            TextTranslator.instance.AddChatItemData(_ChatItemData);
                    //            //CreatChatItem(int.Parse(dataSplit[0]), dataSplit[1], dataSplit[2],
                    //            //              int.Parse(dataSplit[3]), int.Parse(dataSplit[4]),
                    //            //              int.Parse(dataSplit[5]), int.Parse(dataSplit[6]), int.Parse(dataSplit[7]));
                    //        }

                    //    }
                    //}
                }
            }
        }
        UpdateButtomOnChatWindow();
    }
    //存入数据,记录数量
    void UpdataSetStringInfo(int _channel, string _Name, string _Text, int vipLevel, int headIcon, int legionId, int nationId, int useId,int legionCountryID)
    {
        string talkString = "";        
        if (channel == 1)
        {
            XitongNumber = ObscuredPrefs.GetInt("XitongNumber" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"));
            //if (XitongNumber > 100)  用于优化存储条目过多
            //{
            //    XitongNumber = 0;
            //}
            XitongNumber += 1;
            talkString = "XiTongTalk" + XitongNumber + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID");
            ObscuredPrefs.SetInt("XitongNumber" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"), XitongNumber);
            Debug.LogError("存入数据 " + XitongNumber + "    " + "XitongNumber" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"));

        }
        else if (channel == 2)
        {
            WorldNumber = ObscuredPrefs.GetInt("WorldNumber" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"));
            WorldNumber += 1;
            talkString = "WorldTalk" + WorldNumber + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID");
            ObscuredPrefs.SetInt("WorldNumber" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"), WorldNumber);
            Debug.LogError("存入数据 " + WorldNumber + "    " + "WorldNumber" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"));

        }
        else if (channel == 4)
        {
            TeamNumber = ObscuredPrefs.GetInt("TeamNumber" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"));
            TeamNumber += 1;
            talkString = "TeamTalk" + TeamNumber + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID");

            ObscuredPrefs.SetInt("TeamNumber" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"), TeamNumber);
            Debug.LogError("存入数据 " + TeamNumber + "    " + "TeamNumber" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"));

        }
        else if (channel == 3)
        {
            JuntuanNumber = ObscuredPrefs.GetInt("JuntuanNumber" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"));
            JuntuanNumber += 1;
            talkString = "JunTuanTalk" + JuntuanNumber + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID");
            ObscuredPrefs.SetInt("JuntuanNumber" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"), JuntuanNumber);
            Debug.LogError("存入数据 " + JuntuanNumber + "    " + "JuntuanNumber" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"));
        }
        ObscuredPrefs.SetString(talkString, channel + ";" + _Name + ";" + _Text + ";" + vipLevel + ";" + headIcon + ";" + legionId + ";" + nationId + ";" + useId + ";" + legionCountryID+";");
    }
    private void SetMainWindowChatColor()
    {
        if (GameObject.Find("MainWindow") != null) 
        {
            GameObject.Find("MainWindow").GetComponent<MainWindow>().SetChatSpriteColor();
        }
    }


    private void TweenPositionToMove() 
    {
        //if (System.Convert.ToSingle(Screen.height) / Screen.width >= System.Convert.ToSingle(600) / 900 - 0.02f && System.Convert.ToSingle(Screen.height) / Screen.width < System.Convert.ToSingle(1536) / 2048)
        //{
        //    tweenposition.ResetToBeginning();
        //    tweenposition.from = new Vector3(-1000f, 0, 0);
        //    tweenposition.to = new Vector3(-8f, 0, 0);
        //    tweenposition.PlayForward();
        //}
        //else
        //{
        //    tweenposition.ResetToBeginning();
        //    tweenposition.from = new Vector3(-1000f, 0, 0);
        //    tweenposition.to = new Vector3(-118f, 0, 0);
        //    tweenposition.PlayForward();
        //}

        //System.Type T = System.Type.GetType("UnityEditor.GameView,UnityEditor");
        //System.Reflection.MethodInfo GetMainGameView = T.GetMethod("GetMainGameView", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static);
        //System.Object Res = GetMainGameView.Invoke(null, null);
        //var gameView = (UnityEditor.EditorWindow)Res;
        //var prop = gameView.GetType().GetProperty("currentGameViewSize", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        //var gvsize = prop.GetValue(gameView, new object[0] { });
        //var gvSizeType = gvsize.GetType();

        //var debug_h = (int)gvSizeType.GetProperty("height", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).GetValue(gvsize, new object[0] { });
        //var debug_w = (int)gvSizeType.GetProperty("width", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance).GetValue(gvsize, new object[0] { });

        //Debug.LogError("debug_w " + debug_w);
        //Debug.LogError("debug_h " + debug_h);

        ////float length = (debug_w - 1200) / 2f;

        ////float topositionx = -8f - length;

        //float topositionx = (debug_w / 1200f) * (-8f);
        //Debug.LogError("topositionx " + topositionx);

        //tweenposition.ResetToBeginning();
        //tweenposition.from = new Vector3(-1000f, 0, 0);
        //tweenposition.to = new Vector3(topositionx, 0, 0);
        //tweenposition.PlayForward();

        UIAnchor Ua = gameObject.transform.Find("Content").GetComponent<UIAnchor>();
        //if (GameObject.Find("LegionWarWindow") != null)
        //{
        //    Ua.uiCamera = GameObject.Find("LegionWarWindow/CYFCamera").GetComponent<Camera>();
        //    Ua.gameObject.SetActive(true);
        //}
        //else 
        //{
        //    Ua.uiCamera = GameObject.Find("UIRoot/Camera").GetComponent<Camera>();
        //    Ua.gameObject.SetActive(true);
        //}
        Ua.container = GameObject.Find("UIRoot");
    }
}
