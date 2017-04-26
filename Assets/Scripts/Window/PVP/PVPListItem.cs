using UnityEngine;
using System.Collections;

public class PVPListItem : MonoBehaviour {

    public UISprite PVPListSprite;
    public UILabel RankLabel;
    public UILabel LevelLabel;
    public UILabel NameLabel;
    private int RoleID;

    int iconNum = 6;
    void Start()
    {
        if (UIEventListener.Get(this.gameObject).onClick == null)
        {
            UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("1020#" + RoleID + ";");
                UIManager.instance.OpenPanel("LegionMemberItemDetail", false); 
            };
        }
    }
    public void Init(int _Ranking,int _Level,string _Name,int _RoleID)
    {
        if (_Ranking < 4)
        {
            PVPListSprite.spriteName = "word" + _Ranking.ToString();
            PVPListSprite.MakePixelPerfect();

            if (_Ranking == 1) 
            {
                PVPListSprite.transform.GetChild(0).GetComponent<UISprite>().spriteName = "u32_icon4";
            }
            else if (_Ranking == 2) 
            {
                PVPListSprite.transform.GetChild(0).GetComponent<UISprite>().spriteName = "u32_icon3";
            }
            else if (_Ranking == 3) 
            {
                PVPListSprite.transform.GetChild(0).GetComponent<UISprite>().spriteName = "u32_icon5";
            }
            //PVPListSprite.transform.GetChild(0).GetComponent<UISprite>().spriteName = "u32_icon" + (iconNum -_Ranking).ToString();
        }
        else
        {
            PVPListSprite.gameObject.SetActive(false);
            RankLabel.gameObject.SetActive(true);
            RankLabel.text = _Ranking.ToString();
        }
        LevelLabel.text = _Level.ToString();
        NameLabel.text = _Name;
        RoleID = _RoleID;
    }
}
