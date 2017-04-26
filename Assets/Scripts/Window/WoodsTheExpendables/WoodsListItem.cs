using UnityEngine;
using System.Collections;

public class WoodsListItem : MonoBehaviour
{

    public UISprite TopThreeSprite;
    public UILabel RankLabel;
    public UILabel NameLabel;
    public UILabel ScoreLabel;
    public UILabel GateNumberLabel;
    public UISprite GradeSpirte;
    public UISprite ItemSprite;
    public int HeroRoleID = 0;
    int spriteNum = 0;
    void Start()
    {
        UIEventListener.Get(this.gameObject).onClick += delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess(string.Format("1020#{0};", HeroRoleID));
            UIManager.instance.OpenPanel("LegionMemberItemDetail", false); 
        };
    }
    public void Initem(int Rank, int HeroID, string Name, int ItemID, int Level, int Score, int Floor)
    {
        HeroRoleID = HeroID;
        if (Rank < 4)
        {
            switch (Rank)
            {
                case 1:
                    spriteNum = 4;
                    break;
                case 2:
                    spriteNum = 3;
                    break;
                case 3:
                    spriteNum = 5;
                    break;
            }
            TopThreeSprite.spriteName = "u32_icon" + spriteNum.ToString();
            TopThreeSprite.transform.Find("TopNumber").GetComponent<UISprite>().spriteName = "word" + Rank;
        }
        else
        {
            TopThreeSprite.gameObject.SetActive(false);
            RankLabel.gameObject.SetActive(true);
            RankLabel.text = Rank.ToString();
        }
        ItemSprite.spriteName = ItemID.ToString();
        GradeSpirte.spriteName = "yxdi" + (TextTranslator.instance.GetItemByItemCode(ItemID).itemGrade-1);
        NameLabel.text = Name;
        GateNumberLabel.text = Floor.ToString();
        ScoreLabel.text = Score.ToString();
    }
}
