using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform equipmentParent;
    public Transform weaponParent;
    EquipmentSlot[] slots;
    WeaponSlot currentWeapon;
    Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = equipmentParent.GetComponentsInChildren<EquipmentSlot>();
        currentWeapon=weaponParent.GetComponentInChildren<WeaponSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void UpdateUI()
    {
        for(int i = 0; i<slots.Length;i++)
        {
            if (i < inventory.items.Count)
            {
                slots[i].AddItem(inventory.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
        for (int i = 0; i < inventory.items.Count; i++)
        {
            Debug.Log("AHHHHHHHHHH");
            slots[i].ChangeCount(inventory.items[i]);
        }
    }
}
