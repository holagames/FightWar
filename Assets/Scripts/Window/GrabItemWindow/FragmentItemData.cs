using UnityEngine;
using System.Collections;

public class FragmentItemData  {
	public int FiveGrabId { get; private set; } 
    public int FiveGrabNumber{get;private set;}
    public int FiveNumber { get; private set; }

    public int AwardID { get; private set; }
    public int AwardNumber { get; private set; }
    public FragmentItemData(int FiveGrabId,int FiveGrabNumber,int FiveNumber,int AwardID,int AwardNumber)
    {
        this.FiveGrabId = FiveGrabId;
        this.FiveGrabNumber = FiveGrabNumber;
        this.FiveNumber = FiveNumber;
        this.AwardID = AwardID;
        this.AwardNumber = AwardNumber;
    }
}
