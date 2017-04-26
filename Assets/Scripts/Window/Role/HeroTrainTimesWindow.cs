using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroTrainTimesWindow : MonoBehaviour
{
    [SerializeField]
    private GameObject closeButton;
    [SerializeField]
    private List<GameObject> itemList;
    private int selectId;
    private Vip _myVipData;
    private List<bool> itemOpenState = new List<bool>();

    private int trainTimes = 0;
    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.GetInt("TrainTimes", 0) == 0)
        {
            trainTimes = PlayerPrefs.GetInt("TrainTimes", 0);
        }
        else
        {
            trainTimes = PlayerPrefs.GetInt("TrainTimes");
        }

        _myVipData = TextTranslator.instance.GetVipDicByID(CharacterRecorder.instance.Vip);
        for (int i = 0; i < itemList.Count; i++)
        {
            if (i <= _myVipData.RoleWashState)
            {

            }
            else
            {

            }
            switch (i)
            {
                case 0: itemList[i].name = "1"; break;
                case 1: itemList[i].name = "5"; break;
                case 2: itemList[i].name = "10"; break;
            }
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
        if (go != null)
        {
            int indexItem = 0;
            switch (go.name)
            {
                case "1": indexItem = 0; break;
                case "5": indexItem = 1; break;
                case "10": indexItem = 2; break;
            }
            if (indexItem <= _myVipData.RoleWashState)
            {
                //HeroTrainPart.trainTimes = int.Parse(go.name);
                PlayerPrefs.SetInt("TrainTimes", int.Parse(go.name));
                GameObject.Find("HeroTrainPart").GetComponent<HeroTrainPart>().ResetTrainTimes(int.Parse(go.name));
                UIManager.instance.BackUI();
            }
            else
            {
                switch (indexItem)
                {
                    case 1: UIManager.instance.OpenPromptWindow(string.Format("VIP达到{0}级开放", 4), 11, false, PromptWindow.PromptType.Hint, null, null); break;
                    case 2: UIManager.instance.OpenPromptWindow(string.Format("VIP达到{0}级开放", 5), 11, false, PromptWindow.PromptType.Hint, null, null); break;
                }
            }
        }
    }

}
