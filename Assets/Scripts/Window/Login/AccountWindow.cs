using UnityEngine;
using System.Collections;
using CodeStage.AntiCheat.ObscuredTypes;
using System.Collections.Generic;

public class AccountWindow : MonoBehaviour
{
    public GameObject FrogetWin;
    public GameObject Froget;
    public UIInput userName;
    public UIInput passWord;
    public UILabel userName1;
    public UILabel passWord1;

    //快速登录
    public UIInput QuserName;
    public UIInput QpassWord;
    string QnewUserName;

    //快速验证
    public GameObject Register;
    public UISlider uiSlider;
    public GameObject SPrtie;//denglu
    public GameObject QLigen;
    public UIInput mibao;
    public GameObject AgreeMent;
    public GameObject Hint;

    public GameObject Login;
    public GameObject QuicRegister;
    public UIPopupList nameList;
  public  Dictionary<string, string> nameDic;
    string[] namePros;
    int n;
    string newName;
    bool isFirst=false;

    void Awake()
    {
        nameDic = new Dictionary<string, string>();
        GetList();
        UIEventListener.Get(Froget).onClick += delegate (GameObject go)
          {
              Login.SetActive(false);
              FrogetWin.SetActive(true);
          };
    }
    // Use this for initialization
    void Start()
    {
        if (ObscuredPrefs.GetString("Fisrt", "")=="")
        {
            isFirst = true;
            userName.value = ObscuredPrefs.GetString("Account", "");
            passWord.value = ObscuredPrefs.GetString("Password", "");
            ObscuredPrefs.GetString("Fisrt", "1");
        }
        //if (ObscuredPrefs.GetString("first", "")=="")
        //{
        //    userName.value= ObscuredPrefs.GetString("Account", "");
        //    ObscuredPrefs.SetString("first", "2");
        //}
       
       

        if (PlayerPrefs.GetInt(LuaDeliver.instance.GetGuideStateName()) == 0)
        {
            UIManager.instance.NewGuideAnchor(UIManager.AnchorIndex.index_102);
        }

    }

    public void ClickSubmit()
    {
        if (!string.IsNullOrEmpty(userName.value) && !string.IsNullOrEmpty(passWord.value))
        {
            NetworkHandler.instance.Account = userName.value;
            NetworkHandler.instance.Password = passWord.value;
            //ObscuredPrefs.SetString("Account", userName.value);
            // ObscuredPrefs.SetString("Password", passWord.value);
            //   SetList(userName.value, passWord.value);
            SetList();
            NetworkHandler.instance.SendProcess("1001#" + userName.value + ";"+ passWord.value + ";");
            //AudioEditer.instance.PlayLoop("Scene");
        }
    }
    public void ForgetToLofin()
    {
        FrogetWin.SetActive(false);
        Login.SetActive(true);
    }

    //快速登录
     void QGetUserName()
    {
        if (true)
        {
            QnewUserName = QuserName.value;
            Register.SetActive(false);
            SPrtie.SetActive(true);
            Login.SetActive(false);
            QuicRegister.SetActive(true);
        }
    }
    public void OpenAgre()
    {
        QuicRegister.SetActive(false);
        AgreeMent.SetActive(true);
    }
    public void CloseAgre()
    {
        QuicRegister.SetActive(true);
        AgreeMent.SetActive(false);
    }
    public void OpenRegister()
    {
        Register.SetActive(true);
        SPrtie.SetActive(false);

    }
    public void SetName()
    {
        userName.value= userName1.text;
    }
    public void RegisterToLoign()
    {
        Register.SetActive(false);
        SPrtie.SetActive(true);
        Login.SetActive(true);
        QuicRegister.SetActive(false);
    }
    public void QClickSubmit()//登录游戏
    {
        if (!string.IsNullOrEmpty(QuserName.value) && !string.IsNullOrEmpty(QpassWord.value) && !string.IsNullOrEmpty(mibao.value))
        {
            NetworkHandler.instance.Account = QnewUserName;
            NetworkHandler.instance.Password = QpassWord.value;
            NetworkHandler.instance.SendProcess("1001#" + QnewUserName + ";" + QpassWord.value + ";");
            
            //AudioEditer.instance.PlayLoop("Scene");
        }
    }
    public void BackLogin()//返回登录
    {
        QuicRegister.SetActive(false);
        Login.SetActive(true);
    }
    void SetList()
    {
        string sd = userName.value + ',' + passWord.value;
        string nameAndPro = ObscuredPrefs.GetString("Name", "");
       // print(nameAndPro);
        if (nameDic.Count>3)
        {
            string[] id = nameAndPro.Split('!');
          nameAndPro = id[0] + id[1];
        }
        print(sd + '!' + nameAndPro);
        ObscuredPrefs.SetString("Name", sd + '!' + nameAndPro);
    }
    void CutSpace()
    {
        string[] ba = ObscuredPrefs.GetString("Name", "").Split('!');
        for (int i = 0; i < ba.Length; i++)
        {
            if (ba[i] != ",")
            {
                newName = newName + ba[i];
            }
        }
        ObscuredPrefs.SetString("Name", newName);
    }
    void GetList()
    {
        string nameAndPro = ObscuredPrefs.GetString("Name", "");
        namePros = nameAndPro.Split('!');
        for (int i = 0; i < namePros.Length - 1; i++)
        {
            if (!nameDic.ContainsKey(namePros[i].Split(',')[0]))
            {
                nameDic.Add(namePros[i].Split(',')[0], namePros[i].Split(',')[1]);
                nameList.items.Add(namePros[i].Split(',')[0]);
            }
        }
    }
    public void GetPrass()
    {
      
        if (nameDic.ContainsKey(userName.value))
        {
            
            passWord.value = nameDic[userName.value];
        }
        else
        {
            passWord.value = null;
        }


    }

    public void HintWord()
    {
        if (uiSlider.value != 1)
        {
            StartCoroutine(CloseHint());
        }
        else
        {
            QGetUserName();
        }
    }
    //public void ClickLogin()
    //{
    //    NetworkHandler.instance.IsAuto = true;
    //    float RandName = Random.Range(1000000, 9000000);
    //    NetworkHandler.instance.Account = "u" + RandName.ToString();
    //    NetworkHandler.instance.Password = "p" + RandName.ToString();
    //    NetworkHandler.instance.SendProcess("1001#" + NetworkHandler.instance.Account + ";" + NetworkHandler.instance.Password + ";");
    //}

    public void ClickExit()
    {
        Debug.Log("退出游戏");
    }
    // Update is called once per frame
    void Update()
    {
        if (isFirst == false)
        {
            if (!string.IsNullOrEmpty(ObscuredPrefs.GetString("Name", "")))
            {
                userName.value = userName1.text;
            }
        }
        if (userName.value != "")
        {
            TweenAlpha ta = userName.transform.FindChild("Label").GetComponent<TweenAlpha>();
            ta.enabled = false;
            UILabel uil = userName.transform.FindChild("Label").GetComponent<UILabel>();
            uil.color = Color.black;
        }
        else
        {
            TweenAlpha ta = userName.transform.FindChild("Label").GetComponent<TweenAlpha>();
            ta.enabled = true;
        }
        if (passWord.value != "")
        {
            TweenAlpha ta = passWord.transform.FindChild("Label").GetComponent<TweenAlpha>();
            ta.enabled = false;
            UILabel uil = passWord.transform.FindChild("Label").GetComponent<UILabel>();
            uil.color = Color.black;
        }
        else
        {
            TweenAlpha ta = passWord.transform.FindChild("Label").GetComponent<TweenAlpha>();
            ta.enabled = true;
        }
        Verify();
    }
    void Verify()
    {
        if (!string.IsNullOrEmpty(QuserName.value))
        {
            QLigen.GetComponent<BoxCollider>().enabled=true;
        }
        else
        {
            uiSlider.value = 0;
            QLigen.GetComponent<BoxCollider>().enabled = false;
        }
        if (uiSlider.value >= 0.7)
        {
            uiSlider.value = 1;
        }
        else
        {
            uiSlider.value = uiSlider.value - 0.03f;
        }
        if (uiSlider.value == 1)
        {
            QLigen.GetComponent<UITexture>().color = new Color(1f, 1f, 1f);
        }
        else
        {
            QLigen.GetComponent<UITexture>().color = new Color(0.6f, 0.6f, 0.6f);
        }
       
    }
   IEnumerator CloseHint()
    {
        Hint.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        Hint.SetActive(false);
    }
}
