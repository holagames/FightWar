using System;
using UnityEngine.Rendering;
using LuaInterface;

public class CullModeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("Off", GetOff),
		new LuaMethod("Front", GetFront),
		new LuaMethod("Back", GetBack),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Rendering.CullMode", typeof(CullMode), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetOff(IntPtr L)
	{
		LuaScriptMgr.Push(L, CullMode.Off);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetFront(IntPtr L)
	{
		LuaScriptMgr.Push(L, CullMode.Front);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBack(IntPtr L)
	{
		LuaScriptMgr.Push(L, CullMode.Back);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		CullMode o = (CullMode)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

