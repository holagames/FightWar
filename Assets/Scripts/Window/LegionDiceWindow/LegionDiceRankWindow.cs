using UnityEngine;
using System.Collections;

public class LegionDiceRankWindow : MonoBehaviour {

    public GameObject uigrid;
    public GameObject DiceRankItem;
    public GameObject CloseButton;

	void Start () {
        NetworkHandler.instance.SendProcess("8506#;");
        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick = delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };   
        }
	}

    public void LegionGetDicelist(string Recving) //8506
    {
        string[] dataSplit = Recving.Split(';');
        if (dataSplit[0] != "") 
        {
            Debug.Log("进入排行");
            for (int i = 0; i < dataSplit.Length - 1; i++)
            {
                string[] trcSplit = dataSplit[i].Split('$');
                GameObject clone = NGUITools.AddChild(uigrid, DiceRankItem);
                clone.SetActive(true);
                if (i == 0)
                {
                    clone.transform.Find("RankNumber").gameObject.SetActive(false);
                    clone.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                    clone.transform.Find("LabelRankSprite/RankSprite").GetComponent<UISprite>().spriteName = "word1";
                }
                else if (i == 1)
                {
                    clone.transform.Find("RankNumber").gameObject.SetActive(false);
                    clone.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                    clone.transform.Find("LabelRankSprite/RankSprite").GetComponent<UISprite>().spriteName = "word2";
                }
                else if (i == 2)
                {
                    clone.transform.Find("RankNumber").gameObject.SetActive(false);
                    clone.transform.Find("LabelRankSprite").gameObject.SetActive(true);
                    clone.transform.Find("LabelRankSprite/RankSprite").GetComponent<UISprite>().spriteName = "word3";
                }
                else
                {
                    clone.transform.Find("RankNumber").gameObject.SetActive(true);
                    clone.transform.Find("LabelRankSprite").gameObject.SetActive(false);
                    clone.transform.Find("RankNumber").GetComponent<UILabel>().text = (i + 1).ToString();
                }
                Debug.LogError(i);
                clone.transform.Find("CharacterName").GetComponent<UILabel>().text = trcSplit[0];
                clone.transform.Find("BaiNiNum").GetComponent<UILabel>().text = trcSplit[1];
                clone.transform.Find("HeroGrade/HeroIcon").GetComponent<UISprite>().spriteName = trcSplit[2];
            }
            uigrid.GetComponent<UIGrid>().Reposition();  
        }
     
    }
}
