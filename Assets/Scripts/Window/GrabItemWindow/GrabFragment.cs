using UnityEngine;
using System.Collections;

public class GrabFragment : MonoBehaviour {
    public GameObject NumberSprite;
    public GameObject ItemNumber;
    public GameObject Icon;
    public GameObject EquipIcon;
    public Item ItemInfo;
    public int Id;
    public UIAtlas GrabitemAtlas;
    public UIAtlas ItemAtlas;
    public bool Isbol = false;
    public GameObject Fragment;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    /// <summary>
    /// 更新碎片的显示
    /// </summary>
    public void UpdateItemShow(int id,int addid)
    {
        NumberSprite.SetActive(true);
        ItemNumber.SetActive(true);

        if (TextTranslator.instance.GetItemByID(id + addid) != null)
        {
            ItemInfo = TextTranslator.instance.GetItemByID(id + addid);
            if (ItemInfo.itemCount == 0)
            {
                Id = id + addid;
                EquipIcon.SetActive(false);
                Icon.SetActive(true);
                ItemNumber.GetComponent<UILabel>().text = "0";
                Icon.GetComponent<UISprite>().spriteName = (id*10+1).ToString();
                Isbol = false;
            }
            else
            {
                Id = ItemInfo.itemCode;
                ItemNumber.GetComponent<UILabel>().text = ItemInfo.itemCount.ToString();
                EquipIcon.SetActive(true);
                EquipIcon.GetComponent<UISprite>().spriteName = id.ToString();
                Icon.SetActive(false);
                Isbol = true;
            }
        }
        else
        {
            Id = id + addid;
            EquipIcon.SetActive(false);
            Icon.SetActive(true);
            ItemNumber.GetComponent<UILabel>().text = "0";
            Icon.GetComponent<UISprite>().spriteName = (id*10+1).ToString();
            Isbol = false;
        }
        Fragment.SetActive(true);
    }
}
