using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeadSettingWindow : MonoBehaviour
{
    public GameObject CloseButton;
    public GameObject SureButton;
    public GameObject uiGride;
    public GameObject headSettingItem;
    public static int selectHead;
    // Use this for initialization
    void Start()
    {
        selectHead = CharacterRecorder.instance.headIcon;
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }
        if (UIEventListener.Get(SureButton).onClick == null)
        {
            UIEventListener.Get(SureButton).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("1013#" + selectHead + ";");
                UIManager.instance.BackUI();
            };
        }
        SetHeadSettingWindow();
    }

    void SetHeadSettingWindow()
    {
        for (int i = 0; i < CharacterRecorder.instance.ownedHeroList.size; i++)
        {
            if (CharacterRecorder.instance.ownedHeroList[i].cardID != 60201 && CharacterRecorder.instance.ownedHeroList[i].cardID != 60100 && CharacterRecorder.instance.ownedHeroList[i].cardID != 60010 && CharacterRecorder.instance.ownedHeroList[i].cardID != 60104)//去掉直升飞机
            {
                GameObject obj = NGUITools.AddChild(uiGride, headSettingItem);
                obj.GetComponent<HeadSettingItem>().SetHeadSettingItem(CharacterRecorder.instance.ownedHeroList[i]);
                obj.GetComponent<UIToggle>().group = CharacterRecorder.instance.ownedHeroList.size;
            }

        }
        uiGride.GetComponent<UIGrid>().repositionNow = true;
    }
}
