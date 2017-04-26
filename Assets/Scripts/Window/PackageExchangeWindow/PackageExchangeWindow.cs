using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PackageExchangeWindow : MonoBehaviour
{
    public GameObject exitBtn;
    public GameObject sureBtn;
    public UIInput input;

    private List<Item> rewardList = new List<Item>();
    // Use this for initialization
    void Start()
    {
        if (UIEventListener.Get(exitBtn).onClick == null)
        {
            UIEventListener.Get(exitBtn).onClick += delegate(GameObject go)
            {
                DestroyImmediate(this.gameObject);
            };
        }
        if (UIEventListener.Get(sureBtn).onClick == null)
        {
            UIEventListener.Get(sureBtn).onClick += delegate(GameObject go)
            {
                if (string.IsNullOrEmpty(input.value))
                {
                    UIManager.instance.OpenPromptWindow("兑换码不能为空.", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                NetworkHandler.instance.SendProcess(string.Format("9511#{0};", input.value));
                //input.value = "";
            };
        }
    }
    /// <summary>
    /// 接收协议9511返回的数据
    /// </summary>
    /// <param name="diamondNum">当前的钻石数量</param>
    /// <param name="goldNum">当前金币数量</param>
    /// <param name="vipLevel">当前VIP等级</param>
    /// <param name="vipExp">当前VIP经验</param>
    /// <param name="reward">奖励</param>
    public void ReceiverMsg_9511(int diamondNum, int goldNum, int vipLevel, int vipExp, string reward)
    {
        //Debug.LogError(".....:   " + reward);
        GameObject top = GameObject.Find("TopContent");
        if (top != null)
        {
            CharacterRecorder.instance.lunaGem = diamondNum;
            CharacterRecorder.instance.gold = goldNum;
            CharacterRecorder.instance.Vip = vipLevel;
            top.GetComponent<TopContent>().Reset();
        }
        if (!string.IsNullOrEmpty(reward))
        {
            rewardList.Clear();
            string[] dataSplit = reward.Split('!');
            for (int i = 0; i < dataSplit.Length - 1; i++)
            {
                string[] data = dataSplit[i].Split('$');
                Item item = new Item(int.Parse(data[0]), int.Parse(data[1]));
                rewardList.Add(item);
            }
        }
        //Debug.LogError("Length:  " + rewardList.Count);
        if (rewardList.Count > 0)
        {
            UIManager.instance.OpenSinglePanel("AdvanceWindow", false);
            GameObject advan = GameObject.Find("AdvanceWindow");
            advan.layer = 11;
            AdvanceWindow aw = advan.GetComponent<AdvanceWindow>();
            aw.SetInfo(AdvanceWindowType.GainResult, null, null, null, null, rewardList);
        }
    }
}
