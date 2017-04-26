using UnityEngine;
using System.Collections;

public class ShopItemData
{
    public int index { get; private set; }
    public int Id { get; private set; }
    public int count { get; private set; }
    public int grade { get; private set; }
    public int priceType { get; private set; }
    public int priceValue { get; private set; }

    public ShopItemData(int index, int Id, int count, int grade, int priceType, int priceValue)
    {
        this.index = index;
        this.Id = Id;
        this.count = count;
        this.grade = grade;
        this.priceType = priceType;
        this.priceValue = priceValue;
    }
}