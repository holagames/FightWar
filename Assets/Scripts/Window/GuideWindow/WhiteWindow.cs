using UnityEngine;
using System.Collections;

public class WhiteWindow : MonoBehaviour
{
    // Use this for initialization

    /// <summary>
    /// 关闭界面打开起名界面
    /// </summary>
    public void EscWindow()
    {
        UIManager.instance.OpenPanel("LoadingWindow", true);
        VersionChecker.instance.StartLogin();

    }

    public void DestroyGameObject()
    {
        UIManager.instance.OpenPanel("BlackWindow", true);        
    }
}
