using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject Weapon;

    private Vector3 dir;
    private bool IsAttack;
    private float AtkSwing = -45f;

    public ScWeapon scWeapon;
    public Hitbox[] hitBoxes;

    void Start()
    {
        //WeaponSwitch(scWeapon);
    }

    private void Update()
    {
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

        if (Input.GetButtonDown("Fire1"))
        {
            //Weapon.transform.Rotate(Weapon.transform.angle.x - 90, Weapon.transform.angle.y, Weapon.transform.angle.z);
            AtkSwing = -90;
            IsAttack = true;
            for (int i = 0; i < hitBoxes.Length; i++)
            {
                hitBoxes[i].active = true;
            }
        }

        if (IsAttack)
        {
            AtkSwing += 720 * Time.deltaTime / scWeapon.AtkSpeed;
            if (AtkSwing > 90)
            {
                //Weapon.transform.Rotate(Weapon.transform.x + 90, Weapon.transform.y, Weapon.transform.z);
                AtkSwing = 0;
                IsAttack = false;
                for (int i = 0; i < hitBoxes.Length; i++)
                {
                    hitBoxes[i].active = false;
                }
            }
        }
        Quaternion rotation = Quaternion.AngleAxis(angle + AtkSwing, new Vector3(0, 1, 0));
        transform.rotation = rotation;
    }

    public void WeaponSwitch(ScWeapon _scWeapon)
    {

    }
}
