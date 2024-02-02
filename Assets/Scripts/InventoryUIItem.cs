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
    }

    public void OnClickUseItem()
    {
        InventoryManager.instance.RemoveItem(ItemName.text, 1);
    }

    public void OnClickCancel()
    {
        ItemDescription.SetActive(false);
    }
}
