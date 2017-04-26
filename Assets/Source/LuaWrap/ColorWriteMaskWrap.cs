using System;
using UnityEngine.Rendering;
using LuaInterface;

public class ColorWriteMaskWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("Alpha", GetAlpha),
		new LuaMethod("Blue", GetBlue),
		new LuaMethod("Green", GetGreen),
		new LuaMethod("Red", GetRed),
		new LuaMethod("All", GetAll),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Rendering.ColorWriteMask", typeof(ColorWriteMask), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAlpha(IntPtr L)
	{
		LuaScriptMgr.Push(L, ColorWriteMask.Alpha);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBlue(IntPtr L)
	{
		LuaScriptMgr.Push(L, ColorWriteMask.Blue);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetGreen(IntPtr L)
	{
		LuaScriptMgr.Push(L, ColorWriteMask.Green);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetRed(IntPtr L)
	{
		LuaScriptMgr.Push(L, ColorWriteMask.Red);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAll(IntPtr L)
	{
		LuaScriptMgr.Push(L, ColorWriteMask.All);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		ColorWriteMask o = (ColorWriteMask)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

