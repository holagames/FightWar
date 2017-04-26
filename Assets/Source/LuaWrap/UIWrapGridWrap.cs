using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class UIWrapGridWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("InitGrid", InitGrid),
		new LuaMethod("New", _CreateUIWrapGrid),
		new LuaMethod("GetClassType", GetClassType),
		new LuaMethod("__eq", Lua_Eq),
	};

	static LuaField[] fields = new LuaField[]
	{
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateUIWrapGrid(IntPtr L)
	{
		LuaDLL.luaL_error(L, "UIWrapGrid class does not have a constructor function");
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(UIWrapGrid));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UIWrapGrid", typeof(UIWrapGrid), regs, fields, typeof(UnityEngine.MonoBehaviour));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int InitGrid(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		UIWrapGrid obj = LuaScriptMgr.GetUnityObject<UIWrapGrid>(L, 1);
		obj.InitGrid();
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

