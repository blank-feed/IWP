using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    public List<InventoryItem> Item_List;

    public GameObject InventoryUI;
    public GameObject InventoryGrid;

    //public GameObject egg;
    public GameObject Prefab_InventoryItem;

    public bool openInventoryUI = false;
    bool displayOnce = false;

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
        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("MainLobby"))
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            openInventoryUI = !openInventoryUI;
        }

        if (openInventoryUI)
        {
            InventoryUI.SetActive(true);

            //display things
            if (!displayOnce)
            {
                DisplayItems();
            }
        }
        else
        {
            if (InventoryUI == null)
            {
                InventoryUI = GameObject.Find("InventoryUI");
            }
            if (InventoryGrid == null)
            {
                InventoryGrid = GameObject.Find("inventorygrid");
            }
            UndisplayItems();
            InventoryUI.SetActive(false);
        }
    }

    public void AddItem(string item_name, int addValue, int catnum, Sprite item_sprite)
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

        InventoryItem item = new InventoryItem(item_name, addValue, catnum, item_sprite);
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

    public void DisplayItems()
    {
        for (int i = 0; i < Item_List.Count; i++)
        {
            Image itemImage = Prefab_InventoryItem.transform.Find("IUI_Image").GetComponent<Image>();
            TextMeshProUGUI itemNameText = Prefab_InventoryItem.transform.Find("IUI_Name").GetComponent<TextMeshProUGUI>();
            Transform quantityParent = Prefab_InventoryItem.transform.Find("IUI_Quantity");
            TextMeshProUGUI itemQuantityText = quantityParent.Find("IUI_Number").GetComponent<TextMeshProUGUI>();

            itemImage.sprite = Item_List[i].sprite;
            itemNameText.text = Item_List[i].Name;
            if (Item_List[i].quantity > 1)
            {
                if (!quantityParent.gameObject.activeSelf)
                {
                    quantityParent.gameObject.SetActive(true);
                }
                itemQuantityText.text = Item_List[i].quantity.ToString();
            }
            else
            {
                quantityParent.gameObject.SetActive(false);
            }
            Instantiate(Prefab_InventoryItem, InventoryGrid.transform);
        }
        displayOnce = true;
    }

    void UndisplayItems()
    {
        if (!InventoryGrid.activeSelf)
        {
            return;
        }
        for (int i = 0; i < InventoryGrid.transform.childCount; i++)
        {
            Destroy(InventoryGrid.transform.GetChild(i).gameObject);
        }
        displayOnce = false;
    }
}
