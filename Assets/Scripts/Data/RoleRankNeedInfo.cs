using UnityEngine;
using System.Collections;

public class RoleRankNeedInfo
{
    public int heroRank { get; private set; }
    public int heroExNeedPiece { get; private set; }
    public int heroExNeedCoin { get; private set; }
    public int heroUpNeedPiece { get; private set; }
    public int heroUpNeedCoin { get; private set; }
    public int heroConvertPiece { get; private set; }

    public RoleRankNeedInfo(int _heroRank,int _heroExNeedPiece,int _heroExNeedCoin,int _heroUpNeedPiece,int _heroUpNeedCoin,int _heroConvertPiece)
    {
        heroRank = _heroRank;
        heroExNeedPiece = _heroExNeedPiece;
        heroExNeedCoin = _heroExNeedCoin;
        heroUpNeedPiece = _heroUpNeedPiece;
        heroUpNeedCoin = _heroUpNeedCoin;
        heroConvertPiece = _heroConvertPiece;
    }
}
