using UnityEngine;
using System.Collections;

public class StrengerSelectItem : MonoBehaviour {

    public UILabel Name;
    public UILabel Des;
    public GameObject Gou;

	// Use this for initialization
	void Start () {
	
	}

    public void SetInfo(string _Name,string _Des)
    {
        UIEventListener.Get(this.gameObject).onClick = delegate(GameObject go)
        {
            if (Gou.activeSelf)
            {
                Gou.SetActive(false);
            }
            else
            {
                Gou.SetActive(true);
            }
        };

        Name.text = _Name;
        Des.text = _Des;
    }
}
