using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject Weapon;

    private Vector3 dir;
    private bool IsAttack;
    private float AtkSwing;
    private bool InCooldown;
    private float Cooldown;

    public ScWeapon scWeapon;
    public Hitbox[] hitBoxes;

    void Start()
    {
        WeaponSwitch(scWeapon);
    }

    private void Update()
    {
        if (Weapon == null) { return; }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask) && IsAttack == false)
        {
            //transform.position = raycastHit.point;
            dir = new Vector3(raycastHit.point.x - transform.position.x, raycastHit.point.y - transform.position.y, raycastHit.point.z - transform.position.z);
        }

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (raycastHit.point.z > transform.position.z)
        {
            angle *= -1;
        }

        if (Input.GetButtonDown("Fire1") && IsAttack == false && InCooldown == false)
        {
            Weapon.transform.Rotate(90, 0, 0);
            AtkSwing = -135;
            IsAttack = true;
            for (int i = 0; i < hitBoxes.Length; i++)
            {
                hitBoxes[i].active = true;
            }
        }

        if (IsAttack)
        {
            AtkSwing += 720 * Time.deltaTime / scWeapon.AtkSpeed;
            if (AtkSwing > 45)
            {
                Weapon.transform.Rotate(-90, 0, 0);
                AtkSwing = 0;
                IsAttack = false;
                InCooldown = true;
                for (int i = 0; i < hitBoxes.Length; i++)
                {
                    hitBoxes[i].active = false;
                }
            }
        }

        if (InCooldown)
        {
            if (Cooldown > scWeapon.AtkCooldown)
            {
                Cooldown = 0;
                InCooldown = false;
            }
            else
            {
                Cooldown += Time.deltaTime;
            }
        }

        Quaternion rotation = Quaternion.AngleAxis(angle + AtkSwing, new Vector3(0, 1, 0));
        transform.rotation = rotation;
    }

    public void WeaponSwitch(ScWeapon _scWeapon)
    {
        if (Weapon != null) 
        {
            Destroy(Weapon);
        }

        Weapon = Instantiate(_scWeapon.Prefab, new Vector3(transform.position.x + _scWeapon.OffsetX, transform.position.y + _scWeapon.OffsetY, transform.position.z), Quaternion.identity, transform);
        hitBoxes = Weapon.GetComponent<HitboxContainer>().hitboxes;
    }
}
