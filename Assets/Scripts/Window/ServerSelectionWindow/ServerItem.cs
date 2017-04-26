using UnityEngine;
using System.Collections;
using System;

public class ServerItem : MonoBehaviour {

    public UILabel SeverName;
    public GameObject ServerType;
    public GameObject ServersSate;
    public GameObject HeroItem;

    private string ServerTag;//服务器标签
    private int stateNum;//服务器状态
    ServerList _OneServerItem;

	void Start () {
        if (UIEventListener.Get(this.gameObject).onClick == null) 
        {
            UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
            {
                Debug.Log("bbbbbbbbb");
                if (GameObject.Find("LoginWindow") != null)
                {
                    //GameObject.Find("LoginWindow").GetComponent<LoginWindow>().ServerId = ServerId;
                    GameObject.Find("LoginWindow").GetComponent<LoginWindow>().SetServerId(ServerTag);
                }
                UIManager.instance.BackUI();

                //if (CompareTiem())
                //{
                //    if (GameObject.Find("LoginWindow") != null)
                //    {
                //        //GameObject.Find("LoginWindow").GetComponent<LoginWindow>().ServerId = ServerId;
                //        GameObject.Find("LoginWindow").GetComponent<LoginWindow>().SetServerId(ServerTag);
                //    }
                //    UIManager.instance.BackUI();
                //}
                //else 
                //{
                //    GameObject parent = GameObject.Find("UIRoot");
                //    //UIManager.instance.OpenPromptWindow("未到开服时间", PromptWindow.PromptType.Hint, null, null);
                //    //GameObject obj = NGUITools.AddChild(parent, Resources.Load("Prefab/Effect/LabelEffect"));
                //    GameObject obj = (GameObject)Instantiate(GameObject.Find("ServerSelectionWindow").transform.Find("All").transform.Find("Effect").gameObject);
                //    Transform t = obj.transform;
                //    t.parent = parent.transform;
                //    t.localRotation = Quaternion.identity;
                //    t.localScale = Vector3.one;
                //    obj.SetActive(true);
                //    obj.GetComponent<UILabelEffect>().SetLabelNormal("未到开服时间", new Vector3(0, 180, 0), new Vector3(0, 320, 0));
                //    obj.AddComponent<DestroySelf>();
                //}
            };
        }
	}

    public void SetInfo(ServerList ServerItem) //,string heroinfo
    {
        this._OneServerItem = ServerItem;
        this.ServerTag = ServerItem.ServerTag;
        int _Type = ServerItem.Type;
        int _state = ServerItem.State;

        SeverName.text = ServerItem.ServerName;
        if (_state == 1) 
        {
            ServersSate.GetComponent<UISprite>().spriteName = "Serverguang1";
        }
        else if (_state == 2) 
        {
            ServersSate.GetComponent<UISprite>().spriteName = "Serverguang2";
        }
        else if (_state == 3)
        {
            ServersSate.GetComponent<UISprite>().spriteName = "Serverguang3";
        }

        if (_Type == 1) 
        {
            ServerType.GetComponent<UISprite>().spriteName = "Servericon1";
        }
        else if (_Type == 2) 
        {
            ServerType.GetComponent<UISprite>().spriteName = "Servericon2";
        }
        else if (_Type == 3)
        {
            ServerType.GetComponent<UISprite>().spriteName = "Servericon3";
        }
        else if (_Type == 4) 
        {
            ServerType.GetComponent<UISprite>().spriteName = "Servericon4";
        }
        else if (_Type == 5)
        {
            ServerType.GetComponent<UISprite>().spriteName = "Servericon5";
        }


    }

    private bool CompareTiem() //是否到了开服时间
    {
        string timeStr1 = _OneServerItem.OpenDate;
        DateTime timeOpen = Convert.ToDateTime(timeStr1);
        if (timeOpen > Downloader.instance.NowTime)
        {
            Debug.Log("时间未到");
            Debug.Log("开服时间"+timeOpen);
            Debug.Log("现在时间"+Downloader.instance.NowTime);
            return false;
        }
        else 
        {
            Debug.Log("时间到了");
            Debug.Log("开服时间" + timeOpen);
            Debug.Log("现在时间" + Downloader.instance.NowTime);
            return true;
        }      
    }
}
