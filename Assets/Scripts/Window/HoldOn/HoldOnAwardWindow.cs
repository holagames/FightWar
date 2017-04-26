using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class HoldOnAwardWindow : MonoBehaviour
{
    [SerializeField]
    private GameObject closeButton;
    [SerializeField]
    private GameObject sureButton;
    [SerializeField]
    private GameObject MyGrid;
    [SerializeField]
    private GameObject LoginAwardItem;


    [SerializeField]
    private GameObject TopTextureOne;
    [SerializeField]
    private GameObject TopTextureTwo;
    [SerializeField]
    private GameObject BottomLabelOne;
    [SerializeField]
    private GameObject BottomLabelTwo;

    public UITexture SpriterBg;
    public UILabel TimeLabel;
    public UILabel TimeCount;
    public UILabel ExpCount;
    public UILabel LvInfoLabel;
    public UISlider ExpSlider;
    public UILabel ExpSliderLabel;

    public static int GetExpNum = 0;

    private List<GameObject> mList = new List<GameObject>();
    private int _index = 0;

    private int OnLineTimeCount = 0;

    private bool isOnline = false;

    // Use this for initialization
    void Start()
    {
        //NetworkHandler.instance.SendProcess("1903#;");
        if (UIEventListener.Get(closeButton).onClick == null)
        {
            UIEventListener.Get(closeButton).onClick += delegate(GameObject go)
            {
                DestroyImmediate(this.gameObject);
            };
        }
        if (UIEventListener.Get(sureButton).onClick == null)
        {
            UIEventListener.Get(sureButton).onClick += delegate(GameObject go)
            {
                DestroyImmediate(this.gameObject);
            };
        }
    }
    /// <summary>
    /// 设置经验条的信息
    /// </summary>
    public void SetSliderInfo()
    {
        LvInfoLabel.text = string.Format("Lv.{0}", CharacterRecorder.instance.level.ToString());
        ExpSliderLabel.text = CharacterRecorder.instance.exp.ToString() + "/" + CharacterRecorder.instance.expMax.ToString();
        ExpSlider.value = CharacterRecorder.instance.exp * 1.0f / CharacterRecorder.instance.expMax;
    }
    /// <summary>
    /// 设置时间信息
    /// </summary>
    /// <param name="sec"></param>
    private void SetTimeInfo(int sec)
    {
        int hourCount = sec / 3600;
        int minCount = sec % 3600 / 60;
        int secCount = sec % 60;
        if (hourCount == 0)
        {
            if (minCount == 0)
            {
                TimeCount.text = string.Format("{0}秒", secCount);
            }
            else
            {
                TimeCount.text = string.Format("{0}分{1}秒", minCount, secCount);
            }
        }
        else
        {
            TimeCount.text = string.Format("{0}时{1}分{2}秒", hourCount, minCount, secCount);
        }
    }
    /// <summary>
    /// 更新在线时间
    /// </summary>
    void UpTime()
    {
        OnLineTimeCount += 1;
        SetTimeInfo(OnLineTimeCount);
    }

    /// <summary>
    /// 取得挂机奖励
    /// </summary>
    /// <param name="_MyList">在线奖励</param>
    /// <param name="exp">挂机经验</param>
    /// <param name="time">挂机时长</param>
    /// <param name="name">名字</param>
    public void SetHoldOnAwardWindow(List<Item> _MyList, int exp, int time, string name)
    {
        //TopTextureOne.SetActive(true);
        //TopTextureTwo.SetActive(false);
        //BottomLabelOne.SetActive(true);
        //BottomLabelTwo.SetActive(false);

        SpriterBg.mainTexture = Resources.Load("Game/guajishouyi1", typeof(Texture)) as Texture;// Resources.Load("Game/guajishouyi1") as Texture;

        ExpCount.text = string.Format("{0}", exp);
        GetExpNum = exp;
        if (time != -1)
        {
            OnLineTimeCount = time;
        }
        isOnline = true;
        TimeLabel.text = "[ffd907]挂机时长";
        SetTimeInfo(OnLineTimeCount);
        InvokeRepeating("UpTime", 0, 1.0f);

        SetSliderInfo();

        ClearUIGride();
        for (int i = 0; i < _MyList.Count; i++)
        {
            if (_MyList[i].itemCode != 0)
            {
                GameObject go = NGUITools.AddChild(MyGrid, LoginAwardItem);
                go.name = _MyList[i].itemCode.ToString();
                go.GetComponent<LoginAwardItem>().SetLoginAwardItem(_MyList[i]);
                TextTranslator.instance.ItemDescription(go, _MyList[i].itemCode, 0);
            }
        }
        MyGrid.GetComponent<UIGrid>().Reposition();
    }

    public void SetExp_1902(int exp)
    {
        if (isOnline)
        {
            ExpCount.text = string.Format("{0}", exp);           
        }
        SetSliderInfo();
    }
    void ClearUIGride()
    {
        for (int i = MyGrid.transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(MyGrid.transform.GetChild(i).gameObject);
        }
        mList.Clear();
    }

    /// <summary>
    /// 离线信息
    /// </summary>
    /// <param name="_MyList">离线奖励</param>
    /// <param name="time">离线时常</param>
    /// <param name="exp">离线经验</param>
    public void SetHoldOnAwardWindow(List<Item> _MyList, int time, int exp)
    {
        //TopTextureOne.SetActive(false);
        //TopTextureTwo.SetActive(true);
        //BottomLabelOne.SetActive(false);
        //BottomLabelTwo.SetActive(true);

        //string houreStr = (time / 3600).ToString("00");
        //string miniteStr = (time % 3600 / 60).ToString("00");
        //string secondStr = (time % 60).ToString("00");
        //BottomLabelTwo.GetComponent<UILabel>().text= string.Format("{0}:{1}:{2}", houreStr, miniteStr, secondStr);

        SpriterBg.mainTexture = Resources.Load("Game/lixianshouyi2", typeof(Texture)) as Texture;

        TimeLabel.text = "[d4d6d8]离线时长";

        ExpCount.text = string.Format("{0}", exp);

        SetTimeInfo(time);

        SetSliderInfo();

        ClearUIGride();

        List<Item> _itemList = new List<Item>();
        for (int i = 0; i < _MyList.Count; i++)
        {
            if (IsAwardListAreadyContainsThisCode(_itemList, _MyList[i].itemCode))
            {
                _itemList[_index] = new Item(_MyList[i].itemCode, _itemList[_index].itemCount + _MyList[i].itemCount);
            }
            else
            {
                _itemList.Add(new Item(_MyList[i].itemCode, _MyList[i].itemCount));
            }


            //if (_MyList[i].itemCode != 0)
            //{
            //    GameObject go = NGUITools.AddChild(MyGrid, LoginAwardItem);
            //    go.name = _MyList[i].itemCode.ToString();
            //    go.GetComponent<LoginAwardItem>().SetLoginAwardItem(_MyList[i]);
            //    TextTranslator.instance.ItemDescription(go, _MyList[i].itemCode, 0);
            //}
        }

        for (int i = 0; i < _itemList.Count; i++)
        {
            GameObject go = NGUITools.AddChild(MyGrid, LoginAwardItem);
            go.name = _itemList[i].itemCode.ToString();
            go.GetComponent<LoginAwardItem>().SetLoginAwardItem(_itemList[i]);
            TextTranslator.instance.ItemDescription(go, _itemList[i].itemCode, 0);
        }


        MyGrid.GetComponent<UIGrid>().Reposition();
    }
    bool IsAwardListAreadyContainsThisCode(List<Item> _itemList, int _code)
    {
        for (int i = 0; i < _itemList.Count; i++)
        {
            if (_itemList[i].itemCode == _code)
            {
                _index = i;
                return true;
            }
        }
        return false;
    }
}
