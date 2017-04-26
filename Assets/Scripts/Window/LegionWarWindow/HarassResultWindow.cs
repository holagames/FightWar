using UnityEngine;
using System.Collections;

public class HarassResultWindow : MonoBehaviour {

    public GameObject BackButton;
    public GameObject EffectCG;
    public GameObject EffectSB;
    public GameObject EffectGuang;
    public GameObject EffectXiaYiBo;
    public GameObject EffectZLSB;

    public GameObject EffectSRCG;
    public GameObject EffectTZCG;
    public GameObject EffectTZSB;

    int witchBachButton = 0;
	void Start () {

        Invoke("AutoBackButton", 5f);
	}

    public void TakeTenCityResultWithWin() //占领10个城市据点后，占领成功特效改为骚扰成功
    {
        BackButton.SetActive(true);
        gameObject.transform.Find("SpriteBg").gameObject.SetActive(true);

        UIEventListener.Get(BackButton).onClick = delegate(GameObject go)  //军团战或骚扰结束后主动呼叫当前编队信息 ，当前城市点攻方信息
        {
            CharacterRecorder.instance.AutomaticbrushCityID = 0;
            NetworkHandler.instance.SendProcess("8636#;");
            NetworkHandler.instance.SendProcess("8602#" + CharacterRecorder.instance.LegionHarasPoint + ";");
            PictureCreater.instance.StopFight(true);
            UIManager.instance.BackUI();
        };


        //transform.Find("SpriteBg/Label2").gameObject.SetActive(true);
        GameObject effectsrcg = Instantiate(EffectSRCG) as GameObject;
        effectsrcg.name = "effectsrcg";
        effectsrcg.transform.parent = gameObject.transform;
        effectsrcg.transform.localPosition = new Vector3(0, 0, 0);
        effectsrcg.transform.localScale = new Vector3(300, 300, 300);
        effectsrcg.SetActive(true);

        GameObject effectguang = Instantiate(EffectGuang) as GameObject;
        effectguang.name = "effectguang";
        effectguang.transform.parent = gameObject.transform;
        effectguang.transform.localPosition = new Vector3(0, 370, 0);
        effectguang.transform.localScale = new Vector3(400, 400, 400);
        effectguang.SetActive(true);

        TweenScale TweenBg = gameObject.transform.Find("SpriteBg").GetComponent<TweenScale>();
        TweenBg.from = new Vector3(1f, 0, 1);
        TweenBg.to = new Vector3(1f, 1f, 1);
        TweenBg.ResetToBeginning();
        TweenBg.PlayForward();
    }

    public void LegionWarStartWar(bool IsWin) //骚扰特效
    {
        BackButton.SetActive(true);
        gameObject.transform.Find("SpriteBg").gameObject.SetActive(true);
        
        UIEventListener.Get(BackButton).onClick = delegate(GameObject go)  //军团战或骚扰结束后主动呼叫当前编队信息 ，当前城市点攻方信息
        {
            CharacterRecorder.instance.AutomaticbrushCityID = 0;
            NetworkHandler.instance.SendProcess("8636#;");
            NetworkHandler.instance.SendProcess("8602#" + CharacterRecorder.instance.LegionHarasPoint + ";");
            PictureCreater.instance.StopFight(true);
            UIManager.instance.BackUI();          
        };

        if (IsWin)
        {
            transform.Find("SpriteBg/Label").gameObject.SetActive(false);
            if (CharacterRecorder.instance.legionID == 0) //未加入军团
            {
                transform.Find("SpriteBg/Label2").gameObject.SetActive(true);
                GameObject effectsrcg = Instantiate(EffectSRCG) as GameObject;
                effectsrcg.name = "effectsrcg";
                effectsrcg.transform.parent = gameObject.transform;
                effectsrcg.transform.localPosition = new Vector3(0, 0, 0);
                effectsrcg.transform.localScale = new Vector3(300, 300, 300);
                effectsrcg.SetActive(true);
            }
            else
            {
                GameObject effectcg = Instantiate(EffectCG) as GameObject;
                effectcg.name = "effectcg";
                effectcg.transform.parent = gameObject.transform;
                effectcg.transform.localPosition = new Vector3(0, 0, 0);
                effectcg.transform.localScale = new Vector3(300, 300, 300);
                effectcg.SetActive(true);
            }


            GameObject effectguang = Instantiate(EffectGuang) as GameObject;
            effectguang.name = "effectguang";
            effectguang.transform.parent = gameObject.transform;
            effectguang.transform.localPosition = new Vector3(0, 370, 0);
            effectguang.transform.localScale = new Vector3(400, 400, 400);
            effectguang.SetActive(true);

            TweenScale TweenBg = gameObject.transform.Find("SpriteBg").GetComponent<TweenScale>();
            TweenBg.from = new Vector3(1f, 0, 1);
            TweenBg.to = new Vector3(1f, 1f, 1);
            TweenBg.ResetToBeginning();
            TweenBg.PlayForward();
        }
        else 
        {
            //GainResultPart.transform.Find("WinParent").gameObject.SetActive(false);
            //GainResultPart.transform.Find("LoseParent").gameObject.SetActive(true);
            transform.Find("SpriteBg/Label").gameObject.SetActive(true);
            GameObject effectsb = Instantiate(EffectSB) as GameObject;
            effectsb.name = "effectsb";
            effectsb.transform.parent = gameObject.transform;
            effectsb.transform.localPosition = new Vector3(0, 0, 0);
            effectsb.transform.localScale = new Vector3(300, 300, 300);
            effectsb.SetActive(true);

            GameObject effectguang = Instantiate(EffectGuang) as GameObject;
            effectguang.name = "effectguang";
            effectguang.transform.parent = gameObject.transform;
            effectguang.transform.localPosition = new Vector3(0, 370, 0);
            effectguang.transform.localScale = new Vector3(400, 400, 400);
            effectguang.SetActive(true);

            TweenScale TweenBg = gameObject.transform.Find("SpriteBg").GetComponent<TweenScale>();
            TweenBg.from = new Vector3(1f, 0, 1);
            TweenBg.to = new Vector3(1f, 1f, 1);
            TweenBg.ResetToBeginning();
            TweenBg.PlayForward();
        }
    }

    public void LegionWarStartWar() //过了时间,占领失败
    {
        BackButton.SetActive(true);
        gameObject.transform.Find("SpriteBg").gameObject.SetActive(true);

        UIEventListener.Get(BackButton).onClick = delegate(GameObject go)  //军团战或骚扰结束后主动呼叫当前编队信息 ，当前城市点攻方信息
        {
            CharacterRecorder.instance.AutomaticbrushCityID = 0;
            NetworkHandler.instance.SendProcess("8636#;");
            NetworkHandler.instance.SendProcess("8602#" + CharacterRecorder.instance.LegionHarasPoint + ";");
            PictureCreater.instance.StopFight(true);
            UIManager.instance.BackUI();
        };

        transform.Find("SpriteBg/Label").gameObject.SetActive(false);
        transform.Find("SpriteBg/Label2").gameObject.SetActive(true);
        transform.Find("SpriteBg/Label2").GetComponent<UILabel>().text = "每日8：30~21：00开放军团战";
        GameObject effectzlsb = Instantiate(EffectZLSB) as GameObject;
        effectzlsb.name = "effectzlsb";
        effectzlsb.transform.parent = gameObject.transform;
        effectzlsb.transform.localPosition = new Vector3(0, 0, 0);
        effectzlsb.transform.localScale = new Vector3(300, 300, 300);
        effectzlsb.SetActive(true);


        GameObject effectguang = Instantiate(EffectGuang) as GameObject;
        effectguang.name = "effectguang";
        effectguang.transform.parent = gameObject.transform;
        effectguang.transform.localPosition = new Vector3(0, 370, 0);
        effectguang.transform.localScale = new Vector3(400, 400, 400);
        effectguang.SetActive(true);

        TweenScale TweenBg = gameObject.transform.Find("SpriteBg").GetComponent<TweenScale>();
        TweenBg.from = new Vector3(1f, 0, 1);
        TweenBg.to = new Vector3(1f, 1f, 1);
        TweenBg.ResetToBeginning();
        TweenBg.PlayForward();
    }



    public void NextEnemy() 
    {
        StartCoroutine(StaySomeTime());
    }

    IEnumerator StaySomeTime() 
    {
        GameObject effectxiayibo = Instantiate(EffectXiaYiBo) as GameObject;
        effectxiayibo.transform.parent = EffectXiaYiBo.transform.parent;
        effectxiayibo.transform.localPosition = EffectXiaYiBo.transform.localPosition;
        effectxiayibo.transform.localScale = EffectXiaYiBo.transform.localScale;
        effectxiayibo.SetActive(true);
        yield return new WaitForSeconds(3f);
        DestroyImmediate(this.gameObject);
    }


    public void MilitaryRankEffect(bool Win,int ID)
    {
        BackButton.SetActive(true);
        gameObject.transform.Find("SpriteBg").gameObject.SetActive(true);
        witchBachButton = 1;
        UIEventListener.Get(BackButton).onClick = delegate(GameObject go)
        {
            CharacterRecorder.instance.AutomaticbrushCityID = 0;
            PictureCreater.instance.StopFight(true);

            //UIManager.instance.BackTwoUI("LegionWarWindow");
            UIManager.instance.BackUI();
        };

        if (Win)
        {
            if (CharacterRecorder.instance.NationID == 0) 
            {
                transform.Find("SpriteBg/Label3").gameObject.SetActive(true);
                transform.Find("SpriteBg/Label3").GetComponent<UILabel>().text = "恭喜您成功晋级为[ff8c04]" + TextTranslator.instance.GetNationByID(ID).OfficeName + "[-],可在军衔排行中查看奖励";
            }
            else if (TextTranslator.instance.GetNationByID(ID).Condition > TextTranslator.instance.GetNationByID(CharacterRecorder.instance.NationID).Condition) 
            {
                transform.Find("SpriteBg/Label3").gameObject.SetActive(true);
                transform.Find("SpriteBg/Label3").GetComponent<UILabel>().text = "恭喜您成功晋级为[ff8c04]" + TextTranslator.instance.GetNationByID(ID).OfficeName+ "[-],可在军衔排行中查看奖励";
            }
            GameObject effecttzcg = Instantiate(EffectTZCG) as GameObject;
            effecttzcg.name = "effecttzcg";
            effecttzcg.transform.parent = gameObject.transform;
            effecttzcg.transform.localPosition = new Vector3(0, 0, 0);
            effecttzcg.transform.localScale = new Vector3(300, 300, 300);
            effecttzcg.SetActive(true);

            GameObject effectguang = Instantiate(EffectGuang) as GameObject;
            effectguang.name = "effectguang";
            effectguang.transform.parent = gameObject.transform;
            effectguang.transform.localPosition = new Vector3(0, 370, 0);
            effectguang.transform.localScale = new Vector3(400, 400, 400);
            effectguang.SetActive(true);

            TweenScale TweenBg = gameObject.transform.Find("SpriteBg").GetComponent<TweenScale>();
            TweenBg.from = new Vector3(1f, 0, 1);
            TweenBg.to = new Vector3(1f, 1f, 1);
            TweenBg.ResetToBeginning();
            TweenBg.PlayForward();

            CharacterRecorder.instance.NationID = ID;
        }
        else
        {
            GameObject effecttzsb = Instantiate(EffectTZSB) as GameObject;
            effecttzsb.name = "effecttzsb";
            effecttzsb.transform.parent = gameObject.transform;
            effecttzsb.transform.localPosition = new Vector3(0, 0, 0);
            effecttzsb.transform.localScale = new Vector3(300, 300, 300);
            effecttzsb.SetActive(true);

            GameObject effectguang = Instantiate(EffectGuang) as GameObject;
            effectguang.name = "effectguang";
            effectguang.transform.parent = gameObject.transform;
            effectguang.transform.localPosition = new Vector3(0, 370, 0);
            effectguang.transform.localScale = new Vector3(400, 400, 400);
            effectguang.SetActive(true);

            TweenScale TweenBg = gameObject.transform.Find("SpriteBg").GetComponent<TweenScale>();
            TweenBg.from = new Vector3(1f, 0, 1);
            TweenBg.to = new Vector3(1f, 1f, 1);
            TweenBg.ResetToBeginning();
            TweenBg.PlayForward();
        }
    }


    void AutoBackButton() //自动退出
    {
        if (CharacterRecorder.instance.AutomaticbrushCityID == CharacterRecorder.instance.LegionHarasPoint && witchBachButton==0)//返回的点与自动选择的点同,战斗结果自动跳出
        {
            Debug.Log("自动跳出*********" + CharacterRecorder.instance.AutomaticbrushCityID + "   " + CharacterRecorder.instance.LegionHarasPoint);
            CharacterRecorder.instance.AutomaticbrushCityID = 0;
            //NetworkHandler.instance.SendProcess("8636#;");
            //NetworkHandler.instance.SendProcess("8602#" + CharacterRecorder.instance.LegionHarasPoint + ";");
            PictureCreater.instance.StopFight(true);
            UIManager.instance.BackUI();
        }
    }
}
