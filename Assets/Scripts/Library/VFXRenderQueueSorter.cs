using UnityEngine;
using System.Collections;

public class VFXRenderQueueSorter : MonoBehaviour
{
	public enum RenderSortingType
	{
		FRONT,
		BACK
	}
	
	public UIWidget mTarget = null;
	public int mQueue;
	public int mWidgetQueue;
	public RenderSortingType mType = RenderSortingType.FRONT;
	public bool ShouldScaleVFX;
	private bool IsScaled;
	public bool SetQueManually;

	private Renderer[] mRenderers;
	private int mLastQueue = 0;
	
	void Awake ()
	{
		mRenderers = GetComponentsInChildren<Renderer>();
		//if (mTarget == null)
			//mTarget = GetClosestParentUIWidget();
	}

	private ParticleSystem[] systems;

	void Start()
	{
		systems = GetComponentsInChildren<ParticleSystem>();
		if (ShouldScaleVFX)
		{
			//stop emitting particle until scale is done. Can't be in the enable because it's processed before ShotScaleVFX is set
			foreach(ParticleSystem ps in systems)
				ps.enableEmission = false;
		}

	}
	
	void StartParticleAfterScale()
	{
		foreach(ParticleSystem ps in systems)
		{
			ps.enableEmission = true;
			ps.Play(true);
		}
	}

	private bool mReady;
	void Update()
	{
		if (mReady)
			return;

		if (transform.parent != null)
		{
			if (transform.parent.gameObject.activeInHierarchy)
			{
				mReady = true;
			}
		}
		else if (SetQueManually)
			mReady = true;
	}
	
	void FixedUpdate() {
		if (!mReady)
			return;

		if (ShouldScaleVFX && !IsScaled)
		{
			ApplyScaleParticle();
			StartParticleAfterScale();
			IsScaled = true;
		}

		if (!SetQueManually)
		{
			if (mTarget == null)
			{
				mTarget = GetClosestParentUIWidget();
				if (!SetQueManually)
					return;
			}

			if(mTarget.drawCall == null )
			{
				mTarget = GetClosestParentUIWidget();
				return;
			}

			if (mTarget == null || mTarget.drawCall == null)
			{
				return;
			}

			mWidgetQueue = mTarget.drawCall.renderQueue;
			mQueue = mType == RenderSortingType.FRONT ? mWidgetQueue + 1 : mWidgetQueue + - 1;
		}
			
		if( mLastQueue != mQueue )
		{
			mLastQueue = mQueue;
			
			foreach( Renderer rnd in mRenderers )
			{
                if (rnd != null)
                {
                    rnd.material.renderQueue = mLastQueue;
                }
			}
		}

	}

	private UIWidget GetClosestParentUIWidget()
	{
		UIWidget temp = null;
		bool found = false;
		Transform pr = transform.parent;

		while (pr != null && temp == null)
		{
			var widget = pr.GetComponent<UIWidget>();
			var textures = pr.GetComponentsInChildren<UITexture>();
			foreach (var tex in textures)
			{
				if (tex.drawCall != null)
				{
					temp = tex;
					return temp;
				}
			}
			var labels = pr.GetComponentsInChildren<UILabel>();
			foreach (var lb in labels)
			{
				if (lb.drawCall != null)
				{
					temp = lb;
					return lb;
				}
			}
			var sprites = pr.GetComponentsInChildren<UISprite>();
			foreach (var sp in sprites)
			{
				if (sp.drawCall != null)
				{
					temp = sp;
					return sp;
				}
			}
			/*
			var widgets = pr.GetComponentsInChildren<UIWidget>();
			foreach (var wid in widgets)
			{
				if (wid.drawCall != null)
				{
					temp = wid;
					return temp;
				}
			}
			*/

			pr = pr.parent;
		}

		return temp;
	}

	void ApplyScaleParticle()
	{
		var pSystems = GetComponentsInChildren<ParticleSystem>();
		foreach (var ps in pSystems)
		{
			ps.startSpeed *= Mathf.Abs(transform.lossyScale.x);
			ps.startSize *= Mathf.Abs(transform.lossyScale.x);
		}
	}
}

