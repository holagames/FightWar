using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
public class LoginSignWindow : MonoBehaviour
{
    [SerializeField]
    private GameObject closeButton;
    [SerializeField]
    private GameObject MyGrid;
    [SerializeField]
    private GameObject loginSignItem;
    [SerializeField]
    private UILabel hadSignDayCountLabel;

    private List<GameObject> mList = new List<GameObject>();
    private int hadSignCount = 0;
    void OnEnable()
    {

    }
    // Use this for initialization
    void Start()
    {
        UIManager.instance.CountActivitys(UIManager.Activitys.登录奖励);
        UIManager.instance.UpdateActivitys(UIManager.Activitys.登录奖励);

        NetworkHandler.instance.SendProcess("9131#;");
        SetLoginSignWindow(TextTranslator.instance.ActivitySevenLoginList);
        if (UIEventListener.Get(closeButton).onClick == null)
        {
            UIEventListener.Get(closeButton).onClick += delegate(GameObject go)
            {
                //UIManager.instance.BackUI();
                DestroyImmediate(this.gameObject);
            };
        }
    }
    public void SetLoginSignWindow(BetterList<ActivitySevenLogin> _ActivitySevenLoginList)
    {
        hadSignCount = 0;
        ClearUIGride();
        for (int i = 0; i < _ActivitySevenLoginList.size; i++)
        {
            if (_ActivitySevenLoginList[i].status != 2)
            {//未领取和不能领取
                GameObject go = NGUITools.AddChild(MyGrid, loginSignItem);
                go.name = _ActivitySevenLoginList[i].Day.ToString();
                go.GetComponent<LoginSignItem>().SetLoginSignItem(_ActivitySevenLoginList[i]);
                mList.Add(go);
            }
            if (_ActivitySevenLoginList[i].status == 2)
            {
                hadSignCount += 1;
            }
        }
        //Debug.LogError("hadSignCount:   " + hadSignCount);
        for (int i = 0; i < hadSignCount; i++)
        {//已领取   
            if (_ActivitySevenLoginList[i].status == 2)
            {
                GameObject go = NGUITools.AddChild(MyGrid, loginSignItem);
                go.name = _ActivitySevenLoginList[i].Day.ToString();
                go.GetComponent<LoginSignItem>().SetLoginSignItem(_ActivitySevenLoginList[i]);
                mList.Add(go);
            }
        }
        int index = 0;
        for (int i = 0; i < _ActivitySevenLoginList.size; i++)
        {
            if (_ActivitySevenLoginList[i].status == 1)
            {
                index = i;
                break;
            }
        }

        MyGrid.GetComponent<UIGrid>().Reposition();
        hadSignDayCountLabel.text = hadSignCount.ToString();
        //Debug.LogError("============="+index);
        //if (index > 0 && index < 5)
        //{
        //    MyGrid.transform.parent.localPosition = new Vector3(42f, -70f + 107f * index, 0);
        //    MyGrid.transform.parent.GetComponent<UIPanel>().clipOffset = new Vector2(0, -107f * index);
        //}
    }
    void ClearUIGride()
    {
        for (int i = mList.Count - 1; i >= 0; i--)
        {
            DestroyImmediate(mList[i]);
        }
        mList.Clear();
    }
    public void ResetLoginSignData(List<Item> _itemList)
    {
        OpenGainWindow(_itemList);
        TextTranslator.instance.GetActivitySevenLoginByDay(LoginSignItem.curClickDay).status = 2;
        hadSignCount += 1;
        for (int i = 0; i < mList.Count; i++)
        {
            mList[i].GetComponent<LoginSignItem>().ResetLoginSignItem();
        }
        hadSignDayCountLabel.text = hadSignCount.ToString();
    }
    void OpenGainWindow(List<Item> _itemList)
    {
        UIManager.instance.OpenSinglePanel("AdvanceWindow", false);
        GameObject.Find("AdvanceWindow").layer = 11;
        AdvanceWindow aw = GameObject.Find("AdvanceWindow").GetComponent<AdvanceWindow>();
        aw.SetInfo(AdvanceWindowType.GainResult, null, null, null, null, _itemList);
        /*  for (int i = 0; i < _itemList.Count;i++ )
          {
              if (_itemList[i].itemCode > 60000 && _itemList[i].itemCode < 65000)
              {
                  StartCoroutine(SetCardInfos(_itemList[i].itemCode));
              }
          }*/

    }
    IEnumerator SetCardInfos(int _RoleID)
    {
        //Debug.Log("_RoleID..." + _RoleID);
        yield return new WaitForSeconds(0.2f);
        UIManager.instance.OpenSinglePanel("CardWindow", false);
        GameObject _CardWindow = GameObject.Find("CardWindow");
        if (_CardWindow != null)
        {
            //Debug.Log("....._RoleID..." + _RoleID);
            _CardWindow.GetComponent<CardWindow>().SetCardInfo(_RoleID);
            foreach (Component c in _CardWindow.GetComponentsInChildren(typeof(Transform), true))
            {
                c.gameObject.layer = 11;
            }
        }

    }
}
