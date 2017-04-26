using System;
using System.Collections;
using LuaInterface;

public class GlobalWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("AddValue", AddValue),
		new LuaMethod("GetValue", GetValue),
		new LuaMethod("RemoveValue", RemoveValue),
		new LuaMethod("ClearShareVars", ClearShareVars),
		new LuaMethod("New", _CreateGlobal),
		new LuaMethod("GetClassType", GetClassType),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("ShareVars", get_ShareVars, set_ShareVars),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGlobal(IntPtr L)
	{
		LuaDLL.luaL_error(L, "Global class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(Global));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "Global", typeof(Global), regs, fields, null);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_ShareVars(IntPtr L)
	{
		LuaScriptMgr.PushObject(L, Global.ShareVars);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_ShareVars(IntPtr L)
	{
		Global.ShareVars = LuaScriptMgr.GetNetObject<Hashtable>(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddValue(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		string arg1 = LuaScriptMgr.GetLuaString(L, 2);
		Global.AddValue(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetValue(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		object o = Global.GetValue(arg0);
		LuaScriptMgr.PushVarObject(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveValue(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		Global.RemoveValue(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearShareVars(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		Global.ClearShareVars();
		return 0;
	}
}

