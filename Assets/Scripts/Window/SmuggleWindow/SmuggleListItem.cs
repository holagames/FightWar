using UnityEngine;
using System.Collections;

public class SmuggleListItem : MonoBehaviour
{
    public GameObject ItemInfoWindow;
    public int HeroID;
    public int HaveNum;
    public string HeroName;
    public string CarID;
    public int AllTime;
    public void HeroItemInfo(int _HeroID, int _HaveNum, string _HeroName, string _CarID,int Time)
    {
        HeroID = _HeroID;
        gameObject.name = _HeroID.ToString();
        HaveNum = _HaveNum;
        HeroName = _HeroName;
        CarID = _CarID;
        AllTime = Time;
        UpdateTime(AllTime);
        UIEventListener.Get(gameObject).onClick += delegate(GameObject go)
        {
            if (CharacterRecorder.instance.characterName != _HeroName)
            {
                if (int.Parse(GameObject.Find("SmuggleWindow").GetComponent<SmuggleWindow>().SumHaveNumber.text) > 0)
                {
                    if (_HaveNum < 2)
                    {
                        ItemInfoWindow.SetActive(true);
                        ItemInfoWindow.GetComponent<SmuggleHeroInfo>().ItemInfo(_HeroID, _CarID, _HeroName);
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("战队被抢次数不足", PromptWindow.PromptType.Hint, null, null);
                    }
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("您抢劫次数不足", PromptWindow.PromptType.Hint, null, null);
                }
            }
            else
            {
                UIManager.instance.OpenPromptWindow("不能打劫自己", PromptWindow.PromptType.Hint, null, null);
            }
        };
    }
    public void EnemyObjClick(GameObject enemyItem)
    {
        UIEventListener.Get(enemyItem).onClick += delegate(GameObject go)
        {
            if (int.Parse(GameObject.Find("SmuggleWindow").GetComponent<SmuggleWindow>().SumHaveNumber.text) > 0)
            {
                if (HaveNum < 2)
                {
                    ItemInfoWindow.SetActive(true);
                    ItemInfoWindow.GetComponent<SmuggleHeroInfo>().ItemInfo(HeroID, CarID, HeroName);
                }
                else
                {
                    UIManager.instance.OpenPromptWindow("战队被抢次数不足", PromptWindow.PromptType.Hint, null, null);
                }
            }
            else
            {
                UIManager.instance.OpenPromptWindow("您抢劫次数不足", PromptWindow.PromptType.Hint, null, null);
            }
        };
    }

    void UpdateTime(int NowTime)
    {
        CancelInvoke("ShowTime");
        AllTime = NowTime;
        InvokeRepeating("ShowTime", 0, 1.0f);
    }

    void ShowTime()
    {
        if (AllTime >= 0)
        {
            string HourStr = (AllTime / 3600 % 24).ToString("00");
            string MiniStr = (AllTime % 3600 / 60).ToString("00");
            string SecondStr = (AllTime % 60).ToString("00");
            AllTime -= 1;
            gameObject.transform.localPosition += new Vector3(0.75f, 0, 0);
        }
        else
        {
            NetworkHandler.instance.SendProcess("6301#");
            NetworkHandler.instance.SendProcess("6302#");
            //DestroyImmediate(gameObject);
        }
    }
}
