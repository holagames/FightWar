using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GachaWindow : MonoBehaviour
{
    bool IsPlaySound = false;
    public GameObject Tab1;
    public GameObject Tab2;
    public GameObject Tab3;
    private int leftTime = 0;
    private int leftResidue = 5;

    private int rightTime = 0;
    private int rightResidue = 1;
    private int getRoleNum;

    public UILabel LabelLeftDegree;
    public UILabel LabelLeftTime;
    public UILabel LeftOneMoney;

    public UILabel LabeRightlDegree;
    public UILabel LabelRightTime;
    public UILabel RightOneMoney;
    public UILabel Labelpoint;

    public GameObject RedPointLeft;
    public GameObject RedPointRight;


    private float TimeIndex = 0;

    // Use this for initialization

    public GameObject TitleIsStay;
    public GameObject FirstTenTextureIsStay;

    public TweenScale TenChouKaTexture;


    public GameObject LeftAwardButton;
    public GameObject RightAwardButton;
    void Start()
    {
        UIManager.instance.CountSystem(UIManager.Systems.招募);
        UIManager.instance.UpdateSystems(UIManager.Systems.招募);

        if (CharacterRecorder.instance.GuideID[55] == 0)
        {
            TitleIsStay.SetActive(true);
            FirstTenTextureIsStay.SetActive(true);
        }
        else 
        {
            TitleIsStay.SetActive(false);
            FirstTenTextureIsStay.SetActive(false);
        }


        CharacterRecorder.instance.heroIdList.Clear();
        foreach (var heroItem in CharacterRecorder.instance.ownedHeroList)
        {
            CharacterRecorder.instance.heroIdList.Add(heroItem.cardID);
        }
        NetworkHandler.instance.SendProcess("5204#;");
        //NetworkHandler.instance.SendProcess("9301#1;30;");
        if (UIEventListener.Get(GameObject.Find("FGachaButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("FGachaButton")).onClick += delegate(GameObject go)
            {
                //if (TimeIndex > 2f) 
                //{
                //    TimeIndex = 0;
                //    if (leftResidue > 0 && leftTime <= 0)
                //    {
                //        NetworkHandler.instance.SendProcess("5201#3;");
                //    }
                //    else
                //    {
                //        NetworkHandler.instance.SendProcess("5201#4;");
                //    }
                //}

                if (leftResidue > 0 && leftTime <= 0)
                {
                    if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 2 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 5)
                    {
                        SceneTransformer.instance.NewGuideButtonClick();
                    }
                    NetworkHandler.instance.SendProcess("5201#3;");
                }
                else
                {
                    NetworkHandler.instance.SendProcess("5201#4;");
                }
            };
        }

        if (UIEventListener.Get(GameObject.Find("FGacha10Button")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("FGacha10Button")).onClick += delegate(GameObject go)
            {
                //if (TimeIndex > 2)
                //{
                //    TimeIndex = 0;
                //    NetworkHandler.instance.SendProcess("5202#5;");
                //}
                NetworkHandler.instance.SendProcess("5202#5;");
            };
        }


        //if (UIEventListener.Get(GameObject.Find("GFGachaButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("GFGachaButton")).onClick += delegate(GameObject go)
            {
                //if (TimeIndex > 2) 
                //{
                //    TimeIndex = 0;
                //    if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 2 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 3)
                //    {
                //        UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_601);
                //    }
                //    if (CharacterRecorder.instance.ownedHeroList.size < 3)
                //    {
                //        NetworkHandler.instance.SendProcess("5201#2;");
                //    }
                //    else if (rightResidue > 0)
                //    {
                //        NetworkHandler.instance.SendProcess("5201#6;");
                //    }
                //    else
                //    {
                //        NetworkHandler.instance.SendProcess("5201#7;");
                //    }
                //}
                if (CharacterRecorder.instance.ownedHeroList.size < 3)
                {
                    NetworkHandler.instance.SendProcess("5201#2;");
                    if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 4 && PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideSubStateName()) == 5)
                    {
                        SceneTransformer.instance.NewGuideButtonClick();
                    }
                }
                else if (rightResidue > 0)
                {
                    NetworkHandler.instance.SendProcess("5201#6;");
                }
                else
                {
                    NetworkHandler.instance.SendProcess("5201#7;");
                }
            };
        }

        if (UIEventListener.Get(GameObject.Find("GFGacha10Button")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("GFGacha10Button")).onClick += delegate(GameObject go)
            {
                //if (TimeIndex > 2) 
                //{
                //    TimeIndex = 0;
                //    NetworkHandler.instance.SendProcess("5202#8;");
                //}
                NetworkHandler.instance.SendProcess("5202#8;");
            };
        }

        /*  if (UIEventListener.Get(GameObject.Find("SpriteTab1")).onClick == null)
          {
              UIEventListener.Get(GameObject.Find("SpriteTab1")).onClick += delegate(GameObject go)
              {
                  Tab1.SetActive(true);
                  Tab2.SetActive(false);
                  Tab3.SetActive(false);
              };
          }
          if (UIEventListener.Get(GameObject.Find("SpriteTab2")).onClick == null)
          {
              UIEventListener.Get(GameObject.Find("SpriteTab2")).onClick += delegate(GameObject go)
              {
                  Tab1.SetActive(false);
                  Tab2.SetActive(true);
                  Tab3.SetActive(false);
              };
          } */

        Tab1.SetActive(true);
        Tab2.SetActive(false);
        Tab3.SetActive(false);

        AudioEditer.instance.PlayLoop("Gacha");

        //Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        //NetworkHandler.instance.SendProcess("5204#;");
        //Invoke("BoxCollderSize", 1f);
        SetTenChouKaTexture();

        UIEventListener.Get(LeftAwardButton).onClick = delegate(GameObject go)
        {

            UIManager.instance.OpenPanel("GaChaAwardWindow", false);
            GameObject.Find("GaChaAwardWindow").GetComponent<GaChaAwardWindow>().SetGachaType(1);
        };

        UIEventListener.Get(RightAwardButton).onClick = delegate(GameObject go)
        {

            UIManager.instance.OpenPanel("GaChaAwardWindow", false);
            GameObject.Find("GaChaAwardWindow").GetComponent<GaChaAwardWindow>().SetGachaType(2);
        };
    }

    //Invoke("BoxCollderSize", 2f);


    void BoxCollderSize()
    {
        TimeIndex++;
    }

    void OnDestroy()
    {
        AudioEditer.instance.PlayLoop("Scene");
    }
    public void SetInfo(int _leftTime, int _leftResidue, int _rightTime, int _rightResidue, int _getRoleNum)//接受数据
    {
        this.leftTime = _leftTime;//0
        this.leftResidue = _leftResidue;//5

        this.rightTime = _rightTime;//0
        this.rightResidue = _rightResidue;//1
        this.getRoleNum = _getRoleNum % 10;//27

        //Tab1.transform.Find("LabelLeftDegree").GetComponent<UILabel>().text = "免费次数（"+ leftResidue.ToString() + "/5)";
        //Tab1.transform.Find("LabeRightlDegree").GetComponent<UILabel>().text = "免费次数（"+ rightResidue.ToString() + "/1)";
        //Tab1.transform.Find("Labelpoint").GetComponent<UILabel>().text = (10 - getRoleNum).ToString() + "次后必得英雄";
        //LabelLeftDegree.text = "免费次数(" + leftResidue.ToString() + "/5)";
        //LabeRightlDegree.text = "免费次数(" + rightResidue.ToString() + "/1)";
        LabelLeftDegree.text = leftResidue.ToString() + "/5";
        LabeRightlDegree.text = rightResidue.ToString() + "/1";
        if (getRoleNum == 9)
        {
            Labelpoint.text = "本次必得英雄";
        }
        else
        {
            Labelpoint.text = (10 - getRoleNum).ToString() + "次后必得英雄";
        }
        if (leftResidue == 0)
        {
            leftTime = -1;
            //Tab1.transform.Find("SpriteleftMoney").Find("Label").GetComponent<UILabel>().text = "20000";
            LeftOneMoney.text = "20000";
            RedPointLeft.SetActive(false);
            if (!RedPointRight.activeSelf)
            {
                CharacterRecorder.instance.SetRedPoint(6, false);
            }
        }
        else
        {
            //Tab1.transform.Find("SpriteleftMoney").Find("Label").GetComponent<UILabel>().text = "免费";
            LeftOneMoney.text = "免费";
            RedPointLeft.SetActive(true);
            CharacterRecorder.instance.SetRedPoint(6, true);
        }

        if (rightResidue == 0)
        {
            //rightTime = -1;
            //Tab1.transform.Find("SpriterightDiamond").Find("Label").GetComponent<UILabel>().text = "240";
            RightOneMoney.text = "240";
            RedPointRight.SetActive(false);
            if (!RedPointLeft.activeSelf)
            {
                CharacterRecorder.instance.SetRedPoint(6, false);
            }
        }
        else
        {
            //Tab1.transform.Find("SpriterightDiamond").Find("Label").GetComponent<UILabel>().text = "免费";
            RightOneMoney.text = "免费";
            RedPointRight.SetActive(true);
            CharacterRecorder.instance.SetRedPoint(6, true);
        }
        CancelInvoke();
        InvokeRepeating("UpdateTime", 0, 1.0f);
        //Invoke("BoxCollderSize", 1f);
        //InvokeRepeating("BoxCollderSize", 0, 1.0f);
    }

    void UpdateTime()//刷新时间
    {
        if (leftTime >= 0 && leftResidue > 0)
        {
            //string houreStr = (leftTime / 3600).ToString("00");
            string miniteStr = (leftTime % 3600 / 60).ToString("00");
            string secondStr = (leftTime % 60).ToString("00");
            //labelLeftTime.text = ((leftTime / 60)).ToString() + ":" + (leftTime % 60).ToString();
            //Tab1.transform.Find("LabelLeftTime").GetComponent<UILabel>().text = string.Format("{0}:{1}:{2}{3}", houreStr, miniteStr, secondStr,"后免费");
            //LabelLeftTime.text = string.Format("{0}{1}:{2}{3}{4}", "[249bd2]", miniteStr, secondStr, "后免费", "[-]");
            LabelLeftTime.text = string.Format("{0}:{1}{2}", miniteStr, secondStr, "后免费");

            if (leftTime == 0)
            {
                LeftOneMoney.text = "免费";
                LabelLeftTime.text = "";
                RedPointLeft.SetActive(true);
                CharacterRecorder.instance.SetRedPoint(6, true);
            }
            else if (leftTime > 0)
            {
                RedPointLeft.SetActive(false);
                if (!RedPointRight.activeSelf)
                {
                    CharacterRecorder.instance.SetRedPoint(6, false);
                }
                LeftOneMoney.text = "20000";
            }
            leftTime -= 1;
        }
        else if (leftResidue == 0)
        {
            
            //LabelLeftTime.text = "凌晨5点重置免费次数";
            LabelLeftTime.text = "";
        }

        if (rightResidue == 0 && rightTime >= 0)
        {
            RightOneMoney.text = "240";
            string houreStr = (rightTime / 3600).ToString("00");
            string miniteStr = (rightTime % 3600 / 60).ToString("00");
            string secondStr = (rightTime % 60).ToString("00");
            //LabelRightTime.text = string.Format("{0}:{1}:{2}{3}", houreStr, miniteStr, secondStr, "后免费");
            LabelRightTime.text = "";
            rightTime -= 1;
            RedPointRight.SetActive(false);
            if (!RedPointLeft.activeSelf)
            {
                CharacterRecorder.instance.SetRedPoint(6, false);
            }
        }
        else if (rightResidue > 0)
        {
            RightOneMoney.text = "免费";
            LabelRightTime.text = "";
            RedPointRight.SetActive(true);
            CharacterRecorder.instance.SetRedPoint(6, true);
        }
        /*        if (rightTime >=0&&rightResidue>0) 
                {
                    string houreStr = (rightTime / 3600).ToString("00");
                    string miniteStr = (rightTime % 3600 / 60).ToString("00");
                    string secondStr = (rightTime % 60).ToString("00");
                    //Tab1.transform.Find("LabelRightTime").GetComponent<UILabel>().text = string.Format("{0}:{1}:{2}{3}", houreStr, miniteStr, secondStr, "后免费");
                    LabelRightTime.text = string.Format("{0}:{1}:{2}{3}", houreStr, miniteStr, secondStr, "后免费");
                    rightTime -= 1;
                    if (rightTime == 0)
                    {
                        LeftOneMoney.text = "免费";
                    }
                    else if (leftTime > 0)
                    {
                        LeftOneMoney.text = "240";
                    }
                }
                else if (rightResidue == 0) 
                {
                    //Tab1.transform.Find("LabelRightTime").GetComponent<UILabel>().text = "凌晨5点重置免费次数";
                    LabelRightTime.text = "凌晨5点重置免费次数";
                }*/
    }


    void SetTenChouKaTexture() 
    {
        StopCoroutine("IETenChouKaTexture");
        StartCoroutine("IETenChouKaTexture");
    }

    IEnumerator IETenChouKaTexture() 
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);

            TenChouKaTexture.from = new Vector3(1f, 1f, 1f);
            TenChouKaTexture.to = new Vector3(1.2f, 1.2f, 1.2f);
            TenChouKaTexture.duration = 0.2f;
            TenChouKaTexture.delay = 0f;

            TenChouKaTexture.ResetToBeginning();

            TenChouKaTexture.PlayForward();
            yield return new WaitForSeconds(0.2f);

            TenChouKaTexture.from = new Vector3(1.2f, 1.2f, 1.2f);
            TenChouKaTexture.to = new Vector3(1f, 1f, 1f);
            TenChouKaTexture.duration = 0.2f;
            TenChouKaTexture.delay = 0f;

            TenChouKaTexture.ResetToBeginning();

            TenChouKaTexture.PlayForward();
        }
    }
}
