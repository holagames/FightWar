using UnityEngine;
using System.Collections;

public class TopContent : MonoBehaviour
{

    public GameObject BackButton;

    public UILabel LabelMoney;
    public UILabel LabelDiamond;
    public UILabel LabelStamina;
    public UILabel LabelBattle;

    public UILabel LabelTrial;
    public bool isWoods = false;
    IEnumerator WaitForSencondNew()//返回上一层界面
    {
        yield return new WaitForSeconds(0.01f);
        PictureCreater.instance.DestroyAllComponent();
        TextTranslator.instance.GUIReloadCount++;

        if (GameObject.Find("WayWindow") != null)
        {
            UIManager.instance.BackUI();
        }
        else if (GameObject.Find("VIPShopWindow") != null)
        {
            if (CharacterRecorder.instance.isWoods)
            {
                NetworkHandler.instance.SendProcess("1501#");
                CharacterRecorder.instance.isWoods = false;
            }
            else if (GameObject.Find("GaChaGetWindow") != null)
            {
                //UIManager.instance.BackUI();
                DestroyImmediate(GameObject.Find("VIPShopWindow"));
            }
            else
            {
                UIManager.instance.BackUI();
            }
        }
        else if (GameObject.Find("HeroMapWindow") != null)
        {
            UIManager.instance.BackUI();
        }
        else if (GameObject.Find("ChallengeWindow") != null)
        {
            UIManager.instance.OpenPanel("MainWindow", true);
        }
        else if (GameObject.Find("ConquerWindow") != null && GameObject.Find("HarvestWindow") != null)
        {
            CharacterRecorder.instance.TabeID = 0;
            NetworkHandler.instance.SendProcess("6501#");
            //UIManager.instance.BackUI();
           // UIManager.instance.OpenPanel("MainWindow", true);
        }
        else if (GameObject.Find("ConquerWindow") != null)
        {
            UIManager.instance.OpenPanel("ChallengeWindow", true);
        }
        //else if (GameObject.Find("ConquerWindow") != null && GameObject.Find("CheckGateWindow") != null)
        //{
        //     UIManager.instance.OpenPanel("MainWindow", true);
        //}
        else if (CharacterRecorder.instance.isToQualify)
        {
            CharacterRecorder.instance.isToQualify = false;
            UIManager.instance.OpenPanel("MainWindow", true);
        }
        else if (GameObject.Find("RoleWindow") != null)
        {
            bool IsNeedShowTips = CheckToShowTipsOfLeavingSkill();
            if (IsNeedShowTips == false)
            {
                if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 9 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 9)
                {

                }
                UIManager.instance.BackUI();
            }
        }
        //PVPWindow-->"竞技场"    GrabItemWindow-->"夺宝奇兵"    EverydayWindow-->"日常副本"
        else if (GameObject.Find("PVPWindow") != null || GameObject.Find("GrabItemWindow") != null || GameObject.Find("EverydayWindow") != null)
        {
            if (GameObject.Find("PVPWindow") != null)
            {
                GameCenter.leavelName = "PVPWindow";
            }
            else if (GameObject.Find("GrabItemWindow") != null)
            {
                GameCenter.leavelName = "GrabItemWindow";
                CharacterRecorder.instance.isFailed = false;//夺宝返回后清除
            }
            else if (GameObject.Find("EverydayWindow") != null)
            {
                GameCenter.leavelName = "EverydayWindow";
            }
            CharacterRecorder.instance.PVPComeNum = 0;
            CharacterRecorder.instance.OnceSuceessID = 0;
            UIManager.instance.BackUI();
        }
        else if (GameObject.Find("GetRankingReward") != null || GameObject.Find("IntegrationWindow") != null)
        {
            UIManager.instance.BackUI();
        }
        //WoodsTheExpendablesobList --> "丛林冒险"
        else if (GameObject.Find("WoodsTheExpendablesMapList") != null)
        {
            
            if (GameObject.Find("RankShopWindow") != null)
            {
                //Debug.LogError("WoodsTheExpendables++++++++++++++++++");
                UIManager.instance.BackUI();
                NetworkHandler.instance.SendProcess("1501#");
            }
            else if (CharacterRecorder.instance.isTaskGoto)
            {
                CharacterRecorder.instance.isTaskGoto = false;
                UIManager.instance.BackUI();
            }
            else
            {
                //Debug.LogError("WoodsTheExpendables=================");
                GameCenter.leavelName = "WoodsTheExpendables";
                UIManager.instance.OpenPanel("ChallengeWindow", true);
            }
        }
        else if (GameObject.Find("TaskWindow") != null)
        {
            UIManager.instance.BackUI();
        }
        else if (GameObject.Find("BagWindow") != null)
        {
            if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 15 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 11)
            {

            }
            UIManager.instance.OpenPanel("MainWindow", true);
        }
        else if (GameObject.Find("GaChaGetWindow") != null)
        {
            if (GameObject.Find("GachaWindow") != null)
            {
                UIManager.instance.BackUI();
                NetworkHandler.instance.SendProcess("5204#;");
            }
        }
            // 
        else if (GameObject.Find("TeamInvitationWindow") != null)
        {
            GameObject.Find("TeamInvitationWindow").GetComponent<TeamInvitationWindow>().HintWindow.SetActive(true);
            GameObject.Find("TeamInvitationWindow").GetComponent<TeamInvitationWindow>().HintWindow.GetComponent<HintWindow>().SetHintWindow(CharacterRecorder.instance.TeamID, CharacterRecorder.instance.TeamPosition);

        }
        else if (GameObject.Find("TeamBrowseWindow") != null)
        {
            UIManager.instance.OpenSinglePanel("TeamCopyWindow", true);
        }
        //TeamCopyWindow --> "组队副本"
        else if (GameObject.Find("TeamCopyWindow") != null)
        {
            //if (UIManager.instance.TheWindowIsStay("ChallengeWindow"))
            //{
            //    GameCenter.leavelName = "TeamCopyWindow";
            //    UIManager.instance.BackUI();
            //}
            //else 
            //{
            //    UIManager.instance.OpenPanel("MainWindow", true);
            //}
            UIManager.instance.OpenPanel("ChallengeWindow", true);
        }
        else if (GameObject.Find("WorldBossFightWindow") != null)
        {
            NetworkHandler.instance.SendProcess("6211#" + CharacterRecorder.instance.characterName + ";" + CharacterRecorder.instance.headIcon + ";" + CharacterRecorder.instance.MyLocationInWorldBoss + ";");
            GameObject.Find("WorldBossFightWindow").GetComponent<WorldBossFightWindow>().LiveWorldBossFightWindow();
            //NetworkHandler.instance.SendProcess("6211#" + CharacterRecorder.instance.characterName + ";" + CharacterRecorder.instance.headIcon + ";");
        }
        else if (GameObject.Find("LegionMainHaveWindow") != null)
        {
            GameObject.Find("LegionMainHaveWindow").GetComponent<LegionMainHaveWindow>().SetRedMain();
            UIManager.instance.BackUI();
        }
        else
        {
            Debug.LogError("其他UI层=====================");
            if (CharacterRecorder.instance.IsOnputOrRemove)
            {
                NetworkHandler.instance.SendProcess("1005#0;");
                CharacterRecorder.instance.IsOnputOrRemove = false;
            }
            if (GameObject.Find("SmuggleWindow") != null)
            {
                GameCenter.leavelName = "SmuggleWindow";
            }
            else if (GameObject.Find("KingRoadWindow") != null)
            {
                GameCenter.leavelName = "KingRoadWindow";
            }
            UIManager.instance.BackUI();
        }

        if (TextTranslator.instance.isUpdateBag)
        {
            NetworkHandler.instance.SendProcess("5001#;");
        }

        CharacterRecorder.instance.GrabIntegrationOpen = false;//夺宝积分界面是否打卡
        CharacterRecorder.instance.SweptIconID = 0;//此处为装备强化材料扫荡返回清除
        CharacterRecorder.instance.SweptIconNum = 0;
    }
    void Start()
    {
        //this.gameObject.transform.localPosition = new Vector3(-24.5f, 5f, 0);
        if (GameObject.Find("MainWindow") != null && GameObject.Find("VIPShopWindow") == null)
        {
            BackButton.SetActive(false);
        }

        UIEventListener.Get(BackButton).onClick += delegate(GameObject go)
        {
            Time.timeScale = 1;
            if ((PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) != 2 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) != 6) ||
                (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) != 4 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) != 6))
            {
                if ((PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 2 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 8) ||
                    (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 4 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 8) ||
                    (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 9 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 7) ||
                   (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 15 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 8))
                {
                    SceneTransformer.instance.NewGuideButtonClick();
                }
                if (CharacterRecorder.instance.GuideID[26] == 6 || CharacterRecorder.instance.GuideID[38] == 9 || CharacterRecorder.instance.GuideID[28] == 5)
                {
                    SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                }
                if (CharacterRecorder.instance.ChangeAttribute != false)
                {
                    NetworkHandler.instance.SendProcess("1005#0;");
                    CharacterRecorder.instance.ChangeAttribute = false;
                }
                if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) >= 18)
                {
                    if (CharacterRecorder.instance.GuideID[29] == 7)
                    {
                        CharacterRecorder.instance.GuideID[29] += 1;
                    }
                    else if (CharacterRecorder.instance.GuideID[10] == 16)
                    {
                        CharacterRecorder.instance.GuideID[10] += 1;
                    }
                }
                if (CharacterRecorder.instance.GuideID[29] != 6)
                {
                    if (GameObject.Find("RoleEquipInfoWindow") != null)
                    {
                        UIManager.instance.BackUI();
                    }
                    else
                    {
                        StartCoroutine(WaitForSencondNew());//新的 返回上一层界面
                    }
                }
            }
        };
       // if (UIEventListener.Get(GameObject.Find("MoneyButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("MoneyButton")).onClick += delegate(GameObject go)
            {
                if (CharacterRecorder.instance.level < 15)
                {
                    UIManager.instance.OpenPromptWindow("15级开启点金功能", PromptWindow.PromptType.Hint, null, null);
                    //加一个任务点金刷新红点
                }
                else if (GameObject.Find("TeamInvitationWindow")==null)
                {
                    UIManager.instance.OpenPanel("BuyGlodWindow", false);
                }
            };
        }

        //if (UIEventListener.Get(GameObject.Find("DiamondButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("DiamondButton")).onClick += delegate(GameObject go)
            {
                if (GameObject.Find("WoodsTheExpendablesMapList") != false)
                {
                    CharacterRecorder.instance.isWoods = true;
                }
                //if (GameObject.Find("RoleWindow") != null)
                //{
                //    PictureCreater.instance.DestroyAllComponent();
                //    bool IsNeedShowTips = CheckToShowTipsOfLeavingSkill();
                //    if (IsNeedShowTips == false)
                //    {
                //        UIManager.instance.OpenPanel("VIPShopWindow", true);
                //        //UIManager.instance.OpenSinglePanel("VIPShopWindow", false);
                //    }
                //}
                if (GameObject.Find("GaChaGetWindow") != null)
                {
                    //UIManager.instance.OpenPanel("VIPShopWindow", true);
                    UIManager.instance.OpenSinglePanel("VIPShopWindow", false);
                }
                else if (GameObject.Find("VIPShopWindow") == null && GameObject.Find("TeamInvitationWindow") == null)
                {
                    PictureCreater.instance.DestroyAllComponent();
                    UIManager.instance.OpenPanel("VIPShopWindow", true);
                }
            };
        }

        if (GameObject.Find("StaminaButton") != null)
        {
            if (UIEventListener.Get(GameObject.Find("StaminaButton")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("StaminaButton")).onClick += delegate(GameObject go)
                {
                    if (GameObject.Find("TeamInvitationWindow") == null) 
                    {
                        NetworkHandler.instance.SendProcess("5012#10602;");
                    }
                };
            }
        }

        if (GameObject.Find("BattleButton") != null)
        {
            if (UIEventListener.Get(GameObject.Find("BattleButton")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("BattleButton")).onClick += delegate(GameObject go)
                {

                    if (GameObject.Find("TeamInvitationWindow") == null) 
                    {
                        NetworkHandler.instance.SendProcess("5012#10702;");
                    }

                };
            }
        }

        if (GameObject.Find("WoodButton") != null)
        {
            if (UIEventListener.Get(GameObject.Find("WoodButton")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("WoodButton")).onClick += delegate(GameObject go)
                {
                    UIManager.instance.OpenPromptWindow("aaaaaaaaaaaa", PromptWindow.PromptType.Confirm, BackButtonClick, null);
                };
            }
        }
        if (GameObject.Find("TrialButton") != null)
        {
            if (UIEventListener.Get(GameObject.Find("TrialButton")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("TrialButton")).onClick += delegate(GameObject go)
                {
                    Debug.LogError("sssssssssssssss");
                };
            }
        }
        Reset();
    }

    public void Reset()
    {
        if (CharacterRecorder.instance.gold >= 10000)
        {
            int a = CharacterRecorder.instance.gold / 10000;
            int b = CharacterRecorder.instance.gold / 1000 % 10;
            LabelMoney.text = a.ToString() + "." + b.ToString() + "W";
        }
        else
        {
            LabelMoney.text = CharacterRecorder.instance.gold.ToString();
        }

        if (CharacterRecorder.instance.lunaGem >= 10000)
        {
            int a = CharacterRecorder.instance.lunaGem / 10000;
            int b = CharacterRecorder.instance.lunaGem / 1000 % 10;
            LabelDiamond.text = a.ToString() + "." + b.ToString() + "W";
        }
        else
        {
            LabelDiamond.text = CharacterRecorder.instance.lunaGem.ToString();
        }

        //LabelStamina.text = CharacterRecorder.instance.stamina.ToString() + "/" + CharacterRecorder.instance.staminaCap.ToString();
        //LabelBattle.text = CharacterRecorder.instance.sprite.ToString() + "/" + CharacterRecorder.instance.spriteCap.ToString();
        if (isWoods)
        {
            if (GameObject.Find("WoodsTheExpendablesMapList") != false)
            {
                GameObject.Find("WoodsTheExpendables").transform.Find("TopContent").gameObject.SetActive(true);
            }
        }
        else
        {
            if (GameObject.Find("WoodsTheExpendablesMapList") != null)
            {
                GameObject.Find("WoodsTheExpendables").transform.Find("TopContent").gameObject.SetActive(false);
            }
            if (GameObject.Find("WoodsTheExpendablesMapList") == null)//yy 12.9
            {
                if (LabelStamina != null)
                {
                    if (CharacterRecorder.instance.stamina > CharacterRecorder.instance.staminaCap)
                    {
                        LabelStamina.text = "[3ee817]" + CharacterRecorder.instance.stamina.ToString() + "[-]" + "/" + CharacterRecorder.instance.staminaCap.ToString();
                    }
                    else
                    {
                        LabelStamina.text = "[ffffff]" + CharacterRecorder.instance.stamina.ToString() + "[-]" + "/" + CharacterRecorder.instance.staminaCap.ToString();
                    }
                }

                if (LabelBattle != null)
                {
                    if (CharacterRecorder.instance.sprite > CharacterRecorder.instance.spriteCap)
                    {
                        LabelBattle.text = "[3ee817]" + CharacterRecorder.instance.sprite.ToString() + "[-]" + "/" + CharacterRecorder.instance.spriteCap.ToString();
                    }
                    else
                    {
                        LabelBattle.text = "[ffffff]" + CharacterRecorder.instance.sprite.ToString() + "[-]" + "/" + CharacterRecorder.instance.spriteCap.ToString();
                    }
                }
                //LabelStamina.text = CharacterRecorder.instance.stamina.ToString() + "/" + CharacterRecorder.instance.staminaCap.ToString();
                //LabelBattle.text = CharacterRecorder.instance.sprite.ToString() + "/" + CharacterRecorder.instance.spriteCap.ToString();
            }
        }
    }

    void BuyMoney()
    {
    }

    void BuyDiamond()
    {
    }

    void BuyStamina()
    {
    }

    void BuyBattle()
    {
        Debug.Log("BBBBB");
    }
    void BackButtonClick()
    {

    }

    #region 提示是否确认离开技能界面
    bool CheckToShowTipsOfLeavingSkill()
    {
        //Debug.LogError("提示是否确认离开技能界面");
        Hero curHero = CharacterRecorder.instance.ownedHeroList[TextTranslator.instance.HeadIndex];
        RoleDestiny Old_rd = TextTranslator.instance.GetRoleDestinyByID(curHero.cardID, curHero.skillLevel);
        float sliderValue = 1 - (float)curHero.skillNumber / (float)Old_rd.MaxExp;
        //Debug.Log("..sliderValue.." + sliderValue);
        if (CharacterRecorder.instance.RoleTabIndex == 6 && sliderValue < 1.0f)
        {
            //Debug.LogError("弹出提示");
            UIManager.instance.OpenPromptWindow("当前技能槽内还有经验，每天凌晨5点清空，建议完成升级，避免损失。", PromptWindow.PromptType.Confirm, CheckLeavingSkillUI, null);
            return true;
        }
        return false;
    }
    void CheckLeavingSkillUI()
    {
        if (GameObject.Find("RoleWindow") != null)
        {
            UIManager.instance.OpenPanel("MainWindow", true);
        }
    }
    #endregion
}
