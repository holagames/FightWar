using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class HeroItemInfo : MonoBehaviour
{

    public UILabel PlayerName;
    public UILabel PlayerLevel;
    public UILabel PlayerPower;
    public UILabel PlayerLegion;
    public UILabel PlayerAddBuff;
    public UILabel PlayerCapturedNumber;
    public UISprite PlayerIcon;
    public UISprite PlayerGrade;
    public GameObject StartButton;
    public GameObject GetButton;
    public UILabel ButtonLabel;
    public HarvestWindow HarvestWindowInfo;
    public bool isEasyHero = false;
    public bool isFirstTime = false;
    public int PlayerUid;
    public UISprite Line;
    public int HeroPower=0;
    // Use this for initialization
    void Start()
    {
        UIEventListener.Get(StartButton).onClick += delegate(GameObject go)
        {
            if (isFirstTime)
            {
                UIManager.instance.OpenPromptWindow("已经上阵", PromptWindow.PromptType.Hint, null, null);
            }
            else
            {
                if (HarvestWindowInfo.KengID == 11 || HarvestWindowInfo.KengID == 12)
                {
                    CharacterRecorder.instance.KengID = HarvestWindowInfo.KengID;
                    NetworkHandler.instance.SendProcess("6504#" + CharacterRecorder.instance.TabeID + ";" + HarvestWindowInfo.KengID + ";" + PlayerUid + ";");
                    HarvestWindowInfo.FriendInfoWindow.SetActive(false);
                }
                else
                {
                    if (isEasyHero)
                    {
                        CharacterRecorder.instance.KengID = HarvestWindowInfo.KengID;
                        NetworkHandler.instance.SendProcess("6504#" + CharacterRecorder.instance.TabeID + ";" + HarvestWindowInfo.KengID + ";" + PlayerUid + ";");
                    }
                    else
                    {
                        //NetworkHandler.instance.SendProcess("6504#" + CharacterRecorder.instance.TabeID + ";" + HarvestWindowInfo.KengID + ";" + PlayerUid + ";");
                        Debug.LogError("战斗");
                        CharacterRecorder.instance.KengID = HarvestWindowInfo.KengID;
                        CharacterRecorder.instance.PlayerUid = PlayerUid;

                        PictureCreater.instance.FightStyle = 15;
                        NetworkHandler.instance.SendProcess("6003#" + PlayerUid + ";");
                    }
                    HarvestWindowInfo.ConquerListWindow.SetActive(false);
                }
            }   
        };
        UIEventListener.Get(GetButton).onClick += delegate(GameObject go)
       {
           CharacterRecorder.instance.KengID = HarvestWindowInfo.KengID;
           NetworkHandler.instance.SendProcess("6504#" + CharacterRecorder.instance.TabeID + ";" + HarvestWindowInfo.KengID + ";" + PlayerUid + ";");
           HarvestWindowInfo.ConquerListWindow.SetActive(false);
       };
    }

    public void SetHeroInfo(string Name, int HeroIcon, int Level, string Legion, int Power, int CapturedNumber, int UID)
    {
        HeroPower = Power;
        gameObject.name = Power.ToString();
        StartButton.SetActive(true);
        GetButton.SetActive(false);
        PlayerUid = UID;
        List<int> HeroId = GameObject.Find("ConquerWindow").GetComponent<ConquerWindow>().CaptureList;
        gameObject.GetComponent<UISprite>().spriteName = "zhengfu_guangkuang4";
        if (HarvestWindowInfo.KengID == 11 || HarvestWindowInfo.KengID == 12)
        {
            Line.spriteName = "zhengfu_guangkuang2";
            ButtonLabel.text = "邀 请";
            PlayerCapturedNumber.transform.Find("Label").GetComponent<UILabel>().text = "被邀请:";
        }
        else
        {
            Line.spriteName = "zhengfu_guangkuang1";
            ButtonLabel.text = "挑 战";
            PlayerCapturedNumber.transform.Find("Label").GetComponent<UILabel>().text = "被征服:";
        }
        foreach (int id in HeroId)
        {
            if (UID == id)
            {
                Line.spriteName = "zhengfu_guangkuang2";
                //ButtonLabel.text = "已上阵";
                isFirstTime = true;
            }
        }
        if (isFirstTime == false && HarvestWindowInfo.KengID != 11 && HarvestWindowInfo.KengID != 12)
        {
            if (CharacterRecorder.instance.Fight > Power)
            {
                Line.spriteName = "zhengfu_guangkuang2";
                gameObject.GetComponent<UISprite>().spriteName = "zhengfu_guangkuang3";
                StartButton.SetActive(false);
                GetButton.SetActive(true);
                isEasyHero = true;
            }
        }
        PlayerName.text = Name;
        PlayerLevel.text = "LV." + Level.ToString();
        PlayerPower.text = Power.ToString();
        PlayerLegion.text = Legion;
        PlayerAddBuff.text = (10 + 2 * (Power / 50000)) + "%";
        PlayerCapturedNumber.text = CapturedNumber.ToString() + "次";
        PlayerIcon.spriteName = HeroIcon.ToString();
        PlayerGrade.spriteName = "yxdi" + (TextTranslator.instance.GetItemByItemCode(HeroIcon).itemGrade-1).ToString();
    }
}
