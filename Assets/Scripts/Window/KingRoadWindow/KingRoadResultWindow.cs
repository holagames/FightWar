using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class KingRoadResultWindow : MonoBehaviour {

    public GameObject HeroItem;
    public GameObject leftPart;
    public GameObject leftUiGrid;
    public UILabel leftName;
    public UILabel rightName;

    public GameObject rightPart;
    public GameObject rightUiGrid;
    public GameObject SpriteAwardBg;
    public GameObject SpriteAward;
    public GameObject GridAward;
    public GameObject BackButton;

    private List<KingEnemyInfo> KingEnemyList = new List<KingEnemyInfo>();
	void Start () {
	    
	}

    public void SetFightResult(string Name, string ServerId, int CharacterId, int[] winarr, int[] myheroId, int[] enemyheroid, string enemyHeroinfo) 
    {
        int num = 0;
        for (int i = 0; i < 5; i++) 
        {
            num += winarr[i];
        }
        if (num >= 3)
        {
            SpriteAwardBg.SetActive(true);
            SpriteAward.SetActive(true);
            leftName.text = CharacterRecorder.instance.characterName;
            for (int i = 0; i < 5; i++)
            {
                GameObject go = NGUITools.AddChild(leftUiGrid, HeroItem);
                go.SetActive(true);
                go.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                go.name = myheroId[i].ToString();
                go.transform.Find("SpriteHead").GetComponent<UISprite>().spriteName = myheroId[i].ToString();
                go.transform.Find("SpriteHead").name = myheroId[i].ToString();

                for (int j = 0; j < CharacterRecorder.instance.ownedHeroList.size; j++)
                {
                    if (CharacterRecorder.instance.ownedHeroList[j].cardID == myheroId[i])
                    {
                        go.transform.Find("Level").GetComponent<UILabel>().text = CharacterRecorder.instance.ownedHeroList[j].level.ToString();
                        SetRankInfo(CharacterRecorder.instance.ownedHeroList[j].rank, go.transform.Find("JunXianSprite").GetComponent<UISprite>(), go.transform.Find("JunXianLv").GetComponent<UILabel>());
                        int addNum = TextTranslator.instance.SetHeroNameColor(go.GetComponent<UISprite>(), go.transform.Find("SpritePinJie").GetComponent<UISprite>(), CharacterRecorder.instance.ownedHeroList[j].classNumber);
                        for (int x = 0; x < addNum; x++)
                        {
                            GameObject obj = NGUITools.AddChild(go.transform.Find("Grid").gameObject, go.transform.Find("SpritePinJie").gameObject);
                            obj.SetActive(true);
                        }
                        UIGrid _UIGrid = go.transform.Find("Grid").GetComponent<UIGrid>();
                        _UIGrid.sorting = UIGrid.Sorting.Horizontal;
                        _UIGrid.pivot = UIWidget.Pivot.Center;
                        _UIGrid.animateSmoothly = true;
                        break;
                    }
                }
                if (winarr[i] == 1)
                {
                    go.transform.Find("WinSprite").gameObject.SetActive(true);
                }
            }

            leftUiGrid.GetComponent<UIGrid>().Reposition();


            rightName.text = Name;

            string[] dataSplit = enemyHeroinfo.Split('!');
            for (int i = 0; i < dataSplit.Length - 1; i++)
            {
                string[] trcSplit = dataSplit[i].Split('$');
                KingEnemyList.Add(new KingEnemyInfo(int.Parse(trcSplit[0]), int.Parse(trcSplit[1]), int.Parse(trcSplit[2]), int.Parse(trcSplit[3]), int.Parse(trcSplit[4])));
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (KingEnemyList[j].roleID == enemyheroid[i])
                    {
                        GameObject go = NGUITools.AddChild(rightUiGrid, HeroItem);
                        go.SetActive(true);
                        go.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                        go.name = KingEnemyList[j].roleID.ToString();
                        go.transform.Find("SpriteHead").GetComponent<UISprite>().spriteName = KingEnemyList[j].roleID.ToString();
                        go.transform.Find("SpriteHead").name = KingEnemyList[j].roleID.ToString();
                        go.transform.Find("Level").GetComponent<UILabel>().text = KingEnemyList[j].level.ToString();
                        SetRankInfo(KingEnemyList[j].rank, go.transform.Find("JunXianSprite").GetComponent<UISprite>(), go.transform.Find("JunXianLv").GetComponent<UILabel>());

                        int addNum = TextTranslator.instance.SetHeroNameColor(go.GetComponent<UISprite>(), go.transform.Find("SpritePinJie").GetComponent<UISprite>(), KingEnemyList[j].classNumber);
                        for (int x = 0; x < addNum; x++)
                        {
                            GameObject obj = NGUITools.AddChild(go.transform.Find("Grid").gameObject, go.transform.Find("SpritePinJie").gameObject);
                            obj.SetActive(true);
                        }
                        UIGrid _UIGrid = go.transform.Find("Grid").GetComponent<UIGrid>();
                        _UIGrid.sorting = UIGrid.Sorting.Horizontal;
                        _UIGrid.pivot = UIWidget.Pivot.Center;
                        _UIGrid.animateSmoothly = true;

                        if (winarr[i] == 0)
                        {
                            go.transform.Find("WinSprite").gameObject.SetActive(true);
                        }
                        break;
                    }
                }
            }
            rightUiGrid.GetComponent<UIGrid>().Reposition();

            UIEventListener.Get(BackButton).onClick = delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("6405#" + CharacterRecorder.instance.KingServer + ";" + CharacterRecorder.instance.KingEnemyID + ";" + 1 + ";");
            };
        }

        else 
        {
            SpriteAwardBg.SetActive(false);
            SpriteAward.SetActive(false);
            rightName.text = CharacterRecorder.instance.characterName;
            for (int i = 0; i < 5; i++)
            {
                GameObject go = NGUITools.AddChild(rightUiGrid, HeroItem);
                go.SetActive(true);
                go.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                go.name = myheroId[i].ToString();
                go.transform.Find("SpriteHead").GetComponent<UISprite>().spriteName = myheroId[i].ToString();
                go.transform.Find("SpriteHead").name = myheroId[i].ToString();

                for (int j = 0; j < CharacterRecorder.instance.ownedHeroList.size; j++)
                {
                    if (CharacterRecorder.instance.ownedHeroList[j].cardID == myheroId[i])
                    {
                        go.transform.Find("Level").GetComponent<UILabel>().text = CharacterRecorder.instance.ownedHeroList[j].level.ToString();
                        SetRankInfo(CharacterRecorder.instance.ownedHeroList[j].rank, go.transform.Find("JunXianSprite").GetComponent<UISprite>(), go.transform.Find("JunXianLv").GetComponent<UILabel>());
                        int addNum = TextTranslator.instance.SetHeroNameColor(go.GetComponent<UISprite>(), go.transform.Find("SpritePinJie").GetComponent<UISprite>(), CharacterRecorder.instance.ownedHeroList[j].classNumber);
                        for (int x = 0; x < addNum; x++)
                        {
                            GameObject obj = NGUITools.AddChild(go.transform.Find("Grid").gameObject, go.transform.Find("SpritePinJie").gameObject);
                            obj.SetActive(true);
                        }
                        UIGrid _UIGrid = go.transform.Find("Grid").GetComponent<UIGrid>();
                        _UIGrid.sorting = UIGrid.Sorting.Horizontal;
                        _UIGrid.pivot = UIWidget.Pivot.Center;
                        _UIGrid.animateSmoothly = true;
                        break;
                    }
                }
                if (winarr[i] == 1)
                {
                    go.transform.Find("WinSprite").gameObject.SetActive(true);
                }
            }

            rightUiGrid.GetComponent<UIGrid>().Reposition();


            leftName.text = Name;

            string[] dataSplit = enemyHeroinfo.Split('!');
            for (int i = 0; i < dataSplit.Length - 1; i++)
            {
                string[] trcSplit = dataSplit[i].Split('$');
                KingEnemyList.Add(new KingEnemyInfo(int.Parse(trcSplit[0]), int.Parse(trcSplit[1]), int.Parse(trcSplit[2]), int.Parse(trcSplit[3]), int.Parse(trcSplit[4])));
            }

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (KingEnemyList[j].roleID == enemyheroid[i])
                    {
                        GameObject go = NGUITools.AddChild(leftUiGrid, HeroItem);
                        go.SetActive(true);
                        go.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
                        go.name = KingEnemyList[j].roleID.ToString();
                        go.transform.Find("SpriteHead").GetComponent<UISprite>().spriteName = KingEnemyList[j].roleID.ToString();
                        go.transform.Find("SpriteHead").name = KingEnemyList[j].roleID.ToString();
                        go.transform.Find("Level").GetComponent<UILabel>().text = KingEnemyList[j].level.ToString();
                        SetRankInfo(KingEnemyList[j].rank, go.transform.Find("JunXianSprite").GetComponent<UISprite>(), go.transform.Find("JunXianLv").GetComponent<UILabel>());

                        int addNum = TextTranslator.instance.SetHeroNameColor(go.GetComponent<UISprite>(), go.transform.Find("SpritePinJie").GetComponent<UISprite>(), KingEnemyList[j].classNumber);
                        for (int x = 0; x < addNum; x++)
                        {
                            GameObject obj = NGUITools.AddChild(go.transform.Find("Grid").gameObject, go.transform.Find("SpritePinJie").gameObject);
                            obj.SetActive(true);
                        }
                        UIGrid _UIGrid = go.transform.Find("Grid").GetComponent<UIGrid>();
                        _UIGrid.sorting = UIGrid.Sorting.Horizontal;
                        _UIGrid.pivot = UIWidget.Pivot.Center;
                        _UIGrid.animateSmoothly = true;

                        if (winarr[i] == 0)
                        {
                            go.transform.Find("WinSprite").gameObject.SetActive(true);
                        }
                        break;
                    }
                }
            }
            leftUiGrid.GetComponent<UIGrid>().Reposition();

            UIEventListener.Get(BackButton).onClick = delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("6405#" + CharacterRecorder.instance.KingServer + ";" + CharacterRecorder.instance.KingEnemyID + ";" + 0 + ";");
            };
        }
    }

    //设置军衔

    void SetRankInfo(int _rank, UISprite _UISprite, UILabel _myUILabel)
    {
        _UISprite.spriteName = string.Format("rank{0}", _rank.ToString("00"));
        switch (_rank)
        {
            case 1:
                _myUILabel.text = "下士";
                break;
            case 2:
                _myUILabel.text = "中士";
                break;
            case 3:
                _myUILabel.text = "上士";
                break;
            case 4:
                _myUILabel.text = "少尉";
                break;
            case 5:
                _myUILabel.text = "中尉";
                break;
            case 6:
                _myUILabel.text = "上尉";
                break;
            case 7:
                _myUILabel.text = "少校";
                break;
            case 8:
                _myUILabel.text = "中校";
                break;
            case 9:
                _myUILabel.text = "上校";
                break;
            case 10:
                _myUILabel.text = "少将";
                break;
            case 11:
                _myUILabel.text = "中将";
                break;
            case 12:
                _myUILabel.text = "上将";
                break;
            default:
                break;
        }
    }
}
