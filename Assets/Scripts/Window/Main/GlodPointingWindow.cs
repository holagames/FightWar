using UnityEngine;
using System.Collections;

public class GlodPointingWindow : MonoBehaviour {
    //public GameObject Dianjin;
    //public GameObject Baoji;

    public GameObject DianjinGuang;
    public GameObject Chenggong;
    public GameObject DianJin_eff1;

    public GameObject BaojiObj;
    public GameObject Dianjin_eff2;
    public GameObject BaoJi_ui_eff;


    public UILabel Labelbaoji;
    public UILabel LabelMoney;
    public TweenScale Tweenscal;
	void Start () 
    {
        //if (GameObject.Find("RoleWindow") != null || GameObject.Find("GachaWindow") != null)
        //{
        //    this.gameObject.layer = 11;
        //}
        //else
        //{
        //    this.gameObject.layer = 5;
        //}
        this.gameObject.layer = 11;
        if (UIEventListener.Get(GameObject.Find("GlodPointingWindow/BGButton")).onClick == null)
        {
            UIEventListener.Get(GameObject.Find("GlodPointingWindow/BGButton")).onClick += delegate(GameObject go)
            {
                //UIManager.instance.DestroyPanel("GlodPointingWindow");
                UIManager.instance.BackUI();
            };
        }
	}
    //public void SetPointInfo(int Money,int CritNum) 
    //{
    //    if (CritNum > 1)
    //    {
    //        Dianjin.SetActive(false);
    //        Baoji.SetActive(true);
    //        Labelbaoji.text = CritNum.ToString();
    //        LabelMoney.text = "+" + Money.ToString();
    //    }
    //    else
    //    {
    //        Baoji.SetActive(false);
    //        Dianjin.SetActive(true);
    //        LabelMoney.text = "+" + Money.ToString();
    //    }
    //}

    public void SetPointInfo(int Money, int CritNum,int PointNum)
    {
        if (PointNum == 1)
        {
            if (CritNum > 1)
            {
                StartCoroutine(BaoJi(CritNum));
                Labelbaoji.text = CritNum.ToString();
                //LabelMoney.text = "+" + Money.ToString();
            }
            else
            {
                StartCoroutine(Dianjin());
                //LabelMoney.text = "+" + Money.ToString();
            }
        }
        else 
        {
            StartCoroutine(BaoJiTen(CritNum));
            //LabelMoney.text = "+" + Money.ToString();
        }
        StartCoroutine(SetMoney(Money));
    }

    IEnumerator Dianjin() 
    {
        DianJin_eff1.SetActive(true);
        AudioEditer.instance.PlayOneShot("ui_recieve");
        Chenggong.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        //DianjinGuang.SetActive(true);
    }

    IEnumerator BaoJi(int CritNum) 
    {
        Dianjin_eff2.SetActive(true);
        AudioEditer.instance.PlayOneShot("ui_recieve");
        BaojiObj.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        int Num = 0;
        while (Num < CritNum)
        {
            Num += 1;
            if (Num >= CritNum) 
            {
                Num = CritNum;
            }
            Labelbaoji.text =Num.ToString();
            Tweenscal.from = new Vector3(0f, 0, 0);
            Tweenscal.to = new Vector3(1f, 1f, 0);
            Tweenscal.ResetToBeginning();
            Tweenscal.PlayForward();
            BaoJi_ui_eff.layer = 11;
            BaoJi_ui_eff.SetActive(false);
            BaoJi_ui_eff.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator BaoJiTen(int CritNum) 
    {
        Dianjin_eff2.SetActive(true);
        //yield return new WaitForSeconds(0.5f);
        AudioEditer.instance.PlayOneShot("ui_recieve");
        BaojiObj.SetActive(true);
        BaojiObj.transform.Find("Spritebeishu").GetComponent<UISprite>().spriteName = "glodpoint0";
        yield return new WaitForSeconds(0.2f);
        int Num = 0;
        while (Num < CritNum)
        {
            Num += 1;
            if (Num >= CritNum)
            {
                Num = CritNum;
            }
            Labelbaoji.text = Num.ToString();
            Tweenscal.from = new Vector3(0f, 0, 0);
            Tweenscal.to = new Vector3(1f, 1f, 0);
            Tweenscal.ResetToBeginning();
            Tweenscal.PlayForward();
            BaoJi_ui_eff.layer = 11;
            BaoJi_ui_eff.SetActive(false);
            BaoJi_ui_eff.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
    }

    IEnumerator SetMoney(int Money) 
    {
        int MaxNum=0;
        while (MaxNum < Money) 
        {
            MaxNum += 10000;
            if (MaxNum >= Money) 
            {
                MaxNum = Money;
            }
            LabelMoney.text = "+" + MaxNum.ToString();
            yield return new WaitForSeconds(0.01f);
        }
    }
}

