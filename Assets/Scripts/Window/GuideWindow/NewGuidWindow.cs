using UnityEngine;
using System.Collections;

public class NewGuidWindow : MonoBehaviour
{
    public GameObject BigGuide;
    public GameObject MinGuide;
    public GameObject SmallGuide;
    public GameObject CommonGuide;
    public GameObject TalkGuide;
    public GameObject MoveGuide;
    public GameObject MaxWindow;
    private string GuildLabel = "";
    private string GuildLabel1 = "";
    private string GuildLabel2 = "";
    public UILabel TextLabel;
    public UILabel InfoLabel;
    public UILabel CommonLabel;
    public UILabel TalkLabel;
    public UILabel MoveLabel;
    public UITexture MoveTextture;
    public UITexture TalkTextture;
    public UITexture CommonTextture;
    public UITexture InfoTextture;
    public UITexture LeftTextture;
    public UITexture RightTextture;
    public GameObject GuideArrow;
    public GameObject EffectObject;
    public GameObject CenterObject;
    public GameObject CommonObject;
    public GameObject MoveObject;
    public GameObject MaxWindowBg;
    //新手引导箭头
    public GameObject JiantouDown;
    public GameObject JiantouMove;
    public GameObject DownArrayMessage;
    public GameObject SkillMessage;
    public GameObject ActivityMessage;
    public bool EventTalk = false;
    bool IsChangeLeft = true;
    bool IsChangeRight = true;
    bool IsTalkClick = false;
    public float timer = 0;
    public GameObject SkipGuideButton;
    public GameObject JianTouEffect;
    public GameObject JianTouEffect2;

    public UISprite KongBai;
    void OnDestroy()
    {
        StopCoroutine(UpdateName());
    }
    void Awake()
    {
        //Debug.LogError("切换图片" + System.Convert.ToSingle(Screen.height) / Screen.width + "          " + System.Convert.ToSingle(600) / 900);
        if (System.Convert.ToSingle(Screen.height) / Screen.width >= System.Convert.ToSingle(600) / 900 - 0.02f && System.Convert.ToSingle(Screen.height) / Screen.width < System.Convert.ToSingle(1536) / 2048)
        {
            KongBai.width = 1225;
        }
        else if (System.Convert.ToSingle(Screen.height) / Screen.width >= System.Convert.ToSingle(1536) / 2048)
        {
            KongBai.width = 1422;
        }
        else
        {
            //Debug.LogError("ss " + (System.Convert.ToSingle(Screen.width) / Screen.height) + "   " + (System.Convert.ToSingle(960) / 640));
            KongBai.width = (int)(1200 * (System.Convert.ToSingle(Screen.width) / Screen.height) / (System.Convert.ToSingle(960) / 640));
        }
        //Debug.LogError("图片的长度：" + KongBai.width + "界面 " + Screen.width);
    }

    /// <summary>
    /// Type：显示的是那种类型的界面，内容：1，大的对话框界面   2，小的点击的界面  3，全部的背景界面
    /// GuildLabel：文字的显示
    /// LeftRight:大的文字显示的头像左右
    /// Icon: 大的文字显示的头像人物
    /// </summary>
    /// <param name="Type"></param>
    /// <param name="Name"></param>
    /// <param name="LeftRight"></param>
    public void SetInfo(int Type, string GuildLabel, int LeftRight, string Icon, string Name)
    {
        StopAllCoroutines();
        JiantouDown.SetActive(false);
        JiantouMove.SetActive(false);
        DownArrayMessage.SetActive(false);
        SkillMessage.SetActive(false);
        ActivityMessage.SetActive(false);
        SkipGuideButton.SetActive(false);
        JianTouEffect.SetActive(false);
        JianTouEffect2.SetActive(false);
        SceneTransformer.instance.isNextTalk = false;
        SceneTransformer.instance.isClickSkip = false;
        UIEventListener.Get(SkipGuideButton).onClick = delegate(GameObject go)
        {
            if (GameObject.Find("PVPWindow") != null)
            {
                GameObject.Find("PVPWindow/ALL/Scroll View").GetComponent<UIScrollView>().enabled = true;//此段为竞技场滑动在跳过本次引导后可以滑动，有影响就注释掉，哼
            }
            StartCoroutine(SkipClickTalk(0.2f));
        };
        if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 3 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 13)
        {
            JiantouDown.SetActive(true);
        }
        //if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 6 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 7)
        //{
        //    JiantouMove.SetActive(true);
        //}
        if ((PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 0 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 2) || EventTalk)
        {
            UISprite _obj = NGUITools.AddSprite(BigGuide, CenterObject.GetComponent<UISprite>().atlas, "white");
            _obj.MakePixelPerfect();
            _obj.transform.localScale = new Vector3(2000, 2000, 1);
            _obj.depth = 1;
            _obj.color = Color.black;
            _obj.gameObject.name = "blackbg";
        }
        if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 0 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 3)
        {
            PictureCreater.instance.NewbieMove();
            GameObject.Find("NewGuideWindow").transform.Find("BigGuide/blackbg").gameObject.SetActive(false);
        }
        if ((PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 0 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 4))
        {
            DestroyImmediate(GameObject.Find("blackbg"));
        }
        if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 0 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 5)
        {
            PictureCreater.instance.NewbieBlood();
        }
        if (SceneTransformer.instance.CheckGuideIsFinish())
        {
            //if (CharacterRecorder.instance.GuideID[0] == 3)
            //{
            //    DownArrayMessage.SetActive(true);
            //}
            if (CharacterRecorder.instance.GuideID[29] == 6)
            {
                SkillMessage.SetActive(true);
            }
            else if (CharacterRecorder.instance.GuideID[1] == 1)
            {
                ActivityMessage.SetActive(true);
                ActivityMessage.GetComponent<UISprite>().spriteName = "loginSign";
                ActivityMessage.GetComponent<UISprite>().MakePixelPerfect();
            }
            else if (CharacterRecorder.instance.GuideID[8] == 2)
            {
                ActivityMessage.SetActive(true);
                ActivityMessage.GetComponent<UISprite>().spriteName = "fristMoney";
                ActivityMessage.GetComponent<UISprite>().MakePixelPerfect();
            }
        }
        TextLabel.text = "";
        this.GuildLabel = GuildLabel;
        if (Type == 1)
        {
            MoveGuide.SetActive(false);
            TalkGuide.SetActive(false);
            CommonGuide.SetActive(false);
            SmallGuide.SetActive(false);
            BigGuide.SetActive(true);
            MinGuide.SetActive(false);
            MaxWindow.SetActive(false);
            GuideArrow.SetActive(false);
            JianTouEffect2.SetActive(true);
            //if (NetworkHandler.instance.IsCreate)
            //{
            //    JianTouEffect2.SetActive(true);
            //}
            //else
            //{
            //    JianTouEffect.SetActive(true);
            //}
            GuideArrow.transform.localPosition = new Vector3(600, -310, 0);

            if (LeftRight == 1)
            {
                IsChangeLeft = true;
                RightTextture.transform.parent.gameObject.SetActive(false);
                LeftTextture.transform.parent.gameObject.SetActive(true);
                //if (LeftTextture.mainTexture != null)
                //{
                //    if (LeftTextture.mainTexture.name != Icon || IsChangeRight)
                //    {
                //        LeftTextture.GetComponent<TweenPosition>().enabled = true;
                //        LeftTextture.GetComponent<TweenPosition>().ResetToBeginning();
                //    }
                //}
                IsChangeRight = false;
                if (Icon == "Lili")
                {
                    StartCoroutine(UpdateLiliTexture(LeftTextture));
                }
                else
                {
                    LeftTextture.mainTexture = Resources.Load("GuideTexture/" + Icon) as Texture;
                }
                LeftTextture.transform.parent.transform.Find("Name").Find("Label").GetComponent<UILabel>().text = Name;
            }
            else if (LeftRight == 2)
            {
                IsChangeRight = true;
                LeftTextture.transform.parent.gameObject.SetActive(false);
                RightTextture.transform.parent.gameObject.SetActive(true);
                //if (RightTextture.mainTexture != null)
                //{
                //    if (RightTextture.mainTexture.name != Icon || IsChangeLeft)
                //    {
                //        RightTextture.GetComponent<TweenPosition>().enabled = true;
                //        RightTextture.GetComponent<TweenPosition>().ResetToBeginning();
                //    }
                //}
                IsChangeLeft = false;
                if (Icon == "Lili")
                {
                    StartCoroutine(UpdateLiliTexture(RightTextture));
                }
                else
                {
                    RightTextture.mainTexture = Resources.Load("GuideTexture/" + Icon) as Texture;
                }
                RightTextture.transform.parent.transform.Find("Name").Find("Label").GetComponent<UILabel>().text = Name;

            }
            if (GameObject.Find("GuideLabel") != null)
            {
                GameObject.Find("GuideLabel").GetComponent<UILabel>().text = GuildLabel;
            }
            SceneTransformer.instance.isNextTalk = true;
            UIEventListener.Get(BigGuide).onClick = delegate(GameObject go)
            {
                if ((PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 0 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 0))
                {

                }
                else
                {
                    if (SceneTransformer.instance.CheckGuideIsFinish() == false)
                    {
                        if (EventTalk == false)
                        {
                            SceneTransformer.instance.NewGuideButtonClick();
                        }

                    }
                    else
                    {
                        if (EventTalk == false)
                        {
                            SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                        }
                    }
                }
            };
        }
        else if (Type == 2)
        {
            MoveGuide.SetActive(false);
            TalkGuide.SetActive(false);
            CommonGuide.SetActive(false);
            SmallGuide.SetActive(false);
            BigGuide.SetActive(false);
            MinGuide.SetActive(true);
            MaxWindow.SetActive(false);
            GuideArrow.SetActive(true);
            //if (UIEventListener.Get(GameObject.Find("GuidePartTop")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("GuidePartTop")).onClick = delegate(GameObject go)
                {
                    NGUITools.AddChild(CenterObject, EffectObject);
                };
            }
            //if (UIEventListener.Get(GameObject.Find("GuidePartBottom")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("GuidePartBottom")).onClick = delegate(GameObject go)
                {
                    NGUITools.AddChild(CenterObject, EffectObject);
                };
            }
            //if (UIEventListener.Get(GameObject.Find("GuidePartLeft")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("GuidePartLeft")).onClick = delegate(GameObject go)
                {
                    NGUITools.AddChild(CenterObject, EffectObject);
                };
            }
            //if (UIEventListener.Get(GameObject.Find("GuidePartRight")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("GuidePartRight")).onClick = delegate(GameObject go)
                {
                    NGUITools.AddChild(CenterObject, EffectObject);
                };
            }
        }
        else if (Type == 3)
        {
            AudioEditer.instance.PlayOneShot("typing");
            MoveGuide.SetActive(false);
            TalkGuide.SetActive(false);
            CommonGuide.SetActive(false);
            SmallGuide.SetActive(false);
            BigGuide.SetActive(false);
            MinGuide.SetActive(false);
            MaxWindow.SetActive(true);
            GuideArrow.SetActive(false);
            JianTouEffect2.SetActive(true);
            //if (NetworkHandler.instance.IsCreate)
            //{
            //    JianTouEffect2.SetActive(true);
            //}
            //else
            //{
            //    JianTouEffect.SetActive(true);
            //}
            GuideArrow.transform.localPosition = new Vector3(600, -310, 0);
            GuildLabel1 = Icon;
            GuildLabel2 = Name;
            StartCoroutine("UpdateName");
            SceneTransformer.instance.isNextTalk = true;
            UIEventListener.Get(MaxWindowBg).onClick = delegate(GameObject go)
            {
                if ((PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 0 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 1))
                {

                }
                else
                {
                    if (SceneTransformer.instance.CheckGuideIsFinish() == false)
                    {

                        SceneTransformer.instance.NewGuideButtonClick();

                    }
                    else
                    {
                        if (EventTalk == false)
                        {
                            SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                        }
                    }
                }
            };
        }
        else if (Type == 4)
        {
            MoveGuide.SetActive(false);
            TalkGuide.SetActive(false);
            CommonGuide.SetActive(false);
            SmallGuide.SetActive(true);
            BigGuide.SetActive(false);
            MinGuide.SetActive(false);
            MaxWindow.SetActive(false);
            GuideArrow.SetActive(false);
            //JianTouEffect2.SetActive(true);
            //if (NetworkHandler.instance.IsCreate)
            //{
            //    JianTouEffect2.SetActive(true);
            //}
            //else
            //{
            //    JianTouEffect.SetActive(true);
            //}
            GuideArrow.transform.localPosition = new Vector3(600, -310, 0);
            if (Icon == "Lili")
            {
                StartCoroutine(UpdateLiliTexture(InfoTextture));
            }
            else
            {
                InfoTextture.mainTexture = Resources.Load("GuideTexture/" + Icon) as Texture;
            }
            InfoLabel.text = GuildLabel;
            UIEventListener.Get(SmallGuide).onClick = delegate(GameObject go)
            {
                if (SceneTransformer.instance.CheckGuideIsFinish() == false)
                {
                    SceneTransformer.instance.NewGuideButtonClick();
                }
                else
                {
                    if (EventTalk == false)
                    {
                        SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                    }
                }
            };
        }
        else if (Type == 5)
        {
            MoveGuide.SetActive(false);
            TalkGuide.SetActive(false);
            CommonGuide.SetActive(true);
            SmallGuide.SetActive(false);
            BigGuide.SetActive(false);
            MinGuide.SetActive(false);
            MaxWindow.SetActive(false);
            GuideArrow.SetActive(true);
            if (Icon == "Lili")
            {
                StartCoroutine(UpdateLiliTexture(CommonTextture));
            }
            else
            {
                CommonTextture.mainTexture = Resources.Load("GuideTexture/" + Icon) as Texture;
            }
            CommonLabel.text = GuildLabel;

            //if (UIEventListener.Get(GameObject.Find("CommonGuidePartTop")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("CommonGuidePartTop")).onClick = delegate(GameObject go)
                {
                    NGUITools.AddChild(CommonObject, EffectObject);
                };
            }
            //if (UIEventListener.Get(GameObject.Find("CommonGuidePartBottom")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("CommonGuidePartBottom")).onClick = delegate(GameObject go)
                {
                    NGUITools.AddChild(CommonObject, EffectObject);
                };
            }
            //if (UIEventListener.Get(GameObject.Find("CommonGuidePartLeft")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("CommonGuidePartLeft")).onClick = delegate(GameObject go)
                {
                    NGUITools.AddChild(CommonObject, EffectObject);
                };
            }
            //if (UIEventListener.Get(GameObject.Find("CommonGuidePartRight")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("CommonGuidePartRight")).onClick = delegate(GameObject go)
                {
                    NGUITools.AddChild(CommonObject, EffectObject);
                };
            }
        }
        else if (Type == 6)
        {
            MoveGuide.SetActive(false);
            TalkGuide.SetActive(true);
            CommonGuide.SetActive(false);
            SmallGuide.SetActive(false);
            BigGuide.SetActive(false);
            MinGuide.SetActive(false);
            MaxWindow.SetActive(false);
            GuideArrow.SetActive(false);
            IsTalkClick = false;
            if (Icon == "Lili")
            {
                StartCoroutine(UpdateLiliTexture(TalkTextture));
            }
            else
            {
                TalkTextture.mainTexture = Resources.Load("GuideTexture/" + Icon) as Texture;
            }
            TalkLabel.text = GuildLabel;
            StartCoroutine(ClickTalk(2f));

            UIEventListener.Get(TalkGuide).onClick = delegate(GameObject go)
            {
                if (!IsTalkClick)
                {
                    IsTalkClick = true;
                    if (SceneTransformer.instance.CheckGuideIsFinish() == false)
                    {
                        SceneTransformer.instance.NewGuideButtonClick();
                    }
                    else
                    {
                        SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                    }
                }
            };
        }
        else if (Type == 7)
        {
            MoveGuide.SetActive(true);
            TalkGuide.SetActive(false);
            CommonGuide.SetActive(false);
            SmallGuide.SetActive(false);
            BigGuide.SetActive(false);
            MinGuide.SetActive(false);
            MaxWindow.SetActive(false);
            GuideArrow.SetActive(true);
            if (Icon == "Lili")
            {
                StartCoroutine(UpdateLiliTexture(MoveTextture));
            }
            else
            {
                MoveTextture.mainTexture = Resources.Load("GuideTexture/" + Icon) as Texture;
            }
            MoveLabel.text = GuildLabel;

            //if (UIEventListener.Get(GameObject.Find("MoveGuidePartTop")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("MoveGuidePartTop")).onClick = delegate(GameObject go)
                {
                    Debug.Log("AAAA");
                    NGUITools.AddChild(MoveObject, EffectObject);
                };
            }
            //if (UIEventListener.Get(GameObject.Find("MoveGuidePartBottom")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("MoveGuidePartBottom")).onClick = delegate(GameObject go)
                {
                    Debug.Log("AAAABBB");
                    NGUITools.AddChild(MoveObject, EffectObject);
                };
            }
            //if (UIEventListener.Get(GameObject.Find("MoveGuidePartLeft")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("MoveGuidePartLeft")).onClick = delegate(GameObject go)
                {
                    Debug.Log("AAAACCC");
                    NGUITools.AddChild(MoveObject, EffectObject);
                };
            }
            //if (UIEventListener.Get(GameObject.Find("MoveGuidePartRight")).onClick == null)
            {
                UIEventListener.Get(GameObject.Find("MoveGuidePartRight")).onClick = delegate(GameObject go)
                {
                    Debug.Log("AAAADDD");
                    NGUITools.AddChild(MoveObject, EffectObject);
                };
            }
        }
        else if (Type == 0)
        {
            MoveGuide.SetActive(false);
            TalkGuide.SetActive(false);
            CommonGuide.SetActive(false);
            SmallGuide.SetActive(false);
            BigGuide.SetActive(false);
            MinGuide.SetActive(false);
            MaxWindow.SetActive(false);
            GuideArrow.SetActive(false);

        }

    }
    IEnumerator SkipClickTalk(float timer)
    {
        if (!IsTalkClick)
        {
            if (CharacterRecorder.instance.NowGuideID == 19)
            {
                CharacterRecorder.instance.GuideID[17] = 99;
            }
            SceneTransformer.instance.SendGuide();
            SceneTransformer.instance.GuideTimer = 0;
            BigGuide.SetActive(false);
            MinGuide.SetActive(false);
            MaxWindow.SetActive(false);
            GuideArrow.SetActive(false);
            SmallGuide.SetActive(false);
            CommonGuide.SetActive(false);
            TalkGuide.SetActive(false);
            MoveGuide.SetActive(false);
            JiantouDown.SetActive(false);
            JiantouMove.SetActive(false);
            DownArrayMessage.SetActive(false);
            SkillMessage.SetActive(false);
            ActivityMessage.SetActive(false);
            SkipGuideButton.SetActive(false);
            yield return new WaitForSeconds(timer);
            IsTalkClick = true;
        }
        if (SceneTransformer.instance.isClickSkip)
        {
            SceneTransformer.instance.isClickSkip = false;
            SkipGuideButton.SetActive(false);
            PlayerPrefs.SetInt(LuaDeliver.instance.GetGuideStateName(), 18);
        }
    }
    IEnumerator ClickTalk(float timer)
    {
        yield return new WaitForSeconds(timer);
        if (!IsTalkClick)
        {
            IsTalkClick = true;
            if (SceneTransformer.instance.CheckGuideIsFinish() == false)
            {
                SceneTransformer.instance.NewGuideButtonClick();
            }
            else
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
        }
    }
    IEnumerator UpdateLiliTexture(UITexture LiliTexture)
    {
        LiliTexture.mainTexture = Resources.Load("GuideTexture/lili_01") as Texture;
        yield return new WaitForSeconds(0.6f);
        LiliTexture.mainTexture = Resources.Load("GuideTexture/lili_01") as Texture;
        yield return new WaitForSeconds(0.6f);
        LiliTexture.mainTexture = Resources.Load("GuideTexture/lili_02") as Texture;
        //yield return new WaitForSeconds(0.4f);
        //LiliTexture.mainTexture = Resources.Load("GuideTexture/lili_03") as Texture;
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(UpdateLiliTexture(LiliTexture));
    }
    IEnumerator UpdateName()
    {

        //TextLabel.text = UpdateNameSpace(GuildLabel.Length);
        Debug.Log("左对齐显示一定距离的空白格");
        for (int i = 0; i < GuildLabel.Length; i++)
        {
            TextLabel.text += GuildLabel.Substring(i, 1);
            yield return new WaitForSeconds(0.1f);
        }

        if (GuildLabel1 != "")
        {
            TextLabel.text += "\n\n";

            //if (GuildLabel1.Length >= 1)
            //{
            //    TextLabel.text += "\n";
            //    TextLabel.text += UpdateNameSpace(GuildLabel1.Length);
            //}

            for (int i = 0; i < GuildLabel1.Length; i++)
            {
                TextLabel.text += GuildLabel1.Substring(i, 1);
                yield return new WaitForSeconds(0.1f);
            }
            TextLabel.text += GuildLabel2;
        }
    }

    private string UpdateNameSpace(int Length)
    {
        int number = 32 - Length;
        int CurNumberSpace = number / 2;
        string Space = "　";
        string CurSpace = "";
        for (int i = 0; i < CurNumberSpace; i++)
        {
            CurSpace += Space;
        }
        return CurSpace;
    }

    void Update()
    {
        if (Application.loadedLevelName == "Downloader")
        {
            if (SceneTransformer.instance.isNextTalk && EventTalk == false)
            {
                timer += Time.deltaTime;
                if (timer >= 6)
                {
                    timer = 0;
                    if (SceneTransformer.instance.CheckGuideIsFinish())
                    {
                        SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
                    }
                    //else if ((PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 0 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 0))
                    //{
                    //    Debug.LogError("AAA" + PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) + PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()));
                    //}
                    else
                    {
                        Debug.LogError("BBB" + PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) + PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()));
                        SceneTransformer.instance.NewGuideButtonClick();
                    }
                }

            }
            else if (EventTalk)
            {
                timer += Time.deltaTime;
                if (timer >= 6)
                {
                    timer = 0;
                    DestroyImmediate(GameObject.Find("blackbg"));
                    SceneTransformer.instance.SetEventNumber(SceneTransformer.instance.Talkid, SceneTransformer.instance.Countid + 1);
                }
            }
        }
        else
        {
            if (EventTalk && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 6)
            {
                DestroyImmediate(GameObject.Find("blackbg"));
                timer += Time.deltaTime;
                if (timer >= 5)
                {
                    EventTalk = false;
                    timer = 0;
                    SceneTransformer.instance.NewGuideButtonClick();
                }
            }
        }
        if (SceneTransformer.instance.isClickSkip && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) > 4 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) < 18)
        {
            timer += Time.deltaTime;
            if (timer >= 10)
            {
                timer = 0;
                SkipGuideButton.SetActive(true);
            }
        }
    }


}

