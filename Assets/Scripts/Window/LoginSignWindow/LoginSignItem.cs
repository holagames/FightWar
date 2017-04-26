using UnityEngine;
using System.Collections;

public class LoginSignItem : MonoBehaviour
{
    public static int curClickDay;
    [SerializeField]
    private GameObject MyGrid;
    [SerializeField]
    private GameObject LoginAwardItem;
    [SerializeField]
    private GameObject GetButton;
    [SerializeField]
    private UILabel DayLabel;
    private ActivitySevenLogin _ActivitySevenLogin;
    // Use this for initialization
    void Start()
    {
        if (UIEventListener.Get(GetButton).onClick == null)
        {
            UIEventListener.Get(GetButton).onClick += delegate(GameObject go)
            {
                if (_ActivitySevenLogin.status == 1)
                {
                    curClickDay = _ActivitySevenLogin.Day;
                    NetworkHandler.instance.SendProcess(string.Format("9132#{0};", _ActivitySevenLogin.Day));
                }
            };
        }
    }

    public void SetLoginSignItem(ActivitySevenLogin _ActivitySevenLogin)
    {
        this._ActivitySevenLogin = _ActivitySevenLogin;
        DayLabel.text = _ActivitySevenLogin.Day.ToString();
        for (int i = 0; i < _ActivitySevenLogin.AwardItemList.size; i++)
        {
            if (_ActivitySevenLogin.AwardItemList[i].itemCode != 0)
            {
                GameObject go = NGUITools.AddChild(MyGrid, LoginAwardItem);
                go.name = _ActivitySevenLogin.AwardItemList[i].itemCode.ToString();
                go.GetComponent<LoginAwardItem>().SetLoginAwardItem(_ActivitySevenLogin.AwardItemList[i]);
                DealEffect(go.GetComponent<LoginAwardItem>().effectObj, _ActivitySevenLogin.AwardItemList[i].itemCode, _ActivitySevenLogin.status);
                TextTranslator.instance.ItemDescription(go, _ActivitySevenLogin.AwardItemList[i].itemCode, 0);
            }
        }
        SetSignItemState(_ActivitySevenLogin.status);
    }

    public void ResetLoginSignItem()
    {
        if (this._ActivitySevenLogin.Day == curClickDay)
        {
            this._ActivitySevenLogin.status = 2;
            SetSignItemState(this._ActivitySevenLogin.status);
            for (int i = 0; i < MyGrid.transform.childCount; i++)
            {
                if (MyGrid.transform.GetChild(i).GetComponent<LoginAwardItem>().effectObj.activeSelf)
                {
                    MyGrid.transform.GetChild(i).GetComponent<LoginAwardItem>().effectObj.SetActive(false);
                }
            }
        }
        else
        {
            return;
        }
    }
    void DealEffect(GameObject targetEffect, int codeId, int state)
    {
        if (state != 2 && (codeId == 60031 || codeId == 60015 || codeId == 60027 || codeId == 60025 || codeId == 42001 || (codeId >= 40000 && codeId <= 40606) || codeId == 70000 || codeId == 10014 || codeId == 10101))
        {
            targetEffect.SetActive(true);
        }
        else if (state != 2 && codeId == 70031 && this.gameObject.name == "7")
        {
            targetEffect.SetActive(true);
        }
        else
        {
            targetEffect.SetActive(false);
        }
    }
    void SetSignItemState(int _state)// 0 - 不可领,1 - 可领,2 - 已领
    {
        UILabel _GetButtonLabel = GetButton.transform.FindChild("Label").GetComponent<UILabel>();
        switch (_state)
        {
            case 0:
                GetButton.GetComponent<UIButton>().isEnabled = false;
                _GetButtonLabel.effectStyle = UILabel.Effect.None;
                _GetButtonLabel.text = "领 取";
                break;
            case 1:
                GetButton.GetComponent<UIButton>().isEnabled = true;
                _GetButtonLabel.effectStyle = UILabel.Effect.Outline;
                _GetButtonLabel.text = "领 取";
                break;
            case 2:
                GetButton.GetComponent<UIButton>().isEnabled = false;
                _GetButtonLabel.effectStyle = UILabel.Effect.None;
                _GetButtonLabel.text = "已领取";
                break;
        }
    }

}
