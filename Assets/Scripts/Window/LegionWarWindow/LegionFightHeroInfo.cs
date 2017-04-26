using UnityEngine;
using System.Collections;

public class LegionFightHeroInfo : MonoBehaviour {

    //public GameObject HeroObj;
    public int IsAtack;//0  攻击，1守护
    public int WitchRoad;//1,2,3条路
    public int RoadPosition;//每一条路对应位置
    public string CharacterName;
    public int RoleID;
    public int IsWin;//0输1赢
    public int BloodNum;
    public int MaxBloodNum;
    public GameObject BloodInfo;

    public void SetLegionFightHeroInfo(int _IsAtack, int _WitchRoad, int _RoadPosition, string _CharacterName, int _RoleID, int _IsWin, int _BloodNum, int _MaxBloodNum, GameObject _BloodInfo)
    {
        this.IsAtack = _IsAtack;
        this.WitchRoad = _WitchRoad;
        this.RoadPosition = _RoadPosition;
        this.CharacterName = _CharacterName;
        this.RoleID = _RoleID;
        this.IsWin = _IsWin;
        this.BloodNum = _BloodNum;
        this.MaxBloodNum = _MaxBloodNum;
        this.BloodInfo = _BloodInfo;
    }

    public void SetHeroPosition(int _RoadPosition) 
    {
        this.RoadPosition = _RoadPosition;
    }
}
