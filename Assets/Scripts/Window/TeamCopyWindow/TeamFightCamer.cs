using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeamFightCamer : MonoBehaviour {

    public GameObject TansuoScence;//场景加载
    //public GameObject TeamFightCamer;//摄像机
    public GameObject HeroObj;//模型父物体;
    public GameObject ParentObj;//组队人物父物体; 
    public List<GameObject> BossList;
    public List<GameObject> HeroList;

    public GameObject OtherScence;//其他场景

    public int KillBossNum=0;
	void Start () {
	
	}


    public void AddSenceWindow(int teamId) //加载场景
    {
        Debug.Log("加载组队副本场景");
        SceneTransformer.instance.ShowMainScene(false);
        {
            GameObject tansuo = Instantiate(TansuoScence) as GameObject;
            tansuo.transform.position = new Vector3(0, 0, 0);
            tansuo.transform.localScale = new Vector3(1, 1, 1);
            tansuo.name = "CopyScence";
        }
        LightMapAsset lightAsset = Resources.Load("LightMapAsset/lightMapAsset_127") as LightMapAsset;
        int count = lightAsset.lightmapFar.Length;
        LightmapData[] lightmapDatas = new LightmapData[count];
        for (int i = 0; i < count; i++)
        {
            LightmapData lightmapData = new LightmapData();
            lightmapData.lightmapFar = lightAsset.lightmapFar[i];
            lightmapData.lightmapNear = lightAsset.lightmapNear[i];
            lightmapDatas[i] = lightmapData;
        }
        LightmapSettings.lightmaps = lightmapDatas;
        RenderSettings.fog = false;
        Debug.Log("加载雾效结束");
        InstantiateHero();

        Debug.Log("加载组队副本场景");
    }


    /// <summary>
    /// 生成人物模型
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Number"></param>
    /// <param name="ObjPosition"></param>
    private void InstantiateHero()//string Id, int Number, int Num, bool isNextFloor
    {
        GameObject parentObj = GameObject.Instantiate(ParentObj) as GameObject;
        parentObj.SetActive(true);
        parentObj.transform.parent = this.gameObject.transform;//GameObject.Find("CopyScence").transform;
        //parentObj.transform.localPosition = new Vector3(8f, 1.5f, -10f);
        parentObj.transform.localPosition = new Vector3(13f, 1.5f, -10f);

        for (int i = 0; i < CharacterRecorder.instance.CopyHeroIconList.Count; i++)
        {
            Debug.Log("加载人物ID"+CharacterRecorder.instance.CopyHeroIconList[i]);
            GameObject heroObj = GameObject.Instantiate(HeroObj) as GameObject;
            heroObj.SetActive(true);
            heroObj.transform.parent = parentObj.transform;
            //heroObj.transform.localPosition = new Vector3((0 - 3f * i), -1.5f, 0f);
            heroObj.transform.localPosition = new Vector3((0 - 3.35f * i), -1.5f, 0+0.2f*i);
            heroObj.name = CharacterRecorder.instance.CopyHeroIconList[i].ToString();

            GameObject obj = GameObject.Instantiate(Resources.Load("Prefab/Role/" + CharacterRecorder.instance.CopyHeroIconList[i], typeof(GameObject))) as GameObject;
            HeroInfo _heroInfo = TextTranslator.instance.GetHeroInfoByHeroID(CharacterRecorder.instance.CopyHeroIconList[i]);
            float heroScale=0;
            heroScale = (float)_heroInfo.heroScale/1f ;
            obj.transform.parent = heroObj.transform;
            //obj.transform.localScale = new Vector3(4f, 4f, 4f);
            //obj.transform.localScale = new Vector3(3.5f*heroScale, 3.5f*heroScale, 3.5f*heroScale);
            obj.transform.localScale = new Vector3(1.5f * heroScale, 1.5f * heroScale, 1.5f * heroScale);
            obj.transform.localPosition = new Vector3(0f, 0f, 0f);
            obj.transform.Rotate(0, 90, 0);
            obj.name = CharacterRecorder.instance.CopyHeroIconList[i].ToString();
            //BossList.Add(obj);
            HeroList.Add(obj);
        }
        InstantiateBoss();
    }

    /// <summary>
    /// 生成Boss模型
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Number"></param>
    /// <param name="ObjPosition"></param>
    private void InstantiateBoss()//string Id, int Number, int Num, bool isNextFloor
    {
        int i = 0;
        foreach (var TeamGate in TextTranslator.instance.TeamGateList) 
        {
            if (TeamGate.GroupID == CharacterRecorder.instance.CopyNumber) 
            {
                Debug.Log("加载BossID" + TeamGate.BossID);
                GameObject BossObj = GameObject.Instantiate(HeroObj) as GameObject;
                BossObj.SetActive(true);
                BossObj.transform.parent = GameObject.Find("CopyScence").transform;
            //BossObj.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                BossObj.transform.localScale = new Vector3(1f, 1f, 1f);
                BossObj.transform.localPosition = new Vector3(20f + 20 * i, 0f, -10f);
                BossObj.name = (i + 1).ToString();

                GameObject obj = GameObject.Instantiate(Resources.Load("Prefab/Role/" + TeamGate.BossID, typeof(GameObject))) as GameObject;
                obj.transform.parent = BossObj.transform;
                obj.transform.localScale = new Vector3(3f, 3f, 3f);
                obj.transform.localPosition = new Vector3(0f,0f, 0f);
                obj.transform.Rotate(0, -90, 0);
            //obj.transform.localRotation = new Quaternion(0, -180, 0, 0);
                obj.name = TeamGate.BossID.ToString();
                BossList.Add(BossObj);
                i++;
            }
        }
        MoveTeam();
    }

    /// <summary>
    /// 移动队伍
    /// </summary>
    private void MoveTeam() 
    {
        StartCoroutine(MoveToBoss());
    }

    IEnumerator MoveToBoss() 
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < HeroList.Count; i++)
        {
            HeroList[i].GetComponent<Animator>().SetFloat("ft", 0);           
        }
        yield return new WaitForSeconds(0.1f);

        while (this.gameObject.transform.position.x+16 < BossList[0].transform.localPosition.x) 
        {
            yield return new WaitForSeconds(0.01f);
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.localPosition.x + 0.2f, 0f, 0f);
        }

        for (int i = 0; i < HeroList.Count; i++)
        {
            HeroList[i].GetComponent<Animator>().SetFloat("ft", 2);
            HeroList[i].GetComponent<Animator>().Play("attack");
            FightMotion fm2 = TextTranslator.instance.fightMotionDic[int.Parse(HeroList[i].name) * 10 + 1];
            PlayEffect(HeroList[i], 0, fm2);
        }
        //yield return new WaitForSeconds(0.2f);

        //BossList[0].transform.GetChild(0).GetComponent<Animator>().Play("hurt");
        Debug.Log("攻击了第1个Boss");
        if (CharacterRecorder.instance.TeamPosition == 1)
        {
            NetworkHandler.instance.SendProcess("6107#" + CharacterRecorder.instance.TeamID + ";");
        }
        //yield return new WaitForSeconds(1f);
    }



    public void SetKillBoss() 
    {
        StartCoroutine(MoveToKillBoss());
    }
    IEnumerator MoveToKillBoss()
    {
        BossList[KillBossNum].transform.GetChild(0).GetComponent<Animator>().Play("dead");
        yield return new WaitForSeconds(0.5f);
        BossList[KillBossNum].SetActive(false);
        KillBossNum += 1;

        if (KillBossNum < 10)
        {
            for (int i = 0; i < HeroList.Count; i++) 
            {
                HeroList[i].GetComponent<Animator>().SetFloat("ft", 0);
            }
            yield return new WaitForSeconds(0.45f);
            while (this.gameObject.transform.position.x + 16 < BossList[KillBossNum].transform.localPosition.x)
            {
                yield return new WaitForSeconds(0.01f);
                this.gameObject.transform.position = new Vector3(this.gameObject.transform.localPosition.x + 0.2f, 0f, 0f);
            }
            for (int i = 0; i < HeroList.Count; i++)
            {
                HeroList[i].GetComponent<Animator>().SetFloat("ft", 2);
                HeroList[i].GetComponent<Animator>().Play("attack");
                
                FightMotion fm2 = TextTranslator.instance.fightMotionDic[int.Parse(HeroList[i].name) * 10 + 1];
                PlayEffect(HeroList[i],0,fm2);
            }

            //yield return new WaitForSeconds(0.2f);
            //BossList[KillBossNum].transform.GetChild(0).GetComponent<Animator>().Play("hurt");
            Debug.Log("攻击了第" + (KillBossNum+1) + "个Boss");
            if (CharacterRecorder.instance.TeamPosition == 1) //CharacterRecorder.instance.IsCaptain
            {
                NetworkHandler.instance.SendProcess("6107#" + CharacterRecorder.instance.TeamID + ";");
            }

            if (GameObject.Find("TeamFightChoseWindow") != null)
            {
                GameObject.Find("TeamFightChoseWindow").GetComponent<TeamFightChoseWindow>().SetValuePosition(KillBossNum);
            }
            //yield return new WaitForSeconds(1f);
        }
        else 
        {
            yield return new WaitForSeconds(0.2f);
            GameObject.Find("TeamFightChoseWindow").GetComponent<TeamFightChoseWindow>().SetLastAwardWindow();
        }
    }

    public void FightChoseWindowMission() //失败所有英雄消失，boss播放防御动画
    {
        BossList[KillBossNum].transform.GetChild(0).GetComponent<Animator>().Play("hurt");
        for (int i = 0; i < HeroList.Count; i++)
        {
            HeroList[i].SetActive(false);
        }
    }


    GameObject FindChildObject(GameObject RootObject, string ObjectName)
    {
        foreach (Component c in RootObject.GetComponentsInChildren(typeof(Transform), true))
        {
            if (c.name == ObjectName)
            {
                return c.gameObject;
            }
        }
        return null;
    }

    public void PlayEffect(GameObject HeroObj,int RoleIndex,FightMotion fm)
    {
        //HeroInfo _heroInfo = TextTranslator.instance.GetHeroInfoByHeroID(int.Parse(HeroObj.name));
        //float heroScale = (float)_heroInfo.heroScale / 1f;
        for (int i = 0; i < fm.effectList.size; i++)
        {
            FightEffect fe = TextTranslator.instance.fightEffectDic[fm.effectList[i]];           
            if (fe.effect != "0")
            {
                if (fe.projectile == 9999)
                {
                    if (fe.effectParent != "0")
                    {
                        EffectMaker.instance.Create2DEffect("~" + fe.effect, "", null, new Vector3(transform.position.x, transform.position.y , 0), new Vector3(transform.position.x, transform.position.y, 0), transform.localEulerAngles, 0.04f, fm.effectTimeList[i] / 100f, 8f, 1, 1, RoleIndex, null, null, null, null, null, false, false, false, true, false, false, false, false, true, "", null);
                    }
                    else
                    {
                        EffectMaker.instance.Create2DEffect("~" + fe.effect, "", null, new Vector3(transform.position.x, transform.position.y, 0), new Vector3(transform.position.x, transform.position.y, 0), transform.localEulerAngles, 0.04f, fm.effectTimeList[i] / 100f, 8f, 1, 1, RoleIndex, null, null, null, null, null, false, false, false, true, false, false, false, false, true, "", null);
                    }
                }
                else
                {
                    if (fe.effectParent != "0")
                    {
                        EffectMaker.instance.Create2DEffect("~" + fe.effect, "", null, HeroObj.transform.position + new Vector3(fe.effectPosX / 100f, 0, fe.effectPosY / 100f) + new Vector3(1f, 0.5f, 0f), HeroObj.transform.position + new Vector3(fe.effectPosX / 100f, 0, fe.effectPosY / 100f) + new Vector3(1f, 0.5f, 0f), HeroObj.transform.localEulerAngles, 0.04f, fm.effectTimeList[i] / 100f, 8, 1, 1, RoleIndex, null, null, null, null, null, false, false, false, false, false, false, false, false, true, "", null);
                    }
                    else
                    {
                        EffectMaker.instance.Create2DEffect("~" + fe.effect, "", null, HeroObj.transform.position + new Vector3(fe.effectPosX / 100f, 0, fe.effectPosY / 100f) + new Vector3(1f, 0.5f, 0f), HeroObj.transform.position + new Vector3(fe.effectPosX / 100f, 0, fe.effectPosY / 100f) + new Vector3(1f, 0.5f, 0f), HeroObj.transform.localEulerAngles, 0.04f, fm.effectTimeList[i] / 100f, 8, 1, 1, RoleIndex, null, null, null, null, null, false, false, false, false, false, false, false, false, true, "", null);
                    }
                }
            }
            StartCoroutine(AudioEditer.instance.DelaySound(fe.soundDelay, fe.sound));
        }
    }
}
