using UnityEngine;
using System.Collections;

public class ReconnectWindow : MonoBehaviour
{

    float Timer = 0;
    float LoadTimer = 0;
    int ReconnectTime = 1;
    public UILabel LabelReconnect;
    public GameObject Loading;

    // Update is called once per frame

    void Start()
    {
        LabelReconnect.text = "亲，您的网路网境不稳定，系统正在第1次尝试重新连线。。。";
    }

    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > 2)
        {
            Timer -= 2;
            NetworkHandler.instance.GameSocketConnect();
            LabelReconnect.text = "亲，您的网路网境不稳定，系统正在第" + ReconnectTime.ToString() + "次尝试重新连线。。。";
            ReconnectTime++;
        }
        LoadTimer += Time.deltaTime;
        if (LoadTimer > 0.01f)
        {
            LoadTimer -= 0.01f;
            Loading.transform.Rotate(new Vector3(0, 0, 2f));
        }
    }
}
