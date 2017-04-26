using UnityEngine;
using System.Collections;

public class UIRootExtend : MonoBehaviour
{

    public int ManualWidth = 1422;
    public int ManualHeight = 800;
    private UIRoot _UIRoot;
    public bool isFirst = false;
    public GameObject Downloader;
    public float isUiRootRatio;
    public static UIRootExtend instance;
    void Awake()
    {
        _UIRoot = this.GetComponent<UIRoot>();
        instance = this;
        ChangeCamera();
    }

    void ChangeCamera()
    {
        if (System.Convert.ToSingle(Screen.height) / Screen.width >= System.Convert.ToSingle(1536) / 2048)//
        {
            _UIRoot.manualHeight = Mathf.RoundToInt(System.Convert.ToSingle(ManualWidth) / Screen.width * Screen.height);
             isUiRootRatio = 1.38f;  //1280*1024
             //isUiRootRatio = 1.29f; //1024*768
            //isFirst = true;
            //Camera.main.fieldOfView = getCameraFOV(60);
            //CameraInfo();
        }
        else
        {
            //UIRootExtend.instance.isUiRootRatio = 1f;

            isUiRootRatio = 1f;            
           _UIRoot.manualHeight = ManualHeight;
        }

    }
    static public float getCameraFOV(float currentFOV)
    {
        UIRoot root = GameObject.FindObjectOfType<UIRoot>();
        float scale = System.Convert.ToSingle(root.manualHeight / 800f);
        return currentFOV * scale;
    }

    void CameraInfo()
    {
        foreach (var cam in Camera.allCameras)
        {
            cam.aspect = 4 / 3f;
        }
    }
}