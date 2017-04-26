using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Text;
public class ChatItemData
{
    public int channel { get; set; }//频道
    public string name { get; set; }//姓名
    public string textWords { get; set; }//内容
    public int vip { get; set; }//vip
    public int headIcon { get; set; }//头像
    public int legionID { get; set; }//军团id
    public int nationId { get; set; }//军衔
    public int useId { get; set; }//用户id
    public int legionCountryID { get; set; }//国家id
    public ChatItemData(int channel, string name, string textWords, int vip, int headIcon, int legionID, int nationId, int useid,int legionCountryID)
    {
        this.channel = channel;
        this.name = name;
        this.textWords = textWords;
        this.vip = vip;
        this.headIcon = headIcon;
        this.legionID = legionID;
        this.nationId = nationId;
        this.useId = useid;
        this.legionCountryID = legionCountryID;
    }
}
public class ChatItem : MonoBehaviour
{

    public UILabel Name;
    public UILabel vipLabel;
    public UITexture headIcon;
    public UILabel channelLabel;
    public UILabel Text;
    public UISprite ChatBG;
    public UISprite TypeFrame;
    public GameObject Label;
    public List<string> Prefabs;
    public GameObject FacePrefab;
    public UILabel JunxianLabel;

    public GameObject PlayerFrame;
    [SerializeField]
    private GameObject applayButton;
    private int RoomID = -1;//房间号
    private int legionId = 0;//军团ID
    private int channel = 1;
    public enum InfoType
    {
        Text,
        Face,
    }
    public class LabelType
    {
        public string info;
        public InfoType type;

        public LabelType(string text, InfoType tp)
        {
            info = text;
            type = tp;
        }
    }

    int ChatBoardWidth = 918;
    int MaxLineWidth = 450;//500;//聊天label最大宽度
    int cellHeight = 30;
    int labelLength = 0;
    int i = 0;
    List<GameObject> LabelPrafebList = new List<GameObject>();
    List<float> LabelReduceList = new List<float>();
    private static List<LabelType> list;
    private List<UILabel> labelCaches;
    private int positionX = 10;
    private int positionY = 12;
    private int labelNameWidth;

    //表情缓存区
    private List<GameObject> prefabCeches;

    //当前使用的数据
    private List<GameObject> prefabCur;

    private int UseID = 0;
    // Use this for initialization
    void Awake()
    {
        prefabCur = new List<GameObject>();
        labelCaches = new List<UILabel>();
        prefabCeches = new List<GameObject>();
        list = new List<LabelType>();

    }

    void Start()
    {
        if (UIEventListener.Get(PlayerFrame).onClick == null)
        {
            UIEventListener.Get(PlayerFrame).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("1020#" + UseID + ";");
                UIManager.instance.OpenPanel("LegionMemberItemDetail", false);
               // GameObject.Find("LegionMemberItemDetail").GetComponent<LegionMemberItemDetail>().SetLegionMemberItemDetail(this.OneLegionMemberData);
            };
        }

        if (UIEventListener.Get(applayButton).onClick == null)
        {
            UIEventListener.Get(applayButton).onClick += delegate(GameObject go)
            {
                int OpenGate = TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zuduifuben).Level;
                if (CharacterRecorder.instance.lastGateID < OpenGate)
                {
                    UIManager.instance.OpenPromptWindow("长官,通关" + (OpenGate % 10000) + "开放组队副本", PromptWindow.PromptType.Hint, null, null);
                }
                else if (legionId != 0 && legionId == CharacterRecorder.instance.legionID)
                {
                    UIManager.instance.OpenPromptWindow("已是此军团成员", PromptWindow.PromptType.Hint, null, null);
                }
                else if (legionId != 0)
                {
                    int OpenGate2 = TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.juntuan).Level;
                    if (CharacterRecorder.instance.lastGateID > OpenGate2)
                    {
                        NetworkHandler.instance.SendProcess("8008#" + legionId + ";");
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("通关" + (OpenGate2 % 10000) + "开放军团", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else if (RoomID != -1 && GameObject.Find("TeamInvitationWindow") == null)
                {
                    NetworkHandler.instance.SendProcess("6108#;");
                    NetworkHandler.instance.SendProcess("6109#" + RoomID + ";");
                }
                else if (GameObject.Find("TeamInvitationWindow") != null)
                {
                    UIManager.instance.OpenPromptWindow("您当前已在组队房间中", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
    }
    public void SetInfo(string name, string text, int channel, int vipLevel, int headIcon, int legionId, int nationId, int useId)
    {
        this.UseID = useId;
        applayButton.SetActive(false);
        vipLabel.text = string.Format("VIP{0}", vipLevel);
        this.headIcon.mainTexture = Resources.Load(string.Format("Head/{0}", headIcon), typeof(Texture)) as Texture;

        if (text.Contains("军团ID:"))//是否为军团长招募
        {
            string[] legionIdtr = text.Split(':');
            this.legionId = int.Parse(legionIdtr[1]);
            applayButton.SetActive(true);
        }
        else if (text.Contains("房间号")) //此处符号判断是否为组队邀请
        {
            Debug.Log("进入组队");
            if (text.Contains("："))
            {
                int num = 0;
                string[] Roomstr = text.Split('：');
                ASCIIEncoding ascii = new ASCIIEncoding();
                byte[] bytestr = ascii.GetBytes(Roomstr[1]);
                for (int j = 0; j < bytestr.Length; j++)
                {
                    if (bytestr[j] < 48 || bytestr[j] > 57)
                    {
                        num++;
                        break;
                    }
                }
                if (num == 0)
                {
                    Debug.Log("进入房间号");
                    RoomID = int.Parse(Roomstr[1]);
                    applayButton.SetActive(true);
                }
            }
        }

        int sr = text.IndexOf("百度");
        string web = "[url=1222]";
        if (sr != -1)
        {
            text = text.Insert(sr, web + "[u]");

            sr = text.IndexOf("百度");
            text = text.Insert(sr + 2, "[/u][/url]");
        }
        if (name == CharacterRecorder.instance.characterName)
        {

            //name在左边
            Name.text = "[a8e6fc]" + name + "[-]";
            switch (channel)
            {
                case 1:
                    channelLabel.text = "[ffdb50]系统[-]";
                    TypeFrame.spriteName = "frameRectangle4";
                    //Name.text ="[249bd2][世界] [ffffff]" +  Name.text;
                    break;
                case 2:
                    channelLabel.text = "[50e8ff]世界[-]";
                    TypeFrame.spriteName = "frameRectangle";
                    //Name.text ="[bb44ff][军团] [-]" +  Name.text;
                    break;
                case 3:
                    channelLabel.text = "[3fff4d]军团[-]";
                    TypeFrame.spriteName = "frameRectangle3";
                    break;
                case 4:
                    channelLabel.text = "[ff2652]国家[-]";
                    TypeFrame.spriteName = "frameRectangle2";
                    break;
                default:
                    break;
            }
            positionX = (int)ChatBG.transform.localPosition.x + 20;
            ParseSymbol(text);
            ShowSymbol(list, 1);
        }
        else
        {
            Name.text = "[a8e6fc]" + name + "[-]";
            switch (channel)
            {
                case 1:
                    channelLabel.text = "[ffdb50]系统[-]";
                    TypeFrame.spriteName = "frameRectangle4";
                    //Name.text ="[249bd2][世界] [ffffff]" +  Name.text;
                    break;
                case 2:
                    channelLabel.text = "[50e8ff]世界[-]";
                    TypeFrame.spriteName = "frameRectangle";
                    //Name.text ="[bb44ff][军团] [-]" +  Name.text;
                    break;
                case 3:
                    channelLabel.text = "[3fff4d]军团[-]";
                    TypeFrame.spriteName = "frameRectangle3";
                    break;
                case 4:
                    channelLabel.text = "[ff2652]国家[-]";
                    TypeFrame.spriteName = "frameRectangle2";
                    break;
                default:
                    break;
            }

            positionX = (int)ChatBG.transform.localPosition.x + 20;
            ParseSymbol(text);
            ShowSymbol(list, 1);
        }

        if (nationId != null)
        {
            if (nationId > 0 && nationId <= TextTranslator.instance.NationList.size)
            {
                JunxianLabel.gameObject.SetActive(true);
                JunxianLabel.text = "[ff2652]" + TextTranslator.instance.GetNationByID(nationId).OfficeName + "[-]";
            }
        }

        if (ChatBG.height > 100)
        {
            ChatBG.height -= 30;
        }
        applayButton.transform.localPosition = new Vector3(applayButton.transform.localPosition.x, -10 - ChatBG.height, 0);
    }

    private static void ParseSymbol(string value)
    {
        if (string.IsNullOrEmpty(value))
            return;

        int startIndex = 0;
        int endIndex = 0;
        string startString;
        string endString = value;

        string pattern = "\\{\\d\\d*\\}";
        MatchCollection matchs = Regex.Matches(value, pattern);
        string str;

        //Debug.LogError(matchs.Count);
        if (matchs.Count > 0)
        {
            foreach (Match item in matchs)
            {
                //Debug.LogError(item.Value);
                str = item.Value;
                startIndex = endString.IndexOf(str);
                endIndex = startIndex + str.Length;

                if (startIndex > -1)
                {
                    startString = endString.Substring(0, startIndex);

                    if (!string.IsNullOrEmpty(startString))
                        list.Add(new LabelType(startString, InfoType.Text));

                    if (!string.IsNullOrEmpty(str))
                        list.Add(new LabelType(str, InfoType.Face));

                    endString = endString.Substring(endIndex);
                }
            }

            if (!string.IsNullOrEmpty(endString))
                list.Add(new LabelType(endString, InfoType.Text));
        }
        else
        {
            list.Add(new LabelType(endString, InfoType.Text));
        }
    }
    //名字在右边还是左边
    private void ShowSymbol(List<LabelType> list, int type)//type 1:其他人的话  2:自己的话
    {
        if (type == 1)
        {
            foreach (LabelType item in list)
            {
                switch (item.type)
                {
                    case InfoType.Text:
                        CreateTextLabel(item.info);
                        break;
                    case InfoType.Face:
                        CreateFace(item.info);
                        break;
                }
            }
        }
        else if (type == 2)
        {
            foreach (LabelType item in list)
            {
                switch (item.type)
                {
                    case InfoType.Text:
                        //LabelPrafebList.Clear();
                        labelNameWidth = 0;
                        CreateTextLabelRight(item.info);
                        break;
                    case InfoType.Face:
                        CreateFace(item.info);
                        break;
                }
            }
        }
    }

    private void CreateTextLabel(string value)
    {

        UILabel label;
        if (labelCaches.Count > 0)
        {
            label = labelCaches[0];
            labelCaches.Remove(label);
            label.gameObject.SetActive(true);
        }
        else
        {
            GameObject go = GameObject.Instantiate(Label) as GameObject;
            go.transform.parent = this.transform;
            label = go.GetComponent<UILabel>();
            go.transform.localScale = Vector3.one;
        }

        string sbstr = "";
        string text = "";

        NGUIText.finalLineHeight = label.fontSize;
        NGUIText.finalSize = label.fontSize;
        NGUIText.dynamicFont = label.trueTypeFont;
        NGUIText.regionHeight = 10000;
        NGUIText.maxLines = 10000;
        NGUIText.regionWidth = (int)(MaxLineWidth - positionX);// + (Name.width / 2f) - positionX);

        NGUIText.WrapText(value, out sbstr);
        //Debug.LogError(MaxLineWidth + "      " + positionX + "    " + NGUIText.regionWidth + "   ()()()()");
        int index = sbstr.IndexOf("\n");

        if (index > -1)
        {
            text = sbstr.Substring(0, index);
        }
        else
        {
            text = sbstr;
        }

        label.text = text;

        label.gameObject.transform.localPosition = new Vector3(positionX, positionY, 0);
        LabelPrafebList.Add(label.gameObject);
        positionX += label.width;
        labelLength += label.width;
        //labelCur.Add(label);

        sbstr = sbstr.Remove(0, text.Length);

        if (sbstr.Length > 0)
        {
            positionX = 20 + (int)ChatBG.transform.localPosition.x;// +Name.width;
            positionY -= cellHeight;
            ChatBG.height += cellHeight;
            //ChatBG.width = (int)((label.transform.localPosition.x - ChatBG.transform.localPosition.x) + label.width + 30);

            sbstr = sbstr.Replace("\n", "");
            CreateTextLabel(sbstr);

        }
        if (ChatBG.width < (int)((label.transform.localPosition.x - ChatBG.transform.localPosition.x) + label.width + 30))
        {
            ChatBG.width = (int)((label.transform.localPosition.x - ChatBG.transform.localPosition.x) + label.width + 30);
        }
        //Debug.LogError(label.width);
    }

    private void CreateFace(string value)
    {
        int index = Prefabs.IndexOf(value);
        if (index > -1)
        {
            GameObject face;
            UIWidget widget;

            if (prefabCeches.Count > 0)
            {
                face = prefabCeches[0];
                prefabCeches.Remove(face);
                face.SetActive(true);
            }
            else
            {
                face = GameObject.Instantiate(FacePrefab) as GameObject;
                face.transform.parent = gameObject.transform;
                face.transform.localScale = FacePrefab.transform.localScale;
            }

            UISprite sprite = face.GetComponent<UISprite>();
            sprite.spriteName = value;
            widget = face.GetComponent<UIWidget>();
            widget.pivot = UIWidget.Pivot.TopLeft;


            if (MaxLineWidth < (positionX + widget.width))
            {
                positionX = 20 + (int)ChatBG.transform.localPosition.x;// +Name.width;
                positionY -= cellHeight;
                ChatBG.height += cellHeight;

            }

            face.transform.localPosition = new Vector3(positionX, positionY, 0);
            LabelPrafebList.Add(face);
            positionX += widget.width;
            labelLength += widget.width;
            if (ChatBG.width < (int)((sprite.transform.localPosition.x - ChatBG.transform.localPosition.x) + sprite.width + 30))
            {
                ChatBG.width = (int)((sprite.transform.localPosition.x - ChatBG.transform.localPosition.x) + sprite.width + 30);
            }
            prefabCur.Add(face);
        }
        else
        {
            CreateTextLabel(value);
        }
    }

    void SetLabelSide()
    {
        for (int i = 0; i < LabelPrafebList.Count; i++)
        {
            float number = LabelPrafebList[i].transform.localPosition.x - ChatBG.transform.localPosition.x;
            //Debug.LogError("相差:    " + number + "      " + LabelPrafebList[i].transform.localPosition.x + "    " + ChatBG.transform.localPosition.x);
            LabelReduceList.Add(number);
        }
        //ChatBG.transform.localPosition = new Vector3(ChatBoardWidth - ChatBG.width - Name.width - 30, ChatBG.transform.localPosition.y);
        for (int i = 0; i < LabelPrafebList.Count; i++)
        {
            LabelPrafebList[i].transform.localPosition = new Vector3(ChatBG.transform.localPosition.x + LabelReduceList[i], LabelPrafebList[i].transform.localPosition.y);
        }
    }

    private void CreateTextLabelRight(string value)
    {

        UILabel label;
        if (labelCaches.Count > 0)
        {
            label = labelCaches[0];
            labelCaches.Remove(label);
            label.gameObject.SetActive(true);
        }
        else
        {
            GameObject go = GameObject.Instantiate(Label) as GameObject;
            go.transform.parent = this.transform;
            label = go.GetComponent<UILabel>();
            go.transform.localScale = Vector3.one;
        }

        string sbstr = "";
        string text = "";

        NGUIText.finalLineHeight = label.fontSize;
        NGUIText.finalSize = label.fontSize;
        NGUIText.dynamicFont = label.trueTypeFont;
        NGUIText.regionHeight = 10000;
        NGUIText.maxLines = 10000;
        NGUIText.regionWidth = (int)(MaxLineWidth - positionX);// + (Name.width / 2f) - positionX);

        NGUIText.WrapText(value, out sbstr);
        //Debug.LogError(MaxLineWidth + "      " + positionX + "    " + NGUIText.regionWidth + "   ()()()()");
        int index = sbstr.IndexOf("\n");

        if (index > -1)
        {
            text = sbstr.Substring(0, index);
        }
        else
        {
            text = sbstr;
        }

        label.text = text;

        label.gameObject.transform.localPosition = new Vector3(positionX, positionY, 0);
        LabelPrafebList.Add(label.gameObject);
        positionX += label.width;
        labelLength += label.width;
        //labelCur.Add(label);

        sbstr = sbstr.Remove(0, text.Length);
        if (sbstr.Length > 0)
        {
            positionX = 20;// +Name.width;
            positionY -= cellHeight;
            ChatBG.height += cellHeight;
            //ChatBG.width = (int)((label.transform.localPosition.x - ChatBG.transform.localPosition.x) + label.width + 30);

            sbstr = sbstr.Replace("\n", "");
            CreateTextLabel(sbstr);

        }

        if (ChatBG.width < (int)((label.transform.localPosition.x - ChatBG.transform.localPosition.x) + label.width + 30))
        {
            ChatBG.width = (int)((label.transform.localPosition.x - ChatBG.transform.localPosition.x) + label.width + 30);
        }

        if (labelNameWidth == 0)
        {
            labelNameWidth = label.width;
            Invoke("SetLabelSide", 0.01f);
        }

    }
    #region 完整旧的聊天
    /*   public void SetInfo(string name, string text, int channel)
    {
        int sr = text.IndexOf("百度");
        string web = "[url=1222]";
        if (sr != -1)
        {
            text = text.Insert(sr, web + "[u]");

            sr = text.IndexOf("百度");
            text = text.Insert(sr + 2, "[/u][/url]");
        }
        if (name == "悦枫是神lalala")
        {
            Name.text = "[a8e6fc]"+name+"[-]";
            switch (channel)
            {
                case 1:
                    Name.text = Name.text + "[世界]";
                    break;
                case 2:
                    Name.text = Name.text + "[bb44ff][军团][-]";
                    break;
                default:
                    break;
            }
  //          Name.transform.localPosition = new Vector3(ChatBoardWidth - Name.width - 28, 0, 0);
            Name.transform.localPosition = new Vector3(ChatBoardWidth - Name.width - 48, 0, 0);

            //ChatBG.transform.localPosition = new Vector3(ChatBoardWidth - Text.width - Name.width - 61, 31, 0);
            ChatBG.transform.localPosition = new Vector3(Name.width, 31, 0);
            positionX = (int)ChatBG.transform.localPosition.x + 12;
            ParseSymbol(text);
            ShowSymbol(list, 2);

        }
        else
        {
            Name.text = "[a8e6fc]" + name + "[-]";
            switch (channel)
            {
                case 1:
                    Name.text = "[249bd2][世界] [ffffff]" + Name.text;
                    break;
                case 2:
                    Name.text = "[bb44ff][军团] [-]" + Name.text;
                    break;
                default:
                    break;
            }
            ChatBG.transform.localPosition = new Vector3(Name.width, 31, 0);
            positionX = (int)ChatBG.transform.localPosition.x + 12;
            ParseSymbol(text);
            ShowSymbol(list, 1);
        }
    }

    private static void ParseSymbol(string value)
    {
        if (string.IsNullOrEmpty(value))
            return;

        int startIndex = 0;
        int endIndex = 0;
        string startString;
        string endString = value;

        string pattern = "\\{\\d\\d*\\}";
        MatchCollection matchs = Regex.Matches(value, pattern);
        string str;

        //Debug.LogError(matchs.Count);
        if (matchs.Count > 0)
        {
            foreach (Match item in matchs)
            {
                //Debug.LogError(item.Value);
                str = item.Value;
                startIndex = endString.IndexOf(str);
                endIndex = startIndex + str.Length;

                if (startIndex > -1)
                {
                    startString = endString.Substring(0, startIndex);

                    if (!string.IsNullOrEmpty(startString))
                        list.Add(new LabelType(startString, InfoType.Text));

                    if (!string.IsNullOrEmpty(str))
                        list.Add(new LabelType(str, InfoType.Face));

                    endString = endString.Substring(endIndex);
                }
            }

            if (!string.IsNullOrEmpty(endString))
                list.Add(new LabelType(endString, InfoType.Text));
        }
        else
        {
            list.Add(new LabelType(endString, InfoType.Text));
        }
    }

    private void ShowSymbol(List<LabelType> list, int type)//type 1:其他人的话  2:自己的话
    {
        if (type == 1)
        {
            foreach (LabelType item in list)
            {
                switch (item.type)
                {
                    case InfoType.Text:
                        CreateTextLabel(item.info);
                        break;
                    case InfoType.Face:
                        CreateFace(item.info);
                        break;
                }
            }
        }
        else if (type == 2)
        {
            foreach (LabelType item in list)
            {
                switch (item.type)
                {
                    case InfoType.Text:
                        //LabelPrafebList.Clear();
                        labelNameWidth = 0;
                        CreateTextLabel1(item.info);
                        break;
                    case InfoType.Face:
                        CreateFace(item.info);
                        break;
                }
            }
        }
    }

    private void CreateTextLabel(string value)
    {

        UILabel label;
        if (labelCaches.Count > 0)
        {
            label = labelCaches[0];
            labelCaches.Remove(label);
            label.gameObject.SetActive(true);
        }
        else
        {
            GameObject go = GameObject.Instantiate(Label) as GameObject;
            go.transform.parent = this.transform;
            label = go.GetComponent<UILabel>();
            go.transform.localScale = Vector3.one;
        }

        string sbstr = "";
        string text = "";

        NGUIText.finalLineHeight = label.fontSize;
        NGUIText.finalSize = label.fontSize;
        NGUIText.dynamicFont = label.trueTypeFont;
        NGUIText.regionHeight = 10000;
        NGUIText.maxLines = 10000;
        NGUIText.regionWidth = (int)(MaxLineWidth + (Name.width / 2f) - positionX);

        NGUIText.WrapText(value, out sbstr);
        //Debug.LogError(MaxLineWidth + "      " + positionX + "    " + NGUIText.regionWidth + "   ()()()()");
        int index = sbstr.IndexOf("\n");

        if (index > -1)
        {
            text = sbstr.Substring(0, index);
        }
        else
        {
            text = sbstr;
        }

        label.text = text;

        label.gameObject.transform.localPosition = new Vector3(positionX, positionY, 0);
        LabelPrafebList.Add(label.gameObject);
        positionX += label.width;
        labelLength += label.width;
        //labelCur.Add(label);

        sbstr = sbstr.Remove(0, text.Length);

        if (sbstr.Length > 0)
        {
            positionX = 12 + Name.width;
            positionY -= cellHeight;
            ChatBG.height += cellHeight;
            //ChatBG.width = (int)((label.transform.localPosition.x - ChatBG.transform.localPosition.x) + label.width + 30);

            sbstr = sbstr.Replace("\n", "");
            CreateTextLabel(sbstr);

        }
        if (ChatBG.width < (int)((label.transform.localPosition.x - ChatBG.transform.localPosition.x) + label.width + 30))
        {
            ChatBG.width = (int)((label.transform.localPosition.x - ChatBG.transform.localPosition.x) + label.width + 30);
        }       

    }

    private void CreateFace(string value)
    {
        int index = Prefabs.IndexOf(value);
        if (index > -1)
        {
            GameObject face;
            UIWidget widget;

            if (prefabCeches.Count > 0)
            {
                face = prefabCeches[0];
                prefabCeches.Remove(face);
                face.SetActive(true);
            }
            else
            {
                face = GameObject.Instantiate(FacePrefab) as GameObject;
                face.transform.parent = gameObject.transform;
                face.transform.localScale = FacePrefab.transform.localScale;
            }

            UISprite sprite = face.GetComponent<UISprite>();
            sprite.spriteName = value;
            widget = face.GetComponent<UIWidget>();
            widget.pivot = UIWidget.Pivot.TopLeft;


            if (MaxLineWidth < (positionX + widget.width))
            {
                positionX = 12 + Name.width;
                positionY -= cellHeight;
                ChatBG.height += cellHeight;

            }

            face.transform.localPosition = new Vector3(positionX, positionY, 0);
            LabelPrafebList.Add(face);
            positionX += widget.width;
            labelLength += widget.width;
            if (ChatBG.width < (int)((sprite.transform.localPosition.x - ChatBG.transform.localPosition.x) + sprite.width + 30))
            {
                ChatBG.width = (int)((sprite.transform.localPosition.x - ChatBG.transform.localPosition.x) + sprite.width + 30);
            }
            prefabCur.Add(face);
        }
        else
        {
            CreateTextLabel(value);
        }
    }

    void SetLabelSide()
    {
        for (int i = 0; i < LabelPrafebList.Count; i++)
        {
            float number = LabelPrafebList[i].transform.localPosition.x - ChatBG.transform.localPosition.x;
            //Debug.LogError("相差:    " + number + "      " + LabelPrafebList[i].transform.localPosition.x + "    " + ChatBG.transform.localPosition.x);
            LabelReduceList.Add(number);
        }

        ChatBG.transform.localPosition = new Vector3(ChatBoardWidth - ChatBG.width - Name.width - 30, ChatBG.transform.localPosition.y);
        for (int i = 0; i < LabelPrafebList.Count; i++)
        {
            LabelPrafebList[i].transform.localPosition = new Vector3(ChatBG.transform.localPosition.x + LabelReduceList[i], LabelPrafebList[i].transform.localPosition.y);
        }
    }

    private void CreateTextLabel1(string value)
    {

        UILabel label;
        if (labelCaches.Count > 0)
        {
            label = labelCaches[0];
            labelCaches.Remove(label);
            label.gameObject.SetActive(true);
        }
        else
        {
            GameObject go = GameObject.Instantiate(Label) as GameObject;
            go.transform.parent = this.transform;
            label = go.GetComponent<UILabel>();
            go.transform.localScale = Vector3.one;
        }

        string sbstr = "";
        string text = "";

        NGUIText.finalLineHeight = label.fontSize;
        NGUIText.finalSize = label.fontSize;
        NGUIText.dynamicFont = label.trueTypeFont;
        NGUIText.regionHeight = 10000;
        NGUIText.maxLines = 10000;
        NGUIText.regionWidth = (int)(MaxLineWidth + (Name.width / 2f) - positionX);

        NGUIText.WrapText(value, out sbstr);
        //Debug.LogError(MaxLineWidth + "      " + positionX + "    " + NGUIText.regionWidth + "   ()()()()");
        int index = sbstr.IndexOf("\n");

        if (index > -1)
        {
            text = sbstr.Substring(0, index);
        }
        else
        {
            text = sbstr;
        }

        label.text = text;

        label.gameObject.transform.localPosition = new Vector3(positionX, positionY, 0);
        LabelPrafebList.Add(label.gameObject);
        positionX += label.width;
        labelLength += label.width;
        //labelCur.Add(label);

        sbstr = sbstr.Remove(0, text.Length);




        if (sbstr.Length > 0)
        {
            positionX = 12 + Name.width;
            positionY -= cellHeight;
            ChatBG.height += cellHeight;
            //ChatBG.width = (int)((label.transform.localPosition.x - ChatBG.transform.localPosition.x) + label.width + 30);

            sbstr = sbstr.Replace("\n", "");
            CreateTextLabel(sbstr);

        }

        if (ChatBG.width < (int)((label.transform.localPosition.x - ChatBG.transform.localPosition.x) + label.width + 30))
        {
            ChatBG.width = (int)((label.transform.localPosition.x - ChatBG.transform.localPosition.x) + label.width + 30);
        } 

        if (labelNameWidth == 0)
        {
            labelNameWidth = label.width;
            Invoke("SetLabelSide", 0.01f);
        }

    }*/
    #endregion
}
