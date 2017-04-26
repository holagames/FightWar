using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class BaseLuaWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("OnInit", OnInit),
		new LuaMethod("GetGameObject", GetGameObject),
		new LuaMethod("AddClick", AddClick),
		new LuaMethod("RemoveClick", RemoveClick),
		new LuaMethod("ClearClick", ClearClick),
		new LuaMethod("New", _CreateBaseLua),
		new LuaMethod("GetClassType", GetClassType),
		new LuaMethod("__eq", Lua_Eq),
	};

	static LuaField[] fields = new LuaField[]
	{
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateBaseLua(IntPtr L)
	{
		LuaDLL.luaL_error(L, "BaseLua class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(BaseLua));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "BaseLua", typeof(BaseLua), regs, fields, typeof(UnityEngine.MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int OnInit(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 3);
		BaseLua obj = LuaScriptMgr.GetUnityObject<BaseLua>(L, 1);
		AssetBundle arg0 = LuaScriptMgr.GetUnityObject<AssetBundle>(L, 2);
		string arg1 = LuaScriptMgr.GetLuaString(L, 3);
		obj.OnInit(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGameObject(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BaseLua obj = LuaScriptMgr.GetUnityObject<BaseLua>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		GameObject o = obj.GetGameObject(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddClick(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BaseLua obj = LuaScriptMgr.GetUnityObject<BaseLua>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		obj.AddClick(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveClick(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		BaseLua obj = LuaScriptMgr.GetUnityObject<BaseLua>(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 2);
		obj.RemoveClick(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearClick(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		BaseLua obj = LuaScriptMgr.GetUnityObject<BaseLua>(L, 1);
		obj.ClearClick();
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

