using UnityEngine;
using System.Collections;

public class LoadWindow : MonoBehaviour
{
    float IdleTimer;
    float ShowTimer;

    public UITexture Loading;
    public UITexture Texture1;
    public UITexture Texture2;
    public UITexture Texture3;

    public GameObject Idle;

    public GameObject ResetButton;

    int Count = 0;

    void OnEnable()
    {
        IdleTimer = 0;
        Idle.SetActive(false);
    }

    void Start()
    {
        if (UIEventListener.Get(ResetButton).onClick == null)
        {
            UIEventListener.Get(ResetButton).onClick = delegate(GameObject go)
            {
                PlayerPrefs.SetInt("Relogin", 1);
                Time.timeScale = 1;
                CharacterRecorder.instance.userId = 0;
                AsyncOperation async = Application.LoadLevelAsync("Downloader");
            };
        }
    }


    // Update is called once per frame
    void Update()
    {

        IdleTimer += Time.deltaTime;
        if (IdleTimer > 1.2f)
        {
            Idle.SetActive(true);
            ShowTimer += Time.deltaTime;
            if (ShowTimer > 1.2f)
            {
                Loading.mainTexture = (Texture)Resources.Load("Loading/jingli_01");
                Texture3.mainTexture = (Texture)Resources.Load("Loading/guangdian_01");
                Texture1.mainTexture = (Texture)Resources.Load("Loading/guangdian_02");

                Count++;
                ShowTimer = 0;
            }
            else if (ShowTimer > 1f)
            {
                Texture2.mainTexture = (Texture)Resources.Load("Loading/guangdian_01");
                Texture3.mainTexture = (Texture)Resources.Load("Loading/guangdian_02");
            }
            else if (ShowTimer > 0.8f)
            {
                Texture1.mainTexture = (Texture)Resources.Load("Loading/guangdian_01");
                Texture2.mainTexture = (Texture)Resources.Load("Loading/guangdian_02");
            }
            else if (ShowTimer > 0.6f)
            {
                Loading.mainTexture = (Texture)Resources.Load("Loading/jingli_04");
                Texture3.mainTexture = (Texture)Resources.Load("Loading/guangdian_01");
                Texture1.mainTexture = (Texture)Resources.Load("Loading/guangdian_02");
            }
            else if (ShowTimer > 0.4f)
            {
                Loading.mainTexture = (Texture)Resources.Load("Loading/jingli_03");
                Texture2.mainTexture = (Texture)Resources.Load("Loading/guangdian_01");
                Texture3.mainTexture = (Texture)Resources.Load("Loading/guangdian_02");
            }
            else if (ShowTimer > 0.2f)
            {
                Loading.mainTexture = (Texture)Resources.Load("Loading/jingli_02");
                Texture1.mainTexture = (Texture)Resources.Load("Loading/guangdian_01");
                Texture2.mainTexture = (Texture)Resources.Load("Loading/guangdian_02");
            }
            if (Count > 2)
            {
                if (NetworkHandler.instance.IdleCount > 3 || Count > 15)
                {
                    ResetButton.SetActive(true);
                }
                else if (GameObject.Find("FightScene") == null)
                {
                    if (NetworkHandler.instance.CheckSendString.Length > 5)
                    {
                        string CodeEncrytion = NetworkHandler.instance.CheckSendString.Substring(0, 4);
                        if (CodeEncrytion == "1001" || CodeEncrytion == "1003")
                        {
                            NetworkHandler.instance.SendProcess(NetworkHandler.instance.CheckSendString);
                            if (GameObject.Find("LoadingWindow") != null)
                            {
                                GameObject.Find("LoadingWindow").GetComponent<LoadingWindow>().loadSlider.value = 0;
                            }
                        }
                    }
                    DestroyImmediate(gameObject);
                }
                else if (NetworkHandler.instance.IsCreate)
                {
                    Count = 4;
                }
                else
                {
                    ResetButton.SetActive(true);
                }
            }
        }
    }
}
