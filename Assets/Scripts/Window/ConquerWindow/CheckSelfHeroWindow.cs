using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CheckSelfHeroWindow : MonoBehaviour
{
    public UIGrid myGrid;
    public UIScrollView myScrollView;
    public GameObject closeButton;
    public GameObject HeroSelectItem;
    public List<GameObject> HeroSelectItemList = new List<GameObject>();

    // Use this for initialization
    void Start()
    {
        //EquipStrengerPart _EquipStrengerPart = GameObject.Find("EquipStrengerPart").GetComponent<EquipStrengerPart>();
        gameObject.layer = 11;
        //myScrollView.gameObject.layer = 11;
        SetEquipSelectWindow();

        if (UIEventListener.Get(closeButton).onClick == null)
        {
            UIEventListener.Get(closeButton).onClick += delegate(GameObject go)
            {
                DestroyImmediate(this.gameObject);
            };
        }
    }
    private void SetEquipSelectWindow()
    {
        HeroSelectItemList.Clear();
        foreach (var _hero in CharacterRecorder.instance.ownedHeroList)
        {
            GameObject go = NGUITools.AddChild(myGrid.gameObject, HeroSelectItem);
            go.name = _hero.cardID.ToString();
            go.GetComponent<HeroSelectItem>().SetHeroSelectItem(_hero, 1);
            go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("ScrollView").GetComponent<UIScrollView>();
            HeroSelectItemList.Add(go);
        }
        for (int i = 0; i < HeroSelectItemList.Count; i++)
        {
            if (HeroSelectItemList[i].GetComponent<HeroSelectItem>().isFirstTime == true)
            {
                DestroyImmediate(HeroSelectItemList[i]);
            }
        }
        myGrid.GetComponent<UIGrid>().Reposition();
    }
}
