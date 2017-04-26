using UnityEngine;
using System.Collections;

public class AlphaTween : MonoBehaviour {
		public AnimationCurve curve;
		public float delayShow=0;
		public WrapMode loop =  WrapMode.Loop;
		float startTime;
		// Use this for initialization
		void Start () {
				if (delayShow > 0) {

						Invoke("show",delayShow);

						gameObject.SetActive (false);
				}
				startTime = Time.realtimeSinceStartup;
				curve.postWrapMode= loop;
		}
		void show(){

				//	yield	return  new WaitForSeconds(delayShow);
				gameObject.SetActive (true);
				startTime = Time.realtimeSinceStartup;
		}

		// Update is called once per frame
		void Update () {

				float dt=  Time.realtimeSinceStartup-startTime;

				Color c = renderer.material.GetColor("_TintColor");
				c.a= curve.Evaluate (dt);
		renderer.material.SetColor ("_TintColor",c);


		}
}
