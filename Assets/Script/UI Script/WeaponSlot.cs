using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    public Image icon;
    ScWeapon Weapon;

    public void ChangeWeapon(ScWeapon newWeapon)
    {
        Weapon = newWeapon;
        //icon.sprite = Weapon.EquipmentIcon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        Weapon = null;

        icon.sprite = null;
        icon.enabled = false;
    }
}
