using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroListWindow : MonoBehaviour {

    public List<UILabel> powerList = new List<UILabel>();
    public List<UILabel> weakList = new List<UILabel>();
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.英雄榜);
        UIManager.instance.UpdateSystems(UIManager.Systems.英雄榜);

        //int GateID = GameObject.Find("GateInfoWindow").GetComponent<MapGateInfoWindow>().GateID;
        GameObject _mask = this.transform.Find("Mask").gameObject;
        if (UIEventListener.Get(_mask).onClick == null)
        {
            UIEventListener.Get(_mask).onClick = delegate(GameObject go)
            {
                UIManager.instance.BackUI();
                if(GameObject.Find("TheChestWindow") != null)
                {
                    DestroyImmediate(GameObject.Find("TheChestWindow"));
                }
            };
        }
    }

    public void InitWindow(string _PowerStr,string _Weakest)
    {
        string[] power = _PowerStr.Split('$');
        string[] weakest = _Weakest.Split('$');
        int count = 0;
        for (int i = 0; i < power.Length - 1; i++)
        {
            if (power[i] != "")
            {
                powerList[i].text = power[i];
            }
            
        }
        for (int i = 0; i < weakest.Length - 1; i++)
        {
            if (weakest[i] != "")
            {
                
                weakList[count].text = weakest[i];
                count++;
            }
        }
    }
}
