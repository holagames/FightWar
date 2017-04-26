using UnityEngine;
using System.Collections;

public class BagItem : MonoBehaviour
{
    public UIAtlas RoleAtlas;
    public GameObject SpritePiece;
    int ItemCode;
    int ItemCount;
    int ItemGrade;
    int ItemEquipID;
    int Index;
    // Use this for initialization
    void Start()
    {
        if (UIEventListener.Get(gameObject).onClick == null)
        {
            UIEventListener.Get(gameObject).onClick += delegate(GameObject go)
            {
                Debug.Log("BBB" + ItemCode);
                BagWindow _bagWindow = GameObject.Find("BagWindow").GetComponent<BagWindow>();
                BagWindow.SelectedItem.transform.FindChild("yellowLine").gameObject.SetActive(false);
                BagWindow.SelectedItem = this.gameObject;
                _bagWindow.CountNextItemIndex();
                _bagWindow.SetBagClick(ItemCode);
            };
        }
    }

    public void Init(int _ItemCode, int _ItemGrade, int _ItemCount, int _ItemEquipID, int _Index)
    {
        ItemCode = _ItemCode;
        ItemCount = _ItemCount;
        //ItemCount = TextTranslator.instance.GetItemCountByID(ItemCode);
        ItemGrade = _ItemGrade;
        ItemEquipID = _ItemEquipID;
        Index = _Index;
        SpritePiece.SetActive(false);

        TextTranslator.instance.ItemDescription(this.gameObject, _ItemCode, 0);

        if (ItemCode.ToString()[0] == '5')
        {
            gameObject.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = ItemCode.ToString();
        }
        else if (ItemCode.ToString()[0] == '7')
        {
            if (ItemCode == 70000 || ItemCode == 79999)
            {
                gameObject.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = (ItemCode).ToString();
                //gameObject.transform.FindChild("SpritePiece").gameObject.SetActive(true);
                SpritePiece.SetActive(true);
            }
            else
            {
                gameObject.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
                gameObject.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = (ItemCode - 10000).ToString();
                //gameObject.transform.FindChild("SpritePiece").gameObject.SetActive(true);
                SpritePiece.SetActive(true);
            }
        }
        else if (ItemCode.ToString()[0] == '4')
        {
            gameObject.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = (ItemCode).ToString();
            //gameObject.transform.FindChild("SpritePiece").gameObject.SetActive(true);
            if (ItemCode.ToString()[1] == '2')
            {
                SpritePiece.SetActive(true);
            }
            else
            {
                SpritePiece.SetActive(false);
            }
        }
        else if (ItemCode.ToString()[0] == '8')
        {
            gameObject.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(ItemCode).picID.ToString(); //(((ItemCode - 30000) / 10) * 10).ToString();
            // gameObject.transform.FindChild("SpritePiece").gameObject.SetActive(true);
            SpritePiece.SetActive(true);
            switch (ItemCode.ToString()[4])
            {
                case '0':
                    SpritePiece.GetComponent<UISprite>().spriteName = "grab_ui4_icon2";
                    break;
                case '1':
                    SpritePiece.GetComponent<UISprite>().spriteName = "grab_ui4_icon3";
                    break;
                case '2':
                    SpritePiece.GetComponent<UISprite>().spriteName = "grab_ui4_icon4";
                    break;
                case '3':
                    SpritePiece.GetComponent<UISprite>().spriteName = "grab_ui4_icon5";
                    break;
                case '4':
                    SpritePiece.GetComponent<UISprite>().spriteName = "grab_ui4_icon1";
                    break;
                case '5':
                    SpritePiece.GetComponent<UISprite>().spriteName = "grab_ui4_icon6";
                    break;
                default:
                    break;
            }
        }
        else if (ItemCode.ToString()[0] == '2')
        {
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(ItemCode);
            gameObject.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = mItemInfo.picID.ToString();
        }
        else
        {
            gameObject.transform.Find("SpriteItem").gameObject.GetComponent<UISprite>().spriteName = ItemCode.ToString();
        }
        SetColor(this.gameObject, _ItemGrade);
        //gameObject.GetComponent<UISprite>().spriteName = "Grade" + _ItemGrade.ToString();
        gameObject.transform.Find("LabelCount").gameObject.GetComponent<UILabel>().text = ItemCount.ToString();
    }

    void SetColor(GameObject go, int color)
    {
        switch (color)
        {
            case 1:
                go.GetComponent<UISprite>().spriteName = "Grade1";
                break;
            case 2:
                go.GetComponent<UISprite>().spriteName = "Grade2";
                break;
            case 3:
                go.GetComponent<UISprite>().spriteName = "Grade3";
                break;
            case 4:
                go.GetComponent<UISprite>().spriteName = "Grade4";
                break;
            case 5:
                go.GetComponent<UISprite>().spriteName = "Grade5";
                break;
            case 6:
                go.GetComponent<UISprite>().spriteName = "Grade6";
                break;
            case 7:
                go.GetComponent<UISprite>().spriteName = "Grade7";
                break;
        }
    }
}
