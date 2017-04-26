/*
http://www.cgsoso.com/forum-257-1.html

CG搜搜 Unity3d 插件团购

CGSOSO 主打游戏开发，影视设计等CG资源素材。

每日Unity3d插件免费更新，仅限下载试用，如若商用，请务必官网购买！
*/

using UnityEngine;
using System.Collections;

public class DistortionMobile : MonoBehaviour {

  public float TextureScale = 1f;
  public RenderTextureFormat RenderTextureFormat;
  public FilterMode FilterMode = FilterMode.Point;
  public LayerMask CullingMask = ~(1 << 4);
  public RenderingPath RenderingPath;
  //public bool UpdateCameraWhenMove;
  public int FPSWhenMoveCamera = 40;
  public int FPSWhenStaticCamera = 20;

  private RenderTexture renderTexture;
  private Camera cameraInstance;
  private GameObject goCamera;
  private Vector3 oldPosition;
  private Quaternion oldRotation;
  private Transform instanceCameraTransform;
  private bool canUpdateCamera, isStaticUpdate;
  private WaitForSeconds fpsMove, fpsStatic;
  private const int dropedFrames = 50;
  private int frameCountWhenCameraIsStatic;
  private bool isInitialized;
	// Use this for initialization

  void OnEnable()
  {
    fpsMove = new WaitForSeconds(1.0f / FPSWhenMoveCamera);
    fpsStatic = new WaitForSeconds(1.0f / FPSWhenStaticCamera);
 
  }

  private void Update()
  {
    if (!isInitialized) {
      Initialize();
      StartCoroutine(RepeatCameraMove());
      StartCoroutine(RepeatCameraStatic());
    }
    
    if (Vector3.SqrMagnitude(instanceCameraTransform.position - oldPosition) <= 0.00001f && instanceCameraTransform.rotation==oldRotation) {
      ++frameCountWhenCameraIsStatic;
      if (frameCountWhenCameraIsStatic >= dropedFrames)
        isStaticUpdate = true;
    }
    else {
      frameCountWhenCameraIsStatic = 0;
      isStaticUpdate = false;
    }
    oldPosition = instanceCameraTransform.position;
    oldRotation = instanceCameraTransform.rotation;
    if (canUpdateCamera) {
      cameraInstance.enabled = true;
      canUpdateCamera = false;
    }
    else if (cameraInstance.enabled)
      cameraInstance.enabled = false;
  }

  IEnumerator RepeatCameraMove()
  {
    while (true) {
      if (!isStaticUpdate)
        canUpdateCamera = true;
      yield return fpsMove;
    }
  }

  IEnumerator RepeatCameraStatic()
  {
    while (true)
    {
      if (isStaticUpdate)
        canUpdateCamera = true;
      yield return fpsStatic;
    }
  }

  private void OnBecameVisible()
  {
    if(goCamera!=null) goCamera.SetActive(true);
  }

  void OnBecameInvisible()
  {
    if (goCamera != null) goCamera.SetActive(false);
  }

  
  private void Initialize()
  {
    goCamera = new GameObject("RenderTextureCamera");
    cameraInstance = goCamera.AddComponent<Camera>();
    var cam = Camera.main;
    cameraInstance.CopyFrom(cam);
    cameraInstance.depth++;
    cameraInstance.cullingMask = CullingMask;
    cameraInstance.renderingPath = RenderingPath;
    goCamera.transform.parent = cam.transform;
    renderTexture = new RenderTexture(Mathf.RoundToInt(Screen.width * TextureScale), Mathf.RoundToInt(Screen.height * TextureScale), 16, RenderTextureFormat);
    renderTexture.DiscardContents();
    renderTexture.filterMode = FilterMode;
    cameraInstance.targetTexture = renderTexture;
    instanceCameraTransform = cameraInstance.transform;
    oldPosition = instanceCameraTransform.position;
    Shader.SetGlobalTexture("_GrabTextureMobile", renderTexture);
    isInitialized = true;
  }

  private void OnDisable()
  {
    if (goCamera) {
      DestroyImmediate(goCamera);
      goCamera = null;
    }
    if (renderTexture) {
      DestroyImmediate(renderTexture);
      renderTexture = null;
    }
    isInitialized = false;
  }

}
