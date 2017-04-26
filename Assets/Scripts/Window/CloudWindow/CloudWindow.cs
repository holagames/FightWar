using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudWindow : MonoBehaviour {
	// Use this for initialization
	void Start () {
        StartCoroutine("UpdateSprite");
	}
	
    IEnumerator UpdateSprite() {
        yield return new WaitForSeconds(0.5f);
        DestroyImmediate(this.gameObject);
    }
}
