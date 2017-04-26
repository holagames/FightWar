using System;
using UnityEngine;
using LuaInterface;

public class AnimatorCullingModeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("AlwaysAnimate", GetAlwaysAnimate),
		new LuaMethod("BasedOnRenderers", GetBasedOnRenderers),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.AnimatorCullingMode", typeof(AnimatorCullingMode), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAlwaysAnimate(IntPtr L)
	{
		LuaScriptMgr.Push(L, AnimatorCullingMode.AlwaysAnimate);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetBasedOnRenderers(IntPtr L)
	{
		LuaScriptMgr.Push(L, AnimatorCullingMode.BasedOnRenderers);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		AnimatorCullingMode o = (AnimatorCullingMode)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

