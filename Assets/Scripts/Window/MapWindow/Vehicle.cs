using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {
    public GameObject Submarine;
    public GameObject Submarine1;
    public GameObject Submarine2;
	// Use this for initialization
	void Start () {
        //StartCoroutine("InstantiateSubmarine");
        //StartCoroutine("InstantiateSubmarine1");
        InvokeRepeating("InstantiateSubmarine", 0f, 10f);
        InvokeRepeating("InstantiateSubmarine1", 0f, 16f);
	}
	
	// Update is called once per frame
	void Update () {
	 
	}

    void InstantiateSubmarine()
    {
        //while (true)
        {
            InstantiateObj(Submarine);
            InstantiateObj(Submarine1);
        }
    }

    void InstantiateSubmarine1()
    {
        //while (true)
        {
            InstantiateObj(Submarine2);
        }
    }

    //IEnumerator  InstantiateSubmarine() {
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(10f);
    //        InstantiateObj(Submarine);
    //        InstantiateObj(Submarine1);
           
         
    //    }
    //}
    //IEnumerator InstantiateSubmarine1() {
    //    while (true) {
    //        yield return new WaitForSeconds(16f);
    //        InstantiateObj(Submarine2);
    //    }
    //}
    public void InstantiateObj(GameObject SubmarineObj) {
        GameObject obj = Instantiate(SubmarineObj) as GameObject;
        obj.transform.parent = SubmarineObj.transform.parent;
        obj.transform.localScale = SubmarineObj.transform.localScale;
        obj.transform.localPosition = SubmarineObj.transform.localPosition;
        obj.transform.localRotation = SubmarineObj.transform.localRotation;
        obj.SetActive(true);
    }
}
