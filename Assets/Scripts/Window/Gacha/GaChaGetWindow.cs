using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GaChaGetWindow : MonoBehaviour
{

    public GameObject SoloItem;
    public GameObject SoloItemBackMask;
    public UILabel CostLabel;
    public UILabel RemainLabel;
    public GameObject TryAgainButton;
    public GameObject ExitButton;
    public GameObject AwardItemGrid;
    public GameObject CostSprite;
    public UILabel MustRoleNum;
    public int NextRoleNum;

    public UIAtlas RoleAtlas;
    public UIAtlas ItemAtlas;

    public bool IsLock = false;

    List<GameObject> gachaItemList = new List<GameObject>();
    List<GameObject> HeroItemList = new List<GameObject>();//判断是否拥有英雄
    int _gachaType = 0;
    int realRoleId;
    // Use this for initialization
    void Start()
    {
        if (UIEventListener.Get(TryAgainButton).onClick == null)
        {
            UIEventListener.Get(TryAgainButton).onClick = delegate(GameObject go)
            {
                gachaItemList.Clear();
                HeroItemList.Clear();
                //TryAgainButton.GetComponent<BoxCollider>().size = Vector3.zero;
                //ExitButton.SetActive(false);
                ExitButton.SetActive(false);
                TryAgainButton.SetActive(false);
                //GameObject.Find("GachaWindow/TopContent/BackButton").GetComponent<BoxCollider>().size = Vector3.zero;
                if ((PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) != 2 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) != 6) ||
                   (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) != 4 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) != 6))
                {
                    if (SoloItem.activeSelf)
                    {
                        //SoloItemBackMask.SetActive(true); //kino
                        //SoloItem.transform.FindChild("ChouJiang_FanKa").gameObject.SetActive(false);
                        Invoke("BoxCollderSize", 2f);
                        NetworkHandler.instance.SendProcess("5201#" + _gachaType + ";");
                    }
                    else
                    {
                        //Invoke("BoxCollderSize", 5f);
                        NetworkHandler.instance.SendProcess("5202#" + _gachaType + ";");
                    }
                }
            };
        }

        //if (UIEventListener.Get(ExitButton).onClick == null)
        {
            UIEventListener.Get(ExitButton).onClick += delegate(GameObject go)
            {
                if ((PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) != 2 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) != 6) ||
                    (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) != 4 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) != 6))
                {
                    if ((PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 2 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 7) ||
                        (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 4 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 7)
                        )
                    {
                        SceneTransformer.instance.NewGuideButtonClick();
                    }
                    if (GameObject.Find("GachaWindow") != null)
                    {
                        GachaWindow GW = GameObject.Find("GachaWindow").GetComponent<GachaWindow>();
                        //if (GW.Tab1.activeSelf)
                        //{
                        //    GW.Tab1.transform.FindChild("LeftContent").FindChild("ChouJiangPingMu_01").gameObject.SetActive(true);
                        //    GW.Tab1.transform.FindChild("RightContent").FindChild("ChouJiangPingMu_02").gameObject.SetActive(true);
                        //}
                        //if (GW.Tab2.activeSelf)
                        //{
                        //    GW.Tab2.transform.FindChild("LeftContent").FindChild("ChouJiangPingMu_01").gameObject.SetActive(true);
                        //    GW.Tab2.transform.FindChild("RightContent").FindChild("ChouJiangPingMu_02").gameObject.SetActive(true);
                        //}
                        //UIManager.instance.OpenPanel("GachaWindow", true);

                        Debug.LogError("进入1");
                        NetworkHandler.instance.SendProcess("5204#;");
                        UIManager.instance.BackUI();
                    }
                    else if (CharacterRecorder.instance.isRedHeroWindowFirst)
                    {
                        //  NetworkHandler.instance.SendProcess("5204#;");


                        //UIManager.instance.BackUI();   //11.21  yy
                        //UIManager.instance.OpenSinglePanel("ActivityGachaRedHeroWindow", false);
                        //NetworkHandler.instance.SendProcess("9711#;");
                        //NetworkHandler.instance.SendProcess("9712#;");
                        //if (gameobject.find("activitygacharedherowindow") != null)
                        //{
                        //    destroyimmediate(gameobject.find("activitygacharedherowindow"));
                        //}

                        Debug.LogError("进入2");
                        CharacterRecorder.instance.isRedHeroWindowFirst = false;
                        UIManager.instance.BackUI(); 

                    }
                }

            };
        }

        //if (GameObject.Find("GachaWindow") != null)
        //{
        //    GachaWindow GW = GameObject.Find("GachaWindow").GetComponent<GachaWindow>();
        //    if (GW.Tab1.activeSelf)
        //    {
        //        GW.Tab1.transform.FindChild("LeftContent").FindChild("ChouJiangPingMu_01").gameObject.SetActive(false);
        //        GW.Tab1.transform.FindChild("RightContent").FindChild("ChouJiangPingMu_02").gameObject.SetActive(false);
        //    }
        //    if (GW.Tab2.activeSelf)
        //    {
        //        GW.Tab2.transform.FindChild("LeftContent").FindChild("ChouJiangPingMu_01").gameObject.SetActive(false);
        //        GW.Tab2.transform.FindChild("RightContent").FindChild("ChouJiangPingMu_02").gameObject.SetActive(false);
        //    }
        //}

        //TryAgainButton.GetComponent<BoxCollider>().size = Vector3.zero;
        ExitButton.SetActive(false);
        TryAgainButton.SetActive(false);
        //GameObject.Find("GachaWindow/TopContent/BackButton").GetComponent<BoxCollider>().size = Vector3.zero;
        if (SoloItem.activeSelf)
        {
            Invoke("BoxCollderSize", 2f);
        }
        else
        {
            //Invoke("BoxCollderSize", 5f);
        }
    }

    //void OnDestroy()
    //{
    //    if (GameObject.Find("GachaWindow") != null)
    //    {
    //        GachaWindow GW = GameObject.Find("GachaWindow").GetComponent<GachaWindow>();
    //        if (GW.Tab1.activeSelf)
    //        {
    //            GW.Tab1.transform.FindChild("LeftContent").FindChild("ChouJiangPingMu_01").gameObject.SetActive(true);
    //            GW.Tab1.transform.FindChild("RightContent").FindChild("ChouJiangPingMu_02").gameObject.SetActive(true);
    //        }
    //        if (GW.Tab2.activeSelf)
    //        {
    //            GW.Tab2.transform.FindChild("LeftContent").FindChild("ChouJiangPingMu_01").gameObject.SetActive(true);
    //            GW.Tab2.transform.FindChild("RightContent").FindChild("ChouJiangPingMu_02").gameObject.SetActive(true);
    //        }
    //    }
    //}

    public void BoxCollderSize()
    {
        //TryAgainButton.GetComponent<BoxCollider>().size = new Vector3(191, 78, 0);
        //ExitButton.SetActive(true);
        //GameObject.Find("GachaWindow/TopContent/BackButton").GetComponent<BoxCollider>().size = new Vector3(162, 82, 0);
        ExitButton.SetActive(true);
        TryAgainButton.SetActive(true);
    }

    public void SetSoloItem(Item _item, int _itemRank, int gachaType)
    {
        _gachaType = gachaType;
        if (_gachaType == 3)
        {
            _gachaType = 4;
        }
        else if (_gachaType == 2 || _gachaType == 6)
        {
            _gachaType = 7;
        }
        SoloItem.SetActive(true);
        SoloItem.transform.localPosition = new Vector3(0, -190, 0);
        SoloItem.transform.FindChild("LabelNum").GetComponent<UILabel>().text = _item.itemCount.ToString();
        SoloItemBackMask.SetActive(true);

        if (_item.itemCode > 60000 && _item.itemCode < 70000)
        {
            SoloItem.name = _item.itemCode.ToString();
            HeroItemList.Add(SoloItem);
        }
        if (_item.itemCode > 60000 && _item.itemCode < 65000)
        {
            //SetCardInfo(_item.itemCode);
            SoloItem.transform.FindChild("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
            SoloItem.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = _item.itemCode.ToString();
            SoloItem.transform.FindChild("Name").GetComponent<UILabel>().text = _item.itemName;
            SoloItem.transform.FindChild("SuiPian").gameObject.SetActive(false);

        }
        else if (_item.itemCode == 79999)
        {
            SoloItem.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            SoloItem.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = _item.itemCode.ToString();
            SoloItem.transform.FindChild("Name").GetComponent<UILabel>().text = _item.itemName;
            SoloItem.transform.FindChild("SuiPian").gameObject.SetActive(false);
        }
        else if (_item.itemCode == 70000)
        {
            SoloItem.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            SoloItem.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = _item.itemCode.ToString();
            SoloItem.transform.FindChild("Name").GetComponent<UILabel>().text = _item.itemName;
            SoloItem.transform.FindChild("SuiPian").gameObject.SetActive(false);
        }
        else if (_item.itemCode > 65000 && _item.itemCode < 70000)
        {
            SoloItem.transform.FindChild("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
            SoloItem.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = _item.itemCode.ToString();
            SoloItem.transform.FindChild("Name").GetComponent<UILabel>().text = _item.itemName;
            SoloItem.transform.FindChild("SuiPian").gameObject.SetActive(true);
        }
        else if (_item.itemCode > 70000 && _item.itemCode < 80000)
        {
            SoloItem.transform.FindChild("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
            SoloItem.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = (_item.itemCode - 10000).ToString();
            SoloItem.transform.FindChild("Name").GetComponent<UILabel>().text = _item.itemName;
            SoloItem.transform.FindChild("SuiPian").gameObject.SetActive(true);
        }
        else if (_item.itemCode > 40000 && _item.itemCode < 50000)
        {
            SoloItem.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            SoloItem.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = (_item.itemCode - 10000).ToString();
            SoloItem.transform.FindChild("Name").GetComponent<UILabel>().text = _item.itemName;
            SoloItem.transform.FindChild("SuiPian").gameObject.SetActive(true);
        }
        else if (_item.itemCode > 80000)
        {
            SoloItem.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            SoloItem.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = (_item.itemCode - 30000).ToString();
            SoloItem.transform.FindChild("Name").GetComponent<UILabel>().text = _item.itemName;
            SoloItem.transform.FindChild("SuiPian").gameObject.SetActive(true);
        }
        else
        {
            SoloItem.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            SoloItem.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = _item.itemCode.ToString();
            SoloItem.transform.FindChild("Name").GetComponent<UILabel>().text = _item.itemName;
            SoloItem.transform.FindChild("SuiPian").gameObject.SetActive(false);
        }
        SoloItem.transform.FindChild("Name").gameObject.SetActive(false);
        SetColor(SoloItem, _itemRank, _item.itemCode);
        TextTranslator.instance.ItemDescription(SoloItem, _item.itemCode, _item.itemCount);
        SoloItem.GetComponent<BoxCollider>().size = new Vector3(120f, 120f);
        if (SoloItem.GetComponent<TweenPosition>() == null)
        {
            SoloItem.AddComponent("TweenPosition");
        }
        TweenPosition TP = SoloItem.GetComponent<TweenPosition>();

        TP.from = new Vector3(0, -190, 0);
        TP.to = new Vector3(0, 80, 0);
        TP.duration = 0.5f;
        TP.ResetToBeginning();
        TP.PlayForward();

        if (SoloItem.GetComponent<TweenScale>() == null)
        {
            SoloItem.AddComponent("TweenScale");
        }
        TweenScale TS = SoloItem.GetComponent<TweenScale>();

        TS.from = Vector3.zero;
        TS.to = Vector3.one;
        TS.duration = 0.5f;
        TS.ResetToBeginning();
        TS.PlayForward();

        if (_gachaType == 1 || _gachaType == 3 || _gachaType == 4)
        {
            //GameObject.Find("GaChaGetWindow/Cost").GetComponent<UISprite>().spriteName = "90001";
            CostSprite.GetComponent<UISprite>().spriteName = "90001";
            //GameObject.Find("GaChaGetWindow/Remain").GetComponent<UISprite>().spriteName = "90001";
            CostLabel.text = "20000";
            //CostLabel.text = "2万";
            //if (CharacterRecorder.instance.gold >= 10000)
            //{
            //    int a = CharacterRecorder.instance.gold / 10000;
            //    int b = CharacterRecorder.instance.gold / 1000 % 10;
            //    RemainLabel.text = a.ToString() + "." + b.ToString() + "万";
            //}
            //else 
            //{
            //    RemainLabel.text = CharacterRecorder.instance.gold.ToString();
            //}
        }
        else if (_gachaType == 2 || _gachaType == 6 || _gachaType == 7)
        {
            //GameObject.Find("GaChaGetWindow/Cost").GetComponent<UISprite>().spriteName = "90002";
            CostSprite.GetComponent<UISprite>().spriteName = "90002";
            //GameObject.Find("GaChaGetWindow/Remain").GetComponent<UISprite>().spriteName = "90002";
            CostLabel.text = "240";
            //RemainLabel.text = CharacterRecorder.instance.lunaGem.ToString();
            //if (CharacterRecorder.instance.lunaGem >= 10000)
            //{
            //    int a = CharacterRecorder.instance.lunaGem / 10000;
            //    int b = CharacterRecorder.instance.lunaGem / 1000 % 10;
            //    RemainLabel.text = a.ToString() + "." + b.ToString() + "万";
            //}
            //else
            //{
            //    RemainLabel.text = CharacterRecorder.instance.lunaGem.ToString();
            //}
        }
        StartCoroutine(SetSOLORotation(_item.itemCode));
    }

    IEnumerator SetSOLORotation(int _RoleID)
    {
        yield return new WaitForSeconds(0.5f);
        if (!SoloItem.transform.FindChild("SuiPian").gameObject.activeSelf)
        {
            SoloItem.transform.FindChild("WF_ui_ZhuangBei 1").gameObject.SetActive(true);
        }
        else
        {
            SoloItem.transform.FindChild("WF_ui_ZhuangBei 1").gameObject.SetActive(false);
        }
        SoloItem.transform.FindChild("ChouJiang_FanKa").gameObject.SetActive(true);
        AudioEditer.instance.PlayOneShot("ui_lotteryshot");
        yield return new WaitForSeconds(0.1f);
        SoloItem.transform.parent.GetComponent<Animation>().Play("CardAnimation");

        yield return new WaitForSeconds(0.3f);
        if (SoloItem.GetComponent<TweenRotation>() == null)
        {
            SoloItem.AddComponent("TweenRotation");
        }
        TweenRotation TR = SoloItem.GetComponent<TweenRotation>();
        TR.from = Vector3.zero;
        TR.to = new Vector3(0, -180, 0);
        TR.duration = 0.2f;
        TR.ResetToBeginning();
        TR.PlayForward();

        yield return new WaitForSeconds(0.2f);
        SoloItem.transform.FindChild("Name").gameObject.SetActive(true);
        //SoloItem.transform.Rotate(new Vector3(0, 180, 0));
        SoloItemBackMask.SetActive(false);

        if (_RoleID > 60000 && _RoleID < 65000)
        {
            yield return new WaitForSeconds(0.2f);
            SetCardInfos(_RoleID);
        }
    }

    public void SetTenItem(Item _item, int _itemRank, int gachaType, int index)
    {
        _gachaType = gachaType;
        GameObject objj = NGUITools.AddChild(AwardItemGrid, SoloItem.transform.parent.gameObject);
        GameObject obj = objj.transform.FindChild("AwardItem").gameObject;
        //obj.GetComponent<ItemExplanation>().SetAwardItem(_item.itemCode, 0);
        obj.transform.localPosition = new Vector3(400, -350, 0);
        obj.transform.localScale = Vector3.zero;
        objj.SetActive(true);
        obj.SetActive(true);
        gachaItemList.Add(obj);
        obj.transform.FindChild("LabelNum").GetComponent<UILabel>().text = _item.itemCount.ToString();

        if (_item.itemCode > 60000 && _item.itemCode < 70000)//
        {
            obj.name = _item.itemCode.ToString();
            HeroItemList.Add(obj);
        }

        if (_item.itemCode > 60000 && _item.itemCode < 65000)
        {
            obj.transform.FindChild("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
            obj.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = _item.itemCode.ToString();
            obj.transform.FindChild("Name").GetComponent<UILabel>().text = _item.itemName;
            obj.transform.FindChild("SuiPian").gameObject.SetActive(false);

        }
        else if (_item.itemCode > 65000 && _item.itemCode < 70000)
        {
            obj.transform.FindChild("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
            obj.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = _item.itemCode.ToString();
            obj.transform.FindChild("Name").GetComponent<UILabel>().text = _item.itemName;
            obj.transform.FindChild("SuiPian").gameObject.SetActive(true);
        }
        else if (_item.itemCode > 40000 && _item.itemCode < 50000)
        {
            obj.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = (_item.itemCode - 10000).ToString();
            obj.transform.FindChild("Name").GetComponent<UILabel>().text = _item.itemName;
            obj.transform.FindChild("SuiPian").gameObject.SetActive(true);
        }
        else if (_item.itemCode > 80000)
        {
            obj.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = (_item.itemCode - 30000).ToString();
            obj.transform.FindChild("Name").GetComponent<UILabel>().text = _item.itemName;
            obj.transform.FindChild("SuiPian").gameObject.SetActive(true);
        }
        else if (_item.itemCode > 70000 && _item.itemCode < 79999)
        {
            obj.transform.FindChild("Icon").GetComponent<UISprite>().atlas = RoleAtlas;
            obj.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = (_item.itemCode - 10000).ToString();
            obj.transform.FindChild("Name").GetComponent<UILabel>().text = _item.itemName;
            obj.transform.FindChild("SuiPian").gameObject.SetActive(true);
        }
        else
        {
            obj.transform.FindChild("Icon").GetComponent<UISprite>().atlas = ItemAtlas;
            obj.transform.FindChild("Icon").GetComponent<UISprite>().spriteName = _item.itemCode.ToString();
            obj.transform.FindChild("Name").GetComponent<UILabel>().text = _item.itemName;
            obj.transform.FindChild("SuiPian").gameObject.SetActive(false);
        }
        obj.transform.FindChild("Name").gameObject.SetActive(false);
        SetColor(obj, _itemRank, _item.itemCode);
        TextTranslator.instance.ItemDescription(obj, _item.itemCode, _item.itemCount);
        obj.GetComponent<BoxCollider>().size = new Vector3(120f, 120f);
        obj.AddComponent("TweenPosition");
        TweenPosition TP = obj.GetComponent<TweenPosition>();
        TP.ResetToBeginning();
        TP.from = new Vector3(400, -350, 0);
        if (index >= 5)
        {
            TP.to = new Vector3(0 + (index - 5) * 200, -300, 0);
        }
        else
        {
            TP.to = new Vector3(0 + index * 200, -100, 0);
        }
        TP.duration = 0.5f;
        TP.PlayForward();

        obj.AddComponent("TweenScale");
        TweenScale TS = obj.GetComponent<TweenScale>();
        TS.ResetToBeginning();
        TS.from = Vector3.zero;
        TS.to = Vector3.one;
        TS.duration = 0.5f;
        TS.PlayForward();

        if (_gachaType == 5)
        {
            //GameObject.Find("GaChaGetWindow/Cost").GetComponent<UISprite>().spriteName = "90001";
            CostSprite.GetComponent<UISprite>().spriteName = "90001";
            //GameObject.Find("GaChaGetWindow/Remain").GetComponent<UISprite>().spriteName = "90001";
            CostLabel.text = "180000";
            //RemainLabel.text = CharacterRecorder.instance.gold.ToString();
            //CostLabel.text = "18万";
            //if (CharacterRecorder.instance.gold >= 10000)
            //{
            //    int a = CharacterRecorder.instance.gold / 10000;
            //    int b = CharacterRecorder.instance.gold / 1000 % 10;
            //    RemainLabel.text = a.ToString() + "." + b.ToString() + "万";
            //}
            //else
            //{
            //    RemainLabel.text = CharacterRecorder.instance.gold.ToString();
            //}
        }
        else if (_gachaType == 8)
        {
            //GameObject.Find("GaChaGetWindow/Cost").GetComponent<UISprite>().spriteName = "90002";
            CostSprite.GetComponent<UISprite>().spriteName = "90002";
            //GameObject.Find("GaChaGetWindow/Remain").GetComponent<UISprite>().spriteName = "90002";
            CostLabel.text = "2280";
            //RemainLabel.text = CharacterRecorder.instance.lunaGem.ToString();
            //if (CharacterRecorder.instance.lunaGem >= 10000)
            //{
            //    int a = CharacterRecorder.instance.lunaGem / 10000;
            //    int b = CharacterRecorder.instance.lunaGem / 1000 % 10;
            //    RemainLabel.text = a.ToString() + "." + b.ToString() + "万";
            //}
            //else
            //{
            //    RemainLabel.text = CharacterRecorder.instance.lunaGem.ToString();
            //}
        }

        realRoleId = _item.itemCode;
        if (index >= 9)
        {
            StartCoroutine(SetTenRotation());
            StartCoroutine(SetTenRotationShot());
        }
    }

    public void SetCardInfos(int _RoleID)
    {
        UIManager.instance.OpenSinglePanel("CardWindow", false);
        GameObject.Find("CardWindow").GetComponent<CardWindow>().SetCardInfo(_RoleID);
    }

    IEnumerator SetTenRotationShot()
    {
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < gachaItemList.Count; i++)
        {
            GameObject go = gachaItemList[i];
            if (!go.transform.FindChild("SuiPian").gameObject.activeSelf)
            {
                //go.transform.FindChild("ChouJiang_HaoKa").gameObject.SetActive(true);
                go.transform.FindChild("WF_ui_ZhuangBei 1").gameObject.SetActive(true);
            }
            go.transform.FindChild("ChouJiang_FanKa").gameObject.SetActive(true);
            AudioEditer.instance.PlayOneShot("ui_lotteryshot");
            yield return new WaitForSeconds(0.3f);
            go.transform.parent.GetComponent<Animation>().Play("CardAnimation");
        }

    }
    IEnumerator SetTenRotation()
    {
        yield return new WaitForSeconds(0.7f);
        for (int i = 0; i < gachaItemList.Count; i++)
        {
            GameObject go = gachaItemList[i];
            go.AddComponent("TweenRotation");
            TweenRotation TR = go.GetComponent<TweenRotation>();
            TR.from = Vector3.zero;
            TR.to = new Vector3(0, -180, 0);
            TR.duration = 0.3f;
            TR.ResetToBeginning();
            TR.PlayForward();

            yield return new WaitForSeconds(0.3f);
            //AudioEditer.instance.PlayOneShot("ui_lotteryshot");
            go.transform.FindChild("Name").gameObject.SetActive(true);
            //go.transform.Rotate(new Vector3(0, 180, 0));
            go.transform.FindChild("BackMask").gameObject.SetActive(false);

            int roleID = int.Parse(gachaItemList[i].transform.FindChild("Icon").GetComponent<UISprite>().spriteName);
            if (roleID > 60000 && roleID < 65000)
            {
                bool isSuiPsian = false;
                if (gachaItemList[i].transform.FindChild("SuiPian") != null)
                {
                    if (gachaItemList[i].transform.FindChild("SuiPian").gameObject.activeSelf)
                    {
                        isSuiPsian = true;
                    }
                }
                if (!isSuiPsian)
                {
                    yield return new WaitForSeconds(0.2f);
                    SetCardInfos(roleID);

                    IsLock = true;
                }
            }
            while (IsLock)
            {
                yield return new WaitForSeconds(0.3f);
            }

            if (i >= 9) //再来一次按钮显示
            {
                BoxCollderSize();
            }
        }
    }


    public void Reset()
    {
        SoloItem.SetActive(false);
        gachaItemList.Clear();
        int count = AwardItemGrid.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            GameObject go = AwardItemGrid.transform.GetChild(0).gameObject;
            DestroyImmediate(go);
        }
    }

    void SetColor(GameObject obj, int number, int itemCode)
    {
        switch (number)
        {
            case 1:
                obj.GetComponent<UISprite>().spriteName = "Grade1";
                break;
            case 2:
                obj.GetComponent<UISprite>().spriteName = "Grade2";
                break;
            case 3:
                obj.GetComponent<UISprite>().spriteName = "Grade3";
                break;
            case 4:
                obj.GetComponent<UISprite>().spriteName = "Grade4";
                break;
            case 5:
                obj.GetComponent<UISprite>().spriteName = "Grade5";
                break;
            case 6:
                obj.GetComponent<UISprite>().spriteName = "Grade6";
                break;
            case 7:
                obj.GetComponent<UISprite>().spriteName = "Grade7";
                break;
            default:
                break;
        }
        if (itemCode > 65000 && itemCode < 90000)
        {
            obj.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(itemCode).itemGrade.ToString();
        }
        else if (itemCode > 40000 && itemCode < 50000)
        {
            obj.GetComponent<UISprite>().spriteName = "Grade" + TextTranslator.instance.GetItemByItemCode(itemCode).itemGrade.ToString();
        }
    }

    public void SetMustRoleNum()
    {
        if (MustRoleNum.gameObject.activeSelf)
        {
            NextRoleNum = CharacterRecorder.instance.GachaLotteryNum;
            if (NextRoleNum == 9)
            {
                MustRoleNum.text = "本次必得英雄";
            }
            else
            {
                MustRoleNum.text = (10 - NextRoleNum).ToString() + "次后必得英雄";
            }
        }
    }

    //public void SetMustRole() 
    //{
    //    if (MustRoleNum.gameObject.activeSelf) 
    //    {
    //        if(NextRoleNum == 0)
    //        {
    //            NextRoleNum=10;
    //        }
    //        else
    //        {
    //            NextRoleNum-=1;
    //        }
    //        if (NextRoleNum == 0)
    //        {
    //            MustRoleNum.text = "本次后必得英雄";
    //        }
    //        else 
    //        {
    //            MustRoleNum.text = NextRoleNum + "次后必得英雄";
    //        }
    //    }
    //}

    public void SetHeroSuipian2(int HeroId)
    {
        if (HeroItemList.Count > 0)
        {
            for (int i = 0; i < HeroItemList.Count; i++)
            {
                //int HeroId = int.Parse(HeroItemList[i].name);
                if (HeroId == int.Parse(HeroItemList[i].name))
                {
                    int heroRarity = TextTranslator.instance.GetHeroInfoByHeroID(HeroId).heroRarity;
                    if (heroRarity == 2)
                    {
                        HeroItemList[i].transform.Find("LabelNum").GetComponent<UILabel>().text = "16";
                    }
                    else if (heroRarity == 3)
                    {
                        HeroItemList[i].transform.Find("LabelNum").GetComponent<UILabel>().text = "32";
                    }
                    else if (heroRarity == 4)
                    {
                        HeroItemList[i].transform.Find("LabelNum").GetComponent<UILabel>().text = "64";
                    }
                    else if (heroRarity == 5)
                    {
                        HeroItemList[i].transform.Find("LabelNum").GetComponent<UILabel>().text = "120";
                    }
                    HeroItemList[i].transform.Find("SuiPian").gameObject.SetActive(true);
                    Debug.LogError(HeroId + "  ****   ");
                }
            }
        }

        //CharacterRecorder.instance.heroIdList.Clear();
        //foreach (var heroItem in CharacterRecorder.instance.ownedHeroList)
        //{
        //    CharacterRecorder.instance.heroIdList.Add(heroItem.cardID);
        //}
        //CharacterRecorder.instance.ownedHeroBeforeList = CharacterRecorder.instance.ownedHeroList;
    }


    public void SetHeroSuipian()
    {
        if (HeroItemList.Count > 0)
        {
            for (int i = 0; i < HeroItemList.Count; i++)
            {
                int HeroId = int.Parse(HeroItemList[i].name);
                for (int j = 0; j < CharacterRecorder.instance.heroIdList.Count; j++)
                {
                    if (CharacterRecorder.instance.heroIdList[j] == HeroId)
                    {
                        int heroRarity = TextTranslator.instance.GetHeroInfoByHeroID(HeroId).heroRarity;
                        if (heroRarity == 2)
                        {
                            HeroItemList[i].transform.Find("LabelNum").GetComponent<UILabel>().text = "16";
                        }
                        else if (heroRarity == 3)
                        {
                            HeroItemList[i].transform.Find("LabelNum").GetComponent<UILabel>().text = "32";
                        }
                        else if (heroRarity == 4)
                        {
                            HeroItemList[i].transform.Find("LabelNum").GetComponent<UILabel>().text = "64";
                        }
                        else if (heroRarity == 5)
                        {
                            HeroItemList[i].transform.Find("LabelNum").GetComponent<UILabel>().text = "120";
                        }
                        HeroItemList[i].transform.Find("SuiPian").gameObject.SetActive(true);
                        Debug.LogError(HeroId + "  ****   ");
                        return;
                    }
                }
            }
        }

        CharacterRecorder.instance.heroIdList.Clear();
        foreach (var heroItem in CharacterRecorder.instance.ownedHeroList)
        {
            CharacterRecorder.instance.heroIdList.Add(heroItem.cardID);
        }
        //CharacterRecorder.instance.ownedHeroBeforeList = CharacterRecorder.instance.ownedHeroList;
    }
}
