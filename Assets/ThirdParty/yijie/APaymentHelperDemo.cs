using UnityEngine;
using System.Collections;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using System;
using System.IO;
using System.Net;
using Common;
using System.Globalization;
using CodeStage.AntiCheat.ObscuredTypes;



public class APaymentHelperDemo : MonoBehaviour
{

    //自建服务器 测服
    //	public static string CP_LOGIN_CHECK_URL = "http://testomsdk.xiaobalei.com:5555/cp/user/paylog/checkLoginZijian";
    //自建服务器 正服 
    //  public static string CP_LOGIN_CHECK_URL = "http://testomsdk.xiaobalei.com:5555/cp/user/paylog/checkLoginZijianP";
    //test
    //  public static string CP_LOGIN_CHECK_URL = "http://testomsdk.xiaobalei.com:5555/cp/user/paylog/checkLogin";
    //release
    public static string CP_LOGIN_CHECK_URL = "http://123.207.146.159/isdk/login.php";
    public static string CP_PAY_CHECK_URL = "http://testomsdk.xiaobalei.com:5555/cp/user/paylog/get?orderId=";
    public static string CP_PAY_SYNC_URL = "http://123.207.146.159/isdk/pay.php";
    public static string PAYTYPE_PAY = "pay";
    public static string PAYTYPE_CHARGE = "charge";
    public static string PAYTYPE_PAY_EXTEND = "payextend";
    string goodName = "";
    int money = 0;
    string desc = "";
    string paytype = "";
    Rect windowRect = new Rect(20, 20, 400, 600);
    string str = "show result";
    public static SFOnlineUser user;
    public static Boolean bLogined = false;
    public Boolean isQuerying = false;
    public Boolean isDebug = false;
    public Boolean isPause = false;
    public Boolean isFocus = false;
    public ArrayList orderIds = new ArrayList();

    public static APaymentHelperDemo instance;

    struct OderId
    {
        public int retry;
        public string orderId;
    }
    public GUISkin guiSkin;

    /**
     * exit接口用于系统全局退出
     * @param context      上下文Activity
     * @param gameObject   游戏场景中的对象
     * @param listener     退出的监听函数，隶属于gameObject对象的运行时脚本的方法名称，该方法会在收到退出通知后触发
     * */
#if UNITY_IOSOFFCIAL
#elif UNITY_IPHONE
#else
    [DllImport("gangaOnlineUnityHelper")]
    private static extern void exit(IntPtr context, string gameObject, string listener);
#endif
    

#if UNITY_IOSOFFCIAL
#elif UNITY_IPHONE
    [DllImport("__Internal")]  
    private static extern void onCreate_listener(IntPtr context, string gameObject, string listener);
#else
    [DllImport("gangaOnlineUnityHelper")]
    private static extern void onCreate_listener(IntPtr context, string gameObject, string listener);
#endif
    /**
	 * onCreate_listener   接口用于初始化回调
	 * @param context      未用到的参数，为与android的接口保持一致，可以传固定的值
	 * @param gameObject   游戏场景中的对象
	 * @param listener     退出的监听函数，隶属于gameObject对象的运行时脚本的方法名称，该方法会在收到通知后触发
	 * */
    
    /**
     * exit接口用于系统全局退出
     * @param context      未用到的参数，为与android的接口保持一致，可以传固定的值
     * @param gameObject   游戏场景中的对象
     * @param listener     退出的监听函数，隶属于gameObject对象的运行时脚本的方法名称，该方法会在收到退出通知后触发
     * */
#if UNITY_IOSOFFCIAL
#elif UNITY_IPHONE
    [DllImport("__Internal")]  
    private static extern void exitInterface(IntPtr context, string gameObject, string listener);
#else
    [DllImport("gangaOnlineUnityHelper")]
    private static extern void exitInterface(IntPtr context, string gameObject, string listener);
#endif
    

    /**
     * login接口用于SDK登陆
     * @param context      未用到的参数，为与android的接口保持一致，可以传固定的值
     * @param customParams 自定义参数
     * */
#if UNITY_IOSOFFCIAL
#elif UNITY_IPHONE
    [DllImport("__Internal")]  
    private static extern void login(IntPtr context, string customParams);
#else
    [DllImport("gangaOnlineUnityHelper")]
    private static extern void login(IntPtr context, string customParams);
#endif
    

    /**
     * logout接口用于SDK登出
     * @param context      未用到的参数，为与android的接口保持一致，可以传固定的值
     * @param customParams 自定义参数
     * */
#if UNITY_IOSOFFCIAL
#elif UNITY_IPHONE
    [DllImport("__Internal")]  
    private static extern void logout(IntPtr context, string customParams);
#else
    [DllImport("gangaOnlineUnityHelper")]
    private static extern void logout(IntPtr context, string customParams);
#endif
    

    /**
     * charge接口用于用户触发非定额计费
     * @param context      未用到的参数，为与android的接口保持一致，可以传固定的值
     * @param gameObject   游戏场景中的对象
     * @param itemName     虚拟货币名称
     * @param unitPrice    游戏道具单位价格，单位-分
     * @param count        商品或道具数量
     * @param callBackInfo 由游戏开发者定义传入的字符串，会与支付结果一同发送给游戏服务器，
     *                     游戏服务器可通过该字段判断交易的详细内容（金额角色等）
     * @param callBackUrl  将支付结果通知给游戏服务器时的通知地址url，交易结束后，
     * 					   系统会向该url发送http请求，通知交易的结果金额callbackInfo等信息
     * @param payResultListener  支付监听接口，隶属于gameObject对象的运行时脚本的方法名称，该方法会在收到通知后触发
     * */
#if UNITY_IOSOFFCIAL
#elif UNITY_IPHONE
    [DllImport("__Internal")]  
    private static extern void charge(IntPtr context, string gameObject, string itemName, int unitPrice,
            int count, string callBackInfo, string callBackUrl, string payResultListener);
#else
    [DllImport("gangaOnlineUnityHelper")]
    private static extern void charge(IntPtr context, string gameObject, string itemName, int unitPrice,
            int count, string callBackInfo, string callBackUrl, string payResultListener);
#endif
    

    /**
     * pay接口用于用户触发定额计费
     * @param context      未用到的参数，为与android的接口保持一致，可以传固定的值
     * @param gameObject   游戏场景中的对象
     * @param unitPrice    游戏道具单位价格，单位-分
     * @param unitName     虚拟货币名称
     * @param count        商品或道具数量
     * @param callBackInfo 由游戏开发者定义传入的字符串，会与支付结果一同发送给游戏服务器，
     *                     游戏服务器可通过该字段判断交易的详细内容（金额角色等）
     * @param callBackUrl  将支付结果通知给游戏服务器时的通知地址url，交易结束后，
     * 					   系统会向该url发送http请求，通知交易的结果金额callbackInfo等信息
     * @param payResultListener  支付监听接口，隶属于gameObject对象的运行时脚本的方法名称，该方法会在收到通知后触发
     * */
#if UNITY_IOSOFFCIAL
#elif UNITY_IPHONE
    [DllImport("__Internal")]  
    private static extern void pay(IntPtr context, string gameObject, int unitPrice, string unitName,
            int count, string callBackInfo, string callBackUrl, string payResultListener);
#else
    [DllImport("gangaOnlineUnityHelper")]
    private static extern void pay(IntPtr context, string gameObject, int unitPrice, string unitName,
            int count, string callBackInfo, string callBackUrl, string payResultListener);
#endif
    

    /**
     * payExtend接口用于用户触发定额计费
     * @param context      未用到的参数，为与android的接口保持一致，可以传固定的值
     * @param gameObject   游戏场景中的对象
     * @param unitPrice    游戏道具单位价格，单位-分
     * @param unitName     虚拟货币名称
     * @param itemCode     商品ID
     * @param remain       商品自定义参数
     * @param count        商品或道具数量
     * @param callBackInfo 由游戏开发者定义传入的字符串，会与支付结果一同发送给游戏服务器，
     *                     游戏服务器可通过该字段判断交易的详细内容（金额角色等）
     * @param callBackUrl  将支付结果通知给游戏服务器时的通知地址url，交易结束后，
     * 					   系统会向该url发送http请求，通知交易的结果金额callbackInfo等信息
     * @param payResultListener  支付监听接口，隶属于gameObject对象的运行时脚本的方法名称，该方法会在收到通知后触发
     * */
#if UNITY_IOSOFFCIAL
#elif UNITY_IPHONE
    [DllImport("__Internal")]  
    private static extern void payExtend(IntPtr context, string gameObject, int unitPrice, string unitName,
        string itemCode, string remain, int count, string callBackInfo, string callBackUrl, string payResultListener);
#else
    [DllImport("gangaOnlineUnityHelper")]
    private static extern void payExtend(IntPtr context, string gameObject, int unitPrice, string unitName,
        string itemCode, string remain, int count, string callBackInfo, string callBackUrl, string payResultListener);
#endif
    

    /**
     * 部分渠道如UC渠道，要对游戏人物数据进行统计，而且为接入规范，调用时间：在游戏角色登录成功后调用
     *  public static void setRoleData(Context context, String roleId，
     *  	String roleName, String roleLevel, String zoneId, String zoneName)
     *  
     *  @param context   未用到的参数，为与android的接口保持一致，可以传固定的值
     *  @param roleId    角色唯一标识
     *  @param roleName  角色名
     *  @param roleLevel 角色等级
     *  @param zoneId    区域唯一标识
     *  @param zoneName  区域名称
     * */
    //setRoleData接口用于部分渠道如UC渠道，要对游戏人物数据进行统计，接入规范：在游戏角色登录成功后
#if UNITY_IOSOFFCIAL
#elif UNITY_IPHONE
    [DllImport("__Internal")]  
    private static extern void setRoleData(IntPtr context, string roleId,
            string roleName, string roleLevel, string zoneId, string zoneName);
#else
    [DllImport("gangaOnlineUnityHelper")]
    private static extern void setRoleData(IntPtr context, string roleId,
            string roleName, string roleLevel, string zoneId, string zoneName);
#endif
    

    //备用接口
#if UNITY_IOSOFFCIAL
#elif UNITY_IPHONE
    [DllImport("__Internal")]  
    private static extern void setData(IntPtr context, string key, string value);
#else
    [DllImport("gangaOnlineUnityHelper")]
    private static extern void setData(IntPtr context, string key, string value);
#endif
    

    /**
         *	setLoginListener方法用于设置登陆监听
         * 初始化SDK
         *  @param context      未用到的参数，为与android的接口保持一致，可以传固定的值
         *  @param gameObject	游戏场景中的对象，SDK内部完成登陆登出逻辑后，把结果通知到Unity，   故游戏开发者需要指定一个游戏对象和该对象的运行脚本，用于侦听SDK的登入登出结果
         * @param listener      登录的监听函数，隶属于gameObject对象的运行时脚本的方法名称，该方法会在收到通知后触发 */

#if UNITY_IOSOFFCIAL
#elif UNITY_IPHONE
    [DllImport("__Internal")]  
    private static extern void setLoginListener(IntPtr context, string gameObject, string listener);
#else
    [DllImport("gangaOnlineUnityHelper")]
    private static extern void setLoginListener(IntPtr context, string gameObject, string listener);
#endif
    

    /**
     *	extend扩展接口
     * 扩展接口
     *  @param context      未用到的参数，为与android的接口保持一致，可以传固定的值
     * @param data          data
     *  @param gameObject	游戏场景中的对象，
     * @param listener      扩展的监听函数，隶属于gameObject对象的运行时脚本的方法名称，该方法会在收到通知后触发
     */
#if UNITY_IOSOFFCIAL
#elif UNITY_IPHONE
    [DllImport("__Internal")]  
    private static extern void extendInterface(IntPtr context, string data, string gameObject, string listener);
#else
    [DllImport("gangaOnlineUnityHelper")]
    private static extern void extendInterface(IntPtr context, string data, string gameObject, string listener);
#endif
    

    /**
     *	isMusicEnabled方法用于判断SDK是否需要打开游戏声音，目前只有移动基地需要此接口，
     *  游戏开发者需要根据该返回值，设定游戏背景音乐是否开启
     *
     *  @param context      未用到的参数，为与android的接口保持一致，可以传固定的值
     */
#if UNITY_IOSOFFCIAL
#elif UNITY_IPHONE
    [DllImport("__Internal")]  
    private static extern bool isMusicEnabled();
#else
    [DllImport("gangaOnlineUnityHelper")]
    private static extern bool isMusicEnabled();
#endif
    

    /*void OnGUI ()
    {
        if (guiSkin) {   
            GUI.skin = guiSkin;
        } 
        windowRect = GUI.Window (0, windowRect, DoMyWindow, str);

    }*/

    string createQueryURL(string orderId)
    {
        if (user == null)
        {
            return null;
        }
        StringBuilder builder = new StringBuilder();
        builder.Append(CP_PAY_CHECK_URL);
        builder.Append("?orderId=");
        builder.Append(orderId);
        return builder.ToString();


    }


    //bool startcheck;  

    /// <summary>
    /// 初始化登陆
    /// </summary>
    public void InitSDK()
    {
#if KY
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            onCreate_listener((IntPtr)1, "MainCamera", "InitResponse");
        }
#elif UNITY_ANDROID
        onCreate_listener((IntPtr)1, "MainCamera", "InitResponse");
#endif
    }


    /// <summary>
    /// 登陆sdk
    /// </summary>
    public void Logout()
    {
#if UNITY_ANDROID
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject curActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                Debug.Log("logout");
                if (!bLogined)
                {
                    return;
                }
                logout(curActivity.GetRawObject(), "");
            }
        }
#endif
    }


    /// <summary>
    /// 登陆sdk
    /// </summary>
    public void Login()
    {
#if KY
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Debug.Log("login");
            setLoginListener((IntPtr)1, "MainCamera", "LoginResult");
            login((IntPtr)1, "Login");
        }
#elif UNITY_ANDROID
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            Debug.Log("login 1");
            using (AndroidJavaObject curActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                Debug.Log("login 2");
                login(curActivity.GetRawObject(), "Login");
            }
        }
#endif
    }

    /// <summary>
    /// 充值
    /// </summary>
    /// <param name="productPrice">商品价格，分为单位</param>
    /// <param name="productName">商品名字</param>
    /// <param name="productCount">商品数量</param>
    /// <param name="extString">额外字段</param>
    public void Pay(int productPrice, string productName, int productCount, string extString)
    {
#if KY
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            if (!bLogined)
            {
                Debug.Log("please login first");
                return;
            }
            pay((IntPtr)1, "MainCamera", productPrice, productName, productCount, extString, CP_PAY_SYNC_URL, "PayResult");
        }

#elif UNITY_ANDROID

        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            using (AndroidJavaObject curActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                if (!bLogined)
                {
                    return;
                }
                Debug.Log("pay");
                pay(curActivity.GetRawObject(), "MainCamera", productPrice, productName, productCount, extString, CP_PAY_SYNC_URL, "PayResult");
            }
        }
#endif
    }

    // Use this for initialization
    void Start()
    {
        Debug.Log("snowfish Start goodName:" + goodName + " money:" + money.ToString() + " desc:" + desc + " paytype:" + paytype);
        //windowFunction = DoMyWindow;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Backspace) || Input.GetKey(KeyCode.Escape))
        {
#if KY
            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                exitInterface((IntPtr)1, "MainCamera", "ExitResult");
            }
#elif UNITY_ANDROID
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject curActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    exit(curActivity.GetRawObject(), "MainCamera", "ExitResult");
                }
            }
#endif
        }
    }
    void extendCallback(string result)
    {
        Debug.Log("扩展回调：" + result);
    }
    void callback1(string result)
    {
        Debug.Log("------------callback1=" + result);
        SFJSONObject sfjson = new SFJSONObject(result);
        string tag = (string)sfjson.get("tag");
        string value = (string)sfjson.get("value");
        Debug.Log("tag:" + tag + " value:" + value);
        if (tag.Equals("success"))
        {
            Debug.Log("成功");
        }
        else
        {
            Debug.Log("失败");
        }
    }
    //初始化回调
    void InitResponse(string result)
    {
        Debug.Log("------------InitResponse=" + result);
        SFJSONObject sfjson = new SFJSONObject(result);
        string tag = (string)sfjson.get("tag");
        string value = (string)sfjson.get("value");
        Debug.Log("tag:" + tag + " value:" + value);
        if (tag.Equals("success"))
        {
            Debug.Log("init 成功");
            GameObject.Find("MainCamera").GetComponent<HuaWeiGameCenter>().IsInit = true;
            Login();
        }
        else
        {
            Debug.Log("init 失败");
        }

    }
    void callback2(string result)
    {
        Debug.Log("------------callback2=" + result);
        SFJSONObject sfjson = new SFJSONObject(result);
        string tag = (string)sfjson.get("tag");
        string value = (string)sfjson.get("value");
        Debug.Log("tag:" + tag + " value:" + value);
        if (tag.Equals("success"))
        {
            Debug.Log("成功");
        }
        else
        {
            Debug.Log("失败");
        }
    }
    // 支付监听函数
    void PayResult(string result)
    {
        Debug.Log("------------PayResult=" + result);
        SFJSONObject sfjson = new SFJSONObject(result);
        string type = (string)sfjson.get("result");
        string data = (string)sfjson.get("data");

        if (APaymentHelper.PayResult.PAY_SUCCESS == type)
        {
            str = "pay result = pay success " + data;
        }
        else if (APaymentHelper.PayResult.PAY_FAILURE == type)
        {
            str = "pay result = pay failure" + data;
        }
        else if (APaymentHelper.PayResult.PAY_ORDER_NO == type)
        {
            str = "pay result = pay order No" + data;
        }
    }
    void doLogin()
    {
#if KY
        login((IntPtr)1, "Login");
#elif UNITY_ANDROID
        login((IntPtr)1, "Login");
#endif
    }
    // 登陆监听函数
    void LoginResult(string result)
    {

        Debug.Log("------------loginResult=" + result);
        SFJSONObject sfjson = new SFJSONObject(result);
        string type = (string)sfjson.get("result");
        string customParams = (string)sfjson.get("customParams");
        if (APaymentHelper.LoginResult.LOGOUT == type)
        {
            Debug.Log("## type = LOGOUT");
            str = "login result = logout" + customParams;
            user = null;
            if (!isDebug)
            {
                bLogined = false;
            }
            PlayerPrefs.SetInt("Relogin", 1);
            Debug.LogError("Set Relogin 1");
            AsyncOperation async = Application.LoadLevelAsync("Downloader");
            //Invoke("doLogin", 0.2f); 

        }
        else if (APaymentHelper.LoginResult.LOGIN_SUCCESS == type)
        {
            Debug.Log("## type =  LOGIN_SUCCESS");
            SFJSONObject userinfo = (SFJSONObject)sfjson.get("userinfo");
            Debug.Log("## type =  LOGIN_SUCCESS userinfo = " + userinfo);
            if (userinfo != null)
            {
                long id = long.Parse((string)userinfo.get("id"));
                Debug.Log("## type =  LOGIN_SUCCESS id = " + id);
                string channelId = (string)userinfo.get("channelid");
                string ChannelUserId = (string)userinfo.get("channeluserid");
                string UserName = (string)userinfo.get("username");
                string Token = (string)userinfo.get("token");
                string ProductCode = (string)userinfo.get("productcode");
                user = new SFOnlineUser(id, channelId, ChannelUserId,
                                                            UserName, Token, ProductCode);
                Debug.Log("## id:" + id + " channelId:" + channelId + " ChannelUserId:" + ChannelUserId
                    + " UserName:" + UserName + " Token:" + Token + " ProductCode:" + ProductCode);
            }

            str = "login result = login success" + customParams;
            StartCoroutine(LoginCheck());
        }
        else if (APaymentHelper.LoginResult.LOGIN_FAILED == type)
        {
            str = "login result = login failed" + customParams;
            Debug.Log("## type = login failed");
        }
    }
    //ExitResult 退出监听函数
    void ExitResult(string result)
    {
        Debug.Log("------------ExitResult=" + result);
        SFJSONObject sfjson = new SFJSONObject(result);
        string type = (string)sfjson.get("result");
        string data = (string)sfjson.get("data");

        if (APaymentHelper.ExitResult.SDKEXIT == type)
        {
            //SDK退出
            if (data.Equals("true"))
            {
                Application.Quit();
            }
            else if (data.Equals("false"))
            {

            }
        }
        else if (APaymentHelper.ExitResult.SDKEXIT_NO_PROVIDE == type)
        {
            //游戏自带退出界面
            Application.Quit();
        }
    }

    void Awake()
    {
#if UNITY_ANDROID
        if (Application.platform == RuntimePlatform.Android)
        {
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                using (AndroidJavaObject curActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
                {
                    setLoginListener(curActivity.GetRawObject(), "MainCamera", "LoginResult");
                }
            }
        }
#endif
        /*goodName = PlayerPrefs.GetString ("goodName", "");   
        money = PlayerPrefs.GetInt ("money", 0);   
        string itemCode =  PlayerPrefs.GetString ("goodid", "");   
        string remain =  PlayerPrefs.GetString ("remain", "");   
        Debug.Log ("itemCode=" +itemCode+" remain="+remain);
        bLogined = PlayerPrefs.GetInt ("login", 0) == 0 ? false : true;
        if(goodName.Equals("") || money == 0)
        {
           desc = ""; 
           paytype = ""; 
        } 
        else 
        {
           desc = PlayerPrefs.GetString ("desc", ""); 
           paytype = PlayerPrefs.GetString ("paytype", ""); 
		
        }

        PlayerPrefs.DeleteKey("login");

        Debug.Log ("Awake");

        if (bLogined == true) 
        {
            str = "login result = login success";
            if (paytype.Equals (PAYTYPE_PAY)) {
                pay ((IntPtr)1, "Main Camera", money, goodName, 1, desc, CP_PAY_SYNC_URL, "PayResult");
            } else if (paytype.Equals (PAYTYPE_CHARGE)) {
                charge ((IntPtr)1, "Main Camera", goodName, money, 1, desc, CP_PAY_SYNC_URL, "PayResult");
            } else if(paytype.Equals (PAYTYPE_PAY_EXTEND)){
                payExtend ((IntPtr)1, "Main Camera", money, goodName, itemCode, remain, 
                    1, desc, CP_PAY_SYNC_URL, "PayResult"); 
            }
        }	

        isPause = false;

        isFocus = false;*/

        instance = this;
    }

    IEnumerator LoginCheck()
    {
        if (isDebug == true)
        {
            bLogined = true;
            yield return 0;
        }

        string url = createLoginURL();
        Debug.Log("LoginCheck url:" + url);
        if (url == null)
            yield return 0;
        //string result = executeHttpGet(url);

        //WWWForm form = new WWWForm();
        //form.AddField("tokenKey", UrlEncode(result));
        WWW www = new WWW(url);
        yield return www;
        Debug.Log("LoginCheck result:" + www.text);
        if (www.text != "SUCCESS" && false)
        { //|| !(result == "SUCCESS")
            bLogined = false;

        }
        else
        {
            bLogined = true;

            string Account = user.getChannelId() + "_" + user.getChannelUserId().ToString();
            string Password = "t_" + user.getChannelUserId().ToString();
            Debug.Log(Account + " " + Password);

            NetworkHandler.instance.Account = Account;
            NetworkHandler.instance.Password = Password;

            ObscuredPrefs.SetString("Account", Account);
            ObscuredPrefs.SetString("Password", Account);

            NetworkHandler.instance.SendProcess("1001#" + Account + ";" + Password + ";");

            /*setRoleData ((IntPtr)1, "1", "hunter", "30", "1", "1");
			
            SFJSONObject gameinfo = new SFJSONObject();
            gameinfo.put("roleId", "1");
            gameinfo.put("roleName", "猎人");
            gameinfo.put("roleLevel", "100");
            gameinfo.put("zoneId", "1");
            gameinfo.put("zoneName", "阿狸一区");

            setData((IntPtr)1,"gameinfo",gameinfo.ToString());*/

        }

    }

    /// <summary>
    /// 设置角色信息
    /// </summary>
    /// <param name="roleID">角色ID</param>
    /// <param name="roleName">角色名字</param>
    /// <param name="roleLevel">角色等级</param>
    /// <param name="serverId">服务器ID</param>
    /// <param name="serverName">服务器名字</param>
    public void SetGameDate(string roleID, string roleName, string roleLevel, string serverId, string serverName, string keyName)
    {
        serverId = serverId.Substring(8);
#if KY
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Debug.Log("SetGameDate");
            SFJSONObject gameinfo = new SFJSONObject();
            gameinfo.put("roleId", roleID);
            gameinfo.put("roleName", roleName);
            gameinfo.put("roleLevel", roleLevel);
            gameinfo.put("zoneId", serverId);
            gameinfo.put("zoneName", serverName);

            setData((IntPtr)1, "gameinfo", gameinfo.ToString());
            setRoleData((IntPtr)1, roleID, roleName, roleLevel, serverId, serverName);
        }

#elif UNITY_ANDROID
        using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
        {
            Debug.Log("login 1");
            using (AndroidJavaObject curActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            {
                SFJSONObject gameinfo = new SFJSONObject();
                gameinfo.put("roleId", roleID);
                gameinfo.put("roleName", roleName);
                gameinfo.put("roleLevel", roleLevel);
                gameinfo.put("zoneId", serverId);
                gameinfo.put("zoneName", serverName);

                setData(curActivity.GetRawObject(), keyName, gameinfo.toString());

                setRoleData(curActivity.GetRawObject(), roleID, roleName, roleLevel, serverId, serverName);
            }
        }
#endif
    }

    private string createLoginURL()
    {
        if (user == null)
        {
            return null;
        }
        StringBuilder builder = new StringBuilder();
        builder.Append(CP_LOGIN_CHECK_URL);

        builder.Append("?app=");
        builder.Append(user.getProductCode());
        builder.Append("&sdk=");
        builder.Append(user.getChannelId());
        builder.Append("&uin=");
        builder.Append(ToUrlEncode(user.getChannelUserId()));//(Base64.EncodeBase64(user.getChannelUserId()));
        builder.Append("&sess=");
        builder.Append(ToUrlEncode(user.getToken()));//(Base64.EncodeBase64(user.getToken()));
        return builder.ToString();
    }

    public void addOrderId(string orderId)
    {
        if (isDebug)
        {
            return;
        }
        OderId id = new OderId();
        id.retry = 0;
        id.orderId = orderId;
        orderIds.Add(id);
        if (isQuerying)
            return;

        query();
    }

    public void query()
    {
        doQuery();
    }

    public void doQuery()
    {
        if (orderIds != null)
        {//orderIds != null
            OderId oderId = (OderId)orderIds[0];
            if ((object)oderId != null)
            {
                if (oderId.retry > 10)
                {
                    orderIds.RemoveAt(0);
                }
                oderId = (OderId)orderIds[0];
                if (oderId.Equals(null))
                    return;

                oderId.retry++;
                String str = createQueryURL(oderId.orderId);
                if (str == null)
                {
                    String result = "SUCCESS";//executeHttpGet(str);
                    if (result == null || !(result == "SUCCESS"))
                    {
                    }
                    else
                    {
                        orderIds.RemoveAt(0);
                        Debug.Log("user had payed oderId:" + oderId.orderId);

                    }
                }

                if (orderIds.Count != 0)
                {
                    query();
                }
            }
        }
    }

    public static string executeHttpGet(string str)
    {
        WebClient myWebClient = new WebClient();
        //myWebClient.Headers.Add("Content-Type", "multipart/form-data; ");  
        byte[] b = myWebClient.DownloadData(str);
        return (Encoding.UTF8.GetString(b));
    }

    public string HexToStr(string mHex) // 返回十六进制代表的字符串 
    {
        mHex = mHex.Replace(" ", "");
        if (mHex.Length <= 0)
            return "";
        byte[] vBytes = new byte[mHex.Length / 2];
        for (int i = 0; i < mHex.Length; i += 2)
            if (!byte.TryParse(mHex.Substring(i, 2), NumberStyles.HexNumber, null, out vBytes[i / 2]))
                vBytes[i / 2] = 0;
        return ASCIIEncoding.Default.GetString(vBytes);
    } /* HexToStr */
    public string StrToHex(string mStr) //返回处理后的十六进制字符串 
    {
        return BitConverter.ToString(ASCIIEncoding.Default.GetBytes(mStr)).Replace("-", "");
    }

    public static string ToUrlEncode(string strCode)
    {
        StringBuilder sb = new StringBuilder();
        byte[] byStr = System.Text.Encoding.UTF8.GetBytes(strCode); //默认是System.Text.Encoding.Default.GetBytes(str) 
        System.Text.RegularExpressions.Regex regKey = new System.Text.RegularExpressions.Regex("^[A-Za-z0-9]+$");
        for (int i = 0; i < byStr.Length; i++)
        {
            string strBy = Convert.ToChar(byStr[i]).ToString();
            if (regKey.IsMatch(strBy))
            {
                //是字母或者数字则不进行转换  
                sb.Append(strBy);
            }
            else
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }
        }
        return (sb.ToString());
    }
}
