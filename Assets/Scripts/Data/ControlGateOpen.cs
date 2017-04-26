using UnityEngine;
using System.Collections;

public class ControlGateOpen
{
    public int ID { get; set; }
    public int GateID { get; set; }
    public int Features { get; set; }

    public ControlGateOpen(int ID, int GateID, int Features) 
    {
        this.ID = ID;
        this.GateID = GateID;
        this.Features = Features;
    }
}
