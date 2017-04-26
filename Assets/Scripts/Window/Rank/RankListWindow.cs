using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RankListWindow : MonoBehaviour
{
    public List<GameObject> partsList = new List<GameObject>();

    public GameObject uiGrid;
    public GameObject itemPrefab;
    [SerializeField]
    private GameObject lookInfoButton;
    public UIAtlas FightSprite;
    public UIAtlas LevelRankSprite;
    [SerializeField]
    private UILabel firstName;
    [SerializeField]
    private UILabel firstLv;
    [SerializeField]
    private UILabel firstGroupName;
    [SerializeField]
    private UISprite firstIcon;
    private List<GameObject> RankListObj = new List<GameObject>();
    private int rankType;


    public GameObject Message5PartInfo;
    public UILabel ChangeLevelLabel;//改变等级文字变换
    void OnEnable()
    {
        if (CharacterRecorder.instance.LegionRankListButtonType == 4)
        {
            CharacterRecorder.instance.LegionRankListButtonType = 1;
            GameObject.Find("Tab4Button").GetComponent<UIToggle>().value = true;
            GameObject.Find("Tab4Button").GetComponent<UIToggle>().startsActive = true;
            GameObject.Find("Tab1Button").GetComponent<UIToggle>().value = false;
            Button_LegionWarOnClick();
        }
        else 
        {
            Button_FightRankOnClick();
        }
    }

    // Use this for initialization
    void Start()
    {
      /*if (UIEventListener.Get(GameObject.Find("ListCloseButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("ListCloseButton")).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }*/
        UIEventListener.Get(GameObject.Find("Tab1Button")).onClick += delegate(GameObject go)
        {
            Button_FightRankOnClick();
        };
        UIEventListener.Get(GameObject.Find("Tab2Button")).onClick += delegate(GameObject go)
        {
            Button_LevelRankOnClick();
        };
        UIEventListener.Get(GameObject.Find("Tab3Button")).onClick += delegate(GameObject go)
        {
            Button_LegionRankOnClick();
        };
        UIEventListener.Get(GameObject.Find("Tab4Button")).onClick += delegate(GameObject go)
        {
            Button_LegionWarOnClick();
        };
        UIEventListener.Get(lookInfoButton).onClick += delegate(GameObject go)
        {
            if (go.name != "0") 
            {
                Button_LookInfoButtonOnClick(go.name);
            }
        };
    }

    public void CreatItem(string RankType,string RankNumber, string uId, string Name, string HeroIcon, string HeroLevel, string PowerNumber, string JiFen, string sumPraise,string LegionName)
    {
        GameObject obj = NGUITools.AddChild(uiGrid, itemPrefab);
        obj.SetActive(true);
        obj.GetComponent<RankListItem>().SetInfo(RankType,RankNumber, uId, Name, HeroIcon, HeroLevel, PowerNumber);
        uiGrid.GetComponent<UIGrid>().Reposition();
        RankListObj.Add(obj);
    }
    public void SetFirstInfo(string RankNumber, string uId, string Name, string HeroIcon, string HeroLevel, string PowerNumber, string JiFen, string sumPraise,string LegionName)
    {
        lookInfoButton.name = uId;
        firstName.text = Name;
        if (LegionName == "") 
        {
            firstGroupName.text = "无";
        }
        else
        {
            firstGroupName.text = LegionName;
        }
        firstLv.text = HeroLevel.ToString();
        firstIcon.spriteName = HeroIcon;
        Message5PartInfo.SetActive(false);
    }

    public void SetFirstInfo()
    {
        lookInfoButton.name = "0";
        firstName.text = "???";
        firstGroupName.text = "???";//JiFen;
        firstLv.text = "???";
        firstIcon.spriteName = "???";
        Message5PartInfo.SetActive(true);
    }
    public void ShowMyRank(int MyRankType)
    {
        rankType = MyRankType;
        Transform MyRankNumber = GameObject.Find("MyRankNumber").transform.Find("Sprite").transform;
        string _RankNumberStr = "";
        switch (CharacterRecorder.instance.RankNumber)
        {
            case 0: _RankNumberStr = "未上榜"; break;
            default: _RankNumberStr = CharacterRecorder.instance.RankNumber.ToString(); break;
        }
        MyRankNumber.Find("CurMyRankNumber").GetComponent<UILabel>().text = _RankNumberStr;
        if (MyRankType == 3)
        {
            ChangeLevelLabel.text = "等级";
            MyRankNumber.Find("PlayerName").GetComponent<UILabel>().text = CharacterRecorder.instance.characterName;
            MyRankNumber.Find("PlayerLv").GetComponent<UILabel>().text = CharacterRecorder.instance.level.ToString();
            MyRankNumber.Find("Sprite").transform.Find("Label").GetComponent<UILabel>().text = CharacterRecorder.instance.Fight.ToString();
            GameObject SpriteEffect = MyRankNumber.Find("Sprite").transform.Find("SpriteEffect").gameObject;
            UISprite _UISprite = MyRankNumber.Find("Sprite").transform.Find("Sprite").GetComponent<UISprite>();
            _UISprite.atlas = LevelRankSprite;
            _UISprite.spriteName = "force";
            _UISprite.MakePixelPerfect();
            SpriteEffect.SetActive(true);
        }
        else if (MyRankType == 4)
        {
            ChangeLevelLabel.text = "等级";
            MyRankNumber.Find("PlayerName").GetComponent<UILabel>().text = CharacterRecorder.instance.characterName;
            MyRankNumber.Find("PlayerLv").GetComponent<UILabel>().text = CharacterRecorder.instance.level.ToString();
            MyRankNumber.Find("Sprite").transform.Find("Label").GetComponent<UILabel>().text = CharacterRecorder.instance.level.ToString();
            GameObject SpriteEffect = MyRankNumber.Find("Sprite").transform.Find("SpriteEffect").gameObject;
            UISprite _UISprite = MyRankNumber.Find("Sprite").transform.Find("Sprite").GetComponent<UISprite>();
            _UISprite.atlas = LevelRankSprite;
            _UISprite.spriteName = "33_2_ICON ";
            _UISprite.MakePixelPerfect();
            SpriteEffect.SetActive(false);
        }
        else if (MyRankType == 5) 
        {
            ChangeLevelLabel.text = "杀敌数";
            MyRankNumber.Find("PlayerName").GetComponent<UILabel>().text = CharacterRecorder.instance.characterName;
            MyRankNumber.Find("PlayerLv").GetComponent<UILabel>().text = CharacterRecorder.instance.level.ToString();
            MyRankNumber.Find("Sprite").transform.Find("Label").GetComponent<UILabel>().text = CharacterRecorder.instance.Fight.ToString();
            GameObject SpriteEffect = MyRankNumber.Find("Sprite").transform.Find("SpriteEffect").gameObject;
            UISprite _UISprite = MyRankNumber.Find("Sprite").transform.Find("Sprite").GetComponent<UISprite>();
            _UISprite.atlas = LevelRankSprite;
            _UISprite.spriteName = "force";
            _UISprite.MakePixelPerfect();
            SpriteEffect.SetActive(true);
        }
    }
    public void ShowMyRankNumber(string RankNumber) 
    {
        Transform MyRankNumber = GameObject.Find("MyRankNumber").transform.Find("Sprite").transform;
        MyRankNumber.Find("CurMyRankNumber").GetComponent<UILabel>().text = RankNumber;
    }
    private void Button_LevelRankOnClick()
    {
        partsList[0].SetActive(true);
        partsList[1].SetActive(false);
        if (RankListObj.Count != 0) 
        {
            for (int i = 0; i < RankListObj.Count; i++) 
            {
                DestroyImmediate(RankListObj[i]);
                
            }
        }
        RankListObj.Clear();
        NetworkHandler.instance.SendProcess("6004#4;");
       // ShowMyRank(4);
    }
    private void Button_FightRankOnClick()
    {
        partsList[0].SetActive(true);
        partsList[1].SetActive(false);
        if (RankListObj.Count != 0)
        {
            for (int i = 0; i < RankListObj.Count; i++)
            {
                DestroyImmediate(RankListObj[i]);

            }
        }
        RankListObj.Clear();
        NetworkHandler.instance.SendProcess("6004#3;");
       // ShowMyRank(3);
    }

    private void Button_LegionWarOnClick()
    {
        partsList[0].SetActive(true);
        partsList[1].SetActive(false);
        if (RankListObj.Count != 0)
        {
            for (int i = 0; i < RankListObj.Count; i++)
            {
                DestroyImmediate(RankListObj[i]);

            }
        }
        RankListObj.Clear();
        NetworkHandler.instance.SendProcess("8627#;");
        // ShowMyRank(3);
    }

    private void Button_LegionRankOnClick()
    {
        NetworkHandler.instance.SendProcess("8010#0;");
        partsList[0].SetActive(false);
        partsList[1].SetActive(true);
    }
    private void Button_LookInfoButtonOnClick(string _uId)
    {
        //Debug.LogError("查看资料。。。");
        //UIManager.instance.OpenPanel("FightListWindow", true);
        //UIManager.instance.OpenPanel("LookInfoWindow", false);
        //NetworkHandler.instance.SendProcess(string.Format("6008#{0};{1};", _uId, rankType));
        NetworkHandler.instance.SendProcess("1020#" + _uId + ";");
        UIManager.instance.OpenPanel("LegionMemberItemDetail", false); 
    }
}
