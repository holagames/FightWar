using UnityEngine;
using System.Collections;

public class JoinCountryWindow : MonoBehaviour {
    public GameObject TongmengTex;
    public GameObject DiGuoTex;
    public GameObject JoinButton;
    public GameObject SureButton;

	void Start () {

        UIEventListener.Get(JoinButton).onClick = delegate(GameObject go)
        {
            DestroyImmediate(this.gameObject);
        };

        UIEventListener.Get(SureButton).onClick = delegate(GameObject go)
        {
            DestroyImmediate(this.gameObject);
        };

        if (CharacterRecorder.instance.legionCountryID == 1) 
        {
            TongmengTex.SetActive(true);
            DiGuoTex.SetActive(false);
            AudioEditer.instance.PlayOneShot("Countryblood");
        }
        else if (CharacterRecorder.instance.legionCountryID == 2) 
        {
            TongmengTex.SetActive(false);
            DiGuoTex.SetActive(true);
            AudioEditer.instance.PlayOneShot("Countryforce");
        }
	}
	
}
