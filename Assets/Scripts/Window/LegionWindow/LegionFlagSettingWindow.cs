using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionFlagSettingWindow : MonoBehaviour
{
    public GameObject closeButton;
    public GameObject saveButton;
    public List<GameObject> flagItemList = new List<GameObject>();

    public GameObject leftBtn;
    public GameObject rightBtn;

    public GameObject scrollView;
    // Use this for initialization
    void Start()
    {
        InitLegionFlagSettingWindow();
        if (UIEventListener.Get(closeButton).onClick == null)
        {
            UIEventListener.Get(closeButton).onClick += delegate(GameObject obj)
            {
                UIManager.instance.BackUI();
            };
        }
        if (UIEventListener.Get(saveButton).onClick == null)
        {
            UIEventListener.Get(saveButton).onClick += delegate(GameObject obj)
            {
                ClickSaveButton();
            };
        }
        if (UIEventListener.Get(rightBtn).onClick == null)
        {
            UIEventListener.Get(rightBtn).onClick += delegate(GameObject go)
            {
                //scrollView.GetComponent<UIScrollView>().
                scrollView.transform.localPosition -= new Vector3(60, 0, 0);
                scrollView.GetComponent<UIPanel>().clipOffset += new Vector2(60, 0);
                if (scrollView.transform.localPosition.x < -195)
                {
                    scrollView.transform.localPosition = new Vector3(-193.0f, scrollView.transform.localPosition.y, scrollView.transform.localPosition.z);
                    scrollView.GetComponent<UIPanel>().clipOffset = new Vector2(88.0f, scrollView.GetComponent<UIPanel>().clipOffset.y);
                }
            };
        }
        if (UIEventListener.Get(leftBtn).onClick == null)
        {
            UIEventListener.Get(leftBtn).onClick += delegate(GameObject go)
            {
                scrollView.transform.localPosition += new Vector3(60, 0, 0);
                scrollView.GetComponent<UIPanel>().clipOffset -= new Vector2(60, 0);
                if (scrollView.transform.localPosition.x > -101)
                {
                    scrollView.transform.localPosition = new Vector3(-101.0f, scrollView.transform.localPosition.y, scrollView.transform.localPosition.z);
                    scrollView.GetComponent<UIPanel>().clipOffset = new Vector2(-4.0f, scrollView.GetComponent<UIPanel>().clipOffset.y);
                }
            };
        }
    }


    void InitLegionFlagSettingWindow()
    {
        for (int i = 0; i < flagItemList.Count; i++)
        {
            flagItemList[i].GetComponent<UIToggle>().group = flagItemList.Count;
            if (i + 1 == CharacterRecorder.instance.legionFlag)
            {
                flagItemList[i].GetComponent<UIToggle>().value = true;
                flagItemList[i].GetComponent<UIToggle>().startsActive = true;
            }
            else
            {
                flagItemList[i].GetComponent<UIToggle>().value = false;
            }
        }
    }
    public void ResetFlagResult()
    {
        //CharacterRecorder.instance.legionFlag = _legionFlag;
        GameObject _LegionCreatWindow = GameObject.Find("LegionCreatWindow");
        GameObject _LegionBasicInfoPart = GameObject.Find("LegionBasicInfoPart");
        if (_LegionCreatWindow != null)
        {
            _LegionCreatWindow.GetComponent<LegionCreatWindow>().legionFlagButton.GetComponent<UISprite>().spriteName = string.Format("legionFlag{0}", CharacterRecorder.instance.legionFlag);
        }
        else if (_LegionBasicInfoPart != null)
        {
            _LegionBasicInfoPart.GetComponent<LegionBasicInfoPart>().legionFlagIcon.GetComponent<UISprite>().spriteName = string.Format("legionFlag{0}", CharacterRecorder.instance.legionFlag);
        }
        UIManager.instance.BackUI();
    }
    void ClickSaveButton()
    {
        for (int i = 0; i < flagItemList.Count; i++)
        {
            if (flagItemList[i].GetComponent<UIToggle>().value)
            {
                if (CharacterRecorder.instance.legionID != 0)
                {
                    NetworkHandler.instance.SendProcess(string.Format("8021#{0};", flagItemList[i].name));//修改旗帜
                }
                else
                {
                    //创建军团
                    CharacterRecorder.instance.legionFlag = int.Parse(flagItemList[i].name);
                    GameObject _LegionCreatWindow = GameObject.Find("LegionCreatWindow");
                    _LegionCreatWindow.GetComponent<LegionCreatWindow>().legionFlagButton.GetComponent<UISprite>().spriteName = string.Format("legionFlag{0}", CharacterRecorder.instance.legionFlag);
                    UIManager.instance.BackUI();
                }
                break;
            }
        }

    }
}
