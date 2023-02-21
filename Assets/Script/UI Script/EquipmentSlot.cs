using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlot : MonoBehaviour
{
    public Image icon;
    ScEquipment Item;

    public void AddItem(ScEquipment newItem)
    {
        Item = newItem;
        icon.sprite = Item.EquipmentIcon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        Item = null;

        icon.sprite = null;
        icon.enabled = false;
    }
}
