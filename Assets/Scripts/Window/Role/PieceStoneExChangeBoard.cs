using UnityEngine;
using System.Collections;

public class PieceStoneExChangeBoard: MonoBehaviour
{
    public static bool IsAfterChanged = false;
    public GameObject LevelUpEffect;
    public UISlider UiSlider;
    public UILabel UiSliderLabel;
    [SerializeField]
    private GameObject OnceButton;
    [SerializeField]
    private GameObject TenButton;
    [SerializeField]
    private GameObject CloseButton;
    [SerializeField]
    private GameObject Item1;
    [SerializeField]
    private GameObject Item2;

    private int allPowerItemCount = 0;
    private int Item2Code = 0;
    private int exChangecount = 0;
    void OnEnable()
    {

    }
    // Use this for initialization
    void Start()
    {

        if (UIEventListener.Get(OnceButton).onClick == null)
        {
            UIEventListener.Get(OnceButton).onClick += delegate(GameObject go)
            {
                //if (this.Item2Code != 0)
                if (allPowerItemCount > 0)
                {
                    exChangecount = 1;
                    NetworkHandler.instance.SendProcess("5011#" + this.Item2Code + ";" + exChangecount + ";");
                    //this.gameObject.SetActive(false);
                }
                else
                {
                    //UIManager.instance.OpenPromptWindow("无英雄碎片可转换", 11,false, PromptWindow.PromptType.Hint, null, null);
                    UIManager.instance.OpenPromptWindow("无万能碎片可转换", 11, false, PromptWindow.PromptType.Hint, null, null);
                }
            };
        }

        if (UIEventListener.Get(TenButton).onClick == null)
        {
            UIEventListener.Get(TenButton).onClick += delegate(GameObject go)
            {
                //if (this.Item2Code != 0)
                if (allPowerItemCount >= 10)
                {
                    exChangecount = 10;
                    NetworkHandler.instance.SendProcess("5011#" + this.Item2Code + ";" + exChangecount + ";");
                  //  this.gameObject.SetActive(false);
                }
                else if (allPowerItemCount > 0)
                {
                    UIManager.instance.OpenPromptWindow("万能碎片不足10个", 11, false, PromptWindow.PromptType.Hint, null, null);
                }
                else
                {
                    //UIManager.instance.OpenPromptWindow("无英雄碎片可转换", 11, false, PromptWindow.PromptType.Hint, null, null);
                    UIManager.instance.OpenPromptWindow("无万能碎片可转换", 11, false, PromptWindow.PromptType.Hint, null, null);
                }
                
            };
        }

        if (UIEventListener.Get(CloseButton).onClick == null)
        {
            UIEventListener.Get(CloseButton).onClick += delegate(GameObject go)
            {
                if (GameObject.Find("UpClassWindow")!=null)
                {
                    NetworkHandler.instance.SendProcess("3301#" + GameObject.Find("NuclearWeaponWindow").GetComponent<NuclearWeaponWindow>().HeroID.characterRoleID + ";1;");
                }
                this.gameObject.SetActive(false);
            };
        }

    }


    public void SetPieceStoneExChangeBoardInfo(int Item2Code,int Item2OwnedCount,int Item2NeedCount)
    {
        int allPowerItemid = 70000;
        HeroInfo heroInfo = TextTranslator.instance.GetHeroInfoByHeroID(Item2Code - 10000);
        if (heroInfo.heroRarity == 5)//传说
        {
            allPowerItemid = 79999;
        }
        AwardItem _AwardItem1 = new AwardItem(allPowerItemid, TextTranslator.instance.GetItemCountByID(allPowerItemid));
        allPowerItemCount = _AwardItem1.itemCount;
        Item1.GetComponent<LoginAwardItem>().SetLoginAwardItem(_AwardItem1);
        //TextTranslator.ItemInfo miteminfo = TextTranslator.instance.GetItemByItemCode(allPowerItemid);
       // AwardItem _AwardItem2 = new AwardItem(Item2Code, TextTranslator.instance.GetItemCountByID(Item2Code));
        AwardItem _AwardItem2 = new AwardItem(Item2Code, Item2OwnedCount);
        Item2.GetComponent<LoginAwardItem>().SetLoginAwardItem(_AwardItem2);

        this.Item2Code = Item2Code;
        //Debug.LogError(Item2Code + ".." + Item2OwnedCount + "..." + Item2NeedCount);
        if (Item2OwnedCount == 0)
        {
            UiSlider.value = 0;
        }
        else
        {
            UiSlider.value = (float)Item2OwnedCount / Item2NeedCount;
        }
        UiSliderLabel.text = string.Format("{0}/{1}", Item2OwnedCount, Item2NeedCount);
        if (IsAfterChanged)
        {
            GameObject go = NGUITools.AddChild(Item1, LevelUpEffect);
            go.name = "WearEquipEffect";
            go.transform.localScale = new Vector3(400, 400, 400);
            go.transform.localPosition = new Vector3(-6, 4, 0);
            go.layer = 11;
            foreach (Component c in go.GetComponentsInChildren(typeof(Transform), true))
            {
                c.gameObject.layer = 11;
            }
            Invoke("DelayShowItem2Effect",0.5f);
            IsAfterChanged = false;
        }
        
    }
    void DelayShowItem2Effect()
    {
        GameObject go = NGUITools.AddChild(Item2, LevelUpEffect);
        go.name = "WearEquipEffect";
        go.transform.localScale = new Vector3(400, 400, 400);
        go.transform.localPosition = new Vector3(6, 4, 0);
        go.layer = 11;
        foreach (Component c in go.GetComponentsInChildren(typeof(Transform), true))
        {
            c.gameObject.layer = 11;
        }
    }
   
}
