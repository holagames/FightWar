using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class FriendInfoWindow : MonoBehaviour
{
    public GameObject HeroItem;
    public UIGrid MyGrid;
    public List<GameObject> HeroList = new List<GameObject>();
    public GameObject CloseButton;
    public UIScrollView ScrollView;
    void Start()
    {
        gameObject.layer = 11;
        UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
        {
            gameObject.SetActive(false);
        };
    }

    public void SetInfo(List<FriendItemData> FriendItem)
    {
        for (int i = 0; i < HeroList.Count; i++)
        {
            DestroyImmediate(HeroList[i]);
        }
        HeroList.Clear();
        int number = 0;
        List<FriendItemData> itemData = new List<FriendItemData>();
        if (FriendItem.Count > 16)
        {
            number = 9;
        }
        for (int i = number; i < FriendItem.Count; i++)
        {
            itemData.Add(FriendItem[i]);
        }
        itemData.Sort(delegate(FriendItemData a, FriendItemData b) { return a.fight.CompareTo(b.fight); });
        for (int i = itemData.Count; i >0; i--)
        {

            if (itemData[i - 1].name != CharacterRecorder.instance.characterName)
            {

                GameObject go = Instantiate(HeroItem) as GameObject;
                go.transform.parent = MyGrid.transform;
                go.transform.localPosition = new Vector3(0, 0, 0);
                go.transform.localScale = new Vector3(1, 1, 1);
                go.SetActive(true);
                //(string Name,int HeroIcon,int Level,string Legion,int Power,int CapturedNumber)
                go.GetComponent<HeroItemInfo>().SetHeroInfo(itemData[i - 1].name, int.Parse(itemData[i - 1].icon), itemData[i - 1].level, itemData[i - 1].LoginName,
                    itemData[i - 1].fight, itemData[i - 1].CapturedNumber, itemData[i - 1].userId); ;
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
