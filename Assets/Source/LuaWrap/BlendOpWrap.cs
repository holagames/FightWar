using System;
using UnityEngine.Rendering;
using LuaInterface;

public class BlendOpWrap
{
	static LuaMethod[] enums = new LuaMethod[]
	{
		new LuaMethod("Add", GetAdd),
		new LuaMethod("Subtract", GetSubtract),
		new LuaMethod("ReverseSubtract", GetReverseSubtract),
		new LuaMethod("Min", GetMin),
		new LuaMethod("Max", GetMax),
		new LuaMethod("LogicalClear", GetLogicalClear),
		new LuaMethod("LogicalSet", GetLogicalSet),
		new LuaMethod("LogicalCopy", GetLogicalCopy),
		new LuaMethod("LogicalCopyInverted", GetLogicalCopyInverted),
		new LuaMethod("LogicalNoop", GetLogicalNoop),
		new LuaMethod("LogicalInvert", GetLogicalInvert),
		new LuaMethod("LogicalAnd", GetLogicalAnd),
		new LuaMethod("LogicalNand", GetLogicalNand),
		new LuaMethod("LogicalOr", GetLogicalOr),
		new LuaMethod("LogicalNor", GetLogicalNor),
		new LuaMethod("LogicalXor", GetLogicalXor),
		new LuaMethod("LogicalEquivalence", GetLogicalEquivalence),
		new LuaMethod("LogicalAndReverse", GetLogicalAndReverse),
		new LuaMethod("LogicalAndInverted", GetLogicalAndInverted),
		new LuaMethod("LogicalOrReverse", GetLogicalOrReverse),
		new LuaMethod("LogicalOrInverted", GetLogicalOrInverted),
		new LuaMethod("IntToEnum", IntToEnum),
	};

	public static void Register(IntPtr L)
	{
		LuaScriptMgr.RegisterLib(L, "UnityEngine.Rendering.BlendOp", typeof(BlendOp), enums);
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetAdd(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendOp.Add);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetSubtract(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendOp.Subtract);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetReverseSubtract(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendOp.ReverseSubtract);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMin(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendOp.Min);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetMax(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendOp.Max);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLogicalClear(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendOp.LogicalClear);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLogicalSet(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendOp.LogicalSet);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLogicalCopy(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendOp.LogicalCopy);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLogicalCopyInverted(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendOp.LogicalCopyInverted);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLogicalNoop(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendOp.LogicalNoop);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLogicalInvert(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendOp.LogicalInvert);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLogicalAnd(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendOp.LogicalAnd);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLogicalNand(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendOp.LogicalNand);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLogicalOr(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendOp.LogicalOr);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLogicalNor(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendOp.LogicalNor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLogicalXor(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendOp.LogicalXor);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLogicalEquivalence(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendOp.LogicalEquivalence);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLogicalAndReverse(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendOp.LogicalAndReverse);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLogicalAndInverted(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendOp.LogicalAndInverted);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLogicalOrReverse(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendOp.LogicalOrReverse);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int GetLogicalOrInverted(IntPtr L)
	{
		LuaScriptMgr.Push(L, BlendOp.LogicalOrInverted);
		return 1;
	}

	[MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
	static int IntToEnum(IntPtr L)
	{
		int arg0 = (int)LuaDLL.lua_tonumber(L, 1);
		BlendOp o = (BlendOp)arg0;
		LuaScriptMgr.Push(L, o);
		return 1;
	}
}

