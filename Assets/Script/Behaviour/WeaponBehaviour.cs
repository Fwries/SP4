using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    public GameObject Weapon;

    private Vector3 dir;
    private float angle;

    private bool IsAttack;
    private float AtkSwing;
    private float StartSwing;
    private float EndSwing;

    private Vector3 TransformPos;

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

        if (Input.GetButtonDown("Fire1") && IsAttack == false)
        {
            if ((int)scWeapon.AtkType == 1) // Swing
            {
                Weapon.transform.Rotate(-90, 0, 0);
                AtkSwing = StartSwing;
                IsAttack = true;
                foreach (Hitbox hitbox in hitBoxes)
                    hitbox.active = true;
            }
            else if ((int)scWeapon.AtkType == 2) // Stab
            {
                Weapon.transform.Rotate(0, -90, 0);
                Weapon.transform.position -= (Weapon.transform.forward) * -0.2f;
                Weapon.transform.Rotate(0, 90, 0);
                IsAttack = true;

                foreach (Hitbox hitbox in hitBoxes)
                    hitbox.active = true;
            }
            else if ((int)scWeapon.AtkType == 3) // Crush
            {

            }
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
            if ((int)scWeapon.AtkType == 1) // Swing
            {
                AtkSwing += 720 * Time.deltaTime / scWeapon.AtkSpeed;
                if (AtkSwing > EndSwing)
                {
                    Weapon.transform.Rotate(90, 0, 0);
                    AtkSwing = 0;
                    IsAttack = false;

                    foreach (Hitbox hitbox in hitBoxes)
                        hitbox.active = false;
                }
            }
            if ((int)scWeapon.AtkType == 2) // Stab
            {
                Weapon.transform.Rotate(0, -90, 0);
                EndSwing += Time.deltaTime * scWeapon.AtkSpeed;
                Weapon.transform.position -= (Weapon.transform.forward) * EndSwing / scWeapon.AtkReach;
                Weapon.transform.Rotate(0, 90, 0);

                if (EndSwing > (scWeapon.AtkSpeed / scWeapon.AtkReach) / 4)
                {
                    Weapon.transform.position = new Vector3(transform.position.x + scWeapon.OffsetX, transform.position.y + scWeapon.OffsetY, transform.position.z);
                    //Weapon.transform.Rotate(0, 0, -45);
                    EndSwing = 0;
                    IsAttack = false;

                    foreach (Hitbox hitbox in hitBoxes)
                        hitbox.active = false;
                }
            }
        }
        else
        {
            angle = -Mathf.Atan2(dir.z, dir.x) * Mathf.Rad2Deg;
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
        Weapon.GetComponent<MeshCollider>().enabled = false;


        if ((int)scWeapon.AtkType == 1) // Swing
        {
            EndSwing = scWeapon.AtkRange / 4;
            StartSwing = EndSwing * -3;
        }
        else if ((int)scWeapon.AtkType == 3) // Crush
        {

        }

        hitBoxes = Weapon.GetComponent<HitboxContainer>().hitboxes;
    }
}
