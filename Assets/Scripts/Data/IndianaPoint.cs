using System.Collections;

public class IndianaPoint
{
    public int ID { get; private set; }

    public int Point { get; private set; }

    public int ItemID1 { get; private set; }

    public int ItemNum1 { get; private set; }
    public int ItemID2 { get; private set; }

    public int ItemNum2 { get; private set; }
    public int ItemID3 { get; private set; }

    public int ItemNum3 { get; private set; }

    public IndianaPoint(int _id,int _point,int _itemid1,int itemnum1,int _itemid2,int itemnum2,int _itemid3,int itemnum3)
    {
        this.ID=_id;
        this.Point=_point;
        this.ItemID1=_itemid1;
        this.ItemNum1 = itemnum1;
        this.ItemID2=_itemid2;
        this.ItemNum2 = itemnum2;
        this.ItemID3=_itemid3;
        this.ItemNum3 = itemnum3;
    }
}
