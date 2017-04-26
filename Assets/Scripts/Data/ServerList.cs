using UnityEngine;
using System.Collections;


/// <summary>
/// 服务器类
/// </summary>

public class ServerList{
    /// <summary>
    /// 服务器ID
    /// </summary>
    public int ServerID { get; private set; }

    public string ServerTag { get; private set; }

    public string GroupID { get; private set; }

    public string ServerName { get; private set; }

    public string OpenDate { get; private set; }

    public string CloseDate { get; private set; }

    public int Type { get; private set; }

    public int State { get; private set; }

    public string LuaScriptID { get; private set; }

    public string ServerIP { get; private set; }

    public int ServerPort { get; private set; }

    public ServerList(int _ServerID, string _ServerTag,string _GroupID,string _ServerName, string _OpenDate, string _CloseDate, int _Type, int _State, string _LuaScriptID, string _ServerIP, int _ServerPort) 
    {
        this.ServerID = _ServerID;
        this.ServerTag = _ServerTag;
        this.GroupID = _GroupID;
        this.ServerName = _ServerName;
        this.OpenDate = _OpenDate;
        this.CloseDate = _CloseDate;
        this.Type = _Type;
        this.State = _State;
        this.LuaScriptID = _LuaScriptID;
        this.ServerIP = _ServerIP;
        this.ServerPort = _ServerPort;
    }
}
