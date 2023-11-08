using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<InventoryItem> Item_List;

    public GameObject InventoryUI;
    public TextMeshProUGUI InventoryUIText;

    public bool openInventoryUI = false;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        Item_List = new();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            openInventoryUI = !openInventoryUI;
        }

        if (openInventoryUI)
        {
            string thistext = "";
            InventoryUI.SetActive(true);
            SkillManager.instance.SkillTree.SetActive(false);
            if (Item_List.Count == 0)
            {
                InventoryUIText.text = "";
                return;
            }
            for (int i = 0; i < Item_List.Count; i++)
            {
                thistext += Item_List[i].Name + " | " + Item_List[i].quantity + "\n";
                InventoryUIText.text = thistext;
            }
        }
        else
        {
            InventoryUI.SetActive(false);
        }
    }

    public void AddItem(string item_name, int addValue, int catnum)
    {
        for (int i = 0; i < Item_List.Count; i++)
        {
            //duplicate
            if (Item_List[i].Name == item_name)
            {
                //add quantity instead of making new item
                Item_List[i].quantity += addValue;
                return;
            }
        }

        InventoryItem item = new InventoryItem(item_name, addValue, catnum);
        Item_List.Add(item);
    }

    public void RemoveItem(string item_name, int subtractValue)
    {
        for (int i = 0; i < Item_List.Count; i++)
        {
            if (Item_List[i].Name == item_name)
            {
                Item_List[i].quantity -= subtractValue;
                if (Item_List[i].quantity <= 0)
                {
                    Item_List.Remove(Item_List[i]);
                }
            }
        }
    }
}
