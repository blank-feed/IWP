using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIItem : MonoBehaviour
{
    public GameObject ItemDescription;
    public TextMeshProUGUI ItemName;
    public Image ItemImg;
    public TextMeshProUGUI ItemDesc;

    public TextMeshProUGUI IUIItemName;
    public Image IUIItemImg;

    public void OnClickItem()
    {
        ItemDescription.SetActive(true);
        ItemName.text = IUIItemName.text;
        ItemImg.sprite = IUIItemImg.sprite;

        if (ItemName.text == "Egg")
        {
            ItemDesc.text = "Eat egg good for health. Add 10hp";
        }
    }

    public void OnClickUseItem()
    {
        if (ItemName.text == "Egg")
        {
            PlayerManager.instance.health += 10;
            if (PlayerManager.instance.health > PlayerManager.instance.maxHealth)
            {
                PlayerManager.instance.health = PlayerManager.instance.maxHealth;
            }
        }
        InventoryManager.instance.RemoveItem(ItemName.text, 1);
        InventoryManager.instance.UndisplayItems();
        InventoryManager.instance.DisplayItems();
        OnClickCancel();
    }

    public void OnClickCancel()
    {
        ItemDescription.SetActive(false);
    }
}
