using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeroMapItemInfoBoard : MonoBehaviour
{

    public UILabel IntrodunceLabel;
    public UILabel CareerLabel;
    public UILabel NameLabel;
    public UILabel SkillNameLabel;
    public UILabel SkillDesLabel1;
    public UILabel SkillDesLabel2;
    public UILabel SkillNameLabel2;
    public UILabel SkillDes2;
    public UILabel ADLabel;
    public UILabel ADDefenceLabel;
    public UILabel APLabel;
    public UILabel APDefenceLabel;
    public UILabel HPLabel;

    public GameObject DetailButton;
    public GameObject DetailBoard;
    public GameObject Hujia;
    public GameObject AiType;

    public GameObject scrollView;

    public GameObject HeroBoard;

    public UILabel[] talentName;
    public UILabel[] talentDes;

    HeroInfo _HeroInfo;
    Hero mHero;
    // Use this for initialization
    void Start()
    {
        if (UIEventListener.Get(GameObject.Find("Close")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("Close")).onClick += delegate(GameObject obj)
            {
                PictureCreater.instance.DestroyAllComponent();
                scrollView.transform.localPosition = new Vector3(-37, 0, 0);
                scrollView.GetComponent<UIPanel>().clipOffset = Vector2.zero;
                this.gameObject.SetActive(false);
            };
        }
    }

    public void SetInfo(int _RoleID)
    {
        PictureCreater.instance.DestroyAllComponent();
        int i = PictureCreater.instance.CreateRole(_RoleID, "Model", 18, Color.red, 0, 0, 1, 2f, false, false, 1, 1.5f, 0.2f, "Model", 0, 10, 10, 2, 2, 0, 0, 1001, 1, 1002, 1, 4, 1, 1, 0, "");
        PictureCreater.instance.ListRolePicture[0].RoleObject.transform.parent = gameObject.transform;
        if ((Screen.width * 1f / Screen.height) > 1.7f)
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = new Vector3(2400, -400, 4000);
        }
        else if ((Screen.width * 1f / Screen.height) < 1.4f)
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = new Vector3(2110, -400, 4000);
        }
        else
        {
            PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localPosition = new Vector3(2160, -400, 4000);
        }
        PictureCreater.instance.ListRolePicture[0].RoleObject.transform.localScale = new Vector3(300, 300, 300) * TextTranslator.instance.GetHeroInfoByHeroID(PictureCreater.instance.ListRolePicture[0].RoleID).heroScale;
        PictureCreater.instance.ListRolePicture[0].RoleObject.transform.Rotate(new Vector3(0, 65, 0));

        foreach (var item in TextTranslator.instance.heroInfoList)
        {
            if (_RoleID == item.heroID)
            {
                for (int k = 0; k < CharacterRecorder.instance.ownedHeroList.size; k++)
                {
                    if (CharacterRecorder.instance.ownedHeroList[k].cardID == _RoleID)
                    {
                        mHero = CharacterRecorder.instance.ownedHeroList[k];
                    }
                }

                _HeroInfo = item;
                CareerLabel.text = item.heroName;
                ADLabel.text = item.heroStrong.ToString();
                ADDefenceLabel.text = item.heroPhyDef.ToString();
                APLabel.text = item.heroIntell.ToString();
                APDefenceLabel.text = "0";
                HPLabel.text = item.heroHP.ToString();
                SkillNameLabel.text = TextTranslator.instance.GetSkillByID(mHero.skill[0], mHero.skillLevel).skillName;
                SkillDesLabel1.text = TextTranslator.instance.GetSkillByID(mHero.skill[0], mHero.skillLevel).description;
                SkillDesLabel2.text = TextTranslator.instance.GetSkillByID(mHero.skill[0], mHero.skillLevel).description;
                SkillNameLabel2.text = TextTranslator.instance.GetSkillByID(mHero.skill[2], mHero.skillLevel).skillName;
                SkillDes2.text = TextTranslator.instance.GetSkillByID(mHero.skill[2], mHero.skillLevel).description;

                //天赋
                /*
                int count = 0;
                for (int k = 0; k < mHero.heroTalentList.Count; k++)
                {
                    talentName[k].text = mHero.heroTalentList[k].name;
                    talentDes[k].text = mHero.heroTalentList[k].desc;
                    count++;
                }

                for (int k = count; k < item.heroTalentList.Count; k++)
                {
                    talentName[k].text = TextTranslator.instance.GetTalentByID(item.heroTalentList[k]).name;
                    //talentName[k].color = Color.gray;
                    talentDes[k].text = TextTranslator.instance.GetTalentByID(item.heroTalentList[k]).desc;
                    //talentDes[k].color = Color.gray;
                }

                RoleClassUp mRoleClassUp = TextTranslator.instance.GetRoleClassUpInfoByID(mHero.cardID, mHero.classNumber + 1);
                List<Item> classUpList = new List<Item>();
                for (int j = 0; j < mRoleClassUp.NeedItemList.Count; j++)
                {
                    if (mRoleClassUp.NeedItemList[j].itemCode != 0)
                    {
                        classUpList.Add(mRoleClassUp.NeedItemList[j]);
                    }
                }
                */

            }
        }

        HeroInfo hinfo = _HeroInfo;
        NameLabel.text = hinfo.heroName;
        IntrodunceLabel.text = hinfo.heroDescription;
        for (int j = 1; j < 6; j++)
        {
            Hujia.transform.FindChild("Ai" + j).GetComponent<UISprite>().color = Color.gray;
        }
        Hujia.transform.FindChild("Ai" + hinfo.heroAi).GetComponent<UISprite>().color = Color.white;
        switch (hinfo.heroAi)
        {
            case 1:
                AiType.GetComponent<UISprite>().spriteName = "ui1_gaiicon5";
                break;
            case 2:
                AiType.GetComponent<UISprite>().spriteName = "ui1_gaiicon2";
                break;
            case 3:
                AiType.GetComponent<UISprite>().spriteName = "ui1_gaiicon4";
                break;
            case 4:
                AiType.GetComponent<UISprite>().spriteName = "ui1_gaiicon3";
                break;
            case 5:
                AiType.GetComponent<UISprite>().spriteName = "ui1_gaiicon1";
                break;
            default:
                break;
        }

        UIEventListener.Get(DetailButton).onPress = delegate(GameObject obj, bool isPress)
        {
            if (isPress)
            {
                DetailBoard.SetActive(true);
                DetailBoard.transform.FindChild("AttackDistance").FindChild("Number").GetComponent<UILabel>().text = _HeroInfo.heroArea.ToString();
                DetailBoard.transform.FindChild("MoveDistance").FindChild("Number").GetComponent<UILabel>().text = _HeroInfo.heroMove.ToString();
                DetailBoard.transform.FindChild("Crit").FindChild("Number").GetComponent<UILabel>().text = _HeroInfo.heroPcritical.ToString();
                DetailBoard.transform.FindChild("Speed").FindChild("Number").GetComponent<UILabel>().text = _HeroInfo.heroSpeed.ToString();
                DetailBoard.transform.FindChild("PVPAttact").FindChild("Number").GetComponent<UILabel>().text = "0";
                DetailBoard.transform.FindChild("PVPDefence").FindChild("Number").GetComponent<UILabel>().text = "0";
            }
            else
            {
                DetailBoard.SetActive(false);
            }
        };
    }


}
