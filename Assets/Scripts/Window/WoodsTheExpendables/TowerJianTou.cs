using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerJianTou : MonoBehaviour {
    public List<Texture> IconList = new List<Texture>();
    public GameObject MyItem;
	// Use this for initialization
	void Start () {
        StartCoroutine("UpdatTexture");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnDestroy() {
        StopCoroutine(UpdatTexture());
    }
    IEnumerator UpdatTexture() {
        for (int i = 0; i < IconList.Count; i++)
        {

            MyItem.renderer.material.mainTexture = IconList[i];
            if ((IconList.Count - 1) == i) {
                i = 0;
            }
            yield return new WaitForSeconds(0.15f);
        }
    }
}
