using UnityEngine;
using System.Collections;

public class TowerFight : MonoBehaviour
{
    public GameObject FightWindow;
    public GameObject SkipWindow;
    public GameObject LeftObj;
    public GameObject CenterObj;
    public GameObject RightObj;
    private int CurFoor = 1;
    public bool isSkip = false;
    public GameObject Myself;

    // Use this for initialization
    void Start()
    {
        //if (CharacterRecorder.instance.GuideID[12] >= 8 && CharacterRecorder.instance.GuideID[12] < 15)
        //{
        //    CharacterRecorder.instance.GuideID[12] = 12;
        //    StartCoroutine(SceneTransformer.instance.NewbieGuide());
        //}
        if (GameObject.Find("WoodsTheExpendables") != null)
        {
            CurFoor = GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().CurFloor + 1;
        }

        if (UIEventListener.Get(FightWindow.transform.Find("Bg").gameObject).onClick == null)
        {
            UIEventListener.Get(FightWindow.transform.Find("Bg").gameObject).onClick += delegate(GameObject Obj)
            {
                if (GameObject.Find("WoodsTheExpendables") != null)
                {
                    GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().UpdateWindow(WoodsTheExpendablesWindowType.Right);
                }
            };
        }
        if (UIEventListener.Get(LeftObj.transform.Find("Button").gameObject).onClick == null)
        {
            UIEventListener.Get(LeftObj.transform.Find("Button").gameObject).onClick += delegate(GameObject obj)
            {
                PictureCreater.instance.StartWood(PictureCreater.instance.EasyWood, CurFoor, 1, int.Parse(LeftObj.transform.Find("FightLabel").Find("Label").GetComponent<UILabel>().text));
                UIManager.instance.OpenPanel("LoadingWindow", true);

            };
        }
        if (UIEventListener.Get(CenterObj.transform.Find("Button").gameObject).onClick == null)
        {
            UIEventListener.Get(CenterObj.transform.Find("Button").gameObject).onClick += delegate(GameObject obj)
            {

                PictureCreater.instance.StartWood(PictureCreater.instance.NormalWood, CurFoor, 2, int.Parse(CenterObj.transform.Find("FightLabel").Find("Label").GetComponent<UILabel>().text));
                UIManager.instance.OpenPanel("LoadingWindow", true);

            };
        }

        UIEventListener.Get(RightObj.transform.Find("Button").gameObject).onClick += delegate(GameObject obj)
        {
            if (CharacterRecorder.instance.GuideID[12] == 10)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
            PictureCreater.instance.StartWood(PictureCreater.instance.HardWood, CurFoor, 3, int.Parse(RightObj.transform.Find("FightLabel").Find("Label").GetComponent<UILabel>().text));
            UIManager.instance.OpenPanel("LoadingWindow", true);
        };

    }

   public IEnumerator  DelayShowItem(){
        yield return new WaitForSeconds(0.1f);
        LeftObj.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        CenterObj.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        RightObj.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Myself.SetActive(true);
        GameObject.Find("WoodsTheExpendablesMapList").GetComponent<WoodsTheExpendablesMapList>().isClickHero = false;
        FightWindow.transform.Find("Bg").GetComponent<BoxCollider>().enabled = true;
    }
    public void SetInfo()
    {
        if (GameObject.Find("WoodsTheExpendables") != null)
        {
            CurFoor = GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().CurFloor + 1;
        }

        string[] dataSplit1 = PictureCreater.instance.EasyWood.Split('!');
        string[] dataSplit2 = PictureCreater.instance.NormalWood.Split('!');
        string[] dataSplit3 = PictureCreater.instance.HardWood.Split('!');

        TowerData Tower = TextTranslator.instance.GetTowerByID(CurFoor);
        LeftObj.transform.Find("One").transform.Find("LabelNumber").GetComponent<UILabel>().text = Tower.GatePoint1.ToString();
        LeftObj.transform.Find("Two").transform.Find("LabelNumber").GetComponent<UILabel>().text = ((int)Tower.GateStarRate1).ToString();
        LeftObj.transform.Find("HeroIcon").gameObject.GetComponent<UITexture>().mainTexture = (Texture)Resources.Load("Loading/" + dataSplit1[2].Split('$')[0]);
        LeftObj.transform.Find("HeroIcon").gameObject.GetComponent<UITexture>().MakePixelPerfect();
        if (LeftObj.transform.Find("HeroIcon").gameObject.GetComponent<UITexture>().width > 256)
        {
            LeftObj.transform.Find("HeroIcon").gameObject.GetComponent<UITexture>().width = LeftObj.transform.Find("HeroIcon").gameObject.GetComponent<UITexture>().width * 2 / 3;
            LeftObj.transform.Find("HeroIcon").gameObject.GetComponent<UITexture>().height = LeftObj.transform.Find("HeroIcon").gameObject.GetComponent<UITexture>().height * 2 / 3;
        }
        LeftObj.transform.Find("Label").GetComponent<UILabel>().text = dataSplit1[0];
        LeftObj.transform.Find("FightLabel").Find("Label").GetComponent<UILabel>().text = dataSplit1[1];

        CenterObj.transform.Find("One").transform.Find("LabelNumber").GetComponent<UILabel>().text = Tower.GatePoint2.ToString();
        CenterObj.transform.Find("Two").transform.Find("LabelNumber").GetComponent<UILabel>().text = ((int)Tower.GateStarRate2).ToString();
        CenterObj.transform.Find("HeroIcon").gameObject.GetComponent<UITexture>().mainTexture = (Texture)Resources.Load("Loading/" + dataSplit2[2].Split('$')[0]);
        CenterObj.transform.Find("HeroIcon").gameObject.GetComponent<UITexture>().MakePixelPerfect();
        if (CenterObj.transform.Find("HeroIcon").gameObject.GetComponent<UITexture>().width > 256)
        {
            CenterObj.transform.Find("HeroIcon").gameObject.GetComponent<UITexture>().width = CenterObj.transform.Find("HeroIcon").gameObject.GetComponent<UITexture>().width * 2 / 3;
            CenterObj.transform.Find("HeroIcon").gameObject.GetComponent<UITexture>().height = CenterObj.transform.Find("HeroIcon").gameObject.GetComponent<UITexture>().height * 2 / 3;
        }
        CenterObj.transform.Find("Label").GetComponent<UILabel>().text = dataSplit2[0];
        CenterObj.transform.Find("FightLabel").Find("Label").GetComponent<UILabel>().text = dataSplit2[1];

        RightObj.transform.Find("One").transform.Find("LabelNumber").GetComponent<UILabel>().text = Tower.GatePoint3.ToString();
        RightObj.transform.Find("Two").transform.Find("LabelNumber").GetComponent<UILabel>().text = ((int)Tower.GateStarRate3).ToString();
        RightObj.transform.Find("HeroIcon").gameObject.GetComponent<UITexture>().mainTexture = (Texture)Resources.Load("Loading/" + dataSplit3[2].Split('$')[0]);
        RightObj.transform.Find("HeroIcon").gameObject.GetComponent<UITexture>().MakePixelPerfect();
        if (RightObj.transform.Find("HeroIcon").gameObject.GetComponent<UITexture>().width > 256)
        {
            RightObj.transform.Find("HeroIcon").gameObject.GetComponent<UITexture>().width = RightObj.transform.Find("HeroIcon").gameObject.GetComponent<UITexture>().width * 2 / 3;
            RightObj.transform.Find("HeroIcon").gameObject.GetComponent<UITexture>().height = RightObj.transform.Find("HeroIcon").gameObject.GetComponent<UITexture>().height * 2 / 3;
        }
        RightObj.transform.Find("Label").GetComponent<UILabel>().text = dataSplit3[0];
        RightObj.transform.Find("FightLabel").Find("Label").GetComponent<UILabel>().text = dataSplit3[1];

        int power=(int)(GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().BuffPower*100);
        Myself.transform.Find("FightLabel").Find("Label").GetComponent<UILabel>().text =(power/100).ToString("");
    }

    /// <summary>
    /// 自动刷野时选择第一个挑战对手
    /// </summary>
    public void NotClickButtonToFightWindow() 
    {
        //PictureCreater.instance.StartWood(PictureCreater.instance.EasyWood, CurFoor, 1, int.Parse(LeftObj.transform.Find("FightLabel").Find("Label").GetComponent<UILabel>().text));
        //UIManager.instance.OpenPanel("LoadingWindow", true);
        if (PlayerPrefs.GetInt("Automaticbrushfield" + CharacterRecorder.instance.userId.ToString() + PlayerPrefs.GetString("ServerID")) == 1)
        {
            //if (CharacterRecorder.instance.isSkip) 
            {
                //NetworkHandler.instance.SendProcess("1507#");
                //NetworkHandler.instance.SendProcess("1503#" + CurFoor + ";3;3;");
            }

            //GameObject.Find("WoodsTheExpendables").GetComponent<WoodsTheExpendables>().WoodObject.GetComponent<WoodsTheExpendablesMapList>().NotGetMouseToChooseWindow();
        }
    }
}
