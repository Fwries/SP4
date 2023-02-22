using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class EquipmentSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public TMP_Text ItemCount;
    public GameObject DescriptionBox;
    public TMP_Text Description;
    ScEquipment Item;

    public void AddItem(ScEquipment newItem)
    {
        Item = newItem;
        icon.sprite = Item.EquipmentIcon;
        icon.enabled = true;
        Description.text = Item.Description;
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
            ItemCount.text = (Item.stack + 1).ToString();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(Item!=null)
        {
            DescriptionBox.SetActive(true);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        DescriptionBox.SetActive(false);
    }
}
