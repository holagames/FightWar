using UnityEngine;
using System.Collections;

public class ResourceItem : MonoBehaviour {

    public UISprite HeroKuang;
    public UISprite HeroIcon;
    public UILabel CityName;
    public UILabel Condtion;
    GameObject ResourceBoards;
    int OneTime = 1000;
    //string timer;
    public int MapId;
    public int Statue;
    void Start()
    {
        ResourceBoards = GameObject.Find("ResourcesBoard");
        UIEventListener.Get(this.gameObject).onClick = delegate(GameObject go)
        {
            if (CharacterRecorder.instance.GuideID[19] == 2 || CharacterRecorder.instance.GuideID[18] == 3 || CharacterRecorder.instance.GuideID[17] == 1)
            {
                SceneTransformer.instance.AddButtonClick(CharacterRecorder.instance.NowGuideID);
            }
            ResourceBoards.GetComponent<ResourceBoard>().SetSelectKuang(this.name);
            MapWindow mw = GameObject.Find("MapObject").transform.FindChild("MapCon").GetComponent<MapWindow>();
            GameObject.Find("MainCamera").GetComponent<MouseClick>().IsResource = true;
            GameObject.Find("MainCamera").GetComponent<MouseClick>().IsWorldEvev = false;
            GameObject.Find("MainCamera").GetComponent<MouseClick>().IsAction = false;
            GameObject.Find("MainCamera").GetComponent<MouseClick>().IsEnemy = false;
            CharacterRecorder.instance.gotoGateID = mw.ResourceDot[MapId - 90001];
            mw.ResourceMapId = MapId;
            mw.ResItemStatue = Statue;
            mw.SetMapTypeUpdate();
            
        };
    }
    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="_heroKuang"></param>
    /// <param name="_heroIcon"></param>
    /// <param name="_cityName"></param>
    /// <param name="_condtion">statue为1： 战斗力 statue为3：秒</param>
    /// <param name="statue">Item状态 1:推荐战斗力 2:可巡逻 3:巡逻中 4:巡逻完成</param>
    public void Init(string _heroKuang,string _heroIcon,string _cityName,int _condtion,int statue,int _MapId)
    {
        Statue = statue;
        MapId = _MapId;
        CancelInvoke("UpdateTime");
        switch(statue)
        {
            case 1:
                Condtion.text = "[74e5f3]推荐战斗力：" +  _condtion + "[-]";
                break;
            case 2:
                Condtion.text = "[3ee817]可巡逻[-]";
                break;
            case 3:
                OneTime = _condtion;
                InvokeRepeating("UpdateTime",0,1f);
                break;
            case 4:
                Condtion.text = "[b53727]巡逻完成[-]";
                break;
        }
        HeroKuang.spriteName = _heroKuang;
        HeroIcon.spriteName = _heroIcon;
        CityName.text = _cityName;
    }

    void UpdateTime()
    {
        if (OneTime > 0)
        {
            int fenNum = (int)((OneTime / 60) % 60);
            int miao = (int)(OneTime % 60);
            int day = (int)((OneTime / 60) / 60);
            Condtion.text = "[fd792a]正在巡逻 " + ((day < 10) ? "0" : "") + day.ToString() + ":" + ((fenNum < 10) ? "0" : "") + fenNum.ToString() + ":" + ((miao < 10) ? "0" : "") + miao.ToString() + "[-]";
            OneTime -= 1;

        }
        else
        {
            Condtion.text = "[b53727]巡逻完成[-]";
        }
    }
}
