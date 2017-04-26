using UnityEngine;
using System.Collections;


/// <summary>
/// 服务器类
/// </summary>

public class DownloadList{
    /// <summary>
    /// 服务器ID
    /// </summary>
    public string Platform { get; private set; }

    public string Url { get; private set; }

    public DownloadList(string _Platform, string _Url) 
    {
        this.Platform = _Platform;
        this.Url = _Url;
    }
}
