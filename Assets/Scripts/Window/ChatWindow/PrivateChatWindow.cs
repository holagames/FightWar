using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PrivateChatWindow : MonoBehaviour {

    public UIInput input;
    public GameObject SendButton;
    public GameObject CharacterButtonItem;
    public GameObject LeftChatItem;
    public GameObject RightChatItem;


    public GameObject LeftGrid;
    public GameObject RightGrid;

    public GameObject Setting;
    public UILabel HumanLabel;

    public GameObject SetWindow;
    //List<int> UidList = new List<int>(); //
    private List<PrivateChatItemData> PrivateDataList = new List<PrivateChatItemData>();//确定私聊玩家唯一列表

    private int NowVip = 0;
    private int NowHeadIcon = 0;
    private int NowGuid = 0;//对方的guid
    List<int> leftlist = new List<int>();


    public GameObject Toggle1;
    public GameObject Toggle2;
    public GameObject SaveButton;
    public GameObject CloseButton;
    public GameObject TopInfo;
    public GameObject SetCloseButton; 
    void Start() 
    {
        //GetCharacterNumber();
        UIEventListener.Get(SendButton).onClick = ClickSendButton;
        UIEventListener.Get(Setting).onClick = delegate(GameObject go)
        {
            SetWindow.SetActive(true);
        };

        UIEventListener.Get(Toggle1).onClick = delegate(GameObject go)
        {
            if (Toggle1.transform.Find("Checkmark").gameObject.activeSelf)
            {
                Toggle1.transform.Find("Checkmark").gameObject.SetActive(false);
            }
            else
            {
                Toggle1.transform.Find("Checkmark").gameObject.SetActive(true);
            }
        };

        UIEventListener.Get(SaveButton).onClick = delegate(GameObject go)
        {
            if (Toggle1.transform.Find("Checkmark").gameObject.activeSelf)
            {
                PlayerPrefs.SetInt("PrivateState1", 1);
            }
            else
            {
                PlayerPrefs.SetInt("PrivateState1", 0);
            }
            SetWindow.SetActive(false);
        };

        UIEventListener.Get(CloseButton).onClick = delegate(GameObject go)
        {
            UIManager.instance.BackUI();
        };

        UIEventListener.Get(SetCloseButton).onClick = delegate(GameObject go)
        {
            SetWindow.SetActive(false);
        };

        if (PlayerPrefs.GetInt("PrivateState1")==1)
        {
            Toggle1.transform.Find("Checkmark").gameObject.SetActive(true);
        }
        else
        {
            Toggle1.transform.Find("Checkmark").gameObject.SetActive(false);
        }
        if (CharacterRecorder.instance.HaveNewPrivateChatInfo) 
        {
            CharacterRecorder.instance.HaveNewPrivateChatInfo = false;
        }
    }

    void ClickSendButton(GameObject obj) 
    {
        string name = CharacterRecorder.instance.characterName;
        string text = input.value;

        if (NowGuid == 0 || NowGuid == CharacterRecorder.instance.userId)
        {
            UIManager.instance.OpenPromptWindow("请选择需要私聊的对象？", PromptWindow.PromptType.Alert, null, null);
        }
        else if (!string.IsNullOrEmpty(text))
        {
            if (text.IndexOf("@") == 0)
            {
                NetworkHandler.instance.SendProcess(text.Remove(0, 1));
                UIManager.instance.BackUI();
            }
            else
            {
                input.value = "";
                text += " ";
                if (text.Length > 50)
                {
                    UIManager.instance.OpenPromptWindow("所发送的内容不得超过50字", PromptWindow.PromptType.Alert, null, null);
                }
                else 
                {
                    NetworkHandler.instance.SendProcess(string.Format("7001#{0};{1};{2}${3}${4}${5}${6}${7};{8};", 9, text.Replace("@", "＠").Replace("#", "＃").Replace(";", "；"), CharacterRecorder.instance.Vip, CharacterRecorder.instance.headIcon, CharacterRecorder.instance.userId,NowVip ,NowHeadIcon,NowGuid, NowGuid));
                }               
            }
        }
        else
        {
            UIManager.instance.OpenPromptWindow("真的无话可说了吗？", PromptWindow.PromptType.Alert, null, null);
        }
    }

    void ShowTopInfo() 
    {
        if (CharacterRecorder.instance.MyFriendUIDList.Contains(NowGuid)) //是否陌生人
        {
            TopInfo.SetActive(false);
        }
        else 
        {
            TopInfo.SetActive(true);
        }
    }
    public void AddOneCharacterInfo() //添加一个私聊,1020协议用
    {
        PrivateDataList.Clear();
        leftlist.Clear();
        //bool isstay = false;
        //for (int i = PrivateDataList.Count - 1; i >= 0; i--)
        //{
        //    if (PrivateDataList[i].MyGuid == TextTranslator.instance.targetPlayerInfo.uId)
        //    {
        //        isstay = true;
        //    }
        //}
        for (int i = RightGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(RightGrid.transform.GetChild(i).gameObject);
        }
        if (TextTranslator.instance.targetPlayerInfo.uId != CharacterRecorder.instance.userId) 
        {
            //GameObject button = NGUITools.AddChild(LeftGrid, CharacterButtonItem);
            //button.SetActive(true);
            //button.name = TextTranslator.instance.targetPlayerInfo.uId.ToString();
            //button.transform.Find("NormalSprite/PlayerFrame/PlayerHead").GetComponent<UITexture>().mainTexture = Resources.Load(string.Format("Head/{0}", TextTranslator.instance.targetPlayerInfo.headIcon), typeof(Texture)) as Texture;
            //button.transform.Find("NormalSprite/PlayerFrame/VIPLabel").GetComponent<UILabel>().text = "Vip"+TextTranslator.instance.targetPlayerInfo.vip.ToString();
            //button.transform.Find("NormalSprite/NameLabel").GetComponent<UILabel>().text = TextTranslator.instance.targetPlayerInfo.name.ToString();
            //button.transform.Find("ChangeSprite/PlayerFrame/PlayerHead").GetComponent<UITexture>().mainTexture = Resources.Load(string.Format("Head/{0}", TextTranslator.instance.targetPlayerInfo.headIcon), typeof(Texture)) as Texture;
            //button.transform.Find("ChangeSprite/PlayerFrame/VIPLabel").GetComponent<UILabel>().text = "Vip" + TextTranslator.instance.targetPlayerInfo.vip.ToString();
            //button.transform.Find("ChangeSprite/NameLabel").GetComponent<UILabel>().text = TextTranslator.instance.targetPlayerInfo.name.ToString();
            //this.NowGuid = TextTranslator.instance.targetPlayerInfo.uId;
            //this.NowVip = TextTranslator.instance.targetPlayerInfo.vip;
            //this.NowHeadIcon = TextTranslator.instance.targetPlayerInfo.headIcon;
            //UIEventListener.Get(button).onClick = delegate(GameObject go)
            //{
            //    this.NowGuid = TextTranslator.instance.targetPlayerInfo.uId;
            //    this.NowVip = TextTranslator.instance.targetPlayerInfo.vip;
            //    this.NowHeadIcon = TextTranslator.instance.targetPlayerInfo.headIcon;
            //    Debug.Log("NowGuid " + NowGuid);
            //    SetOneCharacterChatInfo();
            //    ShowTopInfo();
            //};
            //LeftGrid.GetComponent<UIGrid>().Reposition();
            //button.GetComponent<UIToggle>().startsActive = true;
            //button.GetComponent<UIToggle>().value = true;
            //HumanLabel.text = "1/50";
            leftlist.Add(TextTranslator.instance.targetPlayerInfo.uId);
            PrivateDataList.Add(new PrivateChatItemData(9, TextTranslator.instance.targetPlayerInfo.name, "0", TextTranslator.instance.targetPlayerInfo.vip, TextTranslator.instance.targetPlayerInfo.headIcon, TextTranslator.instance.targetPlayerInfo.uId, CharacterRecorder.instance.Vip, CharacterRecorder.instance.headIcon, CharacterRecorder.instance.userId));
        }
        GetCharacterNumber();
    }

    public void GetCharacterNumber() //取得私聊玩家列表
    {
        //PrivateDataList.Clear();
        //List<int> leftlist = new List<int>();
        //leftlist.Clear();
        foreach (var _PrivateChatItemData in TextTranslator.instance.PrivateChatItemDataList) 
        {
            if (_PrivateChatItemData.MyGuid != CharacterRecorder.instance.userId && !leftlist.Contains(_PrivateChatItemData.MyGuid))//发送者不是我，判断私聊列表
            {
                leftlist.Add(_PrivateChatItemData.MyGuid);
                Debug.LogError("leftlist1  " + _PrivateChatItemData.MyGuid);
                if (PlayerPrefs.GetInt("PrivateState1") == 1)
                {
                    if (CharacterRecorder.instance.MyFriendUIDList.Contains(_PrivateChatItemData.MyGuid))
                    {

                        PrivateChatItemData PT = new PrivateChatItemData(_PrivateChatItemData.channel, _PrivateChatItemData.name, _PrivateChatItemData.textWords, _PrivateChatItemData.vip, _PrivateChatItemData.headIcon, _PrivateChatItemData.MyGuid, _PrivateChatItemData.Sendvip, _PrivateChatItemData.SendheadIcon, _PrivateChatItemData.SendGuid);
                        PrivateDataList.Add(PT);
                    }
                }
                else
                {
                    PrivateChatItemData PT = new PrivateChatItemData(_PrivateChatItemData.channel, _PrivateChatItemData.name, _PrivateChatItemData.textWords, _PrivateChatItemData.vip, _PrivateChatItemData.headIcon, _PrivateChatItemData.MyGuid, _PrivateChatItemData.Sendvip, _PrivateChatItemData.SendheadIcon, _PrivateChatItemData.SendGuid);
                    PrivateDataList.Add(PT);
                }
            }
            else if (_PrivateChatItemData.MyGuid == CharacterRecorder.instance.userId && !leftlist.Contains(_PrivateChatItemData.SendGuid)) //发送者是我，判断私聊接受列表
            {
                Debug.LogError("leftlist2  " + _PrivateChatItemData.SendGuid);
                leftlist.Add(_PrivateChatItemData.SendGuid);
                if (PlayerPrefs.GetInt("PrivateState1") == 1)
                {
                    if (CharacterRecorder.instance.MyFriendUIDList.Contains(_PrivateChatItemData.MyGuid))
                    {

                        PrivateChatItemData PT = new PrivateChatItemData(_PrivateChatItemData.channel, _PrivateChatItemData.name, _PrivateChatItemData.textWords, _PrivateChatItemData.Sendvip, _PrivateChatItemData.SendheadIcon, _PrivateChatItemData.SendGuid, _PrivateChatItemData.vip, _PrivateChatItemData.headIcon, _PrivateChatItemData.MyGuid);
                        PrivateDataList.Add(PT);
                    }
                }
                else
                {
                    PrivateChatItemData PT = new PrivateChatItemData(_PrivateChatItemData.channel, _PrivateChatItemData.name, _PrivateChatItemData.textWords, _PrivateChatItemData.Sendvip, _PrivateChatItemData.SendheadIcon, _PrivateChatItemData.SendGuid, _PrivateChatItemData.vip, _PrivateChatItemData.headIcon, _PrivateChatItemData.MyGuid);
                    PrivateDataList.Add(PT);
                }
            }
        }
        for (int i = PrivateDataList.Count - 1; i >= 0; i--) 
        {
            if (PrivateDataList[i].MyGuid == CharacterRecorder.instance.userId) 
            {
                PrivateDataList.RemoveAt(i);
                break;
            }
        }

        while (PrivateDataList.Count > 50) 
        {
            PrivateDataList.RemoveAt(0);
        }

        string CharacterIDStr = "";
        foreach (var _PrivateData in PrivateDataList) 
        {
            GameObject button = NGUITools.AddChild(LeftGrid, CharacterButtonItem);
            button.SetActive(true);
            button.name = _PrivateData.MyGuid.ToString();
            button.transform.Find("NormalSprite/PlayerFrame/PlayerHead").GetComponent<UITexture>().mainTexture = Resources.Load(string.Format("Head/{0}", _PrivateData.headIcon), typeof(Texture)) as Texture;
            button.transform.Find("NormalSprite/PlayerFrame/VIPLabel").GetComponent<UILabel>().text = "Vip" + _PrivateData.vip.ToString();
            button.transform.Find("NormalSprite/NameLabel").GetComponent<UILabel>().text = _PrivateData.name.ToString();
            button.transform.Find("ChangeSprite/PlayerFrame/PlayerHead").GetComponent<UITexture>().mainTexture = Resources.Load(string.Format("Head/{0}", _PrivateData.headIcon), typeof(Texture)) as Texture;
            button.transform.Find("ChangeSprite/PlayerFrame/VIPLabel").GetComponent<UILabel>().text = "Vip" + _PrivateData.vip.ToString();
            button.transform.Find("ChangeSprite/NameLabel").GetComponent<UILabel>().text = _PrivateData.name.ToString();

            int _NowGuid = _PrivateData.MyGuid;
            int _NowVip = _PrivateData.vip;
            int _NowHeadIcon = _PrivateData.headIcon;
            UIEventListener.Get(button).onClick = delegate(GameObject go)
            {
                this.NowGuid = _NowGuid;
                this.NowVip = _NowVip;
                this.NowHeadIcon = _NowHeadIcon;
                Debug.Log("NowGuid " + NowGuid);
                SetOneCharacterChatInfo();
                ShowTopInfo();
            };
            if (CharacterIDStr == "")
            {
                CharacterIDStr = _PrivateData.MyGuid.ToString()+"$";
            }
            else 
            {
                CharacterIDStr += _PrivateData.MyGuid.ToString() + "$";
            }
        }
        LeftGrid.GetComponent<UIGrid>().Reposition();
        HumanLabel.text = PrivateDataList.Count + "/" + "50";

        if (PrivateDataList.Count > 0) 
        {
            NowGuid = PrivateDataList[0].MyGuid;
            NowVip = PrivateDataList[0].vip;
            NowHeadIcon = PrivateDataList[0].headIcon;
            NetworkHandler.instance.SendProcess("7004#" + CharacterIDStr+";");
        }
        SetOneCharacterChatInfo();
    }

    void SetOneCharacterChatInfo() 
    {
        for (int i = RightGrid.transform.childCount - 1; i >= 0; i--) 
        {
            DestroyImmediate(RightGrid.transform.GetChild(i).gameObject);
        }
        foreach (var _PrivateChatItemData in TextTranslator.instance.PrivateChatItemDataList)
        {
            if (_PrivateChatItemData.MyGuid == NowGuid&&_PrivateChatItemData.SendGuid==CharacterRecorder.instance.userId) //发送给我
            {
                GameObject _LeftChatItem = NGUITools.AddChild(RightGrid, LeftChatItem);
                _LeftChatItem.SetActive(true);
                //_LeftChatItem.transform.localPosition=new Vector3()
                _LeftChatItem.name = "left";
                _LeftChatItem.transform.Find("PlayerFrame/PlayerHead").GetComponent<UITexture>().mainTexture = Resources.Load(string.Format("Head/{0}", _PrivateChatItemData.headIcon), typeof(Texture)) as Texture;
                _LeftChatItem.transform.Find("PlayerFrame/VIPLabel").GetComponent<UILabel>().text = "Vip" + _PrivateChatItemData.vip.ToString();
                _LeftChatItem.transform.Find("NameLabel").GetComponent<UILabel>().text = _PrivateChatItemData.name;
                _LeftChatItem.transform.Find("TextBoard/Label").GetComponent<UILabel>().text = _PrivateChatItemData.textWords;
            }
            else if (_PrivateChatItemData.MyGuid == CharacterRecorder.instance.userId && _PrivateChatItemData.SendGuid == NowGuid) //我发送给他
            {
                GameObject _RightChatItem = NGUITools.AddChild(RightGrid, RightChatItem);
                _RightChatItem.SetActive(true);
                //_LeftChatItem.transform.localPosition=new Vector3()
                _RightChatItem.name = "right";
                _RightChatItem.transform.Find("PlayerFrame/PlayerHead").GetComponent<UITexture>().mainTexture = Resources.Load(string.Format("Head/{0}", _PrivateChatItemData.headIcon), typeof(Texture)) as Texture;
                _RightChatItem.transform.Find("PlayerFrame/VIPLabel").GetComponent<UILabel>().text = "Vip" + _PrivateChatItemData.vip.ToString();
                _RightChatItem.transform.Find("NameLabel").GetComponent<UILabel>().text = _PrivateChatItemData.name;
                _RightChatItem.transform.Find("TextBoard/Label").GetComponent<UILabel>().text = _PrivateChatItemData.textWords;
            }
        }

        //RightGrid.GetComponent<PrivateChatGrid>().Reposition();
        GridReposition();
        UpdateButtomOnChatWindow();
    }

    public void CreatOneChatItem(int _channel, string _Name, string _Text, int vipLevel, int headIcon, int _MyGuid,int Sendvip,int SendheadIcon,int _SendGuid) //单独频道信息
    {
        if (PlayerPrefs.GetInt("PrivateState1") != 1)
        {
            if (_MyGuid == NowGuid && _SendGuid == CharacterRecorder.instance.userId) //发送给我
            {
                while (RightGrid.transform.childCount >= 19)
                {
                    DestroyImmediate(RightGrid.transform.GetChild(0).gameObject);
                }
                GameObject _LeftChatItem = NGUITools.AddChild(RightGrid, LeftChatItem);
                _LeftChatItem.SetActive(true);
                //_LeftChatItem.transform.localPosition=new Vector3()
                _LeftChatItem.name = "left";
                _LeftChatItem.transform.Find("PlayerFrame/PlayerHead").GetComponent<UITexture>().mainTexture = Resources.Load(string.Format("Head/{0}", headIcon), typeof(Texture)) as Texture;
                _LeftChatItem.transform.Find("PlayerFrame/VIPLabel").GetComponent<UILabel>().text = "Vip" + vipLevel.ToString();
                _LeftChatItem.transform.Find("NameLabel").GetComponent<UILabel>().text = _Name;
                _LeftChatItem.transform.Find("TextBoard/Label").GetComponent<UILabel>().text = _Text;
            }
            else if (_MyGuid == CharacterRecorder.instance.userId && _SendGuid == NowGuid) //我发送给他
            {
                GameObject _RightChatItem = NGUITools.AddChild(RightGrid, RightChatItem);
                _RightChatItem.SetActive(true);
                //_LeftChatItem.transform.localPosition=new Vector3()
                _RightChatItem.name = "right";
                _RightChatItem.transform.Find("PlayerFrame/PlayerHead").GetComponent<UITexture>().mainTexture = Resources.Load(string.Format("Head/{0}", headIcon), typeof(Texture)) as Texture;
                _RightChatItem.transform.Find("PlayerFrame/VIPLabel").GetComponent<UILabel>().text = "Vip" + vipLevel.ToString();
                _RightChatItem.transform.Find("NameLabel").GetComponent<UILabel>().text = _Name;
                _RightChatItem.transform.Find("TextBoard/Label").GetComponent<UILabel>().text = _Text;
            }
        }
        else 
        {
            if (_MyGuid == NowGuid && _SendGuid == CharacterRecorder.instance.userId && CharacterRecorder.instance.MyFriendUIDList.Contains(_MyGuid)) //发送给我
            {
                while (RightGrid.transform.childCount >= 19)
                {
                    DestroyImmediate(RightGrid.transform.GetChild(0).gameObject);
                }
                GameObject _LeftChatItem = NGUITools.AddChild(RightGrid, LeftChatItem);
                //_LeftChatItem.transform.localPosition=new Vector3()
                _LeftChatItem.SetActive(true);
                _LeftChatItem.name = "left";
                _LeftChatItem.transform.Find("PlayerFrame/PlayerHead").GetComponent<UITexture>().mainTexture = Resources.Load(string.Format("Head/{0}", headIcon), typeof(Texture)) as Texture;
                _LeftChatItem.transform.Find("PlayerFrame/VIPLabel").GetComponent<UILabel>().text = "Vip" + vipLevel.ToString();
                _LeftChatItem.transform.Find("NameLabel").GetComponent<UILabel>().text = _Name;
                _LeftChatItem.transform.Find("TextBoard/Label").GetComponent<UILabel>().text = _Text;
            }
            else if (_MyGuid == CharacterRecorder.instance.userId && _SendGuid == NowGuid) 
            {
                GameObject _RightChatItem = NGUITools.AddChild(RightGrid, RightChatItem);
                _RightChatItem.SetActive(true);
                //_LeftChatItem.transform.localPosition=new Vector3()
                _RightChatItem.name = "right";
                _RightChatItem.transform.Find("PlayerFrame/PlayerHead").GetComponent<UITexture>().mainTexture = Resources.Load(string.Format("Head/{0}", headIcon), typeof(Texture)) as Texture;
                _RightChatItem.transform.Find("PlayerFrame/VIPLabel").GetComponent<UILabel>().text = "Vip" + vipLevel.ToString();
                _RightChatItem.transform.Find("NameLabel").GetComponent<UILabel>().text = _Name;
                _RightChatItem.transform.Find("TextBoard/Label").GetComponent<UILabel>().text = _Text;
            }
        }
        //RightGrid.GetComponent<PrivateChatGrid>().Reposition();
        GridReposition();
        UpdateButtomOnChatWindow();
    }

    //void UpdateButtomOnChatWindow() //刷新列表在最底部
    //{
    //    if (HeightAll > 675f)
    //    {
    //        float CuntNum = HeightAll - 675;
    //        uiGrid.transform.parent.localPosition = new Vector3(65f, -45.7f + CuntNum, 0);
    //        uiGrid.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2(0, -CuntNum);
    //    }
    //}


    void GridReposition() 
    {
        for (int i = 0; i < RightGrid.transform.childCount; ++i)
        {
            Transform t = RightGrid.transform.GetChild(i);
            float height = 0;
            //UISprite cell = t.FindChild("TextBoard").gameObject.GetComponent<UISprite>();
            Vector3 pos = new Vector3(0, 0, 0);

            height = 150f * i;
            if (t.name == "left")
            {
                pos = new Vector3(-55, -height, 0);
            }
            else if (t.name == "right")
            {
                pos = new Vector3(60, -height, 0);
            }
            t.localPosition = pos;
        }
    }

    void UpdateButtomOnChatWindow() //刷新列表在最底部
    {
        float HeightAll = RightGrid.transform.childCount * 150f;
        if (HeightAll > 400f)
        {
            float CuntNum = HeightAll - 400f;
            RightGrid.transform.parent.localPosition = new Vector3(133f, -33f + CuntNum, 0);
            RightGrid.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2(0, -CuntNum);
        }
        else 
        {
            RightGrid.transform.parent.localPosition = new Vector3(133f, -33f, 0);
            RightGrid.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2(0,0);
        }
    }



    public void GetPrivateDataListOnLine(string Recving) 
    {
        string[] dataSplit=Recving.Split(';');
        if (dataSplit[0] != "") 
        {
            for (int i = 0; i < dataSplit.Length - 1; i++) 
            {
                string[] trcSplit = dataSplit[i].Split('$');
                for (int j = 0; j < LeftGrid.transform.childCount; j++) 
                {
                    if (LeftGrid.transform.GetChild(j).name == trcSplit[0]) 
                    {
                        GameObject item=LeftGrid.transform.GetChild(j).gameObject;
                        item.transform.Find("NormalSprite/NameLabel").GetComponent<UILabel>().text = trcSplit[2];
                        item.transform.Find("ChangeSprite/NameLabel").GetComponent<UILabel>().text = trcSplit[2];
                        if (trcSplit[1] == "1")
                        {
                            item.transform.Find("NormalSprite/OnlineLabel").GetComponent<UILabel>().text = "[00ff00](在线)[-]";
                            item.transform.Find("ChangeSprite/OnlineLabel").GetComponent<UILabel>().text = "[00ff00](在线)[-]";
                        }
                        else if(trcSplit[1] == "0")
                        {
                            item.transform.Find("NormalSprite/OnlineLabel").GetComponent<UILabel>().text = "[ff0000](离线)[-]";
                            item.transform.Find("ChangeSprite/OnlineLabel").GetComponent<UILabel>().text = "[ff0000](离线)[-]";
                        }
                        break;
                    }
                }
            }
        }
    }

    //protected void CalculateExpressionPos(ref string text)
    //{
    //    string space = "       ";
    //    NGUIText.finalSize = m_chatInput.label.defaultFontSize;//设置当前使用字体大小
    //    lineList.Clear();
    //    int row = 0;
    //    int textWidth = 0;
    //    int lastStartIndex = 0;
    //    string curLine = "";
    //    int length = text.Length;
    //    for (int i = 0; i < length; i++)
    //    {
    //        if (text[i] == '#' && i + 4 < length && text[i + 1] == 'e')
    //        {
    //            string eName = text.Substring(i + 2, 3);
    //            int eIndex = 0;
    //            Vector3 ePos = Vector3.zero;
    //            if (int.TryParse(eName, out eIndex))
    //            {
    //                float fx = 0;

    //                text = text.Remove(i, 5);
    //                text = text.Insert(i, space);//space = "       ";
    //                length = text.Length;
    //                //这里的CalculatePrintedSize是重载过的，
    //                //与原本方法相比多的一个参数自定义行款，替换原方法中的rectWidth即可
    //                textWidth = Mathf.RoundToInt(
    //                   NGUIText.CalculatePrintedSize(
    //                   text.Substring(lastStartIndex, i - lastStartIndex),
    //                   BASELINEWIDTH + 30).x);
    //                //BASELINEWIDTH为标准行宽度，30是根据表情大小确定的，
    //                //这里的表情大小是30*30
    //                if (textWidth > BASELINEWIDTH - 30)
    //                {
    //                    curLine = text.Substring(lastStartIndex, i - lastStartIndex + 1);
    //                    lineList.Add(curLine);

    //                    if (textWidth <= BASELINEWIDTH - 15 ||
    //                        textWidth >= BASELINEWIDTH)//行末尾不够需换行
    //                    {
    //                        fx = 0;
    //                        row++;
    //                        lastStartIndex = i;
    //                        ePos.x = fx - m_offsetX;
    //                        ePos.y = row;
    //                        ePos.z = eIndex;
    //                    }
    //                    else   //行末尾足够不需换行
    //                    {
    //                        fx = textWidth;
    //                        lastStartIndex = i + space.Length;
    //                        ePos.x = fx - m_offsetX;
    //                        ePos.y = row;
    //                        ePos.z = eIndex;
    //                        row++;
    //                    }
    //                }
    //                else
    //                {
    //                    fx = textWidth;
    //                    ePos.x = fx - m_offsetX;
    //                    ePos.y = row;
    //                    ePos.z = eIndex;
    //                }
    //            }
    //            if (eIndex != 0)
    //            {
    //                eList.Add(ePos);
    //            }

    //            if (!expInLine.ContainsKey(row))        //有表情无表情行,以此确定行间距
    //            {
    //                expInLine.Add(row, true);
    //            }
    //        }
    //        else      //记录换行起始Index
    //        {
    //            if (i - lastStartIndex < 0) continue;

    //            float curWidth = Mathf.RoundToInt(
    //               NGUIText.CalculatePrintedSize(
    //               text.Substring(lastStartIndex, i - lastStartIndex + 1),
    //               BASELINEWIDTH + 30).x);
    //            if (curWidth > BASELINEWIDTH)
    //            {
    //                curLine = text.Substring(lastStartIndex, i - lastStartIndex + 1);
    //                lineList.Add(curLine);
    //                lastStartIndex = i + 1;
    //                row++;
    //            }

    //            if (i == length - 1)
    //            {
    //                if (i - lastStartIndex < 0) continue;

    //                curLine = text.Substring(lastStartIndex, i - lastStartIndex + 1);
    //                lineList.Add(curLine);
    //            }
    //        }
    //    }
    //}
}



public class PrivateChatItemData
{
    public int channel { get; set; }
    public string name { get; set; }
    public string textWords { get; set; }
    public int vip { get; set; }
    public int headIcon { get; set; }
    public int MyGuid { get; set; }



    public int Sendvip { get; set; }
    public int SendheadIcon { get; set; }
    public int SendGuid { get; set; }

    public PrivateChatItemData(int channel, string name, string textWords, int vip, int headIcon, int MyGuid,int Sendvip,int SendheadIcon,int SendGuid)
    {
        this.channel = channel;
        this.name = name;
        this.textWords = textWords;
        this.vip = vip;
        this.headIcon = headIcon;
        this.MyGuid = MyGuid;

        this.Sendvip = Sendvip;
        this.SendheadIcon = SendheadIcon;
        this.SendGuid = SendGuid;
    }
}