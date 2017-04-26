using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerRewardItem : MonoBehaviour
{
    public UILabel CurIntegral;
    public GameObject Icon1;
    public GameObject Number1;
    public GameObject Icon2;
    public GameObject Number2;
    public GameObject Icon3;
    public GameObject Number3;
    public GameObject Icon4;
    public GameObject Number4;
    public GameObject GoumaiObj;
    public GameObject SpriteColor;
    public int ItemId;
    public bool isBlack;
    public bool isGetReward;
    public GameObject ItemEffect;
    public GameObject GetRewardWindow;
    // Use this for initialization
    void Start()
    {
        if (UIEventListener.Get(this.gameObject).onClick == null)
        {
            UIEventListener.Get(this.gameObject).onClick += delegate(GameObject obj)
            {
                if (!isBlack)
                {
                    UIManager.instance.OpenPromptWindow("积分低于" + CurIntegral.text, PromptWindow.PromptType.Hint, null, null);
                }
                else if (isGetReward)
                {
                    UIManager.instance.OpenPromptWindow("已领过此积分奖励", PromptWindow.PromptType.Hint, null, null);
                }
                else
                {

                    if (int.Parse(gameObject.name) == CharacterRecorder.instance.HadRewardID+1)
                    {
                        NetworkHandler.instance.SendProcess("1506#" + ItemId + ";");
                    }
                    else
                    {
                        UIManager.instance.OpenPromptWindow("请先领取前一级奖励", PromptWindow.PromptType.Hint, null, null);
                    }
                    //GameObject.Find("GetReward").GetComponent<TowerGetReward>().ShowGetAwardNumber(int.Parse(Number1.GetComponent<UILabel>().text), int.Parse(Number2.GetComponent<UILabel>().text),
                    //int.Parse(Number3.GetComponent<UILabel>().text), int.Parse(Number4.GetComponent<UILabel>().text));
                }
            };
        }
    }

    public void setInfo(int id1, int count1, int id2, int count2, int id3, int count3, int id4, int count4, int Point, int ItemID, int NowIntegral,int colorUI)
    {

        CurIntegral.text = Point.ToString();
        if (int.Parse(CurIntegral.text) <= NowIntegral)
        {
            isBlack = true;
            this.gameObject.transform.Find("Black").gameObject.SetActive(false);
            GoumaiObj.SetActive(true);
            //GoumaiObj.GetComponent<UISprite>().spriteName = "PVPIntegration2";
            GoumaiObj.GetComponent<UISprite>().spriteName = "yuandi";
            GoumaiObj.transform.Find("Spritehuan").gameObject.SetActive(true);
            GoumaiObj.transform.Find("Spritelingqu").gameObject.SetActive(true);
            ItemEffect.SetActive(true);
        }
        SpriteColor.GetComponent<UISprite>().spriteName = "PVPranking" + colorUI;
        Icon1.GetComponent<UISprite>().spriteName = (id1 - 10000).ToString();
        Number1.GetComponent<UILabel>().text = count1.ToString();
        Icon2.GetComponent<UISprite>().spriteName = id2.ToString();
        Number2.GetComponent<UILabel>().text = count2.ToString();
        Icon3.GetComponent<UISprite>().spriteName = id3.ToString();
        Number3.GetComponent<UILabel>().text = count3.ToString();
        Icon4.GetComponent<UISprite>().spriteName = id4.ToString();
        Number4.GetComponent<UILabel>().text = count4.ToString();
        Icon1.transform.parent.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(id1 - 10000).itemGrade.ToString();
        Icon2.transform.parent.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(id2).itemGrade.ToString();
        Icon3.transform.parent.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(id3).itemGrade.ToString();
        Icon4.transform.parent.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(id4).itemGrade.ToString();
        ItemId = ItemID;
    }
    /// <summary>
    /// 真是购买
    /// </summary>
    /// <param name="IsTure"></param>
    public void SetIsGouMai(bool IsTure)
    {
        if (IsTure)
        {
            isGetReward = true;
            GoumaiObj.SetActive(true);
            //GoumaiObj.GetComponent<UISprite>().spriteName = "PVPIntegration1";
            GoumaiObj.GetComponent<UISprite>().spriteName = "PVPIntegration1";
            GoumaiObj.transform.Find("Spritehuan").gameObject.SetActive(false);
            GoumaiObj.transform.Find("Spritelingqu").gameObject.SetActive(false);
            this.gameObject.transform.Find("Black").gameObject.SetActive(true);
            ItemEffect.SetActive(false);
            GetRewardWindow.GetComponent<TowerGetReward>().Item1number += int.Parse(Number1.GetComponent<UILabel>().text);
            GetRewardWindow.GetComponent<TowerGetReward>().Item2number += int.Parse(Number2.GetComponent<UILabel>().text);
            GetRewardWindow.GetComponent<TowerGetReward>().Item3number += int.Parse(Number3.GetComponent<UILabel>().text);
            GetRewardWindow.GetComponent<TowerGetReward>().Item4number += int.Parse(Number4.GetComponent<UILabel>().text);
            GetRewardWindow.GetComponent<TowerGetReward>().Item1Label.text = GetRewardWindow.GetComponent<TowerGetReward>().Item1number.ToString();
            GetRewardWindow.GetComponent<TowerGetReward>().Item2Label.text = GetRewardWindow.GetComponent<TowerGetReward>().Item2number.ToString();
            GetRewardWindow.GetComponent<TowerGetReward>().Item3Label.text = GetRewardWindow.GetComponent<TowerGetReward>().Item3number.ToString();
            GetRewardWindow.GetComponent<TowerGetReward>().Item4Label.text = GetRewardWindow.GetComponent<TowerGetReward>().Item4number.ToString();
        }
        else
        {
            isGetReward = false;
            GoumaiObj.SetActive(false);
        }
    }
}
