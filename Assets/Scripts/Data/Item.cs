using System.Collections;

public class Item {

    public enum ItemType
    {
        All,
        Orb,      //宝石
        Fragment,   //碎片
        Used,       //消耗
    }

    public int itemCount { get; private set; }       //数量

    public int itemCode { get; private set; }        //物品码
    public int picCode { get; private set; }         //图片的id
    public string itemName { get; private set; }     //名称
    public int itemGrade { get; private set; }       //品质
    /// <summary>
    /// 宝珠码
    /// </summary>
    public int itemOrbCode { get; private set; }
    /// <summary>
    /// 宝珠等级
    /// </summary>
    public int itemOrbLevel { get; private set; }

    /// <summary>
    /// 宝珠品质
    /// </summary>
    public int itemOrbClass { get; private set; }

    public bool isNew { get; private set; }
    public ItemType itemType { get; private set; }
    public int equipID { get; private set; }
    public int starCoinPrice { get; private set; }   //星币售价
    public int moonCoinPrice { get; private set; }   //月石售价
    public string description { get; private set; }     //描述
    public int skillID { get; private set; }         //技能书技能id
    public int skillLevel { get; private set; }      //技能等级
    /// <summary>
    /// 喂养经验
    /// </summary>
    public string feedExp { get; private set; }

    public int itemLevel { get; private set; }
    public int itemClassLevel { get; private set; }

    public Item(int code)
    {
        this.itemCode = code;
        this.picCode = code;

        TextTranslator.ItemInfo itemInfo = TextTranslator.instance.GetItemByItemCode(code);

        this.itemName = itemInfo.itemName;
        this.itemGrade = itemInfo.itemGrade;
        this.description = itemInfo.itemDescription;
        this.skillID = itemInfo.skillID;
        this.skillLevel = itemInfo.skillLevel;
        this.feedExp = itemInfo.feedExp;

        switch (code.ToString().Substring(0, 1))
        {
            case "1":
            case "3":
                itemType = ItemType.Used;
                break;
            case "7":
                itemType = ItemType.Fragment;
                break;
            case "2":
                itemType = ItemType.Orb;
                break;
        }

    }

    public Item(int code, int count)
    {
        this.itemCode = code;
        this.picCode = code;
        this.itemCount = count;

        TextTranslator.ItemInfo itemInfo = TextTranslator.instance.GetItemByItemCode(code);

        this.itemName = itemInfo.itemName;
        this.itemGrade = itemInfo.itemGrade;
        this.description = itemInfo.itemDescription;
        this.skillID = itemInfo.skillID;
        this.skillLevel = itemInfo.skillLevel;
        this.feedExp = itemInfo.feedExp;
        this.picCode = itemInfo.picID;
    }

    public Item(int newCode, int newCount, int newGrade, int newStarCoinPrice, int newEquipID,int newItemLevel,int newItemClassLevel)
    {
        this.itemCode = newCode;
        this.itemCount = newCount;
        this.itemLevel = newItemLevel;
        this.itemGrade = newGrade;
        this.starCoinPrice = newStarCoinPrice;
        this.equipID = newEquipID;
        this.itemClassLevel = newItemClassLevel;

        TextTranslator.ItemInfo itemInfo = TextTranslator.instance.GetItemByItemCode(newCode);
        this.itemName = itemInfo.itemName;
        this.description = itemInfo.itemDescription;
        this.skillID = itemInfo.skillID;
        this.skillLevel = itemInfo.skillLevel;
        this.feedExp = itemInfo.feedExp;

        switch (newCode.ToString().Substring(0, 1))
        {
            case "1":
            case "3":
                itemType = ItemType.Used;
                break;
            case "7":
                itemType = ItemType.Fragment;
                break;
            case "2":
                itemType = ItemType.Orb;
                break;
        }
    }


    public void AddCount(int count)
    {
        this.itemCount += count;
    }

    public void SetCount(int count)
    {
        this.itemCount = count;
    }

    public void SetitemOrbLevel(int level)
    {
        this.itemOrbLevel = level;
    }
}
