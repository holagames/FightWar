using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LabSelectWindow: MonoBehaviour 
{
    public UIGrid myGrid;
    public UIScrollView myScrollView;
    public GameObject closeButton;
    public GameObject trainingSelectItem;
    public List<GameObject> ListTrainingSelectItem = new List<GameObject>();

	// Use this for initialization
	void Start () 
    {

        //SetEquipSelectWindow();

        SetLabSelectWindow();
        if (UIEventListener.Get(closeButton).onClick == null)
        {
            UIEventListener.Get(closeButton).onClick += delegate(GameObject go)
            {
                //UIManager.instance.BackUI();
                GameObject.Destroy(this.gameObject);
            };
        }

        
	}

    private void SetLabSelectWindow()
    {
        ListTrainingSelectItem.Clear();
        Debug.Log("上实验室的Hero个数..." + LabWindow.mOnLineTrainingHeroList.Count);
        
        switch(TextTranslator.instance.roleType)
        {
            case 1: 
            case 2: 
            case 3:
                int heroCarrerType = TextTranslator.instance.roleType;
                BetterList < Hero > list = CharacterRecorder.instance.ownedHeroList;
                list = labHeroSort(list);
                foreach (var _hero in list)
                {
                    HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(_hero.cardID);
                    if (!LabWindow.mOnLineTrainingHeroList.Contains(_hero) && hinfo.heroCarrerType == heroCarrerType)
                    {
                        GameObject go = NGUITools.AddChild(myGrid.gameObject, trainingSelectItem);
                        go.name = _hero.cardID.ToString();
                        go.GetComponent<LabSelectItem>().SetLabSelectItem(_hero);
                        go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("ScrollView").GetComponent<UIScrollView>();
                    }
                }
                break;
            case 4:
                BetterList < Hero > list1 = CharacterRecorder.instance.ownedHeroList;
                list1 = labHeroSort(list1);
                foreach (var _hero in list1)
                {
                    HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(_hero.cardID);
                    if (!LabWindow.mOnLineTrainingHeroList.Contains(_hero) && hinfo.heroArea == 1)
                    {
                        GameObject go = NGUITools.AddChild(myGrid.gameObject, trainingSelectItem);
                        go.name = _hero.cardID.ToString();
                        go.GetComponent<LabSelectItem>().SetLabSelectItem(_hero);
                        go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("ScrollView").GetComponent<UIScrollView>();
                    }
                }
                break;
            case 5:
                BetterList < Hero > list2 = CharacterRecorder.instance.ownedHeroList;
                list2 = labHeroSort(list2);
                foreach (var _hero in list2)
                {
                    HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(_hero.cardID);
                    if (!LabWindow.mOnLineTrainingHeroList.Contains(_hero) && hinfo.heroArea != 1)
                    {
                        GameObject go = NGUITools.AddChild(myGrid.gameObject, trainingSelectItem);
                        go.name = _hero.cardID.ToString();
                        go.GetComponent<LabSelectItem>().SetLabSelectItem(_hero);
                        go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("ScrollView").GetComponent<UIScrollView>();
                    }
                }
                break;
            case 6:
                int sex = 1;
                BetterList < Hero > list3 = CharacterRecorder.instance.ownedHeroList;
                list3 = labHeroSort(list3);
                foreach (var _hero in list3)
                {
                    HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(_hero.cardID);
                    if (!LabWindow.mOnLineTrainingHeroList.Contains(_hero) && sex == hinfo.sex)
                    {
                        GameObject go = NGUITools.AddChild(myGrid.gameObject, trainingSelectItem);
                        go.name = _hero.cardID.ToString();
                        go.GetComponent<LabSelectItem>().SetLabSelectItem(_hero);
                        go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("ScrollView").GetComponent<UIScrollView>();
                    }
                }
                break;
            case 7:
                sex = 2;
                BetterList < Hero > list4 = CharacterRecorder.instance.ownedHeroList;
                list4 = labHeroSort(list4);
                foreach (var _hero in list4)
                {
                    HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(_hero.cardID);
                    if (!LabWindow.mOnLineTrainingHeroList.Contains(_hero) && sex == hinfo.sex)
                    {
                        GameObject go = NGUITools.AddChild(myGrid.gameObject, trainingSelectItem);
                        go.name = _hero.cardID.ToString();
                        go.GetComponent<LabSelectItem>().SetLabSelectItem(_hero);
                        go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("ScrollView").GetComponent<UIScrollView>();
                    }
                }
                break;
        }
       /* foreach (var _hero in CharacterRecorder.instance.ownedHeroList)
        {
            HeroInfo hinfo = TextTranslator.instance.GetHeroInfoByHeroID(_hero.cardID);
            if (!LabWindow.mOnLineTrainingHeroList.Contains(_hero) && hinfo.heroCarrerType == heroCarrerType)
            {
                GameObject go = NGUITools.AddChild(myGrid.gameObject, trainingSelectItem);
                go.name = _hero.cardID.ToString();
                go.GetComponent<LabSelectItem>().SetLabSelectItem(_hero);
                go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("ScrollView").GetComponent<UIScrollView>();
            }
        }*/
        myGrid.GetComponent<UIGrid>().Reposition();
    }
    /// <summary>
    /// 可以改造的角色放前面
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    private BetterList<Hero> labHeroSort(BetterList<Hero> list)
    {
        BetterList<Hero> herolist = new BetterList<Hero>();
        herolist.Clear();
        foreach (var _hero in CharacterRecorder.instance.ownedHeroList)
        {
            if (_hero.level >= 30 && _hero.classNumber >= 5)
            {
                //可以改造
                herolist.Add(_hero);
            }
        }
        foreach (var _hero in CharacterRecorder.instance.ownedHeroList)
        {
            if (!herolist.Contains(_hero))
            {
                //可以改造
                herolist.Add(_hero);
            }
        }
        return herolist;
    }

  /*  private void SetEquipSelectWindow()
    {
        ListTrainingSelectItem.Clear();
        Debug.Log("上训练厂的Hero个数..." + LabWindow.mOnLineTrainingHeroList.Count);
        foreach (var _hero in CharacterRecorder.instance.ownedHeroList)
        {
            if (!LabWindow.mOnLineTrainingHeroList.Contains(_hero))
            {
                GameObject go = NGUITools.AddChild(myGrid.gameObject, trainingSelectItem);
                go.name = _hero.cardID.ToString();
                go.GetComponent<LegionTrainingSelectItem>().SetLegionTrainingSelectItem(_hero);
                go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("ScrollView").GetComponent<UIScrollView>();
            }
        }
        myGrid.GetComponent<UIGrid>().Reposition();
    }*/
   
}
