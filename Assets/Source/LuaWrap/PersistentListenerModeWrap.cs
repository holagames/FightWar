using System;
using UnityEngine.Events;
using LuaInterface;

public class PersistentListenerModeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("EventDefined", GetEventDefined),
		new LuaMethod("Void", GetVoid),
		new LuaMethod("Object", GetObject),
		new LuaMethod("Int", GetInt),
		new LuaMethod("Float", GetFloat),
		new LuaMethod("String", GetString),
		new LuaMethod("Bool", GetBool),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Events.PersistentListenerMode", typeof(PersistentListenerMode), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEventDefined(IntPtr L)
	{
		LuaScriptMgr.Push(L, PersistentListenerMode.EventDefined);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetVoid(IntPtr L)
	{
		LuaScriptMgr.Push(L, PersistentListenerMode.Void);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetObject(IntPtr L)
	{
		LuaScriptMgr.Push(L, PersistentListenerMode.Object);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetInt(IntPtr L)
	{
		LuaScriptMgr.Push(L, PersistentListenerMode.Int);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFloat(IntPtr L)
	{
		LuaScriptMgr.Push(L, PersistentListenerMode.Float);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetString(IntPtr L)
	{
		LuaScriptMgr.Push(L, PersistentListenerMode.String);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBool(IntPtr L)
	{
		LuaScriptMgr.Push(L, PersistentListenerMode.Bool);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		PersistentListenerMode o = (PersistentListenerMode)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

