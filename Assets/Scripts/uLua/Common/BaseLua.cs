using UnityEngine;
using LuaInterface;
using System.Collections;
using System.Collections.Generic;
using System;

public class BaseLua : MonoBehaviour {
    private string data = null;
    private bool initialize = false;
    private Transform trans = null;
    private LuaScriptMgr umgr = null;
    private AssetBundle bundle = null;
    private Hashtable buttons = new Hashtable();

    /// <summary>
    /// Lua管理器
    /// </summary>
    protected LuaScriptMgr uluaMgr {
        get {
            if (umgr == null) {
                umgr = ioo.gameManager.uluaMgr;
            }
            return umgr;
        }
    }

    protected void Start() {
        trans = transform;
        LuaState l = uluaMgr.lua;
        l[trans.name + ".transform"] = transform;
        l[trans.name + ".gameObject"] = gameObject;
        CallMethod("Start");
    }

    protected void OnClick() {
        CallMethod("OnClick");
    }

    protected void OnClickEvent(GameObject go) {
        CallMethod("OnClick", go);
    }

    /// <summary>
    /// 初始化面板
    /// </summary>
    public void OnInit(AssetBundle bundle, string text = null) {
        this.data = text;   //初始化附加参数
        this.bundle = bundle; //初始化
        Debug.LogWarning("OnInit---->>>"+ name +" text:>" + text);
    }

    /// <summary>
    /// 获取一个GameObject资源
    /// </summary>
    /// <param name="name"></param>
    public GameObject GetGameObject(string name) {
        if (bundle == null) return null;
        return Util.LoadAsset(bundle, name);
    }

    /// <summary>
    /// 添加单击事件
    /// </summary>
    public void AddClick(string button) {
        Transform to = trans.Find(button);
        if (to == null) return;
        GameObject go = to.gameObject;
        buttons.Add(button, go);
        UIEventListener.Get(go).onClick += OnClickEvent;
    }

    /// <summary>
    /// 移除单击事件
    /// </summary>
    public void RemoveClick(string button) {
        object o = buttons[button];
        if (o == null) return;
        GameObject go = o as GameObject;
        UIEventListener.Get(go).onClick -= OnClickEvent;
    }

    /// <summary>
    /// 清除单击事件
    /// </summary>
    public void ClearClick() {
        foreach (DictionaryEntry de in buttons) {
            RemoveClick(de.Key.ToString());
        }
    }

    //-----------------------------------------------
    /// <summary>
    /// 执行Lua方法-无参数
    /// </summary>
    protected object[] CallMethod(string func) {
        if (uluaMgr == null) return null;
        string funcName = name + "." + func;
        funcName = funcName.Replace("(Clone)", "");
        return uluaMgr.CallLuaFunction(funcName);
    }

    /// <summary>
    /// 执行Lua方法
    /// </summary>
    protected object[] CallMethod(string func, GameObject go) {
        if (uluaMgr == null) return null;
        string funcName = name + "." + func;
        funcName = funcName.Replace("(Clone)", "");
        return uluaMgr.CallLuaFunction(funcName, go);
    }

    /// <summary>
    /// 执行Lua方法-Socket消息
    /// </summary>
    protected object[] CallMethod(string func, int key, ByteBuffer buffer) {
        if (uluaMgr == null) return null;
        string funcName = "Network." + func;
        funcName = funcName.Replace("(Clone)", "");
        return uluaMgr.CallLuaFunction(funcName, key, buffer);
    }

    //-----------------------------------------------------------------
    protected void OnDestroy() {
        if (bundle) {
            bundle.Unload(true);
            bundle = null;  //销毁素材
        }
        ClearClick();
        umgr = null; 
        Debug.Log("~" + name + " was destroy!");
    }
}