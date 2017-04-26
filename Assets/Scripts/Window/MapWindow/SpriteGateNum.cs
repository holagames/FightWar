using UnityEngine;
using System.Collections;

public class SpriteGateNum : MonoBehaviour {

    int gateNum = 0;
    public bool IsBtnClick;
    void Start()
    {
        gateNum = int.Parse(this.name.Substring(this.name.Length -1));
        UIEventListener.Get(GameObject.Find("SpriteLine").transform.Find(("SpriteGateNum" + gateNum.ToString())).FindChild("RoleBg").gameObject).onClick = delegate(GameObject go)
        {
            IsBtnClick = true;
            SetSelectGateNum(gateNum);
            
        };        
    }

    void SetHeightLight(int num, int curNum,bool IsShowEfect)
    {
        for (int i = 1; i <= num; i++)
        {
            if (i != curNum)
            {
                GameObject.Find("SpriteLine").transform.Find(("SpriteGateNum" + i.ToString())).Find("RoleBg").GetComponent<UISprite>().spriteName = "sd_kuang1";
                GameObject.Find("SpriteLine").transform.Find(("SpriteGateNum" + i.ToString())).Find("RoleBg").Find("WF_UI_ZhuangBei_07").gameObject.SetActive(false);
            }
            else
            {
                GameObject.Find("SpriteLine").transform.Find(("SpriteGateNum" + i.ToString())).Find("RoleBg").GetComponent<UISprite>().spriteName = "sd_kuang2";
                if(IsShowEfect)
                {
                    GameObject.Find("SpriteLine").transform.Find(("SpriteGateNum" + i.ToString())).Find("RoleBg").Find("WF_UI_ZhuangBei_07").gameObject.SetActive(true);
                }
                else
                {
                    GameObject.Find("SpriteLine").transform.Find(("SpriteGateNum" + i.ToString())).Find("RoleBg").Find("WF_UI_ZhuangBei_07").gameObject.SetActive(false);
                }
            }
        }
    }

    public void SetSelectGateNum(int num)
    {
        int gateNums = int.Parse(GameObject.Find("SpriteLine").transform.Find(("SpriteGateNum" + num.ToString())).Find("LabelChapter").GetComponent<UILabel>().text);
        if (IsBtnClick)
        {
            IsBtnClick = false;
            NetworkHandler.instance.SendProcess("2017#" + gateNums + ";");
        }
        MapGateInfoWindow _mapGateInfo = GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>();
        _mapGateInfo.InitGateID = gateNums;
        _mapGateInfo.SetChallengeInfo();
        if(gateNums < 20000)
        {
            if (CharacterRecorder.instance.lastGateID <= gateNums)
            {
                SetHeightLight(_mapGateInfo.GateNum, num, true);
            }
            else
            {
                SetHeightLight(_mapGateInfo.GateNum, num, false);
            }
        }
        else
        {
            GameObject _mapWindow = GameObject.Find("MapCon");
            if (_mapWindow != null)
            {
                if(_mapWindow.GetComponent<MapWindow>().CreamSatrList[(gateNums % 10000) - 1]>0)
                {
                    SetHeightLight(GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().GateNum, num, false);
                }
                else
                {
                    SetHeightLight(GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().GateNum, num, true);
                }
            }
        }
        GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().SetTab(0);
        GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().SetPageInfo(gateNums);
        GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().SetSweepBtn();
        
    }
}
