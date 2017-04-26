using UnityEngine;
using System.Collections;

public class EquipRefineCost
{
    public int RefineLevel;
    public int Level;
    public int Exp;
    public int DecoStoneNum;
    public int PieceNum;
    public int DecoGold;
    public EquipRefineCost(int _RefineLevel, int _Level, int _Exp, int _DecoStoneNum, int _PieceNum, int DecoGold)
    {
        RefineLevel = _RefineLevel;
        Level = _Level;
        Exp = _Exp;
        DecoStoneNum = _DecoStoneNum;
        PieceNum = _PieceNum;
        this.DecoGold = DecoGold;
    }
}
