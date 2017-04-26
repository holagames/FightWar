using System.Collections;

public class ItemSort
{
    
    public int ItemSortID { get; private set; }       //
    public int ItemID { get; private set; }        //物品码

    public ItemSort(int ItemSortID, int ItemID)
    {
        this.ItemSortID = ItemSortID;
        this.ItemID = ItemID;
    }

}
