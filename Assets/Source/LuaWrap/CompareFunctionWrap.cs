using System;
using UnityEngine.Rendering;
using LuaInterface;

public class CompareFunctionWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("Disabled", GetDisabled),
		new LuaMethod("Never", GetNever),
		new LuaMethod("Less", GetLess),
		new LuaMethod("Equal", GetEqual),
		new LuaMethod("LessEqual", GetLessEqual),
		new LuaMethod("Greater", GetGreater),
		new LuaMethod("NotEqual", GetNotEqual),
		new LuaMethod("GreaterEqual", GetGreaterEqual),
		new LuaMethod("Always", GetAlways),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Rendering.CompareFunction", typeof(CompareFunction), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDisabled(IntPtr L)
	{
		LuaScriptMgr.Push(L, CompareFunction.Disabled);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNever(IntPtr L)
	{
		LuaScriptMgr.Push(L, CompareFunction.Never);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLess(IntPtr L)
	{
		LuaScriptMgr.Push(L, CompareFunction.Less);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetEqual(IntPtr L)
	{
		LuaScriptMgr.Push(L, CompareFunction.Equal);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLessEqual(IntPtr L)
	{
		LuaScriptMgr.Push(L, CompareFunction.LessEqual);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGreater(IntPtr L)
	{
		LuaScriptMgr.Push(L, CompareFunction.Greater);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetNotEqual(IntPtr L)
	{
		LuaScriptMgr.Push(L, CompareFunction.NotEqual);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGreaterEqual(IntPtr L)
	{
		LuaScriptMgr.Push(L, CompareFunction.GreaterEqual);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAlways(IntPtr L)
	{
		LuaScriptMgr.Push(L, CompareFunction.Always);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		CompareFunction o = (CompareFunction)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

