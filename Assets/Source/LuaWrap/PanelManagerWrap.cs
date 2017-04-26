using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class PanelManagerWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("CreatePanel", CreatePanel),
		new LuaMethod("OnRequestResource", OnRequestResource),
		new LuaMethod("New", _CreatePanelManager),
		new LuaMethod("GetClassType", GetClassType),
		new LuaMethod("__eq", Lua_Eq),
	};

	static LuaField[] fields = new LuaField[]
	{
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreatePanelManager(IntPtr L)
	{
		LuaDLL.luaL_error(L, "PanelManager class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(PanelManager));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "PanelManager", typeof(PanelManager), regs, fields, typeof(UnityEngine.MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int CreatePanel(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		PanelManager obj = LuaScriptMgr.GetUnityObject<PanelManager>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		obj.CreatePanel(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnRequestResource(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 4);
		PanelManager obj = LuaScriptMgr.GetUnityObject<PanelManager>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		AssetBundle arg1 = LuaScriptMgr.GetUnityObject<AssetBundle>(L, 3);
		string arg2 = LuaScriptMgr.GetLuaString(L, 4);
		obj.OnRequestResource(arg0,arg1,arg2);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Lua_Eq(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		Object arg0 = LuaScriptMgr.GetVarObject(L, 1) as Object;
		Object arg1 = LuaScriptMgr.GetVarObject(L, 2) as Object;
		bool o = arg0 == arg1;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

