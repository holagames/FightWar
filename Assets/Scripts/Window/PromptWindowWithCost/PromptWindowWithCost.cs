using UnityEngine;
using System.Collections;

public class PromptWindowWithCost : MonoBehaviour {

    public enum PromptType
    {
        Alert,
        Confirm,
        Hint,
        Popup,
    }

    public GameObject sureBtn;

    public GameObject TipsLabel;

    public delegate void OnConfirm();

    public OnConfirm onConfirm;

    public delegate void OnCancel();

    public OnCancel onCancel;

    [SerializeField]
    UILabel messageLabel;
    [SerializeField]
    UILabel diamondCostLabel;
    [SerializeField]
    GameObject alertObject;
    [SerializeField]
    GameObject confirmObject;


    public GameObject cancelBtn;
    public GameObject cancelBtn1;

    void OnDisable()
    {
        onConfirm = null;
        onCancel = null;
        messageLabel.text = "";
    }

    public void SetPromptWindow(string message, int layer, int DiamondCost, PromptType type, OnConfirm confirmCallback, OnCancel cancelCallBack)
    {
        this.gameObject.layer = layer;
        foreach (Component c in this.gameObject.GetComponentsInChildren(typeof(Transform), true))
        {
            c.gameObject.layer = layer;
        }
        //自己可以获得      x1
        //对方可以获得      x1
        //打开礼包可以随机获得稀有物品
        messageLabel.transform.GetChild(0).gameObject.SetActive(true);
        //messageLabel.text = message;
        onConfirm = confirmCallback;
        onCancel = cancelCallBack;
        //confirmObject.GetComponent<UIButton>().onClick += new EventDelegate (OnConfirmButtonClick());
     
       //switch (type)
       //{
       //    case PromptType.Alert:
       //        NGUITools.SetActive(alertObject, true);
       //        NGUITools.SetActive(confirmObject, false);
       //        break;
       //    case PromptType.Confirm:
       //        NGUITools.SetActive(alertObject, false);
       //        NGUITools.SetActive(confirmObject, true);
       //        break;
       //}
       NGUITools.SetActive(alertObject, false);
       //NGUITools.SetActive(confirmObject, true);
       EventDelegate.Add(sureBtn.GetComponent<UIButton>().onClick, delegate()
       {
           onConfirm();
       });
       //EventDelegate.Add(cancelBtn.GetComponent<UIButton>().onClick, delegate()
       //{
       //    onCancel();
       //});
       //EventDelegate.Add(cancelBtn1.GetComponent<UIButton>().onClick, delegate()
       //{
       //    onCancel();
       //});
        if (DiamondCost > 0)
        {
            diamondCostLabel.text = DiamondCost.ToString();
        }
    }

    void Start()
    {
        EventDelegate.Add(cancelBtn.GetComponent<UIButton>().onClick, delegate()
        {
            Destroy(gameObject);
        });
        EventDelegate.Add(cancelBtn1.GetComponent<UIButton>().onClick, delegate()
        {
            Destroy(gameObject);
        });


        TextTranslator.instance.ItemDescription(messageLabel.transform.GetChild(0).GetChild(0).GetChild(1).gameObject, 20014, 0);
        TextTranslator.instance.ItemDescription(messageLabel.transform.GetChild(0).GetChild(1).GetChild(1).gameObject, 20015, 0);

    }

    ////////////////////// UI EVENTS /////////////////////////

    public void OnConfirmButtonClick()
    {
        if (onConfirm != null)
        {
            onConfirm();
        }
        else
        {
            if (GameObject.Find("RoleWindow") != null)
            {
                RoleWindow rw = GameObject.Find("RoleWindow").GetComponent<RoleWindow>();
                rw.SetHeroClick(-1);
            }
            Debug.Log("No Confirm Callback");
        }
        //Destroy(gameObject);
        if (messageLabel.text == "是否购买水晶？")
        {
            UIManager.instance.OpenPanel("VIPShopWindow", true);
            //UIManager.instance.OpenSinglePanel("VIPShopWindow", false);
        }
        else if (messageLabel.text == "是否消费10钻石重置时间？")
        {
            NetworkHandler.instance.SendProcess("6016#;");
            if (this != null)
            {
                Destroy(gameObject);
            }
        }
        else if (messageLabel.text == "您的账号在其它设备登陆，确定退出!")
        {
            NetworkHandler.instance.OnQuitGame();
            if (this != null)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (this != null)
            {
                Destroy(gameObject);
            }
        }
    }

    public void OnCancelButtonClick()
    {
        if (onCancel != null)
        {
            onCancel();
        }
        else
        {
            Debug.Log("No Cancel Callback");
        }
        Destroy(gameObject);
    }
}
