using UnityEngine;
using System.Collections;

public class GachaPreview {
    public int ID { get; private set; }
    public int UIType { get; private set; }
    public int ItemID { get; private set; }
    public int Level { get; private set; }

    public GachaPreview(int _ID, int _UIType, int _ItemID, int _Level) 
    {
        this.ID = _ID;
        this.UIType = _UIType;
        this.ItemID = _ItemID;
        this.Level = _Level;
    }
}
