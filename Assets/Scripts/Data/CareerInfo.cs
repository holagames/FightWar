using UnityEngine;
using System.Collections;

public class CareerInfo{
    public int CareerID { get; private set; }
    public string CareerName { get; private set; }

    public CareerInfo(int _CareerID, string _CareerName)
    {
        CareerID = _CareerID;
        CareerName = _CareerName;
    }
}
