using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RankRuleWindow : MonoBehaviour
{
    public GameObject LabelAwardItem;
    public GameObject uiGrid;
    public UILabel RankNumber;
    //public UILabel powerNumber;
    // Use this for initialization
    void Start()
    {
        if (UIEventListener.Get(GameObject.Find("RuleCloseButton")).onClick == null)
        {
            Debug.Log("BBB");
            UIEventListener.Get(GameObject.Find("RuleCloseButton")).onClick += delegate(GameObject go)
            {
                Debug.Log("CCC");
                UIManager.instance.BackUI();
            };
        }
        PVPWindow _PVPWindow = GameObject.Find("PVPWindow").GetComponent<PVPWindow>();
        RankNumber.text = _PVPWindow.mRankNumber.text;
        //GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第" + ")可领取奖励";
        //powerNumber.text = _PVPWindow.mPowerNumber.text;
        SetAwardItem();
        MyAward();
    }
    void SetAwardItem() 
    {
        for (int i = 0; i < 23; i++) 
        {
            GameObject go = NGUITools.AddChild(uiGrid, LabelAwardItem);
            switch (i) 
            {
                case 0:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第4名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "85000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "800";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "95";
                    break;
                case 1:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第5名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "80000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "750";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "90";
                    break;
                case 2:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第6名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "75000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "720";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "85";
                    break;
                case 3:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第7名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "70000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "690";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "80";
                    break;
                case 4:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第8名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "65000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "660";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "75";
                    break;
                case 5:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第9名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "60000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "630";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "70";
                    break;
                case 6:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第10名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "55000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "600";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "65";
                    break;
                case 7:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第11~20名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "50000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "570";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "60";
                    break;
                case 8:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第21~50名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "47000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "540";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "55";
                    break;
                case 9:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第51~100名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "38000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "510";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "50";
                    break;
                case 10:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第101~200名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "35000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "480";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "45";
                    break;
                case 11:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第201~300名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "32000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "450";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "40";
                    break;
                case 12:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第301~400名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "29000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "420";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "35";
                    break;
                case 13:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第401~500名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "26000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "390";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "30";
                    break;
                case 14:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第501~600名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "23000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "360";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "25";
                    break;
                case 15:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第601~800名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "20000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "330";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "20";
                    break;
                case 16:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第801~1000名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "17000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "300";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "18";
                    break;
                case 17:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第1001~2000名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "15000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "270";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "16";
                    break;
                case 18:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第2001~3000名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "13000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "240";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "14";
                    break;
                case 19:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第3001~5000名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "12000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "210";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "12";
                    break;
                case 20:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第5001~10000名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "11000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "180";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "10";
                    break;
                case 21:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第10001~20000名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "10000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "150";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "8";
                    break;
                case 22:
                    go.name = (i + 4).ToString();
                    go.transform.GetComponent<UILabel>().text = "第20001~100000名";
                    go.transform.Find("SpriteMoney").Find("Label").GetComponent<UILabel>().text = "8000";
                    go.transform.Find("SpriteiArena").Find("Label").GetComponent<UILabel>().text = "120";
                    go.transform.Find("SpriteDiamond").Find("Label").GetComponent<UILabel>().text = "6";
                    break;
            }
            uiGrid.GetComponent<UIGrid>().Reposition();
        }       
    }
    void MyAward() 
    {
        int MyNum = int.Parse(RankNumber.text);
        switch (MyNum) 
        {
            case 1: GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "100000";
                GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "1200";
                GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "150";
                GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第1名)可领取奖励";
                break;
            case 2: GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "95000";
                GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "1050";
                GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "120";
                GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第2名)可领取奖励";
                break;
            case 3: GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "90000";
                GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "900";
                GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "100";
                GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第3名)可领取奖励";
                break;
            case 4: GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "85000";
                GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "800";
                GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "95";
                GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第4名)可领取奖励";
                break;
            case 5: GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "80000";
                GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "750";
                GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "90";
                GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第5名)可领取奖励";
                break;
            case 6: GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "75000";
                GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "720";
                GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "85";
                GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第6名)可领取奖励";
                break;
            case 7: GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "70000";
                GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "690";
                GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "80";
                GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第7名)可领取奖励";
                break;
            case 8: GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "65000";
                GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "660";
                GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "75";
                GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第8名)可领取奖励";
                break;
            case 9: GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "60000";
                GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "630";
                GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "70";
                GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第9名)可领取奖励";
                break;
            case 10: GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "55000";
                GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "600";
                GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "65";
                GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第10名)可领取奖励";
                break;
        }
        if (MyNum > 10 && MyNum <= 20) 
        {
            GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "50000";
            GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "570";
            GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "60";
            GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第11-20名)可领取奖励";
        }
        else if (MyNum > 20 && MyNum <= 50)
        {
            GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "47000";
            GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "540";
            GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "55";
            GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第21-50名)可领取奖励";
        }
        else if (MyNum > 50 && MyNum <= 100)
        {
            GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "38000";
            GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "510";
            GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "50";
            GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第51-100名)可领取奖励";
        }
        else if (MyNum > 100 && MyNum <= 200)
        {
            GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "35000";
            GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "480";
            GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "45";
            GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第101-200名)可领取奖励";
        }
        else if (MyNum > 200 && MyNum <= 300)
        {
            GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "32000";
            GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "450";
            GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "40";
            GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第201-300名)可领取奖励";
        }
        else if (MyNum > 300 && MyNum <= 400)
        {
            GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "29000";
            GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "420";
            GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "35";
            GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第301-400名)可领取奖励";
        }
        else if (MyNum > 400 && MyNum <= 500)
        {
            GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "26000";
            GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "390";
            GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "30";
            GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第401-500名)可领取奖励";
        }
        else if (MyNum > 500 && MyNum <= 600)
        {
            GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "23000";
            GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "360";
            GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "25";
            GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第501-600名)可领取奖励";
        }
        else if (MyNum > 600 && MyNum <= 800)
        {
            GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "20000";
            GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "330";
            GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "20";
            GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第601-800名)可领取奖励";
        }
        else if (MyNum > 800 && MyNum <= 1000)
        {
            GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "17000";
            GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "300";
            GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "18";
            GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第801-1000名)可领取奖励";
        }
        else if (MyNum > 1000 && MyNum <= 2000)
        {
            GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "15000";
            GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "270";
            GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "16";
            GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第1001-2000名)可领取奖励";
        }
        else if (MyNum > 2000 && MyNum <= 3000)
        {
            GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "13000";
            GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "240";
            GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "14";
            GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第2001-3000名)可领取奖励";
        }
        else if (MyNum > 3000 && MyNum <= 5000)
        {
            GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "12000";
            GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "210";
            GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "12";
            GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第3001-5000名)可领取奖励";
        }
        else if (MyNum > 5000 && MyNum <= 10000)
        {
            GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "11000";
            GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "180";
            GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "10";
            GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第5001-10000名)可领取奖励";
        }
        else if (MyNum > 10000 && MyNum <= 20000)
        {
            GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "10000";
            GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "150";
            GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "8";
            GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第10001-20000名)可领取奖励";
        }
        else if (MyNum > 20000 && MyNum <= 100000)
        {
            GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "8000";
            GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "120";
            GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "6";
            GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第20000-100000名)可领取奖励";
        }
        else if (MyNum > 100000)
        {
            GameObject.Find("All/TopContent/Award1/Label").GetComponent<UILabel>().text = "0";
            GameObject.Find("All/TopContent/Award2/Label").GetComponent<UILabel>().text = "0";
            GameObject.Find("All/TopContent/Award3/Label").GetComponent<UILabel>().text = "0";
            GameObject.Find("All/TopContent/Label2").GetComponent<UILabel>().text = "保持当前排名（第100000名后)可领取奖励";
        }
    }
}
