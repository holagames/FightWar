/*
http://www.cgsoso.com/forum-257-1.html

CG搜搜 Unity3d 插件团购

CGSOSO 主打游戏开发，影视设计等CG资源素材。

每日Unity3d插件免费更新，仅限下载试用，如若商用，请务必官网购买！
*/

using System.Collections;
using UnityEngine;

public class WaterUvAnimation : MonoBehaviour
{
  public bool IsReverse;
  public float Speed = 1;
  public int MaterialNomber = 0;

  private Material mat;
  private float deltaFps;
  private bool isVisible;
  private bool isCorutineStarted;
  private float offset, delta;
  
  private void Awake()
  {
    mat = GetComponent<Renderer>().materials[MaterialNomber];
  }

  private void Update()
  {
    if (IsReverse)
    {
      offset -= Time.deltaTime*Speed;
      if (offset < 0)
        offset = 1;
    }
    else
    {
      offset += Time.deltaTime * Speed;
      if (offset > 1)
        offset = 0;
    }
    var vec = new Vector2(0, offset);
    mat.SetTextureOffset("_BumpMap", vec);
    mat.SetFloat("_OffsetYHeightMap", offset);
  }
}
