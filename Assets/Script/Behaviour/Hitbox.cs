using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hitbox : MonoBehaviour
{
    public int Damage;
    public bool active;

    void OnTriggerEnter(Collider other)
    {
        if (!active) { return; }
        if (other.gameObject.tag == "enemy")
        {
            Animator otherAnim = other.gameObject.GetComponent<Animator>();
            if (!otherAnim.GetBool("IsHit"))
            {
                otherAnim.SetBool("IsHit", true);
                int Health = otherAnim.GetInteger("Health");

                otherAnim.SetInteger("PrevHealth", otherAnim.GetInteger("Health"));
                otherAnim.SetInteger("Health", Health - Damage);

                this.gameObject.GetComponentInParent<HitboxContainer>().OnHit();
            }
        }
        if(other.gameObject.tag=="BreakWall")
        {
            Destroy(other.gameObject);
            transform.gameObject.GetComponentInParent<HitboxContainer>().DestroyWeapon();
        }
    }
}
