using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class WeaponSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image icon;
    public GameObject DescriptionBox;
    public TMP_Text _name;
    public TMP_Text _damage;
    public TMP_Text _type;
    public TMP_Text _cooldown;
    ScWeapon Weapon;

    public void ChangeWeapon(ScWeapon newWeapon)
    {
        Weapon = newWeapon;
        icon.sprite = Weapon.WeaponIcon;
        icon.enabled = true;
        _name.text = Weapon.name;
        _damage.text = Weapon.Description;
        _type.text ="Atk Type: "+Weapon.AtkType.ToString();
        _cooldown.text ="Atk Cooldown: " + Weapon.AtkCooldown.ToString();
    }

    public void ClearSlot()
    {
        Weapon = null;

        icon.sprite = null;
        icon.enabled = false;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (Weapon != null)
        {
            DescriptionBox.SetActive(true);
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        DescriptionBox.SetActive(false);
    }
}
