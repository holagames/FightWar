using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DowerWindow : MonoBehaviour
{
    public static DowerWindow instance;
    public GameObject RefreshWindow;
     GameObject ExchangeWindow;
     GameObject TypeWindow;
    public List<GameObject> ButtenList = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        instance = this;
        for (int i = 0; i < ButtenList.Count; i++)
        {

            UIEventListener.Get(ButtenList[i]).onClick = SetItem;
        }
    }
    // Update is called once per frame
    void SetItem(GameObject go)
    {
        if (go.transform.FindChild("Lock").gameObject.activeSelf == false)
        {
            UIManager.instance.OpenPanel("TypeWindow",false);
            if (GameObject.Find("TypeWindow")!=null)
            {
                TypeWindow = GameObject.Find("TypeWindow");
            }
            TypeWindow.GetComponent<TypeWindow>().SetWindow(go, int.Parse(go.transform.FindChild("Num").GetComponent<UILabel>().text));
        }
        else
        {
            UIManager.instance.OpenPanel("TypeWindow", false);
            if (GameObject.Find("TypeWindow") != null)
            {
                 TypeWindow = GameObject.Find("TypeWindow");
            }
           
          //  TypeWindow.GetComponent<TypeWindow>().SetWindow(go, int.Parse(go.transform.FindChild("Num").GetComponent<UILabel>().text));
            TypeWindow.GetComponent<TypeWindow>().NotWidow(go);
        }
    }


    public void SetAllItem(string[] heroDower)//设置单个
    {
        for (int i = 0; i < ButtenList.Count; i++)
        {
            ButtenList[i].transform.FindChild("Num").GetComponent<UILabel>().text = heroDower[int.Parse(ButtenList[i].name) - 1].ToString();
            if (i == 0 || i == 6 || i == 12)
            {
               
                if (ButtenList[i].transform.FindChild("Num").GetComponent<UILabel>().text == "10")
                {
                    ButtenList[i].transform.FindChild("Num").GetComponent<UILabel>().color = new Color(53 / 255f, 239 / 255f, 9 / 255f);
                }
                else
                {
                    ButtenList[i].transform.FindChild("Num").GetComponent<UILabel>().color = new Color((53 / 255f), 239 / 255f, 9 / 255f);
                }
                ButtenList[i].transform.FindChild("Lock").gameObject.SetActive(false);
                ButtenList[i].transform.FindChild("Icon").GetComponent<UISprite>().spriteName = "";
            }
            else
            {
                ButtenList[i].transform.FindChild("Lock").gameObject.SetActive(true);
                ButtenList[i].transform.FindChild("Num").GetComponent<UILabel>().color = new Color(1,1,1);
                ButtenList[i].transform.FindChild("Icon").GetComponent<UISprite>().spriteName = "";
            }
            if (i == 17)
            {
                for (int j = 0; j < 18; j = j + 6)
                {
                    SetItem(j);
                }
            }
        }

    }
    public void SetItem(int num)
    {
        if (int.Parse(ButtenList[0 + num].transform.FindChild("Num").GetComponent<UILabel>().text) >= 5)
        {
            for (int i = 1 + num; i < 3 + num; i++)
            {
                ButtenList[i].transform.FindChild("Lock").gameObject.SetActive(false);
                ButtenList[i].transform.FindChild("Icon").GetComponent<UISprite>().spriteName = "";
                if (ButtenList[i].transform.FindChild("Num").GetComponent<UILabel>().text=="10")
                {
                    ButtenList[i].transform.FindChild("Num").GetComponent<UILabel>().color = new Color(1, 1, 1);
                }
                else
                {
                    ButtenList[i].transform.FindChild("Num").GetComponent<UILabel>().color = new Color((53/255f), 239/255f,9/255f);
                }
            }
        }
        if (int.Parse(ButtenList[1 + num].transform.FindChild("Num").GetComponent<UILabel>().text) + int.Parse(ButtenList[2 + num].transform.FindChild("Num").GetComponent<UILabel>().text) >= 8)
        {
            for (int i = 3 + num; i < 6 + num; i++)
            {
                ButtenList[i].transform.FindChild("Lock").gameObject.SetActive(false);
                ButtenList[i].transform.FindChild("Icon").GetComponent<UISprite>().spriteName = "";
                if (ButtenList[i].transform.FindChild("Num").GetComponent<UILabel>().text == "10")
                {
                    ButtenList[i].transform.FindChild("Num").GetComponent<UILabel>().color = new Color(1, 1, 1);
                }
                else
                {
                    ButtenList[i].transform.FindChild("Num").GetComponent<UILabel>().color = new Color((53 / 255f), 239 / 255f, 9 / 255f);
                }
            }
        }
    }
}
