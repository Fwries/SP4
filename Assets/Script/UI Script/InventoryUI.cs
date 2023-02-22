using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Transform equipmentParent;
    public Transform weaponParent;
    public GameObject equipmentUI;
    EquipmentSlot[] slots;
    WeaponSlot currentWeapon;
    Inventory inventory;
    // Start is called before the first frame update
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        inventory.onWeaponChangedCallback += UpdateWeapon;
        slots = equipmentParent.GetComponentsInChildren<EquipmentSlot>();
        currentWeapon=weaponParent.GetComponentInChildren<WeaponSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            equipmentUI.SetActive(!equipmentUI.activeSelf);
        }
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
            slots[i].ChangeCount(inventory.items[i]);
        }
    }
    void UpdateWeapon()
    {
        if (inventory.CurrentWeapon != null)
        {
            currentWeapon.ChangeWeapon(inventory.CurrentWeapon);
        }
        else
        {
            currentWeapon.ClearSlot();
        }
    }
}
