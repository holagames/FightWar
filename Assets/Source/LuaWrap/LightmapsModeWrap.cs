using System;
using UnityEngine;
using LuaInterface;

public class LightmapsModeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("Single", GetSingle),
		new LuaMethod("Dual", GetDual),
		new LuaMethod("Directional", GetDirectional),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.LightmapsMode", typeof(LightmapsMode), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSingle(IntPtr L)
	{
		LuaScriptMgr.Push(L, LightmapsMode.Single);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDual(IntPtr L)
	{
		LuaScriptMgr.Push(L, LightmapsMode.Dual);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDirectional(IntPtr L)
	{
		LuaScriptMgr.Push(L, LightmapsMode.Directional);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		LightmapsMode o = (LightmapsMode)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

