using System;
using UnityEngine;
using LuaInterface;
using Object = UnityEngine.Object;

public class iooWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("f", f),
		new LuaMethod("c", c),
		new LuaMethod("AddPrefab", AddPrefab),
		new LuaMethod("GetPrefab", GetPrefab),
		new LuaMethod("RemovePrefab", RemovePrefab),
		new LuaMethod("LoadPrefab", LoadPrefab),
		new LuaMethod("New", _Createioo),
		new LuaMethod("GetClassType", GetClassType),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("manager", get_manager, null),
		new LuaField("gameManager", get_gameManager, null),
		new LuaField("panelManager", get_panelManager, null),
		new LuaField("resourceManager", get_resourceManager, null),
		new LuaField("timerManager", get_timerManager, null),
		new LuaField("musicManager", get_musicManager, null),
		new LuaField("networkManager", get_networkManager, null),
		new LuaField("MainUI", get_MainUI, null),
		new LuaField("guiCamera", get_guiCamera, null),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _Createioo(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			ioo obj = new ioo();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: ioo.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(ioo));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "ioo", typeof(ioo), regs, fields, typeof(System.Object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_manager(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.manager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_gameManager(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.gameManager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_panelManager(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.panelManager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_resourceManager(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.resourceManager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_timerManager(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.timerManager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_musicManager(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.musicManager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_networkManager(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.networkManager);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_MainUI(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.MainUI);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_guiCamera(IntPtr L)
	{
		LuaScriptMgr.Push(L, ioo.guiCamera);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int f(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		object[] objs1 = LuaScriptMgr.GetParamsObject(L, 2, count - 1);
		string o = ioo.f(arg0,objs1);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int c(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		object[] objs0 = LuaScriptMgr.GetParamsObject(L, 1, count);
		string o = ioo.c(objs0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int AddPrefab(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 2);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		GameObject arg1 = LuaScriptMgr.GetUnityObject<GameObject>(L, 2);
		ioo.AddPrefab(arg0,arg1);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetPrefab(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		GameObject o = ioo.GetPrefab(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int RemovePrefab(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		ioo.RemovePrefab(arg0);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int LoadPrefab(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 1);
		string arg0 = LuaScriptMgr.GetLuaString(L, 1);
		GameObject o = ioo.LoadPrefab(arg0);
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

