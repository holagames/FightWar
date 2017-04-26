using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GateInfoWindow : MonoBehaviour
{
    public GameObject SpriteStar1;
    public GameObject SpriteStar2;
    public GameObject SpriteStar3;

    public GameObject uiGrid;
    public GameObject GateAwardItem;

    public GameObject LabelDesc;
    public GameObject LabelCount;
    public GameObject LabelName;

    public UIAtlas ItemAtlas;
    public UIAtlas RoleAtlas;

    float[] SimpleStar = new float[6];
    float[] MasterStar = new float[6];
    float[] ChallengeStar = new float[6];

    int GateID;
    int Index;
    int Tab;
    // Use this for initialization
    void Start()
    {
        if (UIEventListener.Get(GameObject.Find("CloseButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("CloseButton")).onClick += delegate(GameObject go)
            {
                Debug.Log("AAA");
                UIManager.instance.BackUI();
            };
        }

        if (UIEventListener.Get(GameObject.Find("Tab1Button")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("Tab1Button")).onClick += delegate(GameObject go)
            {
                Debug.Log("AAA");
                SetTab(0);
            };
        }

        if (UIEventListener.Get(GameObject.Find("Tab2Button")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("Tab2Button")).onClick += delegate(GameObject go)
            {
                if (GameObject.Find("Tab2Button").GetComponent<UIButton>().isActiveAndEnabled)
                {
                    Debug.Log("AAA");
                    SetTab(1);
                }
            };
        }

        if (UIEventListener.Get(GameObject.Find("Tab3Button")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("Tab3Button")).onClick += delegate(GameObject go)
            {
                if (GameObject.Find("Tab3Button").GetComponent<UIButton>().isActiveAndEnabled)
                {
                    Debug.Log("AAA");
                    SetTab(2);
                }
            };
        }

        if (UIEventListener.Get(GameObject.Find("RushButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("RushButton")).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("2011#" + GateID + ";");
                //UIManager.instance.OpenPromptWindow("即将开放", PromptWindow.PromptType.Alert, null, null);
            };
        }

        if (UIEventListener.Get(GameObject.Find("Rush10Button")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("Rush10Button")).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("2012#" + GateID + ";");
                //UIManager.instance.OpenPromptWindow("即将开放", PromptWindow.PromptType.Alert, null, null);
            };
        }

        if (UIEventListener.Get(GameObject.Find("StartButton")).onClick == null)
        {
            Debug.Log("BBBB");
            UIEventListener.Get(GameObject.Find("StartButton")).onClick += delegate(GameObject go)
            {
                Debug.Log("AAA");
                UIManager.instance.OpenPanel("LoadingWindow", true);
                PictureCreater.instance.StartFight();
            };
        }
    }

    public void Init(int _GateID, int _Index, float[] _SimpleStar, float[] _MasterStar, float[] _ChallengeStar)
    {
        SimpleStar = _SimpleStar;
        MasterStar = _MasterStar;
        ChallengeStar = _ChallengeStar;
        GateID = _GateID;
        Index = _Index;

        if (SimpleStar[Index] == 0)
        {
            SetTab(0);
            GameObject.Find("Tab1Button").GetComponent<UIToggle>().value = true;

            GameObject.Find("Tab2Button").GetComponent<UIButton>().isEnabled = false;
            GameObject.Find("Tab2Button").GetComponent<UIButton>().state = UIButtonColor.State.Disabled;

            GameObject.Find("Tab3Button").GetComponent<UIButton>().isEnabled = false;
            GameObject.Find("Tab3Button").GetComponent<UIButton>().state = UIButtonColor.State.Disabled;
        }
        else if (MasterStar[Index] == 0)
        {
            GameObject.Find("Tab3Button").GetComponent<UIButton>().isEnabled = false;
            GameObject.Find("Tab3Button").GetComponent<UIButton>().state = UIButtonColor.State.Disabled;

            for (int i = 0; i < Index; i++)
            {
                if (MasterStar[i] == 0)
                {
                    SetTab(0);
                    GameObject.Find("Tab1Button").GetComponent<UIToggle>().value = true;

                    GameObject.Find("Tab2Button").GetComponent<UIButton>().isEnabled = false;
                    GameObject.Find("Tab2Button").GetComponent<UIButton>().state = UIButtonColor.State.Disabled;

                    return;
                }
            }

            SetTab(1);
            GameObject.Find("Tab2Button").GetComponent<UIToggle>().value = true;
        }
        else
        {
            for (int i = 0; i < Index; i++)
            {
                if (_ChallengeStar[i] == 0)
                {
                    SetTab(1);
                    GameObject.Find("Tab2Button").GetComponent<UIToggle>().value = true;

                    GameObject.Find("Tab3Button").GetComponent<UIButton>().isEnabled = false;
                    GameObject.Find("Tab3Button").GetComponent<UIButton>().state = UIButtonColor.State.Disabled;

                    return;
                }
            }

            SetTab(2);
            GameObject.Find("Tab3Button").GetComponent<UIToggle>().value = true;
        }
    }

    void SetTab(int _Tab)
    {
        int count = uiGrid.transform.childCount;
        for (int i = 0; i < count; i++)
        {
            GameObject go = uiGrid.transform.GetChild(0).gameObject;
            DestroyImmediate(go);
        }
        Tab = _Tab;
        SpriteStar1.GetComponent<UISprite>().fillAmount = SimpleStar[Index];
        SpriteStar2.GetComponent<UISprite>().fillAmount = MasterStar[Index];
        SpriteStar3.GetComponent<UISprite>().fillAmount = ChallengeStar[Index];

        //Debug.LogError("AAAAAAAAAAAA" + GateID);
        TextTranslator.Gate gate = TextTranslator.instance.GetGateByID(GateID + Tab * 10000);
        SceneTransformer.instance.NowGateID = GateID + Tab * 10000;

        if (gate.itemID1 != 0)
        {
            TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(gate.itemID1);
            GameObject go = NGUITools.AddChild(uiGrid, GateAwardItem);
            go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            SetColor(go, mitemInfo.itemGrade);
            if (gate.itemID1 > 40000 && gate.itemID1 < 50000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID1 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID1 > 80000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID1 - 30000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID1 > 70000 && gate.itemID1 < 79999)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID1 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = gate.itemID1.ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(false);
            }
            go.transform.Find("LabelName").gameObject.GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(gate.itemID1);
        }

        if (gate.itemID2 != 0)
        {
            TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(gate.itemID2);
            GameObject go = NGUITools.AddChild(uiGrid, GateAwardItem);
            go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            SetColor(go, mitemInfo.itemGrade);
            if (gate.itemID2 > 40000 && gate.itemID2 < 50000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID2 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID2 > 80000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID2 - 30000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID2 > 70000 && gate.itemID2 < 79999)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID2 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = gate.itemID2.ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(false);
            }
            go.transform.Find("LabelName").gameObject.GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(gate.itemID2);
        }

        if (gate.itemID3 != 0)
        {
            TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(gate.itemID3);
            GameObject go = NGUITools.AddChild(uiGrid, GateAwardItem);
            go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            SetColor(go, mitemInfo.itemGrade);
            if (gate.itemID3 > 40000 && gate.itemID3 < 50000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID3 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID3 > 80000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID3 - 30000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID3 > 70000 && gate.itemID3 < 79999)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID3 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = gate.itemID3.ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(false);
            }
            go.transform.Find("LabelName").gameObject.GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(gate.itemID3);
        }

        if (gate.itemID4 != 0)
        {
            TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(gate.itemID4);
            GameObject go = NGUITools.AddChild(uiGrid, GateAwardItem);
            go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            SetColor(go, mitemInfo.itemGrade);
            if (gate.itemID4 > 40000 && gate.itemID4 < 50000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID4 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID4 > 80000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID4 - 30000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID4 > 70000 && gate.itemID4 < 79999)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID4 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = gate.itemID4.ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(false);
            }
            go.transform.Find("LabelName").gameObject.GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(gate.itemID4);
        }

        if (gate.itemID5 != 0)
        {
            TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(gate.itemID5);
            GameObject go = NGUITools.AddChild(uiGrid, GateAwardItem);
            go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            SetColor(go, mitemInfo.itemGrade);
            if (gate.itemID5 > 40000 && gate.itemID5 < 50000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID5 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID5 > 80000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID5 - 30000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID5 > 70000 && gate.itemID5 < 79999)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID5 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = gate.itemID5.ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(false);
            }
            go.transform.Find("LabelName").gameObject.GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(gate.itemID5);
        }

        if (gate.itemID6 != 0)
        {
            TextTranslator.ItemInfo mitemInfo = TextTranslator.instance.GetItemByItemCode(gate.itemID6);
            GameObject go = NGUITools.AddChild(uiGrid, GateAwardItem);
            go.transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
            SetColor(go, mitemInfo.itemGrade);
            if (gate.itemID6 > 40000 && gate.itemID6 < 50000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID6 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID6 > 80000)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID6 - 30000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else if (gate.itemID6 > 70000 && gate.itemID6 < 79999)
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = RoleAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = (gate.itemID6 - 10000).ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(true);
            }
            else
            {
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().atlas = ItemAtlas;
                go.transform.Find("SpriteEquip").gameObject.GetComponent<UISprite>().spriteName = gate.itemID6.ToString();
                go.transform.Find("SuiPian").gameObject.SetActive(false);
            }
            go.transform.Find("LabelName").gameObject.GetComponent<UILabel>().text = TextTranslator.instance.GetItemNameByItemCode(gate.itemID6);
        }

        uiGrid.GetComponent<UIGrid>().Reposition();
        LabelDesc.GetComponent<UILabel>().text = gate.description;
        LabelCount.GetComponent<UILabel>().text = "";
        LabelName.GetComponent<UILabel>().text = gate.name;
    }

    void SetColor(GameObject go, int color)
    {
        switch (color)
        {
            case 1:
                go.GetComponent<UISprite>().spriteName = "Grade2";
                break;
            case 2:
                go.GetComponent<UISprite>().spriteName = "Grade3";
                break;
            case 3:
                go.GetComponent<UISprite>().spriteName = "Grade4";
                break;
            case 4:
                go.GetComponent<UISprite>().spriteName = "Grade5";
                break;
            //case 5:
            //    go.GetComponent<UISprite>().spriteName = "Grade5";
            //    break;
        }
    }
}
