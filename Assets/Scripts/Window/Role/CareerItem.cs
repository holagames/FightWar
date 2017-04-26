using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CareerItem : MonoBehaviour {

    public UILabel CareerName;
    public UILabel AtkDistance;
    public UILabel MoveDistance;

    public UISprite AtkDiSprite;
    public GameObject RedRen1;
    public GameObject RedRen2;
    public GameObject RedRen3;

    public UISprite MoveDiSprite;
    public GameObject GreedRen1;
    public GameObject GreedRen2;
    public List<GameObject> GreedRenList = new List<GameObject>();

    public void SetCareerInfo(string _careerName,int _atkDistance,int _moveDistance)
    {
        if (_atkDistance == 1022)
        {
            _atkDistance = 1;
        }
        CareerName.text ="[2e7094]职业：[-]" + "[fd792a]"+_careerName+"[-]";
        AtkDistance.text = "[2e7094]攻击距离:[-][fd792a]" + (_atkDistance % 10) + "[-]";
        MoveDistance.text = "[2e7094]移动距离:[-][fd792a]" + _moveDistance + "[-]";

        switch (_atkDistance)
        {
            case 1:
                RedRen1.SetActive(true);
                RedRen2.SetActive(false);
                RedRen3.SetActive(false);
                break;
            case 12:
                RedRen1.SetActive(false);
                RedRen2.SetActive(true);
                RedRen3.SetActive(false);
                break;
            case 123:
                RedRen1.SetActive(false);
                RedRen2.SetActive(false);
                RedRen3.SetActive(true);
                break;
            default:
                RedRen1.SetActive(false);
                RedRen2.SetActive(false);
                RedRen3.SetActive(false);
                Debug.Log("攻击距离参数有误！" + _atkDistance);
                break;
        }
        AtkDiSprite.spriteName = "gj_di0" + (_atkDistance % 10 + 1).ToString();

      /*  switch (_moveDistance)
        {
            case 1:
                GreedRen1.SetActive(false);
                GreedRen2.SetActive(true);
                break;
            case 2:
                GreedRen1.SetActive(true);
                GreedRen2.SetActive(false);
                break;
            default:
                Debug.Log("移动距离参数有误！" + _moveDistance);
                break;
        }*/
        for (int i = 0; i < GreedRenList.Count;i++ )
        {
            if (_moveDistance == 0 || _moveDistance == 1 || _moveDistance == 2 || _moveDistance == 3 || _moveDistance == 4)
            {
                if (i == _moveDistance)
                {
                    GreedRenList[i].SetActive(true);
                }
                else
                {
                    GreedRenList[i].SetActive(false);
                }
            }
        }
        MoveDiSprite.spriteName = "yd_di" + _moveDistance;
    }
}
