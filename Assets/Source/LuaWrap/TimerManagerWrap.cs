using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class TimerManagerWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("StartTimer", StartTimer),
		new LuaMethod("StopTimer", StopTimer),
		new LuaMethod("AddTimerEvent", AddTimerEvent),
		new LuaMethod("RemoveTimerEvent", RemoveTimerEvent),
		new LuaMethod("StopTimerEvent", StopTimerEvent),
		new LuaMethod("ResumeTimerEvent", ResumeTimerEvent),
		new LuaMethod("New", _CreateTimerManager),
		new LuaMethod("GetClassType", GetClassType),
		new LuaMethod("__eq", Lua_Eq),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("Interval", get_Interval, set_Interval),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateTimerManager(IntPtr L)
	{
		LuaDLL.luaL_error(L, "TimerManager class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(TimerManager));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "TimerManager", typeof(TimerManager), regs, fields, typeof(UnityEngine.MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_Interval(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TimerManager obj = (TimerManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Interval");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Interval on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.Interval);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_Interval(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		TimerManager obj = (TimerManager)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name Interval");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index Interval on a nil value");
			}
		}

		obj.Interval = (float)LuaScriptMgr.GetNumber(L, 3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StartTimer(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TimerManager obj = LuaScriptMgr.GetUnityObject<TimerManager>(L, 1);
		float arg0 = (float)LuaScriptMgr.GetNumber(L, 2);
		obj.StartTimer(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopTimer(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		TimerManager obj = LuaScriptMgr.GetUnityObject<TimerManager>(L, 1);
		obj.StopTimer();
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddTimerEvent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TimerManager obj = LuaScriptMgr.GetUnityObject<TimerManager>(L, 1);
		TimerInfo arg0 = LuaScriptMgr.GetNetObject<TimerInfo>(L, 2);
		obj.AddTimerEvent(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemoveTimerEvent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TimerManager obj = LuaScriptMgr.GetUnityObject<TimerManager>(L, 1);
		TimerInfo arg0 = LuaScriptMgr.GetNetObject<TimerInfo>(L, 2);
		obj.RemoveTimerEvent(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int StopTimerEvent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TimerManager obj = LuaScriptMgr.GetUnityObject<TimerManager>(L, 1);
		TimerInfo arg0 = LuaScriptMgr.GetNetObject<TimerInfo>(L, 2);
		obj.StopTimerEvent(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ResumeTimerEvent(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		TimerManager obj = LuaScriptMgr.GetUnityObject<TimerManager>(L, 1);
		TimerInfo arg0 = LuaScriptMgr.GetNetObject<TimerInfo>(L, 2);
		obj.ResumeTimerEvent(arg0);
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

