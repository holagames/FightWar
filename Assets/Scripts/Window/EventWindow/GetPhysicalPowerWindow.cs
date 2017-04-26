using UnityEngine;
using System.Collections;

public class GetPhysicalPowerWindow : MonoBehaviour
{

    public GameObject MorningButton;
    public GameObject AfternoonButton;
    public GameObject DinnerButton;
    public GameObject GetPowerEffect;
    public UILabel NeedTime;
    public UILabel SpeakLabel;
    public UISprite DoctorTexture;
    public int MorningType;
    public int AfternoonType;
    public int DinnerType;
    public int AllTime;
    public bool isGetPower = false;
    void Start()
    {
        StopAllCoroutines();
        UIEventListener.Get(MorningButton).onClick += delegate(GameObject go)
        {
            if (MorningType == 0)
            {
                UIManager.instance.OpenPromptWindow("制药也是要时间的!", PromptWindow.PromptType.Hint, null, null);
            }
            else if (MorningType == 1)
            {
                NetworkHandler.instance.SendProcess("1092#1;");
                isGetPower = true;
                GetPowerEffectInfo();
            }
            else if (MorningType == 2)
            {
                if (CharacterRecorder.instance.lunaGem >= 20)
                {
                    NetworkHandler.instance.SendProcess("1092#1;");
                    isGetPower = true;
                    GetPowerEffectInfo();
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("钻石不足!", PromptWindow.PromptType.Hint, null, null);
                }
            }
            else
            {
                UIManager.instance.OpenPromptWindow("我可不是慈善家，你已经喝过一次了!", PromptWindow.PromptType.Hint, null, null);
            }
        };


        UIEventListener.Get(AfternoonButton).onClick += delegate(GameObject go)
        {
            if (AfternoonType == 0)
            {
                UIManager.instance.OpenPromptWindow("制药也是要时间的!", PromptWindow.PromptType.Hint, null, null);
            }
            else if (AfternoonType == 1)
            {
                NetworkHandler.instance.SendProcess("1092#2;");
                isGetPower = true;
                GetPowerEffectInfo();
            }
            else if (AfternoonType == 2)
            {
                if (CharacterRecorder.instance.lunaGem >= 20)
                {
                    NetworkHandler.instance.SendProcess("1092#2;");
                    isGetPower = true;
                    GetPowerEffectInfo();
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("钻石不足!", PromptWindow.PromptType.Hint, null, null);
                }
            }
            else
            {
                UIManager.instance.OpenPromptWindow("我可不是慈善家，你已经喝过一次了!", PromptWindow.PromptType.Hint, null, null);
            }
        };


        UIEventListener.Get(DinnerButton).onClick += delegate(GameObject go)
        {
            if (DinnerType == 0)
            {
                UIManager.instance.OpenPromptWindow("制药也是要时间的!", PromptWindow.PromptType.Hint, null, null);
            }
            else if (DinnerType == 1)
            {
                NetworkHandler.instance.SendProcess("1092#3;");
                isGetPower = true;
                GetPowerEffectInfo();
            }
            else if (DinnerType == 2)
            {
                if (CharacterRecorder.instance.lunaGem >= 20)
                {
                    NetworkHandler.instance.SendProcess("1092#3;");
                    isGetPower = true;
                    GetPowerEffectInfo();
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("钻石不足!", PromptWindow.PromptType.Hint, null, null);
                }
            }
            else
            {
                UIManager.instance.OpenPromptWindow("我可不是慈善家，你已经喝过一次了!", PromptWindow.PromptType.Hint, null, null);
            }
        };
        StartCoroutine(DoctorEffect());
        //PowerIcon  PowerGet BG PowerEffect
    }
    IEnumerator DoctorEffect()
    {
        DoctorTexture.spriteName = "LQTL_01";
        yield return new WaitForSeconds(0.1f);
        DoctorTexture.spriteName = "LQTL_02";
        yield return new WaitForSeconds(0.1f);
        DoctorTexture.spriteName = "LQTL_03";
        yield return new WaitForSeconds(0.1f);
        DoctorTexture.spriteName = "LQTL_01";
        yield return new WaitForSeconds(0.1f);
        DoctorTexture.spriteName = "LQTL_02";
        yield return new WaitForSeconds(0.1f);
        DoctorTexture.spriteName = "LQTL_03";
        yield return new WaitForSeconds(0.1f);
        DoctorTexture.spriteName = "LQTL_01";
        yield return new WaitForSeconds(0.1f);
        DoctorTexture.spriteName = "LQTL_02";
        yield return new WaitForSeconds(0.1f);
        DoctorTexture.spriteName = "LQTL_03";
        yield return new WaitForSeconds(0.1f);
        if (isGetPower)
        {
            DoctorTexture.spriteName = "LQTL_04";
            yield return new WaitForSeconds(0.1f);
            DoctorTexture.spriteName = "LQTL_04";
            yield return new WaitForSeconds(0.1f);
            DoctorTexture.spriteName = "LQTL_04";
            yield return new WaitForSeconds(0.1f);
            DoctorTexture.spriteName = "LQTL_04";
            yield return new WaitForSeconds(0.1f);
            isGetPower = false;
        }
        StartCoroutine(DoctorEffect());
    }

    void GetPowerEffectInfo()
    {
        GameObject go = Instantiate(GetPowerEffect) as GameObject;
        go.transform.parent = gameObject.transform;
        go.transform.localPosition = new Vector3(258, -74, 0);
        go.transform.localRotation = new Quaternion(-90, 0, 0, 0);
        go.transform.localScale = new Vector3(1, 1, 1);
        GetPowerEffect.SetActive(true);
    }
    public void SetInfo(string[] dataSplit)
    {
        StopAllCoroutines();
        AllTime = int.Parse(dataSplit[0]);
        MorningType = int.Parse(dataSplit[1]);
        AfternoonType = int.Parse(dataSplit[2]);
        DinnerType = int.Parse(dataSplit[3]);
        ButtonInfo(MorningButton, MorningType);
        ButtonInfo(AfternoonButton, AfternoonType);
        ButtonInfo(DinnerButton, DinnerType);
        if (AfternoonType == 3 && MorningType == 3 && DinnerType == 3)
        {

        }
        else if (int.Parse(dataSplit[4]) == 1 && MorningType == 0 && MorningType != 2)
        {
            ButtonInfo(MorningButton);
        }
        else if (((int.Parse(dataSplit[4]) == 1 && MorningType == 2) || (int.Parse(dataSplit[4]) == 1 && MorningType == 3) || (int.Parse(dataSplit[4]) == 2 && AfternoonType == 0) )&& AfternoonType!=2)
        {
            ButtonInfo(AfternoonButton);
        }
        else if (((int.Parse(dataSplit[4]) == 2 && AfternoonType == 2) || (int.Parse(dataSplit[4]) == 2 && AfternoonType == 3) || (int.Parse(dataSplit[4]) == 3 && DinnerType == 0)) && DinnerType != 2)
        {
            ButtonInfo(DinnerButton);
        }
        SpeakLabel.text = "[e9fafa]来一杯我新研制的特制药水吧！\n可快速增加[-][3ee817]60点[-][e9fafa]体力[-]。";
        if ((MorningType == 3 || MorningType == 2) && (AfternoonType == 3 || AfternoonType == 2) && (DinnerType == 3 || AfternoonType == 2))
        {
            NeedTime.text = "[ffffff]今天没有可领取的药剂了呦";
        }
        else
        {
            if (AllTime >= 0)
            {
                CancelInvoke("ShowTimeInfo");
                InvokeRepeating("ShowTimeInfo", 0f, 1f);
            }
        }
        StartCoroutine(DoctorEffect());
    }
    void ButtonInfo(GameObject go)//即将可领取状态
    {
        go.transform.localScale = new Vector3(1, 1, 1);
        go.transform.Find("PowerIcon").GetComponent<TweenScale>().enabled = false;
        go.transform.Find("PowerIcon/spark").gameObject.SetActive(false);
        go.transform.Find("PowerIcon/Ping").gameObject.SetActive(false);
        go.transform.Find("PowerGet").gameObject.SetActive(false);
        go.transform.Find("PowerEffect").gameObject.SetActive(false);
        go.transform.Find("DiaCost").gameObject.SetActive(false);
        go.transform.Find("ReadyLabel").gameObject.SetActive(true);
        go.transform.Find("BG").GetComponent<UISprite>().spriteName = "huodong_di1";
        go.transform.Find("PowerIcon").GetComponent<UISprite>().spriteName = "huodong_icon5";
    }
    void ShowTimeInfo()
    {
        string hour = (AllTime / 3600).ToString("00");
        string sceond = (AllTime % 3600 / 60).ToString("00");
        string min = (AllTime % 60).ToString("00");
        if (MorningType == 1 || AfternoonType == 1 || DinnerType == 1)
        {
            NeedTime.text = "[ffffff]领取截至倒计时:[3ee817]" + hour + ":" + sceond + ":" + min;
        }
        else
        {
            NeedTime.text = "[ffffff]下一次领取还需:[3ee817]" + hour + ":" + sceond + ":" + min;
        }
        AllTime -= 1;
    }
    void ButtonInfo(GameObject go, int Type)
    {
        go.transform.localScale = new Vector3(1, 1, 1);
        go.transform.Find("PowerIcon").GetComponent<TweenScale>().enabled = false;
        go.transform.Find("PowerIcon/spark").gameObject.SetActive(false);
        go.transform.Find("PowerIcon/Ping").gameObject.SetActive(false);
        go.transform.Find("PowerGet").gameObject.SetActive(false);
        go.transform.Find("PowerEffect").gameObject.SetActive(false);
        go.transform.Find("DiaCost").gameObject.SetActive(false);
        go.transform.Find("ReadyLabel").gameObject.SetActive(false);
        switch (Type)
        {
            case 0:
                go.transform.Find("BG").GetComponent<UISprite>().spriteName = "huodong_di2";
                go.transform.Find("PowerIcon").GetComponent<UISprite>().spriteName = "huodong_icon4";
                break;
            case 1:
                go.transform.Find("BG").GetComponent<UISprite>().spriteName = "huodong_di1";
                go.transform.Find("PowerIcon").GetComponent<UISprite>().spriteName = "huodong_icon5";
                go.transform.Find("PowerEffect").gameObject.SetActive(true);
                go.transform.Find("PowerIcon").GetComponent<TweenScale>().enabled = true;
                go.transform.Find("PowerIcon/spark").gameObject.SetActive(true);
                go.transform.Find("PowerIcon/Ping").gameObject.SetActive(true);
                //StartCoroutine(IconEffect(go.transform.Find("PowerIcon").gameObject));
                break;
            case 2:
                go.transform.Find("BG").GetComponent<UISprite>().spriteName = "huodong_di1";
                go.transform.Find("PowerIcon").GetComponent<UISprite>().spriteName = "huodong_icon5";
                go.transform.Find("PowerGet").GetComponent<UISprite>().spriteName = "huodong_icon2";
                go.transform.Find("PowerGet").gameObject.SetActive(true);
                go.transform.Find("PowerEffect").gameObject.SetActive(true);
                go.transform.Find("DiaCost").gameObject.SetActive(true);
                break;
            case 3:
                go.transform.Find("BG").GetComponent<UISprite>().spriteName = "huodong_di2";
                go.transform.Find("PowerIcon").GetComponent<UISprite>().spriteName = "huodong_icon4";
                go.transform.Find("PowerGet").GetComponent<UISprite>().spriteName = "huodong_icon1";
                go.transform.Find("PowerGet").gameObject.SetActive(true);
                break;
        }
    }
    IEnumerator IconEffect(GameObject go)
    {
        go.transform.Rotate(0, 0, 10);
        yield return new WaitForSeconds(0.05f);
        go.transform.Rotate(0, 0, -10);
        yield return new WaitForSeconds(0.05f);
        go.transform.Rotate(0, 0, -10);
        yield return new WaitForSeconds(0.05f);
        go.transform.Rotate(0, 0, 10);
        yield return new WaitForSeconds(0.05f);
        StartCoroutine(IconEffect(go));
    }

}
