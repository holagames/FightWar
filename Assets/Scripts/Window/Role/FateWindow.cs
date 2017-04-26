using UnityEngine;
using System.Collections;

public class FateWindow: MonoBehaviour 
{
    [SerializeField]
    private GameObject closeButton;
    [SerializeField]
    private GameObject fateItem;
    [SerializeField]
    private GameObject uiGride;
	// Use this for initialization
	void Start () 
    {
        if (UIEventListener.Get(closeButton).onClick == null)
        {
            UIEventListener.Get(closeButton).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }
	}
	
	// Update is called once per frame
	void Update () 
    {
	
	}
    public void SetFateWindow(Hero _mHero, BetterList<RoleFate> _MyRoleFateList)
    {
        Debug.Log("_MyRoleFateList.." + _MyRoleFateList.size);
        for (int i = 0; i < _MyRoleFateList.size;i++ )
        {
            GameObject obj = NGUITools.AddChild(uiGride, fateItem) as GameObject;
            obj.GetComponent<FateItem>().SetFateItem(_mHero.cardID,_MyRoleFateList[i]);
        }
        uiGride.GetComponent<UIGrid>().repositionNow = true;
    }
    
}
