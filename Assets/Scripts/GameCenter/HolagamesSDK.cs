using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;
using System.Runtime.InteropServices;

namespace Holagames
{
    public enum HolagamesSDKType
    {
        Analytics = 1,
        Share = 2,
        Social = 4,
        IAP = 8,
        Ads = 16,
        User = 32,
        Push = 64,
    };

    public enum UserWrapper
    {
        ExitByGame = 0,
        ExitBySdk = 1,

        InitSuccess = 100,
        InitFalied = 101,

        LoginSuccess = 200,
        LoginFailed = 201,
        LoginCancel = 202,
        Logining = 203,
        LoginSwitch = 204,

        LogoutSuccess = 300,
        LogoutFailed = 301,
        notLogin = 302,
        GameContinue = 351,
        GameExit = 352,

        PaySuccess = 400,
        PayFailed = 401,
        PayCancel = 402,
        PayCheck = 403,

    };

    public class HolagamesSDK
    {
        private static HolagamesSDK _instance;

        public static HolagamesSDK getInstance()
        {
            if (null == _instance)
            {
                _instance = new HolagamesSDK();
            }
            return _instance;
        }

        public HolagamesSDK()
        {
#if UNITY_ANDROID
            jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
#endif
        }

        /// <summary>
        /// sdk初始化
        /// </summary>
        /// <param name="appid"></param>
        /// <param name="appkey"></param>
        public void init(string appid, string appkey, string privateKey, string oauthLoginServer)
        {

#if JINLI
#elif UNITY_ANDROID
            Debug.Log("UNITY_ANDROID===HolaSdkInit!!");
            jo.Call("HolaSdkInit", appid, appkey, privateKey, oauthLoginServer);
#endif
#if KY
            //_Init();
#endif
        }

        // 登录
        public void Login(string LoginString)
        {
#if JINLI
            APaymentHelperDemo.instance.Login();
#elif UNITY_ANDROID
            Debug.Log("UNITY_ANDROID===HolaSdk==Login!!");
            jo.Call("Login", LoginString);
#endif

#if KY
            APaymentHelperDemo.instance.Login();
#endif
        }

        // 注销
        public void logout()
        {
#if JINLI
            APaymentHelperDemo.instance.Logout();
#elif UNITY_ANDROID
            jo.Call("Logout");
#endif
#if KY

            //_LogOut();
#endif
        }

        // 切换帐号
        public void switchLogin()
        {
#if JINLI
#elif UNITY_ANDROID
            jo.Call("switchLogin");
#endif
        }

        // 当前登陆状态
        public bool isLogin()
        {
#if JINLI
            return false;
#elif UNITY_ANDROID
            return jo.Call<bool>("isLogin");
#else
            return false;
#endif
        }

        // 隐藏悬浮窗
        public void hideToolBar()
        {
#if JINLI
#elif UNITY_ANDROID
            Debug.Log("hideToolBar");
            jo.Call("hideToolBar");
#endif
        }

        // 显示悬浮窗
        public void createToolBar()
        {
#if JINLI
#elif UNITY_ANDROID
            Debug.Log("createToolBar");
            jo.Call("createToolBar");
#endif
        }

        // 显示悬浮窗
        public void showToolBar()
        {
#if JINLI
#elif UNITY_ANDROID
            Debug.Log("showToolBar");
            jo.Call("showToolBar");
#endif
        }

        // 进入用户中心
        public void entryGameCenter()
        {
#if JINLI
#elif UNITY_ANDROID
            Debug.Log("entryGameCenter");
            jo.Call("entryGameCenter");
#endif

#if KY
            //_entryaGameCenter();
#endif
        }

        public void exitSDK()
        {
#if JINLI
#elif UNITY_ANDROID
            jo.Call("ExitSDK");
#endif
        }


        // 支付
        //zoneid_diamond_guid_serverid_paytype
        public void Pay(string msg)
        {
#if JINLI
#elif UNITY_ANDROID
            Debug.Log("andriod pay=" + msg);
            jo.Call("Pay", msg);
#elif KY
            //_pay(msg);
#endif
        }

        /// <summary>
        /// 获取用户ID
        /// </summary>
        /// <returns></returns>
        public string getUserID()
        {
#if JINLI
            return string.Empty;
#elif UNITY_ANDROID
            return jo.Call<string>("getUserID");
#elif KY
            return "";//_getUserID();
#else
            return string.Empty;
#endif
        }

        /// <summary>
        /// 获取用户ID
        /// </summary>
        /// <returns></returns>
        public string getChannelId()
        {
#if JINLI
            return string.Empty;
#elif UNITY_ANDROID
            return jo.Call<string>("getChannelId");
#else
            return string.Empty;
#endif
        }


        public void registerActionCallback(int type, MonoBehaviour gameObject, string functionName)
        {
#if UNITY_ANDROID
            if (mAndroidJavaClass == null)
            {
                mAndroidJavaClass = new AndroidJavaClass("com.holagames.sdk.MessageHandle");
            }
            string gameObjectName = gameObject.gameObject.name;
            mAndroidJavaClass.CallStatic("registerActionResultCallback", new object[] { type, gameObjectName, functionName });
#endif

#if KY
			//_registerActionResultCallback(type,gameObject.gameObject.name, functionName);
#endif
        }

        public void loginGameRole(string type, string msg)
        {
#if JINLI
#elif UNITY_ANDROID
            jo.Call("LoginGameRole", type, msg);
#endif

        }

#if UNITY_ANDROID
        private AndroidJavaClass jc;
        private AndroidJavaObject jo;

        private AndroidJavaClass mAndroidJavaClass;
#endif
        /**
     	@brief the Dictionary type change to the string type 
    	 @param Dictionary
    	 @return  string
    	*/
        public static Dictionary<string, string> stringToDictionary(string message)
        {
            Dictionary<string, string> param = new Dictionary<string, string>();
            if (null != message)
            {
                /*if (message.Contains("&info="))
                {
                    Regex regex = new Regex(@"code=(.*)&msg=(.*)&info=(.*)");
                    string[] tokens = regex.Split(message);
                    string code = tokens[1];
                    string msg = tokens[2];
                    string info = tokens[3];
                    param.Add("code", code);
                    param.Add("msg", msg);
                    param.Add("info", info);
                }
                else*/
                {
                    /*Regex regex = new Regex(@"code=(.*)&msg=(.*)");
                    string[] tokens = regex.Split(message);                    
					string code = tokens[1];
                    string msg = tokens[2];
                    param.Add("code", code);
                    param.Add("msg", msg);*/


                    string[] tokens = message.Split('&');

                    for (var i = 0; i < tokens.Length; i++)
                    {
                        string[] _s = tokens[i].Split('=');
                        if (_s.Length == 2)
                        {
                            param.Add(_s[0], _s[1]);
                        }
                        else
                        {
                            string _v = "";
                            int _index = tokens[i].IndexOf("=");
                            _v = tokens[i].Substring(_index + 1);

                            param.Add(_s[0], _v);
                        }
                    }

                }
            }

            return param;
        }


        public static string dictionaryToString(Dictionary<string, string> maps)
        {
            StringBuilder param = new StringBuilder();
            if (null != maps)
            {
                foreach (KeyValuePair<string, string> kv in maps)
                {
                    if (param.Length == 0)
                    {
                        param.AppendFormat("{0}={1}", kv.Key, kv.Value);
                    }
                    else
                    {
                        param.AppendFormat("&{0}={1}", kv.Key, kv.Value);
                    }
                }
            }
            //			byte[] tempStr = Encoding.UTF8.GetBytes (param.ToString ());
            //			string msgBody = Encoding.Default.GetString(tempStr);
            return param.ToString();
        }
    }

}
