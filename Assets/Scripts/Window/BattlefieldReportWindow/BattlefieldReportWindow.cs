using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BattlefieldReportWindow : MonoBehaviour
{
    public GameObject myGrid;
    public GameObject exitBtn;
    public GameObject labelPrefab;

    // Use this for initialization
    void Start()
    {
        foreach (Transform tran in GetComponentsInChildren<Transform>())
        {
            tran.gameObject.layer = 9;
        }
        NetworkHandler.instance.SendProcess("8632#;");
        //协议8632
        if (UIEventListener.Get(exitBtn).onClick == null)
        {
            UIEventListener.Get(exitBtn).onClick += delegate(GameObject go)
            {
                DestroyImmediate(this.gameObject);
            };
        }

        //[c1f2fb]19:32  [1dfedf][联盟] [70a5cc]某某某军团[31cc0e]成功占领了[c1f2fb]地点的名字！
        //时间$进攻军团名$进攻姓名$城市id$防守军团名$防守姓名$1打赢0打输


    }

    public void ReseiverMsg_8632(List<string> dataList)
    {
        Debug.LogError("dataList::" + dataList.Count);
        if (dataList.Count > 50)
        {
            int len = dataList.Count - 50;
            dataList.RemoveRange(0, len);
        }
        //最高显示50条
        for (int i = dataList.Count - 1; i >= 0; i--)
        {
            //Debug.LogError(dataList[i]);
            //Fight1(dataList[i]);
            Fight2(dataList[i], i);
        }
        myGrid.GetComponent<UIGrid>().Reposition();
    }
    /*
     1)	当玩家击败了驻守在据点上的玩家，记录：  1:同盟   2：帝国
[ff8c04][同盟][-] [249bd2]军团名称1[-]的[2ee817]玩家名称1[-]击败了[249bd2]军团名称2[-]的驻守军[2ee817]玩家名称2[-]！
      
     */

    public void Fight2(string data, int index)
    {
        string[] dataSplit = data.Split('$');
        if (int.Parse(dataSplit[7]) == 0)//输了
        {
            if (string.IsNullOrEmpty(dataSplit[1]))//进攻方没有军团
            {
                if (string.IsNullOrEmpty(dataSplit[5]))//防守方没有军团
                {
                    /*
         // 1472025455$$梦幻⑦盟$2$24$$萌兄弟族$0$1$
        0     1         2          3        4        5         6      7          8
      时间$进攻军团名$进攻姓名$进攻国家类型$城市id$防守军团名$防守姓名$1打赢0打输$防守国家类型$;
                  输了-----
                     (城市)之战， [防守国家类型]-防守姓名 的  防守军团名 击退了  [进攻国家类型]-进攻姓名  的  进攻军团名
                     (城市)之战， [防守国家类型]-防守姓名 的  防守军团名 击退了  [进攻国家类型]-进攻姓名  的进攻
                     (城市)之战， [进攻国家类型]-进攻姓名 的  进攻城市：城市 失败
                  赢了-----
                     (城市)之战， [进攻国家类型]-进攻姓名 的  进攻军团名 大胜 [防守国家类型]-防守姓名  的  防守军团名 攻下城市：城市
                     (城市)之战， [进攻国家类型]-进攻姓名 的  进攻军团名 浴血奋战，夺取城市：城市
      */
                    GameObject go = NGUITools.AddChild(myGrid, labelPrefab);
                    go.SetActive(true);
                    go.GetComponent<UILabel>().text = string.Format("[c1f2fb]{0}[-] [1dfedf][{1}][-][70a5cc]{2}[-][31cc0e]进攻城市：[-][c1f2fb]{3}，[-][31cc0e]失败![-]",
                                                 GetTimes(dataSplit[0]),//时间
                                                 GetCountry(int.Parse(dataSplit[3])),//进攻国家类型
                                                 dataSplit[2],//进攻名称
                                                 TextTranslator.instance.GetLegionCityByID(int.Parse(dataSplit[4])).CityName);
                }
                else if (!string.IsNullOrEmpty(dataSplit[5]))//防守方有军团
                {
                    GameObject go = NGUITools.AddChild(myGrid, labelPrefab);
                    go.SetActive(true);
                    go.GetComponent<UILabel>().text = string.Format("[c1f2fb]{0}[-] [1dfedf][{1}][-][70a5cc]{3}[-][31cc0e]军团的[-][70a5cc]{2},[-][31cc0e]击退了[-][1dfedf][{4}][-][70a5cc]{5}[-][31cc0e]的进攻！[-]",
                                                  GetTimes(dataSplit[0]),//时间
                        //TextTranslator.instance.GetLegionCityByID(int.Parse(dataSplit[4])).CityName,
                                                GetCountry(int.Parse(dataSplit[8])),//防守国家类型
                                                dataSplit[6],//防守方姓名
                                                dataSplit[5],//防守方军团
                                                GetCountry(int.Parse(dataSplit[3])),//进攻国家类型
                                                dataSplit[2]);//进攻名称
                    //);
                }

            }
            else//进攻方有就团
            {
                if (string.IsNullOrEmpty(dataSplit[5]))//防守方没有军团
                {
                    /*
         // 1472025455$$梦幻⑦盟$2$24$$萌兄弟族$0$1$
                  输了-----
                     (城市)之战， [防守国家类型]-防守姓名 的  防守军团名 击退了  [进攻国家类型]-进攻姓名  的  进攻军团名
                     (城市)之战， [防守国家类型]-防守姓名 的  防守军团名 击退了  [进攻国家类型]-进攻姓名  的进攻
                     (城市)之战， [进攻国家类型]-进攻姓名 的  进攻城市：城市 失败
                  赢了-----
                     (城市)之战， [进攻国家类型]-进攻姓名 的  进攻军团名 大胜 [防守国家类型]-防守姓名  的  防守军团名 攻下城市：城市
                     (城市)之战， [进攻国家类型]-进攻姓名 的  进攻军团名 浴血奋战，夺取城市：城市
        0     1         2          3        4        5         6      7          8
      时间$进攻军团名$进攻姓名$进攻国家类型$城市id$防守军团名$防守姓名$1打赢0打输$防守国家类型$;
      */
                    GameObject go = NGUITools.AddChild(myGrid, labelPrefab);
                    go.SetActive(true);
                    go.GetComponent<UILabel>().text = string.Format("[c1f2fb]{0}[-] [1dfedf][{1}][-][70a5cc]{2}[-][31cc0e]的[-][70a5cc]{3}[-][31cc0e]军团[-][31cc0e]进攻城市：[-][c1f2fb]{4}[-][31cc0e]失败![-]",// [31cc0e]成功占领了 [c1f2fb]{4}！",                     
                        GetTimes(dataSplit[0]),//时间
                        //TextTranslator.instance.GetLegionCityByID(int.Parse(dataSplit[4])).CityName,//城市
                        GetCountry(int.Parse(dataSplit[3])),//进攻国家类型
                        dataSplit[2],//进攻名称
                        dataSplit[1],//进攻方军团
                        TextTranslator.instance.GetLegionCityByID(int.Parse(dataSplit[4])).CityName);
                }
                else if (!string.IsNullOrEmpty(dataSplit[5]))//防守方有军团
                {
                    GameObject go = NGUITools.AddChild(myGrid, labelPrefab);
                    go.SetActive(true);
                    go.GetComponent<UILabel>().text = string.Format("[c1f2fb]{0}[-] [1dfedf][{1}][-][70a5cc]{3}[-][31cc0e]的[-][70a5cc]{2}[-]在[-][c1f2fb]{7}[-][31cc0e]击退了[-][1dfedf][{4}][-][70a5cc]{6}[-][31cc0e]的[-][70a5cc]{5}[-][31cc0e]守卫成功！[-]",
                        GetTimes(dataSplit[0]),//时间
                        //TextTranslator.instance.GetLegionCityByID(int.Parse(dataSplit[4])).CityName,
                        GetCountry(int.Parse(dataSplit[8])),//防守国家类型                                               
                        dataSplit[6],
                        dataSplit[5],//防守名称
                        GetCountry(int.Parse(dataSplit[3])),
                        dataSplit[2],
                        dataSplit[1],
                    TextTranslator.instance.GetLegionCityByID(int.Parse(dataSplit[4])).CityName);
                }
            }
        }
        else//赢了
        {
            if (string.IsNullOrEmpty(dataSplit[1]))//进攻方没有军团
            {
                if (string.IsNullOrEmpty(dataSplit[5]))//防守方没有军团
                {
                    /*
         // 1472025455$$梦幻⑦盟$2$24$$萌兄弟族$0$1$
                  输了-----
                     (城市)之战， [防守国家类型]-防守姓名 的  防守军团名 击退了  [进攻国家类型]-进攻姓名  的  进攻军团名
                     (城市)之战， [防守国家类型]-防守姓名 的  防守军团名 击退了  [进攻国家类型]-进攻姓名  的进攻
                     (城市)之战， [进攻国家类型]-进攻姓名 的  进攻城市：城市 失败
                  赢了-----
                     (城市)之战， [进攻国家类型]-进攻姓名 的  进攻军团名 大胜 [防守国家类型]-防守姓名  的  防守军团名 攻下城市：城市
                     (城市)之战， [进攻国家类型]-进攻姓名 浴血奋战，战胜（城市）的守军！
                     (城市)之战， [进攻国家类型]-进攻姓名 击败了 [防守国家类型]-防守姓名  的  防守军团名 的守军！
                     (城市)之战， [进攻国家类型]-进攻姓名 的  进攻军团名 浴血奋战，夺取城市：城市
        0     1         2          3        4        5         6      7          8
      时间$进攻军团名$进攻姓名$进攻国家类型$城市id$防守军团名$防守姓名$1打赢0打输$防守国家类型$;
      */
                    GameObject go = NGUITools.AddChild(myGrid, labelPrefab);
                    go.SetActive(true);
                    go.GetComponent<UILabel>().text = string.Format("[c1f2fb]{0}[-] [1dfedf][{1}][-][70a5cc]{2}[-][31cc0e]浴血奋战战胜[-][c1f2fb]{3}[-][31cc0e]驻军！[-]",//[31cc0e]成功占领了 [c1f2fb]{4}！",                        
                                                GetTimes(dataSplit[0]),//时间
                        //TextTranslator.instance.GetLegionCityByID(int.Parse(dataSplit[4])).CityName,
                                                GetCountry(int.Parse(dataSplit[3])),//进攻国家类型
                                                dataSplit[2],//进攻名称
                                                TextTranslator.instance.GetLegionCityByID(int.Parse(dataSplit[4])).CityName);
                    //);
                }
                else if (!string.IsNullOrEmpty(dataSplit[5]))//防守方有军团
                {
                    GameObject go = NGUITools.AddChild(myGrid, labelPrefab);
                    go.SetActive(true);
                    go.GetComponent<UILabel>().text = string.Format("[c1f2fb]{0}[-] [1dfedf][{1}][-][70a5cc]{2}[-][31cc0e]击败了[-][1dfedf][{3}][-][70a5cc]{5}[-][31cc0e]的[-][70a5cc]{4}[-][31cc0e]驻军！[-]",
                                                GetTimes(dataSplit[0]),//时间
                        //TextTranslator.instance.GetLegionCityByID(int.Parse(dataSplit[4])).CityName,
                                                GetCountry(int.Parse(dataSplit[3])),//进攻国家类型
                                                dataSplit[2],//进攻名称
                                                GetCountry(int.Parse(dataSplit[8])),
                                                dataSplit[6],
                                                dataSplit[5]);
                    //);
                }
            }
            else//进攻方有军团
            {
                if (string.IsNullOrEmpty(dataSplit[5]))//防守方没有军团
                {
                    /*
         // 1472025455$$梦幻⑦盟$2$24$$萌兄弟族$0$1$
                     * 赢了-----
                     (城市)之战， [进攻国家类型]-进攻姓名 的  进攻军团名 大胜 [防守国家类型]-防守姓名  的  防守军团名 攻下城市：城市
                     (城市)之战， [进攻国家类型]-进攻姓名 浴血奋战，战胜（城市）的守军！
                     (城市)之战， [进攻国家类型]-进攻姓名 击败了 [防守国家类型]-防守姓名  的  防守军团名 的守军！
                     (城市)之战， [进攻国家类型]-进攻姓名 的  进攻军团名 浴血奋战，夺取城市：城市
        0     1         2          3        4        5         6      7          8
      时间$进攻军团名$进攻姓名$进攻国家类型$城市id$防守军团名$防守姓名$1打赢0打输$防守国家类型$;
      */
                    GameObject go = NGUITools.AddChild(myGrid, labelPrefab);
                    go.SetActive(true);
                    go.GetComponent<UILabel>().text = string.Format("[c1f2fb]{0}[-] [1dfedf][{1}][-][70a5cc]{3}[-][31cc0e]的[-][70a5cc]{2}[-][31cc0e]浴血奋战攻下城市：[-][c1f2fb]{4}![-]",
                                            GetTimes(dataSplit[0]),//时间
                        //TextTranslator.instance.GetLegionCityByID(int.Parse(dataSplit[4])).CityName,
                                            GetCountry(int.Parse(dataSplit[3])),//进攻国家类型
                                            dataSplit[2],
                                            dataSplit[1],//进攻名称
                                            TextTranslator.instance.GetLegionCityByID(int.Parse(dataSplit[4])).CityName);
                    //);
                }
                else if (!string.IsNullOrEmpty(dataSplit[5]))//防守方有军团
                {
                    GameObject go = NGUITools.AddChild(myGrid, labelPrefab);
                    go.SetActive(true);
                    // go.GetComponent<UILabel>().text = string.Format("[c1f2fb]{0} [31cc0e]{1}之战，[1dfedf][{2}][31cc0e]-[70a5cc]{3}[31cc0e]的[70a5cc]{4}[31cc0e]军团,[31cc0e]击败了[70a5cc][{5}][31cc0e]-[70a5cc]{6}[31cc0e]的[70a5cc]{7}[31cc0e]军团的驻军,占领城市：[c1f2fb]{8}！",
                    go.GetComponent<UILabel>().text = string.Format("[c1f2fb]{0}[-] [1dfedf][{1}][-][70a5cc]{3}[-][31cc0e]的[-][70a5cc]{2}[-][31cc0e]在[-][c1f2fb]{7}[-][31cc0e]击败了[-][1dfedf][{4}][-][70a5cc]{6}[-][31cc0e]的[-][70a5cc]{5}[-][31cc0e]驻军[-]",
                                                 GetTimes(dataSplit[0]),//时间
                        //TextTranslator.instance.GetLegionCityByID(int.Parse(dataSplit[4])).CityName,
                                                 GetCountry(int.Parse(dataSplit[3])),//进攻国家类型
                                                 dataSplit[2],
                                                 dataSplit[1],//进攻名称
                                                 GetCountry(int.Parse(dataSplit[8])),//进攻国家类型
                                                 dataSplit[6],
                                                 dataSplit[5],
                                                 TextTranslator.instance.GetLegionCityByID(int.Parse(dataSplit[4])).CityName);
                }
            }
        }

    }

    public void Fight3(int success)
    {

    }

    public void Fight1(string data)
    {
        /*
         // 1472025455$$梦幻⑦盟$2$24$$萌兄弟族$0$1$
        0     1         2          3        4        5         6      7          8
      时间$进攻军团名$进攻姓名$进攻国家类型$城市id$防守军团名$防守姓名$1打赢0打输$防守国家类型$;
      */
        // TextTranslator.instance.GetLegionCityByID(2).CityName  //获取城市名称
        string[] dataSplit = data.Split('$');
        //2)	当军团占领了据点（与第一条有可能同时触发），
        if (string.IsNullOrEmpty(dataSplit[5]) && !string.IsNullOrEmpty(dataSplit[1]) && int.Parse(dataSplit[7]) == 1)//军团占领成功
        {
            GameObject go = NGUITools.AddChild(myGrid, labelPrefab);
            go.SetActive(true);
            go.GetComponent<UILabel>().text = string.Format("[c1f2fb]{0}[-]  [1dfedf]{1}[-] [70a5cc]{2}[-] [31cc0e]成功占领了[-] [c1f2fb]{3}！[-]",
                                        GetTimes(dataSplit[0]),//时间
                                        GetCountry(int.Parse(dataSplit[3])),//进攻国家类型
                                        dataSplit[1],//进攻军团
                                        TextTranslator.instance.GetLegionCityByID(int.Parse(dataSplit[4])).CityName);
        }
        else if (string.IsNullOrEmpty(dataSplit[5]) && !string.IsNullOrEmpty(dataSplit[1]) && int.Parse(dataSplit[7]) == 0)//占领失败
        {
            GameObject go = NGUITools.AddChild(myGrid, labelPrefab);
            go.SetActive(true);
            go.GetComponent<UILabel>().text = string.Format("[c1f2fb]{0}[-]  [1dfedf]{1}[-] [70a5cc]{2}[-] [31cc0e]占领[-] [c1f2fb]{3}[-] [31cc0e]失败！[-]",
                                        GetTimes(dataSplit[0]),
                                        GetCountry(int.Parse(dataSplit[3])),
                                        dataSplit[1],
                                        TextTranslator.instance.GetLegionCityByID(int.Parse(dataSplit[4])).CityName);
        }
        //1)	当玩家击败了驻守在据点上的玩家，
        else if (!string.IsNullOrEmpty(dataSplit[5]) && !string.IsNullOrEmpty(dataSplit[1]) && int.Parse(dataSplit[7]) == 1)
        {
            //表示进攻
            /*
        0     1         2          3        4        5         6      7          8
      时间$进攻军团名$进攻姓名$进攻国家类型$城市id$防守军团名$防守姓名$1打赢0打输$防守国家类型$;
      */
            //[ff8c04][同盟][-] [249bd2]军团名称1[-]的[2ee817]玩家名称1[-]击败了[249bd2]军团名称2[-]的驻守军[2ee817]玩家名称2[-]！
            //if (CharacterRecorder.instance.myLegionData.legionName == dataSplit[1])
            //{
            GameObject go = NGUITools.AddChild(myGrid, labelPrefab);
            go.SetActive(true);
            go.GetComponent<UILabel>().text = string.Format("[c1f2fb]{0}[-]  [1dfedf]{1}[-] [70a5cc]{2}[-] [31cc0e]击败了[-] [70a5cc]{3}[-] [31cc0e]成功占领了[-] [c1f2fb]{4}！[-]",
                                        GetTimes(dataSplit[0]),
                                        GetCountry(int.Parse(dataSplit[3])),
                                        dataSplit[1],
                                        dataSplit[5],
                                        TextTranslator.instance.GetLegionCityByID(int.Parse(dataSplit[4])).CityName);
            // }
            //表示抵御成功
            //else
            //{

            //}
        }
        else if (!string.IsNullOrEmpty(dataSplit[5]) && !string.IsNullOrEmpty(dataSplit[1]) && int.Parse(dataSplit[7]) == 0)
        {
            //3)	当玩家成功抵御了其他玩家的进攻（该军团有玩家驻守）
            /*
  0     1         2          3        4        5         6      7          8
时间$进攻军团名$进攻姓名$进攻国家类型$城市id$防守军团名$防守姓名$1打赢0打输$防守国家类型$;
*/
            GameObject go = NGUITools.AddChild(myGrid, labelPrefab);
            go.SetActive(true);
            go.GetComponent<UILabel>().text = string.Format("[c1f2fb]{0}[-]  [1dfedf]{1}[-] [70a5cc]{2}[-] [31cc0e]成功抵御了[-] [70a5cc]{3}[-] [31cc0e]的进攻！[-]",
                                        GetTimes(dataSplit[0]),
                                        GetCountry(int.Parse(dataSplit[8])),
                                        dataSplit[5],//军团名称
                //TextTranslator.instance.GetLegionCityByID(int.Parse(dataSplit[3]).CityName)
                                        dataSplit[1]
                                        );
        }
        //1472024550$$1188冒烟$1$23$YY背锅$军团战1$0$2$
        else if (string.IsNullOrEmpty(dataSplit[1]) && !string.IsNullOrEmpty(dataSplit[2]) && !string.IsNullOrEmpty(dataSplit[5]) && int.Parse(dataSplit[7]) == 0)
        {

            //6)	当玩家没有军团，但击败了驻守在据点上的玩家，
            //       0      1          2   3 4  5 6  7      8
            //8632#Cool12$动感流星战队$ 0$ 26$ $ $ 1$ 0$  1470562500;
            //[c1f2fb]19:32  [1dfedf][联盟] [70a5cc]某某某军团[31cc0e]成功占领了[c1f2fb]地点的名字！
            //[ff8c04][帝国][-][2ee817]玩家名称1[-]击败了[249bd2]军团名称2[-]的驻守军[2ee817]玩家名称2[-]！
            GameObject go = NGUITools.AddChild(myGrid, labelPrefab);
            go.SetActive(true);
            go.GetComponent<UILabel>().text = string.Format("[c1f2fb]{0}[-]  [1dfedf]{1}[-] [70a5cc]{2}[-] [31cc0e]击败了[-] [70a5cc]{3}[-] [c1f2fb]的驻守军！[-]",
                                        GetTimes(dataSplit[0]),
                                        GetCountry(int.Parse(dataSplit[3])),
                                        dataSplit[2],//玩家名称
                //TextTranslator.instance.GetLegionCityByID(int.Parse(dataSplit[4]).CityName),
                                        dataSplit[5]//防守方军团名称
                                        );
        }
        // 1472025455$$梦幻⑦盟$2$24$$萌兄弟族$0$1$
        else if (string.IsNullOrEmpty(dataSplit[1]) && !string.IsNullOrEmpty(dataSplit[2]) && string.IsNullOrEmpty(dataSplit[5]) && int.Parse(dataSplit[7]) == 0)
        {
            GameObject go = NGUITools.AddChild(myGrid, labelPrefab);
            go.SetActive(true);
            go.GetComponent<UILabel>().text = string.Format("[c1f2fb]{0}[-]  [1dfedf]{1}[-] [70a5cc]{2}[-] [31cc0e]击败了[-] [70a5cc]{3}[-] [c1f2fb]的驻守军！[-]",
                                        GetTimes(dataSplit[0]),
                                        GetCountry(int.Parse(dataSplit[3])),
                                        dataSplit[2],//玩家名称
                //TextTranslator.instance.GetLegionCityByID(int.Parse(dataSplit[4]).CityName),
                                        dataSplit[6]//防守方军团名称
                                        );
        }

    }
    /// <summary>
    /// 获取时间，
    /// </summary>
    /// <param name="time">时间戳</param>
    /// <returns></returns>
    string GetTimes(string time)
    {
        return Utils.GetTime(time).ToString("HH:mm");
    }
    /// <summary>
    /// 获取国家的 名称
    /// </summary>
    /// <param name="index">名称索引</param>
    /// <returns>国家名</returns>
    string GetCountry(int index)
    {
        string country = "";

        switch (index)
        {
            case 1:
                country = "同盟";
                break;
            case 2:
                country = "帝国";
                break;
            default:
                break;
        }
        return country;
        //return "[同盟]";
    }
}
