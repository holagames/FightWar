using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SettingLimitBord : MonoBehaviour 
{
    public UILabel Label1;
    public UILabel Label2;
    public GameObject closeButton;
    public GameObject cancleButton;
    public GameObject sureButton;
    public GameObject leftButton1;
    public GameObject leftButton2;
    public GameObject rightButton1;
    public GameObject rightButton2;
    private List<string> stringList1 = new List<string>();
    private List<string> stringList2Two = new List<string>();
    private int index1 = 0;
    private int index2 = 0;
    private int stateValue1;
    private int stateValue2;
	// Use this for initialization
	void Start () 
    {
        InitStringList();
        
        if (UIEventListener.Get(closeButton).onClick == null)
        {
            UIEventListener.Get(closeButton).onClick += delegate(GameObject go)
            {
                this.gameObject.SetActive(false);
            };
        }
        if (UIEventListener.Get(cancleButton).onClick == null)
        {
            UIEventListener.Get(cancleButton).onClick += delegate(GameObject go)
            {
                this.gameObject.SetActive(false);
            };
        }
        if (UIEventListener.Get(sureButton).onClick == null)
        {
            UIEventListener.Get(sureButton).onClick += delegate(GameObject go)
            {
                switch (index1)
                {
                    case 0: this.stateValue1 = 1; break;
                    case 1: this.stateValue1 = 0; break;
                    case 2: this.stateValue1 = -1; break;
                    default: this.stateValue1 = 1; break;
                }
                switch (index2)
                {
                    case 0: this.stateValue2 = 0; break;
                    default: this.stateValue2 = index2 + 23; break;
                }
                NetworkHandler.instance.SendProcess(string.Format("8017#{0}_{1};", this.stateValue1, this.stateValue2));
                this.gameObject.SetActive(false);
            };
        }
        if (UIEventListener.Get(leftButton1).onClick == null)
        {
            UIEventListener.Get(leftButton1).onClick += delegate(GameObject go)
            {
                //Debug.LogError("this index1 = " + this.index1);
                if (this.index1 > 0)
                {
                    this.index1 -= 1;
                }
                else
                {
                    this.index1 = stringList1.Count - 1;
                }
                Label1.text = FindTargetStringToShow(stringList1, this.index1);
            };
        }
        if (UIEventListener.Get(leftButton2).onClick == null)
        {
            UIEventListener.Get(leftButton2).onClick += delegate(GameObject go)
            {
               // Debug.LogError("this index2 = " + this.index2);
                if (this.index2 > 0)
                {
                    this.index2 -= 1;
                }
                else
                {
                    this.index2 = stringList2Two.Count - 1;
                }
                Label2.text = FindTargetStringToShow(stringList2Two, this.index2);
            };
        }
        if (UIEventListener.Get(rightButton1).onClick == null)
        {
            UIEventListener.Get(rightButton1).onClick += delegate(GameObject go)
            {
                //Debug.LogError("this index1 = " + this.index1);
                if (this.index1 < stringList1.Count - 1)
                {
                    this.index1 += 1;
                }
                else
                {
                    this.index1 = 0;
                }
                Label1.text = FindTargetStringToShow(stringList1, this.index1);
            };
        }
        if (UIEventListener.Get(rightButton2).onClick == null)
        {
            UIEventListener.Get(rightButton2).onClick += delegate(GameObject go)
            {
                //Debug.LogError("this index2 = " + this.index2);
                if (this.index2 < stringList2Two.Count - 1)
                {
                    this.index2 += 1;
                }
                else
                {
                    this.index2 = 0;
                }
                Label2.text = FindTargetStringToShow(stringList2Two, this.index2);
            };
        }
	}
    public void SetSettingLimitBord(int stateValue1,int stateValue2)
    {
        InitStringList();
        int index1; 
        int index2;
        switch (stateValue1)
        {
            case 1: index1 = 0; break;
            case 0: index1 = 1; break;
            case -1: index1 = 2; break;
            default: index1 = 0; break;
        }
        switch (stateValue2)
        {
            case 0: index2 = 0; break;
            default: index2 = stateValue2 - 23; break;
        }
        this.stateValue1 = stateValue1;
        this.stateValue2 = stateValue2;
        this.index1 = index1;
        this.index2 = index2;
        Label1.text = FindTargetStringToShow(stringList1, this.index1);
        Label2.text = FindTargetStringToShow(stringList2Two, this.index2);
    }
    void ClickLeftOrRightButton(int _leftOrRightButton, int _controlCurIndex) //_leftOrRightButton 0 - left,1- right; 
    {
 
    }
    string FindTargetStringToShow(List<string> stringList, int index)
    {
        //Debug.LogError(stringList.Count);
        for (int i = 0; i < stringList.Count; i++)
        {
            //Debug.Log(stringList[i]);
            if (i == index)
            {
                return stringList[i];
            }
        }
        return "";
    }
    void InitStringList()
    {
        stringList1.Clear();
        stringList1.Add("申请自动加入"); // this.index1 == 0     stateValue1  1
        stringList1.Add("需审核才能加入"); // this.index1 == 1     stateValue1  0
        stringList1.Add("禁止任何人加入"); // this.index1 == 2     stateValue1  -1
        stringList2Two.Clear();
        stringList2Two.Add("无限制");     //this.index2 == 0          stateValue2   0
        for (int i = 24; i <= 70;i++ )
        {
            stringList2Two.Add(i.ToString() + "级"); //                  stateValue2   i
        }

        Label1.text = FindTargetStringToShow(stringList1, this.index1);
        Label2.text = FindTargetStringToShow(stringList2Two, this.index2);
    }
	
}
