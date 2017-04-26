using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AwardWindow : MonoBehaviour
{
    public GameObject onceEffect;
    public GameObject FlyLight;
    public GameObject HappyGet;
    public GameObject BlueSun;
    public GameObject BG;
    public GameObject BGLight;
    public GameObject Line;

    public GameObject Label1;
    public GameObject Label2;

    public GameObject SureButton;

    public GameObject One;
    public GameObject Two;
    public GameObject Three;
    public GameObject Four;
    public GameObject Five;


    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;
    public delegate void SureOnClick();
    private SureOnClick _CurSureOnClick;
    int number = 0;
    // Use this for initialization
    void Start()
    {
        if (UIEventListener.Get(SureButton).onClick == null)
        {
            UIEventListener.Get(SureButton).onClick = EscOnClick;

        }
        //StartCoroutine(SetEffect());

        //if (UIEventListener.Get(GameObject.Find("AwardCloseButton")).onClick == null)
        //{
        //    UIEventListener.Get(GameObject.Find("AwardCloseButton")).onClick += delegate(GameObject go)
        //    {
        //        Destroy(this.gameObject);
        //    };
        //}
        Invoke("SetOnceEffect", 0.3f);
    }
    void SetOnceEffect()
    {
        GameObject obj = NGUITools.AddChild(BlueSun, onceEffect) as GameObject;
        obj.transform.localScale = new Vector3(300, 300, 300);
        obj.transform.localPosition = new Vector3(0, -40, 0);
    }
    public void SetInfo(BetterList<Item> ItemList, SureOnClick sureClick)
    {
        One.SetActive(true);
        Two.SetActive(true);
        Three.SetActive(true);
        Four.SetActive(true);
        Five.SetActive(true);

        _CurSureOnClick = sureClick;
        number = ItemList.size;
        //Debug.LogError("asdasdasdasdasd" + ItemList[0].itemCode);
        if (ItemList.size == 1)
        {
            SetColor(One.transform.FindChild("Item").gameObject, ItemList[0].itemGrade);
            SetIcon(One.transform.FindChild("Item").gameObject, ItemList[0]);
        }
        else if (ItemList.size == 2)
        {
            SetColor(Two.transform.FindChild("Item1").gameObject, ItemList[0].itemGrade);
            SetIcon(Two.transform.FindChild("Item1").gameObject, ItemList[0]);

            SetColor(Two.transform.FindChild("Item2").gameObject, ItemList[1].itemGrade);
            SetIcon(Two.transform.FindChild("Item2").gameObject, ItemList[1]);
        }
        else if (ItemList.size == 3)
        {
            Debug.Log("AAAAAA");
            SetColor(Three.transform.FindChild("Item1").gameObject, ItemList[0].itemGrade);
            Debug.Log("AAAAAAbbb");
            SetIcon(Three.transform.FindChild("Item1").gameObject, ItemList[0]);
            Debug.Log("AAAAAAcc");
            SetColor(Three.transform.FindChild("Item2").gameObject, ItemList[1].itemGrade);
            Debug.Log("AAAAAAdd");
            SetIcon(Three.transform.FindChild("Item2").gameObject, ItemList[1]);
            Debug.Log("AAAAAAee");
            SetColor(Three.transform.FindChild("Item3").gameObject, ItemList[2].itemGrade);
            Debug.Log("AAAAAAff");
            SetIcon(Three.transform.FindChild("Item3").gameObject, ItemList[2]);
        }
        else if (ItemList.size == 4)
        {
            SetColor(Four.transform.FindChild("Item1").gameObject, ItemList[0].itemGrade);
            SetIcon(Four.transform.FindChild("Item1").gameObject, ItemList[0]);

            SetColor(Four.transform.FindChild("Item2").gameObject, ItemList[1].itemGrade);
            SetIcon(Four.transform.FindChild("Item2").gameObject, ItemList[1]);

            SetColor(Four.transform.FindChild("Item3").gameObject, ItemList[2].itemGrade);
            SetIcon(Four.transform.FindChild("Item3").gameObject, ItemList[2]);

            SetColor(Four.transform.FindChild("Item4").gameObject, ItemList[3].itemGrade);
            SetIcon(Four.transform.FindChild("Item4").gameObject, ItemList[3]);
        }
        else if (ItemList.size == 5)
        {
            SetColor(Five.transform.FindChild("Item1").gameObject, ItemList[0].itemGrade);
            SetIcon(Five.transform.FindChild("Item1").gameObject, ItemList[0]);

            SetColor(Five.transform.FindChild("Item2").gameObject, ItemList[1].itemGrade);
            SetIcon(Five.transform.FindChild("Item2").gameObject, ItemList[1]);

            SetColor(Five.transform.FindChild("Item3").gameObject, ItemList[2].itemGrade);
            SetIcon(Five.transform.FindChild("Item3").gameObject, ItemList[2]);

            SetColor(Five.transform.FindChild("Item4").gameObject, ItemList[3].itemGrade);
            SetIcon(Five.transform.FindChild("Item4").gameObject, ItemList[3]);

            SetColor(Five.transform.FindChild("Item5").gameObject, ItemList[4].itemGrade);
            SetIcon(Five.transform.FindChild("Item5").gameObject, ItemList[4]);
        }

        One.SetActive(false);
        Two.SetActive(false);
        Three.SetActive(false);
        Four.SetActive(false);
        Five.SetActive(false);

        StartCoroutine(SetEffect());
    }
    private void EscOnClick(GameObject obj)
    {
        if (_CurSureOnClick != null)
        {
            _CurSureOnClick();
        }

        Destroy(this.gameObject);

    }
    IEnumerator SetEffect()
    {
        yield return new WaitForSeconds(0.05f);
        FlyLight.SetActive(true);
        yield return new WaitForSeconds(0.6f);
        BG.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        HappyGet.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        BlueSun.SetActive(true);
        BGLight.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Label1.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        Line.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        switch (number)
        {
            case 1:
                One.SetActive(true);
                break;
            case 2:
                Two.SetActive(true);
                break;
            case 3:
                Three.SetActive(true);
                break;
            case 4:
                Four.SetActive(true);
                break;
            case 5:
                Five.SetActive(true);
                break;
            default:
                break;
        }
        yield return new WaitForSeconds(0.3f);
        Label2.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        SureButton.SetActive(true);

    }

    void SetIcon(GameObject go, Item item)
    {
        Debug.Log("DDDDDA1ddd1");
        go.transform.FindChild("SuiPian").gameObject.SetActive(false);
        go.SetActive(true);
        Debug.Log("DDDDDA11ssss");
        if (item.itemCode.ToString()[0] == '4')
        {

            Debug.Log("DDDDDAsss11");
            go.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            Debug.Log("DDDDDwwwwA11");
            go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = (item.itemCode - 10000).ToString();
            Debug.Log("DDDDDA11ssss");
            go.transform.FindChild("SuiPian").gameObject.SetActive(true);
            Debug.Log("DDDDDssssA11");
        }
        else if (item.itemCode.ToString()[0] == '7')
        {
            go.transform.FindChild("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
            go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = (item.itemCode - 10000).ToString();
            go.transform.FindChild("SuiPian").gameObject.SetActive(true);
        }
        else if (item.itemCode.ToString()[0] == '8')
        {
            go.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = (item.itemCode - 30000).ToString();
            go.transform.FindChild("SuiPian").gameObject.SetActive(true);
        }
        else if (item.itemCode.ToString()[0] == '6')
        {
            go.transform.FindChild("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
            go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = (item.itemCode - 10000).ToString();
        }
        else
        {
            Debug.Log("DDDDDA1w3331");
            go.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            Debug.Log("DDaaaaDDDA11");
            go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = (item.itemCode).ToString();
        }
        Debug.Log("DDDDDA11");
        go.transform.FindChild("Count").GetComponent<UILabel>().text = item.itemCount.ToString();
        Debug.Log("DDDDDA11ee");
        go.transform.FindChild("Name").GetComponent<UILabel>().text = item.itemName.ToString();

    }

    void SetColor(GameObject go, int color)
    {
        switch (color)
        {
            case 1:
                go.GetComponent<UISprite>().spriteName = "Grade1";
                break;
            case 2:
                go.GetComponent<UISprite>().spriteName = "Grade2";
                break;
            case 3:
                go.GetComponent<UISprite>().spriteName = "Grade3";
                break;
            case 4:
                go.GetComponent<UISprite>().spriteName = "Grade4";
                break;
            case 5:
                go.GetComponent<UISprite>().spriteName = "Grade5";
                break;
            case 6:
                go.GetComponent<UISprite>().spriteName = "Grade6";
                break;
            case 7:
                go.GetComponent<UISprite>().spriteName = "Grade7";
                break;
        }
    }
}
