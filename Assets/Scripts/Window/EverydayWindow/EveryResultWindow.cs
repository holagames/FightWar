using UnityEngine;
using System.Collections;

public class EveryResultWindow : MonoBehaviour
{

    public GameObject HunterResult;
    public GameObject MilitaryResult;
    public GameObject HeroItemUp;
    public UILabel DamageLabel;
    public UILabel TruckLabel;
    public UILabel GoldLabel;
    public GameObject SureButton;
    public GameObject AwardItemObj;
    public UIGrid AwardGrid;
    public GameObject WinEffect;
    public GameObject LoseEffect;
    public GameObject Award;
    public GameObject combo;
    public GameObject SpriteStar1;
    public GameObject SpriteStar2;
    public GameObject SpriteStar3;
    public int _StarNum;
    //public GameObject heroItem;
    public string[] AwardString;
    [HideInInspector]
    public int IsShowStar = 0;
    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;
    // Use this for initialization
    void Start()
    {
        if (UIEventListener.Get(SureButton).onClick == null)
        {
            UIEventListener.Get(SureButton).onClick += delegate(GameObject go)
            {
                if (PictureCreater.instance.FightStyle == 17)
                {
                    UIManager.instance.BackUI();
                }
                else if (_StarNum != 0)
                {
                    UIManager.instance.BackTwoUI("MapUiWindow");
                }
                else
                {
                    UIManager.instance.BackTwoUI("EverydayWindow");
                }
                if (CharacterRecorder.instance.GuideID[9] == 14)
                {
                    CharacterRecorder.instance.GuideID[9] += 1;
                }
                if (PictureCreater.instance.FightStyle == 6)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_2811);
                }
                else if (PictureCreater.instance.FightStyle == 4)
                {
                    UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_3307);
                }
                PictureCreater.instance.StopFight(true);
                //  UIManager.instance.OpenPanel("EverydayWindow", true);                
            };
        }
    }

    public void SetHunterInfo(int Gold)
    {
        StartCoroutine(DelayShowItem(1));
        StartCoroutine(UpdateGold(Gold));
        TruckLabel.text = ((1 - PictureCreater.instance.ListEnemyPicture[0].RoleNowBlood / PictureCreater.instance.ListEnemyPicture[0].RoleMaxBlood) * 100).ToString("f0") + "%";
        int Damage = (int)(PictureCreater.instance.ListEnemyPicture[0].RoleMaxBlood - PictureCreater.instance.ListEnemyPicture[0].RoleNowBlood);
        DamageLabel.text = Damage.ToString("f0");
        if (((1 - PictureCreater.instance.ListEnemyPicture[0].RoleNowBlood / PictureCreater.instance.ListEnemyPicture[0].RoleMaxBlood) * 100) >= 100)
        {
            TruckLabel.text = "100%";
            if (PictureCreater.instance.ListEnemyPicture[0].RoleMaxBlood > 10000000)
            {
                DamageLabel.text = PictureCreater.instance.ListEnemyPicture[0].RoleMaxBlood.ToString("00000000");
            }
            else if (PictureCreater.instance.ListEnemyPicture[0].RoleMaxBlood > 100000000)
            {
                DamageLabel.text = PictureCreater.instance.ListEnemyPicture[0].RoleMaxBlood.ToString("000000000");
            }
            else
            {
                DamageLabel.text = PictureCreater.instance.ListEnemyPicture[0].RoleMaxBlood.ToString();
            }
        }
    }

    public void SetThousandInfo(string[] Rcvstring)
    {
        AwardString = Rcvstring;
        StartCoroutine(DelayShowItem(2));
        GoldLabel.text = "0";
        TruckLabel.text = ((1 - PictureCreater.instance.ListEnemyPicture[0].RoleNowBlood / PictureCreater.instance.ListEnemyPicture[0].RoleMaxBlood) * 100).ToString("f0") + "%";
        float Damage = (PictureCreater.instance.ListEnemyPicture[0].RoleMaxBlood - PictureCreater.instance.ListEnemyPicture[0].RoleNowBlood);
        DamageLabel.text = Damage.ToString("f0");
        if (((1 - PictureCreater.instance.ListEnemyPicture[0].RoleNowBlood / PictureCreater.instance.ListEnemyPicture[0].RoleMaxBlood) * 100) >= 100)
        {
            TruckLabel.text = "100%";
            if (PictureCreater.instance.ListEnemyPicture[0].RoleMaxBlood > 10000000)
            {
                DamageLabel.text = PictureCreater.instance.ListEnemyPicture[0].RoleMaxBlood.ToString("00000000");
            }
            else if (PictureCreater.instance.ListEnemyPicture[0].RoleMaxBlood > 100000000)
            {
                DamageLabel.text = PictureCreater.instance.ListEnemyPicture[0].RoleMaxBlood.ToString("000000000");
            }
            else
            {
                DamageLabel.text = PictureCreater.instance.ListEnemyPicture[0].RoleMaxBlood.ToString();
            }
        }
    }

    public void SetMilitaryInfo(string[] Rcvstring)
    {
        AwardString = Rcvstring;
        StartCoroutine(DelayShowItem(3));
    }

    public void SetWorldEventResult(string Rcvstring, int id)
    {
        AwardString = Rcvstring.Split('!');
        StartCoroutine(DelayShowItem(4));
        IsShowStar = 1;
        _StarNum = 3;
        MilitaryResult.transform.GetChild(0).GetComponent<UILabel>().text = TextTranslator.instance.GetWorldEventResultDesByID(id);
        MilitaryResult.transform.GetChild(0).localPosition = Vector3.zero;
        MilitaryResult.transform.GetChild(1).gameObject.SetActive(false);
    }


    void ShowStar()
    {
        StartCoroutine(ShowEffect());
    }

    IEnumerator UpdateComboid(int comboid)
    {
        combo.transform.Find("one").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        combo.transform.Find("two").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        combo.transform.Find("three").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        combo.transform.Find("four").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        combo.transform.Find("five").gameObject.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        combo.transform.Find("ClickNumber").gameObject.SetActive(true);
        int num = 0;
        while (num < comboid)
        {
            num += 1;
            yield return new WaitForSeconds(0.01f);
            if (num >= comboid)
            {
                num = comboid;
            }
            combo.transform.Find("ClickNumber").GetComponent<UILabel>().text = num.ToString();
        }

    }
    IEnumerator UpdateGold(int Gold)
    {
        int num = 0;
        while (num < Gold)
        {
            num += 1000;
            yield return new WaitForSeconds(0.01f);
            if (num >= Gold)
            {
                num = Gold;
            }
            GoldLabel.text = num.ToString();
        }

    }
    IEnumerator DelayShowItem(int id)
    {
        if (id == 4 || id == 3)
        {
            WinEffect.SetActive(true);
            yield return new WaitForSeconds(2f);
            WinEffect.SetActive(false);
        }
        else
        {
            combo.SetActive(true);
            StartCoroutine(UpdateComboid(GameObject.Find("FightWindow").GetComponent<FightWindow>().ClickNumber));
        }
        //if (((1 - PictureCreater.instance.ListEnemyPicture[0].RoleNowBlood / PictureCreater.instance.ListEnemyPicture[0].RoleMaxBlood) * 100) >= 100||id==3)
        //{
        //    _StarNum = 3;
        //}
        //else
        //{
        //    if (((1 - PictureCreater.instance.ListEnemyPicture[0].RoleNowBlood / PictureCreater.instance.ListEnemyPicture[0].RoleMaxBlood) * 100) < 51)
        //    {
        //        _StarNum = 1;
        //    }
        //    else
        //    {
        //        _StarNum = 2;
        //    }
        //    //LoseEffect.SetActive(true);
        //    //yield return new WaitForSeconds(2f);
        //    //LoseEffect.SetActive(false);
        //}
        //StartCoroutine(ShowEffect());
        if (IsShowStar == 1)
        {
            ShowStar();
        }
        if (id != 3)
        {
            transform.Find("BG").gameObject.SetActive(true);
        }
        yield return new WaitForSeconds(0.5f);
        if (id == 1 || id == 2)
        {
            HunterResult.SetActive(true);
        }
        else if (id == 4)
        {
            MilitaryResult.SetActive(true);
        }
        if (id == 1)
        {
            Award.SetActive(false);
        }
        else
        {
            Award.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            if (PictureCreater.instance.FightStyle != 17)
            {
                GameObject.Find("Award").transform.Find("AwardBG").gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < AwardString.Length - 1; i++)
            {
                string[] itemString = AwardString[i].Split('$');
                GameObject obj = Instantiate(AwardItemObj) as GameObject;
                obj.transform.parent = AwardGrid.transform;
                obj.SetActive(true);
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localScale = Vector3.one;
                SetAwardIcon(obj.transform.Find("AwardItem").gameObject, int.Parse(itemString[0]));
                //obj.transform.Find("AwardItem").GetComponent<UISprite>().spriteName = itemString[0];
                int iconid = int.Parse(itemString[0]);
                obj.transform.Find("ItemGrade").GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(iconid).itemGrade;
                obj.transform.Find("AwardNum").GetComponent<UILabel>().text = itemString[1];
                if (itemString[0] == "70017")
                {
                    NetworkHandler.instance.SendProcess(string.Format("7002#{0};{1};{2};{3}", 15, CharacterRecorder.instance.characterName, 0, 0));
                }
            }
            AwardGrid.GetComponent<UIGrid>().Reposition();
            yield return new WaitForSeconds(0.5f);
        }
        transform.Find("SureButton").gameObject.SetActive(true);
    }

    IEnumerator ShowEffect()
    {
        yield return new WaitForSeconds(2f);
        GameObject.Find("SpriteWin/SpriteStarblack1").GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        GameObject.Find("SpriteWin/SpriteStarblack2").GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        GameObject.Find("SpriteWin/SpriteStarblack3").GetComponent<UISprite>().color = new Color(255 / 255f, 255 / 255f, 255 / 255f, 255 / 255f);
        if (_StarNum == 1)
        {
            SpriteStar1.SetActive(true);
            AudioEditer.instance.PlayOneShot("ui_start");
            yield return new WaitForSeconds(0.5f);
        }
        else if (_StarNum == 2)
        {
            SpriteStar1.SetActive(true);
            AudioEditer.instance.PlayOneShot("ui_start");
            yield return new WaitForSeconds(0.5f);
            SpriteStar2.SetActive(true);
            AudioEditer.instance.PlayOneShot("ui_start");
            yield return new WaitForSeconds(0.5f);
        }
        else if (_StarNum == 3)
        {
            SpriteStar1.SetActive(true);
            AudioEditer.instance.PlayOneShot("ui_start");
            yield return new WaitForSeconds(0.5f);
            SpriteStar2.SetActive(true);
            AudioEditer.instance.PlayOneShot("ui_start");
            yield return new WaitForSeconds(0.5f);
            SpriteStar3.SetActive(true);
            AudioEditer.instance.PlayOneShot("ui_start");
            yield return new WaitForSeconds(0.5f);
        }
        else
        {
            Debug.Log("111111");
        }
    }

    void SetAwardIcon(GameObject go, int itemId)
    {
        if (itemId > 80000 && itemId < 90000)
        {
            go.GetComponent<UISprite>().atlas = ItemAtlas;
            go.GetComponent<UISprite>().spriteName = (itemId / 10 * 10 - 30000).ToString();//(int.Parse(awardSplit[0]) - 30000).ToString();
            go.transform.parent.Find("SpritePiece").gameObject.SetActive(true);
        }
        else if (itemId > 70000 && itemId < 79999)
        {
            go.GetComponent<UISprite>().atlas = RoleAtlas;
            go.GetComponent<UISprite>().spriteName = (itemId - 10000).ToString();
            go.transform.parent.Find("SpritePiece").gameObject.SetActive(true);
        }
        else if (itemId > 20000 && itemId < 30000)
        {
            go.GetComponent<UISprite>().atlas = ItemAtlas;
            go.GetComponent<UISprite>().spriteName = TextTranslator.instance.GetItemByItemCode(itemId).picID.ToString();
            go.transform.parent.Find("SpritePiece").gameObject.SetActive(false);
        }
        else
        {
            go.GetComponent<UISprite>().atlas = ItemAtlas;
            go.GetComponent<UISprite>().spriteName = itemId.ToString();
            go.transform.parent.Find("SpritePiece").gameObject.SetActive(false);
        }
    }
}
