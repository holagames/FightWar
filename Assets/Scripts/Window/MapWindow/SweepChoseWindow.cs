using UnityEngine;
using System.Collections;

public class SweepChoseWindow : MonoBehaviour {

    public UILabel SellCount;
    public UILabel AblumNum;
    public UILabel SweepNumber;
    public GameObject CutTenButton;
    public GameObject CutOneButton;
    public GameObject AddOneButton;
    public GameObject AddTenButton;
    public GameObject SureButton;

    public UILabel MessageLabel;

    int MapGateID;
    int Num;//体力消耗6，12;
    int SweepNum = 1;
    int MaxNum;//最大次数

    void Start()
    {
        if (UIEventListener.Get(CutTenButton).onClick == null)
        {
            UIEventListener.Get(CutTenButton).onClick += delegate(GameObject go)
            {
                if (MapGateID > 20000)
                {
                    if (SweepNum > 3)
                    {
                        SweepNum = SweepNum - 3;
                    }
                    else
                    {
                        SweepNum = 1;
                    }
                }
                else
                {
                    if (SweepNum > 10)
                    {
                        SweepNum = SweepNum - 10;
                    }
                    else
                    {
                        SweepNum = 1;
                    }
                }
                AblumNum.text = (SweepNum * Num).ToString();
                SweepNumber.text = SweepNum.ToString();
            };
        }
        if (UIEventListener.Get(CutOneButton).onClick == null)
        {
            UIEventListener.Get(CutOneButton).onClick += delegate(GameObject go)
            {
                if (SweepNum > 1)
                {
                    SweepNum = SweepNum - 1;
                }
                else
                {
                    SweepNum = 1;
                }
                AblumNum.text = (SweepNum * Num).ToString();
                SweepNumber.text = SweepNum.ToString();
                //if (MapGateID > 20000)
                //{
                //    SweepNum = GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().ChallengeCount;
                //}
                //else
                //{

                //}
                //UiSlider.value = 1f;
            };
        }
        if (UIEventListener.Get(AddOneButton).onClick == null)
        {
            UIEventListener.Get(AddOneButton).onClick += delegate(GameObject go)
            {
                if (SweepNum < MaxNum)
                {
                    SweepNum = SweepNum +1;
                }
                else
                {
                    SweepNum = MaxNum;
                }
                AblumNum.text = (SweepNum * Num).ToString();
                SweepNumber.text = SweepNum.ToString();
            };
        }
        if (UIEventListener.Get(AddTenButton).onClick == null)
        {
            UIEventListener.Get(AddTenButton).onClick += delegate(GameObject go)
            {
                if (MapGateID > 20000)
                {
                    if (SweepNum <= MaxNum - 3)
                    {
                        SweepNum = SweepNum + 3;
                    }
                    else
                    {
                        SweepNum = MaxNum;
                    }
                }
                else 
                {
                    if (SweepNum <= MaxNum - 10)
                    {
                        SweepNum = SweepNum + 10;
                    }
                    else
                    {
                        SweepNum = MaxNum;
                    }
                }
                AblumNum.text = (SweepNum * Num).ToString();
                SweepNumber.text = SweepNum.ToString();
            };
        }

        if (UIEventListener.Get(SureButton).onClick == null)
        {
            UIEventListener.Get(SureButton).onClick += delegate(GameObject go)
            {
                //SweepNum = int.Parse(SellCount.text);
                if (MapGateID > 20000)
                {
                    if (SweepNum > MaxNum)
                    {
                        UIManager.instance.OpenPromptWindow("扫荡次数不足", PromptWindow.PromptType.Alert, null, null);
                    }
                    else if (SweepNum * 12 > CharacterRecorder.instance.stamina)
                    {
                        //UIManager.instance.OpenPromptWindow("体力不足", PromptWindow.PromptType.Alert, null, null);
                        string resetMessage = "体力不足，是否购买体力药剂";
                        UIManager.instance.OpenPromptWindow(resetMessage, PromptWindow.PromptType.Confirm, ResetBtnClick, null);
                    }
                    else
                    {
                        GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().SweepNum = SweepNum;
                        NetworkHandler.instance.SendProcess("2012#" + MapGateID + ";" + SweepNum + ";");
                        this.gameObject.SetActive(false);
                    }
                }
                else
                {
                    if (SweepNum * 6 > CharacterRecorder.instance.stamina)
                    {
                        string resetMessage = "体力不足，是否购买体力药剂";
                        UIManager.instance.OpenPromptWindow(resetMessage, PromptWindow.PromptType.Confirm, ResetBtnClick, null);
                    }
                    else 
                    {
                        NetworkHandler.instance.SendProcess("2012#" + MapGateID + ";" + SweepNum + ";");
                        this.gameObject.SetActive(false);
                    }
                }
            };
        }

        if (UIEventListener.Get(GameObject.Find("SweepChoseNum/Mask")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("SweepChoseNum/Mask")).onClick += delegate(GameObject go)
            {
                this.gameObject.SetActive(false);
            };
        }
    }


    void ResetBtnClick() 
    {
        //UIManager.instance.OpenSinglePanel("EmployPropsWindow", false);
        NetworkHandler.instance.SendProcess("5012#10602;");
    }
    public void GetMapId(int ID)
    {
        this.MapGateID = ID;
        if (MapGateID < 20000)
        {
            CutTenButton.transform.Find("Label").GetComponent<UILabel>().text = "-10";
            AddTenButton.transform.Find("Label").GetComponent<UILabel>().text = "+10";
            this.Num = 6;
            this.MaxNum = CharacterRecorder.instance.stamina / Num;
            this.SweepNum = 1;
            SweepNumber.text = "1";
            AblumNum.text = Num.ToString();
            MessageLabel.gameObject.SetActive(false);
        }
        else
        {
            CutTenButton.transform.Find("Label").GetComponent<UILabel>().text = "-3";
            AddTenButton.transform.Find("Label").GetComponent<UILabel>().text = "+3";
            this.Num = 12;
            this.MaxNum = GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().ChallengeCount;
            this.SweepNum = 1;
            SweepNumber.text = "1";
            AblumNum.text = Num.ToString();
            MessageLabel.gameObject.SetActive(true);
        }
    }


/*    public UILabel SellCount;
    public UILabel AblumNum;
    public UISlider UiSlider;
    int MapGateID;
    int Num;//体力消耗6，12;
    int SweepNum = 1;
    int MaxNum;//最大次数
	void Start () {
        //UiSlider = gameObject.transform.FindChild("SpriteSlider").GetComponent<UISlider>();
        //SellCount.text = "1";
        //AblumNum.text = (SweepNum * Num).ToString();
        //UiSlider.value = 0;
        if (UIEventListener.Get(GameObject.Find("MinButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("MinButton")).onClick += delegate(GameObject go)
            {
                //if (MapGateID > 20000)
                //{
                //    SweepNum = GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().ChallengeCount;
                //    if (SweepNum != 0)
                //    {
                //        UiSlider.value = 0.25f;
                //    }
                //    else 
                //    {
                //        UiSlider.value = 0;
                //    }
                //}
                //else
                //{
                //    //UiSlider.value = 0.1f;
                //    //SweepNum = 1;
                //    //SellCount.text = SweepNum.ToString(); ;
                //    //AblumNum.text = (SweepNum * Num).ToString();
                //    UiSlider.value = 0;//((float)1 / MaxNum);
                //}
                //UiSlider.value = 0;
                UiSlider.value = 0;
            };
        }
        if (UIEventListener.Get(GameObject.Find("MaxButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("MaxButton")).onClick += delegate(GameObject go)
            {
                if (MapGateID > 20000)
                {
                    SweepNum = GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().ChallengeCount;
                    UiSlider.value = (SweepNum-1) * 0.5f;
                }
                else
                {
                    UiSlider.value = 1f;
                }
                //UiSlider.value = 1f;
            };
        }

        if (UIEventListener.Get(GameObject.Find("SureButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("SureButton")).onClick += delegate(GameObject go)
            {
                SweepNum = int.Parse(SellCount.text);
                if (MapGateID > 20000)
                {
                    Debug.Log("挑战次数xxx   " + SweepNum);
                    if (SweepNum > MaxNum)
                    {
                        UIManager.instance.OpenPromptWindow("扫荡次数不足", PromptWindow.PromptType.Alert, null, null);
                    }
                    else if (SweepNum * 12 > CharacterRecorder.instance.stamina)
                    {
                        UIManager.instance.OpenPromptWindow("体力不足", PromptWindow.PromptType.Alert, null, null);
                    }
                    else
                    {
                        GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().SweepNum = SweepNum;
                        Debug.Log("每次挑战次数tttt：" + GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().ChallengeCount);
                        NetworkHandler.instance.SendProcess("2012#" + MapGateID + ";" + SweepNum + ";");
                        this.gameObject.SetActive(false);
                    }
                }
                else 
                {
                    Debug.Log("每次挑战次数tttt：" + GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().ChallengeCount);
                    NetworkHandler.instance.SendProcess("2012#" + MapGateID + ";" + SweepNum + ";");
                    this.gameObject.SetActive(false);   
                }               
            };
        }

        if (UIEventListener.Get(GameObject.Find("SweepChoseNum/Mask")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("SweepChoseNum/Mask")).onClick += delegate(GameObject go)
            {
                this.gameObject.SetActive(false);
            };
        }
	}
    void Update() 
    {
        if (MapGateID < 20000)
        {
            if (UiSlider.value == 0 || MaxNum == 0)
            {
                SellCount.text = "1";
                AblumNum.text = "6";
            }
            else
            {
                int a = (int)(UiSlider.value * (MaxNum-1));
                SellCount.text = (a+1).ToString();
                AblumNum.text = (int.Parse(SellCount.text) * Num).ToString();
            }
        }
        else
        {
            if (UiSlider.value == 0 || MaxNum == 0)
            {
                SellCount.text = "1";
                AblumNum.text = "12";
            }
            else 
            {

                if (UiSlider.value > (MaxNum - 1) * 0.5f)
                {
                    UiSlider.value = (MaxNum - 1) * 0.5f;
                }
                else 
                {
                    int a = (int)(UiSlider.value * 2);
                    SellCount.text = (a + 1).ToString();
                    AblumNum.text = (int.Parse(SellCount.text) * Num).ToString();
                }
            }

        }

    }
    public void GetMapId(int ID)
    {
        this.MapGateID = ID;
        Debug.Log("准备扫荡的副本ID：" + ID);
        if (MapGateID < 20000)
        {
            this.Num = 6;
            this.MaxNum = CharacterRecorder.instance.stamina / Num;
            UiSlider.numberOfSteps = MaxNum;
            UiSlider.value = 0;


            //if (MaxNum > 10)
            //{
            //    UiSlider.numberOfSteps = MaxNum+1;
            //    UiSlider.value = ((float)10 / MaxNum);
            //}
            //if (MaxNum <= 10)
            //{
            //    UiSlider.numberOfSteps = MaxNum+1;
            //    if (MaxNum == 0)
            //    {
            //        UiSlider.value = 1f;
            //    }
            //    else 
            //    {
            //        UiSlider.value = ((float)1 / MaxNum);
            //    }
            //    //UiSlider.value = 1f;
            //}
            //UiSlider.value = ((float)10/MaxNum);
            //AblumNum.text = "10";
        }
        else
        {
            this.Num = 12;
            this.MaxNum = GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().ChallengeCount;
            Debug.Log("最大扫荡次数：" + this.MaxNum);
            UiSlider.numberOfSteps = 3;
            UiSlider.value = 0;
            //UiSlider.value = 0.33f *MaxNum;
        }
    }
    void SetValue2()
    {
        float num=(float)MaxNum/10;
        for (int i = 0; i < MaxNum; i++) 
        {
            if (UiSlider.value > (num * i) && UiSlider.value <= (num * (i + 1))) 
            {
                SellCount.text = (i + 1).ToString();
                AblumNum.text = (int.Parse(SellCount.text) * Num).ToString();
            }
            if (UiSlider.value == 0) 
            {
                SellCount.text = "0";
                AblumNum.text = (int.Parse(SellCount.text) * Num).ToString();
            }
        }
    }

    void SetValue1() 
    {
        if (UiSlider.value == 0) 
        {
            SellCount.text = "0";
            AblumNum.text = (int.Parse(SellCount.text) * Num).ToString();
        }
        else if (UiSlider.value <= 0.1f&&UiSlider.value>0) 
        {
            SellCount.text = "1";
            AblumNum.text = (int.Parse(SellCount.text) * Num).ToString();
        }
        else if (UiSlider.value > 0.1f && UiSlider.value <= 0.2f) 
        {
            SellCount.text = "2";
            AblumNum.text = (int.Parse(SellCount.text) * Num).ToString();
        }
        else if (UiSlider.value > 0.2f && UiSlider.value <= 0.3f)
        {
            SellCount.text = "3";
            AblumNum.text = (int.Parse(SellCount.text) * Num).ToString();
        }
        else if (UiSlider.value > 0.3f && UiSlider.value <= 0.4f)
        {
            SellCount.text = "4";
            AblumNum.text = (int.Parse(SellCount.text) * Num).ToString();
        }
        else if (UiSlider.value > 0.4f && UiSlider.value <= 0.5f)
        {
            SellCount.text = "5";
            AblumNum.text = (int.Parse(SellCount.text) * Num).ToString();
        }
        else if (UiSlider.value > 0.5f && UiSlider.value <= 0.6f)
        {
            SellCount.text = "6";
            AblumNum.text = (int.Parse(SellCount.text) * Num).ToString();
        }
        else if (UiSlider.value > 0.6f && UiSlider.value <= 0.7f)
        {
            SellCount.text = "7";
            AblumNum.text = (int.Parse(SellCount.text) * Num).ToString();
        }
        else if (UiSlider.value > 0.7f && UiSlider.value <= 0.8f)
        {
            SellCount.text = "8";
            AblumNum.text = (int.Parse(SellCount.text) * Num).ToString();
        }
        else if (UiSlider.value > 0.8f && UiSlider.value <= 0.9f)
        {
            SellCount.text = "9";
            AblumNum.text = (int.Parse(SellCount.text) * Num).ToString();
        }
        else if (UiSlider.value > 0.9f && UiSlider.value <= 1f)
        {
            SellCount.text = "10";
            AblumNum.text = (int.Parse(SellCount.text) * Num).ToString();
        }
    }*/
}
