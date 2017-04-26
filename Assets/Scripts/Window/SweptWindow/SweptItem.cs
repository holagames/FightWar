using UnityEngine;
using System.Collections;

public class SweptItem : MonoBehaviour
{

    public UILabel EXPlabel;
    public UILabel Goldlabel;
    public UILabel Namelabel;
    public GameObject uiGrid;
    public GameObject AwardItem;

    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;
    // Use this for initialization
    void Start()
    {

    }

    public void SetInfo(int EXP, int Gold, string AwardString,string Name)//输入体力，金币等
    {
        EXPlabel.text = "+" + EXP.ToString();
        Goldlabel.text = "+" + Gold.ToString();
        Namelabel.text = Name;
        //string[] dataSplit = AwardString.Split('!');
        //for (int i = 0; i < dataSplit.Length-1; i++)
        //{
        //    string[] secSplit = dataSplit[i].Split('$');
        //    CreatItem(int.Parse(secSplit[0]), int.Parse(secSplit[1]));
        //}
        //StartCoroutine(SetReward(AwardString));
        SetReward(AwardString);
    }
    IEnumerator SetReward1(string AwardString) 
    {
        //yield return new WaitForSeconds(0.5f);
        string[] dataSplit = AwardString.Split('!');
        for (int i = 0; i < dataSplit.Length - 1; i++)
        {
            string[] secSplit = dataSplit[i].Split('$');
            CreatItem(int.Parse(secSplit[0]), int.Parse(secSplit[1]));
        }
        yield return new WaitForSeconds(0);
    }
    void SetReward(string AwardString)
    {
        //yield return new WaitForSeconds(0.5f);
        string[] dataSplit = AwardString.Split('!');
        for (int i = 0; i < dataSplit.Length - 1; i++)
        {
            string[] secSplit = dataSplit[i].Split('$');
            CreatItem(int.Parse(secSplit[0]), int.Parse(secSplit[1]));
            
        }
    }
    void CreatItem(int itemID, int itemCount)
    {
        GameObject go = NGUITools.AddChild(uiGrid, AwardItem);
        go.transform.localScale = new Vector3(0.9f, 0.9f, 0.9f);
        //uiGrid.GetComponent<UIGrid>().Reposition();
        TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(itemID);


        if (itemID.ToString()[0] == '4')
        {
            if (itemID.ToString()[1] == '0')
            {
                go.transform.FindChild("SuiPian").gameObject.SetActive(false);
            }
            else
            {
                go.transform.FindChild("SuiPian").gameObject.SetActive(true);
            }

            SetColor(go, mitemInfo.itemGrade);
            go.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = itemID.ToString();
            go.transform.FindChild("Count").GetComponent<UILabel>().text = itemCount.ToString();
            go.transform.FindChild("SuiPian").gameObject.SetActive(true);
        }
        else if (itemID.ToString()[0] == '6')
        {
            SetColor(go, mitemInfo.itemGrade);
            go.transform.FindChild("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
            go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = itemID.ToString();
            go.transform.FindChild("Count").GetComponent<UILabel>().text = itemCount.ToString();
            go.transform.FindChild("SuiPian").gameObject.SetActive(false);
        }
        else if (itemID == 70000 || itemID == 79999)
        {
            SetColor(go, mitemInfo.itemGrade);
            go.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = itemID.ToString();
            go.transform.FindChild("Count").GetComponent<UILabel>().text = itemCount.ToString();
            go.transform.FindChild("SuiPian").gameObject.SetActive(true);
        }
        else if (itemID.ToString()[0] == '7' && itemID > 70000 && itemID != 79999)
        {
            SetColor(go, mitemInfo.itemGrade);
            go.transform.FindChild("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
            go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = (itemID - 10000).ToString();
            go.transform.FindChild("Count").GetComponent<UILabel>().text = itemCount.ToString();
            go.transform.FindChild("SuiPian").gameObject.SetActive(true);

            HeroInfo hero = TextTranslator.instance.GetHeroInfoByHeroID(itemID - 10000);
            go.GetComponent<UISprite>().spriteName = "Grade" + (hero.heroRarity + 1).ToString();
        }
        else if (itemID.ToString()[0] == '8')
        {
            SetColor(go, mitemInfo.itemGrade);
            go.transform.FindChild("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
            TextTranslator.ItemInfo mItemInfo = TextTranslator.instance.GetItemByItemCode(itemID);
            go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = mItemInfo.picID.ToString();
            go.transform.FindChild("Count").GetComponent<UILabel>().text = itemCount.ToString();
            go.transform.FindChild("SuiPian").gameObject.SetActive(true);
        }
        else
        {
            SetColor(go, mitemInfo.itemGrade);
            go.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            go.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = itemID.ToString();
            go.transform.FindChild("Count").GetComponent<UILabel>().text = itemCount.ToString();
            go.transform.FindChild("SuiPian").gameObject.SetActive(false);
        }

        uiGrid.GetComponent<UIGrid>().repositionNow = true;//mitemInfo.picID
        //if (mitemInfo.picID == CharacterRecorder.instance.SweptIconID)
        //{
        //    CharacterRecorder.instance.SweptIconNum++;
        //    StartCoroutine(ShowEffectGuang(go));
        //}
        //else 
        if (itemID == CharacterRecorder.instance.SweptIconID) 
        {
            CharacterRecorder.instance.SweptIconNum += itemCount;
            StartCoroutine(ShowEffectGuang(go));
        }
    }

    IEnumerator ShowEffectGuang(GameObject go) 
    {
        yield return new WaitForSeconds(0.2f);
        //go.transform.Find("EffectGuang").gameObject.layer = 11;
        go.transform.Find("EffectGuang").gameObject.SetActive(true);
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
            //case 5:
            //    go.GetComponent<UISprite>().spriteName = "Grade5";
            //    break;
        }
    }
}
