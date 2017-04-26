using UnityEngine;
using System.Collections;

public class CardWindow : MonoBehaviour {

    float StayTime = 5.0f;
    //public UISprite SpriteJunXian;
    public UISprite SpriteRare;
    public UITexture HeroSprite;
    public UISprite SpriteCareer;
    public UITexture BGSprite;
    public UITexture FrameSprite;

    int RoleID;
    int LoadingSize = 13;
    void Start()
    {
        if (GameObject.Find("FirstRechargeWindow") != null)
        {
            DestroyImmediate(GameObject.Find("FirstRechargeWindow"));
        }
        if (GameObject.Find("LoginSignWindow") != null)
        {
            DestroyImmediate(GameObject.Find("LoginSignWindow"));
        }
        if (UIEventListener.Get(this.gameObject).onClick == null)
        {
            UIEventListener.Get(this.gameObject).onClick = delegate(GameObject go)
            {
                //UIManager.instance.BackUI();
                //if (GameObject.Find("GaChaGetWindow") != null)
                //{
                //    UIManager.instance.OpenPanel("CardIntroduceWindow", false);
                //}
                //else
                //{
                //    UIManager.instance.OpenPanel("CardIntroduceWindow", true);
                //}
                //GameObject.Find("CardIntroduceWindow").GetComponent<CardIntroduceWindow>().SetIntroduceInfo(RoleID);
                ThisGameObjClick(go);
            };
        }
        GameObject _mainWindow = GameObject.Find("MainWindow");
        if(_mainWindow != null)
        {
            //_mainWindow.SetActive(false);
            CharacterRecorder.instance.isGaChaFromMainWindow = true;
            DestroyImmediate(_mainWindow);  
        }
    }

    void ThisGameObjClick(GameObject go)
    {
        if (GameObject.Find("GaChaGetWindow") != null)
        {
            UIManager.instance.OpenSinglePanel("CardIntroduceWindow", false);
        }
        else
        {
            UIManager.instance.OpenSinglePanel("CardIntroduceWindow", false);
        }
        GameObject.Find("CardIntroduceWindow").GetComponent<CardIntroduceWindow>().SetIntroduceInfo(RoleID);
    }
    public void SetCardInfo(int _RoleID)
    {
        if(_RoleID == 60026)
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_502);
        }
        else if(_RoleID == 60002)
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_602);
        }
        else if (_RoleID == 60016)
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_804);
        }
        else if (_RoleID == 60028)
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_1404);
        }
        RoleID = _RoleID;
        HeroInfo mheroInfo = TextTranslator.instance.GetHeroInfoByHeroID(_RoleID);
        SetHeroType(mheroInfo.heroCarrerType);
        SetRarityIcon(mheroInfo.heroRarity);
        SetFrame(mheroInfo.heroRarity);
        HeroSprite.mainTexture = Resources.Load("Loading/" + _RoleID.ToString()) as Texture;
        HeroSprite.GetComponent<UITexture>().MakePixelPerfect();
        HeroSprite.GetComponent<UITexture>().width = HeroSprite.GetComponent<UITexture>().width / 20 * LoadingSize;
        HeroSprite.GetComponent<UITexture>().height = HeroSprite.GetComponent<UITexture>().height / 20 * LoadingSize;
        
        SpriteCareer.transform.FindChild("LabelName").GetComponent<UILabel>().text = mheroInfo.heroName;
        InvokeRepeating("OpenNextWindow", 0, 1.0f);
    }

    //设置英雄职业
    void SetHeroType(int _herotype)
    {
        if (_herotype == 1 || _herotype == 2 || _herotype == 3)
        {
            SpriteCareer.spriteName = string.Format("heroType{0}", _herotype);
        }
        else
        {
            Debug.LogError("英雄职业类型参数错误" + _herotype);
        }
    }
    //设置稀有度
    void SetRarityIcon(int _heroRarity)
    {
        switch (_heroRarity)
        {
            case 1:
                SpriteRare.spriteName = "word4";
                break;
            case 2:
                SpriteRare.spriteName = "word5";
                break;
            case 3:
                SpriteRare.spriteName = "word6";
                break;
            case 4:
                SpriteRare.spriteName = "word7";
                break;
            case 5:
                SpriteRare.spriteName = "word8";
                break;
            case 6:
                SpriteRare.spriteName = "word9";
                break;
            default:
                break;
        }
    }
    void SetFrame(int _heroRarity)
    {
        string bgName = null;
        string frameName = null;
        switch (_heroRarity)
        {
            case 1:
                bgName = "carddi0";
                frameName = "kuang0";
                break;
            case 2:
                bgName = "carddi1";
                frameName = "kuang1";
                break;
            case 3:
                bgName = "carddi2";
                frameName = "kuang2";
                break;
            case 4:
                bgName = "carddi3";
                frameName = "kuang3";
                break;
            case 5:
                bgName = "carddi4";
                frameName = "kuang4";
                break;
        }
        FrameSprite.mainTexture = Resources.Load("Game/"+frameName) as Texture;
        BGSprite.mainTexture = Resources.Load("Game/" + bgName) as Texture;
    }

    void OpenNextWindow()
    {
        StayTime-=1;
        if(StayTime <= 0)
        {
            Debug.Log("自动调用下一窗口：" +StayTime);
            StartCoroutine(openNextWindow());
            //CancelInvoke("OpenNextWindow");
        }
    }
    IEnumerator openNextWindow()
    {
        yield return new WaitForSeconds(0.1f);
        if (this.gameObject != null)
        {
            ThisGameObjClick(this.gameObject);
        }
        CancelInvoke();
        yield return 0;
    }
}
