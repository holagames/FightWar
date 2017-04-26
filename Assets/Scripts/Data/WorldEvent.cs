using UnityEngine;
using System.Collections;

public class WorldEvent
{
    public int WorldEventId { get; private set; }
    public string Name { get; private set; }
    //ToGate
    public int GateID { get; private set; }
    //GateId
    public int GatePoint { get; private set; }
    public string ResultDes { get; private set; }
    public WorldEvent(int _WorldEventId, string _Name, int _GateID, int _GatePoint, string _ResultDes)
    {
        this.WorldEventId = _WorldEventId;
        this.Name = _Name;
        this.GateID = _GateID;
        GatePoint = _GatePoint;
        this.ResultDes = _ResultDes;
    }
}
