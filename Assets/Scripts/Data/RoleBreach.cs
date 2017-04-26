using UnityEngine;
using System.Collections;

public class RoleBreach{

    public int roleId { get; private set; }
    public int rank { get; private set; }
    public int levelCup { get; private set; }
    public int stoneNeed { get; private set; }
    public int debrisNum { get; private set; }
    public int money { get; private set; }

    public RoleBreach(int _roleId, int _rank, int _levelCup, int _stoneNeed,int _debrisNum,int _money)
    {
        this.roleId = _roleId;
        this.rank = _rank;
        this.levelCup = _levelCup;
        this.stoneNeed = _stoneNeed;
        this.debrisNum = _debrisNum;
        this.money = _money;
    }
}
