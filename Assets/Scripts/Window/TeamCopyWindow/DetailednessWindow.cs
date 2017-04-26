using UnityEngine;
using System.Collections;

public class DetailednessWindow : MonoBehaviour {

    public UISprite HeroBg;
    public UISprite HeroIcon;
    public UILabel HeroGrade;
    public UILabel CharacterName;
    public UILabel characterLevel;

    public GameObject OutButton;
    public GameObject AddButton;
    public GameObject AddButton2;
    public GameObject CloseButton;

	void Start () {

        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                this.gameObject.SetActive(false);
            };
        }
	}

    public void SetDetailness(bool IsOwner,int teamId,int position,string _characterName,int _characterLevel,int heroId,int herolevel) 
    {
        HeroInfo hero = TextTranslator.instance.GetHeroInfoByHeroID(heroId);//_CharacterRoleID
        HeroBg.spriteName= "Grade" + (hero.heroRarity).ToString();
        HeroIcon.spriteName = heroId.ToString();
        HeroGrade.text = herolevel.ToString();
        CharacterName.text = _characterName;
        characterLevel.text = "等级"+_characterLevel.ToString();

        if (IsOwner)
        {
            OutButton.SetActive(true);
            AddButton.SetActive(true);
            AddButton2.SetActive(false);
            //if (UIEventListener.Get(OutButton).onClick == null)
            {
                UIEventListener.Get(OutButton).onClick += delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess("6105#" + teamId.ToString() + ";" + position.ToString() + ";");
                    this.gameObject.SetActive(false);
                };
            }
            //if (UIEventListener.Get(AddButton).onClick == null)
            {
                UIEventListener.Get(AddButton).onClick += delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess("7104#" + _characterName + ";");
                    this.gameObject.SetActive(false);
                };
            }
        }
        else 
        {
            OutButton.SetActive(false);
            AddButton.SetActive(false);
            AddButton2.SetActive(true);
            //if (UIEventListener.Get(AddButton2).onClick == null)
            {
                UIEventListener.Get(AddButton2).onClick += delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess("7104#" + _characterName + ";");
                };
            }
        }
    }
}
