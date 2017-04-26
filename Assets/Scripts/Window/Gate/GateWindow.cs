using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GateWindow : MonoBehaviour
{
    public int Chapter = 0;

    public GameObject LabelGate;

    public GameObject PreviousButton;
    public GameObject NextButton;
    public List<GameObject> ChapterList = new List<GameObject>();
    public List<GameObject> mGateList = new List<GameObject>();
    public UICenterOnChild uiSV;

    public GameObject box1;
    public GameObject box2;
    public GameObject box3;
    public UILabel AllStarNumber;
    public UISlider StarSlider;

    List<TextTranslator.Gate> ListGate = new List<TextTranslator.Gate>();

    float[] SimpleStar = new float[6];
    float[] MasterStar = new float[6];
    float[] ChallengeStar = new float[6];

    public bool IsJump = false;

    Vector3 firstPosition = Vector3.zero;
    int isOpenUpdate = -1;
    float speed = 0.008f;
    GameObject curGo;

    int SelectGateNumber = 0;
    int SelectChapterNumber = 0;
    // Use this for initialization
    void OnEnable()
    {
        NetworkHandler.instance.SendProcess("2013#;");
        SetChapter(SceneTransformer.instance.NowChapterID);
        //if (!IsJump)
        {
            Invoke("SetMovePart", 0.5f);
        }
    }

    void SetMovePart()
    {
        if (!IsJump)
        {
            Transform tf = ChapterList[SceneTransformer.instance.NowChapterID - 1].transform.parent.transform;
            uiSV.CenterOn(tf);

        }
        else
        {
            Transform tf = ChapterList[Chapter - 1].transform.parent.transform;
            uiSV.CenterOn(tf);
            IsJump = false;
        }
    }

    void Start()
    {
        if (UIEventListener.Get(GameObject.Find("BackButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("BackButton")).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPanel("MainWindow", true);
            };
        }

        //if (UIEventListener.Get(GameObject.Find("NextButton")).onClick == null)
        //{
        //    UIEventListener.Get(GameObject.Find("NextButton")).onClick += delegate(GameObject go)
        //    {
        //        if (TextTranslator.instance.listChapter.Count > SceneTransformer.instance.NowChapterID)
        //        {
        //            SceneTransformer.instance.NowChapterID++;
        //            SetChapter(SceneTransformer.instance.NowChapterID);
        //            NetworkHandler.instance.SendProcess("2001#" + SceneTransformer.instance.NowChapterID + ";");
        //        }
        //    };
        //}

        //if (UIEventListener.Get(GameObject.Find("PreviousButton")).onClick == null)
        //{
        //    UIEventListener.Get(GameObject.Find("PreviousButton")).onClick += delegate(GameObject go)
        //    {
        //        if (SceneTransformer.instance.NowChapterID > 1)
        //        {
        //            SceneTransformer.instance.NowChapterID--;
        //            SetChapter(SceneTransformer.instance.NowChapterID);
        //            NetworkHandler.instance.SendProcess("2001#" + SceneTransformer.instance.NowChapterID + ";");
        //        }
        //    };
        //}

        //if (UIEventListener.Get(GameObject.Find("ChapterButton")).onClick == null)
        //{
        //    UIEventListener.Get(GameObject.Find("ChapterButton")).onClick += delegate(GameObject go)
        //    {
        //        Debug.Log("BBBB");
        //        UIManager.instance.OpenPanel("GateChapterWindow", false);
        //    };
        //}

        UIEventListener.Get(GameObject.Find("C1")).onPress += delegate(GameObject go, bool isPress)
        {
            if (!isPress)
            {
                SetMoveChapter();
            }
        };
        UIEventListener.Get(GameObject.Find("C1")).onClick += delegate(GameObject go)
        {
            SceneTransformer.instance.NowChapterID = 1;
            SetChapter(SceneTransformer.instance.NowChapterID);
        };
        UIEventListener.Get(GameObject.Find("C2")).onPress += delegate(GameObject go, bool isPress)
        {
            if (!isPress)
            {
                SetMoveChapter();
            }
        };
        UIEventListener.Get(GameObject.Find("C2")).onClick += delegate(GameObject go)
        {
            SceneTransformer.instance.NowChapterID = 2;
            SetChapter(SceneTransformer.instance.NowChapterID);
        };
        UIEventListener.Get(GameObject.Find("C3")).onPress += delegate(GameObject go, bool isPress)
        {
            if (!isPress)
            {
                SetMoveChapter();
            }
        };
        UIEventListener.Get(GameObject.Find("C3")).onClick += delegate(GameObject go)
        {
            SceneTransformer.instance.NowChapterID = 3;
            SetChapter(SceneTransformer.instance.NowChapterID);
        };
        UIEventListener.Get(GameObject.Find("C4")).onPress += delegate(GameObject go, bool isPress)
        {
            if (!isPress)
            {
                SetMoveChapter();
            }
        };
        UIEventListener.Get(GameObject.Find("C4")).onClick += delegate(GameObject go)
        {
            SceneTransformer.instance.NowChapterID = 4;
            SetChapter(SceneTransformer.instance.NowChapterID);
        };
        //UIEventListener.Get(GameObject.Find("Chapter04")).onPress += delegate(GameObject go, bool isPress)
        //{
        //    if (!isPress)
        //    {
        //        SetMoveChapter();
        //    }
        //};

        AudioEditer.instance.PlayLoop("Gate");
    }

    public void SetGateButton()
    {
        for (int i = 0; i < mGateList.Count; i++)
        {
            mGateList[i].GetComponent<BoxCollider>().size = new Vector3(100, 100, 0);
        }

        //Debug.LogError(uiSV.centeredObject.transform.FindChild("Lock").gameObject.activeSelf);
        if (!uiSV.centeredObject.transform.FindChild("Chapter").FindChild("Lock").gameObject.activeSelf)
        {
            //Debug.LogError(UIEventListener.Get(mGateList[0]).onClick);
            //if (UIEventListener.Get(mGateList[0]).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("Gate1Button")).onClick += delegate(GameObject go)
                {
                    if (!GameObject.Find("Gate1Button").transform.FindChild("Lock01").gameObject.activeSelf)
                    {
                        UIManager.instance.OpenPanel("GateInfoWindow", false);
                        Debug.Log(ListGate[0].id);
                        GameObject.Find("GateInfoWindow").GetComponent<GateInfoWindow>().Init(ListGate[0].id, 0, SimpleStar, MasterStar, ChallengeStar);
                    }
                    else
                    {
                        GameObject.Find("Gate1Button").GetComponent<BoxCollider>().size = Vector3.zero;
                    }
                };
            }

            //if (UIEventListener.Get(GameObject.Find("Gate2Button")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("Gate2Button")).onClick += delegate(GameObject go)
                {
                    //Debug.LogError(1111111111111111111);
                    if (!GameObject.Find("Gate2Button").transform.FindChild("Lock02").gameObject.activeSelf)
                    {
                        UIManager.instance.OpenPanel("GateInfoWindow", false);
                        GameObject.Find("GateInfoWindow").GetComponent<GateInfoWindow>().Init(ListGate[1].id, 1, SimpleStar, MasterStar, ChallengeStar);
                    }
                    else
                    {
                        GameObject.Find("Gate2Button").GetComponent<BoxCollider>().size = Vector3.zero;
                    }
                };
            }

            //if (UIEventListener.Get(GameObject.Find("Gate3Button")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("Gate3Button")).onClick += delegate(GameObject go)
                {
                    if (!GameObject.Find("Gate3Button").transform.FindChild("Lock03").gameObject.activeSelf)
                    {
                        UIManager.instance.OpenPanel("GateInfoWindow", false);
                        GameObject.Find("GateInfoWindow").GetComponent<GateInfoWindow>().Init(ListGate[2].id, 2, SimpleStar, MasterStar, ChallengeStar);
                    }
                    else
                    {
                        GameObject.Find("Gate3Button").GetComponent<BoxCollider>().size = Vector3.zero;
                    }
                };
            }

            //if (UIEventListener.Get(GameObject.Find("Gate4Button")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("Gate4Button")).onClick += delegate(GameObject go)
                {
                    if (!GameObject.Find("Gate4Button").transform.FindChild("Lock04").gameObject.activeSelf)
                    {
                        UIManager.instance.OpenPanel("GateInfoWindow", false);
                        GameObject.Find("GateInfoWindow").GetComponent<GateInfoWindow>().Init(ListGate[3].id, 3, SimpleStar, MasterStar, ChallengeStar);
                    }
                    else
                    {
                        GameObject.Find("Gate4Button").GetComponent<BoxCollider>().size = Vector3.zero;
                    }
                };
            }

            //if (UIEventListener.Get(GameObject.Find("Gate5Button")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("Gate5Button")).onClick += delegate(GameObject go)
                {
                    if (!GameObject.Find("Gate5Button").transform.FindChild("Lock05").gameObject.activeSelf)
                    {
                        UIManager.instance.OpenPanel("GateInfoWindow", false);
                        GameObject.Find("GateInfoWindow").GetComponent<GateInfoWindow>().Init(ListGate[4].id, 4, SimpleStar, MasterStar, ChallengeStar);
                    }
                    else
                    {
                        GameObject.Find("Gate5Button").GetComponent<BoxCollider>().size = Vector3.zero;
                    }
                };
            }

            //if (UIEventListener.Get(GameObject.Find("Gate6Button")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("Gate6Button")).onClick += delegate(GameObject go)
                {
                    if (!GameObject.Find("Gate6Button").transform.FindChild("Lock06").gameObject.activeSelf)
                    {
                        UIManager.instance.OpenPanel("GateInfoWindow", false);
                        GameObject.Find("GateInfoWindow").GetComponent<GateInfoWindow>().Init(ListGate[5].id, 5, SimpleStar, MasterStar, ChallengeStar);
                    }
                    else
                    {
                        GameObject.Find("Gate6Button").GetComponent<BoxCollider>().size = Vector3.zero;
                    }
                };
            }
        }
        else
        {
            for (int i = 0; i < mGateList.Count; i++)
            {
                mGateList[i].GetComponent<BoxCollider>().size = Vector3.zero;
            }
        }

    }

    void SetMoveChapter()
    {
        //Debug.LogError(uiSV.centeredObject.name);
        if (uiSV.centeredObject.name == "C1")
        {
            Debug.Log("第一章");
            SceneTransformer.instance.NowChapterID = 1;
            SetChapter(SceneTransformer.instance.NowChapterID);
        }
        else if (uiSV.centeredObject.name == "C2")
        {
            Debug.Log("第二章");
            SceneTransformer.instance.NowChapterID = 2;
            SetChapter(SceneTransformer.instance.NowChapterID);
        }
        else if (uiSV.centeredObject.name == "C3")
        {
            Debug.Log("第三章");
            SceneTransformer.instance.NowChapterID = 3;
            SetChapter(SceneTransformer.instance.NowChapterID);
        }
        else if (uiSV.centeredObject.name == "C4")
        {
            Debug.Log("第四章");
            SceneTransformer.instance.NowChapterID = 4;
            SetChapter(SceneTransformer.instance.NowChapterID);
        }
    }

    public void SetChapter(int _Chapter)
    {
        //Debug.LogError(SceneTransformer.instance.NowChapterID + "   &&&&***&&&");
        {
            Chapter = _Chapter;
            ListGate.Clear();

            ListGate = TextTranslator.instance.GetChapterGate(Chapter);
            NetworkHandler.instance.SendProcess("2001#" + _Chapter + ";");
            //LabelGate.GetComponent<UILabel>().text = "第" + Chapter.ToString() + "章  " + TextTranslator.instance.listChapter[Chapter - 1].name;
        }

        if (!IsJump)
        {
            SelectChapterNumber = SceneTransformer.instance.NowChapterID;
            SelectGateNumber = int.Parse(CharacterRecorder.instance.lastGateID.ToString().Substring(4, 1));
        }

        //Debug.LogError(SelectChapterNumber + "    &&&&&&      " + SelectGateNumber);
        for (int k = 0; k < mGateList.Count; k++)
        {
            mGateList[k].transform.FindChild("Effect").gameObject.SetActive(false);
            mGateList[k].transform.FindChild("WF_UI_DiTu_TiShiQuan").gameObject.SetActive(false);
        }
        //Debug.LogError(SceneTransformer.instance.NowChapterID + "   ;;;    " + CharacterRecorder.instance.lastGateID.ToString().Substring(2, 1));
        if (!IsJump)
        {
            if (SelectChapterNumber == int.Parse(CharacterRecorder.instance.lastGateID.ToString().Substring(2, 1)))
            {
                mGateList[SelectGateNumber - 1].transform.FindChild("Effect").gameObject.SetActive(true);
                mGateList[SelectGateNumber - 1].transform.FindChild("WF_UI_DiTu_TiShiQuan").gameObject.SetActive(true);
            }
        }
        else
        {
            mGateList[SelectGateNumber - 1].transform.FindChild("Effect").gameObject.SetActive(true);
            mGateList[SelectGateNumber - 1].transform.FindChild("WF_UI_DiTu_TiShiQuan").gameObject.SetActive(true);
        }


        for (int i = 0; i < ChapterList.Count; i++)
        {
            if (ChapterList[i].GetComponent<TweenScale>())
            {
                DestroyImmediate(ChapterList[i].GetComponent<TweenScale>());
            }
        }
        ChapterList[SelectChapterNumber - 1].AddComponent("TweenScale");
        TweenScale ts = ChapterList[SelectChapterNumber - 1].GetComponent<TweenScale>();
        ts.from = Vector3.one;
        ts.to = new Vector3(1.1f, 1.1f, 1.1f);
        ts.style = UITweener.Style.PingPong;
        ts.duration = 2;
        ts.PlayForward();

    }

    public void SetChapterStar(string SimpleGate, string MasterGate, string ChallengeGate)
    {
        string[] SimpleSplit = SimpleGate.Split('$');
        string[] MasterSplit = MasterGate.Split('$');
        string[] ChallengeSplit = ChallengeGate.Split('$');

        for (int i = 0; i < SimpleSplit.Length - 1; i++)
        {
            GameObject.Find("SpriteStar" + (i + 1).ToString() + "1").transform.Find("Sprite").gameObject.GetComponent<UISprite>().fillAmount = int.Parse(SimpleSplit[i]) / 3f;
            SimpleStar[i] = int.Parse(SimpleSplit[i]) / 3f;
        }

        for (int i = 0; i < MasterSplit.Length - 1; i++)
        {
            GameObject.Find("SpriteStar" + (i + 1).ToString() + "2").transform.Find("Sprite").gameObject.GetComponent<UISprite>().fillAmount = int.Parse(MasterSplit[i]) / 3f;
            MasterStar[i] = int.Parse(MasterSplit[i]) / 3f;
        }

        for (int i = 0; i < ChallengeSplit.Length - 1; i++)
        {
            GameObject.Find("SpriteStar" + (i + 1).ToString() + "3").transform.Find("Sprite").gameObject.GetComponent<UISprite>().fillAmount = int.Parse(ChallengeSplit[i]) / 3f;
            ChallengeStar[i] = int.Parse(ChallengeSplit[i]) / 3f;
        }


        for (int i = 0; i < SimpleSplit.Length - 2; i++)
        {

            if (SimpleSplit[i] == "0")
            {
                mGateList[i + 1].transform.FindChild("Lock0" + (i + 2).ToString()).gameObject.SetActive(true);

                mGateList[i + 1].GetComponent<UISprite>().spriteName = "kuang di4";

                mGateList[i + 1].transform.FindChild("OpenNumber0" + (i + 2).ToString()).gameObject.SetActive(false);
                if ((i + 1) == 6)
                {
                    mGateList[i + 1].transform.FindChild("Sprite").GetComponent<UISprite>().color = Color.gray;
                    mGateList[i + 1].transform.FindChild("Sprite").FindChild("Sprite").GetComponent<UISprite>().color = Color.gray;
                    mGateList[i + 1].transform.FindChild("Sprite").FindChild("Sprite").FindChild("Sprite").GetComponent<UISprite>().color = Color.gray;
                }

            }
            else
            {
                mGateList[i + 1].transform.FindChild("Lock0" + (i + 2).ToString()).gameObject.SetActive(false);

                mGateList[i + 1].GetComponent<UISprite>().spriteName = "kuang di3";

                mGateList[i + 1].transform.FindChild("OpenNumber0" + (i + 2).ToString()).gameObject.SetActive(true);

            }
        }

        if (!uiSV.centeredObject.transform.FindChild("Chapter").FindChild("Lock").gameObject.activeSelf)
        {
            mGateList[0].transform.FindChild("Lock01").gameObject.SetActive(false);
            mGateList[0].GetComponent<UISprite>().spriteName = "kuang di3";
            mGateList[0].transform.FindChild("OpenNumber01").gameObject.SetActive(true);
        }
        else
        {
            mGateList[0].transform.FindChild("Lock01").gameObject.SetActive(true);
            mGateList[0].GetComponent<UISprite>().spriteName = "kuang di4";
            mGateList[0].transform.FindChild("OpenNumber01").gameObject.SetActive(false);
        }

        int starNumber = 0;
        for (int i = 0; i < SimpleSplit.Length - 1; i++)
        {
            if (SimpleSplit[i] == "3")
            {
                starNumber++;
            }
        }
        for (int i = 0; i < MasterSplit.Length - 1; i++)
        {
            if (MasterSplit[i] == "3")
            {
                starNumber++;
            }
        }
        for (int i = 0; i < ChallengeSplit.Length - 1; i++)
        {
            if (ChallengeSplit[i] == "3")
            {
                starNumber++;
            }
        }

        AllStarNumber.text = starNumber + "/18";
        StarSlider.value = starNumber / 18f;
        if (starNumber >= 18)
        {
            box1.transform.FindChild("Light").gameObject.SetActive(true);
            UIEventListener.Get(box1).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPromptWindow("暂未开放", PromptWindow.PromptType.Alert, null, null);
            };
            box2.transform.FindChild("Light").gameObject.SetActive(true);
            UIEventListener.Get(box2).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPromptWindow("暂未开放", PromptWindow.PromptType.Alert, null, null);
            };
            box3.transform.FindChild("Light").gameObject.SetActive(true);
            UIEventListener.Get(box3).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPromptWindow("暂未开放", PromptWindow.PromptType.Alert, null, null);
            };
        }
        else if (starNumber < 18 && starNumber >= 12)
        {
            box1.transform.FindChild("Light").gameObject.SetActive(true);
            UIEventListener.Get(box1).onClick = delegate(GameObject go)
            {
                UIManager.instance.OpenPromptWindow("暂未开放", PromptWindow.PromptType.Alert, null, null);
            };
            box2.transform.FindChild("Light").gameObject.SetActive(true);
            UIEventListener.Get(box2).onClick = delegate(GameObject go)
            {
                UIManager.instance.OpenPromptWindow("暂未开放", PromptWindow.PromptType.Alert, null, null);
            };
            box3.transform.FindChild("Light").gameObject.SetActive(false);
            UIEventListener.Get(box3).onClick -= delegate(GameObject go)
            {
                UIManager.instance.OpenPromptWindow("暂未开放", PromptWindow.PromptType.Alert, null, null);
            };
        }
        else if (starNumber < 12 && starNumber >= 6)
        {
            box1.transform.FindChild("Light").gameObject.SetActive(true);
            UIEventListener.Get(box1).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPromptWindow("暂未开放", PromptWindow.PromptType.Alert, null, null);
            };
            box2.transform.FindChild("Light").gameObject.SetActive(false);
            UIEventListener.Get(box2).onClick -= delegate(GameObject go)
            {
                UIManager.instance.OpenPromptWindow("暂未开放", PromptWindow.PromptType.Alert, null, null);
            };
            box3.transform.FindChild("Light").gameObject.SetActive(false);
            UIEventListener.Get(box3).onClick -= delegate(GameObject go)
            {
                UIManager.instance.OpenPromptWindow("暂未开放", PromptWindow.PromptType.Alert, null, null);
            };
        }
        else if (starNumber < 6)
        {
            box1.transform.FindChild("Light").gameObject.SetActive(false);
            UIEventListener.Get(box1).onClick -= delegate(GameObject go)
            {
                UIManager.instance.OpenPromptWindow("暂未开放", PromptWindow.PromptType.Alert, null, null);
            };
            box2.transform.FindChild("Light").gameObject.SetActive(false);
            UIEventListener.Get(box2).onClick -= delegate(GameObject go)
            {
                UIManager.instance.OpenPromptWindow("暂未开放", PromptWindow.PromptType.Alert, null, null);
            };
            box3.transform.FindChild("Light").gameObject.SetActive(false);
            UIEventListener.Get(box3).onClick -= delegate(GameObject go)
            {
                UIManager.instance.OpenPromptWindow("暂未开放", PromptWindow.PromptType.Alert, null, null);
            };
        }
    }

    public void SetChapterOpen(string strRev)//0:全部没打过   1:打过至少一个但没通关   2:通关开启下一张
    {
        for (int i = 0; i < ChapterList.Count; i++)
        {
            ChapterList[i].transform.FindChild("Lock").gameObject.SetActive(true);
        }
        ChapterList[0].transform.FindChild("Lock").gameObject.SetActive(false);

        string[] dataSplit = strRev.Split('$');
        if (dataSplit[1] == "0")
        {
            if (dataSplit[0] == "0" || dataSplit[0] == "1")
            {
                ChapterList[1].GetComponent<UITexture>().mainTexture = Resources.Load("MapTexture/map05") as Texture;
                ChapterList[1].transform.FindChild("Lock").gameObject.SetActive(true);
            }
            else
            {
                ChapterList[1].GetComponent<UITexture>().mainTexture = Resources.Load("MapTexture/map02") as Texture;
                ChapterList[1].transform.FindChild("Lock").gameObject.SetActive(false);
            }
        }
        else if (dataSplit[1] == "1" || dataSplit[1] == "2")
        {
            ChapterList[1].GetComponent<UITexture>().mainTexture = Resources.Load("MapTexture/map02") as Texture;
            ChapterList[1].transform.FindChild("Lock").gameObject.SetActive(false);
        }

        if (dataSplit[2] == "0")
        {
            if (dataSplit[1] == "0" || dataSplit[1] == "1")
            {
                ChapterList[2].GetComponent<UITexture>().mainTexture = Resources.Load("MapTexture/map06") as Texture;
                ChapterList[2].transform.FindChild("Lock").gameObject.SetActive(true);
            }
            else
            {
                ChapterList[2].GetComponent<UITexture>().mainTexture = Resources.Load("MapTexture/map03") as Texture;
                ChapterList[2].transform.FindChild("Lock").gameObject.SetActive(false);
            }
        }
        else if (dataSplit[2] == "1" || dataSplit[2] == "2")
        {
            ChapterList[2].GetComponent<UITexture>().mainTexture = Resources.Load("MapTexture/map03") as Texture;
            ChapterList[2].transform.FindChild("Lock").gameObject.SetActive(false);
        }

        if (dataSplit[3] == "0")
        {
            if (dataSplit[2] == "0" || dataSplit[1] == "1")
            {
                ChapterList[3].GetComponent<UITexture>().mainTexture = Resources.Load("MapTexture/map07") as Texture;
                ChapterList[3].transform.FindChild("Lock").gameObject.SetActive(true);
            }
            else
            {
                ChapterList[3].GetComponent<UITexture>().mainTexture = Resources.Load("MapTexture/map04") as Texture;
                ChapterList[3].transform.FindChild("Lock").gameObject.SetActive(false);
            }
        }
        else if (dataSplit[3] == "1" || dataSplit[3] == "2")
        {
            ChapterList[3].GetComponent<UITexture>().mainTexture = Resources.Load("MapTexture/map04") as Texture;
            ChapterList[3].transform.FindChild("Lock").gameObject.SetActive(false);
        }
    }

    public void SetJumpWindowToGate(int chapterID, int gateID)
    {
        SelectChapterNumber = chapterID;
        SelectGateNumber = int.Parse(gateID.ToString().Substring(4, 1));
        Chapter = chapterID;
        SetChapter(chapterID);

    }



}
