using UnityEngine;
using System.Collections;

public class RefreshWindow : MonoBehaviour {
    public UILabel MenoyNum;
    public UILabel DowerNum;
    public GameObject SureButten;
    public GameObject CloseButten;
    public GameObject CancelButten;
	// Use this for initialization
	void Start ()
    {
        UIEventListener.Get(SureButten).onClick += delegate (GameObject go)
        {
            string[] dsd = { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
            TextTranslator.instance.AddHeroDicv(1, dsd);
        };
        UIEventListener.Get(CloseButten).onClick += delegate (GameObject go)
        {
            this.gameObject.SetActive(false);
        };
        UIEventListener.Get(CancelButten).onClick += delegate (GameObject go)
        {
            this.gameObject.SetActive(false);
        };
    }
	
	public void SetWindow(string  menoy,string num)
    {
        MenoyNum.text = menoy;
        DowerNum.text = num;
    }
}
