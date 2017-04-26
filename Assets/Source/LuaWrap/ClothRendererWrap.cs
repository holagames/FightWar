using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class ClothRendererWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("New", _CreateClothRenderer),
		new LuaMethod("GetClassType", GetClassType),
		new LuaMethod("__eq", Lua_Eq),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("pauseWhenNotVisible", get_pauseWhenNotVisible, set_pauseWhenNotVisible),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateClothRenderer(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			ClothRenderer obj = new ClothRenderer();
			LuaScriptMgr.Push(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: ClothRenderer.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(ClothRenderer));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.ClothRenderer", typeof(ClothRenderer), regs, fields, typeof(UnityEngine.Renderer));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_pauseWhenNotVisible(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ClothRenderer obj = (ClothRenderer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pauseWhenNotVisible");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pauseWhenNotVisible on a nil value");
			}
		}

		LuaScriptMgr.Push(L, obj.pauseWhenNotVisible);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int set_pauseWhenNotVisible(IntPtr L)
	{
		object o = LuaScriptMgr.GetLuaObject(L, 1);
		ClothRenderer obj = (ClothRenderer)o;

		if (obj == null)
		{
			LuaTypes types = LuaDLL.lua_type(L, 1);

			if (types == LuaTypes.LUA_TTABLE)
			{
				LuaDLL.luaL_error(L, "unknown member name pauseWhenNotVisible");
			}
			else
			{
				LuaDLL.luaL_error(L, "attempt to index pauseWhenNotVisible on a nil value");
			}
		}

		obj.pauseWhenNotVisible = LuaScriptMgr.GetBoolean(L, 3);
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

