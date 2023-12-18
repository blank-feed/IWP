using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    string itemname;
    public int itemcat;
    public Sprite itemsprite;

    private void Awake()
    {
        itemname = gameObject.name;
        string[] str = itemname.Split(' ');
        itemname = str[0];
        itemsprite = GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InventoryManager.instance.AddItem(itemname, 1, itemcat, itemsprite);
            Destroy(gameObject);
        }
    }
}
