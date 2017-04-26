using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResultPVPWindow : MonoBehaviour
{
    public GameObject GameWin;
    public GameObject SpriteWin;
    public GameObject WinGrid;
    public GameObject LabelWinName;
    public GameObject LabelWinNum;

    public GameObject SpriteLose;
    public GameObject LoseGrid;
    public GameObject LabelLoseName;
    public GameObject LabelLoseNum;

    //public GameObject SpriteLineBG;
    public GameObject SpriteVSBG;
    public GameObject ButtonLookAgain;
    public GameObject ButtonBack;
    public GameObject ButtonReplay;


    public GameObject GameLose;
    public GameObject ResultPVPItem;
    bool _IsWin = false;

    public void Init(bool IsWin)
    {
        _IsWin = IsWin;
        State1();
    }
    void State1()
    {
        if (_IsWin)
        {
            GameWin.SetActive(true);
            GameLose.SetActive(false);
            Invoke("State2", 0);
            if (CharacterRecorder.instance.RankNumber < CharacterRecorder.instance.PVPRankNumber)
            {
                CharacterRecorder.instance.PVPComeNum = 0;
            }
            else 
            {
                CharacterRecorder.instance.PVPComeNum = 2;
            }
        }
        else
        {
            GameWin.SetActive(false);
            GameLose.SetActive(true);
            Invoke("stateFail1", 1);
            CharacterRecorder.instance.PVPComeNum = 0;
            //if (CharacterRecorder.instance.RankNumber > CharacterRecorder.instance.PVPRankNumber) 
            //{
            //    CharacterRecorder.instance.PVPComeNum = 0;
            //}
            //else
            //{
            //    CharacterRecorder.instance.PVPComeNum = 2;
            //}
        }
    }
    void State2()
    {
        //foreach (var h in CharacterRecorder.instance.ownedHeroList)
        //{
        //    //WinGrid.GetComponent<UIGrid>().Reposition();
        //    GameObject go = NGUITools.AddChild(WinGrid, ResultPVPItem);
        //    go.name = "ReSultPVPItem" + h.characterRoleID.ToString();
        //    go.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
        //    go.GetComponent<ResultPVPItem>().Init(h.cardID, h.level);
        //    WinGrid.GetComponent<UIGrid>().Reposition();
        //}
        int MyBeforeRankNum = CharacterRecorder.instance.RankNumber;
        string[] heroSplit = PictureCreater.instance.PVPPosition.Split('!');
        for (int i = 0; i < heroSplit.Length - 1; i++) 
        {
            string[] strSplit = heroSplit[i].Split('$');
            Hero h = CharacterRecorder.instance.GetHeroByCharacterRoleID(int.Parse(strSplit[0]));
            GameObject go = NGUITools.AddChild(WinGrid, ResultPVPItem);
            go.name = "ReSultPVPItem" + h.characterRoleID.ToString();
            go.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            go.GetComponent<ResultPVPItem>().Init(h.cardID, h.level);
            WinGrid.GetComponent<UIGrid>().Reposition();
        }
            LabelWinName.GetComponent<UILabel>().text = CharacterRecorder.instance.characterName;
        //LabelWinNum.GetComponent<UILabel>().text = CharacterRecorder.instance.RankNumber.ToString();

        string Selipt = PictureCreater.instance.PVPString;
        string[] secSplit = Selipt.Split(';');
        for (int i = 0; i < secSplit.Length-1; i++) 
        {
            string[] IdSplit = secSplit[i].Split('$');
            GameObject go = NGUITools.AddChild(LoseGrid, ResultPVPItem);
            go.name = "ReSultPVPItem" + IdSplit[0].ToString();
            go.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            go.GetComponent<ResultPVPItem>().Init(int.Parse(IdSplit[0]), int.Parse(IdSplit[11]));
            LoseGrid.GetComponent<UIGrid>().Reposition();
        }
        LabelLoseName.GetComponent<UILabel>().text = PictureCreater.instance.PVPName;
        if (CharacterRecorder.instance.RankNumber - CharacterRecorder.instance.PVPRankNumber <= 0)
        {
            LabelWinNum.GetComponent<UILabel>().text = CharacterRecorder.instance.RankNumber.ToString();
            LabelLoseNum.GetComponent<UILabel>().text = CharacterRecorder.instance.PVPRankNumber.ToString();
        }
        else 
        {
            LabelWinNum.GetComponent<UILabel>().text = CharacterRecorder.instance.PVPRankNumber.ToString();
            LabelLoseNum.GetComponent<UILabel>().text = CharacterRecorder.instance.RankNumber.ToString();
            CharacterRecorder.instance.RankNumber = CharacterRecorder.instance.PVPRankNumber;           
        }
        //LabelLoseNum.GetComponent<UILabel>().text = "0";
        
        if (LabelWinNum.GetComponent<UILabel>().text == "1" && MyBeforeRankNum != 1)
        {
            NetworkHandler.instance.SendProcess(string.Format("7002#{0};{1};{2};{3}", 5, CharacterRecorder.instance.characterName, 0, 0));
        }
        else if (int.Parse(LabelWinNum.GetComponent<UILabel>().text) <= 100 && MyBeforeRankNum != int.Parse(LabelWinNum.GetComponent<UILabel>().text))
        {
            NetworkHandler.instance.SendProcess(string.Format("7002#{0};{1};{2};{3}", 20, CharacterRecorder.instance.characterName, LabelLoseName.GetComponent<UILabel>().text, LabelWinNum.GetComponent<UILabel>().text));
        }
        Invoke("State3", 0.5f);
    }
    void State3() 
    {
        //SpriteLineBG.SetActive(true);
        SpriteVSBG.SetActive(true);
        Invoke("State4", 0.5f);

    }
    void State4() 
    {
        //ButtonLookAgain.SetActive(true);
        ButtonBack.SetActive(true);
        ButtonBack.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        ButtonReplay.SetActive(true);
        ButtonReplay.GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        if (GameObject.Find("GameWin") != null)
        {
            /*if (UIEventListener.Get(GameObject.Find("GameWin/ButtonLookAgain")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("GameWin/ButtonLookAgain")).onClick += delegate(GameObject go)
                {
                    //UIManager.instance.DestroyAllPanel();
                };
            }*/
            if (UIEventListener.Get(GameObject.Find("GameWin/ButtonBack")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("GameWin/ButtonBack")).onClick += delegate(GameObject go)
                {
                    UIManager.instance.BackTwoUI("PVPWindow");
                    PictureCreater.instance.StopFight(true);
                   // UIManager.instance.OpenPanel("PVPWindow", true);                    
                };
            }

            if (UIEventListener.Get(GameObject.Find("GameWin/ButtonReplay")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("GameWin/ButtonReplay")).onClick += delegate(GameObject go)
                {                    
                    PictureCreater.instance.IsRemember = false;
                    PictureCreater.instance.IsSkip = false;
                    PictureCreater.instance.ActObject.SetActive(false);
                    PictureCreater.instance.RememberCount = 0;
                    UIManager.instance.DestroyPanel("ResultPVPWindow");
                    UIManager.instance.DestroyPanel("FightWindow");
                    UIManager.instance.PopTowUI();
                    PictureCreater.instance.StartPVP(PictureCreater.instance.PVPString);                    
                };
            }
        }
    }
    void stateFail1() 
    {
        if (UIEventListener.Get(GameObject.Find("GameLose/ButtonZhaomu")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("GameLose/ButtonZhaomu")).onClick += delegate(GameObject go)
            {
                ////UIManager.instance.DestroyAllPanel();
                //PictureCreater.instance.StopFight(true);
                //UIManager.instance.OpenPanel("GachaWindow", false);
                PictureCreater.instance.StopFight(true);
                UIManager.instance.OpenPanel("MainWindow", true);
                CharacterRecorder.instance.enterMapFromMain = false;
                UIManager.instance.OpenPanel("GachaWindow", true);
            };
        }
        //if (UIEventListener.Get(GameObject.Find("GameLose/ButtonShenji")).onClick == null)
        //{
        //    UIEventListener.Get(GameObject.Find("GameLose/ButtonShenji")).onClick += delegate(GameObject go)
        //    {
        //        //UIManager.instance.DestroyAllPanel();
        //        //PictureCreater.instance.StopFight(true);
        //        //UIManager.instance.OpenPanel("RoleWindow", false);
        //        PictureCreater.instance.StopFight(true);
        //        UIManager.instance.OpenPanel("MainWindow", true);
                
        //        CharacterRecorder.instance.enterMapFromMain = false;
        //        UIManager.instance.OpenPanel("RoleWindow", true);
        //        GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(2);
        //    };
        //}

        if (UIEventListener.Get(GameObject.Find("GameLose/ButtonZhuangbei")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("GameLose/ButtonZhuangbei")).onClick += delegate(GameObject go)
            {
                PictureCreater.instance.StopFight(false);
                UIManager.instance.OpenPanel("MainWindow", true);

                CharacterRecorder.instance.enterMapFromMain = false;
                GameObject rw = UIManager.instance.OpenPanel("StrengEquipWindow", true);
                DestroyImmediate(GameObject.Find("FightScene"));
            };
        }
        if (UIEventListener.Get(GameObject.Find("GameLose/ButtonShenping")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("GameLose/ButtonShenping")).onClick += delegate(GameObject go)
            {
                //UIManager.instance.DestroyAllPanel();
                //PictureCreater.instance.StopFight(true);
                //UIManager.instance.OpenPanel("RoleWindow", false);
                PictureCreater.instance.StopFight(true);
                UIManager.instance.OpenPanel("MainWindow", true);

                CharacterRecorder.instance.enterMapFromMain = false;
                UIManager.instance.OpenPanel("RoleWindow", true);
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(4);
            };
        }
        if (UIEventListener.Get(GameObject.Find("GameLose/ButtonZhanshu")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("GameLose/ButtonZhanshu")).onClick += delegate(GameObject go)
            {
                //UIManager.instance.DestroyAllPanel();
                //PictureCreater.instance.StopFight(true);
                //UIManager.instance.OpenPanel("RoleWindow", false);
                PictureCreater.instance.StopFight(true);
                UIManager.instance.OpenPanel("MainWindow", true);

                CharacterRecorder.instance.enterMapFromMain = false;
                UIManager.instance.OpenPanel("TaskWindow", true);
            };
        }
        if (UIEventListener.Get(GameObject.Find("GameLose/ButtonBackPVP")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("GameLose/ButtonBackPVP")).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackTwoUI("PVPWindow");
                PictureCreater.instance.StopFight(true);
              //  UIManager.instance.OpenPanel("PVPWindow", true);
            };
        }

        if (UIEventListener.Get(GameObject.Find("GameLose/ButtonReplayPVP")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("GameLose/ButtonReplayPVP")).onClick += delegate(GameObject go)
            {
                PictureCreater.instance.IsRemember = false;
                PictureCreater.instance.IsSkip = false;
                PictureCreater.instance.ActObject.SetActive(false);
                PictureCreater.instance.RememberCount = 0;
                UIManager.instance.DestroyPanel("ResultPVPWindow");
                UIManager.instance.DestroyPanel("FightWindow");
                UIManager.instance.PopTowUI();
                PictureCreater.instance.StartPVP(PictureCreater.instance.PVPString);
            };
        }
    }
}
