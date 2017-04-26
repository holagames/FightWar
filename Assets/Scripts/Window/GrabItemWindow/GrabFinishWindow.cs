using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class GrabFinishWindow : MonoBehaviour
{
    public GameObject GrabWin;
    public GameObject GrabLose;
    public GameObject AwardItem;
    public GameObject OtherItem;
    public GameObject LoseMessage;
    public GameObject Exp;
    public GameObject GoldObj;
    public GameObject AwardObj;
    public GameObject ClickMessage;
    public GameObject BackButton;
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;
    public UILabel GlodLabel;

    // Use this for initialization

    void Start()
    {
        UIEventListener.Get(BackButton).onClick += delegate(GameObject go)
        {
            //if (CharacterRecorder.instance.GuideID[10] == 11)
            //{
            //    CharacterRecorder.instance.GuideID[10] += 1;
            //    StartCoroutine(SceneTransformer.instance.NewbieGuide());
            //}
            //UIManager.instance.OpenPanel("GrabItemWindow",true);
            if (PictureCreater.instance.FightStyle == 3)
            {
                UIManager.instance.BackTwoUI("GrabItemWindow");
            }
            else
            {
                UIManager.instance.BackTwoUI("SmuggleWindow");
            }
            PictureCreater.instance.StopFight(true);
            if (PictureCreater.instance.FightStyle == 3)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }            
        };
        if (PictureCreater.instance.FightStyle == 3)
        {
            StartCoroutine(AddNumber(500));
        }
    }

    IEnumerator AddNumber(int MaxNumber)
    {
        int count = 0;
        while (count < MaxNumber)
        {
            count += 5;
            yield return new WaitForSeconds(0.01f);
            if (count >= MaxNumber)
            {
                count = MaxNumber;
            }
            GlodLabel.text = "+" + count.ToString();
        }
    }
    public void GrabInfo(int IsWin, List<FragmentItemData> Award)
    {
        GameObject.Find("Black").GetComponent<BoxCollider>().enabled = false;
        if (IsWin == 1)
        {
            GrabWin.SetActive(true);
            AudioEditer.instance.PlayLoop("Win");
        }
        else
        {
            AudioEditer.instance.PlayLoop("Lose");
            GrabLose.SetActive(true);
        }
        StartCoroutine(DelayEffect(IsWin, Award));
    }
    IEnumerator DelayEffect(int IsWin, List<FragmentItemData> Award)
    {
        yield return new WaitForSeconds(2f);
        Exp.SetActive(true);
        if (PictureCreater.instance.FightStyle != 13)
        {
            GoldObj.SetActive(true);
        }
        AwardObj.SetActive(true);
        Exp.transform.Find("Level").GetComponent<UILabel>().text = "LV." + CharacterRecorder.instance.level.ToString();
        Exp.transform.Find("Label").GetComponent<UILabel>().text = CharacterRecorder.instance.exp.ToString() + "/" + CharacterRecorder.instance.expMax.ToString();
        Exp.GetComponent<UISlider>().value = float.Parse(CharacterRecorder.instance.exp.ToString()) / float.Parse(CharacterRecorder.instance.expMax.ToString());
        StartCoroutine(DelayButton());
        if (PictureCreater.instance.FightStyle != 13)
        {
            ItemInfo(Award[0].FiveGrabId, OtherItem);//必得奖励 
            OtherItem.transform.Find("Number").GetComponent<UILabel>().text = Award[0].FiveGrabNumber.ToString();
        }
        else if (IsWin == 1)
        {
            ItemInfo(Award[0].FiveGrabId, OtherItem);//必得奖励 
            OtherItem.transform.Find("Number").GetComponent<UILabel>().text = Award[0].FiveGrabNumber.ToString();
        }
        else
        {
            AwardObj.SetActive(false);
        }
        if (IsWin == 1)
        {
            if (Award[0].AwardID != 0)
            {
                ItemInfo(Award[0].AwardID, AwardItem);//碎片
                AwardItem.transform.Find("ItemName").GetComponent<UILabel>().text =ItemNameColor(TextTranslator.instance.GetItemByItemCode(Award[0].AwardID).itemGrade) +
                    TextTranslator.instance.GetItemByItemCode(Award[0].AwardID).itemName;
            }
            else
            {
                LoseMessage.SetActive(true);
            }
        }
        else
        {

            LoseMessage.SetActive(true);
        }
        if(PictureCreater.instance.FightStyle==13)
        {
            LoseMessage.SetActive(false);
        }  
    }
    IEnumerator DelayButton()
    {
        yield return new WaitForSeconds(0.2f);
        //  ClickMessage.SetActive(true);
        BackButton.SetActive(true);
        GameObject.Find("Black").GetComponent<BoxCollider>().enabled = true;
    }
    public void ItemInfo(int id, GameObject Item)
    {
        Item.SetActive(true);
        Item.GetComponent<UISprite>().spriteName = "Grade" + (TextTranslator.instance.GetItemByItemCode(id).itemGrade);
        if (id > 40000 && id < 50000)
        {
            Item.transform.Find("AwardSuipian").gameObject.SetActive(true);
            Item.transform.Find("AwardItem").GetComponent<UISprite>().atlas = ItemAtlas;
            Item.transform.Find("AwardItem").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(id - 10000).picID.ToString();
        }
        else if (id > 80000 && id < 90000)
        {
            Item.transform.Find("AwardSuipian").gameObject.SetActive(true);
            Item.transform.Find("AwardItem").GetComponent<UISprite>().atlas = ItemAtlas;
            Item.transform.Find("AwardItem").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(id / 10 * 10 - 30000).picID.ToString();

        }
        else if (id > 70000 && id < 79999)
        {
            Item.transform.Find("AwardSuipian").gameObject.SetActive(true);
            Item.transform.Find("AwardItem").GetComponent<UISprite>().atlas = RoleAtlas;
            Item.transform.Find("AwardItem").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(id - 10000).picID.ToString();
        }
        else
        {
            Item.transform.Find("AwardItem").GetComponent<UISprite>().atlas = ItemAtlas;
            Item.transform.Find("AwardItem").GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(id).picID.ToString();
            Item.transform.Find("AwardSuipian").gameObject.SetActive(false);
        }
    }
    public string ItemNameColor(int GradeID)
    {
        string NameColor = "";
        switch (GradeID)
        {
            case 1:
                NameColor = "[-][B3B3B3]";
                break;
            case 2:
                NameColor = "[-][28DF5E]";
                break;
            case 3:
                NameColor = "[-][12A7B8]";
                break;
            case 4:
                NameColor = "[-][842DCE]";
                break;
            case 5:
                NameColor = "[-][DC582D]";
                break;
            case 6:
                NameColor = "[-][D9181E]";
                break;
        }
        return NameColor;
    }

}
