using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobberyHeroList : MonoBehaviour {
    public GameObject RobberyHeroListObj;
    private List<GameObject> Item = new List<GameObject>();
    public GameObject HeroItem;
    public GameObject Grid;
    public GameObject NowValue;
    public GameObject Mask;
	// Use this for initialization
	void Start () {
       // RobberyHeroListObj = GameObject.Find("RobberyHeroList");
        if (UIEventListener.Get(RobberyHeroListObj.transform.Find("EscButton").gameObject).onClick == null) {
            UIEventListener.Get(RobberyHeroListObj.transform.Find("EscButton").gameObject).onClick += delegate(GameObject obj)
            {
                this.gameObject.SetActive(false);
            };
        }
        if (UIEventListener.Get(RobberyHeroListObj.transform.Find("ChangeButton").gameObject).onClick == null) {
            UIEventListener.Get(RobberyHeroListObj.transform.Find("ChangeButton").gameObject).onClick += delegate(GameObject obj)
            {
                Debug.Log("点了换一批了。");
                NetworkHandler.instance.SendProcess("1401#" + GameObject.Find("GoodsItemObj").GetComponent<GoodsItemWindow>().Id+ ";");
            };
        }       
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    public void SetInfo(){
        InstantiateItem();
        if (CharacterRecorder.instance.sprite < 2)
        {
            NowValue.GetComponent<UILabel>().text = "[FB2D50]" + CharacterRecorder.instance.sprite.ToString()+"[-]";
        }
        else
        {
            NowValue.GetComponent<UILabel>().text = CharacterRecorder.instance.sprite.ToString();
        }
    }
    /// <summary>
    /// 生成列表
    /// </summary>
    private void InstantiateItem(){
     List<Getgemlist> getList = new List<Getgemlist>();
     getList = GameObject.Find("GrabItemWindow").GetComponent<GrabWindow>().Getgemlist;
        for (int i = 0; i < Item.Count; i++) {
            DestroyImmediate(Item[i]);
        }
        Item.Clear();
        for (int i = 0; i < getList.Count; i++) {
            GameObject obj = Instantiate(HeroItem) as GameObject;
            obj.transform.Find("HeroItemIcon").gameObject.SetActive(false);
            obj.transform.parent = Grid.transform;
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            obj.SetActive(true);
            Item.Add(obj);
            obj.GetComponent<GrabHeroItem>().SetInfo(getList[i]);
        }
        Grid.GetComponent<UIGrid>().repositionNow = true;
    }
}
