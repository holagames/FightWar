using UnityEngine;
using System.Collections;

public class TowerBoxCostData {

	public int TowerBoxID { get; private set; }
    public int Diamond { get; private set; }
    public TowerBoxCostData(int TowerBoxID, int Diamond)
    {
        this.TowerBoxID = TowerBoxID;
        this.Diamond = Diamond;
        
    }
}
