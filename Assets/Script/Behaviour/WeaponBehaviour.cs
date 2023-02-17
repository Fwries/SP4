using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    public GameObject Weapon;

    private Vector3 dir;
    private bool IsAttack;
    private float AtkSwing;

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
            dir = new Vector3(raycastHit.point.x - transform.position.x,
                              0.0f,
                              raycastHit.point.z - transform.position.z);
        }

        float angle = -Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;

        if (Input.GetButtonDown("Fire1") && IsAttack == false)
        {
            Weapon.transform.Rotate(-90, 0, 0);
            AtkSwing = -135;
            IsAttack = true;
            foreach (Hitbox hitbox in hitBoxes)
                hitbox.active = true;
        }

        if (Input.GetButtonDown("Fire2") && IsAttack == false)
        {

            foreach (Hitbox hitbox in hitBoxes)
                hitbox.active = true;

            Weapon.GetComponent<ThrowWeapon>().Throw(dir, angle, scWeapon, hitBoxes);

            Weapon = null;
            scWeapon = null;
        }

        if (IsAttack)
        {
            AtkSwing += 720 * Time.deltaTime / scWeapon.AtkSpeed;
            if (AtkSwing > 45)
            {
                Weapon.transform.Rotate(90, 0, 0);
                AtkSwing = 0;
                IsAttack = false;

                foreach (Hitbox hitbox in hitBoxes)
                    hitbox.active = false;
            }
        }

        transform.rotation = Quaternion.Euler(0.0f, angle + AtkSwing, 0.0f);
    }

    public void WeaponSwitch(ScWeapon _scWeapon)
    {
        if (Weapon != null)
        {
            Destroy(Weapon);
        }

        scWeapon = _scWeapon;
        Weapon = Instantiate(scWeapon.Prefab, new Vector3(transform.position.x + scWeapon.OffsetX, transform.position.y + scWeapon.OffsetY, transform.position.z), new Quaternion(0, 0, 0, 0), transform);

        hitBoxes = Weapon.GetComponent<HitboxContainer>().hitboxes;
    }
}
