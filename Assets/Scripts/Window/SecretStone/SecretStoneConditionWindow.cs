using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SecretStoneConditionWindow: MonoBehaviour 
{
    [SerializeField]
    private GameObject closeButton;
    [SerializeField]
    private List<GameObject> itemList;
    private int selectId;
    //private Vip _myVipData;
    private List<bool> itemOpenState = new List<bool>();
	// Use this for initialization
	void Start () 
    {
        //_myVipData = TextTranslator.instance.GetVipDicByID(CharacterRecorder.instance.Vip);
        for (int i = 0; i < itemList.Count;i++ )
        {
           /* if (i <= _myVipData.RoleWashState)
            {

            }
            else
            {
 
            }*/
            itemList[i].name = (i + 1).ToString();
            UIEventListener.Get(itemList[i]).onClick = ClickItemButton;
        }
        if (UIEventListener.Get(closeButton).onClick == null)
        {
            UIEventListener.Get(closeButton).onClick = delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }
        
	}
    void ClickItemButton(GameObject go)
    {
        if(go != null)
        {
            /*  int indexItem = 0;
              switch (go.name)
              {
                  case "1": indexItem = 0; break;
                  case "5": indexItem = 1; break;
                  case "10":indexItem = 2; break;
              }
              if (indexItem <= _myVipData.RoleWashState)
              {
                  //HeroTrainPart.trainTimes = int.Parse(go.name);
                  GameObject.Find("HeroTrainPart").GetComponent<HeroTrainPart>().ResetTrainTimes(int.Parse(go.name));
                  UIManager.instance.BackUI();
              }
              else
              {
                  switch(indexItem)
                  {
                      case 1: UIManager.instance.OpenPromptWindow(string.Format("VIP达到{0}级开放", 4), 11, false, PromptWindow.PromptType.Hint, null, null); break;
                      case 2: UIManager.instance.OpenPromptWindow(string.Format("VIP达到{0}级开放", 6), 11, false, PromptWindow.PromptType.Hint, null, null); break;
                  }
              }*/
            GameObject.Find("StoneInfoPart").GetComponent<StoneInfoPart>().ResetAutoSelectCondition(int.Parse(go.name));
            UIManager.instance.BackUI();
        }
    }

}
