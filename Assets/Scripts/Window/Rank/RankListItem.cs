using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RankListItem : MonoBehaviour {

    public UILabel RankNumber;
    public UILabel Level;
    public UILabel Name;
    public UISprite HeadIcon;
    public UILabel FightNumber;
    public List<UISprite> RankNumberSprite;
    private string userId;
    private string rankType;
	// Use this for initialization
	void Start () 
    {
        UIEventListener.Get(this.gameObject).onClick = ClickThisItem;
	}

    public void SetInfo(string RankType,string _RankNumber,string _uId,string _Name,string _HeadIcon,string CurLevel,string Fight)
    {
        rankType = RankType;
        userId = _uId;
        this.name = _uId;
        RankNumber.text = _RankNumber;
        //_Level = Random.Range(30, 50).ToString();
        Level.text = string.Format("{0}", CurLevel);
        Name.text = _Name;
        HeadIcon.spriteName = _HeadIcon;
        FightNumber.text = Fight;
        switch (int.Parse(_RankNumber)) { 
            case 1:
                RankNumber.gameObject.SetActive(false);
                RankNumberSprite[0].spriteName = "u32_icon4";
                RankNumberSprite[1].spriteName = "word1";
                break;
            case 2:
                RankNumber.gameObject.SetActive(false);
                RankNumberSprite[0].spriteName = "u32_icon3";
                RankNumberSprite[1].spriteName = "word2";
                break;
            case 3:
                RankNumber.gameObject.SetActive(false);
                RankNumberSprite[0].spriteName = "u32_icon5";
                RankNumberSprite[1].spriteName = "word3";
                break;
            default:
                RankNumber.gameObject.SetActive(true);
                RankNumberSprite[0].gameObject.SetActive(false);
                RankNumber.text = _RankNumber;
                break;
        }
    }
    private void ClickThisItem(GameObject go)
    {
        if(go != null)
        {
            NetworkHandler.instance.SendProcess("1020#" + userId + ";");
            UIManager.instance.OpenPanel("LegionMemberItemDetail", false); 
        }
    }
}
