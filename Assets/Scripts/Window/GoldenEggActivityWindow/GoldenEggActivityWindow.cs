using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GoldenEggActivityWindow : MonoBehaviour
{
    public GameObject exitBtn;
    public GameObject helpBtn;
    public GameObject helpInfo;

    public GameObject egg_zaPrefab;
    /// <summary>
    /// 显示活动时间段
    /// </summary>
    public UILabel activityTimeLabel;
    /// <summary>
    /// 显示倒计时时间
    /// </summary>
    public UILabel countDownTimeLabel;

    public GameObject leftAgg;
    public GameObject centerAgg;
    public GameObject rightAgg;
    /// <summary>
    /// 今日收益总数
    /// </summary>
    public UILabel diamondCountLabel;
    /// <summary>
    /// 显示剩余锤子个数
    /// </summary>
    public UILabel chuiZiCountLabel;
    /// <summary>
    /// 今日可获取锤子总数
    /// </summary>
    public UILabel availableChongZhiLabel;
    public GameObject availableChongZhiBtn;

    //public GameObject advanceWindowPerfab;

    //RewardPaneal
    public GameObject RewardPaneal;
    public UILabel rewardDiamondNumLabel;
    public UILabel rewardLabel;
    public GameObject ReceiveBtn;
    /// <summary>
    /// 记录锤子的位置图片
    /// </summary>
    public GameObject hammerPosition;
    /// <summary>
    /// 记录当前玩家的名字
    /// </summary>
    private string playerUserName = null;
    /// <summary>
    /// 记录当前砸取的哪一个金蛋
    /// -1 代表没有砸取金蛋，0表示砸取LeftAgg，1表示砸取CenterAgg，2表示RightAgg
    /// </summary>
    private int goldAggObj = -1;
    /// <summary>
    /// 记录玩家砸取的次数
    /// 0表示一个都没有砸，1表示砸一个，2表示砸两个，3表示全部砸完了
    /// </summary>
    public int breakCount = 0;
    /// <summary>
    /// 记录是否已经砸金蛋了，
    /// true表示可以砸，false表示不可以砸等待服务器返回
    /// </summary>
    public bool isBreak = false;
    /// <summary>
    /// 记录活动时间
    /// </summary>
    private string activityTime;
    /// <summary>
    /// 记录当前活动剩余事件倒计时
    /// </summary>
    private int totalTimeSeconds = 1000;
    /// <summary>
    /// 记录已经砸取了多少次
    /// </summary>
    private int alreadyBreak = 0;
    /// <summary>
    /// 记录今日收益
    /// </summary>
    private int todayEarnings = 0;
    /// <summary>
    /// 记录剩余锤子
    /// </summary>
    private int remainingHammer = 0;
    /// <summary>
    /// 今日可获取的锤子数
    /// </summary>
    private int todayHammer = 0;

    private List<Item> itemList = new List<Item>();

    private GameObject presentGameObject;

    GameObject leftTotal;
    GameObject leftBroken;
    GameObject centerTotal;
    GameObject centerBroken;
    GameObject rightTotal;
    GameObject rightBroken;

    Camera uiCamera;

    /// <summary>
    /// 获取服务器发来的消息，设置当前面板的信息
    /// </summary>
    /// <param name="activityTime">活动时间</param>
    /// <param name="countdownTime">倒计时总时长（秒计算）</param>
    /// <param name="remainHammerCount">剩余锤子总数</param>
    /// <param name="todayEarnings">今日收益</param>
    /// <param name="todayAvailableHammerCount">今日可获取锤子数量</param>
    /// <param name="hasHitCount">已经砸取次数</param>
    public void InitSetWindowInfo(string activityTime, int countdownTime, int remainHammerCount, int todayEarnings, int todayAvailableHammerCount, string position, int hasHitCount)
    {
        playerUserName = CharacterRecorder.instance.characterName;
        //活动时间
        activityTimeLabel.text = activityTime;
        //剩余锤子数量
        chuiZiCountLabel.text = remainHammerCount.ToString();
        //活动剩余时间倒计时
        totalTimeSeconds = countdownTime;
        //今日收益数量
        diamondCountLabel.text = todayEarnings.ToString();
        //今日可获取锤子数量+
        availableChongZhiLabel.text = todayAvailableHammerCount.ToString();
        string[] breakenEgg = position.Split('$');
        GoldEggBreaken(breakenEgg);

        //今日已经砸取次数
        alreadyBreak = hasHitCount;

        isBreak = true;

        if (!IsInvoking("GoldEggUpdateTime"))
        {
            InvokeRepeating("GoldEggUpdateTime", 0, 1.0f);
        }

        SetInitPanel();
    }
    /// <summary>
    /// Update
    /// </summary>
    void Update()
    {
        if (uiCamera != null && isBreak)
        {
            Vector3 nowPos = uiCamera.WorldToScreenPoint(hammerPosition.transform.position);

            Vector3 mousePos = Input.mousePosition;

            Vector3 newSPos = new Vector3(mousePos.x, mousePos.y, nowPos.z);

            Vector3 newWPos = uiCamera.ScreenToWorldPoint(newSPos);

            //hammerPosition.transform.position = newWPos;

            if (Input.GetMouseButtonDown(0))
            {
                hammerPosition.transform.position = newWPos;
                StartCoroutine("SetHammerAnimation");
            }
        }
    }
    /// <summary>
    /// 初始化界面的Btn按钮的点击事件
    /// </summary>
    public void InitBtn_OnClick()
    {
        if (UIEventListener.Get(helpBtn).onClick == null)
        {
            UIEventListener.Get(helpBtn).onClick += delegate(GameObject go)
            {
                helpInfo.SetActive(true);
            };
        }
        GameObject helpExitBtn = helpInfo.transform.FindChild("All/ExitBtn").gameObject;
        if (UIEventListener.Get(helpExitBtn).onClick == null)
        {
            UIEventListener.Get(helpExitBtn).onClick += delegate(GameObject go)
            {
                helpInfo.SetActive(false);
            };
        }
        if (UIEventListener.Get(availableChongZhiBtn).onClick == null)
        {
            UIEventListener.Get(availableChongZhiBtn).onClick += delegate(GameObject go)
            {
                //点击充值跳转
                //PictureCreater.instance.DestroyAllComponent();
                //UIManager.instance.OpenSinglePanel("VIPShopWindow", true);
                UIManager.instance.OpenPanel("VIPShopWindow", true);
            };
        }
        //if (UIEventListener.Get(RewardPaneal).onClick == null)
        //{
        //    UIEventListener.Get(RewardPaneal).onClick += delegate(GameObject go)
        //    {
        //        RewardPaneal.SetActive(false);
        //        if (breakCount == 3)
        //        {
        //            breakCount = 0;
        //            InitSetGoldAggInfo();
        //            GoldAggOrder();
        //        }
        //    };
        //}
        if (UIEventListener.Get(ReceiveBtn).onClick == null)
        {
            UIEventListener.Get(ReceiveBtn).onClick += delegate(GameObject go)
            {
                Debug.LogError("ReceiveBtn: ");
                RewardPaneal.SetActive(false);
                if (breakCount == 3)
                {

                }
            };
        }
    }
    /// <summary>
    /// 咋完三个 金蛋后重置
    /// </summary>
    public void InitBreakCount()
    {
        breakCount = 0;
        // InitSetGoldAggInfo();
        // GoldEggOrder();
        NetworkHandler.instance.SendProcess("9701#;");
    }

    public void SetInitPanel()
    {
        InitBtn_OnClick();
        InitGoldAggBtn_OnClick();

        InvokeRepeating("StartSetGoldEggAnimation", 0, 2.7f);
    }
    void Start()
    {
        if (GameObject.Find("UIRoot/Camera") != null)
        {
            uiCamera = GameObject.Find("UIRoot/Camera").GetComponent<Camera>();
        }
        leftTotal = leftAgg.transform.Find("TotalSprite").gameObject;
        leftBroken = leftAgg.transform.Find("BrokenSprite").gameObject;
        centerTotal = centerAgg.transform.Find("TotalSprite").gameObject;
        centerBroken = centerAgg.transform.Find("BrokenSprite").gameObject;
        rightTotal = rightAgg.transform.Find("TotalSprite").gameObject;
        rightBroken = rightAgg.transform.Find("BrokenSprite").gameObject;

        hammerPosition.SetActive(false);

        if (UIEventListener.Get(exitBtn).onClick == null)
        {
            UIEventListener.Get(exitBtn).onClick += delegate(GameObject go)
            {
                DestroyImmediate(this.gameObject);
                //UIManager.instance.BackUI();
            };
        }
        InitSetGoldAggInfo();
        GoldEggOrder();
        NetworkHandler.instance.SendProcess("9701#;");
        //NetworkHandler.instance.SendProcess("9701#;");
        //发送协议获取当前面板的信息
        //InitSetWindowInfo("08月19日05时—08月19日05时", 1000, 1, 100, 10, 1);       
    }
    /// <summary>
    /// 初始化获取已经砸取的金蛋
    /// </summary>
    /// <param name="breakenEgg"></param>
    void GoldEggBreaken(string[] breakenEgg)
    {
        for (int i = 0; i < breakenEgg.Length - 1; i++)
        {
            int breaken = int.Parse(breakenEgg[i]);
            if (breaken == 1)//表示已经砸取
            {
                switch (i)
                {
                    case 0:
                        leftTotal.SetActive(false);
                        leftBroken.SetActive(true);
                        break;
                    case 1:
                        centerTotal.SetActive(false);
                        centerBroken.SetActive(true);
                        break;
                    case 2:
                        rightTotal.SetActive(false);
                        rightBroken.SetActive(true);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (i)
                {
                    case 0:
                        leftTotal.SetActive(true);
                        leftBroken.SetActive(false);
                        break;
                    case 1:
                        centerTotal.SetActive(true);
                        centerBroken.SetActive(false);
                        break;
                    case 2:
                        rightTotal.SetActive(true);
                        rightBroken.SetActive(false);
                        break;
                    default:
                        break;
                }
            }
        }
    }

    /// <summary>
    /// 砸金蛋的时候的金蛋点击事件响应
    /// </summary>
    public void InitGoldAggBtn_OnClick()
    {
        #region leftAgg
        if (UIEventListener.Get(leftAgg).onClick == null)
        {
            UIEventListener.Get(leftAgg).onClick += delegate(GameObject go)
            {
                if (totalTimeSeconds <= 0)
                {
                    UIManager.instance.OpenPromptWindow("活动已结束!", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                //SetHemmerPosition(go);
                if (isBreak)// && alreadyBreak > 0)
                {
                    if (int.Parse(chuiZiCountLabel.text) <= 0)
                    {
                        UIManager.instance.OpenPromptWindow("所需锤子不足，可通过充值获取更多.", PromptWindow.PromptType.Hint, null, null);
                        return;
                    }
                    else if (CharacterRecorder.instance.lunaGem < 10)
                    {
                        UIManager.instance.OpenPromptWindow("所需钻石不足.", PromptWindow.PromptType.Hint, null, null);
                        return;
                    }
                    //砸取左边金蛋
                    goldAggObj = 1;
                    GameObject total = leftAgg.transform.Find("TotalSprite").gameObject;
                    GameObject broken = leftAgg.transform.Find("BrokenSprite").gameObject;
                    if (total.activeSelf && !broken.activeSelf)
                    {//表示没有砸取的金蛋
                        isBreak = false;
                        breakCount++;//表示成功砸取金蛋
                        total.SetActive(false);
                        broken.SetActive(true);
                        BreakenEggSuccess(go);
                    }
                    else//表这个金蛋已经砸取或者不可砸取
                    {
                        UIManager.instance.OpenPromptWindow("已经砸取过了，其他的可能有更多惊喜.", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else
                {
                    if (alreadyBreak <= 0)
                    {
                        //UIManager.instance.OpenPromptWindow("今日砸取金蛋的次数已经达到上限，请明天再来吧.", PromptWindow.PromptType.Hint, null, null);
                    }
                }
            };
        }
        #endregion
        #region  centerAgg
        if (UIEventListener.Get(centerAgg).onClick == null)
        {
            UIEventListener.Get(centerAgg).onClick += delegate(GameObject go)
            {
                if (totalTimeSeconds <= 0)
                {
                    UIManager.instance.OpenPromptWindow("活动已结束!", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                //SetHemmerPosition(go);
                if (isBreak)// && alreadyBreak > 0)
                {
                    if (int.Parse(chuiZiCountLabel.text) <= 0)
                    {
                        UIManager.instance.OpenPromptWindow("所需锤子不足，可通过充值获取更多.", PromptWindow.PromptType.Hint, null, null);
                        return;
                    }
                    else if (CharacterRecorder.instance.lunaGem < 10)
                    {
                        UIManager.instance.OpenPromptWindow("所需钻石不足.", PromptWindow.PromptType.Hint, null, null);
                        return;
                    }
                    //砸取中间边金蛋

                    goldAggObj = 2;
                    GameObject total = centerAgg.transform.Find("TotalSprite").gameObject;
                    GameObject broken = centerAgg.transform.Find("BrokenSprite").gameObject;
                    if (total.activeSelf && !broken.activeSelf)
                    {//表示没有砸取的金蛋
                        isBreak = false;
                        breakCount++;//表示成功砸取金蛋
                        total.SetActive(false);
                        broken.SetActive(true);
                        BreakenEggSuccess(go);
                    }
                    else//表这个金蛋已经砸取或者不可砸取
                    {
                        UIManager.instance.OpenPromptWindow("已经砸取过了，其他的可能有更多惊喜.", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else
                {
                    if (alreadyBreak <= 0)
                    {
                        // UIManager.instance.OpenPromptWindow("今日砸取金蛋的次数已经达到上限，请明天再来吧.", PromptWindow.PromptType.Hint, null, null);
                    }
                }
            };
        }
        #endregion
        #region rightAgg
        if (UIEventListener.Get(rightAgg).onClick == null)
        {
            UIEventListener.Get(rightAgg).onClick += delegate(GameObject go)
            {
                if (totalTimeSeconds <= 0)
                {
                    UIManager.instance.OpenPromptWindow("活动已结束!", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                //SetHemmerPosition(go);
                if (isBreak)// && alreadyBreak > 0)
                {
                    if (int.Parse(chuiZiCountLabel.text) <= 0)
                    {
                        UIManager.instance.OpenPromptWindow("所需锤子不足，可通过充值获取更多.", PromptWindow.PromptType.Hint, null, null);
                        return;
                    }
                    else if (CharacterRecorder.instance.lunaGem < 10)
                    {
                        UIManager.instance.OpenPromptWindow("所需钻石不足.", PromptWindow.PromptType.Hint, null, null);
                        return;
                    }
                    //砸取右边金蛋
                    goldAggObj = 3;
                    GameObject total = rightAgg.transform.Find("TotalSprite").gameObject;
                    GameObject broken = rightAgg.transform.Find("BrokenSprite").gameObject;
                    if (total.activeSelf && !broken.activeSelf)
                    {//表示没有砸取的金蛋
                        isBreak = false;
                        breakCount++;//表示成功砸取金蛋
                        total.SetActive(false);
                        broken.SetActive(true);
                        BreakenEggSuccess(go);
                    }
                    else//表这个金蛋已经砸取或者不可砸取
                    {
                        UIManager.instance.OpenPromptWindow("已经砸取过了，其他的可能有更多惊喜.", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else
                {
                    if (alreadyBreak <= 0)
                    {
                        //UIManager.instance.OpenPromptWindow("今日砸取金蛋的次数已经达到上限，请明天再来吧.", PromptWindow.PromptType.Hint, null, null);
                    }
                }
            };
        }
        #endregion
    }

    /// <summary>
    /// 当三个金蛋全部砸完的时候重新设置金蛋
    /// </summary>
    public void InitSetGoldAggInfo()
    {
        if ((!leftTotal.activeSelf && leftBroken.activeSelf) &&
            (!centerTotal.activeSelf && centerBroken.activeSelf) &&
            (!rightTotal.activeSelf && rightBroken.activeSelf))
        {//三个金蛋全部砸完了,重置金蛋
            leftTotal.SetActive(true);
            leftBroken.SetActive(false);
            centerTotal.SetActive(true);
            centerBroken.SetActive(false);
            rightTotal.SetActive(true);
            rightBroken.SetActive(false);
        }
        else
        {
            Debug.LogError("还有没有砸取的.");
        }
    }
    void BreakenEggSuccess(GameObject go)
    {
        presentGameObject = go;
        //发送协议
        if (GetEggPosition(go.name) != 0)
        {
            AudioEditer.instance.PlayOneShot("ui_unlock");
            NetworkHandler.instance.SendProcess(string.Format("9702#{0};", GetEggPosition(go.name)));
        }
    }
    /// <summary>
    /// 砸取金蛋后启动的协程
    /// </summary>
    /// <param name="go">砸取的金蛋对象</param>
    /// <returns></returns>
    IEnumerator SetBreakEggAnimation()
    {
        GameObject egg = NGUITools.AddChild(presentGameObject, egg_zaPrefab);
        egg.transform.localPosition = Vector3.zero;
        //GoldEggReceiveMsg_9702(90002, 2000, CharacterRecorder.instance.lunaGem);
        yield return new WaitForSeconds(1.2f);
        Destroy(egg);
    }


    public int GetEggPosition(string name)
    {
        int position = 0;
        switch (name)
        {
            case "LeftSprite":
                position = 1;
                break;
            case "CenterSprite":
                position = 2;
                break;
            case "RightSprite":
                position = 3;
                break;
            default:
                break;
        }
        return position;
    }
    /// <summary>
    /// 砸取金蛋成功协议返回获取钻石数量
    /// </summary>
    /// <param name="itemId">钻石的ItemId</param>
    /// <param name="itemCount">钻石的数量</param>
    public void GoldEggReceiveMsg_9702(int itemId, int itemCount, int diamondNum)
    {
        StartCoroutine(SetBreakEggAnimation());
        StartCoroutine(StartBreakSuccess(itemId, itemCount, diamondNum));
        //if (itemCount / 10 >= 10)
        //{
        //    NetworkHandler.instance.SendProcess(string.Format("7002#40;{0};{1};{2};", playerUserName, itemCount / 10, itemCount));
        //}
    }
    /// <summary>
    /// 砸取金蛋成功，播放成功UI
    /// </summary>
    /// <returns></returns>
    IEnumerator StartBreakSuccess(int itemId, int itemCount, int diamondNum)
    {
        yield return new WaitForSeconds(0.6f);

        Item item = new Item(itemId, itemCount);
        itemList.Clear();
        itemList.Add(item);

        UIManager.instance.OpenSinglePanel("AdvanceWindow", false);
        GameObject advan = GameObject.Find("AdvanceWindow");
        advan.layer = 11;
        AdvanceWindow aw = advan.GetComponent<AdvanceWindow>();
        aw.SetInfo(AdvanceWindowType.GainResult, null, null, null, null, itemList);
        //刷新钻石的数量
        GameObject top = GameObject.Find("TopContent");
        if (top != null)
        {
            CharacterRecorder.instance.lunaGem = diamondNum;
            top.GetComponent<TopContent>().Reset();
        }

    }
    /// <summary>
    /// 设置获奖面板的信息
    /// </summary>
    public void SetRewardPanealInfo()
    {
        RewardPaneal.SetActive(true);
        rewardDiamondNumLabel.text = "2000";
        rewardLabel.text = "10";
        isBreak = true;
    }
    /// <summary>
    /// 更新倒计时时间
    /// </summary>
    void GoldEggUpdateTime()
    {
        if (totalTimeSeconds > 0)
        {
            System.TimeSpan ts = new System.TimeSpan(0, 0, System.Convert.ToInt32(totalTimeSeconds));

            //int day = totalTimeSeconds / (3600 * 24);
            //int hour = totalTimeSeconds % (3600 * 24) / 3600;
            //int min = totalTimeSeconds % 3600 / 60;
            //int sec = totalTimeSeconds % 60;
            if (ts.Days <= 0)
            {
                //if (hour <= 0)
                //{
                //    if (min <= 0)
                //    {
                //        countDownTimeLabel.text = string.Format("{0}秒", sec);
                //    }
                //    else
                //    {
                //        countDownTimeLabel.text = string.Format("{0}:{1}秒", min, sec);
                //    }
                //}
                //else
                //{
                countDownTimeLabel.text = string.Format("{0}:{1}:{2}秒", ts.Hours.ToString("00"), ts.Minutes.ToString("00"), ts.Seconds.ToString("00"));
                //}
            }
            else
            {
                countDownTimeLabel.text = string.Format("{0}天-{1}:{2}:{3}", ts.Days, ts.Hours.ToString("00"), ts.Minutes.ToString("00"), ts.Seconds.ToString("00"));
            }
            totalTimeSeconds -= 1;
        }
        else
        {
            countDownTimeLabel.text = "00:00:00秒";
        }
        //yield return new WaitForSeconds(1.0f);
        //StartCoroutine("UpdateTime");
    }
    /// <summary>
    /// 启动金蛋的动画
    /// </summary>
    void StartSetGoldEggAnimation()
    {
        StartCoroutine("SetGoldEggAnimation");
    }
    /// <summary>
    /// 金蛋的动画
    /// </summary>
    /// <returns>协程</returns>
    IEnumerator SetGoldEggAnimation()
    {
        yield return new WaitForSeconds(0.5f);

        if (leftTotal.activeSelf && !leftBroken.activeSelf)
        {
            leftTotal.transform.Rotate(new Vector3(0, 0, 8));
            yield return new WaitForSeconds(0.1f);
            leftTotal.transform.Rotate(new Vector3(0, 0, -16));
            yield return new WaitForSeconds(0.1f);
            leftTotal.transform.Rotate(new Vector3(0, 0, 8));
        }
        yield return new WaitForSeconds(0.5f);
        if (centerTotal.activeSelf && !centerBroken.activeSelf)
        {
            centerTotal.transform.Rotate(new Vector3(0, 0, 8));
            yield return new WaitForSeconds(0.1f);
            centerTotal.transform.Rotate(new Vector3(0, 0, -16));
            yield return new WaitForSeconds(0.1f);
            centerTotal.transform.Rotate(new Vector3(0, 0, 8));
        }
        yield return new WaitForSeconds(0.5f);
        if (rightTotal.activeSelf && !rightBroken.activeSelf)
        {
            rightTotal.transform.Rotate(new Vector3(0, 0, 8));
            yield return new WaitForSeconds(0.1f);
            rightTotal.transform.Rotate(new Vector3(0, 0, -16));
            yield return new WaitForSeconds(0.1f);
            rightTotal.transform.Rotate(new Vector3(0, 0, 8));
        }
    }
    /// <summary>
    /// 锤子的点击动画
    /// </summary>
    /// <returns></returns>
    IEnumerator SetHammerAnimation()
    {
        hammerPosition.SetActive(true);
        hammerPosition.transform.Rotate(new Vector3(0, 0, 15));
        yield return new WaitForSeconds(0.1f);
        hammerPosition.transform.Rotate(new Vector3(0, 0, -15));
        yield return new WaitForSeconds(0.1f);
        hammerPosition.transform.Rotate(new Vector3(0, 0, 15));
        yield return new WaitForSeconds(0.1f);
        hammerPosition.transform.Rotate(new Vector3(0, 0, -15));
        yield return new WaitForSeconds(0.1f);
        hammerPosition.SetActive(false);
    }
    /// <summary>
    /// 随机设置每个颜色金蛋的位置
    /// </summary>
    public void GoldEggOrder()
    {
        int order = Random.Range(0, 6);
        switch (order)
        {
            case 0:
                leftAgg.transform.localPosition = new Vector3(-188, 19, 0);
                centerAgg.transform.localPosition = new Vector3(0, 19, 0);
                rightAgg.transform.localPosition = new Vector3(188, 19, 0);
                break;
            case 1:
                Vector3 left = leftAgg.transform.localPosition;
                leftAgg.transform.localPosition = rightAgg.transform.localPosition;
                rightAgg.transform.localPosition = left;
                break;
            case 2:
                Vector3 left1 = leftAgg.transform.localPosition;
                leftAgg.transform.localPosition = centerAgg.transform.localPosition;
                centerAgg.transform.localPosition = left1;
                break;
            case 3:
                Vector3 center = centerAgg.transform.localPosition;
                centerAgg.transform.localPosition = rightAgg.transform.localPosition;
                rightAgg.transform.localPosition = center;
                break;
            case 4:
                Vector3 center1 = centerAgg.transform.localPosition;
                centerAgg.transform.localPosition = rightAgg.transform.localPosition;
                rightAgg.transform.localPosition = leftAgg.transform.localPosition;
                leftAgg.transform.localPosition = center1;
                break;
            case 5:
                Vector3 right = rightAgg.transform.localPosition;
                rightAgg.transform.localPosition = centerAgg.transform.localPosition;
                centerAgg.transform.localPosition = leftAgg.transform.localPosition;
                leftAgg.transform.localPosition = right;
                break;
            default:
                break;
        }
    }
}
