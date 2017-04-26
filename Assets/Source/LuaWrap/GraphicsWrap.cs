using System;
using UnityEngine;
using LuaInterface;

public class GraphicsWrap
{
	public static LuaMethod[] regs = new LuaMethod[]
	{
		new LuaMethod("DrawMesh", DrawMesh),
		new LuaMethod("DrawMeshNow", DrawMeshNow),
		new LuaMethod("DrawProcedural", DrawProcedural),
		new LuaMethod("DrawProceduralIndirect", DrawProceduralIndirect),
		new LuaMethod("DrawTexture", DrawTexture),
		new LuaMethod("Blit", Blit),
		new LuaMethod("BlitMultiTap", BlitMultiTap),
		new LuaMethod("SetRenderTarget", SetRenderTarget),
		new LuaMethod("SetRandomWriteTarget", SetRandomWriteTarget),
		new LuaMethod("ClearRandomWriteTargets", ClearRandomWriteTargets),
		new LuaMethod("New", _CreateGraphics),
		new LuaMethod("GetClassType", GetClassType),
	};

	static LuaField[] fields = new LuaField[]
	{
		new LuaField("activeColorBuffer", get_activeColorBuffer, null),
		new LuaField("activeDepthBuffer", get_activeDepthBuffer, null),
	};

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int _CreateGraphics(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 0)
		{
			Graphics obj = new Graphics();
			LuaScriptMgr.PushObject(L, obj);
			return 1;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Graphics.New");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetClassType(IntPtr L)
	{
		LuaScriptMgr.Push(L, typeof(Graphics));
		return 1;
	}

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Graphics", typeof(Graphics), regs, fields, typeof(System.Object));
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_activeColorBuffer(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, Graphics.activeColorBuffer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int get_activeDepthBuffer(IntPtr L)
	{
		LuaScriptMgr.PushValue(L, Graphics.activeDepthBuffer);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DrawMesh(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 4)
		{
			Mesh arg0 = LuaScriptMgr.GetUnityObject<Mesh>(L, 1);
			Matrix4x4 arg1 = LuaScriptMgr.GetNetObject<Matrix4x4>(L, 2);
			Material arg2 = LuaScriptMgr.GetUnityObject<Material>(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			Graphics.DrawMesh(arg0,arg1,arg2,arg3);
			return 0;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(Mesh), typeof(LuaTable), typeof(LuaTable), typeof(Material), typeof(int)))
		{
			Mesh arg0 = LuaScriptMgr.GetUnityObject<Mesh>(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Quaternion arg2 = LuaScriptMgr.GetQuaternion(L, 3);
			Material arg3 = LuaScriptMgr.GetUnityObject<Material>(L, 4);
			int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
			Graphics.DrawMesh(arg0,arg1,arg2,arg3,arg4);
			return 0;
		}
		else if (count == 5 && LuaScriptMgr.CheckTypes(L, 1, typeof(Mesh), typeof(Matrix4x4), typeof(Material), typeof(int), typeof(Camera)))
		{
			Mesh arg0 = LuaScriptMgr.GetUnityObject<Mesh>(L, 1);
			Matrix4x4 arg1 = LuaScriptMgr.GetNetObject<Matrix4x4>(L, 2);
			Material arg2 = LuaScriptMgr.GetUnityObject<Material>(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			Camera arg4 = LuaScriptMgr.GetUnityObject<Camera>(L, 5);
			Graphics.DrawMesh(arg0,arg1,arg2,arg3,arg4);
			return 0;
		}
		else if (count == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(Mesh), typeof(Matrix4x4), typeof(Material), typeof(int), typeof(Camera), typeof(int)))
		{
			Mesh arg0 = LuaScriptMgr.GetUnityObject<Mesh>(L, 1);
			Matrix4x4 arg1 = LuaScriptMgr.GetNetObject<Matrix4x4>(L, 2);
			Material arg2 = LuaScriptMgr.GetUnityObject<Material>(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			Camera arg4 = LuaScriptMgr.GetUnityObject<Camera>(L, 5);
			int arg5 = (int)LuaScriptMgr.GetNumber(L, 6);
			Graphics.DrawMesh(arg0,arg1,arg2,arg3,arg4,arg5);
			return 0;
		}
		else if (count == 6 && LuaScriptMgr.CheckTypes(L, 1, typeof(Mesh), typeof(LuaTable), typeof(LuaTable), typeof(Material), typeof(int), typeof(Camera)))
		{
			Mesh arg0 = LuaScriptMgr.GetUnityObject<Mesh>(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Quaternion arg2 = LuaScriptMgr.GetQuaternion(L, 3);
			Material arg3 = LuaScriptMgr.GetUnityObject<Material>(L, 4);
			int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
			Camera arg5 = LuaScriptMgr.GetUnityObject<Camera>(L, 6);
			Graphics.DrawMesh(arg0,arg1,arg2,arg3,arg4,arg5);
			return 0;
		}
		else if (count == 7 && LuaScriptMgr.CheckTypes(L, 1, typeof(Mesh), typeof(Matrix4x4), typeof(Material), typeof(int), typeof(Camera), typeof(int), typeof(MaterialPropertyBlock)))
		{
			Mesh arg0 = LuaScriptMgr.GetUnityObject<Mesh>(L, 1);
			Matrix4x4 arg1 = LuaScriptMgr.GetNetObject<Matrix4x4>(L, 2);
			Material arg2 = LuaScriptMgr.GetUnityObject<Material>(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			Camera arg4 = LuaScriptMgr.GetUnityObject<Camera>(L, 5);
			int arg5 = (int)LuaScriptMgr.GetNumber(L, 6);
			MaterialPropertyBlock arg6 = LuaScriptMgr.GetNetObject<MaterialPropertyBlock>(L, 7);
			Graphics.DrawMesh(arg0,arg1,arg2,arg3,arg4,arg5,arg6);
			return 0;
		}
		else if (count == 7 && LuaScriptMgr.CheckTypes(L, 1, typeof(Mesh), typeof(LuaTable), typeof(LuaTable), typeof(Material), typeof(int), typeof(Camera), typeof(int)))
		{
			Mesh arg0 = LuaScriptMgr.GetUnityObject<Mesh>(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Quaternion arg2 = LuaScriptMgr.GetQuaternion(L, 3);
			Material arg3 = LuaScriptMgr.GetUnityObject<Material>(L, 4);
			int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
			Camera arg5 = LuaScriptMgr.GetUnityObject<Camera>(L, 6);
			int arg6 = (int)LuaScriptMgr.GetNumber(L, 7);
			Graphics.DrawMesh(arg0,arg1,arg2,arg3,arg4,arg5,arg6);
			return 0;
		}
		else if (count == 8)
		{
			Mesh arg0 = LuaScriptMgr.GetUnityObject<Mesh>(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Quaternion arg2 = LuaScriptMgr.GetQuaternion(L, 3);
			Material arg3 = LuaScriptMgr.GetUnityObject<Material>(L, 4);
			int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
			Camera arg5 = LuaScriptMgr.GetUnityObject<Camera>(L, 6);
			int arg6 = (int)LuaScriptMgr.GetNumber(L, 7);
			MaterialPropertyBlock arg7 = LuaScriptMgr.GetNetObject<MaterialPropertyBlock>(L, 8);
			Graphics.DrawMesh(arg0,arg1,arg2,arg3,arg4,arg5,arg6,arg7);
			return 0;
		}
		else if (count == 9)
		{
			Mesh arg0 = LuaScriptMgr.GetUnityObject<Mesh>(L, 1);
			Matrix4x4 arg1 = LuaScriptMgr.GetNetObject<Matrix4x4>(L, 2);
			Material arg2 = LuaScriptMgr.GetUnityObject<Material>(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			Camera arg4 = LuaScriptMgr.GetUnityObject<Camera>(L, 5);
			int arg5 = (int)LuaScriptMgr.GetNumber(L, 6);
			MaterialPropertyBlock arg6 = LuaScriptMgr.GetNetObject<MaterialPropertyBlock>(L, 7);
			bool arg7 = LuaScriptMgr.GetBoolean(L, 8);
			bool arg8 = LuaScriptMgr.GetBoolean(L, 9);
			Graphics.DrawMesh(arg0,arg1,arg2,arg3,arg4,arg5,arg6,arg7,arg8);
			return 0;
		}
		else if (count == 10)
		{
			Mesh arg0 = LuaScriptMgr.GetUnityObject<Mesh>(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Quaternion arg2 = LuaScriptMgr.GetQuaternion(L, 3);
			Material arg3 = LuaScriptMgr.GetUnityObject<Material>(L, 4);
			int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
			Camera arg5 = LuaScriptMgr.GetUnityObject<Camera>(L, 6);
			int arg6 = (int)LuaScriptMgr.GetNumber(L, 7);
			MaterialPropertyBlock arg7 = LuaScriptMgr.GetNetObject<MaterialPropertyBlock>(L, 8);
			bool arg8 = LuaScriptMgr.GetBoolean(L, 9);
			bool arg9 = LuaScriptMgr.GetBoolean(L, 10);
			Graphics.DrawMesh(arg0,arg1,arg2,arg3,arg4,arg5,arg6,arg7,arg8,arg9);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Graphics.DrawMesh");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DrawMeshNow(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Mesh arg0 = LuaScriptMgr.GetUnityObject<Mesh>(L, 1);
			Matrix4x4 arg1 = LuaScriptMgr.GetNetObject<Matrix4x4>(L, 2);
			Graphics.DrawMeshNow(arg0,arg1);
			return 0;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Mesh), typeof(Matrix4x4), typeof(int)))
		{
			Mesh arg0 = LuaScriptMgr.GetUnityObject<Mesh>(L, 1);
			Matrix4x4 arg1 = LuaScriptMgr.GetNetObject<Matrix4x4>(L, 2);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
			Graphics.DrawMeshNow(arg0,arg1,arg2);
			return 0;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Mesh), typeof(LuaTable), typeof(LuaTable)))
		{
			Mesh arg0 = LuaScriptMgr.GetUnityObject<Mesh>(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Quaternion arg2 = LuaScriptMgr.GetQuaternion(L, 3);
			Graphics.DrawMeshNow(arg0,arg1,arg2);
			return 0;
		}
		else if (count == 4)
		{
			Mesh arg0 = LuaScriptMgr.GetUnityObject<Mesh>(L, 1);
			Vector3 arg1 = LuaScriptMgr.GetVector3(L, 2);
			Quaternion arg2 = LuaScriptMgr.GetQuaternion(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			Graphics.DrawMeshNow(arg0,arg1,arg2,arg3);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Graphics.DrawMeshNow");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DrawProcedural(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		if (count == 2)
		{
			MeshTopology arg0 = LuaScriptMgr.GetNetObject<MeshTopology>(L, 1);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
			Graphics.DrawProcedural(arg0,arg1);
			return 0;
		}
		else if (count == 3)
		{
			MeshTopology arg0 = LuaScriptMgr.GetNetObject<MeshTopology>(L, 1);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
			Graphics.DrawProcedural(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Graphics.DrawProcedural");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DrawProceduralIndirect(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		if (count == 2)
		{
			MeshTopology arg0 = LuaScriptMgr.GetNetObject<MeshTopology>(L, 1);
			ComputeBuffer arg1 = LuaScriptMgr.GetNetObject<ComputeBuffer>(L, 2);
			Graphics.DrawProceduralIndirect(arg0,arg1);
			return 0;
		}
		else if (count == 3)
		{
			MeshTopology arg0 = LuaScriptMgr.GetNetObject<MeshTopology>(L, 1);
			ComputeBuffer arg1 = LuaScriptMgr.GetNetObject<ComputeBuffer>(L, 2);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
			Graphics.DrawProceduralIndirect(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Graphics.DrawProceduralIndirect");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int DrawTexture(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2)
		{
			Rect arg0 = LuaScriptMgr.GetNetObject<Rect>(L, 1);
			Texture arg1 = LuaScriptMgr.GetUnityObject<Texture>(L, 2);
			Graphics.DrawTexture(arg0,arg1);
			return 0;
		}
		else if (count == 3)
		{
			Rect arg0 = LuaScriptMgr.GetNetObject<Rect>(L, 1);
			Texture arg1 = LuaScriptMgr.GetUnityObject<Texture>(L, 2);
			Material arg2 = LuaScriptMgr.GetUnityObject<Material>(L, 3);
			Graphics.DrawTexture(arg0,arg1,arg2);
			return 0;
		}
		else if (count == 6)
		{
			Rect arg0 = LuaScriptMgr.GetNetObject<Rect>(L, 1);
			Texture arg1 = LuaScriptMgr.GetUnityObject<Texture>(L, 2);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
			int arg5 = (int)LuaScriptMgr.GetNumber(L, 6);
			Graphics.DrawTexture(arg0,arg1,arg2,arg3,arg4,arg5);
			return 0;
		}
		else if (count == 7 && LuaScriptMgr.CheckTypes(L, 1, typeof(Rect), typeof(Texture), typeof(Rect), typeof(int), typeof(int), typeof(int), typeof(int)))
		{
			Rect arg0 = LuaScriptMgr.GetNetObject<Rect>(L, 1);
			Texture arg1 = LuaScriptMgr.GetUnityObject<Texture>(L, 2);
			Rect arg2 = LuaScriptMgr.GetNetObject<Rect>(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
			int arg5 = (int)LuaScriptMgr.GetNumber(L, 6);
			int arg6 = (int)LuaScriptMgr.GetNumber(L, 7);
			Graphics.DrawTexture(arg0,arg1,arg2,arg3,arg4,arg5,arg6);
			return 0;
		}
		else if (count == 7 && LuaScriptMgr.CheckTypes(L, 1, typeof(Rect), typeof(Texture), typeof(int), typeof(int), typeof(int), typeof(int), typeof(Material)))
		{
			Rect arg0 = LuaScriptMgr.GetNetObject<Rect>(L, 1);
			Texture arg1 = LuaScriptMgr.GetUnityObject<Texture>(L, 2);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
			int arg5 = (int)LuaScriptMgr.GetNumber(L, 6);
			Material arg6 = LuaScriptMgr.GetUnityObject<Material>(L, 7);
			Graphics.DrawTexture(arg0,arg1,arg2,arg3,arg4,arg5,arg6);
			return 0;
		}
		else if (count == 8 && LuaScriptMgr.CheckTypes(L, 1, typeof(Rect), typeof(Texture), typeof(Rect), typeof(int), typeof(int), typeof(int), typeof(int), typeof(LuaTable)))
		{
			Rect arg0 = LuaScriptMgr.GetNetObject<Rect>(L, 1);
			Texture arg1 = LuaScriptMgr.GetUnityObject<Texture>(L, 2);
			Rect arg2 = LuaScriptMgr.GetNetObject<Rect>(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
			int arg5 = (int)LuaScriptMgr.GetNumber(L, 6);
			int arg6 = (int)LuaScriptMgr.GetNumber(L, 7);
			Color arg7 = LuaScriptMgr.GetColor(L, 8);
			Graphics.DrawTexture(arg0,arg1,arg2,arg3,arg4,arg5,arg6,arg7);
			return 0;
		}
		else if (count == 8 && LuaScriptMgr.CheckTypes(L, 1, typeof(Rect), typeof(Texture), typeof(Rect), typeof(int), typeof(int), typeof(int), typeof(int), typeof(Material)))
		{
			Rect arg0 = LuaScriptMgr.GetNetObject<Rect>(L, 1);
			Texture arg1 = LuaScriptMgr.GetUnityObject<Texture>(L, 2);
			Rect arg2 = LuaScriptMgr.GetNetObject<Rect>(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
			int arg5 = (int)LuaScriptMgr.GetNumber(L, 6);
			int arg6 = (int)LuaScriptMgr.GetNumber(L, 7);
			Material arg7 = LuaScriptMgr.GetUnityObject<Material>(L, 8);
			Graphics.DrawTexture(arg0,arg1,arg2,arg3,arg4,arg5,arg6,arg7);
			return 0;
		}
		else if (count == 9)
		{
			Rect arg0 = LuaScriptMgr.GetNetObject<Rect>(L, 1);
			Texture arg1 = LuaScriptMgr.GetUnityObject<Texture>(L, 2);
			Rect arg2 = LuaScriptMgr.GetNetObject<Rect>(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			int arg4 = (int)LuaScriptMgr.GetNumber(L, 5);
			int arg5 = (int)LuaScriptMgr.GetNumber(L, 6);
			int arg6 = (int)LuaScriptMgr.GetNumber(L, 7);
			Color arg7 = LuaScriptMgr.GetColor(L, 8);
			Material arg8 = LuaScriptMgr.GetUnityObject<Material>(L, 9);
			Graphics.DrawTexture(arg0,arg1,arg2,arg3,arg4,arg5,arg6,arg7,arg8);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Graphics.DrawTexture");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int Blit(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Texture), typeof(Material)))
		{
			Texture arg0 = LuaScriptMgr.GetUnityObject<Texture>(L, 1);
			Material arg1 = LuaScriptMgr.GetUnityObject<Material>(L, 2);
			Graphics.Blit(arg0,arg1);
			return 0;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(Texture), typeof(RenderTexture)))
		{
			Texture arg0 = LuaScriptMgr.GetUnityObject<Texture>(L, 1);
			RenderTexture arg1 = LuaScriptMgr.GetUnityObject<RenderTexture>(L, 2);
			Graphics.Blit(arg0,arg1);
			return 0;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Texture), typeof(Material), typeof(int)))
		{
			Texture arg0 = LuaScriptMgr.GetUnityObject<Texture>(L, 1);
			Material arg1 = LuaScriptMgr.GetUnityObject<Material>(L, 2);
			int arg2 = (int)LuaScriptMgr.GetNumber(L, 3);
			Graphics.Blit(arg0,arg1,arg2);
			return 0;
		}
		else if (count == 3 && LuaScriptMgr.CheckTypes(L, 1, typeof(Texture), typeof(RenderTexture), typeof(Material)))
		{
			Texture arg0 = LuaScriptMgr.GetUnityObject<Texture>(L, 1);
			RenderTexture arg1 = LuaScriptMgr.GetUnityObject<RenderTexture>(L, 2);
			Material arg2 = LuaScriptMgr.GetUnityObject<Material>(L, 3);
			Graphics.Blit(arg0,arg1,arg2);
			return 0;
		}
		else if (count == 4)
		{
			Texture arg0 = LuaScriptMgr.GetUnityObject<Texture>(L, 1);
			RenderTexture arg1 = LuaScriptMgr.GetUnityObject<RenderTexture>(L, 2);
			Material arg2 = LuaScriptMgr.GetUnityObject<Material>(L, 3);
			int arg3 = (int)LuaScriptMgr.GetNumber(L, 4);
			Graphics.Blit(arg0,arg1,arg2,arg3);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Graphics.Blit");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int BlitMultiTap(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);
		Texture arg0 = LuaScriptMgr.GetUnityObject<Texture>(L, 1);
		RenderTexture arg1 = LuaScriptMgr.GetUnityObject<RenderTexture>(L, 2);
		Material arg2 = LuaScriptMgr.GetUnityObject<Material>(L, 3);
		Vector2[] objs3 = LuaScriptMgr.GetParamsObject<Vector2>(L, 4, count - 3);
		Graphics.BlitMultiTap(arg0,arg1,arg2,objs3);
		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetRenderTarget(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 1)
		{
			RenderTexture arg0 = LuaScriptMgr.GetUnityObject<RenderTexture>(L, 1);
			Graphics.SetRenderTarget(arg0);
			return 0;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(RenderBuffer), typeof(RenderBuffer)))
		{
			RenderBuffer arg0 = LuaScriptMgr.GetNetObject<RenderBuffer>(L, 1);
			RenderBuffer arg1 = LuaScriptMgr.GetNetObject<RenderBuffer>(L, 2);
			Graphics.SetRenderTarget(arg0,arg1);
			return 0;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(RenderBuffer[]), typeof(RenderBuffer)))
		{
			RenderBuffer[] objs0 = LuaScriptMgr.GetArrayObject<RenderBuffer>(L, 1);
			RenderBuffer arg1 = LuaScriptMgr.GetNetObject<RenderBuffer>(L, 2);
			Graphics.SetRenderTarget(objs0,arg1);
			return 0;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(RenderTexture), typeof(int)))
		{
			RenderTexture arg0 = LuaScriptMgr.GetUnityObject<RenderTexture>(L, 1);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
			Graphics.SetRenderTarget(arg0,arg1);
			return 0;
		}
		else if (count == 3)
		{
			RenderTexture arg0 = LuaScriptMgr.GetUnityObject<RenderTexture>(L, 1);
			int arg1 = (int)LuaScriptMgr.GetNumber(L, 2);
			CubemapFace arg2 = LuaScriptMgr.GetNetObject<CubemapFace>(L, 3);
			Graphics.SetRenderTarget(arg0,arg1,arg2);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Graphics.SetRenderTarget");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int SetRandomWriteTarget(IntPtr L)
	{
		int count = LuaDLL.lua_gettop(L);

		if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(int), typeof(ComputeBuffer)))
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			ComputeBuffer arg1 = LuaScriptMgr.GetNetObject<ComputeBuffer>(L, 2);
			Graphics.SetRandomWriteTarget(arg0,arg1);
			return 0;
		}
		else if (count == 2 && LuaScriptMgr.CheckTypes(L, 1, typeof(int), typeof(RenderTexture)))
		{
			int arg0 = (int)LuaScriptMgr.GetNumber(L, 1);
			RenderTexture arg1 = LuaScriptMgr.GetUnityObject<RenderTexture>(L, 2);
			Graphics.SetRandomWriteTarget(arg0,arg1);
			return 0;
		}
		else
		{
			LuaDLL.luaL_error(L, "invalid arguments to method: Graphics.SetRandomWriteTarget");
		}

		return 0;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int ClearRandomWriteTargets(IntPtr L)
	{
		LuaScriptMgr.CheckArgsCount(L, 0);
		Graphics.ClearRandomWriteTargets();
		return 0;
	}
}

