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
            AtkSwing = -90;
            IsAttack = true;
        }

        if (IsAttack)
        {
            AtkSwing += 720 * Time.deltaTime;
            if (AtkSwing > 90)
            {
                AtkSwing = 0;
                IsAttack = false;
            }
        }
        Quaternion rotation = Quaternion.AngleAxis(angle + AtkSwing, new Vector3(0, 1, 0));
        transform.rotation = rotation;
    }
}
