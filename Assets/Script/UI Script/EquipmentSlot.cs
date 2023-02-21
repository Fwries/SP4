using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EquipmentSlot : MonoBehaviour
{
    public Image icon;
    public TMP_Text ItemCount;
    ScEquipment Item;

    public void AddItem(ScEquipment newItem)
    {
        Item = newItem;
        icon.sprite = Item.EquipmentIcon;
        icon.enabled = true;
        ItemCount.text = " ";
    }

    public void ClearSlot()
    {
        Item = null;

        icon.sprite = null;
        icon.enabled = false;
        ItemCount.text = " ";
    }
    public void ChangeCount(ScEquipment newItem)
    {
        Item = newItem;
        if (Item.stack > 0)
        {
            ItemCount.text = Item.stack.ToString();
        }
    }
}
