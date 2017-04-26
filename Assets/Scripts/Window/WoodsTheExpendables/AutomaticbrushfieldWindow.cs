using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AutomaticbrushfieldWindow : MonoBehaviour {

    public List<GameObject> buffList = new List<GameObject>();
    public GameObject closeButton;
    public GameObject leftButton;
    public GameObject rightButton;
    public GameObject SureButton;
    public UILabel boxNumber;
    public GameObject thisWindow;

    private int boxnum = 1;
	void Start () {
        CharacterRecorder.instance.AutomaticBuffList.Clear();

        for (int i = 0; i < buffList.Count; i++)
        {
            buffList[i].transform.Find("Checkmark").gameObject.SetActive(false);
        }
        buffList[0].transform.Find("Checkmark").gameObject.SetActive(true);
        buffList[1].transform.Find("Checkmark").gameObject.SetActive(true);
        buffList[2].transform.Find("Checkmark").gameObject.SetActive(true);
        buffList[4].transform.Find("Checkmark").gameObject.SetActive(true);

        CharacterRecorder.instance.AutomaticBuffList.Add(1);
        CharacterRecorder.instance.AutomaticBuffList.Add(2);
        CharacterRecorder.instance.AutomaticBuffList.Add(3);
        CharacterRecorder.instance.AutomaticBuffList.Add(5);

        boxNumber.text = boxnum.ToString();

        for (int i = 0; i < buffList.Count; i++) 
        {
            int num=i;
            UIEventListener.Get(buffList[num]).onClick = delegate(GameObject go)
            {
                if (buffList[num].transform.Find("Checkmark").gameObject.activeSelf)
                {
                    buffList[num].transform.Find("Checkmark").gameObject.SetActive(false);
                    //for (int j = 0; j < CharacterRecorder.instance.AutomaticBuffList.Count; j++) 
                    //{
                    //    if (CharacterRecorder.instance.AutomaticBuffList[j] == num + 1) 
                    //    {
                    //        CharacterRecorder.instance.AutomaticBuffList.RemoveAt(j);
                    //    }
                    //}
                }
                else 
                {
                    buffList[num].transform.Find("Checkmark").gameObject.SetActive(true);
                    //CharacterRecorder.instance.AutomaticBuffList.Add(num + 1);
                }
            };
        }

        UIEventListener.Get(closeButton).onClick = delegate(GameObject go)
        {
            PlayerPrefs.SetInt("Automaticbrushfield" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"),0);
            thisWindow.SetActive(false);
        };

        UIEventListener.Get(leftButton).onClick = delegate(GameObject go)
        {
            if (boxnum > 1)
            {
                boxnum--;
                boxNumber.text = boxnum.ToString();
            }
         
        };

        UIEventListener.Get(rightButton).onClick = delegate(GameObject go)
        {
            if (boxnum <10)
            {
                boxnum++;
                boxNumber.text = boxnum.ToString();
            }
        };

        UIEventListener.Get(SureButton).onClick = delegate(GameObject go)
        {
            CharacterRecorder.instance.AutomaticBuffList.Clear();
            for (int i = 0; i < buffList.Count; i++) 
            {
                if (buffList[i].transform.Find("Checkmark").gameObject.activeSelf) 
                {
                    CharacterRecorder.instance.AutomaticBuffList.Add(i + 1);
                }
            }
            PlayerPrefs.SetInt("Automaticbrushfield" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"),1);
            CharacterRecorder.instance.AutomaticOpenBoxNum = boxnum;
            PlayerPrefs.SetInt("AutomaticOpenBoxNum" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID"), boxnum);
            thisWindow.SetActive(false);
            GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().AutomaticbrushButton.transform.Find("Checkmark").gameObject.SetActive(true);
            //GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().WoodObject.GetComponent<WoodsTheExpendablesMapList>().NotGetMouseToChooseWindow();
            Invoke("invoksongmetime", 3f);
        };
	}

    void invoksongmetime() 
    {
        GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().WoodObject.GetComponent<WoodsTheExpendablesMapList>().NotGetMouseToChooseWindow();
    }
}
