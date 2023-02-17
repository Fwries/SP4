using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour
{
    [SerializeField] private LayerMask m_LayerMask;
    [SerializeField] private GameObject m_Weapon;

    public ScWeapon scWeapon;
    public Hitbox[] hitBoxes;

    // Mouse click variables
    private bool m_IsMouseClicked;

    // Weapon move and rotate variables
    private Vector3 m_WeaponDir;
    private int m_RotateSpeed;
    private float m_WeaponRotate;
    private float m_WeaponTravelDist;
    private float m_DistTravelled;

    // Weapon cooldown variables
    private bool m_InCooldown;
    private float m_CoolDownTimer;

    void Start()
    {
        WeaponSwitch(scWeapon);

        m_IsMouseClicked = false;

        m_WeaponDir.Set(0.0f, 0.0f, 0.0f);
        // We take the mass into account to slow down the rotation speed
        m_RotateSpeed = 3600 - (180 * scWeapon.Mass);
        m_WeaponRotate = 0.0f;
        // We want take the weapon reach into account
        m_WeaponTravelDist = 2.0f * scWeapon.AtkReach;
        m_DistTravelled = 0.0f;

        m_InCooldown = false;
        m_CoolDownTimer = 0.0f;
    }

    void Update()
    {
        if (m_Weapon == null) { return; }

        float angle = 0.0f;
        if (!m_IsMouseClicked)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, m_LayerMask))
            {
                m_WeaponDir = new Vector3(raycastHit.point.x - transform.position.x,
                                          0.0f,
                                          raycastHit.point.z - transform.position.z);

                // Unity uses left hand coordinate system
                angle = -Mathf.Atan2(m_WeaponDir.z, m_WeaponDir.x) * Mathf.Rad2Deg;
            }
            
            if (!m_InCooldown && Input.GetButtonDown("Fire1"))
            {
                // Set isMouseClicked to true
                m_IsMouseClicked = true;

                // Set the hitboxes to be active so that collision can be detected
                foreach (Hitbox hitbox in hitBoxes)
                    hitbox.active = true;
            }
            else
            {
                transform.rotation = Quaternion.Euler(0.0f, angle, 0.0f);
            }
        }

        if (m_IsMouseClicked)
        {
            if (m_DistTravelled > m_WeaponTravelDist)
            {
                m_WeaponRotate = 0.0f;
                m_IsMouseClicked = false;
                m_InCooldown = true;
                foreach (Hitbox hitbox in hitBoxes)
                    hitbox.active = false;

                // Unparent the weapon, turn it into a top-level object in the hierarchy
                m_Weapon.transform.SetParent(null);
            }
            else
            {
                // Calculate the weapon travel vector (Direction and magnitude)
                // We want to take force (F=ma) into account, but we will use atkSpeed instead of acceleration for now
                // We only consider acceleration after the weapon reached its destination
                float force = scWeapon.Mass * scWeapon.AtkSpeed;
                Vector3 weaponTravelVec = force * m_WeaponTravelDist * m_WeaponDir.normalized * Time.deltaTime;

                // Update the weapon rotation (Again, subtract because Unity uses left hand coordinate system)
                m_WeaponRotate -= m_RotateSpeed * Time.deltaTime;

                // Move and rotate the weapon to simulate weapon throw
                m_Weapon.transform.position += weaponTravelVec;
                m_Weapon.transform.localRotation = Quaternion.Euler(0.0f, 0.0f, angle + m_WeaponRotate);

                // Update the distance travelled for the weapon
                m_DistTravelled += weaponTravelVec.magnitude;
            }
        }

<<<<<<< Updated upstream
        Weapon = Instantiate(_scWeapon.Prefab,
                             new Vector3(transform.position.x + _scWeapon.OffsetX, transform.position.y + _scWeapon.OffsetY, transform.position.z),
                             Quaternion.identity, transform);
        hitBoxes = Weapon.GetComponent<HitboxContainer>().hitboxes;
=======
        /*
        if (m_InCooldown)
        {
            if (m_CoolDownTimer > scWeapon.AtkCooldown)
            {
                m_CoolDownTimer = 0;
                m_InCooldown = false;
            }
            else
            {
                m_CoolDownTimer += Time.deltaTime;
            }
        }
        */
    }

    public void WeaponSwitch(ScWeapon scWeapon)
    {
        if (m_Weapon != null) 
            Destroy(m_Weapon);

        m_Weapon = Instantiate(scWeapon.Prefab,
                                new Vector3(transform.position.x + scWeapon.OffsetX,
                                            transform.position.y + scWeapon.OffsetY,
                                            transform.position.z),
                                Quaternion.identity, transform);
        hitBoxes = m_Weapon.GetComponent<HitboxContainer>().hitboxes;
>>>>>>> Stashed changes
    }
}
