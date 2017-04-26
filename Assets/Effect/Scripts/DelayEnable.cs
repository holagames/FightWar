using UnityEngine;
using System.Collections;

public class DelayEnable : MonoBehaviour {

	public float time=2;
	void Start () {
		gameObject.SetActive(false);
  		Invoke("show",time);
	}
	
 	void show () {
 		gameObject.SetActive(true);

	}
}
