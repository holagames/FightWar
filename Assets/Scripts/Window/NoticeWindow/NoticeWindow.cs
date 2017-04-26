using UnityEngine;
using System.Collections;

public class NoticeWindow : MonoBehaviour
{
    public GameObject exitBtn;
    public GameObject sureBtn;
    public GameObject cancelBtn;
    public UILabel noticeLabel;
    void Start()
    {
        if (UIEventListener.Get(exitBtn).onClick == null)
        {
            UIEventListener.Get(exitBtn).onClick += delegate(GameObject go)
            {
                Destroy(this.gameObject);
            };
        }
        if (UIEventListener.Get(cancelBtn).onClick == null)
        {
            UIEventListener.Get(cancelBtn).onClick += delegate(GameObject go)
            {
                Destroy(this.gameObject);
            };
        }
        if (UIEventListener.Get(sureBtn).onClick == null)
        {
            UIEventListener.Get(sureBtn).onClick += delegate(GameObject go)
            {
                //开启省电模式
                Destroy(this.gameObject);
            };
        }
    }
    public void SetNoticeWindow(int power)
    {
        noticeLabel.text = string.Format("[89c4d4]现在电量只有 [f84628]{0}% [89c4d4]是否进入省电模式？", power);
    }
}
