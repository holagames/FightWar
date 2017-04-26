using UnityEngine;
using System.Collections;

public class GameAnnouncementWindow : MonoBehaviour
{

    //public UILabel Versionnumber;//游戏版本号
    //public UILabel Resourcepackage;//资源包版本号
    public UILabel Announcement;
    private string[] Version;
    public GameObject QQGroup;
    public UILabel LabelQQ;

    void Start()
    {
        if (UIEventListener.Get(GameObject.Find("KnowButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("KnowButton")).onClick += delegate(GameObject go)
            {
                DestroyImmediate(this.gameObject);
                //UIManager.instance.BackUI();
            };
        }
        if (UIEventListener.Get(GameObject.Find("AllBG")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("AllBG")).onClick += delegate(GameObject go)
            {
                DestroyImmediate(this.gameObject);
                //UIManager.instance.BackUI();
            };
        }
        //Announcement.text = "1.跨服争夺战即将开启跨服争夺战即将开启跨服争夺战即将开启111\n2.跨服争夺战即将开启跨服争夺战即将开启跨服争夺战即将开启\n3跨服争夺战即将开启跨服争夺战即将开启跨服争夺战即将开启";

# if XIAOMI
        LabelQQ.text = "164309709";
#elif HUAWEI
        LabelQQ.text = "368337834";
#elif QIHOO360
        LabelQQ.text = "468300857  ";
#elif BAIDU
        LabelQQ.text = "202626685  ";
#elif OPPO
        QQGroup.SetActive(false);
#elif UC
        QQGroup.SetActive(false);
#elif VIVO
        LabelQQ.text = "469247914  ";
#elif BAIDU
        LabelQQ.text = "202626685  ";
#else

#if UNITY_IPHONE
        QQGroup.SetActive(false);
#else
        QQGroup.SetActive(false);
#endif
#endif

    }

    public void SetAnnouncement(string AnnouncementStr)
    {
        string text = AnnouncementStr.Replace("[$]", "\n");
        //Versionnumber.text = dataSplit[0];
        //Resourcepackage.text = dataSplit[1];
        Announcement.text = text;
        //Version = AnnouncementStr.Split(';');
        //string text = Version[2].Replace("$", "\n");
        //Announcement.text = text;
    }
}
