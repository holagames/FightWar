using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LegionTrainingSelectWindow: MonoBehaviour 
{
    public UIGrid myGrid;
    public UIScrollView myScrollView;
    public GameObject closeButton;
    public GameObject trainingSelectItem;
    public List<GameObject> ListTrainingSelectItem = new List<GameObject>();

	// Use this for initialization
	void Start () 
    {
        //EquipStrengerPart _EquipStrengerPart = GameObject.Find("EquipStrengerPart").GetComponent<EquipStrengerPart>();

        SetEquipSelectWindow();

        if (UIEventListener.Get(closeButton).onClick == null)
        {
            UIEventListener.Get(closeButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }

        
	}
    private void SetEquipSelectWindow()
    {
        ListTrainingSelectItem.Clear();
        Debug.Log("上训练厂的Hero个数..." + LegionTrainingGroundWindow.mOnLineTrainingHeroList.Count);
        foreach (var _hero in CharacterRecorder.instance.ownedHeroList)
        {
            if (!LegionTrainingGroundWindow.mOnLineTrainingHeroList.Contains(_hero))
            {
                GameObject go = NGUITools.AddChild(myGrid.gameObject, trainingSelectItem);
                go.name = _hero.cardID.ToString();
                go.GetComponent<LegionTrainingSelectItem>().SetLegionTrainingSelectItem(_hero);
                go.GetComponent<UIDragScrollView>().scrollView = GameObject.Find("ScrollView").GetComponent<UIScrollView>();
            }
        }
        myGrid.GetComponent<UIGrid>().Reposition();
    }
   
}
