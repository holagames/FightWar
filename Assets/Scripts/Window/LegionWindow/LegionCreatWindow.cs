using UnityEngine;
using System.Collections;

public class LegionCreatWindow : MonoBehaviour
{

    public UIInput input;
    public GameObject CreatButton;
    public GameObject legionFlagButton;
    public GameObject GoToButton;
    public GameObject SucceedCloseButton;
    public GameObject SucceedCreatObj;
    public UILabel SucceedDesLabel;
    private int legionFlag = 1;
    // Use this for initialization
    void Start()
    {
        SucceedCreatObj.SetActive(false);
        if (UIEventListener.Get(CreatButton).onClick == null)
        {
            UIEventListener.Get(CreatButton).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.Vip < 1)
                {
                    UIManager.instance.OpenPromptWindow("VIP1以上才可以创建军团！", PromptWindow.PromptType.Hint, null, null);
                    return;
                }
                if (input.value.Replace(" ", "").IndexOf("同盟") > -1 || input.value.Replace(" ", "").IndexOf("帝国") > -1)
                {
                    UIManager.instance.OpenPromptWindow("军团名称非法", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    if (CharacterRecorder.instance.lunaGem >= 100 && input.value != "")
                    {
                        Debug.Log("军团名字:  " + input.value);
                        NetworkHandler.instance.SendProcess(string.Format("8001#{0};{1};", input.value.Replace(" ", ""), CharacterRecorder.instance.legionFlag));
                    }
                    else if (input.value != "")
                    {
                        UIManager.instance.OpenPromptWindow("钻石不足", PromptWindow.PromptType.Hint, null, null);
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("请输入军团名称", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                //UIManager.instance.OpenPromptWindow("即将开放", PromptWindow.PromptType.Alert, null, null);
            };
        }
        if (UIEventListener.Get(legionFlagButton).onClick == null)
        {
            UIEventListener.Get(legionFlagButton).onClick += delegate(GameObject go)
            {
                //Debug.Log("军团旗帜。。。");
                //UIManager.instance.OpenPromptWindow("暂未开启", PromptWindow.PromptType.Hint, null, null);
                UIManager.instance.OpenPanel("LegionFlagSettingWindow", false);
            };
        }
        if (UIEventListener.Get(GoToButton).onClick == null)
        {
            UIEventListener.Get(GoToButton).onClick += delegate(GameObject go)
            {
                Debug.Log("前往  ");
                UIManager.instance.BackUI();
                UIManager.instance.OpenPanel("LegionMainHaveWindow", true);
            };
        }
        if (UIEventListener.Get(SucceedCloseButton).onClick == null)
        {
            UIEventListener.Get(SucceedCloseButton).onClick += delegate(GameObject go)
            {
                SucceedCreatObj.SetActive(false);
            };
        }
        /*  if (UIEventListener.Get(GameObject.Find("LegionCreatCloseButton")).onClick == null)
          {
              UIEventListener.Get(GameObject.Find("LegionCreatCloseButton")).onClick += delegate(GameObject go)
              {
                  UIManager.instance.BackUI();
              };
          }*/

    }
    public void AfterSucceedCreatLegion()
    {
        SucceedDesLabel.text = "[93d8f3]恭喜您成功创建了 [3ee817]军团名字，[-]赶紧去看看吧";
        SucceedDesLabel.text = SucceedDesLabel.text.Replace("军团名字", input.value);
        SucceedCreatObj.SetActive(true);
    }
    public void RandomFlagHead()
    {
        int key = Random.Range(1, 11);
        legionFlagButton.GetComponent<UISprite>().spriteName = string.Format("legionFlag{0}", key);
    }
}
