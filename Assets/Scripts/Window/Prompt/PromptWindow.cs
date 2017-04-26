using UnityEngine;
using System.Collections;

public class PromptWindow : MonoBehaviour
{

    public enum PromptType
    {
        Alert,
        Confirm,
        Hint,
        Popup,
        Traitor,
    }

    public delegate void OnConfirm();

    public OnConfirm onConfirm;

    public delegate void OnCancel();

    public OnCancel onCancel;
    public Transform ConfilmButten;
    public Transform CancelButton;
    [SerializeField]
    UILabel messageLabel;
    [SerializeField]
    UILabel diamondCostLabel;
    [SerializeField]
    GameObject alertObject;
    [SerializeField]
    GameObject confirmObject;

    void OnDisable()
    {
        onConfirm = null;
        onCancel = null;
        messageLabel.text = "";
    }

    public void SetPromptWindow(string message, PromptType type, OnConfirm confirmCallback, OnCancel cancelCallBack)
    {

       
        onConfirm = confirmCallback;
        onCancel = cancelCallBack;
        switch (type)
        {
            case PromptType.Alert:
                NGUITools.SetActive(alertObject, true);
                NGUITools.SetActive(confirmObject, false);
                break;
            case PromptType.Confirm:
                NGUITools.SetActive(alertObject, false);
                NGUITools.SetActive(confirmObject, true);
                break;
            case PromptType.Traitor:
                messageLabel.color = new Color(255,255,255);
                Vector3 Pos = CancelButton.localPosition;
                CancelButton.localPosition = ConfilmButten.localPosition;
                ConfilmButten.localPosition = Pos;
                NGUITools.SetActive(alertObject, false);
                NGUITools.SetActive(confirmObject, true);
                break;
        }
        messageLabel.text = message;

    }
    public void SetPromptWindow(string message, int layer, PromptType type, OnConfirm confirmCallback, OnCancel cancelCallBack)
    {
        this.gameObject.layer = layer;
        foreach (Component c in this.gameObject.GetComponentsInChildren(typeof(Transform), true))
        {
            c.gameObject.layer = layer;
        }

        messageLabel.text = message;
        onConfirm = confirmCallback;
        onCancel = cancelCallBack;
        switch (type)
        {
            case PromptType.Alert:
                NGUITools.SetActive(alertObject, true);
                NGUITools.SetActive(confirmObject, false);
                break;
            case PromptType.Confirm:
                NGUITools.SetActive(alertObject, false);
                NGUITools.SetActive(confirmObject, true);
                break;
        }
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
        switch (type)
        {
            case PromptType.Alert:
                NGUITools.SetActive(alertObject, true);
                NGUITools.SetActive(confirmObject, false);
                break;
            case PromptType.Confirm:
                NGUITools.SetActive(alertObject, false);
                NGUITools.SetActive(confirmObject, true);
                break;
        }
        if (DiamondCost > 0)
        {
            diamondCostLabel.text = DiamondCost.ToString();
        }
    }

    void Start()
    {
        //if (UIEventListener.Get(GameObject.Find("PromptCloseButton")).onClick == null)
        //{
        //    UIEventListener.Get(GameObject.Find("PromptCloseButton")).onClick += delegate(GameObject go)
        //    {
        //        if (GameObject.Find("RoleWindow") != null)
        //        {
        //            GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetHeroClick(-1);
        //        }                
        //        Destroy(gameObject);
        //    };
        //}
        if (GameObject.Find("RoleWindow") != null || GameObject.Find("GachaWindow") != null || GameObject.Find("StrengEquipWindow") != null)
        {
            this.gameObject.layer = 11;
            foreach (Component c in this.gameObject.GetComponentsInChildren(typeof(Transform), true))
            {
                c.gameObject.layer = 11;
            }
        }
        else
        {
            this.gameObject.layer = 5;
        }
        if (UIEventListener.Get(GameObject.Find("SpriteMask")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("SpriteMask")).onClick += delegate(GameObject go)
            {
                if (GameObject.Find("RoleWindow") != null)
                {
                    GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetHeroClick(-1);
                }
                //Destroy(gameObject);
            };
        }
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

    /////////////////////////////////////////////////////////
}
