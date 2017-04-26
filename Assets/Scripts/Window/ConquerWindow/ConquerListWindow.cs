using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ConquerListWindow : MonoBehaviour
{

    public GameObject HeroItem;
    public UIGrid MyGrid;
    public GameObject RefButton;
    public List<GameObject> HeroList = new List<GameObject>();
    public GameObject CloseButton;
    public UIScrollView ScrollView;
    void Start()
    {
        gameObject.layer = 11;
        //if (GameObject.Find("CheckGateWindow") != null)
        //{
        //    gameObject.layer = 11;
        //    foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
        //    {
        //        child.gameObject.layer = 11;
        //    }
        //    gameObject.transform.FindChild("Mask").localPosition = new Vector3(0, 50, 0);
        //}
        //else
        //{
        //    gameObject.layer = 5;
        //    foreach (Transform child in gameObject.GetComponentsInChildren<Transform>())
        //    {
        //        child.gameObject.layer = 5;
        //    }
        //}
        UIEventListener.Get(RefButton).onClick += delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("6015#");
           //NetworkHandler.instance.SendProcess("6002#");
        };
        UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
        {
            gameObject.SetActive(false);
        };
    }

    public void SetInfo(List<PVPItemData> FriendItemData)
    {
        for (int i = 0; i < HeroList.Count; i++)
        {
            DestroyImmediate(HeroList[i]);
        }
        HeroList.Clear();
        int number = 0;
        List<PVPItemData> itemData = new List<PVPItemData>();
        if (FriendItemData.Count > 16)
        {
            number = 9;
        }
        for (int i = number; i < FriendItemData.Count; i++)
        {
            itemData.Add(FriendItemData[i]);
        }
        itemData.Sort(delegate(PVPItemData a, PVPItemData b) { return a.powerNum.CompareTo(b.powerNum); });
        for (int i = itemData.Count; i >0 ; i--)
        {

            if (itemData[i - 1].name != CharacterRecorder.instance.characterName)
            {
                GameObject go = Instantiate(HeroItem) as GameObject;
                go.transform.parent = MyGrid.transform;
                go.transform.localPosition = new Vector3(0, 0, 0);
                go.transform.localScale = new Vector3(1, 1, 1);
                go.SetActive(true);
                //(string Name,int HeroIcon,int Level,string Legion,int Power,int CapturedNumber)
                go.GetComponent<HeroItemInfo>().SetHeroInfo(itemData[i - 1].name, itemData[i - 1].roleID, itemData[i - 1].level, itemData[i - 1].LoginName,
                    itemData[i - 1].powerNum, itemData[i - 1].CapturedNumber, itemData[i - 1].playerID);
                go.GetComponent<UIDragScrollView>().scrollView = ScrollView;
                HeroList.Add(go);
            }

        }
        for (int i = 0; i < HeroList.Count; i++)
        {
            if (HeroList[i].GetComponent<HeroItemInfo>().isFirstTime == true)
            {
                DestroyImmediate(HeroList[i]);
            }
        }
        MyGrid.repositionNow = true;
    }

}
