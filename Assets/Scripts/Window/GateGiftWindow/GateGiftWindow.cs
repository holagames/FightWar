using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GateGiftWindow : MonoBehaviour
{
    public GameObject sureBtn;
    public GameObject myGrid;
    public GameObject exitBtn;
    public UITexture heroTexture;

    public GameObject desItemPrefab;
    /// <summary>
    /// 记录当前奖励的Item对象
    /// </summary>
    private List<GameObject> myGridList = new List<GameObject>();
    /// <summary>
    /// 初始化GateGiftWindow面板信息
    /// </summary>
    /// <param name="heroId">角色的英雄ID号</param>
    /// <param name="resceiveMsg">奖励Item的信息</param>
    public void SatrtInitPanel(int heroId, string resceiveMsg)
    {
        //添加奖励的Item
        string[] dataSplit = resceiveMsg.Split('!');
        if (dataSplit.Length > 0)
        {
            DestroyItemGrid();
            for (int i = 0; i < dataSplit.Length - 1; i++)
            {
                string[] dataSplit1 = dataSplit[i].Split('$');
                GameObject myItem = NGUITools.AddChild(myGrid, desItemPrefab);
                myGridList.Add(myItem);
                myItem.name = dataSplit1[0];
                myItem.GetComponent<RebirthItem>().SetRebirthItemInfo(int.Parse(dataSplit1[0]), int.Parse(dataSplit1[1]));
            }
            myGrid.GetComponent<UIGrid>().Reposition();
        }
        //heroTexture.mainTexture = Resources.Load("GuideTexture/lili_01", typeof(Texture)) as Texture;
        //heroTexture.MakePixelPerfect();
        //heroTexture.transform.localScale = new Vector3(1, 1, 1) * 1.2f;
        InitBtn_OnClick();
    }
    /// <summary>
    /// 初始化按钮事件
    /// </summary>
    void InitBtn_OnClick()
    {
        if (UIEventListener.Get(sureBtn).onClick == null)
        {
            UIEventListener.Get(sureBtn).onClick += delegate(GameObject go)
            {
                Destroy(this.gameObject);
                ThreadBtnClick();
            };
        }
        if (UIEventListener.Get(exitBtn).onClick == null)
        {
            UIEventListener.Get(exitBtn).onClick += delegate(GameObject go)
            {
                Destroy(this.gameObject);
            };
        }
    }
    /// <summary>
    /// 前往下一关
    /// </summary>
    void ThreadBtnClick()
    {
        TextTranslator.Gate _gate = TextTranslator.instance.GetGateByID(CharacterRecorder.instance.lastGateID);
        //int gotoId = _gate.group + 10000;
        Transform _mapCon = GameObject.Find("MapObject").transform.Find("MapCon");
        if (_mapCon != null)
        {
            int MapSatrListCount = _mapCon.GetComponent<MapWindow>().MapSatrList.Count;
            if (MapSatrListCount < CharacterRecorder.instance.lastGateID - 10000)
            {
                UIManager.instance.OpenPromptWindow("长官，请先解锁关卡！", PromptWindow.PromptType.Hint, null, null);
                return;
            }
            CharacterRecorder.instance.gotoGateID = _gate.id;
            _mapCon.GetComponent<MapWindow>().SetMapTypeUpdate();
        }
    }
    /// <summary>
    /// 销毁奖励的Item的对象
    /// </summary>
    void DestroyItemGrid()
    {
        for (int i = 0; i < myGridList.Count; i++)
        {
            DestroyImmediate(myGridList[i]);
        }
        myGridList.Clear();
        myGrid.GetComponent<UIGrid>().Reposition();
    }
}
