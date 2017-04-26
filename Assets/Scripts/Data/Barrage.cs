using UnityEngine;
using System.Collections;

public class Barrage {

    public int ID { get; private set; }

    public int GateID { get; private set; }

    public string Des { get; private set; }

    public int OutTime { get; private set; }

    public int Position { get; private set; }

    public int Color { get; private set; }




    public Barrage(int _ID, int _GateID, string _Des, int _OutTime,
        int _Position , int _Color)
    {
        this.ID = _ID;
        this.GateID = _GateID;
        this.Des = _Des;
        this.OutTime = _OutTime;
        this.Position = _Position;
        this.Color = _Color;

    }
}
