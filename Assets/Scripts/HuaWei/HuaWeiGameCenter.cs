using UnityEngine;
using System.Collections;
using CodeStage.AntiCheat.ObscuredTypes;
using Holagames;
using System.Collections.Generic;
using System;
using System.Text;

public class HuaWeiGameCenter : MonoBehaviour
{
    public bool IsInit = false;
    public bool IsCheckBill = false;
    public float Timer = 4;
#if KY || JINLI
    private APaymentHelperDemo mDemo;
#endif
    void Awake()
    {
        //Debug.LogError("Awake SDK!!!!!!!!!");
#if KY
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            mDemo = gameObject.AddComponent<APaymentHelperDemo>();
        }
#endif


#if JINLI
        //if (Application.loadedLevelName == "Downloader")
        {
            mDemo = gameObject.AddComponent<APaymentHelperDemo>();
        }
#endif

#if XIAOMI || OPPO || UC || QIHOO360 || VIVO || KY || QQ || BAIDU || HUAWEI || HOLA ||MEIZU || WDJ || QIANHUAN
        HolagamesSDK.getInstance().registerActionCallback((int)HolagamesSDKType.User, this, "HolaSdkCallBack");
        HolagamesSDK.getInstance().registerActionCallback((int)HolagamesSDKType.IAP, this, "HolaSdkCallBack");
#endif
    }

    IEnumerator ContinueLogin()
    {
        while (!NetworkHandler.instance.IsPrefetch)
        {
            yield return new WaitForSeconds(1);
        }
        HolagamesSDK.getInstance().Login("");
    }

    public bool isFrist = false;
    void AndroidReceive(string content)
    {
        Debug.Log(content);
        if (content == "InitSuccess")
        {
#if JINLI       
            GameObject.Find("MainCamera").GetComponent<HuaWeiGameCenter>().IsInit = true;
            StartCoroutine(ContinueLogin());
#endif
        }
#if QQ       
        else if(content == "WX_NotInstall")
        {
            UIManager.instance.OpenPromptWindow("亲，请先安装最新版微信再进行游戏哦！", PromptWindow.PromptType.Alert, null, null);
        }
        else if(content == "QQ_NotInstall")
        {
            UIManager.instance.OpenPromptWindow("亲，请先安装最新版QQ再进行游戏哦！", PromptWindow.PromptType.Alert, null, null);
        }
#endif
    }

    void AndroidReceiveChangeUser(string ChangeUser)
    {
        Debug.Log("ChangeUser");
        PlayerPrefs.SetInt("Relogin", 1);
        VersionChecker.instance.IsSDKLogin = false;
        CharacterRecorder.instance.userId = 0;
        NetworkHandler.instance.IsFirst = false;
        AsyncOperation async = Application.LoadLevelAsync("Downloader");
    }

    void AndroidReceiveLoginFailed(string Value)
    {
        UIManager.instance.OpenPromptWindow(Value, PromptWindow.PromptType.Alert, StartSDK, null);
    }

    void AndroidReceiveLogin(string Value)
    {
        Debug.Log(Value);
        string Account = Value.Split('_')[0];
        string Password = Value.Split('_')[1];
        Debug.Log(Account + " " + Password);

        NetworkHandler.instance.Account = Account;
        NetworkHandler.instance.Password = Password;

        ObscuredPrefs.SetString("Account", "HUAWEI_" + Account);
        ObscuredPrefs.SetString("Password", Account);

        NetworkHandler.instance.SendProcess("1001#" + Account + ";" + Password + ";");

        //using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        //{
        //    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
        //    {
        //        jo.Call("HuaWeiSDKShow");
        //    }
        //}
    }

#if QQ
    void Update()
    {
        if (IsCheckBill)
        {
            Timer += Time.deltaTime;
            if (Timer > 5)
            {
                Timer -= 5;
                using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                {
                    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                    {
                        jo.Call("CheckBill", CharacterRecorder.instance.userId.ToString() + "-" + PlayerPrefs.GetString("ServerID")  + "-1-6");
                    }
                }
            }
        }
    }
#endif
    public void StartSDK()
    {
        Debug.LogError("StartSDK SDK!!!!!!!!!");
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_102);
        }
#if KY
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {           
            mDemo.InitSDK();
            return;
        }
#elif JINLI
        GameObject.Find("MainCamera").GetComponent<HuaWeiGameCenter>().IsInit = true;
        //HolagamesSDK.getInstance().Login("");
        return;
#elif XIAOMI
        HolagamesSDK.getInstance().init("2882303761517475726", "5331747594726", "HfM9lFxOF5S0lMdWbkXgQQ==", "");  
        HolagamesSDK.getInstance().Login("");
        return;
#elif OPPO   
        //HolagamesSDK.getInstance().init("", "ab03f4746b8C0A1d68624598177f64AD", "", "");  
        HolagamesSDK.getInstance().Login("");
        return;
#elif UC
        HolagamesSDK.getInstance().init("", "", "", "");  
        return;
#elif WDJ
        HolagamesSDK.getInstance().init("", "", "", "");  
        return;
#elif QIHOO360
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif HUAWEI
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif VIVO
        HolagamesSDK.getInstance().init("9885e4eaa65e696ed43360ac994a05df", "42816dacc5cfbe9fd0e95cfe35f2eebc", "20160201102033928864", "");  
        return;
#elif QQ
        NetworkHandler.instance.IsSDKLogin = true;
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif BAIDU        
        GameObject.Find("MainCamera").GetComponent<HuaWeiGameCenter>().IsInit = true;
        HolagamesSDK.getInstance().Login("");
        return;
#elif MEIZU
        HolagamesSDK.getInstance().init("","","","");
        return;
#elif HOLA
        GameObject.Find("MainCamera").GetComponent<HuaWeiGameCenter>().IsInit = true;
        HolagamesSDK.getInstance().Login("");
        return;
#elif QIANHUAN
        GameObject.Find("MainCamera").GetComponent<HuaWeiGameCenter>().IsInit = true;
        HolagamesSDK.getInstance().Login("");
        return;
#endif
    }

    private void HolaSdkCallBack(string msg)
    {
        Debug.Log("HolaSdkCallBack==" + msg);
        Dictionary<string, string> dic = HolagamesSDK.stringToDictionary(msg);
        int code = Convert.ToInt32(dic["code"]);
        string result = dic["msg"];
        switch (code)
        {
            case (int)UserWrapper.InitFalied:
                Debug.LogError("SDK Init Failed");
                break;

            case (int)UserWrapper.InitSuccess:
#if UC
            HolagamesSDK.getInstance().createToolBar();
#elif WDJ
            HolagamesSDK.getInstance().createToolBar();
#endif
                Debug.LogError("SDK InitSuccess");
                IsInit = true;
#if QQ
                VersionChecker.instance.IsLogin = true;
                HolagamesSDK.getInstance().Login("");
#elif QIHOO360 || VIVO || UC || HUAWEI || JINLI||MEIZU || WDJ
                
                HolagamesSDK.getInstance().Login("");
#endif
                break;
            case (int)UserWrapper.LoginCancel:
#if XIAOMI
                HolagamesSDK.getInstance().Login("");
#endif
                break;

            case (int)UserWrapper.LoginSwitch:
                break;

            case (int)UserWrapper.LoginFailed:
                break;

            case (int)UserWrapper.Logining:
                break;

            case (int)UserWrapper.LoginSuccess:
                VersionChecker.instance.IsLogin = true;
                VersionChecker.instance.IsSDKLogin = true;
                Debug.LogError("SDK LoginSuccess");
                string _chane = "HUAWEI_";
#if XIAOMI
                _chane = "XIAOMI_";
#elif UC 
                _chane = "UC_";
                 HolagamesSDK.getInstance().showToolBar();
#elif WDJ
                _chane = "{B4447B49-BC295EFE}_";
                 HolagamesSDK.getInstance().showToolBar();
#elif QIHOO360 
                _chane = "QIHOO360_";
#elif OPPO 
                _chane = "OPPO_";  
#elif BAIDU
                _chane = "BAIDU_";  
#elif VIVO 
                _chane = "VIVO_";     
#elif QQ
                _chane = "QQ";   
#elif MEIZU
                _chane = "MEIZU_"; 
#elif HOLA
                _chane = "HOLA_";  
#elif QIANHUAN
                _chane = "QIANHUAN_";  
#elif KY
                WWWForm form = new WWWForm();				
				form.AddField("tokenKey",UrlEncode(result));								
				WWW www = new WWW("http://139.196.14.52/FYLogin/FYLogin.php", form);
                StartCoroutine(LoginPhp(www));
                break;
#elif TBT
                WWWForm form = new WWWForm();				
				form.AddField("tokenKey",result);							
				WWW www = new WWW("http://139.196.14.52/FYLogin/FYLogin.php", form);

                StartCoroutine(LoginPhp(www));
                break;
                                break;
#elif XY
                WWWForm form = new WWWForm();				
				form.AddField("tokenKey",result);							
				WWW www = new WWW("http://139.196.14.52/FYLogin/FYLogin.php", form);

                StartCoroutine(LoginPhp(www));
                break;
#endif
                string Account = _chane + result;
                string Password = "t_" + result;
                Debug.Log(Account + " " + Password);

                NetworkHandler.instance.Account = Account;
                NetworkHandler.instance.Password = Password;

                ObscuredPrefs.SetString("Account", Account);
                ObscuredPrefs.SetString("Password", Account);


                NetworkHandler.instance.SendProcess("1001#" + Account + ";" + Password + ";");

                break;
            case (int)UserWrapper.LogoutFailed:
                break;

            case (int)UserWrapper.LogoutSuccess:
#if UC || OPPO || WDJ
                Application.Quit();
                return;
#elif QQ
                VersionChecker.instance.IsLogin = false;
                UIManager.instance.OpenPanel("LoginWindow", true);
#else
                PlayerPrefs.SetInt("Relogin", 1);
                CharacterRecorder.instance.userId = 0;
                AsyncOperation async = Application.LoadLevelAsync("Downloader");
#endif
                break;

            case (int)UserWrapper.notLogin:
                break;

            case (int)UserWrapper.PayFailed:
                IsCheckBill = false;
                break;

            case (int)UserWrapper.PaySuccess:
                IsCheckBill = false;
                break;

            case (int)UserWrapper.PayCheck:

                IsCheckBill = true;
                break;

        }
    }

    public void SDKShowLogo()
    {
#if XIAOMI
        HolagamesSDK.getInstance().showToolBar();
        return;
#endif


        isFrist = true;
#if UNITY_ANDROID
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                jo.Call("HuaWeiSDKShow");
            }
        }
#endif
    }


    public void DownloadApk(string Url)
    {
        Debug.LogError(Url);
#if UNITY_ANDROID
        using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                jo.Call("DownloadApk", Url);
            }
        }
#endif
    }

    void OnApplicationPause(bool isPause)
    {

#if XIAOMI        
        return;
#endif

#if UNITY_ANDROID
        if (isFrist)
        {
            if (!isPause)
            {
                using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                {
                    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                    {
                        jo.Call("onPause");
                    }
                }
            }
            else
            {
                using (AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
                {
                    using (AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity"))
                    {
                        jo.Call("HuaWeiSDKShow");
                    }
                }
            }
        }
#endif
    }


    /// <summary>
    /// 实现php的URLENCODE函数
    /// </summary>
    /// <param name="str"></param>
    /// <returns></returns>
    public string UrlEncode(string str)
    {
        StringBuilder sb = new StringBuilder();

        for (var i = 0; i < str.Length; i++)
        {
            char c = str[i];
            if ((c >= '0' && c <= '9') || (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
            {
                sb.Append(c);
            }
            else
            {
                byte[] bystr = charToByte(str[i]);

                sb.Append(@"%");

                for (int ij = 0; ij < bystr.Length; ij++)
                {
                    if (bystr[ij] != 0)
                    {
                        sb.Append(Convert.ToString(bystr[ij], 16).ToUpper());
                    }
                }
            }
        }
        return (sb.ToString());
    }

    public byte[] charToByte(char c)
    {
        byte[] b = new byte[2];
        b[0] = (byte)((c & 0xFF00) >> 8);
        b[1] = (byte)(c & 0xFF);
        return b;
    }


    /// <summary>
    /// ios登录取帐号
    /// </summary>
    /// <param name="www"></param>
    /// <returns></returns>
    IEnumerator LoginPhp(WWW www)
    {

        yield return www;
        Debug.LogError(www.text);
        Debug.LogError(www.error);
        if (www.error == null && www.text != "0")
        {
            string _chane = "KY_";

#if KY
#elif TBT
            _chane = "TBT_";
            if (www.text == "-1")
            {
                HolagamesSDK.getInstance().Login("");
                return;
            }
#endif

            string Account = _chane + www.text;
            string Password = "t_" + www.text;
            Debug.Log(Account + " " + Password);

            NetworkHandler.instance.Account = Account;
            NetworkHandler.instance.Password = Password;

            ObscuredPrefs.SetString("Account", Account);
            ObscuredPrefs.SetString("Password", Account);

            NetworkHandler.instance.SendProcess("1001#" + Account + ";" + Password + ";");
        }

    }
}
