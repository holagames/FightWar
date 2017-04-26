using UnityEngine;
using System.Collections;

public class RoleWearItem : MonoBehaviour {

    int ItemCode;
    int ItemCount;
    int ItemGrade;
    int ItemEquipID;
    int Index;
	// Use this for initialization
	void Start () {
        if (UIEventListener.Get(gameObject).onClick == null)
        {
            UIEventListener.Get(gameObject).onClick += delegate(GameObject go)
            {
                Debug.Log("BBB" + ItemCode);
                //GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetEquip(Index);
            };
        }
	}

    public void Init(int _ItemCode, int _ItemGrade, int _ItemCount, int _ItemEquipID, int _Index)
    {
        ItemCode = _ItemCode;
        ItemCount = _ItemCount;
        ItemGrade = _ItemGrade;
        ItemEquipID = _ItemEquipID;
        Index = _Index;
        gameObject.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = ItemCode.ToString();
        gameObject.GetComponent<UISprite>().spriteName = "Grade" + _ItemGrade.ToString();
        gameObject.transform.Find("LabelCount").gameObject.GetComponent<UILabel>().text = _ItemCount.ToString();
    }
}
