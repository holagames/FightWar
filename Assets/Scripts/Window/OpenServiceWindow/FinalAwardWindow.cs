using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class FinalAwardWindow : MonoBehaviour {

    public UILabel Currentprogress;//当前进度
    public UILabel Currentcollar;//当前可领
    public UILabel Nextcollar;//下档可领
    public UILabel Reachedprogress;//已达进度
    public UILabel Needprogress;//需要达到进度
    public UILabel TimeReached;//时间

	void Start () {
        //if (UIEventListener.Get(GameObject.Find("GetButton")).onClick == null) 
        //{
        //    UIEventListener.Get(GameObject.Find("GetButton")).onClick += delegate(GameObject go)
        //    {
        //        UIManager.instance.SendMessage("9123#;");
        //    };
        //}

        //if (UIEventListener.Get(GameObject.Find("CloseButton")).onClick == null) 
        //{
        //    UIEventListener.Get(GameObject.Find("CloseButton")).onClick += delegate(GameObject go)
        //    {
        //        this.gameObject.SetActive(false);
        //    };
        //}
        if (UIEventListener.Get(GameObject.Find("AllBgMask")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("AllBgMask")).onClick += delegate(GameObject go)
            {
                this.gameObject.SetActive(false);
            };
        }
	}

    public void Getactivityfinal(int time, int CanGetFinalAward) 
    {
        //int _Currentprogress, int _Currentcollar, int _Nextcollar, int _Reachedprogress, int _Needprogress, int _TimeReached
        //Currentprogress.text = _Currentprogress.ToString() + "/120";
        int _Currentprogress = 0;
        int _Currentcollar = 0;
        int _Nextcollar = 0;
        Dictionary<int, ActivitySevenDay>.ValueCollection valueColl = TextTranslator.instance.ActivitySevenDayDic.Values;
        foreach (ActivitySevenDay _Act in valueColl)
        {
            if (_Act.CompleteState == 2) 
            {
                _Currentprogress += 1;
            }          
        }
        _Currentcollar = GetLevel(_Currentprogress);
        if (_Currentcollar != 130)
        {
            _Nextcollar = _Currentcollar + 10;
        }
        else 
        {
            _Nextcollar = 130;
        }
        Currentprogress.text = _Currentprogress.ToString() + "/130";
        Currentcollar.text = _Currentcollar.ToString();
        Nextcollar.text = _Nextcollar.ToString();
        Double dValue1 = _Currentprogress /(Double)130;
        Double dValue2 = _Nextcollar / (Double)130;
        Reachedprogress.text = "已达到进度:" + Convert.ToDouble(dValue1).ToString("P");
        Needprogress.text = "需要达到进度:" + Convert.ToDouble(dValue2).ToString("P");
        //Reachedprogress.text = "已达到进度:" + _Currentprogress.ToString() + "%";
        //Needprogress.text = "需要达到进度:" + _Nextcollar.ToString() + "%";

        Updatime((time - 86400), CanGetFinalAward);
        //if ((time - 86400 <= 0 || _Currentprogress == 130) && PlayerPrefs.GetInt("ActivityFinalReward_" + PlayerPrefs.GetString("ServerID") + "_" + PlayerPrefs.GetInt("UserID"))==0)
        //{
        //    GameObject.Find("GetButton").GetComponent<UISprite>().spriteName = "ui2_button4";
        //    GameObject.Find("GetButton").transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(211 / 255f, 88 / 255f, 13 / 255f, 255 / 255f);
        //}
        //else
        //{
        //    GameObject.Find("GetButton").GetComponent<UISprite>().spriteName = "buttonHui";
        //    GameObject.Find("GetButton").transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(150 / 255f, 150 / 255f, 150 / 255f, 255 / 255f);
        //}
        if (CanGetFinalAward == 1)
        {
            transform.Find("FinalScrollView/Allparent/GetButton").GetComponent<UISprite>().spriteName = "ui2_button4";
            transform.Find("FinalScrollView/Allparent/GetButton").transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(211 / 255f, 88 / 255f, 13 / 255f, 255 / 255f);
        }
        else
        {
            transform.Find("FinalScrollView/Allparent/GetButton").GetComponent<UISprite>().spriteName = "buttonHui";
            transform.Find("FinalScrollView/Allparent/GetButton").transform.Find("Label").GetComponent<UILabel>().effectColor = new Color(150 / 255f, 150 / 255f, 150 / 255f, 255 / 255f);
        }
        if (UIEventListener.Get(transform.Find("FinalScrollView/Allparent/GetButton").gameObject).onClick == null)
        {
            UIEventListener.Get(transform.Find("FinalScrollView/Allparent/GetButton").gameObject).onClick += delegate(GameObject go)
            {
                if (CanGetFinalAward==1)
                {
                    NetworkHandler.instance.SendProcess("9123#;");
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("活动需要在第7天结束后或者完成所有任务才可领取奖励！", PromptWindow.PromptType.Hint, null, null);
                }
            };
        }
        //if (UIEventListener.Get(GameObject.Find("GetButton")).onClick == null)
        //{
        //    UIEventListener.Get(GameObject.Find("GetButton")).onClick += delegate(GameObject go)
        //    {
        //        if (time-86400 <= 0 || _Currentprogress == 130)
        //        {
        //            NetworkHandler.instance.SendProcess("9123#;");
        //        }
        //        else 
        //        {
        //            UIManager.instance.OpenPromptWindow("活动需要在第7天结束后或者完成所有任务才可领取奖励！", PromptWindow.PromptType.Hint, null, null);
        //        }
        //    };
        //}
        if (UIEventListener.Get(transform.Find("CloseAwardButton").gameObject).onClick == null)
        {
            UIEventListener.Get(transform.Find("CloseAwardButton").gameObject).onClick += delegate(GameObject go)
            {
                this.gameObject.SetActive(false);
            };
        }
    }
    int GetLevel(int num) 
    {
        if (num < 10) { num=0;}
        else if (num >= 10 && num < 20) { num = 10; }
        else if (num >= 20 && num < 30) { num = 20; }
        else if (num >= 30 && num < 40) { num = 30; }
        else if (num >= 40 && num < 50) { num = 40; }
        else if (num >= 50 && num < 60) { num = 50; }
        else if (num >= 60 && num < 70) { num = 60; }
        else if (num >= 70 && num < 80) { num = 70; }
        else if (num >= 80 && num < 90) { num = 80; }
        else if (num >= 90 && num < 100) { num = 90; }
        else if (num >= 100 && num < 110) { num = 100; }
        else if (num >= 110 && num < 120) { num = 110; }
        else if (num >= 120 && num < 130) { num = 120; }
        else if (num == 130) { num = 130; }
        return num;
    }
    void Updatime(int Time, int CanGetFinalAward) 
    {
        if (CanGetFinalAward == 0) 
        {
            if (Time >= 86400)
            {
                int dayStr = Time / 86400;
                int houreStr = Time % 86400 / 3600;
                int miniteStr = Time % 3600 / 60;
                TimeReached.text = dayStr.ToString() + "天" + houreStr.ToString() + "小时" + miniteStr.ToString() + "分后可领取";
            }
            else if (Time > 0 && Time < 86400)
            {
                int houreStr = Time / 3600;
                int miniteStr = Time % 3600 / 60;
                TimeReached.text = houreStr.ToString() + "小时" + miniteStr.ToString() + "后可领取";
            }
            else
            {
                TimeReached.text = "现在可领取";
            }
        }
        else if (CanGetFinalAward == 1) 
        {
            TimeReached.text = "现在可领取";
        }
        else if (CanGetFinalAward == 2) 
        {
            TimeReached.text = "已领取";
        }
    }
}
