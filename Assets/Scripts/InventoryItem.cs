using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public string Name;
    public int quantity;
    public Sprite sprite;
    public enum Category
    {
        weapon,
        consumable,
        keyItem
    }

    public Category cat;

    public InventoryItem()
    {
        Name = "";
        quantity = 0;
    }

    public InventoryItem(string item_name, int item_quan, int CatNum, Sprite s)
    {
        Name = item_name;
        quantity = item_quan;
        sprite = s;
        switch (CatNum)
        {
            case 0:
                cat = Category.weapon;
                break;
            case 1:
                cat = Category.consumable;
                break;
            case 2:
                cat = Category.keyItem;
                break;
            default:
                break;
        }
    }
}
