using UnityEngine;
using System.Collections;

public class BossWarningWindow : MonoBehaviour
{
    public GameObject boss;
    public float Timer = 0;
    int RepeatTime = 0;

    public void ShowBossInfo(int BossID)
    {
        int roleID = 0;
        if (BossID >= 65000 && BossID < 65300)
        {
            roleID = BossID - 5000;
        }
        else if (BossID >= 65300)
        {
            roleID = BossID - 5300;
        }
        else
        {
            roleID = BossID;
        }
        //boss.renderer.material.mainTexture = Resources.Load("Loading/" + roleID, typeof(Texture)) as Texture;
        boss.GetComponent<UITexture>().mainTexture = Resources.Load("Loading/" + roleID, typeof(Texture)) as Texture;
        boss.GetComponent<UITexture>().MakePixelPerfect();        
        //boss.GetComponent<UITexture>().width = boss.GetComponent<UITexture>().width / 800;
        //boss.GetComponent<UITexture>().height = boss.GetComponent<UITexture>().height / 800;
    }

    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer > 0.1f)
        {
            Timer -= 0.1f;
            RepeatTime++;

            if (RepeatTime == 10)
            {
                AudioEditer.instance.PlayOneShot("Boss_alarm");
            }

            if (GameObject.Find("LoadingWindow") == null)
            {
                if (RepeatTime > 30)
                {
                    Destroy(gameObject);
                }
            }           
        }
    }
}
