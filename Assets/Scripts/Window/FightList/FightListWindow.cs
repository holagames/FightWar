using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FightListWindow : MonoBehaviour
{
    public GameObject HeroHead1;
    public GameObject HeroHead2;
    public GameObject HeroHead3;
    [SerializeField]
    private List<GameObject> PraiseButtonList = new List<GameObject>();
    private string rankType;
    void OnEnable()
    {
        NetworkHandler.instance.SendProcess("6004#3;");
    }
    void OnDestroy()
    {
        for (int i = 0; i < PraiseButtonList.Count; i++)
        {
            UIEventListener.Get(PraiseButtonList[i]).onClick -= ClickPraiseButton;
        }
    }
    // Use this for initialization
    void Start()
    {
      /*  if (UIEventListener.Get(GameObject.Find("FightCloseButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("FightCloseButton")).onClick += delegate(GameObject go)
            {
                UIManager.instance.BackUI();
            };
        }*/

        if (UIEventListener.Get(GameObject.Find("SpriteListButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("SpriteListButton")).onClick += delegate(GameObject go)
            {
                UIManager.instance.OpenPanel("RankListWindow", false);
            };
        }

        if (UIEventListener.Get(GameObject.Find("SpriteTab1")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("SpriteTab1")).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("6004#3;");
            };
        }

        if (UIEventListener.Get(GameObject.Find("SpriteTab2")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("SpriteTab2")).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess("6004#4;");
            };
        }
        for (int i = 0; i < PraiseButtonList.Count; i++)
        {
            UIEventListener.Get(PraiseButtonList[i]).onClick = ClickPraiseButton;
        }
      /*  if (UIEventListener.Get(GameObject.Find("PraiseButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("PraiseButton")).onClick += delegate(GameObject go)
            {
                NetworkHandler.instance.SendProcess(string.Format("1141#{0}",go.name));
            };
        }*/
    }
    private void ClickPraiseButton(GameObject go)
    {
        if (go != null)
        {
            NetworkHandler.instance.SendProcess(string.Format("1141#{0};{1};", go.name, rankType));
        }
    }
    public void PraiseResult(string uId)
    {
        //Debug.Log(uId);
        for (int i = 0; i < PraiseButtonList.Count;i++)
        {
            if (PraiseButtonList[i].name == uId)
            {
                GameObject targeLabel = PraiseButtonList[i].transform.parent.FindChild("LabelNum").gameObject;
                StartCoroutine(PlayPraiseEffect(targeLabel));
            }
        }
    }
    IEnumerator PlayPraiseEffect(GameObject _target)
    {
        if (_target.GetComponent<TweenScale>() == null)
        {
            _target.AddComponent<TweenScale>();
        }
        else
        {
        }
        TweenScale _Ts = _target.GetComponent<TweenScale>();
        _Ts.from = Vector3.one;
        _Ts.to = Vector3.one * 1.2f;
        _Ts.duration = 1.0f;
        _Ts.PlayForward();
        yield return new WaitForSeconds(1.0f);
        _Ts.PlayReverse();
    }
    public void SetInfo(string type, string RankNumber, string uId, string Name, string HeroIcon, string HeroLevel, string PowerNumber, string JiFen, string sumPraise)
    {
        rankType = type;
        switch (type)
        {
            case "3":
                switch (RankNumber)
                {
                    case "1":
                        HeroHead1.transform.FindChild("LabelName").GetComponent<UILabel>().text = Name;
                        HeroHead1.transform.FindChild("LabelLevel").GetComponent<UILabel>().text = string.Format("战力:[89cef3]{0}", PowerNumber);
                        HeroHead1.transform.FindChild("HeroIcon").GetComponent<UISprite>().spriteName = HeroIcon;
                        HeroHead1.transform.FindChild("Sprite").FindChild("LabelNum").GetComponent<UILabel>().text = sumPraise.ToString();
                        PraiseButtonList[0].name = uId;
                        break;
                    case "2":
                        HeroHead2.transform.FindChild("LabelName").GetComponent<UILabel>().text = Name;
                        HeroHead2.transform.FindChild("LabelLevel").GetComponent<UILabel>().text = string.Format("战力:[89cef3]{0}", PowerNumber);
                        HeroHead2.transform.FindChild("HeroIcon").GetComponent<UISprite>().spriteName = HeroIcon;
                        HeroHead2.transform.FindChild("Sprite").FindChild("LabelNum").GetComponent<UILabel>().text = sumPraise.ToString();
                        PraiseButtonList[1].name = uId;
                        break;
                    case "3":
                        HeroHead3.transform.FindChild("LabelName").GetComponent<UILabel>().text = Name;
                        HeroHead3.transform.FindChild("LabelLevel").GetComponent<UILabel>().text = string.Format("战力:[89cef3]{0}", PowerNumber);
                        HeroHead3.transform.FindChild("HeroIcon").GetComponent<UISprite>().spriteName = HeroIcon;
                        HeroHead3.transform.FindChild("Sprite").FindChild("LabelNum").GetComponent<UILabel>().text = sumPraise.ToString();
                        PraiseButtonList[2].name = uId;
                        break;
                    default:
                        break;
                }
                break;
            case "4":
                switch (RankNumber)
                {
                    case "1":
                        HeroHead1.transform.FindChild("LabelName").GetComponent<UILabel>().text = Name;
                        HeroHead1.transform.FindChild("LabelLevel").GetComponent<UILabel>().text = string.Format("等级:[89cef3]{0}", HeroLevel);
                        HeroHead1.transform.FindChild("HeroIcon").GetComponent<UISprite>().spriteName = HeroIcon;
                        HeroHead1.transform.FindChild("Sprite").FindChild("LabelNum").GetComponent<UILabel>().text = sumPraise.ToString();
                        PraiseButtonList[0].name = uId;
                        break;
                    case "2":
                        HeroHead2.transform.FindChild("LabelName").GetComponent<UILabel>().text = Name;
                        HeroHead2.transform.FindChild("LabelLevel").GetComponent<UILabel>().text = string.Format("等级:[89cef3]{0}", HeroLevel);
                        HeroHead2.transform.FindChild("HeroIcon").GetComponent<UISprite>().spriteName = HeroIcon;
                        HeroHead2.transform.FindChild("Sprite").FindChild("LabelNum").GetComponent<UILabel>().text = sumPraise.ToString();
                        PraiseButtonList[1].name = uId;
                        break;
                    case "3":
                        HeroHead3.transform.FindChild("LabelName").GetComponent<UILabel>().text = Name;
                        HeroHead3.transform.FindChild("LabelLevel").GetComponent<UILabel>().text = string.Format("等级:[89cef3]{0}", HeroLevel);
                        HeroHead3.transform.FindChild("HeroIcon").GetComponent<UISprite>().spriteName = HeroIcon;
                        HeroHead3.transform.FindChild("Sprite").FindChild("LabelNum").GetComponent<UILabel>().text = sumPraise.ToString();
                        PraiseButtonList[2].name = uId;
                        break;
                    default:
                        break;
                }
                break;
        }
    }
}
