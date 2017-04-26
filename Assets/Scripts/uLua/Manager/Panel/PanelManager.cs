using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KeyValue {
    public string key;
    public string value;
    public KeyValue(string key, string value) {
        this.key = key; this.value = value;
    }
}

public class PanelManager : MonoBehaviour {
    private Transform parent;
    private static Queue<KeyValue> events = new Queue<KeyValue>();

    Transform Parent {
        get {
            if (parent == null) {
                parent = ioo.guiCamera;
            }
            return parent;
        }
    }

    /// <summary>
    /// 创建面板，请求资源管理器
    /// </summary>
    /// <param name="type"></param>
    public void CreatePanel(string name) {
        AssetBundle bundle = ioo.resourceManager.LoadBundle(name);
        StartCoroutine(StartCreatePanel(name, bundle, string.Empty));
        Debug.LogWarning("CreatePanel::>> " + name + " " + bundle);
    }

    /// <summary>
    /// 请求资源回调函数
    /// </summary>
    public void OnRequestResource(string type, AssetBundle bundle, string text = null) {
        StartCreatePanel(type, bundle, text);
    }

    /// <summary>
    /// 创建面板
    /// </summary>
    IEnumerator StartCreatePanel(string name, AssetBundle bundle, string text = null) {
        name += "Panel";
        GameObject prefab = Util.LoadAsset(bundle, name);
        yield return new WaitForEndOfFrame();
        if (Parent.FindChild(name) != null || prefab == null) {
            yield break;
        }
        GameObject go = Instantiate(prefab) as GameObject;
        go.name = name;
        go.layer = LayerMask.NameToLayer("Default");
        go.transform.parent = Parent;
        go.transform.localScale = Vector3.one;
        go.transform.localPosition = Vector3.zero;

        yield return new WaitForEndOfFrame();
        go.AddComponent<BaseLua>().OnInit(bundle);

        Debug.Log("StartCreatePanel------>>>>" + name);
    }
}
