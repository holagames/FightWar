using UnityEngine;
using System.Collections;

public class CountryWarWindow : MonoBehaviour {

    public GameObject UnionContryBoxClick;
    public GameObject TribeContryBoxClick;
    public GameObject RandomChoseButton;
    public GameObject JoinPartWindow;
    public GameObject JoinPartCloseButton;
    public GameObject SureButton;
    public GameObject CancelButton;
    public UILabel MessageInfoLabel;
    public GameObject BottomLabel;
    public GameObject MaskButton;

    public GameObject TongGuangTex;
    public GameObject TongAnTexTex;
    public GameObject TongTackTex;

    public GameObject DiTackTex;
    public GameObject DiGuangTex;
    public GameObject DiAnTexTex;

    public GameObject CloseButton;
    private bool IsOpenAni = true;
	void Start () {
        UIEventListener.Get(UnionContryBoxClick).onClick = delegate(GameObject go)
        {
            StopCoroutine("ChangeToShowAnimation");
            IsOpenAni = false;
            ChoseJoinContry(1);

            TongGuangTex.SetActive(true);
            TongAnTexTex.SetActive(false);
            TongTackTex.SetActive(true);

            DiGuangTex.SetActive(false);
            DiAnTexTex.SetActive(true);
            DiTackTex.SetActive(false);
        };

        UIEventListener.Get(TribeContryBoxClick).onClick = delegate(GameObject go)
        {
            StopCoroutine("ChangeToShowAnimation");
            IsOpenAni = false;
            ChoseJoinContry(2);

            TongGuangTex.SetActive(false);
            TongAnTexTex.SetActive(true);
            TongTackTex.SetActive(false);

            DiGuangTex.SetActive(true);
            DiAnTexTex.SetActive(false);
            DiTackTex.SetActive(true);
        };

        UIEventListener.Get(RandomChoseButton).onClick = delegate(GameObject go)
        {
            NetworkHandler.instance.SendProcess("8102#0;");
        };

        UIEventListener.Get(CancelButton).onClick = delegate(GameObject go)
        {
            JoinPartWindow.SetActive(false);
        };
        UIEventListener.Get(JoinPartCloseButton).onClick = delegate(GameObject go)
        {
            JoinPartWindow.SetActive(false);
        };

        //UIEventListener.Get(MaskButton).onClick = delegate(GameObject go)
        //{
        //    UIManager.instance.BackUI();
        //};


        //UIEventListener.Get(CloseButton).onClick = delegate(GameObject go)
        //{
        //    UIManager.instance.BackUI();
        //};


        StopCoroutine("ChangeToShowAnimation");
        StartCoroutine("ChangeToShowAnimation");
	}


    IEnumerator ChangeToShowAnimation() 
    {
        TongAnTexTex.SetActive(false);
        DiAnTexTex.SetActive(false);
        TongTackTex.SetActive(true);
        DiTackTex.SetActive(true);
        DiGuangTex.SetActive(false);

        while (IsOpenAni) 
        {
            if (IsOpenAni) { TongGuangTex.SetActive(true); }              
            yield return new WaitForSeconds(0.1f);
            if (IsOpenAni) { TongGuangTex.SetActive(false); }           
            yield return new WaitForSeconds(0.1f);

            if (IsOpenAni) { TongGuangTex.SetActive(true); }             
            yield return new WaitForSeconds(0.1f);

            if (IsOpenAni) { TongGuangTex.SetActive(false); }               
            yield return new WaitForSeconds(0.1f);

            if (IsOpenAni) { TongGuangTex.SetActive(true); }            
            yield return new WaitForSeconds(0.1f);

            if (IsOpenAni) { TongGuangTex.SetActive(false); }             
            yield return new WaitForSeconds(0.5f);


            if (IsOpenAni) { DiGuangTex.SetActive(true); }
            yield return new WaitForSeconds(0.1f);
            if (IsOpenAni) { DiGuangTex.SetActive(false); }
            yield return new WaitForSeconds(0.1f);

            if (IsOpenAni) { DiGuangTex.SetActive(true); }
            yield return new WaitForSeconds(0.1f);

            if (IsOpenAni) { DiGuangTex.SetActive(false); }
            yield return new WaitForSeconds(0.1f);

            if (IsOpenAni) { DiGuangTex.SetActive(true); }
            yield return new WaitForSeconds(0.1f);

            if (IsOpenAni) { DiGuangTex.SetActive(false); }
            yield return new WaitForSeconds(0.5f);
        }
    }


    void ChoseJoinContry(int contryId) 
    {
        JoinPartWindow.SetActive(true);
        switch (contryId) 
        {
            case 0:
                break;
            case 1:
                BottomLabel.SetActive(false);
                MessageInfoLabel.text = "您选择 [ffff00]同盟[-] 是否确认加入？";
                UIEventListener.Get(SureButton).onClick = delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess("8102#1;");
                    JoinPartWindow.SetActive(false);
                };
                break;
            case 2:
                BottomLabel.SetActive(false);
                MessageInfoLabel.text = "您选择 [ffff00]帝国[-] 是否确认加入？";
                UIEventListener.Get(SureButton).onClick = delegate(GameObject go)
                {
                    NetworkHandler.instance.SendProcess("8102#2;");
                    JoinPartWindow.SetActive(false);
                };
                break;
        }
    }
}
