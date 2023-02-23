using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowWeapon : MonoBehaviour
{
    private ScWeapon scWeapon;
    private Hitbox[] hitBoxes;

    // Mouse click variables
    private bool IsThrown;
    public bool StopThrow;

    // Weapon move and rotate variables
    private Vector3 m_WeaponDir;
    private int m_RotateSpeed;
    private float m_WeaponRotate;
    private float m_WeaponTravelDist;
    private float m_DistTravelled;
    private float angle;

    public void Throw(Vector3 NEWm_WeaponDir, float NEWangle, ScWeapon NEWscWeapon, Hitbox[] NEWhitBoxes)
    {
        IsThrown = true;

        m_WeaponDir = NEWm_WeaponDir;
        angle = NEWangle;
        scWeapon = NEWscWeapon;
        hitBoxes = NEWhitBoxes;

        // We take the mass into account to slow down the rotation speed
        m_RotateSpeed = 7200;
        // We want take the weapon reach into account
        m_WeaponTravelDist = 5.0f - scWeapon.Mass;

        // Unparent the weapon, turn it into a top-level object in the hierarchy
        transform.SetParent(null);
    }

    void Update()
    {
        if (IsThrown)
        {
            if (m_DistTravelled > m_WeaponTravelDist || StopThrow)
            {
                m_WeaponRotate = 0.0f;
                IsThrown = false;
                StopThrow = false;
                this.gameObject.GetComponent<MeshCollider>().enabled = true;
                foreach (Hitbox hitbox in hitBoxes)
                    hitbox.active = false;
            }
            else
            {
                // Calculate the weapon travel vector (Direction and magnitude)
                // We want to take force (F=ma) into account, but we will use atkSpeed instead of acceleration for now
                // We only consider acceleration after the weapon reached its destination
                float force = scWeapon.Mass * scWeapon.AtkSpeed;
                Vector3 weaponTravelVec = force * m_WeaponTravelDist * m_WeaponDir.normalized * Time.deltaTime;

                // Update the weapon rotation (Again, subtract because Unity uses left hand coordinate system)
                m_WeaponRotate += m_RotateSpeed * Time.deltaTime;

                // Move and rotate the weapon to simulate weapon throw
                transform.position += weaponTravelVec;
                transform.localRotation = Quaternion.Euler(0.0f, angle, m_WeaponRotate);

                // Update the distance travelled for the weapon
                m_DistTravelled += weaponTravelVec.magnitude;
            }
        }
    }
}