using UnityEngine;
using System.Collections;

public class AnnouncementWindow : MonoBehaviour
{

    public UITexture AnnouncementTexture;
    private string TexName = "Announcement1";
    void Start()
    {

        UIEventListener.Get(GameObject.Find("KnowButton")).onClick = delegate(GameObject go)
        {
            if (GameObject.Find("MainWindow") != null)
            {
                if (CharacterRecorder.instance.GuideID[55] == 0 && CharacterRecorder.instance.lastGateID != 10008 && TexName == "Announcement1")//CharacterRecorder.instance.ActivityRewardIsGet == false
                {
                    //SetTexture("TenGacha");
                    AnnouncementTexture.mainTexture = Resources.Load("Game/TenGacha") as Texture;
                    TexName = "TenGacha";
                }
                else if ((TexName == "Announcement1" || TexName == "TenGacha") && CharacterRecorder.instance.Vip < 2)
                {
                    //SetTexture("newgonggao_dadi8");
                    AnnouncementTexture.mainTexture = Resources.Load("Game/newgonggao_dadi8") as Texture;
                    TexName = "newgonggao_dadi8";
                }
                else if (TexName == "newgonggao_dadi8")
                {
                    if (SceneTransformer.instance.CheckGuideIsFinish() && CharacterRecorder.instance.GuideID[CharacterRecorder.instance.NowGuideID] > 0 && CharacterRecorder.instance.GuideID[CharacterRecorder.instance.NowGuideID] != 99)
                    {
                        SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);

                    }
                    if (SceneTransformer.instance.CheckGuideIsFinish())
                    {
                        UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1706);
                    }
                    DoGC();
                }
                else
                {
                    DoGC();
                }
            }
            else
            {
                if (SceneTransformer.instance.CheckGuideIsFinish() && CharacterRecorder.instance.GuideID[CharacterRecorder.instance.NowGuideID] > 0 && CharacterRecorder.instance.GuideID[CharacterRecorder.instance.NowGuideID] != 99)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);

                }
                DoGC();
            }
        };
    }

    void DoGC()
    {
        DestroyImmediate(this.gameObject);
        TextTranslator.instance.DoGC();
    }

    public void SetTexture(string AnnouncementName)
    {
        AnnouncementTexture.mainTexture = Resources.Load("Game/" + AnnouncementName) as Texture;
    }

    public void SetTextureOnLoadingWindow(string AnnouncementName)
    {
        AnnouncementTexture.mainTexture = Resources.Load("Game/" + AnnouncementName) as Texture;
        if (GameObject.Find("MainWindow") != null)
        {
            if (CharacterRecorder.instance.Landingdays > 7)
            {
                if (CharacterRecorder.instance.GuideID[55] == 0 && CharacterRecorder.instance.lastGateID != 10008)
                {
                    //SetTexture("TenGacha");
                    AnnouncementTexture.mainTexture = Resources.Load("Game/TenGacha") as Texture;
                    TexName = "TenGacha";
                    Debug.LogError("开始进入10连抽");
                }
                else if (CharacterRecorder.instance.Vip < 2)
                {
                    //SetTexture("newgonggao_dadi8");
                    AnnouncementTexture.mainTexture = Resources.Load("Game/newgonggao_dadi8") as Texture;
                    TexName = "newgonggao_dadi8";
                    Debug.LogError("开始进入30元");
                }
                else
                {
                    DestroyImmediate(this.gameObject);
                }
            }
        }
    }
}
