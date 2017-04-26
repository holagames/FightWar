using UnityEngine;
using System.Collections;

public class VehicleTexture : MonoBehaviour
{
    public Texture tex1;
    public Texture tex2;
    public bool IsDestroyObj = true;
   // private bool 
	// Use this for initialization
	void Start () {
        StartCoroutine("UpdateTexture");
        this.gameObject.renderer.material.shader = Shader.Find("Unlit/Transparent Colored");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    IEnumerator UpdateTexture() {
        while (IsDestroyObj)
        { 
           this.gameObject.renderer.material.mainTexture = tex1; 
            yield return new WaitForSeconds(0.1f);
            this.gameObject.renderer.material.mainTexture = tex2;
            yield return new WaitForSeconds(0.1f);
        }
    }
    public void DestroyObj()
    {
        IsDestroyObj = false;

        Invoke("Destroy", 0.1f);
    }
    public void Destroy() {
        DestroyImmediate(this.gameObject);
    }
}
