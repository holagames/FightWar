using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Getgemlist : MonoBehaviour {

    public int Id;
    public string Name;
    public int level;
    public int FightValue;
    /// <summary>
    /// 真是战队，假是机器人
    /// </summary>
    public bool IsRole;
    public List<int> RoleId = new List<int>();
}
