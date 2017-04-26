using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SmuggleHeroInfo : MonoBehaviour
{
    //英雄信息
    public GameObject HeroInfoItem;
    public GameObject HeroGrid;
    public int PLayerID;
    public List<GameObject> HeroItemList = new List<GameObject>();
    private float Timer = 0;
    // Use this for initialization
    // Use this for initialization
    void Start()
    {

    }
    void Update()
    {
        Timer += Time.deltaTime;
        if (Timer >= 2)
        {
            Timer = 0;
            if (GameObject.Find(PLayerID.ToString())==null)
            {
                UIManager.instance.OpenPromptWindow("战队走私完成,窗口关闭", PromptWindow.PromptType.Hint, null, null);
                gameObject.SetActive(false);
            }
        }
    }
    #region ItemInfo
    public void ItemInfo(int HeroID, string CarID, string HeroName)
    {
        PLayerID=HeroID;
        NetworkHandler.instance.SendProcess("1020#" + HeroID + ";");
        gameObject.transform.Find("BG/Message").GetComponent<UILabel>().text = "是否打劫 " + "[ffff00]" + HeroName + "[-] 的" + CarID + " ?";

        UIEventListener.Get(gameObject.transform.Find("BG/CancelButton").gameObject).onClick += delegate(GameObject go)
        {
            gameObject.SetActive(false);
        };
        UIEventListener.Get(gameObject.transform.Find("BG/CloseButton").gameObject).onClick += delegate(GameObject go)
        {
            gameObject.SetActive(false);
        };
        UIEventListener.Get(gameObject.transform.Find("BG/SureButton").gameObject).onClick += delegate(GameObject go)
        {
            PictureCreater.instance.FightStyle = 13;
            PictureCreater.instance.PVPRank = HeroID;
            NetworkHandler.instance.SendProcess("6003#" + HeroID + ";");
            //Debug.LogError("抢劫成功");
            //gameObject.SetActive(false);
        };
    }

    public void HeroInfo(string dataSplit)
    {
        for (int i = 0; i < HeroItemList.Count; i++)
        {
            DestroyImmediate(HeroItemList[i]);
        }
        HeroItemList.Clear();
        string[] heroitem = dataSplit.Split('!');
        for (int i = 0; i < heroitem.Length - 1; i++)
        {
            string[] item = heroitem[i].Split('$');
            GameObject go = Instantiate(HeroInfoItem) as GameObject;
            go.SetActive(true);
            go.transform.parent = HeroGrid.transform;
            go.transform.localPosition = new Vector3(0, 0, 0);
            go.transform.localScale = new Vector3(1, 1, 1);
            TextTranslator.instance.SetHeroNameColor(go.GetComponent<UISprite>(), int.Parse(item[3]));
            //go.GetComponent<UISprite>().spriteName = "yxdi" +(int.Parse(item[3])-1).ToString();
            go.transform.Find("Level").GetComponent<UILabel>().text = item[1];
            go.transform.Find("Icon").GetComponent<UISprite>().spriteName = item[0];
            HeroItemList.Add(go);
        }
        HeroGrid.GetComponent<UIGrid>().repositionNow = true;
    }
    #endregion
}
