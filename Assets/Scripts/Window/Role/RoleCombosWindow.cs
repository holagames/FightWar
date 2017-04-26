using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class RoleCombosWindow : MonoBehaviour
{

    public GameObject TeamLeader;
    public GameObject HeroItm;
    public UIGrid HeroGrid;
    public UILabel SkillName;
    public UILabel SkillDes;
    public GameObject CloseButton;
    private List<GameObject> HeroList = new List<GameObject>();
    // Use this for initialization
    void Start()
    {
        gameObject.layer = 11;
        UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
        {
            DestroyImmediate(this.gameObject);
        };
    }

    public void SetHeroInfo(int HeroID)
    {
        for (int i = 0; i < HeroList.Count; i++)
        {
            DestroyImmediate(HeroList[i]);
        }
        HeroList.Clear();
        CombinSkill HeroInfo = TextTranslator.instance.GetCombinSkillByID(HeroID);
        if (HeroInfo == null)
        {
            return;
        }
        TeamLeader.GetComponent<UISprite>().spriteName = HeroInfo.HeroId1.ToString();
        HeroInfo Hero = TextTranslator.instance.GetHeroInfoByHeroID(HeroInfo.HeroId1);
        TeamLeader.transform.Find("Name").GetComponent<UILabel>().text = "[-][FD792A]" + Hero.heroName;
        for (int i = 0; i < 5; i++)
        {
            GameObject go = Instantiate(HeroItm) as GameObject;
            go.transform.parent = HeroGrid.transform;
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.SetActive(true);
            HeroList.Add(go);
        }
        HeroGrid.repositionNow = true;

        for (int i = 0; i < HeroList.Count; i++)
        {
            bool isHavedHero = false;
            int RoleID = 0;
            string RoleName = "";
            foreach (Hero item in CharacterRecorder.instance.ownedHeroList)
            {
                switch (i)
                {
                    case 0:
                        if (HeroInfo.HeroId2 == item.cardID)
                        {
                            isHavedHero = true;
                            RoleID = item.cardID;
                            RoleName = item.name;
                        }
                        else
                        {
                            if (HeroInfo.HeroId2 != 0)
                            {
                                RoleID = HeroInfo.HeroId2;
                                RoleName = TextTranslator.instance.GetHeroInfoByHeroID(HeroInfo.HeroId2).heroName;
                            }
                        }
                        break;
                    case 1:
                        if (HeroInfo.HeroId3 == item.cardID)
                        {
                            isHavedHero = true;
                            RoleID = item.cardID;
                            RoleName = item.name;
                        }
                        else
                        {
                            if (HeroInfo.HeroId3 != 0)
                            {
                                RoleID = HeroInfo.HeroId3;
                                RoleName = TextTranslator.instance.GetHeroInfoByHeroID(HeroInfo.HeroId3).heroName;
                            }
                        }
                        break;
                    case 2:
                        if (HeroInfo.HeroId4 == item.cardID)
                        {
                            isHavedHero = true;
                            RoleID = item.cardID;
                            RoleName = item.name;
                        }
                        else
                        {
                            if (HeroInfo.HeroId4 != 0)
                            {
                                RoleID = HeroInfo.HeroId4;
                                RoleName = TextTranslator.instance.GetHeroInfoByHeroID(HeroInfo.HeroId4).heroName;
                            }
                        }
                        break;
                    case 3:
                        if (HeroInfo.HeroId5 == item.cardID)
                        {
                            isHavedHero = true;
                            RoleID = item.cardID;
                            RoleName = item.name;
                        }
                        else
                        {

                            if (HeroInfo.HeroId5 != 0)
                            {
                                RoleID = HeroInfo.HeroId5;
                                RoleName = TextTranslator.instance.GetHeroInfoByHeroID(HeroInfo.HeroId5).heroName;
                            }
                        }
                        break;
                    case 4:
                        if (HeroInfo.HeroId6 == item.cardID)
                        {
                            isHavedHero = true;
                            RoleID = item.cardID;
                            RoleName = item.name;
                        }
                        else
                        {
                            if (HeroInfo.HeroId6 != 0)
                            {
                                RoleID = HeroInfo.HeroId6;
                                RoleName = TextTranslator.instance.GetHeroInfoByHeroID(HeroInfo.HeroId6).heroName;
                            }
                        }
                        break;
                }
            }
            if (isHavedHero)
            {
                HeroList[i].transform.Find("Icon").GetComponent<UISprite>().spriteName = RoleID.ToString();
                HeroList[i].transform.Find("Name").GetComponent<UILabel>().text = "[-][FD792A]" + RoleName;
            }
            else
            {
                HeroList[i].GetComponent<UISprite>().spriteName = "zuhe_andi";
                HeroList[i].transform.Find("Icon").GetComponent<UISprite>().spriteName = RoleID.ToString() + "1";
                HeroList[i].transform.Find("Name").GetComponent<UILabel>().text = "[-][969696]" + RoleName;
            }
        }
        SkillName.text = HeroInfo.SkillName;
        SkillDes.text = HeroInfo.SkillDes;
    }
}
