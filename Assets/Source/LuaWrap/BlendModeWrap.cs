using System;
using UnityEngine.Rendering;
using LuaInterface;

public class BlendModeWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("Zero", GetZero),
		new LuaMethod("One", GetOne),
		new LuaMethod("DstColor", GetDstColor),
		new LuaMethod("SrcColor", GetSrcColor),
		new LuaMethod("OneMinusDstColor", GetOneMinusDstColor),
		new LuaMethod("SrcAlpha", GetSrcAlpha),
		new LuaMethod("OneMinusSrcColor", GetOneMinusSrcColor),
		new LuaMethod("DstAlpha", GetDstAlpha),
		new LuaMethod("OneMinusDstAlpha", GetOneMinusDstAlpha),
		new LuaMethod("SrcAlphaSaturate", GetSrcAlphaSaturate),
		new LuaMethod("OneMinusSrcAlpha", GetOneMinusSrcAlpha),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Rendering.BlendMode", typeof(BlendMode), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetZero(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendMode.Zero);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetOne(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendMode.One);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDstColor(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendMode.DstColor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSrcColor(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendMode.SrcColor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetOneMinusDstColor(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendMode.OneMinusDstColor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSrcAlpha(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendMode.SrcAlpha);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetOneMinusSrcColor(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendMode.OneMinusSrcColor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetDstAlpha(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendMode.DstAlpha);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetOneMinusDstAlpha(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendMode.OneMinusDstAlpha);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSrcAlphaSaturate(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendMode.SrcAlphaSaturate);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetOneMinusSrcAlpha(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendMode.OneMinusSrcAlpha);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		BlendMode o = (BlendMode)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

