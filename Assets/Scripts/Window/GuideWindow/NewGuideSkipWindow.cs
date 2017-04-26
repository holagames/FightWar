using UnityEngine;
using System.Collections;

public class NewGuideSkipWindow : MonoBehaviour
{


    public GameObject GotoButton;
    public GameObject ActivityIcon;
    public GameObject ActivitySkillIcon;
    public GameObject ActivityItemEffect;
    public GameObject ActivitySkillEffect;
    public GameObject ActivityMessage;
    public GameObject ActivityName;
    private string message;
    private string Name;
    // Use this for initialization
    void Start()
    {

        NewGuide _NewGuide = TextTranslator.instance.FindNewGuideNowOpenByCurLevel(CharacterRecorder.instance.lastGateID - 1);
        if (_NewGuide == null)
        {
            return;
        }
        if (GameObject.Find("MapUiWindow") != null)
        {
            AudioEditer.instance.PlayOneShot("ui_openbox");
            if (CharacterRecorder.instance.lastGateID - 1 == TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.jinjizhiliao).Level
                || CharacterRecorder.instance.lastGateID - 1 == TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.kongjiangbing).Level ||
                CharacterRecorder.instance.lastGateID - 1 == TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.wudihudun).Level ||
                CharacterRecorder.instance.lastGateID - 1 == TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zhengbaodan).Level
                 || CharacterRecorder.instance.lastGateID - 1 == TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.jijiuwuzi).Level ||
                 CharacterRecorder.instance.lastGateID - 1 == TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zhanshudaodan).Level ||
                 CharacterRecorder.instance.lastGateID - 1 == TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.jinghua).Level ||
                 CharacterRecorder.instance.lastGateID - 1 == TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.qushan).Level
                 || CharacterRecorder.instance.lastGateID - 1 == TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.luzhang).Level ||
                 CharacterRecorder.instance.lastGateID - 1 == TextTranslator.instance.GetGuildByType((int)TextTranslator.NewGuildIdEnum.zhihuiSkill_3).Level)
            {
                ActivitySkillEffect.SetActive(true);
                ActivityItemEffect.SetActive(false);
                ActivitySkillIcon.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load(string.Format("NewGuide/{0}", _NewGuide.MainIcon), typeof(Texture)) as Texture;
            }
            else
            {
                ActivityItemEffect.SetActive(true);
                ActivitySkillEffect.SetActive(false);
                ActivityIcon.GetComponent<MeshRenderer>().material.mainTexture = Resources.Load(string.Format("NewGuide/{0}", _NewGuide.MainIcon), typeof(Texture)) as Texture;
            }
            Name = _NewGuide.Name;
            message = _NewGuide.MapDes;
            StartCoroutine(MessageEffect());
            UIEventListener.Get(GotoButton).onClick += delegate(GameObject go)
            {
                GotoButtonClick(_NewGuide.NewGuideID);
                CharacterRecorder.instance.GuideID[CharacterRecorder.instance.NowGuideID] = 99;
                //SendGuide();
                SceneTransformer.instance.SendGuide();
            };
        }
    }

    IEnumerator MessageEffect()
    {
        if (GameObject.Find("GateInfoWindow") != null)
        {
            GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().CloseTopContent();
            GameObject.Find("GateInfoWindow").gameObject.SetActive(false);
        }
        yield return new WaitForSeconds(1f);
        //ActivitySkillEffect.transform.Find("WF_UI_JiNengShiFang/GameObject/black_bg").gameObject.SetActive(false);
        //ActivityItemEffect.transform.Find("WF_UI_JiNengShiFang/GameObject/black_bg").gameObject.SetActive(false);
        //yield return new WaitForSeconds(0.1f);
        //gameObject.transform.Find("BlackBG").gameObject.SetActive(false);
        //ActivitySkillEffect.transform.Find("WF_UI_JiNengShiFang/GameObject/black_bg").gameObject.SetActive(true);
        //ActivityItemEffect.transform.Find("WF_UI_JiNengShiFang/GameObject/black_bg").gameObject.SetActive(true);
        message = message.Replace("[$]", "$");
        string messageinfo = message.Split('$')[0];
        ActivityName.GetComponent<UILabel>().text = Name;
        ActivityMessage.GetComponent<UILabel>().text = messageinfo;
        //foreach (var item in messageinfo)
        //{
        //    ActivityMessage.GetComponent<UILabel>().text += item;
        //    yield return new WaitForSeconds(0.02f);
        //}
    }
    void GotoButtonClick(int GotoID)
    {
        switch (GotoID)
        {

            case (int)TextTranslator.NewGuildIdEnum.jingyingguangka: //精英关卡  
                CharacterRecorder.instance.gotoGateID = CharacterRecorder.instance.lastCreamGateID;
                CharacterRecorder.instance.IsOpenCreamCN = true;
                GameObject.Find("MapObject").transform.Find("MapCon").GetComponent<MapWindow>().SetMapTypeUpdate();
                //UIManager.instance.OpenPanel("MapUiWindow", true);//最新精英关卡
                DestroyImmediate(gameObject);
                break;
            case (int)TextTranslator.NewGuildIdEnum.jingjichang://竞技场
                UIManager.instance.OpenPanel("PVPWindow", true);
                break;
            case (int)TextTranslator.NewGuildIdEnum.richangfuben://赏金猎人
                UIManager.instance.OpenPanel("EverydayWindow", true);
                break;
            case (int)TextTranslator.NewGuildIdEnum.zhihuiSkill_2 ://第二个指挥官技能
                break;
            case (int)TextTranslator.NewGuildIdEnum.duobao://夺宝奇兵
                UIManager.instance.OpenPanel("GrabItemWindow", true);
                break;
            case (int)TextTranslator.NewGuildIdEnum.qingbao://情报
                UIManager.instance.OpenPanel("TechWindow", true);
                break;
            case (int)TextTranslator.NewGuildIdEnum.jinjizhiliao://紧急治疗
                UIManager.instance.OpenPanel("TacticsWindow", true);
                break;
            case (int)TextTranslator.NewGuildIdEnum.heroRank://英雄培养
                UIManager.instance.OpenPanel("RoleWindow", true);
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(3);
                break;
            case (int)TextTranslator.NewGuildIdEnum.workdEvent://世界事件
                UIManager.instance.OpenPanel("MapUiWindow", true);
                GameObject.Find("MapUiWindow").GetComponent<MapUiWindow>().TheWorldBtnClick();
                break;
            case (int)TextTranslator.NewGuildIdEnum.kongjiangbing://空降兵
                UIManager.instance.OpenPanel("TacticsWindow", true);
                break;
            case (int)TextTranslator.NewGuildIdEnum.ziyuanzhanling://资源占领
                UIManager.instance.OpenPanel("MapUiWindow", true);
                break;
            case (int)TextTranslator.NewGuildIdEnum.wudihudun://无敌护盾
                UIManager.instance.OpenPanel("TacticsWindow", true);
                break;
            case (int)TextTranslator.NewGuildIdEnum.zuduifuben://组队副本
                UIManager.instance.OpenPanel("TeamCopyWindow", true);
                break;
            case (int)TextTranslator.NewGuildIdEnum.juntuan://军团
                UIManager.instance.OpenPanel("LegionMainNoneWindow", true);
                break;
            case (int)TextTranslator.NewGuildIdEnum.zhengbaodan://震爆弹
                UIManager.instance.OpenPanel("TacticsWindow", true);
                break;
            case (int)TextTranslator.NewGuildIdEnum.conglingmaoxian://敢死队
                NetworkHandler.instance.SendProcess("1501#;");
                break;
            case (int)TextTranslator.NewGuildIdEnum.zuojia://座驾
                UIManager.instance.OpenPanel("StrengEquipWindow", true);
                //NetworkHandler.instance.SendProcess("6501#");
                CharacterRecorder.instance.setEquipTableIndex = 4;
                break;
            case (int)TextTranslator.NewGuildIdEnum.zhengfu://征服
                NetworkHandler.instance.SendProcess("6501#");
                break;
            case (int)TextTranslator.NewGuildIdEnum.qiancuibailian://千锤百炼
                UIManager.instance.OpenPanel("EverydayWindow", true);
                CharacterRecorder.instance.EverydayTab = 2;
                break;
            case (int)TextTranslator.NewGuildIdEnum.bianjingzousi:// 军火走私
                UIManager.instance.OpenPanel("SmuggleWindow", true);
                break;
            case (int)TextTranslator.NewGuildIdEnum.zhihuiSkill_3://震爆弹
                UIManager.instance.OpenPanel("TacticsWindow", true);
                break;
            case (int)TextTranslator.NewGuildIdEnum.jinengtupo ://技能
                UIManager.instance.OpenPanel("RoleWindow", true);
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(6);
                break;
            case (int)TextTranslator.NewGuildIdEnum.jijiuwuzi://急救物质
                UIManager.instance.OpenPanel("TacticsWindow", true);
                break;
            case (int)TextTranslator.NewGuildIdEnum.zhuangbeijinglian://装备精炼
                UIManager.instance.OpenPanel("StrengEquipWindow", true);
                CharacterRecorder.instance.setEquipTableIndex = 1;
                StrengEquipWindow.ClickIndex = 1;
                break;
            case (int)TextTranslator.NewGuildIdEnum.zhanshudaodan://战术导弹
                UIManager.instance.OpenPanel("TacticsWindow", true);
                break;
            case (int)TextTranslator.NewGuildIdEnum.wangzhezhilu://王者之路
                UIManager.instance.OpenPanel("KingRoadWindow", true);
                break;
            case (int)TextTranslator.NewGuildIdEnum.huashijunyan://花式军演
                UIManager.instance.OpenPanel("EverydayWindow", true);
                CharacterRecorder.instance.EverydayTab = 4;
                break;
            case (int)TextTranslator.NewGuildIdEnum.jinghua://净化
                UIManager.instance.OpenPanel("TacticsWindow", true);
                break;
            case (int)TextTranslator.NewGuildIdEnum.shiyanshi://实验室
                UIManager.instance.OpenPanel("LabWindow", true);
                break;
            case (int)TextTranslator.NewGuildIdEnum.jixiantiaozhan://极限挑战
                UIManager.instance.OpenPanel("EverydayWindow", true);
                CharacterRecorder.instance.EverydayTab = 3;
                break;
            case (int)TextTranslator.NewGuildIdEnum.qushan://驱散
                UIManager.instance.OpenPanel("TacticsWindow", true);
                break;
            case (int)TextTranslator.NewGuildIdEnum.luzhang://路障
                UIManager.instance.OpenPanel("TacticsWindow", true);
                break;
            case (int)TextTranslator.NewGuildIdEnum.hewuqi://核武器
                UIManager.instance.OpenPanel("StrengEquipWindow", true);
                CharacterRecorder.instance.setEquipTableIndex = 5;
                break;
            case (int)TextTranslator.NewGuildIdEnum.chongsheng://重生
                UIManager.instance.OpenPanel("RoleWindow", true);
                GameObject.Find("RoleWindow").GetComponent<RoleWindow>().SetTab(1);
                break;
            case (int)TextTranslator.NewGuildIdEnum.guozhan://国战
                UIManager.instance.OpenPanel("MainWindow", true);
                break;
            case (int)TextTranslator.NewGuildIdEnum.hedianzhan://国战
                UIManager.instance.OpenPanel("NuclearwarWindow", true);
                break;
        }
    }
}
